﻿using Chuang.Back.Base;
using Interface;
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
    public class MachineTypeController : ApiBaseController
    {
        private static IBase<MachineTypeModel> _IBase
        {
            get
            {
                return new MachineTypeService();
            }
        }

        public ResultObj<List<MachineTypeModel>> GetData(string typeName = "", string typeType = "", int pageIndex = 1, int pageSize = 10)
        {
            // IProduct service = new ProductService();
            //List<ProductModel> products = service.GetAllProducts();

            MachineTypeModel machineTypeInfo = new MachineTypeModel();
            machineTypeInfo.TypeName = typeName;
            machineTypeInfo.TypeType = typeType;
            machineTypeInfo.PageIndex = pageIndex;
            machineTypeInfo.PageSize = pageSize;
            var users = _IBase.GetAll(machineTypeInfo);
            int totalcount = _IBase.GetCount(machineTypeInfo);

            var pagination = new Pagination { PageSize = pageSize, PageIndex = pageIndex, StartIndex = 0, TotalRows = totalcount, TotalPage = 0 };
            return Content(users, pagination);
        }

        public ResultObj<int> PostData(MachineTypeModel machineTypeInfo)
        {
            return Content(_IBase.PostData(machineTypeInfo));
        }

        public ResultObj<int> PutData(MachineTypeModel machineTypeInfo)
        {
            return Content(_IBase.UpdateData(machineTypeInfo));
        }

        public ResultObj<int> DeleteData(string idList)
        {
            return Content(_IBase.DeleteData(idList));
        }

        
    }
}
