<%@ Page language="c#" Codebehind="VoidClaimsCheck.aspx.cs" AutoEventWireup="false" Inherits="Claims.Aspx.VoidClaimsCheck" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Claims Index</title>
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
			//alert(num+'\n'+msDg)			
		}
		
		function VoidClaimCheck()
		{
			var SelectedIDs="";
			for(i=0; i<document.forms[0].elements.length; i++)
			{
				if (document.forms[0].elements[i].type == "checkbox" && document.forms[0].elements[i].checked == true)
				{
					SelectedIDs = SelectedIDs + "," + document.forms[0].elements[i].id
				}
			}
			if(SelectedIDs == "" || SelectedIDs == ",")
			{
				alert("Please select atleast one check for voiding");
				return false;
			}
			else
			{
			
					//Added by praveen on 21 March 2008
					alert('Please do not forget to complete a required activity for the linked claim.');
					//End
					//Added for Itrack Issue 7206 on 20 April 2010 -- To stop multiple times clicking of Void Check button on a selected void check and hence preventing dupicate entries in Account Posting details table.
					for(i=0; i<document.forms[0].elements.length; i++)
					{
						if (document.forms[0].elements[i].type == "button" && document.forms[0].elements[i].value == "Void Check")
						{
							document.forms[0].elements[i].setAttribute("disabled",true);
						}
					}
					VoidChecks();	
					__doPostBack('Void','0')				 
								
			}	
		}
		
	
	
	/*function PutViolations(Result)
		{
			var strXML;
			if(Result.error)
			{        
				var xfaultcode   = Result.errorDetail.code;
				var xfaultstring = Result.errorDetail.string;
				var xfaultsoap   = Result.errorDetail.raw;        				
			}
			else	
			{	
				strXML= Result.value;		
				//alert(strXML);
			}
		}*/
	
	
		/*Menus should still be enabled and in the previous state
		function SetMenu()
		{
			//Disable party,reserve and activities menu at this page
			top.topframe.disableMenu('2,2,1');
			top.topframe.disableMenu('2,2,2');
			top.topframe.disableMenu('2,2,3');
			//Disable customer and policy menus at this page
			top.topframe.disableMenu('2,2,4');
			top.topframe.disableMenu('2,2,5');
			top.topframe.disableMenu('2,2,6');
			top.topframe.disableMenu('2,2,7');
		}*/
		
		
		
		
		</script>
	</HEAD>
	<body oncontextmenu="return false;" leftmargin="0" topmargin="0" onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();"
		MS_POSITIONING="GridLayout">
		
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
									<td id=dd>
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
										<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="AddClaimsNotification.aspx?" TabTitles="Claims Notification"
											TabLength="140"></webcontrol:Tab>
									</td>
								</tr>
							
							</table>
							<input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID">
							<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"> <input type="hidden" name="hidMode">
							<input id="hide" type="hidden" name="ConVar"> 
							
							<input id="hidCheckedRowIDs" type="hidden" runat=server NAME="hidCheckedRowIDs"> 
							
							<span id="singleRec"></span>							
						</td>
					</tr>
				</table>
			</form>
		</div>
		<DIV></DIV>
	</body>
</HTML>

