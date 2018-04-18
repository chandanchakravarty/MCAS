<%@ Page language="c#" Codebehind="PolCopyUmbSchRecords.aspx.cs" AutoEventWireup="false" Inherits="Policies.Aspx.PolCopyUmbSchRecords" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BRICS - List of Schedule of Underlying Records</title>
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
		if(("<%=gIntSaved%>")==1)
		{			 
			window.close();			
		}	
		function SetTitle()
		{			
			if(document.getElementById('hidCalledFor').value=="<%=Cms.BusinessLayer.BlApplication.ClsUmbSchRecords.CALLED_FROM_BOAT%>" || document.getElementById('hidCalledFor').value=="<%=Cms.BusinessLayer.BlApplication.ClsUmbSchRecords.CALLED_FROM_LOCATIONS%>" || document.getElementById('hidCalledFor').value=="<%=Cms.BusinessLayer.BlApplication.ClsUmbSchRecords.CALLED_FROM_VEHICLES%>" || document.getElementById('hidCalledFor').value=="<%=Cms.BusinessLayer.BlApplication.ClsUmbSchRecords.CALLED_FROM_REC_VEH%>")
				document.title = "EBIX ADVANTAGE - List of Schedule of Underlying Records";
			else
				document.title = "EBIX ADVANTAGE -  Copy Schedule of Underlying Records";
		}			
		</script>
	</HEAD>
	<body onload="SetTitle();ChangeColor();ApplyColor();showScroll();">
		<div class="pageContent" id="bodyHeight">
			<form id="Form1" method="post" runat="server">
				<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0">
					<tr>
						<td>
							<table cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
								<tr>
									<td>&nbsp;</td>
								</tr>
								<TR class="midcolora">
									<TD class="headereffectCenter"><asp:label id="lblHeader" Runat="server">List of Schedule of Underlying Records</asp:label></TD>
								</TR>
								<TR>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
								</TR>
								<tr>
									<td class="midcolora"><asp:datagrid id="dgrDriverRecords" Runat="server" Width="100%" HeaderStyle-CssClass="HeadRow"
											AutoGenerateColumns="false" Visible="False">
											<Columns>
												<asp:TemplateColumn HeaderText="Select">
													<ItemTemplate>
														<asp:CheckBox ID="chkSelect" Runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="DRIVER_NAME" Visible="true" ItemStyle-CssClass="DataRow" HeaderText="Driver Name"></asp:BoundColumn>
												<asp:BoundColumn DataField="DRIVER_CODE" Visible="False" HeaderText="Driver Code" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DRIVER_FNAME" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DRIVER_MNAME" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DRIVER_LNAME" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DRIVER_ADDRESS" Visible="False" HeaderText="Address" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DRIVER_DOB" Visible="true" HeaderText="Date of Birth" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DRIVER_ADD1" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DRIVER_ADD2" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DRIVER_CITY" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DRIVER_STATE" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DRIVER_ZIP" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DRIVER_COUNTRY" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DRIVER_HOME_PHONE" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DRIVER_BUSINESS_PHONE" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DRIVER_EXT" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DRIVER_DRIV_TYPE" Visible="true" HeaderText="Driver Type" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DRIVER_LIC_STATE" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="FORM_F95" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DATE_LICENSED" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DRIVER_DRIV_LIC" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DRIVER_MART_STAT" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DRIVER_MOBILE" ItemStyle-CssClass="DataRow" Visible="False"></asp:BoundColumn>
												<asp:BoundColumn DataField="DRIVER_SSN" ItemStyle-CssClass="DataRow" HeaderText="Social Security Number"
													Visible="False"></asp:BoundColumn>
												<asp:BoundColumn DataField="DRIVER_SEX" ItemStyle-CssClass="DataRow" Visible="False"></asp:BoundColumn>
											</Columns>
										</asp:datagrid>
										<asp:datagrid id="dgrLocationRecords" Runat="server" Width="100%" HeaderStyle-CssClass="HeadRow"
											AutoGenerateColumns="false" Visible="False">
											<Columns>
												<%--<asp:TemplateColumn HeaderText="Select" ItemStyle-CssClass="DataRow">
													<ItemTemplate>
														<asp:CheckBox ID="chkSelect" Runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>--%>
												<asp:BoundColumn DataField="LOCATION_NUMBER" Visible="true" HeaderText="Location #" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="LOCATION_ID" Visible="false" HeaderText="Customer Location #" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<%--<asp:BoundColumn DataField="LOC_COUNTY" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>--%>
												<asp:BoundColumn DataField="PHONE_NUMBER" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="FAX_NUMBER" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<%--<asp:BoundColumn DataField="LOCATION_NUMBER" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>--%>
												<asp:BoundColumn DataField="ADDRESS" Visible="True" HeaderText="Address" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<%--<asp:BoundColumn DataField="ADDRESS_1" Visible="False" HeaderText="Address" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="ADDRESS_2" Visible="False" HeaderText="Address" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="CITY" Visible="False" HeaderText="City" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="STATE_NAME" Visible="False" HeaderText="State" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="STATE" Visible="False" HeaderText="State" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="ZIPCODE" Visible="False" HeaderText="Zip" ItemStyle-CssClass="DataRow"></asp:BoundColumn>--%>
												<asp:BoundColumn DataField="OTHER_POLICY" Visible="True" HeaderText="Policy Number" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
											</Columns>
										</asp:datagrid>
										<asp:datagrid id="dgrRecVehRecords" Runat="server" Width="100%" HeaderStyle-CssClass="HeadRow"
											AutoGenerateColumns="false" Visible="False">
											<Columns>
												<%--<asp:TemplateColumn HeaderText="Select" ItemStyle-CssClass="DataRow">
													<ItemTemplate>
														<asp:CheckBox ID="chkSelect" Runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>--%>
												<asp:BoundColumn DataField="REC_VEH_ID" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="RV #"></asp:BoundColumn>
												<asp:BoundColumn DataField="COMPANY_ID_NUMBER" Visible="False" HeaderText="Company ID " ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="YEAR" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Year"></asp:BoundColumn>
												<asp:BoundColumn DataField="MAKE" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Make"></asp:BoundColumn>
												<asp:BoundColumn DataField="MODEL" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Model"></asp:BoundColumn>
												<asp:BoundColumn DataField="SERIAL" Visible="True" HeaderText="Serial" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="STATE_REGISTERED" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="MANUFACTURER_DESC" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="REMARKS" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="HORSE_POWER" Visible="False" HeaderText="City" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DISPLACEMENT" Visible="False" HeaderText="State" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="PRIOR_LOSSES" Visible="False" HeaderText="State" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="IS_UNIT_REG_IN_OTHER_STATE" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="USED_IN_RACE_SPEED" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="RISK_DECL_BY_OTHER_COMP" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DESC_RISK_DECL_BY_OTHER_COMP" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="VEHICLE_MODIFIED" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="VEHICLE_TYPE" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="STATE_NAME" Visible="False" HeaderText="State" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
											</Columns>
										</asp:datagrid>
										<asp:datagrid id="dgrVehicleRecords" Runat="server" Width="100%" HeaderStyle-CssClass="HeadRow"
											AutoGenerateColumns="false" Visible="False">
											<Columns>
												<%--<asp:TemplateColumn HeaderText="Select" ItemStyle-CssClass="DataRow">
													<ItemTemplate>
														<asp:CheckBox ID="chkSelect" Runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>--%>
												<asp:BoundColumn DataField="INSURED_VEH_NUMBER" Visible="True" HeaderText="Vehicle #" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="MOTORCYCLE_TYPE_DESC" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Type of vehicle"></asp:BoundColumn>
												<%--<asp:BoundColumn DataField="VIN" Visible="False" HeaderText="VIN" ItemStyle-CssClass="DataRow"></asp:BoundColumn>--%>
												<asp:BoundColumn DataField="VEHICLE_YEAR" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Year"></asp:BoundColumn>
												<asp:BoundColumn DataField="MAKE" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Make"></asp:BoundColumn>
												<asp:BoundColumn DataField="MODEL" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Model"></asp:BoundColumn>
												<%--<asp:BoundColumn DataField="BODY_TYPE" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="GRG_ADD1" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="GRG_ADD2" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="GRG_CITY" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="GRG_COUNTRY" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="GRG_STATE" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="GRG_ZIP" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="REGISTERED_STATE" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="TERRITORY" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="CLASS" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="REGN_PLATE_NUMBER" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="ST_AMT_TYPE" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="AMOUNT" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="SYMBOL" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="VEHICLE_AGE" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="IS_OWN_LEASE" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="PURCHASE_DATE" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="IS_NEW_USED" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="MILES_TO_WORK" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="VEHICLE_USE" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="VEH_PERFORMANCE" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="MULTI_CAR" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="ANNUAL_MILEAGE" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="PASSIVE_SEAT_BELT" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="AIR_BAG" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="ANTI_LOCK_BRAKES" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DEACTIVATE_REACTIVATE_DATE" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="VEHICLE_CC" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="MOTORCYCLE_TYPE" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="USE_VEHICLE" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="CLASS_PER" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="CLASS_COM" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="VEHICLE_TYPE_PER" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="VEHICLE_TYPE_COM" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="SAFETY_BELT" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="MILES_TO_WORK" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>--%>
											</Columns>
										</asp:datagrid>
										<asp:datagrid id="dgrBoatRecords" Runat="server" Width="100%" HeaderStyle-CssClass="HeadRow" AutoGenerateColumns="false"
											Visible="False">
											<Columns>
												<%--<asp:TemplateColumn HeaderText="Select" ItemStyle-CssClass="DataRow">
													<ItemTemplate>
														<asp:CheckBox ID="chkSelect" Runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>--%>
												<asp:BoundColumn DataField="BOAT_ID" Visible="True" HeaderText="Boat #" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="WATERCRAFT_TYPE" HeaderText="Type of Watecraft" Visible="True" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="YEAR" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Year"></asp:BoundColumn>
												<asp:BoundColumn DataField="MAKE" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Make"></asp:BoundColumn>
												<asp:BoundColumn DataField="MODEL" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Model"></asp:BoundColumn>
												<%--<asp:BoundColumn DataField="HULL_ID_NO" Visible="False" HeaderText="Serial #" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="STATE_REG" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="HULL_MATERIAL" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="FUEL_TYPE" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DATE_PURCHASED" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="LENGTH" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="MAX_SPEED" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="BERTH_LOC" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="WATERS_NAVIGATED" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="TERRITORY" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="TYPE_OF_WATERCRAFT" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="INSURING_VALUE" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="WATERCRAFT_HORSE_POWER" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="BOAT_ID" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="TWIN_SINGLE" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DESC_OTHER_WATERCRAFT" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="INCHES" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="LORAN_NAV_SYSTEM" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DIESEL_ENGINE" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="SHORE_STATION" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="HALON_FIRE_EXT_SYSTEM" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DUAL_OWNERSHIP" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="REMOVE_SAILBOAT" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="COV_TYPE_BASIS" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="LOCATION_ADDRESS" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="LOCATION_CITY" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="LOCATION_STATE" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="LOCATION_ZIP" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="LAY_UP_PERIOD_FROM_DAY" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="LAY_UP_PERIOD_FROM_MONTH" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="LAY_UP_PERIOD_TO_DAY" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="LAY_UP_PERIOD_TO_MONTH" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>--%>
											</Columns>
										</asp:datagrid>
									</td>
								</tr>
								<TR>
									<TD class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnSubmit" runat="server" Text="Submit"></cmsb:cmsbutton></TD>
								</TR>
								<TR>
									<TD class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnClose" runat="server" Visible="False" Text="Close"></cmsb:cmsbutton></TD>
								</TR>
								<tr>
									<td class="midcolorr"><INPUT id="hidCustomerID" type="hidden" value="0" name="hidCustomerID" runat="server">
										<INPUT id="hidPolicyVersionID" type="hidden" value="0" name="hidPolicyVersionID" runat="server">
										<INPUT id="hidPolicyID" type="hidden" value="0" name="hidPolicyID" runat="server">
										<INPUT id="hidCalledFrom" type="hidden" value="0" name="hidCalledFrom" runat="server">
										<INPUT id="hidCalledFor" type="hidden" value="0" name="hidCalledFor" runat="server">
										<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
										<INPUT id="hidTitle" type="hidden" value="0" name="hidTitle" runat="server">
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</form>
		</div>
	</body>
</HTML>











