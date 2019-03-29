using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Configuration;

namespace Giancarlo.App
{
    class Program
    {
        // https://www.microsoft.com/en-us/download/details.aspx?id=50032

        static void Main(string[] args)
        {
            CrmServiceClient crmSvc = new CrmServiceClient(ConfigurationManager.ConnectionStrings["CRMONLINE"].ConnectionString);

            CreateRecords(crmSvc);
            RetrieveRecords(crmSvc);
            UpdateRecords(crmSvc);
            DeleteRecords(crmSvc);

            Console.ReadLine();
        }

        private static void DeleteRecords(CrmServiceClient crmSvc)
        {
            throw new NotImplementedException();
        }

        private static void UpdateRecords(CrmServiceClient crmSvc)
        {
            throw new NotImplementedException();
        }

        private static void RetrieveRecords(CrmServiceClient crmSvc)
        {
            throw new NotImplementedException();
        }

        private static void CreateRecords(CrmServiceClient crmSvc)
        {
            throw new NotImplementedException();
        }
    }
}