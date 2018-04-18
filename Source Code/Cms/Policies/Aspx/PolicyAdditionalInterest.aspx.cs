/******************************************************************************************
<Author				: -		Vijay Arora
<Start Date			: -		10-11-2005
<End Date			: -	
<Description		: - 	Policy Add/Edit/Delete Additional Interest Page.
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		:		
<Modified By		:
<Purpose			: 
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
using Cms.ExceptionPublisher.ExceptionManagement;
using System.Xml;
using Cms.Model.Maintenance;
using Cms.Model.Application;
using Cms.Model;
using Cms.Model.Policy;

namespace Cms.Policies.Aspx
{
	/// <summary>
	/// Summary description for AdditionalInterest.
	/// </summary>
	public class PolicyAddInterest : Cms.Policies.policiesbase
	{
		#region WEBFORM CONTROLS AND OTHER DECLEARATION
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.DropDownList cmbHOLDER_ID;
		protected System.Web.UI.WebControls.Label capHOLDER_ADD1;
		protected System.Web.UI.WebControls.Label capHOLDER_ADD2;
		protected System.Web.UI.WebControls.Label capHOLDER_CITY;
		protected System.Web.UI.WebControls.Label capHOLDER_COUNTRY;
		protected System.Web.UI.WebControls.Label capHOLDER_STATE;
		protected System.Web.UI.WebControls.Label capHOLDER_ZIP;
		protected System.Web.UI.WebControls.Label capMEMO;
		protected System.Web.UI.WebControls.TextBox txtMEMO;
		protected System.Web.UI.WebControls.Label capNATURE_OF_INTEREST;
		protected System.Web.UI.WebControls.Label capLOAN_REF_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtLOAN_REF_NUMBER;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHOLDER_ZIP;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelect;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOOKUP;
		ClsGeneralHolderInterest  objGeneralHolderInterest ;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		protected System.Web.UI.WebControls.Label capBILL_MORTAGAGEE;
		protected System.Web.UI.WebControls.DropDownList cmbBILL_MORTAGAGEE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidBILL_MORTAGAGEE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		
		//START:*********** Local variables to store valid control values  *************
		#region 

	
		int			intLoggedInUserID;
		#endregion
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtHOLDER_ADD2;
		protected System.Web.UI.WebControls.TextBox txtHOLDER_ZIP;
		protected System.Web.UI.WebControls.TextBox txtHOLDER_ADD1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidHolderXML;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMode;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVEHICLE_ID;
		 
		protected System.Web.UI.WebControls.DropDownList cmbHOLDER_STATE;
		protected System.Web.UI.WebControls.TextBox txtHOLDER_CITY;
		protected System.Web.UI.WebControls.DropDownList cmbHOLDER_COUNTRY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revHOLDER_ZIP;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHOLDER_CITY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHOLDER_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHOLDER_ADD1;
		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHOLDER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDWELLING_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTRAILER_ID;
		protected System.Web.UI.WebControls.CustomValidator csvMEMO;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidENGINE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidBOAT_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected System.Web.UI.WebControls.Label lblHOLDER_ID;
		protected System.Web.UI.WebControls.Label capHOLDER_ID;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnHOLDER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidHolderID;
		protected System.Web.UI.WebControls.TextBox txtHOLDER_ID;
		protected System.Web.UI.WebControls.DropDownList cmbHOLDER_AVAIL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHOLDER_AVAIL;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCertificate_Required;
		//protected System.Web.UI.HtmlControls.HtmlGenericControl spnCerificateRequired;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidHOLDER_NAME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidADD_INT_ID;		
		protected System.Web.UI.WebControls.Image imgZipLookup;
		protected System.Web.UI.WebControls.HyperLink hlkZipLookup;
		System.Resources.ResourceManager objResourceMgr;		
		int iPCUSTOMER_ID,iPBOAT_ID;
		#endregion
		
		#region Decalaration of form's public and private variables
		int iCUSTOMER_ID;
		int	iPOLICY_ID;	
		int iPOLICY_VERSION_ID;
		int	iAPP_ID;	
		int iAPP_VERSION_ID;
		int iVEHICLE_ID;
		int iRISK_ID;
		public int iHOLDER_ID;
		int iDWELLING_ID;
		int iBOAT_ID;
		int iTRAILER_ID,iENGINE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbNATURE_OF_INTEREST;
        protected System.Web.UI.WebControls.DropDownList cmbADD_INT_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNATURE_OF_INTEREST;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		public string pageFrom="";
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_VERSION_ID;
		protected System.Web.UI.WebControls.Label capRANK;
		protected System.Web.UI.WebControls.TextBox txtRANK;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRANK;
		protected System.Web.UI.WebControls.RegularExpressionValidator revRANK;
		protected System.Web.UI.WebControls.RangeValidator rngRANK;
		string strCalledFrom;
		#endregion
	
		#region Filling the page details
		public void FillAdditionalInterestDetails()
		{
			DataSet dtSet=new DataSet();
			DataTable dtHolder;
			DataRow rdHolderDetails;
			
			ClsAdditionalInterest objAddInterest = GetBlObject();
			
			int intADD_INT_ID = Convert.ToInt32(this.hidADD_INT_ID.Value);

            dtSet = objAddInterest.FillPolicyAdditionalInterestDetails(iCUSTOMER_ID,iPOLICY_ID,iPOLICY_VERSION_ID,iRISK_ID,intADD_INT_ID);
	
			if(dtSet.Tables[0].Rows.Count>0)
			{
				hidOldData.Value=dtSet.GetXml();
				dtHolder=dtSet.Tables[0];
				//additional interest information found in database
				//hnce populating the controls with data from database
				

				rdHolderDetails = dtHolder.Rows[0];

				txtMEMO.Text				=	rdHolderDetails[0].ToString();
				//	txtNATURE_OF_INTEREST.Text	=	rdHolderDetails[1].ToString();
				cmbNATURE_OF_INTEREST.SelectedValue=rdHolderDetails[1].ToString();
				txtRANK.Text				=	rdHolderDetails["RANK"].ToString();
				txtLOAN_REF_NUMBER.Text		=	rdHolderDetails[3].ToString();
				
				
				if (rdHolderDetails["IS_ACTIVE"] == null)
				{
					hidIS_ACTIVE.Value = "Y";
				}
				else
				{
					hidIS_ACTIVE.Value = rdHolderDetails["IS_ACTIVE"].ToString();
				}
				
				txtHOLDER_ID.Text				=	rdHolderDetails["HOLDER_NAME"].ToString();
				hidHolderID.Value				=	rdHolderDetails["HOLDER_ID"].ToString();
					
	
				txtHOLDER_ADD1.Text				=	rdHolderDetails["HOLDER_ADD1"].ToString();
				txtHOLDER_ADD2.Text				=	rdHolderDetails["HOLDER_ADD2"].ToString();
				txtHOLDER_CITY.Text				=	rdHolderDetails["HOLDER_CITY"].ToString();
				cmbHOLDER_COUNTRY.SelectedValue	=	rdHolderDetails["HOLDER_COUNTRY"].ToString();
				
				string state = rdHolderDetails["HOLDER_STATE"].ToString();	
				ListItem item;

				item = cmbHOLDER_STATE.Items.FindByValue(state);
				
				if ( item != null )
				{
					cmbHOLDER_STATE.SelectedIndex =	cmbHOLDER_STATE.Items.IndexOf(item);	
				}
				
				txtHOLDER_ZIP.Text				=	rdHolderDetails["HOLDER_ZIP"].ToString();
				if((hidLOB_ID.Value==((int)enumLOB.HOME).ToString() || hidLOB_ID.Value==((int)enumLOB.REDW).ToString()) && rdHolderDetails.Table.Columns.Contains("BILL_MORTAGAGEE") && rdHolderDetails["BILL_MORTAGAGEE"]!=null && rdHolderDetails["BILL_MORTAGAGEE"].ToString()!="" && rdHolderDetails["BILL_MORTAGAGEE"].ToString()!="0" && rdHolderDetails["BILL_MORTAGAGEE"].ToString()!="-1")
				{
					cmbBILL_MORTAGAGEE.SelectedValue = rdHolderDetails["BILL_MORTAGAGEE"].ToString();
					cmbBILL_MORTAGAGEE.Visible	= capBILL_MORTAGAGEE.Visible=true;
				}
			}

		}
				
		#endregion

		#region Page Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			this.cmbHOLDER_COUNTRY.SelectedIndex = int.Parse(aCountry);
			#region Setting ScreenId
			// Check from where the screen is called.
			if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"] !="")
			{
				strCalledFrom=Request.QueryString["CalledFrom"].ToString();				
			}
		
			if (Request.QueryString["PageFrom"] != null && Request.QueryString["PageFrom"] !="")
			{
				pageFrom=Request.QueryString["PageFrom"].ToString();
				
			}
			//Setting screen Id.	
			switch (strCalledFrom.ToUpper()) 
			{	case "Home":
				case "HOME":
					if(pageFrom.ToUpper() == "HREC")
					{base.ScreenId="243_1_0";}
					else
					{base.ScreenId="239_2_0";}
					break;
				case "RENTAL":
					base.ScreenId="259_2_0";
					break;
				case "MOT":
					base.ScreenId="231_3_0";
					break;
				case "PPA":
					//base.ScreenId="44_3_0";
					base.ScreenId="227_3_0";
					break;
				case "GEN":
					base.ScreenId="283_3_0";
					break;
				case "WAT":
				switch (pageFrom)
				{
					case "WWAT":
						base.ScreenId="246_4_0"; 
						break;
					case "HWAT":
						//base.ScreenId="148_2_0"; 
						//Added for Additional Interest Watercraft
						base.ScreenId="251_4_0";

						break;
					case "RWAT":
						base.ScreenId="166_2_0"; 
						break;
				
				}
					break;
				case "WEN":				
				switch (pageFrom)
				{
					case "WWEN":
						base.ScreenId = "74_2_0";
						break;
					case "HWEN":
						base.ScreenId="150_2_0";
						break;
					case "RWEN":
						base.ScreenId="168_2_0";
						break;
				}
					break;
				case "WTR":
				
				switch (pageFrom)	
				{
					case "WWTR":
						base.ScreenId="248_1_0";
						break;
					case "HWTR":
						//Added Add.Interest Trailer
						//base.ScreenId="151_2_0"; 
						base.ScreenId="253_1_0";
						break;
					case "RWTR":
						base.ScreenId="169_2_0"; 
						break;
				}
					break;
				default:
					base.ScreenId="227_3_0";
					break;
			}
			#endregion

			//spnCerificateRequired.Visible=false;
			//Retreiving the user id
			intLoggedInUserID		= int.Parse(base.GetUserId());
			
			btnReset.Attributes.Add("onclick","return ResetScreen();");

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnSave.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString				=	gstrSecurityXML;	
			
			btnReset.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write; 
			btnReset.PermissionString	=	gstrSecurityXML;	

			btnActivateDeactivate.CmsButtonClass	= Cms.CmsWeb.Controls.CmsButtonType.Write;
			
			btnActivateDeactivate.PermissionString	= gstrSecurityXML;

			btnDelete.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Delete;
			btnDelete.PermissionString		=	gstrSecurityXML;
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.PolicyAdditionalInterest" ,System.Reflection.Assembly.GetExecutingAssembly());

			iCUSTOMER_ID		=	Convert.ToInt32(Request.QueryString["CUSTOMER_ID"].ToString());
			iPOLICY_ID			=	Convert.ToInt32(GetPolicyID());	
			iPOLICY_VERSION_ID	=	Convert.ToInt32(GetPolicyVersionID());

			if (Request.QueryString["VEHICLE_ID"] != null && Request.QueryString["VEHICLE_ID"] != "")
			{
				iVEHICLE_ID			=	Convert.ToInt32(Request.QueryString["VEHICLE_ID"].ToString());
			}            
			else
			{
				iVEHICLE_ID			=	0;
			}

			if(Request.QueryString["HOLDER_ID"] != null &&  Request.QueryString["HOLDER_ID"].ToString() != "" )
			{
				iHOLDER_ID		=	Convert.ToInt32(Request.QueryString["HOLDER_ID"].ToString());
				hidHolderID.Value=Request.QueryString["HOLDER_ID"].ToString();
			}
			else
			{
				iHOLDER_ID		=	0;
			}
			

			if( Request.QueryString["DWELLING_ID"] != null && Request.QueryString["DWELLING_ID"] != "")
			{
				iDWELLING_ID		=	Convert.ToInt32(Request.QueryString["DWELLING_ID"].ToString());
			}
			else
			{
				iDWELLING_ID		=	0;
			}
				

			if (Request.QueryString["BOAT_ID"] != null &&  Request.QueryString["BOAT_ID"].ToString() != "" )
			{
				iBOAT_ID            =   Convert.ToInt32(Request.QueryString["BOAT_ID"].ToString());
			}
			else
			{
				iBOAT_ID			=	0;
			}

			if (Request.QueryString["TRAILER_ID"] != null &&  Request.QueryString["TRAILER_ID"].ToString() != "" )
			{
				iTRAILER_ID         =   Convert.ToInt32(Request.QueryString["TRAILER_ID"].ToString());
			}
			else
			{
				iTRAILER_ID         =	0;
			}

			if (Request.QueryString["ENGINE_ID"] != null &&  Request.QueryString["ENGINE_ID"].ToString() != "" )
			{
				iENGINE_ID         =   Convert.ToInt32(Request.QueryString["ENGINE_ID"].ToString());
			}
			else
			{
				iENGINE_ID         =	0;
			}

			if (Request.QueryString["RISK_ID"] != null && Request.QueryString["RISK_ID"] != "")
			{
				iRISK_ID			=	Convert.ToInt32(Request.QueryString["RISK_ID"].ToString());
			}
			else
			{
				iRISK_ID			= 0;
			}

			if ( Request.QueryString["ADD_INT_ID"] != null )
			{
				hidADD_INT_ID.Value = Request.QueryString["ADD_INT_ID"].ToString();
			}
            iPCUSTOMER_ID =iCUSTOMER_ID ;
			iPOLICY_ID    =iPOLICY_ID ;
			iPOLICY_VERSION_ID =iPOLICY_VERSION_ID ;
			iPBOAT_ID          =iBOAT_ID ; 
			#region If form is not posted back then setting the default values
			if( ! Page.IsPostBack)
			{
				if(strCalledFrom.ToUpper()!="PPA")
				txtRANK.Text = GetBlObject().GetPolNewRankNumber(iCUSTOMER_ID, iPOLICY_ID, iPOLICY_VERSION_ID);
				else																							
				txtRANK.Text = GetBlObject().GetPPAPolNewRankNumber(iCUSTOMER_ID, iPOLICY_ID, iPOLICY_VERSION_ID,iVEHICLE_ID);
				// Added by Swarup on 30-mar-2007
				imgZipLookup.Attributes.Add("style","cursor:hand");
				base.VerifyAddress(hlkZipLookup, txtHOLDER_ADD1,txtHOLDER_ADD2
					, txtHOLDER_CITY, cmbHOLDER_STATE, txtHOLDER_ZIP);
				string url = ClsCommon.GetLookupWindowURL();
		
				SetErrorMessages();
				SetCaptions();

                //if(hidADD_INT_ID.Value == "" || hidADD_INT_ID.Value == "0")
                //{
                //    ClsAdditionalInterest objAddInt = new ClsAdditionalInterest();
                //    DataSet dsBusiness = objAddInt.GetAddIntDetails(int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()), int.Parse(hidADD_INT_ID.Value));
                //    if (dsBusiness.Tables[0].Rows.Count > 0)
                //    {
                //        hidADD_INT_ID.Value = dsBusiness.Tables[0].Rows[0]["ADD_INT_ID"].ToString();
                //        //LoadData(dsBusiness.Tables[0]);
                //    }
                //}

				if(hidADD_INT_ID.Value != "" && hidADD_INT_ID.Value != "0")
				{
					hidMode.Value = "Update";
				}
				else
				{
						hidMode.Value = "Insert";
				}
                               
				FillCombos();
			
				txtMEMO.Text				=	"";
				txtLOAN_REF_NUMBER.Text		=	"";
				GetQueryStringValues();				

				if(hidADD_INT_ID.Value != "" && hidADD_INT_ID.Value != "0")
				{
					//Display the saved Values
					FillAdditionalInterestDetails();
					btnDelete.Visible=true;
					btnActivateDeactivate.Enabled=true;
					hidMode.Value = "Update";
				}					
				else
				{
					hidMode.Value = "Insert";
				}
				SetBillMortagagee(); //Moved here by Charles on 15-Sep-09 for Itrack 6404.
				
			}
			else
			{
				if (hidLOOKUP.Value == "Y")
				{
					cmbHOLDER_ID_SelectedIndexChanged(null, null);
					hidLOOKUP.Value="";
				}
			}
			#endregion
			SetActivateDeactivate();

			//SetWorkFlowControl();
			
		}

		#endregion
		
		#region WEB FORM DESIGNER GENERATED CODE
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
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void GetQueryStringValues()
		{
			if(hidLOB_ID.Value=="" && GetLOBID()!="")
				hidLOB_ID.Value = GetLOBID();

			hidCUSTOMER_ID.Value			=	Request.QueryString["CUSTOMER_ID"].ToString();
			hidPOLICY_ID.Value					=	GetPolicyID();
			hidPOLICY_VERSION_ID.Value			=	GetPolicyVersionID();

			if( Request.QueryString["VEHICLE_ID"] != null && Request.QueryString["VEHICLE_ID"] != "")
				hidVEHICLE_ID.Value				=	Request.QueryString["VEHICLE_ID"].ToString();
			else
				hidVEHICLE_ID.Value				=	"0";

			if( Request.QueryString["DWELLING_ID"] != null && Request.QueryString["DWELLING_ID"] != "" && Request["DWELLING_ID"].ToString() != "0")
				hidDWELLING_ID.Value				=	Request.QueryString["DWELLING_ID"].ToString();
			else
				hidDWELLING_ID.Value				=	"0";

			if( Request.QueryString["BOAT_ID"] != null && Request.QueryString["BOAT_ID"] != "")
				hidBOAT_ID.Value				=	Request.QueryString["BOAT_ID"].ToString();
			else
				hidBOAT_ID.Value				=	"0";

			if( Request.QueryString["TRAILER_ID"] != null && Request.QueryString["TRAILER_ID"] != "")
				hidTRAILER_ID.Value				=	Request.QueryString["TRAILER_ID"].ToString();
			else
				hidTRAILER_ID.Value				=	"0";

			if( Request.QueryString["ENGINE_ID"] != null && Request.QueryString["ENGINE_ID"] != "")
				hidENGINE_ID.Value				=	Request.QueryString["ENGINE_ID"].ToString();
			else
				hidENGINE_ID.Value				=	"0";

		}


		#region Web Event Handler
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			int intReturn=0;
			intReturn=SaveFormValue();
			if(intReturn>0)
			{
				base.OpenEndorsementDetails();
				SetActivateDeactivate();
				SetBillMortagagee();
			}

			
		}
		
		private void cmbHOLDER_ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			hidLOOKUP.Value = ""; //Clearing the lookup field
			this.hidHOLDER_NAME.Value = this.txtHOLDER_ID.Text.Trim();

			DataTable dtHolder;
			DataRow rdHolderDetails;
			string strHolderXML;

			ClsMortgage objHolder = new ClsMortgage();
			dtHolder = objHolder.FillHolderDetails(hidHolderID.Value, out strHolderXML).Tables[0];
			if(dtHolder.Rows.Count>0)
			{
				rdHolderDetails = dtHolder.Rows[0];

				txtHOLDER_ADD1.Text				=	rdHolderDetails["HOLDER_ADD1"].ToString();
				txtHOLDER_ADD2.Text				=	rdHolderDetails["HOLDER_ADD2"].ToString();
				txtHOLDER_CITY.Text				=	rdHolderDetails["HOLDER_CITY"].ToString();
				cmbHOLDER_COUNTRY.SelectedValue	=	rdHolderDetails["HOLDER_COUNTRY"].ToString();
				
				string state = rdHolderDetails["HOLDER_STATE"].ToString();	
				ListItem item;

				item = cmbHOLDER_STATE.Items.FindByValue(state);
				
				if ( item != null )
				{
					cmbHOLDER_STATE.SelectedIndex =	cmbHOLDER_STATE.Items.IndexOf(item);	
				}
		
				txtHOLDER_ZIP.Text				=	rdHolderDetails["HOLDER_ZIP"].ToString();
			
				hidHolderXML.Value = strHolderXML;
			}
			else
			{
				txtHOLDER_ADD1.Text				=	"";
				txtHOLDER_ADD2.Text				=	"";
				txtHOLDER_CITY.Text				=	"";
				cmbHOLDER_STATE.SelectedIndex	=	-1;
			}
			hidFormSaved.Value="2";
		}
		
		#endregion

		#region SetPageCaptions
		/// <summary>
		/// Function for setting the label Captions by reading resource file
		/// </summary>
		private void SetCaptions()
		{

            capHOLDER_ADD1.Text             =       "Address";// objResourceMgr.GetString("txtHOLDER_ADD1");
			capHOLDER_ADD2.Text				=		objResourceMgr.GetString("txtHOLDER_ADD2");
			capHOLDER_CITY.Text				=		objResourceMgr.GetString("txtHOLDER_CITY");
			capHOLDER_COUNTRY.Text			=		objResourceMgr.GetString("cmbHOLDER_COUNTRY");
			capHOLDER_STATE.Text			=		objResourceMgr.GetString("cmbHOLDER_STATE");
			capMEMO.Text					=		objResourceMgr.GetString("txtMEMO");
            capNATURE_OF_INTEREST.Text      =       "Nature Of Interest";// objResourceMgr.GetString("cmbNATURE_OF_INTEREST");
			capLOAN_REF_NUMBER.Text			=		objResourceMgr.GetString("txtLOAN_REF_NUMBER");
			capHOLDER_ZIP.Text				=		objResourceMgr.GetString("txtHOLDER_ZIP");
			capRANK.Text					=		objResourceMgr.GetString("txtRANK");
		}
		
		#endregion 

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// </summary>
		private void SetErrorMessages()
		{
			
			rfvHOLDER_CITY.ErrorMessage						=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"56");
			rfvHOLDER_STATE.ErrorMessage					=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"35");
			rfvHOLDER_ADD1.ErrorMessage						=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"32");
			rfvHOLDER_ZIP.ErrorMessage						=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"37");

			rfvHOLDER_ID.ErrorMessage                       =   Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"201");  
			csvMEMO.ErrorMessage                            =   Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"202");  
			
			revHOLDER_ZIP.ErrorMessage						=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"24");
            revHOLDER_ZIP.ValidationExpression              =   aRegExpZipUS;
			rfvNATURE_OF_INTEREST.ErrorMessage				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"498");
			rfvRANK.ErrorMessage							=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("753");
			revRANK.ErrorMessage							=   Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"102");
			revRANK.ValidationExpression					=   aRegExpInteger;
			rngRANK.ErrorMessage							=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("754");
		}
		#endregion
					
		#region FUNCTION FOR SAVING THE RECORD
		/// <summary>
		/// saves the posted data into table using the business layer class (clsCustomer)
		/// </summary>
		/// <returns>void </returns>
		/// 

		private ClsAdditionalInterest GetBlObject()
		{
			
			if((strCalledFrom.ToUpper()=="WAT" && pageFrom.ToUpper()=="WWAT") || (strCalledFrom.ToUpper()=="WAT" && pageFrom.ToUpper()=="HWAT"))
				return new ClsPolAdditionalInterestWatercraft();

			else if (strCalledFrom.ToUpper()=="WTR")			
				return new ClsPolAdditionalInterestTrailer();

			else if((strCalledFrom.ToUpper()=="HOME" && pageFrom.ToUpper()=="HREC"))
				return new ClsPolAdditionalInterestRecVeh();

			else if (strCalledFrom.ToUpper()=="HOME" || strCalledFrom.ToUpper()=="RENTAL")	
				return new ClsPolAdditionalInterestHomeOwner();

			else if (strCalledFrom.ToUpper()=="PPA" || strCalledFrom.ToUpper()=="MOT") 
				return new ClsPolAdditionalInterestAutomobile();	

			else 			
				return new ClsPolAdditionalInterestLiability();	 
		}

		private int SaveFormValue()
		{
			int intRetVal = 0;
			try
			{
				ClsAdditionalInterest objAdditionalInterest =  GetBlObject();
				Cms.Model.Application.ClsAdditionalInterestInfo objNewAdditionalInterestInfo = getFormValue();
				Cms.Model.Application.ClsAdditionalInterestInfo objOldAdditionalInterestInfo= new Cms.Model.Application.ClsAdditionalInterestInfo();
				
				if(hidMode.Value =="Insert") 
				{
					intRetVal = objAdditionalInterest.AddPolicyAddtionalInterest(objNewAdditionalInterestInfo);
				}
				else
				{
					base.PopulateModelObject(objOldAdditionalInterestInfo,hidOldData.Value);
					objNewAdditionalInterestInfo.ADD_INT_ID = Convert.ToInt32(this.hidADD_INT_ID.Value);
					objNewAdditionalInterestInfo.HOLDER_ID = objOldAdditionalInterestInfo.HOLDER_ID;
					intRetVal =	objAdditionalInterest.UpdatePolicyAdditionalInterest(objNewAdditionalInterestInfo,objOldAdditionalInterestInfo);
				}
				
				if( intRetVal > 0 )			// update successfully performed
				{
					if(hidMode.Value == "Insert")
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"29");							
						this.hidADD_INT_ID.Value = intRetVal.ToString();
						hidIS_ACTIVE.Value		=	"Y";
						btnDelete.Visible=true;
						btnActivateDeactivate.Enabled=true;
						hidFormSaved.Value		=	"1";
						hidMode.Value = "Update";
						SetActivateDeactivate();
					}
					else
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";

					}

					SetWorkFlowControl();

				}
				else if(intRetVal == -1)	// Duplicate code exist, update failed
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"755");
					hidFormSaved.Value		=	"2";
					lblMessage.Visible = true;
					return 0;
				}

				else if(intRetVal == -2)	// Duplicate code exist, update failed
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("754");
					hidFormSaved.Value		=	"2";
					lblMessage.Visible = true;
					return 0;
				}
				else						// Error occured while processing, update failed
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
					hidFormSaved.Value		=	"2";
				}				

				FillAdditionalInterestDetails();
				

				

			lblMessage.Visible = true;
			}
			catch(Exception ex)
			{
				// Show exception message
				lblMessage.Text		=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"21") + " " + ex.Message + " Try again!" ;
				lblMessage.Visible	=	true;
				
				//Publishing the exception using the static method of Exception manager class
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value	= "2";
				return -1;
			}
			return intRetVal;

			
		}
		#region Function to get FormValues		
		private Cms.Model.Application.ClsAdditionalInterestInfo getFormValue()
		{
			Cms.Model.Application.ClsAdditionalInterestInfo objAdditionalInterestInfo = new Cms.Model.Application.ClsAdditionalInterestInfo();
			
			
			objAdditionalInterestInfo.CUSTOMER_ID			=	Convert.ToInt32(hidCUSTOMER_ID.Value);
			objAdditionalInterestInfo.POLICY_ID				=	Convert.ToInt32(hidPOLICY_ID.Value	);
			objAdditionalInterestInfo.POLICY_VERSION_ID		=	Convert.ToInt32(hidPOLICY_VERSION_ID.Value);
			objAdditionalInterestInfo.VEHICLE_ID			=	Convert.ToInt32(hidVEHICLE_ID.Value);
			objAdditionalInterestInfo.DWELLING_ID			=	Convert.ToInt32(hidDWELLING_ID.Value);
			objAdditionalInterestInfo.BOAT_ID               =   Convert.ToInt32(hidBOAT_ID.Value);
		
			if(Request.QueryString["RISK_ID"] != null &&  Request.QueryString["RISK_ID"].ToString() != "" )
			{
				objAdditionalInterestInfo.RISK_ID			=   Convert.ToInt32(Request.QueryString["RISK_ID"].ToString());
			}

			

			//objAdditionalInterestInfo.TRAILER_ID            =   Convert.ToInt32(hidTRAILER_ID.Value);
			objAdditionalInterestInfo.ENGINE_ID             =   Convert.ToInt32(hidENGINE_ID.Value);
			if(hidADD_INT_ID.Value!="")
				objAdditionalInterestInfo.ADD_INT_ID			=	Convert.ToInt32(hidADD_INT_ID.Value);
			objAdditionalInterestInfo.NATURE_OF_INTEREST	=	cmbNATURE_OF_INTEREST.SelectedValue;
			objAdditionalInterestInfo.MEMO					=	txtMEMO.Text;
			objAdditionalInterestInfo.RANK					=	Convert.ToInt32(txtRANK.Text);
			objAdditionalInterestInfo.LOAN_REF_NUMBER		=	txtLOAN_REF_NUMBER.Text;
			objAdditionalInterestInfo.CREATED_BY			=	int.Parse(GetUserId());
			objAdditionalInterestInfo.CREATED_DATETIME		=	DateTime.Now;
			
			if ( hidHolderID.Value != "" )
			{
				objAdditionalInterestInfo.HOLDER_ID			=			Convert.ToInt32(hidHolderID.Value) ;
			}

			objAdditionalInterestInfo.HOLDER_NAME				=			txtHOLDER_ID.Text;
			objAdditionalInterestInfo.HOLDER_ADD1				=			txtHOLDER_ADD1.Text;
			objAdditionalInterestInfo.HOLDER_ADD2				=			txtHOLDER_ADD2.Text;
			objAdditionalInterestInfo.HOLDER_CITY				=			txtHOLDER_CITY.Text;
			objAdditionalInterestInfo.HOLDER_COUNTRY			=			cmbHOLDER_COUNTRY.SelectedValue;
			objAdditionalInterestInfo.HOLDER_STATE				=			cmbHOLDER_STATE.SelectedValue;
			objAdditionalInterestInfo.HOLDER_ZIP				=			txtHOLDER_ZIP.Text;

			if((hidLOB_ID.Value==((int)enumLOB.HOME).ToString() || hidLOB_ID.Value==((int)enumLOB.REDW).ToString()) && cmbBILL_MORTAGAGEE.SelectedItem!=null && cmbBILL_MORTAGAGEE.SelectedItem.Value!="")
			{
				objAdditionalInterestInfo.BILL_MORTAGAGEE = int.Parse(cmbBILL_MORTAGAGEE.SelectedItem.Value);
				hidBILL_MORTAGAGEE.Value = cmbBILL_MORTAGAGEE.SelectedItem.Value;
			}

			return objAdditionalInterestInfo;
		}
		#endregion


		/// <summary>
		/// Function for setting the Holder's Details
		/// </summary>
		/// <returns>Holder details with all the details</returns>
		private ClsHolderInfo getFormValueHolder()
		{
			ClsHolderInfo objHolderInfo = new ClsHolderInfo();

			if ( hidHolderID.Value != "" )
			{
				objHolderInfo.HOLDER_ID					=			Convert.ToInt32(hidHolderID.Value) ;
			}

			objHolderInfo.HOLDER_NAME				=			txtHOLDER_ID.Text;
			objHolderInfo.HOLDER_ADD1				=			txtHOLDER_ADD1.Text;
			objHolderInfo.HOLDER_ADD2				=			txtHOLDER_ADD2.Text;
			objHolderInfo.HOLDER_CITY				=			txtHOLDER_CITY.Text;
			objHolderInfo.HOLDER_COUNTRY			=			cmbHOLDER_COUNTRY.SelectedValue;
			objHolderInfo.HOLDER_STATE				=			cmbHOLDER_STATE.SelectedValue;
			objHolderInfo.HOLDER_ZIP				=			txtHOLDER_ZIP.Text;
			objHolderInfo.MODIFIED_BY				=			int.Parse(GetUserId());
			return objHolderInfo;
		}
		
		/// <summary>
		/// Sets the appropriate text of activate deactivate button
		/// </summary>
		private void SetActivateDeactivate()
		{
			try
			{
				hidIS_ACTIVE.Value	=	hidIS_ACTIVE.Value.Trim();
				btnActivateDeactivate.Visible=true;
				if (hidIS_ACTIVE.Value == "N")
				{
					btnActivateDeactivate.Text = "Activate";					
				}
				else if (hidIS_ACTIVE.Value == "Y")
				{
					
					btnActivateDeactivate.Text = "Deactivate";
				}
				else
					btnActivateDeactivate.Visible=false;
			}
			catch(Exception objEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
			}
		}
		#endregion

		#region Fill Drop down Lists
		private void FillCombos()
		{
			//States
			DataTable dt = Cms.CmsWeb.ClsFetcher.State;
			cmbHOLDER_STATE.DataSource			=	dt;
			cmbHOLDER_STATE.DataTextField		=	STATE_NAME;
			cmbHOLDER_STATE.DataValueField		=	STATE_ID;
			cmbHOLDER_STATE.DataBind();
			cmbHOLDER_STATE.Items.Insert(0,"");
			
			
			//Fill Countery
			DataTable dt1 = Cms.CmsWeb.ClsFetcher.Country;
			cmbHOLDER_COUNTRY.DataSource		=	dt1;
			cmbHOLDER_COUNTRY.DataTextField		=	COUNTRY_NAME;
			cmbHOLDER_COUNTRY.DataValueField	=	COUNTRY_ID;
			cmbHOLDER_COUNTRY.DataBind();
			
		// Code commented as per Gen Iss # 3269
		//	if(Request.QueryString["CALLEDFROM"].ToUpper()=="PPA" || Request.QueryString["CALLEDFROM"].ToUpper()=="MOT" || Request.QueryString["CALLEDFROM"].ToUpper()=="WAT" || Request.QueryString["CALLEDFROM"].ToUpper()=="WTR" || Request.QueryString["CALLEDFROM"].ToUpper()=="GEN" )
		//	{
				cmbNATURE_OF_INTEREST.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("INTPM");
				cmbNATURE_OF_INTEREST.DataTextField = "LookupDesc";
				cmbNATURE_OF_INTEREST.DataValueField = "LookupID";
				cmbNATURE_OF_INTEREST.DataBind();
				cmbNATURE_OF_INTEREST.Items.Insert(0,"");

                cmbADD_INT_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("ADINT");
                cmbADD_INT_TYPE.DataTextField = "LookupDesc";
                cmbADD_INT_TYPE.DataValueField = "LookupID";
                cmbADD_INT_TYPE.DataBind();
                cmbADD_INT_TYPE.Items.Insert(0, "");

		/*	}
			else
			{				
				if(Request.QueryString["CALLEDFROM"].ToUpper()=="HOME"  ||  Request.QueryString["CALLEDFROM"].ToUpper()=="RENTAL" || Request.QueryString["CALLEDFROM"]=="Rental")
				{

					cmbNATURE_OF_INTEREST.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("INTH");
					cmbNATURE_OF_INTEREST.DataTextField = "LookupDesc";
					cmbNATURE_OF_INTEREST.DataValueField = "LookupID";
					cmbNATURE_OF_INTEREST.DataBind();
					cmbNATURE_OF_INTEREST.Items.Insert(0,"");
				}
			}			
						
			*/

			//Fill Holder
			ClsMortgage objHolder = new ClsMortgage();
			DataTable dtHolder;
			
			string rootPath = Cms.BusinessLayer.BlCommon.ClsCommon.GetApplicationPath();
			string url = ClsCommon.GetLookupURL();

			if (iVEHICLE_ID != 0)
			{
				dtHolder = objHolder.FillHolder(iVEHICLE_ID).Tables[0];
				iAPP_ID = Convert.ToInt32(GetAppID()); 
				iAPP_VERSION_ID = Convert.ToInt32(GetAppVersionID());  
				imgSelect.Attributes.Add("onclick","javascript:OpenLookupWithFunction(\"" + url + "\",\"HOLDER_ID\",\"HOLDER_NAME\",\"hidHolderID\",\"txtHOLDER_ID\",\"AddlInterest\",\"Holder List\",\"@VEHICLE_ID=" + iVEHICLE_ID + ";@APP_ID=" + iAPP_ID + ";@APP_VERSION_ID=" + iAPP_VERSION_ID + " ; @CUSTOMER_ID=" +iCUSTOMER_ID + "\",'PostFromLookup()');");
			}
			else if(iBOAT_ID!=0)
			{
				dtHolder = objHolder.FillHolderFromWatercraft(iBOAT_ID).Tables[0];
				if(hidMode.Value != "Update")
				{
					iAPP_ID = Convert.ToInt32(GetAppID()); 
					iAPP_VERSION_ID = Convert.ToInt32(GetAppVersionID());  
					imgSelect.Attributes.Add("onclick","javascript:OpenLookupWithFunction(\"" + url + "\",\"HOLDER_ID\",\"HOLDER_NAME\",\"hidHolderID\",\"txtHOLDER_ID\",\"AddlInterestWaterCraft\",\"Holder List\",\"@BOAT_ID=" + iBOAT_ID + " ; @APP_ID=" + iAPP_ID + " ; @APP_VERSION_ID=" + iAPP_VERSION_ID + " ; @CUSTOMER_ID=" +iCUSTOMER_ID + "\",'PostFromLookup()');");
				}
			}
			else if(iTRAILER_ID!=0)
			{
				dtHolder = objHolder.FillHolderFromTrailer(iTRAILER_ID).Tables[0];
				iAPP_ID = Convert.ToInt32(GetAppID()); 
				iAPP_VERSION_ID = Convert.ToInt32(GetAppVersionID());  
				imgSelect.Attributes.Add("onclick","javascript:OpenLookupWithFunction(\"" + url + "\",\"HOLDER_ID\",\"HOLDER_NAME\",\"hidHolderID\",\"txtHOLDER_ID\",\"AddlInterestTrailer\",\"Holder List\",\"@TRAILER_ID=" + iTRAILER_ID + " ; @APP_ID=" + iAPP_ID + " ; @APP_VERSION_ID=" + iAPP_VERSION_ID + " ; @CUSTOMER_ID=" +iCUSTOMER_ID + "\",'PostFromLookup()');");
			}		
			else 
			{
                //dtHolder = objHolder.FillHolderFromHomeOwner(iDWELLING_ID).Tables[0];
                dtHolder = objHolder.GetHolderList(iDWELLING_ID).Tables[0];
				//imgSelect.Attributes.Add("onclick","javascript:OpenLookupWithFunction('" + url + "','HOLDER_ID','HOLDER_NAME','hidHolderID','txtHOLDER_ID','AddlInterestHomeowner','Holder List',\"@DWELLING_ID=" + iDWELLING_ID + ";@APP_ID=" + iAPP_ID + "; @APP_VERSION_ID=" + iAPP_VERSION_ID + ";@CUSTOMER_ID=" +iCUSTOMER_ID + "\",'PostFromLookup()');");
			}

			
		}
		
		#endregion

		#region GetOldDataXML Function
//		private void GetOldDataXMLForGeneral()
//		{
//			hidOldData.Value = ClsGeneralHolderInterest.GetXmlForPageControls(iCUSTOMER_ID,iAPP_ID,iAPP_VERSION_ID, Int32.Parse(hidADD_INT_ID.Value));
//		}
		#endregion

		#region Activate/ Deactivate feature
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{			
			int intRetVal=-1;
			try
			{
				ClsAdditionalInterest objAdditionalInterest = GetBlObject();
				Cms.Model.Application.ClsAdditionalInterestInfo objNewAdditionalInterestInfo = getFormValue();
				Cms.Model.Application.ClsAdditionalInterestInfo objOldAdditionalInterestInfo= new Cms.Model.Application.ClsAdditionalInterestInfo();
					

				if(hidIS_ACTIVE.Value.ToString().ToUpper().Trim() == "Y")
				{
					
					objNewAdditionalInterestInfo.IS_ACTIVE="N";
					intRetVal	=	objAdditionalInterest.ActivateDeactivatePolicyAdditionalInterest(objNewAdditionalInterestInfo);
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";	
					base.OpenEndorsementDetails();
				}
				else
				{
					objNewAdditionalInterestInfo.IS_ACTIVE="Y";
					intRetVal	=	objAdditionalInterest.ActivateDeactivatePolicyAdditionalInterest(objNewAdditionalInterestInfo);
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";	
					base.OpenEndorsementDetails();
				}
								
				hidFormSaved.Value			=	"1";										
				FillAdditionalInterestDetails();
				SetActivateDeactivate();
				SetBillMortagagee();
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				ExceptionManager.Publish(ex);
			}
			finally
			{
				lblMessage.Visible = true;
				if(objGeneralHolderInterest!= null)
					objGeneralHolderInterest.Dispose();
			}
		}
		#endregion

		#region Delete button feature
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			int intRetVal=-1;			
				
			ClsAdditionalInterest objAdditionalInterest = GetBlObject();
			Cms.Model.Application.ClsAdditionalInterestInfo objNewAdditionalInterestInfo = getFormValue();
			Cms.Model.Application.ClsAdditionalInterestInfo objOldAdditionalInterestInfo= new Cms.Model.Application.ClsAdditionalInterestInfo();
					
			intRetVal	=	objAdditionalInterest.DeletePolicyAdditionalInterest(objNewAdditionalInterestInfo);
				
				
			if(intRetVal>0)
			{
				lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","127");
				lblDelete.Visible=true;
				hidFormSaved.Value = "5";
				hidOldData.Value = "";
				trBody.Attributes.Add("style","display:none");
				SetWorkFlowControl();
				base.OpenEndorsementDetails();
			}
			else if(intRetVal == -1)
			{
				lblDelete.Text		=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"128");
				hidFormSaved.Value		=	"2";
			}	
			
			SetActivateDeactivate();
			SetWorkFlowControl();				
				
			
		}
		#endregion

		private void SetBillMortagagee()
		{

			ClsAdditionalInterest objAdditionalInterest =  GetBlObject();						
			hidBILL_MORTAGAGEE.Value = objAdditionalInterest.GetPolBillMortagagee(iCUSTOMER_ID, iPOLICY_ID, iPOLICY_VERSION_ID,int.Parse(hidDWELLING_ID.Value),int.Parse(hidADD_INT_ID.Value)).ToString();
			if(objAdditionalInterest!=null)
				objAdditionalInterest = null;

			/*Various values of variable hidBILL_MORTAGAGEE
			-1 =  The billing plan is neither 'Agency Bill 1st term/Mortgagee @renewal'  nor 'Insured Bill 1st term/Mortgagee @renewal'
				Hide the controls completely as we do not want the user to select any value for it
			0 = Billing plan is one of the two listed above and the user has not chosen any value for the bill mortagagee.
				Show the controls and let the user select a value for bill mortagagee
			10963(Yes) - Billing plan is one of the two listed above and user has chosen the bill mortagagee.. 
				Hide the controls as the user has already chosen the value for bill mortagagee
			10964(No)/null/blank - Billing plan is one of the two listed above and but user has chosen the bill mortagagee.. 
				Show the controls and let the user select a value for bill mortagagee
			*/
			if((hidLOB_ID.Value==((int)enumLOB.HOME).ToString() || hidLOB_ID.Value==((int)enumLOB.REDW).ToString()) && hidBILL_MORTAGAGEE.Value !="-1")
			{
				if(cmbBILL_MORTAGAGEE.Items.Count<1)
				{
					cmbBILL_MORTAGAGEE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
					cmbBILL_MORTAGAGEE.DataTextField="LookupDesc"; 
					cmbBILL_MORTAGAGEE.DataValueField="LookupId";
					cmbBILL_MORTAGAGEE.DataBind();
					cmbBILL_MORTAGAGEE.Items.Insert(0,"");
				}
				cmbBILL_MORTAGAGEE.Visible = capBILL_MORTAGAGEE.Visible = true;//Added by Charles on 15-Sep-09 for Itrack 6404
			}
//			//else
//				//cmbBILL_MORTAGAGEE.Visible = capBILL_MORTAGAGEE.Visible = false;
		else
			{
				cmbBILL_MORTAGAGEE.SelectedIndex = -1;
				cmbBILL_MORTAGAGEE.Visible = capBILL_MORTAGAGEE.Visible = false;
			}
		}

		#region set workflow control
		private void SetWorkFlowControl()
		{	//Added the Add Interest Screen ID 251_4_0 and 253_1_0
            //if(GetSystemId().ToUpper()!="I001" && GetSystemId().ToUpper()!="IUAT")
            //{

            //if (base.ScreenId == "239_2_0" || base.ScreenId == "231_3_0" || base.ScreenId == "246_4_0" || base.ScreenId == "248_1_0" || base.ScreenId=="259_2_0" || base.ScreenId=="251_4_0" || base.ScreenId=="253_1_0" || base.ScreenId=="227_3_0" || base.ScreenId=="283_3_0" || base.ScreenId=="243_1_0")
            //{
            //    myWorkFlow.IsTop	=	false;
            //    myWorkFlow.ScreenID	=	base.ScreenId;
				
            //    myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
            //    myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
            //    myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
            //    myWorkFlow.WorkflowModule="POL";
			
            //    SetOtherKeys();

            //    myWorkFlow.GetScreenStatus();
            //    myWorkFlow.SetScreenStatus();
            //}
            //}
		}
		#endregion

		#region SetOtherKeys
		/// <summary>
		/// Sets the keys for work flow depending upon the calledfrom variable
		/// </summary>
		private void SetOtherKeys()
		{
			//Setting screen Id.	
			switch (strCalledFrom.ToUpper()) 
			{
//                case "HOME":
					
//                    if( Request.QueryString["DWELLING_ID"] != null && Request.QueryString["DWELLING_ID"] != "" && Request["DWELLING_ID"].ToString() != "0")
//                        myWorkFlow.AddKeyValue("DWELLING_ID",Request.QueryString["DWELLING_ID"]);
//                    break;
//                case "RENTAL":
////					if(Request.QueryString["DWELLING_ID"] != null || Request.QueryString["DWELLING_ID"] != "")
////						myWorkFlow.AddKeyValue("VEHICLE_ID",Request.QueryString["DWELLING_ID"]);
//                    break;
//                case "MOT":
//                    if(Request.QueryString["VEHICLE_ID"] != null || Request.QueryString["VEHICLE_ID"] != "")
//                        myWorkFlow.AddKeyValue("VEHICLE_ID",Request.QueryString["VEHICLE_ID"]);
//                    break;
//                case "PPA":
//                    if(Request.QueryString["VEHICLE_ID"] != null || Request.QueryString["VEHICLE_ID"] != "")
//                        myWorkFlow.AddKeyValue("VEHICLE_ID",Request.QueryString["VEHICLE_ID"]);
//                    break;
				case "WAT":
				switch (pageFrom)
				{
					case "WWAT":
						break;
					case "HWAT":
						break;
					case "RWAT":
						break;
				}
					break;
				case "WEN":				
				switch (pageFrom)
				{
					case "WWEN":
						break;
					case "HWEN":
						break;
					case "RWEN":
						break;
				}
					break;
				case "WTR":
				switch (pageFrom)	
				{
					case "WWTR":
						if (Request.QueryString["TRAILER_ID"] != null && Request.QueryString["TRAILER_ID"] != "")
							myWorkFlow.AddKeyValue("TRAILER_ID",Request.QueryString["TRAILER_ID"]);						
						break;
					case "HWTR":
						break;
					case "RWTR":
						break;
				}
					break;
				default:
					break;
			}
			
		}
		#endregion
	}
}
