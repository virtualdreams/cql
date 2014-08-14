using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cql
{
	public sealed class App
	{
		public List<string> Names { get; private set; }
		public string Description { get; private set; }
		public Type Type { get; private set; }

		public App(Type type)
		{
			Names = new List<string>();
			Type = type;

			Names.Add(Type.Name.ToLowerInvariant());

			var attr = Type.GetAttribute<AppAttribute>();

			Description = attr.Description;

			if(!String.IsNullOrEmpty(attr.Alias))
			{
				Names.Add(attr.Alias.ToLowerInvariant());
			}
		}
	}
}
