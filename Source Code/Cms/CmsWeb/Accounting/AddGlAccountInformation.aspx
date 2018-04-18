<%@ Page validateRequest=false language="c#" Codebehind="AddGlAccountInformation.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Accounting.AddGlAccountInformation" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>ACT_GL_ACCOUNTS</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">

function AddData()
{
    
    if(document.getElementById('hidDelete').value=="0")
    {
         
    
		DisableValidators();
		document.getElementById('hidACCOUNT_ID').value	=	'New';
		//document.getElementById('cmbCATEGORY_TYPE').focus();
		document.getElementById('cmbCATEGORY_TYPE').options.selectedIndex = 0;
		document.getElementById('cmbGROUP_TYPE').options.selectedIndex = 0;
		document.getElementById('txtACC_TYPE_DESC').value  = '';
		document.getElementById('cmbACC_PARENT_ID').options.selectedIndex = 0;
		document.getElementById('txtACC_NUMBER').value  = '';
		document.getElementById('cmbACC_LEVEL_TYPE').options.selectedIndex = -1;
		document.getElementById('txtACC_DESCRIPTION').value  = '';
		document.getElementById('cmbACC_TOTALS_LEVEL').options.selectedIndex = -1;
		document.getElementById('cmbACC_JOURNAL_ENTRY').options.selectedIndex = -1;
		if(document.getElementById('cmbACC_RELATES_TO_TYPE')!=null)
		document.getElementById('cmbACC_RELATES_TO_TYPE').options.selectedIndex = -1;		
		
		if(document.getElementById('cmbBUDGET_CATEGORY_ID')!=null)		
		document.getElementById('cmbBUDGET_CATEGORY_ID').options.selectedIndex = -1;
		
		if(document.getElementById('cmbWOLVERINE_USER_ID')!=null)		
		document.getElementById('cmbWOLVERINE_USER_ID').options.selectedIndex = -1;
		
		
		document.getElementById('chkACC_CASH_ACCOUNT').checked = false;
		SelectAllSectedTotalsOptions();
		DeselectTotalLevels();
		if(document.getElementById('btnActivateDeactivate'))
		document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
		
		if(document.getElementById('btnDelete'))
		document.getElementById('btnDelete').setAttribute('disabled',true);
		document.getElementById('lblRange').innerHTML = "";
		rfvACC_TOTALS_LEVEL_Selected.style.display="none";
		ChangeColor();
		ShowCash();
		
	}	
}
function populateXML()
{
	var tempXML = document.getElementById('hidOldData').value;
	
	DisableValidators();
	
	if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
	{

		if(tempXML!="")
		{   
			populateFormData(tempXML,ACT_GL_ACCOUNTS);
			SetPageDisplay();
			SelectTotalLevels();
			
			

		}
		else
		{
			ShowAccount();
			AddData();
		}
			
		
	}   
	    	
	DisplayBudgetCategory();
	SetPageDisplay();
	ShowAccount();
	ShowCash();
    
	setTab();
	return false;
}

function ResetForm(strID)
{
    document.getElementById('hidFormSaved').value = '0'
    document.forms[0].reset();
	populateXML();
	return false;
}

function setTab()
{
	var cashType = '<%=cashType%>';
	var cmbCatTyp= document.getElementById('cmbCATEGORY_TYPE');
	var cmbGroupTyp= document.getElementById('cmbGROUP_TYPE');
	var entityType = '';
	entityType = cmbCatTyp.options[cmbCatTyp.selectedIndex].text + "~";
	if(cmbGroupTyp.selectedIndex!=-1)
	{
		entityType += cmbGroupTyp.options[cmbGroupTyp.selectedIndex].text + "~";
	}
	else
	{
		entityType += ' ' + "~";
	}
	entityType += document.getElementById('txtACC_NUMBER').value;

	if (document.getElementById('hidOldData').value	!= '' && cashType == "Y")
	{			
		Url="AddBankInformation.aspx?EntityType=" + entityType + "&";
		DrawTab(2,this.parent,'Bank Information',Url);	
		RemoveTab(3,this.parent);		
	}
	else
	{		
		RemoveTab(3,this.parent);					
		RemoveTab(2,this.parent);	
	}
}
function ShowCashAccount(selectedIndex)
{
BlankHiddenData();//alert("aaaa");
//alert(selectedIndex);alert(parentType[selectedIndex]);//aa
if(selectedIndex==null)
	selectedIndex = document.getElementById('cmbGROUP_TYPE').selectedIndex-1;
else
	selectedIndex--;//as a blank row is there in sub-types dd
if(selectedIndex>=0)
{
	if(parentType[selectedIndex]==1)//i.e. equal asset
	{
		cashAccountCell1.style.display = "inline";
		cashAccountCell2.style.display = "inline";
		cashAccountBankCell.style.display = "none";
	}
	else
	{
		cashAccountCell1.style.display = "none";
		cashAccountCell2.style.display = "none";
		cashRow.style.display = "none";
		cashAccountBankCell.style.display = "inline";
	}
}
else
{
	cashAccountCell1.style.display = "none";
	cashAccountCell2.style.display = "none";
	cashAccountBankCell.style.display = "inline";
}
}
function ShowCashAccountForSubAccounts()
{
BlankHiddenData();
selectedIndex = document.getElementById('cmbACC_PARENT_ID').selectedIndex;
//alert(document.getElementById('cmbACC_PARENT_ID').options[selectedIndex].value);
if(selectedIndex>0)
{
	if(parentAcGroupType[document.getElementById('cmbACC_PARENT_ID').options[selectedIndex].value]==1)//i.e. equal asset
	{
		cashAccountCell1.style.display = "inline";
		cashAccountCell2.style.display = "inline";
		cashAccountBankCell.style.display = "none";
	}
	else
	{
		cashAccountCell1.style.display = "none";
		cashAccountCell2.style.display = "none";
		cashRow.style.display = "none";
		cashAccountBankCell.style.display = "inline";
	}
}
else
{
	cashAccountCell1.style.display = "none";
	cashAccountCell2.style.display = "none";
	cashAccountBankCell.style.display = "inline";
}
}
function ShowCash()
{
	if(document.getElementById('chkACC_CASH_ACCOUNT').checked)
		cashRow.style.display = "inline";
	else
	{
		
		cashRow.style.display = "none";
	}
	//ShowCashAccount();
}
function DisplayRange()
{ 
	var selectedIndex = document.getElementById('cmbGROUP_TYPE').selectedIndex-1;
	if(selectedIndex>-1)
	{
		document.getElementById('lblRange').innerHTML = "Range Lies Between "+fromRange[selectedIndex]+" and "+toRange[selectedIndex];
		document.getElementById('txtACC_TYPE_DESC').value = groups[parentType[selectedIndex]];
		//document.getElementById('lblACC_TYPE_DESC').innerHTML = groups[parentType[selectedIndex]];
		document.getElementById('hidACC_TYPE_ID').value = parentType[selectedIndex];
	
		DisplayBudgetCategory();
	}
	
	
}

//Display the budget category only if the sub type is income or expense.

function DisplayBudgetCategory()
{
   if(document.getElementById('cmbCATEGORY_TYPE').selectedIndex>-1)
	{
		var selVal1 = document.getElementById('cmbCATEGORY_TYPE').options[document.getElementById('cmbCATEGORY_TYPE').selectedIndex].value;
	//	alert(document.getElementById('cmbCATEGORY_TYPE').options[document.getElementById('cmbCATEGORY_TYPE').selectedIndex].value);
		if(selVal1=="1")//Account
		{
			var subType = document.getElementById('cmbGROUP_TYPE').options[document.getElementById('cmbGROUP_TYPE').selectedIndex].text;
			if(subType=="Income" || subType=="Expense")
			{
				document.getElementById('cellBudgetCategory1').style.display = "inline";
				document.getElementById('cellBudgetCategory2').style.display = "inline";
				document.getElementById('BudgetCategoryCell').style.display = "none";
				document.getElementById('trMgrDept').style.display = "inline";
			}
			else
			{			   
				document.getElementById('cellBudgetCategory1').style.display = "none";
				document.getElementById('cellBudgetCategory2').style.display = "none";
				document.getElementById('BudgetCategoryCell').style.display = "inline";
				document.getElementById('trMgrDept').style.display = "none";
			}
        }	
		else
		{		   
			if(document.getElementById('txtACC_TYPE_DESC').value =="Expense" || document.getElementById('txtACC_TYPE_DESC').value =="Income")
			{			  
				document.getElementById('cellBudgetCategory1').style.display = "inline";
				document.getElementById('cellBudgetCategory2').style.display = "inline";
				document.getElementById('BudgetCategoryCell').style.display = "none";
				document.getElementById('trMgrDept').style.display = "inline";
			}
			else
			{			  
				document.getElementById('cellBudgetCategory1').style.display = "none";
				document.getElementById('cellBudgetCategory2').style.display = "none";
				document.getElementById('BudgetCategoryCell').style.display = "inline";
				document.getElementById('trMgrDept').style.display = "none";
			}
		}
    }  
}


function ShowAccount()
{ 
    //set account type to blank when category is changed
	document.getElementById('txtACC_TYPE_DESC').value="";
	if(document.getElementById('cmbCATEGORY_TYPE').selectedIndex>-1)
	{
	var selVal1 = document.getElementById('cmbCATEGORY_TYPE').options[document.getElementById('cmbCATEGORY_TYPE').selectedIndex].value;
	if (selVal1 == "14455")//Account  //Changed by Aditya on 17-oct-2011 for TFS Bug # 1844
	{
		document.getElementById('cmbACC_PARENT_ID').disabled = true;
		document.getElementById('cmbACC_PARENT_ID').selectedIndex = -1;
		document.getElementById('spnGROUP_TYPE').style.display="inline";
		document.getElementById('cmbGROUP_TYPE').className="MandatoryControl";
		document.getElementById('cmbGROUP_TYPE').disabled = false;	
		document.getElementById('rfvGROUP_TYPE').setAttribute("enabled",true);	
		document.getElementById('rfvACC_PARENT_ID').setAttribute("enabled",false);	
		
		document.getElementById('txtACC_NUMBER').maxLength = 5;	
		
		document.getElementById('revACC_NUMBERAcNum').setAttribute("enabled",true);	
		document.getElementById('revACC_NUMBERSubAc').setAttribute("enabled",false);	
		lblSubAcNum.innerHTML = "";
		document.getElementById('spnParentAcMendatory').style.display="none";
		document.getElementById('cmbACC_PARENT_ID').className =""
		//DisplayRange(); Commented by Aditya on 17-oct-2011 for TFS Bug # 1844
		
	}
	else//Sub-Account
	{
		document.getElementById('spnParentAcMendatory').style.display="inline";
		document.getElementById('cmbGROUP_TYPE').disabled = true;
		document.getElementById('cmbACC_PARENT_ID').disabled = false;
		document.getElementById('cmbGROUP_TYPE').selectedIndex = -1;
		document.getElementById('lblRange').innerHTML = "";
		document.getElementById('rfvACC_PARENT_ID').setAttribute("enabled",true);	
		document.getElementById('rfvGROUP_TYPE').setAttribute("enabled",false);	
		document.getElementById('spnGROUP_TYPE').style.display="none";
		document.getElementById('cmbGROUP_TYPE').className="";
		document.getElementById('cmbACC_PARENT_ID').className ="MandatoryControl"
		
		document.getElementById('txtACC_NUMBER').maxLength = 2;
		
		document.getElementById('revACC_NUMBERAcNum').setAttribute("enabled",false);	
		document.getElementById('revACC_NUMBERSubAc').setAttribute("enabled",true);	
		ShowParentAcGroups();
		
		
	}	
	ChangeColor();
	
	
}
}
function ShowParentAcGroups()
{ 
	if(document.getElementById('cmbACC_PARENT_ID').selectedIndex>0)
	{
		var selVal1 = document.getElementById('cmbACC_PARENT_ID').options[document.getElementById('cmbACC_PARENT_ID').selectedIndex].value;
	    //alert("a: "+selVal1+"  "+parentAcGroupType[selVal1]	);
		document.getElementById('txtACC_TYPE_DESC').value = groups[parentAcGroupType[selVal1]];
		document.getElementById('hidACC_TYPE_ID').value = parentAcGroupType[selVal1]; 
		var acNum = document.getElementById('cmbACC_PARENT_ID').options[document.getElementById('cmbACC_PARENT_ID').selectedIndex].text;
	    acNum	  = acNum.substring(0,acNum.indexOf('.'));
		var acNum2 = document.getElementById('txtACC_NUMBER').value;
		lblSubAcNum.innerHTML = acNum;

	/*	acNum	  = acNum.substring(0,acNum.indexOf('.'));	
	    var acNum1=acNum.split(":");	
	    acNum1[1]=acNum1[1].substring(0,acNum1[1].indexOf('.')); 
		var acNum2 = document.getElementById('txtACC_NUMBER').value;		
		lblSubAcNum.innerHTML = acNum1[1];
		*/
		
	}
}
function CheckAcNoInRange()//on save 
{
	Page_ClientValidate();
	var acNo = parseFloat(document.getElementById('txtACC_NUMBER').value);
	if(!isNaN(acNo))
	{
		var selectedIndex = document.getElementById('cmbGROUP_TYPE').selectedIndex-1;
		if(selectedIndex>=0)
		{
			if(!(acNo>=fromRange[selectedIndex] && acNo <= toRange[selectedIndex]))
			{
				alert("Account no. must be between "+ fromRange[selectedIndex] + " and "+ toRange[selectedIndex]+"!");
				return false;
			}
		}
	}
	if(document.getElementById('cmbACC_LEVEL_TYPE').selectedIndex>1)
	{
	var levelType = document.getElementById('cmbACC_LEVEL_TYPE').options[document.getElementById('cmbACC_LEVEL_TYPE').selectedIndex].text
	if(levelType=="Total" && document.ACT_GL_ACCOUNTS.lstACC_TOTALS_LEVEL_Selected.options.length<=0)
	{		
		rfvACC_TOTALS_LEVEL_Selected.style.display="Inline";
		return false;
	}
	{
		var totals="";
		for(var i=0;i<document.ACT_GL_ACCOUNTS.lstACC_TOTALS_LEVEL_Selected.options.length;i++)
		{
			totals += document.ACT_GL_ACCOUNTS.lstACC_TOTALS_LEVEL_Selected.options[i].text;
			if(i<(document.ACT_GL_ACCOUNTS.lstACC_TOTALS_LEVEL_Selected.options.length-1))
				totals += ",";
		}
		document.getElementById('hidSelectedTotals').value = totals;
		rfvACC_TOTALS_LEVEL_Selected.style.display="none";
	}
	}
}
function SetPageDisplay()
{
	if(document.getElementById('cmbACC_LEVEL_TYPE').selectedIndex>-1)
	{
		var levelType = document.getElementById('cmbACC_LEVEL_TYPE').options[document.getElementById('cmbACC_LEVEL_TYPE').selectedIndex].text;
		if(levelType=="Heading" || levelType=="Total" || levelType=="Space")
		{
			BlankHiddenData(levelType);
			rowAcType.style.display = "None";
			document.getElementById('hidACC_JOURNAL_ENTRY').value='-1';
			
			document.getElementById('txtACC_TYPE_DESC').innerText = '';			
			rowJournal.style.display = "None";
			document.getElementById('chkACC_CASH_ACCOUNT').checked = false;
			cashRow.style.display = "None";
			
			document.getElementById('rfvACC_TOTALS_LEVEL').setAttribute("enabled",false);
			document.getElementById('rfvACC_JOURNAL_ENTRY').setAttribute("enabled",false);
			if(levelType=="Space")
			{
				document.getElementById('rfvACC_DESCRIPTION').setAttribute("enabled",false);
				cellDesc1.style.display = "None";
				cellDesc2.style.display = "None";
				cellDescBlank.style.display = "inline";
			}
			else
			{
				document.getElementById('rfvACC_DESCRIPTION').setAttribute("enabled",true);
				cellDesc1.style.display = "inline";
				cellDesc2.style.display = "inline";
				cellDescBlank.style.display = "none";
			}
			if(levelType=="Total")
			{
				rowMultiTotal.style.display = "inline";
			}
			else
			{
				rowMultiTotal.style.display = "none";
			}
	
		}
		else
		{
		   
			rowAcType.style.display = "inline";
			document.getElementById('hidACC_JOURNAL_ENTRY').value='0';
			//DisplayRange(); Commented by Aditya on 17-oct-2011 for TFS Bug # 1844
			rowJournal.style.display = "inline";
			cashRow.style.display = "inline";
			cashAccountCell1.style.display = "inline";
			cashAccountCell2.style.display = "inline";
			cashAccountBankCell.style.display="none";
			cellDesc1.style.display = "inline";
			cellDesc2.style.display = "inline";
			cellDescBlank.style.display = "none";
			document.getElementById('rfvACC_DESCRIPTION').setAttribute("enabled",true);
			document.getElementById('rfvACC_TOTALS_LEVEL').setAttribute("enabled",true);
			document.getElementById('rfvACC_JOURNAL_ENTRY').setAttribute("enabled",true);
			rowMultiTotal.style.display = "none";
		}
	
		ShowCash();
	}
	else
	{
		rowAcType.style.display = "inline";
		rowJournal.style.display = "inline";
		cashRow.style.display = "inline";
		cashAccountCell1.style.display = "none";
		cashAccountCell2.style.display = "none";
		cashAccountBankCell.style.display="inline";
		cellDesc1.style.display = "inline";
		cellDesc2.style.display = "inline";
		cellDescBlank.style.display = "none";
		document.getElementById('rfvACC_DESCRIPTION').setAttribute("enabled",true);
		document.getElementById('rfvACC_TOTALS_LEVEL').setAttribute("enabled",true);
		document.getElementById('rfvACC_JOURNAL_ENTRY').setAttribute("enabled",true);
		rowMultiTotal.style.display = "none";
	}
 
 
}
function BlankHiddenData(levelType)
{
	if(levelType=="Space")
		document.getElementById('txtACC_DESCRIPTION').value  = '';
}
function SelectTotalLevels()
{
	{//alert("fd");	//aa
			var coll = document.ACT_GL_ACCOUNTS.lstACC_TOTALS_LEVEL;
			var selIndex = coll.options.selectedIndex;
				var len = coll.options.length;
				for (i = len- 1; i > -1 ; i--)
				{
				if((coll.options(i).selected == true) && (coll.options(i).value != ""))
					{
						var szSelectedEntity = coll.options(i).value;
						var innerText = coll.options(i).text;
						document.ACT_GL_ACCOUNTS.lstACC_TOTALS_LEVEL_Selected.options[document.ACT_GL_ACCOUNTS.lstACC_TOTALS_LEVEL_Selected.length] = new Option(innerText,szSelectedEntity)
						coll.remove(i);
					}
				}
		}
	return false;
}
function SelectAllSectedTotalsOptions()
{
	var coll = document.ACT_GL_ACCOUNTS.lstACC_TOTALS_LEVEL_Selected ;
	for(var i=0;i<coll.options.length;i++)
	{
		coll.options[i].selected = true;
	}
}
function DeselectTotalLevels()
{
	var UnassignableString = ""
			var Unassignable = UnassignableString.split(",")
			var gszAssignedString = ""
			var Assigned = gszAssignedString.split(",")
			var coll = document.ACT_GL_ACCOUNTS.lstACC_TOTALS_LEVEL_Selected ;
			var selIndex = coll.options.selectedIndex;
				var len = coll.options.length;
				for (i = len- 1; i > -1 ; i--)
				{
					if((coll.options(i).selected == true) && (coll.options(i).value != ""))
					{	
							var flag = true;
							var szSelectedEntity = coll.options(i).value;
							var innerText = coll.options(i).text;
							for(j = 0; j < Unassignable.length ;j++)
							{
								for(k = 0; k < Assigned.length ;k++)
								{							
										if((szSelectedEntity == Unassignable[j]) && (szSelectedEntity == Assigned[k])) 
										{
											flag = false;
										}
								}
							}
							if(flag == true)
							{
//								document.ACT_GL_ACCOUNTS.lstACC_TOTALS_LEVEL.options[document.ACT_GL_ACCOUNTS.lstACC_TOTALS_LEVEL.length] = new Option(innerText,szSelectedEntity)
								var items = document.ACT_GL_ACCOUNTS.lstACC_TOTALS_LEVEL.length;
								var x;
								for(x=1;x<items;x++)
								{
									if(parseInt(document.ACT_GL_ACCOUNTS.lstACC_TOTALS_LEVEL.options[x].text)>parseInt(innerText))
									{
										document.ACT_GL_ACCOUNTS.lstACC_TOTALS_LEVEL.add(new Option(innerText,szSelectedEntity),x);
										break;
									}
								}
								if(items==x)
									document.ACT_GL_ACCOUNTS.lstACC_TOTALS_LEVEL.add(new Option(innerText,szSelectedEntity));
								coll.remove(i);
							}
							else
							{
								alert("Cannot unassign level "+innerText+ "");
							}
					}
				}
	return false;
}
function showPageLookupLayer(controlId)
			{
				var lookupMessage;						
				switch(controlId)
				{
					case "cmbACC_RELATES_TO_TYPE":
						lookupMessage	=	"ACPOST.";
						break;
					default:
						lookupMessage	=	"Look up code not found";
						break;
						
				}
				showLookupLayer(controlId,lookupMessage);							
			}
		</script>
</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();ShowCashAccount();ShowParentAcGroups();SetPageDisplay();populateXML();DisplayBudgetCategory();">
		<FORM id="ACT_GL_ACCOUNTS" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr id="trError" runat="server">
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblError" runat="server" CssClass="errmsg"></asp:label></td>
				</tr>
				<TR id="trbody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="pageHeader" colSpan="4"><asp:Label ID="capMANDATORY" runat="server"></asp:Label></TD><%--Please note that all fields marked with * are 
									mandatory--%>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCATEGORY_TYPE" runat="server">Category</asp:label></TD>
								<TD class="midcolora" colSpan="3">
								<asp:dropdownlist id="cmbCATEGORY_TYPE" onfocus="SelectComboIndex('cmbCATEGORY_TYPE')" runat="server"
										onchange="ShowAccount();">
										<%--<asp:ListItem Value="1">Account</asp:ListItem>
										<asp:ListItem Value="2">Sub-Account</asp:ListItem>--%>
									</asp:dropdownlist></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capGROUP_TYPE" runat="server">Sub Type</asp:label><span class="mandatory" id="spnGROUP_TYPE">*</span></TD>
								<TD class="midcolora" colSpan="3"><asp:dropdownlist id="cmbGROUP_TYPE" onfocus="SelectComboIndex('cmbGROUP_TYPE')" runat="server" onchange="DisplayRange();ShowCashAccount(this.selectedIndex);">
										<ASP:LISTITEM Value="0"></ASP:LISTITEM>
									</asp:dropdownlist><span class="mandatory">
										<asp:label id="lblRange" runat="server"></asp:label></span><BR>
									<asp:requiredfieldvalidator id="rfvGROUP_TYPE" runat="server" Display="Dynamic" ErrorMessage=""
										ControlToValidate="cmbGROUP_TYPE"></asp:requiredfieldvalidator></TD><%--ACC_LEVEL_TYPE can't be blank.--%>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capACC_PARENT_ID" runat="server">Parent Account</asp:label><span id="spnParentAcMendatory" class="mandatory">*</span></TD>
								<TD class="midcolora" id="tdParentAccount" width="32%"><asp:dropdownlist id="cmbACC_PARENT_ID" onfocus="SelectComboIndex('cmbACC_PARENT_ID')" runat="server"
										onchange="ShowParentAcGroups();ShowCashAccountForSubAccounts();DisplayBudgetCategory();">
										<ASP:LISTITEM Value="0"></ASP:LISTITEM>
									</asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvACC_PARENT_ID" runat="server" Display="Dynamic" ErrorMessage=""
										ControlToValidate="cmbACC_PARENT_ID" InitialValue="0"></asp:requiredfieldvalidator></TD><%--ACC_LEVEL_TYPE can't be blank.--%>
								<TD class="midcolora" width="18%">
									<table width="100%" border="0" cellpadding="0" cellspacing="0">
										<tr>
											<TD class="midcolora"><asp:label id="capACC_NUMBER" runat="server">Account Number</asp:label><span class="mandatory">*</span>
											</TD>
											<td class="midcolorr"><span id="lblSubAcNum"></span></td>
										</tr>
									</table>
								</TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtACC_NUMBER" runat="server" maxlength="5" size="10" ondblur="CheckAcNoInRange();"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvACC_NUMBER" runat="server" Display="Dynamic" ErrorMessage=""
										ControlToValidate="txtACC_NUMBER"></asp:requiredfieldvalidator><%--ACC_NUMBER can't be blank.--%><asp:regularexpressionvalidator id="revACC_NUMBERAcNum" runat="server" Display="Dynamic" ErrorMessage=""
										ControlToValidate="txtACC_NUMBER"></asp:regularexpressionvalidator><%--RegularExpressionValidator--%><asp:regularexpressionvalidator id="revACC_NUMBERSubAc" runat="server" Display="Dynamic" ErrorMessage=""
										ControlToValidate="txtACC_NUMBER"></asp:regularexpressionvalidator></TD><%--RegularExpressionValidator--%>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capACC_LEVEL_TYPE" runat="server">Level Type</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbACC_LEVEL_TYPE" onfocus="SelectComboIndex('cmbACC_LEVEL_TYPE')" runat="server"
										onchange="SetPageDisplay();">
										<%--<asp:ListItem Value=""></asp:ListItem>
										<asp:ListItem Value="AS">Account/Sub-Account</asp:ListItem>
										<asp:ListItem Value="Head">Heading</asp:ListItem>
										<asp:ListItem Value="Space">Space</asp:ListItem>
										<asp:ListItem Value="Total">Total</asp:ListItem>--%>
									</asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvACC_LEVEL_TYPE" runat="server" Display="Dynamic" ErrorMessage=""
										ControlToValidate="cmbACC_LEVEL_TYPE"></asp:requiredfieldvalidator></TD><%--ACC_LEVEL_TYPE can't be blank.--%>
								<TD class="midcolora" id="cellDescBlank" style="DISPLAY: none" colSpan="2"></TD>
								<TD class="midcolora" id="cellDesc1" width="18%"><asp:label id="capACC_DESCRIPTION" runat="server">Description</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" id="cellDesc2" width="32%"><asp:textbox id="txtACC_DESCRIPTION" runat="server" maxlength="50" size="40"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvACC_DESCRIPTION" runat="server" Display="Dynamic" ErrorMessage=""
										ControlToValidate="txtACC_DESCRIPTION"></asp:requiredfieldvalidator></TD><%--ACC_DESCRIPTION can't be blank.--%>
							</tr>
							<tr id="rowAcType">
								<TD class="midcolora" width="18%"><asp:label id="capACC_TYPE_ID" runat="server">Account Type </asp:label><%--<span class="mandatory">*</span>--%></TD>
								<TD class="midcolora" width="32%"><asp:textbox class="midcolora" id="txtACC_TYPE_DESC" style="BORDER-RIGHT: medium none; BORDER-TOP: medium none; BORDER-LEFT: medium none; BORDER-BOTTOM: medium none"
										runat="server" maxlength="4" size="30" ReadOnly="True"></asp:textbox><BR>
								</TD>
								<TD class="midcolora" width="18%"><asp:label id="Label1" runat="server">Totals Level</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbACC_TOTALS_LEVEL" onfocus="SelectComboIndex('cmbACC_TOTALS_LEVEL')" runat="server"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvACC_TOTALS_LEVEL" runat="server" Display="Dynamic" ControlToValidate="cmbACC_TOTALS_LEVEL"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr id="rowJournal">
								<TD class="midcolora" width="18%"><asp:label id="capACC_JOURNAL_ENTRY" runat="server">Journal Entry </asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbACC_JOURNAL_ENTRY" onfocus="SelectComboIndex('cmbACC_JOURNAL_ENTRY')" runat="server"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvACC_JOURNAL_ENTRY" runat="server" Display="Dynamic" ErrorMessage=""
										ControlToValidate="cmbACC_JOURNAL_ENTRY"></asp:requiredfieldvalidator></TD><%--ACC_JOURNAL_ENTRY can't be blank.--%>
								<TD class="midcolora" id="cashAccountBankCell" width="18%" colSpan="2"></TD>
								<TD class="midcolora" id="cashAccountCell1" width="18%"><asp:label id="capACC_CASH_ACCOUNT" runat="server">Cash Account </asp:label></TD>
								<TD class="midcolora" id="cashAccountCell2" width="18%" colSpan="1"><asp:checkbox id="chkACC_CASH_ACCOUNT" onclick="ShowCash();" runat="server"></asp:checkbox></TD>
							</tr>
							<tr id="cashRow">
								<TD class="midcolora" width="18%"><asp:label id="capACC_CASH_ACC_TYPE" runat="server">Cash Type </asp:label></TD>
								<TD class="midcolora" width="32%" colspan="3"><asp:radiobutton id="rdbACC_CASH_ACC_TYPEO" runat="server" Text="Checking" GroupName="ACC_CASH_ACC_TYPE"
										Checked="True"></asp:radiobutton><asp:radiobutton id="rdbACC_CASH_ACC_TYPET" runat="server" Text="Saving" GroupName="ACC_CASH_ACC_TYPE"></asp:radiobutton></TD>
							</tr>
							<tr id="rowMultiTotal" style="DISPLAY: none">
								<TD class="midcolora"><asp:label id="capACC_TOTALS_LEVEL" runat="server">Totals Level</asp:label><SPAN class="mandatory"><SPAN class="mandatory">*</SPAN></SPAN></TD>
								<TD class="midcolora" colSpan="1">
									<table cellSpacing="1" cellPadding="0" width="100%" border="0">
										<tr>
											<TD class="midcolora"><asp:listbox id="lstACC_TOTALS_LEVEL" runat="server" width="100px" SelectionMode="Multiple"></asp:listbox></TD>
											<TD vAlign="middle" align="center"><cmsb:cmsbutton class="clsButton" id="btnSelect" runat="server" Text=" >> " Font-Names="arial"></cmsb:cmsbutton><BR>
												<cmsb:cmsbutton class="clsButton" id="btnDeselect" runat="server" Text=" << " Font-Names="Arial"></cmsb:cmsbutton></TD>
											<TD class="midcolorr"><asp:listbox id="lstACC_TOTALS_LEVEL_Selected" runat="server" width="100px" SelectionMode="Multiple"></asp:listbox></TD>
										</tr>
									</table>
									<span id="rfvACC_TOTALS_LEVEL_Selected" style="DISPLAY: none; COLOR: red"><asp:Label ID="capLabl" runat="server"> Please 
										select Totals Level.</asp:Label></span>
								</TD>
								<TD class="midcolora" colSpan="2"></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capRELATES_TO_TYPE" runat="server">Relates To</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbACC_RELATES_TO_TYPE" onfocus="SelectComboIndex('cmbACC_JOURNAL_ENTRY')" runat="server"></asp:dropdownlist><a class="calcolora" href="javascript:showPageLookupLayer('cmbACC_RELATES_TO_TYPE')"><img src="/cms/cmsweb/images/info.gif" width="17" height="16" border="0"></a>
									<br>
									<asp:requiredfieldvalidator id="rfvRELATES_TO_TYPE" runat="server" Display="Dynamic" ErrorMessage=""
										ControlToValidate="cmbACC_RELATES_TO_TYPE"></asp:requiredfieldvalidator><%--ACC_NUMBER can't be blank.--%>
								</TD>
								<!-- start vj -->
								<TD class="midcolora" id="BudgetCategoryCell" width="18%" colSpan="2"></TD>
								<TD class="midcolora" width="18%" id="cellBudgetCategory1" style="DISPLAY:none"><asp:label id="capBUDGET_CATEGORY_ID" runat="server">Budget Category</asp:label></TD>
								<TD class="midcolora" width="32%" id="cellBudgetCategory2" style="DISPLAY:none"><asp:dropdownlist id="cmbBUDGET_CATEGORY_ID" onfocus="SelectComboIndex('cmbBUDGET_CATEGORY_ID')" runat="server"></asp:dropdownlist></TD>
								<!-- end vj -->
							</tr>
							<tr id = "trMgrDept">
								<td class ="midcolora" colspan="2"></td>
								<TD class="midcolora" width="18%"><asp:label id="capWOLVERINE_USER_ID" runat="server">Manager/Department</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbWOLVERINE_USER_ID" onfocus="SelectComboIndex('cmbWOLVERINE_USER_ID')" runat="server"></asp:dropdownlist></TD>
							</tr>
							<tr>
								<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text=""></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete"></cmsb:cmsbutton></td>
								<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
							<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
							<INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
							<INPUT id="hidACCOUNT_ID" type="hidden" value="New" name="hidACCOUNT_ID" runat="server">
							<input type="hidden" id="hidDelete" runat="server" name="hidDelete" value="0"> <INPUT id="hidACC_TYPE_ID" type="hidden" name="hidACC_TYPE_ID" runat="server">
							<INPUT id="hidSelectedTotals" type="hidden" name="hidSelectedTotals" runat="server">
							<INPUT id="hidACC_JOURNAL_ENTRY" type="hidden" name="hidACC_JOURNAL_ENTRY" value="0" runat="server">
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none;Z-INDEX: 101;POSITION: absolute">
			<iframe id="lookupLayerIFrame" style="DISPLAY: none;Z-INDEX: 1001;FILTER: alpha(opacity=0);BACKGROUND-COLOR: #000000"
				width="0" height="0" top="0px;" left="0px"></iframe>
		</div>
		<DIV id="lookupLayer" style="Z-INDEX: 101;VISIBILITY: hidden;POSITION: absolute" onmouseover="javascript:refreshLookupLayer();">
			<table class="TabRow" cellspacing="0" cellpadding="2" width="100%">
				<tr class="SubTabRow">
					<td><b>Add LookUp</b></td>
					<td><p align="right"><a href="javascript:void(0)" onclick="javascript:hideLookupLayer();"><img src="/cms/cmsweb/images/cross.gif" border="0" alt="Close" WIDTH="16" HEIGHT="14"></a></p>
					</td>
				</tr>
				<tr class="SubTabRow">
					<td colspan="2">
						<span id="LookUpMsg"></span>
					</td>
				</tr>
			</table>
		</DIV>
		<!-- For lookup layer ends here-->
		<script>
				RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidACCOUNT_ID').value);
		</script>
	</BODY>
</HTML>
