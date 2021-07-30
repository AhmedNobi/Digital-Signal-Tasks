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
            OutputConvolvedSignal = new Signal(new List<float>(), new List<int>(), false);
            List<float> OutputConvolved = new List<float>();
            int XofSignal = 0 - InputSignal1.SamplesIndices[0];
            int HofSignal = 0 - InputSignal2.SamplesIndices[0];

            int Start = InputSignal1.SamplesIndices[0] + InputSignal2.SamplesIndices[0];
            int End = InputSignal1.SamplesIndices[InputSignal1.Samples.Count - 1] + InputSignal2.SamplesIndices[InputSignal2.Samples.Count - 1];

            int Counter = Start;

            while (true)
            {
                float Sum = 0;
                for (int i = Start; i <= End; i++)
                {
                    float FirstSignal, SecondSignal;
                    if (i + XofSignal >= InputSignal1.Samples.Count || i + XofSignal < 0)
                        FirstSignal = 0;
                    else
                        FirstSignal = InputSignal1.Samples[i + XofSignal];      

                    if ((Counter - i) + HofSignal >= InputSignal2.Samples.Count || (Counter - i) + HofSignal < 0)
                        SecondSignal = 0;
                    else
                        SecondSignal = InputSignal2.Samples[(Counter - i) + HofSignal];

                    Sum += FirstSignal * SecondSignal;
                }

                if (Sum == 0 && OutputConvolved.Count > InputSignal1.Samples.Count && OutputConvolved.Count > InputSignal2.Samples.Count)
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
