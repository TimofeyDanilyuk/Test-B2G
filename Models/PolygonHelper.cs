using System.Collections.Generic;
namespace Test_B2G.Models
{
    public class PolygonHelper
    {
        public static bool IsPointInPolygon(Point point, List<Point> polygon)
        {
            int polygonLength = polygon.Count;
            bool inside = false;

            Point endPoint = polygon[polygonLength - 1];

            foreach (var startPoint in polygon)
            {
                if ((startPoint.Y <= point.Y && point.Y < endPoint.Y || endPoint.Y <= point.Y && point.Y < startPoint.Y) &&
                    (point.X < (endPoint.X - startPoint.X) * (point.Y - startPoint.Y) / (endPoint.Y - startPoint.Y) + startPoint.X))
                {
                    inside = !inside;
                }
                endPoint = startPoint;
            }
            return inside;
        }
    }
}
