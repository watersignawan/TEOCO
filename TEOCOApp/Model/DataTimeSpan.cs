using System;
using System.Text.RegularExpressions;

namespace TEOCOApp.Model
{
    public struct DataTimeSpan : IComparable<DataTimeSpan>, IEquatable<DataTimeSpan>
    {
        public DataTimeSpan(int days, int weeks, int months, int years)
        {
            this.TotalYears = years;
            this.TotalMonths = months;
            this.TotalWeeks = weeks;
            this.TotalDays = days;
        }

        public int TotalDays { get; private set; }

        public int TotalWeeks { get; private set; }

        public int TotalMonths { get; private set; }

        public int TotalYears { get; private set; }

        public int CompareTo(DataTimeSpan other)
        {
            /*
             * Certains assumptions are taken into account.
             * 
             *      1. 1 Year   = 365 Days
             *      2. 1 Month  = 30 Days
             */
            const int DaysInYear = 365;
            const int DaysInMonth = 30;
            const int DaysInWeek = 7;
            return (this.TotalYears * DaysInYear + this.TotalMonths * DaysInMonth + this.TotalWeeks * DaysInWeek + this.TotalDays) - (other.TotalYears * DaysInYear + other.TotalMonths * DaysInMonth + other.TotalWeeks * DaysInWeek + other.TotalDays);
        }

        public bool Equals(DataTimeSpan other)
        {
            return this.CompareTo(other) == 0;
        }

        public override string ToString()
        {
            var formattedValue = String.Empty;

            if (this.TotalYears > 0)
            {
                formattedValue += $"{TotalYears}y";
            }

            if (this.TotalMonths > 0)
            {
                formattedValue += $"{TotalMonths}m";
            }

            if (this.TotalWeeks > 0)
            {
                formattedValue += $"{TotalWeeks}w";
            }

            if (this.TotalDays > 0)
            {
                formattedValue += $"{TotalDays}d";
            }

            return formattedValue;
        }

        private static readonly Regex TimespanRegex = new Regex(@"([1-9][0-9]*)([ymwd])", RegexOptions.Compiled);

        public static DataTimeSpan Parse(string value)
        {
            var matches = TimespanRegex.Matches(value);

            var days = 0;
            var weeks = 0;
            var months = 0;
            var years = 0;

            foreach (Match match in matches)
            {
                var parsedValue = Int32.Parse(match.Groups[1].Value);

                // To handle duplicate specified values we can check if values has already been parsed
                switch (match.Groups[2].Value)
                {
                    case "y":
                        if (years > 0)
                        {
                            throw new ArgumentException("year value has been specified multiple times", nameof(value));
                        }
                        else
                        {
                            years = parsedValue;
                        }
                        break;

                    case "m":
                        if (months > 0)
                        {
                            throw new ArgumentException("month value has been specified multiple times", nameof(value));
                        }
                        else
                        {
                            if (parsedValue > 11)
                            {
                                throw new ArgumentException("month value cannot be greater than 11", nameof(value));
                            }
                            
                            months = parsedValue;
                        }
                        break;

                    case "w":
                        if (weeks > 0)
                        {
                            throw new ArgumentException("week value has been specified multiple times", nameof(value));
                        }
                        else
                        {
                            if (parsedValue > 4)
                            {
                                throw new ArgumentException("week value cannot be greater than 4", nameof(value));
                            }

                            weeks = parsedValue;
                        }
                        break;

                    case "d":
                        if (days > 0)
                        {
                            throw new ArgumentException("day value has been specified multiple times", nameof(value));
                        }
                        else
                        {
                            if (parsedValue > 6)
                            {
                                throw new ArgumentException("day value cannot be greater than 6", nameof(value));
                            }

                            days = parsedValue;
                        }
                        break;
                }
            }

            return new DataTimeSpan(days, weeks, months, years);
        }

        public static bool TryParse(string value, ref DataTimeSpan timespan)
        {
            try
            {
                timespan = DataTimeSpan.Parse(value);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
    }
}
