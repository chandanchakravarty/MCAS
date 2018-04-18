<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="ReportEndorsement.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.ReportEndorsement" validateRequest=false %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>MNT_AGENCY_LIST</title>
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
		
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";
			
			function AddData()
			{
			ChangeColor();
			
			DisableValidators();
			
			document.getElementById('hidAGENCY_ID').value	=	'New';
			document.getElementById('txtAGENCY_DISPLAY_NAME').focus();
			document.getElementById('txtAGENCY_DBA').value = '';
			document.getElementById('cmbAgencyNameDecPage').options.selectedIndex = -1; 
			document.getElementById('txtAGENCY_COMBINED_CODE').value  = '';
			document.getElementById('txtAGENCY_CODE').value  = '';
			document.getElementById('txtAGENCY_DISPLAY_NAME').value  = '';
			document.getElementById('txtAGENCY_LIC_NUM').value  = '';
			document.getElementById('txtAGENCY_ADD1').value  = '';
			document.getElementById('txtAGENCY_ADD2').value  = '';
			document.getElementById('txtAGENCY_CITY').value  = '';
			document.getElementById('cmbAGENCY_STATE').options.selectedIndex = -1;
			document.getElementById('txtAGENCY_ZIP').value  = '';
			//document.getElementById('cmbAGENCY_COUNTRY').value = '<%=aCountry%>';
			document.getElementById('txtAGENCY_PHONE').value  = '';
			document.getElementById('txtAGENCY_EXT').value  = '';
			document.getElementById('txtAGENCY_FAX').value  = '';
			document.getElementById('txtAGENCY_SPEED_DIAL').value  = '';
			
			document.getElementById('txtPRINCIPAL_CONTACT').value  = '';
			document.getElementById('txtOTHER_CONTACT').value  = '';
			document.getElementById('txtFEDERAL_ID').value  = '';
			document.getElementById('txtORIGINAL_CONTRACT_DATE').value  = '';
			document.getElementById('txtCURRENT_CONTRACT_DATE').value  = '';
			//document.getElementById('lstUNDERWRITER_ASSIGNED_AGENCY').value  = '';
			document.getElementById('txtBANK_ACCOUNT_NUMBER').value  = '';
			document.getElementById('txtBANK_NAME').value  = '';
			document.getElementById('txtBANK_BRANCH').value  = '';
			document.getElementById('txtROUTING_NUMBER').value  = '';
			document.getElementById('txtBANK_ACCOUNT_NUMBER1').value  = '';
			document.getElementById('txtROUTING_NUMBER1').value  = '';
			
			
			document.getElementById('txtAGENCY_EMAIL').value  = '';
			document.getElementById('txtAGENCY_WEBSITE').value  = '';
			document.getElementById('cmbAGENCY_PAYMENT_METHOD').options.selectedIndex = 0;
			document.getElementById('cmbAGENCY_BILL_TYPE').options.selectedIndex = 0;
			//NEW FIELDS
			document.getElementById('txtM_AGENCY_ADD_1').value  = '';
			document.getElementById('txtM_AGENCY_ADD_2').value  = '';
			document.getElementById('txtM_AGENCY_CITY').value  = '';
	//		document.getElementById('cmbM_AGENCY_COUNTRY').value  = '';
			document.getElementById('cmbM_AGENCY_STATE').value  = '';
			document.getElementById('txtAGENCY_ZIP').value  = '';
			document.getElementById('txtAGENCY_PHONE').value  = '';
			//document.getElementById('txtM_AGENCY_EXT').value  = '';
			//document.getElementById('txtM_AGENCY_FAX').value  = '';
			
			if(document.getElementById('btnActivateDeactivate'))
			document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);	
			if(document.getElementById('btnDelete'))
			document.getElementById('btnDelete').style.display='none';
			
			}


			function populateXML()
			{
			
			ResetAfterActivateDeactivate();
			
				var tempXML;
				tempXML=document.getElementById("hidOldData").value;
				//alert(tempXML);
				if(document.getElementById('hidFormSaved')!=null &&( document.getElementById('hidFormSaved').value == '0'||document.getElementById('hidFormSaved').value == '1'))
				{				
					if(tempXML!="" && tempXML!="0")
					{
						//document.getElementById('btnDelete').style.display='none';
						populateFormData(tempXML,"MNT_AGENCY_LIST");						
					}
					else
					{
						AddData();
					}
				}
				SetTab();
				return false;
			}
			 function ResetAfterActivateDeactivate()
			{
				if (document.getElementById('hidReset').value == "1")
				{				
					document.MNT_AGENCY_LIST.reset();			
				}
			}
			
			function SetTab()
			{
				var counter = 5;
				
				if('<%=strSystemID.Trim().ToUpper()%>' != '<%=strCarrierSystemID.Trim().ToUpper()%>')
					counter--;
					
				if((document.getElementById('hidFormSaved').value == '1') || (document.getElementById("hidOldData").value != ""))
				{	
					Url="AddDefaultHierarchy.aspx?EntityType=Agency&EntityId="+document.getElementById('hidAGENCY_ID').value + "&";
					DrawTab(counter--,top.frames[1],'Default Hierarchy',Url);
					
					if('<%=strSystemID.Trim().ToUpper()%>' == '<%=strCarrierSystemID.Trim().ToUpper()%>')
					{
						Url="AddUnderwriterAssignment.aspx?EntityType=Agency&EntityId="+document.getElementById('hidAGENCY_ID').value + "&";
						DrawTab(counter--,top.frames[1],'Assign UW/Marketing',Url);
					}
										
					Url="AttachmentIndex.aspx?calledfrom=agency&EntityType=Agency&EntityId="+document.getElementById('hidAGENCY_ID').value + "&";
					DrawTab(counter--,top.frames[1],'Attachment',Url);
					Url="UserIndex.aspx?CalledFrom=AGENCY&AGENCY_CODE="+document.getElementById('hidAGENCY_CODE').value + "&";
					DrawTab(counter--,top.frames[1],'Users',Url);					
					
				}
				else
				{		
					RemoveTab(5,top.frames[1]);
					RemoveTab(4,top.frames[1]);					
					RemoveTab(3,top.frames[1]);
					RemoveTab(2,top.frames[1]);
				}			
			}
			
			function generateCode()
			{
			var strname=new String();
				strname=document.getElementById("txtAGENCY_DISPLAY_NAME").value; 
				
				if(document.getElementById('hidAGENCY_ID').value=='New')
				{
					if(strname.length>4 )
						document.getElementById("txtAGENCY_CODE").value=strname.substring(0,4);
					else
						document.getElementById("txtAGENCY_CODE").value=strname;					
				}
					  
				
			}																		  
																	  
			function CopyPhysicalAddress()
			{	 
				if(document.getElementById('txtM_AGENCY_ADD_1').value=="")
				document.getElementById('txtM_AGENCY_ADD_1').value =document.getElementById('txtAGENCY_ADD1').value ;
				if(document.getElementById('txtM_AGENCY_ADD_2').value=="")
				document.getElementById('txtM_AGENCY_ADD_2').value =document.getElementById('txtAGENCY_ADD2').value ;
				if(document.getElementById('txtM_AGENCY_CITY').value=="")
				document.getElementById('txtM_AGENCY_CITY').value =document.getElementById('txtAGENCY_CITY').value	;
				if(document.getElementById('cmbM_AGENCY_COUNTRY').value=="")
				document.getElementById('cmbM_AGENCY_COUNTRY').value =document.getElementById('cmbAGENCY_COUNTRY').value ;
				if(document.getElementById('cmbM_AGENCY_STATE').value=="")
				document.getElementById('cmbM_AGENCY_STATE').value =document.getElementById('cmbAGENCY_STATE').value ;
				if(document.getElementById('txtM_AGENCY_ZIP').value=="")
				document.getElementById('txtM_AGENCY_ZIP').value =document.getElementById('txtAGENCY_ZIP').value	;
				//if(document.getElementById('txtM_AGENCY_PHONE').value=="")
				//document.getElementById('txtM_AGENCY_PHONE').value =document.getElementById('txtAGENCY_PHONE').value	;
				//if(document.getElementById('txtM_AGENCY_EXT').value=="")
				//document.getElementById('txtM_AGENCY_EXT').value =document.getElementById('txtAGENCY_EXT').value	;
				//if(document.getElementById('txtM_AGENCY_FAX').value=="")
				//document.getElementById('txtM_AGENCY_FAX').value =document.getElementById('txtAGENCY_FAX').value ;
				ChangeColor();
				return false;
			}
																							
																				  
			function showHide()
			{
				if(document.getElementById('RowId').value!='New')
				{
					if(document.getElementById('btnActivateDeactivate'))
					document.getElementById('btnActivateDeactivate').setAttribute('disabled',false);  
					//document.getElementById('btnReset').style.display="none";
				}	
			}
			/*
	function CountUnderWriter()
		{
			document.getElementById("hidUnderWriter").value = "";
			var coll = document.MNT_AGENCY_LIST.lstUNDERWRITER_ASSIGNED_AGENCY;
			var len = coll.options.length;
			var k;
			var szSelectedDept;
			//alert(document.getElementById("lstWATERS_NAVIGATED").options[0].selected);
			for( k = 0;k < len ; k++)
			{											
				if(document.getElementById("lstUNDERWRITER_ASSIGNED_AGENCY").options[k].selected == true)
				{
					szSelectedDept = coll.options(k).value;
					if (document.getElementById("hidUnderWriter").value == "")
					{
						document.getElementById("hidUnderWriter").value =  szSelectedDept;
					}
					else
					{
						document.MNT_AGENCY_LIST.hidUnderWriter.value = document.MNT_AGENCY_LIST.hidUnderWriter.value + "," + szSelectedDept;
						
					}
					//alert(document.APP_WATERCRAFT_INFO.hidWaterNavigateID.value);
				}	
			
			}
//			document.AssignDivDept.TextBox1.style.display = 'none';		
		}*/
	function ChkDate(objSource , objArgs)
	{
		var effdate=document.MNT_AGENCY_LIST.txtORIGINAL_CONTRACT_DATE.value;
		objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>",effdate,jsaAppDtFormat);
	}
	
	
					 
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();">
		<FORM id="MNT_AGENCY_LIST" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
										mandatory</TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capAGENCY_DISPLAY_NAME" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtAGENCY_DISPLAY_NAME" runat="server" maxlength="75"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvAGENCY_DISPLAY_NAME" runat="server" Display="Dynamic" ControlToValidate="txtAGENCY_DISPLAY_NAME"></asp:requiredfieldvalidator>
										<asp:regularexpressionvalidator id="revAGENCY_DISPLAY_NAME" Display="Dynamic" ControlToValidate="txtAGENCY_DISPLAY_NAME"
											Runat="server"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capNUM_AGENCY_CODE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtNUM_AGENCY_CODE" runat="server" MaxLength="4" size="6"></asp:textbox><BR>
										<asp:RegularExpressionValidator ID="revNUM_AGENCY_CODE" Runat="server" ControlToValidate="txtNUM_AGENCY_CODE" Display="Dynamic"></asp:RegularExpressionValidator>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capAGENCY_CODE" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtAGENCY_CODE" runat="server" MaxLength="6" size="8"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvAGENCY_CODE" runat="server" Display="Dynamic" ControlToValidate="txtAGENCY_CODE"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capAGENCY_COMBINED_CODE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtAGENCY_COMBINED_CODE" runat="server" MaxLength="6" size="8"></asp:textbox></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capAGENCY_DBA" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtAGENCY_DBA" runat="server" TextMode="MultiLine"></asp:textbox><BR>
										<asp:regularexpressionvalidator id="revAGENCY_DBA" Display="Dynamic" ControlToValidate="txtAGENCY_DBA" Runat="server"></asp:regularexpressionvalidator>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capAgencyDecName" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbAgencyNameDecPage" runat="server"></asp:dropdownlist></TD>
								</tr>
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4">Physical Address</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capAGENCY_ADD1" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtAGENCY_ADD1" runat="server" maxlength="70"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvAGENCY_ADD1" runat="server" Display="Dynamic" ControlToValidate="txtAGENCY_ADD1"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capAGENCY_ADD2" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtAGENCY_ADD2" runat="server" maxlength="70"></asp:textbox></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capAGENCY_CITY" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtAGENCY_CITY" runat="server" maxlength="40"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revAGENCY_CITY" Display="Dynamic" ControlToValidate="txtAGENCY_CITY" Runat="server"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capAGENCY_COUNTRY" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbAGENCY_COUNTRY" onfocus="SelectComboIndex('cmbAGENCY_COUNTRY')" runat="server"></asp:dropdownlist>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capAGENCY_STATE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbAGENCY_STATE" onfocus="SelectComboIndex('cmbAGENCY_STATE')" runat="server"></asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capAGENCY_ZIP" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtAGENCY_ZIP" runat="server" maxlength="10" size="13"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revAGENCY_ZIP" Display="Dynamic" ControlToValidate="txtAGENCY_ZIP" Runat="server"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capAGENCY_PHONE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtAGENCY_PHONE" runat="server" MaxLength="13" size="17"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revAGENCY_PHONE" Display="Dynamic" ControlToValidate="txtAGENCY_PHONE" Runat="server"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capAGENCY_EXT" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtAGENCY_EXT" runat="server" MaxLength="13" size="17"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revAGENCY_EXT" Display="Dynamic" ControlToValidate="txtAGENCY_EXT" Runat="server"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capAGENCY_FAX" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtAGENCY_FAX" runat="server" MaxLength="15" size="17"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revAGENCY_FAX" Display="Dynamic" ControlToValidate="txtAGENCY_FAX" Runat="server"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capAGENCY_SPEED_DIAL" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtAGENCY_SPEED_DIAL" runat="server" MaxLength="4" size="4"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revAGENCY_SPEED_DIAL" Display="Dynamic" ControlToValidate="txtAGENCY_SPEED_DIAL"
											Runat="server"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4" style="HEIGHT: 21px">Mailing 
										Address</TD>
								</tr>
								<tr>
									<td class="midcolora" width="18%"><asp:label id="lblCopy_Address" runat="server"></asp:label></td>
									<td class="midcolora" width="32%"><cmsb:cmsbutton class="clsButton" id="btnCopyPhysicalAddress" runat="server" Text="Copy Physical Address"></cmsb:cmsbutton></td>
					</TD>
					<td class="midcolora" colSpan="2"></td>
				</TR>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capM_AGENCY_ADD_1" runat="server"></asp:label><span class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtM_AGENCY_ADD_1" runat="server" maxlength="70"></asp:textbox><BR>
						<asp:requiredfieldvalidator id="rfvM_AGENCY_ADD_1" runat="server" Display="Dynamic" ControlToValidate="txtM_AGENCY_ADD_1"></asp:requiredfieldvalidator></TD>
					<TD class="midcolora" width="18%"><asp:label id="capM_AGENCY_ADD_2" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtM_AGENCY_ADD_2" runat="server" maxlength="70"></asp:textbox></TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capM_AGENCY_CITY" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtM_AGENCY_CITY" runat="server" maxlength="40"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revM_AGENCY_CITY" Display="Dynamic" ControlToValidate="txtM_AGENCY_CITY" Runat="server"></asp:regularexpressionvalidator></TD>
					<TD class="midcolora" width="18%"><asp:label id="capM_AGENCY_COUNTRY" runat="server"></asp:label><!--<span class="mandatory">*</span>--></TD>
					<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbM_AGENCY_COUNTRY" onfocus="SelectComboIndex('cmbM_AGENCY_COUNTRY')" runat="server"></asp:dropdownlist><BR>
						<asp:requiredfieldvalidator id="rfvM_AGENCY_COUNTRY" runat="server" Display="Dynamic" ControlToValidate="cmbM_AGENCY_COUNTRY"></asp:requiredfieldvalidator></TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capM_AGENCY_STATE" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbM_AGENCY_STATE" onfocus="SelectComboIndex('cmbM_AGENCY_STATE')" runat="server"></asp:dropdownlist></TD>
					<TD class="midcolora" width="18%"><asp:label id="capM_AGENCY_ZIP" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtM_AGENCY_ZIP" runat="server" maxlength="10" size="13"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revM_AGENCY_ZIP" Display="Dynamic" ControlToValidate="txtM_AGENCY_ZIP" Runat="server"></asp:regularexpressionvalidator></TD>
				</tr>
				<!--BANK INFORMATION :S-->
				<tr>
					<TD class="headerEffectSystemParams" colSpan="4" style="HEIGHT: 21px">Bank 
						Information</TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capBANK_NAME" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtBANK_NAME" runat="server"></asp:textbox><BR>
						<asp:regularexpressionvalidator id="revBANK_NAME" runat="server" Display="Dynamic" ControlToValidate="txtBANK_NAME"
							ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator></TD>
					<TD class="midcolora" width="18%"><asp:label id="capBANK_BRANCH" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtBANK_BRANCH" runat="server" size="23"></asp:textbox><BR>
						<asp:regularexpressionvalidator id="revBANK_BRANCH" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
							ControlToValidate="txtBANK_BRANCH"></asp:regularexpressionvalidator></TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capBANK_ACCOUNT_NUMBER" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtBANK_ACCOUNT_NUMBER" runat="server" maxlength="20"></asp:textbox><BR>
						<asp:regularexpressionvalidator id="revBANK_ACCOUNT_NUMBER" runat="server" Display="Dynamic" ControlToValidate="txtBANK_ACCOUNT_NUMBER"
							ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator></TD>
					<TD class="midcolora" width="18%"><asp:label id="capROUTING_NUMBER" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtROUTING_NUMBER" runat="server" MaxLength="20" size="23"></asp:textbox><BR>
						<asp:regularexpressionvalidator id="revROUTING_NUMBER" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
							ControlToValidate="txtROUTING_NUMBER"></asp:regularexpressionvalidator></TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capBANK_ACCOUNT_NUMBER1" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtBANK_ACCOUNT_NUMBER1" runat="server" maxlength="20"></asp:textbox><BR>
						<asp:regularexpressionvalidator id="revBANK_ACCOUNT_NUMBER1" runat="server" Display="Dynamic" ControlToValidate="txtBANK_ACCOUNT_NUMBER1"
							ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator></TD>
					<TD class="midcolora" width="18%"><asp:label id="capROUTING_NUMBER1" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtROUTING_NUMBER1" runat="server" MaxLength="20" size="23"></asp:textbox><BR>
						<asp:regularexpressionvalidator id="revROUTING_NUMBER1" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
							ControlToValidate="txtROUTING_NUMBER1"></asp:regularexpressionvalidator></TD>
				</tr>
				<!--BANK INFORMATION :E-->
				<tr>
					<TD class="headerEffectSystemParams" colSpan="4" style="HEIGHT: 21px">Other 
						Information</TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capAGENCY_EMAIL" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtAGENCY_EMAIL" runat="server" maxlength="50"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revAGENCY_EMAIL" Display="Dynamic" ControlToValidate="txtAGENCY_EMAIL" Runat="server"></asp:regularexpressionvalidator></TD>
					<td class="midcolora" colSpan="2"></td>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capAGENCY_WEBSITE" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtAGENCY_WEBSITE" runat="server" maxlength="70"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revAGENCY_WEBSITE" Display="Dynamic" ControlToValidate="txtAGENCY_WEBSITE" Runat="server"></asp:regularexpressionvalidator></TD>
					<TD class="midcolora" width="18%"><asp:label id="capAGENCY_PAYMENT_METHOD" runat="server"></asp:label><span class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbAGENCY_PAYMENT_METHOD" onfocus="SelectComboIndex('cmbAGENCY_PAYMENT_METHOD')"
							runat="server"></asp:dropdownlist><BR>
						<asp:requiredfieldvalidator id="rfvAGENCY_PAYMENT_METHOD" runat="server" Display="Dynamic" ControlToValidate="cmbAGENCY_PAYMENT_METHOD"></asp:requiredfieldvalidator></TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capAGENCY_BILL_TYPE" runat="server"></asp:label><span class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbAGENCY_BILL_TYPE" onfocus="SelectComboIndex('cmbAGENCY_BILL_TYPE')" runat="server"></asp:dropdownlist><BR>
						<asp:requiredfieldvalidator id="rfvAGENCY_BILL_TYPE" runat="server" Display="Dynamic" ControlToValidate="cmbAGENCY_BILL_TYPE"></asp:requiredfieldvalidator></TD>
					<TD class="midcolora" width="18%"><asp:label id="capAGENCY_LIC_NUM" runat="server"></asp:label><span class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtAGENCY_LIC_NUM" runat="server" MaxLength="3" size="3"></asp:textbox><BR>
						<asp:requiredfieldvalidator id="rfvAGENCY_LIC_NUM" runat="server" Display="Dynamic" ControlToValidate="txtAGENCY_LIC_NUM"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revAGENCY_LIC_NUM" Display="Dynamic" ControlToValidate="txtAGENCY_LIC_NUM" Runat="server"></asp:regularexpressionvalidator></TD>
				</tr>
				<!--START- NEW FIELDS ADDED-->
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capPRINCIPAL_CONTACT" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtPRINCIPAL_CONTACT" runat="server" MaxLength="50" size="17"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revPRINCIPAL_CONTACT" runat="server" Display="Dynamic" ControlToValidate="txtPRINCIPAL_CONTACT"
							ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator></TD>
					<TD class="midcolora" width="18%"><asp:label id="capOTHER_CONTACT" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtOTHER_CONTACT" runat="server" maxlength="50"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revOTHER_CONTACT" runat="server" Display="Dynamic" ControlToValidate="txtOTHER_CONTACT"
							ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator></TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capFEDERAL_ID" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtFEDERAL_ID" runat="server" maxlength="10"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revFEDERAL_ID" runat="server" Display="Dynamic" ControlToValidate="txtFEDERAL_ID"
							ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator></TD>
					<TD class="midcolora" width="18%"><asp:label id="capORIGINAL_CONTRACT_DATE" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtORIGINAL_CONTRACT_DATE" runat="server" maxlength="10" size="12"></asp:textbox><asp:hyperlink id="hlkORIGINAL_CONTRACT_DATE" runat="server" CssClass="HotSpot">
							<asp:image id="imgORIGINAL_CONTRACT_DATE" runat="server" ImageUrl="/cms/CmsWeb/Images/CalendarPicker.gif"></asp:image>
						</asp:hyperlink>
						<asp:regularexpressionvalidator id="revORIGINAL_CONTRACT_DATE" Display="Dynamic" ControlToValidate="txtORIGINAL_CONTRACT_DATE"
							Runat="server"></asp:regularexpressionvalidator>
						<asp:customvalidator id="csvORIGINAL_CONTRACT_DATE" Display="Dynamic" ControlToValidate="txtORIGINAL_CONTRACT_DATE"
							Runat="server" ClientValidationFunction="ChkDate"></asp:customvalidator></TD>
				</tr>
				<tr id="rowTermination" runat="server">
					<TD class="midcolora" width="18%"><asp:label id="capCURRENT_CONTRACT_DATE" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtCURRENT_CONTRACT_DATE" runat="server" maxlength="10" size="12"></asp:textbox><asp:hyperlink id="hlkCURRENT_CONTRACT_DATE" runat="server" CssClass="HotSpot">
							<asp:image id="imgCURRENT_CONTRACT_DATE" runat="server" ImageUrl="/cms/CmsWeb/Images/CalendarPicker.gif"></asp:image>
						</asp:hyperlink>
						<asp:regularexpressionvalidator id="revCURRENT_CONTRACT_DATE" Display="Dynamic" ControlToValidate="txtCURRENT_CONTRACT_DATE"
							Runat="server"></asp:regularexpressionvalidator>
						<asp:customvalidator id="csvCURRENT_CONTRACT_DATE" Display="Dynamic" ControlToValidate="txtCURRENT_CONTRACT_DATE"
							Runat="server" ClientValidationFunction=""></asp:customvalidator></TD>
					<td class="midcolora" colspan="2"></td>
				</tr>
				<tr>
					<td class="midcolora" width="18%">
						<asp:label id="capTERMINATION_DATE" runat="server"></asp:label>
					</td>
					<td class="midcolora" width="32%">
						<asp:textbox id="txtTERMINATION_DATE" runat="server" maxlength="10" size="12"></asp:textbox>
						<asp:hyperlink id="hlkTERMINATION_DATE" runat="server" CssClass="HotSpot">
							<asp:image id="imgTERMINATION_DATE" runat="server" ImageUrl="/cms/CmsWeb/Images/CalendarPicker.gif"></asp:image>
						</asp:hyperlink>
						<asp:regularexpressionvalidator id="revTERMINATION_DATE" Display="Dynamic" ControlToValidate="txtTERMINATION_DATE"
							Runat="server"></asp:regularexpressionvalidator>
					</td>
					<td class="midcolora" width="18%">
						<asp:label id="capTERMINATION_DATE_RENEW" runat="server"></asp:label>
					</td>
					<td class="midcolora" width="32%">
						<asp:textbox id="txtTERMINATION_DATE_RENEW" runat="server" maxlength="10" size="12"></asp:textbox>
						<asp:hyperlink id="hlkTERMINATION_DATE_RENEW" runat="server" CssClass="HotSpot">
							<asp:image id="imgTERMINATION_DATE_RENEW" runat="server" ImageUrl="/cms/CmsWeb/Images/CalendarPicker.gif"></asp:image>
						</asp:hyperlink>
						<asp:regularexpressionvalidator id="revTERMINATION_DATE_RENEW" Display="Dynamic" ControlToValidate="txtTERMINATION_DATE_RENEW"
							Runat="server"></asp:regularexpressionvalidator>
					</td>
				</tr>
				<tr>
					<!--
					<td class="midcolora" width="18%">
						<asp:label id="capTERMINATION_REASON" runat="server"></asp:label>
					</td>
					<td class="midcolora" width="32%">
						<asp:textbox id="txtTERMINATION_REASON" runat="server" maxlength="10" size="12"></asp:textbox>
					</td> -->
					<TD class="midcolora" style="HEIGHT: 38px" width="18%"><asp:label id="capTERMINATION_NOTICE" runat="server">Termination Notice</asp:label></TD>
					<TD class="midcolora" style="HEIGHT: 38px" width="32%"><asp:dropdownlist id="cmbTERMINATION_NOTICE" runat="server">
							<asp:ListItem Value=''></asp:ListItem>
							<asp:ListItem Value='N'>No</asp:ListItem>
							<asp:ListItem Value='Y'>Yes</asp:ListItem>
						</asp:dropdownlist></TD>
					<td class="midcolora" width="18%">
						<asp:label id="capNOTES" runat="server"></asp:label>
					</td>
					<td class="midcolora" width="32%"><asp:textbox id="txtNOTES" runat="server" size="23" TextMode="MultiLine"></asp:textbox></td>
				</tr>
				<!--END - NEW FIELDS ADDED-->
				<tr>
					<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>&nbsp;
						<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate"
							CausesValidation="false"></cmsb:cmsbutton></td>
					<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" CausesValidation="False"
							causevalidation="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
				</tr>
			</TABLE>
			</TD></TR></TBODY></TABLE><INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="Hidden1" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidAGENCY_ID" type="hidden" value="0" name="hidAGENCY_ID" runat="server">
			<INPUT id="hidAGENCY_CODE" type="hidden" name="hidAGENCY_CODE" runat="server"> <INPUT id="hidUnderWriter" type="hidden" name="hidUnderWriter" runat="server">
			<INPUT id="hidReset" type="hidden" value="0" name="hidReset" runat="server">
		</FORM>
		<script>
		//RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidAGENCY_ID').value,true);
			var temp = '<%=strSystemID%>';
			
			//if(temp.toUpperCase() == 'W001')
			
			if(temp.toUpperCase() == <%=  "'" + CarrierSystemID.ToUpper() + "'" %>)
			{
				RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidAGENCY_ID').value,true);
			}
		</script>
	</BODY>
</HTML>
