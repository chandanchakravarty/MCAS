/******************************************************************************************
<Author					: - Vijay Arora
<Start Date				: -	22-11-2005
<End Date				: -	
<Description			: - Class for Add / Edit / Delete of WaterCraft Outboard Engine Details.
<Review Date			: - 
<Reviewed By			: - 	
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
//using Cms.Model.Application.Watercrafts;
using Cms.CmsWeb;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.Controls;
using Cms.Model.Policy.Watercraft;
using Cms.BusinessLayer.BlCommon;

namespace Cms.Policies.Aspx.Watercrafts
{
	/// <summary>
	/// Summary description for AddWatercraftEngineDetails.
	/// </summary>
	public class PolicyAddWatercraftEngine : Cms.Policies.policiesbase
	{
		
		#region Page Control Declarationsb
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capENGINE_NO;
		protected System.Web.UI.WebControls.TextBox txtENGINE_NO;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvENGINE_NO;
		protected System.Web.UI.WebControls.Label capYEAR;
		protected System.Web.UI.WebControls.TextBox txtYEAR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvYEAR;
		protected System.Web.UI.WebControls.Label capMAKE;
		protected System.Web.UI.WebControls.TextBox txtMAKE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAKE;
		protected System.Web.UI.WebControls.Label capMODEL;
		protected System.Web.UI.WebControls.TextBox txtMODEL;
		protected System.Web.UI.WebControls.Label capSERIAL_NO;
		protected System.Web.UI.WebControls.TextBox txtSERIAL_NO;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdForm;
	
		protected System.Web.UI.WebControls.Label capHORSEPOWER;
		protected System.Web.UI.WebControls.TextBox txtHORSEPOWER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHORSEPOWER;
		protected System.Web.UI.WebControls.RegularExpressionValidator revHORSEPOWER;
		
		protected System.Web.UI.WebControls.Label capINSURING_VALUE;
		protected System.Web.UI.WebControls.TextBox txtINSURING_VALUE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revINSURING_VALUE;
		protected System.Web.UI.WebControls.Label capASSOCIATED_BOAT;
		protected System.Web.UI.WebControls.DropDownList cmbASSOCIATED_BOAT;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		
		protected System.Web.UI.WebControls.Label capOTHER;
		protected System.Web.UI.WebControls.TextBox txtOTHER;
	
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidENGINE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;

		#endregion
	
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		protected System.Web.UI.WebControls.RangeValidator rngASSOCIATED_BOAT;
		protected System.Web.UI.WebControls.RangeValidator rngYEAR;
		//Defining the business layer class object
		ClsWatercraftEngine  objWatercraftEngine ;
		protected System.Web.UI.WebControls.CustomValidator csvOTHER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidBOATID;
		protected System.Web.UI.WebControls.Label capFUEL_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbFUEL_TYPE;
		//END:*********** Local variables *************
		public string lob="WAT";

		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.HtmlControls.HtmlTable tbl;
		protected System.Web.UI.WebControls.RegularExpressionValidator revENGINE_NO;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSERIAL_NO;
		public string strDelete = "";


		#endregion
		
		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvENGINE_NO.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvYEAR.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvMAKE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvHORSEPOWER.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
						
			rngYEAR.MaximumValue = DateTime.Now.AddYears(1).Year.ToString();
            rngYEAR.MinimumValue = aAppMinYear  ;
			rngYEAR.ErrorMessage					=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("673");
			
			//revYEAR.ValidationExpression			= aRegExpInteger;
			revHORSEPOWER.ValidationExpression		= aRegExpDouble;
			
			revINSURING_VALUE.ValidationExpression	= aRegExpCurrencyformat;

			//revYEAR.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"164");
			revHORSEPOWER.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"10");
			
			revINSURING_VALUE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"11");
			csvOTHER.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"17");
			revENGINE_NO.ErrorMessage					=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
			revENGINE_NO.ValidationExpression			= aRegExpInteger ;
			revSERIAL_NO.ValidationExpression		= aRegExpAlphaNumSpaceStrict;
			revSERIAL_NO.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage("72_0","13" );
																										
		}
		#endregion
		
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			
			/* Modified by Asfa (09-June-2008) - iTrack issue #4071
			txtINSURING_VALUE.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			*/
			txtINSURING_VALUE.Attributes.Add("onBlur","javascript:return InsuringValueMsg();");

			#region SETTING SCREEN ID
			if(GetLOBString()=="WAT")
			{
				//base.ScreenId="72_1_0"; 
				base.ScreenId="246_1_0"; 
				lob="WWEN";
			}
			else if(GetLOBString()=="HOME")
			{
				//base.ScreenId="148_1_0"; 
				base.ScreenId="251_1_0"; 
				lob="HWEN";
			}
			else if(GetLOBString()=="RENT")
			{
				base.ScreenId="166_1_0"; 				
				lob="RWEN";
			}
			else if(GetLOBString()=="UMB")
			{
				base.ScreenId="277_1_0"; 
				lob="UWAT";
			}

			#endregion

			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass			=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass			=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnDelete.CmsButtonClass	=	CmsButtonType.Delete;
			btnDelete.PermissionString	=	gstrSecurityXML;

			
			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Read;
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;


			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.Watercrafts.PolicyAddWatercraftEngine" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["CalledFrom"]!=null && Request.QueryString["CalledFrom"].ToUpper()!="" )
					hidCalledFrom.Value=Request.QueryString["CalledFrom"].ToString();

				GetOldDataXML();
				SetCaptions();

				

				#region "Populate Dropdowns"
			
				//Associated Boat
				if(hidCalledFrom.Value.ToUpper()!="UMB")
					cmbASSOCIATED_BOAT.DataSource =clsWatercraftInformation.FetchPolicyBoatInfo(int.Parse (hidCustomerID.Value), int.Parse (hidPOLICY_ID.Value), int.Parse (hidPOLICY_VERSION_ID.Value ),int.Parse(hidBOATID.Value) );
				else
					cmbASSOCIATED_BOAT.DataSource =clsWatercraftInformation.FetchPolicyUmbrellaBoatInfo(int.Parse (hidCustomerID.Value), int.Parse (hidPOLICY_ID.Value), int.Parse (hidPOLICY_VERSION_ID.Value ),int.Parse(hidBOATID.Value) );				

				cmbASSOCIATED_BOAT.DataTextField	= "Boat";
				cmbASSOCIATED_BOAT.DataValueField	= "Boat_ID";
				cmbASSOCIATED_BOAT.DataBind();
				cmbASSOCIATED_BOAT.Items.Insert(0,"");
				ListItem li=new ListItem();
				li=cmbASSOCIATED_BOAT.Items.FindByValue(hidBOATID.Value);  
				if(li!=null)
				{
					li.Selected=true; 
				}

				cmbASSOCIATED_BOAT.Enabled=false; 

				// Fuel Type
				cmbFUEL_TYPE.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("FTYCD");
				cmbFUEL_TYPE.DataTextField	= "LookupDesc";
				cmbFUEL_TYPE.DataValueField	= "LookupID";
				cmbFUEL_TYPE.DataBind();
				cmbFUEL_TYPE.Items.Insert(0,"");
				cmbFUEL_TYPE.Items.RemoveAt(3);  // Remove item option "None" from LookUp values.

				#endregion 

				#region Set Workflow Control
				SetWorkflow();
				#endregion

                if (hidOldData.Value.ToString() != "0")
                {
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString().Trim());
                }


			}
		} 
		#endregion

		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
				ClsWatercraftEngine objWatercraftEngine = new  ClsWatercraftEngine();
				Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEngineInfo objWatercraftEngineInfo = new Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEngineInfo();
				//base.PopulateModelObject(objWatercraftEngineInfo,hidOldData.Value);
				objWatercraftEngineInfo=GetFormValue();
				objWatercraftEngineInfo.ENGINE_ID = int.Parse (strRowId==""?"0":strRowId);
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
                {
                    objWatercraftEngine.ActivateDeactivatePolWatercraftengine(objWatercraftEngineInfo, "N", "");
                    //btnActivateDeactivate.Text="Activate";
                    //btnActivateDeactivate.Enabled=true;
                    objWatercraftEngineInfo.IS_ACTIVE = "N";
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objWatercraftEngineInfo.IS_ACTIVE.ToString().Trim());
                    lblDelete.Text = ClsMessages.FetchGeneralMessage("41");
                    hidIS_ACTIVE.Value = "N";
                    //tdForm.Attributes.Add("style","display:none");
                    hidFormSaved.Value = "0";
                    GetOldDataXML();
                    //strDelete = "Y";
                    base.OpenEndorsementDetails();
                    btnActivateDeactivate.Enabled = true;
                    ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID", "<script>RefreshWebGrid(1," + hidENGINE_ID.Value + ",true);</script>");
                    // RegisterStartupScript("REFRESHGRID", "<script>parent.strSelectedRecordXML='';parent.RemoveTab(5,parent);parent.RemoveTab(4,parent);parent.RemoveTab(3,parent);parent.RemoveTab(2,parent);RefreshWebGrid('5','1',true,true); </script>");
                }
                else
                {
                    objWatercraftEngine.ActivateDeactivatePolWatercraftengine(objWatercraftEngineInfo, "Y", "");
                    objWatercraftEngineInfo.IS_ACTIVE = "Y";
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objWatercraftEngineInfo.IS_ACTIVE.ToString().Trim());
                    lblDelete.Text = ClsMessages.FetchGeneralMessage("40");
                    hidIS_ACTIVE.Value = "Y";
                    //btnActivateDeactivate.Text="Deactivate";
                    hidFormSaved.Value = "0";
                    GetOldDataXML();
                    base.OpenEndorsementDetails();
                    ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID", "<script>RefreshWebGrid(1," + hidENGINE_ID.Value + ",true);</script>");
                }
				
				
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage("72_0","21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				//ExceptionManager.Publish(ex);
			}
			finally
			{
				//lblMessage.Visible = true;
				lblDelete.Visible=true;
				
			}	
			

		}
		
		
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEngineInfo  GetFormValue()
		{
			//Creating the Model object for holding the New data
			Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEngineInfo objWatercraftEngineInfo = new Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEngineInfo();
			
			objWatercraftEngineInfo.ENGINE_NO=	txtENGINE_NO.Text;

			if(txtYEAR.Text.Trim()!="")
				objWatercraftEngineInfo.YEAR=	int.Parse (txtYEAR.Text.Trim().ToString());
			
			objWatercraftEngineInfo.MAKE=	txtMAKE.Text;
			objWatercraftEngineInfo.MODEL=	txtMODEL.Text;
			objWatercraftEngineInfo.SERIAL_NO=	txtSERIAL_NO.Text;
			objWatercraftEngineInfo.HORSEPOWER=	txtHORSEPOWER.Text;
			
			
			if(txtINSURING_VALUE.Text.Trim()!="")
				objWatercraftEngineInfo.INSURING_VALUE=txtINSURING_VALUE.Text.Trim()==""?0:double.Parse(txtINSURING_VALUE.Text.Trim());

			objWatercraftEngineInfo.ASSOCIATED_BOAT=	int.Parse(hidBOATID.Value);

			if(cmbFUEL_TYPE.SelectedItem!=null)
				objWatercraftEngineInfo.FUEL_TYPE  =	int.Parse (cmbFUEL_TYPE.SelectedItem.Value==""?"0":cmbFUEL_TYPE.SelectedItem.Value);
						
			objWatercraftEngineInfo.OTHER=	txtOTHER.Text;
			objWatercraftEngineInfo.CUSTOMER_ID =int.Parse(hidCustomerID.Value.ToString());
			objWatercraftEngineInfo.POLICY_ID =int.Parse(hidPOLICY_ID.Value.ToString());
			objWatercraftEngineInfo.POLICY_VERSION_ID =int.Parse(hidPOLICY_VERSION_ID.Value.ToString());
			objWatercraftEngineInfo.CREATED_BY = int.Parse(GetUserId());
			objWatercraftEngineInfo.CREATED_DATETIME = DateTime.Now;
			objWatercraftEngineInfo.MODIFIED_BY = int.Parse(GetUserId());
			objWatercraftEngineInfo.LAST_UPDATED_DATETIME = DateTime.Now;

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidENGINE_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objWatercraftEngineInfo;
		}

		private Cms.Model.Policy.Umbrella.ClsWaterCraftEngineInfo  GetFormValueUmb()
		{
			//Creating the Model object for holding the New data
			Cms.Model.Policy.Umbrella.ClsWaterCraftEngineInfo objWatercraftEngineInfo = new Cms.Model.Policy.Umbrella.ClsWaterCraftEngineInfo();
			
			objWatercraftEngineInfo.ENGINE_NO=	txtENGINE_NO.Text;

			if(txtYEAR.Text.Trim()!="")
				objWatercraftEngineInfo.YEAR=	int.Parse (txtYEAR.Text.Trim().ToString());
			
			objWatercraftEngineInfo.MAKE=	txtMAKE.Text;
			objWatercraftEngineInfo.MODEL=	txtMODEL.Text;
			objWatercraftEngineInfo.SERIAL_NO=	txtSERIAL_NO.Text;
			objWatercraftEngineInfo.HORSEPOWER=	txtHORSEPOWER.Text;
			
			
			if(txtINSURING_VALUE.Text.Trim()!="")
				objWatercraftEngineInfo.INSURING_VALUE=txtINSURING_VALUE.Text.Trim()==""?0:double.Parse(txtINSURING_VALUE.Text.Trim());

			objWatercraftEngineInfo.ASSOCIATED_BOAT=	int.Parse(hidBOATID.Value);
			
			objWatercraftEngineInfo.OTHER=	txtOTHER.Text;
			objWatercraftEngineInfo.CUSTOMER_ID =int.Parse(hidCustomerID.Value.ToString());
			objWatercraftEngineInfo.POLICY_ID =int.Parse(hidPOLICY_ID.Value.ToString());
			objWatercraftEngineInfo.POLICY_VERSION_ID =int.Parse(hidPOLICY_VERSION_ID.Value.ToString());
			objWatercraftEngineInfo.CREATED_BY = int.Parse(GetUserId());
			objWatercraftEngineInfo.CREATED_DATETIME = DateTime.Now;
			objWatercraftEngineInfo.MODIFIED_BY = int.Parse(GetUserId());
			objWatercraftEngineInfo.LAST_UPDATED_DATETIME = DateTime.Now;

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidENGINE_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objWatercraftEngineInfo;
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

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
				
				objWatercraftEngine = new  ClsWatercraftEngine();
				Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEngineInfo objWatercraftEngineInfo = new Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEngineInfo();
				Cms.Model.Policy.Umbrella.ClsWaterCraftEngineInfo objUmbrellaWatercraftEngineInfo = new Cms.Model.Policy.Umbrella.ClsWaterCraftEngineInfo();
				strRowId		=	hidENGINE_ID.Value;
				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					
					//Calling the add method of business layer class
					if(hidCalledFrom.Value.ToUpper()!="UMB")
					{
						//Retreiving the form values into model class object
						objWatercraftEngineInfo = GetFormValue();						
						intRetVal = objWatercraftEngine.AddPolicyWaterCraftEngine(objWatercraftEngineInfo);
					}
					else
					{
						//Retreiving the form values into model class object						
						objUmbrellaWatercraftEngineInfo = GetFormValueUmb();						
						intRetVal = objWatercraftEngine.AddPolicyUmbrellaWaterCraftEngine(objUmbrellaWatercraftEngineInfo);
					}

					if(intRetVal>0)
					{
						//hidENGINE_ID.Value = intRetVal.ToString();
						
						if(hidCalledFrom.Value.ToUpper()!="UMB")
						{
							hidENGINE_ID.Value	=	objWatercraftEngineInfo.ENGINE_ID.ToString();
							hidOldData.Value	=	ClsWatercraftEngine.FetchPolicyWatercraftEngineInfoXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value),int.Parse (hidENGINE_ID.Value==""?"0":hidENGINE_ID.Value));							
						}
						else
						{
							hidENGINE_ID.Value	=	objUmbrellaWatercraftEngineInfo.ENGINE_ID.ToString();
							hidOldData.Value	=	ClsWatercraftEngine.FetchPolicyUmbrellaWatercraftEngineInfoXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value),int.Parse (hidENGINE_ID.Value==""?"0":hidENGINE_ID.Value));							
						}
                        if (ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString() != "")////Added By Pradeep Kushwaha on 21-sep-2010
                            btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString());
						lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"12");
						hidFormSaved.Value	=	"1";
						hidIS_ACTIVE.Value	=	"Y";

						//Setting the workflow
						SetWorkflow();

						//Showing the endorsement popup window
						base.OpenEndorsementDetails();
					}             
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"13");
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"14");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
					//GetOldDataXML();
				} // end save case
				else //UPDATE CASE
				{
					

					//Updating the record using business layer class object
					if(hidCalledFrom.Value.ToUpper()!="UMB")
					{
						objWatercraftEngineInfo = GetFormValue();	
						//Creating the Model object for holding the Old data						
						Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEngineInfo objOldWatercraftEngineInfo = new Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEngineInfo();
						//Setting  the Old Page details(XML File containing old details) into the Model Object
						base.PopulateModelObject(objOldWatercraftEngineInfo,hidOldData.Value);

						//Setting those values into the Model object which are not in the page
						objWatercraftEngineInfo.ENGINE_ID = int.Parse (strRowId==""?"0":strRowId);
						intRetVal	= objWatercraftEngine.UpdatePolicyWaterCraftEngine(objOldWatercraftEngineInfo,objWatercraftEngineInfo);
					}
					else
					{
						objUmbrellaWatercraftEngineInfo = GetFormValueUmb();
						//Creating the Model object for holding the Old data
						Cms.Model.Policy.Umbrella.ClsWaterCraftEngineInfo objOldWatercraftEngineInfo = new Cms.Model.Policy.Umbrella.ClsWaterCraftEngineInfo();
						//Setting  the Old Page details(XML File containing old details) into the Model Object
						base.PopulateModelObject(objOldWatercraftEngineInfo,hidOldData.Value);

						//Setting those values into the Model object which are not in the page
						objUmbrellaWatercraftEngineInfo.ENGINE_ID = int.Parse (strRowId==""?"0":strRowId);
						intRetVal	= objWatercraftEngine.UpdatePolicyUmbrellaWaterCraftEngine(objOldWatercraftEngineInfo,objUmbrellaWatercraftEngineInfo);
					}
					if( intRetVal > 0 )			// update successfully performed
					{
						if(hidCalledFrom.Value.ToUpper()!="UMB")
							hidOldData.Value	= ClsWatercraftEngine.FetchPolicyWatercraftEngineInfoXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value),int.Parse (hidENGINE_ID.Value==""?"0":hidENGINE_ID.Value));
						else
							hidOldData.Value	= ClsWatercraftEngine.FetchPolicyUmbrellaWatercraftEngineInfoXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value),int.Parse (hidENGINE_ID.Value==""?"0":hidENGINE_ID.Value));

                        if (ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString() != "")////Added By Pradeep Kushwaha on 21-sep-2010
                            btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString());
						lblMessage.Text		=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"15");
						hidFormSaved.Value	=	"1";

						//Setting the workflow
						SetWorkflow();

						//Showing the endorsement popup window
						base.OpenEndorsementDetails();
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"13");
						hidFormSaved.Value		=	"2";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"14");
						hidFormSaved.Value		=	"2";
					}
					lblMessage.Visible = true;
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"16") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				//ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objWatercraftEngine!= null)
					objWatercraftEngine.Dispose();
			}
		}

		#endregion
		
		private void SetCaptions()
		{
			capENGINE_NO.Text		=	objResourceMgr.GetString("txtENGINE_NO");
			capYEAR.Text			=	objResourceMgr.GetString("txtYEAR");
			capMAKE.Text			=	objResourceMgr.GetString("txtMAKE");
			capMODEL.Text			=	objResourceMgr.GetString("txtMODEL");
			capSERIAL_NO.Text		=	objResourceMgr.GetString("txtSERIAL_NO");
			capHORSEPOWER.Text		=	objResourceMgr.GetString("txtHORSEPOWER");
			
			capINSURING_VALUE.Text	=	objResourceMgr.GetString("txtINSURING_VALUE");
			capASSOCIATED_BOAT.Text	=	objResourceMgr.GetString("cmbASSOCIATED_BOAT");
			capFUEL_TYPE.Text		=	objResourceMgr.GetString("cmbFUEL_TYPE");
			capOTHER.Text			=	objResourceMgr.GetString("txtOTHER");
		}
		
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			int intRetVal;	

			objWatercraftEngine = new  ClsWatercraftEngine();			
			ClsPolicyWatercraftEngineInfo objWatercraftEngineInfo = GetFormValue();						
			objWatercraftEngineInfo.ENGINE_ID = int.Parse (strRowId==""?"0":strRowId);
			intRetVal = objWatercraftEngine.DeletePolicyEngine(objWatercraftEngineInfo);
						
			if(intRetVal>0)
			{
				lblDelete.Text			= Cms.CmsWeb.ClsMessages.GetMessage("G","127");
				hidFormSaved.Value		= "5";
				hidOldData.Value		= "";
				strDelete				= "Y";	
				base.OpenEndorsementDetails();	
			}
			else if(intRetVal == -2)
			{			
				lblDelete.Text			=	ClsMessages.GetMessage(base.ScreenId,"128");
				hidFormSaved.Value		=	"2";
			}
			SetWorkflow();			
		}

		private void GetOldDataXML()
		{
			if (Request.QueryString["boat_id"]!=null && Request.QueryString["boat_id"].ToString()!="")
			{
				hidBOATID.Value = Request.QueryString["boat_id"].ToString();
			}
			//If ENGINE_ID is null then it is add mode else edit
			if (Request.QueryString["ENGINEID"]!=null && Request.QueryString["ENGINEID"].ToString()!="")
			{
				
					hidCustomerID.Value = Request.QueryString["CUSTOMER_ID"].ToString();
					hidPOLICY_ID.Value = GetPolicyID();
					hidPOLICY_VERSION_ID.Value= GetPolicyVersionID();   
					hidENGINE_ID.Value = Request.QueryString["ENGINEID"].ToString();	
					if(hidCalledFrom.Value.ToUpper()!="UMB")						
						hidOldData.Value = ClsWatercraftEngine.FetchPolicyWatercraftEngineInfoXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value),int.Parse (hidENGINE_ID.Value==""?"0":hidENGINE_ID.Value));
					else
						hidOldData.Value = ClsWatercraftEngine.FetchPolicyUmbrellaWatercraftEngineInfoXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value),int.Parse (hidENGINE_ID.Value==""?"0":hidENGINE_ID.Value));

                    if (ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString() != "")////Added By Pradeep Kushwaha on 21-sep-2010
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString());
			}
			else 
			{
				//In case of add we take these keys from the session
				hidCustomerID.Value = Request.QueryString["CUSTOMER_ID"].ToString();
				hidPOLICY_ID.Value = GetPolicyID();
				hidPOLICY_VERSION_ID.Value= GetPolicyVersionID();   		
				hidENGINE_ID.Value ="NEW"; 
				
				//Get new Engine Number
				txtENGINE_NO.Text = ClsWatercraftEngine.GetPolNewWatercraftEngineNumber(int.Parse(hidCustomerID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),hidCalledFrom.Value,int.Parse(hidBOATID.Value)).ToString();
		
				
			}
			
			
		}


		private void SetWorkflow()
		{
			if(base.ScreenId	==	"246_1_0" || base.ScreenId	==	"251_1_0" 
				|| base.ScreenId	==	"166_1_0" || base.ScreenId	==	"72_1_0" || base.ScreenId	==	"277_1_0")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;				
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule="POL";
				if ( hidENGINE_ID .Value != "" && hidENGINE_ID .Value.ToUpper() != "NEW" )
				{
					myWorkFlow.AddKeyValue("ENGINE_ID",hidENGINE_ID.Value);
				}
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		} 
	}
}
