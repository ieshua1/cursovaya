using Microsoft.VisualStudio.TestTools.UnitTesting;
using stroyinvest.Model;
using stroyinvest.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stroyinvest.ViewModel.Tests
{
    [TestClass()]
    public class UserVMTests
    {
        Core db = new Core();
        /// <summary>
        /// Тест проверки валидности логина при вводе пустой строки.
        /// Ожидается исключение.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void AuthCheckTest_NoLoginField_ThrowsException()
        {
            bool auth = UserVM.AuthCheck("", "password");
        }
        /// <summary>
        /// Тестирование обработки пустого значения в поле пароля при авторизации.
        /// Ожидается исключение.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void AuthCheckTest_NoPasswordField_ThrowsException()
        {
            bool auth = UserVM.AuthCheck("login", "");
        }
        /// <summary>
        /// Тест валидации учетных данных при вводе несуществующего пользователя.
        /// Ожидается исключение.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void AuthCheckTest_UserDoesntExist_ThrowsException()
        {
            bool auth = UserVM.AuthCheck(".", ".");
        }

        /// <summary>
        /// Тест обработки авторизации с корректными учетными данными.
        /// Предполагается, что пользователь со связкой "admin:admin" существует в базе данных.
        /// </summary>
        [TestMethod()]
        public void AuthCheckTest_Admin_ReturnsTrue()
        {
            bool auth = UserVM.AuthCheck("admin", "admin");
            Assert.IsTrue(auth);
        }


        /// <summary>
        /// Тест добавления нового пользователя в базу данных.
        /// </summary>
        [TestMethod()]
        public void AddUser_ValidData_UserAdded()
        {
            // Arrange
            string firstname = "Иван";
            string lastname = "Иванов";
            string email = "ivan.ivanov@example.com";
            string password = "password123";
            int userRoleId = 1;

            // Act
            UserVM.AddUser(firstname, lastname, email, password, userRoleId);

            // Assert
            // Проверяем, что пользователь был добавлен в базу данных
            Assert.IsTrue(db.context.Users.Any(u => u.UserFirstName == firstname && u.UserLastName == lastname && u.UserEmail == email));
        }
        /// <summary>
        /// Тест добавления нового пользователя в базу данных с отчеством и названием строительной компании.
        /// </summary>
        [TestMethod()]
        public void AddUser_WithPatronymicAndBuilderName_UserAdded()
        {
            // Arrange
            string firstname = "Иван";
            string lastname = "Иванов";
            string email = "ivan.ivanov@example.com";
            string password = "password123";
            int userRoleId = 1;
            string patronymic = "Иванович";
            string builderName = "Стройком";

            // Act
            UserVM.AddUser(firstname, lastname, email, password, userRoleId, builderName, patronymic);

            // Assert
            // Проверяем, что пользователь был добавлен в базу данных с отчеством и названием строительной компании
            Assert.IsTrue(db.context.Users.Any(u => u.UserFirstName == firstname && u.UserLastName == lastname && u.UserEmail == email && u.UserPatronymicName == patronymic && u.UserBuilderName == builderName));
        }

        /// <summary>
        /// Тест добавления нового пользователя в базу данных с пустым отчеством.
        /// </summary>
        [TestMethod()]
        public void AddUser_WithEmptyPatronymic_UserAdded()
        {
            // Arrange
            string firstname = "Иван";
            string lastname = "Иванов";
            string email = "ivan.ivanov@example.com";
            string password = "password123";
            int userRoleId = 1;
            string patronymic = "";
            string builderName = "Стройком";

            // Act
            UserVM.AddUser(firstname, lastname, email, password, userRoleId, builderName, patronymic);

            // Assert
            // Проверяем, что пользователь был добавлен в базу данных с пустым отчеством
            Assert.IsTrue(db.context.Users.Any(u => u.UserFirstName == firstname && u.UserLastName == lastname && u.UserEmail == email && u.UserPatronymicName == patronymic && u.UserBuilderName == builderName));
        }

        /// <summary>
        /// Тест добавления нового пользователя в базу данных с пустым названием строительной компании.
        /// </summary>
        [TestMethod()]
        public void AddUser_WithEmptyBuilderName_UserAdded()
        {
            // Arrange
            string firstname = "Иван";
            string lastname = "Иванов";
            string email = "ivan.ivanov@example.com";
            string password = "password123";
            int userRoleId = 1;
            string patronymic = "Иванович";
            string builderName = "";

            // Act
            UserVM.AddUser(firstname, lastname, email, password, userRoleId, builderName, patronymic);

            // Assert
            // Проверяем, что пользователь был добавлен в базу данных с пустым названием строительной компании
            Assert.IsTrue(db.context.Users.Any(u => u.UserFirstName == firstname && u.UserLastName == lastname && u.UserEmail == email && u.UserPatronymicName == patronymic && u.UserBuilderName == builderName));
        }

}
}