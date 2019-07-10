using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.DatabaseAPI
{
    class MSRInfoAPI
    {
        public MSRInfoAPI()
        {

        }

        public int InsertInitialMSR(Domain.MSRInfo msrInfo)
        {
            int MSR_Id;

            SqlParameter project_param = new SqlParameter("@project", msrInfo.Project);
            SqlParameter wvl_param = new SqlParameter("@wvl", msrInfo.WVL);
            SqlParameter comments_param = new SqlParameter("@comments", msrInfo.Comments);
            SqlParameter budgetYear_param = new SqlParameter("@budgetYear", Convert.ToInt32(msrInfo.BudgetYear));
            SqlParameter budgetPool_param = new SqlParameter("@budgetPool", Convert.ToInt32(msrInfo.BP_No));
            SqlParameter afe_param = new SqlParameter("@afe", msrInfo.AFE);
            SqlParameter sugVendor_param = new SqlParameter("@sugVendor", msrInfo.SugVendor);
            SqlParameter contactVendor_param = new SqlParameter("@contactVendor", msrInfo.ContactVendor);
            SqlParameter request_Originator_param = new SqlParameter("@request_Originator", Convert.ToInt32(msrInfo.Request_Originator));
            SqlParameter company_Approval_param = new SqlParameter("@company_Approval", Convert.ToInt32(msrInfo.Company_Approval));
            SqlParameter req_Date_param = new SqlParameter("@req_Date", msrInfo.Req_Date);
            SqlParameter appr_Date_param = new SqlParameter("@appr_Date", msrInfo.Appr_Date.HasValue ? msrInfo.Appr_Date : Convert.DBNull);
            SqlParameter recieve_By_param = new SqlParameter("@recieve_By", String.IsNullOrEmpty(msrInfo.Recieve_By) ? Convert.DBNull : Convert.ToInt32(msrInfo.Recieve_By));
            //SqlParameter recieve_By_param = new SqlParameter("@recieve_By", Convert.DBNull);
            SqlParameter recieve_Date_param = new SqlParameter("@recieve_Date", msrInfo.Recieve_Date.HasValue ? msrInfo.Recieve_Date : Convert.DBNull);
            SqlParameter pur_Comment_param = new SqlParameter("@pur_Comment", msrInfo.PUR_Comment);
            SqlParameter decline_comments_param = new SqlParameter("@decline_comments", msrInfo.Decline_Comment);
            SqlParameter review_Comment_param = new SqlParameter("@review_Comment", msrInfo.Review_Comment);
            SqlParameter stateFlag_param = new SqlParameter("@stateFlag", msrInfo.StateFlag);

            List<SqlParameter> sqlParametersList = new List<SqlParameter>();
            sqlParametersList.Add(project_param);
            sqlParametersList.Add(wvl_param);
            sqlParametersList.Add(comments_param);
            sqlParametersList.Add(budgetYear_param);
            sqlParametersList.Add(budgetPool_param);
            sqlParametersList.Add(afe_param);
            sqlParametersList.Add(sugVendor_param);
            sqlParametersList.Add(contactVendor_param);
            sqlParametersList.Add(request_Originator_param);
            sqlParametersList.Add(company_Approval_param);
            sqlParametersList.Add(req_Date_param);
            sqlParametersList.Add(appr_Date_param);
            sqlParametersList.Add(recieve_By_param);
            sqlParametersList.Add(recieve_Date_param);
            sqlParametersList.Add(pur_Comment_param);
            sqlParametersList.Add(decline_comments_param);
            sqlParametersList.Add(review_Comment_param);
            sqlParametersList.Add(stateFlag_param);

            String sql = "INSERT INTO MSR (Project, WVL, Comments, BudgetYear, BP_No, AFE, SugVendor, ContactVendor, Request_Originator, Company_Approval, Req_Date, Appr_Date, Recieve_By, Recieve_Date, PUR_Comment, Decline_Comment, Review_Comment, StateFlag) VALUES (@project, @wvl, @comments, @budgetYear, @budgetPool, @afe, @sugVendor, @contactVendor, @request_Originator, @company_Approval, @req_Date, @appr_Date, @recieve_By, @recieve_Date, @pur_Comment, @decline_comments, @review_Comment, @stateFlag) SELECT SCOPE_IDENTITY()";

            MSR_Id = DatabaseAPI.DBAccessSingleton.Instance.MyExecuteInsertStmt_GetIdentity(sql, sqlParametersList);

            return MSR_Id;
        }

        public void InsertInitialFormItems(Domain.FormItems formItemsInfo, int MSRId)
        {
            SqlParameter itemCode_param = new SqlParameter("@itemCode", formItemsInfo.ItemCode);
            SqlParameter itemDesc_param = new SqlParameter("@itemDesc", formItemsInfo.ItemDesc);
            SqlParameter quantity_param = new SqlParameter("@quantity", Convert.ToDouble(formItemsInfo.Quantity));
            SqlParameter unit_param = new SqlParameter("@unit", formItemsInfo.Unit);
            SqlParameter unitPrice_param = new SqlParameter("@unitPrice", String.IsNullOrEmpty(formItemsInfo.UnitPrice) ? Convert.DBNull : Convert.ToDouble(formItemsInfo.UnitPrice));
            SqlParameter currency_param = new SqlParameter("@currency", formItemsInfo.Currency);
            SqlParameter ROS_Date_param = new SqlParameter("@ROS_Date", formItemsInfo.ROS_Date);
            SqlParameter comments_param = new SqlParameter("@comments", formItemsInfo.Comments);
            SqlParameter MSRId_param = new SqlParameter("@MSRId", MSRId.ToString());
            SqlParameter AC_No_param = new SqlParameter("@AC_No", formItemsInfo.AC_No);

            List<SqlParameter> sqlParametersList = new List<SqlParameter>();
            sqlParametersList.Add(itemCode_param);
            sqlParametersList.Add(itemDesc_param);
            sqlParametersList.Add(quantity_param);
            sqlParametersList.Add(unit_param);
            sqlParametersList.Add(unitPrice_param);
            sqlParametersList.Add(currency_param);
            sqlParametersList.Add(ROS_Date_param);
            sqlParametersList.Add(comments_param);
            sqlParametersList.Add(MSRId_param);
            sqlParametersList.Add(AC_No_param);

            String sql = "INSERT INTO FormItems (ItemCode, ItemDesc, Quantity, Unit, UnitPrice, Currency, ROS_Date, Comments, MSRId, AC_No) VALUES (@itemCode, @itemDesc, @quantity, @unit, @unitPrice, @currency, @ROS_Date, @comments, @MSRId, @AC_No)";

            DatabaseAPI.DBAccessSingleton.Instance.MyExecuteInsertStmt(sql, sqlParametersList);
        }

        public void DeleteFormItems(int MSRId)
        {
            SqlParameter MSRId_param = new SqlParameter("@MSRId", MSRId.ToString());

            List<SqlParameter> sqlParametersList = new List<SqlParameter>();
            sqlParametersList.Add(MSRId_param);

            String sql = "Delete FROM FormItems where MSRId = @MSRId";

            DatabaseAPI.DBAccessSingleton.Instance.MyExecuteDeleteStmt(sql, sqlParametersList);
        }

        public void UpdateMSR_SubmitReview(int MSRId, String StateFlag)
        {
            //Update Approve Date
            SqlParameter MSRId_param = new SqlParameter("@MSRId", MSRId.ToString());
            SqlParameter stateFlag_param = new SqlParameter("@stateFlag", StateFlag);

            List<SqlParameter> sqlParametersList = new List<SqlParameter>();
            sqlParametersList.Add(MSRId_param);
            sqlParametersList.Add(stateFlag_param);

            String sql = "UPDATE MSR SET StateFlag = @stateFlag WHERE MSRId = @MSRId";

            DatabaseAPI.DBAccessSingleton.Instance.MyExecuteUpdateStmt(sql, sqlParametersList);
        }

        public void UpdateMSR_ApproveButton(int MSRId, String ApproveButton, String ApproveComments, DateTime ApprovalDate, String StateFlag)
        {
            if (ApproveButton.Equals("Approve"))
            {
                //Update Approve Date
                SqlParameter MSRId_param = new SqlParameter("@MSRId", MSRId.ToString());
                SqlParameter appr_Date_param = new SqlParameter("@appr_Date", ApprovalDate);
                SqlParameter stateFlag_param = new SqlParameter("@stateFlag", StateFlag);

                List<SqlParameter> sqlParametersList = new List<SqlParameter>();
                sqlParametersList.Add(MSRId_param);
                sqlParametersList.Add(appr_Date_param);
                sqlParametersList.Add(stateFlag_param);

                String sql = "UPDATE MSR SET appr_Date = @appr_Date, StateFlag = @stateFlag WHERE MSRId = @MSRId";

                DatabaseAPI.DBAccessSingleton.Instance.MyExecuteUpdateStmt(sql, sqlParametersList);
            }
            else if (ApproveButton.Equals("Send for Review"))
            {
                //Update Review Comment
                SqlParameter MSRId_param = new SqlParameter("@MSRId", MSRId.ToString());
                SqlParameter review_Comment_param = new SqlParameter("@review_Comment", ApproveComments);
                SqlParameter stateFlag_param = new SqlParameter("@stateFlag", StateFlag);

                List<SqlParameter> sqlParametersList = new List<SqlParameter>();
                sqlParametersList.Add(MSRId_param);
                sqlParametersList.Add(review_Comment_param);
                sqlParametersList.Add(stateFlag_param);

                String sql = "UPDATE MSR SET Review_Comment = @review_Comment, StateFlag = @stateFlag WHERE MSRId = @MSRId";

                DatabaseAPI.DBAccessSingleton.Instance.MyExecuteUpdateStmt(sql, sqlParametersList);
            }
            else if (ApproveButton.Equals("Decline"))
            {
                //Update Review Comment
                SqlParameter MSRId_param = new SqlParameter("@MSRId", MSRId.ToString());
                SqlParameter decline_comments_param = new SqlParameter("@decline_comments", ApproveComments);
                SqlParameter stateFlag_param = new SqlParameter("@stateFlag", StateFlag);

                List<SqlParameter> sqlParametersList = new List<SqlParameter>();
                sqlParametersList.Add(MSRId_param);
                sqlParametersList.Add(decline_comments_param);
                sqlParametersList.Add(stateFlag_param);

                String sql = "UPDATE MSR SET Decline_Comment = @decline_comments, StateFlag = @stateFlag WHERE MSRId = @MSRId";

                DatabaseAPI.DBAccessSingleton.Instance.MyExecuteUpdateStmt(sql, sqlParametersList);
            }
            else
            {

            }
        }

        public Domain.MSRInfo GetMSR(String _MSRId)
        {
            Domain.MSRInfo MSRInfoData = null;

            SqlParameter MSRId_param = new SqlParameter("@MSRId", _MSRId);

            List<SqlParameter> sqlParametersList = new List<SqlParameter>();
            sqlParametersList.Add(MSRId_param);

            String sql = "SELECT * FROM V_ShowMSRForm WHERE MSRId = @MSRId";

            using (SqlDataReader dataReader = DBAccessSingleton.Instance.MyExecuteQuery(sql, sqlParametersList))
            {


                while (dataReader.Read())
                {
                    //Project, WVL, Comments, BudgetYear, BP_No, AFE, SugVendor, ContactVendor, Request_Originator, Company_Approval, Req_Date, Appr_Date, Recieve_By, Recieve_Date, PUR_Comment, Decline_Comment, Review_Comment, StateFlag

                    String MSRId = dataReader["MSRId"].ToString();
                    String Project = dataReader["Project"].ToString();
                    String WVL = dataReader["WVL"].ToString();
                    String Comments = dataReader["Comments"].ToString();
                    String BudgetYear = dataReader["BudgetYear"].ToString();
                    String BP_No = dataReader["BP_No"].ToString();
                    String AFE = dataReader["AFE"].ToString();
                    String SugVendor = dataReader["SugVendor"].ToString();
                    String ContactVendor = dataReader["ContactVendor"].ToString();
                    String Request_Originator = dataReader["Request_Originator"].ToString();
                    String Company_Approval = dataReader["Company_Approval"].ToString();
                    DateTime Req_Date = DateTime.Parse(dataReader["Req_Date"].ToString());

                    DateTime? Appr_Date = null;
                    if (!dataReader.IsDBNull(dataReader.GetOrdinal("Appr_Date")))
                    {
                        Appr_Date = DateTime.Parse(dataReader["Appr_Date"].ToString());
                    }

                    String Recieve_By = "";
                    if (!dataReader.IsDBNull(dataReader.GetOrdinal("Recieve_By")))
                    {
                        Recieve_By = dataReader["Recieve_By"].ToString();
                    }

                    DateTime? Recieve_Date = null;
                    if (!dataReader.IsDBNull(dataReader.GetOrdinal("Recieve_Date")))
                    {
                        Recieve_Date = DateTime.Parse(dataReader["Recieve_Date"].ToString());
                    }

                    String PUR_Comment = dataReader["PUR_Comment"].ToString();
                    String Decline_Comment = dataReader["Decline_Comment"].ToString();
                    String Review_Comment = dataReader["Review_Comment"].ToString();
                    String StateFlag = dataReader["StateFlag"].ToString();

                    MSRInfoData = new Domain.MSRInfo(MSRId, Project, WVL, Comments, BudgetYear, BP_No, AFE, SugVendor, ContactVendor, Request_Originator, Company_Approval, Req_Date, Appr_Date, Recieve_By, Recieve_Date, PUR_Comment, Decline_Comment, Review_Comment, StateFlag);
                }

                dataReader.Close();

            }
            return MSRInfoData;
        }

        public String GetOriginatorID(String MSRId)
        {
            String OgId = null;

            SqlParameter MSRId_param = new SqlParameter("@MSRId", MSRId);

            List<SqlParameter> sqlParametersList = new List<SqlParameter>();
            sqlParametersList.Add(MSRId_param);

            String sql = "SELECT Request_Originator FROM MSR WHERE MSRId = @MSRId";

            using (SqlDataReader dataReader = DBAccessSingleton.Instance.MyExecuteQuery(sql, sqlParametersList))
            {


                while (dataReader.Read())
                {
                    //Project, WVL, Comments, BudgetYear, BP_No, AFE, SugVendor, ContactVendor, Request_Originator, Company_Approval, Req_Date, Appr_Date, Recieve_By, Recieve_Date, PUR_Comment, Decline_Comment, Review_Comment, StateFlag

                    String Request_Originator = dataReader["Request_Originator"].ToString();

                    OgId = Request_Originator;
                }

                dataReader.Close();
            }

            return OgId;
        }

        public ICollection<Domain.FormItems> GetFormItems_List(String MSRId, String BP_No)
        {
            ICollection<Domain.FormItems> formItemsData = null;

            SqlParameter MSRId_param = new SqlParameter("@MSRId", MSRId);

            List<SqlParameter> sqlParametersList = new List<SqlParameter>();
            sqlParametersList.Add(MSRId_param);

            String sql = "SELECT ItemCode, ItemDesc, Quantity, Unit, UnitPrice, Currency, ROS_Date, Comments, AC_No FROM FormItems WHERE MSRId = @MSRId";

            using (SqlDataReader dataReader = DBAccessSingleton.Instance.MyExecuteQuery(sql, sqlParametersList))
            {
                formItemsData = new List<Domain.FormItems>();

                while (dataReader.Read())
                {
                    String BudgetPool = BP_No;
                    String ItemCode = dataReader["ItemCode"].ToString();
                    String ItemDesc = dataReader["ItemDesc"].ToString();
                    String Quantity = dataReader["Quantity"].ToString();
                    String Unit = dataReader["Unit"].ToString();
                    String UnitPrice = dataReader["UnitPrice"].ToString();
                    String Currency = dataReader["Currency"].ToString();
                    DateTime ROS_Date = DateTime.Parse(dataReader["ROS_Date"].ToString());
                    String Comments = dataReader["Comments"].ToString();
                    String AC_No = dataReader["AC_No"].ToString();

                    Domain.FormItems temp = new Domain.FormItems(BudgetPool, ItemCode, ItemDesc, Quantity, Unit, UnitPrice, Currency, ROS_Date, Comments, AC_No);
                    formItemsData.Add(temp);
                }

                dataReader.Close();

            }
            return formItemsData;
        }

        public ICollection<Domain.ShowMSRItem> GetshowMSR_List(String DeptId, String StateFlag)
        {
            ICollection<Domain.ShowMSRItem> showMSRData = null;

            SqlParameter deptId_param = new SqlParameter("@DeptId", DeptId);
            SqlParameter stateFlag_param = new SqlParameter("@StateFlag", StateFlag);

            List<SqlParameter> sqlParametersList = new List<SqlParameter>();
            sqlParametersList.Add(deptId_param);
            sqlParametersList.Add(stateFlag_param);

            String sql = "SELECT MSRId, Bp_No, DeptName, Originator, Approver, Req_Date, Comments, Appr_Date FROM V_ShowMSR WHERE DeptId = @DeptId AND StateFlag = @StateFlag";

            using (SqlDataReader dataReader = DBAccessSingleton.Instance.MyExecuteQuery(sql, sqlParametersList))
            {
                showMSRData = new List<Domain.ShowMSRItem>();

                while (dataReader.Read())
                {
                    String MSRId = dataReader["MSRId"].ToString();
                    String Bp_No = dataReader["Bp_No"].ToString();
                    String DeptName = dataReader["DeptName"].ToString();
                    String Originator = dataReader["Originator"].ToString();
                    String Approver = dataReader["Approver"].ToString();
                    DateTime Req_Date = DateTime.Parse(dataReader["Req_Date"].ToString());
                    String Comments = dataReader["Comments"].ToString();

                    String Appr_Date = "No Approval Yet";
                    if (!dataReader.IsDBNull(dataReader.GetOrdinal("Appr_Date")))
                    {
                        Appr_Date = Convert.ToDateTime(dataReader["Appr_Date"]).ToString("M/d/yyyy");
                    }

                    Domain.ShowMSRItem temp = new Domain.ShowMSRItem(MSRId, Bp_No, DeptName, Originator, Approver, Req_Date, Comments, Appr_Date);
                    showMSRData.Add(temp);
                }

                dataReader.Close();

            }
            return showMSRData;
        }

        public ICollection<Domain.ShowMSRItem> GetFiltershowMSR_List(ICollection<Domain.ShowMSRItem> showMSRList, string searchMSRID, string searchDept, string searchOg, string searchAp)
        {

            List<string> searchMSRIDList = searchMSRID.Split(' ').ToList();

            List<string> searchDeptList = searchDept.Split(' ').ToList();

            List<string> searchOgList = searchOg.Split(' ').ToList();

            List<string> searchApList = searchAp.Split(' ').ToList();

            ICollection<Domain.ShowMSRItem> results = (
                                           from item in showMSRList
                                           where
                                                searchMSRIDList.All(sTring => item.MSRId.ToUpperInvariant().Contains(sTring.ToUpperInvariant()))
                                                &&
                                                searchDeptList.All(sTring => item.DeptName.ToUpperInvariant().Contains(sTring.ToUpperInvariant()))
                                                &&
                                                searchOgList.All(sTring => item.Originator.ToUpperInvariant().Contains(sTring.ToUpperInvariant()))
                                                &&
                                                searchApList.All(sTring => item.Approver.ToUpperInvariant().Contains(sTring.ToUpperInvariant()))
                                           select item
                                           ).ToList();

            return results;
        }


    }
}
