﻿using mx_float = System.Single;

// ReSharper disable once CheckNamespace
namespace MxNet.DotNet
{

    public partial class NeuralNet
    {

        #region Methods

        public Symbol BatchNorm(Symbol data,
                                           Symbol gamma,
                                           Symbol beta,
                                           Symbol movingMean,
                                           Symbol movingVar,
                                           double eps = 0.001,
                                           mx_float momentum = 0.9f,
                                           bool fixGamma = true,
                                           bool useGlobalStats = false,
                                           bool outputMeanVar = false,
                                           int axis = 1,
                                           bool cudnnOff = false, string name = "")
        {
            return new Operator("BatchNorm").SetParam("eps", eps)
                                            .SetParam("momentum", momentum)
                                            .SetParam("fix_gamma", fixGamma)
                                            .SetParam("use_global_stats", useGlobalStats)
                                            .SetParam("output_mean_var", outputMeanVar)
                                            .SetParam("axis", axis)
                                            .SetParam("cudnn_off", cudnnOff)
                                            .SetInput("data", data)
                                            .SetInput("gamma", gamma)
                                            .SetInput("beta", beta)
                                            .SetInput("moving_mean", movingMean)
                                            .SetInput("moving_var", movingVar)
                                            .CreateSymbol(name);
        }

        #endregion

    }

}
