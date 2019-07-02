using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSR.UserInterfaceAPI
{
    class UI_Singleton
    {
        //Singleton instance
        private static UI_Singleton instance;

        //User variables
        public BindingSource mainDGV { get; set; }

        public ICollection<Domain.FormItems> formItemsList { get; set; }

        private UI_Singleton()
        {
            mainDGV = new BindingSource();
            formItemsList = new List<Domain.FormItems>();
        }

        public static UI_Singleton Instance
        {
            //not thread safe???
            get
            {
                if (instance == null)
                {
                    instance = new UI_Singleton();
                }
                return instance;
            }
        }

        public void ReInitializeDataSource()
        {
            mainDGV.DataSource = formItemsList;
        }
    }
}
