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
using Word1 = Microsoft.Office.Interop.Word;
using Microsoft.Office;

namespace Prohod
{
    public partial class Spisok_na_Prohod : Form
    {
        Procedure _P = new Procedure();//класс методов
        Shifr _Sh = new Shifr();//класс шифрования
        ToolTip _T = new ToolTip();//подсказка
        ToolTip _T2 = new ToolTip();//вторая подсказка
        Word _W = new Word();//класс который генерирует таблицу
        Word1.Application oWord = new Word1.Application();

        public Spisok_na_Prohod()
        {
            InitializeComponent();
            /*если выбрать dropdawnlist то в combobox не будут показываться данные при событии DataGridView_CellContentClick*/
            comboBox1.KeyPress += (sender, e) => e.Handled = true;//запрет на ввод символов
            comboBox2.KeyPress += (sender, e) => e.Handled = true;//запрет на ввод символовma
            maskedTextBox1.KeyPress += (sender, e) => e.Handled = true;//запрет на ввод символовma
            dateTimePicker1.KeyPress += (sender, e) => e.Handled = true;//запрет на ввод символовma
            dateTimePicker2.KeyPress += (sender, e) => e.Handled = true;//запрет на ввод символовma
            textBox9.KeyPress += (sender, e) => e.Handled = true;//запрет на ввод символовma
            maskedTextBox3.KeyPress += (sender, e) => e.Handled = true;//запрет на ввод символовma
        }

        //private void Grid_Load()
        //{
        //    _P.Zaivka_void();
        //    dataGridView1.DataSource = Program.Zaivka_Select;
        //    dataGridView1.Columns[0].Visible = false;
        //    dataGridView1.Columns[1].HeaderText = "Сотрудник";
        //    dataGridView1.Columns[2].HeaderText = "Дата оформления";
        //    dataGridView1.Columns[3].HeaderText = "Посетитель";
        //    dataGridView1.Columns[4].HeaderText = "Серия и номер";
        //    dataGridView1.Columns[5].HeaderText = "Дата прихода";
        //    dataGridView1.Columns[6].HeaderText = "Время прихода";
        //    dataGridView1.Columns[7].HeaderText = "Место прихода";
        //    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //}

        private void Zaivka_load()
        {
            Using_Base _UB = new Using_Base();
            _UB.SQLconnect.Open();//открытие подключение
            /*Создание виртуальной таблицы */
            SqlCommand _Zaivka_Pos = new SqlCommand("SELECT Zaivka.ID_zaivka, Sotr.FIO_S, Zaivka.Data_oform, Posetitel.FIO_P, Zaivka.Nomer_and_Seria_Pasport, Zaivka.Data_prihoda, Zaivka.Vremi_prihoda, Zaivka.Mesto_prihoda FROM [DBO].[Zaivka], [DBO].[Posetitel], [DBO].[Sotr] WHERE Zaivka.Sotr_ID = Sotr.ID_Sotr and Zaivka.Posetitel_ID = Posetitel.ID_Posetitel", _UB.SQLconnect);
            SqlDataReader ZP = _Zaivka_Pos.ExecuteReader();

            DataTable _Z = new DataTable();

            _Z.Load(ZP);
            dataGridView1.DataSource = _Z;
            dataGridView1.Columns[0].Visible = false;
            //dataGridView1.Columns[0].HeaderText = "Номер сотрудника";
            dataGridView1.Columns[1].HeaderText = "Сотрудник";
            dataGridView1.Columns[2].HeaderText = "Дата оформления";
            dataGridView1.Columns[3].HeaderText = "Посетитель";
            dataGridView1.Columns[4].HeaderText = "Номер и серия паспорта";
            dataGridView1.Columns[5].HeaderText = "Дата прихода";
            dataGridView1.Columns[6].HeaderText = "Время прихода";
            dataGridView1.Columns[7].HeaderText = "Место прихода";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _UB.SQLconnect.Close();
        }

        private void Spisok_na_Prohod_Load(object sender, EventArgs e)
        {
            //Grid_Load();
            Zaivka_load();//таблица
            _P.Sotr_Load(comboBox2);//вывод списка сотрудников
            _P.Pos_Load(comboBox1);//вывод списка посетителей
        }

        private void Spisok_na_Prohod_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 _F = new Form1();//открытие главной формы
            _F.label1.Text = Properties.Settings.Default.L1;
            _F.label2.Text = Properties.Settings.Default.L2;
            this.Hide();
            _F.Show();
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {//событие которое переносит данные выбранного поля в поля ввода
            maskedTextBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            maskedTextBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            dateTimePicker2.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox9.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            maskedTextBox3.Text = _Sh.Codeс(maskedTextBox3.Text, -5/*-(int)numericUpDown1.Value*/);
        }

        private void Button1_Click(object sender, EventArgs e)
        {/*происходит оформление заявки на проход*/
            try
            {/*Добавляется информация в таблицы из полей*/
                if (textBox2.Text == null)
                {
                    _P.Ozaivka_add(textBox2.Text, maskedTextBox1.Text, comboBox1.SelectedIndex + 1, maskedTextBox3.Text = _Sh.Codeс(maskedTextBox3.Text, 5/*Смещение*/), dateTimePicker1.Text, dateTimePicker2.Text, textBox9.Text, comboBox2.SelectedIndex + 1);
                    Using_Base _UB = new Using_Base();/*Действующая заявка на проход удаляется*/
                    _UB.SQLconnect.Open();
                    SqlCommand del = new SqlCommand("[DBO].[Zaivka_delete]", _UB.SQLconnect);/*команда хранит хранимую процедуру на удаление заявки*/
                    del.CommandType = CommandType.StoredProcedure;
                    SqlParameter id = new SqlParameter()//перечисление параметров
                    {
                        ParameterName = "@ID_zaivka",
                        Value = dataGridView1.CurrentRow.Cells[0].Value
                    };
                    del.Parameters.Add(id);
                    del.ExecuteNonQuery();
                    _UB.SQLconnect.Close();
                    MessageBox.Show("Оформлено");
                    //Grid_Load();
                    Zaivka_load();
                }
                else
                {
                    MessageBox.Show("Заполните все поля!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;//все выделения убираются
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)//если таблица е пуста
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))//если данные записанные в поле есть
                        {
                            dataGridView1.Rows[i].Selected = true;//Тогда происходит выделение
                            break;
                        }
            }
        }

        private void PictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            _T2.SetToolTip(pictureBox2, "Распечатать краткосрочную карту");//всплывающая подсказка
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                Word _WD = new Word();//класс, который генерирует таблицу в документ word
                SaveFileDialog sfd = new SaveFileDialog();//создание компонента SaveFileDialog

                sfd.Filter = "Word Documents (*.docx)|*.docx";//расширение файла

                sfd.FileName = "Список.docx";//имя файла

                if (sfd.ShowDialog() == DialogResult.OK)//окно сохранения
                {
                    _WD.MS_Export_Table(dataGridView1, sfd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == null || comboBox1.Text == null)
            {
                MessageBox.Show("Заполните данныеа");
            }
            else
            {
                try
                {
                    Word1.Application app = new Word1.Application();//создание документа
                    Word1.Document doc = app.Documents.Add("C:\\Users\\petya\\Desktop\\Шаблон.docx");//выбор шаблона
                    /*для того что бы поля записались надо добавить закладки в меню вставка>закладка*/
                    doc.Bookmarks["sotr"].Range.Text = comboBox2.Text;
                    doc.Bookmarks["pos"].Range.Text = comboBox1.Text;
                    doc.Bookmarks["date"].Range.Text = DateTime.Now.ToString("dd.MM.yy");
                    doc.SaveAs(FileName: "C:\\Users\\petya\\Desktop\\Краткосрочная карта.doc");//сохранение документа
                    app.Visible = true;//открытие
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {/*подсказка*/
            _T.SetToolTip(pictureBox1, "Распечатать выделенные поля");
        }
    }
}