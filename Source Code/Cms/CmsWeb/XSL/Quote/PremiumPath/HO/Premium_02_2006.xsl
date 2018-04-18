<!-- ============================================================================================ -->
<!-- File Name		:	Primium.xsl																  -->
<!-- Description	:	This xsl file is for generating 
						the Final Primium (For all Homeowner Products)INDIANA and MICHIGAN		  -->
<!-- Developed By	:	Shrikant Bhatt															  -->		
<!-- ============================================================================================ -->

<!--xmlns:xinclude="http://www.w3.org/2005/xpath-functions" <xsl:namespace-alias stylesheet-prefix="xinclude" result-prefix="fn"></xsl:namespace-alias>-->
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"  version="1.0">
<xsl:output encoding="ISO8859-1" omit-xml-declaration="no" method="xml"/>

<!-- ============================================================================================ -->
<!--								Loading ProductFactorMaster File (START)					  -->
<!-- ============================================================================================ -->

<xsl:variable name="HOCoveragesDoc" select="document('FactorPath')"></xsl:variable>

<!-- ============================================================================================ -->
<!--								Loading ProductFactorMaster File (END)						  -->
<!-- ============================================================================================ -->

<xsl:template match="/">
	<xsl:apply-templates select="DWELLINGDETAILS"/>
</xsl:template>

<!-- ============================================================================================ -->
<!--								DEWLLINGDETAILS Template(START)								  -->
<!-- ============================================================================================ -->

<xsl:template match="DWELLINGDETAILS">
<PREMIUM>
	<CREATIONDATE></CREATIONDATE>
	<GETPATH>
		<PRODUCT PRODUCTID="0" DESC="Policy Form: HO-3,  1 Family"> 
			<GROUP GROUPID="0">
				<GROUPCONDITION>
					<CONDITION>
					</CONDITION>
				</GROUPCONDITION> 
			</GROUP> 	
			<GROUP GROUPID="1">
				<GROUPCONDITION>
					<CONDITION>
					</CONDITION>
				</GROUPCONDITION> 
			</GROUP> 
			<GROUP GROUPID="2"> 
				<GROUPCONDITION>
					<CONDITION>
					</CONDITION>
				</GROUPCONDITION> 
			</GROUP> 	
			<GROUP GROUPID="3"> 
				<GROUPCONDITION>
					<CONDITION>	</CONDITION>
				</GROUPCONDITION> 
				<SUBGROUP>
					<IF>
						<STEP STEPID="0">
							<xsl:choose> 
									<xsl:when test="PRODUCTNAME = 'HO-4'">  
										0
									</xsl:when>
									 
									<xsl:when test="PRODUCTNAME = 'HO-6'">  
										<xsl:variable name="VAR1"> 
											<xsl:call-template name="COVERAGE_A_PREMIUM_FOR_HO6"></xsl:call-template>
										</xsl:variable>
										<xsl:choose>
										<xsl:when test="$VAR1 = 0">
											ND
										</xsl:when>
										<xsl:otherwise><xsl:value-of select="$VAR1"/></xsl:otherwise>
										</xsl:choose>
									</xsl:when>
									
									 
									<xsl:otherwise>
										{<xsl:call-template name="CALL_ADJUSTEDBASE"></xsl:call-template>}
									</xsl:otherwise>
								</xsl:choose>
						  
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				
				<SUBGROUP>
					<STEP STEPID="1">
						<PATH><!--<xsl:call-template name="COVERAGE_B_PREMIUM"></xsl:call-template>-->
						<xsl:choose> 
									<xsl:when test="PRODUCTNAME = 'HO-6' or PRODUCTNAME = 'HO-4'">  
										0
									</xsl:when>
									
									<xsl:otherwise>
										ND
									</xsl:otherwise>
								</xsl:choose>
						 </PATH>
					</STEP>
				</SUBGROUP>
					<IF>
						<STEP STEPID="2">
							<PATH>
							<xsl:choose> 
									<xsl:when test="PRODUCTNAME = 'HO-4' or PRODUCTNAME = 'HO-6'">  
										{<xsl:call-template name="CALL_ADJUSTEDBASE"></xsl:call-template>}
									</xsl:when>
								<xsl:otherwise>ND</xsl:otherwise>  
								</xsl:choose> 
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="3">
							<PATH>
							<xsl:variable name="VAR1">  
									<xsl:call-template name="COVERAGE_D_PREMIUM"/>
								</xsl:variable>
								
										<xsl:choose> 
											<xsl:when test="PRODUCTNAME = 'HO-6'"> 
													<xsl:choose>
														<xsl:when test="$VAR1 = '0'"> ND </xsl:when>
														<xsl:otherwise><xsl:value-of select="$VAR1"/> </xsl:otherwise>
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
					<CONDITION>
					</CONDITION>
				</GROUPCONDITION>
				<SUBGROUP>
					<STEP STEPID="4"> 
						<PATH>ND</PATH>
					</STEP> 		
				</SUBGROUP>
				<SUBGROUP>
					<STEP STEPID="5"> 
						<PATH><!--<xsl:call-template name ="Coverage_F_Limit"></xsl:call-template>-->ND</PATH>
					</STEP> 		
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="6">
							<PATH><xsl:call-template name ="INSURANCE_SCORE_CREDIT_DISPLAY"/></PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				<!-- New Steps Added START-->
				<SUBGROUP>
					<IF>
						<STEP STEPID="7">
							<PATH>
								<xsl:call-template name ="SMOKER_DISPLAY"/>
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="8">
							<PATH>
								<xsl:call-template name ="MAX_DISCOUNT_APPLIED_DISPLAY"/>
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				<!-- New Steps Added START-->
				<SUBGROUP>
					<STEP STEPID="9">
						<PATH>
							<!--<xsl:call-template name ="DWELLING_UNDER_CONSTRUCTION"></xsl:call-template>-->
							<xsl:call-template name ="DWELLING_UNDER_CONSTRUCTION_DISPLAY"></xsl:call-template>
						</PATH>
					</STEP>
				</SUBGROUP>
				<SUBGROUP>
					<STEP STEPID="10">
						<PATH>
							<!--<xsl:value-of select= "EXPERIENCE"></xsl:value-of>-->
							<xsl:call-template name ="EXPERIENCEFACTOR_DISPLAY"></xsl:call-template>
						</PATH>
					</STEP>
				</SUBGROUP>
				<!-- New Steps START-->
				<SUBGROUP>
					<STEP STEPID="11"> 
							<PATH>
									<xsl:call-template name="AGEOFHOME_DISPLAY"></xsl:call-template>
							</PATH>
						</STEP>		 	
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="12"> 
							<PATH>
								{
									<xsl:call-template name="PROTECTIVEDEVICE_DISPLAY_TEMP"></xsl:call-template>
								}
							</PATH>
						</STEP>		 	
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<STEP STEPID="13"> 
						<PATH>
							<xsl:call-template name="MULTIPOLICY_DISPLAY"></xsl:call-template>
						</PATH>
					</STEP>		 	
				</SUBGROUP>
				<!-- New Steps END-->
				<SUBGROUP>
					<STEP STEPID="14"> 
						<PATH>
							<xsl:call-template name="VALUEDCUSTOMER_DISPLAY"></xsl:call-template>
						</PATH>
					</STEP>		 	
				</SUBGROUP>
				<SUBGROUP>
					<STEP STEPID="15">
						<PATH>
							<xsl:call-template name="REPLACEMENT_COST_DISPLAY"></xsl:call-template>
						</PATH>
					</STEP>
				</SUBGROUP>
				<SUBGROUP>
					<STEP STEPID="16"> 
						<PATH>
							<xsl:call-template name="PRIOR_LOSS_DISPLAY"></xsl:call-template>
						</PATH>
					</STEP>					
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="17"> 
							<PATH>
								<xsl:call-template name="WSTOVE_DISPLAY"></xsl:call-template>
							</PATH>
						</STEP>					
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="18"> 
							<PATH>
							{
								IF(<xsl:call-template name="BREEDDOG"/> = 1.00) THEN
									0
								ELSE
									<xsl:call-template name="BREEDDOG"/>
								
							}
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="19"> 
							<PATH>
								<xsl:call-template name="SEASONALSECONDARY_DISPLAY"></xsl:call-template>
							</PATH>
						</STEP>					
					</IF>
				</SUBGROUP>
			</GROUP> 
			<GROUP GROUPID="5" > 
				<GROUPCONDITION>
					<CONDITION>
					</CONDITION>
				</GROUPCONDITION> 
				<SUBGROUP>
					<STEP STEPID="20" > 
						<PATH>
							<xsl:call-template name ="PREFERRED_PLUS_DISPLAY"></xsl:call-template>
						</PATH>
					</STEP> 
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="21"> 
							<PATH>
								{   <!--SELECT_HO_34-->
									<!--<xsl:call-template name ="HO_34_REPLACEMENT_COST"></xsl:call-template>-->
									<xsl:call-template name ="SELECT_HO_34_DISPLAY"></xsl:call-template>
								}	
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				<!-- NEW STEP START --> 
				<SUBGROUP>
					<IF>
						<STEP STEPID="22"> 
							<PATH>
								{
									<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY_DISPLAY"></xsl:call-template>
								}	
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="23"> 
							<PATH>
								{
									<xsl:call-template name ="HO_96_REPLACEMENT_COST_PERSONAL_PROPERTY"></xsl:call-template>
								}	
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="24"> 
							<PATH>
								{
									<xsl:call-template name ="HO_312_BUSINESS_PROPERTY_INCREASED_LIMITS"></xsl:call-template>
								}	
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="25"> 
							<PATH>
								{
									<xsl:call-template name ="HO_48_OTHER_STRUCTURE_INCREASED_LIMITS"></xsl:call-template>
								}	
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="26"> 
							<PATH>
								{
									<xsl:call-template name ="HO_40_OTHER_STRUCTURE_RENTED_TO_OTHERS"></xsl:call-template>
								}	
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="27"> 
							<PATH>
								{
									<xsl:call-template name ="HO_42_OTHER_STRUCTURE_WITH_INCIDENTAL"></xsl:call-template>
								}	
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="28"> 
							<PATH>
								{
									<xsl:call-template name ="HO_53_INCREASED_CREDIT_CARD"></xsl:call-template>
								}	
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="29"> 
							<PATH>
								{
									<xsl:call-template name ="UNSCHEDULED_JEWELRY_ADDITIONAL"></xsl:call-template>
								}	
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="30"> 
							<PATH>
								{
									<xsl:call-template name ="MONEY"></xsl:call-template>
								}	
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="31"> 
							<PATH>
								{
									<xsl:call-template name ="SECURITIES"></xsl:call-template>
								}	
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="32"> 
							<PATH>
								{
									<xsl:call-template name ="SILVERWARE_GOLDWARE"></xsl:call-template>
								}	
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="33"> 
							<PATH>
								{
									<xsl:call-template name ="FIREARMS"></xsl:call-template>
								}	
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				<!-- NEW STEP END   -->
				<SUBGROUP>
					<IF>
						<STEP STEPID="34"> 
							<PATH>
								{
									<xsl:call-template name ="ORDINANCE"></xsl:call-template>
								}
							</PATH>
						</STEP> 
					</IF>			
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="35" > 
							<PATH>
								{
									<xsl:call-template name ="HO_455_IDENTITY_FRAUD"/>
								}
							</PATH>
						</STEP> 			
					</IF>
				</SUBGROUP>
				<SUBGROUP>
				<IF>
					<STEP STEPID="36"> 
						<PATH>
							{	
								<xsl:call-template name ="HO_315_EARTHQUAKE"></xsl:call-template>
							}
						</PATH>
					</STEP> 			
				</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="37" > 
							<PATH>
								{
									<xsl:call-template name ="HO_327_WATER_BACK_UP"></xsl:call-template>
								}
							</PATH>
						</STEP> 			
					</IF>
				</SUBGROUP>
				<SUBGROUP><!-- New-->
					<IF>
						<STEP STEPID="38" > 
							<PATH>
								{
									<xsl:call-template name ="HO_33_CONDO_UNIT"></xsl:call-template>
								}
							</PATH>
						</STEP> 			
					</IF>
				</SUBGROUP>
				<!-- NEW STEP START -->
				<SUBGROUP>
					<IF>
						<STEP STEPID="39" > 
							<PATH>'Included'</PATH>
						</STEP> 			
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="40" > 
							<PATH>
								<xsl:call-template name ="HO_287_MINE_SUBSIDENCE_COVERAGE"></xsl:call-template>
							</PATH>
						</STEP> 			
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="41" >
							<PATH>
								{
									<xsl:call-template name ="HO_9_COLLAPSE_FROM_SUB_SURFACE_WATER"></xsl:call-template>
								}
							</PATH>
						</STEP> 			
					</IF>
				</SUBGROUP>
				<!-- NEW STEP END--> 
				<SUBGROUP>
					<IF>
						<STEP STEPID="42" > 
							<PATH>
								{
									<xsl:call-template name ="HO_490_SPECIFIC_STRUCTURED"></xsl:call-template>
								}
							</PATH>
						</STEP> 			
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="43" > 
							<PATH>
								{
									<xsl:call-template name ="HO_489_SPECIFIC_STRUCTURED"></xsl:call-template>
								}
							</PATH>
						</STEP> 			
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="44" > 
							<PATH>
								{
									<xsl:call-template name ="PRIMARY_RESIDENCE"></xsl:call-template>
								}
							</PATH>
						</STEP> 			
					</IF>
				</SUBGROUP>
				
				<SUBGROUP>
					<IF>
						<STEP STEPID="45"> 
							<PATH>
								{
									<xsl:call-template name ="RESIDENCE_EMPLOYEES"></xsl:call-template>
								}
							</PATH>
						</STEP> 
					</IF>			
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="46"> 
							<PATH>
								{
									<xsl:call-template name ="ADDITIONAL_PREMISES"></xsl:call-template>
								}
							</PATH>
						</STEP>
					</IF> 			
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="47" > 
							<PATH>
								{
									<xsl:call-template name ="ADDITIONAL_RESIDENCE_PREMISES"></xsl:call-template>
								}
							</PATH>
						</STEP> 			
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="48"> 
							<PATH>
								{
									<xsl:call-template name ="ADDITIONAL_OTHER_LOCATION_PREMISES_FAMILY1"></xsl:call-template>
								}
							</PATH>
						</STEP> 			
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="49" > 
							<PATH>
								{
									<xsl:call-template name ="ADDITIONAL_OTHER_LOCATION_PREMISES_FAMILY2"></xsl:call-template>
								}
							</PATH>
						</STEP> 			
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="50" > 
							<PATH>
								{
									<xsl:call-template name ="HO_42_ON_PREMISES"></xsl:call-template>
								}
							</PATH>
						</STEP> 
					</IF>			
				</SUBGROUP>
				<SUBGROUP><!-- SKB-->
					<IF>
						<STEP STEPID="51" > 
							<PATH>
								{
									<xsl:call-template name ="HO42_BUSINESS_OTHER_STRUCTURE_WITH_INCIDENTAL"></xsl:call-template>
								}
							</PATH>
						</STEP> 
					</IF>			
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="52" > 
							<PATH>
								{
									<xsl:call-template name ="HO_42_INSTRUCTION_ONLY"></xsl:call-template>
								}
							</PATH>
						</STEP> 			
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="53" > 
							<PATH>
								{
									<xsl:call-template name ="HO_200_WATERBED_LIABILITY"></xsl:call-template>
								}
							</PATH>
						</STEP> 			
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="54" > <!-- New -->
							<PATH>
								{
									<xsl:call-template name ="HO_43_OFF_PREMISES"></xsl:call-template> 
								}
							</PATH>
						</STEP> 			
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<STEP STEPID="55" > 
						<PATH>0</PATH>
					</STEP> 			
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="56" > 
							<PATH>
								{
									<xsl:call-template name ="HO_71_CLASSA"></xsl:call-template>
								}
							</PATH>
						</STEP> 			
					</IF>
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="57"> 
							<PATH>
								{
									<xsl:call-template name ="HO_71_CLASSB"></xsl:call-template>
								}
							</PATH>
						</STEP> 
					</IF>			
				</SUBGROUP>
				<SUBGROUP>
					<IF>
						<STEP STEPID="58"> 
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
						<STEP STEPID="59" > 
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
						<STEP STEPID="60" > 
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
						<STEP STEPID="61" > 
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
						<STEP STEPID="62" > 
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
						<STEP STEPID="63">
							<PATH>
								{
									<xsl:call-template name="HO_82_PERSONAL_INJURY"></xsl:call-template> 									
								}
							</PATH>
						</STEP> 
					</IF> 			
				</SUBGROUP>
			<!-- Group 6-->
			</GROUP>
			<GROUP GROUPID="6" > 
				<GROUPCONDITION>
				<CONDITION>
				</CONDITION>
				</GROUPCONDITION> 
				<SUBGROUP>
					<IF>
						<STEP STEPID="64" > 
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
						<STEP STEPID="65"> 
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
						<STEP STEPID="66" > 
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
						<STEP STEPID="67"> 
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
						<STEP STEPID="68"> 
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
						<STEP STEPID="69"> 
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
						<STEP STEPID="70"> 
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
						<STEP STEPID="71"> 
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
						<STEP STEPID="72" > 
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
						<STEP STEPID="73"> 
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
						<STEP STEPID="74" > 
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
						<STEP STEPID="75" > 
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
						<STEP STEPID="76" > 
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
						<STEP STEPID="77"> 
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
						<STEP STEPID="78"> 
							<PATH>
								{
									<xsl:call-template name="PROPFEE"></xsl:call-template>
								}
							</PATH>
						</STEP> 	
					</IF>
				</SUBGROUP>
				
				<SUBGROUP>
						<STEP STEPID="79"> 
							<PATH>
								   <!-- HO-64 Renters Deluxe Endorsement -->
									 
									  <xsl:call-template name ="HO64_Renters_Deluxe_Endorsement"></xsl:call-template>  
								 	
							</PATH>
						</STEP>
				</SUBGROUP>
				
				
				<SUBGROUP>
					<IF>
						<STEP STEPID="80"> 
							<PATH>
									  <xsl:call-template name="HO66_Condominium_Deluxe_Endorsement"></xsl:call-template>  
							</PATH>
						</STEP> 	
					</IF>
				</SUBGROUP>
				
				
				<SUBGROUP>
					<IF>
						<STEP STEPID="81"> 
							<PATH>
									  <xsl:call-template name="HO32_UNIT_OWNER"></xsl:call-template>  
							</PATH>
						</STEP> 	
					</IF>
				</SUBGROUP>
				
				<SUBGROUP>
					<IF>
						<STEP STEPID="82"> 
							<PATH>
									  <xsl:call-template name="HO50_PERSONAL_PROPERTY_AWAY_PREMISES"></xsl:call-template>  
							</PATH>
						</STEP> 	
					</IF>
				</SUBGROUP>
				
				<SUBGROUP>
					<IF>
						<STEP STEPID="83"> 
							<PATH>
								{
								<xsl:choose> 
									<xsl:when test="PRODUCTNAME = 'HO-4'">  
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
						<STEP STEPID="84"> 
							<PATH>
									  <xsl:call-template name="FINALPRIMIUM"></xsl:call-template>  
							</PATH>
						</STEP> 	
					</IF>
				</SUBGROUP>
				
				<!--<SUBGROUP>
					<IF>
					
						<STEP STEPID = "72">
							<PATH>ND</PATH><xsl:call-template name="GET_TOTAL_DISCOUNT"></xsl:call-template>
							
						</STEP>
					</IF>
				</SUBGROUP>-->
			</GROUP> 		 		
		</PRODUCT>
	</GETPATH>
	<CALCULATION>
		<PRODUCT PRODUCTID="0">
			<xsl:attribute name="DESC"><xsl:call-template name="PRODUCTID0"></xsl:call-template></xsl:attribute> 
			<GROUP GROUPID="0"  CALC_ID="10000"> 
			<xsl:attribute name="DESC"><xsl:call-template name="GROUPID0"></xsl:call-template></xsl:attribute> 
				<GROUPCONDITION>
					<CONDITION>
					</CONDITION>
				</GROUPCONDITION> 
				<GROUPRULE></GROUPRULE>
				<GROUPFORMULA></GROUPFORMULA>
				<GDESC></GDESC>
			</GROUP> 	
			<GROUP GROUPID="1"  CALC_ID="10001"> 
			<xsl:attribute name="DESC"><xsl:call-template name="GROUPID1"></xsl:call-template></xsl:attribute> 
				<GROUPCONDITION>
					<CONDITION>
					</CONDITION>
				</GROUPCONDITION> 
				<GROUPRULE></GROUPRULE>
				<GROUPFORMULA></GROUPFORMULA>
				<GDESC></GDESC>
			</GROUP> 
			<GROUP GROUPID="2"  CALC_ID="10002"> 
				<xsl:attribute name="DESC"><xsl:call-template name="GROUPID2"></xsl:call-template></xsl:attribute> 
				<GROUPCONDITION>
					<CONDITION></CONDITION>
				</GROUPCONDITION> 
				<GROUPRULE></GROUPRULE>
				<GROUPFORMULA></GROUPFORMULA>
				<GDESC></GDESC>
			</GROUP> 	
			<GROUP GROUPID="3"  CALC_ID="10003" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
			<xsl:attribute name="DESC"><xsl:call-template name="GROUPID3"></xsl:call-template></xsl:attribute> 
				<GROUPCONDITION>
					<CONDITION></CONDITION>
				</GROUPCONDITION> 	
				<GROUPRULE>	</GROUPRULE>
				<GDESC></GDESC>			
				<STEP STEPID="0"  CALC_ID="1000" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID0"></xsl:call-template></xsl:attribute>
					<PATH>P</PATH>
					<D_PATH><xsl:value-of select="DEDUCTIBLE"></xsl:value-of></D_PATH>
					
				    <L_PATH><xsl:variable name="VAR1"><xsl:value-of select="COMBINE_COVERAGEA"/></xsl:variable><xsl:value-of select="format-number($VAR1, '###,###')" /></L_PATH>
						
				</STEP>
				<STEP STEPID="1"  CALC_ID="1001" PREFIX="" SUFIX ="" ISCALCULATE="F">
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID1"></xsl:call-template></xsl:attribute>
					<PATH></PATH>
					<D_PATH><xsl:value-of select="DEDUCTIBLE"></xsl:value-of></D_PATH>
					
					<L_PATH><xsl:variable name="VAR1"><xsl:value-of select="HO48INCLUDE"/></xsl:variable><xsl:value-of select="format-number($VAR1, '###,###')" /></L_PATH>
					
				</STEP>
				<STEP STEPID="2"  CALC_ID="1002" PREFIX="" SUFIX ="" ISCALCULATE="F">
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID2"></xsl:call-template></xsl:attribute>
					<PATH>P</PATH>
					<D_PATH><xsl:value-of select="DEDUCTIBLE"/></D_PATH>
					<L_PATH><xsl:variable name="VAR1"><xsl:call-template name="COMBINEDPERSONALPROPERTY"/></xsl:variable><xsl:value-of select="format-number($VAR1, '###,###')" /></L_PATH>
				</STEP>
				
				<STEP STEPID="3"  CALC_ID="1003" PREFIX="" SUFIX ="" ISCALCULATE="F">
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID3"></xsl:call-template></xsl:attribute>
					<PATH>P</PATH>
					<D_PATH><xsl:value-of select="DEDUCTIBLE"></xsl:value-of></D_PATH>
					<L_PATH><xsl:variable name="VAR1">  <xsl:call-template name="COMBINEDADDITIONALLIVINGEXPENSE"/>   </xsl:variable><xsl:value-of select="format-number($VAR1, '###,###')" /></L_PATH>
					
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
			<xsl:attribute name="DESC"><xsl:call-template name ="GROUPID4"></xsl:call-template></xsl:attribute>
					<GROUPCONDITION>
						<CONDITION>
						</CONDITION>
					</GROUPCONDITION> 
					<GROUPRULE></GROUPRULE>
					<GDESC></GDESC>
					<STEP STEPID="4"  CALC_ID="1004" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
					<xsl:attribute name="DESC"><xsl:call-template name ="STEPID4"></xsl:call-template></xsl:attribute>
						<PATH>P</PATH>
						<D_PATH></D_PATH>
						
						<L_PATH><xsl:variable name="VAR1"><xsl:value-of select="PERSONALLIABILITY_LIMIT"/></xsl:variable><xsl:value-of select="format-number($VAR1, '###,###')" /></L_PATH>
						
					</STEP>
					<STEP STEPID="5"  CALC_ID="1005" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
					<xsl:attribute name="DESC"><xsl:call-template name ="STEPID5"></xsl:call-template></xsl:attribute>
						<PATH>0</PATH>
						<D_PATH></D_PATH>
						<L_PATH><xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"/></L_PATH>
						
						<L_PATH><xsl:variable name="VAR1"><xsl:value-of select="MEDICALPAYMENTSTOOTHERS_LIMIT"/></xsl:variable><xsl:value-of select="format-number($VAR1, '###,###')" /></L_PATH>
						
					</STEP>
					 <STEP STEPID="6"  CALC_ID="1006" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
					 <xsl:attribute name="DESC"><xsl:call-template name ="STEPID6"></xsl:call-template></xsl:attribute>
						<PATH>0</PATH>
						<D_PATH></D_PATH>
						<L_PATH></L_PATH>
					</STEP>	
					<!-- New Steps Added START-->
					<STEP STEPID="7"  CALC_ID="1007" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
					<xsl:attribute name="DESC"><xsl:call-template name ="STEPID7"></xsl:call-template></xsl:attribute>
						<PATH>0</PATH>
						<D_PATH></D_PATH>
						<L_PATH></L_PATH>
					</STEP>	 
					<STEP STEPID="8"  CALC_ID="1008" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
					<xsl:attribute name="DESC"><xsl:call-template name ="STEPID8"></xsl:call-template></xsl:attribute>
						<PATH>0</PATH>
						<D_PATH></D_PATH>
						<L_PATH></L_PATH>
					</STEP>
					<!-- New Steps Added END-->
					<STEP STEPID="9"  CALC_ID="1009" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
					<xsl:attribute name="DESC"><xsl:call-template name ="STEPID9"></xsl:call-template></xsl:attribute>
						<PATH>0</PATH>
						<D_PATH></D_PATH>
						<L_PATH></L_PATH>
					</STEP>	 
					<STEP STEPID="10"  CALC_ID="1010" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
					<xsl:attribute name="DESC"><xsl:call-template name ="STEPID10"></xsl:call-template></xsl:attribute>
						<PATH>0</PATH>
						<D_PATH></D_PATH>
						<L_PATH></L_PATH>
					</STEP>
					<!-- New Steps START-->
					<STEP STEPID="11"  CALC_ID="1011" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
					<xsl:attribute name="DESC"><xsl:call-template name ="STEPID11"></xsl:call-template></xsl:attribute>
						<PATH>0</PATH>
						<D_PATH></D_PATH>
						<L_PATH></L_PATH>
					</STEP>	
					<STEP STEPID="12"  CALC_ID="1012" PREFIX="-  Discount - Protected Devices " SUFIX ="%" ISCALCULATE="T"> 
					<xsl:attribute name="DESC"><xsl:call-template name ="STEPID12"></xsl:call-template></xsl:attribute>
						<PATH>0</PATH>
						<D_PATH></D_PATH>
						<L_PATH></L_PATH>
					</STEP>	
					<STEP STEPID="13"  CALC_ID="1013" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
					<xsl:attribute name="DESC"><xsl:call-template name ="STEPID13"></xsl:call-template></xsl:attribute>
						<PATH>0</PATH>
						<D_PATH></D_PATH>
						<L_PATH></L_PATH>
					</STEP>	
					<!-- New Steps END -->
					<STEP STEPID="14"  CALC_ID="1014" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
					<xsl:attribute name="DESC"><xsl:call-template name ="STEPID14"></xsl:call-template></xsl:attribute>
						<PATH>0</PATH>
						<D_PATH></D_PATH>
						<L_PATH></L_PATH>
					</STEP>	
					<STEP STEPID="15"  CALC_ID="1015" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
					<xsl:attribute name="DESC"><xsl:call-template name ="STEPID15"></xsl:call-template></xsl:attribute>
						<PATH>0</PATH>
						<D_PATH></D_PATH>
						<L_PATH></L_PATH>
					</STEP>	
					<STEP STEPID="16"  CALC_ID="1016" PREFIX="-  Charge - Prior Loss $" SUFIX ="" ISCALCULATE="T"> 
					<xsl:attribute name="DESC">  <xsl:call-template name ="STEPID16"/>  </xsl:attribute>
						<PATH>0</PATH>
						<D_PATH></D_PATH>
						<L_PATH></L_PATH>
					</STEP>	
					<STEP STEPID="17"  CALC_ID="1017" PREFIX="-  Charge - Wood Stove " SUFIX ="" ISCALCULATE="T"> 
					<xsl:attribute name="DESC">  <xsl:call-template name ="STEPID17"/>    </xsl:attribute>
						<PATH>0</PATH>
						<D_PATH></D_PATH>
						<L_PATH></L_PATH>
					</STEP>	
					<STEP STEPID="18"  CALC_ID="1018" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
					<xsl:attribute name="DESC"><xsl:call-template name ="STEPID18"></xsl:call-template></xsl:attribute>
						<PATH>0</PATH>
						<D_PATH></D_PATH>
						<L_PATH></L_PATH>
					</STEP>	
					<STEP STEPID="19"  CALC_ID="1019" PREFIX="-  Credit	- Seasonal/Secondary,Wolverine Insures Primary $" SUFIX ="" ISCALCULATE="T"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID19"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				<GROUPFORMULA>
					<OPERAND TOREPLACE="Y">@[CALC_ID=1004]</OPERAND> 
					<OPERATOR>*</OPERATOR> 				
					<OPERAND TOREPLACE="Y">@[CALC_ID=1005]</OPERAND> 
					<OPERATOR>*</OPERATOR> 				
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
			<GROUP GROUPID="5"  CALC_ID="10005" PREFIX="" SUFIX ="" ISCALCULATE="F">
			<xsl:attribute name="DESC"><xsl:call-template name ="GROUPID5"></xsl:call-template></xsl:attribute>
				<GROUPCONDITION>
					<CONDITION>
					</CONDITION>
				</GROUPCONDITION>  
				<GROUPRULE></GROUPRULE>
				<GDESC></GDESC>
				<STEP STEPID="20"  CALC_ID="1020" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID20"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				
				<STEP STEPID="21"  CALC_ID="1021" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID21"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				<STEP STEPID="22"  CALC_ID="1022" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID22"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 			
				<STEP STEPID="23"  CALC_ID="1023" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID23"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 			
				<STEP STEPID="24"  CALC_ID="1024" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID24"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 			
				<STEP STEPID="25"  CALC_ID="1025" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID25"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH><xsl:value-of select ="HO48ADDITIONAL"/></L_PATH>
				</STEP> 			
				<STEP STEPID="26"  CALC_ID="1026" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID26"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 			
				<STEP STEPID="27"  CALC_ID="1027" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID27"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 			
				<STEP STEPID="28"  CALC_ID="1028" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID28"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 			
				<STEP STEPID="29"  CALC_ID="1029" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID29"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 			
				<STEP STEPID="30"  CALC_ID="1030" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID30"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 			
				<STEP STEPID="31"  CALC_ID="1031" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID31"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 			
				<STEP STEPID="32"  CALC_ID="1032" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID32"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 			
				<STEP STEPID="33"  CALC_ID="1033" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID33"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 			
				<STEP STEPID="34"  CALC_ID="1034" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID34"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 			
				<STEP STEPID="35"  CALC_ID="1035" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID35"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH><xsl:call-template name ="HO_455_DEDUCTIBLE"></xsl:call-template></D_PATH>
					<L_PATH><xsl:call-template name ="HO_455_LIMIT"></xsl:call-template></L_PATH>
				</STEP> 			
				<STEP STEPID="36"  CALC_ID="1036" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID36"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 			
				<STEP STEPID="37"  CALC_ID="1037" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID37"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH><xsl:value-of select ="HO327"/></L_PATH>
				</STEP> 
				<!--New Steps Start-->			
				<STEP STEPID="38"  CALC_ID="1038" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID38"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>
				<STEP STEPID="39"  CALC_ID="1039" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID39"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>  			
				<STEP STEPID="40"  CALC_ID="1040" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID40"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				<STEP STEPID="41"  CALC_ID="1041" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID41"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>
				<!--New Steps End -->
				<STEP STEPID="42"  CALC_ID="1042" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID42"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH><xsl:value-of select ="SPECIFICSTRUCTURESADDITIONAL"/></L_PATH>
				</STEP> 			
				<STEP STEPID="43"  CALC_ID="1043" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID43"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				<STEP STEPID="44"  CALC_ID="1044" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID44"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>						
				<STEP STEPID="45"  CALC_ID="1045" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID45"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>			
				<STEP STEPID="46"  CALC_ID="1046" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID46"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>			
				<STEP STEPID="47"  CALC_ID="1047" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID47"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>	
				<STEP STEPID="48"  CALC_ID="1048" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID48"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>
				<STEP STEPID="49"  CALC_ID="1049" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID49"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>
				<STEP STEPID="50"  CALC_ID="1050" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID50"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>
				<STEP STEPID="51"  CALC_ID="1051" PREFIX="" SUFIX ="" ISCALCULATE="F"> <!-- SKB-->
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID51"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>
				<STEP STEPID="52"  CALC_ID="1052" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID52"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>
				<STEP STEPID="53"  CALC_ID="1053" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID53"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>
				<STEP STEPID="54"  CALC_ID="1054" PREFIX="" SUFIX ="" ISCALCULATE="F"> <!-- New-->
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID54"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>
				<STEP STEPID="55"  CALC_ID="1055" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID55"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>
				<STEP STEPID="56"  CALC_ID="1056" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID56"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>
				<STEP STEPID="57"  CALC_ID="1057" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID57"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>
				<STEP STEPID="58"  CALC_ID="1058" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID58"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>
				<STEP STEPID="59"  CALC_ID="1059" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID59"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>
				<STEP STEPID="60"  CALC_ID="1060" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID60"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>
				<STEP STEPID="61"  CALC_ID="1061" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID61"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>
				<STEP STEPID="62"  CALC_ID="1062" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID62"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>
				<STEP STEPID="63"  CALC_ID="1063" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID63"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
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
			<GROUP GROUPID="6" CALC_ID="10006" PREFIX="" SUFIX ="" ISCALCULATE="F">
			<xsl:attribute name="DESC"><xsl:call-template name ="GROUPID6"></xsl:call-template></xsl:attribute>
				<GROUPCONDITION>
					<CONDITION>
					</CONDITION>
				</GROUPCONDITION>  
				<GROUPRULE></GROUPRULE>
				<GDESC></GDESC>		
				<STEP STEPID="64"  CALC_ID="1064" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID64"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH><xsl:value-of select="SCH_BICYCLE_DED"/></D_PATH>
					<L_PATH><xsl:value-of select="SCH_BICYCLE_AMOUNT"/></L_PATH>
				</STEP>			 
				<STEP STEPID="65"  CALC_ID="1065" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID65"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH><xsl:value-of select="SCH_CAMERA_DED"/></D_PATH>
					<L_PATH><xsl:value-of select="SCH_CAMERA_AMOUNT"/></L_PATH>
				</STEP>			
				<STEP STEPID="66"  CALC_ID="1066" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID66"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH><xsl:value-of select="SCH_CELL_DED"/></D_PATH>
					<L_PATH><xsl:value-of select="SCH_CELL_AMOUNT"/></L_PATH>
				</STEP>			
				<STEP STEPID="67"  CALC_ID="1067" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID67"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH><xsl:value-of select="SCH_FURS_DED"/></D_PATH>
					<L_PATH><xsl:value-of select="SCH_FURS_AMOUNT"/></L_PATH>
				</STEP>			
				<STEP STEPID="68"  CALC_ID="1068" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID68"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH><xsl:value-of select="SCH_GOLF_DED"/></D_PATH>
					<L_PATH><xsl:value-of select="SCH_GOLF_AMOUNT"/></L_PATH>
				</STEP>			
				<STEP STEPID="69"  CALC_ID="1069" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID69"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH><xsl:value-of select="SCH_GUNS_DED"/></D_PATH>
					<L_PATH><xsl:value-of select="SCH_GUNS_AMOUNT"/></L_PATH>
				</STEP>			
				<STEP STEPID="70"  CALC_ID="1070" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID70"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH><xsl:value-of select="SCH_JWELERY_DED"/></D_PATH>
					<L_PATH><xsl:value-of select="SCH_JWELERY_AMOUNT"/></L_PATH>
				</STEP>			
				<STEP STEPID="71"  CALC_ID="1071" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID71"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH><xsl:value-of select="SCH_MUSICAL_DED"/></D_PATH>
					<L_PATH><xsl:value-of select="SCH_MUSICAL_AMOUNT"/></L_PATH>
				</STEP>			
				<STEP STEPID="72"  CALC_ID="1072" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID72"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH><xsl:value-of select="SCH_PERSCOMP_DED"/></D_PATH>
					<L_PATH><xsl:value-of select="SCH_PERSCOMP_AMOUNT"/></L_PATH>
				</STEP>			
				<STEP STEPID="73"  CALC_ID="1073" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID73"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH><xsl:value-of select="SCH_SILVER_DED"/></D_PATH>
					<L_PATH><xsl:value-of select="SCH_SILVER_AMOUNT"/></L_PATH>
				</STEP>			
				<STEP STEPID="74"  CALC_ID="1074" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID74"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH><xsl:value-of select="SCH_STAMPS_DED"/></D_PATH>
					<L_PATH><xsl:value-of select="SCH_STAMPS_AMOUNT"/></L_PATH>
				</STEP>			
				<STEP STEPID="75"  CALC_ID="1075" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID75"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH><xsl:value-of select="SCH_RARECOINS_DED"/></D_PATH>
					<L_PATH><xsl:value-of select="SCH_RARECOINS_AMOUNT"/></L_PATH>
				</STEP>			
				<STEP STEPID="76"  CALC_ID="1076" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID76"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH><xsl:value-of select="SCH_FINEARTS_WO_BREAK_DED"/></D_PATH>
					<L_PATH><xsl:value-of select="SCH_FINEARTS_WO_BREAK_AMOUNT"/></L_PATH>
				</STEP>			
				<STEP STEPID="77"  CALC_ID="1077" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID77"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH><xsl:value-of select="SCH_FINEARTS_BREAK_DED"/></D_PATH>
					<L_PATH><xsl:value-of select="SCH_FINEARTS_BREAK_AMOUNT"/></L_PATH>
				</STEP>
				<STEP STEPID="78"  CALC_ID="1078" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID78"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>	
				
				<STEP STEPID="79"  CALC_ID="1079" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID79"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>	
					
				<STEP STEPID="80"  CALC_ID="1080" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID80"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>	
				
				<STEP STEPID="81"  CALC_ID="1081" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID81"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>		
				
				
				<STEP STEPID="82"  CALC_ID="1082" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID82"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>		
				
				<STEP STEPID="83"  CALC_ID="1083" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"> <xsl:call-template name ="STEPID83"> </xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>	
				
				<STEP STEPID="84"  CALC_ID="1084" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID84"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>	
				
					
				<!--<STEP STEPID="72"  CALC_ID="1072" PREFIX="" SUFIX ="" ISCALCULATE="F"> 	
				<xsl:attribute name="DESC"><xsl:call-template name ="STEPID72"></xsl:call-template></xsl:attribute>
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>-->			
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
					<!--<OPERATOR>+</OPERATOR> 
					<OPERAND TOREPLACE="Y">@[CALC_ID=1072]</OPERAND> -->
				</GROUPFORMULA>
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
<!--								  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="DEWLLING-MAIN">
	
	<xsl:variable name="TERCODES" select="TERRITORYCODES"/>
	<xsl:variable name="GROUP_ID" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY']/NODE[@ID='TERRGROUPING']/ATTRIBUTES [@TID = $TERCODES]/@GROUPID"/>
			
	<xsl:variable name="F_CODE" select="FORM_CODE"/>
	<xsl:variable name="FORMGROUP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING']/ATTRIBUTES [@FORMID = $F_CODE]/@GROUPID"/>
	
	<!--
	
	<xsl:variable name="PCODE" select="PRODUCTNAME"/>
		IF('<xsl:value-of select="$FORMGROUP" />'='1')
		THEN <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1"/>			
		ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='2')	
		THEN <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code2"/>
		ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='3')	
		THEN <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code3"/>		
		ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='4')	
		THEN <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code4"/>
		ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='5')	
		THEN <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code5"/>
		ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='6')	
		THEN <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code6"/>
		ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='7')	
		THEN <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code7"/>
		ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='8')	
		THEN <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code8"/>
		ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='9')	
		THEN <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code9"/>
		ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='10')	
		THEN <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code10"/>		
		ELSE 1.00
		-->
		<!-- New calculation added by praveen singh --> 
		<xsl:variable name="PCODE1" select="PRODUCTNAME"/>
		
		<xsl:variable name="PCODE">                      <!-- condition is to be corrected   --> 
		<xsl:choose>
			<xsl:when test ="PRODUCTNAME = 'HO-4' and NUMBEROFUNITS &lt;= 4 and STATENAME='INDIANA'">HO-4 CO I</xsl:when>
			<xsl:when test ="PRODUCTNAME = 'HO-4' and NUMBEROFUNITS &gt;= 5 and STATENAME='INDIANA'">HO-4 CO II</xsl:when>
			<xsl:when test ="PRODUCTNAME = 'HO-6' and NUMBEROFUNITS &lt;= 4 and STATENAME='INDIANA'">HO-4 CO I</xsl:when>
			<xsl:when test ="PRODUCTNAME = 'HO-6' and NUMBEROFUNITS &gt;= 5 and STATENAME='INDIANA'">HO-4 CO II</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select ="PRODUCTNAME"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:variable> 
		
		
		<xsl:choose> 
		<xsl:when test="$FORMGROUP = '1'"><xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1"/></xsl:when>
		<xsl:when test="$FORMGROUP = '2'"> <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code2"/> </xsl:when> 
		<xsl:when test="$FORMGROUP = '3'"> <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code3"/></xsl:when>
		<xsl:when test="$FORMGROUP = '4'"> <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code4"/></xsl:when>
		<xsl:when test="$FORMGROUP = '5'"> <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code5"/></xsl:when>
		<xsl:when test="$FORMGROUP = '6'"> <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code6"/></xsl:when>
		<xsl:when test="$FORMGROUP = '7'"> <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code7"/></xsl:when>
		<xsl:when test="$FORMGROUP = '8'"> <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code8"/></xsl:when>
		<xsl:when test="$FORMGROUP = '9'"> <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code9"/></xsl:when>
		<xsl:when test="$FORMGROUP = '10'"> <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code10"/></xsl:when>		
		
		<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
		
		
</xsl:template>
<!-- For INDINA -->
<xsl:template name="DEWLLING_MAIN_INDIANA">
	<!--<xsl:value-of select ="TERRITORYCODES"></xsl:value-of>-->
	<xsl:variable name="TERCODES" select="TERRITORYCODES"/>
	<xsl:variable name="GROUP_ID" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY']/NODE[@ID='TERRGROUPING']/ATTRIBUTES [@TID = $TERCODES]/@GROUPID"/>
			
	<xsl:variable name="F_CODE" select="FORM_CODE"/>
	<xsl:variable name="FORMGROUP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING']/ATTRIBUTES [@FORMID = $F_CODE]/@GROUPID"/>
	<xsl:variable name="PCODE">
		<xsl:choose>
			<xsl:when test ="PRODUCTNAME = 'HO-4' and NUMBEROFUNITS &lt;= 4 ">HO-4 CO I</xsl:when>
			<xsl:when test ="PRODUCTNAME = 'HO-4' and NUMBEROFUNITS &gt;= 5 ">HO-4 CO II</xsl:when>
			<xsl:when test ="PRODUCTNAME = 'HO-6' and NUMBEROFUNITS &lt;= 4 ">HO-4 CO I</xsl:when>
			<xsl:when test ="PRODUCTNAME = 'HO-6' and NUMBEROFUNITS &gt;= 5 ">HO-4 CO II</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select ="PRODUCTNAME"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:variable> 
	
	
		IF('<xsl:value-of select="$FORMGROUP" />'='1')
		THEN <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1"/>			
		ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='2')	
		THEN <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code2"/>
		ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='3')	
		THEN <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code3"/>		
		ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='4')	
		THEN <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code4"/>
		ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='5')	
		THEN <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code5"/>
		ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='6')	
		THEN <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code6"/>
		ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='7')	
		THEN <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code7"/>
		ELSE 1.00
</xsl:template>

<!-- INDIANA HO-4 Reguler-->
<xsl:template name="DEWLLING_MAIN_INDIANA_HO_4">
	
	<xsl:variable name="TERCODES" select="TERRITORYCODES"/>
	<xsl:variable name="GROUP_ID" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY']/NODE[@ID='TERRGROUPING']/ATTRIBUTES [@TID = $TERCODES]/@GROUPID"/>
			
	<xsl:variable name="F_CODE" select="FORM_CODE"/>
	<xsl:variable name="FORMGROUP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING']/ATTRIBUTES [@FORMID = $F_CODE]/@GROUPID"/>
	<xsl:variable name="PCODE" select="PRODUCTNAME"/>
		IF('<xsl:value-of select="$FORMGROUP" />'='1')
		THEN <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1"/>			
		ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='2')	
		THEN <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code2"/>
		ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='3')	
		THEN <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code3"/>		
		ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='4')	
		THEN <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code4"/>
		ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='5')	
		THEN <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code5"/>
		ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='6')	
		THEN <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code6"/>
		ELSE 1.00
</xsl:template>

<!-- ============================================================================================ -->
<!--								Base Premium Template(END)									  -->
<!--								  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- Coverage Value Old 
<xsl:template name="C-VALUE">
	<xsl:variable name="CVALUE" select="COVERAGEVALUE"/> 
	<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA =$CVALUE]/@Factor"/>
</xsl:template>-->

<!-- ============================================================================================ -->
<!--		     						Template For COVERAGE VALUE  (START)					  -->
<!--			    					  FOR MICHIGAN and INDIANA								  -->
<!-- ============================================================================================ -->

<xsl:template name="C-VALUE">
	<xsl:variable name="CVALUE" select="COVERAGEVALUE"/> 
	<xsl:choose>
		<!--<xsl:when test ="COVERAGEVALUE > 100">-->
		<xsl:when test ="COVERAGEVALUE > 500">
			((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA =100]/@Factor"/>) +
			(((<xsl:value-of select="COVERAGEVALUE"/> - 100) DIV 10)* 0.0177))
		</xsl:when>
		<xsl:otherwise>
			<xsl:choose>
				<xsl:when test ="PRODUCTNAME = 'HO-4'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA =$CVALUE]/@Factor"/>		
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA =$CVALUE]/@Factor"/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- Coverage value for HO-3 INDIANA -->
<xsl:template name="C_VALUE_INDIANA">
	<xsl:variable name="CVALUE" select="COVERAGEVALUE"/> 
	<xsl:choose>
		<xsl:when test ="FORM_CODE = '1F'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '2F'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '3F'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '4F'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '5F'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '6F'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '7F'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G2']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '8F'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G2']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '9F'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G2']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '10F'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G2']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '1M'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '2M'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '3M'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '4M'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '5M'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '6M'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '7M'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '8M'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '9M'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '10M'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G2']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:otherwise>
		1.00
		</xsl:otherwise>
	</xsl:choose>
	
</xsl:template>

<!-- Coverage Value For HO-4 and HO-6 (Regular) -->
<xsl:template name="C_VALUE_HO_4_AND_6">
	<xsl:variable name="CVALUE" select="COVERAGEVALUE"/> 
	<xsl:choose>
		<xsl:when test ="COVERAGEVALUE &gt; 100">
			IF('<xsl:value-of select ="PRODUCTNAME"/>' = 'HO-4')
			THEN
				((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA =100]/@Factor"/>) +
				(((<xsl:value-of select="COVERAGEVALUE"/> - 100) DIV 10)* 0.048))
			ELSE
				((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G2']/ATTRIBUTES[@CovA =100]/@Factor"/>) +
				(((<xsl:value-of select="COVERAGEVALUE"/> - 100) DIV 10)* 0.0228))
		</xsl:when>
		<xsl:otherwise>
			IF('<xsl:value-of select = "PRODUCTNAME"/>' = 'HO-4')
			THEN
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G3']/ATTRIBUTES[@CovC =$CVALUE]/@Factor"/>		
			ELSE
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G4']/ATTRIBUTES[@CovC =$CVALUE]/@Factor"/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--		     						Template For COVERAGE VALUE  (END)						  -->
<!--			    					  FOR MISHIGAN and INDIANA								  -->
<!-- ============================================================================================ -->

<xsl:template name="TERRITORY_CODES">
	<xsl:variable name="TCODES" select="TERRITORYCODES"/>
	<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY']/NODE/ATTRIBUTES[@TID = $TCODES]/@GROUPID"/>
</xsl:template>

<!-- ============================================================================================ -->
<!--						Templates For Coverage A,B,C,D,E,F  (START)							  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="COVERAGE_A_PREMIUM_FOR_HO6">
 

<xsl:variable name ="PCOVERAGE_D_ADDITIONAL_VALUE" select ="PERSONALPROPERTYINCREASEDLIMITADDITIONAL"></xsl:variable>
	<xsl:variable name ="PCOVERAGE_D_ADDITIONAL_COST" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='ADDITIONAL_BULDING_PROPERTY']/ATTRIBUTES/@COST"></xsl:variable>
	<xsl:variable name ="PCOVERAGE_D_ADDITIONAL_INCREASEDAMOUNT" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='ADDITIONAL_BULDING_PROPERTY']/ATTRIBUTES/@INCREASEDAMOUNT"></xsl:variable>
	 
	<xsl:variable name="VAR1">  <xsl:value-of select ="$PCOVERAGE_D_ADDITIONAL_COST"/> </xsl:variable>
	<xsl:variable name="VAR2"> <xsl:value-of select ="$PCOVERAGE_D_ADDITIONAL_VALUE"/> </xsl:variable>
	<xsl:variable name="VAR3"> <xsl:value-of select ="$PCOVERAGE_D_ADDITIONAL_INCREASEDAMOUNT"/>  </xsl:variable>
	<xsl:value-of select="round(round($VAR1*$VAR2) div $VAR3)"/>
</xsl:template>


<xsl:template name="COVERAGE_A_PREMIUM">
<xsl:variable name="VAR1">
			<xsl:call-template name ="C-VALUE"></xsl:call-template>
	 </xsl:variable>
	
	<xsl:variable name="VAR2"> 
		<xsl:call-template name ="DEWLLING-MAIN"></xsl:call-template> 
	</xsl:variable>
 
   <xsl:value-of select="$VAR1*$VAR2"/>
   
</xsl:template>

<!--Coverage B - Other Structures - Limit And Deductible -->
<xsl:template name="COVERAGE_B_PREMIUM">
	<!--<xsl:value-of select ="HO48INCLUDE"/> -->
	1
</xsl:template>

<!--Coverage C - Personal Property - Limit And Deductible -->
<xsl:template name="COVERAGE_C_PREMIUM">
	<xsl:variable name="PNAME" select="PRODUCTNAME"></xsl:variable>
	<xsl:variable name ="PCOVERAGE_C_ADDITIONAL_VALUE" select ="PERSONALPROPERTYINCREASEDLIMITADDITIONAL"></xsl:variable>
	<xsl:variable name ="PCOVERAGE_C_ADDITIONAL_COST" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = $PNAME]/@COST"></xsl:variable>
	<xsl:variable name ="PCOVERAGE_C_ADDITIONAL_INCREASEDAMOUNT" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = $PNAME]/@INCREASEDAMOUNT"></xsl:variable>
	
	((<xsl:value-of select ="$PCOVERAGE_C_ADDITIONAL_COST"></xsl:value-of>*
	<xsl:value-of select ="$PCOVERAGE_C_ADDITIONAL_VALUE"></xsl:value-of>) DIV
	<xsl:value-of select ="$PCOVERAGE_C_ADDITIONAL_INCREASEDAMOUNT"></xsl:value-of>)
	
</xsl:template>

<!-- Coverage D - Loss Of Use - Limit And Deductible-->
<xsl:template name="COVERAGE_D_PREMIUM">
	<xsl:variable name ="PCOVERAGE_D_ADDITIONAL_VALUE" select ="ADDITIONALLIVINGEXPENSEADDITIONAL"></xsl:variable>
	<xsl:variable name ="PCOVERAGE_D_ADDITIONAL_COST" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='ADDITIONAL_LIVING_EXPENSE']/ATTRIBUTES/@COST"></xsl:variable>
	<xsl:variable name ="PCOVERAGE_D_ADDITIONAL_INCREASEDAMOUNT" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='ADDITIONAL_LIVING_EXPENSE']/ATTRIBUTES/@INCREASEDAMOUNT"></xsl:variable>
	
	((<xsl:value-of select ="$PCOVERAGE_D_ADDITIONAL_COST"></xsl:value-of>*
	<xsl:value-of select ="$PCOVERAGE_D_ADDITIONAL_VALUE"></xsl:value-of>) DIV
	<xsl:value-of select ="$PCOVERAGE_D_ADDITIONAL_INCREASEDAMOUNT"></xsl:value-of>)
	
</xsl:template>

<!-- Coverage E - Personal Liability - Each Occurrence Limit And Deductible-->
<xsl:template name="COVERAGE_E_LIMIT">
	<xsl:value-of select="PERSONALLIABILITY_LIMIT"></xsl:value-of>	
</xsl:template>

<!-- Coverage F - Medical Payments to Others - Each Person-->
<xsl:template name="Coverage_F_Limit">
	<xsl:choose>
		<xsl:when test="MEDICALPAYMENTSTOOTHERS_LIMIT > 0">
			<xsl:value-of select="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of>	
		</xsl:when>
		<xsl:otherwise>
			1.00
		</xsl:otherwise>
	
	</xsl:choose>
</xsl:template>


<!-- ============================================================================================ -->
<!--						Templates For Coverage A,B,C,D,E,F  (END)							  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--						Templates for Insurance Score Credit, Display (START)				  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name ="INSURANCE_SCORE_CREDIT_1">
	<xsl:variable name="INS" select="INSURANCESCORE"></xsl:variable>
	<xsl:choose>
		<xsl:when test = "$INS = 0">
			<xsl:value-of select= "$INS"></xsl:value-of>
		</xsl:when>
		<xsl:when test = "$INS = ''">
			1.00
		</xsl:when>
		<xsl:when test = "$INS = 'No Hit No Score'">
			0.87
		</xsl:when>
		<xsl:when test = "$INS &gt;= 751">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE = 751]/@FACTOR"/>
		</xsl:when>		
		<xsl:otherwise>						
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@FACTOR"/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<xsl:template name="INSURANCE_SCORE_CREDIT">
	<xsl:choose>
		<xsl:when test = "INSURANCESCORE = 0">
			<xsl:variable name="INS" select="INSURANCESCORE"></xsl:variable>
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='CREDIT'  ]/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@FACTOR"/>
		</xsl:when>
		<xsl:otherwise>
			1.00		
		</xsl:otherwise>
	</xsl:choose>
</xsl:template> 

<!-- For Display Only -->
<xsl:template name="INSURANCE_SCORE_CREDIT_DISPLAY">
	<xsl:variable name="INS" select="INSURANCESCORE"/>
	<xsl:variable name="INS_RESULT">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='CREDIT']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@CREDIT"/>
	</xsl:variable>		
	<xsl:choose>
		<xsl:when test = "INSURANCESCORE &gt; 0">
			<xsl:choose>
				<xsl:when test ="$INS_RESULT = 1.00">
					0.00
				</xsl:when>
				<xsl:otherwise>
					'Applied'
				</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template> 

<xsl:template name="INSURANCE_SCORE_DISPLAY">
	<xsl:choose>
		<xsl:when test = "INSURANCESCORE &gt; 599">
			<xsl:variable name="INS" select="INSURANCESCORE"></xsl:variable>
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='CREDIT']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@CREDIT"/>
		</xsl:when>
		<xsl:otherwise>
		0.00		
		</xsl:otherwise>
	</xsl:choose>
</xsl:template> 

<!-- Converting INSURANCE SCORE Credit into percent --> 
<xsl:template name ="INSURANCESCORE_PERCENT">
	
	<xsl:variable name ="VAR_SCORE"> <xsl:call-template name ="INSURANCE_SCORE_CREDIT_1" /> </xsl:variable>
	
		 
	
	<xsl:variable name="VAR_SCORE_PERCENT"> 
	<xsl:choose> 
 	<xsl:when test ="$VAR_SCORE != '' and $VAR_SCORE &lt; 1.00">
		   <xsl:value-of select="round((1 - $VAR_SCORE) * 100)"/>%
	</xsl:when>
	
	<xsl:when test ="$VAR_SCORE != '' and $VAR_SCORE &gt; 1.00">
		   <xsl:value-of select="round(($VAR_SCORE -1 ) * 100)"/>%
	</xsl:when>
	 
 </xsl:choose>
 </xsl:variable>
 
 <xsl:value-of select="$VAR_SCORE_PERCENT"/>
</xsl:template>


<!-- ============================================================================================ -->
<!--						Templates for Insurance Score Credit, Display (END)					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--						Templates for Term Factor  (START)									  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name = "TERM_FACTOR">
	<xsl:choose>
		<xsl:when test="TERMFACTOR = 12">
			1.00
		</xsl:when>
		<xsl:otherwise>
			.5
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--						Templates for Term Factor  (END)									  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--		Templates for Discount - Under Construction [Factor,Credit,Display]  (START)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="DWELLING_UNDER_CONSTRUCTION">
	<xsl:choose>
		<xsl:when test = "CONSTRUCTIONCREDIT = 'Y'">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='DWELLINGUNDERCONSTRUCTION']/ATTRIBUTES/@FACTOR"/> 		
		</xsl:when>
		<xsl:otherwise>
		1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="DWELLING_UNDER_CONSTRUCTION_CREDIT">
	<xsl:choose>
		<xsl:when test = "CONSTRUCTIONCREDIT = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='DWELLINGUNDERCONSTRUCTION']/ATTRIBUTES/@CREDIT"/> 		
		</xsl:when>
		<xsl:otherwise>
		1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- For Display Only -->
<xsl:template name="DWELLING_UNDER_CONSTRUCTION_DISPLAY">
	<xsl:choose>
		<xsl:when test = "CONSTRUCTIONCREDIT = 'Y'">
		Applied
		</xsl:when>
		<xsl:otherwise>
		0
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--		Templates for Discount - Under Construction [Factor,Credit,Display]  (END)			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--						Templates for Discount - Deductible Factor (START)					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="DFACTOR">	
	<xsl:variable name="DEDUCTIBLEAMT" select="DEDUCTIBLE"/>
	<xsl:choose>
		<xsl:when test = "DEDUCTIBLE >= 500">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLE']/NODE[@ID ='OPTIONALDEDUCTIBLE']/ATTRIBUTES[@AMOUNT=$DEDUCTIBLEAMT]/@FACTOR"/>
		</xsl:when>
		<xsl:otherwise>
			1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--						Templates for Discount - Deductible Factor (END)					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--		Templates for Discount - Experince Factor [Factor,Credit,Display]  (SATRT)			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="EXPERIENCEFACTOR">
	<xsl:choose>
		<xsl:when test = "EXPERIENCE > 35 ">
			<xsl:variable name="EF" select="EXPERIENCE"></xsl:variable>
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MATURITY']/ATTRIBUTES[@MINAGE &lt;= $EF and @MAXAGE &gt;= $EF]/@FACTOR"/>
		</xsl:when>
		<xsl:otherwise>
			1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template> 

<!--Experince Credit -->
<xsl:template name="EXPERIENCECREDIT">
	<xsl:choose>
		<xsl:when test = "EXPERIENCE > 35 ">
			<xsl:variable name="EF" select="EXPERIENCE"></xsl:variable>
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MATURITY']/ATTRIBUTES[@MINAGE &lt;= $EF and @MAXAGE &gt;= $EF]/@CREDIT"/>
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template> 
<!-- For Display Only-->
<xsl:template name="EXPERIENCEFACTOR_DISPLAY">
	<xsl:choose>
		<xsl:when test = "EXPERIENCE > 35 ">
		Applied
		</xsl:when>
		<xsl:otherwise>
			0
		</xsl:otherwise>
	</xsl:choose>
</xsl:template> 
<!-- ============================================================================================ -->
<!--		Templates for Discount - Experince Factor [Factor,Credit,Display]  (END)			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--		Templates for Discount - Age Of Home Factor  [Factor,Credit,Display]  (SATRT)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="AGEOFHOME_FACTOR">	
	<xsl:choose>
		<xsl:when test = "STATENAME ='MICHIGAN' and AGEOFHOME &gt; 20">
			1.00
		</xsl:when>
		<xsl:when test = "STATENAME ='INDIANA' and AGEOFHOME &gt; 10">
			1.00
		</xsl:when>
		<xsl:otherwise>
			<xsl:choose>
				<xsl:when test ="CONSTRUCTIONCREDIT = 'N'">
					<xsl:variable name="AGE_OF_HOME" select="AGEOFHOME"/>
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/ATTRIBUTES[@AGE = $AGE_OF_HOME]/@FACTOR"/> 
				</xsl:when>
				<xsl:otherwise>
				1.00
				</xsl:otherwise>
			</xsl:choose>
		</xsl:otherwise>
	</xsl:choose>
	
</xsl:template>

<!--Age Of Home CREDIT -->
<xsl:template name="AGEOFHOME_CREDIT">	
	
	<xsl:choose>
		<xsl:when test = "AGEOFHOME &gt; 20 and STATENAME = 'MICHIGAN'">		
		</xsl:when>
		<xsl:when test = "AGEOFHOME &gt; 10 and STATENAME = 'INDIANA'">	
		</xsl:when>
		<xsl:otherwise>
			<xsl:choose>
				<xsl:when test = "CONSTRUCTIONCREDIT = 'N'">
					<xsl:variable name="AGE_OF_HOME" select="AGEOFHOME"/>
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/ATTRIBUTES[@AGE = $AGE_OF_HOME]/@CREDIT"/>%
				</xsl:when>
				<xsl:otherwise>					
				</xsl:otherwise>
			</xsl:choose>
		</xsl:otherwise>
	</xsl:choose>  
	
</xsl:template>

<!--Age Of Home Display -->
<xsl:template name="AGEOFHOME_DISPLAY">	
	<xsl:choose>
		<xsl:when test = "AGEOFHOME &gt; 20 and STATENAME = 'MISHIGAN'">
		0.00
		</xsl:when>
		<xsl:when test = "AGEOFHOME &gt; 10 and STATENAME = 'INDIANA'">
		0.00
		</xsl:when>
		<xsl:otherwise>
			<xsl:choose>
				<xsl:when test = "CONSTRUCTIONCREDIT = 'N'">
					Applied		
				</xsl:when>
				<xsl:otherwise>
				0.00
				</xsl:otherwise>
			</xsl:choose>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--		Templates for Discount - Age Of Home   [Factor,Credit,Display]  (END)				  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--		Templates for Discount - Protective Device   [Factor,Credit,Display]  (SATRT)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="PROTECTIVEDEVICE_DISPLAY_TEMP">
	<!--<xsl:variable name ="CANDISPLAY" ><xsl:call-template name ="PROTECTIVEDEVICE"/></xsl:variable>
IF(<xsl:call-template name ="PROTECTIVEDEVICE"/> &gt; 0.00)
	THEN
		1
	ELSE
		0


IF(<xsl:value-of select ="$CANDISPLAY"/> = 1) THEN
	999.00
ELSE
	888.00-->
<!--<xsl:choose>
	<xsl:when test ="$CANDISPLAY = 1">
	1.00
	</xsl:when>
	<xsl:otherwise>
	0
	</xsl:otherwise>
</xsl:choose>-->
<xsl:variable name ="IS_CALCULATEABLE">
		<xsl:call-template name ="GET_PROTECTIVE_DEVICE_DISCOUNT"/>
		</xsl:variable>
<xsl:choose>
	
	<xsl:when test="$IS_CALCULATEABLE = 1.00">
	0	
	</xsl:when>
	<xsl:otherwise>
		<xsl:variable name="PROTECTIVE_DEVICE_VALUE">
				<xsl:call-template name ="DIRECTBURGLERANDFIRE"/>+
				<xsl:call-template name ="DIRECTFIREANDPOLICE"/>+
				<xsl:call-template name ="LOCALFIREGASALARM"/>
			</xsl:variable>

			IF (<xsl:value-of select ="$PROTECTIVE_DEVICE_VALUE"/> = 0.00)
			
			THEN
				0
			ELSE 
				IF (<xsl:value-of select ="$PROTECTIVE_DEVICE_VALUE"/> &gt; 0.00) 
				THEN
					'Applied'
				ELSE
					'Applied'
	</xsl:otherwise>
</xsl:choose>
</xsl:template>

<xsl:template name="PROTECTIVEDEVICE_DISPLAY">
	<!--<xsl:variable name ="CANDISPLAY" ><xsl:call-template name ="PROTECTIVEDEVICE_DISPLAY_TEMP"/></xsl:variable>
	<xsl:choose>
		<xsl:when test ="$CANDISPLAY = 0">
		Applied
		</xsl:when>
		<xsl:otherwise>
		0
		</xsl:otherwise>
	</xsl:choose>
	<xsl:value-of select ="$CANDISPLAY"></xsl:value-of>-->
	<xsl:call-template name = "PROTECTIVEDEVICE_DISPLAY_TEMP"></xsl:call-template>
	<!--<xsl:value-of select ="PROTECTIVEDEVICE"></xsl:value-of>-->
</xsl:template>

<xsl:template name="PROTECTIVEDEVICE">
	<xsl:choose>
		<xsl:when test ="CONSTRUCTIONCREDIT = 'N'">
		
			<xsl:variable name="PROTECTIVE_DEVICE_VALUE">
				<xsl:call-template name ="DIRECTBURGLERANDFIRE"/>+
				<xsl:call-template name ="DIRECTFIREANDPOLICE"/>+
				<xsl:call-template name ="LOCALFIREGASALARM"/>
			</xsl:variable>

			IF (<xsl:value-of select ="$PROTECTIVE_DEVICE_VALUE"/> = 0.00)
			THEN
				1.00
			ELSE 
				IF (<xsl:value-of select ="$PROTECTIVE_DEVICE_VALUE"/> &gt; 15.00)
				THEN
					(100.00 - 15.00) DIV 100.00 <!--Max credit allowed is 15%. We are sending factor therefore 100 - 15 /100-->
					
				ELSE
					(100.00 - <xsl:value-of select ="$PROTECTIVE_DEVICE_VALUE"></xsl:value-of>) DIV 100.00
					
		</xsl:when>
		<xsl:otherwise>
		1.00
		</xsl:otherwise>
	</xsl:choose>	
			
</xsl:template>

<!-- Central Stations Burglary & Fire Alarm System -->
<xsl:template name="DIRECTBURGLERANDFIRE"> 
(
	IF ('<xsl:value-of select="BURGLAR"/>' = 'Y' and '<xsl:value-of select="CENTRAL_FIRE"/>' = 'Y')
	THEN
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='CSBF']/@CREDIT"/>
	ELSE IF('<xsl:value-of select="BURGLAR"/>' = 'Y') THEN
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='CSB']/@CREDIT"/>
	ELSE IF ('<xsl:value-of select="CENTRAL_FIRE"/>' = 'Y') THEN
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='CSF']/@CREDIT"/>
	ELSE
		0.00
)
</xsl:template>


<!-- Direct to Fire and Police -->
<xsl:template name="DIRECTFIREANDPOLICE"> 
(
	IF ('<xsl:value-of select="BURGLER_ALERT_POLICE"/>' = 'Y' and '<xsl:value-of select="FIRE_ALARM_FIREDEPT"/>' = 'Y')
	THEN
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='DFP']/@CREDIT"/>
	ELSE IF('<xsl:value-of select="BURGLER_ALERT_POLICE"/>' = 'Y') THEN
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='DP']/@CREDIT"/>
	ELSE IF ('<xsl:value-of select="FIRE_ALARM_FIREDEPT"/>' = 'Y') THEN
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='DF']/@CREDIT"/>
	ELSE
		0.00
)
</xsl:template>

<!-- Local Fire or Local Gas Alarm -->
<xsl:template name="LOCALFIREGASALARM"> 
(
	IF (<xsl:value-of select="N0_LOCAL_ALARM"/> = 1)
	THEN
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='LFLG']/@CREDIT"/>
	ELSE IF(<xsl:value-of select="N0_LOCAL_ALARM"/> &gt;= 2) THEN
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='LFA']/@CREDIT"/>
	ELSE
		0.00
)
</xsl:template>

<xsl:template name="PROTECTIVEDEVICE_CREDIT">
	<xsl:variable name="PROTECTIVE_DEVICE_VALUE">
		<xsl:call-template name ="DIRECTBURGLERANDFIRE"/>+
		<xsl:call-template name ="DIRECTFIREANDPOLICE"/>+
		<xsl:call-template name ="LOCALFIREGASALARM"/>
	</xsl:variable>
	
	IF (<xsl:value-of select ="$PROTECTIVE_DEVICE_VALUE"/> = 0.00)
	THEN
		0
	ELSE 
		
		IF (<xsl:value-of select ="$PROTECTIVE_DEVICE_VALUE"/> &gt; 15.00)
		THEN
			15
		ELSE
			<xsl:value-of select ="$PROTECTIVE_DEVICE_VALUE"/>
</xsl:template>
<!-- ============================================================================================ -->
<!--		Templates for Discount - Protective Device   [Factor,Credit,Display]  (END)			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--		Templates for Discount - Multi policy Factor [Factor,Credit,Display]  (SATRT)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="MULTIPOLICY_FACTOR">	
	<xsl:choose>
		<xsl:when test="MULTIPLEPOLICYFACTOR = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MULTIPOLICY']/ATTRIBUTES/@FACTOR"/> 		
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
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MULTIPOLICY']/ATTRIBUTES/@CREDIT"/> 		
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!--Multipolicy CREDIT 
<xsl:template name="MULTIPOLICY_CREDIT">	
	
	<xsl:choose>
		<xsl:when test="MULTIPLEPOLICYFACTOR = 'Y'">
		
		<xsl:variable name ="IS_CALCULATEABLE">
			<xsl:call-template name ="GET_MULTIPOLICY_DISCOUNT"/>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test ="$IS_CALCULATEABLE ">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MULTIPOLICY']/ATTRIBUTES/@CREDIT"/> 		
			</xsl:when>
			<xsl:otherwise>
				1.00 - (<xsl:call-template name ="GET_MULTIPOLICY_DISCOUNT"/>)
			</xsl:otherwise>
		</xsl:choose>
		</xsl:when>
		<xsl:otherwise>
		0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>-->
<!--Multipolicy Display -->
<xsl:template name="MULTIPOLICY_DISPLAY">	
	
	<xsl:choose>
		<xsl:when test="MULTIPLEPOLICYFACTOR = 'Y'">
		Applied
		</xsl:when>
		<xsl:otherwise>
		0
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--		Templates for Discount - Multipolicy Factor [Factor,Credit,Display]  (END)			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--		Templates for Discount - Valued Customer [Factor,Credit,Display]  (SATRT)			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="VALUEDCUSTOMER">
<xsl:choose>
	<xsl:when test="NOTLOSSFREE ='Y'">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_WITH_CLAIMS']/ATTRIBUTES[@MINRENEWALDURATION = '3']/@FACTOR"/>
	</xsl:when>
	<xsl:when test="LOSSFREE ='Y'">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_WITHOUT_CLAIMS']/ATTRIBUTES[@MINRENEWALDURATION = '3']/@FACTOR"/>
	</xsl:when>
	<xsl:otherwise>
		1.00
	</xsl:otherwise>
</xsl:choose>
</xsl:template>


<!--Valued Customer-Renual Credit CREDIT -->
<xsl:template name="VALUEDCUSTOMER_FACTOR">
<xsl:choose>
	<xsl:when test="NOTLOSSFREE ='Y'">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_WITH_CLAIMS']/ATTRIBUTES[@MINRENEWALDURATION = '3']/@CREDIT"/>
	</xsl:when>
	<xsl:when test="LOSSFREE ='Y'">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_WITHOUT_CLAIMS']/ATTRIBUTES[@MINRENEWALDURATION = '3']/@CREDIT"/>
	</xsl:when>
	<xsl:otherwise>
		1.00
	</xsl:otherwise>
</xsl:choose>
</xsl:template>


<xsl:template name ="VCUSTOMER">
	<xsl:choose>
		<xsl:when test = "YEARS = 0">
			<xsl:value-of select= "YEARS"></xsl:value-of>
		</xsl:when>
		<xsl:otherwise>
			<xsl:variable name="YRS" select="YEARS"/>			
			(<xsl:call-template name ="DEWLLING-MAIN"></xsl:call-template>)* <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_WITH_CLAIMS']/ATTRIBUTES[@MINRENEWALDURATION &lt;= $YRS and @MAXRENEWALDURATION &gt;= $YRS]/@FACTOR"/> div 100
		</xsl:otherwise>
	</xsl:choose>	
</xsl:template> 
<xsl:template name ="VCUSTOMER1">
	<xsl:choose>
		<xsl:when test = "YEARS = 0">
			<xsl:value-of select= "YEARS"></xsl:value-of>
		</xsl:when>
		<xsl:otherwise>
			<xsl:variable name="YRS" select="YEARS"/>			
			(<xsl:call-template name ="DEWLLING-MAIN"></xsl:call-template>)*<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_WITHOUT_CLAIMS']/ATTRIBUTES[@MINRENEWALDURATION &lt;= $YRS and @MAXRENEWALDURATION &gt;= $YRS]/@FACTOR"/> div 100
		</xsl:otherwise>
	</xsl:choose>
</xsl:template> 

<xsl:template name="VALUEDCUSTOMER_DISPLAY">
<xsl:choose>
	<xsl:when test="NOTLOSSFREE ='Y'">
		Applied
	</xsl:when>
	<xsl:when test="LOSSFREE ='Y'">
		Applied
	</xsl:when>
	<xsl:otherwise>
		0.00
	</xsl:otherwise>
</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--		Templates for Discount - Valued Customer [Factor,Credit,Display]  (END)				  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--Templates for Charge-Insured to 80% to 99% of Replacement Cost [Factor,Credit,Display](SATRT) -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- In the case of Premium Quote HO Factor will be 1-->
<xsl:template name="REPLACEMENT_COST">
	<xsl:choose>
		<xsl:when test="DWELLING_LIMITS = REPLACEMENTCOSTFACTOR">
		1.00
		</xsl:when>
		<xsl:when test="DWELLING_LIMITS &lt; REPLACEMENTCOSTFACTOR">
		1.18
		</xsl:when>
		<xsl:otherwise>
		1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- For Display Only -->
<xsl:template name="REPLACEMENT_COST_DISPLAY">
	<!--Applied-->
	0
</xsl:template>

<!-- Charge - Prior Loss " -->
<xsl:template name="PRIOR_LOSS">
	<xsl:choose>
		<xsl:when test ="PRIOR_LOSS_SURCHARGE = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGE']/NODE[@ID='NEWBUSINESSPRIORLOSS']/ATTRIBUTES/@FACTOR"/>
		</xsl:when>
		<xsl:otherwise>
			1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>	
<!-- For Display Only -->
<xsl:template name="PRIOR_LOSS_DISPLAY">
	<xsl:choose>
		<xsl:when test ="PRIOR_LOSS_SURCHARGE = 'Y'">
		Applied
		</xsl:when>
		<xsl:otherwise>
		0
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>	
<!-- For Display In The Label -->
<xsl:template name="PRIOR_LOSS_LABEL_VALUE">
	<xsl:choose>
		<xsl:when test ="PRIOR_LOSS_SURCHARGE = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGE']/NODE[@ID='NEWBUSINESSPRIORLOSS']/ATTRIBUTES/@SURCHARGE"/>
		</xsl:when>
		<xsl:otherwise>
			1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>	
<!-- ============================================================================================ -->
<!--Templates for Charge-Insured to 80% to 99% of Replacement Cost [Factor,Credit,Display](END)   -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--				Templates for Charge - Wood Stove [Factor,Credit,Display](START)			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="WSTOVE">
	<xsl:choose>
		<xsl:when test ="WOODSTOVE_SURCHARGE = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGE']/NODE[@ID='WOODSTOVE']/ATTRIBUTES/@FACTOR"/>
		</xsl:when>
		<xsl:otherwise>
			1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>		
<!-- Woodstove Surcharge-->
<xsl:template name="WSTOVE_SURCHARGE">
	<xsl:choose>
		<xsl:when test ="WOODSTOVE_SURCHARGE = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGE']/NODE[@ID='WOODSTOVE']/ATTRIBUTES/@SURCHARGE"/>
		</xsl:when>
		<xsl:otherwise>
			1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>		
<!-- For Display Only-->
<xsl:template name="WSTOVE_DISPLAY">
	<xsl:choose>
		<xsl:when test ="WOODSTOVE_SURCHARGE = 'Y'">
			Applied
		</xsl:when>
		<xsl:otherwise>
			0
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>		
<!-- ============================================================================================ -->
<!--				Templates for Charge - Wood Stove [Factor,Credit,Display](END)  			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--				Templates for Surcharges BREEDDOG [Factor,Display](SATRT)		  			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="BREEDDOG">
	<xsl:choose>
		<xsl:when test ="DOGFACTOR = 'Y'">
			((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGE']/NODE[@ID='SPECIFICBREEDOFDOGS']/ATTRIBUTES/@FACTOR"/> * <xsl:value-of select ="DOGSURCHARGE"></xsl:value-of>)* <xsl:call-template name ="CALL_ADJUSTEDBASE"/>)
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template> 

<!-- No OF Dog Surcharge-->
<xsl:template name ="NO_OF_DOGS">
	<xsl:choose>
		<xsl:when test="DOGSURCHARGE > 0">
			(<xsl:value-of select ="DOGSURCHARGE"/>)
		</xsl:when>
		<xsl:otherwise>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name ="DOGS_CHARGE_INPERCENT">
	<xsl:choose>
	
		<xsl:when test="DOGSURCHARGE &gt; 0 or BREEDOFDOG != 'OTH'">
			<xsl:variable name="VAR1"> 
				<xsl:value-of select ="DOGSURCHARGE"/>
			</xsl:variable>
			<xsl:variable name="VAR2"> 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGE']/NODE[@ID='SPECIFICBREEDOFDOGS']/ATTRIBUTES/@SURCHARGE"/>  
			</xsl:variable>
			
			 <xsl:value-of select="$VAR1*$VAR2"/>
			
			
			
		</xsl:when>
		<xsl:otherwise>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>



<!-- ============================================================================================ -->
<!--				Templates for Surcharges BREEDDOG [Factor,Credit,Display](END)  			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--		Templates for Surcharges PREFERRED_PLUS_DISPLAY[HO-20,HO-21,HO-24,HO-25](START)		  -->
<!-- This Template has been created because in PREFERRED_PLUS_DISPLAY option (ADDITIONAL COVERAGES)
	 has been displayed as  'Included' 															  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<!-- For Display Only -->
<xsl:template name="PREFERRED_PLUS_DISPLAY">
	<xsl:choose>
		<!-- For Michigan REPAIR COST -->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Repair Cost'" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Repair Cost'" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<!-- For Michigan REGULAR -->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			Included
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<!-- For Michigan DELUXE  -->
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Deluxe'" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Deluxe'" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<!-- For Michigan PREMIER -->
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Premier'" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Premier'" >
			Included
		</xsl:when>
		<!-- For INDIANA -->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='INDIANA' " >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='INDIANA' " >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='INDIANA' " >
			Included
		</xsl:when>
		<!-- REPAIR COST-->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='INDIANA' and PRODUCT_PREMIER ='Repair Cost'" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='INDIANA' and PRODUCT_PREMIER ='Repair Cost'" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='INDIANA' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='INDIANA' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<!-- DELUXE-->
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='INDIANA' and PRODUCT_PREMIER = 'Deluxe'" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='INDIANA' and PRODUCT_PREMIER = 'Deluxe'" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- For Calculation [Including in Additional Coverages] -->
<xsl:template name="PREFERRED_PLUS_COVERAGE">
	<xsl:choose>
		<!-- For Michigan REPAIR COST -->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Repair Cost'" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Repair Cost'" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<!-- For Michigan REGULAR -->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			0.00
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<!-- For Michigan DELUXE  -->
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Deluxe'" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Deluxe'" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<!-- For Michigan PREMIER -->
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Premier'" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Premier'" >
			0.00
		</xsl:when>
		<!-- For INDIANA -->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='INDIANA' " >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='INDIANA' " >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='INDIANA' " >
			0.00
		</xsl:when>
		<!-- REPAIR COST-->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='INDIANA' and PRODUCT_PREMIER ='Repair Cost'" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='INDIANA' and PRODUCT_PREMIER ='Repair Cost'" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='INDIANA' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='INDIANA' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<!-- DELUXE-->
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='INDIANA' and PRODUCT_PREMIER = 'Deluxe'" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='INDIANA' and PRODUCT_PREMIER = 'Deluxe'" >
			<xsl:call-template name ="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>
		</xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--		Templates for Surcharges PREFERRED_PLUS_DISPLAY[HO-20,HO-21,HO-24,HO-25](END)  		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--				Templates for HO-20 Preferred Plus [Factor](SATRT)				  			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<xsl:template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP">
	<xsl:choose>
		<xsl:when test ="HO20 ='Y' and TERMFACTOR = 12">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO20']/ATTRIBUTES/@ANNUAL"></xsl:value-of>
		</xsl:when>
		<xsl:when test ="HO20 ='Y' and TERMFACTOR = 6">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO20']/ATTRIBUTES/@SEMI-ANNUAL"></xsl:value-of>
		</xsl:when>
		<xsl:when test ="HO21 ='Y' and TERMFACTOR = 12 and STATENAME = 'MICHIGAN'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO21']/ATTRIBUTES/@ANNUAL"></xsl:value-of>
		</xsl:when>
		<xsl:when test ="HO21 ='Y' and TERMFACTOR = 6 and STATENAME = 'MICHIGAN'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO21']/ATTRIBUTES/@SEMI-ANNUAL"></xsl:value-of>
		</xsl:when>
		<xsl:when test ="HO21 ='Y' and TERMFACTOR = 12 and STATENAME = 'INDIANA'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO22']/ATTRIBUTES/@ANNUAL"></xsl:value-of>
		</xsl:when>
		<xsl:when test ="HO21 ='Y' and TERMFACTOR = 6 and STATENAME = 'INDIANA'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO22']/ATTRIBUTES/@SEMI-ANNUAL"></xsl:value-of>
		</xsl:when>
		<xsl:when test ="HO23 ='Y' and TERMFACTOR = 12 and STATENAME = 'INDIANA'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO24']/ATTRIBUTES/@ANNUAL"></xsl:value-of>
		</xsl:when>
		<xsl:when test ="HO23 ='Y' and TERMFACTOR = 6 and STATENAME = 'INDIANA'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO24']/ATTRIBUTES/@SEMI-ANNUAL"></xsl:value-of>
		</xsl:when>
		<xsl:when test ="HO25 ='Y' and TERMFACTOR = 12">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PREMIER_COVERAGE_HO25']/ATTRIBUTES/@ANNUAL"></xsl:value-of>
		</xsl:when>
		<xsl:when test ="HO25 ='Y' and TERMFACTOR = 6">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PREMIER_COVERAGE_HO25']/ATTRIBUTES/@SEMI-ANNUAL"></xsl:value-of>
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	</xsl:choose> 
</xsl:template>

<!-- ============================================================================================ -->
<!--				Templates for HO-20 Preferred Plus [Factor](END)				  			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--				Templates for HO-25 PREMIER VIP COVERAGE [Factor](SATRT)  					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="HO_25_PREMIER_VIP_COVERAGE">
<xsl:choose>
	<xsl:when test ="STATENAME = 'INDIANA'">
	0.00
	</xsl:when>
	<xsl:otherwise>
		<xsl:choose>
			<xsl:when test ="HO25 ='Y' and TERMFACTOR = 12">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PREMIER_COVERAGE_HO25']/ATTRIBUTES/@ANNUAL"></xsl:value-of>
			</xsl:when>
			<xsl:when test ="HO25 ='Y' and TERMFACTOR = 6">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PREMIER_COVERAGE_HO25']/ATTRIBUTES/@SEMI-ANNUAL"></xsl:value-of>
			</xsl:when>
			<xsl:otherwise>
				0.00
			</xsl:otherwise>
		</xsl:choose> 
	</xsl:otherwise>
</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--				Templates for HO-11 REPLACEMENT COST PERSONAL PROPERTY [Factor](SATRT)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY">
	<xsl:choose>
		<xsl:when test ="HO11 ='Y' and TERMFACTOR = 12">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO11']/ATTRIBUTES/@ANNUAL"></xsl:value-of>
		</xsl:when>
		<xsl:when test ="HO11 ='Y' and TERMFACTOR = 6">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO11']/ATTRIBUTES/@SEMI-ANNUAL"></xsl:value-of>
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	</xsl:choose> 
</xsl:template>
<!-- ============================================================================================ -->
<!--				Templates for HO-11 REPLACEMENT COST PERSONAL PROPERTY [Factor](END)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--		Templates for Surcharges PREFERRED_PLUS_DISPLAY[HO-20,HO-21,HO-24,HO-25](START)		  -->
<!-- This Template has been created because in PREFERRED_PLUS_DISPLAY option (ADDITIONAL COVERAGES)
	 has been displayed as  'Included' 															  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<!-- For Display Only -->
<xsl:template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY_DISPLAY">
	<xsl:choose>
		<!-- For Michigan REPAIR COST -->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Repair Cost'" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Repair Cost'" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<!-- For Michigan REGULAR -->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<!-- For Michigan DELUXE  -->
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Deluxe'" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Deluxe'" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<!-- For Michigan PREMIER -->
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Premier'" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Premier'" >
			'Included'
		</xsl:when>
		<!-- For INDIANA -->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='INDIANA' " >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='INDIANA' " >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='INDIANA' " >
			'Included'
		</xsl:when>
		<!-- REPAIR COST-->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='INDIANA' and PRODUCT_PREMIER ='Repair Cost'" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='INDIANA' and PRODUCT_PREMIER ='Repair Cost'" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='INDIANA' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='INDIANA' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<!-- DELUXE-->
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='INDIANA' and PRODUCT_PREMIER = 'Deluxe'" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='INDIANA' and PRODUCT_PREMIER = 'Deluxe'" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- For Calculation [Including in Additional Coverages] -->
<xsl:template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY_COVERAGE">
	<xsl:choose>
		<!-- For Michigan REPAIR COST -->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Repair Cost'" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Repair Cost'" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<!-- For Michigan REGULAR -->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<!-- For Michigan DELUXE  -->
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Deluxe'" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Deluxe'" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<!-- For Michigan PREMIER -->
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Premier'" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Premier'" >
			0.00
		</xsl:when>
		<!-- For INDIANA -->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='INDIANA' " >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='INDIANA' " >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='INDIANA' " >
			0.00
		</xsl:when>
		<!-- REPAIR COST-->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='INDIANA' and PRODUCT_PREMIER ='Repair Cost'" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='INDIANA' and PRODUCT_PREMIER ='Repair Cost'" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='INDIANA' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='INDIANA' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<!-- DELUXE-->
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='INDIANA' and PRODUCT_PREMIER = 'Deluxe'" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='INDIANA' and PRODUCT_PREMIER = 'Deluxe'" >
			<xsl:call-template name ="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>
		</xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--				Templates for HO-34 Replacement Cost Personal Property [Factor](SATRT)		  -->
<!--		    					  FOR MISHIGAN												  -->
<!-- ============================================================================================ -->

<xsl:template name="HO_34_REPLACEMENT_COST">
 <!--
	
	IF('<xsl:value-of select ="HO34"/>' = 'Y') THEN
		<xsl:variable name="PNAME" select="PRODUCTNAME"></xsl:variable>
		<xsl:variable name="P_MINIMUMVALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO34']/ATTRIBUTES[@FORM= $PNAME]/@MINIMUM"/>
		IF (<xsl:call-template name = "HO_34_REPLACEMENT_COST_FACTORVALUE"/> &gt; <xsl:value-of select = "$P_MINIMUMVALUE"/> )
		THEN
			<xsl:call-template name = "HO_34_REPLACEMENT_COST_FACTORVALUE"/> 
			
		ELSE
			<xsl:value-of select = "$P_MINIMUMVALUE"></xsl:value-of> * <xsl:call-template name ="TERM_FACTOR"/>
	
	
	ELSE
		0.00
-->

<!-- new calculation added by praveen singh --> 

 <xsl:choose> 
	<xsl:when test="HO34 = 'Y'">  
		
		<xsl:variable name="PNAME" select="PRODUCTNAME"></xsl:variable>
		
		<xsl:variable name="P_MINIMUMVALUE"> <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO34']/ATTRIBUTES[@FORM= $PNAME]/@MINIMUM"/> </xsl:variable>
		<xsl:variable name="VAR_HO34_RC"><xsl:call-template name = "HO_34_REPLACEMENT_COST_FACTORVALUE"/></xsl:variable>
		<xsl:variable name="VAR_TERM_FACTOR"><xsl:call-template name ="TERM_FACTOR"/> </xsl:variable>
	 
	 	
			<xsl:choose>
				<xsl:when test="$VAR_HO34_RC &gt; $P_MINIMUMVALUE">
						<xsl:value-of select="$VAR_HO34_RC"/>
				</xsl:when>
				<xsl:otherwise> 
					<xsl:value-of select="$P_MINIMUMVALUE * $VAR_TERM_FACTOR"/> 
				</xsl:otherwise>
				
			</xsl:choose>
			
	</xsl:when>
	<xsl:otherwise>0.00</xsl:otherwise>
</xsl:choose>


</xsl:template>

<!-- ============================================================================================ -->
<!--				Templates for HO-34 Replacement Cost Personal Property [Factor](END)		  -->
<!--		    					  FOR MISHIGAN												  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--				Templates for HO-34 Replacement Cost Personal Property [Factor](SATRT)		  -->
<!--		    					  FOR INDIANA												  -->
<!-- ============================================================================================ -->

<xsl:template name="HO_34_REPLACEMENT_COST_INDINA">

	IF('<xsl:value-of select ="HO34"/>' = 'Y') THEN
		<xsl:variable name="PNAME" select="PRODUCTNAME"></xsl:variable>
		<!-- SEMI_ANNUAL_MIMIMUM -->
		<xsl:variable name="P_SEMI_ANNUAL_MIMIMUM"  select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO34']/ATTRIBUTES[@FORM= $PNAME]/@SEMI_ANNUAL_MIMIMUM"/>
		<!-- SEMI_ANNUAL_MAXIMUM -->
		<xsl:variable name="P_SEMI_ANNUAL_MAXIMUM"  select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO34']/ATTRIBUTES[@FORM= $PNAME]/@SEMI_ANNUAL_MAXIMUM"/>
		<!-- ANNUAL_MINIMUM -->
		<xsl:variable name="P_ANNUAL_MIMIMUM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO34']/ATTRIBUTES[@FORM = 'HO-3']/@ANNUAL_MINIMUM"/>
		<!-- ANNUAL_MAXIMUM -->
		<xsl:variable name="P_ANNUAL_MAXIMUM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO34']/ATTRIBUTES[@FORM = 'HO-3']/@ANNUAL_MAXIMUM"/>
		
		<xsl:choose>
			<xsl:when test ="TERMFACTOR = 12">
				IF (<xsl:call-template name = "HO_34_REPLACEMENT_COST_FACTORVALUE"/> &gt; <xsl:value-of select ="$P_ANNUAL_MIMIMUM"/> and <xsl:call-template name = "HO_34_REPLACEMENT_COST_FACTORVALUE"/> &lt; <xsl:value-of select ="$P_ANNUAL_MAXIMUM"/>)
				THEN
					<xsl:call-template name = "HO_34_REPLACEMENT_COST_FACTORVALUE"/> 
				ELSE IF (<xsl:call-template name = "HO_34_REPLACEMENT_COST_FACTORVALUE"/> &gt; <xsl:value-of select ="$P_ANNUAL_MAXIMUM"/>) THEN
					<xsl:value-of select ="$P_ANNUAL_MAXIMUM"/>
				ELSE
					<xsl:value-of select = "$P_ANNUAL_MIMIMUM"></xsl:value-of> 
			</xsl:when>
			<xsl:otherwise>
				IF (<xsl:call-template name = "HO_34_REPLACEMENT_COST_FACTORVALUE"/> &gt; <xsl:value-of select ="$P_SEMI_ANNUAL_MIMIMUM"/> and  <xsl:call-template name = "HO_34_REPLACEMENT_COST_FACTORVALUE"/> &lt; <xsl:value-of select ="$P_SEMI_ANNUAL_MAXIMUM"/>)
				THEN
					<xsl:call-template name = "HO_34_REPLACEMENT_COST_FACTORVALUE"/> 
				ELSE IF (<xsl:call-template name = "HO_34_REPLACEMENT_COST_FACTORVALUE"/> &gt; <xsl:value-of select ="$P_SEMI_ANNUAL_MAXIMUM"/>) THEN
					<xsl:value-of select ="$P_SEMI_ANNUAL_MAXIMUM"/>
				ELSE
					<xsl:value-of select = "$P_SEMI_ANNUAL_MIMIMUM"/>
			</xsl:otherwise>
		</xsl:choose>
	ELSE
		0.00
		
</xsl:template>

<xsl:template name ="SELECT_HO_34">
	<xsl:choose>
		<xsl:when test ="STATENAME = 'INDIANA'">
			<xsl:call-template name="HO_34_REPLACEMENT_COST_INDINA"/>
		</xsl:when>
		<xsl:otherwise>
			<xsl:call-template name="HO_34_REPLACEMENT_COST"/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name = "HO_34_REPLACEMENT_COST_FACTORVALUE">
	<xsl:variable name="PNAME" select="PRODUCTNAME"/>
	(<xsl:call-template name="CALL_ADJUSTEDBASE"/>) * 
	(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO34']/ATTRIBUTES[@FORM= $PNAME]/@FACTOR"/>)
</xsl:template>

<!-- ============================================================================================ -->
<!--				Templates for HO-34 Replacement Cost Personal Property [Factor](END)		  -->
<!--		    					  FOR INDIANA												  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--		Templates for Surcharges PREFERRED_PLUS_DISPLAY[HO-20,HO-21,HO-24,HO-25](START)		  -->
<!-- This Template has been created because in PREFERRED_PLUS_DISPLAY option (ADDITIONAL COVERAGES)
	 has been displayed as  'Included' 															  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<!-- For Display Only -->
<xsl:template name="SELECT_HO_34_DISPLAY">
	<xsl:choose>
		<!-- For Michigan REPAIR COST -->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Repair Cost'" >
			<xsl:call-template name="HO_34_REPLACEMENT_COST"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Repair Cost'" >
			<xsl:call-template name="HO_34_REPLACEMENT_COST"/>
		</xsl:when>
		<!-- For Michigan REGULAR -->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name="HO_34_REPLACEMENT_COST"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name="HO_34_REPLACEMENT_COST"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name="HO_34_REPLACEMENT_COST"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name="HO_34_REPLACEMENT_COST"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name="HO_34_REPLACEMENT_COST"/>
		</xsl:when>
		<!-- For Michigan DELUXE  -->
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Deluxe'" >
			<xsl:call-template name="HO_34_REPLACEMENT_COST"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Deluxe'" >
			<xsl:call-template name="HO_34_REPLACEMENT_COST"/>
		</xsl:when>
		<!-- For Michigan PREMIER -->
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Premier'" >
			<xsl:call-template name="HO_34_REPLACEMENT_COST"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Premier'" >
			'Included'
		</xsl:when>
		<!-- For INDIANA -->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='INDIANA' " >
			<xsl:call-template name="HO_34_REPLACEMENT_COST_INDINA"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='INDIANA' " >
			<xsl:call-template name="HO_34_REPLACEMENT_COST_INDINA"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='INDIANA' " >
			'Included'
		</xsl:when>
		<!-- REPAIR COST-->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='INDIANA' and PRODUCT_PREMIER ='Repair Cost'" >
			<xsl:call-template name="HO_34_REPLACEMENT_COST_INDINA"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='INDIANA' and PRODUCT_PREMIER ='Repair Cost'" >
			<xsl:call-template name="HO_34_REPLACEMENT_COST_INDINA"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='INDIANA' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name="HO_34_REPLACEMENT_COST_INDINA"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='INDIANA' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name="HO_34_REPLACEMENT_COST_INDINA"/>
		</xsl:when>
		<!-- DELUXE-->
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='INDIANA' and PRODUCT_PREMIER = 'Deluxe'" >
			<xsl:call-template name="HO_34_REPLACEMENT_COST_INDINA"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='INDIANA' and PRODUCT_PREMIER = 'Deluxe'" >
			<xsl:call-template name="HO_34_REPLACEMENT_COST_INDINA"/>
		</xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- For Calculation [Including in Additional Coverages] -->
<xsl:template name="SELECT_HO_34_COVERAGE">
	<xsl:choose>
			<!-- For Michigan REPAIR COST -->
			<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Repair Cost'" >
				<xsl:call-template name="HO_34_REPLACEMENT_COST"/>
			</xsl:when>
			<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Repair Cost'" >
				<xsl:call-template name="HO_34_REPLACEMENT_COST"/>
			</xsl:when>
			<!-- For Michigan REGULAR -->
			<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
				<xsl:call-template name="HO_34_REPLACEMENT_COST"/>
			</xsl:when>
			<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
				<xsl:call-template name="HO_34_REPLACEMENT_COST"/>
			</xsl:when>
			<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
				<xsl:call-template name="HO_34_REPLACEMENT_COST"/>
			</xsl:when>
			<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
				<xsl:call-template name="HO_34_REPLACEMENT_COST"/>
			</xsl:when>
			<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
				<xsl:call-template name="HO_34_REPLACEMENT_COST"/>
			</xsl:when>
			<!-- For Michigan DELUXE  -->
			<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Deluxe'" >
				<xsl:call-template name="HO_34_REPLACEMENT_COST"/>
			</xsl:when>
			<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Deluxe'" >
				<xsl:call-template name="HO_34_REPLACEMENT_COST"/>
			</xsl:when>
			<!-- For Michigan PREMIER -->
			<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Premier'" >
				<xsl:call-template name="HO_34_REPLACEMENT_COST"/>
			</xsl:when>
			<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Premier'" >
				0.00
			</xsl:when>
			<!-- For INDIANA -->
			<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='INDIANA' " >
				<xsl:call-template name="HO_34_REPLACEMENT_COST_INDINA"/>
			</xsl:when>
			<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='INDIANA' " >
				<xsl:call-template name="HO_34_REPLACEMENT_COST_INDINA"/>
			</xsl:when>
			<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='INDIANA' " >
				0.00
			</xsl:when>
			<!-- REPAIR COST-->
			<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='INDIANA' and PRODUCT_PREMIER ='Repair Cost'" >
				<xsl:call-template name="HO_34_REPLACEMENT_COST_INDINA"/>
			</xsl:when>
			<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='INDIANA' and PRODUCT_PREMIER ='Repair Cost'" >
				<xsl:call-template name="HO_34_REPLACEMENT_COST_INDINA"/>
			</xsl:when>
			<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='INDIANA' and PRODUCT_PREMIER = ''" >
				<xsl:call-template name="HO_34_REPLACEMENT_COST_INDINA"/>
			</xsl:when>
			<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='INDIANA' and PRODUCT_PREMIER = ''" >
				<xsl:call-template name="HO_34_REPLACEMENT_COST_INDINA"/>
			</xsl:when>
			<!-- DELUXE-->
			<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='INDIANA' and PRODUCT_PREMIER = 'Deluxe'" >
				<xsl:call-template name="HO_34_REPLACEMENT_COST_INDINA"/>
			</xsl:when>
			<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='INDIANA' and PRODUCT_PREMIER = 'Deluxe'" >
				<xsl:call-template name="HO_34_REPLACEMENT_COST_INDINA"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--		Templates for Surcharges PREFERRED_PLUS_DISPLAY[HO-20,HO-21,HO-24,HO-25](END)		  -->
<!-- This Template has been created because in PREFERRED_PLUS_DISPLAY option (ADDITIONAL COVERAGES)
	 has been displayed as  'Included' 															  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--				Templates for HO-96 REPLACEMENT COST PERSONAL PROPERTY [Factor](SATRT)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="HO_96_REPLACEMENT_COST_PERSONAL_PROPERTY">

	<xsl:variable name ="PHO96FINALVALUE" select ="HO96FINALVALUE"></xsl:variable>	
	<xsl:choose>
	<xsl:when test ="$PHO96FINALVALUE = 0.00">
	0.00
	</xsl:when>
	<xsl:otherwise>
			<xsl:choose>
				<xsl:when test ="TERMFACTOR = 12">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO96']/ATTRIBUTES[@LIMIT = $PHO96FINALVALUE]/@ANNUAL"></xsl:value-of>
				</xsl:when>
				<xsl:when test ="TERMFACTOR = 6">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO96']/ATTRIBUTES[@LIMIT = $PHO96FINALVALUE]/@SEMI-ANNUAL"></xsl:value-of>
				</xsl:when>
				<xsl:otherwise>
					0.00
				</xsl:otherwise>
			</xsl:choose> 
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--				Templates for HO-96 REPLACEMENT COST PERSONAL PROPERTY [Factor](END)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--				Templates for HO-312 BUSINESS PROPERTY INCREASED LIMITS [Factor](START)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="HO_312_BUSINESS_PROPERTY_INCREASED_LIMITS">
	<xsl:variable name ="PHO312VALUE" select ="HO312ADDITIONAL"></xsl:variable>
	<xsl:variable name ="PHO312COST" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='BUSINESS_PROPERTY_INCREASED_LIMITS_HO312']/ATTRIBUTES/@COST"></xsl:variable>
	<xsl:variable name ="PHO312INCREASEDAMOUNT" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='BUSINESS_PROPERTY_INCREASED_LIMITS_HO312']/ATTRIBUTES/@INCREASEDAMOUNT"></xsl:variable>
	
	((<xsl:value-of select ="$PHO312COST"></xsl:value-of>*
	<xsl:value-of select ="$PHO312VALUE"></xsl:value-of>) DIV
	<xsl:value-of select ="$PHO312INCREASEDAMOUNT"></xsl:value-of>)
	
</xsl:template>

<!-- ============================================================================================ -->
<!--				Templates for HO-312 BUSINESS PROPERTY INCREASED LIMITS [Factor](END)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--				Templates for HO-48 OTHER STRUCTURE INCREASED LIMITS [Factor](START)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="HO_48_OTHER_STRUCTURE_INCREASED_LIMITS">
	<xsl:variable name ="PHO48VALUE" select ="HO48ADDITIONAL"></xsl:variable>
	<xsl:variable name ="PHO48COST" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_INCREASED_LIMITS_HO48']/ATTRIBUTES/@COST"></xsl:variable>
	<xsl:variable name ="PHO48INCREASEDAMOUNT" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_INCREASED_LIMITS_HO48']/ATTRIBUTES/@INCREASEDAMOUNT"></xsl:variable>
	
	((<xsl:value-of select ="$PHO48COST"></xsl:value-of>*
	<xsl:value-of select ="$PHO48VALUE"></xsl:value-of>) DIV
	<xsl:value-of select ="$PHO48INCREASEDAMOUNT"></xsl:value-of>)
	
</xsl:template>
<!-- ============================================================================================ -->
<!--				Templates for HO-48 OTHER STRUCTURE INCREASED LIMITS [Factor](END)			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--				Templates for HO-40 OTHER STRUCTURE RENTED TO OTHERS [Factor](START)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="HO_40_OTHER_STRUCTURE_RENTED_TO_OTHERS">
	<xsl:variable name ="PHO40VALUE" select ="HO40ADDITIONAL"></xsl:variable>
	<xsl:variable name ="PHO40COST" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_RENTED_TO_OTHERS_HO40']/ATTRIBUTES/@COST"></xsl:variable>
	<xsl:variable name ="PHO40INCREASEDAMOUNT" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_RENTED_TO_OTHERS_HO40']/ATTRIBUTES/@INCREASEDAMOUNT"></xsl:variable>
	
	((<xsl:value-of select ="$PHO40COST"></xsl:value-of>*
	<xsl:value-of select ="$PHO40VALUE"></xsl:value-of>) DIV
	<xsl:value-of select ="$PHO40INCREASEDAMOUNT"></xsl:value-of>)
	
</xsl:template>
<!-- ============================================================================================ -->
<!--				Templates for HO-40 OTHER STRUCTURE RENTED TO OTHERS [Factor](END)			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--				Templates for HO-42 OTHER STRUCTURE WITH INCIDENTAL  [Factor](START)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="HO_42_OTHER_STRUCTURE_WITH_INCIDENTAL">
	<xsl:choose>
		<xsl:when test = "HO42ADDITIONAL = 0">
			0.00
		</xsl:when>
		<xsl:otherwise>
			<xsl:variable name ="PHO42VALUE" select ="HO42ADDITIONAL"></xsl:variable>
			<xsl:variable name ="PHO42COST" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_WITH_INCIDENTAL_HO42']/ATTRIBUTES/@COST"></xsl:variable>
			<xsl:variable name ="PHO42INCREASEDAMOUNT" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_WITH_INCIDENTAL_HO42']/ATTRIBUTES/@INCREASEDAMOUNT"></xsl:variable>
			
			((<xsl:value-of select ="$PHO42COST"></xsl:value-of>*
			<xsl:value-of select ="$PHO42VALUE"></xsl:value-of>) DIV
			<xsl:value-of select ="$PHO42INCREASEDAMOUNT"></xsl:value-of>)
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--				Templates for HO-42 OTHER STRUCTURE WITH INCIDENTAL  [Factor](END)			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--				Template for HO-53 INCREASED CREDIT CARD [Factor](START)					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="HO_53_INCREASED_CREDIT_CARD">
	<xsl:choose>
		<xsl:when test = "HO53ADDITIONAL = 0">
			0.00
		</xsl:when>
		<xsl:otherwise>
			<xsl:variable name ="PHO53VALUE" select ="HO53ADDITIONAL"></xsl:variable>
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_CREDIT_CARD']/ATTRIBUTES[@LIMIT = $PHO53VALUE]/@COST"></xsl:value-of>
		</xsl:otherwise>
	</xsl:choose>
	
</xsl:template>
<!-- ============================================================================================ -->
<!--				Template for HO-53 INCREASED CREDIT CARD [Factor](START)					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--				Template for UNSCHEDULED_JEWELRY_ADDITIONAL [Additional](START)				  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="UNSCHEDULED_JEWELRY_ADDITIONAL">
	<xsl:variable name ="PJEWELRYVALUE" select ="UNSCHEDULEDJEWELRYADDITIONAL"></xsl:variable>
	<xsl:variable name ="PJEWELRYCOST" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='A']/@COST"></xsl:variable>
	<xsl:variable name ="PJEWELRYINCREASEDAMOUNT" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='A']/@INCREMENT_AMOUNT"></xsl:variable>
	
	((<xsl:value-of select ="$PJEWELRYCOST"></xsl:value-of>*
	<xsl:value-of select ="$PJEWELRYVALUE"></xsl:value-of>) DIV
	<xsl:value-of select ="$PJEWELRYINCREASEDAMOUNT"></xsl:value-of>)
	
</xsl:template>
<!-- ============================================================================================ -->
<!--				Template for UNSCHEDULED_JEWELRY_ADDITIONAL [Additional](END)				  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--							Template for MONEY [Additional](START)							  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="MONEY">
	<xsl:variable name ="PMONEYVALUE" select ="MONEYADDITIONAL"></xsl:variable>
	<xsl:variable name ="PMONEYCOST" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='B']/@COST"></xsl:variable>
	<xsl:variable name ="PMONEYINCREASEDAMOUNT" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='B']/@INCREMENT_AMOUNT"></xsl:variable>
	
	((<xsl:value-of select ="$PMONEYCOST"></xsl:value-of>*
	<xsl:value-of select ="$PMONEYVALUE"></xsl:value-of>) DIV
	<xsl:value-of select ="$PMONEYINCREASEDAMOUNT"></xsl:value-of>)
	
</xsl:template>
<!-- ============================================================================================ -->
<!--							Template for MONEY [Additional](END)							  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--							Template for SECURITIES [Additional](START)						  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="SECURITIES">
	<xsl:variable name ="PSECURITIESVALUE" select ="SECURITIESADDITIONAL"></xsl:variable>
	<xsl:variable name ="PSECURITIESCOST" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='C']/@COST"></xsl:variable>
	<xsl:variable name ="PSECURITIESINCREASEDAMOUNT" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='C']/@INCREMENT_AMOUNT"></xsl:variable>
	
	((<xsl:value-of select ="$PSECURITIESCOST"></xsl:value-of>*
	<xsl:value-of select ="$PSECURITIESVALUE"></xsl:value-of>) DIV
	<xsl:value-of select ="$PSECURITIESINCREASEDAMOUNT"></xsl:value-of>)
	
</xsl:template>
<!-- ============================================================================================ -->
<!--							Template for SECURITIES [Additional](END)						  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--					Template for SILVERWARE, GOLDWARE [Additional](START)					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="SILVERWARE_GOLDWARE">
	<xsl:variable name ="PSILVERWARE_GOLDWAREVALUE" select ="SILVERWAREADDITIONAL"></xsl:variable>
	<xsl:variable name ="PSILVERWARE_GOLDWARECOST" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='D']/@COST"></xsl:variable>
	<xsl:variable name ="PSILVERWARE_GOLDWAREINCREASEDAMOUNT" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='D']/@INCREMENT_AMOUNT"></xsl:variable>
	
	((<xsl:value-of select ="$PSILVERWARE_GOLDWARECOST"></xsl:value-of>*
	<xsl:value-of select ="$PSILVERWARE_GOLDWAREVALUE"></xsl:value-of>) DIV
	<xsl:value-of select ="$PSILVERWARE_GOLDWAREINCREASEDAMOUNT"></xsl:value-of>)
	
</xsl:template>
<!-- ============================================================================================ -->
<!--					Template for SILVERWARE, GOLDWARE [Additional](END)						  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--							Template for FIREARMS[Additional](START)						  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="FIREARMS">
	<xsl:variable name ="PFIREARMSVALUE" select ="FIREARMSADDITIONAL"></xsl:variable>
	<xsl:variable name ="PFIREARMSCOST" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='E']/@COST"></xsl:variable>
	<xsl:variable name ="PFIREARMSINCREASEDAMOUNT" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='E']/@INCREMENT_AMOUNT"></xsl:variable>
	
	((<xsl:value-of select ="$PFIREARMSCOST"></xsl:value-of>*
	<xsl:value-of select ="$PFIREARMSVALUE"></xsl:value-of>) DIV
	<xsl:value-of select ="$PFIREARMSINCREASEDAMOUNT"></xsl:value-of>)
	
</xsl:template>
<!-- ============================================================================================ -->
<!--							Template for FIREARMS[Additional](END)							  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--					Templates for HO-277 Ordinance or Law[Additional](START)				  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="ORDINANCE">
	IF(<xsl:value-of select ="HO277 = 'Y'"/>) THEN
		<xsl:variable name="P_MINIMUMVALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='LAW_COVERAGE']/ATTRIBUTES/@MINIMUM"/>
		
		IF(<xsl:call-template name = "ORDINANCE_COST_FACTORVALUE"></xsl:call-template> &gt; <xsl:value-of select = "$P_MINIMUMVALUE"></xsl:value-of> )
		THEN
			<xsl:call-template name = "ORDINANCE_COST_FACTORVALUE"/>
		ELSE
			<xsl:value-of select = "$P_MINIMUMVALUE"></xsl:value-of>
	ELSE
		0
</xsl:template>

<xsl:template name = "ORDINANCE_COST_FACTORVALUE">
	(<xsl:call-template name="CALL_ADJUSTEDBASE"></xsl:call-template>) * <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='LAW_COVERAGE']/ATTRIBUTES/@CHARGE"/>
</xsl:template>

<!-- ============================================================================================ -->
<!--					Templates for HO-277 Ordinance or Law[Additional](END)					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--   Templates for HO-455 Identity Fraud Expense Coverage[Factor, Limit, DEductible](SATRT)	  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="HO_455_IDENTITY_FRAUD">
	<xsl:choose>
		<xsl:when test = "HO455 = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='FRAUD_EXPENSE']/ATTRIBUTES/@RATE"/> * <xsl:call-template name ="TERM_FACTOR"/>
		</xsl:when>
		<xsl:otherwise>
		0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!--LIMITS -->
<xsl:template name="HO_455_LIMIT">
	<xsl:choose>
		<xsl:when test = "HO455 = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='FRAUD_EXPENSE']/ATTRIBUTES/@COVERAGE"/>
		</xsl:when>
		<xsl:otherwise>
		0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- DEDUCTIBLE -->
<xsl:template name="HO_455_DEDUCTIBLE">
	<xsl:choose>
		<xsl:when test = "HO455 = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='FRAUD_EXPENSE']/ATTRIBUTES/@DEDUCTIBLE"/>
		</xsl:when>
		<xsl:otherwise>
		0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--   Templates for HO-455 Identity Fraud Expense Coverage[Factor, Limit, DEductible](SATRT)	  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--				Templates for HO-315 Earthquake (Zone 0)[Additional](SATRT)					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="HO_315_EARTHQUAKE">
	
		<!-- Variables for FORM, ZONE, INCREASEDCOVERAGEC, INCREASEDCOVGD -->
		<xsl:variable name ="PEXTERIOR_CONSTRUCTION_F_M" select="EXTERIOR_CONSTRUCTION_F_M"></xsl:variable>
		<xsl:variable name ="PPERSONALPROPERTYINCREASEDLIMITADDITIONAL" select="PERSONALPROPERTYINCREASEDLIMITADDITIONAL"></xsl:variable>
		<xsl:variable name ="PADDITIONALLIVINGEXPENSEADDITIONAL" select="ADDITIONALLIVINGEXPENSEADDITIONAL"></xsl:variable>
		<xsl:variable name ="PFORM" select="PRODUCTNAME"></xsl:variable>	
		<xsl:variable name ="PZONE" select ="TERRITORYZONE"/>
		
		<!--IF('<xsl:value-of select="HO315"/>' = 'Y') THEN	-->
		<xsl:choose>
			<xsl:when test ="HO315 = 'Y'">
				
					<!--	1. Get Rate for Frame per thousand by policy form and construction
							2. Multiply Rate with Coverage A Limit
							3. Multiply Rate with increased coverage C limit 
							4. Multiply Rate with increased coverage D limit 
							5. Add Steps 2. , 3. , 4. -->
					

					IF('<xsl:value-of select="$PEXTERIOR_CONSTRUCTION_F_M"/>' ='F')
					THEN 
						<!--<xsl:value-of select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='EARTHQUAKE']/ATTRIBUTES[@FORM='$PFORM' and @ZONE='$PZONE']/@FRAME"/>* (<xsl:value-of select ="DWELLING_LIMITS"/> DIV 1000) -->
																						
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
			</xsl:when>
			<xsl:otherwise>
			0.00
			</xsl:otherwise>
		</xsl:choose>	
</xsl:template>

<xsl:template name="FETCHZONE">	
	<xsl:choose>
		<xsl:when test="STATENAME='MICHIGAN'">
		1
		</xsl:when>
		<xsl:when test="STATENAME='INDIANA'"> <!--CHECK THIS LATER (ZONE FOR OTHER STATES)-->
		1
		</xsl:when>
		<xsl:otherwise>
		1
		</xsl:otherwise>
	</xsl:choose>			
</xsl:template>
<!-- ============================================================================================ -->
<!--				Templates for HO-315 Earthquake (Zone 0)[Additional](END)					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--				Template for HO-327 Water Back-Up and Sump Pump Overflow(START)				  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="HO_327_WATER_BACK_UP">
	<xsl:choose>
		<xsl:when test ="HO327 = 0">
		0
		</xsl:when>
		<xsl:otherwise>
			<xsl:variable name ="PHO327VALUE" select ="HO327"></xsl:variable>
			<xsl:variable name ="PHO327COST" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='WATER_BACKUP']/ATTRIBUTES/@COST"></xsl:variable>
			<xsl:variable name ="PHO327INCREASEDAMOUNT" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='WATER_BACKUP']/ATTRIBUTES/@INCREMENTAL_AMOUNT"></xsl:variable>
			
			((<xsl:value-of select ="$PHO327COST"></xsl:value-of>*
			<xsl:value-of select ="$PHO327VALUE"></xsl:value-of>) DIV
			<xsl:value-of select ="$PHO327INCREASEDAMOUNT"></xsl:value-of>) * <xsl:call-template name ="TERM_FACTOR"/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--				Template for HO-327 Water Back-Up and Sump Pump Overflow(END)				  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--				Template for  HO-33 Condo-Unit Owners Rental to Others(START)				  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="HO_33_CONDO_UNIT">
	<xsl:choose>
		<xsl:when test ="HO33 = 'Not Rented'">
		0
		</xsl:when>
		<xsl:when test ="HO33 = 'Under 2 Months'">
			((<xsl:call-template name ="CALL_ADJUSTEDBASE"/>) * (<xsl:value-of select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID = 'OPTIONALSECTIONCOVG']/NODE[@ID='HO_33_CONDO_UNIT']/ATTRIBUTES[@OPTIONS = '1']/@FACTOR"></xsl:value-of>))
		</xsl:when>
		<xsl:when test ="HO33 = '2 to 6 Months'">
			((<xsl:call-template name ="CALL_ADJUSTEDBASE"/>) * (<xsl:value-of select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID = 'OPTIONALSECTIONCOVG']/NODE[@ID='HO_33_CONDO_UNIT']/ATTRIBUTES[@OPTIONS = '2']/@FACTOR"></xsl:value-of>))
		</xsl:when>
		<xsl:otherwise>
		0	
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--				Template for  HO-33 Condo-Unit Owners Rental to Others(END)					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--				Template for HO-287 Mine Subsidence Coverage (Start )						  -->
<!--		    					  FOR INDIANA												  -->
<!-- ============================================================================================ -->
<xsl:template name = "HO_287_MINE_SUBSIDENCE_COVERAGE">
	<xsl:variable name = "PCOVERAGES" select ="DWELLING_LIMITS"/>
	<xsl:choose>
		<xsl:when test ="STATENAME = 'INDIANA'">
			<xsl:choose>
				<xsl:when test ="HO287 = 'Y'">
					<xsl:value-of select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID = 'OTHER']/NODE[@ID='MINE_SUBSIDENCE_COVERAGE']/ATTRIBUTES[@MINCOVERAGE &lt;= $PCOVERAGES and @MAXCOVERAGE &gt;= $PCOVERAGES]/@PREMIUM"></xsl:value-of>
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
<!--				Template for HO-287 Mine Subsidence Coverage (End)							  -->
<!--		    					  FOR INDIANA												  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--				Template for HO-9 Collapse From Sub Surface Water (Start )					  -->
<!--		    					  FOR INDIANA												  -->
<!-- ============================================================================================ -->
<xsl:template name ="HO_9_COLLAPSE_FROM_SUB_SURFACE_WATER">
	<xsl:choose>
		<xsl:when test ="STATENAME = 'INDIANA'">
			<xsl:choose>
				<xsl:when test ="HO9 = 'Y'">
					<xsl:value-of select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID = 'COLLAPSE_FROM_SUB_SURFACE_WATER_HO9']/ATTRIBUTES/@COST"/>
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

<xsl:template name ="HO_200_WATERBED_LIABILITY">
	<xsl:choose>
		<xsl:when test ="STATENAME = 'INDIANA'">
			<xsl:choose>
				<xsl:when test ="HO200 = 'Y'">
						IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 100000.00) THEN
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='WATERBED_LIABILITY']/@LIABILITY_LIMIT_G1"/>
						ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 300000.00) THEN
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='WATERBED_LIABILITY']/@LIABILITY_LIMIT_G2"/>
						ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 500000.00) THEN
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='WATERBED_LIABILITY']/@LIABILITY_LIMIT_G3"/>
						ELSE
							0
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
<!--				Template for HO-200 Waterbed Liability (End )								  -->
<!--		    					  FOR INDIANA												  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--							Template for HO-490 SPECIFIC STRUCTURED (START)					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="HO_490_SPECIFIC_STRUCTURED">

	<xsl:choose>
		<xsl:when test = "SPECIFICSTRUCTURESADDITIONAL = 0.00">
		0.00
		</xsl:when>
		<xsl:otherwise>
			<xsl:variable name ="PHO490VALUE" select ="SPECIFICSTRUCTURESADDITIONAL"></xsl:variable>
			<xsl:variable name ="PHO490COST" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='SPECIFIC_STRUC_AWAY']/ATTRIBUTES/@RATE"></xsl:variable>
			<xsl:variable name ="PHO490INCREASEDAMOUNT" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='SPECIFIC_STRUC_AWAY']/ATTRIBUTES/@PER_EACH_ADDITION_OF"></xsl:variable>
			
			((<xsl:value-of select ="$PHO490COST"></xsl:value-of>*
			<xsl:value-of select ="$PHO490VALUE"></xsl:value-of>) DIV
			<xsl:value-of select ="$PHO490INCREASEDAMOUNT"></xsl:value-of>)
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--							Template for HO-490 SPECIFIC STRUCTURED (END)					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--							Template for HO-489 SPECIFIC STRUCTURED (START)					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="HO_489_SPECIFIC_STRUCTURED">
	<xsl:variable name ="PHO489VALUE" select ="REPAIRCOSTADDITIONAL"></xsl:variable>
	<xsl:variable name ="PHO489COST" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUC_REPAIR']/ATTRIBUTES/@RATE"></xsl:variable>
	<xsl:variable name ="PHO489INCREASEDAMOUNT" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUC_REPAIR']/ATTRIBUTES/@PER_EACH_ADDITION_OF"></xsl:variable>
	
	((<xsl:value-of select ="$PHO489COST"></xsl:value-of>*
	<xsl:value-of select ="$PHO489VALUE"></xsl:value-of>) DIV
	<xsl:value-of select ="$PHO489INCREASEDAMOUNT"></xsl:value-of>)
	
</xsl:template>
<!-- ============================================================================================ -->
<!--							Template for HO-489 SPECIFIC STRUCTURED (END)					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--							Template for PRIMARY RESIDENCE (START)							  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="PRIMARY_RESIDENCE">
	IF(<xsl:value-of select="RESIDENCE_EMP_NUMBER"/> = 0) THEN
		0.00
	ELSE
		IF(<xsl:value-of select="RESIDENCE_EMP_NUMBER"/> &lt;= 2) THEN
			IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 100000.00) THEN
				<xsl:value-of select="RESIDENCE_EMP_NUMBER"/> * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='PRIMARY_RESIDENCE']/@LIABILITY_LIMIT_G1"/>) +((<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='PRIMARY_RESIDENCE']/@FOR_EACH_ADDITION"/>))DIV 1000.00
			ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 300000.00) THEN
				<xsl:value-of select="RESIDENCE_EMP_NUMBER"/> *(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='PRIMARY_RESIDENCE']/@LIABILITY_LIMIT_G2"/>) + ((<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='PRIMARY_RESIDENCE']/@FOR_EACH_ADDITION"/>)) DIV 1000.00 
			ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 500000.00) THEN
				<xsl:value-of select="RESIDENCE_EMP_NUMBER"/> *(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='PRIMARY_RESIDENCE']/@LIABILITY_LIMIT_G3"/>) + ((<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='PRIMARY_RESIDENCE']/@FOR_EACH_ADDITION"/>)) DIV 1000.00 
			ELSE
				0.00
		ELSE
			0.00
	
</xsl:template>
<!-- ============================================================================================ -->
<!--							Template for PRIMARY RESIDENCE (END)							  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--							Template for RESIDENCE EMPLOYEES (START)						  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="RESIDENCE_EMPLOYEES">
	IF(<xsl:value-of select="RESIDENCE_EMP_NUMBER"/> = 0) THEN
		0.00
	ELSE
		IF(<xsl:value-of select="RESIDENCE_EMP_NUMBER"/> &gt; 2) THEN
			IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 100000.00) THEN
				(<xsl:value-of select="RESIDENCE_EMP_NUMBER"/> - 2) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='RESIDENCE_EMPLOYEES']/@LIABILITY_LIMIT_G1"/>) +((<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='RESIDENCE_EMPLOYEES']/@FOR_EACH_ADDITION"/>))DIV 1000.00
			ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 300000.00) THEN
				(<xsl:value-of select="RESIDENCE_EMP_NUMBER"/> - 2)*(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='RESIDENCE_EMPLOYEES']/@LIABILITY_LIMIT_G2"/>) + ((<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='RESIDENCE_EMPLOYEES']/@FOR_EACH_ADDITION"/>)) DIV 1000.00 
			ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 500000.00) THEN
				(<xsl:value-of select="RESIDENCE_EMP_NUMBER"/> - 2) *(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='RESIDENCE_EMPLOYEES']/@LIABILITY_LIMIT_G3"/>) + ((<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='RESIDENCE_EMPLOYEES']/@FOR_EACH_ADDITION"/>)) DIV 1000.00 
			ELSE
				0.00
		ELSE
			0.00
</xsl:template>
<!-- ============================================================================================ -->
<!--							Template for RESIDENCE EMPLOYEES (END)							  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--				Template for Additional Premises Occupied By Insured (START)				  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="ADDITIONAL_PREMISES">
IF(<xsl:value-of select="OCCUPIED_INSURED"/> = 0.00) THEN
	0.00
ELSE 
	IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 100000.00) THEN
		<xsl:value-of select="OCCUPIED_INSURED"/> * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='ADD_PREMISES_OCC_INSURED']/@LIABILITY_LIMIT_G1"/>) + (((<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='ADD_PREMISES_OCC_INSURED']/@FOR_EACH_ADDITION"/>))DIV 1000.00) * (<xsl:value-of select="OCCUPIED_INSURED"/>)
	ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 300000.00) THEN
		<xsl:value-of select="OCCUPIED_INSURED"/> *(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='ADD_PREMISES_OCC_INSURED']/@LIABILITY_LIMIT_G2"/>) +  (((<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='ADD_PREMISES_OCC_INSURED']/@FOR_EACH_ADDITION"/>))DIV 1000.00) * (<xsl:value-of select="OCCUPIED_INSURED"/>) 
	ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 500000.00) THEN
		<xsl:value-of select="OCCUPIED_INSURED"/> *(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='ADD_PREMISES_OCC_INSURED']/@LIABILITY_LIMIT_G3"/>) +  (((<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='ADD_PREMISES_OCC_INSURED']/@FOR_EACH_ADDITION"/>))DIV 1000.00)  * (<xsl:value-of select="OCCUPIED_INSURED"/>)
	ELSE
		0.00
</xsl:template>

<!-- ============================================================================================ -->
<!--				Template for Additional Premises Occupied By Insured (END)					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--	Template for HO-40 Additional Premises Rented to Others - Residence Premises  (START)	  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="ADDITIONAL_RESIDENCE_PREMISES">
IF(<xsl:value-of select="RESIDENCE_PREMISES"/> = 0.00) THEN
	0.00
ELSE
	IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 100000.00) THEN
		<xsl:value-of select="RESIDENCE_PREMISES"/> * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='ADD_PREMISES_RENTED_AT_PREMISES']/@LIABILITY_LIMIT_G1"/>) + (((<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='ADD_PREMISES_RENTED_AT_PREMISES']/@FOR_EACH_ADDITION"/>))DIV 1000.00) * (<xsl:value-of select ="RESIDENCE_PREMISES"/>)
	ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 300000.00) THEN
		<xsl:value-of select="RESIDENCE_PREMISES"/> *(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='ADD_PREMISES_RENTED_AT_PREMISES']/@LIABILITY_LIMIT_G2"/>) + (((<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='ADD_PREMISES_RENTED_AT_PREMISES']/@FOR_EACH_ADDITION"/>))DIV 1000.00) *  (<xsl:value-of select ="RESIDENCE_PREMISES"/>)
	ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 500000.00) THEN
		<xsl:value-of select="RESIDENCE_PREMISES"/> *(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='ADD_PREMISES_RENTED_AT_PREMISES']/@LIABILITY_LIMIT_G3"/>) + (((<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='ADD_PREMISES_RENTED_AT_PREMISES']/@FOR_EACH_ADDITION"/>))DIV 1000.00)  * (<xsl:value-of select ="RESIDENCE_PREMISES"/>)
	ELSE
		0.00
</xsl:template>

<!-- ============================================================================================ -->
<!--	Template for HO-40 Additional Premises Rented to Others - Residence Premises  (END)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--	Template for HO-70 Additional Premises Rented - Other Location - 1 Family (1)(START)	  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="ADDITIONAL_OTHER_LOCATION_PREMISES_FAMILY1">
IF(<xsl:value-of select="OTHER_LOC_1FAMILY"/> = 0.00) THEN
	0.00
ELSE
	IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 100000.00) THEN
		<xsl:value-of select="OTHER_LOC_1FAMILY"/> * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='ADD_PREMISES_RENTED_OUTSIDE_PREMISES1']/@LIABILITY_LIMIT_G1"/>) + (((<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='ADD_PREMISES_RENTED_OUTSIDE_PREMISES1']/@FOR_EACH_ADDITION"/>))DIV 1000.00) * (<xsl:value-of select="OTHER_LOC_1FAMILY"/>)
	ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 300000.00) THEN
		<xsl:value-of select="OTHER_LOC_1FAMILY"/> *(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='ADD_PREMISES_RENTED_OUTSIDE_PREMISES1']/@LIABILITY_LIMIT_G2"/>) + (((<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='ADD_PREMISES_RENTED_OUTSIDE_PREMISES1']/@FOR_EACH_ADDITION"/>))DIV 1000.00) * (<xsl:value-of select="OTHER_LOC_1FAMILY"/>)
	ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 500000.00) THEN
		<xsl:value-of select="OTHER_LOC_1FAMILY"/> *(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='ADD_PREMISES_RENTED_OUTSIDE_PREMISES1']/@LIABILITY_LIMIT_G3"/>) + (((<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='ADD_PREMISES_RENTED_OUTSIDE_PREMISES1']/@FOR_EACH_ADDITION"/>))DIV 1000.00) * (<xsl:value-of select="OTHER_LOC_1FAMILY"/>)
	ELSE
		0
</xsl:template>
<!-- ============================================================================================ -->
<!--	Template for HO-70 Additional Premises Rented - Other Location - 1 Family (1)(END	)	  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--	Template for HO-70 Additional Premises Rented - Other Location - 1 Family (2)(START)	  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="ADDITIONAL_OTHER_LOCATION_PREMISES_FAMILY2">
IF(<xsl:value-of select="OTHER_LOC_2FAMILY"/> = 0.00) THEN
	0.00
ELSE
	IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 100000.00) THEN
		<xsl:value-of select="OTHER_LOC_2FAMILY"/> *(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='ADD_PREMISES_RENTED_OUTSIDE_PREMISES2']/@LIABILITY_LIMIT_G1"/>) + (((<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='ADD_PREMISES_RENTED_OUTSIDE_PREMISES2']/@FOR_EACH_ADDITION"/>))DIV 1000.00) * (<xsl:value-of select="OTHER_LOC_2FAMILY"/>)
	ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 300000.00) THEN
		<xsl:value-of select="OTHER_LOC_2FAMILY"/> *(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='ADD_PREMISES_RENTED_OUTSIDE_PREMISES2']/@LIABILITY_LIMIT_G2"/>) + (((<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='ADD_PREMISES_RENTED_OUTSIDE_PREMISES2']/@FOR_EACH_ADDITION"/>))DIV 1000.00) * (<xsl:value-of select="OTHER_LOC_2FAMILY"/>)  
	ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 500000.00) THEN
		<xsl:value-of select="OTHER_LOC_2FAMILY"/> *(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='ADD_PREMISES_RENTED_OUTSIDE_PREMISES2']/@LIABILITY_LIMIT_G3"/>) + (((<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='ADD_PREMISES_RENTED_OUTSIDE_PREMISES2']/@FOR_EACH_ADDITION"/>))DIV 1000.00) * (<xsl:value-of select="OTHER_LOC_2FAMILY"/>)  
	ELSE
		0.00
</xsl:template>
<!-- ============================================================================================ -->
<!--	Template for HO-70 Additional Premises Rented - Other Location - 1 Family (3)(END)        -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--	Template for HO-42 Incidental Office Private School or Studio On Premises (START)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="HO_42_ON_PREMISES">
	IF(<xsl:value-of select ="ONPREMISES_HO42 = 'Y'"/>) THEN
		IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 100000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='INCIDENTAL_OFCE_ON_PREMISES']/@LIABILITY_LIMIT_G1"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='INCIDENTAL_OFCE_ON_PREMISES']/@FOR_EACH_ADDITION"/>)DIV 1000.00
		ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 300000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='INCIDENTAL_OFCE_ON_PREMISES']/@LIABILITY_LIMIT_G2"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='INCIDENTAL_OFCE_ON_PREMISES']/@FOR_EACH_ADDITION"/>)DIV 1000.00  
		ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 500000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='INCIDENTAL_OFCE_ON_PREMISES']/@LIABILITY_LIMIT_G3"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='INCIDENTAL_OFCE_ON_PREMISES']/@FOR_EACH_ADDITION"/>)DIV 1000.00  
		ELSE
			0
	ELSE
		0
</xsl:template>
<!-- ============================================================================================ -->
<!--	Template for HO-42 Incidental Office Private School or Studio On Premises (END)			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--	Template for HO-42 Other Structure with incidental business coverage readded (START)	  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="HO42_BUSINESS_OTHER_STRUCTURE_WITH_INCIDENTAL">
	<xsl:choose>
		<xsl:when test="LOCATED_OTH_STRUCTURE = 'Y'">
			((<xsl:value-of select="HO48INCLUDE"/>)DIV 1000) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_WITH_INCIDENTAL_HO42']/ATTRIBUTES/@COST"/>)
		</xsl:when>
		<xsl:otherwise>
		0.00
		</xsl:otherwise>
	</xsl:choose>
	
</xsl:template>
<!-- ============================================================================================ -->
<!--	Template for HO-42 HO-42 Other Structure with incidental business coverage readded (END)  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--	Template for HO-42 Incidental Office Private School or Studio Instruction Only (START)	  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="HO_42_INSTRUCTION_ONLY">
	IF(<xsl:value-of select ="INSTRUCTIONONLY_HO42 = 'Y'"/>) THEN
		IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 100000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='INCIDENTAL_OFCE_ON_INSTRUCTN']/@LIABILITY_LIMIT_G1"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='INCIDENTAL_OFCE_ON_INSTRUCTN']/@FOR_EACH_ADDITION"/>)DIV 1000.00
		ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 300000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='INCIDENTAL_OFCE_ON_INSTRUCTN']/@LIABILITY_LIMIT_G2"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='INCIDENTAL_OFCE_ON_INSTRUCTN']/@FOR_EACH_ADDITION"/>)DIV 1000.00  
		ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 500000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='INCIDENTAL_OFCE_ON_INSTRUCTN']/@LIABILITY_LIMIT_G3"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='INCIDENTAL_OFCE_ON_PREMISES']/@FOR_EACH_ADDITION"/>)DIV 1000.00  
		ELSE
			0
	ELSE
			0
</xsl:template>
<!-- ============================================================================================ -->
<!--	Template for HO-42 Incidental Office Private School or Studio Instruction Only (END)	  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--	Template for HO-43 Incidental Office Private School or Studio Off Premises  (START)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="HO_43_OFF_PREMISES">
	IF(<xsl:value-of select ="OFF_PREMISES_HO43 = 'Y'"/>) THEN
		IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 100000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='OFF_PERSUITS']/@LIABILITY_LIMIT_G1"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='OFF_PERSUITS']/@FOR_EACH_ADDITION"/>)DIV 1000.00
		ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 300000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='OFF_PERSUITS']/@LIABILITY_LIMIT_G2"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='OFF_PERSUITS']/@FOR_EACH_ADDITION"/>)DIV 1000.00  
		ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 500000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='OFF_PERSUITS']/@LIABILITY_LIMIT_G3"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='OFF_PERSUITS']/@FOR_EACH_ADDITION"/>)DIV 1000.00  
		ELSE
			0
	ELSE
			0
</xsl:template>
<!-- ============================================================================================ -->
<!--	Template for HO-43 Incidental Office Private School or Studio Off Premises  (END)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--			Template for HO-71 Business Pursuits Class A - (1 persons)  (START)				  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="HO_71_CLASSA">
	IF(<xsl:value-of select ="CLERICAL_OFFICE_HO71 = 'Y'"/>) THEN
		IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 100000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='CLASSA']/@LIABILITY_LIMIT_G1"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='CLASSA']/@FOR_EACH_ADDITION"/>)DIV 1000.00
		ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 300000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='CLASSA']/@LIABILITY_LIMIT_G2"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='CLASSA']/@FOR_EACH_ADDITION"/>)DIV 1000.00  
		ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 500000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='CLASSA']/@LIABILITY_LIMIT_G3"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='CLASSA']/@FOR_EACH_ADDITION"/>)DIV 1000.00  
		ELSE
			0
	ELSE
			0
</xsl:template>
<!-- ============================================================================================ -->
<!--			Template for HO-71 Business Pursuits Class A - (1 persons)  (END)				  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--			Template for  HO-71 Business Pursuits -    Class B - (1 persons)  (START)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="HO_71_CLASSB">
	IF(<xsl:value-of select ="SALESMEN_INC_INSTALLATION = 'Y'"/>) THEN
		IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 100000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='CLASSB']/@LIABILITY_LIMIT_G1"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='CLASSB']/@FOR_EACH_ADDITION"/>)DIV 1000.00
		ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 300000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='CLASSB']/@LIABILITY_LIMIT_G2"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='CLASSB']/@FOR_EACH_ADDITION"/>)DIV 1000.00  
		ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 500000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='CLASSB']/@LIABILITY_LIMIT_G3"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='CLASSB']/@FOR_EACH_ADDITION"/>)DIV 1000.00  
		ELSE
			0
	ELSE
		0
</xsl:template>
<!-- ============================================================================================ -->
<!--			Template for  HO-71 Business Pursuits -    Class B - (1 persons)  (END)			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--			Template for  HO-71 Business Pursuits -    Class C - (1 persons) (START)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="HO_71_CLASSC">
	IF(<xsl:value-of select ="TEACHER_ATHELETIC = 'Y'"/>) THEN
		IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 100000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='CLASSC']/@LIABILITY_LIMIT_G1"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='CLASSC']/@FOR_EACH_ADDITION"/>)DIV 1000.00
		ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 300000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='CLASSC']/@LIABILITY_LIMIT_G2"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='CLASSC']/@FOR_EACH_ADDITION"/>)DIV 1000.00  
		ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 500000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='CLASSC']/@LIABILITY_LIMIT_G3"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='CLASSC']/@FOR_EACH_ADDITION"/>)DIV 1000.00  
		ELSE
			0
	ELSE
			0
</xsl:template>
<!-- ============================================================================================ -->
<!--			Template for  HO-71 Business Pursuits -    Class C - (1 persons) (END)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->



<!-- ============================================================================================ -->
<!--			Template for  HO-71 Business Pursuits -    Class D - (1 persons) (START)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="HO_71_CLASSD">
	IF(<xsl:value-of select ="TEACHER_NOC = 'Y'"/>) THEN
		IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 100000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='CLASSD']/@LIABILITY_LIMIT_G1"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='CLASSD']/@FOR_EACH_ADDITION"/>)DIV 1000.00
		ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 300000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='CLASSD']/@LIABILITY_LIMIT_G2"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='CLASSD']/@FOR_EACH_ADDITION"/>)DIV 1000.00  
		ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 500000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='CLASSD']/@LIABILITY_LIMIT_G3"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='CLASSD']/@FOR_EACH_ADDITION"/>)DIV 1000.00  
		ELSE
			0
	ELSE
			0
</xsl:template>
<!-- ============================================================================================ -->
<!--			Template for  HO-71 Business Pursuits -    Class D - (1 persons) (END)			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--	Template for  HO-72 Farm Liability - Incidental Farming Residence Premises (START)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="HO_72_INCIDENTAL_FARMING">
	IF(<xsl:value-of select ="INCIDENTAL_FARMING_HO72 = 'Y'"/>) THEN
		IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 100000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='INCIDENTAL_FARMING_RESIDENCE']/@LIABILITY_LIMIT_G1"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='INCIDENTAL_FARMING_RESIDENCE']/@FOR_EACH_ADDITION"/>)DIV 1000.00
		ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 300000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='INCIDENTAL_FARMING_RESIDENCE']/@LIABILITY_LIMIT_G2"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='INCIDENTAL_FARMING_RESIDENCE']/@FOR_EACH_ADDITION"/>)DIV 1000.00  
		ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 500000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='INCIDENTAL_FARMING_RESIDENCE']/@LIABILITY_LIMIT_G3"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='INCIDENTAL_FARMING_RESIDENCE']/@FOR_EACH_ADDITION"/>)DIV 1000.00  
		ELSE
			0
	ELSE
			0
</xsl:template>
<!-- ============================================================================================ -->
<!--	Template for  HO-72 Farm Liability - Incidental Farming Residence Premises (END)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--	Template for  HO-73 Farm Liability - Owned By Insured Rented to Others (2) (START)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="HO_73_OTH_LOC_OPR_OTHERS">
	<xsl:variable name ="PHO73" select ="OTH_LOC_OPR_OTHERS_HO73"/>
	IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 100000.00) THEN
		((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='RENTED_TO_OTHERS']/@LIABILITY_LIMIT_G1"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='RENTED_TO_OTHERS']/@FOR_EACH_ADDITION"/>)DIV 1000.00)* <xsl:value-of select ="$PHO73"/>
	ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 300000.00) THEN
		((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='RENTED_TO_OTHERS']/@LIABILITY_LIMIT_G2"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='RENTED_TO_OTHERS']/@FOR_EACH_ADDITION"/>)DIV 1000.00  )* <xsl:value-of select ="$PHO73"/>
	ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 500000.00) THEN
		((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='RENTED_TO_OTHERS']/@LIABILITY_LIMIT_G3"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='RENTED_TO_OTHERS']/@FOR_EACH_ADDITION"/>)DIV 1000.00  )* <xsl:value-of select ="$PHO73"/>
	ELSE
		0
</xsl:template>
<!-- ============================================================================================ -->
<!--	Template for  HO-73 Farm Liability - Owned By Insured Rented to Others (2) (END)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--	Template for  HO-73 Farm Liability - Owned/Operated By Insured/Insured Employee (START)	  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="HO_73_OTH_LOC_OPR_EMPL">
	<xsl:variable name ="PHO73" select ="OTH_LOC_OPR_EMPL_HO73"/>
	IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 100000.00) THEN
		((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='OPERATED_BY_INSURED']/@LIABILITY_LIMIT_G1"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='OPERATED_BY_INSURED']/@FOR_EACH_ADDITION"/>)DIV 1000.00)* <xsl:value-of select ="$PHO73"/>
	ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 300000.00) THEN
		((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='OPERATED_BY_INSURED']/@LIABILITY_LIMIT_G2"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='OPERATED_BY_INSURED']/@FOR_EACH_ADDITION"/>)DIV 1000.00 )* <xsl:value-of select ="$PHO73"/> 
	ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 500000.00) THEN
		((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='OPERATED_BY_INSURED']/@LIABILITY_LIMIT_G3"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='OPERATED_BY_INSURED']/@FOR_EACH_ADDITION"/>)DIV 1000.00  )* <xsl:value-of select ="$PHO73"/>
	ELSE
		0
</xsl:template>
<!-- ============================================================================================ -->
<!--	Template for  HO-73 Farm Liability - Owned/Operated By Insured/Insured Employee (END)	  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--						Template for  HO-82 Personal Injury (START)							  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="HO_82_PERSONAL_INJURY">
	IF('<xsl:value-of select="PIP_HO82"/>' = 'Y') THEN
		IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 100000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='PERSONAL_INJURY_COVERAGE']/@LIABILITY_LIMIT_G1"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='PERSONAL_INJURY_COVERAGE']/@FOR_EACH_ADDITION"/>)DIV 1000.00
		ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 300000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='PERSONAL_INJURY_COVERAGE']/@LIABILITY_LIMIT_G2"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='PERSONAL_INJURY_COVERAGE']/@FOR_EACH_ADDITION"/>)DIV 1000.00  
		ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 500000.00) THEN
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='PERSONAL_INJURY_COVERAGE']/@LIABILITY_LIMIT_G3"/>) + (<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='PERSONAL_INJURY_COVERAGE']/@FOR_EACH_ADDITION"/>)DIV 1000.00  
		ELSE
			0
	ELSE
			0
</xsl:template>
<!-- ============================================================================================ -->
<!--						Template for  HO-82 Personal Injury (START)							  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--						Template INLAND MARINE Bicycles (HO-900) (START)					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="HO_900_BICYCLES_PREMIUM">
	IF(<xsl:value-of select="SCH_BICYCLE_DED"/> = 0) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID ='CYC']/@DED_GROUP1"/>)* (<xsl:value-of select="SCH_BICYCLE_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_BICYCLE_DED"/> &gt;= 100) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID ='CYC']/@DED_GROUP2"/>)* (<xsl:value-of select="SCH_BICYCLE_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_BICYCLE_DED"/> &gt;= 250) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID ='CYC']/@DED_GROUP3"/>)* (<xsl:value-of select="SCH_BICYCLE_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_BICYCLE_DED"/> &gt;= 500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID ='CYC']/@DED_GROUP4"/>)* (<xsl:value-of select="SCH_BICYCLE_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_BICYCLE_DED"/> &gt;= 750) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID ='CYC']/@DED_GROUP5"/>)* (<xsl:value-of select="SCH_BICYCLE_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_BICYCLE_DED"/> &gt;= 1000) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID ='CYC']/@DED_GROUP6"/>)* (<xsl:value-of select="SCH_BICYCLE_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_BICYCLE_DED"/> &gt;= 2500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID ='CYC']/@DED_GROUP7"/>)* (<xsl:value-of select="SCH_BICYCLE_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE
		0
</xsl:template>
<!-- ============================================================================================ -->
<!--						Template INLAND MARINE Bicycles (HO-900) (END	)					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--						Template INLAND MARINE Cameras Non-Professional  (START)			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="CAMERAS_PREMIUM">
	IF(<xsl:value-of select="SCH_CAMERA_DED"/> = 0) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID ='CAM']/@DED_GROUP1"/>)* (<xsl:value-of select="SCH_CAMERA_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_CAMERA_DED"/> &gt;= 100) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID ='CAM']/@DED_GROUP2"/>)* (<xsl:value-of select="SCH_CAMERA_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_CAMERA_DED"/> &gt;= 250) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID ='CAM']/@DED_GROUP3"/>)* (<xsl:value-of select="SCH_CAMERA_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_CAMERA_DED"/> &gt;= 500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID ='CAM']/@DED_GROUP4"/>)* (<xsl:value-of select="SCH_CAMERA_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_CAMERA_DED"/> &gt;= 750) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID ='CAM']/@DED_GROUP5"/>)* (<xsl:value-of select="SCH_CAMERA_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_CAMERA_DED"/> &gt;= 1000) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID ='CAM']/@DED_GROUP6"/>)* (<xsl:value-of select="SCH_CAMERA_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_CAMERA_DED"/> &gt;= 2500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID ='CAM']/@DED_GROUP7"/>)* (<xsl:value-of select="SCH_CAMERA_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE
		0
</xsl:template>
<!-- ============================================================================================ -->
<!--						Template INLAND MARINE Cameras Non-Professional  (END)				  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--						Template INLAND MARINE Cellular Phones (HO-900)  (START)			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="HO_900_CELLULAR_PHONES_PREMIUM">
	IF(<xsl:value-of select="SCH_CELL_DED"/> = 0) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID ='CALLULAR']/@DED_GROUP1"/>)* (<xsl:value-of select="SCH_CELL_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_CELL_DED"/> &gt;= 100) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID ='CALLULAR']/@DED_GROUP2"/>)* (<xsl:value-of select="SCH_CELL_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_CELL_DED"/> &gt;= 250) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID ='CALLULAR']/@DED_GROUP3"/>)* (<xsl:value-of select="SCH_CELL_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_CELL_DED"/> &gt;= 500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID ='CALLULAR']/@DED_GROUP4"/>)* (<xsl:value-of select="SCH_CELL_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_CELL_DED"/> &gt;= 750) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID ='CALLULAR']/@DED_GROUP5"/>)* (<xsl:value-of select="SCH_CELL_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_CELL_DED"/> &gt;= 1000) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID ='CALLULAR']/@DED_GROUP6"/>)* (<xsl:value-of select="SCH_CELL_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_CELL_DED"/> &gt;= 2500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID ='CALLULAR']/@DED_GROUP7"/>)* (<xsl:value-of select="SCH_CELL_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE
		0
</xsl:template>
<!-- ============================================================================================ -->
<!--						Template INLAND MARINE Cellular Phones (HO-900)  (END	)			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--							Template INLAND MARINE  Furs  (START)							  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="FURS_PREMIUM">
	IF(<xsl:value-of select="SCH_FURS_DED"/> = 0) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='FURS']/@DED_GROUP1"/>)* (<xsl:value-of select="SCH_FURS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_FURS_DED"/> &gt;= 100) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='FURS']/@DED_GROUP2"/>)* (<xsl:value-of select="SCH_FURS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_FURS_DED"/> &gt;= 250) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='FURS']/@DED_GROUP3"/>)* (<xsl:value-of select="SCH_FURS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_FURS_DED"/> &gt;= 500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='FURS']/@DED_GROUP4"/>)* (<xsl:value-of select="SCH_FURS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_FURS_DED"/> &gt;= 750) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='FURS']/@DED_GROUP5"/>)* (<xsl:value-of select="SCH_FURS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_FURS_DED"/> &gt;= 1000) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='FURS']/@DED_GROUP6"/>)* (<xsl:value-of select="SCH_FURS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_FURS_DED"/> &gt;= 2500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='FURS']/@DED_GROUP7"/>)* (<xsl:value-of select="SCH_FURS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE
		0
</xsl:template>
<!-- ============================================================================================ -->
<!--							Template INLAND MARINE  Furs  (END)								  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--							Template INLAND MARINE  Golf Equipment  (START)					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="GOLF_EQUIPMENT_PREMIUM">
	IF(<xsl:value-of select="SCH_GOLF_DED"/> = 0) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='GULFEQUIP']/@DED_GROUP1"/>)* (<xsl:value-of select="SCH_GOLF_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_GOLF_DED"/> &gt;= 100) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='GULFEQUIP']/@DED_GROUP2"/>)* (<xsl:value-of select="SCH_GOLF_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_GOLF_DED"/> &gt;= 250) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='GULFEQUIP']/@DED_GROUP3"/>)* (<xsl:value-of select="SCH_GOLF_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_GOLF_DED"/> &gt;= 500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='GULFEQUIP']/@DED_GROUP4"/>)* (<xsl:value-of select="SCH_GOLF_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_GOLF_DED"/> &gt;= 750) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='GULFEQUIP']/@DED_GROUP5"/>)* (<xsl:value-of select="SCH_GOLF_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_GOLF_DED"/> &gt;= 1000) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='GULFEQUIP']/@DED_GROUP6"/>)* (<xsl:value-of select="SCH_GOLF_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_GOLF_DED"/> &gt;= 2500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='GULFEQUIP']/@DED_GROUP7"/>)* (<xsl:value-of select="SCH_GOLF_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE
		0
</xsl:template>
<!-- ============================================================================================ -->
<!--							Template INLAND MARINE  Golf Equipment  (END)					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--							Template INLAND MARINE  Guns (START)							  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="GUNS_PREMIUM">
	IF(<xsl:value-of select="SCH_GUNS_DED"/> = 0) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='GUNS']/@DED_GROUP1"/>)* (<xsl:value-of select="SCH_GUNS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_GUNS_DED"/> &gt;= 100) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='GUNS']/@DED_GROUP2"/>)* (<xsl:value-of select="SCH_GUNS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_GUNS_DED"/> &gt;= 250) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='GUNS']/@DED_GROUP3"/>)* (<xsl:value-of select="SCH_GUNS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_GUNS_DED"/> &gt;= 500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='GUNS']/@DED_GROUP4"/>)* (<xsl:value-of select="SCH_GUNS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_GUNS_DED"/> &gt;= 750) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='GUNS']/@DED_GROUP5"/>)* (<xsl:value-of select="SCH_GUNS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_GUNS_DED"/> &gt;= 1000) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='GUNS']/@DED_GROUP6"/>)* (<xsl:value-of select="SCH_GUNS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_GUNS_DED"/> &gt;= 2500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='GUNS']/@DED_GROUP7"/>)* (<xsl:value-of select="SCH_GUNS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE
		0
</xsl:template>
<!-- ============================================================================================ -->
<!--							Template INLAND MARINE  Guns (END)								  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--							Template INLAND MARINE  Jewelry (START)							  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="JEWELRY_PREMIUM">
	IF(<xsl:value-of select="SCH_JWELERY_DED"/> = 0) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='JEWELERY']/@DED_GROUP1"/>)* (<xsl:value-of select="SCH_JWELERY_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_JWELERY_DED"/> &gt;= 100) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='JEWELERY']/@DED_GROUP2"/>)* (<xsl:value-of select="SCH_JWELERY_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_JWELERY_DED"/> &gt;= 250) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='JEWELERY']/@DED_GROUP3"/>)* (<xsl:value-of select="SCH_JWELERY_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_JWELERY_DED"/> &gt;= 500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='JEWELERY']/@DED_GROUP4"/>)* (<xsl:value-of select="SCH_JWELERY_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_JWELERY_DED"/> &gt;= 750) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='JEWELERY']/@DED_GROUP5"/>)* (<xsl:value-of select="SCH_JWELERY_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_JWELERY_DED"/> &gt;= 1000) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='JEWELERY']/@DED_GROUP6"/>)* (<xsl:value-of select="SCH_JWELERY_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_JWELERY_DED"/> &gt;= 2500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='JEWELERY']/@DED_GROUP7"/>)* (<xsl:value-of select="SCH_JWELERY_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE
		0
</xsl:template>
<!-- ============================================================================================ -->
<!--							Template INLAND MARINE  Jewelry (END)							  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--			Template INLAND MARINE  Musical (Non-Professional) (START)						  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="MUSICAL_NON_PROFESSIONAL_PREMIUM">
	IF(<xsl:value-of select="SCH_MUSICAL_DED"/> = 0) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='MUSICAL']/@DED_GROUP1"/>)* (<xsl:value-of select="SCH_MUSICAL_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_MUSICAL_DED"/> &gt;= 100) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='MUSICAL']/@DED_GROUP2"/>)* (<xsl:value-of select="SCH_MUSICAL_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_MUSICAL_DED"/> &gt;= 250) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='MUSICAL']/@DED_GROUP3"/>)* (<xsl:value-of select="SCH_MUSICAL_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_MUSICAL_DED"/> &gt;= 500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='MUSICAL']/@DED_GROUP4"/>)* (<xsl:value-of select="SCH_MUSICAL_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_MUSICAL_DED"/> &gt;= 750) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='MUSICAL']/@DED_GROUP5"/>)* (<xsl:value-of select="SCH_MUSICAL_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_MUSICAL_DED"/> &gt;= 1000) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='MUSICAL']/@DED_GROUP6"/>)* (<xsl:value-of select="SCH_MUSICAL_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_MUSICAL_DED"/> &gt;= 2500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='MUSICAL']/@DED_GROUP7"/>)* (<xsl:value-of select="SCH_MUSICAL_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE
		0
</xsl:template>
<!-- ============================================================================================ -->
<!--			Template INLAND MARINE  Musical (Non-Professional) (END)						  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--			Template INLAND MARINE  Personal Computers (HO-214) (START)						  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="PERSONAL_COMPUTERS_PREMIUM">
	IF(<xsl:value-of select="SCH_PERSCOMP_DED"/> = 100) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='PC']/@DED_GROUP2"/>)* (<xsl:value-of select="SCH_PERSCOMP_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE
		0
</xsl:template>
<!-- ============================================================================================ -->
<!--			Template INLAND MARINE  Personal Computers (HO-214) (END)						  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--							Template INLAND MARINE  Silver (START)							  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="SILVER_PREMIUM">
	IF(<xsl:value-of select="SCH_SILVER_DED"/> = 0) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='SILVER']/@DED_GROUP1"/>)* (<xsl:value-of select="SCH_SILVER_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_SILVER_DED"/> &gt;= 100) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='SILVER']/@DED_GROUP2"/>)* (<xsl:value-of select="SCH_SILVER_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_SILVER_DED"/> &gt;= 250) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='SILVER']/@DED_GROUP3"/>)* (<xsl:value-of select="SCH_SILVER_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_SILVER_DED"/> &gt;= 500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='SILVER']/@DED_GROUP4"/>)* (<xsl:value-of select="SCH_SILVER_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_SILVER_DED"/> &gt;= 750) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='SILVER']/@DED_GROUP5"/>)* (<xsl:value-of select="SCH_SILVER_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_SILVER_DED"/> &gt;= 1000) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='SILVER']/@DED_GROUP6"/>)* (<xsl:value-of select="SCH_SILVER_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_SILVER_DED"/> &gt;= 2500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='SILVER']/@DED_GROUP7"/>)* (<xsl:value-of select="SCH_SILVER_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE
		0
</xsl:template>
<!-- ============================================================================================ -->
<!--							Template INLAND MARINE  Silver (END)							  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--							Template INLAND MARINE  Stamps (START)							  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="STAMPS_PREMIUM">
	IF(<xsl:value-of select="SCH_STAMPS_DED"/> = 0) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='STAMPS']/@DED_GROUP1"/>)* (<xsl:value-of select="SCH_STAMPS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_STAMPS_DED"/> &gt;= 100) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='STAMPS']/@DED_GROUP2"/>)* (<xsl:value-of select="SCH_STAMPS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_STAMPS_DED"/> &gt;= 250) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='STAMPS']/@DED_GROUP3"/>)* (<xsl:value-of select="SCH_STAMPS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_STAMPS_DED"/> &gt;= 500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='STAMPS']/@DED_GROUP4"/>)* (<xsl:value-of select="SCH_STAMPS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_STAMPS_DED"/> &gt;= 750) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='STAMPS']/@DED_GROUP5"/>)* (<xsl:value-of select="SCH_STAMPS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_STAMPS_DED"/> &gt;= 1000) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='STAMPS']/@DED_GROUP6"/>)* (<xsl:value-of select="SCH_STAMPS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_STAMPS_DED"/> &gt;= 2500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='STAMPS']/@DED_GROUP7"/>)* (<xsl:value-of select="SCH_STAMPS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE
		0
</xsl:template>
<!-- ============================================================================================ -->
<!--							Template INLAND MARINE  Stamps (END)							  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--							Template INLAND MARINE  Rare Coins  (START)						  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="RARE_COINS_PREMIUM">
	IF(<xsl:value-of select="SCH_RARECOINS_DED"/> = 0) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='COINS']/@DED_GROUP1"/>)* (<xsl:value-of select="SCH_RARECOINS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_RARECOINS_DED"/> &gt;= 100) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='COINS']/@DED_GROUP2"/>)* (<xsl:value-of select="SCH_RARECOINS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_RARECOINS_DED"/> &gt;= 250) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='COINS']/@DED_GROUP3"/>)* (<xsl:value-of select="SCH_RARECOINS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_RARECOINS_DED"/> &gt;= 500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='COINS']/@DED_GROUP4"/>)* (<xsl:value-of select="SCH_RARECOINS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_RARECOINS_DED"/> &gt;= 750) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='COINS']/@DED_GROUP5"/>)* (<xsl:value-of select="SCH_RARECOINS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_RARECOINS_DED"/> &gt;= 1000) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='COINS']/@DED_GROUP6"/>)* (<xsl:value-of select="SCH_RARECOINS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_RARECOINS_DED"/> &gt;= 2500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='COINS']/@DED_GROUP7"/>)* (<xsl:value-of select="SCH_RARECOINS_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE
		0
</xsl:template>
<!-- ============================================================================================ -->
<!--							Template INLAND MARINE  Rare Coins  (END)						  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--					Template INLAND MARINE  Fine Arts without Breakage (START)				  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="FINE_ARTS_WITHOUT_BREAKAGE_PREMIUM">
	IF(<xsl:value-of select="SCH_FINEARTS_WO_BREAK_DED"/> = 0) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='FINEARTSWITHOUTBREAKAGE']/@DED_GROUP1"/>)* (<xsl:value-of select="SCH_FINEARTS_WO_BREAK_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_FINEARTS_WO_BREAK_DED"/> &gt;= 100) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='FINEARTSWITHOUTBREAKAGE']/@DED_GROUP2"/>)* (<xsl:value-of select="SCH_FINEARTS_WO_BREAK_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_FINEARTS_WO_BREAK_DED"/> &gt;= 250) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='FINEARTSWITHOUTBREAKAGE']/@DED_GROUP3"/>)* (<xsl:value-of select="SCH_FINEARTS_WO_BREAK_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_FINEARTS_WO_BREAK_DED"/> &gt;= 500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='FINEARTSWITHOUTBREAKAGE']/@DED_GROUP4"/>)* (<xsl:value-of select="SCH_FINEARTS_WO_BREAK_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_FINEARTS_WO_BREAK_DED"/> &gt;= 750) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='FINEARTSWITHOUTBREAKAGE']/@DED_GROUP5"/>)* (<xsl:value-of select="SCH_FINEARTS_WO_BREAK_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_FINEARTS_WO_BREAK_DED"/> &gt;= 1000) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='FINEARTSWITHOUTBREAKAGE']/@DED_GROUP6"/>)* (<xsl:value-of select="SCH_FINEARTS_WO_BREAK_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_FINEARTS_WO_BREAK_DED"/> &gt;= 2500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='FINEARTSWITHOUTBREAKAGE']/@DED_GROUP7"/>)* (<xsl:value-of select="SCH_FINEARTS_WO_BREAK_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE
		0
</xsl:template>
<!-- ============================================================================================ -->
<!--					Template INLAND MARINE  Fine Arts without Breakage (END)				  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--					Template INLAND MARINE  Fine Arts without Breakage  (START)				  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="FINE_ARTS_BREAKAGE_PREMIUM">

	IF(<xsl:value-of select="SCH_FINEARTS_BREAK_DED"/> = 0) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='FINEARTSWITHBREAKAGE']/@DED_GROUP1"/>)* (<xsl:value-of select="SCH_FINEARTS_BREAK_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_FINEARTS_BREAK_DED"/> &gt;= 100) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='FINEARTSWITHBREAKAGE']/@DED_GROUP2"/>)* (<xsl:value-of select="SCH_FINEARTS_BREAK_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_FINEARTS_BREAK_DED"/> &gt;= 250) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='FINEARTSWITHBREAKAGE']/@DED_GROUP3"/>)* (<xsl:value-of select="SCH_FINEARTS_BREAK_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_FINEARTS_BREAK_DED"/> &gt;= 500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='FINEARTSWITHBREAKAGE']/@DED_GROUP4"/>)* (<xsl:value-of select="SCH_FINEARTS_BREAK_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_FINEARTS_BREAK_DED"/> &gt;= 750) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='FINEARTSWITHBREAKAGE']/@DED_GROUP5"/>)* (<xsl:value-of select="SCH_FINEARTS_BREAK_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_FINEARTS_BREAK_DED"/> &gt;= 1000) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='FINEARTSWITHBREAKAGE']/@DED_GROUP6"/>)* (<xsl:value-of select="SCH_FINEARTS_BREAK_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE IF(<xsl:value-of select="SCH_FINEARTS_BREAK_DED"/> &gt;= 2500) THEN
		(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='ENDORSEMENT_OPTIONS']/ATTRIBUTES[@ID='FINEARTSWITHBREAKAGE']/@DED_GROUP7"/>)* (<xsl:value-of select="SCH_FINEARTS_BREAK_AMOUNT"/> DIV 100)* <xsl:call-template name ="TERM_FACTOR"/>
	ELSE
		0

</xsl:template>
<!-- ============================================================================================ -->
<!--					Template INLAND MARINE  Fine Arts without Breakage  (END)				  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--							Template Property Expense Fee (START)							  -->
<!--		    							FOR MISHIGAN										  -->
<!-- ============================================================================================ -->
<xsl:template name="PROPFEE">
	<xsl:choose>
		<xsl:when test ="TERMFACTOR = 12">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PROPRTYFEE']/NODE[@ID='PFEE']/ATTRIBUTES/@FEES"/>
		</xsl:when>
		<xsl:when test ="TERMFACTOR = 6">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PROPRTYFEE']/NODE[@ID='PFEE']/ATTRIBUTES/@FEES"/>
		</xsl:when>
		<xsl:otherwise>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--							Template Property Expense Fee (START)							  -->
<!--		    							FOR MISHIGAN										  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--							Template SEASONAL SECONDARY CREDIT  (START)						  -->
<!--		    							FOR MISHIGAN and INDIANA							  -->
<!-- ============================================================================================ -->
<xsl:template name="SEASONALSECONDARYCREDIT">
	IF('<xsl:value-of select="SEASONALSECONDARY"/>' = 'Y' AND '<xsl:value-of select="WOLVERINEINSURESPRIMARY"/>' = 'Y') THEN
		IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 100000.00) THEN
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='SEASONAL_CREDIT']/ATTRIBUTES[@ID='INSURE_PRIMARY']/@GROUP1"/> 
		ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 300000.00) THEN
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='SEASONAL_CREDIT']/ATTRIBUTES[@ID='INSURE_PRIMARY']/@GROUP2"/>
		ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 500000.00) THEN
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='SEASONAL_CREDIT']/ATTRIBUTES[@ID='INSURE_PRIMARY']/@GROUP3"/>
		ELSE
			1.00
	ELSE IF('<xsl:value-of select="SEASONALSECONDARY"/>' = 'Y' AND '<xsl:value-of select="WOLVERINEINSURESPRIMARY"/>' = 'N') THEN
		IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 100000.00) THEN
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='SEASONAL_CREDIT']/ATTRIBUTES[@ID='INSURE_NON_PRIMARY']/@GROUP1"/>
		ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 300000.00) THEN
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='SEASONAL_CREDIT']/ATTRIBUTES[@ID='INSURE_NON_PRIMARY']/@GROUP2"/>
		ELSE IF(<xsl:value-of select="PERSONALLIABILITY_LIMIT"/> = 500000.00) THEN
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='SEASONAL_CREDIT']/ATTRIBUTES[@ID='INSURE_NON_PRIMARY']/@GROUP3"/>
		ELSE
			1.00 
	ELSE
		1.00
</xsl:template>

<!-- SEASONAL SECONDARY CREDIT -->
<xsl:template name="SEASONALSECONDARY_DISPLAY">
	<xsl:choose>
		<xsl:when test ="SEASONALSECONDARY = 'Y'">
		Applied
		</xsl:when>
		<xsl:otherwise>
		0
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--							Template SEASONAL SECONDARY CREDIT  (START)						  -->
<!--		    							FOR MISHIGAN and INDIANA							  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--							Template Described Primary Residence Coverage  (START)			  -->
<!--		    							FOR MISHIGAN and INDIANA							  -->
<!-- ============================================================================================ -->

<xsl:template name ="DESCRIBED_PRIMARY_RESIDENCE">
	<xsl:variable name ="COVERAGE_E" select ="PERSONALLIABILITY_LIMIT"/>
	<xsl:choose>
		<xsl:when test ="PERSONALLIABILITY_LIMIT  = 300000.00">
			<xsl:value-of select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='DESCRIBED_PRIMARY_RESIDENCE']/@LIABILITY_LIMIT_G2"/>  +((<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='DESCRIBED_PRIMARY_RESIDENCE']/@FOR_EACH_ADDITION"/>))DIV 1000.00
		</xsl:when>
		<xsl:when test ="PERSONALLIABILITY_LIMIT  = 500000.00">
			<xsl:value-of select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='DESCRIBED_PRIMARY_RESIDENCE']/@LIABILITY_LIMIT_G3"/>  +((<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='DESCRIBED_PRIMARY_RESIDENCE']/@FOR_EACH_ADDITION"/>))DIV 1000.00
		</xsl:when>
		<xsl:when test ="PERSONALLIABILITY_LIMIT  = 100000.00">
			((<xsl:value-of select ="MEDICALPAYMENTSTOOTHERS_LIMIT"></xsl:value-of> - 1000.00) * (<xsl:value-of select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID='DESCRIBED_PRIMARY_RESIDENCE']/@FOR_EACH_ADDITION"/>))DIV 1000.00
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--							Template SEASONAL SECONDARY CREDIT  (START)						  -->
<!--		    							FOR MISHIGAN and INDIANA							  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--									Template Smoker Factor (START)							  -->
<!--		    							FOR MISHIGAN and INDIANA							  -->
<!-- ============================================================================================ -->
<xsl:template name ="SMOKER">
	<xsl:choose>
		<xsl:when test="NONSMOKER = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='NONSMOKERCREIT']/ATTRIBUTES/@FACTOR"/> 		
		</xsl:when>
		<xsl:otherwise>
		1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- For Display Only -->
<xsl:template name="SMOKER_DISPLAY">
	<xsl:choose>
		<xsl:when test = "NONSMOKER = 'Y'">
		Applied
		</xsl:when>
		<xsl:otherwise>
		0
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name ="SMOKER_CREDIT_DISPLAY">
	<xsl:choose>
		<xsl:when test="NONSMOKER = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='NONSMOKERCREIT']/ATTRIBUTES/@CREDIT"/> 		
		</xsl:when>
		<xsl:otherwise>
		1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--									Template Smoker Factor (END)							  -->
<!--		    							FOR MISHIGAN and INDIANA							  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--									50% Discount Applied (START)							  -->
<!--		    							FOR MISHIGAN										  -->
<!-- ============================================================================================ -->
<xsl:template name="MAX_DISCOUNT_APPLIED_DISPLAY">
{
	<xsl:variable name="FINALCREDIT_EXPERIENCE">
		(1.00-(<xsl:call-template name ="AGEOFHOME_FACTOR"/>))
		+	(1.00-(<xsl:call-template name ="GET_PROTECTIVE_DEVICE_DISCOUNT"/>))
		+	(1.00-(<xsl:call-template name ="GET_DUC_DISCOUNT"/>))
		+	(1.00-(<xsl:call-template name ="GET_EXPERIENCE_DISCOUNT"/>))
		+	(1.00-(<xsl:call-template name ="GET_MULTIPOLICY_DISCOUNT"/>))
	</xsl:variable>
	<xsl:choose>
		<xsl:when test ="STATENAME = 'INDIANA'">
			0.00
		</xsl:when>
		<xsl:otherwise>
			IF (<xsl:value-of select ="$FINALCREDIT_EXPERIENCE"/> &gt; 0.50) 
			THEN
				1.00
			ELSE
				0.00		
		</xsl:otherwise>
	</xsl:choose>
}
</xsl:template>
<!-- ============================================================================================ -->
<!--									50% Discount Applied (END)	    						  -->
<!--		    							FOR MISHIGAN										  -->
<!-- ============================================================================================ -->

<xsl:template name="SELECT_HO_20_PREFERRED_PLUS_OR_HO_21_VIP">
	<xsl:choose>
		<xsl:when test ="HO20 ='Y'">HO-20 Preferred Plus</xsl:when>
		<xsl:when test ="HO25 ='Y'">HO-25 Preferred Plus VIP</xsl:when>
		<xsl:when test ="HO21 ='Y'">HO-22 Preferred Plus VIP</xsl:when>
		<xsl:when test ="HO23 ='Y'">HO-24 Premier VIP</xsl:when>
		<xsl:otherwise>
		</xsl:otherwise>
	</xsl:choose> 
</xsl:template>
	

<!-- Prior Loss Label Display -->
<xsl:template name ="PRIOR_LOSS_LABEL_DISPLAY">
<xsl:choose>
		<xsl:when test ="PRIOR_LOSS_SURCHARGE = 'Y'">
		(
			((((((
					(
						(<xsl:call-template name ="DEWLLING-MAIN"></xsl:call-template>)
						*(<xsl:call-template name ="C-VALUE"></xsl:call-template>)
						*(<xsl:call-template name ="INSURANCE_SCORE_CREDIT"></xsl:call-template>)
						*(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)
					)
					+
					(
						(<xsl:call-template name ="COVERAGE_C_PREMIUM"/> )
						+ (<xsl:call-template name ="COVERAGE_D_PREMIUM"/>)
					)
				    
			)
			*
			(<xsl:call-template name ="DFACTOR"/>)
			)*
				(<xsl:call-template name ="MULTIPOLICY_FACTOR"/>)
			)*
			   
			(<xsl:call-template name ="AGEOFHOME_FACTOR"/>))*
			(<xsl:call-template name ="PROTECTIVEDEVICE"/>))*
			(<xsl:call-template name ="EXPERIENCEFACTOR"/>))*
			(<xsl:call-template name ="PRIOR_LOSS"/>)
			-
			(((((
					(
						(<xsl:call-template name ="DEWLLING-MAIN"></xsl:call-template>)
						*(<xsl:call-template name ="C-VALUE"></xsl:call-template>)
						*(<xsl:call-template name ="INSURANCE_SCORE_CREDIT"></xsl:call-template>)
						*(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)
					)
					+
					(
						(<xsl:call-template name ="COVERAGE_C_PREMIUM"/> )
						+ (<xsl:call-template name ="COVERAGE_D_PREMIUM"/>)
					)
				    
			)
			*
			(<xsl:call-template name ="DFACTOR"/>)
			)*
				(<xsl:call-template name ="MULTIPOLICY_FACTOR"/>)
			)*
			   
			(<xsl:call-template name ="AGEOFHOME_FACTOR"/>))*
			(<xsl:call-template name ="PROTECTIVEDEVICE"/>))*
			(<xsl:call-template name ="EXPERIENCEFACTOR"/>)
			)
		</xsl:when>
		<xsl:otherwise>
		0
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<!-- ============================================================================================ -->
<!--									Template for Lables  (START)							  -->
<!-- ============================================================================================ -->
<!--									   Group Details										  -->       
<xsl:template name ="PRODUCTID0"><xsl:value-of select="PRODUCTNAME" />  - <xsl:value-of select="PRODUCT_PREMIER" /></xsl:template>

<xsl:template name ="GROUPID0">
	<xsl:choose> 
		<xsl:when test="SEASONALSECONDARY ='Y'">Seasonal/Secondary Residence,</xsl:when>
		<xsl:otherwise>Primary Residence,</xsl:otherwise>
	</xsl:choose>
<xsl:value-of select="EXTERIOR_CONSTRUCTION_DESC" />(<xsl:value-of select="DOC" />)</xsl:template>

<xsl:template name ="GROUPID1">Fire Class: <xsl:value-of select="PROTECTIONCLASS" /> Hydrant: <xsl:value-of select="FEET2HYDRANT" />, Fire Department: <xsl:value-of select="DISTANCET_FIRESTATION" /> miles</xsl:template>
<xsl:template name ="GROUPID2">Premium Group: <xsl:call-template name="PREMIUM_GROUP" /> , Rated Class:  <xsl:value-of select="PROTECTIONCLASS" />-<xsl:value-of select="EXTERIOR_CONSTRUCTION_F_M" />, Family - <xsl:value-of select="NUMBEROFFAMILIES" /></xsl:template>
<xsl:template name ="GROUPID3">SECTION I - PROPERTY COVERAGES</xsl:template>
<xsl:template name ="GROUPID4">SECTION II - LIABILITY COVERAGES </xsl:template>
<xsl:template name ="GROUPID5">ADDITIONAL COVERAGES</xsl:template>
<xsl:template name ="GROUPID6">INLAND MARINE - SCHEDULED PERSONAL PROPERTY (HO-61)</xsl:template>
<!--Step Details-->
<xsl:template name = "STEPID0">		-  Coverage A - Dwelling</xsl:template>
<xsl:template name = "STEPID1">		-  Coverage B - Other Structures</xsl:template>
<xsl:template name = "STEPID2">		-  Coverage C - Personal Property</xsl:template>
<xsl:template name = "STEPID3">		-  Coverage D - Loss Of Use12</xsl:template>
<xsl:template name = "STEPID4">		-  Coverage E - Personal Liability - Each Occurrence</xsl:template>
<xsl:template name = "STEPID5">		-  Coverage F - Medical Payments to Others - Each Person</xsl:template>
<xsl:template name = "STEPID6">		-  Discount - Insurance Score Credit (Score - <xsl:call-template name ="INSURANCE_SCORE_DISPLAY"/>) - <xsl:call-template name ="INSURANCESCORE_PERCENT"/> </xsl:template>

<!--New Steps Added START-->
<xsl:template name = "STEPID7">		-  Discount - Non smoker <xsl:call-template name ="SMOKER_CREDIT_DISPLAY"/>%</xsl:template>
<xsl:template name = "STEPID8">		-  NOTE: Discounts have 50% MAXIMUM DISCOUNT rule applied</xsl:template>
<!--New Steps Added END -->
<xsl:template name = "STEPID9">		-  Discount - Under Construction <xsl:call-template name="DWELLING_UNDER_CONSTRUCTION_CREDIT"></xsl:call-template>%</xsl:template>
<xsl:template name = "STEPID10">	-  Discount - Experience <xsl:call-template name="EXPERIENCECREDIT"></xsl:call-template>%</xsl:template>
<xsl:template name = "STEPID11">	-  Discount - Age of Home <xsl:call-template name ="AGEOFHOME_CREDIT"/></xsl:template>
<xsl:template name = "STEPID12"><xsl:call-template name ="PROTECTIVEDEVICE_CREDIT"/></xsl:template>
<xsl:template name = "STEPID13">	-  Discount - Multi Policy <xsl:call-template name ="MULTIPOLICY_CREDIT"/>%</xsl:template>
<xsl:template name = "STEPID14">	-  Discount - Valued Customer <xsl:call-template name="VALUEDCUSTOMER_FACTOR"></xsl:call-template>%</xsl:template>
<xsl:template name = "STEPID15">	-  Charge	- Insured to 80% to 99% of Replacement Cost</xsl:template>
<xsl:template name = "STEPID16">
	<xsl:choose>
		<!-- For Michigan REPAIR COST -->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Repair Cost'" >
			1.00
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Repair Cost'" >
			1.00
		</xsl:when>
		<!-- For Michigan REGULAR -->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			1.00
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			1.00
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			1.00
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			1.00
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			1.00
		</xsl:when>
		<!-- For Michigan DELUXE  -->
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Deluxe'" >
			1.00
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Deluxe'" >
			1.00
		</xsl:when>
		<!-- For Michigan PREMIER -->
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Premier'" >
			1.00
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Premier'" >
			1.00
		</xsl:when>
		<!-- For INDIANA -->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='INDIANA' " >
			1.00
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='INDIANA' " >
			<xsl:call-template name="REGULAR_INDIANA_HO3_PRIOR_LOSS_DISPLAY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='INDIANA' " >
			<xsl:call-template name="REGULAR_INDIANA_HO5_PRIOR_LOSS_DISPLAY"/>
		</xsl:when>
		<!-- REPAIR COST-->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='INDIANA' and PRODUCT_PREMIER ='Repair Cost'" >
			1.00
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='INDIANA' and PRODUCT_PREMIER ='Repair Cost'" >
			1.00
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='INDIANA' and PRODUCT_PREMIER = ''" >
			1.00
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='INDIANA' and PRODUCT_PREMIER = ''" >
			1.00
		</xsl:when>
		<!-- DELUXE-->
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='INDIANA' and PRODUCT_PREMIER = 'Deluxe'" >
			1.00
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='INDIANA' and PRODUCT_PREMIER = 'Deluxe'" >
			1.00
		</xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:template>
<xsl:template name = "STEPID17">
<xsl:choose>
		<!-- For Michigan REPAIR COST -->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Repair Cost'" >
			1.00
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Repair Cost'" >
			1.00
		</xsl:when>
		<!-- For Michigan REGULAR -->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			1.00
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			1.00
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			1.00
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			1.00
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			1.00
		</xsl:when>
		<!-- For Michigan DELUXE  -->
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Deluxe'" >
			1.00
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Deluxe'" >
			1.00
		</xsl:when>
		<!-- For Michigan PREMIER -->
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Premier'" >
			1.00
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Premier'" >
			1.00
		</xsl:when>
		<!-- For INDIANA -->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='INDIANA' " >
			1.00
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='INDIANA' " >
			<xsl:call-template name="REGULAR_INDIANA_HO3_WOODSTOVE_DISPLAY"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='INDIANA' " >
			<xsl:call-template name="REGULAR_INDIANA_HO5_WOODSTOVE_DISPLAY"/>
		</xsl:when>
		<!-- REPAIR COST-->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='INDIANA' and PRODUCT_PREMIER ='Repair Cost'" >
			1.00
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='INDIANA' and PRODUCT_PREMIER ='Repair Cost'" >
			1.00
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='INDIANA' and PRODUCT_PREMIER = ''" >
			1.00
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='INDIANA' and PRODUCT_PREMIER = ''" >
			1.00
		</xsl:when>
		<!-- DELUXE-->
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='INDIANA' and PRODUCT_PREMIER = 'Deluxe'" >
			1.00
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='INDIANA' and PRODUCT_PREMIER = 'Deluxe'" >
			1.00
		</xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:template>
<xsl:template name = "STEPID18">	-  Charge	- Breed of Dog <xsl:call-template name="NO_OF_DOGS"/>  <xsl:call-template name="DOGS_CHARGE_INPERCENT"/>%</xsl:template>
<xsl:template name = "STEPID19"><xsl:call-template name ="SEASONALSECONDARYCREDIT"/></xsl:template>
<xsl:template name = "STEPID20">	-  <xsl:call-template name="SELECT_HO_20_PREFERRED_PLUS_OR_HO_21_VIP"></xsl:call-template></xsl:template>
<xsl:template name = "STEPID21">	-  HO-34 Replacement Cost Personal Property</xsl:template>
<xsl:template name = "STEPID22">	-  HO-11 Expanded Replacement Cost for Building</xsl:template>
<xsl:template name = "STEPID23">	-  HO-96 Replacement Cost Personal Property (<xsl:value-of select ="HO96FINALVALUE"/> increase)</xsl:template>
<xsl:template name = "STEPID24">	-  HO-312 Business Property Increase Limits (<xsl:value-of select ="HO312ADDITIONAL"/> increase)</xsl:template>
<xsl:template name = "STEPID25">	-  HO-48 Other Structures Increased Limit</xsl:template>
<xsl:template name = "STEPID26">	-  HO-40 Other Structure Rented To Others (<xsl:value-of select ="HO40ADDITIONAL"/> increase)</xsl:template>
<xsl:template name = "STEPID27">	-  HO-42 Other Structure With Incidental</xsl:template>
<xsl:template name = "STEPID28">	-  HO-53 Credit Card and Depositors Forgery (<xsl:value-of select ="HO53ADDITIONAL"/> increase)</xsl:template>
<xsl:template name = "STEPID29">	-    Unscheduled Jewelry And Furs (<xsl:value-of select ="UNSCHEDULEDJEWELRYADDITIONAL"/> increase)</xsl:template>
<xsl:template name = "STEPID30">	-    Money (<xsl:value-of select ="MONEYADDITIONAL"/> increase)</xsl:template>
<xsl:template name = "STEPID31">	-    Securities (<xsl:value-of select ="SILVERWAREADDITIONAL"/> increase)</xsl:template>
<xsl:template name = "STEPID32">	-    Silverware, Goldware And Pewterware (<xsl:value-of select ="SECURITIESADDITIONAL"/> increase)</xsl:template>
<xsl:template name = "STEPID33">	-    Firearms (<xsl:value-of select ="FIREARMSADDITIONAL"/> increase)</xsl:template>
<xsl:template name = "STEPID34">	-  HO-277 Ordinance or Law</xsl:template>
<xsl:template name = "STEPID35">	-  HO-455 Identity Fraud Expense Coverage</xsl:template>
<xsl:template name = "STEPID36">	-  HO-315 Earthquake (Zone <xsl:value-of select ="TERRITORYZONE"/>)</xsl:template>
<xsl:template name = "STEPID37">	-  HO-327 Water Back-Up and Sump Pump Overflow</xsl:template>
<xsl:template name = "STEPID38">	-  HO-33 Condo-Unit Owners Rental to Others <xsl:value-of select="HO33"/></xsl:template>
<!--New Steps Start-->
<xsl:template name = "STEPID39">	-  HO-373 Contingent Workers Compensation</xsl:template>
<xsl:template name = "STEPID40">	-  Mine Subsidence Coverage</xsl:template>
<xsl:template name = "STEPID41">	-  HO-9 Collapse From Sub-Surface Water</xsl:template>
<!--New Steps End-->
<xsl:template name = "STEPID42">	-  HO-490 Specific Structures Away From Premises</xsl:template>
<xsl:template name = "STEPID43">	-  HO-489 Other Structures Repair Cost Coverage  (<xsl:value-of select ="REPAIRCOSTADDITIONAL"/> increase)</xsl:template>
<xsl:template name = "STEPID44">	-  Primary Residence</xsl:template>
<xsl:template name = "STEPID45">	-  Residence Employees(<xsl:value-of select="RESIDENCE_EMP_NUMBER"/>)</xsl:template>
<xsl:template name = "STEPID46">	-  Additional Premises Occupied By Insured (<xsl:value-of select="format-number(OCCUPIED_INSURED,'#')"/>)</xsl:template>
<xsl:template name = "STEPID47">	-  HO-40 Additional Premises Rented to Others - Residence Premises (<xsl:value-of select="format-number(RESIDENCE_PREMISES,'#')"></xsl:value-of>)</xsl:template>
<xsl:template name = "STEPID48">	-  HO-70 Additional Premises Rented - Other Location - 1 Family (<xsl:value-of select="format-number(OTHER_LOC_1FAMILY,'#')"/>)</xsl:template>
<xsl:template name = "STEPID49">	-  HO-70 Additional Premises Rented - Other Location - 2 Family (<xsl:value-of select="format-number(OTHER_LOC_2FAMILY,'#')"/>)</xsl:template>
<xsl:template name = "STEPID50">	-  HO-42 Incidental Office Private School or Studio On Premises</xsl:template>
<xsl:template name = "STEPID51">	-      Business Located in an Other Structure</xsl:template><!-- SKB-->
<xsl:template name = "STEPID52">	-  HO-42 Incidental Office Private School or Studio Instruction Only</xsl:template>

<xsl:template name = "STEPID53">	-  HO-200 Waterbed Liability</xsl:template>

<xsl:template name = "STEPID54">	-  HO-43 Incidental Office Private School or Studio Off Premises</xsl:template>
<xsl:template name = "STEPID55">	-  HO-71 Business Pursuits</xsl:template>
<xsl:template name = "STEPID56">	-    Class A - (1 persons)</xsl:template>
<xsl:template name = "STEPID57">	-    Class B - (1 persons)</xsl:template>
<xsl:template name = "STEPID58">	-    Class C - (1 persons)</xsl:template>
<xsl:template name = "STEPID59">	-    Class D - (1 persons)</xsl:template>
<xsl:template name = "STEPID60">	-  HO-72 Farm Liability - Incidental Farming Residence Premises</xsl:template>
<xsl:template name = "STEPID61">	-  HO-73 Farm Liability - Owned By Insured Rented to Others </xsl:template>
<xsl:template name = "STEPID62">	-  HO-73 Farm Liability - Owned/Operated By Insured/Insured Employee</xsl:template>
<xsl:template name = "STEPID63">	-  HO-82 Personal Injury</xsl:template>
<xsl:template name = "STEPID64">	-  Bicycle (HO-900)</xsl:template>
<xsl:template name = "STEPID65">	-  Cameras (Non-Professional)</xsl:template>
<xsl:template name = "STEPID66">	-  Cellular Phones (HO-900)</xsl:template>
<xsl:template name = "STEPID67">	-  Furs</xsl:template>
<xsl:template name = "STEPID68">	-  Golf Equipment</xsl:template>
<xsl:template name = "STEPID69">	-  Guns</xsl:template>
<xsl:template name = "STEPID70">	-  Jewelry</xsl:template>
<xsl:template name = "STEPID71">	-  Musical (Non-Professional)</xsl:template>
<xsl:template name = "STEPID72">	-  Personal Computers (HO-214)</xsl:template>
<xsl:template name = "STEPID73">	-  Silver</xsl:template>
<xsl:template name = "STEPID74">	-  Stamps</xsl:template>
<xsl:template name = "STEPID75">	-  Rare Coins</xsl:template>
<xsl:template name = "STEPID76">	-  Fine Arts - Without Breakage</xsl:template>
<xsl:template name = "STEPID77">	-  Fine Arts - With Breakage</xsl:template>
<xsl:template name = "STEPID78">	-  Property Expense Fee</xsl:template>
<xsl:template name = "STEPID79">	-  HO-64 Renters Deluxe Endorsement </xsl:template>
<xsl:template name = "STEPID80">	-  HO-66 Condominium Deluxe Endorsement</xsl:template> 
<xsl:template name = "STEPID81">	-  HO-32 Unit Owners Coverage A Special Coverage</xsl:template> 

<xsl:template name = "STEPID82">	-   HO-50 Personal Property Away From Premises Increase (<xsl:value-of select="PERSONALPROPERTYAWAYADDITIONAL"/>)</xsl:template> 
<xsl:template name = "STEPID83">	-   Building Additions and Alterations (HO-51) (Increase <xsl:value-of select="format-number(PERSONALPROPERTYINCREASEDLIMITADDITIONAL,'#')"/> )  </xsl:template> 
<xsl:template name = "STEPID84">	-   Final Premium </xsl:template> 


<!--<xsl:template name = "STEPID72">	
	<xsl:variable name ="GET_TOTAL_DISCOUNT_VALUE">
		<xsl:call-template name="GET_TOTAL_DISCOUNT"></xsl:call-template>
	</xsl:variable>
	<xsl:choose>
		<xsl:when test ="$GET_TOTAL_DISCOUNT_VALUE = 1.00">
		1.00
		</xsl:when>
		<xsl:otherwise>
		0.00
		</xsl:otherwise>
	</xsl:choose>
	<xsl:value-of select ="$GET_TOTAL_DISCOUNT_VALUE"/>
	1.00
</xsl:template>-->

<!--<xsl:template name ="MULTIPOLICY_DISPLAY">
<xsl:value-of select ="MULTIPOLICY_CREDIT"/>
</xsl:template>-->



<!-- COMBINED ADDITIONAL LIVING EXPENSE  -->
<xsl:template name = "COMBINEDPERSONALPROPERTY"> 
	
	<xsl:variable name="VAR1">
		<xsl:value-of select="PERSONALPROPERTYINCREASEDLIMITINCLUDE"/>
	</xsl:variable>
	
	<xsl:variable name="VAR2">
		<xsl:value-of select="PERSONALPROPERTYINCREASEDLIMITADDITIONAL"/>
	</xsl:variable>

		<xsl:value-of select="round($VAR1+$VAR2)"/>

</xsl:template>

<!-- COMBINED ADDITIONAL LIVING EXPENSE  -->
<xsl:template name = "COMBINEDADDITIONALLIVINGEXPENSE"> 
	
	<xsl:variable name="VAR1">
		<xsl:value-of select="ADDITIONALLIVINGEXPENSEINCLUDE"/>
	</xsl:variable>
	
	<xsl:variable name="VAR2">
		<xsl:value-of select="ADDITIONALLIVINGEXPENSEADDITIONAL"/>
	</xsl:variable>

		<xsl:value-of select="round($VAR1+$VAR2)"/>

</xsl:template>




<!-- ============================================================================================ -->
<!--									Template for Lables  (END)								  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--									Additional Coverage Template (START) 					  -->
<!-- ============================================================================================ -->

<xsl:template name ="ADDITIONALCOVERAGE">
	(<xsl:call-template name ="BREEDDOG"/>) + 
	(<xsl:call-template name="PREFERRED_PLUS_COVERAGE"/>)+
	<!--(<xsl:call-template name="HO_34_REPLACEMENT_COST"/>) +-->
	(<xsl:call-template name="SELECT_HO_34_COVERAGE"/>)+
	(<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY_COVERAGE"/>)+
	(<xsl:call-template name="HO_96_REPLACEMENT_COST_PERSONAL_PROPERTY"/>)+
	(<xsl:call-template name="HO_312_BUSINESS_PROPERTY_INCREASED_LIMITS"/>)+
	(<xsl:call-template name="HO_48_OTHER_STRUCTURE_INCREASED_LIMITS"/>)+
	(<xsl:call-template name="HO_40_OTHER_STRUCTURE_RENTED_TO_OTHERS"/>)+
	(<xsl:call-template name="HO_42_OTHER_STRUCTURE_WITH_INCIDENTAL"/>)+
	(<xsl:call-template name="HO_53_INCREASED_CREDIT_CARD"/>)+
	(<xsl:call-template name="UNSCHEDULED_JEWELRY_ADDITIONAL"/>)+
	(<xsl:call-template name="MONEY"/>)+
	(<xsl:call-template name="SECURITIES"/>)+
	(<xsl:call-template name="SILVERWARE_GOLDWARE"/>)+
	(<xsl:call-template name="FIREARMS"/>)+
	(<xsl:call-template name="ORDINANCE"/>)+
	(<xsl:call-template name="HO_455_IDENTITY_FRAUD"/>)+
	(<xsl:call-template name="HO_315_EARTHQUAKE"/>)+
	(<xsl:call-template name="HO_327_WATER_BACK_UP"/>)+
	(<xsl:call-template name="HO_287_MINE_SUBSIDENCE_COVERAGE"/>)+
	(<xsl:call-template name="HO_9_COLLAPSE_FROM_SUB_SURFACE_WATER"/>)+
	(<xsl:call-template name="HO_490_SPECIFIC_STRUCTURED"/>)+
	(<xsl:call-template name="HO_489_SPECIFIC_STRUCTURED"/>)+
	(<xsl:call-template name="PRIMARY_RESIDENCE"/>)+
	(<xsl:call-template name="RESIDENCE_EMPLOYEES"/>)+
	(<xsl:call-template name="ADDITIONAL_PREMISES"/>)+
	(<xsl:call-template name="ADDITIONAL_RESIDENCE_PREMISES"/>)+
	(<xsl:call-template name="ADDITIONAL_OTHER_LOCATION_PREMISES_FAMILY1"/>)+
	(<xsl:call-template name="ADDITIONAL_OTHER_LOCATION_PREMISES_FAMILY2"/>)+
	(<xsl:call-template name="HO_42_ON_PREMISES"/>)+
	(<xsl:call-template name="HO42_BUSINESS_OTHER_STRUCTURE_WITH_INCIDENTAL"/>)+
	(<xsl:call-template name="HO_42_INSTRUCTION_ONLY"/>)+
	(<xsl:call-template name="HO_43_OFF_PREMISES"/>)+
	(<xsl:call-template name="HO_71_CLASSA"/>)+
	(<xsl:call-template name="HO_71_CLASSB"/>)+
	(<xsl:call-template name="HO_71_CLASSC"/>)+
	(<xsl:call-template name="HO_71_CLASSD"/>)+
	(<xsl:call-template name="HO_72_INCIDENTAL_FARMING"/>)+
	(<xsl:call-template name="HO_73_OTH_LOC_OPR_EMPL"/>)+
	(<xsl:call-template name="HO_73_OTH_LOC_OPR_OTHERS"/>)+
	(<xsl:call-template name="HO_82_PERSONAL_INJURY"/>)+
	(<xsl:call-template name="HO_900_BICYCLES_PREMIUM"/>)+
	(<xsl:call-template name="CAMERAS_PREMIUM"/>)+
	(<xsl:call-template name="HO_900_CELLULAR_PHONES_PREMIUM"/>)+
	(<xsl:call-template name="FURS_PREMIUM"/>)+
	(<xsl:call-template name="GOLF_EQUIPMENT_PREMIUM"/>)+
	(<xsl:call-template name="GUNS_PREMIUM"/>)+
	(<xsl:call-template name="JEWELRY_PREMIUM"/>)+
	(<xsl:call-template name="MUSICAL_NON_PROFESSIONAL_PREMIUM"/>)+
	(<xsl:call-template name="PERSONAL_COMPUTERS_PREMIUM"/>)+
	(<xsl:call-template name="SILVER_PREMIUM"/>)+
	(<xsl:call-template name="STAMPS_PREMIUM"/>)+
	(<xsl:call-template name="RARE_COINS_PREMIUM"/>)+
	(<xsl:call-template name="FINE_ARTS_WITHOUT_BREAKAGE_PREMIUM"/>)+
	(<xsl:call-template name="FINE_ARTS_BREAKAGE_PREMIUM"/>)+
	(<xsl:call-template name="PROPFEE"/>)+
	(<xsl:call-template name="HO_200_WATERBED_LIABILITY"/>)+
	(<xsl:call-template name="HO_33_CONDO_UNIT"/>)
</xsl:template>


<!-- ============================================================================================ -->
<!--									Additional Coverage Template (END) 	    				  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--									Final Primium Template (START)   						  -->
<!-- ============================================================================================ -->
<xsl:template name ="FINALPRIMIUM">
{
	(<xsl:call-template name="CALL_ADJUSTEDBASE"/>) +
	(<xsl:call-template name="ADDITIONALCOVERAGE"/>)
	
}
</xsl:template>
<!-- ============================================================================================ -->
<!--									Final Primium Template (END)   							  -->
<!-- ============================================================================================ -->


<!-- ###################################  START - MICHIGAN ###################################### -->

<!-- ============================================================================================ -->
<!--							ADJUSTEDBASE FOR MICHIGAN(REGULAR) HO-2							  -->
<!-- ============================================================================================ -->
<xsl:template name="ADJUSTEDBASE_REGULAR_MICHIGAN_HO2">
(((((((((((((<xsl:call-template name="DEWLLING-MAIN"></xsl:call-template> )*
			(<xsl:call-template name ="C-VALUE"></xsl:call-template>)*
			(<xsl:call-template name ="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)
			)
			+
			(
				(<xsl:call-template name ="COVERAGE_C_PREMIUM"/> )
				+ (<xsl:call-template name ="COVERAGE_D_PREMIUM"/>)
			)
		    
	   )
	   *
	   (<xsl:call-template name ="DFACTOR"/>))*
	    <!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5 -->
		(
			<xsl:variable name="AGEOFHOMEFACTOR">
				<xsl:call-template name ="AGEOFHOME_FACTOR"/>
			</xsl:variable>			
			<xsl:choose>
				<xsl:when test="$AGEOFHOMEFACTOR &lt; 0.50">
					<xsl:value-of select ="$AGEOFHOMEFACTOR"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$AGEOFHOMEFACTOR"/>
				</xsl:otherwise>
			</xsl:choose>
			
		))*
		(<xsl:call-template name = "GET_PROTECTIVE_DEVICE_DISCOUNT"/>))*
		(<xsl:call-template name = "GET_DUC_DISCOUNT"/>))*
		(<xsl:call-template name = "GET_EXPERIENCE_DISCOUNT"/>))*
		(<xsl:call-template name ="GET_MULTIPOLICY_DISCOUNT"/>))*
		(1.00<!--xsl:call-template name ="VALUEDCUSTOMER"/-->))*
		 <!-- TILL HERE -->
	   (<xsl:call-template name ="PRIOR_LOSS"/>))*
	   (<xsl:call-template name ="WSTOVE"/>))*
	   (1.00<!--xsl:call-template name ="BREEDDOG"/-->))*
	   (<xsl:call-template name ="SEASONALSECONDARYCREDIT"/>)
</xsl:template>

<!-- Wood StoveLAbel Display -->
<xsl:template name ="WOODSTOVE_LABEL_DISPLAY">
((((((((((<xsl:call-template name="DEWLLING-MAIN"></xsl:call-template> )*
			(<xsl:call-template name ="C-VALUE"></xsl:call-template>)*
			(<xsl:call-template name ="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)
			)
			+
			(
				(<xsl:call-template name ="COVERAGE_C_PREMIUM"/> )
				+ (<xsl:call-template name ="COVERAGE_D_PREMIUM"/>)
			)
		    
	   )
	   *
	   (<xsl:call-template name ="DFACTOR"/>))*
	   (<xsl:call-template name ="AGEOFHOME_FACTOR"/>))*
	   (<xsl:call-template name ="PROTECTIVEDEVICE"/>))*
	   (<xsl:call-template name ="EXPERIENCEFACTOR"/>))*
	   (<xsl:call-template name ="MULTIPOLICY_FACTOR"/>))*
	   (<xsl:call-template name ="VALUEDCUSTOMER"/>))*
	   (<xsl:call-template name ="PRIOR_LOSS"/>))*
	   (<xsl:call-template name ="WSTOVE"/>) 
</xsl:template>
<!-- ============================================================================================ -->
<!--					ADJUSTEDBASE FOR MICHIGAN(REPAIR COST) HO-2	and HO-3					  -->
<!-- ============================================================================================ -->
<xsl:template name="ADJUSTEDBASE_REPAIRCOST_MICHIGAN_HO2_HO3">
((((((((((((((<xsl:call-template name="DEWLLING-MAIN"></xsl:call-template> )*
			(<xsl:call-template name ="C_VALUE_HO_4_AND_6"></xsl:call-template>)*
			(1.23)*
			(<xsl:call-template name ="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)
			)
			+
			(
				(<xsl:call-template name ="COVERAGE_C_PREMIUM"/> )
				+ (<xsl:call-template name ="COVERAGE_D_PREMIUM"/>)
			)
		    
	   )
	   *
	   (<xsl:call-template name ="DFACTOR"/>))*
	    <!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5 -->
		(
			<xsl:variable name="AGEOFHOMEFACTOR">
				<xsl:call-template name ="AGEOFHOME_FACTOR"/>
			</xsl:variable>			
			<xsl:choose>
				<xsl:when test="$AGEOFHOMEFACTOR &lt; 0.50">
					<xsl:value-of select ="$AGEOFHOMEFACTOR"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$AGEOFHOMEFACTOR"/>
				</xsl:otherwise>
			</xsl:choose>
			
		))*
		(<xsl:call-template name = "GET_PROTECTIVE_DEVICE_DISCOUNT"/>))*
		(<xsl:call-template name = "GET_DUC_DISCOUNT"/>))*
		(<xsl:call-template name = "GET_EXPERIENCE_DISCOUNT"/>))*
		(<xsl:call-template name ="GET_MULTIPOLICY_DISCOUNT"/>))*
		(1.00<!--xsl:call-template name ="VALUEDCUSTOMER"/-->))*
		 <!-- TILL HERE -->
	   (<xsl:call-template name ="PRIOR_LOSS"/>))*
	   (<xsl:call-template name ="WSTOVE"/>))*
	   (1.00<!--xsl:call-template name ="BREEDDOG"/-->))-
	   (<xsl:call-template name ="SEASONALSECONDARYCREDIT"/>))+
	   (<xsl:call-template name ="MEDICAL_CHARGES"/>)+
	   (<xsl:call-template name ="LIABLITY_CHARGES"/>)
</xsl:template>

<!-- ============================================================================================ -->
<!--							ADJUSTEDBASE FOR MICHIGAN(PREMIER) HO-3							  -->
<!-- ============================================================================================ -->
<xsl:template name="ADJUSTEDBASE_PREMIER_MICHIGAN_HO3">
((((((((((((((<xsl:call-template name="DEWLLING-MAIN"></xsl:call-template> )*
			(<xsl:call-template name ="C-VALUE"></xsl:call-template>)*
			(<xsl:call-template name ="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)
			)
			+
			(
				(<xsl:call-template name ="COVERAGE_C_PREMIUM"/> )
				+ (<xsl:call-template name ="COVERAGE_D_PREMIUM"/>)
			)
		    
	   )
	   *
	   (<xsl:call-template name ="DFACTOR"/>))*
	    <!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5 -->
		(
			<xsl:variable name="AGEOFHOMEFACTOR">
				<xsl:call-template name ="AGEOFHOME_FACTOR"/>
			</xsl:variable>			
			<xsl:choose>
				<xsl:when test="$AGEOFHOMEFACTOR &lt; 0.50">
					<xsl:value-of select ="$AGEOFHOMEFACTOR"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$AGEOFHOMEFACTOR"/>
				</xsl:otherwise>
			</xsl:choose>
			
		))*
		(<xsl:call-template name = "GET_PROTECTIVE_DEVICE_DISCOUNT"/>))*
		(<xsl:call-template name = "GET_DUC_DISCOUNT"/>))*
		(<xsl:call-template name = "GET_EXPERIENCE_DISCOUNT"/>))*
		(<xsl:call-template name ="GET_MULTIPOLICY_DISCOUNT"/>))*
		(<xsl:call-template name ="VALUEDCUSTOMER"/>))*
		 <!-- TILL HERE -->
	   (<xsl:call-template name ="PRIOR_LOSS"/>))*
	   (<xsl:call-template name ="WSTOVE"/>))*
	   (1.00<!--xsl:call-template name ="BREEDDOG"/-->))-
	   (<xsl:call-template name ="SEASONALSECONDARYCREDIT"/>))+
	   (<xsl:call-template name ="MEDICAL_CHARGES"/>)+
	   (<xsl:call-template name ="LIABLITY_CHARGES"/>)
</xsl:template>
<!-- ============================================================================================ -->
<!--							ADJUSTEDBASE FOR MICHIGAN(REGULAR) HO-3							  -->
<!-- ============================================================================================ -->
<xsl:template name="ADJUSTEDBASE_REGULAR_MICHIGAN_HO3">
((((((((((((((<xsl:call-template name="DEWLLING-MAIN"></xsl:call-template> )*
			(<xsl:call-template name ="C-VALUE"></xsl:call-template>)*
			(<xsl:call-template name ="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)
			)
			+
			(
				(<xsl:call-template name ="COVERAGE_C_PREMIUM"/> )
				+ (<xsl:call-template name ="COVERAGE_D_PREMIUM"/>)
			)
		    
	   )
	   *
	   (<xsl:call-template name ="DFACTOR"/>))*
	    <!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5 -->
		(
			<xsl:variable name="AGEOFHOMEFACTOR">
				<xsl:call-template name ="AGEOFHOME_FACTOR"/>
			</xsl:variable>			
			<xsl:choose>
				<xsl:when test="$AGEOFHOMEFACTOR &lt; 0.50">
					<xsl:value-of select ="$AGEOFHOMEFACTOR"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$AGEOFHOMEFACTOR"/>
				</xsl:otherwise>
			</xsl:choose>
			
		))*
		(<xsl:call-template name = "GET_PROTECTIVE_DEVICE_DISCOUNT"/>))*
		(<xsl:call-template name = "GET_DUC_DISCOUNT"/>))*
		(<xsl:call-template name = "GET_EXPERIENCE_DISCOUNT"/>))*
		(<xsl:call-template name ="GET_MULTIPOLICY_DISCOUNT"/>))*
		(<xsl:call-template name ="VALUEDCUSTOMER"/>))*
		 <!-- TILL HERE -->
	   (<xsl:call-template name ="PRIOR_LOSS"/>))*
	   (<xsl:call-template name ="WSTOVE"/>))*
	   (1.00<!--xsl:call-template name ="BREEDDOG"/-->))-
	   (<xsl:call-template name ="SEASONALSECONDARYCREDIT"/>))+
	   (<xsl:call-template name ="MEDICAL_CHARGES"/>)+
	   (<xsl:call-template name ="LIABLITY_CHARGES"/>)
</xsl:template>

<!-- ============================================================================================ -->
<!--							ADJUSTEDBASE FOR MICHIGAN(REGULAR) HO-4 OR HO-6					  -->
<!-- ============================================================================================ -->
<xsl:template name="ADJUSTEDBASE_REGULAR_MICHIGAN_HO4_HO6">
(((((((((((((<xsl:call-template name="DEWLLING-MAIN"></xsl:call-template> ) *
			(<xsl:call-template name ="C_VALUE_HO_4_AND_6"></xsl:call-template>)*
			(<xsl:call-template name ="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>))*
			(<xsl:call-template name ="DFACTOR"/>))*
			<!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5 -->
			(
				<xsl:variable name="AGEOFHOMEFACTOR">
					<xsl:call-template name ="AGEOFHOME_FACTOR"/>
				</xsl:variable>			
				<xsl:choose>
					<xsl:when test="$AGEOFHOMEFACTOR &lt; 0.50">
						<xsl:value-of select ="$AGEOFHOMEFACTOR"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$AGEOFHOMEFACTOR"/>
					</xsl:otherwise>
				</xsl:choose>
				
			))*
			(<xsl:call-template name = "GET_PROTECTIVE_DEVICE_DISCOUNT"/>))*
			(<xsl:call-template name = "GET_DUC_DISCOUNT"/>))*
			(<xsl:call-template name = "GET_EXPERIENCE_DISCOUNT"/>))*
			(<xsl:call-template name ="GET_MULTIPOLICY_DISCOUNT"/>))*
			(1.00<!--xsl:call-template name ="VALUEDCUSTOMER"/-->))*
			<!-- TILL HERE -->
			(<xsl:call-template name ="PRIOR_LOSS"/>))*
			(<xsl:call-template name ="WSTOVE"/>))*
			(1.00<!--xsl:call-template name ="BREEDDOG"/-->))-
			(<xsl:call-template name ="SEASONALSECONDARYCREDIT"/>))+
	   (<xsl:call-template name ="MEDICAL_CHARGES"/>)+
	   (<xsl:call-template name ="LIABLITY_CHARGES"/>)
</xsl:template>
<!-- ============================================================================================ -->
<!--							ADJUSTEDBASE FOR MICHIGAN(REGULER) HO-5					  -->
<!-- ============================================================================================ -->
<xsl:template name="ADJUSTEDBASE_REGULAR_MICHIGAN_HO5">
(((((((((((((((((<xsl:call-template name="DEWLLING-MAIN"></xsl:call-template> )*
			(<xsl:call-template name ="C-VALUE"></xsl:call-template>)*
			(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name ="DFACTOR"></xsl:call-template>)
			)
			+
			(
				+ (<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>)
				+ (75.00<!--xsl:call-template name="HO_34_REPLACEMENT_COST"/-->)
				+ (<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>)
			)
		    
	   )
	   *
	   (<xsl:call-template name ="INSURANCE_SCORE_CREDIT"/>))*
	   (<xsl:call-template name ="DFACTOR"/>))*
	    <!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5 -->
		(
			<xsl:variable name="AGEOFHOMEFACTOR">
				<xsl:call-template name ="AGEOFHOME_FACTOR"/>
			</xsl:variable>			
			<xsl:choose>
				<xsl:when test="$AGEOFHOMEFACTOR &lt; 0.50">
					<xsl:value-of select ="$AGEOFHOMEFACTOR"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$AGEOFHOMEFACTOR"/>
				</xsl:otherwise>
			</xsl:choose>
			
		))*
	   	(<xsl:call-template name = "GET_PROTECTIVE_DEVICE_DISCOUNT"/>))*
		(<xsl:call-template name = "GET_DUC_DISCOUNT"/>))*
		(<xsl:call-template name = "GET_EXPERIENCE_DISCOUNT"/>))*
		(<xsl:call-template name ="GET_MULTIPOLICY_DISCOUNT"/>))*
		(<xsl:call-template name ="VALUEDCUSTOMER"/>))*
		 <!-- TILL HERE -->
	   (<xsl:call-template name ="PRIOR_LOSS"/>))*
	   (<xsl:call-template name ="WSTOVE"/>))*
	   (1.00<!--xsl:call-template name ="BREEDDOG"/-->))*
	   (<xsl:call-template name ="SEASONALSECONDARYCREDIT"/>))-
	   (<xsl:call-template name ="COVERAGE_C_PREMIUM"/> ))+
	   ((<xsl:call-template name ="COVERAGE_C_PREMIUM"/> )+
	   (<xsl:call-template name ="COVERAGE_D_PREMIUM"/>)))+
	   (<xsl:call-template name ="MEDICAL_CHARGES"/>)+
	   (<xsl:call-template name ="LIABLITY_CHARGES"/>)
</xsl:template>
<!-- ============================================================================================ -->
<!--							ADJUSTEDBASE PREMIER FOR MICHIGAN(PREMIER) HO-5					  -->
<!-- ============================================================================================ -->
<xsl:template name="ADJUSTEDBASE_PREMIER_MICHIGAN_HO5">
((((((((((((((((<xsl:call-template name="DEWLLING-MAIN"></xsl:call-template> )*
			(<xsl:call-template name ="C-VALUE"></xsl:call-template>)*
			(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name ="DFACTOR"></xsl:call-template>)
			)
			+
			(
				(<xsl:call-template name ="COVERAGE_C_PREMIUM"/> )
				+ (<xsl:call-template name ="COVERAGE_D_PREMIUM"/>)
			)
		    
	   )
	   *
	   (<xsl:call-template name ="INSURANCE_SCORE_CREDIT"/>))*
	   (<xsl:call-template name ="DFACTOR"/>))*
	    <!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5 -->
		(
			<xsl:variable name="AGEOFHOMEFACTOR">
				<xsl:call-template name ="AGEOFHOME_FACTOR"/>
			</xsl:variable>			
			<xsl:choose>
				<xsl:when test="$AGEOFHOMEFACTOR &lt; 0.50">
					<xsl:value-of select ="$AGEOFHOMEFACTOR"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$AGEOFHOMEFACTOR"/>
				</xsl:otherwise>
			</xsl:choose>
			
		))*
	   	(<xsl:call-template name = "GET_PROTECTIVE_DEVICE_DISCOUNT"/>))*
		(<xsl:call-template name = "GET_DUC_DISCOUNT"/>))*
		(<xsl:call-template name = "GET_EXPERIENCE_DISCOUNT"/>))*
		(<xsl:call-template name ="GET_MULTIPOLICY_DISCOUNT"/>))*
		(<xsl:call-template name ="VALUEDCUSTOMER"/>))*
		 <!-- TILL HERE -->
	   (<xsl:call-template name ="PRIOR_LOSS"/>))*
	   (<xsl:call-template name ="WSTOVE"/>))*
	   (1.00<!--xsl:call-template name ="BREEDDOG"/-->))-
	   (<xsl:call-template name ="SEASONALSECONDARYCREDIT"/>))+
	   (<xsl:call-template name ="COVERAGE_C_PREMIUM"/> ))+
	   (<xsl:call-template name ="MEDICAL_CHARGES"/>)+
	   (<xsl:call-template name ="LIABLITY_CHARGES"/>)
</xsl:template>

<!-- ###################################  END - MICHIGAN ######################################### -->


<!-- ###################################  START - INDIANA  ####################################### -->

<!-- ============================================================================================ -->
<!--								ADJUSTEDBASE FOR INDIANA (REGULAR) HO-2						  -->
<!-- ============================================================================================ -->
<xsl:template name="ADJUSTEDBASE_REGULAR_INDIANA_HO2">
((((((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template> )*
			(<xsl:call-template name ="C_VALUE_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name ="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name ="DFACTOR"/>)*
			(<xsl:call-template name ="AGEOFHOME_FACTOR"/>)
			)
			+
			(
				(<xsl:call-template name ="COVERAGE_C_PREMIUM"/> )
				+ (<xsl:call-template name ="COVERAGE_D_PREMIUM"/>)
			)
		    
	   )
	   *
	   (<xsl:call-template name ="PROTECTIVEDEVICE"/>))*
	   (<xsl:call-template name = "GET_DUC_DISCOUNT"/>))*
	   (<xsl:call-template name ="SMOKER"/>))*
	   (<xsl:call-template name ="EXPERIENCEFACTOR"/>))*
	   (<xsl:call-template name ="MULTIPOLICY_FACTOR"/>))*
	   (<xsl:call-template name ="PRIOR_LOSS"/>))*
	   (<xsl:call-template name="WSTOVE"/>))*
	   (<xsl:call-template name="VALUEDCUSTOMER"/>))-
	   (<xsl:call-template name="SEASONALSECONDARYCREDIT"/>))+
	   (<xsl:call-template name ="MEDICAL_CHARGES"/>)+
	   (<xsl:call-template name ="LIABLITY_CHARGES"/>)
</xsl:template>

<!-- ============================================================================================ -->
<!--								ADJUSTEDBASE FOR INDIANA (REGULAR) HO-3						  -->
<!-- ============================================================================================ -->
<xsl:template name="ADJUSTEDBASE_REGULAR_INDIANA_HO3">
(((((((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name ="C_VALUE_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name ="REPLACEMENT_COST"/>)*
			(<xsl:call-template name ="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name ="DFACTOR"/>)*
			(<xsl:call-template name ="AGEOFHOME_FACTOR"/>)
			)
			+
			(
				(<xsl:call-template name ="COVERAGE_C_PREMIUM"/> )
				+ (<xsl:call-template name ="COVERAGE_D_PREMIUM"/>)
			)
		    
	   )
	   *
	   (<xsl:call-template name ="PROTECTIVEDEVICE"/>))*
	   (<xsl:call-template name = "GET_DUC_DISCOUNT"/>))*
	   (<xsl:call-template name ="SMOKER"/>))*
	   (<xsl:call-template name ="EXPERIENCEFACTOR"/>))*
	   (<xsl:call-template name ="MULTIPOLICY_FACTOR"/>))*
	   (<xsl:call-template name ="PRIOR_LOSS"/>))*
	   (<xsl:call-template name="WSTOVE"/>))*
	   (<xsl:call-template name="VALUEDCUSTOMER"/>))-
	   (<xsl:call-template name="SEASONALSECONDARYCREDIT"/>))+
	   (<xsl:call-template name="DESCRIBED_PRIMARY_RESIDENCE"/>))+
	   (<xsl:call-template name ="MEDICAL_CHARGES"/>)+
	   (<xsl:call-template name ="LIABLITY_CHARGES"/>)
</xsl:template>

<!-- ============================================================================================ -->
<!--								ADJUSTEDBASE FOR INDIANA (REGULAR) HO-5						  -->
<!-- ============================================================================================ -->
<xsl:template name="ADJUSTEDBASE_REGULAR_INDIANA_HO5">
(((((((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template> )*
			(<xsl:call-template name ="C_VALUE_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name ="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name ="DFACTOR"/>)*
			(<xsl:call-template name ="AGEOFHOME_FACTOR"/>)
			)
			+
			(
				(<xsl:call-template name ="COVERAGE_C_PREMIUM"/> )
				+ (<xsl:call-template name ="COVERAGE_D_PREMIUM"/>)
				+ (<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>)
				+ (75.00<!--xsl:call-template name="HO_34_REPLACEMENT_COST"/-->)
				+ (<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>)
			)
		    
	   )
	   *
	   (<xsl:call-template name ="PROTECTIVEDEVICE"/>))*
	   (<xsl:call-template name = "GET_DUC_DISCOUNT"/>))*
	   (<xsl:call-template name ="SMOKER"/>))*
	   (<xsl:call-template name ="EXPERIENCEFACTOR"/>))*
	   (<xsl:call-template name ="MULTIPOLICY_FACTOR"/>))*
	   (<xsl:call-template name ="PRIOR_LOSS"/>))*
	   (<xsl:call-template name="WSTOVE"/>))*
	   (<xsl:call-template name="VALUEDCUSTOMER"/>))-
	   (<xsl:call-template name="SEASONALSECONDARYCREDIT"/>))+
	   (<xsl:call-template name="DESCRIBED_PRIMARY_RESIDENCE"/>))+
	   (<xsl:call-template name ="MEDICAL_CHARGES"/>)+
	   (<xsl:call-template name ="LIABLITY_CHARGES"/>)
</xsl:template>

<!-- ============================================================================================ -->
<!--								ADJUSTEDBASE FOR INDIANA (REPAIR COST) HO-2 and HO-3		  -->
<!-- ============================================================================================ -->
<xsl:template name="ADJUSTEDBASE_REPAIRCOST_INDIANA_HO2_HO3">
(((((((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template> )*
			(<xsl:call-template name ="C_VALUE_INDIANA"></xsl:call-template>)*
			(1.21)*
			(<xsl:call-template name ="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name ="DFACTOR"/>)*
			(<xsl:call-template name ="AGEOFHOME_FACTOR"/>)
			)
			+
			(
				(<xsl:call-template name ="COVERAGE_C_PREMIUM"/> )
				+ (<xsl:call-template name ="COVERAGE_D_PREMIUM"/>)
			)
		    
	   )
	   *
	   (<xsl:call-template name ="PROTECTIVEDEVICE"/>))*
	   (<xsl:call-template name ="SMOKER"/>))*
	   (<xsl:call-template name = "GET_DUC_DISCOUNT"/>))*
	   (<xsl:call-template name ="EXPERIENCEFACTOR"/>))*
	   (<xsl:call-template name ="MULTIPOLICY_FACTOR"/>))*
	   (<xsl:call-template name ="PRIOR_LOSS"/>))*
	   (<xsl:call-template name="WSTOVE"/>))*
	   (<xsl:call-template name="VALUEDCUSTOMER"/>))-
	   (<xsl:call-template name="SEASONALSECONDARYCREDIT"/>))+
	   (<xsl:call-template name="DESCRIBED_PRIMARY_RESIDENCE"/>))+
	   (<xsl:call-template name ="MEDICAL_CHARGES"/>)+
	   (<xsl:call-template name ="LIABLITY_CHARGES"/>)
</xsl:template>

<!-- ============================================================================================ -->
<!--								ADJUSTEDBASE FOR INDIANA (REGULAR) HO-4					  -->
<!-- ============================================================================================ -->
<xsl:template name="ADJUSTEDBASE_REGULAR_INDIANA_HO4">
((((((((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template> )*
			(<xsl:call-template name ="C_VALUE_HO_4_AND_6"></xsl:call-template>)*
			(1.21)*
			(<xsl:call-template name ="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name ="DFACTOR"/>)*
			(<xsl:call-template name ="AGEOFHOME_FACTOR"/>)
			)
			+
			(
				(0.00 )<!-- Will be removed later-->
				+ (0.00)<!-- Will be removed later-->
			)
		    
	   )
	   *
	   (<xsl:call-template name ="PROTECTIVEDEVICE"/>))*
	   (<xsl:call-template name ="SMOKER"/>))*
	   (<xsl:call-template name = "GET_DUC_DISCOUNT"/>))*
	   (<xsl:call-template name ="EXPERIENCEFACTOR"/>))*
	   (<xsl:call-template name ="MULTIPOLICY_FACTOR"/>))*
	   (<xsl:call-template name ="PRIOR_LOSS"/>))*
	   (<xsl:call-template name="WSTOVE"/>))*
	   (<xsl:call-template name="VALUEDCUSTOMER"/>))*
	   (1.10)) -
	   (<xsl:call-template name="SEASONALSECONDARYCREDIT"/>))+
	   (<xsl:call-template name="DESCRIBED_PRIMARY_RESIDENCE"/>))+
	   (<xsl:call-template name ="MEDICAL_CHARGES"/>)+
	   (<xsl:call-template name ="LIABLITY_CHARGES"/>)
</xsl:template>

<!-- ============================================================================================ -->
<!--								ADJUSTEDBASE FOR INDIANA (REGULAR) HO-6						  -->
<!-- ============================================================================================ -->
<xsl:template name="ADJUSTEDBASE_REGULAR_INDIANA_HO6">
(((((((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template> )*
			(<xsl:call-template name ="C_VALUE_HO_4_AND_6"></xsl:call-template>)*
			(1.15)*
			(<xsl:call-template name ="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name ="DFACTOR"/>)*
			(<xsl:call-template name ="AGEOFHOME_FACTOR"/>)
			)
			+
			(
				(0.00 )<!-- Will be removed later-->
				+ (0.00)<!-- Will be removed later-->
			)
		    
	   )
	   *
	   (<xsl:call-template name ="PROTECTIVEDEVICE"/>))*
	   (<xsl:call-template name ="SMOKER"/>))*
	   (<xsl:call-template name = "GET_DUC_DISCOUNT"/>))*
	   (<xsl:call-template name ="EXPERIENCEFACTOR"/>))*
	   (<xsl:call-template name ="MULTIPOLICY_FACTOR"/>))*
	   (<xsl:call-template name ="PRIOR_LOSS"/>))*
	   (<xsl:call-template name="WSTOVE"/>))*
	   (<xsl:call-template name="VALUEDCUSTOMER"/>))-
	   (<xsl:call-template name="SEASONALSECONDARYCREDIT"/>))+
	   (<xsl:call-template name="DESCRIBED_PRIMARY_RESIDENCE"/>))+
	   (<xsl:call-template name ="MEDICAL_CHARGES"/>)+
	   (<xsl:call-template name ="LIABLITY_CHARGES"/>)
</xsl:template>

<!-- ============================================================================================ -->
<!--								ADJUSTEDBASE FOR INDIANA (DELUXE) HO-4			     		  -->
<!-- ============================================================================================ -->
<xsl:template name="ADJUSTEDBASE_DELUXE_INDIANA_HO4">
(((((((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template> )*
			(<xsl:call-template name ="C_VALUE_HO_4_AND_6"></xsl:call-template>)*
			(1.15)*
			(<xsl:call-template name ="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name ="DFACTOR"/>)*
			(<xsl:call-template name ="AGEOFHOME_FACTOR"/>)
			)
			+
			(
				(0.00 )<!-- Will be removed later-->
				+ (0.00)<!-- Will be removed later-->
			)
		    
	   )
	   *
	   (<xsl:call-template name ="PROTECTIVEDEVICE"/>))*
	   (<xsl:call-template name ="SMOKER"/>))*
	   (<xsl:call-template name = "GET_DUC_DISCOUNT"/>))*
	   (<xsl:call-template name ="EXPERIENCEFACTOR"/>))*
	   (<xsl:call-template name ="MULTIPOLICY_FACTOR"/>))*
	   (<xsl:call-template name ="PRIOR_LOSS"/>))*
	   (<xsl:call-template name="WSTOVE"/>))*
	   (<xsl:call-template name="VALUEDCUSTOMER"/>))-
	   (<xsl:call-template name="SEASONALSECONDARYCREDIT"/>))+
	   (<xsl:call-template name="DESCRIBED_PRIMARY_RESIDENCE"/>))+
	   (<xsl:call-template name ="MEDICAL_CHARGES"/>)+
	   (<xsl:call-template name ="LIABLITY_CHARGES"/>)
</xsl:template>

<!-- ============================================================================================ -->
<!--								ADJUSTEDBASE FOR INDIANA (DELUXE) HO-6						  -->
<!-- ============================================================================================ -->
<xsl:template name="ADJUSTEDBASE_DELUXE_INDIANA_HO6">
((((((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template> )*
			(<xsl:call-template name ="C_VALUE_HO_4_AND_6"></xsl:call-template>)*
			(<xsl:call-template name ="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name ="DFACTOR"/>)*
			(<xsl:call-template name ="AGEOFHOME_FACTOR"/>)
			)
			+
			(
				(0.00 )<!-- Will be removed later-->
				+ (0.00)<!-- Will be removed later-->
			)
		    
	   )
	   *
	   (<xsl:call-template name ="PROTECTIVEDEVICE"/>))*
	   (<xsl:call-template name ="SMOKER"/>))*
	   (<xsl:call-template name ="EXPERIENCEFACTOR"/>))*
	   (<xsl:call-template name ="MULTIPOLICY_FACTOR"/>))*
	   (<xsl:call-template name ="PRIOR_LOSS"/>))*
	   (<xsl:call-template name="WSTOVE"/>))*
	   (<xsl:call-template name="VALUEDCUSTOMER"/>))-
	   (<xsl:call-template name="SEASONALSECONDARYCREDIT"/>))+
	   (<xsl:call-template name="DESCRIBED_PRIMARY_RESIDENCE"/>))+
	   (<xsl:call-template name ="MEDICAL_CHARGES"/>)+
	   (<xsl:call-template name ="LIABLITY_CHARGES"/>)
</xsl:template>

<!-- ###################################  END - INDIANA  ######################################## -->

<!-- ============================================================================================ -->
<!--						TEMPLATE FOR CALLING ADJUSTEDBASE AS PER THE INPUT-XML (START)		  -->
<!-- ============================================================================================ -->

<xsl:template name ="CALL_ADJUSTEDBASE">
	<xsl:choose>
		<!-- For Michigan REPAIR COST -->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Repair Cost'" >
			<xsl:call-template name ="ADJUSTEDBASE_REPAIRCOST_MICHIGAN_HO2_HO3"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Repair Cost'" >
			<xsl:call-template name ="ADJUSTEDBASE_REPAIRCOST_MICHIGAN_HO2_HO3"/>
		</xsl:when>
		<!-- For Michigan REGULAR -->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="ADJUSTEDBASE_REGULAR_MICHIGAN_HO2"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="ADJUSTEDBASE_REGULAR_MICHIGAN_HO3"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="ADJUSTEDBASE_REGULAR_MICHIGAN_HO4_HO6"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="ADJUSTEDBASE_REGULAR_MICHIGAN_HO5"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''" >
			  <xsl:call-template name ="ADJUSTEDBASE_REGULAR_MICHIGAN_HO4_HO6"/>  
		</xsl:when>
		<!-- For Michigan DELUXE  -->
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Deluxe'" >
			
			  <xsl:call-template name ="ADJUSTEDBASE_REGULAR_MICHIGAN_HO4_HO6"/>   
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Deluxe'" >
		 
			 <xsl:call-template name ="ADJUSTEDBASE_REGULAR_MICHIGAN_HO4_HO6"/>  
			
		</xsl:when>
		<!-- For Michigan PREMIER -->
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Premier'" >
			<xsl:call-template name ="ADJUSTEDBASE_PREMIER_MICHIGAN_HO3"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Premier'" >
			<xsl:call-template name ="ADJUSTEDBASE_PREMIER_MICHIGAN_HO5"/>
		</xsl:when>
		<!-- For INDIANA -->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='INDIANA' and PRODUCT_PREMIER =''" >
			<xsl:call-template name ="ADJUSTEDBASE_REGULAR_INDIANA_HO2"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='INDIANA'  and PRODUCT_PREMIER =''" >
			<xsl:call-template name ="ADJUSTEDBASE_REGULAR_INDIANA_HO3"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-5' and STATENAME ='INDIANA' " >
			<xsl:call-template name ="ADJUSTEDBASE_REGULAR_INDIANA_HO5"/>
		</xsl:when>
		<!-- REPAIR COST-->
		<xsl:when test ="PRODUCTNAME = 'HO-2' and STATENAME ='INDIANA' and PRODUCT_PREMIER ='Repair Cost'" >
			<xsl:call-template name ="ADJUSTEDBASE_REPAIRCOST_INDIANA_HO2_HO3"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-3' and STATENAME ='INDIANA' and PRODUCT_PREMIER ='Repair Cost'" >
			<xsl:call-template name ="ADJUSTEDBASE_REPAIRCOST_INDIANA_HO2_HO3"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='INDIANA' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="ADJUSTEDBASE_REGULAR_INDIANA_HO4"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='INDIANA' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="ADJUSTEDBASE_REGULAR_INDIANA_HO6"/>
		</xsl:when>
		<!-- DELUXE-->
		<xsl:when test ="PRODUCTNAME = 'HO-4' and STATENAME ='INDIANA' and PRODUCT_PREMIER = 'Deluxe'" >
			<xsl:call-template name ="ADJUSTEDBASE_DELUXE_INDIANA_HO4"/>
		</xsl:when>
		<xsl:when test ="PRODUCTNAME = 'HO-6' and STATENAME ='INDIANA' and PRODUCT_PREMIER = 'Deluxe'" >
			<xsl:call-template name ="ADJUSTEDBASE_DELUXE_INDIANA_HO6"/>
		</xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--						TEMPLATE FOR CALLING ADJUSTEDBASE AS PER THE INPUT-XML (END)		  -->
<!-- ============================================================================================ -->



<!-- ============================================================================================ -->
<!--									TEST TEMPLATE 	(START)									  -->
<!-- ============================================================================================ -->

<xsl:template name ="TestRound" >
	<xsl:param name ="MyValue"/>
	{
	<xsl:value-of select="round(normalize-space($VAR-DEWLLING-MAIN))"></xsl:value-of>
	}
</xsl:template>

 <!--Adjusted Base Primium Template 
<xsl:variable  name ="VAR-DEWLLING-MAIN">
	<xsl:call-template name ="DEWLLING-MAIN"></xsl:call-template>
</xsl:variable>-->

<xsl:variable  name ="VAR-DEWLLING-MAIN">
	<xsl:call-template name ="DEWLLING-MAIN"></xsl:call-template>
</xsl:variable>

<!--<xsl:template name ="P_HO96_FINALVALUE">
	(<xsl:value-of select ="HO96ADDITIONAL"></xsl:value-of> +
	<xsl:value-of select ="HO96INCLUDE"></xsl:value-of>)
</xsl:template>-->

<xsl:template name = "GET_PROTECTIVE_DEVICE_DISCOUNT">
	<xsl:choose>
		<xsl:when test ="CONSTRUCTIONCREDIT = 'N'">
		
			<xsl:variable name="FINALCREDIT_AGEOFHOME">
					(1.00 - <xsl:call-template name ="AGEOFHOME_FACTOR"/>)
			</xsl:variable>
			<!-- Calculate the final credit uptil age of home 
				 If the credit is less than 0.50 then it means 
				 that total discount is greater than 50%  and we do not need to send Protective Device discount-->				 
			<xsl:choose>
				<xsl:when test = "$FINALCREDIT_AGEOFHOME &gt; 0.50">
					<xsl:value-of select="$FINALCREDIT_AGEOFHOME"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:variable name="PROTDEVICE_1"><xsl:call-template name ="PROTECTIVEDEVICE"/></xsl:variable>
						<xsl:variable name="FINALCREDIT_PROTDEVICE">
							(
								<xsl:value-of select="$FINALCREDIT_AGEOFHOME"/>
								+
								(1.00 - (<xsl:value-of select="$PROTDEVICE_1"/>))
							)
						</xsl:variable>
						<xsl:choose>
						<xsl:when test = "$FINALCREDIT_PROTDEVICE = 0.00">
								<xsl:call-template name ="PROTECTIVEDEVICE"/>
						</xsl:when>		
						<xsl:otherwise>
							IF(<xsl:value-of select ="$FINALCREDIT_PROTDEVICE"/> &gt; 0.50)
							THEN
								(<xsl:value-of select="$FINALCREDIT_PROTDEVICE"/> - .50  ) + 
									(<xsl:value-of select="$PROTDEVICE_1"/>)
							ELSE
								IF (<xsl:value-of select="$FINALCREDIT_PROTDEVICE"/> &gt; 0.50)  
								THEN
									<xsl:variable name="PROTDEVICEFACTOR">
										(0.50-(1.00-<xsl:value-of select="$FINALCREDIT_PROTDEVICE"/>)) 
									</xsl:variable>
									<xsl:value-of select="$PROTDEVICEFACTOR"/>					
								ELSE IF (<xsl:value-of select="$FINALCREDIT_PROTDEVICE"/> &lt;= 0.50) THEN
									<xsl:value-of select="$PROTDEVICE_1"/>
								ELSE
									1.00	
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

<xsl:template name = "GET_DUC_DISCOUNT">
	
			<xsl:variable name="FINALCREDIT_PROT_DEVICE_DISCOUNT">
				(1.00-(<xsl:call-template name ="AGEOFHOME_FACTOR"/>))
				+(1.00 -(<xsl:call-template name ="GET_PROTECTIVE_DEVICE_DISCOUNT"/>))
			</xsl:variable>
			
			<!-- Calculate the final credit of home and protective device. 
				 If the credit is greater than 0.50 then it means that total discount is less than 50%  -->
			<xsl:choose>
				<xsl:when test="$FINALCREDIT_PROT_DEVICE_DISCOUNT &gt; 0.50">
					(<xsl:value-of select="$FINALCREDIT_PROT_DEVICE_DISCOUNT"/> - 0.50)
				</xsl:when>
				<xsl:otherwise>
					<xsl:variable name="DUC"><xsl:call-template name ="DWELLING_UNDER_CONSTRUCTION"/></xsl:variable>
					<xsl:variable name="FINALCREDIT_PROT_DEVICE_NEW">
						(
							(<xsl:value-of select="$FINALCREDIT_PROT_DEVICE_DISCOUNT"/>)
							+
							(1.00 - (<xsl:value-of select="$DUC"/>))
						)
					</xsl:variable>
					<xsl:choose>
						<xsl:when test ="$FINALCREDIT_PROT_DEVICE_NEW = 0.00">
							<xsl:call-template name ="DWELLING_UNDER_CONSTRUCTION"/>
						</xsl:when>	
						<xsl:otherwise>
							IF(<xsl:value-of select ="$FINALCREDIT_PROT_DEVICE_NEW"/> &gt; 0.50)
							THEN
								(<xsl:value-of select="$FINALCREDIT_PROT_DEVICE_NEW"/> - 0.50  ) + 
								(<xsl:value-of select = "$DUC"/>)
							ELSE
									IF (<xsl:value-of select="$FINALCREDIT_PROT_DEVICE_NEW"/> &gt; 0.50) 
									THEN
										<xsl:variable name="DUCFACTOR">
											(0.50-(1.00-<xsl:value-of select="$FINALCREDIT_PROT_DEVICE_NEW"/>)) 
										</xsl:variable>
										<xsl:value-of select="$DUCFACTOR"/>					
									ELSE IF (<xsl:value-of select="$FINALCREDIT_PROT_DEVICE_NEW"/> &lt;= 0.50) THEN
										<xsl:value-of select="$DUC"/>
									ELSE
										1.00						
						</xsl:otherwise>
					</xsl:choose>
				</xsl:otherwise>
				
			</xsl:choose>
			<!--including prot device, the credit becomes > 50% --> 
</xsl:template>



<xsl:template name = "GET_EXPERIENCE_DISCOUNT">
			<xsl:variable name="FINALCREDIT_DUC">
				(1.00-(<xsl:call-template name ="AGEOFHOME_FACTOR"/>))
				+ (1.00-(<xsl:call-template name ="GET_PROTECTIVE_DEVICE_DISCOUNT"/>))
				+ (1.00-(<xsl:call-template name ="GET_DUC_DISCOUNT"/>))
			</xsl:variable>
			
			<xsl:choose>
				<xsl:when test = "$FINALCREDIT_DUC &gt; 0.50">
						(<xsl:value-of select="$FINALCREDIT_DUC"/> - 0.50)
				</xsl:when>		
				<xsl:otherwise>	
						<xsl:variable name="EXPERIENCE"><xsl:call-template name ="EXPERIENCEFACTOR"/></xsl:variable>		
						<xsl:variable name="FINALCREDIT_EXPERIENCE">
						(
							(<xsl:value-of select="$FINALCREDIT_DUC"/>)
							+
							(1.00-(<xsl:value-of select="$EXPERIENCE"/>))
						)
						</xsl:variable>
						<xsl:choose>
							<xsl:when test ="$FINALCREDIT_EXPERIENCE = 0.00">
								<xsl:call-template name ="EXPERIENCEFACTOR"/>
							</xsl:when>		
							<xsl:otherwise>
								IF( <xsl:value-of select ="$FINALCREDIT_EXPERIENCE"/> &gt; 0.50)
								THEN
									(<xsl:value-of select="$FINALCREDIT_EXPERIENCE"/> - 0.50  ) + 
									(<xsl:value-of select="$EXPERIENCE"/>)
								ELSE IF (<xsl:value-of select="$FINALCREDIT_EXPERIENCE"/> &lt;= 0.50) 
								THEN
									<xsl:value-of select="$EXPERIENCE"/>
								ELSE
									1.00						
						</xsl:otherwise>	
					</xsl:choose>
				</xsl:otherwise>
			</xsl:choose>			
					<!--including prot device, the credit becomes > 50% -->
</xsl:template>


<xsl:template name = "GET_MULTIPOLICY_DISCOUNT">
			<xsl:variable name="FINALCREDIT_EXPERIENCE">
				(1.00-(<xsl:call-template name ="AGEOFHOME_FACTOR"/>))
				+	(1.00-(<xsl:call-template name ="GET_PROTECTIVE_DEVICE_DISCOUNT"/>))
				+	(1.00-(<xsl:call-template name ="GET_DUC_DISCOUNT"/>))
				+	(1.00-(<xsl:call-template name ="GET_EXPERIENCE_DISCOUNT"/>))
			</xsl:variable>
			
			<xsl:choose>
				<xsl:when test="$FINALCREDIT_EXPERIENCE &gt; 0.50">
						(<xsl:value-of select="$FINALCREDIT_EXPERIENCE"/> - 0.50)
				</xsl:when>
				<xsl:otherwise>
				
					<xsl:variable name="MULTIPOLICY"><xsl:call-template name ="MULTIPOLICY_FACTOR"/></xsl:variable>
					<xsl:variable name="FINALCREDIT_MULTIPOLICY">
					(
						<xsl:value-of select="$FINALCREDIT_EXPERIENCE"/> +
						1.00 - (<xsl:value-of select="$MULTIPOLICY"/>)
					)
					</xsl:variable>
					<!--<xsl:value-of select="$FINALCREDIT_MULTIPOLICY"/>	-->
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
						
					
				</xsl:otherwise>				
			</xsl:choose>	
</xsl:template>

<xsl:template name = "GET_TOTAL_DISCOUNT">
	<xsl:variable name="FINALCREDIT_EXPERIENCE">
		(1.00-(<xsl:call-template name ="AGEOFHOME_FACTOR"/>))
		+	(1.00-(<xsl:call-template name ="GET_PROTECTIVE_DEVICE_DISCOUNT"/>))
		+	(1.00-(<xsl:call-template name ="GET_DUC_DISCOUNT"/>))
		+	(1.00-(<xsl:call-template name ="GET_EXPERIENCE_DISCOUNT"/>))
		+	(1.00-(<xsl:call-template name ="GET_MULTIPOLICY_DISCOUNT"/>))
	</xsl:variable>
	
	IF (<xsl:value-of select ="$FINALCREDIT_EXPERIENCE"/> &gt; 0.50) 
	THEN
		1.00
	ELSE
		0.00
</xsl:template>

<xsl:template name="REGULAR_INDIANA_HO3_WOODSTOVE_DISPLAY">
((((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name ="C_VALUE_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name ="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name ="DFACTOR"/>)*
			(<xsl:call-template name ="AGEOFHOME_FACTOR"/>)
			)
			+
			(
				(<xsl:call-template name ="COVERAGE_C_PREMIUM"/> )
				+ (<xsl:call-template name ="COVERAGE_D_PREMIUM"/>)
			)
		    
	   )
	   *
	   (<xsl:call-template name ="PROTECTIVEDEVICE"/>))*
	   (<xsl:call-template name = "GET_DUC_DISCOUNT"/>))*
	   (<xsl:call-template name ="SMOKER"/>))*
	   (<xsl:call-template name ="EXPERIENCEFACTOR"/>))*
	   (<xsl:call-template name ="MULTIPOLICY_FACTOR"/>))*
	   (<xsl:call-template name ="PRIOR_LOSS"/>))*
	   (<xsl:call-template name="WSTOVE"/>)) - 
	   (((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name ="C_VALUE_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name ="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name ="DFACTOR"/>)*
			(<xsl:call-template name ="AGEOFHOME_FACTOR"/>)
			)
			+
			(
				(<xsl:call-template name ="COVERAGE_C_PREMIUM"/> )
				+ (<xsl:call-template name ="COVERAGE_D_PREMIUM"/>)
			)
		    
	   )
	   *
	   (<xsl:call-template name ="PROTECTIVEDEVICE"/>))*
	   (<xsl:call-template name = "GET_DUC_DISCOUNT"/>))*
	   (<xsl:call-template name ="SMOKER"/>))*
	   (<xsl:call-template name ="EXPERIENCEFACTOR"/>))*
	   (<xsl:call-template name ="MULTIPOLICY_FACTOR"/>))*
	   (<xsl:call-template name ="PRIOR_LOSS"/>))
	  
</xsl:template>

<xsl:template name="REGULAR_INDIANA_HO3_PRIOR_LOSS_DISPLAY">
(((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name ="C_VALUE_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name ="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name ="DFACTOR"/>)*
			(<xsl:call-template name ="AGEOFHOME_FACTOR"/>)
			)
			+
			(
				(<xsl:call-template name ="COVERAGE_C_PREMIUM"/> )
				+ (<xsl:call-template name ="COVERAGE_D_PREMIUM"/>)
			)
		    
	   )
	   *
	   (<xsl:call-template name ="PROTECTIVEDEVICE"/>))*
	   (<xsl:call-template name = "GET_DUC_DISCOUNT"/>))*
	   (<xsl:call-template name ="SMOKER"/>))*
	   (<xsl:call-template name ="EXPERIENCEFACTOR"/>))*
	   (<xsl:call-template name ="MULTIPOLICY_FACTOR"/>))*
	   (<xsl:call-template name ="PRIOR_LOSS"/>))-
	   ((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name ="C_VALUE_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name ="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name ="DFACTOR"/>)*
			(<xsl:call-template name ="AGEOFHOME_FACTOR"/>)
			)
			+
			(
				(<xsl:call-template name ="COVERAGE_C_PREMIUM"/> )
				+ (<xsl:call-template name ="COVERAGE_D_PREMIUM"/>)
			)
		    
	   )
	   *
	   (<xsl:call-template name ="PROTECTIVEDEVICE"/>))*
	   (<xsl:call-template name = "GET_DUC_DISCOUNT"/>))*
	   (<xsl:call-template name ="SMOKER"/>))*
	   (<xsl:call-template name ="EXPERIENCEFACTOR"/>))*
	   (<xsl:call-template name ="MULTIPOLICY_FACTOR"/>))
	  
</xsl:template>
<!-- For HO5 INDIANA START-->
<xsl:template name="REGULAR_INDIANA_HO5_PRIOR_LOSS_DISPLAY">
	(((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template> )*
		(<xsl:call-template name ="C_VALUE_INDIANA"></xsl:call-template>)*
		(<xsl:call-template name ="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
		(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)*
		(<xsl:call-template name ="DFACTOR"/>)*
		(<xsl:call-template name ="AGEOFHOME_FACTOR"/>)
		)
		+
		(
			(<xsl:call-template name ="COVERAGE_C_PREMIUM"/> )
			+ (<xsl:call-template name ="COVERAGE_D_PREMIUM"/>)
			+ (<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>)
			+ (75.00<!--xsl:call-template name="HO_34_REPLACEMENT_COST"/-->)
			+ (<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>)
		)
		
	)
	*
	(<xsl:call-template name ="PROTECTIVEDEVICE"/>))*
	(<xsl:call-template name = "GET_DUC_DISCOUNT"/>))*
	(<xsl:call-template name ="SMOKER"/>))*
	(<xsl:call-template name ="EXPERIENCEFACTOR"/>))*
	(<xsl:call-template name ="MULTIPOLICY_FACTOR"/>))*
	(<xsl:call-template name ="PRIOR_LOSS"/>))
	
	-
	((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template> )*
			(<xsl:call-template name ="C_VALUE_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name ="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name ="DFACTOR"/>)*
			(<xsl:call-template name ="AGEOFHOME_FACTOR"/>)
			)
			+
			(
				(<xsl:call-template name ="COVERAGE_C_PREMIUM"/> )
				+ (<xsl:call-template name ="COVERAGE_D_PREMIUM"/>)
				+ (<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>)
				+ (75.00<!--xsl:call-template name="HO_34_REPLACEMENT_COST"/-->)
				+ (<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>)
			)
		    
	   )
	   *
	   (<xsl:call-template name ="PROTECTIVEDEVICE"/>))*
	   (<xsl:call-template name = "GET_DUC_DISCOUNT"/>))*
	   (<xsl:call-template name ="SMOKER"/>))*
	   (<xsl:call-template name ="EXPERIENCEFACTOR"/>))*
	   (<xsl:call-template name ="MULTIPOLICY_FACTOR"/>))
	   
</xsl:template>

<xsl:template name="REGULAR_INDIANA_HO5_WOODSTOVE_DISPLAY">
	((((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template> )*
		(<xsl:call-template name ="C_VALUE_INDIANA"></xsl:call-template>)*
		(<xsl:call-template name ="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
		(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)*
		(<xsl:call-template name ="DFACTOR"/>)*
		(<xsl:call-template name ="AGEOFHOME_FACTOR"/>)
		)
		+
		(
			(<xsl:call-template name ="COVERAGE_C_PREMIUM"/> )
			+ (<xsl:call-template name ="COVERAGE_D_PREMIUM"/>)
			+ (<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>)
			+ (75.00<!--xsl:call-template name="HO_34_REPLACEMENT_COST"/-->)
			+ (<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>)
		)
		
	)
	*
	(<xsl:call-template name ="PROTECTIVEDEVICE"/>))*
	(<xsl:call-template name = "GET_DUC_DISCOUNT"/>))*
	(<xsl:call-template name ="SMOKER"/>))*
	(<xsl:call-template name ="EXPERIENCEFACTOR"/>))*
	(<xsl:call-template name ="MULTIPOLICY_FACTOR"/>))*
	(<xsl:call-template name ="PRIOR_LOSS"/>))*
	(<xsl:call-template name="WSTOVE"/>))
	-
	((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template> )*
			(<xsl:call-template name ="C_VALUE_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name ="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name ="DFACTOR"/>)*
			(<xsl:call-template name ="AGEOFHOME_FACTOR"/>)
			)
			+
			(
				(<xsl:call-template name ="COVERAGE_C_PREMIUM"/> )
				+ (<xsl:call-template name ="COVERAGE_D_PREMIUM"/>)
				+ (<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP"/>)
				+ (75.00<!--xsl:call-template name="HO_34_REPLACEMENT_COST"/-->)
				+ (<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY"/>)
			)
		    
	   )
	   *
	   (<xsl:call-template name ="PROTECTIVEDEVICE"/>))*
	   (<xsl:call-template name = "GET_DUC_DISCOUNT"/>))*
	   (<xsl:call-template name ="SMOKER"/>))*
	   (<xsl:call-template name ="EXPERIENCEFACTOR"/>))*
	   (<xsl:call-template name ="MULTIPOLICY_FACTOR"/>))
	
	   
</xsl:template>
<!-- ============================================================================================ -->
<!--									TEST TEMPLATE 	(END)									  -->
<!-- ============================================================================================ -->


<!-- ############################################################################################ -->
<!--						New Apporach for calculating the ADJUSTABLE BASE					  -->
<!--						To check the results Call 'ADJUSTEDBASE_REGULAR_MICHIGAN_HO3_12'
							template at STEP NO : 0												  -->
<!-- ############################################################################################ -->

<xsl:template name="ADJUSTEDBASE_REGULAR_MICHIGAN_HO3_12">
<xsl:variable name ="VER1">
	<xsl:call-template name="DEWLLING-MAIN-TEMP"/>
</xsl:variable>

<xsl:variable name ="VER2">
	<xsl:call-template name="C-VALUE"/>
</xsl:variable>

<xsl:variable name ="VER3">
	<xsl:call-template name="TERM_FACTOR"/>
</xsl:variable>

<xsl:variable name ="VER4">
	<xsl:call-template name ="INSURANCE_SCORE_CREDIT"/>
</xsl:variable>

<xsl:variable name ="VER5">
	<xsl:call-template name ="COVERAGE_C_PREMIUM_1"/>
</xsl:variable>

<xsl:variable name ="VER6">
	<xsl:call-template name ="DFACTOR"/>
</xsl:variable>

<xsl:variable name ="VER7">
	<xsl:variable name="AGEOFHOMEFACTOR">
		<xsl:call-template name ="AGEOFHOME_FACTOR"/>
	</xsl:variable>			
	<xsl:choose>
		<xsl:when test="$AGEOFHOMEFACTOR &lt; 0.50">
			<xsl:value-of select ="$AGEOFHOMEFACTOR"/>
		</xsl:when>
		<xsl:otherwise>
			<xsl:value-of select="$AGEOFHOMEFACTOR"/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:variable>

<xsl:variable name ="VER8">
	<xsl:call-template name ="GET_PROTECTIVE_DEVICE_DISCOUNT_1"/>
</xsl:variable>

<xsl:variable name ="VER9">
	<xsl:call-template name ="GET_DUC_DISCOUNT_TEMP"/>
</xsl:variable>

<xsl:variable name ="VER10">
	<xsl:call-template name ="GET_EXPERIENCE_DISCOUNT_TEMP"/>
</xsl:variable>

<xsl:variable name ="VER11">
	<xsl:call-template name ="GET_MULTIPOLICY_DISCOUNT_TEMP"/>
</xsl:variable>

<xsl:variable name ="VER12">
	<xsl:call-template name ="GET_MULTIPOLICY_DISCOUNT_TEMP"/>
</xsl:variable>

<xsl:value-of select ="round(round(round(round(round(round(round(round(round(round(round(round($VER1)*$VER2)*$VER3)*$VER4)+ ($VER5 + 0.00))*$VER6)*$VER7)*$VER8)*$VER9)*$VER10)*$VER11)*$VER12)"/>

<!--<xsl:value-of select ="$VER8"/>-->
</xsl:template>
<xsl:template name="ADJUSTEDBASE_REGULAR_MICHIGAN_HO3_11">
(((((((((((((<xsl:call-template name="DEWLLING-MAIN"></xsl:call-template> )*
			(<xsl:call-template name ="C-VALUE"></xsl:call-template>)*
			(<xsl:call-template name ="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)
			)
			+
			(
				(<xsl:call-template name ="COVERAGE_C_PREMIUM"/> )
				+ (<xsl:call-template name ="COVERAGE_D_PREMIUM"/>)
			)
		    
	   )
	   *
	   (<xsl:call-template name ="DFACTOR"/>))*
	    <!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5 -->
		(
			<xsl:variable name="AGEOFHOMEFACTOR">
				<xsl:call-template name ="AGEOFHOME_FACTOR"/>
			</xsl:variable>			
			<xsl:choose>
				<xsl:when test="$AGEOFHOMEFACTOR &lt; 0.50">
					<xsl:value-of select ="$AGEOFHOMEFACTOR"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$AGEOFHOMEFACTOR"/>
				</xsl:otherwise>
			</xsl:choose>
			
		))*
		(<xsl:call-template name = "GET_PROTECTIVE_DEVICE_DISCOUNT"/>))*
		(<xsl:call-template name = "GET_DUC_DISCOUNT"/>))*
		(<xsl:call-template name = "GET_EXPERIENCE_DISCOUNT"/>))*
		(<xsl:call-template name ="GET_MULTIPOLICY_DISCOUNT"/>))*
		(<xsl:call-template name ="VALUEDCUSTOMER"/>))*
		 <!-- TILL HERE -->
	   (<xsl:call-template name ="PRIOR_LOSS"/>))*
	   (<xsl:call-template name ="WSTOVE"/>))*
	   (1.00<!--xsl:call-template name ="BREEDDOG"/-->))-
	   (<xsl:call-template name ="SEASONALSECONDARYCREDIT"/>)
</xsl:template>

<xsl:template name="DEWLLING-MAIN-TEMP">
	
	<xsl:variable name="TERCODES" select="TERRITORYCODES"/>
	<xsl:variable name="GROUP_ID" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY']/NODE[@ID='TERRGROUPING']/ATTRIBUTES [@TID = $TERCODES]/@GROUPID"/>
			
	<xsl:variable name="F_CODE" select="FORM_CODE"/>
	<xsl:variable name="FORMGROUP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING']/ATTRIBUTES [@FORMID = $F_CODE]/@GROUPID"/>
	<xsl:variable name="PCODE" select="PRODUCTNAME"/>
	
	<xsl:choose>
		<xsl:when test ="$FORMGROUP = 1">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1"/>			
		</xsl:when>
		<xsl:when test ="$FORMGROUP = 2">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code2"/>
		</xsl:when>
		<xsl:when test ="$FORMGROUP = 3">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code3"/>		
		</xsl:when>
		<xsl:when test ="$FORMGROUP = 4">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code4"/>
		</xsl:when>
		<xsl:when test ="$FORMGROUP = 5">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code5"/>
		</xsl:when>
		<xsl:when test ="$FORMGROUP = 6">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code6"/>
		</xsl:when>
		<xsl:when test ="$FORMGROUP = 7">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code7"/>
		</xsl:when>
		<xsl:when test ="$FORMGROUP = 8">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code8"/>
		</xsl:when>
		<xsl:when test ="$FORMGROUP = 9">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code9"/>
		</xsl:when>
		<xsl:when test ="$FORMGROUP = 10">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code10"/>		
		</xsl:when>
		<xsl:otherwise>
		1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<xsl:template name="PREMIUM_GROUP">
	<!--
	<xsl:variable name="TERCODES" select="TERRITORYCODES"/>
	<xsl:variable name="GROUP_ID" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY']/NODE[@ID='TERRGROUPING']/ATTRIBUTES [@TID = $TERCODES]/@GROUPID"/>
	-->
	<xsl:variable name="F_CODE" select="FORM_CODE"/>
	<xsl:choose> 
	<xsl:when test="$F_CODE != ' '">
		<xsl:value-of  select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING']/ATTRIBUTES [@FORMID = $F_CODE]/@GROUPID"/>
	</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<xsl:template name="COVERAGE_C_PREMIUM_1">
	<xsl:variable name="PNAME" select="PRODUCTNAME"></xsl:variable>
	<xsl:variable name ="PCOVERAGE_C_ADDITIONAL_VALUE" select ="PERSONALPROPERTYINCREASEDLIMITADDITIONAL"></xsl:variable>
	<xsl:variable name ="PCOVERAGE_C_ADDITIONAL_COST" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = $PNAME]/@COST"></xsl:variable>
	<xsl:variable name ="PCOVERAGE_C_ADDITIONAL_INCREASEDAMOUNT" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = $PNAME]/@INCREASEDAMOUNT"></xsl:variable>
	
	
	<xsl:choose>
		<xsl:when test ="$PCOVERAGE_C_ADDITIONAL_VALUE = 0">0</xsl:when>
		<xsl:otherwise>
			((<xsl:value-of select ="$PCOVERAGE_C_ADDITIONAL_COST"></xsl:value-of>
			*	
			<xsl:value-of select ="$PCOVERAGE_C_ADDITIONAL_VALUE"></xsl:value-of>) DIV
			<xsl:value-of select ="$PCOVERAGE_C_ADDITIONAL_INCREASEDAMOUNT"></xsl:value-of>)
		</xsl:otherwise>
	</xsl:choose>
	
	
</xsl:template>

			<!-- Calculate the final credit uptil age of home 
				 If the credit is less than 0.50 then it means 
				 that total discount is greater than 50%  and we do not need to send Protective Device discount-->				 

<xsl:template name = "GET_PROTECTIVE_DEVICE_DISCOUNT_1">
	<xsl:choose>
		<xsl:when test ="CONSTRUCTIONCREDIT = 'N'">
		
			<xsl:variable name="FINALCREDIT_AGEOFHOME_TOTAL_DISCOUNT">
					<xsl:call-template name ="AGEOFHOME_FACTOR"/>
			</xsl:variable>
			
			<xsl:variable name="FINALCREDIT_AGEOFHOME">
					<xsl:value-of select ="1.00 - $FINALCREDIT_AGEOFHOME_TOTAL_DISCOUNT"/>
			</xsl:variable>
		
			
			<xsl:choose>
				<xsl:when test = "$FINALCREDIT_AGEOFHOME &gt; 0.50">
					<xsl:value-of select="$FINALCREDIT_AGEOFHOME"/>
				</xsl:when>
				<xsl:otherwise>
						<xsl:variable name="PROTDEVICE_1">
							<xsl:call-template name ="PROTECTIVEDEVICE_TEMP"/>
						</xsl:variable>
						<xsl:variable name="FINALCREDIT_PROTDEVICE">
							<xsl:value-of select="$FINALCREDIT_AGEOFHOME + (1.00 - ($PROTDEVICE_1))"/>
						</xsl:variable>
						<xsl:choose>
						<xsl:when test = "$FINALCREDIT_PROTDEVICE = 0.00">
								<xsl:call-template name ="PROTECTIVEDEVICE_TEMP"/>
						</xsl:when>		
						<xsl:otherwise>
							<xsl:choose>
								<xsl:when test ="FINALCREDIT_PROTDEVICE &gt; 0.50">
									<xsl:value-of select="($FINALCREDIT_PROTDEVICE - 0.50 ) + $PROTDEVICE_1"/>
								</xsl:when>
								<xsl:otherwise>
									<xsl:choose>
										<xsl:when test ="$FINALCREDIT_PROTDEVICE &gt; 0.50">
											<xsl:variable name="PROTDEVICEFACTOR">
												<xsl:value-of select="(0.50-(1.00-$FINALCREDIT_PROTDEVICE))"/> 
											</xsl:variable>
											<xsl:value-of select="$PROTDEVICEFACTOR"/>	
										</xsl:when>
										<xsl:when test ="$FINALCREDIT_PROTDEVICE &lt;= 0.50">
											<xsl:value-of select="$PROTDEVICE_1"/>
										</xsl:when>
										<xsl:otherwise>1.00</xsl:otherwise>
									</xsl:choose>	
								</xsl:otherwise>
							</xsl:choose>
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

<xsl:template name="PROTECTIVEDEVICE_TEMP">
	<xsl:choose>
		<xsl:when test ="CONSTRUCTIONCREDIT = 'N'">
			<xsl:variable name ="VER1"><xsl:call-template name ="DIRECTBURGLERANDFIRE_TEMP"/></xsl:variable>
			<xsl:variable name ="VER2"><xsl:call-template name ="DIRECTFIREANDPOLICE_TEMP"/></xsl:variable>
			<xsl:variable name ="VER3"><xsl:call-template name ="LOCALFIREGASALARM_TEMP"/></xsl:variable>
				
				
			<xsl:variable name="PROTECTIVE_DEVICE_VALUE">
				<xsl:value-of select ="$VER1 + $VER2 + $VER3"></xsl:value-of>
			</xsl:variable>
			
			<xsl:choose>
				<xsl:when test ="$PROTECTIVE_DEVICE_VALUE = 0.00">1.00</xsl:when>
				<xsl:when test ="$PROTECTIVE_DEVICE_VALUE &gt; 15.00">0.85</xsl:when>
				<xsl:when test ="$PROTECTIVE_DEVICE_VALUE &gt; 0.00 and $PROTECTIVE_DEVICE_VALUE &lt; 15.00">
					<xsl:value-of select ="(100.00 - $PROTECTIVE_DEVICE_VALUE) div 100.00"></xsl:value-of>
				</xsl:when>
				<xsl:otherwise>1.00</xsl:otherwise>
			</xsl:choose>
		</xsl:when>	
		<xsl:otherwise>1.00</xsl:otherwise>
	</xsl:choose>	
</xsl:template>


<!-- Central Stations Burglary & Fire Alarm System -->
<xsl:template name="DIRECTBURGLERANDFIRE_TEMP"> 

<xsl:choose>
	<xsl:when test ="BURGLAR = 'Y' and CENTRAL_FIRE = 'Y'">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='CSBF']/@CREDIT"/>
	</xsl:when>
	<xsl:when test ="BURGLAR = 'Y'">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='CSB']/@CREDIT"/>
	</xsl:when>
	<xsl:when test ="CENTRAL_FIRE = 'Y'">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='CSF']/@CREDIT"/>
	</xsl:when>
	<xsl:otherwise>0.00</xsl:otherwise>
</xsl:choose>
</xsl:template>


<!-- Direct to Fire and Police -->
<xsl:template name="DIRECTFIREANDPOLICE_TEMP"> 
<xsl:choose>
	<xsl:when test ="BURGLER_ALERT_POLICE = 'Y' and FIRE_ALARM_FIREDEPT = 'Y'">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='DFP']/@CREDIT"/>
	</xsl:when>
	<xsl:when test ="BURGLER_ALERT_POLICE = 'Y'">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='DP']/@CREDIT"/>
	</xsl:when>
	<xsl:when test ="FIRE_ALARM_FIREDEPT = 'Y'">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='DF']/@CREDIT"/>
	</xsl:when>
	<xsl:otherwise>0.00</xsl:otherwise>
</xsl:choose>	
</xsl:template>

<!-- Local Fire or Local Gas Alarm -->
<xsl:template name="LOCALFIREGASALARM_TEMP"> 
<xsl:choose>
	<xsl:when test ="N0_LOCAL_ALARM = 1">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='LFLG']/@CREDIT"/>
	</xsl:when>
	<xsl:when test ="N0_LOCAL_ALARM = 1">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='LFA']/@CREDIT"/>
	</xsl:when>
	<xsl:otherwise>0.00</xsl:otherwise>
</xsl:choose>
</xsl:template>

<xsl:template name = "GET_DUC_DISCOUNT_TEMP">
			
			<xsl:variable name="FINALCREDIT_PROT_DEVICE_DISCOUNT_TOTAL_DISCOUNT">
				<xsl:variable name = "VAR1">
					<xsl:call-template name ="AGEOFHOME_FACTOR"/>
				</xsl:variable>
				<xsl:variable name  = "VAR2">
					<xsl:call-template name ="GET_PROTECTIVE_DEVICE_DISCOUNT_1"/>
				</xsl:variable>
				
				<xsl:value-of select ="(1.00 - ($VAR1)) + (1.00 - ($VAR2))"/>
			</xsl:variable>
			
			<xsl:variable name="FINALCREDIT_PROT_DEVICE_DISCOUNT">
					<!--<xsl:call-template name ="GET_PROTECTIVE_DEVICE_DISCOUNT_1"/>-->
					0.00
			</xsl:variable>
			
			<xsl:choose>
				<xsl:when test="$FINALCREDIT_PROT_DEVICE_DISCOUNT &gt; 0.50">
					<xsl:value-of select="$FINALCREDIT_PROT_DEVICE_DISCOUNT - 0.50"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:variable name="DUC"><xsl:call-template name ="DWELLING_UNDER_CONSTRUCTION"/></xsl:variable>
					<xsl:variable name="FINALCREDIT_PROT_DEVICE_NEW">
						<xsl:value-of select="$FINALCREDIT_PROT_DEVICE_DISCOUNT + (1.00 - $DUC)"/>
					</xsl:variable>
					<xsl:choose>
						<xsl:when test ="$FINALCREDIT_PROT_DEVICE_NEW = 0.00">
							<xsl:call-template name ="DWELLING_UNDER_CONSTRUCTION"/>
						</xsl:when>	
						<xsl:otherwise>
							<xsl:choose>
								<xsl:when test ="$FINALCREDIT_PROT_DEVICE_NEW &gt; 0.50">
									<xsl:value-of select="($FINALCREDIT_PROT_DEVICE_NEW - 0.50) + $DUC"/>
								</xsl:when>
								<xsl:when test ="$FINALCREDIT_PROT_DEVICE_NEW &lt;= 0.50">
									<xsl:value-of select="$DUC"/>
								</xsl:when>
								<xsl:otherwise>1.00</xsl:otherwise>
							</xsl:choose>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:otherwise>
			</xsl:choose>
			<!--including prot device, the credit becomes > 50% --> 
</xsl:template>
<xsl:template name = "GET_EXPERIENCE_DISCOUNT_TEMP">
			
			<xsl:variable name="FINALCREDIT_DUC">
				<xsl:variable name="VAR1">
					<xsl:call-template name ="AGEOFHOME_FACTOR"/>
				</xsl:variable>			
				<xsl:variable name="VAR2">
					<xsl:call-template name ="GET_PROTECTIVE_DEVICE_DISCOUNT_1"/>
				</xsl:variable>			
				<xsl:variable name="VAR3">
					<xsl:call-template name ="GET_DUC_DISCOUNT_TEMP"/>
				</xsl:variable>			
				
				<xsl:value-of select ="(1.00-($VAR1)) + (1.00-($VAR2))	+ (1.00-($VAR3))"/>
			</xsl:variable>
			
		<xsl:choose>
				<xsl:when test = "$FINALCREDIT_DUC &gt; 0.50">
						<xsl:value-of select="$FINALCREDIT_DUC - 0.50"/>
				</xsl:when>		
				<xsl:otherwise>	
						<xsl:variable name="EXPERIENCE">
							<xsl:call-template name ="EXPERIENCEFACTOR"/>
						</xsl:variable>		
						<xsl:variable name="FINALCREDIT_EXPERIENCE">
							<xsl:value-of select="$FINALCREDIT_DUC	+ (1.00-($EXPERIENCE))"/>
						</xsl:variable>
						<xsl:choose>
							<xsl:when test ="$FINALCREDIT_EXPERIENCE = 0.00">
								<xsl:call-template name ="EXPERIENCEFACTOR"/>
							</xsl:when>		
							<xsl:otherwise>
								<xsl:choose>
									<xsl:when test ="$FINALCREDIT_EXPERIENCE &gt; 0.50">
										<xsl:value-of select="($FINALCREDIT_EXPERIENCE - 0.50)  + $EXPERIENCE"/>
									</xsl:when>
									<xsl:when test ="$FINALCREDIT_EXPERIENCE &lt;= 0.50"> 
										<xsl:value-of select="$EXPERIENCE"/>
									</xsl:when>
									<xsl:otherwise>1.00</xsl:otherwise>
								</xsl:choose>
							</xsl:otherwise>	
						</xsl:choose>
					</xsl:otherwise>
			</xsl:choose>	
					<!--including prot device, the credit becomes > 50% -->
</xsl:template>


<xsl:template name = "GET_MULTIPOLICY_DISCOUNT_TEMP">
			
			<xsl:variable name="FINALCREDIT_EXPERIENCE">
				<xsl:variable name="VAR1">
					<xsl:call-template name ="AGEOFHOME_FACTOR"/>
				</xsl:variable>			
				<xsl:variable name="VAR2">
					<xsl:call-template name ="GET_PROTECTIVE_DEVICE_DISCOUNT_1"/>
				</xsl:variable>			
				<xsl:variable name="VAR3">
					<xsl:call-template name ="GET_DUC_DISCOUNT_TEMP"/>
				</xsl:variable>	
				<xsl:variable name="VAR4">
					<xsl:call-template name ="GET_EXPERIENCE_DISCOUNT_TEMP"/>
				</xsl:variable>				
				
				<xsl:value-of select ="(1.00-($VAR1)) + (1.00-($VAR2))	+ (1.00-($VAR3)) + (1.00-($VAR4))"/>
			</xsl:variable>
			
			<!--<xsl:variable name="FINALCREDIT_EXPERIENCE">
					
				(1.00-(<xsl:call-template name ="AGEOFHOME_FACTOR"/>))
				+	(1.00-(<xsl:call-template name ="GET_PROTECTIVE_DEVICE_DISCOUNT_1"/>))
				+	(1.00-(<xsl:call-template name ="GET_DUC_DISCOUNT_TEMP"/>))
				+	(1.00-(<xsl:call-template name ="GET_EXPERIENCE_DISCOUNT_TEMP"/>))
			</xsl:variable>-->

			<xsl:choose>
				<xsl:when test="$FINALCREDIT_EXPERIENCE &gt; 0.50">
						<xsl:value-of select="$FINALCREDIT_EXPERIENCE - 0.50"/>
				</xsl:when>
				<xsl:otherwise>
				
					<xsl:variable name="MULTIPOLICY">
						<xsl:call-template name ="MULTIPOLICY_FACTOR"/>
					</xsl:variable>
					<xsl:variable name="FINALCREDIT_MULTIPOLICY">
						<xsl:value-of select="($FINALCREDIT_EXPERIENCE + 1.00) - $MULTIPOLICY"/>
					</xsl:variable>
				
					<xsl:choose>
						<xsl:when test ="$FINALCREDIT_MULTIPOLICY &gt; 0.50">
							<xsl:value-of select="($FINALCREDIT_MULTIPOLICY - 0.50 ) + $MULTIPOLICY"/>
						</xsl:when>
						<xsl:when test ="FINALCREDIT_MULTIPOLICY &lt; 0.50">
							<xsl:value-of select="$MULTIPOLICY"/>
						</xsl:when>
						<xsl:otherwise>1.00</xsl:otherwise>
					</xsl:choose>
				</xsl:otherwise>				
			</xsl:choose>
</xsl:template>


<!-- HO-64 Renters Deluxe Endorsement -->
<xsl:template name = "HO64_Renters_Deluxe_Endorsement">  

		<xsl:choose>
			<xsl:when test ="PRODUCTNAME = 'HO-4' and  PRODUCT_PREMIER = 'Deluxe'" >
				Included
			</xsl:when>
			<xsl:otherwise>
				0
			</xsl:otherwise> 
		</xsl:choose>
		
</xsl:template>

<!--  HO-66 Condominium Deluxe Endorsement -->

<xsl:template name = "HO66_Condominium_Deluxe_Endorsement">  

		<xsl:choose>
			<xsl:when test ="PRODUCTNAME = 'HO-6' and  PRODUCT_PREMIER = 'Deluxe'" >
				Included
			</xsl:when>
			<xsl:otherwise>
				0
			</xsl:otherwise> 
		</xsl:choose>
		
</xsl:template>


<xsl:template name = "COMBINE_COVERAGEA">  
<xsl:choose>
			<xsl:when test ="PRODUCTNAME = 'HO-6'" >    <!-- for HO-6 and HO-6 deluxe -->
			
			<xsl:variable name="VAR1"> 
					<xsl:value-of select="PERSONALPROPERTYINCREASEDLIMITINCLUDE"/>
			</xsl:variable>
			
			<xsl:variable name="VAR2"> 
					<xsl:value-of select="PERSONALPROPERTYINCREASEDLIMITADDITIONAL"/>
			</xsl:variable>
			
			<xsl:value-of select="$VAR1+$VAR2"/>
 				   
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="DWELLING_LIMITS"/>
			</xsl:otherwise> 
		</xsl:choose>
		
		
</xsl:template>

  
<xsl:template name = "HO32_UNIT_OWNER">  
<xsl:choose>
			<xsl:when test ="PRODUCTNAME = 'HO-6' and PRODUCT_PREMIER = ''">     <!-- for HO-6 only -->
			
				<xsl:value-of select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID = 'OPTIONALSECTIONCOVG']/NODE[@ID='HO_32_UNIT_OWNER']/ATTRIBUTES[@ID = '1']/@CHARGE"></xsl:value-of> 
			<!-- 
			    <xsl:value-of select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID = 'OPTIONALSECTIONCOVG']/NODE[@ID='HO_33_CONDO_UNIT']/ATTRIBUTES[@OPTIONS = '1']/@FACTOR"></xsl:value-of> 
			   -->
			</xsl:when>
			<xsl:otherwise>
				0
			</xsl:otherwise> 
		</xsl:choose>
		
		
</xsl:template>

<!-- HO-50 PERSONAL PROPERTY AWAY FROM PREMISES  -->
   
   <xsl:template name="HO50_PERSONAL_PROPERTY_AWAY_PREMISES">
   
	  <xsl:variable name="PNAME" select="PRODUCTNAME"></xsl:variable>  
	<xsl:variable name ="PCOVERAGE_C_ADDITIONAL_VALUE" select ="PERSONALPROPERTYAWAYADDITIONAL"></xsl:variable>
	<xsl:variable name ="PCOVERAGE_C_ADDITIONAL_COST" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = 'HO-50']/@COST"></xsl:variable>
	<xsl:variable name ="PCOVERAGE_C_ADDITIONAL_INCREASEDAMOUNT" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = 'HO-50']/@INCREASEDAMOUNT"></xsl:variable>
	
	<xsl:variable name="VAR_CALCULATION"> 
	<xsl:choose>
		
		<xsl:when test ="$PCOVERAGE_C_ADDITIONAL_VALUE = 0">0</xsl:when>
		<xsl:otherwise>
				<xsl:variable name="VAR1"> 
					<xsl:value-of select ="$PCOVERAGE_C_ADDITIONAL_COST"></xsl:value-of>
				</xsl:variable>
				<xsl:variable name="VAR2"> 
					<xsl:value-of select ="$PCOVERAGE_C_ADDITIONAL_VALUE"></xsl:value-of>
				</xsl:variable>
				<xsl:variable name="VAR3"> 
					<xsl:value-of select ="$PCOVERAGE_C_ADDITIONAL_INCREASEDAMOUNT"></xsl:value-of>
				</xsl:variable>
				
				<xsl:value-of select="$VAR1*$VAR2 div $VAR3"/>
			
		</xsl:otherwise>
	</xsl:choose>
	</xsl:variable>
	<xsl:value-of select="$VAR_CALCULATION"/> 
	
	
</xsl:template>


<!-- HO-51 BULDING ALTERATION -->
<xsl:template name="HO51_BULDING_ALTER">
   
	  <xsl:variable name="PNAME" select="PRODUCTNAME"></xsl:variable> 
	<xsl:variable name ="PCOVERAGE_C_ADDITIONAL_VALUE" select ="PERSONALPROPERTYINCREASEDLIMITADDITIONAL"></xsl:variable>
	<xsl:variable name ="PCOVERAGE_C_ADDITIONAL_COST" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = 'HO-51']/@COST"></xsl:variable>
	<xsl:variable name ="PCOVERAGE_C_ADDITIONAL_INCREASEDAMOUNT" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = 'HO-51']/@INCREASEDAMOUNT"></xsl:variable>
	
	<xsl:variable name="VAR_CALCULATION"> 
	<xsl:choose>
		
		<xsl:when test ="$PCOVERAGE_C_ADDITIONAL_VALUE = 0">0</xsl:when>
		<xsl:otherwise>
				<xsl:variable name="VAR1"> 
					<xsl:value-of select ="$PCOVERAGE_C_ADDITIONAL_COST"></xsl:value-of>
				</xsl:variable>
				<xsl:variable name="VAR2"> 
					<xsl:value-of select ="$PCOVERAGE_C_ADDITIONAL_VALUE"></xsl:value-of>
				</xsl:variable>
				<xsl:variable name="VAR3"> 
					<xsl:value-of select ="$PCOVERAGE_C_ADDITIONAL_INCREASEDAMOUNT"></xsl:value-of>
				</xsl:variable>
				
			<xsl:value-of select="$VAR1*$VAR2 div $VAR3"/> 
			
			
		</xsl:otherwise>
	</xsl:choose>
	</xsl:variable>
	<xsl:value-of select="$VAR_CALCULATION"/> 
	
 <xsl:template name="MEDICAL_CHARGES">

<xsl:variable name ="VAR1" select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID = 'PRIMARY_RESIDENCE']/@FOR_EACH_ADDITION"/>

<xsl:choose>
		<xsl:when test="MEDICALPAYMENTSTOOTHERS_LIMIT > 0">
			<xsl:value-of select="((MEDICALPAYMENTSTOOTHERS_LIMIT - 1000) div 1000) * $VAR1"></xsl:value-of>	
		</xsl:when>
		<xsl:otherwise>
			0
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="LIABLITY_CHARGES">
 
<xsl:variable name="VAR1"> 
		<xsl:choose>
				<xsl:when test="PERSONALLIABILITY_LIMIT = 300000.00">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID = 'DESCRIBED_PRIMARY_RESIDENCE']/@LIABILITY_LIMIT_G2"/> 
				</xsl:when>
				<xsl:when test="PERSONALLIABILITY_LIMIT = 500000.00">
						  <xsl:value-of select ="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='COVERAGE_E_AND_F']/ATTRIBUTES[@ID = 'DESCRIBED_PRIMARY_RESIDENCE']/@LIABILITY_LIMIT_G3"/> 
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
</xsl:variable>	

<xsl:choose>
		<xsl:when test="$VAR1 !='' and $VAR1 != ' '">
			 <xsl:value-of select ="$VAR1"/>
		</xsl:when>
		
		<xsl:otherwise>
			0
		</xsl:otherwise>
	</xsl:choose>
 
</xsl:template>


</xsl:stylesheet>
  

