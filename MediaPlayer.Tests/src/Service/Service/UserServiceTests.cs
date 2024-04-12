using MediaPlayer.Core.src.Entity;
using MediaPlayer.Core.src.Enums;
using MediaPlayer.Core.src.RepositoryAbstraction;
using MediaPlayer.Service.Service;
using MediaPlayer.Service.src.DTO;
using Moq;

namespace MediaPlayer.Tests.src.Service.Service
{
    public class UserServiceTests
    {

        public static IEnumerable<object[]> ValidUserCreateData =>
            TestUtils.ValidUserCreateData;
        private Mock<IUserRepository> _mockUserRepo = new Mock<IUserRepository>();

        private Mock<Admin> _mockAdmin = new Mock<Admin>("admin", "admin", "Admin");

        [Theory]
        [MemberData(nameof(ValidUserCreateData))]
        public void AddUser_WithValidUData_ShouldAddAndReturnCorrectUserType(UserCreateDto userCreate)
        {
            var testUserName = "test";
            User? userFound = null;
            _mockUserRepo.Setup(x => x.GetUserByName(testUserName)).Returns(userFound);
            var userService = new UserService(_mockUserRepo.Object, _mockAdmin.Object);
            var result = userService.AddUser(userCreate);
            Assert.Equal(userCreate.Type, result.Type);
            Assert.Equal(result.Username, userCreate.Username);
            _mockUserRepo.Verify(x => x.Add(It.IsAny<User>()), Times.Once());
        }

        [Fact]
        public void AddUser_WithExistingUsername_ShouldNotAddAndReturnNull()
        {
            _mockUserRepo
                .Setup(x => x.GetUserByName(TestUtils.UserCreate.Username))
                .Returns(TestUtils.User1);
            var userService = new UserService(_mockUserRepo.Object, _mockAdmin.Object);
            var result = userService.AddUser(TestUtils.UserCreate);
            Assert.Null(result);
            _mockUserRepo.Verify(x => x.Add(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public void DeleteUserById_WithFoundUser_ShouldDeleteAndReturnTrue()
        {
            var id = Guid.NewGuid();
            _mockUserRepo.Setup(x => x.GetUserById(id)).Returns(TestUtils.User1);
            _mockUserRepo.Setup(x => x.Remove(TestUtils.User1));
            var userService = new UserService(_mockUserRepo.Object, _mockAdmin.Object);
            var result = userService.DeleteUserById(id);
            Assert.True(result);
            _mockUserRepo.Verify(); // Assert both mocked repo methods should be called with provided parameters
        }

        [Fact]
        public void DeleteUserById_WithUnFoundUser_ShouldNotDeleteAndReturnFalse()
        {
            var id = Guid.NewGuid();
            User? user = null;
            _mockUserRepo.Setup(x => x.GetUserById(id)).Returns(user);
            var userService = new UserService(_mockUserRepo.Object, _mockAdmin.Object);
            var result = userService.DeleteUserById(id);
            Assert.False(result);
            _mockUserRepo.Verify(x => x.Remove(It.IsAny<User>()), Times.Never); // Assert Remove not be called
        }

        [Fact]
        public void UpdateUser_WithUserFound_ShouldUpdateAndReturnTrue()
        {
            var id = Guid.NewGuid();
            _mockUserRepo.Setup(x => x.GetUserById(id)).Returns(TestUtils.User1);
            var userService = new UserService(_mockUserRepo.Object, _mockAdmin.Object);
            var result = userService.UpdateUser(id, TestUtils.validUserUpdate);
            Assert.True(result);
            _mockUserRepo.Verify();
        }

        [Fact]
        public void UpdateUser_WithUserNotFound_ShouldNotUpdateAndReturnFalse()
        {
            var id = Guid.NewGuid();
            User? user = null;
            _mockUserRepo.Setup(x => x.GetUserById(id)).Returns(user);
            var userService = new UserService(_mockUserRepo.Object, _mockAdmin.Object);
            var result = userService.UpdateUser(id, TestUtils.validUserUpdate);
            Assert.False(result);
            _mockUserRepo.Verify(
                x =>
                    x.Update(
                        TestUtils.User1,
                        TestUtils.validUserUpdate.Email,
                        TestUtils.validUserUpdate.FullName
                    ),
                Times.Never
            );
        }
    }
}
