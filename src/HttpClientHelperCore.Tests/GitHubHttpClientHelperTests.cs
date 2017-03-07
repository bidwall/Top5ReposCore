using System;
using System.Linq.Expressions;
using Moq;
using Xunit;

namespace HttpClientHelpers.Test
{
    public class GitHubHttpClientHelperTests
    {
        private readonly Mock<IHttpResponseProvider> _mockHttpReponseProvider;
        private readonly GitHubHttpClientHelper _gitHubHttpClientHelper;
        private const string Url = "http://github.com/";

        
        public GitHubHttpClientHelperTests()
        {
            _mockHttpReponseProvider = new Mock<IHttpResponseProvider>();
            _gitHubHttpClientHelper = new GitHubHttpClientHelper(_mockHttpReponseProvider.Object);
        }

        [Fact]
        public void GetDataFromUrl_GiveUrl_SetsBaseAddress()
        {
            //Act
            _gitHubHttpClientHelper.GetDataFromUrl<string>(Url);

            //Assert
            Expression<Func<HttpClientConfig, bool>> expression = y => y.BaseAddress.AbsoluteUri == Url;
            _mockHttpReponseProvider.Verify(x => x.GetResponse<string>(It.Is(expression)), Times.Once);
        }

        [Fact]
        public void GetDataFromUrl_SetsRequestUriToEmptyString()
        {
            //Act
            _gitHubHttpClientHelper.GetDataFromUrl<string>(Url);

            //Assert
            Expression<Func<HttpClientConfig, bool>> expression = y => y.RequestUri == string.Empty;
            _mockHttpReponseProvider.Verify(x => x.GetResponse<string>(It.Is(expression)), Times.Once);
        }

        [Fact]
        public void GetDataFromUrl_SetsAcceptsHeaders()
        {
            //Act
            _gitHubHttpClientHelper.GetDataFromUrl<string>(Url);

            //Assert
            Expression<Func<HttpClientConfig, bool>> expression = y => y.AcceptHeaders.Count == 1 && y.AcceptHeaders[0].MediaType == Constants.JsonContentType;
            _mockHttpReponseProvider.Verify(x => x.GetResponse<string>(It.Is(expression)), Times.Once);
        }

        [Fact]
        public void GetDataFromUrl_SetsUserAgentHeaders()
        {
            //Act
            _gitHubHttpClientHelper.GetDataFromUrl<string>(Url);

            //Assert
            Expression<Func<HttpClientConfig, bool>> expression = y => y.UserAgentHeaders.Count == 1 &&
                                                                       y.UserAgentHeaders[0].Key == Constants.UserAgentHeaderKey &&
                                                                       y.UserAgentHeaders[0].Value == string.Empty;

            _mockHttpReponseProvider.Verify(x => x.GetResponse<string>(It.Is(expression)), Times.Once);
        }
    }
}