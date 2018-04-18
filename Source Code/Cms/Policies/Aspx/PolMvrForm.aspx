<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="PolMvrForm.aspx.cs" AutoEventWireup="false" Inherits="Policies.Aspx.PolMvrForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Pol MVR Form</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<STYLE>.hide { OVERFLOW: hidden; TOP: 5px }
	.show { OVERFLOW: hidden; TOP: 5px }
	#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<script language="javascript">
		var onUnloadFlag=false;
			function Delete()
			{
				if ( document.indexForm.hidKeyValues.value != '' )
				{
					refreshGrid('','');
					document.indexForm.hidKeyValues.value = '';
				}
									
			}
			function CloseWindow()
			{
			window.close(); 
			}
			
			function Confirm()
			{
				if(document.getElementById('btnFetchMVR').value != 'Print')
				{
					var ans; 
					ans=window.confirm('Request for MVR – Do you wish to Continue?'); //alert (ans); 
					if (ans==true)
					{ 
						return FetchMVR(); 
					} 
					else 
					{ 
						return false;
					} 
				}
				else
				{
					window.print();
				}				
			}
			
			function ShowTable()
			{
				/*if(document.getElementById('hidRequestType').value=='MVR')
					document.getElementById('tblMVR').style.display='inline'; 
				else
				{
					document.getElementById('tblMVR').style.display='none'; 
					document.getElementById('trMVRButtons').style.display='none'; 
				}*/
				
				
				if(document.getElementById('hidRequestType').value=='MVR')
				{
					if(document.getElementById('hidOperators').value=='0')
						document.getElementById('tblMVR').style.display='none'; 
					else
						document.getElementById('tblMVR').style.display='inline';
				}
				else
				{
					document.getElementById('tblMVR').style.display='none'; 
					//document.getElementById('trMVRButtons').style.display='none'; 
					if(document.getElementById('btnFetchMVR'))
						document.getElementById('btnFetchMVR').value = 'Print';
				}
			}
					
		function FetchMVR()
		{
			if(document.getElementById('hidRequestType').value=='MVR')
			{

				var i,index,delStr,nocheck,strDirLic;
				document.getElementById('hidOperators').value='1';
				delStr = '';
				strDirLic=''
				nocheck = 0;
				for(i=1;i<=iPs;i++)		
				{
					var ctrl=eval("document.getElementById('ck_" + i + "')");
					var rowTag=eval("document.getElementById('Row_" + i + "')");
					if(ctrl)
					{
						if(ctrl.checked)
						{
							var ColVal= new String;
							ColVal = rowTag.uniqueID;
							index = ColVal.indexOf("=",0);
							ColVal = ColVal.substring(index+1, ColVal.length);
							strXML="";
							populateXML(i,msDg)
							strDirLic=strDirLic + ColVal + '^' + getDriverLic(strXML) + '~' ;
							delStr=delStr + ColVal + "~";	
							nocheck = 1;				
						}
					}
						
				}
				if(nocheck == 0)
				{
					alert('Please select some drivers to fetch MVR');
					return false;
				}	
				else
				{
					var strDirLicSplit=strDirLic.split('~');
					var Confirmflag;
					for ( dr=0;dr<strDirLicSplit.length;dr++)
					{
						if (strDirLicSplit[dr]!='')
						{
						var strDriv_lic=strDirLicSplit[dr].split('^');
							if (strDriv_lic[1]=='')
							{
							Confirmflag=confirm('Do you wish to continue with the MVR Request as we do not have a drivers license number for {' + strDriv_lic[2] + '}');
							if (Confirmflag==false)
								{
								break;
								}
							}
						}
					}
					if (Confirmflag==false)
						return false;
					document.getElementById('lblMessage').value='Fetching MVRs, please wait...';
					document.getElementById('tblMVR').style.display='inline';			
					document.getElementById('hidDRIVER_ID').value=delStr;
					//alert('Fetching MVRs, please wait..., click ok to continue.');
					DisableButton(document.getElementById('btnFetchMVR'));
					onUnloadFlag=true;
					return false;
				}
			}
			else
			{
				window.print();
			}
		}
	function getDriverLic(strDrivXML)
		{
		
			var objXmlHandler = new XMLHandler();
			var strDivLic='';
			var tree1 = objXmlHandler.quickParseXML(strDrivXML);
			if(tree1.childNodes.length==0)
			{
				return '';
			}	
			var tree = objXmlHandler.quickParseXML(strDrivXML).childNodes[0];
				for(i=0; i<tree.childNodes.length; i++)
				{
					nodValue = tree.childNodes[i].getElementsByTagName('Table');
					
					if (nodValue != null)
					{
						if (nodValue[0].firstChild == null)
							continue
							DirLic = tree.childNodes[i].getElementsByTagName('DRIVER_DRIV_LIC');
							DrivName = tree.childNodes[i].getElementsByTagName('DRIVER_NAME');
							if (DirLic != null )
							{
								if (DirLic[0] != null )
								{
								strDivLic=DirLic[0].firstChild.text + '^' + DrivName[0].firstChild.text;
	
								}
								else if (DrivName[0] != null )
								{
									strDivLic= '^' + DrivName[0].firstChild.text;
								}
							} 
							else
							{
								if (DrivName[0] != null )
								{
									strDivLic= '^' + DrivName[0].firstChild.text;
								}
							}
					} 
				}   	
			return 	strDivLic;
		}
		function refershParent()
		{
			if(onUnloadFlag)
			window.opener.location.reload(true);
		}

		
		</script>
	</HEAD>
	<body onload="ShowTable()" MS_POSITIONING="GridLayout" onunload="refershParent()">
		<form id="indexForm" method="post" runat="server">
			
			<table id="tblMVR" width="100%" align="center">
				<tr>
					<td class="midcolora" align="left"><asp:label id="lblMessage" runat="server" CssClass="errmsg">Label</asp:label></td>
				</tr>
				<tr>
					<!--<td class="midcolorc" align="center"><INPUT class="clsButton" onclick="javascript:window.close();" type="button" value="Close">
					</td>-->
				</tr>
			</table>
			<table id="tblUNDISCLOSED_DRIVER" width="100%" align="center">
				<tr>
					<td class="midcolorc" align="center"><asp:label id="lblErr" runat="server" CssClass="errmsg">Label</asp:label></td>
				</tr>
				<!---------------- added-  -->
				<tr>
					<td id="tdGridHolder"><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer><asp:placeholder id="GridHolder" runat="server"></asp:placeholder></td>
				</tr>
				<tr>
					<td><webcontrol:gridspacer id="Gridspacer" runat="server"></webcontrol:gridspacer></td>
				</tr>
								<tr>
					<td id="tdGridHolderOpe"><webcontrol:gridspacer id="grdSpacerOpe" runat="server"></webcontrol:gridspacer><asp:placeholder id="GridHolderOpe" runat="server"></asp:placeholder></td>
				</tr>
				<tr>
					<td><webcontrol:gridspacer id="GridspacerOpe" runat="server"></webcontrol:gridspacer></td>
				</tr>
				<tr id="trMVRButtons">
					<td class="midcolorc" align="left" colspan="5"><INPUT runat="server" id="btnCloseDriv" class="clsButton" onclick="javascript:window.close();" type="button" value="Cancel Request">
					<INPUT runat="server" id="btnFetchMVR" class="clsButton"  type="button" value="Send Request">
					<INPUT runat="server" id="btnFetchUDVI" class="clsButton" type="button" Visible="false" value="New UDVI Request">
					</td>
				</tr>
				<tr>
					<td>
						<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" align="center" border="0">
							<tr id="tabCtlRow">
								<td><webcontrol:tab id="TabCtl" runat="server" TabLength="225" TabTitles=" Drivers/Household Members"
										TabURLs="AddDriverDetails.aspx?"></webcontrol:tab></td>
							</tr>
							<tr>
								<td>
									<table class="tableeffect" cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><iframe class="iframsHeightLong" id="tabLayer" src="" frameBorder="0" width="100%" scrolling="no"
													runat="server"></iframe>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<asp:label id="lblTemplate" Visible="false" Runat="server"></asp:label></table>
					</td>
				</tr>
				<!---    end here         -->
				<tr>
					<td><webcontrol:footer id="pageFooter" runat="server"></webcontrol:footer></td>
				</tr>
			</table>
			<INPUT id="hidKeyValues" type="hidden" name="hidKeyValues" runat="server"> <input id="hidlocQueryStr" type="hidden" name="hidlocQueryStr">
			<input type="hidden" name="hidMode"> <input id="hidCheckedRowIDs" type="hidden" name="hidCheckedRowIDs" runat="server">
			<input id="hidDelString" type="hidden" name="hidDelString" runat="server"> <input type="hidden" id="hidRequestType" name="hidRequestType" runat="server">
			<input type="hidden" id="hidOperators" value="0" name="hidOperators" runat="server">
			<input type="hidden" id="hidDRIVER_ID" name="hidDRIVER_ID" runat="server">		
		</form>
	</body>
</HTML>