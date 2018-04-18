<% /****************************************************************************************
   Author	: nidhi
   Creation Date : 1/06/2005
   Last Updated  :  
   Reviewed By	 : 
   Purpose	: This page diplays the order of tab.
   Comments	: 
   ------------------------------------------------------------------------------------- 
   History	Date	     Modified By		Description   	    
   ------------------------------------------------------------------------------------- 
   *****************************************************************************************/ %>
<%@ Page language="c#" Codebehind="ChangeTabOrder.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.UserDefined.ChangeTabOrder" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>
			BRICS-Change Tab Order
		</title>
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet">
		<script>
		 
		function GoBack()
		{
			var lStrGroup;
			var lStrBack;
			lStrBack="<%=gStrScreenID%>";												
			self.location.href="SubmitScreen.aspx";
			
		}
		function test()
		{			
			var strerrmsg="<%=gstrerrmsg%>";
			var strerrmsgblank="<%=gstrerrmsgblank%>";
			
			var intSubmit=0;
			var intValidateBlank = 0 ;			
			var strcounter = "<%=gIntCountCounter%>";
			var strIds = "<%=glstrCombineID%>";
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
					if(eval("document.TabSequenceChange.cboGroup_" + arrCommaValue[j]))					
					{						
						strControlValue = eval("document.TabSequenceChange.cboGroup_" + arrCommaValue[j]).value;						
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
							counter = parseInt(counter) + 1;
						}
						if(counter > 1)
						{
							alert(strerrmsg);
					
							eval("document.TabSequenceChange.cboGroup_" + arrCommaValue[j]).focus();							
							intSubmit=1;
							return;
							break;
						}
					}
				}
			}
			if(intSubmit==0 && intValidateBlank ==0)
			{
				document.TabSequenceChange.submit();
			}
		}
 			</script>
	</HEAD>
	<body oncontextmenu = "return false;" MS_POSITIONING="GridLayout" onload="top.topframe.main1.mousein =false;ApplyColor();showScroll();">
		<!-- To add bottom menu -->
		<webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
	<div id="bodyHeight" class="pageContent">
		<center>			
			<form id="TabSequenceChange" method="post" runat="server">
				<table align="center" cellpadding="1" cellspacing="1" width="90%" border="0">
					<tr vAlign="middle">
						<td colSpan="4" class="tableeffect" align="left">
							<asp:Label Runat="server" ID="lblNavLevelTwo"></asp:Label>
						</td>
					</tr>
					<tr vAlign="middle">
						<td colSpan="4" class="midcolora" align="center">
							<asp:Label Runat="server" ID="lblScreenName"></asp:Label>
						</td>
					</tr>
					<asp:Label CssClass="errmsg" ID="lblmsg" Runat="server"></asp:Label>
					<asp:Table id="Table1" Width="90%" runat="server" BorderWidth="0">
						<asp:TableRow>
							<asp:TableCell></asp:TableCell>
						</asp:TableRow>
					</asp:Table>
					<asp:Label ID="lblGropuID" Runat="server" Visible="False"></asp:Label>
					<asp:Label ID="lblHidLabelID" Runat="server" Visible="False"></asp:Label>
					<asp:Label ID="lblTabID" Runat="server" Visible="False"></asp:Label>
				</table>
				<input  type="button" ID="btnCancel" class="clsButton"  onclick="javascript:GoBack();" Runat="server" NAME="btnCancel">
				<input  type="button" name="btnSave" id="btnSave" onclick="javascript:test()"  class="clsbutton" runat="server">
			</form>
		</center>
		</div> 
	</body>
</HTML>
