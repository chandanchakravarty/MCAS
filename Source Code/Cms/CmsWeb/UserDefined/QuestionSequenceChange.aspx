<%@ Page language="c#" Codebehind="QuestionSequenceChange.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.UserDefined.QuestionSequenceChange" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BRICS-Change Sequence </title>
		<% /****************************************************************************************
   Author	: nidhi
   Creation Date :  1/06/2005
   Last Updated  :  
   Reviewed By	 : 
   Purpose	: This page diplays the order of question in a questions/group. 
   Comments	: 
   ------------------------------------------------------------------------------------- 
   History	Date	     Modified By		Description   	    
   ------------------------------------------------------------------------------------- 
   *****************************************************************************************/ %>
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
			<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
			<script src="/cms/cmsweb/scripts/common.js"></script>
			<script src="/cms/cmsweb/scripts/form.js"></script>
			<LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet">
				<script language="javascript">
 
		function GoBack()
			{
				var lStrGroup;
				var lStrBack;
				var lStrTab;
				var CalledFrom;
				CalledFrom="<%=gStrCalledFrom%>";
				lStrBack="<%=gStrScreenID%>";				
				lStrGroupID="<%=gStrGroupID%>";				
				lStrTab="<%=gintTabID%>"				
				if(lStrTab=="0" || lStrBack=="0")
				{					
					self.location.href="ReferalQuestion.aspx?CalledFrom="+CalledFrom;
				}
				else if(lStrGroupID!="" && lStrTab!="0")
				{					
					self.location.href="SubmitGroups.aspx?CalledFrom="+CalledFrom+"&TabID="+lStrTab+"&ScreenID="+lStrBack;
				}
				else
				{
					self.location.href="SubmitTab.aspx?CalledFrom="+CalledFrom+"&ScreenID="+lStrBack;
				}				 
			}
			
			
		function test()
		{
			var strcounter="<%=glstrgroupCounter%>";
			var strIds = "<%=glstrCombineID%>";
			var strerrmsg="<%=gstrerrmsg%>";
			var strerrmsgblank="<%=gstrerrmsgblank%>";
			var intSubmit=0;
			var intValidateBlank = 0 ;			
			for(i=1;i<=strcounter;i++)
			{	
				var counter=0;				
				arrCommaValue = strIds.split("_");
				for ( j = 0;j<= arrCommaValue.length-1; j++)				
				{	
					
					if(intSubmit==1)
					{						
						intValidateBlank  = intSubmit;
						intSubmit = 0;
						return;
							break;
					}
					if(eval("document.QuestionSequenceChange.cboGroup" + arrCommaValue[j]))					
					{						
						strControlValue = eval("document.QuestionSequenceChange.cboGroup" + arrCommaValue[j]).value;
					
						/* The following lines are added to prevent the user to save the sequence with blank value */
						if(strControlValue=="")
						{
						intSubmit=1;						
						alert(strerrmsgblank);	
						return;
						break;
						}
						// end of that snippet
												
						if(i==strControlValue)
						{
							counter = parseInt(counter) + 1
						}
						if(counter > 1)
						{
							alert(strerrmsg);
					
							eval("document.QuestionSequenceChange.cboGroup" + arrCommaValue[j]).focus();							
							intSubmit=1;
							return;
							break;
						}
					}
				}
			}
			if(intSubmit==0 && intValidateBlank ==0)
			{
				document.QuestionSequenceChange.submit();
			}
		}
 
				</script>
	</HEAD>
	<body oncontextmenu = "return false;" MS_POSITIONING="GridLayout" onload="top.topframe.main1.mousein =false;showScroll();ApplyColor();">
			<!-- To add bottom menu -->
		<webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
		<TABLE height="157" cellSpacing="0" cellPadding="0" width="979" border="0" ms_2d_layout="TRUE">
			<TR vAlign="top">
				<TD width="979" height="157">
					<center>
						<form id="QuestionSequenceChange" method="post" runat="server">
							<TABLE height="120" cellSpacing="0" cellPadding="0" width="929" border="0" ms_2d_layout="TRUE">
								<TR>
									<TD width="48" height="0"></TD>
									<TD width="423" height="0"></TD>
									<TD width="19" height="0"></TD>
									<TD width="439" height="0"></TD>
								</TR>
								<TR vAlign="top">
									<TD height="95"></TD>
									<TD colSpan="3">
										<table align="center" cellpadding="1" cellspacing="1" width="880" border="0" height="95">
											<tr vAlign="middle">
												<td colSpan="4" class="midcolora" align="center">
													<asp:Label Runat="server" ID="lblScreenName"></asp:Label>
												</td>
											</tr>
											<tr vAlign="middle">
												<td colSpan="4" class="midcolora" align="center">
													<asp:Label Runat="server" ID="lblTabName"></asp:Label>
												</td>
											</tr>
											<asp:Panel ID="pnlGroup" Runat="server" Visible="False">
												<TR vAlign="middle">
													<TD class="midcolora" align="center" colSpan="4">
														<asp:Label id="lblGroupName" Runat="server"></asp:Label></TD>
												</TR>
											</asp:Panel>
											<tr>
												<td colspan="4">
													<hr>
												</td>
											</tr>
											<asp:Label CssClass="errmsg" ID="lblmsg" Runat="server"></asp:Label>
											<asp:Table id="Table1" Width="90%" runat="server">
												<asp:TableRow>
													<asp:TableCell></asp:TableCell>
												</asp:TableRow>
											</asp:Table>
											<asp:Label ID="lblGropuID" Runat="server" Visible="False"></asp:Label>
											<asp:Label ID="lblHidLabelID" Runat="server" Visible="False"></asp:Label>
											<asp:Label ID="lblTabID" Runat="server" Visible="False"></asp:Label>
										</table>
									</TD>
								</TR>
								<TR vAlign="top">
									<TD colSpan="2" height="25"></TD>
									<TD>
										<input type="button" name="btnSave" id="btnSave" onclick="javascript:test()" class="clsbutton"
											runat="server"></TD>
									<TD>
										<input type="button" ID="btnCancel" class="clsButton" onclick="javascript:GoBack();" Runat="server"
											NAME="btnCancel"></TD>
								</TR>
							</TABLE>
						</form>
					</center>
				</TD>
			</TR>
		</TABLE></div> 
	</body>
</HTML>
