﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MxNet.Initializers
{
    public class One : Initializer
    {
        public override void InitWeight(string name, NDArray arr)
        {
            arr.Constant(1);
        }
    }
}
