<%@ Page Language="c#" CodeBehind="index.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Aspx.index" %>

<html>
<head>
    <meta http-equiv="CACHE-CONTROL" content="NO-CACHE" />
    <title>EBIX Advantage</title>
    <script src="/cms/cmsweb/scripts/AJAXcommon.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        window.onbeforeunload = function (evt) {
            var message = '<%=strAlert %>';
            if (typeof evt == 'undefined') {
                evt = window.event;
            }
            if (evt) {
                evt.returnValue = message;

            }
            //top.location.href = '/cms/cmsweb/aspx/index.aspx'; //Done for Itrack Issue 5181 on 7 April 2009
            return message;

        }	
    </script>
    <script language="vbscript" type="text/vbscript">
'' FOR=window EVENT=onbeforeunload>
'if msgbox ("Do you want to close the window",vbyesno,"BRICS") = vbYes then'
	'window.onbeforeunload=true
'else
'	window.onbeforeunload=false
'end if
    </script>
</head>
<frameset rows="70,*" style="width: 100%" border="0">
		<frame valign="top" frameborder="no" name="topframe" src="/cms/cmsweb/menus/topFrame.aspx" scrolling="no" noresize=true >
		<frame frameborder="no" name="botframe" src="/cms/cmsweb/menus/menuBar.aspx" scrolling="no" noresize=true>
	</frameset>
</html>
