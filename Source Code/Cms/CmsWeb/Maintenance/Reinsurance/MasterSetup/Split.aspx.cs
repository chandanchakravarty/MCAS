/* ***************************************************************************************
   Author		: Harmanjeet Singh
   Creation Date: April 30, 2007
   Last Updated : 
   Reviewed By	: 
   Purpose		: This file is used for Split. 
   Comments		: 
   ------------------------------------------------------------------------------------- 
   History	Date	     Modified By		Description
   
   ------------------------------------------------------------------------------------- 
   *****************************************************************************************/

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


using System.Resources; 
using System.Reflection; 

using Cms.CmsWeb;
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlCommon.Reinsurance;  
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 
using Cms.Model.Maintenance.Reinsurance;
using Cms.BusinessLayer.BlCommon;

namespace Cms.CmsWeb.Maintenance.Reinsurance.MasterSetup
{
	/// <summary>
	/// Summary description for Split.
	/// </summary>
	public class Split : Cms.CmsWeb.cmsbase
	{
		# region DECELARATION

		System.Resources.ResourceManager objResourceMgr;

		ClsSplit objSplit;

		private string strRowId, strFormSaved;
		//string oldXML;

		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capREIN_EFFECTIVE_DATE;
		protected System.Web.UI.WebControls.TextBox txtREIN_EFFECTIVE_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.Label capREIN_LINE_OF_BUSINESS;
		protected System.Web.UI.WebControls.DropDownList cmbREIN_LINE_OF_BUSINESS;
		protected System.Web.UI.WebControls.Label capREIN_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbREIN_STATE;
		protected System.Web.UI.WebControls.Label capREIN_COVERAGE;
		protected System.Web.UI.WebControls.ListBox cmbREIN_COVERAGE;
		protected System.Web.UI.WebControls.Button btnSELECT;
		protected System.Web.UI.WebControls.Button btnDESELECT;
		protected System.Web.UI.WebControls.ListBox cmbRECIPIENTS;
		protected System.Web.UI.WebControls.ListBox cmbFROMREIN_COVERAGE;

		protected System.Web.UI.WebControls.Label capREIN_IST_SPLIT;
		protected System.Web.UI.WebControls.TextBox txtREIN_IST_SPLIT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_IST_SPLIT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_LINE_OF_BUSINESS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPolicyType;
		protected System.Web.UI.WebControls.Label capREIN_IST_SPLIT_COVERAGE;
		protected System.Web.UI.WebControls.Label capREIN_1ST_SPLIT_COVERAGE;
		protected System.Web.UI.WebControls.ListBox cmbREIN_IST_SPLIT_COVERAGE;
		protected System.Web.UI.WebControls.ListBox cmbFROMREIN_1ST_SPLIT_COVERAGE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_IST_SPLIT_COVERAGE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_1ST_SPLIT_COVERAGE;
		protected System.Web.UI.WebControls.Button btn1stSELECT;
		protected System.Web.UI.WebControls.Button btn1stDESELECT;
		protected System.Web.UI.WebControls.ListBox cmb1stRECIPIENTS;
		protected System.Web.UI.WebControls.Label capREIN_2ND_SPLIT;
		protected System.Web.UI.WebControls.Label capPolicyType;
		protected System.Web.UI.WebControls.TextBox txtREIN_2ND_SPLIT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_2ND_SPLIT;
		protected System.Web.UI.WebControls.Label capREIN_2ND_SPLIT_COVERAGE;
		protected System.Web.UI.WebControls.ListBox cmbREIN_2ND_SPLIT_COVERAGE;
		protected System.Web.UI.WebControls.ListBox cmbFROMREIN_2ND_SPLIT_COVERAGE;
		protected System.Web.UI.WebControls.Button btn2ndSELECT;
		protected System.Web.UI.WebControls.Button btn2ndDESELECT;
		protected System.Web.UI.WebControls.ListBox cmb2ndRECIPIENTS;
        protected System.Web.UI.WebControls.Label cap2ndRECIPIENTS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_2ND_SPLIT_COVERAGE;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOBChange;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidStateChng;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREIN_SPLIT_DEDUCTION_ID;
		protected System.Web.UI.WebControls.RangeValidator rngREIN_IST_SPLIT;
		protected System.Web.UI.WebControls.RangeValidator rngREIN_2ND_SPLIT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidStateChange;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidReset;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_EFFECTIVE_DATE;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_EFFECTIVE_DATE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidState;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOVERAGE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hid1stCOVERAGE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hid2ndCOVERAGE;

		protected System.Web.UI.WebControls.Label capPOLICY_TYPE;
		protected System.Web.UI.WebControls.ListBox cmbPOLICY_TYPE;
		//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_TYPE;
		protected System.Web.UI.WebControls.Label capRECIPIENTS;
		protected System.Web.UI.WebControls.ListBox cmbFROMPOLICY_TYPE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY;
		protected System.Web.UI.WebControls.CustomValidator csvPOLICY;
		protected System.Web.UI.WebControls.Button btnPolSELECT;
		protected System.Web.UI.WebControls.Button btnPolDESELECT;
		protected System.Web.UI.WebControls.Label capPol_tpye;
		protected System.Web.UI.WebControls.Label capPol_type;

		protected System.Web.UI.HtmlControls.HtmlTableRow trPolicy_type;		
		protected System.Web.UI.HtmlControls.HtmlTableRow trCapPolicy;	
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidShowHide;
        
        protected System.Web.UI.WebControls.Label capMessages;

		# endregion

		# region	PAGE LOAD
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			btnReset.Attributes.Add("onclick","javascript:Reset();");
			//btnActivateDeactivate.Attributes.Add("onclick","javascript:return ActivateDeactivate();");
			btnSELECT.Attributes.Add("onclick","javascript:selectCoverages();return false;");
			btnDESELECT.Attributes.Add("onclick","javascript:deselectCoverages();return false;");

			btn1stSELECT.Attributes.Add("onclick","javascript:select1stCoverages();return false;");
			btn1stDESELECT.Attributes.Add("onclick","javascript:deselect1stCoverages();return false;");
			btn2ndSELECT.Attributes.Add("onclick","javascript:select2ndCoverages();return false;");
			btn2ndDESELECT.Attributes.Add("onclick","javascript:deselect2ndCoverages();return false;");
			btnPolSELECT.Attributes.Add("onclick","javascript:selectPolicies();return false;");
			btnPolDESELECT.Attributes.Add("onclick","javascript:deselectPolicies();return false;");
			btnSave.Attributes.Add("onclick","javascript:setCoverages();setCoverages1st();setCoverages2nd();setPolicies();"); 
			hlkEFFECTIVE_DATE.Attributes.Add("OnClick","fPopCalendar(document.Split.txtREIN_EFFECTIVE_DATE,document.Split.txtREIN_EFFECTIVE_DATE)");
			base.ScreenId="398";
			lblMessage.Visible = false;
			SetErrorMessages();
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
			cmbREIN_LINE_OF_BUSINESS.AutoPostBack=true;
			# region *********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	   =	CmsButtonType.Write;
			btnReset.PermissionString  =	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
			btnActivateDeactivate.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass	  =	 CmsButtonType.Write;
			btnSave.PermissionString  =	 gstrSecurityXML;

			
			# endregion //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Reinsurance.MasterSetup.Split" ,System.Reflection.Assembly.GetExecutingAssembly());
			
			# region POST BACK EVENT HANDLER

			if(!Page.IsPostBack)
			{																 
				if (Request.Params["REIN_SPLIT_DEDUCTION_ID"] != null)
				{
					if (Request.Params["REIN_SPLIT_DEDUCTION_ID"].ToString() != "")
					{
						this.hidREIN_SPLIT_DEDUCTION_ID.Value = Request.Params["REIN_SPLIT_DEDUCTION_ID"].ToString();
						GenerateXML(hidREIN_SPLIT_DEDUCTION_ID.Value);
					}
					//PopulateCoverage();
					StateChange();
				}				
				SetCaptions();
				SetDropdownList();  
				#region "Loading singleton"
				//using singleton object for country and state dropdown
				/*
				DataTable dt = objTIVGroup.GetContractNumber().Tables[0];
				this.cmbREIN_TIV_CONTRACT_NUMBER.DataSource		= dt;
				cmbREIN_TIV_CONTRACT_NUMBER.DataTextField	= "CONTRACT_NUMBER";
				cmbREIN_TIV_CONTRACT_NUMBER.DataValueField	= "CONTRACT_NUMBER";
				cmbREIN_TIV_CONTRACT_NUMBER.DataBind();
				cmbREIN_TIV_CONTRACT_NUMBER.Items[0].Selected=true;  
				
				
				cmbM_REIN_CONTACT_COUNTRY.DataSource		= dt;
				cmbM_REIN_CONTACT_COUNTRY.DataTextField	= "Country_Name";
				cmbM_REIN_CONTACT_COUNTRY.DataValueField	= "Country_Id";
				cmbM_REIN_CONTACT_COUNTRY.DataBind();
				cmbM_REIN_CONTACT_COUNTRY.Items[0].Selected=true; 

				dt = Cms.CmsWeb.ClsFetcher.State;
				cmbREIN_CONTACT_STATE.DataSource		= dt;
				cmbREIN_CONTACT_STATE.DataTextField	= "STATE_NAME";
				cmbREIN_CONTACT_STATE.DataValueField	= "STATE_ID";
				cmbREIN_CONTACT_STATE.DataBind();
				cmbREIN_CONTACT_STATE.Items.Insert(0,"");
				*/
				//START APRIL 11 HARMANJEET
				//this.cmbREIN_DESCRIPTION.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RCONCD");
				//cmbSEX.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("SEX");
				//cmbREIN_DESCRIPTION.DataTextField="LookupDesc";
				//cmbREIN_DESCRIPTION.DataValueField="LookupCode";
				//cmbREIN_DESCRIPTION.DataBind();
				//cmbREIN_DESCRIPTION.Items.Insert(0,"");	
				//END HARMANJEET

				//this.cmbREIN_EXTERIOR_CONSTRUCTION.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CONTYP");
				//cmbSEX.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("SEX");
				//cmbREIN_EXTERIOR_CONSTRUCTION.DataTextField="LookupDesc";
				//cmbREIN_EXTERIOR_CONSTRUCTION.DataValueField="LookupCode";
				//cmbREIN_EXTERIOR_CONSTRUCTION.DataBind();
				//cmbREIN_EXTERIOR_CONSTRUCTION.Items.Insert(0,"");	
				
				DataTable dt = Cms.CmsWeb.ClsFetcher.ActiveState;
				this.cmbREIN_STATE.DataSource		= dt;
				cmbREIN_STATE.DataTextField	= "STATE_NAME";
				cmbREIN_STATE.DataValueField	= "STATE_ID";
				cmbREIN_STATE.DataBind();
				cmbREIN_STATE.Items.Insert(0,"");

				DataTable dtLOB = Cms.CmsWeb.ClsFetcher.LOBs;
				this.cmbREIN_LINE_OF_BUSINESS.DataSource			= dtLOB;
				cmbREIN_LINE_OF_BUSINESS.DataTextField		= "LOB_DESC";
				cmbREIN_LINE_OF_BUSINESS.DataValueField		= "LOB_ID";
				cmbREIN_LINE_OF_BUSINESS.DataBind();
				//Remove General Liability
				ListItem Li = cmbREIN_LINE_OF_BUSINESS.Items.FindByValue("7");//"7" -> General Liability
				if (Li != null)	
				{
					cmbREIN_LINE_OF_BUSINESS.Items.Remove(Li);	
				}
				cmbREIN_LINE_OF_BUSINESS.Items.Insert(0,"");


				#endregion//Loading singleton

              
				GetDataForEditMode();
				StateChange();


                if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "CmsWeb/support/PageXML/" + GetSystemId(), "Split.xml"))
                {
                    setPageControls(Page, @Request.PhysicalApplicationPath + "/CmsWeb/support/PageXML/" + GetSystemId() + "/Split.xml");
                    hidState.Value = cmbREIN_STATE.SelectedItem.Value;   
                }
              
				

			}
			# endregion
			ClientScript.RegisterStartupScript(this.GetType(),"showhidepoltype","<script >showhidepoltype();</script>");
         

		}
		# endregion

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			
			/*
			this.revM_REIN_CONTACT_CITY.ValidationExpression="^[a-zA-Z0-9]{1,50}";
			this.revM_REIN_CONTACT_ADDRESS_1.ValidationExpression="^[a-zA-Z0-9]{1,50}";
			this.revM_REIN_CONTACT_ADDRESS_2.ValidationExpression="^[a-zA-Z0-9]{1,50}";

			this.revM_REIN_CONTACT_CITY.ErrorMessage= "Please eneter valid city";
			this.revM_REIN_CONTACT_ADDRESS_1.ErrorMessage="Please eneter valid address";
			this.revM_REIN_CONTACT_ADDRESS_2.ErrorMessage="Please eneter valid address";

			*/
			//this.revREIN_IST_SPLIT.ValidationExpression="^[0-9]{1,3}";
			//this.revREIN_2ND_SPLIT.ValidationExpression="^[0-9]{1,3}";

			//this.revREIN_IST_SPLIT.ErrorMessage="Please enter value";
			//this.revREIN_2ND_SPLIT.ErrorMessage="Please enter value";
			revREIN_EFFECTIVE_DATE.ErrorMessage	    =	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			this.revREIN_EFFECTIVE_DATE.ValidationExpression		= aRegExpDate;
            rfvREIN_STATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
            rfvREIN_LINE_OF_BUSINESS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
            rfvREIN_IST_SPLIT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");
            rfvREIN_2ND_SPLIT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvREIN_1ST_SPLIT_COVERAGE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");
            rfvREIN_2ND_SPLIT_COVERAGE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "7");
            rfvPolicyType.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "8");
            rngREIN_IST_SPLIT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");
            rngREIN_2ND_SPLIT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");
		}

		#endregion

		# region SET DROPDOWNLIST
		private void SetDropdownList()
		{
			//cmbREIN_CONTACT_IS_BROKER.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			//cmbREIN_CONTACT_IS_BROKER.DataTextField="LookupDesc"; 
			//cmbREIN_CONTACT_IS_BROKER.DataValueField="LookupCode";
			//cmbREIN_CONTACT_IS_BROKER.DataBind();
			//cmbREIN_CONTACT_IS_BROKER.Items.Insert(0,"");
			objSplit = new ClsSplit();
			//DataSet dtContractDetails;
			DataSet oDs = objSplit.GetDataForPageControls(this.hidREIN_SPLIT_DEDUCTION_ID.Value);
			DataTable tbl=oDs.Tables[0];
			//ClsCommon.SelectValueinDDL(cmbREIN_LINE_OF_BUSINESS,tbl.Rows[0]["REIN_LINE_OF_BUSINESS"]);
			//commented on 20 sep 2007		
			/*if(oDs.Tables[0].Rows.Count >0)
			{
				DataRow oDr  = oDs.Tables[0].Rows[0];
				hidCOVERAGE.Value =  oDr["REIN_COVERAGE"].ToString();
			}*/
			//
			if(oDs!=null)
			{
				if(oDs.Tables[0].Rows.Count >0)
				{
					hidCOVERAGE.Value = oDs.Tables[0].Rows[0]["REIN_COVERAGE"].ToString();
					hid1stCOVERAGE.Value = oDs.Tables[0].Rows[0]["REIN_IST_SPLIT_COVERAGE"].ToString();
					hid2ndCOVERAGE.Value = oDs.Tables[0].Rows[0]["REIN_2ND_SPLIT_COVERAGE"].ToString();
					hidPOLICY.Value = oDs.Tables[0].Rows[0]["POLICY_TYPE"].ToString();
				}
			}
		}
		# endregion

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private Cms.Model.Maintenance.Reinsurance.ClsSplitInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			Cms.Model.Maintenance.Reinsurance.ClsSplitInfo objSplitInfo;
			objSplitInfo = new ClsSplitInfo();
			
			if(this.txtREIN_EFFECTIVE_DATE.Text.Trim()!="")
				objSplitInfo.REIN_EFFECTIVE_DATE =ConvertToDate(this.txtREIN_EFFECTIVE_DATE.Text);
			
			//				
			objSplitInfo.REIN_LINE_OF_BUSINESS = this.cmbREIN_LINE_OF_BUSINESS.SelectedItem.Value;//.SelectedValue;//.SelectedItem.Text;
			objSplitInfo.REIN_STATE = this.cmbREIN_STATE.SelectedValue;

			//Policy Type: start
			
			string policy=(string)hidPOLICY.Value;
			if (policy !="" && policy != "0")
			{
				string[] policies= policy.Split(',');  
				policy="";
				for (int i=0;i <policies.GetLength(0)-1 ;i++)
				{
					policy=policy + policies[i].ToString()  + ","; 	
				}
			}
			else 
				policy = "0";
			objSplitInfo.POLICY_TYPE = policy;

			//Policy Type: end
			string coverage=(string)hidCOVERAGE.Value;
			if (coverage !="" && coverage != "0")
			{
				string[] coverages= coverage.Split(',');  
				coverage="";
				for (int i=0;i <coverages.GetLength(0)-1 ;i++)
				{
					coverage=coverage + coverages[i].ToString()  + ","; 	
				}
			}
			if (coverage =="0" ) 
				coverage="";			
			objSplitInfo.REIN_COVERAGE = coverage;
			//1st Split Coverage
			string coverage1st=(string)hid1stCOVERAGE.Value;
			if (coverage1st !="" && coverage1st != "0")
			{
				string[] coverages1st= coverage1st.Split(',');  
				coverage1st="";
				for (int i=0;i <coverages1st.GetLength(0)-1 ;i++)
				{
					coverage1st=coverage1st + coverages1st[i].ToString()  + ","; 	
				}
			}
			if (coverage1st =="0" ) 
				coverage1st="";			
			objSplitInfo.REIN_IST_SPLIT_COVERAGE = coverage1st;
			//2nd Split Coverage
			string coverage2nd=(string)hid2ndCOVERAGE.Value;
			if (coverage2nd !="" && coverage2nd != "0")
			{
				string[] coverages2nd= coverage2nd.Split(',');  
				coverage2nd="";
				for (int i=0;i <coverages2nd.GetLength(0)-1 ;i++)
				{
					coverage2nd=coverage2nd + coverages2nd[i].ToString()  + ","; 	
				}
			}
			if (coverage2nd =="0" ) 
				coverage2nd="";			
			objSplitInfo.REIN_2ND_SPLIT_COVERAGE = coverage2nd;
			//objSplitInfo.REIN_COVERAGE = this.cmbREIN_COVERAGE.SelectedItem.Value;

			objSplitInfo.REIN_IST_SPLIT = this.txtREIN_IST_SPLIT.Text;
			//objSplitInfo.REIN_IST_SPLIT_COVERAGE = this.cmbREIN_IST_SPLIT_COVERAGE.SelectedItem.Value;
			objSplitInfo.REIN_2ND_SPLIT = this.txtREIN_2ND_SPLIT.Text;
			//objSplitInfo.REIN_2ND_SPLIT_COVERAGE = this.cmbREIN_2ND_SPLIT_COVERAGE.SelectedItem.Value;
			


			objSplitInfo.IS_ACTIVE=hidIS_ACTIVE.Value;
			//objSplitInfo.POLICY_TYPE = int.Parse(this.cmbPOLICY_TYPE.SelectedItem.Value);
	
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	this.hidREIN_SPLIT_DEDUCTION_ID.Value;
			//oldXML		= hidOldData.Value;
			//Returning the model object

			return objSplitInfo;
		}
		#endregion
		private void btnReset_Click(object sender, System.EventArgs e)
		{
			StateChange();
		}
		#region "Web Event Handlers"
		/// <summary>
		/// If form is posted back then add entry in database using the BL object
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				objSplit= new ClsSplit();

				//Retreiving the form values into model class object
				ClsSplitInfo objSplitInfo = GetFormValue();
				string CustomInfo ="";
				//				CustomInfo =//"Coverage:" + coverage +"<br>"+
				//					"1st Split Coverage:" + cmbREIN_IST_SPLIT_COVERAGE.Items[cmbREIN_IST_SPLIT_COVERAGE.SelectedIndex].Text +"<br>"
				//					+"2nd Split Coverage:" + cmbREIN_2ND_SPLIT_COVERAGE.Items[cmbREIN_2ND_SPLIT_COVERAGE.SelectedIndex].Text;
				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objSplitInfo.CREATED_BY = int.Parse(GetUserId());
					objSplitInfo.CREATED_DATETIME = DateTime.Parse(DateTime.Now.ToShortDateString());
					//objTIVGroupInfo.IS_ACTIVE="Y"; 

					//Calling the add method of business layer class
					intRetVal = objSplit.Add(objSplitInfo,CustomInfo);

					if(intRetVal>0)
					{
						this.hidREIN_SPLIT_DEDUCTION_ID.Value = objSplitInfo.REIN_SPLIT_DEDUCTION_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						SetDropdownList();
						this.hidOldData.Value	= objSplit.GetDataForPageControls(this.hidREIN_SPLIT_DEDUCTION_ID.Value).GetXml();
                      
						//hidIS_ACTIVE.Value = "Y";
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value			=		"2";
					}
					else if(intRetVal == -2)
					{
						lblMessage.Text				=		"This Combination of Policy type already exists. Please save another Combination of policy type";
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsSplitInfo objOldSplitInfo;
					objOldSplitInfo = new ClsSplitInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldSplitInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					if(strRowId!="")
						objSplitInfo.REIN_SPLIT_DEDUCTION_ID = int.Parse(strRowId);
					objSplitInfo.MODIFIED_BY = int.Parse(GetUserId());
					objSplitInfo.LAST_UPDATED_DATETIME = DateTime.Parse(DateTime.Now.ToShortDateString());
                    

					//Updating the record using business layer class object
					intRetVal	= objSplit.Update(objOldSplitInfo,objSplitInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						this.hidOldData.Value	= objSplit.GetDataForPageControls(strRowId).GetXml();
						
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
						hidFormSaved.Value		=	"1";
					}
					else if(intRetVal == -2)
					{
						lblMessage.Text				=		"This Combination of Policy type already exists. Please save another Combination of policy type";
						hidFormSaved.Value			=		"2";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"1";
					}
					lblMessage.Visible = true;
				}
			}
		
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
                
			}
			finally
			{
				if(objSplit!= null)
					objSplit.Dispose();
			}

			
		}
		
		
		#endregion

		# region GENERATE XML
		/// <summary>
		/// fetching data based on query string values
		/// </summary>
		private void GenerateXML(string REIN_SPLIT_DEDUCTION_ID)
		{
			string strREIN_SPLIT_DEDUCTION_ID=REIN_SPLIT_DEDUCTION_ID;
			objSplit=new ClsSplit(); 
  			
			if(strREIN_SPLIT_DEDUCTION_ID!="" && strREIN_SPLIT_DEDUCTION_ID!=null)
			{
				try
				{
					DataSet ds=new DataSet(); 
					ds=objSplit.GetDataForPageControls(strREIN_SPLIT_DEDUCTION_ID);
					hidOldData.Value=ds.GetXml(); 

					//hidFormSaved.Value="1"; 
				}
				catch(Exception ex)
				{
					lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
					lblMessage.Visible	=	true;
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
					hidFormSaved.Value			=	"2";                
                    
				}
				finally
				{
					if(objSplit!= null)
						objSplit.Dispose();
				}  
                
			}
                
		}

		# endregion GENERATE XML

		# region SET CAPTIONS
		private void SetCaptions()
		{
			this.capREIN_EFFECTIVE_DATE.Text		=		objResourceMgr.GetString("txtREIN_EFFECTIVE_DATE");
			this.capREIN_LINE_OF_BUSINESS.Text		=		objResourceMgr.GetString("cmbREIN_LINE_OF_BUSINESS");
			this.capREIN_STATE.Text					=		objResourceMgr.GetString("cmbREIN_STATE");
			this.capREIN_COVERAGE.Text					=		objResourceMgr.GetString("cmbREIN_COVERAGE");
			this.capREIN_IST_SPLIT.Text		=	objResourceMgr.GetString("txtREIN_IST_SPLIT");
			this.capREIN_IST_SPLIT_COVERAGE.Text		=		objResourceMgr.GetString("cmbREIN_IST_SPLIT_COVERAGE");
			this.capREIN_2ND_SPLIT.Text					=		objResourceMgr.GetString("txtREIN_2ND_SPLIT");
			this.capREIN_2ND_SPLIT_COVERAGE.Text=	objResourceMgr.GetString("cmbREIN_2ND_SPLIT_COVERAGE");
            this.capREIN_1ST_SPLIT_COVERAGE.Text = objResourceMgr.GetString("cmbREIN_1ST_SPLIT_COVERAGE");
            this.cap2ndRECIPIENTS.Text = objResourceMgr.GetString("cmb2ndRECIPIENTS");
            
			//END Harmanjeet
		}

		# endregion SET CAPTIONS

		#region DEACTIVATE ACTIVATE BUTTON CLICK
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
				//objDepartment =  new ClsDepartment();
				Cms.BusinessLayer.BlCommon.Reinsurance.ClsSplit objSplit = new ClsSplit();

				Model.Maintenance.Reinsurance.ClsSplitInfo objSplitInfo;
				objSplitInfo = GetFormValue();
				
				string strRetVal = "";
				string strCustomInfo ="";
				strCustomInfo = "Line Of Business:" + cmbREIN_LINE_OF_BUSINESS.Items[cmbREIN_LINE_OF_BUSINESS.SelectedIndex].Text +"<br>"
					+"State:" + cmbREIN_STATE.Items[cmbREIN_STATE.SelectedIndex].Text ;
				//								+"1st Split Coverage:" + cmbREIN_IST_SPLIT_COVERAGE.Items[cmbREIN_IST_SPLIT_COVERAGE.SelectedIndex].Text +"<br>"
				//								+"2nd Split Coverage:" + cmbREIN_2ND_SPLIT_COVERAGE.Items[cmbREIN_2ND_SPLIT_COVERAGE.SelectedIndex].Text;
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "Split Has Been Deactivated Successfully.";
					objSplit.TransactionInfoParams = objStuTransactionInfo;
					strRetVal = objSplit.ActivateDeactivate(hidREIN_SPLIT_DEDUCTION_ID.Value,"N",strCustomInfo);
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Split Has Been Activated Successfully.";
					objSplit.TransactionInfoParams = objStuTransactionInfo;
					objSplit.ActivateDeactivate(hidREIN_SPLIT_DEDUCTION_ID.Value,"Y",strCustomInfo);
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}

				//				if (strRetVal == "-1")
				//				{
				//					/*Profit Center is assigned*/
				//					lblMessage.Text =  ClsMessages.GetMessage(base.ScreenId,"513");
				//					lblDelete.Visible = false;
				//				}
				
				hidFormSaved.Value			=	"0";
				GetOldDataXML();
				hidReset.Value="0";

			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				lblMessage.Visible = true;
				if(objSplit!= null)
					objSplit.Dispose();
			}
		}
		#endregion
		private void GetOldDataXML()
		{
			Cms.BusinessLayer.BlCommon.Reinsurance.ClsSplit objSplit = new ClsSplit();
			if (hidREIN_SPLIT_DEDUCTION_ID.Value.ToString() != "" )
			{
				this.hidOldData.Value	= objSplit.GetDataForPageControls(strRowId).GetXml();
			}
		}

		/*private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			Cms.BusinessLayer.BlCommon.Reinsurance.ClsSplit objSplit = new Cms.BusinessLayer.BlCommon.Reinsurance.ClsSplit();
			try
			{
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();						
				/*if(this.btnActivate.Text=="Deactivate")
					{
								  
						int intStatusCheck=objSplit.GetDeactivateActivate(this.hidREIN_SPLIT_DEDUCTION_ID.Value.ToString(),"N");
						btnActivate.Text="Activate";
						
					}
					else
					{
						
						int intStatusCheck=objSplit.GetDeactivateActivate(this.hidREIN_SPLIT_DEDUCTION_ID.Value.ToString(),"Y");
						btnActivate.Text="Deactivate";
						
					}
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "Split is Deactivated Succesfully.";
					objSplit.TransactionInfoParams = objStuTransactionInfo;
					int strRetVal = objSplit.GetDeactivateActivate(hidREIN_SPLIT_DEDUCTION_ID.Value.ToString(),"N");
					if(strRetVal == 1)
					{
						//btnActivateDeactivate.Text = "Activate";
						lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
						hidIS_ACTIVE.Value="N";
					}
					else if(strRetVal == -2)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"948");
						hidIS_ACTIVE.Value="Y";
					}
                   
					//string strScript="<script>RefreshWebGrid('" + hidAGENCY_ID.Value + "','');</script>";
					//RegisterStartupScript("REFRESHGRID",strScript);
				}
				else
				{					
					objStuTransactionInfo.transactionDescription = "Split is Activated Succesfully.";
					objSplit.TransactionInfoParams = objStuTransactionInfo;
					int strRetVal = objSplit.GetDeactivateActivate(this.hidREIN_SPLIT_DEDUCTION_ID.Value.ToString(),"Y");
					if(strRetVal == 1)
					{
						lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
						//btnActivateDeactivate.Text = "Deactivate";
						hidIS_ACTIVE.Value="Y";
					}
					else if(strRetVal == -2)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"948");
						hidIS_ACTIVE.Value="N";
					}
					//string strScript="<script>RefreshWebGrid('','" + hidAGENCY_ID.Value + "');</script>";
					//RegisterStartupScript("REFRESHGRID",strScript);
				}
				this.hidOldData.Value	= objSplit.GetDataForPageControls(this.hidREIN_SPLIT_DEDUCTION_ID.Value).GetXml();
        
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
		            
			}
			finally
			{
				lblMessage.Visible = true;
				if(objSplit!= null)
					objSplit.Dispose();
			}
		}*/
		
		# region  G E T   D A T A   F O R   E D I T   M O D E 

		private void GetDataForEditMode()
		{
			try
			{
				objSplit = new ClsSplit();
				//DataSet dtContractDetails;
				DataSet oDs = objSplit.GetDataForPageControls(this.hidREIN_SPLIT_DEDUCTION_ID.Value);
				DataTable tbl=oDs.Tables[0];
				//ClsCommon.SelectValueinDDL(cmbREIN_LINE_OF_BUSINESS,tbl.Rows[0]["REIN_LINE_OF_BUSINESS"]);
					
				if(oDs.Tables[0].Rows.Count >0)
				{
					DataRow oDr  = oDs.Tables[0].Rows[0];
					
						
					this.txtREIN_EFFECTIVE_DATE.Text=ConvertToDateCulture(Convert.ToDateTime(oDr["REIN_EFFECTIVE_DATE"].ToString()));
					this.txtREIN_IST_SPLIT.Text=oDr["REIN_IST_SPLIT"].ToString();
					//Reinsurance Contract Type		
					
					//ClsCommon.SelectValueinDDL(this.cmbREIN_STATE,"India");
					//ClsCommon.SelectValueinDDL(this.cmbREIN_LINE_OF_BUSINESS,oDr["REIN_LINE_OF_BUSINESS"].ToString());
					//ClsCommon.SelectValueinDDL(this.cmbREIN_COVERAGE,oDr["REIN_COVERAGE"]);
					//ClsCommon.SelectValueinDDL(this.cmbPOLICY_TYPE,oDr["POLICY_TYPE"]);
					hidCOVERAGE.Value =  oDr["REIN_COVERAGE"].ToString();
					hid1stCOVERAGE.Value =  oDr["REIN_IST_SPLIT_COVERAGE"].ToString();
					hid2ndCOVERAGE.Value =  oDr["REIN_2ND_SPLIT_COVERAGE"].ToString();
					hidPOLICY.Value = oDr["POLICY_TYPE"].ToString(); 
					//ClsCommon.SelectValueinDDL(this.cmbREIN_IST_SPLIT_COVERAGE,oDr["REIN_IST_SPLIT_COVERAGE"]);
					//ClsCommon.SelectValueinDDL(this.cmbREIN_2ND_SPLIT_COVERAGE,oDr["REIN_2ND_SPLIT_COVERAGE"]);

					//this.cmbREIN_STATE.SelectedValue=
					this.txtREIN_2ND_SPLIT.Text=oDr["REIN_2ND_SPLIT"].ToString();
					//this.txtREIN_TIV_GROUP_CODE.Text=oDr["REIN_TIV_GROUP_CODE"].ToString();

                   if(oDr["IS_ACTIVE"].ToString()=="Y")
                    {
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText("Y");
						
						
                    }
                    if(oDr["IS_ACTIVE"].ToString()=="N")
                    {
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText("N");
						
						
                    }
					this.hidLOB.Value=oDr["REIN_LINE_OF_BUSINESS"].ToString();
					this.hidState.Value=oDr["REIN_STATE"].ToString();
					
				}
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
			finally{}
		}
		
		
		# endregion G E T   D A T A   F O R   E D I T   M O D E
		
		

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
			this.cmbREIN_LINE_OF_BUSINESS.SelectedIndexChanged += new System.EventHandler(this.cmbREIN_LINE_OF_BUSINESS_SelectedIndexChanged);
			this.cmbREIN_STATE.SelectedIndexChanged += new System.EventHandler(this.cmbREIN_STATE_SelectedIndexChanged);		
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
		
		private void cmbREIN_LINE_OF_BUSINESS_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(cmbREIN_STATE.Items.Count > 0)
				cmbREIN_STATE.Items.Clear();
			DataTable dt = Cms.CmsWeb.ClsFetcher.ActiveState;
			this.cmbREIN_STATE.DataSource		= dt;
			cmbREIN_STATE.DataTextField	= "STATE_NAME";
			cmbREIN_STATE.DataValueField	= "STATE_ID";
			cmbREIN_STATE.DataBind();
				
			//ListItem L1 = cmbREIN_LINE_OF_BUSINESS.Items.FindByValue("4");
			if(cmbREIN_LINE_OF_BUSINESS.Items[cmbREIN_LINE_OF_BUSINESS.SelectedIndex].Text.ToUpper()!="WATERCRAFT")
			{//	Remove Wisconsin other than Watercraft LOB					
				cmbREIN_STATE.Items.Remove(cmbREIN_STATE.Items.FindByValue("49"));
				
			}
           
			
			cmbREIN_STATE.Items.Insert(0,"");
			cmbREIN_STATE.SelectedIndex = cmbREIN_STATE.Items.IndexOf(cmbREIN_STATE.Items.FindByValue(hidState.Value));

			if(this.cmbREIN_STATE.SelectedValue!="")
			{
				StateChange();

			}

			SetFocus("cmbREIN_LINE_OF_BUSINESS");
			this.hidStateChange.Value="1";
		}

		private void cmbREIN_STATE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.cmbREIN_LINE_OF_BUSINESS.SelectedValue!="")
			{
				StateChange();

			}
			SetFocus("cmbREIN_STATE");
			this.hidStateChange.Value="1";
		}

		
		
		public void StateChange()
		{
			objSplit=new ClsSplit();	
			DataTable SplitDT = new DataTable();
			if(this.cmbREIN_STATE.SelectedValue =="" && this.cmbREIN_LINE_OF_BUSINESS.SelectedValue == "")
			{
				SplitDT=objSplit.GetCoverageDesc(this.hidState.Value,this.hidLOB.Value).Tables[0];
			}
			else
			{
				SplitDT=objSplit.GetCoverageDesc(this.cmbREIN_STATE.SelectedValue,this.cmbREIN_LINE_OF_BUSINESS.SelectedValue).Tables[0];
			}
				
			//if(SplitDT.Rows.Count >0)
			//{
			this.cmbFROMREIN_1ST_SPLIT_COVERAGE.DataSource		= SplitDT;
			cmbFROMREIN_1ST_SPLIT_COVERAGE.DataTextField	= "COV_DES";
			cmbFROMREIN_1ST_SPLIT_COVERAGE.DataValueField	= "COV_ID";
			cmbFROMREIN_1ST_SPLIT_COVERAGE.DataBind();
			//cmbREIN_IST_SPLIT_COVERAGE.Items[0].Selected=true;

			this.cmbFROMREIN_2ND_SPLIT_COVERAGE.DataSource		= SplitDT;
			cmbFROMREIN_2ND_SPLIT_COVERAGE.DataTextField	= "COV_DES";
			cmbFROMREIN_2ND_SPLIT_COVERAGE.DataValueField	= "COV_ID";
			cmbFROMREIN_2ND_SPLIT_COVERAGE.DataBind();

			this.cmbFROMREIN_COVERAGE.DataSource		= SplitDT;
			cmbFROMREIN_COVERAGE.DataTextField	= "COV_DES";
			cmbFROMREIN_COVERAGE.DataValueField	= "COV_ID";
			cmbFROMREIN_COVERAGE.DataBind();
				
			int stateId =0;
			if(this.cmbREIN_STATE.SelectedValue !="" )
			{
				stateId=Convert.ToInt32(cmbREIN_STATE.SelectedValue);
			}
			else
			{
				stateId=Convert.ToInt32(hidState.Value);
			}
			int selVal =0;
			if(this.cmbREIN_LINE_OF_BUSINESS.SelectedValue != "")
			{
				selVal=int.Parse(cmbREIN_LINE_OF_BUSINESS.SelectedValue);
			}
			else
			{
				selVal=int.Parse(hidLOB.Value);
			}
			if (selVal == (int)enumLOB.HOME) //For Homeowners.
			{	
				if(stateId == (int)enumState.Michigan)
				{
					cmbFROMPOLICY_TYPE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("HOPTPM");
				}
				else
				{
					cmbFROMPOLICY_TYPE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("HOPTYP");
				}
				cmbFROMPOLICY_TYPE.DataTextField="LookupDesc";
				cmbFROMPOLICY_TYPE.DataValueField="LookupID";
				cmbFROMPOLICY_TYPE.DataBind();
				hidShowHide.Value = "1";
			}
					
			else if(selVal == (int)enumLOB.REDW) 	// For Rental Dwelling.
			{
				if(stateId == (int)enumState.Michigan)
				{
					cmbFROMPOLICY_TYPE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RTPTYP");
			
				}
				else
				{
					cmbFROMPOLICY_TYPE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RTPTYI");  
				}
							
				cmbFROMPOLICY_TYPE.DataTextField="LookupDesc";
				cmbFROMPOLICY_TYPE.DataValueField="LookupID";
				cmbFROMPOLICY_TYPE.DataBind();
				hidShowHide.Value = "1";
			}	
			else
			{
				//				trCapPolicy.Visible = true;
				//				trPolicy_type.Visible = false;
				hidShowHide.Value ="0";
			}
			//}
			//SetDropdownList();
	
		}


		/*private void PopulateCoverage()
		{
			objSplit=new ClsSplit();
			DataTable SplitDT=objSplit.PopulateCoverageDesc(this.hidREIN_SPLIT_DEDUCTION_ID.Value).Tables[0];
				
			if(SplitDT.Rows.Count >0)
			{
				this.cmbREIN_IST_SPLIT_COVERAGE.DataSource		= SplitDT;
				cmbREIN_IST_SPLIT_COVERAGE.DataTextField	= "COV_DES";
				cmbREIN_IST_SPLIT_COVERAGE.DataValueField	= "COV_CODE";
				cmbREIN_IST_SPLIT_COVERAGE.DataBind();
				//cmbREIN_IST_SPLIT_COVERAGE.Items[0].Selected=true;

				this.cmbREIN_2ND_SPLIT_COVERAGE.DataSource		= SplitDT;
				cmbREIN_2ND_SPLIT_COVERAGE.DataTextField	= "COV_DES";
				cmbREIN_2ND_SPLIT_COVERAGE.DataValueField	= "COV_CODE";
				cmbREIN_2ND_SPLIT_COVERAGE.DataBind();

				this.cmbREIN_COVERAGE.DataSource		= SplitDT;
				cmbREIN_COVERAGE.DataTextField	= "COV_DES";
				cmbREIN_COVERAGE.DataValueField	= "COV_ID";
				cmbREIN_COVERAGE.DataBind();
			}

		}*/
		
	}
}


