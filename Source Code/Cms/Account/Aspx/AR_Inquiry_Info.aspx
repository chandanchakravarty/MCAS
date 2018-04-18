<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Page language="c#" Codebehind="AR_Inquiry_Info.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.AR_Inquiry_Info" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>Ebix Advantage -Consulta Financeira</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="../../cmsweb/scripts/form.js"></script>
		<script src="../../cmsweb/scripts/AJAXcommon.js"></script>
		<script language="javascript">
		
			function ValidDescription(msg)
			{
				var desc = msg;
				alert(desc.replace(/'/g,'\''));
			}
			function ShowAssoc(obj)
			{	
				document.getElementById('txtPolicyNo').value=obj;
				__doPostBack('btnSave',null);
				return false;
			}
			//changes by uday on 18 oct
			// function SetFocus()
            // {
            //      document.AR_INQUIRY['txtPolicyNo'].focus();
            // }
			//end
			function OpenPolicyLookup()
			{
					var url='<%=URL%>';
					var customer_id='<%=callingCustomerId%>';
					var agency_id ="'<%=callingAgencyId%>'";
					var idField,valueField,lookUpTagName,lookUpTitle;
					if (agency_id.toUpperCase() != '<%=CarrierSystemID.ToUpper() %>')//"'W001'")
					{
					
						if(customer_id == '')
						{
						    var strPolicy = document.getElementById('hidPolicy').value;
							OpenLookupWithFunction(url,'POL_INFORM','POLICY_NUMBER','hidPOLICYINFO','txtPolicyNo','ARREPORT4AGENCY',strPolicy,"@AGENCY_ID="+agency_id,'splitPolicy()');
						}
						else
						{
						    //OpenLookupWithFunction(url, 'POL_INFORM', 'POLICY_NUMBER', 'hidPOLICYINFO', 'txtPolicyNo', 'ARREPORT4AGENCYCUST', 'Policy', '@AGENCY_ID=' + agency_id + ';@CUSTOMER_ID=' + customer_id, 'splitPolicy()'); changed by sonal to show all polocise for particular customer with agency code reference
						    OpenLookupWithFunction(url, 'POL_INFORM', 'POLICY_NUMBER', 'hidPOLICYINFO', 'txtPolicyNo', 'ARREPORT4AGENCYCUST', strPolicy , '@CUSTOMER_ID=' + customer_id, 'splitPolicy()');
										
						}
					}
					else
					{
					
						if(customer_id == '')
							OpenLookupWithFunction(url,'POL_INFORM','POLICY_NUMBER','hidPOLICYINFO','txtPolicyNo','ARREPORT',strPolicy ,'','splitPolicy()');
						else
						{
							OpenLookupWithFunction(url,'POL_INFORM','POLICY_NUMBER','hidPOLICYINFO','txtPolicyNo','ARREPORT4CUST',strPolicy ,'@CUSTOMER_ID='+customer_id,'splitPolicy()');
							
						}
					}
					//END : CHECK FOR AGENCY		
				}
				    
				function splitPolicy()
				{
				//alert(document.getElementById('hidPOLICYINFO').value)
				}
				//Disable Menu Items when called from TopFrame : Praveen kasana'
				function policyfocus()
				{
						document.getElementById('txtPolicyNo').focus();
						document.getElementById('btnClose').style.display = "none";				
				}
				function MenuRequired()
				{
					var val = '<%=calledfrom%>';	
					// alert(val)			
					if(val != 'TopFrame' && val != 'InCLT')					
					{
					    
						//setfirstTime();
						//top.topframe.main1.activeMenuBar='1,2'; //added by uday
						top.topframe.main1.mousein = false;
						findMouseIn();
						//alert('try')
						document.getElementById('txtPolicyNo').focus();
						document.getElementById('btnClose').style.display = "none";
																
					}
					else if(val == 'InCLT')
					{
						top.topframe.main1.activeMenuBar='1,2';
						top.topframe.main1.mousein = false;
						findMouseIn();
						setTimeout('policyfocus()',500);
						//alert('try1')
					}
					else
					{
						document.getElementById('txtPolicyNo').focus();					
					}
				}
				
				function FetchPolInfoXML(PolNum)
				{					
					var ParamArray = new Array();
					obj1=new Parameter('POLICY_NUMBER',PolNum);
					ParamArray.push(obj1);
					var objRequest = _CreateXMLHTTPObject();
					var Action = 'AI_INFO';
					var Message = document.getElementById('hidMessage').value;
					//If else Condition Added For according to PRAVEEN KASANA mail.				
					if(document.getElementById('txtPolicyNo').value.length < 20)
					{
					    alert(Message);
					     document.getElementById('txtPolicyNo').value = "";
					     return false;
					}
					else
					{					  
						_SendAJAXRequest(objRequest,'AI_INFO',ParamArray,CallbackFun);
					}
				}
				
				//Function  Added For according to PRAVEEN KASANA mail.
				function save()
				{
				   if(document.getElementById('txtPolicyNo').value.length < 20)
				   {
				     return false;
				   }
				}
				
				
				function CallbackFun(AJAXREsponse)
				{

				    var called = '<%=calledfrom%>';
				    var Message = document.getElementById('hidMessage').Value;
				    var Message2 = document.getElementById('hidMessage2').Value;
					if(AJAXREsponse == "0" && document.getElementById('txtPolicyNo').value != "")
					{
						if(called == 'InCLT')
						{
						alert(Message2);
						document.getElementById('txtPolicyNo').value = "";
						document.getElementById('txtPolicyNo').focus();
						document.getElementById('tbArReport').style.display = 'none';
						//document.getElementById('tbArReport').setAttribute('visible',false);
						return false;
					    }
					    else {					    
					    alert(Message);
						document.getElementById('txtPolicyNo').value = "";
						document.getElementById('txtPolicyNo').focus();
						document.getElementById('tbArReport').style.display = 'none';
						//document.getElementById('tbArReport').setAttribute('visible',false);
						return false;
					    }
					}
									
					document.getElementById('hidPOLICYINFO').value = AJAXREsponse;
				}
				//New function AJAX call /When user press Enter 
				function FetchPolInfoXMLKeyEnter(PolNum)
				{
					//alert();
					var ParamArray = new Array();
					obj1=new Parameter('POLICY_NUMBER',PolNum);
					ParamArray.push(obj1);
					var objRequest = _CreateXMLHTTPObject();
					var Action = 'AI_INFO';
					
					_SendAJAXRequest(objRequest,'AI_INFO',ParamArray,CallbackFunEnter);
					
				}
				function CallbackFunEnter(AJAXREsponse)
				{	
					
					if(AJAXREsponse == "0" && document.getElementById('txtPolicyNo').value != "")
					{
						//alert('Policy Number invalid for this customer');
						//document.getElementById('txtPolicyNo').value = "";
						//document.getElementById('txtPolicyNo').focus();
						//document.getElementById('tbArReport').style.display = 'none';
						//document.getElementById('tbArReport').setAttribute('visible',false);
						return;
					}
									
					document.getElementById('hidPOLICYINFO').value = AJAXREsponse;
				}
			
		</script>
		<!--style="OVERFLOW: scroll" -->
  </HEAD>
	<body  onload="ApplyColor();ChangeColor();MenuRequired();" scroll="Yes" 
		MS_POSITIONING="GridLayout">
		<webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!--<div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 100%">-->
			<form id="AR_INQUIRY" method="post" runat="server">
				<table class="tableWidth" cellSpacing="1" cellPadding="0" align="center" border="0">
					<tr>
						<td><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer></td>
					</tr>
					<tr>
						<td class="headereffectCenter" colSpan="4">
                            <asp:Label ID="lblInquiryinfo" runat="server" Text="Label"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="midcolorc" align="center" colSpan="4"><asp:label id="lblMessage" Runat="server" CssClass="errmsg"></asp:label></td>
					</tr>
					<tr>
						<td class="midcolora" width="18%">
                            <asp:Label ID="lblPolicyNumber" runat="server" Text="Label"></asp:Label><span class="mandatory">*</span></td>
						<td class="midcolora" width="32%"><asp:textbox id="txtPolicyNo" Runat="server" MaxLength="21" size="30" onchange="FetchPolInfoXML(this.value);" onkeyup="FetchPolInfoXMLKeyEnter(this.value);"></asp:textbox><span id="spnPOLICY_NO" runat="server"><IMG id="imgPOLICY_NO" style="CURSOR: hand" onclick="OpenPolicyLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
									runat="server"></span><br>
							<asp:requiredfieldvalidator id="rfvPolicyNo" Runat="server" Display="Dynamic" ControlToValidate="txtPolicyNo"></asp:requiredfieldvalidator></td>
						<td class="midcolora" colSpan="2">
                            <asp:Label ID="lblTransaction" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;&nbsp;
							<asp:dropdownlist id="cmbTransYr" runat="server" Height="16px" Width="64px"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td class="midcolorr" align="right" width="100%" colSpan="4"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Show Report"></cmsb:cmsbutton></td>
					</tr>
				</table>
				<table class="tableWidth" cellSpacing="1" cellPadding="0" align="center" border="0" id="tbArReport">
					<tr>
						<td id="tdArReport" width="100%" colSpan="5" runat="server"></td>
					</tr>
					<tr>
						<td><asp:Button ID="btnClose" Runat="server" Text="Close" CssClass="clsButton"></asp:Button></td>
						<td colspan="9" align="right">
						<!--<input type ="button"  width="100%" class="clsButton" value = "Print" onClick = "window.print()">-->
						<!--Buttton Print made of cms type by Sibin on 27 Oct 08-->
						    <cmsb:cmsbutton class="clsButton" id="btnPrint" runat="server" Text="Print"></cmsb:cmsbutton>
						</td>
					</tr>
					<tr>
						<td><webcontrol:gridspacer id="Gridspacer1" runat="server"></webcontrol:gridspacer></td>
					</tr>
				</table>
				<input id="hidPOLICYINFO" type="hidden" name="hidPOLICYINFO" runat="server">		
				<input id="hidCUSTOMER_ID" type="hidden" name="hidCUSTOMER_ID" runat="server">		
				<input id="hidPOLICY_ID" type="hidden" name="hidPOLICY_ID" runat="server">		
				<input id="hidPOLICY_VERSION_ID" type="hidden" name="hidPOLICY_VERSION_ID" runat="server">				
				<input id="hidPOLICY_NO" type="hidden" name="hidPOLICY_NO" runat="server">	
				<input id="hidPolicy" type="hidden" runat="server" />	
                <input id="hidMessage" type="hidden" name="hidMessage" runat="server">
                <input id="hidMessage2" type="hidden" name="hidMessage2" runat="server">			               
			</form>
		<!--</div>-->
	</body>
</HTML>
