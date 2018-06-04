using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hslang
{
    public static class func
    {
        public static string runFunc(string className,string funcName,List<string> cs,script pro)
        {
            if (className.Equals("main"))
            {
                if (funcName.Equals("out"))
                {
                    cs.Add(";");
                    string returnValue = run.Run(cs, pro, cfg.RuntimeMode.value);
                    Console.WriteLine(tools.RemoveType(returnValue));
                    return "";
                }
                tools.throwError("在main类中无法找到指定函数：" + funcName);
            }
            if (className.Equals("debug"))
            {
                if (funcName.Equals("outVar"))
                {
                    foreach (var key in pro.Var)
                    {
                        Console.WriteLine("key: {0}  value{1}", key.Key,key.Value);
                    }
                    return "";
                }
                tools.throwError("在main类中无法找到指定函数：" + funcName);
            }
            tools.throwError("无法找到指定类：" + className);
            return "";
        }

        public static string getSx(string className,string sxName,script pro)
        {
            if (className.Equals("main"))
            {
                if (sxName.Equals("path"))
                {
                    return "[string]\"" + pro.cfg.path + "\"";
                }
                tools.throwError("在main类中无法找到指定属性:" + sxName);
                return "";
            }
            tools.throwError("无法找到指定类：" + className);
            return "";
        }
    }
}
