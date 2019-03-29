using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Configuration;

namespace Giancarlo.App
{
    class Program
    {
        static void Main(string[] args)
        {
            CrmServiceClient crmSvc = new CrmServiceClient(ConfigurationManager.ConnectionStrings["CRMONLINE"].ConnectionString);
            Console.ReadLine();
        }
    }
}