using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using Repositories;
using WebApp.Controllers;
using WebApp.Models;
using Xunit;
using TestDataBuilder;

namespace WebApp.Test
{
    public class UsersControllerTests
    {
        private readonly Mock<IRepository> _mockRepository;
        private readonly UsersController _usersController;
        private const string Username = "User";

        public UsersControllerTests()
        {
            _mockRepository = new Mock<IRepository>();
            _usersController = new UsersController(_mockRepository.Object);
        }

        [Fact]
        public void Index_UserNotFound_ReturnsNoResultsFound()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetDetailsForUser(Username)).Returns(default(User));

            //Act
            var result = _usersController.Index(Username) as RedirectToActionResult;

            //Assert
            result.ActionName.Should().Be("NoResultsFound");
            result.RouteValues.Should().Contain("message", $"{Username} does not exists");
        }

        [Fact]
        public void Index_UserFound_ReturnsUserDetails()
        {
            //Arrange
            //var user = Builder<User>.CreateNew().Build();
            var user = Builder.CreateUser().Build();

            _mockRepository.Setup(x => x.GetDetailsForUser(Username)).Returns(user);
            _mockRepository.Setup(x => x.GetReposForUserFromUrl(user.Repos_Url)).Returns(new List<Repo>());

            //Act
            var result = _usersController.Index(Username) as ViewResult;

            //Assert
            var userViewModel = result.Model as UserViewModel;
            userViewModel.Name.Should().Be(user.Name);
            userViewModel.Location.Should().Be(user.Location);
            userViewModel.AvatarUrl.Should().Be(user.Avatar_url);
        }

        [Fact]
        public void Index_ManyRepos_ReturnsTop5StaredRepos()
        {
            //Arrange
            //var repos = Builder<Repo>.CreateListOfSize(10).Build();
            var repos = Builder.GetListOfRepos(10).Build();

            _mockRepository.Setup(x => x.GetDetailsForUser(Username)).Returns(new User());
            _mockRepository.Setup(x => x.GetReposForUserFromUrl(It.IsAny<string>())).Returns(repos);

            //Act
            var result = _usersController.Index(Username) as ViewResult;

            //Assert
            var userViewModel = result.Model as UserViewModel;
            userViewModel.Repos.Should().HaveCount(5);
            userViewModel.Repos.Should().BeInDescendingOrder(x => x.StarGazers_Count);
        }
    }
}