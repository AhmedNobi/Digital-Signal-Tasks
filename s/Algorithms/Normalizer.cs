using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Normalizer : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputMinRange { get; set; }
        public float InputMaxRange { get; set; }
        public Signal OutputNormalizedSignal { get; set; }

        public override void Run()
        {
            //throw new NotImplementedException();
            bool Periodic = InputSignal.Periodic;
            float maxi = float.MinValue, mini = float.MaxValue;
            List<float> output = new List<float>();
            for(int i = 0; i < InputSignal.Samples.Count; i++)
            {
                maxi = Math.Max(maxi, InputSignal.Samples[i]);
                mini = Math.Min(mini, InputSignal.Samples[i]);
            }
            for (int i = 0; i < InputSignal.Samples.Count; i++)
                output.Add(((InputMaxRange - InputMinRange) * ((InputSignal.Samples[i] - mini) / (maxi - mini))) + InputMinRange);
            OutputNormalizedSignal = new Signal(output, Periodic);
        }
    }
}
