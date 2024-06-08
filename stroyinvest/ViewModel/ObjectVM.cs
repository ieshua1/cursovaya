using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using stroyinvest.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace stroyinvest.ViewModel
{
    public class ObjectVM
    {

        public static Core db = new Core();

        /// <summary>
        /// Удаляет объект из базы данных.
        /// </summary>
        /// <param name="delObject">Объект, который нужно удалить.</param>
        public static void RemoveObject(Objects delObject)
        {
            try
            {
                db.context.Objects.Remove(db.context.Objects.Where(x => x.IdObject == delObject.IdObject).FirstOrDefault());
                db.context.SaveChanges();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Создает новый объект в базе данных.
        /// </summary>
        /// <param name="objectName">Название объекта.</param>
        /// <param name="objectPrice">Цена объекта.</param>
        /// <param name="objectTypeId">Идентификатор типа объекта.</param>
        /// <param name="objectRoomCount">Количество комнат в объекте.</param>
        /// <param name="objectSquare">Площадь объекта.</param>
        /// <param name="objectAddress">Адрес объекта.</param>
        /// <param name="objectPhotoPath">Путь к фотографии объекта.</param>
        /// <param name="objectBuilderId">Идентификатор застройщика объекта.</param>
        /// <param name="objectStatusId">Идентификатор статуса объекта.</param>
        /// <param name="objectDescription">Описание объекта (необязательно).</param>
        public static void CreateNewObject(
            string objectName,  int objectPrice, 
            int objectTypeId, int objectRoomCount, int objectSquare,
            string objectAddress, string objectPhotoPath, int objectBuilderId,
            int objectStatusId, string objectDescription = null)
        {
            try
            {
                Objects newobject = new Objects()
                {
                    ObjectName = objectName,
                    ObjectDescription = objectDescription,
                    ObjectPrice = objectPrice,
                    ObjectTypeId = objectTypeId,
                    ObjectRoomCount = objectRoomCount,
                    ObjectSquare = objectSquare,
                    ObjectAddress = objectAddress,
                    ObjectPhotoPath = objectPhotoPath != null ? objectPhotoPath : "../Resources/Images/placeholder.jpg",
                    ObjectBuilderId = objectBuilderId,
                    ObjectStatusId = objectStatusId,
                };
                db.context.Objects.Add(newobject);
                db.context.SaveChanges();
            }
            catch (Exception ex) { throw new Exception(ex.Message);}
        }

        
 
    }
}

