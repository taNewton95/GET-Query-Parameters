using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters.Properties
{
    public static class PropertyFactory
    {

        public static Type FieldOrPropertyType<TSource>(string fieldOrProperty)
        {
            return PropertyType<TSource>(fieldOrProperty) ?? FieldType<TSource>(fieldOrProperty);
        }

        public static Type FieldType<TSource>(string field)
        {
            var fieldInfo = typeof(TSource).GetField(field);
            return fieldInfo?.FieldType;
        }

        public static Type PropertyType<TSource>(string property)
        {
            var propertyInfo = typeof(TSource).GetProperty(property);
            return propertyInfo?.PropertyType;
        }

        public static Expression<Func<TSource, TFieldOrProperty>> FieldOrPropertyExpression<TSource, TFieldOrProperty>(string fieldOrProperty)
        {
            return PropertyExpression<TSource, TFieldOrProperty>(fieldOrProperty) ?? FieldExpression<TSource, TFieldOrProperty>(fieldOrProperty);
        }

        public static Expression<Func<TSource, TField>> FieldExpression<TSource, TField>(string field)
        {
            var genericType = typeof(TSource);

            var fieldInfo = genericType.GetField(field);

            if (fieldInfo == null) return null;

            var param = Expression.Parameter(genericType, "t");

            var fieldExpression = Expression.Field(param, fieldInfo.Name);

            return Expression.Lambda<Func<TSource, TField>>(fieldExpression, param);
        }

        public static Expression<Func<TSource, TField>> PropertyExpression<TSource, TField>(string property)
        {
            var genericType = typeof(TSource);

            var propertyInfo = genericType.GetProperty(property);

            if (propertyInfo == null) return null;

            var param = Expression.Parameter(genericType, "t");

            var propertyExpression = Expression.Property(param, propertyInfo.Name);

            return Expression.Lambda<Func<TSource, TField>>(propertyExpression, param);
        }

    }
}
