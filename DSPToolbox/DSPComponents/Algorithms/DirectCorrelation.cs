using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public override void Run()
        {
            //throw new NotImplementedException();
            
            if (InputSignal2 == null) 
                InputSignal2 = new Signal(new List<float>(InputSignal1.Samples), InputSignal1.Periodic);
            int N = InputSignal1.Samples.Count;
            OutputNonNormalizedCorrelation = new List<float>();
            for(int i = 0; i < N; i++)
            {
                float sum = 0;
                for(int j = 0; j < N; j++)
                {
                    sum += InputSignal1.Samples[j] * InputSignal2.Samples[j];
                }
                InputSignal2.Samples.Add(InputSignal2.Samples[0]);
                InputSignal2.Samples.RemoveAt(0);
                OutputNonNormalizedCorrelation.Add(sum / (N * 1.0f));

            }
            OutputNormalizedCorrelation = new List<float>(new float[N]);
            float maxi = OutputNonNormalizedCorrelation.Max(), mini = OutputNonNormalizedCorrelation.Min();
            for(int i = 0; i < OutputNonNormalizedCorrelation.Count; i++)
            {
                OutputNormalizedCorrelation[i] = (OutputNonNormalizedCorrelation[i] - mini) / (maxi - mini);
            }
            int h = 0;
            
        }
    }
}