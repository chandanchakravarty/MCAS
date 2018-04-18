<!-- ============================================================================================ -->
<!-- File Name		:	Primium.xsl																  -->
<!-- Description	:	This xsl file is for generating 
						the Final Primium (For all Homeowner Products)INDIANA and MICHIGAN		  -->
<!-- Developed By	:	Shrikant Bhatt															  -->
<!-- ============================================================================================ -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:user="urn:user-namespace-here">
	<xsl:output encoding="ISO8859-1" omit-xml-declaration="no" method="xml" />
	<!-- Loading ProductFactorMaster File -->
	<xsl:variable name="HOCoveragesDoc" select="document('FactorPath')"></xsl:variable>
	<!-- Lookup id 1195,1216 in masterlookup_values. these are distinct codes. 
	 Indiana and michigan states have same code for same policy types -->
	<xsl:variable name="POLICYCODE_HO2REPAIR" select="'HO-2^REPAIR'" />
	<xsl:variable name="POLICYCODE_HO2REPLACE" select="'HO-2^REPLACE'" />
	<xsl:variable name="POLICYCODE_HO3PREMIER" select="'HO-3^PREMIER'" />
	<xsl:variable name="POLICYCODE_HO3REPAIR" select="'HO-3^REPAIR'" />
	<xsl:variable name="POLICYCODE_HO3REPLACE" select="'HO-3^REPLACE'" />
	<xsl:variable name="POLICYCODE_HO4DELUXE" select="'HO-4^DELUXE'" />
	<xsl:variable name="POLICYCODE_HO4TENANT" select="'HO-4^TENANT'" />
	<xsl:variable name="POLICYCODE_HO5PREMIER" select="'HO-5^PREMIER'" />
	<xsl:variable name="POLICYCODE_HO5REPLACE" select="'HO-5^REPLACE'" />
	<xsl:variable name="POLICYCODE_HO6DELUXE" select="'HO-6^DELUXE'" />
	<xsl:variable name="POLICYCODE_HO6UNIT" select="'HO-6^UNIT'" />
	<!-- Break up of the above -->
	<!-- Policy types -->
	<xsl:variable name="POLICYTYPE_HO2" select="'HO-2'" />
	<xsl:variable name="POLICYTYPE_HO3" select="'HO-3'" />
	<xsl:variable name="POLICYTYPE_HO4" select="'HO-4'" />
	<xsl:variable name="POLICYTYPE_HO5" select="'HO-5'" />
	<xsl:variable name="POLICYTYPE_HO6" select="'HO-6'" />
	<!-- to check ProductPremier node-->
	<xsl:variable name="POLICYDESC_REPAIR" select="'REPAIR'" />
	<xsl:variable name="POLICYDESC_REPLACEMENT" select="'REPLACE'" />
	<xsl:variable name="POLICYDESC_DELUXE" select="'DELUXE'" />
	<xsl:variable name="POLICYDESC_TENANT" select="'TENANT'" />
	<xsl:variable name="POLICYDESC_PREMIER" select="'PREMIER'" />
	<xsl:variable name="POLICYDESC_UNIT" select="'UNIT'" />
	<!-- State names -->
	<xsl:variable name="STATE_MICHIGAN" select="'MICHIGAN'" />
	<xsl:variable name="STATE_INDIANA" select="'INDIANA'" />
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
				<PRODUCT PRODUCTID="0" DESC="Policy Form:">
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
						<SUBGROUP>
							<IF>
								<STEP STEPID="0"> <!-- Coverage A - Dwelling -->
									<PATH>
										<xsl:call-template name="COVERAGEADWELLING" />
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<STEP STEPID="1"> <!-- COVERAGE - B -->
								<PATH>
									<xsl:choose>
										<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO6 or PRODUCTNAME = $POLICYTYPE_HO4">0</xsl:when>
										<xsl:otherwise>ND</xsl:otherwise>
									</xsl:choose>
								</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="2"> <!-- COVERAGE - C -->
									<PATH>
										<xsl:choose>
											<!-- Coverage C considered for HO4 and HO-6 -->
											<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 or PRODUCTNAME = $POLICYTYPE_HO6">  
										{
											<xsl:call-template name="CALL_ADJUSTEDBASE" /> 
										} 
									</xsl:when>
											<!-- For all the others, nothing to  be displayed in premium column -->
											<xsl:otherwise>ND</xsl:otherwise>
										</xsl:choose>
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="3"> <!-- COVERAGE - D -->
									<PATH>
										<xsl:variable name="VAR1">
											<xsl:call-template name="COVERAGE_D_PREMIUM" />
										</xsl:variable>
										<xsl:choose>
											<!-- To be shown only for HO6  policy  -->
											<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO6 or PRODUCTNAME = $POLICYTYPE_HO4">
												<xsl:choose>
													<xsl:when test="$VAR1 = '0'">ND</xsl:when>
													<xsl:otherwise>
														<xsl:value-of select="$VAR1" />
													</xsl:otherwise>
												</xsl:choose>
											</xsl:when>
											<xsl:otherwise>ND</xsl:otherwise>
										</xsl:choose>
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
					</GROUP>
					<GROUP GROUPID="4">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<SUBGROUP>
							<STEP STEPID="4"> <!-- - Coverage E - Personal Liability - Each Occurrence -->
								<PATH>
									<!--If Personal Liability is "Extended From Home" then Premium should not be calculated - Asfa Praveen - 03-JULY-2007 -->
									<xsl:choose>
										<!--When Personal Liability holds "EFH", Show "Extended" text - Asfa Praveen - 03-July-2007 -->
										<xsl:when test="normalize-space(PERSONALLIABILITY_LIMIT) = 'EFH'">Extended</xsl:when>
										<xsl:otherwise>
											<xsl:choose>
												<xsl:when test="WOLVERINEINSURESPRIMARY ='Y'">Extended</xsl:when>
												<xsl:otherwise>ND</xsl:otherwise>
											</xsl:choose>
										</xsl:otherwise>
									</xsl:choose>
								</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<STEP STEPID="5"> <!-- - Coverage F - Medical Payments to Others - Each Person -->
								<PATH>
									<!--If Medical Payments is "Extended From Home" then Premium should not be calculated - Asfa Praveen - 03-JULY-2007 -->
									<xsl:choose>
										<!--When Medical Payments holds "EFH", Show "Extended" text - Asfa Praveen - 03-July-2007 -->
										<xsl:when test="normalize-space(MEDICALPAYMENTSTOOTHERS_LIMIT) = 'EFH'">Extended</xsl:when>
										<xsl:otherwise>
											<xsl:choose>
												<xsl:when test="WOLVERINEINSURESPRIMARY ='Y'">Extended</xsl:when>
												<xsl:otherwise>ND</xsl:otherwise>
											</xsl:choose>
										</xsl:otherwise>
									</xsl:choose>
								</PATH>
							</STEP>
						</SUBGROUP>
					</GROUP>
					<GROUP GROUPID="5">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<SUBGROUP>
							<IF>
								<STEP STEPID="6"> <!-- Insurance Score -->
									<PATH>
										<xsl:call-template name="INSURANCE_SCORE_CREDIT_DISPLAY" />
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="7"> <!-- Smoker -->
									<PATH>
										<xsl:call-template name="SMOKER_DISPLAY" />
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="8"> <!-- Information: Maximum Discount -->
									<PATH>
										<!--xsl:call-template name="MAX_DISCOUNT_APPLIED_DISPLAY" /-->
										<xsl:variable name="VAR1">
											<xsl:call-template name="MAX_DISCOUNT_APPLIED_DISPLAY" />
										</xsl:variable>
										<xsl:choose>
											<xsl:when test="$VAR1 &gt; 0">ND</xsl:when>
											<xsl:otherwise>
												<xsl:value-of select="$VAR1" />
											</xsl:otherwise>
										</xsl:choose>
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<STEP STEPID="9"> <!-- Dwellng  under construction -->
								<PATH>
									<xsl:call-template name="DWELLING_UNDER_CONSTRUCTION_DISPLAY"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<STEP STEPID="10"> <!-- Experience Factor -->
								<PATH>
									<xsl:call-template name="EXPERIENCEFACTOR_DISPLAY"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<STEP STEPID="11"> <!-- Age of Home -->
								<PATH>
									<xsl:call-template name="AGEOFHOME_DISPLAY"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="12"> <!-- Protective Device -->
									<PATH>
								{
									<xsl:call-template name="PROTECTIVEDEVICE_DISPLAY"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<STEP STEPID="13"> <!-- Multi-policy display -->
								<PATH>
									<xsl:call-template name="MULTIPOLICY_DISPLAY"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<STEP STEPID="14"> <!-- Valued customer -->
								<PATH>
									<xsl:call-template name="VALUEDCUSTOMER_DISPLAY"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<STEP STEPID="15"> <!-- Replacement Cost -->
								<PATH>
									<xsl:choose>
										<xsl:when test="PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT">
											<xsl:call-template name="REPLACEMENT_COST_DISPLAY" />
										</xsl:when>
										<xsl:otherwise>0</xsl:otherwise>
									</xsl:choose>
								</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<STEP STEPID="16"> <!-- Prior Lost -->
								<PATH>
									<xsl:call-template name="PRIOR_LOSS_DISPLAY"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="17"> <!-- Wood Stove -->
									<PATH>
										<xsl:call-template name="WSTOVE_DISPLAY"></xsl:call-template>
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="18"> <!-- Breed of Dog -->
									<PATH>
								{	<xsl:variable name="VAR_BREEDDOG">
											<xsl:call-template name="BREEDDOG" />
										</xsl:variable>
									<xsl:choose>
											<xsl:when test="$VAR_BREEDDOG = 1.00 or BREEDOFDOG = 'OTH'">0</xsl:when>
											<xsl:otherwise>
												<xsl:value-of select="$VAR_BREEDDOG" />
											</xsl:otherwise>
										</xsl:choose>
								}
								 
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="19"> <!-- Seasonal Secondary -->
									<PATH>
										<xsl:call-template name="SEASONALSECONDARY_DISPLAY" />
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
					</GROUP>
					<GROUP GROUPID="6">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<SUBGROUP>
							<STEP STEPID="20"> <!-- Preferred Plus -->
								<PATH>
									<xsl:call-template name="PREFERRED_PLUS_DISPLAY"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<STEP STEPID="21"> <!-- HO-64 Renters Deluxe Endorsement -->
								<PATH>
									<xsl:call-template name="HO64_RENTERS_DELUXE_ENDORSEMENT"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="22"> <!-- HO-66 Condominium Deluxe Endorsement -->
									<PATH>
										<xsl:call-template name="HO66_CONDOMINIUM_DELUXE_ENDORSEMENT"></xsl:call-template>
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="23">{ 
									<PATH> <!--  HO-34 Replacement Cost Personal Property-->
										<xsl:call-template name="SELECT_HO_34_DISPLAY"></xsl:call-template>
									</PATH>} 
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="24"> <!-- HO-11 Expanded Replacement Coverage for Building -->
									<PATH>
								{
									<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY_DISPLAY"></xsl:call-template>
								}	
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="25"> <!-- HO-96 Increased Fire Dept. Service Charge -->
									<PATH>
								{
									<xsl:call-template name="HO_96_REPLACEMENT_COST_PERSONAL_PROPERTY"></xsl:call-template>
								}	
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="26"> <!-- HO-312 Business Property Increased Limits -->
									<PATH>
										<xsl:call-template name="HO_312_BUSINESS_PROPERTY_INCREASED_LIMITS"></xsl:call-template>
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="27"> <!--HO-48 Other Structures - Increased Limits -->
									<PATH>
								{
									<xsl:call-template name="HO_48_OTHER_STRUCTURE_INCREASED_LIMITS"></xsl:call-template>
								}	
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="28"> <!-- HO-40 Other Structures - Rented to Others -->
									<PATH>
								{
									<xsl:call-template name="HO_40_OTHER_STRUCTURE_RENTED_TO_OTHERS"></xsl:call-template>
								}	
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="29"> <!-- HO-42 Other Structure With Incidental --> <!--This step is no more used-->
									<PATH>
								{
									
									<xsl:call-template name="HO_42_OTHER_STRUCTURE_WITH_INCIDENTAL"></xsl:call-template>
								}	
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="30"> <!-- HO-50 Personal Property Away From Premises  -->
									<PATH>
										<xsl:call-template name="HO50_PERSONAL_PROPERTY_AWAY_PREMISES"></xsl:call-template>
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="31"> <!-- Building Additions and Alterations (HO-51) in case of ho4 -->
									<PATH>
								{
								<xsl:choose>
											<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4">
												<xsl:call-template name="HO51_BULDING_ALTER"></xsl:call-template>
											</xsl:when>
											<xsl:otherwise>
										0
									</xsl:otherwise>
										</xsl:choose>
						       }
									 
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="32"> <!-- HO-53 Increased Credit Card -->
									<PATH>
								{
									<xsl:call-template name="HO_53_INCREASED_CREDIT_CARD"></xsl:call-template>
								}	
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Heading HO65 -->
						<SUBGROUP>
							<STEP STEPID="33"> <!--HO 65 Heading -->
								<PATH>
									<xsl:variable name="VAR_HO65">
										<xsl:call-template name="HO_65_ABOVE_0" />
									</xsl:variable>
									<xsl:choose>
										<xsl:when test="normalize-space($VAR_HO65)='Y'">ND</xsl:when>
										<xsl:otherwise>0</xsl:otherwise>
									</xsl:choose>
								</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="34"> <!-- Unscheduled Jewelry, Watches & Fur -->
									<PATH>
								{
									<xsl:call-template name="UNSCHEDULED_JEWELRY_ADDITIONAL" /> 
								}	
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="35"> <!-- Unscheduled Jewelry, Watches & Fur -->
									<PATH>
								{
									<xsl:call-template name="MONEY"></xsl:call-template>
								}	
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="36"> <!-- Securities -->
									<PATH>
								{
									<xsl:call-template name="SECURITIES"></xsl:call-template>
								}	
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="37"> <!-- Silverware, Goldware, Etc. -->
									<PATH>
								{
									<xsl:call-template name="SILVERWARE_GOLDWARE"></xsl:call-template>
								}	
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="38"> <!-- Firearms -->
									<PATH>
								{
									<xsl:call-template name="FIREARMS"></xsl:call-template>
								}	
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="39"> <!-- HO-35  loss assessment in case of ho6 -->
									<PATH>
								{
									<xsl:choose>
											<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO6">
												<xsl:call-template name="HO35_LOSS_ASSESSMENT"></xsl:call-template>
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
								<STEP STEPID="40"> <!-- HO-277 Ordinance or Law coverage Forms -->
									<PATH>
								{
									<xsl:call-template name="ORDINANCE" />
									 
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="41"> <!-- HO-32 Unit Owners Coverage A Special Coverage -->
									<PATH>
										<xsl:call-template name="HO32_UNIT_OWNER"></xsl:call-template>
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="42"> <!-- HO-455 Identity Fraud Expense Coverage -->
									<PATH>
								{
									<xsl:call-template name="HO_455_IDENTITY_FRAUD" />
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="43"> <!-- HO-315 Earthquake -->
									<PATH>
							{	
								<xsl:call-template name="HO_315_EARTHQUAKE"></xsl:call-template>
							}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="44"> <!-- HO-327 Water Back-Up and Sump Pump Overflow -->
									<PATH>
								{
									<xsl:call-template name="HO_327_WATER_BACK_UP"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="45"> <!-- HO-33 Condo-Unit Owner Rental to Others. -->
									<PATH>
										<xsl:call-template name="HO_33_CONDO_UNIT"></xsl:call-template>
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<STEP STEPID="46"> <!-- HO 373 Indiana Contingent Workers  Compensation. -->
								<PATH>
									<xsl:choose>
										<xsl:when test="STATENAME ='INDIANA'">Included</xsl:when>
										<xsl:otherwise>0</xsl:otherwise>
									</xsl:choose>
								</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="47"> <!-- MINE SUBSIDENCE COVERAGE -->
									<PATH>
										<xsl:call-template name="HO_287_MINE_SUBSIDENCE_COVERAGE"></xsl:call-template>
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="48"> <!-- HO-9 Collapse From Sub-Surface Water -->
									<PATH>
								{
									<xsl:call-template name="HO_9_COLLAPSE_FROM_SUB_SURFACE_WATER"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- NEW STEP END-->
						<SUBGROUP>
							<IF>
								<STEP STEPID="49"> <!-- HO-490 Specific Structure(s) Away From Premises -->
									<PATH>
								{
									<xsl:call-template name="HO_490_SPECIFIC_STRUCTURED"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="50"> <!-- HO-489 Other Structures (Repair Cost Coverage) -->
									<PATH>
								{
									<xsl:call-template name="HO_489_SPECIFIC_STRUCTURED"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="51"> <!-- primary Residence (1 or 2 Family) -->
									<PATH>
								{
									<xsl:call-template name="PRIMARY_RESIDENCE"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="52"> <!-- Residence Employees  -->
									<PATH>
								{
									<xsl:call-template name="RESIDENCE_EMPLOYEES"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="53"> <!-- Additional Premises-Occupied By Insured -->
									<PATH>
								{
									<xsl:call-template name="ADDITIONAL_PREMISES"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="54"> <!-- HO-40 Additional Premises Rented to Others - Residence Premises -->
									<PATH>
								{
									<xsl:call-template name="ADDITIONAL_RESIDENCE_PREMISES"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="55"> <!--   HO-70 Additional Premises Rented - Other Location - 1 Family -->
									<PATH>
								{
									<xsl:call-template name="ADDITIONAL_OTHER_LOCATION_PREMISES_FAMILY1"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="56"> <!--   HO-70 Additional Premises Rented - Other Location - 2 Family -->
									<PATH>
								{
									<xsl:call-template name="ADDITIONAL_OTHER_LOCATION_PREMISES_FAMILY2"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="57"> <!--   HO-42 Incidental Office Private School or Studio On Premises -->
									<PATH>
								 {
									<xsl:call-template name="HO_42_ON_PREMISES"></xsl:call-template>
								 }
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="58"> <!-- Business Located in an Other Structure -->
									<PATH>
								 {
									<xsl:call-template name="HO42_BUSINESS_OTHER_STRUCTURE_WITH_INCIDENTAL"></xsl:call-template>
									}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="59"> <!-- HO-42 Incidental Office Private School or Studio Instruction Only -->
									<PATH>
								{
									<xsl:call-template name="HO_42_INSTRUCTION_ONLY"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="60"> <!-- HO-200 Waterbed Liability -->
									<PATH>
								{
									<xsl:call-template name="HO_200_WATERBED_LIABILITY"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="61"> <!-- HO-43 Incidental Office Private School or Studio Off Premises -->
									<PATH>
								{
									<xsl:call-template name="HO_43_OFF_PREMISES"></xsl:call-template> 
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<STEP STEPID="62">
								<PATH>
									<xsl:variable name="VAR_CLASSA">
										<xsl:call-template name="HO_71_CLASSA" />
									</xsl:variable>
									<xsl:variable name="VAR_CLASSB">
										<xsl:call-template name="HO_71_CLASSB" />
									</xsl:variable>
									<xsl:variable name="VAR_CLASSC">
										<xsl:call-template name="HO_71_CLASSC" />
									</xsl:variable>
									<xsl:variable name="VAR_CLASSD">
										<xsl:call-template name="HO_71_CLASSD" />
									</xsl:variable>
									<xsl:choose>
										<xsl:when test="$VAR_CLASSA &gt; 0 or $VAR_CLASSB &gt; 0 or $VAR_CLASSC &gt; 0 or $VAR_CLASSD &gt; 0">ND</xsl:when>
										<xsl:otherwise>0</xsl:otherwise>
									</xsl:choose>
								</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="63"> <!-- HO-71 Business Pursuits:Class A  -->
									<PATH>
								{
									<xsl:call-template name="HO_71_CLASSA"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="64"> <!-- HO-71 Business Pursuits:Class B -->
									<PATH>
								{
									<xsl:call-template name="HO_71_CLASSB"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="65"> <!-- HO-71 Business Pursuits:Class C -->
									<PATH>
								{
									<xsl:call-template name="HO_71_CLASSC"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="66"> <!-- HO-71 Business Pursuits:Class D  -->
									<PATH>
								{
									<xsl:call-template name="HO_71_CLASSD"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="67"> <!-- HO-72 Farm Liability - Incidental Farming Residence Premises -->
									<PATH>
								{
									<xsl:call-template name="HO_72_INCIDENTAL_FARMING"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="68"> <!-- HO-73 Farm Liability - Owned By Insured Rented to Others -->
									<PATH>
								{
									<xsl:call-template name="HO_73_OTH_LOC_OPR_OTHERS"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="69"> <!-- HO-73 Farm Liability - Owned/Operated By Insured/Insured Employee -->
									<PATH>
								{
									<xsl:call-template name="HO_73_OTH_LOC_OPR_EMPL"></xsl:call-template> 
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="70"> <!-- HO-82 Personal Injury -->
									<PATH>
								{
									<xsl:call-template name="HO_82_PERSONAL_INJURY_DISPLAY" /> 								
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Group 6-->
					</GROUP>
					<GROUP GROUPID="7">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<SUBGROUP>
							<IF>
								<STEP STEPID="71"> <!-- Bicycle (HO-900) -->
									<PATH>
								{
									<xsl:call-template name="HO_900_BICYCLES_PREMIUM"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="72"> <!-- Cameras (Non-Professional) -->
									<PATH>
								{
									<xsl:call-template name="CAMERAS_PREMIUM"></xsl:call-template>	
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="73"> <!-- Cellular Phones (HO-900) -->
									<PATH>
								{
									<xsl:call-template name="HO_900_CELLULAR_PHONES_PREMIUM"></xsl:call-template> 
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="74"> <!-- Furs -->
									<PATH>
								{
									<xsl:call-template name="FURS_PREMIUM"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="75"> <!-- Golf Equipment -->
									<PATH>
								{
									<xsl:call-template name="GOLF_EQUIPMENT_PREMIUM"></xsl:call-template> 
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="76"> <!-- Guns -->
									<PATH>
								{
									<xsl:call-template name="GUNS_PREMIUM"></xsl:call-template> 
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="77"> <!-- Jewelry -->
									<PATH>
								{
									<xsl:call-template name="JEWELRY_PREMIUM"></xsl:call-template> 
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="78"> <!-- Musical (Non-Professional) -->
									<PATH>
								{
									<xsl:call-template name="MUSICAL_NON_PROFESSIONAL_PREMIUM"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="79"> <!-- Personal Computers (HO-214) -->
									<PATH>
								{
									<xsl:call-template name="PERSONAL_COMPUTERS_PREMIUM"></xsl:call-template> 
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="80"> <!-- Silver -->
									<PATH>
								{
									<xsl:call-template name="SILVER_PREMIUM"></xsl:call-template> 
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="81"> <!-- Stamps -->
									<PATH>
								{
									<xsl:call-template name="STAMPS_PREMIUM"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="82"> <!-- Rare Coins -->
									<PATH>
								{
									<xsl:call-template name="RARE_COINS_PREMIUM"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="83"> <!-- Fine Arts - Without Breakage -->
									<PATH>
								{
									<xsl:call-template name="FINE_ARTS_WITHOUT_BREAKAGE_PREMIUM"></xsl:call-template> 
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="84"> <!-- Fine Arts - With Breakage -->
									<PATH>
								{
									<xsl:call-template name="FINE_ARTS_BREAKAGE_PREMIUM"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="85"> <!-- Handicap Electronics -->
									<PATH>
								{
									<xsl:call-template name="HANDICAP_ELECTRONICS"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="86"> <!-- Hearing Aids -->
									<PATH>
								{
									<xsl:call-template name="HEARING_AIDS"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="87"> <!--  Insulin Pumps -->
									<PATH>
								{
									<xsl:call-template name="INSULIN_PUMPS"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="88"> <!--  Mart Kay/Amway -->
									<PATH>
								{
									<xsl:call-template name="MART_KAY_AMWAY"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="89"> <!-- Personal Computers - Laptop -->
									<PATH>
								{
									<xsl:call-template name="PERSONAL_COMPUTERS_LAPTOP"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="90"> <!-- Salesman Supplies -->
									<PATH>
								{
									<xsl:call-template name="SALESMAN_SUPPLY"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="91"> <!-- Scuba Diving Equipment -->
									<PATH>
								{
									<xsl:call-template name="SCUBA_DIVING"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="92"> <!-- Snow Skies -->
									<PATH>
								{
									<xsl:call-template name="SNOW_SKY"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="93"> <!--Tack and Saddle   -->
									<PATH>
								{
									<xsl:call-template name="TACK_SADDLE"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="94"> <!-- Tools (At Premises)  -->
									<PATH>
								{
									<xsl:call-template name="TOOLS_AT_PREMISIS"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="95"> <!-- Tools (At Business Location) -->
									<PATH>
								{
									<xsl:call-template name="TOOLS_AT_BSNS_LOCN"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="96"> <!-- Tractors -->
									<PATH>
								{
									<xsl:call-template name="TRACTORS"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="97"> <!-- Train Collections -->
									<PATH>
								{
									<xsl:call-template name="TRAIN_COLLECTION"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="98"> <!-- Wheelchairs/Scooters -->
									<PATH>
								{
									<xsl:call-template name="WHEELCHAIR"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
					</GROUP>
					<GROUP GROUPID="8">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<SUBGROUP>
							<IF>
								<STEP STEPID="99"> <!-- Property Expense Fee -->
									<PATH>
								{
									<xsl:call-template name="PROPFEE" /> 
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
					</GROUP>
					<GROUP GROUPID="9">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<!-- Minimum  Premium -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="100">
									<PATH>
							{
							 <xsl:call-template name="MINIMUM_PREMIUM" />  
							}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Final Premium -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="101">
									<PATH>
										<xsl:call-template name="FINALPRIMIUM" />
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
						<xsl:call-template name="PRODUCTID0"></xsl:call-template>
					</xsl:attribute>
					<GROUP GROUPID="0" CALC_ID="10000">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID0"></xsl:call-template>
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<GROUPRULE></GROUPRULE>
						<GROUPFORMULA></GROUPFORMULA>
						<GDESC></GDESC>
					</GROUP>
					<GROUP GROUPID="1" CALC_ID="10001">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID1"></xsl:call-template>
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<GROUPRULE></GROUPRULE>
						<GROUPFORMULA></GROUPFORMULA>
						<GDESC></GDESC>
					</GROUP>
					<GROUP GROUPID="2" CALC_ID="10002">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID2"></xsl:call-template>
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<GROUPRULE></GROUPRULE>
						<GROUPFORMULA></GROUPFORMULA>
						<GDESC></GDESC>
					</GROUP>
					<GROUP GROUPID="3" CALC_ID="10003" PREFIX="" SUFIX="" ISCALCULATE="F">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID3"></xsl:call-template>
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<GROUPRULE></GROUPRULE>
						<GDESC></GDESC>
						<!-- Coverage A - Dwelling -->
						<STEP STEPID="0" CALC_ID="1000" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID0"></xsl:call-template>
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH>
								<xsl:value-of select="DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:variable name="VAR1">
									<xsl:call-template name="COMBINE_COVERAGEA" />
								</xsl:variable>
								<xsl:value-of select="format-number($VAR1, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>COV_A</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="COVERAGEADWELLING" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'COV_A'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- COVERAGE - B -->
						<STEP STEPID="1" CALC_ID="1001" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID1"></xsl:call-template>
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH>
								<xsl:value-of select="DEDUCTIBLE"></xsl:value-of>
							</D_PATH>
							<L_PATH>
								<xsl:variable name="VAR1">
									<xsl:value-of select="HO48INCLUDE" />
								</xsl:variable>
								<xsl:value-of select="format-number($VAR1, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>COV_B</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'COV_B'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- COVERAGE - C -->
						<STEP STEPID="2" CALC_ID="1002" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID2"></xsl:call-template>
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH>
								<xsl:value-of select="DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:variable name="VAR1">
									<xsl:call-template name="COMBINEDPERSONALPROPERTY" />
								</xsl:variable>
								<xsl:value-of select="format-number($VAR1, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>COV_C</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="COVERAGE_C_PREMIUM" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'COV_C'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- COVERAGE - D -->
						<STEP STEPID="3" CALC_ID="1003" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID3"></xsl:call-template>
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH>
								<xsl:value-of select="DEDUCTIBLE"></xsl:value-of>
							</D_PATH>
							<L_PATH>
								<xsl:variable name="VAR1">
									<xsl:call-template name="COMBINEDADDITIONALLIVINGEXPENSE" />
								</xsl:variable>
								<xsl:value-of select="format-number($VAR1, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>COV_D</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="COVERAGE_D_PREMIUM" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'COV_D'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<GROUPFORMULA>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1000]</OPERAND>
							<OPERATOR>*</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1001]</OPERAND>
							<OPERATOR>*</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1002]</OPERAND>
							<OPERATOR>*</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1003]</OPERAND>
						</GROUPFORMULA>
					</GROUP>
					<GROUP GROUPID="4" CALC_ID="10004">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID4"></xsl:call-template>
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<GROUPRULE></GROUPRULE>
						<GDESC></GDESC>
						<!-- - Coverage E - Personal Liability - Each Occurrence -->
						<STEP STEPID="4" CALC_ID="1004" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID4"></xsl:call-template>
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<!--If Personal Liability is "Extended From Home" then Premium should not be calculated - Asfa Praveen - 03-JULY-2007 -->
								<xsl:call-template name="PERSONALLIABILITY_LIMIT_DISPLAY" />
								<!--xsl:choose>
									<xsl:when test="WOLVERINEINSURESPRIMARY ='Y'"></xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="format-number(PERSONALLIABILITY_LIMIT, '###,###')" />
									</xsl:otherwise>
								</xsl:choose-->
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>COV_E</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="LIABLITY_CHARGES" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'COV_E'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- - Coverage F - Medical Payments to Others - Each Person -->
						<STEP STEPID="5" CALC_ID="1005" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID5" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<!--If Medical Payments is "Extended From Home" then Premium should not be calculated - Asfa Praveen - 03-JULY-2007 -->
								<xsl:call-template name="MEDICALPAYMENTSTOOTHERS_LIMIT_DISPLAY" />
								<!--xsl:choose>
									<xsl:when test="WOLVERINEINSURESPRIMARY ='Y'"></xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="format-number(MEDICALPAYMENTSTOOTHERS_LIMIT, '###,###')" />
									</xsl:otherwise>
								</xsl:choose-->
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>COV_F</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="MEDICAL_CHARGES" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'COV_F'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<GROUPFORMULA>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1004]</OPERAND>
							<OPERATOR>*</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1005]</OPERAND>
							<OPERATOR>*</OPERATOR>
						</GROUPFORMULA>
					</GROUP>
					<GROUP GROUPID="5" CALC_ID="10005">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID5"></xsl:call-template>
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<GROUPRULE></GROUPRULE>
						<GDESC></GDESC>
						<!-- Insurance Score -->
						<STEP STEPID="6" CALC_ID="1006" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID6"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_INS_SCR</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS><xsl:call-template name="REMARKINSURANCE" /></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD><xsl:call-template name="INSURANCESCORE_PERCENT" /></COM_EXT_AD>
						</STEP>
						<!-- Smoker -->
						<STEP STEPID="7" CALC_ID="1007" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID7"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_SMKR</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS><xsl:call-template name="REMARKSMOKER" /></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD><xsl:call-template name="SMOKER_CREDIT_DISPLAY" />
								<xsl:text>%</xsl:text>
							</COM_EXT_AD>
						</STEP>
						<!-- Information: Maximum Discount -->
						<STEP STEPID="8" CALC_ID="1008" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID8"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>MAX_DIS</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Dwellng  under construction -->
						<STEP STEPID="9" CALC_ID="1009" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID9"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_DUC</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS><xsl:call-template name="REMARKCONSTRUCTION" /></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD><xsl:call-template name="DWELLING_UNDER_CONSTRUCTION_CREDIT"></xsl:call-template>
								<xsl:text>%</xsl:text>
							</COM_EXT_AD>
						</STEP>
						<!-- Experience Factor -->
						<STEP STEPID="10" CALC_ID="1010" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID10"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_EXP</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS><xsl:call-template name="REMARKEXPER" /></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD><xsl:call-template name="EXPERIENCECREDIT"></xsl:call-template>
								<xsl:text>%</xsl:text>
							</COM_EXT_AD>
						</STEP>
						<!-- New Steps START-->
						<!-- Age of Home -->
						<STEP STEPID="11" CALC_ID="1011" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID11"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_AGE_HOME</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS><xsl:call-template name="REMARKAGEHOME" /></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD><xsl:call-template name="AGEOFHOME_CREDIT" />
								<xsl:text>%</xsl:text>
							</COM_EXT_AD>
						</STEP>
						<!-- Protective Device -->
						<!-- <STEP STEPID="12"  CALC_ID="1012" PREFIX="-  Discount - Protective Devices " SUFIX ="%" ISCALCULATE="T">   -->
						<STEP STEPID="12" CALC_ID="1012" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID12"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_PROT_DVC</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS><xsl:call-template name="REMARKPROTDEVICE" /></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD>
								<xsl:call-template name="PROTECTIVEDEVICE_CREDIT" />
								<xsl:text>%</xsl:text>
							</COM_EXT_AD>
						</STEP>
						<!-- Multi-policy display -->
						<STEP STEPID="13" CALC_ID="1013" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID13"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_MULTIPOL</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS><xsl:call-template name="REMARKMULTIPOLICY" /></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD>
								<xsl:call-template name="MULTIPOLICY_CREDIT" />
								<xsl:text>%</xsl:text>
							</COM_EXT_AD>
						</STEP>
						<!-- New Steps END -->
						<!-- Valued customer -->
						<STEP STEPID="14" CALC_ID="1014" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID14"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_VALUED_CST</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS><xsl:call-template name="REMARKVALUDCUST" /></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD><xsl:call-template name="VALUEDCUSTOMER_FACTOR"></xsl:call-template>
								<xsl:text>%</xsl:text>
							</COM_EXT_AD>
						</STEP>
						<!-- Replacement Cost -->
						<STEP STEPID="15" CALC_ID="1015" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID15"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE></COMPONENT_CODE> <!--P_CENT_REPL_COST-->
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKREPLACEMENTCOST" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Prior Lost -->
						<STEP STEPID="16" CALC_ID="1016" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID16" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>S</COMPONENT_TYPE>
							<COMPONENT_CODE>PR_LSS</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS><xsl:call-template name="REMARKPRIORLOSS" /></COMP_REMARKS>
							<COMP_EXT>
								<!--xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'PR_LSS'"></xsl:with-param>
								</xsl:call-template-->
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Wood Stove -->
						<STEP STEPID="17" CALC_ID="1017" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID17" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>S</COMPONENT_TYPE>
							<COMPONENT_CODE>S_WOOD_STV</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS><xsl:call-template name="REMARKWOODSTOVE" /></COMP_REMARKS>
							<COMP_EXT>
								<!--xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'S_WOOD_STV'"></xsl:with-param>
								</xsl:call-template-->
							</COMP_EXT>
							<COM_EXT_AD><xsl:call-template name="WSTOVE_SURCHARGE" />
								<xsl:text>%</xsl:text>
							</COM_EXT_AD>
						</STEP>
						<!-- Breed of Dog -->
						<STEP STEPID="18" CALC_ID="1018" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID18"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>S</COMPONENT_TYPE>
							<COMPONENT_CODE>S_BREED</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS><xsl:call-template name="REMRKBREEDDOG" /></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD><xsl:call-template name="DOGS_CHARGE_INPERCENT" />
								<xsl:text>%</xsl:text>
							</COM_EXT_AD>
						</STEP>
						<!-- Seasonal Secondary -->
						<STEP STEPID="19" CALC_ID="1019" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID19"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_SEASNL</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS><xsl:call-template name="REMARKSEASONALSECONDRY" /></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD><xsl:call-template name="SEASONALSECONDARYCREDIT" /></COM_EXT_AD>
						</STEP>
						<GROUPFORMULA>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1006]</OPERAND>
							<OPERATOR>*</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1007]</OPERAND>
							<OPERATOR>*</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1008]</OPERAND>
							<OPERATOR>*</OPERATOR>
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
							<OPERATOR>*</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1015]</OPERAND>
							<OPERATOR>*</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1016]</OPERAND>
							<OPERATOR>*</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1017]</OPERAND>
							<OPERATOR>*</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1018]</OPERAND>
							<OPERATOR>*</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1019]</OPERAND>
						</GROUPFORMULA>
					</GROUP>
					<GROUP GROUPID="6" CALC_ID="10006" PREFIX="" SUFIX="" ISCALCULATE="F">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID6"></xsl:call-template>
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<GROUPRULE></GROUPRULE>
						<GDESC></GDESC>
						<!-- Preferred Plus -->
						<STEP STEPID="20" CALC_ID="1020" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID20"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>PRFRD_PLS</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="PREFERRED_PLUS_COVERAGE" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'PRFRD_PLS'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-64 Renters Deluxe Endorsement -->
						<STEP STEPID="21" CALC_ID="1021" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID21"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>RNT_DLX</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:choose>
									<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4">
										<xsl:call-template name="DELUXENDORSEMENTPREMIUM" />
									</xsl:when>
									<xsl:otherwise>0.00</xsl:otherwise>
								</xsl:choose>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'RNT_DLX'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-66 Condominium Deluxe Endorsement -->
						<STEP STEPID="22" CALC_ID="1022" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID22"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>CND_DLX</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:choose>
									<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO6">
										<xsl:call-template name="DELUXENDORSEMENTPREMIUM" />
									</xsl:when>
									<xsl:otherwise>0.00</xsl:otherwise>
								</xsl:choose>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'CND_DLX'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--  HO-34 Replacement Cost Personal Property -->
						<STEP STEPID="23" CALC_ID="1023" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID23"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>RPLC_CST</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_34_REPLACEMENT_COST" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'RPLC_CST'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-11 Expanded Replacement Coverage for Building -->
						<STEP STEPID="24" CALC_ID="1024" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID24"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>EXPND_RPLC_CST</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'EXPND_RPLC_CST'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-96 Increased Fire Dept. Service Charge -->
						<STEP STEPID="25" CALC_ID="1025" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID25"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>FIRE_DPT</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_96_REPLACEMENT_COST_PERSONAL_PROPERTY"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'FIRE_DPT'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-312 Business Property Increased Limits -->
						<STEP STEPID="26" CALC_ID="1026" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID26"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BSNS_PRP_INCR</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_312_BUSINESS_PROPERTY_INCREASED_LIMITS"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'BSNS_PRP_INCR'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--HO-48 Other Structures - Increased Limits -->
						<STEP STEPID="27" CALC_ID="1027" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID27"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:value-of select="HO48ADDITIONAL" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>OTHR_STR_INCR</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_48_OTHER_STRUCTURE_INCREASED_LIMITS"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-40 Other Structures - Rented to Others -->
						<STEP STEPID="28" CALC_ID="1028" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID28"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>OTHR_STR_RNT</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_40_OTHER_STRUCTURE_RENTED_TO_OTHERS"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'OTHR_STR_RNT'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-42 Other Structure With Incidental -->
						<STEP STEPID="29" CALC_ID="1029" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID29"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>OTHR_STR_INCDT</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-50 Personal Property Away From Premises  -->
						<STEP STEPID="30" CALC_ID="1030" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID30"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>PRS_PRP_AWY_PRM</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO50_PERSONAL_PROPERTY_AWAY_PREMISES"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'PRS_PRP_AWY_PRM'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Building Additions and Alterations (HO-51)  -->
						<STEP STEPID="31" CALC_ID="1031" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID31"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BLDG_ALT</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:choose>
									<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4">
										<xsl:call-template name="HO51_BULDING_ALTER"></xsl:call-template>
									</xsl:when>
									<xsl:otherwise>
										0
									</xsl:otherwise>
								</xsl:choose>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'BLDG_ALT'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-53 Increased Credit Card -->
						<STEP STEPID="32" CALC_ID="1032" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID32"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>CRDT_CRD</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_53_INCREASED_CREDIT_CARD"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'CRDT_CRD'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO65 heading therefore no component code required -->
						<STEP STEPID="33" CALC_ID="1033" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID33"></xsl:call-template>
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
						<!-- Unscheduled Jewelry, Watches & Fur -->
						<STEP STEPID="34" CALC_ID="1034" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID34"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>UNSCD_JWL_FUR</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="UNSCHEDULED_JEWELRY_ADDITIONAL" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'UNSCD_JWL_FUR'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- money -->
						<STEP STEPID="35" CALC_ID="1035" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID35"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MONEY</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="MONEY"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'MONEY'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Securities -->
						<STEP STEPID="36" CALC_ID="1036" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID36"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>SECURITY</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="SECURITIES"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'SECURITY'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Silverware, Goldware, Etc. -->
						<STEP STEPID="37" CALC_ID="1037" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID37"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>SLVR_GLD</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="SILVERWARE_GOLDWARE"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'SLVR_GLD'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Firearms -->
						<STEP STEPID="38" CALC_ID="1038" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID38"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>FIRE_ARM</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="FIREARMS"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'FIRE_ARM'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-35 Loss Assessment -->
						<STEP STEPID="39" CALC_ID="1039" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID39"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<!--xsl:variable name="VAR_HO35INCLUDE">
									<xsl:choose>
										<xsl:when test="normalize-space(HO35INCLUDE)='N/A' or normalize-space(HO35INCLUDE) = 'n/a' or normalize-space(HO35INCLUDE)=''">0.00</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="HO35INCLUDE" />
										</xsl:otherwise>
									</xsl:choose>
								</xsl:variable>
								<xsl:variable name="VAR_HO35ADDITIONAL">
									<xsl:choose>
										<xsl:when test="normalize-space(HO35ADDITIONAL)='N/A' or normalize-space(HO35ADDITIONAL) = 'n/a' or normalize-space(HO35ADDITIONAL)=''">0.00</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="HO35ADDITIONAL" />
										</xsl:otherwise>
									</xsl:choose>
								</xsl:variable>
								<xsl:value-of select="$VAR_HO35INCLUDE + $VAR_HO35ADDITIONAL" /-->
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>LSS_ASMNT</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:choose>
									<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO6">
										<xsl:call-template name="HO35_LOSS_ASSESSMENT"></xsl:call-template>
									</xsl:when>
									<xsl:otherwise>0</xsl:otherwise>
								</xsl:choose>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'LSS_ASMNT'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-277 Ordinance or Law coverage Forms -->
						<STEP STEPID="40" CALC_ID="1040" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID40"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>ORD_LW</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="ORDINANCE" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'ORD_LW'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-32 Unit Owners Coverage A Special Coverage -->
						<STEP STEPID="41" CALC_ID="1041" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID41"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>UNT_OWNR_CVG_A</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO32_UNIT_OWNER"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'UNT_OWNR_CVG_A'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-455 Identity Fraud Expense Coverage -->
						<STEP STEPID="42" CALC_ID="1042" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID42"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:call-template name="HO_455_DEDUCTIBLE"></xsl:call-template>
							</D_PATH>
							<L_PATH>
								<xsl:call-template name="HO_455_LIMIT"></xsl:call-template>
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>ID_FRAUD</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_455_IDENTITY_FRAUD" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'ID_FRAUD'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-315 Earthquake -->
						<STEP STEPID="43" CALC_ID="1043" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID43"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>ERTHQKE</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_315_EARTHQUAKE"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'ERTHQKE'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-327 Water Back-Up and Sump Pump Overflow -->
						<STEP STEPID="44" CALC_ID="1044" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID44"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:value-of select="HO327" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BK_UP_SM_PMP</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_327_WATER_BACK_UP"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'BK_UP_SM_PMP'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--New Steps Start-->
						<!-- HO-33 Condo-Unit Owner Rental to Others. -->
						<STEP STEPID="45" CALC_ID="1045" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID45"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>CND_UNIT</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_33_CONDO_UNIT"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'CND_UNIT'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO 373 Indiana Contingent Workers  Compensation. -->
						<STEP STEPID="46" CALC_ID="1046" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID46"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>WRKR_CMP</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'WRKR_CMP'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- MINE SUBSIDENCE COVERAGE -->
						<STEP STEPID="47" CALC_ID="1047" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID47"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH><xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHER']/NODE[@ID ='MINE_SUBSIDENCE_COVERAGE']/@DEDUCTIBLE_PERCENT" /><xsl:text>%</xsl:text></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MNE_SBS</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_287_MINE_SUBSIDENCE_COVERAGE"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'MNE_SBS'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-9 Collapse From Sub-Surface Water -->
						<STEP STEPID="48" CALC_ID="1048" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID48"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>SUB_SRFCE_WTR</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_9_COLLAPSE_FROM_SUB_SURFACE_WATER"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'SUB_SRFCE_WTR'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--New Steps End -->
						<!-- HO-490 Specific Structure(s) Away From Premises -->
						<STEP STEPID="49" CALC_ID="1049" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID49"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:value-of select="SPECIFICSTRUCTURESADDITIONAL" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>SPCFC_STR_PRMS</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_490_SPECIFIC_STRUCTURED"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'SPCFC_STR_PRMS'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-489 Other Structures (Repair Cost Coverage) -->
						<STEP STEPID="50" CALC_ID="1050" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID50"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>OTHR_STR_RPR_CST </COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_489_SPECIFIC_STRUCTURED"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'OTHR_STR_RPR_CST'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- primary Residence (1 or 2 Family) -->
						<STEP STEPID="51" CALC_ID="1051" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID51"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>PRM_RESI</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Residence Employees  -->
						<STEP STEPID="52" CALC_ID="1052" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID52"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>RESI_EMPL</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="RESIDENCE_EMPLOYEES"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'RESI_EMPL'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Additional Premises-Occupied By Insured -->
						<STEP STEPID="53" CALC_ID="1053" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID53"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>ADDL_PRM_OCC_INS</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="ADDITIONAL_PREMISES"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'ADDL_PRM_OCC_INS'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-40 Additional Premises Rented to Others - Residence Premises -->
						<STEP STEPID="54" CALC_ID="1054" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID54"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>ADDL_PRM_RNTD_OTH_RES</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="ADDITIONAL_RESIDENCE_PREMISES"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'ADDL_PRM_RNTD_OTH_RES'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--   HO-70 Additional Premises Rented - Other Location - 1 Family -->
						<STEP STEPID="55" CALC_ID="1055" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID55"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>ADDL_PRM_RNTD_1FMLY</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="ADDITIONAL_OTHER_LOCATION_PREMISES_FAMILY1"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'ADDL_PRM_RNTD_1FMLY'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--   HO-70 Additional Premises Rented - Other Location - 2 Family -->
						<STEP STEPID="56" CALC_ID="1056" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID56"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>ADDL_PRM_RNTD_2FMLY</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="ADDITIONAL_OTHER_LOCATION_PREMISES_FAMILY2"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'ADDL_PRM_RNTD_2FMLY'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--   HO-42 Incidental Office Private School or Studio On Premises -->
						<STEP STEPID="57" CALC_ID="1057" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID57"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>INCDT_OFCE_PRV_SCHL_PRM</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_42_ON_PREMISES"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'INCDT_OFCE_PRV_SCHL_PRM'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Business Located in an Other Structure -->
						<STEP STEPID="58" CALC_ID="1058" PREFIX="" SUFIX="" ISCALCULATE="F"> <!-- SKB-->
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID58"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BSNS_LCTD_OTH_STR</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO42_BUSINESS_OTHER_STRUCTURE_WITH_INCIDENTAL"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-42 Incidental Office Private School or Studio Instruction Only -->
						<STEP STEPID="59" CALC_ID="1059" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID59"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>INCDT_OFCE_PRV_SCHL_INST</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_42_INSTRUCTION_ONLY"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'INCDT_OFCE_PRV_SCHL_INST'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-200 Waterbed Liability -->
						<STEP STEPID="60" CALC_ID="1060" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID60"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>WTRBD_LBLTY</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_200_WATERBED_LIABILITY"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'WTRBD_LBLTY'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-43 Incidental Office Private School or Studio Off Premises -->
						<STEP STEPID="61" CALC_ID="1061" PREFIX="" SUFIX="" ISCALCULATE="F"> <!-- New-->
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID61"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>INCDT_OFCE_PRV_SCHL_OFF_PRM</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_43_OFF_PREMISES"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'INCDT_OFCE_PRV_SCHL_OFF_PRM'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<STEP STEPID="62" CALC_ID="1062" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID62"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BSNS_PRSUIT</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-71 Business Pursuits:Class A  -->
						<STEP STEPID="63" CALC_ID="1063" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID63"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>CLASS_A</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_71_CLASSA"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'CLASS_A'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-71 Business Pursuits:Class B -->
						<STEP STEPID="64" CALC_ID="1064" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID64"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>CLASS_B</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_71_CLASSB"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'CLASS_B'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-71 Business Pursuits:Class C -->
						<STEP STEPID="65" CALC_ID="1065" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID65"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>CLASS_C</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_71_CLASSC"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'CLASS_C'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-71 Business Pursuits:Class D  -->
						<STEP STEPID="66" CALC_ID="1066" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID66"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>CLASS_D</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_71_CLASSD"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'CLASS_D'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-72 Farm Liability - Incidental Farming Residence Premises -->
						<STEP STEPID="67" CALC_ID="1067" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID67"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>INCT_FRMG_RESI_PRM</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_72_INCIDENTAL_FARMING"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'INCT_FRMG_RESI_PRM'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-73 Farm Liability - Owned By Insured Rented to Others -->
						<STEP STEPID="68" CALC_ID="1068" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID68"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>INCT_FRMG_RNTD_OTH</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_73_OTH_LOC_OPR_OTHERS"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'INCT_FRMG_RNTD_OTH'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-73 Farm Liability - Owned/Operated By Insured/Insured Employee -->
						<STEP STEPID="69" CALC_ID="1069" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID69"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>INCT_FRMG_OPRT_INS</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_73_OTH_LOC_OPR_EMPL"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'INCT_FRMG_OPRT_INS'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- HO-82 Personal Injury -->
						<STEP STEPID="70" CALC_ID="1070" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID70"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>PRS_INJRY</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_82_PERSONAL_INJURY" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'PRS_INJRY'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<GROUPFORMULA>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1020]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1021]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1022]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1023]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1024]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1025]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1026]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1027]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1028]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1029]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1030]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1031]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1032]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1033]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1034]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1035]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1036]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1037]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1038]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1039]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1040]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1041]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1042]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1043]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1044]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1045]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1046]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1047]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1048]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1049]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1050]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1051]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1052]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1053]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1054]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1055]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1056]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1057]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1058]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1059]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1060]</OPERAND>
						</GROUPFORMULA>
					</GROUP>
					<GROUP GROUPID="7" CALC_ID="10007" PREFIX="" SUFIX="" ISCALCULATE="F">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID7" />
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<GROUPRULE></GROUPRULE>
						<GDESC></GDESC>
						<!-- Bicycle (HO-900) -->
						<STEP STEPID="71" CALC_ID="1071" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID71"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_BICYCLE_DED !=0">
										<xsl:value-of select="SCH_BICYCLE_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_BICYCLE_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BICYCLE</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_900_BICYCLES_PREMIUM"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'BICYCLE'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Cameras (Non-Professional) -->
						<STEP STEPID="72" CALC_ID="1072" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID72"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_CAMERA_DED !=0">
										<xsl:value-of select="SCH_CAMERA_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_CAMERA_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>CMRA</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="CAMERAS_PREMIUM"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'CMRA'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Cellular Phones (HO-900) -->
						<STEP STEPID="73" CALC_ID="1073" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID73"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_CELL_DED !=0">
										<xsl:value-of select="SCH_CELL_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_CELL_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>CELL</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HO_900_CELLULAR_PHONES_PREMIUM"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'CELL'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Furs -->
						<STEP STEPID="74" CALC_ID="1074" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID74"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_FURS_DED !=0">
										<xsl:value-of select="SCH_FURS_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_FURS_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>FUR</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="FURS_PREMIUM"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'FUR'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Golf Equipment -->
						<STEP STEPID="75" CALC_ID="1075" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID75"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_GOLF_DED !=0">
										<xsl:value-of select="SCH_GOLF_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_GOLF_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>GOLF</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="GOLF_EQUIPMENT_PREMIUM"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'GOLF'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Guns -->
						<STEP STEPID="76" CALC_ID="1076" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID76"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_GUNS_DED !=0">
										<xsl:value-of select="SCH_GUNS_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_GUNS_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>GUN</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="GUNS_PREMIUM"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'GUN'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Jewelry -->
						<STEP STEPID="77" CALC_ID="1077" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID77"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_JWELERY_DED !=0">
										<xsl:value-of select="SCH_JWELERY_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_JWELERY_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>JWL</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="JEWELRY_PREMIUM"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'JWL'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Musical (Non-Professional) -->
						<STEP STEPID="78" CALC_ID="1078" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID78"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_MUSICAL_DED !=0">
										<xsl:value-of select="SCH_MUSICAL_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_MUSICAL_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MSC_NON_PRF</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="MUSICAL_NON_PROFESSIONAL_PREMIUM"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'MSC_NON_PRF'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Personal Computers (HO-214) -->
						<STEP STEPID="79" CALC_ID="1079" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID79"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_PERSCOMP_DED !=0">
										<xsl:value-of select="SCH_PERSCOMP_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_PERSCOMP_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>PRS_CMP</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="PERSONAL_COMPUTERS_PREMIUM"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'PRS_CMP'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Silver -->
						<STEP STEPID="80" CALC_ID="1080" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID80"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_SILVER_DED !=0">
										<xsl:value-of select="SCH_SILVER_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_SILVER_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>SLVR</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="SILVER_PREMIUM"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'SLVR'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Stamps -->
						<STEP STEPID="81" CALC_ID="1081" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID81"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_STAMPS_DED !=0">
										<xsl:value-of select="SCH_STAMPS_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_STAMPS_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>STMP</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="STAMPS_PREMIUM"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'STMP'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Rare Coins -->
						<STEP STEPID="82" CALC_ID="1082" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID82"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_RARECOINS_DED !=0">
										<xsl:value-of select="SCH_RARECOINS_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_RARECOINS_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>RARE_COIN</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="RARE_COINS_PREMIUM"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'RARE_COIN'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Fine Arts - Without Breakage -->
						<STEP STEPID="83" CALC_ID="1083" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID83"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_FINEARTS_WO_BREAK_DED !=0">
										<xsl:value-of select="SCH_FINEARTS_WO_BREAK_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_FINEARTS_WO_BREAK_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>FNE_ART_WO_BRK</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="FINE_ARTS_WITHOUT_BREAKAGE_PREMIUM"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'FNE_ART_WO_BRK'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Fine Arts - With Breakage -->
						<STEP STEPID="84" CALC_ID="1084" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID84"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_FINEARTS_BREAK_DED !=0">
										<xsl:value-of select="SCH_FINEARTS_BREAK_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_FINEARTS_BREAK_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>FNE_ART_WTH_BRK</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="FINE_ARTS_BREAKAGE_PREMIUM"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'FNE_ART_WTH_BRK'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Handicap Electronics -->
						<STEP STEPID="85" CALC_ID="1085" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID85"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_HANDICAP_ELECTRONICS_DED !=0">
										<xsl:value-of select="SCH_HANDICAP_ELECTRONICS_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_HANDICAP_ELECTRONICS_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>HNDCP_ELCT</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HANDICAP_ELECTRONICS"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'HNDCP_ELCT'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Hearing Aids -->
						<STEP STEPID="86" CALC_ID="1086" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID86"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_HEARING_AIDS_DED !=0">
										<xsl:value-of select="SCH_HEARING_AIDS_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_HEARING_AIDS_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>HRNG_AD</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="HEARING_AIDS"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'HRNG_AD'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Insulin Pumps -->
						<STEP STEPID="87" CALC_ID="1087" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID87"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_INSULIN_PUMPS_DED !=0">
										<xsl:value-of select="SCH_INSULIN_PUMPS_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_INSULIN_PUMPS_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>INSLN_PMP</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="INSULIN_PUMPS"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'INSLN_PMP'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Mart Kay/Amway -->
						<STEP STEPID="88" CALC_ID="1088" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID88"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_MART_KAY_DED !=0">
										<xsl:value-of select="SCH_MART_KAY_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_MART_KAY_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MRT_KY_AMWY</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="MART_KAY_AMWAY"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'MRT_KY_AMWY'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Personal Computers - Laptop -->
						<STEP STEPID="89" CALC_ID="1089" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID89"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_PERSONAL_COMPUTERS_LAPTOP_DED !=0">
										<xsl:value-of select="SCH_PERSONAL_COMPUTERS_LAPTOP_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_PERSONAL_COMPUTERS_LAPTOP_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>PC_LPTP</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="PERSONAL_COMPUTERS_LAPTOP"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'PC_LPTP'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Salesman Supplies -->
						<STEP STEPID="90" CALC_ID="1090" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID90"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_SALESMAN_SUPPLIES_DED !=0">
										<xsl:value-of select="SCH_SALESMAN_SUPPLIES_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_SALESMAN_SUPPLIES_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>SLMN_SPPLY</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="SALESMAN_SUPPLY"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'SLMN_SPPLY'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Scuba Diving Equipment -->
						<STEP STEPID="91" CALC_ID="1091" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID91"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_SCUBA_DRIVING_DED !=0">
										<xsl:value-of select="SCH_SCUBA_DRIVING_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_SCUBA_DRIVING_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>SCB_DVNG_EQP</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="SCUBA_DIVING"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'SCB_DVNG_EQP'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Snow Skies -->
						<STEP STEPID="92" CALC_ID="1092" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID92"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_SNOW_SKIES_DED !=0">
										<xsl:value-of select="SCH_SNOW_SKIES_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_SNOW_SKIES_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>SNW_SKY</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="SNOW_SKY"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
							<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'SNW_SKY'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Tack and Saddle -->
						<STEP STEPID="93" CALC_ID="1093" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID93"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_TACK_SADDLE_DED !=0">
										<xsl:value-of select="SCH_TACK_SADDLE_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_TACK_SADDLE_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>TCK_SDDL</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="TACK_SADDLE"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'TCK_SDDL'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Tools (At Premises)	 -->
						<STEP STEPID="94" CALC_ID="1094" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID94"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_TOOLS_PREMISES_DED !=0">
										<xsl:value-of select="SCH_TOOLS_PREMISES_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_TOOLS_PREMISES_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>TOOLS_PRM</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="TOOLS_AT_PREMISIS"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'TOOLS_PRM'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Tools (At Business Location)	 -->
						<STEP STEPID="95" CALC_ID="1095" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID95"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_TOOLS_BUSINESS_DED !=0">
										<xsl:value-of select="SCH_TOOLS_BUSINESS_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_TOOLS_BUSINESS_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>TOOLS_BSNS</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="TOOLS_AT_BSNS_LOCN"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'TOOLS_BSNS'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Tractors -->
						<STEP STEPID="96" CALC_ID="1096" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID96"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_TRACTORS_DED !=0">
										<xsl:value-of select="SCH_TRACTORS_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_TRACTORS_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>TRCTR</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="TRACTORS"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'TRCTR'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Train Collections -->
						<STEP STEPID="97" CALC_ID="1097" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID97"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_TRAIN_COLLECTIONS_DED !=0">
										<xsl:value-of select="SCH_TRAIN_COLLECTIONS_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_TRAIN_COLLECTIONS_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>TRN_CLLCTN</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="TRAIN_COLLECTION"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'TRN_CLLCTN'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Wheelchairs/Scooters -->
						<STEP STEPID="98" CALC_ID="1098" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID98"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="SCH_WHEELCHAIRS_DED !=0">
										<xsl:value-of select="SCH_WHEELCHAIRS_DED" />
									</xsl:when>
									<xsl:otherwise>$0</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(SCH_WHEELCHAIRS_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>WHLCHR</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="WHEELCHAIR"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'WHLCHR'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<GROUPFORMULA>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1061]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1062]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1063]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1064]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1065]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1066]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1067]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1068]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1069]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1070]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1071]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1072]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1073]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1074]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1075]</OPERAND>
							<OPERATOR>+</OPERATOR>
							<OPERAND TOREPLACE="Y">@[CALC_ID=1076]</OPERAND>
						</GROUPFORMULA>
					</GROUP>
					<GROUP GROUPID="8" CALC_ID="10008" PREFIX="" SUFIX="" ISCALCULATE="F">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID8"></xsl:call-template>
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<GROUPRULE></GROUPRULE>
						<GDESC></GDESC>
						<!-- Property Expense Fee -->
						<STEP STEPID="99" CALC_ID="1099" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID99"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>PRP_EXPNS_FEE</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="PROPFEE" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'PRP_EXPNS_FEE'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<GROUPFORMULA />
					</GROUP>
					<GROUP GROUPID="9" CALC_ID="10009" PREFIX="" SUFIX="" ISCALCULATE="F">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID9"></xsl:call-template>
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<GROUPRULE></GROUPRULE>
						<GDESC></GDESC>
						<!-- Minimum Premium  -->
						<STEP STEPID="100" CALC_ID="1100" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID100" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>HOME_ADD_PREMIUM</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="MINIMUM_PREMIUM" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Final Premium -->
						<STEP STEPID="101" CALC_ID="1101" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID101"></xsl:call-template>
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>SUMTOTAL</COMPONENT_CODE>
							<COMP_ACT_PRE><xsl:call-template name="FINALPRIMIUM" /></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<GROUPFORMULA />
					</GROUP>
					<PRODUCTFORMULA>
						<GROUP1 GROUPID="10003">
							<VALUE>(@[CALC_ID=10003])</VALUE>
							<OPERATOR>+</OPERATOR>
						</GROUP1>
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
						<GROUP1 GROUPID="10007">
							<VALUE>(@[CALC_ID=10007])</VALUE>
							<OPERATOR></OPERATOR>
						</GROUP1>
						<GROUP1 GROUPID="10008">
							<VALUE>(@[CALC_ID=10008])</VALUE>
							<OPERATOR></OPERATOR>
						</GROUP1>
					</PRODUCTFORMULA>
				</PRODUCT>
			</CALCULATION>
		</PREMIUM>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--								DEWLLINGDETAILS Template(END)								  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--								Base Premium Template(START)								  -->
	<!--								  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="DEWLLING-MAIN">
		<xsl:variable name="TERCODES" select="TERRITORYCODES" />
		<xsl:variable name="GROUP_ID" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY']/NODE[@ID='TERRGROUPING']/ATTRIBUTES[@TID=$TERCODES]/@GROUPID" />
		<xsl:variable name="F_CODE" select="FORM_CODE" />
		<xsl:variable name="FORMGROUP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING']/ATTRIBUTES[@FORMID=$F_CODE]/@GROUPID" />
		<xsl:variable name="VAR_NO_OF_UNITS_4" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='NOOFDWELLINGUNITS']/NODE[@ID='NOOFUNITS']/ATTRIBUTES/@NO_OF_UNITS_LESS_THAN" />
		<xsl:variable name="VAR_NO_OF_UNITS_5" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='NOOFDWELLINGUNITS']/NODE[@ID='NOOFUNITS']/ATTRIBUTES/@NO_OF_UNITS_GREATER_THAN" />
		<xsl:variable name="PCODE1" select="PRODUCTNAME" />
		<xsl:variable name="PCODE">
			<xsl:choose>
				<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 and NUMBEROFUNITS &lt;= $VAR_NO_OF_UNITS_4 ">HO-4 CO I</xsl:when>
				<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 and NUMBEROFUNITS &gt;= $VAR_NO_OF_UNITS_5 ">HO-4 CO II</xsl:when>
				<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO6 and NUMBEROFUNITS &lt;= $VAR_NO_OF_UNITS_4 ">HO-6 CO I</xsl:when>
				<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO6 and NUMBEROFUNITS &gt;= $VAR_NO_OF_UNITS_5 ">HO-6 CO II</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="PRODUCTNAME" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="normalize-space($FORMGROUP) = '1'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1" />
			</xsl:when>
			<xsl:when test="normalize-space($FORMGROUP) = '2'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code2" />
			</xsl:when>
			<xsl:when test="normalize-space($FORMGROUP) = '3'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code3" />
			</xsl:when>
			<xsl:when test="normalize-space($FORMGROUP) = '4'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code4" />
			</xsl:when>
			<xsl:when test="normalize-space($FORMGROUP) = '5'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code5" />
			</xsl:when>
			<xsl:when test="normalize-space($FORMGROUP) = '6'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code6" />
			</xsl:when>
			<xsl:when test="normalize-space($FORMGROUP) = '7'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code7" />
			</xsl:when>
			<xsl:when test="normalize-space($FORMGROUP) = '8'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code8" />
			</xsl:when>
			<xsl:when test="normalize-space($FORMGROUP) = '9'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code9" />
			</xsl:when>
			<xsl:when test="normalize-space($FORMGROUP) = '10'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code10" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$FORMGROUP" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- For INDINA -->
	<xsl:template name="DEWLLING_MAIN_INDIANA">
		<!--<xsl:value-of select ="TERRITORYCODES"></xsl:value-of>-->
		<xsl:variable name="TERCODES" select="TERRITORYCODES" />
		<xsl:variable name="GROUP_ID" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY']/NODE[@ID='TERRGROUPING']/ATTRIBUTES [@TID = $TERCODES]/@GROUPID" />
		<xsl:variable name="F_CODE" select="FORM_CODE" />
		<xsl:variable name="FORMGROUP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING']/ATTRIBUTES [@FORMID = $F_CODE]/@GROUPID" />
		<xsl:variable name="VAR_NO_OF_UNITS_4" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='NOOFDWELLINGUNITS']/NODE[@ID='NOOFUNITS']/ATTRIBUTES/@NO_OF_UNITS_LESS_THAN" />
		<xsl:variable name="VAR_NO_OF_UNITS_5" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='NOOFDWELLINGUNITS']/NODE[@ID='NOOFUNITS']/ATTRIBUTES/@NO_OF_UNITS_GREATER_THAN" />
		<xsl:variable name="PCODE">
			<xsl:choose>
				<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 and NUMBEROFUNITS &lt;= $VAR_NO_OF_UNITS_4">HO-4 CO I</xsl:when>
				<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 and NUMBEROFUNITS &gt;= $VAR_NO_OF_UNITS_5">HO-4 CO II</xsl:when>
				<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO6 and NUMBEROFUNITS &lt;= $VAR_NO_OF_UNITS_4">HO-6 CO I</xsl:when>
				<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO6 and NUMBEROFUNITS &gt;= $VAR_NO_OF_UNITS_5">HO-6 CO II</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="PRODUCTNAME" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$FORMGROUP = '1'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1" />
			</xsl:when>
			<xsl:when test="$FORMGROUP = '2'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code2" />
			</xsl:when>
			<xsl:when test="$FORMGROUP = '3'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code3" />
			</xsl:when>
			<xsl:when test="$FORMGROUP = '4'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code4" />
			</xsl:when>
			<xsl:when test="$FORMGROUP = '5'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code5" />
			</xsl:when>
			<xsl:when test="$FORMGROUP = '6'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code6" />
			</xsl:when>
			<xsl:when test="$FORMGROUP = '7'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code7" />
			</xsl:when>
			<xsl:otherwise>1.00 </xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- INDIANA HO-4 Reguler-->
	<xsl:template name="DEWLLING_MAIN_INDIANA_HO_4">
		<xsl:variable name="TERCODES" select="TERRITORYCODES" />
		<xsl:variable name="GROUP_ID" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY']/NODE[@ID='TERRGROUPING']/ATTRIBUTES [@TID = $TERCODES]/@GROUPID" />
		<xsl:variable name="F_CODE" select="FORM_CODE" />
		<xsl:variable name="FORMGROUP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING']/ATTRIBUTES [@FORMID = $F_CODE]/@GROUPID" />
		<xsl:variable name="PCODE" select="PRODUCTNAME" />
		<xsl:choose>
			<xsl:when test="$FORMGROUP = '1'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1" />
			</xsl:when>
			<xsl:when test="$FORMGROUP = '2'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code2" />
			</xsl:when>
			<xsl:when test="$FORMGROUP = '3'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code3" />
			</xsl:when>
			<xsl:when test="$FORMGROUP = '4'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code4" />
			</xsl:when>
			<xsl:when test="$FORMGROUP = '5'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code5" />
			</xsl:when>
			<xsl:when test="$FORMGROUP = '6'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code6" />
			</xsl:when>
			<xsl:otherwise> 1.00 </xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--								Base Premium Template(END)									  -->
	<!--								  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		     						Template For COVERAGE VALUE  (START)					  -->
	<!--			    					  FOR MICHIGAN and INDIANA								  -->
	<!-- ============================================================================================ -->
	<xsl:template name="C-VALUE">
		<!-- Divide by 1000 because product factor master has values in 1000 -->
		<xsl:variable name="CVALUE">
			<xsl:choose>
				<xsl:when test="DWELLING_LIMITS=0">
					<xsl:value-of select="DWELLING_LIMITS" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select='format-number((DWELLING_LIMITS div 1000), "#")' /> <!-- converting to integer. -->
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<!-- Set the Group -->
		<xsl:variable name="TERCODES" select="TERRITORYCODES" />
		<xsl:variable name="VAR_PRODUCTNAME" select="PRODUCTNAME" />
		<xsl:variable name="F_CODE" select="normalize-space(FORM_CODE)" />
		<xsl:variable name="FORMGROUP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING']/ATTRIBUTES[@FORMID=normalize-space($F_CODE)]/@GROUPID" />
		<xsl:variable name="VAR_GROUP">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGE_GROUP']/NODE[@ID='COVERAGEGROUP']/ATTRIBUTES[@FORMTYPE=normalize-space($VAR_PRODUCTNAME) and @FORMGROUP=normalize-space($FORMGROUP)]/@COVERAGEGROUP" />
		</xsl:variable>
		<!-- get the max value of coverages in the database -->
		<xsl:variable name="VAR_MAX_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = normalize-space($VAR_GROUP)]/@MAXVALUE" />
		<xsl:choose>
			<xsl:when test="$CVALUE &gt; $VAR_MAX_VALUE">
				<!-- For each Additional amount-->
				<xsl:variable name="VAR_ADDITIONAL_VALUE">
					<xsl:choose>
						<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 or PRODUCTNAME = $POLICYTYPE_HO6">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/ATTRIBUTES[@CovC = 'ADDITIONAL']/@AMOUNT" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/ATTRIBUTES[@CovA = 'ADDITIONAL']/@AMOUNT" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<!-- value -->
				<xsl:variable name="VAR_ADDITIONAL_FACTOR">
					<xsl:choose>
						<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 or PRODUCTNAME = $POLICYTYPE_HO6">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/ATTRIBUTES[@CovC = 'ADDITIONAL']/@Factor" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/ATTRIBUTES[@CovA = 'ADDITIONAL']/@Factor" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<!-- value for the max factor -->
				<xsl:variable name="VAR_MAX_VALUE_FACTOR">
					<xsl:choose>
						<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 or PRODUCTNAME = $POLICYTYPE_HO6">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP=$VAR_GROUP]/ATTRIBUTES[@CovC=$VAR_MAX_VALUE]/@Factor" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP=$VAR_GROUP]/ATTRIBUTES[@CovA=$VAR_MAX_VALUE]/@Factor" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<!-- Calculations 
					e.g : factor for 400 + ((difference in the cov and the max value)/additional amount)* (factor for additional)-->
				<xsl:value-of select="$VAR_MAX_VALUE_FACTOR + ((($CVALUE - $VAR_MAX_VALUE) div $VAR_ADDITIONAL_VALUE) * $VAR_ADDITIONAL_FACTOR)" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 or PRODUCTNAME = $POLICYTYPE_HO6">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/ATTRIBUTES[@CovC =$CVALUE]/@Factor" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/ATTRIBUTES[@CovA =$CVALUE]/@Factor" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Coverage value for HO-3 INDIANA -->
	<xsl:template name="C_VALUE_INDIANA">
		<xsl:variable name="CVALUE" select="COVERAGEVALUE" />
		<xsl:choose>
			<xsl:when test="FORM_CODE = '01F'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
			</xsl:when>
			<xsl:when test="FORM_CODE = '02F'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
			</xsl:when>
			<xsl:when test="FORM_CODE = '03F'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
			</xsl:when>
			<xsl:when test="FORM_CODE = '04F'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
			</xsl:when>
			<xsl:when test="FORM_CODE = '05F'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
			</xsl:when>
			<xsl:when test="FORM_CODE = '06F'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
			</xsl:when>
			<xsl:when test="FORM_CODE = '07F'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G2']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
			</xsl:when>
			<xsl:when test="FORM_CODE = '08F'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G2']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
			</xsl:when>
			<xsl:when test="FORM_CODE = '09F'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G2']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
			</xsl:when>
			<xsl:when test="FORM_CODE = '10F'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G2']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
			</xsl:when>
			<xsl:when test="FORM_CODE = '01M'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
			</xsl:when>
			<xsl:when test="FORM_CODE = '02M'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
			</xsl:when>
			<xsl:when test="FORM_CODE = '3M'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
			</xsl:when>
			<xsl:when test="FORM_CODE = '04M'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
			</xsl:when>
			<xsl:when test="FORM_CODE = '05M'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
			</xsl:when>
			<xsl:when test="FORM_CODE = '06M'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
			</xsl:when>
			<xsl:when test="FORM_CODE = '07M'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
			</xsl:when>
			<xsl:when test="FORM_CODE = '08M'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
			</xsl:when>
			<xsl:when test="FORM_CODE = '09M'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
			</xsl:when>
			<xsl:when test="FORM_CODE = '10M'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G2']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
			</xsl:when>
			<xsl:otherwise>
		1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Coverage Value For HO-4 and HO-6 (Regular) -->
	<xsl:template name="C_VALUE_HO_4_AND_6">
		<xsl:variable name="FINALVALUE">
			<xsl:variable name="CVALUE" select="COVERAGEVALUE" />
			<!-- Set the Group -->
			<xsl:variable name="VAR_GROUP">G1</xsl:variable>
			<!-- get the max value of coverages in the database -->
			<xsl:variable name="VAR_MAX_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/@MAXVALUE" />
			<xsl:choose>
				<xsl:when test="COVERAGEVALUE &gt; $VAR_MAX_VALUE">
					<xsl:variable name="VAR_ADDITIONAL_VALUE">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/ATTRIBUTES[@CovA = 'ADDITIONAL']/@AMOUNT" />
					</xsl:variable>
					<!-- value -->
					<xsl:variable name="VAR_ADDITIONAL_FACTOR">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/ATTRIBUTES[@CovA = 'ADDITIONAL']/@Factor" />
					</xsl:variable>
					<!-- value for the max factor -->
					<xsl:variable name="VAR_MAX_VALUE_FACTOR">
						<xsl:choose>
							<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $VAR_MAX_VALUE]/@Factor" />
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G2']/ATTRIBUTES[@CovA = $VAR_MAX_VALUE]/@Factor" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:variable>
					<!-- Calculations 
					e.g : factor for 400 + ((difference in the cov and the max value)/additional amount)* (factor for additional)-->
					<xsl:value-of select="$VAR_MAX_VALUE_FACTOR + ((($CVALUE - $VAR_MAX_VALUE) div $VAR_ADDITIONAL_VALUE) * $VAR_ADDITIONAL_FACTOR)" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:choose>
						<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA =$CVALUE]/@Factor" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA =$CVALUE]/@Factor" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$FINALVALUE = '' or $FINALVALUE =0">1</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$FINALVALUE" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		     						Template For COVERAGE VALUE  (END)						  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--						Templates For Coverage A,C,D  (START)							  -->
	<!-- ============================================================================================ -->
	<!-- Coverage A Building Property Increased Limits for HO6 -->
	<xsl:template name="COVERAGE_A_PREMIUM_FOR_HO6">
		<xsl:choose>
			<xsl:when test="PRODUCTNAME =$POLICYTYPE_HO6">
				<xsl:variable name="PCOVERAGE_A_BLDG_PROPERTY_VALUE" select="PERSONALPROPERTYINCREASEDLIMITADDITIONAL" />
				<!-- Coverage A Building Property - Increase Limits: The minimum covg is 10% of covg C or $2000 whichever is greater. This amount may be increased for $2 per $1000 of increase. If the policy has the HO-66 endorsement then the cost is $2.2 per $1000. Asfa Praveen (24-May-2007) -->
				<xsl:variable name="PCOVERAGE_A_BLDG_PROPERTY_ADDITIONAL_COST">
					<xsl:choose>
						<xsl:when test="HO66CONDOMINIUMDELUXE ='N'">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='ADDITIONAL_BULDING_PROPERTY']/ATTRIBUTES[@FORM='OTHERS']/@COST" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='ADDITIONAL_BULDING_PROPERTY']/ATTRIBUTES[@FORM='HO-66']/@COST" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:variable name="PCOVERAGE_A_BLDG_PROPERTY_RATE_PER_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='ADDITIONAL_BULDING_PROPERTY']/ATTRIBUTES/@INCREASEDAMOUNT"></xsl:variable>
				<xsl:value-of select="round(($PCOVERAGE_A_BLDG_PROPERTY_VALUE div $PCOVERAGE_A_BLDG_PROPERTY_RATE_PER_VALUE) * $PCOVERAGE_A_BLDG_PROPERTY_ADDITIONAL_COST)" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Coverage C - Personal Property - Limit And Deductible -->
	<xsl:template name="COVERAGE_C_PREMIUM">
		<xsl:variable name="PNAME" select="PRODUCTNAME"></xsl:variable>
		<xsl:variable name="PCOVERAGE_C_ADDITIONAL_VALUE" select="PERSONALPROPERTYINCREASEDLIMITADDITIONAL"></xsl:variable>
		<xsl:variable name="PCOVERAGE_C_ADDITIONAL_COST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = $PNAME]/@COST"></xsl:variable>
		<xsl:variable name="PCOVERAGE_C_ADDITIONAL_INCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = $PNAME]/@INCREASEDAMOUNT"></xsl:variable>
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="PCOVERAGE_C_REDUCTION_VALUE">
			<xsl:choose>
				<xsl:when test="REDUCTION_IN_COVERAGE_C ='N'">0</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="REDUCTION_IN_COVERAGE_C" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PCOVERAGE_C_ADDITIONAL_INCREASEDAMOUNT &gt; 0.00">
				<xsl:value-of select="round(($PCOVERAGE_C_ADDITIONAL_COST * (($PCOVERAGE_C_ADDITIONAL_VALUE - $PCOVERAGE_C_REDUCTION_VALUE) div $PCOVERAGE_C_ADDITIONAL_INCREASEDAMOUNT))* $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>
			0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Coverage D - Loss Of Use - Limit And Deductible-->
	<xsl:template name="COVERAGE_D_PREMIUM">
		<xsl:variable name="PCOVERAGE_D_ADDITIONAL_VALUE" select="ADDITIONALLIVINGEXPENSEADDITIONAL"></xsl:variable>
		<xsl:variable name="PCOVERAGE_D_ADDITIONAL_COST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='ADDITIONAL_LIVING_EXPENSE']/ATTRIBUTES/@COST"></xsl:variable>
		<xsl:variable name="PCOVERAGE_D_ADDITIONAL_INCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='ADDITIONAL_LIVING_EXPENSE']/ATTRIBUTES/@INCREASEDAMOUNT"></xsl:variable>
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PCOVERAGE_D_ADDITIONAL_INCREASEDAMOUNT &gt; 0.00">
				<xsl:value-of select="round(($PCOVERAGE_D_ADDITIONAL_COST * ($PCOVERAGE_D_ADDITIONAL_VALUE div $PCOVERAGE_D_ADDITIONAL_INCREASEDAMOUNT))* $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>
			0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--						Templates For Coverage A,C,D  (END)							  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--						Templates for Insurance Score Credit, Display (START)				  -->
	<!-- ============================================================================================ -->
	<xsl:template name="INSURANCE_SCORE_CREDIT"> <!-- used in ADJUSTEDBASE_REGULAR_MICHIGAN_HO2 -->
		<!-- Fetch the insurance score factor depending on Insurance Score -->
		<xsl:variable name="INS" select="normalize-space(INSURANCESCORE)" />
		<xsl:choose>
			<xsl:when test="$INS='NOHITNOSCORE'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='CREDIT']/ATTRIBUTES[@MINSCORE = 'N']/@FACTOR" />
			</xsl:when>
			<xsl:when test="$INS &gt;= 0">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='CREDIT']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- For Display Only -->
	<xsl:template name="INSURANCE_SCORE_CREDIT_DISPLAY"> <!-- used in step 6 -->
		<!-- Fetch the Insurance Score factor -->
		<xsl:variable name="INSURANCESCORE_CREDIT">
			<xsl:call-template name="INSURANCE_SCORE_CREDIT" />
		</xsl:variable>
		<!-- Check if the Insurance score credit is 1.00 then not applied.else applied -->
		<xsl:choose>
			<xsl:when test="$INSURANCESCORE_CREDIT = 1.00">0.00</xsl:when>
			<xsl:otherwise>Applied</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="INSURANCE_SCORE_DISPLAY"> <!-- used in step 6 , with text to display the percentage in the caption -->
		<xsl:choose>
			<xsl:when test="normalize-space(INSURANCESCORE) ='NOHITNOSCORE'">No Hit No Score</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="INSURANCESCORE" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="INSURANCESCORE_PERCENT"> <!-- INSURANCE SCORE Credit in percent for display -->
		<xsl:variable name="INS" select="normalize-space(INSURANCESCORE)" />
		<xsl:choose> <!-- Fetch the insurance score credit depending on Insurance Score -->
			<xsl:when test="$INS='NOHITNOSCORE'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='CREDIT'  ]/ATTRIBUTES[@MINSCORE = 'N']/@CREDIT" /><xsl:text>%</xsl:text>
			</xsl:when>
			<xsl:when test="$INS &gt;= 0">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='CREDIT'  ]/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@CREDIT" /><xsl:text>%</xsl:text>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--						Templates for Insurance Score Credit, Display (END)					  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--						Templates for Term Factor  (START)									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="TERM_FACTOR">
		<xsl:variable name="VAR_TERM" select="TERMFACTOR" />
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERMFACTOR']/NODE[@ID ='TERM_FACTOR']/ATTRIBUTES[@MONTH=$VAR_TERM]/@FACTOR" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--						Templates for Term Factor  (END)									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Under Construction [Factor,Credit,Display]  (START)		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="DWELLING_UNDER_CONSTRUCTION">
		<xsl:choose>
			<xsl:when test="CONSTRUCTIONCREDIT = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='DWELLINGUNDERCONSTRUCTION']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DWELLING_UNDER_CONSTRUCTION_CREDIT">
		<xsl:choose>
			<xsl:when test="CONSTRUCTIONCREDIT = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='DWELLINGUNDERCONSTRUCTION']/ATTRIBUTES/@CREDIT" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- For Display Only -->
	<xsl:template name="DWELLING_UNDER_CONSTRUCTION_DISPLAY">
		<xsl:choose>
			<xsl:when test="CONSTRUCTIONCREDIT = 'Y'">Applied</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Under Construction [Factor,Credit,Display]  (END)			  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--						Templates for Discount - Deductible Factor (START)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="DFACTOR">
		<xsl:variable name="DEDUCTIBLEAMT" select="DEDUCTIBLE" />
		<xsl:choose>
			<xsl:when test="normalize-space($DEDUCTIBLEAMT)!='' and  normalize-space($DEDUCTIBLEAMT)!='0'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLE']/NODE[@ID ='OPTIONALDEDUCTIBLE']/ATTRIBUTES[@AMOUNT=$DEDUCTIBLEAMT]/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--						Templates for Discount - Deductible Factor (END)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Experince Factor [Factor,Credit,Display]  (SATRT)			  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="EXPERIENCEFACTOR">
		<xsl:variable name="VAR_MINAGE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MATURITY']/@MINAGE" />
		<xsl:choose>
			<xsl:when test="EXPERIENCE &gt;= $VAR_MINAGE ">
				<xsl:variable name="EF" select="EXPERIENCE" />
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MATURITY']/ATTRIBUTES[@MINAGE &lt;= $EF and @MAXAGE &gt;= $EF]/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Experince Credit -->
	<xsl:template name="EXPERIENCECREDIT">
		<xsl:variable name="VAR_MINAGE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MATURITY']/@MINAGE" />
		<xsl:variable name="EF" select="EXPERIENCE" />
		<xsl:choose>
			<xsl:when test="EXPERIENCE &gt;= $VAR_MINAGE ">
				<!-- Check for state -->
				<xsl:choose>
					<xsl:when test="STATENAME = 'INDIANA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MATURITY']/ATTRIBUTES[@MINAGE &lt;= $EF and @MAXAGE &gt;= $EF]/@CREDIT" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:variable name="VAR1">
							<xsl:call-template name="AGEOFHOME_FACTOR" />
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
						</xsl:variable>
						<xsl:variable name="VAR3">
							<xsl:call-template name="GET_DUC_DISCOUNT" />
						</xsl:variable>
						<xsl:variable name="VAR4">
							<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
						</xsl:variable>
						<xsl:variable name="MAX_APPLICABLE_CREDIT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
						<xsl:variable name="MAX_APPLICABLE_CREDIT_PERCENT" select="(1.00-$MAX_APPLICABLE_CREDIT)*100" />
						<!-- Check the applied total percentage -->
						<xsl:variable name="FINALCREDIT_EXPERIENCE_PERCENT">
							<xsl:value-of select="((1-$VAR1)*100)+((1-$VAR2)*100)+((1-$VAR3)*100)+((1-$VAR4)*100)" />
						</xsl:variable>
						<xsl:variable name="FINALCREDIT_EXPERIENCE">
							<xsl:value-of select="((100.00-$FINALCREDIT_EXPERIENCE_PERCENT) div 100.00)" />
						</xsl:variable>
						<xsl:choose>
							<xsl:when test="($FINALCREDIT_EXPERIENCE_PERCENT - $MAX_APPLICABLE_CREDIT_PERCENT ) &gt; 0">
								<xsl:value-of select="format-number(($MAX_APPLICABLE_CREDIT_PERCENT - $FINALCREDIT_EXPERIENCE_PERCENT),'###')" />
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="format-number(((1-$VAR4)*100),'###')" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- For Display Only -->
	<xsl:template name="EXPERIENCEFACTOR_DISPLAY">
		<xsl:variable name="VAR_MINAGE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MATURITY']/@MINAGE" />
		<xsl:choose>
			<xsl:when test="EXPERIENCE &gt;= $VAR_MINAGE ">Applied</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Experince Factor [Factor,Credit,Display]  (END)			  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Age Of Home Factor  [Factor,Credit,Display]  (SATRT)		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="AGEOFHOME_FACTOR">
		<!-- Not APPLICABLE TO ho4 and ho6 -->
		<xsl:choose>
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 or PRODUCTNAME = $POLICYTYPE_HO6">
				1.00
			</xsl:when>
			<xsl:when test="CONSTRUCTIONCREDIT != 'N'">
				 1.00
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR_MAXAGEOFHOME" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/@MAXAGEOFHOME" />
				<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
				<xsl:choose>
					<xsl:when test="AGEOFHOME &gt; $VAR_MAXAGEOFHOME">1.00</xsl:when>
					<xsl:otherwise>
						<xsl:choose>
							<xsl:when test="CONSTRUCTIONCREDIT = 'N'">
								<xsl:variable name="AGE_OF_HOME" select="AGEOFHOME" />
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/ATTRIBUTES[@AGE = $AGE_OF_HOME]/@FACTOR" />
							</xsl:when>
							<xsl:otherwise>1.00</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Age Of Home CREDIT -->
	<xsl:template name="AGEOFHOME_CREDIT">
		<!-- Not APPLICABLE TO ho4 and ho6 -->
		<xsl:choose>
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 or PRODUCTNAME = $POLICYTYPE_HO6">
				0
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR_MAXAGEOFHOME" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/@MAXAGEOFHOME" />
				<xsl:choose>
					<xsl:when test="AGEOFHOME &gt; $VAR_MAXAGEOFHOME">1.00</xsl:when>
					<xsl:otherwise>
						<xsl:choose>
							<xsl:when test="CONSTRUCTIONCREDIT = 'N'">
								<xsl:variable name="AGE_OF_HOME" select="AGEOFHOME" />
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/ATTRIBUTES[@AGE = $AGE_OF_HOME]/@CREDIT" />
							</xsl:when>
							<xsl:otherwise>0.00</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Age Of Home Display -->
	<xsl:template name="AGEOFHOME_DISPLAY">
		<!-- Not APPLICABLE TO ho4 and ho6 -->
		<xsl:choose>
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 or PRODUCTNAME = $POLICYTYPE_HO6">
				0
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR_MAXAGEOFHOME" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/@MAXAGEOFHOME" />
				<xsl:choose>
					<xsl:when test="AGEOFHOME &gt; $VAR_MAXAGEOFHOME">0.00</xsl:when>
					<xsl:otherwise>
						<xsl:choose>
							<xsl:when test="CONSTRUCTIONCREDIT = 'N'">Applied</xsl:when>
							<xsl:otherwise>0.00</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Age Of Home   [Factor,Credit,Display]  (END)				  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Protective Device   [Factor,Credit,Display]  (SATRT)		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="PROTECTIVEDEVICE_DISPLAY">
		<xsl:variable name="IS_CALCULATEABLE">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$IS_CALCULATEABLE = 1.00">0</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="PROTECTIVE_DEVICE_VALUE">
					<xsl:variable name="VAR1">
						<xsl:call-template name="DIRECTBURGLERANDFIRE" />
					</xsl:variable>
					<xsl:variable name="VAR2">
						<xsl:call-template name="DIRECTFIREANDPOLICE" />
					</xsl:variable>
					<xsl:variable name="VAR3">
						<xsl:call-template name="LOCALFIREGASALARM" />
					</xsl:variable>
					<xsl:value-of select="$VAR1 + $VAR2 + $VAR3" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$PROTECTIVE_DEVICE_VALUE = 0.00">0</xsl:when>
					<xsl:otherwise>
						<xsl:choose>
							<xsl:when test="$PROTECTIVE_DEVICE_VALUE &gt; 0.00">'Applied'</xsl:when>
							<xsl:otherwise>'Applied'</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PROTECTIVEDEVICE"> <!-- Returns a fraction value. factor and not credit percentage -->
		<xsl:choose>
			<xsl:when test="CONSTRUCTIONCREDIT = 'N'"> <!-- Not applicable when dwelling is under construction -->
				<xsl:variable name="VAR1">
					<xsl:call-template name="DIRECTBURGLERANDFIRE" />
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="DIRECTFIREANDPOLICE" />
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="LOCALFIREGASALARM" />
				</xsl:variable>
				<xsl:variable name="PROTECTIVE_DEVICE_VALUE">
					<xsl:value-of select="$VAR1+$VAR2+$VAR3" />
				</xsl:variable>
				<!-- Fetch Protection Device credit limit-->
				<xsl:variable name="VAR_MAXCREDIT_PROCDEVICE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='PROTECTIVEDEVICES']/@MAXCREDIT" />
				<xsl:choose>
					<xsl:when test="$PROTECTIVE_DEVICE_VALUE = 0">1.00</xsl:when>
					<!-- Max credit allowed is 15%. We are sending factor therefore (100 - 15)/100 -->
					<xsl:when test="$PROTECTIVE_DEVICE_VALUE &gt; $VAR_MAXCREDIT_PROCDEVICE">
						<xsl:value-of select="((100.00 - $VAR_MAXCREDIT_PROCDEVICE) div 100.00)" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="(100 - $PROTECTIVE_DEVICE_VALUE) div 100.00" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Central Stations Burglary & Fire Alarm System -->
	<xsl:template name="DIRECTBURGLERANDFIRE">
		<xsl:choose>
			<xsl:when test="normalize-space(BURGLAR) = 'Y' and normalize-space(CENTRAL_FIRE) = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='CSBF']/@CREDIT" />
			</xsl:when>
			<xsl:when test="normalize-space(BURGLAR) = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='CSB']/@CREDIT" />
			</xsl:when>
			<xsl:when test="normalize-space(CENTRAL_FIRE) = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='CSF']/@CREDIT" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Direct to Fire and Police -->
	<xsl:template name="DIRECTFIREANDPOLICE">
		<xsl:choose>
			<xsl:when test="BURGLER_ALERT_POLICE = 'Y' and FIRE_ALARM_FIREDEPT = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='DFP']/@CREDIT" />
			</xsl:when>
			<xsl:when test="BURGLER_ALERT_POLICE = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='DP']/@CREDIT" />
			</xsl:when>
			<xsl:when test="FIRE_ALARM_FIREDEPT = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='DF']/@CREDIT" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Local Fire or Local Gas Alarm -->
	<xsl:template name="LOCALFIREGASALARM">
		<xsl:choose>
			<xsl:when test="normalize-space(N0_LOCAL_ALARM)!='0'">
				<xsl:variable name="VAR_MIN_NO_ALARMS" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/@MIN_N0_OF_ALARMS_FOR_CREDIT" />
				<xsl:choose>
					<xsl:when test="N0_LOCAL_ALARM = $VAR_MIN_NO_ALARMS">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='LFLG']/@CREDIT" />
					</xsl:when>
					<xsl:when test="N0_LOCAL_ALARM &gt;= $VAR_MIN_NO_ALARMS">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='LFA']/@CREDIT" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PROTECTIVEDEVICE_CREDIT">
		<xsl:variable name="VAR1">
			<xsl:call-template name="DIRECTBURGLERANDFIRE" />
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="DIRECTFIREANDPOLICE" />
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:call-template name="LOCALFIREGASALARM" />
		</xsl:variable>
		<xsl:variable name="PROTECTIVE_DEVICE_VALUE" select="$VAR1 + $VAR2 + $VAR3" />
		<xsl:variable name="VAR_MAXCREDIT_PROCDEVICE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='PROTECTIVEDEVICES']/@MAXCREDIT" />
		<xsl:choose>
			<xsl:when test="$PROTECTIVE_DEVICE_VALUE &lt;= 0.00">0</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="$PROTECTIVE_DEVICE_VALUE &gt;= $VAR_MAXCREDIT_PROCDEVICE">
						<xsl:value-of select="$VAR_MAXCREDIT_PROCDEVICE" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$PROTECTIVE_DEVICE_VALUE" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Protective Device   [Factor,Credit,Display]  (END)			  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Multi policy Factor [Factor,Credit,Display]  (SATRT)		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="MULTIPOLICY_FACTOR">
		<!-- Check the minimum cov limit-->
		<xsl:variable name="VAR_COVERAGE">
			<xsl:choose>
				<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 or PRODUCTNAME = $POLICYTYPE_HO6">
					<xsl:call-template name="COMBINEDPERSONALPROPERTY" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:call-template name="COMBINE_COVERAGEA" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_PRODUCTNAME" select="PRODUCTNAME" />
		<xsl:variable name="VAR_LIMIT">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MULTIPOLICY']/ATTRIBUTES[@POLICY_TYPE = $VAR_PRODUCTNAME]/@MINIMUM_COVG_LIMIT" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="MULTIPLEPOLICYFACTOR = 'Y' and $VAR_COVERAGE &gt; $VAR_LIMIT">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MULTIPOLICY']/ATTRIBUTES[@POLICY_TYPE = $VAR_PRODUCTNAME]/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Multipolicy CREDIT -->
	<xsl:template name="MULTIPOLICY_CREDIT">
		<!-- Check the minimum cov limit-->
		<xsl:variable name="VAR_COVERAGE">
			<xsl:choose>
				<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 or PRODUCTNAME = $POLICYTYPE_HO6">
					<xsl:call-template name="COMBINEDPERSONALPROPERTY" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:call-template name="COMBINE_COVERAGEA" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_PRODUCTNAME" select="PRODUCTNAME" />
		<xsl:variable name="VAR_LIMIT">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MULTIPOLICY']/ATTRIBUTES[@POLICY_TYPE = $VAR_PRODUCTNAME]/@MINIMUM_COVG_LIMIT" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="MULTIPLEPOLICYFACTOR = 'Y'  and $VAR_COVERAGE &gt;= $VAR_LIMIT">
				<xsl:choose>
					<xsl:when test="STATENAME ='INDIANA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MULTIPOLICY']/ATTRIBUTES[@POLICY_TYPE = $VAR_PRODUCTNAME]/@CREDIT" />
					</xsl:when>
					<xsl:otherwise>
						<!-- Check for the maximum discount limit. -->
						<xsl:variable name="VAR1">
							<xsl:call-template name="AGEOFHOME_FACTOR" />
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
						</xsl:variable>
						<xsl:variable name="VAR3">
							<xsl:call-template name="GET_DUC_DISCOUNT" />
						</xsl:variable>
						<xsl:variable name="VAR4">
							<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
						</xsl:variable>
						<xsl:variable name="VAR5">
							<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
						</xsl:variable>
						<xsl:variable name="MAX_APPLICABLE_CREDIT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
						<xsl:variable name="MAX_APPLICABLE_CREDIT_PERCENT" select="(1.00-$MAX_APPLICABLE_CREDIT)*100" />
						<!-- Check the applied total percentage -->
						<xsl:variable name="FINALCREDIT_MULTIPOLICY_PERCENT">
							<xsl:value-of select="((1-$VAR1)*100)+((1-$VAR2)*100)+((1-$VAR3)*100)+((1-$VAR4)*100)+((1-$VAR5)*100)" />
						</xsl:variable>
						<xsl:variable name="FINALCREDIT_EXPERIENCE">
							<xsl:value-of select="((100.00-$FINALCREDIT_MULTIPOLICY_PERCENT) div 100.00)" />
						</xsl:variable>
						<xsl:choose>
							<xsl:when test="($FINALCREDIT_MULTIPOLICY_PERCENT - $MAX_APPLICABLE_CREDIT_PERCENT ) &gt; 0">
								<xsl:value-of select="format-number(($MAX_APPLICABLE_CREDIT_PERCENT - $FINALCREDIT_MULTIPOLICY_PERCENT),'###')" />
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="format-number(((1-$VAR5)*100),'###')" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MULTIPOLICYCREDIT_DISPLAY">
		<xsl:variable name="PMULTIPOLICYCREDIT">
			<xsl:call-template name="MULTIPOLICY_CREDIT" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PMULTIPOLICYCREDIT = '0' or $PMULTIPOLICYCREDIT = ''">
	<xsl:text>-  Discount - Multi Policy (Discount adjusted against maximum allowed.)</xsl:text> 
	</xsl:when>
			<xsl:otherwise>
	<xsl:text>-  Discount - Multi Policy </xsl:text><xsl:call-template name="MULTIPOLICY_CREDIT" /><xsl:text>%</xsl:text>
	</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Multipolicy Display -->
	<xsl:template name="MULTIPOLICY_DISPLAY">
		<!-- Check the minimum cov limit-->
		<xsl:variable name="VAR_COVERAGE">
			<xsl:choose>
				<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 or PRODUCTNAME = $POLICYTYPE_HO6">
					<xsl:call-template name="COMBINEDPERSONALPROPERTY" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:call-template name="COMBINE_COVERAGEA" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_PRODUCTNAME" select="PRODUCTNAME" />
		<xsl:variable name="VAR_LIMIT">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MULTIPOLICY']/ATTRIBUTES[@POLICY_TYPE = $VAR_PRODUCTNAME]/@MINIMUM_COVG_LIMIT" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="MULTIPLEPOLICYFACTOR = 'Y'  and $VAR_COVERAGE &gt;= $VAR_LIMIT">Applied</xsl:when>
			<xsl:otherwise>
				0
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Multipolicy Factor [Factor,Credit,Display]  (END)			  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Valued Customer [Factor,Credit,Display]  (SATRT)			  -->
	<!-- The discount is applied per policy and per location at the beginning of the third policy 
	year renewal. Example policy issued 01/01/02 then at 01/01/03 no credit but after 
	two (2) full years and at the 01/01/04 renewal then apply credit.							  -->
	<!-- ============================================================================================ -->
	<!-- Rating xsl, NOTLOSSFREE or LOSSFREE node ='Y' implies tht the case is valid for Valued Customer wrt #yrs. -->
	<xsl:template name="VALUEDCUSTOMER">
		<xsl:variable name="VAR_VALUED_CUST_CREDIT">
			<xsl:choose>
				<xsl:when test="VALUEDCUSTOMER='Y' and NOTLOSSFREE ='Y'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_WITH_CLAIMS']/ATTRIBUTES/@FACTOR" />
				</xsl:when>
				<xsl:when test="VALUEDCUSTOMER='Y' and LOSSFREE ='Y'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_WITHOUT_CLAIMS']/ATTRIBUTES/@FACTOR" />
				</xsl:when>
				<xsl:otherwise>1.00</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="STATENAME='INDIANA'">
				<xsl:value-of select="$VAR_VALUED_CUST_CREDIT" />
			</xsl:when>
			<xsl:otherwise>
				<!-- Check for the maximum discount limit. -->
				<xsl:variable name="VAR1">
					<xsl:call-template name="AGEOFHOME_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="GET_DUC_DISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:value-of select="$VAR_VALUED_CUST_CREDIT" />
				</xsl:variable>
				<xsl:variable name="MAX_APPLICABLE_CREDIT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
				<xsl:variable name="MAX_APPLICABLE_CREDIT_PERCENT" select="(1.00-$MAX_APPLICABLE_CREDIT)*100" />
				<!-- Check the applied total percentage -->
				<xsl:variable name="FINALCREDIT_MULTIPOLICY_PERCENT">
					<xsl:value-of select="((1-$VAR1)*100)+((1-$VAR2)*100)+((1-$VAR3)*100)+((1-$VAR4)*100)+((1-$VAR5)*100)+((1-$VAR6)*100)" />
				</xsl:variable>
				<xsl:variable name="FINALCREDIT_EXPERIENCE">
					<xsl:value-of select="((100.00-$FINALCREDIT_MULTIPOLICY_PERCENT) div 100.00)" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="($FINALCREDIT_MULTIPOLICY_PERCENT - $MAX_APPLICABLE_CREDIT_PERCENT ) &gt; 0">
						<xsl:variable name="ADJUSTEDDISCOUNT" select="(1 - ((($MAX_APPLICABLE_CREDIT_PERCENT - $FINALCREDIT_MULTIPOLICY_PERCENT) + ((1 - $VAR_VALUED_CUST_CREDIT )*100.00)) div 100.00))" />
						<xsl:value-of select="format-number($ADJUSTEDDISCOUNT,'##.00')" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$VAR6" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Valued Customer-Renewal Credit CREDIT -->
	<xsl:template name="VALUEDCUSTOMER_FACTOR">
		<xsl:variable name="VAR_VALUED_CUST_CREDIT">
			<xsl:choose>
				<xsl:when test="VALUEDCUSTOMER='Y' and NOTLOSSFREE ='Y'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_WITH_CLAIMS']/ATTRIBUTES/@CREDIT" />
				</xsl:when>
				<xsl:when test="VALUEDCUSTOMER='Y' and LOSSFREE ='Y'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_WITHOUT_CLAIMS']/ATTRIBUTES/@CREDIT" />
				</xsl:when>
				<xsl:otherwise>1.00</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="STATENAME='INDIANA'">
				<xsl:value-of select="$VAR_VALUED_CUST_CREDIT" />
			</xsl:when>
			<xsl:otherwise>
				<!-- Check for the maximum discount limit. -->
				<xsl:variable name="VAR1">
					<xsl:call-template name="AGEOFHOME_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="GET_DUC_DISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:call-template name="VALUEDCUSTOMER" />
				</xsl:variable>
				<xsl:variable name="MAX_APPLICABLE_CREDIT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
				<xsl:variable name="MAX_APPLICABLE_CREDIT_PERCENT" select="(1.00-$MAX_APPLICABLE_CREDIT)*100" />
				<!-- Check the applied total percentage -->
				<xsl:variable name="FINALCREDIT_MULTIPOLICY_PERCENT">
					<xsl:value-of select="((1-$VAR1)*100)+((1-$VAR2)*100)+((1-$VAR3)*100)+((1-$VAR4)*100)+((1-$VAR5)*100)+((1-$VAR6)*100)" />
				</xsl:variable>
				<xsl:variable name="FINALCREDIT_EXPERIENCE">
					<xsl:value-of select="((100.00-$FINALCREDIT_MULTIPOLICY_PERCENT) div 100.00)" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="($FINALCREDIT_MULTIPOLICY_PERCENT - $MAX_APPLICABLE_CREDIT_PERCENT ) &gt; 0">
						<xsl:variable name="ADJUSTEDDISCOUNT" select="(($MAX_APPLICABLE_CREDIT_PERCENT - $FINALCREDIT_MULTIPOLICY_PERCENT) +  $VAR_VALUED_CUST_CREDIT )" />
						<xsl:value-of select="round((1 - $VAR6)*100)" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="format-number(((1-$VAR6)*100),'###')" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="VALUEDCUSTOMER_FACTOR_DISPLAY">
		<xsl:variable name="PVALUEDCUSTOMER_FACTOR">
			<xsl:call-template name="VALUEDCUSTOMER_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="STATENAME ='MICHIGAN'">
				<xsl:choose>
					<xsl:when test="$PVALUEDCUSTOMER_FACTOR =''">
	<xsl:text>-  Discount - Valued Customer (Discount adjusted against maximum allowed.)</xsl:text>
			</xsl:when>
					<xsl:otherwise>
	<xsl:text>-  Discount - Valued Customer </xsl:text><xsl:call-template name="VALUEDCUSTOMER_FACTOR"></xsl:call-template><xsl:text>%</xsl:text>
	</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
	<xsl:text>-  Discount - Valued Customer </xsl:text><xsl:call-template name="VALUEDCUSTOMER_FACTOR"></xsl:call-template><xsl:text>%</xsl:text>
	</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="VALUEDCUSTOMER_DISPLAY">
		<xsl:choose>
			<xsl:when test="normalize-space(VALUEDCUSTOMER)='Y'">Applied</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Valued Customer [Factor,Credit,Display]  (END)				  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--Templates for Charge-Insured to 80% to 99% of Replacement Cost [Factor,Credit,Display](SATRT) -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- Charge - Prior Loss -->
	<xsl:template name="PRIOR_LOSS">
		<xsl:choose>
			<xsl:when test="PRIOR_LOSS_SURCHARGE = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGE']/NODE[@ID='NEWBUSINESSPRIORLOSS']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- For Display Only -->
	<xsl:template name="PRIOR_LOSS_DISPLAY">
		<xsl:choose>
			<xsl:when test="PRIOR_LOSS_SURCHARGE = 'Y'">Applied</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- For Display In The Label -->
	<xsl:template name="PRIOR_LOSS_LABEL_VALUE">
		<xsl:choose>
			<xsl:when test="PRIOR_LOSS_SURCHARGE = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGE']/NODE[@ID='NEWBUSINESSPRIORLOSS']/ATTRIBUTES/@SURCHARGE" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--Templates for Charge-Insured to 80% to 99% of Replacement Cost [Factor,Credit,Display](END)   -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for Charge - Wood Stove [Factor,Credit,Display](START)			  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="WSTOVE">
		<xsl:choose>
			<xsl:when test="WOODSTOVE_SURCHARGE = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGE']/NODE[@ID='WOODSTOVE']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Woodstove Surcharge-->
	<xsl:template name="WSTOVE_SURCHARGE">
		<xsl:choose>
			<xsl:when test="WOODSTOVE_SURCHARGE = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGE']/NODE[@ID='WOODSTOVE']/ATTRIBUTES/@SURCHARGE" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- For Display Only-->
	<xsl:template name="WSTOVE_DISPLAY">
		<xsl:choose>
			<xsl:when test="WOODSTOVE_SURCHARGE = 'Y'">'Applied'</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for Charge - Wood Stove [Factor,Credit,Display](END)  			  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for Surcharges BREEDDOG [Factor,Display](SATRT)		  			  -->
	<!-- ============================================================================================ -->
	<xsl:template name="BREEDDOG">
		<xsl:choose>
			<xsl:when test="STATENAME='MICHIGAN' and normalize-space(DOGFACTOR) = 'Y'">
				<xsl:variable name="VAR_DOG_FACTOR">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGE']/NODE[@ID='SPECIFICBREEDOFDOGS']/ATTRIBUTES/@FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DOGSURCHARGE">
					<xsl:value-of select="DOGSURCHARGE" />
				</xsl:variable>
				<xsl:variable name="TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<!--xsl:variable name="VAR_COVERAGE">
					<xsl:call-template name="C-VALUE" />
				</xsl:variable-->
				<xsl:variable name="VAR_ADJUSTEDBASE">
					<xsl:call-template name="CALL_ADJUSTEDBASE" />
					<!--<xsl:call-template name="DEWLLING-MAIN" />-->
				</xsl:variable>
				<!-- subtract from adjusted base -->
				<xsl:variable name="VAR_SEASONALSECONDARYCREDIT">
					<xsl:call-template name="SEASONALSECONDARYCREDIT" />
				</xsl:variable>
				<xsl:variable name="VAR_MEDICAL_CHARGES">
					<xsl:call-template name="MEDICAL_CHARGES" />
				</xsl:variable>
				<xsl:variable name="VAR_LIABLITY_CHARGES">
					<xsl:call-template name="LIABLITY_CHARGES" />
				</xsl:variable>
				
				<xsl:variable name="FIRST_STEP" select="(($VAR_ADJUSTEDBASE - ($VAR_MEDICAL_CHARGES + $VAR_LIABLITY_CHARGES)) + $VAR_SEASONALSECONDARYCREDIT)" />
				<xsl:variable name="SECOND_STEP" select="$FIRST_STEP * $VAR_DOG_FACTOR" />
				<xsl:variable name="VAR2" select="format-number($SECOND_STEP,'##.0000')" />
				<xsl:variable name="THIRD_STEP" select="$VAR2 * $VAR_DOGSURCHARGE" />
				<xsl:variable name="VAR3" select="round(format-number($THIRD_STEP,'##.0000'))" />				
				
				<!-- return value of adjusted base  -->
				<!--<xsl:value-of select="round(round((($VAR_ADJUSTEDBASE - ($VAR_MEDICAL_CHARGES + $VAR_LIABLITY_CHARGES))+ $VAR_SEASONALSECONDARYCREDIT) * $VAR_DOG_FACTOR) * $VAR_DOGSURCHARGE)" />-->
				<xsl:value-of select ="$VAR3"></xsl:value-of>
				
			</xsl:when>
			<xsl:otherwise>
			0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- No OF Dog Surcharge-->
	<xsl:template name="NO_OF_DOGS">
		<xsl:choose>
			<xsl:when test="STATENAME='MICHIGAN' and DOGSURCHARGE &gt; 0">
			(<xsl:value-of select="DOGSURCHARGE" />)
		</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DOGS_CHARGE_INPERCENT">
		<xsl:choose>
			<xsl:when test="STATENAME='MICHIGAN' and (DOGSURCHARGE &gt; 0 or BREEDOFDOG != 'OTH')">
				<xsl:variable name="VAR1">
					<xsl:value-of select="DOGSURCHARGE" />
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGE']/NODE[@ID='SPECIFICBREEDOFDOGS']/ATTRIBUTES/@SURCHARGE" />
				</xsl:variable>
				<xsl:value-of select="$VAR1*$VAR2" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for Surcharges BREEDDOG [Factor,Credit,Display](END)  			  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for Surcharges PREFERRED_PLUS_DISPLAY[HO-20,HO-21,HO-24,HO-25](START)		  -->
	<!-- This Template has been created because in PREFERRED_PLUS_DISPLAY option (ADDITIONAL COVERAGES)
	 has been displayed as  'Included' 															  -->
	<!-- ============================================================================================ -->
	<!-- For Display Only -->
	<xsl:template name="PREFERRED_PLUS_DISPLAY">
		<!-- Will be included by default for HO-5 otherwise calculate -->
		<xsl:choose>
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO5">Included</xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- For Calculation [Including in Additional Coverages] -->
	<xsl:template name="PREFERRED_PLUS_COVERAGE">
		<!-- Will be included by default for HO-5 therefore pass 0 otherwise calculate -->
		<xsl:choose>
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO5">0.00</xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Surcharges PREFERRED_PLUS_DISPLAY[HO-20,HO-21,HO-24,HO-25](END)  		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for HO-20 Preferred Plus [Factor](SATRT)				  			  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP">
		<xsl:variable name="TERMMONTHS" select="TERMFACTOR" />
		<xsl:choose>
			<xsl:when test="HO20 ='Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO20']/ATTRIBUTES[@MONTHS=$TERMMONTHS]/@COST"></xsl:value-of>
			</xsl:when>
			<xsl:when test="HO21 ='Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO21']/ATTRIBUTES[@MONTHS=$TERMMONTHS]/@COST"></xsl:value-of>
			</xsl:when>
			<xsl:when test="HO22 ='Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO22']/ATTRIBUTES[@MONTHS=$TERMMONTHS]/@COST"></xsl:value-of>
			</xsl:when>
			<xsl:when test="HO23 ='Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO23']/ATTRIBUTES[@MONTHS=$TERMMONTHS]/@COST"></xsl:value-of>
			</xsl:when>
			<xsl:when test="HO24 ='Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO24']/ATTRIBUTES[@MONTHS=$TERMMONTHS]/@COST"></xsl:value-of>
			</xsl:when>
			<xsl:when test="HO25 ='Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PREMIER_COVERAGE_HO25']/ATTRIBUTES[@MONTHS=$TERMMONTHS]/@COST"></xsl:value-of>
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for HO-20 Preferred Plus [Factor](END)				  			  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for HO-25 PREMIER VIP COVERAGE [Factor](SATRT)  					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_25_PREMIER_VIP_COVERAGE">
		<xsl:variable name="TERMMONTHS" select="TERMFACTOR" />
		<xsl:choose>
			<xsl:when test="STATENAME = 'INDIANA'">0.00</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="HO25 ='Y'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PREMIER_COVERAGE_HO25']/ATTRIBUTES[@MONTHS=$TERMMONTHS]/@COST"></xsl:value-of>
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for HO-11 REPLACEMENT COST PERSONAL PROPERTY [Factor](SATRT)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY">
		<xsl:variable name="TERMMONTHS" select="TERMFACTOR" />
		<xsl:choose>
			<xsl:when test="HO11 ='Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO11']/ATTRIBUTES[@MONTHS=$TERMMONTHS]/@COST" />
			</xsl:when>
			<xsl:otherwise>
			0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- For Display Only -->
	<xsl:template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY_DISPLAY">
		<xsl:choose>
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO5">
			'Included'
		</xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- For Calculation [Including in Additional Coverages] -->
	<xsl:template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY_COVERAGE">
		<xsl:choose>
			<xsl:when test="normalize-space(PRODUCTNAME) = $POLICYTYPE_HO5">
			0.00
		</xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for HO-11 REPLACEMENT COST PERSONAL PROPERTY [Factor](END)		  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for HO-34 Replacement Cost Personal Property [Factor](SATRT)		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_34_REPLACEMENT_COST">
		<xsl:variable name="PNAME" select="PRODUCTNAME" />
		<xsl:variable name="TERM" select="TERMFACTOR" />
		<xsl:variable name="VAR_REPLACEMENT_COST_FACTOR">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO34']/ATTRIBUTES[@FORM= $PNAME and @MONTHS=$TERM]/@FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="normalize-space(HO34) = 'Y'">
				<xsl:variable name="P_MINIMUMVALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO34']/ATTRIBUTES[@FORM = $PNAME and @MONTHS=$TERM]/@MINIMUM_PREMIUM" />
				<xsl:variable name="P_MAXIMUMVALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO34']/ATTRIBUTES[@FORM = $PNAME and @MONTHS=$TERM]/@MAXIMUM_PREMIUM" />
				<xsl:variable name="VAR_HO34_RC">
					<xsl:choose>
						<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO5">
							<xsl:variable name="VAR_BASE">
								<xsl:call-template name="DEWLLING-MAIN" />
							</xsl:variable>
							<xsl:variable name="VAR_COVERAGE">
								<xsl:call-template name="C-VALUE" />
							</xsl:variable>
							<xsl:variable name="VAR_REPLACEMENTCOST">
								<xsl:call-template name="REPLACEMENT_COST_FACTOR" />
							</xsl:variable>
							<xsl:variable name="VAR_DEDUCTIBLE">
								<xsl:call-template name="DFACTOR" />
							</xsl:variable>
							<xsl:variable name="VAR_TERMFACTOR">
								<xsl:call-template name="TERM_FACTOR" />
							</xsl:variable>
							<xsl:value-of select="round($VAR_BASE * $VAR_COVERAGE * $VAR_REPLACEMENTCOST *$VAR_TERMFACTOR) *$VAR_DEDUCTIBLE *$VAR_REPLACEMENT_COST_FACTOR" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:call-template name="HO_34_REPLACEMENT_COST_FACTORVALUE" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<!--xsl:choose>
					<xsl:when test="$VAR_HO34_RC &gt; $P_MINIMUMVALUE and $VAR_HO34_RC &lt; $P_MAXIMUMVALUE">
						<xsl:value-of select="$VAR_HO34_RC" />
					</xsl:when>
					<xsl:when test="$VAR_HO34_RC &gt;= $P_MAXIMUMVALUE">
						<xsl:value-of select="$P_MAXIMUMVALUE" />
					</xsl:when>
					<xsl:when test="$VAR_HO34_RC &lt;= $P_MINIMUMVALUE">
						<xsl:value-of select="$P_MINIMUMVALUE" />
					</xsl:when>
					<xsl:otherwise>
						(<xsl:value-of select="$VAR_HO34_RC" />)
					</xsl:otherwise>
				</xsl:choose-->
				<xsl:choose>
					<xsl:when test="format-number($VAR_HO34_RC,'#.00') &lt;= $P_MINIMUMVALUE">
						<xsl:value-of select="$P_MINIMUMVALUE" />
					</xsl:when>
					<xsl:when test="normalize-space($P_MAXIMUMVALUE)!='' and format-number($VAR_HO34_RC,'#.00') &gt;= $P_MAXIMUMVALUE">
						<xsl:value-of select="$P_MAXIMUMVALUE" />
					</xsl:when>
					<xsl:when test="normalize-space($P_MAXIMUMVALUE)='' and format-number($VAR_HO34_RC,'#.00') &gt; $P_MINIMUMVALUE">
						<xsl:value-of select="$VAR_HO34_RC" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$VAR_HO34_RC" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for HO-34 Replacement Cost Personal Property [Factor](END)		  -->
	<!--		    					  FOR MICHIGAN												  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for HO-34 Replacement Cost Personal Property [Factor](SATRT)		  -->
	<!--		    					  FOR INDIANA												  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_34_REPLACEMENT_COST_INDINA">
		<xsl:variable name="TERM" select="TERMFACTOR" />
		<xsl:variable name="PNAME" select="PRODUCTNAME" />
		<xsl:variable name="VAR_REPLACEMENT_COST_FACTOR">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO34']/ATTRIBUTES[@FORM= $PNAME and @MONTHS=$TERM]/@FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="HO34 = 'Y'">
				<!-- Calculate the value -->
				<xsl:variable name="VAR_HO34_COMPONENT_VALUE">
					<xsl:choose>
						<xsl:when test="(PRODUCTNAME = $POLICYTYPE_HO5 and  PRODUCT_PREMIER = $POLICYDESC_PREMIER)">
							<xsl:variable name="VAR_BASE">
								<xsl:call-template name="DEWLLING-MAIN" />
							</xsl:variable>
							<xsl:variable name="VAR_COVERAGE">
								<xsl:call-template name="C-VALUE" />
							</xsl:variable>
							<xsl:variable name="VAR_REPLACEMENTCOST">
								<xsl:call-template name="REPLACEMENT_COST_FACTOR" />
							</xsl:variable>
							<xsl:variable name="VAR_INSURANCESCORE">
								<xsl:call-template name="INSURANCE_SCORE_CREDIT" />
							</xsl:variable>
							<xsl:variable name="VAR_TERMFACTOR">
								<xsl:call-template name="TERM_FACTOR" />
							</xsl:variable>
							<xsl:variable name="VAR_DFACTOR">
								<xsl:call-template name="DFACTOR" />
							</xsl:variable>
							<xsl:value-of select="($VAR_BASE * $VAR_COVERAGE * $VAR_REPLACEMENTCOST *$VAR_INSURANCESCORE *$VAR_TERMFACTOR *$VAR_DFACTOR) *$VAR_REPLACEMENT_COST_FACTOR" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:call-template name="HO_34_REPLACEMENT_COST_FACTORVALUE" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<!-- Get the maximum and minimum values against form and term months -->
				<xsl:variable name="P_MINIMUM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO34']/ATTRIBUTES[@FORM= $PNAME and @MONTHS=$TERM]/@MINIMUM_PREMIUM" />
				<xsl:variable name="P_MAXIMUM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO34']/ATTRIBUTES[@FORM= $PNAME and @MONTHS=$TERM]/@MAXIMUM_PREMIUM" />
				<!-- Check for minimum and maximum values. If less than min. send minimum, if greater than max. send max value else calculated val 
			 -->
				<xsl:choose>
					<xsl:when test="format-number($VAR_HO34_COMPONENT_VALUE,'#.00') &lt;= format-number($P_MINIMUM,'#.00')">
						<xsl:value-of select="$P_MINIMUM" />
					</xsl:when>
					<xsl:when test="normalize-space($P_MAXIMUM)!='' and format-number($VAR_HO34_COMPONENT_VALUE,'#.00') &gt;= $P_MAXIMUM">
						<xsl:value-of select="$P_MAXIMUM" />
					</xsl:when>
					<xsl:when test="normalize-space($P_MAXIMUM)='' and format-number($VAR_HO34_COMPONENT_VALUE,'#.00') &gt; $P_MINIMUM">
						<xsl:value-of select="$VAR_HO34_COMPONENT_VALUE" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$VAR_HO34_COMPONENT_VALUE" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="SELECT_HO_34">
		<xsl:choose>
			<xsl:when test="normalize-space(STATENAME) ='INDIANA'  and PRODUCTNAME != $POLICYTYPE_HO5">
				<xsl:call-template name="HO_34_REPLACEMENT_COST_INDINA" />
			</xsl:when>
			<xsl:when test="normalize-space(STATENAME) ='INDIANA'  and PRODUCTNAME = $POLICYTYPE_HO5">
				0.00
			</xsl:when>
			<xsl:when test="normalize-space(STATENAME) ='MICHIGAN'  and PRODUCTNAME != $POLICYTYPE_HO5">
				<xsl:call-template name="HO_34_REPLACEMENT_COST" />
			</xsl:when>
			<xsl:when test="normalize-space(STATENAME) ='MICHIGAN'  and PRODUCTNAME = $POLICYTYPE_HO5">
				0.00
			</xsl:when>
			<xsl:otherwise>
				0.00
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ADJUSTED_BASE_HO34">
		<xsl:variable name="VAR_CALL_ADJUSTEDBASE">
			<xsl:call-template name="CALL_ADJUSTEDBASE" />
		</xsl:variable>
		<xsl:variable name="VAR_ADDITIONAL_AMOUNT_COVERAGE">
			<xsl:call-template name="ADDITIONAL_AMOUNT_COVERAGE" />
		</xsl:variable>
		<xsl:variable name="VAR_LIABLITY_CHARGES">
			<xsl:call-template name="LIABLITY_CHARGES" />
		</xsl:variable>
		<xsl:variable name="VAR_MEDICAL_CHARGES">
			<xsl:call-template name="MEDICAL_CHARGES" />
		</xsl:variable>
		<xsl:value-of select="(($VAR_CALL_ADJUSTEDBASE + $VAR_ADDITIONAL_AMOUNT_COVERAGE) -($VAR_LIABLITY_CHARGES + $VAR_MEDICAL_CHARGES))" />
	</xsl:template>
	<xsl:template name="HO_34_REPLACEMENT_COST_FACTORVALUE">
		<xsl:variable name="PNAME" select="PRODUCTNAME" />
		<xsl:variable name="TERM" select="TERMFACTOR" />
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_ADJUSTED_BASE">
			<xsl:call-template name="ADJUSTED_BASE_HO34" />
		</xsl:variable>
		<xsl:variable name="VAR_REPLACEMENT_COST_FACTOR">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO34']/ATTRIBUTES[@FORM= $PNAME and @MONTHS=$TERM]/@FACTOR" />
		</xsl:variable>
		<xsl:value-of select="round($VAR_ADJUSTED_BASE * $VAR_REPLACEMENT_COST_FACTOR)" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for HO-34 Replacement Cost Personal Property [Factor](END)		  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for Surcharges PREFERRED_PLUS_DISPLAY[HO-20,HO-21,HO-24,HO-25](START)		  -->
	<!-- This Template has been created because in PREFERRED_PLUS_DISPLAY option (ADDITIONAL COVERAGES)
	 has been displayed as  'Included' 															  -->
	<!-- ============================================================================================ -->
	<!-- For Display Only -->
	<xsl:template name="SELECT_HO_34_DISPLAY">
		<xsl:choose>
			<xsl:when test="STATENAME ='MICHIGAN' and PRODUCTNAME != $POLICYTYPE_HO5 ">
				<xsl:call-template name="HO_34_REPLACEMENT_COST" />
			</xsl:when>
			<xsl:when test="STATENAME ='MICHIGAN' and PRODUCTNAME = $POLICYTYPE_HO5 ">
				Included
			</xsl:when>
			<xsl:when test="normalize-space(STATENAME) ='INDIANA' and PRODUCTNAME != $POLICYTYPE_HO5 "> <!--and  PRODUCT_PREMIER !=$POLICYDESC_REPLACEMENT" -->
				<xsl:call-template name="SELECT_HO_34" />
			</xsl:when>
			<xsl:when test="normalize-space(STATENAME) ='INDIANA' and PRODUCTNAME = $POLICYTYPE_HO5 "> <!--and  PRODUCT_PREMIER !=$POLICYDESC_REPLACEMENT" -->
				Included
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- For Calculation [Including in Additional Coverages] -->
	<xsl:template name="SELECT_HO_34_COVERAGE">
		<xsl:choose>
			<!-- For Michigan State -->
			<xsl:when test="STATENAME ='MICHIGAN' and PRODUCTNAME != $POLICYTYPE_HO5 ">
				<xsl:call-template name="HO_34_REPLACEMENT_COST" />
			</xsl:when>
			<xsl:when test="STATENAME ='MICHIGAN' and PRODUCTNAME = $POLICYTYPE_HO5 ">
				0.00
			</xsl:when>
			<!-- For Indiana State-->
			<xsl:when test="normalize-space(STATENAME) ='INDIANA'">
				<xsl:call-template name="SELECT_HO_34" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Surcharges PREFERRED_PLUS_DISPLAY[HO-20,HO-21,HO-24,HO-25](END)		  -->
	<!-- This Template has been created because in PREFERRED_PLUS_DISPLAY option (ADDITIONAL COVERAGES)
	 has been displayed as  'Included' 															  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for HO-96 REPLACEMENT COST PERSONAL PROPERTY [Factor](SATRT)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_96_REPLACEMENT_COST_PERSONAL_PROPERTY">
		<xsl:variable name="TERM" select="TERMFACTOR" />
		<xsl:variable name="PHO96FINALVALUE" select="HO96FINALVALUE"></xsl:variable>
		<xsl:choose>
			<xsl:when test="$PHO96FINALVALUE = 0.00">0.00</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO96']/ATTRIBUTES[@LIMITS = $PHO96FINALVALUE and @MONTHS=$TERM ]/@COST" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for HO-96 REPLACEMENT COST PERSONAL PROPERTY [Factor](END)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for HO-312 BUSINESS PROPERTY INCREASED LIMITS [Factor](START)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_312_BUSINESS_PROPERTY_INCREASED_LIMITS">
		<xsl:variable name="PHO312VALUE" select="HO312ADDITIONAL"></xsl:variable>
		<xsl:variable name="PHO312COST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='BUSINESS_PROPERTY_INCREASED_LIMITS_HO312']/ATTRIBUTES/@COST"></xsl:variable>
		<xsl:variable name="PHO312INCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='BUSINESS_PROPERTY_INCREASED_LIMITS_HO312']/ATTRIBUTES/@INCREASEDAMOUNT"></xsl:variable>
		<xsl:variable name="TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="normalize-space($PHO312VALUE) !='' and $PHO312VALUE &gt; 0">
				<xsl:value-of select="($PHO312COST * ($PHO312VALUE div $PHO312INCREASEDAMOUNT)) * $TERMFACTOR" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for HO-312 BUSINESS PROPERTY INCREASED LIMITS [Factor](END)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for HO-48 OTHER STRUCTURE INCREASED LIMITS [Factor](START)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_48_OTHER_STRUCTURE_INCREASED_LIMITS">
		<xsl:variable name="PHO48VALUE" select="HO48ADDITIONAL"></xsl:variable>
		<xsl:variable name="PHO48COST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_INCREASED_LIMITS_HO48']/ATTRIBUTES/@COST"></xsl:variable>
		<xsl:variable name="PHO48INCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_INCREASED_LIMITS_HO48']/ATTRIBUTES/@INCREASEDAMOUNT"></xsl:variable>
		<xsl:variable name="TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PHO48VALUE !='' and $PHO48VALUE &gt; 0">
				<xsl:value-of select="(($PHO48COST * $PHO48VALUE) div $PHO48INCREASEDAMOUNT) * $TERMFACTOR" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for HO-48 OTHER STRUCTURE INCREASED LIMITS [Factor](END)			  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for HO-40 OTHER STRUCTURE RENTED TO OTHERS [Factor](START)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_40_OTHER_STRUCTURE_RENTED_TO_OTHERS">
		<xsl:variable name="PHO40VALUE" select="HO40ADDITIONAL"></xsl:variable>
		<xsl:variable name="PHO40COST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_RENTED_TO_OTHERS_HO40']/ATTRIBUTES/@COST"></xsl:variable>
		<xsl:variable name="PHO40INCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_RENTED_TO_OTHERS_HO40']/ATTRIBUTES/@INCREASEDAMOUNT"></xsl:variable>
		<xsl:variable name="TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PHO40VALUE  !='' and $PHO40VALUE &gt; 0">
				<xsl:value-of select="(($PHO40COST * ($PHO40VALUE div $PHO40INCREASEDAMOUNT))* $TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>
			0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for HO-40 OTHER STRUCTURE RENTED TO OTHERS [Factor](END)			  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for HO-42 OTHER STRUCTURE WITH INCIDENTAL  [Factor](START)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_42_OTHER_STRUCTURE_WITH_INCIDENTAL">
		0.00
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for HO-42 OTHER STRUCTURE WITH INCIDENTAL  [Factor](END)			  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Template for HO-53 INCREASED CREDIT CARD [Factor](START)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_53_INCREASED_CREDIT_CARD">
		<xsl:variable name="PHO53VALUE" select="HO53ADDITIONAL+HO53INCLUDE"></xsl:variable>
		<xsl:variable name="TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:if test="$PHO53VALUE &gt; 0">
			<xsl:value-of select="round($HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_CREDIT_CARD']/ATTRIBUTES[@LIMIT = $PHO53VALUE]/@COST * $TERMFACTOR)" />
		</xsl:if>
		<xsl:if test="$PHO53VALUE &lt;= 0">
		0.00
	</xsl:if>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Template for HO-53 INCREASED CREDIT CARD [Factor](START)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Template for UNSCHEDULED_JEWELRY_ADDITIONAL [Additional](START)				  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="UNSCHEDULED_JEWELRY_ADDITIONAL">
		<xsl:variable name="PJEWELRYVALUE" select="UNSCHEDULEDJEWELRYADDITIONAL"></xsl:variable>
		<xsl:variable name="PJEWELRYINCLUDE" select="UNSCHEDULEDJEWELRYINCLUDE"></xsl:variable>
		<xsl:variable name="PJEWELRYCOST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='A']/@COST"></xsl:variable>
		<xsl:variable name="PJEWELRYINCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='A']/@INCREMENT_AMOUNT"></xsl:variable>
		<xsl:variable name="PJEWELRYMAXADDITIONALAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='A']/@MAX_INCREMENT_AMOUNT"></xsl:variable>
		<xsl:variable name="PJEWELRYMAXCOST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='A']/@MAX_COST"></xsl:variable>
		<xsl:variable name="TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="normalize-space($PJEWELRYVALUE)!='' and $PJEWELRYVALUE &gt; 0">
				<!--xsl:choose>
					<xsl:when test="($PJEWELRYINCLUDE + $PJEWELRYVALUE) &gt; $PJEWELRYMAXADDITIONALAMOUNT ">
						<xsl:value-of select="round($PJEWELRYMAXCOST * $TERMFACTOR)" />
					</xsl:when>
					<xsl:otherwise-->
				<xsl:value-of select="round((($PJEWELRYCOST * $PJEWELRYVALUE) div $PJEWELRYINCREASEDAMOUNT ) * $TERMFACTOR)" />
				<!--/xsl:otherwise>
				</xsl:choose-->
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Template for UNSCHEDULED_JEWELRY_ADDITIONAL [Additional](END)				  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template for MONEY [Additional](START)							  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="MONEY">
		<xsl:variable name="PMONEYVALUE" select="MONEYADDITIONAL"></xsl:variable>
		<xsl:variable name="PMONEYCOST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='B']/@COST"></xsl:variable>
		<xsl:variable name="PMONEYINCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='B']/@INCREMENT_AMOUNT"></xsl:variable>
		<xsl:variable name="TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="normalize-space($PMONEYCOST) !='' and $PMONEYCOST &gt; 0">
				<xsl:value-of select="round((($PMONEYCOST * $PMONEYVALUE) div $PMONEYINCREASEDAMOUNT ) * $TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template for MONEY [Additional](END)							  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template for SECURITIES [Additional](START)						  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="SECURITIES">
		<xsl:variable name="PSECURITIESVALUE" select="SECURITIESADDITIONAL"></xsl:variable>
		<xsl:variable name="PSECURITIESCOST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='C']/@COST"></xsl:variable>
		<xsl:variable name="PSECURITIESINCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='C']/@INCREMENT_AMOUNT"></xsl:variable>
		<xsl:variable name="TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="normalize-space($PSECURITIESVALUE)!= '' and $PSECURITIESVALUE &gt; 0">
				<xsl:value-of select="round((($PSECURITIESCOST * $PSECURITIESVALUE) div $PSECURITIESINCREASEDAMOUNT ) * $TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template for SECURITIES [Additional](END)						  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--					Template for SILVERWARE, GOLDWARE [Additional](START)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="SILVERWARE_GOLDWARE">
		<xsl:variable name="PSILVERWARE_GOLDWAREVALUE" select="SILVERWAREADDITIONAL"></xsl:variable>
		<xsl:variable name="PSILVERWARE_GOLDWARECOST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='D']/@COST"></xsl:variable>
		<xsl:variable name="PSILVERWARE_GOLDWAREINCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='D']/@INCREMENT_AMOUNT"></xsl:variable>
		<!--
	((<xsl:value-of select ="$PSILVERWARE_GOLDWARECOST"></xsl:value-of>*
	<xsl:value-of select ="$PSILVERWARE_GOLDWAREVALUE"></xsl:value-of>) DIV
	<xsl:value-of select ="$PSILVERWARE_GOLDWAREINCREASEDAMOUNT"></xsl:value-of>)
	-->
		<xsl:variable name="TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="normalize-space($PSILVERWARE_GOLDWAREVALUE) !='' and $PSILVERWARE_GOLDWAREVALUE &gt; 0">
				<xsl:value-of select="round((($PSILVERWARE_GOLDWARECOST * $PSILVERWARE_GOLDWAREVALUE) div $PSILVERWARE_GOLDWAREINCREASEDAMOUNT ) * $TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Template for SILVERWARE, GOLDWARE [Additional](END)						  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template for FIREARMS[Additional](START)						  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="FIREARMS">
		<xsl:variable name="PFIREARMSVALUE" select="FIREARMSADDITIONAL"></xsl:variable>
		<xsl:variable name="PFIREARMSCOST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='E']/@COST"></xsl:variable>
		<xsl:variable name="PFIREARMSINCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='E']/@INCREMENT_AMOUNT"></xsl:variable>
		<!--
	((<xsl:value-of select ="$PFIREARMSCOST"></xsl:value-of>*
	<xsl:value-of select ="$PFIREARMSVALUE"></xsl:value-of>) DIV
	<xsl:value-of select ="$PFIREARMSINCREASEDAMOUNT"></xsl:value-of>)
	-->
		<xsl:variable name="TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="normalize-space($PFIREARMSVALUE) !='' and $PFIREARMSVALUE &gt; 0">
				<xsl:value-of select="round((($PFIREARMSCOST * $PFIREARMSVALUE) div $PFIREARMSINCREASEDAMOUNT) * $TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template for FIREARMS[Additional](END)							  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--					Templates for HO-277 Ordinance or Law[Additional](START)				  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ORDINANCE">
		<xsl:choose>
			<xsl:when test="normalize-space(HO277) = 'Y'">
				<xsl:variable name="P_MINIMUMVALUE_ANNUAL" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='LAW_COVERAGE']/ATTRIBUTES/@MINIMUM" />
				<xsl:variable name="VAR_ORDINANCE_COST_FACTORVALUE">
					<xsl:call-template name="ORDINANCE_COST_FACTORVALUE" />
				</xsl:variable>
				<xsl:variable name="TERM">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="P_MINIMUMVALUE" select="round($P_MINIMUMVALUE_ANNUAL * $TERM)" />
				<!-- 
			IF (<xsl:value-of select="$VAR_ORDINANCE_COST_FACTORVALUE"/> &lt; <xsl:value-of select='format-number($P_MINIMUMVALUE,"#.00")'/>)
			THEN
				<xsl:value-of select="$P_MINIMUMVALUE"/>
			ELSE 
				<xsl:value-of select="$VAR_ORDINANCE_COST_FACTORVALUE"/> -->
				<xsl:choose>
					<xsl:when test="$VAR_ORDINANCE_COST_FACTORVALUE &lt; $P_MINIMUMVALUE">
						<xsl:value-of select="$P_MINIMUMVALUE" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="round($VAR_ORDINANCE_COST_FACTORVALUE )" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ORDINANCE_COST_FACTORVALUE">
		<xsl:variable name="VAR_ADJUSTED">
			<xsl:call-template name="CALL_ADJUSTEDBASE" />
		</xsl:variable>
		<xsl:variable name="VAR_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='LAW_COVERAGE']/ATTRIBUTES/@CHARGE" />
		<xsl:value-of select="$VAR_ADJUSTED * $VAR_FACTOR " />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Templates for HO-277 Ordinance or Law[Additional](END)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--   Templates for HO-455 Identity Fraud Expense Coverage[Factor, Limit, DEductible](SATRT)	  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_455_IDENTITY_FRAUD">
		<xsl:choose>
			<xsl:when test="HO455 = 'Y'">
				<xsl:variable name="VAR_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='FRAUD_EXPENSE']/ATTRIBUTES/@RATE" />
				<xsl:variable name="TERM">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:value-of select="round($VAR_FACTOR * $TERM)" />
			</xsl:when>
			<xsl:otherwise>
		0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--LIMITS -->
	<xsl:template name="HO_455_LIMIT">
		<xsl:choose>
			<xsl:when test="HO455 = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='FRAUD_EXPENSE']/ATTRIBUTES/@COVERAGE" />
			</xsl:when>
			<xsl:otherwise>
		0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- DEDUCTIBLE -->
	<xsl:template name="HO_455_DEDUCTIBLE">
		<xsl:choose>
			<xsl:when test="HO455 = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='FRAUD_EXPENSE']/ATTRIBUTES/@DEDUCTIBLE" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--   Templates for HO-455 Identity Fraud Expense Coverage[Factor, Limit, DEductible](SATRT)	  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for HO-315 Earthquake (Zone 0)[Additional](SATRT)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_315_EARTHQUAKE">
		<!-- Variables for FORM, ZONE, INCREASEDCOVERAGEC, INCREASEDCOVGD -->
		<xsl:variable name="PEXTERIOR_CONSTRUCTION_F_M" select="EXTERIOR_CONSTRUCTION_F_M"></xsl:variable>
		<xsl:variable name="PPERSONALPROPERTYINCREASEDLIMITADDITIONAL" select="PERSONALPROPERTYINCREASEDLIMITADDITIONAL"></xsl:variable>
		<xsl:variable name="PADDITIONALLIVINGEXPENSEADDITIONAL" select="ADDITIONALLIVINGEXPENSEADDITIONAL"></xsl:variable>
		<xsl:variable name="PFORM" select="PRODUCTNAME"></xsl:variable>
		<xsl:variable name="PZONE" select="EARTHQUAKEZONE" />
		<!--IF('<xsl:value-of select="HO315"/>' = 'Y') THEN	-->
		<xsl:choose>
			<xsl:when test="HO315 = 'Y'">
				<!--	1. Get Rate for Frame per thousand by policy form and construction
							2. Multiply Rate with Coverage A Limit
							3. Multiply Rate with increased coverage C limit 
							4. Multiply Rate with increased coverage D limit 
							5. Add Steps 2. , 3. , 4. -->
				<xsl:variable name="VAR_RATE_PER_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='EARTHQUAKE']/@RATE_PER_VALUE" />
				<xsl:choose>
					<xsl:when test="$PEXTERIOR_CONSTRUCTION_F_M = 'F'">
						<xsl:variable name="VAR_FRAME" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='EARTHQUAKE']/ATTRIBUTES[@FORM = $PFORM and @ZONE = $PZONE]/@FRAME" />
						<xsl:variable name="VAR1">
							<xsl:value-of select="$VAR_FRAME * DWELLING_LIMITS div $VAR_RATE_PER_VALUE" />
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:choose>
								<xsl:when test="$PPERSONALPROPERTYINCREASEDLIMITADDITIONAL &gt; 0.00">
									<xsl:value-of select="($VAR_FRAME * $PPERSONALPROPERTYINCREASEDLIMITADDITIONAL) div $VAR_RATE_PER_VALUE" />
								</xsl:when>
								<xsl:otherwise>0.00</xsl:otherwise>
							</xsl:choose>
						</xsl:variable>
						<xsl:variable name="VAR3">
							<xsl:choose>
								<xsl:when test="$PADDITIONALLIVINGEXPENSEADDITIONAL &gt; 0.00">
									<xsl:value-of select="($VAR_FRAME * $PADDITIONALLIVINGEXPENSEADDITIONAL) div $VAR_RATE_PER_VALUE" />
								</xsl:when>
								<xsl:otherwise>0.00</xsl:otherwise>
							</xsl:choose>
						</xsl:variable>
						<xsl:variable name="VAR4">
							<xsl:call-template name="TERM_FACTOR" />
						</xsl:variable>
						<xsl:value-of select="($VAR1 + $VAR2 + $VAR3) * $VAR4" />
					</xsl:when>
					<xsl:when test="$PEXTERIOR_CONSTRUCTION_F_M = 'M'">
						<xsl:variable name="VAR_MASONARY">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='EARTHQUAKE']/ATTRIBUTES[@FORM = $PFORM and @ZONE = $PZONE]/@MASONRY" />
						</xsl:variable>
						<xsl:variable name="VAR1">
							<xsl:value-of select="$VAR_MASONARY * DWELLING_LIMITS div $VAR_RATE_PER_VALUE" />
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:choose>
								<xsl:when test="$PPERSONALPROPERTYINCREASEDLIMITADDITIONAL &gt; 0.00">
									<xsl:value-of select="($VAR_MASONARY * $PPERSONALPROPERTYINCREASEDLIMITADDITIONAL) div $VAR_RATE_PER_VALUE" />
								</xsl:when>
								<xsl:otherwise>0.00</xsl:otherwise>
							</xsl:choose>
						</xsl:variable>
						<xsl:variable name="VAR3">
							<xsl:choose>
								<xsl:when test="$PADDITIONALLIVINGEXPENSEADDITIONAL &gt; 0.00">
									<xsl:value-of select="($VAR_MASONARY * $PADDITIONALLIVINGEXPENSEADDITIONAL) div $VAR_RATE_PER_VALUE" />
								</xsl:when>
								<xsl:otherwise>0.00</xsl:otherwise>
							</xsl:choose>
						</xsl:variable>
						<xsl:variable name="VAR4">
							<xsl:call-template name="TERM_FACTOR" />
						</xsl:variable>
						<xsl:value-of select="($VAR1 + $VAR2 + $VAR3) * $VAR4" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
				<!--
					IF('<xsl:value-of select="$PEXTERIOR_CONSTRUCTION_F_M"/>' ='F')
					THEN 
						
																						
						(<xsl:value-of select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='EARTHQUAKE']/ATTRIBUTES[@FORM = $PFORM and @ZONE = $PZONE]/@FRAME" />* (<xsl:value-of select ="DWELLING_LIMITS"/> DIV 1000.00) 			
						+
						(
							IF (<xsl:value-of select="$PPERSONALPROPERTYINCREASEDLIMITADDITIONAL"/> &gt; 0.00)
							THEN
								<xsl:value-of select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='EARTHQUAKE']/ATTRIBUTES[@FORM = $PFORM and @ZONE = $PZONE]/@FRAME" />* (<xsl:value-of select ="$PPERSONALPROPERTYINCREASEDLIMITADDITIONAL"/> DIV 1000.00) 
							ELSE
								0.00
						)
						+ 
						(
							IF (<xsl:value-of select="$PADDITIONALLIVINGEXPENSEADDITIONAL"/> &gt; 0.00)
							THEN
								<xsl:value-of select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='EARTHQUAKE']/ATTRIBUTES[@FORM = $PFORM and @ZONE = $PZONE]/@FRAME" />* (<xsl:value-of select ="$PADDITIONALLIVINGEXPENSEADDITIONAL"/> DIV 1000.00) 
							ELSE
								0.00
						)) * <xsl:call-template name ="TERM_FACTOR"/>
					ELSE IF('<xsl:value-of select="$PEXTERIOR_CONSTRUCTION_F_M"/>' ='M') 
					THEN
						(<xsl:value-of select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='EARTHQUAKE']/ATTRIBUTES[@FORM = $PFORM and @ZONE = $PZONE]/@MASONRY" />* (<xsl:value-of select ="DWELLING_LIMITS"/> DIV 1000.00) 
						+
						(
							IF (<xsl:value-of select="$PPERSONALPROPERTYINCREASEDLIMITADDITIONAL"/> &gt; 0.00)
							THEN
								<xsl:value-of select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='EARTHQUAKE']/ATTRIBUTES[@FORM = $PFORM and @ZONE = $PZONE]/@MASONRY"/>* (<xsl:value-of select ="$PPERSONALPROPERTYINCREASEDLIMITADDITIONAL"/> DIV 1000.00) 
							ELSE
								0.00
						)
						+ 
						(
							IF (<xsl:value-of select="$PADDITIONALLIVINGEXPENSEADDITIONAL"/> &gt; 0.00)
							THEN
								<xsl:value-of select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='EARTHQUAKE']/ATTRIBUTES[@FORM = $PFORM and @ZONE = $PZONE]/@MASONRY"/>* (<xsl:value-of select ="$PADDITIONALLIVINGEXPENSEADDITIONAL"/> DIV 1000.00) 
							ELSE
								0.00
						)) * <xsl:call-template name ="TERM_FACTOR"/>
					ELSE
						0.00
				-->
			</xsl:when>
			<xsl:otherwise>
			0.00
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for HO-315 Earthquake (Zone 0)[Additional](END)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Template for HO-327 Water Back-Up and Sump Pump Overflow(START)				  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_327_WATER_BACK_UP">
		<xsl:choose>
			<xsl:when test="normalize-space(HO327) ='' or HO327 = 0 or HO327 ='N'">
		0
		</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="PHO327VALUE" select="HO327"></xsl:variable>
				<xsl:variable name="PHO327COST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='WATER_BACKUP']/ATTRIBUTES/@COST"></xsl:variable>
				<xsl:variable name="PHO327INCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='WATER_BACKUP']/ATTRIBUTES/@INCREMENTAL_AMOUNT"></xsl:variable>
				<xsl:variable name="VAR_TERM_FACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:value-of select="(($PHO327COST * $PHO327VALUE) div $PHO327INCREASEDAMOUNT)*$VAR_TERM_FACTOR" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Template for HO-327 Water Back-Up and Sump Pump Overflow(END)				  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Template for  HO-33 Condo-Unit Owners Rental to Others(START)				  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_33_CONDO_UNIT">
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="CALL_ADJUSTEDBASE" />
		</xsl:variable>
		<xsl:variable name="VAR_ADDITIONL_AMOUNT">
			<xsl:call-template name="ADDITIONAL_AMOUNT_COVERAGE" />
		</xsl:variable>
		<xsl:variable name="VAR_TERM_FACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_SEASONALSECONDARYCREDIT">
			<xsl:call-template name="SEASONALSECONDARYCREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_MEDICAL_CHARGES">
			<xsl:call-template name="MEDICAL_CHARGES" />
		</xsl:variable>
		<xsl:variable name="VAR_LIABLITY_CHARGES">
			<xsl:call-template name="LIABLITY_CHARGES" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="normalize-space(HO33) = 'Not Rented' or normalize-space(HO33) = 'NOT RENTED'">0.00</xsl:when>
			<xsl:when test="normalize-space(HO33) = 'Under 2 Months' or normalize-space(HO33) = 'UNDER 2 MONTHS'">
				<xsl:variable name="PMINIMUMVALUE1" select="round($HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID = 'OPTIONALSECTIONCOVG']/NODE[@ID='HO_33_CONDO_UNIT']/ATTRIBUTES[@OPTIONS = '1']/@MINAMOUNT * $VAR_TERM_FACTOR)" />
				<xsl:variable name="VAR_FACTOR">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID = 'OPTIONALSECTIONCOVG']/NODE[@ID='HO_33_CONDO_UNIT']/ATTRIBUTES[@OPTIONS = '1']/@FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_CALCULATED_VALUE">
					<xsl:value-of select="((($VAR_BASE + $VAR_ADDITIONL_AMOUNT)- ($VAR_MEDICAL_CHARGES + $VAR_LIABLITY_CHARGES))+ $VAR_SEASONALSECONDARYCREDIT)* $VAR_FACTOR" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$VAR_CALCULATED_VALUE &lt; format-number($PMINIMUMVALUE1,'#.00')">
						<xsl:value-of select="format-number($PMINIMUMVALUE1,'#.00')" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$VAR_CALCULATED_VALUE" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="normalize-space(HO33) = '2 to 6 Months' or normalize-space(HO33) = '2 TO 6 MONTHS'">
				<xsl:variable name="PMINIMUMVALUE2" select="round($HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID = 'OPTIONALSECTIONCOVG']/NODE[@ID='HO_33_CONDO_UNIT']/ATTRIBUTES[@OPTIONS = '2']/@MINAMOUNT * $VAR_TERM_FACTOR)" />
				<xsl:variable name="VAR_FACTOR">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID = 'OPTIONALSECTIONCOVG']/NODE[@ID='HO_33_CONDO_UNIT']/ATTRIBUTES[@OPTIONS = '2']/@FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_CALCULATED_VALUE">
					<xsl:value-of select="((($VAR_BASE + $VAR_ADDITIONL_AMOUNT) - ($VAR_MEDICAL_CHARGES + $VAR_LIABLITY_CHARGES))+ $VAR_SEASONALSECONDARYCREDIT)* $VAR_FACTOR" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$VAR_CALCULATED_VALUE &lt; format-number($PMINIMUMVALUE2,'#.00')">
						<xsl:value-of select="format-number($PMINIMUMVALUE2,'#.00')" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$VAR_CALCULATED_VALUE" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Template for  HO-33 Condo-Unit Owners Rental to Others(END)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Template for HO-287 Mine Subsidence Coverage (Start )						  -->
	<!--		    					  FOR INDIANA												  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_287_MINE_SUBSIDENCE_COVERAGE">
		<xsl:variable name="PCOVERAGES" select="MINESUBSIDENCE_ADDITIONAL" />
		<xsl:variable name="VAR_TERM_FACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="STATENAME = 'INDIANA'">
				<xsl:choose>
					<xsl:when test="HO287 = 'Y'">
						<xsl:value-of select="round($HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID = 'OTHER']/NODE[@ID='MINE_SUBSIDENCE_COVERAGE']/ATTRIBUTES[@MINCOVERAGE &lt;= $PCOVERAGES and @MAXCOVERAGE &gt;= $PCOVERAGES]/@PREMIUM * $VAR_TERM_FACTOR)" />
					</xsl:when>
					<xsl:otherwise>
				0.00
				</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
		0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- MINE SUBSIDENCE DEDUCTIBLE - A deductible of 2% (Minimum $250; Maximum $500) applies to each loss -->
	<xsl:template name="MINE_DED_AMOUNT_DISPLAY">
		<xsl:variable name="PCOVERAGES" select="MINESUBSIDENCE_ADDITIONAL" />
		<xsl:variable name="var_DEDUCTIBLE_PERCENT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHER']/NODE[@ID ='MINE_SUBSIDENCE_COVERAGE']/@DEDUCTIBLE_PERCENT" />
		<xsl:variable name="var_DEDUCTIBLE_AMOUNT">
			<xsl:value-of select="($PCOVERAGES * $var_DEDUCTIBLE_PERCENT) div 100" />
		</xsl:variable>
		<xsl:variable name="var_DED_MINIMUM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHER']/NODE[@ID ='MINE_SUBSIDENCE_COVERAGE']/@DED_MINIMUM" />
		<xsl:variable name="var_DED_MAXIMUM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHER']/NODE[@ID ='MINE_SUBSIDENCE_COVERAGE']/@DED_MAXIMUM" />
		<xsl:choose>
			<xsl:when test="$var_DEDUCTIBLE_AMOUNT &lt; $var_DED_MINIMUM">
				<xsl:value-of select="round($var_DED_MINIMUM)" />
			</xsl:when>
			<xsl:when test="$var_DEDUCTIBLE_AMOUNT &gt; $var_DED_MAXIMUM">
				<xsl:value-of select="round($var_DED_MAXIMUM)" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="round($var_DEDUCTIBLE_AMOUNT)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Template for HO-287 Mine Subsidence Coverage (End)							  -->
	<!--		    					  FOR INDIANA												  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Template for HO-9 Collapse From Sub Surface Water (Start )					  -->
	<!--		    					  FOR INDIANA												  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_9_COLLAPSE_FROM_SUB_SURFACE_WATER">
		<xsl:choose>
			<xsl:when test="STATENAME = 'INDIANA'">
				<xsl:choose>
					<xsl:when test="HO9 = 'Y'">
						<xsl:variable name="SURFACEWATER" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID = 'COLLAPSE_FROM_SUB_SURFACE_WATER_HO9']/ATTRIBUTES/@COST" />
						<xsl:variable name="TERM">
							<xsl:call-template name="TERM_FACTOR" />
						</xsl:variable>
						<xsl:value-of select="$SURFACEWATER * $TERM" />
					</xsl:when>
					<xsl:otherwise>
				0.00
				</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
		0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Template for HO-9 Collapse From Sub Surface Water (End )					  -->
	<!--		    					  FOR INDIANA												  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Template for HO-200 Waterbed Liability (Start )								 -->
	<!--		    					  FOR INDIANA												  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_200_WATERBED_LIABILITY">
		<xsl:variable name="VAR_TERM_FACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="HO200 = 'Y'">
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='WATERBED_LIABILITY']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:choose>
					<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0">
						<xsl:value-of select="round($VAR_LIABILITY_LIMIT_GP * $VAR_TERM_FACTOR)" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Template for HO-200 Waterbed Liability (End )								  -->
	<!--		    					  FOR INDIANA												  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template for HO-490 SPECIFIC STRUCTURED (START)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_490_SPECIFIC_STRUCTURED">
		<xsl:variable name="VAR_TERM_FACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="SPECIFICSTRUCTURESADDITIONAL = 0.00 or normalize-space(SPECIFICSTRUCTURESADDITIONAL)=''">0.00</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="PHO490VALUE" select="SPECIFICSTRUCTURESADDITIONAL"></xsl:variable>
				<xsl:variable name="PHO490COST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='SPECIFIC_STRUC_AWAY']/ATTRIBUTES/@RATE"></xsl:variable>
				<xsl:variable name="PHO490INCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='SPECIFIC_STRUC_AWAY']/ATTRIBUTES/@PER_EACH_ADDITION_OF"></xsl:variable>
				<xsl:value-of select="round(($PHO490COST * $PHO490VALUE) div $PHO490INCREASEDAMOUNT * $VAR_TERM_FACTOR)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template for HO-490 SPECIFIC STRUCTURED (END)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template for HO-489 SPECIFIC STRUCTURED (START)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_489_SPECIFIC_STRUCTURED">
		<xsl:variable name="PHO489VALUE" select="REPAIRCOSTADDITIONAL"></xsl:variable>
		<xsl:variable name="PHO489COST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUC_REPAIR']/ATTRIBUTES/@RATE"></xsl:variable>
		<xsl:variable name="PHO489INCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUC_REPAIR']/ATTRIBUTES/@PER_EACH_ADDITION_OF"></xsl:variable>
		<xsl:variable name="VAR_TERM_FACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="normalize-space($PHO489VALUE) !='' and $PHO489VALUE &gt; 0">
				<xsl:value-of select="round(($PHO489VALUE div $PHO489INCREASEDAMOUNT) * $PHO489COST * $VAR_TERM_FACTOR)" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template for HO-489 SPECIFIC STRUCTURED (END)	  				  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template for PRIMARY RESIDENCE (START)							  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="PRIMARY_RESIDENCE">
		<!--xsl:variable name="VAR_TERM_FACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="RESIDENCE_EMP_NUMBER = 0.00">0.00</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR_PRIMARY_RES_EMP_NO" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID ='RESIDENCE_EMPLOYEES']/@PRIMARY_RES_EMP_NO" />
				<xsl:choose>
					<xsl:when test="RESIDENCE_EMP_NUMBER &gt; $VAR_PRIMARY_RES_EMP_NO">
						<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
						<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
						<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='RESIDENCE_EMPLOYEES']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
						<xsl:variable name="VAR_FOR_EACHADDITIONAL">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='RESIDENCE_EMPLOYEES']/@FOR_EACH_ADDITION" />
						</xsl:variable>
						<xsl:choose>
							<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0">
								<xsl:value-of select="round(($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY) * $VAR_TERM_FACTOR)" />
							</xsl:when>
							<xsl:otherwise>0.00</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose-->
		0.00
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template for PRIMARY RESIDENCE (END)							  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template for RESIDENCE EMPLOYEES (START)						  -->
	<!-- ============================================================================================ -->
	<xsl:template name="RESIDENCE_EMPLOYEES">
		<xsl:variable name="VAR_TERM_FACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="RESIDENCE_EMP_NUMBER=0.00">0.00</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR_PRIMARY_RES_EMP_NO" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID ='RESIDENCE_EMPLOYEES']/@PRIMARY_RES_EMP_NO" />
				<xsl:choose>
					<xsl:when test="RESIDENCE_EMP_NUMBER &gt;  $VAR_PRIMARY_RES_EMP_NO">
						<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
						<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
						<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='RESIDENCE_EMPLOYEES']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
						<xsl:variable name="VAR_FOR_EACHADDITIONAL">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='RESIDENCE_EMPLOYEES']/@FOR_EACH_ADDITION" />
						</xsl:variable>
						<xsl:choose>
							<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0">
								<xsl:value-of select="round(($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY) * (RESIDENCE_EMP_NUMBER - $VAR_PRIMARY_RES_EMP_NO)*$VAR_TERM_FACTOR)" />
							</xsl:when>
							<xsl:otherwise>0.00</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template for RESIDENCE EMPLOYEES (END)							  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Template for Additional Premises Occupied By Insured (START)				  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADDITIONAL_PREMISES">
		<xsl:variable name="VAR_TERM_FACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="OCCUPIED_INSURED = 0.00">0.00</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='ADD_PREMISES_OCC_INSURED']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='ADD_PREMISES_OCC_INSURED']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0">
						<xsl:value-of select="round(OCCUPIED_INSURED *($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY)*$VAR_TERM_FACTOR)" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Template for Additional Premises Occupied By Insured (END)					  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--	Template for HO-40 Additional Premises Rented to Others - Residence Premises  (START)	  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADDITIONAL_RESIDENCE_PREMISES">
		<xsl:variable name="VAR_TERM_FACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="RESIDENCE_PREMISES = 0.00">0.00</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='ADD_PREMISES_RENTED_AT_PREMISES']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='ADD_PREMISES_RENTED_AT_PREMISES']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0">
						<xsl:value-of select="round(RESIDENCE_PREMISES * ($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY)*$VAR_TERM_FACTOR)" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--	Template for HO-40 Additional Premises Rented to Others - Residence Premises  (END)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--	Template for HO-70 Additional Premises Rented - Other Location - 1 Family (1)(START)	  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADDITIONAL_OTHER_LOCATION_PREMISES_FAMILY1">
		<xsl:choose>
			<xsl:when test="OTHER_LOC_1FAMILY = 0.00">0.00</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:variable name="TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='ADD_PREMISES_RENTED_OUTSIDE_PREMISES1']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='ADD_PREMISES_RENTED_OUTSIDE_PREMISES1']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0">
						<xsl:value-of select="round((($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY)*$TERMFACTOR)*OTHER_LOC_1FAMILY)" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--	Template for HO-70 Additional Premises Rented - Other Location - 1 Family (1)(END	)	  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--	Template for HO-70 Additional Premises Rented - Other Location - 1 Family (2)(START)	  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADDITIONAL_OTHER_LOCATION_PREMISES_FAMILY2">
		<xsl:choose>
			<xsl:when test="OTHER_LOC_2FAMILY = 0.00">0.00</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='ADD_PREMISES_RENTED_OUTSIDE_PREMISES2']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='ADD_PREMISES_RENTED_OUTSIDE_PREMISES2']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:variable name="TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0">
						<xsl:value-of select="round((($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY)*$TERMFACTOR)*OTHER_LOC_2FAMILY)" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--	Template for HO-70 Additional Premises Rented - Other Location - 1 Family (3)(END)        -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--	Template for HO-42 Incidental Office Private School or Studio On Premises (START)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_42_ON_PREMISES">
		<xsl:choose>
			<xsl:when test="normalize-space(ONPREMISES_HO42) = 'Y'">
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='INCIDENTAL_OFCE_ON_PREMISES']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='INCIDENTAL_OFCE_ON_PREMISES']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:variable name="TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0">
						<xsl:value-of select="($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY) * $TERMFACTOR" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--	Template for HO-42 Incidental Office Private School or Studio On Premises (END)			  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--	Template for HO-42 Other Structure with incidental business coverage readded (START)	  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO42_BUSINESS_OTHER_STRUCTURE_WITH_INCIDENTAL">
		<xsl:choose>
			<xsl:when test="normalize-space(LOCATED_OTH_STRUCTURE) ='Y'">
				<xsl:variable name="VAR_OTHER_STRUCTURE_WITH_INCIDENTAL_HO42">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_WITH_INCIDENTAL_HO42']/ATTRIBUTES/@COST" />
				</xsl:variable>
				<xsl:variable name="VAR_RATEPERVAL_HO42">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_WITH_INCIDENTAL_HO42']/ATTRIBUTES/@RATEPERVAL_HO42" />
				</xsl:variable>
				<xsl:variable name="TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_HO48INCLUDE">
					<xsl:choose>
						<xsl:when test="HO48INCLUDE='N/A'">0</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="HO48INCLUDE" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:value-of select="((($VAR_HO48INCLUDE+HO48ADDITIONAL) div $VAR_RATEPERVAL_HO42) * $VAR_OTHER_STRUCTURE_WITH_INCIDENTAL_HO42) * $TERMFACTOR" />				
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--	Template for HO-42 HO-42 Other Structure with incidental business coverage readded (END)  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--	Template for HO-42 Incidental Office Private School or Studio Instruction Only (START)	  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_42_INSTRUCTION_ONLY">
		<xsl:choose>
			<xsl:when test="INSTRUCTIONONLY_HO42 = 'Y'">
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='INCIDENTAL_OFCE_ON_INSTRUCTN']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='INCIDENTAL_OFCE_ON_INSTRUCTN']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:variable name="TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<!--
			<xsl:value-of select="$VAR_RATE_PERVAL_MEDPAY"/>#
			<xsl:value-of select="$VAR_LIMIT"/>#
			<xsl:value-of select="$VAR_LIABILITY_LIMIT_GP"/>#
			<xsl:value-of select="$VAR_FOR_EACHADDITIONAL"/>#
			<xsl:value-of select="$TERMFACTOR"/>#
			
			
			-->
				<xsl:choose>
					<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0">
						<xsl:value-of select="($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY) * $TERMFACTOR" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--	Template for HO-42 Incidental Office Private School or Studio Instruction Only (END)	  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--	Template for HO-43 Incidental Office Private School or Studio Off Premises  (START)		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_43_OFF_PREMISES">
		<xsl:choose>
			<xsl:when test="OFF_PREMISES_HO43 = 'Y'">
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='OFF_PERSUITS']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='OFF_PERSUITS']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:variable name="TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="normalize-space($VAR_LIABILITY_LIMIT_GP) != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0">
						<xsl:value-of select="($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) div $VAR_RATE_PERVAL_MEDPAY)* $VAR_FOR_EACHADDITIONAL) * $TERMFACTOR " />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--	Template for HO-43 Incidental Office Private School or Studio Off Premises  (END)		  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--			Template for HO-71 Business Pursuits Class A - (1 persons)  (START)				  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_71_CLASSA">
		<xsl:choose>
			<xsl:when test="CLERICAL_OFFICE_HO71 = 'Y'">
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='CLASSA']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='CLASSA']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:variable name="TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0">
						<xsl:value-of select="($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY) * $TERMFACTOR" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--			Template for  HO-71 Business Pursuits -    Class B - (1 persons)  (START)		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_71_CLASSB">
		<xsl:choose>
			<xsl:when test="SALESMEN_INC_INSTALLATION = 'Y'">
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='CLASSB']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='CLASSB']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:variable name="TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0">
						<xsl:value-of select="($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY) * $TERMFACTOR" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--			Template for  HO-71 Business Pursuits -    Class C - (1 persons) (START)		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_71_CLASSC">
		<xsl:choose>
			<xsl:when test="TEACHER_ATHELETIC = 'Y'">
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='CLASSC']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='CLASSC']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:variable name="TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0">
						<xsl:value-of select="($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY) *$TERMFACTOR" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--			Template for  HO-71 Business Pursuits -    Class D - (1 persons) (START)		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_71_CLASSD">
		<xsl:choose>
			<xsl:when test="TEACHER_NOC = 'Y'">
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='CLASSD']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='CLASSD']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:variable name="TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0">
						<xsl:value-of select="($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY) * $TERMFACTOR" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--	Template for  HO-72 Farm Liability - Incidental Farming Residence Premises (START)		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_72_INCIDENTAL_FARMING">
		<xsl:choose>
			<xsl:when test="INCIDENTAL_FARMING_HO72 = 'Y'">
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='INCIDENTAL_FARMING_RESIDENCE']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='INCIDENTAL_FARMING_RESIDENCE']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:variable name="TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0">
						<xsl:value-of select="($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY) * $TERMFACTOR" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--	Template for  HO-73 Farm Liability - Owned By Insured Rented to Others (2) (START)		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_73_OTH_LOC_OPR_OTHERS">
		<xsl:variable name="PHO73" select="OTH_LOC_OPR_OTHERS_HO73" />
		<xsl:choose>
			<xsl:when test="$PHO73 &gt; 0">
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='RENTED_TO_OTHERS']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='RENTED_TO_OTHERS']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:variable name="TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0">
						<xsl:value-of select="(($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY) * OTH_LOC_OPR_OTHERS_HO73)* $TERMFACTOR" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--	Template for  HO-73 Farm Liability - Owned By Insured Rented to Others (2) (END)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--	Template for  HO-73 Farm Liability - Owned/Operated By Insured/Insured Employee (START)	  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_73_OTH_LOC_OPR_EMPL">
		<xsl:variable name="PHO73" select="OTH_LOC_OPR_EMPL_HO73" />
		<xsl:choose>
			<xsl:when test="$PHO73 &gt; 0">
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='OPERATED_BY_INSURED']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='OPERATED_BY_INSURED']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:variable name="TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0">
						<xsl:value-of select="(($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY) * OTH_LOC_OPR_EMPL_HO73 )*$TERMFACTOR" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--	Template for  HO-73 Farm Liability - Owned/Operated By Insured/Insured Employee (END)	  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--						Template for  HO-82 Personal Injury (START)							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_82_PERSONAL_INJURY">
		<!-- Personal Injury is free in case of Indiana if HO-24 Premier VIP is taken-->
		<xsl:choose>
			<xsl:when test="STATENAME = $STATE_INDIANA and HO24='Y'">
				0.00
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="normalize-space(PIP_HO82) = 'Y'">
						<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
						<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
						<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='PERSONAL_INJURY_COVERAGE']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
						<xsl:variable name="VAR_FOR_EACHADDITIONAL">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='PERSONAL_INJURY_COVERAGE']/@FOR_EACH_ADDITION" />
						</xsl:variable>
						<xsl:variable name="TERMFACTOR">
							<xsl:call-template name="TERM_FACTOR" />
						</xsl:variable>
						<xsl:choose>
							<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0">
								<xsl:value-of select=" ($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY) *$VAR_TERMFACTOR" />
							</xsl:when>
							<xsl:otherwise>0.00</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="HO_82_PERSONAL_INJURY_DISPLAY">
		<!-- Personal Injury is free in case of Indiana if HO-24 Premier VIP is taken-->
		<xsl:choose>
			<xsl:when test="STATENAME = $STATE_INDIANA and HO24='Y'">
				'Included'
			</xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="HO_82_PERSONAL_INJURY" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--						Template INLAND MARINE (START)										-->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_900_BICYCLES_PREMIUM">
		<xsl:choose>
			<xsl:when test="SCH_BICYCLE_AMOUNT != '' and SCH_BICYCLE_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_BICYCLE_DED" />
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='BICYCLES']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_BICYCLE_AMOUNT  div $VAR_RATE_PERVAL_IM)* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="CAMERAS_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_CAMERA_AMOUNT) != '' and SCH_CAMERA_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_CAMERA_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='CAMERA']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_CAMERA_AMOUNT div $VAR_RATE_PERVAL_IM) * $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="HO_900_CELLULAR_PHONES_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_CELL_AMOUNT) != '' and SCH_CELL_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_CELL_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='CELLULAR']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_CELL_AMOUNT div $VAR_RATE_PERVAL_IM ) * $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="FURS_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_FURS_AMOUNT) != '' and SCH_FURS_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_FURS_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='FURS']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select=" round( round((SCH_FURS_AMOUNT div $VAR_RATE_PERVAL_IM) * $VAR_DED_FACTOR)* $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="GOLF_EQUIPMENT_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_GOLF_AMOUNT) != '' and SCH_GOLF_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_GOLF_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='GOLFEQUIP']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_GOLF_AMOUNT div $VAR_RATE_PERVAL_IM) * $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="GUNS_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_GUNS_AMOUNT) != '' and SCH_GUNS_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_GUNS_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='GUNS']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_GUNS_AMOUNT div $VAR_RATE_PERVAL_IM) * $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="JEWELRY_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_JWELERY_AMOUNT) != '' and SCH_JWELERY_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_JWELERY_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='JEWELERY']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_JWELERY_AMOUNT div $VAR_RATE_PERVAL_IM) * $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MUSICAL_NON_PROFESSIONAL_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_MUSICAL_AMOUNT) != '' and SCH_MUSICAL_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_MUSICAL_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='MUSICAL']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_MUSICAL_AMOUNT div $VAR_RATE_PERVAL_IM ) * $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PERSONAL_COMPUTERS_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_PERSCOMP_AMOUNT) != '' and SCH_PERSCOMP_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_PERSCOMP_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='PC']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_PERSCOMP_AMOUNT  div $VAR_RATE_PERVAL_IM)* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="SILVER_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_SILVER_AMOUNT) != '' and SCH_SILVER_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_SILVER_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='SILVER']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_SILVER_AMOUNT div $VAR_RATE_PERVAL_IM) * $VAR_DED_FACTOR)   * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="STAMPS_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_STAMPS_AMOUNT) != '' and SCH_STAMPS_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_STAMPS_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='STAMPS']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_STAMPS_AMOUNT  div $VAR_RATE_PERVAL_IM )* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="RARE_COINS_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_RARECOINS_AMOUNT) != '' and SCH_RARECOINS_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_RARECOINS_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='COINS']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_RARECOINS_AMOUNT  div $VAR_RATE_PERVAL_IM )* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="FINE_ARTS_WITHOUT_BREAKAGE_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_FINEARTS_WO_BREAK_AMOUNT) != '' and SCH_FINEARTS_WO_BREAK_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_FINEARTS_WO_BREAK_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='FINEARTSWITHOUTBREAKAGE']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_FINEARTS_WO_BREAK_AMOUNT  div $VAR_RATE_PERVAL_IM )* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="FINE_ARTS_BREAKAGE_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_FINEARTS_BREAK_AMOUNT) != '' and SCH_FINEARTS_BREAK_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_FINEARTS_BREAK_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='FINEARTSWITHBREAKAGE']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_FINEARTS_BREAK_AMOUNT  div $VAR_RATE_PERVAL_IM )* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="HANDICAP_ELECTRONICS">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_HANDICAP_ELECTRONICS_AMOUNT) != '' and SCH_HANDICAP_ELECTRONICS_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_HANDICAP_ELECTRONICS_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='HANDICAP_ELECTRONICS']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_HANDICAP_ELECTRONICS_AMOUNT  div $VAR_RATE_PERVAL_IM )* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="HEARING_AIDS">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_HEARING_AIDS_AMOUNT) != '' and SCH_HEARING_AIDS_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_HEARING_AIDS_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='HEARING_AIDS']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_HEARING_AIDS_AMOUNT  div $VAR_RATE_PERVAL_IM )* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="INSULIN_PUMPS">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_INSULIN_PUMPS_AMOUNT) != '' and SCH_INSULIN_PUMPS_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_INSULIN_PUMPS_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='INSULIN_PUMPS']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_INSULIN_PUMPS_AMOUNT  div $VAR_RATE_PERVAL_IM )* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MART_KAY_AMWAY">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_MART_KAY_AMOUNT) != '' and SCH_MART_KAY_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_MART_KAY_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='AMWAY']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_MART_KAY_AMOUNT  div $VAR_RATE_PERVAL_IM )* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PERSONAL_COMPUTERS_LAPTOP">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_PERSONAL_COMPUTERS_LAPTOP_AMOUNT) != '' and SCH_PERSONAL_COMPUTERS_LAPTOP_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_PERSONAL_COMPUTERS_LAPTOP_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='PC_LAPTOP']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_PERSONAL_COMPUTERS_LAPTOP_AMOUNT  div $VAR_RATE_PERVAL_IM )* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="SALESMAN_SUPPLY">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_SALESMAN_SUPPLIES_AMOUNT) != '' and SCH_SALESMAN_SUPPLIES_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_SALESMAN_SUPPLIES_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='SALESMAN_SUPPLY']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_SALESMAN_SUPPLIES_AMOUNT  div $VAR_RATE_PERVAL_IM )* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="SCUBA_DIVING">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_SCUBA_DRIVING_AMOUNT) != '' and SCH_SCUBA_DRIVING_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_SCUBA_DRIVING_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='SCUBA_DIVING']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_SCUBA_DRIVING_AMOUNT  div $VAR_RATE_PERVAL_IM )* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="SNOW_SKY">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_SNOW_SKIES_AMOUNT) != '' and SCH_SNOW_SKIES_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_SNOW_SKIES_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='SNOW_SKY']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_SNOW_SKIES_AMOUNT  div $VAR_RATE_PERVAL_IM )* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="TACK_SADDLE">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_TACK_SADDLE_AMOUNT) != '' and SCH_TACK_SADDLE_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_TACK_SADDLE_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='TACK_SADDLE']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_TACK_SADDLE_AMOUNT  div $VAR_RATE_PERVAL_IM )* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="TOOLS_AT_PREMISIS">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_TOOLS_PREMISES_AMOUNT) != '' and SCH_TOOLS_PREMISES_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_TOOLS_PREMISES_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='TOOLS_AT_PREMISIS']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_TOOLS_PREMISES_AMOUNT  div $VAR_RATE_PERVAL_IM )* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="TOOLS_AT_BSNS_LOCN">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_TOOLS_BUSINESS_AMOUNT) != '' and SCH_TOOLS_BUSINESS_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_TOOLS_BUSINESS_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='TOOLS_AT_BSNS_LOCN']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_TOOLS_BUSINESS_AMOUNT  div $VAR_RATE_PERVAL_IM )* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="TRACTORS">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_TRACTORS_AMOUNT) != '' and SCH_TRACTORS_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_TRACTORS_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='TRACTORS']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_TRACTORS_AMOUNT  div $VAR_RATE_PERVAL_IM )* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="TRAIN_COLLECTION">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_TRAIN_COLLECTIONS_AMOUNT) != '' and SCH_TRAIN_COLLECTIONS_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_TRAIN_COLLECTIONS_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='TRAIN_COLLECTION']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_TRAIN_COLLECTIONS_AMOUNT  div $VAR_RATE_PERVAL_IM )* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="WHEELCHAIR">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_WHEELCHAIRS_AMOUNT) != '' and SCH_WHEELCHAIRS_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:variable name="VAR_TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR_DED_AMT" select="SCH_WHEELCHAIRS_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='WHEELCHAIR']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:value-of select="round(round((SCH_WHEELCHAIRS_AMOUNT  div $VAR_RATE_PERVAL_IM )* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Template INLAND MARINE Mart Kay/Amway							  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template Property Expense Fee (START)							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="PROPFEE">
		<!-- CHECK NOT APP FOR HO% PREMIER-->
		<xsl:variable name="TERM" select="TERMFACTOR" />
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PROPRTYFEE']/NODE[@ID='PFEE']/ATTRIBUTES[@TERMMONTHS =$TERM]/@FEES" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template Property Expense Fee (START)							  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template SEASONAL SECONDARY CREDIT  (START)						  -->
	<!--		    							FOR MICHIGAN and INDIANA							  -->
	<!-- ============================================================================================ -->
	<!--If Personal Liability is "Extended From Home" then Premium should not be calculated - Asfa Praveen - 03-JULY-2007 -->
	<xsl:template name="PERSONALLIABILITY_LIMIT_DISPLAY">
		<xsl:choose>
			<xsl:when test="PERSONALLIABILITY_LIMIT !='' and normalize-space(PERSONALLIABILITY_LIMIT) !='EFH'">
				<xsl:choose>
					<xsl:when test="WOLVERINEINSURESPRIMARY ='Y'"></xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="format-number(PERSONALLIABILITY_LIMIT, '###,###')" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--If Medical Payments is "Extended From Home" then Premium should not be calculated - Asfa Praveen - 03-JULY-2007 -->
	<xsl:template name="MEDICALPAYMENTSTOOTHERS_LIMIT_DISPLAY">
		<xsl:choose>
			<xsl:when test="MEDICALPAYMENTSTOOTHERS_LIMIT !='' and normalize-space(MEDICALPAYMENTSTOOTHERS_LIMIT) !='EFH'">
				<xsl:choose>
					<xsl:when test="WOLVERINEINSURESPRIMARY ='Y'"></xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="format-number(MEDICALPAYMENTSTOOTHERS_LIMIT, '###,###')" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="SEASONALSECONDARYCREDIT">
		<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
		<xsl:variable name="VAR_INSURE_PRIMARY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='INSURE_PRIMARY']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@GROUP" />
		<xsl:variable name="VAR_INSURE_NON_PRIMARY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='INSURE_NON_PRIMARY']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@GROUP" />
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="SEASONALSECONDARY='Y' and WOLVERINEINSURESPRIMARY='Y'">
				<xsl:choose>
					<xsl:when test="normalize-space($VAR_INSURE_PRIMARY) != '' and $VAR_INSURE_PRIMARY &gt; 0">
						<xsl:value-of select="round($VAR_INSURE_PRIMARY * $VAR_TERMFACTOR)" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="SEASONALSECONDARY='Y' and WOLVERINEINSURESPRIMARY='N'">
				<xsl:choose>
					<xsl:when test="normalize-space($VAR_INSURE_NON_PRIMARY) != '' and $VAR_INSURE_NON_PRIMARY &gt; 0">
						<xsl:value-of select="round($VAR_INSURE_NON_PRIMARY * $VAR_TERMFACTOR)" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- SEASONAL SECONDARY CREDIT -->
	<xsl:template name="SEASONALSECONDARY_DISPLAY">
		<xsl:choose>
			<xsl:when test="SEASONALSECONDARY = 'Y'">Applied</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="SEASONALSECONDARY_TEXT">
		<xsl:choose>
			<xsl:when test="SEASONALSECONDARY = 'Y' and WOLVERINEINSURESPRIMARY='Y'">
				<xsl:text>-  Credit - Seasonal/Secondary,Wolverine Insures Primary $</xsl:text>
			</xsl:when>
			<xsl:when test="SEASONALSECONDARY = 'Y' and WOLVERINEINSURESPRIMARY='N'">
				<xsl:text>-  Credit - Seasonal/Secondary $</xsl:text>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template SEASONAL SECONDARY CREDIT  (START)						  -->
	<!--		    							FOR MICHIGAN and INDIANA							  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template Described Primary Residence Coverage  (START)			  -->
	<!--		    							FOR MICHIGAN and INDIANA							  -->
	<!-- ============================================================================================ -->
	<!--xsl:template name="DESCRIBED_PRIMARY_RESIDENCE">
		<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
		<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='DESCRIBED_PRIMARY_RESIDENCE']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
		<xsl:variable name="VAR_FOR_EACHADDITIONAL">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='DESCRIBED_PRIMARY_RESIDENCE']/@FOR_EACH_ADDITION" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="normalize-space($VAR_LIABILITY_LIMIT_GP) != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0 and $VAR_LIABILITY_LIMIT_GP != 'Included'">
				<xsl:value-of select="$VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - 1000)* $VAR_FOR_EACHADDITIONAL) div 1000" />
			</xsl:when>
			<xsl:when test="$VAR_LIABILITY_LIMIT_GP = 'Included'">
				<xsl:value-of select="((MEDICALPAYMENTSTOOTHERS_LIMIT - 1000) * $VAR_FOR_EACHADDITIONAL) div 1000" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template-->
	<!-- ============================================================================================ -->
	<!--							Template SEASONAL SECONDARY CREDIT  (START)						  -->
	<!--		    							FOR MICHIGAN and INDIANA							  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--									Template Smoker Factor (START)							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="SMOKER">
		<xsl:choose>
			<xsl:when test="NONSMOKER = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='NONSMOKERCREIT']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- For Display Only -->
	<xsl:template name="SMOKER_DISPLAY">
		<xsl:choose>
			<xsl:when test="NONSMOKER = 'Y'">Applied</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="SMOKER_CREDIT_DISPLAY">
		<xsl:choose>
			<xsl:when test="NONSMOKER = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='NONSMOKERCREIT']/ATTRIBUTES/@CREDIT" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--									Template Smoker Factor (END)							  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--									50% Discount Applied (START)							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="MAX_DISCOUNT_APPLIED_DISPLAY">
		<xsl:variable name="VAR1">
			<xsl:call-template name="AGEOFHOME_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:call-template name="GET_DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR4">
			<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR5">
			<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="MAX_APPLICABLE_CREDIT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
		<xsl:variable name="MAX_APPLICABLE_CREDIT_PERCENT" select="(1.00-$MAX_APPLICABLE_CREDIT)*100" />
		<!-- Check the applied total percentage -->
		<xsl:variable name="FINALCREDIT_EXPERIENCE_PERCENT">
			<xsl:value-of select="((1-$VAR1)*100)+((1-$VAR2)*100)+((1-$VAR3)*100)+((1-$VAR4)*100)+((1-$VAR5)*100)" />
		</xsl:variable>
		<xsl:variable name="FINALCREDIT_EXPERIENCE">
			<xsl:value-of select="((100.00-$FINALCREDIT_EXPERIENCE_PERCENT) div 100.00)" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="STATENAME = 'INDIANA'">
			0.00
		</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="$FINALCREDIT_EXPERIENCE_PERCENT &gt;= $MAX_APPLICABLE_CREDIT_PERCENT">
						<xsl:value-of select="$FINALCREDIT_EXPERIENCE_PERCENT" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--									50% Discount Applied (END)	    						  -->
	<!-- ============================================================================================ -->
	<xsl:template name="SELECT_HO_20_PREFERRED_PLUS_OR_HO_21_VIP">
		<xsl:choose>
			<xsl:when test="normalize-space(HO20) ='Y'">HO-20 Preferred Plus</xsl:when>
			<xsl:when test="normalize-space(HO21) ='Y'">HO-21 Preferred Plus (V.I.P)</xsl:when>
			<xsl:when test="normalize-space(HO22) ='Y'">HO-22 Preferred Plus (V.I.P)</xsl:when>
			<xsl:when test="normalize-space(HO23) ='Y'">HO-23 Preferred Plus (V.I.P)</xsl:when>
			<xsl:when test="normalize-space(HO24) ='Y'">HO-24 Premier (V.I.P)</xsl:when>
			<xsl:when test="normalize-space(HO25) ='Y'">HO-25 Premier (V.I.P)</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Prior Loss Label Display -->
	<xsl:template name="PRIOR_LOSS_LABEL_DISPLAY">
		<xsl:choose>
			<xsl:when test="PRIOR_LOSS_SURCHARGE = 'Y'">
		(
				((((((
						(
							(<xsl:call-template name="DEWLLING-MAIN"></xsl:call-template>)
							*(<xsl:call-template name="C-VALUE"></xsl:call-template>)
							*(<xsl:call-template name="INSURANCE_SCORE_CREDIT"></xsl:call-template>)
							*(<xsl:call-template name="TERM_FACTOR"></xsl:call-template>)
						)
						+
						(
							(<xsl:call-template name="COVERAGE_C_PREMIUM" /> )
							+ (<xsl:call-template name="COVERAGE_D_PREMIUM" />)
						)
					    
				)
				*
				(<xsl:call-template name="DFACTOR" />)
		)*
				(<xsl:call-template name="MULTIPOLICY_FACTOR" />)
			)*
			   
			(<xsl:call-template name="AGEOFHOME_FACTOR" />))*
			(<xsl:call-template name="PROTECTIVEDEVICE" />))*
			(<xsl:call-template name="EXPERIENCEFACTOR" />))*
			(<xsl:call-template name="PRIOR_LOSS" />)
			-
			(((((
					(
						(<xsl:call-template name="DEWLLING-MAIN"></xsl:call-template>)
						*(<xsl:call-template name="C-VALUE"></xsl:call-template>)
						*(<xsl:call-template name="INSURANCE_SCORE_CREDIT"></xsl:call-template>)
						*(<xsl:call-template name="TERM_FACTOR"></xsl:call-template>)
					)
					+
					(
						(<xsl:call-template name="COVERAGE_C_PREMIUM" /> )
						+ (<xsl:call-template name="COVERAGE_D_PREMIUM" />)
					)
				    
			)
			*
			(<xsl:call-template name="DFACTOR" />)
			)*
				(<xsl:call-template name="MULTIPOLICY_FACTOR" />)
			)*
			   
			(<xsl:call-template name="AGEOFHOME_FACTOR" />))*
			(<xsl:call-template name="PROTECTIVEDEVICE" />))*
			(<xsl:call-template name="EXPERIENCEFACTOR" />)
			)
		</xsl:when>
			<xsl:otherwise>
		0
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="HO-65_HO-211">
		<xsl:choose>
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO5 and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT">
		HO-211 Coverage C Increased Special Limits 
	</xsl:when>
			<xsl:otherwise>HO-65 Coverage C Increased Special Limits </xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- START OF TEMPLATE FOR COMP_REMARK -->
	<!--Insurance discount    -->
	<xsl:template name="REMARKINSURANCE">
		<xsl:variable name="VAR_INSURANCESCORE_FACTOR">
			<xsl:call-template name="INSURANCE_SCORE_CREDIT" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$VAR_INSURANCESCORE_FACTOR &gt; 1.00">
				<xsl:text>-  Charge - Insurance Score (Score - </xsl:text>
				<xsl:call-template name="INSURANCE_SCORE_DISPLAY" />
				<xsl:text>)</xsl:text>
			</xsl:when>
			<xsl:otherwise>
				<xsl:text>-  Discount - Insurance Score Credit (Score - </xsl:text>
				<xsl:call-template name="INSURANCE_SCORE_DISPLAY" />
				<xsl:text>) - </xsl:text>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Smoker Discount   -->
	<xsl:template name="REMARKSMOKER">
		<xsl:text>-  Discount - Non smoker</xsl:text>
	</xsl:template>
	<!--Wind and Hail Storm   -->
	<xsl:template name="REMARKHAILSTORM">
		<xsl:text>-	Discount - ACV Loss Settlement Roof Surfaces (Wind or Hail)</xsl:text>
	</xsl:template>
	<!--Dwelling Under construction   -->
	<xsl:template name="REMARKCONSTRUCTION">
		<xsl:text>-  Discount - Under Construction</xsl:text>
	</xsl:template>
	<!--Experince Dsicount   -->
	<xsl:template name="REMARKEXPER">
		<xsl:text>-  Discount - Experience</xsl:text>
	</xsl:template>
	<!--Age of home    -->
	<xsl:template name="REMARKAGEHOME">
		<xsl:text>-  Discount - Age of Home </xsl:text>
	</xsl:template>
	<!-- Protective Dvice-->
	<xsl:template name="REMARKPROTDEVICE">
		<xsl:text>-  Discount - Protective Devices  (HO-216)</xsl:text>
	</xsl:template>
	<!--Multi Policy   -->
	<xsl:template name="REMARKMULTIPOLICY">
		<xsl:text>-  Discount - Multi Policy</xsl:text>
	</xsl:template>
	<!--Valued Customer   -->
	<xsl:template name="REMARKVALUDCUST">
		<xsl:text>-  Discount - Valued Customer</xsl:text>
	</xsl:template>
	<!-- Replacement Cast   -->
	<xsl:template name="REMARKREPLACEMENTCOST">
		<xsl:text>-  Charge	- Insured to 80% to 99% of Replacement Cost</xsl:text>
	</xsl:template>
	<!-- Prior Loss -->
	<xsl:template name="REMARKPRIORLOSS">
		<xsl:text>- Charge - Prior Loss </xsl:text>
	</xsl:template>
	<!--Suburban Prot  -->
	<xsl:template name="REMARKSUBURBAN">
		<xsl:text>- Suburban Protection Class Rating Discount </xsl:text>
	</xsl:template>
	<!--Loss free renewal    -->
	<xsl:template name="REMARKLOSSFREE">
		<xsl:text>- Loss Free Renewal Discount and Loss Surcharge </xsl:text>
	</xsl:template>
	<!-- Wood stove surcharge-->
	<xsl:template name="REMARKWOODSTOVE">
		<xsl:text>- Charge - Wood Stove</xsl:text>
	</xsl:template>
	<!-- Dog Surcharge  -->
	<xsl:template name="REMRKBREEDDOG">
		<xsl:text>-  Charge	- Breed of Dog </xsl:text>
		<xsl:call-template name="NO_OF_DOGS" />
	</xsl:template>
	<!--Seasonal credit  -->
	<xsl:template name="REMARKSEASONALSECONDRY">
		<xsl:choose>
			<xsl:when test="SEASONALSECONDARY = 'Y' and WOLVERINEINSURESPRIMARY='Y'">-  Credit - Seasonal/Secondary, Wolverine Insures Primary</xsl:when>
			<xsl:when test="SEASONALSECONDARY = 'Y' and WOLVERINEINSURESPRIMARY='N'">-  Credit - Seasonal/Secondary</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--									Template for Lables  (START)							  -->
	<!-- ============================================================================================ -->
	<!--									   Group Details										  -->
	<xsl:template name="PRODUCTID0">
		<xsl:choose>
			<xsl:when test="@POLICYTYPE='HO-2 REPLACE'">REPLACEMENT</xsl:when>
			<xsl:when test="@POLICYTYPE='HO-3^REPLACE'">REPLACEMENT</xsl:when>
			<xsl:when test="@POLICYTYPE='HO-5^REPLACE'">REPLACEMENT</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="@POLICYTYPE" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="GROUPID0">
	<xsl:choose>
			<xsl:when test="SEASONALSECONDARY ='Y'">Seasonal/Secondary Residence, </xsl:when>
			<xsl:otherwise>Primary Residence, </xsl:otherwise>
		</xsl:choose>
<xsl:value-of select="EXTERIOR_CONSTRUCTION_DESC" /> (<xsl:value-of select="DOC" />)</xsl:template>
	<xsl:template name="GROUPID1">Fire Class: <xsl:value-of select="FIREPROTECTIONCLASS" /> Hydrant: <xsl:value-of select="FEET2HYDRANT" />, Fire Department: <xsl:value-of select="DISTANCET_FIRESTATION" /> miles</xsl:template>
	<xsl:template name="GROUPID2">Premium Group: <xsl:call-template name="PREMIUM_GROUP" /> , Rated Class:  <xsl:value-of select="FORM_CODE" /><!--xsl:value-of select="EXTERIOR_CONSTRUCTION_F_M" /-->, 
								<xsl:choose>
			<xsl:when test="PRODUCTNAME =$POLICYTYPE_HO4 or PRODUCTNAME =$POLICYTYPE_HO6"> Units - <xsl:value-of select="NUMBEROFUNITS" /></xsl:when>
			<xsl:otherwise> Family - <xsl:value-of select="NUMBEROFFAMILIES" /></xsl:otherwise>
		</xsl:choose></xsl:template>
	<xsl:template name="GROUPID3"><xsl:text>SECTION I - PROPERTY COVERAGES</xsl:text></xsl:template>
	<xsl:template name="GROUPID4"><xsl:text>SECTION II - LIABILITY COVERAGES </xsl:text></xsl:template>
	<xsl:template name="GROUPID5"><xsl:text>POLICY DISCOUNTS AND SURCHARGES </xsl:text></xsl:template>
	<xsl:template name="GROUPID6"><xsl:text>ADDITIONAL COVERAGES</xsl:text></xsl:template>
	<xsl:template name="GROUPID7">
		<xsl:variable name="SHOW_HEADING">
			<xsl:call-template name="DISPLAY_INLAND_MARINE_HEADING" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="normalize-space($SHOW_HEADING) = 'Y'"> INLAND MARINE - SCHEDULED PERSONAL PROPERTY </xsl:when>
			<xsl:otherwise>NODESCRIPTION</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="GROUPID8"><xsl:text>NODESCRIPTION</xsl:text></xsl:template>
	<xsl:template name="GROUPID9"><xsl:text>NODESCRIPTION</xsl:text></xsl:template>
	<!--Step Details-->
	<xsl:template name="STEPID0"><xsl:text>		-  Coverage A - Dwelling </xsl:text></xsl:template>
	<xsl:template name="STEPID1"><xsl:text>		-  Coverage B - Other Structures</xsl:text></xsl:template>
	<xsl:template name="STEPID2"><xsl:text>		-  Coverage C - Personal Property</xsl:text></xsl:template>
	<xsl:template name="STEPID3"><xsl:text>		-  Coverage D - Loss Of Use  </xsl:text></xsl:template>
	<xsl:template name="STEPID4"><xsl:text>		-  Coverage E - Personal Liability - Each Occurrence</xsl:text></xsl:template>
	<xsl:template name="STEPID5"><xsl:text>		-  Coverage F - Medical Payments to Others - Each Person</xsl:text></xsl:template>
	<xsl:template name="STEPID6">
		<xsl:variable name="VAR_INSURANCESCORE_FACTOR">
			<xsl:call-template name="INSURANCE_SCORE_CREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCESCORE_PERCENT">
			<xsl:call-template name="INSURANCESCORE_PERCENT" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$VAR_INSURANCESCORE_FACTOR &gt; 1.00">
			<xsl:text>-  Charge - Insurance Score (Score - </xsl:text><xsl:call-template name="INSURANCE_SCORE_DISPLAY" /><xsl:text>) - </xsl:text><xsl:value-of select="$VAR_INSURANCESCORE_PERCENT" /> 
			</xsl:when>
			<xsl:otherwise>
			<xsl:text>-  Discount - Insurance Score Credit (Score - </xsl:text><xsl:call-template name="INSURANCE_SCORE_DISPLAY" /><xsl:text>) - </xsl:text><xsl:value-of select="$VAR_INSURANCESCORE_PERCENT" /> 
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--New Steps Added START-->
	<xsl:template name="STEPID7"><xsl:text>		-  Discount - Non smoker </xsl:text><xsl:call-template name="SMOKER_CREDIT_DISPLAY" /><xsl:text>%</xsl:text></xsl:template>
	<xsl:template name="STEPID8"><xsl:text>		-  NOTE: Discounts have 50% MAXIMUM DISCOUNT rule applied</xsl:text></xsl:template>
	<!--New Steps Added END -->
	<xsl:template name="STEPID9"><xsl:text>		-  Discount - Under Construction </xsl:text><xsl:call-template name="DWELLING_UNDER_CONSTRUCTION_CREDIT"></xsl:call-template><xsl:text>%</xsl:text></xsl:template>
	<xsl:template name="STEPID10"><xsl:text>	-  Discount - Experience </xsl:text><xsl:call-template name="EXPERIENCECREDIT"></xsl:call-template><xsl:text>%</xsl:text></xsl:template>
	<xsl:template name="STEPID11"><xsl:text>	-  Discount - Age of Home </xsl:text><xsl:call-template name="AGEOFHOME_CREDIT" /><xsl:text>%</xsl:text></xsl:template>
	<xsl:template name="STEPID12"><xsl:text>    -  Discount - Protective Devices  (HO-216) </xsl:text><xsl:call-template name="PROTECTIVEDEVICE_CREDIT" /><xsl:text>%</xsl:text></xsl:template>
	<xsl:template name="STEPID13">
		<xsl:call-template name="MULTIPOLICYCREDIT_DISPLAY" />
	</xsl:template>
	<xsl:template name="STEPID14">
		<xsl:call-template name="VALUEDCUSTOMER_FACTOR_DISPLAY" />
	</xsl:template>
	<xsl:template name="STEPID15">
		<xsl:call-template name="REPLACEMENT_COST_DISPLAY_TEXT" />
	</xsl:template>
	<xsl:template name="STEPID16">
		<xsl:text>- Charge - Prior Loss </xsl:text><xsl:call-template name="PRIOR_LOSS_LABEL_VALUE" /><xsl:text>%</xsl:text>
	</xsl:template>
	<xsl:template name="STEPID17">
		<xsl:text>- Charge - Wood Stove </xsl:text><xsl:call-template name="WSTOVE_SURCHARGE" /><xsl:text>%</xsl:text>
	</xsl:template>
	<xsl:template name="STEPID18"><xsl:text>	-  Charge	- Breed of Dog </xsl:text><xsl:call-template name="NO_OF_DOGS" />  <xsl:call-template name="DOGS_CHARGE_INPERCENT" /><xsl:text>%</xsl:text></xsl:template>
	<xsl:template name="STEPID19">
		<xsl:call-template name="SEASONALSECONDARY_TEXT" />
		<xsl:call-template name="SEASONALSECONDARYCREDIT" />
	</xsl:template>
	<xsl:template name="STEPID20"><xsl:text>	-  </xsl:text><xsl:call-template name="SELECT_HO_20_PREFERRED_PLUS_OR_HO_21_VIP"></xsl:call-template></xsl:template>
	<xsl:template name="STEPID21"><xsl:text>	-  HO-64 Renters Deluxe Endorsement </xsl:text></xsl:template>
	<xsl:template name="STEPID22"><xsl:text>	-  HO-66 Condominium Deluxe Endorsement</xsl:text></xsl:template>
	<xsl:template name="STEPID23"><xsl:text>	-  HO-34 Replacement Cost Personal Property</xsl:text></xsl:template>
	<!-- <xsl:template name = "STEPID1001">	-  HO-64 Renters Deluxe Endorsement </xsl:template> -->
	<xsl:template name="STEPID24"><xsl:text>	-  HO-11 Expanded Replacement Cost for Building</xsl:text></xsl:template>
	<xsl:template name="STEPID25"><xsl:text>	-  HO-96 Fire Department Service Charge (</xsl:text><xsl:value-of select="HO96ADDITIONAL" /><xsl:text> increase)</xsl:text></xsl:template>
	<xsl:template name="STEPID26"><xsl:text>	-  HO-312 Business Property Increase Limits (</xsl:text><xsl:value-of select="HO312ADDITIONAL" /><xsl:text> increase)</xsl:text></xsl:template>
	<xsl:template name="STEPID27"><xsl:text>	-  HO-48 Other Structures Increased Limit</xsl:text></xsl:template>
	<xsl:template name="STEPID28"><xsl:text>	-  HO-40 Other Structure Rented To Others (</xsl:text><xsl:value-of select="HO40ADDITIONAL" /><xsl:text> increase)</xsl:text></xsl:template>
	<xsl:template name="STEPID29"><xsl:text>	-  HO-42 Other Structure With Incidental</xsl:text></xsl:template>
	<xsl:template name="STEPID30"><xsl:text>	-  HO-50 Personal Property Away From Premises (Increase </xsl:text><xsl:value-of select="PERSONALPROPERTYAWAYADDITIONAL" /><xsl:text>)</xsl:text></xsl:template>
	<xsl:template name="STEPID31"><xsl:text>	-  HO-51 Building Additions and Alterations (Increase </xsl:text><xsl:value-of select="format-number(PERSONALPROPERTYINCREASEDLIMITADDITIONAL,'#')" /><xsl:text> )  </xsl:text></xsl:template>
	<xsl:template name="STEPID32"><xsl:text>	-  HO-53 Credit Card and Depositors Forgery (</xsl:text><xsl:value-of select="HO53ADDITIONAL" /><xsl:text> increase)</xsl:text></xsl:template>
	<xsl:template name="STEPID33">
		<xsl:call-template name="HO-65_HO-211" />
	</xsl:template>
	<xsl:template name="STEPID34"><xsl:text>	-:    Unscheduled Jewelry And Furs (</xsl:text><xsl:value-of select="UNSCHEDULEDJEWELRYADDITIONAL" /><xsl:text> increase)</xsl:text></xsl:template>
	<xsl:template name="STEPID35"><xsl:text>	-:    Money (</xsl:text><xsl:value-of select="MONEYADDITIONAL" /><xsl:text> increase)</xsl:text></xsl:template>
	<xsl:template name="STEPID36"><xsl:text>	-:    Securities (</xsl:text><xsl:value-of select="SECURITIESADDITIONAL" /><xsl:text> increase)</xsl:text></xsl:template>
	<xsl:template name="STEPID37"><xsl:text>	-:    Silverware, Goldware And Pewterware (</xsl:text><xsl:value-of select="SILVERWAREADDITIONAL" /><xsl:text> increase)</xsl:text></xsl:template>
	<xsl:template name="STEPID38"><xsl:text>	-:    Firearms (</xsl:text><xsl:value-of select="FIREARMSADDITIONAL" /><xsl:text> increase)</xsl:text></xsl:template>
	<xsl:template name="STEPID39"><xsl:text>	-  HO-35 Loss Assessment Coverage(</xsl:text><xsl:value-of select="HO35ADDITIONAL" /><xsl:text> increase)</xsl:text></xsl:template>
	<xsl:template name="STEPID40"><xsl:text>	-  HO-277 Ordinance or Law</xsl:text></xsl:template>
	<xsl:template name="STEPID41"><xsl:text>	-  HO-32 Unit Owners Coverage A Special Coverage</xsl:text></xsl:template>
	<xsl:template name="STEPID42"><xsl:text>	-  HO-455 Identity Fraud Expense Coverage</xsl:text></xsl:template>
	<xsl:template name="STEPID43"><xsl:text>	-  HO-315 Earthquake (Zone </xsl:text><xsl:value-of select="EARTHQUAKEZONE" /><xsl:text>)</xsl:text></xsl:template>
	<xsl:template name="STEPID44"><xsl:text>	-  HO-327 Water Back-Up and Sump Pump Overflow</xsl:text></xsl:template>
	<xsl:template name="STEPID45"><xsl:text>	-  HO-33 Condo-Unit Owners Rental to Others </xsl:text><xsl:value-of select="HO33" /> </xsl:template>
	<xsl:template name="STEPID46"><xsl:text>	-  HO-373 Contingent Workers Compensation</xsl:text></xsl:template>
	<xsl:template name="STEPID47"><xsl:text>	-  HO-287 Mine Subsidence Coverage</xsl:text></xsl:template>
	<xsl:template name="STEPID48"><xsl:text>	-  HO-9 Collapse From Sub-Surface Water</xsl:text></xsl:template>
	<xsl:template name="STEPID49"><xsl:text>	-  HO-490 Specific Structures Away From Premises</xsl:text></xsl:template>
	<xsl:template name="STEPID50"><xsl:text>	-  HO-489 Other Structures Repair Cost Coverage  (</xsl:text><xsl:value-of select="REPAIRCOSTADDITIONAL" /><xsl:text> increase)</xsl:text></xsl:template>
	<xsl:template name="STEPID51"><xsl:text>	-  Primary Residence</xsl:text></xsl:template>
	<xsl:template name="STEPID52"><xsl:text>	-  Residence Employees(</xsl:text><xsl:value-of select="RESIDENCE_EMP_NUMBER" /><xsl:text>)</xsl:text></xsl:template>
	<xsl:template name="STEPID53"><xsl:text>	-  Additional Premises Occupied By Insured (</xsl:text><xsl:value-of select="format-number(OCCUPIED_INSURED, '#')" /><xsl:text>)</xsl:text></xsl:template>
	<xsl:template name="STEPID54"><xsl:text>	-  HO-40 Additional Premises Rented to Others - Residence Premises (</xsl:text><xsl:value-of select="format-number(RESIDENCE_PREMISES,'#')"></xsl:value-of><xsl:text>)</xsl:text></xsl:template>
	<xsl:template name="STEPID55"><xsl:text>	-  HO-70 Additional Premises Rented - Other Location - 1 Family (</xsl:text><xsl:value-of select="format-number(OTHER_LOC_1FAMILY,'#')" /><xsl:text>)</xsl:text></xsl:template>
	<xsl:template name="STEPID56"><xsl:text>	-  HO-70 Additional Premises Rented - Other Location - 2 Family (</xsl:text><xsl:value-of select="format-number(OTHER_LOC_2FAMILY,'#')" /><xsl:text>)</xsl:text></xsl:template>
	<xsl:template name="STEPID57"><xsl:text>	-  HO-42 Incidental Office Private School or Studio On Premises</xsl:text></xsl:template>
	<xsl:template name="STEPID58"><xsl:text>	-      Business Located in an Other Structure</xsl:text></xsl:template> <!-- SKB-->
	<xsl:template name="STEPID59"><xsl:text>	-  HO-42 Incidental Office Private School or Studio Instruction Only</xsl:text></xsl:template>
	<xsl:template name="STEPID60"><xsl:text>	-  HO-200 Waterbed Liability</xsl:text></xsl:template>
	<xsl:template name="STEPID61"><xsl:text>	-  HO-43 Incidental Office Private School or Studio Off Premises</xsl:text></xsl:template>
	<xsl:template name="STEPID62"><xsl:text>	-  HO-71 Business Pursuits</xsl:text></xsl:template>
	<xsl:template name="STEPID63"><xsl:text>	-    Class A - (1 persons)</xsl:text></xsl:template>
	<xsl:template name="STEPID64"><xsl:text>	-    Class B - (1 persons)</xsl:text></xsl:template>
	<xsl:template name="STEPID65"><xsl:text>	-    Class C - (1 persons)</xsl:text></xsl:template>
	<xsl:template name="STEPID66"><xsl:text>	-    Class D - (1 persons)</xsl:text></xsl:template>
	<xsl:template name="STEPID67"><xsl:text>	-  HO-72 Farm Liability - Incidental Farming Residence Premises</xsl:text></xsl:template>
	<xsl:template name="STEPID68"><xsl:text>	-  HO-73 Farm Liability - Owned By Insured Rented to Others(</xsl:text><xsl:value-of select="format-number(OTH_LOC_OPR_OTHERS_HO73,'#')" /><xsl:text>)</xsl:text></xsl:template>
	<xsl:template name="STEPID69"><xsl:text>	-  HO-73 Farm Liability - Owned/Operated By Insured/Insured Employee(</xsl:text><xsl:value-of select="format-number(OTH_LOC_OPR_EMPL_HO73,'#')" /><xsl:text>)</xsl:text></xsl:template>
	<xsl:template name="STEPID70"><xsl:text>	-  HO-82 Personal Injury</xsl:text></xsl:template>
	<xsl:template name="STEPID71"><xsl:text>	-  Bicycles (HO-900)</xsl:text></xsl:template>
	<xsl:template name="STEPID72"><xsl:text>	-  Cameras (Non-Professional)</xsl:text></xsl:template>
	<xsl:template name="STEPID73"><xsl:text>	-  Cellular Phones (HO-900)</xsl:text></xsl:template>
	<xsl:template name="STEPID74"><xsl:text>	-  Furs</xsl:text></xsl:template>
	<xsl:template name="STEPID75"><xsl:text>	-  Golf Equipment</xsl:text></xsl:template>
	<xsl:template name="STEPID76"><xsl:text>	-  Guns</xsl:text></xsl:template>
	<xsl:template name="STEPID77"><xsl:text>	-  Jewelry</xsl:text></xsl:template>
	<xsl:template name="STEPID78"><xsl:text>	-  Musical Instruments(not professional)(HO-61)</xsl:text></xsl:template>
	<xsl:template name="STEPID79"><xsl:text>	-  Personal Computers - Desktop (HO-214)</xsl:text></xsl:template>
	<xsl:template name="STEPID80"><xsl:text>	-  Silver</xsl:text></xsl:template>
	<xsl:template name="STEPID81"><xsl:text>	-  Stamps</xsl:text></xsl:template>
	<xsl:template name="STEPID82"><xsl:text>	-  Rare Coins</xsl:text></xsl:template>
	<xsl:template name="STEPID83"><xsl:text>	-  Fine Arts - Without Breakage</xsl:text></xsl:template>
	<xsl:template name="STEPID84"><xsl:text>	-  Fine Arts - With Breakage</xsl:text></xsl:template>
	<xsl:template name="STEPID85"><xsl:text>	-  Handicap Electronics</xsl:text></xsl:template>
	<xsl:template name="STEPID86"><xsl:text>	-  Hearing Aids</xsl:text></xsl:template>
	<xsl:template name="STEPID87"><xsl:text>	-  Insulin Pumps</xsl:text></xsl:template>
	<xsl:template name="STEPID88"><xsl:text>	-  Mary Kay/Amway</xsl:text></xsl:template>
	<xsl:template name="STEPID89"><xsl:text>	-  Personal Computers - Laptop (HO-214)</xsl:text></xsl:template>
	<xsl:template name="STEPID90"><xsl:text>	-  Salesman Supplies</xsl:text></xsl:template>
	<xsl:template name="STEPID91"><xsl:text>	-  Scuba Diving Equipment</xsl:text></xsl:template>
	<xsl:template name="STEPID92"><xsl:text>	-  Snow Skis</xsl:text></xsl:template>
	<xsl:template name="STEPID93"><xsl:text>	-  Tack and Saddle</xsl:text></xsl:template>
	<xsl:template name="STEPID94"><xsl:text>	-  Tools (At Premises)</xsl:text></xsl:template>
	<xsl:template name="STEPID95"><xsl:text>	-  Tools (At Business Location)</xsl:text></xsl:template>
	<xsl:template name="STEPID96"><xsl:text>	-  Tractors</xsl:text></xsl:template>
	<xsl:template name="STEPID97"><xsl:text>	-  Train Collections</xsl:text></xsl:template>
	<xsl:template name="STEPID98"><xsl:text>	-  Wheelchairs/Scooters</xsl:text></xsl:template>
	<xsl:template name="STEPID99"><xsl:text>	-  Property Expense Fee</xsl:text></xsl:template>
	<xsl:template name="STEPID100"><xsl:text>	-   Additional Premium charged to meet the minimum premium ($</xsl:text><xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINIMUM_HOME_PREMIUM']/NODE[@ID ='MINIMUM_PREMIUM']/ATTRIBUTES/@MINIMUM_VALUE" /><xsl:text>)</xsl:text></xsl:template>
	<xsl:template name="STEPID101"><xsl:text>	-   Final Premium </xsl:text></xsl:template>
	<!-- COMBINED PERSONAL PROPERTY INCREASED LIMIT ON   (COVERAGE - C)-->
	<xsl:template name="COMBINEDPERSONALPROPERTY">
		<xsl:variable name="VAR1">
			<xsl:choose>
				<xsl:when test="PERSONALPROPERTYINCREASEDLIMITINCLUDE != ''">
					<xsl:value-of select="PERSONALPROPERTYINCREASEDLIMITINCLUDE" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:value-of select="PERSONALPROPERTYINCREASEDLIMITADDITIONAL" />
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:value-of select="REPLACEMENTCOSTFACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR4">
			<xsl:choose>
				<xsl:when test="REDUCTION_IN_COVERAGE_C ='N'">0</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="REDUCTION_IN_COVERAGE_C" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 or PRODUCTNAME = $POLICYTYPE_HO6">
				<xsl:value-of select="round($VAR3)" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME != $POLICYTYPE_HO4 or PRODUCTNAME != $POLICYTYPE_HO6">
				<xsl:value-of select="($VAR1+$VAR2)-$VAR4" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$VAR3" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- COMBINED ADDITIONAL LIVING EXPENSE  (COVERAGE - D) -->
	<xsl:template name="COMBINEDADDITIONALLIVINGEXPENSE">
		<xsl:variable name="VAR1">
			<xsl:value-of select="ADDITIONALLIVINGEXPENSEINCLUDE" />
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:value-of select="ADDITIONALLIVINGEXPENSEADDITIONAL" />
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:value-of select="REPLACEMENTCOSTFACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="PRODUCTNAME !=$POLICYTYPE_HO4 or PRODUCTNAME !=$POLICYTYPE_HO6">
				<xsl:value-of select="round($VAR1+$VAR2)" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="round($VAR1)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--									Template for Lables  (END)								  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--									Additional Coverage Template (START) 					  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADDITIONALCOVERAGE">
		<xsl:variable name="V1">
			<xsl:call-template name="BREEDDOG" />
		</xsl:variable>
		<xsl:variable name="V2">
			<xsl:call-template name="PREFERRED_PLUS_COVERAGE" />
		</xsl:variable>
		<!--xsl:variable name="V3">
			<xsl:call-template name="HO_34_REPLACEMENT_COST" />
		</xsl:variable-->
		<xsl:variable name="V4">
			<xsl:call-template name="SELECT_HO_34_COVERAGE" />
		</xsl:variable>
		<xsl:variable name="V5">
			<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY_COVERAGE" />
		</xsl:variable>
		<xsl:variable name="V6">
			<xsl:call-template name="HO_96_REPLACEMENT_COST_PERSONAL_PROPERTY" />
		</xsl:variable>
		<xsl:variable name="V7">
			<xsl:call-template name="HO_312_BUSINESS_PROPERTY_INCREASED_LIMITS" />
		</xsl:variable>
		<xsl:variable name="V8">
			<xsl:call-template name="HO_40_OTHER_STRUCTURE_RENTED_TO_OTHERS" />
		</xsl:variable>
		<xsl:variable name="V9">
			<xsl:call-template name="HO_42_OTHER_STRUCTURE_WITH_INCIDENTAL" />
		</xsl:variable>
		<xsl:variable name="V10">
			<xsl:call-template name="HO_53_INCREASED_CREDIT_CARD" />
		</xsl:variable>
		<xsl:variable name="V11">
			<xsl:call-template name="MONEY" />
		</xsl:variable>
		<xsl:variable name="V12">
			<xsl:call-template name="SECURITIES" />
		</xsl:variable>
		<xsl:variable name="V13">
			<xsl:call-template name="SILVERWARE_GOLDWARE" />
		</xsl:variable>
		<xsl:variable name="V14">
			<xsl:call-template name="FIREARMS" />
		</xsl:variable>
		<xsl:variable name="V15">
			<xsl:call-template name="ORDINANCE" />
		</xsl:variable>
		<xsl:variable name="V16">
			<xsl:call-template name="HO_455_IDENTITY_FRAUD" />
		</xsl:variable>
		<xsl:variable name="V17">
			<xsl:call-template name="HO_315_EARTHQUAKE" />
		</xsl:variable>
		<xsl:variable name="V18">
			<xsl:call-template name="HO_327_WATER_BACK_UP" />
		</xsl:variable>
		<xsl:variable name="V19">
			<xsl:call-template name="HO_287_MINE_SUBSIDENCE_COVERAGE" />
		</xsl:variable>
		<xsl:variable name="V20">
			<xsl:call-template name="HO_9_COLLAPSE_FROM_SUB_SURFACE_WATER" />
		</xsl:variable>
		<xsl:variable name="V21">
			<xsl:call-template name="HO_490_SPECIFIC_STRUCTURED" />
		</xsl:variable>
		<xsl:variable name="V22">
			<xsl:call-template name="PRIMARY_RESIDENCE" />
		</xsl:variable>
		<xsl:variable name="V23">
			<xsl:call-template name="RESIDENCE_EMPLOYEES" />
		</xsl:variable>
		<xsl:variable name="V24">
			<xsl:call-template name="ADDITIONAL_PREMISES" />
		</xsl:variable>
		<xsl:variable name="V25">
			<xsl:call-template name="ADDITIONAL_RESIDENCE_PREMISES" />
		</xsl:variable>
		<xsl:variable name="V26">
			<xsl:call-template name="ADDITIONAL_OTHER_LOCATION_PREMISES_FAMILY1" />
		</xsl:variable>
		<xsl:variable name="V27">
			<xsl:call-template name="ADDITIONAL_OTHER_LOCATION_PREMISES_FAMILY2" />
		</xsl:variable>
		<xsl:variable name="V28">
			<xsl:call-template name="HO_42_ON_PREMISES" />
		</xsl:variable>
		<xsl:variable name="V29">
			<xsl:call-template name="HO42_BUSINESS_OTHER_STRUCTURE_WITH_INCIDENTAL" />
		</xsl:variable>
		<xsl:variable name="V30">
			<xsl:call-template name="HO_42_INSTRUCTION_ONLY" />
		</xsl:variable>
		<xsl:variable name="V31">
			<xsl:call-template name="HO_43_OFF_PREMISES" />
		</xsl:variable>
		<xsl:variable name="V32">
			<xsl:call-template name="HO_71_CLASSA" />
		</xsl:variable>
		<xsl:variable name="V33">
			<xsl:call-template name="HO_71_CLASSB" />
		</xsl:variable>
		<xsl:variable name="V34">
			<xsl:call-template name="HO_71_CLASSC" />
		</xsl:variable>
		<xsl:variable name="V35">
			<xsl:call-template name="HO_71_CLASSD" />
		</xsl:variable>
		<xsl:variable name="V36">
			<xsl:call-template name="HO_72_INCIDENTAL_FARMING" />
		</xsl:variable>
		<xsl:variable name="V37">
			<xsl:call-template name="HO_73_OTH_LOC_OPR_EMPL" />
		</xsl:variable>
		<xsl:variable name="V38">
			<xsl:call-template name="HO_73_OTH_LOC_OPR_OTHERS" />
		</xsl:variable>
		<xsl:variable name="V39">
			<xsl:call-template name="HO_82_PERSONAL_INJURY" />
		</xsl:variable>
		<xsl:variable name="V40">
			<xsl:call-template name="HO_900_BICYCLES_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="V41">
			<xsl:call-template name="CAMERAS_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="V42">
			<xsl:call-template name="HO_900_CELLULAR_PHONES_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="V43">
			<xsl:call-template name="FURS_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="V44">
			<xsl:call-template name="GOLF_EQUIPMENT_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="V45">
			<xsl:call-template name="GUNS_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="V46">
			<xsl:call-template name="JEWELRY_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="V47">
			<xsl:call-template name="MUSICAL_NON_PROFESSIONAL_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="V48">
			<xsl:call-template name="PERSONAL_COMPUTERS_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="V49">
			<xsl:call-template name="SILVER_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="V50">
			<xsl:call-template name="STAMPS_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="V51">
			<xsl:call-template name="RARE_COINS_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="V52">
			<xsl:call-template name="FINE_ARTS_WITHOUT_BREAKAGE_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="V53">
			<xsl:call-template name="FINE_ARTS_BREAKAGE_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="V54">
			<xsl:call-template name="PROPFEE" />
		</xsl:variable>
		<xsl:variable name="V55">
			<xsl:call-template name="HO_200_WATERBED_LIABILITY" />
		</xsl:variable>
		<xsl:variable name="V56">
			<xsl:call-template name="HO_33_CONDO_UNIT" />
		</xsl:variable>
		<xsl:variable name="V57">
			<xsl:call-template name="HO50_PERSONAL_PROPERTY_AWAY_PREMISES" />
		</xsl:variable>
		<xsl:variable name="V58">
			<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
		</xsl:variable>
		<xsl:variable name="V59">
			<xsl:call-template name="HO_489_SPECIFIC_STRUCTURED" />
		</xsl:variable>
		<xsl:variable name="V60">
			<xsl:call-template name="HO_48_OTHER_STRUCTURE_INCREASED_LIMITS" />
		</xsl:variable>
		<xsl:variable name="V61">
			<xsl:call-template name="UNSCHEDULED_JEWELRY_ADDITIONAL" />
		</xsl:variable>
		<xsl:variable name="V62">
			<xsl:call-template name="HO32_UNIT_OWNER" />
		</xsl:variable>
		<xsl:variable name="V63">
			<xsl:call-template name="PERSONAL_COMPUTERS_LAPTOP" />
		</xsl:variable>
		<xsl:variable name="V64">
			<xsl:call-template name="HO35_LOSS_ASSESSMENT" />
		</xsl:variable>
		<xsl:variable name="V65">
			<xsl:choose>
				<xsl:when test="PRODUCTNAME='HO-6'">
					<xsl:call-template name="COVERAGE_D_PREMIUM" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="V66">
			<xsl:call-template name="WHEELCHAIR" />
		</xsl:variable>
		<xsl:variable name="V67">
			<xsl:call-template name="TRAIN_COLLECTION" />
		</xsl:variable>
		<xsl:variable name="V68">
			<xsl:call-template name="TOOLS_AT_BSNS_LOCN" />
		</xsl:variable>
		<xsl:variable name="V69">
			<xsl:call-template name="TRACTORS" />
		</xsl:variable>
		<xsl:variable name="V70">
			<xsl:call-template name="TOOLS_AT_PREMISIS" />
		</xsl:variable>
		<xsl:variable name="V71">
			<xsl:call-template name="TACK_SADDLE" />
		</xsl:variable>
		<xsl:variable name="V72">
			<xsl:call-template name="SNOW_SKY" />
		</xsl:variable>
		<xsl:variable name="V73">
			<xsl:call-template name="SCUBA_DIVING" />
		</xsl:variable>
		<xsl:variable name="V74">
			<xsl:call-template name="SALESMAN_SUPPLY" />
		</xsl:variable>
		<xsl:variable name="V75">
			<xsl:call-template name="MART_KAY_AMWAY" />
		</xsl:variable>
		<xsl:variable name="V76">
			<xsl:call-template name="INSULIN_PUMPS" />
		</xsl:variable>
		<xsl:variable name="V77">
			<xsl:call-template name="HEARING_AIDS" />
		</xsl:variable>
		<xsl:variable name="V78">
			<xsl:call-template name="HANDICAP_ELECTRONICS" />
		</xsl:variable>
		<xsl:value-of select="$V1+$V2+$V4+$V5+$V6+$V7+$V8+$V9+$V10+$V11+$V12+$V13+$V14+$V15+$V16+$V17+$V18+$V19+$V20+$V21+$V22+$V23+$V24+$V25+$V26+$V27+$V28+$V29+$V30+$V31+$V32+$V33+$V34+$V35+$V36+$V37+$V38+$V39+$V40+$V41+$V42+$V43+$V44+$V45+$V46+$V47+$V48+$V49+$V50+$V51+$V52+$V53+$V54+$V55+$V56+$V57+$V59+$V60+$V61+$V62+$V63+$V64+$V65+$V66+$V67+$V68+$V69+$V70+$V71+$V72+$V73+$V74+$V75+$V76+$V77+$V78" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--									Additional Coverage Template (END) 	    				  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--									Additional Amount Template (START) 	    				  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADDITIONAL_AMOUNT_COVERAGE">
		<xsl:call-template name="COVERAGE_A_PREMIUM_FOR_HO6" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--									Additional Amount Template (END) 	    				  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--									Final Primium Template (START)   						  -->
	<!-- ============================================================================================ -->
	<xsl:template name="FINALPRIMIUM">
		<xsl:variable name="VAR_ADDITIONAL_AMOUNT_COVERAGE">
			<xsl:call-template name="ADDITIONAL_AMOUNT_COVERAGE" />
		</xsl:variable>
		<xsl:variable name="VAR_CALL_ADJUSTEDBASE">
			<xsl:call-template name="CALL_ADJUSTEDBASE" />
		</xsl:variable>
		<xsl:variable name="VAR_ADDITIONALCOVERAGE">
			<xsl:call-template name="ADDITIONALCOVERAGE" />
		</xsl:variable>
		<!-- Minimum Premium component-->
		<xsl:variable name="VAR_MINIMUM_PREMIUM">
			<xsl:call-template name="MINIMUM_PREMIUM" />
		</xsl:variable>
		<xsl:value-of select="$VAR_ADDITIONAL_AMOUNT_COVERAGE + $VAR_CALL_ADJUSTEDBASE + $VAR_ADDITIONALCOVERAGE + $VAR_MINIMUM_PREMIUM" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--									Final Primium Template (END)   							  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--					Templates for MINIMUM PREMIUM (START)									  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="MINIMUM_PREMIUM">
		<!-- Fetch the difference between  the reqd minimum premium and the premium calculated -->
		<xsl:variable name="DIFFERENCE_AMOUNT">
			<xsl:call-template name="PREMIUM_DIFFERENCE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="format-number($DIFFERENCE_AMOUNT,'#.00') &gt; 0.00">
				<xsl:value-of select="$DIFFERENCE_AMOUNT" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
		<!--IF (<xsl:value-of select="$DIFFERENCE_AMOUNT"/>   &gt; 0.00)
		THEN
			<xsl:value-of select="$DIFFERENCE_AMOUNT"/>  
		ELSE
			0-->
	</xsl:template>
	<xsl:template name="PREMIUM_DIFFERENCE">
		<!-- Fetch the minimum premium required. -->
		<xsl:variable name="MINIMUM_FINAL_PREMIUM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINIMUM_HOME_PREMIUM']/NODE[@ID ='MINIMUM_PREMIUM']/ATTRIBUTES/@MINIMUM_VALUE" />
		<!-- Check the final premium value -->
		<xsl:variable name="P_TEMP_FINAL_PREMIUM">
			<!-- Adjusted Base -->
			<xsl:variable name="VAR1">
				<xsl:call-template name="CALL_ADJUSTEDBASE" />
			</xsl:variable>
			<!-- Adjusted Base -->
			<xsl:variable name="VAR3">
				<xsl:call-template name="COVERAGE_A_PREMIUM_FOR_HO6" />
			</xsl:variable>
			<!-- Other Coverages -->
			<xsl:variable name="VAR2">
				<xsl:call-template name="ADDITIONALCOVERAGE" />
			</xsl:variable>
			<xsl:value-of select="$VAR1 + $VAR2 + $VAR3" />
		</xsl:variable>
		<xsl:value-of select="$MINIMUM_FINAL_PREMIUM - $P_TEMP_FINAL_PREMIUM" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Templates for MINIMUM PREMIUM (END)											  -->
	<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN							  -->
	<!-- ============================================================================================ -->
	<!-- ###################################  START - MICHIGAN ###################################### -->
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR MICHIGAN(REGULAR) HO-2			
		1. Base (Find Base in Territory By Policy Form and Protection Class.) X
		2. Coverage A Factor (Tables based on Protection Class and Construction Frame or Masonry.) X
		3. Replacement Cost Factor X
		4. Insurance Score Discount Factor X
		5. Term Factor: 1.00 Annual or .50 Semi-Annual X
		6. Charge for Increased Coverage C or Coverage D (if any)
		7. Deductible Factor X
		8. Age of Home Factor X
		9. Protective Device Factor X
		10. HO-14 Dwelling Under Construction Credit (if applies then no: Age of Home, or Protective Device Credit) X
		11. Experience (Maturity or Age of Insured) Factor X
		12. Multiple Policy (Auto & Home) Factor X
		13. Valued Customer-Renewal Credit Factor X
		14. Surcharges
		15. Apply Section II Credits if any ($15 if Secondary and we have the Primary; or $2 for Secondary and No
		Primary).
		16. Add -Any Other Endorsement Option Charges.
		17. Add-Section II Liability Charges (if any).
		18. Add-Michigan Property Expense Recoupment Charge. - THIS WILL BE ADDED IN ADDITIONAL COVERAGE SECTION
						  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_REGULAR_MICHIGAN_HO2_HO3">
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="DEWLLING-MAIN" />
		</xsl:variable>
		<xsl:variable name="VAR_COVERAGE">
			<xsl:call-template name="C-VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR_REPLACEMENTCOST">
			<xsl:call-template name="REPLACEMENT_COST_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCESCORE">
			<xsl:call-template name="INSURANCE_SCORE_CREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_C">
			<xsl:call-template name="COVERAGE_C_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_D">
			<xsl:call-template name="COVERAGE_D_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_DFACTOR">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5 -->
		<xsl:variable name="VAR_AGEOFHOMEFACTOR">
			<xsl:variable name="AGEOFHOMEFACTOR">
				<xsl:call-template name="AGEOFHOME_FACTOR" />
			</xsl:variable>
			<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
			<xsl:variable name="AGEOFHOME_CREDIT_PERCENT" select="(1.00-$AGEOFHOMEFACTOR) * 100.00" />
			<xsl:variable name="VAR_MAXCREDIT_PERCENT" select="(1.00-$VAR_MAXCREDIT_FACTOR) * 100.00" />
			<xsl:choose>
				<xsl:when test="$AGEOFHOME_CREDIT_PERCENT &lt; $VAR_MAXCREDIT_PERCENT">
					<xsl:value-of select="$AGEOFHOMEFACTOR" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$VAR_MAXCREDIT_FACTOR" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_PROTECTIVE_DEVICE">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_DWELLING_UNDER_CONSTR">
			<xsl:call-template name="GET_DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_EXPERIENCE_DISCOUNT">
			<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_MULTIPOLICY_DISCOUNT">
			<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_VALUEDCUSTOMER">
			<xsl:call-template name="VALUEDCUSTOMER" />
		</xsl:variable>
		<!-- TILL HERE -->
		<xsl:variable name="VAR_PRIOR_LOSS">
			<xsl:call-template name="PRIOR_LOSS" />
		</xsl:variable>
		<xsl:variable name="VAR_WSTOVE">
			<xsl:call-template name="WSTOVE" />
		</xsl:variable>
		<xsl:variable name="VAR_SEASONALSECONDARYCREDIT">
			<xsl:call-template name="SEASONALSECONDARYCREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_MEDICAL_CHARGES">
			<xsl:call-template name="MEDICAL_CHARGES" />
		</xsl:variable>
		<xsl:variable name="VAR_LIABLITY_CHARGES">
			<xsl:call-template name="LIABLITY_CHARGES" />
		</xsl:variable>
		<!-- return value of adjusted base -->
		<xsl:variable name="FIRST_STEP" select="$VAR_BASE * $VAR_COVERAGE" />
		<xsl:variable name="VAR1" select="round(format-number($FIRST_STEP,'##.0000'))" />
		<xsl:variable name="SECOND_STEP" select="$VAR1 * $VAR_REPLACEMENTCOST" />
		<xsl:variable name="VAR2" select="round(format-number($SECOND_STEP,'##.0000'))" />
		<xsl:variable name="THIRD_STEP" select="$VAR2 * $VAR_INSURANCESCORE" />
		<xsl:variable name="VAR3" select="round(format-number($THIRD_STEP,'##.0000'))" />
		<xsl:variable name="FORTH_STEP" select="$VAR3 * $VAR_TERMFACTOR" />
		<xsl:variable name="VAR4" select="round($FORTH_STEP)" />
		<!--xsl:variable name="VAR_1" select="round(round(round(round($VAR_BASE * $VAR_COVERAGE)* $VAR_REPLACEMENTCOST)* $VAR_INSURANCESCORE)* $VAR_TERMFACTOR)" /-->
		<xsl:variable name="VAR_2" select="$VAR4 + $VAR_INCREASED_COV_C + $VAR_INCREASED_COV_D" />
		<xsl:variable name="FIFTH_STEP" select="$VAR_2 * $VAR_DFACTOR" />
		<xsl:variable name="VAR5" select="round($FIFTH_STEP)" />
		<xsl:variable name="SIX_STEP" select="$VAR5 * $VAR_AGEOFHOMEFACTOR" />
		<xsl:variable name="VAR6" select="round(format-number($SIX_STEP,'##.0000'))" />
		<xsl:variable name="SEVENTH_STEP" select="$VAR6 * $VAR_PROTECTIVE_DEVICE" />
		<xsl:variable name="VAR7" select="round(format-number($SEVENTH_STEP,'##.0000'))" />
		<xsl:variable name="EIGHT_STEP" select="$VAR7 * $VAR_DWELLING_UNDER_CONSTR" />
		<xsl:variable name="VAR8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
		<xsl:variable name="NINTH_STEP" select="$VAR8 * $VAR_EXPERIENCE_DISCOUNT" />
		<xsl:variable name="VAR9" select="round(format-number($NINTH_STEP,'##.0000'))" />
		<xsl:variable name="TENTH_STEP" select="$VAR9 * $VAR_MULTIPOLICY_DISCOUNT" />
		<xsl:variable name="VAR10" select="round(format-number($TENTH_STEP,'##.0000'))" />
		<xsl:variable name="ELEVENTH_STEP" select="$VAR10 * $VAR_VALUEDCUSTOMER" />
		<xsl:variable name="VAR11" select="round(format-number($ELEVENTH_STEP,'##.0000'))" />
		<xsl:variable name="TWLVENTH_STEP" select="$VAR11 * $VAR_PRIOR_LOSS" />
		<xsl:variable name="VAR12" select="round(format-number($TWLVENTH_STEP,'##.0000'))" />
		<xsl:variable name="THERTEENTH_STEP" select="$VAR12 * $VAR_WSTOVE" />
		<xsl:variable name="VAR13" select="round($THERTEENTH_STEP)" />
		<!--xsl:variable name="VAR_3" select="round(round(round(round(round(round(round(round(round($VAR_2 * $VAR_DFACTOR)* $VAR_AGEOFHOMEFACTOR)* $VAR_PROTECTIVE_DEVICE)* $VAR_DWELLING_UNDER_CONSTR)* $VAR_EXPERIENCE_DISCOUNT)* $VAR_MULTIPOLICY_DISCOUNT)* $VAR_VALUEDCUSTOMER)* $VAR_PRIOR_LOSS)* $VAR_WSTOVE)" /-->
		<xsl:value-of select="($VAR13 -$VAR_SEASONALSECONDARYCREDIT) + $VAR_MEDICAL_CHARGES + $VAR_LIABLITY_CHARGES" />
	</xsl:template>
	<!--  **********************************************************************  -->
	<!-- Wood StoveLAbel Display -->
	<xsl:template name="WOODSTOVE_LABEL_DISPLAY">
((((((((((<xsl:call-template name="DEWLLING-MAIN"></xsl:call-template> )*
			(<xsl:call-template name="C-VALUE"></xsl:call-template>)*
			(<xsl:call-template name="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name="TERM_FACTOR"></xsl:call-template>)
			)
			+
			(
				(<xsl:call-template name="COVERAGE_C_PREMIUM" /> )
				+ (<xsl:call-template name="COVERAGE_D_PREMIUM" />)
			)
		    
	   )
	   *
	   (<xsl:call-template name="DFACTOR" />))*
	   (<xsl:call-template name="AGEOFHOME_FACTOR" />))*
	   (<xsl:call-template name="PROTECTIVEDEVICE" />))*
	   (<xsl:call-template name="EXPERIENCEFACTOR" />))*
	   (<xsl:call-template name="MULTIPOLICY_FACTOR" />))*
	   (<xsl:call-template name="VALUEDCUSTOMER" />))*
	   (<xsl:call-template name="PRIOR_LOSS" />))*
	   (<xsl:call-template name="WSTOVE" />)
	   
	    
</xsl:template>
	<!-- ============================================================================================ -->
	<!--					ADJUSTEDBASE FOR MICHIGAN(REPAIR COST) HO-2	and HO-3					  -->
	<!--    1. Base (Find Base in Territory By Policy Form and Protection Class.) X
		2. Coverage A Factor (Tables based on Protection Class and Construction Frame or Masonry.) X
		3. Repair Cost Factor 1.23 X
		4. Insurance Score Discount Factor X
		5. Term Factor: 1.00 Annual or .50 Semi-Annual X
		6. Charge for Increased Coverage C or Coverage D (if any)
		7. Deductible Factor X
		8. Age of Home Factor X
		9. Protective Device Factor X
		10. HO-14 Dwelling Under Construction Credit (if applies then no: Age of Home, or Protective Device Credit) X
		11. Experience (Maturity or Age of Insured) Factor X
		12. Multiple Policy (Auto & Home) Factor X
		13. Valued Customer-Renewal Credit Factor X
		14. Surcharges
		15. Apply Section II Credits if any ($15 if Secondary and we have the Primary; or $2 for Secondary and No
		Primary).
		16. Add -Any Other Endorsement Option Charges.
		17. Add-Section II Liability Charges (if any).
		18. Add-Michigan Property Expense Recoupment Charge. - THIS WILL BE ADDED IN ADDITIONAL COVERAGE SECTION
		
-->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_REPAIRCOST_MICHIGAN_HO2_HO3">
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="DEWLLING-MAIN" />
		</xsl:variable>
		<xsl:variable name="VAR_COVERAGE">
			<xsl:call-template name="C-VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR_REPAIRCOST">
			<xsl:call-template name="REPAIR_COST_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCESCORE">
			<xsl:call-template name="INSURANCE_SCORE_CREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_C">
			<xsl:call-template name="COVERAGE_C_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_D">
			<xsl:call-template name="COVERAGE_D_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_DFACTOR">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5 -->
		<xsl:variable name="VAR_AGEOFHOMEFACTOR">
			<xsl:variable name="AGEOFHOMEFACTOR">
				<xsl:call-template name="AGEOFHOME_FACTOR" />
			</xsl:variable>
			<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
			<xsl:variable name="AGEOFHOME_CREDIT_PERCENT" select="(1.00-$AGEOFHOMEFACTOR) * 100.00" />
			<xsl:variable name="VAR_MAXCREDIT_PERCENT" select="(1.00-$VAR_MAXCREDIT_FACTOR) * 100.00" />
			<xsl:choose>
				<xsl:when test="$AGEOFHOME_CREDIT_PERCENT &lt; $VAR_MAXCREDIT_PERCENT">
					<xsl:value-of select="$AGEOFHOMEFACTOR" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$VAR_MAXCREDIT_FACTOR" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_PROTECTIVE_DEVICE">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_DWELLING_UNDER_CONSTR">
			<xsl:call-template name="GET_DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_EXPERIENCE_DISCOUNT">
			<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_MULTIPOLICY_DISCOUNT">
			<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_VALUEDCUSTOMER">
			<xsl:call-template name="VALUEDCUSTOMER" />
		</xsl:variable>
		<!-- TILL HERE -->
		<xsl:variable name="VAR_PRIOR_LOSS">
			<xsl:call-template name="PRIOR_LOSS" />
		</xsl:variable>
		<xsl:variable name="VAR_WSTOVE">
			<xsl:call-template name="WSTOVE" />
		</xsl:variable>
		<xsl:variable name="VAR_SEASONALSECONDARYCREDIT">
			<xsl:call-template name="SEASONALSECONDARYCREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_MEDICAL_CHARGES">
			<xsl:call-template name="MEDICAL_CHARGES" />
		</xsl:variable>
		<xsl:variable name="VAR_LIABLITY_CHARGES">
			<xsl:call-template name="LIABLITY_CHARGES" />
		</xsl:variable>
		<!-- return value of adjusted base -->
		<xsl:variable name="FIRST_STEP" select="$VAR_BASE * $VAR_COVERAGE" />
		<xsl:variable name="VAR1" select="round(format-number($FIRST_STEP,'##.0000'))" />
		<xsl:variable name="SECOND_STEP" select="$VAR1 * $VAR_REPAIRCOST" />
		<xsl:variable name="VAR2" select="round(format-number($SECOND_STEP,'##.0000'))" />
		<xsl:variable name="THIRD_STEP" select="$VAR2 * $VAR_INSURANCESCORE" />
		<xsl:variable name="VAR3" select="round(format-number($THIRD_STEP,'##.0000'))" />
		<xsl:variable name="FORTH_STEP" select="$VAR3 * $VAR_TERMFACTOR" />
		<xsl:variable name="VAR4" select="round($FORTH_STEP)" />
		<!--xsl:variable name="VAR_1" select="round(round(round(round($VAR_BASE * $VAR_COVERAGE)* $VAR_REPAIRCOST)* $VAR_INSURANCESCORE)* $VAR_TERMFACTOR)" /-->
		<xsl:variable name="VAR_2" select="$VAR4 + $VAR_INCREASED_COV_C + $VAR_INCREASED_COV_D" />
		<xsl:variable name="FIFTH_STEP" select="$VAR_2 * $VAR_DFACTOR" />
		<xsl:variable name="VAR5" select="round($FIFTH_STEP)" />
		<xsl:variable name="SIX_STEP" select="$VAR5 * $VAR_AGEOFHOMEFACTOR" />
		<xsl:variable name="VAR6" select="round(format-number($SIX_STEP,'##.0000'))" />
		<xsl:variable name="SEVENTH_STEP" select="$VAR6 * $VAR_PROTECTIVE_DEVICE" />
		<xsl:variable name="VAR7" select="round(format-number($SEVENTH_STEP,'##.0000'))" />
		<xsl:variable name="EIGHT_STEP" select="$VAR7 * $VAR_DWELLING_UNDER_CONSTR" />
		<xsl:variable name="VAR8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
		<xsl:variable name="NINTH_STEP" select="$VAR8 * $VAR_EXPERIENCE_DISCOUNT" />
		<xsl:variable name="VAR9" select="round(format-number($NINTH_STEP,'##.0000'))" />
		<xsl:variable name="TENTH_STEP" select="$VAR9 * $VAR_MULTIPOLICY_DISCOUNT" />
		<xsl:variable name="VAR10" select="round(format-number($TENTH_STEP,'##.0000'))" />
		<xsl:variable name="ELEVENTH_STEP" select="$VAR10 * $VAR_VALUEDCUSTOMER" />
		<xsl:variable name="VAR11" select="round(format-number($ELEVENTH_STEP,'##.0000'))" />
		<xsl:variable name="TWLVENTH_STEP" select="$VAR11 * $VAR_PRIOR_LOSS" />
		<xsl:variable name="VAR12" select="round(format-number($TWLVENTH_STEP,'##.0000'))" />
		<xsl:variable name="THERTEENTH_STEP" select="$VAR12 * $VAR_WSTOVE" />
		<xsl:variable name="VAR13" select="round($THERTEENTH_STEP)" />
		<!--xsl:variable name="VAR_3" select="round(round(round(round(round(round(round(round(round($VAR_2 * $VAR_DFACTOR)* $VAR_AGEOFHOMEFACTOR)* $VAR_PROTECTIVE_DEVICE)* $VAR_DWELLING_UNDER_CONSTR)* $VAR_EXPERIENCE_DISCOUNT)* $VAR_MULTIPOLICY_DISCOUNT)* $VAR_VALUEDCUSTOMER)* $VAR_PRIOR_LOSS)* $VAR_WSTOVE)" /-->
		<xsl:value-of select="($VAR13 -$VAR_SEASONALSECONDARYCREDIT) + $VAR_MEDICAL_CHARGES + $VAR_LIABLITY_CHARGES" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR MICHIGAN(PREMIER) HO-3							  -->
	<!-- 1. Base (Find Base in Territory By Policy Form and Protection Class.) X
	2. Coverage A Factor X (Minimum is $175,000) X
	3. 1.00 if insured at 100% to Replacement Cost. (Premier must be at least 100% or use Regular program.) X
	4. Insurance Score Discount Factor X
	5. Term Factor: 1.00 Annual or .50 Semi-Annual X
	6. Charge for Increased Coverage C or Coverage D (if any)
	7. Deductible Factor X
	8. Multiple Policy (Auto & Home) Factor X
	9. Age of Home Factor X
	10. Protective Device Factor X
	11. HO-14 Dwelling Under Construction Credit (if applies then no: Age of Home, or Protective Device Credit) X
	12. Experience (Maturity or Age of Insured) Factor X
	13. Valued Customer-Renewal Credit Factor X
	14. Surcharges
	15. Apply Section II Credits if any ($15 if Secondary and we have the Primary; or $2 for Secondary and No
	Primary).
	16. Add-Any Other Endorsement Option Charges.
	17. Add-Section II Liability Charges (if any).
	18. Add-Michigan Property Expense Recoupment Charge.  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_PREMIER_MICHIGAN_HO3">
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="DEWLLING-MAIN" />
		</xsl:variable>
		<xsl:variable name="VAR_COVERAGE">
			<xsl:call-template name="C-VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR_REPLACEMENTCOST">
			<xsl:call-template name="REPLACEMENT_COST_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCESCORE">
			<xsl:call-template name="INSURANCE_SCORE_CREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_C">
			<xsl:call-template name="COVERAGE_C_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_D">
			<xsl:call-template name="COVERAGE_D_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_DFACTOR">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5 -->
		<xsl:variable name="VAR_AGEOFHOMEFACTOR">
			<xsl:variable name="AGEOFHOMEFACTOR">
				<xsl:call-template name="AGEOFHOME_FACTOR" />
			</xsl:variable>
			<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
			<xsl:variable name="AGEOFHOME_CREDIT_PERCENT" select="(1.00-$AGEOFHOMEFACTOR) * 100.00" />
			<xsl:variable name="VAR_MAXCREDIT_PERCENT" select="(1.00-$VAR_MAXCREDIT_FACTOR) * 100.00" />
			<xsl:choose>
				<xsl:when test="$AGEOFHOME_CREDIT_PERCENT &lt; $VAR_MAXCREDIT_PERCENT">
					<xsl:value-of select="$AGEOFHOMEFACTOR" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$VAR_MAXCREDIT_FACTOR" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_PROTECTIVE_DEVICE">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_DWELLING_UNDER_CONSTR">
			<xsl:call-template name="GET_DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_EXPERIENCE_DISCOUNT">
			<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_MULTIPOLICY_DISCOUNT">
			<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_VALUEDCUSTOMER">
			<xsl:call-template name="VALUEDCUSTOMER" />
		</xsl:variable>
		<!-- TILL HERE -->
		<xsl:variable name="VAR_PRIOR_LOSS">
			<xsl:call-template name="PRIOR_LOSS" />
		</xsl:variable>
		<xsl:variable name="VAR_WSTOVE">
			<xsl:call-template name="WSTOVE" />
		</xsl:variable>
		<xsl:variable name="VAR_SEASONALSECONDARYCREDIT">
			<xsl:call-template name="SEASONALSECONDARYCREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_MEDICAL_CHARGES">
			<xsl:call-template name="MEDICAL_CHARGES" />
		</xsl:variable>
		<xsl:variable name="VAR_LIABLITY_CHARGES">
			<xsl:call-template name="LIABLITY_CHARGES" />
		</xsl:variable>
		<!-- return value of adjusted base -->
		<xsl:variable name="FIRST_STEP" select="$VAR_BASE * $VAR_COVERAGE" />
		<xsl:variable name="VAR1" select="round(format-number($FIRST_STEP,'##.0000'))" />
		<xsl:variable name="SECOND_STEP" select="$VAR1 * $VAR_REPLACEMENTCOST" />
		<xsl:variable name="VAR2" select="round(format-number($SECOND_STEP,'##.0000'))" />
		<xsl:variable name="THIRD_STEP" select="$VAR2 * $VAR_INSURANCESCORE" />
		<xsl:variable name="VAR3" select="round(format-number($THIRD_STEP,'##.0000'))" />
		<xsl:variable name="FORTH_STEP" select="$VAR3 * $VAR_TERMFACTOR" />
		<xsl:variable name="VAR4" select="round($FORTH_STEP)" />
		<!--xsl:variable name="VAR_1" select="round(round(round(round($VAR_BASE * $VAR_COVERAGE)* $VAR_REPLACEMENTCOST)* $VAR_INSURANCESCORE)* $VAR_TERMFACTOR)" /-->
		<xsl:variable name="VAR_2" select="$VAR4 + $VAR_INCREASED_COV_C + $VAR_INCREASED_COV_D" />
		<xsl:variable name="FIFTH_STEP" select="$VAR_2 * $VAR_DFACTOR" />
		<xsl:variable name="VAR5" select="round($FIFTH_STEP)" />
		<xsl:variable name="SIX_STEP" select="$VAR5 * $VAR_AGEOFHOMEFACTOR" />
		<xsl:variable name="VAR6" select="round(format-number($SIX_STEP,'##.0000'))" />
		<xsl:variable name="SEVENTH_STEP" select="$VAR6 * $VAR_PROTECTIVE_DEVICE" />
		<xsl:variable name="VAR7" select="round(format-number($SEVENTH_STEP,'##.0000'))" />
		<xsl:variable name="EIGHT_STEP" select="$VAR7 * $VAR_DWELLING_UNDER_CONSTR" />
		<xsl:variable name="VAR8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
		<xsl:variable name="NINTH_STEP" select="$VAR8 * $VAR_EXPERIENCE_DISCOUNT" />
		<xsl:variable name="VAR9" select="round(format-number($NINTH_STEP,'##.0000'))" />
		<xsl:variable name="TENTH_STEP" select="$VAR9 * $VAR_MULTIPOLICY_DISCOUNT" />
		<xsl:variable name="VAR10" select="round(format-number($TENTH_STEP,'##.0000'))" />
		<xsl:variable name="ELEVENTH_STEP" select="$VAR10 * $VAR_VALUEDCUSTOMER" />
		<xsl:variable name="VAR11" select="round(format-number($ELEVENTH_STEP,'##.0000'))" />
		<xsl:variable name="TWLVENTH_STEP" select="$VAR11 * $VAR_PRIOR_LOSS" />
		<xsl:variable name="VAR12" select="round(format-number($TWLVENTH_STEP,'##.0000'))" />
		<xsl:variable name="THERTEENTH_STEP" select="$VAR12 * $VAR_WSTOVE" />
		<xsl:variable name="VAR13" select="round($THERTEENTH_STEP)" />
		<!--xsl:variable name="VAR_3" select="round(round(round(round(round(round(round(round(round($VAR_2 * $VAR_DFACTOR)* $VAR_AGEOFHOMEFACTOR)* $VAR_PROTECTIVE_DEVICE)* $VAR_DWELLING_UNDER_CONSTR)* $VAR_EXPERIENCE_DISCOUNT)* $VAR_MULTIPOLICY_DISCOUNT)* $VAR_VALUEDCUSTOMER)* $VAR_PRIOR_LOSS)* $VAR_WSTOVE)" /-->
		<xsl:value-of select="($VAR13 -$VAR_SEASONALSECONDARYCREDIT) + $VAR_MEDICAL_CHARGES + $VAR_LIABLITY_CHARGES" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR MICHIGAN(REGULAR) HO-4 OR HO-6					  -->
	<!-- 1. Base (Find Base in Territory By Policy Form 4 or 6, CO-I or CO-II, and Protection Class.) X
	2. Coverage C Factor (Tables based on whether is HO-4 with minimum $10,000 or HO-6 minimum $15,000.) X
	
	4. Insurance Score Discount Factor X
	5. Term Factor: 1.00 Annual or .50 Semi-Annual X
	6. Deductible Factor X
	7. Protective Device Factor X
	8. Non-Smoker or Smoker Factor X
	9. Experience (Maturity or Age of Insured) Factor X
	10. Multiple Policy (Auto & Home) Factor (HO-4 must be minimum $15,000 Coverage C to get credit. HO-6
	no minimum.) X
	11. Valued Customer-Renewal Credit X
	
	13. Surcharges
	3. DELUXE Endorsement (if NO then X 1.00, if YES minimum Coverage C must be $25,000 and then take rate
	X 1.15 Min. $25.)
	12. Coverage A is 10% of Coverage C ($2,000 minimum), if Coverage A increased and there is no HO-66
	endorsement then rate is $2 per $1,000 increase. If an HO-66 Deluxe endorsement then charge is $2.20
	per $1,000 increased above automatic amount.
	14. Apply Section II Credits if any ($9 if Secondary and we have the Primary; or $2 for Secondary and No
	Primary).
	15. Add - Any Other Endorsement Option Charges.
	16. Add-Section II Liability Charges (if any).
	17. Add-Michigan Property Expense Recoupment Fee. -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_REGULAR_MICHIGAN_HO4_HO6">
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="DEWLLING-MAIN" />
		</xsl:variable>
		<xsl:variable name="VAR_COVERAGE">
			<xsl:call-template name="C-VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR_DELUXE_ENDORSEMENT">
			<xsl:call-template name="DELUXE_ENDORSEMENT" />
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCESCORE">
			<xsl:call-template name="INSURANCE_SCORE_CREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_DFACTOR">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5 -->
		<xsl:variable name="VAR_AGEOFHOMEFACTOR">
			<xsl:variable name="AGEOFHOMEFACTOR">
				<xsl:call-template name="AGEOFHOME_FACTOR" />
			</xsl:variable>
			<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
			<xsl:variable name="AGEOFHOME_CREDIT_PERCENT" select="(1.00-$AGEOFHOMEFACTOR) * 100.00" />
			<xsl:variable name="VAR_MAXCREDIT_PERCENT" select="(1.00-$VAR_MAXCREDIT_FACTOR) * 100.00" />
			<xsl:choose>
				<xsl:when test="$AGEOFHOME_CREDIT_PERCENT &lt; $VAR_MAXCREDIT_PERCENT">
					<xsl:value-of select="$AGEOFHOMEFACTOR" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$VAR_MAXCREDIT_FACTOR" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_PROTECTIVE_DEVICE">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_DWELLING_UNDER_CONSTR">
			<xsl:call-template name="GET_DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_EXPERIENCE_DISCOUNT">
			<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_MULTIPOLICY_DISCOUNT">
			<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_VALUEDCUSTOMER">
			<xsl:call-template name="VALUEDCUSTOMER" />
		</xsl:variable>
		<!-- TILL HERE   -->
		<xsl:variable name="VAR_PRIOR_LOSS">
			<xsl:call-template name="PRIOR_LOSS" />
		</xsl:variable>
		<xsl:variable name="VAR_WSTOVE">
			<xsl:call-template name="WSTOVE" />
		</xsl:variable>
		<xsl:variable name="VAR_SEASONALSECONDARYCREDIT">
			<xsl:call-template name="SEASONALSECONDARYCREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_MEDICAL_CHARGES">
			<xsl:call-template name="MEDICAL_CHARGES" />
		</xsl:variable>
		<xsl:variable name="VAR_LIABLITY_CHARGES">
			<xsl:call-template name="LIABLITY_CHARGES" />
		</xsl:variable>
		<!-- return value of adjusted base-->
		<xsl:variable name="FIRST_STEP" select="$VAR_BASE * $VAR_COVERAGE" />
		<xsl:variable name="VAR1" select="round(format-number($FIRST_STEP,'##.0000'))" />
		<xsl:variable name="SECOND_STEP" select="$VAR1 * $VAR_DELUXE_ENDORSEMENT" />
		<xsl:variable name="VAR2" select="round(format-number($SECOND_STEP,'##.0000'))" />
		<xsl:variable name="THIRD_STEP" select="$VAR2 * $VAR_TERMFACTOR" />
		<xsl:variable name="VAR4" select="round(format-number($THIRD_STEP,'##.0000'))" />
		<xsl:variable name="FIFTH_STEP" select="$VAR4 * $VAR_DFACTOR" />
		<xsl:variable name="VAR5" select="round($FIFTH_STEP)" />
		<!--xsl:variable name="VAR_1" select="round(round(round(round($VAR_BASE * $VAR_COVERAGE)*$VAR_DELUXE_ENDORSEMENT)* $VAR_TERMFACTOR)* $VAR_DFACTOR)" /-->
		<xsl:variable name="SIX_STEP" select="$VAR5 * $VAR_INSURANCESCORE" />
		<xsl:variable name="VAR6" select="round(format-number($SIX_STEP,'##.0000'))" />
		<xsl:variable name="SEVENTH_STEP" select="$VAR6 * $VAR_AGEOFHOMEFACTOR" />
		<xsl:variable name="VAR7" select="round(format-number($SEVENTH_STEP,'##.0000'))" />
		<xsl:variable name="EIGHT_STEP" select="$VAR7 * $VAR_PROTECTIVE_DEVICE" />
		<xsl:variable name="VAR8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
		<xsl:variable name="NINTH_STEP" select="$VAR8 * $VAR_DWELLING_UNDER_CONSTR" />
		<xsl:variable name="VAR9" select="round(format-number($NINTH_STEP,'##.0000'))" />
		<xsl:variable name="TENTH_STEP" select="$VAR9 * $VAR_EXPERIENCE_DISCOUNT" />
		<xsl:variable name="VAR10" select="round(format-number($TENTH_STEP,'##.0000'))" />
		<xsl:variable name="ELEVENTH_STEP" select="$VAR10 * $VAR_MULTIPOLICY_DISCOUNT" />
		<xsl:variable name="VAR11" select="round(format-number($ELEVENTH_STEP,'##.0000'))" />
		<xsl:variable name="TWLVENTH_STEP" select="$VAR11 * $VAR_VALUEDCUSTOMER" />
		<xsl:variable name="VAR12" select="round(format-number($TWLVENTH_STEP,'##.0000'))" />
		<xsl:variable name="THERTEENTH_STEP" select="$VAR12 * $VAR_PRIOR_LOSS" />
		<xsl:variable name="VAR13" select="round(format-number($THERTEENTH_STEP,'##.0000'))" />
		<xsl:variable name="FORTEENTH_STEP" select="$VAR13 * $VAR_WSTOVE" />
		<xsl:variable name="VAR14" select="round($FORTEENTH_STEP)" />
		<!--xsl:variable name="VAR_2" select="round(round(round(round(round(round(round(round(round($VAR_1 * $VAR_INSURANCESCORE)* $VAR_AGEOFHOMEFACTOR)* $VAR_PROTECTIVE_DEVICE)* $VAR_DWELLING_UNDER_CONSTR)* $VAR_EXPERIENCE_DISCOUNT)* $VAR_MULTIPOLICY_DISCOUNT)* $VAR_VALUEDCUSTOMER)* $VAR_PRIOR_LOSS)* $VAR_WSTOVE)" /-->
		<xsl:value-of select="(($VAR14  - $VAR_SEASONALSECONDARYCREDIT) + $VAR_MEDICAL_CHARGES + $VAR_LIABLITY_CHARGES )" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR MICHIGAN(REGULER) HO-5					  -->
	<!-- 1. Base (Find Base in Territory By Policy Form and Protection Class.) X
     2. Coverage A Factor (Note Minimum Coverage A is $125,000.) X
	3. 1.00 if insured to Replacement Cost (HO-5 Must Be at 100% Coverage A to Replacement Cost to be
	eligible.) X
	4. Term Factor: 1.00 Annual or .50 Semi-Annual X
	5. Deductible Factor X
		6. Add HO-34 Replacement Cost Personal Property Endorsement = 10% Minimum $35.	| TO BE ADDED HERE
		7. Add HO-11 Expanded Replacement Coverage for Building = $15 Flat Charge		|
		8. Add HO-23 Premier V.I.P. End = $20										   _|
	9. Insurance Score Discount Factor X
	10. Age of Home Factor X
	11. Protective Device Factor X
	12. HO-14 Dwelling Under Construction Credit (if applies then no: Age of Home, or Protective Device Credit) X
	13. Experience (Maturity or Age of Insured) Factor X
	14. Multiple Policy (Auto & Home) Factor X
	15. Valued Customer-Renewal Credit Factor X
	16. Surcharges
	17. Add Charge for Increased Coverage C or Coverage D (if any).						
		
	18. Apply Section II Credits if any ($15 if Secondary and we have the Primary; or $2 for Secondary and No
	Primary). -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_REGULAR_MICHIGAN_HO5">
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="DEWLLING-MAIN" />
		</xsl:variable>
		<xsl:variable name="VAR_COVERAGE">
			<xsl:call-template name="C-VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR_REPLACEMENTCOST">
			<xsl:call-template name="REPLACEMENT_COST_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCESCORE">
			<xsl:call-template name="INSURANCE_SCORE_CREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_C">
			<xsl:call-template name="COVERAGE_C_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_D">
			<xsl:call-template name="COVERAGE_D_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_DFACTOR">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_HO_34_REPLACEMENT_COST">
			<xsl:call-template name="HO_34_REPLACEMENT_COST" />
		</xsl:variable>
		<xsl:variable name="VAR_HO_20_PREFERRED_PLUS_OR_HO_21_VIP">
			<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
		</xsl:variable>
		<xsl:variable name="VAR_HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY">
			<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY" />
		</xsl:variable>
		<!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5 -->
		<xsl:variable name="VAR_AGEOFHOMEFACTOR">
			<xsl:variable name="AGEOFHOMEFACTOR">
				<xsl:call-template name="AGEOFHOME_FACTOR" />
			</xsl:variable>
			<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
			<xsl:variable name="AGEOFHOME_CREDIT_PERCENT" select="(1.00-$AGEOFHOMEFACTOR) * 100.00" />
			<xsl:variable name="VAR_MAXCREDIT_PERCENT" select="(1.00-$VAR_MAXCREDIT_FACTOR) * 100.00" />
			<xsl:choose>
				<xsl:when test="$AGEOFHOME_CREDIT_PERCENT &lt; $VAR_MAXCREDIT_PERCENT">
					<xsl:value-of select="$AGEOFHOMEFACTOR" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$VAR_MAXCREDIT_FACTOR" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_PROTECTIVE_DEVICE">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_DWELLING_UNDER_CONSTR">
			<xsl:call-template name="GET_DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_EXPERIENCE_DISCOUNT">
			<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_MULTIPOLICY_DISCOUNT">
			<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_VALUEDCUSTOMER">
			<xsl:call-template name="VALUEDCUSTOMER" />
		</xsl:variable>
		<!-- TILL HERE -->
		<xsl:variable name="VAR_PRIOR_LOSS">
			<xsl:call-template name="PRIOR_LOSS" />
		</xsl:variable>
		<xsl:variable name="VAR_WSTOVE">
			<xsl:call-template name="WSTOVE" />
		</xsl:variable>
		<xsl:variable name="VAR_SEASONALSECONDARYCREDIT">
			<xsl:call-template name="SEASONALSECONDARYCREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_MEDICAL_CHARGES">
			<xsl:call-template name="MEDICAL_CHARGES" />
		</xsl:variable>
		<xsl:variable name="VAR_LIABLITY_CHARGES">
			<xsl:call-template name="LIABLITY_CHARGES" />
		</xsl:variable>
		<!-- return value of adjusted base -->
		<xsl:variable name="FIRST_STEP" select="$VAR_BASE * $VAR_COVERAGE" />
		<xsl:variable name="VAR1" select="round(format-number($FIRST_STEP,'##.0000'))" />
		<xsl:variable name="SECOND_STEP" select="$VAR1 * $VAR_REPLACEMENTCOST" />
		<xsl:variable name="VAR2" select="round(format-number($SECOND_STEP,'##.0000'))" />
		<xsl:variable name="THIRD_STEP" select="$VAR2 * $VAR_TERMFACTOR" />
		<xsl:variable name="VAR4" select="round(format-number($THIRD_STEP,'##.0000'))" />
		<xsl:variable name="FIFTH_STEP" select="$VAR4 * $VAR_DFACTOR" />
		<xsl:variable name="VAR5" select="round($FIFTH_STEP)" />
		<!--xsl:variable name="VAR_1" select="round(round(round(round($VAR_BASE * $VAR_COVERAGE)* $VAR_REPLACEMENTCOST)* $VAR_TERMFACTOR)* $VAR_DFACTOR)" /-->
		<xsl:variable name="SIX_STEP" select="$VAR5 + $VAR_HO_34_REPLACEMENT_COST" />
		<xsl:variable name="VAR6" select="round(format-number($SIX_STEP,'##.0000'))" />
		<xsl:variable name="SEVENTH_STEP" select="$VAR6 + $VAR_HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
		<xsl:variable name="VAR7" select="round(format-number($SEVENTH_STEP,'##.0000'))" />
		<xsl:variable name="EIGHT_STEP" select="$VAR7 + $VAR_HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY" />
		<!--xsl:variable name="VAR_2" select="round(round(round($VAR_1 + $VAR_HO_34_REPLACEMENT_COST) + $VAR_HO_20_PREFERRED_PLUS_OR_HO_21_VIP) +  $VAR_HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY)" /-->
		<xsl:variable name="VAR8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
		<xsl:variable name="NINTH_STEP" select="$VAR8 * $VAR_INSURANCESCORE" />
		<xsl:variable name="VAR9" select="round(format-number($NINTH_STEP,'##.0000'))" />
		<xsl:variable name="TENTH_STEP" select="$VAR9 * $VAR_AGEOFHOMEFACTOR" />
		<xsl:variable name="VAR10" select="round(format-number($TENTH_STEP,'##.0000'))" />
		<xsl:variable name="ELEVENTH_STEP" select="$VAR10 * $VAR_PROTECTIVE_DEVICE" />
		<xsl:variable name="VAR11" select="round(format-number($ELEVENTH_STEP,'##.0000'))" />
		<xsl:variable name="TWLVENTH_STEP" select="$VAR11 * $VAR_DWELLING_UNDER_CONSTR" />
		<xsl:variable name="VAR12" select="round(format-number($TWLVENTH_STEP,'##.0000'))" />
		<xsl:variable name="THERTEENTH_STEP" select="$VAR12 * $VAR_EXPERIENCE_DISCOUNT" />
		<xsl:variable name="VAR13" select="round(format-number($THERTEENTH_STEP,'##.0000'))" />
		<xsl:variable name="FORTEENTH_STEP" select="$VAR13 * $VAR_MULTIPOLICY_DISCOUNT" />
		<xsl:variable name="VAR14" select="round(format-number($FORTEENTH_STEP,'##.0000'))" />
		<xsl:variable name="FIFTEENTH_STEP" select="$VAR14 * $VAR_VALUEDCUSTOMER" />
		<xsl:variable name="VAR15" select="round(format-number($FIFTEENTH_STEP,'##.0000'))" />
		<xsl:variable name="SIXTEENTH_STEP" select="$VAR15 * $VAR_PRIOR_LOSS" />
		<xsl:variable name="VAR16" select="round(format-number($SIXTEENTH_STEP,'##.0000'))" />
		<xsl:variable name="SEVENTEENTH_STEP" select="$VAR16 * $VAR_WSTOVE" />
		<xsl:variable name="VAR17" select="round($SEVENTEENTH_STEP)" />
		<!--xsl:variable name="VAR_3" select="round(round(round(round(round(round(round(round(round($VAR_2 *   $VAR_INSURANCESCORE)*$VAR_AGEOFHOMEFACTOR)* $VAR_PROTECTIVE_DEVICE)* $VAR_DWELLING_UNDER_CONSTR)* $VAR_EXPERIENCE_DISCOUNT)* $VAR_MULTIPOLICY_DISCOUNT)* $VAR_VALUEDCUSTOMER)* $VAR_PRIOR_LOSS)* $VAR_WSTOVE)" /-->
		<xsl:value-of select="(($VAR17 + $VAR_INCREASED_COV_C + $VAR_INCREASED_COV_D) - $VAR_SEASONALSECONDARYCREDIT) + $VAR_MEDICAL_CHARGES + $VAR_LIABLITY_CHARGES" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE PREMIER FOR MICHIGAN(PREMIER) HO-5					  -->
	<!--	
		1. Base (Find Base in Territory By Policy Form and Protection Class.) X
		2. Coverage A Factor (Note Minimum Coverage A is $175,000.) X
		3. 1.00 if insured to Replacement Cost (HO-5 Must Be at 100% Coverage A to Replacement Cost to be eligible.)
		X
		4. Term Factor: 1.00 Annual or .50 Semi-Annual X
		5. Deductible Factor X
		6. Add HO-34 Replacement Cost Personal Property Endorsement = 10% Minimum $35.
			7. Add HO-11 Expanded Replacement Coverage for Building = $15 Flat Charge
			8. Add HO-25 Premier V.I.P. End = $30
		9. Insurance Score Discount Factor X
		10. Multiply times Age of Home Factor X
		11. Protective Device Factor X
		12. HO-14 Dwelling Under Construction Credit (if applies then no: Age of Home, or Protective Device Credit) X
		13. Experience (Maturity or Age of Insured) Factor X
		14. Multiple Policy (Auto & Home) Factor X
		15. Valued Customer-Renewal Credit Factor X
		16. Surcharges
		17. Apply Section II Credits if any ($15 if Secondary and we have the Primary; or $2 for Secondary and No
		Primary).
		18. Add Charge for Increased Coverage C or Coverage D (if any).
		19. Add Any Other Endorsement Option Charges.
			
		20. Add Section II Liability Charges (if any).
		21. Add-Michigan Property Expense Recoupment Charges.

-->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_PREMIER_MICHIGAN_HO5">
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="DEWLLING-MAIN" />
		</xsl:variable>
		<xsl:variable name="VAR_COVERAGE">
			<xsl:call-template name="C-VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR_REPLACEMENTCOST">
			<xsl:call-template name="REPLACEMENT_COST_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCESCORE">
			<xsl:call-template name="INSURANCE_SCORE_CREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_C">
			<xsl:call-template name="COVERAGE_C_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_D">
			<xsl:call-template name="COVERAGE_D_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_DFACTOR">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_HO_34_REPLACEMENT_COST">
			<xsl:call-template name="HO_34_REPLACEMENT_COST" />
		</xsl:variable>
		<xsl:variable name="VAR_HO_20_PREFERRED_PLUS_OR_HO_21_VIP">
			<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
		</xsl:variable>
		<xsl:variable name="VAR_HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY">
			<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY" />
		</xsl:variable>
		<!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5 -->
		<xsl:variable name="VAR_AGEOFHOMEFACTOR">
			<xsl:variable name="AGEOFHOMEFACTOR">
				<xsl:call-template name="AGEOFHOME_FACTOR" />
			</xsl:variable>
			<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
			<xsl:variable name="AGEOFHOME_CREDIT_PERCENT" select="(1.00-$AGEOFHOMEFACTOR) * 100.00" />
			<xsl:variable name="VAR_MAXCREDIT_PERCENT" select="(1.00-$VAR_MAXCREDIT_FACTOR) * 100.00" />
			<xsl:choose>
				<xsl:when test="$AGEOFHOME_CREDIT_PERCENT &lt; $VAR_MAXCREDIT_PERCENT">
					<xsl:value-of select="$AGEOFHOMEFACTOR" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$VAR_MAXCREDIT_FACTOR" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_PROTECTIVE_DEVICE">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_DWELLING_UNDER_CONSTR">
			<xsl:call-template name="GET_DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_EXPERIENCE_DISCOUNT">
			<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_MULTIPOLICY_DISCOUNT">
			<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_VALUEDCUSTOMER">
			<xsl:call-template name="VALUEDCUSTOMER" />
		</xsl:variable>
		<!-- TILL HERE -->
		<xsl:variable name="VAR_PRIOR_LOSS">
			<xsl:call-template name="PRIOR_LOSS" />
		</xsl:variable>
		<xsl:variable name="VAR_WSTOVE">
			<xsl:call-template name="WSTOVE" />
		</xsl:variable>
		<xsl:variable name="VAR_SEASONALSECONDARYCREDIT">
			<xsl:call-template name="SEASONALSECONDARYCREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_MEDICAL_CHARGES">
			<xsl:call-template name="MEDICAL_CHARGES" />
		</xsl:variable>
		<xsl:variable name="VAR_LIABLITY_CHARGES">
			<xsl:call-template name="LIABLITY_CHARGES" />
		</xsl:variable>
		<!-- return value of adjusted base   -->
		<xsl:variable name="FIRST_STEP" select="$VAR_BASE * $VAR_COVERAGE" />
		<xsl:variable name="VAR1" select="round(format-number($FIRST_STEP,'##.0000'))" />
		<xsl:variable name="SECOND_STEP" select="$VAR1 * $VAR_REPLACEMENTCOST" />
		<xsl:variable name="VAR2" select="round(format-number($SECOND_STEP,'##.0000'))" />
		<xsl:variable name="THIRD_STEP" select="$VAR2 * $VAR_TERMFACTOR" />
		<xsl:variable name="VAR4" select="round(format-number($THIRD_STEP,'##.0000'))" />
		<xsl:variable name="FIFTH_STEP" select="$VAR4 * $VAR_DFACTOR" />
		<xsl:variable name="VAR5" select="round($FIFTH_STEP)" />
		<!--xsl:variable name="VAR_1" select="round(round(round(round($VAR_BASE * $VAR_COVERAGE)* $VAR_REPLACEMENTCOST)* $VAR_TERMFACTOR)*$VAR_DFACTOR)" /-->
		<xsl:variable name="SIX_STEP" select="$VAR5 + $VAR_HO_34_REPLACEMENT_COST" />
		<xsl:variable name="VAR6" select="round(format-number($SIX_STEP,'##.0000'))" />
		<xsl:variable name="SEVENTH_STEP" select="$VAR6 + $VAR_HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
		<xsl:variable name="VAR7" select="round(format-number($SEVENTH_STEP,'##.0000'))" />
		<xsl:variable name="EIGHT_STEP" select="$VAR7 + $VAR_HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY" />
		<xsl:variable name="VAR8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
		<xsl:variable name="NINTH_STEP" select="$VAR8 * $VAR_INSURANCESCORE" />
		<xsl:variable name="VAR9" select="round(format-number($NINTH_STEP,'##.0000'))" />
		<xsl:variable name="TENTH_STEP" select="$VAR9 * $VAR_AGEOFHOMEFACTOR" />
		<xsl:variable name="VAR10" select="round(format-number($TENTH_STEP,'##.0000'))" />
		<xsl:variable name="ELEVENTH_STEP" select="$VAR10 * $VAR_PROTECTIVE_DEVICE" />
		<xsl:variable name="VAR11" select="round(format-number($ELEVENTH_STEP,'##.0000'))" />
		<xsl:variable name="TWLVENTH_STEP" select="$VAR11 * $VAR_DWELLING_UNDER_CONSTR" />
		<xsl:variable name="VAR12" select="round(format-number($TWLVENTH_STEP,'##.0000'))" />
		<xsl:variable name="THERTEENTH_STEP" select="$VAR12 * $VAR_EXPERIENCE_DISCOUNT" />
		<xsl:variable name="VAR13" select="round(format-number($THERTEENTH_STEP,'##.0000'))" />
		<xsl:variable name="FORTEENTH_STEP" select="$VAR13 * $VAR_MULTIPOLICY_DISCOUNT" />
		<xsl:variable name="VAR14" select="round(format-number($FORTEENTH_STEP,'##.0000'))" />
		<xsl:variable name="FIFTEENTH_STEP" select="$VAR14 * $VAR_VALUEDCUSTOMER" />
		<xsl:variable name="VAR15" select="round(format-number($FIFTEENTH_STEP,'##.0000'))" />
		<xsl:variable name="SIXTEENTH_STEP" select="$VAR15 * $VAR_PRIOR_LOSS" />
		<xsl:variable name="VAR16" select="round(format-number($SIXTEENTH_STEP,'##.0000'))" />
		<xsl:variable name="SEVENTEENTH_STEP" select="$VAR16 * $VAR_WSTOVE" />
		<xsl:variable name="VAR17" select="round($SEVENTEENTH_STEP)" />
		<!--xsl:variable name="VAR_3" select="round(round(round(round(round(round(round(round(round($VAR_2  * $VAR_INSURANCESCORE)* $VAR_AGEOFHOMEFACTOR)* $VAR_PROTECTIVE_DEVICE)* $VAR_DWELLING_UNDER_CONSTR)* $VAR_EXPERIENCE_DISCOUNT)* $VAR_MULTIPOLICY_DISCOUNT)* $VAR_VALUEDCUSTOMER)* $VAR_PRIOR_LOSS)* $VAR_WSTOVE)" /-->
		<xsl:value-of select="($VAR17 -$VAR_SEASONALSECONDARYCREDIT) + $VAR_INCREASED_COV_C + $VAR_INCREASED_COV_D + $VAR_MEDICAL_CHARGES + $VAR_LIABLITY_CHARGES" />
	</xsl:template>
	<!-- ###################################  END - MICHIGAN ######################################### -->
	<!-- ###################################  START - INDIANA  ####################################### -->
	<!-- ============================================================================================ -->
	<!--								ADJUSTEDBASE FOR INDIANA (REGULAR) HO-2						  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_REGULAR_INDIANA_HO2_HO3">
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="DEWLLING_MAIN_INDIANA" />
		</xsl:variable>
		<xsl:variable name="VAR_COVERAGE">
			<xsl:call-template name="C-VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR_REPLACEMENTCOST">
			<xsl:call-template name="REPLACEMENT_COST_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCESCORE">
			<xsl:call-template name="INSURANCE_SCORE_CREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_C">
			<xsl:call-template name="COVERAGE_C_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_D">
			<xsl:call-template name="COVERAGE_D_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_DFACTOR">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5  -->
		<xsl:variable name="VAR_AGEOFHOMEFACTOR">
			<xsl:call-template name="AGEOFHOME_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_PROTECTIVE_DEVICE">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_DWELLING_UNDER_CONSTR">
			<xsl:call-template name="GET_DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_SMOKER">
			<xsl:call-template name="SMOKER" />
		</xsl:variable>
		<xsl:variable name="VAR_EXPERIENCE_DISCOUNT">
			<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_MULTIPOLICY_DISCOUNT">
			<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_VALUEDCUSTOMER">
			<xsl:call-template name="VALUEDCUSTOMER" />
		</xsl:variable>
		<!-- TILL HERE  -->
		<xsl:variable name="VAR_PRIOR_LOSS">
			<xsl:call-template name="PRIOR_LOSS" />
		</xsl:variable>
		<xsl:variable name="VAR_WSTOVE">
			<xsl:call-template name="WSTOVE" />
		</xsl:variable>
		<xsl:variable name="VAR_SEASONALSECONDARYCREDIT">
			<xsl:call-template name="SEASONALSECONDARYCREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_MEDICAL_CHARGES">
			<xsl:call-template name="MEDICAL_CHARGES" />
		</xsl:variable>
		<xsl:variable name="VAR_LIABLITY_CHARGES">
			<xsl:call-template name="LIABLITY_CHARGES" />
		</xsl:variable>
		<!-- return value of adjusted base-->
		<xsl:variable name="FIRST_STEP" select="$VAR_BASE * $VAR_COVERAGE" />
		<xsl:variable name="VAR1" select="round(format-number($FIRST_STEP,'##.0000'))" />
		<xsl:variable name="SECOND_STEP" select="$VAR1 * $VAR_REPLACEMENTCOST" />
		<xsl:variable name="VAR2" select="round(format-number($SECOND_STEP,'##.0000'))" />
		<xsl:variable name="THIRD_STEP" select="$VAR2 * $VAR_INSURANCESCORE" />
		<xsl:variable name="VAR3" select="round(format-number($THIRD_STEP,'##.0000'))" />
		<xsl:variable name="FORTH_STEP" select="$VAR3 * $VAR_TERMFACTOR" />
		<xsl:variable name="VAR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
		<xsl:variable name="FIFTH_STEP" select="$VAR4 * $VAR_DFACTOR" />
		<xsl:variable name="VAR5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
		<xsl:variable name="SIX_STEP" select="$VAR5 * $VAR_AGEOFHOMEFACTOR" />
		<xsl:variable name="VAR6" select="round($SIX_STEP)" />
		<!--xsl:variable name="VAR_1" select="round(round(round(round(round(format-number($FIRST_STEP,'##')* $VAR_REPAIRCOST)* $VAR_INSURANCESCORE)* $VAR_TERMFACTOR)* $VAR_DFACTOR)* $VAR_AGEOFHOMEFACTOR)" /-->
		<xsl:variable name="VAR_2" select="$VAR6 + $VAR_INCREASED_COV_C + $VAR_INCREASED_COV_D" />
		<xsl:variable name="SEVENTH_STEP" select="$VAR_2 * $VAR_PROTECTIVE_DEVICE" />
		<xsl:variable name="VAR7" select="round(format-number($SEVENTH_STEP,'##.0000'))" />
		<xsl:variable name="EIGHT_STEP" select="$VAR7 * $VAR_DWELLING_UNDER_CONSTR" />
		<xsl:variable name="VAR8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
		<xsl:variable name="NINTH_STEP" select="$VAR8 * $VAR_SMOKER" />
		<xsl:variable name="VAR9" select="round(format-number($NINTH_STEP,'##.0000'))" />
		<xsl:variable name="TENTH_STEP" select="$VAR9 * $VAR_EXPERIENCE_DISCOUNT" />
		<xsl:variable name="VAR10" select="round(format-number($TENTH_STEP,'##.0000'))" />
		<xsl:variable name="ELEVENTH_STEP" select="$VAR10 * $VAR_MULTIPOLICY_DISCOUNT" />
		<xsl:variable name="VAR11" select="round(format-number($ELEVENTH_STEP,'##.0000'))" />
		<xsl:variable name="TWLVENTH_STEP" select="$VAR11 * $VAR_PRIOR_LOSS" />
		<xsl:variable name="VAR12" select="round(format-number($TWLVENTH_STEP,'##.0000'))" />
		<xsl:variable name="THERTEENTH_STEP" select="$VAR12 * $VAR_WSTOVE" />
		<xsl:variable name="VAR13" select="round(format-number($THERTEENTH_STEP,'##.0000'))" />
		<xsl:variable name="FORTEENTH_STEP" select="$VAR13 * $VAR_VALUEDCUSTOMER" />
		<xsl:variable name="VAR14" select="round($FORTEENTH_STEP)" />
		<xsl:value-of select="($VAR14 -$VAR_SEASONALSECONDARYCREDIT) + $VAR_MEDICAL_CHARGES + $VAR_LIABLITY_CHARGES" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--								ADJUSTEDBASE FOR INDIANA (REGULAR) HO-3						  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_REGULAR_INDIANA_HO3">
(((((((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name="C-VALUE"></xsl:call-template>)*
			(<xsl:call-template name="REPLACEMENT_COST_FACTOR" />)*
			(<xsl:call-template name="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name="DFACTOR" />)*
			(<xsl:call-template name="AGEOFHOME_FACTOR" />)
			)
			+
			(
				(<xsl:call-template name="COVERAGE_C_PREMIUM" /> )
				+ (<xsl:call-template name="COVERAGE_D_PREMIUM" />)
			)
		    
	   )
	   *
	   (<xsl:call-template name="PROTECTIVEDEVICE" />))*
	   (<xsl:call-template name="GET_DUC_DISCOUNT" />))*
	   (<xsl:call-template name="SMOKER" />))*
	   (<xsl:call-template name="EXPERIENCEFACTOR" />))*
	   (<xsl:call-template name="MULTIPOLICY_FACTOR" />))*
	   (<xsl:call-template name="PRIOR_LOSS" />))*
	   (<xsl:call-template name="WSTOVE" />))*
	   (<xsl:call-template name="VALUEDCUSTOMER" />))-
	   (<xsl:call-template name="SEASONALSECONDARYCREDIT" />)))+
	 
	   (<xsl:call-template name="MEDICAL_CHARGES" />)+
	   (<xsl:call-template name="LIABLITY_CHARGES" />)
</xsl:template>
	<!-- ============================================================================================ -->
	<!--								ADJUSTEDBASE FOR INDIANA (REGULAR) HO-5						 
	1. Base (Find Base in Territory By Policy Form and Protection Class.) X
	2. Coverage A Factor (Note: Minimum Coverage A is $125,000.) X
	3. 1.00 if insured to Replacement Cost (HO-5 Must Be at 100% Coverage A to Replacement Cost to be eligible.) X
	4. Term Factor: 1.00 Annual or 0.50 Semi-Annual X
	5. Deductible Factor X
	6. Add HO-34 Replacement Cost Personal Property Endorsement = 10% Minimum $35; Maximum $75
	7. Add HO-11 Expanded Replacement Coverage for Building = $15 Flat Charge
	8. Add HO-24 Premier V.I.P. End = $40
	9. Insurance Score Discount Factor X
	10. Age of Home Factor X
	11. Protective Device Factor X
	12. Non-Smoker or smoker Factor X
	13. HO-14 Dwelling Under Construction Credit (if applies then no: Age of Home, or Protective Device Credit) X
	14. Experience (Maturity or Age of Insured) Factor X
	15. Multiple Policy (Auto & Home) Factor X
	16. Valued Customer-Renewal Credit Factor X
	17. Surcharges -(a) Prior Loss New Business: (b) Woodstove
	18. Add Charge for Increased Coverage C or Coverage D (if any).
	19. Add Any Other Endorsement Option Charges.
	20. Apply Section II Credits if any ($9 if Secondary and we have the Primary; or $2 for Secondary and No
	Primary).
	21. Add Section II Liability Charges (if any).  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_REGULAR_INDIANA_HO5">
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="DEWLLING-MAIN" />
		</xsl:variable>
		<xsl:variable name="VAR_COVERAGE">
			<xsl:call-template name="C-VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR_REPLACEMENTCOST">
			<xsl:call-template name="REPLACEMENT_COST_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCESCORE">
			<xsl:call-template name="INSURANCE_SCORE_CREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_C">
			<xsl:call-template name="COVERAGE_C_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_D">
			<xsl:call-template name="COVERAGE_D_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_DFACTOR">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_HO_34_REPLACEMENT_COST">
			<xsl:call-template name="HO_34_REPLACEMENT_COST" />
		</xsl:variable>
		<xsl:variable name="VAR_HO_20_PREFERRED_PLUS_OR_HO_21_VIP">
			<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
		</xsl:variable>
		<xsl:variable name="VAR_HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY">
			<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY" />
		</xsl:variable>
		<!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5 -->
		<xsl:variable name="VAR_AGEOFHOMEFACTOR">
			<xsl:variable name="AGEOFHOMEFACTOR">
				<xsl:call-template name="AGEOFHOME_FACTOR" />
			</xsl:variable>
			<xsl:choose>
				<xsl:when test="STATENAME = $STATE_INDIANA">
					<xsl:value-of select="$AGEOFHOMEFACTOR" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
					<xsl:variable name="AGEOFHOME_CREDIT_PERCENT" select="(1.00-$AGEOFHOMEFACTOR) * 100.00" />
					<xsl:variable name="VAR_MAXCREDIT_PERCENT" select="(1.00-$VAR_MAXCREDIT_FACTOR) * 100.00" />
					<xsl:choose>
						<xsl:when test="$AGEOFHOME_CREDIT_PERCENT &lt; $VAR_MAXCREDIT_PERCENT">
							<xsl:value-of select="$AGEOFHOMEFACTOR" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$VAR_MAXCREDIT_FACTOR" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_PROTECTIVE_DEVICE">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_DWELLING_UNDER_CONSTR">
			<xsl:call-template name="GET_DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_SMOKER_DISCOUNT">
			<xsl:call-template name="SMOKER" />
		</xsl:variable>
		<xsl:variable name="VAR_EXPERIENCE_DISCOUNT">
			<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_MULTIPOLICY_DISCOUNT">
			<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_VALUEDCUSTOMER">
			<xsl:call-template name="VALUEDCUSTOMER" />
		</xsl:variable>
		<!-- TILL HERE -->
		<xsl:variable name="VAR_PRIOR_LOSS">
			<xsl:call-template name="PRIOR_LOSS" />
		</xsl:variable>
		<xsl:variable name="VAR_WSTOVE">
			<xsl:call-template name="WSTOVE" />
		</xsl:variable>
		<xsl:variable name="VAR_SEASONALSECONDARYCREDIT">
			<xsl:call-template name="SEASONALSECONDARYCREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_MEDICAL_CHARGES">
			<xsl:call-template name="MEDICAL_CHARGES" />
		</xsl:variable>
		<xsl:variable name="VAR_LIABLITY_CHARGES">
			<xsl:call-template name="LIABLITY_CHARGES" />
		</xsl:variable>
		<!-- return value of adjusted base -->
		<xsl:variable name="FIRST_STEP" select="$VAR_BASE * $VAR_COVERAGE" />
		<xsl:variable name="VAR1" select="round(format-number($FIRST_STEP,'##.0000'))" />
		<xsl:variable name="SECOND_STEP" select="$VAR1 * $VAR_REPLACEMENTCOST" />
		<xsl:variable name="VAR2" select="round(format-number($SECOND_STEP,'##.0000'))" />
		<xsl:variable name="THIRD_STEP" select="$VAR2 * $VAR_TERMFACTOR" />
		<xsl:variable name="VAR4" select="round(format-number($THIRD_STEP,'##.0000'))" />
		<xsl:variable name="FIFTH_STEP" select="$VAR4 * $VAR_DFACTOR" />
		<xsl:variable name="VAR6" select="round($FIFTH_STEP)" />
		<xsl:variable name="SEVENTH_STEP" select="$VAR6 + $VAR_HO_34_REPLACEMENT_COST" />
		<xsl:variable name="VAR7" select="round(format-number($SEVENTH_STEP,'##.0000'))" />
		<xsl:variable name="EIGHT_STEP" select="$VAR7 + $VAR_HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
		<xsl:variable name="VAR8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
		<xsl:variable name="NINTH_STEP" select="$VAR8 + $VAR_HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY" />
		<xsl:variable name="VAR9" select="round($NINTH_STEP)" />
		<!--xsl:variable name="VAR_2" select="round(round(round($VAR_1 + $VAR_HO_34_REPLACEMENT_COST) + $VAR_HO_20_PREFERRED_PLUS_OR_HO_21_VIP) +  $VAR_HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY)" /-->
		<xsl:variable name="TENTH_STEP" select="$VAR9 * $VAR_INSURANCESCORE" />
		<xsl:variable name="VAR10" select="round(format-number($TENTH_STEP,'##.0000'))" />
		<xsl:variable name="ELEVENTH_STEP" select="$VAR10 * $VAR_AGEOFHOMEFACTOR" />
		<xsl:variable name="VAR11" select="round(format-number($ELEVENTH_STEP,'##.0000'))" />
		<xsl:variable name="TWLVENTH_STEP" select="$VAR11 * $VAR_PROTECTIVE_DEVICE" />
		<xsl:variable name="VAR12" select="round(format-number($TWLVENTH_STEP,'##.0000'))" />
		<xsl:variable name="THERTEENTH_STEP" select="$VAR12 * $VAR_DWELLING_UNDER_CONSTR" />
		<xsl:variable name="VAR13" select="round(format-number($THERTEENTH_STEP,'##.0000'))" />
		<xsl:variable name="FORTEENTH_STEP" select="$VAR13 * $VAR_SMOKER_DISCOUNT" />
		<xsl:variable name="VAR14" select="round(format-number($FORTEENTH_STEP,'##.0000'))" />
		<xsl:variable name="FIFTEENTH_STEP" select="$VAR14 * $VAR_EXPERIENCE_DISCOUNT" />
		<xsl:variable name="VAR15" select="round(format-number($FIFTEENTH_STEP,'##.0000'))" />
		<xsl:variable name="SIXTEENTH_STEP" select="$VAR15 * $VAR_MULTIPOLICY_DISCOUNT" />
		<xsl:variable name="VAR16" select="round(format-number($SIXTEENTH_STEP,'##.0000'))" />
		<xsl:variable name="SEVENTEENTH_STEP" select="$VAR16 * $VAR_VALUEDCUSTOMER" />
		<xsl:variable name="VAR17" select="round(format-number($SEVENTEENTH_STEP,'##.0000'))" />
		<xsl:variable name="EIGHTEENTH_STEP" select="$VAR17 * $VAR_PRIOR_LOSS" />
		<xsl:variable name="VAR18" select="round(format-number($EIGHTEENTH_STEP,'##.0000'))" />
		<xsl:variable name="NINTEENTH_STEP" select="$VAR18 * $VAR_WSTOVE" />
		<xsl:variable name="VAR19" select="round($NINTEENTH_STEP)" />
		<!--xsl:variable name="VAR_3" select="round(round(round(round(round(round(round(round(round(round($VAR_2 *   $VAR_INSURANCESCORE)*$VAR_AGEOFHOMEFACTOR)* $VAR_PROTECTIVE_DEVICE)* $VAR_DWELLING_UNDER_CONSTR)*$VAR_SMOKER_DISCOUNT)* $VAR_EXPERIENCE_DISCOUNT)* $VAR_MULTIPOLICY_DISCOUNT)* $VAR_VALUEDCUSTOMER)* $VAR_PRIOR_LOSS)* $VAR_WSTOVE)" /-->
		<xsl:value-of select="(($VAR19 + $VAR_INCREASED_COV_C + $VAR_INCREASED_COV_D) - $VAR_SEASONALSECONDARYCREDIT) + $VAR_MEDICAL_CHARGES + $VAR_LIABLITY_CHARGES" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--								ADJUSTEDBASE FOR INDIANA (REPAIR COST) HO-2 and HO-3		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_REPAIRCOST_INDIANA_HO2_HO3">
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="DEWLLING_MAIN_INDIANA" />
		</xsl:variable>
		<xsl:variable name="VAR_COVERAGE">
			<xsl:call-template name="C-VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR_REPAIRCOST">
			<xsl:call-template name="REPAIR_COST_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCESCORE">
			<xsl:call-template name="INSURANCE_SCORE_CREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_C">
			<xsl:call-template name="COVERAGE_C_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_D">
			<xsl:call-template name="COVERAGE_D_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_DFACTOR">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5 	  -->
		<xsl:variable name="VAR_AGEOFHOMEFACTOR">
			<xsl:call-template name="AGEOFHOME_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_PROTECTIVE_DEVICE">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_DWELLING_UNDER_CONSTR">
			<xsl:call-template name="GET_DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_SMOKER">
			<xsl:call-template name="SMOKER" />
		</xsl:variable>
		<xsl:variable name="VAR_EXPERIENCE_DISCOUNT">
			<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_MULTIPOLICY_DISCOUNT">
			<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_VALUEDCUSTOMER">
			<xsl:call-template name="VALUEDCUSTOMER" />
		</xsl:variable>
		<!-- TILL HERE  -->
		<xsl:variable name="VAR_PRIOR_LOSS">
			<xsl:call-template name="PRIOR_LOSS" />
		</xsl:variable>
		<xsl:variable name="VAR_WSTOVE">
			<xsl:call-template name="WSTOVE" />
		</xsl:variable>
		<xsl:variable name="VAR_SEASONALSECONDARYCREDIT">
			<xsl:call-template name="SEASONALSECONDARYCREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_MEDICAL_CHARGES">
			<xsl:call-template name="MEDICAL_CHARGES" />
		</xsl:variable>
		<xsl:variable name="VAR_LIABLITY_CHARGES">
			<xsl:call-template name="LIABLITY_CHARGES" />
		</xsl:variable>
		<!-- return value of adjusted base-->
		<xsl:variable name="FIRST_STEP" select="$VAR_BASE * $VAR_COVERAGE" />
		<xsl:variable name="VAR1" select="round(format-number($FIRST_STEP,'##.0000'))" />
		<xsl:variable name="SECOND_STEP" select="$VAR1 * $VAR_REPAIRCOST" />
		<xsl:variable name="VAR2" select="round(format-number($SECOND_STEP,'##.0000'))" />
		<xsl:variable name="THIRD_STEP" select="$VAR2 * $VAR_INSURANCESCORE" />
		<xsl:variable name="VAR3" select="round(format-number($THIRD_STEP,'##.0000'))" />
		<xsl:variable name="FORTH_STEP" select="$VAR3 * $VAR_TERMFACTOR" />
		<xsl:variable name="VAR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
		<xsl:variable name="FIFTH_STEP" select="$VAR4 * $VAR_DFACTOR" />
		<xsl:variable name="VAR5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
		<xsl:variable name="SIX_STEP" select="$VAR5 * $VAR_AGEOFHOMEFACTOR" />
		<!--xsl:variable name="VAR6" select="format-number($SIX_STEP,'##.0000')"/-->
		<xsl:variable name="VAR6" select="round($SIX_STEP)" />
		<!--xsl:variable name="VAR_1" select="round(round(round(round(round(format-number($FIRST_STEP,'##')* $VAR_REPAIRCOST)* $VAR_INSURANCESCORE)* $VAR_TERMFACTOR)* $VAR_DFACTOR)* $VAR_AGEOFHOMEFACTOR)" /-->
		<xsl:variable name="VAR_2" select="$VAR6 + $VAR_INCREASED_COV_C + $VAR_INCREASED_COV_D" />
		<xsl:variable name="SEVENTH_STEP" select="$VAR_2 * $VAR_PROTECTIVE_DEVICE" />
		<xsl:variable name="VAR7" select="round(format-number($SEVENTH_STEP,'##.0000'))" />
		<xsl:variable name="EIGHT_STEP" select="$VAR7 * $VAR_DWELLING_UNDER_CONSTR" />
		<xsl:variable name="VAR8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
		<xsl:variable name="NINTH_STEP" select="$VAR8 * $VAR_SMOKER" />
		<xsl:variable name="VAR9" select="round(format-number($NINTH_STEP,'##.0000'))" />
		<xsl:variable name="TENTH_STEP" select="$VAR9 * $VAR_EXPERIENCE_DISCOUNT" />
		<xsl:variable name="VAR10" select="round(format-number($TENTH_STEP,'##.0000'))" />
		<xsl:variable name="ELEVENTH_STEP" select="$VAR10 * $VAR_MULTIPOLICY_DISCOUNT" />
		<xsl:variable name="VAR11" select="round(format-number($ELEVENTH_STEP,'##.0000'))" />
		<xsl:variable name="TWLVENTH_STEP" select="$VAR11 * $VAR_PRIOR_LOSS" />
		<xsl:variable name="VAR12" select="round(format-number($TWLVENTH_STEP,'##.0000'))" />
		<xsl:variable name="THERTEENTH_STEP" select="$VAR12 * $VAR_WSTOVE" />
		<xsl:variable name="VAR13" select="round(format-number($THERTEENTH_STEP,'##.0000'))" />
		<xsl:variable name="FORTEENTH_STEP" select="$VAR13 * $VAR_VALUEDCUSTOMER" />
		<xsl:variable name="VAR14" select="round($FORTEENTH_STEP)" />
		<!--xsl:variable name="VAR_3" select="round(round(round(round(round(round(round(ceiling($VAR_2 * $VAR_PROTECTIVE_DEVICE)* $VAR_DWELLING_UNDER_CONSTR) *$VAR_SMOKER) * $VAR_EXPERIENCE_DISCOUNT)* $VAR_MULTIPOLICY_DISCOUNT)* $VAR_PRIOR_LOSS)* $VAR_WSTOVE)* $VAR_VALUEDCUSTOMER)" /-->
		<xsl:value-of select="round(($VAR14 -$VAR_SEASONALSECONDARYCREDIT) + $VAR_MEDICAL_CHARGES + $VAR_LIABLITY_CHARGES)" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--								ADJUSTEDBASE FOR INDIANA (REGULAR) HO-4					  -->
	<!-- 1. Base (Find Base in Territory By Policy Form 4 or 6, CO-I or CO-II, and Protection Class.) X
		2. Coverage C Factor (Tables based on whether is HO-4 with minimum $10,000 or HO-6 minimum $15,000.) X
		3. DELUXE Endorsement (if NO then X 1.00, if YES minimum Coverage C must be $25,000 and then take rate
		X 1.15-Min. $25.) NO MORE REQD AS WOLVERINE WILL NOT WRITE DELUXE ENDORSEMENTS
		4. Insurance Score Discount Factor X
		5. Term Factor: 1.00 Annual or .50 Semi-Annual X
		6. Deductible Factor X
		7. Protective Device Factor X
		8. Non-Smoker or Smoker Factor X
		9. Experience (Maturity or Age of Insured) Factor X
		10. Multiple Policy (Auto & Home) Factor (HO-4 must be minimum $25,000 Coverage C to get credit. HO-6
		no minimum.) X
		11. Valued Customer-Renewal Credit X
		12. Coverage A is 10% of Coverage C ($2,000 minimum), if Coverage A increased and there is no HO-66
		endorsement then rate is $2 per $1,000 increase. If an HO-66 Deluxe endorsement then charge is $2.20
		per $1,000 increased above automatic amount.
		13. Surcharges- (a) Prior Loss New Business; (b) Woodstove
		14. Apply Section II Credits if any ($9 if Secondary and we have the Primary; or $2 for Secondary and No
		Primary).
		15. Add-Any Other Endorsement Option Charges.
		16. Add-Section II Liability Charges (if any). -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_REGULAR_INDIANA_HO4_HO6">
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="DEWLLING_MAIN_INDIANA" />
		</xsl:variable>
		<xsl:variable name="VAR_COVERAGE">
			<xsl:call-template name="C-VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR_DELUXE_ENDORSEMENT">
			<xsl:call-template name="DELUXE_ENDORSEMENT" />
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCESCORE">
			<xsl:call-template name="INSURANCE_SCORE_CREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_DFACTOR">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_PROTECTIVE_DEVICE">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_SMOKER">
			<xsl:call-template name="SMOKER" />
		</xsl:variable>
		<xsl:variable name="VAR_EXPERIENCE_DISCOUNT">
			<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_MULTIPOLICY_DISCOUNT">
			<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_VALUEDCUSTOMER">
			<xsl:call-template name="VALUEDCUSTOMER" />
		</xsl:variable>
		<!-- TILL HERE   -->
		<xsl:variable name="VAR_PRIOR_LOSS">
			<xsl:call-template name="PRIOR_LOSS" />
		</xsl:variable>
		<xsl:variable name="VAR_WSTOVE">
			<xsl:call-template name="WSTOVE" />
		</xsl:variable>
		<xsl:variable name="VAR_SEASONALSECONDARYCREDIT">
			<xsl:call-template name="SEASONALSECONDARYCREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_MEDICAL_CHARGES">
			<xsl:call-template name="MEDICAL_CHARGES" />
		</xsl:variable>
		<xsl:variable name="VAR_LIABLITY_CHARGES">
			<xsl:call-template name="LIABLITY_CHARGES" />
		</xsl:variable>
		<!-- return value of adjusted base-->
		<xsl:variable name="FIRST_STEP" select="$VAR_BASE * $VAR_COVERAGE" />
		<xsl:variable name="VAR1" select="round(format-number($FIRST_STEP,'##.0000'))" />
		<xsl:variable name="SECOND_STEP" select="$VAR1 * $VAR_DELUXE_ENDORSEMENT" />
		<xsl:variable name="VAR2" select="round(format-number($SECOND_STEP,'##.0000'))" />
		<xsl:variable name="THIRD_STEP" select="$VAR2 * $VAR_INSURANCESCORE" />
		<xsl:variable name="VAR3" select="round(format-number($THIRD_STEP,'##.0000'))" />
		<xsl:variable name="FORTH_STEP" select="$VAR3 * $VAR_TERMFACTOR" />
		<xsl:variable name="VAR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
		<xsl:variable name="FIFTH_STEP" select="$VAR4 * $VAR_DFACTOR" />
		<xsl:variable name="VAR5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
		<xsl:variable name="SIX_STEP" select="$VAR5 * $VAR_PROTECTIVE_DEVICE" />
		<xsl:variable name="VAR6" select="round($SIX_STEP)" />
		<!--xsl:variable name="VAR_1" select="round(round(round(round(round(round($VAR_BASE * $VAR_COVERAGE)*$VAR_DELUXE_ENDORSEMENT)* $VAR_INSURANCESCORE)* $VAR_TERMFACTOR)* $VAR_DFACTOR)* $VAR_PROTECTIVE_DEVICE)" /-->
		<xsl:variable name="SEVENTH_STEP" select="$VAR6 * $VAR_SMOKER" />
		<xsl:variable name="VAR7" select="round(format-number($SEVENTH_STEP,'##.0000'))" />
		<xsl:variable name="EIGHT_STEP" select="$VAR7 * $VAR_EXPERIENCE_DISCOUNT" />
		<xsl:variable name="VAR8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
		<xsl:variable name="NINTH_STEP" select="$VAR8 * $VAR_MULTIPOLICY_DISCOUNT" />
		<xsl:variable name="VAR9" select="round(format-number($NINTH_STEP,'##.0000'))" />
		<xsl:variable name="TENTH_STEP" select="$VAR9 * $VAR_VALUEDCUSTOMER" />
		<xsl:variable name="VAR10" select="round(format-number($TENTH_STEP,'##.0000'))" />
		<xsl:variable name="ELEVENTH_STEP" select="$VAR10 * $VAR_PRIOR_LOSS" />
		<xsl:variable name="VAR11" select="round(format-number($ELEVENTH_STEP,'##.0000'))" />
		<xsl:variable name="TWLVENTH_STEP" select="$VAR11 * $VAR_WSTOVE" />
		<xsl:variable name="VAR13" select="round($TWLVENTH_STEP)" />
		<!--xsl:variable name="VAR_2" select="round(round(round(round(round(round($VAR_1 * $VAR_SMOKER) * $VAR_EXPERIENCE_DISCOUNT)* $VAR_MULTIPOLICY_DISCOUNT)*$VAR_VALUEDCUSTOMER)* $VAR_PRIOR_LOSS)* $VAR_WSTOVE)" /-->
		<xsl:value-of select="($VAR13 - $VAR_SEASONALSECONDARYCREDIT) + $VAR_MEDICAL_CHARGES + $VAR_LIABLITY_CHARGES " />
	</xsl:template>
	<!-- ###################################  END - INDIANA  ######################################## -->
	<!-- ============================================================================================ -->
	<!--						TEMPLATE FOR CALLING ADJUSTEDBASE AS PER THE INPUT-XML (START)		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="CALL_ADJUSTEDBASE">
		<xsl:variable name="VAR_ADJUSTED_BASE">
			<xsl:choose>
				<!-- For Michigan REPAIR COST -HO2 and HO3 have same formula -->
				<xsl:when test=" STATENAME ='MICHIGAN' and PRODUCT_PREMIER =$POLICYDESC_REPAIR and (PRODUCTNAME = $POLICYTYPE_HO2 or PRODUCTNAME = $POLICYTYPE_HO3 )">
					<xsl:call-template name="ADJUSTEDBASE_REPAIRCOST_MICHIGAN_HO2_HO3" />
				</xsl:when>
				<!-- For Michigan Replacement - HO2 and HO3 have same formula -->
				<xsl:when test="STATENAME ='MICHIGAN'   and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT  and (PRODUCTNAME = $POLICYTYPE_HO2 or PRODUCTNAME = $POLICYTYPE_HO3 )">
					<xsl:call-template name="ADJUSTEDBASE_REGULAR_MICHIGAN_HO2_HO3" />
				</xsl:when>
				<!-- For Michigan Replacement - HO5 -->
				<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO5 and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT">
					<xsl:call-template name="ADJUSTEDBASE_REGULAR_MICHIGAN_HO5" />
				</xsl:when>
				<!-- For Michigan - HO4 and HO6 have same formula -->
				<xsl:when test="STATENAME ='MICHIGAN' and (PRODUCTNAME = $POLICYTYPE_HO4 or PRODUCTNAME = $POLICYTYPE_HO6)">
					<xsl:call-template name="ADJUSTEDBASE_REGULAR_MICHIGAN_HO4_HO6" />
				</xsl:when>
				<!-- For Michigan - HO3 PREMIER -->
				<xsl:when test="STATENAME ='MICHIGAN' and PRODUCTNAME = $POLICYTYPE_HO3 and PRODUCT_PREMIER = $POLICYDESC_PREMIER">
					<xsl:call-template name="ADJUSTEDBASE_PREMIER_MICHIGAN_HO3" />
				</xsl:when>
				<!-- For Michigan - HO5 PREMIER -->
				<xsl:when test="STATENAME ='MICHIGAN' and PRODUCTNAME = $POLICYTYPE_HO5 and PRODUCT_PREMIER = $POLICYDESC_PREMIER">
					<xsl:call-template name="ADJUSTEDBASE_PREMIER_MICHIGAN_HO5" />
				</xsl:when>
				<!-- For INDIANA REPLACEMENT -->
				<xsl:when test="STATENAME ='INDIANA' and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT and (PRODUCTNAME = $POLICYTYPE_HO2 or PRODUCTNAME = $POLICYTYPE_HO3 )">
					<xsl:call-template name="ADJUSTEDBASE_REGULAR_INDIANA_HO2_HO3" />
				</xsl:when>
				<xsl:when test="STATENAME ='INDIANA' and PRODUCTNAME = $POLICYTYPE_HO5 and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT">
					<xsl:call-template name="ADJUSTEDBASE_REGULAR_INDIANA_HO5" />
				</xsl:when>
				<!-- For INDIANA REPAIR COST -->
				<xsl:when test=" STATENAME ='INDIANA' and PRODUCT_PREMIER =$POLICYDESC_REPAIR and (PRODUCTNAME = $POLICYTYPE_HO2 or PRODUCTNAME = $POLICYTYPE_HO3 )">
					<xsl:call-template name="ADJUSTEDBASE_REPAIRCOST_INDIANA_HO2_HO3" />
				</xsl:when>
				<!-- For INDIANA -HO4, HO6-->
				<xsl:when test="STATENAME ='INDIANA' and (PRODUCTNAME = $POLICYTYPE_HO4 or PRODUCTNAME = $POLICYTYPE_HO6)">
					<xsl:call-template name="ADJUSTEDBASE_REGULAR_INDIANA_HO4_HO6" />
				</xsl:when>
				<xsl:otherwise>1.00</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:value-of select="$VAR_ADJUSTED_BASE" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--						TEMPLATE FOR CALLING ADJUSTEDBASE AS PER THE INPUT-XML (END)		  -->
	<!-- ============================================================================================ -->
	<!--============================================================================================ -->
	<!--									TEST TEMPLATE 	(START)									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="TestRound">
	<xsl:param name="MyValue" />
	{
	<xsl:value-of select="round(normalize-space($VAR-DEWLLING-MAIN))"></xsl:value-of>
	}
</xsl:template>
	<xsl:template name="GET_PROTECTIVE_DEVICE_DISCOUNT_new">
		<!-- Get max applicable credit factor (for eg. 0.50 = 50%)                                                                                                                                                                                                                                                                ) -->
		<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
		<xsl:choose>
			<xsl:when test="CONSTRUCTIONCREDIT = 'N'"> <!-- Prot. Device credit Applicable only if not under construction -->
				<!-- Calculate the final credit uptil age of home 
				 If the calculated credit is greater than= max applicable credit factor then it means 
				 that total discount is greater than 50% and we do not need to send Protective Device discount -->
				<xsl:variable name="VAR_AGEOFHOME_FACTOR">
					<xsl:call-template name="AGEOFHOME_FACTOR" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$VAR_AGEOFHOME_FACTOR &gt;= $VAR_MAXCREDIT_FACTOR">
					0.00	<!-- No need to calculate Prot. Device discount as credit limit has exceeded -->
				</xsl:when>
					<xsl:otherwise> <!-- Else calculate the protective device -->
						<xsl:variable name="PROTDEVICE_CALCULATEDFACTOR">
							<xsl:call-template name="PROTECTIVEDEVICE" />
						</xsl:variable>
						<xsl:variable name="FINALCREDIT_UPTO_PROTDEVICE">
							<!-- (1-FACTOR because 10% credit = 0.9 factor, 20% credit = 0.8  and total credit  30% = ((1-0.9)+(1-0.8))=(0.1+0.2 =0.3))-->
							<xsl:value-of select="(1- $VAR_AGEOFHOME_FACTOR) + (1-$PROTDEVICE_CALCULATEDFACTOR)" />
						</xsl:variable>
						<xsl:choose>
							<!--including prot device, the credit is less than=  max total credit applicable -->
							<xsl:when test="$FINALCREDIT_UPTO_PROTDEVICE &lt;= $VAR_MAXCREDIT_FACTOR">
								<xsl:value-of select="PROTDEVICE_CALCULATEDFACTOR" />
							</xsl:when>
							<xsl:otherwise> <!--including prot device, the credit exceeds max total credit applicable -->
								<!-- Send the factor balance after subtracting age of home factor from maxcredit.-->
								<xsl:value-of select="($FINALCREDIT_UPTO_PROTDEVICE  - $VAR_MAXCREDIT_FACTOR) + $PROTDEVICE_CALCULATEDFACTOR" />
								<xsl:value-of select="$VAR_MAXCREDIT_FACTOR - (1 - $VAR_AGEOFHOME_FACTOR)" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
		1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="GET_PROTECTIVE_DEVICE_DISCOUNT_old"> <!-- TO BE REMOVED -->
		<!-- Get max applicable credit factor -->
		<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
		<xsl:choose>
			<xsl:when test="CONSTRUCTIONCREDIT = 'N'">
				<xsl:variable name="VAR1">
					<xsl:call-template name="AGEOFHOME_FACTOR" />
				</xsl:variable>
				<xsl:variable name="FINALCREDIT_AGEOFHOME"> <!-- Credit left after age of home -->
					<xsl:value-of select="1 - $VAR1" />
				</xsl:variable>
				<!-- Calculate the final credit uptil age of home 
				 If the calculated credit is less than 0.50 then it means 
				 that total discount is greater than 50%  and we do not need to send Protective Device discount-->
				<xsl:choose>
					<xsl:when test="$FINALCREDIT_AGEOFHOME &gt; $VAR_MAXCREDIT_FACTOR">
						<xsl:value-of select="$FINALCREDIT_AGEOFHOME" />
					</xsl:when>
					<xsl:otherwise> <!-- Else calculate the protective device -->
						<xsl:variable name="PROTDEVICE_CALCULATEDFACTOR">
							<xsl:call-template name="PROTECTIVEDEVICE" />
						</xsl:variable>
						<xsl:variable name="FINALCREDIT_PROTDEVICE"> <!-- Sum of credits uptil prot. device -->
							<xsl:value-of select="$FINALCREDIT_AGEOFHOME + (1 - $PROTDEVICE_CALCULATEDFACTOR)" />
						</xsl:variable>
						<xsl:choose>
							<xsl:when test="$FINALCREDIT_PROTDEVICE = 0.00">
								<xsl:value-of select="PROTDEVICE_CALCULATEDFACTOR" />
							</xsl:when>
							<xsl:otherwise>
								<xsl:choose>
									<xsl:when test="$FINALCREDIT_PROTDEVICE  &gt; $VAR_MAXCREDIT_FACTOR">
										<xsl:value-of select="($FINALCREDIT_PROTDEVICE  - $VAR_MAXCREDIT_FACTOR) + $PROTDEVICE_CALCULATEDFACTOR" />
									</xsl:when>
									<xsl:otherwise>
										<xsl:choose>
											<xsl:when test="$FINALCREDIT_PROTDEVICE  &gt; $VAR_MAXCREDIT_FACTOR">
												<xsl:variable name="PROTDEVICEFACTOR">
												<xsl:value-of select="$VAR_MAXCREDIT_FACTOR - (1 - $FINALCREDIT_PROTDEVICE)" />)) 
											</xsl:variable>
												<xsl:value-of select="$PROTDEVICEFACTOR" />
											</xsl:when>
											<xsl:when test="$FINALCREDIT_PROTDEVICE  &lt; $VAR_MAXCREDIT_FACTOR">
												<xsl:value-of select="$PROTDEVICE_CALCULATEDFACTOR" />
											</xsl:when>
											<xsl:otherwise>1.00</xsl:otherwise>
										</xsl:choose>
									</xsl:otherwise>
								</xsl:choose>
							</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
				<!--including prot device, the credit becomes > 50% -->
			</xsl:when>
			<xsl:otherwise>
		1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="GET_PROTECTIVE_DEVICE_DISCOUNT">
		<!-- Get max applicable credit factor -->
		<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
		<xsl:variable name="VAR_MAXCREDIT_PERCENT" select="(1.00-$VAR_MAXCREDIT_FACTOR)*100" />
		<xsl:choose> <!-- Applicable only when building is not under construction-->
			<xsl:when test="STATENAME= $STATE_MICHIGAN and CONSTRUCTIONCREDIT = 'N'">
				<xsl:variable name="VAR_AOH_FACTOR">
					<xsl:call-template name="AGEOFHOME_FACTOR" />
				</xsl:variable>
				<xsl:variable name="FINALCREDIT_AGEOFHOME"> <!-- Credit Percentage age of home -->
					<xsl:value-of select="(1.00 - $VAR_AOH_FACTOR)*100" />
				</xsl:variable>
				<!-- Calculate the final credit percentage uptil age of home 
				 1. If the calculated credit PERCENTAGE is greater than the max credit 
				 then send 1.00 for proptective device
				 2. If the calculated credit PERCENTAGE is lesser than the max credit 
				 then check the final credit percentage uptil protective device 
				 3. If the calculated credit PERCENTAGE is lesser than the max credit 
				 then send protective device  factor
				 4. If the calculated credit PERCENTAGE is greater than the max credit 
				 then send the (final credit percentage uptil protective device - aoh credit) factor				 
				 -->
				<xsl:choose>
					<xsl:when test="$FINALCREDIT_AGEOFHOME &gt; $VAR_MAXCREDIT_PERCENT">
					1.00
					</xsl:when>
					<xsl:otherwise> <!-- Else calculate the protective device -->
						<xsl:variable name="PROTDEVICE_CALCULATEDFACTOR">
							<xsl:call-template name="PROTECTIVEDEVICE" />
						</xsl:variable>
						<xsl:variable name="FINALCREDIT_PROTDEVICE"> <!-- Sum of credits uptil prot. device -->
							<xsl:value-of select="$FINALCREDIT_AGEOFHOME + ((1.00 - $PROTDEVICE_CALCULATEDFACTOR)*100)" />
						</xsl:variable>
						<xsl:choose>
							<xsl:when test="$FINALCREDIT_PROTDEVICE  &lt;= $VAR_MAXCREDIT_PERCENT">
								<xsl:value-of select="$PROTDEVICE_CALCULATEDFACTOR" />
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="(100 - ($VAR_MAXCREDIT_PERCENT - $FINALCREDIT_AGEOFHOME)) div 100.00" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
				<!--including prot device, the credit becomes > 50% -->
			</xsl:when>
			<xsl:when test="STATENAME= $STATE_INDIANA and CONSTRUCTIONCREDIT = 'N'">
				<xsl:variable name="PROTDEVICE_CALCULATEDFACTOR">
					<xsl:call-template name="PROTECTIVEDEVICE" />
				</xsl:variable>
				<xsl:value-of select="$PROTDEVICE_CALCULATEDFACTOR" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="GET_DUC_DISCOUNT">
		<xsl:choose>
			<xsl:when test="STATENAME=$STATE_INDIANA">
				<xsl:variable name="DUC">
					<xsl:call-template name="DWELLING_UNDER_CONSTRUCTION" />
				</xsl:variable>
				<xsl:value-of select="$DUC" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
				<xsl:variable name="VAR1">
					<xsl:call-template name="AGEOFHOME_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
				</xsl:variable>
				<xsl:variable name="FINALCREDIT_PROT_DEVICE_DISCOUNT">
					<xsl:value-of select="(1.00 - $VAR1) + (1.00 - $VAR2)" />
				</xsl:variable>
				<!-- Calculate the final credit of home and protective device. 
				 If the credit is greater than 0.50 then it means that total discount is less than 50%  -->
				<xsl:choose>
					<xsl:when test="$FINALCREDIT_PROT_DEVICE_DISCOUNT &gt; $VAR_MAXCREDIT_FACTOR">
						<xsl:value-of select="$FINALCREDIT_PROT_DEVICE_DISCOUNT - $VAR_MAXCREDIT_FACTOR" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:variable name="DUC">
							<xsl:call-template name="DWELLING_UNDER_CONSTRUCTION" />
						</xsl:variable>
						<xsl:variable name="FINALCREDIT_PROT_DEVICE_NEW">
							<xsl:value-of select="$FINALCREDIT_PROT_DEVICE_DISCOUNT + (1.00 - $DUC)" />
						</xsl:variable>
						<xsl:choose>
							<xsl:when test="$FINALCREDIT_PROT_DEVICE_NEW = 0.00">
								<xsl:variable name="CONSTRUCTIONFACTOR">
									<xsl:call-template name="DWELLING_UNDER_CONSTRUCTION" />
								</xsl:variable>
								<xsl:value-of select="$CONSTRUCTIONFACTOR" />
							</xsl:when>
							<xsl:otherwise>
								<xsl:choose>
									<xsl:when test="$FINALCREDIT_PROT_DEVICE_NEW  &gt; $VAR_MAXCREDIT_FACTOR">
										<xsl:value-of select="($FINALCREDIT_PROT_DEVICE_NEW  - $VAR_MAXCREDIT_FACTOR) + $DUC" />
									</xsl:when>
									<xsl:otherwise>
										<xsl:choose>
											<xsl:when test="$FINALCREDIT_PROT_DEVICE_NEW &gt; $VAR_MAXCREDIT_FACTOR">
												<xsl:variable name="DUCFACTOR">
													<xsl:value-of select="$VAR_MAXCREDIT_FACTOR - (1 - $FINALCREDIT_PROT_DEVICE_NEW)" />
												</xsl:variable>
												<xsl:value-of select="$DUCFACTOR" />
											</xsl:when>
											<xsl:when test="$FINALCREDIT_PROT_DEVICE_NEW &lt;= $VAR_MAXCREDIT_FACTOR">
												<xsl:value-of select="$DUC" />
											</xsl:when>
											<xsl:otherwise>1.00 </xsl:otherwise>
										</xsl:choose>
									</xsl:otherwise>
								</xsl:choose>
							</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
				<!--including prot device, the credit becomes > 50% -->
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Applying term factor on dwelling construction 
	<xsl:template name="DWELLING_CONSTRUC_FACTOR">
	<xsl:variable name="GET_FACTOR">
			<xsl:call-template name="GET_DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$GET_FACTOR = '1.00' or $GET_FACTOR = '1'">
				<xsl:value-of select="$GET_FACTOR" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$GET_FACTOR * $TERMFACTOR" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>-->
	<xsl:template name="GET_EXPERIENCE_DISCOUNT">
		<xsl:choose>
			<xsl:when test="STATENAME = $STATE_INDIANA"> <!-- No such check for Indiana -->
				<xsl:call-template name="EXPERIENCEFACTOR" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR1">
					<xsl:call-template name="AGEOFHOME_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="GET_DUC_DISCOUNT" />
				</xsl:variable>
				<xsl:variable name="FINALCREDIT_DUC">
					<xsl:value-of select="(1.00-$VAR1)+(1.00-$VAR2)+(1.00-$VAR3)" />
				</xsl:variable>
				<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
				<xsl:choose>
					<xsl:when test="$FINALCREDIT_DUC &gt; 0.50">
						<xsl:value-of select="$FINALCREDIT_DUC - $VAR_MAXCREDIT_FACTOR" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:variable name="EXPERIENCE">
							<xsl:call-template name="EXPERIENCEFACTOR" />
						</xsl:variable>
						<xsl:variable name="FINALCREDIT_EXPERIENCE">
							<xsl:value-of select="$FINALCREDIT_DUC + (1- $EXPERIENCE)" />
						</xsl:variable>
						<xsl:choose>
							<xsl:when test="$FINALCREDIT_EXPERIENCE = 0.00">
								<xsl:call-template name="EXPERIENCEFACTOR" />
							</xsl:when>
							<xsl:otherwise>
								<!--
								IF( <xsl:value-of select ="$FINALCREDIT_EXPERIENCE"/> &gt; 0.50)
								THEN
									(<xsl:value-of select="$FINALCREDIT_EXPERIENCE"/> - 0.50  ) + 
									(<xsl:value-of select="$EXPERIENCE"/>)
								ELSE IF (<xsl:value-of select="$FINALCREDIT_EXPERIENCE"/> &lt;= 0.50) 
								THEN
									<xsl:value-of select="$EXPERIENCE"/>
								ELSE
									1.00
								-->
								<xsl:choose>
									<xsl:when test="$FINALCREDIT_EXPERIENCE &gt; $VAR_MAXCREDIT_FACTOR">
										<xsl:value-of select="($FINALCREDIT_EXPERIENCE - $VAR_MAXCREDIT_FACTOR) + $EXPERIENCE" />
									</xsl:when>
									<xsl:when test="$FINALCREDIT_EXPERIENCE &lt;= $VAR_MAXCREDIT_FACTOR">
										<xsl:value-of select="$EXPERIENCE" />
									</xsl:when>
									<xsl:otherwise>1.00</xsl:otherwise>
								</xsl:choose>
							</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
		<!--including prot device, the credit becomes > 50% -->
	</xsl:template>
	<xsl:template name="GET_MULTIPOLICY_DISCOUNT">
		<xsl:choose>
			<xsl:when test="STATENAME=$STATE_INDIANA">
				<xsl:call-template name="MULTIPOLICY_FACTOR" /> <!-- No max discount check for Indiana-->
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR1">
					<xsl:call-template name="AGEOFHOME_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="GET_DUC_DISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
				</xsl:variable>
				<xsl:variable name="FINALCREDIT_EXPERIENCE">
					<xsl:value-of select="(1.00-$VAR1)+(1.00-$VAR2)+(1.00-$VAR3)+(1.00-$VAR4)" />
				</xsl:variable>
				<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
				<xsl:choose>
					<xsl:when test="$FINALCREDIT_EXPERIENCE &gt; $VAR_MAXCREDIT_FACTOR">
						<xsl:value-of select="$FINALCREDIT_EXPERIENCE - $VAR_MAXCREDIT_FACTOR" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:variable name="MULTIPOLICY">
							<xsl:call-template name="MULTIPOLICY_FACTOR" />
						</xsl:variable>
						<xsl:variable name="FINALCREDIT_MULTIPOLICY">
							<xsl:value-of select="$FINALCREDIT_EXPERIENCE + (1 - $MULTIPOLICY)" />
						</xsl:variable>
						<!--<xsl:value-of select="$FINALCREDIT_MULTIPOLICY"/>	-->
						<!--
						IF(<xsl:value-of select ="$FINALCREDIT_MULTIPOLICY"/> &gt; 0.50)
						THEN
							<xsl:variable name="MULTIPOLICYFACTOR">
								(<xsl:value-of select="$FINALCREDIT_MULTIPOLICY"/> - 0.50  ) + 
								(<xsl:value-of select="$MULTIPOLICY"/>)
							</xsl:variable>
							<xsl:value-of select="$MULTIPOLICYFACTOR"/>		
						ELSE IF	(<xsl:value-of select ="$FINALCREDIT_MULTIPOLICY"/> &lt; 0.50)
						THEN
							<xsl:value-of select="$MULTIPOLICY"/>
						ELSE
							1.00			
						-->
						<xsl:choose>
							<xsl:when test="$FINALCREDIT_MULTIPOLICY &gt; $VAR_MAXCREDIT_FACTOR">
								<xsl:variable name="MULTIPOLICYFACTOR">
									<xsl:value-of select="($FINALCREDIT_MULTIPOLICY - $VAR_MAXCREDIT_FACTOR) + $MULTIPOLICY" />
								</xsl:variable>
								<xsl:value-of select="$MULTIPOLICYFACTOR" />
							</xsl:when>
							<xsl:when test="$FINALCREDIT_MULTIPOLICY &lt;= $VAR_MAXCREDIT_FACTOR">
								<xsl:value-of select="$MULTIPOLICY" />
							</xsl:when>
							<xsl:otherwise>1.00</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="GET_VALUEDCUSTOMER_DISCOUNT">
		<xsl:choose>
			<xsl:when test="STATENAME=$STATE_INDIANA">
				<xsl:call-template name="MULTIPOLICY_FACTOR" /> <!-- No max discount check for Indiana-->
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR1">
					<xsl:call-template name="AGEOFHOME_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="GET_DUC_DISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
				</xsl:variable>
				<xsl:variable name="FINALCREDIT_EXPERIENCE">
					<xsl:value-of select="(1.00-$VAR1)+(1.00-$VAR2)+(1.00-$VAR3)+(1.00-$VAR4)" />
				</xsl:variable>
				<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
				<xsl:choose>
					<xsl:when test="$FINALCREDIT_EXPERIENCE &gt; $VAR_MAXCREDIT_FACTOR">
						<xsl:value-of select="$FINALCREDIT_EXPERIENCE - $VAR_MAXCREDIT_FACTOR" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:variable name="MULTIPOLICY">
							<xsl:call-template name="MULTIPOLICY_FACTOR" />
						</xsl:variable>
						<xsl:variable name="VALUEDCUSTOMER_FACTOR">
							<xsl:call-template name="VALUEDCUSTOMER_FACTOR" />
						</xsl:variable>
						<xsl:variable name="FINALCREDIT_MULTIPOLICY">
							<xsl:value-of select="$FINALCREDIT_EXPERIENCE + (1 - $MULTIPOLICY)" />
						</xsl:variable>
						<!--<xsl:value-of select="$FINALCREDIT_MULTIPOLICY"/>	-->
						<!--
						IF(<xsl:value-of select ="$FINALCREDIT_MULTIPOLICY"/> &gt; 0.50)
						THEN
							<xsl:variable name="MULTIPOLICYFACTOR">
								(<xsl:value-of select="$FINALCREDIT_MULTIPOLICY"/> - 0.50  ) + 
								(<xsl:value-of select="$MULTIPOLICY"/>)
							</xsl:variable>
							<xsl:value-of select="$MULTIPOLICYFACTOR"/>		
						ELSE IF	(<xsl:value-of select ="$FINALCREDIT_MULTIPOLICY"/> &lt; 0.50)
						THEN
							<xsl:value-of select="$MULTIPOLICY"/>
						ELSE
							1.00			
						-->
						<xsl:choose>
							<xsl:when test="$FINALCREDIT_MULTIPOLICY &gt; $VAR_MAXCREDIT_FACTOR">
								<xsl:variable name="MULTIPOLICYFACTOR">
									<xsl:value-of select="($FINALCREDIT_MULTIPOLICY - $VAR_MAXCREDIT_FACTOR) + $MULTIPOLICY" />
								</xsl:variable>
								<xsl:value-of select="$MULTIPOLICYFACTOR" />
							</xsl:when>
							<xsl:when test="$FINALCREDIT_MULTIPOLICY &lt;= $VAR_MAXCREDIT_FACTOR">
								<xsl:value-of select="$VALUEDCUSTOMER_FACTOR" />
							</xsl:when>
							<xsl:otherwise>1.00</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="GET_TOTAL_DISCOUNT">
	<xsl:variable name="FINALCREDIT_EXPERIENCE">
		(1.00-(<xsl:call-template name="AGEOFHOME_FACTOR" />))
		+	(1.00-(<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />))
		+	(1.00-(<xsl:call-template name="GET_DUC_DISCOUNT" />))
		+	(1.00-(<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />))
		+	(1.00-(<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />))
	</xsl:variable>
	
	
<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />	
	IF (<xsl:value-of select="$FINALCREDIT_EXPERIENCE" /> &gt; <xsl:value-of select="$VAR_MAXCREDIT_FACTOR" />) 
	THEN
		1.00
	ELSE
		0.00
	
</xsl:template>
	<xsl:template name="REGULAR_INDIANA_HO3_WOODSTOVE_DISPLAY">
((((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name="C_VALUE_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name="DFACTOR" />)*
			(<xsl:call-template name="AGEOFHOME_FACTOR" />)
			)
			+
			(
				(<xsl:call-template name="COVERAGE_C_PREMIUM" /> )
				+ (<xsl:call-template name="COVERAGE_D_PREMIUM" />)
			)
		    
	   )
	   *
	   (<xsl:call-template name="PROTECTIVEDEVICE" />))*
	   (<xsl:call-template name="GET_DUC_DISCOUNT" />))*
	   (<xsl:call-template name="SMOKER" />))*
	   (<xsl:call-template name="EXPERIENCEFACTOR" />))*
	   (<xsl:call-template name="MULTIPOLICY_FACTOR" />))*
	   (<xsl:call-template name="PRIOR_LOSS" />))*
	   (<xsl:call-template name="WSTOVE" />)) - 
	   (((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name="C_VALUE_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name="DFACTOR" />)*
			(<xsl:call-template name="AGEOFHOME_FACTOR" />)
			)
			+
			(
				(<xsl:call-template name="COVERAGE_C_PREMIUM" /> )
				+ (<xsl:call-template name="COVERAGE_D_PREMIUM" />)
			)
		    
	   )
	   *
	   (<xsl:call-template name="PROTECTIVEDEVICE" />))*
	   (<xsl:call-template name="GET_DUC_DISCOUNT" />))*
	   (<xsl:call-template name="SMOKER" />))*
	   (<xsl:call-template name="EXPERIENCEFACTOR" />))*
	   (<xsl:call-template name="MULTIPOLICY_FACTOR" />))*
	   (<xsl:call-template name="PRIOR_LOSS" />))
	  
</xsl:template>
	<xsl:template name="REGULAR_INDIANA_HO3_PRIOR_LOSS_DISPLAY">
(((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name="C_VALUE_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name="DFACTOR" />)*
			(<xsl:call-template name="AGEOFHOME_FACTOR" />)
			)
			+
			(
				(<xsl:call-template name="COVERAGE_C_PREMIUM" /> )
				+ (<xsl:call-template name="COVERAGE_D_PREMIUM" />)
			)
		    
	   )
	   *
	   (<xsl:call-template name="PROTECTIVEDEVICE" />))*
	   (<xsl:call-template name="GET_DUC_DISCOUNT" />))*
	   (<xsl:call-template name="SMOKER" />))*
	   (<xsl:call-template name="EXPERIENCEFACTOR" />))*
	   (<xsl:call-template name="MULTIPOLICY_FACTOR" />))*
	   (<xsl:call-template name="PRIOR_LOSS" />))-
	   ((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name="C_VALUE_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name="DFACTOR" />)*
			(<xsl:call-template name="AGEOFHOME_FACTOR" />)
			)
			+
			(
				(<xsl:call-template name="COVERAGE_C_PREMIUM" /> )
				+ (<xsl:call-template name="COVERAGE_D_PREMIUM" />)
			)
		    
	   )
	   *
	   (<xsl:call-template name="PROTECTIVEDEVICE" />))*
	   (<xsl:call-template name="GET_DUC_DISCOUNT" />))*
	   (<xsl:call-template name="SMOKER" />))*
	   (<xsl:call-template name="EXPERIENCEFACTOR" />))*
	   (<xsl:call-template name="MULTIPOLICY_FACTOR" />))
	  
</xsl:template>
	<!-- ============================================================================================ -->
	<!--									TEST TEMPLATE 	(END)									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="PREMIUM_GROUP">
		<!-- Set the Group -->
		<xsl:variable name="TERCODES" select="TERRITORYCODES" />
		<xsl:variable name="F_CODE" select="FORM_CODE" />
		<xsl:variable name="FORMGROUP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING']/ATTRIBUTES [@FORMID = $F_CODE]/@GROUPID" />
		<xsl:choose>
			<xsl:when test="normalize-space($F_CODE) != ''">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORM_PREMIUM_GROUP']/ATTRIBUTES [@FORMGROUPID = $FORMGROUP and @TERRITORY_ID = $TERCODES]/@FORM_PREMIUM_GROUP" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Calculate the final credit uptil age of home 
		If the credit is less than 0.50 then it means 
		that total discount is greater than 50%  and we do not need to send Protective Device discount-->
	<xsl:template name="GET_DUC_DISCOUNT_TEMP">
		<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
		<xsl:variable name="FINALCREDIT_PROT_DEVICE_DISCOUNT_TOTAL_DISCOUNT">
			<xsl:variable name="VAR1">
				<xsl:call-template name="AGEOFHOME_FACTOR" />
			</xsl:variable>
			<xsl:variable name="VAR2">
				<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT_1" />
			</xsl:variable>
			<xsl:value-of select="(1.00 - $VAR1) + (1.00 - $VAR2)" />
		</xsl:variable>
		<xsl:variable name="FINALCREDIT_PROT_DEVICE_DISCOUNT">
				 
					0.00
			</xsl:variable>
		<xsl:choose>
			<xsl:when test="$FINALCREDIT_PROT_DEVICE_DISCOUNT &gt; $VAR_MAXCREDIT_FACTOR">
				<xsl:value-of select="$FINALCREDIT_PROT_DEVICE_DISCOUNT - $VAR_MAXCREDIT_FACTOR" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="DUC">
					<xsl:call-template name="DWELLING_UNDER_CONSTRUCTION" />
				</xsl:variable>
				<xsl:variable name="FINALCREDIT_PROT_DEVICE_NEW">
					<xsl:value-of select="$FINALCREDIT_PROT_DEVICE_DISCOUNT + (1.00 - $DUC)" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$FINALCREDIT_PROT_DEVICE_NEW = 0.00">
						<xsl:call-template name="DWELLING_UNDER_CONSTRUCTION" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:choose>
							<!-- <xsl:when test ="$FINALCREDIT_PROT_DEVICE_NEW &gt; 0.50"> -->
							<xsl:when test="$FINALCREDIT_PROT_DEVICE_NEW &gt; $VAR_MAXCREDIT_FACTOR">
								<!-- <xsl:value-of select="($FINALCREDIT_PROT_DEVICE_NEW - 0.50) + $DUC"/> -->
								<xsl:value-of select="($FINALCREDIT_PROT_DEVICE_NEW - $VAR_MAXCREDIT_FACTOR) + $DUC" />
							</xsl:when>
							<!-- <xsl:when test ="$FINALCREDIT_PROT_DEVICE_NEW &lt;= 0.50"> -->
							<xsl:when test="$FINALCREDIT_PROT_DEVICE_NEW &lt;= $VAR_MAXCREDIT_FACTOR">
								<xsl:value-of select="$DUC" />
							</xsl:when>
							<xsl:otherwise>1.00</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
		<!--including prot device, the credit becomes > 50% -->
	</xsl:template>
	<!-- this template not in use -->
	<xsl:template name="GET_EXPERIENCE_DISCOUNT_TEMP">
		<xsl:variable name="FINALCREDIT_DUC">
			<xsl:variable name="VAR1">
				<xsl:call-template name="AGEOFHOME_FACTOR" />
			</xsl:variable>
			<xsl:variable name="VAR2">
				<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT_1" />
			</xsl:variable>
			<xsl:variable name="VAR3">
				<xsl:call-template name="GET_DUC_DISCOUNT_TEMP" />
			</xsl:variable>
			<xsl:value-of select="(1.00 - $VAR1) + (1.00 - $VAR2) + (1.00 - $VAR3)" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$FINALCREDIT_DUC &gt; 0.50">
				<xsl:value-of select="$FINALCREDIT_DUC - 0.50" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="EXPERIENCE">
					<xsl:call-template name="EXPERIENCEFACTOR" />
				</xsl:variable>
				<xsl:variable name="FINALCREDIT_EXPERIENCE">
					<xsl:value-of select="$FINALCREDIT_DUC	+ (1.00 - $EXPERIENCE)" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$FINALCREDIT_EXPERIENCE = 0.00">
						<xsl:call-template name="EXPERIENCEFACTOR" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:choose>
							<xsl:when test="$FINALCREDIT_EXPERIENCE &gt; 0.50">
								<xsl:value-of select="($FINALCREDIT_EXPERIENCE - 0.50)  + $EXPERIENCE" />
							</xsl:when>
							<xsl:when test="$FINALCREDIT_EXPERIENCE &lt;= 0.50">
								<xsl:value-of select="$EXPERIENCE" />
							</xsl:when>
							<xsl:otherwise>1.00</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
		<!--including prot device, the credit becomes > 50% -->
	</xsl:template>
	<!-- HO-64 Renters Deluxe Endorsement -->
	<xsl:template name="DELUXE_ENDORSEMENT">
		<xsl:variable name="VAR_COVERAGE_C">
			<xsl:call-template name="COMBINEDPERSONALPROPERTY" />
		</xsl:variable>
		<xsl:variable name="VAR_LIMIT">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID = 'RENTERS_DELUX_ENDORSEMENT']/NODE[@ID='DELUX_ENDORSEMENT']/@MIN_LIMIT" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 and PRODUCT_PREMIER = $POLICYDESC_DELUXE and normalize-space(HO64RENTERDELUXE)='Y' and $VAR_COVERAGE_C &gt;= $VAR_LIMIT">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID = 'RENTERS_DELUX_ENDORSEMENT']/NODE[@ID='DELUX_ENDORSEMENT']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO6 and PRODUCT_PREMIER = $POLICYDESC_DELUXE and normalize-space(HO66CONDOMINIUMDELUXE)='Y' and $VAR_COVERAGE_C &gt;= $VAR_LIMIT">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID = 'RENTERS_DELUX_ENDORSEMENT']/NODE[@ID='DELUX_ENDORSEMENT']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="HO64_RENTERS_DELUXE_ENDORSEMENT">
		<xsl:choose>
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 and (PRODUCT_PREMIER = $POLICYDESC_TENANT or PRODUCT_PREMIER = $POLICYDESC_DELUXE) and normalize-space(HO64RENTERDELUXE)='Y'">Included</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--  HO-66 Condominium Deluxe Endorsement -->
	<xsl:template name="HO66_CONDOMINIUM_DELUXE_ENDORSEMENT">
		<xsl:choose>
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO6 and (PRODUCT_PREMIER = $POLICYDESC_UNIT or PRODUCT_PREMIER = $POLICYDESC_DELUXE) and normalize-space(HO66CONDOMINIUMDELUXE)='Y' and  normalize-space(HO32)='N' ">Included</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="COMBINE_COVERAGEA">
		<xsl:choose>
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO6"> <!-- for HO-6 and HO-6 deluxe -->
				<xsl:variable name="VAR1">
					<xsl:value-of select="PERSONALPROPERTYINCREASEDLIMITINCLUDE" />
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:value-of select="PERSONALPROPERTYINCREASEDLIMITADDITIONAL" />
				</xsl:variable>
				<xsl:value-of select="($VAR1 + $VAR2)" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="DWELLING_LIMITS" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="HO32_UNIT_OWNER">
		<xsl:choose>
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO6 and HO32 ='Y' and HO66CONDOMINIUMDELUXE ='N'"> <!-- for HO-6 products -->
				<xsl:variable name="HO32_MIN" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID = 'OPTIONALSECTIONCOVG']/NODE[@ID='HO_32_UNIT_OWNER']/ATTRIBUTES[@ID = '1']/@MIN" />
				<xsl:variable name="HO32_RATE_PER_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID = 'OPTIONALSECTIONCOVG']/NODE[@ID='HO_32_UNIT_OWNER']/ATTRIBUTES[@ID = '1']/@RATE_PER_VALUE" />
				<xsl:variable name="HO32_CHARGE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID = 'OPTIONALSECTIONCOVG']/NODE[@ID='HO_32_UNIT_OWNER']/ATTRIBUTES[@ID = '1']/@CHARGE" />
				<xsl:variable name="VAR_PERSONALPROPERTYINCREASEDLIMITADDITIONAL" select="PERSONALPROPERTYINCREASEDLIMITADDITIONAL" />
				<xsl:variable name="VAR_ADDITIONAL">
					<xsl:value-of select="(($VAR_PERSONALPROPERTYINCREASEDLIMITADDITIONAL div $HO32_RATE_PER_VALUE) * $HO32_CHARGE)" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$VAR_ADDITIONAL &lt; $HO32_MIN">
						<xsl:value-of select="$HO32_MIN" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$VAR_ADDITIONAL" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- HO-50 PERSONAL PROPERTY AWAY FROM PREMISES  -->
	<xsl:template name="HO50_PERSONAL_PROPERTY_AWAY_PREMISES">
		<xsl:variable name="PNAME" select="PRODUCTNAME"></xsl:variable>
		<xsl:variable name="PCOVERAGE_C_ADDITIONAL_VALUE" select="PERSONALPROPERTYAWAYADDITIONAL"></xsl:variable>
		<xsl:variable name="PCOVERAGE_C_ADDITIONAL_COST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = 'HO-50']/@COST"></xsl:variable>
		<xsl:variable name="PCOVERAGE_C_ADDITIONAL_INCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = 'HO-50']/@INCREASEDAMOUNT"></xsl:variable>
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_CALCULATION">
			<xsl:choose>
				<xsl:when test="$PCOVERAGE_C_ADDITIONAL_VALUE = 0">0</xsl:when>
				<xsl:otherwise>
					<xsl:variable name="VAR_ADDITIONALCOST">
						<xsl:value-of select="$PCOVERAGE_C_ADDITIONAL_COST" />
					</xsl:variable>
					<xsl:variable name="VAR_ADDITIONAL_COVERAGE">
						<xsl:value-of select="$PCOVERAGE_C_ADDITIONAL_VALUE"></xsl:value-of>
					</xsl:variable>
					<xsl:variable name="VAR_RATE_PER_VALUE">
						<xsl:value-of select="$PCOVERAGE_C_ADDITIONAL_INCREASEDAMOUNT"></xsl:value-of>
					</xsl:variable>
					<xsl:value-of select="round((($VAR_ADDITIONAL_COVERAGE div $VAR_RATE_PER_VALUE) * $VAR_ADDITIONALCOST)* $VAR_TERMFACTOR)" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:value-of select="$VAR_CALCULATION" />
	</xsl:template>
	<xsl:template name="HO51_BULDING_ALTER">
		<xsl:variable name="PNAME" select="PRODUCTNAME"></xsl:variable>
		<xsl:variable name="PBLDG_ALTERATION_ADDITIONAL" select="PERSONALPROPERTYINCREASEDLIMITADDITIONAL"></xsl:variable>
		<xsl:variable name="PHO51_RATE_PER_COST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = 'HO-51']/@COST"></xsl:variable>
		<xsl:variable name="PHO51_RATE_PER" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = 'HO-51']/@INCREASEDAMOUNT"></xsl:variable>
		<xsl:value-of select="($PBLDG_ALTERATION_ADDITIONAL div $PHO51_RATE_PER) * $PHO51_RATE_PER_COST" />
	</xsl:template>
	<xsl:template name="MEDICAL_CHARGES">
		<xsl:variable name="TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_FOR_EACH_ADDITION" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='PRIMARY_RESIDENCE']/@FOR_EACH_ADDITION" />
		<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
		<xsl:choose>
			<xsl:when test="MEDICALPAYMENTSTOOTHERS_LIMIT &gt; 0">
				<xsl:value-of select="((((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) div $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACH_ADDITION)* $TERMFACTOR)"></xsl:value-of>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="LIABLITY_CHARGES">
		<xsl:variable name="TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
		<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='DESCRIBED_PRIMARY_RESIDENCE']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
		<xsl:choose>
			<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0 and $VAR_LIABILITY_LIMIT_GP != 'Included'">
				<xsl:value-of select="$VAR_LIABILITY_LIMIT_GP * $TERMFACTOR" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="TERRITORY_CODES">
		<xsl:variable name="TCODES" select="TERRITORYCODES" />
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY']/NODE/ATTRIBUTES[@TID = $TCODES]/@GROUPID" />
	</xsl:template>
	<xsl:template name="REPAIR_COST_FACTOR">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='REPAIR_COST_FACTOR']/NODE[@ID='REPAIR_COST']/ATTRIBUTES/@FACTOR" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Templates for Replacement Cost [Factor,Credit,Display](SATRT)			  -->
	<!-- ============================================================================================ -->
	<!-- In the case of Replacement Cost depends on the percentage use -->
	<xsl:template name="REPLACEMENT_COST_FACTOR">
		<xsl:variable name="P_REPLACEMENT_COST">
			<xsl:value-of select="round((DWELLING_LIMITS div REPLACEMENTCOSTFACTOR)*100)" />
		</xsl:variable>
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='REPLACEMENT_COST_FACTOR']/NODE[@ID ='REPLACEMENT_COST']/ATTRIBUTES[@LOWER_PERCENTAGE_LIMIT &lt;= $P_REPLACEMENT_COST  and @UPPER_PERCENTAGE_LIMIT &gt;= $P_REPLACEMENT_COST]/@FACTOR" />
	</xsl:template>
	<!-- For Display Only -->
	<xsl:template name="REPLACEMENT_COST_DISPLAY">
		<xsl:variable name="VAR_REPLACEMENT_COST_FACTOR">
			<xsl:call-template name="REPLACEMENT_COST_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$VAR_REPLACEMENT_COST_FACTOR != 1.00">Applied</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="REPLACEMENT_COST_DISPLAY_TEXT">
		<xsl:text>-  Charge	- Insured to 80% to 99% of Replacement Cost</xsl:text>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Templates for Loss Assessment [Factor,Credit,Display](SATRT)			  -->
	<!-- ============================================================================================ -->
	<!-- In the case of Replacement Cost depends on the percentage use -->
	<xsl:template name="HO35_LOSS_ASSESSMENT">
		<xsl:variable name="VAR_HO35INCLUDE">
			<xsl:choose>
				<xsl:when test="normalize-space(HO35INCLUDE)='N/A' or normalize-space(HO35INCLUDE) = 'n/a' or normalize-space(HO35INCLUDE)=''">0.00</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="HO35INCLUDE" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_HO35ADDITIONAL">
			<xsl:choose>
				<xsl:when test="normalize-space(HO35ADDITIONAL)='N/A' or normalize-space(HO35ADDITIONAL) = 'n/a' or normalize-space(HO35ADDITIONAL)=''">0.00</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="HO35ADDITIONAL" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="P_HO35_LOSS_ASSESSMENT_AMOUNT" select="round($VAR_HO35ADDITIONAL)" />
		<xsl:variable name="P_HO35_RATE_PER_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='HO_35_LOSS_ASSESSMENT']/ATTRIBUTES/@RATE_PER_VALUE" />
		<xsl:variable name="P_HO35_RATE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='HO_35_LOSS_ASSESSMENT']/ATTRIBUTES/@RATE" />
		<xsl:value-of select="round(($P_HO35_LOSS_ASSESSMENT_AMOUNT div  $P_HO35_RATE_PER_VALUE) * $P_HO35_RATE )" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					To check if HO65 options are selected [Factor,Credit,Display](SATRT)			  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_65_ABOVE_0">
		<xsl:variable name="VAR_UNSCHEDULED_JEWELRY_ADDITIONAL">
			<xsl:call-template name="UNSCHEDULED_JEWELRY_ADDITIONAL" />
		</xsl:variable>
		<xsl:variable name="VAR_MONEY">
			<xsl:call-template name="MONEY" />
		</xsl:variable>
		<xsl:variable name="VAR_SECURITIES">
			<xsl:call-template name="SECURITIES" />
		</xsl:variable>
		<xsl:variable name="VAR_SILVERWARE_GOLDWARE">
			<xsl:call-template name="SILVERWARE_GOLDWARE" />
		</xsl:variable>
		<xsl:variable name="VAR_FIREARMS">
			<xsl:call-template name="FIREARMS" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="( $VAR_UNSCHEDULED_JEWELRY_ADDITIONAL != '' and  $VAR_UNSCHEDULED_JEWELRY_ADDITIONAL &gt; 0 ) 
						or (  $VAR_MONEY != '' and $VAR_MONEY &gt; 0 )
						or ($VAR_SECURITIES != '' and  $VAR_SECURITIES &gt; 0) 
						or ($VAR_SILVERWARE_GOLDWARE !='' and $VAR_SILVERWARE_GOLDWARE &gt; 0 )
						or ($VAR_FIREARMS !='' and $VAR_FIREARMS &gt; 0)">Y</xsl:when>
			<xsl:otherwise>N</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					To check if Inland marine options are present[Factor,Credit,Display](SATRT)			  -->
	<!-- ============================================================================================ -->
	<xsl:template name="DISPLAY_INLAND_MARINE_HEADING">
		<xsl:choose>
			<xsl:when test="( SCH_BICYCLE_AMOUNT != '' and  SCH_BICYCLE_AMOUNT &gt; 0 ) 
						or (  SCH_CAMERA_AMOUNT != '' and SCH_CAMERA_AMOUNT &gt; 0 )
						or (SCH_CELL_AMOUNT != '' and  SCH_CELL_AMOUNT &gt; 0) 
						or (SCH_FURS_AMOUNT !='' and SCH_FURS_AMOUNT &gt; 0 )
						or (SCH_GOLF_AMOUNT !='' and SCH_GOLF_AMOUNT &gt; 0)
						or  (SCH_GUNS_AMOUNT  !='' and  SCH_GUNS_AMOUNT &gt; 0) 
						or  (SCH_JWELERY_AMOUNT  !='' and SCH_JWELERY_AMOUNT &gt; 0) 
						or  (SCH_MUSICAL_AMOUNT  !='' and SCH_MUSICAL_AMOUNT  &gt; 0) 
						or  (SCH_PERSCOMP_AMOUNT  !='' and SCH_PERSCOMP_AMOUNT &gt; 0) 
						or  (SCH_SILVER_AMOUNT  !='' and  SCH_SILVER_AMOUNT &gt; 0) 
						or  (SCH_STAMPS_AMOUNT  !='' and SCH_STAMPS_AMOUNT &gt; 0) 
						or  (SCH_RARECOINS_AMOUNT  !='' and SCH_RARECOINS_AMOUNT &gt; 0) 
						or  (SCH_FINEARTS_WO_BREAK_AMOUNT  !='' and  SCH_FINEARTS_WO_BREAK_AMOUNT &gt; 0) 
						or  (SCH_FINEARTS_BREAK_AMOUNT  !='' and  SCH_FINEARTS_BREAK_AMOUNT &gt; 0) 
						or  (SCH_HANDICAP_ELECTRONICS_AMOUNT  !='' and  SCH_HANDICAP_ELECTRONICS_AMOUNT &gt; 0) 
						or  (SCH_HEARING_AIDS_AMOUNT  !='' and   SCH_HEARING_AIDS_AMOUNT &gt; 0) 
						or  (SCH_INSULIN_PUMPS_AMOUNT  !='' and  SCH_INSULIN_PUMPS_AMOUNT  &gt; 0) 
						or  (SCH_MART_KAY_AMOUNT  !='' and  SCH_MART_KAY_AMOUNT &gt; 0) 
						or  (SCH_PERSONAL_COMPUTERS_LAPTOP_AMOUNT  !='' and  SCH_PERSONAL_COMPUTERS_LAPTOP_AMOUNT  &gt; 0) 
						or  (SCH_SALESMAN_SUPPLIES_AMOUNT  !='' and  SCH_SALESMAN_SUPPLIES_AMOUNT &gt; 0) 
						or  (SCH_SCUBA_DRIVING_AMOUNT  !='' and SCH_SCUBA_DRIVING_AMOUNT &gt; 0) 
						or  (SCH_SNOW_SKIES_AMOUNT  !='' and  SCH_SNOW_SKIES_AMOUNT &gt; 0) 
						or  (SCH_TACK_SADDLE_AMOUNT  !='' and   SCH_TACK_SADDLE_AMOUNT  &gt; 0) 
						or  (SCH_TOOLS_PREMISES_AMOUNT  !='' and  SCH_TOOLS_PREMISES_AMOUNT &gt; 0) 
						or  (SCH_TOOLS_BUSINESS_AMOUNT  !='' and  SCH_TOOLS_BUSINESS_AMOUNT  &gt; 0) 
						or  (SCH_TRACTORS_AMOUNT  !='' and  SCH_TRACTORS_AMOUNT &gt; 0) 
						or  (SCH_TRAIN_COLLECTIONS_AMOUNT  !='' and  SCH_TRAIN_COLLECTIONS_AMOUNT &gt; 0) 
						or  (SCH_WHEELCHAIRS_AMOUNT  !='' and  SCH_WHEELCHAIRS_AMOUNT &gt; 0)">Y</xsl:when>
			<xsl:otherwise>N</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="COVERAGEADWELLING">
		<xsl:choose>
			<!-- Not applicable in HO-4 policy type -->
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4">0</xsl:when>
			<!-- HO-6 policy type -->
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO6">
				<xsl:variable name="VAR1">
					<xsl:call-template name="COVERAGE_A_PREMIUM_FOR_HO6" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$VAR1 = 0">ND</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$VAR1" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<!-- Applicable in all others -->
			<xsl:otherwise>
				{<xsl:call-template name="CALL_ADJUSTEDBASE"></xsl:call-template>}
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DELUXENDORSEMENTPREMIUM">
		<xsl:variable name="BASEPREMUIM">
			<xsl:call-template name="CALL_ADJUSTEDBASE" />
		</xsl:variable>
		<xsl:variable name="VAR_MEDICAL_CHARGES">
			<xsl:call-template name="MEDICAL_CHARGES" />
		</xsl:variable>
		<xsl:variable name="VAR_LIABLITY_CHARGES">
			<xsl:call-template name="LIABLITY_CHARGES" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="(PRODUCTNAME = $POLICYTYPE_HO4) or (PRODUCTNAME = $POLICYTYPE_HO6)">
				<xsl:value-of select="($BASEPREMUIM - ($VAR_MEDICAL_CHARGES + $VAR_LIABLITY_CHARGES))" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="COMPONENT_CODE">
		<xsl:param name="FACTORELEMENT" />
		<xsl:choose>
			<xsl:when test="$FACTORELEMENT=normalize-space('COV_A')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>3</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>134</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('COV_B')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>5</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>135</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('COV_C')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>7</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>136</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('COV_D')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>8</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>137</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('COV_E')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>10</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>170</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('COV_F')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>13</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>171</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('PR_LSS')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>29</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>138</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('S_WOOD_STV')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>31</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>139</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('PRFRD_PLS')">
				<xsl:choose>
					<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO5"></xsl:when>
					<xsl:otherwise>
						<xsl:choose>
							<xsl:when test="HO20 ='Y'">
								<xsl:if test="STATENAME='MICHIGAN'">
									<xsl:text>37</xsl:text>
								</xsl:if>
								<xsl:if test="STATENAME='INDIANA'">
									<xsl:text>141</xsl:text>
								</xsl:if>
							</xsl:when>
							<xsl:when test="HO21 ='Y'">
								<xsl:if test="STATENAME='MICHIGAN'">
									<xsl:text>53</xsl:text>
								</xsl:if>
							</xsl:when>
							<xsl:when test="HO22 ='Y'">
								<xsl:if test="STATENAME='INDIANA'">
									<xsl:text>142</xsl:text>
								</xsl:if>
							</xsl:when>
							<xsl:when test="HO23 ='Y'">
								<xsl:if test="STATENAME='MICHIGAN'">
									<xsl:text>55</xsl:text>
								</xsl:if>
							</xsl:when>
							<xsl:when test="HO24 ='Y'">
								<xsl:if test="STATENAME='INDIANA'">
									<xsl:text>143</xsl:text>
								</xsl:if>
							</xsl:when>
							<xsl:when test="HO25 ='Y'">
								<xsl:if test="STATENAME='MICHIGAN'">
									<xsl:text>196</xsl:text>
								</xsl:if>
							</xsl:when>
							<xsl:otherwise>0.00</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>				
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('RNT_DLX')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>92</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>165</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('CND_DLX')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>93</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>166</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('RPLC_CST')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>33</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>140</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('EXPND_RPLC_CST')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>56</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>144</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('FIRE_DPT')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>57</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>145</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('BSNS_PRP_INCR')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>58</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>146</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('OTHR_STR_RNT')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>62</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>148</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('PRS_PRP_AWY_PRM')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>66</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>150</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('BLDG_ALT')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>91</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>164</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('CRDT_CRD')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>73</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>153</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('UNSCD_JWL_FUR')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>74</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>154</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('MONEY')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>189</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>188</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('SECURITY')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>191</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>190</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('SLVR_GLD')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>193</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>192</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('FIRE_ARM')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>195</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>194</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('LSS_ASMNT')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>89</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>162</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('ORD_LW')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>79</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>156</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('UNT_OWNR_CVG_A')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>87</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>160</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('ID_FRAUD')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>84</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>158</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('ERTHQKE')">
				<xsl:choose>
				<xsl:when test="STATENAME='MICHIGAN'">
				<xsl:if test="PRODUCTNAME='$POLICYTYPE_HO2' and PRODUCT_PREMIER='$POLICYDESC_REPAIR'">
					<xsl:text>905</xsl:text>
				</xsl:if>
				<xsl:if test="PRODUCTNAME='$POLICYTYPE_HO2' and PRODUCT_PREMIER='$POLICYDESC_REPLACEMENT'">
					<xsl:text>80</xsl:text>
				</xsl:if>
				<xsl:if test="PRODUCTNAME='$POLICYTYPE_HO3' and PRODUCT_PREMIER='$POLICYDESC_REPAIR'">
					<xsl:text>80</xsl:text>
				</xsl:if>
				<xsl:if test="PRODUCTNAME='$POLICYTYPE_HO3' and PRODUCT_PREMIER='$POLICYDESC_REPLACEMENT'">
					<xsl:text>80</xsl:text>
				</xsl:if>
				<xsl:if test="PRODUCTNAME='$POLICYTYPE_HO3' and PRODUCT_PREMIER='$POLICYDESC_PREMIER'">
					<xsl:text>80</xsl:text>
				</xsl:if>
				<xsl:if test="PRODUCTNAME='$POLICYTYPE_HO4' and PRODUCT_PREMIER='$POLICYDESC_TENANT'">
					<xsl:text>80</xsl:text>
				</xsl:if>
				<xsl:if test="PRODUCTNAME='$POLICYTYPE_HO4' and PRODUCT_PREMIER='$POLICYDESC_DELUXE'">
					<xsl:text>80</xsl:text>
				</xsl:if>
				<xsl:if test="PRODUCTNAME='$POLICYTYPE_HO5' and PRODUCT_PREMIER='$POLICYDESC_REPLACEMENT'">
					<xsl:text>905</xsl:text>
				</xsl:if>
				<xsl:if test="PRODUCTNAME='$POLICYTYPE_HO5' and PRODUCT_PREMIER='$POLICYDESC_PREMIER'">
					<xsl:text>905</xsl:text>
				</xsl:if>
				<xsl:if test="PRODUCTNAME='$POLICYTYPE_HO6' and PRODUCT_PREMIER='$POLICYDESC_UNIT'">
					<xsl:text>80</xsl:text>
				</xsl:if>
				<xsl:if test="PRODUCTNAME='$POLICYTYPE_HO6' and PRODUCT_PREMIER='$POLICYDESC_DELUXE'">
					<xsl:text>80</xsl:text>
				</xsl:if>
				</xsl:when>
				<xsl:when test="STATENAME='INDIANA'">
				<xsl:if test="PRODUCTNAME='$POLICYTYPE_HO2' and PRODUCT_PREMIER='$POLICYDESC_REPAIR'">
					<xsl:text>157</xsl:text>
				</xsl:if>
				<xsl:if test="PRODUCTNAME='$POLICYTYPE_HO2' and PRODUCT_PREMIER='$POLICYDESC_REPLACEMENT'">
					<xsl:text>157</xsl:text>
				</xsl:if>
				<xsl:if test="PRODUCTNAME='$POLICYTYPE_HO3' and PRODUCT_PREMIER='$POLICYDESC_REPAIR'">
					<xsl:text>157</xsl:text>
				</xsl:if>
				<xsl:if test="PRODUCTNAME='$POLICYTYPE_HO3' and PRODUCT_PREMIER='$POLICYDESC_REPLACEMENT'">
					<xsl:text>157</xsl:text>
				</xsl:if>
				<xsl:if test="PRODUCTNAME='$POLICYTYPE_HO4' and PRODUCT_PREMIER='$POLICYDESC_TENANT'">
					<xsl:text>157</xsl:text>
				</xsl:if>
				<xsl:if test="PRODUCTNAME='$POLICYTYPE_HO4' and PRODUCT_PREMIER='$POLICYDESC_DELUXE'">
					<xsl:text>157</xsl:text>
				</xsl:if>
				<xsl:if test="PRODUCTNAME='$POLICYTYPE_HO5' and PRODUCT_PREMIER='$POLICYDESC_REPLACEMENT'">
					<xsl:text>904</xsl:text>
				</xsl:if>
				<xsl:if test="PRODUCTNAME='$POLICYTYPE_HO6' and PRODUCT_PREMIER='$POLICYDESC_UNIT'">
					<xsl:text>157</xsl:text>
				</xsl:if>
				<xsl:if test="PRODUCTNAME='$POLICYTYPE_HO6' and PRODUCT_PREMIER='$POLICYDESC_DELUXE'">
					<xsl:text>157</xsl:text>
				</xsl:if>
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('BK_UP_SM_PMP')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>198</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>197</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('CND_UNIT')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>88</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>161</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('WRKR_CMP')">
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>917</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('MNE_SBS')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>112</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>169</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('SUB_SRFCE_WTR')">
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>814</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('SPCFC_STR_PRMS')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>257</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>172</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('OTHR_STR_RPR_CST')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>69</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>152</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('RESI_EMPL')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>277</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>276</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('ADDL_PRM_OCC_INS')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>259</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>258</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('ADDL_PRM_RNTD_OTH_RES')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>260</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>261</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('ADDL_PRM_RNTD_1FMLY')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>948</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>947</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('ADDL_PRM_RNTD_2FMLY')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>950</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>949</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('INCDT_OFCE_PRV_SCHL_PRM')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>267</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>266</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('INCDT_OFCE_PRV_SCHL_INST')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>271</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>270</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('WTRBD_LBLTY')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>813</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>812</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('INCDT_OFCE_PRV_SCHL_OFF_PRM')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>273</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>272</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('CLASS_A')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>278</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>279</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('CLASS_B')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>281</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>280</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('CLASS_C')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>283</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>282</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('CLASS_D')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>285</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>284</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('INCT_FRMG_RESI_PRM')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>287</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>286</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('INCT_FRMG_RNTD_OTH')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>291</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>290</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('INCT_FRMG_OPRT_INS')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>289</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>288</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('PRS_INJRY')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>275</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>274</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('BICYCLE')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>874</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>846</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('CMRA')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>875</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>847</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('CELL')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>876</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>848</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('FUR')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>879</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>851</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('GOLF')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>880</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>852</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('GUN')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>881</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>853</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('JWL')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>885</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>857</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('MSC_NON_PRF')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>885</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>857</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('PRS_CMP')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>888</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>860</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('SLVR')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>893</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>865</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('STMP')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>895</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>867</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('RARE_COIN')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>890</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>862</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('FNE_ART_WO_BRK')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>878</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>850</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('FNE_ART_WTH_BRK')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>877</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>849</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('HNDCP_ELCT')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>882</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>854</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('HRNG_AD')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>883</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>855</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('INSLN_PMP')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>884</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>856</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('MRT_KY_AMWY')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>886</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>858</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('PC_LPTP')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>889</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>861</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('SLMN_SPPLY')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>891</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>863</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('SCB_DVNG_EQP')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>892</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>864</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('SNW_SKY')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>894</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>866</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('TCK_SDDL')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>896</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>868</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('TOOLS_PRM')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>897</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>869</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('TOOLS_BSNS')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>898</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>870</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('TRCTR')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>899</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>871</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('TRN_CLLCTN')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>900</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>872</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('WHLCHR')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>901</xsl:text>
				</xsl:if>
				<xsl:if test="STATENAME='INDIANA'">
					<xsl:text>873</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('PRP_EXPNS_FEE')">
				<xsl:if test="STATENAME='MICHIGAN'">
					<xsl:text>996</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>
