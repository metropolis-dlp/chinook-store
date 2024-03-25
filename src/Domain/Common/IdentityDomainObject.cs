namespace ChinookStore.Domain.Common;

public class IdentityDomainObject
{
    public int Id { get; init; }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    protected virtual bool Equals(IdentityDomainObject other)
    {
        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        return obj.GetType() == GetType() && Equals((IdentityDomainObject) obj);
    }

}
