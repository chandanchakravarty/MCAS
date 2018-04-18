/******************************************************************************************
	<Author					: - > Sumit Chhabra
	<Start Date				: -	> April 20,2006
	<End Date				: - >
	<Description			: - > Page is used to assign limits to authority
	<Review Date			: - >
	<Reviewed By			: - >
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
using System.Globalization;



namespace Cms.Claims.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class AddPropertyDamaged : Cms.Claims.ClaimBase
	{
		#region Page Control Variables 
		protected System.Web.UI.HtmlControls.HtmlTableRow trPARTY_TYPE;
		protected System.Web.UI.WebControls.CustomValidator csvDESCRIPTION;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESCRIPTION;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOUNTRY;
		protected System.Web.UI.WebControls.RangeValidator rngVEHICLE_YEAR;
		protected System.Web.UI.WebControls.RegularExpressionValidator revESTIMATE_AMOUNT;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capPARTY_TYPE_DESC;
		protected System.Web.UI.WebControls.TextBox txtPARTY_TYPE_DESC;
		protected System.Web.UI.WebControls.CustomValidator csvPARTY_TYPE_DESC;
		protected System.Web.UI.WebControls.Label capDAMAGED_ANOTHER_VEHICLE;
		protected System.Web.UI.WebControls.DropDownList cmbDAMAGED_ANOTHER_VEHICLE;		
		protected System.Web.UI.WebControls.Label capPARTY_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbPARTY_TYPE;
		protected System.Web.UI.WebControls.Label capPROP_DAMAGED_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbPROP_DAMAGED_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPROP_DAMAGED_TYPE;
		protected System.Web.UI.WebControls.Label capVIN;
		protected System.Web.UI.WebControls.TextBox txtVIN;		
		protected System.Web.UI.WebControls.Label capVEHICLE_YEAR;
		protected System.Web.UI.WebControls.TextBox txtVEHICLE_YEAR;
		protected System.Web.UI.WebControls.Label capMAKE;
		protected System.Web.UI.WebControls.TextBox txtMAKE;
		protected System.Web.UI.WebControls.Label capMODEL;
		protected System.Web.UI.WebControls.TextBox txtMODEL;
		protected System.Web.UI.WebControls.Label capBODY_TYPE;
		protected System.Web.UI.WebControls.TextBox txtBODY_TYPE;
		protected System.Web.UI.WebControls.Label capPLATE_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtPLATE_NUMBER;
		protected System.Web.UI.WebControls.Label capDESCRIPTION;
		protected System.Web.UI.WebControls.TextBox txtDESCRIPTION;
		protected System.Web.UI.WebControls.DropDownList cmbOTHER_INSURANCE;
		protected System.Web.UI.WebControls.Label capAGENCY_NAME;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_NAME;
		protected System.Web.UI.WebControls.Label capPOLICY_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtPOLICY_NUMBER;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPROPERTY_DAMAGED_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.WebControls.Label capOTHER_INSURANCE;		
		protected string strRowId="NEW";
		private bool LOAD_OLD_DATA = true;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVIN;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidYEAR;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMAKE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMODEL;
		System.Resources.ResourceManager objResourceMgr;
		protected System.Web.UI.WebControls.Label capESTIMATE_AMOUNT;
		protected System.Web.UI.WebControls.TextBox txtESTIMATE_AMOUNT;
        //protected System.Web.UI.WebControls.RangeValidator rngESTIMATE_AMOUNT;
		protected System.Web.UI.WebControls.Label capADDRESS1;
		protected System.Web.UI.WebControls.TextBox txtADDRESS1;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvADDRESS1;
		protected System.Web.UI.WebControls.Label capADDRESS2;
		protected System.Web.UI.WebControls.TextBox txtADDRESS2;
		protected System.Web.UI.WebControls.Label capCITY;
		protected System.Web.UI.WebControls.TextBox txtCITY;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCITY;
		protected System.Web.UI.WebControls.Label capCOUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbCOUNTRY;
		protected System.Web.UI.WebControls.Label capSTATE;
		protected System.Web.UI.WebControls.DropDownList cmbSTATE;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATE;
		protected System.Web.UI.WebControls.Label capZIP;
		protected System.Web.UI.WebControls.TextBox txtZIP;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revZIP;
        protected System.Web.UI.WebControls.Label capHeader;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidtext;
		#endregion

		#region PageLoad event

        public NumberFormatInfo nfi;

    
		private void Page_Load(object sender, System.EventArgs e)
		{						
			base.ScreenId="306_6_0";
			
			lblMessage.Visible = false;
           

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass			=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;
			
			btnSave.CmsButtonClass			=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddPropertyDamaged" ,System.Reflection.Assembly.GetExecutingAssembly());

            if (GetPolicyCurrency() != String.Empty && GetPolicyCurrency() == enumCurrencyId.BR)
                nfi = new CultureInfo(enumCulture.BR, true).NumberFormat;
            else
                nfi = new CultureInfo(enumCulture.US, true).NumberFormat;

			if(!Page.IsPostBack)
			{			
				GetQueryStringValues();
				txtPLATE_NUMBER.Attributes.Add("onKeyPress","javascript: return (this.value = this.value.toUpperCase());");
				txtPLATE_NUMBER.Attributes.Add("onBlur","javascript: return (this.value = this.value.toUpperCase());");
				cmbOTHER_INSURANCE.Attributes.Add("onChange","javascript: return cmbOTHER_INSURANCE_Change();");
				cmbPROP_DAMAGED_TYPE.Attributes.Add("onChange","javascript: return cmbPROP_DAMAGED_TYPE_Change(true);");				
				cmbDAMAGED_ANOTHER_VEHICLE.Attributes.Add("onChange","javascript : return ShowHideFields();");
				btnReset.Attributes.Add("onclick","javascript:return ResetTheForm();");
               // txtESTIMATE_AMOUNT.Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value);ValidatorOnChange();");//Added for Itrack Issue 5639 on 29 April 2009
                txtESTIMATE_AMOUNT.Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value);");
				LoadDropDown();
				//txtESTIMATE_AMOUNT.Attributes.Add("onBlur","javascript: this.value = formatCurrencyWithCents(this.value);");
				GetOldDataXML(LOAD_OLD_DATA);
				SetCaptions();
				SetErrorMessages();
                
			}
		}
		#endregion

		private void LoadDropDown()
		{
			
			try
			{
                if (Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("PDTYP").Select("", "LookupDesc").Length > 0)
                {
                    cmbPROP_DAMAGED_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("PDTYP").Select("", "LookupDesc").CopyToDataTable<DataRow>();
                }
                else
                {
                    cmbPROP_DAMAGED_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("PDTYP");
                }
                    //cmbPROP_DAMAGED_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PDTYP",null,'S');
                    cmbPROP_DAMAGED_TYPE.DataTextField = "LookupDesc";
                    cmbPROP_DAMAGED_TYPE.DataValueField = "LookupID";
                    cmbPROP_DAMAGED_TYPE.DataBind();
                    cmbPROP_DAMAGED_TYPE.Items.Insert(0, new ListItem("", ""));
              


                // Added by santosh kumar gautam on 18 dec 2010

               
                IList LookupData = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
                cmbDAMAGED_ANOTHER_VEHICLE.DataSource = LookupData;
                cmbDAMAGED_ANOTHER_VEHICLE.DataTextField = "LookupDesc";
                cmbDAMAGED_ANOTHER_VEHICLE.DataValueField = "LookupID";
                cmbDAMAGED_ANOTHER_VEHICLE.DataBind();

                cmbOTHER_INSURANCE.DataSource = LookupData;
                cmbOTHER_INSURANCE.DataTextField = "LookupDesc";
                cmbOTHER_INSURANCE.DataValueField = "LookupID";
                cmbOTHER_INSURANCE.DataBind();

                

				cmbPARTY_TYPE.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PR_PRT",null,"S");
				cmbPARTY_TYPE.DataTextField	= "LookupDesc";
				cmbPARTY_TYPE.DataValueField	= "LookupID";
				cmbPARTY_TYPE.DataBind();
				cmbPARTY_TYPE.Items.Insert(0,new ListItem("",""));
			}
			catch (Exception ex)
			{
				trPARTY_TYPE.Attributes.Add("style","display:none");
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			

			if(hidLOB_ID.Value==((int)enumLOB.HOME).ToString() || hidLOB_ID.Value==((int)enumLOB.REDW).ToString())
			{
				ListItem li = cmbPROP_DAMAGED_TYPE.Items.FindByValue(ClsPropertyDamaged.PROP_DAMAGED_TYPE_VEHICLE);
				if(li!=null)
				{
					cmbPROP_DAMAGED_TYPE.Items.Remove(li);
				}
			}

			DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
			cmbCOUNTRY.DataSource		= dt;
			cmbCOUNTRY.DataTextField	= "Country_Name";
			cmbCOUNTRY.DataValueField	= "Country_Id";
			cmbCOUNTRY.DataBind();			

			dt = Cms.CmsWeb.ClsFetcher.State; 
			cmbSTATE.DataSource		= dt;
			cmbSTATE.DataTextField	= "State_Name";
			cmbSTATE.DataValueField	= "State_Id";
			cmbSTATE.DataBind();
			cmbSTATE.Items.Insert(0,"");
		}

		#region GetOldDataXML
		private void GetOldDataXML(bool LOAD_DATA_FLAG)
		{
			if(hidPROPERTY_DAMAGED_ID.Value!="" && hidPROPERTY_DAMAGED_ID.Value!="0")
			{
				DataTable dtOldData = 	ClsPropertyDamaged.GetXmlForPageControls(hidCLAIM_ID.Value,hidPROPERTY_DAMAGED_ID.Value);
				hidOldData.Value	=	Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dtOldData);
				if(LOAD_DATA_FLAG)
					LoadData(dtOldData);
			}
			else
				hidOldData.Value	=	"";
		}
		#endregion

		#region Get Query String Values
		private void GetQueryStringValues()
		{
			if(Request["PROPERTY_DAMAGED_ID"]!=null && Request["PROPERTY_DAMAGED_ID"].ToString()!="")
				hidPROPERTY_DAMAGED_ID.Value = Request["PROPERTY_DAMAGED_ID"].ToString();

			if(Request.QueryString["CLAIM_ID"]!=null && Request.QueryString["CLAIM_ID"].ToString()!="" )
				hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();

			if(Request.QueryString["CUSTOMER_ID"]!=null && Request.QueryString["CUSTOMER_ID"].ToString()!="" )
				hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString();

			if(Request.QueryString["POLICY_ID"]!=null && Request.QueryString["POLICY_ID"].ToString()!="" )
				hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"].ToString();

			if(Request.QueryString["POLICY_VERSION_ID"]!=null && Request.QueryString["POLICY_VERSION_ID"].ToString()!="" )
				hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();

			if(Request.QueryString["LOB_ID"]!=null && Request.QueryString["LOB_ID"].ToString()!="" )
				hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();			

		}
		#endregion

		#region Load Old Data value into fields
		private void LoadData(DataTable dtLoadData)
		{
			if(dtLoadData!=null && dtLoadData.Rows.Count>0)
			{
				if(dtLoadData.Rows[0]["DAMAGED_ANOTHER_VEHICLE"]!=null && dtLoadData.Rows[0]["DAMAGED_ANOTHER_VEHICLE"].ToString()!="")
					cmbDAMAGED_ANOTHER_VEHICLE.SelectedValue = dtLoadData.Rows[0]["DAMAGED_ANOTHER_VEHICLE"].ToString();

				
				
				if(dtLoadData.Rows[0]["PROP_DAMAGED_TYPE"]!=null && dtLoadData.Rows[0]["PROP_DAMAGED_TYPE"].ToString()!="" && dtLoadData.Rows[0]["PROP_DAMAGED_TYPE"].ToString()!="0")
					cmbPROP_DAMAGED_TYPE.SelectedValue = dtLoadData.Rows[0]["PROP_DAMAGED_TYPE"].ToString();
				if(dtLoadData.Rows[0]["PARTY_TYPE"].ToString()!="" && dtLoadData.Rows[0]["PARTY_TYPE"].ToString()!="0")
					cmbPARTY_TYPE.SelectedValue = dtLoadData.Rows[0]["PARTY_TYPE"].ToString();
				
					
				txtVIN.Text = dtLoadData.Rows[0]["VIN"].ToString();
				txtVEHICLE_YEAR.Text  = dtLoadData.Rows[0]["VEHICLE_YEAR"].ToString();
				txtMAKE.Text = dtLoadData.Rows[0]["MAKE"].ToString();
				txtMODEL.Text  = dtLoadData.Rows[0]["MODEL"].ToString();
				txtBODY_TYPE.Text = dtLoadData.Rows[0]["BODY_TYPE"].ToString();
				txtPLATE_NUMBER.Text  = dtLoadData.Rows[0]["PLATE_NUMBER"].ToString();
                //txtDESCRIPTION.Text = dtLoadData.Rows[0]["DESCRIPTION"].ToString();
				txtADDRESS1.Text = dtLoadData.Rows[0]["ADDRESS1"].ToString();
				txtADDRESS2.Text = dtLoadData.Rows[0]["ADDRESS2"].ToString();
				txtCITY.Text = dtLoadData.Rows[0]["CITY"].ToString();
				if(dtLoadData.Rows[0]["COUNTRY"]!=null && dtLoadData.Rows[0]["COUNTRY"].ToString()!="" && dtLoadData.Rows[0]["COUNTRY"].ToString()!="0")
					cmbCOUNTRY.SelectedValue = dtLoadData.Rows[0]["COUNTRY"].ToString();
				if(dtLoadData.Rows[0]["STATE"]!=null && dtLoadData.Rows[0]["STATE"].ToString()!="" && dtLoadData.Rows[0]["STATE"].ToString()!="0")
					cmbSTATE.SelectedValue = dtLoadData.Rows[0]["STATE"].ToString();
				txtZIP.Text = dtLoadData.Rows[0]["ZIP"].ToString();
					
				txtDESCRIPTION.Text = dtLoadData.Rows[0]["DESCRIPTION"].ToString();
				txtPARTY_TYPE_DESC.Text = dtLoadData.Rows[0]["PARTY_TYPE_DESC"].ToString();
				
				if(dtLoadData.Rows[0]["OTHER_INSURANCE"] != null && dtLoadData.Rows[0]["OTHER_INSURANCE"].ToString().Trim() != "")
					cmbOTHER_INSURANCE.SelectedValue = dtLoadData.Rows[0]["OTHER_INSURANCE"].ToString();
                if (cmbOTHER_INSURANCE.SelectedValue == "10963")
				{
					txtAGENCY_NAME.Text  = dtLoadData.Rows[0]["AGENCY_NAME"].ToString();
					txtPOLICY_NUMBER.Text = dtLoadData.Rows[0]["POLICY_NUMBER"].ToString();				
				}
				//txtESTIMATE_AMOUNT.Text = String.Format("{0:,#,###}",Convert.ToInt64(dtLoadData.Rows[0]["ESTIMATE_AMOUNT"]));
				if(dtLoadData.Rows[0]["ESTIMATE_AMOUNT"]!=null && dtLoadData.Rows[0]["ESTIMATE_AMOUNT"].ToString()!="" && dtLoadData.Rows[0]["ESTIMATE_AMOUNT"].ToString()!="0")
                    txtESTIMATE_AMOUNT.Text = Double.Parse(dtLoadData.Rows[0]["ESTIMATE_AMOUNT"].ToString()).ToString("N", nfi);// Changed by santosh kumar gautam on 16 dec 2010			
			}
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion		

		#region Set Validators ErrorMessages
		private void SetErrorMessages()
		{
			csvDESCRIPTION.ErrorMessage		=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvDESCRIPTION.ErrorMessage		=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"13");						
			rngVEHICLE_YEAR.MaximumValue	= (DateTime.Now.Year+1).ToString();
			rngVEHICLE_YEAR.MinimumValue	= aAppMinYear  ;			
			rngVEHICLE_YEAR.ErrorMessage	=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("673");
            //rngESTIMATE_AMOUNT.ErrorMessage			=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");	
//			rfvADDRESS1.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
//			rfvCITY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
//			rfvSTATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
//			rfvZIP.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"8");
//			rfvCOUNTRY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9");
			//csvDESCRIPTION.ErrorMessage         = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("442");  
			//revZIP.ErrorMessage				= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("24");
            revZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("24");
			revZIP.ValidationExpression		= aRegExpZip;
			rfvPROP_DAMAGED_TYPE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			revESTIMATE_AMOUNT.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "216");//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
			//revESTIMATE_AMOUNT.ValidationExpression = aRegExpDoublePositiveNonZero;			
			//revESTIMATE_AMOUNT.ValidationExpression = aRegExpCurrencyformat;//Done by Sibin on 2 Feb 09 for Itrack Issue 5385		
			//revESTIMATE_AMOUNT.ValidationExpression = aRegExpDoublePositiveZero;
            revESTIMATE_AMOUNT.ValidationExpression = aRegExpCurrencyformat;//Added for Itrack Issue 5639 on 29 April 2009
			csvPARTY_TYPE_DESC.ErrorMessage =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"11");
            capHeader.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            hidtext.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1306");
		}
		#endregion

		#region Web Event Handlers
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	
				//For retreiving the return value of business class save function
				ClsPropertyDamaged objPropertyDamaged = new ClsPropertyDamaged();				

				//Retreiving the form values into model class object
				ClsPropertyDamagedInfo objPropertyDamagedInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objPropertyDamagedInfo.CREATED_BY = int.Parse(GetUserId());
                    objPropertyDamagedInfo.CREATED_DATETIME = ConvertToDate(DateTime.Now.ToString());
					objPropertyDamagedInfo.IS_ACTIVE="Y"; 
					
					//Calling the add method of business layer class
					intRetVal = objPropertyDamaged.Add(objPropertyDamagedInfo);

					if(intRetVal>0)
					{
						hidPROPERTY_DAMAGED_ID.Value = objPropertyDamagedInfo.PROPERTY_DAMAGED_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						GetOldDataXML(!LOAD_OLD_DATA);
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
					ClsPropertyDamagedInfo objOldPropertyDamagedInfo = new ClsPropertyDamagedInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldPropertyDamagedInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page					
					objPropertyDamagedInfo.MODIFIED_BY = int.Parse(GetUserId());
                    objPropertyDamagedInfo.LAST_UPDATED_DATETIME = ConvertToDate(DateTime.Now.ToString());                    

					//Updating the record using business layer class object
					intRetVal	= objPropertyDamaged.Update(objOldPropertyDamagedInfo,objPropertyDamagedInfo);					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML(!LOAD_OLD_DATA);
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
		#endregion

		#region SetCaptions
		private void SetCaptions()
		{
			capDAMAGED_ANOTHER_VEHICLE.Text				=		objResourceMgr.GetString("cmbDAMAGED_ANOTHER_VEHICLE");			
			capVIN.Text									=		objResourceMgr.GetString("txtVIN");			
			capVEHICLE_YEAR.Text						=		objResourceMgr.GetString("txtVEHICLE_YEAR");			
			capMAKE.Text								=		objResourceMgr.GetString("txtMAKE");			
			capMODEL.Text								=		objResourceMgr.GetString("txtMODEL");			
			capBODY_TYPE.Text							=		objResourceMgr.GetString("txtBODY_TYPE");			
			capPLATE_NUMBER.Text						=		objResourceMgr.GetString("txtPLATE_NUMBER");			
			capDESCRIPTION.Text							=		objResourceMgr.GetString("txtDESCRIPTION");			
			capOTHER_INSURANCE.Text						=		objResourceMgr.GetString("cmbOTHER_INSURANCE");			
			capAGENCY_NAME.Text							=		objResourceMgr.GetString("txtAGENCY_NAME");			
			capPOLICY_NUMBER.Text						=		objResourceMgr.GetString("txtPOLICY_NUMBER");
			capESTIMATE_AMOUNT.Text					    =		objResourceMgr.GetString("txtESTIMATE_AMOUNT");	
			capADDRESS1.Text		                     =		objResourceMgr.GetString("txtADDRESS1");
			capADDRESS2.Text		=		objResourceMgr.GetString("txtADDRESS2");
			capCITY.Text		=		objResourceMgr.GetString("txtCITY");
			capCOUNTRY.Text		=		objResourceMgr.GetString("cmbCOUNTRY");
			capSTATE.Text		=		objResourceMgr.GetString("cmbSTATE");
			capZIP.Text			=		objResourceMgr.GetString("txtZIP");
			capPROP_DAMAGED_TYPE.Text			=		objResourceMgr.GetString("cmbPROP_DAMAGED_TYPE");
			capPARTY_TYPE.Text			=		objResourceMgr.GetString("cmbPARTY_TYPE");
			capPARTY_TYPE_DESC.Text			=		objResourceMgr.GetString("txtPARTY_TYPE_DESC");
		}
		#endregion
	
		#region GetFormValue
		private ClsPropertyDamagedInfo GetFormValue()
		{
			ClsPropertyDamagedInfo objPropertyDamagedInfo = new ClsPropertyDamagedInfo();
			if(cmbDAMAGED_ANOTHER_VEHICLE.SelectedItem!=null && cmbDAMAGED_ANOTHER_VEHICLE.SelectedItem.Value!="")
				objPropertyDamagedInfo.DAMAGED_ANOTHER_VEHICLE = int.Parse(cmbDAMAGED_ANOTHER_VEHICLE.SelectedItem.Value);

			if(cmbPARTY_TYPE.SelectedItem!=null && cmbPARTY_TYPE.SelectedItem.Value!="")
				objPropertyDamagedInfo.PARTY_TYPE = int.Parse(cmbPARTY_TYPE.SelectedItem.Value);
			objPropertyDamagedInfo.PARTY_TYPE_DESC = txtPARTY_TYPE_DESC.Text.Trim();
            if (objPropertyDamagedInfo.DAMAGED_ANOTHER_VEHICLE == 10963)//for yes
			{
				if(cmbPROP_DAMAGED_TYPE.SelectedItem!=null && cmbPROP_DAMAGED_TYPE.SelectedItem.Value!="")
					objPropertyDamagedInfo.PROP_DAMAGED_TYPE = int.Parse(cmbPROP_DAMAGED_TYPE.SelectedValue);
				
				if(txtDESCRIPTION.Text.Trim()!="")
					objPropertyDamagedInfo.DESCRIPTION			=	txtDESCRIPTION.Text.Trim();

				switch(objPropertyDamagedInfo.PROP_DAMAGED_TYPE.ToString())
				{
					case ClsPropertyDamaged.PROP_DAMAGED_TYPE_VEHICLE:
						objPropertyDamagedInfo.VIN	=	txtVIN.Text.Trim();
						objPropertyDamagedInfo.VEHICLE_YEAR			=	txtVEHICLE_YEAR.Text.Trim();
						objPropertyDamagedInfo.MAKE					=	txtMAKE.Text.Trim();
						objPropertyDamagedInfo.MODEL				=	txtMODEL.Text.Trim();
						objPropertyDamagedInfo.BODY_TYPE			=	txtBODY_TYPE.Text.Trim();
						objPropertyDamagedInfo.PLATE_NUMBER			=	txtPLATE_NUMBER.Text.Trim();
						break;
					case ClsPropertyDamaged.PROP_DAMAGED_TYPE_HOME:
						objPropertyDamagedInfo.ADDRESS1=	txtADDRESS1.Text;
						objPropertyDamagedInfo.ADDRESS2=	txtADDRESS2.Text;
						objPropertyDamagedInfo.CITY=	txtCITY.Text;
						if (cmbCOUNTRY.SelectedItem!=null && cmbCOUNTRY.SelectedItem.Value!="")
							objPropertyDamagedInfo.COUNTRY=	int.Parse(cmbCOUNTRY.SelectedItem.Value) ;			
						if (cmbSTATE.SelectedItem!=null && cmbSTATE.SelectedItem.Value!="")
							objPropertyDamagedInfo.STATE=	int.Parse(cmbSTATE.SelectedValue);
						objPropertyDamagedInfo.ZIP=	txtZIP.Text;
					break;
//					case ClsPropertyDamaged.PROP_DAMAGED_TYPE_OTHER:
//						objPropertyDamagedInfo.DESCRIPTION			=	txtDESCRIPTION.Text.Trim();
//						break;
					default:
						break;
				}
			}
						
			if(cmbOTHER_INSURANCE.SelectedItem!=null && cmbOTHER_INSURANCE.SelectedItem.Value!="")
				objPropertyDamagedInfo.OTHER_INSURANCE	=	int.Parse(cmbOTHER_INSURANCE.SelectedItem.Value);
            if (objPropertyDamagedInfo.OTHER_INSURANCE == 10963)//FOR YES
			{
				objPropertyDamagedInfo.AGENCY_NAME			=	txtAGENCY_NAME.Text.Trim();
				objPropertyDamagedInfo.POLICY_NUMBER		=	txtPOLICY_NUMBER.Text.Trim();
			}
			if(hidCLAIM_ID.Value!="" && hidCLAIM_ID.Value!="0")
				objPropertyDamagedInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);			
			if(txtESTIMATE_AMOUNT.Text.Trim()!="")
				objPropertyDamagedInfo.ESTIMATE_AMOUNT = Convert.ToDouble(txtESTIMATE_AMOUNT.Text.Trim(),nfi);

			if(hidPROPERTY_DAMAGED_ID.Value=="" || hidPROPERTY_DAMAGED_ID.Value=="0")			
			{
				strRowId = "NEW";
			}
			else
			{
				objPropertyDamagedInfo.PROPERTY_DAMAGED_ID = int.Parse(hidPROPERTY_DAMAGED_ID.Value);
				strRowId = hidPROPERTY_DAMAGED_ID.Value;
			}
			
			return objPropertyDamagedInfo;
		}
		#endregion

        protected void txtPARTY_TYPE_DESC_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtPARTY_TYPE_DESC_TextChanged1(object sender, EventArgs e)
        {

        }
	
	}
}
