<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="PolicyAutoMVR.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.PolicyAutoMVR" validateRequest=false %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>POL_MVR_INFORMATION</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
		var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		function ChkDOB(objSource , objArgs)
		{
			//var currentDate="<%=DateTime.Now.Date.ToString().Substring(0,DateTime.Now.Date.ToString().IndexOf(' '))%>";
			//var currentDate=new Date("<%=DateTime.Now.Date.ToString().Substring(0,DateTime.Now.Date.ToString().IndexOf(' '))%>");
			//alert(currentDate);
			var expdate=document.POL_MVR_INFORMATION.txtMVR_DATE.value;
			objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>",expdate,jsaAppDtFormat);
		}
		
		function ChkOccDate(objSource , objArgs)
		{
			//var currentDate="<%=DateTime.Now.Date.ToString().Substring(0,DateTime.Now.Date.ToString().IndexOf(' '))%>";
			//var currentDate=new Date("<%=DateTime.Now.Date.ToString().Substring(0,DateTime.Now.Date.ToString().IndexOf(' '))%>");
			//alert(currentDate);
			var expdate=document.POL_MVR_INFORMATION.txtOCCURENCE_DATE.value;
			objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>",expdate,jsaAppDtFormat);
		}			
		function	ResetPolMVR()
		{
			populateXML();
		}
function AddData()
{
	ChangeColor();
	DisableValidators();
	//document.getElementById('txtMVR_AMOUNT').value  = '';
	document.getElementById('txtMVR_DATE').value  = '';
	document.getElementById('cmbVIOLATION_ID').options.selectedIndex = 0;
	document.getElementById('cmbVERIFIED').options.selectedIndex=0;
	//document.getElementById('chkMVR_DEATH').checked=false;
	document.getElementById('cmbVIOLATION_TYPE').options.selectedIndex=0;	
	if(document.getElementById('btnActivateDeactivate')!=null)		
		document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
	
}
function FetchViolations()
{
		
	if(document.getElementById('cmbVIOLATION_TYPE')==null || document.getElementById('cmbVIOLATION_TYPE').selectedIndex<0)
	{	
		return;
	}
	var intViolationID = parseInt(document.getElementById('cmbVIOLATION_TYPE').options[document.getElementById('cmbVIOLATION_TYPE').selectedIndex].value);		
	var intCustomerId = parseInt(document.getElementById('hidCustomer_Id').value);
	var intPolicyId = parseInt(document.getElementById('hidPolicy_Id').value);
	var intPolicyVersionId = parseInt(document.getElementById('hidPolicy_Version_Id').value);			
	var calledfrom='';
	PolicyAutoMVR.AjaxGetViolations(intCustomerId,intPolicyId,intPolicyVersionId,intViolationID,'<%=CALLED_FROM%>',PutViolations);
	
	
	ChangeColor();
}
function PutViolations(Result)
{
 if(Result.value!=null)
 {
	var strXML;
	if(Result.error)
	{        
		var xfaultcode   = Result.errorDetail.code;
		var xfaultstring = Result.errorDetail.string;
		var xfaultsoap   = Result.errorDetail.raw;        				
	}
	else	
	{	
		strXML= Result.value;		
		
		//////////
		var xmlDoc = new ActiveXObject("Microsoft.XMLDOM") ;
		xmlDoc.async=false;
		xmlDoc.loadXML(strXML);
		xmlTableNodes = xmlDoc.selectNodes('/NewDataSet/Table');
		
		if(document.getElementById('cmbVIOLATION_ID')==null) return;
		
		document.getElementById('cmbVIOLATION_ID').length=0;
		objViolation = document.getElementById('cmbVIOLATION_ID');
		
		//Adding a blank option first in violations		
		document.getElementById('cmbVIOLATION_ID').options[document.getElementById('cmbVIOLATION_ID').length]= new Option("","");
		
		for(var i = 0; i < xmlTableNodes.length; i++ )
		{
			var text = 	xmlTableNodes[i].selectSingleNode('VIOLATION_DES').text;
			var value = 	xmlTableNodes[i].selectSingleNode('VIOLATION_ID').text;
			document.getElementById('cmbVIOLATION_ID').options[document.getElementById('cmbVIOLATION_ID').length]= new Option(text,value);
		}		
		document.getElementById('cmbVIOLATION_ID').selectedIndex=-1;
		
		if (document.getElementById("hidVIOLATION_ID").value != "")
		{
			
			for(i=0;i<document.getElementById('cmbVIOLATION_ID').options.length;i++)
			{
				
				
				if (document.getElementById('cmbVIOLATION_ID').options[i].value == document.getElementById("hidVIOLATION_ID").value)
				{
					document.getElementById('cmbVIOLATION_ID').options[i].selected=true;
//					document.getElementById('rfvVIOLATION_ID').style.display="none";
					SetPointsAssgn();//Done for Itrack Issue 6024 on 29 June 2009
					ApplyColor();
					ChangeColor();
					return;
				}
				
				
				//if(i>5) return;
			}
			//SelectComboOption("cmbVIOLATION_ID", document.getElementById("hidVIOLATION_ID").value);
		}
				
		//alert(document.getElementById('cmbVIOLATION_ID').options.length);
		//////////
	}
	}
		
}
function populateXML()
{	
	if(document.getElementById('trBody').getAttribute("style").display!="none")
	{
	  //Done for Itrack Issue 6024 on 29 June 2009
	   try
	   {
		document.getElementById('cmbVIOLATION_TYPE').focus();
	   }
	   catch(ex)
	   {
	   }
	}

	if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
	{
		var tempXML;
		if(document.getElementById("hidOldData").value!="" )
		{
			//document.getElementById('btnReset').style.display='none';
			tempXML=document.getElementById("hidOldData").value;
			//Enabling the activate deactivate button
			if(document.getElementById('btnActivateDeactivate')!=null)				
				document.getElementById('btnActivateDeactivate').setAttribute('Enabled',true); 
			populateFormData(tempXML,POL_MVR_INFORMATION);			
			FetchViolations();
		}
		else
		{
			
			AddData();
		}
		ChangeColor();
	}

	FetchViolations();
	CheckIIX(1);
	return false;
	
}

// Added by shafi.
	// for checking "MVR violation date cannot be less than date of birth." 
	function CompareDateWithDOB(objSource , objArgs)
		{
		var mvrDate=document.POL_MVR_INFORMATION.txtMVR_DATE.value;
		var dob=document.getElementById('hidDRIVER_DOB').value;
				
		if (dob != "")
		{
		
			objArgs.IsValid = CompareTwoDate(mvrDate,dob,jsaAppDtFormat);
		}	
		
	}	
	
	function SetViolationDesc()
	{
		if (document.getElementById('cmbVIOLATION_ID').selectedIndex != -1)
		{
			document.getElementById('hidVIOLATION_DES').value = document.getElementById('cmbVIOLATION_ID').item(document.getElementById('cmbVIOLATION_ID').selectedIndex).text;
			return true;
		}
	}
	
		//Added by shafi 
		function CompareTwoDate(DateFirst, DateSec, FormatOfComparision)
		{
			var saperator = '/';
			var firstDate, secDate;
			var strDateFirst = DateFirst.split("/");
			var strDateSec = DateSec.split("/");
			if(FormatOfComparision.toLowerCase() == "dd/mm/yyyy")
			{			
				firstDate = (strDateFirst[1].length != 2 ? '0' + strDateFirst[1] : strDateFirst[1]) + '/' + (strDateFirst[0].length != 2 ? '0' + strDateFirst[0] : strDateFirst[0])  + '/' + (strDateFirst[2].length != 4 ? '0' + strDateFirst[2] : strDateFirst[2]);
				secDate = (strDateSec[1].length != 2 ? '0' + strDateSec[1] : strDateSec[1]) + '/' + (strDateSec[0].length != 2 ? '0' + strDateSec[0] : strDateSec[0])  + '/' + (strDateSec[2].length != 4 ? '0' + strDateSec[2] : strDateSec[2]);
			}
			if(FormatOfComparision.toLowerCase() == "mm/dd/yyyy")
			{				
				firstDate = DateFirst
				secDate = DateSec;
			}
			firstDate = new Date(firstDate);
			secDate = new Date(secDate);
			firstSpan = Date.parse(firstDate);
			secSpan = Date.parse(secDate);
			if(firstSpan > secSpan) 
				return true;	// first is greater
			else 
				return false;	// secound is greater
		}
	function formatCurrencyOnLoad()
	{
		FetchViolations();
		//document.POL_MVR_INFORMATION.txtMVR_AMOUNT.value=formatCurrency(document.POL_MVR_INFORMATION.txtMVR_AMOUNT.value);	
		return false;
	}

	function CheckIIX(calledFrom)
	{
		if(document.getElementById('cmbVIOLATION_TYPE').value >= 15000 && document.getElementById('cmbVIOLATION_TYPE').options[document.getElementById('cmbVIOLATION_TYPE').selectedIndex].text!='SUSPENSION')
		{
			document.getElementById('cmbVIOLATION_ID').style.display = 'none';
			document.getElementById('cmbVIOLATION_ID').options.selectedIndex = 0; //Added Praveen (Setting ID 0))
			document.getElementById('capMVR_VIOLATION').style.display = 'none';
			document.getElementById('spnVIOLATION_ID').style.display = 'none';
			document.getElementById('txtDETAILS').style.display = 'inline';
			document.getElementById('spnDETAILS').style.display = 'inline';
			document.getElementById('spnMVR_DATE').style.display = 'none';
			document.getElementById('rfvMVR_DATE').enabled = false;
			document.getElementById('rfvMVR_DATE').style.display = 'none';
			document.getElementById("txtMVR_DATE").style.backgroundColor="white";

			document.getElementById('spnPOINTS_ASSIGNED').style.display = 'none';
			document.getElementById('rfvPOINTS_ASSIGNED').enabled = false;
			document.getElementById('rfvPOINTS_ASSIGNED').style.display = 'none';
			document.getElementById("txtPOINTS_ASSIGNED").style.backgroundColor="white";

//			document.getElementById('rfvVIOLATION_ID').enabled = false;
//			document.getElementById('rfvVIOLATION_ID').style.display = 'none';
			document.getElementById('rfvDETAILS').enabled = true;
			document.getElementById('csvDETAILS').enabled = true;
//			document.getElementById('rfvDETAILS').style.display = 'inline';
			document.getElementById('txtPOINTS_ASSIGNED').value="0";
		}
		else
		{
			if(calledFrom != '1')
				document.getElementById('txtPOINTS_ASSIGNED').value = '';
			document.getElementById('cmbVIOLATION_ID').style.display = 'none';
			document.getElementById('capMVR_VIOLATION').style.display = 'none';
			document.getElementById('spnVIOLATION_ID').style.display = 'none';
			document.getElementById('txtDETAILS').style.display = 'inline';
			document.getElementById('spnDETAILS').style.display = 'none';
			document.getElementById('spnMVR_DATE').style.display = 'inline';
			document.getElementById('rfvMVR_DATE').enabled = true;
			if (document.getElementById("txtMVR_DATE").value=="")
				document.getElementById("txtMVR_DATE").style.backgroundColor="#FFFFD1";
			else
				document.getElementById("txtMVR_DATE").style.backgroundColor="white";

			document.getElementById('spnPOINTS_ASSIGNED').style.display = 'inline';
			document.getElementById('rfvPOINTS_ASSIGNED').enabled = true;
			document.getElementById('txtPOINTS_ASSIGNED').value = "0";
			if (document.getElementById("txtPOINTS_ASSIGNED").value=="")
				document.getElementById("txtPOINTS_ASSIGNED").style.backgroundColor="#FFFFD1";
			else
				document.getElementById("txtPOINTS_ASSIGNED").style.backgroundColor="white";
//			document.getElementById('rfvMVR_DATE').style.display = 'inline';
//			document.getElementById('rfvVIOLATION_ID').enabled = true;
//			document.getElementById('rfvVIOLATION_ID').style.display = 'inline';
			document.getElementById('rfvDETAILS').enabled = false;
			document.getElementById('rfvDETAILS').style.display = 'none';
			document.getElementById('csvDETAILS').enabled = false;
			document.getElementById('csvDETAILS').style.display = 'none';
		}
		ApplyColor();
		ChangeColor();
	}
	
	function SetPointsAssgn()
	{
		var cmbVio= document.getElementById('cmbVIOLATION_ID');
		var VioDesc = cmbVio.options[cmbVio.selectedIndex].text;
		
		var start_brack = VioDesc.lastIndexOf("(");
		var end_brack = VioDesc.lastIndexOf(")");
		
		if(end_brack > start_brack)
		  document.getElementById('txtPOINTS_ASSIGNED').value = VioDesc.substring(start_brack+1,end_brack);
	}
	
	function ChkTextAreaLength(source, arguments)
	{
		var txtArea = arguments.Value;
		if(txtArea.length > 500 ) 
		{
			arguments.IsValid = false;
			return;   // invalid userName
		}
	}


		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="populateXML();formatCurrencyOnLoad();ApplyColor();">
		<FORM id="POL_MVR_INFORMATION" method="post" runat="server">			
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4">
						<webcontrol:WorkFlow id="myWorkFlow" runat="server"></webcontrol:WorkFlow>
						<asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label>
					</td>
				</tr>
				<!--<tr>
					<TD class="pageHeader" colSpan="4">
						
					</TD>
				</tr>-->
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
										mandatory</TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colspan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capVIOLATION_TYPE" runat="server">Violation/Note Type</asp:label><span class="mandatory">*</span>
									</TD>
									<TD class="midcolora" colspan="3"><asp:dropdownlist id="cmbVIOLATION_TYPE" Runat="server"></asp:dropdownlist><br>
										<asp:RequiredFieldValidator ID="rfvVIOLATION_TYPE" ControlToValidate="cmbVIOLATION_TYPE" Runat="server" Display="Dynamic"></asp:RequiredFieldValidator></TD>
									<!--<TD class="midcolora" colspan="2">&nbsp;</TD>-->
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capMVR_VIOLATION" runat="server">Violation</asp:label><span id="spnVIOLATION_ID" class="mandatory">*</span>
									</TD>
									<TD class="midcolora" colspan="3"><asp:dropdownlist id="cmbVIOLATION_ID" onChange="SetPointsAssgn();" Runat="server"></asp:dropdownlist>
									
										<%--<asp:RequiredFieldValidator ID="rfvVIOLATION_ID" Runat="server" ControlToValidate="cmbVIOLATION_ID" Display="Dynamic"></asp:RequiredFieldValidator>--%>
									</TD>
								</tr>
								<TD class="midcolora" width="18%"><asp:label id="capDETAILS" runat="server">Violation Details</asp:label><span id="spnDETAILS" class="mandatory">*</span>
									</TD>
									<TD class="midcolora" colspan="3"><asp:textbox id="txtDETAILS" runat="server" size="18" maxlength="500" Width="446px" Height="80px"
											TextMode="MultiLine"></asp:textbox><br>
											<asp:requiredfieldvalidator id="rfvDETAILS" Runat="server" Display="Dynamic" ControlToValidate="txtDETAILS"
											ErrorMessage="Please provide MVR Details."></asp:requiredfieldvalidator><asp:customvalidator id="csvDETAILS" Runat="server" Display="Dynamic" ControlToValidate="txtDETAILS" ErrorMessage="Maximum length of Violation Details is 500."
											ClientValidationFunction="ChkTextAreaLength"></asp:customvalidator> </TD>
								<tr>
								
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capOCCURENCE_DATE" runat="server">Occurrence Date</asp:label><span class="mandatory">*</span>
									</TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtOCCURENCE_DATE" runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkOCCURENCE_DATE" runat="server" CssClass="HotSpot">
											<asp:Image runat="server" ID="imgOCCURENCE_DATE" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></asp:Image>
										</asp:hyperlink><BR>
										<asp:requiredfieldvalidator id="rfvOCCURENCE_DATE" runat="server" Display="Dynamic" ControlToValidate="txtOCCURENCE_DATE"
											ErrorMessage="Please enter Date of Occurrence."></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revOCCURENCE_DATE" runat="server" Display="Dynamic" ControlToValidate="txtOCCURENCE_DATE"
											ErrorMessage="Please enter Date of Occurrence in dd/MM/yyyy format"></asp:regularexpressionvalidator><asp:customvalidator id="csvOCCURENCE_DATE" Runat="server" Display="Dynamic" ControlToValidate="txtOCCURENCE_DATE"
											ErrorMessage="Date of Occurrence can't be more than current date." ClientValidationFunction="ChkOccDate"></asp:customvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capMVR_DATE" runat="server">Date</asp:label><span class="mandatory" id="spnMVR_DATE">*</span>
									</TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtMVR_DATE" runat="server" maxlength="10" size="12"></asp:textbox><asp:hyperlink id="hlkDRIVER_MVR_DATE" runat="server" CssClass="HotSpot">
											<asp:Image runat="server" ID="imgCO_APPL_DOB" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></asp:Image>
										</asp:hyperlink><br>
										<asp:requiredfieldvalidator id="rfvMVR_DATE" runat="server" Display="Dynamic" ErrorMessage="MVR_DATE can't be blank."
											ControlToValidate="txtMVR_DATE"></asp:requiredfieldvalidator>
										<asp:regularexpressionvalidator id="revMVR_DATE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtMVR_DATE"></asp:regularexpressionvalidator>
										<asp:customvalidator id="csvMVR_DATE" Runat="server" Display="Dynamic" ControlToValidate="txtMVR_DATE"
											ClientValidationFunction="ChkDOB"></asp:customvalidator>
										<asp:customvalidator id="csvMVR_DATE_COMPARE" Display="Dynamic" ControlToValidate="txtMVR_DATE" ClientValidationFunction="CompareDateWithDOB"
											Runat="server"></asp:customvalidator></TD>
									<!--<TD class="midcolora"><asp:label id="capMVR_AMOUNT" runat="server">Amount</asp:label>
									</TD>
									<TD class="midcolora"><asp:textbox id="txtMVR_AMOUNT" runat="server" CssClass="InputCurrency" maxlength="10" size="18"></asp:textbox><BR>
										</TD>-->
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPOINTS_ASSIGNED" runat="server">Points Assigned</asp:label><span class="mandatory" id="spnPOINTS_ASSIGNED">*</span>
									</TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtPOINTS_ASSIGNED" runat="server" size="12" maxlength="2" readOnly="true"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvPOINTS_ASSIGNED" runat="server" Display="Dynamic" ControlToValidate="txtPOINTS_ASSIGNED"
											ErrorMessage="Please enter Points Assigned."></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revPOINTS_ASSIGNED" runat="server" Display="Dynamic" ControlToValidate="txtPOINTS_ASSIGNED"
											ErrorMessage="Please provide only integer."></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capADJUST_VIOLATION_POINTS" runat="server">Adjust Violation Points</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtADJUST_VIOLATION_POINTS" runat="server" size="12" maxlength="2"></asp:textbox><BR>
										<asp:regularexpressionvalidator id="revADJUST_VIOLATION_POINTS" runat="server" Display="Dynamic" ControlToValidate="txtADJUST_VIOLATION_POINTS"
											ErrorMessage="Please provide only integer."></asp:regularexpressionvalidator></TD>
									<!--<TD class="midcolora" width="18%"><asp:label id="capMVR_DEATH" runat="server">Death</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:checkbox id="chkMVR_DEATH" runat="server"></asp:checkbox></TD>-->
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capVERIFIED" runat="server">Entered</asp:label></TD>
									<TD class="midcolora" width="32%" colspan="3"><asp:dropdownlist id="cmbVERIFIED" runat="server"></asp:dropdownlist></TD>
									<!--<TD class="midcolora" colspan="2">&nbsp;</TD>-->
								</tr>
								<tr>
									<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>&nbsp;<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="" CausesValidation="False"></cmsb:cmsbutton></td>
									<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" CausesValidation="False"></cmsb:cmsbutton>&nbsp;<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
								</tr>
								<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
								<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
								<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
								<INPUT id="hidDriver_Id" type="hidden" value="0" name="hidDriver_Id" runat="server">
								<INPUT id="hidCustomer_Id" type="hidden" value="0" name="hidCustomer_Id" runat="server">
								<INPUT id="hidPolicy_Id" type="hidden" value="0" name="hidPolicy_Id" runat="server">
								<INPUT id="hidPolicy_Version_Id" type="hidden" value="0" name="hidPolicy_Version_Id" runat="server">
								<INPUT id="hidPOL_MVR_ID" type="hidden" value="0" name="hidPOL_MVR_ID" runat="server">
								<INPUT id="hidPOL_WATER_MVR_ID" type="hidden" value="0" name="hidPOL_WATER_MVR_ID" runat="server">
								<INPUT id="hidDRIVER_DOB" type="hidden" value="0" name="hidDRIVER_DOB" runat="server">
								<INPUT id="hidVIOLATION_ID" type="hidden" name="hidVIOLATION_ID" runat="server">
								<INPUT id="hidPOL_UMB_MVR_ID" type="hidden" value="0" name="hidPOL_UMB_MVR_ID" runat="server">
								<INPUT id="hidVIOLATION_DES" type="hidden" name="hidVIOLATION_DES" runat="server">
							</TBODY>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidPOL_MVR_ID').value,true);
		</script>
	</BODY>
</HTML>
