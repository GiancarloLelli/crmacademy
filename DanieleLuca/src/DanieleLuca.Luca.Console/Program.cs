using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace DanieleLuca.Luca.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			System.Console.WriteLine("Forza Napoli");
			var pre = DateTime.Now;
			CrmServiceClient crmSvc = new CrmServiceClient(ConfigurationManager.ConnectionStrings["CRM"].ConnectionString);

			CreateRecords(crmSvc);

			var lastReference=RetrieveRecords(crmSvc);
			UpdateRecords(crmSvc,lastReference);

			lastReference = RetrieveRecords(crmSvc);
			DeleteRecords(crmSvc,lastReference);

			var post = DateTime.Now;
			System.Console.WriteLine("SECONDI IMPIEGATI "+ (post - pre).Seconds);
			System.Console.ReadLine();
		}

		private static void DeleteRecords(CrmServiceClient crmSvc, EntityReference reference)
		{
			var newEntity = new Entity(reference.LogicalName);

			crmSvc.Delete("ava_todo",newEntity.Id);
		}

		private static void UpdateRecords(CrmServiceClient crmSvc, EntityReference reference )
		{
			var newEntity = new Entity(reference.LogicalName);
			newEntity.Id = reference.Id;
			newEntity["ava_name"] = "Pippo";
			newEntity["ava_done"] = true;
			crmSvc.Update(newEntity);
		}

		private static EntityReference RetrieveRecords(CrmServiceClient crmSvc)
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
			foreach(var item in entities.Entities)
			{
				reference = item.ToEntityReference();
				var name = item.GetAttributeValue<string>("ava_name");
				System.Console.WriteLine($"NAme:{ name}");
			}

			return reference;
		}

		private static void CreateRecords(CrmServiceClient crmSvc)
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
