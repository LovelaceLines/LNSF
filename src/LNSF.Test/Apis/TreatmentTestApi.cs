using System.Net;
using LNSF.Api.ViewModels;
using LNSF.Domain.Enums;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using Xunit;

namespace LNSF.Test.Apis;

public class TreatmentTestApi : GlobalClientRequest
{
    [Fact]
    public async Task Post_TreatmentValid_Ok()
    {
        // Arrange - Treatment
        var treatmentFake = new TreatmentPostViewModelFake().Generate();

        // Arrange - Count
        var countBefore = await GetCount(_treatmentClient);

        // Act - Treatment
        var treatmentPosted = await Post<TreatmentViewModel>(_treatmentClient, treatmentFake);

        // Act - Count
        var countAfter = await GetCount(_treatmentClient);

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(treatmentFake, treatmentPosted);
    }

    [Fact]
    public async Task Post_TreatmentValidWithNonUniqueNamesAndDifferentTypes_Ok()
    {
        // Arrange - Treatment
        var treatmentFake1 = new TreatmentPostViewModelFake().Generate();
        var treatmentPosted1 = await Post<TreatmentViewModel>(_treatmentClient, treatmentFake1);

        // Arrange - Count
        var countBefore = await GetCount(_treatmentClient);

        // Act - Treatment
        var treatmentFake2 = new TreatmentPostViewModelFake().Generate();
        treatmentFake2.Name = treatmentFake1.Name;
        if (treatmentFake2.Type == treatmentFake1.Type) treatmentFake2.Type = treatmentFake1.Type == TypeTreatment.OTHER ? TypeTreatment.CANCER : TypeTreatment.OTHER;
        var treatmentPosted2 = await Post<TreatmentViewModel>(_treatmentClient, treatmentFake2);

        // Act - Count
        var countAfter = await GetCount(_treatmentClient);

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(treatmentFake1, treatmentPosted1);
        Assert.Equivalent(treatmentFake2, treatmentPosted2);
    }

    [Fact]
    public async Task Post_TreatmentInvalidWithNonUniqueNamesAndType_BadRequest()
    {
        // Arrange - Treatment
        var treatmentFake1 = new TreatmentPostViewModelFake().Generate();
        var treatmentPosted1 = await Post<TreatmentViewModel>(_treatmentClient, treatmentFake1);

        // Arrange - Count
        var countBefore = await GetCount(_treatmentClient);

        // Act - Treatment
        var exception = await Post<AppException>(_treatmentClient, treatmentFake1);

        // Act - Count
        var countAfter = await GetCount(_treatmentClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual(HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exception.StatusCode);
    }

    [Theory]
    [InlineData("a")]
    [InlineData("ab")]
    [InlineData("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz")]
    public async Task Post_TreatmentInvalidWithShortName_BadRequest(string name)
    {
        // Arrange - Treatment
        var treatmentFake = new TreatmentPostViewModelFake().Generate();
        treatmentFake.Name = name;

        // Arrange - Count
        var countBefore = await GetCount(_treatmentClient);

        // Act - Treatment
        var exception = await Post<AppException>(_treatmentClient, treatmentFake);

        // Act - Count
        var countAfter = await GetCount(_treatmentClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual(HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exception.StatusCode);
    }

    [Fact]
    public async Task Put_TreatmentValid_Ok()
    {
        // Arrange - Treatment
        var treatmentFake1 = new TreatmentPostViewModelFake().Generate();
        var treatmentPosted = await Post<TreatmentViewModel>(_treatmentClient, treatmentFake1);

        // Arrange - Count
        var countBefore = await GetCount(_treatmentClient);

        // Act - Treatment
        var treatmentFake2 = new TreatmentPostViewModelFake().Generate();
        var treatmentMapped = _mapper.Map<TreatmentViewModel>(treatmentFake2);
        treatmentMapped.Id = treatmentPosted.Id;
        var treatmentPuted = await Put<TreatmentViewModel>(_treatmentClient, treatmentMapped);

        // Act - Count
        var countAfter = await GetCount(_treatmentClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(treatmentMapped, treatmentPuted);
    }

    [Fact]
    public async Task Put_TreatmentValidWithNonUniqueNamesAndDifferentTypes_Ok()
    {
        // Arrange - Treatment
        var treatmentFake = new TreatmentPostViewModelFake().Generate();
        var treatmentPosted = await Post<TreatmentViewModel>(_treatmentClient, treatmentFake);

        // Arrange - Count
        var countBefore = await GetCount(_treatmentClient);

        // Act - Treatment
        var treatmentPut = treatmentPosted;
        treatmentPut.Type = treatmentPosted.Type == TypeTreatment.OTHER ? TypeTreatment.CANCER : TypeTreatment.OTHER;
        var treatmentPuted = await Put<TreatmentViewModel>(_treatmentClient, treatmentPut);

        // Act - Count
        var countAfter = await GetCount(_treatmentClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(treatmentPut, treatmentPuted);
    }

    [Fact]
    public async Task Put_TreatmentInvalidWithNonUniqueNamesAndType_BadRequest()
    {
        // Arrange - Treatment
        var treatmentFake1 = new TreatmentPostViewModelFake().Generate();
        var treatmentPosted1 = await Post<TreatmentViewModel>(_treatmentClient, treatmentFake1);
        var treatmentFake2 = new TreatmentPostViewModelFake().Generate();
        var treatmentPosted2 = await Post<TreatmentViewModel>(_treatmentClient, treatmentFake2);

        // Arrange - Count
        var countBefore = await GetCount(_treatmentClient);

        // Act - Treatment
        var treatmentPut = treatmentPosted1;
        treatmentPut.Id = treatmentPosted2.Id;        
        var exception = await Put<AppException>(_treatmentClient, treatmentPut);

        // Act - Count
        var countAfter = await GetCount(_treatmentClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual(HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exception.StatusCode);
    } 
}
