﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MxNet.Optimizers
{
    public class Signum : Optimizer
    {
        public Signum(float learning_rate = 0.01f, float momentum = 0.9f, float wd_lh = 0) : base(learning_rate: learning_rate)
        {
            throw new NotImplementedException();
        }

        public override NDArrayDict CreateState(int index, NDArray weight)
        {
            throw new NotImplementedException();
        }

        public override void Update(int index, NDArray weight, NDArray grad, NDArrayDict state)
        {
            throw new NotImplementedException();
        }
    }
}
