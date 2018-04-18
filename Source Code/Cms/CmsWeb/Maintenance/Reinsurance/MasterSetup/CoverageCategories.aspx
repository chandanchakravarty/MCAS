<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="CoverageCategories.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Reinsurance.MasterSetup.CoverageCategories" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CoverageCategories</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<LINK href="Common.css" type="text/css" rel="stylesheet">
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
		var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
		var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
		var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		function AddData()
		{
			document.getElementById('hidCOVERAGE_CATEGORY_ID').value	=	'New';
			if(document.getElementById('btnActivateDeactivate'))
				document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
			if(document.getElementById('btnDelete'))
				document.getElementById('btnDelete').style.display = "none";	
			
		}
		
		function populateXML()
		{
			if(document.getElementById('hidFormSaved').value == '0')
			{
				var tempXML = document.getElementById('hidOldData').value;
				if(tempXML != "" && tempXML!="0")
				{
					//populateFormData(tempXML,COVERAGE_CATEGORY);
					//addCatagories();
				}
				else
				{
					AddData();	
				}
			}
			//if(document.getElementById('txtEFFECTIVE_DATE'))
				
		}
		function setCatagories()
		{
			document.COVERAGE_CATEGORY.hidCATAGORY.value = '';
			for (var i=0;i< document.getElementById('cmbCATEGORY').options.length;i++)
			{
				document.COVERAGE_CATEGORY.hidCATAGORY.value = document.COVERAGE_CATEGORY.hidCATAGORY.value + document.getElementById('cmbCATEGORY').options[i].value + ',';
			}	
			//addCatagories();	
			Page_ClientValidate();						
			var returnVal = funcValidateCatagories();
			return Page_IsValid && returnVal;
		}

    		


		function setCategory(catvalue)
		{
			for(s = document.getElementById('cmbFROMCATEGORY').length-1; s >=0;s--)
			{
				if(document.getElementById('cmbFROMCATEGORY').options[s].value == catvalue)
				{	
					document.getElementById('cmbCATEGORY').options[document.getElementById('cmbCATEGORY').length-1].text = document.getElementById('cmbFROMCATEGORY').options[s].text;
					document.getElementById('cmbFROMCATEGORY').options[s]=null;
					break;
				}
			}
				
		}
		function addCatagories()
		{
			var Catagories = document.getElementById("hidCATAGORY").value;
			var Catagorie = Catagories.split(",");
			for(j = document.getElementById('cmbCATEGORY').length-1; j >=0;j--)
			{
				document.getElementById('cmbCATEGORY').options[j].value= null;
			}
			for(j = 0; j < Catagorie.length-1 ;j++)
			//for(j = Catagorie.length-2; j >=0 ;j--)
			{
				//addOption(document.getElementById('cmbCATEGORY'), document.getElementById('cmbFROMCATEGORY').options[j].text, document.getElementById('cmbFROMCATEGORY').options[j].value);
				document.getElementById('cmbCATEGORY').options.length=document.getElementById('cmbCATEGORY').length+1;
				document.getElementById('cmbCATEGORY').options[document.getElementById('cmbCATEGORY').length-1].value=Catagorie[j];
				setCategory(Catagorie[j]);
			}
		}
		
		function addOption(selectElement, text, value) 
		{ 
			var foundIndex = selectElement.options.length; 

			for (i = 0; i < selectElement.options.length; i++) 
			{ 
				if (selectElement.options[i].innerText.toLowerCase() > text.toLowerCase()) 
				{ 
					foundIndex = i; 
					break; 
				} 
			} 

			var oOption = new Option(text,value); 
			selectElement.options.add(oOption, foundIndex); 

		} 
		function selectCatagories()
		{
			for (var i=document.getElementById('cmbFROMCATEGORY').options.length-1;i>=0;i--)
			//for (var i= 0 ;i<document.getElementById('cmbFROMCATEGORY').options.length;i++)
			{
				//alert(document.getElementById('cmbFROMCATEGORY').options[i].selected + '...' + i);	
					if (document.getElementById('cmbFROMCATEGORY').options[i].selected == true)
					{
						addOption(document.getElementById('cmbCATEGORY'), document.getElementById('cmbFROMCATEGORY').options[i].text, document.getElementById('cmbFROMCATEGORY').options[i].value);
						//document.getElementById('cmbCATEGORY').options.length=document.getElementById('cmbCATEGORY').length+1;
						//document.getElementById('cmbCATEGORY').options[document.getElementById('cmbCATEGORY').length-1].value=document.getElementById('cmbFROMCATEGORY').options[i].value;
						//document.getElementById('cmbCATEGORY').options[document.getElementById('cmbCATEGORY').length-1].text=document.getElementById('cmbFROMCATEGORY').options[i].text;
						document.getElementById('cmbFROMCATEGORY').options[i] = null;
					}
					
		  	}
			
			return false;
		  	
		}
		
		function deselectCatagories()
		{
		  for (var i=document.getElementById('cmbCATEGORY').options.length-1;i>=0;i--)
			{
			
				if (document.getElementById('cmbCATEGORY').options[i].selected == true)
				{
					addOption(document.getElementById('cmbFROMCATEGORY'), document.getElementById('cmbCATEGORY').options[i].text, document.getElementById('cmbCATEGORY').options[i].value);
					//document.getElementById('cmbFROMCATEGORY').options.length=document.getElementById('cmbFROMCATEGORY').length+1;
					//document.getElementById('cmbFROMCATEGORY').options[document.getElementById('cmbFROMCATEGORY').length-1].value=document.getElementById('cmbCATEGORY').options[i].value;
					//document.getElementById('cmbFROMCATEGORY').options[document.getElementById('cmbFROMCATEGORY').length-1].text=document.getElementById('cmbCATEGORY').options[i].text;
					document.getElementById('cmbCATEGORY').options[i] = null;
				}
				
		  	}	
		  	return false;			
		
		}
		
		function funcValidateCatagories()
		{
		    var strSel = document.getElementById('hidselcat').value;
 			if(document.getElementById('cmbCATEGORY').options.length == 0)
			{
			    
				document.getElementById('cmbCATEGORY').className = "MandatoryControl";
				document.getElementById("cmbCATEGORY").style.display="inline";
				document.getElementById("csvCATEGORY").style.display="inline";
				document.getElementById("csvCATEGORY").innerText = strSel;  //"Please select Category";
				return false;
			}
			else
			{
				document.getElementById('cmbCATEGORY').className = "none";
				//document.getElementById("cmbCATEGORY").style.display="none";
				return true;
			}
		}
		function resetmultiselect()
		{
			for(j = document.getElementById('cmbCATEGORY').length-1; j >=0;j--)
			{
				if(document.getElementById('cmbCATEGORY').options[j].text.trim()!="" && document.getElementById('cmbCATEGORY').options[j].text!=null)
				{
					addOption(document.getElementById('cmbFROMCATEGORY'), document.getElementById('cmbCATEGORY').options[j].text, document.getElementById('cmbCATEGORY').options[j].value);
				}
				document.getElementById('cmbCATEGORY').options[j]= null;
			}
		}
		function Reset() {
		   
			DisableValidators();
			ChangeColor();
			document.getElementById('hidFormSaved').value = '0';

			//Modified by Ruchika on 9-Jan-2012 for TFS # 836
            document.getElementById('txtEFFECTIVE_DATE').value = '';
			document.getElementById('cmbLOB_ID').value = -1;
            
			//populateXML();
			resetmultiselect();
			addCatagories();
			setTimeout('Focus()',500);	
			return false;
		}
		function Focus() {
		    
			    //document.getElementById('txtEFFECTIVE_DATE').focus();
		}			
		
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();ChangeColor();setTimeout('Focus()',500);">
		<form id="COVERAGE_CATEGORY" method="post" runat="server">
			<table width="100%" align="center" border="0">
				<TBODY>
					<tr>
						<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
					</tr>
					<TR id="trBody" runat="server">
						<td>
							<table id="tblBody" width="100%" align="center" border="0" runat="server">
								<TBODY>
									<tr>
										<TD class="pageHeader" width="100%" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label>
										</TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capEFFECTIVE_DATE" runat="server">Effective Date</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtEFFECTIVE_DATE" runat="server" size="12" maxlength="10" tabIndex="1"></asp:textbox><asp:hyperlink id="hlkEFFECTIVE_DATE" runat="server" CssClass="HotSpot">
												<ASP:IMAGE id="imgEFFECTIVE_DATE" runat="server" ImageUrl="../../../Images/CalendarPicker.gif" tabIndex="2"></ASP:IMAGE>
											</asp:hyperlink><br>
											<asp:requiredfieldvalidator id="rfvEFFECTIVE_DATE" runat="server" ControlToValidate="txtEFFECTIVE_DATE" ErrorMessage=""
												Display="Dynamic"></asp:requiredfieldvalidator><%--Effective Date can't be blank.--%>
											<asp:regularexpressionvalidator id="revEFFECTIVE_DATE" runat="server" Display="Dynamic" ControlToValidate="txtEFFECTIVE_DATE"
											ErrorMessage=""></asp:regularexpressionvalidator><%--RegularExpressionValidator--%>
											</TD>
										<TD class="midcolora" width="18%"><asp:label id="capLOB_ID" runat="server">Product</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLOB_ID" onfocus="SelectComboIndex('cmbLOB_ID')" Runat="server" tabIndex="3"></asp:dropdownlist><br>
											<asp:requiredfieldvalidator id="rfvLOB_ID" ControlToValidate="cmbLOB_ID" ErrorMessage="" Display="Dynamic"
												Runat="server"></asp:requiredfieldvalidator></TD><%--Please Select LOB--%>
									</tr>
									<tr>
										<TD class="midcolora"><asp:label id="capCATEGORY" runat="server">Category</asp:label><SPAN class="mandatory">*</SPAN></TD>
										<td class="midcolora" align="center" width="18%">
											<%--<asp:label id="capCATEGORYDETAILS" Runat="server">Catagory Details</asp:label><br>--%>
											<asp:listbox id="cmbFROMCATEGORY" Runat="server" Height="79px" AutoPostBack="False" SelectionMode="Multiple" tabIndex="4">
												<ASP:LISTITEM Value="0"><--------Select--------></ASP:LISTITEM>
											</asp:listbox><br>
										</td>
										<td class="midcolorc" align="center" width="18%"><br>
											<asp:button class="clsButton" id="btnSELECT" Runat="server" Text=">>" CausesValidation="True" tabIndex="5"></asp:button><br>
											<br>
											<br>
											<br>
											<asp:button class="clsButton" id="btnDESELECT" Runat="server" Text="<<" CausesValidation="False" tabIndex="7"></asp:button></td>
										<td class="midcolora" align="center">
											<asp:label id="capRECIPIENTS" Runat="server" style="DISPLAY: none">Recipients</asp:label>
											<asp:listbox id="cmbCATEGORY" onblur="" Runat="server" Height="79px" Width="200px" AutoPostBack="False"
												SelectionMode="Multiple" tabIndex="6"></asp:listbox><br>
											<asp:customvalidator id="csvCATEGORY" Display="Dynamic" ControlToValidate="cmbCATEGORY" Runat="server"
												ClientValidationFunction="funcValidateCatagories" ErrorMessage =""></asp:customvalidator><span id="spnCATEGORY" style="DISPLAY: none; COLOR: red">Please 
												select Category.</span><%--Please select Category.--%>
										</td>
									</tr>
									<tr>
										<td class="midcolora" colSpan="2">
											<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" tabIndex="8" CausesValidation="false"></cmsb:cmsbutton>
											<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" tabIndex="9"></cmsb:cmsbutton>
											<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate"
												CausesValidation="false" tabIndex="10"></cmsb:cmsbutton>
										</td>
										<td class="midcolorr" colSpan="2">
											<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" tabIndex="11"></cmsb:cmsbutton>
										</td>
									</tr>
								</TBODY>
							</table>
						</td>
					</TR>
					</TR> <INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
					<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
					<INPUT id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
					<INPUT id="hidCOVERAGE_CATEGORY_ID" type="hidden" value="0" name="hidCOVERAGE_CATEGORY_ID"
						runat="server"> <INPUT id="hidCATAGORY" type="hidden" name="hidCATAGORY" runat="server">
					<INPUT id="hidselcat" type="hidden" runat="server">
				</TBODY>
			</table>
		</form>
		<script>
		    try {
		        if (document.getElementById('hidFormSaved').value == "1") {

		            RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidCOVERAGE_CATEGORY_ID').value, false);
		        }
		    }
		    catch (err) {
		    }      
				
				addCatagories();
		</script>
	</body>
</HTML>
