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
			// init
			Method = method;
			Names = new List<string>();
			var attr = Method.GetAttribute<VerbAttribute>();


			// add name to list
			if (!String.IsNullOrEmpty(attr.Name))
			{
				Names.Add(attr.Name.ToLowerInvariant());
			}
			else
			{
				Names.Add(Method.Name.ToLowerInvariant());
			}

			Description = attr.Description;
			Weight = attr.Weight;

			if (!String.IsNullOrEmpty(attr.Alias))
			{
				Names.Add(attr.Alias.ToLowerInvariant());
			}
		}
	}
}
