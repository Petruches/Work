using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace Prohod
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Vibor_BD());
        }
        public static string IsAdmin;
        public static DataTable Sotr_Select;
        public static DataTable Role_Select;
        public static DataTable Posetitel_Select;
        public static DataTable Zaivka_Select;
        public static DataTable Ozaivka_Select;
        public static DataTable Time_vhod_Select;
    }
}
