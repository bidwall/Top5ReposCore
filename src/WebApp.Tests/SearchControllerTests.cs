using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using WebApp.Models;
using Xunit;

namespace WebApp.Test
{
    public class SearchControllerTests
    {
        private readonly SearchController _searchController;

        public SearchControllerTests()
        {
            _searchController = new SearchController();
        }

        [Fact]
        public void IndexPost_Invalid_ReturnsViewWithModel()
        {
            //Arrange
            _searchController.ModelState.AddModelError("", "");

            //Act
            var result = _searchController.Index(new SearchViewModel()) as ViewResult;

            //Assert
            result.Should().NotBeNull();
            result.ViewName.Should().BeNullOrEmpty();
        }

        [Fact]
        public void IndexPost_ValidModel_RedirectsToUsersIndex()
        {
            //Arrange
            _searchController.ModelState.Clear();

            //Act
            var result = _searchController.Index(new SearchViewModel()) as RedirectToActionResult;

            //Assert
            result.Should().NotBeNull();
            result.ActionName.Should().Be("Index");
            result.ControllerName.Should().Be("Users");
        }
    }
}
