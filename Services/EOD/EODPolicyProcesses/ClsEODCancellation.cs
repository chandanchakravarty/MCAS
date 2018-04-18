using System;
using EODCommon;
using Cms.BusinessLayer.BlProcess;
using System.Data ;
using System.Data.SqlClient ;
using Cms.Model.Policy.Process;
using Cms.DataLayer;
using Cms.BusinessLayer.BlAccount;
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Diary;

namespace EODPolicyProcesses
{
	/// <summary>
	/// Summary description for ClsEODCancellation.
	/// </summary>
	public class ClsEODCancellation:ClsEODCommon 
	{
		
		public ClsEODCancellation()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region LaunchCancellation method
		public void LaunchCancellation()
		{
			DataTable dtPolicies;
			ClsAddNSFEntry objFees = new ClsAddNSFEntry();
			EODLogInfo objLog = new EODLogInfo();
			objLog.Activity = (int)EODActivities.PolicyProcess ;
			objLog.SubActivity = (int) EODActivities.CancellationLaunch ;

			try
			{
				objLog.ActivtyDescription = "Fetching DB Policies to Cancel : ";
				objLog.StartDateTime = System.DateTime.Now ;
 				dtPolicies = GetDBPoliciesToCancel();
			}
			catch(Exception ex)
			{
				objLog.Status = ActivityStatus.FAILED; 
				objLog.EndDateTime = DateTime.Now;
				objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
				WriteLog(objLog);
				return;
			}
			objLog.ActivtyDescription += dtPolicies.Rows.Count.ToString() + " Records to process";
			objLog.Status= ActivityStatus.SUCCEDDED;
			objLog.EndDateTime = DateTime.Now;
			WriteLog(objLog);
            ClsCommon.BL_LANG_CULTURE = "en-US";
            ClsCommon.IsEODProcess = true;
			for(int i = 0 ; i<dtPolicies.Rows.Count; i++)
			{
				try
				{
					DataRow dr = dtPolicies.Rows[i];

					string FROM_AS400 = dr["FROM_AS400"].ToString();

					if(FROM_AS400 == "Y")
					{
						try
						{
							TodolistInfo objTodo=new TodolistInfo();
							ClsDiary objDiary = new ClsDiary();
							objTodo.FROMENTITYID  = EOD_USER_ID;
							objTodo.FROMUSERID    = EOD_USER_ID;
							objTodo.SUBJECTLINE = "Corrective user process is pending on Policy from AS400";
							objTodo.NOTE = "Policy is due for Non Payment DB Cancellation. But as policy is from AS400 system and corrective user is pending can not launch Past Due Cancellation";
							objTodo.LISTTYPEID  = 37;
							objTodo.CUSTOMER_ID = Convert.ToInt32(dr["CUSTOMER_ID"]);
							objTodo.POLICY_ID = Convert.ToInt32(dr["POLICY_ID"]);
							objTodo.POLICY_VERSION_ID = Convert.ToInt32(dr["POLICY_VERSION_ID"]);
							objTodo.CREATED_BY  = EOD_USER_ID;
							objTodo.CREATED_DATETIME = DateTime.Now;
							objTodo.LAST_UPDATED_DATETIME =  DateTime.Now;;
							objTodo.RECDATE =  DateTime.Now;
							objTodo.FOLLOWUPDATE =  DateTime.Now.AddDays(1);
							objTodo.LISTOPEN = "Y";
							objTodo.MODULE_ID = (int)ClsDiary.enumModuleMaster.POLICY_PROCESS;  
							objTodo.LOB_ID = Convert.ToInt32(dr["LOB_ID"]);
							objDiary.DiaryEntryfromSetup(objTodo);
						}
						catch(Exception ex)
						{
						}
						continue;
					}

					ClsProcessInfo objProcessInfo = new ClsProcessInfo();
					DateTime CancellationEffectiveDate ;
                    ClsCommon.SetCustomizedXml(ClsCommon.BL_LANG_CULTURE);
					DateTime PolExpiryDate = Convert.ToDateTime(dr["POL_EXPIRATION_DATE"]);

					int CancelGracePeriod = Convert.ToInt32(dr["CANCEL_GRACE_PERIOD"]);

					objProcessInfo.PROCESS_ID = 2;
					objProcessInfo.CUSTOMER_ID = Convert.ToInt32(dr["CUSTOMER_ID"]);
					objProcessInfo.POLICY_ID   = Convert.ToInt32(dr["POLICY_ID"]);
					objProcessInfo.POLICY_VERSION_ID = Convert.ToInt32(dr["POLICY_VERSION_ID"]);
					objProcessInfo.LOB_ID = Convert.ToInt32(dr["LOB_ID"]);
					objProcessInfo.CREATED_BY = EOD_USER_ID;
					objProcessInfo.COMPLETED_BY = EOD_USER_ID;
					objProcessInfo.CREATED_DATETIME = System.DateTime.Now;
					objProcessInfo.CANCELLATION_TYPE =(int)ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.NON_PAYMENT;
					objProcessInfo.BILL_TYPE= ClsCancellationProcess.BILL_PAYMENT_DIRECT_BILL;
					//Cancellation Option , Effective Date  and Premium to be calculated 
					//Reason  = Non Payment
					//Other Reason = Non Payment

					objLog.ActivtyDescription  = "Initializing Cancellation Parameters : ";
					objLog.StartDateTime = DateTime.Now;
                    objLog.ClientID = objProcessInfo.CUSTOMER_ID;
					objLog.PolicyID = objProcessInfo.POLICY_ID ;
					objLog.PolicyVersionID = objProcessInfo.POLICY_VERSION_ID ;
					
					//First Installment not paid
					if(Convert.ToInt32(dr["PARTIAL_PAID"])  != 1 ) // && Convert.ToInt32(dr["INSTALLMENT_NO"]) == 1)
					{
						//If Cancellation after renewal it will be flat cancellation
						if(Convert.ToInt32(dr["BASE_PROCESS"]) == ClsPolicyProcess.POLICY_COMMIT_RENEWAL_PROCESS)
						{
							objProcessInfo.CANCELLATION_OPTION = (int)CancellationMethod.Flat ;
						
							//Will be cancelled w.e.f effective date
							DateTime PolEffDate = Convert.ToDateTime(dr["POL_EFFECTIVE_DATE"]);

							CancellationEffectiveDate = new DateTime( PolEffDate.Year,PolEffDate.Month,
								PolEffDate.Day,12,1,0);
							objProcessInfo.EFFECTIVE_DATETIME  = CancellationEffectiveDate;

							//Due Date will be EOD_DATE + 1 + Grace Period
							objProcessInfo.DUE_DATE = DateTime.Now.AddDays(CancelGracePeriod);

							//If due date is Saturday or Sunday advance it to next monday.
							if(objProcessInfo.DUE_DATE.DayOfWeek == DayOfWeek.Saturday )
							{
								objProcessInfo.DUE_DATE = objProcessInfo.DUE_DATE.AddDays(2);
							}
							else if(objProcessInfo.DUE_DATE.DayOfWeek == DayOfWeek.Sunday)
							{
								objProcessInfo.DUE_DATE = objProcessInfo.DUE_DATE.AddDays(1);
							}

							if(objProcessInfo.DUE_DATE > PolExpiryDate) 
							{
								objProcessInfo.DUE_DATE =PolExpiryDate;
							}

							objLog.ActivtyDescription += "Calculating cancellation premium:";

							// In case of equity cancellation Cancellation premium will be calculated based 
							// on equity date instead of actual cancellaton date
							objProcessInfo.RETURN_PREMIUM  = ClsPolicyProcess.CalculateReturnPremium(objProcessInfo.CUSTOMER_ID,
								objProcessInfo.POLICY_ID , objProcessInfo.POLICY_VERSION_ID,objProcessInfo.EFFECTIVE_DATETIME );

							objProcessInfo.REASON  = (int)CancellationReason.CancelledFlatForNonPayment ;
							objProcessInfo.OTHER_REASON = CancellationReason.CancelledFlatForNonPayment.ToString();
						}
							//In case of Cancellation after NBS pro - rata method will be used
						else if(Convert.ToInt32(dr["BASE_PROCESS"]) == ClsPolicyProcess.POLICY_COMMIT_NEW_BUSINESS_PROCESS )
						{
							objProcessInfo.CANCELLATION_OPTION = (int)CancellationMethod.ProRata;
						
							CancellationEffectiveDate = new DateTime(DateTime.Now.Year,DateTime.Now.Month,
								DateTime.Now.Day,12,1,0);

							//Will be cancelled w.e.f EOD Date + Grace Period
							objProcessInfo.EFFECTIVE_DATETIME  =CancellationEffectiveDate.AddDays(CancelGracePeriod + 1);

							//Due Date will be EOD_DATE + Grace Period
							objProcessInfo.DUE_DATE = DateTime.Now.AddDays(CancelGracePeriod);
							
							//If due date is Saturday or Sunday advance it to next monday.
							if(objProcessInfo.DUE_DATE.DayOfWeek == DayOfWeek.Saturday )
							{
								objProcessInfo.DUE_DATE = objProcessInfo.DUE_DATE.AddDays(2);
							}
							else if(objProcessInfo.DUE_DATE.DayOfWeek == DayOfWeek.Sunday)
							{
								objProcessInfo.DUE_DATE = objProcessInfo.DUE_DATE.AddDays(1);
							}

							if(objProcessInfo.DUE_DATE > PolExpiryDate) 
							{
								objProcessInfo.DUE_DATE =PolExpiryDate;
							}

							if(objProcessInfo.EFFECTIVE_DATETIME > PolExpiryDate) 
							{
								objProcessInfo.DUE_DATE =PolExpiryDate;
							}

							objLog.ActivtyDescription += "Calculating cancellation premium:";
							
							objProcessInfo.RETURN_PREMIUM  = ClsPolicyProcess.CalculateReturnPremium(objProcessInfo.CUSTOMER_ID,
								objProcessInfo.POLICY_ID , objProcessInfo.POLICY_VERSION_ID,objProcessInfo.EFFECTIVE_DATETIME  );

							objProcessInfo.REASON = (int) CancellationReason.ProRateCancelledForNonPayment ;
							objProcessInfo.OTHER_REASON = CancellationReason.ProRateCancelledForNonPayment.ToString() ;
						}
						else
						{
							objProcessInfo.CANCELLATION_OPTION = (int) CancellationMethod.Equity;
							//Span of policy will be adjusted as per the payment given
                            //DateTime EquityDate = ClsPolicyProcess.GetCancellationDateForEquity(objProcessInfo.CUSTOMER_ID,
                            //    objProcessInfo.POLICY_ID , objProcessInfo.POLICY_VERSION_ID );
                            DateTime EquityDate = DateTime.Now;
                            DataTable dt = ClsCancellationProcess.GetPolicyCancellationDate(objProcessInfo.CUSTOMER_ID,
                                objProcessInfo.POLICY_ID , objProcessInfo.POLICY_VERSION_ID );

                            if (dt != null && dt.Rows.Count > 0)
                                if (dt.Rows[0]["CANCELLATION_DATE"].ToString() != "")
                                    EquityDate = DateTime.Parse(dt.Rows[0]["CANCELLATION_DATE"].ToString());
                                else
                                {
                                    EquityDate = DateTime.Now;
                                }

							DateTime tempDate = DateTime.Today.AddDays(CancelGracePeriod);
						
							if(EquityDate > tempDate )
							{
								CancellationEffectiveDate = new DateTime( EquityDate.Year,EquityDate.Month,
									EquityDate.Day,12,1,0);
							}
							else
							{
								CancellationEffectiveDate = new DateTime( tempDate.Year,tempDate.Month,
									tempDate.Day,12,1,0);
							}
		
							objProcessInfo.EFFECTIVE_DATETIME  = CancellationEffectiveDate.AddDays(1);

							if(objProcessInfo.EFFECTIVE_DATETIME > PolExpiryDate)
							{
								objProcessInfo.EFFECTIVE_DATETIME = PolExpiryDate;
							}

							//Due Date will be Cancellation Effective Date less 1 in case of equity cancelation
							objProcessInfo.DUE_DATE = CancellationEffectiveDate; 

							if(objProcessInfo.DUE_DATE.DayOfWeek == DayOfWeek.Saturday )
							{
								objProcessInfo.DUE_DATE = objProcessInfo.DUE_DATE.AddDays(2);
							}
							else if(objProcessInfo.DUE_DATE.DayOfWeek == DayOfWeek.Sunday)
							{
								objProcessInfo.DUE_DATE  = objProcessInfo.DUE_DATE.AddDays(1);
							}

							if(objProcessInfo.DUE_DATE > PolExpiryDate)
							{
								objProcessInfo.DUE_DATE = PolExpiryDate;
							}

							objLog.ActivtyDescription += "Calculating cancellation premium:";
							// In case of equity cancellation Cancellation premium will be calculated based 
							// on equity date instead of actual cancellaton date
							objProcessInfo.RETURN_PREMIUM  = ClsPolicyProcess.CalculateReturnPremium(objProcessInfo.CUSTOMER_ID,
								objProcessInfo.POLICY_ID , objProcessInfo.POLICY_VERSION_ID,objProcessInfo.EFFECTIVE_DATETIME);


							objProcessInfo.REASON = (int) CancellationReason.EquityCancelledForNonPayment ;
							objProcessInfo.OTHER_REASON = CancellationReason.EquityCancelledForNonPayment.ToString() ;
						}
					}
					else
					{
						objProcessInfo.CANCELLATION_OPTION = (int) CancellationMethod.Equity;
						//Span of policy will be adjusted as per the payment given
                        //DateTime EquityDate = ClsPolicyProcess.GetCancellationDateForEquity(objProcessInfo.CUSTOMER_ID,
                        //    objProcessInfo.POLICY_ID , objProcessInfo.POLICY_VERSION_ID );
                        DateTime EquityDate = DateTime.Now;
                        DataTable dt = ClsCancellationProcess.GetPolicyCancellationDate(objProcessInfo.CUSTOMER_ID,
                            objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID);

						DateTime tempDate = DateTime.Today.AddDays(CancelGracePeriod);
						
						if(EquityDate > tempDate )
						{
							CancellationEffectiveDate = new DateTime( EquityDate.Year,EquityDate.Month,
								EquityDate.Day,12,1,0);
						}
						else
						{
							CancellationEffectiveDate = new DateTime( tempDate.Year,tempDate.Month,
								tempDate.Day,12,1,0);
						}
		
						objProcessInfo.EFFECTIVE_DATETIME  = CancellationEffectiveDate.AddDays(1);

						if(objProcessInfo.EFFECTIVE_DATETIME > PolExpiryDate)
						{
							objProcessInfo.EFFECTIVE_DATETIME = PolExpiryDate;
						}

						//Due Date will be Cancellation Effective Date less 1 in case of equity cancelation
						objProcessInfo.DUE_DATE = CancellationEffectiveDate; 

						if(objProcessInfo.DUE_DATE.DayOfWeek == DayOfWeek.Saturday )
						{
							objProcessInfo.DUE_DATE = objProcessInfo.DUE_DATE.AddDays(2);
						}
						else if(objProcessInfo.DUE_DATE.DayOfWeek == DayOfWeek.Sunday)
						{
							objProcessInfo.DUE_DATE = objProcessInfo.DUE_DATE.AddDays(1);
						}

						if(objProcessInfo.DUE_DATE > PolExpiryDate)
						{
							objProcessInfo.DUE_DATE = PolExpiryDate;
						}

						objLog.ActivtyDescription += "Calculating cancellation premium:";
						// In case of equity cancellation Cancellation premium will be calculated based 
						// on equity date instead of actual cancellaton date
						objProcessInfo.RETURN_PREMIUM  = ClsPolicyProcess.CalculateReturnPremium(objProcessInfo.CUSTOMER_ID,
							objProcessInfo.POLICY_ID , objProcessInfo.POLICY_VERSION_ID,objProcessInfo.EFFECTIVE_DATETIME);


						objProcessInfo.REASON = (int) CancellationReason.EquityCancelledForNonPayment ;
						objProcessInfo.OTHER_REASON = CancellationReason.EquityCancelledForNonPayment.ToString() ;
					}
				
				     
					objLog.ActivtyDescription += "Setting printing options :";
					/****** Printing Defaults *******/
					//Insured  Print On Demand
					objProcessInfo.INSURED = (int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.MICHIGAN_MAILERS;
					//Add Inst   Commit To Print Spool
					objProcessInfo.ADD_INT = (int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.MICHIGAN_MAILERS;
					//Agency Print Commit To Print Spool
					objProcessInfo.AGENCY_PRINT = (int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.MICHIGAN_MAILERS;

					objProcessInfo.SEND_ALL = (int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.YES);
                    objProcessInfo.ENDORSEMENT_TYPE = 14685; //Default to Cancelation Endorsement 
				    
					objLog.Status = ActivityStatus.SUCCEDDED;
					objLog.EndDateTime = System.DateTime.Now;
					WriteLog(objLog);

					
					objLog.ActivtyDescription  = "Launching Cancellation process on policy : ";
					objLog.StartDateTime = DateTime.Now;
					objLog.ClientID = objProcessInfo.CUSTOMER_ID;
					objLog.PolicyID = objProcessInfo.POLICY_ID ;
					objLog.PolicyVersionID = objProcessInfo.POLICY_VERSION_ID ;

					string StatusDescription = "";
					ClsCancellationProcess objProcess = new ClsCancellationProcess();
                    objProcess.SystemID = ClsCommon.EODSystemID;
					if(objProcess.StartAndGenerateNotice(objProcessInfo,out StatusDescription))
					{
//						//Send Cancellation Notice will also update the cancellation notice sent to "Y"
//						objLog.ActivtyDescription += " Generating Cancellation Notices : ";
//						objProcess.GenerateCancellationNotices(objProcessInfo);

						//Problem with pdf generation
						if(ClsPolicyProcess.PrintingErrorFlag)
						{
							objLog.Status= ActivityStatus.FAILED ;
							objLog.EndDateTime = DateTime.Now;
							objLog.AdditionalInfo="Generation of Cancellation Notice failed";
							WriteLog(objLog);
						}
						else
						{
							objLog.Status = ActivityStatus.SUCCEDDED;
							objLog.EndDateTime = System.DateTime.Now;
							WriteLog(objLog);
						}
					}
					else
					{
						objLog.Status= ActivityStatus.FAILED ;
						objLog.EndDateTime = DateTime.Now;
						objLog.AdditionalInfo="Can't Launch cancellation : " + StatusDescription; 
						WriteLog(objLog);
					}
				}
				catch(Exception ex)
				{
					objLog.Status = ActivityStatus.FAILED ;
					objLog.EndDateTime = System.DateTime.Now;
					objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
					WriteLog(objLog);
					continue;
				}
			}

            // launching Endorsement to cancel Non Pay Endorsement for Master Product /Itrack 1209
            // added by Pravesh on 13 June 11
            objLog.Activity = (int)EODActivities.PolicyProcess;
            objLog.SubActivity = (int)EODActivities.CancellationLaunch;

            try
            {
                objLog.ActivtyDescription = "Fetching Policies to Cancel Endorsement: ";
                objLog.StartDateTime = System.DateTime.Now;
                dtPolicies = GetPoliciesToCancelEndorsement();
            }
            catch (Exception ex)
            {
                objLog.Status = ActivityStatus.FAILED;
                objLog.EndDateTime = DateTime.Now;
                objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
                WriteLog(objLog);
                return;
            }
            objLog.ActivtyDescription += dtPolicies.Rows.Count.ToString() + " Records to process";
            objLog.Status = ActivityStatus.SUCCEDDED;
            objLog.EndDateTime = DateTime.Now;
            WriteLog(objLog);
            ClsCommon.BL_LANG_CULTURE = "en-US";
            ClsCommon.IsEODProcess = true;
            for (int i = 0; i < dtPolicies.Rows.Count; i++)
            {
                try
                {
                    DataRow dr = dtPolicies.Rows[i];

                    ClsProcessInfo objProcessInfo = new ClsProcessInfo();
                    DateTime EndorsementEffectiveDate;
                    ClsCommon.SetCustomizedXml(ClsCommon.BL_LANG_CULTURE);
                    //DateTime PolExpiryDate = Convert.ToDateTime(dr["POL_EXPIRATION_DATE"]);

                    int CancelGracePeriod = 0;//Convert.ToInt32(dr["CANCEL_GRACE_PERIOD"]);

                    objProcessInfo.PROCESS_ID = 3;
                    objProcessInfo.CUSTOMER_ID = Convert.ToInt32(dr["CUSTOMER_ID"]);
                    objProcessInfo.POLICY_ID = Convert.ToInt32(dr["POLICY_ID"]);
                    objProcessInfo.POLICY_VERSION_ID = Convert.ToInt32(dr["POLICY_VERSION_ID"]);
                    objProcessInfo.LOB_ID = Convert.ToInt32(dr["LOB_ID"]);
                    objProcessInfo.CREATED_BY = EOD_USER_ID;
                    objProcessInfo.COMPLETED_BY = EOD_USER_ID;
                    objProcessInfo.CREATED_DATETIME = System.DateTime.Now;
                    objProcessInfo.CANCELLATION_TYPE = (int)ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.NON_PAYMENT;
                    objProcessInfo.BILL_TYPE = ClsCancellationProcess.BILL_PAYMENT_DIRECT_BILL;
                    //Cancellation Option , Effective Date  and Premium to be calculated 
                    //Reason  = Non Payment
                    //Other Reason = Non Payment

                    objLog.ActivtyDescription = "Initializing Cancel Endorsement Parameters : ";
                    objLog.StartDateTime = DateTime.Now;
                    objLog.ClientID = objProcessInfo.CUSTOMER_ID;
                    objLog.PolicyID = objProcessInfo.POLICY_ID;
                    objLog.PolicyVersionID = objProcessInfo.POLICY_VERSION_ID;

                    //First Installment not paid
                   // if (Convert.ToInt32(dr["PARTIAL_PAID"]) != 1) // && Convert.ToInt32(dr["INSTALLMENT_NO"]) == 1)
                    {

                            objProcessInfo.CANCELLATION_OPTION = (int)CancellationMethod.Equity;

                            EndorsementEffectiveDate = Convert.ToDateTime(dr["END_EFFECTIVE_DATE"]);
                            //Will be cancelled w.e.f EOD Date + Grace Period
                            objProcessInfo.EFFECTIVE_DATETIME = EndorsementEffectiveDate;
                            objProcessInfo.EXPIRY_DATE = Convert.ToDateTime(dr["END_EXP_DATE"]); ;
                            //objProcessInfo.DUE_DATE = DateTime.Now.AddDays(CancelGracePeriod);
                            objProcessInfo.CO_APPLICANT_ID = Convert.ToInt32(dr["CO_APPLICANT_ID"]);
                            objProcessInfo.SOURCE_VERSION_ID = Convert.ToInt32(dr["END_CANCELLED_POLICY_VERSION_ID"]);
                            /*if (objProcessInfo.EFFECTIVE_DATETIME > PolExpiryDate)
                            {
                                objProcessInfo.DUE_DATE = PolExpiryDate;
                            }*/
                            objLog.ActivtyDescription += "Calculating Endorsement cancellation premium:";
                            objProcessInfo.REASON = (int)CancellationReason.ProRateCancelledForNonPayment;
                            objProcessInfo.OTHER_REASON = CancellationReason.ProRateCancelledForNonPayment.ToString();
                    }
                   

                    objLog.ActivtyDescription += "Setting printing options :";
                    //Insured Commit To Print Spool
                    objProcessInfo.INSURED = (int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL;
                    //Add Inst   Commit To Print Spool
                    objProcessInfo.ADD_INT = (int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL;
                    //Agency Print Commit To Print Spool
                    objProcessInfo.AGENCY_PRINT = (int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL;

                    objProcessInfo.SEND_ALL = (int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.YES);
                    objProcessInfo.ENDORSEMENT_TYPE = 14685; //Default to Cancelation Endorsement 

                    objLog.Status = ActivityStatus.SUCCEDDED;
                    objLog.EndDateTime = System.DateTime.Now;
                    WriteLog(objLog);


                    objLog.ActivtyDescription = "Launching Cancel Endorsment process on policy : ";
                    objLog.StartDateTime = DateTime.Now;
                    objLog.ClientID = objProcessInfo.CUSTOMER_ID;
                    objLog.PolicyID = objProcessInfo.POLICY_ID;
                    objLog.PolicyVersionID = objProcessInfo.POLICY_VERSION_ID;

                    string StatusDescription = "";
                    ClsEndorsmentProcess objProcess = new ClsEndorsmentProcess();
                    objProcess.SystemID = ClsCommon.EODSystemID;
                    if (objProcess.LaunchAutoEndorsement(objProcessInfo)) //, out StatusDescription))
                    {
                        //Problem with pdf generation
                        if (ClsPolicyProcess.PrintingErrorFlag)
                        {
                            objLog.Status = ActivityStatus.FAILED;
                            objLog.EndDateTime = DateTime.Now;
                            objLog.AdditionalInfo = "Generation of Cancel Endorsement Documents failed";
                            WriteLog(objLog);
                        }
                        else
                        {
                            objLog.Status = ActivityStatus.SUCCEDDED;
                            objLog.EndDateTime = System.DateTime.Now;
                            WriteLog(objLog);
                        }
                    }
                    else
                    {
                        objLog.Status = ActivityStatus.FAILED;
                        objLog.EndDateTime = DateTime.Now;
                        objLog.AdditionalInfo = "Can't Launch cancel Endorsement : " + StatusDescription;
                        WriteLog(objLog);
                    }
                }
                catch (Exception ex)
                {
                    objLog.Status = ActivityStatus.FAILED;
                    objLog.EndDateTime = System.DateTime.Now;
                    objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
                    WriteLog(objLog);
                    continue;
                }
            }
			
		}


		#endregion
		
		#region LaunchAutoCommit Method
		/// <summary>
		/// Will Commit Pending Cancellations of Policies for which due date is going to expire 
		/// and adjust the due date of cancellation if Agent has money for the policy
		/// </summary>
		public void LaunchAutoCommit()
		{
			DataTable dtPolicies ;
			EODLogInfo objLog = new EODLogInfo();
			objLog.Activity = (int)EODActivities.PolicyProcess ;
			objLog.SubActivity = (int) EODActivities.CancellationCommit  ;

			try
			{
				objLog.ActivtyDescription = "Fetching Policies To Perform Cancellation Commit ";
				objLog.StartDateTime = System.DateTime.Now ;
				dtPolicies = GetPoliciesToCommitCancellation();
			}
			catch(Exception ex)
			{
				objLog.Status = ActivityStatus.FAILED; 
				objLog.EndDateTime = DateTime.Now;
				objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
				WriteLog(objLog);
				return;
			}
            ClsCommon.BL_LANG_CULTURE = "en-US";
            ClsCommon.IsEODProcess = true;
			objLog.ActivtyDescription += dtPolicies.Rows.Count.ToString() + " Records to process";
			objLog.Status= ActivityStatus.SUCCEDDED;
			objLog.EndDateTime = DateTime.Now;
			WriteLog(objLog);

			int CustomerID = 0, PolicyID = 0 , PolicyVersionID = 0 ,LobID = 0 ;
			ClsPolicyProcess objProcess = new ClsPolicyProcess();
			for(int i = 0 ; i<dtPolicies.Rows.Count; i++)
			{
				try
				{
					DataRow dr = dtPolicies.Rows[i];
					CustomerID = Convert.ToInt32(dr["CUSTOMER_ID"]);
					PolicyID   = Convert.ToInt32(dr["POLICY_ID"]);	
					LobID	   = Convert.ToInt32(dr["LOB_ID"]);
					PolicyVersionID = Convert.ToInt32(dr["POLICY_VERSION_ID"]);
                    ClsCommon.SetCustomizedXml(ClsCommon.BL_LANG_CULTURE);
					
					objLog.ActivtyDescription  = "Initializing Process Info : ";
					objLog.StartDateTime = DateTime.Now;
					objLog.ClientID = CustomerID;
					objLog.PolicyID = PolicyID;
					
					
					ClsProcessInfo objProcessInfo = objProcess.GetRunningProcess(CustomerID,PolicyID);//,PolicyVersionID);
					objProcessInfo.CREATED_BY = EOD_USER_ID;
					objProcessInfo.COMPLETED_BY = EOD_USER_ID;
					objProcessInfo.COMPLETED_DATETIME  = DateTime.Now;
					objProcessInfo.BILL_TYPE  = ClsCancellationProcess.BILL_PAYMENT_DIRECT_BILL; 
					objProcessInfo.LOB_ID = LobID ; 

					objLog.ActivtyDescription += " Committing cancellation : ";
					objLog.PolicyVersionID = objProcessInfo.POLICY_VERSION_ID ;
					
					string StatusDescriptor = "";
					ClsCancellationProcess objCancel = new ClsCancellationProcess();
                    objCancel.SystemID = ClsCommon.EODSystemID;
					if(objCancel.CommitProcess(objProcessInfo,out StatusDescriptor))
					{
						objLog.Status= ActivityStatus.SUCCEDDED;
						objLog.EndDateTime = DateTime.Now;
						WriteLog(objLog);
					}
					else
					{
						objLog.Status= ActivityStatus.FAILED ;
						objLog.EndDateTime = DateTime.Now;
						objLog.AdditionalInfo="Can't Commit cancellation : " + StatusDescriptor ; 
						WriteLog(objLog);
					}
				}
				catch(Exception ex)
				{
					
					objLog.Status = ActivityStatus.FAILED; 
					objLog.EndDateTime = DateTime.Now;
					objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
					WriteLog(objLog);
					continue;
				}
			}
		}

		#endregion

		#region LaunchAutoRollBack Method
		public void LaunchAutoRollBack()
		{
			DataTable dtPolicies ;

			EODLogInfo objLog = new EODLogInfo();
			objLog.Activity = (int)EODActivities.PolicyProcess ;
			objLog.SubActivity = (int) EODActivities.CancellationRollBack  ;

			try
			{
				objLog.ActivtyDescription = "Fetching Policies To Perform Cancellation Rollback ";
				objLog.StartDateTime = System.DateTime.Now ;
				dtPolicies = GetPoliciesToRollback();
			}
			catch(Exception ex)
			{
				objLog.Status = ActivityStatus.FAILED; 
				objLog.EndDateTime = DateTime.Now;
				objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
				WriteLog(objLog);
				return;
			}
            ClsCommon.BL_LANG_CULTURE = "en-US";
            ClsCommon.IsEODProcess = true;
			objLog.ActivtyDescription += dtPolicies.Rows.Count.ToString() + " Records to process";
			objLog.Status= ActivityStatus.SUCCEDDED;
			objLog.EndDateTime = DateTime.Now;
			WriteLog(objLog);

			int CustomerID = 0, PolicyID = 0 , PolicyVersionID = 0, LobID = 0 ;
			ClsPolicyProcess objProcess = new ClsPolicyProcess();

			for(int i = 0 ; i<dtPolicies.Rows.Count; i++)
			{
				try
				{
					DataRow dr = dtPolicies.Rows[i];
					CustomerID = Convert.ToInt32(dr["CUSTOMER_ID"]);
					PolicyID   = Convert.ToInt32(dr["POLICY_ID"]);	
					LobID	   = Convert.ToInt32(dr["LOB_ID"]);	
					PolicyVersionID =Convert.ToInt32(dr["POLICY_VERSION_ID"]);
                    ClsCommon.SetCustomizedXml(ClsCommon.BL_LANG_CULTURE);
					objLog.ActivtyDescription  = "Initializing Process Info : ";
					objLog.StartDateTime = DateTime.Now;
					objLog.ClientID = CustomerID;
					objLog.PolicyID = PolicyID;

					ClsProcessInfo objProcessInfo = objProcess.GetRunningProcess(CustomerID,PolicyID);//,PolicyVersionID);

					ClsCancellationProcess objCancel = new ClsCancellationProcess();
                    objCancel.SystemID = ClsCommon.EODSystemID;
					objLog.ActivtyDescription += " Starting Rollback cancellation : ";
					objLog.PolicyVersionID = objProcessInfo.POLICY_VERSION_ID ;
					objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ROLLBACK_CANCELLATION_PROCESS;
	
					objProcessInfo.CREATED_BY = EOD_USER_ID;
					objProcessInfo.COMPLETED_BY = EOD_USER_ID;
					objProcessInfo.BILL_TYPE  = ClsCancellationProcess.BILL_PAYMENT_DIRECT_BILL; 
					objProcessInfo.LOB_ID	  = LobID;

					if(objCancel.RollbackProcess(objProcessInfo))
					{
						objLog.Status= ActivityStatus.SUCCEDDED;
						objLog.EndDateTime = DateTime.Now;
						WriteLog(objLog);
					}
					else
					{
						objLog.Status= ActivityStatus.FAILED ;
						objLog.EndDateTime = DateTime.Now;
						objLog.AdditionalInfo="Can't Rollback cancellation Method RollbackProcess() returns false "; 
						WriteLog(objLog);
					}
				}
				catch(Exception ex)
				{
					objLog.Status = ActivityStatus.FAILED; 
					objLog.EndDateTime = DateTime.Now;
					objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
					WriteLog(objLog);
					continue;
				}
			}
		}

		#endregion

		private DataTable GetPoliciesToRollback()
		{
			string strCnnString = Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr;
			Cms.DataLayer.DataWrapper  objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
			//DataSet ds = objDataWrapper.ExecuteDataSet("Proc_GetUnderCanc_PoliciesToRollback");
            DataSet ds = objDataWrapper.ExecuteDataSet("Proc_GetRollbackPol_UnderNonPayCancelation");
			objDataWrapper.Dispose();
			return ds.Tables[0];
		}

		private DataTable GetPoliciesToCommitCancellation()
		{
			string strCnnString = Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr;
			Cms.DataLayer.DataWrapper  objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
			//DataSet ds = objDataWrapper.ExecuteDataSet("Proc_GetPoliciesToCancel");
            DataSet ds = objDataWrapper.ExecuteDataSet("Proc_GetNonPayPoliciesToCancel");
			objDataWrapper.Dispose();
			return ds.Tables[0];
		}
		private DataTable GetDBPoliciesToCancel()
		{
			string strCnnString = Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr;
			Cms.DataLayer.DataWrapper  objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
			//DataSet ds = objDataWrapper.ExecuteDataSet("Proc_GetNPT_DB_PoliciesToCancel");
            DataSet ds = objDataWrapper.ExecuteDataSet("Proc_GetNonPayCanceledPolicy_List");
			objDataWrapper.Dispose();
			return ds.Tables[0];
		}
        private DataTable GetPoliciesToCancelEndorsement()
        {
            string strCnnString = Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr;
            Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            DataSet ds = objDataWrapper.ExecuteDataSet("PROC_GetNonPayEndMasterPolicy_List");
            objDataWrapper.Dispose();
            return ds.Tables[0];
        }
	}
}
