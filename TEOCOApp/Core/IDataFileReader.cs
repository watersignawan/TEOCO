using System.Collections.Generic;
using System.IO;
using TEOCOApp.Model;

namespace TEOCOApp.Core
{
    public interface IDataFileReader
    {
        IEnumerable<DataItem> ReadDataItems(StreamReader stream);
    }
}
