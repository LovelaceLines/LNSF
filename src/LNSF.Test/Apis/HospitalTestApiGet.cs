using LNSF.Api.ViewModels;
using LNSF.Domain.Filters;
using Xunit;

namespace LNSF.Test.Apis;

public class HospitalTestApiGet : GlobalClientRequest
{
    [Fact]
    public async Task QueryHospital_Ok()
    {
        var hospital = await GetHospital();

        var hospitalQueried = await QueryFirst<HospitalViewModel>(_hospitalClient, new HospitalFilter(id: hospital.Id));

        Assert.Equivalent(hospital.Id, hospitalQueried.Id);
        Assert.Equivalent(hospital.Name, hospitalQueried.Name);
        Assert.Equivalent(hospital.Acronym, hospitalQueried.Acronym);
    }

    [Fact]
    public async Task QueryHospitalName_Ok()
    {
        var hospital = await GetHospital();

        var hospitalNameQueried = await QueryFirst<HospitalViewModel>(_hospitalClient, new HospitalFilter(id: hospital.Id, name: hospital.Name));

        Assert.Equivalent(hospital.Name, hospitalNameQueried.Name);
    }

    [Fact]
    public async Task QueryHospitalAcronym_Ok()
    {
        var hospital = await GetHospital();

        var hospitalAcronymQueried = await QueryFirst<HospitalViewModel>(_hospitalClient, new HospitalFilter(id: hospital.Id, acronym: hospital.Acronym));

        Assert.Equivalent(hospital.Acronym, hospitalAcronymQueried.Acronym);
    }

    [Fact]
    public async Task QueryHospitalGlobalFilter_Ok()
    {
        var hospital = await GetHospital();
        var hospitalQueried = await QueryFirst<HospitalViewModel>(_hospitalClient, new HospitalFilter(id: hospital.Id));

        var hospitalNameQueried = await QueryFirst<HospitalViewModel>(_hospitalClient, new HospitalFilter(id: hospital.Id, globalFilter: hospital.Name));
        var hospitalAcronymQueried = await QueryFirst<HospitalViewModel>(_hospitalClient, new HospitalFilter(id: hospital.Id, globalFilter: hospital.Acronym));

        Assert.Equivalent(hospitalQueried, hospitalNameQueried);
        Assert.Equivalent(hospitalQueried, hospitalAcronymQueried);
    }
}
