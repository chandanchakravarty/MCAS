<%@ Page language="c#" Codebehind="ChangePasswordFrame.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.aspx.ChangePasswordFrame" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>ChangePasswordFrame</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <script>
  function openparent()
  {
	var myOpener = window.dialogArguments; 
			myOpener.top.location="/cms/cmsweb/aspx/Logout.aspx";	
			window.close();
  }			
</script>								
  <body MS_POSITIONING="GridLayout">
 <form id="Form1" method="post" runat="server">
<div width=800>
<iframe src="\cms\cmsweb\aspx\ChangeAgencyPassword.aspx?CalledFrom=Icon" frameborder=0 height="100%" width="800"></iframe>
</div>
     </form>
	
  </body>
</html>
