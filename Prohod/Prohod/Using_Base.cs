using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Prohod
{
    class Using_Base
    {
        public string bd, bdpass, bdhost, bdlog;//создание переменны
        public static string BD, BDPass, BDHost, BDLog;//создание статических переменных
        /*подключение к базе данных*/
        public SqlConnection SQLconnect = new SqlConnection("Data Source=" + BDHost + ";Initial Catalog=" + BD + ";User ID=" + BDLog + ";Password=" + BDPass + "");

        public void SetConn()
        {/*передача статитеских данных в динамические переменные*/
            try
            {
                BD = bd;
                BDPass = bdpass;
                BDHost = bdhost;
                BDLog = bdlog;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}