/******************************************************************************************
<Author				: -   Gaurav
<Start Date				: -	8/26/2005 12:28:15 PM
<End Date				: -	
<Description				: - 	This file is used for insert and update endorsments
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Maintenance;
using System.Web.UI.WebControls;

namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsEndorsmentDetails : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		private const	string		MNT_ENDORSMENT_DETAILS			=	"MNT_ENDORSMENT_DETAILS";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		//private int _ENDORSMENT_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateMNT_ENDORSMENT_DETAILS";
		#endregion

		#region Public Properties
		public bool TransactionLog
		{
			set
			{
				boolTransactionLog	=	value;
			}
			get
			{
				return boolTransactionLog;
			}
		}
		#endregion
	
		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsEndorsmentDetails()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objEndorsmentDetailsInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsEndorsmentDetailsInfo objEndorsmentDetailsInfo)
		{
			string		strStoredProc	=	"Proc_InsertEndorsmentDetails";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@STATE_ID",objEndorsmentDetailsInfo.STATE_ID);
				objDataWrapper.AddParameter("@LOB_ID",objEndorsmentDetailsInfo.LOB_ID);
				objDataWrapper.AddParameter("@PURPOSE",objEndorsmentDetailsInfo.PURPOSE);
				objDataWrapper.AddParameter("@TYPE",objEndorsmentDetailsInfo.TYPE);
				objDataWrapper.AddParameter("@DESCRIPTION",objEndorsmentDetailsInfo.DESCRIPTION);
				objDataWrapper.AddParameter("@TEXT",objEndorsmentDetailsInfo.TEXT);
				objDataWrapper.AddParameter("@ENDORSEMENT_CODE",objEndorsmentDetailsInfo.ENDORSEMENT_CODE);
				objDataWrapper.AddParameter("@ENDORS_ASSOC_COVERAGE",objEndorsmentDetailsInfo.ENDORS_ASSOC_COVERAGE);
				objDataWrapper.AddParameter("@SELECT_COVERAGE",objEndorsmentDetailsInfo.SELECT_COVERAGE);
				objDataWrapper.AddParameter("@ENDORS_PRINT",objEndorsmentDetailsInfo.ENDORS_PRINT);
				//objDataWrapper.AddParameter("@ENDORS_ATTACHMENT",objEndorsmentDetailsInfo.ENDORS_ATTACHMENT);
				objDataWrapper.AddParameter("@IS_ACTIVE",objEndorsmentDetailsInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objEndorsmentDetailsInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objEndorsmentDetailsInfo.CREATED_DATETIME);
				
				if(objEndorsmentDetailsInfo.EFFECTIVE_FROM_DATE.Ticks !=0)
					objDataWrapper.AddParameter("@EFFECTIVE_FROM_DATE",objEndorsmentDetailsInfo.EFFECTIVE_FROM_DATE );
				else
					objDataWrapper.AddParameter("@EFFECTIVE_FROM_DATE",DBNull.Value  );
				
				if(objEndorsmentDetailsInfo.EFFECTIVE_TO_DATE.Ticks!=0) 
					objDataWrapper.AddParameter("@EFFECTIVE_TO_DATE",objEndorsmentDetailsInfo.EFFECTIVE_TO_DATE);
				else
					objDataWrapper.AddParameter("@EFFECTIVE_TO_DATE",DBNull.Value );
				
				if(objEndorsmentDetailsInfo.DISABLED_DATE.Ticks!=0)
					objDataWrapper.AddParameter("@DISABLED_DATE",objEndorsmentDetailsInfo.DISABLED_DATE );
				else
					objDataWrapper.AddParameter("@DISABLED_DATE",DBNull.Value );

				if(objEndorsmentDetailsInfo.EDITION_DATE.Ticks!=0)
					objDataWrapper.AddParameter("@EDITION_DATE",objEndorsmentDetailsInfo.EDITION_DATE );
				else
					objDataWrapper.AddParameter("@EDITION_DATE",DBNull.Value );

				if(objEndorsmentDetailsInfo.INCREASED_LIMIT!=0)
					objDataWrapper.AddParameter("@INCREASED_LIMIT",objEndorsmentDetailsInfo.INCREASED_LIMIT );
				else
					objDataWrapper.AddParameter("@INCREASED_LIMIT",System.DBNull.Value );

				objDataWrapper.AddParameter("@FORM_NUMBER",objEndorsmentDetailsInfo.FORM_NUMBER );

				if(objEndorsmentDetailsInfo.PRINT_ORDER != 0)
					objDataWrapper.AddParameter("@PRINT_ORDER",objEndorsmentDetailsInfo.PRINT_ORDER );
				else
					objDataWrapper.AddParameter("@PRINT_ORDER",System.DBNull.Value);
			
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@ENDORSMENT_ID",objEndorsmentDetailsInfo.ENDORSMENT_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objEndorsmentDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/AddEndorsment.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objEndorsmentDetailsInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objEndorsmentDetailsInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1578", "");// "Record Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int ENDORSMENT_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (ENDORSMENT_ID == -1)
				{
					return -1;
				}
				else
				{
					objEndorsmentDetailsInfo.ENDORSMENT_ID = ENDORSMENT_ID;
					return returnResult;
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}
		#endregion

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldEndorsmentDetailsInfo">Model object having old information</param>
		/// <param name="objEndorsmentDetailsInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsEndorsmentDetailsInfo objOldEndorsmentDetailsInfo,ClsEndorsmentDetailsInfo objEndorsmentDetailsInfo)
		{
			string		strStoredProc	=	"Proc_UpdateEndorsmentDetails";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@ENDORSMENT_ID",objEndorsmentDetailsInfo.ENDORSMENT_ID);
				objDataWrapper.AddParameter("@STATE_ID",objEndorsmentDetailsInfo.STATE_ID);
				objDataWrapper.AddParameter("@LOB_ID",objEndorsmentDetailsInfo.LOB_ID);
				objDataWrapper.AddParameter("@PURPOSE",objEndorsmentDetailsInfo.PURPOSE);
				objDataWrapper.AddParameter("@TYPE",objEndorsmentDetailsInfo.TYPE);
				objDataWrapper.AddParameter("@DESCRIPTION",objEndorsmentDetailsInfo.DESCRIPTION);
				objDataWrapper.AddParameter("@TEXT",objEndorsmentDetailsInfo.TEXT);
				objDataWrapper.AddParameter("@ENDORSEMENT_CODE",objEndorsmentDetailsInfo.ENDORSEMENT_CODE);
				objDataWrapper.AddParameter("@ENDORS_ASSOC_COVERAGE",objEndorsmentDetailsInfo.ENDORS_ASSOC_COVERAGE);
				objDataWrapper.AddParameter("@SELECT_COVERAGE",objEndorsmentDetailsInfo.SELECT_COVERAGE);
				objDataWrapper.AddParameter("@ENDORS_PRINT",objEndorsmentDetailsInfo.ENDORS_PRINT);
				//objDataWrapper.AddParameter("@ENDORS_ATTACHMENT",objEndorsmentDetailsInfo.ENDORS_ATTACHMENT);

				if(objEndorsmentDetailsInfo.EFFECTIVE_FROM_DATE.Ticks !=0)
					objDataWrapper.AddParameter("@EFFECTIVE_FROM_DATE",objEndorsmentDetailsInfo.EFFECTIVE_FROM_DATE );
				else
					objDataWrapper.AddParameter("@EFFECTIVE_FROM_DATE",DBNull.Value  );
				
				if(objEndorsmentDetailsInfo.EFFECTIVE_TO_DATE.Ticks!=0) 
					objDataWrapper.AddParameter("@EFFECTIVE_TO_DATE",objEndorsmentDetailsInfo.EFFECTIVE_TO_DATE);
				else
					objDataWrapper.AddParameter("@EFFECTIVE_TO_DATE",DBNull.Value );
				
				if(objEndorsmentDetailsInfo.DISABLED_DATE.Ticks!=0)
					objDataWrapper.AddParameter("@DISABLED_DATE",objEndorsmentDetailsInfo.DISABLED_DATE );
				else
					objDataWrapper.AddParameter("@DISABLED_DATE",DBNull.Value );

				if(objEndorsmentDetailsInfo.EDITION_DATE.Ticks!=0)
					objDataWrapper.AddParameter("@EDITION_DATE",objEndorsmentDetailsInfo.EDITION_DATE );
				else
					objDataWrapper.AddParameter("@EDITION_DATE",DBNull.Value );

				objDataWrapper.AddParameter("@FORM_NUMBER",objEndorsmentDetailsInfo.FORM_NUMBER );

				//Added by Mohit Agarwal 27-Jun-2007
				if(objEndorsmentDetailsInfo.INCREASED_LIMIT!=0)
					objDataWrapper.AddParameter("@INCREASED_LIMIT",objEndorsmentDetailsInfo.INCREASED_LIMIT );
				else
					objDataWrapper.AddParameter("@INCREASED_LIMIT",System.DBNull.Value );
				
				if(objEndorsmentDetailsInfo.PRINT_ORDER != 0)
					objDataWrapper.AddParameter("@PRINT_ORDER",objEndorsmentDetailsInfo.PRINT_ORDER );
				else
					objDataWrapper.AddParameter("@PRINT_ORDER",System.DBNull.Value);

				objDataWrapper.AddParameter("@IS_ACTIVE",objEndorsmentDetailsInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@MODIFIED_BY",objEndorsmentDetailsInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objEndorsmentDetailsInfo.LAST_UPDATED_DATETIME);
				if(base.TransactionLogRequired) 
				{
					objEndorsmentDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/AddEndorsment.aspx.resx");
					objBuilder.GetUpdateSQL(objOldEndorsmentDetailsInfo,objEndorsmentDetailsInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objEndorsmentDetailsInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1579", "");// "Information Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) 
				{
					objDataWrapper.Dispose();
				}
				if(objBuilder != null) 
				{
					objBuilder = null;
				}
			}
		}
		#endregion

		#region "GetxmlMethods"
		public static DataSet GetEndorsement(string ENDORSMENT_ID)
		{
			string strSql = "Proc_GetXMLEndorsmentDetails";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@ENDORSMENT_ID",ENDORSMENT_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}
		#endregion

		public static void GetCoverage(DropDownList objDropDownList,int stateId, int lobId, int covid)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@StateId",stateId);
			objDataWrapper.AddParameter("@LobId",lobId);
			objDataWrapper.AddParameter("@CovId",covid);

			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_FillCoverage").Tables[0];
			objDropDownList.DataSource = objDataTable;
			objDropDownList.DataTextField = "COVERAGE";
			objDropDownList.DataValueField = "COV_ID";
			objDropDownList.DataBind();
		}

           
  
        /// <summary>
        /// Get the Clauses Data using CLAUSE_ID
        /// </summary>
        /// <param name="CLAUSE_ID"></param>
        /// <returns></returns>
        public ClsClausesInfo FetchData(Int32 CLAUSE_ID)
        {

            DataSet dsCount = null;
            ClsClausesInfo ObjClsClausesInfo = new ClsClausesInfo();
            try
            {
                dsCount = ObjClsClausesInfo.FetchData(CLAUSE_ID);

                if (dsCount.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(dsCount, ObjClsClausesInfo);
                }

            }
            catch (Exception ex)
            { throw (ex); }
            finally { }
            return ObjClsClausesInfo;
        }

        public int AddClauses(ClsClausesInfo ObjClsClausesInfo)
        {
            int returnValue = 0;

            if (ObjClsClausesInfo.RequiredTransactionLog)
            {
                ObjClsClausesInfo.TransactLabel = ClsCommon.MapTransactionLabel("CmsWeb/Maintenance/AddClausesDetails.aspx.resx");
                returnValue = ObjClsClausesInfo.ADDClausesData();

            }
            return returnValue;
        }

        /// <summary>
        /// Delete the Clauses Data Based on CLAUSE_ID 
        /// </summary>
        /// <param name="ObjMaritimeInfo"></param>
        /// <returns></returns>
        public int DeleteClauses(ClsClausesInfo ObjClsClausesInfo)
        {
            int returnValue = 0;
            if (ObjClsClausesInfo.RequiredTransactionLog)
            {
                ObjClsClausesInfo.TransactLabel = ClsCommon.MapTransactionLabel("CmsWeb/Maintenance/AddClausesDetails.aspx.resx");
                returnValue = ObjClsClausesInfo.DeleteClauses();
            }
            return returnValue;
        }

        /// <summary>
        /// Activate and Deactivate the Clauses base on the CLAUSE_ID and is_Activae 
        /// </summary>
        /// <param name="ObjMaritimeInfo"></param>
        /// <returns></returns>
        public int ActivateDeactivateClauses(ClsClausesInfo ObjClsClausesInfo)
        {
            int returnValue = 0;
            if (ObjClsClausesInfo.RequiredTransactionLog)
            {
                ObjClsClausesInfo.TransactLabel = ClsCommon.MapTransactionLabel("CmsWeb/Maintenance/AddClausesDetails.aspx.resx");

                returnValue = ObjClsClausesInfo.ActivateDeactivateClausesDetails();
            }
            return returnValue;
        }

        /// <summary>
        /// Update Clauses Date Based On the CLAUSE_ID 
        /// </summary>
        /// <param name="ObjMaritimeInfo"></param>
        /// <returns></returns>
        public int UpdateClauses(ClsClausesInfo ObjClsClausesInfo)
        {
            int returnValue = 0;

            if (ObjClsClausesInfo.RequiredTransactionLog)
            {
                ObjClsClausesInfo.TransactLabel = ClsCommon.MapTransactionLabel("CmsWeb/Maintenance/AddClausesDetails.aspx.resx");


                returnValue = ObjClsClausesInfo.UpdateClausesDetails();

            }//if (ObjMaritimeInfo.RequiredTransactionLog)

            return returnValue;
        }//

        public static DataSet GetSUBLOBs(string LOBCODE)
        {
           
            string LANG_ID="";
          

            DataSet objDataSet = GetSUBLOBs(LOBCODE, LANG_ID);
            return objDataSet;
        }

        public static DataSet GetSUBLOBs(String LOBCODE, string LANG_ID)
        {
            string strSql = "proc_getSublobs";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@LOBID", LOBCODE);
            objDataWrapper.AddParameter("@LANG_ID", LANG_ID);
            DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
            return objDataSet;
        }

        public static DataSet GetSUSEPLOBs(string LANG_ID)
        {
            string strSql = "proc_getsuseplobs";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);          
            objDataWrapper.AddParameter("@LANG_ID", LANG_ID);
            DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
            return objDataSet;
        }
	}


}
