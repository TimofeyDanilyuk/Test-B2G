using Microsoft.AspNetCore.Mvc;
using Test_B2G.Models;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;


namespace Test_B2G.Controllers
{
    public class PolygonController : Controller
    {
        private static List<Models.Point> points = new List<Models.Point>();
        private static bool polygonDrawn = false;
        private static Models.Point[] polygonPoints;

        public IActionResult Index()
        {
            ViewBag.Points = points;
            ViewBag.PolygonDrawn = polygonDrawn;
            ViewBag.PolygonPoints = polygonPoints;
            return View();
        }

        [HttpPost]
        public IActionResult AddPoint([FromBody] Models.Point point)
        {
            if (polygonDrawn)
            {
                bool isInside = PolygonHelper.IsPointInPolygon(point, points);
                string message = isInside ? "Эта точка находится внутри полигона" : "Эта точка находится вне полигона";
                return Json(new { message });
            }
            else
            {
                points.Add(point);
                return Json(new { });
            }
        }

        [HttpPost]
        public IActionResult DrawPolygon()
        {
            if (points.Count > 2)
            {
                polygonPoints = points.ToArray();
                polygonDrawn = true;
                return Json(new { });
            }
            else
            {
                return Json(new { message = "Поставьте три точки чтобы нарисовать полигон." });
            }
        }
        [HttpPost]
        public IActionResult ClearPolygon()
        {
            points.Clear();
            polygonDrawn = false;
            polygonPoints = null;
            return Json(new { message = "Полигон очищен" });
        }
    }
}
