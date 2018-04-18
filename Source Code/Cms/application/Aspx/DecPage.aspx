<%@ Page language="c#" Codebehind="DecPage.aspx.cs" AutoEventWireup="false" Inherits="Cms.Application.Aspx.DecPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
    <title>Brics-Dec Page</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
	<meta name="CODE_LANGUAGE" content="C#"/>
	<meta name="vs_defaultClientScript" content="JavaScript" />
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
<script language = "javascript" type="text/javascript">
	 function OpenDeclaration() {
	     document.location = "/Cms/application/Aspx/DeclarationPage.aspx";
	     return false;
	     onload = "javascript:OpenDeclaration();"
        }
</script>	
  </head>
  <body oncontextmenu="return false;" MS_POSITIONING="GridLayout" >
    <form id="Form1" method="post" runat="server">    
     <input id="hidCHECK_PDF" type="hidden" value="0" name="hidCHECK_PDF" runat="server" />
     <input id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server" />
     <input id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server" />
     <input id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server" />
     
     </form>
     <script type="text/javascript" language="javascript">
//		alert(document.getElementById('hidCHECK_PDF').value);
//		if(document.getElementById('hidCHECK_PDF').value == 'BLANK_NUM_SOME')
//			alert('Some check(s) not generated due to Account(s) missing Bank Information');
     </script>
  </body>
</html>
