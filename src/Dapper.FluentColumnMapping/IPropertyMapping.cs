namespace Dapper.FluentColumnMapping
{
    public interface IPropertyMapping<T>
    {
        IMappedType<T> MappedType { get; set; }

        string PropertyName { get; set; }
    }
}