using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSR.UserInterfaceAPI
{
    class UserInterfaceSIngleton
    {
        //Singleton instance
        private static UserInterfaceSIngleton instance;

        //User variables


        private UserInterfaceSIngleton()
        {

        }

        public static UserInterfaceSIngleton Instance
        {
            //not thread safe???
            get
            {
                if (instance == null)
                {
                    instance = new UserInterfaceSIngleton();
                }
                return instance;
            }
        }

        public ICollection<Domain.FormItems> ConvertFormItemDGV_ToFormItemList(DataGridView formItemsDGV)
        {
            //Data List Initialization
            ICollection<Domain.FormItems> formItemData_List = new List<Domain.FormItems>();

            //Copy data from data grid view and populate addListData
            foreach (DataGridViewRow row in formItemsDGV.Rows)
            {
                String BudgetPool = row.Cells["BudgetPool"].FormattedValue.ToString();
                String ItemCode = row.Cells["ItemCode"].FormattedValue.ToString();
                String ItemDesc = row.Cells["ItemDesc"].FormattedValue.ToString();
                String Quantity = row.Cells["Quantity"].FormattedValue.ToString();
                String Unit = row.Cells["Unit"].FormattedValue.ToString();
                String UnitPrice = row.Cells["UnitPrice"].FormattedValue.ToString();
                String Currency = row.Cells["Currency"].FormattedValue.ToString();
                //DateTime Parse
                DateTime ROS_Date = DateTime.Parse(row.Cells["ROS_Date"].FormattedValue.ToString());
                String Comments = row.Cells["Comments"].FormattedValue.ToString();
                String AC_No = row.Cells["AC_No"].FormattedValue.ToString();

                Domain.FormItems addItem = new Domain.FormItems(BudgetPool, ItemCode, ItemDesc, Quantity, Unit, UnitPrice, Currency, ROS_Date, Comments, AC_No);

                formItemData_List.Add(addItem);
            }

            return formItemData_List;
        }

        public void ResetAllControls(Control form)
        {
            foreach (Control control in form.Controls)
            {
                //Check all Controls in each GroupBox
                if (control is GroupBox)
                {
                    ResetAllControls(control);
                }

                if (control is TextBox)
                {
                    TextBox textBox = (TextBox)control;
                    textBox.Text = null;
                }

                if (control is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)control;
                    if (comboBox.Items.Count > 0)
                        comboBox.SelectedIndex = 0;
                }

                if (control is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)control;
                    checkBox.Checked = false;
                }

                if (control is ListBox)
                {
                    ListBox listBox = (ListBox)control;
                    listBox.ClearSelected();
                }

                if (control is DataGridView)
                {
                    DataGridView dataGridView = (DataGridView)control;
                    dataGridView.DataSource = null;
                    dataGridView.Rows.Clear();
                    dataGridView.Refresh();
                }
            }
        }

        public void Custom_DGV_Clear(DataGridView DGV)
        {
            DGV.DataSource = null;
            DGV.Rows.Clear();
            DGV.Refresh();

            DGV.ClearSelection();
        }

    }
}