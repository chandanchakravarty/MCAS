using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Configuration;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Application;
using System.IO;
using Cms.BusinessLayer.BlApplication;

namespace Cms.CmsWeb.aspx
{
	/// <summary>
	/// Summary description for QuickQuoteInfo.
	/// </summary>
	public partial class QuickQuoteInfo : cmsbase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capQQ_NUMBER;
		protected System.Web.UI.WebControls.Label capQQ_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbQQ_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvQQ_TYPE;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidQuoteId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidChkDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnQuickQuote;
		protected Cms.CmsWeb.Controls.CmsButton btnAddToApplication;
		protected Cms.CmsWeb.Controls.CmsButton btnCustomerAssistant;
		protected Cms.CmsWeb.Controls.CmsButton btnBack;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidState;
		public string primaryKeyValues="";

		//Defining the business layer class object
		private ClsQuickQuote objQuickQuote;
		private string strQQ_Id;
		private string strCustomerId;
		public bool DeleteFlag=true;
		protected System.Web.UI.WebControls.Label lblQuickQuoteNumber;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFromMenu;
		protected System.Web.UI.WebControls.Label lblStateName;
		protected System.Web.UI.WebControls.DropDownList cmbQQ_State;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvQQ_State;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tbBody;
		public bool UpdateGrid=false;
		//private string strState;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId	=	"1001";
			SetValidators();
				
			if(Request.QueryString["calledFromMenu"]!=null)
			{
				if(Request.QueryString["calledFromMenu"].ToString()=="Y")
				{
					btnCustomerAssistant.Visible=true; 
					btnBack.Visible=true; 
					base.ScreenId	=	"194"; //Added by Sibin on 07-10-08
				}
				else
				{
					btnCustomerAssistant.Visible=false; 
					btnBack.Visible=false; 
					base.ScreenId	=	"120_1_0";  //Added by Sibin on 07-10-08
				}
			}
			else
			{
				btnCustomerAssistant.Visible=true; 
				btnBack.Visible=true;
				base.ScreenId	=	"120_1_0";  //Added by Sibin on 07-10-08
			}
		
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnActivateDeactivate.CmsButtonClass		=	CmsButtonType.Write;
			btnActivateDeactivate.PermissionString		=	gstrSecurityXML;

			btnDelete.CmsButtonClass		=	CmsButtonType.Delete;
			btnDelete.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass			=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnAddToApplication.CmsButtonClass			=	CmsButtonType.Execute;
			btnAddToApplication.PermissionString		=	gstrSecurityXML;
			
			btnQuickQuote.CmsButtonClass		=	CmsButtonType.Write;
			btnQuickQuote.PermissionString		=	gstrSecurityXML;

			btnCustomerAssistant.CmsButtonClass		=	CmsButtonType.Read ;
			btnCustomerAssistant.PermissionString		=	gstrSecurityXML;

			btnBack.CmsButtonClass					=	CmsButtonType.Read ;
			btnBack.PermissionString				=	gstrSecurityXML;	
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			if(Request.QueryString["calledFromMenu"]!=null)
			{
				btnCustomerAssistant.Attributes.Add("onclick","javascript:return DoBackToAssistant();");
				btnBack.Attributes.Add("onclick","javascript:return DoBack();");
			}
			else
			{
				btnCustomerAssistant.Attributes.Add("onclick","javascript:return DoBackToAssistant_Parent();");
				btnBack.Attributes.Add("onclick","javascript:return DoBack_Parent();");
			}

			//btnCustomerAssistant.Attributes.Add("onclick","javascript:return DoBackToAssistant();");
			//btnBack.Attributes.Add("onclick","javascript:return DoBack();");
			btnAddToApplication.Attributes.Add("onClick","return confirmApplication();");

			objQuickQuote = new ClsQuickQuote();
			lblMessage.Visible = false;
			lblMessage.Text = "";
			
			strCustomerId=GetCustomerID().ToString();
			hidCustomerId.Value = strCustomerId;
			if (!Page.IsPostBack)
			{
				CheckCustomerStatus();
				FillControls();
				
				//Commented on 3 Nov 2008
				/*DataSet lDsLOB = objQuickQuote.GetLineOfBusinesses();
				cmbQQ_TYPE.DataSource = lDsLOB;
				cmbQQ_TYPE.DataTextField = "LOB_DESC";
				cmbQQ_TYPE.DataValueField = "LOB_CODE";
				cmbQQ_TYPE.DataBind();
				cmbQQ_TYPE.Items.Insert(0,"");

				cmbQQ_State.Items.Add(new ListItem("Indiana","INDIANA"));
				cmbQQ_State.Items.Add(new ListItem("Michigan","MICHIGAN"));
				cmbQQ_State.Items.Add(new ListItem("Wisconsin","WISCONSIN"));*/

				//cmbQQ_TYPE.Items.Add(new ListItem("Dwelling Fire","DFIRE"));
				//cmbQQ_TYPE.Items.Add(new ListItem("Homeowners","HOME"));
				//cmbQQ_TYPE.Items.Add(new ListItem("Motorcycle","CYCL"));
				//cmbQQ_TYPE.Items.Add(new ListItem("Private Passenger Automobile","AUTOP"));
				//cmbQQ_TYPE.Items.Add(new ListItem("Watercraft (Small Boat)","BOAT"));
					
				try
				{
					strQQ_Id = "";
					if(Request.QueryString["QQ_ID"]!=null)
						strQQ_Id  = Request.QueryString["QQ_ID"].ToString().Trim();
				}
				catch(Exception ex)
				{
					strQQ_Id = "-1";
				}

				try
				{
					if(Request.QueryString["CalledFromMenu"]==null)
						hidCalledFromMenu.Value = "N";
					else
						hidCalledFromMenu.Value  = Request.QueryString["CalledFromMenu"].ToString().Trim();
					

				}
				catch(Exception ex)
				{
					hidCalledFromMenu.Value = "N";
				}

				if (strQQ_Id=="" || strQQ_Id=="0" || strQQ_Id=="-1")
				{
					strQQ_Id = "-1";
					btnDelete.Visible = false;
					btnAddToApplication.Visible = false;
					btnQuickQuote.Visible  = false;
					btnActivateDeactivate.Visible = false;

					//ADDED BY PRAVEEN KUMAR(27-02-2009):ITRACK 5184
					DataSet ds = objQuickQuote.GetClientStateInfo(strCustomerId);
                    string stateID = "";// ds.Tables[0].Rows[0][3].ToString();

					//hidState.Value = objQuickQuote.GetClientStateInfo(strCustomerId).ToString();
                    hidState.Value = stateID;// ds.Tables[0].Rows[0][0].ToString().Trim();
					//ListItem lstItem  = cmbQQ_State.Items.FindByValue(hidState.Value.ToString().Trim().ToUpper());
					ListItem lstItem  = cmbQQ_State.Items.FindByValue(stateID);
					if(lstItem !=null )
					{
						//ADDED BY PRAVEEN KUMAR(27-02-2009):ITRACK 5184
						//TO SET THE STATE DROPDOWN AS THE STATE OF THE CUSTOMER BY DEFAULT
						//cmbQQ_State.SelectedIndex = -1;
						
						cmbQQ_State.SelectedValue = lstItem.Value;
						lstItem.Selected=true;
						ClsGeneralInformation objGenInfo=new ClsGeneralInformation();
						DataSet dsLOB=new DataSet();

						if(stateID!="-1" && stateID != "0")
						{	
							dsLOB=objGenInfo.GetLOBBYSTATEID(int.Parse(stateID));
							cmbQQ_TYPE.DataSource=dsLOB;
							cmbQQ_TYPE.DataTextField="LOB_DESC";
							cmbQQ_TYPE.DataValueField="LOB_CODE"; //"LOB_ID"; 
							cmbQQ_TYPE.DataBind();
							//cmbQQ_TYPE.Items.Insert(0,new ListItem("","0"));
							cmbQQ_TYPE.Items.Insert(0,"");
				
					
						}
						//END PRAVEEN KUMAR
					}
					//txtQQ_NUMBER.Text = "Quote-" + System.DateTime.Now.Month.ToString() + "/" + System.DateTime.Now.Day.ToString() + "/" + System.DateTime.Now.Year.ToString();
					lblQuickQuoteNumber.Text = "To Be Generated..";
				}
				else
				{
					DataSet DS = objQuickQuote.GetQuickQuoteInfo(strCustomerId,strQQ_Id);
					//txtQQ_NUMBER.Text  = DS.Tables[0].Rows[0]["QQ_NUMBER"].ToString().Trim();
					lblQuickQuoteNumber.Text = DS.Tables[0].Rows[0]["QQ_NUMBER"].ToString().Trim();
					cmbQQ_TYPE.Enabled = false;
					cmbQQ_State.Enabled = false;

					
//					ListItem lstItem  = cmbQQ_TYPE.Items.FindByValue(DS.Tables[0].Rows[0]["QQ_TYPE"].ToString().Trim());
//					if(lstItem !=null )
//					{
//						cmbQQ_TYPE.SelectedIndex =-1;
//						lstItem.Selected=true;
//					}

					//Get
					if(DS.Tables[0].Rows[0]["QQ_STATE"]!=null && DS.Tables[0].Rows[0]["QQ_STATE"].ToString()!="")
					{
						DataSet dstemp = objQuickQuote.GetStateIdQQ(DS.Tables[0].Rows[0]["QQ_STATE"].ToString().Trim());
                        string stateId = dstemp.Tables[0].Rows[0]["STATE_ID"].ToString();
						ListItem lstItem  = cmbQQ_State.Items.FindByValue(stateId.ToString());
						if(lstItem !=null)
						{
							cmbQQ_State.SelectedIndex =-1;
							lstItem.Selected=true;
							//Fill
							//Set Lob

							ClsGeneralInformation objGenInfo=new ClsGeneralInformation();
							DataSet dsLOB=new DataSet();

							if(stateId!="-1" && stateId != "0")
							{	
								dsLOB=objGenInfo.GetLOBBYSTATEID(int.Parse(stateId.ToString()));
								cmbQQ_TYPE.DataSource=dsLOB;
								cmbQQ_TYPE.DataTextField="LOB_DESC";
								cmbQQ_TYPE.DataValueField="LOB_CODE"; //"LOB_ID"; 
								cmbQQ_TYPE.DataBind();
								//cmbQQ_TYPE.Items.Insert(0,new ListItem("","0"));
								cmbQQ_TYPE.Items.Insert(0,"");
				
					
							}
							//Set LOB
							lstItem  = cmbQQ_TYPE.Items.FindByValue(DS.Tables[0].Rows[0]["QQ_TYPE"].ToString().Trim());
							if(lstItem !=null )
							{
								cmbQQ_TYPE.SelectedIndex =-1;
								lstItem.Selected=true;
							}
						}
					}		
			
					



					
					if (DS.Tables[0].Rows[0]["QQ_XML"].ToString().Trim() == "")
						btnAddToApplication.Visible = false;

					btnSave.Visible = false;

					if (DS.Tables[0].Rows[0]["QQ_APP_NUMBER"].ToString().Trim() != "")
					{
						btnAddToApplication.Visible = false;

						//IF APP NUMBER IS THERE DO NO ALLOW DELETE AND ACTIVATE/DEACTIVATE
						btnDelete.Visible = false;
						btnActivateDeactivate.Visible = false;
					}

					if (DS.Tables[0].Rows[0]["IS_ACTIVE"].ToString().Trim() == "Y")
					{
						btnActivateDeactivate.Text = "Deactivate";
					}
					else
					{
                        btnActivateDeactivate.Text = "Activate";
						//Can not Make Application from Deactivated QQ
						btnAddToApplication.Visible = false;
					}
				}
				hidQuoteId.Value = strQQ_Id.ToString();
			}

			SetValidators();
		}

		#region Check Customer Status (Active/InActive) / DisableDeleteButton
		private void CheckCustomerStatus()
		{
			string isActive = "";
			Cms.BusinessLayer.BlClient.ClsApplicantInsued.CheckCustomerIsActive(int.Parse(hidCustomerId.Value.ToString()),out isActive);
			if(isActive == "Y")
			{
				tbBody.Visible=true;
			}
			else
			{
				//SHOW MESSAGE AND set the flag for exit								
				lblMessage.Text= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"937");	
				lblMessage.Visible=true;
				tbBody.Visible=false;

			}
			
			/*DataSet dsCustomer = Cms.BusinessLayer.BlClient.ClsCustomer.GetCustomerDetails(int.Parse(hidCustomerId.Value.ToString()));
			if (dsCustomer!=null && dsCustomer.Tables[0].Rows.Count>0)
			{
				if (dsCustomer.Tables[0].Rows[0]["IS_CUSTOMER_ACTIVE"].ToString().Trim()=="" || dsCustomer.Tables[0].Rows[0]["IS_CUSTOMER_ACTIVE"].ToString().Trim()=="Y")
				{
					tbBody.Visible=true;
				}//END OF IF PART OF CUSTOMER ACTIVE CHECK		
				else
				{
					//SHOW MESSAGE AND set the flag for exit								
					lblMessage.Text= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"937");	
					lblMessage.Visible=true;
					tbBody.Visible=false;
				}//END OF ELSE PART OF CUSTOMER ACTIVE CHECK		
			}//END OF CUSTOMER  NULL CHECK*/
		}
		private void DisableDeleteButton()
		{
			//Diable Delete and Activate / Deactivate
			string qq_ID = "";
			string appNumber = "";
			if(Request.QueryString["QQ_ID"]!=null && Request.QueryString["QQ_ID"].ToString()!="")
			{
				qq_ID = Request.QueryString["QQ_ID"].ToString().Trim();			
				appNumber = objQuickQuote.CheckAppNumberQQ(hidCustomerId.Value,qq_ID);
			}
			if(appNumber!="")
			{
				btnDelete.Visible = false;
				btnActivateDeactivate.Visible = false;
			}
		}
		#endregion

		/// <summary>
		/// Added on 3 Nov 2008
		/// </summary>
		private void FillControls()
		{
            cmbQQ_TYPE.Items.Add("");
            cmbQQ_TYPE.Items.Add("Motor");
            cmbQQ_TYPE.Items.Add("Fire");
            cmbQQ_TYPE.Items.Add("Marine Cargo");
            						
			//LOBs
			/*DataTable dtLOBs = Cms.CmsWeb.ClsFetcher.LOBs;
			cmbQQ_TYPE.DataSource			= dtLOBs;
			cmbQQ_TYPE.DataTextField		= "LOB_DESC";
			cmbQQ_TYPE.DataValueField		= "LOB_CODE";
			cmbQQ_TYPE.DataBind();
			cmbQQ_TYPE.Items.Insert(0,new ListItem("","0"));*/
			//state
			DataTable dtState = Cms.CmsWeb.ClsFetcher.ActiveState ;
			cmbQQ_State.DataSource			= dtState;
			cmbQQ_State.DataTextField		= "STATE_NAME";
			cmbQQ_State.DataValueField		= "STATE_ID";
			cmbQQ_State.DataBind();
			cmbQQ_State.Items.Insert(0,new ListItem("","0"));
			cmbQQ_State.SelectedIndex=0;
		}
		private void cmbQQ_State_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				int stateID;
				ClsGeneralInformation objGenInfo=new ClsGeneralInformation();
				DataSet dsLOB=new DataSet();
				stateID=cmbQQ_State.SelectedItem==null?-1:int.Parse(cmbQQ_State.SelectedItem.Value);
				if(stateID!=-1 && stateID != 0)
				{					
					dsLOB=objGenInfo.GetLOBBYSTATEID(stateID);
					cmbQQ_TYPE.DataSource=dsLOB;
					cmbQQ_TYPE.DataTextField="LOB_DESC";
					cmbQQ_TYPE.DataValueField="LOB_CODE"; //"LOB_ID"; 
					cmbQQ_TYPE.DataBind();
					cmbQQ_TYPE.Items.Insert(0,new ListItem("","0"));
				
					
				}
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		}
		

		private void SetValidators()
		{
			rfvQQ_TYPE.ErrorMessage=ClsMessages.GetMessage(base.ScreenId,"934");
			rfvQQ_State.ErrorMessage = ClsMessages.GetMessage(base.ScreenId,"788");
		}
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
			this.cmbQQ_State.SelectedIndexChanged += new System.EventHandler(this.cmbQQ_State_SelectedIndexChanged);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnAddToApplication.Click += new System.EventHandler(this.btnAddToApplication_Click);
			//this.btnQuickQuote.Click += new System.EventHandler(this.btnQuickQuote_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);


		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			//string strQuoteId = objQuickQuote.SaveQuickQuoteInfo(hidCustomerId.Value,hidQuoteId.Value,txtQQ_NUMBER.Text.ToString(),cmbQQ_TYPE.SelectedItem.Value.ToString()).ToString();
			//if(cmbQQ_State.SelectedItem.Text!="")
			//{
				if(cmbQQ_TYPE.SelectedItem.Text!="")
				{
                    if (cmbQQ_TYPE.SelectedItem.Text == "Motor")
                        cmbQQ_TYPE.SelectedItem.Value = "MTOR";
					string strQuoteId = objQuickQuote.SaveQuickQuoteInfo(hidCustomerId.Value,hidQuoteId.Value,cmbQQ_TYPE.SelectedItem.Value.ToString(),cmbQQ_State.SelectedItem.Text.Trim().ToString().ToUpper()).ToString();
			
					if (strQuoteId.Trim() == "-2")
					{
						lblMessage.Text = "Duplicate Quote Number Entered";
					}
					else
					{
						DataSet DS = objQuickQuote.GetQuickQuoteInfo(strCustomerId,strQuoteId);
						lblQuickQuoteNumber.Text = DS.Tables[0].Rows[0]["QQ_NUMBER"].ToString().Trim();
						btnSave.Visible = false;

						hidQuoteId.Value = strQuoteId.Trim();
						lblMessage.Text = "Information Has Been Saved Successfully";
						UpdateGrid = true;
						primaryKeyValues = hidQuoteId.Value  + "^" + cmbQQ_TYPE.SelectedItem.Value.ToString() + "^" + hidCustomerId.Value;  
						cmbQQ_TYPE.Enabled = false;
						cmbQQ_State.Enabled = false;
						btnDelete.Visible = true;
						//btnAddToApplication.Visible = true;
                        btnQuickQuote.Attributes.Add("onclick", "javascript:return GoToPersonalDetail();");
						btnQuickQuote.Visible  = true;
						btnActivateDeactivate.Visible = true;
				
					}
				}
				else
				{
					 lblMessage.Text = "Please select Lob.";
				}
			//}
			//else 
			//{
            //    lblMessage.Text = "Please select State.";
			//}
			lblMessage.Visible = true;
		}

		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			DataSet DS = objQuickQuote.GetQuickQuoteInfo(hidCustomerId.Value,hidQuoteId.Value);
			string qqAppNumber  = DS.Tables[0].Rows[0]["QQ_APP_NUMBER"].ToString().Trim();
			string qqXml  = DS.Tables[0].Rows[0]["QQ_XML"].ToString().Trim();

            
			string strStatus = btnActivateDeactivate.Text.ToString().Trim();
			objQuickQuote.ActivateDeactivateQuickQuote(hidCustomerId.Value,hidQuoteId.Value,strStatus);
			lblMessage.Visible = true;
			if (btnActivateDeactivate.Text == "Deactivate")
			{
				lblMessage.Text = "Information deactivated successfully";
				btnActivateDeactivate.Text = "Activate";
				//Can not Make a Deactivate QQ
				btnAddToApplication.Visible = false;
			}
			else
			{
				lblMessage.Text = "Information activated successfully";
				btnActivateDeactivate.Text = "Deactivate";

				if(qqXml!="")
					btnAddToApplication.Visible = true;
				if(qqAppNumber!="")
					btnAddToApplication.Visible = true;
			}
			UpdateGrid = true;
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			objQuickQuote.DeleteQuickQuote(hidCustomerId.Value,hidQuoteId.Value);
			lblMessage.Visible = true;
			lblMessage.Text = "Information Has Been Deleted Successfully";
			DeleteFlag = false;
			UpdateGrid = true;
			hidChkDelete.Value="1";
		}

        //private void btnQuickQuote_Click(object sender, System.EventArgs e)
        //{
        //    //Response.Write("<script>parent.location.href = 'QuickQuoteLoad.aspx?ClientId="+ hidCustomerId.Value.ToString() +"&State="+ hidState.Value.ToString() + "&UserId=" + GetUserId().ToString() + "&QuoteId=" + hidQuoteId.Value.ToString() + "&Type=" + cmbQQ_TYPE.SelectedItem.Value + "'; </script>");
        //    //Response.Write("<script>top.botframe.location.href = 'QuickQuoteLoad.aspx?ClientId="+ hidCustomerId.Value.ToString() +"&State="+ hidState.Value.ToString() + "&UserId=" + GetUserId().ToString() + "&QuoteId=" + hidQuoteId.Value.ToString() + "&Type=" + cmbQQ_TYPE.SelectedItem.Value + "'; </script>");
        //    //Response.Write("<script>top.botframe.location.href = 'QuickQuoteLoad.aspx?ClientId="+ hidCustomerId.Value.ToString() +"&State="+ hidState.Value.ToString() + "&UserId=" + GetUserId().ToString() + "&QuoteId=" + hidQuoteId.Value.ToString() + "&Type=" + cmbQQ_TYPE.SelectedItem.Value + "&QuickQuoteNumber=" + txtQQ_NUMBER.Text.ToString().Trim() + "&QuickQuoteLob=" + cmbQQ_TYPE.SelectedItem.Text.ToString().Trim() + "'; </script>");
        //    //Response.Write("<script>top.botframe.location.href = 'QuickQuoteLoad.aspx?Customer_Id="+ hidCustomerId.Value.ToString() +"&state_name1="+ hidState.Value.ToString() + "&UserId=" + GetUserId().ToString() + "&QQ_ID=" + hidQuoteId.Value.ToString() + "&QQ_TYPE=" + cmbQQ_TYPE.SelectedItem.Value + "&QQ_NUMBER=" + lblQuickQuoteNumber.Text.ToString().Trim() + "&LOB_DESC=" + cmbQQ_TYPE.SelectedItem.Text.ToString().Trim() + "'; </script>");
        //    Response.Write("<script>top.botframe.location.href = 'QuickQuoteLoad.aspx?Customer_Id="+ hidCustomerId.Value.ToString() +"&state_name="+ cmbQQ_State.SelectedItem.Text.Trim().ToUpper().ToString() + "&UserId=" + GetUserId().ToString() + "&QQ_ID=" + hidQuoteId.Value.ToString() + "&QQ_TYPE=" + cmbQQ_TYPE.SelectedItem.Value + "&QQ_NUMBER=" + lblQuickQuoteNumber.Text.ToString().Trim() + "&LOB_DESC=" + cmbQQ_TYPE.SelectedItem.Text.ToString().Trim() + "'; </script>");
        //    Response.End();
        //    //Server.Transfer("QuickQuoteInfo.aspx?ClientId="+ hidCustomerId.Value.ToString() +"&State="+ hidState.Value.ToString() + "&UserId=" + GetUserId().ToString() + "&QuoteId=" + hidQuoteId.Value.ToString() + "&Type=" + cmbQQ_TYPE.SelectedItem.Value);
        //}

		private void btnAddToApplication_Click(object sender, System.EventArgs e)
		{
			string AcordXml="";
			string ApplicationNumber="";
			string AppId="";
			string VersionId="";
//			if (cmbQQ_TYPE.SelectedItem.Value.ToString()=="AUTOP")
//			{
				lblMessage.Visible = true;
				//AcordXml = new ClsAuto().PrepareAutoAcordXml(hidCustomerId.Value.ToString(),hidQuoteId.Value.ToString(),Server.MapPath(Request.ApplicationPath + "/cmsweb/support/ACORD_AUTOP_INTERFACING_XML.xml").ToString(),hidState.Value.ToString(),txtQQ_NUMBER.Text.ToString().Trim());
				//AcordXml = new ClsAuto().PrepareAutoAcordXml(hidCustomerId.Value.ToString(),hidQuoteId.Value.ToString(),Server.MapPath(Request.ApplicationPath + "/cmsweb/support/ACORD_AUTOP_INTERFACING_XML.xml").ToString(),hidState.Value.ToString(),lblQuickQuoteNumber.Text.ToString().Trim());
            //AcordXml = new ClsAuto().PrepareAutoAcordXml(hidCustomerId.Value.ToString(), hidQuoteId.Value.ToString(), Server.MapPath(Request.ApplicationPath + "/cmsweb/support/ACORD_AUTOP_INTERFACING_XML.xml").ToString(), cmbQQ_State.SelectedItem.Text.Trim().ToUpper().ToString(), lblQuickQuoteNumber.Text.ToString().Trim());

            //Cms.CmsWeb.AcordXmlParser objParser = new AcordXmlParser();
            //objParser.LoadXmlString(AcordXml);

            //AutoP obj = null;

            //try
            //{
            //    obj = objParser.Parse();
            //    obj.UserID = GetUserId().ToString();
            //}
            //catch (Exception ex)
            //{
            //    lblMessage.Text = ex.Message.ToString();
            //    return;
            //}

            ClsGeneralInfo objAppInfo = null;
            //try
            //{
            //    if (obj != null)
            //    {

            //        objAppInfo = obj.Import();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    lblMessage.Text = ex.Message.ToString();
            //    return;
            //}

				objQuickQuote.UpdateQuickQuoteAppNumber(hidCustomerId.Value.ToString(),hidQuoteId.Value.ToString(),objAppInfo.APP_NUMBER.ToString().Trim());
				ApplicationNumber=objAppInfo.APP_NUMBER.ToString().Trim();
				AppId=objAppInfo.APP_ID.ToString().Trim();
				VersionId=objAppInfo.APP_VERSION_ID.ToString().Trim();
				lblMessage.Text = "Application Created Successfully.";
				DeleteFlag = false;
				UpdateGrid = true;
            //if (cmbQQ_TYPE.SelectedItem.Value.ToString()=="AUTOP")
            //{
            //    lblMessage.Visible = true;
            //    //AcordXml = new ClsAuto().PrepareAutoAcordXml(hidCustomerId.Value.ToString(),hidQuoteId.Value.ToString(),Server.MapPath(Request.ApplicationPath + "/cmsweb/support/ACORD_AUTOP_INTERFACING_XML.xml").ToString(),hidState.Value.ToString(),txtQQ_NUMBER.Text.ToString().Trim());
            //    //AcordXml = new ClsAuto().PrepareAutoAcordXml(hidCustomerId.Value.ToString(),hidQuoteId.Value.ToString(),Server.MapPath(Request.ApplicationPath + "/cmsweb/support/ACORD_AUTOP_INTERFACING_XML.xml").ToString(),hidState.Value.ToString(),lblQuickQuoteNumber.Text.ToString().Trim());
            //    AcordXml = new ClsAuto().PrepareAutoAcordXml(hidCustomerId.Value.ToString(),hidQuoteId.Value.ToString(),Server.MapPath(Request.ApplicationPath + "/cmsweb/support/ACORD_AUTOP_INTERFACING_XML.xml").ToString(),cmbQQ_State.SelectedItem.Text.Trim().ToUpper().ToString(),lblQuickQuoteNumber.Text.ToString().Trim());

            //    Cms.CmsWeb.AcordXmlParser objParser = new AcordXmlParser();
            //    objParser.LoadXmlString(AcordXml);
				
            //    AutoP obj = null;
				
            //    try
            //    {
            //        obj = objParser.Parse();
            //        obj.UserID = GetUserId().ToString();
            //    }
            //    catch(Exception ex)
            //    {
            //        lblMessage.Text = ex.Message.ToString();
            //        return;
            //    }
			
            //    ClsGeneralInfo objAppInfo = null;
            //    try
            //    {
            //        if ( obj!= null )
            //        {
					
            //            objAppInfo = obj.Import();
            //        }
            //    }
            //    catch(Exception ex)
            //    {
            //        lblMessage.Text = ex.Message.ToString();
            //        return;
            //    }

            //    objQuickQuote.UpdateQuickQuoteAppNumber(hidCustomerId.Value.ToString(),hidQuoteId.Value.ToString(),objAppInfo.APP_NUMBER.ToString().Trim());
            //    ApplicationNumber=objAppInfo.APP_NUMBER.ToString().Trim();
            //    AppId=objAppInfo.APP_ID.ToString().Trim();
            //    VersionId=objAppInfo.APP_VERSION_ID.ToString().Trim();
            //    lblMessage.Text = "Application Created Successfully.";
            //    DeleteFlag = false;
            //    UpdateGrid = true;
            //}
            //else if (cmbQQ_TYPE.SelectedItem.Value.ToString()=="CYCL")
            //{
            //    lblMessage.Visible = true;
            //    //AcordXml = new ClsAuto().PrepareCyclAcordXml(hidCustomerId.Value.ToString(),hidQuoteId.Value.ToString(),Server.MapPath(Request.ApplicationPath + "/cmsweb/support/ACORD_MOTORCYCLE_INTERFACING_XML.xml").ToString(),hidState.Value.ToString(),txtQQ_NUMBER.Text.ToString().Trim());
            //    //AcordXml = new ClsAuto().PrepareCyclAcordXml(hidCustomerId.Value.ToString(),hidQuoteId.Value.ToString(),Server.MapPath(Request.ApplicationPath + "/cmsweb/support/ACORD_MOTORCYCLE_INTERFACING_XML.xml").ToString(),hidState.Value.ToString(),lblQuickQuoteNumber.Text.ToString().Trim());
            //    ClsAuto objAuto = new ClsAuto();
            //    AcordXml = objAuto.PrepareCyclAcordXml(hidCustomerId.Value.ToString(),hidQuoteId.Value.ToString(),Server.MapPath(Request.ApplicationPath + "/cmsweb/support/ACORD_MOTORCYCLE_INTERFACING_XML.xml").ToString(),cmbQQ_State.SelectedItem.Text.Trim().ToUpper().ToString(),lblQuickQuoteNumber.Text.ToString().Trim());
				
				
            //    Cms.CmsWeb.AcordXmlParser objParser = new AcordXmlParser();
            //    objParser.LoadXmlString(AcordXml);
				
            //    AutoP obj = null;
				
            //    try
            //    {
            //        obj = objParser.Parse();
            //        obj.UserID = GetUserId().ToString();
            //    }
            //    catch(Exception ex)
            //    {
            //        lblMessage.Text = ex.Message.ToString();
            //        return;
            //    }
			
            //    ClsGeneralInfo objAppInfo = null;
            //    try
            //    {
            //        if ( obj!= null )
            //        {
            //            objAppInfo = obj.Import();
            //        }
            //    }
            //    catch(Exception ex)
            //    {
            //        lblMessage.Text = ex.Message.ToString();
            //        return;
            //    }

            //    objQuickQuote.UpdateQuickQuoteAppNumber(hidCustomerId.Value.ToString(),hidQuoteId.Value.ToString(),objAppInfo.APP_NUMBER.ToString().Trim());
            //    ApplicationNumber=objAppInfo.APP_NUMBER.ToString().Trim();
            //    AppId=objAppInfo.APP_ID.ToString().Trim();
            //    VersionId=objAppInfo.APP_VERSION_ID.ToString().Trim();
            //    lblMessage.Text = "Application Created Successfully.";
            //    DeleteFlag = false;
            //    UpdateGrid = true;
            //}	
            //else if (cmbQQ_TYPE.SelectedItem.Value.ToString()=="BOAT")
            //{
            //    lblMessage.Visible = true;

            //    ClsWatercraft objWater = new ClsWatercraft();
				
            //    AcordXml = objWater.PrepareBoatAcordXml(hidCustomerId.Value.ToString(),hidQuoteId.Value.ToString(),Server.MapPath(Request.ApplicationPath + "/cmsweb/support/ACORD_WATERCRAFT_INTERFACING_XML.xml").ToString(),cmbQQ_State.SelectedItem.Text.Trim().ToUpper().ToString(),lblQuickQuoteNumber.Text.ToString().Trim());
				
            //    //Load ACORD XML into parser
            //    Cms.CmsWeb.ClsWatercraftParser objParser = new ClsWatercraftParser();	
            //    objParser.LoadXmlString(AcordXml);
				
            //    //AutoP obj = null;
            //    //ClsAcordWatercraft objBoat = null;
            //    AcordBase objBase = null;

            //    //Import the Watercraft data
            //    try
            //    {
            //        objBase = objParser.Parse();
            //    }
            //    catch(Exception ex)
            //    {
            //        lblMessage.Text = ex.Message.ToString();
            //        return;
            //    }
			
            //    ClsGeneralInfo objAppInfo = null;
            //    try
            //    {
            //        if ( objBase!= null )
            //        {
            //            objBase.UserID = GetUserId().ToString();
            //            objAppInfo = objBase.Import();
            //        }
            //    }
            //    catch(Exception ex)
            //    {
            //        lblMessage.Text = ex.Message.ToString();
            //        return;
            //    }

            //    objQuickQuote.UpdateQuickQuoteAppNumber(hidCustomerId.Value.ToString(),hidQuoteId.Value.ToString(),objAppInfo.APP_NUMBER.ToString().Trim());
            //    ApplicationNumber=objAppInfo.APP_NUMBER.ToString().Trim();
            //    AppId=objAppInfo.APP_ID.ToString().Trim();
            //    VersionId=objAppInfo.APP_VERSION_ID.ToString().Trim();
            //    lblMessage.Text = "Application Created Successfully.";
            //    DeleteFlag = false;
            //    UpdateGrid = true;
				
            //}	
            //else if (cmbQQ_TYPE.SelectedItem.Value.ToString()=="HOME")
            //{
            //    lblMessage.Visible = true;
            //    ClsHome objHome = new ClsHome();
				
            //    //AcordXml = objHome.PrepareHomeAcordXml(hidCustomerId.Value.ToString(),hidQuoteId.Value.ToString(),Server.MapPath(Request.ApplicationPath + "/cmsweb/support/ACORD_HOME_INTERFACING_XML.XML"),hidState.Value.ToString(),txtQQ_NUMBER.Text.ToString().Trim());
            //    //AcordXml = objHome.PrepareHomeAcordXml(hidCustomerId.Value.ToString(),hidQuoteId.Value.ToString(),Server.MapPath(Request.ApplicationPath + "/cmsweb/support/ACORD_HOME_INTERFACING_XML.XML"),hidState.Value.ToString(),lblQuickQuoteNumber.Text.ToString().Trim());
            //    AcordXml = objHome.PrepareHomeAcordXml(hidCustomerId.Value.ToString(),hidQuoteId.Value.ToString(),Server.MapPath(Request.ApplicationPath + "/cmsweb/support/ACORD_HOME_INTERFACING_XML.XML"),cmbQQ_State.SelectedItem.Text.Trim().ToUpper().ToString(),lblQuickQuoteNumber.Text.ToString().Trim());

            //    Cms.CmsWeb.HomeLOBParser objParser = new HomeLOBParser();
            //    objParser.LoadXmlString(AcordXml);
				
            //    AcordBase obj = null;
				
            //    try
            //    {
            //        obj = objParser.Parse();
					
            //    }
            //    catch(Exception ex)
            //    {
            //        lblMessage.Text = ex.Message.ToString();
            //        return;
            //    }
			
            //    ClsGeneralInfo objAppInfo = null;
            //    try
            //    {
            //        if ( obj!= null )
            //        {
            //            obj.UserID = GetUserId();                       						 
            //            objAppInfo = obj.Import();
            //        }
            //    }
            //    catch(Exception ex)
            //    {
            //        lblMessage.Text = ex.Message.ToString();
            //        return;
            //    }

            //    objQuickQuote.UpdateQuickQuoteAppNumber(hidCustomerId.Value.ToString(),hidQuoteId.Value.ToString(),objAppInfo.APP_NUMBER.ToString().Trim());
            //    ApplicationNumber=objAppInfo.APP_NUMBER.ToString().Trim();
            //    AppId=objAppInfo.APP_ID.ToString().Trim();
            //    VersionId=objAppInfo.APP_VERSION_ID.ToString().Trim();

            //    lblMessage.Text = "Application Created Successfully.";
            //    DeleteFlag = false;
            //    UpdateGrid = true;
            //}
            //else if (cmbQQ_TYPE.SelectedItem.Value.ToString()=="REDW")
            //{
            //    lblMessage.Visible = true;
            //    ClsHome objHome = new ClsHome();
				
            //    //AcordXml = objHome.PrepareHomeAcordXml(hidCustomerId.Value.ToString(),hidQuoteId.Value.ToString(),Server.MapPath(Request.ApplicationPath + "/cmsweb/support/ACORD_HOME_INTERFACING_XML.XML"),hidState.Value.ToString(),txtQQ_NUMBER.Text.ToString().Trim());
            //    //AcordXml = objHome.PrepareHomeAcordXml(hidCustomerId.Value.ToString(),hidQuoteId.Value.ToString(),Server.MapPath(Request.ApplicationPath + "/cmsweb/support/ACORD_HOME_INTERFACING_XML.XML"),hidState.Value.ToString(),lblQuickQuoteNumber.Text.ToString().Trim());
            //    AcordXml = objHome.PrepareRentalHomeAcordXml(hidCustomerId.Value.ToString(),hidQuoteId.Value.ToString(),Server.MapPath(Request.ApplicationPath + "/cmsweb/support/ACORD_RENTAL_INTERFACING_XML.XML"),cmbQQ_State.SelectedItem.Text.Trim().ToUpper().ToString(),lblQuickQuoteNumber.Text.ToString().Trim());

            //    Cms.CmsWeb.HomeLOBParser objParser = new HomeLOBParser();
            //    objParser.LoadXmlString(AcordXml);
				
            //    AcordBase obj = null;
				
            //    try
            //    {
            //        obj = objParser.Parse();
					
            //    }
            //    catch(Exception ex)
            //    {
            //        lblMessage.Text = ex.Message.ToString();
            //        return;
            //    }
			
            //    ClsGeneralInfo objAppInfo = null;
            //    try
            //    {
            //        if ( obj!= null )
            //        {
            //            obj.UserID = GetUserId(); 						 
            //            objAppInfo = obj.Import();
            //        }
            //    }
            //    catch(Exception ex)
            //    {
            //        lblMessage.Text = ex.Message.ToString();
            //        return;
            //    }

            //    objQuickQuote.UpdateQuickQuoteAppNumber(hidCustomerId.Value.ToString(),hidQuoteId.Value.ToString(),objAppInfo.APP_NUMBER.ToString().Trim());
            //    ApplicationNumber=objAppInfo.APP_NUMBER.ToString().Trim();
            //    AppId=objAppInfo.APP_ID.ToString().Trim();
            //    VersionId=objAppInfo.APP_VERSION_ID.ToString().Trim();

            //    lblMessage.Text = "Application Created Successfully.";
            //    DeleteFlag = false;
            //    UpdateGrid = true;
            //}

			if (AppId != "")
			{
				Response.Write("<script>alert('Application Created Successfully. Your Application Number is " + ApplicationNumber + ".');</script>");
				Response.Write("<script>top.botframe.location.href = '/cms/Application/aspx/ApplicationTab.aspx?customer_id=" + hidCustomerId.Value.ToString() + "&app_id=" + AppId + "&app_version_id=" +  VersionId + "'; </script>");
				Response.End();
			}
		}
	}
}
