<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="LossReport.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.aspx.LossReport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>LossReport</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name=GENERATOR><LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<meta content=C# name=CODE_LANGUAGE>
		<meta content=JavaScript name=vs_defaultClientScript>
		<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema>
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<SCRIPT src="/cms/cmsweb/scripts/menubar.js"></SCRIPT>
		<LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		//Added By Sibin on 14 Nov 08 for Itrack Issue 5050
		var onUnloadFlag=false;
		function OkClicked()
		{
			window.close();
		}
		function CloseWindow()
		{
			//window.close(); 
		}
		function FetchLoss()
		{
			var i,index,delStr,nocheck;
			//document.getElementById('hidOperators').value='1';
			delStr = '';
			nocheck = 0;
			for(i=1;i<=iPs;i++)		
			{
				var ctrl=eval("document.getElementById('ck_" + i + "')");
				var rowTag=eval("document.getElementById('Row_" + i + "')");
				if(ctrl)
				{
					if(ctrl.checked)
					{
						var ColVal= new String;
						ColVal = rowTag.uniqueID;
						index = ColVal.indexOf("=",0);
						ColVal = ColVal.substring(index+1, ColVal.length);
						
						delStr=delStr + ColVal + "~";	
						nocheck = 1;				
					}
				}
					
			}
			if(nocheck == 0)
			{
				alert('Please select some drivers to fetch Loss Report');
				return false;
			}
			else
			{
				//document.getElementById('lblMessage').value='Fetching MVRs, please wait...';
				//document.getElementById('tblMVR').style.display='inline';			
				document.getElementById('hidDRIVER_ID').value=delStr;
				alert('Request Loss Report - Click Ok to Continue.');
				DisableButton(document.getElementById('btnFetchMVR'));
				document.getElementById('trPrior').style.display = 'inline';
				//Added By Sibin on 14 Nov 08 for Itrack Issue 5050
				onUnloadFlag=true;
				return false;
				
				//Added till here
			}
			
		}
		
		function init()
		{
			document.getElementById('trPrior').style.display = 'none';
		}
		
		
		//Added By Sibin on 14 Nov 08 for Itrack Issue 5050
		function refershParent()
		{
			if(onUnloadFlag)
			window.opener.location.reload(true);
		}
		</script>
  </HEAD>
	<body oncontextmenu = "return false;" MS_POSITIONING="GridLayout" onload="init();" onunload="refershParent()"><!--Added onunload="refershParent()" By Sibin on 14 Nov 08 for Itrack Issue 5050-->
		<form id="Form1" method="post" runat="server">
				<table class="tableWidth" border="0" cellpadding="0" cellspacing="0" align="center">
					<tr>
						<td>
							<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
								<tr>
									<td class="midcolora" align="left"><asp:label id="lblMessage" runat="server" CssClass="errmsg"></asp:label></td>
								</tr>
								<tr id="trPrior">
									<td>
										<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
										<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
									</td>
								</tr>
								<tr id="trDriver">
									<td id="tdGridHolderOpe"><webcontrol:gridspacer id="grdSpacerOpe" runat="server"></webcontrol:gridspacer><asp:placeholder id="GridHolderOpe" runat="server"></asp:placeholder></td>
									<td><webcontrol:gridspacer id="GridspacerOpe" runat="server"></webcontrol:gridspacer></td>
								</tr>
								<tr id="trMVRButtons">
									<td class="midcolorc" align="left"><INPUT runat="server" id="btnCloseDriv" class="clsButton" onclick="javascript:window.close();" type="button" value="Cancel Request">
									<INPUT runat="server" id="btnFetchMVR" class="clsButton" onclick="javascript:return FetchLoss();" type="button" value="Send Request">
									</td>
								</tr>
								<tr>
									<td>
										<webcontrol:Footer id="pageFooter" runat="server"></webcontrol:Footer>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			<input type="hidden" id="hidDRIVER_ID" name="hidDRIVER_ID" runat="server">z
		</form>
	</body>
</HTML>
