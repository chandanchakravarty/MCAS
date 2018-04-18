<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page CodeBehind="ChangePassword.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Cms.CmsWeb.aspx.ChangePassword" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<HTML>
	<HEAD>
		<title>EBIX-ADVANTAGE</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<META http-equiv="CACHE-CONTROL" content="NO-CACHE">
		<LINK href="/Cms/CmsWeb/Css/Login.css" type="text/css" rel="stylesheet">
			<script src="/cms/cmsweb/scripts/menubar.js"></script>
			<script src="/cms/cmsweb/scripts/common.js"></script>
			<script src="/cms/cmsweb/scripts/form.js"></script>
			<script language="javascript">
			
			function Reset()
			{
			   document.ChangePassword.reset();
			}
		
			function PasswordLengthCheck()
			{
			   var x= document.getElementById('txtNewPassword').value.length;
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
	<body oncontextmenu = "return false;" style="margin-left:0; margin-top:0; overflow:auto;" marginwidth="0" marginheight="0">
  <form id="ChangePassword" name="ChangePassword" runat="server">
  <INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
  <INPUT id="hidCALLEDFROM" type="hidden" value="0" name="hidCALLEDFROM" runat="server">
  <table width="100%" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td align="center"><table width="989" cellspacing="0" cellpadding="0">
      <tr>
        <td width="1000" align="left" valign="top"><img src="/cms/cmsweb/images/top_header.jpg" width="989" height="73" /></td>
      </tr>
      <tr>
        <td><table width="100%" cellspacing="0" cellpadding="0">
          <tr>
            <td width="335" align="left" valign="top"><img src="/cms/cmsweb/images/left_gradient.jpg" width="335" height="318" /></td>
            <td width="654" align="left" valign="top"><img src="/cms/cmsweb/images/main_right_pic.jpg" width="654" height="318" /></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td valign="top" bgcolor="#FFFFFF"><table width="100%" cellspacing="0" cellpadding="0">
          <tr>
            <td width="335" height="280" valign="top" background="/cms/cmsweb/images/login_screen.jpg"><form id="form1" name="form1" method="post" action="">
                <table width="82%" align="center" cellpadding="0" cellspacing="1" class="normal_font" id="tableForm">
                  <tr>
                  <input type="hidden" id="hidLG" name="hidLG" runat="server"/>
                    <td height="66" colspan="3" align="left" ></td>
                  </tr>
                  <tr>
                    <td align="left">&nbsp;</td>
                    <td align="left">&nbsp;</td>
                    <td align="left">&nbsp;</td>
                  </tr>
                  <tr align="left" valign="top">
			        	<td colspan="3" align="center">
					        <asp:Label ForeColor="red" ID="lblMessage" Runat="server" Font-Size="8"></asp:Label>
				        </td>
			      </tr>
                  <tr>
                    <td width="13%" align="left">&nbsp;</td>
                    <td width="26%" align="left"><asp:label id="lblUserName" runat="server"/></td>
                    <td width="61%" align="center">
                    <span class="normal_black_text">
                    <asp:TextBox class="textBoxNormal"  valign = "top" name="txtSystemId" id="txtUserName" size="25" runat="server" MaxLength="50" TabIndex="1"/>                      
                    </span></td>
                  </tr>
                  <tr>
                    <td align="left">&nbsp;</td>
                    <td align="left">&nbsp;</td>
                    <td align="center">&nbsp;</td>
                  </tr>
                  <tr>
                    <td align="left">&nbsp;</td>
                    <td align="left"><asp:label id="lblPwd" runat="server"/></td>
                    <td align="center">
                    <asp:TextBox name="txtUserID" class="textBoxNormal" id="txtPassword"  TextMode="Password" size="25" runat="server" MaxLength="50" TabIndex="2"/>
                    </td>
                  </tr>
                  <tr>
                    <td align="left">&nbsp;</td>
                    <td align="left">&nbsp;</td>
                    <td align="center">&nbsp;</td>
                  </tr>
                  <tr>
                    <td align="left">&nbsp;</td>
                    <td align="left"><asp:label id="lblNWPwd" runat="server"/></td>
                    <td align="center">
                    <asp:TextBox name="txtPassword" class="textBoxNormal" TextMode="Password" id="txtNewPassword" size="25" runat="server" MaxLength="50" TabIndex="3" onkeypress="return RemoveSingleQuote(event);" onBlur="replaceQuote(this);" />
                    </td>
                  </tr>
                  <tr>
                    <td align="left">&nbsp;</td>
                    <td align="left">&nbsp;</td>
                    <td align="center">&nbsp;</td>
                  </tr>
                   <tr>
                    <td align="left">&nbsp;</td>
                    <td align="left"><asp:label id="lblCNFPwd" runat="server"/></td>
                    <td align="center">
                    <asp:TextBox name="txtPassword" class="textBoxNormal" TextMode="Password" id="txtConfirmPassword" size="25" runat="server" MaxLength="50" TabIndex="4" onkeypress="return RemoveSingleQuote(event);" onBlur="replaceQuote(this);" />
                    </td>
                  </tr>
                  <tr>
                    <td colspan="3" align="right">
                       <asp:ImageButton  id="btnSave"  ImageUrl="/cms/cmsweb/images/go.jpg"   Runat="server" />
                     </td>
                    </tr>
                </table>
            </form></td>
            <td width="654" height="280" valign="top" background="/cms/cmsweb/images/right_bg.jpg"><table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                  <td height="23" ></td>
                </tr>
                <tr>
                  <td align="left"><img src="/cms/cmsweb/images/tabs_header.jpg" width="615" height="47" hspace="2" /></td>
                </tr>
                <tr>
                  <td><table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                        <td height="9" colspan="2" valign="top" ></td>
                      </tr>
                      <tr>
                        <td width="35%" valign="top"><table width="59%" align="center" cellpadding="3" cellspacing="0">
                            <tr>
                              <td width="14%" align="center"><img src="/cms/cmsweb/images/bullet.png" width="7" height="7" /></td>
                              <td width="86%" align="left"><a href="#" class="normal_font_black">Ebix Australia</a></td>
                            </tr>
                            <tr>
                              <td align="center"><img src="/cms/cmsweb/images/bullet.png" width="7" height="7" /></td>
                              <td align="left"><a href="#" class="normal_font_black">EbixExchange</a> </td>
                            </tr>
                            <tr>
                              <td align="center"><img src="/cms/cmsweb/images/bullet.png" width="7" height="7" /></td>
                              <td align="left"><a href="#" class="normal_font_black">Ebix BPO</a></td>
                            </tr>
                            <tr>
                              <td align="center"><img src="/cms/cmsweb/images/bullet.png" width="8" height="8" /></td>
                              <td align="left"><a href="#" class="normal_font_black">EbixHealth</a></td>
                            </tr>
                            <tr>
                              <td align="center"><img src="/cms/cmsweb/images/bullet.png" width="8" height="8" /></td>
                              <td align="left"><a href="#" class="normal_font_black">ConfirmNET</a></td>
                            </tr>
                            <tr>
                              <td align="center"><img src="/cms/cmsweb/images/bullet.png" width="8" height="8" /></td>
                              <td align="left"><a href="#" class="normal_font_black">ebixASP</a></td>
                            </tr>
                            <tr>
                              <td align="center"><img src="/cms/cmsweb/images/bullet.png" width="8" height="8" /></td>
                              <td align="left"><a href="#" class="normal_font_black">WinFlex Web</a></td>
                            </tr>
                            <tr>
                              <td align="center"><img src="/cms/cmsweb/images/bullet.png" width="8" height="8" /></td>
                              <td align="left"><a href="#" class="normal_font_black">VitalSales Suite </a></td>
                            </tr>
                        </table></td>
                        <td valign="top"><table width="78%" align="center" cellpadding="3" cellspacing="0">
                            <tr>
                              <td width="22%" align="right"><img src="/cms/cmsweb/images/bullet.png" width="7" height="7" /></td>
                              <td width="39%" align="left"><a href="#" class="normal_font_blue">BRICS</a></td>
                              <td width="5%"><img src="/cms/cmsweb/images/bullet.png" width="7" height="7" /></td>
                              <td width="34%" align="left"><a href="#" class="normal_font_blue"> Sunrise Exchange</a></td>
                            </tr>
                            <tr>
                              <td align="right"><img src="/cms/cmsweb/images/bullet.png" width="7" height="7" /></td>
                              <td align="left"><a href="#" class="normal_font_blue">INFINITY</a> </td>
                              <td><img src="/cms/cmsweb/images/bullet.png" width="7" height="7" /></td>
                              <td align="left"><a href="#" class="normal_font_blue">Winflex</a></td>
                            </tr>
                            <tr>
                              <td align="right"><img src="/cms/cmsweb/images/bullet.png" width="7" height="7" /></td>
                              <td align="left"><a href="#" class="normal_font_blue">Sunrise Exchange</a></td>
                              <td><img src="/cms/cmsweb/images/bullet.png" width="7" height="7" /></td>
                              <td align="left"><a href="#" class="normal_font_blue">VitalSales Suite </a></td>
                            </tr>
                            <tr>
                              <td align="right"><img src="/cms/cmsweb/images/bullet.png" width="7" height="7" /></td>
                              <td align="left"><a href="#" class="normal_font_blue">eGLOBAL</a></td>
                              <td><img src="/cms/cmsweb/images/bullet.png" width="7" height="7" /></td>
                              <td align="left"><a href="#" class="normal_font_blue">LifeSpeed</a></td>
                            </tr>
                            <tr>
                              <td align="right"><img src="/cms/cmsweb/images/bullet.png" width="7" height="7" /></td>
                              <td align="left"><a href="#" class="normal_font_blue">TrackCertsNOW </a></td>
                              <td><img src="/cms/cmsweb/images/bullet.png" width="7" height="7" /></td>
                              <td align="left"><a href="#" class="normal_font_blue">AnnuityNET</a></td>
                            </tr>
                            <tr>
                              <td align="right"><img src="/cms/cmsweb/images/bullet.png" width="7" height="7" /></td>
                              <td align="left"><a href="#" class="normal_font_blue">CertificatesNOW</a></td>
                              <td><img src="/cms/cmsweb/images/bullet.png" width="7" height="7" /></td>
                              <td align="left"><a href="#" class="normal_font_blue">LuminX</a></td>
                            </tr>
                            <tr>
                              <td align="right"><img src="/cms/cmsweb/images/bullet.png" width="7" height="7" /></td>
                              <td align="left"><a href="#" class="normal_font_blue">WinBEAT</a></td>
                              <td><img src="/cms/cmsweb/images/bullet.png" width="7" height="7" /></td>
                              <td align="left"><a href="#" class="normal_font_blue">CertsOnline</a></td>
                            </tr>
                            <tr>
                              <td align="right"><img src="/cms/cmsweb/images/bullet.png" width="7" height="7" /></td>
                              <td align="left"><a href="#" class="normal_font_blue">CBS</a></td>
                              <td>&nbsp;</td>
                              <td align="left">&nbsp;</td>
                            </tr>
                        </table></td>
                      </tr>
                  </table></td>
                </tr>
            </table></td>
          </tr>
        </table></td>
      </tr>
      
    </table></td>
  </tr>
  <tr>
  <td colSpan="3">
   <table width="90%" align="center">
	<tr>
		<td>
				<asp:requiredfieldvalidator id="rfvOldPassword" runat="server" ForeColor="Blue" Display="Dynamic" ControlToValidate="txtPassword"
				Enabled="True"></asp:requiredfieldvalidator>
				<DIV></DIV>
				<asp:regularexpressionvalidator id="revNewPassword" runat="server" ForeColor="Blue" 
				Display="Dynamic" ControlToValidate="txtNewPassword"></asp:regularexpressionvalidator>
				<DIV></DIV>
				<asp:requiredfieldvalidator id="rfvPassword" runat="server" ForeColor="Blue" Display="Dynamic" ControlToValidate="txtPassword"
				Enabled="True"></asp:requiredfieldvalidator>
				<DIV></DIV>
				<asp:requiredfieldvalidator id="rfvNewPassword" runat="server" ForeColor="Blue" Display="Dynamic" ControlToValidate="txtNewPassword"
				Enabled="True"></asp:requiredfieldvalidator>
				<DIV></DIV>
				<asp:customvalidator id="csvNewPassword" Runat="server" ForeColor="Blue" Display="Dynamic" ControlToValidate="txtNewPassword"
				ClientValidationFunction="ChkPasswordLength"></asp:customvalidator>
			<DIV></DIV>
				<asp:requiredfieldvalidator id="rfvConfirmPassword" runat="server" ForeColor="Blue" Display="Dynamic" ControlToValidate="txtConfirmPassword"></asp:requiredfieldvalidator><asp:comparevalidator id="cmpvNewPassword" runat="server" ForeColor="Blue" Display="Dynamic" ControlToValidate="txtConfirmPassword"
				ControlToCompare="txtNewPassword"></asp:comparevalidator>
			<DIV></DIV>
																
																
															</td>
														</tr>
													</table>
												</td>
  </tr>
  <tr>
    <td align="center"><table width="945" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td align="left" class="btm_font"><br />
          Copyright &reg; 2010 <span class="style1">EBIX ADVANTAGE WEB</span>. All Rights Reserved. </td>
      </tr>
    </table></td>
    </tr>
   </table>
     </form>
  <map name="Map" id="Map">
	<area shape="rect" coords="475,200,590,235" href="http://www.ebix.com" target="_blank" alt=""/>
  </map>
</body>
</HTML>
