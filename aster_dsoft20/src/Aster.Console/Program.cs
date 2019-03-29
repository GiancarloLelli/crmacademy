using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aster.App
{
    class Program
    {
        static void Main(string[] args)
        {
            CrmServiceClient crmSvc = new CrmServiceClient(ConfigurationManager.ConnectionStrings["CRMONLINE"].ConnectionString);

            CreateRecords(crmSvc);

            var singleTodo = RetrieveRecords(crmSvc);
            UpdateRecords(crmSvc, singleTodo);

            singleTodo = RetrieveRecords(crmSvc);
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
            entityDaAggiornare["ava_done"] = true;
            crmSvc.Update(entityDaAggiornare);
        }

        private static EntityReference RetrieveRecords(CrmServiceClient crmSvc)
        {
            var query = new QueryExpression("ava_todo");
            query.ColumnSet = new ColumnSet(true);
            query.TopCount = 10;
            query.NoLock = true;

            query.AddOrder("createdon", OrderType.Descending);
            query.Criteria.AddCondition("ava_done", ConditionOperator.Equal, false);

            var entities = crmSvc.RetrieveMultiple(query);
            EntityReference reference = null;

            foreach (var todo in entities.Entities)
            {
                reference = todo.ToEntityReference();
                var name = todo.GetAttributeValue<string>("ava_name");
                Console.WriteLine($"Name: {name}");
            }

            return reference;
        }

        private static void CreateRecords(CrmServiceClient crmSvc)
        {
            var entity = new Entity("ava_todo");
            entity["ava_name"] = "TODO di Giancarlo";
            entity["ava_duedate"] = DateTime.Now.AddYears(2);
            entity["ava_startdate"] = DateTime.Now;
            entity["ava_done"] = false;
            crmSvc.Create(entity);
        }
    }
}
