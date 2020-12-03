using System;
using System.Collections.Generic;
using System.IO;
using TEOCOApp.Core;
using TEOCOApp.Model;

namespace TEOCOApp.Service
{
    public class DataFileReader : IDataFileReader
    {

        public IEnumerable<DataItem> ReadDataItems(StreamReader stream)
        {
            var headerRead = false;
            string data;
            while ((data = stream.ReadLine()) != null)
            {
                if (!headerRead)
                {
                    // for sake of simplicity of the application, it has been assumed that order of column will not change in data.txt
                    headerRead = true;
                    continue;
                }

                var values = data.Split(',');

                if (values.Length == 3)
                {
                    var timespan = new DataTimeSpan();
                    if (DataTimeSpan.TryParse(values[0].Trim(), ref timespan))
                    {
                        var dataset = Int32.Parse(values[1]);
                        var value = Int32.Parse(values[2]);
                        yield return new DataItem(timespan, dataset, value);
                    }
                }
                
            }
        }
    }
}
