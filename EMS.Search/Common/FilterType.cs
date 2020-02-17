using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace EMS.Search
{
    public class StringFilter
    {
        public string Equal { get; set; }
        public string NotEqual { get; set; }
        public string Contain { get; set; }
        public string NotContain { get; set; }
        public string StartWith { get; set; }
        public string NotStartWith { get; set; }
        public string EndWith { get; set; }
        public string NotEndWith { get; set; }

        public StringFilter ToLower()
        {
            Equal = Equal?.ToLower();
            NotEqual = NotEqual?.ToLower();
            Contain = Contain?.ToLower();
            NotContain = NotContain?.ToLower();
            StartWith = StartWith?.ToLower();
            NotStartWith = NotStartWith?.ToLower();
            EndWith = EndWith?.ToLower();
            NotEndWith = NotEndWith?.ToLower();
            return this;
        }

        public StringFilter ToUpper()
        {
            Equal = Equal?.ToUpper();
            NotEqual = NotEqual?.ToUpper();
            Contain = Contain?.ToUpper();
            NotContain = NotContain?.ToUpper();
            StartWith = StartWith?.ToUpper();
            NotStartWith = NotStartWith?.ToUpper();
            EndWith = EndWith?.ToUpper();
            NotEndWith = NotEndWith?.ToUpper();
            return this;
        }
    }

    public class IntFilter
    {
        public int? Equal { get; set; }
        public int? NotEqual { get; set; }
        public int? Less { get; set; }
        public int? LessEqual { get; set; }
        public int? Greater { get; set; }
        public int? GreaterEqual { get; set; }
        public bool HasValue
        {
            get
            {
                return Equal.HasValue || NotEqual.HasValue || Less.HasValue || LessEqual.HasValue || Greater.HasValue || GreaterEqual.HasValue;
            }
        }
    }

    public class LongFilter
    {
        public long? Equal { get; set; }
        public long? NotEqual { get; set; }
        public long? Less { get; set; }
        public long? LessEqual { get; set; }
        public long? Greater { get; set; }
        public long? GreaterEqual { get; set; }
        public bool HasValue
        {
            get
            {
                return Equal.HasValue || NotEqual.HasValue || Less.HasValue || LessEqual.HasValue || Greater.HasValue || GreaterEqual.HasValue;
            }
        }
    }

    public class DoubleFilter
    {
        public double? Equal { get; set; }
        public double? NotEqual { get; set; }
        public double? Less { get; set; }
        public double? LessEqual { get; set; }
        public double? Greater { get; set; }
        public double? GreaterEqual { get; set; }
        public bool HasValue
        {
            get
            {
                return Equal.HasValue || NotEqual.HasValue || Less.HasValue || LessEqual.HasValue || Greater.HasValue || GreaterEqual.HasValue;
            }
        }
    }

    public class DecimalFilter
    {
        public decimal? Equal { get; set; }
        public decimal? NotEqual { get; set; }
        public decimal? Less { get; set; }
        public decimal? LessEqual { get; set; }
        public decimal? Greater { get; set; }
        public decimal? GreaterEqual { get; set; }
        public bool HasValue
        {
            get
            {
                return Equal.HasValue || NotEqual.HasValue || Less.HasValue || LessEqual.HasValue || Greater.HasValue || GreaterEqual.HasValue;
            }
        }
    }

    public class DateFilter
    {
        public DateTime? Equal { get; set; }
        public DateTime? NotEqual { get; set; }
        public DateTime? Less { get; set; }
        public DateTime? LessEqual { get; set; }
        public DateTime? Greater { get; set; }
        public DateTime? GreaterEqual { get; set; }
        public bool HasValue
        {
            get
            {
                return Equal.HasValue || NotEqual.HasValue || Less.HasValue || LessEqual.HasValue || Greater.HasValue || GreaterEqual.HasValue;
            }
        }
    }

    public class GuidFilter
    {
        public Guid? Equal { get; set; }
        public Guid? NotEqual { get; set; }
        public List<Guid> In { get; set; }
        public List<Guid> NotIn { get; set; }
    }

    public class IdFilter
    {
        public long? Equal { get; set; }
        public long? NotEqual { get; set; }
        public List<long> In { get; set; }
        public List<long> NotIn { get; set; }
    }



    public static class FilterExtension
    {
        public static string ChangeToEnglishChar(this string str)
        {
            string[] VietNamChar = new string[]
            {
                "aAeEoOuUiIdDyY",
                "áàạảãâấầậẩẫăắằặẳẵ",
                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                "éèẹẻẽêếềệểễ",
                "ÉÈẸẺẼÊẾỀỆỂỄ",
                "óòọỏõôốồộổỗơớờợởỡ",
                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                "úùụủũưứừựửữ",
                "ÚÙỤỦŨƯỨỪỰỬỮ",
                "íìịỉĩ",
                "ÍÌỊỈĨ",
                "đ",
                "Đ",
                "ýỳỵỷỹ",
                "ÝỲỴỶỸ"
            };
            //Replace   
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
            }

            return str;
        }
        public static Guid ToGuid(this string name)
        {
            MD5 md5 = MD5.Create();
            Byte[] myStringBytes = ASCIIEncoding.Default.GetBytes(name);
            Byte[] hash = md5.ComputeHash(myStringBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return Guid.Parse(sb.ToString());
        }
        public static IQueryable<TSource> Where<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> propertyName, StringFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Equal))
                source = source.Where(BuildPredicate(propertyName, "==", filter.Equal));

            if (!string.IsNullOrEmpty(filter.NotEqual))
                source = source.Where(BuildPredicate(propertyName, "!=", filter.NotEqual));

            if (!string.IsNullOrEmpty(filter.Contain))
                source = source.Where(BuildPredicate(propertyName, "Contains", filter.Contain));

            if (!string.IsNullOrEmpty(filter.NotContain))
                source = source.Where(BuildPredicate(propertyName, "NotContains", filter.NotContain));

            if (!string.IsNullOrEmpty(filter.StartWith))
                source = source.Where(BuildPredicate(propertyName, "StartsWith", filter.StartWith));

            if (!string.IsNullOrEmpty(filter.NotStartWith))
                source = source.Where(BuildPredicate(propertyName, "NotStartsWith", filter.NotStartWith));

            if (!string.IsNullOrEmpty(filter.EndWith))
                source = source.Where(BuildPredicate(propertyName, "EndsWith", filter.EndWith));

            if (!string.IsNullOrEmpty(filter.NotEndWith))
                source = source.Where(BuildPredicate(propertyName, "NotEndsWith", filter.NotEndWith));
            return source;
        }
        public static IQueryable<TSource> Where<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> propertyName, IntFilter filter)
        {
            if (filter.Equal.HasValue)
                source = source.Where(BuildPredicate(propertyName, "==", filter.Equal.Value.ToString()));
            if (filter.NotEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, "!=", filter.NotEqual.Value.ToString()));
            if (filter.Less.HasValue)
                source = source.Where(BuildPredicate(propertyName, "<", filter.Less.Value.ToString()));
            if (filter.LessEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, "<=", filter.LessEqual.Value.ToString()));
            if (filter.Greater.HasValue)
                source = source.Where(BuildPredicate(propertyName, ">", filter.Greater.Value.ToString()));
            if (filter.GreaterEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, ">=", filter.GreaterEqual.Value.ToString()));

            return source;
        }
        public static IQueryable<TSource> Where<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> propertyName, DoubleFilter filter)
        {
            if (filter.Equal.HasValue)
                source = source.Where(BuildPredicate(propertyName, "==", filter.Equal.Value.ToString()));
            if (filter.NotEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, "!=", filter.NotEqual.Value.ToString()));
            if (filter.Less.HasValue)
                source = source.Where(BuildPredicate(propertyName, "<", filter.Less.Value.ToString()));
            if (filter.LessEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, "<=", filter.LessEqual.Value.ToString()));
            if (filter.Greater.HasValue)
                source = source.Where(BuildPredicate(propertyName, ">", filter.Greater.Value.ToString()));
            if (filter.GreaterEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, ">=", filter.GreaterEqual.Value.ToString()));

            return source;
        }

        public static IQueryable<TSource> Where<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> propertyName, DecimalFilter filter)
        {
            if (filter.Equal.HasValue)
                source = source.Where(BuildPredicate(propertyName, "==", filter.Equal.Value.ToString()));
            if (filter.NotEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, "!=", filter.NotEqual.Value.ToString()));
            if (filter.Less.HasValue)
                source = source.Where(BuildPredicate(propertyName, "<", filter.Less.Value.ToString()));
            if (filter.LessEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, "<=", filter.LessEqual.Value.ToString()));
            if (filter.Greater.HasValue)
                source = source.Where(BuildPredicate(propertyName, ">", filter.Greater.Value.ToString()));
            if (filter.GreaterEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, ">=", filter.GreaterEqual.Value.ToString()));

            return source;
        }

        public static IQueryable<TSource> Where<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> propertyName, LongFilter filter)
        {
            if (filter.Equal.HasValue)
                source = source.Where(BuildPredicate(propertyName, "==", filter.Equal.Value.ToString()));
            if (filter.NotEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, "!=", filter.NotEqual.Value.ToString()));
            if (filter.Less.HasValue)
                source = source.Where(BuildPredicate(propertyName, "<", filter.Less.Value.ToString()));
            if (filter.LessEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, "<=", filter.LessEqual.Value.ToString()));
            if (filter.Greater.HasValue)
                source = source.Where(BuildPredicate(propertyName, ">", filter.Greater.Value.ToString()));
            if (filter.GreaterEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, ">=", filter.GreaterEqual.Value.ToString()));

            return source;
        }
        public static IQueryable<TSource> Where<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> propertyName, DateFilter filter)
        {
            if (filter.Equal.HasValue)
                source = source.Where(BuildPredicate(propertyName, "==", filter.Equal.Value.ToString()));
            if (filter.NotEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, "!=", filter.NotEqual.Value.ToString()));
            if (filter.Less.HasValue)
                source = source.Where(BuildPredicate(propertyName, "<", filter.Less.Value.ToString()));
            if (filter.LessEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, "<=", filter.LessEqual.Value.ToString()));
            if (filter.Greater.HasValue)
                source = source.Where(BuildPredicate(propertyName, ">", filter.Greater.Value.ToString()));
            if (filter.GreaterEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, ">=", filter.GreaterEqual.Value.ToString()));

            return source;
        }

        public static IQueryable<TSource> Where<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> propertyName, GuidFilter filter)
        {
            if (filter.Equal.HasValue)
                source = source.Where(BuildPredicate(propertyName, "==", filter.Equal.Value.ToString()));
            if (filter.NotEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, "!=", filter.NotEqual.Value.ToString()));
            if (filter.In != null)
                source = source.Where(BuildPredicate(propertyName, "In", filter.In));
            if (filter.NotIn != null)
                source = source.Where(BuildPredicate(propertyName, "NotIn", filter.In));
            return source;
        }

        public static IQueryable<TSource> Where<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> propertyName, IdFilter filter)
        {
            if (filter.Equal.HasValue)
                source = source.Where(BuildPredicate(propertyName, "==", filter.Equal.Value.ToString()));
            if (filter.NotEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, "!=", filter.NotEqual.Value.ToString()));
            if (filter.In != null)
                source = source.Where(BuildPredicate(propertyName, "In", filter.In));
            if (filter.NotIn != null)
                source = source.Where(BuildPredicate(propertyName, "NotIn", filter.In));
            return source;
        }

        public static bool Contains(this Enum source, Enum destination)
        {
            long sourceValue = Convert.ToInt64(source);
            long destValue = Convert.ToInt64(destination);
            return (sourceValue & destValue) == destValue;
        }


        private static readonly TypeInfo QueryCompilerTypeInfo = typeof(QueryCompiler).GetTypeInfo();

        private static readonly FieldInfo QueryCompilerField = typeof(EntityQueryProvider).GetTypeInfo().DeclaredFields.First(x => x.Name == "_queryCompiler");

        private static readonly FieldInfo QueryModelGeneratorField = QueryCompilerTypeInfo.DeclaredFields.First(x => x.Name == "_queryModelGenerator");

        private static readonly FieldInfo DataBaseField = QueryCompilerTypeInfo.DeclaredFields.Single(x => x.Name == "_database");

        private static readonly PropertyInfo DatabaseDependenciesField = typeof(Database).GetTypeInfo().DeclaredProperties.Single(x => x.Name == "Dependencies");


        public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            var enumerator = query.Provider.Execute<IEnumerable<TEntity>>(query.Expression).GetEnumerator();
            var relationalCommandCache = enumerator.Private("_relationalCommandCache");
            var selectExpression = relationalCommandCache.Private<SelectExpression>("_selectExpression");
            var factory = relationalCommandCache.Private<IQuerySqlGeneratorFactory>("_querySqlGeneratorFactory");

            var sqlGenerator = factory.Create();
            var command = sqlGenerator.GetCommand(selectExpression);

            string sql = command.CommandText;
            return sql;
        }
        private static object Private(this object obj, string privateField) => obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);
        private static T Private<T>(this object obj, string privateField) => (T)obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);

        public static Expression<Func<TSource, bool>> BuildPredicate<TSource, TKey, TValue>(Expression<Func<TSource, TKey>> propertyName, string comparison, List<TValue> values)
        {
            var micontain = typeof(List<TValue>).GetMethod("Contains");
            var mc = Expression.Call(Expression.Constant(values), micontain, propertyName.Body);
            Expression body;
            switch (comparison)
            {
                case "NotIn":
                    body = Expression.Not(mc);
                    break;
                default:
                    body = mc;
                    break;
            }

            return Expression.Lambda<Func<TSource, bool>>(body, propertyName.Parameters);
        }

        public static Expression<Func<TSource, bool>> BuildPredicate<TSource, TKey>(Expression<Func<TSource, TKey>> propertyName, string comparison, string value)
        {
            var left = propertyName.Body;
            Expression body;
            switch (comparison)
            {
                case "NotContains":
                    body = Expression.Not(MakeComparison(left, "Contains", value));
                    break;
                case "NotStartsWith":
                    body = Expression.Not(MakeComparison(left, "StartsWith", value));
                    break;
                case "NotEndsWith":
                    body = Expression.Not(MakeComparison(left, "EndsWith", value));
                    break;
                default:
                    body = MakeComparison(left, comparison, value);
                    break;
            }

            return Expression.Lambda<Func<TSource, bool>>(body, propertyName.Parameters);
        }

        private static Expression MakeComparison(Expression left, string comparison, string value)
        {
            switch (comparison)
            {
                case "==":
                    return MakeBinary(ExpressionType.Equal, left, value);
                case "!=":
                    return MakeBinary(ExpressionType.NotEqual, left, value);
                case ">":
                    return MakeBinary(ExpressionType.GreaterThan, left, value);
                case ">=":
                    return MakeBinary(ExpressionType.GreaterThanOrEqual, left, value);
                case "<":
                    return MakeBinary(ExpressionType.LessThan, left, value);
                case "<=":
                    return MakeBinary(ExpressionType.LessThanOrEqual, left, value);
                case "Contains":
                case "StartsWith":
                case "EndsWith":
                    return Expression.Call(MakeString(left), comparison, Type.EmptyTypes, Expression.Constant(value, typeof(string)));
                default:
                    throw new NotSupportedException($"Invalid comparison operator '{comparison}'.");
            }
        }

        private static Expression MakeString(Expression source)
        {
            return source.Type == typeof(string) ? source : Expression.Call(source, "ToString", Type.EmptyTypes);
        }

        private static Expression MakeBinary(ExpressionType type, Expression left, string value)
        {
            object typedValue = value;
            if (left.Type != typeof(string))
            {
                if (string.IsNullOrEmpty(value))
                {
                    typedValue = null;
                    if (Nullable.GetUnderlyingType(left.Type) == null)
                        left = Expression.Convert(left, typeof(Nullable<>).MakeGenericType(left.Type));
                }
                else
                {
                    var valueType = Nullable.GetUnderlyingType(left.Type) ?? left.Type;
                    typedValue = valueType.IsEnum ? Enum.Parse(valueType, value) :
                        valueType == typeof(Guid) ? Guid.Parse(value) :
                        Convert.ChangeType(value, valueType);
                }
            }
            var right = Expression.Constant(typedValue, left.Type);
            return Expression.MakeBinary(type, left, right);
        }
    }
}

