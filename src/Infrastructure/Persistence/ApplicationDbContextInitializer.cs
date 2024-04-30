using System.Dynamic;
using System.Globalization;
using System.Text.Json;
using ChinookStore.Domain.Common;
using ChinookStore.Domain.Entities;
using ChinookStore.Domain.Enums;
using ChinookStore.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ChinookStore.Infrastructure.Persistence;

public class ApplicationDbContextInitializer(
    ILogger<ApplicationDbContextInitializer> logger,
    ApplicationDbContext context)
{
    public async Task InitializeAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initializing the database");
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeed();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database");
        }
    }

    private async Task TrySeed()
    {
        if (await context.Set<Genre>().AnyAsync())
        {
            return;
        }

        var resourceStream = GetType().Assembly.GetManifestResourceStream(GetType().Namespace + ".InitialData.json");
        var content = await JsonDocument.ParseAsync(resourceStream!);

        var mediaTypesMap = await GetMediaTypesMap();
        var genresMap = DeserializeAndInsertEntities(content, "Genre",
            source => new Genre(source.Name));
        var artistsMap = DeserializeAndInsertEntities(content, "Artist",
            source => new Artist(source.Name));
        var albumsMap = DeserializeAndInsertEntities(content, "Album",
            source => new Album(
                title: source.Title,
                releaseDate: DateOnly.ParseExact(source.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture))
            {
                Genre = genresMap.First(g => g.Id == source.GenreId).Entity,
                Artist = artistsMap.First(a => a.Id == source.ArtistId).Entity
            });
        var tracksMap = DeserializeAndInsertEntities(content, "Track",
            source => new Track(source.Name, source.Composer, source.Milliseconds, source.Bytes, source.UnitPrice)
            {
                MediaType = mediaTypesMap.First(m => m.Id == source.MediaTypeId).MediaType,
                Album = albumsMap.First(a => a.Id == source.AlbumId).Entity
            });

        // * Employees
        var employeesMap = DeserializeAndInsertEntities(content ,"Employee",
            source => new Employee(
                title: source.Title,
                birthDate: DateOnly.ParseExact(source.BirthDate.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                hireDate: DateOnly.ParseExact(source.HireDate.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture))
            {
                User = new User(
                    source.FirstName, source.LastName,
                    new Address(source.Address, source.City, source.State, source.Country, source.PostalCode),
                    new Email(source.Email),
                    new Phone(source.Phone))
            });

        foreach (var source in DeserializeElementsList(content.RootElement.GetProperty("Employee")))
        {
            if (source.ReportsTo is not int reportsTo) continue;

            var employee = employeesMap.First(e => e.Id == source.Id).Entity;
            var manager = employeesMap.First(e => e.Id == reportsTo).Entity;
            employee.SetManager(manager);
        }

        // * Costumers
        var customersMap = DeserializeAndInsertEntities(content, "Customer",
            source => new Customer
            {
                User = new User(
                    source.FirstName, source.LastName,
                    new Address(source.Address, source.City, source.State, source.Country, source.PostalCode),
                    new Email(source.Email),
                    new Phone(source.Phone)),
                SupportRep = employeesMap.First(e => e.Item1 == source.SupportRepId).Item2
            });

        // * Invoices
        var invoicesMap = DeserializeAndInsertEntities(content, "Invoice",
            source =>
            {
                return new Invoice(
                    DateTime.Parse(source.InvoiceDate),
                    source.Total,
                    new Address(
                        source.BillingAddress, source.BillingCity, source.BillingState,
                        source.BillingCountry, source.BillingPostalCode))
                {
                    Customer = customersMap.First(c => c.Id == source.CustomerId).Entity
                };
            });

        // * Invoice Lines
        DeserializeAndInsertEntities(content, "InvoiceLine",
            source => new InvoiceLine(source.UnitPrice, source.Quantity)
            {
                Invoice = invoicesMap.First(i => i.Id == source.InvoiceId).Entity,
                Track = tracksMap.First(t => t.Id == source.TrackId).Entity
            });
        var playlistsMap = DeserializeAndInsertEntities(content, "Playlist",
            source => new Playlist(source.Name));

        DeserializeAndInsertEntities(content, "PlaylistTrack",
            source => new PlaylistTrack
            {
                Playlist = playlistsMap.First(p => p.Id == source.PlaylistId).Entity,
                Track = tracksMap.First(t => t.Id == source.TrackId).Entity
            });

        await context.SaveChangesAsync();
    }

    private async Task<List<(int Id, MediaType MediaType)>> GetMediaTypesMap()
    {
        var aac = await context.Set<MediaType>().FirstAsync(m => m.Id == MediaType.Aac.Id);
        var mpeg = await context.Set<MediaType>().FirstAsync(m => m.Id == MediaType.Mpeg.Id);
        var mpeg4 = await context.Set<MediaType>().FirstAsync(m => m.Id == MediaType.Mpeg4.Id);
        var purchasedAac = await context.Set<MediaType>().FirstAsync(m => m.Id == MediaType.PurchasedAac.Id);
        var protectedAac = await context.Set<MediaType>().FirstAsync(m => m.Id == MediaType.ProtectedAac.Id);

        return
        [
            (aac.Id, aac),
            (mpeg.Id, mpeg),
            (mpeg4.Id, mpeg4),
            (purchasedAac.Id, purchasedAac),
            (protectedAac.Id, protectedAac)
        ];
    }

    private List<(int Id, T Entity)> DeserializeAndInsertEntities<T>(
            JsonDocument content, string arrayName, Func<dynamic, T> generateFunc)
        where T : DomainEntity
    {
        var array = content.RootElement.GetProperty(arrayName);
        var elements = DeserializeElementsList(array);

        var elementsMap = new List<(int Id, T Entity)>();
        foreach (var source in elements)
        {
            var entity = generateFunc(source);
            context.Set<T>().Add(entity);
            if (!((IDictionary<string, object>)source).ContainsKey("Id"))
            {
                continue;
            }

            elementsMap.Add((source.Id, entity));
        }

        return elementsMap;
    }

    private IEnumerable<dynamic> DeserializeElementsList(JsonElement arrayElement)
    {
        return arrayElement.EnumerateArray()
            .Cast<JsonElement?>()
            .Select(genre => DeserializeObject(genre!.Value))
            .ToArray();
    }
    private dynamic DeserializeObject(JsonElement element)
    {
        var result = new ExpandoObject() as IDictionary<string, object>;
        foreach (var property in element.EnumerateObject().Cast<JsonProperty>())
        {
            object? value;
            switch (property.Value.ValueKind)
            {
                case JsonValueKind.Number:
                    if (property.Value.TryGetInt32(out var intValue))
                    {
                        value = intValue;
                    }
                    else if (property.Value.TryGetInt64(out var longValue))
                    {
                        value = longValue;
                    }
                    else if (property.Value.TryGetDecimal(out var decimalValue))
                    {
                        value = decimalValue;
                    }
                    else
                    {
                        throw new FormatException($"Invalid number value: {property.Value.ToString()}");
                    }
                    break;
                case JsonValueKind.String:
                    value = property.Value.GetString();
                    break;
                default:
                    value = null;
                    break;
            }

            result.Add(property.Name, value!);
        }

        return result;
    }
}
