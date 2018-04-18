/******************************************************************************************
<Author					: -  Mohit Gupta
<Start Date				: -	04/05/2005 
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - June 23, 2005
<Modified By			: - Anshuman
<Purpose				: - XML parsing should match with attribute name.

<Modified Date			: - March 23, 2007
<Modified By			: - Anurag verma
<Purpose				: - fetching message from customizeMessage.xml

*******************************************************************************************/ 
using System;
using System.Text;
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

//Added by Charles on 19-Apr-2010 for Multilingual Implementation
using System.Reflection;
using System.Resources;

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for TransactionLogDetail.
	/// </summary>
	public class TransactionLogDetail : cmsbase
	{
		protected System.Web.UI.WebControls.Label capDescription;
		protected System.Web.UI.WebControls.Label lblDescription;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capAppNumber;
		protected System.Web.UI.WebControls.Label lblAppNumber;
		protected System.Web.UI.WebControls.Label capVersionNnmber;
		protected System.Web.UI.WebControls.Label lblVersionNumber;
		protected System.Web.UI.WebControls.Label lblDescriptionaChkDetail;
		protected System.Web.UI.HtmlControls.HtmlTable tblHeadings;
		protected System.Web.UI.WebControls.DataGrid dgTrans;
		protected System.Web.UI.WebControls.Literal ltCoverage;
		protected System.Web.UI.WebControls.Image imgVerifyApp;	
		private string CALLED_FROM_ENDORSEMENT = "ENDORSEMENT";
		StringBuilder sbTran = new StringBuilder();
		public string strCalledFrom="";

        private ResourceManager objResourceManager = null;	
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			//Added code to call either from policy details or customer assitant.Added screen id 224_10_1 for endorsement -Added by Sibin on 21 Oct 08

			if (Request.QueryString["CALLEDFROM"]!=null && Request.QueryString["CALLEDFROM"].ToString().Trim()!="")
			{
				strCalledFrom = Request.QueryString["CALLEDFROM"].ToString().Trim();	
				
			}

			if(strCalledFrom=="ENDORSEMENT")
				base.ScreenId ="224_10_1";

			else 
			    base.ScreenId ="120_6_0";

            //Added by Charles on 19-Apr-2010 for Multilingual Implementation
            objResourceManager = new ResourceManager("Cms.CmsWeb.Maintenance.TransactionLogDetail", Assembly.GetExecutingAssembly());
			
			lblDescription.Visible=false;
			capDescription.Visible=false;
			lblAppNumber.Visible=false;
			capAppNumber.Visible=false;
			lblVersionNumber.Visible=false;
			capVersionNnmber.Visible=false;
			 
			if (Request.QueryString["TRANS_ID"] !=null &&  Request.QueryString["TRANS_ID"].ToString() != "")
			{
				try
				{
					//int transId=int.Parse(Request.QueryString["TRANS_ID"].ToString());
					string transId=Request.QueryString["TRANS_ID"].ToString();
					strCalledFrom=Request.QueryString["CalledFrom"].ToString();
					string strTransDescription = "", strAdditionalInfo = "", strTRANSTYPEID = "", strClientId = "",strpolicyid = "" , strPolicyVersionId = "";
					int tempTransId=0, lthurl=0;
					bool SplitValuesExist = false;
					string []tmpTransArray = transId.Split('^');
					string[] straddinfo = new string[0];
					ClsTransactionLog objTransLog=new ClsTransactionLog();
					if(strCalledFrom==CALLED_FROM_ENDORSEMENT)	
					{					
						if(tmpTransArray!=null && tmpTransArray.Length>0)
						{
                            //Added by Lalit for cancel endorsement 
                            //description 
                            Array.Reverse(tmpTransArray);
							tempTransId = int.Parse(tmpTransArray[0].ToString());
							SplitValuesExist = true;
						}
					}
					else	
						tempTransId = int.Parse(transId);
				
					DataTable dtTransDesc = objTransLog.GetTransactionDescription(tempTransId);
					if(dtTransDesc.Rows.Count > 0)
					{
						if(dtTransDesc.Rows[0]["TRANS_DESC"] != null)
							strTransDescription = dtTransDesc.Rows[0]["TRANS_DESC"].ToString();
						if(dtTransDesc.Rows[0]["ADDITIONAL_INFO"] != null)
							strAdditionalInfo = dtTransDesc.Rows[0]["ADDITIONAL_INFO"].ToString();					
						
                        //string Path = strAdditionalInfo.Split('(');  
						if(dtTransDesc.Rows[0]["TRANS_TYPE_ID"] != null)
							strTRANSTYPEID = dtTransDesc.Rows[0]["TRANS_TYPE_ID"].ToString();
						if(dtTransDesc.Rows[0]["CUSTOMER_ID"] != null)
							strClientId = dtTransDesc.Rows[0]["CUSTOMER_ID"].ToString();
						if(dtTransDesc.Rows[0]["POLICY_ID"] != null)
							strpolicyid = dtTransDesc.Rows[0]["POLICY_ID"].ToString();
						if(dtTransDesc.Rows[0]["POLICY_VERSION_ID"] != null)
							strPolicyVersionId = dtTransDesc.Rows[0]["POLICY_VERSION_ID"].ToString();
					}
					strAdditionalInfo	=	strAdditionalInfo.Replace(";","<br>");
					int windowindex;
					if(strAdditionalInfo.IndexOf(cmsbase.COMMIT_ANYWAYS_RULES)!=-1)
					{
						int iPos=strAdditionalInfo.IndexOf(cmsbase.COMMIT_ANYWAYS_RULES);
						int iLen=strAdditionalInfo.Length;
						int iDiff = iLen-iPos;
						strAdditionalInfo = strAdditionalInfo.Substring(0,iPos) + "(Please click on the image to see the rules violated)";
						imgVerifyApp.Attributes.Add("onClick","RulesViolated(" + dtTransDesc.Rows[0]["CUSTOMER_ID"].ToString() + "," + dtTransDesc.Rows[0]["POLICY_ID"].ToString() + "," + dtTransDesc.Rows[0]["POLICY_VERSION_ID"].ToString() + ")");
						imgVerifyApp.ImageUrl    =  "~/cmsweb/Images" + GetColorScheme()  + "/Rule_ver.gif";
						imgVerifyApp.Visible = true;
					}
                    else if ((windowindex = strAdditionalInfo.IndexOf("COMMON_PDF_URL")) != -1 || (windowindex = strAdditionalInfo.IndexOf("COMMON_BOLETO_URL")) != -1)
					{
						string EncryptedPath="";
						if(strAdditionalInfo.IndexOf("window.open") > 0)
						{
							//strAdditionalInfo.Remove(
							int StartOfPath = strAdditionalInfo.IndexOf("window.open");
							string TempValue = strAdditionalInfo.Substring(StartOfPath);
							//Condition added For itrack Isue #6891.
							if(TempValue.IndexOf(")")>0)
							{
								string TempPath= TempValue.Substring(TempValue.IndexOf("("),TempValue.IndexOf(")") - TempValue.IndexOf("("));

								if(TempPath.IndexOf("/OUTPUTPDFs")>0)
								{
									int load = TempPath.IndexOf("/OUTPUTPDFs");
									string Info = TempPath.Substring(load); 
									string []Path_Info =Info.Split('\"'); 
									string []Path = Path_Info[0].Split('.'); 
									EncryptedPath = ClsCommon.CreateContentViewerURL(Path_Info[0] , Path[1].ToUpper());
									strAdditionalInfo= strAdditionalInfo.Remove(StartOfPath +13,strAdditionalInfo.Length-StartOfPath-13);
									strAdditionalInfo+= EncryptedPath + "." + Path[1].ToString() +"\")>";
								}
                                else 
                                {
                                    EncryptedPath = TempPath.Substring(TempPath.IndexOf("(") + 1);
                                    //EncryptedPath = ClsCommon.CreateContentViewerURL("", TempPath);
                                    strAdditionalInfo = strAdditionalInfo.Remove(StartOfPath + 13, strAdditionalInfo.Length - StartOfPath - 13);
                                    strAdditionalInfo += EncryptedPath + "." + "\")>";
                                }	
							}
						}

						int urlafter = "COMMON_PDF_URL".Length;
						//imgVerifyApp.Attributes.Add("onClick",strAdditionalInfo.Substring(windowindex, strAdditionalInfo.Length-1-windowindex));
						if(strTRANSTYPEID == "3" && strClientId =="0" && strpolicyid == "0" && strPolicyVersionId == "0")
						{
							int intsecondindex = strAdditionalInfo.IndexOf("\")>");
							//string strtest = strAdditionalInfo.Substring(windowindex+urlafter+1,intsecondindex-urlafter-windowindex+1);
							//imgVerifyApp.Attributes.Add("onClick",strAdditionalInfo.Substring(windowindex+urlafter+1,intsecondindex-urlafter-windowindex+1));
							imgVerifyApp.Attributes.Add("onClick","window.open('" + EncryptedPath + "');");
						}
						else
						{
							//imgVerifyApp.Attributes.Add("onClick",strAdditionalInfo.Substring(windowindex+urlafter+1,strAdditionalInfo.Length-urlafter-windowindex-2));
							imgVerifyApp.Attributes.Add("onClick","window.open('" + EncryptedPath + "');");
						}
						
						imgVerifyApp.ImageUrl    =  "~/cmsweb/Images" + GetColorScheme()  + "/Rule_ver.gif";
						imgVerifyApp.ToolTip = "Open Pdf";
						imgVerifyApp.Visible = true;
					}
					else
					{						
						imgVerifyApp.Visible = false;
					}
					
				if(strTRANSTYPEID == "3" && strClientId =="0" && strpolicyid == "0" && strPolicyVersionId == "0")
					{
						string strAdditionalInfoPre = strAdditionalInfo;
						lthurl = strAdditionalInfo.Length;
					     //Comment For Itrack #5293
						//straddinfo = strAdditionalInfo.Split('.');
						if(strAdditionalInfoPre.IndexOf("COMMON_PDF_URL")!=-1)
						{
							//Comment For Itrack #5293
							//lblDescription.Text=strTransDescription + "<br><br>" + straddinfo[0]+ ".pdf";
							lblDescription.Text= strTransDescription + "<br>" +  strAdditionalInfoPre ;
						}
						//Added By Raghav For Activate/Deactivate Decription.
						else
						{
							lblDescription.Text= strTransDescription;
							
						}
					   	//Comment For Itrack #5293 
					   //strAdditionalInfo = strAdditionalInfo.Replace(straddinfo[0]+ ".pdf","");
						//strAdditionalInfo ="";
						//for(int iCounter=1; iCounter<straddinfo.Length; iCounter++)
						//{
						//	strAdditionalInfo +=straddinfo[iCounter].ToString(); 
						//}
					   //Added For Itrack #5293
					   if(strAdditionalInfoPre.IndexOf("COMMON_PDF_URL")==-1)	
					   lblDescriptionaChkDetail.Text ="<br>" + strAdditionalInfo;
						//lblDescriptionaChkDetail.Text ="<br>" + strAdditionalInfo.Replace("pdf","");
					}
					else
					{
						lblDescription.Text=strTransDescription + "<br>" + strAdditionalInfo;
					}

					capDescription.Visible=true;
					lblDescription.Visible=true;
					if(strCalledFrom==CALLED_FROM_ENDORSEMENT)
					{
						if(SplitValuesExist)
						{
							for(int iCtr = 0;iCtr<tmpTransArray.Length;iCtr++)
							{
								GenerateChangedXmlDescription(int.Parse(tmpTransArray[iCtr].ToString()));
							}
						}
					}
					else
					{
						GenerateChangedXmlDescription(tempTransId);
					}

					
					
				}
				catch(Exception ex)
				{

					throw(ex);
				}

			}
			else
			{
				// show message transaction id not valid.
			}

            //Added by Charles on 19-Apr-2010 for Multilingual Implementation
            if (!IsPostBack)
            {
                SetCaptions();    
            }            
		}

        /// <summary>
        /// Set Captions from Resource File
        /// </summary>
        /// Added by Charles on 19-Apr-2010 for Multilingual Implementation
        private void SetCaptions()
        {
            capDescription.Text = objResourceManager.GetString("capDescription");
            capAppNumber.Text = objResourceManager.GetString("capAppNumber");
            capVersionNnmber.Text = objResourceManager.GetString("capVersionNnmber");
        }

		private void GenerateChangedXmlDescription(int transId)
		{
			//string strXml=objTransLog.GetTransactionXML(transId);
			string strXml="",xmlFlag="N";
			ClsTransactionLog objTransLog=new ClsTransactionLog();
			DataTable dtXML=objTransLog.GetTransactionXMLDataSet(transId);
			if(dtXML!=null)
			{
				if(dtXML.Rows.Count>0 )
				{
					strXml=dtXML.Rows[0]["TRANS_DETAILS"].ToString();
					if(dtXML.Rows[0]["is_validxml"].ToString().Equals(" "))
					{
						xmlFlag="Y";
						lblMessage.Visible=true;  
						lblMessage.Text=ClsMessages.FetchGeneralMessage("923");  
						lblMessage.ForeColor=Color.Red;	
					}
					else
						lblMessage.Visible=false;  
				}
			}
			if (strXml !="")
			{				
						
				if (strCalledFrom!="CLT")
				{
					if (strCalledFrom.ToUpper() =="INCLT")
					{
						int custId=int.Parse(Request.QueryString["CUSTOMER_ID"].ToString());
						lblAppNumber.Visible=false;
						capAppNumber.Visible=false;
						lblVersionNumber.Visible=false;
						capVersionNnmber.Visible=false;
					}
					else if (strCalledFrom.ToUpper() != CALLED_FROM_ENDORSEMENT && strCalledFrom.ToUpper() != "MNT")
					{
						int custId=int.Parse(Request.QueryString["CUSTOMER_ID"].ToString());
						int appId=int.Parse(Request.QueryString["APP_ID"].ToString());
						int appVersionId=int.Parse(Request.QueryString["APP_version_ID"].ToString());
						lblAppNumber.Visible=true;
						capAppNumber.Visible=true;
						lblVersionNumber.Visible=true;
						capVersionNnmber.Visible=true;
						DataTable dtTemp=objTransLog.GetApplicationDetail(custId,appId,appVersionId);							
						lblAppNumber.Text=dtTemp.Rows[0]["APP_NUMBER"].ToString();
						lblVersionNumber.Text=dtTemp.Rows[0]["APP_VERSION"].ToString();
					}
				}	
						

				XmlDocument xmlDoc = new XmlDocument();
				//strXml=ClsCommon.EncodeXMLCharacters (strXml); 
				xmlDoc.LoadXml(strXml.Replace("&","&amp;"));
				DataTable dt=new DataTable();
				dt.Columns.Add("LableName",typeof(string));
				dt.Columns.Add("FieldName",typeof(string));
				dt.Columns.Add("OldValue",typeof(string));
				dt.Columns.Add("NewValue",typeof(string));				
						
				//XmlNode root=xmlDoc.FirstChild;
				XmlNodeList nodeList = null;

				string rootName = xmlDoc.DocumentElement.Name;
				string LastHeading="";

				if ( rootName == "root" )
				{
					nodeList = xmlDoc.SelectNodes("root/LabelFieldMapping");
					dt.Columns.Add("Heading",typeof(string));	
				}
				else
				{
					nodeList = xmlDoc.SelectNodes("LabelFieldMapping");
				}

                sbTran.Append("<Table border='1' cellspacing='0' style='border-collapse:collapse;'><tr class='HeadRow'><td>" + objResourceManager.GetString("lblLabelName") + "</td><td>" + objResourceManager.GetString("lblModifiedFrom") + "</td><td>" + objResourceManager.GetString("lblModifiedTo") + "</td></tr>");

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
									
							//Coverage Section
							if ( node.SelectSingleNode("Heading/Coverage") != null )
							{
								if (  node.SelectSingleNode("Heading/Coverage") != null && LastHeading!=node.SelectSingleNode("Heading/Coverage").InnerText)
								{
									dr["Heading"] = node.SelectSingleNode("Heading/Coverage").InnerText;
									LastHeading = node.SelectSingleNode("Heading/Coverage").InnerText;
									dt.Rows.Add(dr);

									sbTran.Append("<tr>");
									sbTran.Append("<td colspan='3' class='DataRow' style='font-weight:bold;'>");
									sbTran.Append(node.SelectSingleNode("Heading/Coverage").InnerText);
									sbTran.Append("</td>");
									sbTran.Append("</tr>");
								}
							}//Endosrsement Section
							else if ( node.SelectSingleNode("Heading/EndorsementDesc") != null )
							{
								if (  node.SelectSingleNode("Heading/EndorsementDesc") != null && LastHeading!=node.SelectSingleNode("Heading/EndorsementDesc").InnerText)
								{
									dr["Heading"] = node.SelectSingleNode("Heading/EndorsementDesc").InnerText;
									LastHeading = node.SelectSingleNode("Heading/EndorsementDesc").InnerText;
									dt.Rows.Add(dr);

									sbTran.Append("<tr>");
									sbTran.Append("<td colspan='3' class='DataRow' style='font-weight:bold;'>");
									sbTran.Append(node.SelectSingleNode("Heading/EndorsementDesc").InnerText.Replace("&amp;","&") );
									sbTran.Append("</td>");
									sbTran.Append("</tr>");
								}
							}
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

				//Showing heading
				//ShowHeadingInformation(xmlDoc);
						
				//dgTrans.DataSource=dt.DefaultView;
				//dgTrans.DataBind();
				dgTrans.Visible=false;
			}
		}

		public void ShowHeadingInformation(System.Xml.XmlDocument doc)
		{
			System.Xml.XmlNode root = doc.SelectSingleNode("/LabelFieldMapping/Heading");
			HtmlTableRow tr;
			HtmlTableCell td;
			
			if (root == null)
			{
				tblHeadings.Visible = false;
				return;
			}

			for (int i=0; i<root.ChildNodes.Count; i++)
			{
				tr = new HtmlTableRow();
				td = new HtmlTableCell();

				td.Attributes.Add("class","midcolora");
				td.InnerHtml = root.ChildNodes[i].Name + " = " + root.ChildNodes[i].InnerXml;
				
				tr.Cells.Add(td);
				tblHeadings.Rows.Add(tr);
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
			//this.dgTrans.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgTrans_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/*private void dgTrans_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if ( e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item )
			{
				DataRowView drv = (DataRowView)e.Item.DataItem;
				
				
				if (drv.Row.Table.Columns.Contains("Heading") == true )
				{
					Response.Write(drv["Heading"].ToString());
				}
			}
		}*/
	}
}
