using stroyinvest.Model;
using System;
using System.Linq;

namespace stroyinvest.ViewModel
{
    public static class UserVM
    {
        public static Core db = new Core();

        /// <summary>
        /// Добавляет нового пользователя в базу данных.
        /// </summary>
        /// <param name="firstname">Имя пользователя.</param>
        /// <param name="lastname">Фамилия пользователя.</param>
        /// <param name="email">Email пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <param name="userRoleId">Идентификатор роли пользователя.</param>
        /// <param name="builderName">Название строительной компании (необязательно).</param>
        /// <param name="patronymic">Отчество пользователя (необязательно).</param>
        public static void AddUser(string firstname, string lastname, string email, string password, int userRoleId, string builderName = null, string patronymic = null)
        {
            db.context.Users.Add(new Users
            {
                UserFirstName = firstname,
                UserLastName = lastname,
                UserEmail = email,
                UserPatronymicName = patronymic,
                UserBuilderName = builderName,
                UserPassword = password,
                UserRoleId = userRoleId

            });
            db.context.SaveChanges();
        }

        /// <summary>
        /// Проверяет авторизацию пользователя по email и паролю.
        /// </summary>
        /// <param name="email">Email пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>True, если авторизация успешна, иначе False.</returns>
        /// <exception cref="Exception">Возвращает исключение, если email или пароль не введены, или пользователь не найден.</exception>
        public static bool AuthCheck(string email, string password)
        {
            int checkUser = db.context.Users.Where(x => x.UserEmail == email && x.UserPassword == password).Count();
            if (String.IsNullOrEmpty(email))
            {
                throw new Exception("Не введён e-mail");
            }
            else if (String.IsNullOrEmpty(password))
            {
                throw new Exception("Не введён пароль");
            }
            else
            {
                if (checkUser == 0)
                {
                    throw new Exception("Пользователь не найден.\nПроверьте правильность связки Логин-Пароль.");
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
