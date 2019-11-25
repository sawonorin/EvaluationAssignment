using System;

namespace AvCalc.Models
{
    [Serializable]
    public class DailyPattern
    {
        public virtual long Id { get; set; }
        public virtual int ColorId { get; set; }
        public virtual int PatternId { get; set; }
        public virtual DateTime ValidTo { get; set; }
    }

    public enum ColorThemeNames
    {
        Red = 0,
        Orange = 1,
        Yellow = 2,
        Green = 3,
        LightBlue = 4,
        Purple = 5,
        Indigo = 6,
        Steel = 7,
        Pink = 8,
        Blue = 9
    }

    public enum PatternThemes
    {
        City = 0,
        Country = 1
    }
}
