using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgettingTools
{
    /// <summary>
    /// Enum representing the different ways to repeat an expense
    /// </summary>
    public enum ExpenseRepetition { Minutely = 0, Hourly, Daily, Weekly, Monthly, Yearly, None }

    /// <summary>
    /// Enum representing the seven days in a week
    /// </summary>
    public enum Weekdays { Sunday = 0, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday }

    /// <summary>
    /// Represents the repetition position of the current timing setup
    /// </summary>
    public class RepetitionPosition
    {
        public int DayNum, WeekNum, HourNum, MinuteNum, SecondNum;
        public Weekdays Weekday;

        public RepetitionPosition(Weekdays weekday = Weekdays.Monday, int dayNum = 1, int weekNum = 1, int hourNum = 1, int minuteNum = 0, int secondNum = 0)
        {
            Weekday = weekday;
            DayNum = dayNum;
            WeekNum = weekNum;
            HourNum = hourNum;
            MinuteNum = minuteNum;
            SecondNum = secondNum;
        }

        public override bool Equals(object obj)
        {
            if(obj.GetType() != GetType())
                return base.Equals(obj);
            else
            {
                RepetitionPosition temp = (RepetitionPosition)obj;
                return temp.DayNum == DayNum && temp.Weekday == Weekday && temp.WeekNum == WeekNum &&
                    temp.HourNum == HourNum && temp.MinuteNum == MinuteNum && temp.SecondNum == SecondNum;
            }
        }
    }
}
