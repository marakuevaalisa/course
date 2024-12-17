using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Npgsql; // Библиотека для работы с PostgreSQL
using BCrypt.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
    public partial class Регистрация : Form
    {
        public Регистрация()
        {
            InitializeComponent();
        }
        
        

        private async void button1_Click(object sender, EventArgs e)
{
            if (textBox2.Text != "" && textBox3.Text != "")
            {
                string name = textBox2.Text;
                string password = textBox3.Text;

                // Хеширование пароля
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                try
                {
                    if (!DBFunctions.IsUserExists(name))
                    {
                        await DBFunctions.addUser(name, hashedPassword);

                        // Открытие файла и запись результата
                        using (System.IO.StreamWriter sw = new System.IO.StreamWriter("Recording_moves.txt", true))
                        {
                            sw.WriteLine();
                            sw.WriteLine(DateTime.Now + " Новый игрок: " + textBox2.Text);
                            sw.Close();
                        }

                        
                        GameForm gForm = new GameForm();
                        gForm.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Такой пользователь уже существует!");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                MessageBox.Show("Имя или пароль не введены!\nПопробуйте ещё раз!");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Регистрация_Load(object sender, EventArgs e)
        {
            

        }
    }
}
