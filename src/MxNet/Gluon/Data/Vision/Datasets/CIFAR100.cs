﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MxNet.Gluon.Data.Vision.Datasets
{
    public class CIFAR100 : CIFAR10
    {
        public CIFAR100(string root = "./datasets/cifar100", bool fine_label = false, bool train = true, Func<NDArrayDict, NDArrayDict> transform = null)
            : base(root, train, transform)
        {
            throw new NotImplementedException();
        }

        internal override (NDArray, NDArray) ReadBatch()
        {
            throw new NotImplementedException();
        }
    }
}
