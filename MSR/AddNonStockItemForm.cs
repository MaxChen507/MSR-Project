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
    public partial class AddNonStockItemForm : Form
    {
        public AddNonStockItemForm()
        {
            InitializeComponent();
        }

        private void ApplyClose_AddNonStock_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
