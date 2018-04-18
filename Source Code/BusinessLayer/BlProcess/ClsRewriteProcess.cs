/******************************************************************************************
<Author				: -		Pravesh K. chandel
<Start Date			: -		21 feb-2007
<End Date			: -		
<Description		: - 	Buisness Layer for Policy Rewrite Process.
<Review Date		: - 
<Reviewed By		: - 	
*******************************************************************************************/ 


using System;
using System.Data;
using System.Data.SqlClient;
using Cms.DataLayer;
using Cms.Model.Policy.Process;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;


namespace Cms.BusinessLayer.BlProcess
{
	/// <summary>
	/// Summary description for ClsRewriteProcess.
	/// </summary>
	public class ClsRewriteProcess : ClsPolicyProcess
	{
		public const int POLICY_LOB_HOME	= 1;
		public const int POLICY_LOB_AUTO	= 2;
		public const int POLICY_LOB_CYCL	= 3;
		public const int POLICY_LOB_BOAT	= 4;
		public const int POLICY_LOB_UMB		= 5;
		public const int POLICY_LOB_RENT	= 6;
		public const int POLICY_LOB_GEN		= 7;

		public ClsRewriteProcess()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region overrided function
		protected override bool OnSetPolicyStatus()
		{
			return false;
		}

		protected override bool OnCheckProcessEligibility()
		{
			return false;
		}

		protected override bool OnWriteTransactionLog()
		{
			return false;
		}
		#endregion
		/// <summary>
		/// Override the StartProcess Method
		/// </summary>
		/// <param name="objProcessInfo"></param>
		/// <returns></returns>
		public override bool StartProcess(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo)
		{
			//Calling the base class start process methos which will
			//insert the record in POL_POLICY_PROCESS table
			//and will do the transaction log entry
			try
			{
				base.BeginTransaction(); 

				//Reseting the transaction descrition text
				TransactionDescription = new System.Text.StringBuilder();

				objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REWRITE_PROCESS;

				//Checking the eligibility
				if (base.CheckProcessEligibility(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.PROCESS_ID) != 1)
				{
					base.RollbackTransaction();
					return false;
				}
				//Creating new version of policy
                int intNewPolicyVersion = 0;
                string newDispversion_Rewritable ;
                string newDispVersion ;
				
				CreatePolicyNewVersionForRewrite(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID, objProcessInfo.CREATED_BY.ToString(), out intNewPolicyVersion,out newDispVersion,out newDispversion_Rewritable);
				objProcessInfo.NEW_POLICY_VERSION_ID = intNewPolicyVersion;
				//TransactionDescription.Append("New version (" + newDispVersion.ToString("#.0") + ") of Policy has been created.;");
				TransactionDescription.Append("New version (" + newDispversion_Rewritable + ") of Policy has been created, but rewritten as (" + newDispVersion + ");");
				//Updating the status
				objProcessInfo.POLICY_CURRENT_STATUS = POLICY_STATUS_UNDER_REWRITE ;
				//Calling the base class function, for starting the process
				//bool retVal =  base.StartProcess (objProcessInfo);
				//
				//At start process status should be pending
				objProcessInfo.PROCESS_STATUS = PROCESS_STATUS_PENDING;
				//Adding the record into policy process table (POL_POLICY_PROCESS)
				AddProcessInformation(objProcessInfo);
				TransactionDescription.Append("\nProcess has been added in process log.;");
				
				//Sets the Policy Status of newer version as under Rewrite
				//				string strNewPolicyNumber="";
//				SetNewPolicyNumber(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID,out strNewPolicyNumber);
//				TransactionDescription.Append("\n New Policy Number (" + strNewPolicyNumber + ") has been generated for this Policy.;");
//			
				#region DIARY ENTRY
				Cms.BusinessLayer.BlCommon.ClsDiary objClsDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
				Cms.Model.Diary.TodolistInfo objModel = new Cms.Model.Diary.TodolistInfo();

				int DiaryListTypeId;
				string strDiaryDesc = GetTransactionLogDesc(objProcessInfo.PROCESS_ID,out DiaryListTypeId);
				//Commented by Anurag Verma on 20/03/2007 as these properties are removed
				//objModel.POLICYCLIENTID = objProcessInfo.CUSTOMER_ID;
				objModel.PROCESS_ROW_ID =objProcessInfo.ROW_ID; 

				objModel.LISTTYPEID = DiaryListTypeId;
				objModel.POLICY_ID = objProcessInfo.POLICY_ID;
				objModel.POLICY_VERSION_ID = objProcessInfo.NEW_POLICY_VERSION_ID;
				objModel.CUSTOMER_ID = objProcessInfo.CUSTOMER_ID;
				if (IsEODProcess)
					objModel.FROMUSERID = objModel.CREATED_BY= EODUserID;
				else
					objModel.FROMUSERID = objModel.CREATED_BY = objProcessInfo.COMPLETED_BY;
				objModel.NOTE = objModel.SUBJECTLINE = strDiaryDesc ;//+ "(New Policy number= " + strNewPolicyNumber + ")";
				objModel.CREATED_DATETIME = DateTime.Now;
				objModel.RECDATE = DateTime.Now;
				objModel.LISTOPEN="Y" ;					
				objModel.MODULE_ID=(int)BlCommon.ClsDiary.enumModuleMaster.POLICY_PROCESS;
				objModel.LOB_ID = objProcessInfo.LOB_ID; 
				objWrapper.ClearParameteres(); 
 				objClsDiary.DiaryEntryfromSetup(objModel,objWrapper); 
				#endregion
				
				//
				//Update the Policy Display Version of Newly created policy version.
				//UpdatePolicyDisplayVersion(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,"1.0"); 
				string strStatusDes="",strStatus="";
				//Updating the status of new version of policy
				SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID,out strStatusDes,out strStatus);
				TransactionDescription.Append("\n Status of new version (" + newDispVersion 
					+ ") of policy has been updated to " + strStatusDes + ".;");
				//UPDATE COVERAGES FOR PROCESS DEPENDEND in case of Home LOB
				if (objProcessInfo.LOB_ID==POLICY_LOB_HOME)
				{
					ClsHomeCoverages objHomeCoverage=new ClsHomeCoverages();
					objHomeCoverage.UpdateCoveragesByRulePolicy(objWrapper,objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID,BlApplication.RuleType.ProcessDependend); 
					objHomeCoverage.Dispose();
				}
				//WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, 
				//	"Rewrite Start Process completed Successfully", objProcessInfo.COMPLETED_BY, TransactionDescription.ToString() );
				
				//fetching MVR in case of Auto and Cyclelob
				//adding by kranti
				
				if (objProcessInfo.LOB_ID  == POLICY_LOB_AUTO || objProcessInfo.LOB_ID  == POLICY_LOB_CYCL)
				{
					
					DataSet dsUpdatedMVR = RequestMVR(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID);
					if (dsUpdatedMVR.Tables[0].Rows.Count >0)  
					{
						InsertDriverMVR(dsUpdatedMVR,objProcessInfo);
						TransactionDescription.Append ("\n Driver MVR has been fetched.;");
						if (objProcessInfo.LOB_ID == POLICY_LOB_AUTO )
						{
							Cms.BusinessLayer.BlApplication.ClsDriverDetail  objAutoDriver = new ClsDriverDetail();  
							objAutoDriver.UpdateVehicleClassPolNew(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID,  objProcessInfo.NEW_POLICY_VERSION_ID,objWrapper);
							TransactionDescription.Append ("\n Vehicle Class has been updated.;");
						}
						else
						{
							this.UpdateVehicleClassForMotor(dsUpdatedMVR,objProcessInfo);	
							TransactionDescription.Append ("\n Motorcycle Class has been updated.;");
						}

						
					}
				}
                #region Genrate new Application No and Update

                ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
                DataSet DsPolicy = objGeneralInformation.GetPolicyDataSet(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, intNewPolicyVersion);
                int AgencyId = 0;
                if (DsPolicy != null && DsPolicy.Tables.Count > 0 && DsPolicy.Tables[0].Rows.Count > 0)
                {
                    if (DsPolicy.Tables[0].Rows[0]["AGENCY_ID"].ToString() != "")
                        AgencyId = int.Parse(DsPolicy.Tables[0].Rows[0]["AGENCY_ID"].ToString());
                }
                //int AgencyID=
                String NewAppNo = ClsGeneralInformation.GenerateApplicationNumber(objProcessInfo.LOB_ID, AgencyId);

                int Update = objGeneralInformation.UpdatePolicyNo(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID,
                    intNewPolicyVersion, NewAppNo, objProcessInfo.PROCESS_ID, objWrapper);
                if (Update > 0)
                {
                    TransactionDescription.Append(Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1899", "") + NewAppNo + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1900", "") + " ;"); //"New Application Number : " + NewAppNo + " has been genrated.;"

                }

                #endregion Genrate new Application No and Update
				//Writing the transaction log
				WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, 
					GetTransactionLogDesc(objProcessInfo.PROCESS_ID) , objProcessInfo.COMPLETED_BY, TransactionDescription.ToString(),TransactionChangeXML.ToString() );
				base.CommitTransaction(); 
				//return retVal;
				return true;
			}
			catch(Exception exc)
			{
				base.RollbackTransaction();
				throw (exc);
			}

		}
		public override bool CommitProcess(ClsProcessInfo objProcessInfo)
		{
			return CommitProcess(objProcessInfo,"");
		}
		public override bool CommitProcess(ClsProcessInfo objProcessInfo,string CalledFrom)
		{
			try
			{				

				//Beging the transaction
				base.BeginTransaction();

				//Reseting the transaction descrition text
				TransactionDescription = new System.Text.StringBuilder();
				bool retval;
				//int returnResult = 0;

				//UpdatePolicyTerm_EffectiveExpirydate(objProcessInfo);

				//Updating the status of new version of policy
				//SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID);
				//TransactionDescription.Append("Status of new version (" + objProcessInfo.NEW_POLICY_VERSION_ID.ToString ("#.0") 
				//	+ ") of policy has been updated.;");
				/*
				//Updating Policy terms,policy effective date and policy ecxpiry date as per new policy term,policy effective date and expiry date on reinstatement process's screeen 
				UpdatePolicyTerm_EffectiveExpirydate(objProcessInfo);
				TransactionDescription.Append("New version (" + objProcessInfo.NEW_POLICY_VERSION_ID.ToString("#.0") 
					+ ") of policy updated for new policy term policy effective date and policy expiry date.;");
				//updating coverages and Endoersement of new version
				
				if (objProcessInfo.SAME_AGENCY == int.Parse(((int)enumAGENCY_CHANGE_FORM_TYPES.CHANGE_IN_FORM).ToString())) 
				{
				  //adjusting Coverages
					AdjustCoverages(objProcessInfo);
					TransactionDescription.Append("New version (" + objProcessInfo.NEW_POLICY_VERSION_ID.ToString("#.0") 
						+ ") of policy updated for Coverages and Endorsement.;");
				 }
				//updating coverages and Endoersement and agency of new version
				if (objProcessInfo.ANOTHER_AGENCY  == int.Parse(((int)enumAGENCY_CHANGE_FORM_TYPES.CHANGE_IN_FORM).ToString())) 
				{
					//adjusting Coverages
					AdjustCoverages(objProcessInfo);
					TransactionDescription.Append("New version (" + objProcessInfo.NEW_POLICY_VERSION_ID.ToString("#.0") 
						+ ") of policy updated for Coverages and Endorsement and Agency.;");
				}
				*/

				//Check the Validity of New Insurance Score Otherwise Call the Insurance Score Method
                //add by kranti 24th April 2007
                #region Commented code of get insurance score
                //if (CheckInsuranceScoreValidity(objProcessInfo.CUSTOMER_ID) == true)
                //{
                //    returnResult =  GetInsertInsuranceScore(objProcessInfo.CUSTOMER_ID,objProcessInfo.COMPLETED_BY);
                //    if (returnResult==-3) //address not validated refer this policy
                //    {
                //        base.UpdatePolicyReferToUnderWriter(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,"ADDRESS_NOT_VALID");
                //        base.CommitTransaction(); 
                //        ClsPolicyErrMsg.strMessage="Process Could not be completed successfully. Customer Address Could not be Validated while fetching Insurance Score.";
                //        return false;
                //    }
                //    else if (returnResult != 0)
                //    {
                //        TransactionDescription.Append("\n Insurance Score has been updated.;");
                //    }
                //}
                #endregion
                //verifying the rules
				string strRulesStatus="0";
				string strRulesStatusReason="";
				bool valid=false;	
				//not called for commmit any way then verify the rules
				if (CalledFrom!="COMMITANYWAY")
				{
					string strRulesHTML = strHTML(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,out valid,out strRulesStatus,"REW");
			
					if(valid && strRulesStatus == "0") // then commit
					{
						valid=true;
					}
					else
					{
						// chk here for referred/rejected cases
						strRulesStatusReason=base.ChkReferedRejCaese(strRulesHTML);
						ClsPolicyErrMsg.strMessage ="Rule Violated for this Policy.";
						valid=false;
					}	
					if(!valid)
					{
						//Rewrite in suspense proces
						SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,POLICY_STATUS_SUSPENSE_REWRITE); 	
						AddDiaryEntryForRulesViolation(objProcessInfo);
						TransactionDescription.Append("\n Rewrite in suspense has been recorded.;"); 
						TransactionDescription.Append(Cms.CmsWeb.cmsbase.COMMIT_ANYWAYS_RULES);
						base.WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, 
							"Rewrite process could not be committed" , objProcessInfo.COMPLETED_BY, TransactionDescription.ToString() );  //strRulesHTML);
						base.CommitTransaction();
						return false;
					}
				}
				else
				{
				}
				//
				//Checking the eligibility, for the newer version of policy
				if (base.CheckProcessEligibility(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID) != 1)
				{
					base.RollbackTransaction();
					return false;
				}
				
				//Assigning New Policy Number To This policy
				
				string strNewPolicyNumber="",strNewPolicyStatusDesc="",NewPolicyStatus="";
                SetNewPolicyNumber(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, out strNewPolicyNumber, objProcessInfo.EFFECTIVE_DATETIME, objProcessInfo.PROCESS_ID,objWrapper);
				TransactionDescription.Append("\n New Policy Number (" + strNewPolicyNumber + ") has been generated for this Policy.;");
				//get display version of new policy
				ClsGeneralInformation objGenInfo=new ClsGeneralInformation();
				DataSet dsPolicy =	objGenInfo.GetPolicyDetails(objProcessInfo.CUSTOMER_ID,0,0, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID); 
				string strDisplayVersion="";
				if (dsPolicy.Tables[0].Rows.Count >0)
				{
					if ( dsPolicy.Tables[0].Rows[0]["POLICY_DISP_VERSION"]!=null)
						strDisplayVersion= dsPolicy.Tables[0].Rows[0]["POLICY_DISP_VERSION"].ToString();
				}
				dsPolicy.Clear();

				#region posting premium
				string TransDesc="";
				double CarryFDAmt=0;
				//int PreviousTerm=0;
				int intResult ;

				//DataSet dsPolicy ;
				//Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGeneralInformation = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
//				DataSet dsPolicy = objGeneralInformation.GetPolicyDataSet(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID);
//				if (dsPolicy.Tables[0].Rows.Count >0)
//				{
//					if(dsPolicy.Tables[0].Rows[0]["APP_TERMS"]!=null && dsPolicy.Tables[0].Rows[0]["APP_TERMS"].ToString()!="")
//						PreviousTerm = int.Parse(dsPolicy.Tables[0].Rows[0]["APP_TERMS"].ToString()); 
//					dsPolicy.Clear();
//				}
				
				dsPolicy=FetchPrevousProcessInfo(objProcessInfo);
				if (dsPolicy.Tables[0].Rows.Count >0)
				{
					if(dsPolicy.Tables[0].Rows[0]["CFD_AMT"]!=null && dsPolicy.Tables[0].Rows[0]["CFD_AMT"].ToString()!="")
						CarryFDAmt = int.Parse(dsPolicy.Tables[0].Rows[0]["CFD_AMT"].ToString()); 
					dsPolicy.Clear(); 
				}
				dsPolicy.Dispose();
				// Rewrite means a new term always
//				if (PreviousTerm ==objProcessInfo.POLICY_TERMS) 
//				{
//					PostReinstatementPremium(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID, 
//						objProcessInfo.POLICY_VERSION_ID,objProcessInfo.NEW_POLICY_VERSION_ID,"SAME_TERM",objProcessInfo.EFFECTIVE_DATETIME,CarryFDAmt,objProcessInfo.ROW_ID,objProcessInfo.COMPLETED_BY,objProcessInfo.PROCESS_ID,
//						out TransDesc,"O",objProcessInfo.PROCESS_ID.ToString());
//				}
//				else
//				{
				//Commented By Ravindra(09-05-2007)
				//No Reinstatement Fees to be charged in case of Rewrite and there whill not be any Carry Forward amount
				//Balance amount from Cancelled policies will be billed to Customer	
//				PostReinstatementPremium(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID, 
//						objProcessInfo.POLICY_VERSION_ID,objProcessInfo.NEW_POLICY_VERSION_ID,"OTHER_TERM",objProcessInfo.EFFECTIVE_DATETIME,CarryFDAmt,objProcessInfo.ROW_ID,objProcessInfo.COMPLETED_BY,objProcessInfo.PROCESS_ID,
//						out TransDesc,"O",objProcessInfo.PROCESS_ID.ToString());
					UpdateProcessInfoBeforeCommit(objProcessInfo);
					intResult = PostPolicyPremium (objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID, 
						objProcessInfo.NEW_POLICY_VERSION_ID,objProcessInfo.COMPLETED_BY,objProcessInfo.PROCESS_ID,
						out TransDesc,"O",objProcessInfo.PROCESS_ID.ToString());
				

					//bool retval;				
					if(intResult == -3 || intResult == -2 )
					{
						Cms.BusinessLayer.BlCommon.ClsDiary objClsDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
						Cms.Model.Diary.TodolistInfo objModel = new Cms.Model.Diary.TodolistInfo();

						if(intResult == -3)
						{
							ClsPolicyErrMsg.strMessage ="Selected Billing plan has been deactivated.Please select new Billing plan.";
							objModel.SUBJECTLINE = "Please select new Billing plan.";
							objModel.NOTE = "Selected Billing plan has been deactivated.";
						}
						else
						{
							ClsPolicyErrMsg.strMessage ="Commission not found,So process not comitted.";
							objModel.SUBJECTLINE = "Rewrite Process Commit Failed.";
							objModel.NOTE = "No Commission found.";					
						}
						base.RollbackTransaction();
						//objModel.TOUSERID =objProcessInfo.UNDERWRITER; 
						objModel.LISTTYPEID =(int)BlCommon.ClsDiary.enumDiaryType.PROCESS_PENDING_REQUEST;//    15;
						objModel.POLICY_ID = objProcessInfo.POLICY_ID;
						objModel.POLICY_VERSION_ID = objProcessInfo.NEW_POLICY_VERSION_ID;//objProcessInfo.POLICY_VERSION_ID;
						objModel.CUSTOMER_ID = objProcessInfo.CUSTOMER_ID;
						objModel.CREATED_BY = objProcessInfo.COMPLETED_BY;
						objModel.CREATED_DATETIME = DateTime.Now;
						objModel.RECDATE = DateTime.Now;
						//objModel.FOLLOWUPDATE = DateTime.Now;    
						objModel.LAST_UPDATED_DATETIME = DateTime.Now;
						//objModel.STARTTIME = (System.DateTime)DateTime.Now;
						//objModel.ENDTIME = (System.DateTime)DateTime.Now;
						objModel.PROCESS_ROW_ID =objProcessInfo.ROW_ID;  
						objModel.MODULE_ID	= (int)BlCommon.ClsDiary.enumModuleMaster.POLICY_PROCESS;
						objModel.LOB_ID		= objProcessInfo.LOB_ID; 
						objClsDiary.DiaryEntryfromSetup(objModel); 
						//objClsDiary.Add(objModel,"");					

						retval= false;
						//base.RollbackTransaction();
						return false;
					}
					else
					{				
					}
				//}
				#endregion
				//Updating the status of previous policy as inactive
				//SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, POLICY_STATUS_INACTIVE);
				//TransactionDescription.Append("Old version (" + objProcessInfo.POLICY_VERSION_ID.ToString("#.0") 
				//	+ ") of policy marked as inactive.;");

				//Updating the status of new version of policy
				SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID,out strNewPolicyStatusDesc, out NewPolicyStatus);
				TransactionDescription.Append("Status of new version (" + strDisplayVersion 
					+ ") of policy has been updated to " + strNewPolicyStatusDesc + ".;");
				objProcessInfo.POLICY_CURRENT_STATUS=NewPolicyStatus;
				//Updating the status of new policy as active
				SetPolicyIsActive (objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, "Y");
				TransactionDescription.Append("New version (" + strDisplayVersion
					+ ") of policy marked as active.;");
				retval = base.CommitProcess (objProcessInfo,"NEWVERSION");
				//Checking the return status
				if (retval == false)
				{
					//Rollbacking the transaction
					base.RollbackTransaction();
					return retval;
				}
				if (CalledFrom=="COMMITANYWAY")
				{
					TransactionDescription.Append(Cms.CmsWeb.cmsbase.COMMIT_ANYWAYS_RULES);
					//string strSubjectLine ="The Rewrite process was committed Anyway.";
					//AddDiaryEntry(objProcessInfo,strSubjectLine,"Rewrite Process committed anyway.",true,objWrapper,CalledFrom);
					TransactionDescription.Append("\n Rewrite Process committed anyway.;"); 
				}
				//Writing the transaction log
				WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, 
					GetTransactionLogDesc(objProcessInfo.PROCESS_ID) , objProcessInfo.COMPLETED_BY, TransactionDescription.ToString() );
				//Committing the transaction
                //				base.CommitTransaction();


                //Commented by Lalit  for New implimentation ,no requirement for print on re-write

                #region Commented code of print job entry
//                #region Add Print Jobs
//                //string strAgencyCode = GetPDFAgencyCode(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID, "POLICY");
//                //AddPrintJobs(objPrintJobsInfo);
				
//                //
////				try
////				{
////					base.BeginTransaction(); 
//                    string fileNameInsured="";
//                    string fileNameAgency="";
//                    string fileNameAccord="";
//                    string fileNameAddInt="",strAutoIdCardPage="";
//                    int oldPolicyVersion=objProcessInfo.POLICY_VERSION_ID; 
//                    objProcessInfo.POLICY_VERSION_ID=objProcessInfo.NEW_POLICY_VERSION_ID;
//                    if (objProcessInfo.SEND_ALL==(int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.YES))
//                        getAdditionalInterest(objProcessInfo);
//                    int tmpInsured=objProcessInfo.INSURED;
//                    int tmpAgency=objProcessInfo.AGENCY_PRINT;
//                    bool IsAddIntExists=IsAddIntExist(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID,objProcessInfo.LOB_ID.ToString(),"POLICY");
//                if(objProcessInfo.LOB_ID==ClsRenewalProcess.POLICY_LOB_AUTO || objProcessInfo.LOB_ID==ClsRenewalProcess.POLICY_LOB_HOME)
//                {
//                    string SucessfullPdf = GeneratePDF(objProcessInfo);
//                    fileNameInsured=WordingPDFFileName;
//                    fileNameAgency=AgentWordingPDFFileName;
//                    strAutoIdCardPage=AutoIdCardPDFFileName;
//                    fileNameAddInt=AdditionalIntrstPDFFileName;
//                    fileNameAccord=AcordPDFFileName;
//                }
//                else
//                {
//                    if (IsAddIntExists)
//                        fileNameAddInt=GeneratePDF(objProcessInfo,"ADDLINT");

//                    objProcessInfo.INSURED =0;
//                    objProcessInfo.AGENCY_PRINT =1;
//                    fileNameAgency=GeneratePDF(objProcessInfo,"DECPAGE");
					
//                    objProcessInfo.INSURED =1;
//                    objProcessInfo.AGENCY_PRINT =0;
//                    fileNameInsured=GeneratePDF(objProcessInfo,"DECPAGE");

//                    fileNameAccord=GeneratePDF(objProcessInfo,"ACORD");
//                    //generating auto ID card as per Itrack 4869
//                    strAutoIdCardPage = GeneratePDF(objProcessInfo,BlCommon.ClsCommon.DOCUMENT_TYPE_AUTO_ID_CARD);						
//                    //objProcessInfo.POLICY_VERSION_ID=oldPolicyVersion; 
//                    objProcessInfo.INSURED=tmpInsured;
//                    objProcessInfo.AGENCY_PRINT =tmpAgency; 

//                }
//                    ClsPrintJobsInfo objPrintJobsInfo = GetPrintJobsValues(objProcessInfo,"NEWVERSION");
//                if(objProcessInfo.PRINTING_OPTIONS == (int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.NO))
//                {
//                    // do entry in print job other than No print required --chnanged on 3 april 2008
//                    objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER;
//                    objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;						
//                    objPrintJobsInfo.FILE_NAME =fileNameInsured;  
//                    if(objProcessInfo.INSURED != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.NOT_REQUIRED).ToString()))
//                        AddPrintJobs(objPrintJobsInfo);	
//                        /*
//                        objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_ACORD;						
//                        objPrintJobsInfo.FILE_NAME =fileNameAccord; 
//                        AddPrintJobs(objPrintJobsInfo);	*/
//                    else // Do entry for non printing files
//                        AddPdfFileLog(objPrintJobsInfo);	
//                    // do entry in print job other than No print required --chnanged on 3 april 2008
					
//                    objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_AGENCY;
//                    objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
//                    objPrintJobsInfo.FILE_NAME =fileNameAgency;  
//                    if(objProcessInfo.AGENCY_PRINT != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.NOT_REQUIRED).ToString()))
//                        AddPrintJobs(objPrintJobsInfo);	
//                        /*
//                        objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_ACORD;						
//                        objPrintJobsInfo.FILE_NAME =fileNameAccord; 
//                        AddPrintJobs(objPrintJobsInfo);	*/
//                    else // Do entry for non printing files
//                        AddPdfFileLog(objPrintJobsInfo);
//                    // do entry in print job other than No print required --chnanged on 3 april 2008
//                    if(IsAddIntExists ==true)
//                    {
//                        if (objProcessInfo.ADD_INT_ID!="")
//                        {
//                            /*objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_ADDL_INTEREST;
//                            objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;						
//                            AddPrintJobs(objPrintJobsInfo);	
//                            objPrintJobsInfo.FILE_NAME =fileNameAddInt;
//                            /*objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_ACORD;						
//                            objPrintJobsInfo.FILE_NAME =fileNameAccord; 
//                            AddPrintJobs(objPrintJobsInfo);		*/									
//                            //GeneratePDF(objProcessInfo,"ADDLINT");
//                            string strAddlInt="";
//                            objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_ADDL_INTEREST;
//                            objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;						
//                            string [] addIntIdArr = objProcessInfo.ADD_INT_ID.Split('~');
//                            if(addIntIdArr!=null && addIntIdArr.Length>0)
//                            {
//                                for(int jCounter=0;jCounter<addIntIdArr.Length;jCounter++)
//                                {
//                                    string [] strArr = addIntIdArr[jCounter].Split('^');
//                                    if(strArr==null || strArr.Length<1)
//                                        continue;

//                                    strAddlInt = strArr[0].ToString()  + "_" + objPrintJobsInfo.CUSTOMER_ID.ToString() + "_" + objPrintJobsInfo.POLICY_ID.ToString() + "_" + objPrintJobsInfo.POLICY_VERSION_ID.ToString() + "_" + strArr[1].ToString()  + "_" + strArr[2].ToString();
//                                    objPrintJobsInfo.FILE_NAME = FindAddIntFileName(fileNameAddInt,strAddlInt);
//                                    if(objProcessInfo.ADD_INT != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.NOT_REQUIRED).ToString()))
//                                        AddPrintJobs(objPrintJobsInfo);	
//                                    else // Do entry for non printing files
//                                        AddPdfFileLog(objPrintJobsInfo);
//                                }
//                            }
//                        }
//                    }
//                    //auto ID Card as per Itrack 4869
//                    if (objProcessInfo.NO_COPIES==0)
//                        objProcessInfo.NO_COPIES=2; //default value
//                    if(objProcessInfo.STATE_CODE=="IN")
//                        objProcessInfo.NO_COPIES=1;
//                    if((objProcessInfo.LOB_ID  == POLICY_LOB_AUTO || objProcessInfo.LOB_ID == POLICY_LOB_CYCL) && strAutoIdCardPage !="")
//                    {
//                        objPrintJobsInfo.DOCUMENT_CODE = objPrintJobsInfo.ENTITY_TYPE = "AUTO_ID_CARD";						
//                        objPrintJobsInfo.FILE_NAME = strAutoIdCardPage;
//                        if(objProcessInfo.AUTO_ID_CARD != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.NOT_REQUIRED).ToString()))
//                        {
//                            for(int i=0;i<objProcessInfo.NO_COPIES;i++)						
//                                AddPrintJobs(objPrintJobsInfo);	
//                        }
//                        else // Do entry for non printing files
//                            AddPdfFileLog(objPrintJobsInfo);
//                    }

//                    // Do entry for non printing files
//                    objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
//                    objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER_NOWORD;													
//                    objPrintJobsInfo.FILE_NAME =NoWordingPDFFileName; 
//                    AddPdfFileLog(objPrintJobsInfo);
//                    // Do entry for non printing files
//                    objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_ACORD;
//                    objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_DOCUMENT_CODE_ACORD;													
//                    objPrintJobsInfo.FILE_NAME =fileNameAccord; 
//                    AddPdfFileLog(objPrintJobsInfo);
//                }
//                else
//                {
//                    // Do entry for non printing files
//                    objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER;
//                    objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;						
//                    objPrintJobsInfo.FILE_NAME =fileNameInsured;  
//                    AddPdfFileLog(objPrintJobsInfo);	

//                    // Do entry for non printing files					
//                    objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_AGENCY;
//                    objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
//                    objPrintJobsInfo.FILE_NAME =fileNameAgency;  
//                    AddPdfFileLog(objPrintJobsInfo);
//                    // Do entry for non printing files
//                    if(IsAddIntExists ==true)
//                    {
//                        if (objProcessInfo.ADD_INT_ID!="")
//                        {
//                            string strAddlInt="";
//                            objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_ADDL_INTEREST;
//                            objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;						
//                            string [] addIntIdArr = objProcessInfo.ADD_INT_ID.Split('~');
//                            if(addIntIdArr!=null && addIntIdArr.Length>0)
//                            {
//                                for(int jCounter=0;jCounter<addIntIdArr.Length;jCounter++)
//                                {
//                                    string [] strArr = addIntIdArr[jCounter].Split('^');
//                                    if(strArr==null || strArr.Length<1)
//                                        continue;
//                                    strAddlInt = strArr[0].ToString()  + "_" + objPrintJobsInfo.CUSTOMER_ID.ToString() + "_" + objPrintJobsInfo.POLICY_ID.ToString() + "_" + objPrintJobsInfo.POLICY_VERSION_ID.ToString() + "_" + strArr[1].ToString()  + "_" + strArr[2].ToString();
//                                    objPrintJobsInfo.FILE_NAME = FindAddIntFileName(fileNameAddInt,strAddlInt);
//                                    AddPdfFileLog(objPrintJobsInfo);
//                                }
//                            }
//                        }
//                    }
//                    // Do entry for non printing files
//                    if((objProcessInfo.LOB_ID  == POLICY_LOB_AUTO || objProcessInfo.LOB_ID == POLICY_LOB_CYCL) && strAutoIdCardPage !="")
//                    {
//                        objPrintJobsInfo.DOCUMENT_CODE = objPrintJobsInfo.ENTITY_TYPE = "AUTO_ID_CARD";						
//                        objPrintJobsInfo.FILE_NAME = strAutoIdCardPage;
//                        AddPdfFileLog(objPrintJobsInfo);
//                    }
//                    // Do entry for non printing files
//                    objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
//                    objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER_NOWORD;													
//                    objPrintJobsInfo.FILE_NAME =NoWordingPDFFileName; 
//                    AddPdfFileLog(objPrintJobsInfo);

//                    // Do entry for non printing files
//                    objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_ACORD;
//                    objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_DOCUMENT_CODE_ACORD;													
//                    objPrintJobsInfo.FILE_NAME =fileNameAccord; 
//                    AddPdfFileLog(objPrintJobsInfo);
//                }
//                /*
//                DataSet dsPrintDoc= base.GetPrintDocuments(objProcessInfo.PROCESS_ID)  ; 
//                for (int i=0;i<dsPrintDoc.Tables[0].Rows.Count -1;i++)
//                {
//                    string doc_code=dsPrintDoc.Tables[0].Rows[i]["DOCUMENT_CODE"].ToString();   
//                    objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL","ALL","Agent",doc_code );
//                    AddPrintJobs(objPrintJobsInfo);	

//                }
//                dsPrintDoc.Clear();
//                dsPrintDoc.Dispose(); 
//                */

//                #endregion
                #endregion commented code

                   base.GeneratePolicyDocuments(objProcessInfo);
                //objProcessInfo.POLICY_VERSION_ID=oldPolicyVersion; 
					base.CommitTransaction(); 
//				}					
//				catch(Exception exx)
//				{
//					base.RollbackTransaction();
//					ClsPolicyProcess.PrintingErrorFlag = true;
//					//throw new Exception("Process Commited but Unable to generate PDF",exx);				
//					ClsPolicyErrMsg.strMessage = "Process Committed but Unable to generate PDF";
//					return retval;
//
//				}
				
			
				
				
				return retval;
			}
			catch(Exception ex)
			{
				base.RollbackTransaction();
				throw(ex);
			}
		}
		public void AddDiaryEntry(ClsProcessInfo objProcessInfo,string strSubjectLine,string strNotes,bool RuleViolate,Cms.DataLayer.DataWrapper objNewWrapper,string strCalledFrom)
		{
			try
			{
				Cms.BusinessLayer.BlCommon.ClsDiary objClsDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
				Cms.Model.Diary.TodolistInfo objModel = new Cms.Model.Diary.TodolistInfo();
				if (IsEODProcess)
					objModel.FROMUSERID  = EODUserID;
				else
					objModel.FROMUSERID  = objProcessInfo.CREATED_BY;
				//objModel.TOUSERID           =   objProcessInfo.UNDERWRITER;     
				objModel.SUBJECTLINE = strSubjectLine; 
				objModel.NOTE =   strNotes; 
				objModel.PROCESS_ROW_ID =objProcessInfo.ROW_ID; 
				if (strCalledFrom=="COMMITANYWAY")
					objModel.LISTTYPEID = (int)BlCommon.ClsDiary.enumDiaryType.REWRITE_REQUESTS ;
				else
					objModel.LISTTYPEID = (int)BlCommon.ClsDiary.enumDiaryType.PROCESS_PENDING_REQUEST; //     .15;
				objModel.POLICY_ID = objProcessInfo.POLICY_ID;
				if (objProcessInfo.NEW_POLICY_VERSION_ID !=0)
					objModel.POLICY_VERSION_ID = objProcessInfo.NEW_POLICY_VERSION_ID ;
				else
					objModel.POLICY_VERSION_ID = objProcessInfo.POLICY_VERSION_ID ;
				objModel.CUSTOMER_ID = objProcessInfo.CUSTOMER_ID;
				if (IsEODProcess)
					objModel.CREATED_BY = EODUserID;
				else
					objModel.CREATED_BY = objProcessInfo.CREATED_BY;
				objModel.LISTOPEN           =   "Y";
				objModel.CREATED_DATETIME = DateTime.Now;
				objModel.LAST_UPDATED_DATETIME =DateTime.Now;
				objModel.RECDATE = DateTime.Now;
				objModel.MODULE_ID = (int)BlCommon.ClsDiary.enumModuleMaster.POLICY_PROCESS;   
				objModel.LOB_ID		= objProcessInfo.LOB_ID; 
				if (RuleViolate==true)
				{
					objModel.RULES_VERIFIED =1;
					if (objNewWrapper!=null)
						objClsDiary.DiaryEntryfromSetup(objModel,objNewWrapper);
					else
					{
						objClsDiary.DiaryEntryfromSetup(objModel);
					}
				}
				else
				{
					objModel.RULES_VERIFIED =0;
					
					if (objNewWrapper!=null)
						objClsDiary.DiaryEntryfromSetup(objModel,objNewWrapper);
					else
					{
						objWrapper.ClearParameteres(); 
						objClsDiary.DiaryEntryfromSetup(objModel,objWrapper);
					}
				}
				//objClsDiary.Add(objModel,"");
			}
			catch(Exception ex)
			{
				throw(ex); 
			}
			
		}
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
				TransactionDescription.Append("New version (" + objProcessInfo.NEW_POLICY_VERSION_ID.ToString("#.0") 
					+ ") of policy has been deleted.;");

				
				//Writing the transaction log
				WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, 
					GetTransactionLogDesc(objProcessInfo.PROCESS_ID) , objProcessInfo.COMPLETED_BY, TransactionDescription.ToString() );
				

				//Commiting  the transaction
				base.CommitTransaction();

				return retval;
			}
			catch(Exception ex)
			{
				base.RollbackTransaction();
				throw(ex);
			}
		}

		///<summary> It will request Updated Mvr
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <param name="POLICY_ID"></param>
		/// <param name="Policy_varsion_ID"></param>
		/// <returns></returns> by pravesh
		private DataSet RequestMVR(int intCUSTOMER_ID, int intPOLICY_ID, int intPOLICY_VERSION_ID)
		{
			objWrapper.ClearParameteres();
			objWrapper.AddParameter("@CUSTOMER_ID",intCUSTOMER_ID);
			objWrapper.AddParameter("@POLICY_ID",intPOLICY_ID );
			objWrapper.AddParameter("@POLICY_VERSION_ID",intPOLICY_VERSION_ID);
			
			try
			{
				DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_CheckMVR_Requird");
				objWrapper.ClearParameteres();
				return dsTemp;
			}
			catch(Exception ex)
			{
				throw(ex);
			}

		}

		/// <summary>
		/// It will check for Insurance score validity.
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <returns></returns>
		private bool CheckInsuranceScoreValidity(int CustomerID)
		{
			int returnResult = 0;
			objWrapper.ClearParameteres();

			objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
			SqlParameter objSqlParameter  = (SqlParameter) objWrapper.AddParameter("@RETURN_VALUE",SqlDbType.Int,ParameterDirection.Output);
							
			returnResult	=  objWrapper.ExecuteNonQuery("Proc_CheckValidity_Insurance_Score");
				
			if ( objSqlParameter.Value != System.DBNull.Value )
			{
				returnResult = Convert.ToInt32(objSqlParameter.Value);
			}
			
			objWrapper.ClearParameteres();	

			if (returnResult == 1)
				return false;
			else
				return true;
				
					
		}

		/// <summary>
		/// It will get the Insurance Score.
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <returns></returns>
		private int GetInsertInsuranceScore(int CustomerID,int CreatedBy)
		{
			Cms.BusinessLayer.BlClient.ClsCustomer  objCustomer = new  Cms.BusinessLayer.BlClient.ClsCustomer();
			Cms.Model.Client.ClsCustomerInfo objCustInfo = new Cms.Model.Client.ClsCustomerInfo();
			Cms.Model.Client.ClsCustomerInfo objCustOldInfo = new Cms.Model.Client.ClsCustomerInfo();

            System.Collections.Specialized.NameValueCollection dic = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationManager.GetSection("IIXSettings");
			string strUserName = dic["UserName"].ToString();
			string strPassword = dic["Password"].ToString();
			string strAccountNumber = dic["AccountNumber"].ToString();
			string strUrl = dic["URL"].ToString();
			Cms.CmsWeb.Utils.Utility objUtility = new Cms.CmsWeb.Utils.Utility(strUserName, strPassword, strAccountNumber,strUrl);	

			Cms.CmsWeb.Utils.CreditScoreDetails objScore;
			
			int intScore = -1;
			
			try
			{
				DataSet dsCustomer = Cms.BusinessLayer.BlClient.ClsCustomer.GetCustomerDetails(CustomerID);
				PopulateModelObject(objCustInfo,dsCustomer.GetXml().ToString());
				PopulateModelObject(objCustOldInfo,dsCustomer.GetXml().ToString());
				objScore = objUtility.GetCustomerCreditScore(objCustInfo);
				intScore = objScore.Score;
			}
			catch(Exception ex)
			{
				if (ex.Message.IndexOf("ADDRESS_NOT_VALIDATED")!=-1) //refer policy
				{
					return -3;
				}
				else
				{
					throw (ex);
				}			
			}
			
//			if ( intScore == -1 )
//			{
//				return 0;
//			}
			
			objCustInfo.CustomerInsuranceScore = intScore;
			objCustInfo.FACTOR1 = objScore.FirstFactor;
			objCustInfo.FACTOR2 = objScore.SecondFactor;
			objCustInfo.FACTOR3 = objScore.ThirdFactor;
			objCustInfo.FACTOR4 = objScore.FourthFactor;
			objCustInfo.MODIFIED_BY=CreatedBy;
			//Update the insurance score details in the database
			int result=objCustomer.SetInsuranceScore(objCustInfo,objCustOldInfo); 

			return result;
		
		}
		/// <summary>
		/// It will Fetch MVR for drivers.
		/// </summary>
		/// <param name=dsDrivers></param>
		/// <param name=""></param>
		private void InsertDriverMVR(DataSet dsUpdatedMVR,ClsProcessInfo objProcessInfo)
		{
			if (dsUpdatedMVR.Tables[0].Rows.Count>0) //fetch mvr
			{
				/////
				DataSet objDSDriverViolDetail;
				System.Xml.XmlNode   objNode= null;
				string strXmlQuery;
                System.Collections.Specialized.NameValueCollection dic = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationManager.GetSection("IIXSettings");
				string strUserName = dic["UserName"].ToString();
				string strPassword = dic["Password"].ToString();
				string strAccountNumber = dic["AccountNumber"].ToString();
				string strUrl = dic["URL"].ToString();
				Cms.CmsWeb.Utils.Utility objMVRUtil = new Cms.CmsWeb.Utils.Utility(strUserName, strPassword, strAccountNumber,strUrl );
				Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
				int nCount;
				string strLOBID="";
				string strStateID;
				// for each driver or operator getting a list of violation from iix web service
				for ( nCount =0; nCount <= dsUpdatedMVR.Tables[0].Rows.Count -1 ; nCount++)
				{
					string strDateOfBirth="";
					if (dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_DOB"]!= null)
					{
						strDateOfBirth=Convert.ToDateTime(dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_DOB"].ToString()).ToString("MMddyyyy");   
					}
					strLOBID=dsUpdatedMVR.Tables[0].Rows[nCount]["LOB_ID"].ToString(); 
					strStateID=	Convert.ToString(dsUpdatedMVR.Tables[0].Rows[nCount]["STATE_ID"].ToString());
					// xml retrived from web service
					System.Xml.XmlDocument   objDriverResponse;
					objDriverResponse = new System.Xml.XmlDocument();
				
					objDriverResponse = objMVRUtil.GetViolation(dsUpdatedMVR.Tables[0].Rows[nCount]["STATE_CODE"]==null?"":dsUpdatedMVR.Tables[0].Rows[nCount]["STATE_CODE"].ToString(),
						dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_DRIV_LIC"]==null?"":dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_DRIV_LIC"].ToString(),
						dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_LNAME"]==null?"":dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString(), 
						dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_FNAME"]==null?"":dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString() ,
						dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_SUFFIX"]==null?"":dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_SUFFIX"].ToString() ,
						dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_MNAME"]==null?"":dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_MNAME"].ToString() ,
						strDateOfBirth ,
						dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_SEX"]== null?"":dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_SEX"].ToString() ,"",objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.NEW_POLICY_VERSION_ID.ToString());
					if(objDriverResponse.DocumentElement.SelectNodes("Error").Count>0)
					{
						continue;
					}
				
					if (objDriverResponse.DocumentElement.SelectNodes("Violation").Count >0)
					{
						// if violations come from iix web service 
						int nViolationRec= objDriverResponse.DocumentElement.ChildNodes.Count; 
						string strViolationCode="";
						int nNode;
						// getting all violation code 
						for (nNode =0; nNode<= nViolationRec - 1; nNode++)
						{
							
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
						if(strViolationCode=="")
						{
							strViolationCode="''";
						}
						// mapp all violation code with wolverine violation code and get details 
						objDSDriverViolDetail= objGenInfo.GetViolationRecords(strViolationCode,strLOBID.ToString(),strStateID);
					
					

						// creating a mver object to store all info retrived 
						//Cms.Model.Application.ClsMvrInfo objMVRInfo= new Cms.Model.Application.ClsMvrInfo();
						Cms.Model.Policy.ClsPolicyAutoMVR objMVRInfo= new Cms.Model.Policy.ClsPolicyAutoMVR(); 
						objMVRInfo.POLICY_ID =objProcessInfo.NEW_POLICY_ID;
						objMVRInfo.CUSTOMER_ID = int.Parse(objProcessInfo.CUSTOMER_ID.ToString());
						objMVRInfo.POLICY_VERSION_ID = objProcessInfo.NEW_POLICY_VERSION_ID ;
						objMVRInfo.CREATED_BY= objProcessInfo.CREATED_BY;
						objMVRInfo.CREATED_DATETIME = DateTime.Now;  

						string[] objArrCode = strViolationCode.Split(','); 
						bool bIsExist; 
						string strDescription="";
						string strVcode =""; 
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
									strDescription=objNode.Attributes["viol_description"].InnerText.Trim()   ; 
									strVcode =objNode.Attributes["AViolation_code"].InnerText.Trim(); 
									string strMVRDate =objNode.Attributes["viol_date"].InnerText.Trim(); 
									if (strMVRDate!="")
									{
										strMVRDate=strMVRDate.Substring(0,2).ToString()  + "/" +  strMVRDate.Substring(2,2) + "/" + strMVRDate.Substring(4,4);
										objMVRInfo.MVR_DATE= Convert.ToDateTime(strMVRDate);
									}
									
									objMVRInfo.DRIVER_ID =int.Parse(dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_ID"].ToString()) ;
									int resultID=objGenInfo.InsertUnmatchedMVRViolationDetail(objMVRInfo,strVcode,strDescription,"0","0",false);
									if (resultID==-1) continue;
									objMVRInfo.MVR_DEATH ="N"; 
									objMVRInfo.MVR_AMOUNT=0; 
									objMVRInfo.VIOLATION_ID =resultID;
									objMVRInfo.IS_ACTIVE ="Y";
									//objMVRInfo.VIOLATION_TYPE =int.Parse("13220");
									Cms.BusinessLayer.BlApplication.ClsMvrInformation objMvr = new Cms.BusinessLayer.BlApplication.ClsMvrInformation();
								
									//if ppa or motercycle
									if (strLOBID == "2" )
									{
										objMvr.Add(objMVRInfo, "PPA");
									}
									else if ( strLOBID == "3")
									{
										objMvr.Add(objMVRInfo, "MOT");
									}
								}
							}

						}
						// now insert all mapped violations and its details 
						if (objDSDriverViolDetail != null)
						{
							if (objDSDriverViolDetail.Tables[0] != null)
							{
								for (int nRecord =0; nRecord <=objDSDriverViolDetail.Tables[0].Rows.Count -1 ;nRecord ++)
								{
									strXmlQuery="/ResultData/Violation[@AViolation_code='"+objDSDriverViolDetail.Tables[0].Rows[nRecord]["SSV_CODE"].ToString().Trim()  +"']";
									objNode=objDriverResponse.DocumentElement.SelectSingleNode(strXmlQuery); 
						
									objMVRInfo.DRIVER_ID=  int.Parse (dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_ID"]==null?"": dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_ID"].ToString() ); 
									if (objNode.Attributes["viol_date"].InnerText !="")
									{
										string date = objNode.Attributes["viol_date"].InnerText ; 
										objMVRInfo.MVR_DATE= new DateTime(int.Parse( date.Substring(4,4)),int.Parse(date.Substring(0,2)),int.Parse(date.Substring(2,2))) ;
									}
									objMVRInfo.MVR_DEATH ="N"; 
									objMVRInfo.MVR_AMOUNT=0; 
									objMVRInfo.VIOLATION_ID =int.Parse ( objDSDriverViolDetail.Tables[0].Rows[nRecord]["VIOLATION_ID"]== null?"":objDSDriverViolDetail.Tables[0].Rows[nRecord]["VIOLATION_ID"].ToString()) ;
									objMVRInfo.IS_ACTIVE =objDSDriverViolDetail.Tables[0].Rows[nRecord]["IS_ACTIVE"]== null?"":objDSDriverViolDetail.Tables[0].Rows[nRecord]["IS_ACTIVE"].ToString() ;
									Cms.BusinessLayer.BlApplication.ClsMvrInformation objMvr = new Cms.BusinessLayer.BlApplication.ClsMvrInformation();
									// if watercraft 
									if (strLOBID == "4")
									{
										objMvr.AddForWater(objMVRInfo);
										
									}
										//if ppa or motercycle
									else if (strLOBID == "2" )
									{
										objMvr.Add(objMVRInfo, "PPA");
										
									}
									else if ( strLOBID == "3")
									{
										objMvr.Add(objMVRInfo, "MOT");
										
									}
						
								}
							}
						}
					} //END IF
				} //END FOR
			}
		}

		/// <summary>
		/// Update Vehicle Class for Motor while fetching MVR
		/// </summary>
		/// <param name="dsDrivers"></param>
		/// <param name="objProcessInfo"></param>
		private void UpdateVehicleClassForMotor(DataSet dsDrivers,ClsProcessInfo objProcessInfo)
		{
			ClsDriverDetail objDriverDetail=new ClsDriverDetail(); 			
			objDriverDetail.UpdateMotorVehicleClassPOL(objWrapper,objProcessInfo.CUSTOMER_ID ,objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID);
		}

		/// <summary>
		/// add diary entry for rule violalation while committing rewrite process
		/// </summary>
		/// <param name="objProcessInfo"></param>
		private void AddDiaryEntryForRulesViolation(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo)
		{
			Cms.Model.Diary.TodolistInfo objModelInfo = new Cms.Model.Diary.TodolistInfo();			
			Cms.BusinessLayer.BlCommon.ClsDiary objDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
			try
			{
				if (IsEODProcess)
					objModelInfo.FROMUSERID   = EODUserID;
				else
					objModelInfo.FROMUSERID         =   int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());    						
				//objModelInfo.TOUSERID           =   objProcessInfo.UNDERWRITER;            
				objModelInfo.LISTTYPEID         =   (int)BlCommon.ClsDiary.enumDiaryType.REWRITE_IN_SUSPENSE;//    25;//Rewrite in Suspense
				//objModelInfo.PRIORITY           =   "H";//Medium Priority
				objModelInfo.SUBJECTLINE		=   "Rewrite in Suspense";			
				objModelInfo.NOTE				= objProcessInfo.COMMENTS ; 
				objModelInfo.NOTE+=" (Please click on the image to see the list of rules violated)";
				//objModelInfo.SYSTEMFOLLOWUPID   =   15;
				objModelInfo.LISTOPEN           =   "Y";
				objModelInfo.CUSTOMER_ID		=	objProcessInfo.CUSTOMER_ID;									
				objModelInfo.POLICY_ID			=	objProcessInfo.POLICY_ID;
				objModelInfo.POLICY_VERSION_ID	=	objProcessInfo.NEW_POLICY_VERSION_ID; 
				//objModelInfo.FOLLOWUPDATE       =   System.DateTime.Now; 
				//objModelInfo.STARTTIME            =	System.DateTime.Now;
				//objModelInfo.ENDTIME              =	System.DateTime.Today.AddDays(7);
				objModelInfo.RECDATE = DateTime.Now;
				if (IsEODProcess)
					objModelInfo.CREATED_BY = objModelInfo.MODIFIED_BY = EODUserID;
				else
					objModelInfo.CREATED_BY = objModelInfo.MODIFIED_BY = objProcessInfo.CREATED_BY;
				objModelInfo.CREATED_DATETIME = objModelInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;			
				objModelInfo.RULES_VERIFIED = 1;
				objModelInfo.PROCESS_ROW_ID	= objProcessInfo.ROW_ID;  
				objModelInfo.MODULE_ID	= (int)BlCommon.ClsDiary.enumModuleMaster.POLICY_PROCESS;
				objModelInfo.LOB_ID		= objProcessInfo.LOB_ID; 
				//objDiary.AddDiarySetup(objModelInfo,objWrapper); 
				objWrapper.ClearParameteres(); 
				objDiary.DiaryEntryfromSetup(objModelInfo,objWrapper); 
				//objDiary.Add(objModelInfo,"");
			}
			catch(Exception ex)
			{
				throw new Exception("Error while adding new diary entry for Rule Violation",ex);
			}
			finally
			{
				if(objModelInfo!=null)
					objModelInfo = null;
				if(objDiary!=null)
					objDiary=null;
			}

		}
		/// <summary>
		/// Update the Policy Display Version.
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <param name="PolicyID"></param>
		/// <param name="PolicyVersionID"></param>
		/// <param name="DisplayVersion"></param>
		/// <returns></returns>
		private int UpdatePolicyDisplayVersion(int CustomerID, int PolicyID, int PolicyVersionID, string DisplayVersion)
		{
			
			objWrapper.ClearParameteres();
			objWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
			objWrapper.AddParameter("@POLICY_ID",PolicyID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID);
			objWrapper.AddParameter("@POLICY_DISP_VERSION",DisplayVersion);
			

			int result = objWrapper.ExecuteNonQuery("Proc_UpdatePolicyDisplayVersion");
			objWrapper.ClearParameteres();
			return result;
		}	

		private void AdjustCoverages(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo)
		{
			//For Home and rental, adjust coverages
			if ( objProcessInfo.LOB_ID  == int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumLOB.HOME).ToString()) || objProcessInfo.LOB_ID == int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumLOB.REDW).ToString()))
			{
				//If policy type has changed, adjust coverages for each dwelling
				if ( objProcessInfo.POLICY_TYPE != 0)
				{
					objWrapper.ClearParameteres();
					
					objWrapper.AddParameter("@CUSTOMER_ID",objProcessInfo.CUSTOMER_ID);
					objWrapper.AddParameter("@POLICY_ID",objProcessInfo.POLICY_ID);
					objWrapper.AddParameter("@POLICY_VERSION_ID",objProcessInfo.NEW_POLICY_VERSION_ID);
					objWrapper.ExecuteNonQuery("Proc_ADJUST_DWELLING_COVERAGES_POLICY_NEW");
						
					objWrapper.ClearParameteres();
					ClsHomeCoverages objHomeCov ;
					if (objProcessInfo.LOB_ID == int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumLOB.REDW).ToString()))
					{
						objHomeCov=new ClsHomeCoverages("1");
					}
					else
					{
						objHomeCov=new ClsHomeCoverages();
					}
					objHomeCov.UpdateCoveragesByRulePolicy(objWrapper,objProcessInfo.CUSTOMER_ID,
						objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID,RuleType.AppDependent); 
					objHomeCov.UpdateCoveragesByRulePolicy(objWrapper,objProcessInfo.CUSTOMER_ID,
						objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID,RuleType.RiskDependent );

				}
			}
				
			//Update coverages according to Effective date
			//if ( objProcessInfo.EFFECTIVE_DATETIME != objOld.APP_EFFECTIVE_DATE)
			//{
				objWrapper.ClearParameteres();
					
				objWrapper.AddParameter("@CUSTOMER_ID",objProcessInfo.CUSTOMER_ID);
				objWrapper.AddParameter("@POLICY_ID",objProcessInfo.POLICY_ID);
				objWrapper.AddParameter("@POLICY_VERSION_ID",objProcessInfo.NEW_POLICY_VERSION_ID);
				objWrapper.ExecuteNonQuery("Proc_ADJUST_POL_COVERAGES");
			//}
			//Adjust coverages based on rules
			if(objProcessInfo.LOB_ID  == int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumLOB.HOME).ToString()) || objProcessInfo.LOB_ID == int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumLOB.BOAT).ToString()))
			{
					objWrapper.ClearParameteres();
					ClsWatercraftCoverages objWatCov = new ClsWatercraftCoverages();
					objWatCov.UpdateCoveragesByRulePolicy(objWrapper,objProcessInfo.CUSTOMER_ID ,objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID ,RuleType.AppDependent);
			}
		}

        public void CreatePolicyNewVersionForRewrite(int PolicyId, int PolicyVersionId, int CustomerId, string CreatedBy, out int NewVersionID, out string NewDisp_version, out string NewDisp_version_Rewritable)
		{
			int returnResult = 0;
	
			try
			{				
				objWrapper.AddParameter("@CUSTOMER_ID", CustomerId, SqlDbType.Int);
				objWrapper.AddParameter("@POLICY_ID", PolicyId, SqlDbType.Int);
				objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId, SqlDbType.Int);
				objWrapper.AddParameter("@CREATED_BY",CreatedBy,SqlDbType.Int);
				SqlParameter objSqlParameter  = (SqlParameter) objWrapper.AddParameter("@NEW_VERSION",SqlDbType.Int,ParameterDirection.Output);
				//SqlParameter objSqlParameter1  = (SqlParameter) objWrapper.AddParameter("@NEW_DISP_VERSION",SqlDbType.NVarChar,ParameterDirection.Output);
                SqlParameter objSqlParameter1 = (SqlParameter)objWrapper.AddParameter("@NEW_DISP_VERSION", null, SqlDbType.NVarChar, ParameterDirection.Output, 50);

				//Added by Anurag Verma on 21/03/2007 for receiving ineligible coverage count for diary entry
				SqlParameter objSqlParameter3  = (SqlParameter) objWrapper.AddParameter("@INVALID_COVERAGE",SqlDbType.Int,ParameterDirection.Output);
                SqlParameter objSqlParameter4 = (SqlParameter)objWrapper.AddParameter("@NEW_DISP_VERSION_REWRITABLE",null,SqlDbType.NVarChar, ParameterDirection.Output,50);
				
				//1 is passed in case of renewal and 0 is default value IN Case of Rewrite Pass 3 
				objWrapper.AddParameter("@Renewal","3",SqlDbType.Int);

                int new_APP_Version_ID;
                    //int_New_Disp_version,
                string New_Disp_version_Rewritable;
                string string_New_Disp_version;
				returnResult	=	objWrapper.ExecuteNonQuery("Proc_PolicyCreateNewVersion");

				objWrapper.ClearParameteres();					

				new_APP_Version_ID = int.Parse(objSqlParameter.Value.ToString());
				//int_New_Disp_version = 
                string_New_Disp_version =  objSqlParameter1.Value.ToString();
                New_Disp_version_Rewritable = objSqlParameter4.Value.ToString();
				NewVersionID = new_APP_Version_ID;
                NewDisp_version = string_New_Disp_version;
                NewDisp_version_Rewritable = New_Disp_version_Rewritable;

				//Added by Anurag verma on 21/03/2007 for making diary entry
				int intErrorCnt=int.Parse(objSqlParameter3.Value.ToString());
				if(intErrorCnt>0)
					base.AddDiaryEntry(CustomerId,PolicyId,new_APP_Version_ID);

				#region Update Underwriting Tier
				ClsUnderwritingTier objTier = new ClsUnderwritingTier();
				objTier.UpdateUnderwritingTier(CustomerId,PolicyId,new_APP_Version_ID,"POLICY","Y",objWrapper);				
				#endregion


			}
			catch(Exception ex)
			{
				throw( new Exception("Error occured while creating new version of policy.\n" + ex.Message));
			}
						
		}

/// <summary>
/// set new Policy number while rewriting a Policy
/// </summary>
/// <param name="CUSTOMER_ID"></param>
/// <param name="POLICY_ID"></param>
/// <param name="POLICY_VERSION_ID"></param>
/// <param name="newPolicynumber"></param>
		private void SetNewPolicyNumber(int CUSTOMER_ID,int POLICY_ID,int POLICY_VERSION_ID, out string  newPolicynumber )
		{
			try
			{
				objWrapper.ClearParameteres();
				objWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);
				objWrapper.AddParameter("@POLICY_ID",POLICY_ID);
				objWrapper.AddParameter("@POLICY_VERSION_ID",POLICY_VERSION_ID);
			
				SqlParameter objSqlParameter  = (SqlParameter) objWrapper.AddParameter("@NEW_POLICY_NUMBER",null,SqlDbType.VarChar,ParameterDirection.Output,100);
				objWrapper.ExecuteNonQuery("Proc_RewritePolicyNumber");
				newPolicynumber = objSqlParameter.Value.ToString();
				objWrapper.ClearParameteres();
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
        private void SetNewPolicyNumber(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, out string newPolicynumber, DateTime RewriteEffectiveDate, int ProcessID, DataWrapper objWrapper)
        {
            try
            {
                string strDiv="", APP_EFF_DATE="";

                int LOb= 0, CO_INSURANCE=0;
                DateTime dtAPP_EFF_DATE = RewriteEffectiveDate;
                ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
              DataSet  ds = objGeneralInformation.GetPolicyDataSet(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    strDiv = ds.Tables[0].Rows[0]["DIV_ID"].ToString();
                    APP_EFF_DATE = ds.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString();
                    LOb = int.Parse(ds.Tables[0].Rows[0]["POLICY_LOB"].ToString());
                    if (ds.Tables[0].Rows[0]["CO_INSURANCE"].ToString() != "")
                        CO_INSURANCE = int.Parse(ds.Tables[0].Rows[0]["CO_INSURANCE"].ToString());
                }
                //if (APP_EFF_DATE != "")
                 //   dtAPP_EFF_DATE = ConvertDBDateToCulture(APP_EFF_DATE); //(APP_EFF_DATE);

                newPolicynumber = ClsGeneralInformation.GenerateAppPolNumber(LOb, int.Parse(strDiv), ConvertToDate(dtAPP_EFF_DATE.ToString()), "POL", CO_INSURANCE);

                int retval = objGeneralInformation.UpdatePolicyNo(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, newPolicynumber, ProcessID, objWrapper);


                //objWrapper.ClearParameteres();
                //objWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                //objWrapper.AddParameter("@POLICY_ID", POLICY_ID);
                //objWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);

                //SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@NEW_POLICY_NUMBER", null, SqlDbType.VarChar, ParameterDirection.Output, 100);
                //objWrapper.ExecuteNonQuery("Proc_RewritePolicyNumber");
                //newPolicynumber = objSqlParameter.Value.ToString();
                //objWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
	}
}
