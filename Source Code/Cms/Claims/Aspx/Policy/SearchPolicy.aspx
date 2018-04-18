<%@ Page language="c#" Codebehind="SearchPolicy.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.Policy.SearchPolicy" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Policy Index</title>
		<META content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<META content="C#" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<!--<LINK href="~/cmsweb/css/menu.css" type="text/css" rel="stylesheet">-->
		<STYLE>
		.hide { OVERFLOW: hidden; TOP: 5px }
		.show { OVERFLOW: hidden; TOP: 5px }
		#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
			<script language="vbscript">		
			Function DisplayMessage(msg)				
				DisplayMessage = msgbox(msg,64,"CMS - Search Policy")
			End function
		</script>
		<script language="javascript">
		
		function findMouseIn()
		{
			if(!top.topframe.main1.mousein)
			{
				//createActiveMenu();
				top.topframe.main1.mousein = true;
			}
			setTimeout('findMouseIn()',5000);
		}
		function onRowClicked(num,msDg )
		{ 
		
		    //Itrack 5985 : kasana
			rowNum=num;
			if(parseInt(num)==0)
				strXML="";			
			populateXML(num,msDg);	
			
			var result = SearchPolicy.ProcessInProgress(strXML);
			
			var policy_status = AjaxCallFunction_CallBack(result);
			
			if(policy_status == '1') //In Process In Suspense
			{
				if(document.getElementById("Row_"+ num).style.color=="#0066cc")
					document.getElementById("Row_"+ num).style.color="red";  	
				DisplayMessage("Can't add claim as policy is in Suspended Process or Process in Progress.");
			}
			else if (policy_status == '2') //REvert back Process
			{
				if(document.getElementById("Row_"+ num).style.color=="#0066cc")
					document.getElementById("Row_"+ num).style.color="red";  	
				DisplayMessage("Can't add claim as policy has been Reverted back.");				
			}
			else if (policy_status == '3') //AS400 back Process
			{
				if(document.getElementById("Row_"+ num).style.color=="#0066cc")
					document.getElementById("Row_"+ num).style.color="red";  	
				DisplayMessage("Can't add claim as policy is from Conversion.");				
			}
			else
			{
				if ( !(event.srcElement.outerHTML.indexOf("OPTION") != -1 || event.srcElement.outerHTML.indexOf("button_Click") != -1))
				{
					if(document.getElementById("hidBackToMatchPolicy").value=="1")
						top.botframe.location.href = "/cms/claims/aspx/MatchClaims.aspx?" + locQueryStr + "&BackFromPolicy=1&DUMMY_POLICY_ID=" + document.getElementById("hidDUMMY_POLICY_ID").value + "&DUMMY_CLAIM_NUMBER=" + document.getElementById("hidDUMMY_CLAIM_NUMBER").value + "&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&";
					else
						top.botframe.location.href = "/cms/claims/aspx/ClaimsNotificationIndex.aspx?" + locQueryStr + "&" ;				
				}
			}
			
			/*if((AjaxCallFunction_CallBack(result))!='1')
			{
				if ( !(event.srcElement.outerHTML.indexOf("OPTION") != -1 || event.srcElement.outerHTML.indexOf("button_Click") != -1))
				{
					if(document.getElementById("hidBackToMatchPolicy").value=="1")
						top.botframe.location.href = "/cms/claims/aspx/MatchClaims.aspx?" + locQueryStr + "&BackFromPolicy=1&DUMMY_POLICY_ID=" + document.getElementById("hidDUMMY_POLICY_ID").value + "&DUMMY_CLAIM_NUMBER=" + document.getElementById("hidDUMMY_CLAIM_NUMBER").value + "&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&";
					else
						top.botframe.location.href = "/cms/claims/aspx/ClaimsNotificationIndex.aspx?" + locQueryStr + "&" ;				
				}
			}
			else
			{
				if(document.getElementById("Row_"+ num).style.color=="#0066cc")
					document.getElementById("Row_"+ num).style.color="red";  	
				alert("Can't add claim as policy is in Suspended Process or Process in Progress.");
			}*/
			
			//changeTab(0,0);
		}
		
		
		function AjaxCallFunction_CallBack(res) 
		{
			if(!res.error)
			{
				if (res.value!="" && res.value!=null ) 
				{
					return res.value;			  					
				}
			}	
		}
		
		function addNewClaim()
		{
			//document.location.href = "/cms/claims/aspx/ClaimsNotificationIndex.aspx?NEW_RECORD=1&";
			//The QueryString parameter New_Record indicates that a new record is being added
			//document.location.href = "/cms/claims/aspx/ClaimsTab.aspx?&NEW_RECORD=1&" ;	
			document.location.href = "/cms/claims/aspx/Policy/DummyPolicyPopUp.aspx?&" ;	
			
		}		
		function SetMenu()
		{
			//disable App/Policy Menu added By Pravesh on 3 dec 08 as sesion values are being set to blank on this page
			top.topframe.disableMenus("1","ALL");
			top.topframe.enableMenu("1,0");
			top.topframe.enableMenu("1,1");
			top.topframe.disableMenu("1,1,1");
			top.topframe.disableMenu("1,1,2");
			// end here Added by Pravesh
			//Disable customer and policy menus at this page
			top.topframe.disableMenu('2,2,4');
			top.topframe.disableMenu('2,2,5');
			top.topframe.disableMenu('2,2,6');
			top.topframe.disableMenu('2,2,7');
			//Enable/Disable claim id at this page based on value of claim id
			if(document.getElementById("hidCLAIM_ID").value!="" && document.getElementById("hidCLAIM_ID").value!="0")
			{
				//Enable party,reserve and activities menu at this page
				top.topframe.enableMenu('2,2,1');
				top.topframe.enableMenu('2,2,2');
				top.topframe.enableMenu('2,2,3');
			}
			else
			{
				//Disable party,reserve and activities menu at this page
				top.topframe.disableMenu('2,2,1');				
				top.topframe.disableMenu('2,2,2');				
				top.topframe.disableMenu('2,2,3');				
			}
			if(document.getElementById("hidDUMMY_POLICY_ID")!=null && document.getElementById("hidDUMMY_POLICY_ID").value!="" && document.getElementById("hidDUMMY_POLICY_ID").value!="0")
			{
				top.topframe.disableMenu('2,2,1');				
				top.topframe.disableMenu('2,2,2');				
				top.topframe.disableMenu('2,2,3');				
			}
			else if(document.getElementById("hidCLAIM_ID").value!="" && document.getElementById("hidCLAIM_ID").value!="0")
			{
				top.topframe.enableMenu('2,2,1');
				top.topframe.enableMenu('2,2,2');
				top.topframe.enableMenu('2,2,3');
			}
		}
		
		</script>
	</HEAD>
	<body oncontextmenu="return true;" leftmargin="0" topmargin="0" onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();SetMenu();"	MS_POSITIONING="GridLayout">
		
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
			<form id="indexForm" method="post" runat="server">
				<table class="tableWidth" border="0" cellpadding="0" cellspacing="0">
					<tr>
						<td align="center" colSpan="4"><asp:label id="lblError" runat="server" cssClass="errMsg"></asp:label></td>
					</tr>
					<tr>
						<td>
							<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
								<tr>
									<td>
										<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
										<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
									</td>
								</tr>
								<tr>
									<td><webcontrol:GridSpacer id="Gridspacer" runat="server"></webcontrol:GridSpacer></td>
								</tr>
								<tr>
									<td>
										<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
											<tr>
												<td>
													<table class="tableeffect" cellpadding="0" cellspacing="0" border="0">
														<tr>
															<td>
																<iframe class="iframsHeightLong" id="tabLayer" runat="server" src="" scrolling="no" frameborder="0"
																	width="100%"></iframe>
															</td>
														</tr>
													</table>
												</td>
											</tr>
											<asp:Label ID="lblTemplate" Runat="server" Visible="false"></asp:Label>
										</table>
									</td>
								</tr>
								<tr>
									<td>
										<webcontrol:Footer id="pageFooter" runat="server"></webcontrol:Footer>
									</td>
								</tr>
							</table>
							<input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID">
							<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"> <input type="hidden" name="hidMode">
							<input type="hidden" name="hidBackToMatchPolicy" id="hidBackToMatchPolicy" runat="server">
							<input type="hidden" name="hidDUMMY_POLICY_ID" id="hidDUMMY_POLICY_ID" runat="server">
							<input type="hidden" name="hidDUMMY_CLAIM_NUMBER" id="hidDUMMY_CLAIM_NUMBER" runat="server">
							<input type="hidden" name="hidCLAIM_ID" id="hidCLAIM_ID" runat="server">
							<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
						</td>
					</tr>
				</table>
			</form>
		</div>
		<DIV></DIV>
	</body>
</HTML>


