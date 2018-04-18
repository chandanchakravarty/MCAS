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
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using Microsoft.Xml.XQuery;
using System.IO;
namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for FactorIndex.
	/// </summary>
	public class FactorIndex : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.DropDownList cmbProducts;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvProducts;
		protected System.Web.UI.WebControls.DropDownList cmbFactors;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlForm FACTORS;
		protected System.Web.UI.WebControls.DropDownList cmbNodes;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNodes;
		protected System.Web.UI.WebControls.DataGrid Screenlisting;
		protected System.Web.UI.HtmlControls.HtmlTable tblAttributesRow;
		protected System.Web.UI.WebControls.Label lblTemp;
		protected System.Web.UI.WebControls.Panel pnlAttributes;
		protected System.Web.UI.WebControls.TextBox txtRows;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAttributeNode;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFactors;
		
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
			this.cmbProducts.SelectedIndexChanged += new System.EventHandler(this.cmbProducts_SelectedIndexChanged);
			this.cmbFactors.SelectedIndexChanged += new System.EventHandler(this.cmbFactors_SelectedIndexChanged);
			this.cmbNodes.SelectedIndexChanged += new System.EventHandler(this.cmbNodes_SelectedIndexChanged);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region CODE FOR EVENTS
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			// Calling AddData function 
			//btnReset.Attributes.Add("onclick","javascript:return AddData();");
		
			//btnSave.Attributes.Add("onclick","javascript:return onBeforeSubmit();");
			
			//Assigning the screen id of form
			base.ScreenId = "196";

			btnSave.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnReset.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReset.PermissionString	=	gstrSecurityXML;
			
			//capMessage.Visible = false;
			if(!Page.IsPostBack)
			{	 
				FillControls();
				//SetFormValues();
				SetErrorMessages();
			}
		}

		
		private void cmbProducts_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// if product is selected, show factors in that product
			if(cmbProducts.SelectedItem !=null)
			{
				DataSet dsFactors= ClsRatingBuilder.FetchFactors(cmbProducts.SelectedItem.Value);
				cmbFactors.DataSource = dsFactors;
				cmbFactors.DataMember = "FACTOR";
				cmbFactors.DataValueField = "ID";
				cmbFactors.DataTextField  = "NAME";
				cmbFactors.DataBind();
				cmbFactors.Items.Insert(0,"");

				Screenlisting.DataSource=null;
				Screenlisting.Visible =false;
			}
		}

		
		private void cmbFactors_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// if factor is selected, show nodes in that product
			if(cmbFactors.SelectedItem !=null)
			{
				if(cmbProducts.SelectedItem!=null && cmbFactors.SelectedItem !=null )
				{
					DataSet dsNodes= ClsRatingBuilder.FetchNodes(cmbProducts.SelectedItem.Value ,cmbFactors.SelectedItem.Value );
					cmbNodes.DataSource = dsNodes;
					cmbNodes.DataMember = "NODE";
					cmbNodes.DataValueField = "ID";
					cmbNodes.DataTextField  = "NAME";
					cmbNodes.DataBind();
					cmbNodes.Items.Insert(0,"");
 
					Screenlisting.DataSource=null;
					Screenlisting.Visible =false;
					pnlAttributes.Visible =false;
				}
			}
		}

		
		private void cmbNodes_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				/* Steps to be performed when the node is clicked.
				 * 1. Check if any node is selected
				 * 2. Check the number of attributes and create the text boxes. 
				 */
			
				if(cmbProducts.SelectedItem!=null && cmbFactors.SelectedItem !=null && cmbNodes.SelectedItem !=null)
				{
					//Calling the function to show appropriate textboxes from the transformed xml
					DisplayAttributesAsTextBoxes();

					#region COMMENTED -	WAS TO BE USED FOR THE GRID CONTROL.(IF REQUIRED)
					//	Screenlisting.Visible =true;
					//					BindData();
					//				
					//					if(Screenlisting.PageCount==0 || Screenlisting.PageCount<2)
					//					{
					//						Screenlisting.PagerStyle.Visible=false;
					//					}

					#endregion
					
				}
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{}
		}	

		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				//Get the new attributes XML
				string newInnerXML =@hidAttributeNode.Value;
				if(cmbProducts.SelectedItem!=null && cmbFactors.SelectedItem !=null && cmbNodes.SelectedItem !=null && cmbNodes.SelectedItem.Value !="" )
				{
					// Pass it on to the business layer along with the other details to save
					ClsRatingBuilder.ReplaceNode(cmbProducts.SelectedItem.Value ,cmbFactors.SelectedItem.Value,cmbNodes.SelectedItem.Value,newInnerXML);
				 
					DisplayAttributesAsTextBoxes();
				}
				//ClsRatingBuilder.AppendAttribute(cmbProducts.SelectedItem.Value,cmbFactors.SelectedItem.Value,cmbNodes.SelectedItem.Value,"TEST");
			}
		
			catch(Exception exc)
			{
				capMessage.Text		=	exc.Message;
				capMessage.Visible	=	true;
			}
			finally
			{

			}
		}

		
		#region THIS CODE CAN BE REMOVED  IF WE DECIDE AGAINST USING DATAGRID - WAS TO BE USED FOR GRID CONTROL
		private void BindData()
		{
			DataSet dsTEMP= ClsRatingBuilder.GetAttributes(cmbProducts.SelectedItem.Value ,cmbFactors.SelectedItem.Value,cmbNodes.SelectedItem.Value);
			Screenlisting.DataSource =dsTEMP;
		
			DataRow dr = dsTEMP.Tables[0].NewRow();
			dsTEMP.Tables[0].Rows.Add(dr);
			Screenlisting.DataBind();
			
			
		}

		protected void Screenlisting_PageIndexChanged( object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e) 
		{
			Screenlisting.CurrentPageIndex = e.NewPageIndex;
			BindData();
			
			
		}	

		public void Screenlisting_Edit(  object Source,DataGridCommandEventArgs E)
		{
			Screenlisting.EditItemIndex = E.Item.ItemIndex;
			BindData();
		}
		public void Screenlisting_Update(  object Source,DataGridCommandEventArgs E)
		{
			Screenlisting.EditItemIndex = E.Item.ItemIndex;
			BindData();
		}
		public void Screenlisting_Cancel(  object Source,DataGridCommandEventArgs E)
		{
			Screenlisting.EditItemIndex = -1;
			BindData();
		}

		public void Screenlisting_ItemCommand(object Source,DataGridCommandEventArgs e)
		{
			if (e.CommandName == "Insert" )
			{
					Screenlisting.EditItemIndex = e.Item.ItemIndex;
				BindData();
			}

		}
		public void Screenlisting_ItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item ||
				e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView rv = (DataRowView)e.Item.DataItem;
				// Get fourth column value.
				int nUnitsInStock = Convert.ToInt32(rv.Row.ItemArray[4]);
				if (nUnitsInStock < 20)
				{
					e.Item.Cells[4].BackColor = Color.Red;
				}
			}
		}

		private void btnReset_Click(object sender, System.EventArgs e)
		{
			DataGridItem  dgItem;
			TableCell dgCell =new TableCell();
			int intColCounter ;
			dgItem = new DataGridItem(2, 0, ListItemType.Item);
			for(intColCounter = 0 ;intColCounter< Screenlisting.Columns.Count - 1; intColCounter++)
			{
				dgCell = new TableCell();
				dgItem.Cells.Add(dgCell);
				dgCell.Text = ".";
			}
			 
			Screenlisting.Controls.AddAt(Screenlisting.Items.Count-1 , dgItem);
		}

		#endregion

		#endregion

		# region UTILITY METHODS

		private void DisplayAttributesAsTextBoxes()
		{
			try
			{
				//Calling the function to show appropriate textboxes
				lblTemp.Text = ClsRatingBuilder.GetTransformedXMLAttributes(cmbProducts.SelectedItem.Value ,cmbFactors.SelectedItem.Value,cmbNodes.SelectedItem.Value);
				pnlAttributes.Visible =true;
				
				DataSet dsTemp =ClsRatingBuilder.GetAttributes(cmbProducts.SelectedItem.Value ,cmbFactors.SelectedItem.Value,cmbNodes.SelectedItem.Value);
				

				int iCurCounter=0;
				iCurCounter= dsTemp.Tables[0].Rows.Count;
				txtRows.Text = Convert.ToString(iCurCounter);
				
			}
			catch
			{}
			finally
			{}
		}
		
		private void FillControls()
		{
			// fill the product combo
			DataSet dsProducts= ClsRatingBuilder.FetchProducts();
			cmbProducts.DataSource = dsProducts;
			cmbProducts.DataMember = "PRODUCT";
			cmbProducts.DataValueField = "ID";
			cmbProducts.DataTextField  = "DESC";
			cmbProducts.DataBind();
			cmbProducts.Items.Insert(0,"");

		}

		private void SetErrorMessages()
		{
			rfvFactors.ErrorMessage	=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"414");
			rfvNodes.ErrorMessage=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"415");
			rfvProducts.ErrorMessage =	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"413");
			
			
		}
		

		# endregion
	}
		
	
}
