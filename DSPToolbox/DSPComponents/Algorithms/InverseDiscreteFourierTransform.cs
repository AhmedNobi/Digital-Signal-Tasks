using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class InverseDiscreteFourierTransform : Algorithm
    {
        public Signal InputFreqDomainSignal { get; set; }
        public Signal OutputTimeDomainSignal { get; set; }

        public override void Run()
        {
            //throw new NotImplementedException();
            float tempCosine;
            OutputTimeDomainSignal = new Signal(new List<float>(), false);
            // amplitude * cosine * phase to get samples in frequency domian
            int N = InputFreqDomainSignal.Frequencies.Count;
            List<Tuple<float, float>> SignalInFrequencyDomain = new List<Tuple<float, float>>();
            for (int k = 0; k < N; k++)
            {
                Tuple<float, float> pair = new Tuple<float, float>(

                    (float)
                    (InputFreqDomainSignal.FrequenciesAmplitudes[k] *
                    Math.Cos(InputFreqDomainSignal.FrequenciesPhaseShifts[k])),

                    (float)
                    (InputFreqDomainSignal.FrequenciesAmplitudes[k] *
                    Math.Sin(InputFreqDomainSignal.FrequenciesPhaseShifts[k])));

                SignalInFrequencyDomain.Add(pair);
            }

            for (int n = 0; n < N; n++)
            {
                tempCosine = 0.0f;
                for (int k = 0; k < N; k++)
                {
                    float theta = (2.0f * 180.0f * n * k) / N;
                    // tempCosine minus Sin after some simplification 
                    tempCosine += SignalInFrequencyDomain[k].Item1 * (float)Math.Cos(degree_to_radian(theta));
                    tempCosine -= SignalInFrequencyDomain[k].Item2 * (float)Math.Sin(degree_to_radian(theta));
                }
                OutputTimeDomainSignal.Samples.Add(tempCosine / N);
            }
            //OutputTimeDomainSignal.Samples.Sort();
        }
        public float degree_to_radian(float theta)
        {
            return (float)(theta * Math.PI / 180.0f);
        }
    }
}
