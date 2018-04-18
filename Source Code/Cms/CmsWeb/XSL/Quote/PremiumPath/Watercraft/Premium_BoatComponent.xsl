<?xml version="1.0" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt">
	<xsl:variable name="HOCoveragesDoc" select="document('FactorPath')" />
	<!-- Following boat types present in the database. 
	These are mapped with the values in the table 'mnt_lookup_values' against lookup_id=1201
	These are constant global variables.
				 
			IO	Inboard / Outboard Boat
			I	Inboard
			S	Sailboat
			MJB	Mini Jet Boat
			PIO	Pontoon Inboard / Outboard Boat
			CN	Canoe
			RB	Row Boat		
			W	Waverunner
			PWT	Jetski (w/Lift Bar)
			JS	Jet Ski		
			OWM	Outboard (w/Motor)
			SWOB	Sailboat w/outboard
			PO	Pontoon Outboard
			PDB	Paddleboat
			JDO	Jet Drive Outboards

		Distinct Styles:
			I - Inboard
			IO - Inboard/Outboard
			JS - Jet Ski
			O - Outboard
			S - Sailboat
			T - Trailer

-->
	<xsl:variable name="BOATTYPE_CANOE" select="'CN'" />
	<xsl:variable name="BOATTYPE_INBOARD_OUTBOARD" select="'IO'" />
	<xsl:variable name="BOATTYPE_INBOARD" select="'I'" />
	<xsl:variable name="BOATTYPE_JETDRIVE_OUTBOARD" select="'JDO'" />
	<xsl:variable name="BOATTYPE_JETSKI_WITH_LB" select="'PWT'" />
	<xsl:variable name="BOATTYPE_JETSKI" select="'JS'" />
	<xsl:variable name="BOATTYPE_MINI_JET" select="'MJB'" />
	<xsl:variable name="BOATTYPE_OUTBOARD_WITH_MOTOR" select="'OWM'" />
	<xsl:variable name="BOATTYPE_PADDLEBOAT" select="'PDB'" />
	<xsl:variable name="BOATTYPE_PONTOON_INBOARD_OUTBOARD" select="'PIO'" />
	<xsl:variable name="BOATTYPE_PONTOON_OUTBOARD" select="'PO'" />
	<xsl:variable name="BOATTYPE_ROW_BOAT" select="'RB'" />
	<xsl:variable name="BOATTYPE_SAILBOAT" select="'S'" />
	<xsl:variable name="BOATTYPE_SAILBOAT_WITH_OUTBOARD" select="'SWOB'" />
	<xsl:variable name="BOATTYPE_WAVERUNNER" select="'W'" />
	<xsl:variable name="BOAT_TYPE_OUTBOARD" select="'O'" />
	<xsl:variable name="BOATTYPE_JETSKI_TRAILER" select="'JT'" />
	<xsl:variable name="BOATTYPE_JETSKI_TRAILER_FROM_APP" select="'TRSK'" />
	<xsl:variable name="BOATTYPE_WAVERUNNER_TRAILER" select="'WRT'" />
	<xsl:variable name="BOATTYPE_TRAILER" select="'TRAI'" />
	<xsl:variable name="BOATSTYLE_OUTBOARD" select="'O'" />
	<xsl:variable name="BOATSTYLE_INBOARD_OUTBOARD" select="'IO'" />
	<xsl:variable name="BOATSTYLE_INBOARD" select="'I'" />
	<xsl:variable name="BOATSTYLE_JETSKI" select="'JS'" />
	<xsl:variable name="BOATSTYLE_SAILBOAT" select="'S'" />
	<xsl:variable name="BOATSTYLE_TRAILER" select="'T'" />
	<xsl:variable name="BOATS_BOATTYPE" select="'B'" />
	<xsl:variable name="BOATS_TRAILERTYPE" select="'T'" />
	<xsl:variable name="WATERTYPE_GREATLAKES" select="'G'" />
	<xsl:variable name="CALLED_FROM_LIABILITY" select="'LIABILITY'" />
	<xsl:variable name="CALLED_FROM_PD" select="'PD'" />
	<xsl:variable name="CONSTRUCTIONCODE_WOOD" select="'W'" /> <!-- Same as that in default xml for watercraft and masterlookup codes -->
	<xsl:variable name="CONSTRUCTIONCODE_FIBREGLASS" select="'F'" /> <!-- Same as that in default xml  for watercraft and masterlookup codes -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE" select="'03/01/2006'" /> <!--Quote effective date check -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_DAY" select="01" /> <!--date day -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_MONTH" select="03" /> <!--date month -->
	<xsl:template match="/">
		<xsl:apply-templates select="INPUTXML/QUICKQUOTE" />
	</xsl:template>
	<xsl:template match="INPUTXML/QUICKQUOTE">
		<PREMIUM>
			<CREATIONDATE></CREATIONDATE>
			<GETPATH>
				<PRODUCT PRODUCTID="0">
					<!--Group for Boat-->
					<GROUP GROUPID="0">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<SUBGROUP>
							<STEP STEPID="0">
								<PATH>ND</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Boat Name and Year -->
						<SUBGROUP>
							<STEP STEPID="1">
								<PATH>ND</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Physical Damage -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="2">
									<PATH>
									{
										  <xsl:call-template name="PHYSICAL_DAMAGE" />  
										 
									}
								</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Watercraft Liability -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="3">
									<PATH>
										<xsl:call-template name="WATERCRAFT_LIABILITY_DISPLAY_EXTENDED" />
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="4">
									{
									<PATH>
										<xsl:choose>
											<xsl:when test="BOATS/BOAT/PERSONALLIABILITY != 'EFH'">
												<xsl:call-template name="WATERCRAFT_LIABILITY" />
											</xsl:when>
											<xsl:otherwise>0</xsl:otherwise>
										</xsl:choose>
									</PATH>
									}
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Medical Payments to others -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="5">
							 {
								<PATH>
										<xsl:call-template name="MEDICAL_PAYMENTS_DISPLAY" />
									</PATH>
							}
							</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="6">
									{
									<PATH>
										<!--If Medical Payment is "Extended From Home" then Premium should not be calculated - Asfa Praveen - 22-May-2007 -->
										<xsl:if test="BOATS/BOAT/MEDICALPAYMENT != 'EFH'">
											<xsl:call-template name="MEDICAL_PAYMENTS" />
										</xsl:if>
									</PATH>
									}
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Uninsured Boaters -Included-->
						<SUBGROUP>
							<STEP STEPID="7">
								<PATH>
									<xsl:call-template name="UNINSURED_BOATERS_DISPLAY" />
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Uninsured Boaters-Value -->
						<SUBGROUP>
						<IF>
							<STEP STEPID="8">
							{
								<PATH>
									<xsl:call-template name="UNINSURED_BOATERS_COVERAGE" />
								</PATH>
								}
							</STEP>
							</IF>
						</SUBGROUP>
						<!-- Boat Towing and Emergency Services -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="9">
						 {
						<PATH>
										<xsl:call-template name="BOAT_TOWING_PREMIUM"></xsl:call-template>
									</PATH>
						 }
						</STEP>
							</IF>
						</SUBGROUP>
						<!-- Boat Replacement Cost Coverage -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="10">
						       {
								<PATH>
										<xsl:call-template name="BOAT_REPLACEMENT_DISPLAY" />
									</PATH>
							   }
							</STEP>
							</IF>
						</SUBGROUP>
						<!-- Client Entertainment, Form OP-720  -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="11">
									<PATH>
								{
									<xsl:call-template name="CLIENT_ENTERTAINMENT" />
								}
								</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Watercraft Liability Pollution Coverage, Form OP-900  -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="12">
									<PATH>
									{
										<xsl:call-template name="LIABILITY_POLLUTION_COVERAGE" />
									}	
								</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Agreed Value Endorsement, Form AV-100  -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="13">
									<PATH>
									{
										<xsl:call-template name="AGREED_VALUE_ENDORSEMENT" />
									}
								 
								</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Discount - Insurance Score Credit (Score 700)  -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="14">
						{
								<PATH>
										<xsl:call-template name="INSURANCESCORE_DISPLAY" />
									</PATH>
						}
						</STEP>
							</IF>
						</SUBGROUP>
						<!-- Discount Wolverine Homeowners  -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="15">
						{
								<PATH>
										<xsl:call-template name="DISCOUNT_BOAT_HOME"></xsl:call-template>
									</PATH>
						}
							</STEP>
							</IF>
						</SUBGROUP>
						<!-- Discount - 5 Years Experience or Navigation Course   -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="16">
						{
								<PATH>
										<xsl:call-template name="EXPERIENCE_CREDIT_PREMIUM"></xsl:call-template>
									</PATH>
						}		
							</STEP>
							</IF>
						</SUBGROUP>
						<!-- Discount - Diesel Engine   -->
						<SUBGROUP>
							<STEP STEPID="17">
								<PATH>
									<xsl:call-template name="DIESEL_ENGINE_PREMIUM"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Discount - Halon Fire Extinguishing System   -->
						<SUBGROUP>
							<STEP STEPID="18">
								<PATH>
									<xsl:call-template name="HALON_FIRE_PREMIUM"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Discount - Loran, GPS or other navigation System  -->
						<SUBGROUP>
							<STEP STEPID="19">
								<PATH>
									<xsl:call-template name="LORAN_GPS_PREMIUM"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Discount - Shore Station -->
						<SUBGROUP>
							<STEP STEPID="20">
								<PATH>
									<xsl:call-template name="SHORE_STATION_PREMIUM"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Discount - Multi-Boat -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="21">
						{
								<PATH>
										<xsl:call-template name="MULTI_BOAT_PREMIUM"></xsl:call-template>
									</PATH>
						}
						</STEP>
							</IF>
						</SUBGROUP>
						<!-- Discount - Age of Boat -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="22">
						{
								<PATH>
										<xsl:call-template name="AGE_DISCOUNT_DISPLAY"></xsl:call-template>
									</PATH>
						}
						</STEP>
							</IF>
						</SUBGROUP>
						<!-- Discount - Wooden Boat -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="23">
						{
								<PATH>
										<xsl:call-template name="WOODEN_BOAT_PREMIUM"></xsl:call-template>
									</PATH>
						}
							</STEP>
							</IF>
						</SUBGROUP>
						<!-- Charge - Dual Ownership -->
						<SUBGROUP>
							<STEP STEPID="24">
								<PATH>
									<xsl:call-template name="DUAL_OWNERSHIP_PREMIUM"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Charge - Waverunner 10 %-->
						<SUBGROUP>
							<STEP STEPID="25">
								<PATH>
									<xsl:call-template name="WAVERUNNER_PREMIUM_DISPLAY"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Charge - Length over 26 Feet-->
						<SUBGROUP>
							<STEP STEPID="26">
								<PATH>
									<xsl:call-template name="LENGTH_26FEET_PREMIUM" />
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Charge - Length over 26 Feet and speed 55-65 miles per hour-->
						<SUBGROUP>
							<STEP STEPID="27">
								<PATH>
									<!--<xsl:choose>
									<xsl:when test="BOATS/BOAT/BOATTYPECODE=$BOATTYPE_INBOARD_OUTBOARD or BOATS/BOAT/BOATTYPECODE=$BOATTYPE_INBOARD">-->
									<xsl:call-template name="BOAT_LENGTH_SPEED_SURCHARGE_PREMIUM" />
									<!--</xsl:when>
									<xsl:otherwise></xsl:otherwise>
								</xsl:choose>-->
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Charge - FibreGlass Boats over 15 year old-->
						<SUBGROUP>
							<IF>
								<STEP STEPID="28">
						{
								<PATH>
										<xsl:call-template name="FIBRE_GLASS_PREMIUM" />
									</PATH>
						}		
						</STEP>
							</IF>
						</SUBGROUP>
						<!-- Charge - Remove Sailboat Racing Exclusion-->
						<SUBGROUP>
							<STEP STEPID="29">
								<PATH>
									<xsl:call-template name="REMOVE_SAILBOAT_PREMIUM" />
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Charge - MINIJET BOAT 10 % -->
						<SUBGROUP>
							<STEP STEPID="30">
								<PATH>
									<xsl:call-template name="MINIJET_PREMIUM" />
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Charge County of Operation -->
						<SUBGROUP>
							<STEP STEPID="31">
								<PATH>
									<xsl:call-template name="COUNTY_OF_OPERATION_DISPLAY" />
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Unattached Equipment 
						<SUBGROUP>
							<IF>
								<STEP STEPID="32">
									<PATH>
									{
									<xsl:call-template name="UNATTACHED_PREMIUM_VALUE" />
									}
								</PATH>
								</STEP>
							</IF>
						</SUBGROUP>-->
						<!-- Unattached Equipment 
						<SUBGROUP>
							<STEP STEPID="33">
								<PATH>
									<xsl:call-template name="UNATTACHED_PREMIUM" />
								</PATH>
							</STEP>
						</SUBGROUP>-->
						<!-- Minimum  Premium -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="32">
									<PATH>
							  {
								<xsl:call-template name="MINIMUM_PREMIUM" />
							 }
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Total  Premium -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="33">
									<PATH>
							{
							<xsl:call-template name="FINAL_PREMIUM" />
							}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
					</GROUP>
				</PRODUCT>
			</GETPATH>
			<CALCULATION>
				<PRODUCT PRODUCTID="0">
					<!--Product Name -->
					<xsl:attribute name="DESC">
						<xsl:call-template name="PRODUCTNAME" />
					</xsl:attribute>
					<!--Group for Boat-->
					<GROUP GROUPID="0" CALC_ID="10000">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID0" />
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<GROUPRULE></GROUPRULE>
						<GROUPFORMULA></GROUPFORMULA>
						<GDESC>Boat</GDESC>
						<!-- Boat name and year  -->
						<STEP STEPID="0" CALC_ID="1000" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID0" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE></COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Boat Description  -->
						<STEP STEPID="1" CALC_ID="1001" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID1" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_LEN</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Physical Damage-->
						<STEP STEPID="2" CALC_ID="1002" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID2" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:call-template name="DEDUCTIBLE_DISPLAY" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="BOATS/BOAT/MARKETVALUE" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>
								<xsl:call-template name="COMPONENTCODE_PHISCALDAMAGE" />
							</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="PHYSICAL_DAMAGE" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<!--xsl:template name="COMP_PHY_DAMMAGE"-->
								<xsl:variable name="VAR_PHY_DAMAGE">
									<xsl:call-template name="COMPONENTCODE_PHISCALDAMAGE" />
								</xsl:variable>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="$VAR_PHY_DAMAGE"></xsl:with-param>
								</xsl:call-template>
								<!--/xsl:template-->
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Watercraft Liability-->
						<STEP STEPID="3" CALC_ID="1003" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID3" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:call-template name="PERSONALLIABILITY_LIMIT_DISPLAY" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_LIABILITY_INCLUDE</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'BOAT_LIABILITY_INCLUDE'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Watercraft Liability-->
						<STEP STEPID="4" CALC_ID="1004" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID4" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:call-template name="PERSONALLIABILITY_LIMIT_DISPLAY" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_LIABILITY_PREMIUM</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:choose>
									<xsl:when test="BOATS/BOAT/PERSONALLIABILITY != 'EFH'">
										<xsl:call-template name="WATERCRAFT_LIABILITY" />
									</xsl:when>
									<xsl:otherwise>0</xsl:otherwise>
								</xsl:choose>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'BOAT_LIABILITY_PREMIUM'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Medical Payments to others-->
						<STEP STEPID="5" CALC_ID="1005" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID5" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:call-template name="MEDICALPAYMENT_LIMIT_DISPLAY" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_MP_INCLUDE</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'BOAT_MP_INCLUDE'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Medical Payments to others-->
						<STEP STEPID="6" CALC_ID="1006" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<!--Medical Payments holds 'EFH' if 'Extended From Home', so Medical Payments Description is not shown in that case  - Asfa Praveen 22/May/2007 -->
								<xsl:if test="BOATS/BOAT/MEDICALPAYMENT != 'EFH'">
									<xsl:call-template name="STEPID6" />
								</xsl:if>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<!--Medical Payments holds 'EFH' if 'Extended From Home', so Medical Payments Limit value is not shown in that case  - Asfa Praveen 22/May/2007 -->
								<xsl:call-template name="MEDICALPAYMENT_LIMIT_DISPLAY" />
								<!--xsl:value-of select="BOATS/BOAT/MEDICALPAYMENT" /-->
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_MP_PREMIUM</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:if test="BOATS/BOAT/MEDICALPAYMENT != 'EFH'">
									<xsl:call-template name="MEDICAL_PAYMENTS" />
								</xsl:if>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'BOAT_MP_PREMIUM'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Uninsured Boaters -Included-->
						<STEP STEPID="7" CALC_ID="1007" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID7" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:value-of select="BOATS/BOAT/UNINSUREDBOATERS" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_UB_INCLUDE</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'BOAT_UB_INCLUDE'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Uninsured Boaters -Value -->
						<STEP STEPID="8" CALC_ID="1008" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID8" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:value-of select="BOATS/BOAT/UNINSUREDBOATERS" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_UB_PREMIUM</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="UNINSURED_BOATERS_COVERAGE" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'BOAT_UB_PREMIUM'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Boat Towing and Emergency services-->
						<STEP STEPID="9" CALC_ID="1009" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID9" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:call-template name="BOAT_TOWING_LIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_TOW</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'BOAT_TOW'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Boat Replacement Cost Coverage-->
						<STEP STEPID="10" CALC_ID="1010" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID10" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_REPLACE</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'BOAT_REPLACE'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Client Entertainment, Form OP-720  -->
						<STEP STEPID="11" CALC_ID="1011" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID11" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_OP720</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="CLIENT_ENTERTAINMENT" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'BOAT_OP720'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Watercraft Liability Pollution Coverage, Form OP-900 -->
						<STEP STEPID="12" CALC_ID="1012" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID12" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:value-of select="BOATS/BOAT/OP900_LIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_OP900</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="LIABILITY_POLLUTION_COVERAGE" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'BOAT_OP900'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Agreed Value Endorsement, Form AV-100 -->
						<STEP STEPID="13" CALC_ID="1013" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID13" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_AV100</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="AGREED_VALUE_ENDORSEMENT" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'BOAT_AV100'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Discount - Insurance Score Credit (Score 700)  -->
						<STEP STEPID="14" CALC_ID="1014" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID14" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_INS_SCORE</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Discount Wolverine Homeowners  -->
						<STEP STEPID="15" CALC_ID="1015" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID15" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_HOME_DIS</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Discount - 5 Years Experience or Navigation Course   -->
						<STEP STEPID="16" CALC_ID="1016" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID16" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_EXPERIENCE</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Discount - Diesel Engine   -->
						<STEP STEPID="17" CALC_ID="1017" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID17" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_DIESEL</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Discount - Halon Extinguishing Fire System  -->
						<STEP STEPID="18" CALC_ID="1018" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID18" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_HALON</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Discount - Loran, GPS or other navigation System  -->
						<STEP STEPID="19" CALC_ID="1019" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID19" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_LORAN</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Discount - Shore Station -->
						<STEP STEPID="20" CALC_ID="1020" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID20" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_SHORE</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Discount - Multi-Boat -->
						<STEP STEPID="21" CALC_ID="1021" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID21" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_MULTI_BOAT</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Discount - Age of Boat-->
						<STEP STEPID="22" CALC_ID="1022" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID22" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_AGE</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Charge - Wooden Boat -->
						<STEP STEPID="23" CALC_ID="1023" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID23" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>S</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_WOODEN</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Charge - Dual Ownership -->
						<STEP STEPID="24" CALC_ID="1024" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID24" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>S</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_DUAL_OWNER</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Charge - Waverunner 10% -->
						<STEP STEPID="25" CALC_ID="1025" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID25" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>S</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_WAVERUNNER</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Charge - Length over 26 Feet -->
						<STEP STEPID="26" CALC_ID="1026" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID26" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>S</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_LENGTH_CHARGE</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Charge - Length over 26 Feet and Speed over 65  -->
						<STEP STEPID="27" CALC_ID="1027" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID27" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>S</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_LENGTH_SPEED_CHARGE</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Charge - Fibre Glass over 15 years old   -->
						<STEP STEPID="28" CALC_ID="1028" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID28" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>S</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_FIBREGLASS</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Charge - Remove Sailboat Racing Exclusion   -->
						<STEP STEPID="29" CALC_ID="1029" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID29" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>S</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_REMOVE_SAILBOAT</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Charge - MIni jet boat 10% -->
						<STEP STEPID="30" CALC_ID="1030" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID30" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>S</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_MINIJET</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- County Of Operation  -->
						<STEP STEPID="31" CALC_ID="1031" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID31" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>S</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_COUNTY_CHARGE</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--Unattached Equipment -->
						<!--<STEP STEPID="32" CALC_ID="1032" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID32" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="BOATS/BOAT/UNATTACHEDEQUIPMENT_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="BOATS/BOAT/UNATTACHEDEQUIPMENT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_UNATTACH_INCLUDE</COMPONENT_CODE>
							<COMPONENT_REMARKS></COMPONENT_REMARKS>
						</STEP>-->
						<!--Unattached Equipment -->
						<!--<STEP STEPID="33" CALC_ID="1033" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID33" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="BOATS/BOAT/UNATTACHEDEQUIPMENT_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="BOATS/BOAT/UNATTACHEDEQUIPMENT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_UNATTACH_PREMIUM</COMPONENT_CODE>
							<COMPONENT_REMARKS></COMPONENT_REMARKS>
						</STEP>-->
						<!-- Minimum Premium  -->
						<STEP STEPID="32" CALC_ID="1032" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID32" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_ADD_PREMIUM</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="MINIMUM_PREMIUM" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Total Premium  -->
						<STEP STEPID="33" CALC_ID="1033" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID33" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>SUMTOTAL</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="FINAL_PREMIUM" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
					</GROUP>
				</PRODUCT>
			</CALCULATION>
		</PREMIUM>
	</xsl:template>
	<!-- &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&& -->
	<!-- &								LABELS FOR DISPLAY										  & -->
	<!-- &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&& -->
	<!--  Group Details  -->
	<xsl:template name="PRODUCTNAME">Watercraft</xsl:template>
	<xsl:template name="GROUPID0">
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/BOATSTYLECODE != $BOATSTYLE_TRAILER">Boat
			<xsl:value-of select="BOATS/BOAT/@ID"></xsl:value-of>
			</xsl:when>
			<xsl:otherwise>Trailer
			<xsl:value-of select="BOATS/BOAT/@ID"></xsl:value-of>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="STEPID0"><xsl:value-of select="BOATS/BOAT/YEAR" />&#xa0;(<xsl:value-of select="BOATS/BOAT/BOATTYPE" /><xsl:text>), Make:</xsl:text><xsl:value-of select="BOATS/BOAT/MANUFACTURER" />,&#xa0;Model:<xsl:value-of select="BOATS/BOAT/MODEL" /><xsl:text>, Serial: </xsl:text><xsl:value-of select="BOATS/BOAT/SERIALNUMBER" /> <xsl:choose>
			<xsl:when test="normalize-space(BOATS/BOAT/CONSTRUCTION)!=''"><xsl:text>,Construction: </xsl:text><xsl:value-of select="BOATS/BOAT/CONSTRUCTION" /></xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>		
		&#xa0;Territory: <xsl:value-of select="BOATS/BOAT/COUNTYOFOPERATION" />
	</xsl:template>
	<xsl:template name="STEPID1">
		<xsl:variable name="X_LENGTH" select="BOATS/BOAT/LENGTH" />
		<xsl:variable name="VAR_1">
			<xsl:value-of select="$X_LENGTH div 12" />
		</xsl:variable>
		<xsl:variable name="VAR_2">
			<xsl:value-of select="$X_LENGTH mod 12" />
		</xsl:variable>
		<xsl:variable name="VAR_3">
			<xsl:choose>
				<xsl:when test="contains($VAR_1,'.')">
					<xsl:value-of select="substring-before($VAR_1, '.')" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$VAR_1" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/BOATSTYLECODE != $BOATSTYLE_TRAILER">
				<xsl:choose>
					<xsl:when test="$VAR_2 !='0' and $VAR_2 !=''">
				 - <xsl:value-of select="$VAR_3" /><xsl:text> Feet </xsl:text><xsl:value-of select="$VAR_2" /><xsl:text> Inches, </xsl:text><xsl:value-of select="BOATS/BOAT/HORSEPOWER" /><xsl:text> HP, </xsl:text><xsl:value-of select="BOATS/BOAT/CAPABLESPEED" /><xsl:text> MPH, </xsl:text><xsl:value-of select="BOATS/BOAT/WATERS" />
				 </xsl:when>
					<xsl:otherwise>
				 - <xsl:value-of select="$VAR_3" /><xsl:text> Feet, </xsl:text><xsl:value-of select="BOATS/BOAT/HORSEPOWER" /><xsl:text> HP, </xsl:text><xsl:value-of select="BOATS/BOAT/CAPABLESPEED" /><xsl:text> MPH, </xsl:text><xsl:value-of select="BOATS/BOAT/WATERS" />
				</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="STEPID2"><xsl:text>Physical Damage </xsl:text></xsl:template>
	<xsl:template name="STEPID3">
		<xsl:call-template name="WATERCRAFT_LIABILITY_TEXT" />
	</xsl:template>
	<xsl:template name="STEPID4">
		<xsl:call-template name="WATERCRAFT_LIABILITY_TEXT" />
	</xsl:template>
	<xsl:template name="STEPID5">
		<xsl:call-template name="MEDICAL_PAYMENTS_TEXT" />
	</xsl:template>
	<xsl:template name="STEPID6">
		<xsl:call-template name="MEDICAL_PAYMENTS_TEXT" />
	</xsl:template>
	<xsl:template name="STEPID7"><xsl:text>Uninsured Boaters</xsl:text></xsl:template>
	<xsl:template name="STEPID8"><xsl:text>Uninsured Boaters</xsl:text></xsl:template>
	<xsl:template name="STEPID9"><xsl:text>Boat Towing and Emergency services</xsl:text></xsl:template>
	<xsl:template name="STEPID10"><xsl:text>Boat Replacement Cost Coverage</xsl:text></xsl:template>
	<xsl:template name="STEPID11"><xsl:text>Client Entertainment, Form OP-720</xsl:text></xsl:template>
	<xsl:template name="STEPID12"><xsl:text>Watercraft Liability Pollution Coverage, Form OP-900</xsl:text></xsl:template>
	<xsl:template name="STEPID13"><xsl:text>Agreed Value Endorsement, Form AV-100</xsl:text></xsl:template>
	<xsl:template name="STEPID14"><xsl:text>	Discount - Insurance Score Credit (Score </xsl:text><xsl:call-template name="INSURENCESCOREDISPLAY" /><xsl:text>)</xsl:text><xsl:value-of select="' '" /><xsl:text> -</xsl:text><xsl:call-template name="INSURANCESCORE_PERCENT" /></xsl:template>
	<xsl:template name="STEPID15">
		<xsl:choose>
			<xsl:when test="POLICY/BOATHOMEDISC = 'Y'"><xsl:text>		Discount - Wolverine Homeowners -</xsl:text><xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID='MULTIPOLICY']/ATTRIBUTES/@CREDIT" /><xsl:text>%</xsl:text></xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="STEPID16">
		<xsl:call-template name="EXPERIENCE_CREDIT_DISPLAY" />
	</xsl:template>
	<xsl:template name="STEPID17">
		<xsl:call-template name="DIESEL_ENGINE_DISPLAY" />
	</xsl:template>
	<xsl:template name="STEPID18">
		<xsl:call-template name="HALON_FIRE_DISPLAY" />
	</xsl:template>
	<xsl:template name="STEPID19">
		<xsl:call-template name="LORAN_GPS_DISPLAY" />
	</xsl:template>
	<xsl:template name="STEPID20">
		<xsl:call-template name="SHORE_STATION_DISPLAY" />
	</xsl:template>
	<xsl:template name="STEPID21">
		<xsl:call-template name="MULTI_BOAT_DISPLAY" />
	</xsl:template>
	<xsl:template name="STEPID22"><xsl:text> Discount - Age of Boat -</xsl:text><xsl:call-template name="AGE_CREDIT" /><xsl:text>% </xsl:text></xsl:template>
	<xsl:template name="STEPID23">
		<xsl:call-template name="WOODEN_BOAT_DISPLAY" />
	</xsl:template>
	<xsl:template name="STEPID24">
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/DUALOWNERSHIP = 'Y'"><xsl:text>Charge - Dual Ownership for Unrelated parties +</xsl:text><xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDDUALOWNERSHIP']/ATTRIBUTES/@SURCHARGE" /><xsl:text>% </xsl:text><!--liability and <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDDUALOWNERSHIP']/ATTRIBUTES/@SURCHARGE" />% Physical Damage--></xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ONLY FOR WAVERUNNER -->
	<xsl:template name="STEPID25">
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/BOATTYPECODE= $BOATTYPE_WAVERUNNER"><xsl:text>Charge - Waverunner +</xsl:text><xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='WAVERUNNER_CHARGE']/NODE[@ID='WAVERUNNER_CHARGES']/ATTRIBUTES/@CHARGE" /><xsl:text>%</xsl:text></xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="STEPID26">
		<xsl:call-template name="LENGTH_26FEET_DISPLAY" />
	</xsl:template>
	<xsl:template name="STEPID27">
		<xsl:call-template name="BOAT_LENGTH_SPEED_SURCHARGE_DISPLAY">
			<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_PD" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template name="STEPID28">
		<xsl:call-template name="FIBRE_GLASS_DISPLAY" />
	</xsl:template>
	<xsl:template name="STEPID29">
		<xsl:call-template name="REMOVE_SAILBOAT_DISPLAY">
			<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_PD" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template name="STEPID30">
		<xsl:call-template name="MINIJET_DISPLAY" />
	</xsl:template>
	<xsl:template name="STEPID31"><xsl:text>Charge - County of Operation +</xsl:text><xsl:call-template name="COUNTY_OPERATION_CHARGE" /><xsl:text>% </xsl:text></xsl:template>
	<!--<xsl:template name="STEPID32">Unattached Equipment and Personal Effects(unscheduled)- Actual Cash Basis</xsl:template>
	<xsl:template name="STEPID33">Unattached Equipment and Personal Effects(unscheduled)- Actual Cash Basis</xsl:template>-->
	<xsl:template name="STEPID32"><xsl:text>Additional Premium charged to meet the minimum premium ($</xsl:text><xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINIMUM_BOAT_PREMIUM']/NODE[@ID ='MINIMUM_PREMIUM']/ATTRIBUTES/@MINIMUM_VALUE" /><xsl:text>)</xsl:text></xsl:template>
	<xsl:template name="STEPID33"><xsl:text>Total </xsl:text><xsl:value-of select="BOATS/BOAT/BOATTYPE" /><xsl:text> Premium </xsl:text></xsl:template>
	<!-- &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&& -->
	<!-- &							END OF LABELS FOR DISPLAY									  & -->
	<!-- &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&& -->
	<!-- ============================================================================================ -->
	<!--				Templates for final premium, Display (END)								  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN			              -->
	<!-- ============================================================================================ -->
	<xsl:template name="FINAL_PREMIUM">
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_TRAILER">
				<!-- Physical Damage -->
				<xsl:call-template name="PHYSICAL_DAMAGE" />
				<!-- 	+
			Unattached Equipment
			<xsl:call-template name="UNATTACHED_PREMIUM_VALUE" />-->
			</xsl:when>
			<xsl:otherwise>
				<!-- Physical Damage -->
				<xsl:variable name="VAR_PHYSICAL_DAMAGE">
					<xsl:call-template name="PHYSICAL_DAMAGE" />
				</xsl:variable>
				<!-- Liability holds "EFH" if "Extended From Home" and then its Premium is not added in Final Premium - Asfa Praveen - 22/May/2007 -->
				<xsl:variable name="VAR_WATERCRAFT_LIABILITY">
					<xsl:choose>
						<xsl:when test="BOATS/BOAT/PERSONALLIABILITY != 'EFH'">
							<xsl:call-template name="WATERCRAFT_LIABILITY" />
						</xsl:when>
						<xsl:otherwise>0</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<!-- Medical Payments holds "EFH" if "Extended From Home" and then its Premium is not added in Final Premium - Asfa Praveen - 22/May/2007 -->
				<xsl:variable name="VAR_MEDICAL_PAYMENTS">
					<xsl:choose>
						<xsl:when test="BOATS/BOAT/MEDICALPAYMENT != 'EFH'">
							<xsl:call-template name="MEDICAL_PAYMENTS" />
						</xsl:when>
						<xsl:otherwise>0</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<!-- Uninsured Boaters-->
				<xsl:variable name="VAR_UNINSURED_BOATERS_COVERAGE">
					<xsl:call-template name="UNINSURED_BOATERS_COVERAGE" />
				</xsl:variable>
				<!-- Add Endorsements -->
				<xsl:variable name="VAR_OPTIONAL_ENDORSEMENTS">
					<xsl:call-template name="OPTIONAL_ENDORSEMENTS" />
				</xsl:variable>
				<!--+
			 Unattached Equipment
			<xsl:call-template name="UNATTACHED_PREMIUM_VALUE" />-->
				<!-- Minimum Premium component-->
				<xsl:variable name="VAR_MINIMUM_PREMIUM">
					<xsl:call-template name="MINIMUM_PREMIUM" />
				</xsl:variable>
				<xsl:value-of select="$VAR_PHYSICAL_DAMAGE + $VAR_WATERCRAFT_LIABILITY + $VAR_MEDICAL_PAYMENTS + $VAR_UNINSURED_BOATERS_COVERAGE + $VAR_OPTIONAL_ENDORSEMENTS + $VAR_MINIMUM_PREMIUM" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for final premium, Display (END)									  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN							  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for optional endorsements Display (START)							  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="OPTIONAL_ENDORSEMENTS">
		<xsl:variable name="VAR1">
			<xsl:call-template name="CLIENT_ENTERTAINMENT" />
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="LIABILITY_POLLUTION_COVERAGE" />
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:call-template name="AGREED_VALUE_ENDORSEMENT" />
		</xsl:variable>
		<xsl:value-of select="$VAR1+$VAR2+$VAR3" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for optional endorsements, Display (END)							  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN							  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for Physical Damage, Display (END)								  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN				              -->
	<!-- ============================================================================================ -->
	<xsl:template name="PHYSICAL_DAMAGE">
		<xsl:variable name="VAR_ID" select="BOATS/BOAT/@ID" />
		<xsl:choose>
			<!--if Coverage Type Basis is "Not Available" then Physical Damage should not be displayed  - Asfa Praveen 27-Apr-2007 -->
			<xsl:when test="(BOATS/BOAT/COVERAGEBASIS ='ANA' and BOATS/BOAT[@ID=$VAR_ID]/@BOATTYPE !=$BOATS_TRAILERTYPE)">0</xsl:when>
			<xsl:when test="(BOATS/BOAT/COVERAGEBASIS ='ANA' and BOATS/BOAT[@ID=$VAR_ID]/@BOATTYPE =$BOATS_TRAILERTYPE)">
				<xsl:call-template name="PHYSICAL_DAMAGE_COMPONENT" />
			</xsl:when>
			<xsl:when test="(BOATS/BOAT/COVERAGEBASIS !='ANA') and (POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN')">
				<xsl:call-template name="PHYSICAL_DAMAGE_COMPONENT" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Watercraft Physical Damage - INDIANA-->
	<xsl:template name="PHYSICAL_DAMAGE_COMPONENT">
		<!-- Physical Damage calculations are dependant on the style of the boat.
			 However, as per the manuals, templates for Minijet, Waverunner, 
			 Jet Ski(w Lift-bar) are different from the other styles 'Inboard,Inboard/Outboard, Sailboat'-->
		<!-- Fetch type -->
		<xsl:variable name="P_BOATTYPECODE">
			<xsl:value-of select="BOATS/BOAT/BOATTYPECODE" />
		</xsl:variable>
		<!-- Fetch style -->
		<xsl:variable name="P_BOAT_STYLE">
			<xsl:value-of select="BOATS/BOAT/BOATSTYLECODE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$P_BOATTYPECODE = $BOATTYPE_WAVERUNNER"> <!-- Waverunner -no lift bar-->
				<xsl:call-template name='WAVE_RATE_PHYSICAL_DAMAGE' />
			</xsl:when>
			<xsl:when test="$P_BOATTYPECODE = $BOATTYPE_MINI_JET"> <!-- Mini Jet Boat -->
				<xsl:call-template name='MINIJET_RATE_CALC' />
			</xsl:when>
			<xsl:when test="$P_BOATTYPECODE = $BOATTYPE_JETSKI_WITH_LB"> <!-- Jet Ski with Liftbar -->
				<xsl:call-template name='JETSKI_W_LIFTBAR_RATE_CALC' />
			</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="$P_BOAT_STYLE = $BOATSTYLE_OUTBOARD"> <!-- Outboard -->
						<xsl:call-template name='OUTBOARD_RATE_CALC' />
					</xsl:when>
					<!-- Inboard, Inboard/Outboard, Sailboat have the same calculations -->
					<xsl:when test="$P_BOAT_STYLE = $BOATSTYLE_INBOARD or $P_BOAT_STYLE = $BOATSTYLE_INBOARD_OUTBOARD or $P_BOAT_STYLE = $BOATSTYLE_SAILBOAT">
						<xsl:call-template name='INBOARD_IO_SAILBOAT_RATE_CALC' />
					</xsl:when>
					<xsl:when test="$P_BOAT_STYLE = $BOATSTYLE_JETSKI ">
						<xsl:call-template name='JETSKI_W_LIFTBAR_RATE_CALC' /> <!-- Same as Jet Ski with Liftbar -->
					</xsl:when>
					<!-- Waverunner Trailer and JetSki Trailer and Trialer have same calculations -->
					<xsl:when test="$P_BOAT_STYLE = $BOATSTYLE_TRAILER">
						<xsl:call-template name='TRAILOR_RATE_CALC' />
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- END OF MAIN FACTOR TEMPLATES -->
	<!-- BEGINNING OF FACTOR TEMPLATES -->
	<!-- ============================================================================================ -->
	<!--				Templates for WaveRunner, Display (START)									  -->
	<!--		    					  FOR INDIANA									              -->
	<!-- ============================================================================================ -->
	<xsl:template name="WAVE_RATE_PHYSICAL_DAMAGE">
		<!-- 
		Use Inboard/Outboard Great Lakes Base Rate multiplied by a factor of 1.10. Use Deductible options from the
			Credits Section. Follow the formula listed below to determine total premium.
			1) Find Inboard/Outboard Great Lakes Rate.
			2) Multiply by a factor of WaveRunner charge (1.10)
			3) Multiply by Coverage Relativity (Use Inboard/Outboard Table)
			4) Multiply Territory Factor. (NOT REQUIRED as we are picking factors territory wise. We do not take base as territory 1)
			5) Multiply Insurance Score Credit.
			6) Apply Deductible Relativity.
			7) Apply Dual Ownership Surcharge (If Eligible)
			8) Term Factor: 1.00 if annual or 0.50 if semi-annual. 
			====================================================			
			9) Add: Agreed Value Endorsement.					|
			10) Add: Liability from Table Below.				| These will be added in the final premium formula if applicable
			11) Add: OP-900 Watercraft Pollution Coverage.		|
			====================================================			
			
	-->
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="BASE_RATES_WAVERUNNER" />
		</xsl:variable>
		<xsl:variable name="VAR_WAVERUNNER_CHARGE">
			<xsl:call-template name="WAVERUNNER_CHARGE_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_COVERAGE_RELATIVITY">
			<xsl:call-template name="COVERAGE_RELATIVITY_WAVERUNNER" />
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCE_SCORE">
			<xsl:call-template name="INSURANCESCORE" />
		</xsl:variable>
		<xsl:variable name="VAR_DEDUCTIBLE">
			<xsl:call-template name="DED_WAVERUNNER" />
		</xsl:variable>
		<xsl:variable name="VAR_DUAL_OWNERSHIP">
			<xsl:call-template name="DUAL_OWNERSHIP_FACTOR">
				<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_PD" />
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR_TERMS">
			<xsl:call-template name="POLICYTERM" />
		</xsl:variable>
		<xsl:value-of select="round(round(round(round(round(round($VAR_BASE * $VAR_WAVERUNNER_CHARGE)* $VAR_COVERAGE_RELATIVITY )* $VAR_INSURANCE_SCORE) *$VAR_DEDUCTIBLE )*$VAR_DUAL_OWNERSHIP) *$VAR_TERMS)" />
	</xsl:template>
	<!--End Calculation-->
	<!-- ============================================================================================ -->
	<!--Coverage Relativity of Waverunner -->
	<xsl:template name="COVERAGE_RELATIVITY_WAVERUNNER">
		<!-- Same as that of Inboard/Outboard -->
		<xsl:variable name="COVERGE_RELATIVITY_VALUE">
			<xsl:call-template name="GET_COVERAGE">
				<xsl:with-param name="STYLE_FOR_RELATIVITY" select="'INOUTBOARD'" />
			</xsl:call-template>
		</xsl:variable>
		<xsl:value-of select="$COVERGE_RELATIVITY_VALUE" />
	</xsl:template>
	<!--Base Rates values of Waverunner:Find Inboard/Outboard Great Lakes Rate.-->
	<xsl:template name="BASE_RATES_WAVERUNNER">
		<xsl:variable name="PBOATTYPE" select="BOATS/BOAT/BOATTYPE" />
		<xsl:variable name="WATERTYPE" select="BOATS/BOAT/WATERSCODE" />
		<xsl:variable name="PTERRITORY" select="BOATS/BOAT/TERRITORYDOCKEDIN" />
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASERATES']/NODE[@ID=$WATERTYPE]/ATTRIBUTES[@TERRITORYCODE=$PTERRITORY]/@INOUTBOARD" />
	</xsl:template>
	<!-- Waverunner charge -->
	<xsl:template name="WAVERUNNER_CHARGE_FACTOR">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='WAVERUNNER_CHARGE']/NODE[@ID='WAVERUNNER_CHARGES']/ATTRIBUTES/@FACTOR" />
	</xsl:template>
	<!--Wave runner Deduction-->
	<xsl:template name="DED_WAVERUNNER">
		<xsl:variable name="DED_DESC" select="BOATS/BOAT/DEDUCTIBLE"></xsl:variable>
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID='DEDUCTIBLE']/ATTRIBUTES[@DESC = $DED_DESC]/@FACTOR" />
	</xsl:template>
	<!-- Display -->
	<xsl:template name="WAVERUNNER_PREMIUM_DISPLAY">
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='INDIANA'  or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<xsl:choose>
					<xsl:when test="BOATS/BOAT/BOATTYPECODE=$BOATTYPE_WAVERUNNER">Included</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for WaveRunner, Display (END) 									  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN									              -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for MINIJET, Display (START)									  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN									              -->
	<!-- ============================================================================================ -->
	<xsl:template name="MINIJET_RATE_CALC">
		<!-- 
		1) Base Rate.
		2) Multiply by Mini Jet Boat Surcharge 1.10 | Not requried since factormaster has data accordingly.
		3) Choose Great Lake or Inland Lakes (0.95).
		4) Choose Territory.(NOT REQUIRED as we are picking factors territory wise. We do not take base as territory 1)
		5) Multiply by Coverage Relativity. Use Inboard/Outboard Table. If Coverage is $1.00 over coverage amount, then
		use next Coverage Relativity. (Example: For a coverage amount of $26,100 use $27,000 coverage relativity.)
		6) Multiply by Insurance Score Factor.
		7) Multiply by Deductible Relativity.
		8) Multiply by Dual Ownership Surcharge, if eligible.
		9) Term Factor: 1.00 if annual or 0.50 if semi-annual.
		====================================================			
		10) Add: Agreed Value Endorsement.					|
		11) Add: Liability from Table Below.				| These will be added in the final premium formula if applicable
		12) Add: OP-900 Watercraft Pollution Coverage.		|
		====================================================	
		-->
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="BASE_RATES_MINIJET" />
		</xsl:variable>
		<xsl:variable name="VAR_COVERAGE_RELATIVITY">
			<xsl:call-template name="COVERAGE_RELATIVITY_MINIJET" />
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCE_SCORE">
			<xsl:call-template name="INSURANCESCORE" />
		</xsl:variable>
		<xsl:variable name="VAR_DEDUCTIBLE">
			<xsl:call-template name="DED_MINIJET" />
		</xsl:variable>
		<xsl:variable name="VAR_DUAL_OWNERSHIP">
			<xsl:call-template name="DUAL_OWNERSHIP_FACTOR">
				<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_PD" />
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR_TERMS">
			<xsl:call-template name="POLICYTERM" />
		</xsl:variable>
		<xsl:value-of select="round(round(round(round(round($VAR_BASE * $VAR_COVERAGE_RELATIVITY) * $VAR_INSURANCE_SCORE )*$VAR_DEDUCTIBLE )*$VAR_DUAL_OWNERSHIP )*$VAR_TERMS)" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--Coverage Relativity of Minijet -->
	<xsl:template name="COVERAGE_RELATIVITY_MINIJET">
		<!-- Same as that of Inboard/Outboard -->
		<xsl:variable name="COVERGE_RELATIVITY_VALUE">
			<xsl:call-template name="GET_COVERAGE">
				<xsl:with-param name="STYLE_FOR_RELATIVITY" select="'INOUTBOARD'" />
			</xsl:call-template>
		</xsl:variable>
		<xsl:value-of select="$COVERGE_RELATIVITY_VALUE" />
	</xsl:template>
	<!--Base Rates values of Minijet:Find Inboard/Outboard Great Lakes Rate.-->
	<xsl:template name="BASE_RATES_MINIJET">
		<xsl:variable name="PBOATTYPE" select="BOATS/BOAT/BOATTYPE" />
		<xsl:variable name="WATERTYPE" select="BOATS/BOAT/WATERSCODE" />
		<xsl:variable name="PTERRITORY" select="BOATS/BOAT/TERRITORYDOCKEDIN" />
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASERATES']/NODE[@ID=$WATERTYPE]/ATTRIBUTES[@TERRITORYCODE=$PTERRITORY]/@MINIJET" />
	</xsl:template>
	<!-- Minijet charge -->
	<xsl:template name="MINIJET_CHARGE_FACTOR">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINIJET_CHARGE']/NODE[@ID='MINIJET_CHARGES']/ATTRIBUTES/@FACTOR" />
	</xsl:template>
	<!--Minijet Deduction-->
	<xsl:template name="DED_MINIJET">
		<xsl:variable name="DED_DESC" select="BOATS/BOAT/DEDUCTIBLE"></xsl:variable>
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID='DEDUCTIBLE']/ATTRIBUTES[@DESC = $DED_DESC]/@FACTOR" />
	</xsl:template>
	<!-- Display -->
	<xsl:template name="MINIJET_PREMIUM_DISPLAY">
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='INDIANA'  or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<xsl:choose>
					<xsl:when test="BOATS/BOAT/BOATTYPE=$BOATTYPE_MINIJET">Included</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for Minijet, Display (START)									  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN									              -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for JETSKI_WITH_LIFTBAR, Display (START)									  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN									              -->
	<!-- ============================================================================================ -->
	<xsl:template name="JETSKI_W_LIFTBAR_RATE_CALC">
		<!-- 
		1) Find Coverage Base Rate. Rate per $100 and by Deductible option listed above.
		2) Multiply Territory Factor.  | Ths is required 
		3) Multiply by Insurance Score Credit.
		4) Apply Dual Ownership Surcharge (If Eligible).		
		5) Term Factor: 1.00 if annual or 0.50 if semi-annual.
		====================================================			
		6) Add: Agreed Value Endorsement.					|
		7) Add: Liability from Table Below.					| These will be added in the final premium formula if applicable
		8) Add: OP-900 Watercraft Pollution Coverage.		|
		====================================================	
		-->
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="BASE_RATES_JETSKI_W_LIFTBAR" />
		</xsl:variable>
		<xsl:variable name="VAR_TERRITORY_FACTOR">
			<xsl:call-template name="COUNTY_OPERATION_TERRITORY_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCE_SCORE">
			<xsl:call-template name="INSURANCESCORE" />
		</xsl:variable>
		<xsl:variable name="VAR_DUAL_OWNERSHIP">
			<xsl:call-template name="DUAL_OWNERSHIP_FACTOR">
				<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_PD" />
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR_TERMS">
			<xsl:call-template name="POLICYTERM" />
		</xsl:variable>
		<xsl:value-of select="round(round(round(round($VAR_BASE * $VAR_TERRITORY_FACTOR)*$VAR_INSURANCE_SCORE )*$VAR_DUAL_OWNERSHIP )*$VAR_TERMS)" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--Base Rates values of JetSki with liftbar:  Rate.-->
	<xsl:template name="BASE_RATES_JETSKI_W_LIFTBAR">
		<!--  <xsl:variable name="P_COVERAGE_AMOUNT" select="BOATS/BOAT/MARKETVALUE" /> -->
		<xsl:variable name="P_COVERAGE_AMOUNT">
			<xsl:call-template name="MARKETVALUE_IN_HUNDRED">
				<xsl:with-param name="FACTOR_MARKETVAL" select="BOATS/BOAT/MARKETVALUE" />
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="PDEDUCTIBLE" select="BOATS/BOAT/DEDUCTIBLE" />
		<xsl:variable name="P_RATE_PER_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='PDJETSKI']/ATTRIBUTES[@DEDUCTIBLE=$PDEDUCTIBLE]/@RATE_PER_VALUE" />
		<xsl:variable name="P_RATE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='PDJETSKI']/ATTRIBUTES[@DEDUCTIBLE=$PDEDUCTIBLE]/@RATE" />
		<xsl:value-of select="round(($P_COVERAGE_AMOUNT div $P_RATE_PER_VALUE) * $P_RATE)" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for Jet Ski , Display (START)									  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN									              -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for Outboard, Display (START)									  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN									              -->
	<!-- ============================================================================================ -->
	<xsl:template name="OUTBOARD_RATE_CALC">
		<!-- 
		1) Find Outboard Base Rate.															|
		2) Choose Territory and multiply by appropriate relativity.							|
		3) Multiply by Coverage Relativity. If Coverage is $1.00 over coverage amount,		|Same.Just calculate base
			then use next Coverage Relativity.												|
			(Example: For a coverage amount of $26,100 use $27,000 coverage relativity.)	|
		4) Multiply by Insurance Score Factor.
		5) Apply Credits in order as listed. Multiply and Round to the Dollar after each calculation.
		6) Apply Surcharges in order as listed. Multiply and Round to the Dollar after each calculation.
		7) Term Factor: 1.00 if annual or 0.50 if semi-annual.
		====================================================			
		8) Add: Agreed Value Endorsement.					|
		9) Add: Liability from Table Below.					| These will be added in the final premium formula if applicable
		10) Add: OP-900 Watercraft Pollution Coverage.		|
		====================================================	
		-->
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="BASE_RATES_OUTBOARD" />
		</xsl:variable>
		<!-- Same as that of Outboard -->
		<xsl:variable name="COVERGE_RELATIVITY_VALUE">
			<xsl:call-template name="GET_COVERAGE">
				<xsl:with-param name="STYLE_FOR_RELATIVITY" select="'OUTBOARD'" />
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCE_SCORE">
			<xsl:call-template name="INSURANCESCORE" />
		</xsl:variable>
		<!-- Credits in listed order 
			1.Multi-Policy Credit with Home
			2.Experience Credit
			3.Diesel Engine Craft
			4.Deductible
			5.Installation of Halon Fire
			6. Loran Navigation System,
			7. Shore Station Credit
			8. Multi-Boat
			9. Age of Watercraft  
		-->
		<xsl:variable name="VAR_BOAT_HOME">
			<xsl:call-template name="DISCOUNT_BOAT_HOME_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_EXPERIENCE_CREDIT">
			<xsl:call-template name="EXPERIENCE_CREDIT_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_DIESEL_ENGINE">
			<xsl:call-template name="DIESEL_ENGINE_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_DEDUCTIBLE">
			<xsl:call-template name="DEDUCTIBLE_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_HALON_FIRE">
			<xsl:call-template name="HALON_FIRE_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_LORAN_GPS_FACTOR">
			<xsl:call-template name="LORAN_GPS_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_SHORE_STATION">
			<xsl:call-template name="SHORE_STATION_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_MULTI_BOAT">
			<xsl:call-template name="MULTI_BOAT_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_AGE_FACTOR">
			<xsl:call-template name="AGE_FACTOR" />
		</xsl:variable>
		<!-- Surcharges in listed order 
			1. Wooden boats over 10 years old.
			2. Fiberglass boats over 15 years old.
			3. Removal of sailboat racing exclusion under 26 feet only.
			4. Dual Ownership
			5. Boats over 26 feet and speeds 55-65 miles per hour.
		
		-->
		<xsl:variable name="VAR_WOODEN_BOAT_FACTOR">
			<xsl:call-template name="WOODEN_BOAT_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_FIBRE_GLASS_FACTOR">
			<xsl:call-template name="FIBRE_GLASS_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_REMOVE_SAILBOAT_FACTOR">
			<xsl:call-template name="REMOVE_SAILBOAT_FACTOR">
				<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_PD" />
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR_DUAL_OWNERSHIP">
			<xsl:call-template name="DUAL_OWNERSHIP_FACTOR">
				<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_PD" />
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR_LENGTH_SPEED_SURCHARGE">
			<xsl:call-template name="BOAT_LENGTH_SPEED_SURCHARGE">
				<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_PD" />
			</xsl:call-template>
		</xsl:variable>
		<!-- Term Factor -->
		<xsl:variable name="VAR_TERMS">
			<xsl:call-template name="POLICYTERM" />
		</xsl:variable>
		<xsl:variable name="VAR_FIRSTCOMPONENT" select="round(round(round(round(round(round(round(round($VAR_BASE * $COVERGE_RELATIVITY_VALUE)* $VAR_INSURANCE_SCORE )*$VAR_BOAT_HOME )*$VAR_EXPERIENCE_CREDIT)*$VAR_DIESEL_ENGINE)*$VAR_DEDUCTIBLE)*$VAR_HALON_FIRE)*$VAR_LORAN_GPS_FACTOR)" />
		<xsl:value-of select="round(round(round(round(round(round(round(round(round($VAR_FIRSTCOMPONENT*$VAR_SHORE_STATION)*$VAR_MULTI_BOAT)*$VAR_AGE_FACTOR)*$VAR_WOODEN_BOAT_FACTOR)*$VAR_FIBRE_GLASS_FACTOR)*$VAR_REMOVE_SAILBOAT_FACTOR)*$VAR_DUAL_OWNERSHIP)*$VAR_LENGTH_SPEED_SURCHARGE)*$VAR_TERMS)" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--Base Rates values of Outboard:  Rate.-->
	<xsl:template name="BASE_RATES_OUTBOARD">
		<xsl:variable name="PBOATTYPE" select="BOATS/BOAT/BOATTYPE" />
		<xsl:variable name="WATERTYPE" select="BOATS/BOAT/WATERSCODE" />
		<xsl:variable name="PTERRITORY" select="BOATS/BOAT/TERRITORYDOCKEDIN" />
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASERATES']/NODE[@ID=$WATERTYPE]/ATTRIBUTES[@TERRITORYCODE=$PTERRITORY]/@OUTBOARD" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for Outboard , Display (START)									  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN									              -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for Inboard, Inboard/Outboard, Sailboat, Display (START)									  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN									              -->
	<!-- ============================================================================================ -->
	<xsl:template name="INBOARD_IO_SAILBOAT_RATE_CALC">
		<!-- 
		1. Find Base Rate depending on Type of Boat. Great Lakes, Territory 1 is the base for all.
		2. Multiply by Inland Lake Credit Factor 0.95, if eligible.
		3. Choose Territory and multiply by appropriate relativity.
		4. If Inboard or Inboard/Outboard over 26 ft. multiply base by 1.10.
		5. Multiply by Coverage Relativity. If Coverage is 1.00 over coverage amount, then use next Coverage Relativity.
		(Example: Fora coverage amount of 26,100 use 27,000 coverage relativity.)
		6. Multiply by Insurance Score Factor.
		7. Apply Credits in order as listed. Multiply and Round to the Dollar after each calculation.
		8. Apply Surcharges in order as listed. Multiply and Round to the Dollar after each calculation.
		9. Term Factor = 1.00 if annual   0.50 if semi-annual.
		====================================================			
		10. Add: Agreed Value Endorsement.					-
		11. Add: Liability from Table Below.					- These will be added in the final premium formula if applicable
		12. Add: OP-900 Watercraft Pollution Coverage.		-
		====================================================	
		-->
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="BASE_RATES_INBOARD_IO_SAILBOAT_RATE" />
		</xsl:variable>
		<!--  If Inboard or Inboard/Outboard over 26 ft. multiply base by the factor (1.10) ASK RAJAN since we are already charging 10% for length ovr 26 ft-->
		<xsl:variable name="VAR_LENGTH_SURCHARGE">
			<xsl:call-template name="LENGTH_26FEET_FACTOR" />
		</xsl:variable>
		<!-- Different for respective boats -->
		<xsl:variable name="PBOATSTYLECODE" select="BOATS/BOAT/BOATSTYLECODE" />
		<xsl:variable name="COVERGE_RELATIVITY_VALUE">
			<xsl:choose>
				<xsl:when test="BOATS/BOAT/BOATSTYLECODE =$BOATSTYLE_INBOARD">
					<xsl:call-template name="GET_COVERAGE">
						<xsl:with-param name="STYLE_FOR_RELATIVITY" select="'INBOARD'" />
					</xsl:call-template>
				</xsl:when>
				<xsl:when test="BOATS/BOAT/BOATSTYLECODE =$BOATSTYLE_SAILBOAT">
					<xsl:call-template name="GET_COVERAGE">
						<xsl:with-param name="STYLE_FOR_RELATIVITY" select="'SAILBOAT'" />
					</xsl:call-template>
				</xsl:when>
				<xsl:when test="BOATS/BOAT/BOATSTYLECODE =$BOATSTYLE_INBOARD_OUTBOARD">
					<xsl:call-template name="GET_COVERAGE">
						<xsl:with-param name="STYLE_FOR_RELATIVITY" select="'INOUTBOARD'" />
					</xsl:call-template>
				</xsl:when>
				<xsl:otherwise>1.00</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCE_SCORE">
			<xsl:call-template name="INSURANCESCORE" />
		</xsl:variable>
		<!-- Credits in listed order 
			1.Multi-Policy Credit with Home
			2.Experience Credit
			3.Diesel Engine Craft
			4.Deductible
			5.Installation of Halon Fire
			6. Loran Navigation System,
			7. Shore Station Credit
			8. Multi-Boat
			9. Age of Watercraft  
		-->
		<xsl:variable name="VAR_BOAT_HOME">
			<xsl:call-template name="DISCOUNT_BOAT_HOME_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_EXPERIENCE_CREDIT">
			<xsl:call-template name="EXPERIENCE_CREDIT_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_DIESEL_ENGINE">
			<xsl:call-template name="DIESEL_ENGINE_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_DEDUCTIBLE">
			<xsl:call-template name="DEDUCTIBLE_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_HALON_FIRE">
			<xsl:call-template name="HALON_FIRE_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_LORAN_GPS_FACTOR">
			<xsl:call-template name="LORAN_GPS_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_SHORE_STATION">
			<xsl:call-template name="SHORE_STATION_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_MULTI_BOAT">
			<xsl:call-template name="MULTI_BOAT_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_AGE_FACTOR">
			<xsl:call-template name="AGE_FACTOR" />
		</xsl:variable>
		<!-- Surcharges in listed order 
			1. Wooden boats over 10 years old.
			2. Fiberglass boats over 15 years old.
			3. Removal of sailboat racing exclusion under 26 feet only.
			4. Dual Ownership
			5. Boats over 26 feet and speeds 55-65 miles per hour.
		
		-->
		<xsl:variable name="VAR_WOODEN_BOAT_FACTOR">
			<xsl:call-template name="WOODEN_BOAT_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_FIBRE_GLASS_FACTOR">
			<xsl:call-template name="FIBRE_GLASS_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_REMOVE_SAILBOAT_FACTOR">
			<xsl:call-template name="REMOVE_SAILBOAT_FACTOR">
				<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_PD" />
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR_DUAL_OWNERSHIP">
			<xsl:call-template name="DUAL_OWNERSHIP_FACTOR">
				<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_PD" />
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR_LENGTH_SPEED_SURCHARGE">
			<xsl:call-template name="BOAT_LENGTH_SPEED_SURCHARGE">
				<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_PD" />
			</xsl:call-template>
		</xsl:variable>
		<!-- Term Factor -->
		<xsl:variable name="VAR_TERMS">
			<xsl:call-template name="POLICYTERM" />
		</xsl:variable>
		<xsl:variable name="VAR_FIRSTCOMPONENT" select="round(round(round(round(round(round(round(round(round($VAR_BASE *$VAR_LENGTH_SURCHARGE) * $COVERGE_RELATIVITY_VALUE)* $VAR_INSURANCE_SCORE )*$VAR_BOAT_HOME )*$VAR_EXPERIENCE_CREDIT)*$VAR_DIESEL_ENGINE)*$VAR_DEDUCTIBLE)*$VAR_HALON_FIRE)*$VAR_LORAN_GPS_FACTOR)" />
		<xsl:value-of select="round(round(round(round(round(round(round(round(round($VAR_FIRSTCOMPONENT*$VAR_SHORE_STATION)*$VAR_MULTI_BOAT)*$VAR_AGE_FACTOR)*$VAR_WOODEN_BOAT_FACTOR)*$VAR_FIBRE_GLASS_FACTOR)*$VAR_REMOVE_SAILBOAT_FACTOR)*$VAR_DUAL_OWNERSHIP)*$VAR_LENGTH_SPEED_SURCHARGE)*$VAR_TERMS)" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--Base Rates values of Inboard, Inboard/Outboard, Sailboat:  Rate.-->
	<xsl:template name="BASE_RATES_INBOARD_IO_SAILBOAT_RATE">
		<xsl:variable name="PBOATSTYLECODE" select="BOATS/BOAT/BOATSTYLECODE" />
		<xsl:variable name="WATERTYPE" select="BOATS/BOAT/WATERSCODE" />
		<xsl:variable name="PTERRITORY" select="BOATS/BOAT/TERRITORYDOCKEDIN" />
		<xsl:choose>
			<xsl:when test="$PBOATSTYLECODE = $BOATSTYLE_INBOARD">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASERATES']/NODE[@ID=$WATERTYPE]/ATTRIBUTES[@TERRITORYCODE=$PTERRITORY]/@INBOARD" />
			</xsl:when>
			<xsl:when test="$PBOATSTYLECODE = $BOATSTYLE_SAILBOAT">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASERATES']/NODE[@ID=$WATERTYPE]/ATTRIBUTES[@TERRITORYCODE=$PTERRITORY]/@SAILBOAT" />
			</xsl:when>
			<xsl:when test="$PBOATSTYLECODE = $BOATSTYLE_INBOARD_OUTBOARD">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASERATES']/NODE[@ID=$WATERTYPE]/ATTRIBUTES[@TERRITORYCODE=$PTERRITORY]/@INOUTBOARD" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for Inboard, Inboard/Outboard, Sailboat , Display (START)									  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN									              -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for Inboard, Inboard/Outboard, Sailboat, Display (START)									  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN									              -->
	<!-- ============================================================================================ -->
	<xsl:template name="TRAILOR_RATE_CALC">
		<!-- 
		1. Find Base Rate depending on deductible.		
		-->
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="BASE_RATES_TRAILERS" />
		</xsl:variable>
		<xsl:value-of select="$VAR_BASE" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--Base Rates values of Trailers :  Rate.-->
	<xsl:template name="BASE_RATES_TRAILERS">
		<xsl:variable name="PDEDUCTIBLE" select="BOATS/BOAT/DEDUCTIBLE" />
		<xsl:variable name="PMARKETVALUE" select="BOATS/BOAT/MARKETVALUE" />
		<!--Deductible  varies for jetski trailer,waverunner trailer,trailer on the basis of year.
			It may  be percentage deductible in one yr and flat in the next. Trailer other than JetSki trailer
			will be treated as Waverunner Trailer -->
		<xsl:variable name="P_TRAILER_TYPE">
			<xsl:choose>
				<xsl:when test="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_JETSKI_TRAILER or  BOATS/BOAT/BOATTYPECODE =$BOATTYPE_JETSKI_TRAILER_FROM_APP">
					<xsl:value-of select="'TRAILER_JETSKI'" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="'TRAILER_WAVERUNNER'" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="contains($PDEDUCTIBLE ,'%-')"> <!--  implies it is a percentage deductible -->
				<xsl:variable name="PRATE_PER_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID=$P_TRAILER_TYPE]/@RATE_PER_VALUE" />
				<xsl:variable name="PRATE_PER" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID=$P_TRAILER_TYPE]/ATTRIBUTES[@DESC = $PDEDUCTIBLE]/@FACTOR" />
				<xsl:value-of select="round($PRATE_PER * $PMARKETVALUE div $PRATE_PER_VALUE)" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="PRATE_PER_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID=$P_TRAILER_TYPE]/@RATE_PER_VALUE" />
				<xsl:variable name="PRATE_PER" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID=$P_TRAILER_TYPE]/ATTRIBUTES[@DEDUCTIBLE = $PDEDUCTIBLE]/@FACTOR" />
				<xsl:value-of select="round($PRATE_PER * $PMARKETVALUE div $PRATE_PER_VALUE)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for Trailer, Display (END)									  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN									              -->
	<!-- ============================================================================================ -->
	<!-- Template for Coverage -->
	<xsl:template name="GET_COVERAGE">
		<xsl:param name="STYLE_FOR_RELATIVITY" />
		<!-- Get the market/coverage value of the boat -->
		<xsl:variable name="CVALUE">
			<!-- <xsl:value-of select="BOATS/BOAT/MARKETVALUE"/>  -->
			<xsl:call-template name="MARKETVALUE_IN_THOUSANDS">
				<xsl:with-param name="FACTOR_MARKETVAL" select="BOATS/BOAT/MARKETVALUE" />
			</xsl:call-template>
		</xsl:variable>
		<!-- get the max value of coverages in the database -->
		<xsl:variable name="P_MAX_VALUE">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='RATERELATIVITY']/NODE[@ID=$STYLE_FOR_RELATIVITY]/@MAXVALUE" />
		</xsl:variable>
		<xsl:choose>
			<!-- If the coverage selected is less than the max value then pick straight -->
			<xsl:when test="$CVALUE &lt;= $P_MAX_VALUE">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='RATERELATIVITY']/NODE[@ID=$STYLE_FOR_RELATIVITY]/ATTRIBUTES[@COVERAGE = $CVALUE]/@RELATIVITY" />
			</xsl:when>
			<!-- otherwise use the additional concept -->
			<xsl:otherwise>
				<!-- For each Additional amount-->
				<xsl:variable name="P_ADDITIONAL_VALUE">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='RATERELATIVITY']/NODE[@ID=$STYLE_FOR_RELATIVITY]/ATTRIBUTES[@COVERAGE = 'ADDITIONAL']/@AMOUNT" />
				</xsl:variable>
				<!-- value -->
				<xsl:variable name="P_ADDITIONAL_FACTOR">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='RATERELATIVITY']/NODE[@ID=$STYLE_FOR_RELATIVITY]/ATTRIBUTES[@COVERAGE = 'ADDITIONAL']/@RELATIVITY" />
				</xsl:variable>
				<!-- value for the max factor -->
				<xsl:variable name="P_MAX_VALUE_FACTOR">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='RATERELATIVITY']/NODE[@ID=$STYLE_FOR_RELATIVITY]/ATTRIBUTES[@COVERAGE = $P_MAX_VALUE]/@RELATIVITY" />
				</xsl:variable>
				<!-- Calculations 
					e.g : factor for 250 + ((difference in the cov and the max value)/additional amount)* (factor for additional)-->
				<xsl:value-of select="($P_MAX_VALUE_FACTOR + ( (($CVALUE - $P_MAX_VALUE) div $P_ADDITIONAL_VALUE) * $P_ADDITIONAL_FACTOR ))" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Insurance Score  ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="INSURANCESCORE">
		<xsl:variable name="INS1" select="POLICY/INSURANCESCORE"></xsl:variable>
		<xsl:variable name="INS" select="translate(translate($INS1,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')" />
		<!-- FOR MAXIMUM SCORE -->
		<xsl:variable name="VAR_MAX_SCORE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='INSURANCE']/ATTRIBUTES[@MAX = 'MAX_SCORE']/@MINSCORE" />
		<xsl:choose>
			<xsl:when test="$INS = 0 or $INS = ''">
				1.00  
			</xsl:when>
			<xsl:when test="$INS = 'NOHITNOSCORE'"> <!-- NO HIT NO SCORE -->
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='INSURANCE']/ATTRIBUTES[@MINSCORE = 'N']/@FACTOR" />
			</xsl:when>
			<xsl:when test="$INS &gt;= $VAR_MAX_SCORE">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='INSURANCE']/ATTRIBUTES[@MINSCORE = $VAR_MAX_SCORE]/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='INSURANCE']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@FACTOR" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="INSURANCESCORE_DISPLAY">
		<xsl:variable name="ISCORE" select="POLICY/INSURANCESCORE"></xsl:variable>
		<xsl:variable name="INS" select="translate(translate($ISCORE,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')" />
		<!-- FOR MAXIMUM SCORE -->
		<xsl:variable name="VAR_MAX_SCORE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='INSURANCE']/ATTRIBUTES[@MAX = 'MAX_SCORE']/@MINSCORE" />
		<xsl:variable name="INSDISCOUNT">
			<xsl:choose>
				<xsl:when test="$INS = 0">
					<xsl:value-of select="$INS"></xsl:value-of>
				</xsl:when>
				<xsl:when test="$INS = ''">
				1.00
			</xsl:when>
				<xsl:when test="$INS = 'NOHITNOSCORE'"> <!-- NO HIT NO SCORE -->
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='INSURANCE']/ATTRIBUTES[@MINSCORE = 'N']/@FACTOR" />
				</xsl:when>
				<xsl:when test="$INS &gt;= $VAR_MAX_SCORE">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='INSURANCE']/ATTRIBUTES[@MINSCORE = $VAR_MAX_SCORE]/@FACTOR" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='INSURANCE']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@FACTOR" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<!-- Triailer style - Not reqd for trailer type-->
			<xsl:when test="BOATS/BOAT/BOATSTYLECODE =  $BOATSTYLE_TRAILER">		
		0
		</xsl:when>
			<xsl:when test="$INSDISCOUNT = 0 or $INSDISCOUNT='' or $INSDISCOUNT=1">
			0
		</xsl:when>
			<xsl:when test="$INSDISCOUNT &gt; 0 and $INSDISCOUNT &lt;= 1">
			Included
		</xsl:when>
			<xsl:otherwise>	
			0		 
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="INSURANCESCORE_PERCENT">
		<xsl:variable name="VAR_SCORE">
			<xsl:call-template name="INSURANCESCORE" />
		</xsl:variable>
		<xsl:variable name="VAR_SCORE_PERCENT">
			<xsl:choose>
				<xsl:when test="$VAR_SCORE != '' and $VAR_SCORE &lt; 1.00">
					<xsl:value-of select="round((1 - $VAR_SCORE) * 100)" /><xsl:text>%</xsl:text>
				</xsl:when>
				<xsl:when test="$VAR_SCORE != '' and $VAR_SCORE &gt; 1.00">
					<xsl:value-of select="round(($VAR_SCORE -1 ) * 100)" /><xsl:text>%</xsl:text>
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:value-of select="$VAR_SCORE_PERCENT" />
	</xsl:template>
	<xsl:template name="INSURENCESCOREDISPLAY">
		<xsl:choose>
			<xsl:when test="normalize-space(POLICY/INSURANCESCORE)='NOHITNOSCORE'">No Hit No Score</xsl:when>
			<xsl:when test="POLICY/INSURANCESCORE &gt;=0">
				<xsl:value-of select="POLICY/INSURANCESCORE" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Insurance Score ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ============================================================================================ -->
	<!--					Templates for Agreed Value Endorsement (AV100), Display (START)			  -->
	<!--		    					  FOR INDIANA	,MICHIGAN,WISCONSIN										              -->
	<!-- 
	1. The maximum Agreed Value will not exceed $75,000. The maximum length of the watercraft 
	 will not exceed 26'.
	2. The Agreed Value will not exceed the high retail value as established in the BUC book.
	 If the value is higher, a current marine survey (last 12 months) or other substantiating evidence 
	 is required prior to binding, such as original purchase agreement if within last 12 months.
	3. The Company will accept a copy of the original purchase agreement or receipt as the Agreed Value 
		if the original	purchase was within 12 months of the date of the application.
	4. The maximum age of the boat will not exceed 20 years at the time the policy is written.
	5. The hull values and outboard motor values will be reviewed and adjusted at company discretion 
		on a periodic basis.Any adjustments made by the company will be provided to the agent and insured.
	6. Marine surveys and/or photos may be required at any time at the discretion of the company.  
	7. MINIMUM =10   -->
	<!-- ============================================================================================ -->
	<xsl:template name="AGREED_VALUE_ENDORSEMENT">
		<!-- <xsl:variable name="P_AGREED_VALUE" select="BOATS/BOAT/MARKETVALUE"/> -->
		<xsl:variable name="P_AGREED_VALUE">
			<xsl:call-template name="MARKETVALUE_IN_THOUSANDS">
				<xsl:with-param name="FACTOR_MARKETVAL" select="BOATS/BOAT/MARKETVALUE" />
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="P_MAX_BOAT_LENGTH" select="BOATS/BOAT/LENGTH" />
		<xsl:variable name="P_MAX_AGREED_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='AGREED_VALUE_ENDORSEMENT']/NODE[@ID ='AGREED_VALUE']/ATTRIBUTES/@MAX_VALUE" />
		<xsl:variable name="P_MIN_AGREED_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='AGREED_VALUE_ENDORSEMENT']/NODE[@ID ='AGREED_VALUE']/ATTRIBUTES/@MINIMUM_ENDORSEMENT" />
		<xsl:choose>
			<!-- <xsl:when test="(BOATS/BOAT/AV100 = 'Y') and (BOATS/BOAT/LENGTH &lt;= $P_MAX_BOAT_LENGTH) and (BOATS/BOAT/MARKETVALUE &lt;= $P_MAX_AGREED_VALUE) "> -->
			<xsl:when test="(BOATS/BOAT/AV100 = 'Y') and (BOATS/BOAT/LENGTH &lt;= $P_MAX_BOAT_LENGTH) and ($P_AGREED_VALUE &lt;= $P_MAX_AGREED_VALUE) ">
				<xsl:variable name="P_RATE_OF_ENDORSEMENT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='AGREED_VALUE_ENDORSEMENT']/NODE[@ID ='AGREED_VALUE']/ATTRIBUTES/@CHARGE" />
				<xsl:variable name="P_RATE_PER_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='AGREED_VALUE_ENDORSEMENT']/NODE[@ID ='AGREED_VALUE']/ATTRIBUTES/@RATE_PER_VALUE" />
				<xsl:variable name="P_AGREED_VALUE_ENDORSEMENT">
					<xsl:value-of select="round(($P_AGREED_VALUE div $P_RATE_PER_VALUE)* $P_RATE_OF_ENDORSEMENT)" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$P_AGREED_VALUE_ENDORSEMENT &lt; $P_MIN_AGREED_VALUE">
						<xsl:value-of select="$P_MIN_AGREED_VALUE" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$P_AGREED_VALUE_ENDORSEMENT" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Templates for Agreed Value Endorsement (AV100), Display (END)			  -->
	<!--		    					  FOR INDIANA,MICHGAN,WISCONSIN					              -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for Client Entertainment (OP720), Display (START)					  -->
	<!--		    					  FOR INDIANA	, MICHIGAN, WISCONSIN			              -->
	<!-- ============================================================================================ -->
	<xsl:template name="CLIENT_ENTERTAINMENT">
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<xsl:choose>
					<xsl:when test="BOATS/BOAT/OP720 = 'Y'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='CLTENTERTAINMENT']/ATTRIBUTES/@RATE" />
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for Client Entertainment (OP720), Display (END)					  -->
	<!--		    					  FOR INDIANA	, MICHIGAN, WISCONSIN			              -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--			Templates for Watercraft Liability Poolution Coverage (OP900), Display (START)	  -->
	<!--		    					  FOR INDIANA									              -->
	<!-- ============================================================================================ -->
	<xsl:template name="LIABILITY_POLLUTION_COVERAGE">
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<xsl:choose>
					<xsl:when test="BOATS/BOAT/OP900 = 'Y'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='POLLUTIONCOV']/ATTRIBUTES/@RATE" />
					</xsl:when>
					<xsl:otherwise>
						0
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for Watercraft Liability Polution Coverage (OP900), Display (END)  -->
	<!--		    					  FOR INDIANA									              -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--					Templates for length over 26 Feet, Display (START)						  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN		              -->
	<!--  Charge - Length over 26 Feet +10% should only be applied to Inboard & Inboard/Outboard  -->
	<!-- ============================================================================================ -->
	<xsl:template name="LENGTH_26FEET_DISPLAY">
		<xsl:variable name="P_MAX_LENGTH" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDBOATLENGTH']/ATTRIBUTES/@BOAT_LENGTH_OVER" />
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER ">
				0
			</xsl:when>
			<xsl:when test="(BOATS/BOAT/LENGTH &gt; $P_MAX_LENGTH and (BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_INBOARD_OUTBOARD or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_INBOARD or BOATS/BOAT/BOATSTYLECODE =$BOATSTYLE_SAILBOAT or BOATS/BOAT/BOATSTYLECODE =$BOATSTYLE_OUTBOARD)and (BOATS/BOAT/BOATTYPECODE!=$BOATTYPE_MINI_JET) and (BOATS/BOAT/BOATTYPECODE!=$BOATTYPE_WAVERUNNER))">Charge - Length over <xsl:value-of select="($P_MAX_LENGTH div 12)" /> Feet +<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDBOATLENGTH']/ATTRIBUTES/@SURCHARGE" /><xsl:text>%</xsl:text></xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="LENGTH_26FEET_PREMIUM">
		<xsl:variable name="P_MAX_LENGTH" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDBOATLENGTH']/ATTRIBUTES/@BOAT_LENGTH_OVER" />
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<xsl:choose>
					<xsl:when test="(BOATS/BOAT/LENGTH &gt; $P_MAX_LENGTH and (BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_INBOARD_OUTBOARD or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_INBOARD or BOATS/BOAT/BOATSTYLECODE =$BOATSTYLE_SAILBOAT or BOATS/BOAT/BOATSTYLECODE =$BOATSTYLE_OUTBOARD) and (BOATS/BOAT/BOATTYPECODE!=$BOATTYPE_MINI_JET) and (BOATS/BOAT/BOATTYPECODE!=$BOATTYPE_WAVERUNNER))">Included</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="LENGTH_26FEET_FACTOR">
		<xsl:variable name="P_MAX_LENGTH" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDBOATLENGTH']/ATTRIBUTES/@BOAT_LENGTH_OVER" />
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<xsl:choose>
					<xsl:when test="(BOATS/BOAT/LENGTH &gt; $P_MAX_LENGTH and (BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_INBOARD_OUTBOARD or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_INBOARD or BOATS/BOAT/BOATSTYLECODE =$BOATSTYLE_SAILBOAT or BOATS/BOAT/BOATSTYLECODE =$BOATSTYLE_OUTBOARD)  and (BOATS/BOAT/BOATTYPECODE!=$BOATTYPE_MINI_JET) and (BOATS/BOAT/BOATTYPECODE!=$BOATTYPE_WAVERUNNER))">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDBOATLENGTH']/ATTRIBUTES/@FACTOR" />
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Templates for length over 26 Feet, Display (END)						  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN			              -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--					Templates for Policy Term Display (START)-->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN							  -->
	<!--																							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="POLICYTERM">
		<xsl:variable name="P_POLICYTERMS" select="POLICY/POLICYTERMS" />
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERM_FACTOR']/NODE[@ID ='TERMFACTOR']/ATTRIBUTES[@MONTHS = $P_POLICYTERMS]/@FACTOR" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Templates for Policy Term, Display (END)			  -->
	<!--		    					  FOR INDIANA,MICHGAN,WISCONSIN					              -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for Boat Towinge, Display (START)									  -->
	<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN				          -->
	<!-- ============================================================================================ -->
	<xsl:template name="BOAT_TOWING_PREMIUM">
		<!-- Fetch style -->
		<xsl:variable name="P_BOAT_STYLE">
			<xsl:value-of select="BOATS/BOAT/BOATSTYLECODE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<xsl:choose>
					<!-- Will not be included for Trailer types (and coverage type basis is "Not Applicable" - Asfa 09-May-2007)  -->
					<xsl:when test="$P_BOAT_STYLE = $BOATSTYLE_TRAILER or BOATS/BOAT/COVERAGEBASIS='ANA' or BOATS/BOAT/BOATTYPECODE='JS' or BOATS/BOAT/BOATTYPECODE='PWT' or BOATS/BOAT/BOATTYPECODE='MJB' or BOATS/BOAT/BOATTYPECODE='W'">
 					0
 				</xsl:when>
					<xsl:otherwise>
 		   			Included
 				</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="BOAT_TOWING_LIMIT">
		<!-- Fetch style -->
		<xsl:variable name="P_BOAT_STYLE">
			<xsl:value-of select="BOATS/BOAT/BOATSTYLECODE" />
		</xsl:variable>
		<xsl:variable name="P_MARKET_VALUE" select="BOATS/BOAT/MARKETVALUE" />
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<xsl:choose>
					<!-- Will not be included for Trailer types  -->
					<xsl:when test="($P_BOAT_STYLE != $BOATSTYLE_TRAILER) and ($P_MARKET_VALUE !='') and ($P_MARKET_VALUE &gt; 0) ">
						<xsl:variable name="P_BOAT_TOWING_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BOAT_TOWING_FACTOR']/NODE[@ID ='BOAT_TOWING']/ATTRIBUTES/@FACTOR" />
						<xsl:value-of select="round($P_MARKET_VALUE * $P_BOAT_TOWING_FACTOR)" />
					</xsl:when>
					<xsl:otherwise>
 		   			 0
 				</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				END Templates for Boat Towinge, Display (START)								  -->
	<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN				          -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for WATERCRAFT LIABILITY, Display (START)							  -->
	<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN								              -->
	<!-- ============================================================================================ -->
	<xsl:template name="WATERCRAFT_LIABILITY_TEXT">
		<xsl:variable name="P_MAX_SAILBOAT_LENGTH_FOR_HOME" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SAILBOAT_LENGTH_FOR_HOME" />
		<!-- Fetch style -->
		<xsl:variable name="P_BOAT_STYLE">
			<xsl:value-of select="BOATS/BOAT/BOATSTYLECODE" />
		</xsl:variable>
		<xsl:choose>
			<!-- Will not be included for Trailer types  -->
			<xsl:when test="$P_BOAT_STYLE = $BOATSTYLE_TRAILER">
 			0
 		</xsl:when>
			<!--When Personal Liability holds "EFH", -->
			<xsl:when test="(BOATS/BOAT/PERSONALLIABILITY='EFH' or POLICY/ATTACHTOWOLVERINE = 'Y' or POLICY/BOATHOMEDISC = 'Y')
						and ((BOATS/BOAT/BOATSTYLECODE =  $BOATSTYLE_OUTBOARD) or (BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_SAILBOAT and BOATS/BOAT/LENGTH &lt;= $P_MAX_SAILBOAT_LENGTH_FOR_HOME) or (BOATS/BOAT/BOATTYPECODE = $BOATTYPE_PONTOON_INBOARD_OUTBOARD))">
					Watercraft Liability - extended from Homeowners
			</xsl:when>
			<xsl:otherwise>Watercraft Liability</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="WATERCRAFT_LIABILITY">
		<xsl:variable name="LIABILITYAMOUNT" select="BOATS/BOAT/PERSONALLIABILITY" />
		<xsl:variable name="LENGTH" select="BOATS/BOAT/LENGTH" />
		<xsl:variable name="LIABILITY_AMOUNT" select="translate(translate($LIABILITYAMOUNT,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')" />
		<xsl:variable name="P_MAX_SAILBOAT_LENGTH_FOR_HOME" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SAILBOAT_LENGTH_FOR_HOME" />
		<xsl:variable name="P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SAILBOAT_LENGTH_FOR_LIABILITY" />
		<!-- Common variables/factors to be used below -->
		<!-- Liability Surcharge for Length and Speed -->
		<xsl:variable name="VAR_LIABILITY_SURCHARGES">
			<xsl:call-template name="BOAT_LENGTH_SPEED_SURCHARGE">
				<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_LIABILITY" />
			</xsl:call-template>
		</xsl:variable>
		<!-- Dual Ownership -->
		<xsl:variable name="VAR_DUAL_OWNERSHIP">
			<xsl:call-template name="DUAL_OWNERSHIP_FACTOR">
				<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_LIABILITY" />
			</xsl:call-template>
		</xsl:variable>
		<!-- Remove Sailboat -->
		<xsl:variable name="VAR_REMOVE_SAILBOAT_FACTOR">
			<xsl:call-template name="REMOVE_SAILBOAT_FACTOR">
				<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_LIABILITY" />
			</xsl:call-template>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$LIABILITY_AMOUNT ='NOCOVERAGE' or $LIABILITY_AMOUNT =''">0</xsl:when>
			<xsl:when test="$LIABILITY_AMOUNT &lt;= 0">0</xsl:when>
			<!-- Waverunner Type -->
			<xsl:when test="BOATS/BOAT/BOATTYPECODE =$BOATTYPE_WAVERUNNER and $LIABILITY_AMOUNT !=''">
				<xsl:variable name="MIN_LIABILITY_WAVERUNNER" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='SECTIONIILIABILITY']/@MINIMUM_COVERAGE" />
				<xsl:variable name="VAR_LIABILITY_AMOUNT">
					<xsl:choose>
						<xsl:when test="$LIABILITY_AMOUNT &lt;= $MIN_LIABILITY_WAVERUNNER">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='SECTIONIILIABILITY']/@MIN_LIABILITY_PREMIUM_WAVERUNNER" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='SECTIONIILIABILITY']/ATTRIBUTES[@LIABILITY=$LIABILITY_AMOUNT]/@WAVERUNNERRATE" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:value-of select="$VAR_LIABILITY_AMOUNT * $VAR_LIABILITY_SURCHARGES" />
			</xsl:when>
			<!-- Mini Jet Boat -->
			<xsl:when test="BOATS/BOAT/BOATTYPECODE =$BOATTYPE_MINI_JET and $LIABILITY_AMOUNT !=''">
				<xsl:variable name="VAR_LIABILITY_AMOUNT">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID='MINIJETBOARD']/ATTRIBUTES[@LIABILITY=$LIABILITY_AMOUNT]/@RATE" />
				</xsl:variable>
				<xsl:value-of select="round($VAR_LIABILITY_AMOUNT * $VAR_DUAL_OWNERSHIP)"></xsl:value-of>
			</xsl:when>
			<!-- Outboard style/Sailboat less than 26 feet and Attached to Home -->
			<xsl:when test="((POLICY/ATTACHTOWOLVERINE = 'Y' or POLICY/BOATHOMEDISC = 'Y' )
						and ((BOATS/BOAT/BOATTYPECODE != $BOATTYPE_WAVERUNNER and BOATS/BOAT/BOATSTYLECODE =  $BOATSTYLE_OUTBOARD) 
							  or (BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_SAILBOAT and BOATS/BOAT/LENGTH &lt;= $P_MAX_SAILBOAT_LENGTH_FOR_HOME)or(BOATS/BOAT/BOATTYPECODE = $BOATTYPE_PONTOON_INBOARD_OUTBOARD)))">
			0			
		</xsl:when>
			<!-- Jet Ski Style-->
			<xsl:when test="normalize-space(BOATS/BOAT/BOATSTYLECODE) = normalize-space($BOATSTYLE_JETSKI) and $LIABILITY_AMOUNT !=''">
				<xsl:variable name="MIN_LIABILITY_JETSKI" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='SECTIONIILIABILITY']/@MINIMUM_COVERAGE" />
				<xsl:variable name="VAR_LIABILITY_AMOUNT">
					<xsl:choose>
						<xsl:when test="$LIABILITY_AMOUNT &lt;= $MIN_LIABILITY_JETSKI">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='SECTIONIILIABILITY']/@MIN_LIABILITY_PREMIUM_JETSKI" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='SECTIONIILIABILITY']/ATTRIBUTES[@LIABILITY=$LIABILITY_AMOUNT]/@JETSKIRATE" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:value-of select="$VAR_LIABILITY_AMOUNT * $VAR_LIABILITY_SURCHARGES" />
			</xsl:when>
			<!-- Triailer style -->
			<xsl:when test="BOATS/BOAT/BOATSTYLECODE =  $BOATSTYLE_TRAILER">0</xsl:when>
			<!-- Outboard style. Speed not available for Outboard -->
			<xsl:when test="BOATS/BOAT/BOATSTYLECODE =  $BOATSTYLE_OUTBOARD and $LIABILITY_AMOUNT !=''">
				<xsl:variable name="VAR_LIABILITY">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID='LIOUTBOARD']/ATTRIBUTES[@LIABILITY=$LIABILITY_AMOUNT]/@RATE" />
				</xsl:variable>
				<xsl:value-of select="round(round(round($VAR_LIABILITY * $VAR_DUAL_OWNERSHIP)*$VAR_LIABILITY_SURCHARGES) * $VAR_REMOVE_SAILBOAT_FACTOR)" />
			</xsl:when>
			<!-- Sailboat style -->
			<xsl:when test="(normalize-space(BOATS/BOAT/BOATSTYLECODE) = normalize-space($BOATSTYLE_SAILBOAT)) and (BOATS/BOAT/LENGTH &lt; $P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY) and $LIABILITY_AMOUNT !=''">
				<xsl:variable name="VAR_LIABILITY">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID='LIOUTBOARD']/ATTRIBUTES[@LIABILITY=$LIABILITY_AMOUNT]/@RATE" />
				</xsl:variable>
				<xsl:value-of select="round($VAR_LIABILITY*$VAR_DUAL_OWNERSHIP*$VAR_LIABILITY_SURCHARGES*$VAR_REMOVE_SAILBOAT_FACTOR)" />
			</xsl:when>
			<!-- Other boat types -->
			<xsl:otherwise>
				<xsl:variable name="VAR_LIABILITY">
					<xsl:choose>
						<xsl:when test="$LIABILITY_AMOUNT = ''">0</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID='ALLOUTBOARD']/ATTRIBUTES[@LIABILITY=$LIABILITY_AMOUNT]/@RATE" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:value-of select="round(round($VAR_LIABILITY * $VAR_DUAL_OWNERSHIP) * $VAR_LIABILITY_SURCHARGES)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="WATERCRAFT_LIABILITY_DISPLAY_EXTENDED">
		<xsl:variable name="P_MAX_SAILBOAT_LENGTH_FOR_HOME" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SAILBOAT_LENGTH_FOR_HOME" />
		<xsl:choose>
			<!-- Outboard style/Sailboat less than 26 feet and Attached to Home -->
			<xsl:when test="(BOATS/BOAT/PERSONALLIABILITY='EFH' or POLICY/ATTACHTOWOLVERINE = 'Y' or POLICY/BOATHOMEDISC = 'Y')
						and ((BOATS/BOAT/BOATSTYLECODE =  $BOATSTYLE_OUTBOARD)or (BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_SAILBOAT and BOATS/BOAT/LENGTH &lt;= $P_MAX_SAILBOAT_LENGTH_FOR_HOME)or (BOATS/BOAT/BOATTYPECODE = $BOATTYPE_PONTOON_INBOARD_OUTBOARD))">
			Extended
		</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--PERSONAL LIABILITY holds 'EFH' if 'Extended From Home', so Watercraft Liability Limit value is not shown in that case  - Asfa Praveen 22/May/2007 -->
	<xsl:template name="PERSONALLIABILITY_LIMIT_DISPLAY">
		<xsl:choose>
			<xsl:when test="normalize-space(BOATS/BOAT/PERSONALLIABILITY) ='EFH' and (BOATS/BOAT/PERSONALLIABILITY ='No Coverage' or BOATS/BOAT/PERSONALLIABILITY ='NO COVERAGE')">
				No Coverage
			</xsl:when>
			<xsl:when test="BOATS/BOAT/PERSONALLIABILITY !=''">
				<xsl:value-of select="BOATS/BOAT/PERSONALLIABILITY" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for WATERCRAFT LIABILITY, Display (END)							  -->
	<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN								              -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for MEDICAL PAYMENTS, Display (START)								 -->
	<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN									              -->
	<!-- ============================================================================================ -->
	<!--MEDICAL PAYMENTS holds 'EFH' if 'Extended From Home', so MEDICAL PAYMENTS Limit value is not shown in that case  - Asfa Praveen 22/May/2007 -->
	<xsl:template name="MEDICALPAYMENT_LIMIT_DISPLAY">
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/MEDICALPAYMENT !='' and normalize-space(BOATS/BOAT/MEDICALPAYMENT) !='EFH'">
				<xsl:value-of select="BOATS/BOAT/MEDICALPAYMENT" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MEDICAL_PAYMENTS_DISPLAY">
		<xsl:variable name="P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SAILBOAT_LENGTH_FOR_LIABILITY" />
		<xsl:variable name="P_MEDICAL_PAYMENT" select="BOATS/BOAT/MEDICALPAYMENT" />
		<xsl:variable name="P_MEDICAL_PAYMENT_AMOUNT" select="translate(translate($P_MEDICAL_PAYMENT,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')" />
		<xsl:choose>
			<!-- No coverage -->
			<xsl:when test="normalize-space($P_MEDICAL_PAYMENT_AMOUNT) ='' or normalize-space($P_MEDICAL_PAYMENT_AMOUNT) ='NOCOVERAGE'">0</xsl:when>
			<!-- Not for trailer -->
			<xsl:when test="BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER">0</xsl:when>
			<!-- Outboard style/Sailboat less than 26 feet and Attached to Home -->
			<!--When MEDICAL PAYMENTS holds "EFH", Show "Extended" text - Asfa Praveen - 22-May-2007 -->
			<xsl:when test="(BOATS/BOAT/MEDICALPAYMENT ='EFH') or (((POLICY/ATTACHTOWOLVERINE = 'Y') or (POLICY/BOATHOMEDISC = 'Y'))
						and ((BOATS/BOAT/BOATSTYLECODE =  $BOATSTYLE_OUTBOARD) 
								or (BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_SAILBOAT and BOATS/BOAT/LENGTH &lt;= $P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY) or  (BOATS/BOAT/BOATTYPECODE = $BOATTYPE_PONTOON_INBOARD_OUTBOARD)))">
			Extended
		</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_JETSKI or  BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET">
					Included
				</xsl:when>
					<xsl:when test="BOATS/BOAT/MEDICALPAYMENT = 1000">
					Included
				</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MEDICAL_PAYMENTS_TEXT">
		<xsl:variable name="P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SAILBOAT_LENGTH_FOR_LIABILITY" />
		<xsl:choose>
			<!-- Not for trailer -->
			<xsl:when test="BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER">
			0
		</xsl:when>
			<!--When MEDICAL PAYMENTS holds "EFH", Show "Extended From Homeowners" text - Asfa Praveen - 22-May-2007 -->
			<xsl:when test="
			(BOATS/BOAT/MEDICALPAYMENT ='EFH') or
			(( (POLICY/ATTACHTOWOLVERINE = 'Y') or (POLICY/BOATHOMEDISC = 'Y') )
			and (BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_OUTBOARD))
			or
			(( (POLICY/ATTACHTOWOLVERINE = 'Y') or (POLICY/BOATHOMEDISC = 'Y') )
			and
			(BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_SAILBOAT and BOATS/BOAT/LENGTH &lt;= $P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY) or (BOATS/BOAT/BOATTYPECODE = $BOATTYPE_PONTOON_INBOARD_OUTBOARD))">
				Medical Payments To Others - extended from Homeowners
		 </xsl:when>
			<xsl:otherwise>Medical Payments To Others</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MEDICAL_PAYMENTS">
		<xsl:variable name="P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SAILBOAT_LENGTH_FOR_LIABILITY" />
		<xsl:variable name="P_MEDICAL_PAYMENT" select="BOATS/BOAT/MEDICALPAYMENT" />
		<xsl:variable name="P_MEDICAL_PAYMENT_AMOUNT" select="translate(translate($P_MEDICAL_PAYMENT,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')" />
		<!-- Liability Surcharge for Length and Speed -->
		<xsl:variable name="VAR_LIABILITY_SURCHARGES">
			<xsl:call-template name="BOAT_LENGTH_SPEED_SURCHARGE">
				<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_LIABILITY" />
			</xsl:call-template>
		</xsl:variable>
		<!-- Dual Ownership -->
		<xsl:variable name="VAR_DUAL_OWNERSHIP">
			<xsl:call-template name="DUAL_OWNERSHIP_FACTOR">
				<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_LIABILITY" />
			</xsl:call-template>
		</xsl:variable>
		<!-- Remove Sailboat -->
		<xsl:variable name="VAR_REMOVE_SAILBOAT_FACTOR">
			<xsl:call-template name="REMOVE_SAILBOAT_FACTOR">
				<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_LIABILITY" />
			</xsl:call-template>
		</xsl:variable>
		<xsl:choose>
			<!-- No coverage -->
			<xsl:when test="$P_MEDICAL_PAYMENT_AMOUNT ='' or normalize-space($P_MEDICAL_PAYMENT_AMOUNT) ='NOCOVERAGE'">0</xsl:when>
			<!-- Not for trailer -->
			<xsl:when test="BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER">0</xsl:when>
			<!-- Not applicable if 
			1. Attached to Home and Outboard style/Sailboat less than 26 feet  
			2. Personal watercraft-->
			<xsl:when test="(
							(POLICY/ATTACHTOWOLVERINE = 'Y' or POLICY/BOATHOMEDISC = 'Y')
							and ((BOATS/BOAT/BOATTYPECODE != $BOATTYPE_WAVERUNNER and BOATS/BOAT/BOATSTYLECODE =  $BOATSTYLE_OUTBOARD) 
									or (BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_SAILBOAT and BOATS/BOAT/LENGTH &lt;= $P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY) or (BOATS/BOAT/BOATTYPECODE = $BOATTYPE_PONTOON_INBOARD_OUTBOARD))
						) 
								or 
						(
							BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_JETSKI or  BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET
						)">
		0
		</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="LENGTH" select="BOATS/BOAT/LENGTH" />
				<xsl:choose>
					<!-- Not applicable for Mini Jet Boat -->
					<xsl:when test="BOATS/BOAT/BOATSTYLECODE = $BOATTYPE_MINI_JET">
						0			
					</xsl:when>
					<!-- Outboard style and Sailboat over 26 feet -->
					<xsl:when test="(BOATS/BOAT/BOATSTYLECODE =  $BOATSTYLE_OUTBOARD) or 
									(BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_SAILBOAT and BOATS/BOAT/LENGTH &lt; $P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY)">
						<xsl:variable name="P_MED_PAY_RATE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='LIOUTBOARD']/ATTRIBUTES/@MED_PAY_RATE" />
						<xsl:variable name="P_MIN_MED_PAY_LIMIT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='LIOUTBOARD']/ATTRIBUTES/@MEDPAY_MIN" />
						<xsl:variable name="P_MED_PAY_RATE_PER" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='LIOUTBOARD']/ATTRIBUTES/@MED_PAY_RATE_PER" />
						<xsl:choose>
							<xsl:when test="$P_MEDICAL_PAYMENT_AMOUNT &gt;= $P_MIN_MED_PAY_LIMIT">
								<!-- Multiply the difference as the first 1000 is included -->
								<xsl:value-of select="round(round(round(round(round(($P_MEDICAL_PAYMENT_AMOUNT - $P_MED_PAY_RATE_PER) div $P_MED_PAY_RATE_PER )*$P_MED_PAY_RATE)*$VAR_LIABILITY_SURCHARGES)*$VAR_DUAL_OWNERSHIP)*$VAR_REMOVE_SAILBOAT_FACTOR)" />
							</xsl:when>
							<xsl:otherwise>0</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<!-- All others -->
					<xsl:otherwise>
						<xsl:variable name="P_MED_PAY_RATE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='ALLOUTBOARD']/ATTRIBUTES/@MED_PAY_RATE" />
						<xsl:variable name="P_MIN_MED_PAY_LIMIT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='ALLOUTBOARD']/ATTRIBUTES/@MEDPAY_MIN" />
						<xsl:variable name="P_MED_PAY_RATE_PER" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='ALLOUTBOARD']/ATTRIBUTES/@MED_PAY_RATE_PER" />
						<xsl:choose>
							<xsl:when test="$P_MEDICAL_PAYMENT_AMOUNT ='' or $P_MEDICAL_PAYMENT_AMOUNT ='NOCOVERAGE'">0</xsl:when>
							<xsl:when test="$P_MEDICAL_PAYMENT_AMOUNT &gt;= $P_MIN_MED_PAY_LIMIT">
								<xsl:value-of select="round(round(round(round(($P_MEDICAL_PAYMENT_AMOUNT - $P_MED_PAY_RATE_PER)div $P_MED_PAY_RATE_PER)*$P_MED_PAY_RATE)*$VAR_LIABILITY_SURCHARGES)*$VAR_DUAL_OWNERSHIP) " />
							</xsl:when>
							<xsl:otherwise>0</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for MEDICAL PAYMENTS, Display (END)								 -->
	<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN									              -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for Uninsured Boaters Coverage, Display (START)					  -->
	<!--		    			  FOR INDIANA,MICHIGAN and WISCONSIN					              -->
	<!-- ============================================================================================ -->
	<xsl:template name="UNINSURED_BOATERS_COVERAGE">
		<xsl:choose>
			<!--Watercraft INDIANA-->
			<xsl:when test="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<xsl:call-template name="UNINSURED_BOATERS" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Uninsured Boaters coverage-->
	<xsl:template name="UNINSURED_BOATERS">
		<xsl:variable name="P_UNINSURED_BOATERS_LIABILITY" select="BOATS/BOAT/UNINSUREDBOATERS" />
		<xsl:variable name="P_UNINSURED_BOATERS" select="BOATS/BOAT/UNINSUREDBOATERS" />
		<xsl:variable name="P_UNINSURED_BOATERS_LIABILITY_AMOUNT" select="translate(translate($P_UNINSURED_BOATERS_LIABILITY,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')" />
		<xsl:variable name="P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SAILBOAT_LENGTH_FOR_LIABILITY" />
		<xsl:variable name="P_INCLUDED" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='UNINSURED_BOATERS_COV']/ATTRIBUTES[@COVERAGE= $P_UNINSURED_BOATERS]/@INCLUDED" />
		<xsl:choose>
			<xsl:when test="$P_UNINSURED_BOATERS_LIABILITY_AMOUNT = 'NOCOVERAGE' or $P_UNINSURED_BOATERS_LIABILITY_AMOUNT = '' or $P_UNINSURED_BOATERS_LIABILITY_AMOUNT = 0">
			0
		</xsl:when>
			<!-- Not applicable to personal watercraft and trailers -->
			<xsl:when test="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_JETSKI  
						or  BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER">
			0
		</xsl:when>
			<xsl:otherwise>
				<!--		<xsl:variable name="P_INCLUDED" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='UNINSURED_BOATERS_COV']/ATTRIBUTES[@INCLUDED = $P_UNINSURED_BOATERS_LIABILITY]/@INCLUDED"/>-->
				<xsl:choose>
					<xsl:when test="(POLICY/ATTACHTOWOLVERINE !='Y' and POLICY/BOATHOMEDISC !='Y' and $P_INCLUDED ='TRUE' and BOATS/BOAT/BOATSTYLECODE!=$BOATSTYLE_OUTBOARD and BOATS/BOAT/BOATSTYLECODE != $BOATSTYLE_SAILBOAT)">
						0
					</xsl:when>
					<xsl:otherwise>
						<!-- Fetch base rates -->
						<xsl:variable name="P_BASE_RATE">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='UNINSURED_BOATERS_COV']/ATTRIBUTES[@COVERAGE = $P_UNINSURED_BOATERS_LIABILITY_AMOUNT]/@RATE" />
						</xsl:variable>
						<!-- Rate when attached to home -->
						<xsl:variable name="P_ATTACHED_TO_HOME_UNB">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='UNINSURED_BOATERS_COV_HOME']/ATTRIBUTES/@RATE" />
						</xsl:variable>
						<!-- fetch the  rates when boat attached to home add $12 -->
						<xsl:variable name="P_UNINSURED_BOATERS_RATE">
							<xsl:choose>
								<xsl:when test="POLICY/QQNUMBER !=''">
									<xsl:choose>
										<xsl:when test="((POLICY/ATTACHTOWOLVERINE ='Y' or POLICY/BOATHOMEDISC ='Y'))
														and
														((BOATS/BOAT/BOATSTYLECODE =  $BOATSTYLE_OUTBOARD) or 
																(BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_SAILBOAT and BOATS/BOAT/LENGTH &lt; $P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY) or (BOATS/BOAT/BOATTYPECODE = $BOATTYPE_PONTOON_INBOARD_OUTBOARD))">
											<xsl:value-of select="$P_BASE_RATE + $P_ATTACHED_TO_HOME_UNB" />
										</xsl:when>
										<xsl:otherwise><xsl:value-of select="$P_BASE_RATE"></xsl:value-of></xsl:otherwise>
									</xsl:choose>
								</xsl:when>
								<xsl:when test="(normalize-space(BOATS/BOAT/PERSONALLIABILITY)='EFH')
												and
												((BOATS/BOAT/BOATSTYLECODE =  $BOATSTYLE_OUTBOARD) or 
														(BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_SAILBOAT and BOATS/BOAT/LENGTH &lt; $P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY) or (BOATS/BOAT/BOATTYPECODE = $BOATTYPE_PONTOON_INBOARD_OUTBOARD))">
									<xsl:value-of select="$P_BASE_RATE + $P_ATTACHED_TO_HOME_UNB" />
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="$P_BASE_RATE" />
								</xsl:otherwise>
							</xsl:choose>
						</xsl:variable>
						<!-- Apply surcharges on this liability -->
						<!-- Liability Surcharge for Length and Speed -->
						<xsl:variable name="VAR_LIABILITY_SURCHARGES">
							<xsl:call-template name="BOAT_LENGTH_SPEED_SURCHARGE">
								<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_LIABILITY" />
							</xsl:call-template>
						</xsl:variable>
						<!-- Dual Ownership -->
						<xsl:variable name="VAR_DUAL_OWNERSHIP">
							<xsl:call-template name="DUAL_OWNERSHIP_FACTOR">
								<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_LIABILITY" />
							</xsl:call-template>
						</xsl:variable>
						<!-- Remove Sailboat -->
						<xsl:variable name="VAR_REMOVE_SAILBOAT_FACTOR">
							<xsl:call-template name="REMOVE_SAILBOAT_FACTOR">
								<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_LIABILITY" />
							</xsl:call-template>
						</xsl:variable>
						<!-- Send uninsured boaters value -->
						<xsl:value-of select="round(round(round($P_UNINSURED_BOATERS_RATE * $VAR_LIABILITY_SURCHARGES) * $VAR_DUAL_OWNERSHIP) * $VAR_REMOVE_SAILBOAT_FACTOR) " />
						
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="UNINSURED_BOATERS_DISPLAY">
		<xsl:variable name="P_UNINSURED_BOATERS_LIABILITY" select="BOATS/BOAT/PERSONALLIABILITY" />
		<xsl:variable name="P_UNINSURED_BOATERS" select="BOATS/BOAT/UNINSUREDBOATERS" />
		<xsl:variable name="P_UNINSURED_BOATERS_LIABILITY_AMOUNT" select="translate(translate($P_UNINSURED_BOATERS_LIABILITY,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')" />
		<xsl:variable name="P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SAILBOAT_LENGTH_FOR_LIABILITY" />
		<xsl:variable name="P_INCLUDED" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='UNINSURED_BOATERS_COV']/ATTRIBUTES[@COVERAGE= $P_UNINSURED_BOATERS]/@INCLUDED" />
		<xsl:choose>
			<xsl:when test="($P_INCLUDED ='TRUE' and POLICY/ATTACHTOWOLVERINE ='Y' and (BOATS/BOAT/BOATTYPECODE = $BOATTYPE_INBOARD_OUTBOARD or BOATS/BOAT/BOATTYPECODE = $BOATTYPE_INBOARD))">
			Included
			</xsl:when>
			<xsl:when test="($P_UNINSURED_BOATERS_LIABILITY_AMOUNT = 'NOCOVERAGE' or $P_UNINSURED_BOATERS_LIABILITY_AMOUNT = '') and (BOATS/BOAT/BOATSTYLECODE != 'O' or BOATS/BOAT/BOATSTYLECODE != 'S')">
			0
		</xsl:when>
			<!-- Not applicable to personal watercraft and trailers -->
			<xsl:when test="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_JETSKI  
						or  BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER ">
			0
		</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<!-- outboard and sailboat less than 26 ft. will always be charged so send 0 for displaying 'Included' -->
					<xsl:when test="(POLICY/ATTACHTOWOLVERINE ='Y' or POLICY/BOATHOMEDISC = 'Y')
												and
												((BOATS/BOAT/BOATSTYLECODE =  $BOATSTYLE_OUTBOARD) or 
														(BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_SAILBOAT and BOATS/BOAT/LENGTH &lt; $P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY))">		
					0	
				</xsl:when>
					<!-- When the boat is standalone( for all boat types other than Mini Jet Boat, Wave runner or Jet Ski ), then Uninsured boaters is always 'Included' without any charge (for 10K or 50K) -->
					<xsl:when test="POLICY/QQNUMBER !=''">
							<xsl:choose>
								<xsl:when test="(POLICY/ATTACHTOWOLVERINE ='Y' or POLICY/BOATHOMEDISC ='Y')and $P_INCLUDED ='TRUE' and (BOATS/BOAT/BOATSTYLECODE !=  $BOATSTYLE_OUTBOARD and BOATS/BOAT/BOATSTYLECODE != $BOATSTYLE_SAILBOAT)">		
											Included			
											</xsl:when>
											<xsl:when test="(POLICY/ATTACHTOWOLVERINE ='N' and POLICY/BOATHOMEDISC ='N')and $P_INCLUDED ='TRUE' and (BOATS/BOAT/BOATSTYLECODE =  $BOATSTYLE_OUTBOARD or BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_SAILBOAT)">		
											Included			
											</xsl:when>
								<xsl:when test="(POLICY/ATTACHTOWOLVERINE !='Y' or POLICY/BOATHOMEDISC !='Y')and $P_INCLUDED ='TRUE' and ((BOATS/BOAT/BOATSTYLECODE !=  $BOATSTYLE_OUTBOARD and BOATS/BOAT/BOATSTYLECODE != $BOATSTYLE_SAILBOAT))">		
											Included			
											</xsl:when>
								<xsl:otherwise>0</xsl:otherwise>
							</xsl:choose>
					</xsl:when>
					<xsl:when test="(POLICY/ATTACHTOWOLVERINE !='Y' )and $P_INCLUDED ='TRUE' and (BOATS/BOAT/BOATSTYLECODE !=  $BOATSTYLE_OUTBOARD and BOATS/BOAT/BOATSTYLECODE != $BOATSTYLE_SAILBOAT and normalize-space(BOATS/BOAT/PERSONALLIABILITY)!='EFH')">		
								Included			
					</xsl:when>
					<xsl:when test="(POLICY/ATTACHTOWOLVERINE !='Y' )and $P_INCLUDED ='TRUE' and ((BOATS/BOAT/BOATSTYLECODE =  $BOATSTYLE_OUTBOARD or BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_SAILBOAT) and normalize-space(BOATS/BOAT/PERSONALLIABILITY)!='EFH')">		
											Included			
					</xsl:when>
					<xsl:otherwise>
					0
				</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for Uninsured Boaters Coverage, Display (END)						  -->
	<!--		    			  FOR INDIANA,MICHIGAN and WISCONSIN					              -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for WATERCRAFT LIABILITY/PD SURCHARGES,(START)					  -->
	<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN								              -->
	<!-- ============================================================================================ -->
	<xsl:template name="CONSTRUCTION_TYPE_SURCHARGE">
		<xsl:variable name="VAR_WOOD_BOAT_AGE">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='WOODENBOAT']/ATTRIBUTES/@BOAT_AGE" />
		</xsl:variable>
		<xsl:variable name="VAR_FIBER_BOAT_AGE">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='FIBERGLASSBOAT']/ATTRIBUTES/@BOAT_AGE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/CONSTRUCTIONCODE = $CONSTRUCTIONCODE_WOOD and BOATS/BOAT/AGE &gt; $VAR_WOOD_BOAT_AGE">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='WOODENBOAT']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:when test="BOATS/BOAT/CONSTRUCTIONCODE = $CONSTRUCTIONCODE_FIBREGLASS and BOATS/BOAT/AGE &gt; $VAR_FIBER_BOAT_AGE">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='FIBERGLASSBOAT']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>
		1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<xsl:template name="BOAT_LENGTH_SPEED_SURCHARGE_PREMIUM">
		<xsl:variable name="P_MAX_LENGTH" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_LENGTH_OF_BOAT" />
		<xsl:variable name="P_MAX_SPEED" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SPEED_OF_BOAT" />
		<xsl:choose>
			<xsl:when test="(BOATS/BOAT/LENGTH &gt; $P_MAX_LENGTH and BOATS/BOAT/CAPABLESPEED &gt; $P_MAX_SPEED) and ((BOATS/BOAT/BOATTYPECODE!=$BOATTYPE_MINI_JET) and (BOATS/BOAT/BOATTYPECODE!=$BOATTYPE_WAVERUNNER) and (BOATS/BOAT/BOATSTYLECODE!=$BOATSTYLE_JETSKI))">
				Included
			</xsl:when>
			<xsl:otherwise>
				0
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="BOAT_LENGTH_SPEED_SURCHARGE">
		<xsl:param name="FACTORELEMENT" /> <!-- Depending on the PD or Liability sections from which this template is called -->
		<xsl:variable name="P_MAX_LENGTH" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_LENGTH_OF_BOAT" />
		<xsl:variable name="P_MAX_SPEED" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SPEED_OF_BOAT" />
		<xsl:choose>
			<xsl:when test="(BOATS/BOAT/LENGTH &gt; $P_MAX_LENGTH and BOATS/BOAT/CAPABLESPEED &gt; $P_MAX_SPEED) and ((BOATS/BOAT/BOATTYPECODE!=$BOATTYPE_MINI_JET) and (BOATS/BOAT/BOATTYPECODE!=$BOATTYPE_WAVERUNNER) and (BOATS/BOAT/BOATSTYLECODE!=$BOATSTYLE_JETSKI))">
				<xsl:choose>
					<xsl:when test="$FACTORELEMENT = $CALLED_FROM_LIABILITY">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LISURCHARGE']/NODE[@ID ='LIBOATSPEED']/ATTRIBUTES/@FACTOR" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = $CALLED_FROM_PD">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDBOATSPEED']/ATTRIBUTES/@FACTOR" />
					</xsl:when>
					<xsl:otherwise>
						1.00
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
				1.00
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="BOAT_LENGTH_SPEED_SURCHARGE_DISPLAY">
		<xsl:param name="FACTORELEMENT" /> <!-- Depending on the PD or Liability sections from which this template is called -->
		<xsl:variable name="P_MAX_LENGTH" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_LENGTH_OF_BOAT" />
		<xsl:variable name="P_MAX_SPEED" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SPEED_OF_BOAT" />
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/LENGTH &gt; $P_MAX_LENGTH and BOATS/BOAT/CAPABLESPEED &gt; $P_MAX_SPEED">
				<xsl:choose>
					<xsl:when test="$FACTORELEMENT = $CALLED_FROM_LIABILITY">
							Charge - Speed over <xsl:value-of select="($P_MAX_SPEED)" /> MPH +<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LISURCHARGE']/NODE[@ID ='LIBOATSPEED']/ATTRIBUTES/@SURCHARGE" /><xsl:text>%</xsl:text> 
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = $CALLED_FROM_PD">
							Charge - Speed over <xsl:value-of select="($P_MAX_SPEED)" /> MPH +<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDBOATSPEED']/ATTRIBUTES/@SURCHARGE" /><xsl:text>%</xsl:text> 
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<xsl:template name="FIBRE_GLASS_DISPLAY">
		<xsl:variable name="VAR_FIBER_BOAT_AGE">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='FIBERGLASSBOAT']/ATTRIBUTES/@BOAT_AGE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/CONSTRUCTIONCODE = $CONSTRUCTIONCODE_FIBREGLASS and BOATS/BOAT/AGE &gt; $VAR_FIBER_BOAT_AGE">
			Charge - FiberGlass Boat + <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='FIBERGLASSBOAT']/ATTRIBUTES/@SURCHARGE" /><xsl:text>%</xsl:text>			
		</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="FIBRE_GLASS_PREMIUM">
		<xsl:variable name="VAR_FIBER_BOAT_AGE">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='FIBERGLASSBOAT']/ATTRIBUTES/@BOAT_AGE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/CONSTRUCTIONCODE = $CONSTRUCTIONCODE_FIBREGLASS and BOATS/BOAT/AGE &gt; $VAR_FIBER_BOAT_AGE">
				<xsl:choose>
					<!-- Not applicable to personal watercraft and trailers -->
					<xsl:when test="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_JETSKI  
											or  BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER "> 	
								0
 					</xsl:when>
					<xsl:otherwise>Included</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="FIBRE_GLASS_FACTOR">
		<xsl:variable name="VAR_FIBER_BOAT_AGE">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='FIBERGLASSBOAT']/ATTRIBUTES/@BOAT_AGE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/CONSTRUCTIONCODE = $CONSTRUCTIONCODE_FIBREGLASS and BOATS/BOAT/AGE &gt; $VAR_FIBER_BOAT_AGE">
				<xsl:choose>
					<!-- Not applicable to personal watercraft and trailers -->
					<xsl:when test="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_JETSKI  
											or  BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER "> 	
								1.00
 							</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='FIBERGLASSBOAT']/ATTRIBUTES/@FACTOR" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
					1.00
					</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--					Templates for WOODEN BOAT, Display (START)								  -->
	<!--		    					 FOR INDIANA,MICHIGAN and WISCONSIN	
			Surcharge -Wooden boats over 10 years old. 								              -->
	<!-- ============================================================================================ -->
	<xsl:template name="WOODEN_BOAT_DISPLAY">
		<xsl:variable name="VAR_WOOD_BOAT_AGE">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='WOODENBOAT']/ATTRIBUTES/@BOAT_AGE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/CONSTRUCTIONCODE = $CONSTRUCTIONCODE_WOOD and BOATS/BOAT/AGE &gt; $VAR_WOOD_BOAT_AGE">
		Charge - Wooden Boat +<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='WOODENBOAT']/ATTRIBUTES/@SURCHARGE" /><xsl:text>%</xsl:text></xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="WOODEN_BOAT_PREMIUM">
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<xsl:variable name="VAR_WOOD_BOAT_AGE">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='WOODENBOAT']/ATTRIBUTES/@BOAT_AGE" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="BOATS/BOAT/CONSTRUCTIONCODE = $CONSTRUCTIONCODE_WOOD and BOATS/BOAT/AGE &gt; $VAR_WOOD_BOAT_AGE">
						<xsl:choose>
							<!-- Not applicable to personal watercraft and trailers -->
							<xsl:when test="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_JETSKI  
									or  BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER "> 					
						0
 						</xsl:when>
							<xsl:otherwise>
 		   				Included
 						</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="WOODEN_BOAT_FACTOR">
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<xsl:variable name="VAR_WOOD_BOAT_AGE">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='WOODENBOAT']/ATTRIBUTES/@BOAT_AGE" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="BOATS/BOAT/CONSTRUCTIONCODE = $CONSTRUCTIONCODE_WOOD and BOATS/BOAT/AGE &gt; $VAR_WOOD_BOAT_AGE">
						<xsl:choose>
							<!-- Not applicable to personal watercraft and trailers -->
							<xsl:when test="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_JETSKI  
									or  BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER "> 					
						1.00
 						</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='WOODENBOAT']/ATTRIBUTES/@FACTOR" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<xsl:template name="REMOVE_SAILBOAT_DISPLAY">
		<xsl:param name="FACTORELEMENT" /> <!-- Depending on the PD or Liability sections from which this template is called -->
		<xsl:variable name="P_BOAT_LENGTH_FOR_REMOVALSAILBOAT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDREMOVALSAILBOAT']/ATTRIBUTES/@BOAT_LENGTH_UNDER" />
		<xsl:choose>
			<xsl:when test="(BOATS/BOAT/REMOVESAILBOAT ='Y' and BOATS/BOAT/LENGTH &lt; $P_BOAT_LENGTH_FOR_REMOVALSAILBOAT) ">Charge - Sailboat Racing Exclusion Waiver 
				<xsl:choose>
					<xsl:when test="$FACTORELEMENT = $CALLED_FROM_LIABILITY">
						+<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LISURCHARGE']/NODE[@ID ='LIREMOVALSAILBOAT']/ATTRIBUTES/@SURCHARGE" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = $CALLED_FROM_PD">
						+<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDREMOVALSAILBOAT']/ATTRIBUTES/@SURCHARGE" />
					</xsl:when>
					<xsl:otherwise>
						0
					</xsl:otherwise>
				</xsl:choose>%
		</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="REMOVE_SAILBOAT_PREMIUM">
		<xsl:param name="FACTORELEMENT" /> <!-- Depending on the PD or Liability sections from which this template is called -->
		<xsl:variable name="P_BOAT_LENGTH_FOR_REMOVALSAILBOAT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDREMOVALSAILBOAT']/ATTRIBUTES/@BOAT_LENGTH_UNDER" />
		<xsl:choose>
			<xsl:when test="(BOATS/BOAT/REMOVESAILBOAT ='Y' and BOATS/BOAT/LENGTH &lt; $P_BOAT_LENGTH_FOR_REMOVALSAILBOAT)">
			Included
		</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="REMOVE_SAILBOAT_FACTOR">
		<xsl:param name="FACTORELEMENT" /> <!-- Depending on the PD or Liability sections from which this template is called -->
		<xsl:variable name="P_BOAT_LENGTH_FOR_REMOVALSAILBOAT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDREMOVALSAILBOAT']/ATTRIBUTES/@BOAT_LENGTH_UNDER" />
		<xsl:choose>
			<xsl:when test="(BOATS/BOAT/REMOVESAILBOAT ='Y' and BOATS/BOAT/LENGTH &lt; $P_BOAT_LENGTH_FOR_REMOVALSAILBOAT)">
				<xsl:choose>
					<xsl:when test="$FACTORELEMENT = $CALLED_FROM_LIABILITY">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LISURCHARGE']/NODE[@ID ='LIREMOVALSAILBOAT']/ATTRIBUTES/@FACTOR" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = $CALLED_FROM_PD">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDREMOVALSAILBOAT']/ATTRIBUTES/@FACTOR" />
					</xsl:when>
					<xsl:otherwise>
						1.00
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<xsl:template name="DUAL_OWNERSHIP_PREMIUM">
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<xsl:choose>
					<xsl:when test="BOATS/BOAT/DUALOWNERSHIP = 'Y'">Included</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DUAL_OWNERSHIP_FACTOR">
		<xsl:param name="FACTORELEMENT" /> <!-- Depending on the PD or Liability sections from which this template is called -->
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/DUALOWNERSHIP = 'Y'">
				<xsl:choose>
					<xsl:when test="$FACTORELEMENT = $CALLED_FROM_LIABILITY">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LISURCHARGE']/NODE[@ID ='LIDUALOWNERSHIP']/ATTRIBUTES/@FACTOR" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = $CALLED_FROM_PD">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDDUALOWNERSHIP']/ATTRIBUTES/@FACTOR" />
					</xsl:when>
					<xsl:otherwise>
					1.00
				</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for WATERCRAFT LIABILITY SURCHARGES,(END)							  -->
	<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN								              -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--					Templates for Credits , Display (START)									  -->
	<!--		    					  FOR INDIANA, MICHIGAN, WISCONSIN									              -->
	<!-- ============================================================================================ -->
	<xsl:template name="DIESEL_ENGINE_DISPLAY">
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/DIESELENGINE = 'Y'">
				<xsl:choose>
					<xsl:when test="BOATS/BOAT/BOATTYPECODE != $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATTYPECODE != $BOATTYPE_MINI_JET or BOATS/BOAT/BOATSTYLECODE != $BOATSTYLE_JETSKI"><xsl:text>Discount - Diesel Engine -</xsl:text><xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='DIESELENGINECRAFT']/ATTRIBUTES/@CREDIT" /><xsl:text>%</xsl:text></xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DIESEL_ENGINE_PREMIUM">
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/DIESELENGINE = 'Y'">
				<xsl:choose>
					<xsl:when test="BOATS/BOAT/BOATTYPECODE != $BOATTYPE_WAVERUNNER and BOATS/BOAT/BOATTYPECODE != $BOATTYPE_MINI_JET and BOATS/BOAT/BOATSTYLECODE != $BOATSTYLE_JETSKI">Included</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DIESEL_ENGINE_FACTOR">
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/DIESELENGINE = 'Y'">
				<xsl:choose>
					<xsl:when test="BOATS/BOAT/BOATTYPECODE != $BOATTYPE_WAVERUNNER and BOATS/BOAT/BOATTYPECODE != $BOATTYPE_MINI_JET and BOATS/BOAT/BOATSTYLECODE != $BOATSTYLE_JETSKI">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='DIESELENGINECRAFT']/ATTRIBUTES/@FACTOR" />
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Templates for Discounts , Display (END)							      -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN												  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--					Templates for Halon Extinguisher, Display (START)			      		  -->
	<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN									              -->
	<!-- ============================================================================================ -->
	<xsl:template name="HALON_FIRE_DISPLAY">
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/HALONFIRE = 'Y' and (BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_INBOARD_OUTBOARD or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_INBOARD or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_SAILBOAT)"><xsl:text>Discount - Halon Fire Extinguishing System -</xsl:text><xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='FIREEXTINGUISHER']/ATTRIBUTES/@CREDIT" /><xsl:text>%</xsl:text></xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="HALON_FIRE_PREMIUM">
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<xsl:choose>
					<xsl:when test="BOATS/BOAT/HALONFIRE = 'Y' and (BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_INBOARD_OUTBOARD or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_INBOARD or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_SAILBOAT)">Included</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="HALON_FIRE_FACTOR">
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/HALONFIRE = 'Y' and (BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_INBOARD_OUTBOARD or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_INBOARD or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_SAILBOAT)">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='FIREEXTINGUISHER']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Templates for Halon Extinguisher, Display (END)							  -->
	<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN												  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--					Templates for Loran GPS or other Navigation System, Display (START)		  -->
	<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN									              -->
	<!-- ============================================================================================ -->
	<xsl:template name="LORAN_GPS_DISPLAY">
		<xsl:choose>
			<!--xsl:when test="BOATS/BOAT/LORANNAVIGATIONSYSTEM = 'Y'">Discount - Loran Navigation system, GPS or Depth Sounder and Ship to shore Radio -<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='NAVIGATIONSYSTEM']/ATTRIBUTES/@CREDIT" />%</xsl:when-->
			<xsl:when test="BOATS/BOAT/LORANNAVIGATIONSYSTEM = 'Y'"><xsl:text>Discount - Navigation System -</xsl:text><xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='NAVIGATIONSYSTEM']/ATTRIBUTES/@CREDIT" /><xsl:text>%</xsl:text></xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="LORAN_GPS_PREMIUM">
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<xsl:choose>
					<xsl:when test="BOATS/BOAT/LORANNAVIGATIONSYSTEM = 'Y'">Included</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="LORAN_GPS_FACTOR">
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/LORANNAVIGATIONSYSTEM = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='NAVIGATIONSYSTEM']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Templates for Loran GPS or other Navigation System, Display (END)		  -->
	<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN												  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--					Templates for Shore Station, Display (START)							  -->
	<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN			              -->
	<!-- ============================================================================================ -->
	<xsl:template name="SHORE_STATION_DISPLAY">
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/SHORESTATION = 'Y'"><xsl:text>Discount - Shore Station -</xsl:text><xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='SHORESTATIONCREDIT']/ATTRIBUTES/@CREDIT" /><xsl:text>%</xsl:text></xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="SHORE_STATION_PREMIUM">
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<xsl:choose>
					<xsl:when test="BOATS/BOAT/SHORESTATION = 'Y'">Included</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="SHORE_STATION_FACTOR">
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/SHORESTATION = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='SHORESTATIONCREDIT']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Templates for Shore Station, Display (END)								  -->
	<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN							  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--					Templates for Multi-Boat, Display (START)								  -->
	<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN			              -->
	<!-- ============================================================================================ -->
	<xsl:template name="MULTI_BOAT_DISPLAY">
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/MULTIBOATCREDIT = 'Y'"><xsl:text>Discount - Multi-Boat -</xsl:text><xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='MULTIBOAT']/ATTRIBUTES/@CREDIT" /><xsl:text>%</xsl:text></xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MULTI_BOAT_PREMIUM">
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<xsl:choose>
					<xsl:when test="BOATS/BOAT/MULTIBOATCREDIT = 'Y'">
						<!--Check condition for trailer types-->
						<xsl:choose>
							<xsl:when test="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_JETSKI  
									or  BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER ">
 							0
 						</xsl:when>
							<xsl:otherwise>
 		   				Included
 						</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MULTI_BOAT_FACTOR">
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/MULTIBOATCREDIT = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='MULTIBOAT']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Templates for  Multi-Boat, Display (END)								  -->
	<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN						  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for Boat Replacement Cost Coverage, Display (START)				  -->
	<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN									              -->
	<!-- ============================================================================================ -->
	<xsl:template name="BOAT_REPLACEMENT_DISPLAY">
		<xsl:variable name="P_AGE_LIMIT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BOAT_REPLACEMENT_COST']/NODE[@ID ='BOAT_REPLACEMENT']/ATTRIBUTES/@AGE_OF_WATERCRAFT" />
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<xsl:choose>
					<xsl:when test="BOATS/BOAT/AGE &lt;= $P_AGE_LIMIT and BOATS/BOAT/AV100 != 'Y'">
						<xsl:choose>
							<xsl:when test="BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER ">
 							0
 						</xsl:when>
							<xsl:otherwise>
 		   					Included
 						</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for Boat Replacement Cost Coverage, Display (END)					  -->
	<!--		    					  FOR INDIANA	, MICHIGAN, WISCONSIN								              -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for Boat-Home(Multipolicy discount), Display (START)				  -->
	<!--		    					  FOR INDIANA	, MICHIGAN, WISCONSIN								              -->
	<!-- ============================================================================================ -->
	<xsl:template name="DISCOUNT_BOAT_HOME">
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<xsl:choose>
					<xsl:when test="POLICY/BOATHOMEDISC = 'Y'">
						<xsl:choose>
							<!-- Not applicable for personal watercraft adn trailers -->
							<xsl:when test="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_JETSKI or  BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or
												 BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER ">
									0
								</xsl:when>
							<xsl:otherwise>Included</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DISCOUNT_BOAT_HOME_FACTOR">
		<xsl:choose>
			<xsl:when test="POLICY/BOATHOMEDISC='Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID='MULTIPOLICY']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for Boat-Home(Multipolicy discount), Display (END)				  -->
	<!--		    					  FOR INDIANA	, MICHIGAN, WISCONSIN								              -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--					Templates for 5 Years Experience or Navigation Course , Display (START)	  -->
	<!--		    					  FOR INDIANA									              
		Experience: 5 or More Years Experience and /or Owners completed navigation
		course in either U.S. Coast Guard Auxiliary or Power Squadron.							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="EXPERIENCE_CREDIT_DISPLAY">
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/POWERSQUADRONCOURSE = 'Y' or BOATS/BOAT/COASTGUARDAUXILARYCOURSE = 'Y' or BOATS/BOAT/HAS_5_YEARSOPERATOREXPERIENCE = 'Y'"><xsl:text>Discount - 5 Years Experience or Navigation Course -</xsl:text><xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='EXPNAVIGATIONCOURSE']/ATTRIBUTES/@CREDIT" /><xsl:text>% </xsl:text></xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="EXPERIENCE_CREDIT_PREMIUM">
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<!-- Not applicable to personal watercraft and trailers -->
				<xsl:choose>
					<xsl:when test="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_JETSKI or  BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER ">
					0
				</xsl:when>
					<xsl:otherwise>
						<xsl:variable name="VAR_EXPERIENCE_CREDIT__DISPLAY">
							<xsl:call-template name="EXPERIENCE_CREDIT_DISPLAY" />
						</xsl:variable>
						<xsl:choose>
							<xsl:when test="$VAR_EXPERIENCE_CREDIT__DISPLAY = 0">
									0
								</xsl:when>
							<xsl:otherwise>Included</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="EXPERIENCE_CREDIT_FACTOR">
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/POWERSQUADRONCOURSE = 'Y' or BOATS/BOAT/COASTGUARDAUXILARYCOURSE = 'Y' or BOATS/BOAT/HAS_5_YEARSOPERATOREXPERIENCE = 'Y'">
				<!-- Not applicable to personal watercraft and trailers -->
				<xsl:choose>
					<xsl:when test="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_JETSKI or  BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER ">
						1.00
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='EXPNAVIGATIONCOURSE']/ATTRIBUTES/@FACTOR" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Templates for 5 Years Experience or Navigation Course , Display (END)	  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN												  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--					Minijet Charge	  -->
	<!--		    			FOR INDIANA,MICHIGAN,WISCONSIN											-->
	<!-- ============================================================================================ -->
	<xsl:template name="MINIJET_DISPLAY">
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET">Charge - Mini Jet Boat +<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='MINIJETBOAT']/ATTRIBUTES/@CREDIT" /><xsl:text>%</xsl:text></xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MINIJET_PREMIUM">
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<xsl:choose>
					<xsl:when test="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET">Included</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Templates for Unattached Equip. & Personal Effects Display (START)		  -->
	<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN												  -->
	<!-- ============================================================================================ -->
	<!-- This is charged on the amount above the unattached equipment value -->
	<xsl:template name="UNATTACHED_PREMIUM">
		<xsl:variable name="P_UNATTACHEDEQUIPMENT">
			<xsl:value-of select="BOATS/BOAT/UNATTACHEDEQUIPMENT" />
		</xsl:variable>
		<xsl:variable name="P_UNATTACHEDEQUIPMENT_VALUE" select="translate($P_UNATTACHEDEQUIPMENT,'$','')" />
		<xsl:variable name="VAR_MINIMUM">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID ='UNATTACHEDEQUIPMENT']/ATTRIBUTES/@MINIMUM" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$P_UNATTACHEDEQUIPMENT_VALUE =$VAR_MINIMUM">Included</xsl:when>
			<xsl:otherwise>			
			0		
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="UNATTACHED_PREMIUM_VALUE">
		<xsl:variable name="P_UNATTACHEDEQUIPMENT">
			<xsl:value-of select="BOATS/BOAT/UNATTACHEDEQUIPMENT" />
		</xsl:variable>
		<xsl:variable name="P_UNATTACHEDEQUIPMENT_VALUE" select="translate($P_UNATTACHEDEQUIPMENT,'$','')" />
		<xsl:variable name="VAR_MINIMUM">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID ='UNATTACHEDEQUIPMENT']/ATTRIBUTES/@MINIMUM" />
		</xsl:variable>
		<xsl:variable name="VAR_RATE_PER_VALUE">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID ='UNATTACHEDEQUIPMENT']/ATTRIBUTES/@RATE_PER_VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR_RATE">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID ='UNATTACHEDEQUIPMENT']/ATTRIBUTES/@RATE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$P_UNATTACHEDEQUIPMENT_VALUE = $VAR_MINIMUM">0</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="round(round(round($P_UNATTACHEDEQUIPMENT_VALUE - $VAR_MINIMUM) div $VAR_RATE_PER_VALUE) * $VAR_RATE)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Templates for Unattached Equip. & Personal Effects Display (END)		  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN												  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--					Templates for Deductible												  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="DEDUCTIBLE_DISPLAY">
		<!-- Get the deductible percentage -->
		<xsl:variable name="DED_DESC" select="BOATS/BOAT/DEDUCTIBLE" />
		<!-- Market Value -->
		<xsl:variable name="MARKETVALUE" select="BOATS/BOAT/MARKETVALUE" />
		<xsl:choose>
			<xsl:when test="$DED_DESC = '' or $DED_DESC ='0'">0</xsl:when>
			<xsl:when test="contains($DED_DESC ,'-')"> <!--  implies it is a percentage deductible -->
				<!-- Get the minimum deductible -->
				<xsl:variable name="MIN_DEDUCTIBLE">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID='DEDUCTIBLE']/ATTRIBUTES[@DESC=$DED_DESC]/@MINIMUMVALUE" />
				</xsl:variable>
				<!-- Get the percentage -->
				<xsl:variable name="DED_PERCENTAGE">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID='DEDUCTIBLE']/ATTRIBUTES[@DESC=$DED_DESC]/@MINIMUMPERCENTAGE" />
				</xsl:variable>
				<!-- compute the deductible -->
				<xsl:variable name="COMPUTED_DEDUCTIBLE">
					<xsl:value-of select="round(round($MARKETVALUE * $DED_PERCENTAGE) div 100.00) " />
				</xsl:variable>
				<!-- Check if the computed value is less than Minimum Deductible. If less then send minimum deductible. -->
				<xsl:choose>
					<xsl:when test="$COMPUTED_DEDUCTIBLE &lt; $MIN_DEDUCTIBLE">
						<xsl:value-of select="$DED_PERCENTAGE" /><xsl:text>%-</xsl:text><xsl:value-of select="$MIN_DEDUCTIBLE" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$DED_PERCENTAGE" /><xsl:text>%-</xsl:text><xsl:value-of select="round($COMPUTED_DEDUCTIBLE)" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$DED_DESC" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DEDUCTIBLE_FACTOR">
		<xsl:variable name="DED_DESC" select="BOATS/BOAT/DEDUCTIBLE" />
		<xsl:choose>
			<xsl:when test="$DED_DESC = '' or $DED_DESC ='0'">1.00</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID='DEDUCTIBLE']/ATTRIBUTES[@DESC=$DED_DESC]/@FACTOR" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Templates for Deductible (END)											  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN							  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--					Templates for AGE (start)												  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="AGE_DISCOUNT_DISPLAY">
		<xsl:variable name="P_DATE" select="POLICY/QUOTEEFFDATE" />
		<xsl:variable name="P_YEAR" select="substring($P_DATE,7,4)" />
		<xsl:variable name="P_MONTH" select="substring($P_DATE,1,2)" />
		<!--xsl:choose>
			<xsl:when test="$QUOTE_EFFECTIVE_DATE_MONTH &lt; $P_MONTH and $QUOTE_EFFECTIVE_DATE_YEAR &lt;= $P_YEAR"-->
		<xsl:variable name="P_AGE" select="BOATS/BOAT/AGE" />
		<!-- FOR MAXIMUM AGE -->
		<xsl:variable name="VAR_MAX_AGE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='AGEWATER']/ATTRIBUTES[@MAX = 'MAX_AGE']/@MINAGE" />
		<xsl:choose>
			<xsl:when test="$P_AGE = ''">
				0
			</xsl:when>
			<xsl:when test="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_JETSKI or  BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER ">
				0
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="P_AGE_CREDIT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='AGEWATER']/ATTRIBUTES[@MINAGE &lt;= $P_AGE and @MAXAGE &gt;= $P_AGE]/@CREDIT" />
				<xsl:choose>
					<xsl:when test="$P_AGE_CREDIT &gt; 0">
						Included
					</xsl:when>
					<xsl:otherwise>						
						0
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
		<!--/xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose-->
	</xsl:template>
	<xsl:template name="AGE_FACTOR">
		<xsl:variable name="P_DATE" select="POLICY/QUOTEEFFDATE" />
		<xsl:variable name="P_YEAR" select="substring($P_DATE,7,4)" />
		<xsl:variable name="P_MONTH" select="substring($P_DATE,1,2)" />
		<!--xsl:choose>
			<xsl:when test="$QUOTE_EFFECTIVE_DATE_MONTH &lt; $P_MONTH and $QUOTE_EFFECTIVE_DATE_YEAR &lt;= $P_YEAR"-->
		<xsl:variable name="P_AGE" select="BOATS/BOAT/AGE" />
		<!-- FOR MAXIMUM AGE -->
		<xsl:variable name="VAR_MAX_AGE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='AGEWATER']/ATTRIBUTES[@MAX = 'MAX_AGE']/@MINAGE" />
		<xsl:choose>
			<xsl:when test="$P_AGE = ''">
				1.00  
			</xsl:when>
			<xsl:when test="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_JETSKI or  BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER ">
				1.00
			</xsl:when>
			<xsl:when test="$P_AGE &gt;= $VAR_MAX_AGE">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='AGEWATER']/ATTRIBUTES[@MINAGE = $VAR_MAX_AGE]/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='AGEWATER']/ATTRIBUTES[@MINAGE &lt;= $P_AGE and @MAXAGE &gt;= $P_AGE]/@FACTOR" />
			</xsl:otherwise>
		</xsl:choose>
		<!--/xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose-->
	</xsl:template>
	<xsl:template name="AGE_CREDIT">
		<xsl:variable name="P_DATE" select="POLICY/QUOTEEFFDATE" />
		<xsl:variable name="P_YEAR" select="substring($P_DATE,7,4)" />
		<xsl:variable name="P_MONTH" select="substring($P_DATE,1,2)" />
		<!--xsl:choose>
			<xsl:when test="$QUOTE_EFFECTIVE_DATE_MONTH &lt; $P_MONTH and $QUOTE_EFFECTIVE_DATE_YEAR &lt;= $P_YEAR"-->
		<xsl:variable name="P_AGE" select="BOATS/BOAT/AGE" />
		<!-- FOR MAXIMUM AGE -->
		<xsl:variable name="VAR_MAX_AGE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='AGEWATER']/ATTRIBUTES[@MAX = 'MAX_AGE']/@MINAGE" />
		<xsl:variable name="VAR_CREDIT">
			<xsl:choose>
				<xsl:when test="$P_AGE = ''">	
				0			
				</xsl:when>
				<xsl:when test="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_JETSKI or  BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER ">
				0
				</xsl:when>
				<xsl:when test="$P_AGE &gt;= $VAR_MAX_AGE">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='AGEWATER']/ATTRIBUTES[@MINAGE = $VAR_MAX_AGE]/@CREDIT" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='AGEWATER']/ATTRIBUTES[@MINAGE &lt;= $P_AGE and @MAXAGE &gt;= $P_AGE]/@CREDIT" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$VAR_CREDIT &gt; 0">
				<xsl:value-of select="$VAR_CREDIT" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
		<!--/xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose-->
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Templates for AGE (END)											  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN							  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--					Templates for MINIMUM PREMIUM (START)									  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="MINIMUM_PREMIUM">
		<!-- This is not applicable for trailer types -->
		<!--xsl:choose>
			<xsl:when test="BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER">
				0			
			</xsl:when>
			<xsl:otherwise-->
				<!-- Fetch the difference between  the reqd minimum premium and the premium calculated -->
				<!--xsl:variable name="DIFFERENCE_AMOUNT">
					<xsl:call-template name="PREMIUM_DIFFERENCE" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$DIFFERENCE_AMOUNT &gt; 0">
						<xsl:value-of select="$DIFFERENCE_AMOUNT" />
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose-->
		0
	</xsl:template>
	<xsl:template name="PREMIUM_DIFFERENCE">
		<!-- Fetch the minimum premium required. -->
		<xsl:variable name="MINIMUM_FINAL_PREMIUM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINIMUM_BOAT_PREMIUM']/NODE[@ID ='MINIMUM_PREMIUM']/ATTRIBUTES/@MINIMUM_VALUE" />
		<!-- Check the final premium value -->
		<xsl:variable name="P_TEMP_FINAL_PREMIUM">
			<!-- Physical Damage -->
			<xsl:variable name="VAR1">
				<xsl:call-template name="PHYSICAL_DAMAGE" />
			</xsl:variable>
			<!-- Watercraft Liability -->
			<xsl:variable name="VAR2">
				<xsl:call-template name="WATERCRAFT_LIABILITY" />
			</xsl:variable>
			<!-- Medical Payments -->
			<xsl:variable name="VAR3">
				<xsl:call-template name="MEDICAL_PAYMENTS" />
			</xsl:variable>
			<!-- Uninsured Boaters Coverage -->
			<xsl:variable name="VAR4">
				<xsl:call-template name="UNINSURED_BOATERS_COVERAGE" />
			</xsl:variable>
			<!-- Optional Endorsements -->
			<xsl:variable name="VAR5">
				<xsl:call-template name="OPTIONAL_ENDORSEMENTS" />
			</xsl:variable>
			<!-- Unattached Premium Value -->
			<xsl:variable name="VAR6">
				<xsl:call-template name="UNATTACHED_PREMIUM_VALUE" />
			</xsl:variable>
			<xsl:value-of select="$VAR1+$VAR2+$VAR3+$VAR4+$VAR5+$VAR6" />
		</xsl:variable>
		<xsl:value-of select="$MINIMUM_FINAL_PREMIUM - $P_TEMP_FINAL_PREMIUM" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Templates for MINIMUM PREMIUM (END)											  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN							  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--					General Templates (END)											  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN							  -->
	<!-- ============================================================================================ -->
	<!-- template for converting market value in ciel thousands -->
	<xsl:template name="MARKETVALUE_IN_THOUSANDS">
		<xsl:param name="FACTOR_MARKETVAL" />
		<xsl:variable name="VAR1" select="$FACTOR_MARKETVAL" />
		<xsl:variable name="VAR2">
			<xsl:value-of select="$VAR1 mod 1000" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$VAR2 &gt; 0">
				<xsl:value-of select="$VAR1 + 1000 - $VAR2" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$VAR1" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MARKETVALUE_IN_HUNDRED">
		<xsl:param name="FACTOR_MARKETVAL" />
		<xsl:variable name="VAR1" select="$FACTOR_MARKETVAL" />
		<xsl:variable name="VAR2">
			<xsl:value-of select="$VAR1 mod 100" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$VAR2 &gt; 0">
				<xsl:value-of select="$VAR1 + 100 - $VAR2" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$VAR1" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- To Display County of Operation -->
	<xsl:template name="COUNTY_OF_OPERATION_DISPLAY">
		<xsl:variable name="P_COUNTY_CREDIT">
			<xsl:call-template name="COUNTY_OPERATION_CHARGE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$P_COUNTY_CREDIT &gt; 0">
				Included
			</xsl:when>
			<xsl:otherwise>
				0
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- To Display County of Operation Credit percentage -->
	<xsl:template name="COUNTY_OPERATION_CHARGE">
		<xsl:variable name="P_COUNTY_TERRITORY" select="BOATS/BOAT/TERRITORYDOCKEDIN" />
		<xsl:choose>
			<xsl:when test="normalize-space($P_COUNTY_TERRITORY) !='' and BOATS/BOAT/BOATSTYLECODE !=$BOATSTYLE_TRAILER">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY_FACTOR']/NODE[@ID ='TERRITORYFACTOR']/ATTRIBUTES[@TERRITORY_CODE=$P_COUNTY_TERRITORY]/@CREDIT" />
			</xsl:when>
			<xsl:otherwise>
				0	
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- To Display County of Operation territory factor-->
	<xsl:template name="COUNTY_OPERATION_TERRITORY_FACTOR">
		<xsl:variable name="P_COUNTY_TERRITORY" select="BOATS/BOAT/TERRITORYDOCKEDIN" />
		<xsl:choose>
			<xsl:when test="normalize-space($P_COUNTY_TERRITORY) !='' and BOATS/BOAT/BOATSTYLECODE !=$BOATSTYLE_TRAILER">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY_FACTOR']/NODE[@ID ='TERRITORYFACTOR']/ATTRIBUTES[@TERRITORY_CODE=$P_COUNTY_TERRITORY]/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>
				1.00	
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="COMPONENTCODE_PHISCALDAMAGE">
		<xsl:variable name="P_BOAT_STYLE">
			<xsl:value-of select="BOATS/BOAT/BOATSTYLECODE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$P_BOAT_STYLE = $BOATSTYLE_TRAILER">
	BOAT_PD_T
	</xsl:when>
			<xsl:otherwise>
	BOAT_PD
	</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="COMPONENT_CODE">
		<xsl:param name="FACTORELEMENT" />
		<xsl:variable name="VAR_PHY_DAMAGE">
			<xsl:call-template name="COMPONENTCODE_PHISCALDAMAGE" />
		</xsl:variable>
		<xsl:variable name="P_BOAT_STYLE">
			<xsl:value-of select="BOATS/BOAT/BOATSTYLECODE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$FACTORELEMENT=$VAR_PHY_DAMAGE">
				<xsl:choose>
					<xsl:when test="normalize-space($FACTORELEMENT) = normalize-space('BOAT_PD_T')">
						<xsl:if test="POLICY/STATENAME=normalize-space('MICHIGAN')">
							<xsl:text>20002</xsl:text>
						</xsl:if>
						<xsl:if test="POLICY/STATENAME=normalize-space('INDIANA')">
							<xsl:text>20001</xsl:text>
						</xsl:if>
						<xsl:if test="POLICY/STATENAME =normalize-space('WISCONSIN')">
							<xsl:text>20003</xsl:text>
						</xsl:if>
					</xsl:when>
					<!--xsl:when test="$P_BOAT_STYLE=$BOATSTYLE_JETSKI">
						<xsl:if test="POLICY/STATENAME='MICHIGAN'">
							63
						</xsl:if>
						<xsl:if test="POLICY/STATENAME='INDIANA'">
							17
						</xsl:if>
						<xsl:if test="POLICY/STATENAME ='WISCONSIN'">
							819
						</xsl:if>
					</xsl:when-->
					<xsl:otherwise>
						<xsl:choose>
							<xsl:when test="BOATS/BOAT/COVERAGEBASIS='ACV'">
								<xsl:if test="POLICY/STATENAME=normalize-space('MICHIGAN')">
									<xsl:text>59</xsl:text>
								</xsl:if>
								<xsl:if test="POLICY/STATENAME=normalize-space('INDIANA')">
									<xsl:text>11</xsl:text>
								</xsl:if>
								<xsl:if test="POLICY/STATENAME =normalize-space('WISCONSIN')">
									<xsl:text>817</xsl:text>
								</xsl:if>
							</xsl:when>
							<xsl:when test="BOATS/BOAT/COVERAGEBASIS='AGV'">
								<xsl:if test="POLICY/STATENAME=normalize-space('MICHIGAN')">
									<xsl:text>61</xsl:text>
								</xsl:if>
								<xsl:if test="POLICY/STATENAME=normalize-space('INDIANA')">
									<xsl:text>15</xsl:text>
								</xsl:if>
								<xsl:if test="POLICY/STATENAME =normalize-space('WISCONSIN')">
									<xsl:text>818</xsl:text>
								</xsl:if>
							</xsl:when>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="normalize-space($FACTORELEMENT)=normalize-space('BOAT_LIABILITY_INCLUDE')">
				<xsl:if test="POLICY/STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>65</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME=normalize-space('INDIANA')">
					<xsl:text>19</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME =normalize-space('WISCONSIN')">
					<xsl:text>820</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="normalize-space($FACTORELEMENT)=normalize-space('BOAT_LIABILITY_PREMIUM')">
				<xsl:if test="POLICY/STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>65</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME=normalize-space('INDIANA')">
					<xsl:text>19</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME =normalize-space('WISCONSIN')">
					<xsl:text>820</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="normalize-space($FACTORELEMENT)=normalize-space('BOAT_MP_INCLUDE')">
				<xsl:if test="POLICY/STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>68</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME=normalize-space('INDIANA')">
					<xsl:text>21</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME =normalize-space('WISCONSIN')">
					<xsl:text>821</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="normalize-space($FACTORELEMENT)=normalize-space('BOAT_MP_PREMIUM')">
				<xsl:if test="POLICY/STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>68</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME=normalize-space('INDIANA')">
					<xsl:text>21</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME =normalize-space('WISCONSIN')">
					<xsl:text>821</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="normalize-space($FACTORELEMENT)=normalize-space('BOAT_UB_INCLUDE')">
				<xsl:if test="POLICY/STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>70</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME=normalize-space('INDIANA')">
					<xsl:text>24</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME =normalize-space('WISCONSIN')">
					<xsl:text>822</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="normalize-space($FACTORELEMENT)=normalize-space('BOAT_UB_PREMIUM')">
				<xsl:if test="POLICY/STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>70</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME=normalize-space('INDIANA')">
					<xsl:text>24</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME =normalize-space('WISCONSIN')">
					<xsl:text>822</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="normalize-space($FACTORELEMENT)=normalize-space('BOAT_TOW')">
				<xsl:if test="POLICY/STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>765</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME=normalize-space('INDIANA')">
					<xsl:text>764</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME =normalize-space('WISCONSIN')">
					<xsl:text>766</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="normalize-space($FACTORELEMENT)=normalize-space('BOAT_REPLACE')">
				<xsl:if test="POLICY/STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>762</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME=normalize-space('INDIANA')">
					<xsl:text>761</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME =normalize-space('WISCONSIN')">
					<xsl:text>763</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="normalize-space($FACTORELEMENT)=normalize-space('BOAT_OP720')">
				<xsl:if test="POLICY/STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>82</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME=normalize-space('INDIANA')">
					<xsl:text>40</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME =normalize-space('WISCONSIN')">
					<xsl:text>828</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="normalize-space($FACTORELEMENT)=normalize-space('BOAT_OP900')">
				<xsl:if test="POLICY/STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>83</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME=normalize-space('INDIANA')">
					<xsl:text>41</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME =normalize-space('WISCONSIN')">
					<xsl:text>829</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="normalize-space($FACTORELEMENT)=normalize-space('BOAT_AV100')">
				<xsl:if test="POLICY/STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>85</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME=normalize-space('INDIANA')">
					<xsl:text>43</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME =normalize-space('WISCONSIN')">
					<xsl:text>830</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>