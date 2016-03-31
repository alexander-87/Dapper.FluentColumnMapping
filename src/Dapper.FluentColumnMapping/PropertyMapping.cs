namespace Dapper.FluentColumnMapping
{
    public sealed class PropertyMapping<T> : IPropertyMapping<T>
    {
        IMappedType<T> IPropertyMapping<T>.MappedType { get; set; }

        string IPropertyMapping<T>.PropertyName { get; set; }
    }
}