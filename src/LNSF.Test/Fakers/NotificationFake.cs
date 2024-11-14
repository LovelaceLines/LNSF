using Bogus;
using LNSF.Domain.Entities;

namespace LNSF.Test.Fakers;

public class NotificationFake : Faker<Notification>
{
	public NotificationFake(int? id = null, string? title = null, string? content = null)
	{
		RuleFor(n => n.Id, f => id ?? 0);
		RuleFor(n => n.Title, f => title ?? f.Lorem.Sentence());
		RuleFor(n => n.Content, f => content ?? f.Lorem.Paragraph());
	}
}
