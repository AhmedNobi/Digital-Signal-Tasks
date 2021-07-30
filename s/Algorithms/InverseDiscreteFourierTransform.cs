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
            float tempSine;
            float tempCosine;
            OutputTimeDomainSignal = new Signal(new List<float>(), false, new List<float>(), new List<float>(), new List<float>());
            int N = InputFreqDomainSignal.Frequencies.Count; 
            for (int n = 0; n < N; n++)
            {
                tempSine = 0.0000000000000f;
                tempCosine = 0.0000000000000f;
                for (int k = 0; k < N; k++)
                {
                    float theta = (float)((2.0000000000000f * 180.0000000000000f * n * k) / N);
                    tempSine += InputFreqDomainSignal.Frequencies[k] * (float)Math.Sin(degree_to_radian(theta));
                    tempCosine += InputFreqDomainSignal.Frequencies[k] * (float)Math.Cos(degree_to_radian(theta));
                }
                OutputTimeDomainSignal.Samples.Add((tempSine + tempCosine) / N);
            }
        }
        public float degree_to_radian(float theta)
        {
            return (float)(theta * Math.PI / 180.0000000000000f);
        }
    }
}
