namespace Dapper.FluentColumnMapping
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>Class containing extension methods for registering column mappings.</summary>
    /// <remarks>
    ///     <para>
    ///         The extension methods herein sometimes return/accept interfaces and sometimes concrete types.
    ///     </para>
    ///     <para>
    ///         This is intentional so as to have IntelliSense only display the appropriate extension methods when
    ///         chaining calls (i.e. mapping fluently).
    ///     </para>
    ///     <para>
    ///         For example, you shouldn't be able to directly chain multiple calls to
    ///         <see cref="ExtensionMethods.ToColumn{T}(PropertyMapping{T}, string)"/>.
    ///         You should only be able to call that method after having called
    ///         <see cref="ExtensionMethods.MapProperty{T}(IMappedType{T}, Expression{Func{T, string}})"/> or
    ///         <see cref="ExtensionMethods.MapProperty{T}(IColumnMapping{T}, Expression{Func{T, string}})"/>.
    ///     </para>
    /// </remarks>
    public static class ExtensionMethods
    {
        /// <summary>Registers a new column mapping for a specific <see cref="Type"/>.</summary>
        /// <typeparam name="T">The <see cref="Type"/> associated with the column mapping.</typeparam>
        /// <param name="mappings">The <see cref="ColumnMappingCollection"/> to register the mapping with.</param>
        /// <returns>
        ///     An instance of <see cref="MappedType{T}"/> that has been
        ///     registered with the <see cref="ColumnMappingCollection"/>
        ///     provided in the <paramref name="mappings"/> parameter.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the <paramref name="mappings"/> parameter is <c>null</c>.
        /// </exception>
        public static MappedType<T> RegisterType<T>(this ColumnMappingCollection mappings)
        {
            if (mappings == null)
                throw new ArgumentNullException(nameof(mappings));

            var mappedType = new MappedType<T>();
            mappings.Mappings.Add(typeof(T), mappedType);

            return mappedType;
        }

        public static bool TypeHasBeenMapped<T>(this ColumnMappingCollection mappings)
        {
            if (mappings == null)
                throw new ArgumentNullException(nameof(mappings));

            return mappings.Mappings.ContainsKey(typeof(T));
        }

        public static MappedType<T> GetTypeMapping<T>(this ColumnMappingCollection mappings)
        {
            if (mappings == null)
                throw new ArgumentNullException(nameof(mappings));

            return mappings.Mappings[typeof(T)] as MappedType<T>;
        }

        /// <summary>Adds a property/field to the mapping.</summary>
        /// <typeparam name="T">The <see cref="Type"/> associated with the column mapping.</typeparam>
        /// <param name="columnMapping">
        ///     An instance of <see cref="IColumnMapping{T}"/> that has been added to the current mapping.
        /// </param>
        /// <param name="propertyName">
        ///     An <see cref="Expression{T}"/> selecting the property/field to add to the mapping.
        /// </param>
        /// <returns>
        ///     An instance of <see cref="PropertyMapping{T}"/> mapped to the property/field
        ///     specified in the <paramref name="propertyName"/> parameter.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the <paramref name="columnMapping"/> or
        ///     <paramref name="propertyName"/> parameter is <c>null</c>.
        /// </exception>
        public static PropertyMapping<T> MapProperty<T>(this IColumnMapping<T> columnMapping, Expression<Func<T, object>> propertyName)
        {
            if (columnMapping == null)
                throw new ArgumentNullException(nameof(columnMapping));

            return columnMapping.MappedType.MapProperty(propertyName);
        }

        /// <summary>Adds a property/field to the mapping.</summary>
        /// <typeparam name="T">The <see cref="Type"/> associated with the column mapping.</typeparam>
        /// <param name="mappedType">
        ///     An instance of <see cref="IMappedType{T}"/> containing the current mapping.
        /// </param>
        /// <param name="propertyName">
        ///     An <see cref="Expression{T}"/> selecting the property/field to add to the mapping.
        /// </param>
        /// <returns>
        ///     An instance of <see cref="PropertyMapping{T}"/> mapped to the property/field
        ///     specified in the <paramref name="propertyName"/> parameter.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the <paramref name="mappedType"/> or <paramref name="propertyName"/> parameter is <c>null</c>.
        /// </exception>
        public static PropertyMapping<T> MapProperty<T>(this IMappedType<T> mappedType, Expression<Func<T, object>> propertyName)
        {
            if (mappedType == null)
                throw new ArgumentNullException(nameof(mappedType));

            if (propertyName == null)
                throw new ArgumentNullException(nameof(propertyName));

            var propertyMapping = new PropertyMapping<T>() as IPropertyMapping<T>;
            propertyMapping.MappedType = mappedType;
            propertyMapping.PropertyName = GetMemberName(propertyName);

            return propertyMapping as PropertyMapping<T>;
        }

        /// <summary>Maps the specified column name to the provided property/field.</summary>
        /// <typeparam name="T">The <see cref="Type"/> associated with the column mapping.</typeparam>
        /// <param name="propertyMapping">The <see cref="PropertyMapping{T}"/> to associate the column name with.</param>
        /// <param name="columnName">The name of the column to map to the property/field.</param>
        /// <returns>
        ///     An instance of <see cref="ColumnMapping{T}"/> with the column name specified in the
        ///     <paramref name="columnName"/> parameter mapped to the property/field specified in
        ///     the <paramref name="propertyName"/> parameter.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the <paramref name="propertyMapping"/> parameter is <c>null</c>.
        /// </exception>
        public static ColumnMapping<T> ToColumn<T>(this PropertyMapping<T> propertyMapping, string columnName)
        {
            if (propertyMapping == null)
                throw new ArgumentNullException(nameof(propertyMapping));

            var propMapping = propertyMapping as IPropertyMapping<T>;
            propMapping.MappedType[propMapping.PropertyName] = columnName;

            var columnMapping = new ColumnMapping<T>() as IColumnMapping<T>;
            columnMapping.MappedType = propMapping.MappedType;
            columnMapping.PropertyName = propMapping.PropertyName;
            columnMapping.ColumnName = columnName;
            columnMapping.MappedType.DefineColumnMapping(columnMapping);

            return columnMapping as ColumnMapping<T>;
        }

        /// <summary>Registers a collection of mapped types with the <see cref="SqlMapper"/>.</summary>
        /// <param name="columnMappingCollection">
        ///     An instance of <see cref="ColumnMappingCollection"/> containing each of the mappings to be registered.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the <paramref name="columnMappingCollection"/> parameter is <c>null</c>.
        /// </exception>
        public static void RegisterWithDapper(this ColumnMappingCollection columnMappingCollection)
        {
            if (columnMappingCollection == null)
                throw new ArgumentNullException(nameof(columnMappingCollection));

            foreach (var typeMapping in columnMappingCollection)
            {
                Func<Type, string, System.Reflection.PropertyInfo> propertySelector =
                    (type, columnName) =>
                    {
                        // Fall back to the property's name if there isn't an explicit mapping
                        var mappedPropertyName = typeMapping.ColumnHasBeenMapped(columnName)
                                               ? typeMapping[columnName]
                                               : columnName;
                        return type.GetProperty(mappedPropertyName);
                    };

                Type mappedType = typeMapping.MappedType;
                var typeMap = new Dapper.CustomPropertyTypeMap(mappedType, propertySelector);
                Dapper.SqlMapper.SetTypeMap(mappedType, typeMap);
            }
        }

        private static string GetMemberName<T>(Expression<Func<T, object>> expression)
        {
            if (expression == null)
                throw new ArgumentException("The expression cannot be null.");

            return GetMemberName(expression.Body);
        }

        private static string GetMemberName(Expression expression)
        {
            if (expression == null)
                throw new ArgumentException("The expression cannot be null.");

            if (expression is MemberExpression)
            {
                // Reference type property or field
                var memberExpression = (MemberExpression)expression;
                return memberExpression.Member.Name;
            }

            if (expression is MethodCallExpression)
            {
                // Reference type method
                var methodCallExpression = (MethodCallExpression)expression;
                return methodCallExpression.Method.Name;
            }

            if (expression is UnaryExpression)
            {
                // Property, field of method returning value type
                var unaryExpression = (UnaryExpression)expression;
                return GetMemberName(unaryExpression.Operand);
            }

            if (expression is LambdaExpression)
            {
                var lambdaExpression = (LambdaExpression)expression;
                return GetMemberName(lambdaExpression.Body);
            }

            throw new ArgumentException("Invalid expression");
        }
    }
}