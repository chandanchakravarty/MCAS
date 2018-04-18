using System;
using System.Collections.Generic;
using System.Linq;
using Cms.Model.Support;
using System.Data;
using Cms.DataLayer;
using System.Collections;
using Cms.Model.Policy;
using Cms.BusinessLayer.BlCommon;

namespace Cms.BusinessLayer.BlApplication
{
    public class ClsPolicyUWQuestion : Cms.BusinessLayer.BlApplication.clsapplication
    {
         #region Constructors
        /// <summary>
        /// deafault constructor
        /// </summary>
        public ClsPolicyUWQuestion()
        {
        }
        #endregion

        public DataSet GetUWQuestions(int LOB_ID)
        {
            string strStoredProc = "Proc_GetUWQuestions";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            objWrapper.AddParameter("@LOB_ID", LOB_ID);


            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

            return ds;
        }
    }
}
