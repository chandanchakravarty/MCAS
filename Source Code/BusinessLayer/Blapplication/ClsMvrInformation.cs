/******************************************************************************************
<Author				: -   Sumit Chhabra
<Start Date				: -	9/27/2005 9:52:33 AM
<End Date				: -	
<Description				: - 	s
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 13-10-2005
<Modified By				: - Vijay Arora
<Purpose				: - Applying Null value to amount

<Modified Date            :  8-11-2005
<Modified By              :  Mohit Gupta
<Purpose                  : Adding function GetDriverDOB for finding driver date of birth.

<Modified Date            :  25-11-2005
<Modified By              :  Vijay Arora
<Purpose                  :  Added the Policy WaterCraft MVR Functions.

<Modified Date            :  09 Feb. 2006
<Modified By              :  Ashwani  Marked as <001>,<002>
<Purpose                  :  Added CalledFrom param in Delete function.
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;				
using Cms.Model.Application;
using Cms.Model.Policy ;
namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// BL for MvrInformation
	/// </summary>
	public class ClsMvrInformation : Cms.BusinessLayer.BlApplication.clsapplication,IDisposable
	{
		private const	string		APP_MVR_INFORMATION			=	"APP_MVR_INFORMATION";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		//private int _APP_MVR_ID;
		private string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateAPP_MVR_INFORMATION";
		
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
		public string Activate_Deactivate_Proc
		{
			set
			{
				ACTIVATE_DEACTIVATE_PROC= value;
				base.strActivateDeactivateProcedure=ACTIVATE_DEACTIVATE_PROC;
			}
			get
			{
				return ACTIVATE_DEACTIVATE_PROC;
			}
		}
		#endregion
		
		
		

		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsMvrInformation()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		/// <summary>
		/// getting MNT_VIOLATIONS id for state lob and type 
		/// </summary>
		/// <param name="dtTemp"></param>
		/// <param name="modified_By">Mohit Agarwal</param>


		public DataSet GetMNTViolationInfo(int lob_id, int state_id, string viol_type)
		{
			
			try
			{
				DataSet dsViol = new DataSet();
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@LOB_ID",lob_id);
				objDataWrapper.AddParameter("@STATE_ID",state_id);
				objDataWrapper.AddParameter("@VIOL_TYPE",viol_type);
				dsViol = objDataWrapper.ExecuteDataSet("Proc_GetMNTViolationIIX");
				return dsViol;			 
			}
			catch
			{
				//throw (exc);
			}
			finally
			{}
			return null;
		}



		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objMvrInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsMvrInfo objMvrInfo,string strCalledFrom)
		{
			string		strStoredProc	=	"Proc_InsertAPP_MVR_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objMvrInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objMvrInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VIOLATION_ID",objMvrInfo.VIOLATION_ID);
				objDataWrapper.AddParameter("@VIOLATION_TYPE",objMvrInfo.VIOLATION_TYPE);				
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@MVR_AMOUNT",System.DBNull.Value);
				objDataWrapper.AddParameter("@MVR_DEATH",System.DBNull.Value);
				if(objMvrInfo.MVR_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@MVR_DATE",objMvrInfo.MVR_DATE);
				else
					objDataWrapper.AddParameter("@MVR_DATE",System.DBNull.Value);
				if(objMvrInfo.OCCURENCE_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@OCCURENCE_DATE",objMvrInfo.OCCURENCE_DATE);
				else
					objDataWrapper.AddParameter("@OCCURENCE_DATE",System.DBNull.Value);
				if(objMvrInfo.DETAILS != "")
					objDataWrapper.AddParameter("@DETAILS",objMvrInfo.DETAILS);
				else
					objDataWrapper.AddParameter("@DETAILS",System.DBNull.Value);

				if(objMvrInfo.POINTS_ASSIGNED < 100)
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",objMvrInfo.POINTS_ASSIGNED);
				else
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",System.DBNull.Value);

				if(objMvrInfo.ADJUST_VIOLATION_POINTS < 100)
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",objMvrInfo.ADJUST_VIOLATION_POINTS);
				else
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",System.DBNull.Value);

				objDataWrapper.AddParameter("@IS_ACTIVE","Y");
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom);
				objDataWrapper.AddParameter("@VERIFIED",objMvrInfo.VERIFIED);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@APP_MVR_ID",objMvrInfo.APP_MVR_ID,SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlParameter1  = (SqlParameter) objDataWrapper.AddParameter("@RET_VIOLATION_TYPE",null,SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlParameter2  = (SqlParameter) objDataWrapper.AddParameter("@RET_VERIFIED",null,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				int APP_MVR_ID = int.Parse(objSqlParameter.Value.ToString());
				int tmpVIOLATION_TYPE = int.Parse(objSqlParameter1.Value.ToString());
				int tmpVERIFIED = int.Parse(objSqlParameter2.Value.ToString());

				objMvrInfo.APP_MVR_ID =APP_MVR_ID; 
				objMvrInfo.VIOLATION_TYPE = tmpVIOLATION_TYPE;
				objMvrInfo.VERIFIED = tmpVERIFIED;
				if(TransactionLogRequired)
				{	
					//Sep 27,2005:Sumit:Following TransactLabel statement has been modified to point to correct address
					//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");					
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddMvrInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objMvrInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objMvrInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objMvrInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"MVR Information Record Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;		
					
					//Executing the query
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				//				else
				//				{
				//					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				//				}
				//				int APP_MVR_ID = int.Parse(objSqlParameter.Value.ToString());
				//				int tmpVIOLATION_TYPE = int.Parse(objSqlParameter1.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (APP_MVR_ID == -1)
				{
					return -1;
				}
				else
				{
					objMvrInfo.APP_MVR_ID = APP_MVR_ID;
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


		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objMvrInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsPolicyAutoMVR objMvrInfo)
		{
			string		strStoredProc	=	"Proc_InsertPOL_MVR_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objMvrInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objMvrInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@VIOLATION_ID",objMvrInfo.VIOLATION_ID);
				objDataWrapper.AddParameter("@VIOLATION_TYPE",objMvrInfo.VIOLATION_TYPE);				
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@MVR_AMOUNT",System.DBNull.Value);
				objDataWrapper.AddParameter("@MVR_DEATH",System.DBNull.Value);
				if(objMvrInfo.MVR_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@MVR_DATE",objMvrInfo.MVR_DATE);
				else
					objDataWrapper.AddParameter("@MVR_DATE",System.DBNull.Value);
				if(objMvrInfo.OCCURENCE_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@OCCURENCE_DATE",objMvrInfo.OCCURENCE_DATE);
				else
					objDataWrapper.AddParameter("@OCCURENCE_DATE",System.DBNull.Value);
				if(objMvrInfo.DETAILS != "")
					objDataWrapper.AddParameter("@DETAILS",objMvrInfo.DETAILS);
				else
					objDataWrapper.AddParameter("@DETAILS",System.DBNull.Value);

				if(objMvrInfo.POINTS_ASSIGNED < 100)
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",objMvrInfo.POINTS_ASSIGNED);
				else
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",System.DBNull.Value);

				if(objMvrInfo.ADJUST_VIOLATION_POINTS < 100)
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",objMvrInfo.ADJUST_VIOLATION_POINTS);
				else
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",System.DBNull.Value);

				objDataWrapper.AddParameter("@IS_ACTIVE","Y");
				objDataWrapper.AddParameter("@VERIFIED",objMvrInfo.VERIFIED);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@APP_MVR_ID",objMvrInfo.APP_MVR_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					//Sep 27,2005:Sumit:Following TransactLabel statement has been modified to point to correct address
					//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");					
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddMvrInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objMvrInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID = objMvrInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objMvrInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Policy MVR Information Record Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;		
					
					
					
					
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int APP_MVR_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();				
				if (APP_MVR_ID == -1)
				{
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
					return -1;
				}
				else
				{
					objMvrInfo.APP_MVR_ID = APP_MVR_ID;
					ClsDriverDetail objDriverDetail = new ClsDriverDetail();					
					objDriverDetail.UpdateVehicleClassPolNew(objMvrInfo.CUSTOMER_ID,objMvrInfo.POLICY_ID,objMvrInfo.POLICY_VERSION_ID,objDataWrapper);
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					return returnResult;
				}
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}
		public int Add(ClsPolicyAutoMVR objMvrInfo, string strCalledFrom)
		{
			return Add(objMvrInfo, strCalledFrom,"",null);
		}

		public int Add(ClsPolicyAutoMVR objMvrInfo, string strCalledFrom,string strCalledFor,DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_InsertPOL_MVR_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			if(objDataWrapper==null)
			 objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objMvrInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objMvrInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@VIOLATION_ID",objMvrInfo.VIOLATION_ID);
				objDataWrapper.AddParameter("@VIOLATION_TYPE",objMvrInfo.VIOLATION_TYPE);				
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@MVR_AMOUNT",System.DBNull.Value);
				objDataWrapper.AddParameter("@MVR_DEATH",System.DBNull.Value);
				if(objMvrInfo.MVR_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@MVR_DATE",objMvrInfo.MVR_DATE);
				else
					objDataWrapper.AddParameter("@MVR_DATE",System.DBNull.Value);
				if(objMvrInfo.OCCURENCE_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@OCCURENCE_DATE",objMvrInfo.OCCURENCE_DATE);
				else
					objDataWrapper.AddParameter("@OCCURENCE_DATE",System.DBNull.Value);
				if(objMvrInfo.DETAILS != "")
					objDataWrapper.AddParameter("@DETAILS",objMvrInfo.DETAILS);
				else
					objDataWrapper.AddParameter("@DETAILS",System.DBNull.Value);

				if(objMvrInfo.POINTS_ASSIGNED < 100)
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",objMvrInfo.POINTS_ASSIGNED);
				else
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",System.DBNull.Value);

				if(objMvrInfo.ADJUST_VIOLATION_POINTS < 100)
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",objMvrInfo.ADJUST_VIOLATION_POINTS);
				else
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",System.DBNull.Value);

				objDataWrapper.AddParameter("@IS_ACTIVE","Y");
				objDataWrapper.AddParameter("@VERIFIED",objMvrInfo.VERIFIED);
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom);					 
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@APP_MVR_ID",objMvrInfo.APP_MVR_ID,SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlParameter1  = (SqlParameter) objDataWrapper.AddParameter("@RET_VIOLATION_TYPE",null,SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlParameter2  = (SqlParameter) objDataWrapper.AddParameter("@RET_VERIFIED",null,SqlDbType.Int,ParameterDirection.Output);


				int returnResult = 0;
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);

				int APP_MVR_ID = int.Parse(objSqlParameter.Value.ToString());
				int tmpVIOLATION_TYPE = int.Parse(objSqlParameter1.Value.ToString());
				int tmpVERIFIED = int.Parse(objSqlParameter2.Value.ToString());


				objMvrInfo.APP_MVR_ID =APP_MVR_ID; 
				objMvrInfo.VIOLATION_TYPE = tmpVIOLATION_TYPE;
				objMvrInfo.VERIFIED = tmpVERIFIED;
				if(tmpVERIFIED>0)
				{
					if(TransactionLogRequired)
					{	
						//Sep 27,2005:Sumit:Following TransactLabel statement has been modified to point to correct address
						//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");					
                        objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Automobile/PolicyAutoMVR.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						string strTranXML = objBuilder.GetTransactionLogXML(objMvrInfo);
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.POLICY_ID = objMvrInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID = objMvrInfo.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Policy MVR Information Record has been Added";
						objTransactionInfo.CHANGE_XML		=	strTranXML;		
						//Executing the query
						objDataWrapper.ExecuteNonQuery(objTransactionInfo);
					}
				
					//				else
					//				{
					//					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					//				}
					//				int APP_MVR_ID = int.Parse(objSqlParameter.Value.ToString());
				}
				if (APP_MVR_ID == -1)
				{
					if (strCalledFor!="PROCESS")
						objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
					return -1;
				}
				else
				{
					objMvrInfo.APP_MVR_ID = APP_MVR_ID;
					objDataWrapper.ClearParameteres();
					//Update the Vehicle Class : 
					ClsDriverDetail objDriverDetail = new ClsDriverDetail();
					if(strCalledFrom.ToUpper() != "MOT") //Class will be updated only in case of AUTO
					{
						objDriverDetail.UpdateVehicleClassPolNew(objMvrInfo.CUSTOMER_ID,objMvrInfo.POLICY_ID,objMvrInfo.POLICY_VERSION_ID,objDataWrapper);
					}
					//End Update the Vehicle Class
					if (strCalledFor!="PROCESS")
						objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					//return returnResult;
					return tmpVERIFIED;
				}
			}
			catch(Exception ex)
			{
				if (strCalledFor!="PROCESS")
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			finally
			{
				//if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}
		#endregion
		#region setting violation

		public int SetDriverViolations(int CustomerId,int Policyid, int PolicyVersionid,string strLobId,int userID,string strStateID,string strDriverid,string strDriverName,XmlDocument objDriverResponse,string strCalledFor,DataWrapper objDataWrapper)
		{
			int nCount =0; 
			DataSet objDSDriverViolDetail;
			XmlNode objNode= null;
			string strXmlQuery="";
			string mvr_status = "0";
			string mvr_remarks = "";
			string MVR_LICENCE_CLASS = "";
			string MVR_DRIVER_LIC_APP = "";
			int NO_OF_UDI = 0,NO_OF_EXC_MVR=0;
			System.Collections.Hashtable htNOMVR=new System.Collections.Hashtable();
			Cms.BusinessLayer.BlApplication.ClsGeneralInformation  objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
			
			int IntCUSTOMER_ID			=CustomerId;
			int IntPOLICY_ID 			=Policyid;
			int IntPOLICY_VERSION_ID	=PolicyVersionid;	
			int IntLobID				=int.Parse(strLobId);
			
			string trans_desc = "MVR Ordered on {"+DateTime.Now.ToString("MM/dd/yyyy")+"} for";
			string trans_custom = "";
			trans_custom += ";" + strDriverName  + ";";
			//trans_custom += " - ";
			// for each driver or operator getting a list of violation from iix web service
				string fetch_desc = "";
				string fetch_custom = "";
				NO_OF_UDI = 0;
			try
			{
				if (objDriverResponse.DocumentElement.SelectNodes("Violation").Count==0)
				{
					if (objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Count>0)
					{
						if(objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(0).Attributes["MVR_LICENCE_CLASS"].InnerText !="") 
							MVR_LICENCE_CLASS=objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(0).Attributes["MVR_LICENCE_CLASS"].InnerText;
						if(objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(0).Attributes["MVR_DRIVER_LIC_APP"].InnerText !="") 
							MVR_DRIVER_LIC_APP =objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(0).Attributes["MVR_DRIVER_LIC_APP"].InnerText;
						if(objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(0).Attributes["MVR_REMARKS"].InnerText !="") 
							mvr_remarks = mvr_remarks + " " + objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(0).Attributes["MVR_REMARKS"].InnerText;
						mvr_status  =objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(0).Attributes["MVR_STATUS"].InnerText;
						nCount=objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Count;
					}
					if(mvr_status == "1" || mvr_status == "2" || mvr_status == "N") //"1" or "2" indicates a NOT FOUND
					{
						fetch_desc = "No MVR Driver Found.";
						fetch_custom = ";0 MVRs found for driver :" + strDriverName;
						htNOMVR.Add(IntCUSTOMER_ID.ToString() + "-" + IntPOLICY_ID.ToString() + "-" + IntPOLICY_VERSION_ID + "-" + strDriverid, strDriverName + " : " + "No MVR Driver Found."); 
					}
					else
					{
						fetch_desc = "MVR Information Updated.";
						fetch_custom = ";MVR Information Updated for driver :" + strDriverName;
						htNOMVR.Add(IntCUSTOMER_ID.ToString() + "-" + IntPOLICY_ID.ToString() + "-" + IntPOLICY_VERSION_ID + "-" + strDriverid,strDriverName + " : " + "MVR Information Updated."); 
					}

					
				}
				if (objDriverResponse.DocumentElement.SelectNodes("Violation").Count >0)
				{
					nCount=objDriverResponse.DocumentElement.SelectNodes("Violation").Count;
					// if violations come from iix web service 
					int nViolationRec= objDriverResponse.DocumentElement.ChildNodes.Count; 
					string strViolationCode="";
					int nNode;
					string recordNum="";
					int mvr_found = 0;
					// getting all violation code 
					for (nNode =0; nNode<= nViolationRec - 1; nNode++)
					{
						XmlNode ViolNode=objDriverResponse.DocumentElement.SelectNodes("Violation").Item(nNode);
						if (ViolNode==null) continue;
						if((recordNum = objDriverResponse.DocumentElement.SelectNodes("Violation").Item(nNode).Attributes["viol_record"].InnerText) != "5")
						{
							if(recordNum == "1")
								mvr_status = objDriverResponse.DocumentElement.SelectNodes("Violation").Item(nNode).Attributes["viol_status"].InnerText;
							if(mvr_status == "1" || mvr_status == "2")
								mvr_status = "N";
							if(recordNum == "2")
								mvr_remarks = objDriverResponse.DocumentElement.SelectNodes("Violation").Item(nNode).Attributes["viol_remarks"].InnerText;
							continue;
						}
						if(nNode==0)
						{
							XmlNode ViolMVRNode=objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(nNode);
							if (ViolMVRNode!=null)
							{
								if(objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(nNode).Attributes["MVR_LICENCE_CLASS"].InnerText !="") 
									MVR_LICENCE_CLASS=objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(nNode).Attributes["MVR_LICENCE_CLASS"].InnerText;
								if(objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(nNode).Attributes["MVR_DRIVER_LIC_APP"].InnerText !="") 
									MVR_DRIVER_LIC_APP =objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(nNode).Attributes["MVR_DRIVER_LIC_APP"].InnerText;
								if(objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(nNode).Attributes["MVR_REMARKS"].InnerText !="") 
									mvr_remarks = mvr_remarks + " " + objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(nNode).Attributes["MVR_REMARKS"].InnerText;
								mvr_status  =objDriverResponse.DocumentElement.SelectNodes("MVRINFO").Item(nNode).Attributes["MVR_STATUS"].InnerText;
							}
		
						}
						mvr_found = 1;
						if (objDriverResponse.DocumentElement.SelectNodes("Violation").Item(nNode).Attributes["AViolation_code"].InnerText !="")
						{
							if (strViolationCode =="")
							{
								strViolationCode ="'"+objDriverResponse.DocumentElement.SelectNodes("Violation").Item(nNode).Attributes["AViolation_code"].InnerText +"'"; 
							}
							else
							{
								strViolationCode =strViolationCode +",'"+ objDriverResponse.DocumentElement.SelectNodes("Violation").Item(nNode).Attributes["AViolation_code"].InnerText +"'"; 
							}
						}
					}
					if(mvr_found == 0)
					{
						if(mvr_status == "1" || mvr_status == "2" || mvr_status == "N") //"1" or "2" indicates a NOT FOUND if(mvr_status != "C")
						{
							fetch_desc = "No MVR Driver Found.";
							fetch_custom = ";0 MVRs found for driver :" + strDriverName;
							htNOMVR.Add(IntCUSTOMER_ID.ToString() + "-" + IntPOLICY_ID.ToString() + "-" + IntPOLICY_VERSION_ID + "-" + strDriverid,strDriverName + " : " + "No MVR Driver Found."); 
						}
						else
						{
							fetch_desc = "MVR Information Updated.";
							fetch_custom = ";MVR Information Updated for driver :" + (strDriverName);
							htNOMVR.Add(IntCUSTOMER_ID.ToString() + "-" + IntPOLICY_ID.ToString() + "-" + IntPOLICY_VERSION_ID + "-" + strDriverid,strDriverName + " : " + "MVR Information Updated."); 
						}
					}
					else
					{
						if(mvr_status != "" && mvr_status != " " && mvr_status != "V")
						{
							fetch_desc = "MVR Report available and imported successfully.";
							htNOMVR.Add(IntCUSTOMER_ID.ToString() + "-" + IntPOLICY_ID.ToString() + "-" + IntPOLICY_VERSION_ID + "-" + strDriverid,strDriverName + " : " + "MVR Report available and imported successfully."); 
						}
						else
						{
							fetch_desc = "MVR Information Updated.";
							htNOMVR.Add(IntCUSTOMER_ID.ToString() + "-" + IntPOLICY_ID.ToString() + "-" + IntPOLICY_VERSION_ID + "-" + strDriverid,strDriverName + " : " + "MVR Information Updated."); 
						}
					}

					if(strViolationCode=="")
					{
						strViolationCode="''";
					}

					// mapp all violation code with wolverine violation code and get details
					if(strLobId == "1")
						objDSDriverViolDetail= objGenInfo.GetViolationRecords(strViolationCode,"4",strStateID);
					else
						objDSDriverViolDetail= objGenInfo.GetViolationRecords(strViolationCode,strLobId,strStateID);					
		
					// creating a mver object to store all info retrived 
					ClsPolicyAutoMVR objMVRInfo = new ClsPolicyAutoMVR();

					objMVRInfo.POLICY_ID = IntPOLICY_ID;
					objMVRInfo.CUSTOMER_ID=IntCUSTOMER_ID;
					objMVRInfo.POLICY_VERSION_ID=IntPOLICY_VERSION_ID;
					objMVRInfo.CREATED_BY= objMVRInfo.MODIFIED_BY=userID; 
					objMVRInfo.CREATED_DATETIME = DateTime.Now;  

					string[] objArrCode = strViolationCode.Split(','); 
					bool bIsExist; 
					string strDescription="";
					string strVcode =""; 
					string strVtype =""; 
					string strVpoints =""; 
					// checking which iix violations mapped with wolverine violation 
					for ( nNode=0; nNode<= objArrCode.Length  - 1; nNode++)
					{
						strDescription="";
						strVcode =""; 
						bIsExist= false; 
						if (objDSDriverViolDetail != null)
						{
							if(objDSDriverViolDetail.Tables[0]!= null)
							{
								for (int nRecord =0; nRecord <=objDSDriverViolDetail.Tables[0].Rows.Count -1 ;nRecord ++)
								{
									string strSSVCode= objDSDriverViolDetail.Tables[0].Rows[nRecord]["SSV_CODE"].ToString().Trim();
									string strVCode = objArrCode[nNode].Replace("'","").Trim()   ;
									if( strSSVCode == strVCode  )
									{
										bIsExist= true;
										break;
									}
								}
							}
						}
						// if iix violation not mapped then put this entry in exception table for future use 
						if (bIsExist== false)
						{
							if(objArrCode[nNode].ToString() != "''")
							{
								strXmlQuery="/ResultData/Violation[@AViolation_code="+objArrCode[nNode].ToString().Trim()+"]";
								objNode=objDriverResponse.DocumentElement.SelectSingleNode(strXmlQuery); 
								if (objNode==null) continue;
								strDescription=objNode.Attributes["viol_description"].InnerText.Trim()   ; 
								strVcode =objNode.Attributes["AViolation_code"].InnerText.Trim();
								strVtype =objNode.Attributes["viol_type"].InnerText.Trim();  
								strVpoints =objNode.Attributes["APoints"].InnerText.Trim();
								string strMVRDate =objNode.Attributes["viol_date"].InnerText.Trim();
								string strMVRConvectionDate =objNode.Attributes["conviction_date"].InnerText.Trim(); 
								if (strMVRDate!="")
								{
									strMVRDate=strMVRDate.Substring(0,2).ToString()  + "/" +  strMVRDate.Substring(2,2) + "/" + strMVRDate.Substring(4,4);
									//objMVRInfo.MVR_DATE= Convert.ToDateTime(strMVRDate);
									objMVRInfo.OCCURENCE_DATE= Convert.ToDateTime(strMVRDate);
								}
								if (strMVRConvectionDate!="")
								{
									strMVRConvectionDate=strMVRConvectionDate.Substring(0,2).ToString()  + "/" +  strMVRConvectionDate.Substring(2,2) + "/" + strMVRConvectionDate.Substring(4,4);
									objMVRInfo.MVR_DATE  = Convert.ToDateTime(strMVRConvectionDate);
								}		
								objMVRInfo.DRIVER_ID =int.Parse(strDriverid) ;
								int resultID=objGenInfo.InsertUnmatchedMVRViolationDetail(objMVRInfo,strVcode,strDescription,Policyid.ToString(),PolicyVersionid.ToString(),false);
								if (resultID==-1) continue;
								NO_OF_EXC_MVR=NO_OF_EXC_MVR+1;
								//								objMVRInfo.MVR_DEATH ="N"; 
								//								objMVRInfo.MVR_AMOUNT=0; 
								objMVRInfo.VIOLATION_ID =resultID;
								objMVRInfo.IS_ACTIVE ="Y";
								if (objDSDriverViolDetail.Tables[0].Rows[0]["VIOLATION_ID"]!=System.DBNull.Value)   
									objMVRInfo.VIOLATION_TYPE = int.Parse(objDSDriverViolDetail.Tables[0].Rows[0]["VIOLATION_ID"].ToString().Trim());

								Cms.BusinessLayer.BlApplication.ClsMvrInformation objMvr = new Cms.BusinessLayer.BlApplication.ClsMvrInformation();
								try
								{
									if(strVtype.StartsWith("ADMI") || strVtype.StartsWith("INFO") ||  strVtype.StartsWith("ACCI") || strVtype.StartsWith("REIN") || strVtype.StartsWith("REVO") || strVtype.StartsWith("CANC") || strVtype.StartsWith("DISQ") || strVtype.StartsWith("DENI") || strVtype.StartsWith("UNKN") || strVtype.StartsWith("GISM")) // ||strVtype.StartsWith("SUSP") || (objDSDriverViolDetail.Tables[0].Rows[nRecord]["VIOLATION_ID"]== null))
									{
										DataSet dsViol = objMvr.GetMNTViolationInfo(int.Parse(strLobId),int.Parse(strStateID),strVtype);
										if(dsViol != null && dsViol.Tables[0].Rows.Count > 0)
										{
											objMVRInfo.VIOLATION_TYPE = int.Parse(dsViol.Tables[0].Rows[0]["VIOLATION_ID"].ToString());
											objMVRInfo.VIOLATION_ID =0;
										}

										objMVRInfo.VERIFIED = 1;
										objMVRInfo.DETAILS = strDescription;
									}
									else
									{
										objMVRInfo.VIOLATION_TYPE = 0;
										objMVRInfo.VERIFIED = 1;
										objMVRInfo.DETAILS = strDescription;
									}
									objMVRInfo.ADJUST_VIOLATION_POINTS = 100;
									if (strVpoints=="") strVpoints="0";
									objMVRInfo.POINTS_ASSIGNED = int.Parse(strVpoints);
								}
								catch
								{
								}
								// if watercraft 
								if (strLobId == "4")
								{
									objMvr.AddForWater(objMVRInfo);
									NO_OF_UDI=NO_OF_UDI +1;
								}
									//if ppa or motercycle
								else if (strLobId == "2" )
								{
									objMvr.Add(objMVRInfo, "PPA",strCalledFor,objDataWrapper);
									NO_OF_UDI=NO_OF_UDI +1;
								}
								else if ( strLobId == "3")
								{
									objMvr.Add(objMVRInfo, "MOT",strCalledFor,objDataWrapper);
									NO_OF_UDI=NO_OF_UDI +1;
								}
								fetch_custom = ";" + NO_OF_UDI.ToString() + " MVRs found for driver :" + (strDriverName);
								////
							}
						}

					}
					// now insert all mapped violations and its details 
					if (objDSDriverViolDetail != null)
					{
						if (objDSDriverViolDetail.Tables[0] != null)
						{
							int ViolCount=objDriverResponse.DocumentElement.SelectNodes("Violation").Count;
							for (int nRecord =0; nRecord <= ViolCount -1 ;nRecord ++)
							{
								XmlNode ViolNode=objDriverResponse.DocumentElement.SelectNodes("Violation").Item(nRecord);
								if (ViolNode==null) continue;
								objNode=ViolNode;
								strDescription=objNode.Attributes["viol_description"].InnerText.Trim()   ; 
								strVcode =objNode.Attributes["AViolation_code"].InnerText.Trim();
								strVtype =objNode.Attributes["viol_type"].InnerText.Trim();  
								strVpoints =objNode.Attributes["APoints"].InnerText.Trim();
								if (strVcode=="") continue;
								objMVRInfo.DRIVER_ID=  int.Parse (strDriverid); 
								if (objNode.Attributes["viol_date"].InnerText !="")
								{
									string date = objNode.Attributes["viol_date"].InnerText ; 
									objMVRInfo.MVR_DATE			= new DateTime(int.Parse( date.Substring(4,4)),int.Parse(date.Substring(0,2)),int.Parse(date.Substring(2,2))) ; //defaulting conviction date to occurance date
									objMVRInfo.OCCURENCE_DATE	= new DateTime(int.Parse( date.Substring(4,4)),int.Parse(date.Substring(0,2)),int.Parse(date.Substring(2,2))) ;
								}
								if (objNode.Attributes["conviction_date"].InnerText !="")
								{
									string Convictiondate = objNode.Attributes["conviction_date"].InnerText ; 
									objMVRInfo.MVR_DATE= new DateTime(int.Parse( Convictiondate.Substring(4,4)),int.Parse(Convictiondate.Substring(0,2)),int.Parse(Convictiondate.Substring(2,2))) ;
								}
								//								objMVRInfo.MVR_DEATH ="N"; 
								//								objMVRInfo.MVR_AMOUNT=0; 
								//objMVRInfo.VIOLATION_ID =int.Parse ( objDSDriverViolDetail.Tables[0].Rows[nRecord]["VIOLATION_ID"]== null?"":objDSDriverViolDetail.Tables[0].Rows[nRecord]["VIOLATION_ID"].ToString()) ;
								//objMVRInfo.IS_ACTIVE =objDSDriverViolDetail.Tables[0].Rows[nRecord]["IS_ACTIVE"]== null?"":objDSDriverViolDetail.Tables[0].Rows[nRecord]["IS_ACTIVE"].ToString() ;
								DataRow[] dr= objDSDriverViolDetail.Tables[0].Select("SSV_CODE='" + strVcode + "'");
								if (dr.Length>0)
								{
									objMVRInfo.VIOLATION_ID =int.Parse (dr[0]["VIOLATION_ID"]==null?"0":dr[0]["VIOLATION_ID"].ToString());
									objMVRInfo.IS_ACTIVE =dr[0]["IS_ACTIVE"]== null?"":dr[0]["IS_ACTIVE"].ToString() ;
									strVpoints=dr[0]["MVR_POINTS"]==null?"0":dr[0]["MVR_POINTS"].ToString();
								}
								else
								{
									objMVRInfo.VIOLATION_ID =0;
									objMVRInfo.IS_ACTIVE="";
								}
								Cms.BusinessLayer.BlApplication.ClsMvrInformation objMvr = new Cms.BusinessLayer.BlApplication.ClsMvrInformation();

								try
								{
									if(strVtype.StartsWith("ADMI") || strVtype.StartsWith("INFO") || strVtype.StartsWith("ACCI") || strVtype.StartsWith("REIN") || strVtype.StartsWith("REVO") || strVtype.StartsWith("CANC") || strVtype.StartsWith("DISQ") || strVtype.StartsWith("DENI") || strVtype.StartsWith("UNKN") || strVtype.StartsWith("GISM")) // || strVtype.StartsWith("SUSP") ||(objDSDriverViolDetail.Tables[0].Rows[nRecord]["VIOLATION_ID"]== null))
									{
										DataSet dsViol = objMvr.GetMNTViolationInfo(int.Parse(strLobId),int.Parse(strStateID),strVtype);
										if(dsViol != null && dsViol.Tables[0].Rows.Count > 0)
										{
											objMVRInfo.VIOLATION_TYPE = int.Parse(dsViol.Tables[0].Rows[0]["VIOLATION_ID"].ToString());
											objMVRInfo.VIOLATION_ID =0;
										}

										objMVRInfo.VERIFIED = 1;
										objMVRInfo.DETAILS = strDescription;
									}
									else
									{
										objMVRInfo.VIOLATION_TYPE = 0;
										objMVRInfo.VERIFIED = 1;
										objMVRInfo.DETAILS = strDescription;
									}
									objMVRInfo.ADJUST_VIOLATION_POINTS = 100;
									if (strVpoints=="") strVpoints="0";
									objMVRInfo.POINTS_ASSIGNED = int.Parse(strVpoints);
								}
								catch(Exception ex)
								{
									throw(ex);
								}

								// if watercraft 
								if (strLobId == "4")
								{
									objMvr.AddForWater(objMVRInfo);
									NO_OF_UDI=NO_OF_UDI +1;
								}
									//if ppa or motercycle

								else if (strLobId == "2" )
								{
									objMvr.Add(objMVRInfo, "PPA",strCalledFor,objDataWrapper);
									NO_OF_UDI=NO_OF_UDI +1; 
								}
								else if ( strLobId == "3")
								{
									objMvr.Add(objMVRInfo, "MOT",strCalledFor,objDataWrapper);
									NO_OF_UDI=NO_OF_UDI +1;
														
								}
								fetch_custom = ";" + NO_OF_UDI.ToString() + " MVRs found for driver :" + strDriverName;
						
							}
						}
					}

				}
				try
				{
					objGenInfo.WriteTransactionLog(IntCUSTOMER_ID, IntPOLICY_ID, IntPOLICY_VERSION_ID, fetch_desc, userID,fetch_custom, "Policy",objDataWrapper);
					objGenInfo.SetMvrOrdered(IntCUSTOMER_ID,IntPOLICY_ID,IntPOLICY_VERSION_ID,int.Parse(strDriverid),strLobId, mvr_remarks, mvr_status, "POLICY", MVR_LICENCE_CLASS ,MVR_DRIVER_LIC_APP,objDataWrapper);
				}
				catch(Exception ex)
				{ throw(ex);}
			
				if(trans_custom != "")
					objGenInfo.WriteTransactionLog(IntCUSTOMER_ID, IntPOLICY_ID, IntPOLICY_VERSION_ID, trans_desc, userID,trans_custom, "Policy",objDataWrapper);
			return nCount;
			}
			catch(Exception ex)
			{throw(ex);}

		}

		#endregion
		#region Add(Insert) Overloaded functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objMvrInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsMvrInfo objMvrInfo,string strCalledFrom,string strDriverName, string strDriverCode)
		{
			string		strStoredProc	=	"Proc_InsertAPP_MVR_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objMvrInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objMvrInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VIOLATION_ID",objMvrInfo.VIOLATION_ID);
				objDataWrapper.AddParameter("@VIOLATION_TYPE",objMvrInfo.VIOLATION_TYPE);				
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@MVR_AMOUNT",System.DBNull.Value);
				objDataWrapper.AddParameter("@MVR_DEATH",System.DBNull.Value);
				if(objMvrInfo.MVR_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@MVR_DATE",objMvrInfo.MVR_DATE);
				else
					objDataWrapper.AddParameter("@MVR_DATE",System.DBNull.Value);
				if(objMvrInfo.OCCURENCE_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@OCCURENCE_DATE",objMvrInfo.OCCURENCE_DATE);
				else
					objDataWrapper.AddParameter("@OCCURENCE_DATE",System.DBNull.Value);
				if(objMvrInfo.DETAILS != "")
					objDataWrapper.AddParameter("@DETAILS",objMvrInfo.DETAILS);
				else
					objDataWrapper.AddParameter("@DETAILS",System.DBNull.Value);

				if(objMvrInfo.POINTS_ASSIGNED < 100)
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",objMvrInfo.POINTS_ASSIGNED);
				else
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",System.DBNull.Value);

				if(objMvrInfo.ADJUST_VIOLATION_POINTS < 100)
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",objMvrInfo.ADJUST_VIOLATION_POINTS);
				else
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",System.DBNull.Value);

				objDataWrapper.AddParameter("@IS_ACTIVE","Y");
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom);
				objDataWrapper.AddParameter("@VERIFIED",objMvrInfo.VERIFIED);				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@APP_MVR_ID",objMvrInfo.APP_MVR_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					//Sep 27,2005:Sumit:Following TransactLabel statement has been modified to point to correct address
					//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");					
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddMvrInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objMvrInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objMvrInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objMvrInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"MVR Information Record Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;		
					objTransactionInfo.CUSTOM_INFO		=	";Driver Name = " + strDriverName + ";Driver Code = " + strDriverCode;
					
					
					
					
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int APP_MVR_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				//objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (APP_MVR_ID == -1)
				{
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
					return -1;
				}
				else
				{
					objMvrInfo.APP_MVR_ID = APP_MVR_ID;					
					//Update the Vehicle Class : 
					ClsDriverDetail objDriverDetail = new ClsDriverDetail();					
					//objDriverDetail.UpdateVehicleClass(objMvrInfo.CUSTOMER_ID,objMvrInfo.APP_ID ,objMvrInfo.APP_VERSION_ID);
					//End Update the Vehicle Class		
					if(returnResult>0)
					{
						if(strCalledFrom.ToUpper() != "MOT") //Class will be updated only in case of AUTO
						{
							objDriverDetail.UpdateVehicleClassNew(objMvrInfo.CUSTOMER_ID,objMvrInfo.APP_ID ,objMvrInfo.APP_VERSION_ID,objDataWrapper);							
						}

						objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					}
					return returnResult;
				}
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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
		/// <param name="objOldMvrInfo">Model object having old information</param>
		/// <param name="objMvrInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsMvrInfo objOldMvrInfo,ClsMvrInfo objMvrInfo, string strCalledFrom)
		{
			string		strStoredProc	=	"Proc_UpdateAPP_MVR_INFORMATION";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@APP_MVR_ID",objMvrInfo.APP_MVR_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objMvrInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objMvrInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VIOLATION_ID",objMvrInfo.VIOLATION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@MVR_AMOUNT",System.DBNull.Value);
				objDataWrapper.AddParameter("@MVR_DEATH",System.DBNull.Value);
				if(objMvrInfo.MVR_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@MVR_DATE",objMvrInfo.MVR_DATE);
				else
					objDataWrapper.AddParameter("@MVR_DATE",System.DBNull.Value);
				if(objMvrInfo.OCCURENCE_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@OCCURENCE_DATE",objMvrInfo.OCCURENCE_DATE);
				else
					objDataWrapper.AddParameter("@OCCURENCE_DATE",System.DBNull.Value);
				if(objMvrInfo.DETAILS != "")
					objDataWrapper.AddParameter("@DETAILS",objMvrInfo.DETAILS);
				else
					objDataWrapper.AddParameter("@DETAILS",System.DBNull.Value);

				if(objMvrInfo.POINTS_ASSIGNED < 100)
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",objMvrInfo.POINTS_ASSIGNED);
				else
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",System.DBNull.Value);

				if(objMvrInfo.ADJUST_VIOLATION_POINTS < 100)
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",objMvrInfo.ADJUST_VIOLATION_POINTS);
				else
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",System.DBNull.Value);

				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom);
				if(base.TransactionLogRequired) 
				{
					//					//Sep27,2005:Sumit: File path changed					
					//					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application/aspx/AddMvrInformation.aspx.resx");
					//					//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");
					//					objBuilder.GetUpdateSQL(objOldMvrInfo,objMvrInfo,out strTranXML);
					//
					//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					//					objTransactionInfo.TRANS_TYPE_ID	=	3;
					//					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					//					objTransactionInfo.TRANS_DESC		=	"Information Has Been Updated";
					//					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"Policies/Aspx/Automobile/PolicyAutoMVR.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldMvrInfo,objMvrInfo);
					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objMvrInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objMvrInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"MVR Information is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

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



		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldMvrInfo">Model object having old information</param>
		/// <param name="objMvrInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsPolicyAutoMVR objOldMvrInfo,ClsPolicyAutoMVR objMvrInfo)
		{
			string		strStoredProc	=	"Proc_UpdatePOL_MVR_INFORMATION";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@POL_MVR_ID",objMvrInfo.APP_MVR_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objMvrInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",objMvrInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@VIOLATION_ID",objMvrInfo.VIOLATION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@MVR_AMOUNT",System.DBNull.Value);
				objDataWrapper.AddParameter("@MVR_DEATH",System.DBNull.Value);
				if(objMvrInfo.MVR_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@MVR_DATE",objMvrInfo.MVR_DATE);
				else
					objDataWrapper.AddParameter("@MVR_DATE",System.DBNull.Value);
				if(objMvrInfo.OCCURENCE_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@OCCURENCE_DATE",objMvrInfo.OCCURENCE_DATE);
				else
					objDataWrapper.AddParameter("@OCCURENCE_DATE",System.DBNull.Value);
				if(objMvrInfo.DETAILS != "")
					objDataWrapper.AddParameter("@DETAILS",objMvrInfo.DETAILS);
				else
					objDataWrapper.AddParameter("@DETAILS",System.DBNull.Value);

				if(objMvrInfo.POINTS_ASSIGNED < 100)
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",objMvrInfo.POINTS_ASSIGNED);
				else
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",System.DBNull.Value);

				if(objMvrInfo.ADJUST_VIOLATION_POINTS < 100)
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",objMvrInfo.ADJUST_VIOLATION_POINTS);
				else
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",System.DBNull.Value);

				objDataWrapper.AddParameter("@VERIFIED",objMvrInfo.VERIFIED);
				if(base.TransactionLogRequired) 
				{
					//					//Sep27,2005:Sumit: File path changed					
					//					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application/aspx/AddMvrInformation.aspx.resx");
					//					//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");
					//					objBuilder.GetUpdateSQL(objOldMvrInfo,objMvrInfo,out strTranXML);
					//
					//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					//					objTransactionInfo.TRANS_TYPE_ID	=	3;
					//					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					//					objTransactionInfo.TRANS_DESC		=	"Information Has Been Updated";
					//					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"Policies/Aspx/Automobile/PolicyAutoMVR.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldMvrInfo,objMvrInfo);
					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID  = objMvrInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID  = objMvrInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"MVR Information is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

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
		

		public int Update(ClsPolicyAutoMVR objOldMvrInfo,ClsPolicyAutoMVR objMvrInfo,string strCalledFrom)
		{
			string		strStoredProc	=	"Proc_UpdatePOL_MVR_INFORMATION";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@POL_MVR_ID",objMvrInfo.APP_MVR_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objMvrInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",objMvrInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@VIOLATION_ID",objMvrInfo.VIOLATION_ID);
				objDataWrapper.AddParameter("@VIOLATION_TYPE",objMvrInfo.VIOLATION_TYPE);				
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@MVR_AMOUNT",System.DBNull.Value);
				objDataWrapper.AddParameter("@MVR_DEATH",System.DBNull.Value);
				if(objMvrInfo.MVR_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@MVR_DATE",objMvrInfo.MVR_DATE);
				else
					objDataWrapper.AddParameter("@MVR_DATE",System.DBNull.Value);
				if(objMvrInfo.OCCURENCE_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@OCCURENCE_DATE",objMvrInfo.OCCURENCE_DATE);
				else
					objDataWrapper.AddParameter("@OCCURENCE_DATE",System.DBNull.Value);
				if(objMvrInfo.DETAILS != "")
					objDataWrapper.AddParameter("@DETAILS",objMvrInfo.DETAILS);
				else
					objDataWrapper.AddParameter("@DETAILS",System.DBNull.Value);

				if(objMvrInfo.POINTS_ASSIGNED < 100)
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",objMvrInfo.POINTS_ASSIGNED);
				else
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",System.DBNull.Value);

				if(objMvrInfo.ADJUST_VIOLATION_POINTS < 100)
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",objMvrInfo.ADJUST_VIOLATION_POINTS);
				else
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",System.DBNull.Value);

				objDataWrapper.AddParameter("@VERIFIED",objMvrInfo.VERIFIED);
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom);					 
				if(base.TransactionLogRequired) 
				{
					//					//Sep27,2005:Sumit: File path changed					
					//					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application/aspx/AddMvrInformation.aspx.resx");
					//					//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");
					//					objBuilder.GetUpdateSQL(objOldMvrInfo,objMvrInfo,out strTranXML);
					//
					//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					//					objTransactionInfo.TRANS_TYPE_ID	=	3;
					//					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					//					objTransactionInfo.TRANS_DESC		=	"Information Has Been Updated";
					//					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"Policies/Aspx/Automobile/PolicyAutoMVR.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldMvrInfo,objMvrInfo);
					if(strTranXML=="" || strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.POLICY_ID  = objMvrInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID  = objMvrInfo.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"MVR Information is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;

						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
				//Update the Vehicle Class : 
				//ClsDriverDetail objDriverDetail = new ClsDriverDetail();
				//objDriverDetail.UpdateVehicleClassPol(objMvrInfo.CUSTOMER_ID,objMvrInfo.POLICY_ID,objMvrInfo.POLICY_VERSION_ID);
				if(returnResult>0)
				{
					ClsDriverDetail objDriverDetail = new ClsDriverDetail();
					objDataWrapper.ClearParameteres();
					if(strCalledFrom.ToUpper() != "MOT") //Class will be updated only in case of AUTO
					{
						objDriverDetail.UpdateVehicleClassPolNew(objMvrInfo.CUSTOMER_ID,objMvrInfo.POLICY_ID,objMvrInfo.POLICY_VERSION_ID,objDataWrapper);
					}
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				}
				else
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				
				//End Update the Vehicle Class	
				return returnResult;
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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

		#region Update Overloaded method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldMvrInfo">Model object having old information</param>
		/// <param name="objMvrInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsMvrInfo objOldMvrInfo,ClsMvrInfo objMvrInfo, string strCalledFrom,string strDriverName, string strDriverCode)
		{
			string		strStoredProc	=	"Proc_UpdateAPP_MVR_INFORMATION";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@APP_MVR_ID",objMvrInfo.APP_MVR_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objMvrInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objMvrInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VIOLATION_ID",objMvrInfo.VIOLATION_ID);
				objDataWrapper.AddParameter("@VIOLATION_TYPE",objMvrInfo.VIOLATION_TYPE);
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@MVR_AMOUNT",System.DBNull.Value);
				objDataWrapper.AddParameter("@MVR_DEATH",System.DBNull.Value);
				if(objMvrInfo.MVR_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@MVR_DATE",objMvrInfo.MVR_DATE);
				else
					objDataWrapper.AddParameter("@MVR_DATE",System.DBNull.Value);
				if(objMvrInfo.OCCURENCE_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@OCCURENCE_DATE",objMvrInfo.OCCURENCE_DATE);
				else
					objDataWrapper.AddParameter("@OCCURENCE_DATE",System.DBNull.Value);
				if(objMvrInfo.DETAILS != "")
					objDataWrapper.AddParameter("@DETAILS",objMvrInfo.DETAILS);
				else
					objDataWrapper.AddParameter("@DETAILS",System.DBNull.Value);

				if(objMvrInfo.POINTS_ASSIGNED < 100)
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",objMvrInfo.POINTS_ASSIGNED);
				else
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",System.DBNull.Value);

				if(objMvrInfo.ADJUST_VIOLATION_POINTS < 100)
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",objMvrInfo.ADJUST_VIOLATION_POINTS);
				else
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",System.DBNull.Value);

				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom);
				objDataWrapper.AddParameter("@VERIFIED",objMvrInfo.VERIFIED);				
				if(base.TransactionLogRequired) 
				{					
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\AddMvrInformation.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldMvrInfo,objMvrInfo);
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else				
					{	
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.APP_ID = objMvrInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objMvrInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"MVR Information is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						objTransactionInfo.CUSTOM_INFO		=	";Driver Name = " + strDriverName + ";Driver Code = " + strDriverCode;
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				//objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				//Update the Vehicle Class : 
				ClsDriverDetail objDriverDetail = new ClsDriverDetail();
				//objDriverDetail.UpdateVehicleClass(objMvrInfo.CUSTOMER_ID,objMvrInfo.APP_ID ,objMvrInfo.APP_VERSION_ID);
				//End Update the Vehicle Class						
				if(returnResult>0)
				{
					if(strCalledFrom.ToUpper() != "MOT") //Class will be updated only in case of AUTO
					{
						objDataWrapper.ClearParameteres();
						objDriverDetail.UpdateVehicleClassNew(objMvrInfo.CUSTOMER_ID,objMvrInfo.APP_ID ,objMvrInfo.APP_VERSION_ID,objDataWrapper);										
					}
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);	
				}
				else
				{
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				}
				return returnResult;
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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
		//Sumit:Sep 27, 2005: The following function has been modified to take new parameter values
		//		public static string GetXmlForPageControls(string APP_MVR_ID)
		//		{
		//			string strSql = "Proc_GetXMLAPP_MVR_INFORMATION";
		//			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
		//			objDataWrapper.AddParameter("@APP_MVR_ID",APP_MVR_ID);
		//			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
		//			return objDataSet.GetXml();
		//		}
		//public static string GetXmlForPageControls(string CUSTOMER_ID, string APP_ID,string APP_VERSION_ID)
		public static string GetXmlForPageControls(string CUSTOMER_ID ,string APP_ID,string APP_VERSION_ID,string DRIVER_ID,string APP_MVR_ID)
		{
			string strSql = "Proc_GetXMLAPP_MVR_INFORMATION";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);			
			objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);			
			objDataWrapper.AddParameter("@APP_ID",APP_ID);			
			objDataWrapper.AddParameter("@APP_VERSION_ID",APP_VERSION_ID);			
			objDataWrapper.AddParameter("@DRIVER_ID",DRIVER_ID);			
			objDataWrapper.AddParameter("@APP_MVR_ID",APP_MVR_ID);			
			//objDataWrapper.AddParameter("@DRIVER_ID",DRIVER_ID);			
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);

			if (objDataSet.Tables[0].Rows.Count>0)
				//return objDataSet.GetXml();
				return ClsCommon.GetXMLEncoded(objDataSet.Tables[0]);
			else
				return "";
		}

		public static string GetXmlForPolicyPageControls(string POL_MVR_ID,int CUSTOMER_ID,int POLICY_ID,int POLICY_VERSION_ID,int DRIVER_ID)
		{
			string strSql = "Proc_GetXMLPOL_MVR_INFORMATION";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);			
			objDataWrapper.AddParameter("@POL_MVR_ID",POL_MVR_ID);			
			objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);			
			objDataWrapper.AddParameter("@POLICY_ID",POLICY_ID);			
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",POLICY_VERSION_ID);			
			objDataWrapper.AddParameter("@DRIVER_ID",DRIVER_ID);			
			//objDataWrapper.AddParameter("@DRIVER_ID",DRIVER_ID);			
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);

			if (objDataSet.Tables[0].Rows.Count>0)
				return ClsCommon.GetXMLEncoded(objDataSet.Tables[0]);
			else
				return "";
		}
		#endregion


		#region Delete function		
		/// <summary>
		/// Deletes the information corresponding to the app_mvr_id
		/// </summary>
		/// <param name="objMvrInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		//		public int Delete(string intAppMvrId)
		//		{			
		//			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
		//			string	strStoredProc =	"Proc_DeleteAPP_MVR_INFORMATION";
		//			objDataWrapper.AddParameter("@APP_MVR_ID",intAppMvrId);			
		//			int intResult = objDataWrapper.ExecuteNonQuery(strStoredProc);	
		//			return intResult;
		//		}
		public int Delete(ClsMvrInfo objMvrInfo)
		{
			string		strStoredProc	=	"Proc_DeleteAPP_MVR_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);							
				objDataWrapper.AddParameter("@APP_ID",objMvrInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objMvrInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@APP_MVR_ID",objMvrInfo.APP_MVR_ID);							
				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					//Sep 27,2005:Sumit:Following TransactLabel statement has been modified to point to correct address
					//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");					
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddMvrInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objMvrInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					//objTransactionInfo.RECORDED_BY		=	objMvrInfo.CREATED_BY;
					objTransactionInfo.APP_ID	=	objMvrInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objMvrInfo.APP_VERSION_ID;
					objTransactionInfo.CUSTOM_INFO	=	";Driver Code = " + objMvrInfo.DRIVER_CODE + ";Driver Name = " + objMvrInfo.DRIVER_NAME;

					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"MVR Information Record Has Been Deleted" ;
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CHANGE_XML	=	"";
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
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
		/// Deletes the information corresponding to the app_mvr_id
		/// </summary>
		/// <param name="objMvrInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		//		public int Delete(string intAppMvrId)
		//		{			
		//			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
		//			string	strStoredProc =	"Proc_DeleteAPP_MVR_INFORMATION";
		//			objDataWrapper.AddParameter("@APP_MVR_ID",intAppMvrId);			
		//			int intResult = objDataWrapper.ExecuteNonQuery(strStoredProc);	
		//			return intResult;
		//		}
		public int Delete(ClsPolicyAutoMVR objMvrInfo)
		{
			string		strStoredProc	=	"Proc_DeletePOL_MVR_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@POL_MVR_ID",objMvrInfo.APP_MVR_ID);							
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);	
				objDataWrapper.AddParameter("@POL_ID",objMvrInfo.POLICY_ID);	
				objDataWrapper.AddParameter("@POL_VERSION_ID",objMvrInfo.POLICY_VERSION_ID);	
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);	
				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					//Sep 27,2005:Sumit:Following TransactLabel statement has been modified to point to correct address
					//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");					
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddMvrInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objMvrInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					//objTransactionInfo.RECORDED_BY		=	objMvrInfo.CREATED_BY;
					objTransactionInfo.POLICY_ID 	=	objMvrInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID 	=	objMvrInfo.POLICY_VERSION_ID;
					objTransactionInfo.CUSTOM_INFO	=	";Driver Code = " + objMvrInfo.DRIVER_CODE + ";Driver Name = " + objMvrInfo.DRIVER_NAME;

					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"MVR Information Record Has Been Deleted" ;
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CHANGE_XML	=	"";
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
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


		public int Delete(ClsPolicyAutoMVR objMvrInfo,string strCalledFrom)
		{
			string		strStoredProc	=	"Proc_DeletePOL_MVR_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@POL_MVR_ID",objMvrInfo.APP_MVR_ID);							
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);	
				objDataWrapper.AddParameter("@POL_ID",objMvrInfo.POLICY_ID);	
				objDataWrapper.AddParameter("@POL_VERSION_ID",objMvrInfo.POLICY_VERSION_ID);	
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);	
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom);					 
				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					//Sep 27,2005:Sumit:Following TransactLabel statement has been modified to point to correct address
					//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");					
                    
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Automobile/PolicyAutoMVR.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objMvrInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					//objTransactionInfo.RECORDED_BY		=	objMvrInfo.CREATED_BY;
					objTransactionInfo.POLICY_ID 	=	objMvrInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID 	=	objMvrInfo.POLICY_VERSION_ID;
					objTransactionInfo.CUSTOM_INFO	=	";Driver Code = " + objMvrInfo.DRIVER_CODE + ";Driver Name = " + objMvrInfo.DRIVER_NAME;

					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"MVR Information Record Has Been Deleted" ;
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CHANGE_XML	=	"";
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				//objDataWrapper.ClearParameteres();				
				//Update the Vehicle Class : 
				//				ClsDriverDetail objDriverDetail = new ClsDriverDetail();
				//				objDriverDetail.UpdateVehicleClassPol(objMvrInfo.CUSTOMER_ID,objMvrInfo.POLICY_ID,objMvrInfo.POLICY_VERSION_ID);
				if(returnResult>0)
				{
					ClsDriverDetail objDriverDetail = new ClsDriverDetail();
					objDataWrapper.ClearParameteres();
					if(strCalledFrom.ToUpper() != "MOT") //Class will be updated only in case of AUTO
					{
						objDriverDetail.UpdateVehicleClassPolNew(objMvrInfo.CUSTOMER_ID,objMvrInfo.POLICY_ID,objMvrInfo.POLICY_VERSION_ID,objDataWrapper);
					}
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				}
				//End Update the Vehicle Class	
				return returnResult;
			}			
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}		
		}
		#endregion

		#region Delete Overloaded function		
		/// <summary>
		/// Deletes the information corresponding to the app_mvr_id
		/// </summary>
		/// <param name="objMvrInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		//		public int Delete(string intAppMvrId)
		//		{			
		//			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
		//			string	strStoredProc =	"Proc_DeleteAPP_MVR_INFORMATION";
		//			objDataWrapper.AddParameter("@APP_MVR_ID",intAppMvrId);			
		//			int intResult = objDataWrapper.ExecuteNonQuery(strStoredProc);	
		//			return intResult;
		//		}
		public int Delete(ClsMvrInfo objMvrInfo, string strDriverName, string strDriverCode,string strCalledFrom)
		{
			string		strStoredProc	=	"Proc_DeleteAPP_MVR_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);							
				objDataWrapper.AddParameter("@APP_ID",objMvrInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objMvrInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@APP_MVR_ID",objMvrInfo.APP_MVR_ID);				
				//<001><Start> Added by Ashwani on 09 Feb. 2006 
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom);
				//<001><End>

				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					//Sep 27,2005:Sumit:Following TransactLabel statement has been modified to point to correct address
					//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");					
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddMvrInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objMvrInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					//objTransactionInfo.RECORDED_BY		=	objMvrInfo.CREATED_BY;
					objTransactionInfo.APP_ID	=	objMvrInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objMvrInfo.APP_VERSION_ID;
					//objTransactionInfo.CUSTOM_INFO	=	";Driver Code = " + objMvrInfo.DRIVER_CODE + ";Driver Name = " + objMvrInfo.DRIVER_NAME;

					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"MVR Information Record Has Been Deleted" ;
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CHANGE_XML	=	"";
					objTransactionInfo.CUSTOM_INFO		=	";Driver Name = " + strDriverName + ";Driver Code = " + strDriverCode + ";Violation = " + objMvrInfo.VIOLATION_DES;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				objDataWrapper.ClearParameteres();				
				//Update the Vehicle Class : 
				ClsDriverDetail objDriverDetail = new ClsDriverDetail();
				//objDriverDetail.UpdateVehicleClass(objMvrInfo.CUSTOMER_ID,objMvrInfo.APP_ID ,objMvrInfo.APP_VERSION_ID);
				//End Update the Vehicle Class		
				if(returnResult>0)
				{
					if(strCalledFrom.ToUpper()!="MOT")
					{
						objDriverDetail.UpdateVehicleClassNew(objMvrInfo.CUSTOMER_ID,objMvrInfo.APP_ID ,objMvrInfo.APP_VERSION_ID,objDataWrapper);
						
					}
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				}
				return returnResult;
			}			
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}		
		}
		
		#endregion



		#region Delete function for WATER LOB
		/// <summary>
		/// Deletes the information corresponding to the app_mvr_id
		/// </summary>
		/// <param name="objMvrInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		//		public int DeleteForWater(string intAppWaterMvrId)
		//		{			
		//			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
		//			string	strStoredProc =	"Proc_DeleteAPP_WATER_MVR_INFORMATION";
		//			objDataWrapper.AddParameter("@APP_WATER_MVR_ID",intAppWaterMvrId);			
		//			int intResult = objDataWrapper.ExecuteNonQuery(strStoredProc);	
		//			return intResult;
		//		}
		public int DeleteForWater(ClsMvrInfo objMvrInfo)
		{
			string		strStoredProc	=	"Proc_DeleteAPP_WATER_MVR_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);							
				objDataWrapper.AddParameter("@APP_ID",objMvrInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objMvrInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);				
				objDataWrapper.AddParameter("@APP_WATER_MVR_ID",objMvrInfo.APP_WATER_MVR_ID);	
						
				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					//Sep 27,2005:Sumit:Following TransactLabel statement has been modified to point to correct address
					//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");					
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddMvrInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					//string strTranXML = objBuilder.GetTransactionLogXML(objMvrInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					//objTransactionInfo.RECORDED_BY		=	objMvrInfo.CREATED_BY;
					objTransactionInfo.APP_ID	=	objMvrInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objMvrInfo.APP_VERSION_ID;				
					
					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"MVR Information Record Has Been Deleted";
					//objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CHANGE_XML		=	"";
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
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

		public int DeleteForWater(ClsMvrInfo objMvrInfo,string strDriverName, string strDriverCode)
		{
			string		strStoredProc	=	"Proc_DeleteAPP_WATER_MVR_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);							
				objDataWrapper.AddParameter("@APP_ID",objMvrInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objMvrInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);				
				objDataWrapper.AddParameter("@APP_WATER_MVR_ID",objMvrInfo.APP_WATER_MVR_ID);	
						
				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					//Sep 27,2005:Sumit:Following TransactLabel statement has been modified to point to correct address
					//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");					
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddMvrInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					//string strTranXML = objBuilder.GetTransactionLogXML(objMvrInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					//objTransactionInfo.RECORDED_BY		=	objMvrInfo.CREATED_BY;
					objTransactionInfo.APP_ID	=	objMvrInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objMvrInfo.APP_VERSION_ID;				
					
					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"MVR Information Record Has Been Deleted";
					//objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CHANGE_XML		=	"";
					objTransactionInfo.CUSTOM_INFO		=	";Operator Name = " + strDriverName + ";Operator Code = " + strDriverCode + ";Violation = " + objMvrInfo.VIOLATION_DES;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
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


		public int DeleteForUmbrella(ClsMvrInfo objMvrInfo,string strDriverName, string strDriverCode)
		{
			string		strStoredProc	=	"Proc_DeleteAPP_UMBRELLA_MVR_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);							
				objDataWrapper.AddParameter("@APP_ID",objMvrInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objMvrInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);				
				objDataWrapper.AddParameter("@APP_UMB_MVR_ID",objMvrInfo.APP_UMB_MVR_ID);	
						
				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					//Sep 27,2005:Sumit:Following TransactLabel statement has been modified to point to correct address
					//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");					
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddMvrInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					//string strTranXML = objBuilder.GetTransactionLogXML(objMvrInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					//objTransactionInfo.RECORDED_BY		=	objMvrInfo.CREATED_BY;
					objTransactionInfo.APP_ID	=	objMvrInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objMvrInfo.APP_VERSION_ID;				
					
					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"MVR Information Record Has Been Deleted";
					//objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CHANGE_XML		=	"";
					objTransactionInfo.CUSTOM_INFO		=	";Driver/Operator Name = " + strDriverName + ";Driver/Operator Code = " + strDriverCode + ";Violation = " + objMvrInfo.VIOLATION_DES;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
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
		/// Deletes the information corresponding to the app_mvr_id
		/// </summary>
		/// <param name="objMvrInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		//		public int DeleteForWater(string intAppWaterMvrId)
		//		{			
		//			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
		//			string	strStoredProc =	"Proc_DeleteAPP_WATER_MVR_INFORMATION";
		//			objDataWrapper.AddParameter("@APP_WATER_MVR_ID",intAppWaterMvrId);			
		//			int intResult = objDataWrapper.ExecuteNonQuery(strStoredProc);	
		//			return intResult;
		//		}
		public int DeleteForWater(ClsPolicyAutoMVR objMvrInfo)
		{
			string		strStoredProc	=	"Proc_DeletePOL_WATER_MVR_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@APP_WATER_MVR_ID",objMvrInfo.POL_WATER_MVR_ID);	
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);	
				objDataWrapper.AddParameter("@POL_ID",objMvrInfo.POLICY_ID);	
				objDataWrapper.AddParameter("@POL_VERSION_ID",objMvrInfo.POLICY_VERSION_ID);	
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);	
						
				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					//Sep 27,2005:Sumit:Following TransactLabel statement has been modified to point to correct address
					//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");					
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddMvrInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					//string strTranXML = objBuilder.GetTransactionLogXML(objMvrInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					//objTransactionInfo.RECORDED_BY		=	objMvrInfo.CREATED_BY;
					objTransactionInfo.POLICY_ID 	=	objMvrInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID 	=	objMvrInfo.POLICY_VERSION_ID;				
					
					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"MVR Information Record Has Been Deleted";
					//objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CHANGE_XML		=	"";
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
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
		#endregion

		#region Add(Insert) functions for WaterCraft LOB
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objMvrInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int AddForWater(ClsMvrInfo objMvrInfo)
		{
			string		strStoredProc	=	"Proc_InsertAPP_WATER_MVR_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objMvrInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objMvrInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VIOLATION_ID",objMvrInfo.VIOLATION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@VIOLATION_TYPE",objMvrInfo.VIOLATION_TYPE);
				objDataWrapper.AddParameter("@MVR_AMOUNT",System.DBNull.Value);
				objDataWrapper.AddParameter("@MVR_DEATH",System.DBNull.Value);
				if(objMvrInfo.MVR_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@MVR_DATE",objMvrInfo.MVR_DATE);
				else
					objDataWrapper.AddParameter("@MVR_DATE",System.DBNull.Value);
				if(objMvrInfo.OCCURENCE_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@OCCURENCE_DATE",objMvrInfo.OCCURENCE_DATE);
				else
					objDataWrapper.AddParameter("@OCCURENCE_DATE",System.DBNull.Value);
				if(objMvrInfo.DETAILS != "")
					objDataWrapper.AddParameter("@DETAILS",objMvrInfo.DETAILS);
				else
					objDataWrapper.AddParameter("@DETAILS",System.DBNull.Value);

				if(objMvrInfo.POINTS_ASSIGNED < 100)
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",objMvrInfo.POINTS_ASSIGNED);
				else
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",System.DBNull.Value);

				if(objMvrInfo.ADJUST_VIOLATION_POINTS < 100)
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",objMvrInfo.ADJUST_VIOLATION_POINTS);
				else
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",System.DBNull.Value);

				objDataWrapper.AddParameter("@IS_ACTIVE","Y");
				objDataWrapper.AddParameter("@VERIFIED",objMvrInfo.VERIFIED);	
				objDataWrapper.AddParameter("@CREATED_BY",objMvrInfo.CREATED_BY);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@APP_WATER_MVR_ID",objMvrInfo.APP_WATER_MVR_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					//Sep 27,2005:Sumit:Following TransactLabel statement has been modified to point to correct address
					//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");					
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddMvrInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objMvrInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objMvrInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objMvrInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"MVR Information Record Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;		
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int APP_WATER_MVR_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (APP_WATER_MVR_ID == -1)
				{
					return -1;
				}
				else
				{
					objMvrInfo.APP_WATER_MVR_ID= APP_WATER_MVR_ID;
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

		public int AddForWater(ClsMvrInfo objMvrInfo,string strDriverName, string strDriverCode)
		{
			string		strStoredProc	=	"Proc_InsertAPP_WATER_MVR_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objMvrInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objMvrInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VIOLATION_ID",objMvrInfo.VIOLATION_ID);
				objDataWrapper.AddParameter("@VIOLATION_TYPE",objMvrInfo.VIOLATION_TYPE);				
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@MVR_AMOUNT",System.DBNull.Value);
				objDataWrapper.AddParameter("@MVR_DEATH",System.DBNull.Value);
				if(objMvrInfo.MVR_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@MVR_DATE",objMvrInfo.MVR_DATE);
				else
					objDataWrapper.AddParameter("@MVR_DATE",System.DBNull.Value);
				if(objMvrInfo.OCCURENCE_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@OCCURENCE_DATE",objMvrInfo.OCCURENCE_DATE);
				else
					objDataWrapper.AddParameter("@OCCURENCE_DATE",System.DBNull.Value);
				if(objMvrInfo.DETAILS != "")
					objDataWrapper.AddParameter("@DETAILS",objMvrInfo.DETAILS);
				else
					objDataWrapper.AddParameter("@DETAILS",System.DBNull.Value);

				if(objMvrInfo.POINTS_ASSIGNED < 100)
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",objMvrInfo.POINTS_ASSIGNED);
				else
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",System.DBNull.Value);

				if(objMvrInfo.ADJUST_VIOLATION_POINTS < 100)
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",objMvrInfo.ADJUST_VIOLATION_POINTS);
				else
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",System.DBNull.Value);

				objDataWrapper.AddParameter("@IS_ACTIVE","Y");
				objDataWrapper.AddParameter("@VERIFIED",objMvrInfo.VERIFIED);				
				objDataWrapper.AddParameter("@CREATED_BY",objMvrInfo.CREATED_BY);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@APP_WATER_MVR_ID",objMvrInfo.APP_WATER_MVR_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					//Sep 27,2005:Sumit:Following TransactLabel statement has been modified to point to correct address
					//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");					
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddMvrInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objMvrInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objMvrInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objMvrInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"MVR Information Record Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;		
					objTransactionInfo.CUSTOM_INFO		=	";Operator Name = " + strDriverName + ";Operator Code = " + strDriverCode;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int APP_WATER_MVR_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (APP_WATER_MVR_ID == -1)
				{
					return -1;
				}
				else
				{
					objMvrInfo.APP_WATER_MVR_ID= APP_WATER_MVR_ID;
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


		public int AddForUmbrella(ClsMvrInfo objMvrInfo,string strDriverName, string strDriverCode)
		{
			string		strStoredProc	=	"Proc_InsertAPP_UMBRELLA_MVR_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objMvrInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objMvrInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VIOLATION_ID",objMvrInfo.VIOLATION_ID);
				objDataWrapper.AddParameter("@VIOLATION_TYPE",objMvrInfo.VIOLATION_TYPE);				
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@MVR_AMOUNT",DefaultValues.GetDoubleNullFromNegative(objMvrInfo.MVR_AMOUNT));
				objDataWrapper.AddParameter("@MVR_DEATH",objMvrInfo.MVR_DEATH);
				objDataWrapper.AddParameter("@MVR_DATE",objMvrInfo.MVR_DATE);
				objDataWrapper.AddParameter("@IS_ACTIVE","Y");
				objDataWrapper.AddParameter("@VERIFIED",objMvrInfo.VERIFIED);	
				objDataWrapper.AddParameter("@CREATED_BY",objMvrInfo.CREATED_BY);	
				objDataWrapper.AddParameter("@CREATED_DATETIME",objMvrInfo.CREATED_DATETIME);	
				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@APP_UMB_MVR_ID",objMvrInfo.APP_UMB_MVR_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					//Sep 27,2005:Sumit:Following TransactLabel statement has been modified to point to correct address
					//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");					
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddMvrInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objMvrInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objMvrInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objMvrInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"MVR Information Record Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;		
					objTransactionInfo.CUSTOM_INFO		=	";Driver/Operator Name = " + strDriverName + ";Driver/Operator Code = " + strDriverCode;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int APP_UMB_MVR_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (APP_UMB_MVR_ID == -1)
				{
					return -1;
				}
				else
				{
					objMvrInfo.APP_UMB_MVR_ID= APP_UMB_MVR_ID;
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



		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objMvrInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int AddForWater(ClsPolicyAutoMVR objMvrInfo)
		{
			return AddForWater(objMvrInfo,"");
		}
		
		public int AddForWater(ClsPolicyAutoMVR objMvrInfo,string CalledFrom)
		{
			string		strStoredProc	=	"Proc_InsertPOL_WATER_MVR_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			int returnResult = 0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objMvrInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",objMvrInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@VIOLATION_ID",objMvrInfo.VIOLATION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@VIOLATION_TYPE",objMvrInfo.VIOLATION_TYPE);
				objDataWrapper.AddParameter("@MVR_AMOUNT",System.DBNull.Value);
				objDataWrapper.AddParameter("@MVR_DEATH",System.DBNull.Value);
				if(objMvrInfo.MVR_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@MVR_DATE",objMvrInfo.MVR_DATE);
				else
					objDataWrapper.AddParameter("@MVR_DATE",System.DBNull.Value);
				if(objMvrInfo.OCCURENCE_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@OCCURENCE_DATE",objMvrInfo.OCCURENCE_DATE);
				else
					objDataWrapper.AddParameter("@OCCURENCE_DATE",System.DBNull.Value);
				if(objMvrInfo.DETAILS != "")
					objDataWrapper.AddParameter("@DETAILS",objMvrInfo.DETAILS);
				else
					objDataWrapper.AddParameter("@DETAILS",System.DBNull.Value);

				if(objMvrInfo.POINTS_ASSIGNED < 100)
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",objMvrInfo.POINTS_ASSIGNED);
				else
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",System.DBNull.Value);

				if(objMvrInfo.ADJUST_VIOLATION_POINTS < 100)
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",objMvrInfo.ADJUST_VIOLATION_POINTS);
				else
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",System.DBNull.Value);

				objDataWrapper.AddParameter("@IS_ACTIVE","Y");
				objDataWrapper.AddParameter("@CREATED_BY",objMvrInfo.CREATED_BY);
				objDataWrapper.AddParameter("@VERIFIED",objMvrInfo.VERIFIED);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@POL_WATER_MVR_ID",objMvrInfo.POL_WATER_MVR_ID,SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlParameter1  = (SqlParameter) objDataWrapper.AddParameter("@RET_VIOLATION_TYPE",null,SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlParameter2  = (SqlParameter) objDataWrapper.AddParameter("@RET_VERIFIED",null,SqlDbType.Int,ParameterDirection.Output);
				
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				int tmpVIOLATION_TYPE=0;
				if (objSqlParameter1.Value!=DBNull.Value)
					tmpVIOLATION_TYPE = int.Parse(objSqlParameter1.Value.ToString());
				int tmpVERIFIED = int.Parse(objSqlParameter2.Value.ToString());

				objMvrInfo.VIOLATION_TYPE = tmpVIOLATION_TYPE;
				objMvrInfo.VERIFIED = tmpVERIFIED;
				if (tmpVERIFIED>0)
				{
					if(TransactionLogRequired)
					{	
						//Sep 27,2005:Sumit:Following TransactLabel statement has been modified to point to correct address
						//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");					
						objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddMvrInformation.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						string strTranXML = objBuilder.GetTransactionLogXML(objMvrInfo);
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.POLICY_ID  = objMvrInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID  = objMvrInfo.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"MVR Information Record Has Been Added";
						objTransactionInfo.CHANGE_XML		=	strTranXML;		
						//Executing the query
						 objDataWrapper.ExecuteNonQuery(objTransactionInfo);
					}
//					else
//					{
//						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
//					}
				}
				int POL_WATER_MVR_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (POL_WATER_MVR_ID == -1)
				{
					return -1;
				}
				else
				{
					objMvrInfo.POL_WATER_MVR_ID= POL_WATER_MVR_ID;
					//return returnResult;
					return tmpVERIFIED;
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

		#region Update for Water LOB
		
		public int UpdateForWater(ClsMvrInfo objOldMvrInfo,ClsMvrInfo objMvrInfo)
		{
			string		strStoredProc	=	"Proc_UpdateAPP_WATER_MVR_INFORMATION";			
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@APP_WATER_MVR_ID",objMvrInfo.APP_WATER_MVR_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objMvrInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objMvrInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VIOLATION_ID",objMvrInfo.VIOLATION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@MVR_AMOUNT",System.DBNull.Value);
				objDataWrapper.AddParameter("@MVR_DEATH",System.DBNull.Value);
				if(objMvrInfo.MVR_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@MVR_DATE",objMvrInfo.MVR_DATE);
				else
					objDataWrapper.AddParameter("@MVR_DATE",System.DBNull.Value);
				if(objMvrInfo.OCCURENCE_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@OCCURENCE_DATE",objMvrInfo.OCCURENCE_DATE);
				else
					objDataWrapper.AddParameter("@OCCURENCE_DATE",System.DBNull.Value);
				if(objMvrInfo.DETAILS != "")
					objDataWrapper.AddParameter("@DETAILS",objMvrInfo.DETAILS);
				else
					objDataWrapper.AddParameter("@DETAILS",System.DBNull.Value);

				if(objMvrInfo.POINTS_ASSIGNED < 100)
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",objMvrInfo.POINTS_ASSIGNED);
				else
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",System.DBNull.Value);

				if(objMvrInfo.ADJUST_VIOLATION_POINTS < 100)
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",objMvrInfo.ADJUST_VIOLATION_POINTS);
				else
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",System.DBNull.Value);

				objDataWrapper.AddParameter("@MODIFIED_BY",objMvrInfo.MODIFIED_BY);
				if(base.TransactionLogRequired) 
				{
					//					//Sep27,2005:Sumit: File path changed					
					//					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application/aspx/AddMvrInformation.aspx.resx");
					//					//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");
					//					objBuilder.GetUpdateSQL(objOldMvrInfo,objMvrInfo,out strTranXML);
					//
					//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					//					objTransactionInfo.TRANS_TYPE_ID	=	3;
					//					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					//					objTransactionInfo.TRANS_DESC		=	"Information Has Been Updated";
					//					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\AddMvrInformation.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldMvrInfo,objMvrInfo);
					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objMvrInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objMvrInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"MVR Information is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

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

		public int UpdateForWater(ClsMvrInfo objOldMvrInfo,ClsMvrInfo objMvrInfo,string strDriverName, string strDriverCode)
		{
			string		strStoredProc	=	"Proc_UpdateAPP_WATER_MVR_INFORMATION";			
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@APP_WATER_MVR_ID",objMvrInfo.APP_WATER_MVR_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objMvrInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objMvrInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VIOLATION_ID",objMvrInfo.VIOLATION_ID);
				objDataWrapper.AddParameter("@VIOLATION_TYPE",objMvrInfo.VIOLATION_TYPE);				
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@MVR_AMOUNT",System.DBNull.Value);
				objDataWrapper.AddParameter("@MVR_DEATH",System.DBNull.Value);
				if(objMvrInfo.MVR_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@MVR_DATE",objMvrInfo.MVR_DATE);
				else
					objDataWrapper.AddParameter("@MVR_DATE",System.DBNull.Value);
				if(objMvrInfo.OCCURENCE_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@OCCURENCE_DATE",objMvrInfo.OCCURENCE_DATE);
				else
					objDataWrapper.AddParameter("@OCCURENCE_DATE",System.DBNull.Value);
				if(objMvrInfo.DETAILS != "")
					objDataWrapper.AddParameter("@DETAILS",objMvrInfo.DETAILS);
				else
					objDataWrapper.AddParameter("@DETAILS",System.DBNull.Value);

				if(objMvrInfo.POINTS_ASSIGNED < 100)
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",objMvrInfo.POINTS_ASSIGNED);
				else
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",System.DBNull.Value);

				if(objMvrInfo.ADJUST_VIOLATION_POINTS < 100)
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",objMvrInfo.ADJUST_VIOLATION_POINTS);
				else
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",System.DBNull.Value);

				objDataWrapper.AddParameter("@VERIFIED",objMvrInfo.VERIFIED);	
				objDataWrapper.AddParameter("@MODIFIED_BY",objMvrInfo.MODIFIED_BY);
				if(base.TransactionLogRequired) 
				{										
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\AddMvrInformation.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldMvrInfo,objMvrInfo);
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else				
					{	
                        Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();					
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.APP_ID = objMvrInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objMvrInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"MVR Information is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						objTransactionInfo.CUSTOM_INFO		=	";Operator Name  = " + strDriverName + ";Operator Code = " + strDriverCode;

						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

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


		public int UpdateForUmbrella(ClsMvrInfo objOldMvrInfo,ClsMvrInfo objMvrInfo,string strDriverName, string strDriverCode)
		{
			string		strStoredProc	=	"Proc_UpdateAPP_UMBRELLA_MVR_INFORMATION";			
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@APP_UMB_MVR_ID",objMvrInfo.APP_UMB_MVR_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objMvrInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objMvrInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VIOLATION_ID",objMvrInfo.VIOLATION_ID);
				objDataWrapper.AddParameter("@VIOLATION_TYPE",objMvrInfo.VIOLATION_TYPE);				
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@MVR_AMOUNT",DefaultValues.GetDoubleNullFromNegative(objMvrInfo.MVR_AMOUNT));
				objDataWrapper.AddParameter("@MVR_DEATH",objMvrInfo.MVR_DEATH);
				objDataWrapper.AddParameter("@MVR_DATE",objMvrInfo.MVR_DATE);	
				objDataWrapper.AddParameter("@VERIFIED",objMvrInfo.VERIFIED);
				objDataWrapper.AddParameter("@MODIFIED_BY",objMvrInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objMvrInfo.LAST_UPDATED_DATETIME);
				if(base.TransactionLogRequired) 
				{										
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\AddMvrInformation.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldMvrInfo,objMvrInfo);
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else				
					{	
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();					
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.APP_ID = objMvrInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objMvrInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"MVR Information is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						objTransactionInfo.CUSTOM_INFO		=	";Driver/Operator Name  = " + strDriverName + ";Driver/Operator Code = " + strDriverCode;

						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

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


		
		
		public int UpdateForWater(ClsPolicyAutoMVR objOldMvrInfo,ClsPolicyAutoMVR objMvrInfo)
		{
			string		strStoredProc	=	"Proc_UpdatePOL_WATER_MVR_INFORMATION";			
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@POL_WATER_MVR_ID",objMvrInfo.POL_WATER_MVR_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objMvrInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",objMvrInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@VIOLATION_ID",objMvrInfo.VIOLATION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@MVR_AMOUNT",System.DBNull.Value);
				objDataWrapper.AddParameter("@MVR_DEATH",System.DBNull.Value);
				if(objMvrInfo.MVR_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@MVR_DATE",objMvrInfo.MVR_DATE);
				else
					objDataWrapper.AddParameter("@MVR_DATE",System.DBNull.Value);
				if(objMvrInfo.OCCURENCE_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@OCCURENCE_DATE",objMvrInfo.OCCURENCE_DATE);
				else
					objDataWrapper.AddParameter("@OCCURENCE_DATE",System.DBNull.Value);
				if(objMvrInfo.DETAILS != "")
					objDataWrapper.AddParameter("@DETAILS",objMvrInfo.DETAILS);
				else
					objDataWrapper.AddParameter("@DETAILS",System.DBNull.Value);

				if(objMvrInfo.POINTS_ASSIGNED < 100)
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",objMvrInfo.POINTS_ASSIGNED);
				else
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",System.DBNull.Value);

				if(objMvrInfo.ADJUST_VIOLATION_POINTS < 100)
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",objMvrInfo.ADJUST_VIOLATION_POINTS);
				else
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",System.DBNull.Value);

				objDataWrapper.AddParameter("@MODIFIED_BY",objMvrInfo.MODIFIED_BY);
				if(base.TransactionLogRequired) 
				{
					//					//Sep27,2005:Sumit: File path changed					
					//					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application/aspx/AddMvrInformation.aspx.resx");
					//					//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");
					//					objBuilder.GetUpdateSQL(objOldMvrInfo,objMvrInfo,out strTranXML);
					//
					//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					//					objTransactionInfo.TRANS_TYPE_ID	=	3;
					//					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					//					objTransactionInfo.TRANS_DESC		=	"Information Has Been Updated";
					//					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\AddMvrInformation.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldMvrInfo,objMvrInfo);
					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID = objMvrInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objMvrInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"MVR Information is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

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

		#region "GetxmlMethods" for WaterCraft LOB
		//Sumit:Sep 27, 2005: The following function has been modified to take new parameter values
		//		public static string GetXmlForPageControls(string APP_MVR_ID)
		//		{
		//			string strSql = "Proc_GetXMLAPP_MVR_INFORMATION";
		//			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
		//			objDataWrapper.AddParameter("@APP_MVR_ID",APP_MVR_ID);
		//			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
		//			return objDataSet.GetXml();
		//		}
		//public static string GetXmlForPageControls(string CUSTOMER_ID, string APP_ID,string APP_VERSION_ID)
		public static string GetXmlForPageControlsForWater(string CUSTOMER_ID ,string APP_ID,string APP_VERSION_ID,string DRIVER_ID,string APP_WATER_MVR_ID)
		{
			string strSql = "Proc_GetXMLAPP_WATER_MVR_INFORMATION";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);			
			objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);			
			objDataWrapper.AddParameter("@APP_ID",APP_ID);			
			objDataWrapper.AddParameter("@APP_VERSION_ID",APP_VERSION_ID);			
			objDataWrapper.AddParameter("@DRIVER_ID",DRIVER_ID);	
			objDataWrapper.AddParameter("@APP_WATER_MVR_ID",APP_WATER_MVR_ID);			
			//objDataWrapper.AddParameter("@DRIVER_ID",DRIVER_ID);			
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);

			if (objDataSet.Tables[0].Rows.Count>0)
				//return objDataSet.GetXml();
				return ClsCommon.GetXMLEncoded(objDataSet.Tables[0]);
			else
				return "";
		}

		public static string GetXmlForPageControlsForUmbrella(string CUSTOMER_ID ,string APP_ID,string APP_VERSION_ID,string DRIVER_ID,string APP_UMB_MVR_ID)
		{
			string strSql = "Proc_GetXMLAPP_UMB_MVR_INFORMATION";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);			
			objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);			
			objDataWrapper.AddParameter("@APP_ID",APP_ID);			
			objDataWrapper.AddParameter("@APP_VERSION_ID",APP_VERSION_ID);			
			objDataWrapper.AddParameter("@DRIVER_ID",DRIVER_ID);	
			objDataWrapper.AddParameter("@APP_UMB_MVR_ID",APP_UMB_MVR_ID);			
			//objDataWrapper.AddParameter("@DRIVER_ID",DRIVER_ID);			
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);

			if (objDataSet.Tables[0].Rows.Count>0)
				//return objDataSet.GetXml();
				return ClsCommon.GetXMLEncoded(objDataSet.Tables[0]);
			else
				return "";
		}

		public static string GetXmlForPolicyPageControlsForWater(string APP_WATER_MVR_ID,int CUSTOMER_ID,int POLICY_ID,int POLICY_VERSION_ID,int DRIVER_ID)
		{
			string strSql = "Proc_GetXMLPOL_WATER_MVR_INFORMATION";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);			
			objDataWrapper.AddParameter("@APP_WATER_MVR_ID",APP_WATER_MVR_ID);			
			objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);			
			objDataWrapper.AddParameter("@POLICY_ID",POLICY_ID);			
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",POLICY_VERSION_ID);			
			objDataWrapper.AddParameter("@DRIVER_ID",DRIVER_ID);			

			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);

			if (objDataSet.Tables[0].Rows.Count>0)
				return objDataSet.GetXml();
			else
				return "";
		}
		#endregion

		#region GerMVRDetails
		/// <summary>
		/// Gets the MVR details of app id, app version id, customer id and driver id
		/// </summary>
		/// <param name="AppID">Application identification no</param>
		/// <param name="AppVerId">Application version identification no</param>
		/// <param name="CustomerId">Customeridentification no</param>
		/// <param name="DriverId">Driver identification no</param>
		/// <returns>Datatable of MVR information</returns>
		internal DataTable GetMVRDetails(int AppID, int AppVerId, int CustomerId, int DriverId)
		{
			try
			{
				return DataWrapper.ExecuteDataset(ClsCommon.ConnStr, CommandType.Text
					, "SELECT "
					+ " APP_MVR_ID,CUSTOMER_ID, APP_ID, APP_VERSION_ID, "
					+ " DRIVER_ID,MVR_AMOUNT, MVR_DEATH, MVR_DATE, "
					+ " VIOLATION_ID, IS_ACTIVE "
					+ " FROM APP_MVR_INFORMATION WHERE CUSTOMER_ID=" + CustomerId
					+ " AND APP_ID = " + AppID + " AND APP_VERSION_ID = " + AppVerId
					+ " AND DRIVER_ID = " + DriverId
					).Tables[0];

			}
			catch(Exception objExp)
			{
				throw(objExp);
			}
		}
		#endregion

		#region GetViolationPoints
		/// <summary>
		/// Gets the Sum of MVR violation points of any driver
		/// </summary>
		/// <param name="AppID">Application identification no</param>
		/// <param name="AppVerId">Application version identification no</param>
		/// <param name="CustomerId">Customeridentification no</param>
		/// <param name="DriverId">Driver identification no</param>
		/// <returns>Violation points</returns>
		internal int GetViolationPoints(int AppID, int AppVerId, int CustomerId, int DriverId)
		{
			try
			{
				//Fetching the MVR details for this driver
				DataTable dtMVR = GetMVRDetails(AppID, AppVerId, CustomerId, DriverId);

				int iPoints = 0;
				
				int iAccidentPoint = 3;		//For accident 3 points are defined
				bool blnFirst = true;		//Flag for hoding whether first or not

				//Calculating the points
				foreach(DataRow dr in dtMVR.Rows)
				{
					if (dr["MVR_AMOUNT"] != null)
					{
						if (Convert.ToDouble(dr["MVR_AMOUNT"]) > 1000)
						{
							//Checking whether first accident or not
							if (blnFirst)
							{
								if (CanForgiveAccident(AppID, AppVerId, CustomerId, DriverId, Convert.ToInt32(dr["APP_MVR_ID"])))
								{
								}
							}

							//For amount greater then 1000 violation will be treated as accident
							iPoints += iAccidentPoint;
							blnFirst = false;
						}
						else
						{

							//Adding the point as defined in violation table
							iPoints += Convert.ToInt32(dr["SD_POINTS"]);
						}
					}
				}

				return iPoints;
			}
			catch(Exception objExp)
			{
				throw(objExp);
			}
		}
		#endregion

		public static DataTable GetDriverDetailsForMVR(string Customer_ID, string App_ID, string App_Version_ID, string Driver_ID, string strCalledFrom)
		{			
			DataSet dsTransDesc=new DataSet();
			dsTransDesc=DataWrapper.ExecuteDataset(ConnStr,"Proc_GetDriversForMVR",Customer_ID,App_ID,App_Version_ID,Driver_ID,strCalledFrom);
			return dsTransDesc.Tables[0];
		}

		public static DataTable GetDriverDetailsForPolicyMVR(string Customer_ID, string Pol_ID, string Pol_Version_ID, string Driver_ID, string strCalledFrom)
		{			
			DataSet dsTransDesc=new DataSet();
			dsTransDesc=DataWrapper.ExecuteDataset(ConnStr,"Proc_GetDriversForPolicyMVR",Customer_ID,Pol_ID,Pol_Version_ID,Driver_ID,strCalledFrom);
			return dsTransDesc.Tables[0];
		}
		

		public static string GetDriverDOBForPolicy(int customerID,int polID,int polVersionID,int driverId,string calledfrom)
		{
			string	strStoredProc =	"Proc_GetDriverDOBForPolicy";
			string dob="";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POL_ID",polID);
			objWrapper.AddParameter("@POL_VERSION_ID",polVersionID);
			objWrapper.AddParameter("@DRIVER_ID",driverId);
			objWrapper.AddParameter("@CALLEDFROM",calledfrom);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			if(ds.Tables[0].Rows.Count > 0)
			{
				dob=ds.Tables[0].Rows[0][0].ToString();
			}			
			return dob;

		}
		
		#region
		/// <summary>
		/// Return whether the passed violation of any driver against any application can be forgive or not
		/// </summary>
		/// <param name="AppID">Application identification no</param>
		/// <param name="AppVerId">Application version identification no</param>
		/// <param name="CustomerId">Customeridentification no</param>
		/// <param name="DriverId">Driver identification no</param>
		/// <param name="AppMvrId">Violation identification number</param>
		/// <returns>True if can be forgive else false</returns>
		private bool CanForgiveAccident(int AppID, int AppVerId, int CustomerId, int DriverId, int AppMvrId)
		{
			return false;
		}
		#endregion
		
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objMvrInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int SaveWatercraftViolations(ClsMvrInfo objMvrInfo,DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_Save_APP_WATER_MVR_INFORMATION_ACORD";
			DateTime	RecordDate		=	DateTime.Now;
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_ID",objMvrInfo.APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objMvrInfo.APP_VERSION_ID);
			objDataWrapper.AddParameter("@VIOLATION_ID",objMvrInfo.VIOLATION_ID);
			objDataWrapper.AddParameter("@VIOLATION_CODE",objMvrInfo.VIOLATION_CODE);
			objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
			objDataWrapper.AddParameter("@MVR_AMOUNT",DefaultValues.GetDoubleNullFromNegative(objMvrInfo.MVR_AMOUNT));
			objDataWrapper.AddParameter("@MVR_DEATH",objMvrInfo.MVR_DEATH);
			objDataWrapper.AddParameter("@MVR_DATE",objMvrInfo.MVR_DATE);
			objDataWrapper.AddParameter("@POINTS_ASSIGNED",objMvrInfo.POINTS_ASSIGNED);
			objDataWrapper.AddParameter("@IS_ACTIVE","Y");

			SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@APP_MVR_ID",objMvrInfo.APP_MVR_ID,SqlDbType.Int,ParameterDirection.Output);

			//int returnResult = 0;
				
			objDataWrapper.ExecuteNonQuery(strStoredProc);
				
			int APP_MVR_ID = -1;//int.Parse(objSqlParameter.Value.ToString());

			
			if ( objSqlParameter.Value != System.DBNull.Value )
			{
				APP_MVR_ID = Convert.ToInt32(objSqlParameter.Value);
			}
					
			objMvrInfo.APP_MVR_ID = APP_MVR_ID;

			objDataWrapper.ClearParameteres();
				
			if(TransactionLogRequired)
			{	
				//Sep 27,2005:Sumit:Following TransactLabel statement has been modified to point to correct address
				//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");					
				objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddMvrInformation.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objMvrInfo);
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	1;
				objTransactionInfo.APP_ID = objMvrInfo.APP_ID;
				objTransactionInfo.APP_VERSION_ID = objMvrInfo.APP_VERSION_ID;
				objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
				objTransactionInfo.RECORDED_BY		=	objMvrInfo.CREATED_BY;
				objTransactionInfo.TRANS_DESC		=	"MVR Information Record has been added from Quick quote";
				objTransactionInfo.CHANGE_XML		=	strTranXML;		
				
				
				//Executing the query
				objDataWrapper.ExecuteNonQuery(objTransactionInfo);
			}
				

			return 1;
			
		}
		#region Accident --> Prior Loss --> Watercraft
		/// <summary>
		/// Accident --> Prior Loss --> Watercraft
		/// </summary>
		/// <param name="objMvrInfo"></param>
		/// <param name="objDataWrapper"></param>
		/// <returns></returns>
		public int SavePriorLossAccident(ClsMvrInfo objMvrInfo,DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_SavePriorLoss_Acord";
			DateTime	RecordDate		=	DateTime.Now;

			objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
			objDataWrapper.AddParameter("@MVR_AMOUNT",DefaultValues.GetDoubleNullFromNegative(objMvrInfo.MVR_AMOUNT));
			objDataWrapper.AddParameter("@MVR_DATE",objMvrInfo.MVR_DATE);
			objDataWrapper.AddParameter("@IS_ACTIVE","Y");
			objDataWrapper.AddParameter("@CREATED_BY",objMvrInfo.CREATED_BY);

			//int returnResult = 0;
				
			objDataWrapper.ExecuteNonQuery(strStoredProc);
				
			
			objDataWrapper.ClearParameteres();
				
			if(TransactionLogRequired)
			{	
				
				objMvrInfo.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel("application/Aspx/AddPriorPolicy.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objMvrInfo);
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	1;
				objTransactionInfo.APP_ID			=   objMvrInfo.APP_ID;
				objTransactionInfo.APP_VERSION_ID	=   objMvrInfo.APP_VERSION_ID;
				objTransactionInfo.CLIENT_ID		=   objMvrInfo.CUSTOMER_ID;
				objTransactionInfo.RECORDED_BY		=	objMvrInfo.CREATED_BY;
				objTransactionInfo.TRANS_DESC		=	"Prior Loss Information has been added from Quick quote";
				objTransactionInfo.CHANGE_XML		=	strTranXML;		
				
				
				//Executing the query
				objDataWrapper.ExecuteNonQuery(objTransactionInfo);
			}
				

			return 1;
			
		}
		#endregion
		#region Accident --> Prior Loss --> Auto and MotorCycle
		/// <summary>
		/// Accident --> Prior Loss --> Auto and MotorCycle
		/// </summary>
		/// <param name="objMvrInfo"></param>
		/// <param name="objDataWrapper"></param>
		/// <returns></returns>
		public int SavePriorLossAccidentVehicle(string app_lob,ClsMvrInfo objMvrInfo,DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_SavePriorLossVehicle_Acord";
			DateTime	RecordDate		=	DateTime.Now;

			objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
			objDataWrapper.AddParameter("@APP_ID",objMvrInfo.APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objMvrInfo.APP_VERSION_ID);
			objDataWrapper.AddParameter("@VIOLATION_CODE",objMvrInfo.VIOLATION_CODE);
			objDataWrapper.AddParameter("@MVR_AMOUNT",DefaultValues.GetDoubleNullFromNegative(objMvrInfo.MVR_AMOUNT));
			objDataWrapper.AddParameter("@MVR_DATE",objMvrInfo.MVR_DATE);
			objDataWrapper.AddParameter("@APP_LOB",app_lob);
			objDataWrapper.AddParameter("@IS_ACTIVE","Y");
			objDataWrapper.AddParameter("@CREATED_BY",objMvrInfo.CREATED_BY);

			objDataWrapper.ExecuteNonQuery(strStoredProc);
				
			
			objDataWrapper.ClearParameteres();
				
			if(TransactionLogRequired)
			{	
				
				objMvrInfo.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel("application/Aspx/AddPriorPolicy.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objMvrInfo);
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	1;
				objTransactionInfo.APP_ID			=   objMvrInfo.APP_ID;
				objTransactionInfo.APP_VERSION_ID	=   objMvrInfo.APP_VERSION_ID;
				objTransactionInfo.CLIENT_ID		=   objMvrInfo.CUSTOMER_ID;
				objTransactionInfo.RECORDED_BY		=	objMvrInfo.CREATED_BY;
				objTransactionInfo.TRANS_DESC		=	"Prior Loss Information has been added from Quick quote";
				objTransactionInfo.CHANGE_XML		=	strTranXML;		
				
				
				//Executing the query
				objDataWrapper.ExecuteNonQuery(objTransactionInfo);
			}
				

			return 1;
			
		}
		#endregion

		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objMvrInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int SaveViolations(ClsMvrInfo objMvrInfo,DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_SaveAPP_MVR_INFORMATION_ACORD";
			DateTime	RecordDate		=	DateTime.Now;
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objMvrInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objMvrInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VIOLATION_ID",objMvrInfo.VIOLATION_ID);
				objDataWrapper.AddParameter("@VIOLATION_CODE",objMvrInfo.VIOLATION_CODE);
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@MVR_AMOUNT",DefaultValues.GetDoubleNullFromNegative(objMvrInfo.MVR_AMOUNT));
				objDataWrapper.AddParameter("@MVR_DEATH",objMvrInfo.MVR_DEATH);
				objDataWrapper.AddParameter("@MVR_DATE",objMvrInfo.MVR_DATE);
				objDataWrapper.AddParameter("@POINTS_ASSIGNED",objMvrInfo.POINTS_ASSIGNED);

				objDataWrapper.AddParameter("@IS_ACTIVE","Y");

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@APP_MVR_ID",objMvrInfo.APP_MVR_ID,SqlDbType.Int,ParameterDirection.Output);

				//int returnResult = 0;
				
				objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				int APP_MVR_ID = -1;//int.Parse(objSqlParameter.Value.ToString());

			
				if ( objSqlParameter.Value != System.DBNull.Value )
				{
					APP_MVR_ID = Convert.ToInt32(objSqlParameter.Value);
				}
					
				objMvrInfo.APP_MVR_ID = APP_MVR_ID;

				objDataWrapper.ClearParameteres();
				
				if(TransactionLogRequired)
				{	
					//Sep 27,2005:Sumit:Following TransactLabel statement has been modified to point to correct address
					//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");					
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddMvrInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objMvrInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objMvrInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objMvrInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objMvrInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"MVR Information Record has been added from Quick quote";
					objTransactionInfo.CHANGE_XML		=	strTranXML;		
				
				
					//Executing the query
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				

				return 1;
			
		}
		

		public static string GetDriverDOB(int customerID,int appID,int appVersionID,int driverId)
		{
			string	strStoredProc =	"Proc_GetDriverDOB";
			string dob="";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@DRIVER_ID",driverId);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			if(ds.Tables[0].Rows.Count > 0)
			{
				dob=ds.Tables[0].Rows[0][0].ToString();
			}			
			return dob;
		
		}
		public static string GetWaterDriverDOB(int customerID,int appID,int appVersionID,int driverId)
		{
			string	strStoredProc =	"Proc_GetWaterDriverDOB";
			string dob="";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@DRIVER_ID",driverId);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			if(ds.Tables[0].Rows.Count > 0)
			{
				dob=ds.Tables[0].Rows[0][0].ToString();
			}			
			return dob;
		
		}
		public static string GetUmbDriverDOB(int customerID,int appID,int appVersionID,int driverId)
		{
			string	strStoredProc =	"Proc_GetUmbDriverDOB";
			string dob="";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@DRIVER_ID",driverId);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			if(ds.Tables[0].Rows.Count > 0)
			{
				dob=ds.Tables[0].Rows[0][0].ToString();
			}			
			return dob;
		
		}

		#region ACTIVATE/DEACTIVATE
		public void ActivateDeactivatePolMVR(int CustomerId, int AppId, int AppVersionId, int intDriverId, string strStatus,int POL_MVR_ID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			string strStoredProc = "Proc_ActivateDeactivatePOL_MVR_INFORMATION";
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
				objDataWrapper.AddParameter("@POL_ID",AppId);
				objDataWrapper.AddParameter("@POL_VERSION_ID",AppVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID",intDriverId);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);
				objDataWrapper.AddParameter("@POL_MVR_ID",POL_MVR_ID);

				objDataWrapper.ExecuteNonQuery(strStoredProc);
				//Update the Vehicle Class : 
//				ClsDriverDetail objDriverDetail = new ClsDriverDetail();
//				objDriverDetail.UpdateVehicleClassPol(CustomerId,AppId,AppVersionId);
				ClsDriverDetail objDriverDetail = new ClsDriverDetail();
				objDataWrapper.ClearParameteres();
				objDriverDetail.UpdateVehicleClassPolNew(CustomerId,AppId,AppVersionId,objDataWrapper);
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				//End Update the Vehicle Class	
				
			}
			catch(Exception exc)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw (exc);
			}
			finally
			{
				objDataWrapper.Dispose();
			}
		}

		public void ActivateDeactivatePolWaterMVR(int CustomerId, int AppId, int AppVersionId, int intDriverId, string strStatus,int POL_MVR_ID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			string strStoredProc = "Proc_ActivateDeactivatePOL_WATER_MVR_INFORMATION";
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
				objDataWrapper.AddParameter("@POL_ID",AppId);
				objDataWrapper.AddParameter("@POL_VERSION_ID",AppVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID",intDriverId);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);
				objDataWrapper.AddParameter("@APP_WATER_MVR_ID",POL_MVR_ID);

				objDataWrapper.ExecuteNonQuery(strStoredProc);
				
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

		public void ActivateDeactivateMVR(string CustomerId, string AppId, string AppVersionId, string intDriverId, string strStatus,string AppMvrId)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			string strStoredProc = "Proc_ActivateDeactivateAPP_MVR_INFORMATION";
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
				objDataWrapper.AddParameter("@APP_ID",AppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID",intDriverId);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);
				objDataWrapper.AddParameter("@APP_MVR_ID",AppMvrId);

				objDataWrapper.ExecuteNonQuery(strStoredProc);
				
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

		public void ActivateDeactivateWaterMVR(string CustomerId, string AppId, string AppVersionId, string intDriverId, string strStatus,string APP_WATER_MVR_ID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			string strStoredProc = "Proc_ActivateDeactivateAPP_WATER_MVR_INFORMATION";
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
				objDataWrapper.AddParameter("@APP_ID",AppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID",intDriverId);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);
				objDataWrapper.AddParameter("@APP_WATER_MVR_ID",APP_WATER_MVR_ID);

				objDataWrapper.ExecuteNonQuery(strStoredProc);
				
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

		public void ActivateDeactivateWaterMVR(ClsMvrInfo objMvrInfo, string IS_ACTIVE,string strDriverName,string strDriverCode)
		{
			string		strStoredProc	=	"Proc_ActivateDeactivateAPP_WATER_MVR_INFORMATION";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objMvrInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objMvrInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",IS_ACTIVE);
				objDataWrapper.AddParameter("@APP_WATER_MVR_ID",objMvrInfo.APP_WATER_MVR_ID);				

				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					//Sep 27,2005:Sumit:Following TransactLabel statement has been modified to point to correct address
					//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");					
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddMvrInformation.aspx.resx");
					//SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					//string strTranXML = objBuilder.GetTransactionLogXML(objMvrInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objMvrInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objMvrInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					if(IS_ACTIVE.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	"MVR Information Record Has Been Activated";
					else 
						objTransactionInfo.TRANS_DESC		=	"MVR Information Record Has Been Deactivated";
					objTransactionInfo.CUSTOM_INFO		=	";Operator Name = " + strDriverName + ";Operator Code = " + strDriverCode + ";Violation = " + objMvrInfo.VIOLATION_DES;
					//objTransactionInfo.CHANGE_XML		=	strTranXML;		
					
					
					
					
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
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


		public void ActivateDeactivateUmbrellaMVR(ClsMvrInfo objMvrInfo, string IS_ACTIVE,string strDriverName,string strDriverCode)
		{
			string		strStoredProc	=	"Proc_ActivateDeactivateAPP_UMBRELLA_MVR_INFORMATION";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objMvrInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objMvrInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",IS_ACTIVE);
				objDataWrapper.AddParameter("@APP_UMB_MVR_ID",objMvrInfo.APP_UMB_MVR_ID);				

				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					//Sep 27,2005:Sumit:Following TransactLabel statement has been modified to point to correct address
					//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");					
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddMvrInformation.aspx.resx");
					//SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					//string strTranXML = objBuilder.GetTransactionLogXML(objMvrInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objMvrInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objMvrInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					if(IS_ACTIVE.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	"MVR Information Record Has Been Activated";
					else 
						objTransactionInfo.TRANS_DESC		=	"MVR Information Record Has Been Deactivated";
					objTransactionInfo.CUSTOM_INFO		=	";Driver/Operator Name = " + strDriverName + ";Driver/Operator Code = " + strDriverCode + ";Violation = " + objMvrInfo.VIOLATION_DES;
					//objTransactionInfo.CHANGE_XML		=	strTranXML;		
					
					
					
					
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
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

		public void ActivateDeactivateMVR(ClsMvrInfo objMvrInfo, string IS_ACTIVE,string strDriverName,string strDriverCode,string strCalledFrom)
		{
			string		strStoredProc	=	"Proc_ActivateDeactivateAPP_MVR_INFORMATION";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objMvrInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objMvrInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",IS_ACTIVE);
				objDataWrapper.AddParameter("@APP_MVR_ID",objMvrInfo.APP_MVR_ID);	
				//<002> <Start> Added by Ashwani on 9 Feb. 2006
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom);	
				//<002> <End> 
				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					//Sep 27,2005:Sumit:Following TransactLabel statement has been modified to point to correct address
					//objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cms.BusinessLayer.BlApplication/Cms.Model.Application.AddMvrInformation.resx");					
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddMvrInformation.aspx.resx");
					//SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					//string strTranXML = objBuilder.GetTransactionLogXML(objMvrInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objMvrInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objMvrInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					if(IS_ACTIVE.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	"MVR Information Record Has Been Activated";
					else 
						objTransactionInfo.TRANS_DESC		=	"MVR Information Record Has Been Deactivated";
					objTransactionInfo.CUSTOM_INFO		=	";Driver Name = " + strDriverName + ";Driver Code = " + strDriverCode + ";Violation = " + objMvrInfo.VIOLATION_DES;
					//objTransactionInfo.CHANGE_XML		=	strTranXML;		
					
					
					
					
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				objDataWrapper.ClearParameteres();				
				//Update the Vehicle Class : 
				ClsDriverDetail objDriverDetail = new ClsDriverDetail();
				//objDriverDetail.UpdateVehicleClass(objMvrInfo.CUSTOMER_ID,objMvrInfo.APP_ID ,objMvrInfo.APP_VERSION_ID);
				if(returnResult>0)
				{
					if(strCalledFrom.ToUpper()!="MOT")
					{
						objDriverDetail.UpdateVehicleClassNew(objMvrInfo.CUSTOMER_ID,objMvrInfo.APP_ID ,objMvrInfo.APP_VERSION_ID,objDataWrapper);
						
					}
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				}
				//End Update the Vehicle Class		
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
			
		}




		#region POLICY WATERCRAFT FUNCTIONS
		/// <summary>
		/// Fetch the Policy WaterCraft MVR Information.
		/// </summary>
		/// <param name="CUSTOMER_ID"></param>
		/// <param name="POLICY_ID"></param>
		/// <param name="POLICY_VERSION_ID"></param>
		/// <param name="DRIVER_ID"></param>
		/// <param name="APP_WATER_MVR_ID"></param>
		/// <returns></returns>
		public static string GetXmlForPolicyWaterCraft(int CUSTOMER_ID,int POLICY_ID,int POLICY_VERSION_ID,int DRIVER_ID,int APP_WATER_MVR_ID)
		{
			string strSql = "Proc_GetXMLPOL_WATERCRAFT_MVR_INFORMATION";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);			
			objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);			
			objDataWrapper.AddParameter("@POLICY_ID",POLICY_ID);			
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",POLICY_VERSION_ID);			
			objDataWrapper.AddParameter("@DRIVER_ID",DRIVER_ID);			
			objDataWrapper.AddParameter("@APP_WATER_MVR_ID",APP_WATER_MVR_ID);			

			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);

			if (objDataSet.Tables[0].Rows.Count>0)
			{
				return ClsCommon.GetXMLEncoded(objDataSet.Tables[0]);
				//return objDataSet.GetXml();
			}
			else
				return "";
		}
		public static string GetXmlForPolicyUmbrella(int CUSTOMER_ID,int POLICY_ID,int POLICY_VERSION_ID,int DRIVER_ID,int POL_UMB_MVR_ID)
		{
			string strSql = "Proc_GetXMLPOL_UMBRELLA_MVR_INFORMATION";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);			
			objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);			
			objDataWrapper.AddParameter("@POLICY_ID",POLICY_ID);			
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",POLICY_VERSION_ID);			
			objDataWrapper.AddParameter("@DRIVER_ID",DRIVER_ID);			
			objDataWrapper.AddParameter("@POL_UMB_MVR_ID",POL_UMB_MVR_ID);			

			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);

			if (objDataSet.Tables[0].Rows.Count>0)
			{
				return ClsCommon.GetXMLEncoded(objDataSet.Tables[0]);
				//return objDataSet.GetXml();
			}
			else
				return "";
		}
	
		/// <summary>
		/// Saves the Policy WaterCraft MVR Information.
		/// </summary>
		/// <param name="objMvrInfo"></param>
		/// <returns></returns>
		public int AddPolicyWaterCraft(Cms.Model.Policy.ClsPolicyAutoMVR objMvrInfo)
		{
			string		strStoredProc	=	"Proc_InsertPOL_WATERCRAFT_MVR_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objMvrInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objMvrInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@VIOLATION_ID",objMvrInfo.VIOLATION_ID);
				objDataWrapper.AddParameter("@VIOLATION_TYPE",objMvrInfo.VIOLATION_TYPE);				
				objDataWrapper.AddParameter("@MVR_AMOUNT",System.DBNull.Value);
				objDataWrapper.AddParameter("@MVR_DEATH",System.DBNull.Value);

				if(objMvrInfo.MVR_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@MVR_DATE",objMvrInfo.MVR_DATE);
				else
					objDataWrapper.AddParameter("@MVR_DATE",System.DBNull.Value);

				if(objMvrInfo.OCCURENCE_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@OCCURENCE_DATE",objMvrInfo.OCCURENCE_DATE);
				else
					objDataWrapper.AddParameter("@OCCURENCE_DATE",System.DBNull.Value);
				if(objMvrInfo.DETAILS != "")
					objDataWrapper.AddParameter("@DETAILS",objMvrInfo.DETAILS);
				else
					objDataWrapper.AddParameter("@DETAILS",System.DBNull.Value);

				if(objMvrInfo.POINTS_ASSIGNED < 100)
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",objMvrInfo.POINTS_ASSIGNED);
				else
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",System.DBNull.Value);

				if(objMvrInfo.ADJUST_VIOLATION_POINTS < 100)
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",objMvrInfo.ADJUST_VIOLATION_POINTS);
				else
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",System.DBNull.Value);

				objDataWrapper.AddParameter("@IS_ACTIVE","Y");
				objDataWrapper.AddParameter("@VERIFIED",objMvrInfo.VERIFIED);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@APP_WATER_MVR_ID",objMvrInfo.POL_WATER_MVR_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/Automobile/PolicyAutoMVR.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objMvrInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID = objMvrInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =  objMvrInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Policy MVR Information Record Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;		
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				int APP_WATER_MVR_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				if (APP_WATER_MVR_ID == -1)
				{
					return -1;
				}
				else
				{
					objMvrInfo.POL_WATER_MVR_ID = APP_WATER_MVR_ID;
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


		public int AddPolicyUmbrella(Cms.Model.Policy.ClsPolicyAutoMVR objMvrInfo)
		{
			string		strStoredProc	=	"Proc_InsertPOL_UMBRELLA_MVR_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objMvrInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objMvrInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@VIOLATION_ID",objMvrInfo.VIOLATION_ID);
				objDataWrapper.AddParameter("@VIOLATION_TYPE",objMvrInfo.VIOLATION_TYPE);				
				objDataWrapper.AddParameter("@MVR_AMOUNT",DefaultValues.GetDoubleNullFromNegative(objMvrInfo.MVR_AMOUNT));
				objDataWrapper.AddParameter("@MVR_DEATH",objMvrInfo.MVR_DEATH);
				objDataWrapper.AddParameter("@MVR_DATE",objMvrInfo.MVR_DATE);
				objDataWrapper.AddParameter("@IS_ACTIVE","Y");
				objDataWrapper.AddParameter("@VERIFIED",objMvrInfo.VERIFIED);
				objDataWrapper.AddParameter("@CREATED_BY",objMvrInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objMvrInfo.CREATED_DATETIME);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@POL_UMB_MVR_ID",objMvrInfo.POL_UMB_MVR_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/Automobile/PolicyAutoMVR.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objMvrInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID = objMvrInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =  objMvrInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Policy MVR Information Record Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;		
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				int POL_UMB_MVR_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				if (POL_UMB_MVR_ID == -1)
				{
					return -1;
				}
				else
				{
					objMvrInfo.POL_UMB_MVR_ID = POL_UMB_MVR_ID;
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

		/// <summary>
		/// Update the Policy WaterCraft MVR Information.
		/// </summary>
		/// <param name="objOldMvrInfo"></param>
		/// <param name="objMvrInfo"></param>
		/// <returns></returns>
		public int UpdatePolicyWaterCraft(Cms.Model.Policy.ClsPolicyAutoMVR objOldMvrInfo,Cms.Model.Policy.ClsPolicyAutoMVR objMvrInfo)
		{
			string		strStoredProc	=	"Proc_UpdatePOL_WATERCRAFT_MVR_INFORMATION";			
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objMvrInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objMvrInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@APP_WATER_MVR_ID",objMvrInfo.POL_WATER_MVR_ID);
				objDataWrapper.AddParameter("@VIOLATION_ID",objMvrInfo.VIOLATION_ID);
				objDataWrapper.AddParameter("@VIOLATION_TYPE",objMvrInfo.VIOLATION_TYPE);				
				objDataWrapper.AddParameter("@MVR_AMOUNT",System.DBNull.Value);
				objDataWrapper.AddParameter("@MVR_DEATH",System.DBNull.Value);
				if(objMvrInfo.MVR_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@MVR_DATE",objMvrInfo.MVR_DATE);
				else
					objDataWrapper.AddParameter("@MVR_DATE",System.DBNull.Value);

				if(objMvrInfo.OCCURENCE_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@OCCURENCE_DATE",objMvrInfo.OCCURENCE_DATE);
				else
					objDataWrapper.AddParameter("@OCCURENCE_DATE",System.DBNull.Value);
				if(objMvrInfo.DETAILS != "")
					objDataWrapper.AddParameter("@DETAILS",objMvrInfo.DETAILS);
				else
					objDataWrapper.AddParameter("@DETAILS",System.DBNull.Value);

				if(objMvrInfo.POINTS_ASSIGNED < 100)
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",objMvrInfo.POINTS_ASSIGNED);
				else
					objDataWrapper.AddParameter("@POINTS_ASSIGNED",System.DBNull.Value);

				if(objMvrInfo.ADJUST_VIOLATION_POINTS < 100)
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",objMvrInfo.ADJUST_VIOLATION_POINTS);
				else
					objDataWrapper.AddParameter("@ADJUST_VIOLATION_POINTS",System.DBNull.Value);

				objDataWrapper.AddParameter("@VERIFIED",objMvrInfo.VERIFIED);
				if(base.TransactionLogRequired) 
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies/aspx/Automobile/PolicyAutoMVR.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldMvrInfo,objMvrInfo);
					if(strTranXML=="" || strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					else
					{
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.POLICY_ID = objMvrInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID = objMvrInfo.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Policy MVR Information is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;

						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

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


		public int UpdatePolicyUmbrella(Cms.Model.Policy.ClsPolicyAutoMVR objOldMvrInfo,Cms.Model.Policy.ClsPolicyAutoMVR objMvrInfo)
		{
			string		strStoredProc	=	"Proc_UpdatePOL_UMBRELLA_MVR_INFORMATION";			
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objMvrInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objMvrInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@POL_UMB_MVR_ID",objMvrInfo.POL_UMB_MVR_ID);
				objDataWrapper.AddParameter("@VIOLATION_ID",objMvrInfo.VIOLATION_ID);
				objDataWrapper.AddParameter("@VIOLATION_TYPE",objMvrInfo.VIOLATION_TYPE);				
				objDataWrapper.AddParameter("@MVR_AMOUNT",DefaultValues.GetDoubleNullFromNegative(objMvrInfo.MVR_AMOUNT));
				objDataWrapper.AddParameter("@MVR_DEATH",objMvrInfo.MVR_DEATH);
				objDataWrapper.AddParameter("@MVR_DATE",objMvrInfo.MVR_DATE);		
				objDataWrapper.AddParameter("@VERIFIED",objMvrInfo.VERIFIED);
				objDataWrapper.AddParameter("@MODIFIED_BY",objMvrInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objMvrInfo.LAST_UPDATED_DATETIME);
				if(base.TransactionLogRequired) 
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies/aspx/Automobile/PolicyAutoMVR.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldMvrInfo,objMvrInfo);
					if(strTranXML=="" || strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					else
					{
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.POLICY_ID = objMvrInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID = objMvrInfo.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Policy MVR Information is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;

						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

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

		
		/// <summary>
		/// Delete the Policy WaterCraft MVR Information.
		/// </summary>
		/// <param name="objMvrInfo"></param>
		/// <returns></returns>
		public int DeletePolicyWaterCraft(Cms.Model.Policy.ClsPolicyAutoMVR objMvrInfo)
		{
			string		strStoredProc	=	"Proc_DeletePOL_WATERCRAFT_MVR_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);							
				objDataWrapper.AddParameter("@POLICY_ID",objMvrInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objMvrInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);				
				objDataWrapper.AddParameter("@APP_WATER_MVR_ID",objMvrInfo.POL_WATER_MVR_ID);	
						
				int returnResult = 0;
				if(TransactionLogRequired)
				{	
				
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/Automobile/PolicyAutoMVR.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID	    =	objMvrInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	objMvrInfo.POLICY_VERSION_ID;				
					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"Policy MVR Information Record Has Been Deleted";
					objTransactionInfo.CHANGE_XML		=	"";
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
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


		public int DeletePolicyUmbrella(Cms.Model.Policy.ClsPolicyAutoMVR objMvrInfo)
		{
			string		strStoredProc	=	"Proc_DeletePOL_UMBRELLA_MVR_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objMvrInfo.CUSTOMER_ID);							
				objDataWrapper.AddParameter("@POLICY_ID",objMvrInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objMvrInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DRIVER_ID",objMvrInfo.DRIVER_ID);				
				objDataWrapper.AddParameter("@POL_UMB_MVR_ID",objMvrInfo.POL_UMB_MVR_ID);	
						
				int returnResult = 0;
				if(TransactionLogRequired)
				{	
				
					objMvrInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/Automobile/PolicyAutoMVR.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID	    =	objMvrInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	objMvrInfo.POLICY_VERSION_ID;				
					objTransactionInfo.RECORDED_BY		=	objMvrInfo.MODIFIED_BY;
					objTransactionInfo.CLIENT_ID = objMvrInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"Policy MVR Information Record Has Been Deleted";
					objTransactionInfo.CHANGE_XML		=	"";
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
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
		/// Activate Deactivate the Policy WaterCraft MVR Information.
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <param name="PolicyID"></param>
		/// <param name="PolicyVersionID"></param>
		/// <param name="DriverID"></param>
		/// <param name="strStatus"></param>
		/// <param name="POL_WATER_MVR_ID"></param>
		public int ActivateDeactivatePolicyWaterCraft(int CustomerID, int PolicyID, int PolicyVersionID, int DriverID, string strStatus,int POL_WATER_MVR_ID)
		{
			int intRetVal;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			string strStoredProc = "Proc_ActivateDeactivatePOL_WATERCRAFT_MVR_INFORMATION";
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objDataWrapper.AddParameter("@POLICY_ID",PolicyID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID);
				objDataWrapper.AddParameter("@DRIVER_ID",DriverID);
				objDataWrapper.AddParameter("@APP_WATER_MVR_ID",POL_WATER_MVR_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);

				intRetVal=objDataWrapper.ExecuteNonQuery(strStoredProc);
				if(intRetVal>0)
				{
					return  1;
				}
				else
				{
					return -1;
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


		public int ActivateDeactivatePolicyUmbrella(int CustomerID, int PolicyID, int PolicyVersionID, int DriverID, string strStatus,int POL_UMB_MVR_ID)
		{
			int intRetVal;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			string strStoredProc = "Proc_ActivateDeactivatePOL_UMBRELLA_MVR_INFORMATION";
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objDataWrapper.AddParameter("@POLICY_ID",PolicyID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID);
				objDataWrapper.AddParameter("@DRIVER_ID",DriverID);
				objDataWrapper.AddParameter("@POL_UMB_MVR_ID",POL_UMB_MVR_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);

				intRetVal=objDataWrapper.ExecuteNonQuery(strStoredProc);
				if(intRetVal>0)
				{
					return  1;
				}
				else
				{
					return -1;
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

	}
}
