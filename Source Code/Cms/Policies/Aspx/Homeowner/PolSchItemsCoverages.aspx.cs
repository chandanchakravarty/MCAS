/******************************************************************************************
<Author					: -   Pradeep Iyer
<Start Date				: -	  8 Nov , 2005
<End Date				: -	
<Description			: -  Scheduled Items / Coverages for Inland Marine (Policy)
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			:  
<Modified By			:   
<Purpose				:  
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
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Application.HomeOwners;
using Cms.Model.Application;
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using System.Xml;
using System.Text;

namespace Cms.Policies.aspx.HomeOwners
{
	/// <summary>
	/// Summary description for Coverages.
	/// </summary>
	public class PolSchItemsCoverages : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.DataGrid dgCoverages;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREC_VEH_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		//protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_LOB;
		string calledFrom = "";
		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldXml;
		DataSet dsCoverages = null;
		XmlDocument xmldoc=new XmlDocument() ;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolcyType;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidROW_COUNT;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.HtmlControls.HtmlTableRow trError;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		StringBuilder sbScript = new StringBuilder();
		StringBuilder sbDisableScript = new StringBuilder();
		private string strCustomerID, strAppId, strAppVersionId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFilledCovgXML;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;

		DataSet dsCovgRange = null;
		public string WebServiceURL;
		private void Page_Load(object sender, System.EventArgs e)
		{
			GetSessionValues();
			WebServiceURL = ClsCommon.WebServiceURL;
			if (!CanShow())
			{
				lblMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("109");
				lblMessage.Visible=true;
				return;
			}
			
			cltClientTop.PolicyID=int.Parse(strAppId);
			cltClientTop.CustomerID=int.Parse(strCustomerID);
			cltClientTop.PolicyVersionID=int.Parse(strAppVersionId);
			cltClientTop.ShowHeaderBand = "Policy";
			cltClientTop.Visible= true;


			trError.Visible=false;
			// if called from private passenger automobile, otherwise use if else
			//string strCalledFrom = "";
			#region setting screen id
			if ( Request.QueryString["CalledFrom"] != null )
			{
				calledFrom =  Request.QueryString["CalledFrom"].ToString();
				hidCalledFrom.Value = calledFrom;
				
				//strLobId= Request.QueryString["LOB_ID"].ToString();
			}

			switch (calledFrom.ToUpper()) 
			{
				case "HOME":
					base.ScreenId="236";
					break;
				case "RENTAL":
					base.ScreenId="161";
					break;	
				default:
					base.ScreenId="236";
					break;
			}
			#endregion



			//btnReset.Attributes.Add("onClick","javascript:return Reset();");

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			//btnReset.CmsButtonClass		=	CmsButtonType.Write;
			//btnReset.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnDelete.CmsButtonClass		=	CmsButtonType.Delete;
			btnDelete.PermissionString	=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************

			

			// Put user code to initialize the page here
			if ( !Page.IsPostBack)
			{

				hidCustomerID.Value	 =  GetCustomerID();
				hidPolicyID.Value = GetPolicyID();
				hidPolicyVersionID.Value = GetPolicyVersionID();

				//Get LOB ID on basis of Custmoer id, Application id, Application Version Id
				//hidAPP_LOB.Value = ClsVehicleInformation.GetApplicationLOBID(hidCustomerID.Value,hidAppID.Value,hidAppVersionID.Value).ToString();
				
				//this.hidREC_VEH_ID.Value = Request.QueryString["DWELLING_ID"].ToString();

				//btnReset.Attributes.Add("onClick","javascript:return Reset();");
				
				if (Request.QueryString["PageTitle"] != null)
				{
					lblTitle.Text = Request.QueryString["PageTitle"].ToString();
				}

				ViewState["CurrentPageIndex"] = 1;
				
				BindGrid(calledFrom);


			}
			SetWorkFlowControl();
			HttpCookie CovgID = new HttpCookie("CovgID","0");
		}

		private bool CanShow()
		{
			//Checking whether customer id exits in database or not
			if (strAppId == "")
			{
				return false;
			}

			return true;
		}
	
		private void BindGrid(string calledFrom)
		{
			ClsCoverages objHome = new ClsCoverages();
			
			int pageSize = 0;

			if ( System.Configuration.ConfigurationSettings.AppSettings["CoverageRows"] == null )
			{
				pageSize = 10;
			}
			else
			{
				pageSize = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["CoverageRows"]);
			}
		
			int currentPageIndex = Convert.ToInt32(ViewState["CurrentPageIndex"]);
			
			
			Cms.BusinessLayer.BlApplication.ClsSchItemsCovg objCovInformation = new Cms.BusinessLayer.BlApplication.ClsSchItemsCovg();
			
			//Get the relevant coverages
			switch(calledFrom)
			{	
				
			
				case "Rental":
				case "Home":
					
					dsCoverages=objCovInformation.GetPolicyInlandCoverages(Convert.ToInt32(hidCustomerID.Value),
						Convert.ToInt32(hidPolicyID.Value),
						Convert.ToInt32(hidPolicyVersionID.Value),
						"N"
						);

				
					break;
				default:
					
					dsCoverages=objCovInformation.GetPolicyInlandCoverages(Convert.ToInt32(hidCustomerID.Value),
						Convert.ToInt32(hidPolicyID.Value),
						Convert.ToInt32(hidPolicyVersionID.Value),
						"N"
						);
				
					break;

			}
			
			
			dgCoverages.DataSource = dsCoverages.Tables[0];
			dgCoverages.DataBind();


			this.hidROW_COUNT.Value = 	dgCoverages.Items.Count.ToString();
			DataTable dataTable		= dsCoverages.Tables[0];
			hidOldData.Value		=  ClsCommon.GetXMLEncoded(dataTable);
			dsCovgRange				=  dsCoverages;

			DataSet dsFilledCoverages = objCovInformation.GetInLandCoveragesFilledPolicy(Convert.ToInt32(hidCustomerID.Value),Convert.ToInt32(hidPolicyID.Value),Convert.ToInt32(hidPolicyVersionID.Value));
			hidFilledCovgXML.Value = dsFilledCoverages.GetXml();

			RegisterScript();


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
			this.dgCoverages.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgCoverages_ItemDataBound);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void GetSessionValues()
		{
			strAppId = base.GetPolicyID();
			strAppVersionId = base.GetPolicyVersionID();
			strCustomerID = base.GetCustomerID();
		}

		private void dgCoverages_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if ( e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item )
			{				
				string strCOV_ID = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"COV_ID"));
				e.Item.Attributes.Add("id","Row_" + strCOV_ID);   
				CheckBox chk = (CheckBox)(e.Item.FindControl("cbDelete"));
				if (chk != null)
					chk.Attributes.Add ("OnClick","MakeRowEditable('" + strCOV_ID + "',this);");

				HyperLink lnk = (HyperLink)(e.Item.Cells[1].Controls[0]);

				if (lnk != null)
				{
					lnk.Attributes.Add("ID","lbl_" + strCOV_ID);
					lnk.Attributes.Add("style","cursor:default");
				}
				
				//ITrack # 6140 Manoj Rathore 22 July 2009
				Label AMT = (Label)(e.Item.FindControl("lblAmount"));
				
				if (AMT != null)
				{	
					string amount = "";
					
					Cms.BusinessLayer.BlApplication.ClsSchItemsCovg objCovInformation = new Cms.BusinessLayer.BlApplication.ClsSchItemsCovg();
					amount = objCovInformation.GetInLandCoveragesAmount(Convert.ToInt32(hidCustomerID.Value),
						Convert.ToInt32(hidPolicyID.Value),
						Convert.ToInt32(hidPolicyVersionID.Value),
						"POLICY", strCOV_ID);
					 
					if(amount != "0" && amount!="")//Changed from 0.00 by Charles on 7-Oct-09 for Itrack 6488
					{
						decimal Decamount=Convert.ToDecimal(amount);
						//amount = String.Format("{0:C}",Decamount); //Commented by Charles on 7-Oct-09 for Itrack 6488
						AMT.Text = amount.Replace("$","");
					}
				}
				
				//Select the ranges applicable to this Coverage
				DataView dv  = dsCoverages.Tables[1].DefaultView;
				dv.RowFilter = "COV_ID = " + strCOV_ID + " AND LIMIT_DEDUC_TYPE = 'Deduct'";
				
				DropDownList DL = (DropDownList)(e.Item.FindControl("ddlRange"));
				if (DL != null)
				{	
					DL.ClearSelection();
					DL.Items.Clear();
					for (int i=0; i < dv.Count; i++)
					{
						ListItem LI = new ListItem();						
						LI.Text  = Convert.ToDouble(dv[i][3].ToString()).ToString("N").Split('.')[0];
						LI.Value = dv[i][1].ToString();
						DL.Items.Add(LI);
					}
					/*DL.DataSource = dv;
					DL.DataTextFormatString  = "{0:,#,###}";
					DL.DataTextField  = "LIMIT_DEDUC_AMOUNT";
					DL.DataValueField = "LIMIT_DEDUC_ID";
					DL.DataBind();
					DL.ClearSelection();
					DL.SelectedIndex = -1;*/
				}
			}
		}


		private void RegisterScript()
		{
		}
	
		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			//Loop througth all the records and pass the value			
			#region Item_list_loop
			//string strSelectedCovg = "";
			//string strSelectedDedu = "";
		    ArrayList arList=new ArrayList();


			
			foreach(DataGridItem dgi in dgCoverages.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					CheckBox chk = (CheckBox)dgi.FindControl("cbDelete");
										
					if (chk != null)
					{
						Cms.Model.Policy.HomeOwners.ClsSchItemsCovgInfo        
						objInfo	 = new Cms.Model.Policy.HomeOwners.ClsSchItemsCovgInfo();
						objInfo.CUSTOMER_ID		= int.Parse(GetCustomerID());
						objInfo.POLICY_ID			= int.Parse(GetPolicyID());
						objInfo.POLICY_VERSION_ID	= int.Parse(GetPolicyVersionID());
						
						string tmpCovgID;
						tmpCovgID = ((Label)(dgi.FindControl("lblCOV_ID"))).Text;
						objInfo.COVERAGE_CODE_ID=Convert.ToInt32(tmpCovgID.ToString());
						if (chk.Checked == true)
						{
							//							string tmpCovgID;
							//							string tmpDedAmt;
							//							tmpCovgID = ((Label)(dgi.FindControl("lblCOV_ID"))).Text;
							//							tmpDedAmt = ((DropDownList)(dgi.FindControl("ddlRange"))).SelectedValue;
							//
							//							if (strSelectedCovg == "" && strSelectedDedu == "")
							//							{
							//								strSelectedCovg = tmpCovgID;
							//								strSelectedDedu = tmpDedAmt;
							//							}
							//							else
							//							{
							//								strSelectedCovg = strSelectedCovg + "," + tmpCovgID;
							//								strSelectedDedu = strSelectedDedu + "," + tmpDedAmt;
							//							}
							
							objInfo.CREATED_BY		= int.Parse(GetUserId());
							objInfo.CREATED_DATETIME= DateTime.Now;
							string tmpDedAmt;
							tmpDedAmt = ((DropDownList)(dgi.FindControl("ddlRange"))).SelectedValue;
							objInfo.Action="I";
							objInfo.DEDUCTIBLE=Convert.ToDouble(tmpDedAmt.ToString());
							
						}
						else
						{
							objInfo.Action="D";

						}
						arList.Add(objInfo);
					}
				}
			}
			#endregion
			//Response.Write  ("strSelectedCovg " + strSelectedCovg + "<br>");
			//Response.Write  ("strSelectedDedu  " + strSelectedDedu  + "<br>");

			ClsSchItemsCovg objCoverages = new ClsSchItemsCovg();
			

			
			
			int retVal = objCoverages.SaveInlandMarineCovgNewPolicy(arList);
			
			if(retVal > 0)
			{
				if ( Convert.ToInt32(ViewState["TotalRecords"]) == 0 )
				{
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","29");
					
				}
				else
				{
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","31");
				
				}

				BindGrid(calledFrom);
				
				SetWorkFlowControl();
				//Opening the endorsement details page
				base.OpenEndorsementDetails();
				return;
			}

			if ( retVal == -2 )
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","332");
				return;
			}
		}
		
		private void SetWorkFlowControl()
		{		
			
			if(base.ScreenId	==	"236" || base.ScreenId == "161")
			{
				myWorkFlow.IsTop	=	true;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",hidCustomerID.Value);
				myWorkFlow.AddKeyValue("POLICY_ID",hidPolicyID.Value);
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",hidPolicyVersionID.Value);
				
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
				myWorkFlow.WorkflowModule="POL";
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}

		

	}
}
