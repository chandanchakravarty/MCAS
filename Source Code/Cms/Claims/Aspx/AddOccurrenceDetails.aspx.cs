/******************************************************************************************
	<Author					: - > Vijay Arora
	<Start Date				: -	> May 04, 2006
	<End Date				: - >
	<Description			: - > Presentation Layer class for occurrance details.
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
using Cms.CustomException;
using Cms.CmsWeb.WebControls;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Claims;
using Cms.BusinessLayer.BlApplication;
using Cms.CmsWeb;
using Cms.BusinessLayer.BLClaims;
using Cms.CmsWeb.Controls;
namespace  Cms.Claims.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class AddOccurrenceDetails : Cms.Claims.ClaimBase
	{
		#region Page Control Variable
		protected System.Web.UI.WebControls.CustomValidator csvLOSS_LOCATION;
	
		protected System.Web.UI.WebControls.CustomValidator csvVIOLATION;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.WebControls.Label capDATE_OF_LOSS;
		protected System.Web.UI.WebControls.Label lblDATE_OF_LOSS;
		protected System.Web.UI.WebControls.Label capLOSS_TIME;
		protected System.Web.UI.WebControls.Label lblLOSS_HOUR;
		protected System.Web.UI.WebControls.Label lblLOSS_MIN;
		protected System.Web.UI.WebControls.Label lblLOSS_AM_PM;
		//protected System.Web.UI.WebControls.DropDownList cmbLOSS_TYPE;
		protected System.Web.UI.WebControls.Label capLOSS_LOCATION;
		protected System.Web.UI.WebControls.TextBox txtLOSS_LOCATION;
		protected System.Web.UI.WebControls.Label capESTIMATE_AMOUNT;
		protected System.Web.UI.WebControls.TextBox txtESTIMATE_AMOUNT;
		protected System.Web.UI.WebControls.Label capOTHER_DESCRIPTION;
		protected System.Web.UI.WebControls.TextBox txtOTHER_DESCRIPTION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvOTHER_DESCRIPTION;
		
		protected System.Web.UI.WebControls.Label capDESCRIPTION;
		protected System.Web.UI.WebControls.TextBox txtDESCRIPTION;
		protected System.Web.UI.WebControls.Label capAUTHORITY;
		protected System.Web.UI.WebControls.TextBox txtAUTHORITY;
		protected System.Web.UI.WebControls.Label capREPORT;
		protected System.Web.UI.WebControls.TextBox txtREPORT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOCCURRENCE_DETAIL_ID;
		protected System.Web.UI.WebControls.Label capVIOLATION;
		protected System.Web.UI.WebControls.Label capLOSS_TYPE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.WebControls.TextBox txtVIOLATION;
		protected System.Web.UI.WebControls.RegularExpressionValidator  revESTIMATE_AMOUNT;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidText;


        protected System.Web.UI.WebControls.Label capLOSS_LOCATION_ZIP;
        protected System.Web.UI.WebControls.Label capLOSS_LOCATION_CITY;
        protected System.Web.UI.WebControls.Label capLOSS_LOCATION_STATE;

        protected HtmlInputHidden hidZipeCodeVerificationMsg;
        protected System.Web.UI.WebControls.TextBox txtLOSS_LOCATION_ZIP;
        protected System.Web.UI.WebControls.TextBox txtLOSS_LOCATION_CITY;
        protected System.Web.UI.WebControls.DropDownList cmbLOSS_LOCATION_STATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOSS_LOCATION_ZIP;
        protected System.Web.UI.WebControls.RegularExpressionValidator revLOSS_LOCATION_ZIP;

        protected System.Web.UI.WebControls.Button btnSELECT;
		protected System.Web.UI.WebControls.Button btnDESELECT;
		protected System.Web.UI.WebControls.CustomValidator csvRECIPIENTS;
		protected System.Web.UI.WebControls.ListBox cmbFROMLOSS_TYPE;
		protected System.Web.UI.WebControls.Label capRECIPIENTS;
		protected System.Web.UI.WebControls.ListBox cmbLOSS_TYPE;
		protected System.Web.UI.WebControls.CustomValidator csvLOSS_TYPE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOSS_TYPE;
		//Added by Charles on 1-Dec-09 for Itrack 6647
		protected System.Web.UI.WebControls.Label capWaterBackup_SumpPump_Loss;
		protected System.Web.UI.WebControls.DropDownList cmbWaterBackup_SumpPump_Loss;
		protected System.Web.UI.HtmlControls.HtmlTableRow trWaterBackup_SumpPump_Loss;
		//Added till here
		//Added for Itrack 6647 on 9 dec 09
		protected System.Web.UI.WebControls.Label capWeather_Related_Loss;
		protected System.Web.UI.WebControls.DropDownList cmbWeather_Related_Loss;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvWeather_Related_Loss;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOSS_LOCATION_STATE;        
		protected System.Web.UI.HtmlControls.HtmlTableRow trWeather_Related_Loss;
        protected System.Web.UI.WebControls.Label capHeader;
        protected System.Web.UI.WebControls.Label capMessage;


        
		#endregion

		#region Local form variables
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		ClsOccurrenceDetails objOD = new ClsOccurrenceDetails();
		private string strRowId, strFormSaved;
		#endregion
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnOTHER_DESCRIPTION;
		
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
            Ajax.Utility.RegisterTypeForAjax(typeof(AddOccurrenceDetails));
			SetActivityStatus("");
			#region Setting ScreenId
			base.ScreenId="306_1";
			#endregion
            
			
			lblMessage.Visible = false;

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass		= CmsButtonType.Write;
			btnReset.PermissionString	= gstrSecurityXML;

			btnSave.CmsButtonClass		= CmsButtonType.Write;
			btnSave.PermissionString	= gstrSecurityXML;
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************

			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddOccurrenceDetails"  ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				btnReset.Attributes.Add("onclick","javascript:return ResetTheForm();");
				//btnSave.Attributes.Add("onclick","javascript:return setLossTypes();");   
				btnSELECT.Attributes.Add("onclick","javascript:selectLossTypes();return false;");
				btnDESELECT.Attributes.Add("onclick","javascript:deselectLossTypes();return false;");
				txtESTIMATE_AMOUNT.Attributes.Add("onBlur","javascript:this.value=formatCurrencyWithCents(this.value);");
				//Done for Itrack Issue 6640 on 10 Dec 09
				btnSave.Attributes.Add("onkeypress","javascript:if(event.keycode==13){Weather_Related_Loss();}");
				//btnSave.Attributes.Add("onClick","javascript:Weather_Related_Loss();");
				btnSave.Attributes.Add("onClick","javascript:return setLossTypes();Weather_Related_Loss();");
				GetQueryString();
				GetOldDataXML();
				SetCaptions();
				FillLossDateTime();
				FillCombo();
				LoadData();
				ShowHideControls();
				SetErrorMessages();
            
                
			}
		}//end pageload
		#endregion

		#region Set Error Messages for validators on the page
		private void SetErrorMessages()
		{
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            revLOSS_LOCATION_ZIP.ValidationExpression = aRegExpZipBrazil;
            revLOSS_LOCATION_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("24");

            //rfvLOSS_LOCATION_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");//commented by avijit goswami for singapore dev
            rfvLOSS_LOCATION_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("37");// addad by avijit goswami for singapore dev

			//revESTIMATE_AMOUNT.ValidationExpression	=	aRegExpCurrencyformat;//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
			revESTIMATE_AMOUNT.ValidationExpression	=	aRegExpDoublePositiveZero;
			revESTIMATE_AMOUNT.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
			rfvOTHER_DESCRIPTION.ErrorMessage		=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("550");
			rfvWeather_Related_Loss.ErrorMessage		=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1058");
            //if(hidLOB_ID.Value==((int)enumLOB.GENL).ToString() || hidLOB_ID.Value==((int)enumLOB.REDW).ToString() || hidLOB_ID.Value==((int)enumLOB.HOME).ToString())
            //    csvDESCRIPTION.ErrorMessage				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");			
            //else
            //    csvDESCRIPTION.ErrorMessage				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");			
			csvLOSS_LOCATION.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");			
			csvVIOLATION.ErrorMessage=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
            csvLOSS_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1305");
            capMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1305");
            hidText.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1305");
            rfvLOSS_LOCATION_STATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");
            capHeader.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
		}
		#endregion

		#region Hide controls for Gen Lib
		private void ShowHideControls()
		{
			if(hidLOB_ID.Value==((int)enumLOB.GENL).ToString() || hidLOB_ID.Value==((int)enumLOB.REDW).ToString() || hidLOB_ID.Value==((int)enumLOB.HOME).ToString())
			{
				capVIOLATION.Visible = false;
				txtVIOLATION.Visible = false;	
				//txtREPORT.Visible = false;
				//capREPORT.Visible = false;
				//Added by Charles on 1-Dec-09 for Itrack 6647
				if(hidLOB_ID.Value==((int)enumLOB.HOME).ToString())
				{
					trWaterBackup_SumpPump_Loss.Visible=true;
					trWeather_Related_Loss.Visible=true;//Added for Itrack 6640 on 9 Dec 09
				}
				else
				{
					trWaterBackup_SumpPump_Loss.Visible=false;
					trWeather_Related_Loss.Visible=false;//Added for Itrack 6640 on 9 Dec 09
				}
				//Added till here
			}
			else
			{
				trWaterBackup_SumpPump_Loss.Visible=false; //Added by Charles on 1-Dec-09 for Itrack 6647
				trWeather_Related_Loss.Visible=false;//Added for Itrack 6640 on 9 Dec 09
				capESTIMATE_AMOUNT.Visible = false;
				txtESTIMATE_AMOUNT.Visible = false;
				revESTIMATE_AMOUNT.Enabled = false;				
				txtOTHER_DESCRIPTION.Visible = false;
				capOTHER_DESCRIPTION.Visible = false;
				rfvOTHER_DESCRIPTION.Visible = false;
				spnOTHER_DESCRIPTION.Attributes.Add("style","display:none");
			}
		}
		#endregion
		
		#region Fill Date and Time Values
		private void FillLossDateTime()
		{
			DataSet dsTemp = objOD.GetDateTimeOfLoss(int.Parse(hidCLAIM_ID.Value));
			if (dsTemp.Tables[0].Rows.Count > 0)
			{	
				if (dsTemp.Tables[0].Rows[0]["LOSS_TIME"] != DBNull.Value)
				{
					int hour = 0;
					hour = Convert.ToInt32(Convert.ToDateTime(dsTemp.Tables[0].Rows[0]["LOSS_TIME"]).Hour.ToString());
					if (hour > 12)
						hour = hour - 12;
					lblLOSS_HOUR.Text =  hour.ToString();
					lblLOSS_MIN.Text = Convert.ToDateTime(dsTemp.Tables[0].Rows[0]["LOSS_TIME"]).Minute.ToString();
				}
			

				if (dsTemp.Tables[0].Rows[0]["LOSS_DATE"] != DBNull.Value)
                    lblDATE_OF_LOSS.Text = ConvertDBDateToCulture(dsTemp.Tables[0].Rows[0]["LOSS_DATE"].ToString());
				
				if (dsTemp.Tables[0].Rows[0]["LOSS_TIME_AM_PM"] != DBNull.Value)
				{
					if (dsTemp.Tables[0].Rows[0]["LOSS_TIME_AM_PM"].ToString() == "0")
						lblLOSS_AM_PM.Text = "AM";
					else
						lblLOSS_AM_PM.Text = "PM";
				
				}
			}
		}
		#endregion

		#region Fill Combo
		private void FillCombo()
		{
			/*Commented by Asfa (04-Apr-2008) - iTrack issue #3994
			 
			DataTable dtTemp = ClsDefaultValues.GetDefaultValuesDetails(5);   //For Loss Types
			cmbFROMLOSS_TYPE.DataSource = dtTemp;
			cmbFROMLOSS_TYPE.DataTextField = "DETAIL_TYPE_DESCRIPTION";
			cmbFROMLOSS_TYPE.DataValueField = "DETAIL_TYPE_ID";
			cmbFROMLOSS_TYPE.DataBind();	
			//cmbLOSS_TYPE.Items.Insert(0,"");
			*/
			
			//Added by Asfa (04-Apr-2008) - iTrack issue #3994
            DataTable dtLossCodes = ClsLossCodes.GetLossCodes(Convert.ToInt32(hidLOB_ID.Value),int.Parse(GetLanguageID()));
			if(dtLossCodes!=null && dtLossCodes.Rows.Count>0)
			{
				cmbFROMLOSS_TYPE.DataSource		=	dtLossCodes;
				cmbFROMLOSS_TYPE.DataTextField	=	"DESCRIPTION";
				cmbFROMLOSS_TYPE.DataValueField	=	"LOSS_CODE_TYPE";
				cmbFROMLOSS_TYPE.DataBind();
			}

			//Added by Charles on 1-Dec-09 for Itrack 6647
			cmbWaterBackup_SumpPump_Loss.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbWaterBackup_SumpPump_Loss.DataTextField="LookupDesc"; 
			cmbWaterBackup_SumpPump_Loss.DataValueField="LookupID"; 
			cmbWaterBackup_SumpPump_Loss.DataBind(); 
			cmbWaterBackup_SumpPump_Loss.Items.Insert(0,"");
			//Added till here

			//Added for Itrack 6640 on 9 Dec 09
			cmbWeather_Related_Loss.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbWeather_Related_Loss.DataTextField="LookupDesc"; 
			cmbWeather_Related_Loss.DataValueField="LookupID"; 
			cmbWeather_Related_Loss.DataBind(); 
			cmbWeather_Related_Loss.Items.Insert(0,"");
			//Added till here

            // modified by Santosh Kumar Gautam on 08 jul 2011(Ref Itrack:1021)
            Cms.BusinessLayer.BlCommon.ClsStates objStates = new Cms.BusinessLayer.BlCommon.ClsStates();
            //DataSet ds = objStates.GetStatesCountry(5); // for brazil
            DataSet ds = objStates.GetStatesCountry(7); //For Singapore
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                cmbLOSS_LOCATION_STATE.DataSource = ds;
                cmbLOSS_LOCATION_STATE.DataTextField = STATE_NAME;
                cmbLOSS_LOCATION_STATE.DataValueField = STATE_ID;
                cmbLOSS_LOCATION_STATE.DataBind();
                
            }
            cmbLOSS_LOCATION_STATE.Items.Insert(0, "");
          

		}
		#endregion

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsOccurrenceDetailsInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsOccurrenceDetailsInfo objODInfo = new ClsOccurrenceDetailsInfo();

			/*if (cmbLOSS_TYPE.SelectedItem!=null && cmbLOSS_TYPE.SelectedItem.Value!="")
			{
				if (Convert.ToInt32(cmbLOSS_TYPE.SelectedValue) > 0)				
					objODInfo.LOSS_TYPE = int.Parse(cmbLOSS_TYPE.SelectedValue);
			}
			else	
				objODInfo.LOSS_TYPE = 0;*/
			int val = 0;
			string loss = "";
			string losstype=(string)hidLOSS_TYPE.Value;
			if (losstype !="" && losstype != "0")
			{
				string[] losstypes= losstype.Split(',');  
				losstype="";
				for (int i=0;i <losstypes.GetLength(0)-1 ;i++)
				{
					val = Convert.ToInt32(losstypes[i]);
					if(val ==62)
						loss = "OTHER";
					else
						loss ="";
					losstype=losstype + losstypes[i].ToString()  + ","; 	
				}
			}
			if (losstype =="0" ) 
				losstype="";			
			objODInfo.LOSS_TYPE = losstype;

			
			if (txtLOSS_LOCATION.Text != "")
				objODInfo.LOSS_LOCATION = txtLOSS_LOCATION.Text;

			if (txtDESCRIPTION.Text != "")
				objODInfo.LOSS_DESCRIPTION = txtDESCRIPTION.Text;
			
			if (txtAUTHORITY.Text != "")
				objODInfo.AUTHORITY_CONTACTED = txtAUTHORITY.Text;
			
			if (txtREPORT.Text != "")
				objODInfo.REPORT_NUMBER = txtREPORT.Text;

			if (txtVIOLATION.Text != "")
				objODInfo.VIOLATIONS = txtVIOLATION.Text;
			
			if(txtESTIMATE_AMOUNT.Text.Trim()!="")
				objODInfo.ESTIMATE_AMOUNT = Convert.ToDouble(txtESTIMATE_AMOUNT.Text.Trim());
			if(loss=="OTHER" && txtOTHER_DESCRIPTION.Text.Trim()!="")
				objODInfo.OTHER_DESCRIPTION = txtOTHER_DESCRIPTION.Text.Trim();

			//Added by Charles on 1-Dec-09 for Itrack 6647
			if(cmbWaterBackup_SumpPump_Loss.SelectedItem!=null && cmbWaterBackup_SumpPump_Loss.SelectedItem.Value!="")
				objODInfo.WATERBACKUP_SUMPPUMP_LOSS = int.Parse(cmbWaterBackup_SumpPump_Loss.SelectedItem.Value);
			
			//Added for Itrack 6640 on 9 Dec 09
			if(cmbWeather_Related_Loss.SelectedItem!=null && cmbWeather_Related_Loss.SelectedItem.Value!="")
				objODInfo.WEATHER_RELATED_LOSS = int.Parse(cmbWeather_Related_Loss.SelectedItem.Value);

            // Added by Santosh Kumar Gautam on 25 Nov 2010
            if (!string.IsNullOrEmpty(txtLOSS_LOCATION_ZIP.Text))
                objODInfo.LOSS_LOCATION_ZIP = txtLOSS_LOCATION_ZIP.Text;

            if (!string.IsNullOrEmpty(txtLOSS_LOCATION_CITY.Text))
                objODInfo.LOSS_LOCATION_CITY = txtLOSS_LOCATION_CITY.Text;

           if (!string.IsNullOrEmpty(cmbLOSS_LOCATION_STATE.SelectedValue))
               objODInfo.LOSS_LOCATION_STATE = int.Parse( cmbLOSS_LOCATION_STATE.SelectedValue);

			objODInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidOCCURRENCE_DETAIL_ID.Value; 
			//Returning the model object
			return objODInfo;
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
				ClsOccurrenceDetailsInfo objODInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("0")) //save case
				{
					objODInfo.CREATED_BY = int.Parse(GetUserId());

					//Calling the add method of business layer class
					intRetVal = objOD.Add(objODInfo);

					if(intRetVal>0)
					{
						hidOCCURRENCE_DETAIL_ID.Value = objODInfo.OCCURRENCE_DETAIL_ID.ToString();
						lblMessage.Text	=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value=	"1";
						hidOldData.Value = objOD.GetXmlForPageControls(int.Parse(hidOCCURRENCE_DETAIL_ID.Value),int.Parse(hidCLAIM_ID.Value));
						LoadData();
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"165");
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsOccurrenceDetailsInfo objOldODInfo = new ClsOccurrenceDetailsInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldODInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objODInfo.OCCURRENCE_DETAIL_ID = int.Parse(strRowId);
					objODInfo.MODIFIED_BY = int.Parse(GetUserId());

					//Updating the record using business layer class object
					intRetVal	= objOD.Update(objOldODInfo,objODInfo);
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
				hidFormSaved.Value	=	"2";
			}
			
		}


		#endregion

		#region SetCaptions
		/// <summary>
		/// Show the caption of labels from resource file
		/// </summary>
		private void SetCaptions()
		{
            // Added by Santosh Kumar Gautam onn 25 Nov 2010
            hidZipeCodeVerificationMsg.Value = objResourceMgr.GetString("hidZipeCodeVerificationMsg"); 
            capLOSS_LOCATION_ZIP.Text = objResourceMgr.GetString("txtLOSS_LOCATION_ZIP");
            capLOSS_LOCATION_CITY.Text = objResourceMgr.GetString("txtLOSS_LOCATION_CITY");
            capLOSS_LOCATION_STATE.Text = objResourceMgr.GetString("cmbLOSS_LOCATION_STATE");

			capDATE_OF_LOSS.Text		=		objResourceMgr.GetString("lblDATE_OF_LOSS");
			capLOSS_TIME.Text			=		objResourceMgr.GetString("capLOSS_TIME");
			
				capLOSS_TYPE.Text		=		objResourceMgr.GetString("cmbLOSS_TYPE");
				//				capDESCRIPTION.Text		=		objResourceMgr.GetString("txtDESCRIPTION");
				//				capAUTHORITY.Text		=		objResourceMgr.GetString("txtAUTHORITY");
				capDESCRIPTION.Text		=		objResourceMgr.GetString("txtLOSS_DESCRIPTION");
				capAUTHORITY.Text		=		objResourceMgr.GetString("txtAUTHORITY_CONTACTED");
			
			
			capOTHER_DESCRIPTION.Text	=		objResourceMgr.GetString("txtOTHER_DESCRIPTION");
			capLOSS_LOCATION.Text		=		objResourceMgr.GetString("txtLOSS_LOCATION");						
			//capREPORT.Text				=		objResourceMgr.GetString("txtREPORT");
			//capVIOLATION.Text			=		objResourceMgr.GetString("txtVIOLATION");
			capREPORT.Text				=		objResourceMgr.GetString("txtREPORT_NUMBER");
			capVIOLATION.Text			=		objResourceMgr.GetString("txtVIOLATIONS");
			capESTIMATE_AMOUNT.Text		=		objResourceMgr.GetString("txtESTIMATE_AMOUNT");
			//Modified on 3 June 2010 for Itrack 6932
			capWaterBackup_SumpPump_Loss.Text	= objResourceMgr.GetString("cmbWATERBACKUP_SUMPPUMP_LOSS");//Added by Charles on 1-Dec-09 for Itrack 6647
			capWeather_Related_Loss.Text		= objResourceMgr.GetString("cmbWEATHER_RELATED_LOSS");//Added for Itrack 6640 on 9 Dec 09
			
            
		}
		#endregion

		#region GetQueryString and GetOldDataXML Functions
		/// <summary>
		/// Get query string from url into hidden controls
		/// </summary>
		private void GetQueryString()
		{
			if (Request["CLAIM_ID"] != null)
				hidCLAIM_ID.Value = Request.Params["CLAIM_ID"];
			if (Request.QueryString["LOB_ID"] != null && Request.QueryString["LOB_ID"].ToString()!="")
				hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();
		}

		/// <summary>
		/// retreive the information about selected record in the form of XML
		/// and saves it into hidden control
		/// </summary>
		private void GetOldDataXML()
		{
			if ( hidOCCURRENCE_DETAIL_ID.Value != "" && hidCLAIM_ID.Value != "") 
				hidOldData.Value = objOD.GetXmlForPageControls(int.Parse(hidOCCURRENCE_DETAIL_ID.Value),int.Parse(hidCLAIM_ID.Value));
		}
		#endregion

		#region LoadData
		private void LoadData()
		{
			if ( hidCLAIM_ID.Value != "") 
			{
				DataSet dsTemp = objOD.GetValuesOfOccuranceDetail(int.Parse(hidOCCURRENCE_DETAIL_ID.Value),int.Parse(hidCLAIM_ID.Value));	
				if (dsTemp.Tables[0].Rows.Count > 0)
				{
					foreach(DataRow dr in dsTemp.Tables[0].Rows) 
					{
						hidOCCURRENCE_DETAIL_ID.Value = dr["OCCURRENCE_DETAIL_ID"].ToString();
						
//						if (Convert.ToInt32(dr["LOSS_TYPE"].ToString()) > 0)
//							cmbLOSS_TYPE.SelectedValue = dr["LOSS_TYPE"].ToString();
//						else
//							cmbLOSS_TYPE.SelectedIndex = 0;
						
						if (dr["LOSS_TYPE"].ToString() != "")
							hidLOSS_TYPE.Value = dr["LOSS_TYPE"].ToString();
						
						txtLOSS_LOCATION.Text = dr["LOSS_LOCATION"].ToString();
						txtDESCRIPTION.Text = dr["LOSS_DESCRIPTION"].ToString();
						txtAUTHORITY.Text = dr["AUTHORITY_CONTACTED"].ToString();
						txtREPORT.Text = dr["REPORT_NUMBER"].ToString();
						txtVIOLATION.Text = dr["VIOLATIONS"].ToString();
						txtOTHER_DESCRIPTION.Text = dr["OTHER_DESCRIPTION"].ToString();
						if(dr["ESTIMATE_AMOUNT"]!=null && dr["ESTIMATE_AMOUNT"].ToString()!="0")
							txtESTIMATE_AMOUNT.Text= Double.Parse(dr["ESTIMATE_AMOUNT"].ToString()).ToString("N");
						//txtESTIMATE_AMOUNT.Text=String.Format("{0:,#,###}",Convert.ToInt64(dr["ESTIMATE_AMOUNT"]));
						//Added by Charles on 1-Dec-09 for Itrack 6647
						ListItem li=null;
						li = cmbWaterBackup_SumpPump_Loss.Items.FindByValue(dr["WATERBACKUP_SUMPPUMP_LOSS"].ToString());
						if(dr["WATERBACKUP_SUMPPUMP_LOSS"]!=null && dr["WATERBACKUP_SUMPPUMP_LOSS"].ToString()!="" && li!=null)
						  cmbWaterBackup_SumpPump_Loss.SelectedIndex = cmbWaterBackup_SumpPump_Loss.Items.IndexOf(li);
						//Added till here

						li = cmbWeather_Related_Loss.Items.FindByValue(dr["WEATHER_RELATED_LOSS"].ToString());
						if(dr["WEATHER_RELATED_LOSS"]!=null && dr["WEATHER_RELATED_LOSS"].ToString()!="" && li!=null)
							cmbWeather_Related_Loss.SelectedIndex = cmbWeather_Related_Loss.Items.IndexOf(li);
	
                        // Added by Santosh Kumar Gautam on 25 Nov 2010                       
                            txtLOSS_LOCATION_ZIP.Text = dr["LOSS_LOCATION_ZIP"].ToString();
                            txtLOSS_LOCATION_CITY.Text = dr["LOSS_LOCATION_CITY"].ToString();
                            if (dr["LOSS_LOCATION_STATE"] != null && dr["LOSS_LOCATION_STATE"].ToString() != "0" && !string.IsNullOrEmpty(dr["LOSS_LOCATION_STATE"].ToString()))
                                cmbLOSS_LOCATION_STATE.SelectedValue = dr["LOSS_LOCATION_STATE"].ToString();
                           

						
					}					
				}
			}
		}
		#endregion

       // FILLE DETAIL ON FILL ZIP CODE
        [System.Web.Services.WebMethod]
        public static String GetCustomerAddressDetailsUsingZipeCode(String ZIPCODE, String COUNTRYID)
        {

            CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();  //Declare the object ob common webservice

            String ReturnValue = String.Empty;
            DataSet ds = new DataSet();

            ReturnValue = obj.GetAddressDetailsBasedOnZipeCode(ZIPCODE, Convert.ToInt32(COUNTRYID));


            return ReturnValue;
        }
	}
}
