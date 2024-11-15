using Bogus;
using LNSF.Domain.Entities;

namespace LNSF.Test.Fakers;

public class NotificationFake : Faker<Notification>
{
	public NotificationFake(int? id = null, string? title = null, string? content = null, DateTime? validFrom = null, DateTime? expiredAt = null)
	{
		RuleFor(n => n.Id, f => id ?? 0);
		RuleFor(n => n.Title, f => title ?? f.Lorem.Sentence());
		RuleFor(n => n.Content, f => content ?? f.Lorem.Paragraph());
		RuleFor(n => n.ValidFrom, f => validFrom ?? f.Date.Past());
		RuleFor(n => n.ExpiredAt, f => expiredAt ?? f.Date.Future());
	}
}
