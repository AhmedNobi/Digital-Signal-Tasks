using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DiscreteFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public float InputSamplingFrequency { get; set; }
        public Signal OutputFreqDomainSignal { get; set; }

        public override void Run()
        {
            //throw new NotImplementedException();

            List<float> Sine = new List<float>();
            List<float> Cosine = new List<float>();
            int N = InputTimeDomainSignal.Samples.Count;
            for (int k = 0; k < N; k++)
            {
                float tempSine = 0.0f;
                float tempCosine = 0.0f;
                for(int n = 0; n < N; n++)
                {
                    float theta = 2 * 180 * n * k / N;
                    tempSine += -InputTimeDomainSignal.Samples[n] * (float)Math.Sin(degree_to_radian(theta));
                    tempCosine += InputTimeDomainSignal.Samples[n] * (float)Math.Cos(degree_to_radian(theta));
                }
                Sine.Add(tempSine);
                Cosine.Add(tempCosine);
            }

            List<float> amplitude = new List<float>();
            List<float> phase = new List<float>();
            for (int i = 0; i < N; i++)
            {
                amplitude.Add((float)Math.Sqrt(Sine[i] * Sine[i] + Cosine[i] * Cosine[i]));
                phase.Add((float)Math.Atan2(degree_to_radian(Sine[i]) , degree_to_radian( Cosine[i])));
            }
            List<float> tempOutput = new List<float>();
            float ratio = (float)(2 * Math.PI / (N * (1 / InputSamplingFrequency)));
            for(int i = 1; i <= N; i++)
            {
                tempOutput.Add((float)(i * ratio));
            }
            OutputFreqDomainSignal = new Signal(tempOutput, false);
            OutputFreqDomainSignal.FrequenciesAmplitudes = amplitude;
            OutputFreqDomainSignal.FrequenciesPhaseShifts = phase;

            
        }
        public float degree_to_radian(float theta)
        {
            return (float)(theta * Math.PI / 180);
        }
    }

}
