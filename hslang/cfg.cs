using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hslang
{
    public static class cfg
    {
        public static string[] ImportantWord = { "int", "string", "float", "include" };//关键字
        public enum ParserMode
        {
            common = 1,
            text = 2,
            escape = 3,
            jump = 4
        }//解析时模式
        public static string[] u = { "+", "-", "*", "/", "(", ")", ",", ";" };//单独符号
        public enum type
        {
            Int=1,
            String=2,
            Float=3,
            Null=4,
            Other=5
        }//数据类型
        public enum RuntimeMode
        {
            command=1,//普通模式
            value=2,//取值模式，之后有返回值
        }
        public enum typeX
        {
            Null=0,
            var=1,
            func=2,
        }
    }
}
