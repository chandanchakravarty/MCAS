<%@ Page language="c#" Codebehind="topFrame.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.menus.topFrame" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE></TITLE>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR"/>
		<link href="/cms/cmsweb/css/menu<%=GetColorScheme()%>.css" type="text/css" rel="stylesheet"/>
		<link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet" />
        <%--<link href="css/topband.css" rel="stylesheet" type="text/css" />--%>
        <link href="/cms/cmsweb/css/topband.css" type="text/css" rel="Stylesheet" />

		<script src="/cms/cmsweb/scripts/common.js" type="text/javascript"></script>
		<script src="/cms/cmsweb/scripts/xmldom.js" type="text/javascript"></script>
		<script src="/cms/cmsweb/scripts/menuContents.js" type="text/javascript"></script>
		<script src="/cms/cmsweb/scripts/menuBar.js" type="text/javascript"></script>
		<script src="/cms/cmsweb/scripts/AJAXCommon.js" type="text/javascript"></script>
		<script type="text/JavaScript">
		    function MM_swapImage(imgId, imgPath) {
		        // alert(imgId + ' ' + imgPath );
		        imgId.src = imgPath;
		    }
		    function MM_swapImgRestore(imgId, imgPath) { //v3.0
		        imgId.src = imgPath;
		    }
		    //-->
        </script>
		<script language="JavaScript" type="text/javascript">
		    var colorScheme = "<%=GetColorScheme()%>";
		    function getImageFolder() 
		    {
		        var loc = new String();
		        loc = self.location.toString();
		        var arr = loc.split("=");
		        img1.src = arr[1];
		    }
		    function redirectPage() 
		    {
		        try {
		            top.document.location = '../aspx/login.aspx?action=lg';
		        } catch (err) { }
		    }
		    function redirectHome() 
		    {
		        try {
		            top.document.location = '../aspx/index.aspx';
		        } catch (err) { }
		    }
		    function OpenBricsHelp() 
		    {
		        window.open('/cms/cmsweb/CompanyProfile/EBX-Adv-Help v1.6.pdf', 'EBIXADVANTAGEWEB', 'resizable=yes,scrollbars=yes,left=150,top=50,width=800,height=500');
		    }
		    function redirectChangePasswordPage() 
		    {
		        //window.showModalDialog("../aspx/ChangeAgencyPassword.aspx?CalledFrom=Icon",window,'center:yes;resizable:no;status:no;dialogWidth:800px;dialogHight:100px');
		        window.showModalDialog("../aspx/ChangePasswordFrame.aspx?CalledFrom=Icon", window, 'center:yes;resizable:no;status:no;dialogWidth:850px;dialogHight:850px');
		        //var newwin = window.open("../aspx/ChangeAgencyPassword.aspx?CalledFrom=Icon","ChangePassword",'model=1,location=0,status=1,scrollbars=0,width=700,height=400');
		    }
		    var refSubmitWin;
		    function ShowLocation() 
		    {
		        if (parent.refSubmitWin != null) 
		        {
		            parent.refSubmitWin.close();
		        }
		        parent.refSubmitWin = window.open('/cms/cmsweb/aspx/searchlocation.aspx', 'BRICS', 'resizable=yes,scrollbars=yes,left=150,top=50,width=800,height=500');

		        //return false;
		    }

		    //ShowAccountSearch() and CheckSession() and RemoveRatingSoftwareLink() for AGency Login: Praveen Kasana
		    function ShowAccountSearch() 
		    {
		        if (parent.refSubmitWin != null) {
		            parent.refSubmitWin.close();
		        }
		        parent.refSubmitWin = window.open('/cms/Account/Aspx/AR_Inquiry_Info.aspx?CalledFrom=TopFrame', 'BRICSAI', 'resizable=yes,scrollbars=no,left=150,top=50,width=800,height=500');
		    }

		    function CheckSession() 
		    {
		        gSessionVal = '<%=Session["systemID"]%>';
		        CarrierSystemID = '<%=CarrierSystemID%>';
		        if (gSessionVal.toUpperCase() != CarrierSystemID.toUpperCase()) //hide the Account Search if it is a Agency user:
		        {
		            if (document.getElementById('acct'))
		                document.getElementById('acct').style.display = "none";
		            if (document.getElementById('loc'))
		                document.getElementById('loc').style.display = "none";

		        }

		    }

		    /*function RateAndMenual()
		    {
		    top.document.location='http://manual.wolverinemutual.com/';
		    }
			
			function RateingSoftware()
		    {
		    top.document.location='http://www.customraters.com/wolverine/company.asp';
		    }*/

		    //Added to show User's Full Name without using cookies
		    function callSession() {
		        var userName = '<%=Session["userFLName"]%>';
		        var langCulture = '<%=Session["languageCode"]%>';  //Added by Charles on 15-Mar-10 for Multilingual Implementation
		        return (userName + "^" + langCulture);  //Changed by Charles on 15-Mar-10 for Multilingual Implementation
		    }
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" rightMargin="0" onload="CheckSession();">		
    <table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0">
    <tr>
        <td>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#2d2d2d">
  <tr>
    <td width="50%"><table border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td height="20" onmouseover="this.className = 'hlt';" onmouseout="this.className = 'norm';" class="norm"><a href="#" style="color:#ccc; text-decoration:none;"></a></td>
    <%--<td height="20" onmouseover="this.className = 'hlt';" onmouseout="this.className = 'norm';" class="norm"><a href="#" style="color:#ccc; text-decoration:none;">Add</a></td>--%>
    <td height="20" onmouseover="this.className = 'hlt';" onmouseout="this.className = 'norm';" class="norm"><a href="#" style="color:#ccc; text-decoration:none;"></a></td>
    <%--<td height="20" onmouseover="this.className = 'hlt';" onmouseout="this.className = 'norm';" class="norm"><a href="#" style="color:#ccc; text-decoration:none;">Risk Information</a></td>--%>
    <td height="20" onmouseover="this.className = 'hlt';" onmouseout="this.className = 'norm';" class="norm"><a href="#" style="color:#ccc; text-decoration:none;"></a></td>
    <td height="20" onmouseover="this.className = 'hlt';" onmouseout="this.className = 'norm';" class="norm"><a href="#" style="color:#ccc; text-decoration:none;"></a></td>
    <td height="20" onmouseover="this.className = 'hlt';" onmouseout="this.className = 'norm';" class="norm"><a href="#" style="color:#ccc; text-decoration:none;"></a></td>
  </tr>
</table></td>
    <td width="50%" align="right"><table border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td style="font-size:12px;color:#FFFFFF;" valign="middle"><%=GetUserFLName()%></td>
        <td>&nbsp;</td>
        <td onmouseover="this.className = 'hlt1';" onmouseout="this.className = 'norm1';" class="norm1"><a href="javascript:redirectHome();"><img src="/cms/cmsweb/images<%=GetColorScheme()%>/top_home.png" width="30" height="20" border="0" alt="Home" /></a></td>
        <td onmouseover="this.className = 'hlt1';" onmouseout="this.className = 'norm1';" class="norm1"><a href="javascript:redirectChangePasswordPage();"><img src="/cms/cmsweb/images<%=GetColorScheme()%>/top_chpw.png" width="30" height="20" border="0" alt="Change Password" /></a></td>
        <td onmouseover="this.className = 'hlt1';" onmouseout="this.className = 'norm1';" class="norm1"><a href="javascript:ShowLocation();"><img src="/cms/cmsweb/images<%=GetColorScheme()%>/top_locsrch.png" width="30" height="20" border="0" alt="Locations" /></a></td>
        <td onmouseover="this.className = 'hlt1';" onmouseout="this.className = 'norm1';" class="norm1"><a href="javascript:ShowAccountSearch();"><img src="/cms/cmsweb/images<%=GetColorScheme()%>/top_accenq.png" width="30" height="20" border="0" alt="Account Enquiry" /></a></td>
        <td onmouseover="this.className = 'hlt1';" onmouseout="this.className = 'norm1';" class="norm1"><a href="#"><img src="/cms/cmsweb/images<%=GetColorScheme()%>/top_help.png" width="30" height="20" border="0" alt="Preferences" /></a></td>
        <td onmouseover="this.className = 'hlt1';" onmouseout="this.className = 'norm1';" class="norm1"><a href="javascript:redirectPage();"><img src="/cms/cmsweb/images<%=GetColorScheme()%>/logout.png" width="30" height="20" border="0" alt="Logout" /></a></td>
      </tr>
    </table></td>
  </tr>
</table>
        </td>
    </tr>
       
	 <tr>
     <td>
       <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#FFFFFF">
        <tr>
        <td width="17%" height="51" align="left" valign="top"><img src="/cms/cmsweb/images<%=GetColorScheme()%>/logo.jpg" width="364" height="51" border="0" usemap="#Map2" /></td>
        <td width="10%" height="51" align="left" valign="top" background="/cms/cmsweb/images<%=GetColorScheme()%>/bg_top_header.jpg">&nbsp;</td>
        <td width="75%" height="51" align="left" valign="top" background="/cms/cmsweb/images<%=GetColorScheme()%>/bg_top_header.jpg">
          <table width="100%" border="0" cellspacing="0" cellpadding="0" style="padding-top:2px;">
             <%--<tr id="trTOPRIGHT" runat="server" visible="true">
                <td height="47" width="50%" align="left" valign="top" background="/cms/cmsweb/images<%=GetColorScheme()%>/right1.jpg">
                         style="padding-top:3px;"
                </td>
                <td width="50%" align="right" valign="top"  background="/cms/cmsweb/images<%=GetColorScheme()%>/right1.jpg"><img src="/cms/cmsweb/images<%=GetColorScheme()%>/right_bar.jpg" width="290" height="47" border="0" usemap="#Map" /></td>
              </tr>--%>
              <tr>
               <td>
                    <div margin-left:0px margin-top:0px id="maintop" onselectstart="javascript:return false;" onmousedown="javascript:return false;"
			            ondrag="javascript:return false;">
			            <div margin-left:0px margin-top:0px id="topButton"></div>
		            </div>
               </td>
              </tr>
            </table>
            </td>
          </tr>
        </table>
       </td>
      </tr>
    </table>

	   <%-- <map name="Map3" id="Map3">
			<area shape="rect" alt="Home" coords="2,2,24,24" href="javascript:redirectHome();"/>
		</map>
		<map name="Map4" id="Map4">
			<area shape="rect" alt="Logout" coords="5,1,22,24" href="javascript:redirectPage();"/>
		</map>
		<map name="Map5" id="Map5">
			<area shape="rect" alt="Change Password" coords="5,1,22,24" href="javascript:redirectChangePasswordPage();"/>
		</map>
		<map name="Map6" id="Map6">
			<area shape="rect" alt="Brics Help" coords="5,1,22,24" href="javascript:OpenBricsHelp();"/>			
		</map>
		
		<map name="Map2" id="Map2">
            <area shape="rect" coords="25,25,329,94" href="#" />
        </map>
        <map name="Map" id="Map">
        <area shape="rect" coords="251,10,285,43" href="javascript:redirectHome();" alt="rwrerrwr"/>
        <area shape="rect" coords="2,13,30,44" href="javascript:redirectHome();" alt='<%=strHome%>'  />
        <area shape="rect" coords="33,13,52,44" href="javascript:redirectChangePasswordPage();" alt='<%=strPassword%>'  />
        <area shape="rect" coords="54,13,79,42" href="javascript:ShowLocation();" alt="<%=strSearch%>" />
        <area shape="rect" coords="84,13,110,42" href="javascript:ShowAccountSearch();" alt="<%=strInquiry%>" />
        <area shape="rect" coords="115,14,138,42" href="javascript:OpenBricsHelp();" alt="<%=strHelp%>" />
        <area shape="rect" coords="140,14,165,42" href="javascript:redirectPage();" alt=<%=strLogOut%> />
        </map>--%>
		<script language="javascript" type="text/javascript">
		    //setTimeout('callService1()', 1000);
		    setTimeout('callServiceForName(callSession())', 1000); //Added to show User's Full Name without using cookies
		</script>
	</BODY>
</HTML>