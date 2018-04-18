using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Application;
using Cms.BusinessLayer.BlCommon;
using System.Collections;


namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ClsApplicantInsured.
	/// </summary>
	public class ClsApplicantInsured : Cms.BusinessLayer.BlApplication.clsapplication
	{
		public ClsApplicantInsured()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		//CheckApplicantForApplication
		public static DataTable CheckApplicantForApplication(int CustomerID,int AppID,int AppVersionID)
		{
			try
			{
				DataSet dsTemp = new DataSet();			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPVERSIONID",AppVersionID,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_CheckCoApplicantsForApplication");
				return dsTemp.Tables[0];
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{}
		}
		public static DataTable FetchCustApplicantInsured(int CustomerID)
		{
			try
			{
				DataSet dsTemp = new DataSet();			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID,SqlDbType.Int);
				//objDataWrapper.AddParameter("@APPID",AppID,SqlDbType.Int);
				//objDataWrapper.AddParameter("@APPVERSIONID",AppVersionID,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetCustApplicantInsured");
				return dsTemp.Tables[0];
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{}
		}		
		public void SavePrimaryApplicantInsured(ArrayList arr)	
		{
			string	strStoredProc = "Proc_InsertPrimaryApplicantInsured";
			try
			{				
				for (int i=0 ; i<= arr.Count-1 ; i++)
				{
					ClsApplicantInsuredInfo objApp=(ClsApplicantInsuredInfo)arr[i];
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
					objDataWrapper.AddParameter("@APPLICANT_ID",objApp.APPLICANT_ID);
					objDataWrapper.AddParameter("@CUSTOMER_ID",objApp.CUSTOMER_ID);
					objDataWrapper.AddParameter("@APP_ID",objApp.APP_ID);
					objDataWrapper.AddParameter("@APP_VERSION_ID",objApp.APP_VERSION_ID);
					objDataWrapper.AddParameter("@CREATED_BY",objApp.CREATED_BY);
					objDataWrapper.AddParameter("@IS_PRIMARY_APPLICANT",objApp.IS_PRIMARY_APPLICANT);								
					if(TransactionLogRequired && i==arr.Count-1)
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;						
						objTransactionInfo.RECORDED_BY		= objApp.CREATED_BY;
						objTransactionInfo.APP_ID			=	objApp.APP_ID;
						objTransactionInfo.APP_VERSION_ID	=	objApp.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objApp.CUSTOMER_ID;
						objTransactionInfo.TRANS_DESC		=	"Primary Applicant at application is added";												
						objDataWrapper.ExecuteNonQuery(strStoredProc ,objTransactionInfo);
						objDataWrapper.ClearParameteres();
						objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);			
					}
					else
					{
						objDataWrapper.ExecuteNonQuery(strStoredProc);		
					}
				}
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{}
		}

		/// <summary>
		/// Updating applicants for customer ,application & applicantion version.
		/// Delete is performed for customer ,application & applicantion version befor updating the
		/// applicants. 
		/// </summary>
		/// <param name="dtTemp"></param>
		/// <param name="modified_By"></param>
		public int  UpdatePrimaryApplicantInsured(ArrayList arr,int OldApplicantID, string strCustomInfo)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			int NewApplicantID=OldApplicantID;
			try
			{		
				if(arr.Count>0)
					ClsApplicantInsured.DeleteSelectedCustApplicant((ClsApplicantInsuredInfo)arr[0],objDataWrapper);
				
				string	strStoredProc = "Proc_InsertPrimaryApplicantInsured";//COMMENTED BY PAWAN "Proc_UpdatePrimaryApplicantInsured";
				objDataWrapper.ClearParameteres();
				for (int i=0 ; i<= arr.Count-1 ; i++)
				{
					ClsApplicantInsuredInfo objApp=(ClsApplicantInsuredInfo)arr[i];
					objDataWrapper.AddParameter("@APPLICANT_ID",objApp.APPLICANT_ID);
					objDataWrapper.AddParameter("@CUSTOMER_ID",objApp.CUSTOMER_ID);
					objDataWrapper.AddParameter("@APP_ID",objApp.APP_ID);
					objDataWrapper.AddParameter("@APP_VERSION_ID",objApp.APP_VERSION_ID);
					objDataWrapper.AddParameter("@CREATED_BY",objApp.CREATED_BY);
					objDataWrapper.AddParameter("@IS_PRIMARY_APPLICANT",objApp.IS_PRIMARY_APPLICANT);
					NewApplicantID=objApp.APPLICANT_ID;
					if(objApp.IS_PRIMARY_APPLICANT==1 && NewApplicantID!=OldApplicantID)
					{				
						
						
							if(TransactionLogRequired)
							{
								//objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/AddDriverDetails.aspx.resx");
								//SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
								//string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
								Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
								objTransactionInfo.TRANS_TYPE_ID	=	1;
								//objTransactionInfo.RECORDED_BY		=	objApp.CREATED_BY;
								objTransactionInfo.RECORDED_BY		= objApp.MODIFIED_BY;
								objTransactionInfo.APP_ID			=	objApp.APP_ID;
								objTransactionInfo.APP_VERSION_ID	=	objApp.APP_VERSION_ID;
								objTransactionInfo.CLIENT_ID		=	objApp.CUSTOMER_ID;
								objTransactionInfo.TRANS_DESC		=	"Primary Applicant is modified";
								objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
								//Executing the query
								objDataWrapper.ExecuteNonQuery(strStoredProc ,objTransactionInfo);
								objDataWrapper.ClearParameteres();
								objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);			
							}
					}
					else
					{
						
						//objDataWrapper.AddParameter("@IS_SECONDARY_APPLICANT",int.Parse(dr["IsSecondaryApplicant"].ToString()));*/
						objDataWrapper.ExecuteNonQuery(strStoredProc);
						objDataWrapper.ClearParameteres();
						objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);			
					}
				}
				return NewApplicantID;
			}
			catch(Exception exc)
			{
				objDataWrapper.RollbackTransaction(Cms.DataLayer.DataWrapper.CloseConnection.YES);
				throw (exc);
			}
			finally
			{	
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}
		/// <summary>
		/// Deleting all the apllicants for particular customer,applicant & application version.
		/// </summary>
		/// <param name="dtTemp"></param>
		/// <param name="objDataWrapper"></param>
		private static void DeleteSelectedCustApplicant(ClsApplicantInsuredInfo objApp,DataWrapper objDataWrapper)
		{		
				string strStoredProc="Proc_DeletePrimaryApplicantInsured";
				objDataWrapper.AddParameter("@CUSTOMER_ID",objApp.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objApp.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objApp.APP_VERSION_ID);
				objDataWrapper.ExecuteNonQuery(strStoredProc);
				objDataWrapper.ClearParameteres();
				
		}

	

	}
}
