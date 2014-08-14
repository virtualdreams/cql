using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cql
{
	[Serializable]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class AppAttribute : Attribute
	{
		/// <summary>
		/// Set an alternate name. Thos overrides the default.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Set an alias name.
		/// </summary>
		public string Alias { get; set; }

		/// <summary>
		/// Set a description string for help.
		/// </summary>
		public string Description { get; set; }
	}
}
