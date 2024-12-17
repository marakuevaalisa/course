using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;
using Newtonsoft.Json;

namespace WinFormsApp1
{
    public partial class GameForm : Form
    {
        // Константы для цветов и имен файлов изображений шашек
        const string WHITE = "white";
        const string BLACK = "black";
        Image WHITE_CHECKER;
        Image BLACK_CHECKER;
        DateTime start;
        Board myBoard; // Игровая доска

        public Dictionary<string, Panel>
            dictOfPanels = new Dictionary<string, Panel>(); // Словарь для связи имен клеток и панелей на форме

        private string log;

        public GameForm()
        {
            InitializeComponent();
            myBoard = new Board(); // Создание экземпляра игровой доски
            // Добавление панелей в словарь dictOfPanels

            #region DictOfCells.Add();

            dictOfPanels.Add("A1", A1);
            dictOfPanels.Add("A2", A2);
            dictOfPanels.Add("A3", A3);
            dictOfPanels.Add("A4", A4);
            dictOfPanels.Add("A5", A5);
            dictOfPanels.Add("A6", A6);
            dictOfPanels.Add("A7", A7);
            dictOfPanels.Add("A8", A8);
            dictOfPanels.Add("B1", B1);
            dictOfPanels.Add("B2", B2);
            dictOfPanels.Add("B3", B3);
            dictOfPanels.Add("B4", B4);
            dictOfPanels.Add("B5", B5);
            dictOfPanels.Add("B6", B6);
            dictOfPanels.Add("B7", B7);
            dictOfPanels.Add("B8", B8);
            dictOfPanels.Add("C1", C1);
            dictOfPanels.Add("C2", C2);
            dictOfPanels.Add("C3", C3);
            dictOfPanels.Add("C4", C4);
            dictOfPanels.Add("C5", C5);
            dictOfPanels.Add("C6", C6);
            dictOfPanels.Add("C7", C7);
            dictOfPanels.Add("C8", C8);
            dictOfPanels.Add("D1", D1);
            dictOfPanels.Add("D2", D2);
            dictOfPanels.Add("D3", D3);
            dictOfPanels.Add("D4", D4);
            dictOfPanels.Add("D5", D5);
            dictOfPanels.Add("D6", D6);
            dictOfPanels.Add("D7", D7);
            dictOfPanels.Add("D8", D8);
            dictOfPanels.Add("E1", E1);
            dictOfPanels.Add("E2", E2);
            dictOfPanels.Add("E3", E3);
            dictOfPanels.Add("E4", E4);
            dictOfPanels.Add("E5", E5);
            dictOfPanels.Add("E6", E6);
            dictOfPanels.Add("E7", E7);
            dictOfPanels.Add("E8", E8);
            dictOfPanels.Add("F1", F1);
            dictOfPanels.Add("F2", F2);
            dictOfPanels.Add("F3", F3);
            dictOfPanels.Add("F4", F4);
            dictOfPanels.Add("F5", F5);
            dictOfPanels.Add("F6", F6);
            dictOfPanels.Add("F7", F7);
            dictOfPanels.Add("F8", F8);
            dictOfPanels.Add("G1", G1);
            dictOfPanels.Add("G2", G2);
            dictOfPanels.Add("G3", G3);
            dictOfPanels.Add("G4", G4);
            dictOfPanels.Add("G5", G5);
            dictOfPanels.Add("G6", G6);
            dictOfPanels.Add("G7", G7);
            dictOfPanels.Add("G8", G8);
            dictOfPanels.Add("H1", H1);
            dictOfPanels.Add("H2", H2);
            dictOfPanels.Add("H3", H3);
            dictOfPanels.Add("H4", H4);
            dictOfPanels.Add("H5", H5);
            dictOfPanels.Add("H6", H6);
            dictOfPanels.Add("H7", H7);
            dictOfPanels.Add("H8", H8);

            #endregion

            DrawBoard(myBoard.dictOfCells);
            start = DateTime.Now;
            NewGame();

        }



        public void DrawBoard(Dictionary<string, Cell> dictOfCells)
        {
            foreach (KeyValuePair<string, Cell> item in dictOfCells)
            {
                dictOfPanels[item.Value.CellName].BackgroundImage = null;
                if (item.Value.CellColor == WHITE) dictOfPanels[item.Value.CellName].BackColor = Color.SaddleBrown;
                else dictOfPanels[item.Value.CellName].BackColor = Color.Snow;
                dictOfPanels[item.Value.CellName].BorderStyle = BorderStyle.FixedSingle;
            }
        }

        // вставляем картинки в кнопки
        public void DrawChecker(string panelName, string color)
        {
            WHITE_CHECKER = new Bitmap(new Bitmap(@"D:\Курсовая\w.png"), new Size(54, 54));
            BLACK_CHECKER = new Bitmap(new Bitmap(@"D:\Курсовая\b.png"), new Size(54, 54));
            Image im;
            if (color == WHITE) im = WHITE_CHECKER;
            else im = BLACK_CHECKER;
            dictOfPanels[panelName].BackgroundImage = im;
            myBoard.dictOfCells[panelName].IsChecker = true;
            myBoard.dictOfCells[panelName].CheckColor = color;
        }

        public void DrawCheckers(Dictionary<string, Cell> dictOfCells) // наносим шашки на доску
        {
            foreach (KeyValuePair<string, Cell> item in dictOfCells)
            {
                if (item.Value.coord.Y < 4 && item.Value.CellColor == BLACK) DrawChecker(item.Value.CellName, WHITE);
                if (item.Value.coord.Y > 5 && item.Value.CellColor == BLACK) DrawChecker(item.Value.CellName, BLACK);
            }

            textBox1.Text = "";
        }

        public void NewGame()
        {
            DBFunctions.SaveLog("");
            // открытие файла и запись результата
            using (StreamWriter sw = new StreamWriter("Recording_moves", true))
            {
                sw.WriteLine();
                sw.WriteLine("Новая игра!");
                sw.Close();
            }

            B8.Visible = true;
            myBoard = null;
            myBoard = new Board();
            DrawBoard(myBoard.dictOfCells);
            DrawCheckers(myBoard.dictOfCells);
            foreach (KeyValuePair<string, Cell> item in myBoard.dictOfCells) // перепись всех шашек
            {
                Checker curChecker = new Checker();
                if (item.Value.IsChecker)
                {
                    curChecker.Color = item.Value.CheckColor;
                    curChecker.Location = item.Key;
                    if (curChecker.Color == WHITE)
                    {
                        myBoard.lstOfWhiteChecker.Add(curChecker);
                    }
                    else
                    {
                        myBoard.lstOfBlackChecker.Add(curChecker);
                    }
                }
            }

            textBox1.Text = "";
        }
        

       //кнопка создание новой игры
        private void begin_Click(object sender, EventArgs e)
        {
            NewGame();
        }
       //кнопка выход
       
       private async void exit_Click(object sender, EventArgs e)
       {
           var gameState = SerializeGameState();

           // Сохраняем состояние игры асинхронно
           await DBFunctions.SaveGameState(Form1.name, gameState);

           DialogResult result = MessageBox.Show("Хотите выйти?", "Выход", MessageBoxButtons.YesNo);

           if (result == System.Windows.Forms.DialogResult.Yes)
           {
               this.Close();
           }
       }

        
        private string SerializeGameState()
        {
            try
            {
                var state = new
                {
                    WhiteCheckers = myBoard.lstOfWhiteChecker.Select(c => new { c.Location, c.Color }),
                    BlackCheckers = myBoard.lstOfBlackChecker.Select(c => new { c.Location, c.Color }),
                    CurrentMove = myBoard.currentMove,
                    GameLog = textBox1.Text
                };

                return JsonConvert.SerializeObject(state);
            }
            catch (JsonSerializationException jsonEx)
            {
                MessageBox.Show($"Ошибка сериализации JSON: {jsonEx.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении состояния игры: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }


        public void LoadGameState(string gameState)
        {
            try
            {
                var state = JsonConvert.DeserializeObject<dynamic>(gameState);

                myBoard.lstOfWhiteChecker = JsonConvert.DeserializeObject<List<Checker>>(state["WhiteCheckers"].ToString());
                myBoard.lstOfBlackChecker = JsonConvert.DeserializeObject<List<Checker>>(state["BlackCheckers"].ToString());

                foreach (var checker in myBoard.lstOfWhiteChecker)
                {
                    DrawChecker(checker.Location, checker.Color);
                }

                foreach (var checker in myBoard.lstOfBlackChecker)
                {
                    DrawChecker(checker.Location, checker.Color);
                }
                
                textBox1.Text = state["GameLog"]?.ToString() ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке состояния игры: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool IsValidCheckMove(string locationFrom, string locationTo)
        {
            bool flagValidCheckMove = false;
            List<Move> tmpListOfMoves = new List<Move>();
            myBoard.GetListOfMoves(tmpListOfMoves, myBoard.lstOfWhiteChecker, WHITE);
            foreach (Move move in tmpListOfMoves)
            {
                if (move.MoveFrom == locationFrom
                &&
                move.MoveTo == locationTo)
                {
                    flagValidCheckMove = true;
                    break;
                }
                if (move.IsAttack)
                    foreach (Move attackMove in move.lstOfAttackMoves)
                    {
                        if (attackMove.MoveFrom == locationFrom
                        &&
                        attackMove.MoveTo == locationTo)
                        {
                            flagValidCheckMove = true;
                            break;
                        }
                    }
            }
            return flagValidCheckMove;
        }

        private bool IsValidClick(string location)
        {
            bool flagValidClick = false;
            if (myBoard.dictOfCells[location].CheckColor == WHITE)
            {
                flagValidClick = true;
            }
            return flagValidClick;
        }

        private void ComputerMove()
        {
            Move compMove = myBoard.ComputerMove();
            if (compMove.MoveFrom == null)
            {
                MessageBox.Show("Конец партии. Шашки компьютера не имеют возможности хода.");
                DialogResult result = MessageBox.Show("Начать новую партию?", "", MessageBoxButtons.YesNo);

                if (result == System.Windows.Forms.DialogResult.No)
                {
                    // EndGame();
                    this.Close();

                }
                else
                {
                    NewGame();
                }
            }
            else
            {
                if (compMove.IsAttack)
                {
                    foreach (Move item in compMove.lstOfAttackMoves)
                    {
                        Move(item);
                    }
                }
                else
                {
                    Move(compMove);
                }
            }
        }

        private void Move(Move curMove)
        {
            myBoard.MoveImitation(curMove, myBoard.GetListOfCheckersByColor(curMove.ColorOfMovedChecker));
            DrawChecker(curMove.MoveTo, curMove.ColorOfMovedChecker);
            dictOfPanels[curMove.MoveFrom].BackgroundImage = null;
            dictOfPanels[curMove.MoveFrom].BorderStyle = BorderStyle.FixedSingle;
            dictOfPanels[curMove.MoveFrom].BackColor = Color.LightCyan;
            if (curMove.IsAttack)
            {
                KillChecker(curMove.LocationOfdeletedChecker);
            }
            MarkMovedCells(curMove);
        }

        private async void MarkMovedCells(Move curMove)
        {
            if (myBoard.MoveMarkedLocationFrom != null)
            {
                dictOfPanels[myBoard.MoveMarkedLocationFrom].BorderStyle = BorderStyle.None;
                dictOfPanels[myBoard.MoveMarkedLocationTo].BorderStyle = BorderStyle.None;
            }
            dictOfPanels[curMove.MoveFrom].BorderStyle = BorderStyle.Fixed3D;
            dictOfPanels[curMove.MoveTo].BorderStyle = BorderStyle.Fixed3D;
            dictOfPanels[curMove.MoveFrom].BackColor = Color.LightCyan;
            dictOfPanels[curMove.MoveTo].BackColor = Color.LightCyan;

            myBoard.MoveMarkedLocationFrom = curMove.MoveFrom;
            myBoard.MoveMarkedLocationTo = curMove.MoveTo;
            if (curMove.ColorOfMovedChecker == "white")
            {
                textBox1.Text += "Человек - " + curMove.MoveFrom + ":" + curMove.MoveTo + ", ";
            }
            else
            {
                textBox1.Text += "Компьютер - " + curMove.MoveFrom + ":" + curMove.MoveTo + ", ";
            }
            textBox1.Text += curMove.MoveFrom + ":" + curMove.MoveTo + ", ";
            await DBFunctions.UpdateLog(textBox1.Text);
            
            using (StreamWriter sw = new StreamWriter("Recording_moves.txt", true))
            {
                if (curMove.ColorOfMovedChecker == "white")
                {
                    sw.WriteLine("Человек - " + curMove.MoveFrom + ":" + curMove.MoveTo + ", ");
                }
                else
                {
                    sw.WriteLine("Компьютер - " + curMove.MoveFrom + ":" + curMove.MoveTo + ", ");
                }
                sw.Close();
            }
        }
        

        //вывод на экран победитела
        private void KillChecker(string location)
        {
            dictOfPanels[location].BackgroundImage = null;
            myBoard.dictOfCells[location].IsChecker = false;
            myBoard.dictOfCells[location].CheckColor = null;
            string winner = IsEndOfGame();

           if (!string.IsNullOrEmpty(winner))
            {
                MessageBox.Show($"Игра окончена! Победитель: {winner}", "Конец партии");
                
            }

        }

        //выявляет кто победил и записывает в файл
        private string IsEndOfGame()
        {
            string winner = null;

            if (myBoard.lstOfWhiteChecker.Count == 0)
            {
                // Победил игрок
                winner = "Игрок";

                // Открытие файла и запись результата
                using (StreamWriter sw = new StreamWriter("Recording_moves.txt", true))
                {
                    sw.WriteLine("Победа игрока!");
                }
            }
            else if (myBoard.lstOfBlackChecker.Count == 0)
            {
                // Победил компьютер
                winner = "Компьютер";
                // Открытие файла и запись результата
                using (StreamWriter sw = new StreamWriter("Recording_moves.txt", true))
                {
                    sw.WriteLine("Победа компьютера!");
                }
            }
            DBFunctions.SaveEnd(Form1.name, winner, start);

            return winner;
        }


        private void EndGame()
        {
            Close();
        }
       
        private void ClickOnCell(object sender, MouseEventArgs e)
        {
            bool flagMultiKill = false;
            Panel tempPanel = sender as Panel;
            if (!myBoard.IsStartMove)
            {
                if (myBoard.dictOfCells[tempPanel.Name.ToString()].IsChecker
                    && IsValidClick(tempPanel.Name.ToString()))
                {
                    myBoard.currentMove.MoveFrom = tempPanel.Name.ToString();
                    myBoard.currentMove.ColorOfMovedChecker = myBoard.dictOfCells[tempPanel.Name.ToString()].CheckColor;
                    myBoard.IsStartMove = true;
                    tempPanel.BorderStyle = BorderStyle.Fixed3D;
                    tempPanel.BackColor = Color.Yellow;
                }
            }
            else
            {
                myBoard.currentMove.MoveTo = tempPanel.Name.ToString();
                if (myBoard.currentMove.MoveTo == myBoard.currentMove.MoveFrom)
                {
                    myBoard.IsStartMove = false;
                    tempPanel.BorderStyle = BorderStyle.None;
                    tempPanel.BackColor = Color.LightCyan;
                }
                else
                {
                    myBoard.IsAttack(myBoard.currentMove);
                    if (myBoard.currentMove.IsAttack)
                        flagMultiKill = myBoard.IsAttackElse(myBoard.currentMove.Clone() as Move);
                    if (IsValidCheckMove(myBoard.currentMove.MoveFrom, myBoard.currentMove.MoveTo))
                    {
                        Move(myBoard.currentMove);
                        string winner = IsEndOfGame();
                        if (!string.IsNullOrEmpty(winner))
                        {
                            MessageBox.Show($"Игра окончена! Победитель: {winner}", "Конец партии");
                            DialogResult result = MessageBox.Show("Начать новую партию?", "", MessageBoxButtons.YesNo);

                            if (result == DialogResult.No)
                            {
                                EndGame();
                            }
                            else
                            {
                                NewGame();
                            }
                        }
                        else
                        {
                            myBoard.IsStartMove = false;
                            if (!flagMultiKill)
                            {
                                
                                ComputerMove();
                            }
                            
                        }
                    }
                }
            }
        }


        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void A7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void F5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void D4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }
    }
    }
    