using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//public class FilterButterworth
//{
//    /// <summary>
//    /// rez amount, from sqrt(2) to ~ 0.1
//    /// </summary>
//    private readonly float resonance;
//    // private readonly float frequency;
//    private readonly int sampleRate;
//    private readonly float frequencyLow;
//    private readonly float frequencyHigh;

//    private readonly float c, a1, a2, a3, b1, b2;
//    private readonly float LPc, LPa1, LPa2, LPa3, LPb1, LPb2;

//    /// <summary>
//    /// Array of input values, latest are in front
//    /// </summary>
//    private float[] inputHistory = new float[2];

//    /// <summary>
//    /// Array of output values, latest are in front
//    /// </summary>
//    private float[] outputHistory = new float[3];

//    public FilterButterworth(float resonance, float frequencyLow, float frequencyHigh, int sampleRate)
//    {
//        this.resonance = resonance;
//        // this.frequency = frequency;

//        this.frequencyHigh = frequencyHigh;
//        this.frequencyLow = frequencyLow;
//        this.sampleRate = sampleRate;



//        LPc = 1.0f / (float)Math.Tan(Math.PI * frequencyHigh / sampleRate);
//        LPa1 = 1.0f / (1.0f + resonance * LPc + LPc * LPc);
//        LPa2 = 2f * LPa1;
//        LPa3 = LPa1;
//        LPb1 = 2.0f * (1.0f - LPc * LPc) * LPa1;
//        LPb2 = (1.0f - resonance * LPc + LPc * LPc) * LPa1;


//        c = (float)Math.Tan(Math.PI * frequencyLow / sampleRate);
//                a1 = 1.0f / (1.0f + resonance * c + c * c);
//                a2 = -2f * a1;
//                a3 = a1;
//                b1 = 2.0f * (c * c - 1.0f) * a1;
//                b2 = (1.0f - resonance * c + c * c) * a1;
//    }


//    public void Update(float newInput)
//    {
//        //High pass filtered
//        float newOutput = a1 * newInput + a2 * this.inputHistory[0] + a3 * this.inputHistory[1] - b1 * this.outputHistory[0] - b2 * this.outputHistory[1];

//        //Low pass filtered
//        newOutput = LPa1 * newOutput + LPa2 * this.inputHistory[0] + LPa3 * this.inputHistory[1] - LPb1 * this.outputHistory[0] - LPb2 * this.outputHistory[1];


//        this.inputHistory[1] = this.inputHistory[0];
//        this.inputHistory[0] = newInput;

//        this.outputHistory[2] = this.outputHistory[1];
//        this.outputHistory[1] = this.outputHistory[0];
//        this.outputHistory[0] = newOutput;
//    }

//    public float Value
//    {
//        get { return this.outputHistory[0]; }
//    }
//}
public class FilterButterworth
{
    /// <summary>
    /// rez amount, from sqrt(2) to ~ 0.1
    /// </summary>
    private readonly float resonance;

    private readonly float frequency;
    private readonly int sampleRate;
    private readonly PassType passType;

    private readonly float c, a1, a2, a3, b1, b2;

    /// <summary>
    /// Array of input values, latest are in front
    /// </summary>
    private float[] inputHistory = new float[2];

    /// <summary>
    /// Array of output values, latest are in front
    /// </summary>
    private float[] outputHistory = new float[3];

    public FilterButterworth(float frequency, int sampleRate, PassType passType, float resonance)
    {
        this.resonance = resonance;
        this.frequency = frequency;
        this.sampleRate = sampleRate;
        this.passType = passType;

        switch (passType)
        {
            case PassType.Lowpass:
                c = 1.0f / (float)Math.Tan(Math.PI * frequency / sampleRate);
                a1 = 1.0f / (1.0f + resonance * c + c * c);
                a2 = 2f * a1;
                a3 = a1;
                b1 = 2.0f * (1.0f - c * c) * a1;
                b2 = (1.0f - resonance * c + c * c) * a1;
                break;
            case PassType.Highpass:
                c = (float)Math.Tan(Math.PI * frequency / sampleRate);
                a1 = 1.0f / (1.0f + resonance * c + c * c);
                a2 = -2f * a1;
                a3 = a1;
                b1 = 2.0f * (c * c - 1.0f) * a1;
                b2 = (1.0f - resonance * c + c * c) * a1;
                break;
        }
    }

    public enum PassType
    {
        Highpass,
        Lowpass,
    }

    public void Update(float newInput)
    {
 
        float newOutput = a1 * newInput + a2 * this.inputHistory[0] + a3 * this.inputHistory[1] - b1 * this.outputHistory[0] - b2 * this.outputHistory[1];

        this.inputHistory[1] = this.inputHistory[0];
        this.inputHistory[0] = newInput;

        this.outputHistory[2] = this.outputHistory[1];
        this.outputHistory[1] = this.outputHistory[0];
        this.outputHistory[0] = newOutput;
    }

    public float Value
    {
        get { return this.outputHistory[0]; }
    }
}