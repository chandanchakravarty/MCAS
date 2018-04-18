<%@ Page validateRequest=false language="c#" Codebehind="AddSubRanges.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Accounting.AddSubRanges" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>Define Sub Ranges</title>
		<meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language='javascript'>
function AddData()
{
ChangeColor();
DisableValidators();
document.getElementById('hidCATEGORY_ID').value	=	'New';
//added by uday get focus on hidden text
try
{	 
document.getElementById('cmbPARENT_CATEGORY_ID').focus();
}
catch(e)
{
 return false;
}	
//document.getElementById('cmbPARENT_CATEGORY_ID').focus();
document.getElementById('cmbPARENT_CATEGORY_ID').options.selectedIndex = 0;
document.getElementById('cmbfracRangeFrom').options.selectedIndex = 0;
document.getElementById('cmbfracRangeTo').options.selectedIndex = 0;
document.getElementById('txtCATEGORY_DESC').value  = '';
document.getElementById('txtwholeRangeFrom').value  = '';
document.getElementById('txtwholeRangeTO').value  = '';
if(document.getElementById('btnDelete'))
document.getElementById('btnDelete').setAttribute("disabled",true);


}
var fOldData; var tOldData;
function LoadOldData()
{
if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
{
	fOldData = document.getElementById('txtwholeRangeFrom').value + document.getElementById('cmbfracRangeFrom').options[document.getElementById('cmbfracRangeFrom').selectedIndex].text;
	tOldData = document.getElementById('txtwholeRangeTo').value + document.getElementById('cmbfracRangeTo').options[document.getElementById('cmbfracRangeTo').selectedIndex].text;
	document.getElementById('hidfOldData').value = fOldData;
	document.getElementById('hidtOldData').value = tOldData;
}
else
{
	fOldData = document.getElementById('hidfOldData').value ;
	tOldData = document.getElementById('hidtOldData').value ;
}
}
function populateXML()
{
var tempXML = document.getElementById('hidOldData').value;
//alert(document.getElementById('hidFormSaved').value);
//alert(tempXML);
if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
{

	if(tempXML!="")
	{
		populateFormData(tempXML,ACT_GL_ACCOUNT_RANGES);
		LoadOldData();
	}
	else
	{
		AddData();
	}
	
}
WriteRange();
LoadOldData();
return false;
}
function WriteRange() 
{
    var strFor = document.getElementById('hidFor').value;
    var strRange = document.getElementById('hidRange').value;
    var strAnd = document.getElementById('hidAnd').value;
	var index = document.getElementById('cmbPARENT_CATEGORY_ID').options[document.getElementById('cmbPARENT_CATEGORY_ID').selectedIndex].value;
	var mainTypeName = document.getElementById('cmbPARENT_CATEGORY_ID').options[document.getElementById('cmbPARENT_CATEGORY_ID').selectedIndex].text;
	var message = strFor + " " + mainTypeName + " " + strRange + " " + mainRangesFrom[index] + " " + strAnd + " " + mainRangesTo[index];
//	var message = "For "+mainTypeName+" type, account number range should be between "+mainRangesFrom[index]+" and "+mainRangesTo[index];
	document.getElementById('lblRangeLimit').innerHTML = message;
}
function CheckNewRanges()
{
	Page_ClientValidate();
	if(document.getElementById('txtwholeRangeFrom').value.length==0)
		return false;
	var index =  document.getElementById('cmbPARENT_CATEGORY_ID').options[document.getElementById('cmbPARENT_CATEGORY_ID').selectedIndex].value;
	var fRange = parseFloat(document.getElementById('txtwholeRangeFrom').value + document.getElementById('cmbfracRangeFrom').options[document.getElementById('cmbfracRangeFrom').selectedIndex].text);
	var tRange = parseFloat(document.getElementById('txtwholeRangeTo').value + document.getElementById('cmbfracRangeTo').options[document.getElementById('cmbfracRangeTo').selectedIndex].text);

	if (fRange >= tRange) 
	{
	    var strrange1 = document.getElementById('hidRange1').value;
	    alert(strrange1);
		//alert("To Range must be greater than From range!");
		SelectFrom();
		return false;
	}
	
	//======CHECKING MAIN TYPE RANGES
	if(fRange < mainRangesFrom[index])
	{
	    var strRange2 = document.getElementById('hidRange2').value;
	    alert(strRange2+" "+ mainRangesFrom[index] + " !");
//		alert("From range can not be less than "+mainRangesFrom[index]+" !");
		SelectFrom();
		return false;
	}
	if(fRange >= mainRangesTo[index])
	{

	    var strRange3 = document.getElementById('hidRange3').value;
	    alert(strRange3+" " + mainRangesTo[index] + " !");
//		alert("From range must be less than "+ mainRangesTo[index]+" !");
		SelectFrom();
		return false;
	}
	if(tRange >mainRangesTo[index] )
	{
	    var strRange4 = document.getElementById('hidRange4').value;
	    alert(strRange4+" " + mainRangesTo[index] + " !");
//		alert("To range can not be more than "+mainRangesTo[index]+" !");
		SelectTo();
		return false;
	}
	if(tRange < mainRangesFrom[index] )
	{
	    var strRange5 = document.getElementById('hidRange5').value;
	    alert(strRange5+" " + mainRangesFrom[index] + " !");
//		alert("To range must be more than "+mainRangesFrom[index]+" !");
		SelectTo();
		return false;
	}
	//END ======CHECKING MAIN TYPE RANGES
	
	//CHECKING SUB TYPE RANGES	
	if(subRangesFrom[index])
	{
		for(var i=0;i<subRangesFrom[index].length;i++)
		{
			if(fRange>=subRangesFrom[index][i] && tRange <=subRangesTo[index][i])
			{	
				if(fOldData)
				{
					if(fRange>=fOldData && tRange <=tOldData)
					{
						continue;
					}
				}
				var strRANGE_n = document.getElementById('hidRANGE_n').value;
				var str_assign = document.getElementById('hid_assign').value;
				var strFrom = document.getElementById('hidFrom').value;
				alert(strRANGE_n+" "+ subRangesFrom[index][i] + " -- " + subRangesTo[index][i] + str_assign+" - " + categoryDesc[index][i] + ", "+strFrom);
				//alert("Range "+subRangesFrom[index][i]+" -- "+subRangesTo[index][i]+" is already assigned to - "+categoryDesc[index][i]+", From Range -- To Range can not fall in this range.");
				SelectFrom();
				return false;
			}
			if(tRange>=subRangesFrom[index][i] && tRange <=subRangesTo[index][i])
			{	
				if(fOldData)
				{
					if(tRange>=fOldData && tRange <=tOldData)
					{
						continue;
					}
				}
				var strTo = document.getElementById('hidTo').value;
				alert(strRANGE_n+" "+subRangesFrom[index][i]+" -- "+subRangesTo[index][i]+ str_assign+"  - "+categoryDesc[index][i]+", "+strTo);
				//alert("Range "+subRangesFrom[index][i]+" -- "+subRangesTo[index][i]+" is already assigned to - "+categoryDesc[index][i]+", To Range can not fall in this range.");
				SelectTo();
				return false;
			}
			if(fRange>=subRangesFrom[index][i] && fRange <=subRangesTo[index][i])
			{	
				if(fOldData)
				{
					if(fRange>=fOldData && fRange <=tOldData)
					{
						continue;
					}
				}
				var strRangeFall = document.getElementById('hidRangefall').value;
				alert(strRANGE_n+" " + subRangesFrom[index][i] + " -- " + subRangesTo[index][i] +str_assign +"  - " + categoryDesc[index][i] + ", "+strRangeFall);
				//alert("Range "+subRangesFrom[index][i]+" -- "+subRangesTo[index][i]+" is already assigned to - "+categoryDesc[index][i]+", From Range can not fall in this range.");
				SelectFrom();
				return false;
			}
		}
// Calling lient side validate function
		Page_ClientValidate();
   	}
	//END:CHECKING SUB TYPE RANGES
}
function SelectTo()
{
	document.getElementById('txtwholeRangeTo').select();
}
function SelectFrom()
{
	document.getElementById('txtwholeRangeFrom').select();
}

		</script>
</HEAD>
	<BODY leftMargin='0' topMargin='0' onload='populateXML();ApplyColor();'>
		<FORM id='ACT_GL_ACCOUNT_RANGES' method='post' runat='server'>
			<TABLE cellSpacing='0' cellPadding='0' width='100%' border='0'>
				<TR>
					<TD>
						<TABLE width='100%' border='0' align='center'>
							<tr>
								<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblRangeLimit" runat="server" CssClass="errmsg"></asp:label></td>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capPARENT_CATEGORY_ID" runat="server">Main Type</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora'>
									<asp:DropDownList id='cmbPARENT_CATEGORY_ID' onchange="WriteRange()" OnFocus="SelectComboIndex('cmbPARENT_CATEGORY_ID')"
										runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvPARENT_CATEGORY_ID" runat="server" ControlToValidate="cmbPARENT_CATEGORY_ID"
										ErrorMessage="" Display="Dynamic"></asp:requiredfieldvalidator><%--PARENT_CATEGORY_ID can't be blank.--%>
								</TD>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capCATEGORY_DESC" runat="server">Sub Type Description</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtCATEGORY_DESC' runat='server' size='30' maxlength='70'></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvCATEGORY_DESC" runat="server" ControlToValidate="txtCATEGORY_DESC" ErrorMessage=""
										Display="Dynamic"></asp:requiredfieldvalidator><%--CATEGORY_DESC can't be blank.--%>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capRANGE_FROM" runat="server">From</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtwholeRangeFrom' runat='server' size='10' maxlength='5' CssClass="INPUTCURRENCY"></asp:textbox>
									<asp:DropDownList id="cmbfracRangeFrom" runat="server"></asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvRANGE_FROM" runat="server" ControlToValidate="txtwholeRangeFrom" ErrorMessage=""
										Display="Dynamic"></asp:requiredfieldvalidator><%--RANGE_FROM can't be blank.--%>
									<asp:regularexpressionvalidator id="revwholeRangeFrom" runat="server" Display="Dynamic" ErrorMessage=""
										ControlToValidate="txtwholeRangeFrom"></asp:regularexpressionvalidator><%--RegularExpressionValidator--%>
								</TD>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capRANGE_TO" runat="server">To</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtwholeRangeTO' runat='server' size='10' maxlength='5' CssClass="INPUTCURRENCY"></asp:textbox>
									<asp:DropDownList id="cmbfracRangeTo" runat="server"></asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvRANGE_TO" runat="server" ControlToValidate="txtwholeRangeTO" ErrorMessage=""
										Display="Dynamic"></asp:requiredfieldvalidator><%--RANGE_TO can't be blank.--%>
									<asp:regularexpressionvalidator id="revwholeRangeTO" runat="server" Display="Dynamic" ErrorMessage=""
										ControlToValidate="txtwholeRangeTO"></asp:regularexpressionvalidator><%--RegularExpressionValidator--%>
								</TD>
							</tr>
							<tr>
								<td class='midcolora' colspan='2'>
									<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text='Delete'></cmsb:cmsbutton>
								</td>
								<td class='midcolorr' colspan="2">
									<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
								</td>
							</tr>
							<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
							<INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
							<INPUT id="hidCATEGORY_ID" type="hidden" name="hidCATEGORY_ID" runat="server">
							<INPUT id="hidfOldData" type="hidden" name="hidfOldData" runat="server">
							<INPUT id="hidtOldData" type="hidden" name="hidtOldData" runat="server">
							<INPUT id="hidFor" type="hidden" runat="server">
							<INPUT id="hidRange" type="hidden" runat="server">
							<INPUT id="hidAnd" type="hidden" runat="server">
							<INPUT id="hidRange5" type="hidden" runat="server">
							<INPUT id="hidRange1" type="hidden" runat="server">
							<INPUT id="hidRange2" type="hidden" runat="server">
							<INPUT id="hidRange3" type="hidden" runat="server">
							<INPUT id="hidRange4" type="hidden" runat="server">
							<INPUT id="hidRANGE_n" type="hidden" runat="server">
							<INPUT id="hid_assign" type="hidden" runat="server">
							<INPUT id="hidFrom" type="hidden" runat="server">
							<INPUT id="hidTo" type="hidden" runat="server">
							<INPUT id="hidRangefall" type="hidden" runat="server">
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidCATEGORY_ID').value,false);
		</script>
	</BODY>
</HTML>
