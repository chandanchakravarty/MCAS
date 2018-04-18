<%@ Page CodeBehind="login.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Cms.CmsWeb.Aspx.login" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>EBIX Advantage</title>
<style type="text/css">
.maintext {
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size: 11px;
	font-variant: normal;
	color: #000000;
	text-decoration: none;
}	
</style>
<link href="ea_style.css" rel="stylesheet" type="text/css" />

        <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1"/>
		<meta http-equiv="CACHE-CONTROL" content="NO-CACHE"/>
		<link href="/Cms/CmsWeb/Css/Login.css" type="text/css" rel="stylesheet"/>
		<script src="/cms/cmsweb/scripts/menubar.js" type="text/javascript"></script>
		<script src="/cms/cmsweb/scripts/common.js" type="text/javascript"></script>
		<script src="/cms/cmsweb/scripts/form.js" type="text/javascript"></script>
		<script language='javascript' type="text/javascript">
		function OpenFullWindow()
		{
		  try
		  {
		  	if(window != window.top)   
			{
				window.top.location.href = "login.aspx";
		   	}
		  }
		  catch(cancel)
		  {}		   
		}				
		function authenticate()
		{
		    
			if(document.getElementById("hidLG").value!=1)
			{
				if(document.loginForm.txtSystemId.value!="" && document.loginForm.txtUserID.value!="" && document.loginForm.txtPassword.value!=""	 ) 
				{
					lblMessage.innerHTML="";
					document.loginForm.action ="login.aspx";
					document.loginForm.submit(); 
				}
				else
				{
					lblMessage.innerHTML="Please enter complete information";
				}	
			}
			else
			{
				top.location="/cms/cmsweb/aspx/index.aspx"; 			
			}				
		}		
		
		function Logout()
		{
			top.location="/cms/cmsweb/aspx/login.aspx?action=lg"; 
		}
		
		function setTabIndex()
		{			
			if(document.getElementById("hidLG").value==1)
			{
				document.getElementById("tableForm").style.display="none";
				//document.getElementById("hiddenTable").style.display="inline";
			}
			else
			{
				document.getElementById("tableForm").style.display="inline";				
				document.getElementById('txtSystemId').focus(); 
				//document.getElementById("hiddenTable").style.display="none";
			}			
		}
		
		function logout()
		{		
		 top.location="/cms/cmsweb/aspx/Logout.aspx";		
		}		
		
		document.onkeydown=function(e)
		{
		    var eventInstance = window.event ? event : e;
		    var keyCode = eventInstance.keyCode ? eventInstance.keyCode : eventInstance.which ? eventInstance.which : eventInstance.charCode;
		    if (keyCode == 13)
			{
				if(document.getElementById("hidLG").value!=1)
				{
					if(document.loginForm.txtSystemId.value!="" && document.loginForm.txtUserID.value!="" && document.loginForm.txtPassword.value!=""	 ) 
					{
						lblMessage.innerHTML="";
						document.loginForm.action ="login.aspx";
						document.loginForm.submit(); 
					}
					else
					{
						lblMessage.innerHTML="Please enter complete information";
					}	
				}
			else
			{
				top.location="/cms/cmsweb/aspx/index.aspx"; 
			}
		  }
		} 
		</script>
</head>
<body oncontextmenu = "return false;" style="margin-left:0; margin-top:0; overflow:auto;" marginwidth="0" marginheight="0" onload="javascript:setTabIndex();OpenFullWindow();">
  <form method="post" action="" runat="server" ID="loginForm" name="loginForm">
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
                    <td width="26%" align="left"><asp:label id="capSystemId" runat="server"/></td>
                    <td width="61%" align="center">
                    <span class="normal_black_text">
                    <asp:TextBox class="textBoxNormal"  valign = "top" name="txtSystemId" id="txtSystemId" size="25" runat="server" MaxLength="50" TabIndex="1"/>                      
                    </span></td>
                  </tr>
                  <tr>
                    <td align="left">&nbsp;</td>
                    <td align="left">&nbsp;</td>
                    <td align="center">&nbsp;</td>
                  </tr>
                  <tr>
                    <td align="left">&nbsp;</td>
                    <td align="left"><asp:label id="capUserID" runat="server"/></td>
                    <td align="center">
                    <asp:TextBox name="txtUserID" class="textBoxNormal" id="txtUserID" size="25" runat="server" MaxLength="50" TabIndex="2"/>
                    </td>
                  </tr>
                  <tr>
                    <td align="left">&nbsp;</td>
                    <td align="left">&nbsp;</td>
                    <td align="center">&nbsp;</td>
                  </tr>
                  <tr>
                    <td align="left">&nbsp;</td>
                    <td align="left"><asp:label id="capPassword" runat="server"/></td>
                    <td align="center">
                    <asp:TextBox name="txtPassword" class="textBoxNormal" TextMode="Password" id="txtPassword" size="25" runat="server" MaxLength="50" TabIndex="3" />
                    </td>
                  </tr>
                  <tr>
                    <td colspan="3" align="right">
                       <a href="#"><img border="0" src="/cms/cmsweb/images/go.jpg" width="46" height="42" vspace="9"  onclick="javascript: authenticate();" /></a>
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
</html>