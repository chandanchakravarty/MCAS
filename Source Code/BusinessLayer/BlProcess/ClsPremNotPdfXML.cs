using System;
using System.Xml;
using System.Data;
using Cms.DataLayer;
using System.Collections;

namespace Cms.BusinessLayer.BlProcess
{
	/// <summary>
	/// <CreatedBy>Mohit Agarwal</CreatedBy>
	/// <Dated>08-Jan-2007</Dated>
	/// <Purpose>To Create XML for ADDL INT PDF for HOME LOB</Purpose>
	/// </summary>
	public class ClsPremNotPdfXML : ClsCommonPdfXML
	{
		
		#region Declarations
		private XmlElement DecPageRootElement;
		private int mCustomerID, mPolicyID , mPolicyVersionID , mBillMortgagee,mAgencyID ;
		private string mDueDate , mPolicyNumber;
		private DataSet dsNoticeData;
		private  double mMinimumDue , mTotalDue , mTotalPremium;
		string ShowInsSch;

		public int AgencyID
		{
			get
			{
				return mAgencyID;
			}
		}
		public bool BillMortgagee 
		{
			get
			{
				if(mBillMortgagee  == 0)
				{
					return false ;
				}
				else
				{
					return true;
				}
			}
		}
		#endregion

		#region Constructor

		private void DoInitialisation(string PolicyNumber,string DueDate,int CustomerID , int PolicyID , int PolicyVersionID ,DataWrapper objDatawrapper) 
		{
			mPolicyNumber = PolicyNumber;
			mCustomerID   = CustomerID ; 
			mPolicyID	= PolicyID ; 
			mPolicyVersionID = PolicyVersionID;
			mDueDate   = DueDate;
			base.objWrapper = objDatawrapper;
			mBillMortgagee = 0;
			SetPDFVersionLobNode("CHK",System.DateTime.Now);
			GetNoticeData();
			FillMonth();
		}
		public ClsPremNotPdfXML(string PolicyNumber ,DataWrapper objDatawrapper)
		{
			DoInitialisation(PolicyNumber,"",0,0,0,objDatawrapper);
		}
		public ClsPremNotPdfXML(string PolicyNumber, string DueDate,DataWrapper objDatawrapper)
		{
			DoInitialisation(PolicyNumber,DueDate,0,0,0,objDatawrapper);
		}
		public ClsPremNotPdfXML(int CustomerID , int PolicyID , int PolicyVersionID ,DataWrapper objDatawrapper)
		{
			DoInitialisation("","",CustomerID,PolicyID,PolicyVersionID,objDatawrapper);
		}
		public ClsPremNotPdfXML(int CustomerID , int PolicyID , int PolicyVersionID , string DueDate,DataWrapper objDatawrapper)
		{
			DoInitialisation("",DueDate ,CustomerID,PolicyID,PolicyVersionID,objDatawrapper);
		}
		
		#endregion

		
		//Added By Ravindra
		
		public string GetXMLForInsured(out double MinimumDue, out double  TotalDue, out double  TotalPremiumDue)
		{
			CreateCommonElements("I");
			AddAccountHistory();
			if(ShowInsSch == "Y")
			{
				AddInstallmentHistory();
			}

			MinimumDue = mMinimumDue;
			TotalDue   = mTotalDue;
			TotalPremiumDue  = mTotalPremium ;
			return AcordPDFXML.OuterXml;
		}

		public string GetXMLForMortgagee (out double MinimumDue, out double  TotalDue)
		{
			
			CreateCommonElements("M");
			AddAccountHistory();
			if(ShowInsSch == "Y")
			{
				AddInstallmentHistory();
			}

			MinimumDue = mMinimumDue;
			TotalDue   = mTotalDue;
			return AcordPDFXML.OuterXml;
		}

		private void GetNoticeData()
		{
			if(mCustomerID ==0)
			{
				objWrapper.AddParameter("@CUSTOMER_ID", DBNull.Value  );
			}
			else
			{
				objWrapper.AddParameter("@CUSTOMER_ID",mCustomerID );
			}

			if(mPolicyID == 0)
			{
				objWrapper.AddParameter("@POLICY_ID" ,DBNull.Value);
			}
			else
			{
				objWrapper.AddParameter("@POLICY_ID",mPolicyID   );
			}

			if(mPolicyVersionID ==0)
			{
				objWrapper.AddParameter("@POLICY_VERSION_ID", DBNull.Value  );
			}
			else
			{
				objWrapper.AddParameter("@POLICY_VERSION_ID",mPolicyVersionID );
			}

			if(mDueDate=="")
			{
				objWrapper.AddParameter("@NOTICE_DUE_DATE", DBNull.Value  );
			}
			else
			{
				objWrapper.AddParameter("@NOTICE_DUE_DATE",mDueDate );
			}

			if(mPolicyNumber == "")
			{
				objWrapper.AddParameter("@POLICY_NUMBER", DBNull.Value  );
			}
			else
			{
				objWrapper.AddParameter("@POLICY_NUMBER",mPolicyNumber);
			}

			if(IsEODProcess)
			{
				objWrapper.AddParameter("@IS_EOD", 1 );
			}
			else
			{
				objWrapper.AddParameter("@IS_EOD", DBNull.Value  );
			}

			dsNoticeData =  objWrapper.ExecuteDataSet("Proc_GetPremiumNoticeData");

			if(dsNoticeData != null )
			{
				if(dsNoticeData.Tables[0].Rows.Count > 0)
				{
					mBillMortgagee = Convert.ToInt32(dsNoticeData.Tables[0].Rows[0]["BILL_MORTAGAGEE"]);
					mAgencyID =  Convert.ToInt32(dsNoticeData.Tables[0].Rows[0]["AGENCY_ID"]);
				}
			}

		}

		private void CreateCommonElements(string CalledFor)
		{
			AcordPDFXML = new XmlDocument();
			AcordPDFXML.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?> <" + RootElement + "/>");
			DecPageRootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
			AcordPDFXML.SelectSingleNode(RootElement).AppendChild(DecPageRootElement);
			DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("PREMPAGE"));
			
			XmlElement NoticeCommon;
			NoticeCommon    = AcordPDFXML.CreateElement("NOTICECOMMON");
			DecPageRootElement.AppendChild(NoticeCommon);
			NoticeCommon.SetAttribute(fieldType,fieldTypeSingle );
			NoticeCommon.SetAttribute(PrimPDF,getAcordPDFNameFromXML("PRMPAGE"));
			NoticeCommon.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("PRMPAGE"));
			NoticeCommon.SetAttribute(SecondPDF,getAcordPDFNameFromXML("PRMPAGEEXTN"));
			NoticeCommon.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("PRMPAGEEXTN"));

		
			string app_term = "";

			due_date =  dsNoticeData.Tables[0].Rows[0]["DUE_DATE"].ToString();
			if((dsNoticeData.Tables[0].Rows[0]["APP_INCEPTION_DATE"] != System.DBNull.Value)&&(dsNoticeData.Tables[0].Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value))
				app_term = Convert.ToDateTime(dsNoticeData.Tables[0].Rows[0]["APP_INCEPTION_DATE"]).ToString("MM/dd/yyyy")+"-"+Convert.ToDateTime(dsNoticeData.Tables[0].Rows[0]["APP_EXPIRATION_DATE"]).ToString("MM/dd/yyyy");
			
			
			try
			{ 
				mMinimumDue = double.Parse(dsNoticeData.Tables[0].Rows[0]["MIN_DUE"].ToString().Replace("$","").Replace(",","")); 
				mTotalDue = double.Parse(dsNoticeData.Tables[0].Rows[0]["TOTAL_DUE"].ToString().Replace("$","").Replace(",","")); 
				mTotalPremium  = double.Parse(dsNoticeData.Tables[0].Rows[0]["TOTAL_PREMIUM_DUE"].ToString().Replace("$","").Replace(",","")); 
			} 
			catch(Exception ex) 
			{
				System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
				addInfo.Add("Err Descriptor ","Error while calculation Min & Total Due in premium notice generation");
				addInfo.Add("CustomerID" ,mCustomerID.ToString());
				addInfo.Add("PolicyID",mPolicyID.ToString());
				ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex,addInfo );
			}

			NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<prem_notic " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[0].Rows[0]["NOTICE_TYPE"].ToString()) + "</prem_notic>";
			NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<prem_name1 " + fieldType + "=\"" + fieldTypeText + "\"></prem_name1>";

			//If INsured
			if(CalledFor == "I")
			{
				NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<prem_name2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString())+ " "+RemoveJunkXmlCharacters(dsNoticeData.Tables[0].Rows[0]["CUSTOMER_SUFFIX"].ToString()) +"</prem_name2>";
				string custaddr = dsNoticeData.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString();
				if(dsNoticeData.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString() != "")
					custaddr += ", " + dsNoticeData.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString();
				NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<prem_addr1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(custaddr) + "</prem_addr1>";
				NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<prem_addr2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString() + ", " + dsNoticeData.Tables[0].Rows[0]["STATE_CODE"].ToString() + " " + dsNoticeData.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString()) + "</prem_addr2>";
			}
			//Mortagagee
			else
			{
				NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<prem_name2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[4].Rows[0]["HOLDER_NAME"].ToString()) + "</prem_name2>";
				string custaddr = dsNoticeData.Tables[4].Rows[0]["HOLDER_ADDRESS1"].ToString();
				if(dsNoticeData.Tables[4].Rows[0]["HOLDER_ADDRESS2"].ToString() != "")
					custaddr += ", " + dsNoticeData.Tables[4].Rows[0]["HOLDER_ADDRESS2"].ToString();
				NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<prem_addr1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(custaddr) + "</prem_addr1>";
				NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<prem_addr2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[4].Rows[0]["HOLDER_CITY"].ToString() + ", " + dsNoticeData.Tables[4].Rows[0]["STATE_CODE"].ToString() + " " + dsNoticeData.Tables[4].Rows[0]["HOLDER_ZIP"].ToString()) + "</prem_addr2>";

				//Insured Name 
				NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<insured_msg " + fieldType + "=\"" + fieldTypeText + "\">Mortgagee Pays Premium For:</insured_msg>";
				NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<insured_name " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString()) + "</insured_name>";
				custaddr = dsNoticeData.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString();
				if(dsNoticeData.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString() != "")
					custaddr += ", " + dsNoticeData.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString();
				NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<insured_addr1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(custaddr) + "</insured_addr1>";
				NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<insured_addr2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString() + ", " + dsNoticeData.Tables[0].Rows[0]["STATE_CODE"].ToString() + " " + dsNoticeData.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString()) + "</insured_addr2>";

			}

			NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<Agency_name " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</Agency_name>";
			NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<Agency_code " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[0].Rows[0]["AGENCY_CODE"].ToString().ToUpper().Trim()) + "</Agency_code>";
			NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<Agency_phone " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[0].Rows[0]["AGENCY_PHONE"].ToString()) + "</Agency_phone>";
			NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<prem_due " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(due_date) + "</prem_due>";
			NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<prem_tot " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[0].Rows[0]["TOTAL_DUE"].ToString()) + "</prem_tot>";
			NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<prem_min " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[0].Rows[0]["MIN_DUE"].ToString()) + "</prem_min>";
			NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<prem_pol " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</prem_pol>";
		
			NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<foot_tot_due " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[0].Rows[0]["TOTAL_DUE"].ToString()) + "</foot_tot_due>";
			NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<foot_due_date " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(due_date) + "</foot_due_date>";
			NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<foot_min_due " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[0].Rows[0]["MIN_DUE"].ToString()) + "</foot_min_due>";
			NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<foot_polnumber " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</foot_polnumber>";
			NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<foot_billdate " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DateTime.Now.ToString("MM/dd/yyyy")) + "</foot_billdate>";
			NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<foot_billplan " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[0].Rows[0]["PLAN_DESCRIPTION"].ToString()) + "</foot_billplan>";
			NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<foot_eff_date " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[0].Rows[0]["EFFECTIVE_DATE"].ToString()) + "</foot_eff_date>";
			
//			NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<nsf_fee " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[0].Rows[0]["NSF_FEE"].ToString()) + "</nsf_fee>";
//			NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<late_fee " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[0].Rows[0]["LATE_FEE"].ToString()) + "</late_fee>";

			NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<nsf_fee " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[2].Rows[0]["NSF_MESSAGE"].ToString()) + "</nsf_fee>";
			NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<late_fee " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[2].Rows[0]["LF_MESSAGE"].ToString()) + "</late_fee>";

			NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<ocra " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[0].Rows[0]["OCRA"].ToString()) + "</ocra>";
			NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<foot_poltyp " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[0].Rows[0]["POLICY_TYPE"].ToString()) + "</foot_poltyp>";
			NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<foot_pol_term " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(app_term) + "</foot_pol_term>";

			//Ravindra(03-10-2008) If under full pay hide installment schedule
			ShowInsSch = dsNoticeData.Tables[0].Rows[0]["SHOW_INS_SCHEDULE"].ToString();

			if(ShowInsSch == "Y")
			{
				NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<Installment_Schedule " + fieldType + "=\"" + fieldTypeText + "\">Installment Schedule</Installment_Schedule>";
				NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<Due_Date " + fieldType + "=\"" + fieldTypeText + "\">Due Date</Due_Date>";
				NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<Amount_Due " + fieldType + "=\"" + fieldTypeText + "\">Amount</Amount_Due>";
			}
			else
			{
				NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<Installment_Schedule " + fieldType + "=\"" + fieldTypeText + "\"> </Installment_Schedule>";
				NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<Due_Date " + fieldType + "=\"" + fieldTypeText + "\"> </Due_Date>";
				NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<Amount_Due " + fieldType + "=\"" + fieldTypeText + "\"> </Amount_Due>";
			}

		
			string bill_plan = dsNoticeData.Tables[0].Rows[0]["BILL_PLAN"].ToString().Trim();
			
			//Ravindra(10-21-2008):: Condition is wrong.
			if(BillMortgagee && CalledFor == "I")
			{
				NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<messg " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[2].Rows[0]["MESSAGE_INS"].ToString() + " Your Mortgage Company has been billed for the Premium Due.") + "</messg>";
			}
			else
			{
				NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<messg " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[2].Rows[0]["MESSAGE_INS"].ToString()) + "</messg>";
			}
			NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<msg_donotpay " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[2].Rows[0]["MSG_DO_NOT_PAY"].ToString()) + "</msg_donotpay>";

			//NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<foot_messg " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNoticeData.Tables[2].Rows[0]["MESSAGE_FEES"].ToString()) + "</foot_messg>";

		//	string polNum = dsNoticeData.Tables[0].Rows[0]["POLICY_NUMBER"].ToString();

		//	if(polNum.StartsWith("H") || polNum.StartsWith("R"))
			if(dsNoticeData.Tables[0].Rows[0]["lob_code"].ToString()=="REDW" || dsNoticeData.Tables[0].Rows[0]["lob_code"].ToString()=="HOME")
			{
				//DataSet DSLocDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_Dwelling_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'POLICY'");
				objWrapper.ClearParameteres();
				objWrapper.AddParameter("@CUSTOMERID",mCustomerID);
				objWrapper.AddParameter("@POLID",mPolicyID);
				objWrapper.AddParameter("@VERSIONID",mPolicyVersionID);
				objWrapper.AddParameter("@CALLEDFROM","NOTICE");
				DataSet DSLocDataSet  = objWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_Dwelling_Details");
				objWrapper.ClearParameteres();

				if(DSLocDataSet != null && DSLocDataSet.Tables[0].Rows.Count > 0)
					NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<property " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSLocDataSet.Tables[0].Rows[0]["LOC_ADDRESS"].ToString() + ", " + DSLocDataSet.Tables[0].Rows[0]["LOC_CITYSTATEZIP"].ToString()) + "</property>";
			}
			else
			{
				NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<property_loc " + fieldType + "=\"" + fieldTypeText + "\"></property_loc>";
			}
			if(IsEODProcess)
			{
				NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<message_virtual " + fieldType + "=\"" + fieldTypeText + "\"></message_virtual>";
			}
			else
			{
				NoticeCommon.InnerXml = NoticeCommon.InnerXml +  "<message_virtual " + fieldType + "=\"" + fieldTypeText + "\">Virtual</message_virtual>";
			}
		}

		
		private void AddAccountHistory()
		{
			
			XmlElement DecPageAccHistElement;
			DecPageAccHistElement    = AcordPDFXML.CreateElement("ACCHIST");
			DecPageRootElement.AppendChild(DecPageAccHistElement);
			DecPageAccHistElement.SetAttribute(fieldType,fieldTypeMultiple);
			DecPageAccHistElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("PRMPAGE"));
			DecPageAccHistElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("PRMPAGE"));
			DecPageAccHistElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("PRMPAGEEXTN"));
			DecPageAccHistElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("PRMPAGEEXTN"));
		
			int RowCounter=0;
			XmlElement DecPageAccElement;
			DecPageAccElement = AcordPDFXML.CreateElement("ACCROW");
			DecPageAccHistElement.AppendChild(DecPageAccElement);
			DecPageAccElement.SetAttribute(fieldType,fieldTypeNormal);
			DecPageAccElement.SetAttribute(id,RowCounter.ToString());
			
			foreach(DataRow AccountHistory in dsNoticeData.Tables[1].Rows)
			{
				if(RowCounter > 0)
				{
					DecPageAccElement = AcordPDFXML.CreateElement("ACCROW");
					DecPageAccHistElement.AppendChild(DecPageAccElement);
					DecPageAccElement.SetAttribute(fieldType,fieldTypeNormal);
					DecPageAccElement.SetAttribute(id,RowCounter.ToString());
				}

				if(AccountHistory["BILL_DATE"] != System.DBNull.Value)
					DecPageAccElement.InnerXml= DecPageAccElement.InnerXml + "<acc_date " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Convert.ToDateTime(AccountHistory["BILL_DATE"]).ToString("MM/dd/yyyy"))+"</acc_date>";
				DecPageAccElement.InnerXml= DecPageAccElement.InnerXml + "<acc_activity " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AccountHistory["ACTIVITY_DESC"].ToString())+"</acc_activity>";
				if(AccountHistory["EFF_DATE"] != System.DBNull.Value)
					DecPageAccElement.InnerXml= DecPageAccElement.InnerXml + "<acc_eff " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Convert.ToDateTime(AccountHistory["EFF_DATE"]).ToString("MM/dd/yyyy"))+"</acc_eff>";
				DecPageAccElement.InnerXml= DecPageAccElement.InnerXml + "<acc_amt " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AccountHistory["AMOUNT"].ToString())+"</acc_amt>";
				RowCounter++;
			}
		}
		
		private void AddInstallmentHistory()
		{
			
			XmlElement DecPageInsHistElement;
			DecPageInsHistElement    = AcordPDFXML.CreateElement("INSHIST");
			
			DecPageRootElement.AppendChild(DecPageInsHistElement);
			DecPageInsHistElement.SetAttribute(fieldType,fieldTypeMultiple);
			DecPageInsHistElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("PRMPAGE"));
			DecPageInsHistElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("PRMPAGE"));
			DecPageInsHistElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("PRMPAGEEXTN"));
			DecPageInsHistElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("PRMPAGEEXTN"));
		
			int RowCounter=0;
			foreach(DataRow InstallmentHistory in dsNoticeData.Tables[3].Rows)
			{
				XmlElement DecPageInsElement;
				DecPageInsElement = AcordPDFXML.CreateElement("INSROW");
				DecPageInsHistElement.AppendChild(DecPageInsElement);
				DecPageInsElement.SetAttribute(fieldType,fieldTypeNormal);
				DecPageInsElement.SetAttribute(id,RowCounter.ToString());
				DecPageInsElement.InnerXml= DecPageInsElement.InnerXml + "<ins_amt " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(InstallmentHistory["AMOUNT"].ToString())+"</ins_amt>";
				DecPageInsElement.InnerXml= DecPageInsElement.InnerXml + "<ins_dat " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(InstallmentHistory["DUE_DATE"].ToString())+"</ins_dat>";
				RowCounter++;
			}
		}



		#region Obselete Code (can be commented) Ravindra(03-11-2008)
		
		private Hashtable htpremium=new Hashtable(); 
		private DataWrapper gobjSqlHelper;
		//private string stCode="";
		private string Ins_Lein = "";
		public string gStrLobCode="";
		public string due_date="";
		int addlindex = 0;

		/// <summary>
		/// Obselete method do not use this
		/// </summary>
		/// <param name="lstrClientId"></param>
		/// <param name="lstrPolicyId"></param>
		/// <param name="lstrVersionId"></param>
		/// <param name="strLOB"></param>
		/// <param name="dueDate"></param>
		/// <param name="lstrCalledFor"></param>
		/// <param name="lstrCalledFrom"></param>
		/// <param name="addlintindex"></param>
		public ClsPremNotPdfXML(string lstrClientId,string lstrPolicyId,string lstrVersionId, string strLOB, string dueDate, string lstrCalledFor, string lstrCalledFrom, int addlintindex)
		{
			gStrClientID=lstrClientId;
			gStrPolicyId=lstrPolicyId;
			gStrPolicyVersion=lstrVersionId;
			gStrPdfFor=lstrCalledFrom.Trim();
			Ins_Lein = lstrCalledFor.Trim();
			due_date = dueDate;
			gStrLobCode = strLOB;
			addlindex = addlintindex;
			
			DSTempDataSet = new DataSet();
			gobjSqlHelper = new DataWrapper(ConnStr,CommandType.Text);
			SetPDFVersionLobNode("CHK",System.DateTime.Now);
		
		}

		public string getPremNotPDFXml(string eod, ref string bill_plan)
		{
			AcordPDFXML = new XmlDocument();
			AcordPDFXML.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?> <" + RootElement + "/>");

			createRootElementForAllRootPDFs();
			//LoadRateXML();
			FillMonth();

			//creating Xml From Here
			if(CreateCustAgencyXML(eod, ref bill_plan) == 1)
			{
				createAccHistoryXML();
				if(DSTempDataSet.Tables.Count >= 5)
				{
					createInstallmentHistoryXML();
				}

				return AcordPDFXML.OuterXml;
			}
			else
				return "";
		}

		private void createRootElementForAllRootPDFs()
		{
			DecPageRootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
			AcordPDFXML.SelectSingleNode(RootElement).AppendChild(DecPageRootElement);
			DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("PREMPAGE"));
		}
	
		private int CreateCustAgencyXML(string eod, ref string bill_plan)
		{
			//string due_date = DateTime.Now.AddDays(5).ToString("MM/dd/yyyy");
			
			if(due_date == "")
				DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPremiumNoticeData " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",NULL,'" + eod + "', '" + gStrPdfFor + "'");
			else
			{
				try
				{
					due_date = Convert.ToDateTime(due_date).ToString("MM/dd/yyyy");
					DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPremiumNoticeData " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + due_date + "', '" + eod + "', '" + gStrPdfFor + "'");
				} 
				catch 
				{
					//Added by Mohit Agarwal 24-Jan-08 ITrack 3477
					DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPremiumNoticeData " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",NULL,'" + eod + "', '" + gStrPdfFor + "'");
				}
			}
			
			if(DSTempDataSet.Tables[0].Rows.Count < 1)
				return 0;
	
			#region Commented Code
			//			#region Policy Agency Part
			//			XmlElement DecPagePolicyElement;
			//			DecPagePolicyElement = AcordPDFXML.CreateElement("POLICY");
			//			DecPageRootElement.AppendChild(DecPagePolicyElement);
			//			DecPagePolicyElement.SetAttribute(fieldType,fieldTypeMultiple);
			//
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_notic " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["NOTICE_TYPE"].ToString()) + "</prem_notic>";
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_name1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</prem_name1>";
			//
			//			DataSet DSTempDwellinAdd=null;
			//			if(gStrLobCode == "HOME" || gStrLobCode == "RENT")
			//				DSTempDwellinAdd = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_Additional_Interest " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",0,'POLICY'");
			//			else if(gStrLobCode == "MOT" || gStrLobCode == "PPA")
			//				DSTempDwellinAdd = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",0,'POLICY'");
			//			else if(gStrLobCode == "WAT")
			//				DSTempDwellinAdd = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",0,'POLICY'");
			//			
			//			if(Ins_Lein == "INSURED")
			//			{
			//				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_name2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString()) + "</prem_name2>";
			//				string custaddr = DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString();
			//				if(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString() != "")
			//					custaddr += ", " + DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString();
			//				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_addr1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(custaddr) + "</prem_addr1>";
			//				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_addr2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString() + ", " + DSTempDataSet.Tables[0].Rows[0]["STATE_CODE"].ToString() + " " + DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString()) + "</prem_addr2>";
			//			}
			//			else
			//			{
			//				#region Addl Interest Information
			//			
			//				if(gStrLobCode == "HOME" || gStrLobCode == "RENT")
			//				{
			//					//						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDITIONALINTERESTNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["RANK"].ToString())+"</ADDITIONALINTERESTNO>"; 
			//					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +"<prem_name2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlindex]["HOLDER_NAME"].ToString())+"</prem_name2>"; 
			//					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +"<prem_addr1 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlindex]["ADDRESS"].ToString())+"</prem_addr1>"; 
			//					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +"<prem_addr2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlindex]["HOLDERCITYSTATEZIP"].ToString())+"</prem_addr2>"; 
			//				}
			//				else if(gStrLobCode == "MOT" || gStrLobCode == "PPA")
			//				{
			//					//						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDITIONALINTERESTNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["RANK"].ToString())+"</ADDITIONALINTERESTNO>"; 
			//					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +"<prem_name2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlindex]["HOLDER_NAME"].ToString())+"</prem_name2>"; 
			//					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +"<prem_addr1 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlindex]["ADDRESS"].ToString())+"</prem_addr1>"; 
			//					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +"<prem_addr2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlindex]["CITYSTATEZIP"].ToString())+"</prem_addr2>"; 
			//				}
			//				else if(gStrLobCode == "WAT")
			//				{
			//					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +"<prem_name2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlindex]["HOLDER_NAME"].ToString())+"</prem_name2>"; 
			//					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +"<prem_addr1 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlindex]["ADDRESS"].ToString())+"</prem_addr1>"; 
			//					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +"<prem_addr2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlindex]["CITYSTATEZIP"].ToString())+"</prem_addr2>"; 
			//				}
			//				#endregion Addl Interest Information
			//			}
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<Agency_name " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</Agency_name>";
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<Agency_phone " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString()) + "</Agency_phone>";
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_due " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(due_date) + "</prem_due>";
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_tot " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["TOTAL_DUE"].ToString()) + "</prem_tot>";
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_min " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["MIN_DUE"].ToString()) + "</prem_min>";
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_pol " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</prem_pol>";
			//			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_tot " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</prem_tot>";
			////			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_amt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AMOUNT_PAID"].ToString()) + "</prem_amt>";
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_tot_due " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["TOTAL_DUE"].ToString()) + "</foot_tot_due>";
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_due_date " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(due_date) + "</foot_due_date>";
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_min_due " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["MIN_DUE"].ToString()) + "</foot_min_due>";
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_polnumber " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</foot_polnumber>";
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_billdate " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DateTime.Now.ToString("MM/dd/yyyy")) + "</foot_billdate>";
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_billplan " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PLAN_DESCRIPTION"].ToString()) + "</foot_billplan>";
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_eff_date " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["EFFECTIVE_DATE"].ToString()) + "</foot_eff_date>";
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<nsf_fee " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["NSF_FEE"].ToString()) + "</nsf_fee>";
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<late_fee " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["LATE_FEE"].ToString()) + "</late_fee>";
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<ocra " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["OCRA"].ToString()) + "</ocra>";
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_poltyp " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_TYPE"].ToString()) + "</foot_poltyp>";
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_pol_term " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(app_term) + "</foot_pol_term>";
			//
			//			bill_plan = DSTempDataSet.Tables[0].Rows[0]["BILL_PLAN"].ToString();
			//			if(DSTempDataSet.Tables.Count >= 3 && Ins_Lein == "INSURED" && DSTempDataSet.Tables[0].Rows[0]["BILL_PLAN"].ToString().IndexOf("Mortgagee Bill from Inception") >= 0 && DSTempDwellinAdd != null && DSTempDwellinAdd.Tables[0].Rows.Count > 0)
			//				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<messg " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[2].Rows[0]["MESSAGE"].ToString() + " Your Mortgagee Company has been billed for the Premium Due.") + "</messg>";
			//			else if(DSTempDataSet.Tables.Count >= 3)
			//				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<messg " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[2].Rows[0]["MESSAGE"].ToString()) + "</messg>";
			//			if(DSTempDataSet.Tables.Count >= 4)
			//				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_messg " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[3].Rows[0]["MESSAGE"].ToString()) + "</foot_messg>";
			//			if(DSTempDataSet.Tables.Count >= 5)
			//			{
			//				/*
			//				if(DSTempDataSet.Tables[4].Rows.Count > 0)
			//				{
			//					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<ins_sch " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Installment Schedule") + "</ins_sch>";
			//					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<ins_date " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Date") + "</ins_date>";
			//					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<Ins_amount " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Amount") + "</Ins_amount>";
			//					string strfloatX = "466";
			//					string strfloatY = "560";
			//					string strfloatW = "150";
			//					string strfloatH = "166";
			//					string strpageNo = "1";
			//
			//					DecPagePolicyElement.InnerXml += "<INSTALLMENT " + fieldType +"=\""+ fieldTypeImage +"\" IMAGEPATH=\"" + RemoveJunkXmlCharacters("Installment Info.jpg") + "\" " + imageType + "=\"" + imageTypeYes + "\" " + floatX + "=\"" + strfloatX + "\" " + floatY + "=\"" + strfloatY + "\" " + floatW + "=\"" + strfloatW  + "\" " + floatH + "=\"" + strfloatH  + "\" " + pageNo + "=\"" + strpageNo  + "\"></INSTALLMENT>";
			//				}
			//				else
			//				{
			//					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<ins_sch " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</ins_sch>";
			//					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<ins_date " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</ins_date>";
			//					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<Ins_amount " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</Ins_amount>";
			//				}
			//				*/
			//			}
			//			else
			//			{
			//				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<ins_sch " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</ins_sch>";
			//				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<ins_date " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</ins_date>";
			//				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<Ins_amount " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</Ins_amount>";
			//			}
			//			string polNum = DSTempDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString();
			//
			//			if(polNum.StartsWith("H") || polNum.StartsWith("R"))
			//			{
			//				DataSet DSLocDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_Dwelling_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'POLICY'");
			//				if(DSLocDataSet != null && DSLocDataSet.Tables[0].Rows.Count > 0)
			//					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<property " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSLocDataSet.Tables[0].Rows[0]["LOC_ADDRESS"].ToString() + ", " + DSLocDataSet.Tables[0].Rows[0]["LOC_CITYSTATEZIP"].ToString()) + "</property>";
			//			}
			//			else
			//			{
			//				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<property_loc " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</property_loc>";
			//			}
			//			#endregion
			#endregion
			return 1;
		}
		private void createPolicyInfoAllPages(ref XmlElement DecPageAccElement)
		{
			string app_term = "";

			due_date = DSTempDataSet.Tables[0].Rows[0]["DUE_DATE"].ToString();
			if((DSTempDataSet.Tables[0].Rows[0]["APP_INCEPTION_DATE"] != System.DBNull.Value)&&(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value))
				app_term = Convert.ToDateTime(DSTempDataSet.Tables[0].Rows[0]["APP_INCEPTION_DATE"]).ToString("MM/dd/yyyy")+"-"+Convert.ToDateTime(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"]).ToString("MM/dd/yyyy");
			
			
			try
			{ 
				//Ravindra(02-28-2008) : Total due of notice to be posted to Account Inquiry
				MinimumDue = double.Parse(DSTempDataSet.Tables[0].Rows[0]["MIN_DUE"].ToString().Replace("$","").Replace(",","")); 
				notice_amount = double.Parse(DSTempDataSet.Tables[0].Rows[0]["TOTAL_DUE"].ToString().Replace("$","").Replace(",","")); 
			} 
			catch {}

			DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<prem_notic " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["NOTICE_TYPE"].ToString()) + "</prem_notic>";
			DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<prem_name1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</prem_name1>";

			DataSet DSTempDwellinAdd=null;
			if(gStrLobCode == "HOME" || gStrLobCode == "RENT")
				DSTempDwellinAdd = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_Additional_Interest " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",0,'POLICY'");
			else if(gStrLobCode == "MOT" || gStrLobCode == "PPA")
				DSTempDwellinAdd = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",0,'POLICY'");
			else if(gStrLobCode == "WAT")
				DSTempDwellinAdd = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",0,'POLICY'");
			
			if(Ins_Lein == "INSURED")
			{
				DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<prem_name2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString()) + " "+ RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_SUFFIX"].ToString()) + "</prem_name2>";
				string custaddr = DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString();
				if(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString() != "")
					custaddr += ", " + DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString();
				DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<prem_addr1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(custaddr) + "</prem_addr1>";
				DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<prem_addr2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString() + ", " + DSTempDataSet.Tables[0].Rows[0]["STATE_CODE"].ToString() + " " + DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString()) + "</prem_addr2>";
			}
			else
			{
				#region Addl Interest Information
			
				if(gStrLobCode == "HOME" || gStrLobCode == "RENT")
				{
					//						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDITIONALINTERESTNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["RANK"].ToString())+"</ADDITIONALINTERESTNO>"; 
					DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +"<prem_name2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlindex]["HOLDER_NAME"].ToString())+"</prem_name2>"; 
					DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +"<prem_addr1 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlindex]["ADDRESS"].ToString())+"</prem_addr1>"; 
					DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +"<prem_addr2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlindex]["HOLDERCITYSTATEZIP"].ToString())+"</prem_addr2>"; 
				}
				else if(gStrLobCode == "MOT" || gStrLobCode == "PPA")
				{
					//						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDITIONALINTERESTNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["RANK"].ToString())+"</ADDITIONALINTERESTNO>"; 
					DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +"<prem_name2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlindex]["HOLDER_NAME"].ToString())+"</prem_name2>"; 
					DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +"<prem_addr1 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlindex]["ADDRESS"].ToString())+"</prem_addr1>"; 
					DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +"<prem_addr2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlindex]["CITYSTATEZIP"].ToString())+"</prem_addr2>"; 
				}
				else if(gStrLobCode == "WAT")
				{
					DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +"<prem_name2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlindex]["HOLDER_NAME"].ToString())+"</prem_name2>"; 
					DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +"<prem_addr1 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlindex]["ADDRESS"].ToString())+"</prem_addr1>"; 
					DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +"<prem_addr2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlindex]["CITYSTATEZIP"].ToString())+"</prem_addr2>"; 
				}
				#endregion Addl Interest Information
			}
			DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<Agency_name " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</Agency_name>";
			DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<Agency_phone " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString()) + "</Agency_phone>";
			DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<prem_due " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(due_date) + "</prem_due>";
			DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<prem_tot " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["TOTAL_DUE"].ToString()) + "</prem_tot>";
			DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<prem_min " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["MIN_DUE"].ToString()) + "</prem_min>";
			DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<prem_pol " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</prem_pol>";
			//			DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<prem_tot " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</prem_tot>";
			//			DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<prem_amt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AMOUNT_PAID"].ToString()) + "</prem_amt>";
			DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<foot_tot_due " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["TOTAL_DUE"].ToString()) + "</foot_tot_due>";
			DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<foot_due_date " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(due_date) + "</foot_due_date>";
			DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<foot_min_due " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["MIN_DUE"].ToString()) + "</foot_min_due>";
			DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<foot_polnumber " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</foot_polnumber>";
			DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<foot_billdate " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DateTime.Now.ToString("MM/dd/yyyy")) + "</foot_billdate>";
			DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<foot_billplan " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PLAN_DESCRIPTION"].ToString()) + "</foot_billplan>";
			DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<foot_eff_date " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["EFFECTIVE_DATE"].ToString()) + "</foot_eff_date>";
			DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<nsf_fee " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["NSF_FEE"].ToString()) + "</nsf_fee>";
			DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<late_fee " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["LATE_FEE"].ToString()) + "</late_fee>";
			DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<ocra " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["OCRA"].ToString()) + "</ocra>";
			DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<foot_poltyp " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_TYPE"].ToString()) + "</foot_poltyp>";
			DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<foot_pol_term " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(app_term) + "</foot_pol_term>";

			//Ravindra(03-10-2008) If under full pay hide installment schedule
			string ShowInsSch = DSTempDataSet.Tables[0].Rows[0]["SHOW_INS_SCHEDULE"].ToString();

			if(ShowInsSch == "Y")
			{
				DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<Installment_Schedule " + fieldType + "=\"" + fieldTypeText + "\">Installment Schedule</Installment_Schedule>";
				DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<Due_Date " + fieldType + "=\"" + fieldTypeText + "\">Due Date</Due_Date>";
				DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<Amount_Due " + fieldType + "=\"" + fieldTypeText + "\">Amount</Amount_Due>";
			}
			else
			{
				DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<Installment_Schedule " + fieldType + "=\"" + fieldTypeText + "\"> </Installment_Schedule>";
				DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<Due_Date " + fieldType + "=\"" + fieldTypeText + "\"> </Due_Date>";
				DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<Amount_Due " + fieldType + "=\"" + fieldTypeText + "\"> </Amount_Due>";
			}
			string bill_plan = DSTempDataSet.Tables[0].Rows[0]["BILL_PLAN"].ToString().Trim();
			//if(DSTempDataSet.Tables.Count >= 3 && Ins_Lein == "INSURED" && DSTempDataSet.Tables[0].Rows[0]["BILL_PLAN"].ToString().IndexOf("Mortgagee Bill from Inception") >= 0 && DSTempDwellinAdd != null && DSTempDwellinAdd.Tables[0].Rows.Count > 0)
			if(bill_plan == BILL_TYPE_MORTGAGEE.ToString() && Ins_Lein == "INSURED" && DSTempDwellinAdd != null && DSTempDwellinAdd.Tables[0].Rows.Count > 0)
				DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<messg " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[2].Rows[0]["MESSAGE"].ToString() + " Your Mortgage Company has been billed for the Premium Due.") + "</messg>";
			else if(DSTempDataSet.Tables.Count >= 3)
				DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<messg " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[2].Rows[0]["MESSAGE"].ToString()) + "</messg>";
			if(DSTempDataSet.Tables.Count >= 4)
				DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<foot_messg " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[3].Rows[0]["MESSAGE"].ToString()) + "</foot_messg>";
			if(DSTempDataSet.Tables.Count >= 5)
			{
				/*
				if(DSTempDataSet.Tables[4].Rows.Count > 0)
				{
					DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<ins_sch " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Installment Schedule") + "</ins_sch>";
					DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<ins_date " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Date") + "</ins_date>";
					DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<Ins_amount " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Amount") + "</Ins_amount>";
					string strfloatX = "466";
					string strfloatY = "560";
					string strfloatW = "150";
					string strfloatH = "166";
					string strpageNo = "1";

					DecPageAccElement.InnerXml += "<INSTALLMENT " + fieldType +"=\""+ fieldTypeImage +"\" IMAGEPATH=\"" + RemoveJunkXmlCharacters("Installment Info.jpg") + "\" " + imageType + "=\"" + imageTypeYes + "\" " + floatX + "=\"" + strfloatX + "\" " + floatY + "=\"" + strfloatY + "\" " + floatW + "=\"" + strfloatW  + "\" " + floatH + "=\"" + strfloatH  + "\" " + pageNo + "=\"" + strpageNo  + "\"></INSTALLMENT>";
				}
				else
				{
					DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<ins_sch " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</ins_sch>";
					DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<ins_date " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</ins_date>";
					DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<Ins_amount " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</Ins_amount>";
				}
				*/
			}
			else
			{
				DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<ins_sch " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</ins_sch>";
				DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<ins_date " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</ins_date>";
				DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<Ins_amount " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</Ins_amount>";
			}
			string polNum = DSTempDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString();

			if(polNum.StartsWith("H") || polNum.StartsWith("R"))
			{
				DataSet DSLocDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_Dwelling_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'NOTICE'");
				if(DSLocDataSet != null && DSLocDataSet.Tables[0].Rows.Count > 0)
					DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<property " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSLocDataSet.Tables[0].Rows[0]["LOC_ADDRESS"].ToString() + ", " + DSLocDataSet.Tables[0].Rows[0]["LOC_CITYSTATEZIP"].ToString()) + "</property>";
			}
			else
			{
				DecPageAccElement.InnerXml = DecPageAccElement.InnerXml +  "<property_loc " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</property_loc>";
			}

		}
	
		private void createAccHistoryXML()
		{
			
			XmlElement DecPageAccHistElement;
			DecPageAccHistElement    = AcordPDFXML.CreateElement("ACCHIST");
			//			int DwellingCtr = 0,AddInt=0;			
			
			#region Account History for DecPage
			DecPageRootElement.AppendChild(DecPageAccHistElement);
			DecPageAccHistElement.SetAttribute(fieldType,fieldTypeMultiple);
			DecPageAccHistElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("PRMPAGE"));
			DecPageAccHistElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("PRMPAGE"));
			DecPageAccHistElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("PRMPAGEEXTN"));
			DecPageAccHistElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("PRMPAGEEXTN"));
			#endregion

			int RowCounter=0;
			XmlElement DecPageAccElement;
			DecPageAccElement = AcordPDFXML.CreateElement("ACCROW");
			DecPageAccHistElement.AppendChild(DecPageAccElement);
			DecPageAccElement.SetAttribute(fieldType,fieldTypeNormal);
			DecPageAccElement.SetAttribute(id,RowCounter.ToString());
			
			
			createPolicyInfoAllPages(ref DecPageAccElement);

			foreach(DataRow AccountHistory in DSTempDataSet.Tables[1].Rows)
			{
				if(RowCounter > 0)
				{
					DecPageAccElement = AcordPDFXML.CreateElement("ACCROW");
					DecPageAccHistElement.AppendChild(DecPageAccElement);
					DecPageAccElement.SetAttribute(fieldType,fieldTypeNormal);
					DecPageAccElement.SetAttribute(id,RowCounter.ToString());
					createPolicyInfoAllPages(ref DecPageAccElement);
				}

				if(AccountHistory["BILL_DATE"] != System.DBNull.Value)
					DecPageAccElement.InnerXml= DecPageAccElement.InnerXml + "<acc_date " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Convert.ToDateTime(AccountHistory["BILL_DATE"]).ToString("MM/dd/yyyy"))+"</acc_date>";
				DecPageAccElement.InnerXml= DecPageAccElement.InnerXml + "<acc_activity " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AccountHistory["ACTIVITY_DESC"].ToString())+"</acc_activity>";
				if(AccountHistory["EFF_DATE"] != System.DBNull.Value)
					DecPageAccElement.InnerXml= DecPageAccElement.InnerXml + "<acc_eff " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Convert.ToDateTime(AccountHistory["EFF_DATE"]).ToString("MM/dd/yyyy"))+"</acc_eff>";
				DecPageAccElement.InnerXml= DecPageAccElement.InnerXml + "<acc_amt " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AccountHistory["AMOUNT"].ToString())+"</acc_amt>";
				RowCounter++;
			}
		}
	
		private void createInstallmentHistoryXML()
		{
			
			XmlElement DecPageInsHistElement;
			DecPageInsHistElement    = AcordPDFXML.CreateElement("INSHIST");
			//			int DwellingCtr = 0,AddInt=0;			
			
			#region Account History for DecPage
			DecPageRootElement.AppendChild(DecPageInsHistElement);
			DecPageInsHistElement.SetAttribute(fieldType,fieldTypeMultiple);
			DecPageInsHistElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("PRMPAGE"));
			DecPageInsHistElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("PRMPAGE"));
			DecPageInsHistElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("PRMPAGEEXTN"));
			DecPageInsHistElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("PRMPAGEEXTN"));
			#endregion

			int RowCounter=0;
			foreach(DataRow InstallmentHistory in DSTempDataSet.Tables[4].Rows)
			{
				XmlElement DecPageInsElement;
				DecPageInsElement = AcordPDFXML.CreateElement("INSROW");
				DecPageInsHistElement.AppendChild(DecPageInsElement);
				DecPageInsElement.SetAttribute(fieldType,fieldTypeNormal);
				DecPageInsElement.SetAttribute(id,RowCounter.ToString());
				DecPageInsElement.InnerXml= DecPageInsElement.InnerXml + "<ins_amt " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(InstallmentHistory["AMOUNT"].ToString())+"</ins_amt>";
				DecPageInsElement.InnerXml= DecPageInsElement.InnerXml + "<ins_dat " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(InstallmentHistory["DUE_DATE"].ToString())+"</ins_dat>";
				RowCounter++;
			}
		}
		#endregion 

		
	}
}