﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MxNet
{
    public class Slice
    {
        public int Begin { get; set; }

        public int? End { get; set; }

        public Slice(int begin, int? end)
        {
            Begin = begin;
            End = end;
        }

        public override string ToString()
        {
            if (End.HasValue)
                return string.Format("{0}:{1}", Begin, End.Value);
            else
                return string.Format("{0}:", Begin);
        }
    }
}
