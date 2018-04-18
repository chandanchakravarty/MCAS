<%@ Page language="c#" Codebehind="ShowDialog.aspx.cs" AutoEventWireup="false" Inherits="Cms.Application.Aspx.ShowDialog" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>EBIX Advantage -<%=head%></title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script>
  function checkForalert()
  {
	//Set the variable of client top
	//Call The Client Top Function
	window.opener.document.getElementById('cltClientTop:hidReturnPolicyStatus').value=document.getElementById('hidSubmitApp').value
	window.opener.document.getElementById('cltClientTop:hidCltNewBusinessProcess').value=document.getElementById('hidNewBusinessProcess').value
	window.opener.CalledFromShow();
	window.close();
  }	   
  
  function checkForDeactiveApp()
  {
	alert('<%=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("993")%>')
	window.close();
  }
	  
  function refreshParent()
  {
	/*var flag='<%=validApplication%>';
	if(flag!=0)*/
	if( <%= "'" + strCalledFrom  + "'" %> == "PROCESS")
	{
		return false;
	}
	var loc=window.opener.location.href;   
		window.opener.location=loc.toString();  
		window.focus();
		
		document.title="EBIX Advantage - Rule Mandatory Information";
	
  }
		</script>
</HEAD>
	<body  MS_POSITIONING="GridLayout" onload="refreshParent();" >
		<form id="Form1" method="post" runat="server">
		<input id="hidSubmitApp" type="hidden" value="0" runat="server" NAME="hidSubmitApp">
		<input id="policyNumber" type="hidden" value="0" runat="server" NAME="PolicyNumber">
		<input id="hidPolicy_ID" type="hidden" value="0" runat="server" NAME="hidPolicy_ID">
		<input id="hidPolicy_Version_ID" type="hidden" value="0" runat="server" NAME="hidPolicy_Version_ID">
		<input id="hidNewBusinessProcess" type="hidden" value="0" runat="server" NAME="hidNewBusinessProcess">
		 
		</form>
		
	</body>
</HTML>
