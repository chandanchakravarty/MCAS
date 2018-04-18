<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="~/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="DiarySetup.aspx.cs" AutoEventWireup="false" validateRequest="false" Inherits="Cms.CmsWeb.Maintenance.DiarySetup" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>DiarySetup</title>
<meta content="Microsoft Visual Studio .NET 7.1" name=GENERATOR>
<meta content=C# name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema>
<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
<script src="/cms/cmsweb/scripts/xmldom.js"></script>

<script src="/cms/cmsweb/scripts/common.js"></script>

<script src="/cms/cmsweb/scripts/form.js"></script>
<LINK href="/cms/cmsweb/css/menu.css" type=text/css rel=stylesheet >
<script src="../../cmsweb/scripts/AJAXcommon.js"></script>

<script language=javascript>
		var request;
		var response;
		var diaryType=document.getElementById ('cmbDiaryType');
		
		function diaryTypes() {
		   
			var module=document.getElementById('cmbModuleName');
			
			if(module.options[module.selectedIndex].value!="") 
			{
				return SendRequest(module.options[module.selectedIndex].value);
			}
			else
				clearSelect(diaryType);
		
		}
		
		function populateList(response) {
		    
			alert(response)
			var XmlDoc=new ActiveXObject("Microsoft.XMLDOM");
			XmlDoc.async=false;
			XmlDoc.loadXML(response);
			
			var opt;
			var diaryName=XmlDoc.getElementsByTagName("DiaryEntry");
			//var toDoType=diaryName[0].getElementsByTagName("TODOLISTTYPES");
			
			if(diaryName.length>0)
			{
				for(var i=0;i<diaryName.length;i++)
				{
					var textNode=document.createTextNode(diaryName[i].getAttribute('TYPEDESC')); 
					appendToSelect(diaryType,diaryName[i].getAttribute('TYPEID'),textNode)
					
				}
			}
			
		}
		
		function appendToSelect(select,value,content) {
		   
			var opt;
			opt=document.createElement("option"); 		
			opt.value=value;
			opt.appendChild(content);
			select.appendChild(opt);	
			
		}
		
		function clearToSelect(select)
		{
			select.options.length=1;
		}
		
			
		function SendRequest(ID)
		{
		    
			var objRequest = _CreateXMLHTTPObject();
			var Action = 'MOD';
			var ParamArray = new Array();
			obj1=new Parameter('POLICY_ID',ID);
			ParamArray.push(obj1);
			alert('in')
			_SendAJAXRequest(objRequest,'MOD',ParamArray,populateList);			
			
		}	
		
		
		function HideDisplay(cntID) {
		   
			var chk=document.getElementById('ckType_' + cntID );
			
			if(chk)
			{
				var pnl=document.getElementById('pRow_' + cntID );
				if(chk.checked)
				{
					if(pnl)
						pnl.style.display="inline";					
				}
				else
				{					
					if(pnl)
						pnl.style.display="none";
				}
			}
		}
		
		
		function checkMandatory() {
        // changes by praveer for itrack no 1482
        if (document.getElementById('cmbModuleName').options[document.getElementById('cmbModuleName').selectedIndex].value == 'Select Option' || document.getElementById('cmbModuleName').options[document.getElementById('cmbModuleName').selectedIndex].value == 'Selecione a opção' || document.getElementById('cmbDiaryType').options[document.getElementById('cmbDiaryType').selectedIndex].value == '')			
			{
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
			var arrDiary = document.getElementById("hidDiaryID").value.split(",")
			//var flagCheck=0;

			for (i = 0; i < arrDiary.length; i++) 
			{

			    var ckType = document.getElementById('ckType_' + (arrDiary[i]));
			    var objUGrp = document.getElementById('cmbUserGroup_' + (arrDiary[i]));
			    var objULst = document.getElementById('cmbUserList_' + (arrDiary[i])); 
				
                
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
						if(objUGrp.options.selectedIndex==-1 || objULst.options.selectedIndex==-1)
						{
							ugFlag=0;
							ulFlag=0;
							//alert(ugFlag + "---" + ulFlag + "---" )
							
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
				//alert(ugFlag + "---" + ulFlag + "---sddsf--" )
				if (document.getElementById('cmbModuleName').options[document.getElementById('cmbModuleName').selectedIndex].innerHTML != "Customer") {
				    if (ugFlag <= 0 && ulFlag <= 0) {
				        alert(document.getElementById("hidmsg2").value);
				        return false;
				    }
				}
			}
			
			
			
			var txResult=checkFollowUP(maxCount);

			if (txResult == false)
			{
			    alert(document.getElementById("hidmsg3").value);
				return false;
			}
		
			
			//alert(document.getElementById("hidCheckFlag").value)
			if (document.getElementById('cmbModuleName').options[document.getElementById('cmbModuleName').selectedIndex].innerHTML != "Customer") {

			    if (flagCheck <= 0) {
			        if (document.getElementById("hidCheckFlag").value == "") {
			            alert(document.getElementById("hidmsg4").value);
			            return false;
			        }
			    }   
			}
			/*if(document.getElementById("hidCheckFlag").value!="0")
			{
				alert("Please check at least one check box");
				return false;
			}*/
		
			return true;
		}
		
		function checkFollowUP(maxCnt) {
		    
			var i=0;
			
			for(i=0;i<maxCnt;i++)
			{
				var ckType=document.getElementById('ckType_' + (i+1)); 
				var txFoll=document.getElementById('txFollow_' + (i+1)); 
				
				if(ckType!=null)
				{
					//if(ckType.checked)
					//{
				    if (txFoll != null && txFoll.value!='')
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
		
		
		function checkAll(obj) {
		     
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
</HEAD>
<BODY  leftMargin=0 topMargin=0 onload="top.topframe.main1.mousein = false;showScroll();">
		<!-- To add bottom menu -->
<div class=pageContent id=bodyHeight>
<FORM id=Form1 method=post runat="server">
<TABLE class=tableWidthHeader>
  <tr>
    <td colSpan=4 class="midcolorc" >
    <webcontrol:gridspacer id=grdSpacer runat="server"></webcontrol:gridspacer>
    <asp:label Visible=False  id=lblMess Runat="server" CssClass="errmsg"></asp:label>   
    </td></tr>
  <tr>
    <td class=midcolora><asp:label id="capModuleName" runat="server">Module Name</asp:label></td>
    <td class=midcolora><asp:dropdownlist id="cmbModuleName" AutoPostBack="True" Runat="server"></asp:dropdownlist></td>
    <td class=midcolora><asp:label id="capDiaryListType" runat="server">Diary List Type</asp:label></td>
    <td class=midcolora><asp:dropdownlist id="cmbDiaryType" AutoPostBack="True" Runat="server"></asp:dropdownlist></td></tr>
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
 </td></tr></TABLE></FORM></div>
	</BODY>
</HTML>
