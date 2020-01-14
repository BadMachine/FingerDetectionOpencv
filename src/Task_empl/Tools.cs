using System;
using System.Collections.Generic;
using System.Text;
using OpenCvSharp;

namespace Task_empl
{
   public class Tools
    {

        public static void soften(ref Mat input_image)
        {

            Cv2.MedianBlur(input_image, input_image, 7);

            OpenCvSharp.Point morph_point = new OpenCvSharp.Point(1, 1);
            Mat erodeElement = Cv2.GetStructuringElement(shape: MorphShapes.Rect, new OpenCvSharp.Size(3, 3));
            Mat dilateElement = Cv2.GetStructuringElement(shape: MorphShapes.Rect, new OpenCvSharp.Size(3, 3));
            Cv2.Erode(input_image, input_image, erodeElement, morph_point, 5, borderType: BorderTypes.Reflect);
            Cv2.Dilate(input_image, input_image, dilateElement, morph_point, 5, borderType: BorderTypes.Reflect);
        }


        public static int ptDist(ref OpenCvSharp.Point p1, ref OpenCvSharp.Point p2) // rms distance. Среднеквадратичное расстояние между точками
        {
            var dx = p2.X - p1.X;
            var dy = p2.Y - p1.Y;
            return (int)Math.Sqrt(dx * dx + dy * dy);
        }



        //Removing closest points. Удаляем ближайшие точки по принципу из ближайших точек выбираем самую дальнюю от ценрта ладони
        public static void SimplifyNeighbors(ref List<OpenCvSharp.Point> handPTS, ref OpenCvSharp.Point center_of_palm) 
        {
            for (int it = 0; it < handPTS.Count; it++)
            {

                for (int j = it + 1; j < handPTS.Count;)
                {
                    var pointHere = handPTS[it];
                    var pointThere = handPTS[j];

                    var PtFirst_to_palm = ptDist(ref center_of_palm, ref pointHere);
                    var PTsec_to_palm = ptDist(ref center_of_palm, ref pointThere);

                    var Exdistance = ptDist(ref pointHere, ref pointThere);

                    if (Exdistance <= 25)
                    {
                        if (PTsec_to_palm > PtFirst_to_palm)
                        {
                            handPTS.RemoveAt(it);
                        }
                        else
                        {
                            handPTS.RemoveAt(j);
                        }

                    }
                    else
                    {
                        j += 1;
                    }
                }
            }

        }




        public static OpenCvSharp.Point[] FindBiggestContour(ref OpenCvSharp.Point[][] contours)
        {

            int biggest_solid_contourID;
            if (contours.Length > 1)
            {
                biggest_solid_contourID = -1;
                double biggest_contour = 0.0;

                for (int i = 0; i < contours.Length; i++)
                {
                    double contour = Cv2.ContourArea(contours[i], false);
                    if (contour > biggest_contour)
                    {
                        biggest_contour = contour;
                        biggest_solid_contourID = i;
                    }
                }

            }
            else
            {
                biggest_solid_contourID = 0;
            }

            return contours[biggest_solid_contourID];
        }



        public static void RemoveWristPoints(ref List<OpenCvSharp.Point> handPTS, int frame_width, int frame_height) 
        {

            for (int it = 0; it < handPTS.Count;)
            {
                if (handPTS[it].X > frame_width - 10 || handPTS[it].X < 10 || handPTS[it].Y > frame_height - 10 || handPTS[it].Y < 10)
                {
                    handPTS.RemoveAt(it);
                }
                else it++;
            }

        }








    }
}
