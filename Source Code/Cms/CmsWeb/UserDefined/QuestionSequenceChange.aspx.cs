/* ***************************************************************************************
   Author	: Nidhi
   Creation Date : 1/06/2005
   Last Updated  : 1/06/2005
   Reviewed By	 : 
   Purpose	: This page diplays the order of groups/questions  
   Comments	: 
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
using Cms.BusinessLayer;
using System.Data.SqlClient;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlCommon;

namespace Cms.CmsWeb.UserDefined
{
	/// <summary>
	/// Summary description for QuestionSequenceChange.
	/// </summary>
	public class QuestionSequenceChange : Cms.CmsWeb.cmsbase 
	{
		public string gStrStyle,cssFolder; 
		public int gIntSaveFlag;
		protected System.Web.UI.WebControls.Table Table1;
		protected System.Web.UI.WebControls.Label lblQuesDesc,lblGropuID,lblHidLabelID,lblTabID,lblmsg;
		protected System.Web.UI.WebControls.Label lblScreenName,lblTabName,lblGroupName;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnSave,btnCancel;
		protected System.Web.UI.WebControls.DropDownList cboUserList;
		protected System.Web.UI.WebControls.Panel pnlGroup;
		protected string gStrGroupID="";
		protected int gintTabID;
		protected string gStrScreenID="";
		protected string gStyleOrder="";
		protected int glstrgroupCounter=0;
		protected string glstrCombineID="";
		protected string gstrerrmsg="";
		protected string gstrerrmsgblank="";
		protected string gStrCalledFrom="";
		TableRow otabrow;
		TableCell otabcell;
		TableCell otabcell1;
		UserDefinedOne objQuesSeq;
		 
		protected int gIntScreenID=26;

		public string gStrNavigationText="";	
		public string gStrScreenPrefix="";
		public string gStrTabPrefix="";
		public string gStrGroupPrefix="";
		private System.Resources.ResourceManager aobjResMang;
		  
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				 			 

				objQuesSeq= new UserDefinedOne();
				string lStrResourceName="";
				lStrResourceName = "Cms.CmsWeb.UserDefined.QuestionSequenceChange";		//The fully qualified class name.
				aobjResMang=new System.Resources.ResourceManager(lStrResourceName ,System.Reflection.Assembly.GetExecutingAssembly()); 

				btnSave.Value= aobjResMang.GetString("btnSaveText");
				btnCancel.Value= aobjResMang.GetString("btnBackText");
				gstrerrmsg= aobjResMang.GetString("gstrerrmsg");
				gstrerrmsgblank	= aobjResMang.GetString("gstrerrmsgblank");
				gStyleOrder="Z-INDEX: 102; LEFT: 28px; POSITION: absolute; TOP: 400px";

				int lintCounter=0,rowcount=0;				
				string lstrQuestionDesc="";
				int lintGroupID=0;
				int lintSeqNumber = 0;
				string lstrGroupType;
				string lstrGroupName;
				string lstrGroupSeqNo="";
				string lstrGroupID="";
				int lintTabID=1;
				int lintReturn=0;
				int lintCountCounter=0;
				DataSet ds = new DataSet();
				DataSet ds1 = new DataSet();
			
			
				gStrGroupID=lstrGroupID= (Request["GroupID"]==null||Request["GroupID"]=="")?"0":Request["GroupID"];		
				gintTabID=lintTabID=int.Parse((Request["TabID"]==null||Request["TabID"]=="")?"0":Request["TabID"]);	 
				 
				gStrScreenID= (Request["ScreenID"]==null||Request["ScreenID"]=="")?"0":Request["ScreenID"];					 
				lblHidLabelID.Text=lstrGroupID;
				lblTabID.Text = lintTabID.ToString();
				gStrCalledFrom = Request["CalledFrom"]==null?"":Request["CalledFrom"];
				if(lintTabID==0)
				{
					pnlGroup.Visible=false;
				}

				gStrScreenPrefix=aobjResMang.GetString("strScreenPrefix");
				gStrNavigationText=ClsUserQuestion.GetScreenNameText(gStrScreenID,"1");
				lblScreenName.Text=gStrScreenPrefix + gStrNavigationText;

				gStrTabPrefix=aobjResMang.GetString("strTabPrefix");
				gStrNavigationText=UserDefinedOne.GetTabNameText(gStrScreenID,"1",lintTabID.ToString());
				lblTabName.Text=gStrTabPrefix + gStrNavigationText;


				if((gStrGroupID.Trim()!="" && gStrGroupID.Trim()!="0")&& gStrScreenID.Trim()!="0")
				{
					pnlGroup.Visible=true;
					gStrGroupPrefix=aobjResMang.GetString("strGroupPrefix");
					gStrNavigationText=UserDefinedOne.GetGroupNameText(gStrScreenID,lintTabID.ToString(),gStrGroupID);
					lblGroupName.Text=gStrGroupPrefix + gStrNavigationText;						
				}
							

				TableRow otabrowlblmsg = new TableRow();
				TableCell otabcelllblmsg= new TableCell();
				otabrowlblmsg.CssClass="midcolorc";			
				otabcelllblmsg.Controls.Add(lblmsg);
				otabcelllblmsg.ColumnSpan=2;
				otabrowlblmsg.Controls.Add(otabcelllblmsg);
				otabrowlblmsg.HorizontalAlign=System.Web.UI.WebControls.HorizontalAlign.Center;
				otabrowlblmsg.VerticalAlign=System.Web.UI.WebControls.VerticalAlign.Middle;
			
				Table1.Rows.Add(otabrowlblmsg);
				rowcount=rowcount+2;
				rowcount=0;


				if(lblHidLabelID.Text.Trim()=="")
				{
					ds = UserDefinedOne.GetGroupCount(gStrScreenID, lblTabID.Text.ToString());
					lintCounter = int.Parse(ds.Tables[0].Rows.Count.ToString());
					glstrgroupCounter = int.Parse(ds.Tables[0].Rows.Count.ToString());
					ds = objQuesSeq.fnGetTabGroupQues(gStrScreenID,lblTabID.Text.ToString());
					glstrCombineID="";
					while(rowcount< ds.Tables[0].Rows.Count)
					{
						cboUserList = new DropDownList();
						for(int i=lintCounter;i>=1;i--)
						{							
							cboUserList.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
							cboUserList.SelectedIndex = 0;
						}
						cboUserList.Items.Insert(0, new ListItem("", ""));
						cboUserList.SelectedIndex = 0;

						lintGroupID = int.Parse(ds.Tables[0].Rows[rowcount].ItemArray[1].ToString());
						lstrQuestionDesc = ds.Tables[0].Rows[rowcount].ItemArray[2].ToString();
						lstrGroupType = ds.Tables[0].Rows[rowcount].ItemArray[3].ToString();
						lintSeqNumber = int.Parse(ds.Tables[0].Rows[rowcount].ItemArray[4].ToString());
						for(int j=0;j<=cboUserList.Items.Count - 1;j++)	// To populate QuestionType 
						{	
							if(j==lintSeqNumber)
							{
								cboUserList.SelectedIndex = j;
								break;
							}
						}
						if(lstrGroupType=="G")
						{
							ds1 = objQuesSeq.fnGetGroupSeqName(int.Parse (gStrScreenID), lblTabID.Text, int.Parse(gStrGroupID ));
							if(ds1.Tables[0].Rows.Count > 0)
							{
								if(lblGropuID.Text!=lintGroupID.ToString())
								{
									if(glstrCombineID.Trim()=="")
									{
										glstrCombineID=lintGroupID.ToString();
									}
									else
									{
										glstrCombineID+="_"+lintGroupID.ToString();
									}
									lblGropuID.Text=lintGroupID.ToString();
									lstrGroupName = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
									otabrow = new TableRow();
									otabcell = new TableCell();
									otabcell1 = new TableCell();
									lblQuesDesc = new Label();								
									lblQuesDesc.Text=lstrGroupName;							
									otabcell.Controls.Add(lblQuesDesc);									
									cboUserList.ID="cboGroup"+lintGroupID.ToString();
									otabcell1.Controls.Add(cboUserList);
									otabcell1.CssClass="midcolora";
									otabcell.CssClass="midcolora";
									otabrow.Cells.Add(otabcell);
									otabrow.Cells.Add(otabcell1);
									Table1.Rows.Add(otabrow);
								}
							}						
						}
						else
						{
							if(glstrCombineID.Trim()=="")
							{
								glstrCombineID=lintGroupID.ToString();
							}
							else
							{
								glstrCombineID+="_"+lintGroupID.ToString();
							}
							otabrow = new TableRow();
							otabcell = new TableCell();
							otabcell1 = new TableCell();
							lblQuesDesc = new Label();								
							lblQuesDesc.Text=lstrQuestionDesc;					
							otabcell.Controls.Add(lblQuesDesc);							
							otabcell1.Controls.Add(cboUserList);
							otabcell1.CssClass="midcolora";
							otabcell.CssClass="midcolora";
							cboUserList.ID="cboGroup"+lintGroupID.ToString();
							otabrow.Cells.Add(otabcell);
							otabrow.Cells.Add(otabcell1);
							Table1.Rows.Add(otabrow);
						}
						rowcount = rowcount + 1;
					}				
				}
				else
				{
					ds = objQuesSeq.fnGetTabGroupQuestion(gStrScreenID, lblTabID.Text.ToString(),gStrGroupID);
					lintCounter = int.Parse(ds.Tables[0].Rows.Count.ToString());
					if(lintCounter > 0)
					{
						glstrgroupCounter = int.Parse(ds.Tables[0].Rows.Count.ToString());
						rowcount=0;
						while(rowcount< ds.Tables[0].Rows.Count)
						{
							cboUserList = new DropDownList();
							for(int i=lintCounter;i>=1;i--)
							{	
								cboUserList.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
								cboUserList.SelectedIndex = 0;
							}
							cboUserList.Items.Insert(0, new ListItem("", ""));
							cboUserList.SelectedIndex = 0;

							lintGroupID = int.Parse(ds.Tables[0].Rows[rowcount].ItemArray[0].ToString());
							lstrQuestionDesc = ds.Tables[0].Rows[rowcount].ItemArray[1].ToString();
							lintSeqNumber = int.Parse(ds.Tables[0].Rows[rowcount].ItemArray[3].ToString());
							for(int j=0;j<=cboUserList.Items.Count - 1;j++)	// To populate QuestionType 
							{	
								if(j==lintSeqNumber)
								{
									cboUserList.SelectedIndex = j;
									break;
								}
							}
							if(glstrCombineID.Trim()=="")
							{
								glstrCombineID=lintGroupID.ToString();
							}
							else
							{
								glstrCombineID+="_"+lintGroupID.ToString();
							}
							otabrow = new TableRow();
							otabcell = new TableCell();
							otabcell1 = new TableCell();
							lblQuesDesc = new Label();								
							lblQuesDesc.Text=lstrQuestionDesc;							
							otabcell.Controls.Add(lblQuesDesc);						
							cboUserList.ID="cboGroup"+lintGroupID.ToString();
							otabcell1.Controls.Add(cboUserList);
							otabcell1.CssClass="midcolora";
							otabcell.CssClass="midcolora";
							otabrow.Cells.Add(otabcell);
							otabrow.Cells.Add(otabcell1);
							Table1.Rows.Add(otabrow);					
							rowcount = rowcount + 1;
						}
					}
					else
					{
						lblmsg.Text= aobjResMang.GetString("lblMesg1");
						// No row returned

					}

				}
			
				if(IsPostBack)
				{
					string[] strarrystring = glstrCombineID.Split(new char[]{'_'});
					if(lblHidLabelID.Text.Trim()=="")
					{
						for(int i=0; i<=strarrystring.Length - 1;i++)	
						{	
							lintCountCounter = lintCountCounter + 1;
							lstrGroupSeqNo = Request["cboGroup"+strarrystring[i]];	
							lintReturn = objQuesSeq.fnUpdateQuestionSequence(int.Parse(gStrScreenID), lintTabID,int.Parse(strarrystring[i]),lstrGroupSeqNo);
						}
					}
					else
					{
						for(int i=0; i<=strarrystring.Length -1;i++)	
						{	
							lintCountCounter = lintCountCounter + 1;
							lstrGroupSeqNo = Request["cboGroup"+strarrystring[i]];	
							lintReturn = objQuesSeq.fnUpdateGroupSeqNo(int.Parse (gStrScreenID) ,lintTabID,int.Parse(strarrystring[i]),lstrGroupSeqNo);
						}
					}
					gIntSaveFlag=1;

					lblmsg.Text= aobjResMang.GetString("strSaveMsg");
				}
				if(lintCounter==0)
				{
					btnSave.Visible=false;					
				}
				
				TableRow otabrowbtn = new TableRow();
				TableCell otabcellbtn1 = new TableCell();
				otabcellbtn1.HorizontalAlign=System.Web.UI.WebControls.HorizontalAlign.Left;
				otabcellbtn1.Controls.Add(btnCancel);
				otabrowbtn.Controls.Add(otabcellbtn1);
				Table1.Rows.Add(otabrowbtn);

				TableCell otabcellbtn = new TableCell();
				otabcellbtn.HorizontalAlign=System.Web.UI.WebControls.HorizontalAlign.Right;
				if(lintCounter>0)
				{
					otabcellbtn.Controls.Add(btnSave);
				}
				
				otabrowbtn.Controls.Add(otabcellbtn);			
				 
			}
			catch(Exception ex)
			{
				throw(ex);
				
			}
			finally
			{
				if (objQuesSeq!=null)
					objQuesSeq.Dispose();				
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
