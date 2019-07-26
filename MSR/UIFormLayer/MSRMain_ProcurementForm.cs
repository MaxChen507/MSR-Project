using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSR.UIFormLayer
{
    public partial class MSRMain_ProcurementForm : Form
    {
        //Binding Source Variables
        BindingSource approvedTabDGVSource = new BindingSource();

        public MSRMain_ProcurementForm()
        {
            InitializeComponent();
        }
        
        private void MSRMain_ProcurementForm_Load(object sender, EventArgs e)
        {
            InitalizeStartingFields();
            RefreshDataGridViews();
        }

        private void InitalizeStartingFields()
        {
            //No starting fields
        }

        private void RefreshDataGridViews()
        {
            //DGV clear
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.Custom_DGV_Clear(approvedTab_dataGridView);

            //Populate approvedTab_dataGridView from BusinessAPI
            approvedTabDGVSource.DataSource = BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI.GetShowMSRListProcure(Domain.WorkFlowTrace.APPROVED);
            approvedTab_dataGridView.DataSource = approvedTabDGVSource;
            approvedTab_dataGridView.ClearSelection();
        }

        private void PopulateFilteredShowMSRItemListDGV(String workFlowTrace, String idSearchText, String deptSearchText, String ogSearchText, String apSearchText, BindingSource tabDGVSource, DataGridView dataGridView)
        {
            ICollection<Domain.ShowMSRItem> showMSRItemData = BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI.GetShowMSRListProcure(workFlowTrace);

            ICollection<Domain.ShowMSRItem> showMSRItemDatafilter = BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI.GetFilterShowMSRList(showMSRItemData, idSearchText, deptSearchText, ogSearchText, apSearchText);

            if (showMSRItemDatafilter == null)
            {
                MessageBox.Show("DB error");
                return;
            }

            tabDGVSource.DataSource = showMSRItemDatafilter;
            dataGridView.DataSource = tabDGVSource;
            dataGridView.ClearSelection();
        }


        #region ApprovedTab Code Block

        private void IdSearch_approvedTab_textBox_TextChanged(object sender, EventArgs e)
        {
            PopulateFilteredShowMSRItemListDGV(Domain.WorkFlowTrace.APPROVED, idSearch_approvedTab_textBox.Text, deptSearch_approvedTab_textBox.Text, ogSearch_approvedTab_textBox.Text, apSearch_approvedTab_textBox.Text, approvedTabDGVSource, approvedTab_dataGridView);
        }

        private void DeptSearch_approvedTab_textBox_TextChanged(object sender, EventArgs e)
        {
            PopulateFilteredShowMSRItemListDGV(Domain.WorkFlowTrace.APPROVED, idSearch_approvedTab_textBox.Text, deptSearch_approvedTab_textBox.Text, ogSearch_approvedTab_textBox.Text, apSearch_approvedTab_textBox.Text, approvedTabDGVSource, approvedTab_dataGridView);
        }

        private void OgSearch_approvedTab_textBox_TextChanged(object sender, EventArgs e)
        {
            PopulateFilteredShowMSRItemListDGV(Domain.WorkFlowTrace.APPROVED, idSearch_approvedTab_textBox.Text, deptSearch_approvedTab_textBox.Text, ogSearch_approvedTab_textBox.Text, apSearch_approvedTab_textBox.Text, approvedTabDGVSource, approvedTab_dataGridView);
        }

        private void ApSearch_approvedTab_textBox_TextChanged(object sender, EventArgs e)
        {
            PopulateFilteredShowMSRItemListDGV(Domain.WorkFlowTrace.APPROVED, idSearch_approvedTab_textBox.Text, deptSearch_approvedTab_textBox.Text, ogSearch_approvedTab_textBox.Text, apSearch_approvedTab_textBox.Text, approvedTabDGVSource, approvedTab_dataGridView);
        }

        private void ApprovedTab_dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(approvedTab_dataGridView.SelectedRows[0].Cells["MSRId"].FormattedValue.ToString());

            //Check if header is selected
            if (e.RowIndex == -1) return;

            this.Hide();

            UIFormLayer.ShowMSR_Approved fShowMSR = new UIFormLayer.ShowMSR_Approved(approvedTab_dataGridView.SelectedRows[0].Cells["MSRId"].FormattedValue.ToString());

            fShowMSR.ShowDialog();

            //Update state of DGV
            RefreshDataGridViews();

            this.Show();
        }

        #endregion


    }
}
