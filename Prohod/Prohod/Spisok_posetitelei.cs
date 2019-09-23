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
    public partial class Spisok_posetitelei : Form
    {
        Procedure _P = new Procedure();//класс методов
        Shifr _Sh = new Shifr();//класс шифрование

        public Spisok_posetitelei()
        {
            InitializeComponent();
        }

        private void Grid_Load()
        {/*таблица посетителей*/
            _P.Posetitel_void();
            dataGridView1.DataSource = Program.Posetitel_Select;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Посетитель";
            dataGridView1.Columns[2].HeaderText = "Номер и серия паспорта";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {/*При выборе поля в datagridview данные дублируются в полях ввода*/
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            maskedTextBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            maskedTextBox1.Text = _Sh.Codeс(maskedTextBox1.Text, -5/*-(int)numericUpDown1.Value*/);
        }

        private void Spisok_posetitelei_FormClosed(object sender, FormClosedEventArgs e)
        {
            AddZaivka _AD = new AddZaivka();//форма создания заявки
            this.Hide();
            _P.Sotr_Load(_AD.comboBox2);//список сотрудников
            _P.Pos_Load(_AD.comboBox1);//список посетителей
            _AD.Zaivka_Load();//таблица заявок
        }

        private void Button1_Click(object sender, EventArgs e)
        {/*Добавление нового пользователя*/
            if (textBox1.TextLength == 0 || maskedTextBox1.TextLength == 0)
            {
                MessageBox.Show("Занесите все данные!!!");
            }
            else
            {
                _P.Posetitel_add(textBox1.Text, maskedTextBox1.Text = _Sh.Codeс(maskedTextBox1.Text, 5/*Смещение*/)/*Шифрование*/);
                MessageBox.Show("Посетитель добавлен");
                Grid_Load();
            }
        }

        private void Spisok_posetitelei_Load(object sender, EventArgs e)
        {
            Grid_Load();
            this.Activate();//форма активна
        }

        private void Button2_Click(object sender, EventArgs e)
        {/*обновление записи*/
            try
            {
                Using_Base _UB = new Using_Base();
                _UB.SQLconnect.Open();
                SqlCommand UP = new SqlCommand("UPDATE Posetitel SET " +
                "FIO_P = '" + textBox1.Text + "' ," +
                "Nomer_and_Seria_Pasport = '" + (maskedTextBox1.Text = _Sh.Codeс(maskedTextBox1.Text, 5/*Смещение*/)) + "' WHERE ID_Posetitel = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, _UB.SQLconnect);
                UP.ExecuteNonQuery();
                _UB.SQLconnect.Close();
                Grid_Load();
                MessageBox.Show("Обновлено");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {/*удаление записи*/
            Using_Base _UB = new Using_Base();
            _UB.SQLconnect.Open();
            /*команда содержащая хранимую процедуру на удаление*/
            SqlCommand del = new SqlCommand("[DBO].[Posetitel_delete]", _UB.SQLconnect);
            del.CommandType = CommandType.StoredProcedure;//тип команды процедура
            try
            {
                SqlParameter id = new SqlParameter()//создание параметра
                {
                    ParameterName = "@ID_Posetitel",//имя параметра
                    Value = dataGridView1.CurrentRow.Cells[0].Value
                };
                del.Parameters.Add(id);
                del.ExecuteNonQuery();
                _UB.SQLconnect.Close();
                Grid_Load();
                MessageBox.Show("Посетитель удалён");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;//Фокус убирается
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)//если таблица не равно нулю
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))//ишет поле по значению, которое написано в textBox8
                        {
                            dataGridView1.Rows[i].Selected = true;//если найдено тогда выделяется синим
                            break;
                        }
            }
        }
    }
}