/******************************************************************************************
<Author				: -   Gaurav Tyagi
<Start Date				: -	5/9/2005 2:26:05 PM
<End Date				: -	
<Description				: - 	This file is used to
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 5/24/2005
<Modified By			: - Mohit Gupta
<Purpose				: - Adding method "UpdateUserPreferences" & "GetXmlForUserPreferences" for updating UserPreferences.

<Modified Date			: - Anshuman
<Modified By			: - June 08, 2005
<Purpose				: - transaction description modified

<Modified Date			: - Gaurav
<Modified By			: - June 24, 2005
<Purpose				: - FillUser(string UserTypeCode,string AgencyCode)

*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using System.Web.UI.WebControls;
using Cms.Model.Maintenance;
using System.Collections;

namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// This file is used 
	/// </summary>
	public class ClsUser : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		private const	string		MNT_USER_LIST			=	"MNT_USER_LIST";
		private const string	AGENCY_ID = "27";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		//private int _USER_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateUser";
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

		
		#region Public Utility Functions

	
		public static DataSet FetchUserforDiary(string sql, DataWrapper objWrapper)
		{
			objWrapper.ClearParameteres();
			objWrapper.AddParameter("@sqlquery",sql);
			DataSet ds = objWrapper.ExecuteDataSet("Proc_FetchUserforDiary");
			objWrapper.ClearParameteres();
				
			return ds;
		}

		public static DataSet FetchUserforDiary(string sql)
		{
			SqlParameter [] sparam=new SqlParameter[1];
			try
			{
				sparam[0]=new SqlParameter("@sqlquery",SqlDbType.NVarChar,2000);
				sparam[0].Value=sql;
				return DataWrapper.ExecuteDataset(ConnStr,"Proc_FetchUserforDiary",sparam); 
				
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				
			}

		}



		/// <summary>
		/// Gets the first Underwriter from the underwriters list 
		/// </summary>
		/// <returns>userid of the underwriter</returns>
		public static string GetUnderwriterForApplication()
		{
			try
			{
				 
				DataSet dsUnderwriters = new DataSet();
				string underwriterID="0";

				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				dsUnderwriters = objDataWrapper.ExecuteDataSet("Proc_GetUnderwriterForApplication");
				if (dsUnderwriters!= null && dsUnderwriters.Tables[0]!=null)
				{
					if(dsUnderwriters.Tables[0].Rows.Count>0)
					{
						underwriterID = dsUnderwriters.Tables[0].Rows[0][0].ToString();
					}
				}

				return underwriterID;
				
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsUser()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Get User Name

		/// <summary>
		/// This is used to fetch all producers
		///  
		/// </summary>
		/// <returns>Datatable</returns>
		public static DataTable GetAllProducers()
		{
			try
			{
				DataSet dsCSR = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				dsCSR = objDataWrapper.ExecuteDataSet("Proc_GetAllProducers");
			
				return dsCSR.Tables[0];
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}



		/// <summary>
		/// This is used to fetch the users of an agency
		///  
		/// </summary>
		/// <returns>Datatable</returns>
		public static DataTable GetAgencyUsers(int AgencyID)
		{
			try
			{
				DataSet dsCSR = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@AGENCYID",AgencyID,SqlDbType.Int);

				dsCSR = objDataWrapper.ExecuteDataSet("Proc_GetAgencyUsers");
			
				return dsCSR.Tables[0];
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}


		/// <summary>
		/// This is used to fetch the users of an agency
		///  based on app_id, customer_id, app_version_id
		/// </summary>
		/// <returns>Datatable</returns>
		public static DataTable GetAgencyUsers(int custID,string from)
		{
			try
			{
				DataSet dsCSR = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@AGENCYID",0,SqlDbType.Int); // passing zero for default parameter agency id
				objDataWrapper.AddParameter("@FROM",from,SqlDbType.VarChar,ParameterDirection.Input,1); //passing Y to be used in the condition
				objDataWrapper.AddParameter("@CUSTOMER_ID",custID,SqlDbType.Int);
                

				dsCSR = objDataWrapper.ExecuteDataSet("Proc_GetAgencyUsers");
			
				return dsCSR.Tables[0];
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}
		#endregion

		#region List of CSRs for use at General Information
        public DataTable GetCSRProducers(int AgencyID, int LobID,string sSystemId) //LobID added by Charles on 28-May-2010   
		{
			DataSet dsCSR = null;
			DataWrapper objDataWrapper = null;
			try
			{
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@AGENCYID",AgencyID,SqlDbType.Int);
                objDataWrapper.AddParameter("@LOB_ID", LobID, SqlDbType.SmallInt);//Added by Charles on 28-May-2010 
                objDataWrapper.AddParameter("@USER_SYSTEM_ID", sSystemId, SqlDbType.NVarChar); //Added by Charles on 31-May-2010
				dsCSR = objDataWrapper.ExecuteDataSet("Proc_GetCSRProducers");
				if(dsCSR!=null && dsCSR.Tables.Count>0)
					return dsCSR.Tables[0];
				else
					return null;
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{
				if(dsCSR!=null)
					dsCSR = null;
				if(objDataWrapper!=null)
					objDataWrapper = null;
			}
		}

		#endregion

		#region List of producers for use at General Information
		
		public DataTable GetProducers(int AgencyID)
		{
			DataSet dsCSR = null;
			DataWrapper objDataWrapper = null;
			try
			{
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@AGENCYID",AgencyID,SqlDbType.Int); 
				dsCSR = objDataWrapper.ExecuteDataSet("Proc_GetProducers");
				if(dsCSR!=null && dsCSR.Tables.Count>0)
					return dsCSR.Tables[0];
				else
					return null;
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{
				if(dsCSR!=null)
					dsCSR = null;
				if(objDataWrapper!=null)
					objDataWrapper = null;
			}
		}	


		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objUserInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsUserInfo objUserInfo)
		{
			string		strStoredProc	=	"Proc_InsertUser";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@USER_LOGIN_ID",objUserInfo.USER_LOGIN_ID);

				//Done by Sibin for Itrack Issue 5139 on 3 Dec 08
				//objDataWrapper.AddParameter("@USER_TYPE_ID",objUserInfo.USER_TYPE_ID);
				if(objUserInfo.USER_TYPE_ID!=0)
					objDataWrapper.AddParameter("@USER_TYPE_ID",objUserInfo.USER_TYPE_ID);
				else
					objDataWrapper.AddParameter("@USER_TYPE_ID",System.DBNull.Value);

				objDataWrapper.AddParameter("@USER_PWD",objUserInfo.USER_PWD);
				objDataWrapper.AddParameter("@USER_TITLE",objUserInfo.USER_TITLE);
				if(objUserInfo.SUB_CODE!="")
					objDataWrapper.AddParameter("@SUB_CODE",objUserInfo.SUB_CODE);
				else
					objDataWrapper.AddParameter("@SUB_CODE",System.DBNull.Value);

				objDataWrapper.AddParameter("@USER_FNAME",objUserInfo.USER_FNAME);
				objDataWrapper.AddParameter("@USER_LNAME",objUserInfo.USER_LNAME);
				objDataWrapper.AddParameter("@USER_INITIALS",objUserInfo.USER_INITIALS);
				objDataWrapper.AddParameter("@USER_ADD1",objUserInfo.USER_ADD1);
				objDataWrapper.AddParameter("@USER_ADD2",objUserInfo.USER_ADD2);
				objDataWrapper.AddParameter("@USER_CITY",objUserInfo.USER_CITY);
				objDataWrapper.AddParameter("@USER_STATE",objUserInfo.USER_STATE);
				objDataWrapper.AddParameter("@USER_ZIP",objUserInfo.USER_ZIP);
				objDataWrapper.AddParameter("@USER_PHONE",objUserInfo.USER_PHONE);
				objDataWrapper.AddParameter("@USER_EXT",objUserInfo.USER_EXT);
				objDataWrapper.AddParameter("@USER_FAX",objUserInfo.USER_FAX);
				objDataWrapper.AddParameter("@USER_MOBILE",objUserInfo.USER_MOBILE);
				objDataWrapper.AddParameter("@USER_EMAIL",objUserInfo.USER_EMAIL);
				objDataWrapper.AddParameter("@User_Supervisor",objUserInfo.USER_SPR);
				objDataWrapper.AddParameter("@PINK_SLIP_NOTIFY",objUserInfo.PINK_SLIP_NOTIFY);
				objDataWrapper.AddParameter("@USER_MGR_ID",objUserInfo.USER_MGR_ID);
				objDataWrapper.AddParameter("@USER_DEF_DIV_ID",objUserInfo.USER_DEF_DIV_ID);
				objDataWrapper.AddParameter("@USER_DEF_DEPT_ID",objUserInfo.USER_DEF_DEPT_ID);
				objDataWrapper.AddParameter("@USER_DEF_PC_ID",objUserInfo.USER_DEF_PC_ID);
				//objDataWrapper.AddParameter("@USER_CHANGE_COMM",objUserInfo.USER_CHANGE_COMM);
				//objDataWrapper.AddParameter("@USER_VIEW_COMM",objUserInfo.USER_VIEW_COMM);
				//objDataWrapper.AddParameter("@USER_LOCKED_DATETIME",objUserInfo.USER_LOCKED_DATETIME);
				objDataWrapper.AddParameter("@USER_TIME_ZONE",objUserInfo.USER_TIME_ZONE);
				objDataWrapper.AddParameter("@USER_NOTES",objUserInfo.USER_NOTES);//***************//
				//objDataWrapper.AddParameter("@USER_SHOW_COMPLETE_TODOLIST",objUserInfo.USER_SHOW_COMPLETE_TODOLIST);
				objDataWrapper.AddParameter("@IS_ACTIVE",objUserInfo.IS_ACTIVE);
				//objDataWrapper.AddParameter("@USER_INACTIVE_DATETIME",objUserInfo.USER_INACTIVE_DATETIME);
				objDataWrapper.AddParameter("@CREATED_BY",objUserInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objUserInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);
				objDataWrapper.AddParameter("@USER_SYSTEM_ID",objUserInfo.USER_SYSTEM_ID);
				objDataWrapper.AddParameter("@USER_IMAGE_FOLDER",objUserInfo.USER_IMAGE_FOLDER);
                objDataWrapper.AddParameter("@ACTIVITY", objUserInfo.ACTIVITY);
                if (objUserInfo.REG_ID_ISSUE_DATE != DateTime.MinValue)
                {
                    objDataWrapper.AddParameter("@REG_ID_ISSUE_DATE", objUserInfo.REG_ID_ISSUE_DATE);
                }
                else
                    objDataWrapper.AddParameter("@REG_ID_ISSUE_DATE", System.DBNull.Value);
                objDataWrapper.AddParameter("@REG_ID_ISSUE", objUserInfo.REG_ID_ISSUE);
                objDataWrapper.AddParameter("@CPF", objUserInfo.CPF);
                objDataWrapper.AddParameter("@REGIONAL_IDENTIFICATION", objUserInfo.REGIONAL_IDENTIFICATION);

				objDataWrapper.AddParameter("@USER_COLOR_SCHEME",objUserInfo.USER_COLOR_SCHEME);

                //added by Chetna
                objDataWrapper.AddParameter("@LANG_ID", objUserInfo.LANG_ID);

				//objDataWrapper.AddParameter("@USER_COLOR_SCHEME",null);
				objDataWrapper.AddParameter("@Country",objUserInfo.COUNTRY);
				
				objDataWrapper.AddParameter("@SSN_NO",objUserInfo.SSN_NO);
				if(objUserInfo.DATE_OF_BIRTH !=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_OF_BIRTH",objUserInfo.DATE_OF_BIRTH);
				}
				else
					objDataWrapper.AddParameter("@DATE_OF_BIRTH",System.DBNull.Value);
				objDataWrapper.AddParameter("@DRIVER_LIC_NO",objUserInfo.DRIVER_LIC_NO);
				if(objUserInfo.DATE_EXPIRY!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_EXPIRY",objUserInfo.DATE_EXPIRY);
				}
				else
					objDataWrapper.AddParameter("@DATE_EXPIRY",System.DBNull.Value);
				objDataWrapper.AddParameter("@LICENSE_STATUS",objUserInfo.LICENSE_STATUS);

				//Added by Sibin for Itrack Issue 4173 on 15 Jan 09
				if(objUserInfo.NON_RESI_LICENSE_EXP_DATE!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@NON_RESI_LICENSE_EXP_DATE",objUserInfo.NON_RESI_LICENSE_EXP_DATE);
				}
				else
					objDataWrapper.AddParameter("@NON_RESI_LICENSE_EXP_DATE",System.DBNull.Value);

				objDataWrapper.AddParameter("@NON_RESI_LICENSE_STATUS",objUserInfo.NON_RESI_LICENSE_STATUS);

				if(objUserInfo.NON_RESI_LICENSE_EXP_DATE2!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@NON_RESI_LICENSE_EXP_DATE2",objUserInfo.NON_RESI_LICENSE_EXP_DATE2);
				}
				else
					objDataWrapper.AddParameter("@NON_RESI_LICENSE_EXP_DATE2",System.DBNull.Value);

				objDataWrapper.AddParameter("@NON_RESI_LICENSE_STATUS2",objUserInfo.NON_RESI_LICENSE_STATUS2);
				//Added till here

				objDataWrapper.AddParameter("@NON_RESI_LICENSE_STATE",objUserInfo.NON_RESI_LICENSE_STATE);
				objDataWrapper.AddParameter("@NON_RESI_LICENSE_NO",objUserInfo.NON_RESI_LICENSE_NO);

				//********************
				objDataWrapper.AddParameter("@NON_RESI_LICENSE_STATE2",objUserInfo.NON_RESI_LICENSE_STATE2);
				objDataWrapper.AddParameter("@NON_RESI_LICENSE_NO2",objUserInfo.NON_RESI_LICENSE_NO2);
				//objDataWrapper.AddParameter("@BRICS_USER",objUserInfo.BRICS_USER);

				//******************
				objDataWrapper.AddParameter("@LIC_BRICS_USER",objUserInfo.LIC_BRICS_USER);
				objDataWrapper.AddParameter("@ADJUSTER_CODE",objUserInfo.ADJUSTER_CODE);
				objDataWrapper.AddParameter("@CHANGE_PWD_NEXT_LOGIN",objUserInfo.CHANGE_PWD_NEXT_LOGIN);//sibin
				objDataWrapper.AddParameter("@USER_LOCKED",objUserInfo.USER_LOCKED);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@USER_ID",objUserInfo.USER_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objUserInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Cmsweb/Maintenance/AddUser.aspx.resx");
					
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					objUserInfo.USER_PWD="";//Added by Sibin for Itrack Issue 5212 on 5 Feb 09
					string strTranXML = objBuilder.GetTransactionLogXML(objUserInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objUserInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1512", "");//"New user is added";
					//Added By Raghav For Itrack Issue #4995
					DataSet ds = null;
					ds = ClsAgency.GetAgencyIDAndNameFromCode(objUserInfo.USER_SYSTEM_ID);
					if(ds!=null && ds.Tables[0].Rows.Count>0)
					{
						if(ds.Tables[0].Rows[0]["AGENCY_DISP_NAME"]!=null && ds.Tables[0].Rows[0]["AGENCY_DISP_NAME"].ToString()!="")
							objTransactionInfo.CUSTOM_INFO		=	";Agency Name = " + ds.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString() + ";Agency Number = " + objUserInfo.USER_SYSTEM_ID.ToString();
					}
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int USER_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();				
				/*if(objUserInfo.USER_TYPE_ID==13) //user is of underwritter type
				{
					SetUserIDForUnderwriter(objUserInfo.USER_TYPE_ID);
				}*/
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (USER_ID < 0 )
				{
					return USER_ID ;
				}			
				else
				{
					objUserInfo.USER_ID = USER_ID;
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

		public int CheckUserLicence(string UserSystemId)
		{
			string strStoredProc = "Proc_CheckUserLicence";
			DataSet dsUserPreferences = new DataSet();
			string User_id ="";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);				
			try
			{
				objDataWrapper.AddParameter("@User_System_Id",UserSystemId);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@User_Id",User_id,SqlDbType.VarChar,ParameterDirection.Output,8);
				objDataWrapper.ExecuteDataSet(strStoredProc);
				if (objSqlParameter!= null && objSqlParameter.Value != null)
				{
					return (int.Parse(objSqlParameter.Value.ToString()));
				}
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				objDataWrapper.Dispose();
			}
		
			return 0;
		}

		/// <summary>
		///  Created by Sibin on 7 Nov 08 for Itrack Issue 4994-function to change user password if user login name and old password match
		/// </summary>
		/// <param name="objUserInfo"></param>
		/// <param name="New_Password"></param>
		/// <returns></returns>
		public int SaveNewPassword(ClsUserInfo objUserInfo,string New_Password)
		{
			string strStoredProc = "Proc_SaveNewPassword";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			int returnResult=0;
						
			try
			{
				objDataWrapper.AddParameter("@USER_ID",objUserInfo.USER_ID);
				objDataWrapper.AddParameter("@User_Login_ID",objUserInfo.USER_LOGIN_ID);
				objDataWrapper.AddParameter("@OLD_PWD",objUserInfo.USER_PWD);
				objDataWrapper.AddParameter("@NEW_PWD",New_Password);
				SqlParameter objRetParam = (SqlParameter) objDataWrapper.AddParameter("@RET_VALUE",System.Data.DbType.Int16,ParameterDirection.Output);
				
				if(TransactionLogRequired) 
				{
				
					objUserInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Cmsweb/aspx/ChangeAgencyPassword.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.RECORDED_BY		=	objUserInfo.USER_ID;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1559", "");// "Password is Updated";
					objTransactionInfo.CUSTOM_INFO		=	";User First Name = " + objUserInfo.USER_LOGIN_ID;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
				if (Convert.ToInt32(objRetParam.Value) > 0)
				{
					return returnResult;
				}
				else
				{
					return Convert.ToInt32(objRetParam.Value);
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

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldUserInfo">Model object having old information</param>
		/// <param name="objUserInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsUserInfo objOldUserInfo,ClsUserInfo objUserInfo)
		{
			string		strStoredProc =	"Proc_UpdateUser";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@USER_ID",objUserInfo.USER_ID);
				objDataWrapper.AddParameter("@USER_LOGIN_ID",objUserInfo.USER_LOGIN_ID);
				if(objUserInfo.SUB_CODE!="")
					objDataWrapper.AddParameter("@SUB_CODE",objUserInfo.SUB_CODE);	
				else
					objDataWrapper.AddParameter("@SUB_CODE",System.DBNull.Value);
	
				//Done by Sibin for Itrack Issue 5139 on 3 Dec 08
				//objDataWrapper.AddParameter("@USER_TYPE_ID",objUserInfo.USER_TYPE_ID);
				if(objUserInfo.USER_TYPE_ID!=0)
					objDataWrapper.AddParameter("@USER_TYPE_ID",objUserInfo.USER_TYPE_ID);
				else
					objDataWrapper.AddParameter("@USER_TYPE_ID",System.DBNull.Value);
				objDataWrapper.AddParameter("@USER_PWD",objUserInfo.USER_PWD);
				objDataWrapper.AddParameter("@USER_TITLE",objUserInfo.USER_TITLE);
				objDataWrapper.AddParameter("@USER_FNAME",objUserInfo.USER_FNAME);
				objDataWrapper.AddParameter("@USER_LNAME",objUserInfo.USER_LNAME);
				objDataWrapper.AddParameter("@USER_INITIALS",objUserInfo.USER_INITIALS);
				objDataWrapper.AddParameter("@USER_ADD1",objUserInfo.USER_ADD1);
				objDataWrapper.AddParameter("@USER_ADD2",objUserInfo.USER_ADD2);
				objDataWrapper.AddParameter("@USER_CITY",objUserInfo.USER_CITY);
				objDataWrapper.AddParameter("@USER_STATE",objUserInfo.USER_STATE);
				objDataWrapper.AddParameter("@USER_ZIP",objUserInfo.USER_ZIP);
				objDataWrapper.AddParameter("@USER_PHONE",objUserInfo.USER_PHONE);
				objDataWrapper.AddParameter("@USER_EXT",objUserInfo.USER_EXT);
				objDataWrapper.AddParameter("@USER_FAX",objUserInfo.USER_FAX);
				objDataWrapper.AddParameter("@USER_MOBILE",objUserInfo.USER_MOBILE);
				objDataWrapper.AddParameter("@USER_EMAIL",objUserInfo.USER_EMAIL);
				objDataWrapper.AddParameter("@User_Supervisor",objUserInfo.USER_SPR);
				objDataWrapper.AddParameter("@PINK_SLIP_NOTIFY",objUserInfo.PINK_SLIP_NOTIFY);
				objDataWrapper.AddParameter("@USER_MGR_ID",objUserInfo.USER_MGR_ID);
				objDataWrapper.AddParameter("@USER_DEF_DIV_ID",objUserInfo.USER_DEF_DIV_ID);
				objDataWrapper.AddParameter("@USER_DEF_DEPT_ID",objUserInfo.USER_DEF_DEPT_ID);
				objDataWrapper.AddParameter("@USER_DEF_PC_ID",objUserInfo.USER_DEF_PC_ID);
				objDataWrapper.AddParameter("@USER_TIME_ZONE",objUserInfo.USER_TIME_ZONE);
				objDataWrapper.AddParameter("@USER_NOTES",objUserInfo.USER_NOTES);//**************
				objDataWrapper.AddParameter("@IS_ACTIVE",objUserInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@MODIFIED_BY",objUserInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objUserInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@USER_SYSTEM_ID",objUserInfo.USER_SYSTEM_ID);
				objDataWrapper.AddParameter("@USER_IMAGE_FOLDER",objUserInfo.USER_IMAGE_FOLDER);
				objDataWrapper.AddParameter("@Country",objUserInfo.COUNTRY);
				objDataWrapper.AddParameter("@SSN_NO",objUserInfo.SSN_NO);
				objDataWrapper.AddParameter("@CHANGE_PWD_NEXT_LOGIN",objUserInfo.CHANGE_PWD_NEXT_LOGIN);//sibin
				objDataWrapper.AddParameter("@USER_LOCKED",objUserInfo.USER_LOCKED);
                objDataWrapper.AddParameter("@ACTIVITY", objUserInfo.ACTIVITY);
                if (objUserInfo.REG_ID_ISSUE_DATE != DateTime.MinValue)
                {
                    objDataWrapper.AddParameter("@REG_ID_ISSUE_DATE", objUserInfo.REG_ID_ISSUE_DATE);
                }
                else
                    objDataWrapper.AddParameter("@REG_ID_ISSUE_DATE", System.DBNull.Value);
                objDataWrapper.AddParameter("@REG_ID_ISSUE", objUserInfo.REG_ID_ISSUE);
                objDataWrapper.AddParameter("@CPF", objUserInfo.CPF);
                objDataWrapper.AddParameter("@REGIONAL_IDENTIFICATION", objUserInfo.REGIONAL_IDENTIFICATION);
				
				if(objUserInfo.DATE_OF_BIRTH !=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_OF_BIRTH",objUserInfo.DATE_OF_BIRTH);
				}
				else
					objDataWrapper.AddParameter("@DATE_OF_BIRTH",System.DBNull.Value);
				objDataWrapper.AddParameter("@DRIVER_LIC_NO",objUserInfo.DRIVER_LIC_NO);
				if(objUserInfo.DATE_EXPIRY!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_EXPIRY",objUserInfo.DATE_EXPIRY);
				}
				else
					objDataWrapper.AddParameter("@DATE_EXPIRY",System.DBNull.Value);
				objDataWrapper.AddParameter("@LICENSE_STATUS",objUserInfo.LICENSE_STATUS);

				//Added by Sibin for Itrack Issue 4173 on 15 Jan 09
				if(objUserInfo.NON_RESI_LICENSE_EXP_DATE!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@NON_RESI_LICENSE_EXP_DATE",objUserInfo.NON_RESI_LICENSE_EXP_DATE);
				}
				else
					objDataWrapper.AddParameter("@NON_RESI_LICENSE_EXP_DATE",System.DBNull.Value);

				objDataWrapper.AddParameter("@NON_RESI_LICENSE_STATUS",objUserInfo.NON_RESI_LICENSE_STATUS);

				if(objUserInfo.NON_RESI_LICENSE_EXP_DATE2!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@NON_RESI_LICENSE_EXP_DATE2",objUserInfo.NON_RESI_LICENSE_EXP_DATE2);
				}
				else
					objDataWrapper.AddParameter("@NON_RESI_LICENSE_EXP_DATE2",System.DBNull.Value);

				objDataWrapper.AddParameter("@NON_RESI_LICENSE_STATUS2",objUserInfo.NON_RESI_LICENSE_STATUS2);
				//Added till here

				objDataWrapper.AddParameter("@NON_RESI_LICENSE_STATE",objUserInfo.NON_RESI_LICENSE_STATE);
				objDataWrapper.AddParameter("@NON_RESI_LICENSE_NO",objUserInfo.NON_RESI_LICENSE_NO);


				//Added By Manoj Rathore(11-06-2006)
				objDataWrapper.AddParameter("@NON_RESI_LICENSE_STATE2",objUserInfo.NON_RESI_LICENSE_STATE2);
				objDataWrapper.AddParameter("@NON_RESI_LICENSE_NO2",objUserInfo.NON_RESI_LICENSE_NO2);
				//objDataWrapper.AddParameter("@BRICS_USER",objUserInfo.BRICS_USER);
				//

				objDataWrapper.AddParameter("@LIC_BRICS_USER",objUserInfo.LIC_BRICS_USER);
				objDataWrapper.AddParameter("@ADJUSTER_CODE",objUserInfo.ADJUSTER_CODE);
				SqlParameter objRetParam = (SqlParameter) objDataWrapper.AddParameter("@RETVAL",System.Data.DbType.Int16,ParameterDirection.ReturnValue);

				if(TransactionLogRequired) 
				{
				
					objUserInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Cmsweb/Maintenance/AddUser.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objOldUserInfo.USER_PWD=objUserInfo.USER_PWD="";//Added by Sibin for Itrack Issue 5212 on 5 Feb 09
					strTranXML = objBuilder.GetTransactionLogXML(objOldUserInfo,objUserInfo);
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objUserInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1555","");//"User is updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	";User First Name = " + objUserInfo.USER_FNAME + ";User Last Name = " + objUserInfo.USER_LNAME;
					//Added By Raghav For Itrack Issue #4995
					DataSet ds = null;
					ds = ClsAgency.GetAgencyIDAndNameFromCode(objUserInfo.USER_SYSTEM_ID);
					if(ds!=null && ds.Tables[0].Rows.Count>0)
					{
						if(ds.Tables[0].Rows[0]["AGENCY_DISP_NAME"]!=null && ds.Tables[0].Rows[0]["AGENCY_DISP_NAME"].ToString()!="")
							//objTransactionInfo.CUSTOM_INFO		=	";Agency Name = " + ds.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString() + ";Agency Number = " + objUserInfo.USER_SYSTEM_ID.ToString();
				             objTransactionInfo.CUSTOM_INFO 		=	";Agency Name = " + ds.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString() + ";Agency Number = " + objUserInfo.USER_SYSTEM_ID.ToString()+ ";User Name = " + objUserInfo.USER_FNAME + " " +objUserInfo.USER_LNAME.ToString() + ";User Sub Code = " + objUserInfo.SUB_CODE;
					}
				
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				/*if(objUserInfo.USER_TYPE_ID==13) //user is of underwritter type
				{
					SetUserIDForUnderwriter(objUserInfo.USER_TYPE_ID);
				}*/
				
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				if (Convert.ToInt32(objRetParam.Value) > 0)
				{
					return returnResult;
				}
				else
				{
					return Convert.ToInt32(objRetParam.Value);
				}
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

		#region Activate Deactivate
		public int ActivateDeactivateUser(ClsUserInfo objUserInfo, string strSTATUS)
		{
			string	strStoredProc =	"Proc_ActivateDeactivateMNT_USER_LIST";
			int retVal=-1;
			int returnResult;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objDataWrapper.AddParameter("@USER_ID",objUserInfo.USER_ID);
			objDataWrapper.AddParameter("@USER_SYSTEM_ID",objUserInfo.USER_SYSTEM_ID);
			objDataWrapper.AddParameter("@IS_ACTIVE",strSTATUS);
			SqlParameter paramRetVal = (SqlParameter)objDataWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			if(TransactionLogRequired) 
			{
				objUserInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Cmsweb/Maintenance/AddUser.aspx.resx");
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				//string strTranXML = objBuilder.GetTransactionLogXML(objUserInfo);
				//Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	3;
				objTransactionInfo.RECORDED_BY		=	objUserInfo.MODIFIED_BY;
				if(strSTATUS.ToUpper().Equals("N"))
					objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1556","");//"User is deactivated successfully";
				else
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1557", "");// "User is activated successfully";	
				//objTransactionInfo.CHANGE_XML = strTranXML;
				objTransactionInfo.CUSTOM_INFO		=	";User First Name = " + objUserInfo.USER_FNAME + ";User Last Name = " + objUserInfo.USER_LNAME;
				//Added By Raghav For Itrack Issue #4995
				DataSet ds = null;
				ds = ClsAgency.GetAgencyIDAndNameFromCode(objUserInfo.USER_SYSTEM_ID);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					if(ds.Tables[0].Rows[0]["AGENCY_DISP_NAME"]!=null && ds.Tables[0].Rows[0]["AGENCY_DISP_NAME"].ToString()!="")
						//objTransactionInfo.CUSTOM_INFO		=	";Agency Name = " + ds.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString() + ";Agency Number = " + objUserInfo.USER_SYSTEM_ID.ToString();
				        objTransactionInfo.CUSTOM_INFO		=	";Agency Name = " + ds.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString() + ";Agency Number = " + objUserInfo.USER_SYSTEM_ID.ToString()+ ";User Name = " + objUserInfo.USER_FNAME + " " +objUserInfo.USER_LNAME.ToString() + ";User Sub Code = " + objUserInfo.SUB_CODE;
					}
				returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
			}
			else
			{
				returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
			}
			
			if(paramRetVal.Value.ToString()!="")
				retVal = Convert.ToInt32(paramRetVal.Value);
			return retVal;
		}
		#endregion

		#region "Fill Drop down Functions"
		/// <summary>
		/// This function is used to fill User Type dropdown on User page
		/// </summary>
		/// <param name="objDropDownList"></param>
		public static void GetUserTypeDropDown(DropDownList objDropDownList)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@LANG_ID", ClsCommon.BL_LANG_ID);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_FillUserTypeDropDown").Tables[0];
			objDropDownList.DataSource = objDataTable;
			objDropDownList.DataTextField = "USER_TYPE_DESC";
			objDropDownList.DataValueField = "USER_TYPE_ID";
			objDropDownList.DataBind();
		}

		#region "Fill List box Functions"
		/// <summary>
		/// This function is used to fill User Type dropdown on User page
		/// </summary>
		/// <param name="objDropDownList"></param>
		public static void GetUserTypeDropDown(ListBox  objDropDownList)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_FillUserTypeDropDown").Tables[0];
			objDropDownList.DataSource = objDataTable;
			objDropDownList.DataTextField = "USER_TYPE_DESC";
			objDropDownList.DataValueField = "USER_TYPE_ID";
			objDropDownList.DataBind();
		}
		#endregion
		/// <summary>
		/// This function is used to fill Agency User Type dropdown on User page
		/// </summary>
		/// <param name="objDropDownList"></param>
		public static void GetAgencyUserTypeDropDown(DropDownList objDropDownList)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@AGENCY","Y");
            objDataWrapper.AddParameter("@LANG_ID", ClsCommon.BL_LANG_ID);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_FillUserTypeDropDown").Tables[0];
			objDropDownList.DataSource = objDataTable;
			objDropDownList.DataTextField = "USER_TYPE_DESC";
			objDropDownList.DataValueField = "USER_TYPE_ID";
			objDropDownList.DataBind();
		}

		/// <summary>
		/// This function is used to fill Manager drop down on User Page
		/// </summary>
		/// <param name="objDropDownList"></param>
		public static void GetUserManagerDropDown(DropDownList objDropDownList,string userSystemId)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.Text);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_FillUserManagerDropDown '"+userSystemId+"'").Tables[0];
			objDropDownList.DataSource = objDataTable;
			objDropDownList.DataTextField = "Manager Name";
			objDropDownList.DataValueField = "User_Id";
			objDropDownList.DataBind();
		}
		#endregion

		#region "Get Xml Methods"
		public static string GetXmlForPageControls(string strUserId)
		{
			//<Gaurav> 31 May 2005 START: InLine Query Changes to Stroded Proc
			string strProcedure = "Proc_GetUser";
			DataSet objDataSet = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			try
			{
				objDataWrapper.AddParameter("@UserId",strUserId);
				objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);

				if(objDataSet.Tables[0].Rows.Count!=0)
				{
					return objDataSet.GetXml();
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
			//<Gaurav> 31 May 2005 END: InLine Query Changes to Stroded Proc

			
		}

		public static DataSet GetAgencyDetails(string agencyCode)
		{
			//<Gaurav> 31 May 2005 START: InLine Query Changes to Stroded Proc
			string strProcedure = "Proc_GetAgencyDetails";
			DataSet objDataSet = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			try
			{
				objDataWrapper.AddParameter("@agency_code",agencyCode);
				objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);

				
				return objDataSet;
			
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				objDataWrapper.Dispose();
			}
			//<Gaurav> 31 May 2005 END: InLine Query Changes to Stroded Proc

			
		}
		#endregion

		#region Removed
		private const  string   ACTIVATE_DEACTIVATE_PROCEDURE_USERTYPE = "Proc_ActivateDeactivateUserType";

		/// <summary>
		/// This function is used to Update a record in MNT_USER_TYPES 
		/// Function is called from AddUserType Page 
		/// </summary>
		/// <param name="intUserTypeId">Id of the UserType</param>
		///<param name="strUserTypeCode"> This is unique for every record</param>
		/// <param name="strUserTypeDescription">Small Description for User Type</param>
		/// <param name="strUserTypeSystem">Describe System for User Type</param>
		/// <param name="intUserTypeForCarrier">Describe Carrier for User Type</param>
		/// <param name="strIsActive">Describe the status for of the User Type, default is Active - y</param>
		/// <param name="intCreatedBy">Id of the currently logged in user</param>
		/// <param name="dtCreateDateTime">Current Date And Time</param>
		/// <param name="intModifiedBy">Id of the currently logged in user in Edit mode</param>
		/// <param name="drLastUpdatedDateTime">Current date and time</param>
		/// <param name="TransactionLogReq">Transaction log required or not</param>
		/// <returns>Number of Records</returns>
		public int UpdateUserType(int intUserTypeId, string strUserTypeCode, string strUserTypeDescription, string strUserTypeSystem, int intUserTypeForCarrier, /*string strIsActive,*/ int intModifiedBy, DateTime drLastUpdatedDateTime,bool TransactionLogReq )
		{
			string strProcName		=	"Proc_UpdateUserType";
			DateTime	dtRecordDate	=	DateTime.Now;
			//int intNewUserTypeId;		//holds the value of listid of the rows which will get inserted
			int intreturnResult;		// Value to be return to called function

			Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objDataWrapper.AddParameter("@User_Type_Id",intUserTypeId);
				objDataWrapper.AddParameter("@User_Type_Code",strUserTypeCode);
				objDataWrapper.AddParameter("@User_Type_Desc",strUserTypeDescription);
				objDataWrapper.AddParameter("@User_Type_System",strUserTypeSystem);
				objDataWrapper.AddParameter("@User_Type_For_Carrier",intUserTypeForCarrier);
				//objDataWrapper.AddParameter("@Is_Active",null);
				//objDataWrapper.AddParameter("@Created_By",null);
				//objDataWrapper.AddParameter("@Created_DateTime",null);
				objDataWrapper.AddParameter("@Modified_By",intModifiedBy);
				objDataWrapper.AddParameter("@Last_Updated_DateTime",drLastUpdatedDateTime);
				

				
				
				intreturnResult		=	objDataWrapper.ExecuteNonQuery(strProcName);
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return intreturnResult;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objDataWrapper!=null)	
					objDataWrapper.Dispose();
			}
		}
		
		/// <summary>
		/// This function is used to Update a record in MNT_USER_LIST
		/// Function is called from AddUser Page 
		/// </summary>
		/// <param name="intUserId"> Id of the User</param> 
		/// <param name="strLoginId">User Id for user</param>
		/// <param name="strUserPassword">Password for login</param>
		/// <param name="strUserTitle">Title for user</param></param>
		/// <param name="strUserFirstName">First name of the user</param>
		/// <param name="strUserLastName">Last name of the user</param>
		/// <param name="strUserInitials">User Initials of user</param>
		/// <param name="strUserAddress1">First Address of User</param>
		/// <param name="strUserAddress2">Second Address of User</param>
		/// <param name="strUserCity">City of the user</param>
		/// <param name="strUserState">State of the user</param>
		/// <param name="strUserZipCode">Zip Code of the user</param>
		/// <param name="strUserPhone">Phone number of the user</param>
		/// <param name="strUserExtention">Extention number of user</param>
		/// <param name="strUserFax">Fax number of user</param>
		/// <param name="strUserMobile">Mobile number of user</param>
		/// <param name="strUserEmail">Email for user</param>
		/// <param name="strIsActive">Status of user</param>
		/// <param name="intCreatedBy">Id of the currently logged in user</param>
		/// <param name="dtCreateDateTime">Current date and time</param>
		/// <param name="oldXML">XML to check whether value has been changed or not</param>
		/// <returns>No of records </returns>

		/// <summary>
		/// This function is used to Add new record in MNT_USER_TYPES table
		/// This is called from AddUserType Page
		/// </summary>
		/// <param name="strUserTypeCode"> This is unique for every record</param>
		/// <param name="strUserTypeDescription">Small Description for User Type</param>
		/// <param name="strUserTypeSystem">Describe System for User Type</param>
		/// <param name="intUserTypeForCarrier">Describe Carrier for User Type</param>
		/// <param name="strIsActive">Describe the status for of the User Type, default is Active - y</param>
		/// <param name="intCreatedBy">Id of the currently logged in user</param>
		/// <param name="dtCreateDateTime">Current Date And Time</param>
		/// <param name="intModifiedBy">Id of the currently logged in user in Edit mode</param>
		/// <param name="drLastUpdatedDateTime">Current date and time</param>
		/// <returns>Number of Records</returns>
		public int AddUserType(ref int intUserTypeId, string strUserTypeCode, string strUserTypeDescription, string strUserTypeSystem, int intUserTypeForCarrier, string strIsActive, int intCreatedBy, DateTime dtCreateDateTime, bool TransactionLogReq )
		{
			string strProcName		=	"Proc_InsertUserType";
			DateTime	dtRecordDate	=	DateTime.Now;
			//int intNewUserTypeId;		//holds the value of listid of the rows which will get inserted
			int intreturnResult;		// Value to be return to called function


			Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objDataWrapper.AddParameter("@User_Type_Code",strUserTypeCode);
				objDataWrapper.AddParameter("@User_Type_Desc",strUserTypeDescription);
				objDataWrapper.AddParameter("@User_Type_System",strUserTypeSystem);
				objDataWrapper.AddParameter("@User_Type_For_Carrier",intUserTypeForCarrier);
				objDataWrapper.AddParameter("@Is_Active",strIsActive);
				objDataWrapper.AddParameter("@Created_By",intCreatedBy);
				objDataWrapper.AddParameter("@Created_DateTime",DateTime.Now);
				objDataWrapper.AddParameter("@Modified_By",null);
				objDataWrapper.AddParameter("@Last_Updated_DateTime",null);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@User_Type_Id",DbType.Int16,ParameterDirection.Output);
				//objDataWrapper.AddParameter("@User_Type_Id",null,SqlDbType.SmallInt,ParameterDirection.Output);


				intreturnResult		=	objDataWrapper.ExecuteNonQuery(strProcName);

				intUserTypeId	=	intUserTypeId		=	int.Parse(objSqlParameter.Value.ToString());

				if(intUserTypeId == -1)
				{
					//Duplicate Record Found
					objDataWrapper.ClearParameteres();
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					return -1;
				}
				else
				{
					objDataWrapper.ClearParameteres();
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					return intreturnResult;
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

		/// <summary>
		/// This function is used to Add new record in MNT_USER_LIST table
		/// This is called from AddUser Page
		/// </summary>
		/// <param name="strLoginId">User Id for user</param>
		/// <param name="strUserPassword">Password for login</param>
		/// <param name="strUserTitle">Title for user</param></param>
		/// <param name="strUserFirstName">First name of the user</param>
		/// <param name="strUserLastName">Last name of the user</param>
		/// <param name="strUserInitials">User Initials of user</param>
		/// <param name="strUserAddress1">First Address of User</param>
		/// <param name="strUserAddress2">Second Address of User</param>
		/// <param name="strUserCity">City of the user</param>
		/// <param name="strUserState">State of the user</param>
		/// <param name="strUserZipCode">Zip Code of the user</param>
		/// <param name="strUserPhone">Phone number of the user</param>
		/// <param name="strUserExtention">Extention number of user</param>
		/// <param name="strUserFax">Fax number of user</param>
		/// <param name="strUserMobile">Mobile number of user</param>
		/// <param name="strUserEmail">Email for user</param>
		/// <param name="strIsActive">Status of user</param>
		/// <param name="intCreatedBy">Id of the currently logged in user</param>
		/// <param name="dtCreateDateTime">Current date and time</param>
		/// <param name="TransactionLogReq">Transaction log required or not</param>
		/// <returns>Number of Records</returns>
		/// 



		/// <summary>
		/// This Function is used to update Status of a record in MNT_USER_TYPES table.
		/// This function is called from AddUserType page.
		/// </summary>
		/// <param name="UserTypeId">User Type Id to check the status of record</param>
		/// <param name="value">Value can be N or Y</param>
		/// <returns></returns>
		public bool ActivateDeactivateUserType(int intUserTypeId, string value)
		{
			try
			{
				base.strActivateDeactivateProcedure = ACTIVATE_DEACTIVATE_PROCEDURE_USERTYPE;
				base.ActivateDeactivate(intUserTypeId.ToString(),value);
				//			Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
				//			
				//				objDataWrapper.AddParameter("@User_Type_ID",intUserTypeId);
				//				objDataWrapper.AddParameter("@Is_Active",value);
				//				objDataWrapper.ExecuteNonQuery(strStoredProc);
				//				objDataWrapper.ClearParameteres();
				//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				return true;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
							
			
		}

		/// <summary>
		/// This Function is used to update Status of a record in MNT_USER_LIST table.
		/// This function is called from AddUser page.
		/// </summary>
		/// <param name="UserTypeId">Id of User type for check the status of record</param>
		/// <param name="value">Value can be Y or N</param>
		/// <returns></returns>
	
		#endregion

		#region Functions For Customer Account Info
		
		public DataSet FillUser(string UserTypeCode,int intAgencyId)
		{
			string		strStoredProc	=	"Proc_GetUserDetails";//Will be replaced with CONST
						
			DataSet dsAccounts = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
						
			objDataWrapper.AddParameter("@USER_TYPE_CODE",UserTypeCode);
			objDataWrapper.AddParameter("@AgencyId",intAgencyId);
			dsAccounts = objDataWrapper.ExecuteDataSet(strStoredProc);
		
			return dsAccounts;
		}
		//COMMENETD BY PAWAN, NOT BEING USED
		//		public DataSet FillUser(string UserTypeCode,string AgencyCode)
		//		{
		//			string		strStoredProc	=	"Proc_GetUserDetails2";//Will be replaced with CONST
		//						
		//			DataSet dsAccounts = new DataSet();
		//			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
		//						
		//			objDataWrapper.AddParameter("@USER_TYPE_CODE",UserTypeCode);
		//			objDataWrapper.AddParameter("@AgencyCode",AgencyCode);
		//			dsAccounts = objDataWrapper.ExecuteDataSet(strStoredProc);
		//		
		//			return dsAccounts;
		//		}

		public DataSet FillUser(string UserTypeCode)
		{
			string		strStoredProc	=	"Proc_GetUserDetails";//Will be replaced with CONST
						
			DataSet dsAccounts = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
						
			objDataWrapper.AddParameter("@USER_TYPE_CODE",UserTypeCode);
			objDataWrapper.AddParameter("@AgencyId",null);
			dsAccounts = objDataWrapper.ExecuteDataSet(strStoredProc);
		
			return dsAccounts;
		}
		public DataSet FillUser()
		{
			string		strStoredProc	=	"Proc_GetUserDetails";//Will be replaced with CONST
						
			DataSet dsAccounts = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
						
			objDataWrapper.AddParameter("@USER_TYPE_CODE",null);
			dsAccounts = objDataWrapper.ExecuteDataSet(strStoredProc);
		
			return dsAccounts;
		}
		
		#endregion

		#region "Get Xml for User Preferences"
		/// <summary>
		/// Fetching data for the user for updating the user preferences.
		/// </summary>
		/// <param name="strUserId"></param>
		/// <returns></returns>
		public static string GetXmlForUserPreferences(int intUserId)
		{
			string strStoredProc = "Proc_GetUserForPreferences";
			DataSet dsUserPreferences = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);				
			try
			{
				objDataWrapper.AddParameter("@USERID",intUserId);
				dsUserPreferences = objDataWrapper.ExecuteDataSet(strStoredProc);
				if (dsUserPreferences.Tables[0].Rows.Count != 0)
				{
					return dsUserPreferences.GetXml();
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

		public static string GetUserIDForUnderwriter(int intUserId)
		{
			string strStoredProc = "Proc_GetUserIDForUnderwriter";
			DataSet dsUserPreferences = new DataSet();
			string strUserId="",tempUserID;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);				
			try
			{
				objDataWrapper.AddParameter("@USERID",intUserId);
				dsUserPreferences = objDataWrapper.ExecuteDataSet(strStoredProc);
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (dsUserPreferences.Tables[0].Rows.Count != 0)
				{
					for(int i=0;i<dsUserPreferences.Tables[0].Rows.Count;i++)
					{
						tempUserID=dsUserPreferences.Tables[0].Rows[i]["USER_ID"].ToString();
						strUserId+=tempUserID + ",";
					}
					if(strUserId.Length>0)
					{
						strUserId= strUserId.Substring(0,(strUserId.Length-1));// "56,66,72,80,179,187,"						
					}
				}				
				return strUserId;
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

		public static void SetUserIDForUnderwriter(int UserID)
		{
			//string strStoredProc = "Proc_SetUserIDForUnderwriter";
			DataSet dsUserPreferences = new DataSet();
			string strUserID = GetUserIDForUnderwriter(UserID);
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);				
			try
			{
				if(strUserID.Length>0)
				{					
					objDataWrapper.AddParameter("@USERID",strUserID);
					objDataWrapper.AddParameter("@AGENCY_ID",AGENCY_ID);
					objDataWrapper.ExecuteNonQuery("Proc_SetUserIDForUnderwriter");	
					objDataWrapper.ClearParameteres();
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
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

		//ADDED BY PRAVEEN KUMAR(26-03-2009)

		public int AddUserAgency(string loggedUser,int userid,string agencyid,string assignAgency,string userSubCode)
		{
			string strProc = "PROC_INSERTMNT_USER_AGENCY";
			DataSet userName = new DataSet();
			
			DataSet agency = new DataSet();
			int returnResult =0;
			userName = GetUserName(loggedUser);
			agency = Populate_UnAssignedAgency(userid);
			
			StringBuilder sb = new StringBuilder();
			for(int i =0;i< agency.Tables[0].Rows.Count;i++)
			{
				if(i == agency.Tables[0].Rows.Count-1)
					sb.Append(agency.Tables[0].Rows[i][1]);
				else
					sb.Append(agency.Tables[0].Rows[i][1]+", ");
			}

			DataSet ds = null;
			ds = ClsAgency.GETUSER_AGENCYNAME_CODE(userid);

			
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				
				objDataWrapper.AddParameter("@USERID",userid);
				objDataWrapper.AddParameter("@AGENCYID",agencyid);
				
				if(TransactionLogRequired) 
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.RECORDED_BY		=	int.Parse(loggedUser);//userid;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1558", "");// "Agency assignment has been Updated.";
					if(ds!=null && ds.Tables[0].Rows.Count>0)
					{
						if(userSubCode != "" || userSubCode != null)
							objTransactionInfo.CUSTOM_INFO		=	";Agency Name = " + ds.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString() + ";Agency Number = " + ds.Tables[0].Rows[0]["AGENCY_CODE"].ToString() + ";User Name : " + userName.Tables[0].Rows[0][0]+";User Sub Code : " + userSubCode+"; Previous Assigned Agency Name(s) : " + sb.ToString()+"; New Assigned Agency Name(s) : " + assignAgency;
						else
							objTransactionInfo.CUSTOM_INFO		=	";Agency Name = " + ds.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString() + ";Agency Number = " + ds.Tables[0].Rows[0]["AGENCY_CODE"].ToString() + ";User Name : " + userName.Tables[0].Rows[0][0]+"; Previous Assigned Agency Name(s) : " + sb.ToString()+"; New Assigned Agency Name(s) : " + assignAgency;
					}
					else
						objTransactionInfo.CUSTOM_INFO		=	";User Name : " + userName.Tables[0].Rows[0][0]+"; Previous Assigned Agency Name(s) : " + sb.ToString()+"; New Assigned Agency Name(s) : " + assignAgency;
					returnResult = objDataWrapper.ExecuteNonQuery(strProc,objTransactionInfo);
				}
				
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);	
				
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				objDataWrapper.Dispose();
			}
			return returnResult;
		}

		public static DataSet Populate_AssignedAgency(int userid)
		{
			string strStoredProc	=	"PROC_GETUSERAGENCY_LIST";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				objDataWrapper.AddParameter("@USERID",userid);
				ds = objDataWrapper.ExecuteDataSet(strStoredProc);
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return ds;
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

		public static DataSet Populate_UnAssignedAgency(int userid)
		{
			string strStoredProc	=	"PROC_GETUNASSIGNEDAGENCY_USER";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				objDataWrapper.AddParameter("@USERID",userid);
				ds = objDataWrapper.ExecuteDataSet(strStoredProc);
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return ds;
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

        
		//END PRAVEEN KUMAR

        //Added by Chetna on 26th Feb,10
        //Method to fetch data from MNT_LANGUAGE_MASTER
        public DataSet GetDDLLanguage()
        {
            string strStoredProc = "Proc_GetLanguageList";
            DataSet ds = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@LANG_ID", BlCommon.ClsCommon.BL_LANG_ID);
            try
            {
                ds = objDataWrapper.ExecuteDataSet(strStoredProc);
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return ds;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }

		/// <summary>
		/// For updating user preferences in MNT_USER_LIST table.
		/// </summary>
		/// <param name="objOldUserInfo"></param>
		/// <param name="objUserInfo"></param>
		/// <returns></returns>
		public int UpdateUserPreferences(ClsUserInfo objOldUserInfo,ClsUserInfo objUserInfo)
		{
			string strStoredProc =	"Proc_UpdateUserPreferences";
			string strTranXML;
			int returnResult = 0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@USERID",objUserInfo.USER_ID);				
				objDataWrapper.AddParameter("@MODIFIED_BY",objUserInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objUserInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@USER_COLOR_SCHEME",objUserInfo.USER_COLOR_SCHEME);
				objDataWrapper.AddParameter("@GRID_SIZE",objUserInfo.GRID_SIZE);
                objDataWrapper.AddParameter("@Lang_ID", objUserInfo.LANG_ID);

				if(TransactionLogRequired)
				{
					objUserInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Cmsweb/Maintenance/AddUserPreferences.aspx.resx");Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strUpdate = objBuilder.GetUpdateSQL(objOldUserInfo,objUserInfo,out strTranXML);					
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objUserInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1515", "");// "User preference is modified";
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
			}		
		}

		public static DataSet GetUserName(string UserID)
		{
			string		strStoredProc	=	"Proc_GetUserName";//Will be replaced with CONST
						
			DataSet dsTemp = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
						
			objDataWrapper.AddParameter("@UserId",int.Parse(UserID));
			dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);
		
			return dsTemp;
		}


		/// <summary>
		/// Created by Sibin on 7 Nov 08 for Itrack Issue 4994- Function returns the User Login Name
		/// </summary>
		/// <param name="UserID"></param>
		/// <returns></returns>
		public static DataSet GetUserLoginName(string UserID)
		{
			string		strStoredProc	=	"Proc_GetUserLoginName";
						
			DataSet dsTemp = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
						
			objDataWrapper.AddParameter("@UserId",int.Parse(UserID));
			dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);
		
			return dsTemp;
		}
        //added by avijit goswami,for SUB_CODE value change automatically for adding new user
        //tfs id= ,date=07/12/2011
        public string GetSubCode()
        {
            string strStoredProc = "Proc_GetUserSubCode";

            DataSet dsTemp = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            
            dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);

            return dsTemp.Tables[0].Rows[0][0].ToString();
        }
    }
}
