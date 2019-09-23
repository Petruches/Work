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
    public partial class Avtoriz : Form
    {
        Using_Base _UB = new Using_Base();
        Form1 _F = new Form1();
        Procedure _P = new Procedure();
        DataSet ds = new DataSet();
        ToolTip _T = new ToolTip();

        public Avtoriz()
        {
            InitializeComponent();
        }

        private void Avtoriz_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            GC.Collect();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                _UB.SQLconnect.Open();
                /*переменные которые показывают ФИО сотрудника, роль сотрудника и его ID на основе написанного логина в textBox*/
                SqlCommand FIO_S = new SqlCommand("SELECT [FIO_S] FROM [DBO].[Sotr] WHERE [Log_S]='" + textBox1.Text + "'", _UB.SQLconnect);
                SqlCommand Role = new SqlCommand("SELECT [Rolle] FROM [DBO].[Rolli] WHERE [ID_role]=(SELECT [Role_ID] FROM [DBO].[Sotr] WHERE [Log_S]='" + textBox1.Text + "')", _UB.SQLconnect);
                SqlCommand ID = new SqlCommand("SELECT [ID_sotr] FROM [DBO].[Sotr] WHERE [Log_S]='" + textBox1.Text + "'", _UB.SQLconnect);
                /*Проверка всех логинов*/
                SqlCommand getAcc = new SqlCommand("SELECT * FROM [DBO].[Sotr] WHERE [Log_S]='" + textBox1.Text + "'", _UB.SQLconnect);
                var Acc = getAcc.ExecuteScalar();
                _UB.SQLconnect.Close();
                if (Acc != null)
                {
                    _UB.SQLconnect.Open();
                    /*Проверка пароля на основании логина*/
                    SqlCommand getPass = new SqlCommand("SELECT [Pass_S] FROM [DBO].[Sotr] WHERE [Log_S]='" + textBox1.Text + "'", _UB.SQLconnect);
                    string password = getPass.ExecuteScalar().ToString();

                    if (password == textBox2.Text)
                    {/*Вывод сообщения с ФИО и ролью авторизированного пользователя*/
                        MessageBox.Show("Вы вошли как: " + FIO_S.ExecuteScalar().ToString() + ", Роль: " + Role.ExecuteScalar().ToString());
                        Program.IsAdmin = Role.ExecuteScalar().ToString();
                        _F.label1.Text = FIO_S.ExecuteScalar().ToString();
                        _F.label2.Text = Role.ExecuteScalar().ToString();

                        Properties.Settings.Default.L1 = _F.label1.Text;//запись в системную переменную
                        Properties.Settings.Default.L2 = _F.label2.Text;//запись в системную переменную
                        Properties.Settings.Default.Rol = Program.IsAdmin;//запись в системную переменную
                        Properties.Settings.Default.Save(); // Сохранение системных переменных
                        /*Добавление времени авторизации и сотрудника с базу данных*/
                        _P.Time_vhod_add(Convert.ToInt32(ID.ExecuteScalar().ToString()), FIO_S.ExecuteScalar().ToString(), toolStripLabel1.Text);

                        this.Hide();
                        _F.Show();
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль!");
                    }
                }
            }       
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show("Неверный логин или пароль");
            }
                _UB.SQLconnect.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Reg_Sotr _RG = new Reg_Sotr();
            this.Hide();
            _RG.Show();
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.PasswordChar = '\0';
            textBox2.UseSystemPasswordChar = false;// пароль раскрыт
            pictureBox1.BackgroundImage = Properties.Resources.Пароль_виден;
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;// пароль раскрыт
            pictureBox1.BackgroundImage = Properties.Resources.Пароль_не_виден;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            toolStripLabel1.Text = DateTime.Now.ToString("MM/dd/yy H:mm:ss");
        }

        private void Button2_MouseMove(object sender, MouseEventArgs e)
        {
            _T.SetToolTip(button2, "Если вы не зарегестрированы \nпожалуйста нажмите сюда!");
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {/*можно водить только те символы котрые указаны в кавычках*/
            if ((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z') || e.KeyChar == (char)Keys.Back)
            {
            }
            else
            {
                e.Handled = true;
            }
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {/*можно водить только те символы котрые указаны в кавычках*/
            if ((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back)
            {
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}