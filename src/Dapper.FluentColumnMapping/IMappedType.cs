namespace Dapper.FluentColumnMapping
{
    using System;
    using System.Collections.Generic;

    public interface IMappedType<T> : IMappedType
    {
        void DefineColumnMapping(IColumnMapping<T> columnMapping);
    }

    public interface IMappedType
    {
        /// <summary>Gets the <see cref="Type"/> being mapped.</summary>
        Type MappedType { get; }

        /// <summary>Gets the name of the property/field that has been mapped to the specified column name.</summary>
        /// <param name="columnName">The name of the column.</param>
        /// <returns>The name of the mapped property/field.</returns>
        string this[string columnName] { get; set; }

        /// <summary>Determines whether a property/field has been mapped to the specified column name.</summary>
        /// <param name="columnName">The name of the column.</param>
        /// <returns>
        ///     <c>True</c> if a property/field has been mapped to the specified column; otherwise, <c>false</c>.
        /// </returns>
        bool ColumnHasBeenMapped(string columnName);

        /// <summary>Gets the collection of <see cref="KeyValuePair{TKey, TValue}"/> property to column mappings.</summary>
        IDictionary<string, string> MappedColumns { get; }
    }
}