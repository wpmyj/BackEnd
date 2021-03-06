﻿using Interface;
using Model.Customer;
using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Service
{
    public class CustomerService:AbstractService, IBase<CustomerModel>
    {

        public List<CustomerModel> GetAll(CustomerModel customerInfo)
        {
            string userStatus = HttpContextHandler.GetHeaderObj("Sts").ToString();
            var userClientId = HttpContextHandler.GetHeaderObj("UserClientId").ToString();
            var dics = new Dictionary<string, object>();

            dics.Add("ClientName", customerInfo.ClientName + "%");
            List<CustomerModel> result = null;
            if (userStatus == "100" || userStatus == "99")
            {
                dics.Add("ClientId", "self");
            }
            else
            {
                dics.Add("ClientId", userClientId);
               
            }
            if (customerInfo.PageIndex == 1)
            {
                dics.Add("PageIndex", customerInfo.PageIndex - 1);
                dics.Add("PageSize", customerInfo.PageSize);
            }

            result = GenerateDal.Load<CustomerModel>(CommonSqlKey.GetCustomer, dics);
            CustomerModel curItem = new CustomerModel();
            curItem.Id = userClientId;
            var finalResult = LoopToAppendChildren(result, curItem);


            return finalResult;
        }

        private int keyI = 0;
        private int keyJ = 0;
        private List<CustomerModel> LoopToAppendChildren(List<CustomerModel> all, CustomerModel curItem)
        {
            var subItems = all.Where(ee => ee.ClientFatherId == curItem.Id).ToList();
            if (subItems.Count > 0)
            {
                curItem.children = new List<CustomerModel>();
                curItem.children.AddRange(subItems);
            }
           
            foreach (var subItem in subItems)
            {
                
                subItem.key = keyI.ToString() + keyJ.ToString();
                keyJ++;
                LoopToAppendChildren(all, subItem);
            }
            keyI++;
            return subItems;
        }


        public int GetCount(CustomerModel customerInfo)
        {
            var result = 0;
            string userStatus = HttpContextHandler.GetHeaderObj("Sts").ToString();
            var userClientId = HttpContextHandler.GetHeaderObj("UserClientId").ToString();

            var dics = new Dictionary<string, object>();

            dics.Add("ClientName", customerInfo.ClientName + "%");
            if (userStatus == "100" || userStatus == "99")
            {
                dics.Add("ClientId", "self");
            }
            else
            {
                dics.Add("ClientId", userClientId);

            }

            result = GenerateDal.CountByDictionary<CustomerModel>(CommonSqlKey.GetCustomerCount, dics);

            return result;
        }


        /// <summary>
        /// 新增/修改会员信息
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public int PostData(CustomerModel customerInfo)
        {
            int result;
            string userClientId = HttpContextHandler.GetHeaderObj("UserClientId").ToString();
            string userAccount = HttpContextHandler.GetHeaderObj("UserAccount").ToString();
            if(!string.IsNullOrEmpty(userClientId))
            {
                customerInfo.ClientFatherId = userClientId;
            }

            if (!string.IsNullOrEmpty(userAccount))
            {
                customerInfo.Creator = userAccount;
            }

            customerInfo.Id = Guid.NewGuid().ToString();

            result = GenerateDal.Create(customerInfo);




            return result;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <returns></returns>
        public int DeleteData(string id)
        {
            string userClientId = HttpContextHandler.GetHeaderObj("UserClientId").ToString();
            try
            {
                
                GenerateDal.BeginTransaction();
                // 删除有子客户的客户 将子客户父级更新为删除对象的父级
                string fatherId = string.Empty;
                var fatherClientInfo = GenerateDal.Get<CustomerModel>(id);
                if (fatherClientInfo != null)
                {
                    fatherId = fatherClientInfo.ClientFatherId;
                }
                CustomerModel customerInfo = new CustomerModel();
                customerInfo.Id = id;
                int delResult = GenerateDal.Delete<CustomerModel>(CommonSqlKey.DeleteCustomer, customerInfo);
                if (delResult > 0)
                {
                    CustomerModel updInfo = new CustomerModel();
                    updInfo.ClientFatherId = fatherId;
                    updInfo.Id = id;
                    string userAccount = HttpContextHandler.GetHeaderObj("UserAccount").ToString();
                    if (!string.IsNullOrEmpty(userAccount))
                    {
                        updInfo.Updater = userAccount;
                    }
                    GenerateDal.Update(CommonSqlKey.UpdateChildCustomer, updInfo);
                }
                GenerateDal.CommitTransaction();
                return 1;
            }
            catch (Exception ee)
            {
                GenerateDal.RollBack();
                return 0;
            }
           
        }

        public int UpdateData(CustomerModel customerInfo)
        {
            string userAccount = HttpContextHandler.GetHeaderObj("UserAccount").ToString();
            if (!string.IsNullOrEmpty(userAccount))
            {
                customerInfo.Updater = userAccount;
            }
            return GenerateDal.Update(CommonSqlKey.UpdateCustomer, customerInfo);
        }
    }
}
