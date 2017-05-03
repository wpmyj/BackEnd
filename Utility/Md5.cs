﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    public class Md5
    {
         public static string md5(string str, int code)  //code 16 或 32 
        {
                if (code == 16) //16位MD5加密（取32位加密的9~25字符） 
                {
                    return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
                }

                if (code == 32) //32位加密 
                {
                    return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
                }

                return "00000000000000000000000000000000";
            }
        }
    
}
