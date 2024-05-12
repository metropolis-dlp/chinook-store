using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ChinookStore.Infrastructure.Persistence.Common;

public class TransactionBehaviour<TRequest, TResponse>(
    ApplicationDbContext context,
    ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = default(TResponse);
        var typeName = typeof(TRequest).Name;

        try
        {
            if (context.HasActiveTransaction)
            {
                return await next();
            }

            var strategy = context.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await context.BeginTransactionAsync();

                logger.LogInformation("--- Begin transaction {TransactionId} for {CommandName} ({@Command})", transaction!.TransactionId, typeName, request);
                response = await next();
                logger.LogInformation("--- Commit transaction {TransactionId} for {CommandName}", transaction!.TransactionId, typeName);

                await context.CommitTransactionAsync(transaction);
            });

            return response!;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "--- ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);
            throw;
        }
    }
}
