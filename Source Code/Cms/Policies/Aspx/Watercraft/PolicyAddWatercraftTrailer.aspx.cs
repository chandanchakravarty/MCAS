/******************************************************************************************
<Author					: -		Vijay Arora
<Start Date				: -		28-11-20025
<End Date				: -	
<Description			: - 	Class for Add / Edit / Delete for Policy WaterCraft Trailer.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: -		3rd Apr,06
<Modified By			: -		Swastika Gaur
<Purpose				: -		Added Delete and Activate/Deactivate Button
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
	/// Summary description for AddTrailerInformation.
	/// </summary>
	public class PolicyAddWatercraftTrailer : Cms.Policies.policiesbase 
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capTRAILER_NO;
		protected System.Web.UI.WebControls.TextBox txtTRAILER_NO;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTRAILER_NO;
		protected System.Web.UI.WebControls.Label capYEAR;
		protected System.Web.UI.WebControls.TextBox txtYEAR;
		protected System.Web.UI.WebControls.Label capMANUFACTURER;
		protected System.Web.UI.WebControls.TextBox txtMANUFACTURER;
		protected System.Web.UI.WebControls.Label capSERIAL_NO;
		protected System.Web.UI.WebControls.TextBox txtSERIAL_NO;
		protected System.Web.UI.WebControls.Label capINSURED_VALUE;
		protected System.Web.UI.WebControls.Label capASSOCIATED_BOAT;
		protected System.Web.UI.WebControls.DropDownList cmbASSOCIATED_BOAT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTRAILER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.WebControls.RegularExpressionValidator revYEAR;
		protected System.Web.UI.WebControls.Label lblDelete;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvASSOCIATED_BOAT;
            protected System.Web.UI.WebControls.Label capMessages;

		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
                
		protected System.Web.UI.WebControls.TextBox txtINSURED_VALUE;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revINSURED_VALUE;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.WebControls.RangeValidator rngINSURED_VALUE;
		protected System.Web.UI.WebControls.RangeValidator rngYEAR;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;

		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		int Gvar_TRAILER_TYPE= 0;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//protected System.Web.UI.WebControls.CustomValidator csvYEAR;
		protected System.Web.UI.WebControls.RegularExpressionValidator regTRAILER_NO;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINSURED_VALUE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_ID;
		protected System.Web.UI.WebControls.Label capTRAILER_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbTRAILER_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTRAILER_TYPE;
		protected System.Web.UI.WebControls.Label capMODEL;
		protected System.Web.UI.WebControls.TextBox txtMODEL;
		protected System.Web.UI.WebControls.Label capTRAILER_DED_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTRAILERDEDID;
		protected System.Web.UI.HtmlControls.HtmlSelect cmbTRAILER_DED_ID;
		        
		//Defining the business layer class object
		ClsTrailer  objTrailerInformation ;
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
			rfvTRAILER_NO.ErrorMessage			    =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			revYEAR.ErrorMessage                    =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			//revINSURED_VALUE.ErrorMessage           =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			
			rngYEAR.MaximumValue = DateTime.Now.AddYears(1).Year.ToString();
			rngYEAR.ErrorMessage					=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("673");
            
			revYEAR.ValidationExpression            =  aRegExpInteger;
			//revINSURED_VALUE.ValidationExpression= aRegExpDoublePositiveNonZero;
			regTRAILER_NO.ValidationExpression		=  aRegExpInteger;

			regTRAILER_NO.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			rfvINSURED_VALUE.ErrorMessage=   Cms.CmsWeb.ClsMessages.GetMessage("G","554");
			rngINSURED_VALUE.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			rfvTRAILER_TYPE.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"20");
			rfvASSOCIATED_BOAT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("832");
//			csvYEAR.ErrorMessage= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"18");
		}
		#endregion

		public string lob="WAT";
    
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			txtINSURED_VALUE.Attributes.Add("onBlur","this.value=formatCurrency(this.value);"); 
            

			#region SETTING SCREEN ID
			if(GetLOBString()=="WAT")
			{
				base.ScreenId="248_0"; 
				lob="WWTR";
			}
			else if(GetLOBString()=="HOME")
			{
				//base.ScreenId="150_0"; 
				base.ScreenId="253_0"; 
				lob="HWTR";
			}
			else if(GetLOBString()=="RENT")
			{
				base.ScreenId="168_0"; 				
				lob="RWTR";
			}
			#endregion

			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	        =	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass	        =	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnDelete.CmsButtonClass		=	CmsButtonType.Delete;
			btnDelete.PermissionString		=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	= Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	= gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.Watercrafts.PolicyAddWatercraftTrailer"  ,System.Reflection.Assembly.GetExecutingAssembly());
			PopulateHiddenFields();
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");	
			//Added by Asfa
			if (Request.Form["__EVENTTARGET"] == "TRAILER_TYPE_Changed")
			{   
				cmbTRAILER_TYPE_SelectedIndexChanged();				
			}

			if(!Page.IsPostBack)
			{
				if(Request.QueryString["CalledFrom"]!=null )
					hidCalledFrom.Value=Request.QueryString["CalledFrom"].ToString();
				if(Request.QueryString["TRAILERID"]!=null)
					GenerateXML(int.Parse(Request.QueryString["TRAILERID"].ToString()));
				if((Request.QueryString["TRAILERID"]!=null && Request.QueryString["TRAILERID"].ToString().Length>0))
				{
					btnDelete.Visible=true;
					btnActivateDeactivate.Visible=true;
				}
				else
				{
					btnDelete.Visible=false;
					btnActivateDeactivate.Visible=false;
				}
				ClsTrailer  objTrailerInformation = new ClsTrailer();
				txtTRAILER_NO.Text = objTrailerInformation.GetPolNewWatercraftTrailerNumber(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value)).ToString();
		    	SetCaptions();
				FillCombo();
				//FillPolicyTrailerDedCombo(Gvar_TRAILER_TYPE); //Added by asfa:
				FillPolicyTrailerDedCombo(); //Added by asfa:
				PopulateTrailerType();
				#region Set Workflow Control
				SetWorkflow();
				#endregion
                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString());
			}

		}

		private void PopulateTrailerType()
		{
			cmbTRAILER_TYPE.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupForwaterCraftType("TRTCD",null,"lookup_value_desc");
			cmbTRAILER_TYPE.DataTextField	= "LookupDesc";
			cmbTRAILER_TYPE.DataValueField	= "LookupID";
			cmbTRAILER_TYPE.DataBind();
			cmbTRAILER_TYPE.Items.Insert(0,"");
			
////
////			//Display only trailer types
////			string strTrailer="";			
////			int i;
////			
////			for(int count = 0; count < cmbTRAILER_TYPE.Items.Count;count++)
////			{
////				strTrailer=cmbTRAILER_TYPE.Items[count].Text.ToString().ToUpper().Trim();
////				if(!(strTrailer.StartsWith("TRAILER") || strTrailer.EndsWith("TRAILER")|| strTrailer.EndsWith("TRAILERS")))
////				{
////					cmbTRAILER_TYPE.Items.RemoveAt(count);
////					count--;
////				}
////			}
		
	}



		private void FillCombo()
		{
			ClsTrailer objTrailerInfo=new ClsTrailer ();  
			clsWatercraftInformation objWatercraftInfo=new clsWatercraftInformation();
			try
			{
				DataSet ds=new DataSet(); 
				ds=clsWatercraftInformation.FetchPolicyBoatInfo(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value));
				if(ds.Tables[0].Rows.Count>0 )
				{
					cmbASSOCIATED_BOAT.DataSource = ds;
					cmbASSOCIATED_BOAT.DataValueField = "BOAT_ID";
					cmbASSOCIATED_BOAT.DataTextField="BOAT";
					cmbASSOCIATED_BOAT.DataBind(); 
					cmbASSOCIATED_BOAT.Items.Insert(0,"");
				}


			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"13") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";                             
			}
			finally
			{
				if(objTrailerInfo!= null)
					objTrailerInfo.Dispose();
			}  
		}
		

		private void FillPolicyTrailerDedCombo()
		{
			try
			{
				cmbTRAILER_DED_ID.Items.Clear();
				DataSet Tempds=new DataSet(); 
				string var_TRAILER_TYPE = Gvar_TRAILER_TYPE==11761?"JS":"O";
				ClsTrailer objTralier=new ClsTrailer();
				DataSet ds=new DataSet(); 
				ds=clsWatercraftInformation.FetchPolicyTrailerDed(int.Parse(hidPOLICY_ID.Value),int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),Gvar_TRAILER_TYPE);
				ds=objTralier.GetDeductToRemoveBasedOnType(var_TRAILER_TYPE,ds);
				DateTime AppEffectiveDate=Convert.ToDateTime(ds.Tables[0].Rows[0]["APP_EFF_DATE"].ToString());
				ClsVehicleCoverages.BindDropDown(cmbTRAILER_DED_ID,ds.Tables[1].DefaultView,"Limit_1_Display","LIMIT_DEDUC_ID",AppEffectiveDate);
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"13") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value	=	"2";                             
			}
			finally
			{

			}  
		}

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private Cms.Model.Policy.Watercraft.ClsPolicyWatercraftTrailerInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			Cms.Model.Policy.Watercraft.ClsPolicyWatercraftTrailerInfo objTrailerInfo = new Cms.Model.Policy.Watercraft.ClsPolicyWatercraftTrailerInfo();

			objTrailerInfo.TRAILER_NO=	txtTRAILER_NO.Text=="" ? 0 : int.Parse(txtTRAILER_NO.Text) ;
			objTrailerInfo.YEAR=	txtYEAR.Text=="" ? 0 : int.Parse(txtYEAR.Text);
            objTrailerInfo.MODEL=	txtMODEL.Text;
			objTrailerInfo.MANUFACTURER=	txtMANUFACTURER.Text;
			objTrailerInfo.SERIAL_NO=	txtSERIAL_NO.Text;
            
			objTrailerInfo.INSURED_VALUE=	txtINSURED_VALUE.Text==""?0.0 :double.Parse(txtINSURED_VALUE.Text);
			objTrailerInfo.ASSOCIATED_BOAT=	cmbASSOCIATED_BOAT.SelectedValue==""?0: int.Parse(cmbASSOCIATED_BOAT.SelectedValue) ;
			if(cmbTRAILER_TYPE.SelectedItem!=null)
				objTrailerInfo.TRAILER_TYPE=Convert.ToInt32(cmbTRAILER_TYPE.SelectedItem.Value);
			if(cmbTRAILER_DED_ID.Value !="")
			{
				objTrailerInfo.TRAILER_DED_ID =Convert.ToInt32(cmbTRAILER_DED_ID.Items[cmbTRAILER_DED_ID.SelectedIndex].Value);
				int index_of_dash = cmbTRAILER_DED_ID.Items[cmbTRAILER_DED_ID.SelectedIndex].Text.IndexOf("-");
			
				if(index_of_dash != -1)
				{
					string str= cmbTRAILER_DED_ID.Items[cmbTRAILER_DED_ID.SelectedIndex].Text;
					string str_TRAILER_DED = str.Substring(0,index_of_dash);
					string str_TRAILER_DED_AMOUNT_TEXT = str.Substring(index_of_dash);
				
					objTrailerInfo.TRAILER_DED = Convert.ToDouble(str_TRAILER_DED);
					objTrailerInfo.TRAILER_DED_AMOUNT_TEXT = str_TRAILER_DED_AMOUNT_TEXT;
				}
				else
				{
					objTrailerInfo.TRAILER_DED = Convert.ToDouble(cmbTRAILER_DED_ID.Items[cmbTRAILER_DED_ID.SelectedIndex].Text);
					objTrailerInfo.TRAILER_DED_AMOUNT_TEXT = "";
				}
			}
			objTrailerInfo.POLICY_ID = hidPOLICY_ID.Value==""?0:int.Parse(hidPOLICY_ID.Value);
			objTrailerInfo.POLICY_VERSION_ID = hidPOLICY_VERSION_ID.Value==""?0:int.Parse(hidPOLICY_VERSION_ID.Value);
			objTrailerInfo.CUSTOMER_ID = hidCUSTOMER_ID.Value==""?0:int.Parse(hidCUSTOMER_ID.Value);
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidTRAILER_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objTrailerInfo;
		}
		#endregion


		/// <summary>
		/// populating hidden fields
		/// </summary>
		private void PopulateHiddenFields()
		{
			hidCUSTOMER_ID.Value    = GetCustomerID().ToString();
			hidPOLICY_ID.Value         = GetPolicyID().ToString();
			hidPOLICY_VERSION_ID.Value = GetPolicyVersionID().ToString();
		}


		/// <summary>
		/// Fetching data from app_home_rating_info and saving in hidOldData hidden fields
		/// </summary>
		private void GenerateXML(int trailer_id)
		{
			ClsTrailer objTrailerInfo=new ClsTrailer();  
			try
			{
				DataSet ds=new DataSet(); 
				ds=objTrailerInfo.FetchPolicyWaterCraftTrailer(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),trailer_id);
				if(ds.Tables[0].Rows.Count>0 )
				{
					hidOldData.Value=Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(ds.Tables[0]);
					Gvar_TRAILER_TYPE=int.Parse(ds.Tables[0].Rows[0]["TRAILER_TYPE"].ToString());
				}
					//hidOldData.Value=ds.GetXml(); 

			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"13") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";                             
			}
			finally
			{
				if(objTrailerInfo!= null)
					objTrailerInfo.Dispose();
			}  
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
				objTrailerInformation = new  ClsTrailer();

				//Retreiving the form values into model class object
				Cms.Model.Policy.Watercraft.ClsPolicyWatercraftTrailerInfo objTrailerInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objTrailerInfo.CREATED_BY = int.Parse(GetUserId());
					objTrailerInfo.CREATED_DATETIME = DateTime.Now;
					objTrailerInfo.IS_ACTIVE = "Y";
					//Calling the add method of business layer class
					intRetVal = objTrailerInformation.AddPolicyWaterCraftTrailer(objTrailerInfo);
					

					if(intRetVal>0)
					{
						hidTRAILER_ID.Value     = objTrailerInfo.TRAILER_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"14");
						hidFormSaved.Value		=	"1";
						hidIS_ACTIVE.Value      = "Y";
						GenerateXML(int.Parse(hidTRAILER_ID.Value));
						SetWorkflow();
						btnDelete.Visible=true;
						btnActivateDeactivate.Visible=true;
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objTrailerInfo.IS_ACTIVE.ToString().Trim());

						//Showing the endorsement popup window
						base.OpenEndorsementDetails();
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"15");
						hidFormSaved.Value			=		"2";
						
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"17");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					Cms.Model.Policy.Watercraft.ClsPolicyWatercraftTrailerInfo  objOldTrailerInfo;
					objOldTrailerInfo = new Cms.Model.Policy.Watercraft.ClsPolicyWatercraftTrailerInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldTrailerInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
                    
					objTrailerInfo.TRAILER_ID = int.Parse(strRowId);
					objTrailerInfo.MODIFIED_BY = int.Parse(GetUserId());
					objTrailerInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					objTrailerInfo.IS_ACTIVE = hidIS_ACTIVE.Value;
					hidTRAILER_ID.Value     = objTrailerInfo.TRAILER_ID.ToString();


					//Updating the record using business layer class object
					intRetVal	= objTrailerInformation.UpdatePolicyWaterCraftTrailer(objOldTrailerInfo,objTrailerInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"16");
						hidFormSaved.Value		=	"1";
						GenerateXML(int.Parse(hidTRAILER_ID.Value));
						SetWorkflow();
						//Showing the endorsement popup window
						base.OpenEndorsementDetails();
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"15");
						hidFormSaved.Value		=	"2";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"17");
						hidFormSaved.Value		=	"2";
						GenerateXML(int.Parse(hidTRAILER_ID.Value));
					}
					lblMessage.Visible = true;
				}
				//SetWorkflow();
				
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
				if(objTrailerInformation!= null)
					objTrailerInformation.Dispose();
			}
            
		}
		#endregion

		private void SetCaptions()
		{
			capTRAILER_NO.Text		=		objResourceMgr.GetString("txtTRAILER_NO");
			capYEAR.Text			=		objResourceMgr.GetString("txtYEAR");
            capMODEL.Text			=		objResourceMgr.GetString("txtMODEL");
			capMANUFACTURER.Text	=		objResourceMgr.GetString("txtMANUFACTURER");
			capSERIAL_NO.Text		=		objResourceMgr.GetString("txtSERIAL_NO");
            capINSURED_VALUE.Text	=		objResourceMgr.GetString("txtINSURED_VALUE");
			capASSOCIATED_BOAT.Text	=		objResourceMgr.GetString("cmbASSOCIATED_BOAT");
			capTRAILER_TYPE.Text	=		objResourceMgr.GetString("cmbTRAILER_TYPE");
			//Added by asfa
			capTRAILER_DED_ID.Text	=	objResourceMgr.GetString("cmbTRAILER_DED_ID");
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
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}

		#endregion


		private void SetWorkflow()
		{//Added the ScreenID for Trailer
			if(base.ScreenId	==	"248_0" || base.ScreenId	==	"253_0" || base.ScreenId	==	"168_0" )
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule="POL";
				
				if ( Request.QueryString["TRAILERID"] != null )
				{
					myWorkFlow.AddKeyValue("TRAILER_ID",Request.QueryString["TRAILERID"]);
				}
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
				//------------------------
				//FillPolicyTrailerDedCombo(Gvar_TRAILER_TYPE);
				//FillPolicyTrailerDedCombo();
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
				ClsTrailer objTrailerInformation = new  ClsTrailer();
				Cms.Model.Policy.Watercraft.ClsPolicyWatercraftTrailerInfo objTrailerInfo = GetFormValue();
				objTrailerInfo.TRAILER_ID=int.Parse(hidTRAILER_ID.Value);

				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{		 			
						
					objTrailerInformation.ActivateDeactivatePolTrailer(objTrailerInfo,"N");
                    objTrailerInfo.IS_ACTIVE = "N";	
					btnActivateDeactivate.Text=ClsMessages.FetchActivateDeactivateButtonsText(objTrailerInfo.IS_ACTIVE.ToString().Trim());	
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
					base.OpenEndorsementDetails();	
					ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID","<script>RefreshWebGrid(1," + hidTRAILER_ID.Value + ",true);</script>");
					trBody.Attributes.Add("style","display:none");
						
				}
				else
				{									
					
					objTrailerInformation.ActivateDeactivatePolTrailer(objTrailerInfo,"Y");
						lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
                    objTrailerInfo.IS_ACTIVE = "Y";
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objTrailerInfo.IS_ACTIVE.ToString().Trim());
					base.OpenEndorsementDetails();	
					
				}
				hidFormSaved.Value			=	"0";
				//Generating the XML again
			     GenerateXML(int.Parse(hidTRAILER_ID.Value));
////				DataSet ds=new DataSet(); 
////				ds=objTrailerInformation.FetchPolicyWaterCraftTrailer(int.Parse(hidAPP_ID.Value),int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidAPP_VERSION_ID.Value),int.Parse(hidTRAILER_ID.Value));
////				if(ds.Tables[0].Rows.Count>0 )
////					hidOldData.Value=Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(ds.Tables[0]);
			ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID","<script>RefreshWebGrid(1," + hidTRAILER_ID.Value + ",true);</script>");
			
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
        //        hidIS_ACTIVE.Value = hidIS_ACTIVE.Value.Trim();
        //        btnActivateDeactivate.Visible = true;
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
        //            btnActivateDeactivate.Visible = false;
        //    }
        //    catch (Exception objEx)
        //    {
        //        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
        //    }
        //}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			int intRetVal;
			ClsTrailer objTrailerInformation = new  ClsTrailer();
			//Cms.Model.Policy.Watercraft.ClsPolicyWatercraftTrailerInfo objTrailerInfo = GetFormValue();
			Cms.Model.Policy.Watercraft.ClsPolicyWatercraftTrailerInfo objTrailerInfo = new Cms.Model.Policy.Watercraft.ClsPolicyWatercraftTrailerInfo();
			
			objTrailerInfo = GetFormValue();
			//base.PopulateModelObject(objTrailerInfo,hidOldData.Value);
			objTrailerInfo.MODIFIED_BY=int.Parse(GetUserId());
			
			if(hidTRAILER_ID.Value!=null && hidTRAILER_ID.Value!="")
				objTrailerInfo.TRAILER_ID=int.Parse(hidTRAILER_ID.Value);
			intRetVal = objTrailerInformation.DeletePolTrailer(objTrailerInfo);
						
			if(intRetVal>0)
			{
				lblDelete.Text			= Cms.CmsWeb.ClsMessages.GetMessage("G","127");
				hidFormSaved.Value = "5";
				hidOldData.Value = "";
				base.OpenEndorsementDetails();	
				trBody.Attributes.Add("style","display:none");
			}
			else if(intRetVal == -1)
			{
			
				lblDelete.Text			=	ClsMessages.GetMessage(base.ScreenId,"128");
				hidFormSaved.Value		=	"2";
			}
			SetWorkflow();
			lblDelete.Visible = true;
		}
		//Added by asfa:
		private void cmbTRAILER_TYPE_SelectedIndexChanged()
		{
			if(cmbTRAILER_TYPE.SelectedIndex >0)
			{
				Gvar_TRAILER_TYPE = Convert.ToInt32(cmbTRAILER_TYPE.SelectedValue);
				//FillPolicyTrailerDedCombo(Convert.ToInt32(cmbTRAILER_TYPE.SelectedValue)); 
				FillPolicyTrailerDedCombo(); 
				hidFormSaved.Value			=	"2";   
                SetFocus("cmbTRAILER_TYPE");          
			}
		}

	}
}
