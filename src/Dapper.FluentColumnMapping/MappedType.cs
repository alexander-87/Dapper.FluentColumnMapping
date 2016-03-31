namespace Dapper.FluentColumnMapping
{
    using System;
    using System.Collections.Generic;

    public sealed class MappedType<T> : IMappedType<T>
    {
        private readonly IDictionary<string, string> columnMaps = new Dictionary<string, string>();

        string IMappedType.this[string columnName]
        {
            get { return columnMaps[columnName]; }
            set { columnMaps[columnName] = value; }
        }

        bool IMappedType.ColumnHasBeenMapped(string columnName)
        {
            return columnMaps.ContainsKey(columnName);
        }

        Type IMappedType.MappedType { get { return typeof(T); } }

        void IMappedType<T>.DefineColumnMapping(IColumnMapping<T> columnMapping)
        {
            if (columnMaps.ContainsKey(columnMapping.ColumnName))
            {
                columnMaps[columnMapping.ColumnName] = columnMapping.PropertyName;
            }
            else
            {
                columnMaps.Add(columnMapping.ColumnName, columnMapping.PropertyName);
            }
        }
    }
}