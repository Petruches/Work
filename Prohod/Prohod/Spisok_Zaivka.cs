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
using System.Diagnostics;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office;
using System.Drawing.Printing;

namespace Prohod
{
    public partial class Spisok_Zaivka : Form
    {
        Procedure _P = new Procedure();//класс методов
        Word _WD = new Word();//класс генерации word
        ToolTip _T = new ToolTip();//подсказка

        public Spisok_Zaivka()
        {
            InitializeComponent();
        }

        //private void Grid_Load()
        //{
        //    _P.Ozaivka_void();
        //    dataGridView1.DataSource = Program.Ozaivka_Select;
        //    dataGridView1.Columns[0].Visible = false;
        //    dataGridView1.Columns[1].HeaderText = "Место выдачи";
        //    dataGridView1.Columns[2].HeaderText = "Дата оформления";
        //    dataGridView1.Columns[3].HeaderText = "Посетитель";
        //    dataGridView1.Columns[4].HeaderText = "Номер и серия паспорта";
        //    dataGridView1.Columns[5].HeaderText = "Дата прихода";
        //    dataGridView1.Columns[6].HeaderText = "Время прихода";
        //    dataGridView1.Columns[7].HeaderText = "Место прихода";
        //    dataGridView1.Columns[8].HeaderText = "Сотрудник";
        //    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //}

        private void Spisok_Zaivka_Load(object sender, EventArgs e)
        {/*Таблица оформленных заявок*/
            //Grid_Load();
            Spisok_Zaivka_load();
        }

        private void Spisok_Zaivka_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 _F = new Form1();//главная форма
            _F.label1.Text = Properties.Settings.Default.L1;
            _F.label2.Text = Properties.Settings.Default.L2;
            this.Hide();
            _F.Show();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {/*Печать только выделенных записей*/
            try
            {
                Word _WD = new Word();
                SaveFileDialog sfd = new SaveFileDialog();//создание компонента SaveFileDialog

                sfd.Filter = "Word Documents (*.docx)|*.docx";//расширение файла

                sfd.FileName = "Список.docx";//имя файла

                if (sfd.ShowDialog() == DialogResult.OK)//Окно сохранения
                {
                    _WD.MS_Export_Table(dataGridView1, sfd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {/*Подсказка*/
            _T.SetToolTip(pictureBox1, "Распечатать выделенные поля");
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            Using_Base _UB = new Using_Base();
            _UB.SQLconnect.Open();
            /*Команда хранит хранимую процедуру на удаление*/
            SqlCommand del = new SqlCommand("[DBO].[Oformlennai_zaivka_delete]", _UB.SQLconnect);
            del.CommandType = CommandType.StoredProcedure;//тип команды процедруа
            try
            {
                SqlParameter id = new SqlParameter()//создние параметра
                {
                    ParameterName = "@ID_Ozaivka",//имя параметра
                    Value = dataGridView1.CurrentRow.Cells[0].Value
                };
                del.Parameters.Add(id);
                del.ExecuteNonQuery();
                _UB.SQLconnect.Close();
                //Grid_Load();
                Spisok_Zaivka_load();
                MessageBox.Show("Запись удалена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Spisok_Zaivka_load()
        {
            try
            {
                Using_Base _UB = new Using_Base();
                _UB.SQLconnect.Open();//открытие подключение
                                      /*Создание виртуальной таблицы */
                SqlCommand _Spisok_Zaivka_Pos = new SqlCommand("SELECT Oformlennai_zaivka.ID_Ozaivka, Oformlennai_zaivka.Mesto_Vidachi, Oformlennai_zaivka.Data_oform, Posetitel.FIO_P, " +
                    "Oformlennai_zaivka.Nomer_and_Seria_Pasport, Oformlennai_zaivka.Data_prihoda, Oformlennai_zaivka.Vremi_prihoda, Oformlennai_zaivka.Mesto_prihoda, Sotr.FIO_S " +
                    "FROM [DBO].[Oformlennai_zaivka], [DBO].[Posetitel], [DBO].[Sotr] WHERE Oformlennai_zaivka.S_ID = Sotr.ID_Sotr and Oformlennai_zaivka.Pos_ID = Posetitel.ID_Posetitel", _UB.SQLconnect);
                SqlDataReader SZP = _Spisok_Zaivka_Pos.ExecuteReader();

                DataTable _SZP = new DataTable();
                _SZP.Load(SZP);
                dataGridView1.DataSource = _SZP;
                /*Наименование столбцов*/
                dataGridView1.Columns[0].Visible = false;
                //dataGridView1.Columns[0].HeaderText = "";
                dataGridView1.Columns[1].HeaderText = "Место выдачи";
                dataGridView1.Columns[2].HeaderText = "Дата оформления";
                dataGridView1.Columns[3].HeaderText = "Посетитель";
                dataGridView1.Columns[4].HeaderText = "Номер и серия паспорта";
                dataGridView1.Columns[5].HeaderText = "Дата прихода";
                dataGridView1.Columns[6].HeaderText = "Время прихода";
                dataGridView1.Columns[7].HeaderText = "Место прихода";
                dataGridView1.Columns[8].HeaderText = "Сотрудник";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                _UB.SQLconnect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}