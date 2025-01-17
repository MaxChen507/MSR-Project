﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.BusinessAPI
{
    class BudgetInfoAPI
    {
        public ICollection<Domain.ApproverInfo> GetBudgetHolderList(String bpNo)
        {
            ICollection<Domain.ApproverInfo> budgetHolderData = null;

            using (var context = new MSR_MaxEntities())
            {
                //Log DB commands to console
                context.Database.Log = Console.WriteLine;

                budgetHolderData = new List<Domain.ApproverInfo>();

                var budgetHolderData_db = (from a in context.V_Approver_BP
                                        where a.BP_No.Equals(bpNo)
                                        select new Domain.ApproverInfo { UserId = a.UserId.ToString(), Username = a.Username, FullName = a.FullName, Email = a.Email, DeptId = a.DeptId.ToString() }).ToList();

                budgetHolderData = budgetHolderData_db;
            }

            return budgetHolderData;
        }

    }
}
