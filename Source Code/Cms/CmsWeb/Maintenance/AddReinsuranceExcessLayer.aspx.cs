/* ***************************************************************************************
   Author		: Deepak Batra 
   Creation Date: 05/01/2006
   Last Updated : 
   Reviewed By	: 
   Purpose		: This file is used for Excess Layer for a reinsurance contract. 
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
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Maintenance.Reinsurance;
using Cms.ExceptionPublisher.ExceptionManagement;
using System.Xml;
using Cms;
using Cms.CmsWeb.Controls;

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for AddReinsuranceExcessLayer.
	/// </summary>
	public class AddReinsuranceExcessLayer : Cms.CmsWeb.cmsbase
	{
		# region D E C L A R A T I O N
		
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capLAYER_AMOUNT;
		protected System.Web.UI.WebControls.Label capUNDERLYING_AMOUNT;
		protected System.Web.UI.WebControls.Label capLAYER_PREMIUM;
		protected System.Web.UI.WebControls.Label capCEDING_COMMISSION;
		protected System.Web.UI.WebControls.Label capAC_PREMIUM;

		protected System.Web.UI.WebControls.TextBox txtLAYER_AMOUNT;
		protected System.Web.UI.WebControls.TextBox txtUNDERLYING_AMOUNT;
		protected System.Web.UI.WebControls.TextBox txtLAYER_PREMIUM;
		protected System.Web.UI.WebControls.TextBox txtCEDING_COMMISSION;
		protected System.Web.UI.WebControls.TextBox txtAC_PREMIUM;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLAYER_AMOUNT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUNDERLYING_AMOUNT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLAYER_PREMIUM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCEDING_COMMISSION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAC_PREMIUM;

		protected System.Web.UI.WebControls.RegularExpressionValidator revLAYER_AMOUNT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revUNDERLYING_AMOUNT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLAYER_PREMIUM;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCEDING_COMMISSION;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAC_PREMIUM;

		protected System.Web.UI.WebControls.CustomValidator csvCEDING_COMMISSION;
		
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEXCESS_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCONTRACT_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLAYER_TYPE;

		ClsReinsuranceExcessLayer objReinsuranceExcessLayer;
		System.Resources.ResourceManager objResourceMgr;

		private string strRowId, strFormSaved;
		protected string oldXML;
        
		# endregion 

		# region P A G E   L O A D 

		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				objReinsuranceExcessLayer = new ClsReinsuranceExcessLayer();
				btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");

				base.ScreenId = "262_2_0";
				lblMessage.Visible = false;
				SetErrorMessages();

				//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
				btnReset.CmsButtonClass		=	CmsButtonType.Execute;
				btnReset.PermissionString	=	gstrSecurityXML;

				btnSave.CmsButtonClass		=	CmsButtonType.Execute;
				btnSave.PermissionString	=	gstrSecurityXML;

				objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddReinsuranceExcessLayer" ,System.Reflection.Assembly.GetExecutingAssembly());

				if(!Page.IsPostBack)
				{
					if(Request.QueryString["ContractID"]!=null && Request.QueryString["ContractID"].ToString().Length>0)
						hidCONTRACT_ID.Value = Request.QueryString["ContractID"].ToString();
					
					if(Request.QueryString["EXCESS_ID"]!=null && Request.QueryString["EXCESS_ID"].ToString().Length>0)
					{
						hidEXCESS_ID.Value = Request.QueryString["EXCESS_ID"].ToString();						
					}

					hidLAYER_TYPE.Value = "E"; //This is set to show that the screen is of 'EXCESS LAYER'
					
					SetCaptions();

					GetDataForEditMode();
				}
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);			
			}
		}

		# endregion 

		# region  S E T   P A G E   V A L I D A T I O N S   E R R O R   M E S S A G E S 

		private void SetErrorMessages()
		{
			try
			{
				rfvLAYER_AMOUNT.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
				rfvUNDERLYING_AMOUNT.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
				rfvLAYER_PREMIUM.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
				rfvCEDING_COMMISSION.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
				rfvAC_PREMIUM.ErrorMessage						= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
				csvCEDING_COMMISSION.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"10");
				
				this.revLAYER_AMOUNT.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage("G","216");
				this.revLAYER_AMOUNT.ValidationExpression		= aRegExpDoublePositiveNonZero;
				this.revUNDERLYING_AMOUNT.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage("G","216");
				this.revUNDERLYING_AMOUNT.ValidationExpression	= aRegExpDoublePositiveNonZero;
				this.revLAYER_PREMIUM.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage("G","216");
				this.revLAYER_PREMIUM.ValidationExpression		= aRegExpDoublePositiveNonZero;
				this.revCEDING_COMMISSION.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage("G","216");
				this.revCEDING_COMMISSION.ValidationExpression	= aRegExpDoublePositiveNonZero;
				this.revAC_PREMIUM.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage("G","216");
				this.revAC_PREMIUM.ValidationExpression			= aRegExpDoublePositiveNonZero;
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
		}

		# endregion

		# region  S E T   L A B E L   C A P T I O N S 

		private void SetCaptions()
		{
			try
			{
				capLAYER_AMOUNT.Text			= objResourceMgr.GetString("txtLAYER_AMOUNT");
				capUNDERLYING_AMOUNT.Text		= objResourceMgr.GetString("txtUNDERLYING_AMOUNT");
				capLAYER_PREMIUM.Text			= objResourceMgr.GetString("txtLAYER_PREMIUM");
				capCEDING_COMMISSION.Text		= objResourceMgr.GetString("txtCEDING_COMMISSION");
				capAC_PREMIUM.Text				= objResourceMgr.GetString("txtAC_PREMIUM");
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
		}

		# endregion

		#region G E T   F O R M   V A L U E S 
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsReinsuranceExcessLayerInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsReinsuranceExcessLayerInfo objReinsuranceExcessLayerInfo = new ClsReinsuranceExcessLayerInfo();

			if(hidCONTRACT_ID.Value != "")
				objReinsuranceExcessLayerInfo.CONTRACT_ID			= Convert.ToInt32(hidCONTRACT_ID.Value);
			if(txtLAYER_AMOUNT.Text != "")
				objReinsuranceExcessLayerInfo.LAYER_AMOUNT			= Convert.ToDouble(txtLAYER_AMOUNT.Text);
			if(txtUNDERLYING_AMOUNT.Text != "")
				objReinsuranceExcessLayerInfo.UNDERLYING_AMOUNT		= Convert.ToDouble(txtUNDERLYING_AMOUNT.Text);
			if(txtLAYER_PREMIUM.Text != "")
				objReinsuranceExcessLayerInfo.LAYER_PREMIUM			= Convert.ToDouble(txtLAYER_PREMIUM.Text);
			if(txtCEDING_COMMISSION.Text != "")
				objReinsuranceExcessLayerInfo.CEDING_COMMISSION		= Convert.ToDouble(txtCEDING_COMMISSION.Text);
			if(txtAC_PREMIUM.Text != "")
				objReinsuranceExcessLayerInfo.AC_PREMIUM			= Convert.ToDouble(txtAC_PREMIUM.Text);			

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidEXCESS_ID.Value;
			//oldXML			=	hidOldData.Value;
			//Returning the model object
			
			return objReinsuranceExcessLayerInfo;
		}
		#endregion

		#region W E B  F O R M   D E S I G N E R   G E N E R A T E D   C O D E
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
			this.Load			+= new System.EventHandler(this.Page_Load);
			this.btnSave.Click	+= new System.EventHandler(this.btnSave_Click);
		}
		#endregion

		# region S A V E   F O R M   D A T A 

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				objReinsuranceExcessLayer = new ClsReinsuranceExcessLayer();

				ClsReinsuranceExcessLayerInfo objReinsuranceExcessLayerInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //Add Mode
				{
					objReinsuranceExcessLayerInfo.CREATED_BY			= int.Parse(GetUserId());
					objReinsuranceExcessLayerInfo.CREATED_DATETIME		= DateTime.Now;	
					objReinsuranceExcessLayerInfo.IS_ACTIVE				= "Y";
					objReinsuranceExcessLayerInfo.LAYER_TYPE			= "E"; //Excess Layer

					intRetVal = objReinsuranceExcessLayer.AddExcess(objReinsuranceExcessLayerInfo);

					if(intRetVal>0)
					{
						hidEXCESS_ID.Value		=	objReinsuranceExcessLayerInfo.EXCESS_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"7");
						hidFormSaved.Value		=	"1";
						hidLAYER_TYPE.Value		=	"E";
						hidOldData.Value		= objReinsuranceExcessLayer.GetDataForPageControls(hidEXCESS_ID.Value, hidCONTRACT_ID.Value, hidLAYER_TYPE.Value).GetXml();
						hidIS_ACTIVE.Value		=	"Y";						
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"6");
						hidFormSaved.Value		=	"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"8");
						hidFormSaved.Value		=	"2";
					}
					lblMessage.Visible = true;
				}
				else // Edit Mode
				{
					//Creating the Model object for holding the Old data
					ClsReinsuranceExcessLayerInfo objOldReinsuranceExcessLayerInfo = new ClsReinsuranceExcessLayerInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldReinsuranceExcessLayerInfo, hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objReinsuranceExcessLayerInfo.EXCESS_ID				= int.Parse(strRowId);
					objReinsuranceExcessLayerInfo.MODIFIED_BY			= int.Parse(GetUserId());
					objReinsuranceExcessLayerInfo.LAST_UPDATED_DATETIME	= DateTime.Now;
					objReinsuranceExcessLayerInfo.LAYER_TYPE			= "E";

					intRetVal	= objReinsuranceExcessLayer.UpdateExcess(objOldReinsuranceExcessLayerInfo,objReinsuranceExcessLayerInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"9");
						hidFormSaved.Value		=	"1";
						hidOldData.Value		=	objReinsuranceExcessLayer.GetDataForPageControls(strRowId, hidCONTRACT_ID.Value, hidLAYER_TYPE.Value).GetXml();
					}
					else if(intRetVal == -1)	
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"8");
						hidFormSaved.Value		=	"1";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"8");
						hidFormSaved.Value		=	"1";
					}
					lblMessage.Visible = true;
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"6") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objReinsuranceExcessLayer!= null)
					objReinsuranceExcessLayer.Dispose();
			}
		}

		# endregion 

		# region  G E T   D A T A   F O R   E D I T   M O D E 

		private void GetDataForEditMode()
		{
			try
			{
				objReinsuranceExcessLayer = new ClsReinsuranceExcessLayer();
				DataSet oDs = objReinsuranceExcessLayer.GetDataForPageControls(hidEXCESS_ID.Value, hidCONTRACT_ID.Value, hidLAYER_TYPE.Value);
				if(oDs.Tables[0].Rows.Count >0)
				{
					DataRow oDr  = oDs.Tables[0].Rows[0];
					
					if(oDr["LAYER_AMOUNT"].ToString() != "")
						txtLAYER_AMOUNT.Text			= oDr["LAYER_AMOUNT"].ToString();

					if(oDr["UNDERLYING_AMOUNT"].ToString() != "")
						txtUNDERLYING_AMOUNT.Text		= oDr["UNDERLYING_AMOUNT"].ToString();
					
					if(oDr["LAYER_PREMIUM"].ToString() != "")
						txtLAYER_PREMIUM.Text 			= oDr["LAYER_PREMIUM"].ToString();
					
					if(oDr["CEDING_COMMISSION"].ToString() != "")
						txtCEDING_COMMISSION.Text 		= oDr["CEDING_COMMISSION"].ToString();
					
					if(oDr["AC_PREMIUM"].ToString() != "")
						txtAC_PREMIUM.Text 				= oDr["AC_PREMIUM"].ToString();
				}
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
			finally{}
		}

		# endregion 
	}
}
