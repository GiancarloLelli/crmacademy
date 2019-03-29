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
			var pre = DateTime.Now;
			CrmServiceClient crmSvc = new CrmServiceClient(ConfigurationManager.ConnectionStrings["CRM"].ConnectionString);
			var post = DateTime.Now;
			System.Console.WriteLine("SECONDI IMPIEGATI "+ (post - pre).Seconds);
			System.Console.ReadLine();
		}
	}
}
