using cql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CmdQueryLanguage
{
	class Program
	{
		static void Main(string[] args)
		{
			Parser p = new Parser();
			p.Run(args, Assembly.GetExecutingAssembly());
		}
	}

	[App(Description="Access layer to domain table.", Alias="Domains")]
	public class Domain
	{
		[Verb(Description="List all available domains.")]
		public void List()
		{
			Console.WriteLine("domain list");
		}

		[Verb(Description="List the domain given by id.")]
		public void List(int id)
		{
			Console.WriteLine("domain list by id {0}", id);
		}

		[Verb(Description = "List the domain given by float.")]
		public void List(float id)
		{
			Console.WriteLine("domain list by float {0}", id);
		}

		[Verb(Description = "List the domain given by name.", Weight=99)]
		public void List(string name)
		{
			Console.WriteLine("domain list by name {0}", name);
		}

		[Verb(Description = "Test")]
		public void Test(int id)
		{
			Console.WriteLine("Test");
		}
	}

	[App(Description="Access layer to user table.")]
	public class User
	{
		[Verb(Description = "List all available users.")]
		public void List()
		{
			Console.WriteLine("user list");
		}

		[Verb(Description = "List the user given by id.")]
		public void List(int id)
		{
			Console.WriteLine("user list {0}", id);
		}
	}

	

	

	
}
