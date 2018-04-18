<%@ Page language="c#" Codebehind="TemplateLoad.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.DocumentMerge.TemplateLoad" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>TemplateLoad</title>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
			function findMouseIn()
			{
				if(!top.topframe.main1.mousein)
				{
					top.topframe.main1.mousein = true;
				}
				setTimeout('findMouseIn()',5000);
			}
		</script>
    <script language="vbscript">
			function loadExe()
				spnSetup.style.display ="none"
				spnUpdate.style.display = "none"
				
				Dim objShell
				Dim FSO
				Dim path,versionClient,VersionServer
				Dim strTreeVersion,strUserDefineVersion,strMode,strId,strUploadDownloadURL,strUserId
				
				strTreeVersion = "<%=strTreeVersion%>"
				strUserDefineVersion = "<%=strUserDefineVersion%>"
				strMode = "<%=strMode%>"
				strId = "<%=strId%>"
				strUploadDownloadURL = "<%=strUploadDownloadURL%>"
				strUserId = "<%=strUserId%>"
								
				path = ""
				set objShell = createobject("WScript.Shell")
				Set FSO = CreateObject("Scripting.FileSystemObject")
				On Error Resume Next
				path = objShell.RegRead("HKEY_CLASSES_ROOT\Wolverine\")
				if path <> "" then
					VersionServer = "<%=strDocumentMergeVersion%>"
					versionClient = objShell.RegRead("HKEY_CLASSES_ROOT\Wolverine\Version\Version")
					
					If VersionServer=versionClient then
						If FSO.FileExists(path & "\DocumentMerge.exe") Then
							objShell.Exec path & "\DocumentMerge.exe " & strTreeVersion & " " & strUserDefineVersion & " " & strMode & " " & strId & " " & strUploadDownloadURL & " " & strUserId
						Else 
							spnSetup.style.display =""		
						End If
					Else
						If FSO.FileExists(path & "\DMUpdate.exe") Then 
							objShell.Exec path & "\DMUpdate.exe " & strUploadDownloadURL & " " & VersionServer
						Else
							spnUpdate.style.display = ""
						End If
					End If
				else 
					spnSetup.style.display =""
				End If 
			End function
		</script>
		<script>
			function CloseWindow()
			{
				window.close();
			}
		</script>
  </head>
  <body leftMargin="0" topMargin="0" onload="CloseWindow();">
		<%if (strMode=="MERGE") {%>
		<webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<%}%>
		<form id="TemplateLoad" method="post" runat="server">
			<table width="90%" align=center>
				<tr>
					<td align=center class="midcolorc">
						<span id="spnSetup">
							<br><br><label id="lblDownloadSetup">Document merge is not installed. Please <a href="setup.exe">click here</a> to download document merge setup.</label>
						</span>
						<span id="spnUpdate">
							<br><br><label id="lbldownloadNewVersion">Older version of Document merge exists on your machine. Please <a href="setup.exe">click here</a> to download latest document merge setup.</label>
						</span>
					</td>
				</tr>
			</table>
			<script language=vbscript>loadExe</script>
		</form>
  </body>
</html>
