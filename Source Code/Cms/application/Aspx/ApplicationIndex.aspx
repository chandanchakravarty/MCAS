<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Page language="c#" Codebehind="ApplicationIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.Application.Aspx.ApplicationIndex" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Application Index</title>
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
		<SCRIPT src="../../cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="../../cmsweb/scripts/common.js"></script>
		<script src="../../cmsweb/scripts/form.js"></script>
		<script language="javascript">
	
		
		function onRowClicked(num,msDg )
		{
			rowNum=num;
			if(parseInt(num)==0)
				strXML="";
			populateXML(num,msDg);		
			
			if(imageClick!=1)
			{
			    top.botframe.location.href = "/cms/policies/aspx/policyTab.aspx?CalledFrom=" + document.getElementById('hidCalledFrom').value + "&" + locQueryStr;
			}
			else
				imageClick=0;
				
				
			//changeTab(0, 0);
		}
		function deleteApp()
		{
				var lStrGroup;
				var lStrBack;
				var lStrTab;
			 
				lStrBack=document.indexForm.hidTemplateID.value;				
				var locQueryStr = document.indexForm.hidlocQueryStr.value;				
				 
				var lStrGrdMsg="Please select an application";
				var szTemplateID = locQueryStr;			
				var arrszTemplateID;
				var value;
				var arrKeys;
			 	if (locQueryStr!=null && locQueryStr!=''&& locQueryStr!='-1')
				{
					if(szTemplateID!='' || szTemplateID !="-1" )
					{
						arrszTemplateID=szTemplateID.split("=");
						if(arrszTemplateID.length>0)
						{
							value=arrszTemplateID[1];							 
						}
						
						arrKeys=szTemplateID.split("&");
						if(arrKeys.length>0)
						{
							var arrKeyValues;
							
							arrKeyValues = arrKeys[0].split("=");
							document.getElementById('hidCustomerID').value= arrKeyValues[1];
							
							arrKeyValues = arrKeys[1].split("=");
							document.getElementById('hidAppID').value= arrKeyValues[1];
							
							arrKeyValues = arrKeys[2].split("=");
							document.getElementById('hidAppVersionID').value= arrKeyValues[1];
							
						}
					}
				}
				if(szTemplateID=="" ||szTemplateID=="-1")
				{
					alert(lStrGrdMsg);
					return;	
				}
				else
				{
					var cnfrmDelete =  confirm ("Are you sure you want to delete this application?");				 
					if (cnfrmDelete=true)
					{			
						lStrCalledFrom = document.getElementById('hidCalledFrom').value;						 
						self.location.href="ApplicationIndex.aspx?CalledFrom="+lStrCalledFrom+"&DELETEAPP=1&CustomerID="+document.getElementById('hidCustomerID').value+"&AppID="+document.getElementById('hidAppID').value+ "&AppVersionID="+document.getElementById('hidAppVersionID').value;
						 
					}
					else
					{
						document.getElementById('hidDeleteApp').value= '';
					}
					//window.open("PreviewQuestion.aspx?CalledFrom="+lStrCalledFrom+"&QID="+value+"&ScreenID="+lStrBack+"&TabID="+lStrTab,'','width=500,height=250,menubars=NO,scrollbars=yes,top=225,left=250,statusbar=NO,toolbars=NO');
				}
		}
		function Check()
		{
		var temp = '<%=strCALLEDFROM%>';
		if(temp.toUpperCase()!="INCLT")
		{
		findMouseIn();
		}
		//else
		
		}
		</script>
	</HEAD>
	<body oncontextmenu="return false;" topmargin=0 leftmargin=0 onload="setfirstTime();top.topframe.main1.activeMenuBar='1,2';top.topframe.main1.mousein = false;Check();"
		MS_POSITIONING="GridLayout">		
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
			<table class="<%=strCssClass%>" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
							<form id="indexForm" method="post" runat="server">
								<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
								<p align="center"><asp:Label ID="capMessage" Runat="server" Visible="False" CssClass="errmsg"></asp:Label></p>
								<tr>
									<TD id="tdClientTop" class="pageHeader" colSpan="4">
										<webcontrol:ClientTop id="cltClientTop" runat="server" width="100%"></webcontrol:ClientTop>
									</TD>
								</tr>
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
											<tr id="tabCtlRow">
												<td>
													<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="GeneralInformation.aspx?" TabTitles="General Information"
														TabLength="150"></webcontrol:Tab>
												</td>
											</tr>
											<tr>
												<td>
													<table class="tableeffect" width="100%" cellpadding="0" cellspacing="0" border="0">
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
						<INPUT id="hidDeleteApp" type="hidden" value="0" name="hidDeleteApp" runat="server">
						<INPUT id="hidCustomerID" type="hidden" value="0" name="hidCustomerID" runat="server">
						<INPUT id="hidAppID" type="hidden" value="0" name="hidAppID" runat="server"> <INPUT id="hidAppVersionID" type="hidden" value="0" name="hidAppVersionID" runat="server">
						<INPUT id="hidCalledFrom" type="hidden" value="0" name="hidCalledFrom" runat="server">
						</FORM>
					</td>
				</tr>
			</table>
		</div>
		<DIV></DIV>
	</body>
</HTML>
