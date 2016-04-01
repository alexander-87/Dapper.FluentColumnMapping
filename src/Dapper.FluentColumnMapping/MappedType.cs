namespace Dapper.FluentColumnMapping
{
    using System;
    using System.Collections.Generic;

    public sealed class MappedType<T> : IMappedType<T>
    {
        string IMappedType.this[string columnName]
        {
            get { return MappedColumns[columnName]; }
            set { MappedColumns[columnName] = value; }
        }

        bool IMappedType.ColumnHasBeenMapped(string columnName)
        {
            return MappedColumns.ContainsKey(columnName);
        }

        Type IMappedType.MappedType { get { return typeof(T); } }

        public IDictionary<string, string> MappedColumns { get; } = new Dictionary<string, string>();

        void IMappedType<T>.DefineColumnMapping(IColumnMapping<T> columnMapping)
        {
            if (MappedColumns.ContainsKey(columnMapping.ColumnName))
                MappedColumns[columnMapping.ColumnName] = columnMapping.PropertyName;
            else
                MappedColumns.Add(columnMapping.ColumnName, columnMapping.PropertyName);
        }
    }
}