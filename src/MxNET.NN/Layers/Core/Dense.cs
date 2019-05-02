﻿using System;
using System.Collections.Generic;
using System.Text;
using MxNet.DotNet;
using Newtonsoft.Json;
using SiaDNN.Initializers;
using MxNet.NN.Layers.Activations;
using SiaDNN.Constraints;
using MxNet.NN.Regularizers;

namespace MxNet.NN.Layers
{
    public class Dense : BaseLayer, ILayer
    {
        public int Dim { get; set; }

        public ILayer Activation { get; set; }

        public bool UseBias { get; set; }

        public BaseInitializer KernalInitializer { get; set; }

        public BaseInitializer BiasInitializer { get; set; }

        public BaseConstraint KernalConstraint { get; set; }

        public BaseConstraint BiasConstraint { get; set; }

        public BaseRegularizer KernalRegularizer { get; set; }

        public BaseRegularizer BiasRegularizer { get; set; }

        public Dense(int dim, ActivationType activation = ActivationType.Linear, 
                    BaseInitializer kernalInitializer = null, BaseRegularizer kernalRegularizer = null, BaseConstraint kernalConstraint = null,
                    bool useBias = false, BaseInitializer biasInitializer = null, BaseRegularizer biasRegularizer=null, BaseConstraint biasConstraint = null)
            : base("dense")
        {
            Dim = dim;
            Activation = ActivationRegistry.Get(activation);
            UseBias = useBias;
            KernalInitializer = kernalInitializer ?? new GlorotUniform();
            BiasInitializer = biasInitializer ?? new Zeros();
            KernalConstraint = kernalConstraint;
            BiasConstraint = biasConstraint;
            KernalRegularizer = kernalRegularizer;
            BiasRegularizer = biasRegularizer;
        }

        public Symbol Build(Symbol data)
        {
            var weightName = UUID.GetID(ID + "_w");
            var biasName = UUID.GetID(ID + "_b");

            InitParams.Add(weightName, KernalInitializer);
            InitParams.Add(biasName, BiasInitializer);

            ConstraintParams.Add(weightName, KernalConstraint);
            ConstraintParams.Add(biasName, BiasConstraint);

            RegularizerParams.Add(weightName, KernalRegularizer);
            RegularizerParams.Add(biasName, BiasRegularizer);

            if (Activation != null)
            {
                return Activation.Build(ops.NN.FullyConnected(data, Symbol.Variable(weightName), Symbol.Variable(biasName), Dim));
            }
            else
            {
                return ops.NN.FullyConnected(data, Symbol.Variable(weightName), Symbol.Variable(biasName), Dim);
            }
        }
    }
}
