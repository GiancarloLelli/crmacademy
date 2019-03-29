using Avanade.Xrm.Batches;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;


namespace Avanade.TeamBill.ConsoleApp.Workers
{
	class CRUDBatches : IFlow
	{
		public void Execute(IFlowContext context)
		{
			var crmSvc = context.Service;
			CreateRecords(crmSvc);
		   
			var singleTodo = RetrieveRecords(crmSvc);
			UpdateRecords(crmSvc, singleTodo);

			singleTodo = RetrieveRecords(crmSvc);
			DeleteRecords(crmSvc, singleTodo);

			
		}
		private  void DeleteRecords(IOrganizationService crmSvc, EntityReference todo) // Operazione di Delete 
		{
			crmSvc.Delete(todo.LogicalName, todo.Id); // Prende il nome e l'ID della vista da cancellare e la cancella.

		}

		private  void UpdateRecords(IOrganizationService crmSvc, EntityReference todo) // Operazione di Update 
		{

			var entityDaAggiornare = new Entity(todo.LogicalName); // Nome della vista da dove prende i campi da aggiornare
			entityDaAggiornare.Id = todo.Id; // ID della vista 
			entityDaAggiornare["ava_name"] = "ciao!";
			entityDaAggiornare["ava_duedate"] = DateTime.Now;
			entityDaAggiornare["ava_done"] = true;

			crmSvc.Update(entityDaAggiornare);


		}

		private  EntityReference RetrieveRecords(IOrganizationService crmSvc) // Operazione di Lettura
		{
			var query = new QueryExpression("ava_todo");
			query.ColumnSet = new ColumnSet(true);
			query.TopCount = 10;  // Numero massimo record da prendere 
			query.NoLock = true;  // Non blocca la tabella del DB quando esegue la query 

			query.AddOrder("createdon", OrderType.Descending);  //Ordinamento dei valori 
			query.Criteria.AddCondition("ava_done", ConditionOperator.Equal, false);  // Criterio di come filtrare i dati 

			var entities = crmSvc.RetrieveMultiple(query); // E' il contenitore del risultato della query 
			EntityReference reference = null; // E' un dato che memorizza il riferimento al singolo record 

			foreach (var todo in entities.Entities)
			{
				reference = todo.ToEntityReference();
				var name = todo.GetAttributeValue<string>("ava_name"); // E' molto importante perchè se non trova , dentro in questo caso "ava_name", restituisce "NULL"
				Console.WriteLine($"Name: {name}");

			}

			return reference;

		}

		private void CreateRecords(IOrganizationService crmSvc)  // Operazione di Create
		{
			var entity = new Entity("ava_todo"); //Definizione dell'entità e quali campi creare dentro essa e con cosa.

			entity["ava_name"] = "Ciao";
			entity["ava_duedate"] = DateTime.Now.AddDays(2);
			entity["ava_startdate"] = DateTime.Now;
			entity["ava_done"] = false;

			crmSvc.Create(entity);
		}
	}
}
