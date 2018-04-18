/******************************************************************************************
	<Author					: - > Anurag Verma
	<Start Date				: -	> 15/04/2005
	<End Date				: - > 
	<Description			: - > This file is created to perform transfer diary entry to other users
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - > 24/06/2005
	<Modified By			: - > Anurag Verma
	<Purpose				: - > updating notes and followupdate as diary get transferred
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
using Cms.BusinessLayer.BlCommon;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Diary;  
using Cms.DataLayer;
using System.Resources;
using System.Reflection;


namespace Cms.CmsWeb.diary
{
	/// <summary>
	/// This class is inherited from cmsbase and is created to perform the transfer diary entry
	/// </summary>
	public class TransferDiaryEntry : Cms.CmsWeb.cmsbase  
	{
        #region CONTROL REFERENCE
            protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.ListBox cmbToUserId;
            protected System.Web.UI.WebControls.TextBox txtFollowUpDate;
            protected System.Web.UI.WebControls.HyperLink hlkCalandarDate;
            protected System.Web.UI.WebControls.RequiredFieldValidator rfvFollowUpDate;
            protected System.Web.UI.WebControls.RegularExpressionValidator revFollowUpDate;
            protected System.Web.UI.WebControls.TextBox txtNote;
            protected Cms.CmsWeb.Controls.CmsButton btnClose;
            protected Cms.CmsWeb.Controls.CmsButton btnSave;
            protected System.Web.UI.WebControls.Label capHeader;
            protected System.Web.UI.WebControls.Label capMandatory;
            protected System.Web.UI.WebControls.Label capTo;
            protected System.Web.UI.WebControls.Label capFollow;
            protected System.Web.UI.WebControls.Label capNote;
            protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
            public string ts;
            public string Alert;
           
        #endregion 

        #region GLOBAL OBJECT
            ClsDiary objDiary	= new ClsDiary();		//holds the diary object to save the Diary entry]            
            private int			intFromUserId;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidRowId;//holds the value of the user id adding entry in data(loggedin user)
            private int         intRowId;      // integer variable to store list id  
        #endregion 

		protected string gStrEEC="0", gStrQPAC="0", gStrQPBC="0", gStrRRC="0", gStrBRC="0", gStrERC="0", gStrAAC="0", cStrCRE = "0", cStrANF = "0", cStrCF = "0";
		protected string gStrAppDates="";
		protected string listtypeid1="",listtypeid2="",listtypeid3="",listtypeid4="",listtypeid5="",listtypeid6="",listtypeid7="",listtypeid8="",listtypeid9="",listtypeid10="";
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected string strCalledFrom="";
		DataSet dsEntry=new DataSet();
        System.Resources.ResourceManager objResourceMgr;
        private const string CALLED_FROM_INCLIENT = "INCLT";
        private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			btnSave.Attributes.Add("onclick","return checkToUserID();");  
			if(Request.QueryString["calledfrom"]!=null && Request.QueryString["calledfrom"]!="")
			{
				strCalledFrom = Request.QueryString["calledfrom"].ToString();
			}
			if(strCalledFrom=="InCLT")
			{
				base.ScreenId ="120_5_0_0";
			}
			else
			{
				base.ScreenId ="1_0_0";
			}
            objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.diary.TransferDiaryEntry", System.Reflection.Assembly.GetExecutingAssembly());
            capMandatory.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

			btnClose.Attributes.Add ("onclick","javascript:window.close();");
            intFromUserId				    =	int.Parse(GetUserId());
            if(Request.QueryString["listid"].ToString()!="")
            {
                intRowId                    =   int.Parse(Request.QueryString["listid"].ToString());  
                hidRowId.Value              =   intRowId.ToString() ; 
            }   
			
            #region SETTING PERMISSION
				btnClose.PermissionString	=	gstrSecurityXML;
				btnClose.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Read;    		
                btnSave.PermissionString	=	gstrSecurityXML;
                btnSave.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Write;    		
            #endregion    

            #region code for calendar picker                
                hlkCalandarDate.Attributes.Add("OnClick","fPopCalendar(document.Form1.txtFollowUpDate,document.Form1.txtFollowUpDate)"); //Javascript Implementation for Calender				
            #endregion

            if(!Page.IsPostBack)
            {
				dsEntry=objDiary.FetchData(intRowId); 
				SetControls(dsEntry);
                PopulateForm(dsEntry);
                SetCaptions();
            }

		}
        
        private void PopulateForm(DataSet dsEntry)
        {
            
            if(dsEntry!=null)
                if(dsEntry.Tables[0].Rows.Count > 0  )
                {
                    txtNote.Text=   dsEntry.Tables[0].Rows[0]["Note"].ToString();
                   var date= dsEntry.Tables[0].Rows[0]["followupdate"].ToString();
                   txtFollowUpDate.Text = ConvertToDate(date).ToShortDateString();
                }                

        }
        private void SetCaptions()
        {
            capHeader.Text = objResourceMgr.GetString("capHeader");
            capFollow.Text = objResourceMgr.GetString("capFollow");
            capTo.Text = objResourceMgr.GetString("capTo");
            capNote.Text = objResourceMgr.GetString("capNote");
            btnClose.Text = objResourceMgr.GetString("btnClose");
            btnSave.Text = objResourceMgr.GetString("btnSave");
            ts = objResourceMgr.GetString("ts");
            Alert = objResourceMgr.GetString("Alert");
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            int toUserId=-1; // variable to store new user id to which diary entry is going to be transferred
            try
            {
                 toUserId                =   int.Parse(cmbToUserId.SelectedItem.Value);    
                 string notes            =   txtNote.Text.Trim();
                 string followDate       =   txtFollowUpDate.Text.Trim();   
				 //int UserId			 = 	int.Parse(Request.QueryString["calUser"]);
				 int UserId				 =  int.Parse(GetUserId());
				 string Customer		 =	Request.QueryString["calCustomer"];
				 string AppId="";
				 string AppVersionid=""; 
				 string PolId="" ;
				 string PolVersionid="" ;
				if(Request.QueryString["calAPP_ID"]!="")
				{
					 AppId			 =  Request.QueryString["calAPP_ID"];
				}
				if(Request.QueryString["calAPP_VERSION_ID"]!="")
				{
					 AppVersionid     =  Request.QueryString["calAPP_VERSION_ID"];
				}
				if(Request.QueryString["calPOL_ID"]!="")
				{
					 PolId			 =  Request.QueryString["calPOL_ID"];
				}
				if(Request.QueryString["calPOL_VERSION_ID"]!="")
				{
					 PolVersionid     =  Request.QueryString["calPOL_VERSION_ID"];
				}
				 int result;
				
				 //calling method of clsDiary method
                //int result=objDiary.TransferDiaryEntry(intRowId,toUserId,notes,followDate);  
				if(Customer!="")
					result=objDiary.TransferDiaryEntry(intRowId,toUserId,notes,followDate,UserId,Customer,AppId,AppVersionid,PolId,PolVersionid);  
				else
					result=objDiary.TransferDiaryEntry(intRowId,toUserId,notes,followDate);  
               
                if(result>0)
                {
                    lblMessage.Text			=	ClsMessages.GetMessage(ScreenId,"87");
                    //hidRowId.Value			=	hidRowId.Value;
                    hidFormSaved.Value		=	"1";
                    //hidListOpen.Value       =   "Y";
					SetCountVariables();
					SetDiaryDates();
                    
                }
                else
                {
                    lblMessage.Text			=	ClsMessages.GetMessage(ScreenId,"20");
                    //hidFormSaved.Value		=	"2";
                }
                lblMessage.Visible = true;
            }
            catch(Exception ex)
            {
                
                lblMessage.Text			=	ClsMessages.GetMessage(ScreenId,"21")
                    + ex.Message + "\n Try again!";
					
                //hidFormSaved.Value			=	"2";
                lblMessage.Visible		=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
                if(objDiary != null) 
                    objDiary.Dispose();
            }

        }

        private void SetControls(DataSet dsEntry)
        {
			int mID=0,i=0;
			string USERGROUP_ID="",USERLIST_ID="";
			DataSet dsUserList=ClsDiary.GetUserListforDiaryEntry(intRowId,out mID);
			DataWrapper objDataWrapper = new DataWrapper(ClsCommon.ConnStr,CommandType.StoredProcedure);
			TodolistInfo objToDo=new TodolistInfo(); 
			ClsDiary objDiary=new ClsDiary();
			ArrayList algrp=new ArrayList(); 
			ArrayList userList=new ArrayList(); 

			if(dsUserList!=null)
			{
				if(dsUserList.Tables.Count>0)
				{
					if(dsUserList.Tables[0].Rows.Count>0  )
					{
						USERGROUP_ID=dsUserList.Tables[0].Rows[0]["MDD_USERGROUP_ID"]==null?"":dsUserList.Tables[0].Rows[0]["MDD_USERGROUP_ID"].ToString(); 						
						USERLIST_ID=dsUserList.Tables[0].Rows[0]["MDD_USERLIST_ID"]==null?"":dsUserList.Tables[0].Rows[0]["MDD_USERLIST_ID"].ToString(); 						
					}
				}


				if(dsEntry!=null)
					if(dsEntry.Tables.Count>0)
						if(dsEntry.Tables[0].Rows.Count>0)
						{
							objToDo.CUSTOMER_ID			=	dsEntry.Tables[0].Rows[0]["CUSTOMER_ID"]==System.DBNull.Value?0:int.Parse(dsEntry.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());     	
							objToDo.POLICY_ID			=	dsEntry.Tables[0].Rows[0]["POLICY_ID"]==System.DBNull.Value?0:int.Parse(dsEntry.Tables[0].Rows[0]["POLICY_ID"].ToString());     	
							objToDo.POLICY_VERSION_ID	=	dsEntry.Tables[0].Rows[0]["POLICY_VERSION_ID"]==System.DBNull.Value?0:int.Parse(dsEntry.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());     	
							objToDo.CLAIMID				=	dsEntry.Tables[0].Rows[0]["CLAIM_ID"]==System.DBNull.Value?0:int.Parse(dsEntry.Tables[0].Rows[0]["CLAIM_ID"].ToString());     	

							objToDo.MODULE_ID			=	dsEntry.Tables[0].Rows[0]["MODULE_ID"]==System.DBNull.Value?0:int.Parse(dsEntry.Tables[0].Rows[0]["MODULE_ID"].ToString());     	
						}


			
				
				string newList="";
				string []arList=USERGROUP_ID.Split(new char[]{','});
				string []arUList=USERLIST_ID.Split(new char[]{','});
				ArrayList colName=new ArrayList();
			


				for(i=0;i<arList.Length;i++) 
					newList += "'" + arList[i] + "',";

				if(newList.LastIndexOf(",")!=-1) 
				{	
					newList = newList.Substring(0,(newList.Length-1));
				}

				#region FINDING USER LIST TO WHOM DIARY ENTRY HAVE BEEN SENT
				Hashtable hColName=objDiary.ReadingXMlForDiary(objToDo.MODULE_ID); 
				userList=objDiary.CreateQueryfordiaryDetails(USERGROUP_ID,hColName, objToDo.MODULE_ID,objToDo,ref colName, objDataWrapper);

				if(arUList.Length>0 )
				{
					for(i=0;i<arUList.Length;i++)
					{
						userList.Add(arUList[i]); 
					}
				}
					
				
				#endregion

				#region FETCHING USER TYPE DETAILS
				//fetching user type details according to user group selection from the maintenance
				DataSet dsUsertpDt=new	DataSet();
				dsUsertpDt=objDiary.getUsertypeDetails(newList) ; 
				
				if(dsUsertpDt!=null)
				{
					if(dsUsertpDt.Tables.Count>0)
					{
						for(i=0;i<dsUsertpDt.Tables[0].Rows.Count;i++)
							{
							if (!colName.Contains(dsUsertpDt.Tables[0].Rows[i]["USER_TYPE_DESC"].ToString()))	
								colName.Add(dsUsertpDt.Tables[0].Rows[i]["USER_TYPE_DESC"].ToString()); 	
							}	
					}
				}
				#endregion

				DataSet dsUserDrp=new	DataSet();
				dsUserDrp=objDiary.getUsersName() ; 

				string grpDesc="";
				int iRow=0;

				//populating dropdown list and grouping first according to user group and then individual users
				if(dsUserDrp!=null)
				{
					
					#region POPULATING USERS OF USER GROUPS 
					if(dsUserDrp.Tables.Count>0)
					{
						grpDesc="";
						for(i=0;i<colName.Count;i++)
						//for(i=0;i<dsUsertpDt.Tables[0].Rows.Count;i++)						
						{
							DataRow []objDRow=dsUserDrp.Tables[0].Select("USER_TYPE_DESC='" + colName[i] + "'");   
							if(objDRow!=null)
							{
								ListItem li=new ListItem();
								li.Text = "--" + colName[i] + "--";
								li.Value="-1";
								cmbToUserId.Items.Add(li);  

								for(iRow=0;iRow<objDRow.Length;iRow++)
								{
//									if(!grpDesc.ToUpper().Equals(objDRow[iRow]["USER_TYPE_DESC"].ToString().ToUpper()))
//									{
//										grpDesc=objDRow[iRow]["USER_TYPE_DESC"].ToString().ToUpper(); 
//										ListItem li=new ListItem();
//										li.Text = "--" + objDRow[iRow]["USER_TYPE_DESC"].ToString().ToUpper() + "--";
//										li.Value="-1";
//										cmbToUserId.Items.Add(li);  
//
//										ListItem li1=new ListItem();
//										li1.Text = objDRow[iRow]["USER_NAME"].ToString();
//										li1.Value=  objDRow[iRow]["USER_id"].ToString();
//										cmbToUserId.Items.Add(li1);  
//									}
//									else
//									{
//										grpDesc=objDRow[iRow]["USER_TYPE_DESC"].ToString().ToUpper(); 

										ListItem li1=new ListItem();
										li1.Text = objDRow[iRow]["USER_NAME"].ToString();
										li1.Value=  objDRow[iRow]["USER_id"].ToString();
										cmbToUserId.Items.Add(li1);  
//									}
								}

							}
						}

					}
					#endregion

					#region POPULATING INDIVIDUAL USERS
					if(dsUserDrp.Tables.Count>0)
					{
						grpDesc="";
						if(colName.Count>0 )
						//if(dsUsertpDt.Tables[0].Rows.Count > 0)	
						{
							for(i=0;i<colName.Count;i++)
							//for(i=0;i<dsUsertpDt.Tables[0].Rows.Count;i++)	
							{
								DataRow []objDRow=dsUserDrp.Tables[0].Select("USER_TYPE_DESC<>'" + colName[i] + "'");   
								if(objDRow!=null)
								{
									for(iRow=0;iRow<objDRow.Length;iRow++)
									{
										if(!grpDesc.ToUpper().Equals("-- Individual Users --".ToUpper()) )
										{
											grpDesc="-- Individual Users --";
											ListItem li=new ListItem();
                                            li.Text = "-- Individual Users --";
											li.Value="-1";
											cmbToUserId.Items.Add(li);  

											ListItem li1=new ListItem();
											li1.Text = objDRow[iRow]["USER_NAME"].ToString();
											li1.Value=  objDRow[iRow]["USER_id"].ToString();
											cmbToUserId.Items.Add(li1);  
										}
										else
										{
											grpDesc="-- Individual Users --";

											ListItem li1=new ListItem();
											li1.Text = objDRow[iRow]["USER_NAME"].ToString();
											li1.Value=  objDRow[iRow]["USER_id"].ToString();
											cmbToUserId.Items.Add(li1);  
										}
									}

								}
							}//end of for loop of user group array list 
						}//end of user group column name count check 
						else
						{
							// if no user group is selected then show all the users
							cmbToUserId.DataSource = dsUserDrp;
							cmbToUserId.DataTextField = "USER_NAME";
							cmbToUserId.DataValueField = "USER_id";
							cmbToUserId.DataBind();
 
							cmbToUserId.Items.Insert(0,Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1888"));
							cmbToUserId.Items[0].Value = "-1";
						}

					}
					#endregion
				}				
			}
			else
			{
				cmbToUserId.DataSource		=	ClsCommon.GetUserList();
				cmbToUserId.DataTextField	=	USERNAME;
				cmbToUserId.DataValueField	=	USERID;

				cmbToUserId.DataBind();
			}

			#region LIST ITEM SELECTION IN TO USER LIST -- DO NOT DELETE
//			for(i=0;i<userList.Count;i++)
//			{
//				ListItem li2=new ListItem(); 
//				li2=cmbToUserId.Items.FindByValue(userList[i].ToString());   
//				if(li2!=null)
//				    li2.Selected=true; 				
//			}
			#endregion

            rfvFollowUpDate.ErrorMessage				=	ClsMessages.GetMessage(ScreenId, "42");
            revFollowUpDate.ValidationExpression		=	aRegExpDate;
            revFollowUpDate.ErrorMessage				=	ClsMessages.GetMessage(ScreenId, "22");	          
        }

		private void SetCountVariables()
		{
			objDiary		=	new ClsDiary();
			int UserId		=	int.Parse(Request.QueryString["calUser"]);
			string Customer	=	Request.QueryString["calCustomer"];
            if (strCalledFrom.ToUpper() != CALLED_FROM_INCLIENT)
                Customer = "";
			DataSet lObjDs	=	objDiary.GetCountListType(UserId,Customer);
			foreach(DataRow lRowJoin in lObjDs.Tables[0].Rows)
			{
				if(lRowJoin["listtypeid"].ToString().Equals("1"))
				{
					gStrEEC=lRowJoin["Counting"].ToString();
					listtypeid1=lRowJoin["listtypeid"].ToString();
				}
				
				if(lRowJoin["listtypeid"].ToString().Equals("3"))
				{
					gStrQPAC=lRowJoin["Counting"].ToString();
					listtypeid2=lRowJoin["listtypeid"].ToString();
				}
				if(lRowJoin["listtypeid"].ToString().Equals("4"))
				{
					gStrQPBC=lRowJoin["Counting"].ToString();
					listtypeid3=lRowJoin["listtypeid"].ToString();
				}
				if(lRowJoin["listtypeid"].ToString().Equals("2"))
				{
					gStrRRC=lRowJoin["Counting"].ToString();
					listtypeid4=lRowJoin["listtypeid"].ToString();
				}
				if(lRowJoin["listtypeid"].ToString().Equals("7"))
				{
					gStrBRC=lRowJoin["Counting"].ToString();
					listtypeid5=lRowJoin["listtypeid"].ToString();
				}
				if(lRowJoin["listtypeid"].ToString().Equals("8"))
				{
					gStrERC=lRowJoin["Counting"].ToString();
					listtypeid6=lRowJoin["listtypeid"].ToString();
				}
				if(lRowJoin["listtypeid"].ToString().Equals("6"))
				{
					gStrAAC=lRowJoin["Counting"].ToString();
					listtypeid7=lRowJoin["listtypeid"].ToString();
				}
				if(lRowJoin["listtypeid"].ToString().Equals("9"))
				{
					cStrCF=lRowJoin["Counting"].ToString();
					listtypeid8=lRowJoin["listtypeid"].ToString();
				}
				if(lRowJoin["listtypeid"].ToString().Equals("10"))
				{
					cStrANF=lRowJoin["Counting"].ToString();
					listtypeid9=lRowJoin["listtypeid"].ToString();
				}
				if(lRowJoin["listtypeid"].ToString().Equals("11"))
				{
					cStrCRE=lRowJoin["Counting"].ToString();
					listtypeid10=lRowJoin["listtypeid"].ToString();
				}
			}
			objDiary.Dispose();
		}

		private void SetDiaryDates()
		{
			objDiary		=	new ClsDiary();
			int UserId		=	int.Parse(Request.QueryString["calUser"]);
			string Customer	=	Request.QueryString["calCustomer"];
            if (strCalledFrom.ToUpper() != CALLED_FROM_INCLIENT)
                Customer = "";
			gStrAppDates	=	objDiary.SetDiaryDates(UserId,Customer);
			objDiary.Dispose();
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
