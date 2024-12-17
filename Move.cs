using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Newtonsoft.Json;

namespace WinFormsApp1
{
    public class Move :  ICloneable
    {
        // Поля класса Move
        private string _locationOfdeletedChecker;
        private bool _isAttack = false;// флаг атакующего хода
        private string _moveFrom;//начальная позиция шашки
        private string _moveTo;//конечной позициях шашки
        private double _estimateOfProfit;// оценка прибыли от хода
        private string _colorOfMovedChecker;//цвет двигающейся шашки

        // Конструкторы класса Move
        public Move() { }

        public Move(string moveFrom, string moveTo, string color)
        {
            MoveFrom = moveFrom;
            MoveTo = moveTo;
            ColorOfMovedChecker = color;
        }

        // Список атакующих ходов
        public List<Move> lstOfAttackMoves = new List<Move>();

        // Свойства класса Move
        public string MoveFrom
        {
            get { return _moveFrom; }
            set { _moveFrom = value; }
        }

        public string MoveTo
        {
            get { return _moveTo; }
            set { _moveTo = value; }
        }

        public double EstimateOfProfit
        {
            get { return _estimateOfProfit; }
            set { _estimateOfProfit = value; }
        }

        public string ColorOfMovedChecker
        {
            get { return _colorOfMovedChecker; }
            set { _colorOfMovedChecker = value; }
        }

        public bool IsAttack
        {
            get { return _isAttack; }
            set { _isAttack = value; }
            
        }
        
        [JsonIgnore]
        public string LocationOfdeletedChecker
        {
            get
            {
                // Расчет координат удаляемой шашки
                _locationOfdeletedChecker = GetLocation(new Point((ParseStringToCoord(MoveFrom).X + ParseStringToCoord(MoveTo).X) / 2,
                    (ParseStringToCoord(MoveFrom).Y + ParseStringToCoord(MoveTo).Y) / 2));
                return _locationOfdeletedChecker;
            }
            set { _locationOfdeletedChecker = value; }
        }

        // Методы класса Move

        // реализует клонирование объекта `Move`, чтобы можно было создавать копии ходов для анализа и принятия решений.
        public object Clone()
        {
            Move cloneMove = new Move();
            cloneMove.ColorOfMovedChecker = this.ColorOfMovedChecker;
            cloneMove.EstimateOfProfit = this.EstimateOfProfit;
            cloneMove.IsAttack = this.IsAttack;
            cloneMove.LocationOfdeletedChecker = this.LocationOfdeletedChecker;
            cloneMove.MoveFrom = this.MoveFrom;
            cloneMove.MoveTo = this.MoveTo;
            cloneMove.EstimateOfProfit = this.EstimateOfProfit;
            return cloneMove;
        }

        // Добавление атакующего хода в список
        public void AddMoveToListOfAttack(string locationFrom, string locationTo)
        {
            Move tmpMove = new Move(locationFrom, locationTo, this.ColorOfMovedChecker);
            tmpMove.EstimateOfProfit = this.EstimateOfProfit;
            tmpMove.IsAttack = true;
            lstOfAttackMoves.Add(tmpMove);
        }

        // Преобразование строки координаты в точку
        private Point ParseStringToCoord(string location)
        {
            Point tempPoint = new Point();
            tempPoint.X = (int)location[0] - 64;
            tempPoint.Y = (int)location[1] - 48;
            return tempPoint;
        }

        // Получение строки координаты из точки
        private string GetLocation(Point curPoint)
        {
            string resLocation;
            char tempCharX = (char)(curPoint.X + 64);
            char tempCharY = (char)(curPoint.Y + 48);
            resLocation = tempCharX.ToString() + tempCharY.ToString();
            return resLocation;
        }
        
        }

    

}
