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

public class Test : MonoBehaviour
{

    public VideoCapture webcam;
    public enum BlurType {Blur, Median, Gaussian};

    // Use this for initialization
    void Start()
    {
        Debug.Log("test");

        //Init the webcam
        //webcam = new VideoCapture("C:/Users/atetart/Videos/DK.mp4");
        webcam = new VideoCapture(0);

    }

    // Update is called once per frame
    void Update()
    {
        //A Mat image - basic container
        Mat image;

        //Query the frame the webcam
        image = webcam.QueryFrame();

        //Flip the image
        Mat flippedImage = image.Clone();
        CvInvoke.Flip(image, flippedImage, FlipType.Horizontal);


        Mat imgGray = image.Clone();
        Mat imgHSV = image.Clone();

        CvInvoke.CvtColor(image, imgGray, ColorConversion.Bgr2Gray);
        CvInvoke.CvtColor(image, imgHSV, ColorConversion.Bgr2Hsv);

        Mat imgBlur = image.Clone();


        switch (1) {
            case 0:
                CvInvoke.Blur(image, imgBlur, new Size(2, 2), new Point(-1, 1));
                break;

            case 1:
                CvInvoke.MedianBlur(image, imgBlur, 3);
                break;
            case 2:
                CvInvoke.GaussianBlur(image, imgBlur, new Size(2, 2), 2);
                break;
        }

        //On va faire ressortir une seule couleur (ici vert)
        Image<Hsv, byte> imageHSV = imgHSV.ToImage<Hsv, byte>();
        Hsv teinteRougeBas = new Hsv(60, 100, 0);
        Hsv teinteRougeHaut = new Hsv(80, 255, 255);

        Image<Gray, byte> imageGray = imageHSV.InRange(teinteRougeBas, teinteRougeHaut);

        //Invoke the c++ interface function "imshow"
        //Display image in a separated window named "Webcam view"

        CvInvoke.Imshow("Webcam view classic", image);
        //CvInvoke.Imshow("Webcam view flipped", flippedImage);
        //CvInvoke.Imshow("Webcam view Gray", imgGray);
        //CvInvoke.Imshow("Webcam view HSV", imgHSV);
        //CvInvoke.Imshow("Webcam view Blur", imgBlur);
        CvInvoke.Imshow("Webcam view Blur", imageGray);

        //for(int i=0; i<100; i++) {

        //    CvInvoke.Imshow("Webcam view HSV" + i.ToString(), flippedImage);

        //}

        CvInvoke.WaitKey(24);


    }

    private void OnDestroy()
    {
        webcam.Dispose();
        CvInvoke.DestroyAllWindows();
    }
}
