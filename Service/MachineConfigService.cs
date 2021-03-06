﻿using Interface;
using Model.Machine;
using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Service
{
    public class MachineConfigService : AbstractService, IBase<MachineConfigModel>
    {

        public List<MachineConfigModel> GetAll(MachineConfigModel machineConfigInfo)
        {
            string userClientId = HttpContextHandler.GetHeaderObj("UserClientId").ToString();

            var conditions = new List<Condition>();
          
            if (!string.IsNullOrEmpty(machineConfigInfo.DeviceId))
            {
                conditions.Add(new Condition
                {
                    LeftBrace = " AND ",
                    ParamName = "DeviceId",
                    DbColumnName = "b.device_id",
                    ParamValue = "%" + machineConfigInfo.DeviceId + "%",
                    Operation = ConditionOperate.Like,
                    RightBrace = "",
                    Logic = ""
                });
            }
            /*
          if (!string.IsNullOrEmpty(machineConfigInfo.ClientText))
          {
              conditions.Add(new Condition
              {
                  LeftBrace = " AND ",
                  ParamName = "ClientName",
                  DbColumnName = "b.client_name",
                  ParamValue = "%" + machineConfigInfo.ClientText + "%",
                  Operation = ConditionOperate.Like,
                  RightBrace = "",
                  Logic = ""
              });
          }

          if (!string.IsNullOrEmpty(machineConfigInfo.UserAccount))
          {
              conditions.Add(new Condition
              {
                  LeftBrace = " AND ",
                  ParamName = "UserAccount",
                  DbColumnName = "a.usr_account",
                  ParamValue = "%" + machineConfigInfo.UserAccount + "%",
                  Operation = ConditionOperate.Like,
                  RightBrace = "",
                  Logic = ""
              });
          }

          if (!string.IsNullOrEmpty(machineConfigInfo.TypeId))
          {
              conditions.Add(new Condition
              {
                  LeftBrace = " AND ",
                  ParamName = "TypeId",
                  DbColumnName = "a.type_id",
                  ParamValue = machineConfigInfo.TypeId,
                  Operation = ConditionOperate.Equal,
                  RightBrace = "",
                  Logic = ""
              });
          }
              */
            conditions.Add(new Condition
          {
              LeftBrace = "",
              ParamName = "ClientId",
              DbColumnName = "",
              ParamValue = userClientId,
              Operation = ConditionOperate.None,
              RightBrace = "",
              Logic = ""
          });
        
            conditions.AddRange(CreatePaginConditions(machineConfigInfo.PageIndex, machineConfigInfo.PageSize));

            return GenerateDal.LoadByConditions<MachineConfigModel>(CommonSqlKey.GetMachineConfig, conditions);
        }


        public int GetCount(MachineConfigModel machineConfigInfo)
        {
            var result = 0;

            string userClientId = HttpContextHandler.GetHeaderObj("UserClientId").ToString();

            var conditions = new List<Condition>();
           
            if (!string.IsNullOrEmpty(machineConfigInfo.DeviceId))
            {
                conditions.Add(new Condition
                {
                    LeftBrace = " AND ",
                    ParamName = "DeviceId",
                    DbColumnName = "a.device_id",
                    ParamValue = "%" + machineConfigInfo.DeviceId + "%",
                    Operation = ConditionOperate.Like,
                    RightBrace = "",
                    Logic = ""
                });
            }
            /*
           if (!string.IsNullOrEmpty(machineConfigInfo.ClientText))
           {
               conditions.Add(new Condition
               {
                   LeftBrace = " AND ",
                   ParamName = "ClientName",
                   DbColumnName = "b.client_name",
                   ParamValue = "%" + machineConfigInfo.ClientText + "%",
                   Operation = ConditionOperate.Like,
                   RightBrace = "",
                   Logic = ""
               });
           }

           if (!string.IsNullOrEmpty(machineConfigInfo.UserAccount))
           {
               conditions.Add(new Condition
               {
                   LeftBrace = " AND ",
                   ParamName = "UserAccount",
                   DbColumnName = "a.usr_account",
                   ParamValue = "%" + machineConfigInfo.UserAccount + "%",
                   Operation = ConditionOperate.Like,
                   RightBrace = "",
                   Logic = ""
               });
           }

           if (!string.IsNullOrEmpty(machineConfigInfo.TypeId))
           {
               conditions.Add(new Condition
               {
                   LeftBrace = " AND ",
                   ParamName = "TypeId",
                   DbColumnName = "a.type_id",
                   ParamValue = machineConfigInfo.TypeId,
                   Operation = ConditionOperate.Equal,
                   RightBrace = "",
                   Logic = ""
               });
           }
             */
            conditions.Add(new Condition
           {
               LeftBrace = "",
               ParamName = "ClientId",
               DbColumnName = "",
               ParamValue = userClientId,
               Operation = ConditionOperate.None,
               RightBrace = "",
               Logic = ""
           });

          

            result = GenerateDal.CountByConditions(CommonSqlKey.GetMachineConfigCount, conditions);

            return result;
        }


        /// <summary>
        /// 新增/修改会员信息
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public int PostData(MachineConfigModel machineConfigInfo)
        {
            try
            {
                GenerateDal.BeginTransaction();

                if (!string.IsNullOrEmpty(machineConfigInfo.MachineId))
                {
                    DeleteData(machineConfigInfo.MachineId);
                }
                string userAccount = HttpContextHandler.GetHeaderObj("UserAccount").ToString();
                machineConfigInfo.UpdateDate = DateTime.Now;
                machineConfigInfo.Updater = userAccount;
                GenerateDal.Create(machineConfigInfo);
                //需要插入机器下行表中待机器取
                MachineService mc = new MachineService();
                mc.PostToMachine(machineConfigInfo.MachineId, "t");
                GenerateDal.CommitTransaction();

                return 1;
            }
            catch (Exception e)
            {
                GenerateDal.RollBack();
                return 0;
            }
           

        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <returns></returns>
        public int DeleteData(string id)
        {
            MachineConfigModel machineConfigInfo = new MachineConfigModel();
            machineConfigInfo.MachineId = id;
            return GenerateDal.Delete<MachineConfigModel>(CommonSqlKey.DeleteMachineConfig, machineConfigInfo);
        }

        public int UpdateData(MachineConfigModel machineConfigInfo)
        {
            machineConfigInfo.UpdateDate = DateTime.Now;
            string userAccount = HttpContextHandler.GetHeaderObj("UserAccount").ToString();
            machineConfigInfo.Updater = userAccount;
            return GenerateDal.Update(CommonSqlKey.UpdateMachineConfig, machineConfigInfo);
        }
    }
}
