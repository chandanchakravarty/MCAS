/******************************************************************************************
	<Author					: - > Ravindra Gupta
	<Start Date				: -	> 03/24/2009
	<Description			: - > This page will server as a renderer for Dec Sheets and other attachments to be 
									viewed form BRICS.
	<Review Date			: - >
	<Reviewed By			: - >
	
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
using System.IO;
 


namespace Cms.CmsWeb.aspx
{
	/// <summary>
	/// Summary description for Viewer.
	/// </summary>
	public class Viewer  : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidmsg;
        
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			string pathName="";
			try
			{
				pathName = Request.QueryString["PN"].ToString().ToString().Replace(" ","+");
				string ext = Request.QueryString["Ext"].ToString().Replace(" ","+");

				pathName = ClsCommon.DecryptString(pathName);
				ext = ClsCommon.DecryptString(ext);

				string strUserName = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName");
                string strPassWd = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd");
                string strDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain");
                hidmsg.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2073");// "Unable to open file.";
				//Beginigng the impersonation 
				Cms.BusinessLayer.BlCommon.ClsAttachment objAttachment = new ClsAttachment();
				if (objAttachment.ImpersonateUser(strUserName, strPassWd, strDomain))
				{

                    string strQueryString = ext;
                    if(pathName.Split('=').Length>0)
                        pathName = pathName.Split('=')[0].ToString();
                    if(ext.Split('=').Length>0)
                        ext = ext.Split('=')[0].ToString();

                    //=======================================================
                    // ADDED BY SANTOSH KUMAR GAUTAM ON 29 JUNE 2011
                    // IF IS_PROCESSED FLAG IS FALSE THEN DO NOT CKECK EXISTS
                    // ======================================================

                    string IS_PROCESSED_FLAG="";
                    string IS_GENERETED_BOLETO = String.Empty;
                    string[] strArr = strQueryString.Split('&');
                    if (strArr.Length > 0)
                    {
                        for (int Count = 0; Count < strArr.Length; Count++)
                        {
                            string strIS_PROCESSED = strArr[Count];

                            if (strIS_PROCESSED.IndexOf("IS_GENERETED_BOLETO=") >= 0)
                            {   //To get IS_GENERETED_BOLETO's value
                                IS_GENERETED_BOLETO = strIS_PROCESSED.Substring(strIS_PROCESSED.IndexOf("IS_GENERETED_BOLETO=") + 20, strIS_PROCESSED.Length - (strIS_PROCESSED.IndexOf("IS_GENERETED_BOLETO=") + 20));
                            }
                            else
                            {
                                int Idx = strIS_PROCESSED.IndexOf("IS_PROCESSED=");
                                if (Idx >= 0)
                                {
                                    IS_PROCESSED_FLAG = strIS_PROCESSED.Substring(Idx + 13, strIS_PROCESSED.Length - (Idx + 13));
                                }
                            }
                        }
                    }

                     
                    //====================================

                    string FilePath = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString() + pathName;
                    //Added by Pradeep
                    //Check if file exists 
                    if ((IS_PROCESSED_FLAG == "Y" || IS_PROCESSED_FLAG == "") &&
                        (IS_GENERETED_BOLETO == "Y" || IS_GENERETED_BOLETO == "") 
                        //&&  objAttachment.IsFileExists(FilePath)// discussed with Mr. rajan
                        )
                    {
                        Response.ClearContent();
                        Response.ClearHeaders();
                        switch (ext)
                        {

                            case FILE_TYPE_PDF:
                                Response.ContentType = CONTENT_TYPE_PDF;
                                break;
                            case FILE_TYPE_TEXT:
                                Response.ContentType = CONTENT_TYPE_TEXT;
                                break;
                            case FILE_TYPE_IMG:
                                Response.ContentType = CONTENT_TYPE_IMAGE;
                                break;
                            case FILE_TYPE_WORD:
                                Response.ContentType = CONTENT_TYPE_WORD;
                                break;
                            case FILE_TYPE_GIF:
                                Response.ContentType = CONTENT_TYPE_GIF;
                                break;
                            case FILE_TYPE_EXCEL:
                                Response.ContentType = CONTENT_TYPE_EXCEL;
                                break;
                            case FILE_TYPE_PNG:
                                Response.ContentType = CONTENT_TYPE_PNG;
                                break;
                            case FILE_TYPE_PPT:
                                Response.ContentType = CONTENT_TYPE_PPT;
                                break;
                            case FILE_TYPE_XML:
                                Response.ContentType = CONTENT_TYPE_XML;
                                break;
                            case FILE_TYPE_BMP:
                                Response.ContentType = CONTENT_TYPE_BMP;
                                break;
                            case FILE_TYPE_DOCX:
                                Response.AddHeader("Content-Disposition", "attachment; filename=" + pathName);//Added on 30 Dec 2009 for Itrack Issue 6890
                                Response.ContentType = CONTENT_TYPE_MSWORD;
                                break;
                            case FILE_TYPE_CSS:
                                Response.ContentType = CONTENT_TYPE_CSS;
                                break;
                            case FILE_TYPE_BITMAP:
                                Response.ContentType = CONTENT_TYPE_BITMAP;
                                break;
                            case FILE_TYPE_TIFF:
                                Response.ContentType = CONTENT_TYPE_TIFF;
                                break;
                            case FILE_TYPE_MPEG:
                                Response.ContentType = CONTENT_TYPE_MPEG;
                                break;
                            case FILE_TYPE_MPG:
                                Response.ContentType = CONTENT_TYPE_MPG;
                                break;
                            case FILE_TYPE_PPTX:
                                Response.ContentType = CONTENT_TYPE_PPTX;
                                break;
                            case FILE_TYPE_RTF:
                                Response.AddHeader("Content-Disposition", "attachment; filename=" + pathName);//Added on 14 May 2009
                                Response.ContentType = CONTENT_TYPE_RTF;
                                break;
                            case FILE_TYPE_XLSX:
                                Response.AddHeader("Content-Disposition", "attachment; filename=" + pathName);
                                Response.ContentType = CONTENT_TYPE_XLSX;
                                break;
                            case FILE_TYPE_DOTX:
                                Response.ContentType = CONTENT_TYPE_DOTX;
                                break;
                            case FILE_TYPE_HTML:
                                Response.ContentType = CONTENT_TYPE_HTML;
                                break;
                        }
                        Response.WriteFile(FilePath);
                        Response.Flush();
                        Response.Close();

                        //ending the impersonation 
                        objAttachment.endImpersonation();
                    }
                    else
                    {  //Added by Pradeep Kushwaha itrack-872
                        if (ext.Split('=')[0].ToString() == FILE_TYPE_PDF)
                        {
                            Response.ClearContent();
                            Response.ClearHeaders();
                            string str = "/cms/application/Aspx/DeclarationPage.aspx?PN=" + Request.QueryString["PN"].ToString().ToString().Replace(" ", "+")
                                        + "&Ext=" + Request.QueryString["Ext"].ToString().Replace(" ", "+")
                                        + "&CALLED_FROM=VIEWER" + "&";
                            Response.Redirect(str);
                            return;
                        }
                        //Added till here
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        string msg = ClsMessages.FetchGeneralMessage("1439"); //"Please view file after sometime while utility is preparing the details";
                        Response.Write("<div  style='text-align:center; color:Red; font-weight:bold'>" + msg + "</div>");
                    }
				}
				else
				{
					//Impersation failed
					lblMessage.Text += "\n Unable to upload the file. User imporsonation failed.";
				}
			}
			catch(Exception ex)
			{
				//add error publishing code and custom message
				//Response.Write("Unable to open file");
				Response.Write("<script language='javascript'> alert('"+hidmsg.Value+"'); </script>");
				System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
				addInfo.Add("Err Descriptor ",ex.Message);
				addInfo.Add("message" ,pathName);
						
				ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex,addInfo);
				//throw(ex);
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
	}
}
