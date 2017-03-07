using System.Collections.Generic;
//using FizzWare.NBuilder;
using TestDataBuilder;
using FluentAssertions;
using HttpClientHelpers;
using Models;
using Moq;
using Xunit;

namespace Repositories.Test
{
    public class GitHubRepositoryTests
    {
        private readonly Mock<IHttpClientHelper> _mockHttpClientHelper;
        private readonly GitHubRepository _gitHubRepository;

        private const string UserName = "username";
        private const string RepoUrl = "url";
        private readonly string _userUrl = $"{GitHubRepository.GitHubUri}/users/{UserName}";
        
        public GitHubRepositoryTests()
        {
            _mockHttpClientHelper = new Mock<IHttpClientHelper>();
            _gitHubRepository = new GitHubRepository(_mockHttpClientHelper.Object);
        }

        [Fact]
        public void GetDetailsForUser_ConstructsGitHubUrl()
        {
            //Act
            _gitHubRepository.GetDetailsForUser(UserName);

            //Assert
            _mockHttpClientHelper.Verify(x => x.GetDataFromUrl<User>(_userUrl), Times.Once);
        }

        [Fact]
        public void GetDetailsForUser_ReturnsUser()
        {
            //Arrange
            //var testUser = Builder<User>.CreateNew().Build();
            var testUser = Builder.CreateUser().Build();
            _mockHttpClientHelper.Setup(x => x.GetDataFromUrl<User>(It.IsAny<string>())).Returns(testUser);
            
            //Act
            var user = _gitHubRepository.GetDetailsForUser(UserName);

            //Assert
            user.Name.Should().Be(testUser.Name);
        }

        [Fact]
        public void GetReposForUserFromUrl_GetReposForUrl()
        {
            //Act
            _gitHubRepository.GetReposForUserFromUrl(RepoUrl);

            //Assert
            _mockHttpClientHelper.Verify(x => x.GetDataFromUrl<IEnumerable<Repo>>(RepoUrl), Times.Once);
        }

        [Fact]
        public void GetReposForUserFromUrl_ReturnsRepos()
        {
            //Arrange
            //var testRepos = Builder<Repo>.CreateListOfSize(1).Build();
            var testRepos = Builder.GetListOfRepos(1).Build();
            _mockHttpClientHelper.Setup(x => x.GetDataFromUrl<IEnumerable<Repo>>(It.IsAny<string>())).Returns(testRepos);

            //Act
            var repos = _gitHubRepository.GetReposForUserFromUrl(RepoUrl);

            //Assert
            repos.Should().BeEquivalentTo(testRepos);
        }
    }
}
