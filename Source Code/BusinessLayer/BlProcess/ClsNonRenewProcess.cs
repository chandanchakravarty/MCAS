/******************************************************************************************
<Author				: -  
<Start Date			: -	 
<End Date			: -	 
<Description		: -  
<Review Date		: - 
<Reviewed By		: - 	

<Modified By		: - Vijay Arora
<Modified Dated		: - 24-01-2006
<Modified Purpose	: - Implement the Start Process, Rollback Process and Complete Process

<Modified By		: - Pravesh K. Chandel
<Modified Dated		: -  feb 2007
<Modified Purpose	: - Comlete the Implement of Start Process, Rollback Process and Complete Process
*******************************************************************************************/ 

using System;
using System.Data;
using System.Data.SqlClient;
using Cms.CmsWeb; 
using Cms.Model.Policy.Process;
namespace Cms.BusinessLayer.BlProcess
{
	/// <summary>
	/// Summary description for ClsNonRenewProcess.
	/// </summary>
	public class ClsNonRenewProcess : Cms.BusinessLayer.BlProcess.ClsPolicyProcess
	{
		public ClsNonRenewProcess()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region overrided function
		/// <summary>
		/// Override the Write Transaction Log Property
		/// </summary>
		/// <returns></returns>
		protected override bool OnWriteTransactionLog()
		{
			return false;
		}
		protected override bool OnSetPolicyStatus()
		{
			return false;
		}
		protected override bool OnCheckProcessEligibility()
		{
			return false;
		}
		#endregion
/// <summary>
/// Prepare Process object for Non Renewal Process and Process started Automatic
/// </summary>
/// <returns></returns>
		public bool PrepareProcessObjectAutoStart(int intCustomer_ID,int intPolicy_ID,int intPolicy_version_ID,string  CalledFor,Cms.Model.Policy.Process.ClsProcessInfo objProcess)
		{
			
			
			try
			{
				//Cms.Model.Policy.Process.ClsProcessInfo objProcess= new Cms.Model.Policy.Process.ClsProcessInfo();
				//
				base.BeginTransaction();
				objProcess.CUSTOMER_ID = intCustomer_ID;
				objProcess.POLICY_ID = intPolicy_ID ;
				objProcess.POLICY_VERSION_ID = intPolicy_version_ID ;
				objProcess.NEW_CUSTOMER_ID  = intCustomer_ID ;
				objProcess.NEW_POLICY_ID = intPolicy_ID ;
				objProcess.NEW_POLICY_VERSION_ID = intPolicy_version_ID;
				objProcess.POLICY_CURRENT_STATUS = GetPolicyStatus(intCustomer_ID,intPolicy_ID,intPolicy_version_ID);
				objProcess.POLICY_PREVIOUS_STATUS = GetPolicyStatus(intCustomer_ID,intPolicy_ID,intPolicy_version_ID);
	
				objProcess.PROCESS_ID = POLICY_NON_RENEWAL_PROCESS;
				Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGen = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();    
				DataSet dsPolicy=objGen.GetPolicyDataSet(intCustomer_ID,intPolicy_ID,intPolicy_version_ID);  
				
				if(dsPolicy.Tables[0].Rows[0]["APP_EXPIRATION_DATE"]!=System.DBNull.Value )
				{
                    objProcess.EFFECTIVE_DATETIME = ConvertToDate(dsPolicy.Tables[0].Rows[0]["EXPIRATION_DATE"].ToString());  // (Policy_ExpiryDate); //Policy Expiry date
					//Default time -
					objProcess.EFFECTIVE_DATETIME = new DateTime(objProcess.EFFECTIVE_DATETIME.Year, objProcess.EFFECTIVE_DATETIME.Month, objProcess.EFFECTIVE_DATETIME.Day,12,01, 0);
				}
				objProcess.LOB_ID =int.Parse(dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString());
				if (CalledFor=="AGENCY")
				{
					//objProcess.REASON=0; //'Agent/Company Termination (Non Renewal)'
					if(objProcess.LOB_ID== ClsRenewalProcess.POLICY_LOB_HOME    || objProcess.LOB_ID==ClsRenewalProcess.POLICY_LOB_RENT)
						objProcess.REASON = 13049; 
					else if(objProcess.LOB_ID==ClsRenewalProcess.POLICY_LOB_BOAT)
						objProcess.REASON = 13050; 
					else if(objProcess.LOB_ID==ClsRenewalProcess.POLICY_LOB_AUTO  || objProcess.LOB_ID==ClsRenewalProcess.POLICY_LOB_CYCL)
						objProcess.REASON = 13051; 
					else if(objProcess.LOB_ID==ClsRenewalProcess.POLICY_LOB_UMB)
						objProcess.REASON = 13053; 
					else 
						objProcess.REASON = 13052; 
					if (base.AgenyNotificationVerification(intCustomer_ID,intPolicy_ID,intPolicy_version_ID)==1)
					{
						objProcess.CANCELLATION_TYPE = int.Parse(((int)clsprocess.enumPROC_CANCEL_TYPE.AGENCY_TERMINATE_NOTIFICATION).ToString());//13027;//13026  Agency Terminated/Notification // 
						objProcess.INSURED = int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString());  //default
						objProcess.AGENCY_PRINT = int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL).ToString()) ;
						objProcess.ADD_INT=int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL).ToString());
					}
					else
					{
						objProcess.CANCELLATION_TYPE = int.Parse(((int)clsprocess.enumPROC_CANCEL_TYPE.AGENCY_TERMINATE_NO_NOTIFICATION).ToString()); ;// 13027 'Agency Terminated/No Notification'
						objProcess.INSURED = int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.NOT_REQUIRED).ToString());  //default
						objProcess.AGENCY_PRINT = int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.NOT_REQUIRED).ToString()) ;
						objProcess.ADD_INT=int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.NOT_REQUIRED).ToString());
					}
					objProcess.CANCELLATION_OPTION =11995 ;//Flat
				
				}
				else
				{
					if(dsPolicy.Tables[0].Rows[0]["NOT_RENEW_REASON"]!=System.DBNull.Value) 
						objProcess.REASON = int.Parse(dsPolicy.Tables[0].Rows[0]["NOT_RENEW_REASON"].ToString())   ;    //Non Renewal / Underwriting Reason . Pull the Reason Description from the Renewal Instruction Tab - Non Renewal Details
					if (objProcess.REASON ==0) ////Non Renewal / Underwriting Reason default
					{
						if(objProcess.LOB_ID== ClsRenewalProcess.POLICY_LOB_HOME    || objProcess.LOB_ID==ClsRenewalProcess.POLICY_LOB_RENT)
							objProcess.REASON= 13021; 
						else if(objProcess.LOB_ID==ClsRenewalProcess.POLICY_LOB_BOAT)
							objProcess.REASON = 13022; 
						else if(objProcess.LOB_ID==ClsRenewalProcess.POLICY_LOB_AUTO  || objProcess.LOB_ID==ClsRenewalProcess.POLICY_LOB_CYCL)
							objProcess.REASON = 13023; 
						else if(objProcess.LOB_ID==ClsRenewalProcess.POLICY_LOB_UMB)
							objProcess.REASON = 13025; 
						else 
							objProcess.REASON = 13024; 
					}
					objProcess.OTHER_REASON= dsPolicy.Tables[0].Rows[0]["NOT_RENEW_REASON_DESC"].ToString();
					objProcess.CANCELLATION_TYPE = int.Parse(((int)clsprocess.enumPROC_CANCEL_TYPE.NON_RENEWAL).ToString()); //"11991">Non Renewal
					objProcess.CANCELLATION_OPTION =13028;//NOt Applicable
					objProcess.INSURED = int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString());  //default
					objProcess.AGENCY_PRINT = int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL).ToString()) ;
					objProcess.ADD_INT=int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL).ToString());
	
				}
				if(dsPolicy.Tables[0].Rows[0]["UNDERWRITER"]!=System.DBNull.Value )
				{
					objProcess.UNDERWRITER  = int.Parse(dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString());
				}
				//objProcess.UNDERWRITER =0;        //Automatic System Process
				//objProcess.LOB_ID =int.Parse(dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString());
				dsPolicy.Clear();
				dsPolicy.Dispose(); 
				
				objProcess.PRINTING_OPTIONS = (int)(cmsbase.enumYESNO_LOOKUP_CODE.NO);
				if(IsEODProcess)
				{
					objProcess.CREATED_BY = objProcess.COMPLETED_BY = EODUserID;
				}
				else
				{
					objProcess.CREATED_BY = objProcess.COMPLETED_BY = int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());  //Automatic System Process
				}
				objProcess.CREATED_DATETIME = objProcess.COMPLETED_DATETIME = System.DateTime.Now;
				objProcess.SEND_ALL = (int)(cmsbase.enumYESNO_LOOKUP_CODE.YES);
				//
				objProcess.PROCESS_STATUS = ClsPolicyProcess.PROCESS_STATUS_PENDING;
				//AddProcessInformation(objProcess);
				if (this.StartProcess(objProcess)==false)
					return false;
				else
				{
					//Ravindra(10-2-2007)
					//If from EOD Renewal thread, Commit Non Renewal
					if(IsEODProcess)
					{
						this.CommitProcess(objProcess);
					}
					return true;
				}
			}
			catch(Exception ex)
			{
				//return false;
				throw( new Exception("Error occured while Preparing Non Renewal Process \n" + ex.Message));

			}
		}
		

		/// <summary>
		/// Override the Start Process Method
		/// </summary>
		/// <param name="objProcessInfo"></param>
		/// <returns></returns>
		public override bool StartProcess(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo)
		{
			//Calling the base class start process methos which will
			//insert the record in POL_POLICY_PROCESS table
			//and will do the transaction log entry
			//Reseting the transaction descrition text
			TransactionDescription = new System.Text.StringBuilder();
			try
			{
				
				base.BeginTransaction();
				objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_NON_RENEWAL_PROCESS;
				//Checking the eligibility
				if (base.CheckProcessEligibility(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.PROCESS_ID) != 1)
				{
					base.RollbackTransaction();
					return false;
				}
				//creating New version
                int intNewVersion = 0;
                string intNewDispVersion="";
					CreatePolicyNewVersion(objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID,objProcessInfo.CUSTOMER_ID,objProcessInfo.CREATED_BY.ToString(),out intNewVersion,out intNewDispVersion);     
					objProcessInfo.NEW_POLICY_VERSION_ID =intNewVersion;
                    TransactionDescription.Append(Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1476","") +intNewDispVersion + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1477","") );//"New version ("//") of policy has been created.;"
				string  strPolicyStatusDesc="",strNewStatus="";
				//Updating the status of new version of policy
				SetPolicyStatus(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID,objProcessInfo.PROCESS_ID,out strPolicyStatusDesc,out strNewStatus);
				objProcessInfo.POLICY_CURRENT_STATUS =strNewStatus;
                TransactionDescription.Append(Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1339","")+strPolicyStatusDesc + ".;");//"Policy status has been updated to " 
				bool retval = base.StartProcess (objProcessInfo);
				if (retval==true)
				{
						WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, 
						GetTransactionLogDesc(objProcessInfo.PROCESS_ID) , objProcessInfo.COMPLETED_BY, TransactionDescription.ToString(),TransactionChangeXML.ToString());
					base.CommitTransaction();
				}
				else
				{
					base.RollbackTransaction();
				}
				return retval;
			}
			catch (Exception objExp)
			{
				base.RollbackTransaction();
				throw(new Exception("Error Occured while starting the process.\n" + objExp.Message));
			}
		}

		/// <summary>
		/// Override the base commit process method.
		/// </summary>
		/// <param name="objProcessInfo"></param>
		/// <returns></returns>
		public override bool CommitProcess(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo)
		{
			try
			{
				base.BeginTransaction();
				ClsPolicyErrMsg.strMessage="";
				TransactionDescription.Length=0;
				objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_NON_RENEWAL_PROCESS;
				//Updating the status of policy on which process has been launched.
				string PolicyStatusDesc,NewPolicyStatus;
				if(objProcessInfo.EFFECTIVE_DATETIME.Date  > System.DateTime.Now.Date)
				{
					SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,objProcessInfo.PROCESS_ID,POLICY_STATUS_MARKED_NONRENEW);
					PolicyStatusDesc="Marked for Non-Renewal";
					NewPolicyStatus=POLICY_STATUS_MARKED_NONRENEW;

				}
				else
				{
					SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,objProcessInfo.PROCESS_ID, out PolicyStatusDesc, out NewPolicyStatus);
				}

                TransactionDescription.Append("\n "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1339","") + PolicyStatusDesc + ".;");//Policy Status has been updated to
				if(NewPolicyStatus!="")
					objProcessInfo.POLICY_CURRENT_STATUS = NewPolicyStatus;
				bool retval = base.CommitProcess (objProcessInfo,"NEWVERSION");
				if (retval)
                {
                    //Commented By Lalit April 21,2011
                    //No requirement come till now
                    #region commneted code  Genrate Non renewal Notice
                    /*
                    #region Genrate Non renewal Notice
                    int intNotices=this.generateNonRenewalNotices(objProcessInfo);
                    #endregion
                    #region Add Print Jobs for Non Renewal Notice
                    //					ClsPrintJobsInfo objPrintJobsInfo = GetPrintJobsValues(objProcessInfo,"NEWVERSION");
//					string stateCode=new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().SetStateCode("POLICY",objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID,objProcessInfo.CUSTOMER_ID); 
//					string AgencyCode = GetPDFAgencyCode(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID, "POLICY");
//					//objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode(System.Web.HttpContext.Current.Session["LOBString"].ToString(),stateCode,"Insured","NREN");
//					objPrintJobsInfo.URL_PATH = ClsPolicyProcess.PRINT_JOBS_URL_PATH + AgencyCode + "/" + objProcessInfo.CUSTOMER_ID.ToString() + "/" + "POLICY/NREN_NOTICE" + "/" + "final";
//					#region generating PDF files for Insured,agency and for Additional Interest
//					string fileNameInsured="";
//					string fileNameAgency="";
//					string strAddIntFile="";
//					string strCOMINFile="";
//					int tmpInsured = objProcessInfo.INSURED;
//					int tmpAgency  = objProcessInfo.AGENCY_PRINT;  	 
//					int intOldVersion=objProcessInfo.POLICY_VERSION_ID; 
//					objProcessInfo.POLICY_VERSION_ID=objProcessInfo.NEW_POLICY_VERSION_ID;
//					objProcessInfo.INSURED = 1;
//					objProcessInfo.AGENCY_PRINT = 0;
//					string strNoticeType="NREN_NOTICE";
//					if (objProcessInfo.CANCELLATION_TYPE==int.Parse(enumPROC_CANCEL_TYPE.AGENCY_TERMINATE_NO_NOTIFICATION.ToString()))
//							strNoticeType="NREN_NOTICE_NOTIFICATION";
//					else if(objProcessInfo.CANCELLATION_TYPE==int.Parse(enumPROC_CANCEL_TYPE.AGENCY_TERMINATE_NOTIFICATION.ToString()))
//							strNoticeType="NREN_NOTICE_NO_NOTIFICATION";
//					else
//							strNoticeType="NREN_NOTICE";
//					fileNameInsured=GeneratePDF(objProcessInfo,strNoticeType);
//					objProcessInfo.INSURED = 0;
//					objProcessInfo.AGENCY_PRINT = 1;
//					fileNameAgency=GeneratePDF(objProcessInfo,strNoticeType);
//					strAddIntFile=GeneratePDF(objProcessInfo,"NREN_NOTICE_ADDLINT");
//					strCOMINFile=GeneratePDF(objProcessInfo,"NREN_NOTICE_CERT_MAIL");
//					#endregion
//					#region  //adding Entry in print job table for customer/insured
//					
//					 objProcessInfo.INSURED=tmpInsured ;
//					 objProcessInfo.AGENCY_PRINT=tmpAgency;  	 
//					//added by pravesh adding printing job entry
//					//ClsPrintJobsInfo objPrintJobsInfo = GetPrintJobsValues(objProcessInfo);
//					if (objProcessInfo.PRINTING_OPTIONS ==(int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.NO))
//					{
//						//adding entry in print job for Insured
//						if(objProcessInfo.INSURED  == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL).ToString()) ||objProcessInfo.INSURED  == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString()) )
//						{
//							objPrintJobsInfo.ONDEMAND_FLAG ="Y";
//							objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER;
//							objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode(System.Web.HttpContext.Current.Session["LOBString"].ToString(),stateCode,"Insured","NREN");
//							objPrintJobsInfo.FILE_NAME =fileNameInsured;
//							AddPrintJobs(objPrintJobsInfo);	
//							//for COMIN NOTICE
//							objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CERT_MAIL;
//							objPrintJobsInfo.ONDEMAND_FLAG ="Y";
//							objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL","ALL","Insured","CERT_MAIL");
//							objPrintJobsInfo.FILE_NAME =strCOMINFile;
//							AddPrintJobs(objPrintJobsInfo);	
//						}
//						//adding entry in print job for agnecy
//						if(objProcessInfo.AGENCY_PRINT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL).ToString()))
//						{
//							objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_AGENCY;
//							objPrintJobsInfo.ONDEMAND_FLAG ="N";
//							objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode(System.Web.HttpContext.Current.Session["LOBString"].ToString(),stateCode,"Agent","NREN");
//							objPrintJobsInfo.FILE_NAME =fileNameAgency;
//							AddPrintJobs(objPrintJobsInfo);	
//						}
//						#region//additional Interest in print job table
//						if(objProcessInfo.ADD_INT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL).ToString())|| objProcessInfo.ADD_INT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString()))
//						{
//							if (objProcessInfo.ADD_INT_ID !="")
//							{
//								string strAddlInt="";
//								objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_ADDL_INTEREST;
//								objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL","ALL","ADDLINT","NREN");						
//								if (objProcessInfo.ADD_INT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString()))
//									objPrintJobsInfo.ONDEMAND_FLAG ="Y";
//								else
//									objPrintJobsInfo.ONDEMAND_FLAG ="N";
//								string [] addIntIdArr = objProcessInfo.ADD_INT_ID.Split('~');
//								if(addIntIdArr!=null && addIntIdArr.Length>0)
//								{
//									for(int jCounter=0;jCounter<addIntIdArr.Length;jCounter++)
//									{
//										string [] strArr = addIntIdArr[jCounter].Split('^');
//										if(strArr==null || strArr.Length<1)
//											continue;
//										strAddlInt = objPrintJobsInfo.CUSTOMER_ID.ToString() + "_" + objPrintJobsInfo.POLICY_ID.ToString() + "_" + objPrintJobsInfo.POLICY_VERSION_ID.ToString() + "_" + strArr[1].ToString()  + "_" + strArr[2].ToString();
//										objPrintJobsInfo.FILE_NAME = FindAddIntFileName(strAddIntFile,strAddlInt);
//										AddPrintJobs(objPrintJobsInfo);
//									}
//								}
//
//							}
//						}
//						#endregion
//					}
//					#endregion 
					#endregion
					//base.WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, 
					//	GetTransactionLogDesc(objProcessInfo.PROCESS_ID) , objProcessInfo.COMPLETED_BY,TransactionDescription.ToString());
					
                     */
                    #endregion

                    base.WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,
                        Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1916", ""),objProcessInfo.COMPLETED_BY, TransactionDescription.ToString());
				
					base.CommitTransaction();
					

				}
				else
				{
					base.RollbackTransaction();
				}
				return retval;
			}
			catch (Exception objExp)
			{
				base.RollbackTransaction();
				throw(new Exception("Error Occured while completing the process.\n" + objExp.Message));
			}
		}
		public int generateNonRenewalNotices(ClsProcessInfo objProcessInfo)
		{
			try
			{
				#region Generating Non Renewal Notice
				ClsPrintJobsInfo objPrintJobsInfo = GetPrintJobsValues(objProcessInfo,"NEWVERSION");
				string stateCode=new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().SetStateCode("POLICY",objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID,objProcessInfo.CUSTOMER_ID); 
				string AgencyCode = GetPDFAgencyCode(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID, "POLICY");
				//Ravindra(10-2-2007): Removed Session dependency
				//objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode(System.Web.HttpContext.Current.Session["LOBString"].ToString(),stateCode,"Insured","NREN");
				objPrintJobsInfo.URL_PATH = ClsPolicyProcess.PRINT_JOBS_URL_PATH + AgencyCode + "/" + objProcessInfo.CUSTOMER_ID.ToString() + "/" + "POLICY/NREN_NOTICE" + "/" + "final";
				#region generating PDF files for Insured,agency and for Additional Interest
					string fileNameInsured="";
					string fileNameAgency="";
					string strAddIntFile="";
					string strCOMINFile="";
					int tmpInsured = objProcessInfo.INSURED;
					int tmpAgency  = objProcessInfo.AGENCY_PRINT;  	 
					int intOldVersion=objProcessInfo.POLICY_VERSION_ID; 
					objProcessInfo.POLICY_VERSION_ID=objProcessInfo.NEW_POLICY_VERSION_ID;
					objProcessInfo.INSURED = 1;
					objProcessInfo.AGENCY_PRINT = 0;
					string strNoticeType="NREN_NOTICE";
				   	string strNoticeCode="NREN"; 
					if (objProcessInfo.CANCELLATION_TYPE==int.Parse(((int)enumPROC_CANCEL_TYPE.AGENCY_TERMINATE_NO_NOTIFICATION).ToString()))
					{
						strNoticeType="NREN_NOTICE_NOTIFICATION";
						strNoticeCode="AGN_TERM_NREN";
					}
					else if(objProcessInfo.CANCELLATION_TYPE==int.Parse(((int)enumPROC_CANCEL_TYPE.AGENCY_TERMINATE_NOTIFICATION).ToString()))
					{
						strNoticeType="NREN_NOTICE_NO_NOTIFICATION";
						strNoticeCode="AGN_TERM_NREN";
					}
					else
						strNoticeType="NREN_NOTICE";

					fileNameInsured=GeneratePDF(objProcessInfo,strNoticeType);
					objProcessInfo.INSURED = 0;
					objProcessInfo.AGENCY_PRINT = 1;
					fileNameAgency=GeneratePDF(objProcessInfo,strNoticeType);
					bool IsAddIntExists=IsAddIntExist(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID,objProcessInfo.LOB_ID.ToString(),"POLICY");
					if (IsAddIntExists)
						strAddIntFile=GeneratePDF(objProcessInfo,"NREN_NOTICE_ADDLINT");
					strCOMINFile=GeneratePDF(objProcessInfo,"NREN_NOTICE_CERT_MAIL");
				#endregion
				#region  //adding Entry in print job table for customer/insured
					
				objProcessInfo.INSURED=tmpInsured ;
				objProcessInfo.AGENCY_PRINT=tmpAgency;  	 
				//added by pravesh adding printing job entry
				//ClsPrintJobsInfo objPrintJobsInfo = GetPrintJobsValues(objProcessInfo);
				if (objProcessInfo.PRINTING_OPTIONS ==(int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.NO))
				{
					//adding entry in print job for Insured 
					//add entry in print job other than No print required changed on 3 april 2008
					//if(objProcessInfo.INSURED  == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL).ToString()) ||objProcessInfo.INSURED  == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString()) )
					if(objProcessInfo.INSURED  != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.NOT_REQUIRED).ToString()))
					{
						objPrintJobsInfo.ONDEMAND_FLAG ="Y";
						objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER;
						//Ravindra(10-2-2007): Removed Session dependency
						//objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode(System.Web.HttpContext.Current.Session["LOBString"].ToString(),stateCode,"Insured","NREN");
						objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode((stateCode=="IN" && strNoticeCode=="AGN_TERM_NREN") || (stateCode=="MI" && (objProcessInfo.LOB_ID == ClsRenewalProcess.POLICY_LOB_AUTO ||objProcessInfo.LOB_ID == ClsRenewalProcess.POLICY_LOB_CYCL))?"ALL":BlCommon.ClsCommon.GetLobCodeForLobId(objProcessInfo.LOB_ID.ToString()),stateCode,"Insured",strNoticeCode);
						objPrintJobsInfo.FILE_NAME =fileNameInsured;
						AddPrintJobs(objPrintJobsInfo);	
						//for COMIN NOTICE
						objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CERT_MAIL;
						objPrintJobsInfo.ONDEMAND_FLAG ="Y";
						objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL","ALL","Insured","CERT_MAIL");
						objPrintJobsInfo.FILE_NAME =strCOMINFile;
						AddPrintJobs(objPrintJobsInfo);	
					}
					//adding entry in print job for agnecy
					//add entry in print job other than No print required changed on 3 april 2008
					//if(objProcessInfo.AGENCY_PRINT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL).ToString()))
					if(objProcessInfo.AGENCY_PRINT != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.NOT_REQUIRED).ToString()))
					{
						objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_AGENCY;
						objPrintJobsInfo.ONDEMAND_FLAG ="N";
						//objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode(System.Web.HttpContext.Current.Session["LOBString"].ToString(),stateCode,"Agent","NREN");
						objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode((stateCode=="IN" && strNoticeCode=="AGN_TERM_NREN") || (stateCode=="MI" && (objProcessInfo.LOB_ID == ClsRenewalProcess.POLICY_LOB_AUTO ||objProcessInfo.LOB_ID == ClsRenewalProcess.POLICY_LOB_CYCL))?"ALL":BlCommon.ClsCommon.GetLobCodeForLobId(objProcessInfo.LOB_ID.ToString()),stateCode,"Agent",strNoticeCode);
						objPrintJobsInfo.FILE_NAME =fileNameAgency;
						AddPrintJobs(objPrintJobsInfo);	
					}
					//additional Interest in print job table
					//add entry in print job other than No print required changed on 3 april 2008
					//if( IsAddIntExists== true  && (objProcessInfo.ADD_INT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL).ToString())|| objProcessInfo.ADD_INT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString())))
					if( IsAddIntExists== true  && (objProcessInfo.ADD_INT != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.NOT_REQUIRED).ToString()) ))
					{
						if (objProcessInfo.ADD_INT_ID !="")
						{
							string strAddlInt="";
							objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_ADDL_INTEREST;
							objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL","ALL","ADDLINT","NREN");						
							if (objProcessInfo.ADD_INT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString()))
								objPrintJobsInfo.ONDEMAND_FLAG ="Y";
							else
								objPrintJobsInfo.ONDEMAND_FLAG ="N";
							string [] addIntIdArr = objProcessInfo.ADD_INT_ID.Split('~');
							if(addIntIdArr!=null && addIntIdArr.Length>0)
							{
								for(int jCounter=0;jCounter<addIntIdArr.Length;jCounter++)
								{
									string [] strArr = addIntIdArr[jCounter].Split('^');
									if(strArr==null || strArr.Length<1)
										continue;
									strAddlInt = objPrintJobsInfo.CUSTOMER_ID.ToString() + "_" + objPrintJobsInfo.POLICY_ID.ToString() + "_" + objPrintJobsInfo.POLICY_VERSION_ID.ToString() + "_" + strArr[1].ToString()  + "_" + strArr[2].ToString();
									objPrintJobsInfo.FILE_NAME = FindAddIntFileName(strAddIntFile,strAddlInt);
									AddPrintJobs(objPrintJobsInfo);
								}
							}

						}
					}
				}
				#endregion 
				#endregion
				objProcessInfo.POLICY_VERSION_ID=intOldVersion;
				return 1;
			}
			catch(Exception ex)
			{
				throw(new Exception("Non Renewal Notices Could Not be generated",ex));
				//ClsPolicyErrMsg.strMessage="Non Renewal Notices Could not be generated";
				//return -1;
			}

		}
		/// <summary>
		/// Overrides the base rollback process method.
		/// </summary>
		/// <param name="objProcessInfo"></param>
		/// <returns></returns>
		public override bool RollbackProcess(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo)
		{
			try
			{
				base.BeginTransaction();
				objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ROLLBACK_NON_RENEWAL_PROCESS; 
				//bool retval; 
				if (base.RollbackProcess (objProcessInfo,"NEWVERSION")==true)
				{
					//Deleting the newer version
					base.DeletePolicyVersion(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID);
					TransactionDescription.Append(Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1476","")+ objProcessInfo.NEW_POLICY_VERSION_ID.ToString("#.0")
                        + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1481","") );//"New version (" //") of policy has been deleted.;"

			
					base.WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, 
						GetTransactionLogDesc(objProcessInfo.PROCESS_ID) , objProcessInfo.COMPLETED_BY,TransactionDescription.ToString());
					base.CommitTransaction();
					return true;
				}
				else
				{
					base.RollbackTransaction();
					return false;
				}

			}
			catch (Exception objExp)
			{
				base.RollbackTransaction();
				throw(new Exception("Error Occured while rollback the process.\n" + objExp.Message));
			}
		}



	}
}
