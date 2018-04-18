<%@ Page language="c#" Codebehind="ChangeAgencyPassword.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.aspx.ChangeAgencyPassword" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Change Password</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<!--<LINK href="~/cmsweb/css/menu.css" type="text/css" rel="stylesheet">-->
		<BASE target="_self">
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		
		<script language="javascript">
			function Reset()
			{
			   document.ChangeAgencyPassword.reset();
			}
			
			function PasswordLengthCheck()
			{
			   var x= document.getElementById('txtNew_Password').value.length;
			   if(x<8)
			   {	
					return false;
				}
				
			   else
			   { 
					return true;
			   }

		   }
			function ChkPasswordLength(objSource , objArgs)
			{
					objArgs.IsValid = PasswordLengthCheck();	
			}
			function RedirectToLogoutPage()
			{
			  document.getElementById('btnReset').disabled=true;
			  document.getElementById('btnCancel').disabled=true;
			  document.getElementById('btnSave').disabled=true;
			  var cTicks=5
			  var timer = setInterval(
					function()
					{
						try
						{
							if( cTicks )
							{
							cTicks = --cTicks;
							var strSec = cTicks
							document.getElementById('REDIRECT').innerHTML="<Font color=red><B>" + strSec + ""  +  " Sec. remaining.<B></Font>"
							}
							else
							{
								clearInterval(timer);
								if (document.getElementById('hidCALLEDFROM').value=='Icon')
								{
								var myOpener = window.dialogArguments; 
								myOpener.top.location="/cms/cmsweb/aspx/Logout.aspx";	
								window.close();
								}
								else
								window.top.location="/cms/cmsweb/aspx/Logout.aspx";	
							}
						}
						catch(err)
						{}
				  }, 1000);
			}
			function Cancel()		
			{
				window.close();
			}
			function StopToPage()
			{
			window.focus();
			}
	
	function showHideHeader()
	{
		try 
		{

			if (document.getElementById('hidCALLEDFROM').value=='Icon')
			{
			document.getElementById('dvLogo').style.display='none';
			document.getElementById('dvbotomlogo').style.display='none';
			document.getElementById('btnCancel').style.display='inline';
			}
			else
			{
			document.getElementById('dvLogo').style.display='none';
			document.getElementById('dvbotomlogo').style.display='none';
			document.getElementById('btnCancel').style.display='none';
			}
			if(document.getElementById('txtOld_Password'))
				document.getElementById('txtOld_Password').focus();
			
		}catch(err){
			document.getElementById('dvLogo').style.display='none';
			document.getElementById('dvbotomlogo').style.display='none';
			document.getElementById('btnCancel').style.display='inline';
			}
	}
	
	function RemoveSingleQuote(objEvent) 
	{
	    objEvent = (objEvent) ? objEvent : event;
	    var charCode = (objEvent.charCode) ? objEvent.charCode : ((objEvent.keyCode) ? objEvent.keyCode : ((objEvent.which) ? objEvent.which : 0));
	  
	    if (charCode == '39') {
	       return false;
	    }
	    return true;
	}
	
	function replaceQuote(obj)
	{
			chr = obj.value;
			if(chr.indexOf("'") != -1)
			{	
				obj.value = ReplaceAll(chr,"'",'')
			}
	}

	
   </script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="showHideHeader();ApplyColor();">
		<script language="javascript">window.name="modal";</script>
		<form id="ChangeAgencyPassword" method="post" runat="server" target="modal">
			<div id='dvLogo'>
			<table width="100%" border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td background="/cms/cmsweb/images/top_bg.gif"><table width="100%" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td width="42%"><div align="left"><img src="/cms/cmsweb/images/logo.gif" width="335" height="95"></div>
								</td>
								<td width="29%">&nbsp;</td>
								<td width="29%"><div align="right"><img src="/cms/cmsweb/images/bricks_logo.gif" width="245" height="95"></div>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>	
			</div>
			<TABLE class="tableWidth" align='center'>
				<TBODY>
				  <TR><TD colSpan="2">
				  <table border="0" cellspacing="0" cellpadding="0" width="100%" class="headereffectCenter">
						<tr>
						<td><asp:Label ID="cap_pass" runat="server"></asp:Label></td> <%--Change Password Tab--%>
						</tr>
					</tr>
					</table></TD>
					</TR>
				   <tr>
						<td class="midcolorc" align="right" colSpan="2"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label><br>
						<div class="counter" id="REDIRECT" CssClass="errmsg"></div>
						</td>
				  </tr>
					<TR>
						<TD class="midcolora" width="30%"><asp:label id="capUserName" runat="server"></asp:label><SPAN class="mandatory">*</SPAN></TD>
						<TD class="midcolora" width="70%">
							<asp:textbox id="txtUserName" runat="server" readOnly="True"></asp:textbox><BR>
							<asp:requiredfieldvalidator id="rfvUserName" runat="server" Enabled="True" Display="Dynamic" ControlToValidate="txtUsername"></asp:requiredfieldvalidator>
						</TD>
					<TR>
						<TD class="midcolora" width="30%"><asp:label id="capOld_Password" runat="server"></asp:label><SPAN class="mandatory">*</SPAN></TD>
						<TD class="midcolora" width="70%">
							<asp:textbox id="txtOld_Password" runat="server" TextMode="Password" AutoComlete = "Off"></asp:textbox><BR>
							<asp:requiredfieldvalidator id="rfvOld_Password" runat="server" Enabled="True" Display="Dynamic" ControlToValidate="txtOld_Password"></asp:requiredfieldvalidator>
						</TD>
					<TR>
						<TD class="midcolora" width="30%"><asp:label id="capNew_Password" runat="server"></asp:label><SPAN class="mandatory">*</SPAN></TD>
						<TD class="midcolora" width="70%">
							<asp:textbox id="txtNew_Password" onkeypress="return RemoveSingleQuote(event);"  runat="server" TextMode="Password" AutoComlete = "Off" onBlur="replaceQuote(this);"></asp:textbox><BR>
							<asp:requiredfieldvalidator id="rfvNew_Password" runat="server" Enabled="True" Display="Dynamic" ControlToValidate="txtNew_Password"></asp:requiredfieldvalidator>
							<asp:CustomValidator id="csvNew_Password" Runat="server" Display="Dynamic" ControlToValidate="txtNew_Password" ClientValidationFunction="ChkPasswordLength"></asp:CustomValidator>
							<asp:regularexpressionvalidator id="revNew_Password" runat="server" Display="Dynamic" ControlToValidate="txtNew_Password"
											ErrorMessage="Please Enter a valid password Containing at least one numeric, one Upper case and One Lower Case and a special Character."></asp:regularexpressionvalidator>
						</TD>
					<TR>
						<TD class="midcolora" width="30%"><asp:label id="capConfirm_Password" runat="server"></asp:label><SPAN class="mandatory">*</SPAN></TD>
						<TD class="midcolora" width="70%">
							<asp:textbox id="txtConfirm_Password" AutoComlete = "Off" onkeypress="return RemoveSingleQuote(event);" onBlur="replaceQuote(this);" runat="server" TextMode="Password"></asp:textbox><BR>
							<asp:requiredfieldvalidator id="rfvConfirm_Password" runat="server" Display="Dynamic" ControlToValidate="txtConfirm_Password"></asp:requiredfieldvalidator>
							<asp:comparevalidator id="cmpvNew_Password" runat="server" Display="Dynamic" ControlToValidate="txtConfirm_Password"
								ControlToCompare="txtNew_Password"></asp:comparevalidator><br>
							<asp:regularexpressionvalidator id="revConfirm_Password" runat="server" Display="Dynamic" ControlToValidate="txtConfirm_Password"></asp:regularexpressionvalidator>	
						</TD>
					</TR>
					<TR>
					
						<TD class="midcolora" colSpan="1">
							<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
							<cmsb:cmsbutton class="clsButton" id="btnCancel" runat="server" Text="Cancel"></cmsb:cmsbutton>
						</TD>
						
						<TD class="midcolorr" colSpan="1" align="right">
							<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
						</TD>
					</TR>
					<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
					<INPUT id="hidCALLEDFROM" type="hidden" value="0" name="hidCALLEDFROM" runat="server">
				</TBODY>
			</TABLE>
			<div id='dvbotomlogo'>
					<table width="100%">
						<tr>
						<td background="/cms/cmsweb/images/bottom_bg.gif" width="100%">
						<table width="100%" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td width="15%"><a href="" onclick="javascript:return openWindowHomePage('http://www.ebix.com');"><img src="/cms/cmsweb/images/powered_ebix.jpg"  height="28" border="0"></a></td>
								
								<td width="85%" align="right" class="ck"><div align="right">Copyright ® 2005 Wolverine Mutual. All Rights Reserved.</div>
								</td>
							</tr>
						</table>
						</td>
					</td></tr>
					</table>
			</div>
			<table  class="tableWidth" align='center'>
			<tr>
				<td align="center" colspan="2"><webcontrol:footer id="pageFooter" runat="server"></webcontrol:footer></td>
			</tr>
			</table>
		</form>
	</body>
</HTML>
