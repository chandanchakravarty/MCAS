/******************************************************************************************
	<Author					: - > Sumit Chhabra
	<Start Date				: -	> May 04,2006
	<End Date				: - >
	<Description			: - > Page is used to display driver details at claims 
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History


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
using System.Resources; 
using System.Reflection; 
using Cms.Model.Claims;
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 
using Cms.BusinessLayer.BLClaims;




namespace Cms.Claims.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class AddWatercraftDriverDetails : Cms.Claims.ClaimBase
	{
		#region Page controls declaration
		
		
		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		//string oldXML;
		
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId;//, strFormSaved;
			
		
		#endregion		
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblVEHICLE_ID;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capNAME;
		protected System.Web.UI.WebControls.TextBox txtNAME;
		protected System.Web.UI.WebControls.TextBox txtRELATION_INSURED;
		protected System.Web.UI.WebControls.Label capADDRESS1;
		protected System.Web.UI.WebControls.TextBox txtADDRESS1;
		protected System.Web.UI.WebControls.Label capADDRESS2;
		protected System.Web.UI.WebControls.Label capDRIVERS_INJURY;
		protected System.Web.UI.WebControls.TextBox txtDRIVERS_INJURY;
		protected System.Web.UI.WebControls.TextBox txtADDRESS2;
		protected System.Web.UI.WebControls.Label capCITY;
		protected System.Web.UI.WebControls.TextBox txtCITY;
		protected System.Web.UI.WebControls.Label capSTATE;
		protected System.Web.UI.WebControls.DropDownList cmbSTATE;
		protected System.Web.UI.WebControls.Label capZIP;
		protected System.Web.UI.WebControls.TextBox txtZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revZIP;
		protected System.Web.UI.WebControls.Label capHOME_PHONE;
		protected System.Web.UI.WebControls.TextBox txtHOME_PHONE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revHOME_PHONE;
		protected System.Web.UI.WebControls.Label capWORK_PHONE;
		protected System.Web.UI.WebControls.TextBox txtWORK_PHONE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revWORK_PHONE;
		protected System.Web.UI.WebControls.Label capEXTENSION;
		protected System.Web.UI.WebControls.TextBox txtEXTENSION;
		protected System.Web.UI.WebControls.Label capMOBILE_PHONE;
		protected System.Web.UI.WebControls.TextBox txtMOBILE_PHONE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMOBILE_PHONE;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTYPE_OF_DRIVER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRESET;
		protected System.Web.UI.WebControls.Label capRELATION_INSURED;		
		protected System.Web.UI.WebControls.Label capDATE_OF_BIRTH;
		protected System.Web.UI.WebControls.TextBox txtDATE_OF_BIRTH;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDATE_OF_BIRTH;
		protected System.Web.UI.WebControls.Label capLICENSE_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtLICENSE_NUMBER;
		protected System.Web.UI.WebControls.Label capLICENSE_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbLICENSE_STATE;		
		protected System.Web.UI.WebControls.RangeValidator rngEXTENSION;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDRIVER_ID;
		
		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNAME;
		
		protected System.Web.UI.WebControls.CustomValidator csvDATE_OF_BIRTH;
		protected System.Web.UI.WebControls.Label capVEHICLE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbVEHICLE_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVEHICLE_ID;
		protected System.Web.UI.WebControls.Label capCOUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbCOUNTRY;
		protected System.Web.UI.WebControls.HyperLink hlkDATE_OF_BIRTH;
		protected System.Web.UI.WebControls.Label capSEX;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvDATE_OF_BIRTH;
		protected System.Web.UI.WebControls.Label capSSN;
		protected System.Web.UI.WebControls.TextBox txtSSN;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSSN;
		protected System.Web.UI.WebControls.DropDownList cmbSEX;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvSEX;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvLICENSE_NUMBER;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvLICENSE_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOUNTRY;
		protected System.Web.UI.WebControls.Label capVEHICLE_OWNER;
		protected System.Web.UI.WebControls.DropDownList cmbVEHICLE_OWNER;	
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTYPE_OF_OWNER;		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNAMES;
		protected System.Web.UI.WebControls.DropDownList cmbNAME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNAME;
		protected string strCalledComboFrom = "NAME";

		//Itrack 5003
		protected System.Web.UI.WebControls.Label	capSSN_NO_HID; 
		protected Cms.CmsWeb.Controls.CmsButton btnSSN_NO; 	
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSSN_NO; 
		protected System.Web.UI.HtmlControls.HtmlInputHidden  hidChkVal;
		
	

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{						
			base.ScreenId="306_5_0";
			
			lblMessage.Visible = false;
//			cmbCOUNTRY.SelectedIndex = int.Parse(aCountry);
			

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;
			
			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnDelete.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Delete;
			btnDelete.PermissionString	=	gstrSecurityXML;

			
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddWatercraftDriverDetails" ,System.Reflection.Assembly.GetExecutingAssembly());
			
			GetQueryStringValues();
			
			if(!Page.IsPostBack)
			{	
				this.cmbCOUNTRY.SelectedIndex = int.Parse(aCountry);
				hlkDATE_OF_BIRTH.Attributes.Add("OnClick","fPopCalendar(document.CLM_DRIVER_INFORMATION.txtDATE_OF_BIRTH,document.CLM_DRIVER_INFORMATION.txtDATE_OF_BIRTH)"); //Javascript Implementation for Calender																		
				if(hidRESET.Value=="1")
					ResetFields("");
				hidRESET.Value="0";
				LoadDropDowns();				
				//Load data into controls
				GetOldDataXML(true);				
				btnReset.Attributes.Add("onclick","javascript:return ResetTheForm();");
				SetCaptions();
				SetErrorMessages();								
			}
			
		}
		#endregion

		#region GetOldDataXML
		private void GetOldDataXML(bool LOAD_DATA_FLAG)
		{
			DataSet dsOldData = new DataSet();
			if(hidDRIVER_ID.Value!="" && hidDRIVER_ID.Value!="0" && hidDRIVER_ID.Value.ToUpper()!="NEW")
			{
				dsOldData	=	ClsDriverDetails.GetDriverDetails(int.Parse(hidDRIVER_ID.Value),int.Parse(hidCLAIM_ID.Value));
				if(dsOldData!=null && dsOldData.Tables.Count>0)
				{
					hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsOldData.Tables[0]);
					if(LOAD_DATA_FLAG)
						LoadData(dsOldData);
					LoadNameDropDown();
					btnReset.Enabled=false;//Done for Itrack issue 6327
				}
				else
					hidOldData.Value	=	"";
			}
			else
				hidOldData.Value	=	"";
		}
		#endregion

		#region Reset Fields
		private void ResetFields(string strCalledFrom)
		{
			//if(cmbVEHICLE_OWNER.Items.Count>0)
			//	cmbVEHICLE_OWNER.SelectedIndex = 3;			
			txtADDRESS1.Text		=		"";
			txtADDRESS2.Text		=		"";
			txtCITY.Text			=		"";
			cmbSTATE.SelectedIndex	=		-1;
			txtZIP.Text				=		"";
			txtHOME_PHONE.Text		=		"";
			txtWORK_PHONE.Text		=		"";
			txtEXTENSION.Text		=		"";
			txtMOBILE_PHONE.Text	=		"";
			//txtNAME.Text			=		"";
			txtDRIVERS_INJURY.Text =		"";
			txtRELATION_INSURED.Text =		"";
			if(strCalledFrom!=strCalledComboFrom)
				cmbNAME.SelectedIndex		=	-1;
			txtDATE_OF_BIRTH.Text = "";
			txtLICENSE_NUMBER.Text = "";
			cmbLICENSE_STATE.SelectedIndex = -1;
			//			txtNAME.Text				=	"";
				
		}
		#endregion


		#region LoadData function to load values into controls
		private void LoadData(DataSet dsLoadData)
		{
			try
			{
				if(dsLoadData!=null && dsLoadData.Tables.Count>0&& dsLoadData.Tables[0].Rows.Count>0)
				{
										
					txtADDRESS1.Text		=		dsLoadData.Tables[0].Rows[0]["ADDRESS1"].ToString();
					txtADDRESS2.Text		=		dsLoadData.Tables[0].Rows[0]["ADDRESS2"].ToString();
					txtDRIVERS_INJURY.Text  =       dsLoadData.Tables[0].Rows[0]["DRIVERS_INJURY"].ToString();
					//Itrack 5003
                    //txtSSN.Text				=		dsLoadData.Tables[0].Rows[0]["SSN"].ToString();
					//Decrypt 
					hidSSN_NO.Value = dsLoadData.Tables[0].Rows[0]["SSN"].ToString();
					string strSSN_NO = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(dsLoadData.Tables[0].Rows[0]["SSN"].ToString());
					if(strSSN_NO != "")
					{
						string strvaln = "xxx-xx-";
						strvaln += strSSN_NO.Substring(strvaln.Length, strSSN_NO.Length - strvaln.Length);
						capSSN_NO_HID.Text = strvaln;
					}
					else
						capSSN_NO_HID.Text = "";
					//End Decrypt
					txtCITY.Text			=		dsLoadData.Tables[0].Rows[0]["CITY"].ToString();
					if(dsLoadData.Tables[0].Rows[0]["STATE"]!=null && dsLoadData.Tables[0].Rows[0]["STATE"].ToString()!="" && dsLoadData.Tables[0].Rows[0]["STATE"].ToString()!="0")
						cmbSTATE.SelectedValue	=		dsLoadData.Tables[0].Rows[0]["STATE"].ToString();
					txtZIP.Text				=		dsLoadData.Tables[0].Rows[0]["ZIP"].ToString();
					txtHOME_PHONE.Text		=		dsLoadData.Tables[0].Rows[0]["HOME_PHONE"].ToString();
					txtWORK_PHONE.Text		=		dsLoadData.Tables[0].Rows[0]["WORK_PHONE"].ToString();
					txtEXTENSION.Text		=		dsLoadData.Tables[0].Rows[0]["EXTENSION"].ToString();
					txtMOBILE_PHONE.Text	=		dsLoadData.Tables[0].Rows[0]["MOBILE_PHONE"].ToString();
					

					if(dsLoadData.Tables[0].Rows[0]["RELATION_INSURED"]!=null && dsLoadData.Tables[0].Rows[0]["RELATION_INSURED"].ToString()!="")
						txtRELATION_INSURED.Text = dsLoadData.Tables[0].Rows[0]["RELATION_INSURED"].ToString();
					if(dsLoadData.Tables[0].Rows[0]["DATE_OF_BIRTH"]!=null && dsLoadData.Tables[0].Rows[0]["DATE_OF_BIRTH"].ToString()!="")
						txtDATE_OF_BIRTH.Text	=		dsLoadData.Tables[0].Rows[0]["DATE_OF_BIRTH"].ToString().Trim();
					txtLICENSE_NUMBER.Text	=		dsLoadData.Tables[0].Rows[0]["LICENSE_NUMBER"].ToString();
					if(dsLoadData.Tables[0].Rows[0]["SEX"]!=null && dsLoadData.Tables[0].Rows[0]["SEX"].ToString()!="" && dsLoadData.Tables[0].Rows[0]["SEX"].ToString()!="0")
						cmbSEX.SelectedValue = dsLoadData.Tables[0].Rows[0]["SEX"].ToString().Trim();
					if(dsLoadData.Tables[0].Rows[0]["COUNTRY"]!=null && dsLoadData.Tables[0].Rows[0]["COUNTRY"].ToString()!="" && dsLoadData.Tables[0].Rows[0]["COUNTRY"].ToString()!="0")
						cmbCOUNTRY.SelectedValue	=		dsLoadData.Tables[0].Rows[0]["COUNTRY"].ToString();
					if(dsLoadData.Tables[0].Rows[0]["LICENSE_STATE"]!=null && dsLoadData.Tables[0].Rows[0]["LICENSE_STATE"].ToString()!="0")
						cmbLICENSE_STATE.SelectedValue = dsLoadData.Tables[0].Rows[0]["LICENSE_STATE"].ToString();
															
					if(dsLoadData.Tables[0].Rows[0]["VEHICLE_OWNER"]!=null && dsLoadData.Tables[0].Rows[0]["VEHICLE_OWNER"].ToString()!="" && dsLoadData.Tables[0].Rows[0]["VEHICLE_OWNER"].ToString()!="0")
						cmbVEHICLE_OWNER.SelectedValue = dsLoadData.Tables[0].Rows[0]["VEHICLE_OWNER"].ToString();
					hidTYPE_OF_OWNER.Value	=		dsLoadData.Tables[0].Rows[0]["TYPE_OF_OWNER"].ToString();					

					string strNAME = dsLoadData.Tables[0].Rows[0]["NAME"].ToString();
					txtNAME.Text			=		strNAME;
					hidNAME.Value			=		strNAME;
							
					LoadNameDropDown();
					//Done for Itrack Issue 6523 on 7 Oct 09
					if(dsLoadData.Tables[0].Rows[0]["VEHICLE_OWNER"]!=null && dsLoadData.Tables[0].Rows[0]["VEHICLE_OWNER"].ToString()!="" && dsLoadData.Tables[0].Rows[0]["VEHICLE_OWNER"].ToString()!="0")
					{
						cmbNAME_SelectedIndexChanged(null,null);
					}
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{}

		}
		#endregion

		private void LoadNameDropDown()
		{
			if(cmbVEHICLE_OWNER.SelectedItem!=null && cmbVEHICLE_OWNER.SelectedItem.Value!="")
			{
				string strVEHICLE_OWNER = cmbVEHICLE_OWNER.SelectedItem.Value;
				if(strVEHICLE_OWNER== ((int)enumVEHICLE_OWNER.INSURED).ToString() || strVEHICLE_OWNER== ((int)enumVEHICLE_OWNER.NAMED_INSURED).ToString() || strVEHICLE_OWNER== ((int)enumVEHICLE_OWNER.RATED_DRIVER).ToString())
				{
					cmbVEHICLE_OWNER_SelectedIndexChanged(null,null);
					ListItem lItem = cmbNAME.Items.FindByText(hidNAME.Value);
					cmbNAME.SelectedIndex = cmbNAME.Items.IndexOf(lItem);
				}
			}	
		}

		
		
		private void GetQueryStringValues()
		{
			if(Request.QueryString["TYPE_OF_DRIVER"]!=null && Request.QueryString["TYPE_OF_DRIVER"].ToString()!="")
			{
				hidTYPE_OF_DRIVER.Value = Request.QueryString["TYPE_OF_DRIVER"].ToString();
				hidChkVal.Value = "1";
			}

			if(Request.QueryString["DRIVER_ID"]!=null && Request.QueryString["DRIVER_ID"].ToString()!="")
				hidDRIVER_ID.Value = Request.QueryString["DRIVER_ID"].ToString();

			if(Request.QueryString["CLAIM_ID"]!=null && Request.QueryString["CLAIM_ID"].ToString()!="")
				hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();

			if(Request.QueryString["CUSTOMER_ID"]!=null && Request.QueryString["CUSTOMER_ID"].ToString()!="" )
				hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString();

			if(Request.QueryString["POLICY_ID"]!=null && Request.QueryString["POLICY_ID"].ToString()!="" )
				hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"].ToString();

			if(Request.QueryString["POLICY_VERSION_ID"]!=null && Request.QueryString["POLICY_VERSION_ID"].ToString()!="" )
				hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();

			if(Request.QueryString["LOB_ID"]!=null && Request.QueryString["LOB_ID"].ToString()!="")
				hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();		
			if(Request.QueryString["TYPE_OF_OWNER"]!=null && Request.QueryString["TYPE_OF_OWNER"].ToString()!="")
				hidTYPE_OF_OWNER.Value = Request.QueryString["TYPE_OF_OWNER"].ToString();
			
		}


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{				
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.cmbVEHICLE_OWNER.SelectedIndexChanged += new System.EventHandler(this.cmbVEHICLE_OWNER_SelectedIndexChanged);			
			this.cmbNAME.SelectedIndexChanged += new System.EventHandler(this.cmbNAME_SelectedIndexChanged);

		}
		#endregion

		private void cmbNAME_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(cmbNAME.SelectedValue !="")
			{
				if(hidCUSTOMER_ID.Value!="0" && hidPOLICY_ID.Value!="0" && hidPOLICY_VERSION_ID.Value!="0" && hidCUSTOMER_ID.Value!="" && hidPOLICY_ID.Value!="" && hidPOLICY_VERSION_ID.Value!="")
				{
					DataTable dtNames = ClsOwnerDetails.GetNamedInsured(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),int.Parse(cmbVEHICLE_OWNER.SelectedItem.Value),int.Parse(hidCLAIM_ID.Value),cmbNAME.SelectedValue,"",0);//Modified for Itrack Issue 6490 on 30 Sept 2009					
					txtNAME.Text = "";

					if(sender!=null && e!=null)
						ResetFields(strCalledComboFrom);

					/*txtADDRESS1.Text = txtADDRESS2.Text = txtADDRESS2.Text = txtCITY.Text = txtSSN.Text = "";
					txtZIP.Text = txtHOME_PHONE.Text = txtRELATION_INSURED.Text = "";
					txtHOME_PHONE.Text = txtMOBILE_PHONE.Text = txtDATE_OF_BIRTH.Text = txtWORK_PHONE.Text = "";
					txtEXTENSION.Text = txtLICENSE_NUMBER.Text = "";
					cmbSTATE.SelectedIndex = cmbSEX.SelectedIndex = cmbCOUNTRY.SelectedIndex = cmbLICENSE_STATE.SelectedIndex = -1;
					*/
					if(dtNames!=null && dtNames.Rows.Count>0)
					{
						DataRow drTemp = dtNames.Rows[0];
						if(drTemp["ADDRESS1"].ToString().Trim()!="")
						{
							txtADDRESS1.Text = drTemp["ADDRESS1"].ToString().Trim();
							txtADDRESS1.Enabled = false;
						}
						else
						{
							txtADDRESS1.Text = "";
							txtADDRESS1.Enabled = true;
						}

						if(drTemp["ADDRESS2"].ToString().Trim()!="")
						{
							txtADDRESS2.Text = drTemp["ADDRESS2"].ToString().Trim();
							txtADDRESS2.Enabled = false;
						}
						else
							txtADDRESS2.Enabled = true;

						if(drTemp["CITY"].ToString().Trim()!="")
						{
							txtCITY.Text = drTemp["CITY"].ToString().Trim();
							txtCITY.Enabled = false;
						}
						else
							txtCITY.Enabled = true;
						
						if (drTemp["STATE"] != null && drTemp["STATE"].ToString().Trim() != "0")
						{
							cmbSTATE.SelectedValue = drTemp["STATE"].ToString().Trim();
							cmbSTATE.Enabled = false;
						}
						else
							cmbSTATE.Enabled = true;
						
						if(drTemp["ZIP_CODE"].ToString().Trim()!="")
						{
							txtZIP.Text = drTemp["ZIP_CODE"].ToString().Trim();
							txtZIP.Enabled = false;
						}
						else
							txtZIP.Enabled = true;

						if(drTemp["COUNTRY"].ToString().Trim()!="")
						{
							cmbCOUNTRY.SelectedValue = drTemp["COUNTRY"].ToString().Trim();
							cmbCOUNTRY.Enabled = false;
						}
						else
							cmbCOUNTRY.Enabled = true;

						if(drTemp["PHONE"].ToString().Trim()!="")
						{
							txtHOME_PHONE.Text = drTemp["PHONE"].ToString().Trim();
							txtHOME_PHONE.Enabled = false;
						}
						else
							txtHOME_PHONE.Enabled = true;

						if(drTemp["MOBILE_PHONE"].ToString().Trim()!="")
						{
							txtMOBILE_PHONE.Text = drTemp["MOBILE_PHONE"].ToString().Trim();
							txtMOBILE_PHONE.Enabled = false;
						}
						else
							txtMOBILE_PHONE.Enabled = true;
						
						
						if(drTemp["DATE_OF_BIRTH"].ToString().Trim()!="")
						{
							txtDATE_OF_BIRTH.Text = drTemp["DATE_OF_BIRTH"].ToString().Trim();
							txtDATE_OF_BIRTH.Enabled = false;
						}
						else
							txtDATE_OF_BIRTH.Enabled = true;
						
						if(drTemp["WORK_PHONE"].ToString().Trim()!="")
						{
							txtWORK_PHONE.Text = drTemp["WORK_PHONE"].ToString().Trim();
							txtWORK_PHONE.Enabled = false;
						}
						else
							txtWORK_PHONE.Enabled = true;

						if(drTemp["EXTENSION"].ToString().Trim()!="")
						{
							txtEXTENSION.Text = drTemp["EXTENSION"].ToString().Trim();
							txtEXTENSION.Enabled = false;
						}
						else
							txtEXTENSION.Enabled = true;

						if(drTemp["RELATION_INSURED"].ToString().Trim()!="")
						{
							txtRELATION_INSURED.Text = drTemp["RELATION_INSURED"].ToString().Trim();
							txtRELATION_INSURED.Enabled = false;
						}
						else
							txtRELATION_INSURED.Enabled = true;

						if(drTemp["RELATION_INSURED"].ToString().Trim()!="")
						{
							txtRELATION_INSURED.Text = drTemp["RELATION_INSURED"].ToString().Trim();
							txtRELATION_INSURED.Enabled = false;
						}
						else
							txtRELATION_INSURED.Enabled = true;		

						if(drTemp["LICENSE_NUMBER"].ToString().Trim()!="")
						{
							txtLICENSE_NUMBER.Text = drTemp["LICENSE_NUMBER"].ToString().Trim();
							txtLICENSE_NUMBER.Enabled = false;
						}
						else
							txtLICENSE_NUMBER.Enabled = true;		

						if(drTemp["LICENSE_STATE"].ToString().Trim()!="")
						{
							cmbLICENSE_STATE.SelectedValue = drTemp["LICENSE_STATE"].ToString().Trim();
							cmbLICENSE_STATE.Enabled = false;
						}
						else
							cmbLICENSE_STATE.Enabled = true;	

						if(drTemp["SSN_NO"].ToString().Trim()!="") 
						{
							hidSSN_NO.Value = drTemp["SSN_NO"].ToString().Trim();
							string strSSN_NO = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(drTemp["SSN_NO"].ToString().Trim());
							if(strSSN_NO != "")
							{
								
								string strvaln = "xxx-xx-";
								strvaln += strSSN_NO.Substring(strvaln.Length, strSSN_NO.Length - strvaln.Length);
								capSSN_NO_HID.Text = strvaln;

							}
							else
								capSSN_NO_HID.Text = "";
							//txtSSN.Text = drTemp["SSN_NO"].ToString().Trim();
							//txtSSN.Text = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(drTemp["SSN_NO"].ToString().Trim());
							txtSSN.Enabled = false;
							//btnSSN_NO.Enabled = false;
						}
						else
						{
							txtSSN.Enabled = true;	
							capSSN_NO_HID.Text = "";
							hidSSN_NO.Value = "";
							if(hidSSN_NO.Value=="")
							{
                                txtSSN.Enabled = false;	
							}
						}

						if(drTemp["SEX"].ToString().Trim()!="")
						{
							cmbSEX.SelectedValue = drTemp["SEX"].ToString().Trim();
							cmbSEX.Enabled = false;
						}
						else
							cmbSEX.Enabled = true;	

					}
					else
					{
						txtADDRESS1.Enabled = txtADDRESS2.Enabled = txtADDRESS2.Enabled = txtCITY.Enabled = txtSSN.Enabled = true;
						cmbSTATE.Enabled = txtZIP.Enabled = txtHOME_PHONE.Enabled = txtRELATION_INSURED.Enabled = true;
						txtHOME_PHONE.Enabled = txtMOBILE_PHONE.Enabled = txtDATE_OF_BIRTH.Enabled = txtWORK_PHONE.Enabled = true;
						txtEXTENSION.Enabled = cmbCOUNTRY.Enabled = cmbLICENSE_STATE.Enabled = true;
						cmbSEX.Enabled = txtLICENSE_NUMBER.Enabled = true;
					}
						
				}
				
			}
			else
				ResetFields(strCalledComboFrom);
		}

		

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			
			revZIP.ValidationExpression				=		  aRegExpZip;
			revZIP.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("24");
			revSSN.ValidationExpression			= aRegExpSSN;
			revSSN.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"130");
			//			revDEFAULT_PHONE_TO_NOTICE.ValidationExpression	=		  aRegExpEmail;
			//			revDEFAULT_PHONE_TO_NOTICE.ErrorMessage			=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("23");
			rfvNAME.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("34");			
			rfvNAMES.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("746");		
			//			rfvZIP.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("37");			
			revHOME_PHONE.ValidationExpression	=		  aRegExpPhone;
			revHOME_PHONE.ErrorMessage			=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");						
			revWORK_PHONE.ValidationExpression	=		  aRegExpPhone;
			revWORK_PHONE.ErrorMessage			=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");						
			revMOBILE_PHONE.ValidationExpression	=		  aRegExpPhone;
			revMOBILE_PHONE.ErrorMessage			=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");						
			revDATE_OF_BIRTH.ValidationExpression	=	aRegExpDate;
			revDATE_OF_BIRTH.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");						
			csvDATE_OF_BIRTH.ErrorMessage					=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"10");
			csvDATE_OF_BIRTH.ErrorMessage					=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("198");
			//rngESTIMATE_AMOUNT.ErrorMessage			=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");			
			rngEXTENSION.ErrorMessage			=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
			rfvVEHICLE_ID.ErrorMessage  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("862");
			rfvCOUNTRY.ErrorMessage				=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("33");			
			//rfvDATE_OF_BIRTH.ErrorMessage				=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("162");			
			//rfvSEX.ErrorMessage				=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("189");			
			//rfvLICENSE_NUMBER.ErrorMessage				=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("190");			
			//rfvLICENSE_STATE.ErrorMessage				=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("111");			
			//			rfvADDRESS1.ErrorMessage					=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"2");
			//			rfvSTATE.ErrorMessage					=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("35");

			
		}

		#endregion

		

		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal=1;	
				
				//For retreiving the return value of business class save function
				ClsDriverDetails objDriverDetails = new ClsDriverDetails();				

				//Retreiving the form values into model class object
				ClsDriverDetailsInfo objDriverDetailsInfo = GetFormValue();


				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objDriverDetailsInfo.CREATED_BY = int.Parse(GetUserId());
					objDriverDetailsInfo.CREATED_DATETIME = DateTime.Now;
					objDriverDetailsInfo.IS_ACTIVE="Y"; 
					
					//Calling the add method of business layer class
					intRetVal = objDriverDetails.Add(objDriverDetailsInfo);

					if(intRetVal>0)
					{
						hidDRIVER_ID.Value = objDriverDetailsInfo.DRIVER_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						//Don't require to load the data into controls, sending false therefore
						
						GetOldDataXML(true);
						btnReset.Enabled=false;//Done for Itrack issue 6327
					}					
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}					
				} // end save case
				else //UPDATE CASE
				{
					//Creating the Model object for holding the Old data
					ClsDriverDetailsInfo objOldDriverDetailsInfo = new ClsDriverDetailsInfo();
					
					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldDriverDetailsInfo,hidOldData.Value);
					
					//Setting those values into the Model object which are not in the page					
					objDriverDetailsInfo.MODIFIED_BY = int.Parse(GetUserId());
					objDriverDetailsInfo.LAST_UPDATED_DATETIME = DateTime.Now;                    
					
					//Updating the record using business layer class object
					intRetVal	= objDriverDetails.Update(objOldDriverDetailsInfo,objDriverDetailsInfo);					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						//Don't require to load the data into controls, sending false therefore
						hidChkVal.Value = "1";
						GetOldDataXML(true);
					}					
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"1";
					}					
				}
				lblMessage.Visible = true;
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
				//				if(objAgency!= null)
				//					objAgency.Dispose();
			}
		}
		private void SetCaptions()
		{			
			capVEHICLE_OWNER.Text					=		objResourceMgr.GetString("cmbVEHICLE_OWNER");
			capNAME.Text							=		objResourceMgr.GetString("txtNAME");
			capADDRESS1.Text						=		objResourceMgr.GetString("txtADDRESS1");
			capDRIVERS_INJURY.Text					=       objResourceMgr.GetString("txtDRIVERS_INJURY");
			capADDRESS2.Text						=		objResourceMgr.GetString("txtADDRESS2");
			capCITY.Text							=		objResourceMgr.GetString("txtCITY");
			capSTATE.Text							=		objResourceMgr.GetString("cmbSTATE");
			capZIP.Text								=		objResourceMgr.GetString("txtZIP");
			capHOME_PHONE.Text						=		objResourceMgr.GetString("txtHOME_PHONE");
			capWORK_PHONE.Text						=		objResourceMgr.GetString("txtWORK_PHONE");
			capEXTENSION.Text						=		objResourceMgr.GetString("txtEXTENSION");
			capMOBILE_PHONE.Text					=		objResourceMgr.GetString("txtMOBILE_PHONE");			
			capRELATION_INSURED.Text				=		objResourceMgr.GetString("txtRELATION_INSURED");			
			capDATE_OF_BIRTH.Text					=		objResourceMgr.GetString("txtDATE_OF_BIRTH");			
			capLICENSE_NUMBER.Text					=		objResourceMgr.GetString("txtLICENSE_NUMBER");			
			capVEHICLE_ID.Text						=		objResourceMgr.GetString("cmbVEHICLE_ID");
			capLICENSE_STATE.Text					=		objResourceMgr.GetString("cmbLICENSE_STATE");						
			capCOUNTRY.Text							=		objResourceMgr.GetString("cmbCOUNTRY");
			capSEX.Text								=		objResourceMgr.GetString("cmbSEX");
			capSSN.Text								=		objResourceMgr.GetString("txtSSN");
			
			
		}
	

		#region GetFormValue
		private ClsDriverDetailsInfo GetFormValue()
		{
			ClsDriverDetailsInfo objDriverDetailsInfo = new ClsDriverDetailsInfo();			
			objDriverDetailsInfo.ADDRESS1			=	txtADDRESS1.Text.Trim();
			objDriverDetailsInfo.ADDRESS2			=	txtADDRESS2.Text.Trim();
			objDriverDetailsInfo.CITY				=	txtCITY.Text.Trim();
			if(cmbSTATE.SelectedItem!=null && cmbSTATE.SelectedItem.Value!="")
				objDriverDetailsInfo.STATE			=	int.Parse(cmbSTATE.SelectedItem.Value);
			
			objDriverDetailsInfo.ZIP					=	txtZIP.Text.Trim();
			objDriverDetailsInfo.HOME_PHONE			=	txtHOME_PHONE.Text.Trim();
			objDriverDetailsInfo.WORK_PHONE			=	txtWORK_PHONE.Text.Trim();
			objDriverDetailsInfo.EXTENSION			=	txtEXTENSION.Text.Trim();
			objDriverDetailsInfo.MOBILE_PHONE		=	txtMOBILE_PHONE.Text.Trim();
			objDriverDetailsInfo.CLAIM_ID	  = int.Parse(hidCLAIM_ID.Value);
			objDriverDetailsInfo.TYPE_OF_DRIVER = int.Parse(hidTYPE_OF_DRIVER.Value);
			
			objDriverDetailsInfo.RELATION_INSURED = txtRELATION_INSURED.Text.Trim();
			if(txtDATE_OF_BIRTH.Text.Trim()!="" && IsDate(txtDATE_OF_BIRTH.Text.Trim()))
				objDriverDetailsInfo.DATE_OF_BIRTH  = Convert.ToDateTime(txtDATE_OF_BIRTH.Text.Trim());
			objDriverDetailsInfo.LICENSE_NUMBER = txtLICENSE_NUMBER.Text.Trim();
			if(cmbLICENSE_STATE.SelectedItem!=null && cmbLICENSE_STATE.SelectedItem.Value!="")
				objDriverDetailsInfo.LICENSE_STATE = int.Parse(cmbLICENSE_STATE.SelectedItem.Value);
			if(cmbVEHICLE_ID.SelectedItem!=null && cmbVEHICLE_ID.SelectedItem.Value!="")
				objDriverDetailsInfo.VEHICLE_ID = int.Parse(cmbVEHICLE_ID.SelectedItem.Value);
			if(cmbCOUNTRY.SelectedItem!=null && cmbCOUNTRY.SelectedItem.Value!="")
				objDriverDetailsInfo.COUNTRY = int.Parse(cmbCOUNTRY.SelectedItem.Value);
			if(cmbSEX.SelectedItem!=null && cmbSEX.SelectedItem.Value!="")
				objDriverDetailsInfo.SEX = cmbSEX.SelectedItem.Value.Trim();

			//Itrack 5003
			//if(txtSSN.Text.Trim()!="")
				//objDriverDetailsInfo.SSN = txtSSN.Text.Trim();

			if(txtSSN.Text.Trim()!="")
			{
				objDriverDetailsInfo.SSN			=	Cms.BusinessLayer.BlCommon.ClsCommon.EncryptString(txtSSN.Text.Trim());
				txtSSN.Text = "";
			}
			else
				objDriverDetailsInfo.SSN			= hidSSN_NO.Value;

			//End
			
			if(hidDRIVER_ID.Value.ToUpper()=="NEW" || hidDRIVER_ID.Value=="0" || hidDRIVER_ID.Value=="")
				strRowId="NEW";
			else
			{
				strRowId=hidDRIVER_ID.Value;
				objDriverDetailsInfo.DRIVER_ID		=	int.Parse(hidDRIVER_ID.Value);
			}
			if(hidTYPE_OF_OWNER.Value == ((int)enumTYPE_OF_OWNER.INSURED_VEHICLE).ToString())
			{
				if(cmbVEHICLE_OWNER.SelectedItem!=null && cmbVEHICLE_OWNER.SelectedItem.Value!="")
					objDriverDetailsInfo.VEHICLE_OWNER = int.Parse(cmbVEHICLE_OWNER.SelectedItem.Value);
			}			
			else
				objDriverDetailsInfo.VEHICLE_OWNER = 0;
			if(objDriverDetailsInfo.VEHICLE_OWNER== (int)enumVEHICLE_OWNER.INSURED || objDriverDetailsInfo.VEHICLE_OWNER== (int)enumVEHICLE_OWNER.NAMED_INSURED || objDriverDetailsInfo.VEHICLE_OWNER== (int)enumVEHICLE_OWNER.RATED_DRIVER)							
				objDriverDetailsInfo.NAME		=	cmbNAME.SelectedItem.Text;
			else
				objDriverDetailsInfo.NAME		=	txtNAME.Text.Trim();
			objDriverDetailsInfo.TYPE_OF_OWNER = int.Parse(hidTYPE_OF_OWNER.Value);

			objDriverDetailsInfo.DRIVERS_INJURY	=	txtDRIVERS_INJURY.Text.Trim();

			return objDriverDetailsInfo;
		}
		#endregion

		private void cmbVEHICLE_OWNER_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(cmbVEHICLE_OWNER.SelectedItem!=null && cmbVEHICLE_OWNER.SelectedItem.Value!="" && cmbVEHICLE_ID.SelectedItem!=null && cmbVEHICLE_ID.SelectedItem.Value!=null && cmbVEHICLE_ID.SelectedItem.Value!="" && cmbVEHICLE_ID.SelectedItem.Value!="0")//Done for Itrack Issue 6313 on 27 Aug 2009
			{
				if(hidRESET.Value=="1")
				{
					if(sender!=null && e!=null)
						ResetFields("");
				}
				if(sender!=null && e!=null)
					hidChkVal.Value = "0";

				
				string strVEHICLE_OWNER = cmbVEHICLE_OWNER.SelectedItem.Value;
				/*if(hidOldData.Value=="" || hidOldData.Value=="0")
				{
					txtADDRESS1.Text = txtADDRESS2.Text = txtADDRESS2.Text = txtCITY.Text = txtSSN.Text = "";
					txtZIP.Text = txtHOME_PHONE.Text = txtRELATION_INSURED.Text = "";
					txtHOME_PHONE.Text = txtMOBILE_PHONE.Text = txtDATE_OF_BIRTH.Text = txtWORK_PHONE.Text = "";
					txtEXTENSION.Text = txtLICENSE_NUMBER.Text = "";
					cmbSTATE.SelectedIndex = cmbSEX.SelectedIndex = cmbCOUNTRY.SelectedIndex = cmbLICENSE_STATE.SelectedIndex = -1;
				}*/
				if(strVEHICLE_OWNER== ((int)enumVEHICLE_OWNER.INSURED).ToString() || strVEHICLE_OWNER== ((int)enumVEHICLE_OWNER.NAMED_INSURED).ToString()|| strVEHICLE_OWNER== ((int)enumVEHICLE_OWNER.RATED_DRIVER).ToString())
				{
					if(hidCUSTOMER_ID.Value!="0" && hidPOLICY_ID.Value!="0" && hidPOLICY_VERSION_ID.Value!="0" && hidCUSTOMER_ID.Value!="" && hidPOLICY_ID.Value!="" && hidPOLICY_VERSION_ID.Value!="")
					{
						DataTable dtNames = ClsOwnerDetails.GetNamedInsured(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),int.Parse(cmbVEHICLE_OWNER.SelectedItem.Value),int.Parse(hidCLAIM_ID.Value),int.Parse(cmbVEHICLE_ID.SelectedItem.Value));//Added for Itrack Issue 6053 on 31 July 2009
						txtNAME.Text = "";
						if(dtNames!=null && dtNames.Rows.Count>0)
						{
							cmbNAME.DataSource = dtNames;
							cmbNAME.DataTextField = "NAMED_INSURED";
							cmbNAME.DataValueField = "NAMED_INSURED_ID";
							cmbNAME.DataBind();
							cmbNAME.Items.Insert(0,"");
							if(hidChkVal.Value!="1")
                                ResetFields("");
						}
					}
					
				}
				else
				{
					txtADDRESS1.Enabled = true;
					txtADDRESS2.Enabled = true;
					txtCITY.Enabled = true;
					cmbSTATE.Enabled = true;
					txtZIP.Enabled = true;
					txtHOME_PHONE.Enabled = true;
					cmbCOUNTRY.Enabled = true;
				}
			}		
			else //Done for Itrack Issue 6313 on 27 Aug 2009
			{
				cmbVEHICLE_OWNER.SelectedIndex=0;
				cmbNAME.Items.Clear();
			}
		}

		#region LoadDropDowns
		private void LoadDropDowns()
		{
			
			try
			{
				
				DataTable dt = Cms.CmsWeb.ClsFetcher.Country;

				cmbCOUNTRY.DataSource		= dt;
				cmbCOUNTRY.DataTextField		= "Country_Name";
				cmbCOUNTRY.DataValueField	= "Country_Id";
				cmbCOUNTRY.DataBind();
				
					
				dt = Cms.CmsWeb.ClsFetcher.State;
				cmbSTATE.DataSource		= dt;
				cmbSTATE.DataTextField	= "State_Name";
				cmbSTATE.DataValueField	= "State_Id";
				cmbSTATE.DataBind();
				cmbSTATE.Items.Insert(0,"");

				cmbLICENSE_STATE.DataSource		=  Cms.CmsWeb.ClsFetcher.State; 
				cmbLICENSE_STATE.DataTextField	= "State_Name";
				cmbLICENSE_STATE.DataValueField	= "State_Id";
				cmbLICENSE_STATE.DataBind();					 
				cmbLICENSE_STATE.Items.Insert(0,"");

				
				cmbSEX.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("SEX");
				cmbSEX.DataTextField="LookupDesc";
				cmbSEX.DataValueField="LookupCode";
				cmbSEX.DataBind();
				cmbSEX.Items.Insert(0,"");

				if(hidTYPE_OF_OWNER.Value == ((int)enumTYPE_OF_OWNER.INSURED_VEHICLE).ToString())
				{
					cmbVEHICLE_OWNER.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CLVO");
					cmbVEHICLE_OWNER.DataTextField	= "LookupDesc";
					cmbVEHICLE_OWNER.DataValueField	= "LookupID";
					cmbVEHICLE_OWNER.DataBind();					
					cmbVEHICLE_OWNER.Items.Insert(0,"");
				}
				
				ClsDriverDetails objDriverDetails = new ClsDriverDetails();
				DataTable dtBoats = objDriverDetails.GetClaimBoats(int.Parse(hidCLAIM_ID.Value));
				
				if(dtBoats!=null && dtBoats.Rows.Count>0)
				{
					
//					//Add an empty listitem when there are multiple vehicles added
//					if(dtBoats.Rows.Count>1)
//						cmbVEHICLE_ID.Items.Add(new ListItem("",""));
					foreach(DataRow dtRow in dtBoats.Rows)
					{
						string sVal = "";
						string sText = "";
						for(int i=0;i<dtBoats.Columns.Count-1;i++)
							sText+= dtRow[i].ToString() + "-";

						sText = sText.Substring(0,sText.Length-1);
						if(dtRow["BOAT_ID"]!=null && dtRow["BOAT_ID"].ToString()!="")
							sVal = dtRow["BOAT_ID"].ToString();
						//sVal=sVal.Substring(0,sVal.Length-5).Trim();
						//sText = dtRow["BOAT_ID"].ToString() + "-" + dtRow["VIN"].ToString()  + "-" + dtRow["VEHICLE_YEAR"].ToString()  + "-" + dtRow["MAKE"].ToString()  + "-" + dtRow["MODEL"].ToString()  + "-" + dtRow["BODY_TYPE"].ToString();
						//ListItem lItem = new ListItem(dtRow["VIN"].ToString(),sVal);
						ListItem lItem = new ListItem(sText,sVal);
						cmbVEHICLE_ID.Items.Add(lItem);
					}
					lblVEHICLE_ID.Visible = false;
				}
				else
				{
					if(hidLOB_ID.Value == "4")
						lblVEHICLE_ID.Text	=  "No Boat added until now. Please click <a href='javascript:Redirect_boat();' onclick='Redirect_boat();'>here</a> to add boat";
					else
						lblVEHICLE_ID.Text	=  "No boat added until now. Please click <a href='javascript:Redirect();' onclick='Redirect();'>here</a> to add boat";
					cmbVEHICLE_ID.Attributes.Add("style","display:none");
//					rfvVEHICLE_ID.Attributes.Add("style","enabled:false");
//					rfvVEHICLE_ID.Attributes.Add("style","IsValid:false");
					rfvVEHICLE_ID.Enabled = false;
				}
			}
			catch//(Exception ex)
			{
			}
			finally
			{
				
			}
			
			
		}
		#endregion

		
		
		
	}
}
