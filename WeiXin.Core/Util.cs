﻿
/*
 * 程序中文名称: 微联盟
 * 
 * 程序英文名称: WeiMeng
 * 
 * 程序版本: 1.0.X
 * 
 * 程序作者: 王兵 (商业合作请联系：refinedking@gmail.com)
 * 
 * 官方网站: http://weixin.cqzuxia.com/
 * 
 * 
 */
/*****************************************************
* 本类库的核心系 AderTemplates
* (C) Andrew Deren 2004
* http://www.adersoftware.com
*****************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WeiXin.Core
{
    public static class Util
    {
        static object syncObject = new object();

        static Regex regExVarName;

        public static bool ToBool(object obj)
        {
            if (obj is bool)
                return (bool)obj;
            else if (obj is string)
            {
                string str = (string)obj;
                if (string.Compare(str, "true", true) == 0)
                    return true;
                else if (string.Compare(str, "yes", true) == 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public static bool IsValidVariableName(string name)
        {
            return RegExVarName.IsMatch(name);
        }

        private static Regex RegExVarName
        {
            get
            {
                if ((regExVarName == null))
                {
                    System.Threading.Monitor.Enter(syncObject);
                    if (regExVarName == null)
                    {
                        try
                        {
                            regExVarName = new Regex("^[a-zA-Z_][a-zA-Z0-9_]*$", RegexOptions.Compiled);
                        }
                        finally
                        {
                            System.Threading.Monitor.Exit(syncObject);
                        }
                    }
                }

                return regExVarName;
            }
        }
    }
}
