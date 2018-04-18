/******************************************************************************************
<Author				: -		Mohit Agarwal
<Start Date			: -		16-Jan-2007
<End Date			: -		
<Description		: - 	Buisness Layer for Policy Cancellation Process.
<Review Date		: - 
<Reviewed By		: - 	
*******************************************************************************************/ 


using System;
using System.Data;
using System.Data.SqlClient;
using Cms.DataLayer;
using Cms.Model.Policy.Process;
using System.Collections;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlQuote;

namespace Cms.BusinessLayer.BlProcess
{
	/// <summary>
	/// Summary description for ClsCancellationProcess.
	/// </summary>
	public class ClsCorrectiveUserProcess: ClsPolicyProcess
	{
		public ClsCorrectiveUserProcess()
		{
			//
			// TODO: Add constructor logic here
			//
		}

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

		public override bool CommitProcess(ClsProcessInfo objProcessInfo)
		{
			try
			{
				
				objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_CORRECTIVE_USER_PROCESS;

				base.BeginTransaction();
				//TransactionDescription.Append("\n " +  Desc  + ";");
				//base.CommitProcess(objProcessInfo,"NEWVERSION");

				//TransactionDescription.Append("\n Corrective User Process has been done;");

//				base.WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, 
//					GetTransactionLogDesc(objProcessInfo.PROCESS_ID) , objProcessInfo.COMPLETED_BY,TransactionDescription.ToString());

				SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID,POLICY_STATUS_INACTIVE);

                TransactionDescription.Append("\n "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1487",""));//Original Policy version has been made Inactive;


				//generatring rates
				//if (NewPolicyStatus!=POLICY_STATUS_SUSPENDED)
				GenaratePolicyPremium(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID,objProcessInfo.COMPLETED_BY,objProcessInfo.PROCESS_ID.ToString());

				string PolicyStatusDesc,NewPolicyStatus;
				SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID, out PolicyStatusDesc, out NewPolicyStatus);
                TransactionDescription.Append("\n "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1485","") + PolicyStatusDesc + ".;");//Policy Status has been updated to 
				objProcessInfo.POLICY_CURRENT_STATUS=NewPolicyStatus;

				bool retValue=base.CommitProcess(objProcessInfo,"NEWVERSION");
				if (!retValue)
				{
					base.RollbackTransaction();
					return false;
				}
                TransactionDescription.Append("\n"+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1489",""));// Corrective User Process has been done;

				base.WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, 
					GetTransactionLogDesc(objProcessInfo.PROCESS_ID) , objProcessInfo.COMPLETED_BY,TransactionDescription.ToString());

				#region Print pdfs
				// Added by Mohit Agarwal 16-Jan-08
				string policy_status = "";
				try { policy_status=	new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().GetPolicyStatus("POLICY",objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID,objProcessInfo.CUSTOMER_ID); }
				catch//(Exception ex) 
                {}
				
				//Printing Dec Pages if launched on other than Suspended Policy
                //if(policy_status.ToUpper() != "SUSPENDED")
                //    PrintJobsForCorrUser(objProcessInfo);
				#endregion Print pdfs

				base.CommitTransaction();
				return true;
			}
			catch(Exception ex)
			{
				base.RollbackTransaction();
				throw(ex);
				return false;
			}
		}
		public override bool StartProcess(ClsProcessInfo objProcessInfo)
		{
			try
			{
				
				objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_CORRECTIVE_USER_PROCESS;

				base.BeginTransaction();
				
				//Checking the eligibility
				if (base.CheckProcessEligibility(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.PROCESS_ID) != 1)
				{
					base.RollbackTransaction();
					return false;
				}
				//TransactionDescription.Append("\n " +  Desc  + ";");
				objProcessInfo.POLICY_CURRENT_STATUS = POLICY_STATUS_UNDER_CORRECTIVE_USER;
                int intNewVersionId = 0;
                string intNewDispVersion="";
				CreatePolicyNewVersion(objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID,objProcessInfo.CUSTOMER_ID,objProcessInfo.CREATED_BY.ToString(),out intNewVersionId,out intNewDispVersion);
				objProcessInfo.NEW_POLICY_VERSION_ID=intNewVersionId;
				TransactionDescription.Append("\n New Version "+ intNewDispVersion + " Created;");
				bool retval = base.StartProcess (objProcessInfo);
				if (!retval)
				{
				base.RollbackTransaction();
				return retval;
				}
				//TransactionDescription.Append("\n Corrective User Process has been started;");
	
				string strPolicyStatusDesc = "", strNewPolicyStatus = "";
				SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID,out strPolicyStatusDesc, out strNewPolicyStatus);
	
				TransactionDescription.Append("\n "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1490","") + objProcessInfo.NEW_POLICY_VERSION_ID.ToString ("#.0")
                    + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1491","") + strPolicyStatusDesc + ".;");//Status of new version (//) of policy has been updated to 
	
				base.WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, 
					GetTransactionLogDesc(objProcessInfo.PROCESS_ID) , objProcessInfo.COMPLETED_BY,TransactionDescription.ToString());
				
				base.CommitTransaction();
				return retval;
			}
			catch(Exception ex)
			{
				base.RollbackTransaction();
				throw(ex);
				return false;
			}
		}

		//Modified Mohit Agarwal 7-Dec-07
		public override bool RollbackProcess(ClsProcessInfo objProcessInfo)
		{	

			try
			{
				//BEging the transaction
				base.BeginTransaction();

				//Reseting the transaction descrition text
				TransactionDescription = new System.Text.StringBuilder();

				
				//Checking the eligibility
				if (base.CheckProcessEligibility(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID) != 1)
				{
					base.RollbackTransaction();
					return false;
				}

				bool retval = base.RollbackProcess (objProcessInfo,"NEWVERSION");

				//Checking the return status
				if (retval == false)
				{
					//Rollbacking the transaction
					base.RollbackTransaction();
					return retval;
				}

				//Deleting the newer version
				base.DeletePolicyVersion(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID);
				TransactionDescription.Append(Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1476","") + objProcessInfo.NEW_POLICY_VERSION_ID.ToString("#.0")
                    + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1481",""));//"New version ("//) of policy has been deleted.;


				//Changing the status of older policy as active policy
				//SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, POLICY_STATUS_NORMAL);
				//TransactionDescription.Append("Status of previous version " + objProcessInfo.POLICY_VERSION_ID.ToString("#.0") + " has been marked as Active.;");

				//Writing the transaction log
				WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, 
					GetTransactionLogDesc(objProcessInfo.PROCESS_ID) , objProcessInfo.COMPLETED_BY,TransactionDescription.ToString());
				

				//Commiting  the transaction
				base.CommitTransaction();

				return retval;
			}
			catch (Exception objExp)
			{
				base.RollbackTransaction();
				throw ( new Exception("Error occured while RollbackProcess Corrective User process \n" + objExp.Message));
			}
		}

		public int GenaratePolicyPremium(int CustomerId, int PolicyId, int PolicyVersionId,int UserID,string strProcessType)
		{
			try
			{
				int AppId, AppVersionId, LobId;//, retVal=0;
				GetAppLobDetails(CustomerId, PolicyId,PolicyVersionId, out AppId, out AppVersionId, out LobId); 
				//Ravindra(03-19-2008): Can call the method from BLQuote itself web service call would be slow

//				string CmsWebUrl  = "";
//				if(IsEODProcess)
//				{
//					CmsWebUrl = ClsCommon.CmsWebUrl ;
//					UserID  = EODUserID;
//				}
//				else
//				{
//					CmsWebUrl = System.Configuration.ConfigurationSettings.AppSettings["CmsWebUrl"].ToString();
//				}
//				BlProcess.Quote_RuleWebServices.wscmsweb objQuote = new Cms.BusinessLayer.BlProcess.Quote_RuleWebServices.wscmsweb(CmsWebUrl );
//				string strQuoteID = objQuote.GeneratePolicyQuote(CustomerId,PolicyId,PolicyVersionId,LobId.ToString() ,UserID);

				ClsGenerateQuote objQuote = new ClsGenerateQuote(mSystemID);
				int quodeId;
				objQuote.GeneratePolicyQuote(CustomerId,PolicyId,PolicyVersionId,LobId.ToString() ,out quodeId,UserID.ToString() );
				


				//Call Split Premium 
				Cms.BusinessLayer.BlQuote.clsSplitPremium objSplitPremium=new Cms.BusinessLayer.BlQuote.clsSplitPremium();
				ArrayList arrPremiumXML =objSplitPremium.SplitPremiumsPol(CustomerId,PolicyId,PolicyVersionId,strProcessType,objWrapper); 
				return 1;
			}
			catch( Exception ex)
			{
				throw( new  Exception("Error while generating Premium.",ex));
			}
	    }
		//

		#region PrintJobs
		/// <summary>
		/// Makes entries for print jobs
		/// </summary>
		/// <param name="objProcess"></param>
		/// <returns>void</returns>
		public void PrintJobsForCorrUser(ClsProcessInfo objProcessInfo)
		{
			try
			{
				#region  generating PDFs
				string strInsuredFile="",strAgencyFile="",strAddIntFile="",strFileNameAccord="",strAutoIdCardPage="";
				
				int tmpInsured = objProcessInfo.INSURED;
				int tmpAgencyPrint = objProcessInfo.AGENCY_PRINT;
				int oldVersion	= objProcessInfo.POLICY_VERSION_ID;
				objProcessInfo.POLICY_VERSION_ID=objProcessInfo.NEW_POLICY_VERSION_ID;  

				if(objProcessInfo.LOB_ID==ClsRenewalProcess.POLICY_LOB_AUTO || objProcessInfo.LOB_ID==ClsRenewalProcess.POLICY_LOB_HOME)
				{
					string SucessfullPdf = GeneratePDF(objProcessInfo);
					strInsuredFile = WordingPDFFileName;
					strAgencyFile = AgentWordingPDFFileName;
					strFileNameAccord = AcordPDFFileName;
					strAutoIdCardPage = AutoIdCardPDFFileName;
					strAddIntFile = AdditionalIntrstPDFFileName;
				}
				else
				{
					//generatting dec page for Additional Interest
					//				strAddIntFile=GeneratePDF(objProcessInfo,"ADDLINT");
					//generatting dec page for indured
					objProcessInfo.INSURED = 1;
					objProcessInfo.AGENCY_PRINT = 0;
					strInsuredFile = GeneratePDF(objProcessInfo,"DECPAGE");
					//generatting dec page for Agency
					objProcessInfo.INSURED = 0;
					objProcessInfo.AGENCY_PRINT = 1;
					strAgencyFile = GeneratePDF(objProcessInfo,"DECPAGE");
					//				strFileNameAccord=GeneratePDF(objProcessInfo,"ACORD");
					//generating auto ID card
					//				strAutoIdCardPage = GeneratePDF(objProcessInfo,BlCommon.ClsCommon.DOCUMENT_TYPE_AUTO_ID_CARD);						
				}
				objProcessInfo.INSURED =tmpInsured ;
				objProcessInfo.AGENCY_PRINT =tmpAgencyPrint; 
				#endregion
			#region adding printing in Print job
				ClsPrintJobsInfo objPrintJobsInfo = GetPrintJobsValues(objProcessInfo);
				// Do entry for non printing files
				objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER;
				objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;						
				objPrintJobsInfo.FILE_NAME =strInsuredFile;  
				AddPdfFileLog(objPrintJobsInfo);	

				// Do entry for non printing files					
				objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_AGENCY;
				objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
				objPrintJobsInfo.FILE_NAME =strAgencyFile;  
				AddPdfFileLog(objPrintJobsInfo);

//				 //Commented by Pravesh on 14 Feb as discussed with ravinder/Rajan ( No entry in Print Job for Corrective User Process)
//					ClsPolicyProcess objPolicyProcesses  = new ClsPolicyProcess();
////				if (objProcessInfo.INTERNAL_CHANGE == Convert.ToString((int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.NO )  ))
//				{
//					ClsPrintJobsInfo objPrintJobsInfo = GetPrintJobsValues(objProcessInfo);
////					if(objProcessInfo.PRINTING_OPTIONS == (int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.NO))
//					{
//						//customer
//						if(objProcessInfo.INSURED == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString()))
//						{
//							objPrintJobsInfo.ENTITY_TYPE = ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_CUSTOMER;
//							objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;						
//							AddPrintJobs(objPrintJobsInfo);	
//							//objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_ACORD;						
//							//AddPrintJobs(objPrintJobsInfo);	
//						}
//						//agency
//						if(objProcessInfo.AGENCY_PRINT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString()))
//						{
//							objPrintJobsInfo.ENTITY_TYPE = ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_AGENCY;
//							objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
//							AddPrintJobs(objPrintJobsInfo);	
//							//objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_ACORD;						
//							//AddPrintJobs(objPrintJobsInfo);	
//						}
//						//additional interest
//			/*			if(objProcessInfo.ADD_INT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString()))
//						{
//							if (objProcessInfo.ADD_INT_ID !="")
//							{
//								string strAddlInt="";
//								objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_ADDL_INTEREST;
//								objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;						
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
//							}
//						} */
//						//auto ID Card
//						/*if (objProcessInfo.NO_COPIES==0)
//							objProcessInfo.NO_COPIES=2; //default value
//						if((objProcessInfo.LOB_ID ==ClsRenewalProcess.POLICY_LOB_AUTO)  || (objProcessInfo.LOB_ID == ClsRenewalProcess.POLICY_LOB_CYCL))
//						{
//							objPrintJobsInfo.DOCUMENT_CODE = objPrintJobsInfo.ENTITY_TYPE = "AUTO_ID_CARD";						
//							objPrintJobsInfo.FILE_NAME = strAutoIdCardPage;
//							for(int i=0;i<objProcessInfo.NO_COPIES;i++)						
//								AddPrintJobs(objPrintJobsInfo);	
//						}*/
//					}
//				}
//				
			#endregion
				objProcessInfo.POLICY_VERSION_ID=oldVersion	;
			}

			catch(Exception exc)
			{
				throw( new Exception("Error while generating PDFs! please try later...", exc));
			}
			finally
			{}
		}
		#endregion

	}
}
