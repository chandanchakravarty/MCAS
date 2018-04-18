
/******************************************************************************************
<Author				: - Santosh KUmar Gautam
<Start Date			: -	06-May-2011
<End Date			: -	
<Description		: - 
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: -  
<Modified By		: -  
<Purpose			: - 
*******************************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;
using Cms.Model.Account;
using System.Data;
using Cms.Model.Support;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Xml;


namespace Cms.BusinessLayer.BlAccount
 {
    public class ClsAcceptedCOILoadDetails : Cms.BusinessLayer.BlAccount.ClsAccount
    {
        public ClsAcceptedCOILoadDetails()
        { }

        public DataTable GetImportRequestDetails(Int32 IMPORT_REQUEST_ID, Int32 LANG_ID)
        {
            ClsAcceptedCOILoadInfo ObjImportRequest = new ClsAcceptedCOILoadInfo();

            DataSet ds = ObjImportRequest.GetImportRequestDetails(IMPORT_REQUEST_ID, LANG_ID);

            if (ds.Tables.Count>0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }

           


        }

        public int DeleteFile(ClsAcceptedCOILoadInfo objImportRequest)
        {
            int returnValue = 0;
           if (objImportRequest.RequiredTransactionLog)
           {
               objImportRequest.TransactLabel = ClsCommon.MapTransactionLabel("/account/aspx/AddAcceptedCOILoad.aspx.resx");
               returnValue = objImportRequest.DeleteFile();

           }
           return returnValue;



        }

        public int AddImportRequestFile(ClsAcceptedCOILoadInfo objImportRequest)
        {
            int returnValue = 0;

            if (objImportRequest.RequiredTransactionLog)
            {
                objImportRequest.TransactLabel = ClsCommon.MapTransactionLabel("/account/aspx/AddAcceptedCOILoad.aspx.resx");
                returnValue = objImportRequest.AddImportRequestFile();

            }
            return returnValue;
        }

       

        public int AddImportRequest(ClsAcceptedCOILoadInfo objImportRequest)
        {
            int returnValue = 0;

            if (objImportRequest.RequiredTransactionLog)
            {
                objImportRequest.TransactLabel = ClsCommon.MapTransactionLabel("/account/aspx/AddAcceptedCOILoad.aspx.resx");
                returnValue = objImportRequest.AddImportRequest();

            }
            return returnValue;
        }

        public int UpdateImportRequest(ClsAcceptedCOILoadInfo objImportRequest)
        {
            int returnValue = 0;
            if (objImportRequest.RequiredTransactionLog)
            {
                objImportRequest.TransactLabel = ClsCommon.MapTransactionLabel("/account/aspx/AddAcceptedCOILoad.aspx.resx");
                returnValue = objImportRequest.UpdateImportRequest();

            }
            return returnValue;


                
        }
        #region "GetxmlMethods"

        public DataSet GetAcceptedCOLDetail(Int32 IMPORT_REQUEST_ID, Int32 IMPORT_SERIAL_NO, Int32 LANG_ID ,String  MODE)
        {
            ClsAcceptedCOILoadInfo objClsAcceptedCOILoadDetails = new ClsAcceptedCOILoadInfo();

           return objClsAcceptedCOILoadDetails.GetAcceptedCOLDetail(IMPORT_REQUEST_ID, IMPORT_SERIAL_NO, LANG_ID, MODE);

           



        }
        //public DataSet GetAcceptedCOLDetail(Int32 IMPORT_REQUEST_ID, Int32 IMPORT_SERIAL_NO, Int32 LANG_ID ,String  MODE)
        //{
        //    ClsAcceptedCOILoadDetails objClsAcceptedCOILoadDetails = new ClsAcceptedCOILoadDetails();

        //    string strSql = "PROC_MIG_GET_EXCEPTION_DETAILS";
        //    DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
        //    objDataWrapper.AddParameter("@IMPORT_REQUEST_ID", IMPORT_REQUEST_ID);
        //    objDataWrapper.AddParameter("@IMPORT_SERIAL_NO", IMPORT_SERIAL_NO);
        //    objDataWrapper.AddParameter("@IMPORT_REQUEST_ID", LANG_ID);
        //    objDataWrapper.AddParameter("@MODE", MODE);
        //    DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
        //    return objDataSet;
        //}

        #endregion

        
        #region Initial Load
               

        public int AddInialLoadImportRequest(ClsAcceptedCOILoadInfo objImportRequest)
        {
            int returnValue = 0;

            if (objImportRequest.RequiredTransactionLog)
            {
                objImportRequest.TransactLabel = ClsCommon.MapTransactionLabel("/account/aspx/AddInitialLoad.aspx.resx");
                returnValue = objImportRequest.AddInialLoadImportRequest();

            }
            return returnValue;
        }


        public int AddInialLoadImportRequestFile(ClsAcceptedCOILoadInfo objImportRequest)
        {
            int returnValue = 0;

            if (objImportRequest.RequiredTransactionLog)
            {
                objImportRequest.TransactLabel = ClsCommon.MapTransactionLabel("/account/aspx/AddInitialLoad.aspx.resx");
                returnValue = objImportRequest.AddInialLoadImportRequestFile();

            }
            return returnValue;
        }
        //Added by Pradeep Kr Kushwaha on 27-10-2011
        /// <summary>
        /// Get the Initial Load Layout Coumns details based on Column Type and SUSEP Code
        /// </summary>
        /// <param name="COLUMN_TYPE"></param>
        /// <param name="SUSEP_LOB_CODE"></param>
        /// <returns></returns>
        public DataSet GetInialLoadLayOutCoumnsDetails(Int32 COLUMN_TYPE, String SUSEP_LOB_CODE)
        {
            ClsAcceptedCOILoadInfo ObjImportRequest = new ClsAcceptedCOILoadInfo();

            DataSet ds = ObjImportRequest.GetInialLoadLoadLayOutCoumnsDetail(COLUMN_TYPE, SUSEP_LOB_CODE );

            if (ds.Tables.Count > 0)
            {
                return ds;
            }//if (ds.Tables.Count > 0)
            else
            {
                return null;
            }

        }//public DataTable GetInialLoadLayOutCoumnsDetails(Int32 COLUMN_TYPE, String SUSEP_LOB_CODE)

        public DataTable GetInialLoadImportRequestDetails(Int32 IMPORT_REQUEST_ID, Int32 LANG_ID)
        {
            ClsAcceptedCOILoadInfo ObjImportRequest = new ClsAcceptedCOILoadInfo();

            DataSet ds = ObjImportRequest.GetInialLoadImportRequestDetails(IMPORT_REQUEST_ID, LANG_ID);

            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }

        }

        public int DeleteInialLoadFile(ClsAcceptedCOILoadInfo objImportRequest)
        {
            int returnValue = 0;
            if (objImportRequest.RequiredTransactionLog)
            {
                objImportRequest.TransactLabel = ClsCommon.MapTransactionLabel("/account/aspx/AddInitialLoad.aspx.resx");
                returnValue = objImportRequest.DeleteInialLoadFile();

            }
            return returnValue;



        }
        public int DeleteInialLoadPolicyCommit(ClsAcceptedCOILoadInfo objImportRequest, int _IMPORT_REQUEST_ID, string _IMPORT_SERIAL_NO, string _CALLED_FOR)
        {
            int returnValue = 0;
            if (objImportRequest.RequiredTransactionLog)
            {
                objImportRequest.TransactLabel = ClsCommon.MapTransactionLabel("/account/aspx/AddInitialLoad.aspx.resx");
                returnValue = objImportRequest.DeleteInialLoadCommitedDetails(  _IMPORT_REQUEST_ID,  _IMPORT_SERIAL_NO,  _CALLED_FOR);

            }//if (objImportRequest.RequiredTransactionLog)
            return returnValue;
        }//public int DeleteInialLoadPolicyCommit(ClsAcceptedCOILoadInfo objImportRequest, int _IMPORT_REQUEST_ID, string _IMPORT_SERIAL_NO, string _CALLED_FOR)


        public int UpdateInialLoadImportRequest(ClsAcceptedCOILoadInfo objImportRequest)
        {
            int returnValue = 0;
            if (objImportRequest.RequiredTransactionLog)
            {
                objImportRequest.TransactLabel = ClsCommon.MapTransactionLabel("/account/aspx/AddInitialLoad.aspx.resx");
                returnValue = objImportRequest.UpdateInialLoadImportRequest();

            }
            return returnValue;



        }


        public int StartImportProcess(ClsAcceptedCOILoadInfo objImportRequest)
        {
            int returnValue = 0;
            if (objImportRequest.RequiredTransactionLog)
            {
                objImportRequest.TransactLabel = ClsCommon.MapTransactionLabel("/account/aspx/AddInitialLoad.aspx.resx");
                returnValue = objImportRequest.StartImportProcess();

            }
            return returnValue;



        }


        public DataSet GetInitialLoadImportDetails(Int32 IMPORT_REQUEST_ID, Int32 IMPORT_SERIAL_NO, Int32 LANG_ID, String MODE)
        {
            ClsAcceptedCOILoadInfo objClsAcceptedCOILoadDetails = new ClsAcceptedCOILoadInfo();

            return objClsAcceptedCOILoadDetails.GetInitialLoadImportDetails(IMPORT_REQUEST_ID, IMPORT_SERIAL_NO, LANG_ID, MODE);





        }

        #endregion

       

    }
 }