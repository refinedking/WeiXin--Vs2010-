
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

namespace WeiXin.Core
{
    public class TemplateRuntimeException : Exception
    {
        int line;
        int col;

        public TemplateRuntimeException(string msg, int line, int col)
            : base(msg)
        {
            this.line = line;
            this.col = col;
        }

        public TemplateRuntimeException(string msg, Exception innerException, int line, int col)
            : base(msg, innerException)
        {
            this.line = line;
            this.col = col;
        }

        public int Col
        {
            get { return this.col; }
        }

        public int Line
        {
            get { return this.line; }
        }
    }
}
