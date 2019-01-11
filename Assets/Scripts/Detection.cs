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
using Emgu.CV.Cuda;
using System.Drawing;

public class Detection : MonoBehaviour
{

    public VideoCapture webcam;
    public EventHandler eventHandler;
    private Mat image;
    private Mat imageGray;
    public CascadeClassifier frontFaceCascadeClassifier;
    public string pathFaceCascadeClassifier;

    /// <summary>
    /// A table of Rectangle that is gonna contain the region of interest of the detected faces
    /// </summary>
    private Rectangle[] frontFaces;

    private int MIN_FACE_SIZE = 50;
    private int MAX_FACE_SIZE = 200;

    // Use this for initialization
    void Start()
    {
        webcam = new VideoCapture(0);
        image = new Mat();

        pathFaceCascadeClassifier = "C:/Users/atetart/Documents/opencv-master/opencv-master/data/haarcascades/haarcascade_frontalface_default.xml";
        frontFaceCascadeClassifier = new CascadeClassifier(pathFaceCascadeClassifier);
       // webcam.Start();
        webcam.ImageGrabbed += new EventHandler(HandleWebcamQueryFrame);

    }

    // Update is called once per frame
    void Update()
    {
        webcam.Grab();
        //Debug.Log(frontFaces.Length.ToString());

    }

    public void HandleWebcamQueryFrame(object sender, EventArgs e)
    {
        if (webcam.IsOpened) webcam.Retrieve(image);
        if (image.IsEmpty) return;

        imageGray = image.Clone();
        CvInvoke.CvtColor(image, imageGray, ColorConversion.Bgr2Gray);
        if (imageGray.IsEmpty) return;

        frontFaces = frontFaceCascadeClassifier.DetectMultiScale(image: imageGray, scaleFactor: 1.1, minNeighbors: 5, minSize: new Size(MIN_FACE_SIZE, MIN_FACE_SIZE), maxSize: new Size(MAX_FACE_SIZE, MAX_FACE_SIZE));
        Debug.Log(frontFaces.Length.ToString());

        for (int i = 0; i < frontFaces.Length; i++) {

            CvInvoke.Rectangle(image, frontFaces[i], new MCvScalar(0, 180, 0), 5);
            Debug.Log("i: "+ i.ToString());

        }
        CvInvoke.Imshow("Webcam view Gray", image);

    }

    private void OnDestroy()
    {
        webcam.Dispose();
        webcam.Stop();
        CvInvoke.DestroyAllWindows();
    }
}
