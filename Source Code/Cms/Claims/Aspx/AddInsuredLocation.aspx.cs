/******************************************************************************************
<Author					: -		Vijay Arora
<Start Date				: -		5/2/2006
<End Date				: -	
<Description			: - 	Show the Add Page for Location.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
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
using Cms.CustomException;
using Cms.CmsWeb.WebControls;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Claims;
using Cms.BusinessLayer.BlApplication;
using Cms.CmsWeb;
using Cms.BusinessLayer.BLClaims;
using Cms.CmsWeb.Controls;

namespace Cms.Claims.Aspx
{
	/// <summary>
	/// Show the Add Location Page.
	/// </summary>
	public class AddInsuredLocation : Cms.Claims.ClaimBase
	{

		#region Page controls declaration
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capDESCRIPTION;
		protected System.Web.UI.WebControls.TextBox txtDESCRIPTION;
		protected System.Web.UI.WebControls.CustomValidator csvDESCRIPTION;
		protected System.Web.UI.WebControls.Label capLOC_ADD1;
		protected System.Web.UI.WebControls.TextBox txtLOC_ADD1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOC_ADD1;
		protected System.Web.UI.WebControls.Label capLOC_ADD2;
		protected System.Web.UI.WebControls.TextBox txtLOC_ADD2;
		protected System.Web.UI.WebControls.Label capLOC_CITY;
		protected System.Web.UI.WebControls.TextBox txtLOC_CITY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOC_CITY;
		protected System.Web.UI.WebControls.Label capLOC_COUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbLOC_COUNTRY;
		protected System.Web.UI.WebControls.Label capLOC_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbLOC_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOC_STATE;
		protected System.Web.UI.WebControls.Label capLOC_ZIP;
		protected System.Web.UI.WebControls.TextBox txtLOC_ZIP;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOC_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLOC_ZIP;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.WebControls.Label capLOCATION;
		protected System.Web.UI.WebControls.DropDownList cmbLOCATION;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOCATION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOCATION;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		//Added for Itrack Issue 5833 on 20 July 2009
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.WebControls.Label lblDelete;
		//Added till here
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOC_COUNTRY;
		
		#endregion

		#region Local form variables
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		ClsInsuredLocation objIL = new ClsInsuredLocation();
		private string strRowId, strFormSaved;
		#endregion
		protected System.Web.UI.WebControls.Label capLOCATION_ID;
		protected System.Web.UI.HtmlControls.HtmlTableRow trLOCATION;
		protected System.Web.UI.WebControls.TextBox txtLOCATION_ID;

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{		
	
			this.cmbLOC_COUNTRY.SelectedIndex = int.Parse(aCountry);
						
			#region Setting ScreenId
			base.ScreenId="304_10";
			#endregion

			btnReset.Attributes.Add("onclick","javascript:return ResetTheForm();");
			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass		= CmsButtonType.Write;
			btnReset.PermissionString	= gstrSecurityXML;

			//Added for Itrack Issue 5833 on 20 July 2009
			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;

			btnDelete.CmsButtonClass	=	CmsButtonType.Write;
			btnDelete.PermissionString=	gstrSecurityXML;
			//Added till here

			btnSave.CmsButtonClass		= CmsButtonType.Write;
			btnSave.PermissionString	= gstrSecurityXML;
 
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************

			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddInsuredLocation"  ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				//cmbLOCATION.Attributes.Add("onChange","javascript: return FetchLocationData();");
				GetQueryString();
				GetOldDataXML();
				SetCaptions();
				FillCombo();
				LoadData();

				//Done for Itrack Issue 5833 on 20 July 2009
				if(hidOldData.Value!="")
				{
					btnActivateDeactivate.Enabled = true;
					btnReset.Enabled = false;
					if(Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("IS_ACTIVE",hidOldData.Value) == "Y")
					{
						btnActivateDeactivate.Text = "Deactivate";
					}
					else
					{
						btnActivateDeactivate.Text = "Activate";
					}
				}
				else
				{
					btnActivateDeactivate.Enabled = false;
					btnReset.Enabled = true;
					btnActivateDeactivate.Text = "Deactivate";
				}

				ClsInsuredLocation objIL = new ClsInsuredLocation();
				if(hidLOCATION_ID.Value.ToUpper()!= "0")
				{
					int intReserve = objIL.CheckClaimActivityReserve(int.Parse(GetClaimID()),int.Parse(hidLOCATION_ID.Value),"Rental_Home");
					if(intReserve == 1)
					{
						btnDelete.Visible=false;
					}
					else
					{
						btnDelete.Visible= true;
					}
				}
				else
				{
					btnDelete.Visible= true;
					btnDelete.Enabled = false;
				}
				//Added till here
			}
			//Added till here
			//Following will be done through JavaScript
			//if (Request["INSURED_LOCATION_ID"] != null &&  int.Parse(Request["INSURED_LOCATION_ID"]) > 0 )  
			//	cmbLOCATION.Enabled = false;			

		}//end pageload
		#endregion

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvLOC_ADD1.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvLOC_CITY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvLOC_STATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvLOC_ZIP.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			rfvLOC_COUNTRY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			csvDESCRIPTION.ErrorMessage         = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("442");  
			revLOC_ZIP.ErrorMessage				= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("24");
			revLOC_ZIP.ValidationExpression		= aRegExpZip;
		}
		#endregion

		#region Fill Combo
		private void FillCombo()
		{
			#region "Loading singleton"
			DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
			cmbLOC_COUNTRY.DataSource		= dt;
			cmbLOC_COUNTRY.DataTextField	= "Country_Name";
			cmbLOC_COUNTRY.DataValueField	= "Country_Id";
			cmbLOC_COUNTRY.DataBind();
			//cmbLOC_COUNTRY.Items.Insert(0,"");

			dt = Cms.CmsWeb.ClsFetcher.State; 
			cmbLOC_STATE.DataSource		= dt;
			cmbLOC_STATE.DataTextField	= "State_Name";
			cmbLOC_STATE.DataValueField	= "State_Id";
			cmbLOC_STATE.DataBind();
			cmbLOC_STATE.Items.Insert(0,"");
			#endregion//Loading singleton

			string strVal="",strText="";
			//No need to fill Location dropdown when in edit mode
			if(hidOldData.Value!="" && hidOldData.Value!="0") 
				return;
			DataTable dtTemp = objIL.GetPolicyLocations(int.Parse(hidCLAIM_ID.Value)).Tables[0];
			/*cmbLOCATION.Items.Add(new ListItem("Other","-1"));
			if(hidOldData.Value!="" && hidOldData.Value!="0") 
				return;
			foreach(DataRow dtRow in dtTemp.Rows)
			{
				string sVal = "";
				for(int i=0;i<dtTemp.Columns.Count;i++)
					sVal+= dtRow[i].ToString() + "^";

				sVal=sVal.Substring(0,sVal.Length-1).Trim();

				ListItem lItem = new ListItem(dtRow["LOC_NUM"].ToString(),sVal);
				
				cmbLOCATION.Items.Add(lItem);
			}*/
			/*if(dtTemp!=null && dtTemp.Rows.Count>0)
			{
				cmbLOCATION.DataSource = dtTemp;
				cmbLOCATION.DataTextField = "LOC_NUM";
				cmbLOCATION.DataValueField = "LOCATION_ID";
				cmbLOCATION.DataBind();
				cmbLOCATION.Items.Insert(0,"");
			}*/
			if(dtTemp!=null && dtTemp.Rows.Count>0)
			{
				for(int i=0;i<dtTemp.Rows.Count;i++)
				{
					strText = dtTemp.Rows[i]["LOC_NUM"].ToString()  + "-" +  dtTemp.Rows[i]["ADDRESS_1"].ToString()  + "-" +  dtTemp.Rows[i]["ADDRESS_2"].ToString()  + "-" +  dtTemp.Rows[i]["CITY"].ToString()  + "-" +  dtTemp.Rows[i]["STATE_NAME"].ToString()  + "-" +  dtTemp.Rows[i]["ZIPCODE"].ToString();
					strVal = dtTemp.Rows[i]["LOCATION_ID"].ToString();
					cmbLOCATION.Items.Add(new ListItem(strText, strVal));
				}
				cmbLOCATION.Items.Insert(0,new ListItem("",""));
			}
			else
				trLOCATION.Attributes.Add("style","display:none");
		}
		#endregion

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsInsuredLocationInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsInsuredLocationInfo objILInfo = new ClsInsuredLocationInfo();

			if (txtDESCRIPTION.Text.Trim() != "")
				objILInfo.LOCATION_DESCRIPTION = txtDESCRIPTION.Text.Trim();
						
			if(txtLOC_ADD1.Text.Trim()!="")
				objILInfo.ADDRESS1=	txtLOC_ADD1.Text;
			
			if(txtLOC_ADD2.Text.Trim()!="")
				objILInfo.ADDRESS2=	txtLOC_ADD2.Text;
			
			if(txtLOC_CITY.Text.Trim()!="")
				objILInfo.CITY=	txtLOC_CITY.Text;

			if (cmbLOC_COUNTRY.SelectedItem!=null && cmbLOC_COUNTRY.SelectedItem.Value!="")
				objILInfo.COUNTRY=	int.Parse(cmbLOC_COUNTRY.SelectedItem.Value) ;
			
			if (cmbLOC_STATE.SelectedItem!=null && cmbLOC_STATE.SelectedItem.Value!="")
				objILInfo.STATE=	int.Parse(cmbLOC_STATE.SelectedValue);

			objILInfo.ZIP=	txtLOC_ZIP.Text;

			if(cmbLOCATION.SelectedItem!=null && cmbLOCATION.SelectedItem.Value!="")
				objILInfo.POLICY_LOCATION_ID = int.Parse(cmbLOCATION.SelectedItem.Value);
			objILInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidLOCATION_ID.Value;
			//Returning the model object
			return objILInfo;
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
			this.cmbLOCATION.SelectedIndexChanged += new System.EventHandler(this.cmbLOCATION_SelectedIndexChanged);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);//Added for Itrack Issue 5833 on 20 July 2009
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);//Done for Itrack Issue 5833 on 20 July 2009
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

				//Retreiving the form values into model class object
				ClsInsuredLocationInfo objILInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("0")) //save case
				{
					objILInfo.CREATED_BY = int.Parse(GetUserId());

					//Calling the add method of business layer class
					intRetVal = objIL.Add(objILInfo);

					if(intRetVal>0)
					{
						hidLOCATION_ID.Value = objILInfo.INSURED_LOCATION_ID.ToString();
						lblMessage.Text	=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value=	"1";
						hidOldData.Value = objIL.GetXmlForPageControls(int.Parse(hidLOCATION_ID.Value),int.Parse(hidCLAIM_ID.Value));
						LoadData();		
						btnActivateDeactivate.Enabled=true;
						btnDelete.Enabled = true;//Added for Itrack Issue 5833 on 20 July 2009
						btnReset.Enabled = false;//Done for Itrack Issue 5833 on 20 July 2009
						btnActivateDeactivate.Text = "Deactivate";
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"165");
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
					ClsInsuredLocationInfo objOldILInfo = new ClsInsuredLocationInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldILInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objILInfo.INSURED_LOCATION_ID = int.Parse(strRowId);
					objILInfo.MODIFIED_BY = int.Parse(GetUserId());

					//Updating the record using business layer class object
					intRetVal	= objIL.Update(objOldILInfo,objILInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						LoadData();
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"165");
						hidFormSaved.Value		=	"2";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"2";
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
			
		}

	
		//Added for Itrack Issue 5833 on 20 July 2009
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				objIL = new ClsInsuredLocation();
				ClsInsuredLocationInfo objILInfo = GetFormValue();
				objILInfo.CREATED_BY = int.Parse(GetUserId());//Done for Itrack Issue 6932 on 3 June 2010
				intRetVal = objIL.DeleteLocation(objILInfo,int.Parse(GetClaimID()),int.Parse(hidLOCATION_ID.Value),"RENTAL_HOME");
				//Retreiving the form values into model class object
				if(intRetVal>0)
				{
					hidFormSaved.Value		=	"1";
					lblDelete.Text		 =	ClsMessages.GetMessage(base.ScreenId,"127");
					trBody.Attributes.Add("style","display:none");
				}
				else if(intRetVal == 0)
				{
					lblDelete.Text		=	ClsMessages.GetMessage(base.ScreenId,"128");
				}
				lblDelete.Visible = true;
				
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objIL!= null)
					objIL.Dispose();
			}
		}

		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
				objIL = new ClsInsuredLocation();
				ClsInsuredLocationInfo objILInfo = GetFormValue();
				objILInfo.CREATED_BY = int.Parse(GetUserId());//Done for Itrack Issue 6932 on 3 June 2010
				if(Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("IS_ACTIVE",hidOldData.Value) == "Y")
				{
					objIL.ActivateDeactivateLocation(objILInfo,int.Parse(hidCLAIM_ID.Value),int.Parse(hidLOCATION_ID.Value),"N","RENTAL_HOME");
					lblMessage.Text		= ClsMessages.GetMessage(base.ScreenId,"41");
					lblMessage.Visible=true;
					btnActivateDeactivate.Text = "Activate";
				}
				else
				{
					objIL.ActivateDeactivateLocation(objILInfo,int.Parse(GetClaimID()),int.Parse(hidLOCATION_ID.Value),"Y","RENTAL_HOME");
					lblMessage.Text		= ClsMessages.GetMessage(base.ScreenId,"40");
					lblMessage.Visible=true;
					btnActivateDeactivate.Text = "Deactivate";
				}
				hidOldData.Value = objIL.GetXmlForPageControls(int.Parse(hidLOCATION_ID.Value),int.Parse(hidCLAIM_ID.Value));
				ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID","<script>RefreshWebGrid(1," + hidLOCATION_ID.Value + ");</script>");
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objIL!= null)
					objIL.Dispose();
			}
		}
		//Added till here
		#endregion

		#region SetCaptions
		/// <summary>
		/// Show the caption of labels from resource file
		/// </summary>
		private void SetCaptions()
		{
			capLOCATION.Text		=		objResourceMgr.GetString("cmbLOCATION");
			//Done for Itrack Issue 6932 on 15 Jan 2010
//			capDESCRIPTION.Text		=		objResourceMgr.GetString("txtDESCRIPTION");
//			capLOC_ADD1.Text		=		objResourceMgr.GetString("txtLOC_ADD1");
//			capLOC_ADD2.Text		=		objResourceMgr.GetString("txtLOC_ADD2");
//			capLOC_CITY.Text		=		objResourceMgr.GetString("txtLOC_CITY");
//			capLOC_COUNTRY.Text		=		objResourceMgr.GetString("cmbLOC_COUNTRY");
//			capLOC_STATE.Text		=		objResourceMgr.GetString("cmbLOC_STATE");
//			capLOC_ZIP.Text			=		objResourceMgr.GetString("txtLOC_ZIP");

			capDESCRIPTION.Text		=		objResourceMgr.GetString("txtLOCATION_DESCRIPTION");
			capLOC_ADD1.Text		=		objResourceMgr.GetString("txtADDRESS1");
			capLOC_ADD2.Text		=		objResourceMgr.GetString("txtADDRESS2");
			capLOC_CITY.Text		=		objResourceMgr.GetString("txtCITY");
			capLOC_COUNTRY.Text		=		objResourceMgr.GetString("cmbCOUNTRY");
			capLOC_STATE.Text		=		objResourceMgr.GetString("cmbSTATE");
			capLOC_ZIP.Text			=		objResourceMgr.GetString("txtZIP");
		}
		#endregion

		#region GetQueryString and GetOldDataXML Functions
		/// <summary>
		/// Get query string from url into hidden controls
		/// </summary>
		private void GetQueryString()
		{
			if(Request["INSURED_LOCATION_ID"] != null && int.Parse(Request["INSURED_LOCATION_ID"].ToString()) > 0 )
				hidLOCATION_ID.Value = Request.Params["INSURED_LOCATION_ID"];
			else
				hidLOCATION_ID.Value = "0";

			if (Request["CLAIM_ID"] != null)
				hidCLAIM_ID.Value = Request.Params["CLAIM_ID"];

		}

		/// <summary>
		/// retreive the information about selected record in the form of XML
		/// and saves it into hidden control
		/// </summary>
		private void GetOldDataXML()
		{
			if ( hidLOCATION_ID.Value != "0" && hidCLAIM_ID.Value != "") 
			{
				hidOldData.Value = objIL.GetXmlForPageControls(int.Parse(hidLOCATION_ID.Value),int.Parse(hidCLAIM_ID.Value));
			}

		}
		#endregion

		#region LoadData
		private void LoadData()
		{
			if ( hidLOCATION_ID.Value != "" && hidCLAIM_ID.Value != "") 
			{
				DataSet dsTemp = objIL.GetValuesOfPageControls(int.Parse(hidLOCATION_ID.Value),int.Parse(hidCLAIM_ID.Value));	
				if (dsTemp.Tables[0].Rows.Count > 0)
				{
					DataRow dr = dsTemp.Tables[0].Rows[0];
					//foreach(DataRow dr in dsTemp.Tables[0].Rows) 
					//{
						txtDESCRIPTION.Text = dr["LOCATION_DESCRIPTION"].ToString();
						txtLOC_ADD1.Text = dr["ADDRESS1"].ToString();
						txtLOC_ADD2.Text = dr["ADDRESS2"].ToString();
						txtLOC_CITY.Text = dr["CITY"].ToString();
						if(dr["COUNTRY"]!=null && dr["COUNTRY"].ToString()!="")
							cmbLOC_COUNTRY.SelectedValue = dr["COUNTRY"].ToString();
                        //if(dr["STATE"]!=null && dr["STATE"].ToString()!="")
                        //    cmbLOC_STATE.SelectedValue = dr["STATE"].ToString();
						txtLOC_ZIP.Text = dr["ZIP"].ToString();
					//}					
					if(Convert.ToInt32(dr["POLICY_LOCATION_ID"].ToString())>0) //Disable the controls
						txtLOC_ADD1.Enabled = txtLOC_ADD2.Enabled = txtLOC_CITY.Enabled = txtLOC_ZIP.Enabled = cmbLOC_STATE.Enabled = cmbLOC_COUNTRY.Enabled = false;
				}
			}
		}
		#endregion

		private void cmbLOCATION_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataTable dtPolicyLocation = new DataTable();
			ClsInsuredLocation objInsuredLocation = new ClsInsuredLocation();
			try
			{
				if(cmbLOCATION.SelectedItem!=null && cmbLOCATION.SelectedItem.Value!="")
				{
					dtPolicyLocation = objInsuredLocation.GetPolicyLocationById(int.Parse(hidCLAIM_ID.Value),int.Parse(cmbLOCATION.SelectedItem.Value));
					if(dtPolicyLocation!=null && dtPolicyLocation.Rows.Count>0)
					{
						txtDESCRIPTION.Text = dtPolicyLocation.Rows[0]["DESCRIPTION"].ToString();						
						txtLOC_ADD1.Text = dtPolicyLocation.Rows[0]["ADDRESS_1"].ToString();
						txtLOC_ADD2.Text = dtPolicyLocation.Rows[0]["ADDRESS_2"].ToString();
						txtLOC_CITY.Text = dtPolicyLocation.Rows[0]["CITY"].ToString();
						txtLOC_ZIP.Text = dtPolicyLocation.Rows[0]["ZIPCODE"].ToString();
						if(dtPolicyLocation.Rows[0]["STATE"]!=null && dtPolicyLocation.Rows[0]["STATE"].ToString()!="" && dtPolicyLocation.Rows[0]["STATE"].ToString()!="0")
							cmbLOC_STATE.SelectedValue = dtPolicyLocation.Rows[0]["STATE"].ToString();
						if(dtPolicyLocation.Rows[0]["COUNTRY"]!=null && dtPolicyLocation.Rows[0]["COUNTRY"].ToString()!="" && dtPolicyLocation.Rows[0]["COUNTRY"].ToString()!="0")
							cmbLOC_COUNTRY.SelectedValue = dtPolicyLocation.Rows[0]["COUNTRY"].ToString();

						//Disable the controls
						txtLOC_ADD1.Enabled = txtLOC_ADD2.Enabled = txtLOC_CITY.Enabled = txtLOC_ZIP.Enabled = cmbLOC_STATE.Enabled = cmbLOC_COUNTRY.Enabled = false;
					}
				}
				else //Enable the address fields
					txtLOC_ADD1.Enabled = txtLOC_ADD2.Enabled = txtLOC_CITY.Enabled = txtLOC_ZIP.Enabled = cmbLOC_STATE.Enabled = cmbLOC_COUNTRY.Enabled = true;
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
			}
			finally
			{
				if(dtPolicyLocation!=null)
					dtPolicyLocation.Dispose();
				if(objInsuredLocation!=null)
					objInsuredLocation.Dispose();
			}
		}

//		#region Fill Selected Location Data
//		private void cmbLOCATION_SelectedIndexChanged(object sender, System.EventArgs e)
//		{
//			if (hidCLAIM_ID.Value != "") 
//			{
//				DataSet dsTemp = objIL.GetPolicyLocations(int.Parse(hidCLAIM_ID.Value));	
//				if (dsTemp.Tables[0].Rows.Count > 0)
//				{
//					foreach(DataRow dr in dsTemp.Tables[0].Rows) 
//					{
//						txtDESCRIPTION.Text = dr["DESCRIPTION"].ToString();
//						txtLOC_ADD1.Text = dr["LOC_ADD1"].ToString();
//						txtLOC_ADD2.Text = dr["LOC_ADD2"].ToString();
//						txtLOC_CITY.Text = dr["LOC_CITY"].ToString();
//						cmbLOC_COUNTRY.SelectedValue = dr["LOC_COUNTRY"].ToString();
//						cmbLOC_STATE.SelectedValue = dr["LOC_STATE"].ToString();
//						txtLOC_ZIP.Text = dr["LOC_ZIP"].ToString();
//					}					
//				}
//			}		
//		}
//		#endregion
	}
}