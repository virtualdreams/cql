cql: Command-line query language
================================

Usage
-----

```c#
class Program
{
	static void Main(string[] args)
	{
		Parser p = new Parser();
		p.Run(args, Assembly.GetExecutingAssembly());
	}
}

[App(Description="Access layer to domain table.")]
public class Domain
{
	[Verb(Description="List all available domains.")]
	public void List()
	{
	
	}

	[Verb(Description="List the domain given by id.")]
	public void List(int id)
	{
		
	}

	[Verb(Description = "List the domain given by name.", Weight=99)]
	public void List(string name)
	{
		
	}
}

[App(Description="Access layer to user table.")]
public class User
{
	[Verb(Description = "List all available users.")]
	public void List()
	{
		
	}

	[Verb(Description = "List the user given by id.")]
	public void List(int id)
	{
		
	}
}
```

	$ myexe domain list
	$ myexe domain list 1
	$ myexe user list