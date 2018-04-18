<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Page language="c#" Codebehind="OpenCheckImage.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.CheckAmount" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BRICS RTL Check IMAGES</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">		
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
	</HEAD>
	<body MS_POSITIONING="GridLayout" >
		<form id="Form1" method="post" runat="server">	
		<table id="tblRTL_MSG" align="center" runat="server">		
			<tr><td class="midcolora"><asp:label id="lblMessage" Runat="server" CssClass="errmsg"></asp:label></td></tr>
		</table>
		 <%-- //TO MOVE TO LOCAL VSS
		<table id="tblRTL_IMAGES" width="90%" align="center" runat="server">		
		<tr><td class="midcolorc"><a id="hrefCheckFilePath_Front" runat="server">Open check front image</a> </td></tr>
		<tr><td class="midcolorc"><a id="hrefCheckFilePath_Back" runat="server">Open check back image</a> </td></tr>
		<tr><td class="midcolorc"><a id="hrefStubFilePath_Front" runat="server">Open stub front image</a> </td></tr>
		<tr><td class="midcolorc"><a id="hrefStubFilePath_Back" runat="server">Open stub back image</a> </td></tr>
		</table>--%>
		<table align ="center">
		<asp:Repeater ID="rpRt_IMG" runat="server">		      
		       <ItemTemplate>
		         <tr>
		          <td align ="left">
		         <a id="hrefCheckFilePath_Front" runat="server" href='<%# DataBinder.Eval(Container, "DataItem.CheckFilePath_Front") %>'>
		           <asp:Label ID = "lblCheckFilePath_Front" Runat = "server" text = "Check Image"></asp:Label>     
		         </a> 		        
		       	</td>
		       	<td></td>
		       	<td></td>
		       	<td></td>
		       	<td></td>
		       	<td>
		         <a id="hrefStubFilePath_Front" runat="server" href='<%# DataBinder.Eval(Container, "DataItem.StubFilePath_Front") %>'>
		           <asp:Label ID = "lblStubFilePath_Front" Runat = "server" text = "Stub Image"></asp:Label>     
		         </a> 
		         </td>
		            </tr>
		           </ItemTemplate>   
		          
	           </asp:Repeater> 		
	           </table>	
		</form>
	</body>
</HTML>
