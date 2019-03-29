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
			Console.WriteLine("Ciao Avanade");
		
		    CrmServiceClient crmSvc = new CrmServiceClient(ConfigurationManager.ConnectionStrings["CRMOnline"].ConnectionString);

			Console.ReadLine();
		}
	}
}
