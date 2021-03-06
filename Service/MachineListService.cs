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

    public class MachineListService : AbstractService, IBase<MachineListModel>
    {

        public List<MachineListModel> GetAll(MachineListModel machineListInfo)
        {
            string userClientId = HttpContextHandler.GetHeaderObj("UserClientId").ToString();

            var conditions = new List<Condition>();
            if (!string.IsNullOrEmpty(machineListInfo.DeviceId))
            {
                conditions.Add(new Condition{
                    LeftBrace = " AND ",
                    ParamName = "DeviceId",
                    DbColumnName = "a.device_id",
                    ParamValue = "%" + machineListInfo.DeviceId + "%",
                    Operation = ConditionOperate.Like,
                    RightBrace = "",
                    Logic = ""
                });
            }

            if (!string.IsNullOrEmpty(machineListInfo.ClientText))
            {
                conditions.Add(new Condition
                {
                    LeftBrace = " AND ",
                    ParamName = "ClientName",
                    DbColumnName = "b.client_name",
                    ParamValue = "%" + machineListInfo.ClientText + "%",
                    Operation = ConditionOperate.Like,
                    RightBrace = "",
                    Logic = ""
                });
            }

            if (!string.IsNullOrEmpty(machineListInfo.UserAccount))
            {
                conditions.Add(new Condition
                {
                    LeftBrace = " AND ",
                    ParamName = "UserAccount",
                    DbColumnName = "a.usr_account",
                    ParamValue = "%" + machineListInfo.UserAccount + "%",
                    Operation = ConditionOperate.Like,
                    RightBrace = "",
                    Logic = ""
                });
            }

            if (!string.IsNullOrEmpty(machineListInfo.TypeId))
            {
                conditions.Add(new Condition
                {
                    LeftBrace = " AND ",
                    ParamName = "TypeId",
                    DbColumnName = "a.type_id",
                    ParamValue = machineListInfo.TypeId,
                    Operation = ConditionOperate.Equal,
                    RightBrace = "",
                    Logic = ""
                });
            }

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
            
            conditions.AddRange(CreatePaginConditions(machineListInfo.PageIndex, machineListInfo.PageSize));

            return GenerateDal.LoadByConditions<MachineListModel>(CommonSqlKey.GetMachineList, conditions);
        }


        public int GetCount(MachineListModel machineListInfo)
        {
            var result = 0;

            string userClientId = HttpContextHandler.GetHeaderObj("UserClientId").ToString();

            var conditions = new List<Condition>();
            if (!string.IsNullOrEmpty(machineListInfo.DeviceId))
            {
                conditions.Add(new Condition
                {
                    LeftBrace = " AND ",
                    ParamName = "DeviceId",
                    DbColumnName = "a.device_id",
                    ParamValue = "%"+machineListInfo.DeviceId + "%",
                    Operation = ConditionOperate.Like,
                    RightBrace = "",
                    Logic = ""
                });
            }

            if (!string.IsNullOrEmpty(machineListInfo.ClientText))
            {
                conditions.Add(new Condition
                {
                    LeftBrace = " AND ",
                    ParamName = "ClientName",
                    DbColumnName = "b.client_name",
                    ParamValue = "%" + machineListInfo.ClientText+"%",
                    Operation = ConditionOperate.Like,
                    RightBrace = "",
                    Logic = ""
                });
            }

            if (!string.IsNullOrEmpty(machineListInfo.UserAccount))
            {
                conditions.Add(new Condition
                {
                    LeftBrace = " AND ",
                    ParamName = "UserAccount",
                    DbColumnName = "a.usr_account",
                    ParamValue = "%" + machineListInfo.UserAccount+"%",
                    Operation = ConditionOperate.Like,
                    RightBrace = "",
                    Logic = ""
                });
            }

            if (!string.IsNullOrEmpty(machineListInfo.TypeId))
            {
                conditions.Add(new Condition
                {
                    LeftBrace = " AND ",
                    ParamName = "TypeId",
                    DbColumnName = "a.type_id",
                    ParamValue = machineListInfo.TypeId,
                    Operation = ConditionOperate.Equal,
                    RightBrace = "",
                    Logic = ""
                });
            }

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



            result = GenerateDal.CountByConditions(CommonSqlKey.GetMachineListCount, conditions);

            return result;
        }


        /// <summary>
        /// 新增/修改会员信息
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public int PostData(MachineListModel machineListInfo)
        {
            int result;

            string userAccount = HttpContextHandler.GetHeaderObj("UserAccount").ToString();
            machineListInfo.MachineId = machineListInfo.DeviceId;
            machineListInfo.CreateDate = DateTime.Now;
            machineListInfo.Creator = userAccount;
            result = GenerateDal.Create(machineListInfo);




            return result;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <returns></returns>
        public int DeleteData(string id)
        {
           
            try
            {
                GenerateDal.BeginTransaction();

                MachineListModel machineListInfo = new MachineListModel();
                machineListInfo.MachineId = id;
                GenerateDal.Delete<MachineListModel>(CommonSqlKey.DeleteMachineList, machineListInfo);
                MachineConfigService mcService = new MachineConfigService();
                mcService.DeleteData(id);
                GenerateDal.CommitTransaction();

                return 1;
            }
            catch (Exception e)
            {
                GenerateDal.RollBack();
                return 0;
            }
        }

        public int UpdateData(MachineListModel machineListInfo)
        {
            machineListInfo.UpdateDate = DateTime.Now;
             string userAccount = HttpContextHandler.GetHeaderObj("UserAccount").ToString();
             machineListInfo.Updater = userAccount;
            return GenerateDal.Update(CommonSqlKey.UpdateMachineList, machineListInfo);
        }
       
    }
}
