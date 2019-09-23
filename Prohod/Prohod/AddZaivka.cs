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

namespace Prohod
{
    public partial class AddZaivka : Form
    {
        Procedure _P = new Procedure();//класс методов
        Shifr _Sh = new Shifr();//класс шифрования
        Using_Base _UB = new Using_Base();//класс подключения к БД

        public AddZaivka()
        {
            InitializeComponent();
            /*если выбрать dropdawnlist то в combobox не будут показываться данные при событии DataGridView_CellContentClick*/
            comboBox1.KeyPress += (sender, e) => e.Handled = true;//запрет на ввод символов
            comboBox2.KeyPress += (sender, e) => e.Handled = true;//запрет на ввод символов
            maskedTextBox1.KeyPress += (sender, e) => e.Handled = true;//запрет на ввод символов
        }

        private void AddZaivka_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 _F = new Form1();//главное окно
            _F.label1.Text = Properties.Settings.Default.L1;
            _F.label2.Text = Properties.Settings.Default.L2;
            this.Hide();
            _F.Show();
        }

        //private void Grid_Load()
        //{
        //    _P.Zaivka_void();
        //    dataGridView1.DataSource = Program.Zaivka_Select;
        //    dataGridView1.Columns[0].Visible = false;
        //    dataGridView1.Columns[1].HeaderText = "Сотрудник";
        //    dataGridView1.Columns[2].HeaderText = "Дата оформления";
        //    dataGridView1.Columns[3].HeaderText = "Посетитель";
        //    dataGridView1.Columns[4].HeaderText = "Серия и номер паспорта";
        //    dataGridView1.Columns[5].HeaderText = "Дата прихода";
        //    dataGridView1.Columns[6].HeaderText = "Время прихода";
        //    dataGridView1.Columns[7].HeaderText = "Место прихода";
        //    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //}

        private void AddZaivka_Load(object sender, EventArgs e)
        {
            //Grid_Load();
            Zaivka_Load();//таблица заявок
            _P.Sotr_Load(comboBox2);//список сотрудников
            _P.Pos_Load(comboBox1);//список посетителей
        }

        private void Button4_Click(object sender, EventArgs e)
        {/*при открытии окна посетителей окно заявок не закрывается*/
            Spisok_posetitelei _SP = new Spisok_posetitelei();//окно список посетителей
            _SP.Show();
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {/*При выборе поля в datagridview данные дублируются в полях ввода */
            comboBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            maskedTextBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            maskedTextBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            dateTimePicker2.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            maskedTextBox2.Text = _Sh.Codeс(maskedTextBox2.Text, -5/*-(int)numericUpDown1.Value*/);
        }

        private void Button1_Click(object sender, EventArgs e)
        {/*Добавление записи используя метод из класса*/
            try
            {
                if (comboBox2.Text == null || maskedTextBox1.TextLength == 0 || comboBox1.Text == null || maskedTextBox2.TextLength == 0 || textBox7.TextLength == 0)
                {
                 MessageBox.Show("Занесите все данные!!!");
                }
                else
                {
                   _P.Zaivka_add(/*Convert.ToInt32(comboBox2.Text)*/comboBox2.SelectedIndex + 1, maskedTextBox1.Text, comboBox1.SelectedIndex + 1, maskedTextBox2.Text = _Sh.Codeс(maskedTextBox2.Text, 5/*Смещение*/), dateTimePicker2.Text, dateTimePicker1.Text, textBox7.Text);
                    MessageBox.Show("Заявка добавлена добавлен");
                   //Grid_Load();
                    Zaivka_Load();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Произошла ошибка при добавлении заявки, \nпожалуйста оповестите об этом администратора!");
                MessageBox.Show(ex.Message);
            }
}

        private void Button2_Click(object sender, EventArgs e)
        {/*изменение записи*/
            try
            {
                Using_Base _UB = new Using_Base();
                _UB.SQLconnect.Open();
                SqlCommand UP = new SqlCommand("UPDATE Zaivka SET " +
                "Sotr_ID = '" + (comboBox2.SelectedIndex + 1) + "' ," +
                "Data_oform = '" + maskedTextBox1.Text + "' ," +
                "Posetitel_ID = '" + (comboBox1.SelectedIndex + 1) + "' ," +
                "Nomer_and_Seria_Pasport = '" + (maskedTextBox2.Text = _Sh.Codeс(maskedTextBox2.Text, 5/*Смещение*/)) + "' ," +
                "Data_prihoda = '" + dateTimePicker2.Text + "' ," +
                "Vremi_prihoda = '" + dateTimePicker1.Text + "' ," +
                "Mesto_prihoda = '" + textBox7.Text + "' WHERE ID_zaivka = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, _UB.SQLconnect);
                UP.ExecuteNonQuery();
                _UB.SQLconnect.Close();
                //Grid_Load();
                Zaivka_Load();
                MessageBox.Show("Обновлено");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {/*Удаление выбранной записи*/
            Using_Base _UB = new Using_Base();
            _UB.SQLconnect.Open();
            /*создание коанды которая хранит зранимую процедуру*/
            SqlCommand del = new SqlCommand("[DBO].[Zaivka_delete]", _UB.SQLconnect);
            del.CommandType = CommandType.StoredProcedure;//тип команды процедура
            try
            {
                SqlParameter id = new SqlParameter()//создание параметра
                {
                    ParameterName = "@ID_zaivka",//объявление параметра
                    Value = dataGridView1.CurrentRow.Cells[0].Value
                };
                del.Parameters.Add(id);
                del.ExecuteNonQuery();
                _UB.SQLconnect.Close();
                Zaivka_Load();
                MessageBox.Show("Заявка удалена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Zaivka_Load()
        {
            try
            {
                Using_Base _UB = new Using_Base();
                _UB.SQLconnect.Open();//подключение к БД
                /*Виртуальная таблица*/
                SqlCommand _Posetitel = new SqlCommand("SELECT Zaivka.ID_zaivka, Sotr.FIO_S, Zaivka.Data_oform, Posetitel.FIO_P, Zaivka.Nomer_and_Seria_Pasport, Zaivka.Data_prihoda, Zaivka.Vremi_prihoda, Zaivka.Mesto_prihoda FROM [DBO].[Zaivka], [DBO].[Sotr], [DBO].[Posetitel] WHERE Zaivka.Sotr_ID = Sotr.ID_sotr and Zaivka.Posetitel_ID = Posetitel.ID_Posetitel", _UB.SQLconnect);
                SqlDataReader Posetitel = _Posetitel.ExecuteReader();

                DataTable _P = new DataTable();
                _P.Load(Posetitel);
                dataGridView1.DataSource = _P;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Cотрудник";
                dataGridView1.Columns[2].HeaderText = "Дата оформления";
                dataGridView1.Columns[3].HeaderText = "Посетитель";
                dataGridView1.Columns[4].HeaderText = "Серия и номер паспорта";
                dataGridView1.Columns[5].HeaderText = "Дата прихода";
                dataGridView1.Columns[6].HeaderText = "Время прихода";
                dataGridView1.Columns[7].HeaderText = "Место прихода";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                _UB.SQLconnect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)//если таблица не пуста
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))//если данные записанные в поле есть
                        {
                            dataGridView1.Rows[i].Selected = true;//Тогда происходит выделение
                            break;
                        }
            }
        }

        private void TextBox7_KeyPress(object sender, KeyPressEventArgs e)
        {/*можно водить только те символы котрые указаны в кавычках*/
            if ((e.KeyChar >= 'А' && e.KeyChar <= 'Я') || (e.KeyChar >= 'а' && e.KeyChar <= 'я') || /*(e.KeyChar >= '0' && e.KeyChar <= '9') ||*/
                e.KeyChar == (char)Keys.CapsLock || e.KeyChar == (char)Keys.Space || e.KeyChar == '_' || e.KeyChar == ',' || e.KeyChar == (char)Keys.Back)
            {
            }
            else
            {
                e.Handled = true;
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {/*запись текущей даты для того что бы дату не подделали*/
            maskedTextBox1.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
}