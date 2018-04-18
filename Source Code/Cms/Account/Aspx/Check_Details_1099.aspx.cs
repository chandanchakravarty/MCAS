		/******************************************************************************************
<Author					: - praveen kasana
<Start Date				: -	7/17/2008 4:16:15 PM
<End Date				: -	
<Description			: - Code behind for 1099 Check Details
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - Code behind for 1099 Check Details
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
using Cms.BusinessLayer.BlAccount;
using Cms.Model.Account;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.BusinessLayer.BlCommon;
using System.Text;
using System.Xml;


namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for 1099 Check details
	/// </summary>
	public class Check_Details_1099 : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.DataGrid grd1099CheckDetails;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblTransMessage;
		protected System.Web.UI.WebControls.Literal ltCoverage;
		StringBuilder sbTran = new StringBuilder();	
		
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				if (Request.QueryString["FORM_1099_ID"]!=null && Request.QueryString["FORM_1099_ID"].ToString().Trim()!="")
					BindGrid(int.Parse(Request.QueryString["FORM_1099_ID"].ToString().Trim()));	
			}
			
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
		}
		#endregion		
		private void BindGrid(int formId)
		{
			DataSet ds = null;
			ClsAccount objAccount = new ClsAccount();
			try
			{
				ds = objAccount.Get1099CheckDetails(formId);
				grd1099CheckDetails.DataSource = ds;
				grd1099CheckDetails.DataBind();

				ds = objAccount.GetTransactionDetails1099(formId);
				foreach(DataRow drTrans in ds.Tables[0].Rows)
				{
					if(drTrans["TRANS_ID"]!=null && drTrans["TRANS_ID"].ToString()!="")
					{
						string strHeader = "<tr class='HeadRow'><td>Modified By : " + drTrans["RECORDED_BY_NAME"].ToString() + "</td><td>Modified Date : " + drTrans["RECORD_DATE_TIME"].ToString() + "</td></tr>";
						GenerateChangedXmlDescription(int.Parse(drTrans["TRANS_ID"].ToString().Trim()),strHeader);
					}
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objAccount!=null)
					objAccount = null;

			}
			
		}		
		
		private void GenerateChangedXmlDescription(int transId,string Header)
		{
			//Added For Itrack #Issue #5065
			System.Xml.XmlDocument transdoc  = new System.Xml.XmlDocument();      
			string strXml="",xmlFlag="N";
			ClsTransactionLog objTransLog=new ClsTransactionLog();
			DataTable dtXML=objTransLog.GetTransactionXMLDataSet(transId);
			if(dtXML!=null)
			{
				if(dtXML.Rows.Count>0 )
				{
					strXml=dtXML.Rows[0]["TRANS_DETAILS"].ToString();
					transdoc.LoadXml(strXml); 
					XmlNode nodeREGARDING = transdoc.SelectSingleNode("//Map[@field='RECIPIENT_STATE']");
					//Added For Itrack #Issue #5065
					if(nodeREGARDING!=null)
					{
						XmlAttribute attrREGARDING_OLD = nodeREGARDING.Attributes["OldValue"];
						XmlAttribute attrREGARDING_NEW = nodeREGARDING.Attributes["NewValue"];
						if(attrREGARDING_NEW!=null) 
						{
							string OldStateName = "";string NewStateName ="";
							string OldStateID  = attrREGARDING_OLD.InnerText.ToString();   
							string NewStateID = attrREGARDING_NEW.InnerText.ToString();
							if(OldStateID != null && OldStateID !="") 
							{ 
								OldStateName = ClsStates.GetStateList(OldStateID);  
								
							}   
							if (NewStateID != null && NewStateID !="")
							{
								NewStateName = ClsStates.GetStateList(NewStateID);   
							}
							attrREGARDING_OLD.InnerText = OldStateName;
							attrREGARDING_NEW.InnerText = NewStateName;

							strXml = transdoc.OuterXml;


						}
					}
					//Added Till here.
					
					if(dtXML.Rows[0]["is_validxml"].ToString().Equals(" "))
					{
						xmlFlag="Y";
						lblTransMessage.Visible=true;  
						lblTransMessage.Text= "Manual Updation on 1099 Details.";
						lblTransMessage.ForeColor=Color.Red;	
					}
					else
						lblTransMessage.Visible=false;  
				}
			}
			if (strXml !="")
			{				
						
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(strXml.Replace("&","&amp;"));
				DataTable dt=new DataTable();
				dt.Columns.Add("LableName",typeof(string));
				dt.Columns.Add("FieldName",typeof(string));
				dt.Columns.Add("OldValue",typeof(string));
				dt.Columns.Add("NewValue",typeof(string));				
						
				XmlNodeList nodeList = null;

				string rootName = xmlDoc.DocumentElement.Name;
				if ( rootName == "root" )
				{
					nodeList = xmlDoc.SelectNodes("root/LabelFieldMapping");
					dt.Columns.Add("Heading",typeof(string));	
				}
				else
				{
					nodeList = xmlDoc.SelectNodes("LabelFieldMapping");
				}

				sbTran.Append("<Table border='1' cellspacing='0' style='border-collapse:collapse;'>" + Header);
				sbTran.Append("</Table>");
					
						
				sbTran.Append("<Table border='1' cellspacing='0' style='border-collapse:collapse;'><tr class='HeadRow'><td>Field Name</td><td>Modified From</td><td>Modified To</td></tr>");

				foreach(XmlNode node in nodeList)
				{
					XmlNode root = node;
					for (int i=0; i < root.ChildNodes.Count; i++)
					{
						DataRow dr=dt.NewRow();
						try
						{
							dr["LableName"]	=	root.ChildNodes[i].Attributes["label"].Value.Trim();
							dr["FieldName"]	=	root.ChildNodes[i].Attributes["field"].Value.Trim();
							dr["OldValue"]	=	root.ChildNodes[i].Attributes["OldValue"].Value.Trim() == "null" ? "" : root.ChildNodes[i].Attributes["OldValue"].Value.Trim();
							dr["NewValue"]	=	root.ChildNodes[i].Attributes["NewValue"].Value.Trim() == "null" ? "" : root.ChildNodes[i].Attributes["NewValue"].Value.Trim();
									
							if( !( ( dr["OldValue"].ToString() == "" ) && ( dr["NewValue"].ToString() == "" ) ) && (dr["LableName"].ToString() != ""))
							{

								sbTran.Append("<tr>");
								sbTran.Append("<td  class='DataRow' style='font-weight:bold;'>");
								sbTran.Append(root.ChildNodes[i].Attributes["label"].Value.Trim());
								sbTran.Append("</td>");
									
								if(root.ChildNodes[i].Attributes["OldValue"].Value.Trim()!="null" && root.ChildNodes[i].Attributes["OldValue"].Value.Trim()!="")
								{
									sbTran.Append("<td  class='DataRow'>");
									sbTran.Append(root.ChildNodes[i].Attributes["OldValue"].Value.Trim().Replace("&amp;","&"));
									sbTran.Append("</td>");
								}
								else
									sbTran.Append("<td  class='DataRow'></td>");

								if(root.ChildNodes[i].Attributes["NewValue"].Value.Trim()!="null" && root.ChildNodes[i].Attributes["NewValue"].Value.Trim()!="")
								{
									sbTran.Append("<td  class='DataRow'>");
									sbTran.Append(root.ChildNodes[i].Attributes["NewValue"].Value.Trim().Replace("&amp;","&"));
									sbTran.Append("</td>");
									sbTran.Append("</tr>");
								}
								else
									sbTran.Append("<td  class='DataRow'></td></tr>");
							}

							if( !( ( dr["OldValue"].ToString() == "" ) && ( dr["NewValue"].ToString() == "" ) ) && (dr["LableName"].ToString() != ""))
								dt.Rows.Add(dr);
						}
						catch
						{
							continue;
						}
					}
				}

				sbTran.Append("</Table><br>");

				this.ltCoverage.Text = this.sbTran.ToString();

				
				
			}
		}

	}
}
