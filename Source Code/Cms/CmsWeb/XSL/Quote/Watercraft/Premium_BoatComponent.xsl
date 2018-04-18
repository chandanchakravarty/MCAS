<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xinclude="http://www.w3.org/1999/XML/xinclude"  version="1.0">
<xsl:output encoding="ISO8859-1" omit-xml-declaration="no" method="xml"/>
<xsl:variable name="HOCoveragesDoc" select="document('FactorPath')"></xsl:variable>
<xsl:template match="/">
	<xsl:apply-templates select="INPUTXML/QUICKQUOTE"/>
</xsl:template>

<xsl:template match="INPUTXML/QUICKQUOTE">
<PREMIUM>
	<CREATIONDATE></CREATIONDATE>
	<GETPATH>
		<PRODUCT PRODUCTID="0" >
			<!--Group for Boat-->
			<GROUP GROUPID="0">				
				<GROUPCONDITION>
					<CONDITION>
					</CONDITION>
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
										<xsl:call-template name="PHYSICAL_DAMAGE"></xsl:call-template>
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
								<xsl:call-template name="WATERCRAFT_LIABILITY_DISP1"/>
							</PATH>
						</STEP>								 						
						</IF>
					</SUBGROUP>
					
					<SUBGROUP>
						<IF>
						<STEP STEPID="4">
							<PATH>
							{
								<xsl:call-template name="WATERCRAFT_LIABILITY_DISP2"/>
							}
							</PATH>
						</STEP>								 						
						</IF>
					</SUBGROUP>
					
					
					
				<!-- Medical Payments to others -->		
					<SUBGROUP>
						<STEP STEPID="5">
								<PATH>
										<xsl:call-template name="MEDICAL_PAYMENTS_DISP1"/>
								</PATH>
							</STEP>								 						
					</SUBGROUP>
					
					<SUBGROUP>
						<IF>
							<STEP STEPID="6">
								<PATH>
								{
									<xsl:call-template name="MEDICAL_PAYMENTS_DISP2"/>
								}	
								</PATH>
							</STEP>								 						
						</IF>
					</SUBGROUP>
				<!-- Uninsured Boaters -->		
					<SUBGROUP>
						<STEP STEPID="7">
								<PATH>
									<xsl:call-template name="UNINSURED_BOATERS_COVERAGE"></xsl:call-template>
								</PATH>
							</STEP>								 						
					</SUBGROUP>					
				
				<!-- Boat Towing and Emergency Services -->		
					<SUBGROUP>
						<STEP STEPID="8">
							<PATH>Included</PATH>
						</STEP>								 						
					</SUBGROUP>						
					
				<!-- Boat Replacement Cost Coverage -->		
					<SUBGROUP>
						<STEP STEPID="9">
								<PATH>
									<xsl:call-template name="BOAT_REPLACEMENT_DISPLAY"/>
								</PATH>
							</STEP>								 						
					</SUBGROUP>							
					
				<!-- Client Entertainment, Form OP-720  -->		
					<SUBGROUP>
						<IF>
						<STEP STEPID="10">
								<PATH>
								{
									<xsl:call-template name="CLIENT_ENTERTAINMENT"/>
								}
								</PATH>
							</STEP>								 						
						</IF>	
					</SUBGROUP>						
					
				<!-- Watercraft Liability Pollution Coverage, Form OP-900  -->		
					<SUBGROUP>
						<IF>
						<STEP STEPID="11">
								<PATH>
									{
										<xsl:call-template name="LIABILITY_POLLUTION_COVERAGE"/>
									}	
								</PATH>
							</STEP>								 						
						</IF>	
					</SUBGROUP>							
					
				<!-- Agreed Value Endorsement, Form AV-100  -->		
					<SUBGROUP>
						<IF>				
						<STEP STEPID="12">
							<PATH>
									{
										<xsl:call-template name="AGREED_VALUE_ENDORSEMENT"/>
									}
									<!--<xsl:value-of select="BOATS/BOAT/AV100"/>-->
								</PATH>
							</STEP>								 						
						</IF>
					</SUBGROUP>								
					
				<!-- Discount - Insurance Score Credit (Score 700)  -->		
					<SUBGROUP>
						<STEP STEPID="13">
								<PATH>
								<xsl:call-template name="INSURANCE_SCORE"></xsl:call-template>
								</PATH>
							</STEP>								 						
					</SUBGROUP>									
					
				<!-- Discount Wolverine Homeowners  -->		
					<SUBGROUP>
						<STEP STEPID="14">
								<PATH>
									<xsl:call-template name="DISCOUNT_WOLVERINE_PREMIUM"></xsl:call-template>
								</PATH>
							</STEP>								 						
					</SUBGROUP>										
					
				<!-- Discount - 5 Years Experience or Navigation Course   -->		
					<SUBGROUP>
						<STEP STEPID="15">
								<PATH>
									<xsl:call-template name="EXPERIENCE_CREDIT_PREMIUM"></xsl:call-template>
								</PATH>
							</STEP>								 						
					</SUBGROUP>										
				
				<!-- Discount - Diesel Engine   -->		
					<SUBGROUP>
						<STEP STEPID="16">
								<PATH>
									<xsl:call-template name="DIESEL_ENGINE_PREMIUM"></xsl:call-template>
								</PATH>
							</STEP>								 						
					</SUBGROUP>											
	
				<!-- Discount - Halon Fire Extinguishing System   -->		
					<SUBGROUP>
						<STEP STEPID="17">
								<PATH>
									<xsl:call-template name="HALON_FIRE_PREMIUM"></xsl:call-template>
								</PATH>
							</STEP>								 						
					</SUBGROUP>											
					
				<!-- Discount - Loran, GPS or other navigation System  -->			
					<SUBGROUP>
						<STEP STEPID="18">
								<PATH>
									<xsl:call-template name="LORAN_GPS_PREMIUM"></xsl:call-template>
								</PATH>
							</STEP>
					</SUBGROUP>
					
				<!-- Discount - Shore Station -->
					<SUBGROUP>
						<STEP STEPID="19">
								<PATH>
									<xsl:call-template name="SHORE_STATION_PREMIUM"></xsl:call-template>
								</PATH>
							</STEP>
					</SUBGROUP>
				
				<!-- Discount - Multi-Boat -->			
					<SUBGROUP>
						<STEP STEPID="20">
								<PATH>
									<xsl:call-template name="MULTI_BOAT_PREMIUM"></xsl:call-template>
								</PATH>
							</STEP>								 						
					</SUBGROUP>													
					
					<!-- Discount - Wooden Boat -->			
					<SUBGROUP>
						<STEP STEPID="21">
								<PATH>
									<xsl:call-template name="WOODEN_BOAT_PREMIUM"></xsl:call-template>
								</PATH>
							</STEP>								 						
					</SUBGROUP>													
					
					<!-- Charge - Dual Ownership -->			
					<SUBGROUP>
						<STEP STEPID="22">
							<PATH>
								<xsl:call-template name="DUAL_OWNERSHIP_PREMIUM"></xsl:call-template>
							</PATH>
						</STEP>
					</SUBGROUP>
					
					
					<!-- Charge - Waverunner 10 %-->
					<SUBGROUP>
						<STEP STEPID="23">
								<PATH>
									<xsl:call-template name="WAVERUNNER_PREMIUM"></xsl:call-template>
								</PATH>
							</STEP>
					</SUBGROUP>
					
					
					<!-- Charge - Length over 26 Feet-->
					<SUBGROUP>
						<STEP STEPID="24">
								<PATH>
									<xsl:call-template name="LENGTH_26FEET_PREMIUM"></xsl:call-template>
								</PATH>
							</STEP>
					</SUBGROUP>
					
					<!-- Charge - Length over 26 Feet and speed 55-65 miles per hour-->
						<SUBGROUP>
						<STEP STEPID="25">
								<PATH>
									<xsl:call-template name="LENGTH_26FEET_SPEED65_PREMIUM"></xsl:call-template>
								</PATH>
							</STEP>
					</SUBGROUP>
					
					<!-- Charge - FibreGlass Boats over 15 year old-->
					<SUBGROUP>
						<STEP STEPID="26">
								<PATH>
									<xsl:call-template name="FIBRE_GLASS_PREMIUM"></xsl:call-template>
								</PATH>
							</STEP>
					</SUBGROUP>
					
					<!-- Charge - Remove Sailboat Racing Exclusion-->
					<SUBGROUP>
						<STEP STEPID="27">
								<PATH>
									<xsl:call-template name="REMOVE_SAILBOAT_PREMIUM"></xsl:call-template>
								</PATH>
							</STEP>
					</SUBGROUP>
					
					
					<!-- Total Inboard Premium -->
					<SUBGROUP>
						<IF>
							<STEP STEPID="28">
								<PATH>
									{
									<xsl:call-template name="FINAL_PREMIUM"></xsl:call-template>
									}
								</PATH>
							</STEP>								 						
						</IF>
					</SUBGROUP>
					
					<!-- Unattached Equipment -->
					<SUBGROUP>
					<IF>
						<STEP STEPID="29" > 
							<PATH>
							{
							<xsl:call-template name="UNATTACHED_PREMIUM"></xsl:call-template>
							}
							</PATH>		
						</STEP> 
					</IF>	
					</SUBGROUP>	
					
					<!-- Scheduled Miscellaneous Sports Equipment -->
					<SUBGROUP>
						<STEP STEPID="30" > 
							<PATH>ND</PATH>		
						</STEP> 
					</SUBGROUP>
					
						
			</GROUP> 				
		</PRODUCT>
	</GETPATH>
	<CALCULATION>
		<PRODUCT PRODUCTID="0" >
			<!--Product Name -->
			<xsl:attribute name="DESC"><xsl:call-template name="PRODUCTNAME"/></xsl:attribute> 
			<!--Group for Boat-->
			<GROUP GROUPID="0" CALC_ID="10000"> 
				<xsl:attribute name="DESC"><xsl:call-template name="GROUPID0"/></xsl:attribute> 
				<GROUPCONDITION>
					<CONDITION>
					</CONDITION>
				</GROUPCONDITION> 
				<GROUPRULE></GROUPRULE>
				<GROUPFORMULA></GROUPFORMULA>
				<GDESC></GDESC>
				
				<!-- Boat name and year  -->
				<STEP STEPID="0" CALC_ID="1000" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID0"/></xsl:attribute> 
					<PATH>P</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				
				<!-- Boat Description  -->
				<STEP STEPID="1" CALC_ID="1001" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID1"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				
				
				<!-- Physical Damage-->
				<STEP STEPID="2" CALC_ID="1002" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID2"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH><xsl:value-of select="BOATS/BOAT/DEDUCTIBLE"/></D_PATH>
					<L_PATH><xsl:value-of select="BOATS/BOAT/MARKETVALUE"/></L_PATH>
				</STEP> 
				
				<!-- Watercraft Liability-->
				<STEP STEPID="3" CALC_ID="1003" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID3"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH><xsl:value-of select="POLICY/PERSONALLIABILITY"/></L_PATH>
				</STEP> 
				
				<!-- Watercraft Liability-->
				<STEP STEPID="4" CALC_ID="1004" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID4"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH><xsl:value-of select="POLICY/PERSONALLIABILITY"/></L_PATH>
				</STEP> 
				
				<!-- Medical Payments to others-->
				<STEP STEPID="5" CALC_ID="1005" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID5"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH>
						<xsl:choose>
							<xsl:when test="POLICY/MEDICALPAYMENTSOTHER = 1000 or (BOATS/BOAT/BOATTYPE = 11386 or BOATS/BOAT/BOATTYPE = 11387)">
								$1000
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="POLICY/MEDICALPAYMENTSOTHER"></xsl:value-of>		
							</xsl:otherwise>			
						</xsl:choose>
					</L_PATH>
				</STEP> 
				
				<!-- Medical Payments to others-->
				<STEP STEPID="6" CALC_ID="1006" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID6"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH>
						<xsl:choose>
							<xsl:when test="POLICY/MEDICALPAYMENTSOTHER = 1000 or (BOATS/BOAT/BOATTYPE = 11386 or BOATS/BOAT/BOATTYPE = 11387)">
								$1000
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="POLICY/MEDICALPAYMENTSOTHER"></xsl:value-of>		
							</xsl:otherwise>			
						</xsl:choose>
					</L_PATH>
				</STEP> 
				
				<!-- Uninsured Boaters-->
				<STEP STEPID="7" CALC_ID="1007" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID7"/></xsl:attribute> 
					<PATH>Included</PATH>
					<D_PATH></D_PATH>
					<L_PATH>$10,000</L_PATH>
				</STEP> 
				
				<!-- Boat Towing and Emergency services-->
				<STEP STEPID="8" CALC_ID="1008" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID8"/></xsl:attribute> 
					<PATH>Included</PATH>
					<D_PATH></D_PATH>
					<L_PATH><xsl:call-template name="BOAT_TOWING"/></L_PATH>
				</STEP> 
				
				<!-- Boat Replacement Cost Coverage-->
				<STEP STEPID="9" CALC_ID="1009" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID9"/></xsl:attribute> 
					<PATH></PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				
				<!-- Client Entertainment, Form OP-720  -->		
				<STEP STEPID="10" CALC_ID="1010" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID10"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				
				<!-- Watercraft Liability Pollution Coverage, Form OP-900 -->		
				<STEP STEPID="11" CALC_ID="1011" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID11"/></xsl:attribute> 
					<PATH></PATH>
					<D_PATH></D_PATH>
					<L_PATH>$10000</L_PATH>
				</STEP> 			
				
				<!-- Agreed Value Endorsement, Form AV-100 -->		
				<STEP STEPID="12" CALC_ID="1012" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID12"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 			
				
				<!-- Discount - Insurance Score Credit (Score 700)  -->		
				<STEP STEPID="13" CALC_ID="1013" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID13"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 			
				
				<!-- Discount Wolverine Homeowners  -->		
				<STEP STEPID="14" CALC_ID="1014" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID14"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 			
				
				<!-- Discount - 5 Years Experience or Navigation Course   -->		
				<STEP STEPID="15" CALC_ID="1015" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID15"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 			
				
				<!-- Discount - Diesel Engine   -->		
				<STEP STEPID="16" CALC_ID="1016" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID16"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 		
				
				<!-- Discount - Halon Extinguishing Fire System  -->		
				<STEP STEPID="17" CALC_ID="1017" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID17"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 			
				
				<!-- Discount - Loran, GPS or other navigation System  -->		
				<STEP STEPID="18" CALC_ID="1018" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID18"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 			
				
				<!-- Discount - Shore Station -->			
				<STEP STEPID="19" CALC_ID="1019" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID19"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 			
	
				<!-- Discount - Multi-Boat -->			
				<STEP STEPID="20" CALC_ID="1020" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID20"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 			


				<!-- Discount - Wooden Boat -->			
				<STEP STEPID="21" CALC_ID="1021" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID21"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 			


				<!-- Charge - Dual Ownership -->			
				<STEP STEPID="22" CALC_ID="1022" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID22"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 	
				
				
				<!-- Charge - Waverunner 10% -->			
				<STEP STEPID="23" CALC_ID="1023" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID23"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 	
				
				<!-- Charge - Length over 26 Feet -->			
				<STEP STEPID="24" CALC_ID="1024" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID24"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 	
				
				<!-- Charge - Length over 26 Feet and Speed over 65  -->			
				<STEP STEPID="25" CALC_ID="1025" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID25"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 	
						
				<!-- Charge - Fibre Glass over 15 years old   -->			
				<STEP STEPID="26" CALC_ID="1026" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID26"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 	
				
				<!-- Charge - Remove Sailboat Racing Exclusion   -->			
				<STEP STEPID="27" CALC_ID="1027" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID27"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 	
				
				<!-- Total Inboard Premium -->			
				<STEP STEPID="28" CALC_ID="1028" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID28"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 			
				
				<!-- Unattached Equipment -->			
				<STEP STEPID="29" CALC_ID="1029" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID29"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH>$100</D_PATH>
					<L_PATH><xsl:value-of select="POLICY/UNATTACHEDEQUIPMENT"/></L_PATH>
				</STEP> 			
				
				<!-- Scheduled Miscellaneous Sports Equipment -->
				<STEP STEPID="30" CALC_ID="1030" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID30"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>
				
									
			</GROUP>			
		</PRODUCT>
	</CALCULATION>
</PREMIUM>
  </xsl:template>
<!--  Template for Lables   -->
<!--  Group Details  -->       
<xsl:template name ="PRODUCTNAME">Watercraft</xsl:template>
<xsl:template name ="GROUPID0">Boats</xsl:template>

<xsl:template name = "STEPID0"><xsl:call-template name="BOAT_NAME_YEAR"/></xsl:template>
<xsl:template name = "STEPID1"><xsl:call-template name="BOAT_DESCRIPTION"/></xsl:template>


<xsl:template name = "STEPID2">Physical Damage</xsl:template>
<xsl:template name = "STEPID3"><xsl:call-template name="WATERCRAFT_LIABILITY_TEXT"/></xsl:template>
<xsl:template name = "STEPID4"><xsl:call-template name="WATERCRAFT_LIABILITY_TEXT"/></xsl:template>
<xsl:template name = "STEPID5"><xsl:call-template name="MEDICAL_PAYMENTS_TEXT"/></xsl:template>
<xsl:template name = "STEPID6"><xsl:call-template name="MEDICAL_PAYMENTS_TEXT"/></xsl:template>
<xsl:template name = "STEPID7">Uninsured Boaters</xsl:template>
<xsl:template name = "STEPID8">Boat Towing and Emergency services</xsl:template>
<xsl:template name = "STEPID9">Boat Replacement Cost Coverage</xsl:template>
<xsl:template name = "STEPID10">Client Entertainment, Form OP-720</xsl:template>
<xsl:template name = "STEPID11">Watercraft Liability Pollution Coverage, Form OP-900</xsl:template>
<xsl:template name = "STEPID12">Agreed Value Endorsement, Form AV-100</xsl:template>
<xsl:template name = "STEPID13"><xsl:call-template name="INSURANCE_SCORE_CREDIT_DISPLAY"/></xsl:template>
<xsl:template name = "STEPID14"><xsl:call-template name="DISCOUNT_WOLVERINE_DISPLAY"></xsl:call-template></xsl:template>
<xsl:template name = "STEPID15"><xsl:call-template name="EXPERIENCE_CREDIT_DISPLAY"/></xsl:template>
<xsl:template name = "STEPID16"><xsl:call-template name="DIESEL_ENGINE_DISPLAY"/></xsl:template>
<xsl:template name = "STEPID17"><xsl:call-template name="HALON_FIRE_DISPLAY"/></xsl:template>
<xsl:template name = "STEPID18"><xsl:call-template name="LORAN_GPS_DISPLAY"/></xsl:template>
<xsl:template name = "STEPID19"><xsl:call-template name="SHORE_STATION_DISPLAY"/></xsl:template>
<xsl:template name = "STEPID20"><xsl:call-template name="MULTI_BOAT_DISPLAY"/></xsl:template>	
<xsl:template name = "STEPID21"><xsl:call-template name="WOODEN_BOAT_DISPLAY"/></xsl:template>
<xsl:template name = "STEPID22"><xsl:call-template name="DUAL_OWNERSHIP_DISPLAY"/></xsl:template>
<xsl:template name = "STEPID23"><xsl:call-template name="WAVERUNNER_DISPLAY"/></xsl:template>
<xsl:template name = "STEPID24"><xsl:call-template name="LENGTH_26FEET_DISPLAY"/></xsl:template>
<xsl:template name = "STEPID25"><xsl:call-template name="LENGTH_26FEET_SPEED65_DISPLAY"/></xsl:template>
<xsl:template name = "STEPID26"><xsl:call-template name="FIBRE_GLASS_DISPLAY"/></xsl:template>
<xsl:template name = "STEPID27"><xsl:call-template name="REMOVE_SAILBOAT_DISPLAY"/></xsl:template>
<xsl:template name = "STEPID28"><xsl:call-template name="TOTAL_PREMIUM_DISPLAY"/></xsl:template>

<xsl:template name = "STEPID29">
	<xsl:variable name ="VARBOATCOUNT">
		<xsl:call-template name="BOATCOUNTFUNCTION"></xsl:call-template>  
	</xsl:variable>
	<xsl:choose>
		<xsl:when test ="$VARBOATCOUNT  = 1">Unattached Equipment and Personal Effects(unscheduled)</xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name = "STEPID30"> 
	<xsl:variable name ="VARBOATCOUNT">
		<xsl:call-template name="BOATCOUNTFUNCTION"></xsl:call-template>  
	</xsl:variable>
	
	<xsl:choose>
		<xsl:when test ="$VARBOATCOUNT  = 1">Scheduled Miscellaneous Sports Equipment</xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!--Function to count the number of boats-->
<xsl:template name = "BOATCOUNTFUNCTION">
	<xsl:variable name ="VARBOATCOUNT">
		<xsl:value-of select = "BOATCOUNT"/>
	</xsl:variable>
	<xsl:choose>
		<xsl:when test ="BOATS/BOAT/@id  = $VARBOATCOUNT">1</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>  


<!-- ============================================================================================ -->
<!--						Templates for Boat name and year, Display (START)					  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="BOAT_NAME_YEAR">
<xsl:value-of select="BOATS/BOAT/YEAR"/>, <xsl:value-of select="BOATS/BOAT/MANUFACTURER"/> <xsl:value-of select="BOATS/BOAT/MODEL"/>, Serial: <xsl:value-of select="BOATS/BOAT/SERIALNUMBER"/>  
</xsl:template> 
<!-- ============================================================================================ -->
<!--						Templates for Boat name and year, Display (START)					  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--						Templates for Boat Description, Display (START)						  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="BOAT_DESCRIPTION">
 - <xsl:value-of select="BOATS/BOAT/LENGTH"/> Feet, <xsl:value-of select="BOATS/BOAT/WEIGHT"/> lbs, <xsl:value-of select="BOATS/BOAT/HORSEPOWER"/>HP<xsl:value-of select="BOATS/BOAT/CAPABLESPEED"/> MPH, <xsl:value-of select="BOATS/BOAT/WATERS"></xsl:value-of>  
</xsl:template> 

<!-- ============================================================================================ -->
<!--						Templates for Boat Description, Display (START)						  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--						Templates for Watercraft Liability Text , Display (START)			  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="WATERCRAFT_LIABILITY_TEXT">
	<xsl:choose>
		<xsl:when test="POLICY/ATTACHTOWOLVERINE = 'Y' and POLICY/BOATHOMEDISC = 'Y'">Watercraft Liability - extended from Homeowners</xsl:when>
		<xsl:otherwise>Watercraft Liability</xsl:otherwise>		
	</xsl:choose>
</xsl:template> 

<!-- ============================================================================================ -->
<!--						Templates for Medical Payment Text , Display (START)				  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="MEDICAL_PAYMENTS_TEXT">
	<xsl:choose>
		<xsl:when test="POLICY/ATTACHTOWOLVERINE = 'Y' and POLICY/BOATHOMEDISC = 'Y'">Medical Payments To Others - extended from Homeowners</xsl:when>
		<xsl:otherwise>Medical Payments To Others</xsl:otherwise>		
	</xsl:choose>
</xsl:template> 



<!-- ============================================================================================ -->
<!--						Templates for Insurance Score Credit, Display (START)				  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name ="INSURANCESCORE">
	<xsl:variable name="INS" select="POLICY/INSURANCESCORE"></xsl:variable>
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
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='INSURANCE']/ATTRIBUTES[@MINSCORE = 751]/@FACTOR"/>
		</xsl:when>		
		<xsl:otherwise>						
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='INSURANCE']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@FACTOR"/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name ="INSURANCESCORE_DISPLAY">
	<xsl:variable name="INS" select="POLICY/INSURANCESCORE"></xsl:variable>
	<xsl:variable name="INSDISCOUNT">
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
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='INSURANCE']/ATTRIBUTES[@MINSCORE = 751]/@FACTOR"/>
			</xsl:when>		
			<xsl:otherwise>						
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='INSURANCE']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@FACTOR"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:variable>
	<xsl:choose>
		<xsl:when test = "$INSDISCOUNT = 0 or $INSDISCOUNT='' or $INSDISCOUNT=1">
			0
		</xsl:when>	
		<xsl:when test = "$INSDISCOUNT &gt; 0 and $INSDISCOUNT &lt; 1">
			Included
		</xsl:when>		 
		<xsl:otherwise>	
			0		 
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name ="INSURANCESCORE_CREDIT">
	<xsl:variable name="INS" select="POLICY/INSURANCESCORE"></xsl:variable>
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
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='INSURANCE']/ATTRIBUTES[@MINSCORE = 751]/@CREDIT"/>
		</xsl:when>		
		<xsl:otherwise>						
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='INSURANCE']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@CREDIT"/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- OLD INSURANCE SCORE -->
<xsl:template name="INSURANCE_SCORE_CREDIT_DISPLAY">
	<xsl:variable name="SCORE" select="POLICY/INSURANCESCORE"></xsl:variable>
	<xsl:choose>
		<xsl:when test="$SCORE &lt;= 599 or $SCORE = 'No Hit' or $SCORE = 'No Score' or $SCORE = 0">
			0
		</xsl:when>
		<xsl:otherwise>Discount - Insurance Score Credit (Score <xsl:value-of select ="POLICY/INSURANCESCORE"/> )-<xsl:call-template name="INSURANCESCORE_CREDIT"></xsl:call-template></xsl:otherwise>
	</xsl:choose>
</xsl:template> 

<xsl:template name="INSURANCE_SCORE_CREDIT">
	<xsl:variable name="SCORE" select="POLICY/INSURANCESCORE"></xsl:variable>
	<xsl:choose>		
		<xsl:when test = "$SCORE &gt; 599">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='CREDIT']/ATTRIBUTES[@MINSCORE &lt;= $SCORE and @MAXSCORE &gt;= $SCORE]/@CREDIT"/>
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="INSURANCE_SCORE">
	<xsl:variable name="SCORE" select="POLICY/INSURANCESCORE"></xsl:variable>
	<xsl:choose>
		<xsl:when test="$SCORE &lt;= 599 or $SCORE = 'No Hit' or $SCORE = 'No Score' or $SCORE = 0">
			0
		</xsl:when>
		<xsl:otherwise>Included</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--						Templates for Insurance Score Credit, Display (END)					  -->
<!--		    					  FOR INDIANA												  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--					Templates for 5 Years Experience or Navigation Course , Display (START)	  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
<xsl:template name="EXPERIENCE_CREDIT">
	<xsl:choose>
		<xsl:when test = "OPERATORS/OPERATOR/POWERSQUADRONCOURSE = 'Y' or OPERATORS/OPERATOR/COASTGUARDAUXILARYCOURSE = 'Y' or OPERATORS/OPERATOR/HAS_5_YEARSOPERATOREXPERIENCE = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='EXPNAVIGATIONCOURSE']/ATTRIBUTES/@CREDIT"/>
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<xsl:template name="EXPERIENCE_CREDIT_DISPLAY">
	<xsl:choose>
		<xsl:when test = "OPERATORS/OPERATOR/POWERSQUADRONCOURSE = 'Y' or OPERATORS/OPERATOR/COASTGUARDAUXILARYCOURSE = 'Y' or OPERATORS/OPERATOR/HAS_5_YEARSOPERATOREXPERIENCE = 'Y'">Discount - 5 Years Experience or Navigation Course -(<xsl:call-template name="EXPERIENCE_CREDIT"></xsl:call-template>%)</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="EXPERIENCE_CREDIT_PREMIUM">
	<xsl:choose>
		<xsl:when test ="OPERATORS/OPERATOR/POWERSQUADRONCOURSE = 'Y' or OPERATORS/OPERATOR/COASTGUARDAUXILARYCOURSE = 'Y' or OPERATORS/OPERATOR/HAS_5_YEARSOPERATOREXPERIENCE = 'Y'">Included</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="EXPERIENCE_CREDIT_FACTOR">
	<xsl:choose>
		<xsl:when test = "OPERATORS/OPERATOR/POWERSQUADRONCOURSE = 'Y' or OPERATORS/OPERATOR/COASTGUARDAUXILARYCOURSE = 'Y' or OPERATORS/OPERATOR/HAS_5_YEARSOPERATOREXPERIENCE = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='EXPNAVIGATIONCOURSE']/ATTRIBUTES/@FACTOR"/>
		</xsl:when>
		<xsl:otherwise>1.00</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--					Templates for 5 Years Experience or Navigation Course , Display (END)	  -->
<!--		    					  FOR INDIANA												  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--					Templates for Diesel Engine , Display (START)							  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
<xsl:template name="DIESEL_ENGINE">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/DIESELENGINE = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='DIESELENGINECRAFT']/ATTRIBUTES/@CREDIT"/>			
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="DIESEL_ENGINE_DISPLAY">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/DIESELENGINE = 'Y'">Discount - Diesel Engine -<xsl:call-template name="DIESEL_ENGINE"></xsl:call-template>%</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="DIESEL_ENGINE_PREMIUM">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/DIESELENGINE = 'Y'">Included</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="DIESEL_ENGINE_FACTOR">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/DIESELENGINE = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='DIESELENGINECRAFT']/ATTRIBUTES/@FACTOR"/>			
		</xsl:when>
		<xsl:otherwise>1.00</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--					Templates for Diesel Engine , Display (END)							      -->
<!--		    					  FOR INDIANA												  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--					Templates for Halon Extinguisher, Display (START)			      		  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
<xsl:template name="HALON_FIRE">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/HALONFIRE = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='FIREEXTINGUISHER']/ATTRIBUTES/@CREDIT"/>		
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="HALON_FIRE_DISPLAY">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/HALONFIRE = 'Y'">Discount - Halon Fire Extinguishing System	-<xsl:call-template name="HALON_FIRE"></xsl:call-template>%</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="HALON_FIRE_PREMIUM">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/HALONFIRE = 'Y'">Included</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="HALON_FIRE_FACTOR">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/HALONFIRE = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='FIREEXTINGUISHER']/ATTRIBUTES/@FACTOR"/>		
		</xsl:when>
		<xsl:otherwise>1.00</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--					Templates for Halon Extinguisher, Display (END)							  -->
<!--		    					  FOR INDIANA												  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--					Templates for Loran GPS or other Navigation System, Display (START)		  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
<xsl:template name="LORAN_GPS">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/LORANNAVIGATIONSYSTEM = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='NAVIGATIONSYSTEM']/ATTRIBUTES/@CREDIT"/>
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="LORAN_GPS_DISPLAY">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/LORANNAVIGATIONSYSTEM = 'Y'">Discount - Loran, GPS or other Navigation System -<xsl:call-template name="LORAN_GPS"></xsl:call-template>%</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="LORAN_GPS_PREMIUM">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/LORANNAVIGATIONSYSTEM = 'Y'">Included</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="LORAN_GPS_FACTOR">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/LORANNAVIGATIONSYSTEM = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='NAVIGATIONSYSTEM']/ATTRIBUTES/@FACTOR"/>
		</xsl:when>
		<xsl:otherwise>1.00</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<!-- ============================================================================================ -->
<!--					Templates for Loran GPS or other Navigation System, Display (END)		  -->
<!--		    					  FOR INDIANA												  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--					Templates for Shore Station, Display (START)							  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
<xsl:template name="SHORE_STATION">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/SHORESTATION = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='SHORESTATIONCREDIT']/ATTRIBUTES/@CREDIT"/>						
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="SHORE_STATION_DISPLAY">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/SHORESTATION = 'Y'">Discount - Shore Station - <xsl:call-template name="SHORE_STATION"></xsl:call-template>%</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="SHORE_STATION_PREMIUM">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/SHORESTATION = 'Y'">Included</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="SHORE_STATION_FACTOR">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/SHORESTATION = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='SHORESTATIONCREDIT']/ATTRIBUTES/@FACTOR"/>				
		</xsl:when>
		<xsl:otherwise>1.00</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--					Templates for Shore Station, Display (END)								  -->
<!--		    					  FOR INDIANA												  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--					Templates for Multi-Boat, Display (START)								  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
<xsl:template name="MULTI_BOAT">
<xsl:variable name ="VARBOATCOUNT">
	<xsl:value-of select = "BOATCOUNT"/>
</xsl:variable>
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/MULTIBOATCREDIT = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='MULTIBOAT']/ATTRIBUTES/@CREDIT"/>								
		</xsl:when>
		<xsl:when test = "$VARBOATCOUNT &gt; 1">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='MULTIBOAT']/ATTRIBUTES/@CREDIT"/>								
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<xsl:template name="MULTI_BOAT_DISPLAY">
<xsl:variable name ="VARBOATCOUNT">
	<xsl:value-of select = "BOATCOUNT"/>
</xsl:variable>
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/MULTIBOATCREDIT = 'Y'">Discount - Multi-Boat -<xsl:call-template name="MULTI_BOAT"></xsl:call-template>%</xsl:when>
		<xsl:when test = "$VARBOATCOUNT &gt; 1">Discount - Multi-Boat -<xsl:call-template name="MULTI_BOAT"></xsl:call-template>%</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<xsl:template name="MULTI_BOAT_PREMIUM">
<xsl:variable name ="VARBOATCOUNT">
	<xsl:value-of select = "BOATCOUNT"/>
</xsl:variable>
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/MULTIBOATCREDIT = 'Y'">Included</xsl:when>
		<xsl:when test = "$VARBOATCOUNT &gt; 1">Included</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="MULTI_BOAT_FACTOR">
<xsl:variable name ="VARBOATCOUNT">
	<xsl:value-of select = "BOATCOUNT"/>
</xsl:variable>
	
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/MULTIBOATCREDIT = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='MULTIBOAT']/ATTRIBUTES/@FACTOR"/>								
		</xsl:when>
		<xsl:when test = "$VARBOATCOUNT &gt; 1">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='MULTIBOAT']/ATTRIBUTES/@FACTOR"/>								
		</xsl:when>
		<xsl:otherwise>1.00</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--					Templates for  Multi-Boat, Display (END)								  -->
<!--		    					  FOR INDIANA												  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--					Templates for WOODEN BOAT, Display (START)								  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
<xsl:template name="WOODEN_BOAT">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/CONSTRUCTION = 'Wood' and BOATS/BOAT/AGE &gt; 10">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='WOODENBOAT']/ATTRIBUTES/@SURCHARGE"/>
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="WOODEN_BOAT_DISPLAY">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/CONSTRUCTION = 'Wood' and BOATS/BOAT/AGE &gt; 10">
		Charge - Wooden Boat +<xsl:call-template name="WOODEN_BOAT"></xsl:call-template>%</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="WOODEN_BOAT_PREMIUM">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/CONSTRUCTION = 'Wood' and BOATS/BOAT/AGE &gt; 10">Included</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--					Templates for  Wooden Boat, Display (END)								  -->
<!--		    					  FOR INDIANA												  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--					Templates for Dual Ownership, Display (START)							  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
<xsl:template name="DUAL_OWNERSHIP">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/DUALOWNERSHIP = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDDUALOWNERSHIP']/ATTRIBUTES/@SURCHARGE"/>							
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<xsl:template name="DUAL_OWNERSHIP_DISPLAY">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/DUALOWNERSHIP = 'Y'">Charge - Dual Ownership +<xsl:call-template name="DUAL_OWNERSHIP"></xsl:call-template>%</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<xsl:template name="DUAL_OWNERSHIP_PREMIUM">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/DUALOWNERSHIP = 'Y'">Included</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="DUAL_OWNERSHIP_FACTOR">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/DUALOWNERSHIP = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDDUALOWNERSHIP']/ATTRIBUTES/@FACTOR"/>							
		</xsl:when>
		<xsl:otherwise>1.00</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<!-- ============================================================================================ -->
<!--					Templates for  Dual Ownership, Display (END)							  -->
<!--		    					  FOR INDIANA												  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--					Templates for Waverunner 10%, Display (START)							  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
<xsl:template name="WAVERUNNER_DISPLAY">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/BOATTYPE='Waverunner'">Charge - Waverunner + 10%</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="WAVERUNNER_PREMIUM">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/BOATTYPE='Waverunner'">Included</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--							Templates for Waverunner 10%, Display (END)						  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--					Templates for length over 26 Feet, Display (START)						  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
<xsl:template name="LENGTH_26FEET">
	<xsl:choose>
		<xsl:when test = "(BOATS/BOAT/LENGTH &gt; 26 and BOATS/BOAT/BOATTYPE='Inboard')  or (BOATS/BOAT/LENGTH &gt; 26 and BOATS/BOAT/BOATTYPE='Inboard/Outboard') or (BOATS/BOAT/LENGTH &gt; 26 and BOATS/BOAT/BOATTYPE='Waverunner')">10</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="LENGTH_26FEET_DISPLAY">
	<xsl:choose>
		<xsl:when test = "(BOATS/BOAT/LENGTH &gt; 26 and BOATS/BOAT/BOATTYPE='Inboard')  or (BOATS/BOAT/LENGTH &gt; 26 and BOATS/BOAT/BOATTYPE='Inboard/Outboard') or (BOATS/BOAT/LENGTH &gt; 26 and BOATS/BOAT/BOATTYPE='Waverunner')">Charge - Length over 26 Feet +<xsl:call-template name="LENGTH_26FEET"></xsl:call-template>%</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="LENGTH_26FEET_PREMIUM">
	<xsl:choose>
		<xsl:when test = "(BOATS/BOAT/LENGTH &gt; 26 and BOATS/BOAT/BOATTYPE='Inboard')  or (BOATS/BOAT/LENGTH &gt; 26 and BOATS/BOAT/BOATTYPE='Inboard/Outboard') or (BOATS/BOAT/LENGTH &gt; 26 and BOATS/BOAT/BOATTYPE='Waverunner')">Included</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!--Template for over 26 feet and speed over 65-->
<xsl:template name="LENGTH_26FEET_SPEED65">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/LENGTH &gt; 26 and BOATS/BOAT/CAPABLESPEED &gt; 65">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LISURCHARGE']/NODE[@ID ='LIBOATSPEED']/ATTRIBUTES/@SURCHARGE"/>
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="LENGTH_26FEET_SPEED65_DISPLAY">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/LENGTH &gt; 26 and BOATS/BOAT/CAPABLESPEED &gt; 65">Charge - Speed over 54 MPH +<xsl:call-template name="LENGTH_26FEET_SPEED65"></xsl:call-template>%</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="LENGTH_26FEET_SPEED65_PREMIUM">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/LENGTH &gt; 26 and BOATS/BOAT/CAPABLESPEED &gt; 65">Included</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!--OLD
<xsl:template name="LENGTH_26FEET">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/LENGTH &gt; 26 and BOATS/BOAT/CAPABLESPEED &gt; 65">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LISURCHARGE']/NODE[@ID ='LIBOATSPEED']/ATTRIBUTES/@SURCHARGE"/>
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="LENGTH_26FEET_DISPLAY">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/LENGTH &gt; 26 and BOATS/BOAT/CAPABLESPEED &gt; 65">
			Charge - Length over 26 Feet +<xsl:call-template name="LENGTH_26FEET"></xsl:call-template>%			
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="LENGTH_26FEET_PREMIUM">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/LENGTH &gt; 26 and BOATS/BOAT/CAPABLESPEED &gt; 65">Included</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>-->

<!-- ============================================================================================ -->
<!--					Templates for length over 26 Feet, Display (END)						  -->
<!--		    					  FOR INDIANA												  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--					Templates for Fibre Glass over 15 years old , Display (END)				  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
<xsl:template name="FIBRE_GLASS">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/CONSTRUCTION = 'Fiberglass' and BOATS/BOAT/AGE &gt; 15">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='FIBERGLASSBOAT']/ATTRIBUTES/@SURCHARGE"/>
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="FIBRE_GLASS_DISPLAY">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/CONSTRUCTION = 'Fiberglass' and BOATS/BOAT/AGE &gt; 15">
			Charge - Fibre Glass Boat +<xsl:call-template name="FIBRE_GLASS"></xsl:call-template>%			
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="FIBRE_GLASS_PREMIUM">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/CONSTRUCTION = 'Fiberglass' and BOATS/BOAT/AGE &gt; 15">Included</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--					Templates for Fibre Glass over 15 years old , Display (END)				  -->
<!--		    					  FOR INDIANA												  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--					Templates for Remove Sailboat Racing Exclusion, Display (START)			  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
<xsl:template name="REMOVE_SAILBOAT">
	<xsl:choose>
		<xsl:when test ="BOATS/BOAT/REMOVESAILBOAT = 'Y' and BOATS/BOAT/LENGTH &lt; 26">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDREMOVALSAILBOAT']/ATTRIBUTES/@SURCHARGE"/>
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="REMOVE_SAILBOAT_DISPLAY">
	<xsl:choose>
		<xsl:when test ="BOATS/BOAT/REMOVESAILBOAT = 'Y' and BOATS/BOAT/LENGTH &lt; 26">Charge - Sailboat Racing Exclusion Waiver +<xsl:call-template name="REMOVE_SAILBOAT"></xsl:call-template>%</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="REMOVE_SAILBOAT_PREMIUM">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/REMOVESAILBOAT = 'Y' and BOATS/BOAT/LENGTH &lt; 26">Included</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="REMOVE_SAILBOAT_FACTOR">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/REMOVESAILBOAT = 'Y' and BOATS/BOAT/LENGTH &lt; 26">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDREMOVALSAILBOAT']/ATTRIBUTES/@FACTOR"/>
		</xsl:when>
		<xsl:otherwise>1.00</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--					Templates for Remove Sailboat Racing Exclusion , Display (END)			  -->
<!--		    					  FOR INDIANA												  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--					Templates for Final boat premium , Display (START)						  -->
<!--		    					  FOR INDIANA												  -->
<!-- ============================================================================================ -->

<xsl:template name="FINAL_PREMIUM">
 	(<xsl:call-template name='PHYSICAL_DAMAGE'></xsl:call-template>)
 	+
 	(<xsl:call-template name='WATERCRAFT_LIABILITY'></xsl:call-template>)
 </xsl:template>

<xsl:template name="TOTAL_PREMIUM_DISPLAY">
	Total <xsl:value-of select="BOATS/BOAT/BOATTYPE"></xsl:value-of> Premium
</xsl:template>

<!-- ============================================================================================ -->
<!--					Templates for Final boat premium , Display (END)						  -->
<!--		    					  FOR INDIANA												  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--					Templates for Unattached Equip. & Personal Effects Display (START)		  -->
<!--		    					  FOR INDIANA												  -->
<!-- ============================================================================================ -->

<xsl:template name="UNATTACHED_PREMIUM">

<xsl:variable name ="VARBOATCOUNT">
		<xsl:call-template name="BOATCOUNTFUNCTION"></xsl:call-template>  
	</xsl:variable>
	
	<xsl:choose>
		<xsl:when test ="$VARBOATCOUNT  = 0">0</xsl:when>
		<xsl:otherwise>
			<xsl:variable name="MIN_VALUE">
				<xsl:value-of select="POLICY/UNATTACHEDEQUIPMENT"/>
			</xsl:variable>
			<xsl:choose>
				<xsl:when test ="$MIN_VALUE ='1500'">'Included'</xsl:when>
				<xsl:otherwise>
						(((<xsl:value-of select="POLICY/UNATTACHEDEQUIPMENT"/>
						-
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID ='UNATTACHEDEQUIPMENT']/ATTRIBUTES/@MINIMUMINCREASE"/>)
						DIV <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID ='UNATTACHEDEQUIPMENT']/ATTRIBUTES/@RATE_PER_VALUE"/>)
						*<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID ='UNATTACHEDEQUIPMENT']/ATTRIBUTES/@MINIMUMPREMIUM"/>)
				</xsl:otherwise>
			</xsl:choose>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--					Templates for Unattached Equip. & Personal Effects Display (END)		  -->
<!--		    					  FOR INDIANA												  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--					Templates for Scheduled Miscellaneous Sports Equipment, Display (START)	  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
<xsl:template name="SPORTS_EQUIPMENT">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/LENGTH &gt; 26">
			<!--<xsl:value-of select= "/POLICY/@INSURANCESCORE"></xsl:value-of>-->
			10%
		</xsl:when>
		<xsl:otherwise>
			0
			<!--<xsl:variable name="INS" select="/POLICY/@INSURANCESCORE"></xsl:variable>
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='CREDIT']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@FACTOR"/>-->
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<!-- ============================================================================================ -->
<!--					Templates for Scheduled Miscellaneous Sports Equipment, Display (END)	  -->
<!--		    					  FOR INDIANA												  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--					Templates for Agreed Value Endorsement (AV100), Display (START)			  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->

<xsl:template name="AGREED_VALUE_ENDORSEMENT">
	<xsl:choose>
            <!--watercraft INDAIAN-->
			<xsl:when test ="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name ="AGREED_VALUE_ENDORSEMENT_INDIANA"/>
			</xsl:when>
			<!--watercraft MICHIGAN-->
			<xsl:when test ="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name ="AGREED_VALUE_ENDORSEMENT_MICHIGAN"/>
			</xsl:when>
			<!--watercraft WISCONSIN-->
			<xsl:when test ="POLICY/STATENAME ='WISCONSIN'">
				<xsl:call-template name ="AGREED_VALUE_ENDORSEMENT_WISCONSIN"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
<!--Agreed value endorsement INDAIAN-->	
<xsl:template name = "AGREED_VALUE_ENDORSEMENT_INDIANA">
	<xsl:choose>
		<xsl:when test="BOATS/BOAT/AV100 = 'Y'">
			<xsl:choose>				
				<xsl:when test="BOATS/BOAT/LENGTH &lt;= 26 and BOATS/BOAT/MARKETVALUE &lt;= 75000 and BOATS/BOAT/AGE &gt;= 20">	
					<xsl:choose>
						<xsl:when test="BOATS/BOAT/MARKETVALUE &gt; 10000">
							((<xsl:value-of select="BOATS/BOAT/MARKETVALUE"/>) DIV 1000)										
						</xsl:when>
						<xsl:otherwise>
							10
						</xsl:otherwise>
					</xsl:choose>				
				</xsl:when>				
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!--Agreed value endorsement MICHIGAN-->	
<xsl:template name = "AGREED_VALUE_ENDORSEMENT_MICHIGAN">
	<xsl:choose>
		<xsl:when test="BOATS/BOAT/AV100 = 'Y'">
			<xsl:choose>				
				<xsl:when test="BOATS/BOAT/LENGTH &lt;= 26 and BOATS/BOAT/MARKETVALUE &lt;= 75000 and BOATS/BOAT/AGE &gt;= 20">	
					<xsl:choose>
						<xsl:when test="BOATS/BOAT/MARKETVALUE &gt; 10000">
							((<xsl:value-of select="BOATS/BOAT/MARKETVALUE"/>) DIV 1000)										
						</xsl:when>
						<xsl:otherwise>
							10
						</xsl:otherwise>
					</xsl:choose>				
				</xsl:when>				
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!--Agreed value endorsement WISCONSIN-->	
<xsl:template name = "AGREED_VALUE_ENDORSEMENT_WISCONSIN">
	<xsl:choose>
		<xsl:when test="BOATS/BOAT/AV100 = 'Y'">
			<xsl:choose>				
				<xsl:when test="BOATS/BOAT/LENGTH &lt;= 26 and BOATS/BOAT/MARKETVALUE &lt;= 75000 and BOATS/BOAT/AGE &gt;= 20">	
					<xsl:choose>
						<xsl:when test="BOATS/BOAT/MARKETVALUE &gt; 10000">
							((<xsl:value-of select="BOATS/BOAT/MARKETVALUE"/>) DIV 1000)										
						</xsl:when>
						<xsl:otherwise>
							10
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
<!--					Templates for Agreed Value Endorsement (AV100), Display (END)			  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--			Templates for Watercraft Liability Poolution Coverage (OP900), Display (START)	  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->

<xsl:template name="LIABILITY_POLLUTION_COVERAGE">
		<xsl:choose>
            <!--watercraft INDAIAN-->
			<xsl:when test ="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name ="LIABILITY_POLLUTION_COVERAGE_INDIANA"/>
			</xsl:when>
			<!--watercraft MICHIGAN-->
			<xsl:when test ="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name ="LIABILITY_POLLUTION_COVERAGE_MICHIGAN"/>
			</xsl:when>
			<!--watercraft WISCONSIN-->
			<xsl:when test ="POLICY/STATENAME ='WISCONSIN'">
				<xsl:call-template name ="LIABILITY_POLLUTION_COVERAGE_WISCONSIN"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
</xsl:template>
<!--Liability Pollution coverage INDIANA-->	
<xsl:template name = "LIABILITY_POLLUTION_COVERAGE_INDIANA">
	<xsl:choose>
		<xsl:when test="BOATS/BOAT/OP900 = 'Y'">
			10	
		</xsl:when>
		<xsl:otherwise>
			0
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!--Liability Pollution coverage MICHIGAN-->	
<xsl:template name = "LIABILITY_POLLUTION_COVERAGE_MICHIGAN">
	<xsl:choose>
		<xsl:when test="BOATS/BOAT/OP900 = 'Y'">
			10	
		</xsl:when>
		<xsl:otherwise>
			0
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!--Liability Pollution coverage WISCONSIN-->	
<xsl:template name = "LIABILITY_POLLUTION_COVERAGE_WISCONSIN">
	<xsl:choose>
		<xsl:when test="BOATS/BOAT/OP900 = 'Y'">
			10	
		</xsl:when>
		<xsl:otherwise>
			0
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--				Templates for Watercraft Liability Poolution Coverage (OP900), Display (END)  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--				Templates for Client Entertainment (OP720), Display (START)					  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
<xsl:template name="CLIENT_ENTERTAINMENT">
		<xsl:choose>
		     <!--watercraft INDAIAN-->
			<xsl:when test ="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name ="CLIENT_ENTERTAINMENT_INDIANA"/>
			</xsl:when>
			 <!--watercraft MICHIGAN-->
			<xsl:when test ="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name ="CLIENT_ENTERTAINMENT_MICHIGAN"/>
			</xsl:when>
			 <!--watercraft WISCONSIN-->
			<xsl:when test ="POLICY/STATENAME ='WISCONSIN'">
				<xsl:call-template name ="CLIENT_ENTERTAINMENT_WISCONSIN"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
<!--Client Entertainment INDAIAN-->
<xsl:template name = "CLIENT_ENTERTAINMENT_INDIANA">
	<xsl:choose>
		<xsl:when test="BOATS/BOAT/OP720 = 'Y'">
			15	
		</xsl:when>
		<xsl:otherwise>
			0
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!--Client Entertainment MICHIGAN-->
<xsl:template name = "CLIENT_ENTERTAINMENT_MICHIGAN">
	<xsl:choose>
		<xsl:when test="BOATS/BOAT/OP720 = 'Y'">
			15	
		</xsl:when>
		<xsl:otherwise>
			0
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!--Client Entertainment WISCONSIN-->
<xsl:template name = "CLIENT_ENTERTAINMENT_WISCONSIN">
	<xsl:choose>
		<xsl:when test="BOATS/BOAT/OP720 = 'Y'">
			15	
		</xsl:when>
		<xsl:otherwise>
			0
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--				Templates for Client Entertainment (OP720), Display (END)  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--				Templates for WATERCRAFT LIABILITY, Display (START)							  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->

<xsl:template name = "WATERCRAFT_LIABILITY">
	<xsl:variable name="PER_AMOUNT" select="POLICY/PERSONALLIABILITY"></xsl:variable>
	<xsl:variable name="LENGTH" select="BOATS/BOAT/LENGTH"></xsl:variable> 
	<xsl:choose>
		<xsl:when test="BOATS/BOAT/BOATTYPE ='Outboard'">
			((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID='LIOUTBOARD']/ATTRIBUTES[@LIABILITY=$PER_AMOUNT]/@RATE"></xsl:value-of>)			
			*
			(<xsl:call-template name="DUAL_OWNERSHIP_FACTOR"></xsl:call-template>)
			*
			(<xsl:call-template name="REMOVE_SAILBOAT_FACTOR"></xsl:call-template>))
		</xsl:when>
		<xsl:when test="BOATS/BOAT/BOATTYPE ='Sailboat'">
		   <xsl:choose>
			<xsl:when test="$LENGTH &lt; 26">
				((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID='LIOUTBOARD']/ATTRIBUTES[@LIABILITY=$PER_AMOUNT]/@RATE"></xsl:value-of>)			
				*
				(<xsl:call-template name="DUAL_OWNERSHIP_FACTOR"></xsl:call-template>)
				*
				(<xsl:call-template name="BOAT_FEET_SPEED"></xsl:call-template>)
				*
				(<xsl:call-template name="REMOVE_SAILBOAT_FACTOR"></xsl:call-template>))
			</xsl:when>
			<xsl:otherwise>
				((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID='ALLOUTBOARD']/ATTRIBUTES[@LIABILITY=$PER_AMOUNT]/@RATE"></xsl:value-of>)			
				*
				(<xsl:call-template name="DUAL_OWNERSHIP_FACTOR"></xsl:call-template>)
				*
				(<xsl:call-template name="BOAT_FEET_SPEED"></xsl:call-template>))
			</xsl:otherwise>
		</xsl:choose>
			
		</xsl:when>		
		<xsl:when test="BOATS/BOAT/BOATTYPE ='Waverunner'">
			((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='SECTIONIILIABILITY']/ATTRIBUTES[@LIABILITY=$PER_AMOUNT]/@WAVERUNNERRATE"></xsl:value-of>)			
			<!--*
			(<xsl:call-template name="DUAL_OWNERSHIP_FACTOR"></xsl:call-template>)-->
			*
			(<xsl:call-template name="BOAT_FEET_SPEED"></xsl:call-template>))
		</xsl:when>		
		<xsl:when test="BOATS/BOAT/BOATTYPE ='Jet Ski'">
			((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='SECTIONIILIABILITY']/ATTRIBUTES[@LIABILITY=$PER_AMOUNT]/@JETSKIRATE"></xsl:value-of>)			
			<!--*
			(<xsl:call-template name="DUAL_OWNERSHIP_FACTOR"></xsl:call-template>)-->
			*
			(<xsl:call-template name="BOAT_FEET_SPEED"></xsl:call-template>))
		</xsl:when>		
		<xsl:otherwise>
			((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID='ALLOUTBOARD']/ATTRIBUTES[@LIABILITY=$PER_AMOUNT]/@RATE"></xsl:value-of>)			
			*
			(<xsl:call-template name="DUAL_OWNERSHIP_FACTOR"></xsl:call-template>)
			*
			(<xsl:call-template name="BOAT_FEET_SPEED"></xsl:call-template>))
		</xsl:otherwise>	
	</xsl:choose>	
</xsl:template>

<!--Old-->
<!--<xsl:template name = "WATERCRAFT_LIABILITY">
	<xsl:variable name="PER_AMOUNT" select="POLICY/PERSONALLIABILITY"></xsl:variable>
	<xsl:choose>
		<xsl:when test="BOATS/BOAT/BOATTYPE ='Outboard' or BOATS/BOAT/BOATTYPE ='Sailboat'">
			((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID='LIOUTBOARD']/ATTRIBUTES[@LIABILITY=$PER_AMOUNT]/@RATE"></xsl:value-of>)			
			*
			(<xsl:call-template name="DUAL_OWNERSHIP_FACTOR"></xsl:call-template>)
			*
			(<xsl:call-template name="REMOVE_SAILBOAT_FACTOR"></xsl:call-template>))
			*
			(<xsl:call-template name="BOAT_FEET_SPEED"></xsl:call-template>))
		</xsl:when>
		<xsl:when test="BOATS/BOAT/BOATTYPE ='Waverunner'">
			((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='SECTIONIILIABILITY']/ATTRIBUTES[@LIABILITY=$PER_AMOUNT]/@WAVERUNNERRATE"></xsl:value-of>)			
			*
			(<xsl:call-template name="DUAL_OWNERSHIP_FACTOR"></xsl:call-template>)
			*
			(<xsl:call-template name="BOAT_FEET_SPEED"></xsl:call-template>))
		</xsl:when>		
		<xsl:when test="BOATS/BOAT/BOATTYPE ='Jet Ski'">
			((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='SECTIONIILIABILITY']/ATTRIBUTES[@LIABILITY=$PER_AMOUNT]/@JETSKIRATE"></xsl:value-of>)			
			*
			(<xsl:call-template name="DUAL_OWNERSHIP_FACTOR"></xsl:call-template>)
			*
			(<xsl:call-template name="BOAT_FEET_SPEED"></xsl:call-template>))
		</xsl:when>		
		<xsl:otherwise>
			((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID='ALLOUTBOARD']/ATTRIBUTES[@LIABILITY=$PER_AMOUNT]/@RATE"></xsl:value-of>)			
			*
			(<xsl:call-template name="DUAL_OWNERSHIP_FACTOR"></xsl:call-template>)
			*
			(<xsl:call-template name="BOAT_FEET_SPEED"></xsl:call-template>))
		</xsl:otherwise>	
	</xsl:choose>	
</xsl:template>-->

<xsl:template name="WATERCRAFT_LIABILITY_DISP1">
		<xsl:choose>
		    <!--Watercraft INDIANA-->
			<xsl:when test ="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name ="WATERCRAFT_LIABILITY_DISP1_INDIANA"/>
			</xsl:when>
			<!--Watercraft MICHIGAN-->
			<xsl:when test ="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name ="WATERCRAFT_LIABILITY_DISP1_MICHIGAN"/>
			</xsl:when>
			<!--Watercraft WISCONSIN-->
			<xsl:when test ="POLICY/STATENAME ='WISCONSIN'">
				<xsl:call-template name ="WATERCRAFT_LIABILITY_DISP1_WISCONSIN"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>


<xsl:template name = "WATERCRAFT_LIABILITY_DISP1_INDIANA">
	<xsl:choose>
		<xsl:when test="POLICY/ATTACHTOWOLVERINE = 'Y' and POLICY/BOATHOMEDISC = 'Y'">Extended</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>		
	</xsl:choose>
</xsl:template>	
<xsl:template name = "WATERCRAFT_LIABILITY_DISP1_MICHIGAN">
	<xsl:choose>
		<xsl:when test="POLICY/ATTACHTOWOLVERINE = 'Y' and POLICY/BOATHOMEDISC = 'Y'">Extended</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>		
	</xsl:choose>
</xsl:template>	
<xsl:template name = "WATERCRAFT_LIABILITY_DISP1_WISCONSIN">
	<xsl:choose>
		<xsl:when test="POLICY/ATTACHTOWOLVERINE = 'Y' and POLICY/BOATHOMEDISC = 'Y'">Extended</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>		
	</xsl:choose>
</xsl:template>	


<xsl:template name="WATERCRAFT_LIABILITY_DISP2">
		<xsl:choose>
		    <!--Watercraft INDIANA-->
			<xsl:when test ="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name ="WATERCRAFT_LIABILITY_DISP2_INDIANA"/>
			</xsl:when>
			<!--Watercraft MICHIGAN-->
			<xsl:when test ="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name ="WATERCRAFT_LIABILITY_DISP2_MICHIGAN"/>
			</xsl:when>
			<!--Watercraft WISCONSIN-->
			<xsl:when test ="POLICY/STATENAME ='WISCONSIN'">
				<xsl:call-template name ="WATERCRAFT_LIABILITY_DISP2_WISCONSIN"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
</xsl:template>
	
<xsl:template name = "WATERCRAFT_LIABILITY_DISP2_INDIANA">
	<xsl:choose>
		<xsl:when test="POLICY/ATTACHTOWOLVERINE = 'Y' and POLICY/BOATHOMEDISC = 'Y'">
			0
		</xsl:when>
		<xsl:otherwise>
			<xsl:call-template name="WATERCRAFT_LIABILITY"/>
		</xsl:otherwise>		
	</xsl:choose>
</xsl:template>

<xsl:template name = "WATERCRAFT_LIABILITY_DISP2_MICHIGAN">
	<xsl:choose>
		<xsl:when test="POLICY/ATTACHTOWOLVERINE = 'Y' and POLICY/BOATHOMEDISC = 'Y'">
			0
		</xsl:when>
		<xsl:otherwise>
			<xsl:call-template name="WATERCRAFT_LIABILITY"/>
		</xsl:otherwise>		
	</xsl:choose>
</xsl:template>

<xsl:template name = "WATERCRAFT_LIABILITY_DISP2_WISCONSIN">
	<xsl:choose>
		<xsl:when test="POLICY/ATTACHTOWOLVERINE = 'Y' and POLICY/BOATHOMEDISC = 'Y'">
			0
		</xsl:when>
		<xsl:otherwise>
			<xsl:call-template name="WATERCRAFT_LIABILITY"/>
		</xsl:otherwise>		
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--				Templates for WATERCRAFT LIABILITY, Display (END)							  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--				Templates for Medical Payments, Display (START)							      -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->

<xsl:template name = "MEDICAL_PAYMENTS">
 <xsl:variable name="PER_AMOUNT" select="POLICY/MEDICALPAYMENTSOTHER"></xsl:variable>
	<xsl:choose>
		<xsl:when test="BOATS/BOAT/BOATTYPE = 11386 or BOATS/BOAT/BOATTYPE = 11387">
		0
		</xsl:when>	
		<xsl:otherwise>
			<xsl:choose>
				<xsl:when test="POLICY/MEDICALPAYMENTSOTHER &gt; 1000">
					(((<xsl:value-of select="POLICY/MEDICALPAYMENTSOTHER"/> - 1000) DIV 1000) * 5)
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:otherwise>
	</xsl:choose>	
</xsl:template>


<xsl:template name="MEDICAL_PAYMENTS_DISP1">
		<xsl:choose>
		    <!--Watercraft INDIANA-->
			<xsl:when test ="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name ="MEDICAL_PAYMENTS_DISP1_INDIANA"/>
			</xsl:when>
			<!--Watercraft MICHIGAN-->
			<xsl:when test ="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name ="MEDICAL_PAYMENTS_DISP1_MICHIGAN"/>
			</xsl:when>
			<!--Watercraft WISCONSIN-->
			<xsl:when test ="POLICY/STATENAME ='WISCONSIN'">
				<xsl:call-template name ="MEDICAL_PAYMENTS_DISP1_WISCONSIN"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
</xsl:template>

<!--Medical Payments Display 1 for INDIANA-->
<xsl:template name = "MEDICAL_PAYMENTS_DISP1_INDIANA">
	<xsl:choose>
		<xsl:when test="POLICY/ATTACHTOWOLVERINE = 'Y' and POLICY/BOATHOMEDISC = 'Y'">Extended</xsl:when>	
		<xsl:otherwise>
			<xsl:choose>
				<xsl:when test="POLICY/MEDICALPAYMENTSOTHER = 1000 or (BOATS/BOAT/BOATTYPE = 11386 or BOATS/BOAT/BOATTYPE = 11387)">Included</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>		
			</xsl:choose>	
		</xsl:otherwise>
	</xsl:choose>	
</xsl:template>	
<!--Medical Payments Display 1 for MICHIGAN-->
<xsl:template name = "MEDICAL_PAYMENTS_DISP1_MICHIGAN">
	<xsl:choose>
		<xsl:when test="POLICY/ATTACHTOWOLVERINE = 'Y' and POLICY/BOATHOMEDISC = 'Y'">Extended</xsl:when>	
		<xsl:otherwise>
			<xsl:choose>
				<xsl:when test="POLICY/MEDICALPAYMENTSOTHER = 1000 or (BOATS/BOAT/BOATTYPE = 11386 or BOATS/BOAT/BOATTYPE = 11387)">Included</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>		
			</xsl:choose>	
		</xsl:otherwise>
	</xsl:choose>	
</xsl:template>
<!--Medical Payments Display 1 for WISCONSIN-->
<xsl:template name = "MEDICAL_PAYMENTS_DISP1_WISCONSIN">
	<xsl:choose>
		<xsl:when test="POLICY/ATTACHTOWOLVERINE = 'Y' and POLICY/BOATHOMEDISC = 'Y'">Extended</xsl:when>	
		<xsl:otherwise>
			<xsl:choose>
				<xsl:when test="POLICY/MEDICALPAYMENTSOTHER = 1000 or (BOATS/BOAT/BOATTYPE = 11386 or BOATS/BOAT/BOATTYPE = 11387)">Included</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>		
			</xsl:choose>	
		</xsl:otherwise>
	</xsl:choose>	
</xsl:template>		

<xsl:template name="MEDICAL_PAYMENTS_DISP2">
		<xsl:choose>
		    <!--Watercraft INDIANA-->
			<xsl:when test ="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name ="MEDICAL_PAYMENTS_DISP2_INDIANA"/>
			</xsl:when>
			<!--Watercraft MICHIGAN-->
			<xsl:when test ="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name ="MEDICAL_PAYMENTS_DISP2_MICHIGAN"/>
			</xsl:when>
			<!--Watercraft WISCONSIN-->
			<xsl:when test ="POLICY/STATENAME ='WISCONSIN'">
				<xsl:call-template name ="MEDICAL_PAYMENTS_DISP2_WISCONSIN"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
</xsl:template>

<!--Medical Payments Display 2 INDIANA-->
<xsl:template name = "MEDICAL_PAYMENTS_DISP2_INDIANA">
	<xsl:choose>
		<xsl:when test="POLICY/MEDICALPAYMENTSOTHER = 1000 or (BOATS/BOAT/BOATTYPE = 11386 or BOATS/BOAT/BOATTYPE = 11387)">
			0
		</xsl:when>
		<xsl:otherwise>
			<xsl:call-template name="MEDICAL_PAYMENTS"/>
		</xsl:otherwise>		
	</xsl:choose>
</xsl:template>
<!--Medical Payments Display 2 MICHIGAN-->
<xsl:template name = "MEDICAL_PAYMENTS_DISP2_MICHIGAN">
	<xsl:choose>
		<xsl:when test="POLICY/MEDICALPAYMENTSOTHER = 1000 or (BOATS/BOAT/BOATTYPE = 11386 or BOATS/BOAT/BOATTYPE = 11387)">
			0
		</xsl:when>
		<xsl:otherwise>
			<xsl:call-template name="MEDICAL_PAYMENTS"/>
		</xsl:otherwise>		
	</xsl:choose>
</xsl:template>
<!--Medical Payments Display 2 WISCONSIN-->
<xsl:template name = "MEDICAL_PAYMENTS_DISP2_WISCONSIN">
	<xsl:choose>
		<xsl:when test="POLICY/MEDICALPAYMENTSOTHER = 1000 or (BOATS/BOAT/BOATTYPE = 11386 or BOATS/BOAT/BOATTYPE = 11387)">
			0
		</xsl:when>
		<xsl:otherwise>
			<xsl:call-template name="MEDICAL_PAYMENTS"/>
		</xsl:otherwise>		
	</xsl:choose>
</xsl:template>

<xsl:template name = "MEDICAL_PAYMENTS_LIMITS_DISPLAY">
	<xsl:choose>
	<xsl:when test="POLICY/MEDICALPAYMENTSOTHER = 1000 or (BOATS/BOAT/BOATTYPE = 11386 or BOATS/BOAT/BOATTYPE = 11387)">1000</xsl:when>
	<xsl:otherwise>
		<xsl:value-of select="POLICY/MEDICALPAYMENTSOTHER"></xsl:value-of>		
	</xsl:otherwise>			
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--				Templates for Medical Payments, Display (END)								  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--				Templates for Boat towing, Display (START)									  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->

<xsl:template name = "BOAT_TOWING">
	<xsl:choose>
	<xsl:when test="BOATS/BOAT/MARKETVALUE != ''">
		((<xsl:value-of select="BOATS/BOAT/MARKETVALUE"></xsl:value-of>)* (0.05))
	</xsl:when>	
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--				Templates for Boat towing, Display (END)									  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--				Templates for Boat Replacement Cost Coverage, Display (START)				  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
<xsl:template name="BOAT_REPLACEMENT_DISPLAY">
		<xsl:choose>
			<xsl:when test ="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name ="BOAT_REPLACEMENT_DISPLAY_INDIANA"/>
			</xsl:when>
			<xsl:when test ="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name ="BOAT_REPLACEMENT_DISPLAY_MICHIGAN"/>
			</xsl:when>
			<xsl:when test ="POLICY/STATENAME ='WISCONSIN'">
				<xsl:call-template name ="BOAT_REPLACEMENT_DISPLAY_WISCONSIN"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>

<!--Boat replacement INDIANA-->
<xsl:template name="BOAT_REPLACEMENT_DISPLAY_INDIANA">	
	<xsl:choose>
		<xsl:when test="BOATS/BOAT/AGE &gt; 5">Included</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>	
	</xsl:choose> 
</xsl:template>
<!--Boat replacement MICHIGAN-->
<xsl:template name="BOAT_REPLACEMENT_DISPLAY_MICHIGAN">	
	<xsl:choose>
		<xsl:when test="BOATS/BOAT/AGE &gt; 5">Included</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>	
	</xsl:choose> 
</xsl:template>
<!--Boat replacement WISCONSIN-->
<xsl:template name="BOAT_REPLACEMENT_DISPLAY_WISCONSIN">	
	<xsl:choose>
		<xsl:when test="BOATS/BOAT/AGE &gt; 5">Included</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>	
	</xsl:choose> 
</xsl:template>
<!-- ============================================================================================ -->
<!--				Templates for Boat Replacement Cost Coverage, Display (END)					  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--				Templates for Uninsured Boaters Coverage, Display (END)						  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
<xsl:template name="UNINSURED_BOATERS_COVERAGE">
		<xsl:choose>
		    <!--Watercraft INDIANA-->
			<xsl:when test ="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name ="UNINSURED_BOATERS_COVERAGE_INDIANA"/>
			</xsl:when>
			<!--Watercraft MICHIGAN-->
			<xsl:when test ="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name ="UNINSURED_BOATERS_COVERAGE_MICHIGAN"/>
			</xsl:when>
			<!--Watercraft WISCONSIN-->
			<xsl:when test ="POLICY/STATENAME ='WISCONSIN'">
				<xsl:call-template name ="UNINSURED_BOATERS_COVERAGE_WISCONSIN"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
</xsl:template>
<!--Uninsured Boaters coverage INDIANA-->
	<xsl:template name="UNINSURED_BOATERS_COVERAGE_INDIANA">
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/BOATTYPE = 11369">
				<xsl:choose>		
					<xsl:when test="POLICY/ATTACHTOWOLVERINE = 'Y' and POLICY/BOATHOMEDISC = 'Y'">13</xsl:when>
					<xsl:otherwise>Included</xsl:otherwise>
				</xsl:choose>	
			</xsl:when>
			<xsl:otherwise>Included</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
<!--Uninsured Boaters coverage MICHIGAN-->
	<xsl:template name="UNINSURED_BOATERS_COVERAGE_MICHIGAN">
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/BOATTYPE = 11369">
				<xsl:choose>		
					<xsl:when test="POLICY/ATTACHTOWOLVERINE = 'Y' and POLICY/BOATHOMEDISC = 'Y'">13</xsl:when>
					<xsl:otherwise>Included</xsl:otherwise>
				</xsl:choose>	
			</xsl:when>
			<xsl:otherwise>Included</xsl:otherwise>
		</xsl:choose>
	</xsl:template>	
	<!--Uninsured Boaters coverage WISCONSIN-->
	<xsl:template name="UNINSURED_BOATERS_COVERAGE_WISCONSIN">
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/BOATTYPE = 11369">
				<xsl:choose>		
					<xsl:when test="POLICY/ATTACHTOWOLVERINE = 'Y' and POLICY/BOATHOMEDISC = 'Y'">13</xsl:when>
					<xsl:otherwise>Included</xsl:otherwise>
				</xsl:choose>	
			</xsl:when>
			<xsl:otherwise>Included</xsl:otherwise>
		</xsl:choose>
	</xsl:template>	
<!-- ============================================================================================ -->
<!--				Templates for Uninsured Boaters Coverage, Display (END)						  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->

<xsl:template name="DISCOUNT_WOLVERINE">
	<xsl:choose>
		<xsl:when test="POLICY/BOATHOMEDISC = 'Y'">15</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>		
	</xsl:choose>
</xsl:template>

<xsl:template name="DISCOUNT_WOLVERINE_DISPLAY">
	<xsl:choose>
		<xsl:when test="POLICY/BOATHOMEDISC = 'Y'">Discount - Wolverine Homeowners -(<xsl:call-template name="DISCOUNT_WOLVERINE"></xsl:call-template>%)</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>		
	</xsl:choose>
</xsl:template>

<xsl:template name="DISCOUNT_WOLVERINE_PREMIUM">
	<xsl:choose>
		<xsl:when test="POLICY/BOATHOMEDISC = 'Y'">Included</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>		
	</xsl:choose>
</xsl:template>

<xsl:template name="DISCOUNT_WOLVERINE_FACTOR">
	<xsl:choose>
		<xsl:when test="POLICY/BOATHOMEDISC = 'Y'">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID='MULTIPOLICY']/ATTRIBUTES/@FACTOR"></xsl:value-of>
		</xsl:when>
		<xsl:otherwise>1.00</xsl:otherwise>		
	</xsl:choose>
</xsl:template>



<!-- ============================================================================================ -->
<!--				Templates for Physical Damage, Display (END)								  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
	
	<xsl:template name="PHYSICAL_DAMAGE">
		<xsl:choose>
		    <!--Watercraft INDIANA-->
			<xsl:when test ="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name ="PHYSICAL_DAMAGE_INDIANA"/>
			</xsl:when>
			<!--Watercraft MICHIGAN-->
			<xsl:when test ="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name ="PHYSICAL_DAMAGE_MICHIGAN"/>
			</xsl:when>
			<!--Watercraft WISCONSIN-->
			<xsl:when test ="POLICY/STATENAME ='WISCONSIN'">
				<xsl:call-template name ="PHYSICAL_DAMAGE_WISCONSIN"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	<!--Watercraft Physical Damage INDIANA-->
	<xsl:template name="PHYSICAL_DAMAGE_INDIANA">
		<xsl:variable name="BOATTYPE1">
			<xsl:call-template name='BOATTYPE'/>
		</xsl:variable>
		
		<xsl:choose>
			<xsl:when test="$BOATTYPE1 = 'Outboard'">
				<xsl:call-template name='OUTBOARD_RATE_CALC'/>
			</xsl:when>			
			<xsl:when test="$BOATTYPE1 = 'Inboard'">
				<xsl:call-template name='INBOARD_RATE_CALC'/>
			</xsl:when>			
			<xsl:when test="$BOATTYPE1 = 'Inboard/Outboard'">
				<xsl:call-template name='INOUTBOARD_RATE_CALC'/>
			</xsl:when>			
			<xsl:when test="$BOATTYPE1 = 'Sailboat'">
				<xsl:call-template name='SAILBOAT_RATE_CALC'/>
			</xsl:when>			
			<xsl:when test="$BOATTYPE1 = 'Jet Ski'">
				<xsl:call-template name='JETSKI_RATE_CALC'/>
			</xsl:when>	
			<xsl:when test="$BOATTYPE1 = 'Waverunner'">
				<xsl:call-template name='WAVE_RATE_CALC'/>
			</xsl:when>		
			<xsl:when test="$BOATTYPE1 = 'Jetski Trailer'">
				<xsl:call-template name='JETSKI_TRAILOR_RATE_CALC'/>
			</xsl:when>
			<xsl:when test="$BOATTYPE1 = 'WaveRunner Trailer'">
				<xsl:call-template name='WAVERUNNER_TRAILOR_RATE_CALC'/>
			</xsl:when>
		</xsl:choose>
	</xsl:template>	
	
	<!--Watercraft Physical Damage MICHIGAN-->
	<xsl:template name="PHYSICAL_DAMAGE_MICHIGAN">
		<xsl:variable name="BOATTYPE1">
			<xsl:call-template name='BOATTYPE'/>
		</xsl:variable>
		
		<xsl:choose>
			<xsl:when test="$BOATTYPE1 = 'Outboard'">
				<xsl:call-template name='OUTBOARD_RATE_CALC'/>
			</xsl:when>			
			<xsl:when test="$BOATTYPE1 = 'Inboard'">
				<xsl:call-template name='INBOARD_RATE_CALC'/>
			</xsl:when>			
			<xsl:when test="$BOATTYPE1 = 'Inboard/Outboard'">
				<xsl:call-template name='INOUTBOARD_RATE_CALC'/>
			</xsl:when>			
			<xsl:when test="$BOATTYPE1 = 'Sailboat'">
				<xsl:call-template name='SAILBOAT_RATE_CALC'/>
			</xsl:when>			
			<xsl:when test="$BOATTYPE1 = 'Jet Ski'">
				<xsl:call-template name='JETSKI_RATE_CALC'/>
			</xsl:when>	
			<xsl:when test="$BOATTYPE1 = 'Waverunner'">
				<xsl:call-template name='WAVE_RATE_CALC'/>
			</xsl:when>		
			<xsl:when test="$BOATTYPE1 = 'Jetski Trailer'">
				<xsl:call-template name='JETSKI_TRAILOR_RATE_CALC'/>
			</xsl:when>
			<xsl:when test="$BOATTYPE1 = 'WaveRunner Trailer'">
				<xsl:call-template name='WAVERUNNER_TRAILOR_RATE_CALC'/>
			</xsl:when>
		</xsl:choose>
	</xsl:template>	
	
	<!--Watercraft Physical Damage WISCONSIN-->
	<xsl:template name="PHYSICAL_DAMAGE_WISCONSIN">
		<xsl:variable name="BOATTYPE1">
			<xsl:call-template name='BOATTYPE'/>
		</xsl:variable>
		
		<xsl:choose>
			<xsl:when test="$BOATTYPE1 = 'Outboard'">
				<xsl:call-template name='OUTBOARD_RATE_CALC'/>
			</xsl:when>			
			<xsl:when test="$BOATTYPE1 = 'Inboard'">
				<xsl:call-template name='INBOARD_RATE_CALC'/>
			</xsl:when>			
			<xsl:when test="$BOATTYPE1 = 'Inboard/Outboard'">
				<xsl:call-template name='INOUTBOARD_RATE_CALC'/>
			</xsl:when>			
			<xsl:when test="$BOATTYPE1 = 'Sailboat'">
				<xsl:call-template name='SAILBOAT_RATE_CALC'/>
			</xsl:when>			
			<xsl:when test="$BOATTYPE1 = 'Jet Ski'">
				<xsl:call-template name='JETSKI_RATE_CALC'/>
			</xsl:when>	
			<xsl:when test="$BOATTYPE1 = 'Waverunner'">
				<xsl:call-template name='WAVE_RATE_CALC'/>
			</xsl:when>		
			<xsl:when test="$BOATTYPE1 = 'Jetski Trailer'">
				<xsl:call-template name='JETSKI_TRAILOR_RATE_CALC'/>
			</xsl:when>
			<xsl:when test="$BOATTYPE1 = 'WaveRunner Trailer'">
				<xsl:call-template name='WAVERUNNER_TRAILOR_RATE_CALC'/>
			</xsl:when>
		</xsl:choose>
	</xsl:template>	
<!-- ============================================================================================ -->
<!--				Templates for Outboard, Display (START)								          -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
	<xsl:template name="OUTBOARD_RATE_CALC">
		<xsl:variable name="MARKETVALUE"><xsl:call-template name='MARKETVALUE'/></xsl:variable>
		
		<xsl:variable name="DED_DESC"><xsl:call-template name='DEDUCTIBLE'/></xsl:variable>
		
		((((((((((((<xsl:call-template name="OUTBOARD_RATE"/>)
		*
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='RATERELATIVITY']/NODE[@ID='OUTBOARD']/ATTRIBUTES[@COVERAGE=$MARKETVALUE]/@RELATIVITY"></xsl:value-of>)				
		*
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID='DEDUCTIBLE']/ATTRIBUTES[@DESC=$DED_DESC]/@FACTOR"></xsl:value-of>)
		*<!--Multipolicy Credit-->
		<xsl:call-template name="DISCOUNT_WOLVERINE_FACTOR"></xsl:call-template>)
		*<!--Insurance Score-->
		<xsl:call-template name="INSURANCE_SCORE_CALC"></xsl:call-template>)
		*<!--Constructor type Fibre,Water etc..-->
		<xsl:call-template name="CONSTRUCTION_TYPE"></xsl:call-template>)
		*<!--Charge Dual Ownership-->
		<xsl:call-template name="DUAL_OWNERSHIP_FACTOR"></xsl:call-template>)
		*<!--Credit Diesel Engine-->
		<xsl:call-template name="DIESEL_ENGINE_FACTOR"></xsl:call-template>)
		*<!--Credit Shore Station-->
		<xsl:call-template name="SHORE_STATION_FACTOR"></xsl:call-template>)
		*<!--Credit Loran GPS Factor-->
		<xsl:call-template name="LORAN_GPS_FACTOR"></xsl:call-template>)
		*<!--Credit MultiBoat-->
		<xsl:call-template name="MULTI_BOAT_FACTOR"></xsl:call-template>)
		*<!--Credit 5 or More Years Experience/Coast Guard/Power Squadron-->
		<xsl:call-template name="EXPERIENCE_CREDIT_FACTOR"></xsl:call-template>)
	</xsl:template>
	
	<xsl:template name="OUTBOARD_RATE">
		<xsl:variable name="TERRITORY"><xsl:call-template name='TERRITORY'/></xsl:variable>
		<xsl:variable name="WATERS"><xsl:call-template name='WATERS'/></xsl:variable>						
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASERATES']/NODE[@ID=$WATERS]/ATTRIBUTES[@TERRITORYCODE=$TERRITORY]/@OUTBOARD"></xsl:value-of>				
	</xsl:template>	

<!-- ============================================================================================ -->
<!--				Templates for Outboard, Display (END)										  -->
<!--		    				  FOR INDIANA										              -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--				Templates for Inboard, Display (START)										  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
	
	
	<xsl:template name="INBOARD_RATE_CALC">
		<xsl:variable name="MARKETVALUE"><xsl:call-template name='MARKETVALUE'/></xsl:variable>		
		<xsl:variable name="DED_DESC"><xsl:call-template name='DEDUCTIBLE'/></xsl:variable>
		
		(((((((((((((((<xsl:call-template name="INBOARD_RATE"/>)
		*
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='RATERELATIVITY']/NODE[@ID='INBOARD']/ATTRIBUTES[@COVERAGE=$MARKETVALUE]/@RELATIVITY"></xsl:value-of>)				
		*
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID='DEDUCTIBLE']/ATTRIBUTES[@DESC=$DED_DESC]/@FACTOR"></xsl:value-of>)							
		*<!--Multipolicy Credit-->
		<xsl:call-template name="DISCOUNT_WOLVERINE_FACTOR"></xsl:call-template>)
		*<!--Insurance Score-->
		<xsl:call-template name="INSURANCE_SCORE_CALC"></xsl:call-template>)
		*<!--Constructor type Fibre,Water etc..-->
		<xsl:call-template name="CONSTRUCTION_TYPE"></xsl:call-template>)
		*<!--Charge Dual Ownership-->
		<xsl:call-template name="DUAL_OWNERSHIP_FACTOR"></xsl:call-template>)
		*<!--Charge Over 26 Feet-->
		<xsl:call-template name="BOAT_FEET"></xsl:call-template>)
		*<!--Charge Over 26 Feet and Speed 65 MPH-->
		<xsl:call-template name="BOAT_FEET_SPEED"></xsl:call-template>)
		*<!--Credit Diesel Engine-->
		<xsl:call-template name="DIESEL_ENGINE_FACTOR"></xsl:call-template>)
		*<!--Credit MultiBoat-->
		<xsl:call-template name="MULTI_BOAT_FACTOR"></xsl:call-template>)
		*<!--Credit Shore Station-->
		<xsl:call-template name="SHORE_STATION_FACTOR"></xsl:call-template>)
		*<!--Credit Loran GPS Factor-->
		<xsl:call-template name="LORAN_GPS_FACTOR"></xsl:call-template>)
		*<!--Credit Haron Fire Factor-->
		<xsl:call-template name="HALON_FIRE_FACTOR"></xsl:call-template>)
		*<!--Credit 5 or More Years Experience/Coast Guard/Power Squadron-->
		<xsl:call-template name="EXPERIENCE_CREDIT_FACTOR"></xsl:call-template>)
	</xsl:template>
	
	<xsl:template name="INBOARD_RATE">
		<xsl:variable name="TERRITORY"><xsl:call-template name='TERRITORY'/></xsl:variable>				
		<xsl:variable name="WATERS"><xsl:call-template name='WATERS'/></xsl:variable>						
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASERATES']/NODE[@ID=$WATERS]/ATTRIBUTES[@TERRITORYCODE=$TERRITORY]/@INBOARD"></xsl:value-of>				
	</xsl:template>
	
	<xsl:template name="CONSTRUCTION_TYPE">
		<xsl:choose>
			<xsl:when test = "BOATS/BOAT/CONSTRUCTION = 'Wood' and BOATS/BOAT/AGE &gt; 10">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='WOODENBOAT']/ATTRIBUTES/@FACTOR"/>	
			</xsl:when>
			<xsl:when test = "BOATS/BOAT/CONSTRUCTION = 'Fiberglass' and BOATS/BOAT/AGE &gt; 15">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='FIBERGLASSBOAT']/ATTRIBUTES/@FACTOR"/>	
			</xsl:when>
			<xsl:otherwise>
			1
			</xsl:otherwise>
	  </xsl:choose>
	</xsl:template>
	
	<xsl:template name="BOAT_FEET">
		<xsl:choose>
			<xsl:when test="BOATS/BOAT/LENGTH &gt; 26">
			1.10
			</xsl:when>
			<xsl:otherwise>
			1
			</xsl:otherwise>
		</xsl:choose>
		
	</xsl:template>
	
	<xsl:template name="BOAT_FEET_SPEED">
		<xsl:choose>
			<xsl:when test = "BOATS/BOAT/LENGTH &gt; 26 and BOATS/BOAT/CAPABLESPEED &gt; 65">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LISURCHARGE']/NODE[@ID ='LIBOATSPEED']/ATTRIBUTES/@FACTOR"/>
			</xsl:when>
			<xsl:otherwise>
			1
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	
	
<!-- ============================================================================================ -->
<!--				Templates for Inboard, Display (END)										  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
	
<!-- ============================================================================================ -->
<!--				Templates for InOutboard, Display (START)									  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
	
	<xsl:template name="INOUTBOARD_RATE_CALC">
		<xsl:variable name="MARKETVALUE"><xsl:call-template name='MARKETVALUE'/></xsl:variable>		
		<xsl:variable name="DED_DESC"><xsl:call-template name='DEDUCTIBLE'/></xsl:variable>
		
		(((((((((((((((<xsl:call-template name="INOUTBOARD_RATE"/>)
		*
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='RATERELATIVITY']/NODE[@ID='INOUTBOARD']/ATTRIBUTES[@COVERAGE=$MARKETVALUE]/@RELATIVITY"></xsl:value-of>)
		*
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID='DEDUCTIBLE']/ATTRIBUTES[@DESC=$DED_DESC]/@FACTOR"></xsl:value-of>)
		*<!--Multipolicy Credit-->
		<xsl:call-template name="DISCOUNT_WOLVERINE_FACTOR"></xsl:call-template>)
		*<!--Insurance Score-->
		<xsl:call-template name="INSURANCE_SCORE_CALC"></xsl:call-template>)
		*<!--Constructor type Fibre,Water etc..-->
		<xsl:call-template name="CONSTRUCTION_TYPE"></xsl:call-template>)
		*<!--Charge Dual Ownership-->
		<xsl:call-template name="DUAL_OWNERSHIP_FACTOR"></xsl:call-template>)
		*<!--Credit Diesel Engine-->
		<xsl:call-template name="DIESEL_ENGINE_FACTOR"></xsl:call-template>)
		*<!--Credit Shore Station-->
		<xsl:call-template name="SHORE_STATION_FACTOR"></xsl:call-template>)
		*<!--Credit Loran GPS Factor-->
		<xsl:call-template name="LORAN_GPS_FACTOR"></xsl:call-template>)
		*<!--Credit MultiBoat-->
		<xsl:call-template name="MULTI_BOAT_FACTOR"></xsl:call-template>)
		*<!--Credit Haron Fire Factor-->
		<xsl:call-template name="HALON_FIRE_FACTOR"></xsl:call-template>)
		*<!--Credit 5 or More Years Experience/Coast Guard/Power Squadron-->
		<xsl:call-template name="EXPERIENCE_CREDIT_FACTOR"></xsl:call-template>)
		*<!--Charge Over 26 Feet-->
		<xsl:call-template name="BOAT_FEET"></xsl:call-template>)
		*<!--Charge Over 26 Feet and Speed 65 MPH-->
		<xsl:call-template name="BOAT_FEET_SPEED"></xsl:call-template>)
	</xsl:template>
	
	<xsl:template name="INOUTBOARD_RATE">
		<xsl:variable name="TERRITORY"><xsl:call-template name='TERRITORY'/></xsl:variable>				
		<xsl:variable name="WATERS"><xsl:call-template name='WATERS'/></xsl:variable>						
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASERATES']/NODE[@ID=$WATERS]/ATTRIBUTES[@TERRITORYCODE=$TERRITORY]/@INOUTBOARD"></xsl:value-of>				
	</xsl:template>
	
<!-- ============================================================================================ -->
<!--				Templates for InOutboard, Display (END)										  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
	

<!-- ============================================================================================ -->
<!--				Templates for Sailboat, Display (START)										  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
	
	<xsl:template name="SAILBOAT_RATE_CALC">
		<xsl:variable name="MARKETVALUE"><xsl:call-template name='MARKETVALUE'/></xsl:variable>		
		<xsl:variable name="DED_DESC"><xsl:call-template name='DEDUCTIBLE'/></xsl:variable>
		
		((((((((((((((<xsl:call-template name="SAILBOAT_RATE"/>)
		*
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='RATERELATIVITY']/NODE[@ID='SAILBOAT']/ATTRIBUTES[@COVERAGE=$MARKETVALUE]/@RELATIVITY"></xsl:value-of>)
		*
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID='DEDUCTIBLE']/ATTRIBUTES[@DESC=$DED_DESC]/@FACTOR"></xsl:value-of>)				
		*<!--Multipolicy Credit-->
		<xsl:call-template name="DISCOUNT_WOLVERINE_FACTOR"></xsl:call-template>)
		*<!--Insurance Score-->
		<xsl:call-template name="INSURANCE_SCORE_CALC"></xsl:call-template>)
		*<!--Constructor type Fibre,Water etc..-->
		<xsl:call-template name="CONSTRUCTION_TYPE"></xsl:call-template>)
		*<!--Charge Dual Ownership-->
		<xsl:call-template name="DUAL_OWNERSHIP_FACTOR"></xsl:call-template>)
		*<!--Credit Diesel Engine-->
		<xsl:call-template name="DIESEL_ENGINE_FACTOR"></xsl:call-template>)
		*<!--Credit Shore Station-->
		<xsl:call-template name="SHORE_STATION_FACTOR"></xsl:call-template>)
		*<!--Credit Loran GPS Factor-->
		<xsl:call-template name="LORAN_GPS_FACTOR"></xsl:call-template>)
		*<!--Credit MultiBoat-->
		<xsl:call-template name="MULTI_BOAT_FACTOR"></xsl:call-template>)
		*<!--Credit 5 or More Years Experience/Coast Guard/Power Squadron-->
		<xsl:call-template name="EXPERIENCE_CREDIT_FACTOR"></xsl:call-template>)
		*<!--Charge Over 26 Feet and Speed 65 MPH-->
		<xsl:call-template name="BOAT_FEET_SPEED"></xsl:call-template>)
		*<!--Removal of sailboat -->
		<xsl:call-template name="REMOVE_SAILBOAT_FACTOR"></xsl:call-template>)
		
	</xsl:template>
	
	<xsl:template name="SAILBOAT_RATE">
		<xsl:variable name="TERRITORY"><xsl:call-template name='TERRITORY'/></xsl:variable>				
		<xsl:variable name="WATERS"><xsl:call-template name='WATERS'/></xsl:variable>						
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASERATES']/NODE[@ID=$WATERS]/ATTRIBUTES[@TERRITORYCODE=$TERRITORY]/@SAILBOAT"></xsl:value-of>				
	</xsl:template>	
	
<!-- ============================================================================================ -->
<!--				Templates for Sailboat, Display (END)										  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
	
<!-- ============================================================================================ -->
<!--				Templates for Jetski, Display (START)										  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
	<xsl:template name="JETSKI_RATE_CALC">
		<xsl:variable name ="DED_DESC" select= "BOATS/BOAT/DEDUCTIBLE"></xsl:variable>
			((<xsl:call-template name="MARKETVALUE"/>)
			*
			(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='PDJETSKI']/ATTRIBUTES[@DEDUCTIBLE=$DED_DESC]/@RATE"/>)
			DIV 100 
			* 
			(<xsl:call-template name="INSURANCE_SCORE_CALC"></xsl:call-template>))
	</xsl:template>
	
	<xsl:template name="JETSKI_RATE">
		<xsl:variable name="TERRITORY"><xsl:call-template name="TERRITORY"/></xsl:variable>				
		<xsl:variable name="WATERS"><xsl:call-template name="WATERS"/></xsl:variable>						
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASERATES']/NODE[@ID=$WATERS]/ATTRIBUTES[@TERRITORYCODE=$TERRITORY]/@OUTBOARD"></xsl:value-of>				
	</xsl:template>
	
	<xsl:template name="INSURANCE_SCORE_CALC">
		<xsl:variable name="INS_SCORE"><xsl:call-template name="INSURANCESCORE"></xsl:call-template></xsl:variable>
		<xsl:value-of select="$INS_SCORE"></xsl:value-of>
	</xsl:template>
	
<!-- ============================================================================================ -->
<!--				Templates for Jetski, Display (END)										      -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
	
<!-- ============================================================================================ -->
<!--				Templates for WaveRunner, Display (START)									  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
	<xsl:template name="WAVE_RATE_CALC">
		(((((((<xsl:call-template name="BASE_RATES_WAVERUNNER"></xsl:call-template>)
		*
		<xsl:call-template name="MARKETVALUE_WAVERUNNER"/>)
		*
		<xsl:call-template name="DED_WAVERUNNER"/>)
		*
		1.10)
		*
		<xsl:call-template name="INSURANCE_SCORE_CALC"></xsl:call-template>)
		*<!--Charge Over 26 Feet-->
		<xsl:call-template name="BOAT_FEET"></xsl:call-template>)
		*<!--Charge Over 26 Feet and Speed 65 MPH-->
		<xsl:call-template name="BOAT_FEET_SPEED"></xsl:call-template>)
	
	</xsl:template>
	
	
	<!--Market Value of Waverunner -->
	<xsl:template name="MARKETVALUE_WAVERUNNER">
		<xsl:variable name ="PBOATTYPE" select= "BOATS/BOAT/BOATTYPE"></xsl:variable>
		<xsl:variable name ="PCOVERAGE" select= "BOATS/BOAT/MARKETVALUE"></xsl:variable>
		<xsl:choose>
			<xsl:when test ="BOATS/BOAT/BOATTYPE ='Waverunner'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='RATERELATIVITY']/NODE[@ID='INOUTBOARD']/ATTRIBUTES[@COVERAGE=$PCOVERAGE]/@RELATIVITY"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='RATERELATIVITY']/NODE[@ID=$PBOATTYPE]/ATTRIBUTES[@COVERAGE=$PCOVERAGE]/@RELATIVITY"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	<!--Base Rates values of Waverunner -->
	<xsl:template name ="BASE_RATES_WAVERUNNER">
	    <xsl:variable name ="PBOATTYPE" select= "BOATS/BOAT/BOATTYPE"></xsl:variable>
		<xsl:variable name ="WATERTYPE" select= "BOATS/BOAT/WATERS"></xsl:variable>
		<xsl:variable name ="PTERRITORY" select= "POLICY/TERRITORYCODE"></xsl:variable>
		
			<xsl:choose>
				<xsl:when test ="$PBOATTYPE ='Waverunner'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASERATES']/NODE[@ID=$WATERTYPE]/ATTRIBUTES[@TERRITORYCODE=$PTERRITORY]/@INOUTBOARD"/>
				</xsl:when>
				<xsl:when test ="$PBOATTYPE ='Inboard/Outboard'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASERATES']/NODE[@ID=$WATERTYPE]/ATTRIBUTES[@TERRITORYCODE=$PTERRITORY]/@INOUTBOARD"/>
				</xsl:when>
				<xsl:when test ="$PBOATTYPE ='inboard'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASERATES']/NODE[@ID=$WATERTYPE]/ATTRIBUTES[@TERRITORYCODE=$PTERRITORY]/@INBOARD"/>
				</xsl:when>
				<xsl:when test ="$PBOATTYPE ='Sailboat'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASERATES']/NODE[@ID=$WATERTYPE]/ATTRIBUTES[@TERRITORYCODE=$PTERRITORY]/@SAILBOAT"/>
				</xsl:when>
				<xsl:when test ="$PBOATTYPE ='MINIJET'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASERATES']/NODE[@ID=$WATERTYPE]/ATTRIBUTES[@TERRITORYCODE=$PTERRITORY]/@MINIJET"/>
				</xsl:when>
				<xsl:when test ="$PBOATTYPE ='Outboard'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASERATES']/NODE[@ID=$WATERTYPE]/ATTRIBUTES[@TERRITORYCODE=$PTERRITORY]/@OUTBOARD"/>
				</xsl:when>
				<xsl:otherwise>
				</xsl:otherwise>
			</xsl:choose>
			
	</xsl:template>
	
	<!--Wave runner Deduction-->
	<xsl:template name="DED_WAVERUNNER">
		<xsl:variable name ="DED_DESC" select= "BOATS/BOAT/DEDUCTIBLE"></xsl:variable>
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID='DEDUCTIBLE']/ATTRIBUTES[@DESC = $DED_DESC]/@FACTOR"/>	
	</xsl:template>

<!-- ============================================================================================ -->
<!--				Templates for WaveRunner, Display (END) 									  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
	
<!-- ============================================================================================ -->
<!--				Templates for Sailboat, Display (END)										  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
	
	<xsl:template name="DEDUCTIBLE">
		<xsl:value-of select="BOATS/BOAT/DEDUCTIBLE"></xsl:value-of>
	</xsl:template>
	
	<xsl:template name="WATERS">
		<xsl:value-of select="BOATS/BOAT/WATERS"></xsl:value-of>
	</xsl:template>
	
	<xsl:template name="TERRITORY">
		<xsl:value-of select="POLICY/TERRITORYCODE"></xsl:value-of>
	</xsl:template>
	
	<xsl:template name="BOATTYPE">
		<xsl:value-of select="BOATS/BOAT/BOATTYPE"></xsl:value-of>
	</xsl:template>
	
	<xsl:template name="MARKETVALUE">
		<xsl:value-of select="BOATS/BOAT/MARKETVALUE"></xsl:value-of>
	</xsl:template>
	
	
	
<!-- ============================================================================================ -->
<!--				Templates for Jetski Trailor, Display (START)								  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
	<xsl:template name="JETSKI_TRAILOR_RATE_CALC">
		((<xsl:call-template name="MARKETVALUE"/>)		
		*
		(<xsl:call-template name="JETSKI_TRAIL_DEDUCT"></xsl:call-template>)
		DIV 100 )
	</xsl:template>
	
	<xsl:template name="JETSKI_TRAIL_DEDUCT">
	<xsl:variable name ="DED_DESC" select= "BOATS/BOAT/DEDUCTIBLE"></xsl:variable>
	<!--<xsl:value-of select="$DED_DESC"></xsl:value-of>-->
			<xsl:choose>
				<xsl:when test ="$DED_DESC =250">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='TRAILER']/ATTRIBUTES[@ID='JETSKI']/@DED250"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='TRAILER']/ATTRIBUTES[@ID='JETSKI']/@DED500"/>
				</xsl:otherwise>
			</xsl:choose>
	 		
	</xsl:template>
	
<!-- ============================================================================================ -->
<!--				Templates for Waverunner Trailor, Display (START)							  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->
	<xsl:template name="WAVERUNNER_TRAILOR_RATE_CALC">
		((<xsl:call-template name="MARKETVALUE"/>)		
		*
		(<xsl:call-template name="WAVERUNNER_TRAIL_DEDUCT"></xsl:call-template>)
		DIV 100 )
	</xsl:template>
	
	<xsl:template name="WAVERUNNER_TRAIL_DEDUCT">
	<xsl:variable name ="DED_DESC" select= "BOATS/BOAT/DEDUCTIBLE"></xsl:variable>
	<!--<xsl:value-of select="$DED_DESC"></xsl:value-of>-->
			<xsl:choose>
				<xsl:when test ="$DED_DESC ='1%-100'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='TRAILER']/ATTRIBUTES[@ID='WAVERUNNER']/@DED100"/>
				</xsl:when>
				<xsl:when test ="$DED_DESC ='2%-200'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='TRAILER']/ATTRIBUTES[@ID='WAVERUNNER']/@DED200"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='TRAILER']/ATTRIBUTES[@ID='WAVERUNNER']/@DED500"/>
				</xsl:otherwise>
			</xsl:choose>
	 		
	</xsl:template>
	
<!-- ============================================================================================ -->
<!--				Templates for Physical Damage, Display (END)								  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->


<xsl:template name ="GROUPID1">Boats</xsl:template>
</xsl:stylesheet>
  

