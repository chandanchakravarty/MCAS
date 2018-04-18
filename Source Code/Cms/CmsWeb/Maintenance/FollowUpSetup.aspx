<%@ Page language="c#" Codebehind="FollowUpSetup.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.FollowUpSetup" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="~/cmsweb/webcontrols/menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>FollowUpSetup</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
    <script src="/cms/cmsweb/scripts/xmldom.js"></script>

<script src="/cms/cmsweb/scripts/common.js"></script>

<script src="/cms/cmsweb/scripts/form.js"></script>
<LINK href="/cms/cmsweb/css/menu.css" type=text/css rel=stylesheet >
<script language=javascript>
		function checkMandatory()
		{
			if(document.getElementById('cmbModuleName').options[document.getElementById('cmbModuleName').selectedIndex].value=='Select Option' ||  document.getElementById('cmbDiaryType').options[document.getElementById('cmbDiaryType').selectedIndex].value=='' )			
			{
			    //alert("Please select Module Name and LOB options.")
			    alert(document.getElementById("hidmsg1").value);
				return false;
			}
			
			var i=0;
				
			var maxCount;
		
			if(document.getElementById("hidFlag").value=="Y")
				maxCount=7;
			else
				maxCount=1;
			
			var bool=true;
			var flagCheck=0;
			var arrDiary=document.getElementById("hidDiaryID").value.split(",")
			//var flagCheck=0;
			
			for(i=0;i<arrDiary.length;i++)
			{
				
				var ckType=document.getElementById('ckType_' + (arrDiary[i])); 
				var objUGrp=document.getElementById('cmbUserGroup_' + (arrDiary[i])); 
				var objULst=document.getElementById('cmbUserList_' + (arrDiary[i])); 
				
				
				if(ckType!=null)
				{
					if(ckType.checked)
					{
						flagCheck=1;
						var ugFlag=-1,ulFlag=-1;
						if(objUGrp!=null && objULst!=null)
						{
							if(objUGrp.options.selectedIndex==-1 && objULst.options.selectedIndex==-1)
							{
								ugFlag=0;
								ulFlag=0;	
							}
							else
							{
								if(objUGrp.options.selectedIndex!=-1)
								{
									ugFlag=checkAll(objUGrp);		
								}
								if(objULst.options.selectedIndex!=-1)
								{
									ulFlag=checkAll(objULst);
								}							
							}
						}
					}
				}
				else
				{
					if(objUGrp!=null && objULst!=null)
					{	
						if(objUGrp.options.selectedIndex==-1 && objULst.options.selectedIndex==-1)
						{
							ugFlag=0;
							ulFlag=0;
						}
						else
						{
							
							if(objUGrp.options.selectedIndex!=-1)
							{
								ugFlag=checkAll(objUGrp);									
							}
							if(objULst.options.selectedIndex!=-1)
							{
								ulFlag=checkAll(objULst);
							}							
						}
					}				
				}
				//alert(ugFlag + "---" + ulFlag + "---" + ckType.name)
				
				if(ugFlag<=0 && ulFlag<=0)
				{
				    //alert("Please select either user group or user list");
				    alert(document.getElementById("hidmsg2").value);
					return false;
				}
			}
			
			var txResult=checkFollowUP(maxCount);
			
			if(txResult==false)
			{
			    //alert("Please fill numbers in followup textbox")
			    alert(document.getElementById("hidmsg3").value);
				return false;
			}
		
			
			//alert(document.getElementById("hidCheckFlag").value)
			if(flagCheck<=0)
			if(document.getElementById("hidCheckFlag").value=="")
			{
			    //alert("Please check at least one check box");
			    alert(document.getElementById("hidmsg4").value);
				return false;
			}		
			/*if(flagCheck==0)
			{
				alert("Please check at least one check box");
				return false;
			}*/
			return true;
		}
		
		function checkFollowUP(maxCnt)
		{
			var i=0;
			
			for(i=0;i<maxCnt;i++)
			{
				var ckType=document.getElementById('ckType_' + (i+1)); 
				var txFoll=document.getElementById('txFollow_' + (i+1)); 
				
				if(ckType!=null)
				{
					//if(ckType.checked)
					//{
						if(txFoll!=null)
						{
							if(isNaN(txFoll.value))			
							{
								txFoll.value='';		
								return false;
							}
						}
					//}
				}				
			}						
		}	
		function checkAll(obj)
		{
			if(obj!=null)
			{
				var i;
				var flag=0;
				for(i=0;i<obj.options.length;i++)
				{
					if(obj.options[i].selected==true)
					{
						
						flag=i;	
					}						
				}				
			}
			
			return flag;
		}
		
</script>  
  </head>
<BODY oncontextmenu = "return false;" leftMargin=0 topMargin=0 onload="top.topframe.main1.mousein = false;showScroll();">
	<div class=pageContent id=bodyHeight>
    <form id="Form1" method="post" runat="server">
<TABLE class=tableWidthHeader>
  <tr>
    <td colSpan=4 class="midcolorc" >
    <webcontrol:gridspacer id=grdSpacer runat="server"></webcontrol:GridSpacer>
    <asp:label Visible=False  id="lblMess" Runat="server" CssClass="errmsg"></asp:Label>   
    </TD></TR>
  <tr>
    <td class=midcolora><asp:label id="capModuleName" runat="server">Module Name</asp:label></TD>
    <td class=midcolora><asp:dropdownlist id="cmbModuleName" AutoPostBack="True" Runat="server"></asp:dropdownlist></TD>
    <td class=midcolora><asp:label id="capLineOfBusiness" runat="server">Line of Business</asp:label></TD>
    <td class=midcolora><asp:dropdownlist id="cmbDiaryType" AutoPostBack="True" Runat="server"></asp:dropdownlist></TD></TR>
  <TR>
    <TD id=trMessage colSpan=4 runat="server"></TD></TR>
  <tr>
    <td class=midcolorr colSpan=4>
	<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
	<input id="hidFlag" type="hidden" name="hidFlag" runat="server">
	<input id="hidDiaryID" type="hidden" name="hidDiaryID" runat="server">
	<input id="hidLOB" type="hidden" runat="server" NAME="hidLOB"> 
	<input id="hidCheckFlag" type="hidden" runat="server" NAME="hidCheckFlag">
	<input id="hidmsg1" type="hidden" runat="server" name="hidmsg1">
	<input id="hidmsg2" type="hidden" runat="server" name="hidmsg2">
	<input id="hidmsg3" type="hidden" runat="server" name="hidmsg3">
	<input id="hidmsg4" type="hidden" runat="server" name="hidmsg4">
	</TD></TR></TABLE></FORM></DIV>
	</BODY>
</HTML>
