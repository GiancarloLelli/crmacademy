using Avanade.Xrm.Batches;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;


namespace DanieleLuca.Luca.Console.Workers
{
    class CRUDBatch : IFlow
    {
        public void Execute(IFlowContext context)
        {
            var crmSvc = context.Service;
            CreateRecords(crmSvc);

            var lastReference = RetrieveRecords(crmSvc);
            UpdateRecords(crmSvc, lastReference);

            lastReference = RetrieveRecords(crmSvc);
            DeleteRecords(crmSvc, RetrieveRecords(crmSvc));
        }


        private void DeleteRecords(IOrganizationService crmSvc, EntityReference reference)
        {
            var newEntity = new Entity(reference.LogicalName);

            crmSvc.Delete("ava_todo", newEntity.Id);
        }

        private void UpdateRecords(IOrganizationService crmSvc, EntityReference reference)
        {
            var newEntity = new Entity(reference.LogicalName);
            newEntity.Id = reference.Id;
            newEntity["ava_name"] = "Pippo";
            newEntity["ava_done"] = true;
            crmSvc.Update(newEntity);
        }

        private EntityReference RetrieveRecords(IOrganizationService crmSvc)
        {

            var query = new QueryExpression("ava_todo");

            // Si segnalano le varie opzioni tra cui  le colonne da prendere
            // Query.ColumnSet = new ColumnSet("ava_name"); colonna singola
            query.ColumnSet = new ColumnSet(true);
            query.TopCount = 10;
            query.AddOrder("createdon", OrderType.Descending);
            query.Criteria.AddCondition("ava_done", ConditionOperator.Equal, false);

            var entities = crmSvc.RetrieveMultiple(query);

            //setta riferimento default
            EntityReference reference = null;

            //ciclo dei record e popola le reference
            foreach (var item in entities.Entities)
            {
                reference = item.ToEntityReference();
                var name = item.GetAttributeValue<string>("ava_name");
                System.Console.WriteLine($"NAme:{ name}");
            }

            return reference;
        }

        private void CreateRecords(IOrganizationService crmSvc)
        {
            var entity = new Entity("ava_todo");
            entity["ava_name"] = "TODO-1";
            entity["ava_duedate"] = DateTime.Now.AddYears(2);
            entity["ava_startdate"] = DateTime.Now;
            entity["ava_done"] = false;

            crmSvc.Create(entity);
        }
    }
}
