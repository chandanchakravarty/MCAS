<%@ Page language="c#" Codebehind="Quote.aspx.cs" AutoEventWireup="false" Inherits="Cms.Application.Aspx.Quote.Quote" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title><%=header%></title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
		function setQuoteXML()
		{  			 
			if(window.opener.document.getElementById("hidQuoteXML")!=null)
			{	 
				document.write(window.opener.document.getElementById("hidQuoteXML").value);			
			}

}
//Added By Lalit For Show Rate details At PageLoad
function setRATEXML() {
    debugger;
    if (window.opener.document.getElementById("hidRATE_XML") != null) {
        alert(window.opener.document.getElementById("hidRATE_XML").value)
        document.write(window.opener.document.getElementById("hidRATE_XML").value);
    }
}
		
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;">
		<form id="frmGenerateQuote" method="post" runat="server">
		<input type="hidden" id="hidRATE_HTML" name="hidRATE_HTML" runat="server" />
		</form>
	</BODY>
</HTML>
