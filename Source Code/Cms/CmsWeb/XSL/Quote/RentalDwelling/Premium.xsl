<!-- ============================================================================================ 
	File Name		:	Primium.xsl																  
	Description		:	Generate the final premium for the Rental Dwelling 
	Developed By	:	Ashwani 
	Date			:   26 Oct.2005	
	Modified By		:	16 Nov 2005													  		
 ============================================================================================ -->
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"  version="1.0">
<xsl:output encoding="ISO8859-1" omit-xml-declaration="no" method="xml"/>
<!-- ============================================================================================ 
								Loading ProductFactorMaster File (START)					  
 ============================================================================================ -->

<xsl:variable name="RDCoveragesDoc" select="document('FactorPath')"></xsl:variable>

<!-- ============================================================================================ 
								Loading ProductFactorMaster File (END)						  
 ============================================================================================ -->

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
	<PRODUCT PRODUCTID="0" DESC="Rental Dwelling"> 
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
				<CONDITION>
				</CONDITION>
			</GROUPCONDITION> 
		</GROUP> 		
		<GROUP GROUPID="4"> 
			<GROUPCONDITION>
				<CONDITION>	</CONDITION>
			</GROUPCONDITION> 
			<SUBGROUP>
				<IF>
					<STEP STEPID="0">
						<PATH>
						{
							<!--<xsl:call-template name ="CALL_ADJUSTEDBASE"/>-->
							<xsl:call-template name ="CALL_ADJUSTEDBASE"/>
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
					<PATH>ND</PATH>
				</STEP>
			</SUBGROUP>
			<SUBGROUP>
				<IF>
					<STEP STEPID="5">
						<PATH>
						{
							<xsl:call-template name = "DUC_DISCOUNT_DISPLAY"/>		
						}
						</PATH>
					</STEP>
				</IF>
			</SUBGROUP>				
			<SUBGROUP>
				<IF>
					<STEP STEPID="6">
						<PATH>
						{
							<xsl:call-template name = "PROTECTIVE_DEVICE_DISCOUNT_DISPLAY"/>		
						}
						</PATH>
					</STEP>
				</IF>
			</SUBGROUP>				
			<SUBGROUP>
				<IF>
					<STEP STEPID="7">
						<PATH>
						{
							<xsl:call-template name = "VALUEDCUSROMER_DISCOUNT_DISPLAY"/>		
						}
						</PATH>
					</STEP>
				</IF>
			</SUBGROUP>	
			<SUBGROUP>
				<IF>
					<STEP STEPID="8">
						<PATH>
						{
							<xsl:call-template name = "AGEOFHOME_DISPLAY"/>	
						}
						</PATH>
					</STEP>
				</IF>
			</SUBGROUP>
			<SUBGROUP>
				<IF>
					<STEP STEPID="9">
						<PATH>
						{
							<xsl:call-template name = "MULTIPOLICY_DISPLAY"/>			
						}	
						</PATH>
					</STEP>
				</IF>
			</SUBGROUP>			
		</GROUP> 
		<GROUP GROUPID="5"> 
			<GROUPCONDITION>
				<CONDITION>	</CONDITION>
			</GROUPCONDITION> 
			<SUBGROUP>
				<IF>
					<STEP STEPID="10">
						<PATH>
						{
							<xsl:call-template name = "LP_124_LANDLORDS_LIABILITY"/>
						}
						</PATH>
					</STEP>
				</IF>
			</SUBGROUP>	
			<SUBGROUP>
				<IF>
					<STEP STEPID="11">
						<PATH>
						{
							<xsl:call-template name = "MEDICAL_PAYMENTS_TO_OTHERS_EACH_PERSON_CREDITDISPLAY"/>		
						}
						</PATH>
					</STEP>
				</IF>
			</SUBGROUP>
			<SUBGROUP>
				<IF>
					<STEP STEPID="12">
						<PATH>
						{
							<xsl:call-template name = "INCIDENTALOFFICE_DISPLAY"/>		
						}
						</PATH>
					</STEP>
				</IF>
			</SUBGROUP>			
		</GROUP>		
		<GROUP GROUPID="6"> 
			<GROUPCONDITION>
				<CONDITION>	</CONDITION>
			</GROUPCONDITION> 
			<SUBGROUP>
				<IF>
					<STEP STEPID="13">
						<PATH>
						{
							<xsl:call-template name ="EARTHQUAKE"/>		
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
							<xsl:call-template name ="MINE_SUBSIDENCE_COVERAGE_PREMIUM"/>	
						}
						</PATH>
					</STEP>
				</IF>
			</SUBGROUP>			
			<SUBGROUP>
				<IF>
					<STEP STEPID="15">
						<PATH>
						{
							<xsl:call-template name ="COVERAGE_B_ADDITIONAL"/>
						}
						</PATH>
					</STEP>
				</IF>
			</SUBGROUP>			
			<SUBGROUP>
				<IF>
					<STEP STEPID="16">
						<PATH>
						{
							<xsl:call-template name ="BUILDING_IMPROVEMENTS_ADDITIONAL"/>	
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
							<xsl:call-template name ="COVERAGE_C_ADDITIONAL"/>
						}
						</PATH>
					</STEP>
				</IF>
			</SUBGROUP>	
			<SUBGROUP>
				<IF>
					<STEP STEPID="18">
						<PATH>
						{
							<xsl:call-template name ="INCREASED_COVERAGE_D_ADDITIONAL"/>		
						}
						</PATH>
					</STEP>
				</IF>
			</SUBGROUP>	
			<SUBGROUP>
				<IF>
					<STEP STEPID="19">
						<PATH>
						{
							<xsl:call-template name ="CONTENTS_IN_STORAGE_ADDITIONAL"/>
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
							<xsl:call-template name ="TREES_LAWNS_AND_SHRUBS_ADDITIONAL"/>
						}
						</PATH>
					</STEP>
				</IF>
			</SUBGROUP>	
			<SUBGROUP>
				<IF>
					<STEP STEPID="21">
						<PATH>
						{
							<xsl:call-template name ="RADIO_AND_TV_EQUIPMENT_ADDITIONAL"/>		
						}
						</PATH>
					</STEP>
				</IF>
			</SUBGROUP>	
			<SUBGROUP>
				<IF>
					<STEP STEPID="22">
						<PATH>
						{
							<xsl:call-template name ="SATELLITE_DISHES_ADDITIONAL"/>			
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
							<xsl:call-template name ="AWNINGS_AND_CANOPIES_ADDITIONAL"/>		
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


							<xsl:call-template name ="INSTALLATION_FLOATER_IF_184_BUILDING_MATERIALS"/>				
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
							<xsl:call-template name ="INSTALLATION_FLOATER_IF_184_NON_STRUCTURAL_EQUIPMENT"/>		
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
							<xsl:call-template name ="PROPFEE"/>		
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
							<xsl:call-template name ="FINALPRIMIUM"/>		
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
				<xsl:call-template name="PRODUCTID0"/>
			</xsl:attribute> 		
			<GROUP GROUPID="0"  CALC_ID="10000" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC">
					<xsl:call-template name="GROUPID0"/>
				</xsl:attribute> 
				<GROUPCONDITION><CONDITION/></GROUPCONDITION> 
				<GROUPRULE/>
				<GROUPFORMULA/>
				<GDESC/>
			</GROUP>						 	
			<GROUP GROUPID="1"  CALC_ID="10001" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC">
					<xsl:call-template name="GROUPID1"/>
				</xsl:attribute> 
				<GROUPCONDITION><CONDITION/></GROUPCONDITION> 
				<GROUPRULE/>
				<GROUPFORMULA/>
				<GDESC/>
			</GROUP>			 
			<GROUP GROUPID="2"  CALC_ID="10002" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC">
					<xsl:call-template name="GROUPID2"/>
				</xsl:attribute> 
				<GROUPCONDITION><CONDITION/></GROUPCONDITION> 
				<GROUPRULE/>
				<GROUPFORMULA/>
				<GDESC/>
			</GROUP>			
			<GROUP GROUPID="3"  CALC_ID="10003" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC">
					<xsl:call-template name="GROUPID3"/>
				</xsl:attribute> 
				<GROUPCONDITION><CONDITION/></GROUPCONDITION> 
				<GROUPRULE/>
				<GROUPFORMULA/>
				<GDESC/>
			</GROUP>						 	
			<GROUP GROUPID="4"  CALC_ID="10004" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC">
					<xsl:call-template name="GROUPID4"/>
				</xsl:attribute> 
				<GROUPCONDITION><CONDITION/></GROUPCONDITION> 	
				<GROUPRULE/>
				<GDESC/>
				<STEP STEPID="0"  CALC_ID="1000" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID0"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH><xsl:value-of select ="DEDUCTIBLE"/></D_PATH>
					<L_PATH><xsl:value-of select ="REPLACEMENTCOSTFACTOR"/></L_PATH>
				</STEP>
				<STEP STEPID="1"  CALC_ID="1001" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID1"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH><xsl:value-of select ="DEDUCTIBLE"/></D_PATH>
					<L_PATH><xsl:value-of select ="APPURTENANTSTRUCTURES_INCLUDE"/></L_PATH>
				</STEP>				
				<STEP STEPID="2"  CALC_ID="1002" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID2"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH><xsl:value-of select ="DEDUCTIBLE"/></D_PATH>
					<L_PATH><xsl:value-of select ="PERSONALPROPERTY_INCLUDE"/></L_PATH>
				</STEP>				
				<STEP STEPID="3"  CALC_ID="1003" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID3"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH><xsl:value-of select ="DEDUCTIBLE"/></D_PATH>
					<L_PATH><xsl:value-of select ="PERSONALPROPERTY_INCLUDE"/></L_PATH>
				</STEP>					
				<STEP STEPID="4"  CALC_ID="1004" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID4"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>
				<STEP STEPID="5"  CALC_ID="1005" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID5"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>
				<STEP STEPID="6"  CALC_ID="1006" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID6"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>
				<STEP STEPID="7"  CALC_ID="1007" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID7"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>
				<STEP STEPID="8"  CALC_ID="1008" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID8"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>
				<STEP STEPID="9"  CALC_ID="1009" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID9"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
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
			<GROUP GROUPID="5"  CALC_ID="10005" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC">
					<xsl:call-template name="GROUPID5"/>
				</xsl:attribute> 
				<GROUPCONDITION><CONDITION/></GROUPCONDITION> 	
				<GROUPRULE/>
				<GDESC/>				
				
				<STEP STEPID="10"  CALC_ID="10010" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID10"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH></D_PATH>
					<L_PATH><xsl:call-template name="LIABILITY_EACH_OCCURRENCE_DISPLAY"/></L_PATH>			
				</STEP>								
				<GROUPFORMULA>
					<OPERAND TOREPLACE="Y">@[CALC_ID=1006]</OPERAND> 
					<OPERATOR>*</OPERATOR> 
					<OPERAND TOREPLACE="Y">@[CALC_ID=1007]</OPERAND> 
					<OPERATOR>*</OPERATOR> 
					<OPERAND TOREPLACE="Y">@[CALC_ID=1008]</OPERAND>					
				</GROUPFORMULA> 
				<STEP STEPID="11"  CALC_ID="1011" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID11"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH></D_PATH>
					<L_PATH><xsl:call-template name="MEDICAL_PAYMENTS_TO_OTHERS_EACH_PERSON_DISPLAY"/></L_PATH>									
				</STEP>
				<STEP STEPID="12" CALC_ID="1012" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID12"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>				
			</GROUP>		
			<GROUP GROUPID="6"  CALC_ID="10006" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
				<xsl:attribute name="DESC">
					<xsl:call-template name="GROUPID6"/>
				</xsl:attribute> 
				<GROUPCONDITION><CONDITION/></GROUPCONDITION> 	
				<GROUPRULE/>
				<GDESC/>				
				
				<STEP STEPID="13"  CALC_ID="1013" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID13"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>							
				<STEP STEPID="14"  CALC_ID="1014" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID14"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>								
				<STEP STEPID="15"  CALC_ID="1015" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID15"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH><xsl:value-of select ="DEDUCTIBLE"/></D_PATH>
					<L_PATH><xsl:value-of select ="APPURTENANTSTRUCTURES_ADDITIONAL"/></L_PATH>
				</STEP>								
				<STEP STEPID="16"  CALC_ID="1016" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID16"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH><xsl:value-of select ="DEDUCTIBLE"/></D_PATH>
					<L_PATH><xsl:value-of select ="BUILDINGIMPROVEMENTS_ADDITIONAL"/></L_PATH>		
				</STEP>
				<STEP STEPID="17"  CALC_ID="1017" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID17"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH><xsl:value-of select ="DEDUCTIBLE"/></D_PATH>
					<L_PATH><xsl:value-of select ="PERSONALPROPERTY_ADDITIONAL"/></L_PATH>					
				</STEP>
				<STEP STEPID="18"  CALC_ID="1018" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID18"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH><xsl:value-of select ="DEDUCTIBLE"/></D_PATH>
					<L_PATH><xsl:value-of select ="RENTALVALUE_ADDITIONAL"/></L_PATH>			
				</STEP>
				<STEP STEPID="19"  CALC_ID="1019" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID19"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH><xsl:value-of select ="DEDUCTIBLE"/></D_PATH>			
					<L_PATH><xsl:value-of select ="CONTENTSINSTORAGE_ADDITIONAL"/></L_PATH>					
				</STEP>
				<STEP STEPID="20"  CALC_ID="1020" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID20"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH><xsl:value-of select ="DEDUCTIBLE"/></D_PATH>			
					<L_PATH><xsl:value-of select ="TREESLAWNSSHRUBS_ADDITIONAL"/></L_PATH>					
				</STEP>
				<STEP STEPID="21"  CALC_ID="1021" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID21"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH><xsl:value-of select ="DEDUCTIBLE"/></D_PATH>			
					<L_PATH><xsl:value-of select ="RADIOTV_ADDITIONAL"/></L_PATH>				
				</STEP>
				<STEP STEPID="22"  CALC_ID="1022" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID22"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH><xsl:value-of select ="DEDUCTIBLE"/></D_PATH>			
					<L_PATH><xsl:value-of select ="SATELLITEDISHES_ADDITIONAL"/></L_PATH>					
				</STEP>
				<STEP STEPID="23"  CALC_ID="1023" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID23"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH><xsl:value-of select ="DEDUCTIBLE"/></D_PATH>				
					<L_PATH><xsl:value-of select ="AWNINGSCANOPIES_ADDITIONAL"/></L_PATH>					
				</STEP>
				<STEP STEPID="24"  CALC_ID="1024" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID24"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH>500</D_PATH>
					<L_PATH><xsl:value-of select ="FLOATERBUILDINGMATERIALS_ADDITIONAL"/></L_PATH>									
				</STEP>
				<STEP STEPID="25" CALC_ID="1025" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID25"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH>500</D_PATH>
					<L_PATH><xsl:value-of select ="FLOATERNONSTRUCTURAL_ADDITIONAL"/></L_PATH>
				</STEP>		
				<STEP STEPID="26" CALC_ID="1026" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID26"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP>	
				<STEP STEPID="27" CALC_ID="1027" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC">
						<xsl:call-template name ="STEPID27"/>
					</xsl:attribute>
					<PATH>P</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
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

<!-- ========================================================================================================= 
									Templates for Lables  (START)							  
 ============================================================================================================= -->
 <!-- ===============================   Group Details	====================================================== -->       
 
<xsl:template name ="PRODUCTID0"><xsl:value-of select ="PRODUCTNAME"/> - <xsl:value-of select ="PRODUCT_PREMIER"/> ( Terrotery : <xsl:value-of select ="TERRITORYNAME"/>[<xsl:value-of select ="TERRITORYCODES"/>])</xsl:template>

<xsl:template name ="GROUPID0">Policy Form: DP-2 Repair Cost,2 Family</xsl:template>
<xsl:template name ="GROUPID1">Seasonal, Mixed 33 Frame) (<xsl:value-of select ="DOC"/>)</xsl:template>
<xsl:template name ="GROUPID2">Fire Class: 02, Hydrant: <xsl:value-of select ="FEET2HYDRANT"/>, Fire Department: <xsl:value-of select ="DISTANCET_FIRESTATION"/> miles</xsl:template>
<xsl:template name ="GROUPID3">Premium Group: 7, Rated Class: <xsl:value-of select ="PROTECTIONCLASS"/></xsl:template>
<xsl:template name ="GROUPID4">SECTION I - PROPERTY DAMAGE</xsl:template>
<xsl:template name ="GROUPID5">SECTION II - LIABILITY COVERAGE (LP-124)</xsl:template>
<xsl:template name ="GROUPID6">ADDITIONAL COVERAGES</xsl:template>
<xsl:template name ="GROUPID7">Final Premium</xsl:template>
<!--Step Details-->
<xsl:template name = "STEPID0">		-  Coverage A - Dwelling</xsl:template>
<xsl:template name = "STEPID1">		-  Coverage B - Other Structures</xsl:template>
<xsl:template name = "STEPID2">		-  Coverage C - Landlords Personal Property</xsl:template>
<xsl:template name = "STEPID3">		-  Coverage D - Rental Value</xsl:template>
<xsl:template name = "STEPID4">
	<xsl:choose>
		<xsl:when test ="REPLACEMENTCOSTFACTOR = DWELLING_LIMITS">
			-    Insured to 100 of Replacement Cost (<xsl:value-of select ="REPLACEMENTCOSTFACTOR"/>)
		</xsl:when>
		<xsl:otherwise>
			-    Insured to 100 of Replacement Cost (<xsl:value-of select ="REPLACEMENTCOSTFACTOR"/>)
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<xsl:template name = "STEPID5">		-    Discount - Under Construction <xsl:call-template name="DUC_DISCOUNT_CREDIT"/>%</xsl:template>
<xsl:template name = "STEPID6">		-    Discount - Protective Devices <xsl:call-template name="PROTECTIVE_DEVICE_DISCOUNT_CREDIT"/>%</xsl:template>
<xsl:template name = "STEPID7">		-    Discount - Valued Customer <xsl:call-template name="VALUEDCUSROMER_DISCOUNT_CREDIT"/>%</xsl:template>
<xsl:template name = "STEPID8">		-    Discount - New Home <xsl:call-template name="AGEOFHOME_CREDIT"/>%</xsl:template>
<xsl:template name = "STEPID9">		-    Discount - Multi Policy <xsl:call-template name="MULTIPOLICY_CREDIT"/>%</xsl:template>
<xsl:template name = "STEPID10">		- Liability - Each Occurrence</xsl:template>
<xsl:template name = "STEPID11">		- Medical Payments to Others - Each Person</xsl:template>
<xsl:template name = "STEPID12">	-  With Incidental Office Occupancy (By Insured Only)</xsl:template>
<xsl:template name = "STEPID13">	-  DP-470 Earthquake (Zone <xsl:value-of select ="TERRITORYZONE"/>)</xsl:template>
<xsl:template name = "STEPID14">	-  Mine Subsidence Coverage</xsl:template>
<xsl:template name = "STEPID15">	-  Increased Coverage B - Other Structures</xsl:template>
<xsl:template name = "STEPID16">	-  Building Improvements/Alteration</xsl:template>
<xsl:template name = "STEPID17">	-  Increased Coverage C - Landlords Personal Property</xsl:template>
<xsl:template name = "STEPID18">	-  Increased Coverage D - Rental Value</xsl:template>
<xsl:template name = "STEPID19">	-  Contents In Storage</xsl:template>
<xsl:template name = "STEPID20">	-  Trees, Lawns, and Shrubs</xsl:template>
<xsl:template name = "STEPID21">	-  Radio and TV Equipment</xsl:template>
<xsl:template name = "STEPID22">	-  Satellite Dishes</xsl:template>
<xsl:template name = "STEPID23">	-  Awnings and Canopies</xsl:template>
<xsl:template name = "STEPID24">	-  Installation Floater - Building Materials</xsl:template>
<xsl:template name = "STEPID25">	-  Installation Floater - Non-Structural Equipment</xsl:template>
<xsl:template name = "STEPID26">	-  Property Expense Recoupment Charge</xsl:template>
<xsl:template name = "STEPID27">	-  Final Premium</xsl:template>

<!--========================================================================================================== 
									Template for Lables  (END)								  
 ============================================================================================================== --> 

<!-- ============================================================================================ -->
<!--								Base Premium Groups Template(START)								  -->
<!--								  FOR MIcHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="DEWLLING-MAIN">	
	<xsl:variable name="TERCODES" select="TERRITORYCODES"/>
	<xsl:variable name="GROUP_ID" select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY']/NODE[@ID='TERRGROUPING']/ATTRIBUTES[@TID = $TERCODES]/@GROUPID"/>			
	
	<xsl:variable name="F_CODE" select="FORM_CODE"/>
	<xsl:variable name="FORMGROUP">
		<xsl:choose>
			<xsl:when test ="TERRITORYCODES = 1">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING-1']/ATTRIBUTES[@FORMID = $F_CODE]/@GROUPID"/>
			</xsl:when>
			<xsl:when test ="TERRITORYCODES = 2">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING-2']/ATTRIBUTES[@FORMID = $F_CODE]/@GROUPID"/>
			</xsl:when>
			<xsl:otherwise>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:variable> 
	<xsl:variable name="PCODE" select="PRODUCTNAME"/>
	
	<xsl:choose>
		<xsl:when test ="TERRITORYCODES = 1">
			IF('<xsl:value-of select="$FORMGROUP" />'='1')
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1"/>			
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='2')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code2"/>
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='3')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code3"/>		
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='4')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code4"/>
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='5')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code5"/>
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='6')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code6"/>
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='7')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code7"/>
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='8')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code8"/>
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='9')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code9"/>
			ELSE 1.00
		</xsl:when>
		<xsl:when test ="TERRITORYCODES = 2">
			IF('<xsl:value-of select="$FORMGROUP" />'='10')
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1"/>			
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='11')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code2"/>
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='12')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code3"/>		
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='13')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code4"/>
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='14')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code5"/>
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='15')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code6"/>
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='16')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code7"/>
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='17')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code8"/>
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='18')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code9"/>
			ELSE 1.00
		</xsl:when>
		<xsl:otherwise>1.00</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="DEWLLING-MAIN_INDIANA">	
	<xsl:variable name="TERCODES" select="TERRITORYCODES"/>
	<xsl:variable name="GROUP_ID" select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY']/NODE[@ID='TERRGROUPING']/ATTRIBUTES[@TID = $TERCODES]/@GROUPID"/>			
	
	<xsl:variable name="F_CODE" select="FORM_CODE"/>
	<xsl:variable name="FORMGROUP">
		<xsl:choose>
			<xsl:when test ="TERRITORYCODES = 1">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING-1']/ATTRIBUTES[@FORMID = $F_CODE]/@GROUPID"/>
			</xsl:when>
			<xsl:when test ="TERRITORYCODES = 2">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING-2']/ATTRIBUTES[@FORMID = $F_CODE]/@GROUPID"/>
			</xsl:when>
			<xsl:otherwise>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:variable> 
	<xsl:variable name="PCODE" select="PRODUCTNAME"/>
	
	<xsl:choose>
		<xsl:when test ="TERRITORYCODES = 1">
			IF('<xsl:value-of select="$FORMGROUP" />'='1')
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1"/>			
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='2')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code2"/>
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='3')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code3"/>		
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='4')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code4"/>
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='5')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code5"/>
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='6')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code6"/>
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='7')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code7"/>
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='8')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code8"/>
			ELSE 1.00
		</xsl:when>
		<xsl:when test ="TERRITORYCODES = 2">
			IF('<xsl:value-of select="$FORMGROUP" />'='9')
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1"/>			
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='10')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code2"/>
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='11')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code3"/>		
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='12')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code4"/>
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='13')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code5"/>
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='14')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code6"/>
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='15')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code7"/>
			ELSE IF ('<xsl:value-of select="$FORMGROUP" />'='16')	
			THEN <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code8"/>
			ELSE 1.00
		</xsl:when>
		<xsl:otherwise>1.00</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<xsl:template name="C_VALUE">
	<xsl:variable name="CVALUE" select="COVERAGEVALUE"/> 
	<xsl:choose>
		<xsl:when test ="FORM_CODE = '1F'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '2F'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '3F'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '4F'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '5F'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '6F'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '7F'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G2']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '8F'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G2']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '9F'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G2']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '10F'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G2']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '1M'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '2M'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '3M'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '4M'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '5M'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '6M'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '7M'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '8M'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '9M'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:when test ="FORM_CODE = '10M'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G2']/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
		</xsl:when>
		<xsl:otherwise>
		1.00
		</xsl:otherwise>
	</xsl:choose>
	
</xsl:template>
<!-- ============================================================================================ -->
<!--								Base Premium Template(END)									  -->
<!--								  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--					Templates for Replacement Cost [Factor,Credit,Display](SATRT)			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- In the case of Premium Quote HO Factor will be 1-->
<xsl:template name="REPLACEMENT_COST">
	<xsl:choose>
		<xsl:when test="STATENAME = 'INDIANA' and  DWELLING_LIMITS = REPLACEMENTCOSTFACTOR ">
		0.85
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


<!-- ============================================================================================ -->
<!--					Templates for Replacement Cost [Factor,Credit,Display](end)				  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name ="TERM_FACTOR">
1.00
</xsl:template>
<xsl:template name ="NO_OF_FALILY_UNITS_DWELLINGS_FACTOR">
1.00
</xsl:template>
<!-- ============================================================================================ -->
<!--						Templates for Discount - Deductible Factor (START)					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="DFACTOR">	
	<xsl:variable name="DEDUCTIBLEAMT" select="DEDUCTIBLE"/>
	<xsl:choose>
		<xsl:when test = "DEDUCTIBLE >= 500">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLES']/NODE[@ID ='OPTIONALDEDUCTIBLE']/ATTRIBUTES[@AMOUNT=$DEDUCTIBLEAMT]/@FACTOR"/>
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
					<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/ATTRIBUTES[@AGE = $AGE_OF_HOME]/@FACTOR"/> 
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
		<xsl:when test = "AGEOFHOME &gt; 20 and STATENAME = 'MISHIGAN'">
		1.00
		</xsl:when>
		<xsl:when test = "AGEOFHOME &gt; 10 and STATENAME = 'INDIANA'">
		1.00
		</xsl:when>
		<xsl:otherwise>
			<xsl:choose>
				<xsl:when test = "CONSTRUCTIONCREDIT = 'N'">
					<xsl:variable name="AGE_OF_HOME" select="AGEOFHOME"/>
					<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/ATTRIBUTES[@AGE = $AGE_OF_HOME]/@CREDIT"/> 
				</xsl:when>
				<xsl:otherwise>
					0.00
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
					'Applied'		
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
<!--		Templates for Discount - Protective Devices  [Factor,Credit,Display]  (START)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name = "PROTECTIVE_DEVICE_DISCOUNT_FACTOR">
	<xsl:choose>
		<xsl:when test ="N0_LOCAL_ALARM = 1 and DWELL_UND_CONSTRUCTION_DP1143 = 'N'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='PROTECTIVEDEVICES']/ATTRIBUTES[@ID = 'OLSA']/@FACTOR"/>
		</xsl:when>
		<xsl:when test ="N0_LOCAL_ALARM &gt; 1 and DWELL_UND_CONSTRUCTION_DP1143 = 'N'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='PROTECTIVEDEVICES']/ATTRIBUTES[@ID = 'LFA']/@FACTOR"/>
		</xsl:when>
		<xsl:otherwise>
		1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name = "PROTECTIVE_DEVICE_DISCOUNT_CREDIT">
	<xsl:choose>
		<xsl:when test ="N0_LOCAL_ALARM = 1 and DWELL_UND_CONSTRUCTION_DP1143 = 'N'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='PROTECTIVEDEVICES']/ATTRIBUTES[@ID = 'OLSA']/@CREDIT"/>
		</xsl:when>
		<xsl:when test ="N0_LOCAL_ALARM &gt; 1 and DWELL_UND_CONSTRUCTION_DP1143 = 'N'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='PROTECTIVEDEVICES']/ATTRIBUTES[@ID = 'LFA']/@CREDIT"/>
		</xsl:when>
		<xsl:otherwise>
		0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name = "PROTECTIVE_DEVICE_DISCOUNT_DISPLAY">
	<xsl:choose>
		<xsl:when test ="N0_LOCAL_ALARM = 1 and DWELL_UND_CONSTRUCTION_DP1143 = 'N'">
			'Applied'
		</xsl:when>
		<xsl:when test ="N0_LOCAL_ALARM &gt; 1 and DWELL_UND_CONSTRUCTION_DP1143 = 'N'">
			'Applied'
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--		Templates for Discount - Protective Devices  [Factor,Credit,Display]  (END)			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--	Templates for Discount -  Discount - Under Construction [Factor,Credit,Display](START)	  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name = "DUC_DISCOUNT">
	<xsl:choose>
		<xsl:when test ="DWELL_UND_CONSTRUCTION_DP1143 = 'Y'">
			0.55
		</xsl:when>
		<xsl:otherwise>
			1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name = "DUC_DISCOUNT_DISPLAY">
	<xsl:choose>
		<xsl:when test ="DWELL_UND_CONSTRUCTION_DP1143 = 'Y'">
		'Applied'
		</xsl:when>
		<xsl:otherwise>
		0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name = "DUC_DISCOUNT_CREDIT">
	<xsl:choose>
		<xsl:when test ="DWELL_UND_CONSTRUCTION_DP1143 = 'Y'">
		45
		</xsl:when>
		<xsl:otherwise>
		0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--	Templates for Discount -  Discount - Under Construction [Factor,Credit,Display](END)	  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--		Templates for Discount - Multi policy Factor [Factor,Credit,Display]  (START)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="MULTIPOLICY_DISCOUNT_FACTOR">	
	<xsl:choose>
		<xsl:when test="MULTIPLEPOLICYFACTOR = 'Y'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MULTIPOLICY']/ATTRIBUTES/@FACTOR"/> 		
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
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MULTIPOLICY']/ATTRIBUTES/@CREDIT"/> 		
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
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--	   Templates for Liability - Each Occurrence - Each Person [Factor,Credit,Display] START) -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="LIABILITY_EACH_OCCURRENCE_DISPLAY">
	<xsl:choose>
		<xsl:when test ="PERSONALLIABILITY_LIMIT = 'No Coverage'">0</xsl:when>
		<xsl:otherwise>
			<xsl:value-of select="PERSONALLIABILITY_LIMIT"/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name ="LP_124_LANDLORDS_LIABILITY">
	<xsl:variable name ="P_PERSONALLIABILITY_LIMIT">
		<xsl:choose>
			<xsl:when test ="PERSONALLIABILITY_LIMIT = 'No Coverage'">0</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="PERSONALLIABILITY_LIMIT"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:variable>
	<xsl:variable name ="P_MEDICALPAYMENTSTOOTHERS_LIMIT">
		<xsl:choose>
			<xsl:when test ="MEDICALPAYMENTSTOOTHERS_LIMIT = 'No Coverage'">0</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="MEDICALPAYMENTSTOOTHERS_LIMIT"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:variable>
	
<!--<xsl:value-of select="NUMBEROFFAMILIES"/>-->
	IF(<xsl:value-of select="$P_PERSONALLIABILITY_LIMIT"/> = 0 AND <xsl:value-of select="$P_MEDICALPAYMENTSTOOTHERS_LIMIT"/> = 0) THEN
		0.00
	ELSE IF(<xsl:value-of select="$P_PERSONALLIABILITY_LIMIT"/> = 50000 AND <xsl:value-of select="$P_MEDICALPAYMENTSTOOTHERS_LIMIT"/> = 1000 AND <xsl:value-of select="NUMBEROFFAMILIES"/> = 1) THEN
		 (<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDSLIABILITY']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES[@NAME='1_FAMILY']/@LIABILITY_LIMIT_G1"/>)
	ELSE IF(<xsl:value-of select="$P_PERSONALLIABILITY_LIMIT"/> = 50000 AND <xsl:value-of select="$P_MEDICALPAYMENTSTOOTHERS_LIMIT"/> &gt; 1000 AND <xsl:value-of select="NUMBEROFFAMILIES"/> = 1) THEN
		 (<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDSLIABILITY']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES[@NAME='1_FAMILY']/@LIABILITY_LIMIT_G1"/>)
		 + ((((<xsl:value-of select="$P_MEDICALPAYMENTSTOOTHERS_LIMIT"/>)-1000) DIV 1000) * (<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDSLIABILITY']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES[@NAME='1_FAMILY']/@FOR_EACH_ADDITION"/>))
	ELSE IF(<xsl:value-of select="$P_PERSONALLIABILITY_LIMIT"/> = 50000 AND <xsl:value-of select="$P_MEDICALPAYMENTSTOOTHERS_LIMIT"/> &gt; 1000 AND <xsl:value-of select="NUMBEROFFAMILIES"/> &gt; 1) THEN
		(<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDSLIABILITY']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES[@NAME='1_FAMILY']/@LIABILITY_LIMIT_G1"/>)
		+ ((((<xsl:value-of select="$P_MEDICALPAYMENTSTOOTHERS_LIMIT"/>)-1000) DIV 1000) * (<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDSLIABILITY']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES[@NAME='1_FAMILY']/@FOR_EACH_ADDITION"/>))
		+ ((<xsl:value-of select="NUMBEROFFAMILIES"/>) * (<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDSLIABILITY']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES[@NAME='EAADLFAMILY']/@LIABILITY_LIMIT_G1"/>))
	ELSE IF(<xsl:value-of select="$P_PERSONALLIABILITY_LIMIT"/> = 100000 AND <xsl:value-of select="$P_MEDICALPAYMENTSTOOTHERS_LIMIT"/> = 1000 AND <xsl:value-of select="NUMBEROFFAMILIES"/> = 1) THEN
		(<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDSLIABILITY']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES[@NAME='1_FAMILY']/@LIABILITY_LIMIT_G2"/>)
	ELSE IF(<xsl:value-of select="$P_PERSONALLIABILITY_LIMIT"/> = 100000 AND <xsl:value-of select="$P_MEDICALPAYMENTSTOOTHERS_LIMIT"/> &gt; 1000 AND <xsl:value-of select="NUMBEROFFAMILIES"/> = 1) THEN
		(<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDSLIABILITY']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES[@NAME='1_FAMILY']/@LIABILITY_LIMIT_G2"/>)
		+ ((((<xsl:value-of select="$P_MEDICALPAYMENTSTOOTHERS_LIMIT"/>)-1000) DIV 1000) * (<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDSLIABILITY']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES[@NAME='1_FAMILY']/@FOR_EACH_ADDITION"/>))
	ELSE IF(<xsl:value-of select="$P_PERSONALLIABILITY_LIMIT"/> = 100000 AND <xsl:value-of select="$P_MEDICALPAYMENTSTOOTHERS_LIMIT"/> &gt; 1000 AND <xsl:value-of select="NUMBEROFFAMILIES"/> &gt; 1) THEN
		(<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDSLIABILITY']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES[@NAME='1_FAMILY']/@LIABILITY_LIMIT_G2"/>)
		+ ((((<xsl:value-of select="$P_MEDICALPAYMENTSTOOTHERS_LIMIT"/>)-1000) DIV 1000) * (<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDSLIABILITY']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES[@NAME='1_FAMILY']/@FOR_EACH_ADDITION"/>))
		+ ((<xsl:value-of select="NUMBEROFFAMILIES"/>) * (<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDSLIABILITY']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES[@NAME='EAADLFAMILY']/@LIABILITY_LIMIT_G2"/>))
	ELSE IF(<xsl:value-of select="$P_PERSONALLIABILITY_LIMIT"/> = 300000 AND <xsl:value-of select="$P_MEDICALPAYMENTSTOOTHERS_LIMIT"/> = 1000 AND <xsl:value-of select="NUMBEROFFAMILIES"/> = 1) THEN
		 (<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDSLIABILITY']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES[@NAME='1_FAMILY']/@LIABILITY_LIMIT_G3"/>)
	ELSE IF(<xsl:value-of select="$P_PERSONALLIABILITY_LIMIT"/> = 300000 AND <xsl:value-of select="$P_MEDICALPAYMENTSTOOTHERS_LIMIT"/> &gt; 1000 AND <xsl:value-of select="NUMBEROFFAMILIES"/> = 1) THEN
		 (<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDSLIABILITY']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES[@NAME='1_FAMILY']/@LIABILITY_LIMIT_G3"/>)
		 + ((((<xsl:value-of select="$P_MEDICALPAYMENTSTOOTHERS_LIMIT"/>)-1000) DIV 1000) * (<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDSLIABILITY']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES[@NAME='1_FAMILY']/@FOR_EACH_ADDITION"/>))
	ELSE IF(<xsl:value-of select="$P_PERSONALLIABILITY_LIMIT"/> = 300000 AND <xsl:value-of select="$P_MEDICALPAYMENTSTOOTHERS_LIMIT"/> &gt; 1000 AND <xsl:value-of select="NUMBEROFFAMILIES"/> &gt; 1) THEN
		(<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDSLIABILITY']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES[@NAME='1_FAMILY']/@LIABILITY_LIMIT_G3"/>)
		+ ((((<xsl:value-of select="$P_MEDICALPAYMENTSTOOTHERS_LIMIT"/>)-1000) DIV 1000) * (<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDSLIABILITY']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES[@NAME='1_FAMILY']/@FOR_EACH_ADDITION"/>))
		+ ((<xsl:value-of select="NUMBEROFFAMILIES"/>) * (<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDSLIABILITY']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES[@NAME='EAADLFAMILY']/@LIABILITY_LIMIT_G3"/>))
	ELSE IF(<xsl:value-of select="$P_PERSONALLIABILITY_LIMIT"/> = 500000 AND <xsl:value-of select="$P_MEDICALPAYMENTSTOOTHERS_LIMIT"/> = 1000 AND <xsl:value-of select="NUMBEROFFAMILIES"/> = 1) THEN
		 (<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDSLIABILITY']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES[@NAME='1_FAMILY']/@LIABILITY_LIMIT_G4"/>)
	ELSE IF(<xsl:value-of select="$P_PERSONALLIABILITY_LIMIT"/> = 500000 AND <xsl:value-of select="$P_MEDICALPAYMENTSTOOTHERS_LIMIT"/> &gt; 1000 AND <xsl:value-of select="NUMBEROFFAMILIES"/> = 1) THEN
		 (<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDSLIABILITY']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES[@NAME='1_FAMILY']/@LIABILITY_LIMIT_G4"/>)
		 + ((((<xsl:value-of select="$P_MEDICALPAYMENTSTOOTHERS_LIMIT"/>)-1000) DIV 1000) * (<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDSLIABILITY']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES[@NAME='1_FAMILY']/@FOR_EACH_ADDITION"/>))
	ELSE IF(<xsl:value-of select="$P_PERSONALLIABILITY_LIMIT"/> = 500000 AND <xsl:value-of select="$P_MEDICALPAYMENTSTOOTHERS_LIMIT"/> &gt; 1000 AND <xsl:value-of select="NUMBEROFFAMILIES"/> &gt; 1) THEN
		(<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDSLIABILITY']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES[@NAME='1_FAMILY']/@LIABILITY_LIMIT_G4"/>)
		+ ((((<xsl:value-of select="$P_MEDICALPAYMENTSTOOTHERS_LIMIT"/>)-1000) DIV 1000) * (<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDSLIABILITY']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES[@NAME='1_FAMILY']/@FOR_EACH_ADDITION"/>))
		+ ((<xsl:value-of select="NUMBEROFFAMILIES"/>) * (<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDSLIABILITY']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES[@NAME='EAADLFAMILY']/@LIABILITY_LIMIT_G4"/>))
	ELSE
		0.00
</xsl:template>
<!-- ============================================================================================ -->
<!--	   Templates for Liability - Each Occurrence - Each Person [Factor,Credit,Display] END)   -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--		Templates for Medical Payments to Others - Each Person [Factor,Credit,Display] START) -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="MEDICAL_PAYMENTS_TO_OTHERS_EACH_PERSON_DISPLAY">
	<xsl:choose>
		<xsl:when test ="MEDICALPAYMENTSTOOTHERS_LIMIT = 'No Coverage'">0</xsl:when>
		<xsl:otherwise>
			<xsl:value-of select="MEDICALPAYMENTSTOOTHERS_LIMIT"/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="MEDICAL_PAYMENTS_TO_OTHERS_EACH_PERSON_CREDITDISPLAY">
IF(<xsl:call-template name="LIABILITY_EACH_OCCURRENCE_DISPLAY"/> &gt; 1)THEN
	<xsl:choose>
		<xsl:when test ="MEDICALPAYMENTSTOOTHERS_LIMIT = 'No Coverage'">0</xsl:when>
		<xsl:otherwise>
			'Included'
		</xsl:otherwise>
	</xsl:choose>
ELSE
	0
</xsl:template>
<!-- ============================================================================================ -->
<!--		Templates for Medical Payments to Others - Each Person [Factor,Credit,Display]  (END) -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--		Templates for With Incidental Office Occupancy (By Insured Only) [Display]  (START)  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="INCIDENTALOFFICE_DISPLAY">
	<xsl:choose>
		<xsl:when test ="INCIDENTALOFFICE = 'Y'">
		'Included'
		</xsl:when>
		<xsl:otherwise>
		0
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--		Templates for With Incidental Office Occupancy (By Insured Only) [Display]  (END)	  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<!-- ============================================================================================ -->
<!--			Templates for Discount - VALUED CUSTOMER [Factor,Credit,Display]  (START)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="VALUEDCUSROMER_DISCOUNT_MICHIGAN">
	<xsl:choose>
		<xsl:when test="NO_YEARS_WITH_WOLVERINE &lt;= 2">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@ID = 'V1']/@FACTOR"/>
		</xsl:when>
		<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 2 and NO_YEARS_WITH_WOLVERINE &lt;= 5">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@ID = 'V2']/@FACTOR"/>
		</xsl:when>
		<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 5">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@ID = 'V3']/@FACTOR"/>
		</xsl:when>
		<xsl:otherwise>
			1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="VALUEDCUSROMER_DISCOUNT_CREDIT_MICHIGAN">
	<xsl:choose>
		<xsl:when test="NO_YEARS_WITH_WOLVERINE &lt;= 2">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@ID = 'V1']/@CREDIT"/>
		</xsl:when>
		<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 2 and NO_YEARS_WITH_WOLVERINE &lt;= 5">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@ID = 'V2']/@CREDIT"/>
		</xsl:when>
		<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 5">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@ID = 'V3']/@CREDIT"/>
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="VALUEDCUSROMER_DISCOUNT_DISPLAY_MICHIGAN">
	<xsl:choose>
		<xsl:when test="NO_YEARS_WITH_WOLVERINE &lt;= 2">
			0
		</xsl:when>
		<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 2 and NO_YEARS_WITH_WOLVERINE &lt;= 5">
			'Applied'
		</xsl:when>
		<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 5">
			'Applied'
		</xsl:when>
		<xsl:otherwise>
			0
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<!-- For INDIANA -->
<xsl:template name="VALUEDCUSROMER_DISCOUNT_DISPLAY_INDIANA">
	<xsl:choose>
		<xsl:when test="NO_YEARS_WITH_WOLVERINE &lt;= 2">
			'Applied'
		</xsl:when>
		<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 2 and NO_YEARS_WITH_WOLVERINE &lt;= 4 and LOSSFREE = 'N'">
			'Applied'
		</xsl:when>
		<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 2 and NO_YEARS_WITH_WOLVERINE &lt;= 4 and LOSSFREE = 'Y'">
			'Applied'
		</xsl:when>
		<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 4 and LOSSFREE = 'N'">
			'Applied'
		</xsl:when>
		<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 4 and LOSSFREE = 'Y'">
			'Applied'
		</xsl:when>
		<xsl:otherwise>
			1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="VALUEDCUSROMER_DISCOUNT_INDIANA">
	<xsl:choose>
		<xsl:when test="NO_YEARS_WITH_WOLVERINE &lt;= 2">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@ID = 'V1']/@NO_LOSS_FACTOR"/>
		</xsl:when>
		<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 2 and NO_YEARS_WITH_WOLVERINE &lt;= 5 and LOSSFREE = 'N'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@ID = 'V2']/@LOSS_FACTOR"/>
		</xsl:when>
		<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 2 and NO_YEARS_WITH_WOLVERINE &lt;= 5 and LOSSFREE = 'Y'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@ID = 'V2']/@NO_LOSS_FACTOR"/>
		</xsl:when>
		<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 5 and LOSSFREE = 'N'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@ID = 'V3']/@LOSS_FACTOR"/>
		</xsl:when>
		<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 5 and LOSSFREE = 'Y'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@ID = 'V3']/@NO_LOSS_FACTOR"/>
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="VALUEDCUSROMER_DISCOUNT_CREDIT_INDIANA">
	<xsl:choose>
		<xsl:when test="NO_YEARS_WITH_WOLVERINE &lt;= 2">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@ID = 'V1']/@NO_LOSS_CREDIT"/>
		</xsl:when>
		<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 2 and NO_YEARS_WITH_WOLVERINE &lt;= 5 and LOSSFREE = 'N'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@ID = 'V2']/@LOSS_CREDIT"/>
		</xsl:when>
		<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 2 and NO_YEARS_WITH_WOLVERINE &lt;= 5 and LOSSFREE = 'Y'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@ID = 'V2']/@NO_LOSS_CREDIT"/>
		</xsl:when>
		<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 5 and LOSSFREE = 'N'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@ID = 'V3']/@LOSS_CREDIT"/>
		</xsl:when>
		<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 5 and LOSSFREE = 'Y'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@ID = 'V3']/@NO_LOSS_CREDIT"/>
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<xsl:template name="VALUEDCUSROMER_DISCOUNT">
	<xsl:choose>
		<xsl:when test ="STATENAME ='MICHIGAN'">
			<xsl:call-template name="VALUEDCUSROMER_DISCOUNT_MICHIGAN"/>
		</xsl:when>
		<xsl:when test ="STATENAME ='INDIANA'">
			<xsl:call-template name="VALUEDCUSROMER_DISCOUNT_INDIANA"/>
		</xsl:when>
		<xsl:otherwise>
		0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="VALUEDCUSROMER_DISCOUNT_CREDIT">
	<xsl:choose>
		<xsl:when test ="STATENAME ='MICHIGAN'">
			<xsl:call-template name="VALUEDCUSROMER_DISCOUNT_CREDIT_MICHIGAN"/>
		</xsl:when>
		<xsl:when test ="STATENAME ='INDIANA'">
			<xsl:call-template name="VALUEDCUSROMER_DISCOUNT_CREDIT_INDIANA"/>
		</xsl:when>
		<xsl:otherwise>
		0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="VALUEDCUSROMER_DISCOUNT_DISPLAY">
	<xsl:choose>
		<xsl:when test ="STATENAME ='MICHIGAN'">
			<xsl:call-template name="VALUEDCUSROMER_DISCOUNT_DISPLAY_MICHIGAN"/>
		</xsl:when>
		<xsl:when test ="STATENAME ='INDIANA'">
			<xsl:call-template name="VALUEDCUSROMER_DISCOUNT_DISPLAY_INDIANA"/>
		</xsl:when>
		<xsl:otherwise>
		1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<!-- ============================================================================================ -->
<!--			Templates for Discount - VALUED CUSTOMER [Factor,Credit,Display]  (END)			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--									Earthquake(START)										  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name ="EARTHQUAKE">
0.00
</xsl:template>
<!-- ============================================================================================ -->
<!--									Earthquake(END)											  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--					Installation Floater Building Materials IF-184(START)					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name ="INSTALLATION_FLOATER_IF_184_BUILDING_MATERIALS">
	<xsl:choose>
		<xsl:when test ="DWELL_UND_CONSTRUCTION_DP1143 = 'Y'">
			((<xsl:value-of select ="FLOATERBUILDINGMATERIALS_ADDITIONAL"/> DIV 1000.00) * 20)
		</xsl:when>
		<xsl:otherwise>0.00</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--							Installation Floater IF-184(end)								  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--				Installation Floater NON_Structural Equipment IF-184(START)					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name ="INSTALLATION_FLOATER_IF_184_NON_STRUCTURAL_EQUIPMENT">
	<xsl:choose>
		<xsl:when test ="DWELL_UND_CONSTRUCTION_DP1143 = 'Y'">
			((<xsl:value-of select ="FLOATERNONSTRUCTURAL_ADDITIONAL"/> DIV 1000.00) * 50)
		</xsl:when>
		<xsl:otherwise>0.00</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--				Installation Floater NON_Structural Equipment IF-184(START)					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--			Templates for Mine Subsidence Coverage[Premium]  (START)						  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name ="MINE_SUBSIDENCE_COVERAGE_PREMIUM">
	<xsl:variable name = "P_REPLACEMENTCOSTFACTOR" select ="REPLACEMENTCOSTFACTOR"/>
	<xsl:choose>
		<xsl:when test ="MINESUBSIDENCEDP480 ='Y'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINE']/NODE[@ID ='MINE_PREMIUM']/ATTRIBUTES[@MINCOVERAGE &lt;= $P_REPLACEMENTCOSTFACTOR and @MAXCOVERAGE &gt;= $P_REPLACEMENTCOSTFACTOR]/@PREMIUM"/>
		</xsl:when>
		<xsl:otherwise>
		0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--			Templates for Mime Subsidence Coverage[Premium]  (END)							  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->




<!-- ============================================================================================ -->
<!--		Templates for Surcharge - Surcharges Factor [Factor]  (START)						  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name ="SURCHARGES">
	<xsl:choose>
		<xsl:when test ="NUMBEROFFAMILIES = 1 and SEASONALSECONDARY = 'N'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGES']/NODE[@ID ='FAMILYUNITSINDWELL']/ATTRIBUTES[@UNITS = '1']/@PRIMARY"/>
		</xsl:when>		
		<xsl:when test ="NUMBEROFFAMILIES = 2 and SEASONALSECONDARY = 'N'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGES']/NODE[@ID ='FAMILYUNITSINDWELL']/ATTRIBUTES[@UNITS = '2']/@PRIMARY"/>
		</xsl:when>		
		<xsl:when test ="NUMBEROFFAMILIES = 1 and SEASONALSECONDARY = 'Y'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGES']/NODE[@ID ='FAMILYUNITSINDWELL']/ATTRIBUTES[@UNITS = '1']/@SEASONAL"/>
		</xsl:when>		
		<xsl:when test ="NUMBEROFFAMILIES = 2 and SEASONALSECONDARY = 'Y'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGES']/NODE[@ID ='FAMILYUNITSINDWELL']/ATTRIBUTES[@UNITS = '2']/@SEASONAL"/>
		</xsl:when>		
		<xsl:otherwise>
		1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--		Templates for Surcharge - Surcharges Factor [Factor]  (END)							  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--						TEMPLATE FOR CALLING ADJUSTEDBASE AS PER THE INPUT-XML (START)		  -->
<!-- ============================================================================================ -->

<xsl:template name ="CALL_ADJUSTEDBASE">
	<xsl:choose>
		<xsl:when test ="STATENAME ='MICHIGAN' and PRODUCTNAME = 'DP-2' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="ADJUSTEDBASE_MICHIGAN_REGULER_DP2"/>
		</xsl:when>
		<xsl:when test ="STATENAME ='MICHIGAN' and PRODUCTNAME = 'DP-3' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="ADJUSTEDBASE_MICHIGAN_REGULER_DP3"/>
		</xsl:when>
		<!-- For Michigan REPAIR COST -->
		<xsl:when test ="STATENAME ='MICHIGAN' and PRODUCTNAME = 'DP-3' and PRODUCT_PREMIER = 'Premier'" >
			<xsl:call-template name ="ADJUSTEDBASE_MICHIGAN_PREMIER_DP3"/>
		</xsl:when>
		<xsl:when test ="STATENAME ='MICHIGAN' and PRODUCTNAME = 'DP-2' and PRODUCT_PREMIER = 'Repair Cost'" >
			<xsl:call-template name ="ADJUSTEDBASE_MICHIGAN_REPAIR_COST_DP2"/>
		</xsl:when>
		<xsl:when test ="STATENAME ='MICHIGAN' and PRODUCTNAME = 'DP-3' and PRODUCT_PREMIER = 'Repair Cost'" >
			<xsl:call-template name ="ADJUSTEDBASE_MICHIGAN_REPAIR_COST_DP3"/>
		</xsl:when>
		<!-- For INDIANA -->
		<xsl:when test ="STATENAME ='INDIANA' and PRODUCTNAME = 'DP-2' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="ADJUSTEDBASE_INDIANA_REGULER_DP2"/>
		</xsl:when>
		<xsl:when test ="STATENAME ='INDIANA' and PRODUCTNAME = 'DP-3' and PRODUCT_PREMIER = ''" >
			<xsl:call-template name ="ADJUSTEDBASE_INDIANA_REGULER_DP3"/>
		</xsl:when>
		<xsl:when test ="STATENAME ='INDIANA' and PRODUCTNAME = 'DP-2' and PRODUCT_PREMIER = 'Repair Cost'" >
			<xsl:call-template name ="ADJUSTEDBASE_INDIANA_REPAIR_COST_DP2"/>
		</xsl:when>
		<xsl:when test ="STATENAME ='INDIANA' and PRODUCTNAME = 'DP-3' and PRODUCT_PREMIER = 'Repair Cost'" >
			<xsl:call-template name ="ADJUSTEDBASE_INDIANA_REPAIR_COST_DP3"/>
		</xsl:when>
		<xsl:otherwise>
		</xsl:otherwise>
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
((((((
		(<xsl:call-template name="DEWLLING-MAIN"></xsl:call-template> )*
		(<xsl:call-template name ="C_VALUE"></xsl:call-template>)*
		(<xsl:call-template name ="REPLACEMENT_COST"></xsl:call-template>)*
		(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)*
		(<xsl:call-template name ="NO_OF_FALILY_UNITS_DWELLINGS_FACTOR"></xsl:call-template>)
		)*
		(<xsl:call-template name = "DFACTOR"/>))*
		(<xsl:call-template name = "PROTECTIVE_DEVICE_DISCOUNT_FACTOR"/>))*
		(<xsl:call-template name = "DUC_DISCOUNT"/>))*
		(<xsl:call-template name = "MULTIPOLICY_DISCOUNT_FACTOR"/>))*
		(<xsl:call-template name = "VALUEDCUSROMER_DISCOUNT"/>))*
		(<xsl:call-template name = "SURCHARGES"/>)
</xsl:template>

<!-- ============================================================================================ -->
<!--							ADJUSTEDBASE FOR MICHIGAN(REGULER) DP-3	(END)					  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--							ADJUSTEDBASE FOR MICHIGAN(REGULER) DP-2	(START)					  -->
<!-- ============================================================================================ -->
<xsl:template name="ADJUSTEDBASE_MICHIGAN_REGULER_DP2">
((((((
		(<xsl:call-template name="DEWLLING-MAIN"></xsl:call-template> )*
		(<xsl:call-template name ="C_VALUE"></xsl:call-template>)*
		(<xsl:call-template name ="REPLACEMENT_COST"></xsl:call-template>)*
		(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)*
		(<xsl:call-template name ="NO_OF_FALILY_UNITS_DWELLINGS_FACTOR"></xsl:call-template>)
		)*
		(<xsl:call-template name = "DFACTOR"/>))*
		(<xsl:call-template name = "PROTECTIVE_DEVICE_DISCOUNT_FACTOR"/>))*
		(<xsl:call-template name = "DUC_DISCOUNT"/>))*
		(<xsl:call-template name = "MULTIPOLICY_DISCOUNT_FACTOR"/>))*
		(<xsl:call-template name = "VALUEDCUSROMER_DISCOUNT"/>))*
		(<xsl:call-template name = "SURCHARGES"/>)
</xsl:template>

<!-- ============================================================================================ -->
<!--							ADJUSTEDBASE FOR MICHIGAN(REGULER) DP-2	(END)					  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--							ADJUSTEDBASE FOR MICHIGAN(PREMIUM) DP-3	(START)					  -->
<!-- ============================================================================================ -->
<xsl:template name="ADJUSTEDBASE_MICHIGAN_PREMIER_DP3">
((((((
		(<xsl:call-template name="DEWLLING-MAIN"/> )*
		(<xsl:call-template name ="C_VALUE"/>)*
		(<xsl:call-template name ="REPLACEMENT_COST"/>)*
		(<xsl:call-template name ="TERM_FACTOR"/>)*
		(<xsl:call-template name ="NO_OF_FALILY_UNITS_DWELLINGS_FACTOR"/>)
		)*
		(<xsl:call-template name = "DFACTOR"/>))*
		(<xsl:call-template name = "PROTECTIVE_DEVICE_DISCOUNT_FACTOR"/>))*
		(<xsl:call-template name = "AGEOFHOME_FACTOR"/>))*
		(<xsl:call-template name = "MULTIPOLICY_DISCOUNT_FACTOR"/>))*
		(<xsl:call-template name = "VALUEDCUSROMER_DISCOUNT"/>))*
		(<xsl:call-template name = "SURCHARGES"/>)
</xsl:template>

<!-- ============================================================================================ -->
<!--							ADJUSTEDBASE FOR MICHIGAN(PREMIUM) DP-3	(END)					  -->
<!-- ============================================================================================ -->

<!--============(((((((((((((()))))))))))))))))))))))))))===========================-->

<!-- ============================================================================================ -->
<!--							ADJUSTEDBASE FOR MICHIGAN(REPAIR COST) DP-3	(START)				  -->
<!-- ============================================================================================ -->
<xsl:template name="ADJUSTEDBASE_MICHIGAN_REPAIR_COST_DP3">
((((((
		(<xsl:call-template name="DEWLLING-MAIN"></xsl:call-template> )*
		(<xsl:call-template name ="C_VALUE"></xsl:call-template>)*
		(1.26)*
		(<xsl:call-template name ="REPLACEMENT_COST"></xsl:call-template>)*
		(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)*
		(<xsl:call-template name ="NO_OF_FALILY_UNITS_DWELLINGS_FACTOR"></xsl:call-template>)
		)*
		(<xsl:call-template name = "DFACTOR"/>))*
		(<xsl:call-template name = "PROTECTIVE_DEVICE_DISCOUNT_FACTOR"/>))*
		(<xsl:call-template name = "DUC_DISCOUNT"/>))*
		(<xsl:call-template name = "MULTIPOLICY_DISCOUNT_FACTOR"/>))*
		(<xsl:call-template name = "VALUEDCUSROMER_DISCOUNT"/>))*
		(<xsl:call-template name = "SURCHARGES"/>)
</xsl:template>

<!-- ============================================================================================ -->
<!--							ADJUSTEDBASE FOR MICHIGAN(REPAIR COST) DP-3	(END)				  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--							ADJUSTEDBASE FOR MICHIGAN(REPAIR COST) DP-2	(START)				  -->
<!-- ============================================================================================ -->
<xsl:template name="ADJUSTEDBASE_MICHIGAN_REPAIR_COST_DP2">
(((((
		(<xsl:call-template name="DEWLLING-MAIN"></xsl:call-template> )*
		(<xsl:call-template name ="C_VALUE"></xsl:call-template>)*
		(1.26)*
		(<xsl:call-template name ="REPLACEMENT_COST"></xsl:call-template>)*
		(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)*
		(<xsl:call-template name ="NO_OF_FALILY_UNITS_DWELLINGS_FACTOR"></xsl:call-template>)
		)*
		(<xsl:call-template name = "DFACTOR"/>))*
		(<xsl:call-template name = "PROTECTIVE_DEVICE_DISCOUNT_FACTOR"/>))*
		(<xsl:call-template name = "DUC_DISCOUNT"/>))*
		(<xsl:call-template name = "MULTIPOLICY_DISCOUNT_FACTOR"/>))*
		(<xsl:call-template name = "VALUEDCUSROMER_DISCOUNT"/>)
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
((((((
		(<xsl:call-template name="DEWLLING-MAIN_INDIANA"></xsl:call-template> )*
		(<xsl:call-template name ="C_VALUE"></xsl:call-template>)*
		(<xsl:call-template name ="REPLACEMENT_COST"></xsl:call-template>)*
		(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)*
		(<xsl:call-template name ="NO_OF_FALILY_UNITS_DWELLINGS_FACTOR"></xsl:call-template>)
		)*
		(<xsl:call-template name = "DFACTOR"/>))*
		(<xsl:call-template name = "PROTECTIVE_DEVICE_DISCOUNT_FACTOR"/>))*
		(<xsl:call-template name = "DUC_DISCOUNT"/>))*
		(<xsl:call-template name = "MULTIPOLICY_DISCOUNT_FACTOR"/>))*
		(<xsl:call-template name = "VALUEDCUSROMER_DISCOUNT_INDIANA"/>))*
		(<xsl:call-template name = "SURCHARGES"/>)
</xsl:template>

<!-- ============================================================================================ -->
<!--							ADJUSTEDBASE FOR INDIANA(REGULER) DP-3	(END)					  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--							ADJUSTEDBASE FOR INDIANA(REGULER) DP-2	(START)					  -->
<!-- ============================================================================================ -->
<xsl:template name="ADJUSTEDBASE_INDIANA_REGULER_DP2">
((((((
		(<xsl:call-template name="DEWLLING-MAIN_INDIANA"/>)*
		(<xsl:call-template name ="C_VALUE"/>)*
		(<xsl:call-template name ="REPLACEMENT_COST"/>)*
		(<xsl:call-template name ="TERM_FACTOR"/>)*
		(<xsl:call-template name ="NO_OF_FALILY_UNITS_DWELLINGS_FACTOR"/>)
		)*
		(<xsl:call-template name = "DFACTOR"/>))*
		(<xsl:call-template name = "PROTECTIVE_DEVICE_DISCOUNT_FACTOR"/>))*
		(<xsl:call-template name = "DUC_DISCOUNT"/>))*
		(<xsl:call-template name = "MULTIPOLICY_DISCOUNT_FACTOR"/>))*
		(<xsl:call-template name = "VALUEDCUSROMER_DISCOUNT_INDIANA"/>))*
		(<xsl:call-template name = "SURCHARGES"/>)
</xsl:template>

<!-- ============================================================================================ -->
<!--							ADJUSTEDBASE FOR INDIANA(REGULER) DP-2	(END)					  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--							ADJUSTEDBASE FOR INDIANA(REPAIR COST) DP-3	(START)				  -->
<!-- ============================================================================================ -->
<xsl:template name="ADJUSTEDBASE_INDIANA_REPAIR_COST_DP3">
((((((
		(<xsl:call-template name="DEWLLING-MAIN_INDIANA"></xsl:call-template> )*
		(<xsl:call-template name ="C_VALUE"></xsl:call-template>)*
		(1.16)*
		<!--(<xsl:call-template name ="REPLACEMENT_COST"></xsl:call-template>)*-->
		(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)*
		(<xsl:call-template name ="NO_OF_FALILY_UNITS_DWELLINGS_FACTOR"></xsl:call-template>)
		)*
		(<xsl:call-template name = "DFACTOR"/>))*
		(<xsl:call-template name = "PROTECTIVE_DEVICE_DISCOUNT_FACTOR"/>))*
		(<xsl:call-template name = "DUC_DISCOUNT"/>))*
		(<xsl:call-template name = "MULTIPOLICY_DISCOUNT_FACTOR"/>))*
		(<xsl:call-template name = "VALUEDCUSROMER_DISCOUNT_INDIANA"/>))*
		(<xsl:call-template name = "SURCHARGES"/>)
</xsl:template>

<!-- ============================================================================================ -->
<!--							ADJUSTEDBASE FOR INDIANA(REPAIR COST) DP-3	(END)				  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--							ADJUSTEDBASE FOR INDIANA(REPAIR COST) DP-2	(START)				  -->
<!-- ============================================================================================ -->
<xsl:template name="ADJUSTEDBASE_INDIANA_REPAIR_COST_DP2">
(((((
		(<xsl:call-template name="DEWLLING-MAIN_INDIANA"></xsl:call-template> )*
		(<xsl:call-template name ="C_VALUE"></xsl:call-template>)*
		(1.16)*
		<!--(<xsl:call-template name ="REPLACEMENT_COST"></xsl:call-template>)*-->
		(<xsl:call-template name ="TERM_FACTOR"></xsl:call-template>)*
		(<xsl:call-template name ="NO_OF_FALILY_UNITS_DWELLINGS_FACTOR"></xsl:call-template>)
		)*
		(<xsl:call-template name = "DFACTOR"/>))*
		(<xsl:call-template name = "PROTECTIVE_DEVICE_DISCOUNT_FACTOR"/>))*
		(<xsl:call-template name = "DUC_DISCOUNT"/>))*
		(<xsl:call-template name = "MULTIPOLICY_DISCOUNT_FACTOR"/>))*
		(<xsl:call-template name = "VALUEDCUSROMER_DISCOUNT_INDIANA"/>)
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

<xsl:template name ="TREES_LAWNS_AND_SHRUBS_ADDITIONAL">
	<xsl:variable name ="PTREES_LAWNS">
		<xsl:choose>
			<xsl:when test ="DEDUCTIBLE = 500 and PRODUCTNAME = 'DP-3'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBES_2']/ATTRIBUTES[@NAME = 'TREES_LAWNS_AND_SHRUBS' and @FORM_CODE='DP-3']/@DEDUCTIBLE_1"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 750 and PRODUCTNAME = 'DP-3'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBES_2']/ATTRIBUTES[@NAME = 'TREES_LAWNS_AND_SHRUBS' and @FORM_CODE='DP-3']/@DEDUCTIBLE_2"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 1000 and PRODUCTNAME = 'DP-3'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBES_2']/ATTRIBUTES[@NAME = 'TREES_LAWNS_AND_SHRUBS' and @FORM_CODE='DP-3']/@DEDUCTIBLE_3"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 2500 and PRODUCTNAME = 'DP-3'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBES_2']/ATTRIBUTES[@NAME = 'TREES_LAWNS_AND_SHRUBS' and @FORM_CODE='DP-3']/@DEDUCTIBLE_4"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 500 and PRODUCTNAME = 'DP-2'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBES_2']/ATTRIBUTES[@NAME = 'TREES_LAWNS_AND_SHRUBS' and @FORM_CODE='DP-2']/@DEDUCTIBLE_1"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 750 and PRODUCTNAME = 'DP-2'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBES_2']/ATTRIBUTES[@NAME = 'TREES_LAWNS_AND_SHRUBS' and @FORM_CODE='DP-2']/@DEDUCTIBLE_2"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 1000 and PRODUCTNAME = 'DP-2'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBES_2']/ATTRIBUTES[@NAME = 'TREES_LAWNS_AND_SHRUBS' and @FORM_CODE='DP-2']/@DEDUCTIBLE_3"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 2500 and PRODUCTNAME = 'DP-2'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBES_2']/ATTRIBUTES[@NAME = 'TREES_LAWNS_AND_SHRUBS' and @FORM_CODE='DP-2']/@DEDUCTIBLE_4"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	
	</xsl:variable>
	<xsl:choose>
		<xsl:when test = "TREESLAWNSSHRUBS_ADDITIONAL = '0'">
		0.00
		</xsl:when>
		<xsl:otherwise>
			(((<xsl:value-of select ="TREESLAWNSSHRUBS_ADDITIONAL"/>) DIV 100.00)* <xsl:value-of select ="$PTREES_LAWNS"/> )
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--									Trees, Lawns, and Shrubs (END)		  					  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--									Radio and TV Equipment (START)		  					  -->
<!-- ============================================================================================ -->

<xsl:template name ="RADIO_AND_TV_EQUIPMENT_ADDITIONAL">
	<xsl:variable name ="PRADIOTV_ADDITIONAL">
		<xsl:choose>
			<xsl:when test ="DEDUCTIBLE = 500">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBES_2']/ATTRIBUTES[@NAME = 'RADIO_AND_TV_EQUIPMENT']/@DEDUCTIBLE_1"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 750">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBES_2']/ATTRIBUTES[@NAME = 'RADIO_AND_TV_EQUIPMENT']/@DEDUCTIBLE_2"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 1000">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBES_2']/ATTRIBUTES[@NAME = 'RADIO_AND_TV_EQUIPMENT']/@DEDUCTIBLE_3"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 2500">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBES_2']/ATTRIBUTES[@NAME = 'RADIO_AND_TV_EQUIPMENT']/@DEDUCTIBLE_4"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	
	</xsl:variable>
	<xsl:choose>
		<xsl:when test = "RADIOTV_ADDITIONAL = '0'">
		0.00
		</xsl:when>
		<xsl:otherwise>
			(((<xsl:value-of select ="RADIOTV_ADDITIONAL"/>) DIV 100.00)* <xsl:value-of select ="$PRADIOTV_ADDITIONAL"/> )
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--									Radio and TV Equipment (END)		  					  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--									Satellite Dishes  (START)			  					  -->
<!-- ============================================================================================ -->
<xsl:template name ="SATELLITE_DISHES_ADDITIONAL">
	<xsl:variable name ="PSATELLITE_DISHES_ADDITIONAL">
		<xsl:choose>
			<xsl:when test ="DEDUCTIBLE = 500">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBES_2']/ATTRIBUTES[@NAME = 'SATELLITE_DISHES']/@DEDUCTIBLE_1"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 750">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBES_2']/ATTRIBUTES[@NAME = 'SATELLITE_DISHES']/@DEDUCTIBLE_2"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 1000">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBES_2']/ATTRIBUTES[@NAME = 'SATELLITE_DISHES']/@DEDUCTIBLE_3"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 2500">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBES_2']/ATTRIBUTES[@NAME = 'SATELLITE_DISHES']/@DEDUCTIBLE_4"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	
	</xsl:variable>
	<xsl:choose>
		<xsl:when test = "SATELLITEDISHES_ADDITIONAL = '0'">
		0.00
		</xsl:when>
		<xsl:otherwise>
			(((<xsl:value-of select ="SATELLITEDISHES_ADDITIONAL"/>) DIV 100.00)* <xsl:value-of select ="$PSATELLITE_DISHES_ADDITIONAL"/> )
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--									Satellite Dishes  (END)				  					  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--									Awnings and Canopies  (START)			  				  -->
<!-- ============================================================================================ -->
<xsl:template name ="AWNINGS_AND_CANOPIES_ADDITIONAL">
	<xsl:variable name ="PAWNINGS_AND_CANOPIES_ADDITIONAL">
		<xsl:choose>
			<xsl:when test ="DEDUCTIBLE = 500">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBES_2']/ATTRIBUTES[@NAME = 'AWNINGS_CANOPIES']/@DEDUCTIBLE_1"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 750">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBES_2']/ATTRIBUTES[@NAME = 'AWNINGS_CANOPIES']/@DEDUCTIBLE_2"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 1000">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBES_2']/ATTRIBUTES[@NAME = 'AWNINGS_CANOPIES']/@DEDUCTIBLE_3"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 2500">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBES_2']/ATTRIBUTES[@NAME = 'AWNINGS_CANOPIES']/@DEDUCTIBLE_4"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:variable>
	<xsl:choose>
		<xsl:when test = "AWNINGSCANOPIES_ADDITIONAL = '0'">
		0.00
		</xsl:when>
		<xsl:otherwise>
			(((<xsl:value-of select ="AWNINGSCANOPIES_ADDITIONAL"/>) DIV 100.00)* <xsl:value-of select ="$PAWNINGS_AND_CANOPIES_ADDITIONAL"/> )
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--									Awnings and Canopies  (END)				  				  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--							Appurtenant Structures (Coverage B)  (START)			  		  -->
<!-- ============================================================================================ -->
<xsl:template name ="COVERAGE_B_ADDITIONAL">
	<xsl:variable name ="PCOVERAGE_B_ADDITIONAL">
		<xsl:choose>
			<xsl:when test ="DEDUCTIBLE = 500">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'COVERAGE_B']/@DEDUCTIBLE_1"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 750">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'COVERAGE_B']/@DEDUCTIBLE_2"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 1000">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'COVERAGE_B']/@DEDUCTIBLE_3"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 2500">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'COVERAGE_B']/@DEDUCTIBLE_4"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:variable>
	<xsl:choose>
		<xsl:when test = "APPURTENANTSTRUCTURES_ADDITIONAL = '0'">
		0.00
		</xsl:when>
		<xsl:otherwise>
			(((<xsl:value-of select ="APPURTENANTSTRUCTURES_ADDITIONAL"/>) DIV 1000.00)* <xsl:value-of select ="$PCOVERAGE_B_ADDITIONAL"/> )
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--							Appurtenant Structures (Coverage B)  (END)				  		  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--							Building Improvements/Alteration  (START)				  		  -->
<!-- ============================================================================================ -->
<xsl:template name ="BUILDING_IMPROVEMENTS_ADDITIONAL">
	<xsl:variable name ="PBUILDING_IMPROVEMENTS_ADDITIONAL">
		<xsl:choose>
			<xsl:when test ="DEDUCTIBLE = 500">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'BUILDING_IMPROVEMENTS_AND_ALTERATIONS']/@DEDUCTIBLE_1"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 750">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'BUILDING_IMPROVEMENTS_AND_ALTERATIONS']/@DEDUCTIBLE_2"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 1000">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'BUILDING_IMPROVEMENTS_AND_ALTERATIONS']/@DEDUCTIBLE_3"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 2500">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'BUILDING_IMPROVEMENTS_AND_ALTERATIONS']/@DEDUCTIBLE_4"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:variable>
	<xsl:choose>
		<xsl:when test = "BUILDINGIMPROVEMENTS_ADDITIONAL = '0'">
		0.00
		</xsl:when>
		<xsl:otherwise>
			(((<xsl:value-of select ="BUILDINGIMPROVEMENTS_ADDITIONAL"/>) DIV 1000.00)* <xsl:value-of select ="$PBUILDING_IMPROVEMENTS_ADDITIONAL"/> )
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--							Building Improvements/Alteration  (END)					  		  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--							 Increased Coverage D - Rental Value  (START)			  		  -->
<!-- ============================================================================================ -->
<xsl:template name ="INCREASED_COVERAGE_D_ADDITIONAL">
	<xsl:variable name ="PRENTAL_VALUE_ADDITIONAL">
		<xsl:choose>
			<xsl:when test ="DEDUCTIBLE = 500">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'RENTAL_VALUE']/@DEDUCTIBLE_1"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 750">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'RENTAL_VALUE']/@DEDUCTIBLE_2"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 1000">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'RENTAL_VALUE']/@DEDUCTIBLE_3"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 2500">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'RENTAL_VALUE']/@DEDUCTIBLE_4"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:variable>
	<xsl:choose>
		<xsl:when test = "RENTALVALUE_ADDITIONAL = '0'">
		0.00
		</xsl:when>
		<xsl:otherwise>
			(((<xsl:value-of select ="RENTALVALUE_ADDITIONAL"/>) DIV 1000.00)* <xsl:value-of select ="$PRENTAL_VALUE_ADDITIONAL"/> )
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--							 Increased Coverage D - Rental Value  (END)				  		  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--					 Increased Coverage C - Landlords Personal Property  (START)			  -->
<!-- ============================================================================================ -->
<xsl:template name ="COVERAGE_C_ADDITIONAL">
	<xsl:variable name ="PCOVERAGE_C_ADDITIONAL">
		<xsl:choose>
			<xsl:when test ="DEDUCTIBLE = 500">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'COVERAGE_C']/@DEDUCTIBLE_1"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 750">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'COVERAGE_C']/@DEDUCTIBLE_2"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 1000">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'COVERAGE_C']/@DEDUCTIBLE_3"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 2500">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'COVERAGE_C']/@DEDUCTIBLE_4"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:variable>
	<xsl:choose>
		<xsl:when test = "PERSONALPROPERTY_ADDITIONAL = '0'">
		0.00
		</xsl:when>
		<xsl:otherwise>
			(((<xsl:value-of select ="PERSONALPROPERTY_ADDITIONAL"/>) DIV 1000.00)* <xsl:value-of select ="$PCOVERAGE_C_ADDITIONAL"/> )
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--							 Increased Coverage C - Landlords Personal Property   (END)		  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--									Contents In Storage (START)								  -->
<!-- ============================================================================================ -->
<xsl:template name ="CONTENTS_IN_STORAGE_ADDITIONAL">
	<xsl:variable name ="PCOVERAGE_C_ADDITIONAL">
		<xsl:choose>
			<xsl:when test ="DEDUCTIBLE = 500">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'CONTENTS_IN_STORAGE']/@DEDUCTIBLE_1"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 750">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'CONTENTS_IN_STORAGE']/@DEDUCTIBLE_2"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 1000">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'CONTENTS_IN_STORAGE']/@DEDUCTIBLE_3"/>
			</xsl:when>
			<xsl:when test ="DEDUCTIBLE = 2500">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'CONTENTS_IN_STORAGE']/@DEDUCTIBLE_4"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:variable>
	<xsl:choose>
		<xsl:when test = "CONTENTSINSTORAGE_ADDITIONAL = '0'">
		0.00
		</xsl:when>
		<xsl:otherwise>
			(((<xsl:value-of select ="CONTENTSINSTORAGE_ADDITIONAL"/>) DIV 1000.00)* <xsl:value-of select ="$PCOVERAGE_C_ADDITIONAL"/> )
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
		<xsl:when test ="TERMFACTOR = 12 and STATENAME ='MICHIGAN'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PROPRTYFEE']/NODE[@ID='PFEE']/ATTRIBUTES/@ANNUAL_FEES"/>
		</xsl:when>
		<xsl:when test ="TERMFACTOR = 6 and STATENAME ='MICHIGAN'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PROPRTYFEE']/NODE[@ID='PFEE']/ATTRIBUTES/@SEMI_ANNUAL_FEES"/>
		</xsl:when>
		<xsl:otherwise>
		0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--							Template Property Expense Fee (START)							  -->
<!--		    							FOR MISHIGAN										  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--									Additional Coverage Template (START) 					  -->
<!-- ============================================================================================ -->

<xsl:template name ="ADDITIONALCOVERAGE">
	(<xsl:call-template name ="EARTHQUAKE"/>)+
	(<xsl:call-template name ="INSTALLATION_FLOATER_IF_184_BUILDING_MATERIALS"/>)+
	(<xsl:call-template name ="INSTALLATION_FLOATER_IF_184_NON_STRUCTURAL_EQUIPMENT"/>)+
	(<xsl:call-template name ="MINE_SUBSIDENCE_COVERAGE_PREMIUM"/>)+
	(<xsl:call-template name ="TREES_LAWNS_AND_SHRUBS_ADDITIONAL"/>)+
	(<xsl:call-template name ="RADIO_AND_TV_EQUIPMENT_ADDITIONAL"/>)+
	(<xsl:call-template name ="SATELLITE_DISHES_ADDITIONAL"/>)+
	(<xsl:call-template name ="AWNINGS_AND_CANOPIES_ADDITIONAL"/>)+
	(<xsl:call-template name ="COVERAGE_B_ADDITIONAL"/>)+
	(<xsl:call-template name ="BUILDING_IMPROVEMENTS_ADDITIONAL"/>)+
	(<xsl:call-template name ="INCREASED_COVERAGE_D_ADDITIONAL"/>)+
	(<xsl:call-template name ="COVERAGE_C_ADDITIONAL"/>)+
	(<xsl:call-template name ="CONTENTS_IN_STORAGE_ADDITIONAL"/>)+
	(<xsl:call-template name ="LP_124_LANDLORDS_LIABILITY"/>)+
	(<xsl:call-template name ="PROPFEE"/>)
</xsl:template>

<!-- ============================================================================================ -->
<!--									Additional Coverage Template (END)		  				  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--									Final Primium Template (START)   						  -->
<!-- ============================================================================================ -->
<xsl:template name ="FINALPRIMIUM">

	(<xsl:call-template name="CALL_ADJUSTEDBASE"/>) +
	(<xsl:call-template name="ADDITIONALCOVERAGE"/>)
	

</xsl:template>
<!-- ============================================================================================ -->
<!--									Final Primium Template (END)   							  -->
<!-- ============================================================================================ -->


</xsl:stylesheet>

