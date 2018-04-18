<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="CopyPolicyToNewClient.aspx.cs" AutoEventWireup="false" Inherits="Policies.Aspx.CopyPolicyToNewClient" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title><%=head%></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		function SetTitle()
		{
			document.title=document.getElementById('hidTitle').value;
		}		
		function onRowClicked(num,msDg )
		{
		     
			rowNum = num;
			if(parseInt(num)==0)
				strXML="";
			populateXML(num,msDg)
			//alert(strXML);
			populateFormData(strXML,COPYPOLICY);	
			GetCSRProducer();
			//alert('pk=' + primaryKeyValues); 
			//alert('querys='+ locQueryStr); 
			//changeTab(0, 0);


}

 	
		function fillCSRproducer(strCSRXML)
		{
		     
			var objXmlHandler = new XMLHandler();
			var tree1 = objXmlHandler.quickParseXML(strCSRXML);
			if(tree1.childNodes.length==0)
			{
			document.getElementById('cmbCSR').length=0;
			document.getElementById('cmbPRODUCER').length=0;
				return;
			}	
			var tree = objXmlHandler.quickParseXML(strCSRXML).childNodes[0];
			//adding a blank option
				oOption = document.createElement("option");
				oOption.value = "";
				oOption.text = "";
				oOptionP = document.createElement("option");
				oOptionP.value = "";
				oOptionP.text = "";
				document.getElementById('cmbCSR').length=0;
				document.getElementById('cmbCSR').add(oOption);
				document.getElementById('cmbPRODUCER').length=0;
				document.getElementById('cmbPRODUCER').add(oOptionP);
				 
				for(i=0; i<tree.childNodes.length; i++)
				{
					nodValue = tree.childNodes[i].getElementsByTagName('Table');
					
					if (nodValue != null)
					{
									
						if (nodValue[0].firstChild == null)
							continue
						
						//if (nodValue[0].firstChild.text == policyTerm || nodValue[0].firstChild.text == 0 )
						//{
								
							userID = tree.childNodes[i].getElementsByTagName('USER_ID');
							userDesc = tree.childNodes[i].getElementsByTagName('USER_NAME_ID');
							userIsAcrive = tree.childNodes[i].getElementsByTagName('IS_ACTIVE');
							
							if (userID != null && userDesc != null)
							{
								if (userID[0] != null &&  userDesc[0] != null )
								{
									oOption = document.createElement("option");
									oOption.value = userID[0].firstChild.text;
									oOption.text = userDesc[0].firstChild.text;
									if(userIsAcrive[0].firstChild.text=='N')
										oOption.style.color='#ff0000'; 
									document.getElementById('cmbCSR').add(oOption);
																		
									oOptionP = document.createElement("option");
									oOptionP.value = userID[0].firstChild.text;
									oOptionP.text = userDesc[0].firstChild.text;
									if(userIsAcrive[0].firstChild.text=='N')
										oOptionP.style.color='#ff0000'; 
									document.getElementById('cmbPRODUCER').add(oOptionP);
								}
							} 
						//} 
					} 
				}   	
				
		}		
		//for CSR Details
		function GetCSRProducer_Old()
		{ 
			serviceInitialiseThis() ;
			GlobalError=true;
			if(document.getElementById('hidAGENCY_ID').value!="" && document.getElementById('hidAGENCY_ID').value!="0" )
			{ 
					var intAgencyID = document.getElementById('hidAGENCY_ID').value;
					
					var co=myTSMain1.createCallOptions();
					co.funcName = "FetchAgencyCSRProducer";
					co.async = false;
					co.SOAPHeader= new Object();
					var oResult = myTSMain1.FetchCSR.callService(co,intAgencyID);
					//alert(oResult);
					handleResult(oResult);	
					if(GlobalError)
					{
						return false;
					}
					else
					{
						return true;
					}
			}
			else 
				return true;		
		}
		function handleResult(res) 
		{
			if(!res.error)
			{
			if (res.value!="" && res.value!=null ) 
				{
					GlobalError=false;
					fillCSRproducer(res.value)
				}
				else
				{
					GlobalError=true;
				}
			}
			else
			{
				GlobalError=true;		
			}
		}
		
		///////AJAX CSR/////////////////
		function GetCSRProducer()
		{ 
			GlobalError=true;
			if(document.getElementById('hidAGENCY_ID').value!="" && document.getElementById('hidAGENCY_ID').value!="0" )
			{
			    var intAgencyID = document.getElementById('hidAGENCY_ID').value;
			    var LOBID = document.getElementById('hidLOBID').value;
					var result = CopyPolicyToNewClient.AjaxFetchAgencyCSRProducer(intAgencyID, LOBID);
					return AjaxCallFunction_CallBack(result);	
				
			}
			
		}
		
		function AjaxCallFunction_CallBack(response)
		{		
		 
		  if(document.getElementById('hidAGENCY_ID').value!="" && document.getElementById('hidAGENCY_ID').value!="0" )
			{ 
					handleResult(response);
					if(GlobalError)
					{
						return false;
					}
					else
					{
						return true;
					}
			}
			else 
				return true;		
		}
		
		////END AJAX///////////////////
		
		function ConfirmTransfer()
		{
			if(document.getElementById('hidCUSTOMER_ID').value!="" && document.getElementById('hidCUSTOMER_ID').value!="0")
			{
			    //alert(document.getElementById('hidCUSTOMER_ID').value);

			    var flag = confirm("<%=strMsg%>");
				//alert(flag);
				if(!flag)
				{
				//alert('in false');
				return false;
				}
				else
				{
				 DisableButton(document.getElementById('btnSubmit'));
				return true;
				}
			}	
			else
			{
				alert("<%=str%>");
				return false;
			}	
		}
		function closewindow()
		{
		window.close();
		}
		function refreshParent()
		{
		    
		// pk;
		 if (document.getElementById('hidCUSTOMER_ID').value!="" && document.getElementById('hidCUSTOMER_ID').value!="0" && document.getElementById('hidNEW_POLICY_ID').value!="0" && document.getElementById('hidNEW_POLICY_ID').value!="" ) {
		      
			 var CustomerID='';
				if ( document.getElementById('hidCUSTOMER_ID')!=null)
				{
					CustomerID= document.getElementById('hidCUSTOMER_ID').value;
				}
				var AppID='';
				if (document.getElementById('hidAPP_ID')!=null)
				{
					AppID=document.getElementById('hidAPP_ID').value;
				}
				var AppVersionID='';
				if (document.getElementById('hidAPP_VERSION_ID')!=null)
				{
					AppVersionID=document.getElementById('hidAPP_VERSION_ID').value;
				}
				
				var LobID='';
				if (document.getElementById('hidLOBID')!=null)
				{
					LobID=document.getElementById('hidLOBID').value;
				}
				var PolicyID = '';
				var PolicyVersionID = '';
				if (document.getElementById('hidNEW_POLICY_ID')!=null)
				{
					PolicyID=document.getElementById('hidNEW_POLICY_ID').value;
				}
				if (document.getElementById('hidNEW_POLICY_VERSION_ID')!=null)
				{
					PolicyVersionID= document.getElementById('hidNEW_POLICY_VERSION_ID').value;
				}
		 //window.parent.execScript(eval('ResetForm1()'));
		 //eval('window.opener.ResetForm1()');
		 //window.parent.document.location.href="PolicyInformation.aspx?CUSTOMER_ID="+CustomerID+"&APP_ID="+AppID+"&APP_VERSION_ID="+AppVersionID+"&POLICY_LOB="+ LobID  +"&POLICY_ID="+ PolicyID + "&POLICY_VERSION_ID="+ PolicyVersionID +  "&transferdata=";
			var loc=window.opener.parent.location.href;   
			var	ParentIndex= loc.indexOf('?');
			var ParentLoc = loc.substring(0, ParentIndex + 1);
			var Msg = document.getElementById('hidCopyMsg').value;
			ParentLoc=ParentLoc + "customer_id=" + CustomerID + "&policy_id=" + PolicyID + "&policy_version_id=" + PolicyVersionID + "&app_id=" + AppID + "&app_version_id=" + AppVersionID + "&"
			window.opener.parent.location=ParentLoc.toString();
			alert(Msg);
			document.getElementById('btnClose').disabled=false;
			document.getElementById('btnSubmit').disabled=true;
			window.close();
		 }
		
		}
		function getProducerID()
		{
			var selectedIndx=document.getElementById('cmbPRODUCER').options.selectedIndex;
			if(selectedIndx!=0 && selectedIndx!=-1)
				document.getElementById('hidPRODUCER_ID').value= document.getElementById('cmbPRODUCER').options[selectedIndx].value;
			else
				document.getElementById('hidPRODUCER_ID').value="0";
			//alert(document.getElementById('hidPRODUCER_ID').value)
		}
		
		function getCSRID()
		{
		var selectedIndex=document.getElementById('cmbCSR').options.selectedIndex;
		if(selectedIndex!=0 && selectedIndex!=-1)
			document.getElementById('hidCSR_ID').value= document.getElementById('cmbCSR').options[selectedIndex].value;
		else
			document.getElementById('hidCSR_ID').value="0";
		//alert(document.getElementById('hidCSR_ID').value);
		
		}
		function pageint() {
		    ChangeColor();
		    ApplyColor();
		    showScroll();
		    refreshParent();
		}
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" onload="pageint();">
		<div class="pageContent" id="bodyHeight">
			<form id="COPYPOLICY" method="post" runat="server">				
				<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0">
					<tr>
						<td>
							<table cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
								<TBODY>
									<tr>
										<td>&nbsp;</td>
									</tr>
									<TR class="midcolora">
										<TD class="headereffectCenter"><asp:label id="lblHeader" Runat="server">Copy Policy</asp:label></TD>
									</TR>
									<TR>
										<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
									</TR>
									<tr>
										<td id="tdGridHolder"><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer><asp:placeholder id="GridHolder" runat="server"></asp:placeholder></td>
									</tr>
									<tr>
										<td><webcontrol:gridspacer id="Gridspacer" runat="server"></webcontrol:gridspacer></td>
									</tr>
					</tr>
				</table>
				<table class="tableWidth" cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
					<TR>
						<td class="midcolorc" align="center" colSpan="4"><asp:label id="lblcustomermsg" runat="server" Visible="True" CssClass="errmsg">Selected Customer Details</asp:label></td>
					</TR>
					<tr>
						<TD class="midcolora" width="18%"><asp:label id="lblCustomerCode" runat="server">Customer Code</asp:label></TD>
						<TD class="midcolora" width="32%"><asp:label id="lblCUSTOMER_CODE" runat="server"><b></b></asp:label></TD>
						<TD class="midcolora" width="18%"><asp:label id="lblCustomerName" runat="server">Customer Name</asp:label></TD>
						<TD class="midcolora" width="32%"><asp:label id="lblCUSTOMER_NAME" runat="server"><b></b></asp:label></TD>
					</tr>
					<tr>
						<TD class="midcolora" width="18%"><asp:label id="lblAgency" runat="server">Agency</asp:label></TD>
						<TD class="midcolora" width="32%"><asp:label id="lblAGENCY_DISPLAY_NAME" runat="server"><b></b></asp:label></TD>
						<TD class="midcolora" width="18%"><asp:label id="lblStateName" runat="server">State</asp:label></TD>
						<TD class="midcolora" width="32%"><asp:label id="lblSTATE_NAME" runat="server"><b></b></asp:label></TD>
					</tr>
					<TR>
					<TD class="midcolora" width="18%"><asp:label id="lblCSR" runat="server">CSR</asp:label></TD>
					<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCSR" runat="server" AutoPostBack="False" onclick="getCSRID();"></asp:dropdownlist> </TD>
					<TD class="midcolora" width="18%"><asp:label id="lblPRODUCER" runat="server">Producer</asp:label></TD>
					<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPRODUCER" runat="server" AutoPostBack="False" onclick="getProducerID();"></asp:dropdownlist></TD>
					
					<TR>
						<TD class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSubmit" runat="server" Text="Copy Policy"></cmsb:cmsbutton></TD>
						<TD class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnClose" runat="server" Visible="True" Text="Close"></cmsb:cmsbutton></TD>
					</TR>
					<tr>
						<td class="midcolorr"><INPUT id="hidCustomerID" type="hidden" value="0" name="hidCustomerID" runat="server">
							<INPUT id="hidPolicyVersionID" type="hidden" value="0" name="hidPolicyVersionID" runat="server">
							<INPUT id="hidPolicyID" type="hidden" value="0" name="hidPolicyID" runat="server">
							<INPUT id="hidCalledFrom" type="hidden" value="0" name="hidCalledFrom" runat="server">
							<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
							<INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
							<INPUT id="hidSTATE_CODE" type="hidden" value="0" name="hidSTATE_CODE" runat="server">
							<INPUT id="hidNEW_POLICY_VERSION_ID" type="hidden" value="0" name="hidNEW_POLICY_VERSION_ID" runat="server"> 
							<INPUT id="hidNEW_POLICY_ID" type="hidden" value="0" name="hidNEW_POLICY_ID" runat="server"> 
							<INPUT id="hidAPP_ID" type="hidden" value="0" name="hidAPP_ID" runat="server">
							<INPUT id="hidAPP_VERSION_ID" type="hidden" value="0" name="hidAPP_VERSION_ID" runat="server">
							<INPUT id="hidLOBID" type="hidden" value="0" name="hidLOBID" runat="server"> 
							<INPUT id="hidAGENCY_ID" type="hidden" value="0" name="hidAGENCY_ID" runat="server"> 
							<INPUT id="hidCSR_ID" type="hidden" value="0" name="hidCSR_ID" runat="server"> 
							<INPUT id="hidPRODUCER_ID" type="hidden" value="0" name="hidPRODUCER_ID" runat="server"> 
							<INPUT id="hidTitle" type="hidden" value="0" name="hidTitle" runat="server">
							<INPUT id="hidlocQueryStr" type="hidden" value="0" name="hidlocQueryStr" runat="server">
							<INPUT id="hidMsg" type="hidden" value="" name="hidMsg" runat="server">
							<INPUT id="hidCopyMsg" type="hidden" value="" name="hidCopyMsg" runat="server">
						</td>
					</tr>
				</table>
				</TD></TR></TBODY></TABLE></form>
		</div>
	</body>
</HTML>
