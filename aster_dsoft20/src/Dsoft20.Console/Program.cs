using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Xrm.Tooling.Connector;

namespace Dsoft20.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			System.Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAA");

			string connection_String = ConfigurationManager.ConnectionStrings["CRM"].ConnectionString;

			CrmServiceClient svc = null;
			
			svc = new CrmServiceClient(connection_String);

			if (string.IsNullOrEmpty(svc.ConnectedOrgFriendlyName))
			{
				System.Console.WriteLine("IMPOSSIBILE CONNETTERSI!");
				System.Console.ReadLine();
				return;
			}

			

			System.Console.WriteLine($"Connesso a {svc.ConnectedOrgFriendlyName}");
			System.Console.ReadLine();
		}
	}
}
