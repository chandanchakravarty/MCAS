/******************************************************************************************
<Author					: - Vijay Arora
<Start Date				: -	22-11-2005
<End Date				: -	
<Description			: - Class for Add / Edit / Delete of Policy WaterCraft Equipment Details.
<Review Date			: - 
<Reviewed By			: - 	

Modification History
<Modified Date			: -		4th Apr,06
<Modified By			: -		Swastika Gaur
<Purpose				: -		Added Delete and Activate/Deactivate Button

<Modified Date			: - 15/05/2006	
<Modified By			: - RPSINGH
<Purpose				: - Changing Equipent Type to Description
						  - Adding new mandatory Field Equipment Type 
						  
<Modified Date			: - 24/05/2006	
<Modified By			: - RPSINGH
<Purpose				: - Removing the functionality of 
							"No Boat Available. Click here to add boats." 
							as boats are not linked with equipments from now.
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
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.BusinessLayer.BlApplication;
//using Cms.Model.Application.Watercrafts;
using System.Resources;
using System.Reflection;

namespace Cms.Policies.Aspx.Watercrafts
{
	/// <summary>
	/// Summary description for AddEquipmentGenInformation.
	/// </summary>
	public class PolicyWatercraftAddEquipment : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Label capOTHER_DESCRIPTION;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capEQP_TYP;
		protected System.Web.UI.WebControls.Label capEQUIP_NO;
		protected System.Web.UI.WebControls.TextBox txtEQUIP_NO;
		protected System.Web.UI.WebControls.TextBox txtOTHER_DESCRIPTION;
		protected System.Web.UI.WebControls.DropDownList cmbEQUIPMENT_TYPE;
		protected System.Web.UI.WebControls.Label capEQUIP_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbEQUIP_TYPE;
		protected System.Web.UI.WebControls.Label capSHIP_TO_SHORE;
		protected System.Web.UI.WebControls.DropDownList cmbSHIP_TO_SHORE;
		protected System.Web.UI.WebControls.Label capYEAR;
		protected System.Web.UI.WebControls.TextBox txtYEAR;
		protected System.Web.UI.WebControls.Label capMAKE;
		protected System.Web.UI.WebControls.TextBox txtMAKE;
		protected System.Web.UI.WebControls.Label capMODEL;
		protected System.Web.UI.WebControls.TextBox txtMODEL;
		protected System.Web.UI.WebControls.Label capSERIAL_NO;
		protected System.Web.UI.WebControls.TextBox txtSERIAL_NO;
        protected System.Web.UI.WebControls.Label capEQUIP_AMOUNT;
		protected System.Web.UI.WebControls.Label capINSURED_VALUE;
		protected System.Web.UI.WebControls.TextBox txtINSURED_VALUE;
		//protected System.Web.UI.WebControls.Label capASSOCIATED_BOAT;
		//protected System.Web.UI.WebControls.DropDownList cmbASSOCIATED_BOAT;
		protected System.Web.UI.WebControls.DropDownList cmbEQUIP_AMOUNT;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEQUIP_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvcmbEQUIP_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvOTHER_DESCRIPTION; 

		protected System.Web.UI.WebControls.RegularExpressionValidator revEQUIP_NO;
		protected System.Web.UI.WebControls.RegularExpressionValidator revYEAR;
        
		protected System.Web.UI.WebControls.RangeValidator rngINSURED_VALUE;
		protected System.Web.UI.WebControls.CustomValidator csvYEAR;
		protected System.Web.UI.HtmlControls.HtmlTableRow trform;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		public string lob="WAT";
		public string calledFrom="";
		public string pageFrom="";


		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEQUIP_NO;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvASSOCIATED_BOAT;		
		protected System.Web.UI.WebControls.Label lblNOBOAT;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;        
        
		//Defining the business layer class object
		ClsWatercraftEquipment  objWatercraftEquipment ;
		//END:*********** Local variables *************

		#endregion

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			revEQUIP_NO.ValidationExpression=aRegExpInteger ;
			revYEAR.ValidationExpression=aRegExpInteger ;
			csvYEAR.ErrorMessage= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			revYEAR.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			revEQUIP_NO.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
            
			rngINSURED_VALUE.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			rfvEQUIP_NO.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			//rfvASSOCIATED_BOAT.ErrorMessage= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");

			rfvOTHER_DESCRIPTION.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("550");
			rfvcmbEQUIP_TYPE.ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("750");
		
		}
		public int noBoat=1;
		#endregion
    
		private void Page_Load(object sender, System.EventArgs e)
		{			
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			txtINSURED_VALUE.Attributes.Add("onBlur","this.value=formatCurrency(this.value);"); 
         
			#region SETTING SCREEN ID
			if(GetLOBString()=="WAT")
			{
				base.ScreenId="249_0"; 
				lob="WWTE";
				calledFrom="WAT";
				pageFrom="WWTE";
			}
			else if(GetLOBString()=="HOME")
			{
				//base.ScreenId="151_0"; 
				base.ScreenId="254_0"; 
				lob="HWTE";
				calledFrom="WAT";
				pageFrom="HWTE";
			}
			else if(GetLOBString()=="RENT")
			{
				base.ScreenId="169_0"; 				
				lob="RWTE";
				calledFrom="WAT";
				pageFrom="RWTE";
			}
			#endregion

			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnDelete.CmsButtonClass		= CmsButtonType.Delete;
			btnDelete.PermissionString		= gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.Watercrafts.PolicyWatercraftAddEquipment"  ,System.Reflection.Assembly.GetExecutingAssembly());
			//Added by Uday on 27-Mar-2008 for Itrack # 3955
			//Done for Itrack Issue 6666 on 29 Oct 09
            //if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y") || hidIS_ACTIVE.Value.ToString()=="0")
            //    btnActivateDeactivate.Text="Deactivate";	
            //else
            //    btnActivateDeactivate.Text="Activate";
           
			//
			if(!Page.IsPostBack)
			{
				PopulateHiddenFields();
				if(Request.QueryString["EQUIP_ID"]!=null)
					GenerateXML(int.Parse(Request.QueryString["EQUIP_ID"].ToString()));
				if((Request.QueryString["EQUIP_ID"]!=null && Request.QueryString["EQUIP_ID"].ToString().Length>0))
				{
					btnDelete.Visible=true;
					btnActivateDeactivate.Visible=true;
				}
				else
				{
					btnDelete.Visible=false;
					btnActivateDeactivate.Visible=false;
				}
				SetCaptions();
				FillCombo();
				#region Set Workflow Control
				SetWorkflow();
				#endregion
				objWatercraftEquipment = new  ClsWatercraftEquipment();
				txtEQUIP_NO.Text = objWatercraftEquipment.GetPolNewWatercraftEquipNumber(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value)).ToString();

                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value.ToString().Trim()));


			}

			btnSave.Attributes.Add ("OnClick","setOtherDesc();");

		}

		private void FillCombo()
		{
			clsWatercraftInformation objWatercraftInfo=new clsWatercraftInformation();  
			try
			{
				DataSet ds=new DataSet(); 
				/*
				ds=clsWatercraftInformation.FetchPolicyBoatInfo(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value));
				if(ds.Tables[0].Rows.Count>0 )
				{
					cmbASSOCIATED_BOAT.DataSource = ds;
					cmbASSOCIATED_BOAT.DataValueField = "BOAT_ID";
					cmbASSOCIATED_BOAT.DataTextField="BOAT";
					cmbASSOCIATED_BOAT.DataBind();
					cmbASSOCIATED_BOAT.Items.Insert(0,"");
				}
                */
				cmbEQUIP_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("EQPTYP");
				cmbEQUIP_TYPE.DataValueField= "LookupID";
				cmbEQUIP_TYPE.DataTextField="LookupDesc";
				cmbEQUIP_TYPE.DataBind();
				//RPSINGH - 15 May 2006 - Adding new option 'Others'
				ListItem LI = new ListItem("Others","-1");
				cmbEQUIP_TYPE.Items.Insert(cmbEQUIP_TYPE.Items.Count,LI);//adding others in end
				//End of addition by RPSINGH - 15 May 2006 
				cmbEQUIP_TYPE.Items.Insert(0,"");


				cmbEQUIPMENT_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("EQTYPE");
				cmbEQUIPMENT_TYPE.DataValueField= "LookupID";
				cmbEQUIPMENT_TYPE.DataTextField="LookupDesc";
				cmbEQUIPMENT_TYPE.DataBind(); 

				cmbSHIP_TO_SHORE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("SHPSHT");
				cmbSHIP_TO_SHORE.DataValueField= "LookupID";
				cmbSHIP_TO_SHORE.DataTextField="LookupDesc";
				cmbSHIP_TO_SHORE.DataBind(); 
				cmbSHIP_TO_SHORE.Items.Insert(0,"");
                 
				//Added By Shafi To add Deductible

		    	cmbEQUIP_AMOUNT.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("WCEDD",null,"Y");
			    cmbEQUIP_AMOUNT.DataTextField	= "LookupDesc";
			    cmbEQUIP_AMOUNT.DataBind();
			    cmbEQUIP_AMOUNT.Items.Insert(0,"");

			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"7") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";                             
			}
			finally
			{
				if(objWatercraftInfo!= null)
					objWatercraftInfo.Dispose();
			}  
		}

		/// <summary>
		/// populating hidden fields
		/// </summary>

		private void PopulateHiddenFields()
		{
			hidCUSTOMER_ID.Value    = GetCustomerID().ToString();
			hidPOLICY_ID.Value      = GetPolicyID().ToString();
			hidPOLICY_VERSION_ID.Value = GetPolicyVersionID().ToString();
		}

		/// <summary>
		/// Fetching data to fill the details.
		/// </summary>

		private void GenerateXML(int equip_id)
		{
            
			ClsWatercraftEquipment objWatercraftEquipment=new ClsWatercraftEquipment(); 
			try
			{
				DataSet ds=new DataSet(); 
				ds=objWatercraftEquipment.FetchPolicyWaterCraftEquipmentData(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),equip_id);
				if(ds.Tables[0].Rows.Count>0 )
					hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(ds.Tables[0]);
					//hidOldData.Value=ds.GetXml(); 

			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"7") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";                             
			}
			finally
			{
				if(objWatercraftEquipment!= null)
					objWatercraftEquipment.Dispose();
			}  
		}

		private void SetCaptions()
		{
			capEQUIP_NO.Text						=		objResourceMgr.GetString("txtEQUIP_NO");
			capEQUIP_TYPE.Text						=		objResourceMgr.GetString("cmbEQUIP_TYPE");
			capSHIP_TO_SHORE.Text					=		objResourceMgr.GetString("cmbSHIP_TO_SHORE");
			capYEAR.Text							=		objResourceMgr.GetString("txtYEAR");
			capMAKE.Text							=		objResourceMgr.GetString("txtMAKE");
			capMODEL.Text							=		objResourceMgr.GetString("txtMODEL");
			capSERIAL_NO.Text						=		objResourceMgr.GetString("txtSERIAL_NO");
            
			capINSURED_VALUE.Text					=		objResourceMgr.GetString("txtINSURED_VALUE");
			//capASSOCIATED_BOAT.Text					=		objResourceMgr.GetString("cmbASSOCIATED_BOAT");
			capEQUIP_AMOUNT.Text                    =       objResourceMgr.GetString("cmbEQUIP_AMOUNT"); 

			capEQP_TYP.Text							=		objResourceMgr.GetString("cmbEQP_TYP");
			capOTHER_DESCRIPTION.Text				=		objResourceMgr.GetString("txtOTHER_DESCRIPTION");
		}


		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEquipmentInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEquipmentInfo objWatercraftEquipmentInfo = new Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEquipmentInfo();
			
			objWatercraftEquipmentInfo.EQUIP_NO=	txtEQUIP_NO.Text=="" ? 0 : int.Parse(txtEQUIP_NO.Text) ;
			objWatercraftEquipmentInfo.EQUIP_TYPE=	cmbEQUIP_TYPE.SelectedValue=="" ? 0 : int.Parse(cmbEQUIP_TYPE.SelectedValue);
			objWatercraftEquipmentInfo.SHIP_TO_SHORE=	cmbSHIP_TO_SHORE.SelectedValue=="" ? 0 : int.Parse(cmbSHIP_TO_SHORE.SelectedValue);
			objWatercraftEquipmentInfo.YEAR=	txtYEAR.Text=="" ? 0: int.Parse(txtYEAR.Text);
			objWatercraftEquipmentInfo.MAKE=	txtMAKE.Text;
			objWatercraftEquipmentInfo.MODEL=	txtMODEL.Text;
			objWatercraftEquipmentInfo.SERIAL_NO=	txtSERIAL_NO.Text;            
			objWatercraftEquipmentInfo.INSURED_VALUE=	txtINSURED_VALUE.Text=="" ? 0.0 : double.Parse(txtINSURED_VALUE.Text);;
			//objWatercraftEquipmentInfo.ASSOCIATED_BOAT=	cmbASSOCIATED_BOAT.SelectedValue=="" ? 0 : int.Parse(cmbASSOCIATED_BOAT.SelectedValue) ;
			objWatercraftEquipmentInfo.POLICY_ID =hidPOLICY_ID.Value==""?0:int.Parse(hidPOLICY_ID.Value);
			objWatercraftEquipmentInfo.POLICY_VERSION_ID=hidPOLICY_VERSION_ID.Value==""?0:int.Parse(hidPOLICY_VERSION_ID.Value);
			objWatercraftEquipmentInfo.CUSTOMER_ID=hidCUSTOMER_ID.Value==""?0:int.Parse(hidCUSTOMER_ID.Value);
			objWatercraftEquipmentInfo.EQUIP_AMOUNT=cmbEQUIP_AMOUNT.SelectedValue =="" ? 0.0 : double.Parse(cmbEQUIP_AMOUNT.SelectedValue);

			objWatercraftEquipmentInfo.EQUIPMENT_TYPE =	int.Parse(cmbEQUIPMENT_TYPE.SelectedValue);
			
			//If others is not selected then other desc will be saved blank
			if (objWatercraftEquipmentInfo.EQUIP_TYPE != -1)
				objWatercraftEquipmentInfo.OTHER_DESCRIPTION = "";
			else
				objWatercraftEquipmentInfo.OTHER_DESCRIPTION = txtOTHER_DESCRIPTION.Text;

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidEQUIP_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object1

			return objWatercraftEquipmentInfo;
		}
		#endregion

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
				objWatercraftEquipment = new  ClsWatercraftEquipment();

				//Retreiving the form values into model class object
				Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEquipmentInfo objWatercraftEquipmentInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objWatercraftEquipmentInfo.CREATED_BY = int.Parse(GetUserId());
					objWatercraftEquipmentInfo.CREATED_DATETIME = DateTime.Now;
					objWatercraftEquipmentInfo.IS_ACTIVE = "Y";

					//Calling the add method of business layer class
					intRetVal = objWatercraftEquipment.AddPolicyWaterCraftEquipment(objWatercraftEquipmentInfo);

					if(intRetVal>0)
					{
						hidEQUIP_ID.Value = objWatercraftEquipmentInfo.EQUIP_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"8");
						GenerateXML(int.Parse(hidEQUIP_ID.Value));
						hidFormSaved.Value			=	"1";
						SetWorkflow();
						hidIS_ACTIVE.Value = "Y";
						btnDelete.Visible=true;
						btnActivateDeactivate.Visible=true;
						base.OpenEndorsementDetails();
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objWatercraftEquipmentInfo.IS_ACTIVE.ToString().Trim());

					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"9");
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"10");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEquipmentInfo objOldWatercraftEquipmentInfo = new Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEquipmentInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldWatercraftEquipmentInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objWatercraftEquipmentInfo.EQUIP_ID = int.Parse(strRowId);
					objWatercraftEquipmentInfo.MODIFIED_BY = int.Parse(GetUserId());
					objWatercraftEquipmentInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					objWatercraftEquipmentInfo.IS_ACTIVE = hidIS_ACTIVE.Value;
                    
					//Updating the record using business layer class object
					intRetVal	= objWatercraftEquipment.UpdatePolicyWaterCraftEquipment(objOldWatercraftEquipmentInfo,objWatercraftEquipmentInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"11");
						hidEQUIP_ID.Value       =   objOldWatercraftEquipmentInfo.EQUIP_ID.ToString();
						GenerateXML(int.Parse(hidEQUIP_ID.Value));
						hidFormSaved.Value		=	"1";
						base.OpenEndorsementDetails();
						SetWorkflow();
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9");
						hidFormSaved.Value		=	"2";
						btnDelete.Visible=false;
						btnActivateDeactivate.Visible=false;
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"10");
						hidFormSaved.Value		=	"2";
					}
					lblMessage.Visible = true;
				}
				//SetWorkflow();
				
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"7") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				// ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objWatercraftEquipment!= null)
					objWatercraftEquipment.Dispose();
			}
            
		}
		#endregion


		private void SetWorkflow()
		{//Added ScreenID for the Watercraft Equipment Info.
			if(base.ScreenId == "249_0" || base.ScreenId == "254_0" || base.ScreenId == "169_0")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule="POL";

				 
				if ( hidEQUIP_ID .Value != "" && hidEQUIP_ID .Value != "0")
				{
					myWorkFlow.AddKeyValue("EQUIP_ID",hidEQUIP_ID.Value);
				}
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}

		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
				objWatercraftEquipment = new  ClsWatercraftEquipment();
				Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEquipmentInfo objWatercraftEquipmentInfo = GetFormValue();
			
				objWatercraftEquipmentInfo.EQUIP_ID=int.Parse(hidEQUIP_ID.Value);
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{		 			
						
					objWatercraftEquipment.ActivateDeactivatePolEquipment(objWatercraftEquipmentInfo,"N");
                    objWatercraftEquipmentInfo.IS_ACTIVE = "N";	
					btnActivateDeactivate.Text=ClsMessages.FetchActivateDeactivateButtonsText(objWatercraftEquipmentInfo.IS_ACTIVE.ToString().Trim());	
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
					trform.Attributes.Add("style","display:none");
					ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID","<script>RefreshWebGrid(1," + hidEQUIP_ID.Value + ",true);</script>");
					
						
				}
				else
				{									
					
					objWatercraftEquipment.ActivateDeactivatePolEquipment(objWatercraftEquipmentInfo,"Y");
                    objWatercraftEquipmentInfo.IS_ACTIVE = "Y";
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objWatercraftEquipmentInfo.IS_ACTIVE.ToString().Trim());	
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
					
				}
				hidFormSaved.Value			=	"0";
				base.OpenEndorsementDetails();
				//Generating the XML again
			    GenerateXML(int.Parse(hidEQUIP_ID.Value));
				ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID","<script>RefreshWebGrid(1," + hidEQUIP_ID.Value + ",true);</script>");
			
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				//ExceptionManager.Publish(ex);
			}
			finally
			{
				lblMessage.Visible = true;
			
			}
		}

        //private void SetActivateDeactivate()
        //{
        //    try
        //    {
        //        hidIS_ACTIVE.Value	=	hidIS_ACTIVE.Value.Trim();
        //        btnActivateDeactivate.Visible=true;
        //        if (hidIS_ACTIVE.Value == "N")
        //        {
        //            //Record is deactivate 
        //            btnActivateDeactivate.Text = "Activate";
        //        }
        //        else if (hidIS_ACTIVE.Value == "Y")
        //        {
        //            btnActivateDeactivate.Text = "Deactivate";
        //        }
        //        else
        //            btnActivateDeactivate.Visible=false;
        //    }
        //    catch(Exception objEx)
        //    {
        //        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
        //    }
        //}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			int intRetVal;	

			objWatercraftEquipment = new  ClsWatercraftEquipment();
			//ClsWatercraftEquipmentsInfo objWatercraftEquipmentInfo = GetFormValue();
			Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEquipmentInfo objWatercraftEquipmentInfo = GetFormValue();
			//base.PopulateModelObject(objWatercraftEquipmentInfo,hidOldData.Value);

			objWatercraftEquipmentInfo.MODIFIED_BY=int.Parse(GetUserId());
			
			if(hidEQUIP_ID.Value!=null && hidEQUIP_ID.Value!="")
				objWatercraftEquipmentInfo.EQUIP_ID=int.Parse(hidEQUIP_ID.Value);
			intRetVal = objWatercraftEquipment.DeletePolEquipmentInfo(objWatercraftEquipmentInfo);
						
			if(intRetVal>0)
			{
				lblDelete.Text			= Cms.CmsWeb.ClsMessages.GetMessage("G","127");
				hidFormSaved.Value = "5";
				hidOldData.Value = "";
				trform.Attributes.Add("style","display:none");
				base.OpenEndorsementDetails();
			}
			else if(intRetVal == -1)
			{
			
				lblDelete.Text			=	ClsMessages.GetMessage(base.ScreenId,"128");
				hidFormSaved.Value		=	"2";
			}
			SetWorkflow();
			lblDelete.Visible = true;
		} 
	}
}
