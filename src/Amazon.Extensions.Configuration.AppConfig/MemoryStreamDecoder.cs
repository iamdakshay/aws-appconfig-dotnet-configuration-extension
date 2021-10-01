using System;
using System.IO;
using System.Text;

namespace Amazon.Extensions.Configuration.AppConfig
{
    public class MemoryStreamDecoder
    {
        public static string DecodeMemoryStreamToString(MemoryStream content)
        {
            StreamReader reader = new StreamReader(content);
            return reader.ReadToEnd();
        }
    }
}
