<%@ Page language="c#" Codebehind="AddCustomerNotes.aspx.cs" AutoEventWireup="false" Inherits="Cms.Client.Aspx.AddCustomerNotes" validateRequest="false"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>CLT_CUSTOMER_NOTES</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script language="javascript">
		function AddData() {
		   // debugger;
			ChangeColor();
			DisableValidators();
			document.getElementById('hidNOTES_ID').value	=	'New';
			document.getElementById('txtNOTES_SUBJECT').value  = '';
			document.getElementById('cmbNOTES_TYPE').options.selectedIndex = 0;
			//document.getElementById('cmbNOTES_TYPE').focus();
			//document.getElementById('cmbPOLICY_ID').options.selectedIndex = -1;
			//document.getElementById('cmbPOLICY_ID').focus();
			if (document.getElementById('hidClaimID').value == "")
			document.getElementById('cmbCLAIMS_ID').options.selectedIndex = -1;
			//document.getElementById('cmbCLAIMS_ID').focus();			
			document.getElementById('txtNOTES_DESC').value  = '';
			
			
				
			//document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
			//if(document.getElementById('txtNOTES_SUBJECT'))
			//document.getElementById('txtNOTES_SUBJECT').focus();
			//document.getElementById('txtFOLLOW_UP_DATE').value = <=System.DateTime.Today.Date.ToShortDateString();%>;
			var dt =new Date();
				 //document.getElementById('txtFOLLOW_UP_DATE').value = dt.getMonth()+1 +  '/' + dt.getDate() + '/' + dt.getFullYear(); 
			document.getElementById('cmbDIARY_ITEM_REQ').options.selectedIndex = 0;
			document.getElementById('spnFOLLOW_UP_DATE').style.display = "none";
		}
		function populateXML()
		{
			//debugger;
			if(document.getElementById('hidFormSaved').value == '0')
			{
			    var tempXML;
				
				if(document.getElementById('hidOldData').value != "")
				{

					populateFormData(document.getElementById('hidOldData').value, CLT_CUSTOMER_NOTES);
					if (document.getElementById('cmbDIARY_ITEM_REQ').options[document.getElementById('cmbDIARY_ITEM_REQ').options.selectedIndex].value =='1')
					{
					   //alert('in yes');
						document.getElementById('txtFOLLOW_UP_DATE').style.display='inline';	
						document.getElementById('capFOLLOW_UP_DATE').style.display='inline';
						document.getElementById('imgFOLLOW_UP_DATE').style.display = 'inline'; 
						
						if (document.getElementById('txtFOLLOW_UP_DATE') != null)
							{
							document.getElementById('rfvFOLLOW_UP_DATE').setAttribute("enabled",true);
							
							}
							
							if (document.getElementById('spnFOLLOW_UP_DATE') != null)
							{
								document.getElementById('spnFOLLOW_UP_DATE').style.display = "inline";
				}
				document.getElementById('txtFOLLOW_UP_DATE').value = document.getElementById('hidFollowUpDate').value;
					}
					
					
					else
					{
					
							if (document.getElementById('rfvFOLLOW_UP_DATE') != null)
							{
								document.getElementById('rfvFOLLOW_UP_DATE').setAttribute("enabled",false);
								document.getElementById('rfvFOLLOW_UP_DATE').style.display = 'none';
							}
							//if (document.getElementById('spnFOLLOW_UP_DATE') != null)    //comment praveen kumar
							//{
								document.getElementById('spnFOLLOW_UP_DATE').style.display = "none";
							//}
							
					}
				}
				else
				{
					AddData();
				}
			}
			//alert('in out of if');
			if (document.getElementById('cmbDIARY_ITEM_REQ').options[document.getElementById('cmbDIARY_ITEM_REQ').options.selectedIndex].value == '1')
			{
				//alert('in yes');
				document.getElementById('txtFOLLOW_UP_DATE').style.display='inline';	
				document.getElementById('capFOLLOW_UP_DATE').style.display='inline';
				document.getElementById('imgFOLLOW_UP_DATE').style.display='inline'; 
				if (document.getElementById('txtFOLLOW_UP_DATE') != null)
				{
					document.getElementById('rfvFOLLOW_UP_DATE').setAttribute("enabled",true);
					//document.getElementById('rfvFOLLOW_UP_DATE').style.display = 'none';
				}
				
				
				if (document.getElementById('spnFOLLOW_UP_DATE') != null)
				{
					document.getElementById('spnFOLLOW_UP_DATE').style.display = "inline";
				}		
					
			}
			else
			{
			
				
				if (document.getElementById('rfvFOLLOW_UP_DATE') != null)
				{
					document.getElementById('rfvFOLLOW_UP_DATE').setAttribute("enabled",false);
					document.getElementById('rfvFOLLOW_UP_DATE').style.display = 'none';
					document.getElementById('revFOLLOW_UP_DATE').setAttribute("enabled", false);
					document.getElementById('revFOLLOW_UP_DATE').style.display = 'none';
					document.getElementById('csvFOLLOW_UP_DATE').setAttribute("enabled", false);
					document.getElementById('csvFOLLOW_UP_DATE').style.display = 'none';
					
				}
				//if (document.getElementById('spnFOLLOW_UP_DATE') != null)    //comment praveen kumar
				//{
				document.getElementById('spnFOLLOW_UP_DATE').style.display = "none";
				
				
				//}	
		
				
				
					
			}
			
			return false;
		}
		
		
		function ChkDate(objSource , objArgs)
		{
			
			var expdate=document.getElementById('txtFOLLOW_UP_DATE').value;
			//alert(expdate);
			if(expdate!="" || expdate!=null)
			{
				objArgs.IsValid = DateComparer(expdate,"<%=System.DateTime.Now.AddDays(-1)%>",'mm/dd/yyyy');   //jsaAppDtFormat
			}
		}
		
		
		function ShowFOLLOW_UP_DATE()
		{
		    
			
			if  (document.getElementById('cmbDIARY_ITEM_REQ').options[document.getElementById('cmbDIARY_ITEM_REQ').options.selectedIndex].value == '1')
			{
				//document.getElementById('tdFollowupCap').style.display='inline';	
				//document.getElementById('tdFollowuptxt').style.display='inline';	
				document.getElementById('txtFOLLOW_UP_DATE').style.display='inline';	
				document.getElementById('capFOLLOW_UP_DATE').style.display='inline';
				document.getElementById('imgFOLLOW_UP_DATE').style.display = 'inline';
				document.getElementById('rfvFOLLOW_UP_DATE').setAttribute("enabled", true);
				document.getElementById('revFOLLOW_UP_DATE').setAttribute("enabled", true);
				document.getElementById('csvFOLLOW_UP_DATE').setAttribute("enabled", true);
				//   Added by praveen kumar 0 8-12-08 for itrack 5138
				if (document.getElementById('txtFOLLOW_UP_DATE') != null)
				{
				    
				document.getElementById('rfvFOLLOW_UP_DATE').style.display = 'none';
				}
				
		         
				if (document.getElementById('txtFOLLOW_UP_DATE') != "")
				{
					document.getElementById('spnFOLLOW_UP_DATE').style.display = "inline";
				}
					//  End Praveen Kumar
				if (document.getElementById('rfvFOLLOW_UP_DATE') != null)
					{
						document.getElementById('rfvFOLLOW_UP_DATE').setAttribute('enabled',true);
						if (document.getElementById('rfvFOLLOW_UP_DATE').isvalid == false)
							document.getElementById('rfvFOLLOW_UP_DATE').style.display = 'inline';
					} 	
		         
				//if (document.getElementById('spnFOLLOW_UP_DATE') != null)  //comment praveen kumar
				//	{
						document.getElementById('spnFOLLOW_UP_DATE').style.display = "inline";
				//	}
			}  
			else
		    {
				  //document.getElementById('tdFollowupCap').style.display='none';	
				  //document.getElementById('tdFollowuptxt').style.display='none';	
				  
				  document.getElementById('txtFOLLOW_UP_DATE').style.display='none';
		          document.getElementById('capFOLLOW_UP_DATE').style.display='none';
		          document.getElementById('imgFOLLOW_UP_DATE').style.display = 'none';
		          
		            
				  if (document.getElementById('rfvFOLLOW_UP_DATE') != null)
					{
						document.getElementById('rfvFOLLOW_UP_DATE').setAttribute("enabled",false);
						document.getElementById('rfvFOLLOW_UP_DATE').setAttribute("isvalid",true);
						document.getElementById('rfvFOLLOW_UP_DATE').style.display = 'none';
						
					}
				 //if (document.getElementById('spnFOLLOW_UP_DATE') != null)    //comment praveen kumar
				//	{
						document.getElementById('spnFOLLOW_UP_DATE').style.display = "none";
				//	}	
		          
		    
		     }     
		}
		
////		function ChkTextAreaLength(source, arguments)
////			{
////				var txtArea = arguments.Value;
////				if(txtArea.length > 500 ) 
////				{
////					arguments.IsValid = false;
////					return;   // invalid userName
////				}
////			}	
			function showPageLookupLayer(controlId)
					{
				var lookupMessage;						
				switch(controlId)
				{
					case "cmbNOTES_TYPE":
						lookupMessage	=	"";
						break;
					default:
						lookupMessage	=	"Look up code not found";
						break;
				}
				
				showLookupLayer(controlId,lookupMessage);							
			}
			function CheckForClaim()
			{				
				if(document.getElementById('hidClaimsPopUp').value=="1" && document.getElementById('hidCalledFrom').value=="CLAIMS")
				{
					SelectComboOption("cmbNOTES_TYPE",document.getElementById("hidPinkSlipNotesTypeID").value);					
					document.getElementById('cmbNOTES_TYPE').setAttribute("disabled",true);
					if(document.getElementById("hidSelectedPolicy").value!="")
					{
						SelectComboOption("cmbPOLICY_ID",document.getElementById("hidSelectedPolicy").value);
						document.getElementById("cmbPOLICY_ID").disabled = true;
					}
					if(parent.document.getElementById("trClaimTop"))
						parent.document.getElementById("trClaimTop").style.display="none";
						
					document.getElementById("hrfNOTES_TYPE").style.display = "none";
				}
			}
		</script>
	</HEAD>
	<BODY oncontextmenu="return false;" leftMargin="0" topMargin="0" onload="populateXML();CheckForClaim();ApplyColor();">
		<FORM id="CLT_CUSTOMER_NOTES" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="pageHeader" colSpan="4"><asp:label ID="capMessage" runat="server"></asp:label></TD><%--Please note that all fields marked with * are 
									mandatory --%>
								
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capTOUSERID" Runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbTOUSERID" Runat="server"></asp:dropdownlist></TD>
								<TD class="midcolora" width="18%"></TD>
								<TD class="midcolora" width="32%"></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capNOTES_SUBJECT" runat="server">Subject</asp:label><span class="mandatory">*</span>
								</TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtNOTES_SUBJECT" runat="server" maxlength="50" size="30"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvNOTES_SUBJECT" runat="server" Display="Dynamic" ErrorMessage="NOTES_SUBJECT can't be blank."
										ControlToValidate="txtNOTES_SUBJECT"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capNOTES_TYPE" runat="server"> Type</asp:label></TD>
								<TD class="midcolora" width="32%">
									<asp:dropdownlist id="cmbNOTES_TYPE" onfocus="SelectComboIndex('cmbNOTES_TYPE')" runat="server" onselectedindexchanged="cmbNOTES_TYPE_SelectedIndexChanged" Width='150px'>
										<asp:ListItem Value='0'></asp:ListItem></asp:dropdownlist><A id="hrfNOTES_TYPE" class="calcolora" href="javascript:showPageLookupLayer('cmbNOTES_TYPE')">
										<IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A>
								</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%" style="HEIGHT: 38px"><asp:label id="capPOLICY_ID" runat="server">Quote/Policy</asp:label></TD>
								<TD class="midcolora" width="32%" style="HEIGHT: 100px">
                                    <asp:dropdownlist id="cmbPOLICY_ID" Width="300px" onfocus="SelectComboIndex('cmbPOLICY_ID')" 
                                        runat="server" onselectedindexchanged="cmbPOLICY_ID_SelectedIndexChanged1">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"></TD>
									<TD class="midcolora" width="32%"></TD>  <%--ashish--%>
								
							</tr>
							<tr>
							
								<TD class="midcolora" width="18%" style="HEIGHT: 38px"><asp:label id="capDIARY_ITEM_REQ" runat="server">Diary Item Required</asp:label></TD>
								<TD class="midcolora" width="32%" style="HEIGHT: 38px"><asp:dropdownlist id="cmbDIARY_ITEM_REQ" onfocus="SelectComboIndex('cmbDIARY_ITEM_REQ')" runat="server">
								<%--<asp:ListItem Value='N'>No</asp:ListItem>
								<asp:ListItem Value='Y'>Yes</asp:ListItem>--%>
									</asp:dropdownlist></TD>
									<TD class="midcolora" width="18%" style="HEIGHT: 38px"><asp:label id="capCLAIMS_ID" runat="server">Claim</asp:label></TD>
								<TD class="midcolora" width="32%" style="HEIGHT: 38px"><asp:dropdownlist id="cmbCLAIMS_ID" onfocus="SelectComboIndex('cmbCLAIMS_ID')" runat="server">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist></TD>  <%--ashish--%>
									
								
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capNOTES_DESC" runat="server">Description</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%" ><asp:textbox id="txtNOTES_DESC" runat="server" size="30" TextMode="MultiLine" width="300px" height="100px" ontextchanged="txtNOTES_DESC_TextChanged"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvNOTES_DESC" runat="server" Display="Dynamic" ErrorMessage="NOTES_DESC can't be blank."
										ControlToValidate="txtNOTES_DESC"></asp:requiredfieldvalidator>
									<asp:CustomValidator ControlToValidate="txtNOTES_DESC" Runat="server"
										Display="Dynamic" ID="csvNOTES_DESC"></asp:CustomValidator></TD>
								<%--ashish--%>
								<TD id="tdFollowupCap" class="midcolora" width="18%" style="HEIGHT: 38px"><asp:label id="capFOLLOW_UP_DATE" runat="server" style="DISPLAY:none">Follow up date</asp:label><span class="mandatory" id="spnFOLLOW_UP_DATE" style="DISPLAY:none">*</span></TD>
								<TD id="tdFollowuptxt" class="midcolora" width="32%"><asp:textbox id="txtFOLLOW_UP_DATE" runat="server" maxlength="10" size="12"  style="DISPLAY:none"></asp:textbox><asp:hyperlink id="hlkFOLLOW_UP_DATE" runat="server" CssClass="HotSpot">
										<ASP:IMAGE id="imgFOLLOW_UP_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"
											style="display:none"></ASP:IMAGE>
									</asp:hyperlink><br>
									<asp:requiredfieldvalidator id="rfvFOLLOW_UP_DATE" Display="Dynamic" ControlToValidate="txtFOLLOW_UP_DATE" Runat="server"></asp:requiredfieldvalidator>
									<asp:regularexpressionvalidator id="revFOLLOW_UP_DATE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtFOLLOW_UP_DATE"></asp:regularexpressionvalidator>
									<asp:customvalidator id="csvFOLLOW_UP_DATE" Display="Dynamic" ControlToValidate="txtFOLLOW_UP_DATE" Runat="server"
										ClientValidationFunction="ChkDate"></asp:customvalidator></TD>	<%--ashish--%>	
										
							</tr>
							<tr>
								<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></td>
								<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" align="left"></cmsb:cmsbutton></td>
							</tr>
						</TABLE>
						<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
						<input type="hidden" name="hidClaimsPopUp" id="hidClaimsPopUp" runat="server"> <input type="hidden" name="hidSelectedPolicy" id="hidSelectedPolicy" runat="server">
						<input type="hidden" name="hidCalledFrom" id="hidCalledFrom" runat="server"> <input type="hidden" name="hidPinkSlipNotesTypeID" id="hidPinkSlipNotesTypeID" runat="server">
						<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
						<INPUT id="hidNOTES_ID" type="hidden" value="0" name="hidNOTES_ID" runat="server">
						<INPUT id="hidCustomer_ID" type="hidden" value="0" name="hidCustomer_ID" runat="server">
						<INPUT id="hidApp_ID" type="hidden" value="0" name="hidApp_ID" runat="server"> <INPUT id="hidPolicy_ID" type="hidden" name="hidPolicy_ID" runat="server">
						<INPUT id="hidApp_Version_ID" type="hidden" value="0" name="hidApp_Version_ID" runat="server">
						<INPUT id="hidPolicy_Version_ID" type="hidden" value="0" name="hidPolicy_Version_ID" runat="server">
						<INPUT id="hidClaimID" type="hidden" name="hidClaimID" runat="server">
						<INPUT id="hidFollowUpDate" type="hidden" name="hidFollowUpDate" runat="server">
					</TD>
				</TR>
			</TABLE>
		</FORM>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none;Z-INDEX: 101;POSITION: absolute">
			<iframe id="lookupLayerIFrame" style="DISPLAY: none;Z-INDEX:1001;FILTER: alpha(opacity=0);BACKGROUND-COLOR: #000000"
				width="0" height="0" top="0px;" left="0px"></iframe>
		</div>
		<DIV id="lookupLayer" style="Z-INDEX: 101;VISIBILITY: hidden;POSITION: absolute" onmouseover="javascript:refreshLookupLayer();">
			<table class="TabRow" cellspacing="0" cellpadding="2" width="100%">
				<tr class="SubTabRow">
					<td><b><asp:Label ID="Caplook" runat="server"></asp:Label></b></td>
					<td><p align="right"><a href="javascript:void(0)" onclick="javascript:hideLookupLayer();"><img src="/cms/cmsweb/images/cross.gif" border="0" alt="Close" WIDTH="16" HEIGHT="14"></a></p>
					</td>
				</tr>
				<tr class="SubTabRow">
					<td colspan="2">
						<span id="LookUpMsg"></span>
					</td>
				</tr>
			</table>
		</DIV>
		<!-- For lookup layer ends here-->
		<script>
			
			RefreshWebGrid(document.getElementById('hidFormSaved').value,
			document.getElementById('hidNOTES_ID').value,false);
			
		</script>
	</BODY>
</HTML>
