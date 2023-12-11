using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class TreatmentTestApi : GlobalClientRequest
{
    [Fact]
    public async Task Post_ValidTreatment_Ok()
    {
        // Arrange - Treatment
        var treatmentFake = new TreatmentPostViewModelFake().Generate();

        // Arrange - Count
        var countBefore = await GetCount(_treatmentClient);

        // Act - Treatment
        var treatmentPosted = await Post<TreatmentViewModel>(_treatmentClient, treatmentFake);

        // Act - Count
        var countAfter = await GetCount(_treatmentClient);

        // Act - Query
        var query = await Query<List<TreatmentViewModel>>(_treatmentClient, new TreatmentFilter(id: treatmentPosted.Id));
        var treatmentQueried = query.FirstOrDefault();

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(treatmentFake, treatmentPosted);
        Assert.Equivalent(treatmentPosted, treatmentQueried);
    }

    [Fact]
    public async Task Post_ValidTreatmentWithNonUniqueNameAndDifferentType_Ok()
    {
        // Arrange - Treatment
        var treatment = await GetTreatment();
        var treatmentToPost = new TreatmentPostViewModelFake(name: treatment.Name).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_treatmentClient);

        // Act - Treatment
        var treatmentPosted = await Post<TreatmentViewModel>(_treatmentClient, treatmentToPost);

        // Act - Count
        var countAfter = await GetCount(_treatmentClient);

        // ACt - Query
        var query = await Query<List<TreatmentViewModel>>(_treatmentClient, new TreatmentFilter(id: treatmentPosted.Id));
        var treatmentQueried = query.FirstOrDefault();

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(treatmentToPost, treatmentPosted);
        Assert.Equivalent(treatmentPosted, treatmentQueried);
    }

    [Fact]
    public async Task Post_InvalidTreatmentWithNonUniqueNameAndType_Conflict()
    {
        // Arrange - Treatment
        var treatment = await GetTreatment();
        var treatmentToPost = new TreatmentPostViewModelFake(name: treatment.Name, type: treatment.Type).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_treatmentClient);

        // Act - Treatment
        var exception = await Post<AppException>(_treatmentClient, treatmentToPost);

        // Act - Count
        var countAfter = await GetCount(_treatmentClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEqual(HttpStatusCode.Conflict, exception.StatusCode);
    }

    [Theory]
    [InlineData("a")]
    [InlineData("ab")]
    [InlineData("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz")]
    public async Task Post_InvalidTreatmentWithShortName_BadRequest(string name)
    {
        // Arrange - Treatment
        var treatmentFake = new TreatmentPostViewModelFake(name: name).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_treatmentClient);

        // Act - Treatment
        var exception = await Post<AppException>(_treatmentClient, treatmentFake);

        // Act - Count
        var countAfter = await GetCount(_treatmentClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
    }

    [Fact]
    public async Task Put_ValidTreatment_Ok()
    {
        // Arrange - Treatment
        var treatment = await GetTreatment();
        var treatmentToPut = new TreatmentViewModelFake(treatment.Id).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_treatmentClient);

        // Act - Treatment
        var treatmentPuted = await Put<TreatmentViewModel>(_treatmentClient, treatmentToPut);

        // Act - Count
        var countAfter = await GetCount(_treatmentClient);

        // Act - Query
        var query = await Query<List<TreatmentViewModel>>(_treatmentClient, new TreatmentFilter(id: treatmentPuted.Id));
        var treatmentQueried = query.FirstOrDefault();

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(treatmentToPut, treatmentPuted);
        Assert.Equivalent(treatmentPuted, treatmentQueried);
    }

    [Fact]
    public async Task Put_ValidTreatmentWithNonUniqueNameAndDifferentType_Ok()
    {
        // Arrange - Treatment
        var treatment = await GetTreatment();
        var treatmentToPut = new TreatmentViewModelFake(treatment.Id, name: treatment.Name).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_treatmentClient);

        // Act - Treatment
        var treatmentPuted = await Put<TreatmentViewModel>(_treatmentClient, treatmentToPut);

        // Act - Count
        var countAfter = await GetCount(_treatmentClient);

        // Act - Query
        var query = await Query<List<TreatmentViewModel>>(_treatmentClient, new TreatmentFilter(id: treatmentPuted.Id));
        var treatmentQueried = query.FirstOrDefault();

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(treatmentToPut, treatmentPuted);
        Assert.Equivalent(treatmentPuted, treatmentQueried);
    }

    [Fact]
    public async Task Put_TreatmentInvalidWithNonUniqueNamesAndType_Conflict()
    {
        // Arrange - Treatment
        var treatment = await GetTreatment();
        var treatmentToPut = new TreatmentViewModelFake(treatment.Id, name: treatment.Name, type: treatment.Type).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_treatmentClient);

        // Act - Treatment
        var exception = await Put<AppException>(_treatmentClient, treatmentToPut);

        // Act - Count
        var countAfter = await GetCount(_treatmentClient);

        // Act - Query
        var query = await Query<List<TreatmentViewModel>>(_treatmentClient, new TreatmentFilter(id: treatmentToPut.Id));
        var treatmentQueried = query.FirstOrDefault();

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEqual(HttpStatusCode.Conflict, exception.StatusCode);
        Assert.Equal(treatment, treatmentQueried);
    }
}
