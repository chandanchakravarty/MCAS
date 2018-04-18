/******************************************************************************************
	<Author					:Nidhi - >
	<Start Date				:Dec 27,2005 - >
	<End Date				:Dec 27,2005 - >
	<Description			: This screen displays the Re-insurance contract information - >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - >
	<Modified By			: - >
	<Purpose				: - >
*******************************************************************************************/


using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb;
using System.Reflection;
using System.Resources;
using Cms.BusinessLayer.BlCommon;

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for ReinsuranceInfoGrid.
	/// </summary>
	public class ReinsuranceInfoGrid  : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.HtmlControls.HtmlForm Form2;
		protected System.Web.UI.HtmlControls.HtmlForm Form3;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlForm Form4;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidlocQueryStr;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        ResourceManager objResourceMgr = null;
		protected string strContractId = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "262";
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.ReinsuranceInfoGrid", Assembly.GetExecutingAssembly());

            string lang_id = GetLanguageID();
            if(Request.QueryString["CONTRACT_ID"] != null && Request.QueryString["CONTRACT_ID"] != "")
			{
				strContractId	=	Request.QueryString["CONTRACT_ID"];
				hidlocQueryStr.Value = strContractId;
			}
			#region GETTING BASE COLOR FOR ROW SELECTION
			string colorScheme=GetColorScheme();
			string colors="";

			switch (int.Parse(colorScheme))
			{
				case 1:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR1").ToString();     
					break;
				case 2:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR2").ToString();     
					break;
				case 3:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR3").ToString();     
					break;
				case 4:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR4").ToString();     
					break;
			}
			if(colors!="")
			{
				string [] baseColor=colors.Split(new char []{','});  
				if(baseColor.Length>0)
					colors= "#" + baseColor[0];
			}
			#endregion   
		
			#region loading web grid control
			Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{

                int BaseCurrency = 1;
                if (GetSYSBaseCurrency() == enumCurrencyId.BR)
                    BaseCurrency = 2;
                else
                    BaseCurrency = 1;

				BaseDataGrid objWebGrid;
				objWebGrid = (BaseDataGrid)c1;

				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

				//objWebGrid.SelectClause = "CONTRACT_ID,CONTRACT_NUMBER,MNT_CONTRACT_NAME.CONTRACT_NAME AS CONTRACT_NAME, MNT_REINSURANCE_CONTRACT_TYPE.CONTRACT_TYPE_DESC as ContractType,MNT_LOB_MASTER.LOB_DESC as LOB, ISNULL(Convert(varchar,EFFECTIVE_DATE),'') AS EffectiveDate,ISNULL(Convert(varchar,EXPIRATION_DATE),'') AS ExpirationDate,MNT_REINSURANCE_CONTRACT.IS_ACTIVE";
				//objWebGrid.SelectClause = "A.CONTRACT_ID CONTRACT_ID,C.CONTRACT_TYPE_DESC as CONTRACT_TYPE,L.Lookup_value_Desc RISK_EXPOSURE,A.CONTRACT_NUMBER CONTRACT_NUMBER,B.LOB_DESC as LOB,D.STATE_CODE STATE,A.CALCULATION_BASE PREMIUM_BASIS,ISNULL(Convert(varchar,A.EFFECTIVE_DATE,101),'') AS EFFECTIVE_DATE,ISNULL(Convert(varchar,A.EXPIRATION_DATE,101),'') AS EXPIRATION_DATE, A.COMMISSION COMMISSION,ISNULL(Convert(varchar,A.TERMINATION_DATE,101),'') TERMINATION_DATE,A.IS_ACTIVE IS_ACTIVE,D.STATE_NAME STATE_NAME,L1.Lookup_value_Desc CALCULATION_BASE,B.LOB_ID AS LOB_ID,A.EFFECTIVE_DATE as EFF_DATE";
                objWebGrid.SelectClause = "A.SEQUENCE_NUMBER,A.CONTRACT_ID CONTRACT_ID, "+
                                           " ISNULL(L.CONTRACT_TYPE_DESC, C.CONTRACT_TYPE_DESC) as CONTRACT_TYPE,   " +
                                           " A.CONTRACT_NUMBER CONTRACT_NUMBER,ISNULL(LMV.LOB_DESC,B.LOB_DESC) as LOB,A.CALCULATION_BASE PREMIUM_BASIS,ISNULL(Convert(varchar,A.EFFECTIVE_DATE,case when " + ClsCommon.BL_LANG_ID + "=3 then 103 else 101 end),'') AS EFFECTIVE_DATE,ISNULL(Convert(varchar,A.EXPIRATION_DATE,case when " + ClsCommon.BL_LANG_ID + "=3 then 103 else 101 end),'') AS EXPIRATION_DATE,"+
                                          " CASE WHEN " + BaseCurrency + "=2 THEN REPLACE(CAST( COMMISSION AS VARCHAR(50)),'.',',') ELSE CAST( COMMISSION AS VARCHAR(50)) END AS COMMISSION , " +                                       
                                          " A.CONTACT_YEAR,A.IS_ACTIVE IS_ACTIVE,ISNULL(MLV.Lookup_value_Desc,L1.Lookup_value_Desc) CALCULATION_BASE,B.LOB_ID AS LOB_ID,A.EFFECTIVE_DATE as EFF_DATE, CAST(ISNULL(A.SEQUENCE_NUMBER,'0') AS INT) AS SEQUENCE_NUMBER_1";//Added by Sibin on 12 Ded 08-Reference to mail by Pravesh sir on 12 Dec 08
				objWebGrid.FromClause	="MNT_REINSURANCE_CONTRACT A inner join  MNT_REIN_CONTRACT_LOB lb ON lb.CONTRACT_ID=A.CONTRACT_ID ";
				objWebGrid.FromClause += " INNER JOIN MNT_LOB_MASTER B ON B.LOB_ID = lb.CONTRACT_LOB ";
				//objWebGrid.FromClause += "INNER JOIN MNT_REIN_CONTRACT_STATE st ON st.CONTRACT_ID=A.CONTRACT_ID ";
				//objWebGrid.FromClause += "INNER JOIN MNT_COUNTRY_STATE_LIST D On D.State_ID=st.STATE_ID ";
                objWebGrid.FromClause += " LEFT OUTER JOIN MNT_REINSURANCE_CONTRACT_TYPE C ON C.CONTRACTTYPEID =A.CONTRACT_TYPE ";
                objWebGrid.FromClause += " LEFT OUTER JOIN MNT_REINSURANCE_CONTRACT_TYPE_MULTILINGUAL L ON C.CONTRACTTYPEID=L.CONTRACTTYPEID AND L.LANG_ID=" + lang_id + "";
               
				//objWebGrid.FromClause += "INNER JOIN MNT_REIN_CONTRACT_RISKEXPOSURE RSK on RSK.CONTRACT_ID=A.CONTRACT_ID ";
				//objWebGrid.FromClause += "INNER JOIN MNT_LOOKUP_VALUES L ON LOOKUP_UNIQUE_ID=RSK.RISK_EXPOSURE and L.LOOKUP_ID=1325 ";
				objWebGrid.FromClause += " INNER JOIN MNT_LOOKUP_VALUES L1 ON L1.LOOKUP_UNIQUE_ID=A.CALCULATION_BASE";
                objWebGrid.FromClause += " LEFT OUTER JOIN MNT_LOOKUP_VALUES_MULTILINGUAL MLV  ON MLV.LOOKUP_UNIQUE_ID=A.CALCULATION_BASE AND MLV.LANG_ID=" + lang_id + "";
                objWebGrid.FromClause += " LEFT OUTER JOIN MNT_LOB_MASTER_MULTILINGUAL LMV on LMV.LOB_ID=B.LOB_ID AND LMV.LANG_ID=" + lang_id + "";

				//mnt_lookup_values L   on  T.REIN_DESCRIPTION = L.LOOKUP_VALUE_CODE and L.LOOKUP_ID  =1320
				//LookupDesc";
				//cmbRISK_EXPOSURE.DataValueField	= "LookupId";
				//objWebGrid.FromClause = "MNT_REINSURANCE_CONTRACT A INNER JOIN MNT_REIN_LOB lb ON lb.CONTRACT_ID=A.CONTRACT_ID ON  MNT_LOB_MASTER B ON B.LOB_ID = A.CONTRACT_LOB ";
				//objWebGrid.FromClause += " INNER JOIN MNT_COUNTRY_STATE_LIST D On D.State_ID=A.STATE_ID ";  
				//objWebGrid.FromClause +=  " INNER JOIN MNT_REINSURANCE_CONTRACT_TYPE C ON C.CONTRACTTYPEID =A.CONTRACT_TYPE ";
				//objWebGrid.FromClause = "MNT_REINSURANCE_CONTRACT INNER JOIN MNT_REINSURANCE_CONTRACT_TYPE ON MNT_REINSURANCE_CONTRACT_TYPE.CONTRACTTYPEID =MNT_REINSURANCE_CONTRACT.CONTRACT_TYPE  INNER JOIN MNT_CONTRACT_NAME ON MNT_CONTRACT_NAME.CONTRACT_NAME_ID = MNT_REINSURANCE_CONTRACT.CONTRACT_NAME_ID INNER JOIN MNT_LOB_MASTER ON MNT_LOB_MASTER.LOB_ID = MNT_REINSURANCE_CONTRACT.CONTRACT_LOB ";
				//objWebGrid.WhereClause = "";

//				objWebGrid.SearchColumnHeadings = "Contract Type^Contract #^Premium Basis^Effective Date^Expiration Date^Termination Date^Commission^Risk Exposure^Line of Business";
//				objWebGrid.SearchColumnNames = "C.CONTRACT_TYPE_DESC^CONTRACT_NUMBER^L1.Lookup_value_Desc^ISNULL(Convert(varchar,A.EFFECTIVE_DATE,101),'')^ISNULL(Convert(varchar,A.EXPIRATION_DATE,101),'')^ISNULL(Convert(varchar,A.TERMINATION_DATE,101),'')^COMMISSION^L.Lookup_value_Desc^B.LOB_ID";
//				objWebGrid.SearchColumnType = "T^T^T^D^D^D^T^T^L";
//				objWebGrid.DropDownColumns ="^^^^^^^^LOB";
//				//objWebGrid.OrderByClause = "A.CONTRACT_NUMBER,L.Lookup_value_Desc,D.STATE_NAME,L1.Lookup_value_Desc ,EFFECTIVE_DATE desc";
////				objWebGrid.OrderByClause = "A.CONTRACT_NUMBER";
//				objWebGrid.OrderByClause = "EFF_DATE DESC";

				//Added by Sibin on 12 Ded 08-Reference to mail by Pravesh sir on 12 Dec 08
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Sequence #^Contract Type^Contract #^Premium Basis^Effective Date^Expiration Date^Contract Year^Commission^Risk Exposure^Product";
				objWebGrid.SearchColumnNames = "A.SEQUENCE_NUMBER^C.CONTRACT_TYPE_DESC^CONTRACT_NUMBER^L1.Lookup_value_Desc^ISNULL(Convert(varchar,A.EFFECTIVE_DATE,101),'')^ISNULL(Convert(varchar,A.EXPIRATION_DATE,101),'')^A.CONTACT_YEAR^COMMISSION^B.LOB_ID";
				objWebGrid.SearchColumnType = "T^T^T^T^D^D^T^T^L";
				objWebGrid.DropDownColumns ="^^^^^^^^LOB";
				objWebGrid.OrderByClause = "A.SEQUENCE_NUMBER ASC,EFF_DATE DESC";
				//Added till here

				objWebGrid.DisplayColumnNumbers = "6^7^5^2^3^8";
				//objWebGrid.DisplayColumnNames = "CONTRACT_TYPE^CONTRACT_NUMBER^RISK_EXPOSURE^LOB^STATE_NAME^CALCULATION_BASE^EFFECTIVE_DATE^EXPIRATION_DATE^COMMISSION";
				//objWebGrid.DisplayColumnHeadings = "Contract Type^Contract #^Risk Exposure^Line of Business^State^Premium Basis^Effective Date^Expiration Date^Commission";
//				objWebGrid.DisplayColumnNames		= "CONTRACT_TYPE^CONTRACT_NUMBER^CALCULATION_BASE^EFFECTIVE_DATE^EXPIRATION_DATE^TERMINATION_DATE^COMMISSION^RISK_EXPOSURE^LOB";
//				objWebGrid.DisplayColumnHeadings    = "Contract Type^Contract #^Premium Basis^Effective Date^Expiration Date^Termination Date^Commission^Risk Exposure^Line of Business";
//				objWebGrid.DisplayTextLength = "100^100^100^100^100^100^50^150^100";
//				objWebGrid.DisplayColumnPercent = "15^15^15^15^15^15^5^10^10";  
//				objWebGrid.PrimaryColumns = "1";
				//Added by Sibin on 12 Ded 08-Reference to mail by Pravesh sir on 12 Dec 08
				objWebGrid.DisplayColumnNames		= "SEQUENCE_NUMBER^CONTRACT_TYPE^CONTRACT_NUMBER^CALCULATION_BASE^EFFECTIVE_DATE^EXPIRATION_DATE^CONTACT_YEAR^COMMISSION^LOB";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Sequence #^Contract Type^Contract #^Premium Basis^Effective Date^Expiration Date^Contract Year^Commission^Risk Exposure^Product";
				objWebGrid.DisplayTextLength = "50^100^50^100^100^100^50^50^100";
				objWebGrid.DisplayColumnPercent = "10^15^10^15^15^15^10^5^20";  
				objWebGrid.PrimaryColumns = "1";
				//Added till here
				objWebGrid.PrimaryColumnsName = "A.CONTRACT_ID";
                objWebGrid.CellHorizontalAlign = "7";

				//objWebGrid.ColumnTypes = "B^B^B^B^B^B^B^B^B";
				objWebGrid.ColumnTypes = "N^B^B^B^B^B^B^B^B";//Added by Sibin on 12 Ded 08 -Reference to mail by Pravesh sir on 12 Dec 08
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^6^7^8^9^11";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";objWebGrid.ExtraButtons = "";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"2^Add~Copy^0~1^addRecord~copyRecord";
				objWebGrid.PageSize =int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Reinsurance Contracts Information" ;
				objWebGrid.SelectClass = colors ;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
				objWebGrid.FilterColumnName="A.IS_ACTIVE";
				objWebGrid.FilterValue="Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch="Y";
				objWebGrid.QueryStringColumns = "CONTRACT_ID";
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);

				
				//TabCtl.TabURLs = "AddReinsuranceInfo.aspx?";
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			# endregion 
            TabCtl.TabURLs = "AddReinsuranceInfo.aspx?&";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
		}

		 
	
 
	#region Web Form Designer generated code
	override protected void OnInit(EventArgs e)
{
	//
	// CODEGEN: This call is required by the ASP.NET Web Form Designer.
	//
	InitializeComponent();
	base.OnInit(e);
}
		
	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
{    
	this.Load += new System.EventHandler(this.Page_Load);

}
	#endregion
 
}}