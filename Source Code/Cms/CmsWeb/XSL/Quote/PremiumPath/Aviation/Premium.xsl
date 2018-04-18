<!-- ============================================================================================ -->
<!-- File Name		:	Primium.xsl																  -->
<!-- Description	:	This xsl file is for generating 
						the Final Primium (For vehicle component Auto)INDIANA and MICHIGAN		  -->
<!-- Developed By	:	Nidhi Sahay													  -->
<!-- ============================================================================================ -->
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0" xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:user="urn:user-namespace-here">
	<xsl:output encoding="ISO8859-1" omit-xml-declaration="no" method="xml" />
	<msxsl:script language="c#" implements-prefix="user">
<![CDATA[ 
		
		int ZONE_ID=2;
		int violationPercentage=0;
		public int GetPvioAcc(int AutoViolationValue)
		{
			int val=0;
			if(AutoViolationValue < 0)
			{
				violationPercentage=0;	
			}
			else
			{
			
				val=AutoViolationValue;
				violationPercentage = violationPercentage + val;
				
			}
			return violationPercentage;
		}
]]></msxsl:script>
	<!-- ============================================================================================ -->
	<!--								Loading ProductFactorMaster File (START)					  -->
	<!-- ============================================================================================ -->
	<xsl:variable name="HOCoveragesDoc" select="document('FactorPath')"></xsl:variable>
	<xsl:variable name="VT_CAMPER_TRAVEL_TRAILER" select="'CTT'" />
	<xsl:variable name="VT_UTILITY_TRAILER" select="'TR'" />
	<xsl:variable name="VT_CUSTOMIZED_VAN" select="'CV'" />
	<xsl:variable name="VT_PRIVATE_PASSENGER" select="'PP'" />
	<xsl:variable name="VT_MOTORHOME" select="'MH'" />
	<xsl:variable name="VT_SUSPENDED_COMP" select="'SCO'" />
	<xsl:variable name="STATE_INDIANA" select="'INDIANA'" />
	<xsl:variable name="STATE_MICHIGAN" select="'MICHIGAN'" />
	<xsl:variable name="VEHICLE_CLASS_PRE_2" select="2" />
	<xsl:variable name="VEHICLE_CLASS_PRE_3" select="3" />
	<xsl:variable name="VEHICLE_CLASS_PRE_4" select="4" />
	<xsl:variable name="VEHICLE_CLASS_PRE_6" select="6" />
	<xsl:variable name="VT_CLASSIC_CAR" select="'CLC'" />
	<xsl:variable name="VT_ANTIQUE_CAR" select="'ANC'" />
	<!-- ============================================================================================ -->
	<!--								Loading ProductFactorMaster File (END)						  -->
	<!-- ============================================================================================ -->
	<xsl:template match="/">
		<xsl:apply-templates select="INPUTXML/QUICKQUOTE" />
	</xsl:template>
	<xsl:template match="INPUTXML/QUICKQUOTE">
		<PREMIUM>
			<CREATIONDATE></CREATIONDATE>
			<GETPATH>
				<PRODUCT PRODUCTID="0">
					<!--Group for Vehicles-->
					<GROUP GROUPID="0">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID0" />
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<!-- Vehicle year VIN -->
						<SUBGROUP>
							<STEP STEPID="0">
								<PATH>ND</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Symbol, Class-->
						<SUBGROUP>
							<STEP STEPID="1">
								<PATH>ND</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Use ,Miles each way-->
						<SUBGROUP>
							<STEP STEPID="2">
								<PATH>ND</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Garaged At, Territory-->
						<SUBGROUP>
							<STEP STEPID="3">
								<PATH>ND</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Hull All Risk -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="4">
									<PATH>
									{
									<xsl:call-template name="HUAL" />
									}
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Hull Additional Coverage -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="5">
									<PATH>ND</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Combined Single Limit (Bodily Injury, PD) -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="6">
									<PATH>
									{
									<xsl:call-template name="EXTEXP_TRA" />
									}
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Runway Foaming & Crash Control -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="7">
									<PATH>
							{
								<xsl:call-template name="RUNFOAM_CRASH_CONT" />
							}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Search & Rescue -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="8">
									<PATH>
									{
										<xsl:call-template name="SEARCH_RESCUE" />
									}
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Michigan Historical Vehicle -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="9">
									<PATH>
									{
										<xsl:call-template name="SPC_EQUIP_RATE" />
									}
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- PPI -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="10">
									<PATH>
								{
									<xsl:call-template name="SPECIAL_EQUIP" />
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Medical Payments -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="11">
									<PATH>
								{
									<xsl:call-template name="NONOWNED_AIRCRAFT_PD" />
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!--Uninsured Motorists -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="12">
									<PATH>
								{
									<xsl:call-template name="THIRDPARTY_LIAB" />
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Under Insured Motorists -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="13">
									<PATH>ND</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!--Uninsured Motorists - Property Damagae-->
						<SUBGROUP>
							<IF>
								<STEP STEPID="14">
									<PATH>
								{
									<xsl:call-template name="CARGO_LIABILITY" />
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Comprehensive -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="15">
									<PATH>
								{
									<xsl:call-template name="GUESS_VOL_SETTL" />
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Collision -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="16">
									<PATH>
								 {
								 <xsl:call-template name="PERSONAL_INJURY" />											
								 }
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Mini-tort -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="17">
									<PATH>
									{
										<xsl:call-template name="PREMISES_LIAB" />
									}
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Road Service -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="18">
									<PATH>
									{
										<xsl:call-template name="PREMIS_MED_PAY" />
									}
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Rental Reimbursement -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="19">
									<PATH>
									{
										<xsl:call-template name="TRIP_INTURP" />
									}
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Loan/Loss Lease Gap-->
						<SUBGROUP>
							<IF>
								<STEP STEPID="20">
									<PATH>
								{
									 <xsl:call-template name="AV_52" />
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Sound Reproducing Tapes -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="21">
									<PATH>
								{
								<xsl:call-template name="AIRCRAFT_LIA_TP" />
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Sound Receiving and Transmitting Equipments -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="22">
									<PATH>
								{
								<xsl:call-template name="HULL_WAR_RISK" />
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Extra Equipment - Comprehensive  -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="23">
									<PATH>
								{
									<xsl:call-template name="SPARE_PARTS" />
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Extra Equipment -Broadened Collision -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="24">
									<PATH>
								{
									 
									<xsl:call-template name="WAR_SPARE" />
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
					</GROUP>
					<!--Group for Final Premium-->
					<GROUP GROUPID="1">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID1" />
						</xsl:attribute>
						<!-- Final Premium -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="25">
									<PATH>
							{
								<xsl:call-template name="FINALPREMIUM" />
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
					<!--Group for Vehicles-->
					<GROUP GROUPID="0" CALC_ID="10000">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID0" />
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<GROUPRULE></GROUPRULE>
						<GROUPFORMULA></GROUPFORMULA>
						<GDESC></GDESC>
						<!-- Vehicle year VIN -->
						<STEP STEPID="0" CALC_ID="1000" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID0" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE></COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Symbol, Class-->
						<STEP STEPID="1" CALC_ID="1001" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID1" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE>SYM_CLS</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Use ,Miles each way-->
						<STEP STEPID="2" CALC_ID="1002" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID2" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE>USE</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Garaged At, Territory-->
						<STEP STEPID="3" CALC_ID="1003" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID3" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE>GRG_LOC</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Hull All Risk -->
						<STEP STEPID="4" CALC_ID="1004" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID4" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/HULL_ALL_RISK_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/HULL_ALL_RISK_LIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>HUAR</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HUAL" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'HUAR'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Hull Additional Coverage -->
						<STEP STEPID="5" CALC_ID="1005" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID5" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE></COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- ExtraExpense_TRA (Temporary Replacement Aircraft Parts) -->
						<STEP STEPID="6" CALC_ID="1006" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID6" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/EXTRA_EXPENSE_TRA_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/EXTRA_EXPENSE_TRA_LIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>EETRA</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="EXTEXP_TRA" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'EETRA'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Runway Foaming & Crash Control -->
						<STEP STEPID="7" CALC_ID="1007" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID7" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/RUNWAY_FOAMING_CRASH_CONTROL_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/RUNWAY_FOAMING_CRASH_CONTROL_LIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>RFCCL</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="RUNFOAM_CRASH_CONT" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'RFCCL'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Hull War Risks -->
						<!-- Search & Rescue -->
						<STEP STEPID="8" CALC_ID="1008" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID8" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/SEARCH_RESCUE_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/SEARCH_RESCUE_LIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>SR</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="SEARCH_RESCUE" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'SR'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Replacement Aircraft Rental Expense -->
						<STEP STEPID="9" CALC_ID="1009" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID9" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/REPLACEMENT_AIRCRAFT_RENTAL_EXPENSE_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/REPLACEMENT_AIRCRAFT_RENTAL_EXPENSE_LIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>SEL</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="SPC_EQUIP_RATE" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'SEL'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Special Equipment-->
						<STEP STEPID="10" CALC_ID="1010" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID10" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/SPECIAL_EQUIPMENT_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/SPECIAL_EQUIPMENT_LIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>SE</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="SPECIAL_EQUIP" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'SE'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Non-Owned Aircraft Physical Damage -->
						<STEP STEPID="11" CALC_ID="1011" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID11" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/NON_OWNED_AIRCRAFT_PHYSICAL_DAMAGE_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/NON_OWNED_AIRCRAFT_PHYSICAL_DAMAGE_LIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>NOAPD</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="NONOWNED_AIRCRAFT_PD" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'NOAPD'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Third Party Liability -->
						<STEP STEPID="12" CALC_ID="1012" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID12" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/THIRD_PARTY_LIABILITY_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/THIRD_PARTY_LIABILITY_LIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>TPLD</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="THIRDPARTY_LIAB" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'TPLD'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- TP Liability Additional Coverage -->
						<STEP STEPID="13" CALC_ID="1013" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID13" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<!--xsl:value-of select="AIRCRAFTS/AIRCRAFT/TP_LIABILITY_ADDITIONAL_COVERAGE_LIMIT" /--></D_PATH>
							<L_PATH>
								<!--xsl:value-of select="AIRCRAFTS/AIRCRAFT/TP_LIABILITY_ADDITIONAL_COVERAGE_DEDUCTIBLE" /--></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>TPLAC</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<!--xsl:call-template name="TP_LIA_ADD_RATE" /--></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<!--xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'TPLAC'"></xsl:with-param>
								</xsl:call-template--></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Cargo Liability -->
						<STEP STEPID="14" CALC_ID="1014" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID14" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/CARGO_LIABILITY_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/CARGO_LIABILITY_LIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>CARLIA</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="CARGO_LIABILITY" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'CARLIA'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Guess Voluntary Settlement -->
						<STEP STEPID="15" CALC_ID="1015" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID15" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/GUESS_VOLUNTARY_SETTLEMENT_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/GUESS_VOLUNTARY_SETTLEMENT_LIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>GVS</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="GUESS_VOL_SETTL" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'GVS'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Personal Injury -->
						<STEP STEPID="16" CALC_ID="1016" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID16" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/PERSONAL_INJURY_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/PERSONAL_INJURY_LIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>PI</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="PERSONAL_INJURY" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'PI'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Premises Liability-->
						<STEP STEPID="17" CALC_ID="1017" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID17" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/PREMISES_LIABILITY_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/PREMISES_LIABILITY_LIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>PRE_LIA</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="PREMISES_LIAB" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'PRE_LIA'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Premises Medical Payments -->
						<STEP STEPID="18" CALC_ID="1018" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID18" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/PREMISES_MEDICAL_PAYMENTS_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/PREMISES_MEDICAL_PAYMENTS_LIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>PRE_MED_PAY</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="PREMIS_MED_PAY" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'PRE_MED_PAY'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Trip Interruption -->
						<STEP STEPID="19" CALC_ID="1019" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID19" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/TRIP_INTERRUPTION_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/TRIP_INTERRUPTION_LIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>TRP_INTRP</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="TRIP_INTURP" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'TRP_INTRP'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- AV-52 (War TP Liability)  -->
						<STEP STEPID="20" CALC_ID="1020" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID20" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/AV_52_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/AV_52_LIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>AV_WAR</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="AV_52" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'AV_WAR'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Non-Owned Aircraft Liability - TP -->
						<STEP STEPID="21" CALC_ID="1021" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID21" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/NON_OWNED_AIRCRAFT_LIABILITY_TP_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/NON_OWNED_AIRCRAFT_LIABILITY_TP_LIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>AIR_LIA_TP</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="AIRCRAFT_LIA_TP" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'AIR_LIA_TP'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<STEP STEPID="22" CALC_ID="1022" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID22" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/HULL_WAR_RISKS_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/HULL_WAR_RISKS_LIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>HUL_WR</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HULL_WAR_RISK" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'HUL_WR'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Hull Spares Parts -->
						<STEP STEPID="23" CALC_ID="1023" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID23" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/SPARE_PARTS_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/SPARE_PARTS_LIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>SP</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="SPARE_PARTS" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'SP'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Hull War Spares -->
						<STEP STEPID="24" CALC_ID="1024" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID24" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/HULL_WAR_SPARE_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="AIRCRAFTS/AIRCRAFT/HULL_WAR_SPARE_LIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>HWS</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="WAR_SPARE" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'HWS'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
					</GROUP>
					<!--Group for Final Premium-->
					<GROUP GROUPID="1" CALC_ID="10001" PREFIX="" SUFIX="" ISCALCULATE="F">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID1" />
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<GROUPRULE></GROUPRULE>
						<GROUPFORMULA></GROUPFORMULA>
						<GDESC></GDESC>
						<!-- Total Premium -->
						<STEP STEPID="25" CALC_ID="1025" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID25" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>SUMTOTAL</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="FINALPREMIUM" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
					</GROUP>
					<PRODUCTFORMULA>
						<!-- Formula for calculation of premium -->
						<GROUP1 GROUPID="10000">
							<VALUE>(@[CALC_ID=10000])</VALUE>
						</GROUP1>
						<GROUP1 GROUPID="10001">
							<VALUE>(@[CALC_ID=10001])</VALUE>
						</GROUP1>
					</PRODUCTFORMULA>
				</PRODUCT>
			</CALCULATION>
		</PREMIUM>
	</xsl:template>
	<!-- Start of factor templates-->
	<!-- ######################################### FINAL PREMIUM ################# -->
	<xsl:template name="FINALPREMIUM">
		<xsl:variable name="FINAL_PREMIUM">
			<xsl:call-template name="FINALPREMIUM_AVIATION" />
		</xsl:variable>
		<xsl:value-of select="$FINAL_PREMIUM" />
	</xsl:template>
	<xsl:template name="FINALPREMIUM_AVIATION">
		<xsl:variable name="VAR1">
			<xsl:call-template name="HUAL" />
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="THIRDPARTY_LIAB" />
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:call-template name="WAR_SPARE" />
		</xsl:variable>
		<xsl:variable name="VAR4">
			<xsl:call-template name="TP_LIA_ADD_RATE" />
		</xsl:variable>
		<xsl:variable name="VAR5">
			<xsl:call-template name="EXTEXP_TRA" />
		</xsl:variable>
		<xsl:variable name="VAR6">
			<xsl:call-template name="RUNFOAM_CRASH_CONT" />
		</xsl:variable>
		<xsl:variable name="VAR7">
			<xsl:call-template name="SPECIAL_EQUIP" />
		</xsl:variable>
		<xsl:variable name="VAR8">
			<xsl:call-template name="SEARCH_RESCUE" />
		</xsl:variable>
		<xsl:variable name="VAR9">
			<xsl:call-template name="SPC_EQUIP_RATE" />
		</xsl:variable>
		<xsl:variable name="VAR10">
			<xsl:call-template name="CARGO_LIABILITY" />
		</xsl:variable>
		<xsl:variable name="VAR11">
			<xsl:call-template name="GUESS_VOL_SETTL" />
		</xsl:variable>
		<xsl:variable name="VAR12">
			<xsl:call-template name="PERSONAL_INJURY" />
		</xsl:variable>
		<xsl:variable name="VAR13">
			<xsl:call-template name="HULL_WAR_RISK" />
		</xsl:variable>
		<xsl:variable name="VAR14">
			<xsl:call-template name="PREMIS_MED_PAY" />
		</xsl:variable>
		<xsl:variable name="VAR15">
			<xsl:call-template name="SPARE_PARTS" />
		</xsl:variable>
		<xsl:variable name="VAR16">
			<xsl:call-template name="PREMISES_LIAB" />
		</xsl:variable>
		<xsl:variable name="VAR17">
			<xsl:call-template name="TRIP_INTURP" />
		</xsl:variable>
		<xsl:variable name="VAR18">
			<xsl:call-template name="AV_52" />
		</xsl:variable>
		<xsl:variable name="VAR19">
			<xsl:call-template name="AIRCRAFT_LIA_TP" />
		</xsl:variable>
		<xsl:value-of select="$VAR1 + $VAR2 +  $VAR3 + $VAR4 + $VAR5 + $VAR6 + $VAR7 + $VAR8 + $VAR9 + $VAR10 + $VAR11 + $VAR12 + $VAR13 + $VAR14 + $VAR15 + $VAR16 + $VAR17 + $VAR18 + $VAR19" />
	</xsl:template>
	<!-- ######################################### END OF FINAL PREMIUM ################# -->
	<xsl:template name="HUAL">
		<xsl:choose>
			<xsl:when test="AIRCRAFTS/AIRCRAFT/HULL_ALL_RISK_RATE!=''">
				<xsl:value-of select="AIRCRAFTS/AIRCRAFT/HULL_ALL_RISK_RATE" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************************************* -->
	<!-- ********************************  Start of Property Damage**************************************************** -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- Property Damage -->
	<xsl:template name="THIRDPARTY_LIAB">
		<xsl:choose>
			<xsl:when test="AIRCRAFTS/AIRCRAFT/THIRD_PARTY_LIABILITY_RATE!=''">
				<xsl:value-of select="AIRCRAFTS/AIRCRAFT/THIRD_PARTY_LIABILITY_RATE" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of Property Damage - Commercial Michigan-->
	<!-- ********************************************************************************************************************************************* -->
	<!-- ********************************  Start of PPI  **************************************************** -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- PPI -->
	<xsl:template name="WAR_SPARE">
		<xsl:choose>
			<xsl:when test="AIRCRAFTS/AIRCRAFT/HULL_WAR_SPARE_RATE!=''">
				<xsl:value-of select="AIRCRAFTS/AIRCRAFT/HULL_WAR_SPARE_RATE" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of PPI -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- *************************************  Start of PIP ******************************************************************* -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="TP_LIA_ADD_RATE">
		<xsl:choose>
			<xsl:when test="AIRCRAFTS/AIRCRAFT/TP_LIABILITY_ADDITIONAL_COVERAGE_RATE!=''">
				<xsl:value-of select="AIRCRAFTS/AIRCRAFT/TP_LIABILITY_ADDITIONAL_COVERAGE_RATE" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of PIP -->
	<!--End of PIP -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- *************************************  Start of Medical  Payment ******************************************************************* -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- Medical  Payment -->
	<xsl:template name="EXTEXP_TRA">
		<xsl:choose>
			<xsl:when test="AIRCRAFTS/AIRCRAFT/EXTRA_EXPENSE_TRA_RATE!=''">
				<xsl:value-of select="AIRCRAFTS/AIRCRAFT/EXTRA_EXPENSE_TRA_RATE" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of Medical  Payment -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- *************************************  Start of Uninsured Motorists  ******************************************************************* -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- Uninsured Motorists -->
	<xsl:template name="RUNFOAM_CRASH_CONT">
		<xsl:choose>
			<xsl:when test="AIRCRAFTS/AIRCRAFT/RUNWAY_FOAMING_CRASH_CONTROL_RATE!=''">
				<xsl:value-of select="AIRCRAFTS/AIRCRAFT/RUNWAY_FOAMING_CRASH_CONTROL_RATE" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Start of UM Property Damage -->
	<xsl:template name="SPECIAL_EQUIP">
		<xsl:choose>
			<xsl:when test="AIRCRAFTS/AIRCRAFT/SPECIAL_EQUIPMENT_RATE!=''">
				<xsl:value-of select="AIRCRAFTS/AIRCRAFT/SPECIAL_EQUIPMENT_RATE" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of Uninsured Motorists-->
	<!-- ********************************************************************************************************************************************* -->
	<!-- **********************************  Start of Under Insured Motorists  ******************************************************************* -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- Underinsured Motorists -->
	<xsl:template name="SEARCH_RESCUE">
		<xsl:choose>
			<xsl:when test="AIRCRAFTS/AIRCRAFT/SEARCH_RESCUE_RATE!=''">
				<xsl:value-of select="AIRCRAFTS/AIRCRAFT/SEARCH_RESCUE_RATE" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of Underinsured Motorists  -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- ************************** Start of Comprehensive For Indiana and Michigan ******************************************************************* -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- Comprehensive -->
	<xsl:template name="SPC_EQUIP_RATE">
		<xsl:choose>
			<xsl:when test="AIRCRAFTS/AIRCRAFT/REPLACEMENT_AIRCRAFT_RENTAL_EXPENSE_RATE!=''">
				<xsl:value-of select="AIRCRAFTS/AIRCRAFT/REPLACEMENT_AIRCRAFT_RENTAL_EXPENSE_RATE" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="NONOWNED_AIRCRAFT_PD">
		<xsl:choose>
			<xsl:when test="AIRCRAFTS/AIRCRAFT/NON_OWNED_AIRCRAFT_PHYSICAL_DAMAGE_RATE!=''">
				<xsl:value-of select="AIRCRAFTS/AIRCRAFT/NON_OWNED_AIRCRAFT_PHYSICAL_DAMAGE_RATE" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************************************* -->
	<!-- ***********************************  Mini-Tort  ****************************************************************** -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="CARGO_LIABILITY">
		<xsl:choose>
			<xsl:when test="AIRCRAFTS/AIRCRAFT/CARGO_LIABILITY_RATE!=''">
				<xsl:value-of select="AIRCRAFTS/AIRCRAFT/CARGO_LIABILITY_RATE" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************************************* -->
	<!-- ***********************************  Road Services  ****************************************************************** -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="GUESS_VOL_SETTL">
		<xsl:choose>
			<xsl:when test="AIRCRAFTS/AIRCRAFT/GUESS_VOLUNTARY_SETTLEMENT_RATE!=''">
				<xsl:value-of select="AIRCRAFTS/AIRCRAFT/GUESS_VOLUNTARY_SETTLEMENT_RATE" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************************************* -->
	<!-- ****************************************  Rental Reimbursement ****************************************************************** -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="PERSONAL_INJURY">
		<xsl:choose>
			<xsl:when test="AIRCRAFTS/AIRCRAFT/PERSONAL_INJURY_RATE!=''">
				<xsl:value-of select="AIRCRAFTS/AIRCRAFT/PERSONAL_INJURY_RATE" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of Rental Reimbursement-->
	<!-- ********************************************************************************************************************************************* -->
	<!-- ************************************   MCCA Fee  ****************************************************************** -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="HULL_WAR_RISK">
		<xsl:choose>
			<xsl:when test="AIRCRAFTS/AIRCRAFT/HULL_WAR_RISKS_RATE!=''">
				<xsl:value-of select="AIRCRAFTS/AIRCRAFT/HULL_WAR_RISKS_RATE" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of MCCA Fee-->
	<!-- ********************************************************************************************************************************************* -->
	<!--MCCHA FEE -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="SPARE_PARTS">
		<xsl:choose>
			<xsl:when test="AIRCRAFTS/AIRCRAFT/SPARE_PARTS_RATE!=''">
				<xsl:value-of select="AIRCRAFTS/AIRCRAFT/SPARE_PARTS_RATE" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PREMISES_LIAB">
		<xsl:choose>
			<xsl:when test="AIRCRAFTS/AIRCRAFT/PREMISES_LIABILITY_RATE!=''">
				<xsl:value-of select="AIRCRAFTS/AIRCRAFT/PREMISES_LIABILITY_RATE" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************************************* -->
	<!-- *********************************  Sound reproducing tapes ************************************************************************ -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="PREMIS_MED_PAY">
		<xsl:choose>
			<xsl:when test="AIRCRAFTS/AIRCRAFT/REMISES_MEDICAL_PAYMENTS_RATE!=''">
				<xsl:value-of select="AIRCRAFTS/AIRCRAFT/REMISES_MEDICAL_PAYMENTS_RATE" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Sound receiving and transmitting tapes -->
	<xsl:template name="TRIP_INTURP">
		<xsl:choose>
			<xsl:when test="AIRCRAFTS/AIRCRAFT/TRIP_INTERRUPTION_RATE!=''">
				<xsl:value-of select="AIRCRAFTS/AIRCRAFT/TRIP_INTERRUPTION_RATE" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="AV_52">
		<xsl:choose>
			<xsl:when test="AIRCRAFTS/AIRCRAFT/AV_52_RATE!=''">
				<xsl:value-of select="AIRCRAFTS/AIRCRAFT/AV_52_RATE" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ************************************************************************************************************************************ -->
	<!-- *********************************  Extra Equipment :Collision ********************************************************************************* -->
	<!-- ************************************************************************************************************************************ -->
	<xsl:template name="AIRCRAFT_LIA_TP">
		<xsl:choose>
			<xsl:when test="AIRCRAFTS/AIRCRAFT/NON_OWNED_AIRCRAFT_LIABILITY_TP_RATE!=''">
				<xsl:value-of select="AIRCRAFTS/AIRCRAFT/NON_OWNED_AIRCRAFT_LIABILITY_TP_RATE" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ****************************************************************************************************************** -->
	<!-- *********************************** Template for Lables   ******************************************************************************** -->
	<!-- ****************************************************************************************************************** -->
	<!--  Group Details  -->
	<xsl:template name="GROUPID0">
		<xsl:text>AIRCRAFT : </xsl:text>
		<xsl:value-of select="VEHICLES/VEHICLE/VEHICLETYPEDESC" />
	</xsl:template>
	<xsl:template name="GROUPID3">
		<xsl:text>Final Premium</xsl:text>
	</xsl:template>
	<xsl:template name="PRODUCTNAME">
		<xsl:text>Aviation</xsl:text>
	</xsl:template>
	<xsl:template name="STEPID0"><xsl:value-of select="AIRCRAFTS/AIRCRAFT/AIRCRAFTROWID" />.<xsl:value-of select="'   '" /><xsl:value-of select="AIRCRAFTS/AIRCRAFT/VEHICLE_YEAR" /> <xsl:value-of select="'   '" />  <xsl:value-of select="AIRCRAFTS/AIRCRAFT/MAKE" /> <xsl:value-of select="'   '" />  <xsl:value-of select="AIRCRAFTS/AIRCRAFT/MAKE_OTHER" /> <xsl:value-of select="'   '" /> <xsl:value-of select="AIRCRAFTS/AIRCRAFT/MODEL" /> <xsl:value-of select="'   '" />     <xsl:text>VIN:</xsl:text><xsl:value-of select="AIRCRAFTS/AIRCRAFT/MODEL_OTHER" /></xsl:template>
	<xsl:template name="STEPID1"><xsl:text>ENGINE TYPE: </xsl:text><xsl:value-of select="VEHICLES/VEHICLE/ENGINE_TYPE" />,<xsl:value-of select="'   '" /><xsl:text>WING TYPE: </xsl:text><xsl:value-of select="VEHICLES/VEHICLE/WING_TYPE" />,<xsl:value-of select="'   '" />
						<xsl:value-of select="AIRCRAFTS/AIRCRAFT/USE_VEHICLE" />
	</xsl:template>
	<xsl:template name="STEPID2">
		<xsl:text>Use: </xsl:text>
		<xsl:value-of select="AIRCRAFTS/AIRCRAFT/USE_VEHICLE" />
	</xsl:template>
	<xsl:template name="STEPID3">
		<xsl:choose>
			<xsl:when test="AIRCRAFTS/AIRCRAFT/ZIPCODEGARAGEDLOCATION !='' and AIRCRAFTS/AIRCRAFT/ZIPCODEGARAGEDLOCATION !='0'">
				<xsl:text>Garaged Location: </xsl:text>
				<xsl:value-of select="AIRCRAFTS/AIRCRAFT/GARAGEDLOCATION" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="STEPID4">
		<xsl:text>Hull All Risks</xsl:text>
	</xsl:template>
	<xsl:template name="STEPID5">
		<xsl:text>Hull Additional Coverage</xsl:text>
	</xsl:template>
	<xsl:template name="STEPID6">
		<xsl:text>ExtraExpense_TRA (Temporary Replacement Aircraft Parts)</xsl:text>
	</xsl:template>
	<!--xsl:template name="STEPID6">Residual Liability (BI and PD)</xsl:template-->
	<xsl:template name="GROUPID1">
		<xsl:text>Final Premium</xsl:text>
	</xsl:template>
	<xsl:template name="STEPID7">
		<xsl:text>Runway Foaming &amp; Crash Control</xsl:text>
	</xsl:template>
	<xsl:template name="STEPID8">
		<xsl:text>Search &amp; Rescue</xsl:text>
	</xsl:template>
	<xsl:template name="STEPID9">
		<xsl:text>Replacement Aircraft Rental Expense</xsl:text>
	</xsl:template>
	<xsl:template name="STEPID10">
		<xsl:text>Special Equipment</xsl:text>
	</xsl:template>
	<xsl:template name="STEPID11">
		<xsl:text>Non-Owned Aircraft Physical Damage</xsl:text>
	</xsl:template>
	<xsl:template name="STEPID12">
		<xsl:text>Third Party Liability</xsl:text>
	</xsl:template>
	<xsl:template name="STEPID13">
		<xsl:text>TP Liability Additional Coverage</xsl:text>
	</xsl:template>
	<xsl:template name="STEPID14">
		<xsl:text>Cargo Liability</xsl:text>
	</xsl:template>
	<xsl:template name="STEPID15">
		<xsl:text>Guess Voluntary Settlement</xsl:text>
	</xsl:template>
	<xsl:template name="STEPID16">
		<xsl:text>Personal Injury</xsl:text>
	</xsl:template>
	<xsl:template name="STEPID17">
		<xsl:text>Premises Liability</xsl:text>
	</xsl:template>
	<xsl:template name="STEPID18">
		<xsl:text>Premises Medical Payments</xsl:text>
	</xsl:template>
	<xsl:template name="STEPID19">
		<xsl:text>Trip Interruption</xsl:text>
	</xsl:template>
	<xsl:template name="STEPID20">
		<xsl:text>AV-52 (War TP Liability)</xsl:text>
	</xsl:template>
	<xsl:template name="STEPID21">
		<xsl:text>Non-Owned Aircraft Liability - TP</xsl:text>
	</xsl:template>
	<xsl:template name="STEPID22">
		<xsl:text>Hull War Risks</xsl:text>
	</xsl:template>
	<xsl:template name="STEPID23">
		<xsl:text>Hull Spares Parts</xsl:text>
	</xsl:template>
	<xsl:template name="STEPID24">
		<xsl:text>Hull War Spares</xsl:text>
	</xsl:template>
	<xsl:template name="STEPID25">
		<xsl:text>Total Aircraft Premium</xsl:text>
	</xsl:template>
	<xsl:template name="COMPONENT_CODE">
		<xsl:param name="FACTORELEMENT" />
		<xsl:choose>
			<xsl:when test="$FACTORELEMENT='HUAR'">
				<xsl:text>1065</xsl:text>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='TPLD'">
				<xsl:text>1067</xsl:text>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='TPLAC'">
				<xsl:text>1068</xsl:text>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='HUL_WR'">
				<xsl:text>1083</xsl:text>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='SP'">
				<xsl:text>1084</xsl:text>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='HWS'">
				<xsl:text>1085</xsl:text>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='EETRA'">
				<xsl:text>1069</xsl:text>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='RFCCL'">
				<xsl:text>1070</xsl:text>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='SR'">
				<xsl:text>1071</xsl:text>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='SE'">
				<xsl:text>1073</xsl:text>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='SEL'">
				<xsl:text>1072</xsl:text>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='NOAPD'">
				<xsl:text>1074</xsl:text>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='CARLIA'">
				<xsl:text>1075</xsl:text>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='GVS'">
				<xsl:text>1076</xsl:text>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='PI'">
				<xsl:text>1077</xsl:text>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='PRE_LIA'">
				<xsl:text>1078</xsl:text>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='PRE_MED_PAY'">
				<xsl:text>1079</xsl:text>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='TRP_INTRP'">
				<xsl:text>1080</xsl:text>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='AV_WAR'">
				<xsl:text>1081</xsl:text>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='AIR_LIA_TP'">
				<xsl:text>1082</xsl:text>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>
