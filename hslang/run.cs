using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hslang
{
    public static class run
    {
        /// <summary>
        /// 运行解析后的脚本
        /// </summary>
        /// <param name="code">解析后的数组</param>
        /// <param name="pro">脚本对象</param>
        /// <param name="RuntimeMode">运行时模式，如果为cfg.RuntimeMode.value则返回执行后的结果，反之不返回</param>
        /// <returns>指定代码执行后该代码返回的值</returns>
        public static string Run(List<string> code,script pro,cfg.RuntimeMode RuntimeMode)
        {
            cfg.ParserMode ParserMode = cfg.ParserMode.common;
            int jumpNum = 0;
            string left = "";
            string type = "null";
            cfg.typeX typeX = cfg.typeX.Null;
            for(int point = 0; point < code.Count; point++)
            {
                string word = code[point];
                if (ParserMode == cfg.ParserMode.jump)
                {
                    jumpNum--;
                    if (jumpNum == 0)
                    {
                        ParserMode = cfg.ParserMode.common;
                    }
                    continue;
                }

                if (tools.IsImportantWord(word))
                {
                    if (word.Equals("include") && code.Count >= point + 2)
                    {
                        Console.WriteLine("[debug]引入了" + word + "包");
                        continue;
                    }
                    if ((word.Equals("int") || word.Equals("string")) && code.Count >= point + 1)
                    {
                        type = word;
                        continue;
                    }
                }

                if (tools.IsNum(word))
                {
                    if (ParserMode == cfg.ParserMode.common)
                    {
                        left = "[int]" + word;
                        continue;
                    }
                }

                if (tools.IsString(word))
                {
                    if (ParserMode == cfg.ParserMode.common)
                    {
                        left = "[string]" + word;
                        continue;
                    }
                }

                if (word.Equals("="))
                {
                    if (!type.Equals("null")&&typeX==cfg.typeX.var)//类型不为空的情况下
                    {
                        List<string> s_code = new List<string>();
                        int i = 1;
                        for (; point + i < code.Count; i++)
                        {
                            s_code.Add(code[point - 1 + i]);
                            if (code[point - 1 + i].Equals(";"))
                            {
                                break;
                            }
                        }
                        ParserMode = cfg.ParserMode.jump;
                        jumpNum = i - 1;
                        s_code.Add(";");
                        pro.Var.Add(left, Run(code, pro, cfg.RuntimeMode.value));
                        type = "null";
                        if (RuntimeMode == cfg.RuntimeMode.value)
                        {
                            return "[bool]true";
                        }
                        continue;
                    }
                }

                if (code.Count >= point && tools.IsNum(code[point]) && tools.IsNum(left))
                {
                    if (word.Equals("+"))
                    {
                        left = "[int]" + Convert.ToString(tools.getTrue(left) + int.Parse(code[point]));
                    }
                    if (word.Equals("-"))
                    {
                        left = "[int]" + Convert.ToString(tools.getTrue(left) - int.Parse(code[point]));
                    }
                    if (word.Equals("*"))
                    {
                        left = "[int]" + Convert.ToString(tools.getTrue(left) * int.Parse(code[point]));
                    }
                    if (word.Equals("/"))
                    {
                        left = "[int]" + Convert.ToString(tools.getTrue(left) / int.Parse(code[point]));
                    }
                    ParserMode = cfg.ParserMode.jump;
                    jumpNum = 1;
                }

                if (!tools.IsNum(word) && !tools.IsString(word))
                {
                    if (code.Count >= point + 3 && code[point].Equals(".") && code[point + 2].Equals("("))
                    {
                        string className = word;
                        string funcName = code[point + 1];
                        List<string> cs = new List<string>();
                        int i = 0;
                        for (; point + 3 + i < code.Count; i++)
                        {
                            if (code[point + 2 + i].Equals(")"))
                            {
                                break;
                            }
                            else
                            {
                                cs.Add(code[point + 2 + i]);
                            }
                        }
                        ParserMode = cfg.ParserMode.jump;
                        jumpNum = 2 + i;//现在是在类的位置，跳过两个是点+funcName，x最小为1，最少是左括号，右括号也在x里，参数也算在x里
                        left = func.runFunc(className, funcName, cs, pro);
                        continue;
                    }
                    if (code.Count >= point + 2 && code[point].Equals("."))
                    {
                        string className = word;
                        string sxName = code[point + 1];
                        left = func.getSx(className, sxName, pro);
                        ParserMode = cfg.ParserMode.jump;
                        jumpNum = 2;
                        continue;
                    }
                    if (!type.Equals("null")&& code.Count > point + 2)//应该是函数的返回值(#0003中不可能有函数)或变量的数据类型
                    {
                        if (code[point].Equals("("))
                        {
                            //do something
                        }
                        else
                        {
                            pro.Var.Add("[" + type + "]", "");
                            left = "[" + type + "]" + word;
                        }
                        continue;
                    }
                    if (RuntimeMode == cfg.RuntimeMode.value)
                    {
                        if (pro.Var.ContainsKey("[string]" + word))
                        {
                            left = pro.Var["[string]" + word];
                        }
                        if (pro.Var.ContainsKey("[int]" + word))
                        {
                            left = pro.Var["[int]" + word];
                        }
                        continue;
                    }
                }
                if (word.Equals(";"))
                {
                    ParserMode = cfg.ParserMode.common;
                    jumpNum = 0;
                    left = "";
                    if (RuntimeMode == cfg.RuntimeMode.value)
                    {
                        return left;
                    }
                    continue;
                }
            }
            return "";
        }
    }
}
