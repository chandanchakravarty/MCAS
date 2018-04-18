/* ***************************************************************************************
   Author	: Nidhi
   Creation Date : 1/06/2005
   Last Updated  : 1/06/2005
   Reviewed By	 : 
   Purpose	: This page diplays the order of tab of a screen. 
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
	/// Summary description for ChangeTabOrder.
	/// </summary>
	public class ChangeTabOrder : Cms.CmsWeb.cmsbase 
	{
		public string gStrStyle,cssFolder; 
		public int gIntSaveFlag;
		protected System.Web.UI.WebControls.Table Table1;
		protected System.Web.UI.WebControls.Label lblQuesDesc,lblGropuID,lblHidLabelID,lblTabID,lblnavid,lblmsg;
		protected System.Web.UI.WebControls.Label lblNavLevelTwo,lblScreenName;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnSave,btnCancel;
		protected System.Web.UI.WebControls.DropDownList cboUserList;
		protected string gStrScreenID="";
		public int gIntScreenID=26;
		protected string gStyleOrder="";
		protected string gstrerrmsg="";
		protected string gstrerrmsgblank="";
		protected int gIntCountCounter = 0;
		protected string glstrCombineID = "";
		public string gStrNavigationText="";
		public string gStrScreenPrefix="";
		TableRow otabrow;
		TableCell otabcell;
		TableCell otabcell1;
		protected System.Web.UI.HtmlControls.HtmlForm TabSequenceChange;
		UserDefinedOne objQuesSeq;
		private System.Resources.ResourceManager aobjResMang;
		  
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				base.ScreenId	=	"128_2";
				objQuesSeq= new UserDefinedOne();
				string lStrResourceName="";
				lStrResourceName = "Cms.CmsWeb.UserDefined.ChangeTabOrder";		//The fully qualified class name.
				aobjResMang=new System.Resources.ResourceManager(lStrResourceName ,System.Reflection.Assembly.GetExecutingAssembly()); 
	
				gStrScreenID= Request["ScreenID"]==null?"":Request["ScreenID"].ToString();

				gStrScreenPrefix=aobjResMang.GetString("strScreenPrefix");
				lblNavLevelTwo.Text=aobjResMang.GetString("strNavigationTxt");
				gStrNavigationText=ClsUserQuestion.GetScreenNameText(gStrScreenID,"1");
				lblScreenName.Text=gStrScreenPrefix + gStrNavigationText;
				btnSave.Value= aobjResMang.GetString("btnSave");
				btnCancel.Value= aobjResMang.GetString("btnCancel");
				gstrerrmsg= aobjResMang.GetString("gstrerrmsg");
				gstrerrmsgblank	= aobjResMang.GetString("gstrerrmsgblank");
				gStyleOrder="Z-INDEX: 102; LEFT: 28px; POSITION: absolute; TOP: 400px";

				int lintCounter=0,rowcount=0;				
				int lintSeqNumber = 0;
				int lintTabID=0;
				string lstrTabName="";				
				DataSet ds = new DataSet();
				DataSet ds1 = new DataSet();
				
				string lstrTabSeqNo= "";			
				int lintReturn=0;																
					
				lintCounter = objQuesSeq.fnGetTotalTab(gStrScreenID);				
				rowcount=0;
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
				if(gStrScreenID.Trim()!="")
				{
					ds = objQuesSeq.GetTabDetails(gStrScreenID);					
					
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

						lintTabID = int.Parse(ds.Tables[0].Rows[rowcount].ItemArray[0].ToString());
						lstrTabName= ds.Tables[0].Rows[rowcount].ItemArray[1].ToString();						
						lintSeqNumber = int.Parse(ds.Tables[0].Rows[rowcount].ItemArray[2].ToString());
						for(int j=0;j<=cboUserList.Items.Count - 1;j++)	// To populate QuestionType 
						{	
							if(j==lintSeqNumber)
							{
								cboUserList.SelectedIndex = j;
								break;
							}
						}
						
						if(glstrCombineID=="")
						{
							glstrCombineID = lintTabID.ToString();
						}
						else
						{
							glstrCombineID += "_" + lintTabID.ToString();
						}
						otabrow = new TableRow();
						otabcell = new TableCell();
						otabcell1 = new TableCell();
						lblQuesDesc = new Label();								
						lblQuesDesc.Text=lstrTabName;							
						otabcell.Controls.Add(lblQuesDesc);						
						cboUserList.ID="cboGroup_"+lintTabID.ToString();
						otabcell1.Controls.Add(cboUserList);
						otabcell1.CssClass="midcolorr";
						otabcell1.HorizontalAlign=System.Web.UI.WebControls.HorizontalAlign.Right;
						otabcell.CssClass="midcolora";
						otabrow.Cells.Add(otabcell);
						otabrow.Cells.Add(otabcell1);
						Table1.Rows.Add(otabrow);
						gIntCountCounter = gIntCountCounter + 1;
						rowcount = rowcount + 1;
					}				
				}
						
				
			
				if(IsPostBack)
				{
					string[] strarrystring = glstrCombineID.Split(new char[]{'_'});
					
					for(int i=0; i<=strarrystring.Length - 1;i++)	
					{	
						gIntCountCounter = gIntCountCounter + 1;
						lstrTabSeqNo = Request["cboGroup_"+strarrystring[i]];	
						lintReturn = objQuesSeq.fnUpdateTabSequence(gStrScreenID,lstrTabSeqNo,int.Parse(strarrystring[i]));
					}					
					
					gIntSaveFlag=1;

					lblmsg.Text= aobjResMang.GetString("strSaveMsg");
				}
				
				if(lintCounter==0)
				{
					btnSave.Visible=false;	
					lblmsg.Text= aobjResMang.GetString("lblNoRow");	
				}

				TableRow otabrowbtn = new TableRow();

				TableCell otabcellbtn1 = new TableCell();
				otabcellbtn1.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
				otabcellbtn1.Controls.Add(btnCancel);
				otabrowbtn.Controls.Add(otabcellbtn1);

				TableCell otabcellbtn = new TableCell();
				otabcellbtn.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
				if(lintCounter > 0)
				{
					otabcellbtn.Controls.Add(btnSave);
				}
				otabrowbtn.Controls.Add(otabcellbtn);			
				Table1.Rows.Add(otabrowbtn);		
			}
			catch(Exception ex)
			{
				if(GetUserId()!=null)
				{				
					throw(ex);
				}
				else
				{
					Response.Write("<SCRIPT language=Javascript>\n<!--\n");
					Response.Write("top.location.href = \"../index.aspx?se=1\"\n");
					Response.Write("//-->\n</SCRIPT>\n");
					Response.End();

				}
				
			}
			finally
			{
				if(objQuesSeq!=null)
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
