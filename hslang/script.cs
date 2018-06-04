using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hslang
{
    public class script
    {
        public script()
        {

        }
        /// <summary>
        /// 变量表
        /// </summary>
        public Dictionary<string, string> Var = new Dictionary<string, string>();
        public struct Config
        {
            public string path;
        }

        /// <summary>
        /// 配置表
        /// </summary>
        public Config cfg;
    }
}
