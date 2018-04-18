<%@ Page language="c#" Codebehind="ApplicantInsuedIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.Client.Aspx.ApplicantInsuedIndex" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>EmailIndex</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<STYLE>
			.hide { OVERFLOW: hidden; TOP: 5px }
			.show { OVERFLOW: hidden; TOP: 5px }
			#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<SCRIPT src="../../cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="../../cmsweb/scripts/common.js"></script>
		<script src="../../cmsweb/scripts/form.js"></script>
		<script language="javascript">
					
		function onRowClicked(num,msDg )
		{
			document.getElementById('capMessage').innerHTML='';
			document.getElementById('capMessage').style.display='none';
			if(parseInt(num)==0)
				strXML="";
			populateXML(num,msDg);	
				changeTab(0, 0);
			
		}
		function findMouseIn()
			{
				if(!top.topframe.main1.mousein)
				{
					//createActiveMenu();
					top.topframe.main1.mousein = true;
				}
				setTimeout('findMouseIn()',5000);
			}
		function BackToCustomer()
			{
			//alert('jdf');
			top.botframe.location.href="CustomerManagerIndex.aspx";
			return false;
			}
			function BackToApplicationPolicy()
			{
			
			top.botframe.location.href=document.getElementById("hidReturnURL").value;
			return false;
			}							
			
		function addRecord1()
		{
			if (document.getElementById('hidIsActive').value !='N')
			{
				ShowTab();
				if(document.getElementById('hidlocQueryStr'))
					document.getElementById('hidlocQueryStr').value="Add";

				if(document.getElementById('hidlocQueryStr'))
					{
						document.getElementById('hidlocQueryStr').value = '';
					}
		
				addNew = true; 
				//Setting the selected record xml to blank
				strSelectedRecordXML = "";
				show=2;	
				onRowClicked(0,msDg);				
				if (window.SetTabs) 
				{
					window.SetTabs(0,true);
				}
			}
			else
			{
				document.getElementById('capMessage').style.display='inline'
				document.getElementById('capMessage').innerHTML='Co-Applicant for Inactive customer can not be created.';
				
				
			}
		}
		function AddNewApplicantForApplication()
		{
			if((document.getElementById('hidBackToApplication').value=="1" || document.getElementById('hidBackToApplication').value=="2") && (parent.CALLED_FROM_APPLICATION!=undefined && parent.CALLED_FROM_APPLICATION==1))
				setTimeout('addRecord1()',1000);
		}
		</script>
	</HEAD>
	<body  leftmargin="0" topmargin="0" onload="setfirstTime();AddNewApplicantForApplication();" MS_POSITIONING="GridLayout">
		
		<div class="pageContent" id="bodyHeight">
			<FORM id="indexForm" method="post" runat="server">
				<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" align="center" border="0">
				
					<!-- <input id="hide" type="hidden" name="ConVar"><span id="singleRec"></span> -->
					<tr>
						<TD class="pageHeader" id="tdClientTop" colSpan="4"><webcontrol:clienttop id="cltClientTop" runat="server" width="100%"></webcontrol:clienttop></TD>
					</tr>
					<tr>
						<td><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer>
						<asp:placeholder id="GridHolder" runat="server"></asp:placeholder></td>
					</tr>
					<tr>
						<td><webcontrol:gridspacer id="Gridspacer1" runat="server"></webcontrol:gridspacer></td>
					</tr>
						<tr>
						<td align="center"><asp:label id="capMessage" CssClass="errmsg" Runat="server"></asp:label></td>
					</tr>
					<tr>
						<td>
							<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" align="center" border="0">
								<tr id="tabCtlRow">
									<td><webcontrol:tab id="TabCtl" runat="server" TabLength="150" TabTitles="Co-Applicant Details" TabURLs="AddApplicantInsued.aspx?"></webcontrol:tab></td>
								</tr>
								<tr>
									<td>
										<table class="tableeffect" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<tr>
												<td><iframe class="iframsHeightLong" id="tabLayer" src="" frameBorder="0" width="100%" scrolling="no"
														runat="server"></iframe>
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<asp:label id="lblTemplate" Visible="false" Runat="server"></asp:label></table>
						</td>
					</tr>
					<tr>
						<td><webcontrol:footer id="pageFooter" runat="server"></webcontrol:footer></td>
					</tr>
				</table>
				<input type="hidden" name="hidTemplateID" id="Hidden1" runat="server"> <input type="hidden" name="hidRowID" id="Hidden2" runat="server">
				<input id="hidlocQueryStr" type="hidden" name="hidlocQueryStr" runat="server"> <input type="hidden" name="hidMode" id="Hidden3" runat="server">
				<input type="hidden" name="hidIsActive" id="hidIsActive" runat="server">
				<input type="hidden" name="hidBackToApplication" id="hidBackToApplication" runat="server"> 				
				<input type="hidden" name="hidReturnURL" id="hidReturnURL" runat="server">
				
			</FORM>
		</div>
		<DIV></DIV>
	</body>
</HTML>
