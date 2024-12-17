using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static WinFormsApp1.Board;
using static WinFormsApp1.Cell;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {

        public static string name = "";
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                name = textBox1.Text;
                string password = textBox2.Text;
                string hashedPassword = DBFunctions.GetHashedPassword(name);

                if (hashedPassword != null && BCrypt.Net.BCrypt.Verify(password, hashedPassword))
                {
                    // Авторизация успешна
                    try
                    {
                        // Открытие файла и запись результата
                        using (System.IO.StreamWriter sw = new System.IO.StreamWriter("Recording_moves.txt", true))
                        {
                            sw.WriteLine();
                            sw.WriteLine(DateTime.Now + " Успешная авторизация: " + name);
                            sw.Close();
                        }

                        GameForm gForm = new GameForm();
                        gForm.ShowDialog();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else
                {
                    
                    MessageBox.Show("Неправильное имя пользователя или пароль!");
                }
            }
        }


        private async void Form1_Load(object sender, EventArgs e)
        {
            string lastGameState = await DBFunctions.GetLastGameState(name);
            buttonContinue.Enabled = !string.IsNullOrEmpty(lastGameState);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Регистрация registrationForm = new Регистрация();
            //
            registrationForm.Show();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void buttonContinue_Click(object sender, EventArgs e)
        {
            try
            {
                string gameState = await DBFunctions.GetLastGameState(name);
                if (!string.IsNullOrEmpty(gameState))
                {
                    GameForm gameForm = new GameForm();
                    gameForm.LoadGameState(gameState);
                    gameForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Нет сохраненной игры.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке игры: " + ex.Message);
            }
        }


    }

}
