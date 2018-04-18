//Created by naveen 
//Retention limit Detail.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cms.Model;
using Cms.DataLayer;
using Cms.Model.Maintenance;
using System.Data;
using System.Data.SqlClient;
using System.Xml;


namespace Cms.BusinessLayer.BlCommon
{
    public class ClsRetentionLimit : Cms.BusinessLayer.BlCommon.ClsCommon
    {


        #region Update method
        /// <summary>
        /// Update method that recieves Model object to save.
        /// </summary>
        /// <param name="objOldReinsurerInfo">Model object having old information</param>
        /// <param name="ObjReinsurerInfo">Model object having new information(form control's value)</param>
        /// <returns>No. of rows updated (1 or 0)</returns>
        

        public int Add(ClsRetentionLimitInfo ObjRetentionInfo)
        {
            int returnValue = 0;
            if (ObjRetentionInfo.RequiredTransactionLog)
            {
                ObjRetentionInfo.TransactLabel = ClsCommon.MapTransactionLabel("CmsWeb/Maintenance/AddRetentionLimitDetail.aspx.resx");
                returnValue = ObjRetentionInfo.AddRetentionLimit();
            }
            return returnValue;

        }


        public int Update(ClsRetentionLimitInfo ObjRetentionInfo)
        {
            int returnValue = 0;
            if (ObjRetentionInfo.RequiredTransactionLog)
            {
                ObjRetentionInfo.TransactLabel = ClsCommon.MapTransactionLabel("CmsWeb/Maintenance/AddRetentionLimitDetail.aspx.resx");
                returnValue = ObjRetentionInfo.UpdateRetensionLimitDetail();
            }
            return returnValue;

        }



        public int Delete(ClsRetentionLimitInfo ObjRetentionInfo)
        {
            int returnValue = 0;
            if (ObjRetentionInfo.RequiredTransactionLog)
            {
                ObjRetentionInfo.TransactLabel = ClsCommon.MapTransactionLabel("CmsWeb/Maintenance/AddRetentionLimitDetail.aspx.resx");
                returnValue = ObjRetentionInfo.DeleteRetentionLimit();
            }
            return returnValue;

        }

        public ClsRetentionLimitInfo GetRetentionLimit(int RETENTION_LIMIT_ID)
        {
            DataSet dsretentionlimit = new DataSet();
            string strStoredProc = "Proc_Get_Retention_LIMIT";
            ClsRetentionLimitInfo objRetentionLimit = new ClsRetentionLimitInfo();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
          

           objDataWrapper.AddParameter("@RETENTION_LIMIT_ID", RETENTION_LIMIT_ID);
           dsretentionlimit = objDataWrapper.ExecuteDataSet(strStoredProc);
            if (dsretentionlimit.Tables[0].Rows.Count != 0)
            {              
               Cms.BusinessLayer.BlCommon.ClsCommon.PopulateEbixPageModel(dsretentionlimit, objRetentionLimit);                
                 
            }
            objDataWrapper.ClearParameteres();
            objDataWrapper.Dispose();
            return objRetentionLimit;

        }

        public static DataSet GetSUSEPSUBLOBs(string LANG_ID) //Added BY ADITYA FOR TFS BUG # 404
        {
            string strSql = "PROC_GET_SUBLOB";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@LANG_ID", LANG_ID);
            DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
            return objDataSet;
        }

        #endregion
    }
}
