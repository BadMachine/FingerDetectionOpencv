using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using OpenCvSharp;
using System.Numerics;

public static class Color
{
    public static readonly Scalar blue = new Scalar(255, 38, 0);
    public static readonly Scalar red = new Scalar(0, 12, 255);
    public static readonly Scalar unfiltered = new Scalar(0, 212, 255);

}



namespace Task_empl
{


    public partial class Task : Form
    {
        public Task()
        {
            InitializeComponent();
        }



        private void OpenImage_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog Openfile = new OpenFileDialog();
            if (Openfile.ShowDialog() == DialogResult.OK)
            {

                Mat input = new Mat(Openfile.FileName, ImreadModes.Color);
                Mat grayScale = new Mat();

                Tools.soften(ref input);
                OpenCvSharp.Point[][] contours;
                HierarchyIndex[] hierarchie_indexes;

                
                Cv2.CvtColor(input, grayScale, ColorConversionCodes.BGRA2GRAY); //Canny edge detector to remove soft edges. Убираем "мягкие переходы после блюра
                Cv2.Canny(grayScale, grayScale, 50, 150, 3);
                Cv2.FindContours(grayScale, out contours, out hierarchie_indexes, mode: RetrievalModes.External, method: ContourApproximationModes.ApproxSimple);
                Cv2.DrawContours(input, contours, -1, color: Color.blue, 2, lineType: LineTypes.Link8, hierarchy: hierarchie_indexes, maxLevel: int.MaxValue);

                var handContour = Tools.FindBiggestContour(ref contours); // looking for biggest contour in image. Находим наибольший контур на изображении, выбирая максимальный по размеру контейнер с координатами контура
                var hullPoints = Cv2.ConvexHull(handContour); 



                List<List<OpenCvSharp.Point>> all_pts = new List<List<OpenCvSharp.Point>>();
                List<OpenCvSharp.Point> exclusive_points = new List<OpenCvSharp.Point>();

                
                for (int it = 0; it < hullPoints.Length; it++)
                {
                    exclusive_points.Add(hullPoints[it]);
                }

                all_pts.Add(exclusive_points);


                //Cv2.Polylines(input, all_pts, true, color: new Scalar(0, 12, 255)); // looking for outside controu of whole palm. Находим внешний контур ко всей ладони

                List<OpenCvSharp.Point> cnt = new List<OpenCvSharp.Point>();
                for (int it = 0; it < handContour.Length; it++)
                {
                    cnt.Add(handContour[it]);
                }

                // Поиск центра ладони по ближайшей средней точке между min экстремумами контура ладони
                // Looking for center point of palm using minimal extremums mean values

                Rect bounding_rectangle = Cv2.BoundingRect(hullPoints);

                OpenCvSharp.Point center_of_palm = new OpenCvSharp.Point(
                (bounding_rectangle.TopLeft.X + bounding_rectangle.BottomRight.X) / 2,
                (bounding_rectangle.TopLeft.Y + bounding_rectangle.BottomRight.Y) / 2
                );


                //Cv2.Circle(input, center: center_of_palm, radius: 6, color: new Scalar(17, 255, 21), lineType: LineTypes.Link4, thickness: 8);


                //for (int i = 0; i < hullPoints.Length; i++)
                //{

                //    Cv2.Circle(input, center: hullPoints[i], radius: 6, color: Color.unfiltered);
                //}

                Tools.SimplifyNeighbors(ref exclusive_points, ref center_of_palm);
                Tools.RemoveWristPoints(ref exclusive_points, input.Cols, input.Rows); // удаляем точки, сопряженные с краем изображения, в данном случае запястья
              

               for (int i = 0; i < exclusive_points.Count; i++)
               {
                        Cv2.Circle(input, center: exclusive_points[i], radius: 2, color: Color.red, thickness: 6);
               }

                Cv2.Resize(input, input, dsize: new OpenCvSharp.Size(640, 480));
                Viewport.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(input);       
            }

        }


    }
}
