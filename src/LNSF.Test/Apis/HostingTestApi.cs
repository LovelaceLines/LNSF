using System.Collections;
using System.Net;
using LNSF.Api.ViewModels;
using LNSF.Domain.DTOs;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using Xunit;

namespace LNSF.Test.Apis;

public class HostingTestData : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] { 0, true, true },
        new object[] { 0, true, false },
        new object[] { 0, false, true },
        new object[] { 0, false, false },
        new object[] { 1, true, true },
        new object[] { 1, true, false },
        new object[] { 1, false, true },
        new object[] { 1, false, false },
        new object[] { 2, true, true },
        new object[] { 2, true, false },
        new object[] { 2, false, true },
        new object[] { 2, false, false },
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class HostingTestDataWithCheckOut : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] { 0, true, true },
        new object[] { 0, false, true },
        new object[] { 1, true, true },
        new object[] { 1, true, false },
        new object[] { 1, false, true },
        new object[] { 2, true, true },
        new object[] { 2, true, false },
        new object[] { 2, false, true },
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}


public class HostingTestApi : GlobalClientRequest
{
    [Theory]
    [ClassData(typeof(HostingTestData))]
    public async Task Post_HostingValidWithEscortsAndChecks_Ok(int numberEscorts, bool escortHasCheckOut, bool patientHasCheckOut)
    {
        // Arrange - Patient
        var patient = await GetPatient();

        // Arrange - Escorts
        var escortInfos = new List<HostingEscortInfo>();
        for (int i = 0; i < numberEscorts; i++)
        {
            var escort = await GetEscort();
            var escortInfoFake = new HostingEscortInfoFake().Generate();
            escortInfoFake.Id = escort.Id;
            escortInfoFake.CheckOut = escortHasCheckOut ? escortInfoFake.CheckOut : null; 

            escortInfos.Add(escortInfoFake);
        }

        // Arrange - Hosting
        var hostingFake = new HostingPostViewModelFake().Generate();
        hostingFake.PatientId = patient.Id;
        hostingFake.EscortInfos = escortInfos;
        hostingFake.CheckOut = patientHasCheckOut ? hostingFake.CheckOut : null;

        // Arrange - Count
        var countBefore = await GetCount(_hostingClient);

        // Act - Hosting
        var hostingPosted = await Post<HostingViewModel>(_hostingClient, hostingFake);

        // Act - Count
        var countAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(hostingFake, hostingPosted);
        var hostingGeted = (await GetById<List<HostingViewModel>>(_hostingClient, hostingPosted.Id)).First();
        Assert.Equivalent(hostingPosted, hostingGeted);
    }

    [Theory]
    [ClassData(typeof(HostingTestDataWithCheckOut))]
    public async Task Post_HostingInvalidWithCheckInGreaterThanCheckOut_BadRequest(int numberEscorts, bool escortHasCheckOut, bool patientHasCheckOut)
    {
        // Arrange - Patient
        var patient = await GetPatient();

        // Arrange - Escorts
        var escortInfos = new List<HostingEscortInfo>();
        for (int i = 0; i < numberEscorts; i++)
        {
            var escort = await GetEscort();
            var escortInfoFake = new HostingEscortInfoFake().Generate();
            escortInfoFake.Id = escort.Id;
            escortInfoFake.CheckIn = escortHasCheckOut ? escortInfoFake.CheckOut!.Value.AddDays(1) : escortInfoFake.CheckIn; // CheckIn > CheckOut

            escortInfos.Add(escortInfoFake);
        }

        // Arrange - Hosting
        var hostingFake = new HostingPostViewModelFake().Generate();
        hostingFake.PatientId = patient.Id;
        hostingFake.EscortInfos = escortInfos;
        hostingFake.CheckIn = patientHasCheckOut ? hostingFake.CheckOut!.Value.AddDays(1) : hostingFake.CheckIn; // CheckIn > CheckOut

        // Arrange - Count
        var countBefore = await GetCount(_hostingClient);

        // Act - Hosting
        var exception = await Post<AppException>(_hostingClient, hostingFake);

        // Act - Count
        var countAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
    }

    [Theory]
    [ClassData(typeof(HostingTestData))]
    public async Task Put_HostingValidWithCheckOut_Ok(int numberEscorts, bool escortHasCheckOut, bool patientHasCheckOut)
    {
        // Arrange - Hosting
        var hosting = await GetHosting(numberEscorts, patientHasCheckOut, escortHasCheckOut);

        // Arrange - Count
        var countBefore = await GetCount(_hostingClient);
        
        // Act - Hosting
        hosting.CheckOut = DateTime.Now;
        hosting.EscortInfos?.ForEach(e => e.CheckOut = DateTime.Now);
        var hostingPutted = await Put<HostingViewModel>(_hostingClient, hosting);

        // Act - Count
        var countAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        var hostingGeted = (await GetById<List<HostingViewModel>>(_hostingClient, hostingPutted.Id)).First();
        Assert.Equivalent(hostingPutted, hostingGeted);
    }

    [Theory]
    [ClassData(typeof(HostingTestData))]
    public async Task Put_HostingInvalidWithNewPatient_BadRequest(int numberEscorts, bool escortHasCheckOut, bool patientHasCheckOut)
    {
        // Arrange - Hosting
        var hosting = await GetHosting(numberEscorts, patientHasCheckOut, escortHasCheckOut);

        // Arrange - Count
        var countBefore = await GetCount(_hostingClient);
        
        // Act - Hosting
        var hostingFake = new HostingPostViewModelFake().Generate();
        hostingFake.PatientId = (await GetPatient()).Id; // New Patient
        var exception = await Put<AppException>(_hostingClient, hostingFake);

        // Act - Count
        var countAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
    }

    [Theory]
    [ClassData(typeof(HostingTestDataWithCheckOut))]
    public async Task Put_HostingInvalidWithCheckInGreaterThanCheckOut_BadRequest(int numberEscorts, bool escortHasCheckOut, bool patientHasCheckOut)
    {
        // Arrange - Hosting
        var hosting = await GetHosting(numberEscorts, patientHasCheckOut, escortHasCheckOut);

        // Arrange - Count
        var countBefore = await GetCount(_hostingClient);
        
        // Act - Hosting
        hosting.CheckIn = patientHasCheckOut ? hosting.CheckOut!.Value.AddDays(1) : hosting.CheckIn; // CheckIn > CheckOut
        hosting.EscortInfos?.ForEach(e => 
            e.CheckIn = escortHasCheckOut ? e.CheckOut!.Value.AddDays(1) : e.CheckIn); // CheckIn > CheckOut
        var exception = await Put<AppException>(_hostingClient, hosting);

        // Act - Count
        var countAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
    }
}
