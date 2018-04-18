/* ***************************************************************************************
   Author		: Harmanjeet Singh
   Creation Date: May 7, 2007
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
	/// Summary description for AddReinsuranceMinorPart.
	/// </summary>
	public class AddReinsuranceMinorPart : Cms.CmsWeb.cmsbase
	{
		# region D E C L A R A T I O N
		
		protected System.Web.UI.WebControls.Label lblMessage;
		
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCONTRACT_ID;

		ClsReinsuranceMinorParticipation objReinsuranceMinorParticipation;
		System.Resources.ResourceManager objResourceMgr;

		private string strRowId, strFormSaved;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnActivate;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidReset;
		protected System.Web.UI.WebControls.DropDownList cmbMINOR_PARTICIPANTS;
		protected System.Web.UI.WebControls.DropDownList cmbMAJOR_PARTICIPANTS;
		protected System.Web.UI.WebControls.Label capMINOR_PARTICIPANTS;
		protected System.Web.UI.WebControls.Label capMAJOR_PARTICIPANTS;
		protected System.Web.UI.WebControls.TextBox txtMAJOR_PARTICIPANTS;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMINOR_PARTICIPATION_ID;
		protected System.Web.UI.WebControls.Label capMINOR_LAYER;
		protected System.Web.UI.WebControls.TextBox txtMINOR_LAYER;
		protected System.Web.UI.WebControls.Label capMINOR_WHOLE_PERCENT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMINOR_WHOLE_PERCENT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMINOR_LAYER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAJOR_PARTICIPANTS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMINOR_PARTICIPANTS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMINOR_WHOLE_PERCENT;
		protected System.Web.UI.WebControls.CustomValidator csvMINOR_WHOLE_PERCENT; 
		protected System.Web.UI.WebControls.CustomValidator csvMINOR_WHOLE_PERCENT_AMOUNT; 
		protected System.Web.UI.WebControls.TextBox txtMINOR_WHOLE_PERCENT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLayer;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMajor_Participants;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOLDTOTALPERCENT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMINOR_WHOLE_PERCENT;
        protected System.Web.UI.WebControls.Label capMessages;
	
		protected string oldXML;
		protected int intContactID;
		protected int intLayer;
        
		# endregion 

		# region P A G E   L O A D 

		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				objReinsuranceMinorParticipation = new ClsReinsuranceMinorParticipation();
				//btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");

				string url = ClsCommon.GetLookupURL();
				//imgSelect.Attributes.Add("onclick","javascript:OpenLookup('" + url + "','REIN_COMAPANY_ID','REIN_COMAPANY_ACC_NUMBER','hidACCOUNT_NUMBER_ID','txtACCOUNT_NUMBER','ReinsuranceAccountNumber','ReinsuranceAccountNumber','');");			
				btnReset.Attributes.Add("onclick","javascript:return Reset();");
				//base.ScreenId = "262_4_0";
				base.ScreenId = "262_6";
				lblMessage.Visible = false;
				SetErrorMessages();
                capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

				//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
				btnReset.CmsButtonClass		=	CmsButtonType.Write;
				btnReset.PermissionString	=	gstrSecurityXML;

				btnSave.CmsButtonClass		=	CmsButtonType.Write;
				btnSave.PermissionString	=	gstrSecurityXML;
				
				btnActivate.CmsButtonClass	=	CmsButtonType.Write;
				btnActivate.PermissionString		=	gstrSecurityXML;

				this.btnDelete.CmsButtonClass =     CmsButtonType.Delete;
				this.btnDelete.PermissionString =	gstrSecurityXML;
		
				objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddReinsuranceMinorPart" ,System.Reflection.Assembly.GetExecutingAssembly());

				this.hidMajor_Participants.Value=Request.QueryString["Major_Participants"];
				this.hidLayer.Value=Request.QueryString["Layer"];
				if (Request.QueryString["ContractID"]!=null && Request.QueryString["ContractID"].ToString().Length>0)
					intContactID=int.Parse(Request.QueryString["ContractID"].ToString());
				else
					intContactID=0;
				SetCaptions();
				txtMINOR_LAYER.ReadOnly=true;
				
				
				if(!Page.IsPostBack)
				{
					//if(Request.QueryString["ContractID"]!=null && Request.QueryString["ContractID"].ToString().Length>0)
					//	hidCONTRACT_ID.Value = Request.QueryString["ContractID"].ToString();
					
				
					if(Request.QueryString["MINOR_PARTICIPATION_ID"]!=null && Request.QueryString["MINOR_PARTICIPATION_ID"].ToString().Length>0)
					{
						this.hidMINOR_PARTICIPATION_ID.Value = Request.QueryString["MINOR_PARTICIPATION_ID"].ToString();						
						GenerateXML(this.hidMINOR_PARTICIPATION_ID.Value);
						//hidPARTICIPATION_TYPE.Value = "MINOR"; //This is set to show that the screen is of 'MAJOR PARTICIPATION'
					}
						
					
					#region "Loading singleton"
					//using singleton object for country and state dropdown
				
					//this.cmbMINOR_PARTICIPANTS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
					this.cmbMINOR_PARTICIPANTS.DataSource =ClsReinsuranceMinorParticipation.GetMajorMinorParticipents(intContactID,"MINOR");
					cmbMINOR_PARTICIPANTS.DataTextField="REIN_COMPANY_NAME";
					cmbMINOR_PARTICIPANTS.DataValueField="REIN_COMPANY_ID";
					cmbMINOR_PARTICIPANTS.DataBind();
					cmbMINOR_PARTICIPANTS.Items.Insert(0,"");
					//cmbMINOR_PARTICIPANTS.Items[0].Selected=true;

					cmbMAJOR_PARTICIPANTS.DataSource= ClsReinsuranceMinorParticipation.GetMajorMinorParticipents(intContactID,"MAJOR");
					cmbMAJOR_PARTICIPANTS.DataTextField="REIN_COMPANY_NAME";
					cmbMAJOR_PARTICIPANTS.DataValueField="PARTICIPATION_ID";
					cmbMAJOR_PARTICIPANTS.DataBind();
					cmbMAJOR_PARTICIPANTS.Items.Insert(0,"");

					
					//cmbMINOR_PARTICIPANTS.SelectedIndex=1;




					#endregion//Loading singleton
					GetDataForEditMode();
					
				}
				if(txtMINOR_LAYER.Text != "")
				{
					intLayer = int.Parse(txtMINOR_LAYER.Text); 
				}
				hidOLDTOTALPERCENT.Value= ClsReinsuranceMinorParticipation.GetTotalPercentage(intContactID,"MINOR",intLayer).ToString();
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
			
			this.revMINOR_WHOLE_PERCENT.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage("G","216");
			this.revMINOR_WHOLE_PERCENT.ValidationExpression			= aRegExpDoublePositiveNonZero;
			
			//this.revLAYER.ErrorMessage="Please enter numbers upto 2 digits";
			//this.revLAYER.ValidationExpression="^[0-9]{0,2}";
			csvMINOR_WHOLE_PERCENT.ErrorMessage							=Cms.CmsWeb.ClsMessages.GetMessage("G","989");

			
		}

		#endregion

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsReinsuranceMinorParticipationInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			Cms.Model.Maintenance.Reinsurance.ClsReinsuranceMinorParticipationInfo  objReinsuranceMinorParticipationInfo;
			objReinsuranceMinorParticipationInfo = new ClsReinsuranceMinorParticipationInfo();
			
			//objReinsuranceMajorParticipationInfo.REINSURANCE_COMPANY = this.cmbREINSURANCE_COMPANY.SelectedValue;
			//objReinsuranceMajorParticipationInfo.LAYER =int.Parse(this.txtLAYER.Text);
			//objReinsuranceMajorParticipationInfo.NET_RETENTION = int.Parse(this.cmbNET_RETENTION.SelectedValue);
			string[] strMAJORPARTICIPANTS =cmbMAJOR_PARTICIPANTS.SelectedValue.Split('~');
			objReinsuranceMinorParticipationInfo.MAJOR_PARTICIPANTS=strMAJORPARTICIPANTS[0].ToString(); //this.txtMAJOR_PARTICIPANTS.Text;

            //SANTOSH GAUTAM : BELOW LINE MODIFIED ON 01 Nov 2010
            //OLD VALUE => TbjReinsuranceMinorParticipationInfo.MINOR_LAYER = int.Parse(this.txtMINOR_LAYER.Text);  
            if(!string.IsNullOrEmpty(strMAJORPARTICIPANTS[1]))
                objReinsuranceMinorParticipationInfo.MINOR_LAYER = int.Parse(strMAJORPARTICIPANTS[1]);

			objReinsuranceMinorParticipationInfo.MINOR_WHOLE_PERCENT=double.Parse(this.txtMINOR_WHOLE_PERCENT.Text);
			objReinsuranceMinorParticipationInfo.MINOR_PARTICIPANTS=int.Parse(this.cmbMINOR_PARTICIPANTS.SelectedValue);
			objReinsuranceMinorParticipationInfo.CONTRACT_ID=intContactID;
			//objReinsuranceContactInfo.IS_ACTIVE=hidIS_ACTIVE.Value;
			
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	this.hidMINOR_PARTICIPATION_ID.Value;
			//oldXML		= hidOldData.Value;
			//Returning the model object

			return objReinsuranceMinorParticipationInfo;
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
				
				objReinsuranceMinorParticipation= new ClsReinsuranceMinorParticipation();

				//Retreiving the form values into model class object
				ClsReinsuranceMinorParticipationInfo objReinsuranceMinorParticipationInfo = GetFormValue();
				
				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					if (double.Parse(hidOLDTOTALPERCENT.Value) + double.Parse(txtMINOR_WHOLE_PERCENT.Text) >100)
					{
						csvMINOR_WHOLE_PERCENT.IsValid =false;
						return;
					}
					else
					{
						objReinsuranceMinorParticipationInfo.CREATED_BY = int.Parse(GetUserId());
						objReinsuranceMinorParticipationInfo.CREATED_DATETIME = DateTime.Now;
						//objTIVGroupInfo.IS_ACTIVE="Y"; 

						//Calling the add method of business layer class
						intRetVal = objReinsuranceMinorParticipation.Add(objReinsuranceMinorParticipationInfo);

						if(intRetVal>0)
						{
							this.hidMINOR_PARTICIPATION_ID.Value = objReinsuranceMinorParticipationInfo.MINOR_PARTICIPATION_ID.ToString();
							lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
							hidFormSaved.Value			=	"1";
							this.hidOldData.Value	= objReinsuranceMinorParticipation.GetDataForPageControls(this.hidMINOR_PARTICIPATION_ID.Value).GetXml();
							hidOLDTOTALPERCENT.Value= ClsReinsuranceMinorParticipation.GetTotalPercentage(intContactID,"MINOR",intLayer).ToString();
							hidIS_ACTIVE.Value = "Y";
						}
						else if(intRetVal == -1)
						{
							lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"18");
							hidFormSaved.Value			=		"2";
						}
						else
						{
							lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
							hidFormSaved.Value			=	"2";
						}
						lblMessage.Visible = true;
					}
				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsReinsuranceMinorParticipationInfo objOldReinsuranceMinorParticipationInfo;
					
					objOldReinsuranceMinorParticipationInfo = new ClsReinsuranceMinorParticipationInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldReinsuranceMinorParticipationInfo,hidOldData.Value);
					double totalper = objOldReinsuranceMinorParticipationInfo.MINOR_WHOLE_PERCENT;
					if (((double.Parse(hidOLDTOTALPERCENT.Value)- totalper) + double.Parse(txtMINOR_WHOLE_PERCENT.Text)) >100)
					{
						csvMINOR_WHOLE_PERCENT.IsValid =false;
						return;
					}
					else
					{
						//Setting those values into the Model object which are not in the page
						if(strRowId!="")
							objReinsuranceMinorParticipationInfo.MINOR_PARTICIPATION_ID = int.Parse(strRowId);
						objReinsuranceMinorParticipationInfo.MODIFIED_BY = int.Parse(GetUserId());
						objReinsuranceMinorParticipationInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                    

						//Updating the record using business layer class object

						intRetVal	= objReinsuranceMinorParticipation.Update(objOldReinsuranceMinorParticipationInfo,objReinsuranceMinorParticipationInfo);
						if( intRetVal > 0 )			// update successfully performed
						{
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
							hidFormSaved.Value		=	"1";
							this.hidOldData.Value	= objReinsuranceMinorParticipation.GetDataForPageControls(strRowId).GetXml();
							hidOLDTOTALPERCENT.Value= ClsReinsuranceMinorParticipation.GetTotalPercentage(intContactID,"MINOR",intLayer).ToString();
	               
						}
						else if(intRetVal == -1)	// Duplicate code exist, update failed
						{
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
							hidFormSaved.Value		=	"1";
						}
						else 
						{
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
							hidFormSaved.Value		=	"1";
						}
						lblMessage.Visible = true;
						}
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
				if(objReinsuranceMinorParticipation!= null)
					objReinsuranceMinorParticipation.Dispose();
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
            
			objReinsuranceMinorParticipation=new ClsReinsuranceMinorParticipation(); 
  
			
			if(strPARTICIPATION_ID!="" && strPARTICIPATION_ID!=null)
			{
				try
				{
					DataSet ds=new DataSet(); 
					ds=objReinsuranceMinorParticipation.GetDataForPageControls(strPARTICIPATION_ID);
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
					if(objReinsuranceMinorParticipation!= null)
						objReinsuranceMinorParticipation.Dispose();
				}  
                
			}
                
		}

		# endregion GENERATE XML

		# region SET CAPTIONS
		private void SetCaptions()
		{
			this.capMAJOR_PARTICIPANTS.Text=		objResourceMgr.GetString("txtMAJOR_PARTICIPANTS");
			this.capMINOR_LAYER.Text		=				objResourceMgr.GetString("txtMINOR_LAYER");
			this.capMINOR_PARTICIPANTS.Text			=		objResourceMgr.GetString("cmbMINOR_PARTICIPANTS");
			this.capMINOR_WHOLE_PERCENT.Text					=		objResourceMgr.GetString("txtMINOR_WHOLE_PERCENT");
			
			
		}

		# endregion SET CAPTIONS

		#region DEACTIVATE ACTIVATE BUTTON CLICK
		
		private void btnActivate_Click(object sender, System.EventArgs e)
		{
			objReinsuranceMinorParticipation = new ClsReinsuranceMinorParticipation();

			ClsReinsuranceMinorParticipationInfo objReinsuranceMinorParticipationInfo;
					
			objReinsuranceMinorParticipationInfo = new ClsReinsuranceMinorParticipationInfo();

			//Setting  the Old Page details(XML File containing old details) into the Model Object
			base.PopulateModelObject(objReinsuranceMinorParticipationInfo,hidOldData.Value);

			//Setting those values into the Model object which are not in the page
			objReinsuranceMinorParticipationInfo.MINOR_PARTICIPATION_ID = int.Parse(hidMINOR_PARTICIPATION_ID.Value.ToString());
			objReinsuranceMinorParticipationInfo.MODIFIED_BY = int.Parse(GetUserId());
			objReinsuranceMinorParticipationInfo.LAST_UPDATED_DATETIME = DateTime.Now;
						
			if(this.btnActivate.Text=="Deactivate")
			{
						  
				int intStatusCheck=objReinsuranceMinorParticipation.GetDeactivateActivate(this.hidMINOR_PARTICIPATION_ID.Value.ToString(),"N", objReinsuranceMinorParticipationInfo);
				btnActivate.Text="Activate";
				this.btnDelete.Visible=false;
				lblMessage.Text=ClsMessages.GetMessage("G", "41");
				lblMessage.Visible=true;
				hidFormSaved.Value="1";
				//SetReadOnly();
			}
			else
			{
				
				if (int.Parse(hidOLDTOTALPERCENT.Value) + double.Parse(txtMINOR_WHOLE_PERCENT.Text) >100)
				{
					csvMINOR_WHOLE_PERCENT.IsValid =false;
					return;
				}
				int intStatusCheck=objReinsuranceMinorParticipation.GetDeactivateActivate(this.hidMINOR_PARTICIPATION_ID.Value.ToString(),"Y", objReinsuranceMinorParticipationInfo);
				btnActivate.Text="Deactivate";
				this.btnDelete.Visible=true;
				lblMessage.Text=ClsMessages.GetMessage("G","40");
				lblMessage.Visible=true;
				hidFormSaved.Value="1";
			}
		
			hidOLDTOTALPERCENT.Value= ClsReinsuranceMinorParticipation.GetTotalPercentage(intContactID,"MINOR",intLayer).ToString();
	
		}

		
		#endregion

		# region  G E T   D A T A   F O R   E D I T   M O D E 

		private void GetDataForEditMode()
		{
			try
			{
				
				objReinsuranceMinorParticipation = new ClsReinsuranceMinorParticipation();
				DataSet oDs = objReinsuranceMinorParticipation.GetDataForPageControls(this.hidMINOR_PARTICIPATION_ID.Value);
				if(oDs.Tables[0].Rows.Count >0)
				{
					DataRow oDr  = oDs.Tables[0].Rows[0];
					
						
					this.txtMINOR_LAYER.Text=oDr["MINOR_LAYER"].ToString();
					this.txtMINOR_WHOLE_PERCENT.Text=oDr["MINOR_WHOLE_PERCENT"].ToString();
					
					if(oDr["IS_ACTIVE"].ToString()=="Y")
					{
						btnActivate.Text="Deactivate";
						this.btnDelete.Visible=true;
						
					}
					string[] MAJOR_PARTICIPANTS;
					for (int intIndex=0;intIndex < cmbMAJOR_PARTICIPANTS.Items.Count;intIndex++)
					{
					    MAJOR_PARTICIPANTS =cmbMAJOR_PARTICIPANTS.Items[intIndex].Value.Split('~'); 
						if (MAJOR_PARTICIPANTS[0]==oDr["MAJOR_PARTICIPANTS"].ToString())
						{
							this.cmbMAJOR_PARTICIPANTS.SelectedIndex=intIndex;
							break;
						}

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
		
		# region DELETE RECORDS
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				objReinsuranceMinorParticipation = new ClsReinsuranceMinorParticipation();
				ClsReinsuranceMinorParticipationInfo objReinsuranceMinorParticipationInfo=GetFormValue();
				objReinsuranceMinorParticipationInfo.MODIFIED_BY=int.Parse(GetUserId());
				objReinsuranceMinorParticipationInfo.LAST_UPDATED_DATETIME=DateTime.Now;
				int intStatusCheck=objReinsuranceMinorParticipation.Delete(this.hidMINOR_PARTICIPATION_ID.Value.ToString(),objReinsuranceMinorParticipationInfo);
				hidOLDTOTALPERCENT.Value= ClsReinsuranceMinorParticipation.GetTotalPercentage(intContactID,"MINOR",intLayer).ToString();
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
				if(objReinsuranceMinorParticipation!= null)
					objReinsuranceMinorParticipation.Dispose();
			}
		
		
		}
		
		# endregion 

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

		
		

		

		
	}
}






