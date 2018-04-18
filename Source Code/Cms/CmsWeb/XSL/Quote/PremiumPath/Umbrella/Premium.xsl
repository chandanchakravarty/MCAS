<!-- =============================================================================================
Description:Generate the final premium for the UMBRELLA 
Developed By:NEERAJ SINGH 
Date:   24 Oct.2006   

============================================================================================ -->
<!--============================================================================================ -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:user="urn:user-namespace-here">
	<xsl:output encoding="ISO8859-1" omit-xml-declaration="no" method="xml" />
	<!-- ============================================================================================ -->
	<msxsl:script language="c#" implements-prefix="user">
<![CDATA[ 
		
		int ZONE_ID=2;
		int Premium=0;
		public int GetCal(int waterCraftValue)
		{
			int val=0;
			if(waterCraftValue < 0)
			{
				Premium=0;	
			}
			else
			{
			
				val=waterCraftValue;
				Premium = Premium + val;
				
			}
			return Premium;
		}
						
		public void SetZoneID(int zid)
		{
			ZONE_ID=zid;
		
		}
		
		public int GetZoneID()
		{
			return ZONE_ID;
		}
		
		
		int CountyCtr=0;
		public void SetCountyCounter(int ctr)
		{
			CountyCtr=ctr;
		
		}
		
		public int GetCountyCounter()
		{
			return CountyCtr;
		}
]]></msxsl:script>
	<!-- ============================================================================================ 
								Loading ProductFactorMaster File and other variables (START)					  
 ============================================================================================ -->
	<xsl:variable name="RDUmbrellaDoc" select="document('FactorPath')"></xsl:variable>
	<!-- ============================================================================================ 
								Loading ProductFactorMaster File (END)						  
 ============================================================================================ -->
	<xsl:template match="/">
		<xsl:apply-templates select="INPUTXML/QUICKQUOTE/UMBRELLA" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--								UMBRELLA Template(START)								  -->
	<!-- ============================================================================================ -->
	<xsl:template match="INPUTXML/QUICKQUOTE/UMBRELLA">
		<PREMIUM>
			<CREATIONDATE></CREATIONDATE>
			<GETPATH>
				<PRODUCT PRODUCTID="0" DESC="Premium Umbrella Rates">
					<!--<GROUP GROUPID="0">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
					</GROUP>
					<GROUP GROUPID="1">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
					</GROUP>-->
					<GROUP GROUPID="0">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
					</GROUP>
					<GROUP GROUPID="1">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<!-- Policy Premium -->
						<SUBGROUP>
							<STEP STEPID="0">
								<PATH>ND</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<STEP STEPID="1">
								<PATH>ND</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<STEP STEPID="2">
								<PATH>ND</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<STEP STEPID="3">
								<PATH>ND</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<STEP STEPID="4">
								<PATH>ND</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<STEP STEPID="5">
								<PATH>ND</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<STEP STEPID="6">
								<PATH>ND</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<STEP STEPID="7">
								<PATH>ND</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="8">
									<PATH>
								{
									<xsl:call-template name="PERSONAL_PREMISES_EXPOSURES" />
								}
								</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="9">{
									<PATH>
										<xsl:call-template name="RENTAL_UNIT_CHARGES" />
									</PATH>}
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<STEP STEPID="10">
								<PATH>ND</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<STEP STEPID="11">
								<PATH>ND</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="12">
									{	
										<PATH>
										<xsl:variable name="PAUTOMOBILE">
											<xsl:call-template name="AUTOMOBILE" />
										</xsl:variable>
										<xsl:choose>
											<xsl:when test="$PAUTOMOBILE &gt; 0">
												<xsl:value-of select="$PAUTOMOBILE" />
											</xsl:when>
											<xsl:otherwise>ND</xsl:otherwise>
										</xsl:choose>
									</PATH>
									}
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="13">
									<PATH>
								{
								
								<xsl:variable name="EXCLUDEUNINSMOTORIST" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/EXCLUDEUNINSMOTORIST" />
								<xsl:variable name="STATENAME" select="STATENAME" />
								<xsl:choose>
											<xsl:when test="$EXCLUDEUNINSMOTORIST = 'N'and $STATENAME != 'MICHIGAN'">
												<xsl:call-template name="UNINSURED_UNDERINSURED_MOTORIST_COVERAGE_AUTO" />
											</xsl:when>
											<xsl:otherwise>0</xsl:otherwise>
										</xsl:choose>								
								
								}
								</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="14">
									<PATH>
								{
								<xsl:call-template name="INEXPERIENCED_DRIVER_AUTOS" />
								}
								</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="15">
									{
									<PATH>
										<xsl:variable name="PMOTORHOMES">
											<xsl:call-template name="MOTORHOMES" />
										</xsl:variable>
										<xsl:choose>
											<xsl:when test="$PMOTORHOMES &gt; 0">
												<xsl:value-of select="$PMOTORHOMES" />
											</xsl:when>
											<xsl:otherwise>ND</xsl:otherwise>
										</xsl:choose>
									</PATH>
									}
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="16">
									<PATH>
								{
								
								<xsl:variable name="EXCLUDEUNINSMOTORIST" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/EXCLUDEUNINSMOTORIST" />
								<xsl:variable name="STATENAME" select="STATENAME" />
								<xsl:choose>
											<xsl:when test="$EXCLUDEUNINSMOTORIST = 'N'and $STATENAME != 'MICHIGAN'">
												<xsl:call-template name="UNINSURED_UNDERINSURED_MOTORIST_COVERAGE_MOTORHOME" />
											</xsl:when>
											<xsl:otherwise>0</xsl:otherwise>
										</xsl:choose>								
								
								}
								</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="17">
									<PATH>
									{
									<xsl:call-template name="INEXPERIENCED_DRIVER_MOTORHOMES" />
									}
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="18">
									{
									<PATH>
										<xsl:variable name="PMOTORCYCLE">
											<xsl:call-template name="MOTORCYCLE" />
										</xsl:variable>
										<xsl:choose>
											<xsl:when test="$PMOTORCYCLE &gt; 0">
												<xsl:value-of select="$PMOTORCYCLE" />
											</xsl:when>
											<xsl:otherwise>ND</xsl:otherwise>
										</xsl:choose>
									</PATH>						
									}									
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="19">
									<PATH>
								{
								
								<xsl:variable name="EXCLUDEUNINSMOTORIST" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/EXCLUDEUNINSMOTORIST" />
								<xsl:variable name="STATENAME" select="STATENAME" />
								<xsl:choose>
											<xsl:when test="$EXCLUDEUNINSMOTORIST = 'N'and $STATENAME = 'INDIANA'">
												<xsl:call-template name="UNINSURED_UNDERINSURED_MOTORIST_COVERAGE_MOTORCYCLE" />
											</xsl:when>
											<xsl:otherwise>0</xsl:otherwise>
										</xsl:choose>								
								
								}
								</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="20">
									<PATH>
								{
								<xsl:call-template name="INEXPERIENCED_DRIVER_MOTORCYCLES" />
								}
								</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="21">
									{
										<xsl:call-template name="ADDITIONAL_CHARGES_AND_DISCOUNTS" />	
									}
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<STEP STEPID="22">
								<PATH>ND</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="23">
									<PATH>
									{
										<xsl:call-template name="CALL_ADJUSTEDBASE" />
									}
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="24">
									
									{
									<PATH>
										<xsl:variable name="WATERCRAFT_PREM">
											<xsl:call-template name="WATERCRAFT_EXPOSURES" />
										</xsl:variable>
										<xsl:variable name="PWATERCRAFTTYPE">
											<xsl:call-template name="WATERCRAFT_TYPE" />
										</xsl:variable>
										<xsl:choose>
											<xsl:when test="$PWATERCRAFTTYPE = ''">ND</xsl:when>
											<xsl:when test="$WATERCRAFT_PREM ='' or $WATERCRAFT_PREM =0">ND</xsl:when>
											<xsl:otherwise>
												<xsl:value-of select="$WATERCRAFT_PREM" />
											</xsl:otherwise>
										</xsl:choose>
									</PATH>
									}
									
								</STEP>
							</IF>
						</SUBGROUP>
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
					<xsl:attribute name="DESC">
						<!--<xsl:call-template name="PRODUCTID0"/> --></xsl:attribute>
					<!--<GROUP GROUPID="0" CALC_ID="10000" PREFIX="" SUFIX="" ISCALCULATE="F">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID0" />
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION />
						</GROUPCONDITION>
						<GROUPRULE />
						<GROUPFORMULA />
						<GDESC />
					</GROUP>
					<GROUP GROUPID="1" CALC_ID="10001" PREFIX="" SUFIX="" ISCALCULATE="F">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID1" />
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION />
						</GROUPCONDITION>
						<GROUPRULE />
						<GROUPFORMULA />
						<GDESC />
					</GROUP>-->
					<GROUP GROUPID="0" CALC_ID="10002" PREFIX="" SUFIX="" ISCALCULATE="F">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID2" />
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION />
						</GROUPCONDITION>
						<GROUPRULE />
						<GROUPFORMULA />
						<GDESC />
					</GROUP>
					<GROUP GROUPID="1" CALC_ID="10003" PREFIX="" SUFIX="" ISCALCULATE="F">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID3" />
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION />
						</GROUPCONDITION>
						<GROUPRULE />
						<GROUPFORMULA />
						<GDESC />
						<!--Underlying Policy Limits  -->
						<STEP STEPID="0" CALC_ID="1000" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID0" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE></COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--  Personal AUTO -->
						<STEP STEPID="1" CALC_ID="1001" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID1" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE>AUTO_COV</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Uninsured/Underinsured Motorists -->
						<STEP STEPID="2" CALC_ID="1002" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID2" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE>UM</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Motorcycle -->
						<STEP STEPID="3" CALC_ID="1003" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID3" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE>MOT_COV</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--  Homeowners  -->
						<STEP STEPID="4" CALC_ID="1004" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID4" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE>HOM_COV</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Rental Dwelling  -->
						<STEP STEPID="5" CALC_ID="1005" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID5" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE>DW_COV</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Watercraft -->
						<STEP STEPID="6" CALC_ID="1006" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID6" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE>WL_COV</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Personal Excess Liability (Each Occurrence) -->
						<STEP STEPID="7" CALC_ID="1007" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID7" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH>
								<xsl:call-template name="EACH_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="UUMBRELLA_LIMIT" />
							</L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE></COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Personal/Premises Exposures -->
						<STEP STEPID="8" CALC_ID="1008" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID8" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>PEL</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="PERSONAL_PREMISES_EXPOSURES" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Rental Unit  -->
						<STEP STEPID="9" CALC_ID="1009" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID9" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE>REN_UN</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="RENTAL_UNIT_CHARGES" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Office -->
						<STEP STEPID="10" CALC_ID="1010" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID10" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE></COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Personal Automobile Exposures (6 Drivers) -->
						<STEP STEPID="11" CALC_ID="1011" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID11" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE></COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Automobile -->
						<STEP STEPID="12" CALC_ID="1012" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID12" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>AUTO_PRE</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:variable name="PAUTOMOBILE">
									<xsl:call-template name="AUTOMOBILE" />
								</xsl:variable>
								<xsl:choose>
									<xsl:when test="$PAUTOMOBILE &gt; 0">
										<xsl:value-of select="$PAUTOMOBILE" />
									</xsl:when>
									<xsl:otherwise>0</xsl:otherwise>
								</xsl:choose>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--Excess Uninsured/Underinsured Motorist Coverage Auto  -->
						<STEP STEPID="13" CALC_ID="10013" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:variable name="EXCLUDEUNINSMOTORIST" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/EXCLUDEUNINSMOTORIST" />
								<xsl:choose>
									<xsl:when test="$EXCLUDEUNINSMOTORIST = 'N'">
										<xsl:call-template name="STEPID13" />
									</xsl:when>
									<xsl:otherwise>
										<xsl:call-template name="STEPID13" />
									</xsl:otherwise>
								</xsl:choose>
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>UNIN_AUTO</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:variable name="EXCLUDEUNINSMOTORIST" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/EXCLUDEUNINSMOTORIST" />
								<xsl:variable name="STATENAME" select="STATENAME" />
								<xsl:choose>
									<xsl:when test="$EXCLUDEUNINSMOTORIST = 'N'and $STATENAME != 'MICHIGAN'">
										<xsl:call-template name="UNINSURED_UNDERINSURED_MOTORIST_COVERAGE_AUTO" />
									</xsl:when>
									<xsl:otherwise>0</xsl:otherwise>
								</xsl:choose>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Inexperienced Driver -->
						<STEP STEPID="14" CALC_ID="10014" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID14" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>AUTO_INEX_DRIV</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="INEXPERIENCED_DRIVER_AUTOS" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Motorhomes -->
						<STEP STEPID="15" CALC_ID="10015" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID15" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MOT_HOME_PRE</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:variable name="PMOTORHOMES">
									<xsl:call-template name="MOTORHOMES" />
								</xsl:variable>
								<xsl:choose>
									<xsl:when test="$PMOTORHOMES &gt; 0">
										<xsl:value-of select="$PMOTORHOMES" />
									</xsl:when>
									<xsl:otherwise>0</xsl:otherwise>
								</xsl:choose>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--Excess Uninsured/Underinsured Motorist Coverage  Motorhome -->
						<STEP STEPID="16" CALC_ID="10016" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:variable name="EXCLUDEUNINSMOTORIST" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/EXCLUDEUNINSMOTORIST" />
								<xsl:choose>
									<xsl:when test="$EXCLUDEUNINSMOTORIST = 'N'">
										<xsl:call-template name="STEPID13" />
									</xsl:when>
									<xsl:otherwise>
										<xsl:call-template name="STEPID13" />
									</xsl:otherwise>
								</xsl:choose>
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>UNIN_MOTOR_HOME</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:variable name="EXCLUDEUNINSMOTORIST" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/EXCLUDEUNINSMOTORIST" />
								<xsl:variable name="STATENAME" select="STATENAME" />
								<xsl:choose>
									<xsl:when test="$EXCLUDEUNINSMOTORIST = 'N'and $STATENAME != 'MICHIGAN'">
										<xsl:call-template name="UNINSURED_UNDERINSURED_MOTORIST_COVERAGE_MOTORHOME" />
									</xsl:when>
									<xsl:otherwise>0</xsl:otherwise>
								</xsl:choose>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Inexperienced Driver -->
						<STEP STEPID="17" CALC_ID="10017" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID17" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MOTORHOME_INEX_DRIV</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="INEXPERIENCED_DRIVER_MOTORHOMES" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Motorcycle -->
						<STEP STEPID="18" CALC_ID="10018" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID18" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MOTORCYCLE_PRE</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:variable name="PMOTORCYCLE">
									<xsl:call-template name="MOTORCYCLE" />
								</xsl:variable>
								<xsl:choose>
									<xsl:when test="$PMOTORCYCLE &gt; 0">
										<xsl:value-of select="$PMOTORCYCLE" />
									</xsl:when>
									<xsl:otherwise>0</xsl:otherwise>
								</xsl:choose>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--Excess Uninsured/Underinsured Motorist Coverage  Motorcycle -->
						<STEP STEPID="19" CALC_ID="10019" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:variable name="EXCLUDEUNINSMOTORIST" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/EXCLUDEUNINSMOTORIST" />
								<xsl:choose>
									<xsl:when test="$EXCLUDEUNINSMOTORIST = 'N'">
										<xsl:call-template name="STEPID13" />
									</xsl:when>
									<xsl:otherwise>
										<xsl:call-template name="STEPID13" />
									</xsl:otherwise>
								</xsl:choose>
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>UNIN_MOTOR_CYCLE</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:variable name="EXCLUDEUNINSMOTORIST" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/EXCLUDEUNINSMOTORIST" />
								<xsl:variable name="STATENAME" select="STATENAME" />
								<xsl:choose>
									<xsl:when test="$EXCLUDEUNINSMOTORIST = 'N'and $STATENAME = 'INDIANA'">
										<xsl:call-template name="UNINSURED_UNDERINSURED_MOTORIST_COVERAGE_MOTORCYCLE" />
									</xsl:when>
									<xsl:otherwise>0</xsl:otherwise>
								</xsl:choose>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Inexperienced Driver -->
						<STEP STEPID="20" CALC_ID="10020" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID20" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MOTORCYCLE_INEX_DRIV</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="INEXPERIENCED_DRIVER_MOTORCYCLES" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Discount - Mature Age 5% -->
						<STEP STEPID="21" CALC_ID="1021" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID21" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_MATURE</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="ADDITIONAL_CHARGES_AND_DISCOUNTS" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--  Additional Charges and Discounts -->
						<STEP STEPID="22" CALC_ID="10022" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID22" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE></COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Minimum Premium -->
						<STEP STEPID="23" CALC_ID="1023" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID23" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MINIMUMPREMIUM</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="CALL_ADJUSTEDBASE" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Watercraft Exposures -->
						<STEP STEPID="24" CALC_ID="1024" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID24" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>WAT_EXP</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:variable name="WATERCRAFT_PREM">
									<xsl:call-template name="WATERCRAFT_EXPOSURES" />
								</xsl:variable>
								<xsl:variable name="PWATERCRAFTTYPE">
									<xsl:call-template name="WATERCRAFT_TYPE" />
								</xsl:variable>
								<xsl:choose>
									<xsl:when test="$PWATERCRAFTTYPE = ''">0</xsl:when>
									<xsl:when test="$WATERCRAFT_PREM ='' or $WATERCRAFT_PREM =0">0</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="$WATERCRAFT_PREM" />
									</xsl:otherwise>
								</xsl:choose>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Final Premium -->
						<STEP STEPID="25" CALC_ID="1025" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID25" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>SUMTOTAL</COMPONENT_CODE>
							<COMP_ACT_PRE><xsl:call-template name="FINALPREMIUM" /></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
					</GROUP>
					<!--</PRODUCTFORMULA>-->
				</PRODUCT>
			</CALCULATION>
		</PREMIUM>
	</xsl:template>
	<!-- =========================================================================================================== 
								STEPDETAILS Template(END)								  
 ============================================================================================================== -->
	<!-- ========================================================================================================= 
									Templates for Lables  (START)							  
 ============================================================================================================= -->
	<!-- ===============================   Group Details	====================================================== -->
	<xsl:template name="GROUPID0"></xsl:template>
	<xsl:template name="GROUPID1"></xsl:template>
	<xsl:template name="GROUPID2">Territory :<xsl:value-of select="normalize-space(TERRITORYCODES)" /></xsl:template>
	<xsl:template name="GROUPID3">Personal Umbrella -Premium Quote</xsl:template>
	<!-- ===============================   Step Details	====================================================== -->
	<xsl:template name="STEPID0">Underlying Policy Limits</xsl:template>
	<xsl:template name="STEPID1">
		<xsl:call-template name="PERSONALAUTOPOLICYDDESCRIPTION" />
	</xsl:template>
	<xsl:template name="STEPID2">
		<xsl:call-template name="UNINSUREDMOTORIST" />
	</xsl:template> <!---<xsl:value-of select="normalize-space(PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/UNINSUNDERINSMOTORISTLIMIT)" />-->
	<xsl:template name="STEPID3">
		<xsl:call-template name="MOTORCYCLEDESCRIPTION" />
	</xsl:template>
	<xsl:template name="STEPID4">
		<xsl:call-template name="HOMEOWNERPOLICYDESCRIPTION" />
	</xsl:template>
	<xsl:template name="STEPID5">
		<xsl:call-template name="DWELLINGFIREPOLICYDESCRIPTION" />
	</xsl:template>
	<xsl:template name="STEPID6">
		<xsl:call-template name="WATERCRAFTPOLICYDESCRIPTION" />
	</xsl:template>
	<xsl:template name="STEPID7">Personal Excess Liability (Each Occurrence)
	<xsl:variable name="EXCLUDEUNINSMOTORIST" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/EXCLUDEUNINSMOTORIST" />
	<xsl:choose>
			<xsl:when test="$EXCLUDEUNINSMOTORIST = 'Y' and STATENAME!='MICHIGAN'">
		- THIS POLICY DOES NOT PROVIDE UNINSURED OR
			 UNDERINSURED MOTORIST COVERAGE  
			Form A-9 Notice of Option to Reject or Modify Indiana
			 Uninsured Motorist Coverages is REQUIRED.			
		</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="STEPID8">Personal/Premises Exposures</xsl:template>
	<xsl:template name="STEPID9">
		<xsl:call-template name="RENTALDWELLINGUNIT" />
	</xsl:template>
	<xsl:template name="STEPID10">
		<xsl:call-template name="OFFICEPREMISES" />
	</xsl:template>
	<xsl:template name="STEPID11">Personal Automobile Exposures ( <xsl:value-of select="normalize-space(PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/TOTALNUMBERDRIVERS)" /> Drivers)</xsl:template>
	<xsl:template name="STEPID12">
		<xsl:call-template name="AUTOMOBILE_NUMBER" />
	</xsl:template>
	<xsl:template name="STEPID13">Excess Uninsured/Underinsured Motorist Coverage</xsl:template>
	<xsl:template name="STEPID14">Charge for <xsl:value-of select="normalize-space(PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/INEXPDRIVERSAUTO)" /> Inexperienced Driver(AUTO)</xsl:template>
	<xsl:template name="STEPID15">
		<xsl:call-template name="MOTORHOMES_NUMBER" />
	</xsl:template>
	<xsl:template name="STEPID17"> Charge for <xsl:value-of select="normalize-space(PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/INEXPDRIVERSMOTORHOME)" /> Inexperienced Driver(MOTORHOME)</xsl:template>
	<xsl:template name="STEPID18">
		<xsl:call-template name="MOTORCYCLE_NUMBER" />
	</xsl:template>
	<xsl:template name="STEPID20"> Charge for <xsl:value-of select="normalize-space(PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/INEXPDRIVERSMOTORCYCL)" /> Inexperienced Driver(MOTORCYCLE)</xsl:template>
	<xsl:template name="STEPID24">
		<xsl:call-template name="WATERCRAFT_EXPOSER_DISPLAY" />
	</xsl:template>
	<!--<xsl:template name="STEPID20"><xsl:value-of select="normalize-space(TYPE)" />(<xsl:value-of select="normalize-space(LENGTH)" />,<xsl:value-of select="normalize-space(RATEDSPEED)" />,<xsl:value-of select="normalize-space(HOESEPOWER)" />)</xsl:template>-->
	<xsl:template name="STEPID22">
		<xsl:call-template name="DISCOUNTCHARGES" />
	</xsl:template>
	<xsl:template name="STEPID21">		- Discount - Mature Age 5%</xsl:template>
	<xsl:template name="STEPID23">Minimum Premium</xsl:template>
	<xsl:template name="STEPID25">Final Premium</xsl:template>
	<!--========================================================================================================== 
									Template for Lables  (END)								  
 ============================================================================================================== -->
	<!-- ============================================================================================ -->
	<!--								Base Premium Groups Template(START)								 -->
	<!--								  FOR MIcHIGAN and INDIANA										-->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--								STEPID Template(START)								 -->
	<!-- ============================================================================================ -->
	<xsl:template name="WATERCRAFT_EXPOSER_DISPLAY">
		<xsl:choose>
			<xsl:when test="WATERCRAFT_EXPOSURES/WATERCRAFT[@ID &lt; 999]">
				<xsl:variable name="VAR_BOATTYPE" select="BOATTYPE" />
				<xsl:choose>
					<xsl:when test="$VAR_BOATTYPE = ''">
					No Watercraft
					</xsl:when>
					<xsl:otherwise>
					Watercraft Exposures
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>No Watercraft</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Number of Auto Mobiles -->
	<xsl:template name="AUTOMOBILE_NUMBER">
		<xsl:variable name="PAUTOMOBILE" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/AUTOMOBILES" />
		<xsl:choose>
			<xsl:when test="$PAUTOMOBILE &gt; 0">
			- <xsl:value-of select="normalize-space(PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/AUTOMOBILES)" /> Auto 
		</xsl:when>
			<xsl:otherwise>No Auto</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Number of Motor Homes -->
	<xsl:template name="MOTORHOMES_NUMBER">
		<xsl:variable name="PMOTORHOMES" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/MOTOTHOMES" />
		<xsl:choose>
			<xsl:when test="$PMOTORHOMES &gt; 0">
			- <xsl:value-of select="normalize-space(PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/MOTOTHOMES)" /> Motorhomes 
		</xsl:when>
			<xsl:otherwise>No Motorhomes</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Number of Motor Cycle -->
	<xsl:template name="MOTORCYCLE_NUMBER">
		<xsl:variable name="PMOTORCYCLE" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/MOTORCYCLES" />
		<xsl:choose>
			<xsl:when test="$PMOTORCYCLE &gt; 0">
			- <xsl:value-of select="normalize-space(PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/MOTORCYCLES)" /> Motorcycle 
		</xsl:when>
			<xsl:otherwise>No Motorcycle</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PERSONALAUTOPOLICYDDESCRIPTION">
		<xsl:variable name="PERSONALAUTOPOLICY" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/PERSONALAUTOPOLICYLOWERLIMIT" />
		<xsl:variable name="PERSONALAUTOPOLICYCSL" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/AUTOCSL" />
		<xsl:variable name="FORMATEDCSL">
			<xsl:value-of select="format-number($PERSONALAUTOPOLICYCSL,'###')" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="(normalize-space($PERSONALAUTOPOLICY) !='' and normalize-space($PERSONALAUTOPOLICY) !='0') ">
	- Personal Auto -$<xsl:value-of select="normalize-space(PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/PERSONALAUTOPOLICYLOWERLIMIT)" />/<xsl:value-of select="normalize-space(PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/PERSONALAUTOPOLICYUPPERLIMIT)" />/<xsl:value-of select="normalize-space(PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/AUTOPD)" />
	</xsl:when>
			<xsl:when test="normalize-space($PERSONALAUTOPOLICYCSL) !='0' and normalize-space($PERSONALAUTOPOLICYCSL) !=''">
	- Personal Auto -$<xsl:value-of select="normalize-space($FORMATEDCSL)" /> CSL
	</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="UNINSUREDMOTORIST">
		<xsl:variable name="VAR_STATE" select="STATENAME" />
		<xsl:variable name="VAR_EXCLUDEUNINMOTORIST" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/EXCLUDEUNINSMOTORIST" />
		<xsl:variable name="VAR_UNINSUREDMOTORIST_BI" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/UNINSINSMOTORISTLIMITBISPLIT" />
		<xsl:variable name="VAR_UNINSUREDMOTORIST_CSL" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/UNINSUNDERINSMOTORISTLIMITCSL" />
		<xsl:choose>
			<xsl:when test="$VAR_STATE = 'MICHIGAN'"></xsl:when>
			<xsl:when test="$VAR_STATE = 'INDIANA' and $VAR_UNINSUREDMOTORIST_BI !='' and $VAR_UNINSUREDMOTORIST_BI !=0 and $VAR_EXCLUDEUNINMOTORIST ='N'">
				- Uninsured/Underinsured Motorists $<xsl:value-of select="$VAR_UNINSUREDMOTORIST_BI" />
			</xsl:when>
			<xsl:when test="$VAR_STATE = 'INDIANA' and $VAR_UNINSUREDMOTORIST_CSL !='' and $VAR_UNINSUREDMOTORIST_CSL !=0 and $VAR_EXCLUDEUNINMOTORIST ='N'">
				- Uninsured/Underinsured Motorists $<xsl:value-of select="$VAR_UNINSUREDMOTORIST_CSL" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MOTORCYCLEDESCRIPTION">
		<xsl:variable name="MOTORCYCLE" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/MOTORCYCLEPOLICYLOWERLIMIT" />
		<xsl:variable name="MOTORCSL" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/MOTORCSL" />
		<xsl:variable name="FORMATEDCSL">
			<xsl:value-of select="format-number($MOTORCSL,'###')" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$MOTORCYCLE !='' and $MOTORCYCLE !='0'">
	- Motorcycle - $<xsl:value-of select="normalize-space(PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/MOTORCYCLEPOLICYLOWERLIMIT)" />/<xsl:value-of select="normalize-space(PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/MOTORCYCLEPOLICYUPPERLIMIT)" />/<xsl:value-of select="normalize-space(PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/MOTORPD)" />
	</xsl:when>
			<xsl:when test="$MOTORCSL !='' and $MOTORCSL !='0'">
	- Motorcycle - $<xsl:value-of select="normalize-space($FORMATEDCSL)" /> CSL
	</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="HOMEOWNERPOLICYDESCRIPTION">
		<xsl:variable name="HOMEOWNER" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/HOMEOWNERPOLICY" />
		<xsl:choose>
			<xsl:when test="$HOMEOWNER !='' and $HOMEOWNER !='0'">
	- Homeowners - $<xsl:value-of select="normalize-space(PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/HOMEOWNERPOLICY)" />
	</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DWELLINGFIREPOLICYDESCRIPTION">
		<xsl:variable name="DWELINGFIRE" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/DWELLINGFIREPOLICY" />
		<xsl:choose>
			<xsl:when test="$DWELINGFIRE !='' and $DWELINGFIRE !='0'">
	- Rental Dwelling - $<xsl:value-of select="normalize-space(PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/DWELLINGFIREPOLICY)" />
	</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="WATERCRAFTPOLICYDESCRIPTION">
		<xsl:variable name="WATERCRAFT" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/WATERCRAFTPOLICY" />
		<xsl:choose>
			<xsl:when test="$WATERCRAFT !='' and $WATERCRAFT !='0'">
		- Watercraft -  $<xsl:value-of select="normalize-space(PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/WATERCRAFTPOLICY)" />
	</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="RENTALDWELLINGUNIT">
		<xsl:variable name="RENATL" select="PERSONAL_AUTO_EXPOSURES/PERSONAL_EXPOSURE/RENTALDWELLINGUNIT" />
		<xsl:choose>
			<xsl:when test="$RENATL !='' and $RENATL !='0'">
	- <xsl:value-of select="normalize-space(PERSONAL_AUTO_EXPOSURES/PERSONAL_EXPOSURE/RENTALDWELLINGUNIT)" /> Rental Unit 
	</xsl:when>
			<xsl:otherwise> NO Rental Unit</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="OFFICEPREMISES">
		<xsl:variable name="Office" select="PERSONAL_AUTO_EXPOSURES/PERSONAL_EXPOSURE/OFFICEPREMISES" />
		<xsl:choose>
			<xsl:when test="$Office !='' and $Office !='0'">
			- <xsl:value-of select="normalize-space(PERSONAL_AUTO_EXPOSURES/PERSONAL_EXPOSURE/OFFICEPREMISES)" /> Office 
	</xsl:when>
			<xsl:otherwise> No Office</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DISCOUNTCHARGES">
		<xsl:variable name="ADDITIONALCHARGES">
			<xsl:call-template name="CALL_ADJUSTEDBASE" />
		</xsl:variable>
		<xsl:variable name="DISCOUNT" select="MATUREDISCOUNT" />
		<xsl:choose>
			<xsl:when test="$ADDITIONALCHARGES !='' and $DISCOUNT !=''">
	Additional Charges and Discounts
	</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ==================================================================================================-->
	<!--								Rental charges for more than 4 units (Start)											-->
	<!-- ==================================================================================================-->
	<xsl:template name="RENTAL_UNIT_CHARGES">
		<xsl:variable name="RENTAL_UNIT_NUM" select="PERSONAL_AUTO_EXPOSURES/PERSONAL_EXPOSURE/RENTALDWELLINGUNIT" />
		<xsl:variable name="UNIT_CHARGES" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='RUC']/NODE[@ID='RENTAL_UNITS']/ATTRIBUTES/@CHARGE" />
		<xsl:variable name="MIN_UNIT" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='RUC']/NODE[@ID='RENTAL_UNITS']/ATTRIBUTES/@RENTALUNITS" />
		<xsl:choose>
			<xsl:when test="$RENTAL_UNIT_NUM &gt; $MIN_UNIT">
				<xsl:value-of select="round(($RENTAL_UNIT_NUM - $MIN_UNIT)*$UNIT_CHARGES)" />
			</xsl:when>
			<xsl:otherwise>ND</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ==================================================================================================-->
	<!--								Rental charges for more than 4 units (End)											-->
	<!-- ==================================================================================================-->
	<!--===================================================================================================-->
	<!--							For all Types of WaterCraft it gives Premium							-->
	<!--===================================================================================================-->
	<xsl:template name="WATERCRAFT_EXPOSURES">
		<xsl:variable name="UUMBRELLALIMIT" select="UUMBRELLA_LIMIT" />
		<xsl:variable name="VAR_TERRITORY" select="TERRITORYCODES" />
		<!-- Run a loop for each boat. -->
		<xsl:variable name="VAR_SUMOFPREMIUMS" select="user:GetCal(-1)" />
		<xsl:variable name="test">
			<xsl:for-each select="WATERCRAFT_EXPOSURES/WATERCRAFT[@ID &lt; 9999999]">
				<!-- add the premiums -->
				<xsl:variable name="VAR_LENTH" select="LENGTH" />
				<xsl:variable name="VAR_HORSEPOWER" select="HORSEPOWER" />
				<xsl:variable name="VAR_SPEED" select="RATEDSPEED" />
				<xsl:variable name="VAR_PREMIUM_FOR_BOAT" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='WATERCRAFT_PREMIUMS']/NODE[@ID='PREMIUM_VALUE']/ATTRIBUTES[@TERRITORY = $VAR_TERRITORY and @SPEED_LL &lt;= $VAR_SPEED and @SPEED_UL &gt;= $VAR_SPEED and @LENGTH_LL &lt;= $VAR_LENTH and @LENGTH_UL &gt;= $VAR_LENTH and @HP_LL &lt;= $VAR_HORSEPOWER and @HP_UL &gt;= $VAR_HORSEPOWER]/@PREMIUM" />
				<xsl:if test="$VAR_PREMIUM_FOR_BOAT !=0">
					<xsl:value-of select="user:GetCal($VAR_PREMIUM_FOR_BOAT)" />
				</xsl:if>
			</xsl:for-each>
		</xsl:variable>
		<xsl:variable name="var1" select="user:GetCal(0)" />
		<xsl:variable name="LIMIT_FACTOR">
			<xsl:call-template name="PREMIUM_FACTOR" />
		</xsl:variable>
		<xsl:value-of select="$var1 * $LIMIT_FACTOR" />
	</xsl:template>
	<xsl:template name="WATERCRAFT_TYPE">
		<xsl:for-each select="WATERCRAFT_EXPOSURES/WATERCRAFT">
			<xsl:value-of select="BOATTYPE" />
		</xsl:for-each>
	</xsl:template>
	<!-- ============================================================================================== -->
	<!--		Templates is used to find Premium of MOTORCYCLE and AUTOMOBILE  and MOTORHOMES			-->
	<!-- ============================================================================================== -->
	<xsl:template name="MOTORHOMES">
		<xsl:variable name="UUMBRELLALIMIT" select="UUMBRELLA_LIMIT" />
		<xsl:variable name="TERRITORYS" select="TERRITORYCODES" />
		<xsl:variable name="MOTOTHOME" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/MOTOTHOMES" />
		<xsl:variable name="MOTORHOMESPOLICYLOWERLIMITS" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/PERSONALAUTOPOLICYLOWERLIMIT" />
		<xsl:variable name="MOTORHOMESPOLICYUPPERLIMITS" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/PERSONALAUTOPOLICYUPPERLIMIT" />
		<xsl:variable name="MOTORCSL" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/AUTOCSL" />
		<xsl:variable name="MOTORPD" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/AUTOPD" />
		<!-- Umbrella's available bisplit limit -->
		<xsl:variable name="UMBBISPLITLOWERLIMIT" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGES']/NODE[@ID='MOTORCYCLE_MOTOR_HOME_BI']/ATTRIBUTES[@TERRITORY=$TERRITORYS]/@LOWERLIMITBISPLIT" />
		<xsl:variable name="UMBBISPLITUPPERLIMIT" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGES']/NODE[@ID='MOTORCYCLE_MOTOR_HOME_BI']/ATTRIBUTES[@TERRITORY=$TERRITORYS]/@UPPERLIMITBISPLIT" />
		<!-- Umbrella's available PD limit -->
		<xsl:variable name="UMBPDLIMIT" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGES']/NODE[@ID='MOTORCYCLE_MOTOR_HOME_BI']/ATTRIBUTES[@TERRITORY=$TERRITORYS]/@PD_LIMIT" />
		<!-- Inputxml bisplitlimit -->
		<xsl:variable name="XMLBISPLITLOWERLIMIT">
			<xsl:choose>
				<xsl:when test="normalize-space($MOTORHOMESPOLICYLOWERLIMITS) = ''">0</xsl:when>
				<xsl:when test="$MOTORHOMESPOLICYLOWERLIMITS &gt; 0">
					<xsl:value-of select="format-number($MOTORHOMESPOLICYLOWERLIMITS,'###')" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="XMLBISPLITUPERLIMIT">
			<xsl:choose>
				<xsl:when test="normalize-space($MOTORHOMESPOLICYUPPERLIMITS) = ''">0</xsl:when>
				<xsl:when test="$MOTORHOMESPOLICYUPPERLIMITS &gt; 0">
					<xsl:value-of select="format-number(($MOTORHOMESPOLICYUPPERLIMITS div 1000),'###')" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<!-- Picking correct bisplit limit to genrate rates -->
		<xsl:variable name="BISPLITLOWERLIMIT">
			<xsl:choose>
				<xsl:when test="$UMBBISPLITLOWERLIMIT!=$XMLBISPLITLOWERLIMIT">
					<xsl:value-of select="$UMBBISPLITLOWERLIMIT" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$XMLBISPLITLOWERLIMIT" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="BISPLITUPERLIMIT">
			<xsl:choose>
				<xsl:when test="$UMBBISPLITUPPERLIMIT!=$XMLBISPLITUPERLIMIT">
					<xsl:value-of select="$UMBBISPLITUPPERLIMIT" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$XMLBISPLITUPERLIMIT" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<!-- Inputxml PDlimit -->
		<xsl:variable name="XMLPD">
			<xsl:choose>
				<xsl:when test="normalize-space($MOTORPD) = ''">0</xsl:when>
				<xsl:when test="$MOTORPD &gt; 0">
					<xsl:value-of select="format-number(($MOTORPD div 1000),'###')" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<!-- picking correct pd limit to genrate rates -->
		<xsl:variable name="PD">
			<xsl:choose>
				<xsl:when test="$UMBPDLIMIT != $XMLPD">
					<xsl:value-of select="$UMBPDLIMIT" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$XMLPD" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<!-- inputxml csl limit -->
		<xsl:variable name="XMLCSLLIMIT">
			<xsl:choose>
				<xsl:when test="normalize-space($MOTORCSL) = ''">0</xsl:when>
				<xsl:when test="$MOTORCSL &gt; 0">
					<xsl:value-of select="format-number(($MOTORCSL div 1000),'###')" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<!-- umbrella available csl limit -->
		<xsl:variable name="UMBCSLLIMIT">
			<xsl:variable name="AVUMBCSLLIMIT" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MAPING_COVERAGES']/NODE[@ID='MAPING_MOTORCYCLE_MOTOR_HOME_CSL']/ATTRIBUTES[@TERRITORY = $TERRITORYS and @CSL_LIMIT_MIN &lt;= $XMLCSLLIMIT and @CSL_LIMIT_MAX &gt; $XMLCSLLIMIT]/@CSL_LIMIT_MIN" />
			<xsl:variable name="AVUMBCSLLIMITMIN" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MAPING_COVERAGES']/NODE[@ID='MAPING_MOTORCYCLE_MOTOR_HOME_CSL']/ATTRIBUTES[@TERRITORY = $TERRITORYS]/@CSL_MIN" />
			<xsl:choose>
				<xsl:when test="$AVUMBCSLLIMIT !=''"> <!-- if xml limit is between  available UMB limit-->
					<xsl:value-of select="$AVUMBCSLLIMIT" />
				</xsl:when>
				<xsl:when test="$XMLCSLLIMIT &lt; $AVUMBCSLLIMITMIN"> <!-- if xml limit is less than available minimum UMB limit-->
					<xsl:value-of select="$AVUMBCSLLIMITMIN" />
				</xsl:when>
				<xsl:otherwise> <!-- if xml limit is greater than highest available UMB limit-->
					<xsl:value-of select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MAPING_COVERAGES']/NODE[@ID='MAPING_MOTORCYCLE_MOTOR_HOME_CSL']/ATTRIBUTES[@TERRITORY = $TERRITORYS ]/@CSL_MAX" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<!-- picking correct umbrella limit for genrating rates -->
		<xsl:variable name="CSLLIMIT">
			<xsl:choose>
				<xsl:when test="$UMBCSLLIMIT!=$XMLCSLLIMIT">
					<xsl:value-of select="$UMBCSLLIMIT" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$XMLCSLLIMIT" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="MOTORHOMESPOLICYVALUE">
			<xsl:choose>
				<xsl:when test="normalize-space($MOTORHOMESPOLICYUPPERLIMITS) !='' and normalize-space($MOTORHOMESPOLICYUPPERLIMITS) !='0' and normalize-space($MOTORHOMESPOLICYLOWERLIMITS) !='' and normalize-space($MOTORHOMESPOLICYLOWERLIMITS) !='0'">
					<xsl:value-of select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGES']/NODE[@ID='MOTORCYCLE_MOTOR_HOME_BI']/ATTRIBUTES[@TERRITORY = $TERRITORYS and @LOWERLIMITBISPLIT = $BISPLITLOWERLIMIT and @UPPERLIMITBISPLIT = $BISPLITUPERLIMIT and @PD_LIMIT=$PD]/@VALUE" />
				</xsl:when>
				<xsl:when test="normalize-space($MOTORCSL) != '' and  normalize-space($MOTORCSL) != '0'"> <!-- CSL limit case -->
					<xsl:value-of select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGES']/NODE[@ID='MOTORCYCLE_MOTOR_HOME_CSL']/ATTRIBUTES[@TERRITORY = $TERRITORYS and @CSL_LIMIT = $CSLLIMIT]/@VALUE" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="var1">
			<xsl:choose>
				<xsl:when test="normalize-space($MOTOTHOME) =''">0</xsl:when>
				<xsl:when test="$MOTOTHOME &gt; 0">
					<xsl:value-of select="round($MOTORHOMESPOLICYVALUE * $MOTOTHOME)" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="LIMIT_FACTOR">
			<xsl:call-template name="PREMIUM_FACTOR" />
		</xsl:variable>
		<xsl:value-of select="$var1 * $LIMIT_FACTOR" />
	</xsl:template>
	<xsl:template name="MOTORCYCLE">
		<xsl:variable name="UUMBRELLALIMIT" select="UUMBRELLA_LIMIT" />
		<xsl:variable name="TERRITORYS" select="TERRITORYCODES" />
		<xsl:variable name="MOTTORCYCLES" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/MOTORCYCLES" />
		<xsl:variable name="MOTORCYCLEPOLICYLOWERLIMITS" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/MOTORCYCLEPOLICYLOWERLIMIT" />
		<xsl:variable name="MOTORCYCLEPOLICYUPPERLIMITS" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/MOTORCYCLEPOLICYUPPERLIMIT" />
		<xsl:variable name="MOTORCSL" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/MOTORCSL" />
		<xsl:variable name="MOTORPD" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/MOTORPD" />
		<!-- Umbrella's available bisplit limit -->
		<xsl:variable name="UMBBISPLITLOWERLIMIT" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGES']/NODE[@ID='MOTORCYCLE_MOTOR_HOME_BI']/ATTRIBUTES[@TERRITORY=$TERRITORYS]/@LOWERLIMITBISPLIT" />
		<xsl:variable name="UMBBISPLITUPPERLIMIT" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGES']/NODE[@ID='MOTORCYCLE_MOTOR_HOME_BI']/ATTRIBUTES[@TERRITORY=$TERRITORYS]/@UPPERLIMITBISPLIT" />
		<!-- Umbrella's available PD limit -->
		<xsl:variable name="UMBPDLIMIT" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGES']/NODE[@ID='MOTORCYCLE_MOTOR_HOME_BI']/ATTRIBUTES[@TERRITORY=$TERRITORYS]/@PD_LIMIT" />
		<!-- inputxml available bisplit limit -->
		<xsl:variable name="XMLBISPLITLOWERLIMIT">
			<xsl:choose>
				<xsl:when test="normalize-space($MOTORCYCLEPOLICYLOWERLIMITS) = ''">0</xsl:when>
				<xsl:when test="$MOTORCYCLEPOLICYLOWERLIMITS &gt; 0">
					<xsl:value-of select="format-number($MOTORCYCLEPOLICYLOWERLIMITS,'###')" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="XMLBISPLITUPERLIMIT">
			<xsl:choose>
				<xsl:when test="normalize-space($MOTORCYCLEPOLICYUPPERLIMITS) = ''">0</xsl:when>
				<xsl:when test="$MOTORCYCLEPOLICYUPPERLIMITS &gt; 0">
					<xsl:value-of select="format-number(($MOTORCYCLEPOLICYUPPERLIMITS div 1000),'###')" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<!-- pick correct bisplit limit to genrate rate -->
		<xsl:variable name="BISPLITLOWERLIMIT">
			<xsl:choose>
				<xsl:when test="$UMBBISPLITLOWERLIMIT!=$XMLBISPLITLOWERLIMIT">
					<xsl:value-of select="$UMBBISPLITLOWERLIMIT" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$XMLBISPLITLOWERLIMIT" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="BISPLITUPERLIMIT">
			<xsl:choose>
				<xsl:when test="$UMBBISPLITUPPERLIMIT!=$XMLBISPLITUPERLIMIT">
					<xsl:value-of select="$UMBBISPLITUPPERLIMIT" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$XMLBISPLITUPERLIMIT" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<!-- inputxml pd limit -->
		<xsl:variable name="XMLPD">
			<xsl:choose>
				<xsl:when test="normalize-space($MOTORPD) = ''">0</xsl:when>
				<xsl:when test="$MOTORPD &gt; 0">
					<xsl:value-of select="format-number(($MOTORPD div 1000),'###')" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<!-- Picking correct pd limit to genrate rates-->
		<xsl:variable name="PD">
			<xsl:choose>
				<xsl:when test="$UMBPDLIMIT != $XMLPD">
					<xsl:value-of select="$UMBPDLIMIT" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$XMLPD" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<!--  inputxml csl limit -->
		<xsl:variable name="XMLCSLLIMIT">
			<xsl:choose>
				<xsl:when test="normalize-space($MOTORCSL) = ''">0</xsl:when>
				<xsl:when test="$MOTORCSL &gt; 0">
					<xsl:value-of select="format-number(($MOTORCSL div 1000),'###')" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<!-- picking umbrella csllimit  -->
		<xsl:variable name="UMBCSLLIMIT">
			<xsl:variable name="AVUMBCSLLIMIT" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MAPING_COVERAGES']/NODE[@ID='MAPING_MOTORCYCLE_MOTOR_HOME_CSL']/ATTRIBUTES[@TERRITORY = $TERRITORYS and @CSL_LIMIT_MIN &lt;= $XMLCSLLIMIT and @CSL_LIMIT_MAX &gt; $XMLCSLLIMIT]/@CSL_LIMIT_MIN" />
			<xsl:variable name="AVUMBCSLLIMITMIN" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MAPING_COVERAGES']/NODE[@ID='MAPING_MOTORCYCLE_MOTOR_HOME_CSL']/ATTRIBUTES[@TERRITORY = $TERRITORYS]/@CSL_MIN" />
			<xsl:choose>
				<xsl:when test="$AVUMBCSLLIMIT !=''"> <!-- if xml limit is between available UMB limit-->
					<xsl:value-of select="$AVUMBCSLLIMIT" />
				</xsl:when>
				<xsl:when test="$XMLCSLLIMIT &lt; $AVUMBCSLLIMITMIN"> <!-- if xml limit is less than available minimum UMB limit-->
					<xsl:value-of select="$AVUMBCSLLIMITMIN" />
				</xsl:when>
				<xsl:otherwise> <!-- if xml limit is greater than highest available UMB limit-->
					<xsl:value-of select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MAPING_COVERAGES']/NODE[@ID='MAPING_MOTORCYCLE_MOTOR_HOME_CSL']/ATTRIBUTES[@TERRITORY = $TERRITORYS ]/@CSL_MAX" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<!-- picking correct umbrella limit for genrating rates -->
		<xsl:variable name="CSLLIMIT">
			<xsl:choose>
				<xsl:when test="$UMBCSLLIMIT!=$XMLCSLLIMIT">
					<xsl:value-of select="$UMBCSLLIMIT" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$XMLCSLLIMIT" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="MOTORCYCLEPOLICYVALUE">
			<xsl:choose>
				<xsl:when test="normalize-space($UMBBISPLITUPPERLIMIT) !='' and normalize-space($UMBBISPLITUPPERLIMIT) !='0' and normalize-space($MOTORCYCLEPOLICYLOWERLIMITS) !='' and normalize-space($MOTORCYCLEPOLICYLOWERLIMITS) !='0'">
					<xsl:value-of select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGES']/NODE[@ID='MOTORCYCLE_MOTOR_HOME_BI']/ATTRIBUTES[@TERRITORY = $TERRITORYS and @LOWERLIMITBISPLIT = $BISPLITLOWERLIMIT and @UPPERLIMITBISPLIT = $BISPLITUPERLIMIT and @PD_LIMIT=$PD]/@VALUE" />
				</xsl:when>
				<xsl:when test="normalize-space($MOTORCSL) != '' and  normalize-space($MOTORCSL) != '0'"> <!-- CSL limit case -->
					<xsl:value-of select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGES']/NODE[@ID='MOTORCYCLE_MOTOR_HOME_CSL']/ATTRIBUTES[@TERRITORY = $TERRITORYS and @CSL_LIMIT = $CSLLIMIT]/@VALUE" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="var1">
			<xsl:choose>
				<xsl:when test="normalize-space($MOTTORCYCLES) =''">0</xsl:when>
				<xsl:when test="$MOTTORCYCLES &gt; 0">
					<xsl:value-of select="round($MOTORCYCLEPOLICYVALUE * $MOTTORCYCLES)" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="LIMIT_FACTOR">
			<xsl:call-template name="PREMIUM_FACTOR" />
		</xsl:variable>
		<xsl:value-of select="$var1 * $LIMIT_FACTOR" />
	</xsl:template>
	<xsl:template name="AUTOMOBILE">
		<xsl:variable name="UUMBRELLALIMIT" select="UUMBRELLA_LIMIT" />
		<xsl:variable name="TERRITORYS" select="TERRITORYCODES" />
		<xsl:variable name="VEHICLEDRIVERS" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/TOTALNUMBERDRIVERS" />
		<xsl:variable name="NUMBERAUTO" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/AUTOMOBILES" />
		<xsl:variable name="PERSONALAUTOPOLICYLOWERLIMITS" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/PERSONALAUTOPOLICYLOWERLIMIT" />
		<xsl:variable name="PERSONALAUTOPOLICYUPPERLIMITS" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/PERSONALAUTOPOLICYUPPERLIMIT" />
		<xsl:variable name="AUTOPD" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/AUTOPD" />
		<xsl:variable name="AUTOCSL" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/AUTOCSL" />
		<!-- Umbrella's available bisplit limit -->
		<xsl:variable name="UMBBISPLITLOWERLIMIT" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGES']/NODE[@ID='AUTOMOBILE_BI']/ATTRIBUTES[@TERRITORY=$TERRITORYS]/@LOWERLIMITBISPLIT" />
		<xsl:variable name="UMBBISPLITUPPERLIMIT" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGES']/NODE[@ID='AUTOMOBILE_BI']/ATTRIBUTES[@TERRITORY=$TERRITORYS]/@UPPERLIMITBISPLIT" />
		<!-- Umbrella's available PD limit -->
		<xsl:variable name="UMBPDLIMIT" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGES']/NODE[@ID='AUTOMOBILE_BI']/ATTRIBUTES[@TERRITORY=$TERRITORYS]/@PD_LIMIT" />
		<!-- inputxml available bisplit limit -->
		<xsl:variable name="XMLBISPLITLOWERLIMIT">
			<xsl:choose>
				<xsl:when test="normalize-space($PERSONALAUTOPOLICYLOWERLIMITS) = ''">0</xsl:when>
				<xsl:when test="$PERSONALAUTOPOLICYLOWERLIMITS &gt; 0">
					<xsl:value-of select="format-number($PERSONALAUTOPOLICYLOWERLIMITS,'###')" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="XMLBISPLITUPPERLIMIT">
			<xsl:choose>
				<xsl:when test="normalize-space($PERSONALAUTOPOLICYUPPERLIMITS) = ''">0</xsl:when>
				<xsl:when test="$PERSONALAUTOPOLICYUPPERLIMITS &gt; 0">
					<xsl:value-of select='format-number(($PERSONALAUTOPOLICYUPPERLIMITS div 1000),"###")' />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<!-- Picking correct bisplit limit to genrate rates -->
		<xsl:variable name="BISPLITLOWERLIMIT">
			<xsl:choose>
				<xsl:when test="$UMBBISPLITLOWERLIMIT!=$XMLBISPLITLOWERLIMIT">
					<xsl:value-of select="$UMBBISPLITLOWERLIMIT" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$XMLBISPLITLOWERLIMIT" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="BISPLITUPPERLIMIT">
			<xsl:choose>
				<xsl:when test="$UMBBISPLITUPPERLIMIT!=$XMLBISPLITUPPERLIMIT">
					<xsl:value-of select="$UMBBISPLITUPPERLIMIT" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$XMLBISPLITUPPERLIMIT" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<!--Input xml PD limit  -->
		<xsl:variable name="XMLPD">
			<xsl:choose>
				<xsl:when test="normalize-space($AUTOPD) = ''">0</xsl:when>
				<xsl:when test="$AUTOPD &gt; 0">
					<xsl:value-of select="format-number(($AUTOPD div 1000),'###')" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<!-- Picking correct pd limit to genrate rates-->
		<xsl:variable name="PD">
			<xsl:choose>
				<xsl:when test="$UMBPDLIMIT != $XMLPD">
					<xsl:value-of select="$UMBPDLIMIT" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$XMLPD" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<!-- Inputxml CSL Limit  -->
		<xsl:variable name="XMLCSLLIMIT">
			<xsl:choose>
				<xsl:when test="normalize-space($AUTOCSL) = ''">0</xsl:when>
				<xsl:when test="$AUTOCSL &gt; 0">
					<xsl:value-of select="format-number(($AUTOCSL div 1000),'###')" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<!-- picking umbrella csllimit  -->
		<xsl:variable name="UMBCSLLIMIT">
			<xsl:variable name="AVUMBCSLLIMIT" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MAPING_COVERAGES']/NODE[@ID='MAPPING_AUTOMOBILE_CSL']/ATTRIBUTES[@TERRITORY = $TERRITORYS and @CSL_LIMIT_MIN &lt;= $XMLCSLLIMIT and @CSL_LIMIT_MAX &gt; $XMLCSLLIMIT]/@CSL_LIMIT_MIN" />
			<xsl:variable name="AVUMBCSLLIMITMIN" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MAPING_COVERAGES']/NODE[@ID='MAPPING_AUTOMOBILE_CSL']/ATTRIBUTES[@TERRITORY = $TERRITORYS]/@CSL_MIN" />
			<xsl:choose>
				<xsl:when test="$AVUMBCSLLIMIT !=''">
					<xsl:value-of select="$AVUMBCSLLIMIT" /> <!-- if xml limit is between available UMB limit-->
				</xsl:when>
				<xsl:when test="$XMLCSLLIMIT &lt; $AVUMBCSLLIMITMIN"> <!-- if xml limit is less than available minimum UMB limit-->
					<xsl:value-of select="$AVUMBCSLLIMITMIN" />
				</xsl:when>
				<xsl:otherwise> <!-- if xml limit is greater than highest available UMB limit-->
					<xsl:value-of select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MAPING_COVERAGES']/NODE[@ID='MAPPING_AUTOMOBILE_CSL']/ATTRIBUTES[@TERRITORY = $TERRITORYS ]/@CSL_MAX" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<!-- picking correct umbrella limit for genrating rates -->
		<xsl:variable name="CSLLIMIT">
			<xsl:choose>
				<xsl:when test="$UMBCSLLIMIT!=$XMLCSLLIMIT">
					<xsl:value-of select="$UMBCSLLIMIT" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$XMLCSLLIMIT" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="PERSONALAUTOPOLICYVALUE">
			<xsl:choose>
				<xsl:when test="normalize-space($PERSONALAUTOPOLICYLOWERLIMITS) != '' and  normalize-space($PERSONALAUTOPOLICYLOWERLIMITS) != '0' and normalize-space($PERSONALAUTOPOLICYUPPERLIMITS) != '' and  normalize-space($PERSONALAUTOPOLICYUPPERLIMITS) != '0'"> <!-- Split limit case -->
					<xsl:value-of select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGES']/NODE[@ID='AUTOMOBILE_BI']/ATTRIBUTES[@TERRITORY = $TERRITORYS and @LOWERLIMITBISPLIT = $BISPLITLOWERLIMIT and @UPPERLIMITBISPLIT = $BISPLITUPPERLIMIT and @PD_LIMIT = $PD ]/@VALUE" />
				</xsl:when>
				<xsl:when test="normalize-space($AUTOCSL) != '' and  normalize-space($AUTOCSL) != '0'"> <!-- CSL case -->
					<xsl:value-of select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGES']/NODE[@ID='AUTOMOBILE_CSL']/ATTRIBUTES[@TERRITORY = $TERRITORYS and @CSL_LIMIT = $CSLLIMIT]/@VALUE" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="var1">
			<xsl:choose>
				<xsl:when test="normalize-space($NUMBERAUTO) =''">0</xsl:when>
				<xsl:when test="$NUMBERAUTO &gt; 0">
					<xsl:choose>
						<xsl:when test="$VEHICLEDRIVERS &lt;= $NUMBERAUTO">
							<xsl:value-of select="round($PERSONALAUTOPOLICYVALUE * $VEHICLEDRIVERS)" />
						</xsl:when>
						<xsl:when test="$NUMBERAUTO &lt; $VEHICLEDRIVERS">
							<xsl:value-of select="round($PERSONALAUTOPOLICYVALUE * $NUMBERAUTO)" />
						</xsl:when>
						<xsl:otherwise>0</xsl:otherwise>
					</xsl:choose>
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="LIMIT_FACTOR">
			<xsl:call-template name="PREMIUM_FACTOR" />
		</xsl:variable>
		<xsl:value-of select="$var1 * $LIMIT_FACTOR" />
	</xsl:template>
	<!-- ********************************************************************************************** -->
	<!--					Templates is used to find the Premium of personal premises(Start)					-->
	<!-- ********************************************************************************************** -->
	<xsl:template name="PERSONAL_PREMISES_EXPOSURES">
		<xsl:variable name="UUMBRELLALIMIT" select="UUMBRELLA_LIMIT" />
		<xsl:variable name="TERRITORYS" select="TERRITORYCODES" />
		<xsl:variable name="var1">
			<xsl:value-of select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PREMIUM']/NODE[@ID='PERSONAL_PREMISES_EXPOSURES']/ATTRIBUTES[@TERRITORY = $TERRITORYS]/@VALUE" />
		</xsl:variable>
		<xsl:variable name="LIMIT_FACTOR">
			<xsl:call-template name="PREMIUM_FACTOR" />
		</xsl:variable>
		<xsl:value-of select="$var1 * $LIMIT_FACTOR" />
	</xsl:template>
	<!-- ********************************************************************************************** -->
	<!--					Templates is used to find the Premium of personal premises(End)				-->
	<!-- ********************************************************************************************** -->
	<!-- ============================================================================================== -->
	<!--							Charge for inexperienced driver	(Start)							    -->
	<!-- For each driver in the household under age 25 or each driver with less than three (3) 
			years driving experience.																	-->
	<!-- ============================================================================================== -->
	<xsl:template name="INEXPERIENCED_DRIVER_MOTORHOMES">
		<xsl:variable name="UUMBRELLALIMIT" select="UUMBRELLA_LIMIT" />
		<xsl:variable name="TERRITORYS" select="TERRITORYCODES" />
		<xsl:variable name="INEXPERIENCEDDRIVERCHARGE" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INEXPERIENCED_DRIVER']/NODE[@ID='UNDER_AGE_25']/ATTRIBUTES[@TERRITORY = $TERRITORYS]/@VALUE" />
		<xsl:variable name="INEXPDRIVERSMOTORHOMES" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/INEXPDRIVERSMOTORHOME" />
		<xsl:variable name="LIMIT_FACTOR">
			<xsl:call-template name="PREMIUM_FACTOR" />
		</xsl:variable>
		<!-- Calculations -->
		<xsl:variable name="var1">
			<xsl:choose>
				<xsl:when test="$INEXPDRIVERSMOTORHOMES !=''">
					<xsl:value-of select="round($INEXPERIENCEDDRIVERCHARGE * $INEXPDRIVERSMOTORHOMES)" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:value-of select="$var1 * $LIMIT_FACTOR" />
	</xsl:template>
	<xsl:template name="INEXPERIENCED_DRIVER_MOTORCYCLES">
		<xsl:variable name="UUMBRELLALIMIT" select="UUMBRELLA_LIMIT" />
		<xsl:variable name="TERRITORYS" select="TERRITORYCODES" />
		<xsl:variable name="INEXPDRIVERSMOTORCYCLS" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/INEXPDRIVERSMOTORCYCL" />
		<xsl:variable name="INEXPERIENCEDDRIVERS" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INEXPERIENCED_DRIVER']/NODE[@ID='UNDER_AGE_25']/ATTRIBUTES[@TERRITORY = $TERRITORYS]/@VALUE" />
		<xsl:variable name="LIMIT_FACTOR">
			<xsl:call-template name="PREMIUM_FACTOR" />
		</xsl:variable>
		<!-- Calculations -->
		<xsl:variable name="var1">
			<xsl:choose>
				<xsl:when test="$INEXPDRIVERSMOTORCYCLS !=''">
					<xsl:value-of select="round($INEXPERIENCEDDRIVERS * $INEXPDRIVERSMOTORCYCLS)" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:value-of select="$var1 * $LIMIT_FACTOR" />
	</xsl:template>
	<xsl:template name="INEXPERIENCED_DRIVER_AUTOS">
		<xsl:variable name="UUMBRELLALIMIT" select="UUMBRELLA_LIMIT" />
		<xsl:variable name="INEXPDRIVERSAUTOS" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/INEXPDRIVERSAUTO" />
		<xsl:variable name="TERRITORYS" select="TERRITORYCODES" />
		<xsl:variable name="INEXPERIENCEDDRIVERS" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INEXPERIENCED_DRIVER']/NODE[@ID='UNDER_AGE_25']/ATTRIBUTES[@TERRITORY = $TERRITORYS]/@VALUE" />
		<xsl:variable name="LIMIT_FACTOR">
			<xsl:call-template name="PREMIUM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="var1">
			<xsl:choose>
				<xsl:when test="$INEXPDRIVERSAUTOS !=''">
					<xsl:value-of select="round($INEXPERIENCEDDRIVERS * $INEXPDRIVERSAUTOS)" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:value-of select="$var1 * $LIMIT_FACTOR" />
	</xsl:template>
	<!-- ================================================================================================ -->
	<!--					Charge for inexperienced driver	[END]										  -->
	<!-- ================================================================================================ -->
	<xsl:template name="EACH_DEDUCTIBLE">
		<xsl:variable name="TERRITORY" select="TERRITORYCODES" />
		<xsl:value-of select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAS']/NODE[@ID='DEDUCTIBLE']/ATTRIBUTES[@TERRITORY = $TERRITORY]/@DEDUCTIBLES" />
	</xsl:template>
	<xsl:template name="WaterCraftEach">
		<xsl:for-each select="WATERCRAFT_EXPOSURES/WATERCRAFT[@ID &lt; 999]">
	- <xsl:value-of select="normalize-space(TYPE)" />(<xsl:value-of select="normalize-space(LENGTH)" />,<xsl:value-of select="normalize-space(RATEDSPEED)" />,<xsl:value-of select="normalize-space(HOESEPOWER)" />)
	</xsl:for-each>
	</xsl:template>
	<xsl:template name="WATERCRAFT_EXCLUSION">
		<xsl:variable name="WaterCraftlen" select="LENGTH" />
		<xsl:variable name="WaterCraftspd" select="RATEDSPEED" />
		<xsl:variable name="WaterCraftpwr" select="HOESEPOWER" />
		<xsl:for-each select="WATERCRAFT_EXPOSURES/WATERCRAFT[@ID &lt; 999]">
			<xsl:choose>
				<xsl:when test="($WaterCraftlen &gt; 40)or ($WaterCraftspd &gt; 65) or ($WaterCraftpwr &gt; 600)">
				Excluded
				</xsl:when>
				<xsl:otherwise>Included</xsl:otherwise>
			</xsl:choose>
		</xsl:for-each>
	</xsl:template>
	<!-- =========================================================================================== -->
	<!--				  Excess Uninsured/Underinsured Motorist Coverage	AUTO						-->
	<!-- =========================================================================================== -->
	<xsl:template name="UNINSURED_UNDERINSURED_MOTORIST_COVERAGE_AUTO">
		<xsl:choose>
			<xsl:when test="normalize-space(STATENAME)!='MICHIGAN'">
				<xsl:variable name="UUMBRELLALIMIT" select="UUMBRELLA_LIMIT" />
				<xsl:variable name="TERRITORYS" select="TERRITORYCODES" />
				<xsl:variable name="EXCLUDEUNINSMOTORIST" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/EXCLUDEUNINSMOTORIST" />
				<xsl:variable name="PAUTOMOBILES" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/AUTOMOBILES" />
				<xsl:variable name="PUNINSUREDMOTORIST_BI" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/UNINSINSMOTORISTLIMITBISPLIT" />
				<xsl:variable name="PUNINSUREDMOTORIST_CSL" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/UNDERINSMOTORISTLIMITCSL" />
				<xsl:variable name="UNINSMOTORISTVALUE" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGES']/NODE[@ID='UNINSURED_UNDERINSURED_MOTORIST_COVERAGE']/ATTRIBUTES[@TERRITORY = $TERRITORYS]/@VALUE" />
				<xsl:if test="normalize-space($PAUTOMOBILES) =''">0</xsl:if>
				<xsl:variable name="VAR_PREMIUM">
					<xsl:choose>
						<xsl:when test="normalize-space($EXCLUDEUNINSMOTORIST) = 'N' and ($PUNINSUREDMOTORIST_BI != 0 )">
							<xsl:value-of select="round($UNINSMOTORISTVALUE * $PAUTOMOBILES)" />
						</xsl:when>
						<xsl:otherwise>0</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:variable name="LIMIT_FACTOR">
					<xsl:call-template name="PREMIUM_FACTOR" />
				</xsl:variable>
				<xsl:value-of select="$VAR_PREMIUM * $LIMIT_FACTOR" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- =========================================================================================== -->
	<!--				  Excess Uninsured/Underinsured Motorist Coverage	MotorHome						-->
	<!-- =========================================================================================== -->
	<xsl:template name="UNINSURED_UNDERINSURED_MOTORIST_COVERAGE_MOTORHOME">
		<xsl:choose>
			<xsl:when test="normalize-space(STATENAME)!='MICHIGAN'">
				<xsl:variable name="UUMBRELLALIMIT" select="UUMBRELLA_LIMIT" />
				<xsl:variable name="TERRITORYS" select="TERRITORYCODES" />
				<xsl:variable name="EXCLUDEUNINSMOTORIST" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/EXCLUDEUNINSMOTORIST" />
				<xsl:variable name="PMOTOTHOMES" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/MOTOTHOMES" />
				<xsl:variable name="PUNINSUREDMOTORIST_BI" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/UNINSINSMOTORISTLIMITBISPLIT" />
				<xsl:variable name="PUNINSUREDMOTORIST_CSL" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/UNDERINSMOTORISTLIMITCSL" />
				<xsl:variable name="UNINSMOTORISTVALUE" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGES']/NODE[@ID='UNINSURED_UNDERINSURED_MOTORIST_COVERAGE']/ATTRIBUTES[@TERRITORY = $TERRITORYS]/@VALUE" />
				<xsl:if test="normalize-space($PMOTOTHOMES) =''">0</xsl:if>
				<xsl:variable name="VAR_PREMIUM">
					<xsl:choose>
						<xsl:when test="normalize-space($EXCLUDEUNINSMOTORIST) = 'N' and ($PUNINSUREDMOTORIST_BI != 0 )">
							<xsl:value-of select="round($UNINSMOTORISTVALUE * $PMOTOTHOMES)" />
						</xsl:when>
						<xsl:otherwise>0</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:variable name="LIMIT_FACTOR">
					<xsl:call-template name="PREMIUM_FACTOR" />
				</xsl:variable>
				<xsl:value-of select="$VAR_PREMIUM * $LIMIT_FACTOR" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- =========================================================================================== -->
	<!--				  Excess Uninsured/Underinsured Motorist Coverage	MtorCycle						-->
	<!-- =========================================================================================== -->
	<xsl:template name="UNINSURED_UNDERINSURED_MOTORIST_COVERAGE_MOTORCYCLE">
		<xsl:choose>
			<xsl:when test="normalize-space(STATENAME)='INDIANA'">
				<xsl:variable name="UUMBRELLALIMIT" select="UUMBRELLA_LIMIT" />
				<xsl:variable name="TERRITORYS" select="TERRITORYCODES" />
				<xsl:variable name="EXCLUDEUNINSMOTORIST" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/EXCLUDEUNINSMOTORIST" />
				<xsl:variable name="PMOTORCYCLES" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/MOTORCYCLES" />
				<xsl:variable name="PUNINSUREDMOTORIST_BI" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/UNINSINSMOTORISTLIMITBISPLIT" />
				<xsl:variable name="PUNINSUREDMOTORIST_CSL" select="PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/UNDERINSMOTORISTLIMITCSL" />
				<xsl:variable name="UNINSMOTORISTVALUE" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGES']/NODE[@ID='UNINSURED_UNDERINSURED_MOTORIST_COVERAGE']/ATTRIBUTES[@TERRITORY = $TERRITORYS]/@VALUE" />
				<xsl:if test="normalize-space($PMOTORCYCLES) =''">0</xsl:if>
				<xsl:variable name="VAR_PREMIUM">
					<xsl:choose>
						<xsl:when test="normalize-space($EXCLUDEUNINSMOTORIST) = 'N' and ($PUNINSUREDMOTORIST_BI != 0 )">
							<xsl:value-of select="round($UNINSMOTORISTVALUE * $PMOTORCYCLES)" />
						</xsl:when>
						<xsl:otherwise>0</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:variable name="LIMIT_FACTOR">
					<xsl:call-template name="PREMIUM_FACTOR" />
				</xsl:variable>
				<xsl:value-of select="$VAR_PREMIUM * $LIMIT_FACTOR" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							PREMIUM	 Factor												  -->
	<!-- ============================================================================================ -->
	<xsl:template name="PREMIUM_FACTOR">
		<xsl:variable name="UUMBRELLALIMIT" select="UUMBRELLA_LIMIT" />
		<xsl:value-of select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PREMIUM_FACTOR']/NODE[@ID='PER_PREMIUM_VALUE']/ATTRIBUTES[@UMBRELLA_LIMIT = $UUMBRELLALIMIT]/@FACTOR_VALUE" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							MINIMUM PREMIUM													  -->
	<!-- ============================================================================================ -->
	<xsl:template name="MINIMUM_PREMIUM">
		<xsl:variable name="TERRITORYS" select="TERRITORYCODES" />
		<xsl:variable name="UUMBRELLALIMIT" select="UUMBRELLA_LIMIT" />
		<xsl:value-of select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINIMUM_PREMIUM']/NODE[@ID='MINIMUM_POLICY_PREMIUM']/ATTRIBUTES[@TERRITORY = $TERRITORYS and @UMBRELLA_LIMIT = $UUMBRELLALIMIT]/@VALUE" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							MINIMUM PREMIUM	CALCULATION(START)												  -->
	<!-- ============================================================================================ -->
	<xsl:template name="CALL_ADJUSTEDBASE">
		<xsl:variable name="TERRITORYS" select="TERRITORYCODES" />
		<xsl:variable name="UUMBRELLALIMIT" select="UUMBRELLA_LIMIT" />
		<xsl:variable name="MINIMUMFACTOR">
			<xsl:call-template name="MINIMUM_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="FINALPREMIUMS">
			<xsl:call-template name="FINALPRIMUM_LESS_DISCOUNT" />
		</xsl:variable>
		<!-- return the minimum premium -->
		<xsl:choose>
			<xsl:when test="($FINALPREMIUMS &lt; $MINIMUMFACTOR)">
				<xsl:value-of select="round($MINIMUMFACTOR - $FINALPREMIUMS)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							MINIMUM PREMIUM	CALCULATION(END)												  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							For Additional Charges and Discount									-->
	<!-- ============================================================================================ -->
	<xsl:template name="ADDITIONAL_CHARGES_AND_DISCOUNTS">
		<xsl:variable name="MATUREDISCOUNTS" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/MATUREDISCOUNT" />
		<xsl:choose>
			<xsl:when test="normalize-space($MATUREDISCOUNTS) = 'Y'">
				<xsl:variable name="DISCOUNT" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAS']/NODE[@ID='MATURE_AGE_DISCOUNT']/ATTRIBUTES/@FACTOR" />
				<xsl:variable name="TOTALPREMIUM">
					<xsl:call-template name="FINALPRIMUM_LESS_DISCOUNT" />
				</xsl:variable>
				<xsl:variable name="FINALPREMIUM" select="round($DISCOUNT * $TOTALPREMIUM)" />
				<xsl:value-of select="round($TOTALPREMIUM - $FINALPREMIUM)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--============================================================================================ -->
	<!--									Final Primium Templates (START)   						  -->
	<!-- ============================================================================================ -->
	<xsl:template name="FINALPRIMUM_LESS_DISCOUNT">
		<xsl:variable name="var1">
			<xsl:call-template name="WATERCRAFT_EXPOSURES" />
		</xsl:variable>
		<xsl:variable name="var2">
			<xsl:call-template name="UNINSURED_UNDERINSURED_MOTORIST_COVERAGE_AUTO" />
		</xsl:variable>
		<xsl:variable name="var3">
			<xsl:call-template name="INEXPERIENCED_DRIVER_AUTOS" />
		</xsl:variable>
		<xsl:variable name="var4">
			<xsl:call-template name="INEXPERIENCED_DRIVER_MOTORCYCLES" />
		</xsl:variable>
		<xsl:variable name="var5">
			<xsl:call-template name="INEXPERIENCED_DRIVER_MOTORHOMES" />
		</xsl:variable>
		<xsl:variable name="var6">
			<xsl:call-template name="PERSONAL_PREMISES_EXPOSURES" />
		</xsl:variable>
		<xsl:variable name="var7">
			<xsl:call-template name="AUTOMOBILE" />
		</xsl:variable>
		<xsl:variable name="var8">
			<xsl:call-template name="MOTORHOMES" />
		</xsl:variable>
		<xsl:variable name="var9">
			<xsl:call-template name="MOTORCYCLE" />
		</xsl:variable>
		<xsl:variable name="var10">
			<xsl:call-template name="UNINSURED_UNDERINSURED_MOTORIST_COVERAGE_MOTORHOME" />
		</xsl:variable>
		<xsl:variable name="var11">
			<xsl:call-template name="UNINSURED_UNDERINSURED_MOTORIST_COVERAGE_MOTORCYCLE" />
		</xsl:variable>
		<xsl:value-of select="round($var1 + $var2 + $var3 + $var4 + $var5 + $var6 + $var7 + $var8 + $var9 + $var10 + $var11)" />
	</xsl:template>
	<!-- *************************************************************************************************************-->
	<xsl:template name="FINALPREMIUM_WITH_DISCOUNT">
		<xsl:variable name="MATUREDISCOUNTS" select="PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/MATUREDISCOUNT" />
		<xsl:choose>
			<xsl:when test="normalize-space($MATUREDISCOUNTS) = 'Y'">
				<xsl:variable name="DISCOUNT" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAS']/NODE[@ID='MATURE_AGE_DISCOUNT']/ATTRIBUTES/@FACTOR" />
				<xsl:variable name="TOTALPREMIUM">
					<xsl:call-template name="FINALPRIMUM_LESS_DISCOUNT" />
				</xsl:variable>
				<xsl:value-of select="round($DISCOUNT * $TOTALPREMIUM)" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="TOTALPREMIUM">
					<xsl:call-template name="FINALPRIMUM_LESS_DISCOUNT" />
				</xsl:variable>
				<xsl:value-of select="$TOTALPREMIUM" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ************************************************************************************************ -->
	<xsl:template name="FINALPREMIUM">
		<xsl:variable name="TERRITORYS" select="TERRITORYCODES" />
		<xsl:variable name="UUMBRELLALIMIT" select="UUMBRELLA_LIMIT" />
		<xsl:value-of select="UUMBRELLALIMIT" />
		<xsl:variable name="LIMIT_FACTOR">
			<xsl:call-template name="PREMIUM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="MINIMUMFACTOR" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINIMUM_PREMIUM']/NODE[@ID='MINIMUM_POLICY_PREMIUM']/ATTRIBUTES[@TERRITORY = $TERRITORYS and @UMBRELLA_LIMIT = $LIMIT_FACTOR]/@VALUE" />
		<xsl:variable name="FINALPREMIUMS">
			<xsl:call-template name="FINALPRIMUM_LESS_DISCOUNT" />
		</xsl:variable>
		<!-- return the minimum premium -->
		<xsl:choose>
			<xsl:when test="($FINALPREMIUMS &lt; $MINIMUMFACTOR)">
				(<xsl:call-template name="FINALPRIMUM_LESS_DISCOUNT" />)+   
				(<xsl:call-template name="CALL_ADJUSTEDBASE" />)
				
			</xsl:when>
			<xsl:otherwise>
				(<xsl:call-template name="FINALPREMIUM_WITH_DISCOUNT" />)+   
				(<xsl:call-template name="CALL_ADJUSTEDBASE" />)
				
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--									Final Primium Template (END)   							  -->
	<!-- ============================================================================================ -->
</xsl:stylesheet>
