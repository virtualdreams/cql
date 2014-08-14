using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace cql
{
	static internal class Extensions
	{
		static public T GetAttribute<T>(this MemberInfo method) where T : Attribute
		{
			var attribute = Attribute.GetCustomAttribute(method, typeof(T));

			return (T)attribute;
		}

		static public T GetAttribute<T>(this MethodInfo method) where T : Attribute
		{
			var attribute = Attribute.GetCustomAttribute(method, typeof(T));

			return (T)attribute;
		}

		static public T GetAttribute<T>(this ParameterInfo method) where T : Attribute
		{
			var attribute = Attribute.GetCustomAttribute(method, typeof(T));

			return (T)attribute;
		}
		
		static public bool None<T>(this IEnumerable<T> collection)
		{
			return collection == null || !collection.Any();
		}

		static public bool HasAttribute<T>(this MethodInfo method) where T : Attribute
		{
			return Attribute.IsDefined(method, typeof(T));
		}

		//static public bool HasAttribute<T>(this Type type) where T : Attribute
		//{
		//	return Attribute.IsDefined(type, typeof(T));
		//}

		static public IEnumerable<MethodInfo> GetMethodsWith<T>(this Type type) where T : Attribute
		{
			return GetAllMethods(type).Where(m => m.HasAttribute<T>());
		}

		static public IEnumerable<MethodInfo> GetAllMethods(this Type type)
		{
			return type.GetMethods(
				BindingFlags.Public |
				BindingFlags.NonPublic |
				BindingFlags.Instance |
				BindingFlags.Static |
				BindingFlags.FlattenHierarchy
			);
		}
	}
}
