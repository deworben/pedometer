/*
 * MCEN90032 Sensor Systems.
 * Benjamin De Worsop 913844
 */
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AppManager : MonoBehaviour

{
    [Tooltip("The DataLogger GameObject to be used for data logging purposes.")]
    public GameObject stepCountViewer;
    public int initialise = 0;

    //public FilterButterworth fbwX = new FilterButterworth((float)1.414, (float)5.0, (float)10.0, (int)50);


    //                                              float resonance, float frequencyLow, float frequencyHigh, int sampleRate)
    public FilterButterworth fbwX = new FilterButterworth(5, 50, FilterButterworth.PassType.Highpass, (float)1.414);
    public Pedometer pedometer = new Pedometer((float)0.2);


    void FixedUpdate()
    {

        initialise += 1;


        if (initialise > 2 / 0.02) {
            // Log the time and acceleration data
            string data = System.DateTime.Now.ToString();
            fbwX.Update(Input.acceleration.x);
            // print(fbwX.Value);
            pedometer.update(fbwX.Value);
            //data = data + "   " + pedometer.numSteps;
            //data = data + "   " + Input.acceleration.x;
            //stepCountViewer.GetComponent<UnityEngine.UI.Text>().text = data;

            stepCountViewer.GetComponent<UnityEngine.UI.Text>().text = "Currently at " + pedometer.numSteps + " steps";


            //data += "," + Input.acceleration.x + "," + Input.acceleration.y + "," + Input.acceleration.z + "," + fbwX.Value + "," + fbwY.Value + "," + fbwZ.Value + ",";
            data += "," + Input.acceleration.x + "," + Input.acceleration.y + "," + Input.acceleration.z + ",";
            //print(data);
        }

    }

}
