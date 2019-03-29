using Avanade.Xrm.Batches;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;


namespace Dsoft20.Console.Workers
{
	public class CRUDBatch : IFlow
	{
		public void Execute(IFlowContext context)
		{
			IOrganizationService svc = null;

			svc = context.Service;
			CreateRecords(svc);

			var singleTodo = RetrieveRecords(svc);
			UpdateRecords(svc, singleTodo);

			singleTodo = RetrieveRecords(svc);
			DeleteRecords(svc, singleTodo);
			
		}

		Random rnd = new Random();

		private void DeleteRecords(IOrganizationService svc, EntityReference reference)
		{
			svc.Delete(reference.LogicalName, reference.Id);
		}

		private void UpdateRecords(IOrganizationService svc, EntityReference reference)
		{
			//ColumnSet attributes = new ColumnSet(new string[] { "ava_name"});
			//Entity entity = svc.Retrieve(reference.LogicalName, reference.Id, attributes);

			//entity["ava_name"] = $"EDITEDTODO {rnd.Next(0,int.MaxValue)}";

			//svc.Update(entity);

			Entity entity = new Entity(reference.LogicalName);
			entity.Id = reference.Id;
			entity["ava_name"] = $"EDITED TODO {rnd.Next(0, int.MaxValue)}";
			svc.Update(entity);
		}

		private EntityReference RetrieveRecords(IOrganizationService svc)
		{
			QueryExpression query = new QueryExpression("ava_todo");
			query.ColumnSet = new ColumnSet(true);
			query.TopCount = 10;
			query.NoLock = true;
			query.AddOrder("createdon", OrderType.Descending);
			query.Criteria.AddCondition("ava_done", ConditionOperator.Equal, false);

			var entities = svc.RetrieveMultiple(query);
			EntityReference reference = null;

			for (int i = 0; i < entities.Entities.Count; i++)
			{
				System.Console.WriteLine("================================================");
				System.Console.WriteLine($"Entità {i}");
				System.Console.WriteLine("================================================");
				System.Console.WriteLine($"Nome: {entities.Entities[i].GetAttributeValue<string>("ava_name")}");
				System.Console.WriteLine($"Finito:  {entities.Entities[i].GetAttributeValue<bool>("ava_done")}");
				System.Console.WriteLine($"Data inizio: {entities.Entities[i].GetAttributeValue<DateTime>("ava_startdate")}");
				System.Console.WriteLine($"Data fine: {entities.Entities[i].GetAttributeValue<DateTime>("ava_duedate")}");
				System.Console.WriteLine($"Assegnato a: {entities.Entities[i].GetAttributeValue<Entity>("ava_assignedtoid")}");

				reference = entities.Entities[i].ToEntityReference();
			}

			//foreach (var entity in entities.Entities)
			//{
			//	reference = entity.ToEntityReference();
			//	System.Console.WriteLine("================================================");
			//	var name = entity.GetAttributeValue<string>("ava_name");
			//	System.Console.WriteLine($"nome {name}");
			//	System.Console.WriteLine("================================================");
			//}

			return reference;
		}

		private void CreateRecords(IOrganizationService svc)
		{

			System.Console.WriteLine($"Creata entità ava_todo");
			var entity = new Entity("ava_todo");
			entity["ava_name"] = $"TODO {rnd.Next(0, int.MaxValue)}";
			entity["ava_done"] = false;
			entity["ava_startdate"] = DateTime.Now;
			entity["ava_duedate"] = DateTime.Now.AddYears(2);
			entity["ava_assignedtoid"] = null;

			svc.Create(entity);

			System.Console.WriteLine($"Creata entità ava_todo");
		}
	}
}
