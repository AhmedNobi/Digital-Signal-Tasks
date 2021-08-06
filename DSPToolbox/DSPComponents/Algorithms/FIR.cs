using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FIR : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public FILTER_TYPES InputFilterType { get; set; }
        public float InputFS { get; set; }
        public float? InputCutOffFrequency { get; set; }
        public float? InputF1 { get; set; }
        public float? InputF2 { get; set; }
        public float InputStopBandAttenuation { get; set; }
        public float InputTransitionBand { get; set; }
        public Signal OutputHn { get; set; }
        public Signal OutputYn { get; set; }

        public override void Run()
        {
            //throw new NotImplementedException();
            double TableValue = 0, normalizeInputTransitionBand = InputTransitionBand / InputFS;
            int N;
            if (InputStopBandAttenuation <= 21) 
                TableValue = 0.9;
            else if (InputStopBandAttenuation <= 44) 
                TableValue = 3.1;
            else if (InputStopBandAttenuation <= 53) 
                TableValue = 3.3;
            else if (InputStopBandAttenuation <= 74) 
                TableValue = 5.5;
            N = (int)(TableValue / normalizeInputTransitionBand);
            if (N % 2 == 0)
                N++;

            // Set Signal saples and indices to avoid exeption later 
            // set them to zeroes
            List<float> SignalSamples = new List<float>(new float[N]);
            List<int> SignalInices = new List<int>(new int[N]);


            for (int n = 0; n <= N / 2; n++)
            {

                double CurrentH = 0, CurrentWindow = 0;
                if (InputFilterType == FILTER_TYPES.LOW)
                {
                    // Shift frequency
                    double FrequencyShifter = (double)(InputCutOffFrequency + (InputTransitionBand / 2)) / InputFS;
                    double Omega = FrequencyShifter * 2 * Math.PI;

                    // Calculate current sample
                    if (n == 0)
                        CurrentH = 2 * FrequencyShifter;
                    else
                        CurrentH = 2 * FrequencyShifter * Math.Sin(n * Omega) / (n * Omega);

                }
                else if (InputFilterType == FILTER_TYPES.HIGH)
                {
                    // Shift frequency
                    double FrequencyShifter = ((double)InputCutOffFrequency + (InputTransitionBand / 2)) / InputFS;
                    double Omega = FrequencyShifter * 2 * Math.PI;

                    // Calculate current sample
                    if (n == 0)
                        CurrentH = 1 - (2 * FrequencyShifter);
                    else
                        CurrentH = -2 * FrequencyShifter * Math.Sin(n * Omega) / (n * Omega);
                }
                else if (InputFilterType == FILTER_TYPES.BAND_PASS)
                {
                    // Shift first and second frequency
                    double FrequencyShifter1 = ((double)InputF1 - (InputTransitionBand / 2)) / InputFS;
                    double Omega1 = FrequencyShifter1 * 2 * Math.PI;

                    double FrequencyShifter2 = ((double)InputF2 + (InputTransitionBand / 2)) / InputFS;
                    double Omega2 = FrequencyShifter2 * 2 * Math.PI;

                    // Calculate current sample
                    if (n == 0)
                        CurrentH = 2 * (FrequencyShifter2 - FrequencyShifter1);
                    else
                        CurrentH = (2 * FrequencyShifter2 * Math.Sin(n * Omega2) / (n * Omega2)) - (2 * FrequencyShifter1 * Math.Sin(n * Omega1) / (n * Omega1));

                }
                else if (InputFilterType == FILTER_TYPES.BAND_STOP)
                {
                    // Shift first and second frequency
                    double FrequencyShifter1 = ((double)InputF1 - (InputTransitionBand / 2)) / InputFS;
                    double Omega1 = FrequencyShifter1 * 2 * Math.PI;

                    double FrequencyShifter2 = ((double)InputF2 + (InputTransitionBand / 2)) / InputFS;
                    double Omega2 = FrequencyShifter2 * 2 * Math.PI;

                    // Calculate current sample
                    if (n == 0)
                        CurrentH = 1 - (2 * (FrequencyShifter2 - FrequencyShifter1));
                    else
                        CurrentH = (2 * FrequencyShifter1 * Math.Sin(n * Omega1) / (n * Omega1)) - (2 * FrequencyShifter2 * Math.Sin(n * Omega2) / (n * Omega2));
                }

                

                if (InputStopBandAttenuation <= 21)
                    CurrentWindow = 1;
                else if (InputStopBandAttenuation <= 44)
                    CurrentWindow = 0.5 + (0.5 * (Math.Cos(2 * Math.PI * n / N)));
                else if (InputStopBandAttenuation <= 53)
                    CurrentWindow = 0.54 + (0.46 * (Math.Cos(2 * Math.PI * n / N)));
                else if (InputStopBandAttenuation <= 74)
                    CurrentWindow = 0.42 + (0.5 * (Math.Cos(2 * Math.PI * n / (N - 1)))) + (0.08 * (Math.Cos(4 * Math.PI * n / (N - 1))));

                // Calculate samples
                // they are symetric 
                SignalSamples[N / 2 - n] = SignalSamples[N / 2 + n] =(float)(CurrentH * CurrentWindow);
                SignalInices[N / 2 - n] = -n;
                SignalInices[N / 2 + n] = n;


            }

            OutputHn = new Signal(new List<float>(SignalSamples), new List<int>(SignalInices), false);
            // Conv coefficients wth input signal
            DirectConvolution Convolution = new DirectConvolution();
            Convolution.InputSignal1 = InputTimeDomainSignal;
            Convolution.InputSignal2 = OutputHn;
            Convolution.Run();
            OutputYn = Convolution.OutputConvolvedSignal;
        }
    }
}
