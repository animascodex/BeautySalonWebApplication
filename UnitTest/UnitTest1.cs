using BeautySalonWebApplication.Controllers;
using BeautySalonWebApplication.Data;
using BeautySalonWebApplication.Models;
using BeautySalonWebApplication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace BeautySalonWebApplication.Tests
{
    public class AppointmentsControllerTests
    {
        private readonly ITestOutputHelper _output;

        public AppointmentsControllerTests(ITestOutputHelper output)
        {
            _output = output;
        }
        [Fact]
        public async Task TestUserCreationMethod()
        {
            _output.WriteLine("Starting the test...");
            // Arrange: Create a mock instance of UserManager<ApplicationUser>
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            var mockDbContext = Mock.Of<ApplicationDbContext>();

            var mockEmailService = Mock.Of<IEmailService>();

            var controller = new AppointmentsController(mockDbContext, mockEmailService, mockUserManager.Object);


            var viewModel = new RegisterViewModel
            {
                Email = "example@example.com",
                Password = "Password123!"
                // You can initialize other properties as needed
            };


            // Configure the mock to return a success result when CreateAsync is called
            mockUserManager.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                            .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await controller.Register(viewModel);

            // Assert: Check if the user creation operation succeeded
            Xunit.Assert.NotNull(result);
            _output.WriteLine("User creation should succeed");
        }
    }

}
