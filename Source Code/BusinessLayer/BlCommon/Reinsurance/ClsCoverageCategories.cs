/* ***************************************************************************************
   Author		: Swarup Kumar Pal 
   Creation Date: August 09, 2007
   Last Updated : 
   Reviewed By	: 
   Purpose		: Class file for Coverage Catagory 
   Comments		: 
   ------------------------------------------------------------------------------------- 
   History	Date	     Modified By		Description
   
   ------------------------------------------------------------------------------------- 
   *****************************************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.Configuration;
using Cms.DataLayer;
using System.Web.UI.WebControls;
using Cms.Model.Maintenance.Reinsurance;

namespace Cms.BusinessLayer.BlCommon.Reinsurance
{
	/// <summary>
	/// Summary description for ClsCoverageCategories.
	/// </summary>
	public class ClsCoverageCategories:Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		# region D E C L A R A T I O N 

		//private const string MNT_REIN_SPLIT="MNT_REIN_SPLIT";
		private	bool boolTransactionLog;
		public ClsCoverageCategories()
		{
			boolTransactionLog	= base.TransactionLogRequired;
		
		}
		
		# endregion 

		#region A D D  (I N S E R T)
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objReinsuranceInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		
		public int Add(ClsCoverageCategoriesInfo objCoverageCategoriesInfo)
		{
			string		strStoredProc	=	"Proc_InsertREINSURANCE_COVERAGE_CATEGORY";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@EFFECTIVE_DATE",objCoverageCategoriesInfo.EFFECTIVE_DATE);
				objDataWrapper.AddParameter("@LOB_ID",objCoverageCategoriesInfo.LOB_ID);
				objDataWrapper.AddParameter("@CATEGORY",objCoverageCategoriesInfo.CATEGORY);
				objDataWrapper.AddParameter("@CREATED_BY",objCoverageCategoriesInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objCoverageCategoriesInfo.CREATED_DATETIME);				
				
				string strCategory = objCoverageCategoriesInfo.CATEGORY;
				string strCategoryDesc = "";
				string strLob = "";
				string strDate = "";

//				objDataWrapper.AddParameter("@COVERAGE_CATEGORY_ID",objCoverageCategoriesInfo.COVERAGE_CATEGORY_ID);
//				objDataWrapper.AddParameter("@IS_ACTIVE",objCoverageCategoriesInfo.IS_ACTIVE);

				string strGetCoverageCategoryProcedure="Proc_GetCoverageCatagory";
				DataWrapper objWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
				strCategory = strCategory.Substring(0,(strCategory.Length-1));
				objWrapper.AddParameter("@CATEGORY",strCategory);
				DataSet dsCategory = new DataSet();
				dsCategory = objWrapper.ExecuteDataSet(strGetCoverageCategoryProcedure);
				DataSet dsTemp = new DataSet();
				dsTemp = GetLOBString(Convert.ToInt32(objCoverageCategoriesInfo.LOB_ID));
				strLob = dsTemp.Tables[0].Rows[0][0].ToString();
				foreach (DataRow dr in dsCategory.Tables[0].Rows)
				{
					strCategoryDesc = strCategoryDesc + dsCategory.Tables[0].Rows[0].ItemArray[0].ToString() + ",";
				}
				strDate = Convert.ToDateTime(objCoverageCategoriesInfo.EFFECTIVE_DATE).ToShortDateString();

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@COVERAGE_CATEGORY_ID",objCoverageCategoriesInfo.COVERAGE_CATEGORY_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objCoverageCategoriesInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/Maintenance/Reinsurance/MasterSetup/CoverageCategories.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objCoverageCategoriesInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objCoverageCategoriesInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New Coverage Category Has Been Added";
					//objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	";<B> Effective Date = </B>" + strDate + ";<B> Lob = </B>" + strLob + ";<B> Coverage Category = </B>" + strCategoryDesc;

					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int  intCOVERAGE_CATEGORY_ID= int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (intCOVERAGE_CATEGORY_ID == -1)
				{
					return -1;
				}
				else
				{
					objCoverageCategoriesInfo.COVERAGE_CATEGORY_ID= intCOVERAGE_CATEGORY_ID;
					return intCOVERAGE_CATEGORY_ID;
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
			
		# endregion 

		#region U P D A T E   
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldReinsuranceInfo">Model object having old information</param>
		/// <param name="objReinsuranceInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsCoverageCategoriesInfo objOldCoverageCategoriesInfo,ClsCoverageCategoriesInfo objCoverageCategoriesInfo)
		{
			string	strStoredProc	=	"Proc_UpdateREINSURANCE_COVERAGE_CATEGORY";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@COVERAGE_CATEGORY_ID",objCoverageCategoriesInfo.COVERAGE_CATEGORY_ID);
				objDataWrapper.AddParameter("@EFFECTIVE_DATE",objCoverageCategoriesInfo.EFFECTIVE_DATE);
				objDataWrapper.AddParameter("@LOB_ID",objCoverageCategoriesInfo.LOB_ID);
				objDataWrapper.AddParameter("@CATEGORY",objCoverageCategoriesInfo.CATEGORY);
				objDataWrapper.AddParameter("@MODIFIED_BY",objCoverageCategoriesInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objCoverageCategoriesInfo.LAST_UPDATED_DATETIME);				
				
				
				
				
				string strGetCoverageCategoryProcedure="Proc_GetCoverageCatagory";

				//For Capture Old Data :Start
				string strOldCategoryDesc = "";
				string strOldLob = "";
				string strOldDate = "";
				string strOldCategory = objOldCoverageCategoriesInfo.CATEGORY;
				DataWrapper objOldWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
				strOldCategory = strOldCategory.Substring(0,(strOldCategory.Length-1));
				objOldWrapper.AddParameter("@CATEGORY",strOldCategory);
				DataSet dsOldCategory = new DataSet();
				dsOldCategory = objOldWrapper.ExecuteDataSet(strGetCoverageCategoryProcedure);
				DataSet dsOldTemp = new DataSet();
				dsOldTemp = GetLOBString(Convert.ToInt32(objOldCoverageCategoriesInfo.LOB_ID));
				strOldLob = dsOldTemp.Tables[0].Rows[0][0].ToString();
				foreach (DataRow dr in dsOldCategory.Tables[0].Rows)
				{
					strOldCategoryDesc = strOldCategoryDesc + dsOldCategory.Tables[0].Rows[0].ItemArray[0].ToString() + ",";
				}
				strOldCategoryDesc = strOldCategoryDesc.Substring(0,(strOldCategoryDesc.Length-1));
				strOldDate = Convert.ToDateTime(objOldCoverageCategoriesInfo.EFFECTIVE_DATE).ToShortDateString();
				//For Capture Old Data :End

				//For Capture new Data :Start
				string strCategoryDesc = "";
				string strLob = "";
				string strDate = "";
				string strCategory = objCoverageCategoriesInfo.CATEGORY;
				DataWrapper objWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
				strCategory = strCategory.Substring(0,(strCategory.Length-1));
				objWrapper.AddParameter("@CATEGORY",strCategory);
				DataSet dsCategory = new DataSet();
				dsCategory = objWrapper.ExecuteDataSet(strGetCoverageCategoryProcedure);
				DataSet dsTemp = new DataSet();
				dsTemp = GetLOBString(Convert.ToInt32(objCoverageCategoriesInfo.LOB_ID));
				strLob = dsTemp.Tables[0].Rows[0][0].ToString();
				foreach (DataRow dr in dsCategory.Tables[0].Rows)
				{
					strCategoryDesc = strCategoryDesc + dsCategory.Tables[0].Rows[0].ItemArray[0].ToString() + ",";
				}
				strCategoryDesc = strCategoryDesc.Substring(0,(strCategoryDesc.Length-1));
				strDate = Convert.ToDateTime(objCoverageCategoriesInfo.EFFECTIVE_DATE).ToShortDateString();
				//For Capture new Data :End
				
				//For Custom Info:Start
				string strDateChange = "";
				string strLobChange = "";
				string strCategoryChange = "";
				if(objOldCoverageCategoriesInfo.EFFECTIVE_DATE != objCoverageCategoriesInfo.EFFECTIVE_DATE)
				{
					strDateChange = ";<B>Effective Date From = </B>" + strOldDate + " <B>To =</B>" + strDate;
				}

				if(objOldCoverageCategoriesInfo.LOB_ID != objCoverageCategoriesInfo.LOB_ID)
				{
					strLobChange = ";<B> LOB From = </B>" + strOldLob + " <B>To = </B>" + strLob;
				}

				if(objOldCoverageCategoriesInfo.CATEGORY != objCoverageCategoriesInfo.CATEGORY)
				{
					strCategoryChange = ";<B>Coverage Category From = </B>" + strOldCategoryDesc + "<B> To = </B>" + strCategoryDesc;
				}
				string strCustominfo = strDateChange + strLobChange + strCategoryChange;

				//For Custom Info:End
				if(base.TransactionLogRequired) 
				{
					objCoverageCategoriesInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/Maintenance/Reinsurance/MasterSetup/CoverageCategories.aspx.resx");
					objBuilder.GetUpdateSQL(objOldCoverageCategoriesInfo, objCoverageCategoriesInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objCoverageCategoriesInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Coverage Category Has Been Updated";
					//objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	strCustominfo;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
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

		# endregion 

		#region		ACTIVATE/DEACTIVATE
		public void ActivateDeactivateCoverageCatagory(ClsCoverageCategoriesInfo objCoverageCategoriesInfo,string isActive)
		{
			DataWrapper objDataWrapper	=	new DataWrapper( ConnStr, CommandType.StoredProcedure);
			string strActivateDeactivateProcedure="Proc_ActivateDeactivateCoverageCatagory";
			try
			{
				string strCategory = objCoverageCategoriesInfo.CATEGORY;
				string strCategoryDesc = "";
				string strLob = "";
				string strDate = "";

				objDataWrapper.AddParameter("@COVERAGE_CATEGORY_ID",objCoverageCategoriesInfo.COVERAGE_CATEGORY_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",isActive);

					string strGetCoverageCategoryProcedure="Proc_GetCoverageCatagory";
					DataWrapper objWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
					strCategory = strCategory.Substring(0,(strCategory.Length-1));
					objWrapper.AddParameter("@CATEGORY",strCategory);
					DataSet dsCategory = new DataSet();
					dsCategory = objWrapper.ExecuteDataSet(strGetCoverageCategoryProcedure);
					DataSet dsTemp = new DataSet();
					dsTemp = GetLOBString(Convert.ToInt32(objCoverageCategoriesInfo.LOB_ID));
					strLob = dsTemp.Tables[0].Rows[0][0].ToString();
					foreach (DataRow dr in dsCategory.Tables[0].Rows)
					{
						strCategoryDesc = strCategoryDesc + dsCategory.Tables[0].Rows[0].ItemArray[0].ToString() + ",";
					}
				strDate = Convert.ToDateTime(objCoverageCategoriesInfo.EFFECTIVE_DATE).ToShortDateString();


				if(TransactionLogRequired) 
				{
                    /*==========================================================
                     * SANTOSH GAUTAM: BELOW LINE COMMENTED ON 28 OCT 2010
                     *==========================================================*/
                    //objCoverageCategoriesInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddWatercraftEngineDetails.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY      =   objCoverageCategoriesInfo.MODIFIED_BY;
					if(isActive.Equals("Y"))
						objTransactionInfo.TRANS_DESC		=	"Coverage Catagory has been activated.";
					else
						objTransactionInfo.TRANS_DESC		=	"Coverage Catagory has been deactivated.";
//					objTransactionInfo.CUSTOM_INFO			=	strCode;
					objTransactionInfo.CUSTOM_INFO		=	";<B> Effective Date = </B>" + strDate + ";<B> Lob = </B>" + strLob + ";<B> Coverage Category = </B>" + strCategoryDesc;
					objDataWrapper.ExecuteNonQuery(strActivateDeactivateProcedure, objTransactionInfo);
				}
				else
					objDataWrapper.ExecuteNonQuery(strActivateDeactivateProcedure);

				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}	
		}

		#endregion

		# region DELETE

		/// <summary>
		/// Delete the whole record of spefied id 
		/// </summary>
		/// <returns>Nos of rows deleted</returns>
		public int Delete(ClsCoverageCategoriesInfo objCoverageCategoriesInfo,int intCatagoryId, int UserId)
		{
			try
			{
				/*Calling the stored procedure to get the maximum Journal entry no*/
				String strStoredProc = "Proc_DeleteREINSURANCE_COVERAGE_CATEGORY";
				int Value;

				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
				objDataWrapper.AddParameter("@COVERAGE_CATEGORY_ID", intCatagoryId);
				SqlParameter objRetVal = (SqlParameter) objDataWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);


				string strCategory = objCoverageCategoriesInfo.CATEGORY;
				string strCategoryDesc = "";
				string strLob = "";
				string strDate = "";
				string strGetCoverageCategoryProcedure="Proc_GetCoverageCatagory";
				DataWrapper objWrapper = new DataWrapper( ConnStr, CommandType.StoredProcedure);
				strCategory = strCategory.Substring(0,(strCategory.Length-1));
				objWrapper.AddParameter("@CATEGORY",strCategory);
				DataSet dsCategory = new DataSet();
				dsCategory = objWrapper.ExecuteDataSet(strGetCoverageCategoryProcedure);
				DataSet dsTemp = new DataSet();
				dsTemp = GetLOBString(Convert.ToInt32(objCoverageCategoriesInfo.LOB_ID));
				strLob = dsTemp.Tables[0].Rows[0][0].ToString();
				foreach (DataRow dr in dsCategory.Tables[0].Rows)
				{
					strCategoryDesc = strCategoryDesc + dsCategory.Tables[0].Rows[0].ItemArray[0].ToString() + ",";
				}
				strDate = Convert.ToDateTime(objCoverageCategoriesInfo.EFFECTIVE_DATE).ToShortDateString();


				if(TransactionLogRequired) 
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY = UserId;
					objTransactionInfo.TRANS_DESC		=	"Coverage Catagory has been deleted.";					
					objTransactionInfo.CUSTOM_INFO		=	";<B> Effective Date = </B>" + strDate + ";<B> Lob = </B>" + strLob + ";<B> Coverage Category = </B>" + strCategoryDesc;
					Value = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
				}
				else
					Value = objDataWrapper.ExecuteNonQuery(strStoredProc);

				Value = int.Parse(objRetVal.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();

				return Value ;
			}
			catch (Exception objEx)
			{
				throw(objEx);
			}
		}
		
		#endregion

		#region GetCoverageCatagoryInfo
		/// <summary>
		/// Returns the data in the form of XML of specified intCoverageId
		/// </summary>
		/// <param name="CoverageId">Coverage Id whose data will be returned</param>
		/// <returns>XML of data</returns>
		public static string GetCoverageCatagoryInfo(int intCoverageId )
		{
			String strStoredProc = "Proc_GetREINSURANCE_COVERAGE_CATEGORY";
			DataSet dsCoverageInfo = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@COVERAGE_CATEGORY_ID",intCoverageId);
				
				dsCoverageInfo = objDataWrapper.ExecuteDataSet(strStoredProc);
				
				if (dsCoverageInfo.Tables[0].Rows.Count != 0)
				{
					return dsCoverageInfo.GetXml();
				}
				else
				{
					return "";
				}
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				objDataWrapper.Dispose();
			}
		}
		#endregion


		public DataSet GetLOBString(int LOBID)
		{
			string		strStoredProc	=	"Proc_GetLOBString";
			DataSet dsTemp;
			 
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{		
				
				objDataWrapper.AddParameter("@LOB_ID",LOBID);
				

				dsTemp = new DataSet();
				dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);
			
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return dsTemp;
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
			}		
		}
		
	}
}
