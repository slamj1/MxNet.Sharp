﻿using MxNet.Gluon.NN;
using System;
using System.Collections.Generic;
using System.Text;

namespace MxNet.Gluon.Contrib.NN
{
    public class Concurrent : Sequential
    {
        public Concurrent(int axis = -1, string prefix = null, ParameterDict @params = null) : base(prefix, @params)
        {
            throw new NotImplementedException();
        }

        public override NDArrayOrSymbol Forward(NDArrayOrSymbol input, params NDArrayOrSymbol[] args)
        {
            throw new NotImplementedException();
        }
    }
}
