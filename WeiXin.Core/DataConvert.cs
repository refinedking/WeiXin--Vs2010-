
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
/*-------------------------------------------------------
// Copyright (C) 2011 重庆足下科技有限公司 版权所有。 
// 文件名：  DataConvert.cs
// 功能描述：OA数据转换
// 创建标识：刘新奇  2012-05-06
// 修改标识：见每个方法前面
// 修改描述：见每个方法前面
//------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;

namespace WeiXin.Core
{
    /// <summary>
    /// 数据转换帮助类
    /// </summary>
    public class DataConvert
    {
        #region 泛型与DataSet互转

        #region 转换成DataSet
        /// <summary> 
        /// 集合装换DataSet 
        /// </summary> 
        /// <param name="list">集合</param>
        /// <returns></returns>   
        public static DataSet ToDataSet(IList p_List)
        {
            DataSet result = new DataSet();
            DataTable _DataTable = new DataTable();
            if (p_List.Count > 0)
            {
                PropertyInfo[] propertys = p_List[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    _DataTable.Columns.Add(pi.Name, pi.PropertyType);
                }
                for (int i = 0; i < p_List.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(p_List[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    _DataTable.LoadDataRow(array, true);
                }
            }
            result.Tables.Add(_DataTable);
            return result;
        }
        /// <summary>
        /// 泛型集合转换DataSet
        /// </summary>   
        /// <typeparam name="T"></typeparam>  
        /// <param name="list">泛型集合</param>  
        /// <returns></returns>   
        public static DataSet ToDataSet<T>(IList<T> list)
        {
            return ToDataSet<T>(list, null);
        }
        /// <summary> 
        /// 泛型集合转换DataSet 
        /// </summary> 
        /// <typeparam name="T"></typeparam>
        /// <param name="p_List">泛型集合</param>
        /// <param name="p_PropertyName">待转换属性名数组</param>     
        /// <returns></returns> 
        public static DataSet ToDataSet<T>(IList<T> p_List, params string[] p_PropertyName)
        {
            List<string> propertyNameList = new List<string>();
            if (p_PropertyName != null)
                propertyNameList.AddRange(p_PropertyName);
            DataSet result = new DataSet();
            DataTable _DataTable = new DataTable();
            if (p_List.Count > 0)
            {
                PropertyInfo[] propertys = p_List[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    if (propertyNameList.Count == 0)
                    {
                        // 没有指定属性的情况下全部属性都要转换  
                        _DataTable.Columns.Add(pi.Name, pi.PropertyType);
                    }
                    else
                    {
                        if (propertyNameList.Contains(pi.Name))
                            _DataTable.Columns.Add(pi.Name, pi.PropertyType);
                    }
                }
                for (int i = 0; i < p_List.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        if (propertyNameList.Count == 0)
                        {
                            object obj = pi.GetValue(p_List[i], null);
                            tempList.Add(obj);
                        }
                        else
                        {
                            if (propertyNameList.Contains(pi.Name))
                            {
                                object obj = pi.GetValue(p_List[i], null);
                                tempList.Add(obj);
                            }

                        }
                    }
                    object[] array = tempList.ToArray();
                    _DataTable.LoadDataRow(array, true);
                }
            }
            result.Tables.Add(_DataTable);
            return result;
        }
        #endregion

        #region 转换成泛型
        /// <summary>
        /// DataSet装换为泛型集合     
        /// </summary>     
        /// <typeparam name="T"></typeparam>    
        /// <param name="p_DataSet">DataSet</param>    
        /// <param name="p_TableIndex">待转换数据表索引</param>    
        /// <returns></returns>     
        public static IList<T> DataSetToIList<T>(DataSet p_DataSet, int p_TableIndex)
        {

            if (p_DataSet == null || p_DataSet.Tables.Count < 0)
                return null;
            if (p_TableIndex > p_DataSet.Tables.Count - 1)
                return null;
            if (p_TableIndex < 0)
                p_TableIndex = 0;
            DataTable p_Data = p_DataSet.Tables[p_TableIndex];
            // 返回值初始化
            IList<T> result = new List<T>();
            for (int j = 0; j < p_Data.Rows.Count; j++)
            {
                T _t = (T)Activator.CreateInstance(typeof(T));
                PropertyInfo[] propertys = _t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    for (int i = 0; i < p_Data.Columns.Count; i++)
                    {
                        // 属性与字段名称一致的进行赋值,且属性类型不是OA系统自定义对象
                        if (pi.Name.ToUpper().Equals(p_Data.Columns[i].ColumnName.ToUpper()))
                        {
                            // 数据库NULL值单独处理
                            if (p_Data.Rows[j][i] != DBNull.Value)
                                pi.SetValue(_t, p_Data.Rows[j][i], null);
                            else
                                pi.SetValue(_t, null, null);
                            break;
                        }
                    }
                }
                result.Add(_t);
            }
            return result;
        }

        /// <summary> 
        /// DataSet装换为泛型集合 
        /// </summary> 
        /// <typeparam name="T"></typeparam>
        /// <param name="p_DataSet">DataSet</param>  
        /// <param name="p_TableName">待转换数据表名称</param>   
        /// <returns></returns>   
        public static IList<T> DataSetToIList<T>(DataSet p_DataSet, string p_TableName)
        {
            int _TableIndex = 0;
            if (p_DataSet == null || p_DataSet.Tables.Count < 0)
                return null;
            if (string.IsNullOrEmpty(p_TableName))
                return null;
            for (int i = 0; i < p_DataSet.Tables.Count; i++)
            {
                // 获取Table名称在Tables集合中的索引值
                if (p_DataSet.Tables[i].TableName.Equals(p_TableName))
                {
                    _TableIndex = i;
                    break;
                }
            }
            return DataSetToIList<T>(p_DataSet, _TableIndex);
        }
        #endregion

        #region 转换成实体
        /// <summary>
        /// DataSet转换成单个实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_DataSet"></param>
        /// <param name="p_TableName"></param>
        /// <returns></returns>
        public static T DataSetToEntity<T>(DataSet p_DataSet, int p_TableIndex)
        {
            T entity = (T)Activator.CreateInstance(typeof(T));
            PropertyInfo[] propertyInfos = entity.GetType().GetProperties();
            for (int i = 0; i < p_DataSet.Tables[p_TableIndex].Rows.Count; i++)
            {
                foreach (PropertyInfo pi in propertyInfos)
                {
                    for (int j = 0; j < p_DataSet.Tables[p_TableIndex].Columns.Count; j++)
                    {
                        if (pi.Name.ToUpper().Equals(p_DataSet.Tables[p_TableIndex].Columns[j].ColumnName.ToUpper()) && pi.PropertyType.Namespace != "Teaching.Models")
                        {
                            if (p_DataSet.Tables[p_TableIndex].Rows[i][j] != DBNull.Value)
                            {
                                pi.SetValue(entity, p_DataSet.Tables[p_TableIndex].Rows[i][j], null);
                            }
                            else
                            {
                                pi.SetValue(entity, null, null);
                            }
                        }
                    }
                }

            }
            return entity;
        }
        #endregion

        #endregion

        #region 2012-05-06 刘新奇 添加功能:汉字转换成拼音
        /// <summary>
        /// 简体拼音哈希表
        /// </summary>
        private static Hashtable hashSpell;

        /// <summary>
        /// 将姓名转成拼音
        /// </summary>
        /// <param name="name">简体中文姓名</param>
        /// <returns>该名字的拼音</returns>
        public static string ToSpell(string name)
        {
            if (string.IsNullOrEmpty(name)) return "";
            //初始化Hashtable
            if (hashSpell == null)
            {
                hashSpell = new Hashtable();
                hashSpell.Add(-20319, "a");
                hashSpell.Add(-20317, "ai"); hashSpell.Add(-20304, "an"); hashSpell.Add(-20295, "ang");
                hashSpell.Add(-20292, "ao"); hashSpell.Add(-20283, "ba"); hashSpell.Add(-20265, "bai");
                hashSpell.Add(-20257, "ban"); hashSpell.Add(-20242, "bang"); hashSpell.Add(-20230, "bao");
                hashSpell.Add(-20051, "bei"); hashSpell.Add(-20036, "ben"); hashSpell.Add(-20032, "beng");
                hashSpell.Add(-20026, "bi"); hashSpell.Add(-20002, "bian"); hashSpell.Add(-19990, "biao");
                hashSpell.Add(-19986, "bie"); hashSpell.Add(-19982, "bin"); hashSpell.Add(-19976, "bing");
                hashSpell.Add(-19805, "bo"); hashSpell.Add(-19784, "bu"); hashSpell.Add(-19775, "ca");
                hashSpell.Add(-19774, "cai"); hashSpell.Add(-19763, "can"); hashSpell.Add(-19756, "cang");
                hashSpell.Add(-19751, "cao"); hashSpell.Add(-19746, "ce"); hashSpell.Add(-19741, "ceng");
                hashSpell.Add(-19739, "cha"); hashSpell.Add(-19728, "chai"); hashSpell.Add(-19725, "chan");
                hashSpell.Add(-19715, "chang"); hashSpell.Add(-19540, "chao"); hashSpell.Add(-19531, "che");
                hashSpell.Add(-19525, "chen"); hashSpell.Add(-19515, "cheng"); hashSpell.Add(-19500, "chi");
                hashSpell.Add(-19484, "chong"); hashSpell.Add(-19479, "chou"); hashSpell.Add(-19467, "chu");
                hashSpell.Add(-19289, "chuai"); hashSpell.Add(-19288, "chuan"); hashSpell.Add(-19281, "chuang");
                hashSpell.Add(-19275, "chui"); hashSpell.Add(-19270, "chun"); hashSpell.Add(-19263, "chuo");
                hashSpell.Add(-19261, "ci"); hashSpell.Add(-19249, "cong"); hashSpell.Add(-19243, "cou");
                hashSpell.Add(-19242, "cu"); hashSpell.Add(-19238, "cuan"); hashSpell.Add(-19235, "cui");
                hashSpell.Add(-19227, "cun"); hashSpell.Add(-19224, "cuo"); hashSpell.Add(-19218, "da");
                hashSpell.Add(-19212, "dai"); hashSpell.Add(-19038, "dan"); hashSpell.Add(-19023, "dang");
                hashSpell.Add(-19018, "dao"); hashSpell.Add(-19006, "de"); hashSpell.Add(-19003, "deng");
                hashSpell.Add(-18996, "di"); hashSpell.Add(-18977, "dian"); hashSpell.Add(-18961, "diao");
                hashSpell.Add(-18952, "die"); hashSpell.Add(-18783, "ding"); hashSpell.Add(-18774, "diu");
                hashSpell.Add(-18773, "dong"); hashSpell.Add(-18763, "dou"); hashSpell.Add(-18756, "du");
                hashSpell.Add(-18741, "duan"); hashSpell.Add(-18735, "dui"); hashSpell.Add(-18731, "dun");
                hashSpell.Add(-18722, "duo"); hashSpell.Add(-18710, "e"); hashSpell.Add(-18697, "en");
                hashSpell.Add(-18696, "er"); hashSpell.Add(-18526, "fa"); hashSpell.Add(-18518, "fan");
                hashSpell.Add(-18501, "fang"); hashSpell.Add(-18490, "fei"); hashSpell.Add(-18478, "fen");
                hashSpell.Add(-18463, "feng"); hashSpell.Add(-18448, "fo"); hashSpell.Add(-18447, "fou");
                hashSpell.Add(-18446, "fu"); hashSpell.Add(-18239, "ga"); hashSpell.Add(-18237, "gai");
                hashSpell.Add(-18231, "gan"); hashSpell.Add(-18220, "gang"); hashSpell.Add(-18211, "gao");
                hashSpell.Add(-18201, "ge"); hashSpell.Add(-18184, "gei"); hashSpell.Add(-18183, "gen");
                hashSpell.Add(-18181, "geng"); hashSpell.Add(-18012, "gong"); hashSpell.Add(-17997, "gou");
                hashSpell.Add(-17988, "gu"); hashSpell.Add(-17970, "gua"); hashSpell.Add(-17964, "guai");
                hashSpell.Add(-17961, "guan"); hashSpell.Add(-17950, "guang"); hashSpell.Add(-17947, "gui");
                hashSpell.Add(-17931, "gun"); hashSpell.Add(-17928, "guo"); hashSpell.Add(-17922, "ha");
                hashSpell.Add(-17759, "hai"); hashSpell.Add(-17752, "han"); hashSpell.Add(-17733, "hang");
                hashSpell.Add(-17730, "hao"); hashSpell.Add(-17721, "he"); hashSpell.Add(-17703, "hei");
                hashSpell.Add(-17701, "hen"); hashSpell.Add(-17697, "heng"); hashSpell.Add(-17692, "hong");
                hashSpell.Add(-17683, "hou"); hashSpell.Add(-17676, "hu"); hashSpell.Add(-17496, "hua");
                hashSpell.Add(-17487, "huai"); hashSpell.Add(-17482, "huan"); hashSpell.Add(-17468, "huang");
                hashSpell.Add(-17454, "hui"); hashSpell.Add(-17433, "hun"); hashSpell.Add(-17427, "huo");
                hashSpell.Add(-17417, "ji"); hashSpell.Add(-17202, "jia"); hashSpell.Add(-17185, "jian");
                hashSpell.Add(-16983, "jiang"); hashSpell.Add(-16970, "jiao"); hashSpell.Add(-16942, "jie");
                hashSpell.Add(-16915, "jin"); hashSpell.Add(-16733, "jing"); hashSpell.Add(-16708, "jiong");
                hashSpell.Add(-16706, "jiu"); hashSpell.Add(-16689, "ju"); hashSpell.Add(-16664, "juan");
                hashSpell.Add(-16657, "jue"); hashSpell.Add(-16647, "jun"); hashSpell.Add(-16474, "ka");
                hashSpell.Add(-16470, "kai"); hashSpell.Add(-16465, "kan"); hashSpell.Add(-16459, "kang");
                hashSpell.Add(-16452, "kao"); hashSpell.Add(-16448, "ke"); hashSpell.Add(-16433, "ken");
                hashSpell.Add(-16429, "keng"); hashSpell.Add(-16427, "kong"); hashSpell.Add(-16423, "kou");
                hashSpell.Add(-16419, "ku"); hashSpell.Add(-16412, "kua"); hashSpell.Add(-16407, "kuai");
                hashSpell.Add(-16403, "kuan"); hashSpell.Add(-16401, "kuang"); hashSpell.Add(-16393, "kui");
                hashSpell.Add(-16220, "kun"); hashSpell.Add(-16216, "kuo"); hashSpell.Add(-16212, "la");
                hashSpell.Add(-16205, "lai"); hashSpell.Add(-16202, "lan"); hashSpell.Add(-16187, "lang");
                hashSpell.Add(-16180, "lao"); hashSpell.Add(-16171, "le"); hashSpell.Add(-16169, "lei");
                hashSpell.Add(-16158, "leng"); hashSpell.Add(-16155, "li"); hashSpell.Add(-15959, "lia");
                hashSpell.Add(-15958, "lian"); hashSpell.Add(-15944, "liang"); hashSpell.Add(-15933, "liao");
                hashSpell.Add(-15920, "lie"); hashSpell.Add(-15915, "lin"); hashSpell.Add(-15903, "ling");
                hashSpell.Add(-15889, "liu"); hashSpell.Add(-15878, "long"); hashSpell.Add(-15707, "lou");
                hashSpell.Add(-15701, "lu"); hashSpell.Add(-15681, "lv"); hashSpell.Add(-15667, "luan");
                hashSpell.Add(-15661, "lue"); hashSpell.Add(-15659, "lun"); hashSpell.Add(-15652, "luo");
                hashSpell.Add(-15640, "ma"); hashSpell.Add(-15631, "mai"); hashSpell.Add(-15625, "man");
                hashSpell.Add(-15454, "mang"); hashSpell.Add(-15448, "mao"); hashSpell.Add(-15436, "me");
                hashSpell.Add(-15435, "mei"); hashSpell.Add(-15419, "men"); hashSpell.Add(-15416, "meng");
                hashSpell.Add(-15408, "mi"); hashSpell.Add(-15394, "mian"); hashSpell.Add(-15385, "miao");
                hashSpell.Add(-15377, "mie"); hashSpell.Add(-15375, "min"); hashSpell.Add(-15369, "ming");
                hashSpell.Add(-15363, "miu"); hashSpell.Add(-15362, "mo"); hashSpell.Add(-15183, "mou");
                hashSpell.Add(-15180, "mu"); hashSpell.Add(-15165, "na"); hashSpell.Add(-15158, "nai");
                hashSpell.Add(-15153, "nan"); hashSpell.Add(-15150, "nang"); hashSpell.Add(-15149, "nao");
                hashSpell.Add(-15144, "ne"); hashSpell.Add(-15143, "nei"); hashSpell.Add(-15141, "nen");
                hashSpell.Add(-15140, "neng"); hashSpell.Add(-15139, "ni"); hashSpell.Add(-15128, "nian");
                hashSpell.Add(-15121, "niang"); hashSpell.Add(-15119, "niao"); hashSpell.Add(-15117, "nie");
                hashSpell.Add(-15110, "nin"); hashSpell.Add(-15109, "ning"); hashSpell.Add(-14941, "niu");
                hashSpell.Add(-14937, "nong"); hashSpell.Add(-14933, "nu"); hashSpell.Add(-14930, "nv");
                hashSpell.Add(-14929, "nuan"); hashSpell.Add(-14928, "nue"); hashSpell.Add(-14926, "nuo");
                hashSpell.Add(-14922, "o"); hashSpell.Add(-14921, "ou"); hashSpell.Add(-14914, "pa");
                hashSpell.Add(-14908, "pai"); hashSpell.Add(-14902, "pan"); hashSpell.Add(-14894, "pang");
                hashSpell.Add(-14889, "pao"); hashSpell.Add(-14882, "pei"); hashSpell.Add(-14873, "pen");
                hashSpell.Add(-14871, "peng"); hashSpell.Add(-14857, "pi"); hashSpell.Add(-14678, "pian");
                hashSpell.Add(-14674, "piao"); hashSpell.Add(-14670, "pie"); hashSpell.Add(-14668, "pin");
                hashSpell.Add(-14663, "ping"); hashSpell.Add(-14654, "po"); hashSpell.Add(-14645, "pu");
                hashSpell.Add(-14630, "qi"); hashSpell.Add(-14594, "qia"); hashSpell.Add(-14429, "qian");
                hashSpell.Add(-14407, "qiang"); hashSpell.Add(-14399, "qiao"); hashSpell.Add(-14384, "qie");
                hashSpell.Add(-14379, "qin"); hashSpell.Add(-14368, "qing"); hashSpell.Add(-14355, "qiong");
                hashSpell.Add(-14353, "qiu"); hashSpell.Add(-14345, "qu"); hashSpell.Add(-14170, "quan");
                hashSpell.Add(-14159, "que"); hashSpell.Add(-14151, "qun"); hashSpell.Add(-14149, "ran");
                hashSpell.Add(-14145, "rang"); hashSpell.Add(-14140, "rao"); hashSpell.Add(-14137, "re");
                hashSpell.Add(-14135, "ren"); hashSpell.Add(-14125, "reng"); hashSpell.Add(-14123, "ri");
                hashSpell.Add(-14122, "rong"); hashSpell.Add(-14112, "rou"); hashSpell.Add(-14109, "ru");
                hashSpell.Add(-14099, "ruan"); hashSpell.Add(-14097, "rui"); hashSpell.Add(-14094, "run");
                hashSpell.Add(-14092, "ruo"); hashSpell.Add(-14090, "sa"); hashSpell.Add(-14087, "sai");
                hashSpell.Add(-14083, "san"); hashSpell.Add(-13917, "sang"); hashSpell.Add(-13914, "sao");
                hashSpell.Add(-13910, "se"); hashSpell.Add(-13907, "sen"); hashSpell.Add(-13906, "seng");
                hashSpell.Add(-13905, "sha"); hashSpell.Add(-13896, "shai"); hashSpell.Add(-13894, "shan");
                hashSpell.Add(-13878, "shang"); hashSpell.Add(-13870, "shao"); hashSpell.Add(-13859, "she");
                hashSpell.Add(-13847, "shen"); hashSpell.Add(-13831, "sheng"); hashSpell.Add(-13658, "shi");
                hashSpell.Add(-13611, "shou"); hashSpell.Add(-13601, "shu"); hashSpell.Add(-13406, "shua");
                hashSpell.Add(-13404, "shuai"); hashSpell.Add(-13400, "shuan"); hashSpell.Add(-13398, "shuang");
                hashSpell.Add(-13395, "shui"); hashSpell.Add(-13391, "shun"); hashSpell.Add(-13387, "shuo");
                hashSpell.Add(-13383, "si"); hashSpell.Add(-13367, "song"); hashSpell.Add(-13359, "sou");
                hashSpell.Add(-13356, "su"); hashSpell.Add(-13343, "suan"); hashSpell.Add(-13340, "sui");
                hashSpell.Add(-13329, "sun"); hashSpell.Add(-13326, "suo"); hashSpell.Add(-13318, "ta");
                hashSpell.Add(-13147, "tai"); hashSpell.Add(-13138, "tan"); hashSpell.Add(-13120, "tang");
                hashSpell.Add(-13107, "tao"); hashSpell.Add(-13096, "te"); hashSpell.Add(-13095, "teng");
                hashSpell.Add(-13091, "ti"); hashSpell.Add(-13076, "tian"); hashSpell.Add(-13068, "tiao");
                hashSpell.Add(-13063, "tie"); hashSpell.Add(-13060, "ting"); hashSpell.Add(-12888, "tong");
                hashSpell.Add(-12875, "tou"); hashSpell.Add(-12871, "tu"); hashSpell.Add(-12860, "tuan");
                hashSpell.Add(-12858, "tui"); hashSpell.Add(-12852, "tun"); hashSpell.Add(-12849, "tuo");
                hashSpell.Add(-12838, "wa"); hashSpell.Add(-12831, "wai"); hashSpell.Add(-12829, "wan");
                hashSpell.Add(-12812, "wang"); hashSpell.Add(-12802, "wei"); hashSpell.Add(-12607, "wen");
                hashSpell.Add(-12597, "weng"); hashSpell.Add(-12594, "wo"); hashSpell.Add(-12585, "wu");
                hashSpell.Add(-12556, "xi"); hashSpell.Add(-12359, "xia"); hashSpell.Add(-12346, "xian");
                hashSpell.Add(-12320, "xiang"); hashSpell.Add(-12300, "xiao"); hashSpell.Add(-12120, "xie");
                hashSpell.Add(-12099, "xin"); hashSpell.Add(-12089, "xing"); hashSpell.Add(-12074, "xiong");
                hashSpell.Add(-12067, "xiu"); hashSpell.Add(-12058, "xu"); hashSpell.Add(-12039, "xuan");
                hashSpell.Add(-11867, "xue"); hashSpell.Add(-11861, "xun"); hashSpell.Add(-11847, "ya");
                hashSpell.Add(-11831, "yan"); hashSpell.Add(-11798, "yang"); hashSpell.Add(-11781, "yao");
                hashSpell.Add(-11604, "ye"); hashSpell.Add(-11589, "yi"); hashSpell.Add(-11536, "yin");
                hashSpell.Add(-11358, "ying"); hashSpell.Add(-11340, "yo"); hashSpell.Add(-11339, "yong");
                hashSpell.Add(-11324, "you"); hashSpell.Add(-11303, "yu"); hashSpell.Add(-11097, "yuan");
                hashSpell.Add(-11077, "yue"); hashSpell.Add(-11067, "yun"); hashSpell.Add(-11055, "za");
                hashSpell.Add(-11052, "zai"); hashSpell.Add(-11045, "zan"); hashSpell.Add(-11041, "zang");
                hashSpell.Add(-11038, "zao"); hashSpell.Add(-11024, "ze"); hashSpell.Add(-11020, "zei");
                hashSpell.Add(-11019, "zen"); hashSpell.Add(-11018, "zeng"); hashSpell.Add(-11014, "zha");
                hashSpell.Add(-10838, "zhai"); hashSpell.Add(-10832, "zhan"); hashSpell.Add(-10815, "zhang");
                hashSpell.Add(-10800, "zhao"); hashSpell.Add(-10790, "zhe"); hashSpell.Add(-10780, "zhen");
                hashSpell.Add(-10764, "zheng"); hashSpell.Add(-10587, "zhi"); hashSpell.Add(-10544, "zhong");
                hashSpell.Add(-10533, "zhou"); hashSpell.Add(-10519, "zhu"); hashSpell.Add(-10331, "zhua");
                hashSpell.Add(-10329, "zhuai"); hashSpell.Add(-10328, "zhuan"); hashSpell.Add(-10322, "zhuang");
                hashSpell.Add(-10315, "zhui"); hashSpell.Add(-10309, "zhun"); hashSpell.Add(-10307, "zhuo");
                hashSpell.Add(-10296, "zi"); hashSpell.Add(-10281, "zong"); hashSpell.Add(-10274, "zou");
                hashSpell.Add(-10270, "zu"); hashSpell.Add(-10262, "zuan"); hashSpell.Add(-10260, "zui");
                hashSpell.Add(-10256, "zun"); hashSpell.Add(-10254, "zuo"); hashSpell.Add(-10247, "zz");
            }
            byte[] b = System.Text.Encoding.Default.GetBytes(name);
            int p;
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < b.Length; i++)
            {
                p = (int)b[i];
                if (p > 160)
                {
                    p = p * 256 + b[++i] - 65536;

                    if (p < -20319 || p > -10247)
                        result.Append("");

                    while (!hashSpell.ContainsKey(p))
                        p--;
                    result.Append(hashSpell[p].ToString());
                }
                else
                {
                    result.Append((char)p);
                }
            }
            return
              result.ToString();
        }
        #endregion
    }
}