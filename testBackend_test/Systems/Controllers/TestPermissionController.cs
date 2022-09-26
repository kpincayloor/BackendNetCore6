using System.Threading.Tasks;
using Xunit;
using Moq;
using testBackend_test.MockData;
using FluentAssertions;
using DataAccess.Entity;
using testBackend.Controllers;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace testBackend_test.Systems.Controllers
{
    public class TestPermissionController
    {
        [Fact]
        public async Task GetAsync_ShouldReturn200Status()
        {
            //Arrange
            var permissionService = new Mock<IQueryHandler<Permission>>();
            permissionService.Setup(_ => _.GetPermissions()).Returns(PermissionMockData.GetPermissions());
            var sut = new PermissionController(permissionService.Object, null);

            //Act
            var result = (OkObjectResult)await sut.Get();

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAsync_ShouldReturn204NoContentStatus()
        {
            /// Arrange
            var permissionService = new Mock<IQueryHandler<Permission>>();
            permissionService.Setup(_ => _.GetPermissions()).Returns(PermissionMockData.GetEmptyPermissions());
            var sut = new PermissionController(permissionService.Object, null);

            /// Act
            var result = (NoContentResult)await sut.Get();


            /// Assert
            result.StatusCode.Should().Be(204);
            permissionService.Verify(_ => _.GetPermissions(), Times.Exactly(1));
        }        

        [Fact]
        public async Task SaveAsync_ShouldCall_ITodoService_SaveAsync_AtleastOnce()
        {
            /// Arrange
            var permissionService = new Mock<ICommandHandler<Permission>>();
            var newPermission = PermissionMockData.NewPermission();
            var sut = new PermissionController(null, permissionService.Object);

            /// Act
            var result = await sut.Post(newPermission);

            /// Assert
            permissionService.Verify(_ => _.Execute(newPermission), Times.Exactly(1));
        }
    }
}
