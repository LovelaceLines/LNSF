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
    public async Task Post_ValidHostingWithoutConflictDates_Ok()
    {
        // Arrange - Hosting
        var checkIn = new Bogus.DataSets.Date().Past().AddDays(-5);
        var checkOut = new Bogus.DataSets.Date().Future().AddDays(5);
        var hosting = await GetHosting(checkIn: checkIn, checkOut: checkOut);

        // Arrange - HostingWithoutConflictDates
        var hostingWithoutConflictFake = new HostingPostViewModelFake(patientId: hosting.PatientId, checkIn: checkOut.AddDays(1), checkOut: checkOut.AddDays(5)).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_hostingClient);

        // Act - Hosting
        var hostingPosted = await Post<HostingViewModel>(_hostingClient, hostingWithoutConflictFake);

        // Act - Count
        var countAfter = await GetCount(_hostingClient);

        // Act - Query
        var query = await Query<List<HostingViewModel>>(_hostingClient, new HostingFilter(id: hostingPosted.Id));
        var hostingQueried = query.First();

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(hostingWithoutConflictFake, hostingPosted);
        Assert.Equivalent(hostingPosted, hostingQueried);
    }

    [Fact]
    public async Task Post_InvalidHostingWithConflictDates_Conflict()
    {
        // Arrange - Hosting
        var checkIn = new Bogus.DataSets.Date().Past().AddDays(-5);
        var checkOut = new Bogus.DataSets.Date().Future().AddDays(5);
        var hosting = await GetHosting(checkIn: checkIn, checkOut: checkOut);

        // Arrange - HostingWithConflictDates
        var hostingWithConflictBeforeCheckInAndDuringCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-1), checkOut: checkOut).Generate();
        var hostingWithConflictBeforeCheckInAndBeforeCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-1), checkOut: checkOut.AddDays(-1)).Generate();
        var hostingWithConflictBeforeCheckInAndBeforeCheckIn = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-5), checkOut: checkIn.AddDays(-1)).Generate();
        var hostingWithConflictBetweenCheckInAndCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(1), checkOut: checkOut.AddDays(-1)).Generate();
        var hostingWithConflictDuringCheckInAndBeforeCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn, checkOut: checkOut.AddDays(-1)).Generate();
        var hostingWithConflictBeforeCheckOutAndAfterCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkOut.AddDays(-1), checkOut: checkOut.AddDays(1)).Generate();
        var hostingWithConflictDuringCheckOutAndAfterCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkOut, checkOut: checkOut.AddDays(1)).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_hostingClient);

        // Act - Hosting
        var exceptionBeforeCheckInAndDuringCheckOut = await Post<AppException>(_hostingClient, hostingWithConflictBeforeCheckInAndDuringCheckOut);
        var exceptionBeforeCheckInAndBeforeCheckOut = await Post<AppException>(_hostingClient, hostingWithConflictBeforeCheckInAndBeforeCheckOut);
        var exceptionBeforeCheckInAndBeforeCheckIn = await Post<AppException>(_hostingClient, hostingWithConflictBeforeCheckInAndBeforeCheckIn);
        var exceptionBetweenCheckInAndCheckOut = await Post<AppException>(_hostingClient, hostingWithConflictBetweenCheckInAndCheckOut);
        var exceptionDuringCheckInAndBeforeCheckOut = await Post<AppException>(_hostingClient, hostingWithConflictDuringCheckInAndBeforeCheckOut);
        var exceptionBeforeCheckOutAndAfterCheckOut = await Post<AppException>(_hostingClient, hostingWithConflictBeforeCheckOutAndAfterCheckOut);
        var exceptionDuringCheckOutAndAfterCheckOut = await Post<AppException>(_hostingClient, hostingWithConflictDuringCheckOutAndAfterCheckOut);

        // Act - Count
        var countAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.Conflict, exceptionBeforeCheckInAndDuringCheckOut.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, exceptionBeforeCheckInAndBeforeCheckOut.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, exceptionBeforeCheckInAndBeforeCheckIn.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, exceptionBetweenCheckInAndCheckOut.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, exceptionDuringCheckInAndBeforeCheckOut.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, exceptionBeforeCheckOutAndAfterCheckOut.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, exceptionDuringCheckOutAndAfterCheckOut.StatusCode);
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
    public async Task Post_InvalidHostingAfterPostValidHostingWithoutCheckOut_Conflict()
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
    public async Task Put_ValidHostingUpdateDates_Ok()
    {
        // Arrange - Hosting
        var checkIn = new Bogus.DataSets.Date().Past().AddDays(-5);
        var checkOut = new Bogus.DataSets.Date().Future().AddDays(5);
        var hosting = await GetHosting(checkIn: checkIn, checkOut: checkOut);
        
        var hostingToPutWithBeforeCheckInAndDuringCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-1), checkOut: checkOut).Generate();
        var hostingToPutWithBeforeCheckInAndBeforeCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-1), checkOut: checkOut.AddDays(-1)).Generate();
        var hostingToPutWithBeforeCheckInAndBeforeCheckIn = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-5), checkOut: checkIn.AddDays(-1)).Generate();
        var hostingToPutWithBetweenCheckInAndCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(1), checkOut: checkOut.AddDays(-1)).Generate();
        var hostingToPutWithDuringCheckInAndBeforeCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn, checkOut: checkOut.AddDays(-1)).Generate();
        var hostingToPutWithBeforeCheckOutAndAfterCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkOut.AddDays(-1), checkOut: checkOut.AddDays(1)).Generate();
        var hostingToPutWithDuringCheckOutAndAfterCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkOut, checkOut: checkOut.AddDays(1)).Generate();
        var hostingToPutWithAfterCheckOutAndAfterCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkOut.AddDays(1), checkOut: checkOut.AddDays(5)).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_hostingClient);
        
        // Act - Hosting
        var hostingPuttedWithBeforeCheckInAndDuringCheckOut = await Put<HostingViewModel>(_hostingClient, hostingToPutWithBeforeCheckInAndDuringCheckOut);
        var hostingPuttedWithBeforeCheckInAndBeforeCheckOut = await Put<HostingViewModel>(_hostingClient, hostingToPutWithBeforeCheckInAndBeforeCheckOut);
        var hostingPuttedWithBeforeCheckInAndBeforeCheckIn = await Put<HostingViewModel>(_hostingClient, hostingToPutWithBeforeCheckInAndBeforeCheckIn);
        var hostingPuttedWithBetweenCheckInAndCheckOut = await Put<HostingViewModel>(_hostingClient, hostingToPutWithBetweenCheckInAndCheckOut);
        var hostingPuttedWithDuringCheckInAndBeforeCheckOut = await Put<HostingViewModel>(_hostingClient, hostingToPutWithDuringCheckInAndBeforeCheckOut);
        var hostingPuttedWithBeforeCheckOutAndAfterCheckOut = await Put<HostingViewModel>(_hostingClient, hostingToPutWithBeforeCheckOutAndAfterCheckOut);
        var hostingPuttedWithDuringCheckOutAndAfterCheckOut = await Put<HostingViewModel>(_hostingClient, hostingToPutWithDuringCheckOutAndAfterCheckOut);
        var hostingPuttedWithAfterCheckOutAndAfterCheckOut = await Put<HostingViewModel>(_hostingClient, hostingToPutWithAfterCheckOutAndAfterCheckOut);
        
        // Act - Count
        var countAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(hostingToPutWithBeforeCheckInAndDuringCheckOut, hostingPuttedWithBeforeCheckInAndDuringCheckOut);
        Assert.Equivalent(hostingToPutWithBeforeCheckInAndBeforeCheckOut, hostingPuttedWithBeforeCheckInAndBeforeCheckOut);
        Assert.Equivalent(hostingToPutWithBeforeCheckInAndBeforeCheckIn, hostingPuttedWithBeforeCheckInAndBeforeCheckIn);
        Assert.Equivalent(hostingToPutWithBetweenCheckInAndCheckOut, hostingPuttedWithBetweenCheckInAndCheckOut);
        Assert.Equivalent(hostingToPutWithDuringCheckInAndBeforeCheckOut, hostingPuttedWithDuringCheckInAndBeforeCheckOut);
        Assert.Equivalent(hostingToPutWithBeforeCheckOutAndAfterCheckOut, hostingPuttedWithBeforeCheckOutAndAfterCheckOut);
        Assert.Equivalent(hostingToPutWithDuringCheckOutAndAfterCheckOut, hostingPuttedWithDuringCheckOutAndAfterCheckOut);
        Assert.Equivalent(hostingToPutWithAfterCheckOutAndAfterCheckOut, hostingPuttedWithAfterCheckOutAndAfterCheckOut);
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
    public async Task AddEscortToHosting_ValidHostingWithoutConflictDates_Ok()
    {
        // Arrange - Hosting
        var checkIn = new Bogus.DataSets.Date().Past().AddDays(-5);
        var checkOut = new Bogus.DataSets.Date().Future().AddDays(5);
        var hosting = await GetHosting(checkIn: checkIn, checkOut: checkOut);

        // Arrange - Escort
        var escort = await GetEscort();

        // Arrange - HostingEscort
        var hostingEscort = await GetAddEscortToHosting(hostingId: hosting.Id, escortId: escort.Id);
        
        // Arrange - HostingWithoutConflictDates
        var hostingWithoutConflict = await GetHosting(checkIn: checkOut.AddDays(1), checkOut: checkOut.AddDays(5));

        // Arrange - HostingEscortWithoutConflictDates
        var hostingEscortWithoutConflictFake = new HostingEscortViewModelFake(hostingId: hostingWithoutConflict.Id, escortId: escort.Id).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_hostingEscortClient);

        // Act - HostingEscort
        var hostingEscortWithoutConflictPosted = await Post<HostingEscortViewModel>(_addEscortToHostingClient, hostingEscortWithoutConflictFake); 

        // Act - Count
        var countAfter = await GetCount(_hostingEscortClient);

        // Act - Query
        var query = await Query<List<HostingEscortViewModel>>(_hostingEscortClient, new HostingEscortFilter(hostingId: hostingEscortWithoutConflictPosted.HostingId));
        var hostingEscortQueried = query.First();

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(hostingEscortWithoutConflictFake, hostingEscortWithoutConflictPosted);
        Assert.Equivalent(hostingEscortWithoutConflictPosted, hostingEscortQueried);

    }

    [Fact]
    public async Task AddEscortToHosting_InvalidHostingEscortWithConflicDates_Conflict()
    {
        // Arrange - Hosting
        var checkIn = new Bogus.DataSets.Date().Past().AddDays(-5);
        var checkOut = new Bogus.DataSets.Date().Future().AddDays(5);
        var hosting = await GetHosting(checkIn: checkIn, checkOut: checkOut);

        // Arrange - EscortHosting
        var escort = await GetEscort();
        var escortHosting = await GetAddEscortToHosting(hostingId: hosting.Id, escortId: escort.Id);

        // Arrange - Hosting
        var hostingWithConflictBeforeCheckInAndDuringCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-1), checkOut: checkOut).Generate();
        var hostingWithConflictBeforeCheckInAndBeforeCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-1), checkOut: checkOut.AddDays(-1)).Generate();
        var hostingWithConflictBeforeCheckInAndBeforeCheckIn = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-5), checkOut: checkIn.AddDays(-1)).Generate();
        var hostingWithConflictBetweenCheckInAndCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(1), checkOut: checkOut.AddDays(-1)).Generate();
        var hostingWithConflictDuringCheckInAndBeforeCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn, checkOut: checkOut.AddDays(-1)).Generate();
        var hostingWithConflictBeforeCheckOutAndAfterCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkOut.AddDays(-1), checkOut: checkOut.AddDays(1)).Generate();
        var hostingWithConflictDuringCheckOutAndAfterCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkOut, checkOut: checkOut.AddDays(1)).Generate();

        // Arrange - HostingEscort
        var hostingEscortWithConflictBeforeCheckInAndDuringCheckOut = new HostingEscortViewModelFake(hostingId: hostingWithConflictBeforeCheckInAndDuringCheckOut.Id, escortId: escort.Id).Generate();
        var hostingEscortWithConflictBeforeCheckInAndBeforeCheckOut = new HostingEscortViewModelFake(hostingId: hostingWithConflictBeforeCheckInAndBeforeCheckOut.Id, escortId: escort.Id).Generate();
        var hostingEscortWithConflictBeforeCheckInAndBeforeCheckIn = new HostingEscortViewModelFake(hostingId: hostingWithConflictBeforeCheckInAndBeforeCheckIn.Id, escortId: escort.Id).Generate();
        var hostingEscortWithConflictBetweenCheckInAndCheckOut = new HostingEscortViewModelFake(hostingId: hostingWithConflictBetweenCheckInAndCheckOut.Id, escortId: escort.Id).Generate();
        var hostingEscortWithConflictDuringCheckInAndBeforeCheckOut = new HostingEscortViewModelFake(hostingId: hostingWithConflictDuringCheckInAndBeforeCheckOut.Id, escortId: escort.Id).Generate();
        var hostingEscortWithConflictBeforeCheckOutAndAfterCheckOut = new HostingEscortViewModelFake(hostingId: hostingWithConflictBeforeCheckOutAndAfterCheckOut.Id, escortId: escort.Id).Generate();
        var hostingEscortWithConflictDuringCheckOutAndAfterCheckOut = new HostingEscortViewModelFake(hostingId: hostingWithConflictDuringCheckOutAndAfterCheckOut.Id, escortId: escort.Id).Generate();
        
        // Arrange - Count
        var countBefore = await GetCount(_hostingEscortClient);

        // Act - HostingEscort
        var exceptionBeforeCheckInAndDuringCheckOut = await Post<AppException>(_addEscortToHostingClient, hostingEscortWithConflictBeforeCheckInAndDuringCheckOut);
        var exceptionBeforeCheckInAndBeforeCheckOut = await Post<AppException>(_addEscortToHostingClient, hostingEscortWithConflictBeforeCheckInAndBeforeCheckOut);
        var exceptionBeforeCheckInAndBeforeCheckIn = await Post<AppException>(_addEscortToHostingClient, hostingEscortWithConflictBeforeCheckInAndBeforeCheckIn);
        var exceptionBetweenCheckInAndCheckOut = await Post<AppException>(_addEscortToHostingClient, hostingEscortWithConflictBetweenCheckInAndCheckOut);
        var exceptionDuringCheckInAndBeforeCheckOut = await Post<AppException>(_addEscortToHostingClient, hostingEscortWithConflictDuringCheckInAndBeforeCheckOut);
        var exceptionBeforeCheckOutAndAfterCheckOut = await Post<AppException>(_addEscortToHostingClient, hostingEscortWithConflictBeforeCheckOutAndAfterCheckOut);
        var exceptionDuringCheckOutAndAfterCheckOut = await Post<AppException>(_addEscortToHostingClient, hostingEscortWithConflictDuringCheckOutAndAfterCheckOut);

        // Act - Count
        var countAfter = await GetCount(_hostingEscortClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.Conflict, exceptionBeforeCheckInAndDuringCheckOut.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, exceptionBeforeCheckInAndBeforeCheckOut.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, exceptionBeforeCheckInAndBeforeCheckIn.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, exceptionBetweenCheckInAndCheckOut.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, exceptionDuringCheckInAndBeforeCheckOut.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, exceptionBeforeCheckOutAndAfterCheckOut.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, exceptionDuringCheckOutAndAfterCheckOut.StatusCode);

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
}
