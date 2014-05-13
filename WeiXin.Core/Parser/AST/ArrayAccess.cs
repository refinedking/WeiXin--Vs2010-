
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

#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace WeiXin.Core.Parser.AST
{
	public class ArrayAccess : Expression
	{
		Expression exp;
		Expression index;

		public ArrayAccess(int line, int col, Expression exp, Expression index)
			:base(line, col)
		{
			this.exp = exp;
			this.index = index;
		}

		public Expression Exp
		{
			get { return this.exp; }
		}

		public Expression Index
		{
			get { return this.index; }
		}

	}
}
