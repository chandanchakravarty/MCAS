/******************************************************************************************
	<Author					: - > Sumit Chhabra
	<Start Date				: -	> April 20,2006
	<End Date				: - >
	<Description			: - > Page is used to assign limits to authority
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
	public class AddOwnerDetails : Cms.Claims.ClaimBase
	{
		#region Page controls declaration
		
		
		#endregion
		#region Local form variables
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId;
		protected System.Web.UI.WebControls.Label lblVEHICLE_ID;
		protected System.Web.UI.WebControls.RangeValidator rngEXTENSION;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;		
		protected System.Web.UI.WebControls.Label capNAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvOTHER_DESCRIPTION;		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNAMES;
		protected System.Web.UI.WebControls.Label capADDRESS1;
		protected System.Web.UI.WebControls.TextBox txtADDRESS1;
		protected System.Web.UI.WebControls.Label capADDRESS2;
		protected System.Web.UI.WebControls.TextBox txtADDRESS2;
		protected System.Web.UI.WebControls.Label capCITY;
		protected System.Web.UI.WebControls.TextBox txtCITY;
		protected System.Web.UI.WebControls.Label capSTATE;
		protected System.Web.UI.WebControls.Label capDRIVER_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_TYPE;	
		protected System.Web.UI.WebControls.DropDownList cmbSTATE;
		protected System.Web.UI.WebControls.DropDownList cmbNAME;		
		protected System.Web.UI.WebControls.Label capVEHICLE_OWNER;
		protected System.Web.UI.WebControls.DropDownList cmbVEHICLE_OWNER;		
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
		protected System.Web.UI.WebControls.Label capDEFAULT_PHONE_TO_NOTICE;
		protected System.Web.UI.WebControls.DropDownList cmbDEFAULT_PHONE_TO_NOTICE;
		protected System.Web.UI.WebControls.Label capPRODUCTS_INSURED_IS;
		protected System.Web.UI.WebControls.DropDownList cmbPRODUCTS_INSURED_IS;
		protected System.Web.UI.WebControls.Label capOTHER_DESCRIPTION;
		protected System.Web.UI.WebControls.Label capTYPE_OF_PRODUCT;
		protected System.Web.UI.WebControls.TextBox txtTYPE_OF_PRODUCT;
		protected System.Web.UI.WebControls.Label capWHERE_PRODUCT_SEEN;
		protected System.Web.UI.WebControls.TextBox txtWHERE_PRODUCT_SEEN;
		protected System.Web.UI.WebControls.Label capOTHER_LIABILITY;
		protected System.Web.UI.WebControls.TextBox txtOTHER_LIABILITY;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlTableRow trLIABILITY_OWNER_MANF;
		protected System.Web.UI.HtmlControls.HtmlTableRow trINURED_VEHICLE;		
		protected System.Web.UI.HtmlControls.HtmlTableRow trLIABILITY_OWNER1;
		protected System.Web.UI.HtmlControls.HtmlTableRow trLIABILITY_OWNER2;
		protected System.Web.UI.HtmlControls.HtmlTableRow trLIABILITY_MANF;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTYPE_OF_HOME;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNAME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOWNER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTYPE_OF_OWNER;
		protected System.Web.UI.WebControls.TextBox txtNAME;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;	
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnName;
		protected System.Web.UI.WebControls.TextBox txtOTHER_DESCRIPTION;
		protected System.Web.UI.WebControls.Label capVEHICLE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbVEHICLE_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVEHICLE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRESET;	
		protected System.Web.UI.WebControls.Label capCOUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbCOUNTRY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOUNTRY;
		protected string strCalledComboFrom = "NAME";
		
		#endregion
	

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{						
			base.ScreenId="306_8_0";
			
			lblMessage.Visible = false;
			

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;
			
			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnDelete.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Delete;
			btnDelete.PermissionString	=	gstrSecurityXML;

			
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddOwnerDetails" ,System.Reflection.Assembly.GetExecutingAssembly());
			
			GetQueryStringValues();
			
			if(!Page.IsPostBack || hidRESET.Value=="1")
			{	
				this.cmbCOUNTRY.SelectedIndex = int.Parse(aCountry);
				if(hidRESET.Value=="1")
					ResetFields("");
				hidRESET.Value="0";
				LoadDropDowns();
				GetOldDataXML(true);		
				//cmbNAME.Attributes.Add("onChange","javascript: return LoadInsuredName();");
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
			if(hidOWNER_ID.Value!="" && hidOWNER_ID.Value!="0" && hidOWNER_ID.Value.ToUpper()!="NEW")
			{
				dsOldData	=	ClsOwnerDetails.GetOwnerDetails(int.Parse(hidOWNER_ID.Value),int.Parse(hidCLAIM_ID.Value));
				if(dsOldData!=null && dsOldData.Tables.Count>0)
				{
					hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsOldData.Tables[0]);
					if(LOAD_DATA_FLAG)
						LoadData(dsOldData);
					LoadNameDropDown();
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
			txtNAME.Text				=	"";
			if(strCalledFrom!=strCalledComboFrom)
				cmbNAME.SelectedIndex		=	-1;			
			//			txtNAME.Text				=	"";
				
		}
		#endregion

		#region LoadData function to load values into controls
		private void LoadData(DataSet dsLoadData)
		{
			try
			{
				if(dsLoadData!=null && dsLoadData.Tables.Count>0)
				{
					if(dsLoadData.Tables[0].Rows[0]["VEHICLE_OWNER"]!=null && dsLoadData.Tables[0].Rows[0]["VEHICLE_OWNER"].ToString()!="" && dsLoadData.Tables[0].Rows[0]["VEHICLE_OWNER"].ToString()!="0")
						cmbVEHICLE_OWNER.SelectedValue = dsLoadData.Tables[0].Rows[0]["VEHICLE_OWNER"].ToString();
					
					txtADDRESS1.Text		=		dsLoadData.Tables[0].Rows[0]["ADDRESS1"].ToString();
					txtADDRESS2.Text		=		dsLoadData.Tables[0].Rows[0]["ADDRESS2"].ToString();
					txtCITY.Text			=		dsLoadData.Tables[0].Rows[0]["CITY"].ToString();
					if(dsLoadData.Tables[0].Rows[0]["STATE"]!=null && dsLoadData.Tables[0].Rows[0]["STATE"].ToString()!="" && dsLoadData.Tables[0].Rows[0]["STATE"].ToString()!="0")
						cmbSTATE.SelectedValue	=		dsLoadData.Tables[0].Rows[0]["STATE"].ToString();
					if(dsLoadData.Tables[0].Rows[0]["COUNTRY"]!=null && dsLoadData.Tables[0].Rows[0]["COUNTRY"].ToString()!="" && dsLoadData.Tables[0].Rows[0]["COUNTRY"].ToString()!="0")
						cmbCOUNTRY.SelectedValue	=		dsLoadData.Tables[0].Rows[0]["COUNTRY"].ToString();
					txtZIP.Text				=		dsLoadData.Tables[0].Rows[0]["ZIP"].ToString();
					txtHOME_PHONE.Text		=		dsLoadData.Tables[0].Rows[0]["HOME_PHONE"].ToString();
					txtWORK_PHONE.Text		=		dsLoadData.Tables[0].Rows[0]["WORK_PHONE"].ToString();
					txtEXTENSION.Text		=		dsLoadData.Tables[0].Rows[0]["EXTENSION"].ToString();
					txtMOBILE_PHONE.Text	=		dsLoadData.Tables[0].Rows[0]["MOBILE_PHONE"].ToString();
	
					if(dsLoadData.Tables[0].Rows[0]["DEFAULT_PHONE_TO_NOTICE"]!=null && dsLoadData.Tables[0].Rows[0]["DEFAULT_PHONE_TO_NOTICE"].ToString()!="" && dsLoadData.Tables[0].Rows[0]["DEFAULT_PHONE_TO_NOTICE"].ToString()!="0")
						cmbDEFAULT_PHONE_TO_NOTICE.SelectedValue = dsLoadData.Tables[0].Rows[0]["DEFAULT_PHONE_TO_NOTICE"].ToString();
					if(dsLoadData.Tables[0].Rows[0]["PRODUCTS_INSURED_IS"]!=null && dsLoadData.Tables[0].Rows[0]["PRODUCTS_INSURED_IS"].ToString()!="" && dsLoadData.Tables[0].Rows[0]["PRODUCTS_INSURED_IS"].ToString()!="0")
						cmbPRODUCTS_INSURED_IS.SelectedValue = dsLoadData.Tables[0].Rows[0]["PRODUCTS_INSURED_IS"].ToString();
					txtTYPE_OF_PRODUCT.Text	=		dsLoadData.Tables[0].Rows[0]["TYPE_OF_PRODUCT"].ToString();
					txtWHERE_PRODUCT_SEEN.Text=		dsLoadData.Tables[0].Rows[0]["WHERE_PRODUCT_SEEN"].ToString();				
					txtOTHER_DESCRIPTION.Text=		dsLoadData.Tables[0].Rows[0]["OTHER_DESCRIPTION"].ToString();
					txtOTHER_LIABILITY.Text	=		dsLoadData.Tables[0].Rows[0]["OTHER_LIABILITY"].ToString();
					hidTYPE_OF_OWNER.Value	=		dsLoadData.Tables[0].Rows[0]["TYPE_OF_OWNER"].ToString();					
					string strNAME = dsLoadData.Tables[0].Rows[0]["NAME"].ToString();
					txtNAME.Text			=		strNAME;
					hidNAME.Value			=		strNAME;
					if(dsLoadData.Tables[0].Rows[0]["VEHICLE_ID"]!=null && dsLoadData.Tables[0].Rows[0]["VEHICLE_ID"].ToString()!="" && dsLoadData.Tables[0].Rows[0]["VEHICLE_ID"].ToString()!="0")
						cmbVEHICLE_ID.SelectedValue = dsLoadData.Tables[0].Rows[0]["VEHICLE_ID"].ToString();


					LoadNameDropDown();						
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}

		}
		#endregion

		private void LoadNameDropDown()
		{
			if(cmbVEHICLE_OWNER.SelectedItem!=null && cmbVEHICLE_OWNER.SelectedItem.Value!="")
			{
				string strVEHICLE_OWNER = cmbVEHICLE_OWNER.SelectedItem.Value;
				if(strVEHICLE_OWNER== ((int)enumVEHICLE_OWNER.NAMED_INSURED).ToString())
					cmbDRIVER_TYPE_SelectedIndexChanged(null,null);
				if(strVEHICLE_OWNER== ((int)enumVEHICLE_OWNER.INSURED).ToString() || strVEHICLE_OWNER== ((int)enumVEHICLE_OWNER.NAMED_INSURED).ToString())
				{
					cmbVEHICLE_OWNER_SelectedIndexChanged(null,null);
					ListItem lItem = cmbNAME.Items.FindByText(hidNAME.Value);
					cmbNAME.SelectedIndex = cmbNAME.Items.IndexOf(lItem);
				}
			}	
		}


		private void GetQueryStringValues()
		{
			if(Request.QueryString["TYPE_OF_OWNER"]!=null && Request.QueryString["TYPE_OF_OWNER"].ToString()!="")
				hidTYPE_OF_OWNER.Value = Request.QueryString["TYPE_OF_OWNER"].ToString();

			if(Request.QueryString["OWNER_ID"]!=null && Request.QueryString["OWNER_ID"].ToString()!="")
				hidOWNER_ID.Value = Request.QueryString["OWNER_ID"].ToString();
			if(Request.QueryString["CLAIM_ID"]!=null && Request.QueryString["CLAIM_ID"].ToString()!="")
				hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();

			if(Request.QueryString["CUSTOMER_ID"]!=null && Request.QueryString["CUSTOMER_ID"].ToString()!="" )
				hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString();

			if(Request.QueryString["POLICY_ID"]!=null && Request.QueryString["POLICY_ID"].ToString()!="" )
				hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"].ToString();

			if(Request.QueryString["POLICY_VERSION_ID"]!=null && Request.QueryString["POLICY_VERSION_ID"].ToString()!="" )
				hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();
			if(Request.QueryString["TYPE_OF_HOME"]!=null && Request.QueryString["TYPE_OF_HOME"].ToString()!="")
				hidTYPE_OF_HOME.Value = Request.QueryString["TYPE_OF_HOME"].ToString();
			if(Request.QueryString["LOB_ID"]!=null && Request.QueryString["LOB_ID"].ToString()!="")
				hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();			
			
		}


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{			
			this.cmbVEHICLE_OWNER.SelectedIndexChanged += new System.EventHandler(this.cmbVEHICLE_OWNER_SelectedIndexChanged);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.cmbNAME.SelectedIndexChanged += new System.EventHandler(this.cmbNAME_SelectedIndexChanged);
			this.cmbDRIVER_TYPE.SelectedIndexChanged += new System.EventHandler(this.cmbDRIVER_TYPE_SelectedIndexChanged);
			

		}
		#endregion


		private void cmbDRIVER_TYPE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(cmbDRIVER_TYPE.SelectedItem!=null && cmbDRIVER_TYPE.SelectedItem.Value!="" && cmbVEHICLE_ID.SelectedItem!=null && cmbVEHICLE_ID.SelectedItem.Value!=null && cmbVEHICLE_ID.SelectedItem.Value!="" && cmbVEHICLE_ID.SelectedItem.Value!="0")//Done for Itrack Issue 6313 on 1 Oct 2009
				{
					if(hidCUSTOMER_ID.Value!="0" && hidPOLICY_ID.Value!="0" && hidPOLICY_VERSION_ID.Value!="0" && hidCUSTOMER_ID.Value!="" && hidPOLICY_ID.Value!="" && hidPOLICY_VERSION_ID.Value!="")
					{
						DataTable dtNames = ClsOwnerDetails.GetNamedInsured(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),int.Parse(cmbVEHICLE_OWNER.SelectedItem.Value),int.Parse(hidCLAIM_ID.Value),"",cmbDRIVER_TYPE.SelectedItem.Value,0);//Added for Itrack Issue 6053 on 31 July 2009
						/*if(hidOldData.Value=="" || hidOldData.Value=="0")
								{
									txtNAME.Text = "";
									txtADDRESS1.Text = txtADDRESS2.Text = txtADDRESS2.Text = txtCITY.Text = txtLICENSE_NUMBER.Text = "";
									txtEXTENSION.Text = txtZIP.Text = txtHOME_PHONE.Text = txtRELATION_INSURED.Text = "";
									txtHOME_PHONE.Text = txtMOBILE_PHONE.Text = txtDATE_OF_BIRTH.Text = txtWORK_PHONE.Text = "";
									cmbSTATE.SelectedIndex= cmbCOUNTRY.SelectedIndex = cmbLICENSE_STATE.SelectedIndex =  -1;
								}*/
						if(sender!=null && e!=null)
							ResetFields("");
						if(dtNames!=null && dtNames.Rows.Count>0)
						{
							cmbNAME.DataSource = dtNames;
							cmbNAME.DataTextField = "NAMED_INSURED";
							cmbNAME.DataValueField = "NAMED_INSURED_ID";
							cmbNAME.DataBind();
							cmbNAME.Items.Insert(0,"");
						}
						
					}
				}
				else //Done for Itrack Issue 6313 on 1 Oct 2009
				{
					cmbVEHICLE_OWNER.SelectedIndex=0;
					cmbNAME.Items.Clear();
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
			}
			finally
			{}
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
			rfvNAME.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("34");			
			rfvNAMES.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("746");			
			revHOME_PHONE.ValidationExpression	=		  aRegExpPhone;
			revHOME_PHONE.ErrorMessage			=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");						
			revWORK_PHONE.ValidationExpression	=		  aRegExpPhone;
			revWORK_PHONE.ErrorMessage			=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");						
			revMOBILE_PHONE.ValidationExpression	=		  aRegExpPhone;
			revMOBILE_PHONE.ErrorMessage			=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");						
			rngEXTENSION.ErrorMessage			=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");//Message changed for Itrack Issue 5641
			rfvOTHER_DESCRIPTION.ErrorMessage	=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("550");
			rfvVEHICLE_ID.ErrorMessage  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("562");
			rfvCOUNTRY.ErrorMessage				=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("33");			
		}

		#endregion
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal=1;	
				//For retreiving the return value of business class save function
				ClsOwnerDetails objOwnerDetails = new ClsOwnerDetails();				

				//Retreiving the form values into model class object
				ClsOwnerDetailsInfo objOwnerDetailsInfo = GetFormValue();


				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objOwnerDetailsInfo.CREATED_BY = int.Parse(GetUserId());
					objOwnerDetailsInfo.CREATED_DATETIME = DateTime.Now;
					objOwnerDetailsInfo.IS_ACTIVE="Y"; 
					
					//Calling the add method of business layer class
					intRetVal = objOwnerDetails.Add(objOwnerDetailsInfo);

					if(intRetVal>0)
					{
						hidOWNER_ID.Value = objOwnerDetailsInfo.OWNER_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						GetOldDataXML(true);
						//EnableDisableNameField();
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
					ClsOwnerDetailsInfo objOldOwnerDetailsInfo = new ClsOwnerDetailsInfo();
//
//					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldOwnerDetailsInfo,hidOldData.Value);
//
//					//Setting those values into the Model object which are not in the page					
					objOwnerDetailsInfo.MODIFIED_BY = int.Parse(GetUserId());
					objOwnerDetailsInfo.LAST_UPDATED_DATETIME = DateTime.Now;                    
//
//					//Updating the record using business layer class object
					intRetVal	= objOwnerDetails.Update(objOldOwnerDetailsInfo,objOwnerDetailsInfo);					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						//Don't require to load the data into controls
						GetOldDataXML(false);
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
		}


		private void SetCaptions()
		{			
			capVEHICLE_OWNER.Text					=		objResourceMgr.GetString("cmbVEHICLE_OWNER");
			capNAME.Text							=		objResourceMgr.GetString("txtNAME");
			capADDRESS1.Text						=		objResourceMgr.GetString("txtADDRESS1");
			capADDRESS2.Text						=		objResourceMgr.GetString("txtADDRESS2");
			capCITY.Text							=		objResourceMgr.GetString("txtCITY");
			capSTATE.Text							=		objResourceMgr.GetString("cmbSTATE");
			capZIP.Text								=		objResourceMgr.GetString("txtZIP");
			capHOME_PHONE.Text						=		objResourceMgr.GetString("txtHOME_PHONE");
			capWORK_PHONE.Text						=		objResourceMgr.GetString("txtWORK_PHONE");
			capEXTENSION.Text						=		objResourceMgr.GetString("txtEXTENSION");
			capMOBILE_PHONE.Text					=		objResourceMgr.GetString("txtMOBILE_PHONE");
			capDEFAULT_PHONE_TO_NOTICE.Text			=		objResourceMgr.GetString("cmbDEFAULT_PHONE_TO_NOTICE");
			//capPRODUCTS_INSURED_IS.Text				=		objResourceMgr.GetString("cmbPRODUCTS_INSURED_IS");
			capOTHER_DESCRIPTION.Text				=		objResourceMgr.GetString("txtOTHER_DESCRIPTION");
			capTYPE_OF_PRODUCT.Text					=		objResourceMgr.GetString("txtTYPE_OF_PRODUCT");
			capWHERE_PRODUCT_SEEN.Text				=		objResourceMgr.GetString("txtWHERE_PRODUCT_SEEN");
			capOTHER_LIABILITY.Text					=		objResourceMgr.GetString("txtOTHER_LIABILITY");
			capWHERE_PRODUCT_SEEN.Text				=		objResourceMgr.GetString("txtWHERE_PRODUCT_SEEN");
			capVEHICLE_ID.Text						=		objResourceMgr.GetString("cmbVEHICLE_ID");
			capCOUNTRY.Text							=		objResourceMgr.GetString("cmbCOUNTRY");
			capDRIVER_TYPE.Text						=		objResourceMgr.GetString("cmbDRIVER_TYPE");
		}
	

		#region GetFormValue
		private ClsOwnerDetailsInfo GetFormValue()
		{
			ClsOwnerDetailsInfo objOwnerDetailsInfo = new ClsOwnerDetailsInfo();
			
			objOwnerDetailsInfo.ADDRESS1			=	txtADDRESS1.Text.Trim();
			objOwnerDetailsInfo.ADDRESS2			=	txtADDRESS2.Text.Trim();
			objOwnerDetailsInfo.CITY				=	txtCITY.Text.Trim();
			
			if(cmbSTATE.SelectedItem!=null && cmbSTATE.SelectedItem.Value!="")
				objOwnerDetailsInfo.STATE			=	int.Parse(cmbSTATE.SelectedItem.Value);

			objOwnerDetailsInfo.CITY				=	txtCITY.Text.Trim();
			objOwnerDetailsInfo.ZIP					=	txtZIP.Text.Trim();
			objOwnerDetailsInfo.HOME_PHONE			=	txtHOME_PHONE.Text.Trim();
			objOwnerDetailsInfo.WORK_PHONE			=	txtWORK_PHONE.Text.Trim();
			objOwnerDetailsInfo.EXTENSION			=	txtEXTENSION.Text.Trim();
			objOwnerDetailsInfo.MOBILE_PHONE		=	txtMOBILE_PHONE.Text.Trim();
	
			if(hidTYPE_OF_OWNER.Value == ((int)enumTYPE_OF_OWNER.LIABILITY_TYPE_OWNER).ToString() || hidTYPE_OF_OWNER.Value == ((int)enumTYPE_OF_OWNER.LIABILITY_TYPE_MANUFACTURER).ToString())
			{
				if(cmbDEFAULT_PHONE_TO_NOTICE.SelectedItem!=null && cmbDEFAULT_PHONE_TO_NOTICE.SelectedItem.Value!="")
					objOwnerDetailsInfo.DEFAULT_PHONE_TO_NOTICE = cmbDEFAULT_PHONE_TO_NOTICE.SelectedItem.Value;
				else
					objOwnerDetailsInfo.DEFAULT_PHONE_TO_NOTICE = "";
			}
			else
				objOwnerDetailsInfo.DEFAULT_PHONE_TO_NOTICE = "";
			if(cmbCOUNTRY.SelectedItem!=null && cmbCOUNTRY.SelectedItem.Value!="")
				objOwnerDetailsInfo.COUNTRY = int.Parse(cmbCOUNTRY.SelectedItem.Value);

			if(hidTYPE_OF_OWNER.Value == ((int)enumTYPE_OF_OWNER.LIABILITY_TYPE_OWNER).ToString())
			{
				if(cmbPRODUCTS_INSURED_IS.SelectedItem!=null && cmbPRODUCTS_INSURED_IS.SelectedItem.Value!="")
					objOwnerDetailsInfo.PRODUCTS_INSURED_IS = int.Parse(cmbPRODUCTS_INSURED_IS.SelectedItem.Value);
				objOwnerDetailsInfo.TYPE_OF_PRODUCT	= txtTYPE_OF_PRODUCT.Text.Trim();
				objOwnerDetailsInfo.WHERE_PRODUCT_SEEN = txtWHERE_PRODUCT_SEEN.Text.Trim();				
				/*Lookup values for Products Insured Is dropdown : 11749>Manufacturer
																   11750>Vendor	
																	11751>Other
				*/
				//Save description only when Other is chosen from dropdown
				if(objOwnerDetailsInfo.PRODUCTS_INSURED_IS==11751)
					objOwnerDetailsInfo.OTHER_DESCRIPTION  = txtOTHER_DESCRIPTION.Text.Trim();
				else
					objOwnerDetailsInfo.OTHER_DESCRIPTION  = "";
			}
			else
			{
				objOwnerDetailsInfo.TYPE_OF_PRODUCT = "";
				objOwnerDetailsInfo.WHERE_PRODUCT_SEEN = "";
				objOwnerDetailsInfo.PRODUCTS_INSURED_IS = 0; //to indicate that no value has been selected/chosen/available
				objOwnerDetailsInfo.OTHER_DESCRIPTION  = "";
			}

			if(hidTYPE_OF_OWNER.Value == ((int)enumTYPE_OF_OWNER.LIABILITY_TYPE_MANUFACTURER).ToString())
				objOwnerDetailsInfo.OTHER_LIABILITY	=	txtOTHER_LIABILITY.Text.Trim();			
			else
				objOwnerDetailsInfo.OTHER_LIABILITY = "";

			if(hidTYPE_OF_OWNER.Value == ((int)enumTYPE_OF_OWNER.INSURED_VEHICLE).ToString())
			{
				if(cmbVEHICLE_OWNER.SelectedItem!=null && cmbVEHICLE_OWNER.SelectedItem.Value!="")
					objOwnerDetailsInfo.VEHICLE_OWNER = int.Parse(cmbVEHICLE_OWNER.SelectedItem.Value);
			}			
			else
				objOwnerDetailsInfo.VEHICLE_OWNER = 0;
			

			objOwnerDetailsInfo.TYPE_OF_OWNER = int.Parse(hidTYPE_OF_OWNER.Value);
			
			if(objOwnerDetailsInfo.VEHICLE_OWNER== (int)enumVEHICLE_OWNER.INSURED || objOwnerDetailsInfo.VEHICLE_OWNER== (int)enumVEHICLE_OWNER.NAMED_INSURED)							
				objOwnerDetailsInfo.NAME		=	cmbNAME.SelectedItem.Text;
			else
				objOwnerDetailsInfo.NAME		=	txtNAME.Text.Trim();

			if(cmbVEHICLE_ID.SelectedItem!=null && cmbVEHICLE_ID.SelectedItem.Value!="")
				objOwnerDetailsInfo.VEHICLE_ID = int.Parse(cmbVEHICLE_ID.SelectedItem.Value);
			
			objOwnerDetailsInfo.CLAIM_ID	  = int.Parse(hidCLAIM_ID.Value);
			objOwnerDetailsInfo.TYPE_OF_HOME = hidTYPE_OF_HOME.Value;
			if(hidOWNER_ID.Value.ToUpper()=="NEW" || hidOWNER_ID.Value=="0" || hidOWNER_ID.Value=="")
				strRowId="NEW";
			else
			{
				strRowId=hidOWNER_ID.Value;
				objOwnerDetailsInfo.OWNER_ID		=	int.Parse(hidOWNER_ID.Value);
			}
			return objOwnerDetailsInfo;
		}
		#endregion

		#region LoadDropDowns
		private void LoadDropDowns()
		{
			string sVal = "";
			string sText = "";
			DataTable dtState = new DataTable();
			try
			{
				//Loading of states list
				dtState					= Cms.CmsWeb.ClsFetcher.State;
				cmbSTATE.DataSource		= dtState;
				cmbSTATE.DataTextField	= "STATE_NAME";
				cmbSTATE.DataValueField	= "STATE_ID";
				cmbSTATE.DataBind();
				cmbSTATE.Items.Insert(0,"");

				dtState						= Cms.CmsWeb.ClsFetcher.Country;
				cmbCOUNTRY.DataSource		= dtState;
				cmbCOUNTRY.DataTextField	= "COUNTRY_NAME";
				cmbCOUNTRY.DataValueField	= "COUNTRY_ID";
				cmbCOUNTRY.DataBind();

				//Loading of Default Phone to Notice dropdown list
				cmbDEFAULT_PHONE_TO_NOTICE.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CLMDPN");
				cmbDEFAULT_PHONE_TO_NOTICE.DataTextField	= "LookupDesc";
				cmbDEFAULT_PHONE_TO_NOTICE.DataValueField	= "LookupID";
				cmbDEFAULT_PHONE_TO_NOTICE.DataBind();
				cmbDEFAULT_PHONE_TO_NOTICE.Items.Insert(0,"");				
				cmbDEFAULT_PHONE_TO_NOTICE.SelectedIndex = 1;

				//Loading of Products Insured is dropdown list
				cmbPRODUCTS_INSURED_IS.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CLPI");
				cmbPRODUCTS_INSURED_IS.DataTextField	= "LookupDesc";
				cmbPRODUCTS_INSURED_IS.DataValueField	= "LookupID";
				cmbPRODUCTS_INSURED_IS.DataBind();
				cmbPRODUCTS_INSURED_IS.Items.Insert(0,"");

				//Load Owner of Vehicle dropdown only in the case of Insured Vehicle
				if(hidTYPE_OF_OWNER.Value == ((int)enumTYPE_OF_OWNER.INSURED_VEHICLE).ToString())
				{
					cmbVEHICLE_OWNER.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CLVO");
					cmbVEHICLE_OWNER.DataTextField	= "LookupDesc";
					cmbVEHICLE_OWNER.DataValueField	= "LookupID";
					cmbVEHICLE_OWNER.DataBind();		
					Cms.BusinessLayer.BlCommon.ClsCommon.RemoveOptionFromDropdownByValue(cmbVEHICLE_OWNER,"14151");
					cmbVEHICLE_OWNER.Items.Insert(0,"");
					cmbVEHICLE_OWNER.SelectedIndex=2;
					LoadNameDropDown();
				}

				ClsInsuredVehicle objInsuredVehicle = new ClsInsuredVehicle();
				DataTable dtPolicyVehicles = objInsuredVehicle.GetClaimVehicles(int.Parse(hidCLAIM_ID.Value));

				if(dtPolicyVehicles!=null && dtPolicyVehicles.Rows.Count>0)
				{
					//Add an empty listitem when there are multiple vehicles added
					if(dtPolicyVehicles.Rows.Count>1)
						cmbVEHICLE_ID.Items.Add(new ListItem("",""));
					foreach(DataRow dtRow in dtPolicyVehicles.Rows)
					{
						sVal = "";
						sText = "";
						for(int i=0;i<dtPolicyVehicles.Columns.Count-1;i++)
							sText+= dtRow[i].ToString() + "-";

						sText = sText.Substring(0,sText.Length-1);
						if(dtRow["VEHICLE_ID"]!=null && dtRow["VEHICLE_ID"].ToString()!="")
							sVal = dtRow["VEHICLE_ID"].ToString();
						
						ListItem lItem = new ListItem(sText,sVal);
						cmbVEHICLE_ID.Items.Add(lItem);
					}
					lblVEHICLE_ID.Visible = false;
				}
				else
				{
					lblVEHICLE_ID.Text	=  "No vehicle added until now. Please click <a href='javascript:Redirect();' onclick='Redirect();'>here</a> to add vehicle";
					cmbVEHICLE_ID.Attributes.Add("style","display:none");					
				}
				
			}
			catch//(Exception ex)
			{
			}
			finally
			{
				if(dtState!=null)
					dtState.Dispose();
			}
			
			
		}
		#endregion


		private void cmbVEHICLE_OWNER_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(cmbVEHICLE_OWNER.SelectedItem!=null && cmbVEHICLE_OWNER.SelectedItem.Value!="")
			{
				if(sender!=null && e!=null)
					ResetFields("");
				string strVEHICLE_OWNER = cmbVEHICLE_OWNER.SelectedItem.Value;
				if(strVEHICLE_OWNER== ((int)enumVEHICLE_OWNER.INSURED).ToString())
				{
					if(hidCUSTOMER_ID.Value!="0" && hidPOLICY_ID.Value!="0" && hidPOLICY_VERSION_ID.Value!="0" && hidCUSTOMER_ID.Value!="" && hidPOLICY_ID.Value!="" && hidPOLICY_VERSION_ID.Value!="")
					{
						DataTable dtNames = ClsOwnerDetails.GetNamedInsured(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),int.Parse(cmbVEHICLE_OWNER.SelectedItem.Value),int.Parse(hidCLAIM_ID.Value),0);//Added for Itrack Issue 6053 on 31 July 2009
						if(hidOldData.Value=="" || hidOldData.Value=="0")
						{
							txtNAME.Text = txtADDRESS1.Text = txtADDRESS2.Text = txtADDRESS2.Text = txtCITY.Text = "";
							txtEXTENSION.Text = txtZIP.Text = txtHOME_PHONE.Text = "";
							txtHOME_PHONE.Text = txtMOBILE_PHONE.Text = txtWORK_PHONE.Text = "";
							cmbSTATE.SelectedIndex= cmbCOUNTRY.SelectedIndex = -1;
						}
						if(dtNames!=null && dtNames.Rows.Count>0)
						{
							cmbNAME.DataSource = dtNames;
							cmbNAME.DataTextField = "NAMED_INSURED";
							cmbNAME.DataValueField = "NAMED_INSURED_ID";
							cmbNAME.DataBind();
							cmbNAME.Items.Insert(0,"");
						}
						
					}

				}
				else if(strVEHICLE_OWNER==((int)enumVEHICLE_OWNER.NAMED_INSURED).ToString()) 
				{
					cmbDRIVER_TYPE.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("WTRPO",null,"Y");
					cmbDRIVER_TYPE.DataTextField	= "LookupDesc";
					cmbDRIVER_TYPE.DataValueField	= "LookupCode";
					cmbDRIVER_TYPE.DataBind();
					cmbDRIVER_TYPE.Items.Add("Other");
					cmbDRIVER_TYPE_SelectedIndexChanged(null,null);
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
		}

		private void cmbNAME_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(cmbNAME.SelectedValue !="")
			{
				if(hidCUSTOMER_ID.Value!="0" && hidPOLICY_ID.Value!="0" && hidPOLICY_VERSION_ID.Value!="0" && hidCUSTOMER_ID.Value!="" && hidPOLICY_ID.Value!="" && hidPOLICY_VERSION_ID.Value!="")
				{
//					DataTable dtNames = ClsOwnerDetails.GetNamedInsured(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),int.Parse(cmbVEHICLE_OWNER.SelectedItem.Value),int.Parse(hidCLAIM_ID.Value),cmbNAME.SelectedValue);
					string strDriverType="";
					if(cmbDRIVER_TYPE.SelectedItem!=null && cmbDRIVER_TYPE.SelectedItem.Value!="")
						strDriverType = cmbDRIVER_TYPE.SelectedItem.Value;
					DataTable dtNames = ClsOwnerDetails.GetNamedInsured(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),int.Parse(cmbVEHICLE_OWNER.SelectedItem.Value),int.Parse(hidCLAIM_ID.Value),cmbNAME.SelectedValue,strDriverType,0);//Added for Itrack Issue 6053 on 31 July 2009
					if(sender!=null && e!=null)
						ResetFields(strCalledComboFrom);
					if(dtNames!=null && dtNames.Rows.Count>0)
					{
						DataRow drTemp = dtNames.Rows[0];
						if(drTemp["ADDRESS1"].ToString().Trim()!="")
						{
							txtADDRESS1.Text = drTemp["ADDRESS1"].ToString().Trim();
							txtADDRESS1.Enabled = false;
						}
						else
							txtADDRESS1.Enabled = true;

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

						
					}
					else
					{
						txtADDRESS1.Enabled = txtADDRESS2.Enabled = txtADDRESS2.Enabled = txtCITY.Enabled =  true;	
						cmbSTATE.Enabled = txtZIP.Enabled = txtHOME_PHONE.Enabled = cmbCOUNTRY.Enabled =  true;
						txtHOME_PHONE.Enabled = txtMOBILE_PHONE.Enabled = txtWORK_PHONE.Enabled = true;
						txtEXTENSION.Enabled =  true;
					}
						
				}
			}
			else				
				ResetFields(strCalledComboFrom);
		}
	}
}
