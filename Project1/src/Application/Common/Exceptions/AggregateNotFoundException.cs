namespace Project1.Application.Common.Exceptions;

public sealed class AggregateNotFoundException : Exception
{
    private AggregateNotFoundException(string typeName, string id) : base($"Not found resource '{typeName}' with Id '{id}'")
    {
    }

    public static void For<T>(T? entity, Guid id) => For<T>(entity, id.ToString());

    public static void For<T>(T? entity, string id)
    {
        if (entity is null)
            throw new AggregateNotFoundException(typeof(T).Name, id);
    }
}
