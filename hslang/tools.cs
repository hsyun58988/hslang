using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hslang
{
    /// <summary>
    /// hslang通用工具类
    /// </summary>
    public static class tools
    {
        /// <summary>
        /// 判断一个文本是否为数字，在parser时使用的
        /// </summary>
        /// <param name="text">要判断的文本</param>
        /// <returns></returns>
        public static bool Parser_isNum(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (!text.Substring(i, 1).Equals("0"))
                {
                    if (!text.Substring(i, 1).Equals("1"))
                    {
                        if (!text.Substring(i, 1).Equals("2"))
                        {
                            if (!text.Substring(i, 1).Equals("3"))
                            {
                                if (!text.Substring(i, 1).Equals("4"))
                                {
                                    if (!text.Substring(i, 1).Equals("5"))
                                    {
                                        if (!text.Substring(i, 1).Equals("6"))
                                        {
                                            if (!text.Substring(i, 1).Equals("7"))
                                            {
                                                if (!text.Substring(i, 1).Equals("8"))
                                                {
                                                    if (!text.Substring(i, 1).Equals("9"))
                                                    {
                                                        return false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 判断是否为关键字（在cfg类中设置的）
        /// </summary>
        /// <param name="text">要判断的文本</param>
        /// <returns></returns>
        public static bool IsImportantWord(string text)
        {
            for (int i = 0; i < cfg.ImportantWord.Length; i++)
            {
                if (text.Equals(cfg.ImportantWord[i]))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 判断一个文本是否为数字，在运行时使用的
        /// </summary>
        /// <param name="text">要判断的文本</param>
        /// <returns></returns>
        public static bool IsNum(string text)
        {
            if (IfType(text) == cfg.type.Int){
                return true;
            }
            Parser_isNum(text);
            return true;
        }

        /// <summary>
        /// 抛出一个错误
        /// </summary>
        /// <param name="errorText">错误文本</param>
        public static void throwError(string errorText)
        {
            Console.WriteLine("错误:" + errorText);
        }

        /// <summary>
        /// 判断一个值得数据类型
        /// </summary>
        /// <param name="text">要判断的值</param>
        /// <returns>数据类型，在cfg中的枚举</returns>
        public static cfg.type IfType(string text)
        {
            if(text.Length>=5 && text.Substring(0, 5).Equals("[int}"))
            {
                return cfg.type.Int;
            }
            if (text.Length >= 8 && text.Substring(0, 8).Equals("[string}"))
            {
                return cfg.type.String;
            }
            return cfg.type.Null;
        }

        /// <summary>
        /// 判断是否为文本
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsString(string text)
        {
            if (text.Substring(0, 1).Equals("\"") && text.Substring(text.Length - 1, text.Length).Equals("\""))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取真正的文本
        /// </summary>
        /// <param name="text"></param>
        /// <returns>真正的文本</returns>
        public static string GetTrueString(string text)
        {
            if (text.Length >= 8 && IfType(text) == cfg.type.String)
            {
                return text.Substring(9, text.Length - 1);
            }
            return text.Substring(1, text.Length - 1);
        }

        /// <summary>
        /// 删除一个值的前面的数据类型标识符
        /// </summary>
        /// <param name="text"></param>
        /// <returns>删除之后的文本</returns>
        public static string RemoveType(string text)
        {
            //System.out.println(text);
            if (IfType(text) == cfg.type.Int)
            {
                return text.Substring(5, text.Length);
            }
            if (IfType(text) == cfg.type.String)
            {
                return text.Substring(8, text.Length);
            }
            return "";
        }

        public static int getTrue(string text)
        {
            if (text.Length >= 5)
            {
                return int.Parse(RemoveType(text));
            }
            return -1;
        }
    }
}
