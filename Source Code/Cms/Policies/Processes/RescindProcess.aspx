<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="PolicyTop" Src="/cms/cmsweb/webcontrols/PolicyTop.ascx" %>
<%@ Page language="c#" Codebehind="RescindProcess.aspx.cs" ValidateRequest="false"  AutoEventWireup="false" Inherits="Cms.Policies.Processes.RescindProcess" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Policy Rescind Process</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script language="javascript">
		var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		
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
				document.getElementById('txtEFFECTIVE_DATETIME').value  = '';
				document.getElementById('txtEFFECTIVE_HOUR').value  = '12';
				document.getElementById('txtEFFECTIVE_MINUTE').value  = '01';
				document.getElementById('cmbMERIDIEM').options.selectedIndex ='0';
				document.getElementById('cmbCANCELLATION_OPTION').options.selectedIndex = -1;
				document.getElementById('cmbCANCELLATION_TYPE').options.selectedIndex = -1;
				document.getElementById('cmbREASON').options.selectedIndex = -1;
				document.getElementById('txtOTHER_REASON').value  = '';
				document.getElementById('cmbREQUESTED_BY').options.selectedIndex = -1;				
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
						populateFormData(document.getElementById("hidOldData").value,  PROCESS_RESCIND);
					}
					else
					{
						AddData();
					}
				}
				//Check();
				//CommentEnable();
				return false;
			}
				
	
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
		function Check()
	{
		if (document.getElementById('cmbREASON').selectedIndex != '-1')		
		{

				
				if (document.getElementById('cmbREASON').options[document.getElementById('cmbREASON').selectedIndex].value == '11560')
				{
					document.getElementById('txtOTHER_REASON').style.display='inline';
					//document.getElementById('lblOTHER_REASON').style.display='none';
					document.getElementById("rfvOTHER_REASON").setAttribute('enabled',true);
					document.getElementById("rfvOTHER_REASON").setAttribute('isValid',true);
					document.getElementById('spnOTHER_REASON').style.display='inline';				
				}
				else
				{
					//document.getElementById('txtOTHER_REASON').style.display='none';
					//document.getElementById('lblOTHER_REASON').style.display='inline';
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
		
		function DisplayAgentPhoneNo()
		{
			if (document.getElementById('cmbREQUESTED_BY').options[document.getElementById('cmbREQUESTED_BY').selectedIndex].value == '1')
			{
				document.getElementById('lblAGENT_PHONE_NUMBER').style.display = 'inline';
				document.getElementById('capAGENTPHONENO').style.display = 'inline';
			}
			else
			{
				document.getElementById('lblAGENT_PHONE_NUMBER').style.display = 'none';
				document.getElementById('capAGENTPHONENO').style.display = 'none';
			}
		}
		
		function ShowDetailsPolicy()
		{
			//top.botframe.callItemClicked('1,2,3','/cms/policies/aspx/policytab.aspx?CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPOLICY_ID').value + '&POLICY_VERSION_ID=' + document.getElementById('hidPOLICY_VERSION_ID').value + '&');
			if (document.getElementById('hidNEW_POLICY_VERSION_ID').value!="" && document.getElementById('hidNEW_POLICY_VERSION_ID').value!="0")
				window.open('/Cms/Application/Aspx/DecPage.aspx?CALLEDFOR=DECPAGE&CALLEDFROM=POLICY&CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPOLICY_ID').value + '&POLICY_VER_ID=' + document.getElementById('hidNEW_POLICY_VERSION_ID').value + '&');
			else
				window.open('/Cms/Application/Aspx/DecPage.aspx?CALLEDFOR=DECPAGE&CALLEDFROM=POLICY&CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPOLICY_ID').value + '&POLICY_VER_ID=' + document.getElementById('hidPOLICY_VERSION_ID').value + '&');
			return false;
		}
		
		function formReset()
		{
			document.PROCESS_RESCIND.reset();			
			//CommentEnable();
			//Check();
			DisplayAgentPhoneNo();
			DisableValidators();
			ChangeColor();
			return false;
		}
		
		function chkSEND_ALL_Change()			
		{
			var chk = document.getElementById('chkSEND_ALL');
			if(chk==null)
				return false;								
			document.getElementById("hidADD_INT_ID").value='';
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
		function DisplayOtherRescissionDate()
		{		
			if (document.getElementById('cmbOtherRescissionDate').options[document.getElementById('cmbOtherRescissionDate').selectedIndex].value == '1')
			{
				document.getElementById('capDateTime').style.display = 'inline';
				document.getElementById('txtDateTime').style.display = 'inline';
				document.getElementById('imgDateTime').style.display = 'inline';
				document.getElementById('OtherResDateMandatory').style.display='inline';				
				EnableValidator('rfvDateTime',true);
				EnableValidator('revDateTime',true);				
			}
			else
			{
				document.getElementById('capDateTime').style.display = 'none';
				document.getElementById('txtDateTime').style.display = 'none';
				document.getElementById('imgDateTime').style.display = 'none';
				document.getElementById('OtherResDateMandatory').style.display='none';
				EnableValidator('rfvDateTime',false);
				EnableValidator('revDateTime',false);
								
			}		
		}
		
		function cmbADD_INT_Change()
		{
			combo = document.getElementById('cmbADD_INT');
			if(combo==null || combo.selectedIndex==-1)
				return false;
			
			if(combo.options[combo.selectedIndex].value=="<%=((int)Cms.BusinessLayer.BlProcess.clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString()%>")
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
				document.getElementById('hidADD_INT_ID').value = '';
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
				if(document.getElementById('btnRollBack'))
					document.getElementById('btnRollBack').disabled = true;
     			 DisableButton(document.getElementById('btnComplete'));
				top.topframe.disableMenus("1,7","ALL");//Added for Itrack Issue 6203 on 31 July 2009		
				}	
			}
			function HideShowCommit()
			{
				document.getElementById('btnComplete').disabled = true;
				document.getElementById('btnSave').disabled = true;
     			 DisableButton(document.getElementById('btnRollBack'));
     			top.topframe.disableMenus("1,7","ALL");//Added for Itrack Issue 6203 on 31 July 2009
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
			function CallService()
			{ 			
				
				//if( typeof(myTSMain.useService) == 'undefined') 
				//{
				//	setTimeout( 'CallService()', 1000);
				//}
				//else 
				//{
				//	myTSMain.useService(lstr.toString(), "TSM");
					if(document.getElementById('txtEFFECTIVE_DATETIME').value!="")
					{
						var strEffectiveDate = document.getElementById("txtEFFECTIVE_DATETIME").value = FormatDateForGrid(document.getElementById("txtEFFECTIVE_DATETIME"),'');						
						if(DateComparer(strEffectiveDate,strEffectiveDate,jsaAppDtFormat))
						{
							//myTSMain.TSM.callService(createData, "CancelProcReturnPremium", document.getElementById('hidCUSTOMER_ID').value ,document.getElementById('hidPOLICY_ID').value,document.getElementById('hidPOLICY_VERSION_ID').value,strEffectiveDate);
							RescindProcess.AjaxCancelProcReturnPremium(document.getElementById('hidCUSTOMER_ID').value ,document.getElementById('hidPOLICY_ID').value,document.getElementById('hidPOLICY_VERSION_ID').value,strEffectiveDate,createData);
						}
					}
					
					
				//}	
				
			}		
			function createData(Result)
			{
				//alert(formatCurrency(Result.value));
				if(!(Result.error) && Result.value!="undefined" && Result.value != "" && document.getElementById('hidCOMMIT_FLAG').value!="True")
				{
					document.getElementById("txtRETURN_PREMIUM").value = formatCurrency(Result.value);
				}
			}
			
			function Init()
			{
				DisplayOtherRescissionDate();
				ApplyColor();
				ChangeColor();
				top.topframe.main1.mousein = false;
				ShowHideAddIntCombos(true);
				findMouseIn(); 
				populateXML();
				DisplayBody();
				document.getElementById("txtRETURN_PREMIUM").value = formatCurrency(document.getElementById("txtRETURN_PREMIUM").value);
				SetAssignAddInt();
				DisplayAgentPhoneNo();
				cmbADD_INT_Change();
				Check();
				ApplyColor();
				ChangeColor();				
				//cmbCANCELLATION_TYPE_Change(true);
				DisplayOtherRescissionDate();
				document.getElementById('btnCommitInProgress').style.display="none";

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
	<BODY leftMargin="0" topMargin="0" scroll="yes" onload="Init();">
		<FORM id="PROCESS_RESCIND" method="post" runat="server">
		 <DIV><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu></DIV>
			<!-- To add bottom menu start -->
			<!-- To add bottom menu end -->
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
					<td class="headereffectcenter">Rescind&nbsp;Process</td>
				</tr>
				<tr>
				</tr>
				<tr>
					<td id="tdGridHolder"><webcontrol:gridspacer id="Gridspacer2" runat="server"></webcontrol:gridspacer><asp:placeholder id="GridHolder" runat="server"></asp:placeholder></td>
				</tr>
				<tr>
					<TD class="pageHeader" colSpan="4">Please note that all fields marked with <span class="mandatory">
							*</span> are mandatory</TD>
				</tr>
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				</tr>
				<tr id="trBody">
					<td>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capEFFECTIVE_DATETIME" runat="server" Text="Effective Date of Rescind"></asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtEFFECTIVE_DATETIME" runat="server" maxlength="11" size="12"></asp:textbox><asp:hyperlink id="hlkEFFECTIVE_DATE" runat="server" CssClass="HotSpot">
										<ASP:IMAGE id="imgEFFECTIVE_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
									</asp:hyperlink><BR>
									<asp:requiredfieldvalidator id="rfvEFFECTIVE_DATETIME" runat="server" Display="Dynamic" ErrorMessage="EFFECTIVE_DATETIME can't be blank."
										ControlToValidate="txtEFFECTIVE_DATETIME"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revEFFECTIVE_DATETIME" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtEFFECTIVE_DATETIME"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capEFFECTIVE_TIME" runat="server"></asp:label><span class="mandatory">*</span></TD>
								<td class="midcolora" width="32%"><asp:textbox id="txtEFFECTIVE_HOUR" runat="server" maxlength="2" size="3">12</asp:textbox><asp:label id="lblEFFECTIVE_HOUR" runat="server">HH</asp:label><asp:textbox id="txtEFFECTIVE_MINUTE" runat="server" maxlength="2" size="3">01</asp:textbox><asp:label id="Label1" runat="server">MM</asp:label><asp:dropdownlist id="cmbMERIDIEM" runat="server"></asp:dropdownlist><!--Removed onfocus="SelectComboIndex('cmbMERIDIEM')" by Charles on 31-Aug-09 for Itrack 6323 --><BR>
									<asp:requiredfieldvalidator id="rfvEFFECTIVE_HOUR" runat="server" Display="Dynamic" ErrorMessage="Effective Time can't be blank.<br>"
										ControlToValidate="txtEFFECTIVE_HOUR"></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="rfvEFFECTIVE_MINUTE" runat="server" Display="Dynamic" ErrorMessage="Effective Time can't be blank.<br>"
										ControlToValidate="txtEFFECTIVE_MINUTE"></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="rfvMERIDIEM" runat="server" Display="Dynamic" ErrorMessage="cmbMERIDIEM can't be blank."
										ControlToValidate="cmbMERIDIEM"></asp:requiredfieldvalidator>
									<!-- Range Validator --><asp:rangevalidator id="rnvEFFECTIVE_HOUR" runat="server" Display="Dynamic" ControlToValidate="txtEFFECTIVE_HOUR"
										Type="Currency" MaximumValue="12" MinimumValue="0" Text="Hours must be from 0 to 12.<br>"></asp:rangevalidator><!-- Added Text for rangevalidator by Charles on 31-Aug-09 for Itrack 6323 --><asp:rangevalidator id="rnvtEFFECTIVE_MINUTE" runat="server" Text="Minutes must be from 0 to 59.<br>"
										Display="Dynamic" ControlToValidate="txtEFFECTIVE_MINUTE" Type="Integer" MaximumValue="59" MinimumValue="0"></asp:rangevalidator><!-- customvalidator added by Charles on 31-Aug-09 for Itrack 6323 --><asp:customvalidator id="csvEFFECTIVE_HOUR" Display="Dynamic" ControlToValidate="txtEFFECTIVE_HOUR" Runat="server"
											ClientValidationFunction="txtEFFECTIVE_HOUR_VALIDATE" ErrorMessage="HH 00 or 0 must be in AM"></asp:customvalidator></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCANCELLATION_TYPE" runat="server">Type</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCANCELLATION_TYPE" onfocus="SelectComboIndex('cmbCANCELLATION_TYPE')" runat="server"></asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvCANCELLATION_TYPE" runat="server" Display="Dynamic" ErrorMessage="CANCELLATION_TYPE can't be blank."
										ControlToValidate="cmbCANCELLATION_TYPE"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capCANCELLATION_OPTION" runat="server">Option</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCANCELLATION_OPTION" onfocus="SelectComboIndex('cmbCANCELLATION_OPTION')"
										runat="server"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvCANCELLATION_OPTION" runat="server" Display="Dynamic" ErrorMessage="CANCELLATION_OPTION can't be blank."
										ControlToValidate="cmbCANCELLATION_OPTION"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capREASON" runat="server">Reason</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbREASON" onfocus="SelectComboIndex('cmbREASON')" runat="server" onChange="Check();"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvREASON" runat="server" Display="Dynamic" ErrorMessage="REASON can't be blank."
										ControlToValidate="cmbREASON"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capOTHER_REASON" runat="server">Reason</asp:label><span class="mandatory" id="spnOTHER_REASON">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtOTHER_REASON" runat="server" maxlength="0" TextMode="MultiLine" Columns="50"
										Rows="10"></asp:textbox><BR>
										<!--ClientValidationFunction added by Charles on 12-Aug-09 for Itrack 6251 -->
									<asp:customvalidator id="csvOTHER_REASON" Display="Dynamic" ControlToValidate="txtOTHER_REASON" Runat="server" ClientValidationFunction="txtCOMMENTS_VALIDATE"></asp:customvalidator>
									<asp:requiredfieldvalidator id="rfvOTHER_REASON" runat="server" Display="Dynamic" ErrorMessage="OTHER_REASON can't be blank." ControlToValidate="txtOTHER_REASON"></asp:requiredfieldvalidator>
								</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capREQUESTED_BY" runat="server">By</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbREQUESTED_BY" onfocus="SelectComboIndex('cmbREQUESTED_BY')" runat="server"></asp:dropdownlist></TD>
								<TD class="midcolora" width="18%"><asp:label id="capAGENTPHONENO" runat="server">Agent Phone No</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:label id="lblAGENT_PHONE_NUMBER" runat="server" CssClass="labelfont"></asp:label></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capOtherRescissionDate" runat="server"> Date </asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbOtherRescissionDate" runat="server"></asp:dropdownlist></TD>
								<TD class="midcolora" width="18%"><asp:label id="capDateTime" runat="server"> Date </asp:label><span class="mandatory" id="OtherResDateMandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtDateTime" runat="server" maxlength="11" size="12"></asp:textbox><asp:hyperlink id="hlkDateTime" runat="server" CssClass="HotSpot">
										<ASP:IMAGE id="imgDateTime" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
									</asp:hyperlink><BR>
									<asp:requiredfieldvalidator id="rfvDateTime" runat="server" Display="Dynamic" ErrorMessage="Other Rescission date can't be blank."
										ControlToValidate="txtDateTime"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revDateTime" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtDateTime"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capRETURN_PREMIUM" runat="server">Premium</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtRETURN_PREMIUM" CssClass="INPUTCURRENCY" maxlength="8" size="12" Runat="server"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revRETURN_PREMIUM" Display="Dynamic" ControlToValidate="txtRETURN_PREMIUM" Runat="server"></asp:regularexpressionvalidator>
									<%--<asp:label id="lblRETURN_PREMIUM" CssClass="labelfont" Runat="server"></asp:label>--%>
								</TD>
								<TD class="midcolora" colspan="2"></TD>
								<!--
								<TD class="midcolora" width="18%"><asp:label id="capPAST_DUE_PREMIUM" runat="server">Due</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:label id="lblPAST_DUE_PREMIUM" CssClass="labelfont" Runat="server"></asp:label></TD>
								-->
							</tr>
							<!--
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capPRINT_COMMENTS" runat="server">Comments</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPRINT_COMMENTS" onfocus="SelectComboIndex('cmbPRINT_COMMENTS')" runat="server"></asp:dropdownlist></TD>
								<TD class="midcolora" width="18%"><asp:label id="capCOMMENTS" runat="server">Comments</asp:label><span class="mandatory" id="CommentsMandatory">*</span></TD>
								<TD class="midcolora"><asp:textbox id="txtCOMMENTS" runat="server" maxlength="0" TextMode="MultiLine" Columns="50"
										Rows="10"></asp:textbox><asp:label id="lblCOMM" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
									   requiredfieldvalidator id="rfvCOMMENTS" runat="server" Display="Dynamic" ControlToValidate="txtCOMMENTS"></asp:requiredfieldvalidator>
									     customvalidator id="csvCOMMENTS" Display="Dynamic" ControlToValidate="txtCOMMENTS" Runat="server"
										ClientValidationFunction="txtCOMMENTS_VALIDATE"></asp:customvalidator></TD>
							</tr>
							-->
							<tr>
								<TD class="headerEffectSystemParams" colSpan="4">Printing Options Details</TD>
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
								<TD class="headerEffectSystemParams" colSpan="4">Additional Interest Details</TD>
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
							<!--
							<tr>
								<TD class="headerEffectSystemParams" colSpan="4">Letter</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capSTD_LETTER_REQD" runat="server">Standard Letter Required</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:DropDownList ID="cmbSTD_LETTER_REQD" Runat="server"></asp:DropDownList></TD>
								<TD class="midcolora" width="18%"><asp:label id="capCUSTOM_LETTER_REQD" runat="server">Customized Letter Required</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:DropDownList ID="cmbCUSTOM_LETTER_REQD" Runat="server"></asp:DropDownList></TD>
							</tr>
							-->
							<!-- End Print Oprion -->
							<!-- cms buttons  -->
							<tr>
								<td class="midcolora" align="right" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnRollBack" runat="server" Text="Rollback" CausesValidation="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnPolicyDetails" runat="server" Text="View Dec Page"></cmsb:cmsbutton><br>
									<cmsb:cmsbutton class="clsButton" id="btnBack_To_Search" runat="server" Text="Back To Search" CausesValidation="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnBack_To_Customer_Assistant" runat="server" Text="Back To Customer Assistant"
										CausesValidation="false"></cmsb:cmsbutton></td>
								<td class="midcolorr" align="left" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnComplete" runat="server" Text="Commit"></cmsb:cmsbutton>
								<cmsb:cmsbutton class="clsButton" id="btnCommitInProgress" runat="server" Text="Commit in Progress"></cmsb:cmsbutton>
								<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton><br>
									<cmsb:cmsbutton class="clsButton" id="btnCalculate_Return_premium" runat="server" Text="Calculate Return premium"
										style="DISPLAY:none"></cmsb:cmsbutton></td>
							</tr>
						</TABLE>
					</td>
				</tr>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidROW_ID" type="hidden" value="0" name="hidROW_ID" runat="server">
			<INPUT id="hidPOLICY_ID" type="hidden" name="hidPOLICY_ID" runat="server"> <INPUT id="hidPOLICY_VERSION_ID" type="hidden" name="hidPOLICY_VERSION_ID" runat="server">
			<INPUT id="hidCUSTOMER_ID" type="hidden" name="hidCUSTOMER_ID" runat="server"> <INPUT id="hidPROCESS_ID" type="hidden" name="hidPROCESS_ID" runat="server">
			<INPUT id="Hidden1" type="hidden" value="0" name="hidROW_ID" runat="server"> <INPUT id="hidDisplayBody" type="hidden" name="hidDisplayBody" runat="server">
			<INPUT id="hidLOB_ID" type="hidden" value="0" name="hidLOB_ID" runat="server"> <INPUT id="hidADD_INT_ID" type="hidden" name="hidADD_INT_ID" runat="server">
			<INPUT id="hidUNDERWRITER" type="hidden" value="0" name="hidUNDERWRITER" runat="server">
			<INPUT id="hidPRE_STATUS" type="hidden" value="0" name="hidPRE_STATUS" runat="server">
			<INPUT id="hidNEW_POLICY_VERSION_ID" type="hidden" name="hidNEW_POLICY_VERSION_ID" runat="server">
			<INPUT id="hidPOLICY_PREVIOUS_STATUS" type="hidden" name="hidPOLICY_PREVIOUS_STATUS" runat="server">
			<INPUT id="hidPOLICY_CURRENT_STATUS" type="hidden" name="hidPOLICY_CURRENT_STATUS" runat="server">
			<INPUT id="hidCOMMIT_FLAG" type="hidden" name="hidCOMMIT_FLAG" runat="server">
		</FORM>
	</BODY>
</HTML>
