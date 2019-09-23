using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Diagnostics;
using System.Data;
using System.Windows.Forms;

namespace Prohod
{
    class Procedure
    {
        //-------------------------------СОТРУДНИКИ-------------------------------
        public void Sotr_void()//таблица сотрудников
        {
            Using_Base _UB = new Using_Base();/*класс подключения в БД*/
            _UB.SQLconnect.Open();//открытие
            /*команда хранит таблицу*/
            SqlCommand _Sotr = new SqlCommand("SELECT [ID_Sotr], [FIO_S], [Log_S], [Pass_S], [Role_ID], [Dop_Inf] FROM [DBO].[Sotr]", _UB.SQLconnect);
            SqlDataReader tableReader = _Sotr.ExecuteReader();//чтение таблицы
            DataTable Table = new DataTable();//создание таблицы
            Table.Load(tableReader);//загрузка таблицы
            Program.Sotr_Select = Table;
            _UB.SQLconnect.Close();//закрытие
        }

        public void Sotr_add(string A, string B, string C, int D, string F)//ДОБАВЛЕНИЕ СОТРУДНИКОВ
        {
                Using_Base _UB = new Using_Base();
                _UB.SQLconnect.Open();
                SqlCommand Sotr_ADD = new SqlCommand("sotr_insertinto", _UB.SQLconnect);
                Sotr_ADD.CommandType = CommandType.StoredProcedure;
                Sotr_ADD.Parameters.AddWithValue("@FIO_S", A);
                Sotr_ADD.Parameters.AddWithValue("@Log_S", B);
                Sotr_ADD.Parameters.AddWithValue("@Pass_S", C);
                Sotr_ADD.Parameters.AddWithValue("@Role_ID", D);
                Sotr_ADD.Parameters.AddWithValue("@Dop_Inf", F);
                Sotr_ADD.ExecuteNonQuery();
                _UB.SQLconnect.Close();
        }

        //---------------------------Роль-------------------------
        public void Role_void()//таблица Роль
        {
            Using_Base _UB = new Using_Base();
            _UB.SQLconnect.Open();
            SqlCommand _Role = new SqlCommand("SELECT [ID_role], [Rolle] FROM [DBO].[Rolli]", _UB.SQLconnect);
            SqlDataReader tableReader = _Role.ExecuteReader();
            DataTable Table = new DataTable();
            Table.Load(tableReader);
            Program.Role_Select = Table;
            _UB.SQLconnect.Close();
        }

        public void role_add(string A)//ДОБАВЛЕНИЕ Роли
        {
            Using_Base _UB = new Using_Base();
            _UB.SQLconnect.Open();
            SqlCommand role_ADD = new SqlCommand("role_insertinto", _UB.SQLconnect);
            role_ADD.CommandType = CommandType.StoredProcedure;
            role_ADD.Parameters.AddWithValue("@Rolle", A);
            role_ADD.ExecuteNonQuery();
            _UB.SQLconnect.Close();
        }

        //------------------------------Посетитель----------------------------
        public void Posetitel_void()//таблица сотрудников
        {
            Using_Base _UB = new Using_Base();
            _UB.SQLconnect.Open();
            SqlCommand _Posetitel = new SqlCommand("SELECT [ID_Posetitel], [FIO_P], [Nomer_and_Seria_Pasport] FROM [DBO].[Posetitel]", _UB.SQLconnect);
            SqlDataReader tableReader = _Posetitel.ExecuteReader();
            DataTable Table = new DataTable();
            Table.Load(tableReader);
            Program.Posetitel_Select = Table;
            _UB.SQLconnect.Close();
        }

        public void Posetitel_add(string A, string B)//ДОБАВЛЕНИЕ Посетителей
        {
            Using_Base _UB = new Using_Base();
            _UB.SQLconnect.Open();
            SqlCommand Posetitel_ADD = new SqlCommand("Posetitel_insertinto", _UB.SQLconnect);
            Posetitel_ADD.CommandType = CommandType.StoredProcedure;
            Posetitel_ADD.Parameters.AddWithValue("@FIO_P", A);
            Posetitel_ADD.Parameters.AddWithValue("@Nomer_and_Seria_Pasport", B);
            Posetitel_ADD.ExecuteNonQuery();
            _UB.SQLconnect.Close();
        }

        //----------------------------------Заявка-------------------------------
        public void Zaivka_void()//таблица заявки
        {
            Using_Base _UB = new Using_Base();
            _UB.SQLconnect.Open();
            SqlCommand _Zaivka = new SqlCommand("SELECT [ID_zaivka], [Sotr_ID], [Data_oform], [Posetitel_ID], [Nomer_and_Seria_Pasport], [Data_prihoda], [Vremi_prihoda], [Mesto_prihoda] FROM [DBO].[Zaivka]", _UB.SQLconnect);
            SqlDataReader tableReader = _Zaivka.ExecuteReader();
            DataTable Table = new DataTable();
            Table.Load(tableReader);
            Program.Zaivka_Select = Table;
            _UB.SQLconnect.Close();
        }

        public void Zaivka_add(int A, string B, int C, string D, string I, string F, string J)//ДОБАВЛЕНИЕ Заявки
        {
            Using_Base _UB = new Using_Base();
            _UB.SQLconnect.Open();
            SqlCommand Zaivka_ADD = new SqlCommand("Zaivka_insertinto", _UB.SQLconnect);
            Zaivka_ADD.CommandType = CommandType.StoredProcedure;
            Zaivka_ADD.Parameters.AddWithValue("@Sotr_ID", A);
            Zaivka_ADD.Parameters.AddWithValue("@Data_oform", B);
            Zaivka_ADD.Parameters.AddWithValue("@Posetitel_ID", C);
            Zaivka_ADD.Parameters.AddWithValue("@Nomer_and_Seria_Pasport", D);
            Zaivka_ADD.Parameters.AddWithValue("@Data_prihoda", I);
            Zaivka_ADD.Parameters.AddWithValue("@Vremi_prihoda", F);
            Zaivka_ADD.Parameters.AddWithValue("@Mesto_prihoda", J);
            Zaivka_ADD.ExecuteNonQuery();
            _UB.SQLconnect.Close();
        }

        //--------------------------------------Оформленная заявка-----------------------------------
        public void Ozaivka_void()//таблица оформленной заявки
        {
            Using_Base _UB = new Using_Base();
            _UB.SQLconnect.Open();
            SqlCommand _Ozaivka = new SqlCommand("SELECT [ID_Ozaivka], [Mesto_Vidachi], [Data_oform], [Pos_ID], [Nomer_and_Seria_Pasport], [Data_prihoda], [Vremi_prihoda], [Mesto_prihoda], [S_ID] FROM [DBO].[Oformlennai_zaivka]", _UB.SQLconnect);
            SqlDataReader tableReader = _Ozaivka.ExecuteReader();
            DataTable Table = new DataTable();
            Table.Load(tableReader);
            Program.Ozaivka_Select = Table;
        }

        public void Ozaivka_add(string A, string B, int C, string D, string E, string F, string G, int H)//ДОБАВЛЕНИЕ в учёт
        {
            Using_Base _UB = new Using_Base();
            _UB.SQLconnect.Open();
            SqlCommand Ozaivka_ADD = new SqlCommand("Oformlennai_zaivka_insertinto", _UB.SQLconnect);
            Ozaivka_ADD.CommandType = CommandType.StoredProcedure;
            Ozaivka_ADD.Parameters.AddWithValue("@Mesto_Vidachi", A);
            Ozaivka_ADD.Parameters.AddWithValue("@Data_oform", B);
            Ozaivka_ADD.Parameters.AddWithValue("@Pos_ID", C);
            Ozaivka_ADD.Parameters.AddWithValue("@Nomer_and_Seria_Pasport", D);
            Ozaivka_ADD.Parameters.AddWithValue("@Data_prihoda", E);
            Ozaivka_ADD.Parameters.AddWithValue("@Vremi_prihoda", F);
            Ozaivka_ADD.Parameters.AddWithValue("@Mesto_prihoda", G);
            Ozaivka_ADD.Parameters.AddWithValue("@S_ID", H);
            Ozaivka_ADD.ExecuteNonQuery();
            _UB.SQLconnect.Close();
        }

        //-------------------------------Учёт входа пользователей--------------------------------
        public void Time_vhod_void()//таблица учёта входа пользователей
        {
            Using_Base _UB = new Using_Base();
            _UB.SQLconnect.Open();
            SqlCommand _Time_vhod = new SqlCommand("SELECT [ID_vhod], [Sotr_ID], [FIO_S], [Vrema] FROM [DBO].[Time_vhod]", _UB.SQLconnect);
            SqlDataReader tableReader = _Time_vhod.ExecuteReader();
            DataTable Table = new DataTable();
            Table.Load(tableReader);
            Program.Time_vhod_Select = Table;
        }

        public void Time_vhod_add(int A, string B, string C)//ДОБАВЛЕНИЕ в учёт
        {
            Using_Base _UB = new Using_Base();
            _UB.SQLconnect.Open();
            SqlCommand Time_vhod_ADD = new SqlCommand("Time_vhod_insertinto", _UB.SQLconnect);
            Time_vhod_ADD.CommandType = CommandType.StoredProcedure;
            Time_vhod_ADD.Parameters.AddWithValue("@Sotr_ID", A);
            Time_vhod_ADD.Parameters.AddWithValue("@FIO_S", B);
            Time_vhod_ADD.Parameters.AddWithValue("@Vrema", C);
            Time_vhod_ADD.ExecuteNonQuery();
            _UB.SQLconnect.Close();
        }

        //--------------------------------------------combobox роли--------------------------------
        public class Role
        {/*создание переменных*/
            public int id { get; set; }
            public string ROLLE { get; set; }
            public override string ToString()
            {
                return string.Format("Rolle: {1}, ID_role: {0}", ROLLE, id);
            }
        }

        public void Rol_Load(ComboBox CB)
        {
            try
            {
                Using_Base _UB = new Using_Base();
                _UB.SQLconnect.Open();
                /*команда создания таблицы*/
                SqlCommand _rolli = new SqlCommand("SELECT ID_role, Rolle FROM [DBO].[Rolli]", _UB.SQLconnect);// вывод таблицы с ролями
                /*создание листа в которой каждая переменная будет хранить поле из таблицы*/
                List<Role> rol = new List<Role>();
                {
                    SqlDataReader ReadRol = _rolli.ExecuteReader();
                    while (ReadRol.Read())
                    {
                        Role coun = new Role
                        {
                            id = int.Parse(ReadRol["ID_role"].ToString()),
                            ROLLE = (ReadRol["Rolle"].ToString())
                        };
                        rol.Add(coun);
                    }
                }
                CB.DataSource = rol;
                CB.DisplayMember = "ROLLE";//показывает это
                CB.ValueMember = Convert.ToString("id");//записывает это
                _UB.SQLconnect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //--------------------------------------------combobox сотрудник--------------------------------
        public class Sotr
        {
            public int ID_sotr { get; set; }
            public string FIO_S { get; set; }
            public override string ToString()
            {
                return string.Format("FIO_S: {1}, ID_sotr: {0}", FIO_S, ID_sotr);
            }
        }

        public void Sotr_Load(ComboBox CB)
        {
            Using_Base _UB = new Using_Base();
            _UB.SQLconnect.Open();
            SqlCommand _rolli = new SqlCommand("SELECT ID_sotr, FIO_S FROM [DBO].[Sotr]", _UB.SQLconnect);// вывод таблицы

            List<Sotr> rol = new List<Sotr>();//создание листа
            {
                SqlDataReader ReadRol = _rolli.ExecuteReader();
                while (ReadRol.Read())
                {
                    Sotr coun = new Sotr
                    {
                        ID_sotr = int.Parse(ReadRol["ID_sotr"].ToString()),
                        FIO_S = (ReadRol["FIO_S"].ToString())
                    };
                    rol.Add(coun);
                }
            }
            CB.DataSource = rol;
            CB.DisplayMember = "FIO_S";
            CB.ValueMember = Convert.ToString("ID_sotr");
            _UB.SQLconnect.Close();
        }

        //--------------------------------------------combobox посетитель--------------------------------
        public class Pos
        {
            public int ID_Posetitel { get; set; }
            public string FIO_P { get; set; }
            public override string ToString()
            {
                return string.Format("FIO_P: {1}, ID_Posetitel: {0}", FIO_P, ID_Posetitel);
            }
        }

        public void Pos_Load(ComboBox CB)
        {
            Using_Base _UB = new Using_Base();
            _UB.SQLconnect.Open();
            SqlCommand _rolli = new SqlCommand("SELECT ID_Posetitel, FIO_P FROM [DBO].[Posetitel]", _UB.SQLconnect);// вывод таблицы

            List<Pos> rol = new List<Pos>();//создание листа
            {
                SqlDataReader ReadRol = _rolli.ExecuteReader();
                while (ReadRol.Read())
                {
                    Pos coun = new Pos
                    {
                        ID_Posetitel = int.Parse(ReadRol["ID_Posetitel"].ToString()),
                        FIO_P = (ReadRol["FIO_P"].ToString())
                    };
                    rol.Add(coun);
                }
            }
            CB.DataSource = rol;
            CB.DisplayMember = "FIO_P";
            CB.ValueMember = Convert.ToString("ID_Posetitel");
            _UB.SQLconnect.Close();
        }
    }
}