
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
using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXin.Core
{
    public interface ITemplateHandler
    {
        /// <summary>
        /// this method will be called before any processing
        /// </summary>
        /// <param name="manager">manager doing the execution</param>
        void BeforeProcess(TemplateManager manager);

        /// <summary>
        /// this method will be called after all processing is done
        /// </summary>
        /// <param name="manager">manager doing the execution</param>
        void AfterProcess(TemplateManager manager);
    }
}
