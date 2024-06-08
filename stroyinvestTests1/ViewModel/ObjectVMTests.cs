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
    public class ObjectVMTests
    {
        Core db = new Core();
        /// <summary>
        /// Тест создания и удаления объекта.
        /// </summary>
        [TestMethod]
        public void CreateNewObject_RemoveObject_ObjectCreatedAndRemoved()
        {
            // Arrange
            string objectName = "Тестовый объект";
            int objectPrice = 1000000;
            int objectTypeId = 1;
            int objectRoomCount = 3;
            int objectSquare = 100;
            string objectAddress = "ул. Тестовая, 1";
            string objectPhotoPath = "path/to/photo.jpg";
            int objectBuilderId = 1;
            int objectStatusId = 1;

            // Act
            ObjectVM.CreateNewObject(objectName, objectPrice, objectTypeId, objectRoomCount, objectSquare, objectAddress, objectPhotoPath, objectBuilderId, objectStatusId);

            // Получение созданного объекта
            var createdObject = db.context.Objects.FirstOrDefault(o => o.ObjectName == objectName);

            // Удаление объекта
            ObjectVM.RemoveObject(createdObject);

            // Assert
            // Проверяем, что объект был удален
            Assert.IsNull(db.context.Objects.FirstOrDefault(o => o.ObjectName == objectName));
        }
    }
}