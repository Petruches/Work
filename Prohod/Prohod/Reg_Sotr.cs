using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using Microsoft.SqlServer.Server;
using System.IO;

namespace Prohod
{
    public partial class Reg_Sotr : Form
    {
        Procedure _P = new Procedure();//класс методов
        ToolTip _T = new ToolTip();
        Using_Base _UB = new Using_Base();//подключение к БД
        Shifr _Sh = new Shifr();//шифрования

        public Reg_Sotr()
        {
            InitializeComponent();
            textBox5.MaxLength = 6;
            textBox6.MaxLength = 6;
            comboBox1.KeyPress += (sender, e) => e.Handled = true;//запрет на ввод символов
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Avtoriz _A = new Avtoriz();
            this.Hide();
            _A.Show();
        }

        private void Reg_Sotr_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox5.Text == textBox6.Text)
                {
                    if (textBox1.TextLength == 0 || textBox4.TextLength == 0 || textBox5.TextLength == 0 || comboBox1.Text == null || textBox7.TextLength == 0)
                    {
                        MessageBox.Show("Занесите все данные!!!");
                    }
                    else
                    {/*Добавление нового сотрудника*/
                        _P.Sotr_add(textBox1.Text, textBox4.Text, textBox5.Text, comboBox1.SelectedIndex + 1, textBox7.Text);
                        MessageBox.Show("Вы зарегестрированы, \nпожалуйста не забудте введённые данные!");
                        Avtoriz _A = new Avtoriz();
                        this.Hide();
                        _A.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Пароли не совпадают!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show("Пользовательй с таким логиным уже существует!");
            }

        }

        private void Reg_Sotr_Load(object sender, EventArgs e)
        {
            _P.Rol_Load(comboBox1);//вывод ролей
        }

        private void Label8_MouseMove(object sender, MouseEventArgs e)
        {
            _T.SetToolTip(label8, "Укажите отдел, факультет или место \nк которому вы принадлежите");
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            textBox5.UseSystemPasswordChar = false;// пароль раскрыт
            textBox6.UseSystemPasswordChar = false;// паротль раскрыт
            pictureBox1.BackgroundImage = Properties.Resources.Пароль_виден;
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            textBox5.UseSystemPasswordChar = true;// пароль раскрыт
            textBox6.UseSystemPasswordChar = true;// пароль раскрыт
            pictureBox1.BackgroundImage = Properties.Resources.Пароль_не_виден;
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {/*можно водить только те символы котрые указаны в кавычках*/
            if ((e.KeyChar >= 'А' && e.KeyChar <= 'Я') || (e.KeyChar >= 'а' && e.KeyChar <= 'я') || e.KeyChar == '_' || e.KeyChar == (char)Keys.CapsLock || e.KeyChar == (char)Keys.Space || e.KeyChar == (char)Keys.Back)
            {
            }
            else
            {
                e.Handled = true;
            }
        }

        private void TextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {/*можно водить только те символы котрые указаны в кавычках*/
            if ((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z') || e.KeyChar == (char)Keys.Back)
            {
            }
            else
            {
                e.Handled = true;
            }
        }

        private void TextBox5_KeyPress(object sender, KeyPressEventArgs e)
        {/*можно водить только те символы котрые указаны в кавычках*/
            if ((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back)
            {
            }
            else
            {
                e.Handled = true;
            }
        }

        private void TextBox7_KeyPress(object sender, KeyPressEventArgs e)
        {/*можно водить только те символы котрые указаны в кавычках*/
            if ((e.KeyChar >= 'А' && e.KeyChar <= 'Я') || (e.KeyChar >= 'а' && e.KeyChar <= 'я') || (e.KeyChar >= '0' && e.KeyChar <= '9') ||
                e.KeyChar == '_' || e.KeyChar == ',' || e.KeyChar == (char)Keys.Back)
            {
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}