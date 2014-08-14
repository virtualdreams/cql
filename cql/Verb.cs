using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace cql
{
	public sealed class Verb
	{
		public List<string> Names { get; private set; }
		public string Description { get; private set; }
		public int Weight { get; private set; }
		public MethodInfo Method { get; private set; }

		public Verb(MethodInfo method)
		{
			Names = new List<string>();
			Method = method;

			Names.Add(Method.Name.ToLowerInvariant());

			var attr = Method.GetAttribute<VerbAttribute>();

			Description = attr.Description;
			Weight = attr.Weight;

			if (!String.IsNullOrEmpty(attr.Alias))
			{
				Names.Add(attr.Alias.ToLowerInvariant());
			}
		}
	}
}
