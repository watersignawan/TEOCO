using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEOCOApp.Core;
using TEOCOApp.Model;
using TEOCOApp.Service;

namespace TEOCOApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var stream = File.OpenRead("data.txt"))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    IDataFileReader reader = new DataFileReader();
                    var items = reader.ReadDataItems(streamReader).ToArray();

                    // Write files
                    WriteToFile(items.OrderBy(i => i.Timespan).ThenBy(i => i.Dataset).ThenBy(i => i.Value), "result_1.txt");
                    WriteToFile(items.OrderBy(i => i.Dataset).ThenBy(i => i.Timespan).ThenBy(i => i.Value), "result_2.txt");
                }
            }
        }

        private static void WriteToFile(IEnumerable<DataItem> items, string filename)
        {
            using (var stream = File.OpenWrite(filename))
            {
                var writer = new StreamWriter(stream);
                writer.WriteLine(@"ftimespan, dataset, value");

                foreach (var item in items)
                {
                    writer.WriteLine(item);
                }
                writer.Flush();
                stream.Flush();
            }

        }
    }
}
