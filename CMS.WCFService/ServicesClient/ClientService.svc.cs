using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CMS.BussinessLayer.Clients;
using CMS.Entities;
using CMS.Entities.ClientObjects;
using CMS.Kernel;

namespace CMS.WCFService.ServicesClient
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ClientService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ClientService.svc or ClientService.svc.cs at the Solution Explorer and start debugging.
    [ServiceContract]
    public class ClientService
    {
        [OperationContract]
        public CONews CreateNews(CONews newsToCreate)
        {
            try
            {
                //using (var sessionOfWork = new SessionOfWork())
                //{
                using (var unitOfWork = new UnitOfWork<DBClientContext>(true))
                {
                    var generalService = unitOfWork.GetService<BusinessClientService>();
                    generalService.SaveNews(newsToCreate);
                    unitOfWork.Comit();
                    return newsToCreate;
                }
                //}
            }
            catch (Exception ex)
            {
                //Todo log file
                return null;
            }

        }


        [OperationContract]
        public IEnumerable<CONews> GetNews()
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBClientContext>(false))
                {
                    var generalService = unitOfWork.GetService<BusinessClientService>();

                    return generalService.GetNews();
                }
            }
            catch (Exception ex)
            {
                //Todo Log
                return null;
            }
        }

        [OperationContract]
        public CONews GetNewByID(int Id)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBClientContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessClientService>();
                    return generalService.GetByID(Id);
                }
            }
            catch
            {
                //Todo Logs
                return null;
            }
        }

        [OperationContract]
        public CONews Update(CONews entity)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBClientContext>())
                {

                    var generalService = unitOfWork.GetService<BusinessClientService>();
                    generalService.SaveNews(entity);
                    unitOfWork.Comit();
                    return generalService.GetByID(entity.EntityId);


                }
            }
            catch (Exception ex)
            {
                //Todo log file
                return null;
            }
        }


        [OperationContract]
        public string DeleteNews(int entityID, bool isSoftDeleted)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBClientContext>())
                {

                    var generalService = unitOfWork.GetService<BusinessClientService>();
                    var result = generalService.DeleteNews(entityID, isSoftDeleted);
                    unitOfWork.Comit();
                    return result;

                }
            }
            catch (Exception ex)
            {
                //Todo log file
                return ex.Message;
            }
        }

        [OperationContract]
        public string SaveMultiNews(string title1, string title2)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBClientContext>())
                {

                    var generalService = unitOfWork.GetService<BusinessClientService>();
                    var result = generalService.SaveMultiNews(title1, title2);
                    unitOfWork.Comit();
                    return result;

                }
            }
            catch (Exception ex)
            {
                //Todo log file
                return ex.Message;
            }
        }

        [OperationContract]
        public IEnumerable<CONews> UpdateMultiNews()
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBClientContext>())
                {

                    var generalService = unitOfWork.GetService<BusinessClientService>();
                    var result = generalService.UpdateMultiNews();
                    unitOfWork.Comit();
                    return result;

                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        [OperationContract]
        public COUsers SaveUser(COUsers user)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBClientContext>())
                {

                    var generalService = unitOfWork.GetService<BusinessClientService>();
                    generalService.SaveUser(user);
                    unitOfWork.Comit();
                    return user;
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        [OperationContract]
        public void DeletedUser(Guid userGuid)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBClientContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessClientService>();
                    generalService.DeleteUser(userGuid);
                    unitOfWork.Comit();
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        #region COGroupCustomerSupplier
        [OperationContract]
        public COGroupCustomerSupplier SaveGroupCustomerSupplier(COGroupCustomerSupplier info)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBClientContext>())
                {

                    var generalService = unitOfWork.GetService<BusinessClientService>();
                    generalService.SaveGroupCustomerSupplier(info);
                    unitOfWork.Comit();
                    return info;
                }
            }
            catch (Exception ex)
            {
                if (ex is InvalidEntityException)
                    throw ex;

                //Todo log file
                throw ex;
            }
        }

        [OperationContract]
        public void DeleteGroupCustomerSupplier(Guid userGuid)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBClientContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessClientService>();
                    generalService.DeleteGroupCustomerSupplier(userGuid);
                    unitOfWork.Comit();
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        [OperationContract]
        public void DeleteGroupCustomerSuppliers(IEnumerable<COGroupCustomerSupplier> entities)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBClientContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessClientService>();
                    generalService.DeleteGroupCustomerSuppliers(entities);
                    unitOfWork.Comit();
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }



        [OperationContract]
        public List<COGroupCustomerSupplier> GetAllCOGroupCustomerSupplier(string discriminator)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBClientContext>(false))
                {
                    var generalService = unitOfWork.GetService<BusinessClientService>();

                    return generalService.GetAllGroupCustomerSupplier(discriminator);
                }
            }
            catch (Exception ex)
            {
                //Todo Log
                throw ex;
            }
        }

        [OperationContract]
        public COGroupCustomerSupplier GetByIdCOGroupCustomerSupplier(string discriminator, int ID)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBClientContext>(false))
                {
                    var generalService = unitOfWork.GetService<BusinessClientService>();

                    return generalService.GetByIDGroupCustomerSupplier(discriminator,ID);
                }
            }
            catch (Exception ex)
            {
                //Todo Log
                throw ex;
            }
        }
        #endregion

    }
}
