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

        public void UpdateBusinessSingletonFormItemList(DataGridView formItemsDGV)
        {
            //Data List Initialization
            ICollection<Domain.FormItems> formItemData_List = new List<Domain.FormItems>();

            //Copy data from data grid view and populate addListData
            foreach (DataGridViewRow row in formItemsDGV.Rows)
            {
                String BudgetPool = row.Cells["BudgetPool"].FormattedValue.ToString();
                String ItemCode = row.Cells["ItemCode"].FormattedValue.ToString();
                String ItemDesc = row.Cells["ItemDesc"].FormattedValue.ToString();
                String Unit = row.Cells["Unit"].FormattedValue.ToString();
                String AC_No = row.Cells["AC_No"].FormattedValue.ToString();

                Domain.FormItems addItem = new Domain.FormItems(BudgetPool, ItemCode, ItemDesc, "1", Unit, "", "", "", "", AC_No);

                formItemData_List.Add(addItem);
            }

            BusinessAPI.BusinessSingleton.Instance.formItemList = formItemData_List;
        }

        public void ResetAllControls(Control form)
        {
            foreach (Control control in form.Controls)
            {
                //MessageBox.Show(control.Name.ToString());
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

    }
}