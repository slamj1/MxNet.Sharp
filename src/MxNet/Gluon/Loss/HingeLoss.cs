﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MxNet.Gluon
{
    public class HingeLoss : Loss
    {
        public HingeLoss(float margin = 1, float? weight = null, int? batch_axis = 0, string prefix = null, ParameterDict @params = null) : base(weight, batch_axis, prefix, @params)
        {
            throw new NotImplementedException();
        }

        public override NDArrayOrSymbol HybridForward(NDArrayOrSymbol pred, NDArrayOrSymbol label, NDArrayOrSymbol sample_weight = null)
        {
            throw new NotImplementedException();
        }
    }
}
