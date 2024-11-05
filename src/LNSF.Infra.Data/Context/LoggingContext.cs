using LNSF.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace LNSF.Infra.Data.Context;

public static class LoggingContext
{
	private static readonly List<EntityState> entityStates = [EntityState.Added, EntityState.Modified, EntityState.Deleted];
	private static readonly IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();

	public static async Task LogChanges(this AppDbContext context)
	{
		var logTime = DateTime.Now;
		const string idColumn = "Id";

		var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
		userId ??= "0";

		var changes = context.ChangeTracker.Entries()
			.Where(e => entityStates.Contains(e.State) && e.Entity.GetType().GetProperties().Any(p => p.Name == idColumn))
			.ToList();

		foreach (var change in changes)
		{
			var originalValues = "{}";
			var updatedValues = JsonConvert.SerializeObject(change.CurrentValues.Properties.ToDictionary(p => p.Name, p => change.CurrentValues[p]));

			if (change.State == EntityState.Modified)
			{
				var dbValues = await change.GetDatabaseValuesAsync();
				dbValues ??= change.OriginalValues;

				originalValues = JsonConvert.SerializeObject(dbValues.Properties.ToDictionary(p => p.Name, p => dbValues[p]));
			}

			var changesValues = new JObject
			{
				["OriginalValues"] = JObject.Parse(originalValues),
			};

			if (change.State == EntityState.Added)
				changesValues["AddedValues"] = JObject.Parse(updatedValues);

			if (change.State == EntityState.Modified)
				// Apenas valores diferentes de originalValues
				changesValues["UpdatedValues"] = JObject.FromObject(JObject.Parse(updatedValues).Properties().Where(p =>
					!JToken.DeepEquals(p.Value, JObject.Parse(updatedValues)[p.Name])).ToDictionary(p => p.Name, p => p.Value));

			if (change.State == EntityState.Deleted)
			{
				changesValues["OriginalValues"] = JObject.Parse(updatedValues);
				changesValues["DeletedValues"] = JObject.Parse(originalValues);
			}

			var idCurrentValues = int.TryParse(change.CurrentValues[idColumn]?.ToString(), out int id) ? id : 0;
			var entityId = idCurrentValues < 0 ? 0 : idCurrentValues;

			var logEntry = new LogEntry
			{
				UserId = int.Parse(userId),
				EntityName = change.Entity.GetType().Name,
				EntityId = entityId,
				Action = change.State.ToString(),
				ValuesChanges = changesValues.ToString(),
				LogDateTime = logTime
			};

			context.LogEntries.Add(logEntry);
		}
	}
}
