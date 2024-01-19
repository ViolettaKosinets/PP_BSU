using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjectPP
{
    public class Json
    {
        public static void JsonWrite(string path, List<double> res)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize<List<double>>(fs, res);
            }
        }

        public static void JsonRead(string filename, List<string> res)
        {
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                List<string> person = JsonSerializer.Deserialize<List<string>>(fs);
            }
        }
    }
}
