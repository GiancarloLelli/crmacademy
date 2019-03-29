using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanieleConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			//Use the connection string named "MyCRMServer"  
			//from the configuration file  
			CrmServiceClient crmSvc = new CrmServiceClient(ConfigurationManager.ConnectionStrings["MyCRMServer"].ConnectionString);

			//https://www.microsoft.com/en-us/download/confirmation.aspx?id=50032
			
			CreateRecords(crmSvc);
            RetrieveRecords(crmSvc);
            UpdateRecords(crmSvc);
			RetrieveRecords(crmSvc);
			DeleteRecords(crmSvc, RetrieveRecords(crmSvc));
			
			System.Console.ReadLine();
		}

		private static void DeleteRecords(CrmServiceClient crmSvc, EntityReference todo)
		{
			crmSvc.Delete(todo.LogicalName, todo.Id);
		}

		private static void UpdateRecords(CrmServiceClient crmSvc)
		{
			Guid id = RetrieveRecords(crmSvc).Id;
			//esempio di retrieve
			//ColumnSet column = new ColumnSet("ava_name","ava_date");
			Entity entityDaModificare = crmSvc.Retrieve("ava_todo", id, new ColumnSet(false));
			entityDaModificare["ava_name"] = "TODO #modificata";
			crmSvc.Update(entityDaModificare);
		}

		private static void UpdateRecords(CrmServiceClient crmSvc, EntityReference todo)
		{
		
			Entity entityDaModificare = crmSvc.Retrieve("ava_todo", todo.Id, new ColumnSet(false));
			entityDaModificare["ava_name"] = "TODO #modificata";
			crmSvc.Update(entityDaModificare);

		}

		private static void UpdateRecords(CrmServiceClient crmSvc, Guid id)
		{

			Entity entityDaModificare = crmSvc.Retrieve("ava_todo", id, new ColumnSet(false));

			entityDaModificare["ava_name"] = "TODO #modificata";

			crmSvc.Update(entityDaModificare);

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
				Console.WriteLine($"Name:{name}");
			}

			return reference;

		}

		private static void CreateRecords(CrmServiceClient crmSvc)
		{
			var entity = new Entity("ava_todo");
			entity["ava_name"] = "TODO #1";
			entity["ava_deudate"] =  DateTime.Now.AddYears(2);
			entity["ava_startdate"] = DateTime.Now;
			entity["ava_done"] = false;

			crmSvc.Create(entity);

		}
	}
}
