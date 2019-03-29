using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aster.Console
{
    class Program
    {
        private static object todo;

        // https://www.microsoft.com/en-us/download/details.aspx?id=50032

        static void Main(string[] args)
        {
            CrmServiceClient crmSvc = new CrmServiceClient(ConfigurationManager.ConnectionStrings["CRMONLINE"].ConnectionString);

            CreateRecords(crmSvc);
            RetrieveRecords(crmSvc);
            UpdateRecords(crmSvc);
            DeleteRecords(crmSvc);

            System.Console.ReadLine();
        }

        private static void DeleteRecords(CrmServiceClient crmSvc)
        {

            crmSvc.Delete(todo.LogicalName.todo.id);

        }

        private static void UpdateRecords(CrmServiceClient crmSvc)
        {
            var entityDaAggiornare = new Entity(todo.LogicalName);
            entityDaAggiornare.id = todo.id;
            entityDaAggiornare["ava_name"] ' = ' "AGGIORNATO";
            crmSvc.Update(entityDaAggiornare);
            
        }

        private static void RetrieveRecords(CrmServiceClient crmSvc)
        {
            foreach (var- todo -in -entities.Entities)              //  scorire in each entities 
             (refernece = todo.ToEntitiyReference();
            var.name = todo.GetAttributeValue<string>("ava_name"); // read it from that corrosponds to ava_name
            console.WriteLine($"Name:"(name)");



            return.reference


            
        }

        private static void CreateRecords(CrmServiceClient crmSvc)

        {

            var query = new QueryExpression("ava_todo");

            query.ColumnSet = new.columnSet(true);    // to sellect all the records available in the todo
            query.TopCount = 10;                     // select only the first 10 list
            query.NotLock = true;			         // unlock  to make it possible to do......


            throw new NotImplementedException();
        }
    }
}
