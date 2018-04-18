#region Namespaces
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
using Cms.BusinessLayer.BlApplication;
using Cms.CmsWeb;
using Cms.CustomException;
using Cms.CmsWeb.WebControls;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.Controls;
using System.Xml;
#endregion

namespace Policies.Aspx
{
	/// <summary>
	/// Summary description for PolCopyUmbSchRecords.
	/// </summary>
	public class PolCopyUmbSchRecords : Cms.Policies.policiesbase
	{
		#region Declarations
		protected System.Web.UI.WebControls.Label lblHeader;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFor;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTitle;
		protected Cms.CmsWeb.Controls.CmsButton btnSubmit;
		protected Cms.CmsWeb.Controls.CmsButton btnClose;		
		protected int gIntSaved=0;		
		protected System.Web.UI.HtmlControls.HtmlForm Form1;		
		
		protected System.Web.UI.WebControls.DataGrid dgrDriverRecords;
		protected System.Web.UI.WebControls.DataGrid dgrBoatRecords;
		protected System.Web.UI.WebControls.DataGrid dgrVehicleRecords;
		protected System.Web.UI.WebControls.DataGrid dgrRecVehRecords;
		protected System.Web.UI.WebControls.DataGrid dgrLocationRecords;
		
		public string strCalledFrom="";
		#endregion

		public bool anyRecordSelected=false;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			#region ScreenID

			if(Request.QueryString["calledfrom"]!=null )
				strCalledFrom=Request.QueryString["calledfrom"].ToString(); 

			switch(strCalledFrom)
			{
				case "PPA" :
					base.ScreenId	=	"";
					break;
				case "MOT" :
					base.ScreenId	=	"";
					break;
				case "UMB" :
					base.ScreenId	=	"";
					break;
				case "WAT" :
					base.ScreenId	=	"";
					break;
				case "HOME" :
					base.ScreenId	=	"";
					break;
				case "RENT" :
					base.ScreenId	=	"";
					break;
				default :
					base.ScreenId	=	"";
					break;
			}
			#endregion

			#region Button Permissions
			btnSubmit.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSubmit.PermissionString = gstrSecurityXML;

			btnClose.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnClose.PermissionString = gstrSecurityXML;
			#endregion


			if (!IsPostBack)
			{				
				GetQueryStringValues();
				if (hidCalledFrom.Value == "UMB" && (hidCalledFor.Value == ClsUmbSchRecords.CALLED_FROM_LOCATIONS || hidCalledFor.Value == ClsUmbSchRecords.CALLED_FROM_REC_VEH || hidCalledFor.Value == ClsUmbSchRecords.CALLED_FROM_VEHICLES || hidCalledFor.Value == ClsUmbSchRecords.CALLED_FROM_BOAT))
				{
					btnSubmit.Attributes.Add("style","display:none");
					btnClose.Visible=true;					
					lblHeader.Text = "List of Schedule of Underlying Records";
				}
				btnClose.Attributes.Add("onClick","javascript: window.close();");
				BindGrid();				
			}
		}
		private void BindGrid()
		{
			try
			{	
				DataTable dtDriver = new DataTable();
//				lblHeader.Text = "Copy Schedule of Underlying Records";
				switch(hidCalledFor.Value)
				{
					case ClsUmbSchRecords.CALLED_FROM_DRIVER:				
						dtDriver = ClsUmbSchRecords.FetchPolDrivers(int.Parse(hidCustomerID.Value),int.Parse(hidPolicyID.Value),int.Parse(hidPolicyVersionID.Value));
						if(dtDriver.Rows.Count>0)
						{
							btnSubmit.Enabled=true;
						}
						else
						{	
							btnSubmit.Enabled=false;
						}					
						dgrDriverRecords.DataSource=dtDriver.DefaultView;
						dgrDriverRecords.DataBind();
						dgrDriverRecords.Visible = true;						
						break;				
					case ClsUmbSchRecords.CALLED_FROM_LOCATIONS:				
						dtDriver = ClsUmbSchRecords.FetchPolLocations(int.Parse(hidCustomerID.Value),int.Parse(hidPolicyID.Value),int.Parse(hidPolicyVersionID.Value));
//						if(dtDriver.Rows.Count>0)
//						{
//							btnSubmit.Enabled=true;
//						}
//						else
//						{	
//							btnSubmit.Enabled=false;
//						}					
						dgrLocationRecords.DataSource=dtDriver.DefaultView;
						dgrLocationRecords.DataBind();
						dgrLocationRecords.Visible = true;					
						break;
					case ClsUmbSchRecords.CALLED_FROM_REC_VEH:				
					
						dtDriver = ClsUmbSchRecords.FetchPolRecVehicles(int.Parse(hidCustomerID.Value),int.Parse(hidPolicyID.Value),int.Parse(hidPolicyVersionID.Value));
//						if(dtDriver.Rows.Count>0)
//						{
//							btnSubmit.Enabled=true;
//						}
//						else
//						{	
//							btnSubmit.Enabled=false;
//						}					
						dgrRecVehRecords.DataSource=dtDriver.DefaultView;
						dgrRecVehRecords.DataBind();
						dgrRecVehRecords.Visible = true;						
						break;
					case ClsUmbSchRecords.CALLED_FROM_VEHICLES:		
						dtDriver = ClsUmbSchRecords.FetchPolVehicles(int.Parse(hidCustomerID.Value),int.Parse(hidPolicyID.Value),int.Parse(hidPolicyVersionID.Value));
//						if(dtDriver.Rows.Count>0)
//						{
//							btnSubmit.Enabled=true;
//						}
//						else
//						{	
//							btnSubmit.Enabled=false;
//						}					
						dgrVehicleRecords.DataSource=dtDriver.DefaultView;
						dgrVehicleRecords.DataBind();
						dgrVehicleRecords.Visible = true;
						break;
					case ClsUmbSchRecords.CALLED_FROM_BOAT:	
//						lblHeader.Text = "List of Schedule of Underlying Records";
						dtDriver = ClsUmbSchRecords.FetchPolBoats(int.Parse(hidCustomerID.Value),int.Parse(hidPolicyID.Value),int.Parse(hidPolicyVersionID.Value));
						if(dtDriver.Rows.Count>0)
						{
							btnSubmit.Enabled=true;
						}
						else
						{	
							btnSubmit.Enabled=false;
						}					
						dgrBoatRecords.DataSource=dtDriver.DefaultView;
						dgrBoatRecords.DataBind();
						dgrBoatRecords.Visible = true;
						break;
					default:
						break;
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text=ex.Message;
				lblMessage.Visible=true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		}

		private void GetQueryStringValues()
		{
			if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
			{ 						
				hidCustomerID.Value=Request.QueryString["CUSTOMER_ID"].ToString(); 
			}				
			if (Request.QueryString["POLICY_ID"]!=null && Request.QueryString["POLICY_ID"].ToString() != "")
			{ 						
				hidPolicyID.Value=Request.QueryString["POLICY_ID"].ToString();
			}				
			if (Request.QueryString["POLICY_VERSION_ID"]!=null && Request.QueryString["POLICY_VERSION_ID"].ToString() != "")
			{ 					
				hidPolicyVersionID.Value=Request.QueryString["POLICY_VERSION_ID"].ToString();
			}		
			if (Request.QueryString["CALLEDFROM"]!=null && Request.QueryString["CALLEDFROM"].ToString() != "")
			{ 					
				hidCalledFrom.Value=Request.QueryString["CALLEDFROM"].ToString();
			}

			if (Request.QueryString["CALLEDFOR"]!=null && Request.QueryString["CALLEDFOR"].ToString() != "")
			{ 					
				hidCalledFor.Value=Request.QueryString["CALLEDFOR"].ToString();
			}
		}

		XmlDocument GetDataGridNodes(DataGrid dgRecords,XmlDocument xmlDoc)
		{
			
			CheckBox chkBox;
			foreach(DataGridItem dgi in dgRecords.Items)
			{
				chkBox=(CheckBox)dgi.FindControl("chkSelect");
				if (chkBox != null && chkBox.Checked)
				{
					anyRecordSelected = true;//atleast one record is chosen
					
					XmlElement parentNode = null;
					switch(hidCalledFor.Value)
					{
						case ClsUmbSchRecords.CALLED_FROM_DRIVER:
							parentNode  = xmlDoc.CreateElement("Driver");
							break;
						case ClsUmbSchRecords.CALLED_FROM_LOCATIONS:
							parentNode  = xmlDoc.CreateElement("Locations");
							break;
						case ClsUmbSchRecords.CALLED_FROM_REC_VEH:
							parentNode  = xmlDoc.CreateElement("Rec_Vehicles");
							break;
						case ClsUmbSchRecords.CALLED_FROM_VEHICLES:
							parentNode  = xmlDoc.CreateElement("Vehicles");
							break;
						case ClsUmbSchRecords.CALLED_FROM_BOAT:
							parentNode  = xmlDoc.CreateElement("Boat");
							break;
						default:
							parentNode = null;
							break;
					}					
									
					parentNode.SetAttribute("ID", dgi.ItemIndex.ToString());
					XmlNode xNode = xmlDoc.CreateElement("CREATED_BY");
					xNode.InnerText = GetUserId();
					parentNode.AppendChild(xNode);

					for (int i=1 ; i<dgi.Cells.Count ; i++)
					{
						xNode = xmlDoc.CreateElement(((BoundColumn)dgRecords.Columns[i]).DataField);						
						xNode.InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.DecodeXMLCharacters(dgi.Cells[i].Text.Trim());
						parentNode.AppendChild(xNode);									
					}
					xmlDoc.DocumentElement.PrependChild(parentNode);
				}
			}
			return xmlDoc;

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
			this.Load += new System.EventHandler(this.Page_Load);
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
		}
		#endregion

		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
			
			try
			{
				XmlDocument xmlDoc = new XmlDocument();			
				XmlElement rootNode = null;

				anyRecordSelected = false;
				
				switch(hidCalledFor.Value)
				{
					case ClsUmbSchRecords.CALLED_FROM_DRIVER:
						rootNode = xmlDoc.CreateElement(ClsUmbSchRecords.DRIVERS_PARENT_NODE);
						break;
					case ClsUmbSchRecords.CALLED_FROM_LOCATIONS:
						rootNode = xmlDoc.CreateElement(ClsUmbSchRecords.LOCATIONS_PARENT_NODE);
						break;
					case ClsUmbSchRecords.CALLED_FROM_REC_VEH:
						rootNode = xmlDoc.CreateElement(ClsUmbSchRecords.REC_VEH_PARENT_NODE);
						break;
					case ClsUmbSchRecords.CALLED_FROM_VEHICLES:
						rootNode = xmlDoc.CreateElement(ClsUmbSchRecords.VEHICLES_PARENT_NODE);
						break;
					case ClsUmbSchRecords.CALLED_FROM_BOAT:
						rootNode = xmlDoc.CreateElement(ClsUmbSchRecords.BOAT_PARENT_NODE);
						break;
					default:
						break;
				}

				rootNode.SetAttribute("CustomerID",hidCustomerID.Value);
				rootNode.SetAttribute("PolID",hidPolicyID.Value);
				rootNode.SetAttribute("PolVersionID",hidPolicyVersionID.Value);
				xmlDoc.AppendChild(rootNode);

				switch(hidCalledFor.Value)
				{
					case ClsUmbSchRecords.CALLED_FROM_DRIVER:
						xmlDoc = GetDataGridNodes(dgrDriverRecords,xmlDoc);
						break;
					case ClsUmbSchRecords.CALLED_FROM_LOCATIONS:
						xmlDoc = GetDataGridNodes(dgrLocationRecords,xmlDoc);			
						break;
					case ClsUmbSchRecords.CALLED_FROM_REC_VEH:
						xmlDoc = GetDataGridNodes(dgrRecVehRecords,xmlDoc);		
						break;
					case ClsUmbSchRecords.CALLED_FROM_VEHICLES:
						xmlDoc = GetDataGridNodes(dgrVehicleRecords,xmlDoc);		
						break;
					case ClsUmbSchRecords.CALLED_FROM_BOAT:
						xmlDoc = GetDataGridNodes(dgrBoatRecords,xmlDoc);		
						break;
					default:
						break;
				}
			
				//				foreach(DataGridItem dgi in dgrDriverRecords.Items)
				//				{
				//						chkBox=(CheckBox)dgi.FindControl("chkSelect");
				//						if (chkBox != null && chkBox.Checked)
				//						{
				//							blChecked=true;//var to chk that atleat one drv is selected
				//							XmlElement parentNode;
				//							switch(hidCalledFor.Value)
				//							{
				//								case "DRIVERS":
				//									parentNode  = xmlDoc.CreateElement("Driver");
				//									break;
				//								case "VEHICLES":
				//									parentNode  = xmlDoc.CreateElement("Vehicle");
				//									break;
				//								default:
				//									parentNode = null;
				//									break;
				//							}							
				//									
				//							parentNode.SetAttribute("ID", dgi.ItemIndex.ToString());
				//
				//							for (int i=1 ; i<dgi.Cells.Count ; i++)
				//							{
				//								//XmlElement NodeName  = xmlDoc.CreateElement(i.ToString() + dgrDriverRecords.Columns[i].HeaderText.Replace(" ","_"));
				//								XmlElement NodeName  = xmlDoc.CreateElement(((BoundColumn)dgrDriverRecords.Columns[i]).DataField);								
				//								XmlText    NodeText  = xmlDoc.CreateTextNode(dgi.Cells[i].Text);
				//								NodeName.AppendChild(NodeText);
				//								parentNode.AppendChild(NodeName);
				//							}							
				//							xmlDoc.DocumentElement.PrependChild(parentNode);
				//						}
				//				}
				
				int intRetVal = -1;								
				ClsUmbSchRecords objUmbSchRecords = new ClsUmbSchRecords();
				if(anyRecordSelected)
				{
					switch(hidCalledFor.Value)
					{
						case ClsUmbSchRecords.CALLED_FROM_DRIVER:
							intRetVal = objUmbSchRecords.SavePolUmbrellaDriversSOU(xmlDoc);
							//intRetVal = ClsUmbSchRecords.SavePolUmbrellaDriversSOU(xmlDoc);
							break;
						case ClsUmbSchRecords.CALLED_FROM_LOCATIONS:
							intRetVal = objUmbSchRecords.SaveUmbrellaPolLocationsSOU(xmlDoc);
							break;
						case ClsUmbSchRecords.CALLED_FROM_REC_VEH:
							intRetVal = objUmbSchRecords.SavePolUmbrellaPolRecVehiclesSOU(xmlDoc);
							break;
						case ClsUmbSchRecords.CALLED_FROM_VEHICLES:
							intRetVal = objUmbSchRecords.SavePolUmbrellaVehiclesSOU(xmlDoc);
							break;
						case ClsUmbSchRecords.CALLED_FROM_BOAT:
							intRetVal = objUmbSchRecords.SavePolUmbrellaBoatsSOU(xmlDoc);
							break;
						default:
							intRetVal = -1;
							break;
					}
				}
				if(intRetVal>0)
				{
					lblMessage.Text=ClsMessages.FetchGeneralMessage("859");
					lblMessage.Visible=true;
					btnSubmit.Visible=false;
					dgrDriverRecords.Visible=false;
					dgrLocationRecords.Visible = false;
					btnClose.Visible=true;
					hidFormSaved.Value = "1";
					string strScript="";
					strScript = @"<script language='javascript'>" + 
						"window.opener.RefreshWebgrid('');" +
						"window.close();" + 
						"</script>" 
						;
				
					if (!ClientScript.IsStartupScriptRegistered("Refresh"))
					{
                        ClientScript.RegisterStartupScript(this.GetType(),"Refresh", strScript);
					}

				}
				else if(intRetVal==-1)
				{
					hidFormSaved.Value = "0";
					lblMessage.Text=ClsMessages.FetchGeneralMessage("858");
				}
				else
				{
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
					hidFormSaved.Value		=	"2";
				}
				lblMessage.Visible = true;
				
			}
			catch(Exception ex)
			{
				gIntSaved=0;
				lblMessage.Text=ex.Message;
				lblMessage.Visible=true;			
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			} 		
			
		}
	}
}
