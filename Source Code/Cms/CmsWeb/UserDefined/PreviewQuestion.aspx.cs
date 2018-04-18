/****************************************************************************************
   Author	: Nidhi
   Creation Date : 31/05/2005
   Last Updated  : 01/06/2005
   Reviewed By	 : 
   Purpose	: This page diplays the preview of the Questions
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
using Cms.BusinessLayer.BlCommon;
using System.Data.SqlClient;

namespace Cms.CmsWeb.UserDefined
{
	/// <summary>
	/// Summary description for PreviewQuestion.
	/// </summary>
	public class PreviewQuestion : Cms.CmsWeb.cmsbase 
	{
		public string gStrStyle,cssFolder; 
		protected System.Web.UI.WebControls.PlaceHolder TabHolder;
		protected string gstrHtml;
		
		protected System.Web.UI.WebControls.Label lblControl, lblSuffix,lblGroupName,lblPrefix,lblNotes,lblgroupBreak,lblGrid,lblComIds,lblOthDesc, lblBlanktext,lblTabID,lblErroMsg, lblScreenID ;
		protected System.Web.UI.WebControls.Label lblPrevQID;
		protected System.Web.UI.WebControls.Label lblGroupID;
		
		protected System.Web.UI.WebControls.Button btnAddNew;
		protected System.Web.UI.WebControls.Button btnSave;	

		protected System.Web.UI.WebControls.TextBox txtControl;
		protected System.Web.UI.WebControls.HyperLink hrCalender;
		protected System.Web.UI.WebControls.ListBox ListControl;
		protected System.Web.UI.WebControls.RadioButtonList rbList;
		protected System.Web.UI.WebControls.CheckBoxList chkBoxList;
		protected System.Web.UI.WebControls.ListBox Ls1;
		protected System.Web.UI.WebControls.RequiredFieldValidator reqFieldValidator;
		protected System.Web.UI.WebControls.CompareValidator compFieldValidator;
		protected System.Web.UI.WebControls.DropDownList cboUserList;
		

		
		protected string gstrCombineQID="";
		protected string gstrControlName="";
		protected string gStrInsertUpdateFlag="";
		//private string gStrAddTxn,gStrUpdateTxn;
		public int ArrayCounter=0;
		
		protected System.Web.UI.WebControls.Label Label2;
		ClsUserQuestion objUserQuestion = new ClsUserQuestion();
	    
		//Variable declare to populate from the resource file to display the messge for multilinguil
		protected string lstrbtnSave="";
		protected string lstrbtnAddNew="";
		protected string lstrvldBlank="";		
		protected string lstrvldSelect="";
		protected string lstrvldNumeric="";
		protected System.Web.UI.WebControls.Table Table2;
		protected string lstrvldDate="";
		protected string gstrScreenName="";	
		
		protected int gintQuestionExists=1;
		protected string lstrSaveMessage="";
		protected string lStrMessage1 = "";
		protected string lStrMessage2 = "";
		
		//string[] lArrayQues=null;
		protected System.Web.UI.WebControls.Table tblUDScreen;
		//string[] lArrayAns=null;

		//adding the varible to check wether the group is already added to screen or not
		private string GroupAdded = "";
		protected string gstrTabID="";
		protected string gstrScreenID="";


		//manab added for style purpose
		protected Int32 MaxCols=1;
		private string ControlValidationType;
		private string ControlStyle;
		private string ControlMaxLength;
		private string ControlSpan;
		protected string lsCheckBoxesLIst;
		//private int iQuestionCtr= -1 ;
		protected System.Web.UI.WebControls.Label lblhidScreenID;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnclose;
		protected System.Web.UI.WebControls.Label lblhidGroupID;
		System.Resources.ResourceManager aobjResMang;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				objUserQuestion= new ClsUserQuestion();
				string lStrResourceName="";
				string lstrCarrier = "";
				
				//cssFolder=gStrStyle.Substring(0,gStrStyle.IndexOf("."))+"/";
				lstrCarrier="1";// EbxSession.Get("CarrierID");
				//lstrBrokerID = "1";//EbxSession.Get("BrokerID");
				
				lStrResourceName = "Cms.CmsWeb.UserDefined.PreviewQuestion";		//The fully qualified class name.
				aobjResMang=new System.Resources.ResourceManager(lStrResourceName ,System.Reflection.Assembly.GetExecutingAssembly()); 
	
				lstrbtnSave=aobjResMang.GetString("btnSave");
				lstrbtnAddNew=aobjResMang.GetString("btnAddNew");
				lstrvldBlank=aobjResMang.GetString("vldBlank");
				lstrvldNumeric=aobjResMang.GetString("vldNumeric");
				lstrvldSelect=aobjResMang.GetString("vldSelect");
				lstrvldDate = aobjResMang.GetString("vldDate");
				
				gstrScreenID = Request["ScreenID"];
				if(!IsPostBack)
				{
					gstrTabID = Request["TabID"];
					lblTabID.Text=gstrTabID;
					ViewState["glintControlCounterl"]=1;				
					lblhidScreenID.Text = Request["ScreenID"];
					lblPrevQID.Text = Request["QID"];
                }
			 
				lblGroupID = new Label();		 
				lblGroupID.Text="";				
				 
				gstrScreenName = ClsUserQuestion.GetScreenNameText(lblhidScreenID.Text,lstrCarrier);
                
				fnGenerateHtml(lstrCarrier);
								
			}
			catch(Exception ex)
			{
				 throw(ex);
			}
			finally
			{
				objUserQuestion.Dispose();
			}	
			lblComIds.Text = gstrCombineQID;		
		}


		
		public string fnGenerateHtml(string psCarrierId)
		{
			//i m taking the format as
			// 1 2 3 4 5 6 7 8 9 10
			//10 TD for displaying 2 questions in a row
			// 1,6 for question desc
			// 2,7 for prefix
			// 3,8 for control
			// 4,9 for suffix
			// 5,10 for validators

			// in case of group make a row of column span 10 and show the questions underneath of them
			//in case of group 1 td will also contain a tab space

		
			string lsQuestionType			=	"";;
			string lsQuestionDesc			=	"";
			int	   liQuestionID				=	-1;
			string lsReq					=	"";
			string lsgroupid				=	"";
			string lsQNotes					=	"";
			string lsListID					=	"";
			string lsSuffix					=	"";
			string lsPrefix					=	"";
			string lsflgOtherQuess			=	"";
			string lsOthQuesDesc			=	"";
			string lsOthQuesCompareVal		=	"";
			string lsGroupType				=	"";
			string lsListType				=	"";
			string lsListTable				=	"";
			DataSet dsUserQuestion			=   null; 
			string lsQuesAnswer				=	"";
			string lsOptAnswer				=	"";
			string lsOptionAnswerID			=	"";
			string lsOthAnswerDesc			=	"";
			
			string lsDepQuestionText		=	"";
			string lsDepQuesType			=	"";
			string lsDepAnsType				=	"";
			string lsDepQuesList			=	"";


			
			gStrInsertUpdateFlag="insert";
			//****************************Case of Insert*****************************
			//we have to fetch the questions

			dsUserQuestion = objUserQuestion.GetTabQuestion(lblTabID.Text,lblhidScreenID.Text,psCarrierId,lblPrevQID.Text);
			if(dsUserQuestion.Tables[0].Rows.Count == 0 )
			{
				lblErroMsg.Visible=true;
				lblErroMsg.Text="There is No Question Posted against this section";
				gintQuestionExists=0;
			}
			else
			{
				//generate html by running a looop fetch the values

				if (dsUserQuestion.Tables[0].Rows.Count > 0 )
				{
					int TABCOLUMNS			= Convert.ToInt32(dsUserQuestion.Tables[0].Rows[0]["REPEATCONTROLS"].ToString());
					MaxCols					= TABCOLUMNS*3 + (TABCOLUMNS -1);
						
					tblUDScreen.Rows[0].Cells[0].ColumnSpan=MaxCols;
					tblUDScreen.Rows[1].Cells[0].ColumnSpan=MaxCols;
				}


				foreach(DataRow ldrRow in dsUserQuestion.Tables[0].Rows)
				{
					lsQuestionType		= ldrRow["QUESTIONTYPEID"].ToString();
					liQuestionID		= int.Parse(ldrRow["QID"].ToString());
					lsReq				= ldrRow["ISREQ"].ToString();
					lsQuestionDesc		= ldrRow["QDESC"].ToString();
					lsSuffix			= ldrRow["SUFFIX"].ToString();
					lsPrefix			= ldrRow["PREFIX"].ToString();
					lsgroupid			= ldrRow["GROUPID"].ToString();
					lsQNotes			= ldrRow["QNOTES"].ToString();
					lsListID			= ldrRow["QUESTIONLISTID"].ToString();
					lsflgOtherQuess		= ldrRow["FLGQUESDESC"].ToString();
					lsOthQuesDesc		= ldrRow["DESCTEXT"].ToString();
					lsOthQuesCompareVal = ldrRow["FLGCOMPVALUE"].ToString();
					lsGroupType			= ldrRow["GroupType"].ToString();
					lsListType			= ldrRow["TYPE"].ToString();
					lsListTable			= ldrRow["TABLENAME"].ToString();
						
					ControlStyle			= ldrRow["STYLE"].ToString();
					ControlValidationType	= ldrRow["VALIDATIONTYPE"].ToString();
					ControlMaxLength		= ldrRow["MAXLENGTH"].ToString();
					ControlSpan				= ldrRow["COLSPAN"].ToString();


					if (ldrRow["DEPQUESTIONTEXT"] != DBNull.Value && ldrRow["DEPQUESTIONTEXT"] != null)
						lsDepQuestionText = ldrRow["DEPQUESTIONTEXT"].ToString();

					if (ldrRow["DEPQUESTYPE"] != DBNull.Value && ldrRow["DEPQUESTYPE"] != null)
						lsDepQuesType = ldrRow["DEPQUESTYPE"].ToString();
									
					if (ldrRow["DEPANSTYPE"] != DBNull.Value && ldrRow["DEPANSTYPE"] != null)
						lsDepAnsType = ldrRow["DEPANSTYPE"].ToString();

					if (ldrRow["DEPQUESTIONLIST"] != DBNull.Value && ldrRow["DEPQUESTIONLIST"] != null)
						lsDepQuesList = ldrRow["DEPQUESTIONLIST"].ToString();

					
					PrepareHTML(lsQuestionType,liQuestionID.ToString(),lsReq,lsQuestionDesc,lsSuffix,lsPrefix,lsgroupid,lsQNotes,lsListID,lsflgOtherQuess,lsOthQuesDesc,lsOthQuesCompareVal,lsGroupType,lsListType,lsListTable,ControlStyle,ControlValidationType,ControlMaxLength,ControlSpan,lsQuesAnswer,lsOptAnswer,lsOptionAnswerID,lsOthAnswerDesc,lsDepQuestionText,lsDepQuesType,lsDepAnsType,lsDepQuesList);
				}
				//checking wether the last cell is incomplete or not
				TableRow trLastRow = tblUDScreen.Rows[tblUDScreen.Rows.Count - 1];
				if (trLastRow.Cells.Count <= MaxCols && trLastRow.Cells.Count > 0 && trLastRow.Cells[trLastRow.Cells.Count-1].ColumnSpan == 0 )
				{
					trLastRow.Cells[trLastRow.Cells.Count-1].ColumnSpan = MaxCols - trLastRow.Cells.Count + 1;
				}

				int iCellsCount = 0;
				foreach(TableRow trTemp  in tblUDScreen.Rows)
				{
					iCellsCount = 0;
					foreach(TableCell tcTemp in trTemp.Cells)
					{
						if (tcTemp.ColumnSpan == 0)
						{
							iCellsCount++;
						}
						else
						{
							iCellsCount += tcTemp.ColumnSpan;
						}
					}
					if (iCellsCount < MaxCols)
					{
						trTemp.Cells[trTemp.Cells.Count-1].ColumnSpan = MaxCols-iCellsCount;
					}
				}
				
			}
			return "";
		}

		
		public void PrepareHTML(string psQuestionType,string piQuestionID,string psReq,string psQuestionDesc,string psSuffix,string psPrefix,string psgroupid,string psQNotes,string psListID,string psflgOtherQuess,string psOthQuesDesc,string psOthQuesCompareVal,string psGroupType,string psListType,string psListTable,string psControlStyle,string psControlValidationType,string psControlMaxLength,string psControlSpan,string psQuesAnswer,string psOptAnswer,string psOptionAnswerID,string psOthAnswerDesc,string lsDepQuestionText,string lsDepQuesType,string lsDepAnsType,string lsDepQuesList)
		{
			
			TableRow trBlankRow = new TableRow();
			
			trBlankRow.CssClass  = "midcolora";


			TableCellCollection tcCollection  = null;

			//checking wther previous row has space or not
			//intialize cells collection depending upon space in previous row
			int liSpaceRemaining = (MaxCols- tblUDScreen.Rows[tblUDScreen.Rows.Count - 1].Cells.Count);
			int lastColSpan =tblUDScreen.Rows[tblUDScreen.Rows.Count - 1].Cells[tblUDScreen.Rows[tblUDScreen.Rows.Count - 1].Cells.Count - 1].ColumnSpan;
			if (lastColSpan != 0)
			{
				liSpaceRemaining ++;
				liSpaceRemaining -= lastColSpan;
			}

			int liSpaceNeeded  = int.Parse(psControlSpan)*3;
			bool blAddBlankCell =false;


			if (liSpaceRemaining >=  liSpaceNeeded)
			{
				if (psQuestionType == "9" || psQuestionType == "8" || psQuestionType == "10")
				{
					tblUDScreen.Rows[tblUDScreen.Rows.Count - 1].Cells[tblUDScreen.Rows[tblUDScreen.Rows.Count - 1].Cells.Count -1].ColumnSpan = liSpaceRemaining + 1;
					tcCollection = trBlankRow.Cells;
					liSpaceRemaining=0;
				}
				else
				{
					tcCollection = tblUDScreen.Rows[tblUDScreen.Rows.Count - 1].Cells;
					blAddBlankCell =true;
				}
			}
			else
			{
				tcCollection = trBlankRow.Cells;
			}


			//string lsCarrierId = "1";

			//it is a group or a question
			if(psgroupid!="" && psGroupType=="G" && GroupAdded != psgroupid)
			{

				if (tcCollection.Count != 0)
				{
					//finishing the exiting row
					tcCollection[tcCollection.Count - 1].ColumnSpan = MaxCols - tcCollection.Count + 1;
					
					trBlankRow = new TableRow();
					trBlankRow.CssClass  = "midcolora";
					tcCollection = trBlankRow.Cells;
				}

				tcCollection.Add(new TableCell());
				tcCollection[0].ColumnSpan=MaxCols;

				//declaring label for group
				Label loGroupLabel = new Label();

				string lsGroupName = objUserQuestion.GetGroupName(psgroupid,lblTabID.Text,gstrScreenID);
				loGroupLabel.Text = lsGroupName;
				loGroupLabel.Font.Bold= true;
				tcCollection[0].Controls.Add(loGroupLabel);
				
				tblUDScreen.Rows.Add(trBlankRow);
				GroupAdded = psgroupid;

				//end of adding the group 

				//for the question
				trBlankRow = new TableRow();
				trBlankRow.CssClass  = "midcolora";
				tcCollection = trBlankRow.Cells;
				liSpaceRemaining=0;
				blAddBlankCell=false;
			}
		
			//now we will add 2 questions  in a row
			TableCell tcControl = new TableCell(); //adding suffix and validators
			TableCell tcPrefix = new TableCell();
			TableCell tcGrid = new TableCell();
			TableCell tcBlankCell = new TableCell();

			
			tcPrefix.HorizontalAlign= HorizontalAlign.Right;
			tcBlankCell.Width =Unit.Percentage(5);
			

			if (int.Parse(psControlSpan) > 1)
			{
				tcControl.ColumnSpan=((int.Parse(psControlSpan)-1)*3 + 1);
			}


			//getting the question control based upon the questoin typoe

			getQuestionControl(ref tcGrid,ref tcControl,ref tcPrefix,piQuestionID,Convert.ToInt32(psQuestionType),psListID,psListType,psListTable,psflgOtherQuess,psOthQuesCompareVal,psReq,psPrefix,psSuffix,psControlStyle,psControlValidationType,psControlMaxLength,psQuesAnswer,psOptAnswer,psOptionAnswerID,psOthAnswerDesc,lsDepQuestionText,lsDepQuesType,lsDepAnsType,lsDepQuesList);


				
			if(psQNotes.Trim()!="")
			{
				lblNotes = new Label();
				lblNotes.ID = "lblNotes_"+ piQuestionID + "_" + gstrTabID + "_" +  gstrScreenID ;
				lblNotes.Text = "<br>Notes: " + psQNotes;
				lblNotes.CssClass="input";
				lblNotes.ForeColor = System.Drawing.Color.Black;
			}		


			if (psReq.Trim().ToUpper() == "Y")
			{
				psQuestionDesc+= "<span class='mandatory'>*</span>";
			}

			if (psQuestionType == "10")
			{
				//for heading
				TableCell tcQuestion = new TableCell();
				tcQuestion.Font.Bold=true;
				tcQuestion.Text = psQuestionDesc;
				tcQuestion.ColumnSpan=MaxCols;
				tcQuestion.CssClass="tableHeader";
				tcQuestion.VerticalAlign = VerticalAlign.Middle;
				tcCollection.Add(tcQuestion);
			}
			else if (psQuestionType == "9" || psQuestionType == "8")
			{
				//for vertical and horizontal grid
				tcGrid.ColumnSpan=MaxCols;	
				((Table)tcGrid.Controls[0]).Rows[0].Cells[0].Text = psQuestionDesc;
				((Table)tcGrid.Controls[0]).Rows[0].Cells[0].Font.Bold=true;
				if(psQNotes.Trim()!="")
				{
					tcGrid.Controls.Add(lblNotes);
				}
				tcCollection.Add(tcGrid);
			}
			else
			{
				TableCell tcQuestion = new TableCell();
				//tcQuestion.Font.Bold=true;
				tcQuestion.Text = psQuestionDesc;
				tcQuestion.VerticalAlign = VerticalAlign.Middle;
				tcPrefix.VerticalAlign = VerticalAlign.Middle;
				tcControl.VerticalAlign = VerticalAlign.Middle;
				tcQuestion.Wrap=false;

				if (blAddBlankCell)
				{
					tcCollection.Add(tcBlankCell);
				}
				
				//tcQuestion.Width = Unit.Percentage(20);
				if(psQNotes.Trim()!="")
				{
					tcControl.Controls.Add(lblNotes);
				}

				tcCollection.Add(tcQuestion);
				tcCollection.Add(tcPrefix);
				tcCollection.Add(tcControl);
			}
			

			//if (tblUDScreen.Rows[tblUDScreen.Rows.Count - 1].Cells.Count == 3 && tblUDScreen.Rows[tblUDScreen.Rows.Count - 1].Cells[tblUDScreen.Rows[tblUDScreen.Rows.Count - 1].Cells.Count -1].ColumnSpan == 0)
			if (liSpaceRemaining >=  liSpaceNeeded)
			{
				//add code to put two controls in a row only
			}
			else
			{
				//adding row to table
				if (psQuestionType != "9" && psQuestionType != "8" && psQuestionType != "10")
				{
					trBlankRow.Height = Unit.Pixel(24);
				}
				if (psQuestionType == "10")
				{
					trBlankRow.Height = Unit.Pixel(28);
					trBlankRow.VerticalAlign= VerticalAlign.Middle;
				}
				
				tblUDScreen.Rows.Add(trBlankRow);
				
				if (psQuestionType == "9" || psQuestionType == "8")
				{
					TableRow loRow = new TableRow();
					TableCell loCell = new TableCell();
					loCell.ColumnSpan=MaxCols;
					loRow.Cells.Add(loCell);
					loRow.Height =Unit.Pixel(4);
					loRow.CssClass = "midcolora";
					tblUDScreen.Rows.Add(loRow);
				}
			}
		}


		public void getQuestionControl(ref TableCell tcGridCell,ref TableCell tcControlCell,ref TableCell tcPrefixCell, string QuestionID,int Question_Type,string lsListId,string lsListType,string lsTableName,string lsOtherFlag,string lsOtherCmpValue,string psRequired,string lsPrefix,string lsSuffix,string psControlStyle,string psControlValidationType,string psControlMaxLength,string psQuesAnswer,string psOptAnswer,string psOptionAnswerID,string psOthAnswerDesc,string lsDepQuestionText,string lsDepQuesType,string lsDepAnsType,string lsDepQuesList)
		{
			//based upon question type generating the Control
			objUserQuestion = new ClsUserQuestion();
			DataSet ldsGridQusetions = null;
			string lsCarrierID = "1";
			compFieldValidator = new CompareValidator();

			switch (Question_Type)
			{
				case 1:
					#region DROP DOWN LIST
					cboUserList = new DropDownList();					
					cboUserList.ID="Ques_" + QuestionID + "_0_0_" + Question_Type;
					
					if(gstrCombineQID!="")
						gstrCombineQID = gstrCombineQID + "#" + cboUserList.ID;
					else
						gstrCombineQID = cboUserList.ID;
					

					cboUserList.CssClass="input";
					if(lsListId.Trim()!="")
					{
						cboUserList.DataSource = new DataView(objUserQuestion.GetUserDefineList(lsListId,lsListType,lsTableName).Tables[0]);
						cboUserList.DataTextField="OPTIONNAME";
						cboUserList.DataValueField="COMBINEID";
						cboUserList.DataBind();
						if (cboUserList.Items.Count == 0)
						{
							cboUserList.Width=Unit.Pixel(50);
						}
						cboUserList.Items.Insert(0, new ListItem("", ""));
						cboUserList.SelectedIndex = 0;

						if (psControlStyle.IndexOf("disable") != -1)
						{
							cboUserList.Enabled= false;
						}
						if (psControlStyle != "")
						{
							cboUserList.Attributes.Add("style",psControlStyle.Replace("disable",""));
						}
						
						
						tcControlCell.Controls.Add(cboUserList);
					}
					//if lsOtherFlag value is 'Y' then we know that we have to provide the text box
					//for entering the description 
					if(lsOtherFlag == "Y" && lsDepQuesType != "")
					{
						///preparing the string in the case of we have to provide the text box on selection of any of the drop down value. 
						///which consist of of the id of the control and the value on which we have to enable the text box. which is access into the javascript
							
							
						//addding depenedent question
						HtmlGenericControl loSpan = new HtmlGenericControl("span");
						loSpan.ID = "span_" + QuestionID +  "_0_0_" + Question_Type;
						

						lblOthDesc = new Label();
						lblOthDesc.Text="&nbsp;&nbsp;" + lsDepQuestionText + "&nbsp;";
						loSpan.Controls.Add(lblOthDesc);
							
						int liDepQuesType = int.Parse(lsDepQuesType);

						Control baseControl = new Control();
						string lsControlId = "AnswDesc_" + QuestionID +  "_0_0_" + Question_Type;
						CompareValidator loBaseValid = null;
						HyperLink lhlDepDate = null;

						#region Logic For Dependent Question
						switch(liDepQuesType)
						{
							case 1:
								baseControl = new DropDownList();
								((DropDownList)baseControl).DataSource = new DataView(objUserQuestion.GetUserDefineList(lsDepQuesList,lsDepAnsType,lsTableName).Tables[0]);
								((DropDownList)baseControl).DataTextField="OPTIONNAME";
								((DropDownList)baseControl).DataValueField="COMBINEID";
								((DropDownList)baseControl).DataBind();
								((DropDownList)baseControl).Items.Insert(0, new ListItem("", ""));
								((DropDownList)baseControl).SelectedIndex = 0;
								break;
							case 2:
								baseControl = new ListBox();
								((ListBox)baseControl).DataSource =new DataView(objUserQuestion.GetUserDefineList(lsDepQuesList,lsDepAnsType,lsTableName).Tables[0]);
								((ListBox)baseControl).DataTextField="OPTIONNAME";
								((ListBox)baseControl).DataValueField="COMBINEID";
								((ListBox)baseControl).DataBind();
								((ListBox)baseControl).Items.Insert(0, new ListItem("", ""));
								((ListBox)baseControl).SelectedIndex = 0;
								break;
							case 4:
								baseControl = new TextBox();
								loBaseValid = new CompareValidator();
								loBaseValid.Type = System.Web.UI.WebControls.ValidationDataType.Integer;
								loBaseValid.ErrorMessage = lstrvldNumeric;
								break;
							case 5:
								baseControl = new TextBox();
								((TextBox)baseControl).ReadOnly=true;
								lhlDepDate = new HyperLink();
								lhlDepDate.ImageUrl="../images/calender.gif";					
								lhlDepDate.Attributes.Add("OnClick","fPopCalendar(document.UserQuestion."+lsControlId+",document.UserQuestion."+lsControlId+")"); //Javascript Implementation for Calender
								break;
							case 6:
								baseControl = new TextBox();
								break;
							case 7:
								baseControl = new TextBox();
								((TextBox)baseControl).TextMode= TextBoxMode.MultiLine;
								break;
							case 13:
								((RadioButtonList)baseControl).DataSource = new DataView(objUserQuestion.GetUserDefineList(lsDepQuesList,lsDepAnsType,lsTableName).Tables[0]);
								((RadioButtonList)baseControl).DataTextField="OPTIONNAME";
								((RadioButtonList)baseControl).DataValueField="COMBINEID";
								((RadioButtonList)baseControl).DataBind();
								break;
							case 14:
								((CheckBoxList)baseControl).DataSource =new DataView(objUserQuestion.GetUserDefineList(lsDepQuesList,lsDepAnsType,lsTableName).Tables[0]);
								((CheckBoxList)baseControl).DataTextField="OPTIONNAME";
								((CheckBoxList)baseControl).DataValueField="COMBINEID";
								((CheckBoxList)baseControl).DataBind();
								break;
							default:
								baseControl = new TextBox();
								break;
						}
						#endregion


						if (baseControl != null)
						{
							baseControl.ID=  lsControlId;
							loSpan.Controls.Add(baseControl);
						}
						if (lhlDepDate != null)
						{
							loSpan.Controls.Add(lhlDepDate);
						}
						if (loBaseValid != null)
						{
							loBaseValid.Operator = System.Web.UI.WebControls.ValidationCompareOperator.DataTypeCheck;
							loBaseValid.ControlToValidate = baseControl.ID;
							loBaseValid.Display=System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
							loSpan.Controls.Add(loBaseValid);
						}

						reqFieldValidator = new RequiredFieldValidator();
						reqFieldValidator.ErrorMessage =lstrvldBlank;
						reqFieldValidator.ControlToValidate = baseControl.ID;
						reqFieldValidator.Display=System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
						
						loSpan.Controls.Add(reqFieldValidator);
						

						
						loSpan.Style.Add("display","none");
						cboUserList.Attributes.Add("onchange","fnShowControl(this,'" + lsListId + "#" + lsOtherCmpValue + "','" + loSpan.ID +"')");
							
						
						tcControlCell.Controls.Add(loSpan);
						//tcControlCell.Controls.Add(txtControl);

					}
					////if the Required field is yes then we add the required field validator
					if(psRequired.Trim() == "Y")
					{
						reqFieldValidator = new RequiredFieldValidator();
						reqFieldValidator.ErrorMessage =lstrvldSelect;
						reqFieldValidator.ControlToValidate = cboUserList.ID;
						reqFieldValidator.Display=System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
						tcControlCell.Controls.Add(reqFieldValidator);
					}
					
					#endregion
					break;
				case 2:
					#region Multiple List box control
					if(lsPrefix != "")
					{
						lblPrefix = new Label();
						lblPrefix.ID = "lblPrefix_" + QuestionID + "_0_0_" + Question_Type;
						lblPrefix.Text =  lsPrefix;
						tcPrefixCell.Controls.Add(lblPrefix);	
					}
					
						
						
					ListControl = new ListBox();
					ListControl.ID= "Ques_" + QuestionID + "_0_0_" + Question_Type;
					if(gstrCombineQID!="")
						gstrCombineQID = gstrCombineQID + "#" + ListControl.ID;
					else
						gstrCombineQID = ListControl.ID;

					ListControl.DataSource = new DataView(objUserQuestion.GetUserDefineList(lsListId,lsListType,lsTableName).Tables[0]);
					ListControl.DataTextField="OPTIONNAME";
					ListControl.DataValueField="COMBINEID";
					ListControl.DataBind();

					if (psControlStyle.IndexOf("disable") != -1)
					{
						ListControl.Enabled= false;
					}
					if (psControlStyle != "")
					{
						ListControl.Attributes.Add("style",psControlStyle.Replace("disable",""));
					}


					if (ListControl.Items.Count == 0)
					{
						ListControl.Width=Unit.Pixel(50);
					}

					ListControl.CssClass="input";
					ListControl.SelectionMode = System.Web.UI.WebControls.ListSelectionMode.Multiple;

					
						
					tcControlCell.Controls.Add(ListControl);
					if(lsSuffix != "")
					{
						lblSuffix = new Label();
						lblSuffix.ID = "lblSuffix_" + QuestionID;
						lblSuffix.Text =  lsSuffix;
						tcControlCell.Controls.Add(lblSuffix);	
					}
						
					////if the Required field is yes then we add the required field validator
					if(psRequired=="Y")
					{
						reqFieldValidator = new RequiredFieldValidator();
						reqFieldValidator.ErrorMessage =lstrvldSelect;
						reqFieldValidator.ControlToValidate = ListControl.ID;
						reqFieldValidator.Display=System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
						tcControlCell.Controls.Add(reqFieldValidator);
					}
					#endregion
					break;
				case 4:			
					#region NUMBER FIELD TEXT BOX
					if(lsPrefix != "")
					{
						lblPrefix = new Label();
						lblPrefix.ID = "lblPrefix_" + QuestionID + "_0_0_" + Question_Type;
						lblPrefix.Text =  lsPrefix;
						tcPrefixCell.Controls.Add(lblPrefix);	
					}
						
					txtControl = new TextBox();					
					txtControl.ID = "Ques_" + QuestionID + "_0_0_" + Question_Type;

					if(gstrCombineQID!="")
						gstrCombineQID = gstrCombineQID + "#" + txtControl.ID;
					else
						gstrCombineQID = txtControl.ID;


					if (psControlStyle.IndexOf("disable") != -1)
					{
						txtControl.Enabled= false;
					}
					if (psControlStyle != "")
					{
						txtControl.Attributes.Add("style",psControlStyle.Replace("disable",""));
					}
					if (psControlMaxLength != "" && psControlMaxLength != "0")
					{
						txtControl.MaxLength =  int.Parse(psControlMaxLength);
					}
					

					tcControlCell.Controls.Add(txtControl);	
						
					if(lsSuffix!="")
					{
						lblSuffix = new Label();
						lblSuffix.ID = "lblSuffix_" + QuestionID;
						lblSuffix.Text =  lsSuffix;
						tcControlCell.Controls.Add(lblSuffix);	
					}

					////if the Required field is yes then we add the required field validator
					if(psRequired=="Y")
					{
						reqFieldValidator = new RequiredFieldValidator();
						reqFieldValidator.ErrorMessage =lstrvldNumeric;
						reqFieldValidator.ControlToValidate = txtControl.ID;
						reqFieldValidator.Display=System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
						tcControlCell.Controls.Add(reqFieldValidator);
					}

					compFieldValidator.Type = System.Web.UI.WebControls.ValidationDataType.Integer;
					compFieldValidator.Operator = System.Web.UI.WebControls.ValidationCompareOperator.DataTypeCheck;
					compFieldValidator.ControlToValidate = txtControl.ID;
					compFieldValidator.Display=System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
					compFieldValidator.ErrorMessage = lstrvldNumeric;
					tcControlCell.Controls.Add(compFieldValidator);
					#endregion
					break;
				case 5:
					#region Date field control
					if(lsPrefix!="")
					{
						lblPrefix = new Label();
						lblPrefix.ID = "lblPrefix_" + QuestionID + "_0_0_" +Question_Type;
						lblPrefix.Text =  lsPrefix;
						tcPrefixCell.Controls.Add(lblPrefix);	
					}

					
					

					txtControl = new TextBox();					
					hrCalender = new HyperLink();
					txtControl.ID = "Ques_" + QuestionID + "_0_0_" + Question_Type;
					txtControl.ReadOnly=true;

					if(gstrCombineQID!="")
						gstrCombineQID = gstrCombineQID + "#" + txtControl.ID;
					else
						gstrCombineQID = txtControl.ID;
					
					hrCalender.ImageUrl="../images/calender.gif";					
					hrCalender.Attributes.Add("OnClick","fPopCalendar(document.UserQuestion."+txtControl.ID+",document.UserQuestion."+txtControl.ID+")"); //Javascript Implementation for Calender
					txtControl.CssClass="input";
					if (psControlStyle.IndexOf("disable") != -1)
					{
						txtControl.Enabled= false;
						hrCalender.Enabled= false;
					}
					

					tcControlCell.Controls.Add(txtControl);	
					tcControlCell.Controls.Add(hrCalender);	
					if(lsSuffix!="")
					{
						lblSuffix = new Label();
						lblSuffix.ID = "lblSuffix_" + QuestionID;
						lblSuffix.Text =  lsSuffix;
						tcControlCell.Controls.Add(lblSuffix);	
					}
						
					////if the Required field is yes then we add the required field validator
					if(psRequired.Trim() == "Y")
					{
						reqFieldValidator = new RequiredFieldValidator();
						reqFieldValidator.ErrorMessage = lstrvldBlank;
						reqFieldValidator.ControlToValidate = txtControl.ID;
						reqFieldValidator.Display=System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
						tcControlCell.Controls.Add(reqFieldValidator);
					}
						

					#endregion
					break;
				case 6:
					#region Text box control
					if(lsPrefix != "")
					{
						lblPrefix = new Label();
						lblPrefix.ID  = "lblPrefix_" + QuestionID + "_0_0_" + Question_Type;
						lblPrefix.Text =  lsPrefix;
						tcPrefixCell.Controls.Add(lblPrefix);	
					}

						
					txtControl = new TextBox();					
					txtControl.ID = "Ques_" + QuestionID + "_0_0_" + Question_Type;				

					if(gstrCombineQID!="")
						gstrCombineQID = gstrCombineQID + "#" + txtControl.ID;
					else
						gstrCombineQID = txtControl.ID;

					txtControl.CssClass="input";
					if (psControlStyle.IndexOf("disable") != -1)
					{
						txtControl.Enabled= false;
					}
					if (psControlStyle != "")
					{
						txtControl.Attributes.Add("style",psControlStyle.Replace("disable",""));
					}
					if (psControlMaxLength != "" && psControlMaxLength != "0")
					{
						txtControl.MaxLength = int.Parse(psControlMaxLength);
					}
					
					tcControlCell.Controls.Add(txtControl);
						

					if(lsSuffix!="")
					{
						lblSuffix = new Label();
						lblSuffix.ID = "lblSuffix_"+QuestionID;
						lblSuffix.Text =  lsSuffix;
						tcControlCell.Controls.Add(lblSuffix);	
					}

					////if the Required field is yes then we add the required field validator
					if(psRequired == "Y")
					{
						reqFieldValidator = new RequiredFieldValidator();
						reqFieldValidator.ErrorMessage =lstrvldBlank;
						reqFieldValidator.ControlToValidate = txtControl.ID;
						reqFieldValidator.Display=System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
						tcControlCell.Controls.Add(reqFieldValidator);
					}

					if (psControlValidationType != "")
					{
						RegularExpressionValidator loValid = new RegularExpressionValidator();
						loValid.ErrorMessage = "<br>Invalid value for this field.";
						loValid.ValidationExpression =psControlValidationType;
						loValid.ControlToValidate = txtControl.ID;
						loValid.Display=System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
						tcControlCell.Controls.Add(loValid);
					}
					#endregion
					break;
				case 7:
					#region TextArea control
					if(lsPrefix != "")
					{
						lblPrefix = new Label();
						lblPrefix.ID = "lblPrefix_" + QuestionID + "_0_0_" + Question_Type;
						lblPrefix.Text =  lsPrefix;
						tcPrefixCell.Controls.Add(lblPrefix);	
					}
					txtControl = new TextBox();					
					txtControl.ID = "Ques_" + QuestionID + "_0_0_" + Question_Type;
					if(gstrCombineQID!="")
						gstrCombineQID = gstrCombineQID + "#" + txtControl.ID;
					else
						gstrCombineQID = txtControl.ID;

					txtControl.TextMode=System.Web.UI.WebControls.TextBoxMode.MultiLine;
					txtControl.Rows = 3;
					txtControl.Columns = 30;
					txtControl.CssClass="input";
					if (psControlStyle.IndexOf("disable") != -1)
					{
						txtControl.Enabled= false;
					}
					if (psControlStyle != "")
					{
						txtControl.Attributes.Add("style",psControlStyle.Replace("disable",""));
					}
					if (psControlMaxLength != "" && psControlMaxLength != "0")
					{
						txtControl.MaxLength =  int.Parse(psControlMaxLength);
					}
					


					tcControlCell.Controls.Add(txtControl);
						
					if(lsSuffix!="")
					{
						lblSuffix = new Label();
						lblSuffix.ID = "lblSuffix_" + QuestionID;
						lblSuffix.Text =  lsSuffix;
						tcControlCell.Controls.Add(lblSuffix);	
					}

					//if the Required field is yes then we add the required field validator
					if(psRequired.Trim() == "Y")
					{
						reqFieldValidator = new RequiredFieldValidator();
						reqFieldValidator.ErrorMessage =lstrvldBlank;
						reqFieldValidator.ControlToValidate = txtControl.ID;
						reqFieldValidator.Display=System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
						tcControlCell.Controls.Add(reqFieldValidator);
					}
					if (psControlValidationType != "")
					{
						RegularExpressionValidator loValid = new RegularExpressionValidator();
						loValid.ErrorMessage = "Invalid value for this field.";
						loValid.ValidationExpression =psControlValidationType;
						loValid.ControlToValidate = txtControl.ID;
						loValid.Display=System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
						tcControlCell.Controls.Add(loValid);
					}
					#endregion
					break;	
				case 10:
					#region Label question
					lblControl = new Label();
					lblControl.ID = "Ques_" + QuestionID + "_0_0_" + Question_Type;
					tcControlCell.Controls.Add(lblControl);
					#endregion
					break;
				case 9:
					#region HORIZONTAL GRID QUESTION
					
					//int lintUserQuestionCount  = 0;
					//int lintPostQuestionCount = 0;
					///if the user is coming for the first time then we are calculating how many rows we have to show him
					///in the horizontal grid.
					///in case of add new and save we are not reading it from db.
					
					ldsGridQusetions = objUserQuestion.GetGridQuestion(Convert.ToInt32(QuestionID),lsCarrierID,Request["TabID"],lblhidScreenID.Text);			
					int liGridOptId;
					int lintGridOptTypeID;
					string lstrOptionText;
						
					Table loTable = new Table();
					loTable.CellSpacing=1;
					loTable.CellPadding=0;
					loTable.BorderWidth = Unit.Pixel(0);
					loTable.GridLines= GridLines.Both;
					


					
					loTable.Width=Unit.Percentage(95);

					TableRow trRows = new TableRow();
					TableCell tcHead = new TableCell();
					tcHead.ColumnSpan = ldsGridQusetions.Tables[0].Rows.Count  +1;
					tcHead.CssClass="midcolora";
					trRows.Cells.Add(tcHead);

					loTable.Rows.Add(trRows);

					#region adding heading of the horizontal grid
					//***********************************************
					//adding heading of the horizontal grid
					//***********************************************

					TableRow trHeadingRow = new TableRow();
					trHeadingRow.CssClass="tabcls";
					trHeadingRow.Height=Unit.Pixel(20);

					TableCell tcHeadingCell = null;
						
					foreach(DataRow drGridRow in  ldsGridQusetions.Tables[0].Rows)
					{

						liGridOptId = int.Parse(drGridRow["QGRIDOPTIONID"].ToString());
						lstrOptionText = drGridRow["OPTIONTEXT"].ToString();
						tcHeadingCell = new TableCell();
							
						if(lstrOptionText.Trim()!="")
						{	
							tcHeadingCell = new TableCell();
							lblGrid =  new Label();
							lblGrid.ID = "Grid_" + QuestionID + "_" + liGridOptId;
							lblGrid.Text = lstrOptionText;	
							tcHeadingCell.Controls.Add(lblGrid);
							tcHeadingCell.Width= Unit.Percentage(95/ldsGridQusetions.Tables[0].Rows.Count);
							trHeadingRow.Controls.Add(tcHeadingCell);
							trHeadingRow.CssClass="tabcls";
						}
					}
					trHeadingRow.Cells.Add(new TableCell());

					loTable.Rows.Add(trHeadingRow);
					#endregion
						
					if(IsPostBack)
					{ 
						if(Request["btnAddNew"+QuestionID]=="Add New")
						{	
							if (ViewState["HorControlCounter" + QuestionID] != null)
							{
								ViewState["HorControlCounter" + QuestionID] = int.Parse(ViewState["HorControlCounter" + QuestionID].ToString()) + 1;
							}
						}
					}
					if (!IsPostBack)
					{
						ViewState["HorControlCounter" + QuestionID] = 1;
					}
					

					for (int iHGCtr=1;iHGCtr<=int.Parse(ViewState["HorControlCounter" + QuestionID].ToString());iHGCtr++)
					{	
						#region Adding columns In Horizontal Grid
						
						string lsGridIsReq;
						//string lstrGridAnswer="";						
							
						

						TableRow trNewRow = new TableRow();
						trNewRow.CssClass="midcolorc";
						trNewRow.Height = Unit.Pixel(22);
						trNewRow.VerticalAlign = VerticalAlign.Middle;

						TableCell tcColumn = null;

						foreach(DataRow drGridRow in  ldsGridQusetions.Tables[0].Rows)
						{	

							tcColumn			= new TableCell();
							liGridOptId			= int.Parse(drGridRow["QGRIDOPTIONID"].ToString());
							lsGridIsReq			= drGridRow["ISREQUIRED"].ToString();
							lintGridOptTypeID	= int.Parse(drGridRow["OPTIONTYPEID"].ToString());
							lsListId			= drGridRow["LISTID"].ToString();

							//fetching answer of question
							

							switch(lintGridOptTypeID)
							{
								case 1:		
									#region DROP DOWN LIST
									cboUserList		  = new DropDownList();
									reqFieldValidator = new RequiredFieldValidator();
									cboUserList.ID	  = "Ques_" + QuestionID + "_" + liGridOptId + "_" + iHGCtr + "_" + lintGridOptTypeID;

									if(gstrCombineQID!="")
										gstrCombineQID = gstrCombineQID + "#" + cboUserList.ID;
									else
										gstrCombineQID = cboUserList.ID;
									
									if(lsListId.Trim()!="")
									{
										cboUserList.DataSource = new DataView(objUserQuestion.GetUserDefineList(lsListId,lintGridOptTypeID.ToString(),lsTableName).Tables[0]);
										cboUserList.DataTextField="OPTIONNAME";
										cboUserList.DataValueField="COMBINEID";			
										cboUserList.DataBind();
										if (cboUserList.Items.Count == 0)
										{
											cboUserList.Width=Unit.Pixel(50);
										}
										cboUserList.CssClass="input";
									}
									

									tcColumn.Controls.Add(cboUserList);

									//if the Required field is yes then we add the required field validator
									if(lsGridIsReq=="Y")
									{
										reqFieldValidator.ErrorMessage ="<br>" + lstrvldSelect;
										reqFieldValidator.ControlToValidate = cboUserList.ID;
										reqFieldValidator.Display = ValidatorDisplay.Dynamic;
										tcColumn.Controls.Add(reqFieldValidator);
									}
									#endregion
									break;
								case 2:
									#region LIST BOX
									ListControl = new ListBox();
									reqFieldValidator = new RequiredFieldValidator();
									ListControl.ID="Ques_"+QuestionID+"_"+liGridOptId + "_" + iHGCtr + "_" + lintGridOptTypeID;
									if(gstrCombineQID!="")
										gstrCombineQID = gstrCombineQID + "#" + ListControl.ID;
									else
										gstrCombineQID = ListControl.ID;

									ListControl.DataSource = new DataView(objUserQuestion.GetUserDefineList(lsListId,lintGridOptTypeID.ToString(),lsTableName).Tables[0]);
									ListControl.DataTextField="OPTIONNAME";
									ListControl.DataValueField="COMBINEID";
									ListControl.DataBind();
									if (ListControl.Items.Count == 0)
									{
										ListControl.Width=Unit.Pixel(50);
									}
									ListControl.CssClass="input";

									ListControl.SelectionMode = System.Web.UI.WebControls.ListSelectionMode.Multiple;							

									
									tcColumn.Controls.Add(ListControl);
								
									//if the Required field is yes then we add the required field validator
									if(lsGridIsReq=="Y")
									{
										reqFieldValidator.ErrorMessage ="<br>" + lstrvldSelect;
										reqFieldValidator.ControlToValidate = ListControl.ID;
										reqFieldValidator.Display = ValidatorDisplay.Dynamic;
										tcColumn.Controls.Add(reqFieldValidator);
									}
									#endregion
									break;
								case 4:
									#region NUMBER FIELD TEXT BOX
									compFieldValidator  = new CompareValidator();
									txtControl			= new TextBox();					
									txtControl.ID		= "Ques_"+QuestionID+"_"+liGridOptId+"_"+  iHGCtr + "_" + lintGridOptTypeID;
									txtControl.CssClass	= "input";	
									
									

									tcColumn.Controls.Add(txtControl);
									if(gstrCombineQID!="")
										gstrCombineQID = gstrCombineQID + "#" + txtControl.ID;
									else
										gstrCombineQID = txtControl.ID;
									
									//if the Required field is yes then we add the required field validator
									if(lsGridIsReq=="Y")
									{
										reqFieldValidator = new RequiredFieldValidator();
										reqFieldValidator.ErrorMessage ="<br>" + lstrvldBlank;
										reqFieldValidator.ControlToValidate = txtControl.ID;
										reqFieldValidator.Display = ValidatorDisplay.Dynamic;
										tcColumn.Controls.Add(reqFieldValidator);
									}
									compFieldValidator.Type = System.Web.UI.WebControls.ValidationDataType.Integer;
									compFieldValidator.Operator = System.Web.UI.WebControls.ValidationCompareOperator.DataTypeCheck;
									compFieldValidator.ControlToValidate = txtControl.ID;
									compFieldValidator.ErrorMessage = lstrvldNumeric;
									compFieldValidator.Display = ValidatorDisplay.Dynamic;
									tcColumn.Controls.Add(compFieldValidator);
									#endregion
									break;
								case 5:
									#region DATE FIELD TEXT BOX

									compFieldValidator = new CompareValidator();
									hrCalender = new HyperLink();
									txtControl = new TextBox();	
									
									txtControl.ID = "Ques_"+QuestionID + "_" + liGridOptId + "_" + iHGCtr + "_" +  lintGridOptTypeID;
									if(gstrCombineQID!="")
										gstrCombineQID = gstrCombineQID + "#" + txtControl.ID;
									else
										gstrCombineQID = txtControl.ID;

									hrCalender.ImageUrl="../images/calender.gif";
									hrCalender.Attributes.Add("OnClick","fPopCalendar(document.UserQuestion."+txtControl.ID+",document.UserQuestion."+txtControl.ID+")");
									
									

									txtControl.CssClass="input";
									tcColumn.Controls.Add(txtControl);
									tcColumn.Controls.Add(hrCalender);
									
									//if the Required field is yes then we add the required field validator
									if(lsGridIsReq=="Y")
									{
										reqFieldValidator = new RequiredFieldValidator();
										reqFieldValidator.ErrorMessage ="<br>" + lstrvldBlank;
										reqFieldValidator.ControlToValidate = txtControl.ID;
										reqFieldValidator.Display = ValidatorDisplay.Dynamic;
										tcColumn.Controls.Add(reqFieldValidator);
									}
									//									compFieldValidator.Type = System.Web.UI.WebControls.ValidationDataType.Date;
									//									compFieldValidator.Operator = System.Web.UI.WebControls.ValidationCompareOperator.DataTypeCheck;
									//									compFieldValidator.ControlToValidate = txtControl.ID;
									//									compFieldValidator.Display = ValidatorDisplay.Dynamic;
									//									compFieldValidator.ErrorMessage = lstrvldNumeric;
									#endregion
									break;
								case 6:
									#region TEXT BOX CONTROL
									txtControl = new TextBox();					
									txtControl.ID = "Ques_"+QuestionID + "_" + liGridOptId + "_" + iHGCtr + "_" +  lintGridOptTypeID;
									if(gstrCombineQID!="")
										gstrCombineQID = gstrCombineQID + "#" + txtControl.ID;
									else
										gstrCombineQID = txtControl.ID;
									
									
									
									tcColumn.Controls.Add(txtControl);
									
									//if the Required field is yes then we add the required field validator
									if(lsGridIsReq=="Y")
									{
										reqFieldValidator = new RequiredFieldValidator();
										reqFieldValidator.ErrorMessage ="<br>" + lstrvldBlank;
										reqFieldValidator.ControlToValidate = txtControl.ID;
										reqFieldValidator.Display = System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
										tcColumn.Controls.Add(reqFieldValidator);
									}
									#endregion
									break;
								case 7:						
									#region TEXT AREA CONTROL
									txtControl = new TextBox();					
									txtControl.ID = "Ques_"+QuestionID+"_"+liGridOptId+"_" + iHGCtr + "_" +  lintGridOptTypeID;
									if(gstrCombineQID!="")
										gstrCombineQID = gstrCombineQID + "#" + txtControl.ID;
									else
										gstrCombineQID = txtControl.ID;
									txtControl.TextMode=System.Web.UI.WebControls.TextBoxMode.MultiLine;
									txtControl.Rows = 3;
									txtControl.Columns = 30;
									

									tcColumn.Controls.Add(txtControl);
									
									//if the Required field is yes then we add the required field validator
									if(lsGridIsReq=="Y")
									{
										reqFieldValidator = new RequiredFieldValidator();
										reqFieldValidator.ErrorMessage ="<br>" + lstrvldBlank;
										reqFieldValidator.ControlToValidate = txtControl.ID;
										reqFieldValidator.Display=System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
										tcColumn.Controls.Add(reqFieldValidator);
									}
									#endregion
									break;		
							}
							trNewRow.Cells.Add(tcColumn);

						
						}
						TableCell tcButton = new TableCell();
						trNewRow.Cells.Add(tcButton);
						
						
						loTable.Rows.Add(trNewRow);
						#endregion
					}
					
					btnAddNew = new Button();
					btnAddNew.ID = "btnAddNew" + QuestionID;
					btnAddNew.CausesValidation=false;
					//btnAddNew.Attributes.Add("onclick","return duplicateRows(this.parentElement.parentElement.parentElement);");
					btnAddNew.Text = lstrbtnAddNew;
					btnAddNew.CssClass="clsbutton";
					loTable.Rows[loTable.Rows.Count-1].Cells[loTable.Rows[loTable.Rows.Count-1].Cells.Count-1].Controls.Add(btnAddNew);

					tcGridCell.Controls.Add(loTable);
							
					#endregion
					break;
				case 8:
					#region VERTICAL GRID QUESTION
					ldsGridQusetions = objUserQuestion.GetGridVerticalQuestion(Convert.ToInt32(QuestionID),lsCarrierID,Request["TabID"],lblhidScreenID.Text);
					string lsVOptionText;
					int liVGridOptId;
					int liVGridOptTypeId;
					string lsVGridIsReq;
					string lsVListId;
					//string lsVAnswer = "";
						
					Table loVTable = new Table();
					loVTable.CellSpacing=1;
					loVTable.Width = Unit.Percentage(95);

					DataRow[] HorizontalArray = ldsGridQusetions.Tables[0].Select("GRIDOPTIONLAYOUT = 'H'");
						
					#region Adding the Heading

					
					TableRow trVRows = new TableRow();
					TableCell tcVHead = new TableCell();
					tcVHead.ColumnSpan = HorizontalArray.Length + 1;
					tcVHead.CssClass="midcolora";
					trVRows.Cells.Add(tcVHead);
					loVTable.Rows.Add(trVRows);


					TableRow trVHead = new TableRow();
					trVHead.CssClass="tabcls";
					trVHead.Height = Unit.Pixel(20);
					TableCell tcVHeadColumn = new TableCell();
					tcVHeadColumn.Width=Unit.Percentage(20);
					trVHead.Cells.Add(tcVHeadColumn);
					
					
					foreach(DataRow ldrGridQuestion in HorizontalArray)
					{
						tcVHeadColumn = new TableCell();
						lblGrid = new Label();				
						liVGridOptId = int.Parse(ldrGridQuestion["QGRIDOPTIONID"].ToString());
						lsVOptionText = ldrGridQuestion["OPTIONTEXT"].ToString();
						
						if(lsVOptionText.Trim()!="")
						{
							lblGrid.ID = "Grid_Heading_" + QuestionID + "_" + liVGridOptId;
							lblGrid.Text = lsVOptionText;
							tcVHeadColumn.Controls.Add(lblGrid);
							tcVHeadColumn.Width = Unit.Percentage(75/HorizontalArray.Length);
							trVHead.Cells.Add(tcVHeadColumn);
						}
					}	
					loVTable.Rows.Add(trVHead);
					#endregion
					
					#region GETTING THE VERTICAL GRID QUESTIONS
					DataRow[] VerticalArray = ldsGridQusetions.Tables[0].Select("GRIDOPTIONLAYOUT = 'V'");

					TableRow trVGridQues = null;
					TableCell tcVGridQuestion = null;
					TableCell tcHGridQuestion = null;

					
					foreach(DataRow ldrVGridQuestion in VerticalArray)
					{
						trVGridQues = new TableRow();
						tcVGridQuestion = new TableCell();

						liVGridOptId = int.Parse(ldrVGridQuestion["QGRIDOPTIONID"].ToString());
						lsVOptionText = ldrVGridQuestion["OPTIONTEXT"].ToString();
						
						//adding the Vertical Question In The Grid
						lblGrid = new Label();
						lblGrid.ID = "Grid_" + QuestionID + "_" + liVGridOptId;
						lblGrid.Text =lsVOptionText; 						
						tcVGridQuestion.CssClass="tabcls";						
						tcVGridQuestion.Controls.Add(lblGrid);
						trVGridQues.Cells.Add(tcVGridQuestion);
						//******************************************
						
				
						//creating the controls on the grid.
						foreach(DataRow ldrHGridQuestion in HorizontalArray)
						{	
							int liVHGridOptId  = int.Parse(ldrHGridQuestion["QGRIDOPTIONID"].ToString());
							liVGridOptTypeId = int.Parse(ldrHGridQuestion["OPTIONTYPEID"].ToString());
							lsVGridIsReq = ldrHGridQuestion["ISREQUIRED"].ToString();
							lsVListId = ldrHGridQuestion["LISTID"].ToString();						
							tcHGridQuestion = new TableCell();
							tcHGridQuestion.CssClass="midcolorc";

							
							switch(liVGridOptTypeId)
							{
								case 1:
									#region DROP DOWN LIST
									cboUserList = new DropDownList();
									reqFieldValidator = new RequiredFieldValidator();
									cboUserList.ID="Ques_"+QuestionID+"_"+liVGridOptId+"_"+liVHGridOptId+"_"+liVGridOptTypeId;
									
									if(gstrCombineQID!="")
										gstrCombineQID = gstrCombineQID + "#" + cboUserList.ID;
									else
										gstrCombineQID = cboUserList.ID;
									
									if(lsVListId.Trim()!="")
									{
										cboUserList.DataSource = new DataView(objUserQuestion.GetUserDefineList(lsVListId,liVGridOptTypeId.ToString(),lsTableName).Tables[0]);
										cboUserList.DataTextField="OPTIONNAME";
										cboUserList.DataValueField="COMBINEID";			
										cboUserList.DataBind();
										if (cboUserList.Items.Count == 0)
										{
											cboUserList.Width=Unit.Pixel(50);
										}
										cboUserList.CssClass="input";
									}			
									
									tcHGridQuestion.Controls.Add(cboUserList);
										
									//if the Required field is yes then we add the required field validator
									if(lsVGridIsReq=="Y")
									{
										reqFieldValidator.ErrorMessage =lstrvldSelect;
										reqFieldValidator.ControlToValidate = cboUserList.ID;
										reqFieldValidator.Display=System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
										tcHGridQuestion.Controls.Add(reqFieldValidator);
									}
									#endregion
									break;
								case 2:
									#region LIST BOX CONTROL
									ListControl = new ListBox();
									reqFieldValidator = new RequiredFieldValidator();
									ListControl.ID		="Ques_"+QuestionID+"_"+liVGridOptId+"_"+liVHGridOptId+"_"+liVGridOptTypeId;
									if(gstrCombineQID!="")
										gstrCombineQID = gstrCombineQID + "#" + ListControl.ID;
									else
										gstrCombineQID = ListControl.ID;

									ListControl.DataSource = new DataView(objUserQuestion.GetUserDefineList(lsVListId,liVGridOptTypeId.ToString(),lsTableName).Tables[0]);
									ListControl.DataTextField="OPTIONNAME";
									ListControl.DataValueField="COMBINEID";
									ListControl.DataBind();
									if (ListControl.Items.Count == 0)
									{
										ListControl.Width=Unit.Pixel(50);
									}
									ListControl.CssClass="input";
									ListControl.SelectionMode = System.Web.UI.WebControls.ListSelectionMode.Multiple;					
									

									tcHGridQuestion.Controls.Add(ListControl);
					
									if(lsVGridIsReq=="Y")
									{
										reqFieldValidator.ErrorMessage =lstrvldSelect;
										reqFieldValidator.ControlToValidate = ListControl.ID;
										reqFieldValidator.Display=System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
										tcHGridQuestion.Controls.Add(reqFieldValidator);
									}
									#endregion
									break;
								case 4:
									#region NUMBER FIELD TEXT BOX
									
									compFieldValidator = new CompareValidator();
									txtControl = new TextBox();					
									txtControl.ID = "Ques_"+QuestionID+"_"+liVGridOptId+"_"+liVHGridOptId+"_"+liVGridOptTypeId;
									if(gstrCombineQID!="")
										gstrCombineQID = gstrCombineQID + "#" + txtControl.ID;
									else
										gstrCombineQID = txtControl.ID;

									txtControl.CssClass="input";
									


									tcHGridQuestion.Controls.Add(txtControl);							

									//if the Required field is yes then we add the required field validator
									if(lsVGridIsReq=="Y")
									{
										reqFieldValidator = new RequiredFieldValidator();
										reqFieldValidator.ErrorMessage ="<br>" + lstrvldBlank;
										reqFieldValidator.ControlToValidate = txtControl.ID;
										reqFieldValidator.Display=System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
										tcHGridQuestion.Controls.Add(reqFieldValidator);
									}
									compFieldValidator.Type = System.Web.UI.WebControls.ValidationDataType.Integer;
									compFieldValidator.Operator = System.Web.UI.WebControls.ValidationCompareOperator.DataTypeCheck;
									compFieldValidator.ControlToValidate = txtControl.ID;
									compFieldValidator.Display = ValidatorDisplay.Dynamic;
									compFieldValidator.ErrorMessage = lstrvldNumeric;
									tcHGridQuestion.Controls.Add(compFieldValidator);
									#endregion
									break;
								case 5:
									#region DATE FIELD TEXT BOX
									
									txtControl = new TextBox();
									hrCalender = new HyperLink();
									txtControl.ID = "Ques_"+QuestionID+"_"+liVGridOptId+"_"+liVHGridOptId+"_"+liVGridOptTypeId;
									if(gstrCombineQID!="")
										gstrCombineQID = gstrCombineQID + "#" + txtControl.ID;
									else
										gstrCombineQID = txtControl.ID;

									txtControl.ReadOnly=true;
									hrCalender.ImageUrl="../images/calender.gif";
									hrCalender.Attributes.Add("OnClick","fPopCalendar(document.UserQuestion."+txtControl.ID+",document.UserQuestion."+txtControl.ID+")");
									txtControl.CssClass="input";
									
									tcHGridQuestion.Controls.Add(txtControl);
									tcHGridQuestion.Controls.Add(hrCalender);

									//if the Required field is yes then we add the required field validator
									if(lsVGridIsReq=="Y")
									{
										reqFieldValidator = new RequiredFieldValidator();
										reqFieldValidator.ErrorMessage =lstrvldBlank;
										reqFieldValidator.ControlToValidate = txtControl.ID;
										reqFieldValidator.Display=System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
										tcHGridQuestion.Controls.Add(reqFieldValidator);
									}

									#endregion
									break;
								case 6:
									#region TEXT BOX
									
									txtControl = new TextBox();					
									txtControl.ID = "Ques_"+QuestionID+"_"+liVGridOptId+"_"+liVHGridOptId+"_"+liVGridOptTypeId;

									if(gstrCombineQID!="")
										gstrCombineQID = gstrCombineQID + "#" + txtControl.ID;
									else
										gstrCombineQID = txtControl.ID;

									txtControl.CssClass="input";
									

									tcHGridQuestion.Controls.Add(txtControl);
									
									//if the Required field is yes then we add the required field validator
									if(lsVGridIsReq=="Y")
									{
										reqFieldValidator = new RequiredFieldValidator();
										reqFieldValidator.ErrorMessage =lstrvldBlank;
										reqFieldValidator.ControlToValidate = txtControl.ID;
										reqFieldValidator.Display=System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
										tcHGridQuestion.Controls.Add(reqFieldValidator);
									}
									#endregion
									break;
								case 7:
									#region TEXT AREA CONTROL
									txtControl = new TextBox();					
									txtControl.ID = "Ques_"+QuestionID+"_"+liVGridOptId+"_"+liVHGridOptId+"_"+liVGridOptTypeId;

									if(gstrCombineQID!="")
										gstrCombineQID = gstrCombineQID + "#" + txtControl.ID;
									else
										gstrCombineQID = txtControl.ID;

									txtControl.TextMode=System.Web.UI.WebControls.TextBoxMode.MultiLine;
									txtControl.CssClass="input";
									txtControl.Rows = 3;
									txtControl.Columns = 30;

									

									tcHGridQuestion.Controls.Add(txtControl);
									
									//if the Required field is yes then we add the required field validator
									if(lsVGridIsReq=="Y")
									{
										reqFieldValidator = new RequiredFieldValidator();
										reqFieldValidator.ErrorMessage =lstrvldBlank;
										reqFieldValidator.ControlToValidate = txtControl.ID;
										reqFieldValidator.Display=System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
										tcHGridQuestion.Controls.Add(reqFieldValidator);
									}
									#endregion
									break;
							}

							trVGridQues.Cells.Add(tcHGridQuestion);
						}
						
						loVTable.Rows.Add(trVGridQues);
					}			
					#endregion
						
					tcGridCell.Controls.Add(loVTable);
					#endregion
					break;
				case 13:
					#region Radio Button
					if(lsPrefix != "")
					{
						lblPrefix = new Label();
						lblPrefix.ID = "lblPrefix_" + QuestionID + "_0_0_" + Question_Type;
						lblPrefix.Text =  lsPrefix;
						tcPrefixCell.Controls.Add(lblPrefix);	
					}
						
					rbList = new RadioButtonList();
					if (lsListId.Trim() != "")
					{
					
						rbList.ID= "Ques_" + QuestionID + "_0_0_" + Question_Type;

						if(gstrCombineQID!="")
							gstrCombineQID = gstrCombineQID + "#" + rbList.ID;
						else
							gstrCombineQID = rbList.ID;

						
						rbList.DataSource = new DataView(objUserQuestion.GetUserDefineList(lsListId,lsListType,lsTableName).Tables[0]);
						rbList.DataTextField="OPTIONNAME";
						rbList.DataValueField="COMBINEID";
						rbList.DataBind();
					}

					rbList.RepeatColumns=3;
					rbList.RepeatDirection=RepeatDirection.Horizontal;
					rbList.RepeatLayout= RepeatLayout.Table;
					rbList.CssClass= "midcolora";


					

					if (rbList.Items.Count == 0)
					{
						rbList.Width=Unit.Pixel(50);
					}

					tcControlCell.Controls.Add(rbList);
					if(lsSuffix != "")
					{
						lblSuffix = new Label();
						lblSuffix.ID = "lblSuffix_" + QuestionID;
						lblSuffix.Text =  lsSuffix;
						tcControlCell.Controls.Add(lblSuffix);	
					}
						
					////if the Required field is yes then we add the required field validator
					if(psRequired=="Y" && rbList.Items.Count > 0)
					{
						rbList.Items[0].Selected=true;
					}
					#endregion
					break;
				case 14:
					#region Check Box
					if(lsPrefix != "")
					{
						lblPrefix = new Label();
						lblPrefix.ID = "lblPrefix_" + QuestionID + "_0_0_" + Question_Type;
						lblPrefix.Text =  lsPrefix;
						tcPrefixCell.Controls.Add(lblPrefix);	
					}
						
					chkBoxList = new CheckBoxList();
					if (lsListId.Trim() != "")
					{
						chkBoxList.ID= "Ques_" + QuestionID + "_0_0_" + Question_Type;
						
						chkBoxList.DataSource = new DataView(objUserQuestion.GetUserDefineList(lsListId,lsListType,lsTableName).Tables[0]);
						chkBoxList.DataTextField="OPTIONNAME";
						chkBoxList.DataValueField="COMBINEID";
						chkBoxList.DataBind();


						HtmlInputHidden loHidden = new HtmlInputHidden();
						loHidden.ID = chkBoxList.ID + "~" + chkBoxList.Items.Count;

						foreach(ListItem liValues in chkBoxList.Items)
						{
							loHidden.Value += liValues.Value + ",";
						}

						if(gstrCombineQID !="")
						{
							gstrCombineQID = gstrCombineQID + "#" +loHidden.ID;
							lsCheckBoxesLIst = lsCheckBoxesLIst + "#" +loHidden.ID;
						}
						else
						{
							gstrCombineQID = loHidden.ID;
							lsCheckBoxesLIst = loHidden.ID;
						}

						tcControlCell.Controls.Add(loHidden);


					}

					chkBoxList.RepeatColumns=3;
					chkBoxList.RepeatDirection=RepeatDirection.Horizontal;
					chkBoxList.RepeatLayout= RepeatLayout.Table;
					chkBoxList.CssClass= "midcolora";


					


					if (chkBoxList.Items.Count == 0)
					{
						chkBoxList.Width=Unit.Pixel(50);
					}

					tcControlCell.Controls.Add(chkBoxList);
					if(lsSuffix != "")
					{
						lblSuffix = new Label();
						lblSuffix.ID = "lblSuffix_" + QuestionID;
						lblSuffix.Text =  lsSuffix;
						tcControlCell.Controls.Add(lblSuffix);	
					}
						
					#endregion
					break;
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
