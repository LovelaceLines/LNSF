using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using LNSF.Test.Global;
using System.Net;

namespace LNSF.Test.API;

public class TreatmentTestApiPost : GlobalClientRequest
{
	[Fact]
	public async Task Post_Treatment_Ok()
	{
		var treatmentFake = new TreatmentFake().Generate();

		var treatmentPosted = await PostFromBody<Treatment>(_treatmentClient, treatmentFake);

		Assert.Equal(treatmentFake.Name, treatmentPosted.Name);
		Assert.Equal(treatmentFake.Type, treatmentPosted.Type);
	}

	[Fact]
	public async Task Post_TreatmentWithRepeatedUniqueNameAndDifferentType_Ok()
	{
		var treatment = await GetTreatment();
		var treatmentFake = new TreatmentFake(name: treatment.Name).Generate();
		if (treatment.Type == treatmentFake.Type)
			treatmentFake.Type = treatment.Type == TypeTreatment.CANCER ? TypeTreatment.OTHER : TypeTreatment.CANCER;

		var treatmentPosted = await PostFromBody<Treatment>(_treatmentClient, treatmentFake);

		Assert.Equal(treatmentFake.Name, treatmentPosted.Name);
		Assert.Equal(treatmentFake.Type, treatmentPosted.Type);
	}

	[Fact]
	public async Task Post_TreatmentWithRepeatedUniqueNameAndType_Conflict()
	{
		var treatment = await GetTreatment();
		var treatmentFake = new TreatmentFake(name: treatment.Name, type: treatment.Type).Generate();

		var exception = await PostFromBody<AppException>(_treatmentClient, treatmentFake);

		Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
	}

	[Theory]
	[InlineData("ab")]
	[InlineData("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz")]
	public async Task Post_TreatmentWithShortAndLongName_BadRequest(string name)
	{
		var treatmentFake = new TreatmentFake(name: name).Generate();

		var exception = await PostFromBody<AppException>(_treatmentClient, treatmentFake);

		Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
	}
}
