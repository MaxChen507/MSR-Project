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
        //Form variables
        String Bp_No;

        //Binding Source Initialization
        BindingSource itemListDGV_source = new BindingSource();

        //Data List Initialization
        ICollection<Domain.StockItems> stockItemsData = null;

        public AddStockItemForm(String Bp_No)
        {
            InitializeComponent();
            this.Bp_No = Bp_No;
            ItemListDGV_Load();
        }

        private void ItemListDGV_Load()
        {
            try
            {
                stockItemsData = DatabaseAPI.DBAccessSingleton.Instance.StockItemsAPI.GetStockItem_List(Bp_No);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }

            if (stockItemsData == null)
            {
                MessageBox.Show("DB error");
                return;
            }

            itemListDGV_source.DataSource = stockItemsData;
            itemList_addStock_dataGridView.DataSource = itemListDGV_source;

            itemList_addStock_dataGridView.ClearSelection();
        }


        private void ApplyClose_AddStock_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
