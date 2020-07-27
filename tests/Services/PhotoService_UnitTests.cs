using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Xunit;

public class PhotoService_UnitTests
{
    [Fact]
    public async void GetAlbumById_CallsApiEndpointAsync()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(@"[
                                             {
                                                ""albumId"": 1,
                                                ""id"": 1,
                                                ""title"": ""accusamus beatae ad facilis cum similique qui sunt"",
                                                ""url"": ""https://via.placeholder.com/600/92c952"",
                                                ""thumbnailUrl"": ""https://via.placeholder.com/150/92c952""
                                             },
                                             {
                                                ""albumId"": 1,
                                                ""id"": 2,
                                                ""title"": ""reprehenderit est deserunt velit ipsam"",
                                                ""url"": ""https://via.placeholder.com/600/771796"",
                                                ""thumbnailUrl"": ""https://via.placeholder.com/150/771796""
                                             }
                                          ]"),
        };

        handlerMock
           .Protected()
           .Setup<Task<HttpResponseMessage>>(
              "SendAsync",
              ItExpr.IsAny<HttpRequestMessage>(),
              ItExpr.IsAny<CancellationToken>())
           .ReturnsAsync(response)
           .Verifiable();
        var httpClient = new HttpClient(handlerMock.Object);
        var photoService = new PhotoService(httpClient);

        var retrievedPhotos = await photoService.GetAlbumByIdAsync(1);

        Assert.NotNull(retrievedPhotos);
        Assert.Equal(1, retrievedPhotos[0].AlbumId);
        Assert.Equal(1, retrievedPhotos[1].AlbumId);

        Assert.Equal(1, retrievedPhotos[0].Id);
        Assert.Equal(2, retrievedPhotos[1].Id);
        
        Assert.Equal("accusamus beatae ad facilis cum similique qui sunt", retrievedPhotos[0].Title);
        Assert.Equal("reprehenderit est deserunt velit ipsam", retrievedPhotos[1].Title);
        
        Assert.Equal("https://via.placeholder.com/600/92c952", retrievedPhotos[0].Url);
        Assert.Equal("https://via.placeholder.com/600/771796", retrievedPhotos[1].Url);
        
        Assert.Equal("https://via.placeholder.com/150/92c952", retrievedPhotos[0].ThumbnailUrl);
        Assert.Equal("https://via.placeholder.com/150/771796", retrievedPhotos[1].ThumbnailUrl);
    }
}