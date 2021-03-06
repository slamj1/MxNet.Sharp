﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MxNet.Initializers
{
    public class Xavier : Initializer
    {
        public string RndType { get; set; }

        public string FactorType { get; set; }

        public float Magnitude { get; set; }

        public Xavier(string rnd_type= "uniform", string factor_type= "avg", float magnitude= 3)
        {
            RndType = rnd_type;
            FactorType = factor_type;
            Magnitude = magnitude;
        }

        public override void InitWeight(string name, NDArray arr)
        {
            var shape = arr.Shape;
            float hw_scale = 1;
            if (shape.Dimension < 2)
                throw new ArgumentException(string.Format("Xavier initializer cannot be applied to vector {0}. It requires at least 2D", name));

            float fan_in = shape[1] * hw_scale;
            float fan_out = shape[0] * hw_scale;
            float factor = 1;
            if (FactorType == "avg")
                factor = (fan_in + fan_out) / 2;
            else if (FactorType == "in")
                factor = fan_in;
            else if (FactorType == "out")
                factor = fan_out;
            else
                throw new ArgumentException("Incorrect factor type");

            var scale = (float)Math.Sqrt(Magnitude / factor);

            if (RndType == "uniform")
                arr = nd.Random.Uniform(-scale, scale, arr.Shape);
            else if (RndType == "gaussian")
                arr = nd.Random.Normal(0, scale, arr.Shape);
            else
                throw new ArgumentException("Unknown random type");
        }
    }
}
