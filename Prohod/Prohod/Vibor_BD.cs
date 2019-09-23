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
    public partial class Vibor_BD : Form
    {
        Using_Base _UB = new Using_Base();

        public Vibor_BD()
        {
            InitializeComponent();
        }

        public void getInst()
        {
            try
            {//Создание таблицы в которой хранятся все локальные сервера
                using (DataTable SQLSources = SqlDataSourceEnumerator.Instance.GetDataSources())
                {
                    foreach (DataRow source in SQLSources.Rows)//цикл предоставляющий строку
                    {
                        string instanceName = source["InstanceName"].ToString();

                        if (!string.IsNullOrEmpty(instanceName))
                        {//отображает список локальных серверов
                            comboBox1.Items.Add(source["ServerName"] + "\\" + source["InstanceName"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка выбора сервера!");
                //MessageBox.Show(ex.Message);
            }

        }

        private void Vibor_BD_Load(object sender, EventArgs e)
        {
            getInst();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == null || textBox1.Text == null || textBox2.Text == null)
                {
                    MessageBox.Show("Выберите сервер");
                }
                else
                {
                    _UB.bdhost = comboBox1.Text;
                    _UB.bdlog = textBox1.Text;
                    _UB.bdpass = textBox2.Text;
                    _UB.bd = comboBox2.Text;
                    SqlConnection dbconn = new SqlConnection("Data Source=" + comboBox1.Text + ";User ID=" + textBox1.Text + ";Password=" + textBox2.Text + "");
                    dbconn.Open();
                    /*отображение списка баз данных исходя из введнённых данных в поля указанные выше*/
                    SqlCommand getDb = new SqlCommand("SELECT name FROM master.dbo.sysdatabases;", dbconn);
                    SqlDataReader reader = getDb.ExecuteReader();

                    if (reader.HasRows) // если есть данные
                    {
                        comboBox2.Enabled = true;
                        button2.Enabled = true;
                        // выводятся названия столбцов
                        while (reader.Read()) // построчно считываются данные
                        {
                            comboBox2.Items.Add(reader.GetString(0));
                        }
                    }
                    dbconn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выберите сервер");
                //MessageBox.Show(ex.Message);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox2.Text == null)
                {
                    MessageBox.Show("Выберите источник данных");
                }
                else
                {
                    _UB.bd = comboBox2.Text;//источник данных появляется в поле
                    _UB.SetConn();
                    Avtoriz _A = new Avtoriz();
                    this.Hide();
                    _A.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выберите источник данных");
                //MessageBox.Show(ex.Message);
            }
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {//если кнопка нажата пароль виден
            textBox2.UseSystemPasswordChar = false;// пароль раскрыт
            pictureBox1.BackgroundImage = Properties.Resources.Пароль_виден;
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {//если кнопка отпущена пароль не виден
            textBox2.UseSystemPasswordChar = true;// пароль раскрыт
            pictureBox1.BackgroundImage = Properties.Resources.Пароль_не_виден;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Application.Exit();//закрытие программы
        }

        private void Vibor_BD_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();//закрытие программы
        }
    }
}