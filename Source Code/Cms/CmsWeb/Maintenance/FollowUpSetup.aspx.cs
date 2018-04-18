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
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Maintenance; 
using Cms.Model.Diary;
using System.Resources;
using System.Reflection;  

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for FollowUpSetup.
	/// </summary>
	public class FollowUpSetup : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label lblMess;
		protected System.Web.UI.WebControls.Label label2;
		protected System.Web.UI.WebControls.DropDownList cmbModuleName;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList cmbDiaryType;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.HtmlControls.HtmlTableCell trMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFlag;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDiaryID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCheckFlag;
        //Added by Pradeep Kushwaha on 22-07-2010
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidmsg1;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidmsg2;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidmsg3;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidmsg4;
        protected System.Web.UI.WebControls.Label capModuleName;
        protected System.Web.UI.WebControls.Label capLineOfBusiness;
        //Added till here 

		#region VALRIABLE DECLARATION
		ClsDiary objDiary=new ClsDiary();		
		DataSet dsModule=new DataSet();
		DataSet dsModuleName=new DataSet(); 
		string mName="",dName="";
		int mID=0,dID=0;
		DataSet  dsDiary=new DataSet();
		//protected System.Web.UI.HtmlControls.HtmlInputHidden hidDiaryID; 
		//string LOBFlag="";
		
		
		#endregion
        private System.Resources.ResourceManager objResourceMgr;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			//base.ScreenId="366_2";
			base.ScreenId="397_1";
			btnSave.Attributes.Add("onclick","return checkMandatory();");

            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.DiarySetup", Assembly.GetExecutingAssembly());
            SetCaptions();

			if(!Page.IsPostBack)
			{
				dsModule=objDiary.GetModuleDiaryType();
				ClsDiary.GetModule(cmbModuleName,"F");  				
			}
			
			//dsDiary=ClsDiary.GetFollowUpDiaryType("F");

			btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;
		}

        private void SetCaptions()
        {
            hidmsg1.Value = objResourceMgr.GetString("lblmsg1");
            hidmsg2.Value = objResourceMgr.GetString("lblmsg2");
            hidmsg3.Value = objResourceMgr.GetString("lblmsg3");
            hidmsg4.Value = objResourceMgr.GetString("lblmsg4");
            capModuleName.Text = objResourceMgr.GetString("capModuleName");
            capLineOfBusiness.Text = objResourceMgr.GetString("lblLOB");
        }
		private void MakeTablewithValues(int mID,int dID,string flag)
		{
			dsModuleName=objDiary.GetFollowUpDetails(mID,dID); 
			if(dsModuleName!=null)
			{
				if(dsModuleName.Tables.Count>0  )
				{
					if(dsModuleName.Tables[0].Rows.Count>0  )
					{
						if(dsModuleName.Tables[0].Rows[0]["MDD_LOB_ID"].ToString() != "" )
						{
							CreateTable(flag);
						}
					}
					else
						CreateTable(flag);
				}
				else
					CreateTable(flag);
				
			}
			else
				CreateTable(flag);
		}


		private void CreateTable(string flag)
		{
			if(mName!="")	
			{
				HtmlTable hTable=new HtmlTable();
				//hTable.Width = Unit.Percentage(100).ToString() ; 
				//hTable.Align= "Center";
				//hTable.Border=1; 
				hTable.ID="hTable";
				hTable.Attributes.Add("class","tableWidthheader");  

				hTable.Rows.Add(MakeTextRow());
				int iCnt=0;

				if(flag.Equals("Y"))
				{
					hidFlag.Value =flag;
				}
				else
				{					
					hidFlag.Value=flag;
				}

				dsDiary=ClsDiary.GetDiaryByModule(mID.ToString() ,"F");  		
				
				string strDiaryID="";
				if(dsDiary!=null)
				{
					hidLOB.Value =	dsDiary.Tables[0].Rows.Count.ToString() ;						
					for(iCnt=0;iCnt<dsDiary.Tables[0].Rows.Count;iCnt++)
					{							
						strDiaryID += dsDiary.Tables[0].Rows[iCnt]["TYPEID"].ToString() + "," ;
						hTable.Rows.Add(MakePanel(iCnt,flag,dsDiary.Tables[0].Rows[iCnt]["TYPEID"].ToString(),dsDiary.Tables[0].Rows[iCnt]["TYPEDESC"].ToString() ));		
					}						
				}
				else
					hTable.Rows.Add(MakePanel(iCnt,flag,"1",""));		

				if(strDiaryID.LastIndexOf(",")!=-1)
				{
					strDiaryID=strDiaryID.Substring(0,strDiaryID.Length-1);  
				}

				hidDiaryID.Value=strDiaryID;
				
				trMessage.Controls.Add(hTable);  
			}
		}


		/// <summary>
		/// Creating row for diaplaying control description
		/// </summary>
		/// <returns>HTML row with control description</returns>
		private HtmlTableRow MakeTextRow()
		{
			HtmlTableRow hRowtext=new HtmlTableRow();
			HtmlTableCell hCelltext1=new HtmlTableCell();
	
			HtmlTableCell hCelltext3=new HtmlTableCell();
			//HtmlTableCell hCelltext4=new HtmlTableCell();
			HtmlTableCell hCelltext5=new HtmlTableCell();
			HtmlTableCell hCelltext6=new HtmlTableCell();
			HtmlTableCell hCelltext7=new HtmlTableCell();

            //hCelltext7.InnerHtml = "<b>LOB</b>";  
            hCelltext7.InnerHtml = "<b>" + objResourceMgr.GetString("lblLOB") + "</b>"; 
			hCelltext7.Attributes.Add("class","midcolora");
			//hCelltext1.InnerHtml="<b>Subject</b>"; 
            hCelltext1.InnerHtml = "<b>" + objResourceMgr.GetString("lblSubject") + "</b>"; 

			hCelltext1.Attributes.Add("class","midcolora");
            //hCelltext3.InnerHtml="<b>Priority</b>";
            hCelltext3.InnerHtml = "<b>" + objResourceMgr.GetString("lblPriority") + "</b>";

			hCelltext3.Attributes.Add("class","midcolora");
			//hCelltext4.InnerHtml="<b>Notification List</b>";
			//	hCelltext4.Attributes.Add("class","midcolora");
            
            //hCelltext5.InnerHtml="<b>User Group List</b>";
            hCelltext5.InnerHtml = "<b>" + objResourceMgr.GetString("lblUserGroupList") + "</b>";
			
            hCelltext5.Attributes.Add("class","midcolora");
            //hCelltext6.InnerHtml="<b>User List</b>";
            hCelltext6.InnerHtml = "<b>" + objResourceMgr.GetString("lblUserList") + "</b>";
			hCelltext6.Attributes.Add("class","midcolora");

			hRowtext.Cells.Add(hCelltext7)  ;
			hRowtext.Cells.Add(hCelltext1)  ;
	
			hRowtext.Cells.Add(hCelltext3)  ;
			//hRowtext.Cells.Add(hCelltext4)  ;
			hRowtext.Cells.Add(hCelltext5)  ;
			hRowtext.Cells.Add(hCelltext6)  ;

			return hRowtext;

		}


		/// <summary>
		/// Creating control rows and populating combo and list boxes for every diary type
		/// </summary>
		/// <param name="rowCnt">for creating control ID</param>
		/// <param name="moduleId">to be stored within hidden variable</param>
		/// <param name="diaryTypeId">to be stored within hidden variable</param>
		/// <returns>HTML row containing populated controls</returns>
		private HtmlTableRow MakePanel(int rowCnt,string flag,string typeID,string typeDesc)
		{
			
			HtmlTableRow  pRow=new HtmlTableRow();
			HtmlTableCell hCellCtr1=new HtmlTableCell();
	
			HtmlTableCell hCellCtr3=new HtmlTableCell();
			//HtmlTableCell hCellCtr4=new HtmlTableCell();
			HtmlTableCell hCellCtr5=new HtmlTableCell();
			HtmlTableCell hCellCtr6=new HtmlTableCell();
			HtmlTableCell hCellCtr7=new HtmlTableCell();


			Label lbBlank=new Label(); 
			Label lbLOB=new Label(); 
			CheckBox ckType=new CheckBox(); 
			HtmlInputHidden hidID=new HtmlInputHidden(); 
			TextBox txSubject=new TextBox();
		
			DropDownList cmbPriority =new DropDownList();
			//DropDownList cmbNotification=new DropDownList();
			ListBox cmbUserGroup=new ListBox();
			ListBox cmbUserList=new ListBox();

 
			pRow.ID = "pRow_"  + (typeID);			
			hidID.ID = "hidLOB_" + (typeID);				
			ckType.ID="ckType_" +  (typeID);	
			lbBlank.ID = "lbBlank_" + (typeID);
			txSubject.ID = "txSubject_" + (typeID);
		
			cmbPriority.ID = "cmbPriotiry_" + (typeID);
			//cmbNotification.ID = "cmbNotification_" +  (rowCnt+1);
			cmbUserGroup.ID = "cmbUserGroup_" +  (typeID);
			cmbUserList.ID = "cmbUserList_" +  (typeID);


			ckType.Checked = false;
			if(!typeID.Equals(""))
			{
				ckType.Text= typeDesc;  
				hidID.Value= typeID; 
			}

            lbBlank.Text = objResourceMgr.GetString("lblDiaryType");//"All Diary Type";			
			txSubject.TextMode=TextBoxMode.MultiLine; 	
			txSubject.Rows = 3;
			txSubject.Columns = 25;

            cmbPriority.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PRIOR");
            cmbPriority.DataTextField = "LookupDesc";
            cmbPriority.DataValueField = "LookupID";
            cmbPriority.DataBind();

            //cmbPriority.Items.Add(new ListItem("Low","L"));
            //cmbPriority.Items.Add(new ListItem("Medium","M"));
            //cmbPriority.Items.Add(new ListItem("High","H"));
            //cmbPriority.Items.Insert(0,"");
			cmbUserGroup.Height=Unit.Pixel(50);  
			cmbUserList.Height=Unit.Pixel(50);  
			
			cmbUserGroup.SelectionMode=ListSelectionMode.Multiple; 
			ClsUser.GetUserTypeDropDown(cmbUserGroup);
            cmbUserGroup.Items.Insert(0, (ClsCommon.BL_LANG_ID == 2 ? "User Groups Escolha" : "Select User Groups"));//"Select User Groups");
			cmbUserGroup.Items[0].Value="";  	

			cmbUserList.SelectionMode=ListSelectionMode.Multiple; 
			cmbUserList.DataSource		=	ClsCommon.GetUserListForDiarySetup();
			cmbUserList.DataTextField	=	USERNAME;
			cmbUserList.DataValueField	=	USERID;
			cmbUserList.DataBind();
            cmbUserList.Items.Insert(0, (ClsCommon.BL_LANG_ID == 2 ? "Selecionar Usuários" : "Select Users"));//"Select Users");
			cmbUserList.Items[0].Value="";  	


			if(dsModuleName!=null)
			{
				if(dsModuleName.Tables.Count>0   )		
					if(dsModuleName.Tables[0].Rows.Count>0)
						//if(dsModuleName.Tables[0].Rows[rowCnt][0].ToString() !="" && dsModuleName.Tables[0].Rows[rowCnt][0].ToString()!="-1")
						if(typeDesc=="")
							PopulateControls(ref ckType,ref hidID,ref txSubject,ref cmbPriority,ref cmbUserGroup,ref cmbUserList,-1);
						else
							PopulateControls(ref ckType,ref hidID,ref txSubject,ref cmbPriority,ref cmbUserGroup,ref cmbUserList,int.Parse(typeID));

			}

			hCellCtr1.Attributes.Add("class","midcolora");
			hCellCtr3.Attributes.Add("class","midcolora");
			//hCellCtr4.Attributes.Add("class","midcolora");
			hCellCtr5.Attributes.Add("class","midcolora");
			hCellCtr6.Attributes.Add("class","midcolora");
			hCellCtr7.Attributes.Add("class","midcolora");
						
			hCellCtr1.Controls.Add(txSubject);
			
			hCellCtr3.Controls.Add(cmbPriority);
			//hCellCtr4.Controls.Add(cmbNotification);
			hCellCtr5.Controls.Add(cmbUserGroup);
			hCellCtr6.Controls.Add(cmbUserList);
			if(!flag.Equals("Y"))
			{
				hCellCtr7.Controls.Add(lbBlank);
				
			}
			else
			{
				hCellCtr7.Controls.Add(ckType);
				hCellCtr7.Controls.Add(hidID);
			}


			//hCellCtr7.Controls.Add(hidID);

			pRow.Cells.Add(hCellCtr7)  ;
			pRow.Cells.Add(hCellCtr1)  ;
			
			pRow.Cells.Add(hCellCtr3)  ;
			//pRow.Cells.Add(hCellCtr4)  ;
			pRow.Cells.Add(hCellCtr5)  ;
			pRow.Cells.Add(hCellCtr6)  ;


			return pRow;
		}


		private void PopulateControls(ref CheckBox ckType,ref HtmlInputHidden hidID,ref TextBox  txSubject,ref DropDownList cmbPriority,ref ListBox  cmbUserGroup,ref ListBox cmbUserList,int rowCnt)
		{
			string [] strGrpArray={};	
			string [] strUserArray={};	
			int iCnt=0;

			string strLobID="";  
			string strUserGrp="";  
			string strUserList="";  
			string strDiaryList="";
			string strSubject="";  
			string strPriority="";  

			DataRow [] dRow=dsModuleName.Tables[0].Select("MDD_DIARYTYPE_ID=" + rowCnt); 
		
			if(dRow!=null)
			{
				if(dRow.Length>0 )
				{
					strLobID=dRow[0]["MDD_LOB_ID"].ToString(); 
					strDiaryList=dRow[0]["MDD_DIARYTYPE_ID"].ToString(); 
					strUserGrp=dRow[0]["MDD_USERGROUP_ID"].ToString(); 
					strUserList=dRow[0]["MDD_USERLIST_ID"].ToString();  
					strSubject=dRow[0]["MDD_SUBJECTLINE"].ToString();  
					strPriority=dRow[0]["MDD_PRIORITY"].ToString();  
				}
				else
					return;

			}
			else
				return;
			
			//string strNotification=dsModuleName.Tables[0].Rows[rowCnt]["MDD_NOTIFICATION_LIST"].ToString();  


			if(dsModuleName!=null)
			{
				if(dsModuleName.Tables.Count>0   )		
					if(dsModuleName.Tables[0].Rows.Count>0)
					{
						//if(strLobID==hidID.Value)
						//{
						if(!strDiaryList.Equals("0"))
						{
							//hidID.Value=strLobID;
							ckType.Checked=true; 
						}
						//						else
						//							hidID.Value="-1";

						

						if(strDiaryList!="0")	
						{
							if(!strUserGrp.Equals("") )
								strGrpArray=strUserGrp.Split(new char []{','}); 
						
						
							if(strGrpArray.Length >0)
							{
								for(iCnt=0;iCnt<strGrpArray.Length;iCnt++)
								{
									if(strGrpArray[iCnt]!="")
									{
										ListItem liGrp=new ListItem();
										liGrp=cmbUserGroup.Items.FindByValue(strGrpArray[iCnt]);  
										if(liGrp!=null)
											liGrp.Selected=true; 
									}
								}

							}

						
							if(!strUserList.Equals("") )
								strUserArray=strUserList.Split(new char []{','}); 

						
							if(strUserArray.Length >0)
							{
								for(iCnt=0;iCnt<strUserArray.Length;iCnt++)
								{
									if(strUserArray[iCnt]!="")
									{
										ListItem liUser=new ListItem();
										liUser=cmbUserList.Items.FindByValue(strUserArray[iCnt]);  
										if(liUser!=null)
											liUser.Selected=true; 
									}
								}
							}

						
							if(!strSubject.Equals(""))
							{
								txSubject.Text= strSubject;
							}

						
							if(!strPriority.Trim().Equals(""))
							{
								ListItem li=new ListItem();
								li=cmbPriority.Items.FindByValue(strPriority.Trim());  
								if(li!=null)
									li.Selected=true; 
							}

						
							//							if(!strNotification.Equals(""))
							//							{
							//								ListItem li=new ListItem();
							//								li=cmbNotification.Items.FindByValue(strNotification);  
							//								if(li!=null)
							//									li.Selected=true; 
							//							}
							//}
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
			this.cmbModuleName.SelectedIndexChanged += new System.EventHandler(this.cmbModuleName_SelectedIndexChanged);
			this.cmbDiaryType.SelectedIndexChanged += new System.EventHandler(this.cmbDiaryType_SelectedIndexChanged);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	
		/// <summary>
		/// If form is posted back then add entry in database using the BL object
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			lblMess.Visible  =true;
            int iCnt = 0;// count=0,
			TodolistInfo objDefModelInfo=new TodolistInfo(); 

//			if(hidFlag.Value.Equals("Y"))
//			{
//				count=hidLOB.Value==""?-1:int.Parse(hidLOB.Value)  ;			
//			}
			mID=cmbModuleName.SelectedItem.Value==""?-1:int.Parse(cmbModuleName.SelectedItem.Value);
			dsDiary=ClsDiary.GetDiaryByModule(mID.ToString(),"F"); 

			objDiary.DeleteFollowSetup(int.Parse(cmbModuleName.SelectedItem.Value),int.Parse(cmbDiaryType.SelectedItem.Value));
			if(dsDiary!=null)
			{
				for(iCnt=0;iCnt<dsDiary.Tables[0].Rows.Count;iCnt++)
				{
					objDefModelInfo=GetFormValue(int.Parse(dsDiary.Tables[0].Rows[iCnt]["TYPEID"].ToString()));
					int result=objDiary.AddDiarySetup(objDefModelInfo);

					if(result>0)	
					{
						lblMess.Text=ClsMessages.GetMessage(base.ScreenId,"29");
					}
					else
					{
						lblMess.Text=ClsMessages.GetMessage(base.ScreenId,"20");
					}
				}
			}
				
			mName=cmbModuleName.SelectedItem.Text ;
			dName=cmbDiaryType.SelectedItem.Text; 
			

			MakeTablewithValues(int.Parse(cmbModuleName.SelectedItem.Value),int.Parse(cmbDiaryType.SelectedItem.Value),hidFlag.Value);
		}


		#region Method code to do form's processing
		
		/// <summary>
		/// Fetch form's value and stores into variables.
		/// </summary>
		private TodolistInfo  GetFormValue(int rowCnt)
		{

			TodolistInfo objModelInfo = new TodolistInfo();
			CheckBox ck=new CheckBox();
			HtmlInputHidden hidId=new HtmlInputHidden();
			ListBox luGrpBx=new ListBox();
			ListBox luLstBx=new ListBox();
			TextBox tSubBox=new TextBox();
			
			//DropDownList ddlNot=new DropDownList(); 
			DropDownList ddlPri=new DropDownList(); 

			ck.ID= "ckType_" + (rowCnt);
			tSubBox.ID="txSubject_" + (rowCnt);
			
			//ddlNot.ID="cmbNotification_" + (rowCnt+1);
			ddlPri.ID = "cmbPriotiry_" + (rowCnt);
			luGrpBx.ID="cmbUserGroup_" + (rowCnt);
			luLstBx.ID="cmbUserList_" +(rowCnt); 
			hidId.ID = "hidLOB_" + (rowCnt); 

			objModelInfo.LOB_ID         =   cmbDiaryType.SelectedItem.Value =="" ? -1 : int.Parse(cmbDiaryType.SelectedItem.Value);              ; 

			if(Request.Form[ddlPri.ID]!=null)
				objModelInfo.PRIORITY           =   Request.Form[ddlPri.ID].ToString(); 
			
			objModelInfo.SUBJECTLINE        =   Request.Form[tSubBox.ID].ToString() ;

			//			if(Request.Form[ddlNot.ID]!=null)
			//				objModelInfo.SYSTEMFOLLOWUPID   =   Request.Form[ddlNot.ID].ToString() =="" ? -1 : int.Parse(Request.Form[ddlNot.ID].ToString() );            
			objModelInfo.LISTOPEN           =   "Y"; 			
			objModelInfo.MODULE_ID			=	   cmbModuleName.SelectedItem.Value=="" ? -1 : int.Parse(cmbModuleName.SelectedItem.Value);            ;

			if(Request.Form[luGrpBx.ID]!=null)
				objModelInfo.USERGROUP_ID		=   Request.Form[luGrpBx.ID].ToString() ;   

			if(Request.Form[luLstBx.ID]!=null)
				objModelInfo.USERLIST_ID 		=   Request.Form[luLstBx.ID].ToString() ;			

			if(hidFlag.Value.Equals("Y"))
			{
				if(Request.Form[ck.ID]!=null)
					if(Request.Form[ck.ID].ToString().ToLower() =="on" )
						objModelInfo.LISTTYPEID 				=	Request.Form[hidId.ID].ToString()=="" ? 0 : int.Parse(Request.Form[hidId.ID].ToString());   
					else
						objModelInfo.LISTTYPEID					=	0;
			}
			else
			{
				objModelInfo.LISTTYPEID 						=	-1;
			}
			objModelInfo.CREATED_BY = int.Parse(GetUserId());

			
			return objModelInfo;			
		}
		#endregion

		private void cmbDiaryType_SelectedIndexChanged(object sender, System.EventArgs e)
		{

			lblMess.Visible=false; 
			mID=cmbModuleName.SelectedItem.Value==""?0:int.Parse(cmbModuleName.SelectedItem.Value);
			dID=cmbDiaryType.SelectedItem.Value==""?0:int.Parse(cmbDiaryType.SelectedItem.Value);

			mName=cmbModuleName.SelectedItem.Text ;
			dName=cmbDiaryType.SelectedItem.Text; 

//			switch (mID.ToString() )
//			{
//				case "1":
//				case "2":
//				case "3":
//				case "5":
//					MakeTablewithValues(mID,dID,"Y");
//					break;
//				case "4":				
//				case "6":
//					MakeTablewithValues(mID,dID,"N");
//					break;
//				case "-1":
//					MakeTablewithValues(mID,dID,"Y");
//					break;			
//			}

			MakeTablewithValues(mID,dID,"Y");
		}


		private void cmbModuleName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			lblMess.Visible=false;
			string modID=cmbModuleName.SelectedItem.Value;
			mName=cmbModuleName.SelectedItem.Text ;  
			
			if(!modID.Equals("Select Option"))
			{
				DataSet dsLOB=new DataSet(); 
				ClsStates objState=new ClsStates(); 
				dsLOB=objState.PoplateLob();
				mID=int.Parse(modID); 
				cmbDiaryType.DataSource		=	dsLOB;
				cmbDiaryType.DataValueField	=	"LOB_ID";
				cmbDiaryType.DataTextField	=	"LOB_DESC";
				cmbDiaryType.DataBind();
                cmbDiaryType.Items.Insert(0, (ClsCommon.BL_LANG_ID == 2 ? "Selecionar todos os produtos" : "Select All Products")); //"Select All Products");
				cmbDiaryType.Items[0].Value="-1";

                dName = "All Products";
//				switch (modID.ToString() )
//				{
//					case "1":
//					case "2":
//					case "3":
//					case "5":
//						cmbDiaryType.Items[0].Value="-1";  
//						MakeTablewithValues(int.Parse(modID),-1,"Y");
//						break;
//					case "4":					
//					case "6":
//						cmbDiaryType.Items[0].Value="-2"; 
//						MakeTablewithValues(int.Parse(modID),-2,"N");
//						break;
//				}			
	
				MakeTablewithValues(int.Parse(modID),-1,"Y");
			}
			else
			{
				cmbDiaryType.Items.Clear();  	
			}
		}
	

	
	}
}
