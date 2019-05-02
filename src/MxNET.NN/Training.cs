﻿using MxNet.DotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace MxNet.NN
{
    public partial class Module
    {
        public void Fit(DataIter train, uint epochs=1, uint batchSize=32, DataIter validation = null, bool shuffle = false)
        {
            var args = new SortedDictionary<string, NDArray>();
            var argGrads = new SortedDictionary<string, NDArray>();
            string labelName = "label";
            var label = Symbol.Variable(labelName);
            
            List<uint> inputShape = new List<uint>();
            inputShape.Add(batchSize);
            inputShape.AddRange(InputShape);
            
            args["X"] = new NDArray(new Shape(inputShape.ToArray()));
            args[labelName] = new NDArray(new Shape(batchSize));
            
            Model.InferArgsMap(Global.Device, args, args);
            
            var defaultInitializer = new SiaDNN.Initializers.GlorotUniform();

            foreach (var arg in args)
            {
                argGrads.Add(arg.Key, new NDArray(arg.Value.GetShape()));
                if (ParamInitializers.ContainsKey(arg.Key))
                {
                    ParamInitializers[arg.Key].Operator(arg.Key, arg.Value);
                }
                else
                {
                    defaultInitializer.Operator(arg.Key, arg.Value);
                }
            }

            ModelOptimizer.SetParam("rescale_grad", 1.0 / batchSize);

            using (var exec = Model.SimpleBind(Global.Device, args, argGrads))
            {
                var argNames = Model.ListArguments();

                // Start training
                var sw = new Stopwatch();
                for (var iter = 1; iter <= epochs; iter++)
                {
                    uint samples = 0;
                    train.BatchSize = batchSize;
                    train.Reset();
                    Metric.Reset();
                    TrainMetric.Reset();
                    sw.Restart();

                    while (train.Next())
                    {
                        samples += batchSize;
                        var dataBatch = train.GetDataBatch();
                        
                        // Set data and label
                        dataBatch.Data.CopyTo(args["X"]);
                        dataBatch.Label.CopyTo(args[labelName]);
                        
                        // Compute gradients
                        exec.Forward(true);
                        exec.Backward(exec.Outputs);
                        // Update parameters
                        for (var i = 0; i < argNames.Count; ++i)
                        {
                            if (argNames[i] == "X" || argNames[i] == labelName)
                                continue;

                            ModelOptimizer.Update(i, exec.ArgmentArrays[i], exec.GradientArrays[i]);
                        }

                        TrainMetric.Update(dataBatch.Label, exec.Outputs[0]);
                    }
                    
                    sw.Stop();

                    if (validation != null)
                    {
                        validation.BatchSize = batchSize;
                        validation.Reset();
                        while (validation.Next())
                        {
                            var dataBatch = validation.GetDataBatch();
                            dataBatch.Data.CopyTo(args["X"]);
                            dataBatch.Label.CopyTo(args[labelName]);

                            // Forward pass is enough as no gradient is needed when evaluating
                            exec.Forward(false);
                            Metric.Update(dataBatch.Label, exec.Outputs[0]);
                        }
                    }


                    var duration = sw.ElapsedMilliseconds == 0 ? 1 : sw.ElapsedMilliseconds;
                    if (validation == null)
                    {
                        Logging.LG($"Epoch: {iter} {Convert.ToInt32(samples * 1000 / duration)} samples/sec Train_Metric: {TrainMetric.Get()}");
                    }
                    else
                    {
                        Logging.LG($"Epoch: {iter} {Convert.ToInt32(samples * 1000 / duration)} samples/sec, Train_Metric: {TrainMetric.Get()},  Val_Metric: {Metric.Get()}");
                    }
                }
            }

            MXNet.MXNotifyShutdown();
        }
    }
}
