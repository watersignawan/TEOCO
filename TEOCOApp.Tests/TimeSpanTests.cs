using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TEOCOApp.Model;

namespace TEOCOApp.Tests
{
    [TestClass]
    public class TimeSpanTests
    {
        [TestMethod]
        public void Timespan_Are_Equal()
        {
            // Arrange
            var ts1 = new DataTimeSpan(1, 2, 3, 4);
            var ts2 = new DataTimeSpan(1, 2, 3, 4);

            // Act
            var areEqual = ts1.Equals(ts2);

            // Assert
            Assert.AreEqual(true, areEqual);
        }

        [TestMethod]
        public void Timespan_Are_Not_Equal()
        {
            // Arrange
            var ts1 = new DataTimeSpan(1, 2, 3, 4);
            var ts2 = new DataTimeSpan(4, 3, 2, 1);

            Console.WriteLine(ts1.CompareTo(ts2));

            // Act
            var areEqual = ts1.Equals(ts2);

            // Assert
            Assert.AreEqual(false, areEqual);
        }

        [TestMethod]
        public void Can_Parse_Timespan()
        {
            // Arrange & Act
            var ts1 = DataTimeSpan.Parse("1y1m1w1d");

            // Assert
            Assert.AreEqual(1, ts1.TotalYears);
            Assert.AreEqual(1, ts1.TotalMonths);
            Assert.AreEqual(1, ts1.TotalWeeks);
            Assert.AreEqual(1, ts1.TotalDays);
        }

        [TestMethod]
        public void Can_Parse_Timespan_With_TryParse()
        {

            // Arrange
            var ts1 = new DataTimeSpan();

            // Act
            var parsed = DataTimeSpan.TryParse("1y1m1w1d", ref ts1);

            // Assert
            Assert.AreEqual(true, parsed);
            Assert.AreEqual(1, ts1.TotalYears);
            Assert.AreEqual(1, ts1.TotalMonths);
            Assert.AreEqual(1, ts1.TotalWeeks);
            Assert.AreEqual(1, ts1.TotalDays);
        }

        [TestMethod]
        public void Timespan_Parse_Should_Throw_Exception_When_Duplicates_Unit_Parsed()
        {
            // Arrange
            const string duplicateYears = "1y2y";
            const string duplicateMonths = "1m2m";
            const string duplicateWeeks = "1w2w";
            const string duplicateDays = "1d2d";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => DataTimeSpan.Parse(duplicateYears));
            Assert.ThrowsException<ArgumentException>(() => DataTimeSpan.Parse(duplicateMonths));
            Assert.ThrowsException<ArgumentException>(() => DataTimeSpan.Parse(duplicateWeeks));
            Assert.ThrowsException<ArgumentException>(() => DataTimeSpan.Parse(duplicateDays));
        }

        [TestMethod]
        public void Timespan_Parse_Should_Failed_WithoutException()
        {
            // Arrange
            var ts1 = new DataTimeSpan();

            // Act
            var parsed = DataTimeSpan.TryParse("1y1y", ref ts1);

            // Assert
            Assert.AreEqual(false, parsed);
        }

        [TestMethod]
        public void Timespan_Months_Cannot_Be_More_Than_Eleven()
        {
            // Arrange
            const string timespanValue = "12m";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => DataTimeSpan.Parse(timespanValue));
        }

        [TestMethod]
        public void Timespan_Weeks_Cannot_Be_More_Than_Four()
        {
            // Arrange
            const string timespanValue = "5w";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => DataTimeSpan.Parse(timespanValue));
        }

        [TestMethod]
        public void Timespan_Months_Cannot_Be_More_Than_Six()
        {
            // Arrange
            const string timespanValue = "7d";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => DataTimeSpan.Parse(timespanValue));
        }

        
    }
}
