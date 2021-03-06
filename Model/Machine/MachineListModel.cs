﻿using Model.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Machine
{
     [Table("table_machine")]
    public class MachineListModel: BaseModel
    {
        [Column(Name = "machine_id")]
        public string MachineId
        {
            get;
            set;
        }

         [Column(Name = "device_id")]
         public string DeviceId
         {
             get;
             set;
         }


         [Column(Name = "type_id")]
         public string TypeId
         {
             get;
             set;
         }

         public string TypeText
         {
             get;
             set;
         }

         [Column(Name = "client_id")]
         public string ClientId
         {
             get;
             set;
         }

         public string ClientText
         {
             get;
             set;
         }

         [Column(Name = "usr_account")]
         public string UserAccount
         {
             get;
             set;
         }

         public string UserAccountName
         {
             get;
             set;
         }

         [Column(Name = "start_date")]
         public DateTime StartDate
         {
             get;
             set;
         }

         [Column(Name = "stop_date")]
         public DateTime StopDate
         {
             get;
             set;
         }

         [Column(Name = "stop_reason")]
         public DateTime StopReason
         {
             get;
             set;
         }


         [Column(Name = "updater")]
         public string Updater
         {
             get;
             set;
         }

         [Column(Name = "update_date")]
         public DateTime UpdateDate
         {
             get;
             set;
         }

         [Column(Name = "latest_date")]
         public DateTime LatestDate
         {
             get;
             set;
         }

         public string LatestOnline
         {
             get;
             set;
         }
    }
}
