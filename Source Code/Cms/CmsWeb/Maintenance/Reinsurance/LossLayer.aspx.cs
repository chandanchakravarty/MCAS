/* ***************************************************************************************
   Author		: Harmanjeet Singh
   Creation Date: May 08, 2007
   Last Updated : 
   Reviewed By	: 
   Purpose		: This file is used for Minor Participation for a reinsurance contract.
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

using Cms.BusinessLayer.BlCommon.Reinsurance;
using Cms.Model.Maintenance.Reinsurance;
using Cms.ExceptionPublisher.ExceptionManagement;
using System.Xml;
using Cms;
using Cms.CmsWeb.Controls;
 

namespace Cms.CmsWeb.Maintenance.Reinsurance
{
	/// <summary>
	/// Summary description for LossLayer.
	/// </summary>
	public class LossLayer : Cms.CmsWeb.cmsbase
	{
		# region D E C L A R A T I O N

		System.Resources.ResourceManager objResourceMgr;



        private string strRowId;//, strFormSaved
		//string oldXML;

		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivate;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCONTRACT_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidReset;
		protected System.Web.UI.WebControls.Label capLAYER;
		protected System.Web.UI.WebControls.TextBox txtLAYER;
		protected System.Web.UI.WebControls.Label capCOMPANY_RETENTION;
		protected System.Web.UI.WebControls.DropDownList cmbCOMPANY_RETENTION;
		protected System.Web.UI.WebControls.Label capLAYER_AMOUNT;
		protected System.Web.UI.WebControls.TextBox txtLAYER_AMOUNT;
		protected System.Web.UI.WebControls.Label capRETENTION_AMOUNT;
		protected System.Web.UI.WebControls.TextBox txtRETENTION_AMOUNT;
		protected System.Web.UI.WebControls.Label capRETENTION_PERCENTAGE;
		protected System.Web.UI.WebControls.TextBox txtRETENTION_PERCENTAGE;
		protected System.Web.UI.WebControls.Label capREIN_CEDED;
		protected System.Web.UI.WebControls.TextBox txtREIN_CEDED;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOSS_LAYER_ID;
        protected System.Web.UI.WebControls.Label capMessages;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLAYER;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLAYER;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOSS_COMPANY_RETENTION;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLAYER_AMOUNT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLAYER_AMOUNT;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOSS_COMPANY_RETENTION_TXT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLOSS_COMPANY_RETENTION_TXT;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRETENTION_PERCENTAGE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revRETENTION_PERCENTAGE;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_CEDED;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_CEDED;
		//protected System.Web.UI.WebControls.RangeValidator rngRETENTION_PERCENTAGE;
        //protected System.Web.UI.WebControls.RangeValidator rngREIN_CEDED_PERCENTAGE;

		protected System.Web.UI.WebControls.Label capREIN_CEDED_PERCENTAGE;
		protected System.Web.UI.WebControls.TextBox txtREIN_CEDED_PERCENTAGE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_CEDED_PERCENTAGE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_CEDED_PERCENTAGE;

        protected System.Web.UI.WebControls.CustomValidator csvREIN_CEDED_PERCENTAGE;
        protected System.Web.UI.WebControls.CustomValidator csvRETENTION_PERCENTAGE;
        protected System.Web.UI.WebControls.CustomValidator csvRETENTION_AMOUNT;
        
        
		# endregion 

		# region P A G E   L O A D 
      
		private void Page_Load(object sender, System.EventArgs e)
		{
			//base.ScreenId = "262_2";
			base.ScreenId = "262_9_0";
			btnReset.CmsButtonClass		=	CmsButtonType.Execute;
			btnReset.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass		=	CmsButtonType.Execute;
			btnSave.PermissionString	=	gstrSecurityXML;
				
			btnActivate.CmsButtonClass	=	CmsButtonType.Write;
			btnActivate.PermissionString		=	gstrSecurityXML;

			this.btnDelete.CmsButtonClass =     CmsButtonType.Delete;
			this.btnDelete.PermissionString =	gstrSecurityXML;

			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Reinsurance.LossLayer" ,System.Reflection.Assembly.GetExecutingAssembly());
			
			#region Set Attributes
			btnReset.Attributes.Add("onclick","javascript:Reset();return false;");
//			txtLAYER.Attributes.Add("onBlur","FormatAmount(document.getElementById('txtLAYER'));");
//			txtLAYER.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
            //txtLAYER_AMOUNT.Attributes.Add("onBlur", "formatBaseCurrencyAmount(document.getElementById('txtLAYER_AMOUNT'));");
            //txtRETENTION_AMOUNT.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
            //txtREIN_CEDED.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
            //txtLAYER_AMOUNT.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
			#endregion
			
			if(!Page.IsPostBack)
			{
                txtRETENTION_PERCENTAGE.Attributes.Add("onBlur", "this.value=formatRateBase(this.value,4);");
                txtREIN_CEDED_PERCENTAGE.Attributes.Add("onBlur", "this.value=formatRateBase(this.value,4);");
                txtLAYER_AMOUNT.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");

                txtRETENTION_AMOUNT.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
                txtREIN_CEDED.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
				GetQueryStringValues();
				GetOldDataXML();
				SetCaptions();
				FillDropdowns();
				SetErrorMessages();
                capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
                
			}
		}

		# endregion 

		# region SET CAPTIONS
		private void SetCaptions()
		{
			this.capLAYER.Text							=		objResourceMgr.GetString("txtLAYER");
			this.capCOMPANY_RETENTION.Text				=		objResourceMgr.GetString("cmbCOMPANY_RETENTION");
			this.capLAYER_AMOUNT.Text					=		objResourceMgr.GetString("txtLAYER_AMOUNT");
			this.capRETENTION_AMOUNT.Text				=		objResourceMgr.GetString("txtRETENTION_AMOUNT");
			this.capRETENTION_PERCENTAGE.Text			=		objResourceMgr.GetString("txtRETENTION_PERCENTAGE");
			this.capREIN_CEDED.Text						=		objResourceMgr.GetString("txtREIN_CEDED");
			this.capREIN_CEDED_PERCENTAGE.Text			=		objResourceMgr.GetString("txtREIN_CEDED_PERCENTAGE");
		}

		# endregion SET CAPTIONS

		# region Fill DropDowns
		private void FillDropdowns()
		{
			cmbCOMPANY_RETENTION.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbCOMPANY_RETENTION.DataTextField="LookupDesc"; 
			cmbCOMPANY_RETENTION.DataValueField="LookupCode";
			cmbCOMPANY_RETENTION.DataBind();
			cmbCOMPANY_RETENTION.Items.Insert(0,"");
		}

		#endregion


		# region Button Actions

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				ClsLossLayer objLossLayer = new ClsLossLayer();

				//Retreiving the form values into model class object
				ClsLossLayerInfo objLossLayerInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{

					//Filling the CREATED_BY to current login user and CREATED_DATE to current date
					objLossLayerInfo.CREATED_BY = int.Parse(GetUserId());
					objLossLayerInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objLossLayer.Add(objLossLayerInfo);

					if(intRetVal>0)				//Saved successfully
					{
						hidLOSS_LAYER_ID.Value = objLossLayerInfo.LOSS_LAYER_ID.ToString();
						lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value	=	"1";
						hidIS_ACTIVE.Value	=	"Y";
                        btnActivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1315"); 
						GetOldDataXML();
					}
					else if(intRetVal == -1)	//Duplicate journal entry number error occured
					{
						//Showing the error message from customized message file
						lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value	=	"2";
					}
					else						//Some other error occured
					{
						//Showing the error message from customized message file
						lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value	=	"2";
					}
					lblMessage.Visible = true;
				}	// end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsLossLayerInfo objOldLossLayerInfo;
					objOldLossLayerInfo = new ClsLossLayerInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldLossLayerInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objLossLayerInfo.MODIFIED_BY = int.Parse(GetUserId());
					objLossLayerInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					objLossLayerInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

					//Updating the record using business layer class object
					intRetVal	= objLossLayer.Update(objOldLossLayerInfo,objLossLayerInfo);
					if( intRetVal > 0 )				//update successfully performed
					{
						
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML();
					}
					
					lblMessage.Visible = true;
				}
			}
			catch(Exception ex)
			{
				//Some exception occured in code, hence showing the exception error message
				lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				hidFormSaved.Value	=	"2";
				
				//Publishing the exception
				ExceptionManager.Publish(ex);
			}
			finally
			{
				//			if(objCoverageCategoriesInfo!= null)
				//				objCoverageCategoriesInfo.Dispose();
			}
		}
		private void btnReset_Click(object sender, System.EventArgs e)
		{

		}
		private void btnActivate_Click(object sender, System.EventArgs e)
		{
			ClsLossLayer objLossLayer = new ClsLossLayer();
			//Retreiving the form values into model class object
			ClsLossLayerInfo objLossLayerInfo = GetFormValue();

			objLossLayerInfo.CREATED_BY = int.Parse(GetUserId());
			objLossLayerInfo.CREATED_DATETIME = DateTime.Now;

			try
			{
				//Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo();
				
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objLossLayer.ActivateDeactivateLossLayer(objLossLayerInfo,hidLOSS_LAYER_ID.Value,"N");
					lblMessage.Text = ClsMessages.FetchGeneralMessage("41");
                    btnActivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1314");
					hidIS_ACTIVE.Value="N";
				}
				else
				{					
					objLossLayer.ActivateDeactivateLossLayer(objLossLayerInfo,hidLOSS_LAYER_ID.Value,"Y");
					lblMessage.Text = ClsMessages.FetchGeneralMessage("40");
                    btnActivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1315");
					hidIS_ACTIVE.Value="Y";
				}
				hidOldData.Value = ClsLossLayer.GetLossLayerInfo(int.Parse(hidCONTRACT_ID.Value),int.Parse(hidLOSS_LAYER_ID.Value));
				hidFormSaved.Value			=	"1";
				
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				ExceptionManager.Publish(ex);
			}
			finally
			{
				lblMessage.Visible = true;
				if(objLossLayer!= null)
					objLossLayer.Dispose();
			}

		}
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				/*Deleting the whole record*/
				ClsLossLayer objLossLayer = new ClsLossLayer();
				//Retreiving the form values into model class object
				ClsLossLayerInfo objLossLayerInfo = GetFormValue();

				objLossLayerInfo.CREATED_BY = int.Parse(GetUserId());
				objLossLayerInfo.CREATED_DATETIME = DateTime.Now;

				int intRetVal = objLossLayer.Delete(objLossLayerInfo,int.Parse(hidLOSS_LAYER_ID.Value),int.Parse(GetUserId()));

				if (intRetVal > 0)
				{
					//Deleted successfully
					lblDelete.Visible = true;
					lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "127");
					hidLOSS_LAYER_ID.Value = "";
					hidFormSaved.Value = "5";
					hidOldData.Value = "";
					trBody.Attributes.Add("style","display:none");
				}
				else if(intRetVal == -2)
				{
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"6");
					hidFormSaved.Value		=	"2";
				}
				else
				{
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "128");
					hidFormSaved.Value = "2";
				}
				lblMessage.Visible = true;
			}
			catch (Exception objEx)
			{
				lblMessage.Text = objEx.Message.ToString();
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
			}
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
			this.Load += new System.EventHandler(this.Page_Load);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

		}
		#endregion

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsLossLayerInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsLossLayerInfo objLossLayer;
			objLossLayer = new ClsLossLayerInfo();

			//Populating the fields values of model class object from controls
					
			if (txtLAYER.Text.Trim() != "")
				objLossLayer.LAYER = int.Parse(txtLAYER.Text.Trim());
			if (cmbCOMPANY_RETENTION.SelectedValue != null && cmbCOMPANY_RETENTION.SelectedValue.Trim() != "")
				objLossLayer.COMPANY_RETENTION = int.Parse(cmbCOMPANY_RETENTION.SelectedValue);
			if (txtLAYER_AMOUNT.Text.Trim() != "")
                objLossLayer.LAYER_AMOUNT = double.Parse(txtLAYER_AMOUNT.Text, NfiBaseCurrency);
			if (txtRETENTION_AMOUNT.Text.Trim() != "")
                objLossLayer.RETENTION_AMOUNT = double.Parse(txtRETENTION_AMOUNT.Text, NfiBaseCurrency);
			if (txtRETENTION_PERCENTAGE.Text.Trim() != "")
                objLossLayer.RETENTION_PERCENTAGE = Convert.ToDouble(txtRETENTION_PERCENTAGE.Text, NfiBaseCurrency);  
													
			if (txtREIN_CEDED.Text.Trim() != "")
                objLossLayer.REIN_CEDED = double.Parse(txtREIN_CEDED.Text, NfiBaseCurrency);
            
			if (txtREIN_CEDED_PERCENTAGE.Text.Trim() != "")
                objLossLayer.REIN_CEDED_PERCENTAGE = Convert.ToDouble(txtREIN_CEDED_PERCENTAGE.Text, NfiBaseCurrency); 
			//These  assignments are common to all pages.
			strRowId		=	hidLOSS_LAYER_ID.Value;
			if(!strRowId.ToUpper().Equals("NEW"))
			{
				objLossLayer.LOSS_LAYER_ID= int.Parse(strRowId);
			}
				objLossLayer.CONTRACT_ID = int.Parse(hidCONTRACT_ID.Value);
			
			//Returning the model object
			return objLossLayer;
		}
		#endregion

		#region GetOldDataXML
		private void GetOldDataXML()
		{
			if ( hidLOSS_LAYER_ID.Value != "" ) 
			{
				//Retreiving the information of selected journal entry in the form of XML 				
				hidOldData.Value = ClsLossLayer.GetLossLayerInfo(int.Parse(hidCONTRACT_ID.Value),int.Parse(hidLOSS_LAYER_ID.Value));
              
				if (hidOldData.Value != "")
				{
                    btnActivate.Visible = true;
					System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
					doc.LoadXml(hidOldData.Value);
				
					System.Xml.XmlNode Layer = doc.SelectSingleNode("/NewDataSet/Table/LAYER");
					System.Xml.XmlNode compRetention = doc.SelectSingleNode("/NewDataSet/Table/COMPANY_RETENTION");
					System.Xml.XmlNode layerAmount = doc.SelectSingleNode("/NewDataSet/Table/LAYER_AMOUNT");
					System.Xml.XmlNode retAmount = doc.SelectSingleNode("/NewDataSet/Table/RETENTION_AMOUNT");
					System.Xml.XmlNode retPercentage = doc.SelectSingleNode("/NewDataSet/Table/RETENTION_PERCENTAGE");
					System.Xml.XmlNode reinCoded = doc.SelectSingleNode("/NewDataSet/Table/REIN_CEDED");
					System.Xml.XmlNode reinCodedPer = doc.SelectSingleNode("/NewDataSet/Table/REIN_CEDED_PERCENTAGE");
                    
                    if (retPercentage != null)
                    {
                        if (retPercentage.InnerXml.Trim() != "")
                        {
                            //txtRETENTION_PERCENTAGE.Text = Convert.ToDouble(retPercentage.InnerXml.Trim()).ToString("N", NfiBaseCurrency);  //retPercentage.InnerXml.Trim().ToUpper();
                            txtRETENTION_PERCENTAGE.Text = retPercentage.InnerXml.Trim();  //retPercentage.InnerXml.Trim().ToUpper();
                        }
                    }
                    if (reinCodedPer != null)
                    {
                        if (reinCodedPer.InnerXml.Trim() != "")
                        {
                            //txtREIN_CEDED_PERCENTAGE.Text = Convert.ToDouble(reinCodedPer.InnerXml.Trim()).ToString("N", NfiBaseCurrency);  //reinCodedPer.InnerXml.Trim();
                            txtREIN_CEDED_PERCENTAGE.Text = reinCodedPer.InnerXml.Trim();  //reinCodedPer.InnerXml.Trim();
                        }
                    }
                    NfiBaseCurrency.NumberDecimalDigits = 2;
					if (Layer != null)
					{
						if (Layer.InnerXml.Trim() != "")
						{
							txtLAYER.Text = Layer.InnerXml.Trim().ToUpper();
						}
					}
					if (compRetention != null)
					{
						if (compRetention.InnerXml.Trim() != "")
						{
							cmbCOMPANY_RETENTION.SelectedValue = compRetention.InnerXml.Trim().ToUpper();
						}
					}
					if (layerAmount != null)
					{
						if (layerAmount.InnerXml.Trim() != "")
						{
                            txtLAYER_AMOUNT.Text = Convert.ToDouble(layerAmount.InnerXml.Trim()).ToString("N", NfiBaseCurrency);//layerAmount.InnerXml.Trim();
						}
					}

					if (retAmount != null)
					{
						if (retAmount.InnerXml.Trim() != "")
						{
                            //txtRETENTION_AMOUNT.Text = Convert.ToDouble(retAmount.InnerXml.Trim()).ToString("N", NfiBaseCurrency);//retAmount.InnerXml.Trim().ToUpper();
                            txtRETENTION_AMOUNT.Text = retAmount.InnerXml.Trim().ToUpper();

						}
					}
					
					if (reinCoded != null)
					{
						if (reinCoded.InnerXml.Trim() != "")
						{
                            //txtREIN_CEDED.Text = Convert.ToDouble(reinCoded.InnerXml.Trim()).ToString("N", NfiBaseCurrency); //reinCoded.InnerXml.Trim();
                            txtREIN_CEDED.Text =reinCoded.InnerXml.Trim();
						}
					}
					
					System.Xml.XmlNode IsActive = doc.SelectSingleNode("/NewDataSet/Table/IS_ACTIVE");
					if (IsActive != null)
					{
						if (IsActive.InnerXml.Trim() != "")
						{
							hidIS_ACTIVE.Value= IsActive.InnerXml.Trim().ToUpper();
                            if (IsActive.InnerXml.Trim() == "Y")
                                btnActivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1315"); //"Deactivate";
                            else if (IsActive.InnerXml.Trim() == "N")
                                btnActivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1314"); //"Activate"; //sneha
						}
					}
					doc = null;
				}

			}
            else
                btnActivate.Visible = false;
		}
			
		#endregion

		#region GetQueryStringValues
		private void GetQueryStringValues()
		{
			if(Request.QueryString["ContractID"]!=null && Request.QueryString["ContractID"].ToString()!="")
				hidCONTRACT_ID.Value = Request.QueryString["ContractID"].ToString();
			if(Request.QueryString["LOSS_LAYER_ID"]!=null && Request.QueryString["LOSS_LAYER_ID"].ToString()!="")
				hidLOSS_LAYER_ID.Value = Request.QueryString["LOSS_LAYER_ID"].ToString();

		}
		#endregion

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvLAYER.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvLOSS_COMPANY_RETENTION.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvLAYER_AMOUNT.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvLOSS_COMPANY_RETENTION_TXT.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			rfvRETENTION_PERCENTAGE.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			rfvREIN_CEDED.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			//rfvCATEGORY.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("194");
            revLAYER.ValidationExpression =aRegExpCurrencyformat;
			//revLAYER_AMOUNT.ValidationExpression =aRegExpCurrencyformat;
            revLAYER_AMOUNT.ValidationExpression = aRegExpBaseCurrencyformat;
            revLOSS_COMPANY_RETENTION_TXT.ValidationExpression = aRegExpBaseCurrencyformat;
            revRETENTION_PERCENTAGE.ValidationExpression = aRegExpBaseDouble;
            revREIN_CEDED.ValidationExpression =  aRegExpBaseCurrencyformat;

            //revLAYER.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			revLAYER_AMOUNT.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			revLOSS_COMPANY_RETENTION_TXT.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			revRETENTION_PERCENTAGE.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			revREIN_CEDED.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			revREIN_CEDED_PERCENTAGE.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			//rngRETENTION_PERCENTAGE.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "8");
			//rngREIN_CEDED_PERCENTAGE.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "10");
            revREIN_CEDED_PERCENTAGE.ValidationExpression = aRegExpBaseDouble;
			revREIN_CEDED.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			rfvREIN_CEDED_PERCENTAGE.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9");

            csvREIN_CEDED_PERCENTAGE.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"10");
            csvRETENTION_PERCENTAGE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "8");
            csvRETENTION_AMOUNT.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "11");
            
       }

		
		#endregion
		
	}
}





