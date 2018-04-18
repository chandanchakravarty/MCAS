<%@ Page language="c#" Codebehind="RenewalProcess.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Processes.RenewalProcess" ValidateRequest="false"%>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="PolicyTop" Src="/cms/cmsweb/webcontrols/PolicyTop.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RenewalProcess</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script language="javascript">
		
		//Setting the menu
		//This function will be called after starting the endorsement process
		//using RegisterStartupScript method
		function setMenu()
		{
			
			//IF menu on top frame is not ready then
			//menuXmlReady will false
			//If menu is not ready, we will again call setmenu again after 1 sec
			if (top.topframe.main1.menuXmlReady == false)
				setTimeout('setMenu();',1000);
			
			
			//Enabling or disabling menus
			top.topframe.main1.activeMenuBar = '1';
			top.topframe.createActiveMenu();
			top.topframe.enableMenus('1','ALL');
			top.topframe.enableMenu('1,1,1');

			/*
			if Policy under renewal in progress ,it should be treated as apllication,
			should not redirect on Policy information page,should redirect on application information page
			refer itrack # 947
			*/
			if ('<%=GetPolicyStatus().Trim().ToUpper() %>' != '<%=  Cms.BusinessLayer.BlCommon.ClsCommon.POLICY_STATUS_UNDER_RENEW.ToString()%>')
			{
			top.topframe.enableMenu("1,2,3");
			}			
			//top.topframe.enableMenu('1,2,3');
			//top.topframe.disableMenus("1,3","ALL");
			//selectedLOB = lobMenuArr[document.getElementById('hidLOBID').value];
			//top.topframe.enableMenu("1,3," + selectedLOB);			
			
			}
		
		
			function AddData()
			{
			document.getElementById('hidROW_ID').value	=	'New';
			document.getElementById('txtCOMMENTS').focus();
			document.getElementById('txtCOMMENTS').value  = '';
			DisableValidators();
			ChangeColor();
			}
			function populateXML()
			{
			if(document.getElementById('hidFormSaved').value == '0')
			{
			var tempXML;
			if(document.getElementById("hidOldData").value != "")
			{
			//populateFormData(document.getElementById("hidOldData").value, NewBusinessProcess);
			populateFormData(document.getElementById("hidOldData").value, RenewalProcess);
			}
			else
			{
			AddData();
			}
			}
			return false;
			}
			function Init()
			{
			showScroll();			
			top.topframe.main1.mousein = false;
			findMouseIn();
			populateXML();
			AutoIdCardDisplay();
			DisplayBody();
			cmbADD_INT_Change();
			SetAssignAddInt();
			ApplyColor();
			ChangeColor();	
			document.getElementById('btnCommitInProgress').style.display="none";
			document.getElementById('btnCommitAnywayInProgress').style.display="none";
			//chkSEND_ALL_Change();		
			//Added for Itrack Issue 6203 on 31 July 2009
			if (top.topframe.main1.menuXmlReady == false)
			setTimeout("top.topframe.enableMenus('1,7','ALL');",1000);
			else	
			top.topframe.enableMenus('1,7','ALL');
			}
			function cmbADD_INT_Change()
			{
			combo = document.getElementById('cmbADD_INT');
			if(combo==null || combo.selectedIndex==-1)
			return false;
			//MICHIGAN_MAILERS #Itrack 4068
			if(combo.options[combo.selectedIndex].value=="<%=((int)Cms.BusinessLayer.BlProcess.clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString()%>"
			|| combo.options[combo.selectedIndex].value=="<%=((int)Cms.BusinessLayer.BlProcess.clsprocess.enumPROC_PRINT_OPTIONS.MICHIGAN_MAILERS).ToString()%>")
			{
			document.getElementById('chkSEND_ALL').style.display="inline";
			document.getElementById('capSEND_ALL').style.display="inline";
			//document.getElementById('trAddIntList').style.display="inline";				
			chkSEND_ALL_Change();
			}
			else
			{
			document.getElementById('chkSEND_ALL').style.display="none";
			document.getElementById('capSEND_ALL').style.display="none";
			document.getElementById('trAddIntList').style.display="none";
			//document.getElementById('hidADD_INT_ID').value = '';
			}
			return false;
			}
			function chkSEND_ALL_Change()			
			{
			var chk = document.getElementById('chkSEND_ALL');
			if(chk==null)
			return false;								
				
			//document.getElementById("hidADD_INT_ID").value='';
			if(chk.checked==true)
			{
			ShowHideAddIntCombos(true);
			AssignAddInt(true);
			}
			else
			{
			ShowHideAddIntCombos(false);
			UnAssignAddInt(true);
			}
				
			return false;
			/*if(chk.checked==true)
			document.getElementById("trAddIntList").style.display ="none";
			else
			document.getElementById("trAddIntList").style.display ="inline";
			*/
				
			}
			function ShowHideAddIntCombos(flag)
			{
				var chk = document.getElementById('chkSEND_ALL');
				if(chk==null)
					return false;	
					
				if(flag && chk.checked==true)
					document.getElementById('trAddIntList').style.display="none";
				else
					document.getElementById('trAddIntList').style.display="inline";
			}
			function GetAssignAddInt()
			{
				document.getElementById("hidADD_INT_ID").value = "";
				var coll = document.getElementById('cmbAssignAddInt');
				var len = coll.options.length;
				for(var k = 0;k < len ; k++)
				{
					var szSelectedDept = coll.options(k).value;
					if (document.getElementById("hidADD_INT_ID").value == "")
					{
						document.getElementById("hidADD_INT_ID").value=  szSelectedDept;
					}
					else
					{
						document.getElementById("hidADD_INT_ID").value = document.getElementById("hidADD_INT_ID").value + "~" + szSelectedDept;
					}
				}
					
			}

			// Added by Shikha 
			function Installment_Renewal_Result() {
			 
			    var strCOUNT = document.getElementById('hidCOUNT').value;
			    if (strCOUNT == "1") {
			        return confirm(document.getElementById('hidpopup').value);
			    }
			}


			function HideShowCommitInProgress()
			{
				GetAssignAddInt();
				Page_ClientValidate();	
				if(!Page_IsValid)
				{
				return false;
				}
				else
				{
				document.getElementById('btnCommitInProgress').style.display="inline";
				document.getElementById('btnCommitInProgress').disabled = true;
				document.getElementById('btnCommit').style.display="none";
				if(document.getElementById('btnComitAynway'))
					document.getElementById('btnComitAynway').disabled = true;
				document.getElementById('btnSave').disabled = true;
				if(document.getElementById('btnRollback'))
					document.getElementById('btnRollback').disabled = true;	
     			 DisableButton(document.getElementById('btnCommit'));
     			 top.topframe.disableMenus("1,7","ALL");//Added for Itrack Issue 6203 on 31 July 2009
				return true;
				}	
			}
			function HideShowCommitAnywayInProgress()
			{
				GetAssignAddInt();
				Page_ClientValidate();
				if(!Page_IsValid)
				{
				return false;
				}
				else
				{
				document.getElementById('btnCommitAnywayInProgress').style.display="inline";
				document.getElementById('btnCommitAnywayInProgress').disabled = true;
				document.getElementById('btnComitAynway').style.display="none";
				document.getElementById('btnCommit').disabled = true;
				document.getElementById('btnSave').disabled = true;
				if(document.getElementById('btnRollback'))
					document.getElementById('btnRollback').disabled = true;	
     			 DisableButton(document.getElementById('btnComitAynway'));
     			 top.topframe.disableMenus("1,7","ALL");//Added for Itrack Issue 6203 on 31 July 2009
				return true;
				}	
			}
			function HideShowCommit()
			{
				
				if(document.getElementById('btnComitAynway'))
					document.getElementById('btnComitAynway').disabled = true;
				document.getElementById('btnCommit').disabled = true;
				document.getElementById('btnSave').disabled = true;
     			DisableButton(document.getElementById('btnRollback'));
     			top.topframe.disableMenus('1,7','ALL');//Added for Itrack Issue 6203 on 31 July 2009
			}
			function SetAssignAddInt()
			{
				if(document.getElementById("hidADD_INT_ID").value=='' || document.getElementById("hidADD_INT_ID").value=='0')
					return;
				var selectedAddInt = new String(document.getElementById("hidADD_INT_ID").value);
				var selectedAddIntArr = selectedAddInt.split('~');
				if(selectedAddIntArr==null || selectedAddIntArr.length<1)
					return;				
				
				var coll = document.getElementById('cmbUnAssignAddInt');
				var selIndex = coll.options.selectedIndex;
				var len = coll.options.length;
				var arrLen = selectedAddIntArr.length;
				if(len<1) return;				
				var num=0;				
				for(var j=0;j<arrLen;j++)
				{
					for (var i = len- 1; i > -1 ; i--)
					{
						if(coll.options(i).value == selectedAddIntArr[j])
						{
							num = i;
							var szSelectedDept = coll.options(i).value;
							var innerText = coll.options(i).text;
							document.getElementById('cmbAssignAddInt').options[document.getElementById('cmbAssignAddInt').length] = new Option(innerText,szSelectedDept)
							coll.remove(i);																	
							len = coll.options.length;
						}										
					}
				}/*	
				len = coll.options.length;	
				if(	num < len )
				{
					document.getElementById('cmbUnAssignAddInt').options(num).selected = true;
				}	
				else
				{
					if(num>0)
						document.getElementById('cmbUnAssignAddInt').options(num - 1).selected = true;
				}	*/			
			}	
			function AssignAddInt(flag)
			{					
			var coll = document.getElementById('cmbUnAssignAddInt');
			var selIndex = coll.options.selectedIndex;				
			var len = coll.options.length;			
			if(len<1) return;				
			var num=0;				
			for (var i = len- 1; i > -1 ; i--)
			{
				if(coll.options(i).selected == true || flag)
				{
					num = i;
					var szSelectedDept = coll.options(i).value;
					var innerText = coll.options(i).text;
					document.getElementById('cmbAssignAddInt').options[document.getElementById('cmbAssignAddInt').length] = new Option(innerText,szSelectedDept)
					coll.remove(i);																	
				}										
			}	
			len = coll.options.length;	
			if(	num < len )
			{
				document.getElementById('cmbUnAssignAddInt').options(num).selected = true;
			}	
			else
			{
				if(num>0)
					document.getElementById('cmbUnAssignAddInt').options(num - 1).selected = true;
			}			
			
		}
			function UnAssignAddInt(flag)
			{					
				var UnassignableString = "";								
				var Unassignable = UnassignableString.split(",");
				var gszAssignedString = "";
				var Assigned = gszAssignedString.split(",");				
				var coll = document.getElementById('cmbAssignAddInt');
				var selIndex = coll.options.selectedIndex;
				var len = coll.options.length;		
				var num=0;						
				if(len==0) return;
				
				for (var i = len-1; i > -1 ; i--)
				{
					if(coll.options(i).selected == true || flag)
					{
							num = i;						
							var flag = true;
							var szSelectedDept = coll.options(i).value;
							var innerText = coll.options(i).text;
							for(j = 0; j < Unassignable.length ;j++)
							{
								for(k = 0; k < Assigned.length ;k++)
								{							
										if((szSelectedDept == Unassignable[j]) && (szSelectedDept == Assigned[k])) 
										{
											flag = false;
										}
								}
							}
							
							if(flag == true)
							{
								document.getElementById('cmbUnAssignAddInt').options[document.getElementById('cmbUnAssignAddInt').length] = new Option(innerText,szSelectedDept)					
								coll.remove(i);
															
							}
							/*else
							{
								alert("Cannot unassign Department "+innerText+" because some divisions are associasted with it");
							}*/
					}			
				}
				var len = coll.options.length;
				if(len<1) return;				
				if(	num < len )
				{
					document.getElementById('cmbAssignAddInt').options(num).selected = true;
				}	
				else
				{
					document.getElementById('cmbAssignAddInt').options(num - 1).selected = true;
				}
				
			}			
		//Validates the maximum length for comments
		function txtCOMMENTS_VALIDATE(source, arguments)
		{
				var txtArea = arguments.Value;
				if(txtArea.length > 500 ) 
				{
					arguments.IsValid = false;
					return false;   // invalid userName
				}
		}
		function AutoIdCardDisplay()
		{
			if(document.getElementById('hidLOB_ID').value=='<%=((int)Cms.CmsWeb.cmsbase.enumLOB.AUTOP).ToString()%>' || document.getElementById('hidLOB_ID').value=='<%=((int)Cms.CmsWeb.cmsbase.enumLOB.CYCL).ToString()%>')
			{
				document.getElementById('trAutoIdHeader').style.display="inline";
				document.getElementById('trAutoIdControls').style.display="inline";
			}
			else
			{
				document.getElementById('trAutoIdHeader').style.display="none";
				document.getElementById('trAutoIdControls').style.display="none";
			}
				
		}		
		
		function DisplayBody()
		{
			if (document.getElementById('hidDisplayBody').value == "True")
			{
				document.getElementById('trBody').style.display='inline';		
			}
			else
			{
				document.getElementById('trBody').style.display='none';
			}
			
		}
		
		function ShowDetailsPolicy()
		{
			/*if (document.getElementById('hidNEW_POLICY_VERSION_ID').value !="" &&  document.getElementById('hidNEW_POLICY_VERSION_ID').value !="0")
			top.botframe.callItemClicked('1,2,3','/cms/policies/aspx/policytab.aspx?CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPOLICY_ID').value + '&POLICY_VERSION_ID=' + document.getElementById('hidNEW_POLICY_VERSION_ID').value + '&');
			else
			top.botframe.callItemClicked('1,2,3','/cms/policies/aspx/policytab.aspx?CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPOLICY_ID').value + '&POLICY_VERSION_ID=' + document.getElementById('hidPOLICY_VERSION_ID').value + '&');
			*/
			if (document.getElementById('hidNEW_POLICY_VERSION_ID').value!="" && document.getElementById('hidNEW_POLICY_VERSION_ID').value!="0")
				window.open('/Cms/Application/Aspx/DecPage.aspx?CALLEDFOR=DECPAGE&CALLEDFROM=POLICY&CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPOLICY_ID').value + '&POLICY_VER_ID=' + document.getElementById('hidNEW_POLICY_VERSION_ID').value + '&');
			else
				window.open('/Cms/Application/Aspx/DecPage.aspx?CALLEDFOR=DECPAGE&CALLEDFROM=POLICY&CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPOLICY_ID').value + '&POLICY_VER_ID=' + document.getElementById('hidPOLICY_VERSION_ID').value + '&');
			return false;
		}
		
		function formReset()
		{
			document.RenewalProcess.reset();
			return false;
		}

		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" scroll="yes" onload="Init();">
			
		<form id="RenewalProcess" method="post" runat="server">
			<DIV><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu></DIV>
			<TABLE cellSpacing="0" cellPadding="0" width="90%" align="center" border="0">
				<tr>
					<td><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer></td>
				</tr>
				<tr>
					<TD><webcontrol:policytop id="cltPolicyTop" runat="server"></webcontrol:policytop></TD>
				</tr>
				<tr>
					<td><webcontrol:gridspacer id="Gridspacer1" runat="server"></webcontrol:gridspacer></td>
				</tr>
				<tr>
					<td class="headereffectcenter"><asp:Label ID="capHeader" runat="server"></asp:Label></td><%--Renewal Process--%>
				</tr>
				<tr>
					<td id="tdGridHolder"><webcontrol:gridspacer id="Gridspacer2" runat="server"></webcontrol:gridspacer><asp:placeholder id="GridHolder" runat="server"></asp:placeholder></td>
				</tr>
				<tr>
					<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessage" runat="server"></asp:Label></TD><%--Please note that all fields marked with * are mandatory--%>
				</tr>
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				</tr>
				<tr id="trBody">
					<td>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCOMMENTS" runat="server">Comments</asp:label></TD>
								<TD class="midcolora" colSpan="3"><asp:textbox id="txtCOMMENTS" runat="server" Rows="5" Columns="50" TextMode="MultiLine" MaxLength="500"></asp:textbox><br>
									<asp:customvalidator id="csvCOMMENTS" ControlToValidate="txtCOMMENTS" Display="Dynamic" ClientValidationFunction="txtCOMMENTS_VALIDATE"
										Runat="server"></asp:customvalidator></TD>
							</tr>
							<tr>
								<TD class="headerEffectSystemParams" colSpan="4"><asp:label ID=lblPrinting runat="server">Printing Options Details</asp:label></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capPRINTING_OPTIONS" runat="server">Printing Options</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:checkbox id="chkPRINTING_OPTIONS" Runat="server"></asp:checkbox></TD>
								<TD class="midcolora" width="18%"><asp:label id="capINSURED" runat="server">Client</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbINSURED" Runat="server"></asp:dropdownlist></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capAGENCY_PRINT" runat="server">Agency</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbAGENCY_PRINT" Runat="server"></asp:dropdownlist></TD>
								<TD class="midcolora" colSpan="2"></TD>
							</tr>
							<tr>
								<TD class="headerEffectSystemParams" colSpan="4"><asp:label ID=Label1 runat="server">Additional Interest Details</asp:label></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capADD_INT" runat="server">Additional Interest</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbADD_INT" Runat="server"></asp:dropdownlist></TD>
								<TD class="midcolora" width="18%"><asp:label id="capSEND_ALL" runat="server">Send to All</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:checkbox id="chkSEND_ALL" Runat="server"></asp:checkbox></TD>
							</tr>
							<tr id="trAddIntList">
								<td class="midcolora" colSpan="4">
									<table>
										<tr>
											<td class="midcolorc" align="center" width="37%"><asp:label id="capUnassignLossCodes" runat="server">All Additional Interest</asp:label></td>
											<td class="midcolorc" vAlign="middle" align="center" width="33%" rowSpan="7"><br>
												<br>
												<input class="clsButton" id="btnAssignLossCodes" onclick="javascript:AssignAddInt(false);"
													type="button" value=">>" name="btnAssignLossCodes" runat="server"><br>
												<br>
												<input class="clsButton" id="btnUnAssignLossCodes" onclick="javascript:UnAssignAddInt(false);"
													type="button" value="<<" name="btnUnAssignLossCodes" runat="server">
											</td>
											<td class="midcolorc" align="center" width="33%"><asp:label id="capAssignedLossCodes" runat="server">Selected Additional Interest</asp:label></td>
										</tr>
										<tr>
											<td class="midcolorc" align="center" width="33%" rowSpan="6"><asp:listbox id="cmbUnAssignAddInt" Runat="server" SelectionMode="Multiple" Height="150px" Width="300px"></asp:listbox></td>
											<td class="midcolorc" align="center" width="33%" rowSpan="6"><asp:listbox id="cmbAssignAddInt" Runat="server" SelectionMode="Multiple" Height="150px" Width="300px"></asp:listbox></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr id="trAutoIdHeader">
								<TD class="headerEffectSystemParams" colSpan="4">Auto ID Card Details</TD>
							</tr>
							<tr id="trAutoIdControls">
								<TD class="midcolora" width="18%"><asp:label id="capAUTO_ID_CARD" runat="server">Auto ID Card</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:DropDownList ID="cmbAUTO_ID_CARD" Runat="server"></asp:DropDownList></TD>
								<TD class="midcolora" width="18%"><asp:label id="capNO_COPIES" runat="server">No. of Copies</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:TextBox ID="txtNO_COPIES" Runat="server" MaxLength="3" size="6"></asp:TextBox><br>
									<asp:rangevalidator id="rngNO_COPIES" Display="Dynamic" ControlToValidate="txtNO_COPIES" Runat="server"
										Type="Integer" MinimumValue="0" MaximumValue="999"></asp:rangevalidator>
								</TD>
							</tr>
							<!-- cms buttons  -->
							<tr>
								<td class="midcolora" align="right" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnRollback" runat="server" Text="Rollback" ></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnPolicyDetails" runat="server" Text="View Dec Page" Visible="false"></cmsb:cmsbutton><BR>
									<cmsb:cmsbutton class="clsButton" id="btnBack_To_Search" runat="server" Text="Back To Search"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnBack_To_Customer_Assistant" runat="server" Text="Back To Customer Assistant"></cmsb:cmsbutton></td>
								<td class="midcolorr" align="left" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnPrint_Preview" runat="server" Text="Print Preview" style="DISPLAY:none"></cmsb:cmsbutton>
								<cmsb:cmsbutton class="clsButton" id="btnCommit" runat="server" Text="Commit"></cmsb:cmsbutton>
								<cmsb:cmsbutton class="clsButton" id="btnCommitInProgress" runat="server" Text="Commit in Progress"></cmsb:cmsbutton>
								<cmsb:cmsbutton class="clsButton" id="btnGet_Premium" style="DISPLAY: inline" runat="server" Text="Get Premium"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton><br>
									<cmsb:cmsbutton class="clsButton" id="btnGenerate_Policy" runat="server" Text="Generate Policy"
										style="DISPLAY:none"></cmsb:cmsbutton>
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td><webcontrol:gridspacer id="Gridspacer3" runat="server"></webcontrol:gridspacer></td>
				</tr>
			</TABLE>
			<TABLE cellSpacing="0" cellPadding="0" width="90%" align="center" border="0">
				<tr>
					<TD class="headereffectcenter" colSpan="4"><span id="spnURStatus" runat="server">Underwriting 
							Rules Status</span></TD>
				</tr>
				<tr>
					<td>
						<div class="midcolora" id="myDIV" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 189px" align="center" runat="server"></div>
					</td>
				</tr>
				<tr>
					<td class="midcolorr" align="left" colSpan="4">
					<cmsb:cmsbutton class="clsButton" id="btnComitAynway" runat="server" Text="Commit Anyway"></cmsb:cmsbutton>
					<cmsb:cmsbutton class="clsButton" id="btnCommitAnywayInProgress" runat="server" Text="Commit in Progress"></cmsb:cmsbutton>
					</td>
				</tr>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidROW_ID" type="hidden" name="hidROW_ID" runat="server">
			<input id="hidPOLICY_ID" type="hidden" name="hidPOLICY_ID" runat="server"> <input id="hidPOLICY_VERSION_ID" type="hidden" name="hidPOLICY_VERSION_ID" runat="server">
			<input id="hidCUSTOMER_ID" type="hidden" name="hidCUSTOMER_ID" runat="server"> <input id="hidPROCESS_ID" type="hidden" name="hidPROCESS_ID" runat="server">
			<INPUT id="hidDisplayBody" type="hidden" name="hidDisplayBody" runat="server"> <input id="hidNEW_POLICY_VERSION_ID" type="hidden" name="hidNEW_POLICY_VERSION_ID" runat="server">
			<INPUT id="hidLOB_ID" type="hidden" name="hidLOB_ID" runat="server" value="0"> <INPUT id="hidADD_INT_ID" type="hidden" name="hidADD_INT_ID" runat="server">
			<INPUT id="hidUNDERWRITER" type="hidden" name="hidUNDERWRITER" runat="server">
            <input id="hidCOUNT" type="hidden" name="hidCOUNT" runat="server">
            <INPUT id="hidpopup" type="hidden" name="hidpopup" runat="server">
		</form>
	</body>
</HTML>
