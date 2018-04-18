<%@ Control Language="c#" AutoEventWireup="false" Codebehind="CmsTimePicker.ascx.cs" Inherits="Cms.CmsWeb.WebControls.CmsTimePicker" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script language="javascript">
	function selectHour(thisHour)
	{
		if(parseInt(thisHour) != NaN)
		{
			document.getElementById('timePickerHour').selectedIndex = thisHour;
		}
	}
	function selectMinute(thisMinute)
	{
		if(parseInt(thisMinute) != NaN)
		{
			document.getElementById('timePickerMinute').selectedIndex = thisMinute;
		}
	}
	function selectMeridian(thisMeridian)
	{
		if(thisMeridian.toUpperCase() == "AM")
		{
			document.getElementById('timePickerMinute').selectedIndex = 1;
		}
		else if(thisMeridian.toUpperCase() == "PM")
		{
			document.getElementById('timePickerMinute').selectedIndex = 2;
		}
	}
</script>
<asp:DropDownList ID="timePickerHour" Runat="server"></asp:DropDownList>
&nbsp;:&nbsp;
<asp:DropDownList ID="timePickerMinute" Runat="server"></asp:DropDownList>
&nbsp;-&nbsp;
<asp:DropDownList ID="timePickerMeridian" Runat="server"></asp:DropDownList>
