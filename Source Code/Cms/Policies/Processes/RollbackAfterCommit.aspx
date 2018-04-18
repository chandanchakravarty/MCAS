<%@ Register TagPrefix="webcontrol" TagName="ProcessLogTop" Src="/cms/cmsweb/webcontrols/ProcessLogTop.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="PolicyTop" Src="/cms/cmsweb/webcontrols/PolicyTop.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="RollbackAfterCommit.aspx.cs" AutoEventWireup="false" Inherits="Policies.Processes.RollbackAfterCommit" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>Revert Commit Process</title>
		<meta content="Microsoft Visual Studio 7.0" name=GENERATOR>
		<meta content=C# name=CODE_LANGUAGE>
		<meta content=JavaScript name=vs_defaultClientScript>
		<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script language=javascript>
		
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
		function ShowDetailsPolicy()
		{
			if (document.getElementById('hidNEW_POLICY_VERSION_ID').value !="" &&  document.getElementById('hidNEW_POLICY_VERSION_ID').value!="0")
				top.botframe.callItemClicked('1,2,3','/cms/policies/aspx/policytab.aspx?CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPOLICY_ID').value + '&POLICY_VERSION_ID=' + document.getElementById('hidNEW_POLICY_VERSION_ID').value + '&');
			else
				top.botframe.callItemClicked('1,2,3','/cms/policies/aspx/policytab.aspx?CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPOLICY_ID').value + '&POLICY_VERSION_ID=' + document.getElementById('hidPOLICY_VERSION_ID').value + '&');
			
			return false;
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
		function VerifyCheck(radio) {
		    
				var frm = document.REVERT_PROCESS ;
				if (radio.checked==true)
				{
					var strRadioId = radio.id;
					var strCommonId=strRadioId.split("_");
					//	var strCheckName=strCommonId[0]+"_"+strCommonId[1]+"_"+"chkSECONDARY_APPLICANT";
					//document.all[strCheckName].checked=true;				
				}
				var radioid = radio.id
				var lastIndex = radioid.lastIndexOf('_'); 
				var hid = radioid.substring(0,lastIndex) + '_hidLAST_REVERT';
			    document.getElementById('hidLAST_REVERT_ID').value = document.getElementById(hid).value;
     			// alert(document.getElementById('hidLAST_REVERT_ID').value);
				for(i=0;i< frm.length;i++)
				 {
					e=frm.elements[i];
					if ( e.type=='radio' && e.id.indexOf("rdbSelect") != -1 )
					{	
						if (e.id!=radio.id)
						{			
							e.checked=false;				
							
						}
					}
				}
			}	
		function CheckLastProcessID()
		{
			if (document.getElementById('hidLAST_REVERT_ID').value=="") {
			    var msg = document.getElementById('hidAlertmsg').value;
			    alert(msg); //alert("Please select the last process you wish to revert upto.");
				return false;
			}
		
			Page_ClientValidate();
			Page_IsValid = Page_IsValid;
			if (Page_IsValid)
				HideShowCommitInProgress();
			if(document.getElementById('txtOTHER_REASON').value=="")
			{
				return Page_IsValid;
			}
				//return false;
			//}
			
		//	document.getElementById('btnCommitInProgress').style.display="inline";
		//	document.getElementById('btnComplete').style.display="none";					

		}
		function HideShowCommitInProgress()
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
		function HideShowCommit()
			{
				document.getElementById('btnComplete').disabled = true;
				document.getElementById('btnSave').disabled = true;
     			 DisableButton(document.getElementById('btnRollBack'));
     			 top.topframe.disableMenus('1,7','ALL');//Added for Itrack Issue 6203 on 31 July 2009
			}
		
		function Init()
			{
				document.getElementById('btnCommitInProgress').style.display="none";
				//Added for Itrack Issue 6203 on 31 July 2009
				if (top.topframe.main1.menuXmlReady == false)
					setTimeout("top.topframe.enableMenus('1,7','ALL');",1000);
				else	
					top.topframe.enableMenus('1,7','ALL');
			}	
		function formReset()
		{
			document.REVERT_PROCESS.reset();
			DisableValidators();
			ChangeColor();
			return false;
		}
		</script>
		<style type="text/css">
		.td
		{
			display:none;
		}
		</style>
</HEAD>
<BODY leftMargin=0 topMargin=0 scroll=yes onload="ApplyColor();ChangeColor();top.topframe.main1.mousein = false;findMouseIn();Init();">
	<FORM id=REVERT_PROCESS method=post runat="server">
	<DIV><webcontrol:menu id=bottomMenu runat="server"></webcontrol:menu></DIV>	
		<!-- To add bottom menu start -->
		<!-- To add bottom menu end -->
		<TABLE cellSpacing=0 cellPadding=0 width="90%" align=center border=0>
			<tr>
				<td><webcontrol:gridspacer id=grdSpacer runat="server"></webcontrol:gridspacer></td></tr>
			<tr>
				<TD><webcontrol:policytop id=cltPolicyTop runat="server"></webcontrol:policytop></TD></tr>
			<tr>
				<td><webcontrol:gridspacer id=Gridspacer1 runat="server"></webcontrol:gridspacer></td></tr>
			<!--<tr>
			<td><webcontrol:gridspacer id="grdSpacer2" runat="server"></webcontrol:gridspacer></td>
			/tr>
			tr>
			<TD><webcontrol:ProcessLogTop id="cltProcessTop" runat="server"></webcontrol:ProcessLogTop></TD>
			/tr -->
			<TR class=midcolora>
				<TD align=center>
					<asp:datagrid id=dgCommitedProcess runat="server" 
                        HeaderStyle-VerticalAlign="Top" ItemStyle-CssClass="DataRow2" 
                        AlternatingItemStyle-CssClass="AlternateDataRow2" DataKeyField="PROCESS_ID" 
                        Width="100%" HeaderStyle-CssClass="headereffectWebGrid" 
                        AutoGenerateColumns="False" onitemdatabound="dgCommitedProcess_ItemDataBound">
						<Columns>
							<asp:TemplateColumn HeaderText="Select" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
								<ItemTemplate>
									<INPUT type="radio" runat="server" onclick="VerifyCheck(this);" id="rdbSelect" name="rdbSelect"	VALUE="rdbSelect">
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="0%" ItemStyle-CssClass="td" HeaderStyle-CssClass="td" >
								<ItemTemplate>
									<INPUT type= "hidden" runat="server" id="hidLAST_REVERT" value='<%# DataBinder.Eval(Container, "DataItem.PROCESS_UNIQUE_ID") %>' name="hidLAST_REVERT">
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:BoundColumn DataField="Customer_id" HeaderText="" HeaderStyle-Width="0%" Visible="False"></asp:BoundColumn>
							<asp:BoundColumn DataField="Policy_id" HeaderText="" HeaderStyle-Width="0%" Visible="False"></asp:BoundColumn>
							<asp:BoundColumn DataField="Row_id" HeaderText="" HeaderStyle-Width="0%" Visible="False"></asp:BoundColumn>
							<asp:BoundColumn DataField="Process_id" HeaderText="" HeaderStyle-Width="0%" Visible="False"></asp:BoundColumn>
							<asp:BoundColumn DataField="POLICY_DISPLAY_VERSION" HeaderText="Policy Version" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
							<asp:BoundColumn DataField="NEW_Policy_Version_id" HeaderText="Policy Version" ItemStyle-CssClass="td" HeaderStyle-CssClass="td" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
							<asp:BoundColumn DataField="Process_NAME" HeaderText="Process" HeaderStyle-Width="18%"></asp:BoundColumn>
							<asp:BoundColumn DataField="completed_DATETIME" HeaderText="Committed Date" HeaderStyle-Width="18%"></asp:BoundColumn>
							<asp:BoundColumn DataField="Effective_Date" HeaderText="Process Effective Date" HeaderStyle-Width="10%"></asp:BoundColumn>
							<asp:BoundColumn DataField="Created_By" HeaderText="Created By" HeaderStyle-Width="10%"></asp:BoundColumn>
							<asp:BoundColumn DataField="Process_Status" HeaderText="Process Status" HeaderStyle-Width="15%"></asp:BoundColumn>
						</Columns>
					</asp:datagrid>
				</TD>
			</TR>
			<tr>
				<td><webcontrol:gridspacer id=Gridspacer3 runat="server"></webcontrol:gridspacer></td>
			</tr>
			<tr>
				<td class=headereffectcenter><asp:label id="capHeader" runat="server" ></asp:label></td><%--Revert back Commit	Process--%>
			</tr>
			<tr>
				<td id=tdGridHolder>
					<webcontrol:gridspacer id=Gridspacer2 runat="server"></webcontrol:gridspacer>
					<asp:placeholder id=GridHolder runat="server"></asp:placeholder>
				</td>
			</tr>
			<tr>
				<TD class=pageHeader colSpan=4><asp:Label ID="capMessage" runat="server"></asp:Label> </TD><%--Please note that all fields marked with * are mandatory--%>
			</tr>
			<tr>
				<td class=midcolorc align=right colSpan=4><asp:label id=lblMessage runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
			</tr>
			<tr id=trBody>
				<td>
					<TABLE width="100%" align=center border=0>
						<tr>
							<TD class=midcolora width="18%">
								<asp:label id=capREASON runat="server">Reason</asp:label><span class=mandatory>*</span>
							</TD>
							<TD class=midcolora width="32%">
								<asp:dropdownlist id=cmbREASON onfocus="SelectComboIndex('cmbREASON')" runat="server"></asp:dropdownlist>
								<BR>
								<asp:requiredfieldvalidator id=rfvREASON runat="server" ControlToValidate="cmbREASON"  Display="Dynamic"></asp:requiredfieldvalidator><%--ErrorMessage="Please select Reason."--%>
							</TD>
							<TD class=midcolora width="18%">
								<asp:label id=capOTHER_REASON runat="server">Reason desc(Max 250 characters)</asp:label><span class=mandatory id=spnOTHER_REASON>*</span>
							</TD>
							<TD class=midcolora width="32%">
								<asp:textbox id=txtOTHER_REASON onkeypress="MaxLength(this,250)" runat="server" Rows="10" Columns="50" TextMode="MultiLine" ></asp:textbox><BR>
								<!-- Added by Charles on 12-Aug-09 for Itrack 6251 -->
								<asp:customvalidator id="csvOTHER_REASON" Runat="server" ControlToValidate="txtOTHER_REASON" Display="Dynamic" ClientValidationFunction="txtCOMMENTS_VALIDATE"></asp:customvalidator><%--ErrorMessage="Please enter only 250 characters."--%>
								<asp:requiredfieldvalidator id=rfvOTHER_REASON runat="server" ControlToValidate="txtOTHER_REASON"  Display="Dynamic"></asp:requiredfieldvalidator><%--ErrorMessage="Please enter Reason desc."--%>
							</TD>
						</tr>
						<tr>
							<td class=midcolora colSpan="2">
								<cmsb:cmsbutton class=clsButton id=btnReset runat="server" causesValidation="false" Text="Reset"></cmsb:cmsbutton>
								<cmsb:cmsbutton class=clsButton id=btnRollBack runat="server" Text="Rollback" CausesValidation="false"></cmsb:cmsbutton>
								<cmsb:cmsbutton class=clsButton id=btnPolicyDetails runat="server" Text="Policy Details" CausesValidation="false"></cmsb:cmsbutton>
								<cmsb:cmsbutton class=clsButton id=btnBackToSearch runat="server" causesValidation="false" Text="Back To Search"></cmsb:cmsbutton>
								<cmsb:cmsbutton class=clsButton id=btnBackToCustomerAssistant runat="server" Text="Back To Customer Assistant" CausesValidation="false"></cmsb:cmsbutton>
							</td>
							<td class=midcolorr colSpan=2>
								<cmsb:cmsbutton class=clsButton id=btnGeneratePremiumNotice style="DISPLAY: none" runat="server" causesValidation="false" Text="Premium Notice"></cmsb:cmsbutton>
								<cmsb:cmsbutton class=clsButton id=btnComplete runat="server" Text="Commit" causesValidation="true" ></cmsb:cmsbutton>
								<cmsb:cmsbutton class="clsButton" id="btnCommitInProgress" runat="server" Text="Commit in Progress"></cmsb:cmsbutton>
								<cmsb:cmsbutton class=clsButton id=btnGet_Premium style="DISPLAY: none" runat="server" Text="Get Premium"></cmsb:cmsbutton>
								<cmsb:cmsbutton class=clsButton id=btnSave runat="server" Text="Save"></cmsb:cmsbutton><br>
								<cmsb:cmsbutton class=clsButton id=btnGenerateReinstateDecPage style="DISPLAY: none" runat="server" causesValidation="false" Text="Reinstatement Dec Page" Visible='false'></cmsb:cmsbutton>
								<cmsb:cmsbutton class=clsButton id=btnCommitToSpool style="DISPLAY: none" runat="server" Text="Commit To Spool"></cmsb:cmsbutton>
							</td>
						</tr>
					</TABLE>
				</td>
			</tr>
		</TABLE>
		<INPUT id=hidFormSaved type=hidden value=0 name=hidFormSaved runat="server"> 
		<INPUT id=hidIS_ACTIVE type=hidden value=0 name=hidIS_ACTIVE runat="server"> 
		<INPUT id=hidOldData type=hidden name=hidOldData runat="server"> 
		<INPUT id=hidROW_ID type=hidden value=0 name=hidROW_ID runat="server"> 
		<INPUT id=hidPOLICY_ID type=hidden name=hidPOLICY_ID runat="server"> 
		<INPUT id=hidPOLICY_VERSION_ID type=hidden name=hidPOLICY_VERSION_ID runat="server"> 
		<INPUT id=hidCUSTOMER_ID type=hidden name=hidCUSTOMER_ID runat="server"> 
		<INPUT id=hidPROCESS_ID type=hidden name=hidPROCESS_ID runat="server"> 
		<INPUT id=hidBASE_VERSION_ID type=hidden value=0 name=hidBASE_VERSION_ID runat="server"> 
		<INPUT id=hidNEW_POLICY_VERSION_ID type=hidden value=0 name=hidNEW_POLICY_VERSION_ID runat="server">
		<INPUT id=hidDisplayBody type=hidden name=hidDisplayBody runat="server"> 
		<INPUT id=hidUNDERWRITER type=hidden name=hidUNDERWRITER runat="server"> 
		<INPUT id=hidLOB_ID type=hidden name=hidLOB_ID runat="server"> 
		<INPUT id=hidSTATE_ID type=hidden name=hidSTATE_ID runat="server"> 
		<INPUT id=hidLAST_REVERT_ID type=hidden name=hidLAST_REVERT_ID runat="server"> 
				<INPUT id=hidAlertmsg type=hidden name=hidAlertmsg runat="server"> 
	</FORM>
</BODY>
</HTML>
