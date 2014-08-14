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
			// init
			Type = type;
			Names = new List<string>();
			var attr = Type.GetAttribute<AppAttribute>();
			
			// add name to list
			Names.Add(Type.Name.ToLowerInvariant());

			Description = attr.Description;

			if(!String.IsNullOrEmpty(attr.Alias))
			{
				Names.Add(attr.Alias.ToLowerInvariant());
			}
		}
	}
}
