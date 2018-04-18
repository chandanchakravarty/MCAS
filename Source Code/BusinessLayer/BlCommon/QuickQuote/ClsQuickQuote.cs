using System;
using System.Xml;
using System.Data.SqlClient;
using Cms.DataLayer;
using System.Data;
using Cms.Model.Quote;
using System.Configuration;
namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsQuickQuote.
	/// </summary>
	public class ClsQuickQuote: Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		public ClsQuickQuote()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public DataSet GetQuickQuoteInfo(string strCustomerId,string strQuoteId)
		{
			DataSet ldsQuickQuoteInfo = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_GetQuickQuoteInfo " + strCustomerId + "," + strQuoteId);
			return(ldsQuickQuoteInfo);
		}

		//CHANGED TO RETURN DATASET INSTEAD OF STRING:PRAVEEN KUMAR(23-02-2009)
		public DataSet GetClientStateInfo(string strCustomerId)
		{
			DataSet ldsQuickQuoteInfo = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_GetQuickQuoteClientStateInfo " + strCustomerId );
			return(ldsQuickQuoteInfo); 
			//			if (ldsQuickQuoteInfo.Tables[0].Rows.Count > 0)
			//				return(ldsQuickQuoteInfo.Tables[0].Rows[0][0].ToString().Trim());
			//			else
			//				return("");
		}
/// <summary>
/// Added on 03 Nov 2008 : Get State_ID from State Name for QQ
/// </summary>
/// <param name="strCustomerId"></param>
/// <param name="strQuoteId"></param>
/// <returns></returns>
		public DataSet GetStateIdQQ(string strStateName)
		{
			DataSet ds = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_GetStateIdQQ " + strStateName);
			return(ds);
		}

		//public string SaveQuickQuoteInfo(string strCustomerId,string strQuoteId,string QuoteNumber,string QQType)
		public string SaveQuickQuoteInfo(string strCustomerId,string strQuoteId,string QQType,string QQ_STATE)
		{
			string QuoteNumber="";
			DataSet ldsQuickQuoteInfo = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_GenerateQuickQuoteNumber '" + QQType + "'");
			QuoteNumber = ldsQuickQuoteInfo.Tables[0].Rows[0][0].ToString().Trim();

			ldsQuickQuoteInfo = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_InsertUpdateQuickQuoteInfo " + strCustomerId+ "," + strQuoteId + ",'" + QuoteNumber.Replace("'","''") + "','" + QQType + "','" + QQ_STATE + "'");
			string qq_type = "";
			if(QQType =="HOME")
			{
				qq_type = "Homeowners";
			}
			else if(QQType =="AUTOP")
			{
				qq_type = "Automobile";
			}
			else if(QQType =="CYCL")
			{
				qq_type = "Motorcycle";
			}
			else if(QQType =="BOAT")
			{
				qq_type = "Watercraft";
			}
			else if(QQType =="REDW")
			{
				qq_type = "Rental";
			}
            else if (QQType == "Motor")
            {
                qq_type = "Motor";
            }
		
			/*Transaction Log*/
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			objTransactionInfo.TRANS_TYPE_ID	=	1;
			objTransactionInfo.RECORDED_BY		=	int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString()); 
			objTransactionInfo.QUOTE_ID			=	int.Parse(ldsQuickQuoteInfo.Tables[0].Rows[0][0].ToString().Trim());
			objTransactionInfo.CLIENT_ID		=	int.Parse(strCustomerId);
			objTransactionInfo.TRANS_DESC		=	"New Quick Quote has been added";			
			objTransactionInfo.CUSTOM_INFO		=	"Quote Number : " + QuoteNumber  + "<br>LOB : " + qq_type + "<br>State : " + QQ_STATE;
			
			//Executing the query
			objDataWrapper.ExecuteNonQuery(objTransactionInfo);

			objDataWrapper.ClearParameteres();
			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			return(ldsQuickQuoteInfo.Tables[0].Rows[0][0].ToString().Trim());

		}

        public string SaveQuickQuoteInfo(string strCustomerId, string strQuoteId, string QQType)
        {
            string QuoteNumber = "";
            DataSet ldsQuickQuoteInfo = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr, System.Data.CommandType.Text, "Proc_GenerateQuickQuoteNumber '" + QQType + "'");
            QuoteNumber = ldsQuickQuoteInfo.Tables[0].Rows[0][0].ToString().Trim();

            ldsQuickQuoteInfo = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr, System.Data.CommandType.Text, "Proc_InsertUpdateQuickQuoteInfo " + strCustomerId + "," + strQuoteId + ",'" + QuoteNumber.Replace("'", "''") + "','" + QQType + "'");
            string qq_type = "";
            if (QQType == "Motor")
            {
                qq_type = "Motor";
            }
            else if (QQType == "Fire")
            {
                qq_type = "Fire";
            }
            else if (QQType == "Marine Cargo")
            {
                qq_type = "Marine Cargo";
            }

            /*Transaction Log*/
            DataWrapper objDataWrapper = new DataWrapper(ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);


            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
            objTransactionInfo.TRANS_TYPE_ID = 1;
            objTransactionInfo.RECORDED_BY = int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());
            objTransactionInfo.QUOTE_ID = int.Parse(ldsQuickQuoteInfo.Tables[0].Rows[0][0].ToString().Trim());
            objTransactionInfo.CLIENT_ID = int.Parse(strCustomerId);
            objTransactionInfo.TRANS_DESC = "New Quick Quote has been added";
            objTransactionInfo.CUSTOM_INFO = "Quote Number : " + QuoteNumber + "<br>LOB : " + qq_type;

            //Executing the query
            objDataWrapper.ExecuteNonQuery(objTransactionInfo);

            objDataWrapper.ClearParameteres();
            objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            return (ldsQuickQuoteInfo.Tables[0].Rows[0][0].ToString().Trim());

        }
		//To calculate the EARTHQUAKE ZONE 
		public string GetEarthquakeZone(string strLOBCode,string QQ_STATE,string strZipCode)
		{
			//string earthQuakeZone="";
			DataSet ldsQuickQuoteInfo = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_GetMNT_EARTHQUAKEZONE '" + strLOBCode + "','" + QQ_STATE + "','" + strZipCode + "'");
			if (ldsQuickQuoteInfo.Tables[0].Rows.Count > 0)
				return(ldsQuickQuoteInfo.Tables[0].Rows[0][0].ToString().Trim());
			else
				return("");
		}

		// Added by Mohit Agarwal
		private void GetQuickQuoteInfo(string customer_id, string qq_id, ref string qq_number, ref string qq_type, ref string qq_state)
		{
			try
			{
				DataSet ldsQuickQuoteInfo = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_GetQuickQuoteNumber " + customer_id + "," + qq_id);
				if (ldsQuickQuoteInfo != null && ldsQuickQuoteInfo.Tables[0].Rows.Count > 0)
				{
					qq_number = ldsQuickQuoteInfo.Tables[0].Rows[0]["QQ_NUMBER"].ToString();
					qq_type = ldsQuickQuoteInfo.Tables[0].Rows[0]["LOB_DESC"].ToString();
					qq_state = ldsQuickQuoteInfo.Tables[0].Rows[0]["QQ_STATE"].ToString();
				}
			}
			catch(Exception ex)
			{
			}
		}

        public int GetQuickQuoteDetails(string customer_id, string qq_number)
        {
            int qq_id = 0;
            try
            {
                DataSet ldsQuickQuoteInfo = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr, System.Data.CommandType.Text, "Proc_GetQuickQuoteID " + customer_id + "," + qq_number);
                if (ldsQuickQuoteInfo != null && ldsQuickQuoteInfo.Tables[0].Rows.Count > 0)
                {
                    qq_id = int.Parse(ldsQuickQuoteInfo.Tables[0].Rows[0]["QQ_ID"].ToString());
                   
                }
            }
            catch (Exception ex)
            {
                qq_id = 0;
            }
            return qq_id;
        }

        public int GetCustomerParticularID(int customerID, int custpartID)
        {
            int qq_id = 0;
            try
            {
                DataSet ldsQuickQuoteInfo = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr, System.Data.CommandType.Text, "Proc_GetCustomerParticularInfo " + customerID + "," + custpartID);
                
                if (ldsQuickQuoteInfo != null && ldsQuickQuoteInfo.Tables[0].Rows.Count > 0)
                {
                    qq_id = int.Parse(ldsQuickQuoteInfo.Tables[0].Rows[0]["ID"].ToString());

                }
            }
            catch (Exception ex)
            {
                qq_id = 0;
            }
            return qq_id;
        }
		//
        //Created By kuldeep for Marine Cargo Invoice Details on 19-03-2012
        public int GetInvoiceParticularID(int customerID, int custpartID)
        {
            int qq_id = 0;
            try
            {
                DataSet ldsQuickQuoteInfo = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr, System.Data.CommandType.Text, "Proc_GetInvoiceParticularInfo " + customerID + "," + custpartID);
                
                if (ldsQuickQuoteInfo != null && ldsQuickQuoteInfo.Tables[0].Rows.Count > 0)
                {
                    qq_id = int.Parse(ldsQuickQuoteInfo.Tables[0].Rows[0]["ID"].ToString());

                }
            }
            catch (Exception ex)
            {
                qq_id = 0;
            }
            return qq_id;
        }
		public void UpdateQuickQuoteXml(string strCustomerId,string strQuoteId,string QuoteXml)
		{
			DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_UpdateQuickQuoteXml " + strCustomerId+ "," + strQuoteId + ",'" + QuoteXml.Replace("'","''") + "'");
		}

		public string UpdateGaragedLocationAddress(string QuoteXml,string LOBID)
		{
			XmlDocument lxmlDoc = new XmlDocument();
			lxmlDoc.LoadXml(QuoteXml);
			foreach (XmlNode Node in lxmlDoc.SelectNodes("//vehicles/*"))
			{
				//fetch the element <TerrCodeGaragedLocation> : It Contains the Terr Code 
				string terrCode = "";
				if(Node.SelectSingleNode("TerrCodeGaragedLocation").InnerText!="")
				{
					terrCode = Node.SelectSingleNode("TerrCodeGaragedLocation").InnerText.ToString().Trim();
				}
				//DataSet ldsQuickQuoteInfo = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"SELECT  TOP 1 ISNULL(COUNTY,'') +'  COUNTY, ' + ISNULL(CITY,'') +' ('+ ZIP+')' + ', TERRITORY : '+ CONVERT(NVARCHAR(5),TERR) [ADD] FROM MNT_TERRITORY_CODES WHERE LOBID=" + LOBID + " AND ZIP='" + Node.SelectSingleNode("ZipCodeGaragedLocation").InnerText.ToString() + "'");
				DataSet ldsQuickQuoteInfo = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"SELECT  TOP 1 ISNULL(COUNTY,'') +'  COUNTY, ' + ISNULL(CITY,'') +' ('+ ZIP+')' + ', TERRITORY : '+ CONVERT(NVARCHAR(5),TERR) [ADD] FROM MNT_TERRITORY_CODES WHERE LOBID=" + LOBID + " AND ZIP='" + Node.SelectSingleNode("ZipCodeGaragedLocation").InnerText.ToString() + "' AND TERR ='"+ terrCode +"'");
				if (Node.SelectSingleNode("GARAGEDLOCATION")!=null)
				{
					if (ldsQuickQuoteInfo.Tables[0].Rows.Count > 0)
						Node.SelectSingleNode("GARAGEDLOCATION").InnerText = ldsQuickQuoteInfo.Tables[0].Rows[0]["ADD"].ToString();
					else
						Node.SelectSingleNode("GARAGEDLOCATION").InnerText = "";
				}
			}
			if (lxmlDoc.SelectSingleNode("quickQuote/policy/TERRITORY")!=null)
			{
				DataSet DS = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"SELECT TERR FROM MNT_TERRITORY_CODES WHERE LOBID=" + LOBID + " AND ZIP='" + lxmlDoc.SelectSingleNode("quickQuote/policy/zipCode").InnerText.ToString().Trim() + "'");
				if (DS.Tables[0].Rows.Count > 0)
					lxmlDoc.SelectSingleNode("quickQuote/policy/TERRITORY").InnerText = DS.Tables[0].Rows[0][0].ToString().Trim();
				else
					lxmlDoc.SelectSingleNode("quickQuote/policy/TERRITORY").InnerText = "";
			}
			string strXml =lxmlDoc.OuterXml.ToString().Replace("'","&apos;");
			return (strXml);
		}

		public void ActivateDeactivateQuickQuote(string strCustomerId,string strQuoteId,string strStatus)
		{
			DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_ActivateDeactivateQuickQuoteInfo " + strCustomerId+ "," + strQuoteId);

			/*Transaction Log*/
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			string qq_num = "", qq_type = "", qq_state = "";
			GetQuickQuoteInfo(strCustomerId, strQuoteId, ref qq_num, ref qq_type, ref qq_state);
 
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			objTransactionInfo.TRANS_TYPE_ID	=	1;
			objTransactionInfo.RECORDED_BY		=	int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString()); 
			objTransactionInfo.QUOTE_ID			=	int.Parse(strQuoteId.ToString().Trim());
			objTransactionInfo.CLIENT_ID		=	int.Parse(strCustomerId);
			if(strStatus == "Deactivate")
				objTransactionInfo.TRANS_DESC		=	"QuickQuote has been Deactivated";			
			else
				objTransactionInfo.TRANS_DESC		=	"QuickQuote has been Activated";			
			objTransactionInfo.CUSTOM_INFO		=	"Quote Number : " + qq_num  + "<br>LOB : " + qq_type + "<br>State : " + qq_state;
			//Executing the query
			objDataWrapper.ExecuteNonQuery(objTransactionInfo);

			objDataWrapper.ClearParameteres();
			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
		}
		
		public void DeleteQuickQuote(string strCustomerId,string strQuoteId)
		{

			/*Transaction Log*/
			string qq_num = "", qq_type = "", qq_state = "";
			GetQuickQuoteInfo(strCustomerId, strQuoteId, ref qq_num, ref qq_type, ref qq_state);

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			objTransactionInfo.TRANS_TYPE_ID	=	1;
			objTransactionInfo.RECORDED_BY		=	int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString()); 
			objTransactionInfo.QUOTE_ID			=	int.Parse(strQuoteId.ToString().Trim());
			objTransactionInfo.CLIENT_ID		=	int.Parse(strCustomerId);
			objTransactionInfo.TRANS_DESC		=	"QuickQuote has been deleted";			
			objTransactionInfo.CUSTOM_INFO		=	"Quote Number : " + qq_num  + "<br>LOB : " + qq_type + "<br>State : " + qq_state;
			//Executing the query
			objDataWrapper.ExecuteNonQuery(objTransactionInfo);

			objDataWrapper.ClearParameteres();
			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			/*End : Transaction Log*/

			DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_DeleteQuickQuoteInfo " + strCustomerId+ "," + strQuoteId);

			
		}

		//Added on 16 may 2008 : iTRACK 4205
		public string CheckAppNumberQQ(string strCustomerId,string strQuoteId)
		{
			DataSet ldsQQStatus = null;
			string appNumber = "";
			try
			{
				ldsQQStatus = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,CommandType.Text,"Proc_GetQuickQuoteNumber " + strCustomerId+ "," + strQuoteId);
				if(ldsQQStatus.Tables[0].Rows.Count > 0)
				{
					appNumber = ldsQQStatus.Tables[0].Rows[0]["QQ_APP_NUMBER"].ToString().Trim();
				}
				return(appNumber);
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}

		public string GetQuickQuoteXml(string strCustomerId,string strQuoteId)
		{
			DataSet ldsQuickQuoteInfo = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_GetQuickQuoteInfo " + strCustomerId+ "," + strQuoteId);
			return(ldsQuickQuoteInfo.Tables[0].Rows[0]["QQ_XML"].ToString().Trim());
		}

		public void UpdateQuickQuoteAppNumber(string strCustomerId,string strQuoteId,string AppNumber)
		{
			DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_UpdateQuickQuoteAppNumber " + strCustomerId+ "," + strQuoteId + ",'" + AppNumber.Replace("'","''") + "'");
		}

		public void UpdateQuickQuoteRatingReport(string strCustomerId,string strQuoteId,string RatingReport)
		{
			DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_UpdateQuickQuoteRatingReport " + strCustomerId+ "," + strQuoteId + ",'" + RatingReport.Replace("'","''") + "'");
		}
		public string GetQuickQuoteStatus(string strCustomerId,string strQuoteId)
		{
            DataSet ldsQQStatus = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,CommandType.Text,"Proc_GetQuickQuoteStatus " + strCustomerId+ "," + strQuoteId);
			return(ldsQQStatus.Tables[0].Rows[0]["IS_ACTIVE"].ToString().Trim());
		}
		public string CheckZipCode(string StateName,string LobCd,string ZipCode,string polEffectiveDate)
		{
			DataSet ldsQuickQuoteInfo = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_CheckQuickQuoteZipCode '" + LobCd + "','" + StateName + "','" + ZipCode + "','" + polEffectiveDate +"'");
			if (ldsQuickQuoteInfo.Tables[0].Rows.Count > 0)
				return(ldsQuickQuoteInfo.Tables[0].Rows[0][0].ToString().Trim());
			else
				return("");
		}
		public string CheckZipCode(string StateName,string LobCd,string ZipCode)
		{
			DataSet ldsQuickQuoteInfo = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_CheckQuickQuoteZipCode '" + LobCd + "','" + StateName + "','" + ZipCode + "'");
			if (ldsQuickQuoteInfo.Tables[0].Rows.Count > 0)
				return(ldsQuickQuoteInfo.Tables[0].Rows[0][0].ToString().Trim());
			else
				return("");
		}
		#region ZIPCODE for Auto Commercial
		public string CheckZipCodeAutoComm(string StateName,string LobCd,string ZipCode,string polEffectiveDate)
		{
			DataSet ldsQuickQuoteInfo = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_CheckQuickQuoteZipCodeAutoComm '" + LobCd + "','" + StateName + "','" + ZipCode + "','" + polEffectiveDate +"'");
			if (ldsQuickQuoteInfo.Tables[0].Rows.Count > 0)
				return(ldsQuickQuoteInfo.Tables[0].Rows[0][0].ToString().Trim());
			else
				return("");
		}
		#endregion

		public string GetQuickQuoteRatingReport(string strCustomerId,string strQuoteId)
		{
			DataSet ldsQuickQuoteInfo = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_GetQuickQuoteRatingReport " + strCustomerId+ "," + strQuoteId);
			return(ldsQuickQuoteInfo.Tables[0].Rows[0]["QQ_RATING_REPORT"].ToString().Trim());
		}
		public bool CheckQuickQuoteUpdateForRating(string strCustomerId,string strQuoteId)
		{
			DataSet ldsQuickQuoteInfo = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"SELECT 1 FROM CLT_QUICKQUOTE_LIST  WHERE CUSTOMER_ID=" + strCustomerId + " AND QQ_ID=" + strQuoteId + " AND ISNULL(QQ_XML_TIME,'1-1-1900') > ISNULL(QQ_RATING_TIME,'1-1-1900')");
			if (ldsQuickQuoteInfo.Tables[0].Rows.Count > 0)
				return(true);
			else
				return(false);
		}
		public string GetQuickQuoteOthDtls(string strCustomerId,string strQuickQuoteId,string strLob,string UserId,string CompanyCode)
		{
			DataSet ldsQuickQuoteOthDtls = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_GetQQCoApplicantDetails " + strCustomerId);
			DataSet DS = GetQuickQuoteInfo(strCustomerId,strQuickQuoteId);
			string strQuickQuoteOthDtls =	"<quickquote>" + 
												"<QuickQuoteNumber>" + DS.Tables[0].Rows[0]["QQ_NUMBER"].ToString().Trim() + "</QuickQuoteNumber>" +
												"<QuickQuoteLob>" + strLob + "</QuickQuoteLob>" +
												"<AppNumber>" + DS.Tables[0].Rows[0]["QQ_APP_NUMBER"].ToString().Trim() + "</AppNumber>" +
												"<QuickQuoteXmlSaved>0</QuickQuoteXmlSaved>" + 
												"<QuickQuoteZip></QuickQuoteZip>" +
												"<CustomerAge>0</CustomerAge>" +
												"<UserType></UserType>";

			foreach (DataRow Row in ldsQuickQuoteOthDtls.Tables[0].Rows)
			{
				strQuickQuoteOthDtls = strQuickQuoteOthDtls + "<Driver>" + 
																	"<FName>" + Row["FNAME"].ToString().Trim() + "</FName>" +
																	"<MName>" + Row["MNAME"].ToString().Trim() + "</MName>" +
																	"<LName>" + Row["LNAME"].ToString().Trim() + "</LName>" +
																	"<DOB>" + Row["DOB"].ToString().Trim() + "</DOB>" +
																	"<MaritalStatus>" + Row["MARITALSTATUS"].ToString().Trim() + "</MaritalStatus>" +
																	"<Gender>" + Row["GENDER"].ToString().Trim() + "</Gender>" +
																"</Driver>";
			}
			strQuickQuoteOthDtls = strQuickQuoteOthDtls + "</quickquote>";

			if (DS.Tables[0].Rows[0]["QQ_XML"].ToString().Trim() != "")
				strQuickQuoteOthDtls=strQuickQuoteOthDtls.Replace("<QuickQuoteXmlSaved>0</QuickQuoteXmlSaved>","<QuickQuoteXmlSaved>1</QuickQuoteXmlSaved>");
			
			ldsQuickQuoteOthDtls = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_GetQuickQuoteClientStateInfo " + strCustomerId);
			if (ldsQuickQuoteOthDtls.Tables[0].Rows[0]["STATE"].ToString().Trim().ToUpper() == DS.Tables[0].Rows[0]["QQ_STATE"].ToString().Trim().ToUpper())
				strQuickQuoteOthDtls=strQuickQuoteOthDtls.Replace("<QuickQuoteZip></QuickQuoteZip>","<QuickQuoteZip>" + ldsQuickQuoteOthDtls.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString().Trim().Substring(0,5) + "</QuickQuoteZip>");

			if (ldsQuickQuoteOthDtls.Tables[0].Rows[0]["DATE_OF_BIRTH"].ToString().Trim() != "")
			{
				/*Commented Date of Birth*/
				//int Age = System.DateTime.Now.Year - DateTime.Parse(ldsQuickQuoteOthDtls.Tables[0].Rows[0]["DATE_OF_BIRTH"].ToString().Trim()).Year;
				//Passing full DOB,without calculating age,Age will be calculated upon policy date on QQ 
				string cutomerDOB = ldsQuickQuoteOthDtls.Tables[0].Rows[0]["DATE_OF_BIRTH"].ToString().Trim();
				strQuickQuoteOthDtls=strQuickQuoteOthDtls.Replace("<CustomerAge>0</CustomerAge>","<CustomerAge>" + cutomerDOB.ToString() + "</CustomerAge>");
			}
			
			ldsQuickQuoteOthDtls = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"SELECT CASE USER_SYSTEM_ID WHEN '" + CompanyCode + "' THEN 'COMPANY' ELSE 'AGENCY' END USER_TYPE FROM MNT_USER_LIST WHERE USER_ID=" + UserId);
			strQuickQuoteOthDtls=strQuickQuoteOthDtls.Replace("<UserType></UserType>","<UserType>" + ldsQuickQuoteOthDtls.Tables[0].Rows[0]["USER_TYPE"].ToString().Trim() + "</UserType>");
			
			return(strQuickQuoteOthDtls);
		}
		public DataSet GetLineOfBusinesses()
		{
			DataSet ldsLOB = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"SELECT LOB_CODE,LOB_DESC FROM MNT_LOB_MASTER WHERE LOB_CODE NOT IN ('GENL','UMB') ORDER BY 2");
			return(ldsLOB);
		}
		//**
		#region Get Violation types
		//For fetching Violation types according to the State and LOB : 04
		public string GetViolationTypeQQ(string strStateName,string strLobcode)
		{
			DataSet ldsViolationModel = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_GetViolationType " + strStateName + ",'" + strLobcode + "'");
			string violationXml="";
			foreach(DataRow Row in ldsViolationModel.Tables[0].Rows)
			{
				violationXml = violationXml + Row[0].ToString();
			}
			violationXml = violationXml.Replace("<VIOLATIONS/>","");
			violationXml = violationXml.Replace("<VIOLATIONS ","<VIOLATION_TYPE ");
			violationXml = "<VTYPE><VIOLATION_TYPE VIOLATION_ID=\"\" VIOLATION_DES=\"\"/>" + violationXml + "</VTYPE>";

			return(violationXml);
		}

		//Getting Description according to the Violation Types: 07 April 2006
		public string GetViolationDescQQ(string strviolationID,string strStateName,string strLobcode)
		{
			DataSet ldsViolationModel = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_GetMNT_VIOLATIONS_QQ " + strviolationID + ",'" + strStateName + "'" + ",'" + strLobcode + "'");
			string violationXml="";
			foreach(DataRow Row in ldsViolationModel.Tables[0].Rows)
			{
				violationXml = violationXml + Row[0].ToString();
			}
			violationXml = violationXml.Replace("<VIOLATIONS/>","");
			violationXml = violationXml.Replace("<VIOLATIONS ","<VIOLATIONDESC ");
			violationXml = "<VDESC><VIOLATION_TYPE VIOLATIONDESC_ID=\"\" MVRPOINTS=\"\" WOLVERINE_VIOLATIONS=\"\" VIOLATION_CODE=\"\" VIOLATION_DES=\"\"/>" + violationXml + "</VDESC>";

			return(violationXml);
		}


		//End 
		#endregion


		
//		public int GetMVRPointsForSurcharge(int CustomerID, int AppID, int AppVersionID, int DriverID, int NumYear,string strCalledFrom)
//		{
//			int returnResult = -1;
//			string strProc;
//			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
//			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
//			objDataWrapper.AddParameter("@NUM_YEAR",NumYear);
//			objDataWrapper.AddParameter("@DRIVER_ID",DriverID);
//			if(strCalledFrom.ToUpper()=="APP")
//			{
//				objDataWrapper.AddParameter("@APP_ID",AppID);
//				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID);				 
//				strProc = "GetMVRViolationPoints";
//			}
//			else
//			{
//				objDataWrapper.AddParameter("@POLICY_ID",AppID);
//				objDataWrapper.AddParameter("@POLICY_VERSION_ID",AppVersionID);		
//				strProc = "GetMVRViolationPointsPol";
//			}			
//			
//			SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETURN",null,SqlDbType.Int ,ParameterDirection.ReturnValue);				
//			objDataWrapper.ExecuteNonQuery(strProc);				
//			objDataWrapper.ClearParameteres();
//			if(objSqlParameter.Value.ToString()!="")
//				returnResult = Convert.ToInt32(objSqlParameter.Value);
//			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
//			objDataWrapper.Dispose();
//			return returnResult;
//
//		}

		public static DataSet GetAccidentViolationConfigData(int LOB_ID)
		{
			XmlDocument xAccidentViolation = new XmlDocument();
			try
			{
				string strPath ;
				if(IsEODProcess)
				{
					strPath = WebAppUNCPath +  @"\cmsweb\support\AccidentViolationConfig.xml";
				}
				else
				{
					strPath= System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/AccidentViolationConfig.xml");				
				}
				xAccidentViolation.Load(strPath);								
				XmlNode xNode = xAccidentViolation.SelectSingleNode("Configuration/AccidentPoints/LOB[@ID='" + LOB_ID.ToString() + "']/Accident");				
				string strAcc = "<Configuration>";
				if(xNode!=null)
					strAcc+=xNode.OuterXml.ToString();
				xNode = xAccidentViolation.SelectSingleNode("Configuration/ViolationPoints/LOB[@ID='" + LOB_ID.ToString() + "']/Violation");								
				//System.IO.StringReader objStringReader = new System.IO.StringReader(xAccidentViolation.SelectSingleNode("Configuration/AccidentPoints/LOB[@ID='" + LOB_ID.ToString() + "']/Accident").OuterXml.ToString());				
				if(xNode!=null)
					strAcc+=xNode.OuterXml.ToString();
				strAcc+="</Configuration>";
				System.IO.StringReader objStringReader = new System.IO.StringReader(strAcc);
				DataSet dsTemp = new DataSet();
				dsTemp.ReadXml(objStringReader);
				DataTable dtTemp = new DataTable();
				if(dsTemp!=null && dsTemp.Tables.Count>0)
				{
					return dsTemp;
				}
				else
					return null;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(xAccidentViolation!=null)
					xAccidentViolation = null;
			}
		}
		public static string GetAccidentViolationConfigData(int LOB_ID, string strstate,string streffectivedate)
		{
			XmlDocument xAccidentViolation = new XmlDocument();
			try
			{
				string strPath ;
				string strSurchargeXml = "";
				if(IsEODProcess)
				{
					strPath = WebAppUNCPath +  @"\cmsweb\support\AccidentViolationConfig.xml";
				}
				else
				{
					strPath= System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/AccidentViolationConfig.xml");				
				}
				xAccidentViolation.Load(strPath);
				// Accident Xml
				foreach(XmlNode Node in xAccidentViolation.SelectNodes("Configuration/AccidentPoints/*"))
				{
					if(Node.Attributes["ID"].Value.ToString() == LOB_ID.ToString())
					{
						if (DateTime.Parse(streffectivedate) >= DateTime.Parse(Node.Attributes["START"].Value.ToString()) && DateTime.Parse(streffectivedate) <= DateTime.Parse(Node.Attributes["END"].Value.ToString()))
						{
							if(Node.Attributes["STATE"].Value.ToString().ToUpper() == strstate.ToUpper())  //changed to uppercase :itrack 5566
								{
									strSurchargeXml = "<LOBACC>" +  Node.OuterXml ;
								}
						}
					}
				}			
				//XmlNode xNode = xAccidentViolation.SelectSingleNode("Configuration/AccidentPoints/LOB[@ID='" + LOB_ID.ToString() + "' + and + @START =< '"+ streffectivedate +"' + and + @END >= '" +streffectivedate+ "' + and +@STATE = '" + strstate + "'");
				//string strAcc = "<Configuration>";
				// Violation Xml
				foreach(XmlNode Node in xAccidentViolation.SelectNodes("Configuration/ViolationPoints/*"))
				{
					if(Node.Attributes["ID"].Value.ToString() == LOB_ID.ToString())
					{
						if (DateTime.Parse(streffectivedate) >= DateTime.Parse(Node.Attributes["START"].Value.ToString()) && DateTime.Parse(streffectivedate) <= DateTime.Parse(Node.Attributes["END"].Value.ToString()))
						{
							if(Node.Attributes["STATE"].Value.ToString().ToUpper() == strstate.ToUpper())  //changed to uppercase :itrack 5566
							{
								strSurchargeXml += Node.OuterXml + "</LOBACC>";
							}
						}
					}
				}
			//	xNode = xAccidentViolation.SelectSingleNode("Configuration/ViolationPoints/LOB[@ID='" + LOB_ID.ToString() + "']/Violation");								
				//System.IO.StringReader objStringReader = new System.IO.StringReader(xAccidentViolation.SelectSingleNode("Configuration/AccidentPoints/LOB[@ID='" + LOB_ID.ToString() + "']/Accident").OuterXml.ToString());				
			//	if(xNode!=null)
			//		strAcc+=xNode.OuterXml.ToString();
				
//				System.IO.StringReader objStringReader = new System.IO.StringReader(strSurchargeXml);
//				DataSet dsTemp = new DataSet();
//				dsTemp.ReadXml(objStringReader);
//				DataTable dtTemp = new DataTable();
//				if(dsTemp!=null && dsTemp.Tables.Count>0)
//				{
//					return dsTemp;
//				}
//				else
//					return null;
				return strSurchargeXml;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
		#region GET WATERCRAFT VIOLATIONS
		public DataTable GetMVRPoints(int CustomerID, int AppID, int AppVersionID, int DriverID, string strCalledFrom)
		{
			return GetMVRPoints(CustomerID, AppID, AppVersionID, DriverID, strCalledFrom,null);
		}
		#endregion
		public DataTable GetMVRPoints(int CustomerID, int AppID, int AppVersionID, int DriverID, string strCalledFrom,DataWrapper objDataWrapper)
		{
			string strProc= "Proc_GetWatercraftMVRViolations";
			
			if(objDataWrapper==null)
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
			objDataWrapper.AddParameter("@DRIVER_ID",DriverID);
			objDataWrapper.AddParameter("@APP_ID",AppID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID);
			objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom);		 
							
			
			DataSet dsMVRpoints = objDataWrapper.ExecuteDataSet(strProc);				
			objDataWrapper.ClearParameteres();
			
			if(dsMVRpoints!=null && dsMVRpoints.Tables.Count>0 )
				return dsMVRpoints.Tables[0];
			else
				return null;

			/*string strTotalVioalions ="";
			try
			{
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objDataWrapper.AddParameter("@DRIVER_ID",DriverID);
				objDataWrapper.AddParameter("@APP_ID",AppID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID);
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom);	

				DataSet ds = objWrapper.ExecuteDataSet(strProc);
				if(ds!=null && ds.Tables.Count>0)
					return ds.Tables[0];
				else
					return null;

			}
			catch(Exception ex)
			{
				throw ex;
			}*/

		}

		#region GET VIOLATION AND ACCIDENT POINTS 
		public DataTable GetMVRPointsForSurcharge(int CustomerID, int AppID, int AppVersionID, int DriverID, string strCalledFrom)
		{
			return GetMVRPointsForSurcharge(CustomerID, AppID, AppVersionID, DriverID, strCalledFrom,null);
		}		
		/// <summary>
		/// Gets the Sum of violation and accident points of any driver
		/// </summary>
		/// <param name="CustomerID">CustomerID
		/// <param name="AppID">AppID--For application, app_id is passed whereas for policy, policyId is passed
		/// <param name="AppVersionID">AppVersionID--For application, AppVersionID is passed whereas for policy, policyVersionId is passed
		/// <param name="CustomerID">DriverID
		/// <param name="strCalledFrom">strCalledFrom--Called_From---App or Pol
		/// <returns DataTable>MVR Points ^ Accident Points ^ Count of Accident Points</returns>
		public DataTable GetMVRPointsForSurcharge(int CustomerID, int AppID, int AppVersionID, int DriverID, string strCalledFrom,DataWrapper objDataWrapper)
		{
			//int returnResult = -1;
			string strProc;
			int ViolationNumYears=0,AccidentNumYears=0;
			int LOB_ID=0;
			string strstate="", streffectivedate="";
			string straccipaidamount="",strviopaidamount="",AcciChargableNumYears="",VioiChargableNumYears="",AcciNonChargableNumYears="",VioiNonChargableNumYears="",AcciMajorVioChargeNumYears="",VioMajorVioChargeNumYears="";
			string[] stateID = new string[0];
			if(strCalledFrom=="APP")
			{
				LOB_ID = GetApplicationLOBID(CustomerID,AppID,AppVersionID,objDataWrapper);
				strstate = GetApplicationDateadState(CustomerID,AppID,AppVersionID,objDataWrapper);
				if(strstate !="0")
				{
					stateID = strstate.Split('^');
					strstate = stateID[1];
					streffectivedate = stateID[0];
				}
			}
			else
			{
				LOB_ID = GetPolicyLOBID(CustomerID,AppID,AppVersionID,objDataWrapper);
				strstate = GetPolicyDateadState(CustomerID,AppID,AppVersionID,objDataWrapper);
				if(strstate !="0")
				{
					stateID = strstate.Split('^');
					strstate = stateID[1];
					streffectivedate = stateID[0];
				}
			}
				
			string  strAccViolations =  GetAccidentViolationConfigData(LOB_ID,strstate,streffectivedate);
			if(strAccViolations!=null && strAccViolations != "")
			{
				strAccViolations.Replace("\"","'");
				//strAccViolations.Replace(""","'");

				XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strAccViolations);
				
				// Accident Details parameter
					AccidentNumYears = int.Parse(xmlDoc.SelectSingleNode("LOBACC/LOB/Accident/NumYears").InnerText);
					straccipaidamount = xmlDoc.SelectSingleNode("LOBACC/LOB/Accident/PaidAmount").InnerText;
					AcciChargableNumYears = xmlDoc.SelectSingleNode("LOBACC/LOB/Accident/ChargableNumYears").InnerText;
					AcciNonChargableNumYears = xmlDoc.SelectSingleNode("LOBACC/LOB/Accident/NonChargableNumYears").InnerText;
					AcciMajorVioChargeNumYears = xmlDoc.SelectSingleNode("LOBACC/LOB/Accident/MajorVioChargeNumYears").InnerText;
				
				// Violation Details parameter
					ViolationNumYears = int.Parse(xmlDoc.SelectSingleNode("LOBACC/LOB/Violation/NumYears").InnerText);
					strviopaidamount = xmlDoc.SelectSingleNode("LOBACC/LOB/Violation/PaidAmount").InnerText;
					VioiChargableNumYears = xmlDoc.SelectSingleNode("LOBACC/LOB/Violation/ChargableNumYears").InnerText;
					VioiNonChargableNumYears = xmlDoc.SelectSingleNode("LOBACC/LOB/Violation/NonChargableNumYears").InnerText;
					VioMajorVioChargeNumYears = xmlDoc.SelectSingleNode("LOBACC/LOB/Violation/MajorVioChargeNumYears").InnerText;


			}
			DataSet dsViolations;
			
			if(objDataWrapper==null)
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
			objDataWrapper.AddParameter("@ACCIDENT_NUM_YEAR",AccidentNumYears);			
			objDataWrapper.AddParameter("@VIOLATION_NUM_YEAR",ViolationNumYears);
			objDataWrapper.AddParameter("@ACCIDENT_PAID_AMOUNT",straccipaidamount);
//			objDataWrapper.AddParameter("@ACCIDENT_CHARGE_NUM_YEARS",AcciChargableNumYears);
//			objDataWrapper.AddParameter("@ACCIDENT_NONCHARGE_NUM_YEARS",AcciNonChargableNumYears);
//			objDataWrapper.AddParameter("@ACCIDENT_MAJCHARGE_NUM_YEARS",AcciMajorVioChargeNumYears);
//			objDataWrapper.AddParameter("@VIOLATION_PAID_AMOUNT",strviopaidamount);
//			objDataWrapper.AddParameter("@VIOLATION_CHARGE_NUM_YEARS",VioiChargableNumYears);
			objDataWrapper.AddParameter("@VIOLATION_NONCHARGE_NUM_YEARS",VioiNonChargableNumYears);
//			objDataWrapper.AddParameter("@VIOLATION_MAJCHARGE_NUM_YEARS",VioMajorVioChargeNumYears);
			objDataWrapper.AddParameter("@DRIVER_ID",DriverID);
			if(strCalledFrom.ToUpper()=="APP")
			{
				objDataWrapper.AddParameter("@APP_ID",AppID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID);
				if(LOB_ID==3) 
				{
					strProc = "GetMotorMVRViolationPoints";
				}
				else
				{
					strProc = "GetMVRViolationPoints";
				}
			}
			else
			{
				objDataWrapper.AddParameter("@POLICY_ID",AppID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",AppVersionID);
				if(LOB_ID==3 || LOB_ID == 38)
				{
					strProc = "GetMotorMVRViolationPointsPol";
				}
				else
				{
					strProc = "GetMVRViolationPointsPol";
				}
				
			}			
			
			//SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETURN",null,SqlDbType.Int ,ParameterDirection.ReturnValue);				
			dsViolations = objDataWrapper.ExecuteDataSet(strProc);				
			objDataWrapper.ClearParameteres();
			//if(objSqlParameter.Value.ToString()!="")
			//	returnResult = Convert.ToInt32(objSqlParameter.Value);
			//objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			//objDataWrapper.Dispose();
			//return returnResult;
			if(dsViolations!=null && dsViolations.Tables.Count>0)
				return dsViolations.Tables[0];
			else
				return null;

		}

		#endregion


		#region GET VIOLATION AND ACCIDENT POINTS 
		/// <summary>
		/// Gets the Sum of violation and accident points of any driver
		/// </summary>
		/// <param name="violationInputXml">Violation Input XML. This contains the Policy node and the violations node along with LOBCODE in the Policy node</param>
		/// <returns>MVR Points ^ Violation Points ^ Accident Point</returns>
		public string GetMVRPointsForSurcharge(string violationInputXml,string LobCode)
		{

			//int accidentPoints = 0, additionalAccidentPoints =0, 
			//double chargeableAccidentAmount=0;

			//additionalAccidentPoints =4, 
			//Apply business logic depending on the LOBCODE - Motor/PPA/Watercraft 
			string mvr="";
			double mvrPoints =0;
			double violationPoints = 0;
			double AccidentPoints = -1;
			int firstAccidentPoints = 3;
			int amountPaid;
			int yearsContInsuredWithWolverine = 0;
			
			XmlDocument MvrPointsDoc = new XmlDocument();
			MvrPointsDoc.LoadXml(violationInputXml);

			XmlNode MvrPointsNode = MvrPointsDoc.SelectSingleNode("ViolationsMVRPoints");
			string strState;
			DateTime dtEffdate;

			if (LobCode == "BOAT")
				dtEffdate =DateTime.Parse(MvrPointsNode.SelectSingleNode("QUOTEEFFDATE").InnerText.ToString());
			else
				//Case QQ//dtEffdate =DateTime.Parse(MvrPointsNode.SelectSingleNode("quoteEffDate").InnerText.ToString());
				dtEffdate =DateTime.Parse(MvrPointsNode.SelectSingleNode("QUOTEEFFDATE").InnerText.ToString());


			strState = MvrPointsNode.SelectSingleNode("STATENAME").InnerText.ToString();
			
			//Check amount paid.For motor/Baot Considered to be accident if amount paid > 500
			if (LobCode == "CYCL" || LobCode == "BOAT")
			{
				amountPaid = 500;
			}
			else	
			{
				//For Auto check for 1000 amount.Considered to be accident if amount paid > 1000
				amountPaid = 1000;
				//yearsContInsuredWithWolverine = Convert.ToInt32(MvrPointsNode.SelectSingleNode("yearsContInsuredWithWolverine").InnerText);
				yearsContInsuredWithWolverine = Convert.ToInt32(MvrPointsNode.SelectSingleNode("YEARSCONTINSUREDWITHWOLVERINE").InnerText);
			}

			/* Run a loop on Violations 
				 *  Case 1 - Auto
				 * 1. Check if it is a chargeable accident. 
					Assumed to be accident if $1000 (chargeableAccidentAmount) or more was paid
					(after the deductible, if any).*/				 		

			foreach(XmlNode VioNode in MvrPointsNode.SelectNodes("violations/*"))
			{
				string LastVioDate="";
				if (Double.Parse(VioNode.SelectSingleNode("AMOUNTPAID").InnerText) >= amountPaid)
				{
					if (LobCode == "AUTOP")
					{
						if (AccidentPoints==-1)
						{	/*3. Check if accident can be forgiven. 
							The first at-fault accident will be forgiven depending on dates 
								*********** THIS CHECK IS APPLIED ONLY IN AUTOP ********/
							if ((DateTime.Parse(VioNode.SelectSingleNode("VIODATE").InnerText.ToString()) < DateTime.Parse("02/01/2003")) && Double.Parse(VioNode.SelectSingleNode("AMOUNTPAID").InnerText.ToString()) < 3000 && yearsContInsuredWithWolverine>=3)
							{
								/*	For accidents occurring before 01/01/2003 .. 
										a) total payments are less than $3,000 AND 
										b) the policy has been continuously in force over three years with Wolverine Mutual.
									*/ 	
								AccidentPoints = 0;
								LastVioDate = VioNode.SelectSingleNode("VIODATE").InnerText.ToString();
							}
							else if ((DateTime.Parse(VioNode.SelectSingleNode("VIODATE").InnerText.ToString()) >= DateTime.Parse("02/01/2003")) && Double.Parse(VioNode.SelectSingleNode("AMOUNTPAID").InnerText.ToString()) < 2000 && yearsContInsuredWithWolverine>=3)
							{
								/*For accidents occurring 01/01/2003 or later, 
									a) The first at-fault accident will be forgiven if total payments are less than $2,000 AND 
									b) The policy has been continuously in force over three years
									with Wolverine Mutual.*/

								AccidentPoints = 0;
								LastVioDate = VioNode.SelectSingleNode("VIODATE").InnerText.ToString();
							}
							else
								AccidentPoints = firstAccidentPoints;
						
						}		
						else if (AccidentPoints == 0)
						{
							TimeSpan TS = DateTime.Parse(LastVioDate)-DateTime.Parse(VioNode.SelectSingleNode("VIODATE").InnerText.ToString());
							if ((TS.Days/365)>=-3 && (TS.Days/365)<=3)
								AccidentPoints = 7;
							else
								AccidentPoints = firstAccidentPoints;
						}
						else
							AccidentPoints = AccidentPoints + 4;	

					}
					
						// ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
					else if (LobCode == "CYCL" || LobCode == "BOAT")
					{
						/* The surcharge period will be 
						 * 1. two (2) years from conviction date for minor violations; 
						 * 2. three (3) years for an accident.
						 * 3. Major Violations (as defined in auto) have a five (5) year surcharge.					  
						*/

						// compare the violation date with the permissible date as per the category (in this case 3 yrs since it is an accident)
						//if date is less than the permissible date then proceed else quit
                       	//add the points depending on category..    ACCIDENTS
						/* Three (3) years for an accident.*/
						if (DateTime.Parse(VioNode.SelectSingleNode("VIODATE").InnerText.ToString()) >= dtEffdate.AddYears(-3))
						{
							if (AccidentPoints==-1)
							{	/* Check if accident can be forgiven. */
								AccidentPoints = firstAccidentPoints;
							}
							else if (AccidentPoints == 0)
							{
								TimeSpan TS = DateTime.Parse(LastVioDate)-DateTime.Parse(VioNode.SelectSingleNode("VIODATE").InnerText.ToString());
								if ((TS.Days/365)>=-3 && (TS.Days/365)<=3)
									AccidentPoints = 7;
								else
									AccidentPoints = firstAccidentPoints;
							}
							else
								AccidentPoints = AccidentPoints + 4;	

						}
          
					}

					// ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
				}
				else
				{
					if (LobCode =="AUTOP")
					{
						/*5. In case of violation simply add the vioaltion points*/
						if (VioNode.SelectSingleNode("WOLVERINE_VIOLATIONS").InnerText.ToString() !="")							
						violationPoints = violationPoints +  double.Parse(VioNode.SelectSingleNode("WOLVERINE_VIOLATIONS").InnerText.ToString()); 
					} 
					else if (LobCode =="CYCL" || LobCode =="BOAT")
					{
						//check the violation type. categorize it as per minor violation/serious violation
						// compare the violation date with the permissible date as per the category (in this case 3 yrs since it is an accident)
						//if date is less than the permissible date then proceed else quit
						//<VIOLATIONID>1830</VIOLATIONID> SERIOUS OFFENCES(INCLUDING MOVING VIOLATIONS)
						if (VioNode.SelectSingleNode("VIOLATIONID").InnerText.ToString() == "1830")
						{
							// 1. Major/Serious Violations (as defined in auto) have a five (5) year surcharge.
							if (DateTime.Parse(VioNode.SelectSingleNode("VIODATE").InnerText.ToString()) >= dtEffdate.AddYears(-5))
							{	
								if (VioNode.SelectSingleNode("WOLVERINE_VIOLATIONS").InnerText.ToString() !="")							
								violationPoints = violationPoints +  double.Parse(VioNode.SelectSingleNode("WOLVERINE_VIOLATIONS").InnerText.ToString()); 
							}							
						}
						else if (DateTime.Parse(VioNode.SelectSingleNode("VIODATE").InnerText.ToString()) >= dtEffdate.AddYears(-2))
						{
							//  two (2) years from conviction date for minor violations; 
							if (VioNode.SelectSingleNode("WOLVERINE_VIOLATIONS").InnerText.ToString() !="")							
							violationPoints = violationPoints +  double.Parse(VioNode.SelectSingleNode("WOLVERINE_VIOLATIONS").InnerText.ToString()); 
						}	
					}
				}

				
				if(LobCode == "AUTOP")
				{
					/*7. The Violation points will be sum of all violation (last 2 years) 
					*  points entered through  MVR screen. All violations (even if its is named Accident) entered through MVR screen are Violations not accidents.
					* */
					if (VioNode.SelectSingleNode("MVRPOINTS").InnerText.ToString()!="" && double.Parse(VioNode.SelectSingleNode("MVRPOINTS").InnerText.ToString())>0)
					{
						int dateDiff = (dtEffdate.Year - DateTime.Parse(VioNode.SelectSingleNode("VIODATE").InnerText.ToString()).Year);
						if(dateDiff>=0 && dateDiff<=2)					
							mvrPoints = mvrPoints +  double.Parse(VioNode.SelectSingleNode("MVRPOINTS").InnerText.ToString()); 
					}
				}
				else
				{
					/*6. MVR points will be calculated by sum of MVR points in each violation node.*/					
					if (VioNode.SelectSingleNode("MVRPOINTS").InnerText.ToString()!="")
						mvrPoints = mvrPoints +  double.Parse(VioNode.SelectSingleNode("MVRPOINTS").InnerText.ToString()); 
				}


				



			}
			mvr = "<POINTS>" + "<MVR>" + mvrPoints + "</MVR>" + "<SUMOFVIOLATIONPOINTS>" + violationPoints + "</SUMOFVIOLATIONPOINTS>"  + "<SUMOFACCIDENTPOINTS>" + AccidentPoints + "</SUMOFACCIDENTPOINTS>" + "</POINTS>";
			return mvr;

		}

		#endregion



		
	}
}
