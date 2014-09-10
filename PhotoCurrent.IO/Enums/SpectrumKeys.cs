using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoCurrent.IO.Enums
{
    /// <summary>
    /// SpectrumKeys contains all of the keys into the 
    /// HeaderItems Dictionary that is in both the
    /// Parser.CSV and Writer.CSV objects.
    /// </summary>
    public class SpectrumKeys
    {
        public const String FileDateKey = "FileDate";
        public const String CommentKey = "Comment";
        public const String StartKey = "Start";
        public const String EndKey = "End";
        public const String IncrementKey = "Increment";
        public const String DelayKey = "Delay";
        public const String DataTypeKey = "Data Type";
        public const String ScaleKey = "Scale";
        public const String PowerKey = "Power";
    }
}
