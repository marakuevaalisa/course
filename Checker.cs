using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WinFormsApp1
{
    public class Checker
    {
        public Checker() { }

        public Checker(string location, string color)
        {
            Location = location;
            Color = color;
        }

        // Цвет шашки
        public string Color
        {
            get { return _color; }
            set { _color = value; }
        }

        // Положение шашки на доске
        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        // Координаты шашки
        public Point Coord
        {
            get { return _coord = ParseStringToCoord(Location); }
            set { _coord = value; }
        }



        private string _color; // Цвет шашки
        private string _location; // Положение шашки на доске
        private Point _coord; // Координаты шашки

        // Преобразование строки положения в координаты
        private Point ParseStringToCoord(string location)
        {
            Point tempPoint = new Point();
            tempPoint.X = (int)location[0] - 64;
            tempPoint.Y = (int)location[1] - 48;
            return tempPoint;
        }
    }



}
