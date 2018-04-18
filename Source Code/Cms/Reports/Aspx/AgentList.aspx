<%@ Page language="c#" Codebehind="AgentList.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.AgentList" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Agent List Report</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">		
		
		function GetValue(obj,type,defaultValue)
			{
				var sVal="";
				
				if(type=='D')
				{
					for(var i=0;i<obj.length;i++)
					{
						if(obj.options(i).selected)
						{
							if(obj.options(i).value=='All')
							{
								if((defaultValue!='undefined') && (defaultValue!=null)) 
									sVal=defaultValue+",";
								else
									sVal="0,";
								break;
							}
							else
								sVal += obj.options(i).value + ",";
						}
					}
					if(sVal!='')
						sVal=sVal.toString().substr(0,sVal.length-1);
				}
				else if(type=='T')	
				{
					sVal = obj.value;
				}
				
				return sVal;
			}
			
			
			function ShowReport()
			{			
				var Agency="";
				var HierarchySelected="";
										
				//Agency				= GetValue(document.getElementById('lstAgentName'),'D');
				Agency				= document.getElementById('hidAGENCY_ID').value;
				HierarchySelected	= GetValue(document.getElementById('lstHierarchy'),'T');
			
				if(HierarchySelected == "Yes")
					HierarchySelected ="1";
				else if(HierarchySelected == "No")
					HierarchySelected ="0";
								
				//alert('Agency '+ Agency);
				//alert('HierarchySelected '+ HierarchySelected);
				
				//if (Agency!= '')
				//{					
					var url="CustomReport.aspx?PageName=AgentList&Agencyid="+ Agency + "&HierarchySelectedid="+ HierarchySelected; 
					var windowobj = window.open(url,'AgentList','resizable=yes,scrollbar=yes,top=100,left=50')
					windowobj.focus();
				/*}
				else
				{
					alert("Please select Agency");
				}*/
				
				/* Testing
				if (Agency!= '')
				{
					//var url="CustomReport.aspx?PageName=AgentList"; 
					if (document.Form1)
					{
						document.getElementById("hidAGENCY_ID").value = Agency;
						document.getElementById("hidHierarchySelected").value = HierarchySelected;
						document.Form1.action = "CustomReport.aspx?PageName=AgentList";
						
						var windowobj = window.open("TrailBalance.aspx",'mywindow','resizable=yes,scrollbar=yes,top=100,left=50')
						document.Form1.target = "mywindow";
						
						//document.Form1.target = "_blank";
						document.Form1.submit();
					}
				}
				else
				{
					alert("Please select Agency");
				}*/
			}
			
			function OpenAgencyLookup()
			{			
				var url='<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>';
				OpenLookupWithFunction( url,"AGENCY_ID","Name","hidAGENCY_ID","txtAGENCY_NAME","Agency","Agency Names",'','PostFromLookup()');			
			}
		
			function PostFromLookup()
			{
				__doPostBack('hidAGENCY_ID','')
			}
			
			/*
									
			if (Agency!= '')
			{
				
				//alert(url);
				var windowobj = window.open(url,'mywindow','resizable=yes,scrollbar=yes,top=100,left=50')
				windowobj.focus();
				
				/*var url="CustomReport.aspx?PageName=AgentList"; 
				if (document.Form1)
				{
					document.getElementById("hidAgentid").value = Agency;
					document.getElementById("hidHierarchySelected").value = HierarchySelected;
					document.Form1.action = url;
					document.Form1.target = "_blank";
					document.Form1.submit();
				}			
			}*/
			
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" MS_POSITIONING="GridLayout" onload="top.topframe.main1.mousein = false;findMouseIn();">
		<form id="Form1" method="post" runat="server">
		  <DIV><webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu></DIV>	
			<!-- To add bottom menu -->
			
			<!-- To add bottom menu ends here -->
			<asp:Panel ID="Panel1" Runat="server">
				<TABLE class="tableWidth" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
					<TR>
						<TD class="pageHeader" id="tdClientTop" colSpan="4">
							<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer></TD>
					</TR>
					<TR>
						<TD class="headereffectCenter" colSpan="4">Agent List Report Selection Criteria
						</TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolora" width="36%">
							<asp:label id="lblAgent" runat="server">Select Agents</asp:label></TD>
						<TD class="midcolora" width="64%" colSpan="3">
							<asp:TextBox id="txtAGENCY_NAME" runat="server" ReadOnly="True" size="40"></asp:TextBox><IMG id="imgAGENCY_NAME" style="CURSOR: hand" onclick="OpenAgencyLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
								runat="server"></TD>
					</TR>
					<TR>
						<TD class="midcolora" vAlign="middle" colSpan="1">
							<asp:label id="lblHierarchy" runat="server">Default Hierarchy</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:DropDownList id="lstHierarchy" Runat="server" Width="83px"></asp:DropDownList></TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"><cmsb:cmsbutton class="clsbutton" id="btnReport" Runat="server" Text="Display Report"></cmsb:cmsbutton>
						</TD>
					</TR>
				</TABLE>
			</asp:Panel>
			<input id="hidAGENCY_ID" type="hidden" name="hidAGENCY_ID" runat="server"> <input id="hidHierarchySelected" type="hidden" name="hidHierarchySelected" runat="server">
		</form>
	</body>
</HTML>
