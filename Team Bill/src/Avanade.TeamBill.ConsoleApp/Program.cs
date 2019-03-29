using Avanade.Xrm.Batches;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avanade.TeamBill.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var conventions = new Conventions
			{

				FilterAssembly = FullAssemblyName => FullAssemblyName.IndexOf("Avanade.TeamBill.ConsoleApp", 0, StringComparison.InvariantCultureIgnoreCase) != -1,

			};
			Bootstrapper.Boot(args, conventions);
		   
		}

		
	}
}
