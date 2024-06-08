using stroyinvest.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace stroyinvest.ViewModel
{
    public class UserManagementVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Users> _users;

        private Core db = new Core();
        public ObservableCollection<Users> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public ICommand DeleteUserCommand { get; private set; }

        public UserManagementVM()
        {
            // Инициализация команд
            DeleteUserCommand = new RelayCommand<Users>(DeleteUser);
        }

        /// <summary>
        /// Загружает список пользователей из базы данных в ViewModel.
        /// </summary>
        public void LoadUsers()
        {
                // Получаем список пользователей из базы данных
                var users = db.context.Users.ToList();

                // Обновляем список пользователей в ViewModel
                Users = new ObservableCollection<Users>(users);
            
        }

        /// <summary>
        /// Удаляет пользователя из базы данных и обновляет список пользователей в ViewModel.
        /// </summary>
        /// <param name="user">Пользователь, которого нужно удалить.</param>
        public void DeleteUser(Users user)
        {
            
                db.context.Users.Remove(db.context.Users.Where(x => x.IdUser == user.IdUser).FirstOrDefault());
                db.context.SaveChanges();

                // Обновляем список пользователей
                LoadUsers();
            
        }
        /// <summary>
        /// Вызывает событие PropertyChanged для указанного свойства.
        /// </summary>
        /// <param name="propertyName">Имя свойства, для которого вызывается событие.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    // Класс RelayCommand
    public class RelayCommand<T> : ICommand
    {
        private Action<T> _execute;
        private Func<T, bool> _canExecute;

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }
        /// <summary>
        /// Событие, которое вызывается при изменении состояния возможности выполнения команды.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Проверяет, может ли команда быть выполнена.
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        /// <returns>True, если команда может быть выполнена, иначе False.</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        /// <summary>
        /// Выполняет команду.
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
