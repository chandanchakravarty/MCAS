using System;
using System.Data;
using System.Text;
using System.Xml;
using Cms.DataLayer;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using Cms.Model.Maintenance;
/******************************************************************************************
	<Author					: Vijay Joshi- >
	<Start Date				: March 18, 2005-	>
	<End Date				: - >
	<Description			: - This class will be used for add, edit or deletion entries in ATTACHMENT table>
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - > 06-05-2005
	<Modified By			: - >Pradeep Iyer
	<Purpose				: - >Added a GetAttachmentById function
	
	<Modified Date			: - > 06-10-2005
	<Modified By			: - > Vijay Arora
	<Purpose				: - > Add New Attachment Function For Application.
	Testinh Check in 
	Testing Check in  --Shikha
*******************************************************************************************/
namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsAttachment.
	/// </summary>
	public class ClsAttachment: Cms.BusinessLayer.BlCommon.ClsCommon
	{
		private const string MNT_ATTACHMENT_LIST = "MNT_ATTACHMENT_LIST";
		
		#region variables, function and Constants to be used for imporsonation
		// Declare the logon types as constants
		const int LOGON32_LOGON_INTERACTIVE = 2;
		const int LOGON32_LOGON_NETWORK = 3;

		// Declare the logon providers as constants
		const int LOGON32_PROVIDER_DEFAULT = 0;
		const int LOGON32_PROVIDER_WINNT50 = 3;
		const int LOGON32_PROVIDER_WINNT40 = 2;
		const int LOGON32_PROVIDER_WINNT35 = 1;

		WindowsImpersonationContext impersonationContext; 

		[DllImport("advapi32.dll", CharSet=CharSet.Auto)]
		public static extern int LogonUser(String lpszUserName,	String lpszDomain,String lpszPassword,int dwLogonType, int dwLogonProvider,ref IntPtr phToken);
		[DllImport("advapi32.dll", CharSet=System.Runtime.InteropServices.CharSet.Auto, SetLastError=true)]
		public extern static int DuplicateToken(IntPtr hToken, 	int impersonationLevel,  ref IntPtr hNewToken);
		#endregion

		public ClsAttachment()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// This function is used to Add attachment
		/// </summary>
		/// <param name="EntityId"></param>
		/// <param name="FileName"></param>
		/// <param name="Date"></param>
		/// <param name="User"></param>
		/// <param name="FileDesc"></param>
		/// <param name="Type"></param>
		/// <returns>No. of rows inserted</returns>
		public int AddAttachment(ref int intAttachmentId,int intEntityId, string strFileName
				, DateTime dtdate, int intUserId, string strFileDesc, string strType
                , System.IO.Stream objFileStream, int CustomerID, int intApplicationID, int intAppVersionID, string strEntityType, int intAttachType)
		{
			return AddAttachment(ref intAttachmentId, intEntityId, strFileName
				, dtdate, intUserId, strFileDesc, strType
				, objFileStream, strEntityType
                , CustomerID,intApplicationID,intAppVersionID, intUserId, intAttachType);
		}

		public int AddAttachment(ref int intAttachmentId,int intEntityId, string strFileName
			, DateTime dtdate, int intUserId, string strFileDesc, string strType
            , System.IO.Stream objFileStream, string strEntityType, int CustomerID, int intApplicationID, int intAppVersionID, int intUserID, int intAttachType)
		{
			return AddAttachment(ref intAttachmentId, intEntityId, strFileName
				, dtdate, intUserId, strFileDesc, strType
				, objFileStream, strEntityType
                , CustomerID,intApplicationID,intAppVersionID, intUserId, "", intAttachType);
		}

		public int AddAttachment(ref int intAttachmentId,int intEntityId, string strFileName
			, DateTime dtdate, int intUserId, string strFileDesc, string strType
            , System.IO.Stream objFileStream, string strEntityType, int CustomerID, int intApplicationID, int intAppVersionID, int intUserID, string strCustomInfo, int intAttachType)
		{
			string		strStoredProc	=	"Proc_InsertAttachment";
			int			returnResult;
			string entityName = "", entityCode = "";

			string []entity = strEntityType.Split('~');
			if(entity.Length > 2)
			{
				entityName = entity[1];
				entityCode = entity[2];
			}
			strEntityType = entity[0];

			Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objDataWrapper.AddParameter("@LOC","");
				objDataWrapper.AddParameter("@EntityID",intEntityId);
				objDataWrapper.AddParameter("@FileName",strFileName);
				objDataWrapper.AddParameter("@AttachDate",dtdate);
				objDataWrapper.AddParameter("@UserId",intUserId);
				objDataWrapper.AddParameter("@FileDesc",strFileDesc);
				objDataWrapper.AddParameter("@PolicyId",null);
				objDataWrapper.AddParameter("@PolVarId",null);
				objDataWrapper.AddParameter("@GenFileName","");
				objDataWrapper.AddParameter("@FileType",strType);
				objDataWrapper.AddParameter("@EntityType",strEntityType);
				//added by vj 
                objDataWrapper.AddParameter("@CustomerID", CustomerID);
                objDataWrapper.AddParameter("@ApplicationID", intApplicationID);
                objDataWrapper.AddParameter("@ApplicationVerID", intAppVersionID);
				objDataWrapper.AddParameter("@AttachType",intAttachType);
				
				string strAttachType ="";
				switch(intAttachType)
				{
					case 11791:
						strAttachType ="Home Photograph";
						break;
					case 11792:
						strAttachType ="Protective Device Certificate";
						break;
					case 11793:
						strAttachType ="Scheduled Articles Photograph";
						break;
					case 11794:
						strAttachType ="Appraisal";
						break;
					case 11795:
						strAttachType ="Bill of Sale";
						break;
					case 11796:
						strAttachType ="Other";
						break;
					case 11933:
						strAttachType ="Supporting Document";
						break;
				}
				


				//Defining the output variable for retreiving the attachment id
				//objDataWrapper.AddParameter("@AttachmentId",null,SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@AttachmentId",null,SqlDbType.Int,ParameterDirection.Output);

				if(TransactionLogRequired)
				{					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	191;
					objTransactionInfo.RECORDED_BY		=	intUserID;					
					objTransactionInfo.CLIENT_ID		=	CustomerID;
					if(strEntityType == "Mortgage")
						objTransactionInfo.TRANS_DESC	=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1540","");//Attachment at Additional Interest has been added";
					else if(strEntityType == "Division")
						objTransactionInfo.TRANS_DESC	=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1539","");//"Attachment at Division has been added";						
					else if(strEntityType == "Department")
						objTransactionInfo.TRANS_DESC	=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1538","");//"Attachment at Department has been added";
					else if(strEntityType == "ProfitCenter")
						objTransactionInfo.TRANS_DESC	=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1537","");//"Attachment at Profit Center has been added";
					else if(strEntityType == "Vendor")
						objTransactionInfo.TRANS_DESC	=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1536","");//"Attachment at Vendor has been added";
					else if(strEntityType == "BankInformation")
						objTransactionInfo.TRANS_DESC	=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1535","");//"Attachment at Bank Information is added";
					else
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1534", "");// "Attachment at customer assistant has been added";

					if (strCustomInfo.Trim() == "")
					{
						Cms.Model.Maintenance.ClsDivisionInfo objDivisionInfo  = new Cms.Model.Maintenance.ClsDivisionInfo();						
						Cms.Model.Maintenance.ClsDepartmentInfo objDepartmentInfo  = new Cms.Model.Maintenance.ClsDepartmentInfo();
						Cms.Model.Maintenance.ClsProfitCenterInfo objProfitCenterInfo  = new Cms.Model.Maintenance.ClsProfitCenterInfo();
						if (strEntityType == "Division")
						{
							objTransactionInfo.CUSTOM_INFO = "; Division Name:" + entityName +"<br>"+
								"; Division Code:" + entityCode +"<br>"+
								"; File Name:"     + strFileName +"<br>"+
								"; File Type:"     + strType +"<br>"+ 
								"; File Description:" + strFileDesc +"<br>"+
								"; Attachment Type:"  + strAttachType;
  
						}
						else if (strEntityType == "Department")
						{
							objTransactionInfo.CUSTOM_INFO = "; Department Name:" + entityName +"<br>"+
								"; Department Code:" + entityCode +"<br>"+
								"; File Name:"     + strFileName +"<br>"+
								"; File Type:"     + strType +"<br>"+ 
								"; File Description:" + strFileDesc +"<br>"+
								"; Attachment Type:"  + strAttachType;
						}
						else if (strEntityType == "ProfitCenter")
						{
							objTransactionInfo.CUSTOM_INFO = "; ProfitCenter Name:" + entityName +"<br>"+
								"; ProfitCenter Code:" + entityCode +"<br>"+
								"; File Name:"         + strFileName +"<br>"+
								"; File Type:"         + strType +"<br>"+ 
								"; File Description:"  + strFileDesc +"<br>"+
								"; Attachment Type:"   + strAttachType;
						}
						else if (strEntityType == "Mortgage")
						{
							objTransactionInfo.CUSTOM_INFO = "; Additional Interest Name:" + entityName +"<br>"+
								"; Additional Interest Code:" + entityCode +"<br>"+
								"; File Name:"         + strFileName +"<br>"+
								"; File Type:"         + strType +"<br>"+ 
								"; File Description:"  + strFileDesc +"<br>"+
								"; Attachment Type:"   + strAttachType;
						}
						else if (strEntityType == "Vendor")
						{
							objTransactionInfo.CUSTOM_INFO = "; Company Name: " + entityName +"<br>"+
								"; Vendor Code:" + entityCode +"<br>"+
								"; File Name:"         + strFileName +"<br>"+
								"; File Type:"         + strType +"<br>"+ 
								"; File Description:"  + strFileDesc +"<br>"+
								"; Attachment Type:"   + strAttachType;
						}
						else
						{
                            objTransactionInfo.CUSTOM_INFO = "; "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1841","")+" "+ strFileName + "<br>" + "; "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1842","")+" "+ strType + "<br>" + "; "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1843","")+" "+ strFileDesc + "<br>" + "; "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1844","")+" "+ strAttachType; //"; File Name:" + strFileName + "<br>" + "; File Type:" + strType + "<br>" + "; File Description:" + strFileDesc + "<br>" + "; Attachment Type:" + strAttachType;
						}
					}
					else
					{
                        objTransactionInfo.CUSTOM_INFO = strCustomInfo + "; "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1841","")+" "+ strFileName + "<br>" + "; "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1842","")+" "+ strType + "<br>" + "; "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1843","")+" "+ strFileDesc + "<br>" + "; "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1844","")+" "+ intAttachType;//strCustomInfo + "; File Name:" + strFileName +"<br>"+"; File Type:"+ strType +"<br>"+ "; File Description:"+ strFileDesc +"<br>"+"; Attachment Type:" + intAttachType;
					}
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc ,objTransactionInfo);
					
				}
				else
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);				
				intAttachmentId = int.Parse(objSqlParameter.Value.ToString());
				
				objDataWrapper.ClearParameteres();
			
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
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

		public int AddAttachment(ClsAttachmentInfo objAttachmentInfo, ref int intAttachmentId,System.IO.Stream objFileStream,string strCustomInfo,int intAttachType)
		{
			string		strStoredProc	=	"Proc_InsertAttachment";
			int			returnResult;

			Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objDataWrapper.AddParameter("@LOC","");
				objDataWrapper.AddParameter("@EntityID",objAttachmentInfo.ATTACH_ENT_ID);
				objDataWrapper.AddParameter("@FileName",objAttachmentInfo.ATTACH_FILE_NAME);
				objDataWrapper.AddParameter("@AttachDate",objAttachmentInfo.ATTACH_DATE_TIME);
				objDataWrapper.AddParameter("@UserId",objAttachmentInfo.ATTACH_USER_ID);
				objDataWrapper.AddParameter("@FileDesc",objAttachmentInfo.ATTACH_FILE_DESC);
				objDataWrapper.AddParameter("@PolicyId",null);
				objDataWrapper.AddParameter("@PolVarId",null);
				objDataWrapper.AddParameter("@GenFileName","");
				objDataWrapper.AddParameter("@FileType",objAttachmentInfo.ATTACH_FILE_TYPE);
				objDataWrapper.AddParameter("@EntityType",objAttachmentInfo.ATTACH_ENTITY_TYPE);
				//added by vj 
				objDataWrapper.AddParameter("@CustomerID",objAttachmentInfo.ATTACH_CUSTOMER_ID);
				objDataWrapper.AddParameter("@ApplicationID",0);
				objDataWrapper.AddParameter("@ApplicationVerID",0);
				objDataWrapper.AddParameter("@AttachType",intAttachType);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@AttachmentId",null,SqlDbType.Int,ParameterDirection.Output);

				if(TransactionLogRequired)
				{					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 191;
					objTransactionInfo.RECORDED_BY		=	objAttachmentInfo.CREATED_BY;					
					objTransactionInfo.CLIENT_ID		=	objAttachmentInfo.ATTACH_CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1534","");//"Attachment at customer assistant has been added";					
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					// + ";File " + objAttachmentInfo.ATTACH_FILE_NAME + " is attached.";
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc ,objTransactionInfo);
					
				}
				else
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);				
				intAttachmentId = int.Parse(objSqlParameter.Value.ToString());
				
				objDataWrapper.ClearParameteres();
			
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
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
		/// This function is used to Add Application attachment
		/// </summary>
		/// <param name="intAttachmentId"></param>
		/// <param name="intEntityId"></param>
		/// <param name="strFileName"></param>
		/// <param name="dtdate"></param>
		/// <param name="intUserId"></param>
		/// <param name="strFileDesc"></param>
		/// <param name="strType"></param>
		/// <param name="objFileStream"></param>
		/// <param name="strEntityType"></param>
		/// <param name="intCustomerID"></param>
		/// <param name="intApplicationID"></param>
		/// <param name="intAppVersionID"></param>
		/// <returns>No. or rows inserted</returns>
		public int AddAttachment(ref int intAttachmentId,int intEntityId, string strFileName, 
			DateTime dtdate, int intUserId, string strFileDesc, string strType, 
			System.IO.Stream objFileStream, string strEntityType, int intCustomerID, 
			int intApplicationID, int intAppVersionID, int PolicyID, int PolicyVerID, int intAttachType)
		{
			string		strStoredProc	=	"Proc_InsertAttachment";
			int			returnResult;

			Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objDataWrapper.AddParameter("@LOC","");
				objDataWrapper.AddParameter("@EntityID",intEntityId);
				objDataWrapper.AddParameter("@FileName",strFileName);
				objDataWrapper.AddParameter("@AttachDate",dtdate);
				objDataWrapper.AddParameter("@UserId",intUserId);
				objDataWrapper.AddParameter("@FileDesc",strFileDesc);
				objDataWrapper.AddParameter("@PolicyId",PolicyID);
				objDataWrapper.AddParameter("@PolVarId",PolicyVerID);
				objDataWrapper.AddParameter("@GenFileName","");
				objDataWrapper.AddParameter("@FileType",strType);
				objDataWrapper.AddParameter("@EntityType",strEntityType);
				objDataWrapper.AddParameter("@CustomerID",intCustomerID);
				objDataWrapper.AddParameter("@ApplicationID",intApplicationID);
				objDataWrapper.AddParameter("@ApplicationVerID",intAppVersionID);
				objDataWrapper.AddParameter("@AttachType",intAttachType);
				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@AttachmentId",null,SqlDbType.Int,ParameterDirection.Output);

				if(TransactionLogRequired)
				{					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 191;
					objTransactionInfo.APP_ID			=	intApplicationID;
					objTransactionInfo.APP_VERSION_ID	=	intAppVersionID;
					objTransactionInfo.CLIENT_ID		=	intCustomerID;
					objTransactionInfo.RECORDED_BY		=	intUserId;
                    objTransactionInfo.POLICY_ID = PolicyID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = PolicyVerID;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1547", "");// "Attachment at application has been added";
                    objTransactionInfo.CUSTOM_INFO = "; "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1841","")+" "+ strFileName + "<br>" + "; "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1844","")+" "+ intAttachType + "<br>" + "; "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1842","")+" "+ strType; //"; File Name: " + strFileName + "<br>" + "; Attachment Type :" + intAttachType + "<br>" + "; File Type:" + strType;
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
				intAttachmentId = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
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

		
		public int GetAppAttachSourceID(int AttachId)
		{
			string		strStoredProc	=	"Proc_GetAppAttachSourceID";
			int			returnResult;
			int			returnSourceId;

			Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objDataWrapper.AddParameter("@ATTACH_ID",AttachId);
				objDataWrapper.AddParameter("@ATTACH_SOURCE_ID",null,SqlDbType.Int,ParameterDirection.Output);

				returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
				returnSourceId	=	int.Parse(objDataWrapper.CommandParameters[1].Value.ToString());

				objDataWrapper.ClearParameteres();
			
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				return returnSourceId;
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
		/// This function is used to update any entry of attachment
		/// </summary>
		/// <param name="AttachmentId">Id of record </param>
		/// <param name="FileDesc">New description of file</param>
		/// <param name="oldXML">XML containing old data</param>
		/// <returns>No. of rows effected</returns>
		public int Update(int intAttachmentID
			, string strFileDesc
			, string intAttachType
			,string oldXML)
		{
			XmlDocument		xmlDoc					=		new XmlDocument();
			xmlDoc.LoadXml(oldXML);
			StringBuilder	updateSql				=		new StringBuilder();
		

			updateSql.Append("UPDATE " + MNT_ATTACHMENT_LIST + " set ");

			string strOldFileDesc	= GetNodeValue(xmlDoc,"//ATTACH_FILE_DESC");
			
	//Changed datatype to string from int of variable intOldAttachType, on 11-Aug-09 for Itrack 6242, Charles
			string intOldAttachType = GetNodeValue(xmlDoc,"//ATTACH_TYPE");
			string dtOldATATCH = GetNodeValue(xmlDoc,"//ATTACH_DATE_TIME");
				
			Cms.DataLayer.SqlUpdateBuilder	objSqlBuilder = new Cms.DataLayer.SqlUpdateBuilder();

			//Passing the column names and its old and new value to the SQLUpdateBuilder class
			//Then this class will give us the whole update query
			objSqlBuilder.AddColumn("ATTACH_FILE_DESC",strFileDesc,strOldFileDesc,Cms.DataLayer.MSSQLType.NVarChar,false);
//			objSqlBuilder.AddColumn("ATTACH_TYPE","\"" + intAttachType + "\"","\"" + intOldAttachType + "\"" ,Cms.DataLayer.MSSQLType.Int,false);
			objSqlBuilder.AddColumn("ATTACH_TYPE",intAttachType.ToString() ,intOldAttachType.ToString(),Cms.DataLayer.MSSQLType.Int,false);

            //for itrack 466
			//objSqlBuilder.AddColumn("ATTACH_DATE_TIME",DateTime.Now.ToString() ,dtOldATATCH,Cms.DataLayer.MSSQLType.DateTime,false);
				
			objSqlBuilder.TableName		= MNT_ATTACHMENT_LIST;							//Table, which will be updated
			objSqlBuilder.WhereClause	= " WHERE ATTACH_ID = " + intAttachmentID ;	//Where condition used for updation

				
			//Retreiving the Update query
			string strUpdateSql	= objSqlBuilder.GetUpdateSQL();
			
			objSqlBuilder.ClearColumns();	
			int returnResult = 0;	
			if(strUpdateSql != "")
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.Text,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				try
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strUpdateSql);
						
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					return returnResult;
				}
				catch(Exception ex)
				{
					throw(ex);
				}
				finally
				{
					if(xmlDoc != null) 
					{
						xmlDoc = null;
					}
					if(updateSql != null) 
					{
						updateSql = null;
					}
					if(objDataWrapper != null) 
					{
						objDataWrapper.Dispose();
					}
					if(objSqlBuilder != null) 
					{
						objSqlBuilder = null;
					}
				}
			}
			else
			{
				return 1;
			}
		}

		public int Update(int intAttachmentID
			, string strFileDesc
			,string oldXML,int CustomerID, int intUserID, int flag)				
		{
			XmlDocument		xmlDoc					=		new XmlDocument();
			xmlDoc.LoadXml(oldXML);
			StringBuilder	updateSql				=		new StringBuilder();
			// string tmp;

			updateSql.Append("UPDATE " + MNT_ATTACHMENT_LIST + " set ");

			string strOldFileDesc	= GetNodeValue(xmlDoc,"//ATTACH_FILE_DESC");
				
			Cms.DataLayer.SqlUpdateBuilder	objSqlBuilder = new Cms.DataLayer.SqlUpdateBuilder();

			//Passing the column names and its old and new value to the SQLUpdateBuilder class
			//Then this class will give us the whole update query
			objSqlBuilder.AddColumn("ATTACH_FILE_DESC",strFileDesc,strOldFileDesc,Cms.DataLayer.MSSQLType.NVarChar,false);
				
			objSqlBuilder.TableName		= MNT_ATTACHMENT_LIST;							//Table, which will be updated
			objSqlBuilder.WhereClause	= " WHERE ATTACH_ID = " + intAttachmentID ;	//Where condition used for updation

				
			//Retreiving the Update query
			string strUpdateSql	= objSqlBuilder.GetUpdateSQL();
			
			objSqlBuilder.ClearColumns();	

			int returnResult = 0;		

			if(strUpdateSql != "")
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.Text,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				try
				{
					if(TransactionLogRequired)
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;						
						objTransactionInfo.CLIENT_ID		=	CustomerID;
						objTransactionInfo.RECORDED_BY		=	intUserID;
						//objTransactionInfo.CUSTOM_INFO		=	"; File Name: " + strFileName +"<br>"+ "; Attachment Type :" + intAttachType +"<br>"+"; File Type:" + strType;
                        if (flag == 1)
                            objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1541", "");//	"Attachment at customer assistant has been modified.";												
                        else
                            objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1542", "");// "Attachment at application has been modified.";												

						returnResult	= objDataWrapper.ExecuteNonQuery(strUpdateSql,objTransactionInfo);
					}
					else
						returnResult = objDataWrapper.ExecuteNonQuery(strUpdateSql);
						
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					return returnResult;
				}
				catch(Exception ex)
				{
					throw(ex);
				}
				finally
				{
					if(xmlDoc != null) 
					{
						xmlDoc = null;
					}
					if(updateSql != null) 
					{
						updateSql = null;
					}
					if(objDataWrapper != null) 
					{
						objDataWrapper.Dispose();
					}
					if(objSqlBuilder != null) 
					{
						objSqlBuilder = null;
					}
				}
			}
			else
			{
				return 1;
			}
		}

		public int Update(int intAttachmentID
			, string strFileDesc
			,string oldXML,int CustomerID, int ApplicationID, int AppVersionID,int intUserID, int flag)				
		{
			XmlDocument		xmlDoc					=		new XmlDocument();
			xmlDoc.LoadXml(oldXML);
			StringBuilder	updateSql				=		new StringBuilder();
		

			updateSql.Append("UPDATE " + MNT_ATTACHMENT_LIST + " set ");

			string strOldFileDesc	= GetNodeValue(xmlDoc,"//ATTACH_FILE_DESC");
				
			Cms.DataLayer.SqlUpdateBuilder	objSqlBuilder = new Cms.DataLayer.SqlUpdateBuilder();

			//Passing the column names and its old and new value to the SQLUpdateBuilder class
			//Then this class will give us the whole update query
			objSqlBuilder.AddColumn("ATTACH_FILE_DESC",strFileDesc,strOldFileDesc,Cms.DataLayer.MSSQLType.NVarChar,false);
				
			objSqlBuilder.TableName		= MNT_ATTACHMENT_LIST;							//Table, which will be updated
			objSqlBuilder.WhereClause	= " WHERE ATTACH_ID = " + intAttachmentID ;	//Where condition used for updation

				
			//Retreiving the Update query
			string strUpdateSql	= objSqlBuilder.GetUpdateSQL();
			
			objSqlBuilder.ClearColumns();	

			int returnResult = 0;		
			if(strUpdateSql != "")
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.Text,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				try
				{
					if(TransactionLogRequired)
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;						
						objTransactionInfo.CLIENT_ID		=	CustomerID;
						objTransactionInfo.RECORDED_BY		=	intUserID;
						objTransactionInfo.APP_ID			=	ApplicationID;
						objTransactionInfo.APP_VERSION_ID	=	AppVersionID;
						//objTransactionInfo.CUSTOM_INFO		=	"; File Name: " + strFileName +"<br>"+ "; Attachment Type :" + intAttachType +"<br>"+"; File Type:" + strType;
						if(flag==1)
                            objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1541", "");//"Attachment at customer assistant has been modified.";												
						else
                            objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1542", "");// "Attachment at application has been modified.";												

						returnResult	= objDataWrapper.ExecuteNonQuery(strUpdateSql,objTransactionInfo);
					}
					else
						returnResult = objDataWrapper.ExecuteNonQuery(strUpdateSql);
						
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					return returnResult;
				}
				catch(Exception ex)
				{
					throw(ex);
				}
				finally
				{
					if(xmlDoc != null) 
					{
						xmlDoc = null;
					}
					if(updateSql != null) 
					{
						updateSql = null;
					}
					if(objDataWrapper != null) 
					{
						objDataWrapper.Dispose();
					}
					if(objSqlBuilder != null) 
					{
						objSqlBuilder = null;
					}
				}
			}
			else
			{
				return 1;
			}
		}

		public int Update(int intAttachmentID
			, string strFileDesc
			,string oldXML,int CustomerID, int intUserID, int flag, string strCustomInfo)				
		{
			XmlDocument		xmlDoc					=		new XmlDocument();
			xmlDoc.LoadXml(oldXML);
			StringBuilder	updateSql				=		new StringBuilder();
			// string tmp;

			updateSql.Append("UPDATE " + MNT_ATTACHMENT_LIST + " set ");

			string strOldFileDesc	= GetNodeValue(xmlDoc,"//ATTACH_FILE_DESC");
				
			Cms.DataLayer.SqlUpdateBuilder	objSqlBuilder = new Cms.DataLayer.SqlUpdateBuilder();

			//Passing the column names and its old and new value to the SQLUpdateBuilder class
			//Then this class will give us the whole update query
			objSqlBuilder.AddColumn("ATTACH_FILE_DESC",strFileDesc,strOldFileDesc,Cms.DataLayer.MSSQLType.NVarChar,false);
				
			objSqlBuilder.TableName		= MNT_ATTACHMENT_LIST;							//Table, which will be updated
			objSqlBuilder.WhereClause	= " WHERE ATTACH_ID = " + intAttachmentID ;	//Where condition used for updation

				
			//Retreiving the Update query
			string strUpdateSql	= objSqlBuilder.GetUpdateSQL();
			
			objSqlBuilder.ClearColumns();	

			int returnResult = 0;		

			if(strUpdateSql != "")
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.Text,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				try
				{
					if(TransactionLogRequired)
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;						
						objTransactionInfo.CLIENT_ID		=	CustomerID;
						objTransactionInfo.RECORDED_BY		=	intUserID;
						if(flag==1)
						{
                            objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1541", "");//"Attachment at customer assistant has been modified.";												
							objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
						}
						else
                            objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1542", ""); //"Attachment at application has been modified.";												

						returnResult	= objDataWrapper.ExecuteNonQuery(strUpdateSql,objTransactionInfo);
					}
					else
						returnResult = objDataWrapper.ExecuteNonQuery(strUpdateSql);
						
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					return returnResult;
				}
				catch(Exception ex)
				{
					throw(ex);
				}
				finally
				{
					if(xmlDoc != null) 
					{
						xmlDoc = null;
					}
					if(updateSql != null) 
					{
						updateSql = null;
					}
					if(objDataWrapper != null) 
					{
						objDataWrapper.Dispose();
					}
					if(objSqlBuilder != null) 
					{
						objSqlBuilder = null;
					}
				}
			}
			else
			{
				return 1;
			}
		}

        

		public int Update(ClsAttachmentInfo objAttachmentInfo,ClsAttachmentInfo objOldAttachmentInfo,int flag, string strCustomInfo,int intAttachType)				
		{
			string		strStoredProc	=	"Proc_UpdateAttachment";
			string strTranXML;
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

            try
            {

                objDataWrapper.AddParameter("@ATTACH_ID", objAttachmentInfo.ATTACH_ID);
                objDataWrapper.AddParameter("@ATTACH_FILE_DESC", objAttachmentInfo.ATTACH_FILE_DESC);
                objDataWrapper.AddParameter("@ATTACH_TYPE", objAttachmentInfo.ATTACH_TYPE);


                int returnResult = 0;
                if (TransactionLogRequired)
                {
                    //objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddMvrInformation.aspx.resx");
                    objAttachmentInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/maintenance/AddAttachment.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    strTranXML = objBuilder.GetTransactionLogXML(objOldAttachmentInfo, objAttachmentInfo);
                    if (strTranXML == "<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
                        returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                    else
                    {
                        Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                        objTransactionInfo.TRANS_TYPE_ID = 1;
                        objTransactionInfo.CLIENT_ID = objAttachmentInfo.ATTACH_CUSTOMER_ID;
                        objTransactionInfo.RECORDED_BY = objAttachmentInfo.CREATED_BY;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1541", "");//"Attachment at customer assistant has been modified.";
                        objTransactionInfo.CHANGE_XML = strTranXML;
                        objTransactionInfo.CUSTOM_INFO = strCustomInfo;

                        //Executing the query
                        returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                    }
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return 1;
            }
            catch (Exception ex)
            {
                throw (ex);
                //return -1;
            }
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}

			
		}
		

		public int Update(ClsAttachmentInfo objAttachmentInfo,ClsAttachmentInfo objOldAttachmentInfo,string strEntityType)				
		{
			
			string		strStoredProc	=	"Proc_UpdateAttachment";
			string strTranXML;
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				
				string entityName = "", entityCode = "";

				string []entity = strEntityType.Split('~');
				if(entity.Length > 2)
				{
					entityName = entity[1];
					entityCode = entity[2];
				}
				strEntityType = entity[0];

				objDataWrapper.AddParameter("@ATTACH_ID",objAttachmentInfo.ATTACH_ID);
				objDataWrapper.AddParameter("@ATTACH_FILE_DESC",objAttachmentInfo.ATTACH_FILE_DESC);
				objDataWrapper.AddParameter("@ATTACH_TYPE",objAttachmentInfo.ATTACH_TYPE);

				int returnResult = 0;
				if(TransactionLogRequired)
				{						
					//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddMvrInformation.aspx.resx");
					objAttachmentInfo.TransactLabel= ClsCommon.MapTransactionLabel("cmsweb/maintenance/AddAttachment.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					strTranXML = objBuilder.GetTransactionLogXML(objOldAttachmentInfo,objAttachmentInfo);
					if(strTranXML=="<LabelFieldMapping></LabelFieldMapping>" || strTranXML=="")
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{

						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
	
						objTransactionInfo.TRANS_TYPE_ID	=	173;					
						objTransactionInfo.CLIENT_ID		=	objAttachmentInfo.ATTACH_CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objAttachmentInfo.CREATED_BY;
						objTransactionInfo.APP_ID			=	objAttachmentInfo.ATTACH_APP_ID;
						objTransactionInfo.APP_VERSION_ID	=	objAttachmentInfo.ATTACH_APP_VER_ID;
                        objTransactionInfo.POLICY_ID        =objAttachmentInfo.ATTACH_POLICY_ID;
                        objTransactionInfo.POLICY_VER_TRACKING_ID = objAttachmentInfo.ATTACH_POLICY_VER_TRACKING_ID;
						objTransactionInfo.TRANS_DESC		=	"";
						objTransactionInfo.CHANGE_XML		=	strTranXML;		
/*						objTransactionInfo.CUSTOM_INFO		=	"; File Name:" + objAttachmentInfo.ATTACH_FILE_NAME +"<br>" +
																"; File Description:" + objAttachmentInfo.ATTACH_FILE_DESC + "<br>"+
																"; File Type:" + objAttachmentInfo.ATTACH_FILE_TYPE + "<br>"+
																"; Attachment Type:" + objAttachmentInfo.ATTACH_TYPE;
*/
						if (strEntityType == "Division")
						{
							objTransactionInfo.CUSTOM_INFO = "; Division Name:" + entityName +"<br>"+
								"; Division Code:" + entityCode +"<br>";
  
						}
						else if (strEntityType == "Department")
						{
							objTransactionInfo.CUSTOM_INFO = "; Department Name:" + entityName +"<br>"+
								"; Department Code:" + entityCode +"<br>";
						}
						else if (strEntityType == "ProfitCenter")
						{
							objTransactionInfo.CUSTOM_INFO = "; ProfitCenter Name:" + entityName +"<br>"+
								"; ProfitCenter Code:" + entityCode +"<br>";
						}
						else if (strEntityType == "Mortgage")
						{
							objTransactionInfo.CUSTOM_INFO = "; Additional Interest Name:" + entityName +"<br>"+
								"; Additional Interest Code:" + entityCode +"<br>";
						}
						objTransactionInfo.CUSTOM_INFO +=		//"; File Name:"         + objAttachmentInfo.ATTACH_FILE_NAME +"<br>"+
																//"; File Type:"         + objAttachmentInfo.ATTACH_FILE_TYPE +"<br>"+ 
																"; File Description: From "  + objOldAttachmentInfo.ATTACH_FILE_DESC +  "; To " + objAttachmentInfo.ATTACH_FILE_DESC +"<br>"+
																"; Attachment Type: From "   + objOldAttachmentInfo.ATTACH_TYPE + "; To " + objAttachmentInfo.ATTACH_TYPE;


						//Executing the query
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}					
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
				return 1;
			}
			catch(Exception ex)
			{
				throw(ex);
				//return -1;
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}

			
		}
		
		/// <summary>
		/// Returns a single attachment record
		/// </summary>
		/// <param name="attachmentID"></param>
		/// <returns></returns>
		public DataSet GetAttachmentByID(int attachmentID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			string	strStoredProc =	"Proc_GetAttachmentByID";

			objDataWrapper.AddParameter("@ATTACH_ID",attachmentID);

			DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;

		}

		public string GetCustomerDetails(string Customer_ID)			
		{			
			string strCustomerName,strCustomerCode,strCustomInfo="";
			DataSet dsTransDesc=new DataSet();
			dsTransDesc=DataWrapper.ExecuteDataset(ConnStr,"Proc_GetCustomerInfo",Customer_ID);			
			if(dsTransDesc.Tables.Count>0)
			{
				strCustomerName= dsTransDesc.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"].ToString() + " " + dsTransDesc.Tables[0].Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString() + " " + dsTransDesc.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"].ToString();
				strCustomerCode = dsTransDesc.Tables[0].Rows[0]["CUSTOMER_CODE"].ToString();
				strCustomInfo=";Customer Name = " + strCustomerName + ";Customer Code = " + strCustomerCode;
			}
			return strCustomInfo;
		}
	

		#region Delete functions
		/// <summary>
		/// deletes the information passed in model object to database.
		/// </summary>
		public int Delete(int intAttachID,string strCustomInfo)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
			string	strStoredProc =	"Proc_DeleteAttachment";
			objDataWrapper.AddParameter("@AttachmentId",intAttachID);
			int intResult = objDataWrapper.ExecuteNonQuery(strStoredProc);	
			return intResult;
		
		}

		public int Delete(int intAttachID,int CustomerID,int intUserID,string strCalledFrom,string strCustomInfo)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
			string	strStoredProc =	"Proc_DeleteAttachment";
			objDataWrapper.AddParameter("@AttachmentId",intAttachID);
			int intResult;// = objDataWrapper.ExecuteNonQuery(strStoredProc);	
			if(TransactionLogRequired)
			{
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	192;						
				objTransactionInfo.CLIENT_ID		=	CustomerID;
				objTransactionInfo.RECORDED_BY		=	intUserID;
				if(strCalledFrom.ToUpper()=="APPLICATION")
				{
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1543", "");// "Attachment at application is deleted.";												
				}
				else if(strCalledFrom == "InCLT")
				{
					objTransactionInfo.CUSTOM_INFO	=	strCustomInfo;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1544", "");// "Attachment at customer assistant is deleted.";												
				}	
				else if(strCalledFrom!= "")
				{
					objTransactionInfo.CUSTOM_INFO	=	strCustomInfo;
                    objTransactionInfo.TRANS_DESC =  Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1548","") + strCalledFrom.ToUpper() + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1549","");	//"Attachment at " + strCalledFrom.ToUpper()+ " is deleted.";												
				}
							

				intResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
			}
			else
				intResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

			return intResult;
		
		}

		public int Delete(ClsAttachmentInfo objAttachmentInfo,int modifiedBy, string strEntityType)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
			string	strStoredProc =	"Proc_DeleteAttachment";
			objDataWrapper.AddParameter("@AttachmentId",objAttachmentInfo.ATTACH_ID);
			int intResult;// = objDataWrapper.ExecuteNonQuery(strStoredProc);	
			if(TransactionLogRequired)
			{
				string entityName = "", entityCode = "";
              
                    string[] entity = strEntityType.Split('~');
                    if (entity.Length > 2)
                    {
                        entityName = entity[1];
                        entityCode = entity[2];
                    }
                    strEntityType = entity[0];
           
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	192;	
				objTransactionInfo.RECORDED_BY		=	modifiedBy;
                objTransactionInfo.POLICY_ID = objAttachmentInfo.ATTACH_POLICY_ID;
                objTransactionInfo.POLICY_VER_TRACKING_ID = objAttachmentInfo.ATTACH_POLICY_VER_TRACKING_ID;
				if(objAttachmentInfo.ATTACH_CUSTOMER_ID != 0)	
				{
					objTransactionInfo.CLIENT_ID		=	objAttachmentInfo.ATTACH_CUSTOMER_ID;
					//objTransactionInfo.RECORDED_BY		=	modifiedBy;
					objTransactionInfo.APP_ID			=	objAttachmentInfo.ATTACH_APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objAttachmentInfo.ATTACH_APP_VER_ID;
				}
				if(objAttachmentInfo.ATTACH_CUSTOMER_ID != 0)
				{
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1545", "");// "Attachment at application has been deleted.";
				}
				else if(strEntityType !="")
				{
					if(strEntityType=="BankInformation")
					{
						strEntityType="Bank Information";
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1548", "") + strEntityType + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1550", "");//"Attachment at " + strEntityType + " has been deleted";
					}
					else
					{
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1548", "") + strEntityType + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1550", "");	//"Attachment at " + strEntityType + " has been deleted";
					}
				}
				else
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1546", "");// "Attachment has been deleted";
		
	/*			objTransactionInfo.CUSTOM_INFO		=	"File Name:" + objAttachmentInfo.ATTACH_FILE_NAME +"<br>"+
														"File Description:" + objAttachmentInfo.ATTACH_FILE_DESC + "<br>"+
														"File Type:" + objAttachmentInfo.ATTACH_FILE_TYPE + "<br>"+
														"Attachment Type:" + objAttachmentInfo.ATTACH_TYPE;
*/
				if (strEntityType == "Division")
				{
					objTransactionInfo.CUSTOM_INFO = "; Division Name:" + entityName +"<br>"+
													 "; Division Code:" + entityCode +"<br>";
  
				}
				else if (strEntityType == "Department")
				{
					objTransactionInfo.CUSTOM_INFO = "; Department Name:" + entityName +"<br>"+
												     "; Department Code:" + entityCode +"<br>";
				}
				else if (strEntityType == "ProfitCenter")
				{
					objTransactionInfo.CUSTOM_INFO = "; ProfitCenter Name:" + entityName +"<br>"+
													 "; ProfitCenter Code:" + entityCode +"<br>";
				}
				else if (strEntityType == "Mortgage")
				{
					objTransactionInfo.CUSTOM_INFO = "; Additional Interest Name:" + entityName +"<br>"+
						"; Additional Interest Code:" + entityCode +"<br>";
				}
				else if (strEntityType == "Vendor")
				{
					objTransactionInfo.CUSTOM_INFO = "; Company Name:" + entityName +"<br>"+
						"; Vendor Code:" + entityCode +"<br>";
				}
				string strAttachType ="";
				switch(objAttachmentInfo.ATTACH_TYPE)
				{
					case 11791:
						strAttachType ="Home Photograph";
						break;
					case 11792:
						strAttachType ="Protective Device Certificate";
						break;
					case 11793:
						strAttachType ="Scheduled Articles Photograph";
						break;
					case 11794:
						strAttachType ="Appraisal";
						break;
					case 11795:
						strAttachType ="Bill of Sale";
						break;
					case 11796:
						strAttachType ="Other";
						break;
					case 11933:
						strAttachType ="Supporting Document";
						break;
				}
				objTransactionInfo.CUSTOM_INFO +=		"; File Name: "         + objAttachmentInfo.ATTACH_FILE_NAME +"<br>"+
					"; File Type: "         + objAttachmentInfo.ATTACH_FILE_TYPE +"<br>"+ 
					"; File Description: "  + objAttachmentInfo.ATTACH_FILE_DESC +"<br>"+
					"; Attachment Type: "   + strAttachType;
				intResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
			}
			else
				intResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

			return intResult;
		
		}
		#endregion

		#region ImpersonateUser
		/// <summary>
		/// Impersonate the specified user on specifed domain with specified password
		/// </summary>
		/// <param name="userName">Login Name</param>
		/// <param name="password">Password of Login</param>
		/// <param name="domainName">Domain Name</param>
		/// <returns></returns>
		public bool ImpersonateUser(String userName,  String password,String domainName)
		{
			WindowsIdentity tempWindowsIdentity;
			IntPtr token = IntPtr.Zero;
			IntPtr tokenDuplicate = IntPtr.Zero;
			bool authentication = false;

			try
			{
				//Temprary code for Block Impersonation (Use for Development)
				if(ConfigurationManager.AppSettings.Get("Impersonate") == "0")
				{
					authentication =  true;
				}
				else
				{
			
					if(LogonUser(userName, domainName, password, LOGON32_LOGON_INTERACTIVE,	LOGON32_PROVIDER_DEFAULT, ref token) != 0)
					{
						if(DuplicateToken(token, 2, ref tokenDuplicate) != 0) 
						{
							tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
							impersonationContext = tempWindowsIdentity.Impersonate();
							if (impersonationContext != null)
								authentication =  true;
							else
								authentication = false; 
						}
						else
							authentication =  false;
					} 
					else
						authentication = false;
				}
			}
			catch
			{
			}
			return authentication ;
		}
		#endregion

		#region end impersonate
		/// <summary>
		/// End the imporsonation
		/// </summary>
		public void endImpersonation()
		{
			try
			{
				if (impersonationContext !=null) impersonationContext.Undo();
			}
			catch (Exception ex)
			{
				System.Diagnostics.EventLog.WriteEntry("EbixASP WebMerge 3.0","Impersionation Error; Message:-" + ex.Message);	
			}
		} 
		#endregion
        //Added by Pradeep Kushwaha on 28 June 2011
        /// <summary>
        /// Use to check file exist or not on virtual folder with Impersonation
        /// </summary>
        /// <param name="urlPath"></param>
        /// <returns></returns>
        public bool IsFileExists(string urlPath)
        {
            string filePath;
            string WebURl = CmsWebUrl.ToUpper();

            filePath = WebURl.Replace("CMS/CMSWEB/", "") + @"/" + urlPath;

            try
            {
                System.Net.WebRequest objWebReq = System.Net.WebRequest.Create(filePath);

                objWebReq.Credentials = new System.Net.NetworkCredential(ClsCommon.ImpersonationUserId, ClsCommon.ImpersonationPassword, ClsCommon.ImpersonationDomain);
                objWebReq.Method = "HEAD";
                System.Net.WebResponse wresp = objWebReq.GetResponse();
                if (wresp is System.Net.HttpWebResponse)
                {
                    System.Net.HttpWebResponse hwresp = (System.Net.HttpWebResponse)wresp;
                    if (hwresp.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (System.Net.WebException wex)
            {
                if (wex.Response == null)
                {
                    return false;
                }
                else
                {
                    if (wex.Response is System.Net.HttpWebResponse)
                    {
                        System.Net.HttpWebResponse hwresp =
                            (System.Net.HttpWebResponse)wex.Response;
                        if (hwresp.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            throw new SystemException("File not found.");
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

            }

        }

	}
}
