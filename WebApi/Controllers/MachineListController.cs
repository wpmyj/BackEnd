﻿using Chuang.Back.Base;
using Interface;
using Model.Common;
using Model.Machine;
using Model.Sys;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Chuang.Back.Controllers
{
    public class MachineListController : ApiBaseController
    {
        private static IBase<MachineListModel> _IBase
        {
            get
            {
                return new MachineListService();
            }
        }

        public ResultObj<List<MachineListModel>> GetData(string deviceId = "", string clinetName = "", string userAccount="", string typeId="", int pageIndex = 1, int pageSize = 10)
        {
            // IProduct service = new ProductService();
            //List<ProductModel> products = service.GetAllProducts();

            MachineListModel machineListInfo = new MachineListModel();
            machineListInfo.DeviceId = deviceId;
            machineListInfo.ClientText = clinetName;
            machineListInfo.UserAccount = userAccount;
            machineListInfo.TypeId = typeId;
            machineListInfo.PageIndex = pageIndex;
            machineListInfo.PageSize = pageSize;
            var users = _IBase.GetAll(machineListInfo);
            int totalcount = _IBase.GetCount(machineListInfo);

            var pagination = new Pagination { PageSize = pageSize, PageIndex = pageIndex, StartIndex = 0, TotalRows = totalcount, TotalPage = 0 };
            return Content(users, pagination);
        }

        public ResultObj<int> PostData(MachineListModel machineListInfo)
        {
            return Content(_IBase.PostData(machineListInfo));
        }

        public ResultObj<int> PutData(MachineListModel machineListInfo)
        {
            return Content(_IBase.UpdateData(machineListInfo));
        }

        public ResultObj<int> DeleteData(string idList)
        {
            return Content(_IBase.DeleteData(idList));
        }

        //取机型字典
        public ResultObj<List<CommonDic>> GetMachineTypeDic()
        {
            ICommon commonService = new CommonService();
            return Content(commonService.GetMachineTypeDic());
        }

    }
}
