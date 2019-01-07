using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
//using System.Drawing;
using System.IO;


public class Test : MonoBehaviour {

    //   public VideoCapture webcam;

    //   // Use this for initialization
    //   void Start () {
    //       Debug.Log("test");

    //       //Init the webcaù
    //       VideoCapture webcam = new VideoCapture(0);

    //}

    //// Update is called once per frame
    //void Update () {
    //       //A Mat image - basic container
    //       Mat image;

    //       //Query the frame the webcam
    //       image = webcam.QueryFrame();

    //       //Invoke the c++ interface function "imshow"
    //       //Display image in a separated window named "Webcam view"
    //       CvInvoke.Imshow("Webcam view", image);

    //       CvInvoke.WaitKey(24);
    //}

    public VideoCapture _webcam;
    public int webcamId;
    // Use this for initialization
    void Start()
    {
        _webcam = new VideoCapture(");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("vid " + _webcam.IsOpened);
        Mat image;
        image = _webcam.QueryFrame();
        CvInvoke.Imshow("Webcam View", image);
        CvInvoke.WaitKey(24);
    }
}
