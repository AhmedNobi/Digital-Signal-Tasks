using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class QuantizationAndEncoding : Algorithm
    {
        // You will have only one of (InputLevel or InputNumBits), the other property will take a negative value
        // If InputNumBits is given, you need to calculate and set InputLevel value and vice versa
        public int InputLevel { get; set; }
        public int InputNumBits { get; set; }
        public Signal InputSignal { get; set; }
        public Signal OutputQuantizedSignal { get; set; }
        public List<int> OutputIntervalIndices { get; set; }
        public List<string> OutputEncodedSignal { get; set; }
        public List<float> OutputSamplesError { get; set; }

        public override void Run()
        {
            //throw new NotImplementedException();
            OutputQuantizedSignal = new Signal(new List<float>(), false);
            OutputIntervalIndices = new List<int>();
            OutputEncodedSignal = new List<string>();
            OutputSamplesError = new List<float>();

            // Mapping continuous infinite values to a smaller set of discreate finite values
            if (InputNumBits < 1)
                InputNumBits = (int)Math.Log(InputLevel, 2);
            else
                InputLevel = 1 << InputNumBits;

            
            float delta = (InputSignal.Samples.Max() - InputSignal.Samples.Min()) * 1.0f / InputLevel;

            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                float start = InputSignal.Samples.Min();
                float end;
                for (int j = 0; j < InputLevel; j++)
                {
                    end = start + delta;
                    if ((InputSignal.Samples[i] >= start && InputSignal.Samples[i] < end) || (j == InputLevel - 1))
                    {
                        float Midpoint = (start + end) / (2.0f * 1.0f);
                        OutputQuantizedSignal.Samples.Add(Midpoint);
                        OutputIntervalIndices.Add(j + 1);
                        // Convert level to binary and get binary value
                        string BinaryValue = Convert.ToString(j, 2);
                        while (BinaryValue.Length < InputNumBits)
                            BinaryValue = "0" + BinaryValue;
                        
                        OutputEncodedSignal.Add(BinaryValue);
                        OutputSamplesError.Add(Midpoint - InputSignal.Samples[i]);
                        break;
                    }
                    start = end;
                }
            }
        }
    }
}