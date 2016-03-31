namespace Dapper.FluentColumnMapping
{
    public sealed class ColumnMapping<T> : IColumnMapping<T>
    {
        string IColumnMapping<T>.ColumnName { get; set; }

        IMappedType<T> IPropertyMapping<T>.MappedType { get; set; }

        string IPropertyMapping<T>.PropertyName { get; set; }
    }
}