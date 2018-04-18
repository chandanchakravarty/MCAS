/******************************************************************************************
<Author					: -  Shafee
<Start Date				: -	 Jan 06, 2006
<End Date				: -	
<Description			: -  Add/Edit page for Reinsurance Posting
<Review Date			: - 
<Reviewed By			: - 	
Modification History

*/
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
using Cms.CmsWeb;
using System.Resources;
using Cms.CustomException;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using Cms.Model.Maintenance.Reinsurance;
using Cms.Model.Maintenance;


namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for AddReinsurancePosting.
	/// </summary>
	public class AddReinsurancePosting :Cms.CmsWeb.cmsbase 
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capContract_Name;
		protected System.Web.UI.WebControls.Label capContract_Namet;
		protected System.Web.UI.WebControls.Label capCommision_Applicable;
		protected System.Web.UI.WebControls.DropDownList cmbCommision_Applicable;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCommision_Applicable;
		protected System.Web.UI.WebControls.Label capRein_Premium_Act;
		protected System.Web.UI.WebControls.DropDownList cmbRein_Premium_Act;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRein_Premium_Act;
		protected System.Web.UI.WebControls.Label capRein_Payment_Act;
		protected System.Web.UI.WebControls.DropDownList cmbRein_Payment_Act;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvReinsurance_Pay_Act;
		protected System.Web.UI.WebControls.Label capRein_Commision_Act;
		protected System.Web.UI.WebControls.DropDownList cmbRein_Commision_Act;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRein_Commision_Act;
		protected System.Web.UI.WebControls.Label capRein_Commision_Recevable;
		protected System.Web.UI.WebControls.DropDownList cmbRein_Commision_Recevable;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRein_Commision_Recevable;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidGL_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidContact_id;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREIN_COMPANY_ID;
		protected System.Web.UI.WebControls.Panel pnlCommision_Applicable;
		System.Resources.ResourceManager objResourceMgr;
		ClsReinsurancePostingInfo objModelReinsure;
        ClsReinsurancePosting objDataReinsure; 				
	    #region Page_Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			#region Form variables
			base.ScreenId="221";

					
			#endregion
			cmbCommision_Applicable.Attributes.Add("onChange","javascript:return Commision();");
			objDataReinsure=new ClsReinsurancePosting();
			objModelReinsure=new ClsReinsurancePostingInfo();
			SetSecutityXml();
			if(!Page.IsPostBack)
			{
            
			
				SetCaption();
			
				SetErrorMessages();
				PoplateComboBox();

				// 
				if(Request.QueryString["CONTRACT_ID"]!="" || Request.QueryString["CONTRACT_ID"]!=null)
				{
					hidContact_id.Value =Request.QueryString["CONTRACT_ID"].ToString();
					capContract_Namet.Text=hidContact_id.Value ;
					//Get The Contract  Name From Id
					capContract_Namet.Text      =        objDataReinsure.GetContactNameFromId(int.Parse(hidContact_id.Value));
					objDataReinsure.GetComboBox();
					GetoldXml();
				}
//				if(Request.QueryString["REIN_COMAPANY_ID"]!="" || Request.QueryString["REIN_COMAPANY_ID"]!=null)
//				{
//					hidREIN_COMPANY_ID.Value = Request.QueryString["REIN_COMAPANY_ID"].ToString();
//				}
//				else
//				{
//					hidOldData.Value ="";
//				}
			}
		}
		
		#endregion

	    #region GetoldXml

		private void GetoldXml()
		{
            DataSet objds;
			DataTable dt;
			//Get The Contract Id Record
			objds=objDataReinsure.GetContractInformation(int.Parse(hidContact_id.Value));
			//Check For Record Existance
			if(objds.Tables[0].Rows.Count>0)
			{
				//Edit Mode
			    dt=objds.Tables[0];
			    hidOldData.Value      =     ClsCommon.GetXML(dt);

				//Fill Combo Box
				ListItem listItem;
				//Get The ListItem According To The Value
				listItem = cmbCommision_Applicable.Items.FindByValue(Convert.ToString(dt.Rows[0]["COMMISION_APPLICABLE"]));
				//Select it
				cmbCommision_Applicable.SelectedIndex= cmbCommision_Applicable.Items.IndexOf(listItem);	
								
				listItem = cmbRein_Premium_Act.Items.FindByValue(Convert.ToString(dt.Rows[0]["REIN_PREMIUM_ACT"]));
				cmbRein_Premium_Act.SelectedIndex= cmbRein_Premium_Act.Items.IndexOf(listItem);	
				
				listItem = cmbRein_Payment_Act.Items.FindByValue(Convert.ToString(dt.Rows[0]["REIN_PAYMENT_ACT"]));
				cmbRein_Payment_Act.SelectedIndex= cmbRein_Payment_Act.Items.IndexOf(listItem);	
				
				listItem = cmbRein_Commision_Act.Items.FindByValue(Convert.ToString(dt.Rows[0]["REIN_COMMISION_ACT"]));
				cmbRein_Commision_Act.SelectedIndex= cmbRein_Commision_Act.Items.IndexOf(listItem);	
				
				listItem = cmbRein_Commision_Recevable.Items.FindByValue(Convert.ToString(dt.Rows[0]["REIN_COMMISION_RECEVABLE"]));
				cmbRein_Commision_Recevable.SelectedIndex= cmbRein_Commision_Recevable.Items.IndexOf(listItem);	

             }
			 else
			 {
				//Add Mode
				 hidOldData.Value ="";
			 }
                
    	}
		#endregion

		#region PoplateComboBox

		private void PoplateComboBox()
		{
			DataSet objData;
			IList objList = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbCommision_Applicable.DataSource = objList;
			cmbCommision_Applicable.DataTextField="LookupDesc"; 
			cmbCommision_Applicable.DataValueField="LookupCode";
			cmbCommision_Applicable.DataBind();  
			cmbCommision_Applicable.Items.Insert(0,"");
			DataTable objTable;

			//Get The Dataset For Combo Box
			objData=objDataReinsure.GetComboBox();

			//Reinsurance Income type account
			if(objData.Tables[0].Rows.Count>0)
			{
				//Get The Table From Table Collection
				objTable=objData.Tables[0];
				cmbRein_Premium_Act.DataSource=objTable;
				cmbRein_Premium_Act.DataTextField ="ACC_DESCRIPTION";
				cmbRein_Premium_Act.DataValueField="AccountID";
                cmbRein_Premium_Act.DataBind();
				cmbRein_Premium_Act.Items.Insert(0,"");
			}
			//Reinsurance Liablity Type Account
			if(objData.Tables[1].Rows.Count>0)
			{
				//Get The Table From Table Collection
                objTable=objData.Tables[1];
				cmbRein_Payment_Act.DataSource = objTable;
				cmbRein_Payment_Act.DataTextField ="ACC_DESCRIPTION";
				cmbRein_Payment_Act.DataValueField ="AccountID";
				cmbRein_Payment_Act.DataBind();
				cmbRein_Payment_Act.Items.Insert(0,"");
			}
			//Reinsurance Expence Type Account
			if(objData.Tables[2].Rows.Count>0)
			{
				//Get The Table From Table Collection
                objTable=objData.Tables[2];
				cmbRein_Commision_Act.DataSource =objTable;
				cmbRein_Commision_Act.DataTextField ="ACC_DESCRIPTION";
				cmbRein_Commision_Act.DataValueField ="AccountID";
				cmbRein_Commision_Act.DataBind();
				cmbRein_Commision_Act.Items.Insert(0,"");
			}
			//Reinsurance Asset Type Account
			if(objData.Tables[3].Rows.Count>0)
			{
				//Get The Table From Table Collection
				objTable=objData.Tables[3];
				cmbRein_Commision_Recevable.DataSource =objTable;
				cmbRein_Commision_Recevable.DataTextField ="ACC_DESCRIPTION";
				cmbRein_Commision_Recevable.DataValueField ="AccountID";
				cmbRein_Commision_Recevable.DataBind();
				cmbRein_Commision_Recevable.Items.Insert(0,"");
			}
		}

		#endregion

        #region SetCaption

		private void SetCaption()
		{
			//Set Captions
			objResourceMgr                 =    new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddReinsurancePosting" ,System.Reflection.Assembly.GetExecutingAssembly());
			capContract_Name.Text          =    objResourceMgr.GetString("capContract_Name");
			capCommision_Applicable.Text   =    objResourceMgr.GetString("cmbCommision_Applicable");
			capRein_Premium_Act.Text       =    objResourceMgr.GetString("cmbRein_Premium_Act");
			capRein_Payment_Act.Text       =    objResourceMgr.GetString("cmbRein_Payment_Act");
			capRein_Commision_Act.Text     =    objResourceMgr.GetString("cmbRein_Commision_Act");
			capRein_Commision_Recevable.Text =  objResourceMgr.GetString("cmbRein_Commision_Recevable");

		}

        #endregion

		
		#region SetSecutityXml
		private void SetSecutityXml()
		{
			//Set Button Security
			btnSave.CmsButtonClass        =  CmsButtonType.Write;
			btnSave.PermissionString      =  gstrSecurityXML;

			btnReset.CmsButtonClass       =  CmsButtonType.Write;
			btnReset.PermissionString     =  gstrSecurityXML; 


		}

		#endregion
		#region SetErrorMessages
		private void SetErrorMessages()
		{
			//Accepts Screen ID and message ID and returns the string message.
			rfvCommision_Applicable.ErrorMessage         =          Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvRein_Premium_Act.ErrorMessage             =          Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvReinsurance_Pay_Act.ErrorMessage          =          Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvRein_Commision_Act.ErrorMessage           =          Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			rfvRein_Commision_Recevable.ErrorMessage     =          Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			
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
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
        #region Web Form Designer generated code
		
       #endregion
		#region Get Form Values

		private ClsReinsurancePostingInfo  GetFormValues()
		{
			string[] split;
			const char INTERNALSEPARATOR = '^';
           
			objModelReinsure.contract_id=int.Parse(hidContact_id.Value.ToString());
			objModelReinsure.Commision_applicable=int.Parse(cmbCommision_Applicable.SelectedValue);
			 //Split to seperate GL_id and Account Id Based on ^ 
			split=cmbRein_Premium_Act.SelectedValue.ToString().Split(new char[]{INTERNALSEPARATOR});
			//Get Gl Id
			objModelReinsure.GL_ID=int.Parse(split[0].ToString());
			//Get The Premium Account No
			objModelReinsure.Rein_Premium_Act =int.Parse(split[1].ToString());
            
			split=cmbRein_Payment_Act.SelectedValue.ToString().Split(new char[]{INTERNALSEPARATOR});
			objModelReinsure.Rein_Payment_Act =int.Parse(split[1].ToString());
			//Check For Commision Applicable
			if(int.Parse(cmbCommision_Applicable.SelectedValue) ==1)
			{
				split=cmbRein_Commision_Act.SelectedValue.ToString().Split(new char[]{INTERNALSEPARATOR});
				objModelReinsure.Rein_Commision_Act =int.Parse(split[1].ToString());
			
				split=cmbRein_Commision_Recevable.SelectedValue.ToString().Split(new char[]{INTERNALSEPARATOR});
				objModelReinsure.Rein_Commision_Recevable  =int.Parse(split[1].ToString());
			}
			//Return Model Object
			return objModelReinsure;
		}
		#endregion
		#region Save Click
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			objModelReinsure=new ClsReinsurancePostingInfo();
			//Get froma values
			objModelReinsure=GetFormValues();
			objModelReinsure.CREATED_BY =int.Parse(GetUserId());
			int intResult;
		try
		{
			if(hidOldData.Value=="")
			{
				//Add Mode
				intResult  = objDataReinsure.Add(objModelReinsure);   
				if(intResult>0)
				{
					
					GetoldXml();
					lblMessage.Visible = true;
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","29");

				}
				
			}

				//Update Mode

			else
			{
				ClsReinsurancePostingInfo objOldReinsuranceInfo;
				objOldReinsuranceInfo = new ClsReinsurancePostingInfo();

				//Setting  the Old Page details(XML File containing old details) into the Model Object
				base.PopulateModelObject(objOldReinsuranceInfo,hidOldData.Value);
				objModelReinsure.MODIFIED_BY = int.Parse(GetUserId());
				objModelReinsure.LAST_UPDATED_DATETIME = DateTime.Now;
				intResult=objDataReinsure.Update(objOldReinsuranceInfo,objModelReinsure);
					if(intResult>0)
					{
						lblMessage.Visible = true;
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","31");
						GetoldXml();
					}


			}
		}
			catch(Exception ex)
			{
				lblMessage.Visible = true;
				lblMessage.Text = ex.Message;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

				if ( ex.InnerException != null )
				{
					lblMessage.Text = ex.InnerException.Message;
				}

				
			}

          
			
		
		}
		#endregion

		private void btnReset_Click(object sender, System.EventArgs e)
		{
			
	    //For Reset The controls According to Old Data
		//Disable The Label
		lblMessage.Visible =false;
		GetoldXml();
		}
	}
}
