﻿using Model.Machine;
using Model.Pay;
using Model.Sys;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Interface
{
    public interface IMachine
    {
        [Remark("根据机器取商品", ParmsNote = "", ReturnNote = "返回返页列表")]
        List<ProductForMachineModel> GetProductByMachine(ProductForMachineModel machineInfo);

        [Remark("根据机器取商品的总共数据", ParmsNote = "", ReturnNote = "返回返页列表")]
        int GetCount(ProductForMachineModel machineInfo);

        [Remark("微信支付结果回调", ParmsNote = "机器和货道和商品信息", ReturnNote = "void")]
        void PostPayResultW(KeyJsonModel keyJsonModel, string tradeNo);

        [Remark("支付宝支付结果回调", ParmsNote = "商品信息列表", ReturnNote = "void")]
        int PostPayResultA(KeyJsonModel keyJsonModel, string tradeNo);

        [Remark("判断是否已存在该单", ParmsNote = "商户交易号", ReturnNote = "int")]
        int GetCountByTradeNo(string tradeNo);

        [Remark("上报出货结果", ParmsNote = "商户交易号", ReturnNote = "int")]
        int PutPayResult(KeyJsonModel keyJsonInfo);

        [Remark("一键补货", ParmsNote = "机器编号", ReturnNote = "int")]
        int GetFullfilGood(string machineId);

        [Remark("按照货道补货", ParmsNote = "货道实体", ReturnNote = "int")]
        int GetFullfilGoodByTunnel(KeyJsonModel keyJsonModel);

        [Remark("心跳包", ParmsNote = "机器编号", ReturnNote = "DataTable")]
        DataTable GetBeepHeart(string machineId);

        [Remark("机器上报下行处理结果", ParmsNote = "机器编号,标识类型", ReturnNote = "int")]
        int GetHandleResult(string machineId, string machineStatus);

         [Remark("向机器下行价格信息", ParmsNote = "机器编号,起始数据,取数据长度", ReturnNote = "DataTable")]
        DataTable GetToMachinePrice(string machineId, int startNo, int len);

         [Remark("向机器下行当前库存信息", ParmsNote = "机器编号,起始数据,取数据长度", ReturnNote = "DataTable")]
         DataTable GetToMachineStock(string machineId, int startNo, int len);

        [Remark("机器端设置最大库存和价格", ParmsNote = "价格和库存列表，机器编号", ReturnNote = "int")]
        int PostMaxStockAndPrice(List<PriceAndMaxStockModel> lstPriceAndStock, string machineId);

        [Remark("机器端取机器设置", ParmsNote = "机器编号", ReturnNote = "DataTable")]
        DataTable GetMachineSetting(string machineId);
    }
}
