/******************************************************************************************
<Author					: - >Amar Singh
<Start Date				: -	>May 03,2006
<End Date				: - >
<Description			: - >This file is being used to show/search Insured Vehicle Information
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
using Cms.CmsWeb;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb.WebControls;
using System.Reflection;
using System.Resources;


namespace Cms.Claims.Aspx
{
	/// <summary>
	/// This class is used for showing grid that search and display Parties
	/// </summary>
	public class PartyIndex : Cms.Claims.ClaimBase
	{
		
		#region "web form designer controls declaration"
		//protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.HtmlControls.HtmlTableRow tabCtlRow;
		protected System.Web.UI.WebControls.Label lblError;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNEW_RECORD;
		#endregion
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAddNew;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPROPERTY_DAMAGED_ID;
		protected Cms.CmsWeb.WebControls.ClaimTop cltClaimTop;		
		protected string strCustomerId,strPolicyID,strPolicyVersionID,strClaimID,strLOB_ID;
        ResourceManager objResourceMgr = null;

		#region Local form variables
		private string gStrClaimId="";
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			SetActivityStatus("");
			base.ScreenId="307";
			SetClaimTop();
            objResourceMgr = new ResourceManager("Cms.Claims.Aspx.PartyIndex", Assembly.GetExecutingAssembly());	
			// Put user code to initialize the page here

			
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
			if(Session["claimID"]!=null)
				gStrClaimId = Session["claimID"].ToString();

			if(Request["ADD_NEW"]!=null && Request["ADD_NEW"].ToString()!="")
				hidAddNew.Value = Request["ADD_NEW"].ToString();
			else
				hidAddNew.Value = "0";

			//
			if(Request["PROPERTY_DAMAGED_ID"]!=null && Request["PROPERTY_DAMAGED_ID"].ToString()!="")
				hidPROPERTY_DAMAGED_ID.Value = Request["PROPERTY_DAMAGED_ID"].ToString();
			else
				hidPROPERTY_DAMAGED_ID.Value = "0";
			
			
		    BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{
                int LangID = int.Parse(GetLanguageID());
				string sSELECTCLAUSE="", sFROMCLAUSE="";
				sSELECTCLAUSE+= " * ";
				if(hidPROPERTY_DAMAGED_ID.Value!="0")
                    sFROMCLAUSE = " ( SELECT CASE PARTY_TYPE_ID  WHEN 111 THEN ISNULL(ISNULL(M.TYPE_DESC, B.DETAIL_TYPE_DESCRIPTION),'') + ' ' + ISNULL(PARTY_TYPE_DESC,'')  ELSE ISNULL(M.TYPE_DESC, B.DETAIL_TYPE_DESCRIPTION) END AS PARTY_TYPE, '' AS RELATED_TO, A.NAME, A.REFERENCE,CONVERT(VARCHAR(10),A.CREATED_DATETIME,101) AS RECORDED_ON, A.PARTY_ID, B.DETAIL_TYPE_ID,A.IS_ACTIVE,CASE WHEN  LEN(ISNULL(A.OTHER_DETAILS,'')) >= 30 THEN SUBSTRING(ISNULL(A.OTHER_DETAILS,''),1,30) + '...' ELSE ISNULL(A.OTHER_DETAILS,'') END AS OTHER_DETAILS FROM CLM_PARTIES A LEFT JOIN CLM_TYPE_DETAIL B ON A.PARTY_TYPE_ID = B.DETAIL_TYPE_ID AND B.TYPE_ID = 2 LEFT OUTER JOIN CLM_TYPE_DETAIL_MULTILINGUAL M ON A.PARTY_TYPE_ID = M.DETAIL_TYPE_ID AND B.TYPE_ID = 2 AND LANG_ID=" + LangID + " WHERE A.CLAIM_ID = " + gStrClaimId + " and A.PROP_DAMAGED_ID=" + hidPROPERTY_DAMAGED_ID.Value.ToString() + ") Test ";
				else
                    sFROMCLAUSE = " ( SELECT  CASE PARTY_TYPE_ID  WHEN 111 THEN ISNULL(ISNULL(M.TYPE_DESC, B.DETAIL_TYPE_DESCRIPTION),'') + ' ' + ISNULL(PARTY_TYPE_DESC,'')  ELSE ISNULL(M.TYPE_DESC, B.DETAIL_TYPE_DESCRIPTION) END AS PARTY_TYPE, '' AS RELATED_TO, A.NAME, A.REFERENCE,CONVERT(VARCHAR(10),A.CREATED_DATETIME,101) AS RECORDED_ON, A.PARTY_ID, B.DETAIL_TYPE_ID,A.IS_ACTIVE,CASE WHEN  LEN(ISNULL(A.OTHER_DETAILS,'')) >= 30 THEN SUBSTRING(ISNULL(A.OTHER_DETAILS,''),1,30) + '...' ELSE ISNULL(A.OTHER_DETAILS,'') END AS OTHER_DETAILS FROM CLM_PARTIES A LEFT JOIN CLM_TYPE_DETAIL B ON A.PARTY_TYPE_ID = B.DETAIL_TYPE_ID AND B.TYPE_ID = 2 LEFT OUTER JOIN CLM_TYPE_DETAIL_MULTILINGUAL M ON A.PARTY_TYPE_ID = M.DETAIL_TYPE_ID AND B.TYPE_ID = 2 AND LANG_ID=" + LangID + " WHERE A.CLAIM_ID = " + gStrClaimId + " ) Test ";
					

			
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = sSELECTCLAUSE;
				objWebGrid.FromClause = sFROMCLAUSE ; 
				//objWebGrid.SearchColumnHeadings = "Name^Party Type^Related To^Recorded On";
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");// "Name^Party Type";
				//objWebGrid.SearchColumnNames = "NAME^DETAIL_TYPE_ID^RELATED_TO^RECORDED_ON";
				objWebGrid.SearchColumnNames = "NAME^DETAIL_TYPE_ID";
				//objWebGrid.SearchColumnType = "T^T^T^T";
				objWebGrid.SearchColumnType = "T^T";
				objWebGrid.DropDownColumns  = "^PARTYTYPE^^^";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Party Type^Name^Details";
				objWebGrid.DisplayColumnNumbers = "1^3^9";
				objWebGrid.DisplayColumnNames = "PARTY_TYPE^NAME^OTHER_DETAILS";
				objWebGrid.DisplayTextLength = "25^25^30";
				objWebGrid.DisplayColumnPercent = "25^25^30";
				objWebGrid.PrimaryColumns = "6";
				objWebGrid.PrimaryColumnsName = "PARTY_ID";
				objWebGrid.ColumnTypes = "B^B^B";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Parties Details";
				objWebGrid.OrderByClause	="NAME Asc";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7^9";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";											
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse (GetCacheSize());
				objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim()+ "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";				 
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel"); //"Include Inactive";
				objWebGrid.FilterColumnName = "IS_ACTIVE";
				objWebGrid.FilterValue = "Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.QueryStringColumns = "PARTY_ID";
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);

                if(hidPROPERTY_DAMAGED_ID.Value!="0")
					TabCtl.TabURLs = "AddPartyDetails.aspx?CLAIM_ID=" + gStrClaimId+"&PROPERTY_DAMAGED_ID=" + hidPROPERTY_DAMAGED_ID.Value.ToString()+"&"; 
				else
					TabCtl.TabURLs = "AddPartyDetails.aspx?CLAIM_ID=" + gStrClaimId+"&";
                TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
                TabCtl.TabLength = 150;
	
			}
 
			catch (Exception ex)
			{throw (ex);}
			#endregion

		}

		private void SetClaimTop()
		{
			
			strCustomerId = GetCustomerID();
			strPolicyID = GetPolicyID();
			strPolicyVersionID = GetPolicyVersionID();
			strClaimID = GetClaimID();
			strLOB_ID = GetLOBID();

			if(strCustomerId!=null && strCustomerId!="")
			{
				cltClaimTop.CustomerID = int.Parse(strCustomerId);
			}			

			if(strPolicyID!=null && strPolicyID!="")
			{
				cltClaimTop.PolicyID = int.Parse(strPolicyID);
			}

			if(strPolicyVersionID!=null && strPolicyVersionID!="")
			{
				cltClaimTop.PolicyVersionID = int.Parse(strPolicyVersionID);
			}
			if(strClaimID!=null && strClaimID!="")
			{
				cltClaimTop.ClaimID = int.Parse(strClaimID);
			}
			if(strLOB_ID!=null && strLOB_ID!="")
			{
				cltClaimTop.LobID = int.Parse(strLOB_ID);
			}
        
			cltClaimTop.ShowHeaderBand ="Claim";

			cltClaimTop.Visible = true;        
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
	}
}
