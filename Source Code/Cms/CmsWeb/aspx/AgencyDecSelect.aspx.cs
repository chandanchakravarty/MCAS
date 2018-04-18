/******************************************************************************************

Created By				: Mohit Agarwal
Dated					: 24 Jul 2007
Purpose					: To select type of Dec pages and open them  
  
<Modified Date			: - > 
<Modified By			: - > 
<Purpose				: - > 
******************************************************************************************/

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
using System.IO;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.CmsWeb.WebControls;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;

namespace Cms.CmsWeb.aspx
{
	/// <summary>
	/// Summary description for AgencyDecSelect.
	/// </summary>
	public class AgencyDecSelect : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnClose;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnFetchDec;
		protected System.Web.UI.WebControls.CheckBoxList chkDecList;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidKeyValues;
		string custId="", polId="", polVerId="", lobId="",FilePath="";
		protected System.Web.UI.WebControls.CheckBox chkDec;
		protected System.Web.UI.WebControls.CheckBoxList chkAddlList;
		protected System.Web.UI.WebControls.CheckBox chkAddl;
		string []add_list;
		DataTable objPolicyMaxVerTable;
		string strPolicyMaxVer="";
		private void Page_Load(object sender, System.EventArgs e)
		{
			// register with ajax dll
			//Ajax.Utility.RegisterTypeForAjax(typeof(AgencyDecSelect));

			GetQueryStringValues();
			getAddlList();
			if(!Page.IsPostBack)
			{
				FillDecList();
			}
			// Put user code to initialize the page here
		}
//		#region AJAX CALL TO FILL DRIVER DROPDOWNS
//		[Ajax.AjaxMethod()]
//		public void AjaxCallFunction() 
//		{
//			fxnShowDecSheet();
//			//return fxnShowDecSheet();
//		}
//		#endregion 
		private void GetQueryStringValues()
		{
			if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
			{ 						
				custId=Request.QueryString["CUSTOMER_ID"].ToString(); 
			}				
			if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"].ToString() != "")
			{ 						
				polId=Request.QueryString["POLICY_ID"].ToString(); 
			}				
			if (Request.QueryString["POLICY_VER_ID"] != null && Request.QueryString["POLICY_VER_ID"].ToString() != "")
			{ 						
				polVerId=Request.QueryString["POLICY_VER_ID"].ToString(); 
			}				
			if (Request.QueryString["LOB_ID"] != null && Request.QueryString["LOB_ID"].ToString() != "")
			{ 						
				lobId=Request.QueryString["LOB_ID"].ToString(); 
			}				
			
		}

		private void getAddlList()
		{
			string veh_id="";
			string addint_id="";
			string Addilname="";
			bool flgdec=false;
			int index =0;
			Cms.BusinessLayer.BlApplication.clsapplication objApp = new clsapplication();
			DataSet addintds= objApp.FetchAddIntDetails(custId, polId, polVerId, lobId, "POLICY");
			DataSet addintHmWt=objApp.FetchAddIntDetails(custId, polId, polVerId, "4", "POLICY");
			add_list = new string[addintds.Tables[0].Rows.Count + addintHmWt.Tables[0].Rows.Count +3];
			

			if(addintds != null && addintds.Tables[0].Rows.Count > 0)
			{
				flgdec=true;
				//add_list = new string[addintds.Tables[0].Rows.Count+3];
					add_list[0] = "DEC_PAGE_NOWORD_" +  custId + "_" + polId + "_" + polVerId;
					add_list[1] = "DEC_PAGE_C_" +  custId + "_" + polId + "_" + polVerId;
					add_list[2] = "DEC_PAGE_A_" +  custId + "_" + polId + "_" + polVerId;
					index = 3;
				
				foreach(DataRow addintdr in addintds.Tables[0].Rows)
				{
					if(lobId == "1" || lobId == "6")
						veh_id = addintdr["DWELLING_ID"].ToString();
					else if(lobId == "2" || lobId == "3")
						veh_id = addintdr["VEHICLE_ID"].ToString();
					else if(lobId == "4")
						veh_id = addintdr["BOAT_ID"].ToString();
					addint_id = addintdr["ADD_INT_ID"].ToString();
					Addilname = addintdr["ADD_INT_NAME"].ToString();
					add_list[index] = "ADDLINT_" + Addilname + "_" +  custId + "_" + polId + "_" + polVerId + "_" + veh_id + "_" + addint_id;
					index ++;
				}
			}
			// If Policy Lob is Home with Watercraft
			if(lobId=="1")
			{
				addintHmWt=objApp.FetchAddIntDetails(custId, polId, polVerId, "4", "POLICY");
				if(addintHmWt != null && addintHmWt.Tables[0].Rows.Count > 0)
				{
					
					if(flgdec==false)
					{
						flgdec=true;
						add_list[0] = "DEC_PAGE_NOWORD_" +  custId + "_" + polId + "_" + polVerId;
						add_list[1] = "DEC_PAGE_C_" +  custId + "_" + polId + "_" + polVerId;
						add_list[2] = "DEC_PAGE_A_" +  custId + "_" + polId + "_" + polVerId;
						index = 3;
					}
					foreach(DataRow addintdr in addintHmWt.Tables[0].Rows)
					{
						//if(lobId == "4")
							veh_id = addintdr["BOAT_ID"].ToString();
						addint_id = addintdr["ADD_INT_ID"].ToString();
						Addilname = addintdr["ADD_INT_NAME"].ToString();
						add_list[index] = "ADDLINT_" + Addilname + "_" +  custId + "_" + polId + "_" + polVerId + "_" + veh_id + "_" + addint_id;
						index ++;
					}
				}
			}

			if(flgdec==false)
			{
				chkAddl.Visible = false;
				chkAddlList.Visible = false;
				//add_list = new string[3];
				add_list[0] = "DEC_PAGE_NOWORD_" +  custId + "_" + polId + "_" + polVerId;
				add_list[1] = "DEC_PAGE_C_" +  custId + "_" + polId + "_" + polVerId;
				add_list[2] = "DEC_PAGE_A_" +  custId + "_" + polId + "_" + polVerId;
			}
		}

		private void FillDecList()
		{
			chkDecList.Items.Add("Client's Copy ");
			chkDecList.Items.Add("Client's Copy (Attached Forms & Endorsements)");
			chkDecList.Items.Add("Agent's Copy");
			chkDecList.Items[0].Selected = true;
			
			Cms.BusinessLayer.BlApplication.clsapplication objApp = new clsapplication();
			DataSet addintds= objApp.FetchAddIntDetails(custId, polId, polVerId, lobId, "POLICY");
			DataSet addintHmWt=objApp.FetchAddIntDetails(custId, polId, polVerId, "4", "POLICY");
			
			if(addintds != null && addintds.Tables[0].Rows.Count > 0)
			{
				foreach(DataRow addintdr in addintds.Tables[0].Rows)
				{
					if(lobId == "1" || lobId == "6")
						chkAddlList.Items.Add(addintdr["NAME_ADDRESS"].ToString());
					else
						chkAddlList.Items.Add(addintdr["HOLDER_NAME"].ToString());
				}
			}
			if(lobId=="1")
			{
				if(addintHmWt != null && addintHmWt.Tables[0].Rows.Count > 0)
				{
					foreach(DataRow addintdr in addintHmWt.Tables[0].Rows)
					{
						chkAddlList.Items.Add(addintdr["HOLDER_NAME"].ToString());
					}
				}
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
			this.chkDec.CheckedChanged += new System.EventHandler(this.chkDec_CheckedChanged);
			this.chkAddl.CheckedChanged += new System.EventHandler(this.chkAddl_CheckedChanged);
			this.btnFetchDec.ServerClick += new System.EventHandler(this.btnFetchDec_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnFetchDec_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string file_name = "";
				int dec_open = -1, aditi_open = -1;
				string url_path="";
				string agency_name ="";
				string url_path_final = "";
				string agency_id = GetSystemId();
				if (Request.QueryString["CALLEDAGENCY"] != null && Request.QueryString["CALLEDAGENCY"].ToString() != "")
				{
					agency_name =Request.QueryString["CALLEDAGENCY"].ToString();
					if(agency_name.EndsWith("."))
					{
						agency_name = agency_name.Substring(0,agency_name.Length-1);
					}
				}
				else
				{
					agency_name =GetSystemId().ToString();
					if(agency_name.EndsWith("."))
					{
						agency_name = agency_name.Substring(0,agency_name.Length-1);
					}
				}
				url_path_final = "/cms/Upload/OUTPUTPDFs/" + agency_name + "/" + custId + "/POLICY/final";

				if (Request.QueryString["CALLEDAGENCY"] != null && Request.QueryString["CALLEDAGENCY"].ToString() != "")
				{
					url_path_final = "/cms/Upload/OUTPUTPDFs/" + agency_name + "/" + custId + "/POLICY/final";
				}
				//added by pravesh on 29 april 2008 to check if dec page is genrated for the process
				DataTable objProcessTable;
				DataTable objPolicyStatusTable;
				string strProcessStatus="";
				string strPolicyStatus="";
				int intProcessID=0;
                string strCarrierSystemID = CarrierSystemID.ToUpper();//System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString().ToUpper();
				//added by pravesh on 29 april 2008 to check if dec page is genrated for the process
				Cms.BusinessLayer.BlCommon.ClsCommon objPolicyProcess=new Cms.BusinessLayer.BlCommon.ClsCommon();
				objProcessTable = objPolicyProcess.GetPolicyProcessInfo(int.Parse(custId),int.Parse(polId),int.Parse(polVerId));
				if (objProcessTable!=null && objProcessTable.Rows.Count>0)
				{
					intProcessID=int.Parse(objProcessTable.Rows[0]["PROCESS_ID"].ToString());
					strProcessStatus=objProcessTable.Rows[0]["PROCESS_STATUS"].ToString();
				}
				objPolicyStatusTable = objPolicyProcess.GetPolicyStatusInfo(int.Parse(custId),int.Parse(polId),int.Parse(polVerId));
				if (objPolicyStatusTable!=null && objPolicyStatusTable.Rows.Count>0)
				{
					strPolicyStatus=objPolicyStatusTable.Rows[0]["POLICY_STATUS"].ToString();
				}
				//end here
				objPolicyMaxVerTable = objPolicyProcess.GetPolicyMaxVerInfo(int.Parse(custId),int.Parse(polId),int.Parse(polVerId));
				if (objPolicyMaxVerTable!=null && objPolicyMaxVerTable.Rows.Count>0)
				{
					strPolicyMaxVer=objPolicyMaxVerTable.Rows[0]["NEW_POLICY_VERSION_ID"].ToString();
				}
				Cms.BusinessLayer.BlApplication.clsapplication objApp = new clsapplication();
				DataSet prnjobds= objApp.FetchPrnJobDetails(int.Parse(custId), int.Parse(polId), int.Parse(polVerId));
				if(prnjobds != null && prnjobds.Tables[0].Rows.Count > 0)
				{
					string filepath="";
					if(agency_id !=strCarrierSystemID.Trim())
					{
						if(intProcessID.ToString()=="29" || intProcessID.ToString()=="2" || intProcessID.ToString()=="12" || intProcessID.ToString()=="28")
						{
							Response.Write("<script language='javascript'> alert('Declaration Page is Not Available – See Prior Version.'); </script>");
							Response.Write("<script>window.close();</script>");
							return;
						}
						else
						{
//							foreach(DataRow prnjobdr in prnjobds.Tables[0].Rows)
//							{
								if(chkDecList.Items[0].Selected == true && add_list[0] != "")
								{
									if(dec_open != 1)
										dec_open = 0;
									foreach(DataRow prnjobdrcust in prnjobds.Tables[0].Rows)
									{
										url_path = prnjobdrcust["URL_PATH"].ToString();
										if(prnjobdrcust["ENTITY_TYPE"].ToString() == "CUSTOMER")
										{
											
											if(prnjobdrcust["ENTITY_TYPE"].ToString() == "CUSTOMER" && prnjobdrcust["FILE_NAME"].ToString().IndexOf(add_list[0]) > 0)
											{
												//Response.Write("<script language='javascript'> window.open('" + prnjobdrcust["URL_PATH"].ToString() + "/" + prnjobdrcust["FILE_NAME"].ToString() + "'); </script>");
												if(prnjobdrcust["URL_PATH"].ToString().IndexOf("/cms/Upload")>=0)
												{
													FilePath = prnjobdrcust["URL_PATH"].ToString().Replace("/cms/Upload","")+ "/" + prnjobdrcust["FILE_NAME"].ToString() ;
												}
												filepath= ClsCommon.CreateContentViewerURL(FilePath, FILE_TYPE_PDF ); 
												Response.Write("<script language='javascript'> window.open('"+ filepath+ "'); </script>");
												//Response.Redirect( ClsCommon.CreateContentViewerURL(FilePath, FILE_TYPE_PDF )); 
												add_list[0] = "";
												dec_open = 1;
											}
											else if((file_name = GetExistingPdf(url_path_final, add_list[0])) != "")
											{
												//Response.Write("<script language='javascript'> window.open('" + url_path_final + "/" + file_name + "'); </script>");
												if(url_path_final.IndexOf("/cms/Upload")>=0)
												{
													FilePath = url_path_final.Replace("/cms/Upload","")+ "/" + file_name ;
												}
												//Response.Redirect( ClsCommon.CreateContentViewerURL(FilePath, FILE_TYPE_PDF )) ; 
												filepath= ClsCommon.CreateContentViewerURL(FilePath, FILE_TYPE_PDF ); 
												Response.Write("<script language='javascript'> window.open('"+ filepath+ "'); </script>");
												add_list[0] = "";
												dec_open = 1;
											}
										}
									}
								}
								if(chkDecList.Items[1].Selected == true && add_list[1] != "")
								{
									if(dec_open != 1)
										dec_open = 0;
									foreach(DataRow prnjobdrcust in prnjobds.Tables[0].Rows)
									{
										url_path = prnjobdrcust["URL_PATH"].ToString();
										if(prnjobdrcust["ENTITY_TYPE"].ToString() == "CUSTOMER")
										{
											if(prnjobdrcust["ENTITY_TYPE"].ToString() == "CUSTOMER" && prnjobdrcust["FILE_NAME"].ToString().IndexOf(add_list[1]) > 0)
											{
												//Response.Write("<script language='javascript'> window.open('" + prnjobdrcust["URL_PATH"].ToString() + "/" + prnjobdrcust["FILE_NAME"].ToString() + "'); </script>");
												if(prnjobdrcust["URL_PATH"].ToString().IndexOf("/cms/Upload")>=0)
												{
													FilePath = prnjobdrcust["URL_PATH"].ToString().Replace("/cms/Upload","")+ "/" + prnjobdrcust["FILE_NAME"].ToString() ;
												}
												filepath= ClsCommon.CreateContentViewerURL(FilePath, FILE_TYPE_PDF ); 
												Response.Write("<script language='javascript'> window.open('"+ filepath+ "'); </script>");
												add_list[1] = "";
												dec_open = 1;
											}
											else if((file_name = GetExistingPdf(url_path_final, add_list[1])) != "")
											{
												if(url_path_final.IndexOf("/cms/Upload")>=0)
												{
													FilePath = url_path_final.Replace("/cms/Upload","")+ "/" + file_name ;
												}
												//Response.Write("<script language='javascript'> window.open('" + url_path_final + "/" + file_name + "'); </script>");
												filepath= ClsCommon.CreateContentViewerURL(FilePath, FILE_TYPE_PDF ); 
												Response.Write("<script language='javascript'> window.open('"+ filepath+ "'); </script>");
												
												add_list[1] = "";
												dec_open = 1;
											}
										}
									}
								}
								if(chkDecList.Items[2].Selected == true && add_list[2] != "")
								{
									if(dec_open != 1)
										dec_open = 0;
									foreach(DataRow prnjobdragy in prnjobds.Tables[0].Rows)
									{
										url_path = prnjobdragy["URL_PATH"].ToString();
										if(prnjobdragy["ENTITY_TYPE"].ToString() == "AGENCY")
										{
											if(prnjobdragy["ENTITY_TYPE"].ToString() == "AGENCY" && prnjobdragy["FILE_NAME"].ToString().IndexOf(add_list[2]) > 0)
											{
												//Response.Write("<script language='javascript'> window.open('" + prnjobdragy["URL_PATH"].ToString() + "/" + prnjobdragy["FILE_NAME"].ToString() + "'); </script>");
												if(prnjobdragy["URL_PATH"].ToString().IndexOf("/cms/Upload")>=0)
												{
													FilePath = prnjobdragy["URL_PATH"].ToString().Replace("/cms/Upload","")+ "/" + prnjobdragy["FILE_NAME"].ToString() ;
												}
												filepath= ClsCommon.CreateContentViewerURL(FilePath, FILE_TYPE_PDF ); 
												Response.Write("<script language='javascript'> window.open('"+ filepath+ "'); </script>");
												add_list[2] = "";
												dec_open = 1;
											}
											else if((file_name = GetExistingPdf(url_path_final, add_list[2])) != "")
											{
												if(url_path_final.IndexOf("/cms/Upload")>=0)
												{
													FilePath = url_path_final.Replace("/cms/Upload","")+ "/" + file_name ;
												}
												//Response.Write("<script language='javascript'> window.open('" + url_path_final + "/" + file_name + "'); </script>");
												filepath= ClsCommon.CreateContentViewerURL(FilePath, FILE_TYPE_PDF ); 
												Response.Write("<script language='javascript'> window.open('"+ filepath+ "'); </script>");
												add_list[2] = "";
												dec_open = 1;
											}
										}
									}
								}
								for(int index = 0; index < chkAddlList.Items.Count; index++)
								{
									if((chkAddlList.Items[index].Selected == true) && (add_list[index+3] != ""))
									{
										foreach(DataRow prnjobdradt in prnjobds.Tables[0].Rows)
										{
											int intfileOpenAgencyView=0;
											if(prnjobdradt["ENTITY_TYPE"].ToString() == "ADDL_INT")
											{
												if(prnjobdradt["ENTITY_TYPE"].ToString() == "ADDL_INT" && prnjobdradt["FILE_NAME"].ToString().IndexOf(add_list[index+3]) > 0)
												{
													if(prnjobdradt["URL_PATH"].ToString().IndexOf("/cms/Upload")>=0)
													{
														FilePath = prnjobdradt["URL_PATH"].ToString().Replace("/cms/Upload","")+ "/" + prnjobdradt["FILE_NAME"].ToString() ;
													}
													filepath= ClsCommon.CreateContentViewerURL(FilePath, FILE_TYPE_PDF ); 
													aditi_open=1;
													intfileOpenAgencyView=1;
													Response.Write("<script language='javascript'> window.open('"+ filepath+ "'); </script>");
												
													//	Response.Write("<script language='javascript'> window.open('" + prnjobdradt["URL_PATH"].ToString() + "/" + prnjobdradt["FILE_NAME"].ToString() + "'); </script>");
												}
											}
											else if((file_name = GetExistingPdf(url_path_final, add_list[index+3])) != "")
											{
												//	Response.Write("<script language='javascript'> window.open('" + url_path_final + "/" + file_name + "'); </script>");
												if(url_path_final.IndexOf("/cms/Upload")>=0)
												{
													FilePath = url_path_final.Replace("/cms/Upload","")+ "/" + file_name ;
												}
												filepath =  ClsCommon.CreateContentViewerURL(FilePath, FILE_TYPE_PDF ) ; 
												aditi_open=1;
												intfileOpenAgencyView=1;
												Response.Write("<script language='javascript'> window.open('" +filepath+ "'); </script>");
											}
											if(intfileOpenAgencyView==1)
													add_list[index+3] = "";
											
										}
									}
								}
							//}
						}
					}
					else
					{
//						foreach(DataRow prnjobdr in prnjobds.Tables[0].Rows)
//						{
							
							if(chkDecList.Items[0].Selected == true && add_list[0] != "")
							{
								
								if(dec_open != 1)
									dec_open = 0;
								foreach(DataRow prnobdrcust in prnjobds.Tables[0].Rows)
								{
									
									url_path = prnobdrcust["URL_PATH"].ToString();
									if(prnobdrcust["ENTITY_TYPE"].ToString() == "CUSTOMER")
									{
										if(prnobdrcust["ENTITY_TYPE"].ToString() == "CUSTOMER" && prnobdrcust["FILE_NAME"].ToString().IndexOf(add_list[0]) > 0)
										{
											//Response.Write("<script language='javascript'> window.open('" + prnobdrcust["URL_PATH"].ToString() + "/" + prnobdrcust["FILE_NAME"].ToString() + "'); </script>");
											if(prnobdrcust["URL_PATH"].ToString().IndexOf("/cms/Upload")>=0)
											{
												FilePath = prnobdrcust["URL_PATH"].ToString().Replace("/cms/Upload","")+ "/" + prnobdrcust["FILE_NAME"].ToString() ;
											}
											filepath= ClsCommon.CreateContentViewerURL(FilePath, FILE_TYPE_PDF ); 
											Response.Write("<script language='javascript'> window.open('"+ filepath+ "'); </script>");
											add_list[0] = "";
											dec_open = 1;
										}
										else if((file_name = GetExistingPdf(url_path_final, add_list[0])) != "")
										{
										//	Response.Write("<script language='javascript'> window.open('" + url_path_final + "/" + file_name + "'); </script>");
											if(url_path_final.IndexOf("/cms/Upload")>=0)
											{
												FilePath = url_path_final.Replace("/cms/Upload","")+ "/" + file_name ;
											}
											filepath =  ClsCommon.CreateContentViewerURL(FilePath, FILE_TYPE_PDF ) ; 
											Response.Write("<script language='javascript'> window.open('"+ filepath+ "'); </script>");
											add_list[0] = "";
											dec_open = 1;
										}
									}
								}
							}
							if(chkDecList.Items[1].Selected == true && add_list[1] != "")
							{
								if(dec_open != 1)
									dec_open = 0;
								
								foreach(DataRow prnobdrcust in prnjobds.Tables[0].Rows)
								{
									filepath="";
									url_path = prnobdrcust["URL_PATH"].ToString();
									if(prnobdrcust["ENTITY_TYPE"].ToString() == "CUSTOMER")
									{
										if(prnobdrcust["ENTITY_TYPE"].ToString() == "CUSTOMER" && prnobdrcust["FILE_NAME"].ToString().IndexOf(add_list[1]) > 0)
										{
											//Response.Write("<script language='javascript'> window.open('" + prnobdrcust["URL_PATH"].ToString() + "/" + prnobdrcust["FILE_NAME"].ToString() + "'); </script>");
											if(prnobdrcust["URL_PATH"].ToString().IndexOf("/cms/Upload")>=0)
											{
												FilePath = prnobdrcust["URL_PATH"].ToString().Replace("/cms/Upload","")+ "/" + prnobdrcust["FILE_NAME"].ToString() ;
											}
											filepath =   ClsCommon.CreateContentViewerURL(FilePath, FILE_TYPE_PDF ); 
											Response.Write("<script language='javascript'> window.open('"+ filepath+ "'); </script>");
											add_list[1] = "";
											dec_open = 1;
										}
										else if((file_name = GetExistingPdf(url_path_final, add_list[1])) != "")
										{
											if(url_path_final.IndexOf("/cms/Upload")>=0)
											{
												FilePath = url_path_final.Replace("/cms/Upload","")+ "/" + file_name ;
											}
											filepath =  ClsCommon.CreateContentViewerURL(FilePath, FILE_TYPE_PDF ) ; 
											Response.Write("<script language='javascript'> window.open('" +filepath+ "'); </script>");
											//Response.Write("<script language='javascript'> window.open('" + url_path_final + "/" + file_name + "'); </script>");
											add_list[1] = "";
											dec_open = 1;
										}
									}
								}
							}
							if(chkDecList.Items[2].Selected == true && add_list[2] != "")
							{
								if(dec_open != 1)
									dec_open = 0;
								filepath="";
								foreach(DataRow prnobdragy in prnjobds.Tables[0].Rows)
								{
									url_path = prnobdragy["URL_PATH"].ToString();
									if(prnobdragy["ENTITY_TYPE"].ToString() == "AGENCY")
									{
										if(prnobdragy["ENTITY_TYPE"].ToString() == "AGENCY" && prnobdragy["FILE_NAME"].ToString().IndexOf(add_list[2]) > 0)
										{
											//Response.Write("<script language='javascript'> window.open('" + prnobdragy["URL_PATH"].ToString() + "/" + prnobdragy["FILE_NAME"].ToString() + "'); </script>");
											if(prnobdragy["URL_PATH"].ToString().IndexOf("/cms/Upload")>=0)
											{
												FilePath = prnobdragy["URL_PATH"].ToString().Replace("/cms/Upload","")+ "/" + prnobdragy["FILE_NAME"].ToString() ;
											}
											filepath= ClsCommon.CreateContentViewerURL(FilePath, FILE_TYPE_PDF ); 
											Response.Write("<script language='javascript'> window.open('"+ filepath+ "'); </script>");
											add_list[2] = "";
											dec_open = 1;
										}
										else if((file_name = GetExistingPdf(url_path_final, add_list[2])) != "")
										{
											//Response.Write("<script language='javascript'> window.open('" + url_path_final + "/" + file_name + "'); </script>");
											if(url_path_final.IndexOf("/cms/Upload")>=0)
											{
												FilePath = url_path_final.Replace("/cms/Upload","")+ "/" + file_name ;
											}
											filepath =  ClsCommon.CreateContentViewerURL(FilePath, FILE_TYPE_PDF ) ; 
											Response.Write("<script language='javascript'> window.open('" +filepath+ "'); </script>");
										
											add_list[2] = "";
											dec_open = 1;
										}
									}
								}
							}
							for(int index = 0; index < chkAddlList.Items.Count; index++)
							{
								if((chkAddlList.Items[index].Selected == true) && (add_list[index+3] != ""))
								{
									filepath="";
									foreach(DataRow prnobdradit in prnjobds.Tables[0].Rows)
									{
										int intfileOpen=0;
										if(prnobdradit["ENTITY_TYPE"].ToString() == "ADDL_INT")
										{
											if(prnobdradit["ENTITY_TYPE"].ToString() == "ADDL_INT" && prnobdradit["FILE_NAME"].ToString().IndexOf(add_list[index+3]) > 0)
											{
												if(prnobdradit["URL_PATH"].ToString().IndexOf("/cms/Upload")>=0)
												{
													FilePath = prnobdradit["URL_PATH"].ToString().Replace("/cms/Upload","")+ "/" + prnobdradit["FILE_NAME"].ToString() ;
												}
												filepath= ClsCommon.CreateContentViewerURL(FilePath, FILE_TYPE_PDF ); 
												aditi_open=1;
												intfileOpen=1;
												Response.Write("<script language='javascript'> window.open('"+ filepath+ "'); </script>");
												//Response.Write("<script language='javascript'> window.open('" + prnobdradit["URL_PATH"].ToString() + "/" + prnobdradit["FILE_NAME"].ToString() + "'); </script>");
											}
										}
										else if((file_name = GetExistingPdf(url_path_final, add_list[index+3])) != "")
										{
											//Response.Write("<script language='javascript'> window.open('" + url_path_final + "/" + file_name + "'); </script>");
											if(url_path_final.IndexOf("/cms/Upload")>=0)
											{
												FilePath = url_path_final.Replace("/cms/Upload","")+ "/" + file_name ;
											}
											filepath =  ClsCommon.CreateContentViewerURL(FilePath, FILE_TYPE_PDF ) ; 
											aditi_open=1;
											intfileOpen=1;
											Response.Write("<script language='javascript'> window.open('" +filepath+ "'); </script>");
										}
										if(intfileOpen==1)
                                            add_list[index+3] = "";
									}									
									
								}
							}
						//}
					}
					if(dec_open == 0)
					{
						if((file_name = GetExistingPdf(url_path, add_list[0])) != "")
							Response.Write("<script language='javascript'> window.open('" + url_path + "/" + file_name + "'); </script>");
						else
						{
							//change by pravesh on 29 april 08
							if(intProcessID.ToString()=="37" ||intProcessID.ToString()=="29"  ) //Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REVERT_PROCESS || intProcessID==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RESCIND_PROCESS
							{
								if(agency_id !=strCarrierSystemID.Trim() && intProcessID.ToString()=="29")
								{
									Response.Write("<script language='javascript'> alert('Declaration Page is Not Available – See Prior Version.'); </script>");
								}
								else
								{
									Response.Write("<script language='javascript'> alert('Declaration Page is not available for this version.'); </script>");
								}
							}
							else if(strProcessStatus=="PENDING") //Cms.BusinessLayer.BlCommon.PROCESS_STATUS_PENDING
							{
								if(agency_id !=strCarrierSystemID.Trim())
								{
									if(intProcessID.ToString()=="2" || intProcessID.ToString()=="12" || intProcessID.ToString()=="28" || strPolicyStatus=="EXPIRED")
									{
										Response.Write("<script language='javascript'> alert('Declaration Page is Not Available – See Prior Version.'); </script>");
									}
									else
									{
										Response.Write("<script language='javascript'> alert('This version of the policy is In Progress and has not been committed. Declaration Page is not available at this time.'); </script>");
									}
								}
								else
								{
									Response.Write("<script language='javascript'> alert('This version of the policy is In Progress and has not been committed. Declaration Page is not available at this time.'); </script>");
								}
							}
							else if(agency_id !=strCarrierSystemID.Trim())
							{
								if(intProcessID.ToString()=="2" || intProcessID.ToString()=="12" || intProcessID.ToString()=="28")
								{
									Response.Write("<script language='javascript'> alert('Declaration Page is Not Available – See Prior Version.'); </script>");
									Response.Write("<script>window.close();</script>");
								}
								else
								{
									Response.Write("<script language='javascript'> alert('Declaration Page is not available at this time.'); </script>");
									Response.Write("<script>window.close();</script>");
								}
							}
							else
							{
								Response.Write("<script language='javascript'> alert('Declaration Page is not available at this time.'); </script>");
							}

						}
					}
					else if(dec_open == -1 && aditi_open == -1)
					{
						Response.Write("<script language='javascript'> alert('Please select atleast one option for Dec Page.'); </script>");						      
					}

				}
				else
				{

					//Forloop added  For Itrack Issue #6233.
					bool flgchecked = false;
					for(int check = 0;check < chkAddlList.Items.Count;check++)
					{
						if(chkAddlList.Items[check].Selected == true)
						{
							flgchecked = true;
						}

					}
					
					//Checkbox checked Condition Added For Itrack Issue #6233. 
					if(chkDecList.Items[0].Selected == true || chkDecList.Items[1].Selected == true  || chkDecList.Items[2].Selected == true || flgchecked == true)
					{  
						string querystring = "&CUSTOMER_ID=" + custId + "&POLICY_ID=" + polId + "&POLICY_VER_ID=" + polVerId;
						string decpageurl = "../../application/aspx/DecPage.aspx?CalledFrom=POLICY&CALLEDFOR=DECPAGE&LOB_ID=" + lobId + querystring;
						Response.Write("<script language='javascript'> window.open('" + decpageurl + "'); </script>");
						if(agency_id !=strCarrierSystemID.Trim())
						{
							if(polVerId!="" && strPolicyMaxVer!="")
							{
								if(Convert.ToInt32(polVerId) < Convert.ToInt32(strPolicyMaxVer))
								{
									Response.Write("<script>window.close();</script>");
								}
							}
							if(intProcessID.ToString()=="29" || intProcessID.ToString()=="2" || intProcessID.ToString()=="12" || intProcessID.ToString()=="28" || strProcessStatus=="PENDING")
							{
								Response.Write("<script>window.close();</script>");
							}
						}
					}
						//Else  Condition Added For Itrack Issue #6233. 
					else
					{
						Response.Write("<script language='javascript'> alert('Please select atleast one option for Dec Page.'); </script>");						      
					}
				}
			}
			catch(Exception ex)
			{
				Response.Write("<script language='javascript'> alert('Exception occurred: " + ex.Message + "'); </script>");
			}
		
		}
				
		//Added by Mohit Agarwal 31-Jan-2007
		// Check if pdf already generated in final folder
		public string GetExistingPdf(string strpath, string strnameBegin)
		{
			try
			{
				Cms.BusinessLayer.BlCommon.ClsAttachment objAttachment =  new Cms.BusinessLayer.BlCommon.ClsAttachment();
				string strImpersonationUserId="",strImpersonationPassword="",strImpersonationDomain="";
				//AcordPDF.ImpersonateWrapper objEbixImpersonatePDF = new AcordPDF.ImpersonateWrapper();
				strImpersonationUserId = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName").ToString().Trim();
                strImpersonationPassword = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd").ToString().Trim();
                strImpersonationDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain").ToString().Trim();
				objAttachment.ImpersonateUser(strImpersonationUserId, strImpersonationPassword, strImpersonationDomain);
				
				string FilePath = Server.MapPath(strpath+"/temp");

				FileInfo finfo = new FileInfo(FilePath);

				DirectoryInfo dinfo = finfo.Directory;

				FileSystemInfo[] fsinfo = dinfo.GetFiles();

				foreach (FileSystemInfo info in fsinfo)
				{
					//if(info.Name.StartsWith(strnameBegin))
					if(info.Name.IndexOf(strnameBegin) > 0)
					{
						objAttachment.endImpersonation();
						return info.Name;
					}
				}
				objAttachment.endImpersonation();
				return "";
			}
			catch//(Exception ex)
			{
				return "";
			}
		}

		private void chkDec_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkDec.Checked == true)
			{
				for(int chkindex = 0; chkindex < chkDecList.Items.Count; chkindex++)
					chkDecList.Items[chkindex].Selected = true;
				chkAddl.Checked = true;
			}
			if(chkDec.Checked == false)
			{
				for(int chkindex = 0; chkindex < chkDecList.Items.Count; chkindex++)
					chkDecList.Items[chkindex].Selected = false;
				chkAddl.Checked = false;
			}
			chkAddl_CheckedChanged(null, null);
		}

		private void chkAddl_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkAddl.Checked == true)
			{
				for(int chkindex = 0; chkindex < chkAddlList.Items.Count; chkindex++)
					chkAddlList.Items[chkindex].Selected = true;
			}
			if(chkAddl.Checked == false)
			{
				for(int chkindex = 0; chkindex < chkAddlList.Items.Count; chkindex++)
					chkAddlList.Items[chkindex].Selected = false;
			}
		}

	}
}
