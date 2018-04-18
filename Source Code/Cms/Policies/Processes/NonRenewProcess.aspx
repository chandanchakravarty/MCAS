<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="PolicyTop" Src="/cms/cmsweb/webcontrols/PolicyTop.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="NonRenewProcess.aspx.cs" validateRequest=false AutoEventWireup="false" Inherits="Policies.Processes.NonRenewProcess" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>NonRenewProcess</title>
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
			top.topframe.enableMenu('1,2,3');
			//top.topframe.disableMenus("1,3","ALL");
			//selectedLOB = lobMenuArr[document.getElementById('hidLOBID').value];
			//top.topframe.enableMenu("1,3," + selectedLOB);			
			
		}
		
		function AddData()
		{
			document.getElementById('hidROW_ID').value	=	'New';
			document.getElementById('txtEFFECTIVE_DATETIME').focus();
			document.getElementById('txtOTHER_REASON').value  = '';
			//
				document.getElementById('cmbCANCELLATION_TYPE').options.selectedIndex = -1;
				document.getElementById('cmbREASON').options.selectedIndex = -1;
				document.getElementById('txtOTHER_REASON').value  = '';
				/*document.getElementById('cmbREQUESTED_BY').options.selectedIndex = -1;				
				document.getElementById('lblAGENT_PHONE_NUMBER').value  = '';
				document.getElementById('txtRETURN_PREMIUM').value  = '';
				document.getElementById('lblPAST_DUE_PREMIUM').value  = '';						
				document.getElementById('cmbPRINT_COMMENTS').options.selectedIndex = -1;	
				document.getElementById('txtCOMMENTS').value  = '';*/
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
					populateFormData(document.getElementById("hidOldData").value, NonRenewProcess);
			
				}
				else
				{
					AddData();
				}
			}
			return false;
		}
		function ShowTextFiled()
		{
		
			if(document.getElementById('cmbReason').options[document.getElementById('cmbReason').selectedIndex].text =="Other")
			{
				document.getElementById('txtOTHER_REASON').style.display="inline";
				document.getElementById('capOTHER_REASON').style.display="inline";
				document.getElementById('spnOTHER_REASON').style.display='inline';				
				document.getElementById('rfvOTHER_REASON').setAttribute("enabled",true);
			}
			else
			{
				//document.getElementById('txtOTHER_REASON').style.display="none";
				//document.getElementById('capOTHER_REASON').style.display="none";
				document.getElementById('spnOTHER_REASON').style.display='none';				
				document.getElementById('rfvOTHER_REASON').setAttribute("enabled",false);   
				document.getElementById('rfvOTHER_REASON').style.display ="none";
			}
			
		Check();
		}
		function Check()
		{
			if (document.getElementById('cmbREASON').selectedIndex != '-1')		
			{
	
				if (document.getElementById('cmbREASON').options[document.getElementById('cmbREASON').selectedIndex].value == '11505'
				|| document.getElementById('cmbREASON').options[document.getElementById('cmbREASON').selectedIndex].value == '11517'
				|| document.getElementById('cmbREASON').options[document.getElementById('cmbREASON').selectedIndex].value == '11510'
				|| document.getElementById('cmbREASON').options[document.getElementById('cmbREASON').selectedIndex].value == '11528'
				|| document.getElementById('cmbREASON').options[document.getElementById('cmbREASON').selectedIndex].value == '11523'
				)
				{
				
					document.getElementById('txtOTHER_REASON').style.display='inline';
					//document.getElementById('lblOTHER_REASON').style.display='none';
					document.getElementById('capOTHER_REASON').style.display="inline";
					document.getElementById("rfvOTHER_REASON").setAttribute('enabled',true);
					document.getElementById("rfvOTHER_REASON").setAttribute('isValid',true);
					document.getElementById('spnOTHER_REASON').style.display='inline';				
				}
				else
				{
				
					//document.getElementById('txtOTHER_REASON').style.display='none';
					//document.getElementById('capOTHER_REASON').style.display='none';
					document.getElementById("rfvOTHER_REASON").setAttribute('enabled',false);
					document.getElementById("rfvOTHER_REASON").setAttribute('isValid',false);
					document.getElementById("rfvOTHER_REASON").style.display = "none";
					document.getElementById('spnOTHER_REASON').style.display='none';
				
				}
				ApplyColor();
				ChangeColor();
			}
			else
			{
				//document.getElementById('txtOTHER_REASON').style.display='none';
				//document.getElementById('lblOTHER_REASON').style.display='inline';
				document.getElementById("rfvOTHER_REASON").setAttribute('enabled',false);
				document.getElementById("rfvOTHER_REASON").setAttribute('isValid',false);
				document.getElementById("rfvOTHER_REASON").style.display = "none";
				document.getElementById('spnOTHER_REASON').style.display='none';
				ApplyColor();
				ChangeColor();
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
			/*
			if (document.getElementById('hidNEW_POLICY_VERSION_ID').value!="" && document.getElementById('hidNEW_POLICY_VERSION_ID').value!="0")
				top.botframe.callItemClicked('1,2,3','/cms/policies/aspx/policytab.aspx?CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPolicy_id').value + '&POLICY_VERSION_ID=' + document.getElementById('hidNEW_POLICY_VERSION_ID').value + '&');
			else
				top.botframe.callItemClicked('1,2,3','/cms/policies/aspx/policytab.aspx?CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPolicy_id').value + '&POLICY_VERSION_ID=' + document.getElementById('hidPOLICY_VERSION_ID').value + '&');
			*/
			if (document.getElementById('hidNEW_POLICY_VERSION_ID').value!="" && document.getElementById('hidNEW_POLICY_VERSION_ID').value!="0")
				window.open('/Cms/Application/Aspx/DecPage.aspx?CALLEDFOR=DECPAGE&CALLEDFROM=POLICY&CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPolicy_id').value + '&POLICY_VER_ID=' + document.getElementById('hidNEW_POLICY_VERSION_ID').value + '&');
			else
				window.open('/Cms/Application/Aspx/DecPage.aspx?CALLEDFOR=DECPAGE&CALLEDFROM=POLICY&CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPolicy_id').value + '&POLICY_VER_ID=' + document.getElementById('hidPOLICY_VERSION_ID').value + '&');
			
			return false;
		}
		
		function formReset()
		{
			document.NonRenewProcess.reset();
			return false;
		}
//
	function CommentEnable()
	{
		if (document.getElementById('cmbPRINT_COMMENTS').selectedIndex != '-1')		
		{
		
				if (document.getElementById('cmbPRINT_COMMENTS').options[document.getElementById('cmbPRINT_COMMENTS').selectedIndex].value == '0')
				{				
					document.getElementById('txtCOMMENTS').style.display='none';
					//document.getElementById('txtCOMMENTS').value = '';
					document.getElementById('rfvCOMMENTS').enabled = false;
					document.getElementById('rfvCOMMENTS').style.display='none';
					document.getElementById('CommentsMandatory').style.display='none';
					document.getElementById('lblCOMM').style.display='inline';						
				}
				else
				{			
					document.getElementById('txtCOMMENTS').style.display='inline';
					//document.getElementById('rfvCOMMENTS').style.display='inline';
					document.getElementById('CommentsMandatory').style.display='inline';
					document.getElementById('lblCOMM').style.display='none';				
				}
		}
		else
		{
				document.getElementById('txtCOMMENTS').style.display='none';
				document.getElementById('rfvCOMMENTS').style.display='none';
				document.getElementById('CommentsMandatory').style.display='none';
				document.getElementById('lblCOMM').style.display='inline';		
				//document.getElementById('txtCOMMENTS').value = '';
		}
	
	}
	
		//Validates the maximum length for comments
		function txtCOMMENTS_VALIDATE(source, arguments)
		{
				var txtArea = arguments.Value;
				if(txtArea.length > 250 ) 
				{
					arguments.IsValid = false;
					return false;   // invalid userName
				}
		}
		function Init()
		{
			showScroll();			
			top.topframe.main1.mousein = false;
			findMouseIn();
			populateXML();
			DisplayBody();			
			cmbADD_INT_Change();
			//chkSEND_ALL_Change();
			//ShowHideAddIntCombos(true);
			SetAssignAddInt();
			ApplyColor();
			ChangeColor();			

			ShowTextFiled();
			ApplyColor();
			ChangeColor();	
			document.getElementById('btnCommitInProgress').style.display="none";
			//chkSEND_ALL_Change();		
			//Added for Itrack Issue 6203 on 31 July 2009
			if (top.topframe.main1.menuXmlReady == false)
				setTimeout("top.topframe.enableMenus('1,7','ALL');",1000);
			else	
				top.topframe.enableMenus('1,7','ALL');
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
		function cmbADD_INT_Change()
		{
			combo = document.getElementById('cmbADD_INT');
			if(combo==null || combo.selectedIndex==-1)
				return false;
			
			if(combo.options[combo.selectedIndex].value=="<%=((int)Cms.BusinessLayer.BlProcess.clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL).ToString()%>")
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
			function HideShowCommitInProgress()
			{
				Page_ClientValidate();	
				if(!Page_IsValid)
				{
				return false;
				}
				else
				{
				document.getElementById('btnCommitInProgress').style.display="inline";
				document.getElementById('btnCommitInProgress').disabled = true;
				document.getElementById('btnComplete').style.display="none";
				document.getElementById('btnSave').disabled = true;	
				if(document.getElementById('btnRollback'))
					document.getElementById('btnRollback').disabled = true;	
     			 DisableButton(document.getElementById('btnComplete'));
     			 top.topframe.disableMenus("1,7","ALL");//Added for Itrack Issue 6203 on 31 July 2009
				}
			}
			function HideShowCommit()
			{
				
				document.getElementById('btnComplete').disabled = true;
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
				}
				
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
			
		function txtEFFECTIVE_HOUR_VALIDATE(source, arguments)//Function added by Charles on 31-Aug-09 for Itrack 6323
		{	
			var EFFECTIVE_HOUR= document.getElementById('txtEFFECTIVE_HOUR').value.replace(' ','');			
			if((EFFECTIVE_HOUR == '00' || EFFECTIVE_HOUR == '0') && document.getElementById('cmbMERIDIEM').options[1].selected == '1')
				{
					arguments.IsValid = false;
					return;					
				}
		}
		function disableHourValidator() //Function added by Charles on 1-Sep-09 for Itrack 6323
		{
			var EFFECTIVE_HOUR= document.getElementById('txtEFFECTIVE_HOUR').value.replace(' ','');
			if(document.getElementById('cmbMERIDIEM').options[1].selected == '1' && (EFFECTIVE_HOUR == '00' || EFFECTIVE_HOUR == '0') )
			{	
				document.getElementById('csvEFFECTIVE_HOUR').setAttribute('isValid',false);
				document.getElementById('csvEFFECTIVE_HOUR').style.display='inline';				
			}
			else
			{
				document.getElementById('csvEFFECTIVE_HOUR').setAttribute('isValid',true);
				document.getElementById('csvEFFECTIVE_HOUR').style.display='none';
			}
			
		}

		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" scroll="yes" onload="Init();">
		<form id="NonRenewProcess" method="post" runat="server">
			<DIV><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu></DIV>
			<!-- To add bottom menu -->
			<!-- To add bottom menu ends here -->
			<TABLE cellSpacing="0" cellPadding="0" width="90%" align="center" border="0">
				<TBODY>
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
						<td class="headereffectcenter"><asp:Label ID="capHeader" runat="server"></asp:Label></td><%--Non-Renew Process--%>
					</tr>
					<tr>
						<TD class="pageHeader" colSpan="4"><asp:label ID="capMessage" runat="server" ></asp:label></TD><%--Please note that all fields marked with * are mandatory--%>
					</tr>
					<tr>
						<td class="midcolorc" style="HEIGHT: 25px" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
					</tr>
					<tr id="trBody">
						<td>
							<table>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capCANCELLATION_TYPE" runat="server">Type</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCANCELLATION_TYPE" onfocus="SelectComboIndex('cmbCANCELLATION_TYPE')" runat="server"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvCANCELLATION_TYPE" runat="server" ControlToValidate="cmbCANCELLATION_TYPE"
											Display="Dynamic" ErrorMessage="Type can't be blank."></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capCANCELLATION_OPTION" runat="server">Option</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCANCELLATION_OPTION" onfocus="SelectComboIndex('cmbCANCELLATION_OPTION')"
											runat="server"></asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvCANCELLATION_OPTION" runat="server" ControlToValidate="cmbCANCELLATION_OPTION"
											Display="Dynamic" ErrorMessage=""></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capEFFECTIVE_DATETIME" Runat="server"></asp:label><span class="mandatory" id="spnEFFECTIVE_DATETIME">*</span>
									</TD>
									<td class="midcolora" width="32%"><asp:textbox id="txtEFFECTIVE_DATETIME" runat="server" maxlength="10"></asp:textbox><asp:hyperlink id="hlkEFFECTIVE_DATETIME" runat="server" CssClass="HotSpot">
											<ASP:IMAGE id="imgEFFECTIVE_DATETIME" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
										</asp:hyperlink><BR>
										<asp:regularexpressionvalidator id="revEFFECTIVE_DATETIME" Runat="server" ValidationExpression="/b" ControlToValidate="txtEFFECTIVE_DATETIME"
											Display="Dynamic"></asp:regularexpressionvalidator><asp:requiredfieldvalidator id="rfvEFFECTIVE_DATETIME" Runat="server" ControlToValidate="txtEFFECTIVE_DATETIME"
											Display="Dynamic"></asp:requiredfieldvalidator>
										<asp:RangeValidator ID="rngEFFECTIVE_DATE" Runat="server" Display="Dynamic" ControlToValidate="txtEFFECTIVE_DATETIME"
											Type="Date"></asp:RangeValidator>
									</td>
									<TD class="midcolora" width="18%"><asp:label id="capEFFECTIVE_TIME" runat="server"></asp:label><span class="mandatory">*</span>
									</TD>
									<td class="midcolora" width="32%"><asp:textbox id="txtEFFECTIVE_HOUR" runat="server" maxlength="2" size="3">12</asp:textbox><asp:label id="lblEFFECTIVE_HOUR" runat="server">HH</asp:label><asp:textbox id="txtEFFECTIVE_MINUTE" runat="server" maxlength="2" size="3">01</asp:textbox><asp:label id="Label1" runat="server">MM</asp:label>
									<asp:dropdownlist id="cmbMERIDIEM" runat="server"></asp:dropdownlist><!--Removed onfocus="SelectComboIndex('cmbMERIDIEM')" by Charles on 31-Aug-09 for Itrack 6323 --><BR>
										<asp:requiredfieldvalidator id="rfvEFFECTIVE_HOUR" runat="server" ControlToValidate="txtEFFECTIVE_HOUR" Display="Dynamic"
											ErrorMessage=""></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="rfvEFFECTIVE_MINUTE" runat="server" ControlToValidate="txtEFFECTIVE_MINUTE"
											Display="Dynamic" ErrorMessage=""></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="rfvMERIDIEM" runat="server" ControlToValidate="cmbMERIDIEM" Display="Dynamic"
											ErrorMessage=""></asp:requiredfieldvalidator>
										<!-- Range Validator --><asp:rangevalidator id="rnvEFFECTIVE_HOUR" runat="server" ControlToValidate="txtEFFECTIVE_HOUR" Display="Dynamic"
											Type="Currency" MaximumValue="12" MinimumValue="0" Text=""></asp:rangevalidator><!-- Added Text for rangevalidator by Charles on 31-Aug-09 for Itrack 6323 --><asp:rangevalidator id="rnvtEFFECTIVE_MINUTE" runat="server" ControlToValidate="txtEFFECTIVE_MINUTE"
											Display="Dynamic" Text="" Type="Integer" MaximumValue="59" MinimumValue="0"></asp:rangevalidator><!-- customvalidator added by Charles on 31-Aug-09 for Itrack 6323 --><asp:customvalidator id="csvEFFECTIVE_HOUR" Display="Dynamic" ControlToValidate="txtEFFECTIVE_HOUR" Runat="server"
											ClientValidationFunction="txtEFFECTIVE_HOUR_VALIDATE" ErrorMessage=""></asp:customvalidator></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capReason" Runat="server"></asp:label><span class="mandatory" id="spnReason">*</span>
									</TD>
									<td class="midcolora" width="32%"><asp:dropdownlist id="cmbReason" onfocus="SelectComboIndex('cmbReason')" Runat="server" onclick="Check();"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvReason" Runat="server" ControlToValidate="cmbReason" Display="Dynamic"></asp:requiredfieldvalidator></td>
									<td class="midcolora" width="18%"><asp:label id="capOTHER_REASON" Runat="server">Reason</asp:label> <span class="mandatory" id="spnOTHER_REASON">*</span> </td>
									<td class="midcolora" width="32%"><asp:textbox id="txtOTHER_REASON" Runat="server" MaxLength="255" TextMode="MultiLine" Columns="50"
											Rows="10"></asp:textbox><BR>
										<asp:customvalidator id="csvOTHER_REASON" Runat="server" ControlToValidate="txtOTHER_REASON" Display="Dynamic"
											ClientValidationFunction="txtCOMMENTS_VALIDATE"></asp:customvalidator>
										<asp:requiredfieldvalidator id="rfvOTHER_REASON" runat="server" ControlToValidate="txtOTHER_REASON" Display="Dynamic"
											ErrorMessage="OTHER_REASON can't be blank."></asp:requiredfieldvalidator></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capINCLUDE_REASON_DESC" runat="server">Include Reason Description on Cancellation/Forms</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:checkbox id="chkINCLUDE_REASON_DESC" Runat="server"></asp:checkbox></TD>
									<td class="midcolora" colSpan="2"></td>
								</tr>
								<!--by pravesh   -->
								<!--
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capREQUESTED_BY" Runat="server"></asp:label><span class="mandatory" id="spnREQUESTED_BY">*</span>
									</TD>
									<td class="midcolora" width="32%"><asp:dropdownlist id="cmbREQUESTED_BY" Runat="server">
											<asp:ListItem Value=""></asp:ListItem>
											<asp:ListItem Value="1">Wolverine</asp:ListItem>
											<asp:ListItem Value="2">Agency</asp:ListItem>
										</asp:dropdownlist><br>
										</td>
								</tr>
								
								
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="Label3" runat="server">By</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="Dropdownlist2" onfocus="SelectComboIndex('cmbREQUESTED_BY')" runat="server"></asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capAGENTPHONENO" runat="server">Agent Phone No</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:label id="lblAGENT_PHONE_NUMBER" runat="server" CssClass="labelfont"></asp:label></TD>
								</tr>
								
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capRETURN_PREMIUM" runat="server">Premium</asp:label></TD>
									<TD class="midcolora" width="32%">
										<asp:TextBox ID="txtRETURN_PREMIUM" Runat="server" CssClass="INPUTCURRENCY" maxlength="8" size="12"></asp:TextBox><br>
										
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capPAST_DUE_PREMIUM" runat="server">Due</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:label id="lblPAST_DUE_PREMIUM" CssClass="labelfont" Runat="server"></asp:label></TD>
								</tr>
								
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPRINT_COMMENTS" runat="server">Comments</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPRINT_COMMENTS" onfocus="SelectComboIndex('cmbPRINT_COMMENTS')" runat="server"></asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capCOMMENTS" runat="server">Comments</asp:label><span class="mandatory" id="CommentsMandatory">*</span></TD>
									<TD class="midcolora"><asp:textbox id="txtCOMMENTS" runat="server" maxlength="0" TextMode="MultiLine" Columns="50"
											Rows="10"></asp:textbox><asp:label id="lblCOMM" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
										
								</tr>
								-->
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4"><asp:Label ID="capPrinting" runat="server"></asp:Label></TD><%--Printing Options Details--%>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPRINTING_OPTIONS" runat="server">No printing Required at all</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:checkbox id="chkPRINTING_OPTIONS" Runat="server"></asp:checkbox></TD>
									<td class="midcolora" colSpan="2"></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capINSURED" runat="server">Insured</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbINSURED" Runat="server"></asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capAGENCY_PRINT" runat="server">Agency</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbAGENCY_PRINT" Runat="server"></asp:dropdownlist></TD>
								</tr>
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4"><asp:Label ID="capAdditional" runat="server"></asp:Label></TD><%--Additional Interest Details--%>
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
												<td class="midcolorc" align="center" width="37%"><asp:label id="capUnassignLossCodes" runat="server">All Additional Interests</asp:label></td>
												<td class="midcolorc" vAlign="middle" align="center" width="33%" rowSpan="7"><br>
													<br>
													<input class="clsButton" id="btnAssignLossCodes" onclick="javascript:AssignAddInt(false);"
														type="button" value=">>" name="btnAssignLossCodes" runat="server"><br>
													<br>
													<input class="clsButton" id="btnUnAssignLossCodes" onclick="javascript:UnAssignAddInt(false);"
														type="button" value="<<" name="btnUnAssignLossCodes" runat="server">
												</td>
												<td class="midcolorc" align="center" width="33%"><asp:label id="capAssignedLossCodes" runat="server">Selected Additional Interests</asp:label></td>
											</tr>
											<tr>
												<td class="midcolorc" align="center" width="33%" rowSpan="6"><asp:listbox id="cmbUnAssignAddInt" Runat="server" SelectionMode="Multiple" Height="150px" Width="300px"></asp:listbox></td>
												<td class="midcolorc" align="center" width="33%" rowSpan="6"><asp:listbox id="cmbAssignAddInt" Runat="server" SelectionMode="Multiple" Height="150px" Width="300px"></asp:listbox></td>
											</tr>
										</table>
									</td>
								</tr>
								<!--end here   -->
								<tr>
									<td class="midcolora" align="right" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnRollback" runat="server" CausesValidation="false" text=""></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnPolicyDetails" runat="server" Visible="false" Text="View Dec Page"></cmsb:cmsbutton><br>
										<cmsb:cmsbutton class="clsButton" id="btnBackToSearch" runat="server" CausesValidation="false" text="Back To Search"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnBackToCustomer" runat="server" CausesValidation="false"
											text="Back To Customer Assistant"></cmsb:cmsbutton></td>
									<td class="midcolorr" align="left" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnPrint" runat="server" text="Print Preview" style="DISPLAY:none"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnComplete" runat="server" text="Commit"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnCommitInProgress" runat="server" Text="Commit in Progress"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton><br>
										<cmsb:cmsbutton class="clsButton" id="btnGenNonRenew" runat="server" text="Generate Non-Renewal"
											style="DISPLAY:none"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnCommit" runat="server" text="Commit To Spool" style="DISPLAY:none"></cmsb:cmsbutton></td>
								</tr>
							</table>
						</td>
					</tr>
				</TBODY>
			</TABLE>
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidROW_ID" type="hidden" name="hidROW_ID" runat="server"> <INPUT id="hidPROCESS_ID" type="hidden" name="hidPROCESS_ID" runat="server">
			<INPUT id="hidDisplayBody" type="hidden" value="True" name="hidDisplayBody" runat="server">
			<INPUT id="hidPOLICY_ID" type="hidden" name="hidPOLICY_ID" runat="server"> <INPUT id="hidPOLICY_VERSION_ID" type="hidden" name="hidPOLICY_VERSION_ID" runat="server">
			<INPUT id="hidCUSTOMER_ID" type="hidden" name="hidCUSTOMER_ID" runat="server"> <INPUT id="hidADD_INT_ID" type="hidden" name="hidADD_INT_ID" runat="server">
			<INPUT id="hidLOB_ID" type="hidden" value="0" name="hidLOB_ID" runat="server"> <INPUT id="hidUNDERWRITER" type="hidden" value="0" name="hidUNDERWRITER" runat="server">
			<INPUT id="hidNEW_POLICY_VERSION_ID" type="hidden" name="hidNEW_POLICY_VERSION_ID" runat="server">
		</form>
	</body>
</HTML>
