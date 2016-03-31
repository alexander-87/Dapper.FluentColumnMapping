namespace Dapper.FluentColumnMapping
{
    public interface IColumnMapping<T> : IPropertyMapping<T>
    {
        string ColumnName { get; set; }
    }
}