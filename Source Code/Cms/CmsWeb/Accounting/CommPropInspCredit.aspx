<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page validateRequest=false language="c#" Codebehind="CommPropInspCredit.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Accounting.CommPropInspCredit" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>Commission Setup - Property Inspection Credit</title>
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
	
		function AddData()
		{
			
			//document.getElementById('hidOldData').value= '';
			DisableValidators();
			document.getElementById('hidCOM_ID').value = 'New';
			document.getElementById('cmbLOB_ID').options.selectedIndex = -1;
			document.getElementById('cmbTERM').options.selectedIndex = -1;
			document.getElementById('txtEFFECTIVE_FROM_DATE').value  = '';
			document.getElementById('txtEFFECTIVE_TO_DATE').value  = '';
			document.getElementById('txtCOMMISSION_PERCENT').value  = '';
			document.getElementById('txtREMARKS').value  = '';
			if(document.getElementById('btnActivateDeactivate'))
			document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
			ChangeColor();
		}
		function ResetPage()
		{
			parent.changeTab(0, 0);
		}
		function populateXML()
		{
		 
			var tempXML = document.getElementById('hidOldData').value;
			if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
			//if(document.getElementById('hidFormSaved').value == '0')
			{
			    
				if(tempXML!="") {
				   populateFormData(tempXML,ACT_REG_COMM_SETUP);
//				   for (i = 0; i < document.getElementById('cmbSTATE_ID').options.length; i++) {
//				       
//						if(document.getElementById('cmbSTATE_ID').options[i].value==document.getElementById('hidState').value)	
//							document.getElementById('cmbSTATE_ID').options[i].selected=true;
//					}				
				}
				else
				{
					AddData();
				}
            
            }
                else
                {
				    //Added By Raghav Deactivted the button after saving the record
				   if(document.getElementById('btnActivateDeactivate'))
				       document.getElementById('btnActivateDeactivate').setAttribute('disabled', true);

				  //Added by Ruchika Chauhan on 30-Jan-2012 for TFS # 836 
				   SelectComboOption("cmbLOB_ID", document.getElementById('hidLOB_ID').value)
				   
                }			
			 
					//Added By Raghav To avoid a null check.
					if(document.getElementById("capCOMMISSION_PERCENT")!=null)
					document.getElementById('capCOMMISSION_PERCENT').innerHTML = "Credit %";		
			
			return false;
		}

		function Validate(objSource , objArgs)
		{	
			var comm = parseFloat(document.getElementById('txtCOMMISSION_PERCENT').value);
			if(comm < 0 || comm > 100)
			{
				document.getElementById('txtCOMMISSION_PERCENT').select();
				objArgs.IsValid=false;
			}
			else
				objArgs.IsValid=true;
		}
		//date comparision
		var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
		var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
		var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		function ChkDate(objSource , objArgs) {
            var fromDate=document.getElementById('txtEFFECTIVE_FROM_DATE').value;				
			var toDate=document.getElementById('txtEFFECTIVE_TO_DATE').value;
			objArgs.IsValid = DateComparer(toDate,fromDate, jsaAppDtFormat);
		}		
	
		function formReset()
		{
			document.location.href = document.location.href; 
			return false;
		}
		function CopyRecords()
		{
			//window.open('CopyCommission.aspx?COMMISSION_TYPE=R','CopyCommission','800','800','Yes','Yes','No','No','No');
			window.open('CopyCommission.aspx?COMMISSION_TYPE=<%=strCalledFrom%>','CopyCommission',"width=800,height=600,screenX=50,screenY=150,top=80,left=80,scrollbars=yes,resizable=no,menubar=no,toolbar=no,status=no","");
			return false;
		}
		//Added By Amit mishra For tfs bug #836
		function FillLOB() {

		    //Added By raghav to avoid a null check
		    if (document.getElementById("cmbLOB_ID") != null)
		        document.getElementById('cmbLOB_ID').innerHTML = '';
		    var Xml = document.getElementById('hidLobXMLForClass').value;
//		    var stID;
		    //LOB id is not selected then returning 
//		    //Added By raghav to avoid a null check
//		    if (document.getElementById("cmbSTATE_ID") != null) {
//		        if (document.getElementById('cmbSTATE_ID').selectedIndex == -1) {
//		            document.getElementById('hidLOB_ID').value = '';
//		            return false;
//		        }
//		        stID = document.getElementById('cmbSTATE_ID').options[document.getElementById('cmbSTATE_ID').selectedIndex].value;
//		    }

		    var objXmlHandler = new XMLHandler();
		    var tree = objXmlHandler.quickParseXML(Xml).childNodes[0];
		    //adding a blank option
		    oOption = document.createElement("option");
		    oOption.value = "";
		    oOption.text = "";
		    //Added By Raghav to avoid null check
		    if (document.getElementById("cmbLOB_ID") != null)
		        document.getElementById('cmbLOB_ID').add(oOption);

		    //adding a all option
		    //oOption = document.createElement("option");
		    //oOption.value = "0";
		    //oOption.text = "All";
		    //document.getElementById('cmbLOB_ID').add(oOption);

		    for (i = 0; i < tree.childNodes.length; i++) {
		        //				stateValue = tree.childNodes[i].getElementsByTagName('STATE_ID');
		        //				if (stateValue != null)
		        //				{
		        //					if (stateValue[0].firstChild == null)
		        //						continue;

		        //					if(stID!="0")
		        //					{						
		        //					if (stateValue[0].firstChild.text==stID)
		        //					{
		        //						
		        LobId = tree.childNodes[i].getElementsByTagName('LOB_ID');
		        LobDesc = tree.childNodes[i].getElementsByTagName('LOB_DESC');

		        if (LobId != null && LobDesc != null) {
		            if ((LobId[0] != null || LobId[0] == 'undefined')
								&& (LobDesc[0] != null || LobDesc[0] == 'undefined')) {
		                oOption = document.createElement("option");
		                oOption.value = LobId[0].firstChild.text;
		                oOption.text = LobDesc[0].firstChild.text;
		                document.getElementById('cmbLOB_ID').add(oOption);

		            }
		        }
		        //}
		        //					}
		        //}
		    }
		}
		function setHidLob() {
		   
		    if (document.getElementById('cmbLOB_ID').options[document.getElementById('cmbLOB_ID').selectedIndex].value != "")
		        document.getElementById('hidLOB_ID').value = document.getElementById('cmbLOB_ID').options[document.getElementById('cmbLOB_ID').selectedIndex].value;
		}
		</script>
</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="FillLOB();populateXML();ApplyColor();">
		<FORM id="ACT_REG_COMM_SETUP" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="4"><asp:Label ID="capMaindatory" runat="server"></asp:Label> </TD><%--Please note that all fields marked with * are mandatory--%>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
								</tr>
								<tbody id="tbForm" runat="server">
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capCountry" runat="server"></asp:label></TD><%--Country--%>
										<TD class="midcolora" width="32%"><asp:label id="lblCOUNTRY_NAME" runat="server"></asp:label></TD>
										<TD class="midcolora" width="18%"><asp:label id="capSTATE_ID" runat="server">State</asp:label><SPAN class="mandatory">*</SPAN></TD>
										<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSTATE_ID" onfocus="SelectComboIndex('cmbSTATE_ID')" runat="server" Width="90px"></asp:dropdownlist><br>
											<asp:requiredfieldvalidator id="rfvSTATE_ID" runat="server" ControlToValidate="cmbSTATE_ID" ErrorMessage="STATE_ID can't be blank."
												Display="Dynamic"></asp:requiredfieldvalidator></TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capLOB_ID" runat="server">Line of Business</asp:label><SPAN class="mandatory">*</SPAN></TD>
										<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLOB_ID" onfocus="SelectComboIndex('cmbLOB_ID')" onchange="setHidLob()" runat="server"></asp:dropdownlist><br>
											<asp:requiredfieldvalidator id="rfvLOB_ID" runat="server" ControlToValidate="cmbLOB_ID" ErrorMessage="LOB_ID can't be blank."
												Display="Dynamic"></asp:requiredfieldvalidator></TD>
										<TD class="midcolora" width="18%"><asp:label id="capTERM" runat="server">Term</asp:label><SPAN class="mandatory">*</SPAN></TD>
										<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbTERM" onfocus="SelectComboIndex('cmbTERM')" runat="server" Width="90px">
												<asp:ListItem></asp:ListItem>
												<asp:ListItem Value="F">First Term(NBS)</asp:ListItem>
												<asp:ListItem Value="O">Other Term</asp:ListItem>
											</asp:dropdownlist><BR>
											<asp:requiredfieldvalidator id="rfvTERM" runat="server" ControlToValidate="cmbTERM" ErrorMessage="TERM can't be blank."
												Display="Dynamic"></asp:requiredfieldvalidator></TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capEFFECTIVE_FROM_DATE" runat="server">Effective From Date</asp:label><SPAN class="mandatory">*</SPAN></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtEFFECTIVE_FROM_DATE" runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkFROM_DATE" runat="server" CssClass="HotSpot">
												<asp:image id="Image1" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
											</asp:hyperlink><br>
											<asp:requiredfieldvalidator id="rfvEFFECTIVE_FROM_DATE" runat="server" ControlToValidate="txtEFFECTIVE_FROM_DATE"
												ErrorMessage="EFFECTIVE_FROM_DATE can't be blank." Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revEFFECTIVE_FROM_DATE" runat="server" ControlToValidate="txtEFFECTIVE_FROM_DATE"
												ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:regularexpressionvalidator></TD>
										<TD class="midcolora" width="18%"><asp:label id="capEFFECTIVE_TO_DATE" runat="server">Effective To Date</asp:label><SPAN class="mandatory">*</SPAN></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtEFFECTIVE_TO_DATE" runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkTO_DATE" runat="server" CssClass="HotSpot">
												<asp:image id="imgCHECK_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
											</asp:hyperlink><BR>
											<asp:requiredfieldvalidator id="rfvEFFECTIVE_TO_DATE" runat="server" ControlToValidate="txtEFFECTIVE_TO_DATE"
												ErrorMessage="EFFECTIVE_TO_DATE can't be blank." Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revEFFECTIVE_TO_DATE" runat="server" ControlToValidate="txtEFFECTIVE_TO_DATE"
												ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:regularexpressionvalidator><asp:customvalidator id="csvEFFECTIVE_TO_DATE" ControlToValidate="txtEFFECTIVE_TO_DATE" Display="Dynamic" Runat="server"
												ClientValidationFunction="ChkDate"></asp:customvalidator></TD>
									</tr>
									<tr>
										<td class="midcolora" width="18%"><asp:label id="capCOMMISSION_PERCENT" runat="server">Credit %</asp:label><SPAN class="mandatory">*</SPAN>
										</td>
										<TD class="midcolora" width="32%"><asp:textbox id="txtCOMMISSION_PERCENT" style="TEXT-ALIGN: right" runat="server" CssClass="INPUTCURRENCY"
												size="6" maxlength="5"></asp:textbox><br>
											<asp:requiredfieldvalidator id="rfvCOMMISSION_PERCENT" runat="server" ControlToValidate="txtCOMMISSION_PERCENT"
												ErrorMessage="COMMISSION_PERCENT can't be blank." Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revCOMMISSION_PERCENT" runat="server" ControlToValidate="txtCOMMISSION_PERCENT"
												ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:regularexpressionvalidator><asp:customvalidator id="csvCOMMISSION_PERCENT" ControlToValidate="txtCOMMISSION_PERCENT" Display="Dynamic"
												Runat="server" ClientValidationFunction="Validate"></asp:customvalidator></TD>
										<td class="midcolora" width="18%"><asp:label id="capREMARKS" runat="server">Remarks</asp:label></td>
										<TD class="midcolora" width="32%"><asp:textbox onkeypress="MaxLength(this,256);" id="txtREMARKS" runat="server" Width="250px" maxlength="250"
												TextMode="MultiLine"></asp:textbox><br>
										</TD>
									</tr>
									<tr>
										<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate"></cmsb:cmsbutton></td>
										<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnCopy" runat="server" Text="Copy" CausesValidation="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
									</tr>
								</tbody>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
			<INPUT id="hidCOM_ID" type="hidden" value="0" name="hidCOM_ID" runat="server"> <INPUT id="hidCOUNTRY_ID" type="hidden" name="hidCOUNTRY_ID" runat="server">
			<INPUT id="hidState" type="hidden" value="" name="hidState" runat="server">
            <INPUT id="hidLobXMLForClass" type="hidden" name="hidLobXMLForClass" runat="server"> 
            <INPUT id="hidLOB_ID" type="hidden" name="hidLOB_ID" runat="server"> 
		</FORM>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none; Z-INDEX: 101; POSITION: absolute"><iframe id="lookupLayerIFrame" style="DISPLAY: none; Z-INDEX: 1001; FILTER: alpha(opacity=0); BACKGROUND-COLOR: #000000"
				width="0" height="0" top="0px;" left="0px"></iframe>
		</div>
		<DIV id="lookupLayer" onmouseover="javascript:refreshLookupLayer();" style="Z-INDEX: 101; VISIBILITY: hidden; POSITION: absolute">
			<table class="TabRow" cellSpacing="0" cellPadding="2" width="100%">
				<tr class="SubTabRow">
					<td><b>Add LookUp</b></td>
					<td>
						<p align="right"><A onclick="javascript:hideLookupLayer();" href="javascript:void(0)"><IMG height="14" alt="Close" src="/cms/cmsweb/images/cross.gif" width="16" border="0"></A></p>
					</td>
				</tr>
				<tr class="SubTabRow">
					<td colSpan="2"><span id="LookUpMsg"></span></td>
				</tr>
			</table>
		</DIV>
		<!-- For lookup layer ends here-->
		<script>							
				RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidCOM_ID').value);
		</script>
	</BODY>
</HTML>
