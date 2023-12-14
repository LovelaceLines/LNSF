using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class HostingTestApi : GlobalClientRequest
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task Post_ValidHosting_Ok(bool hasCheckOut)
    {
        // Arrange - Patient
        var patient = await GetPatient();

        // Arrange - Hosting
        var hostingFake = new HostingPostViewModelFake(patientId: patient.Id).Generate();
        if (hasCheckOut) hostingFake.CheckOut = new Bogus.DataSets.Date().Future();

        // Arrange - Count
        var countBefore = await GetCount(_hostingClient);

        // Act - Hosting
        var hostingPosted = await Post<HostingViewModel>(_hostingClient, hostingFake);

        // Act - Count
        var countAfter = await GetCount(_hostingClient);

        // Act - Query
        var query = await Query<List<HostingViewModel>>(_hostingClient, new HostingFilter(id: hostingPosted.Id));
        var hostingQueried = query.First();

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(hostingFake, hostingPosted);
        Assert.Equivalent(hostingPosted, hostingQueried);
    }

    [Fact]
    public async Task Post_InvalidHostingWithCheckInGreaterThanCheckOut_BadRequest()
    {
        // Arrange - Patient
        var patient = await GetPatient();

        // Arrange - Hosting
        var hostingFake = new HostingPostViewModelFake(patientId: patient.Id, checkIn: DateTime.Now, checkOut: DateTime.Now.AddDays(-1)).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_hostingClient);

        // Act - Hosting
        var exception = await Post<AppException>(_hostingClient, hostingFake);

        // Act - Count
        var countAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
    }

    [Fact]
    public async Task Put_ValidHosting_Ok()
    {
        // Arrange - Hosting
        var hosting = await GetHosting();
        var hostingToPut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_hostingClient);
        
        // Act - Hosting
        var hostingPutted = await Put<HostingViewModel>(_hostingClient, hostingToPut);

        // Act - Count
        var countAfter = await GetCount(_hostingClient);

        // Act - Query
        var query = await Query<List<HostingViewModel>>(_hostingClient, new HostingFilter(id: hostingPutted.Id));
        var hostingQueried = query.First();

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(hostingToPut, hostingPutted);
        Assert.Equivalent(hostingPutted, hostingQueried);
    }

    [Fact]
    public async Task Put_InvalidHostingWithOtherPatient_NotFound()
    {
        // Arrange - Patient
        var patient = await GetPatient();

        // Arrange - Hosting
        var hosting = await GetHosting();
        var hostingToPut = new HostingViewModelFake(id: hosting.Id, patientId: patient.Id).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_hostingClient);
        
        // Act - Hosting
        var exception = await Put<AppException>(_hostingClient, hostingToPut);

        // Act - Count
        var countAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task Put_InvalidHostingWithCheckInGreaterThanCheckOut_BadRequest()
    {
        // Arrange - Hosting
        var hosting = await GetHosting();
        var hostingToPut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: DateTime.Now, checkOut: DateTime.Now.AddDays(-1)).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_hostingClient);
        
        // Act - Hosting
        var exception = await Put<AppException>(_hostingClient, hostingToPut);

        // Act - Count
        var countAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task AddEscortToHosting_ValidHostingEscort_OK(int count)
    {
        // Arrange - Hosting
        var hosting = await GetHosting();

        // Arrange - Escort
        var escorts = new List<EscortViewModel>();
        for (int i = 0; i < count; i++)
        {
            var escort = await GetEscort();
            escorts.Add(escort);
        }

        // Arrange - HostingEscort
        var hostingsEscortsFake = new List<HostingEscortViewModel>();
        foreach (var escort in escorts)
        {
            var hostingEscortFake = new HostingEscortViewModelFake(hostingId: hosting.Id, escortId: escort.Id).Generate();
            hostingsEscortsFake.Add(hostingEscortFake);
        }

        // Arrange - Count
        var countBefore = await GetCount(_hostingEscortClient);

        // Act - HostingEscort
        var hostingsEscortsPosted = new List<HostingEscortViewModel>();
        foreach (var hostingEscortFake in hostingsEscortsFake)
        {
            var hostingEscortPosted = await Post<HostingEscortViewModel>(_addEscortToHostingClient, hostingEscortFake);
            hostingsEscortsPosted.Add(hostingEscortPosted);
        }

        // Act - Count
        var countAfter = await GetCount(_hostingEscortClient);

        // Act - Query
        var hostingsEscortsQueried = await Query<List<HostingEscortViewModel>>(_hostingEscortClient, new HostingEscortFilter(hostingId: hosting.Id));

        // Assert
        Assert.Equal(countBefore + count, countAfter);
        Assert.Equivalent(hostingsEscortsFake, hostingsEscortsPosted);
        Assert.Equivalent(hostingsEscortsPosted, hostingsEscortsQueried);
    }

    [Fact]
    public async Task AddEscortToHosting_InvalidHostingEscortWithNotExistsHosting_NotFound()
    {
        // Arrange - Escort
        var escort = await GetEscort();

        // Arrange - HostingEscort
        var hostingEscortFake = new HostingEscortViewModelFake(hostingId: 0, escortId: escort.Id).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_hostingEscortClient);

        // Act - HostingEscort
        var exception = await Post<AppException>(_addEscortToHostingClient, hostingEscortFake);

        // Act - Count
        var countAfter = await GetCount(_hostingEscortClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task AddEscortToHosting_InvalidHostingEscortWithNotExistsEscort_NotFound()
    {
        // Arrange - Hosting
        var hosting = await GetHosting();

        // Arrange - HostingEscort
        var hostingEscortFake = new HostingEscortViewModelFake(hostingId: hosting.Id, escortId: 0).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_hostingEscortClient);

        // Act - HostingEscort
        var exception = await Post<AppException>(_addEscortToHostingClient, hostingEscortFake);

        // Act - Count
        var countAfter = await GetCount(_hostingEscortClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task RemoveEscortFromHosting_ValidHostingEscort_Ok()
    {
        // Arrange - HostingEscort
        var hostingEscort = await GetAddEscortToHosting();

        // Arrange - Count
        var countBefore = await GetCount(_hostingEscortClient);

        // Act - HostingEscort
        var hostingEscortDeleted = await DeleteByBody<HostingEscortViewModel>(_removeEscortFromHostingClient, hostingEscort);

        // Act - Count
        var countAfter = await GetCount(_hostingEscortClient);

        // Assert
        Assert.Equal(countBefore - 1, countAfter);
        Assert.Equivalent(hostingEscort, hostingEscortDeleted);
    }

    [Fact]
    public async Task RemoveEscortFromHosting_InvalidHostingEscortWithNotExistsHosting_NotFound()
    {
        // Arrange - Escort
        var escort = await GetEscort();

        // Arrange - HostingEscort
        var hostingEscortFake = new HostingEscortViewModelFake(hostingId: 0, escortId: escort.Id).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_hostingEscortClient);

        // Act - HostingEscort
        var exception = await DeleteByBody<AppException>(_removeEscortFromHostingClient, hostingEscortFake);

        // Act - Count
        var countAfter = await GetCount(_hostingEscortClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task RemoveEscortFromHosting_InvalidHostingEscortWithNotExistsEscort_NotFound()
    {
        // Arrange - Hosting
        var hosting = await GetHosting();

        // Arrange - HostingEscort
        var hostingEscortFake = new HostingEscortViewModelFake(hostingId: hosting.Id, escortId: 0).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_hostingEscortClient);

        // Act - HostingEscort
        var exception = await DeleteByBody<AppException>(_removeEscortFromHostingClient, hostingEscortFake);

        // Act - Count
        var countAfter = await GetCount(_hostingEscortClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task Post_InvalidHostingWithConflicDates_Conflict()
    {
        // Arrange - Hosting
        var checkOut = new Bogus.DataSets.Date().Future();
        var hosting = await GetHosting(checkOut: checkOut);
        var hostingToPostBefore = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkOut.AddDays(-5), checkOut: checkOut.AddDays(-1)).Generate();
        var hostingToPostBeforeDuring = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: hosting.CheckIn.AddDays(-1), checkOut: checkOut).Generate();
        var hostingToPostDuring = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: hosting.CheckIn.AddDays(1), checkOut: checkOut.AddDays(-1)).Generate();
        var hostingToPostAfterDuring = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: hosting.CheckIn, checkOut: checkOut.AddDays(1)).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_hostingClient);

        // Act - Hosting
        var exceptionBefore = await Post<AppException>(_hostingClient, hostingToPostBefore);
        var exceptionBeforeDuring = await Post<AppException>(_hostingClient, hostingToPostBeforeDuring);
        var exceptionDuring = await Post<AppException>(_hostingClient, hostingToPostDuring);
        var exceptionAfterDuring = await Post<AppException>(_hostingClient, hostingToPostAfterDuring);

        // Act - Count
        var countAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.Conflict, exceptionBefore.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, exceptionBeforeDuring.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, exceptionDuring.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, exceptionAfterDuring.StatusCode);
    }

    [Fact]
    public async Task Post_ValidHostingAfterPostValidHostingWithCheckOut_Ok()
    {
        // Arrange - Hosting
        var checkOut = new Bogus.DataSets.Date().Future();
        var hosting = await GetHosting(checkOut: checkOut);
        var hostingToPost = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkOut.AddDays(1), checkOut: checkOut.AddDays(5)).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_hostingClient);

        // Act - Hosting
        var hostingPosted = await Post<HostingViewModel>(_hostingClient, hostingToPost);

        // Act - Count
        var countAfter = await GetCount(_hostingClient);

        // Act - Query
        var query = await Query<List<HostingViewModel>>(_hostingClient, new HostingFilter(id: hostingPosted.Id));
        var hostingQueried = query.First();

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(hostingPosted, hostingQueried);
    }

    [Fact]
    public async Task Post_InvalidHostingAfterPostValidHostingWithoutCheckOut_Ok()
    {
        // Arrange - Patient
        var patient = await GetPatient();

        // Arrange - Hosting
        var hosting = new HostingPostViewModelFake(patientId: patient.Id).Generate();
        hosting.CheckOut = null;
        var hostingPosted = await Post<HostingViewModel>(_hostingClient, hosting);

        var hostingToPost = new HostingViewModelFake(id: hostingPosted.Id, patientId: hostingPosted.PatientId, checkIn: hostingPosted.CheckIn.AddDays(1), checkOut: hostingPosted.CheckIn.AddDays(5)).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_hostingClient);

        // Act - Hosting
        var exception = await Post<AppException>(_hostingClient, hostingToPost);

        // Act - Count
        var countAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
    }
}
