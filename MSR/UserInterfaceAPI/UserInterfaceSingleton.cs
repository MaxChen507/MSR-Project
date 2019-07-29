using System;
using System.Collections.Generic;
using System.Drawing;
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

        public ICollection<Domain.FormItems> ConvertFormItemDGVToFormItemList(DataGridView formItemsDGV)
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

        public void CustomDGVClear(DataGridView dgv)
        {
            dgv.DataSource = null;
            dgv.Rows.Clear();
            dgv.Refresh();

            dgv.ClearSelection();
        }

        public Boolean CheckMSRFormItemsDGV(DataGridView dataGridView, String budgetPool, String usrCAId)
        {
            //Unselect DataGridView
            dataGridView.ClearSelection();

            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("Please add an item.");
                return false;
            }

            //Checking if all item's fields are correct
            Boolean itemsCorrectFlag = true;

            //Checking if all items's budget pool matches combobox budget pool
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (!(row.Cells["BudgetPool"].FormattedValue.ToString().Equals(budgetPool)))
                {
                    Color lightRed = ControlPaint.Light(Color.Red);
                    row.Cells["BudgetPool"].Style.BackColor = lightRed;
                    itemsCorrectFlag = false;
                }
                else
                {
                    row.Cells["BudgetPool"].Style.BackColor = (Color)System.Drawing.SystemColors.Window;
                }
            }

            if (itemsCorrectFlag)
            {

            }
            else
            {
                MessageBox.Show("Highlighted item's budget pool doesn't match with selected Budget Pool.");
                return false;
            }

            //Checking if all items's AC_No matches combobox approver
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (!BusinessAPI.BusinessSingleton.Instance.CheckACNoCAIdMatch(row.Cells["AC_No"].FormattedValue.ToString(), usrCAId))
                {
                    Color lightRed = ControlPaint.Light(Color.Red);
                    row.Cells["AC_No"].Style.BackColor = lightRed;
                    itemsCorrectFlag = false;
                }
                else
                {
                    row.Cells["AC_No"].Style.BackColor = (Color)System.Drawing.SystemColors.Window;
                }
            }

            if (itemsCorrectFlag)
            {

            }
            else
            {
                MessageBox.Show("Highlighted item's AC_No doesn't match with selected Approver.");
                return false;
            }

            //Checking if all numeric fields are numeric
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (!BusinessAPI.BusinessSingleton.Instance.IsNumeric(row.Cells["Quantity"].FormattedValue.ToString()))
                {
                    Color lightRed = ControlPaint.Light(Color.Red);
                    row.Cells["Quantity"].Style.BackColor = lightRed;
                    itemsCorrectFlag = false;
                }
                else
                {
                    row.Cells["Quantity"].Style.BackColor = (Color)System.Drawing.SystemColors.Window;
                }

                if (!BusinessAPI.BusinessSingleton.Instance.IsNumeric(row.Cells["UnitPrice"].FormattedValue.ToString()) && !String.IsNullOrEmpty(row.Cells["UnitPrice"].FormattedValue.ToString()))
                {
                    Color lightRed = ControlPaint.Light(Color.Red);
                    row.Cells["UnitPrice"].Style.BackColor = lightRed;
                    itemsCorrectFlag = false;
                }
                else
                {
                    row.Cells["UnitPrice"].Style.BackColor = (Color)System.Drawing.SystemColors.Window;
                }
            }

            if (itemsCorrectFlag)
            {

            }
            else
            {
                MessageBox.Show("Highlighted items' fields must be corrected to a (decimal) number.");
                return false;
            }

            //Checking if all required form item fields are set
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (String.IsNullOrEmpty(row.Cells["Quantity"].FormattedValue.ToString()))
                {
                    Color lightRed = ControlPaint.Light(Color.Red);
                    row.Cells["Quantity"].Style.BackColor = lightRed;
                    itemsCorrectFlag = false;
                }
                else
                {
                    row.Cells["Quantity"].Style.BackColor = (Color)System.Drawing.SystemColors.Window;
                }
            }

            if (itemsCorrectFlag)
            {

            }
            else
            {
                MessageBox.Show("Highlighted items' fields must be filled.");
                return false;
            }

            return true;
        }

        public void CustomDGVSetColumnResize(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Columns.Count - 1; i++)
            {
                dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            dgv.Columns[dgv.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                int colw = dgv.Columns[i].Width;
                dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns[i].Width = colw;
            }
        }

    }
}