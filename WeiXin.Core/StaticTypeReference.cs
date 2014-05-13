
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

namespace WeiXin.Core
{
    /// <summary>
    /// StaticTypeReference is used by TemplateManager to hold references to types.
    /// When invoking methods, or accessing properties of this object, it will actually
    /// do static methods of the type
    /// </summary>
    class StaticTypeReference
    {
        readonly Type type;

        public StaticTypeReference(Type type)
        {
            this.type = type;
        }

        public Type Type
        {
            get { return type; }
        }
    }
}
