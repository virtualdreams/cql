using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace cql
{
	public class Parameter
	{
		public string Name { get; set; }
		public string Description { get; private set; }
		public ParameterInfo Argument { get; private set; }

		public Parameter(ParameterInfo parameter)
		{
			Argument = parameter;
		}
	}
}
