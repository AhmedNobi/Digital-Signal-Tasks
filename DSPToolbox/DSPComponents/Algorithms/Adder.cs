using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Adder : Algorithm
    {
        public List<Signal> InputSignals { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            //throw new NotImplementedException();
            int maxi = 0;
            bool Periodic = true;
            for(int i = 0; i < InputSignals.Count; i++)
            {
                maxi = Math.Max(maxi, InputSignals[i].Samples.Count);
            }
            List<float> output = new List<float>();
            for(int i = 0; i < maxi; i++)
            {
                output.Add(0);
            }
            for(int i = 0; i < InputSignals.Count; i++)
            {
                if (InputSignals[i].Samples.Count < maxi)
                {
                    for(int j = 0; j < maxi; j++)
                    {
                        InputSignals[i].Samples.Add(0);
                    }
                }
            }
            for(int i = 0; i < InputSignals.Count; i++)
            {
                for(int j = 0; j < InputSignals[i].Samples.Count; j++)
                {
                    output[j] += InputSignals[i].Samples[j];
                    Periodic &= InputSignals[i].Periodic;
                }
            }
            OutputSignal = new Signal(output, Periodic);
        }
    }
}