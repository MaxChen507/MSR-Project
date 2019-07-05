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

        public int CreateInitialMSR(Domain.MSRInfo msrInfo)
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
    }
}
