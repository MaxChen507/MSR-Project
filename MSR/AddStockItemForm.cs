using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSR
{
    public partial class AddStockItemForm : Form
    {
        public AddStockItemForm()
        {
            InitializeComponent();
        }

        private void ApplyClose_AddStock_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
