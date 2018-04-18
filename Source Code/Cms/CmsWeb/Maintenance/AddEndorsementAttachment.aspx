<%@ Page language="c#" validateRequest=false Codebehind="AddEndorsementAttachment.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.AddEndorsementAttachment" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>MNT_ENDORSEMENT_ATTACHMENT</title>
		<meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language='javascript'>
		var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		
		function CompareDateWithCurrentDate(objSource , objArgs)
		{
		var validDate=document.getElementById('txtVALID_DATE').value;
		var currentDate=new Date();
		//currentDate.setDate(currentDate.getDate()-1);alert(currentDate);return;
		currentDate=currentDate.toDateString();		
		//alert(currentDate.toDateString());
		
		/*if (validDate != "")
		{		
			objArgs.IsValid = CompareTwoDate(validDate,currentDate,jsaAppDtFormat);
		}*/
		//Removed check for current date
		objArgs.IsValid = true;
		
	   }	
		
		function CheckEndDate(objSource , objArgs)
		{
			var startdate=document.getElementById('txtVALID_DATE').value;
			var enddate=document.getElementById('txtEFFECTIVE_TO_DATE').value;
			objArgs.IsValid = CompareTwoDate(startdate,enddate,jsaAppDtFormat);
		}
		
		function CheckDisabledDate(objSource , objArgs)
		{
			var disableddate=document.getElementById('txtDISABLED_DATE').value;
			var enddate=document.getElementById('txtEFFECTIVE_TO_DATE').value;
			objArgs.IsValid = CompareTwoDate(enddate,disableddate,jsaAppDtFormat);
			
		}	
		/*function CompareTwoDate(DateFirst, DateSec, FormatOfComparision)
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
			if(firstSpan >= secSpan) 
				return true;	// first is greater
			else 
				return false;	// secound is greater
		}*/
		
		function CompareTwoDate(DateFirst, DateSec, FormatOfComparision)
		{
			if(DateFirst == "" ||DateFirst ==null )
				return false;
			if(DateSec == "" || DateSec==null)
				return true;
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
			if(firstSpan <= secSpan) 
				return true;	// first is less than or equal
			else 
				return false;	// First date is greater
		}
		
		
		
		function AddData()
		{ 	
			ChangeColor();
			DisableValidators();
			document.getElementById('hidENDORSEMENT_ATTACH_ID').value	=	'New';
			//document.getElementById('txtATTACH_FILE').focus();
			document.getElementById("txtATTACH_FILE").style.display = "inline";
			document.getElementById('txtVALID_DATE').value  = '';
			document.getElementById("rfvATTACH_FILE").setAttribute('enabled',true);
			document.getElementById("rfvATTACH_FILE").setAttribute('isValid',true);
			document.getElementById('MNT_ENDORSEMENT_ATTACHMENT').reset();
			//document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
		}
		function FileExist()//Added for Itrack Issue 5906 on 8 June 09
		{
			if('<%=flagFileCheck%>' == 0)
			{
			  alert('File not found.');
			  return false;
			}
		}
		
		function populateXML()
		{
			if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
			{
				if(document.getElementById('hidOldData').value!="" && document.getElementById('hidOldData').value!='<NewDataSet />')
				{
						
					//Enabling the activate deactivate button
					// if(document.getElementById('btnActivateDeactivate'))
					//document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
					//Storing the XML in hidRowId hidden fields 
					populateFormData(document.getElementById('hidOldData').value,MNT_ENDORSEMENT_ATTACHMENT);
					document.getElementById("revATTACH_FILE").style.display='none';//Added for ItrackIssue 5553 on 9 march 2009
					document.getElementById("rfvATTACH_FILE").setAttribute('isValid',false);
					document.getElementById("rfvATTACH_FILE").style.display='none';
					document.getElementById("rfvATTACH_FILE").setAttribute('enabled',false);
		           // document.getElementById("txtATTACH_FILE").style.display='none';
		          //  document.getElementById("trBody").style.display='none';
		         //   alert(document.getElementById('hidOldData').value);
					var objXmlHandler = new XMLHandler();
					var tree = (objXmlHandler.quickParseXML(document.getElementById('hidOldData').value).getElementsByTagName('Table')[0]);
					
					var i=0;
					for(i=0;i<tree.childNodes.length;i++)
					{
						if(!tree.childNodes[i].firstChild) 
							continue;
						
						var nodeName = tree.childNodes[i].nodeName;
						var nodeValue = tree.childNodes[i].firstChild.text;
						
						var fileName;
						//alert(document.getElementById('hidOldData').value);
						switch(nodeName)
						{
							case "ATTACH_FILE":
								//document.getElementById("lblATTACH_FILE").innerHTML = "<a href = '" + document.getElementById("hidRootPath").value +  nodeValue + "' target='blank'>" + nodeValue + "</a>";
								document.getElementById("lblATTACH_FILE").innerHTML = "<a href = '" + document.getElementById("hidfileLink").value + "' target='blank' onclick='return FileExist();'>" + nodeValue + "</a>";//Added for Itrack Issue 5906 on 8 June 09
								//document.getElementById("txtATTACH_FILE").value = 'nodeValue';
								document.getElementById("hidATTACH_FILE").value = nodeValue;
								//document.getElementById("txtATTACH_FILE").style.display = "none";
								break;
							case "VALID_DATE":
								//alert(nodeValue);
								//document.getElementById("lblVALID_DATE").style.display = "none";
								//document.getElementById("txtVALID_DATE").style.display = "none";
								//document.getElementById("hlkVALID_DATE").style.display = "none";
								//document.getElementById("imgVALID_DATE").style.display = "none";
								break;
						}
					}
				}
				else
				{
					AddData();
					document.getElementById("btnDelete").disabled=true;//Done for Itrack Issue 5706
				}
		}
		return false;
		}

		function SetFileType()
		{

			if (document.getElementById("hidENDORSEMENT_ATTACH_ID").value == "New")
			{	
				var FileName = MNT_ENDORSMENT_DETAILS.txtATTACH_FILE.value;
				if (FileName != "")
				{
					var Index = FileName.lastIndexOf(".");
					if (Index != -1)
					{
						FileName = FileName.substring(Index + 1);
					}
				}
				//document.getElementById("txtATTACH_FILE_TYPE").value = FileName;
				//document.getElementById("txtATTACH_FILE_TYPE").setAttribute("readOnly",true);		
			}
		}	
		
		function deleteClick()
		{
			document.getElementById("trMain").style.display = 'none';
			document.getElementById("trBelow").style.display = 'none';
			document.getElementById("trFile").style.display = 'none';
			document.getElementById("trBody").style.display = 'none';
			document.getElementById('hidDELETE').value = 1;
		}	
		
		</script>
	</HEAD>
	<BODY leftMargin='0' topMargin='0' onload='populateXML();ApplyColor();'>
		<FORM id='MNT_ENDORSEMENT_ATTACHMENT' method='post' runat='server'>
			<TABLE cellSpacing='0' cellPadding='0' width='100%' border='0'>
				<TBODY>
					<TR>
						<TD>
							<TABLE width='100%' border='0' align='center'>
								<TBODY>
									<tr>
										<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
											mandatory</TD>
									</tr>
									<tr>
										<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
									</tr>
									<tr id="trMain">
										<TD class='midcolora' width='18%'>
											<asp:Label id="capVALID_DATE" runat="server">Start Date</asp:Label><span class="mandatory">*</span></TD>
										<TD class='midcolora' width='32%'>
											<asp:textbox id='txtVALID_DATE' runat='server' size='12' maxlength='10'></asp:textbox>
											<asp:hyperlink id="hlkVALID_DATE" runat="server" CssClass="HotSpot">
												<ASP:IMAGE id="imgVALID_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
											</asp:hyperlink><br>
											<asp:requiredfieldvalidator id="rfvVALID_DATE" runat="server" ControlToValidate="txtVALID_DATE" ErrorMessage="START_DATE can't be blank."
												Display="Dynamic"></asp:requiredfieldvalidator>
											<asp:RegularExpressionValidator id="revVALID_DATE" runat="server" Display="Dynamic" ControlToValidate="txtVALID_DATE"></asp:RegularExpressionValidator>
											<asp:customvalidator id="csvVALID_DATE" Display="Dynamic" ControlToValidate="txtVALID_DATE" ClientValidationFunction="CompareDateWithCurrentDate"
												Runat="server"></asp:customvalidator>
										</TD>
										<td class="midcolora" width="18%"><asp:label id="capEFFECTIVE_TO_DATE" Runat="server">End Date</asp:label></td>
										<td class="midcolora" width="32%"><asp:textbox id="txtEFFECTIVE_TO_DATE" Runat="server" maxlength="10" size="12"></asp:textbox><asp:hyperlink id="hlkEFFECTIVE_TO_DATE" runat="server" CssClass="HotSpot">
												<asp:Image runat="server" ID="imgEFFECTIVE_TO_DATE" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></asp:Image>
											</asp:hyperlink><br>
											<asp:regularexpressionvalidator id="revEFFECTIVE_TO_DATE" Runat="server" ControlToValidate="txtEFFECTIVE_TO_DATE"
												Display="Dynamic"></asp:regularexpressionvalidator><asp:customvalidator id="csvEFFECTIVE_TO_DATE" Runat="server" ControlToValidate="txtEFFECTIVE_TO_DATE"
												Display="Dynamic" ClientValidationFunction="CheckEndDate"></asp:customvalidator>
										</td>
									</tr>
									<tr id="trBelow">
										<td class="midcolora" width="18%"><asp:label id="capDISABLED_DATE" Runat="server">Disabled Date</asp:label></td>
										<td class="midcolora" width="32%"><asp:textbox id="txtDISABLED_DATE" Runat="server" maxlength="10" size="12"></asp:textbox><asp:hyperlink id="hlkDISABLED_DATE" runat="server" CssClass="HotSpot">
												<asp:Image runat="server" ID="imgDISABLED_DATE" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></asp:Image>
											</asp:hyperlink><br>
											<asp:regularexpressionvalidator id="revDISABLED_DATE" Runat="server" ControlToValidate="txtDISABLED_DATE" Display="Dynamic"></asp:regularexpressionvalidator><asp:customvalidator id="csvDISABLED_DATE" Runat="server" ControlToValidate="txtDISABLED_DATE" Display="Dynamic"
												ClientValidationFunction="CheckDisabledDate"></asp:customvalidator>
										</td>
										<td class="midcolora" width="18%"><asp:label id="capFORM_NUMBER" Runat="server">Form Number</asp:label></td>
										<td class="midcolora" width="32%"><asp:textbox id="txtFORM_NUMBER" Runat="server" size="35" MaxLength="20"></asp:textbox></td>
									</tr>
									<tr id="trFile">
										<TD class="midcolora" width="18%"><asp:Label id="capATTACH_FILE" runat="server">File Name</asp:Label><span class="mandatory" id="spnFileName" runat="server">*</span></TD>
										<TD class="midcolora" width="32%"><input id="txtATTACH_FILE" type="file" size="30" runat="server" NAME="txtATTACH_FILE" Visible="True" onchange="RemoveSpecialChar(document.getElementById('txtATTACH_FILE').value,document.getElementById('revATTACH_FILE'));RemoveExecutableFiles(document.getElementById('txtATTACH_FILE').value,document.getElementById('revATTACH_FILE_EXT'));AllowPDFFiles(document.getElementById('txtATTACH_FILE').value,document.getElementById('revATTACH_FILE_PDF'));"><br><%--Added for ItrackIssue 5706 on 16 April 2009--%>
											<asp:label id="lblATTACH_FILE" Runat="server"></asp:label><br>
											<asp:requiredfieldvalidator id="rfvATTACH_FILE" runat="server" ControlToValidate="txtATTACH_FILE" Display="Dynamic"></asp:requiredfieldvalidator>
											<asp:regularexpressionvalidator id="revATTACH_FILE" Runat="server" ControlToValidate="txtATTACH_FILE" Display="Dynamic"></asp:regularexpressionvalidator>
											<asp:regularexpressionvalidator id="revATTACH_FILE_EXT" Runat="server" ControlToValidate="txtATTACH_FILE" Display="Dynamic"></asp:regularexpressionvalidator>
											<asp:regularexpressionvalidator id="revATTACH_FILE_PDF" Runat="server" ControlToValidate="txtATTACH_FILE" Display="Dynamic"></asp:regularexpressionvalidator><%--Added for ItrackIssue 5706 on 16 April 2009--%>
										</TD>
										<td class="midcolora" width="18%"><asp:label id="capEDITION_DATE" Runat="server">Edition Date </asp:label>
											<span class="mandatory">*</span></td>
										<td class="midcolora" width="32%"><asp:textbox id="txtEDITION_DATE" Runat="server" maxlength="5" size="12"></asp:textbox><br>
											<asp:requiredfieldvalidator id="rfvEDITION_DATE" runat="server" ControlToValidate="txtEDITION_DATE" ErrorMessage="Please provide Edition Date."
												Display="Dynamic"></asp:requiredfieldvalidator>
											<asp:regularexpressionvalidator id="revEDITION_DATE" Runat="server" ControlToValidate="txtEDITION_DATE" Display="Dynamic"></asp:regularexpressionvalidator>
										</td>
									</tr>
									<tr id="trBody" runat="server">
										<td class='midcolora' align="left" colspan="2">
											<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
											<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text='Delete'></cmsb:cmsbutton>
										</td>
										<td class='midcolorr' colspan="2">
											<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
										</td>
									</tr>
									<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
									<INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
									<INPUT id="hidENDORSEMENT_ATTACH_ID" type="hidden" value="0" name="hidENDORSEMENT_ATTACH_ID"
										runat="server"> <INPUT id="hidDELETE" type="hidden" value="0" name="hidDELETE" runat="server">
									<input id="hidRootPath" type="hidden" runat="server"> <INPUT id="hidATTACH_FILE" type="hidden" name="hidATTACH_FILE" runat="server">
									<INPUT id="hidfileLink" type="hidden" value="" name="hidfileLink" runat="server">
									<TR>
										<TD></TD>
		</FORM>
		<script>
			RefreshWebGrid(1,document.getElementById('hidENDORSEMENT_ATTACH_ID').value);
			if(document.getElementById('hidDELETE').value == 1)
			{
				deleteClick();
				document.getElementById('hidDELETE').value = 0;
			}
		</script>
		</TR></TBODY></TABLE></TD></TR></TBODY></TABLE>
	</BODY>
</HTML>
