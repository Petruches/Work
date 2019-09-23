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
    public partial class APanel : Form
    {
        Procedure _P = new Procedure();//класс методов
        ToolTip _T = new ToolTip();// всплывающие подсказки

        public APanel()
        {
            InitializeComponent();
            textBox5.MaxLength = 6;
            /*если выбрать dropdawnlist то в combobox не будут показываться данные при событии DataGridView_CellContentClick*/
            comboBox1.KeyPress += (sender, e) => e.Handled = true;//запрет на ввод символов
        }

        private void APanel_Load(object sender, EventArgs e)
        {
            textBox5.MaxLength = 6;
            //Grid_Load();

            Sotr_Rolli();//таблицы сотрудников
            Grid_Load3();//таблица авторизаций
            _P.Rol_Load(comboBox1);//роли
        }

        private void APanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 _F = new Form1();// открытие главной формы
            _F.label1.Text = Properties.Settings.Default.L1;
            _F.label2.Text = Properties.Settings.Default.L2;
            this.Hide();
            _F.Show();
        }

        //public void Grid_Load()
        //{
        //    _P.Sotr_void();
        //    dataGridView1.DataSource = Program.Sotr_Select;
        //    //dataGridView1.Columns[0].Visible = false;
        //    dataGridView1.Columns[0].HeaderText = "Номер сотрудника";
        //    dataGridView1.Columns[1].HeaderText = "Фамилия";
        //    dataGridView1.Columns[2].HeaderText = "Имя";
        //    dataGridView1.Columns[3].HeaderText = "Отчество";
        //    dataGridView1.Columns[4].HeaderText = "Логин";
        //    dataGridView1.Columns[5].HeaderText = "Пароль";
        //    dataGridView1.Columns[6].HeaderText = "Роль";
        //    dataGridView1.Columns[7].HeaderText = "Дополнительная информация";
        //}

        public void Grid_Load3()
        {/*Таблица главной формы*/
            _P.Time_vhod_void();
            dataGridView2.DataSource = Program.Time_vhod_Select;
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[1].HeaderText = "Номер сотрудника";
            dataGridView2.Columns[2].HeaderText = "Сотрудник";
            dataGridView2.Columns[3].HeaderText = "Время авторизации";
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void Button1_Click(object sender, EventArgs e)
        {/*добавление новой записи, если одно поле пустое - ошибка*/
            try
            {
                    if (textBox1.TextLength == 0 || textBox4.TextLength == 0 || textBox5.TextLength == 0 || comboBox1.Text == null || textBox7.TextLength == 0)
                    {
                        MessageBox.Show("Занесите все данные!!!");
                    }
                    else
                    {
                        _P.Sotr_add(textBox1.Text, textBox4.Text, textBox5.Text, comboBox1.SelectedIndex + 1, textBox7.Text);
                        MessageBox.Show("Сотрудник добавлен");
                        //Grid_Load();
                        Sotr_Rolli();
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Пользовательй с таким логиным уже существует!");
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {/*При выборе поля в datagridview данные дублируются в полях ввода*/
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void Button3_Click(object sender, EventArgs e)
        {/*Удаление выбранного поля по id*/
            try
            {
            Using_Base _UB = new Using_Base();
            _UB.SQLconnect.Open();
            SqlCommand del = new SqlCommand("[DBO].[sotr_delete]", _UB.SQLconnect);
            del.CommandType = CommandType.StoredProcedure;
                SqlParameter id = new SqlParameter()
                {
                    ParameterName = "@ID_sotr",
                    Value = dataGridView1.CurrentRow.Cells[0].Value
                };
                del.Parameters.Add(id);
                del.ExecuteNonQuery();
                _UB.SQLconnect.Close();
                //Grid_Load();
                Sotr_Rolli();
                MessageBox.Show("Сотрудник удалён");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("Данный сотрудник используется в другой таблице");
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {/*команда обновления полей в таблице*/
            try
            {
                Using_Base _UB = new Using_Base();
                _UB.SQLconnect.Open();/*Обновление поля*/
                SqlCommand UP = new SqlCommand("UPDATE Sotr SET " +
                "FIO_S = '" + textBox1.Text + "' ," +
                "Log_S = '" + textBox4.Text + "' ," +
                "Pass_S = '" + textBox5.Text + "' ," +
                "Role_ID = '" + (comboBox1.SelectedIndex +1) + "' ," +
                "Dop_Inf = '" + textBox7.Text + "' WHERE ID_sotr = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, _UB.SQLconnect);
                UP.ExecuteNonQuery();
                _UB.SQLconnect.Close();
                //Grid_Load();
                Sotr_Rolli();
                MessageBox.Show("Обновлено");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;//Фокус убирается
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)//если таблица не равно нулю
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox8.Text))//ишет поле по значению, которое написано в textBox8
                        {
                            dataGridView1.Rows[i].Selected = true;//если найдено тогда выделяется синим
                            break;
                        }
            }
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            textBox5.UseSystemPasswordChar = false;// пароль раскрыт
            pictureBox1.BackgroundImage = Properties.Resources.Пароль_виден;
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            textBox5.UseSystemPasswordChar = true;// пароль раскрыт
            pictureBox1.BackgroundImage = Properties.Resources.Пароль_не_виден;
        }

        private void Sotr_Rolli()
        {
            try
            {
                Using_Base _UB = new Using_Base();
                _UB.SQLconnect.Open();
                /*вывод виртуальной таблицы*/
                SqlCommand _Sotr_Rolli = new SqlCommand("SELECT Sotr.ID_Sotr, Sotr.FIO_S, Sotr.Log_S, Sotr.Pass_S, Rolli.Rolle, Sotr.Dop_Inf " +
                    "FROM [DBO].[Sotr], Rolli WHERE Sotr.Role_ID = Rolli.ID_role", _UB.SQLconnect);
                SqlDataReader SR = _Sotr_Rolli.ExecuteReader();

                DataTable _S = new DataTable();
                _S.Load(SR);
                dataGridView1.DataSource = _S;
                //dataGridView1.Columns[0].Visible = false;/*Название таблиц*/
                dataGridView1.Columns[0].HeaderText = "Номер сотрудника";
                dataGridView1.Columns[1].HeaderText = "Сотрудник";
                dataGridView1.Columns[2].HeaderText = "Логин";
                //dataGridView1.Columns[3].HeaderText = "Пароль";
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].HeaderText = "Роль";
                dataGridView1.Columns[5].HeaderText = "Дополнительная информация";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;//выравнивание таблицы по всей ширине
                _UB.SQLconnect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBox7_MouseMove(object sender, MouseEventArgs e)
        {
            _T.SetToolTip(textBox7, "Укажите факультет или место, \n где Вы работаете");
        }

        private void TextBox7_Enter(object sender, EventArgs e)
        {
            _T.SetToolTip(textBox7, "Укажите факультет или место, \n где Вы работаете");
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            Using_Base _UB = new Using_Base();
            _UB.SQLconnect.Open();
            SqlCommand del = new SqlCommand("[DBO].[Time_vhod_delete]", _UB.SQLconnect);//создание переменной которая зранит таблицу
            del.CommandType = CommandType.StoredProcedure;//интерпритация переменной как хранимая процедура
            try
            {
                SqlParameter id = new SqlParameter()// параметр id
                {
                    ParameterName = "@ID_vhod",//выбор параметра
                    Value = dataGridView2.SelectedRows.Count//задаёт значение выбранной ячейки
                };
                del.Parameters.Add(id);//добавляет параметр
                del.ExecuteNonQuery();
                _UB.SQLconnect.Close();
                //Grid_Load3();
                Sotr_Rolli();
                MessageBox.Show("Запись удалена");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Вы ничего не выбрали");
                //MessageBox.Show(ex.Message);
            }
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
    }
}