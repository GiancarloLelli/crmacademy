using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Xrm.Tooling.Connector;

namespace DanieleLuca.Luca.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			System.Console.WriteLine("Forza Napoli");
			CrmServiceClient crmSvc = new CrmServiceClient(ConfigurationManager.ConnectionStrings["CRM"].ConnectionString);
			System.Console.ReadLine();
		}
	}
}
