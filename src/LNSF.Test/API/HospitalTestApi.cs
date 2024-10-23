using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Test.DTOs;
using LNSF.Test.Fakers;
using LNSF.Test.Global;
using System.Net;

namespace LNSF.Test.API;

public class HospitalTestApi : GlobalClientRequest
{
	[Fact]
	public async Task QueryHospital_Ok()
	{
		var hospital = await GetHospital();

		var result = await GetFromQuery<QueryResult<Hospital>>(_hospitalClient, new HospitalFilter { Id = hospital.Id });
		var hospitalQueried = result.Items.Single();

		Assert.Equal(hospital.Id, hospitalQueried.Id);
		Assert.Equal(hospital.Name, hospitalQueried.Name);
		Assert.Equal(hospital.Acronym, hospitalQueried.Acronym);
	}

	[Fact]
	public async Task Post_HospitalValid_Ok()
	{
		var hospitalFake = new HospitalFake().Generate();

		var hospitalPosted = await PostFromBody<Hospital>(_hospitalClient, hospitalFake);

		Assert.Equal(hospitalFake.Acronym, hospitalPosted.Acronym);
		Assert.Equal(hospitalFake.Name, hospitalPosted.Name);
	}

	[Fact]
	public async Task Post_HospitalWithRepeatedUniqueName_Conflict()
	{
		var hospital = await GetHospital();
		var hospitalFake = new HospitalFake(name: hospital.Name).Generate();

		var exception = await PostFromBody<AppHttpResponse>(_hospitalClient, hospitalFake);

		Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
	}

	[Fact]
	public async Task Put_Hospital_Ok()
	{
		var hospital = await GetHospital();
		var hospitalFake = new HospitalFake(id: hospital.Id).Generate();

		var hospitalPuted = await PutFromBody<Hospital>(_hospitalClient, hospitalFake);

		Assert.Equal(hospitalPuted.Id, hospitalFake.Id);
		Assert.Equal(hospitalPuted.Name, hospitalFake.Name);
		Assert.Equal(hospitalPuted.Acronym, hospitalFake.Acronym);
	}

	[Fact]
	public async Task Put_HospitalInvalidRepeatedUniqueName_Conflict()
	{
		var hospital1 = await GetHospital();
		var hospital2 = await GetHospital();
		var hospitalFake = new HospitalFake(id: hospital1.Id, name: hospital2.Name).Generate();

		var exception = await PutFromBody<AppHttpResponse>(_hospitalClient, hospitalFake);

		Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
	}
}
