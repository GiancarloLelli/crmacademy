using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Avanade.Xrm.Batches;

namespace DanieleLuca.Luca.Console
{
	//https://www.microsoft.com/en-us/download/confirmation.aspx?id=50032

	class Program
	{
		static void Main(string[] args)
		{
			var conventions = new Conventions
			{
				FilterAssembly = fullAssembyName => fullAssembyName.IndexOf("DanieleLuca.Luca.Console.",0, StringComparison.InvariantCultureIgnoreCase)!=-1
			};
			Bootstrapper.Boot(args, conventions);
		}
	}
}
