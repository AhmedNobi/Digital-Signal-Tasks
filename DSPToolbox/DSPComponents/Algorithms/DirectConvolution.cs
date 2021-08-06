using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        public override void Run()
        {
            //throw new NotImplementedException();
            List<float> OutputConvolved = new List<float>();
            // Produces a third function that expresses how the shape 
            // is modified by the other
            int FirstSignalSamples = 0 - InputSignal1.SamplesIndices[0], SecondSignalSamples = 0 - InputSignal2.SamplesIndices[0];

            int Start = InputSignal1.SamplesIndices[0] + InputSignal2.SamplesIndices[0];
            int End = InputSignal1.SamplesIndices[InputSignal1.Samples.Count - 1] + InputSignal2.SamplesIndices[InputSignal2.Samples.Count - 1];

            int Counter = Start;

            OutputConvolvedSignal = new Signal(new List<float>(), new List<int>(), false);
            while (true)
            {
                float Sum = 0;
                for (int i = Start; i <= End; i++)
                {
                    float FirstSignal = 0, SecondSignal = 0;
                    if (i + FirstSignalSamples < InputSignal1.Samples.Count && i + FirstSignalSamples >= 0)
                        FirstSignal = InputSignal1.Samples[i + FirstSignalSamples];      

                    if ((Counter - i) + SecondSignalSamples < InputSignal2.Samples.Count && (Counter - i) + SecondSignalSamples >= 0)
                        SecondSignal = InputSignal2.Samples[(Counter - i) + SecondSignalSamples];

                    Sum += FirstSignal * SecondSignal;
                }
                if (OutputConvolved.Count > InputSignal1.Samples.Count && OutputConvolved.Count > InputSignal2.Samples.Count && Sum == 0)
                    break;
                else
                {
                    OutputConvolved.Add(Sum);
                    OutputConvolvedSignal.SamplesIndices.Add(Counter);
                }
                Counter++;
            }
            OutputConvolvedSignal.Samples = OutputConvolved;
        }
    }
}
