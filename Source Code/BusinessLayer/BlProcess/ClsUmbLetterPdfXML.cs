using System;
using System.Xml;
using System.Data;
using Cms.DataLayer;
using System.Collections;

namespace Cms.BusinessLayer.BlProcess
{
	/// <summary>
	/// <CreatedBy>Mohit Agarwal</CreatedBy>
	/// <Dated>22-Jun-2007</Dated>
	/// <Purpose>To Create XML for Umbrella Letter</Purpose>
	/// </summary>
	public class ClsUmbLetterPdfXML : ClsCommonPdfXML
	{
		#region Declarations
		private XmlElement DecPageRootElement;
		private Hashtable htpremium=new Hashtable(); 
		private DataWrapper gobjSqlHelper;
		private string stCode="";
		private string gStrLobCode="";
		//private string gInsAgn="Insured";
		#endregion

		#region Constructor
		public ClsUmbLetterPdfXML(string lstrClientId,string lstrPolicyId,string lstrVersionId)
		{
			gStrClientID=lstrClientId;
			gStrPolicyId=lstrPolicyId;
			gStrPolicyVersion=lstrVersionId;
			gStrLobCode="UMB";
			stCode="IN";			
		}
		#endregion

		public string getUmbLetterPdfXML()
		{
			DSTempDataSet = new DataSet();
			gobjSqlHelper = new DataWrapper(ConnStr,CommandType.Text);
			SetPDFVersionLobNode(gStrLobCode,System.DateTime.Now);
		
			AcordPDFXML = new XmlDocument();
			AcordPDFXML.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?> <" + RootElement + "/>");

			createRootElementForAllRootPDFs();
			//LoadRateXML();
			FillMonth();

			//creating Xml From Here
			CreateCustAgencyXML();

			return AcordPDFXML.OuterXml;
		}

		#region To Create Root Element For All Root PDFs
		private void createRootElementForAllRootPDFs()
		{
			DecPageRootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
			AcordPDFXML.SelectSingleNode(RootElement).AppendChild(DecPageRootElement);

			DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("LETTER"));
		}
		#endregion

		#region Creating Customer And Agency Xml 
		private void CreateCustAgencyXML()
		{
			string due_date = DateTime.Now.Month.ToString() + "/" + (DateTime.Now.Day + 5).ToString() + "/" + DateTime.Now.Year.ToString();
			DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetUmbLetterData " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion);
			
			#region Policy Agency Part
			XmlElement DecPagePolicyElement;
			DecPagePolicyElement = AcordPDFXML.CreateElement("POLICY");
			DecPageRootElement.AppendChild(DecPagePolicyElement);
			DecPagePolicyElement.SetAttribute(fieldType,fieldTypeSingle);
		
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<UMB_DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DateTime.Now.ToString("MM/dd/yyyy")) + "</UMB_DATE>";
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<UMB_NAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString()) + "</UMB_NAME>";
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<POL_NUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</POL_NUMBER>";
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<UMB_AGENCY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</UMB_AGENCY>";
			if(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value)
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<EXP_DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Convert.ToDateTime(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"]).ToString("MM/dd/yyyy")) + "</EXP_DATE>";
			if(DSTempDataSet.Tables[0].Rows[0]["DUE_DATE"] != System.DBNull.Value)
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<DUE_DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Convert.ToDateTime(DSTempDataSet.Tables[0].Rows[0]["DUE_DATE"]).ToString("MM/dd/yyyy")) + "</DUE_DATE>";
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<UMB_PHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["UNDERWRITER_PHONE"].ToString()) + "</UMB_PHONE>";
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<UMB_EXT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["UNDERWRITER_EXT"].ToString()) + "</UMB_EXT>";
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<UMB_EMAIL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["UNDERWRITER_EMAIL"].ToString()) + "</UMB_EMAIL>";
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<UNDERWRITER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["UNDERWRITER_NAME"].ToString()) + "</UNDERWRITER>";

			#endregion
		}
		#endregion

	}
}