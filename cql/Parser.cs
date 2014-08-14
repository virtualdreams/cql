using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace cql
{
	/// <summary>
	/// Command line parser.
	/// </summary>
	public class Parser
	{
		/// <summary>
		/// Run the parser for the given arguments.
		/// </summary>
		/// <param name="args">List of arguments</param>
		public void Run(string[] args, Assembly assembly)
		{
			if (args.None() || args.All(a => String.IsNullOrEmpty(a)))
			{
				return;
			}

			//if (args.Count() == 1)
			//{
			//	GenerateHelp(assembly);
			//	return;
			//}

			if (args.Count() >= 2)
			{
				RunInternal(args, assembly);
			}
		}

		/// <summary>
		/// Parse the arguments and try to run the app.
		/// </summary>
		/// <param name="args">List of arguments</param>
		private void RunInternal(string[] args, Assembly assembly)
		{
			string app = args[0];
			string verb = args[1];
			string[] param = args.Skip(2).ToArray();

			if (app.Equals("help", StringComparison.OrdinalIgnoreCase))
			{
				GenerateHelp(assembly);
				return;
			}

			// get the app
			var _app = (from a in GetApps(assembly)
						where a.Names.Contains(app.ToLowerInvariant())
						select a).Single();

			// create a instance of the app
			var _instance = Activator.CreateInstance(_app.Type.UnderlyingSystemType);

			// get method by name and parameter count
			var _verbs = from m in GetVerbs(_app)
						 where m.Names.Contains(verb.ToLowerInvariant())
						 where m.Method.GetParameters().Length == param.Length
						 select m;

			var _verb = SelectVerb(_verbs, param);
			if (_verb != null)
			{
				_verb.Method.Invoke(_instance, ConvertParameters(_verb, param));
			}
			else
			{
				throw new Exception("Can't find suitable verb.");
			}
		}

		/// <summary>
		/// Get all types that have the AppAttribute.
		/// </summary>
		/// <param name="assembly">The assembly to search for.</param>
		/// <returns></returns>
		private IEnumerable<App> GetApps(Assembly assembly)
		{
			return (from a in assembly.GetTypes()
					where a.IsDefined(typeof(AppAttribute), true)
					select new App(a)).ToList();
		}

		/// <summary>
		/// Get all methods for an app.
		/// </summary>
		/// <param name="app">The app to search for.</param>
		/// <returns></returns>
		private IEnumerable<Verb> GetVerbs(App app)
		{
			return app.Type.GetMethodsWith<VerbAttribute>().Select(v => new Verb(v)).OrderByDescending(o => o.Weight).ToList();
		}

		/// <summary>
		/// Get all parameter for a verb.
		/// </summary>
		/// <param name="verb"></param>
		/// <returns></returns>
		private IEnumerable<Parameter> GetParameters(Verb verb)
		{
			return verb.Method.GetParameters().Select(p => new Parameter(p)).ToList();
		}

		/// <summary>
		/// Try to select a verb if is ambiguous.
		/// </summary>
		/// <param name="verbs"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		private Verb SelectVerb(IEnumerable<Verb> verbs, string[] parameters)
		{
			foreach (var verb in verbs)
			{
				bool pass = true;
				var _parameters = verb.Method.GetParameters();
				for (int i = 0; i < _parameters.Length; i++)
				{
					pass = CanChangeType(parameters[i], _parameters[i].ParameterType);
					if (!pass)
						break;
				}
				if (!pass)
					continue;

				return verb;
			}
			return null;
		}

		/// <summary>
		/// Try to convert the parameters to match the method.
		/// </summary>
		/// <param name="method">The method</param>
		/// <param name="parameters">List of parameters</param>
		/// <returns></returns>
		private object[] ConvertParameters(Verb verb, string[] parameters)
		{
			List<object> p = new List<object>();
			var _parameters = verb.Method.GetParameters();

			for (int i = 0; i < _parameters.Length; i++)
			{
				p.Add(Convert.ChangeType(parameters[i], _parameters[i].ParameterType));
			}

			return p.ToArray();
		}

		/// <summary>
		/// Try to change parameter.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		private bool CanChangeType(object value, Type type)
		{
			try
			{
				Convert.ChangeType(value, type);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		private void CheckForDuplicates(IEnumerable<App> apps)
		{
			
		}

		private void GenerateHelp(Assembly assembly)
		{
			Console.WriteLine("Usage: app verb [value [value [...] ] ]");
			Console.WriteLine();
			
			var _apps = GetApps(assembly);

			foreach (var app in _apps)
			{
				Console.WriteLine("{0}: {1}", app.Names[0], app.Description);

				var _verbs = GetVerbs(app);

				foreach (var verb in _verbs)
				{
					Console.WriteLine("  {0}: {1}", verb.Names[0], verb.Description);

					var _parameters = GetParameters(verb);

					Console.ForegroundColor = ConsoleColor.Cyan;
					foreach (var parameter in _parameters)
					{
						Console.WriteLine("  - {0}:{1}", parameter.Argument.Name, parameter.Argument.ParameterType.Name);
					}
					Console.ResetColor();
				}
				
			}
		}
	}
}
