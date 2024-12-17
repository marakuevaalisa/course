using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinFormsApp1
{
    public class Combination 
    {
        /*
        * Класс содержит комбинацию ходов обеих сторон и пользу обеих сторон
        */

        // Список ходов, составляющих комбинацию
        public List<Move> combinationOfMoves = new List<Move>();

        // Польза (суммарная оценка прибыли) стороны с белыми шашками
        public double WhiteProfit
        {
            get
            {
                return CalculateSumOfProfit("white");
            }
            set { _whiteProfit = value; }
        }

        // Польза (суммарная оценка прибыли) стороны с черными шашками
        public double BlackProfit
        {
            get
            {
                return CalculateSumOfProfit("black");
            }
            set { _blackProfit = value; }
        }

        // Вычисляет суммарную оценку прибыли для указанного цвета
        private double CalculateSumOfProfit(string color)
        {
            double profit = 0.0;
            foreach (Move item in combinationOfMoves)
            {
                if (item.ColorOfMovedChecker == color)
                    profit += item.EstimateOfProfit;
            }
            return profit;
        }

        private double _whiteProfit = 0.0; // Суммарная оценка прибыли для белых шашек
        private double _blackProfit = 0.0; // Суммарная оценка прибыли для черных шашек

      
    }

}
