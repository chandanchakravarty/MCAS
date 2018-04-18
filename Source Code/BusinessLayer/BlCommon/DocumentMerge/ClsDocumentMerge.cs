using System;
using System.Data;
using Cms.DataLayer;
using Cms.Model;

namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsDocumentMerge.
	/// </summary>
	public class ClsDocumentMerge: Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		public ClsDocumentMerge()
		{
			
		}

		public string getTemplateName(string TemplateID)
		{
			DataSet DSTemplate = new DataSet();
			string StoredProcedure="Proc_DOC_GetTemplateInformation " + TemplateID;
			DSTemplate = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,CommandType.Text,StoredProcedure);
			if (DSTemplate.Tables[0].Rows.Count>0)
			{
				if (DSTemplate.Tables[0].Rows[0]["DISPLAYNAME"] !=DBNull.Value)
					return(DSTemplate.Tables[0].Rows[0]["DISPLAYNAME"].ToString());
				else
					return("");
			}
			else 
				return("");
		}
		public DataSet getTemplateInfo(string TemplateId,string UserId,string Mode)
		{
			DataSet DSTemplate = new DataSet();
			string StoredProcedure="Proc_DOC_GetTemplateInformation " + TemplateId + "," + UserId + ",'" + Mode + "'";
			DSTemplate = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,CommandType.Text,StoredProcedure);
			return(DSTemplate);
		}

		public DataSet GetLineOfBusinesses()
		{
			DataSet ldsLOB = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"SELECT LOB_ID,LOB_DESC FROM MNT_LOB_MASTER WHERE LOB_CODE NOT IN ('GENL') ORDER BY 2");
			return(ldsLOB);
		}

		public DataSet getUserDataSet()
		{
			DataSet ldsUser = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"SELECT USER_ID,USER_FNAME+' '+USER_LNAME USER_NAME FROM MNT_USER_LIST WHERE IS_ACTIVE='Y' ORDER BY 2");
			return(ldsUser);
		}

		public DataSet getTemplateTypeDataSet()
		{
            DataSet ldsLookup = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr, System.Data.CommandType.Text, "SELECT isnull(mlmm.LOOKUP_UNIQUE_ID,LV.LOOKUP_UNIQUE_ID)LOOKUP_UNIQUE_ID,isnull(mlmm.LOOKUP_VALUE_DESC,LV.LOOKUP_VALUE_DESC) as LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES LV left outer JOIN MNT_LOOKUP_TABLES LT ON LT.LOOKUP_ID=LV.LOOKUP_ID left outer join MNT_LOOKUP_VALUES_MULTILINGUAL mlmm on mlmm.LOOKUP_UNIQUE_ID=LV.LOOKUP_UNIQUE_ID and mlmm.LANG_ID=" + BlCommon.ClsCommon.BL_LANG_ID.ToString() + " WHERE LT.LOOKUP_NAME='DOCTYP' AND LV.IS_ACTIVE='Y' union SELECT isnull(mlmm.LOOKUP_UNIQUE_ID,LV.LOOKUP_UNIQUE_ID)LOOKUP_UNIQUE_ID,isnull(mlmm.LOOKUP_VALUE_DESC,LV.LOOKUP_VALUE_DESC) as LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES LV left outer JOIN MNT_LOOKUP_TABLES LT ON LT.LOOKUP_ID=LV.LOOKUP_ID left outer join MNT_LOOKUP_VALUES_MULTILINGUAL mlmm on mlmm.LOOKUP_UNIQUE_ID=LV.LOOKUP_UNIQUE_ID and mlmm.LANG_ID=" + BlCommon.ClsCommon.BL_LANG_ID.ToString() + " WHERE LT.LOOKUP_NAME='ACPOST' AND LV.IS_ACTIVE='Y' and lookup_value_code='clm'  ORDER BY 2 ");
			return(ldsLookup);
            //SELECT LOOKUP_UNIQUE_ID,LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES LV INNER JOIN MNT_LOOKUP_TABLES LT ON LT.LOOKUP_ID=LV.LOOKUP_ID WHERE LT.LOOKUP_NAME='DOCTYP' AND LV.IS_ACTIVE='Y' union SELECT LOOKUP_UNIQUE_ID,LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES LV INNER JOIN MNT_LOOKUP_TABLES LT ON LT.LOOKUP_ID=LV.LOOKUP_ID WHERE LT.LOOKUP_NAME='ACPOST' AND LV.IS_ACTIVE='Y' and lookup_value_code='clm'  ORDER BY 2 
		}

		public string InsertUpdateTemplateInfo(string[] strTemplateInfo)
		{
			DataSet DSTemplate = new DataSet();
			string StoredProcedure="Proc_DOC_InsertUpdateTemplateInfo " + strTemplateInfo[0] + ",'" + strTemplateInfo[1].Replace("'","''") + "','" + strTemplateInfo[2].Replace("'","''") + "'," + strTemplateInfo[3] + "," + strTemplateInfo[4] + "," + strTemplateInfo[5] + "," + strTemplateInfo[6] + "," + strTemplateInfo[7] + "";
			DSTemplate = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,CommandType.Text,StoredProcedure);
			if (DSTemplate.Tables[0].Rows.Count>0)
			{
				if (DSTemplate.Tables[0].Rows[0][0] !=DBNull.Value)
					return(DSTemplate.Tables[0].Rows[0][0].ToString());
				else
					return("");
			}
			else 
				return("");
		}

		public void DeleteActivateDeactivateTemplate(string TemplateId,string Activity)
		{
			string StoredProcedure="Proc_DOC_DeleteActivateDeactivateTemplate " + TemplateId + ",'" + Activity + "'";
			
			DataLayer.DataWrapper.ExecuteNonQuery(ClsCommon.ConnStr,CommandType.Text,StoredProcedure);
		}

		public string InsertUpdateTransactionLog(string MergeID, string OldTransId, string TransactionId,string TransDesc)
		{
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo;
			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

			DataSet DsMerge = new DataSet();
			
			if (OldTransId=="0" && MergeID!="0")
			{
				DsMerge = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,CommandType.Text,"Proc_DOC_GetTemplateMergeInfo '" + MergeID.Trim().ToUpper() + "'");
			}
			else
			{
				objWrapper.AddParameter("@TRANS_ID",(object)OldTransId,SqlDbType.Int);
				DataSet DsTrans = new DataSet();
				DsMerge = objWrapper.ExecuteDataSet("Proc_DOC_GetTransactionDetails");
			}
			
			if (DsMerge.Tables[0].Rows.Count>0)
			{
				objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();     
				
				objTransactionInfo.RECORDED_BY  = int.Parse(DsMerge.Tables[0].Rows[0]["USER_ID"].ToString());
				objTransactionInfo.CLIENT_ID  = int.Parse(DsMerge.Tables[0].Rows[0]["CLIENT_ID"].ToString());
				
				if (DsMerge.Tables[0].Rows[0]["POLICY_ID"]!=DBNull.Value)
				{
					objTransactionInfo.POLICY_ID = int.Parse(DsMerge.Tables[0].Rows[0]["POLICY_ID"].ToString());
					objTransactionInfo.POLICY_VER_TRACKING_ID = int.Parse(DsMerge.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
					objTransactionInfo.TRANS_TYPE_ID = 90;
				}
				else if (DsMerge.Tables[0].Rows[0]["APP_ID"]!=DBNull.Value)
				{
					objTransactionInfo.APP_ID  = int.Parse(DsMerge.Tables[0].Rows[0]["APP_ID"].ToString());
					objTransactionInfo.APP_VERSION_ID  = int.Parse(DsMerge.Tables[0].Rows[0]["APP_VERSION_ID"].ToString());
					objTransactionInfo.TRANS_TYPE_ID = 89;
				}				
				else 
					objTransactionInfo.TRANS_TYPE_ID = 88;

				objTransactionInfo.TRANS_DESC  = TransDesc;
				objWrapper.ExecuteNonQuery(objTransactionInfo);
				TransactionId = objTransactionInfo.TransID.ToString();

				//TransDesc = TransDesc + "<br><br>To view generated document <a href=\"#\" onclick=\"javascript:self.location='../DocumentMerge/TemplateLoad.aspx?Mode=TRANS&TransId=" + TransactionId + "'\">click here</a>.";
				TransDesc = TransDesc + "<br><br>To view generated document <a href=\"javascript:function Open(){return false;}\" onclick=\"javascript:window.open('../DocumentMerge/TemplateLoad.aspx?Mode=TRANS&TransId=" + TransactionId + "')\">click here</a>.";

				objWrapper.ClearParameteres();
				objWrapper.AddParameter("@TRANS_ID",(object)TransactionId,SqlDbType.Int);
				objWrapper.AddParameter("@TRANSDESC",(object)TransDesc,SqlDbType.Text);
				objWrapper.ExecuteNonQuery("Proc_DOC_UpdateTransactionDesc");	
				objWrapper.ClearParameteres();
				//Following block added by Shailja on 03/13/2007 for Diary Item Creation(#1195)
				if (OldTransId=="0" && MergeID!="0" && (DsMerge.Tables[0].Rows[0]["DIARY_ITEM_REQ"].ToString()=="Y"))
				{
					try
					{
						ClsDiary objDiary = new ClsDiary();
						objDiary.TransactionLogRequired = true;		
		
						//Insert into Diary
						Cms.Model.Diary.TodolistInfo objToDoListInfo = new Cms.Model.Diary.TodolistInfo();						
						objToDoListInfo.SUBJECTLINE = DsMerge.Tables[0].Rows[0]["DISPLAYNAME"].ToString();
						objToDoListInfo.NOTE = TransDesc;
						//objToDoListInfo.STARTTIME = DateTime.Parse(DsMerge.Tables[0].Rows[0]["FOLLOW_UP_DATE"].ToString());//Done for Itrack Issue 5198 on 19 May 2009
						//objToDoListInfo.ENDTIME = DateTime.Parse(DsMerge.Tables[0].Rows[0]["FOLLOW_UP_DATE"].ToString());//Done for Itrack Issue 5198 on 19 May 2009
						objToDoListInfo.RECDATE = System.DateTime.Now;
						//objToDoListInfo.TOUSERID = int.Parse(DsMerge.Tables[0].Rows[0]["USER_ID"].ToString());
						objToDoListInfo.TOUSERID = int.Parse(DsMerge.Tables[0].Rows[0]["DIARY_ITEM_TO"].ToString());//Done for Itrack Issue 4846 on 4 June 2009	
						objToDoListInfo.CREATED_DATETIME = System.DateTime.Now;
						objToDoListInfo.FOLLOWUPDATE = DateTime.Parse(DsMerge.Tables[0].Rows[0]["FOLLOW_UP_DATE"].ToString());	
						objToDoListInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;
						objToDoListInfo.CUSTOMER_ID = int.Parse(DsMerge.Tables[0].Rows[0]["CLIENT_ID"].ToString());
						objToDoListInfo.FROMUSERID = int.Parse(DsMerge.Tables[0].Rows[0]["USER_ID"].ToString());
						objToDoListInfo.LISTTYPEID = 27 ; 
						if(int.Parse(DsMerge.Tables[0].Rows[0]["POLICY_ID"].ToString())!=0)
						{
							objToDoListInfo.POLICY_ID = int.Parse(DsMerge.Tables[0].Rows[0]["POLICY_ID"].ToString());
							objToDoListInfo.POLICY_VERSION_ID = int.Parse(DsMerge.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
							//Commented by Anurag Verma on 20/03/2007 as these properties are removed
							//objToDoListInfo.POLICYCLIENTID = int.Parse(DsMerge.Tables[0].Rows[0]["CLIENT_ID"].ToString());
							//objToDoListInfo.POLICYID = int.Parse(DsMerge.Tables[0].Rows[0]["POLICY_ID"].ToString());
							//objToDoListInfo.POLICYVERSION  = int.Parse(DsMerge.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

						}
						if(int.Parse(DsMerge.Tables[0].Rows[0]["APP_ID"].ToString())!=0)
						{
							objToDoListInfo.APP_ID = int.Parse(DsMerge.Tables[0].Rows[0]["APP_ID"].ToString());
							objToDoListInfo.APP_VERSION_ID = int.Parse(DsMerge.Tables[0].Rows[0]["APP_VERSION_ID"].ToString());
						}
						objToDoListInfo.PRIORITY = "M";
						objDiary.Add(objToDoListInfo,objWrapper);
						objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					}						
					catch(Exception ex)
					{
						throw(ex);
					}
					finally
					{ 
						if(objWrapper!=null)objWrapper.Dispose();
					}
				}
			}
			return(TransactionId);
		}
		
		public string GetMergeedTemplateTransactionPath(string TransactionId)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

			objWrapper.ClearParameteres();
			objWrapper.AddParameter("@TRANS_ID",(object)TransactionId,SqlDbType.Int);
			DataSet DsTrans = new DataSet();
			DsTrans = objWrapper.ExecuteDataSet("Proc_DOC_GetTransactionDetails");
			
			if (DsTrans.Tables[0].Rows.Count > 0)
				return(DsTrans.Tables[0].Rows[0]["AGENCY_CODE"].ToString().Trim() + "\\" + DsTrans.Tables[0].Rows[0]["CLIENT_ID"].ToString().Trim() + "\\");
			else
				return("");
		}

		public DataSet GetTemplates(string LetterType,string Lob,string Agency)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

			objWrapper.ClearParameteres();
			objWrapper.AddParameter("@LETTERTYPE",(object)LetterType,SqlDbType.NVarChar);
			objWrapper.AddParameter("@LOB",(object)Lob,SqlDbType.Int);
			objWrapper.AddParameter("@AGENCY",(object)Agency,SqlDbType.Int);
			DataSet DsTrans = new DataSet();
			DsTrans = objWrapper.ExecuteDataSet("Proc_DOC_GetTemplates");
			
			return(DsTrans);
		}

		public string InsertUpdateSendDocument(string[] strMergeInfo)
		{
			
			DataSet DSDocument = new DataSet();
			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
			objWrapper.ClearParameteres();
			objWrapper.AddParameter("@MERGE_ID",(object)strMergeInfo[0],SqlDbType.Int);
			objWrapper.AddParameter("@TEMPLATE_ID",(object)strMergeInfo[1],SqlDbType.Int);
			objWrapper.AddParameter("@CLIENT_ID",(object)strMergeInfo[2],SqlDbType.Int);
			objWrapper.AddParameter("@APP_ID",(object)strMergeInfo[3],SqlDbType.Int);
			objWrapper.AddParameter("@APP_VERSION_ID",(object)strMergeInfo[4],SqlDbType.Int);
			objWrapper.AddParameter("@POLICY_ID",(object)strMergeInfo[5],SqlDbType.Int);
			objWrapper.AddParameter("@POLICY_VERSION_ID",(object)strMergeInfo[6],SqlDbType.Int);
			objWrapper.AddParameter("@USER_ID",(object)strMergeInfo[7],SqlDbType.Int);
			objWrapper.AddParameter("@MERGE_STATUS",(object)strMergeInfo[8],SqlDbType.NVarChar);
			//following two parameters added by Shailja on 03/13/2007 for #1195
			objWrapper.AddParameter("@DIARY_ITEM_REQ",(object)strMergeInfo[9],SqlDbType.Char);
			objWrapper.AddParameter("@FOLLOW_UP_DATE",(object)strMergeInfo[10],SqlDbType.DateTime);
			objWrapper.AddParameter("@CHECK_ID",(object)strMergeInfo[11],SqlDbType.Int);
			objWrapper.AddParameter("@APPLICANT_ID",(object)strMergeInfo[12],SqlDbType.Int);
			objWrapper.AddParameter("@HOLDER_ID",(object)strMergeInfo[13],SqlDbType.Int);
			objWrapper.AddParameter("@DIARY_ITEM_TO",(object)strMergeInfo[14],SqlDbType.Int);

			objWrapper.AddParameter("@CLAIM_ID",(object)strMergeInfo[15],SqlDbType.Int);
			objWrapper.AddParameter("@PARTY_ID",(object)strMergeInfo[16],SqlDbType.Int);

			DSDocument = objWrapper.ExecuteDataSet("Proc_DOC_InsertUpdateSendLetter");
			
			
			if (DSDocument.Tables[0].Rows.Count>0)
			{
				if (DSDocument.Tables[0].Rows[0][0] !=DBNull.Value)
				{
					//Added Transaction Log : June 15 2009
					if(TransactionLogRequired)
					{
						//objDriverDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/AddDriverDetails.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.RECORDED_BY		=	int.Parse(strMergeInfo[7].ToString());
						objTransactionInfo.APP_ID			=	int.Parse(strMergeInfo[3].ToString());
						objTransactionInfo.APP_VERSION_ID	=	int.Parse(strMergeInfo[4].ToString());
						objTransactionInfo.POLICY_ID		=   int.Parse(strMergeInfo[5].ToString());
						objTransactionInfo.POLICY_VER_TRACKING_ID = int.Parse(strMergeInfo[6].ToString());
						objTransactionInfo.CLIENT_ID		=	int.Parse(strMergeInfo[2].ToString());
						objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1687","");//"Document has been merged Successfully.";
						//objTransactionInfo.CHANGE_XML		=	strTranXML;
						//Executing the query
						objWrapper.ExecuteNonQuery(objTransactionInfo);
					}

					return(DSDocument.Tables[0].Rows[0][0].ToString());
				}
				else
					return("-1");
			}
			else 
				return("-1");
		}
		

		#region Add Diary Entry
		public string Diary(string strSubject,string strMessage,string[] strMergeInfo,int intCreatedBy)
		{
				ClsDiary objDiary = new ClsDiary();
				objDiary.TransactionLogRequired = true;		
				int returnresult;
				//Insert into Diary 
					Cms.Model.Diary.TodolistInfo objToDoListInfo = new Cms.Model.Diary.TodolistInfo();
					objToDoListInfo.SUBJECTLINE				=	strSubject;
					objToDoListInfo.NOTE					=	strMessage;				
					objToDoListInfo.RECDATE					=	System.DateTime.Now;				
					objToDoListInfo.CREATED_DATETIME		=	DateTime.Now;				
					objToDoListInfo.LAST_UPDATED_DATETIME	=	DateTime.Now;
					objToDoListInfo.CUSTOMER_ID				=	int.Parse(strMergeInfo[2].ToString());
					objToDoListInfo.FROMUSERID				=	intCreatedBy;
					objToDoListInfo.LISTTYPEID				=	(int)ClsDiary.enumDiaryType.COMMUNICATION_FOLLOW_UPS;   
					objToDoListInfo.MODULE_ID				=	(int)ClsDiary.enumModuleMaster.CUSTOMER;  
					objToDoListInfo.LISTOPEN				=	"Y";
					objToDoListInfo.SYSTEMFOLLOWUPID		=	-1;
					objToDoListInfo.APP_ID					=	int.Parse(strMergeInfo[3].ToString());
					objToDoListInfo.APP_VERSION_ID			=   int.Parse(strMergeInfo[4].ToString());
					objToDoListInfo.POLICY_ID				=	int.Parse(strMergeInfo[5].ToString());
					objToDoListInfo.POLICY_VERSION_ID		=	int.Parse(strMergeInfo[6].ToString());
					objToDoListInfo.RECDATE					=	System.DateTime.Now;
					objToDoListInfo.FOLLOWUPDATE			=  Convert.ToDateTime((object)strMergeInfo[0]);
					objToDoListInfo.LAST_UPDATED_DATETIME	=	DateTime.Now;
					objToDoListInfo.TOUSERID				=	int.Parse(strMergeInfo[1].ToString());
					objToDoListInfo.PRIORITY				=	"M";
					objToDoListInfo.RECORDED_BY				=	intCreatedBy;
					returnresult = objDiary.Add(objToDoListInfo);
					if(returnresult != -1)
							return("1");
					else
						return("-1");
		}
		#endregion
		public DataSet GetCustomerAppPolicyValues(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, int APP_ID, int APP_VERSION_ID, string CalledFor)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

			objWrapper.ClearParameteres();
			objWrapper.AddParameter("@CUSTOMER_ID",(object)CUSTOMER_ID,SqlDbType.Int);
			objWrapper.AddParameter("@APP_ID",(object)APP_ID,SqlDbType.Int);
			objWrapper.AddParameter("@APP_VERSION_ID",(object)APP_VERSION_ID,SqlDbType.Int);
			objWrapper.AddParameter("@POLICY_ID",(object)POLICY_ID,SqlDbType.Int);
			objWrapper.AddParameter("@POLICY_VERSION_ID",(object)POLICY_VERSION_ID,SqlDbType.Int);
			objWrapper.AddParameter("@CalledFor",(object)CalledFor,SqlDbType.NVarChar);
			DataSet DsTrans = new DataSet();
			DsTrans = objWrapper.ExecuteDataSet("Proc_GetInformationForLetter");
			
			return(DsTrans);
		}

		/// <summary>
		/// To Get Claim Information when Send Document Called from Claim
		/// Itrack 5402
		/// </summary>
		/// <param name="claim_id"></param>
		/// <returns></returns>
		public DataSet GetClaimInfoDocMerge(int claim_id)
		{
			DataSet DsTrans = new DataSet();
			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

			try
			{
				objWrapper.ClearParameteres();
				objWrapper.AddParameter("@CLAIM_ID",(object)claim_id,SqlDbType.Int);
				
				DsTrans = objWrapper.ExecuteDataSet("Proc_GetClaimInfoDocMerge");
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{ 
				if(objWrapper!=null)objWrapper.Dispose();
			}
			return  DsTrans;
		}
		//Get Template Path for Mail attachment:
		public DataSet GetTemplatePath(int mergeID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

			objWrapper.ClearParameteres();
			objWrapper.AddParameter("@MERGE_ID",mergeID);
			DataSet DsTempPath = new DataSet();
			DsTempPath = objWrapper.ExecuteDataSet("Proc_DOC_GetTemplatePath");
			
			return(DsTempPath);


		}

		public DataSet GetDocLetterDetails(int mergeId)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

			objWrapper.ClearParameteres();
			objWrapper.AddParameter("@MERGE_ID",mergeId);
			DataSet DsTempPath = new DataSet();
			DsTempPath = objWrapper.ExecuteDataSet("Proc_GetDocLetter_Details");
			
			return(DsTempPath);


		}
		
	}
}
