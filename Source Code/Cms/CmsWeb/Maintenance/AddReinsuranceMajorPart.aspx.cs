/* ***************************************************************************************
   Author		: Harmanjeet Singh
   Creation Date: April 30, 2007
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

using Cms.BusinessLayer.BlCommon;
using Cms.Model.Maintenance.Reinsurance;
using Cms.ExceptionPublisher.ExceptionManagement;
using System.Xml;
using Cms;
using Cms.CmsWeb.Controls;

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for AddReinsuranceMajorPart.
	/// </summary>
	public class AddReinsuranceMajorPart : Cms.CmsWeb.cmsbase
	{
		# region D E C L A R A T I O N
        
		System.Resources.ResourceManager objResourceMgr;

		ClsReinsuranceMajorParticipation objReinsuranceMajorParticipation;

		private string strRowId, strFormSaved;
		string oldXML;


		protected System.Web.UI.WebControls.Label lblMessage;
		
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

		protected System.Web.UI.WebControls.Label lblDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnActivate;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidReset;
		protected System.Web.UI.WebControls.Label capREINSURANCE_COMPANY;
		protected System.Web.UI.WebControls.Label capLAYER;
		protected System.Web.UI.WebControls.TextBox txtLAYER;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLAYER;
		protected System.Web.UI.WebControls.Label capNET_RETENTION;
		protected System.Web.UI.WebControls.Label capWHOLE_PERCENT;
		protected System.Web.UI.WebControls.TextBox txtWHOLE_PERCENT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revWHOLE_PERCENT;
		protected System.Web.UI.WebControls.Label capMINOR_PARTICIPANTS;
		protected System.Web.UI.WebControls.DropDownList cmbNET_RETENTION;
		protected System.Web.UI.WebControls.DropDownList cmbMINOR_PARTICIPANTS;
		protected System.Web.UI.WebControls.DropDownList cmbREINSURANCE_COMPANY;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCONTRACT_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPARTICIPATION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOLDTOTALPERCENT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidWHOLE_PERCENT;
		protected System.Web.UI.WebControls.RangeValidator rngWHOLE_PERCENT;
		protected System.Web.UI.WebControls.CustomValidator csvWHOLE_PERCENT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLAYER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNET_RETENTION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvWHOLE_PERCENT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMINOR_PARTICIPANTS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREINSURANCE_COMPANY;
		protected int intContactID;
		
        
		# endregion 

		# region P A G E   L O A D 

		private void Page_Load(object sender, System.EventArgs e)
		{
            
          
			try
			{
				objReinsuranceMajorParticipation = new ClsReinsuranceMajorParticipation();
				//btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
				btnReset.Attributes.Add("onclick","javascript:return Reset();");
				string url = ClsCommon.GetLookupURL();
				//imgSelect.Attributes.Add("onclick","javascript:OpenLookup('" + url + "','REIN_COMAPANY_ID','REIN_COMAPANY_ACC_NUMBER','hidACCOUNT_NUMBER_ID','txtACCOUNT_NUMBER','ReinsuranceAccountNumber','ReinsuranceAccountNumber','');");			

				//base.ScreenId = "262_5_0";
				base.ScreenId = "262_4";
				lblMessage.Visible = false;
				SetErrorMessages();

				//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
				btnReset.CmsButtonClass		=	CmsButtonType.Write;
				btnReset.PermissionString	=	gstrSecurityXML;

				btnSave.CmsButtonClass		=	CmsButtonType.Write;
				btnSave.PermissionString	=	gstrSecurityXML;
				
				btnActivate.CmsButtonClass	=	CmsButtonType.Write;
				btnActivate.PermissionString		=	gstrSecurityXML;

				this.btnDelete.CmsButtonClass =     CmsButtonType.Delete;
				this.btnDelete.PermissionString =	gstrSecurityXML;
		
				if(Request.QueryString["ContractID"]!=null && Request.QueryString["ContractID"].ToString().Length>0)
					hidCONTRACT_ID.Value = Request.QueryString["ContractID"].ToString();
				intContactID= int.Parse(hidCONTRACT_ID.Value);
				//hidOLDTOTALPERCENT.Value= ClsReinsuranceMinorParticipation.GetTotalPercentage(intContactID,"MAJOR").ToString();
		
				objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddReinsuranceMajorPart" ,System.Reflection.Assembly.GetExecutingAssembly());
				if(!Page.IsPostBack)
				{
					SetCaptions();
					if(Request.QueryString["PARTICIPATION_ID"]!=null && Request.QueryString["PARTICIPATION_ID"].ToString().Length>0)
					{
						hidPARTICIPATION_ID.Value = Request.QueryString["PARTICIPATION_ID"].ToString();						
						GenerateXML(this.hidPARTICIPATION_ID.Value);
						//hidPARTICIPATION_TYPE.Value = "MAJOR"; //This is set to show that the screen is of 'MAJOR PARTICIPATION'
					}
					
					#region "Loading singleton"
					//using singleton object for country and state dropdown
				
					DataTable dt = objReinsuranceMajorParticipation.GetReinsurerBroker().Tables[0];
					this.cmbREINSURANCE_COMPANY.DataSource		= dt;
					cmbREINSURANCE_COMPANY.DataTextField	= "REIN_COMPANY_NAME";
					cmbREINSURANCE_COMPANY.DataValueField	= "REIN_COMPANY_ID";
					cmbREINSURANCE_COMPANY.DataBind();
					cmbREINSURANCE_COMPANY.Items.Insert(0,"");
					//cmbREINSURANCE_COMPANY.Items[0].Selected=true;  
				
					this.cmbNET_RETENTION.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
					cmbNET_RETENTION.DataTextField="LookupDesc";
					cmbNET_RETENTION.DataValueField="LookupID";
					cmbNET_RETENTION.DataBind();
					cmbNET_RETENTION.Items.Insert(0,"");
					cmbNET_RETENTION.Items[0].Selected=true;
					//cmbNET_RETENTION.SelectedIndex=1;
					
					this.cmbMINOR_PARTICIPANTS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
					cmbMINOR_PARTICIPANTS.DataTextField="LookupDesc";
					cmbMINOR_PARTICIPANTS.DataValueField="LookupID";
					cmbMINOR_PARTICIPANTS.DataBind();
					cmbMINOR_PARTICIPANTS.Items.Insert(0,"");
					//cmbMINOR_PARTICIPANTS.Items[0].Selected=true;
					
					//cmbMINOR_PARTICIPANTS.SelectedIndex=1;
					#endregion//Loading singleton
					GetDataForEditMode();
				}
			}
			catch(Exception oEx)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);			
			}
		}

		# endregion 

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{			
			this.revWHOLE_PERCENT.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage("G","216");
            this.revWHOLE_PERCENT.ValidationExpression = aRegExpDoublePositiveNonZero;
			
			this.revLAYER.ErrorMessage="Please enter numbers upto 2 digits";
			this.revLAYER.ValidationExpression="^[0-9]{0,2}";
			this.csvWHOLE_PERCENT.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage("G","989");			
		}

		#endregion

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsReinsuranceMajorParticipationInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			Cms.Model.Maintenance.Reinsurance.ClsReinsuranceMajorParticipationInfo objReinsuranceMajorParticipationInfo;
			objReinsuranceMajorParticipationInfo = new ClsReinsuranceMajorParticipationInfo();
			
			objReinsuranceMajorParticipationInfo.REINSURANCE_COMPANY = this.cmbREINSURANCE_COMPANY.SelectedValue;
			if (txtLAYER.Text!="")
			objReinsuranceMajorParticipationInfo.LAYER =int.Parse(this.txtLAYER.Text);
			else
			objReinsuranceMajorParticipationInfo.LAYER =0;
			objReinsuranceMajorParticipationInfo.NET_RETENTION = int.Parse(this.cmbNET_RETENTION.SelectedValue);
			if (txtWHOLE_PERCENT.Text!="")
				objReinsuranceMajorParticipationInfo.WHOLE_PERCENT=double.Parse(this.txtWHOLE_PERCENT.Text);
			else
			   objReinsuranceMajorParticipationInfo.WHOLE_PERCENT=0;
			objReinsuranceMajorParticipationInfo.MINOR_PARTICIPANTS=int.Parse(this.cmbMINOR_PARTICIPANTS.SelectedValue);
			objReinsuranceMajorParticipationInfo.CONTRACT_ID= int.Parse(hidCONTRACT_ID.Value);

			//objReinsuranceContactInfo.IS_ACTIVE=hidIS_ACTIVE.Value;
			
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	this.hidPARTICIPATION_ID.Value;
			//oldXML		= hidOldData.Value;
			//Returning the model object

			return objReinsuranceMajorParticipationInfo;
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
                
				objReinsuranceMajorParticipation= new ClsReinsuranceMajorParticipation();

				//Retreiving the form values into model class object
				ClsReinsuranceMajorParticipationInfo objReinsuranceMajorParticipationInfo = GetFormValue();
				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objReinsuranceMajorParticipationInfo.CREATED_BY = int.Parse(GetUserId());
					objReinsuranceMajorParticipationInfo.CREATED_DATETIME = DateTime.Now;
					//objTIVGroupInfo.IS_ACTIVE="Y"; 

					//Calling the add method of business layer class
					intRetVal = objReinsuranceMajorParticipation.Add(objReinsuranceMajorParticipationInfo);

					if(intRetVal>0)
					{
						this.hidPARTICIPATION_ID.Value = objReinsuranceMajorParticipationInfo.PARTICIPATION_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						this.hidOldData.Value	= objReinsuranceMajorParticipation.GetDataForPageControls(this.hidPARTICIPATION_ID.Value).GetXml();
                        hidOLDTOTALPERCENT.Value = ClsReinsuranceMinorParticipation.GetTotalPercentage(intContactID, "MAJOR", objReinsuranceMajorParticipationInfo.LAYER, objReinsuranceMajorParticipationInfo.PARTICIPATION_ID).ToString();
		
						hidIS_ACTIVE.Value = "Y";
					}
					else if(intRetVal == -1)
					{
                        lblMessage.Text =                   "Reinsurance Contracts Commission should not be grater than 100%.";
                                                            //ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value			=		"2";
					}
                    else if (intRetVal == -2)
                    {
                        lblMessage.Text =                   "Information could not be saved.Please Fill Loss Layer First.";
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetVal == -3)
                    {
                        lblMessage.Text =                   "Information already added for the same Reinsurer/Broker";
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                        hidFormSaved.Value = "2";
                    }
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsReinsuranceMajorParticipationInfo objOldReinsuranceMajorParticipationInfo;
					
					objOldReinsuranceMajorParticipationInfo = new ClsReinsuranceMajorParticipationInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldReinsuranceMajorParticipationInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					if(strRowId!="")
					objReinsuranceMajorParticipationInfo.PARTICIPATION_ID = int.Parse(strRowId);
					objReinsuranceMajorParticipationInfo.MODIFIED_BY = int.Parse(GetUserId());
					objReinsuranceMajorParticipationInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                    

					//Updating the record using business layer class object
					intRetVal	= objReinsuranceMajorParticipation.Update(objOldReinsuranceMajorParticipationInfo,objReinsuranceMajorParticipationInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						this.hidOldData.Value	= objReinsuranceMajorParticipation.GetDataForPageControls(strRowId).GetXml();
                        hidOLDTOTALPERCENT.Value = ClsReinsuranceMinorParticipation.GetTotalPercentage(intContactID, "MAJOR", objOldReinsuranceMajorParticipationInfo.LAYER, objReinsuranceMajorParticipationInfo.PARTICIPATION_ID).ToString();
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
                        lblMessage.Text =                "Reinsurance Contracts Commission should not be grater than 100%.";
                                                          //Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
						hidFormSaved.Value		    =	  "1";
					}
					else 
					{
						lblMessage.Text			=	    Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	    "1";
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
			finally
			{
				if(objReinsuranceMajorParticipation!= null)
					objReinsuranceMajorParticipation.Dispose();
			}

			
		}
	
		#endregion

		# region GENERATE XML
		/// <summary>
		/// fetching data based on query string values
		/// </summary>
		private void GenerateXML(string PARTICIPATION_ID)
		{
			string strPARTICIPATION_ID=PARTICIPATION_ID;
            
			objReinsuranceMajorParticipation=new ClsReinsuranceMajorParticipation(); 
  
			
			if(strPARTICIPATION_ID!="" && strPARTICIPATION_ID!=null)
			{
				try
				{
					DataSet ds=new DataSet(); 
					ds=objReinsuranceMajorParticipation.GetDataForPageControls(strPARTICIPATION_ID);
					hidOldData.Value=ds.GetXml(); 

					//hidFormSaved.Value="1"; 
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
					if(objReinsuranceMajorParticipation!= null)
						objReinsuranceMajorParticipation.Dispose();
				}  
                
			}
                
		}

		# endregion GENERATE XML

		# region SET CAPTIONS
		private void SetCaptions()
		{
			this.capREINSURANCE_COMPANY.Text=		objResourceMgr.GetString("cmbREINSURANCE_COMPANY");
			this.capLAYER.Text		=		objResourceMgr.GetString("txtLAYER");
			this.capNET_RETENTION.Text					=		objResourceMgr.GetString("cmbNET_RETENTION");
			this.capWHOLE_PERCENT.Text					=		objResourceMgr.GetString("txtWHOLE_PERCENT");
			this.capMINOR_PARTICIPANTS.Text			=		objResourceMgr.GetString("cmbMINOR_PARTICIPANTS");
	
			//END Harmanjeet
		}

		# endregion SET CAPTIONS

		#region DEACTIVATE ACTIVATE BUTTON CLICK
		private void btnActivate_Click(object sender, System.EventArgs e)
		{
		
			objReinsuranceMajorParticipation = new ClsReinsuranceMajorParticipation();
			ClsReinsuranceMajorParticipationInfo objReinsuranceMajorParticipationInfo = GetFormValue();

			objReinsuranceMajorParticipationInfo.MODIFIED_BY = int.Parse(GetUserId());
			objReinsuranceMajorParticipationInfo.LAST_UPDATED_DATETIME = DateTime.Now;
			
			if(this.btnActivate.Text=="Deactivate")
			{
						  
				int intStatusCheck=objReinsuranceMajorParticipation.GetDeactivateActivate(objReinsuranceMajorParticipationInfo,this.hidPARTICIPATION_ID.Value.ToString(),"N");
				btnActivate.Text="Activate";
				this.btnDelete.Visible=false;
				lblMessage.Text=ClsMessages.GetMessage("G","41");
				lblMessage.Visible=true;
				hidFormSaved.Value="1";
			}
			else
			{
				
				
				if (int.Parse(hidOLDTOTALPERCENT.Value) + int.Parse(txtWHOLE_PERCENT.Text) >100)
				{
					csvWHOLE_PERCENT.IsValid =false;
					return;
				}
				int intStatusCheck=objReinsuranceMajorParticipation.GetDeactivateActivate(objReinsuranceMajorParticipationInfo,this.hidPARTICIPATION_ID.Value.ToString(),"Y");
				btnActivate.Text="Deactivate";
				this.btnDelete.Visible=true;
				lblMessage.Text=ClsMessages.GetMessage("G","40");
				lblMessage.Visible=true;
				hidFormSaved.Value="1";
			}
			//hidOLDTOTALPERCENT.Value= ClsReinsuranceMinorParticipation.GetTotalPercentage(intContactID,"MAJOR").ToString();
		
		}

		
		#endregion

		# region  G E T   D A T A   F O R   E D I T   M O D E 

		private void GetDataForEditMode()
		{
			try
			{
				
				objReinsuranceMajorParticipation = new ClsReinsuranceMajorParticipation();
				DataSet oDs = objReinsuranceMajorParticipation.GetDataForPageControls(this.hidPARTICIPATION_ID.Value);
				if(oDs.Tables[0].Rows.Count >0)
				{
					DataRow oDr  = oDs.Tables[0].Rows[0];
					
						
					this.txtLAYER.Text=oDr["LAYER"].ToString();
					this.txtWHOLE_PERCENT.Text=oDr["WHOLE_PERCENT"].ToString();
					
					if(oDr["IS_ACTIVE"].ToString()=="Y")
					{
						btnActivate.Text="Deactivate";
						this.btnDelete.Visible=true;
						
					}
					if(oDr["IS_ACTIVE"].ToString()=="N")
					{
						btnActivate.Text="Activate";
						this.btnDelete.Visible=false;
						
					}
					
				}
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
			finally{}
		}
		
		
		# endregion G E T   D A T A   F O R   E D I T   M O D E

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
			this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		

		

		

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
		
			try
			{
				objReinsuranceMajorParticipation = new ClsReinsuranceMajorParticipation();
				ClsReinsuranceMajorParticipationInfo objReinsuranceMajorParticipationInfo=GetFormValue();
				objReinsuranceMajorParticipationInfo.MODIFIED_BY=int.Parse(GetUserId());
				objReinsuranceMajorParticipationInfo.LAST_UPDATED_DATETIME=System.DateTime.Now;
				int intStatusCheck=objReinsuranceMajorParticipation.Delete(this.hidPARTICIPATION_ID.Value.ToString(),objReinsuranceMajorParticipationInfo);
                hidOLDTOTALPERCENT.Value = ClsReinsuranceMinorParticipation.GetTotalPercentage(intContactID, "MAJOR", objReinsuranceMajorParticipationInfo.LAYER, objReinsuranceMajorParticipationInfo.PARTICIPATION_ID).ToString();
				lblDelete.Visible = true;
				hidFormSaved.Value="1";
				trBody.Attributes.Add("style","display:none");
				lblDelete.Text ="Record has been deleted successfully";
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	"Could not be deleted.Error while Deleting. Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objReinsuranceMajorParticipation!= null)
					objReinsuranceMajorParticipation.Dispose();
			}
		
		
		
		}

		

		
	}
}


