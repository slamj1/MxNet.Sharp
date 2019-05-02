﻿using System;
using System.Collections.Generic;
using System.Text;
using MxNet.DotNet;

namespace MxNet.NN.Layers
{
    public class AvgPooling1D : BaseLayer, ILayer
    {
        public uint PoolSize { get; set; }

        public uint Strides { get; set; }

        public uint? Padding { get; set; }

        public AvgPooling1D(uint poolSize = 2, uint? strides = null, uint? padding = null)
            :base("avgpooling1d")
        {
            PoolSize = poolSize;
            Strides = strides.HasValue ? padding.Value : poolSize;
            Padding = padding;
        }

        public Symbol Build(Symbol x)
        {
            Shape pad = new Shape(); ;
            if (Padding.HasValue)
            {
                pad = new Shape(Padding.Value);
            }

            return ops.NN.Pooling(x, new Shape(PoolSize), PoolingPoolType.Avg, false, Global.UseCudnn, 
                                    PoolingPoolingConvention.Valid, new Shape(Strides), pad, 0, true, ConvolutionLayout.None, ID);
        }
    }
}
