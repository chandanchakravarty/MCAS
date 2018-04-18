<%@ Page language="c#" Codebehind="VerifiedAddressList.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.VerifiedAddressList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>EBIX Advantage -<%=header%></title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
    <script language="javascript">
    
    function InsertRow()
    { 
		
		window.opener.top.topframe.add1.value = event.srcElement.parentElement.children[0].innerText;
		//window.opener.top.topframe.add2.value = event.srcElement.parentElement.children[1].innerText;
		window.opener.top.topframe.District.value = event.srcElement.parentElement.children[1].innerText;
		window.opener.top.topframe.city.value = event.srcElement.parentElement.children[2].innerText;
		
		var state = window.opener.top.topframe.state;
		SelectComboOptionByText(state,event.srcElement.parentElement.children[3].innerText);
		window.opener.top.topframe.zip.value = event.srcElement.parentElement.children[4].innerText;
		window.close();
    }
    function SelectComboOptionByText(comboId,selectedValue)
    {
	    for(var j=0; j<comboId.options.length; j++)
	    {
	    if(selectedValue == comboId.options[j].text)
		    {
			    comboId.options.selectedIndex = j;
			    break;
		    }
	    }
    }

    function showAddress()
    {
      
		var strAddress = "";
		strAddress = "<table  width='100%'>"
			+ "<tr><td colspan=6 class='headereffectcenter'>"+'<%= lblHeaderEffectCenter %> '+"</td></tr>"
			+ "<tr><td class='midcolora'><span class='labelfont'>" + '<%= lblAddress %> ' + "</span></td><td class='midcolora'><span class='labelfont'>" + '<%= lblDistrict %> ' + " </span></td> <td class='midcolora'><span class='labelfont'>" + '<%= lblCity %> ' + "</span></td><td class='midcolora'><span class='labelfont'>" + '<%= lblState %> ' + "</span></td><td class='midcolora'><span class='labelfont'>" + '<%= lblZip %> ' + "</span></td></tr>"
		
		var AddressProperty = window.opener.top.topframe.VerifyAddressListArray;
		
		for (var i = 0; i < AddressProperty.length; i++)
		{
			var str;
			if (AddressProperty[i].Address1n2 != null)
			{
				strAddress = strAddress + "<td style='CURSOR: hand' onclick='InsertRow()' class='midcolora'>" +  AddressProperty[i].Address1n2 + "</td>";
			}
			else
			{
				strAddress = strAddress + "<td style='CURSOR: hand' onclick='InsertRow()' class='midcolora'></td>";
			}
//			if (AddressProperty[i].Address2 != null)
//			{
//				strAddress = strAddress + "<td style='CURSOR: hand' onclick='InsertRow()' class='midcolora'>" +  AddressProperty[i].Address2 + "</td>";
//			}
//			else
//			{
//				strAddress = strAddress + "<td style='CURSOR: hand' onclick='InsertRow()' class='midcolora'></td>";
//			}
			if (AddressProperty[i].District != null)
			{
				strAddress = strAddress + "<td style='CURSOR: hand' onclick='InsertRow()' class='midcolora'>" +  AddressProperty[i].District + "</td>";
			}
			else
			{
				strAddress = strAddress + "<td style='CURSOR: hand' onclick='InsertRow()' class='midcolora'></td>";
			}
			if (AddressProperty[i].City != null)
			{
				strAddress = strAddress + "<td style='CURSOR: hand' onclick='InsertRow()' class='midcolora'>" +  AddressProperty[i].City + "</td>";;
			}
			else
			{
				strAddress = strAddress + "<td style='CURSOR: hand' onclick='InsertRow()' class='midcolora'></td>";
			}
			
			if (AddressProperty[i].State != null)
			{
				strAddress = strAddress + "<td style='CURSOR: hand' onclick='InsertRow()' class='midcolora'>" +  AddressProperty[i].State + "</td>";
			}
			else
			{
				strAddress = strAddress + "<td style='CURSOR: hand' onclick='InsertRow()' class='midcolora'></td>";
			}
			
			if (AddressProperty[i].Zip != null)
			{
				strAddress = strAddress + "<td style='CURSOR: hand' onclick='InsertRow()' class='midcolora'>" +  AddressProperty[i].Zip + "</td>";
			}
			else
			{
				strAddress = strAddress + "<td style='CURSOR: hand' onclick='InsertRow()' class='midcolora'></td>";
			}
			
			strAddress = "<tr>" + strAddress + "</tr>";
		}
		strAddress = strAddress + "</table>";
		
		document.getElementById("address").innerHTML = strAddress;
		
    }
    
    function setStatusMessage()
    {
		if (window.opener.top.topframe.LIST_ADDRESS_MATCH_STATUS)
			document.getElementById("statusMessage").innerHTML = window.opener.top.topframe.LIST_ADDRESS_MATCH_STATUS;	
    }
    </script>
  </head>
  <body MS_POSITIONING="GridLayout" onload = "showAddress();">
	
    <form id="Form1" method="post" runat="server">
		<table width="100%">
		<tr>
			<td class="midcolorc" colspan=2>
				&nbsp;<div id="statusMessage" style="FONT-WEIGHT: bold; COLOR: red"></div>
			</td>
		</tr>
		<tr>
			<td colspan=4>
			<div id="address" width="100%">
			</div>
			</td>
		</tr>
		
		<tr>
			<td class="midcolorr" colspan="2">
				<input type="button" id="btnClose" onclick="window.close();" value="Close" class="clsbutton" runat="server">
			</td>
		</tr>
		</table>
     </form>
  </body>
</html>
<script>
	setStatusMessage();
</script>