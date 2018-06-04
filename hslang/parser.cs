using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hslang
{
    public static class parser
    {
        /// <summary>
        /// 解析任意hslang脚本
        /// </summary>
        /// <param name="script">脚本的内容</param>
        /// <returns>返回解析后的单词表，可以拿来运行脚本</returns>
        public static List<string> Parser(string script)
        {
            List<string> word = new List<string>();//全部单词
            string text = "";//有效字符
            cfg.ParserMode mode = cfg.ParserMode.common;//初始化模式
            bool wrong = true;//错误，为true时会在解析完毕后抛出错误并返回空的，当扫描到；时自动变成false
            for (int i = 0; i < script.Length; i++)
            {
                string text1 = script.Substring(i, 1);//现行文字
                bool wrong1 = false;
                //文本域
                if (text.Equals("\"") && mode == cfg.ParserMode.common)//在普通模式，如果遇到"则代表进入文本模式，如果已经进入则代表进入正常模式，如果为转义模式则把本符号计入有效字符
                {
                    mode = cfg.ParserMode.text;
                    text = text + text1;
                    continue;
                }
                if (text1.Equals("\"") && mode == cfg.ParserMode.text)//在文本模式
                {
                    mode = cfg.ParserMode.common;//进入普通模式
                    text = text + text1;
                    word.Add(text);//添加单词
                    text = "";//清空
                    continue;
                }
                if (text1.Equals("\"") && mode == cfg.ParserMode.escape)//在转义模式
                {
                    mode = cfg.ParserMode.text;//进入文本模式
                    text = text + text1;
                    continue;
                }

                //遇到/时的情况
                if (text1.Equals("/") && mode == cfg.ParserMode.text)
                {//在文本模式
                    text = text + text1;
                    continue;
                }
                if (text1.Equals("/") && script.Length >= i && script.Substring(i + 1, 1).Equals("/"))
                {//文本数组下标大于当前下标并下一个字符也是/
                    break;  //直接结束for循环
                }
                if (text1.Equals("/") && script.Length >= i && !script.Substring(i + 1, 1).Equals("/"))
                {//文本数组下标大于当前下标并下一个字符不是/
                    word.Add(text);
                    word.Add(text1);
                    text = "";
                    continue;
                }


                //遇到\时的情况
                if (text1.Equals("\\") && mode == cfg.ParserMode.text)
                {//在文本模式
                    mode = cfg.ParserMode.escape;//进入转义模式
                    continue;
                }
                if (text1.Equals("\\") && mode == cfg.ParserMode.escape)
                {//在转义模式
                    text = text + text1;
                    mode = cfg.ParserMode.text;
                    continue;
                }

                //如果为+-*（/）=;,则将前面的做单词，本符号也做单词，不在文本模式
                if (mode != cfg.ParserMode.text)
                {
                    for (int a = 0; a < cfg.u.Length; a++)
                    {
                        if (cfg.u[a].Equals(text1))
                        {
                            if (wrong)
                            {
                                if (text1.Equals(";"))
                                {
                                    wrong = false;
                                }
                            }
                            if (!text.Equals(""))
                            {
                                word.Add(text);//前面做一个单词
                            }
                            word.Add(text1);
                            text = "";
                            wrong1 = true;
                            break;
                        }
                    }
                    if (wrong1)
                    {
                        continue;
                    }
                }

                //如果为.时的情况
                if (text1.Equals(".") && mode != cfg.ParserMode.text && tools.Parser_isNum(text) != true)
                {//引用了类
                    word.Add(text);
                    word.Add(text1);
                    text = "";
                    continue;
                }
                if (text1.Equals(".") && mode != cfg.ParserMode.text && tools.Parser_isNum(text) == true)
                {//浮点整数
                    text = text + text1;
                    continue;
                }

                //如果为空格时的情况
                if (text1.Equals(" ") && mode != cfg.ParserMode.text && tools.IsImportantWord(text))
                {
                    word.Add(text);
                    text = "";
                    continue;
                }
                if (text1.Equals(" ") && mode != cfg.ParserMode.text && !tools.IsImportantWord(text))
                {
                    continue;
                }

                //没什么就当做有效字符
                text = text + text1;
            }
            if (wrong)
            {
                Console.WriteLine("警告:在解析阶段没有扫描到结束符号（;）");
            }
            return word;
        }
    }
}
