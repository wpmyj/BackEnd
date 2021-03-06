﻿using Model.Common;
using Model.Sys;
using Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface ICommon
    {
        [Remark("取数据列表", ParmsNote = "当前页,每页显示行数", ReturnNote = "返回返页列表")]
        List<MenuModel> GetMenus(string userAccount);

        [Remark("取数据列表", ParmsNote = "当前页,每页显示行数", ReturnNote = "返回返页列表")]
        UserModel PostUser(UserModel userInfo);

        [Remark("取字典方法", ParmsNote = "字典对应的key值", ReturnNote = "实体列表")]
        List<DicModel> GetDic(string id);


        [Remark("取客户等级方法", ParmsNote = "字典对应的key值", ReturnNote = "实体列表")]
        List<DicModel> GetRank(string id);

        [Remark("取客户当字典", ParmsNote = "", ReturnNote = "字典实体列表")]
        List<CommonDic> GetClientDic();

        [Remark("取权限模板当字典", ParmsNote = "", ReturnNote = "字典实体列表")]
        List<CommonDic> GetAuthDic();

        [Remark("取机型模板当字典", ParmsNote = "", ReturnNote = "字典实体列表")]
        List<CommonDic> GetMachineTypeDic();

        [Remark("根据客户ID取他的用户们", ParmsNote = "客户ID", ReturnNote = "字典实体列表")]
        List<CommonDic> GetUserByClientId(string id);

        [Remark("取机器做字典", ParmsNote = "", ReturnNote = "字典实体列表")]
        List<CommonDic> GetMachineDic();

        [Remark("取图片做字典", ParmsNote = "", ReturnNote = "字典实体列表")]
        List<CommonDic> GetPictureDic();

        [Remark("取商品做字典", ParmsNote = "", ReturnNote = "字典实体列表")]
        List<CommonDic> GetProductDic();

        [Remark("取货柜做字典", ParmsNote = "", ReturnNote = "字典实体列表")]
        List<CommonDic> GetCabinetDic();

        [Remark("修改密码", ParmsNote = "用户实体", ReturnNote = "int")]
        int UpdatePassword(UserModel userInfo);

    }
}
