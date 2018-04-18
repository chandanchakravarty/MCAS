<!-- ============================================================================================ 
	File Name		:	Primium.xsl																  
	Description		:	Generate the final premium for the Rental Dwelling 
	Developed By	:	Ashwani 
	Date			:   26 Oct.2005	
	Modified By		:	16 Nov 2005													  		
 ============================================================================================ -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:user="urn:user-namespace-here">
	<xsl:output encoding="ISO8859-1" omit-xml-declaration="no" method="xml" />
	<!-- ============================================================================================ -->
	<msxsl:script language="c#" implements-prefix="user">
<![CDATA[ 
		
		int ZONE_ID=2;
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
	<xsl:variable name="RDCoveragesDoc" select="document('FactorPath')"></xsl:variable>
	<!-- Lookup id 1203,1223 in masterlookup_values. these are distinct codes. 
	 Indiana and michigan states have same code for same policy types
	 ANY CHANGES HERE WILL NEED TO BE INCORPORATED IN RESP. FACTORMASTERPATH FILE TOO
	  -->
	<xsl:variable name="POLICYCODE_DP2REPAIR" select="'DP-2^REPAIR'" />
	<xsl:variable name="POLICYCODE_DP2REPLACE" select="'DP-2^REPLACE'" />
	<xsl:variable name="POLICYCODE_DP3PREMIER" select="'DP-3^PREMIER'" />
	<xsl:variable name="POLICYCODE_DP3REPAIR" select="'DP-3^REPAIR'" />
	<xsl:variable name="POLICYCODE_DP3REPLACE" select="'DP-3^REPLACE'" />
	<!-- Break up of the above -->
	<!-- Policy types -->
	<xsl:variable name="POLICYTYPE_DP2" select="'DP-2'" />
	<xsl:variable name="POLICYTYPE_DP3" select="'DP-3'" />
	<!-- to check ProductPremier node-->
	<xsl:variable name="POLICYDESC_REPAIR" select="'REPAIR'" />
	<xsl:variable name="POLICYDESC_REPLACEMENT" select="'REPLACE'" />
	<xsl:variable name="POLICYDESC_PREMIER" select="'PREMIER'" />
	<!-- ============================================================================================ 
								Loading ProductFactorMaster File (END)						  
 ============================================================================================ -->
	<xsl:template match="/">
		<xsl:apply-templates select="DWELLINGDETAILS" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--								DEWLLINGDETAILS Template(START)								  -->
	<!-- ============================================================================================ -->
	<xsl:template match="DWELLINGDETAILS">
		<PREMIUM>
			<CREATIONDATE></CREATIONDATE>
			<GETPATH>
				<PRODUCT PRODUCTID="0" DESC="Rental Dwelling">
					<GROUP GROUPID="0">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
					</GROUP>
					<GROUP GROUPID="1">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
					</GROUP>
					<GROUP GROUPID="2">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
					</GROUP>
					<GROUP GROUPID="3">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
					</GROUP>
					<GROUP GROUPID="4">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<!-- Call Adjusted Base -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="0">
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
								<STEP STEPID="1">
									<PATH>
						{
							'Included'		
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="2">
									<PATH>
						{
							'Included'
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="3">
									<PATH>
						{
							'Included'	
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<STEP STEPID="4">
								<PATH>
									<xsl:choose>
										<xsl:when test="PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT">DESCRIPTIONNOTREQUIRED</xsl:when>
										<xsl:otherwise>0</xsl:otherwise>
									</xsl:choose>
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Dwelling Under Construction -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="5">
									<PATH>
						{
							<xsl:call-template name="DUC_DISCOUNT_DISPLAY" />		
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Discount: Protective Device -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="6">
									<PATH>
						{
							<xsl:call-template name="PROTECTIVE_DEVICE_DISCOUNT_DISPLAY" />		
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Discount : Valued Customer -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="7">
									<PATH>
						 {
							<xsl:call-template name="VALUEDCUSTOMER_DISCOUNT_DISPLAY" />		
						 }
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Age of Home -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="8">
									<PATH>
						{
						 
						  <xsl:call-template name="AGEOFHOME_DISPLAY" />
							
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Discount : Multipolicy -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="9">
									<PATH>
						{
							<xsl:call-template name="MULTIPOLICY_DISPLAY" />			
						}	
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
					</GROUP>
					<GROUP GROUPID="5">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<!-- Land lord liablity -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="10">
									<PATH>
						{
							<xsl:call-template name="LP_124_LANDLORDS_LIABILITY" />
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- medical payements to others -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="11">
									<PATH>
						{
							<xsl:call-template name="MEDICAL_PAYMENTS_TO_OTHERS_EACH_PERSON_CREDITDISPLAY" />		
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Incidental office -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="12">
									<PATH>
						{
							<xsl:call-template name="INCIDENTALOFFICE_DISPLAY" />		
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
					</GROUP>
					<GROUP GROUPID="6">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<!-- Earth Quake -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="13">
									<PATH>
						 {
							  <xsl:call-template name="EARTHQUAKE" />	 
						 }
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Mines Subsidence -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="14">
									<PATH>
						{
							<xsl:call-template name="MINE_SUBSIDENCE_COVERAGE_PREMIUM" />	
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- COVERAGE:B - ADDITIONAL -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="15">
									<PATH>
						{
							<xsl:call-template name="COVERAGE_B_ADDITIONAL" />
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- BUILDING IMPROVEMENTS ADDITIONAL -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="16">
									<PATH>
						{
							<xsl:call-template name="BUILDING_IMPROVEMENTS_ADDITIONAL" />	
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- COVERAGE C ADDITIONAL -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="17">
									<PATH>
						{
							<xsl:call-template name="COVERAGE_C_ADDITIONAL" />
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- INCREASED COVERAGE D ADDITIONAL -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="18">
									<PATH>
						{
							<xsl:call-template name="INCREASED_COVERAGE_D_ADDITIONAL" />		
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- CONTENTS IN STORAGE ADDITIONAL -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="19">
									<PATH>
						{
							<xsl:call-template name="CONTENTS_IN_STORAGE_ADDITIONAL" />
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!--  TREES LAWNS AND SHRUBS ADDITIONAL -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="20">
									<PATH>
						{
							<xsl:call-template name="TREES_LAWNS_AND_SHRUBS_ADDITIONAL" />
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- RADIO AND TV EQUIPMENT ADDITIONAL -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="21">
									<PATH>
						{
							<xsl:call-template name="RADIO_AND_TV_EQUIPMENT_ADDITIONAL" />		
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- SATELLITE DISHES ADDITIONAL -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="22">
									<PATH>
						{
							<xsl:call-template name="SATELLITE_DISHES_ADDITIONAL" />		
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- AWNINGS AND CANOPIES ADDITIONAL -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="23">
									<PATH>
						{
							<xsl:call-template name="AWNINGS_AND_CANOPIES_ADDITIONAL" />		
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- INSTALLATION FLOATER IF 184 BUILDING MATERIALS -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="24">
									<PATH>
						{


						<xsl:call-template name="INSTALLATION_FLOATER_IF_184_BUILDING_MATERIALS" />		
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- INSTALLATION FLOATER IF 184 NON STRUCTURAL EQUIPMENT -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="25">
									<PATH>
						{
							<xsl:call-template name="INSTALLATION_FLOATER_IF_184_NON_STRUCTURAL_EQUIPMENT" />
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Limited Lead Liability (DP 392)  -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="26">
									<PATH>
						{
							<xsl:call-template name="LIMITED_LEAD_LIABILITY" />
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- PROPFEE -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="27">
									<PATH>
						{
							<xsl:call-template name="PROPFEE" />		
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Minimum  Premium -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="28">
									<PATH>
							{
							<xsl:call-template name="MINIMUM_PREMIUM" />
							}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Final Primium -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="29">
									<PATH>
						{
							<xsl:call-template name="FINALPRIMIUM" />		
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
					</GROUP>
				</PRODUCT>
			</GETPATH>
			<!--=========================================================================================================== 
									 CALCULATIONS PART 
============================================================================================================ -->
			<CALCULATION>
				<PRODUCT PRODUCTID="0">
					<xsl:attribute name="DESC">
						<xsl:call-template name="PRODUCTID0" />
					</xsl:attribute>
					<GROUP GROUPID="0" CALC_ID="10000" PREFIX="" SUFIX="" ISCALCULATE="F">
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
					</GROUP>
					<GROUP GROUPID="2" CALC_ID="10002" PREFIX="" SUFIX="" ISCALCULATE="F">
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
					<GROUP GROUPID="3" CALC_ID="10003" PREFIX="" SUFIX="" ISCALCULATE="F">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID3" />
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION />
						</GROUPCONDITION>
						<GROUPRULE />
						<GROUPFORMULA />
						<GDESC />
					</GROUP>
					<GROUP GROUPID="4" CALC_ID="10004" PREFIX="" SUFIX="" ISCALCULATE="F">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID4" />
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION />
						</GROUPCONDITION>
						<GROUPRULE />
						<GDESC />
						<!-- Coverage A - Dwelling  -->
						<STEP STEPID="0" CALC_ID="1000" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID0" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH>
								<xsl:value-of select="DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="DWELLING_LIMITS" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>COV_A</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="CALL_ADJUSTEDBASE" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'COV_A'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Coverage B - Other Structures -->
						<STEP STEPID="1" CALC_ID="1001" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID1" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH>
								<xsl:value-of select="DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="APPURTENANTSTRUCTURES_INCLUDE" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>COV_B</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<!--xsl:call-template name="COVERAGE_B_ADDITIONAL" /--></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'COV_B'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Coverage C - Landlords Personal Property -->
						<STEP STEPID="2" CALC_ID="1002" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID2" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH>
								<xsl:value-of select="DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="PERSONALPROPERTY_INCLUDE" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>COV_C</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<!--xsl:call-template name="COVERAGE_C_ADDITIONAL" /--></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'COV_C'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Coverage D - Rental Value -->
						<STEP STEPID="3" CALC_ID="1003" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID3" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH>
								<xsl:value-of select="DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="RENTALVALUE_INCLUDE" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>COV_D</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<!--xsl:call-template name="INCREASED_COVERAGE_D_ADDITIONAL" /--></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'COV_D'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--  Insured to replmnt cst  -->
						<STEP STEPID="4" CALC_ID="1004" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID4" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>
								<xsl:call-template name="COMPONENTTYPE" />
							</COMPONENT_TYPE>
							<COMPONENT_CODE>P_CENT_REPL_COST</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKREPLACEMENTCOST" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD>
								<xsl:call-template name="EXT_ADREPLACEMENTCOST" />
							</COM_EXT_AD>
						</STEP>
						<!-- Dwelling Under Construction -->
						<STEP STEPID="5" CALC_ID="1005" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID5" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_DUC</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKBUILDERSRISK" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD><xsl:call-template name="DUC_DISCOUNT_CREDIT" /><xsl:text>%</xsl:text></COM_EXT_AD>
						</STEP>
						<!-- Discount : Protective Device -->
						<STEP STEPID="6" CALC_ID="1006" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID6" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_PROT_DVC</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKPROTECTIVEDEVI" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD><xsl:call-template name="PROTECTIVE_DEVICE_DISCOUNT_CREDIT" /><xsl:text>%</xsl:text></COM_EXT_AD>
						</STEP>
						<!-- Discount : Valued Customer -->
						<STEP STEPID="7" CALC_ID="1007" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID7" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_VALUED_CST</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKVALUEDCUSTOMER" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD><xsl:call-template name="VALUEDCUSTOMER_DISCOUNT_CREDIT" /><xsl:text>%</xsl:text></COM_EXT_AD>
						</STEP>
						<!-- Discount Age of home -->
						<STEP STEPID="8" CALC_ID="1008" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID8" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_AGE_HOME</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKAGEOFHOME" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD><xsl:call-template name="AGEOFHOME_CREDIT" /><xsl:text>%</xsl:text></COM_EXT_AD>
						</STEP>
						<!-- Discount - Multi Policy -->
						<STEP STEPID="9" CALC_ID="1009" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID9" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_MULTIPOL</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKMULTIPOLICY" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD><xsl:call-template name="MULTIPOLICY_CREDIT" /><xsl:text>%</xsl:text></COM_EXT_AD>
						</STEP>
						<GROUPFORMULA>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1000]</OPERAND>
							<OPERATOR>*</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1001]</OPERAND>
							<OPERATOR>*</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1002]</OPERAND>
							<OPERATOR>*</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1003]</OPERAND>
							<OPERATOR>*</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1004]</OPERAND>
							<OPERATOR>*</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1005]</OPERAND>
						</GROUPFORMULA>
					</GROUP>
					<GROUP GROUPID="5" CALC_ID="10005" PREFIX="" SUFIX="" ISCALCULATE="F">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID5" />
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION />
						</GROUPCONDITION>
						<GROUPRULE />
						<GDESC />
						<!-- Liability - Each Occurrence -->
						<STEP STEPID="10" CALC_ID="10010" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID10" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:call-template name="LIABILITY_EACH_OCCURRENCE_DISPLAY" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>COV_E</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="LP_124_LANDLORDS_LIABILITY" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'COV_E'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<GROUPFORMULA>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1006]</OPERAND>
							<OPERATOR>*</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1007]</OPERAND>
							<OPERATOR>*</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1008]</OPERAND>
						</GROUPFORMULA>
						<!--  Medical Payments to Others - Each Person -->
						<STEP STEPID="11" CALC_ID="1011" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID11" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:call-template name="MEDICAL_PAYMENTS_TO_OTHERS_EACH_PERSON_DISPLAY" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>COV_F</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'COV_F'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- With Incidental Office Occupancy (By Insured Only) -->
						<STEP STEPID="12" CALC_ID="1012" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID12" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>INCI_OFCE</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'INCI_OFCE'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
					</GROUP>
					<GROUP GROUPID="6" CALC_ID="10006" PREFIX="" SUFIX="" ISCALCULATE="F">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID6" />
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION />
						</GROUPCONDITION>
						<GROUPRULE />
						<GDESC />
						<!-- DP-470 Earthquake  EARTHQUAKE_COVERAGE  -->
						<STEP STEPID="13" CALC_ID="1013" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID13" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH>
						<!-- Commented by Asfa (23-Jan-2008) - iTrack issue #3464 
								<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@ID = '1']/@DEDUCTIBLE_PERCENT" />%-$<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@ID = '1']/@MINIMUM" /> -->
								<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@ID = '1']/@DEDUCTIBLE_PERCENT" /><xsl:text>%</xsl:text>
					 		</D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>ERTHQKE</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="EARTHQUAKE" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'ERTHQKE'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Mine Subsidence Coverage -->
						<STEP STEPID="14" CALC_ID="1014" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID14" />
							</xsl:attribute>
							<PATH>P</PATH>
							<!-- Commented by Asfa (23-Jan-2008) - iTrack issue #3466 -->
							<!-- D_PATH >
								$<xsl:call-template name="MINE_DED_AMOUNT_DISPLAY" /--> <!--xsl:value-of select="MINESUBSIDENCEHO287_DED" /-->  <!-- -<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINE']/NODE[@ID ='MINE_PREMIUM']/@DEDUCTIBLE_PERCENT" />%
							</D_PATH>-->
							<D_PATH>
								<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINE']/NODE[@ID ='MINE_PREMIUM']/@DEDUCTIBLE_PERCENT" /><xsl:text>%</xsl:text>
							</D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MNE_SBS</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="MINE_SUBSIDENCE_COVERAGE_PREMIUM" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'MNE_SBS'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Increased Coverage B - Other Structures -->
						<STEP STEPID="15" CALC_ID="1015" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID15" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH>
								<xsl:value-of select="DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="APPURTENANTSTRUCTURES_ADDITIONAL" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>INCR_COV_B</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="INCREASED_COV_B" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'INCR_COV_B'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Building Improvements/Alteration -->
						<STEP STEPID="16" CALC_ID="1016" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID16" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH>
								<xsl:value-of select="DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="BUILDINGIMPROVEMENTS_ADDITIONAL" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BLDG_ALT</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="BUILDING_IMPROVEMENTS_ADDITIONAL" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'BLDG_ALT'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Increased Coverage C - Landlords Personal Property -->
						<STEP STEPID="17" CALC_ID="1017" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID17" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH>
								<xsl:value-of select="DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="PERSONALPROPERTY_ADDITIONAL" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>INCR_COV_C</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="COVERAGE_C_INCR" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'INCR_COV_C'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Increased Coverage D - Rental Value -->
						<STEP STEPID="18" CALC_ID="1018" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID18" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH>
								<xsl:value-of select="DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="RENTALVALUE_ADDITIONAL" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>INCR_COV_D</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="COVERAGE_D_INCR" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'INCR_COV_D'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Contents In Storage -->
						<STEP STEPID="19" CALC_ID="1019" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID19" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH>
								<xsl:value-of select="DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="CONTENTSINSTORAGE_ADDITIONAL" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>CNTNTS_STRG</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="CONTENTS_IN_STORAGE" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'CNTNTS_STRG'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Trees, Lawns, and Shrubs -->
						<STEP STEPID="20" CALC_ID="1020" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID20" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH>
								<xsl:value-of select="DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="TREESLAWNSSHRUBS_ADDITIONAL" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>TREES</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="TREES_LAWNS_AND_SHRUBS_ADDITIONAL" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'TREES'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Radio and TV Equipment -->
						<STEP STEPID="21" CALC_ID="1021" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID21" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH>
								<xsl:value-of select="DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="RADIOTV_ADDITIONAL" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>RADIO</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="RADIO_AND_TV_EQUIPMENT_ADDITIONAL" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'RADIO'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Satellite Dishes -->
						<STEP STEPID="22" CALC_ID="1022" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID22" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH>
								<xsl:value-of select="DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:call-template name="SATELLITEDISHES_COMBINE" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>SAT_DISH</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="SATELLITE_DISHES_ADDITIONAL" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'SAT_DISH'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Awnings and Canopies -->
						<STEP STEPID="23" CALC_ID="1023" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID23" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH>
								<xsl:value-of select="DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="AWNINGSCANOPIES_ADDITIONAL" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>AWNG</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="AWNINGS_AND_CANOPIES_ADDITIONAL" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'AWNG'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Installation Floater - Building Materials -->
						<STEP STEPID="24" CALC_ID="1024" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID24" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH>
								<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSTALLATIONFLOATER']/NODE[@ID ='INSTALLATION_FLOATER']/ATTRIBUTES/@DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="FLOATERBUILDINGMATERIALS_ADDITIONAL" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>INST_FLTR_BLDG</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="INSTALLATION_FLOATER_IF_184_BUILDING_MATERIALS" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'INST_FLTR_BLDG'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Installation Floater - Non-Structural Equipment -->
						<STEP STEPID="25" CALC_ID="1025" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID25" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH>
								<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSTALLATIONFLOATER']/NODE[@ID ='INSTALLATION_FLOATER']/ATTRIBUTES/@DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="FLOATERNONSTRUCTURAL_ADDITIONAL" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>INST_FLTR_NON_STR</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="INSTALLATION_FLOATER_IF_184_NON_STRUCTURAL_EQUIPMENT" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'INST_FLTR_NON_STR'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--Limited Lead Liability (DP 392)-->
						<STEP STEPID="26" CALC_ID="1026" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID26" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>LEAD_LIA</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="LIMITED_LEAD_LIABILITY" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:if test="STATENAME='MICHIGAN'">
									<xsl:call-template name="COMPONENT_CODE">
										<xsl:with-param name="FACTORELEMENT" select="'LEAD_LIA'"></xsl:with-param>
									</xsl:call-template>
								</xsl:if>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Property Expense Recoupment Charge -->
						<STEP STEPID="27" CALC_ID="1027" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID27" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>PRP_EXPNS_FEE</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="PROPFEE" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:if test="STATENAME='MICHIGAN'">
									<xsl:call-template name="COMPONENT_CODE">
										<xsl:with-param name="FACTORELEMENT" select="'PRP_EXPNS_FEE'"></xsl:with-param>
									</xsl:call-template>
								</xsl:if>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Minimum Premium -->
						<STEP STEPID="28" CALC_ID="1028" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID28" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MIN_PREMIUM</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="MINIMUM_PREMIUM" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Final Premium -->
						<STEP STEPID="29" CALC_ID="1029" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID29" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>SUMTOTAL</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="FINALPRIMIUM" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<GROUPFORMULA>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1009]</OPERAND>
							<OPERATOR>*</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1010]</OPERAND>
							<OPERATOR>*</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1011]</OPERAND>
							<OPERATOR>*</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1012]</OPERAND>
							<OPERATOR>*</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1013]</OPERAND>
							<OPERATOR>*</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1014]</OPERAND>
						</GROUPFORMULA>
					</GROUP>
					<PRODUCTFORMULA>
						<GROUP1 GROUPID="10004">
							<VALUE>(@[CALC_ID=10004])</VALUE>
							<OPERATOR>+</OPERATOR>
						</GROUP1>
						<GROUP1 GROUPID="10005">
							<VALUE>(@[CALC_ID=10005])</VALUE>
							<OPERATOR>+</OPERATOR>
						</GROUP1>
						<GROUP1 GROUPID="10006">
							<VALUE>(@[CALC_ID=10006])</VALUE>
							<OPERATOR></OPERATOR>
						</GROUP1>
					</PRODUCTFORMULA>
				</PRODUCT>
			</CALCULATION>
		</PREMIUM>
	</xsl:template>
	<!-- =========================================================================================================== 
								DEWLLINGDETAILS Template(END)								  
 ============================================================================================================== -->
	<!-- =========================================================================================================== 
								DEWLLINGDETAILS Discount text for split table(start)								  
 ============================================================================================================== -->
	<xsl:template name="REMARKBUILDERSRISK"><xsl:text>Discount - Builders Risk</xsl:text></xsl:template>
	<xsl:template name="REMARKPROTECTIVEDEVI"><xsl:text>Discount - Protective Devices</xsl:text></xsl:template>
	<xsl:template name="REMARKVALUEDCUSTOMER"><xsl:text>Discount - Valued Customer</xsl:text></xsl:template>
	<xsl:template name="REMARKAGEOFHOME"><xsl:text>Discount - Age of Home</xsl:text></xsl:template>
	<xsl:template name="REMARKMULTIPOLICY"><xsl:text>Discount - Multi Policy</xsl:text></xsl:template>
	<xsl:template name="REMARKREPLACEMENTCOST">
		<xsl:variable name="P_DWELLING_PERCENT" select="round((DWELLING_LIMITS div REPLACEMENTCOSTFACTOR)*100)" />
		<xsl:choose>
			<xsl:when test="$P_DWELLING_PERCENT &gt;=80 and $P_DWELLING_PERCENT &lt;90"><xsl:text>Insured to 80-89% of Replacement Cost</xsl:text></xsl:when>
			<xsl:when test="$P_DWELLING_PERCENT &gt;=90 and $P_DWELLING_PERCENT &lt;100"><xsl:text>Insured to 90-99% of Replacement Cost</xsl:text></xsl:when>
			<xsl:otherwise><xsl:text>Insured to 100% of Replacement Cost</xsl:text></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="EXT_ADREPLACEMENTCOST">
		<xsl:variable name="P_DWELLING_PERCENT" select="round((DWELLING_LIMITS div REPLACEMENTCOSTFACTOR)*100)" />
		<xsl:variable name="VAR_REPLACEMENTCOSTSURCHARGE">
			<xsl:call-template name="REPLACEMENTCOSTSURCHARGE" />
		</xsl:variable>
		<xsl:variable name="VAR_REPLACEMENTCASTFACTOR">
			<xsl:call-template name="REPLACEMENT_COST" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$P_DWELLING_PERCENT &gt;=80 and $P_DWELLING_PERCENT &lt;90">
				<xsl:choose>
					<xsl:when test="$VAR_REPLACEMENTCOSTSURCHARGE !='' and $VAR_REPLACEMENTCOSTSURCHARGE !='0'">
						<xsl:choose>
							<xsl:when test="$VAR_REPLACEMENTCASTFACTOR &gt;= '1'">+<xsl:value-of select="$VAR_REPLACEMENTCOSTSURCHARGE" /></xsl:when>
							<xsl:when test="$VAR_REPLACEMENTCASTFACTOR &lt; '1'">-<xsl:value-of select="$VAR_REPLACEMENTCOSTSURCHARGE" /></xsl:when>
							<xsl:otherwise></xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$P_DWELLING_PERCENT &gt;=90 and $P_DWELLING_PERCENT &lt;100">
				<xsl:choose>
					<xsl:when test="$VAR_REPLACEMENTCOSTSURCHARGE !='' and $VAR_REPLACEMENTCOSTSURCHARGE !='0'">
						<xsl:choose>
							<xsl:when test="$VAR_REPLACEMENTCASTFACTOR &gt;= '1'">+<xsl:value-of select="$VAR_REPLACEMENTCOSTSURCHARGE" /></xsl:when>
							<xsl:when test="$VAR_REPLACEMENTCASTFACTOR &lt; '1'">-<xsl:value-of select="$VAR_REPLACEMENTCOSTSURCHARGE" /></xsl:when>
							<xsl:otherwise></xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="$VAR_REPLACEMENTCOSTSURCHARGE !='' and $VAR_REPLACEMENTCOSTSURCHARGE !='0'">
						<xsl:choose>
							<xsl:when test="$VAR_REPLACEMENTCASTFACTOR &gt;= '1'">+<xsl:value-of select="$VAR_REPLACEMENTCOSTSURCHARGE" /></xsl:when>
							<xsl:when test="$VAR_REPLACEMENTCASTFACTOR &lt; '1'">-<xsl:value-of select="$VAR_REPLACEMENTCOSTSURCHARGE" /></xsl:when>
							<xsl:otherwise></xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- =========================================================================================================== 
								DEWLLINGDETAILS Discount text for split table(END)								  
 ============================================================================================================== -->
	<!-- ========================================================================================================= 
									Templates for Lables  (START)							  
 ============================================================================================================= -->
	<!-- ===============================   Group Details	====================================================== -->
	<xsl:template name="PRODUCTID0"><xsl:value-of select="PRODUCTNAME" /> - <xsl:value-of select="PRODUCT_PREMIER" /><xsl:text> (Territory : </xsl:text><xsl:value-of select="TERRITORYNAME" /><xsl:text>[</xsl:text><xsl:value-of select="TERRITORYCODES" /><xsl:text>])</xsl:text></xsl:template>
	<xsl:template name="GROUPID0"><xsl:text>Policy Form: </xsl:text><xsl:value-of select="normalize-space(PRODUCTNAME)" /> <xsl:choose>
			<xsl:when test="normalize-space(PRODUCT_PREMIER) =''"></xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="' '" />
				<xsl:value-of select="normalize-space(PRODUCT_PREMIER)" />
			</xsl:otherwise>
		</xsl:choose> , <xsl:value-of select="NUMBEROFFAMILIES" /><xsl:text>-Family</xsl:text></xsl:template>
	<xsl:template name="GROUPID1"><xsl:choose>
			<xsl:when test="SEASONALSECONDARY ='Y'"><xsl:text>Seasonal</xsl:text></xsl:when>
			<xsl:otherwise><xsl:text>Not Seasonal</xsl:text></xsl:otherwise>
		</xsl:choose>,<xsl:value-of select="' '" /> <xsl:value-of select="EXTERIOR_CONSTRUCTION_DESC" /><xsl:text>  (</xsl:text><xsl:value-of select="DOC" /><xsl:text>)</xsl:text></xsl:template>
	<xsl:template name="GROUPID2"><xsl:text>Fire Class: </xsl:text><xsl:value-of select="FIREPROTECTIONCLASS" /><xsl:text>, Hydrant: </xsl:text><xsl:value-of select="FEET2HYDRANT" /><xsl:text>, Fire Department: </xsl:text><xsl:value-of select="DISTANCET_FIRESTATION" /><xsl:text> miles</xsl:text></xsl:template>
	<xsl:template name="GROUPID3"><xsl:text>Premium Group: </xsl:text><xsl:call-template name="DWELLING_GROUPS" /><xsl:text> Rated Class: </xsl:text><xsl:value-of select="PROTECTIONCLASS" /></xsl:template>
	<xsl:template name="GROUPID4"><xsl:text>SECTION I - PROPERTY DAMAGE</xsl:text></xsl:template>
	<xsl:template name="GROUPID5"><xsl:text>SECTION II - LIABILITY COVERAGE (LP-124)</xsl:text></xsl:template>
	<xsl:template name="GROUPID6"><xsl:text>ADDITIONAL COVERAGES</xsl:text></xsl:template>
	<xsl:template name="GROUPID7"><xsl:text>Final Premium</xsl:text></xsl:template>
	<!--Step Details-->
	<xsl:template name="STEPID0"><xsl:text>		-  Coverage A - Dwelling</xsl:text></xsl:template>
	<xsl:template name="STEPID1"><xsl:text>		-  Coverage B - Other Structures</xsl:text></xsl:template>
	<xsl:template name="STEPID2"><xsl:text>		-  Coverage C - Landlords Personal Property</xsl:text></xsl:template>
	<xsl:template name="STEPID3"><xsl:text>		-  Coverage D - Rental Value</xsl:text></xsl:template>
	<!--xsl:template name="STEPID4">		-    Insured to <xsl:value-of select="round((DWELLING_LIMITS div REPLACEMENTCOSTFACTOR)*100)" />% of Replacement Cost ($<xsl:value-of select="format-number(REPLACEMENTCOSTFACTOR, '###,###')" />)</xsl:template-->
	<xsl:template name="STEPID5"><xsl:text>		-    Discount - Builders Risk </xsl:text><xsl:call-template name="DUC_DISCOUNT_CREDIT" /><xsl:text>%</xsl:text></xsl:template>
	<xsl:template name="STEPID6"><xsl:text>		-    Discount - Protective Devices </xsl:text><xsl:call-template name="PROTECTIVE_DEVICE_DISCOUNT_CREDIT" /><xsl:text>%</xsl:text></xsl:template>
	<xsl:template name="STEPID7"><xsl:text>		-    Discount - Valued Customer </xsl:text><xsl:call-template name="VALUEDCUSTOMER_DISCOUNT_CREDIT" /><xsl:text>%</xsl:text></xsl:template>
	<xsl:template name="STEPID8"><xsl:text>		-    Discount - Age of Home </xsl:text><xsl:call-template name="AGEOFHOME_CREDIT" /><xsl:text>%</xsl:text></xsl:template>
	<xsl:template name="STEPID9"><xsl:text>		-    Discount - Multi Policy </xsl:text><xsl:call-template name="MULTIPOLICY_CREDIT" /><xsl:text>%</xsl:text></xsl:template>
	<xsl:template name="STEPID10"><xsl:text>		- Liability - Each Occurrence</xsl:text></xsl:template>
	<xsl:template name="STEPID11"><xsl:text>		- Medical Payments to Others - Each Person</xsl:text></xsl:template>
	<xsl:template name="STEPID12"><xsl:text>	-  With Incidental Office Occupancy (By Insured Only)</xsl:text></xsl:template>
	<xsl:template name="STEPID13">	-  <!-- DP-470 Earthquake  --> <xsl:call-template name="EARTHQUAKE_COVERAGE" /><xsl:text> (Zone </xsl:text><xsl:value-of select="EARTHQUAKEZONE" /><xsl:text>) </xsl:text></xsl:template>
	<xsl:template name="STEPID14"><xsl:text>	-  Mine Subsidence Coverage</xsl:text></xsl:template>
	<xsl:template name="STEPID15"><xsl:text>	-  Increased Coverage B - Other Structures</xsl:text></xsl:template>
	<xsl:template name="STEPID16"><xsl:text>	-  Building Improvements/Alteration</xsl:text></xsl:template>
	<xsl:template name="STEPID17"><xsl:text>	-  Increased Coverage C - Landlords Personal Property</xsl:text></xsl:template>
	<xsl:template name="STEPID18"><xsl:text>	-  Increased Coverage D - Rental Value</xsl:text></xsl:template>
	<xsl:template name="STEPID19"><xsl:text>	-  Contents In Storage</xsl:text></xsl:template>
	<xsl:template name="STEPID20"><xsl:text>	-  Trees, Lawns, and Shrubs</xsl:text></xsl:template>
	<xsl:template name="STEPID21"><xsl:text>	-  Radio and TV Equipment</xsl:text></xsl:template>
	<xsl:template name="STEPID22"><xsl:text>	-  Satellite Dishes</xsl:text></xsl:template>
	<xsl:template name="STEPID23"><xsl:text>	-  Awnings and Canopies</xsl:text></xsl:template>
	<xsl:template name="STEPID24"><xsl:text>	-  Installation Floater - Building Materials</xsl:text></xsl:template>
	<xsl:template name="STEPID25"><xsl:text>	-  Installation Floater - Non-Structural Equipment</xsl:text></xsl:template>
	<xsl:template name="STEPID26"><xsl:text>	-  Limited Lead Liability (DP 392)</xsl:text></xsl:template>
	<xsl:template name="STEPID27"><xsl:text>	-  Property Expense Recoupment Charge</xsl:text></xsl:template>
	<xsl:template name="STEPID28"><xsl:text>Additional Premium charged to meet the minimum premium ($</xsl:text><xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINIMUM_RENTAL_PREMIUM']/NODE[@ID ='MINIMUM_PREMIUM']/ATTRIBUTES/@MINIMUM_VALUE" /><xsl:text>)</xsl:text></xsl:template>
	<xsl:template name="STEPID29"><xsl:text>Final Premium</xsl:text></xsl:template>
	<xsl:template name="REPLACEMENTCOSTSURCHARGE">
		<xsl:variable name="REPLACEMENTCASTFACTOR">
			<xsl:call-template name="REPLACEMENT_COST" />
		</xsl:variable>
		<xsl:variable name="VAR_REPLACEMENTCAST_PERCENT">
			<xsl:choose>
				<xsl:when test="$REPLACEMENTCASTFACTOR != '' and $REPLACEMENTCASTFACTOR &lt; 1.00">
					<xsl:value-of select="round((1 - $REPLACEMENTCASTFACTOR) * 100)" /><xsl:text>%</xsl:text>
				</xsl:when>
				<xsl:when test="$REPLACEMENTCASTFACTOR != '' and $REPLACEMENTCASTFACTOR &gt; 1.00">
					<xsl:value-of select="round(($REPLACEMENTCASTFACTOR -1 ) * 100)" /><xsl:text>%</xsl:text> 
				</xsl:when>
			</xsl:choose>
		</xsl:variable>
		<xsl:value-of select="$VAR_REPLACEMENTCAST_PERCENT" />
	</xsl:template>
	<xsl:template name="STEPID4">
		<xsl:variable name="P_DWELLING_PERCENT" select="round((DWELLING_LIMITS div REPLACEMENTCOSTFACTOR)*100)" />
		<xsl:variable name="VAR_REPLACEMENTCOSTSURCHARGE">
			<xsl:call-template name="REPLACEMENTCOSTSURCHARGE" />
		</xsl:variable>
		<xsl:variable name="VAR_REPLACEMENTCASTFACTOR">
			<xsl:call-template name="REPLACEMENT_COST" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$P_DWELLING_PERCENT &gt;=80 and $P_DWELLING_PERCENT &lt;90"><xsl:text>		-    Insured to 80-89% of Replacement Cost </xsl:text><xsl:choose>
					<xsl:when test="$VAR_REPLACEMENTCOSTSURCHARGE !='' and $VAR_REPLACEMENTCOSTSURCHARGE !='0'">
						<xsl:choose>
							<xsl:when test="$VAR_REPLACEMENTCASTFACTOR &gt;= '1'">+<xsl:value-of select="$VAR_REPLACEMENTCOSTSURCHARGE" /></xsl:when>
							<xsl:when test="$VAR_REPLACEMENTCASTFACTOR &lt; '1'">-<xsl:value-of select="$VAR_REPLACEMENTCOSTSURCHARGE" /></xsl:when>
							<xsl:otherwise></xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$P_DWELLING_PERCENT &gt;=90 and $P_DWELLING_PERCENT &lt;100"><xsl:text>		-    Insured to 90-99% of Replacement Cost </xsl:text><xsl:choose>
					<xsl:when test="$VAR_REPLACEMENTCOSTSURCHARGE !='' and $VAR_REPLACEMENTCOSTSURCHARGE !='0'">
						<xsl:choose>
							<xsl:when test="$VAR_REPLACEMENTCASTFACTOR &gt;= '1'">+<xsl:value-of select="$VAR_REPLACEMENTCOSTSURCHARGE" /></xsl:when>
							<xsl:when test="$VAR_REPLACEMENTCASTFACTOR &lt; '1'">-<xsl:value-of select="$VAR_REPLACEMENTCOSTSURCHARGE" /></xsl:when>
							<xsl:otherwise></xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise><xsl:text>		-    Insured to 100% of Replacement Cost </xsl:text><xsl:choose>
					<xsl:when test="$VAR_REPLACEMENTCOSTSURCHARGE !='' and $VAR_REPLACEMENTCOSTSURCHARGE !='0'">
						<xsl:choose>
							<xsl:when test="$VAR_REPLACEMENTCASTFACTOR &gt;= '1'">+<xsl:value-of select="$VAR_REPLACEMENTCOSTSURCHARGE" /></xsl:when>
							<xsl:when test="$VAR_REPLACEMENTCASTFACTOR &lt; '1'">-<xsl:value-of select="$VAR_REPLACEMENTCOSTSURCHARGE" /></xsl:when>
							<xsl:otherwise></xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="COMPONENTTYPE">
		<xsl:variable name="VAR_REPLACEMENTCASTFACTOR">
			<xsl:call-template name="REPLACEMENT_COST" />
		</xsl:variable>
		<xsl:variable name="VAR_REPLACEMENTCOSTSURCHARGE">
			<xsl:call-template name="REPLACEMENTCOSTSURCHARGE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$VAR_REPLACEMENTCOSTSURCHARGE !='' and $VAR_REPLACEMENTCOSTSURCHARGE !='0'">
				<xsl:choose>
					<xsl:when test="$VAR_REPLACEMENTCASTFACTOR &gt; 1">S</xsl:when>
					<xsl:otherwise>D</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--========================================================================================================== 
									Template for Lables  (END)								  
 ============================================================================================================== -->
	<!-- ============================================================================================ -->
	<!--								Base Premium Groups Template(START)								  -->
	<!--								  FOR MIcHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="DEWLLING-MAIN_MICHIGAN">
		<xsl:variable name="P_PRODUCT_PREMIER" select="PRODUCT_PREMIER"></xsl:variable>
		<xsl:variable name="PRODUCT_PREMIER_IN_CAPS" select="translate(translate($P_PRODUCT_PREMIER,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')" />
		<!-- Divert in case of Premier product -->
		<xsl:choose>
			<xsl:when test="$PRODUCT_PREMIER_IN_CAPS =$POLICYDESC_PREMIER">
				<xsl:call-template name="DEWLLING-MAIN_PREMIER_MICHIGAN" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="DEWLLING-MAIN" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- For DP3 - Premier -Michigan State-->
	<xsl:template name="DEWLLING-MAIN_PREMIER_MICHIGAN">
		<!-- Get the premium group -->
		<xsl:variable name="TERCODES" select="TERRITORYCODES" />
		<xsl:variable name="GROUP_ID" select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY']/NODE[@ID='TERRGROUPING']/ATTRIBUTES[@TID = $TERCODES]/@GROUPID" />
		<!-- Get the Form group -->
		<xsl:variable name="F_CODE" select="FORM_CODE" />
		<xsl:variable name="PCODE" select="PRODUCTNAME" />
		<xsl:variable name="FORMGROUP">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING']/ATTRIBUTES[@FORMID = $F_CODE]/@GROUPID" />
		</xsl:variable>
		<!-- Fetch the factor from the table -->
		<xsl:choose>
			<xsl:when test="$FORMGROUP ='1'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1" />
			</xsl:when>
			<xsl:when test="$FORMGROUP ='2'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code2" />
			</xsl:when>
			<xsl:when test="$FORMGROUP ='3'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code3" />
			</xsl:when>
			<xsl:when test="$FORMGROUP ='4'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code4" />
			</xsl:when>
			<xsl:when test="$FORMGROUP ='5'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code5" />
			</xsl:when>
			<xsl:when test="$FORMGROUP ='6'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code6" />
			</xsl:when>
			<xsl:when test="$FORMGROUP ='7'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code7" />
			</xsl:when>
			<xsl:when test="$FORMGROUP ='8'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code8" />
			</xsl:when>
			<xsl:when test="$FORMGROUP ='9'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code9" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- For all products of michigan other than DP3 - Premier -->
	<xsl:template name="DEWLLING-MAIN">
		<xsl:variable name="TERCODES" select="TERRITORYCODES" />
		<xsl:variable name="GROUP_ID" select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY']/NODE[@ID='TERRGROUPING']/ATTRIBUTES[@TID = $TERCODES]/@GROUPID" />
		<xsl:variable name="F_CODE" select="FORM_CODE" />
		<xsl:variable name="FORMGROUP">
			<xsl:choose>
				<xsl:when test="TERRITORYCODES = 1">
					<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING-1']/ATTRIBUTES[@FORMID = $F_CODE]/@GROUPID" />
				</xsl:when>
				<xsl:when test="TERRITORYCODES = 2">
					<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING-2']/ATTRIBUTES[@FORMID = $F_CODE]/@GROUPID" />
				</xsl:when>
				<xsl:otherwise>1</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="PCODE" select="PRODUCTNAME" />
		<xsl:choose>
			<xsl:when test="normalize-space(TERRITORYCODES) = '1'">
				<xsl:choose>
					<xsl:when test="$FORMGROUP = '1'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '2'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code=$GROUP_ID and @Form_Code=$PCODE]/@PrmGroup_Code2" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '3'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code3" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '4'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code4" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '5'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code5" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '6'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code6" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '7'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code7" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '8'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code8" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '9'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code9" />
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TERRITORYCODES = 2">
				<xsl:choose>
					<xsl:when test="$FORMGROUP = '10'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '11'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code2" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '12'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code3" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '13'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code4" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '14'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code5" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '15'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code6" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '16'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code7" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '17'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code8" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '18'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code9" />
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************** -->
	<!--New : Templates is used to find the PREMIER GROUPS According to the CLASS  Dispalyed on Header-->
	<xsl:template name="DWELLING_GROUPS">
		<xsl:variable name="TERCODES" select="TERRITORYCODES" />
		<xsl:variable name="GROUP_ID" select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY']/NODE[@ID='TERRGROUPING']/ATTRIBUTES[@TID = $TERCODES]/@GROUPID" />
		<xsl:variable name="F_CODE" select="FORM_CODE" />
		<xsl:variable name="FORMGROUP">
			<xsl:choose>
				<xsl:when test="TERRITORYCODES = 1">
					<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING-1']/ATTRIBUTES[@FORMID = $F_CODE]/@GROUPID" />
				</xsl:when>
				<xsl:when test="TERRITORYCODES = 2">
					<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING-2']/ATTRIBUTES[@FORMID = $F_CODE]/@GROUPID" />
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:value-of select="$FORMGROUP" />
	</xsl:template>
	<!--END New : Templates is used to find the PREMIER GROUPS According to the CLASS  Dispalyed on Header-->
	<xsl:template name="DEWLLING-MAIN_INDIANA">
		<xsl:variable name="TERCODES" select="TERRITORYCODES" />
		<xsl:variable name="GROUP_ID" select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY']/NODE[@ID='TERRGROUPING']/ATTRIBUTES[@TID = $TERCODES]/@GROUPID" />
		<xsl:variable name="F_CODE" select="FORM_CODE" />
		<xsl:variable name="FORMGROUP">
			<xsl:choose>
				<xsl:when test="TERRITORYCODES = 1">
					<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING-1']/ATTRIBUTES[@FORMID = $F_CODE]/@GROUPID" />
				</xsl:when>
				<xsl:when test="TERRITORYCODES = 2">
					<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING-2']/ATTRIBUTES[@FORMID = $F_CODE]/@GROUPID" />
				</xsl:when>
				<xsl:otherwise>1</xsl:otherwise> <!-- give territory 1 by default -->
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="PCODE" select="PRODUCTNAME" />
		<xsl:choose>
			<xsl:when test="TERRITORYCODES = 1">
				<xsl:choose>
					<xsl:when test="$FORMGROUP = '1'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '2'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code2" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '3'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code3" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '4'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code4" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '5'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code5" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '6'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code6" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '7'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code7" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '8'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code8" />
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TERRITORYCODES = 2">
				<xsl:choose>
					<xsl:when test="$FORMGROUP = '9'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '10'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code2" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '11'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code3" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '12'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code4" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '13'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code5" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '14'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code6" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '15'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code7" />
					</xsl:when>
					<xsl:when test="$FORMGROUP = '16'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code8" />
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="C_VALUE">
		<!-- Divide by 1000 because product factor master has values in 1000 -->
		<xsl:variable name="VAR_DWELLING_LIMITS" select="DWELLING_LIMITS" />
		<xsl:variable name="P_GROUP">
			<xsl:choose>
				<xsl:when test="contains(FORM_CODE,'M')">G1</xsl:when>
				<xsl:when test="contains(FORM_CODE,'F')">G2</xsl:when>
				<xsl:otherwise>G1</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="P_MIN_VALUE">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $P_GROUP]/@MINVALUE" />
		</xsl:variable>
		<xsl:variable name="P_DWELLING_LIMITS">
			<xsl:choose>
				<xsl:when test="$VAR_DWELLING_LIMITS &lt;= $P_MIN_VALUE">
					<xsl:value-of select="$P_MIN_VALUE" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$VAR_DWELLING_LIMITS" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="CVALUE" select='format-number(($P_DWELLING_LIMITS div 1000), "#")' /> <!-- converting to integer. -->
		<xsl:variable name="Var_CVALUE">
			<!-- get the max value of coverages in the database -->
			<xsl:variable name="P_MAX_VALUE">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $P_GROUP]/@MAXVALUE" />
			</xsl:variable>
			<xsl:choose>
				<!-- If the coverage selected is less than the max value then pick straight -->
				<xsl:when test="$CVALUE &lt;= $P_MAX_VALUE">
					<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $P_GROUP]/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
				</xsl:when>
				<!-- otherwise use the additional concept -->
				<xsl:otherwise>
					<!-- For each Additional amount-->
					<xsl:variable name="P_ADDITIONAL_VALUE">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $P_GROUP]/ATTRIBUTES[@CovA = 'ADDITIONAL']/@AMOUNT" />
					</xsl:variable>
					<!-- value -->
					<xsl:variable name="P_ADDITIONAL_FACTOR">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $P_GROUP]/ATTRIBUTES[@CovA = 'ADDITIONAL']/@Factor" />
					</xsl:variable>
					<!-- value for the max factor -->
					<xsl:variable name="P_MAX_VALUE_FACTOR">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $P_GROUP]/ATTRIBUTES[@CovA = $P_MAX_VALUE]/@Factor" />
					</xsl:variable>
					<!-- Calculations 
					e.g : factor for 250 + ((difference in the cov and the max value)/additional amount)* (factor for additional)-->
					<xsl:value-of select="($P_MAX_VALUE_FACTOR + ( (($CVALUE - $P_MAX_VALUE) div $P_ADDITIONAL_VALUE) * $P_ADDITIONAL_FACTOR ))" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$Var_CVALUE = '' or $Var_CVALUE ='0'">	 
			1
		</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$Var_CVALUE" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--								Base Premium Template(END)									  -->
	<!--								  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--					Templates for Replacement Cost [Factor,Credit,Display](SATRT)			  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- In the case of Replacement Cost depends on the percentage use -->
	<xsl:template name="REPLACEMENT_COST">
		<xsl:variable name="P_REPLACEMENT_COST">
			<xsl:value-of select="round((DWELLING_LIMITS div REPLACEMENTCOSTFACTOR)*100)" />
		</xsl:variable>
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='REPLACEMENT_COST_FACTOR']/NODE[@ID ='REPLACEMENT_COST']/ATTRIBUTES[@LOWER_PERCENTAGE_LIMIT &lt;= $P_REPLACEMENT_COST  and @UPPER_PERCENTAGE_LIMIT &gt;= $P_REPLACEMENT_COST]/@FACTOR" />
	</xsl:template>
	<!-- For Display Only -->
	<xsl:template name="REPLACEMENT_COST_DISPLAY">
	<!--Applied-->
	0
</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Templates for Replacement Cost [Factor,Credit,Display](end)				  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="TERM_FACTOR">
		<!-- Not Less Than Annual on Builders Risk). If dwelling under construction then months=12 by default-->
		<xsl:variable name="PTERM" select="TERMFACTOR" />
		<xsl:choose>
			<xsl:when test="DWELL_UND_CONSTRUCTION_DP1143 ='Y'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERM_FACTOR']/NODE[@ID ='TERMFACTOR']/ATTRIBUTES[@MONTHS=12]/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERM_FACTOR']/NODE[@ID ='TERMFACTOR']/ATTRIBUTES[@MONTHS=$PTERM]/@FACTOR" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="NO_OF_FAMILY_UNITS_DWELLINGS_FACTOR">
		<!-- No. of Families Units In Dwelling 
			Units Primary Seasonal
			1-Family 1.000 1.100
			2-Family 1.100 1.210  -->
		<xsl:variable name="NO_OF_FAMILIES" select="NUMBEROFFAMILIES" />
		<xsl:choose>
			<xsl:when test="$NO_OF_FAMILIES ='' or $NO_OF_FAMILIES ='0'">
			1.00
		</xsl:when>
			<xsl:when test="$NO_OF_FAMILIES &gt; 0">
				<!-- we assume that if the number of families is greater than 2 ,the input xml sends 2-->
				<xsl:choose>
					<xsl:when test="SEASONALSECONDARY ='Y'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGES']/NODE[@ID ='FAMILYUNITSINDWELL']/ATTRIBUTES[@UNITS=$NO_OF_FAMILIES]/@SEASONAL" />
					</xsl:when>
					<xsl:when test="SEASONALSECONDARY ='N'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGES']/NODE[@ID ='FAMILYUNITSINDWELL']/ATTRIBUTES[@UNITS=$NO_OF_FAMILIES]/@PRIMARY" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGES']/NODE[@ID ='FAMILYUNITSINDWELL']/ATTRIBUTES[@UNITS=$NO_OF_FAMILIES]/@PRIMARY" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--						Templates for Discount - Deductible Factor (START)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="DFACTOR">
		<xsl:variable name="DEDUCTIBLEAMT" select="DEDUCTIBLE" />
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLES']/NODE[@ID ='OPTIONALDEDUCTIBLE']/ATTRIBUTES[@AMOUNT=$DEDUCTIBLEAMT]/@FACTOR" />
		<!--xsl:choose>
			<xsl:when test="(DEDUCTIBLE &gt;= 500 and STATENAME ='MICHIGAN')">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLES']/NODE[@ID ='OPTIONALDEDUCTIBLE']/ATTRIBUTES[@AMOUNT=$DEDUCTIBLEAMT]/@FACTOR" />
			</xsl:when>
			<xsl:when test="STATENAME ='INDIANA'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLES']/NODE[@ID ='OPTIONALDEDUCTIBLE']/ATTRIBUTES[@AMOUNT=$DEDUCTIBLEAMT]/@FACTOR" />			
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose-->
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--						Templates for Discount - Deductible Factor (END)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Age Of Home Factor  [Factor,Credit,Display]  (SATRT)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="AGEOFHOME_FACTOR">
		<xsl:variable name="MAX_AGE_LIMIT">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/@MAX_AGE_LIMIT" />
		</xsl:variable>
		<xsl:variable name="AGE_OF_HOME" select="AGEOFHOME" />
		<xsl:choose>
			<xsl:when test="(DWELL_UND_CONSTRUCTION_DP1143 !='Y') and ($AGE_OF_HOME &lt;= $MAX_AGE_LIMIT)">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/ATTRIBUTES[@AGE = $AGE_OF_HOME]/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>
		1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Age Of Home CREDIT -->
	<xsl:template name="AGEOFHOME_CREDIT">
		<xsl:variable name="MAX_AGE_LIMIT">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/@MAX_AGE_LIMIT" />
		</xsl:variable>
		<xsl:variable name="AGE_OF_HOME" select="AGEOFHOME" />
		<xsl:choose>
			<xsl:when test="(DWELL_UND_CONSTRUCTION_DP1143 ='N') and ($AGE_OF_HOME &lt;= $MAX_AGE_LIMIT)">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/ATTRIBUTES[@AGE = $AGE_OF_HOME]/@CREDIT" />
			</xsl:when>
			<xsl:otherwise>
			0
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Age Of Home Display -->
	<xsl:template name="AGEOFHOME_DISPLAY">
		<xsl:variable name="MAX_AGE_LIMIT">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/@MAX_AGE_LIMIT" />
		</xsl:variable>
		<xsl:variable name="AGE_OF_HOME" select="AGEOFHOME" />
		<xsl:choose>
			<xsl:when test="(DWELL_UND_CONSTRUCTION_DP1143 ='N') and ($AGE_OF_HOME &lt;= $MAX_AGE_LIMIT)">
			'Applied'		
		</xsl:when>
			<xsl:otherwise>
		0
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Age Of Home   [Factor,Credit,Display]  (END)				  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Protective Devices  [Factor,Credit,Display]  (START)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="PROTECTIVE_DEVICE_DISCOUNT_FACTOR">
		<!-- This discount is applicable only when the dwelling is not under construction -->
		<xsl:choose>
			<xsl:when test="N0_LOCAL_ALARM = 1 and DWELL_UND_CONSTRUCTION_DP1143 = 'N'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='PROTECTIVEDEVICES']/ATTRIBUTES[@ID = 'OLSA']/@FACTOR" />
			</xsl:when>
			<xsl:when test="N0_LOCAL_ALARM &gt; 1 and DWELL_UND_CONSTRUCTION_DP1143 = 'N'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='PROTECTIVEDEVICES']/ATTRIBUTES[@ID = 'LFA']/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>
		1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PROTECTIVE_DEVICE_DISCOUNT_CREDIT">
		<xsl:choose>
			<xsl:when test="N0_LOCAL_ALARM = 1 and DWELL_UND_CONSTRUCTION_DP1143 = 'N'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='PROTECTIVEDEVICES']/ATTRIBUTES[@ID = 'OLSA']/@CREDIT" />
			</xsl:when>
			<xsl:when test="N0_LOCAL_ALARM &gt; 1 and DWELL_UND_CONSTRUCTION_DP1143 = 'N'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='PROTECTIVEDEVICES']/ATTRIBUTES[@ID = 'LFA']/@CREDIT" />
			</xsl:when>
			<xsl:otherwise>
		0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PROTECTIVE_DEVICE_DISCOUNT_DISPLAY">
		<xsl:choose>
			<xsl:when test="N0_LOCAL_ALARM = 1 and DWELL_UND_CONSTRUCTION_DP1143 = 'N'">
			'Applied'
		</xsl:when>
			<xsl:when test="N0_LOCAL_ALARM &gt; 1 and DWELL_UND_CONSTRUCTION_DP1143 = 'N'">
			'Applied'
		</xsl:when>
			<xsl:otherwise>
			0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Protective Devices  [Factor,Credit,Display]  (END)			  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--	Templates for Discount -  Discount - Under Construction [Factor,Credit,Display](START)	  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="DUC_DISCOUNT">
		<xsl:choose>
			<xsl:when test="DWELL_UND_CONSTRUCTION_DP1143 = 'Y'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DUC']/NODE[@ID ='DUC_CREDIT']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DUC_DISCOUNT_DISPLAY">
		<xsl:choose>
			<xsl:when test="DWELL_UND_CONSTRUCTION_DP1143 = 'Y'">
		'Applied'
		</xsl:when>
			<xsl:otherwise>
		0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DUC_DISCOUNT_CREDIT">
		<xsl:choose>
			<xsl:when test="DWELL_UND_CONSTRUCTION_DP1143 = 'Y'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DUC']/NODE[@ID ='DUC_CREDIT']/ATTRIBUTES/@CREDIT" />
			</xsl:when>
			<xsl:otherwise>
		0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--	Templates for Discount -  Discount - Under Construction [Factor,Credit,Display](END)	  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Multi policy Factor [Factor,Credit,Display]  (START)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="MULTIPOLICY_DISCOUNT_FACTOR">
		<xsl:choose>
			<xsl:when test="MULTIPLEPOLICYFACTOR = 'Y'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MULTIPOLICY']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise> 
		1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Multipolicy CREDIT -->
	<xsl:template name="MULTIPOLICY_CREDIT">
		<xsl:choose>
			<xsl:when test="MULTIPLEPOLICYFACTOR = 'Y'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MULTIPOLICY']/ATTRIBUTES/@CREDIT" />
			</xsl:when>
			<xsl:otherwise>
			0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Multipolicy Display -->
	<xsl:template name="MULTIPOLICY_DISPLAY">
		<xsl:choose>
			<xsl:when test="MULTIPLEPOLICYFACTOR = 'Y'">
		'Applied'
		</xsl:when>
			<xsl:otherwise>
		0
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Multipolicy Factor [Factor,Credit,Display]  (END)			  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--	   Templates for Liability - Each Occurrence - Each Person [Factor,Credit,Display] START) -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="LIABILITY_EACH_OCCURRENCE_DISPLAY">
		<xsl:choose>
			<xsl:when test="PERSONALLIABILITY_LIMIT = 'No Coverage' or PERSONALLIABILITY_LIMIT = 'NO COVERAGE'">0</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="PERSONALLIABILITY_LIMIT" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ####################################### landlord liability #################################-->
	<xsl:template name="LP_124_LANDLORDS_LIABILITY">
		<xsl:variable name="P_PERSONALLIABILITY_LIMIT">
			<xsl:choose>
				<xsl:when test="PERSONALLIABILITY_LIMIT = 'No Coverage' or PERSONALLIABILITY_LIMIT = 'NO COVERAGE'">0</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="PERSONALLIABILITY_LIMIT" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="P_MEDICALPAYMENTSTOOTHERS_LIMIT">
			<xsl:choose>
				<xsl:when test="MEDICALPAYMENTSTOOTHERS_LIMIT = 'No Coverage' or MEDICALPAYMENTSTOOTHERS_LIMIT = 'NO COVERAGE'">0</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="MEDICALPAYMENTSTOOTHERS_LIMIT" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$P_PERSONALLIABILITY_LIMIT &gt; 0 and $P_MEDICALPAYMENTSTOOTHERS_LIMIT &gt; 0">
				<xsl:variable name="P_NUMBER_OF_FAMILIES" select="NUMBEROFFAMILIES" />
				<!-- Each Addition Medical Payments -->
				<xsl:variable name="P_FOR_EACH_ADDITIONAL_MED_PAY">
					<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDS_LIABILITY']/NODE[@ID='LANDLORDS_LIABILITY_LIMIT']/@FOR_EACH_ADDITIONAL_MEDICAL" />
				</xsl:variable>
				<!-- Value for single family -->
				<xsl:variable name="P_1_FAMILY">
					<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDS_LIABILITY']/NODE[@ID='LANDLORDS_LIABILITY_LIMIT']/ATTRIBUTES[@LIABILITY_LIMIT = $P_PERSONALLIABILITY_LIMIT]/@MED_PAY_1_FAMILY" />
				</xsl:variable>
				<xsl:variable name="P_1_FAMILY_MED_PAY_LIMIT">
					<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDS_LIABILITY']/NODE[@ID='LANDLORDS_LIABILITY_LIMIT']/ATTRIBUTES[@LIABILITY_LIMIT = $P_PERSONALLIABILITY_LIMIT]/@MED_PAY_LIMIT" />
				</xsl:variable>
				<!-- Additional Medical for 1 family -->
				<xsl:variable name="P_ADDITIONAL_1_FAMILY">
					<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDS_LIABILITY']/NODE[@ID='LANDLORDS_LIABILITY_LIMIT']/ATTRIBUTES[@LIABILITY_LIMIT = $P_PERSONALLIABILITY_LIMIT]/@ADDITIONAL_1_FAMILY" />
				</xsl:variable>
				<!-- Medical for each additional family (over 1 family)-->
				<xsl:variable name="P_EACH_ADDL_FAMILY">
					<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDS_LIABILITY']/NODE[@ID='LANDLORDS_LIABILITY_LIMIT']/ATTRIBUTES[@LIABILITY_LIMIT = $P_PERSONALLIABILITY_LIMIT]/@EACH_ADDL_FAMILY" />
				</xsl:variable>
				<xsl:variable name="P_EACH_ADDL_FAMILY_MED_PAY_LIMIT">
					<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDS_LIABILITY']/NODE[@ID='LANDLORDS_LIABILITY_LIMIT']/ATTRIBUTES[@LIABILITY_LIMIT = $P_PERSONALLIABILITY_LIMIT]/@MED_PAY_LIMIT" />
				</xsl:variable>
				<!-- Additional Medical for each additional family (over 1 family)-->
				<xsl:variable name="P_ADDITIONAL_EACH_ADDL_FAMILY">
					<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDS_LIABILITY']/NODE[@ID='LANDLORDS_LIABILITY_LIMIT']/ATTRIBUTES[@LIABILITY_LIMIT = $P_PERSONALLIABILITY_LIMIT]/@ADDITIONAL_EACH_ADDL_FAMILY" />
				</xsl:variable>
				<!-- main formula -->
				<!-- 
					1. Fetch the Liability rates for 1Family against the liability limit and medical payment limit =1000
					2. For the first family, the additional medical payment limit per thousand will be added.
					3. From second family onwards, the additional medical payment limit per thousand for each addl family will be added. 
					4. Multiply by Term Factor
				-->
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="FINAL_VALUE">
					<!-- %%%%%%%%%%%%% -->
					<xsl:variable name="V1" select="$P_1_FAMILY" />
					<xsl:variable name="V2" select="$P_MEDICALPAYMENTSTOOTHERS_LIMIT" />
					<xsl:variable name="V3" select="$P_1_FAMILY_MED_PAY_LIMIT" />
					<xsl:variable name="V4" select="$P_FOR_EACH_ADDITIONAL_MED_PAY" />
					<xsl:variable name="V5" select="$P_ADDITIONAL_1_FAMILY" />
					<xsl:variable name="V6" select="$P_NUMBER_OF_FAMILIES - 1" />
					<xsl:variable name="V7" select="$P_EACH_ADDL_FAMILY" />
					<xsl:variable name="V8" select="$P_MEDICALPAYMENTSTOOTHERS_LIMIT" />
					<xsl:variable name="V9" select="$P_EACH_ADDL_FAMILY_MED_PAY_LIMIT" />
					<xsl:variable name="V10" select="$P_FOR_EACH_ADDITIONAL_MED_PAY" />
					<xsl:variable name="V11" select="$P_ADDITIONAL_EACH_ADDL_FAMILY" />
					<xsl:variable name="V12" select="$VAR_TERMFACTOR" />
					<xsl:value-of select="round(($V1 + ((($V2 - $V3) div $V4 ) * $V5)+ ($V6 * ( $V7   + ((($V8 - $V9) div $V10) * $V11))))*$V12)" />
				</xsl:variable>
				<xsl:value-of select="$FINAL_VALUE" />
			</xsl:when>
			<xsl:otherwise>
		0		
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--	   Templates for Liability - Each Occurrence - Each Person [Factor,Credit,Display] END)   -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for Medical Payments to Others - Each Person [Factor,Credit,Display] START) -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="MEDICAL_PAYMENTS_TO_OTHERS_EACH_PERSON_DISPLAY">
		<xsl:choose>
			<xsl:when test="MEDICALPAYMENTSTOOTHERS_LIMIT = 'No Coverage' or MEDICALPAYMENTSTOOTHERS_LIMIT = 'NO COVERAGE'">0</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="MEDICALPAYMENTSTOOTHERS_LIMIT" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MEDICAL_PAYMENTS_TO_OTHERS_EACH_PERSON_CREDITDISPLAY">
		<!--
IF(<xsl:call-template name="LIABILITY_EACH_OCCURRENCE_DISPLAY"/> &gt; 1)THEN
	<xsl:choose>
		<xsl:when test ="MEDICALPAYMENTSTOOTHERS_LIMIT = 'No Coverage' or MEDICALPAYMENTSTOOTHERS_LIMIT = 'NO COVERAGE'">0</xsl:when>
		<xsl:otherwise>
			'Included'
		</xsl:otherwise>
	</xsl:choose>
ELSE
	0
-->
		<xsl:variable name="VAR1">
			<xsl:call-template name="LIABILITY_EACH_OCCURRENCE_DISPLAY" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$VAR1 &gt; 1">
				<xsl:choose>
					<xsl:when test="MEDICALPAYMENTSTOOTHERS_LIMIT = 'No Coverage' or MEDICALPAYMENTSTOOTHERS_LIMIT = 'NO COVERAGE'">
					0
				</xsl:when>
					<xsl:otherwise>
					'Included'
				</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Medical Payments to Others - Each Person [Factor,Credit,Display]  (END) -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for With Incidental Office Occupancy (By Insured Only) [Display]  (START)  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="INCIDENTALOFFICE_DISPLAY">
		<xsl:choose>
			<xsl:when test="INCIDENTALOFFICE = 'Y'">
		'Included'
		</xsl:when>
			<xsl:otherwise>
		0
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for With Incidental Office Occupancy (By Insured Only) [Display]  (END)	  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--			Templates for Discount - VALUED CUSTOMER [Factor,Credit,Display]  (START)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- The amount of discount depends on the number of consecutive years,
	 ending with the current renewal date, that the policy has been with Wolverine Mutual. The discount is
	 applied per policy and per location at the beginning of the third policy year continuously inforce. Any loss
	 last 12 months reduces discount. -->
	<!-- ============================================================================================ -->
	<xsl:template name="VALUEDCUSTOMER_DISCOUNT_MICHIGAN">
		<xsl:variable name="P_NO_YEARS_WITH_WOLVERINE" select="NO_YEARS_WITH_WOLVERINE" />
		<!-- Check if it is loss free or nt loss free -->
		<xsl:choose>
			<xsl:when test="normalize-space(LOSSFREE) = normalize-space('Y')">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@MINRENEWALDURATION &lt;= $P_NO_YEARS_WITH_WOLVERINE and @MAXRENEWALDURATION &gt;= $P_NO_YEARS_WITH_WOLVERINE]/@LOSSFREEFACTOR" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@MINRENEWALDURATION &lt;= $P_NO_YEARS_WITH_WOLVERINE and @MAXRENEWALDURATION &gt;= $P_NO_YEARS_WITH_WOLVERINE]/@LOSSPRIOR12MONTHSFACTOR" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="VALUEDCUSTOMER_DISCOUNT_CREDIT_MICHIGAN">
		<xsl:variable name="P_NO_YEARS_WITH_WOLVERINE" select="NO_YEARS_WITH_WOLVERINE" />
		<!-- Check if it is loss free or nt loss free -->
		<xsl:choose>
			<xsl:when test="normalize-space(LOSSFREE) =normalize-space('Y')">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@MINRENEWALDURATION &lt;= $P_NO_YEARS_WITH_WOLVERINE and @MAXRENEWALDURATION &gt;= $P_NO_YEARS_WITH_WOLVERINE]/@LOSSFREECREDIT" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@MINRENEWALDURATION &lt;= $P_NO_YEARS_WITH_WOLVERINE and @MAXRENEWALDURATION &gt;= $P_NO_YEARS_WITH_WOLVERINE]/@LOSSPRIOR12MONTHSCREDIT" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="VALUEDCUSTOMER_DISCOUNT_DISPLAY_MICHIGAN">
		<!-- 
	1. It is applicable for Renewal case only
	2. Year to be taken (in case of input xml) will be current effective date minus the inception date 
	-->
		<xsl:choose>
			<xsl:when test="NO_YEARS_WITH_WOLVERINE ='' or NO_YEARS_WITH_WOLVERINE &lt;= 1">
					0
				</xsl:when>
			<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 1">
					'Applied'
				</xsl:when>
			<xsl:otherwise>
					0
				</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- For INDIANA -->
	<xsl:template name="VALUEDCUSTOMER_DISCOUNT_DISPLAY_INDIANA">
		<!-- 
		1. It is applicable for Renewal case only
		2. Year to be taken (in case of input xml) will be current effective date minus the inception date 
	-->
		<xsl:choose>
			<xsl:when test="NO_YEARS_WITH_WOLVERINE &lt;= 1">
					0
				</xsl:when>
			<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 1 and NO_YEARS_WITH_WOLVERINE &lt;= 4 and LOSSFREE = 'N'">
					'Applied'
				</xsl:when>
			<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 1 and NO_YEARS_WITH_WOLVERINE &lt;= 4 and LOSSFREE = 'Y'">
					'Applied'
				</xsl:when>
			<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 4 and LOSSFREE = 'N'">
					'Applied'
				</xsl:when>
			<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 4 and LOSSFREE = 'Y'">
					'Applied'
				</xsl:when>
			<xsl:otherwise>
					0
				</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="VALUEDCUSTOMER_DISCOUNT_INDIANA">
		<!-- 
		1. It is applicable for Renewal case only
		2. Year to be taken (in case of input xml) will be current effective date minus the inception date 
	-->
		<xsl:variable name="P_NO_YEARS_WITH_WOLVERINE" select="NO_YEARS_WITH_WOLVERINE" />
		<xsl:choose>
			<xsl:when test="normalize-space(LOSSFREE) = 'Y'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@MINRENEWALDURATION &lt;= $P_NO_YEARS_WITH_WOLVERINE and @MAXRENEWALDURATION &gt;= $P_NO_YEARS_WITH_WOLVERINE]/@NO_LOSS_FACTOR" />
			</xsl:when>
			<xsl:when test="(LOSSFREE = '' or normalize-space(LOSSFREE)='N')">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@MINRENEWALDURATION &lt;= $P_NO_YEARS_WITH_WOLVERINE and @MAXRENEWALDURATION &gt;= $P_NO_YEARS_WITH_WOLVERINE]/@LOSS_FACTOR" />
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="VALUEDCUSTOMER_DISCOUNT_CREDIT_INDIANA">
		<xsl:variable name="P_NO_YEARS_WITH_WOLVERINE" select="NO_YEARS_WITH_WOLVERINE" />
		<xsl:choose>
			<xsl:when test="normalize-space(LOSSFREE) = 'Y'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@MINRENEWALDURATION &lt;= $P_NO_YEARS_WITH_WOLVERINE and @MAXRENEWALDURATION &gt;= $P_NO_YEARS_WITH_WOLVERINE]/@NO_LOSS_CREDIT" />
			</xsl:when>
			<xsl:when test="(LOSSFREE = '' or normalize-space(LOSSFREE)='N')">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@MINRENEWALDURATION &lt;= $P_NO_YEARS_WITH_WOLVERINE and @MAXRENEWALDURATION &gt;= $P_NO_YEARS_WITH_WOLVERINE]/@LOSS_CREDIT" />
			</xsl:when>
			<xsl:otherwise>
			0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="VALUEDCUSTOMER_DISCOUNT">
		<xsl:choose>
			<xsl:when test="STATENAME ='MICHIGAN'">
				<xsl:call-template name="VALUEDCUSTOMER_DISCOUNT_MICHIGAN" />
			</xsl:when>
			<xsl:when test="STATENAME ='INDIANA'">
				<xsl:call-template name="VALUEDCUSTOMER_DISCOUNT_INDIANA" />
			</xsl:when>
			<xsl:otherwise>
		1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="VALUEDCUSTOMER_DISCOUNT_CREDIT">
		<xsl:choose>
			<xsl:when test="STATENAME ='MICHIGAN'">
				<xsl:call-template name="VALUEDCUSTOMER_DISCOUNT_CREDIT_MICHIGAN" />
			</xsl:when>
			<xsl:when test="STATENAME ='INDIANA'">
				<xsl:call-template name="VALUEDCUSTOMER_DISCOUNT_CREDIT_INDIANA" />
			</xsl:when>
			<xsl:otherwise>
		0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="VALUEDCUSTOMER_DISCOUNT_DISPLAY">
		<xsl:choose>
			<xsl:when test="STATENAME ='MICHIGAN'">
				<xsl:call-template name="VALUEDCUSTOMER_DISCOUNT_DISPLAY_MICHIGAN" />
			</xsl:when>
			<xsl:when test="STATENAME ='INDIANA'">
				<xsl:call-template name="VALUEDCUSTOMER_DISCOUNT_DISPLAY_INDIANA" />
			</xsl:when>
			<xsl:otherwise>
		1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--			Templates for Discount - VALUED CUSTOMER [Factor,Credit,Display]  (END)			  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--									Earthquake(START)										  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- 
		1. When added to the Fire policy, this peril shall apply to the same coverages and 
			for the same limits that apply	to the peril of Fire.
		2. Deductible: The deductible is 10% of the limit of liability for each coverage and 
			is subject to a $250 minimum.
			The deductible applies separately to loss under the various coverages of the policy. 
			If the limit of liability on certain property is increased by endorsement, the total 
			limit of liability is used to determine the deductible.
		3. Binder: Earthquake Coverage may be bound subject to a 14-day waiting period. 
			The waiting period ends at 12:01 A.M. fourteen (14) days following the date the insured 
			requests coverage. The binder or insured's request must be received by the company prior 
			to the expiration of the 14-day waiting period. Earthquake coverage may not be deleted from 
			new, renewal or rewritten policies prior to the policy expiration. -->
	<!-- ============================================================================================ -->
	<xsl:template name="EARTHQUAKE">
		<xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE" />
		<xsl:variable name="VAR_COV_A">
			<xsl:call-template name="EARTHQUAKE_COV_A" />
		</xsl:variable>
		<xsl:variable name="VAR_COV_B">
			<xsl:choose>
				<xsl:when test="APPURTENANTSTRUCTURES_ADDITIONAL != '' and APPURTENANTSTRUCTURES_ADDITIONAL &gt;0">
					<xsl:call-template name="EARTHQUAKE_COV_B" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_COV_D">
			<xsl:call-template name="EARTHQUAKE_COV_D" />
		</xsl:variable>
		<xsl:variable name="VAR_COV_E">
			<xsl:call-template name="EARTHQUAKE_COV_E" />
		</xsl:variable>
		<xsl:variable name="VAR_COV_C">
			<xsl:call-template name="EARTHQUAKE_COV_C" />
		</xsl:variable>
		<xsl:variable name="P_EARTHQUAKE_OTHER_COVERAGES_FACTOR">
			<xsl:call-template name="EARTHQUAKE_OTHER_COVERAGES_FACTOR" />
		</xsl:variable>
		<!-- Building Improvements/Alterations
		Contents In Storage
		Trees, Lawns, and Shrubs
		Radio and TV Equipment'
		Satellite Dishes
		Awnings and Canopies		
    -->
		<xsl:variable name="VAR1">
			<xsl:call-template name="EARTHQUAKE_CALC_COV_A">
				<xsl:with-param name="FACTORELEMENT" select="$VAR_COV_A"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="EARTHQUAKE_CALC_COV_B">
				<xsl:with-param name="FACTORELEMENT" select="$VAR_COV_B"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:call-template name="EARTHQUAKE_CALC_COV_C">
				<xsl:with-param name="FACTORELEMENT" select="$VAR_COV_C"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR4">
			<xsl:call-template name="EARTHQUAKE_CALC_COV_D">
				<xsl:with-param name="FACTORELEMENT" select="$VAR_COV_D"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR5"> <!-- Satellite Dishes -->
			<xsl:call-template name="EARTHQUAKE_CALC_SATELLITEDISHES_ADDITIONAL">
				<xsl:with-param name="FACTORELEMENT" select="$P_EARTHQUAKE_OTHER_COVERAGES_FACTOR"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable>
		<!--xsl:variable name="VAR5">
			<xsl:call-template name="EARTHQUAKE_CALC_COV_E">
				<xsl:with-param name="FACTORELEMENT" select="$VAR_COV_E"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable-->
		<!-- Building Improvements/Alterations
		Contents In Storage
		Trees, Lawns, and Shrubs
		Radio and TV Equipment'
		Satellite Dishes
		Awnings and Canopies		
    -->
		<!--xsl:variable name="P_EARTHQUAKE_OTHER_COVERAGES_FACTOR">
			<xsl:call-template name="EARTHQUAKE_OTHER_COVERAGES_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR6"-->  <!-- Building Improvements/Alterations-->
		<!--xsl:call-template name="EARTHQUAKE_CALC_BUILDINGIMPROVEMENTS_ADDITIONAL">
				<xsl:with-param name="FACTORELEMENT" select="$P_EARTHQUAKE_OTHER_COVERAGES_FACTOR"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable-->
		<xsl:variable name="VAR7"> <!--Contents In Storage-->
			<xsl:call-template name="EARTHQUAKE_CALC_CONTENTSINSTORAGE_ADDITIONAL">
				<xsl:with-param name="FACTORELEMENT" select="$P_EARTHQUAKE_OTHER_COVERAGES_FACTOR"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable>
		<!--xsl:variable name="VAR8"--> <!-- Trees, Lawns, and Shrubs -->
		<!--xsl:call-template name="EARTHQUAKE_CALC_TREESLAWNSSHRUBS_ADDITIONAL">
				<xsl:with-param name="FACTORELEMENT" select="$P_EARTHQUAKE_OTHER_COVERAGES_FACTOR"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR9"-->  <!-- Radio and TV Equipment -->
		<!--xsl:call-template name="EARTHQUAKE_CALC_RADIOTV_ADDITIONAL">
				<xsl:with-param name="FACTORELEMENT" select="$P_EARTHQUAKE_OTHER_COVERAGES_FACTOR"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR10"-->  <!-- Satellite Dishes -->
		<!--xsl:call-template name="EARTHQUAKE_CALC_SATELLITEDISHES_ADDITIONAL">
				<xsl:with-param name="FACTORELEMENT" select="$P_EARTHQUAKE_OTHER_COVERAGES_FACTOR"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR11"-->  <!--Awnings and Canopies	 -->
		<!--xsl:call-template name="EARTHQUAKE_CALC_AWNINGSCANOPIES_ADDITIONAL">
				<xsl:with-param name="FACTORELEMENT" select="$P_EARTHQUAKE_OTHER_COVERAGES_FACTOR"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR12">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable-->
		<!--xsl:value-of select="round(($VAR1 + $VAR2 + $VAR3 + $VAR4+$VAR5+$VAR6+$VAR7+$VAR8+$VAR9+$VAR10+$VAR11)*$VAR12)" /-->
		<xsl:value-of select="round($VAR1 + $VAR2 + $VAR3 + $VAR4 + $VAR5 + $VAR7)" />
	</xsl:template>
	<!--  ********************************************************************************************************************** -->
	<!-- EARTH QUAKE for Coverage A -->
	<xsl:template name="EARTHQUAKE_COV_A">
		<xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE" />
		<xsl:choose>
			<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'F'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovA' and @TERRZONE = $VAR_ZONE]/@FRAME" />
			</xsl:when>
			<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'M'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovA' and @TERRZONE = $VAR_ZONE]/@MASONRY" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- calculation of value for earthquake against coverage A  -->
	<xsl:template name="EARTHQUAKE_CALC_COV_A">
		<xsl:param name="FACTORELEMENT" />
		<xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE" />
		<xsl:variable name="P_COVERAGE_A" select="DWELLING_LIMITS" />
		<xsl:variable name="P_RATE_PER">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovA' and @TERRZONE = $VAR_ZONE]/@RATE_PER_VALUE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="EARTHQUAKEDP469 = 'Y' and $P_COVERAGE_A &gt; 0">
				<xsl:value-of select="($P_COVERAGE_A div $P_RATE_PER)* $FACTORELEMENT" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--  ********************************************************************************************************************** -->
	<!-- EARTH QUAKE for Coverage B -->
	<xsl:template name="EARTHQUAKE_COV_B">
		<xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE" />
		<xsl:choose>
			<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'F'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovB' and @TERRZONE = $VAR_ZONE]/@FRAME" />
			</xsl:when>
			<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'M'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovB' and @TERRZONE = $VAR_ZONE]/@MASONRY" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- calculation of value for earthquake against coverage B  -->
	<xsl:template name="EARTHQUAKE_CALC_COV_B">
		<xsl:param name="FACTORELEMENT" />
		<xsl:variable name="P_APPURTENANTSTRUCTURES_ADDITIONAL" select="APPURTENANTSTRUCTURES_ADDITIONAL" />
		<xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE" />
		<xsl:variable name="P_RATE_PER">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovB' and @TERRZONE = $VAR_ZONE]/@RATE_PER_VALUE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="EARTHQUAKEDP469 = 'Y' and  $P_APPURTENANTSTRUCTURES_ADDITIONAL &gt; 0">
				<xsl:value-of select="($P_APPURTENANTSTRUCTURES_ADDITIONAL div $P_RATE_PER)* $FACTORELEMENT" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--  ********************************************************************************************************************** -->
	<!-- EARTH QUAKE for Coverage C -->
	<xsl:template name="EARTHQUAKE_COV_C">
		<xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE" />
		<xsl:choose>
			<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'F'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovC' and @TERRZONE = $VAR_ZONE]/@FRAME" />
			</xsl:when>
			<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'M'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovC' and @TERRZONE = $VAR_ZONE]/@MASONRY" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="EARTHQUAKE_CALC_COV_C">
		<xsl:param name="FACTORELEMENT" />
		<xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE" />
		<xsl:variable name="P_COV_C" select="PERSONALPROPERTY_ADDITIONAL" />
		<xsl:variable name="P_RATE_PER">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovC' and @TERRZONE = $VAR_ZONE]/@RATE_PER_VALUE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="EARTHQUAKEDP469 = 'Y' and $P_COV_C &gt; 0 ">
				<xsl:value-of select="($P_COV_C div $P_RATE_PER)* $FACTORELEMENT" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--  ********************************************************************************************************************** -->
	<!-- EARTH QUAKE for Coverage D -->
	<xsl:template name="EARTHQUAKE_COV_D">
		<xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE" />
		<xsl:choose>
			<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'F'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovD' and @TERRZONE = $VAR_ZONE]/@FRAME" />
			</xsl:when>
			<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'M'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovD' and @TERRZONE = $VAR_ZONE]/@MASONRY" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="EARTHQUAKE_CALC_COV_D">
		<xsl:param name="FACTORELEMENT" />
		<xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE" />
		<xsl:variable name="P_COV_D" select="RENTALVALUE_ADDITIONAL" />
		<xsl:variable name="P_RATE_PER">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovD' and @TERRZONE = $VAR_ZONE]/@RATE_PER_VALUE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="EARTHQUAKEDP469 = 'Y'">
				<xsl:value-of select="round(($P_COV_D div $P_RATE_PER)* $FACTORELEMENT)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
		<!--0.00  TEMPORARILY SENDING 0. WILL BE REMOVED AFTER CLARIFICATION WITH ASHISH -->
	</xsl:template>
	<!--  ********************************************************************************************************************** -->
	<!-- EARTH QUAKE for Coverage E -->
	<xsl:template name="EARTHQUAKE_COV_E">
		<xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE" />
		<xsl:choose>
			<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'F'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovE' and @TERRZONE = $VAR_ZONE]/@FRAME" />
			</xsl:when>
			<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'M'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovE' and @TERRZONE = $VAR_ZONE]/@MASONRY" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="EARTHQUAKE_CALC_COV_E">
	<!--
	<xsl:param name="FACTORELEMENT" />
	<xsl:variable name="P_LIABILITY_EACH_OCCURRENCE_DISPLAY" select="LIABILITY_EACH_OCCURRENCE_DISPLAY"/>
	<xsl:variable name="P_RATE_PER">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovD' and @TERRZONE = $VAR_ZONE]/@RATE_PER_VALUE"/> 
	</xsl:variable>
	<xsl:choose> 
		<xsl:when test="EARTHQUAKEDP469 = 'Y'">		 
			<xsl:value-of select="round(($P_LIABILITY_EACH_OCCURRENCE_DISPLAY div $P_RATE_PER)* $FACTORELEMENT)"/>
		</xsl:when>
		 <xsl:otherwise>0</xsl:otherwise>
   </xsl:choose>
  -->
  0.00
</xsl:template>
	<!-- Factor for other coverages -->
	<xsl:template name="EARTHQUAKE_OTHER_COVERAGES_FACTOR">
		<xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE" />
		<xsl:choose>
			<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'F'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'Other' and @TERRZONE = $VAR_ZONE]/@FRAME" />
			</xsl:when>
			<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'M'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'Other' and @TERRZONE = $VAR_ZONE]/@MASONRY" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--  ********************************************************************************************************************** -->
	<!-- EARTH QUAKE for Building Improvements/Alterations -->
	<xsl:template name="EARTHQUAKE_CALC_BUILDINGIMPROVEMENTS_ADDITIONAL">
		<xsl:param name="FACTORELEMENT" />
		<xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE" />
		<xsl:variable name="P_BUILDINGIMPROVEMENTS_ADDITIONAL" select="BUILDINGIMPROVEMENTS_ADDITIONAL" />
		<xsl:variable name="P_RATE_PER">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'Other' and @TERRZONE = $VAR_ZONE]/@RATE_PER_VALUE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$P_BUILDINGIMPROVEMENTS_ADDITIONAL !='' and $P_BUILDINGIMPROVEMENTS_ADDITIONAL !='NO COVERAGE' and $P_BUILDINGIMPROVEMENTS_ADDITIONAL &gt; 0">
				<xsl:choose>
					<xsl:when test="EARTHQUAKEDP469 = 'Y'">
						<xsl:value-of select="($P_BUILDINGIMPROVEMENTS_ADDITIONAL div $P_RATE_PER)* $FACTORELEMENT" />
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--  ********************************************************************************************************************** -->
	<!-- EARTH QUAKE for Contents In Storage -->
	<xsl:template name="EARTHQUAKE_CALC_CONTENTSINSTORAGE_ADDITIONAL">
		<xsl:param name="FACTORELEMENT" />
		<xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE" />
		<xsl:variable name="P_CONTENTSINSTORAGE_ADDITIONAL" select="CONTENTSINSTORAGE_ADDITIONAL" />
		<xsl:variable name="P_RATE_PER">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'Other' and @TERRZONE = $VAR_ZONE]/@RATE_PER_VALUE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$P_CONTENTSINSTORAGE_ADDITIONAL !='' and $P_CONTENTSINSTORAGE_ADDITIONAL !='NO COVERAGE' and $P_CONTENTSINSTORAGE_ADDITIONAL &gt; 0">
				<xsl:choose>
					<xsl:when test="EARTHQUAKEDP469 = 'Y'">
						<xsl:value-of select="($P_CONTENTSINSTORAGE_ADDITIONAL div $P_RATE_PER)* $FACTORELEMENT" />
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--  ********************************************************************************************************************** -->
	<!-- EARTH QUAKE for Trees, Lawns, and Shrubs -->
	<xsl:template name="EARTHQUAKE_CALC_TREESLAWNSSHRUBS_ADDITIONAL">
		<xsl:param name="FACTORELEMENT" />
		<xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE" />
		<xsl:variable name="P_TREESLAWNSSHRUBS_ADDITIONAL" select="TREESLAWNSSHRUBS_ADDITIONAL" />
		<xsl:variable name="P_RATE_PER">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'Other' and @TERRZONE = $VAR_ZONE]/@RATE_PER_VALUE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$P_TREESLAWNSSHRUBS_ADDITIONAL !='' and $P_TREESLAWNSSHRUBS_ADDITIONAL !='NO COVERAGE' and $P_TREESLAWNSSHRUBS_ADDITIONAL &gt; 0">
				<xsl:choose>
					<xsl:when test="EARTHQUAKEDP469 = 'Y'">
						<xsl:value-of select="($P_TREESLAWNSSHRUBS_ADDITIONAL div $P_RATE_PER)* $FACTORELEMENT" />
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--  ********************************************************************************************************************** -->
	<!-- EARTH QUAKE for Radio and TV Equipment' -->
	<xsl:template name="EARTHQUAKE_CALC_RADIOTV_ADDITIONAL">
		<xsl:param name="FACTORELEMENT" />
		<xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE" />
		<xsl:variable name="P_RADIOTV_ADDITIONAL" select="RADIOTV_ADDITIONAL" />
		<xsl:variable name="P_RATE_PER">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'Other' and @TERRZONE = $VAR_ZONE]/@RATE_PER_VALUE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$P_RADIOTV_ADDITIONAL !='' and $P_RADIOTV_ADDITIONAL !='NO COVERAGE' and $P_RADIOTV_ADDITIONAL &gt; 0">
				<xsl:choose>
					<xsl:when test="EARTHQUAKEDP469 = 'Y'">
						<xsl:value-of select="($P_RADIOTV_ADDITIONAL div $P_RATE_PER)* $FACTORELEMENT" />
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--  ********************************************************************************************************************** -->
	<!-- EARTH QUAKE for Satellite Dishes -->
	<xsl:template name="EARTHQUAKE_CALC_SATELLITEDISHES_ADDITIONAL">
		<xsl:param name="FACTORELEMENT" />
		<xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE" />
		<xsl:variable name="P_SATELLITEDISHES_ADDITIONAL" select="SATELLITEDISHES_ADDITIONAL" />
		<xsl:variable name="P_RATE_PER">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'Other' and @TERRZONE = $VAR_ZONE]/@RATE_PER_VALUE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$P_SATELLITEDISHES_ADDITIONAL !='' and $P_SATELLITEDISHES_ADDITIONAL !='NO COVERAGE' and $P_SATELLITEDISHES_ADDITIONAL &gt; 0">
				<xsl:choose>
					<xsl:when test="EARTHQUAKEDP469 = 'Y'">
						<xsl:value-of select="($P_SATELLITEDISHES_ADDITIONAL div $P_RATE_PER)* $FACTORELEMENT" />
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--  ********************************************************************************************************************** -->
	<!-- EARTH QUAKE for Awnings and Canopies -->
	<xsl:template name="EARTHQUAKE_CALC_AWNINGSCANOPIES_ADDITIONAL">
		<xsl:param name="FACTORELEMENT" />
		<xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE" />
		<xsl:variable name="P_AWNINGSCANOPIES_ADDITIONAL" select="AWNINGSCANOPIES_ADDITIONAL" />
		<xsl:variable name="P_RATE_PER">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'Other' and @TERRZONE = $VAR_ZONE]/@RATE_PER_VALUE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$P_AWNINGSCANOPIES_ADDITIONAL !='' and $P_AWNINGSCANOPIES_ADDITIONAL !='NO COVERAGE' and $P_AWNINGSCANOPIES_ADDITIONAL &gt; 0">
				<xsl:choose>
					<xsl:when test="EARTHQUAKEDP469 = 'Y'">
						<xsl:value-of select="($P_AWNINGSCANOPIES_ADDITIONAL div $P_RATE_PER)* $FACTORELEMENT" />
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--									Earthquake(END)											  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--					Installation Floater Building Materials IF-184(START)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="INSTALLATION_FLOATER_IF_184_BUILDING_MATERIALS">
		<xsl:variable name="P_FLOATERBUILDINGMATERIALS_ADDITIONAL" select="FLOATERBUILDINGMATERIALS_ADDITIONAL" />
		<xsl:variable name="VAR_TERM">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="P_RATES_PER_VALUE">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSTALLATIONFLOATER']/NODE[@ID ='INSTALLATION_FLOATER']/@RATES_PER_VALUE" />
		</xsl:variable>
		<xsl:variable name="P_FLOATERBUILDINGMATERIALS_ADDITIONAL_FACTOR_AMOUNT">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSTALLATIONFLOATER']/NODE[@ID ='INSTALLATION_FLOATER']/ATTRIBUTES/@BUILDING_MATERIALS" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$P_FLOATERBUILDINGMATERIALS_ADDITIONAL =''">
			0.00
		</xsl:when>
			<xsl:when test="DWELL_UND_CONSTRUCTION_DP1143 = 'Y' and $P_FLOATERBUILDINGMATERIALS_ADDITIONAL &gt; 0">
				<xsl:value-of select="round((round(($P_FLOATERBUILDINGMATERIALS_ADDITIONAL_FACTOR_AMOUNT div $P_RATES_PER_VALUE) * $P_FLOATERBUILDINGMATERIALS_ADDITIONAL ))*$VAR_TERM)" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Installation Floater IF-184(end)								  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Installation Floater NON_Structural Equipment IF-184(START)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="INSTALLATION_FLOATER_IF_184_NON_STRUCTURAL_EQUIPMENT">
		<xsl:variable name="P_FLOATERNONSTRUCTURAL_ADDITIONAL" select="FLOATERNONSTRUCTURAL_ADDITIONAL" />
		<xsl:variable name="P_RATES_PER_VALUE">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSTALLATIONFLOATER']/NODE[@ID ='INSTALLATION_FLOATER']/@RATES_PER_VALUE" />
		</xsl:variable>
		<xsl:variable name="P_FLOATERNONSTRUCTURAL_ADDITIONAL_FACTOR_AMOUNT">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSTALLATIONFLOATER']/NODE[@ID ='INSTALLATION_FLOATER']/ATTRIBUTES/@NON_STRUCTURAL_EQUIPMENT" />
		</xsl:variable>
		<xsl:variable name="VAR_TERM">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$P_FLOATERNONSTRUCTURAL_ADDITIONAL =''">
			0.00
		</xsl:when>
			<xsl:when test="DWELL_UND_CONSTRUCTION_DP1143 = 'Y' and $P_FLOATERNONSTRUCTURAL_ADDITIONAL &gt; 0">
				<xsl:value-of select="round((round(($P_FLOATERNONSTRUCTURAL_ADDITIONAL_FACTOR_AMOUNT div $P_RATES_PER_VALUE) * $P_FLOATERNONSTRUCTURAL_ADDITIONAL )) *$VAR_TERM)" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Installation Floater NON_Structural Equipment IF-184(START)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--			Templates for Mine Subsidence Coverage[Premium]  (START)						  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="MINE_SUBSIDENCE_COVERAGE_PREMIUM">
		<xsl:variable name="P_MINESUBSIDENCE_ADDITIONAL" select="MINESUBSIDENCE_ADDITIONAL" />
		<xsl:choose>
			<xsl:when test="MINESUBSIDENCEDP480 ='Y' and  STATENAME='INDIANA'">
				<xsl:variable name="VAR_COVG_THRESHOLD" select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINE']/NODE[@ID ='MINE_PREMIUM']/@COVG_THRESHOLD" />
				<xsl:if test="$P_MINESUBSIDENCE_ADDITIONAL &gt; $VAR_COVG_THRESHOLD">
					<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINE']/NODE[@ID ='MINE_PREMIUM']/ATTRIBUTES[@MINCOVERAGE = $VAR_COVG_THRESHOLD]/@PREMIUM" />
				</xsl:if>
				<xsl:if test="$P_MINESUBSIDENCE_ADDITIONAL &lt;= $VAR_COVG_THRESHOLD">
					<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINE']/NODE[@ID ='MINE_PREMIUM']/ATTRIBUTES[@MINCOVERAGE &lt;= $P_MINESUBSIDENCE_ADDITIONAL and @MAXCOVERAGE &gt;= $P_MINESUBSIDENCE_ADDITIONAL]/@PREMIUM" />
				</xsl:if>
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- MINE SUBSIDENCE DEDUCTIBLE - A deductible of 2% (Minimum $250; Maximum $500) applies to each loss -->
	<xsl:template name="MINE_DED_AMOUNT_DISPLAY">
		<xsl:variable name="var_MINESUBSIDENCE_ADDITIONAL" select="MINESUBSIDENCE_ADDITIONAL" />
		<xsl:variable name="var_DEDUCTIBLE_PERCENT" select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINE']/NODE[@ID ='MINE_PREMIUM']/@DEDUCTIBLE_PERCENT" />
		<xsl:variable name="var_DEDUCTIBLE_AMOUNT">
			<xsl:value-of select="round(($var_MINESUBSIDENCE_ADDITIONAL * $var_DEDUCTIBLE_PERCENT) div 100)" />
		</xsl:variable>
		<xsl:variable name="var_DED_MINIMUM" select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINE']/NODE[@ID ='MINE_PREMIUM']/@DED_MINIMUM" />
		<xsl:variable name="var_DED_MAXIMUM" select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINE']/NODE[@ID ='MINE_PREMIUM']/@DED_MAXIMUM" />
		<xsl:choose>
			<xsl:when test="$var_DEDUCTIBLE_AMOUNT &lt; $var_DED_MINIMUM">
				<xsl:value-of select="$var_DED_MINIMUM" />
			</xsl:when>
			<xsl:when test="$var_DEDUCTIBLE_AMOUNT &gt; $var_DED_MAXIMUM">
				<xsl:value-of select="$var_DED_MAXIMUM" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$var_DEDUCTIBLE_AMOUNT" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--			Templates for Mime Subsidence Coverage[Premium]  (END)							  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--			Templates for Limited Lead Liability [Premium]  (Start)							  -->
	<!--		    					  FOR MICHIGAN 								  -->
	<!-- ============================================================================================ -->
	<xsl:template name="LIMITED_LEAD_LIABILITY">
		<xsl:choose>
			<xsl:when test="STATENAME ='MICHIGAN' and LEADLIABILITY ='Y'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITY_LEAD_PREMIUM']/NODE[@ID ='LEAD_PREMIUM']/ATTRIBUTES/@PREMIUM" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--			Templates for Limited Lead Liability [Premium]  (END)							  -->
	<!--		    					  FOR MICHIGAN 									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for Surcharge - Surcharges Factor [Factor]  (START)						  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="SURCHARGES">
		<xsl:choose>
			<xsl:when test="NUMBEROFFAMILIES = 1 and SEASONALSECONDARY != 'Y'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGES']/NODE[@ID ='FAMILYUNITSINDWELL']/ATTRIBUTES[@UNITS = '1']/@PRIMARY" />
			</xsl:when>
			<xsl:when test="NUMBEROFFAMILIES = 2 and SEASONALSECONDARY != 'Y'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGES']/NODE[@ID ='FAMILYUNITSINDWELL']/ATTRIBUTES[@UNITS = '2']/@PRIMARY" />
			</xsl:when>
			<xsl:when test="NUMBEROFFAMILIES = 1 and SEASONALSECONDARY = 'Y'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGES']/NODE[@ID ='FAMILYUNITSINDWELL']/ATTRIBUTES[@UNITS = '1']/@SEASONAL" />
			</xsl:when>
			<xsl:when test="NUMBEROFFAMILIES = 2 and SEASONALSECONDARY = 'Y'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGES']/NODE[@ID ='FAMILYUNITSINDWELL']/ATTRIBUTES[@UNITS = '2']/@SEASONAL" />
			</xsl:when>
			<xsl:otherwise>
		1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Surcharge - Surcharges Factor [Factor]  (END)							  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--						TEMPLATE FOR CALLING ADJUSTEDBASE AS PER THE INPUT-XML (START)		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="CALL_ADJUSTEDBASE">
		<xsl:variable name="P_PRODUCT_PREMIER" select="PRODUCT_PREMIER"></xsl:variable>
		<xsl:variable name="PRODUCT_PREMIER_IN_CAPS" select="translate(translate($P_PRODUCT_PREMIER,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')" />
		<xsl:choose>
			<!-- For MICHIGAN State -->
			<xsl:when test="STATENAME ='MICHIGAN' and PRODUCTNAME = $POLICYTYPE_DP2 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPLACEMENT">
				<xsl:call-template name="ADJUSTEDBASE_MICHIGAN_REGULER_DP2" />
			</xsl:when>
			<xsl:when test="STATENAME ='MICHIGAN' and PRODUCTNAME = $POLICYTYPE_DP3 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPLACEMENT">
				<xsl:call-template name="ADJUSTEDBASE_MICHIGAN_REGULER_DP3" />
			</xsl:when>
			<xsl:when test="STATENAME ='MICHIGAN' and PRODUCTNAME = $POLICYTYPE_DP3 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_PREMIER">
				<xsl:call-template name="ADJUSTEDBASE_MICHIGAN_PREMIER_DP3" />
			</xsl:when>
			<xsl:when test="STATENAME ='MICHIGAN' and PRODUCTNAME = $POLICYTYPE_DP2 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPAIR">
				<xsl:call-template name="ADJUSTEDBASE_MICHIGAN_REPAIR_COST_DP2" />
			</xsl:when>
			<xsl:when test="STATENAME ='MICHIGAN' and PRODUCTNAME = $POLICYTYPE_DP3 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPAIR">
				<xsl:call-template name="ADJUSTEDBASE_MICHIGAN_REPAIR_COST_DP3" />
			</xsl:when>
			<!-- For INDIANA State -->
			<xsl:when test="STATENAME ='INDIANA' and PRODUCTNAME = $POLICYTYPE_DP2 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPLACEMENT">
				<xsl:call-template name="ADJUSTEDBASE_INDIANA_REGULER_DP2" />
			</xsl:when>
			<xsl:when test="STATENAME ='INDIANA' and PRODUCTNAME = $POLICYTYPE_DP3 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPLACEMENT">
				<xsl:call-template name="ADJUSTEDBASE_INDIANA_REGULER_DP3" />
			</xsl:when>
			<xsl:when test="STATENAME ='INDIANA' and PRODUCTNAME = $POLICYTYPE_DP2 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPAIR">
				<xsl:call-template name="ADJUSTEDBASE_INDIANA_REPAIR_COST_DP2" />
			</xsl:when>
			<xsl:when test="STATENAME ='INDIANA' and PRODUCTNAME = $POLICYTYPE_DP3 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPAIR">
				<xsl:call-template name="ADJUSTEDBASE_INDIANA_REPAIR_COST_DP3" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--						TEMPLATE FOR CALLING ADJUSTEDBASE AS PER THE INPUT-XML (END)		  -->
	<!-- ============================================================================================ -->
	<!-- ############################################################################################ -->
	<!--								ADJUSTEDBASE FOR MICHIGAN	(START)							  -->
	<!-- ############################################################################################ -->
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR MICHIGAN(REGULER) DP-3	(START)					  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_MICHIGAN_REGULER_DP3">
		<xsl:variable name="VAR1">
			<xsl:call-template name="DEWLLING-MAIN_MICHIGAN" />
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="C_VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:call-template name="REPLACEMENT_COST" />
		</xsl:variable>
		<xsl:variable name="VAR4">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR5">
			<xsl:call-template name="NO_OF_FAMILY_UNITS_DWELLINGS_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR6">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR7">
			<xsl:call-template name="PROTECTIVE_DEVICE_DISCOUNT_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR8">
			<xsl:call-template name="AGEOFHOME_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR9">
			<xsl:call-template name="DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR10">
			<xsl:call-template name="MULTIPOLICY_DISCOUNT_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR11">
			<xsl:call-template name="VALUEDCUSTOMER_DISCOUNT" />
		</xsl:variable>
		<xsl:value-of select="round(round(round(round(round(round(round(round(round(round($VAR1*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR MICHIGAN(REGULER) DP-3	(END)					  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR MICHIGAN(REGULER) DP-2	(START)					  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_MICHIGAN_REGULER_DP2">
		<xsl:variable name="VAR1">
			<xsl:call-template name="DEWLLING-MAIN_MICHIGAN" />
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="C_VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:call-template name="REPLACEMENT_COST" />
		</xsl:variable>
		<xsl:variable name="VAR4">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR5">
			<xsl:call-template name="NO_OF_FAMILY_UNITS_DWELLINGS_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR6">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR7">
			<xsl:call-template name="PROTECTIVE_DEVICE_DISCOUNT_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR8">
			<xsl:call-template name="AGEOFHOME_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR9">
			<xsl:call-template name="DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR10">
			<xsl:call-template name="MULTIPOLICY_DISCOUNT_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR11">
			<xsl:call-template name="VALUEDCUSTOMER_DISCOUNT" />
		</xsl:variable>
		<xsl:value-of select="round(round(round(round(round(round(round(round(round(round($VAR1*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR MICHIGAN(REGULER) DP-2	(END)					  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR MICHIGAN(PREMIUM) DP-3	(START)					  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_MICHIGAN_PREMIER_DP3">
		<xsl:variable name="VAR1">
			<xsl:call-template name="DEWLLING-MAIN_MICHIGAN" />
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="C_VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:call-template name="REPLACEMENT_COST" />
		</xsl:variable>
		<xsl:variable name="VAR4">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR5">
			<xsl:call-template name="NO_OF_FAMILY_UNITS_DWELLINGS_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR6">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR7">
			<xsl:call-template name="PROTECTIVE_DEVICE_DISCOUNT_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR8">
			<xsl:call-template name="AGEOFHOME_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR9">
			<xsl:call-template name="MULTIPOLICY_DISCOUNT_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR10">
			<xsl:call-template name="VALUEDCUSTOMER_DISCOUNT" />
		</xsl:variable>
		<xsl:value-of select="round(round(round(round(round(round(round(round(round($VAR1*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR MICHIGAN(PREMIUM) DP-3	(END)					  -->
	<!-- ============================================================================================ -->
	<!--============(((((((((((((()))))))))))))))))))))))))))===========================-->
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR MICHIGAN(REPAIR COST) DP-3	(START)				  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_MICHIGAN_REPAIR_COST_DP3">
		<xsl:variable name="VAR1">
			<xsl:call-template name="DEWLLING-MAIN_MICHIGAN" />
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="C_VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:call-template name="REPAIR_COST_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR4">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR5">
			<xsl:call-template name="NO_OF_FAMILY_UNITS_DWELLINGS_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR6">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR7">
			<xsl:call-template name="PROTECTIVE_DEVICE_DISCOUNT_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR8">
			<xsl:call-template name="AGEOFHOME_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR9">
			<xsl:call-template name="DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR10">
			<xsl:call-template name="MULTIPOLICY_DISCOUNT_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR11">
			<xsl:call-template name="VALUEDCUSTOMER_DISCOUNT" />
		</xsl:variable>
		<xsl:value-of select="round(round(round(round(round(round(round(round(round(round($VAR1*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR MICHIGAN(REPAIR COST) DP-3	(END)				  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR MICHIGAN(REPAIR COST) DP-2	(START)				  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_MICHIGAN_REPAIR_COST_DP2">
		<xsl:variable name="VAR1">
			<xsl:call-template name="DEWLLING-MAIN_MICHIGAN" />
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="C_VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:call-template name="REPAIR_COST_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR4">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR5">
			<xsl:call-template name="NO_OF_FAMILY_UNITS_DWELLINGS_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR6">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR7">
			<xsl:call-template name="PROTECTIVE_DEVICE_DISCOUNT_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR8">
			<xsl:call-template name="AGEOFHOME_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR9">
			<xsl:call-template name="DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR10">
			<xsl:call-template name="MULTIPOLICY_DISCOUNT_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR11">
			<xsl:call-template name="VALUEDCUSTOMER_DISCOUNT" />
		</xsl:variable>
		<!--(
	<xsl:call-template name="DEWLLING-MAIN_MICHIGAN"/>
	*
	<xsl:call-template name ="C_VALUE"/>
	*
	<xsl:call-template name ="REPAIR_COST_FACTOR"/>
	*
	<xsl:call-template name ="REPLACEMENT_COST"/>
	*
	<xsl:call-template name ="TERM_FACTOR"/>
	*
	<xsl:call-template name = "NO_OF_FAMILY_UNITS_DWELLINGS_FACTOR"/>
	*
	<xsl:call-template name ="DFACTOR"/>
	*
	<xsl:call-template name ="PROTECTIVE_DEVICE_DISCOUNT_FACTOR"/>
	
	*
	<xsl:call-template name = "AGEOFHOME_FACTOR"/> 
	*<xsl:call-template name = "DUC_DISCOUNT"/>	
	*<xsl:call-template name = "MULTIPOLICY_DISCOUNT_FACTOR"/>
	
	* <xsl:call-template name = "VALUEDCUSTOMER_DISCOUNT"/> 
	)
	-->
		<xsl:value-of select="round(round(round(round(round(round(round(round(round(round($VAR1*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR MICHIGAN(REPAIR COST) DP-2	(END)				  -->
	<!-- ============================================================================================ -->
	<!-- ############################################################################################ -->
	<!--								ADJUSTEDBASE FOR MICHIGAN	(END)							  -->
	<!-- ############################################################################################ -->
	<!-- ############################################################################################ -->
	<!--								ADJUSTEDBASE FOR INDIANA	(START)							  -->
	<!-- ############################################################################################ -->
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR INDIANA(REGULER) DP-3	(START)					  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_INDIANA_REGULER_DP3">
		<xsl:variable name="VAR1">
			<xsl:call-template name="DEWLLING-MAIN_INDIANA" />
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="C_VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:call-template name="REPLACEMENT_COST" />
		</xsl:variable>
		<xsl:variable name="VAR4">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR5">
			<xsl:call-template name="NO_OF_FAMILY_UNITS_DWELLINGS_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR6">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR7">
			<xsl:call-template name="PROTECTIVE_DEVICE_DISCOUNT_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR8">
			<xsl:call-template name="AGEOFHOME_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR9">
			<xsl:call-template name="DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR10">
			<xsl:call-template name="MULTIPOLICY_DISCOUNT_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR11">
			<xsl:call-template name="VALUEDCUSTOMER_DISCOUNT_INDIANA" />
		</xsl:variable>
		<xsl:value-of select="round(round(round(round(round(round(round(round(round(round($VAR1*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR INDIANA(REGULER) DP-3	(END)					  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR INDIANA(REGULER) DP-2	(START)					  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_INDIANA_REGULER_DP2">
		<xsl:variable name="VAR1">
			<xsl:call-template name="DEWLLING-MAIN_INDIANA" />
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="C_VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:call-template name="REPLACEMENT_COST" />
		</xsl:variable>
		<xsl:variable name="VAR4">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR5">
			<xsl:call-template name="NO_OF_FAMILY_UNITS_DWELLINGS_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR6">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR7">
			<xsl:call-template name="PROTECTIVE_DEVICE_DISCOUNT_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR8">
			<xsl:call-template name="AGEOFHOME_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR9">
			<xsl:call-template name="MULTIPOLICY_DISCOUNT_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR10">
			<xsl:call-template name="VALUEDCUSTOMER_DISCOUNT_INDIANA" />
		</xsl:variable>
		<xsl:variable name="VAR11">
			<xsl:call-template name="DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:value-of select="round(round(round(round(round(round(round(round(round(round($VAR1*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR INDIANA(REGULER) DP-2	(END)					  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR INDIANA(REPAIR COST) DP-3	(START)				  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_INDIANA_REPAIR_COST_DP3">
		<xsl:variable name="VAR1">
			<xsl:call-template name="DEWLLING-MAIN_INDIANA" />
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="C_VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:call-template name="REPAIR_COST_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR4">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR5">
			<xsl:call-template name="NO_OF_FAMILY_UNITS_DWELLINGS_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR6">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR7">
			<xsl:call-template name="PROTECTIVE_DEVICE_DISCOUNT_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR8">
			<xsl:call-template name="AGEOFHOME_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR9">
			<xsl:call-template name="MULTIPOLICY_DISCOUNT_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR10">
			<xsl:call-template name="VALUEDCUSTOMER_DISCOUNT_INDIANA" />
		</xsl:variable>
		<xsl:variable name="VAR11">
			<xsl:call-template name="DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:value-of select="round(round(round(round(round(round(round(round(round(round($VAR1*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR INDIANA(REPAIR COST) DP-3	(END)				  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR INDIANA(REPAIR COST) DP-2	(START)				  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_INDIANA_REPAIR_COST_DP2">
		<xsl:variable name="VAR1">
			<xsl:call-template name="DEWLLING-MAIN_INDIANA" />
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="C_VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:call-template name="REPAIR_COST_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR4">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR5">
			<xsl:call-template name="NO_OF_FAMILY_UNITS_DWELLINGS_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR6">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR7">
			<xsl:call-template name="PROTECTIVE_DEVICE_DISCOUNT_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR8">
			<xsl:call-template name="AGEOFHOME_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR9">
			<xsl:call-template name="MULTIPOLICY_DISCOUNT_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR10">
			<xsl:call-template name="VALUEDCUSTOMER_DISCOUNT_INDIANA" />
		</xsl:variable>
		<xsl:variable name="VAR11">
			<xsl:call-template name="DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:value-of select="round(round(round(round(round(round(round(round(round(round($VAR1*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR INDIANA(REPAIR COST) DP-2	(END)				  -->
	<!-- ============================================================================================ -->
	<!-- ############################################################################################ -->
	<!--								ADJUSTEDBASE FOR INDIANA	(END)							  -->
	<!-- ############################################################################################ -->
	<!-- ============================================================================================ -->
	<!--									Trees, Lawns, and Shrubs (START)	  					  -->
	<!-- ============================================================================================ -->
	<xsl:template name="TREES_LAWNS_AND_SHRUBS_ADDITIONAL">
		<xsl:variable name="P_RATES_PER_VALUE">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_2']/@RATES_PER_VALUE" />
		</xsl:variable>
		<!-- get form  code-->
		<xsl:variable name="P_FORM_CODE" select="PRODUCTNAME" />
		<!-- get deductible -->
		<xsl:variable name="P_DEDUCTIBLE" select="DEDUCTIBLE" />
		<!-- get factor for trees,lawns etc -->
		<xsl:variable name="PTREES_LAWNS">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_2']/ATTRIBUTES[@NAME = 'TREES_LAWNS_AND_SHRUBS' and @FORM_CODE=$P_FORM_CODE and @DEDUCTIBLE=$P_DEDUCTIBLE]/@DEDUCTIBLE_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_TERM">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<!-- calculate only for additional amount -->
		<xsl:choose>
			<xsl:when test="TREESLAWNSSHRUBS_ADDITIONAL = '' or TREESLAWNSSHRUBS_ADDITIONAL &lt;= 0">
		0.00
		</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR1" select="TREESLAWNSSHRUBS_ADDITIONAL" />
				<xsl:value-of select="round(round(($VAR1 div $P_RATES_PER_VALUE) * $PTREES_LAWNS) * $VAR_TERM)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--									Trees, Lawns, and Shrubs (END)		  					  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--									Radio and TV Equipment (START)		  					  -->
	<!-- ============================================================================================ -->
	<xsl:template name="RADIO_AND_TV_EQUIPMENT_ADDITIONAL">
		<xsl:variable name="P_RATES_PER_VALUE">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_2']/@RATES_PER_VALUE" />
		</xsl:variable>
		<!-- get form  code-->
		<xsl:variable name="P_FORM_CODE" select="PRODUCTNAME" />
		<!-- get deductible -->
		<xsl:variable name="P_DEDUCTIBLE" select="DEDUCTIBLE" />
		<!-- get factor for Radio and TV Equipment -->
		<xsl:variable name="PRADIOTV_ADDITIONAL">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_2']/ATTRIBUTES[@NAME = 'RADIO_AND_TV_EQUIPMENT' and @FORM_CODE=$P_FORM_CODE and @DEDUCTIBLE=$P_DEDUCTIBLE]/@DEDUCTIBLE_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_TERM">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<!-- calculate only for additional amount -->
		<xsl:choose>
			<xsl:when test="RADIOTV_ADDITIONAL = '' or RADIOTV_ADDITIONAL &lt;= 0">
		0.00
		</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR1" select="RADIOTV_ADDITIONAL" />
				<xsl:value-of select="round(($VAR1 div $P_RATES_PER_VALUE) * $PRADIOTV_ADDITIONAL * $VAR_TERM)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--									Radio and TV Equipment (END)		  					  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--									Satellite Dishes  (START)			  					  -->
	<!-- ============================================================================================ -->
	<xsl:template name="SATELLITE_DISHES_ADDITIONAL">
		<xsl:variable name="P_RATES_PER_VALUE">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_2']/@RATES_PER_VALUE" />
		</xsl:variable>
		<!-- get form  code-->
		<xsl:variable name="P_FORM_CODE" select="PRODUCTNAME" />
		<!-- get deductible -->
		<xsl:variable name="P_DEDUCTIBLE" select="DEDUCTIBLE" />
		<!-- get factor for Satellite Dishes -->
		<xsl:variable name="PSATELLITE_DISHES_ADDITIONAL">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_2']/ATTRIBUTES[@NAME = 'SATELLITE_DISHES' and @FORM_CODE=$P_FORM_CODE and @DEDUCTIBLE=$P_DEDUCTIBLE]/@DEDUCTIBLE_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_TERM">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<!-- calculate only for additional amount -->
		<xsl:choose>
			<xsl:when test="SATELLITEDISHES_ADDITIONAL = '' or SATELLITEDISHES_ADDITIONAL &lt;= 0">
			0.00
		</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR1" select="SATELLITEDISHES_ADDITIONAL" />
				<xsl:value-of select="round(($VAR1 div $P_RATES_PER_VALUE) * $PSATELLITE_DISHES_ADDITIONAL * $VAR_TERM )" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="SATELLITEDISHES_COMBINE">
		<xsl:variable name="VAR1" select="SATELLITEDISHES_INCLUDE" />
		<xsl:variable name="VAR2" select="SATELLITEDISHES_ADDITIONAL" />
		<xsl:value-of select="$VAR1+$VAR2"></xsl:value-of>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--									Satellite Dishes  (END)				  					  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--									Awnings and Canopies  (START)			  				  -->
	<!-- ============================================================================================ -->
	<xsl:template name="AWNINGS_AND_CANOPIES_ADDITIONAL">
		<xsl:variable name="P_RATES_PER_VALUE">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_2']/@RATES_PER_VALUE" />
		</xsl:variable>
		<!-- get form  code-->
		<xsl:variable name="P_FORM_CODE" select="PRODUCTNAME" />
		<!-- get deductible -->
		<xsl:variable name="P_DEDUCTIBLE" select="DEDUCTIBLE" />
		<!-- get factor for Awnings and Canopies -->
		<xsl:variable name="PAWNINGS_AND_CANOPIES_ADDITIONAL">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_2']/ATTRIBUTES[@NAME = 'AWNINGS_CANOPIES' and @FORM_CODE=$P_FORM_CODE and @DEDUCTIBLE=$P_DEDUCTIBLE]/@DEDUCTIBLE_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_TERM">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<!-- calculate only for additional amount -->
		<xsl:choose>
			<xsl:when test="AWNINGSCANOPIES_ADDITIONAL = '' or AWNINGSCANOPIES_ADDITIONAL &lt;= 0">
		0.00
		</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR1" select="AWNINGSCANOPIES_ADDITIONAL" />
				<xsl:value-of select="round(($VAR1 div $P_RATES_PER_VALUE) * $PAWNINGS_AND_CANOPIES_ADDITIONAL * $VAR_TERM)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--									Awnings and Canopies  (END)				  				  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Appurtenant Structures (Coverage B)  (START)			  		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="COVERAGE_B_ADDITIONAL">
		<xsl:variable name="P_RATES_PER_VALUE">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/@RATES_PER_VALUE" />
		</xsl:variable>
		<!-- get form  code-->
		<xsl:variable name="P_FORM_CODE" select="PRODUCTNAME" />
		<!-- get deductible -->
		<xsl:variable name="P_DEDUCTIBLE" select="DEDUCTIBLE" />
		<!-- get factor for Appurtenant Structures (Coverage B) -->
		<xsl:variable name="PCOVERAGE_B_ADDITIONAL">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'COVERAGE_B' and @FORM_CODE=$P_FORM_CODE and @DEDUCTIBLE=$P_DEDUCTIBLE]/@DEDUCTIBLE_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_TERM">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<!-- calculate only for additional amount -->
		<xsl:choose>
			<xsl:when test="APPURTENANTSTRUCTURES_ADDITIONAL ='' or APPURTENANTSTRUCTURES_ADDITIONAL &lt;= 0">
		0.00
		</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR1" select="APPURTENANTSTRUCTURES_ADDITIONAL" />
				<xsl:value-of select="round(($VAR1 div $P_RATES_PER_VALUE) * $PCOVERAGE_B_ADDITIONAL * $VAR_TERM)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Appurtenant Structures (Coverage B)  (END)				  		  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Building Improvements/Alteration  (START)				  		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="BUILDING_IMPROVEMENTS_ADDITIONAL">
		<xsl:variable name="P_RATES_PER_VALUE">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/@RATES_PER_VALUE" />
		</xsl:variable>
		<!-- get form  code-->
		<xsl:variable name="P_FORM_CODE" select="PRODUCTNAME" />
		<!-- get deductible -->
		<xsl:variable name="P_DEDUCTIBLE" select="DEDUCTIBLE" />
		<!-- get factor for Building Improvements/Alteration -->
		<xsl:variable name="PBUILDING_IMPROVEMENTS_ADDITIONAL">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'BUILDING_IMPROVEMENTS_AND_ALTERATIONS' and @FORM_CODE=$P_FORM_CODE and @DEDUCTIBLE=$P_DEDUCTIBLE]/@DEDUCTIBLE_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_TERM">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<!-- calculate only for additional amount -->
		<xsl:choose>
			<xsl:when test="BUILDINGIMPROVEMENTS_ADDITIONAL = '' or BUILDINGIMPROVEMENTS_ADDITIONAL &lt;= 0">
		0.00
		</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR1" select="BUILDINGIMPROVEMENTS_ADDITIONAL" />
				<xsl:value-of select="round(($VAR1 div $P_RATES_PER_VALUE) * $PBUILDING_IMPROVEMENTS_ADDITIONAL * $VAR_TERM)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Building Improvements/Alteration  (END)					  		  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							 Increased Coverage D - Rental Value  (START)			  		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="INCREASED_COVERAGE_D_ADDITIONAL">
		<xsl:variable name="P_RATES_PER_VALUE">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/@RATES_PER_VALUE" />
		</xsl:variable>
		<!-- get form  code-->
		<xsl:variable name="P_FORM_CODE" select="PRODUCTNAME" />
		<!-- get deductible -->
		<xsl:variable name="P_DEDUCTIBLE" select="DEDUCTIBLE" />
		<!-- get factor for Increased Coverage D - Rental Value -->
		<xsl:variable name="PRENTAL_VALUE_ADDITIONAL">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'RENTAL_VALUE' and @FORM_CODE=$P_FORM_CODE and @DEDUCTIBLE=$P_DEDUCTIBLE]/@DEDUCTIBLE_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_TERM">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<!-- calculate only for additional amount -->
		<xsl:choose>
			<xsl:when test="RENTALVALUE_ADDITIONAL = '' or RENTALVALUE_ADDITIONAL &lt;= 0">
		0.00
		</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR1" select="RENTALVALUE_ADDITIONAL" />
				<xsl:value-of select="round(round(($VAR1 div $P_RATES_PER_VALUE) * $PRENTAL_VALUE_ADDITIONAL) * $VAR_TERM)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							 Increased Coverage D - Rental Value  (END)				  		  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--					 Increased Coverage C - Landlords Personal Property  (START)			  -->
	<!-- ============================================================================================ -->
	<xsl:template name="COVERAGE_C_ADDITIONAL">
		<xsl:variable name="P_RATES_PER_VALUE">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/@RATES_PER_VALUE" />
		</xsl:variable>
		<!-- get form  code-->
		<xsl:variable name="P_FORM_CODE" select="PRODUCTNAME" />
		<!-- get deductible -->
		<xsl:variable name="P_DEDUCTIBLE" select="DEDUCTIBLE" />
		<!-- get factor for Increased Coverage C - Landlords Personal Property -->
		<xsl:variable name="PCOVERAGE_C_ADDITIONAL">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'COVERAGE_C' and @FORM_CODE=$P_FORM_CODE and @DEDUCTIBLE=$P_DEDUCTIBLE]/@DEDUCTIBLE_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_TERM">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<!-- calculate only for additional amount -->
		<xsl:choose>
			<xsl:when test="PERSONALPROPERTY_ADDITIONAL = '' or PERSONALPROPERTY_ADDITIONAL &lt;=0">
		0.00
		</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR1" select="PERSONALPROPERTY_ADDITIONAL" />
				<xsl:value-of select="($VAR1 div $P_RATES_PER_VALUE) * $PCOVERAGE_C_ADDITIONAL *$VAR_TERM" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							 Increased Coverage C - Landlords Personal Property   (END)		  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--									Contents In Storage (START)								  -->
	<!-- ============================================================================================ -->
	<xsl:template name="CONTENTS_IN_STORAGE_ADDITIONAL">
		<xsl:variable name="P_RATES_PER_VALUE">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/@RATES_PER_VALUE" />
		</xsl:variable>
		<!-- get form  code-->
		<xsl:variable name="P_FORM_CODE" select="PRODUCTNAME" />
		<!-- get deductible -->
		<xsl:variable name="P_DEDUCTIBLE" select="DEDUCTIBLE" />
		<!-- get factor for Contents In Storage -->
		<xsl:variable name="PCONTENTSINSTORAGE_ADDITIONAL">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'CONTENTS_IN_STORAGE' and @FORM_CODE=$P_FORM_CODE and @DEDUCTIBLE=$P_DEDUCTIBLE]/@DEDUCTIBLE_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_TERM">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<!-- calculate only for additional amount -->
		<xsl:choose>
			<xsl:when test="CONTENTSINSTORAGE_ADDITIONAL = '' or CONTENTSINSTORAGE_ADDITIONAL &lt;= 0">
			0.00
		</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR1" select="CONTENTSINSTORAGE_ADDITIONAL" />
				<xsl:value-of select="round(($VAR1 div $P_RATES_PER_VALUE) * $PCONTENTSINSTORAGE_ADDITIONAL *$VAR_TERM)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--									Contents In Storage (END)								  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--						TEMPLATE FOR SECTION II LANDLORDS LIABILITY(LP-124) (START)			  -->
	<!-- ============================================================================================ -->
	<xsl:template name="PROPFEE">
		<xsl:choose>
			<xsl:when test="QQNUMBER!=''">
				<xsl:choose>
					<xsl:when test="TERMFACTOR = 12 and STATENAME ='MICHIGAN'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PROPRTYFEE']/NODE[@ID='PFEE']/ATTRIBUTES/@ANNUAL_FEES" />
					</xsl:when>
					<xsl:when test="TERMFACTOR = 6 and STATENAME ='MICHIGAN'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PROPRTYFEE']/NODE[@ID='PFEE']/ATTRIBUTES/@SEMI_ANNUAL_FEES" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="TERMFACTOR = 12 and STATENAME ='MICHIGAN' and TEMPEXPFEE='Y'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PROPRTYFEE']/NODE[@ID='PFEE']/ATTRIBUTES/@ANNUAL_FEES" />
					</xsl:when>
					<xsl:when test="TERMFACTOR = 6 and STATENAME ='MICHIGAN' and TEMPEXPFEE='Y'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PROPRTYFEE']/NODE[@ID='PFEE']/ATTRIBUTES/@SEMI_ANNUAL_FEES" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template Property Expense Fee (START)							  -->
	<!--		    							FOR MICHIGAN										  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--									Additional Coverage Template (START) 					  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADDITIONALCOVERAGE">
		<!-- Section II: Liability Charges -->
		<xsl:variable name="VAR1">
			<xsl:call-template name="LP_124_LANDLORDS_LIABILITY" />
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="PROPFEE" />
		</xsl:variable>
		<!-- Mine Subsidence -->
		<xsl:variable name="VAR3">
			<xsl:call-template name="MINE_SUBSIDENCE_COVERAGE_PREMIUM" />
		</xsl:variable>
		<!-- Other Endorsements and Coverages -->
		<xsl:variable name="VAR4">
			<xsl:call-template name="EARTHQUAKE" />
		</xsl:variable>
		<xsl:variable name="VAR5">
			<xsl:call-template name="INSTALLATION_FLOATER_IF_184_BUILDING_MATERIALS" />
		</xsl:variable>
		<xsl:variable name="VAR6">
			<xsl:call-template name="INSTALLATION_FLOATER_IF_184_NON_STRUCTURAL_EQUIPMENT" />
		</xsl:variable>
		<xsl:variable name="VAR7">
			<xsl:call-template name="TREES_LAWNS_AND_SHRUBS_ADDITIONAL" />
		</xsl:variable>
		<xsl:variable name="VAR8">
			<xsl:call-template name="RADIO_AND_TV_EQUIPMENT_ADDITIONAL" />
		</xsl:variable>
		<xsl:variable name="VAR9">
			<xsl:call-template name="SATELLITE_DISHES_ADDITIONAL" />
		</xsl:variable>
		<xsl:variable name="VAR10">
			<xsl:call-template name="AWNINGS_AND_CANOPIES_ADDITIONAL" />
		</xsl:variable>
		<xsl:variable name="VAR11">
			<xsl:call-template name="COVERAGE_B_ADDITIONAL" />
		</xsl:variable>
		<xsl:variable name="VAR12">
			<xsl:call-template name="BUILDING_IMPROVEMENTS_ADDITIONAL" />
		</xsl:variable>
		<xsl:variable name="VAR13">
			<xsl:call-template name="INCREASED_COVERAGE_D_ADDITIONAL" />
		</xsl:variable>
		<xsl:variable name="VAR14">
			<xsl:call-template name="COVERAGE_C_ADDITIONAL" />
		</xsl:variable>
		<xsl:variable name="VAR15">
			<xsl:call-template name="CONTENTS_IN_STORAGE_ADDITIONAL" />
		</xsl:variable>
		<xsl:variable name="VAR16">
			<xsl:call-template name="LIMITED_LEAD_LIABILITY" />
		</xsl:variable>
		<!-- Calculations -->
		<xsl:value-of select="round(round(round(round(round(round(round(round(round(round(round(round(round(round(round($VAR1+$VAR2)+$VAR3)+$VAR4)+$VAR5)+$VAR6)+$VAR7)+$VAR8)+$VAR9)+$VAR10)+$VAR11)+$VAR12)+$VAR13)+$VAR14)+$VAR15)+$VAR16)" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--									Additional Coverage Template (END)		  				  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--									Final Primium Template (START)   						  -->
	<!-- ============================================================================================ -->
	<xsl:template name="FINALPRIMIUM">
		<xsl:variable name="VAR_CALL_ADJUSTEDBASE">
			<xsl:call-template name="CALL_ADJUSTEDBASE" />
		</xsl:variable>
		<xsl:variable name="VAR_ADDITIONALCOVERAGE">
			<xsl:call-template name="ADDITIONALCOVERAGE" />
		</xsl:variable>
		<xsl:variable name="VAR_MINIMUM_PREMIUM">
			<xsl:call-template name="MINIMUM_PREMIUM" />
		</xsl:variable>
		<xsl:value-of select="$VAR_CALL_ADJUSTEDBASE + $VAR_ADDITIONALCOVERAGE + $VAR_MINIMUM_PREMIUM" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--									Final Primium Template (END)   							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="EARTHQUAKE_COVERAGE">
		<xsl:if test="STATENAME='MICHIGAN'">
		DP-469 Earthquake
	</xsl:if>
		<xsl:if test="STATENAME='INDIANA'">
		DP-470 Earthquake 
	</xsl:if>
	</xsl:template>
	<xsl:template name="REPAIR_COST_FACTOR">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='REPAIR_COST_FACTOR']/NODE[@ID='REPAIR_COST']/ATTRIBUTES/@FACTOR" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Templates for MINIMUM PREMIUM (START)									  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="MINIMUM_PREMIUM">
		<!-- Fetch the difference between  the reqd minimum premium and the premium calculated -->
		<xsl:variable name="DIFFERENCE_AMOUNT">
			<xsl:call-template name="PREMIUM_DIFFERENCE" />
		</xsl:variable>
		<!--
		IF (<xsl:call-template name="PREMIUM_DIFFERENCE"/> &gt; 0)
		THEN
			<xsl:call-template name="PREMIUM_DIFFERENCE"/>
		ELSE
			0-->
		<xsl:choose>
			<xsl:when test="$DIFFERENCE_AMOUNT &gt; 0">
				<xsl:value-of select="$DIFFERENCE_AMOUNT" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PREMIUM_DIFFERENCE">
		<!-- Subtract Minimum Premium  from the total premium. 
		(
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINIMUM_RENTAL_PREMIUM']/NODE[@ID ='MINIMUM_PREMIUM']/ATTRIBUTES/@MINIMUM_VALUE "/>
			-
			(<xsl:call-template name="CALL_ADJUSTEDBASE"/>  +
			 <xsl:call-template name="ADDITIONALCOVERAGE"/>)
		) -->
		<xsl:variable name="MINIMUM_PREMIUM" select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINIMUM_RENTAL_PREMIUM']/NODE[@ID ='MINIMUM_PREMIUM']/ATTRIBUTES/@MINIMUM_VALUE " />
		<xsl:variable name="VAR_ADJUSTEDBASE">
			<xsl:call-template name="CALL_ADJUSTEDBASE" />
		</xsl:variable>
		<xsl:variable name="VAR_ADDITIONALCOVERAGE">
			<xsl:call-template name="ADDITIONALCOVERAGE" />
		</xsl:variable>
		<xsl:value-of select="($MINIMUM_PREMIUM - ($VAR_ADJUSTEDBASE + $VAR_ADDITIONALCOVERAGE))" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Templates for MINIMUM PREMIUM (END)											  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="INCREASED_COV_B">
		<xsl:variable name="VAR_COVERAGE_B_ADDITIONAL">
			<xsl:call-template name="COVERAGE_B_ADDITIONAL" />
		</xsl:variable>
		<xsl:value-of select="round($VAR_COVERAGE_B_ADDITIONAL)" />
	</xsl:template>
	<xsl:template name="COVERAGE_C_INCR">
		<xsl:variable name="VAR_COVERAGE_C_ADDITIONAL">
			<xsl:call-template name="COVERAGE_C_ADDITIONAL" />
		</xsl:variable>
		<xsl:value-of select="round($VAR_COVERAGE_C_ADDITIONAL)" />
	</xsl:template>
	<xsl:template name="COVERAGE_D_INCR">
		<xsl:variable name="VAR_COVERAGE_D_ADDITIONAL">
			<xsl:call-template name="INCREASED_COVERAGE_D_ADDITIONAL" />
		</xsl:variable>
		<xsl:value-of select="round($VAR_COVERAGE_D_ADDITIONAL)" />
	</xsl:template>
	<xsl:template name="CONTENTS_IN_STORAGE">
		<xsl:variable name="VAR_CONTENTS_IN_STORAGE">
			<xsl:call-template name="CONTENTS_IN_STORAGE_ADDITIONAL" />
		</xsl:variable>
		<xsl:value-of select="round($VAR_CONTENTS_IN_STORAGE)" />
	</xsl:template>
	<xsl:template name="COMPONENT_CODE">
		<xsl:param name="FACTORELEMENT" />
		<xsl:choose>
			<xsl:when test="$FACTORELEMENT=normalize-space('COV_A')">
				<xsl:if test="STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>793</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME=normalize-space('INDIANA')">
					<xsl:text>773</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('COV_B')">
				<xsl:if test="STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>794</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME=normalize-space('INDIANA')">
					<xsl:text>774</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('COV_C')">
				<xsl:if test="STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>795</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME=normalize-space('INDIANA')">
					<xsl:text>775</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('COV_D')">
				<xsl:if test="STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>796</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME=normalize-space('INDIANA')">
					<xsl:text>776</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('COV_E')">
				<xsl:if test="STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>797</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME=normalize-space('INDIANA')">
					<xsl:text>777</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('COV_F')">
				<xsl:if test="STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>798</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME=normalize-space('INDIANA')">
					<xsl:text>778</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('INCI_OFCE')">
				<xsl:if test="STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>815</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME=normalize-space('INDIANA')">
					<xsl:text>816</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('ERTHQKE')">
				<xsl:if test="STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>808</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME=normalize-space('INDIANA')">
					<xsl:text>788</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('MNE_SBS')">
				<xsl:if test="STATENAME=normalize-space('INDIANA')">
					<xsl:text>792</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('INCR_COV_B')">
				<xsl:if test="STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>799</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME=normalize-space('INDIANA')">
					<xsl:text>779</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('BLDG_ALT')">
				<xsl:if test="STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>800</xsl:text>	
				</xsl:if>
				<xsl:if test="STATENAME=normalize-space('INDIANA')">
					<xsl:text>780</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('INCR_COV_C')">
				<xsl:if test="STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>795</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME=normalize-space('INDIANA')">
					<xsl:text>775</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('INCR_COV_D')">
				<xsl:if test="STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>796</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME=normalize-space('INDIANA')">
					<xsl:text>776</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('CNTNTS_STRG')">
				<xsl:if test="STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>802</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME=normalize-space('INDIANA')">
					<xsl:text>782</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('TREES')">
				<xsl:if test="STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>804</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME=normalize-space('INDIANA')">
					<xsl:text>784</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('RADIO')">
				<xsl:if test="STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>805</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME=normalize-space('INDIANA')">
					<xsl:text>785</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('SAT_DISH')">
				<xsl:if test="STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>806</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME=normalize-space('INDIANA')">
					<xsl:text>786</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('AWNG')">
				<xsl:if test="STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>807</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME=normalize-space('INDIANA')">
					<xsl:text>787</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('INST_FLTR_BLDG')">
				<xsl:if test="STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>810</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME=normalize-space('INDIANA')">
					<xsl:text>790</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('INST_FLTR_NON_STR')">
				<xsl:if test="STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>811</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME=normalize-space('INDIANA')">
					<xsl:text>791</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('LEAD_LIA')">
				<xsl:if test="STATENAME=normalize-space('MICHIGAN')">
					<xsl:text>978</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('PRP_EXPNS_FEE')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>20010</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>
