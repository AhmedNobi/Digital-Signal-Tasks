using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Subtractor : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputSignal { get; set; }

        /// <summary>
        /// To do: Subtract Signal2 from Signal1 
        /// i.e OutSig = Sig1 - Sig2 
        /// </summary>
        public override void Run()
        {
            //throw new NotImplementedException();
            bool Periodic = InputSignal1.Periodic & InputSignal2.Periodic;
            MultiplySignalByConstant multiply = new MultiplySignalByConstant();
            multiply.InputConstant = (float)-1.0;
            multiply.InputSignal = InputSignal2;
            multiply.Run();
            InputSignal2 = multiply.OutputMultipliedSignal;
            Adder adder = new Adder();
            List<Signal> signals = new List<Signal>();
            signals.Add(InputSignal1);
            signals.Add(InputSignal2);
            adder.InputSignals = signals;
            adder.Run();
            OutputSignal = new Signal(adder.OutputSignal.Samples, Periodic);

        }
    }
}