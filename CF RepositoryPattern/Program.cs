using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.Interfaces;
using Ninject;
using BLL.Util;
using System.Reflection;
using System.Configuration;

namespace CF_RepositoryPattern
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string connection = ConfigurationManager.ConnectionStrings["Mongodb"].ConnectionString;
            var kernel = new StandardKernel(new NinjectRegister(), new ServiceModule(connection));
           
            IDbCrud dbCrud = kernel.Get<IDbCrud>();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run( new Menu(dbCrud));
        }
    }
}
