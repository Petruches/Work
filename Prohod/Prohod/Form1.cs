using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prohod
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {/*Разграничение прав на основе роли содержащейся в label2*/
            switch (label2.Text)
            {
                case "Администратор                 ":
                    {
                        //button1.Visible = false;
                        break;
                    }
                case "Оператор                      ":
                    {
                        button1.Visible = false;
                        button4.Visible = false;
                        break;
                    }
                case "Декан                         ":
                    {
                        button2.Visible = false;
                        button5.Visible = false;
                        button4.Visible = false;
                        break;
                    }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();//закрытие программы
        }

        private void Button3_Click(object sender, EventArgs e)
        {/*Переъод на форму авторизации*/
            Avtoriz _A = new Avtoriz();
            this.Hide();
            Properties.Settings.Default.Reset();//переменные по умолчанию
            _A.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {/*переход на форму создания заявки*/
            AddZaivka _AZ = new AddZaivka();
            Properties.Settings.Default.L1 = label1.Text;
            Properties.Settings.Default.L2 = label2.Text;
            Properties.Settings.Default.Save();// Сохранение системных переменных
            this.Hide();
            _AZ.Show();
        }

        private void Button4_Click(object sender, EventArgs e)
        {/*переход на панель адинистратора*/
            APanel _AP = new APanel();
            Properties.Settings.Default.L1 = label1.Text;
            Properties.Settings.Default.L2 = label2.Text;
            Properties.Settings.Default.Save();// Сохранение системных переменных
            this.Hide();
            _AP.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {/*переходи на форму сисок заявок на проход*/
            Spisok_na_Prohod _SP = new Spisok_na_Prohod();
            Properties.Settings.Default.L1 = label1.Text;
            Properties.Settings.Default.L2 = label2.Text;
            Properties.Settings.Default.Save();// Сохранение системных переменных
            this.Hide();
            _SP.Show();
        }

        private void Button5_Click(object sender, EventArgs e)
        {/*переход на форму список оформленных заявок*/
            Spisok_Zaivka _SZ = new Spisok_Zaivka();
            Properties.Settings.Default.L1 = label1.Text;
            Properties.Settings.Default.L2 = label2.Text;
            Properties.Settings.Default.Save();// Сохранение системных переменных
            this.Hide();
            _SZ.Show();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Application.Exit();//закрытие программы 
        }
    }
}