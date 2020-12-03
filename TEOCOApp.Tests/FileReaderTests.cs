using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEOCOApp.Core;
using TEOCOApp.Service;

namespace TEOCOApp.Tests
{
    [TestClass]
    public class FileReaderTests
    {
        [TestMethod]
        public void Can_Read_Input_File_With_Data()
        {

            // Arrange
            var content = @"ftimespan, dataset, value
9m, 10, 30

1d3m, 10, 20


1d3m, 11, 120";

            // for testing we use memory stream
            using (var ms = new MemoryStream())
            {
                var data = Encoding.UTF8.GetBytes(content);
                ms.Write(data, 0, data.Length);
                ms.Position = 0;

                using (var streamReader = new StreamReader(ms))
                {
                    // Act
                    IDataFileReader reader = new DataFileReader();
                    var items = reader.ReadDataItems(streamReader).ToArray();

                    // Assert
                    Assert.AreEqual(3, items.Length);
                }
            }

        }
    }
}
