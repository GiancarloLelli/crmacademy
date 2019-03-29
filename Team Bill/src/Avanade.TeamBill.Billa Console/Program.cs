using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billa_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("CIAO MONDO");

            //Use the connection string named "MyCRMServer"  
            //from the configuration file  
            CrmServiceClient crmSvc = new CrmServiceClient(System.Configuration.ConfigurationManager.ConnectionStrings["CRM"].ConnectionString);

            CreateRecords(crmSvc);
           var singleTodo = RetrieveRecords(crmSvc);
            UpdateRecords(crmSvc, singleTodo);
            DeleteRecords(crmSvc, singleTodo);
           
                 Console.ReadLine();

        }

        private static void DeleteRecords(CrmServiceClient crmSvc, EntityReference todo)
        {
            crmSvc.Delete(todo.LogicalName, todo.Id);
        }

        private static void UpdateRecords(CrmServiceClient crmSvc, EntityReference todo)
        {
            var entityDaAggiornare = new Entity(todo.LogicalName);
            entityDaAggiornare.Id = todo.Id;
            entityDaAggiornare["ava_name"] = "AGGIORNATO";
            entityDaAggiornare["ava_duedate"] = DateTime.Now.AddDays(2);
            entityDaAggiornare["ava_startdate"] = DateTime.Now;
            entityDaAggiornare["ava_done"] = true;

            crmSvc.Update(entityDaAggiornare);

        }

        private static EntityReference RetrieveRecords(CrmServiceClient crmSvc) // abbiamo messo EntityReference al posto di void perchè, non ci serviva, ci serviva solo un riferimento. solo 2 cose indentifica, Id e il logical record.
        {
            var query = new QueryExpression("ava_todo");
            query.ColumnSet = new ColumnSet(true);  // le query, in retrieve le usiamo per far leggere il codice.
            query.TopCount = 10;                      // essendo poche, usiamo ColunmSet (true) che le legge tutte, ma non è molto utile se ce ne sono di più, perchè renderebbe il tutto più pesante.
            query.NoLock = true;

            query.AddOrder("createdon", OrderType.Descending);
            query.Criteria.AddCondition("ava_done", ConditionOperator.Equal, false);

            var entities = crmSvc.RetrieveMultiple(query);
            EntityReference reference = null;
            foreach (var todo in entities.Entities)
            {
                reference = todo.ToEntityReference();
                var name = todo.GetAttributeValue<string>("ava_name"); // GetAttributeValue è un metodo safe, se non trova ava_name ci dà Null. la usiamo quando ci serve restituire un valore.
                Console.WriteLine($"Name : {name}");
            }


            return reference;

                
        }

        private static void CreateRecords(CrmServiceClient crmSvc)
        {
            var entity = new Entity("ava_todo");
            entity["ava_name"] = "TODO n°1";
            entity["ava_duedate"] = DateTime.Now.AddHours(1);
            entity["ava_startdate"] = DateTime.Now;
            entity["ava_done"] = false;
           



            crmSvc.Create(entity);
            
          
        }
    }
}
