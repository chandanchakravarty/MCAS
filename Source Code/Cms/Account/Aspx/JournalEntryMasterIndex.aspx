<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Page language="c#" Codebehind="JournalEntryMasterIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.JournalEntryMasterIndex" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BRICS</title>
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
	
		function onRowClicked(num,msDg )
		{
			//saving the passed param in top frame variable
			parent.ClickRowNo = num;
			parent.ClickRowMsg = msDg;
			
			
			rowNum = num;
			if(parseInt(num)==0)
				strXML="";
			populateXML(num,msDg);		
			changeTab(0, 0);
			
			if (document.getElementById("capMessage"))
				document.getElementById("capMessage").style.display = "none";
				
			if (document.getElementById("hidlocQueryStr").value != "")
			{
				//Extracting the last selected journal id from hidlocQueryStr
				var id = document.getElementById("hidlocQueryStr").value.toString().split('=')[1];
				parent.JournalId = id;
			}
			
		}
		
		
		//Called when user clicks on preview button
		function OnPreview(num,msDg)
		{
			if (document.getElementById("hidlocQueryStr").value == "")
			{
				alert("Please select any Journal Entry from the grid, which you want to preview.");
				return;
			}
			
			var id = document.getElementById("hidlocQueryStr").value.toString().split('=')[1];
			window.open("JournalEntryPreview.aspx?JOURNAL_ID=" + id);
		}
		
		function ShowDetails()
		{
			msDg = parent.ClickRowMsg;
			//Added by Asfa (07-july-2008) - iTrack issue #4066 + Detail page should open in case of Adding new record too.
			if(parent.ClickRowNo == 0)
			{
				if(parent.JournalId != "")
				{
					var Journal_Id = window.parent.self.document.forms[0].hidSELECTED_JOURNAL_ID.value ;
					var Cookie;
					Cookie=document.cookie;
					var valuesSplit;
					if(Cookie.indexOf("GridClickRowNumber=") != -1)
					{
						valuesSplit = Cookie.split("GridClickRowNumber=")
						var cookie_Row_No;
						var temp=valuesSplit[1];
						cookie_Row_No = temp.split(';')[0]
						if(parent.ClickRowNo == 0)
							parent.ClickRowNo = cookie_Row_No;
						RefreshWebgrid(Journal_Id);
						var str = document.getElementById('Row_'+  parent.ClickRowNo);
						highlightrowClick(str);
						return;
					}
				}
			}
			else
			{
				var str = document.getElementById('Row_'+  parent.ClickRowNo);
				highlightrowClick(str);
			}
		}
		
		function LoadDetail()
		{
			if ( typeof(parent.ClickRowNo) != "undefined")
			{
				if (parent.JournalId != "")
				{
					var Journal_Id = window.parent.self.document.forms[0].hidSELECTED_JOURNAL_ID.value ;
					var Cookie;
					Cookie=document.cookie;
					var valuesSplit;
					valuesSplit = Cookie.split("GridClickRowNumber=")
					var cookie_Row_No;
					var temp=valuesSplit[1];
					if(typeof(temp) != "undefined")
					{
						cookie_Row_No = temp.split(';')[0]
						/*Commented by Asfa(09-June-2008) - iTrack issue #4066
						parent.ClickRowNo = cookie_Row_No;
						*/
						RefreshWebgrid(Journal_Id);
						setTimeout("ShowDetails()",2000);
						return;
					}
					/*RefreshWebgrid(parent.JournalId);
					setTimeout("ShowDetails()",2000);
					return;*/
				}
				//Added by Asfa(26-June-2008) - iTrack issue #4066
				RefreshWebgrid(Journal_Id);
				ShowDetails();
			}
		}		
		
		
		function LoadDetailsScreen()
		{	
			setfirstTime();	
			if ( typeof(parent.ClickRowNo) != "undefined")
			{
				//If any record exists in top frame, then loading that record
				setTimeout("LoadDetail()",2000);
			}
			
		}
		
		</script>
	</HEAD>
	<body oncontextmenu="return false;" leftmargin="0" rightmargin="0" onload="setfirstTime();top.topframe.main1.mousein = false;LoadDetailsScreen();"
		MS_POSITIONING="GridLayout">
		
		<div id="bodyHeight" class="pageContent">
			<table class="tableWidthHeader" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<table class="tableWidthHeader" width="100%" cellSpacing="0" cellPadding="0" border="0"
							align="center">
							<form id="indexForm" method="post" runat="server">
								<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
								<p align="center"><asp:Label ID="capMessage" Runat="server" Visible="False" CssClass="errmsg"></asp:Label></p>
								<tr>
									<td id="tdGridHolder">
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
													<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="AddJournalEntryMaster.aspx?" TabTitles="Journal Entry"
														TabLength="150"></webcontrol:Tab>
												</td>
											</tr>
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
						<input type="hidden" runat="server" name="hidlocQueryStr" id="hidlocQueryStr"> <input runat="server" type="hidden" name="hidMode" id="hidMode">
						<input runat="server" type="hidden" name="hidGridRowClickNumber" id="hidGridRowClickNumber">
						<input runat="server" type="hidden" name="hidGridRowClickMsg" id="hidGridRowClickMsg">
						</FORM>
					</td>
				</tr>
			</table>
		</div>
		<DIV></DIV>
	</body>
</HTML>
