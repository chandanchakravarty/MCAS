<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ProcessLogTop.ascx.cs" Inherits="Cms.CmsWeb.webcontrols.ProcessLogTop" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script language="javascript">
	var refWindow;
	var ProcessTop = {};
	ProcessTop.imageIcon		=	new Array();
	ProcessTop.imageIcon[0]	=	new Image();
	ProcessTop.imageIcon[1]	=	new Image();
	
	ProcessTop.imageIcon[0].src	=	"/cms/cmsweb/Images/plus2.gif";
	ProcessTop.imageIcon[1].src	=	"/cms/cmsweb/Images/minus2.gif";
    
    function showHideProcessTop()
	{
		/*
		for(var j=7; j > 4; j--)
		{
			if(document.getElementById('PR' + j).style.display	==	'none')
			{
				document.getElementById('PR' + j).style.display	=	'inline';
			}
			else
			{
				document.getElementById('PR' + j).style.display	=	'none';
			}
		}*/
		if(document.getElementById('ProcessTopLink').src == ProcessTop.imageIcon[0].src)
		{
			document.getElementById('ProcessTopLink').src	=	ProcessTop.imageIcon[1].src;
		}
		else
		{
			document.getElementById('ProcessTopLink').src	=	ProcessTop.imageIcon[0].src;
		}
	}

</script>
<table class="headereffectCenter" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td width="95%">
		Process Information
		<td align="right" width="5%"><IMG id="ProcessTopLink" style="CURSOR: hand" onclick="javascript:showHideProcessTop();"
				src="ProcessTop.imageIcon[0].src"></td>
	</tr>
</table>
<TABLE class="tableeffectTopHeader" cellSpacing="1" cellPadding="0" width="100%" border="0">
	<TR id="PR1">
		<TD class="midcolora" vAlign="top" align="left" width="13%"><asp:label id="capName" Runat="server" Font-Bold="True"></asp:label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%"><asp:label id="capFullName" Runat="server"></asp:label></TD>
		<TD class="midcolora" vAlign="top" align="left" width="13%"><asp:label id="capPROCESS_STATUS" Runat="server" Font-Bold="True"></asp:label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%"><asp:label id="capPROCESS_STATUS_DESC" Runat="server"></asp:label></TD>
		<TD class="midcolora" vAlign="top" align="left" width="13%"><asp:label id="capCREATEDBY" Runat="server" Font-Bold="True"></asp:label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%"><asp:label id="capCREATED_BY" Runat="server"></asp:label></TD>
	</TR>
	<tr id="PR2">
		<TD class="midcolora" vAlign="top" align="left" width="13%"><asp:label id="capCREATEDDATE" Runat="server" Font-Bold="True"></asp:label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%"><asp:label id="capCREATED_DATETIME" Runat="server"></asp:label></TD>
		<TD class="midcolora" vAlign="top" align="left" width="13%"><asp:label id="capCOMPLETEDBY" Runat="server" Font-Bold="True"></asp:label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%"><asp:label id="capCOMPLETED_BY" Runat="server"></asp:label></TD>
		<TD class="midcolora" vAlign="top" align="left" width="13%"><asp:label id="capCOMPLETEDATE" Runat="server" Font-Bold="True"></asp:label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%"><asp:label id="capCOMPLETE_DATETIME" Runat="server"></asp:label></TD>
	</tr>
	<tr id="PR3">
		<TD class="midcolora" vAlign="top" align="left" width="13%"><asp:label id="capOLDPOLICY_VERSION_ID" Runat="server" Font-Bold="True"></asp:label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%"><asp:label id="capPOLICY_VERSION_ID" Runat="server"></asp:label></TD>
		<TD class="midcolora" vAlign="top" align="left" width="13%"><asp:label id="capNEWPOLICY_VERSION_ID" Runat="server" Font-Bold="True"></asp:label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%"><asp:label id="capNEW_POLICY_VERSION_ID" Runat="server"></asp:label></TD>
		<TD class="midcolora" vAlign="top" align="left" width="13%"><asp:label id="capCOMMENTS" Runat="server" Font-Bold="True"></asp:label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%"><asp:label id="capPRINTS_COMMENTS" Runat="server"></asp:label></TD>
	</tr>
	<tr id="PR4">
		<TD class="midcolora" vAlign="top" align="left" width="13%"><asp:label id="capPREVIOUS_POLICY_STATUS" Runat="server" Font-Bold="True"></asp:label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%"><asp:label id="capPOLICY_PREVIOUS_STATUS" Runat="server"></asp:label></TD>
		<TD class="midcolora" vAlign="top" align="left" width="13%"><asp:label id="capPOLICY_CUR_STATUS" Runat="server" Font-Bold="True"></asp:label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%"><asp:label id="capPOLICY_CURRENT_STATUS" Runat="server"></asp:label></TD>
		<TD class="midcolora" vAlign="top" align="left" width="13%"><asp:label id="capExpirationDate" Runat="server" Font-Bold="True"></asp:label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%"><asp:label id="capProcessExpirationDate" Runat="server"></asp:label></TD>
	</tr>
</TABLE>
<input id="hidProcess_ID" type="hidden" name="hidProcess_ID" runat="server"> <input id="hidBASE_VERSION_ID" type="hidden" name="hidBASE_VERSION_ID" runat="server">
<script language="javascript">
showHideProcessTop();
</script>
