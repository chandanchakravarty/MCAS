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
	<xsl:variable name="HOCoveragesDoc" select="document('FactorPath')"></xsl:variable>
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
						<!-- CC's , Symbol -->
						<SUBGROUP>
							<STEP STEPID="1">
								<PATH>ND</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Class, Vehicle Type-->
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
						<!-- Bodily Injury Liability -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="4">
									<PATH>
								{
									  <xsl:call-template name="BI" />  
									 
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Property Damage Liability-->
						<SUBGROUP>
							<IF>
								<STEP STEPID="5">
									<PATH>
								{
									<xsl:call-template name="PD" />  
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Combined Single Limit (Bodily Injury, PD) -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="6">
									<PATH>
								{
									  <xsl:call-template name="CSLBIPD"></xsl:call-template>  
									
									
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Michigan Statutory Assessment -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="7">
									<PATH>
							{
								<xsl:call-template name="MCCAFEE"></xsl:call-template>
							}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Medical Payments-->
						<SUBGROUP>
							<IF>
								<STEP STEPID="8">
									<PATH>
								 
								{
									   <xsl:choose>
											<xsl:when test="POLICY/STATENAME ='INDIANA'">
												<xsl:call-template name="MEDPAY" />
											</xsl:when>
											<xsl:otherwise>0</xsl:otherwise>
										</xsl:choose>
								}
					
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Medical Options-->
						<SUBGROUP>
							<IF>
								<STEP STEPID="9">
									<PATH>
								{
									 <xsl:choose>
											<xsl:when test="normalize-space(POLICY/STATENAME)='MICHIGAN'">
												<xsl:call-template name="MEDPAY_MICHIGAN" />
											</xsl:when>
											<xsl:otherwise>0</xsl:otherwise>
										</xsl:choose> 
             					}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!--Uninsured Motorists -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="10">
									<PATH>
								{								
									
									
											<xsl:call-template name="UM"></xsl:call-template>
										
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Under Insured Motorists -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="11">
									<PATH>
										<xsl:choose>
											<xsl:when test="POLICY/STATENAME ='INDIANA'">
												<xsl:call-template name="UIM"></xsl:call-template>
											</xsl:when>
											<xsl:otherwise>
											0
										</xsl:otherwise>
										</xsl:choose>
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!--Uninsured Motorists - Property Damagae-->
						<SUBGROUP>
							<IF>
								<STEP STEPID="12">
									<PATH>
								{
									<xsl:call-template name="UMPD"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Comprehensive -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="13">
									<PATH>
								{
									<!-- not applicable if not selected  -->
									<!--<xsl:choose>
										<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE != '' and VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE !='0'">
											<xsl:call-template name="COMP"></xsl:call-template>
										</xsl:when>
										<xsl:otherwise>
											0
										</xsl:otherwise>
									</xsl:choose> -->
									<xsl:call-template name="COMP"></xsl:call-template>
									
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Collision -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="14">
									<PATH>
								{
									<!-- not applicable if not selected -->
									<xsl:choose>
											<xsl:when test="VEHICLES/VEHICLE/COLLISIONDEDUCTIBLE != '' and VEHICLES/VEHICLE/COLLISIONDEDUCTIBLE != '0'">
												<xsl:call-template name="COLL"></xsl:call-template>
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
						<!-- Road Service -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="15">
									<PATH>
										<xsl:call-template name="ROADSERVICES_DISPLAY"></xsl:call-template>
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Helmet and Riding Apparel (M-15)-->
						<SUBGROUP>
							<IF>
								<STEP STEPID="16">
									<PATH>
										<xsl:call-template name="HELMET_RIDING_APPAREL_DISPLAY"></xsl:call-template>
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Trailer Coverage (M-49) -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="17">
									<PATH>
										<xsl:call-template name="TRAILER_COVERAGE_DISPLAY"></xsl:call-template>
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Additional Physical Damage Coverage(M-14) -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="18">
									<PATH>
								{
								<xsl:call-template name="ADDITIONAL_PHYSICAL_DAMAGE"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Discount:Multi-Cycle-->
						<SUBGROUP>
							<STEP STEPID="19">
								<PATH>
									<xsl:call-template name="MULTIVEHICLE_DISPLAY"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Discount:Transfer/Renewal-->
						<SUBGROUP>
							<STEP STEPID="20">
								<PATH>
									<xsl:call-template name="TRANSFER_RENEWAL_DISPLAY"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Discount:Multi-Policy(Cycle -Auto/Home)-->
						<SUBGROUP>
							<STEP STEPID="21">
								<PATH>
									<xsl:call-template name="MULTIPOLICY_DISPLAY"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Discount:Preferred Risk-->
						<SUBGROUP>
							<STEP STEPID="22">
								<PATH>
									<xsl:call-template name="PREFERRED_RISK_DISPLAY"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Discount:Mature Operator Discount -->
						<SUBGROUP>
							<STEP STEPID="23">
								<PATH>
									<xsl:call-template name="MATURE_CREDIT_DISPLAY"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Discount:Insurance Score Credit-->
						<SUBGROUP>
							<STEP STEPID="24">
								<PATH>
									<xsl:call-template name="INSURANCESCORE_DISPLAY"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Discount:No cycle Endorsement on License -->
						<SUBGROUP>
							<STEP STEPID="25">
								<PATH>
									<xsl:call-template name="NO_CYCLE_ENDORSEMENT_DISPLAY"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Discount or Surcharge:Bike Type-->
						<SUBGROUP>
							<STEP STEPID="26">
								<PATH>
									<xsl:call-template name="BIKE_TYPE_DISPLAY"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="28">
									<PATH>
										<xsl:call-template name="ACCIDENT_VIOLATION_SURCHARGE_DISPLAY" />
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
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<!-- Minimum Premium Suspended Comp Only-->
						<SUBGROUP>
							<IF>
								<STEP STEPID="27">
									<PATH>
							{
								<xsl:call-template name="MINIMUMPREMIUM"></xsl:call-template>
							}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Final Premium -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="29">
									<PATH>
							{
								<xsl:call-template name="FINALPREMIUM"></xsl:call-template>
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
						<!-- CC's , Symbol-->
						<STEP STEPID="1" CALC_ID="1001" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID1" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE>SYM_CC</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Class, Vehicle Type-->
						<STEP STEPID="2" CALC_ID="1002" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID2" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE>CLS</COMPONENT_CODE>
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
						<!-- Bodily Injury Liability -->
						<STEP STEPID="4" CALC_ID="1004" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID4" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/BI" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BI</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="BI"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Property Damage Liability -->
						<STEP STEPID="5" CALC_ID="1005" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID5" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/PD" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>PD</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="PD"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Combined Single Limit (Bodily Injury, PD) -->
						<STEP STEPID="6" CALC_ID="1006" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID6" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/CSL" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>CSL</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="CSLBIPD"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'CSL'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Michigan Statutory Assessment -->
						<STEP STEPID="7" CALC_ID="1007" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID7" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MCCAFEE</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="MCCAFEE"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'MCCAFEE'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Medical Payments -->
						<STEP STEPID="8" CALC_ID="1008" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID8" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/MEDPM" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MED_PMT</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:choose>
									<xsl:when test="POLICY/STATENAME ='INDIANA'">
										<xsl:call-template name="MEDPAY"></xsl:call-template>
									</xsl:when>
									<xsl:otherwise>0</xsl:otherwise>
								</xsl:choose>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'MED_PMT'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Medical Options -->
						<STEP STEPID="9" CALC_ID="1009" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID9" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:call-template name="MEDICAL_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/MEDPMLIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MED_OPT</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:choose>
									<xsl:when test="normalize-space(POLICY/STATENAME)='MICHIGAN'">
										<xsl:call-template name="MEDPAY_MICHIGAN"></xsl:call-template>
									</xsl:when>
									<xsl:otherwise>0</xsl:otherwise>
								</xsl:choose>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'MED_OPT'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--Uninsured Motorist-->
						<STEP STEPID="10" CALC_ID="1010" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID10" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:choose>
									<xsl:when test="VEHICLES/VEHICLE/UMCSL !='' and VEHICLES/VEHICLE/UMCSL !='0' and VEHICLES/VEHICLE/UMCSL != 'NO COVERAGE'">
										<xsl:value-of select="VEHICLES/VEHICLE/UMCSL" />
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="VEHICLES/VEHICLE/UMSPLIT" />
									</xsl:otherwise>
								</xsl:choose>
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>UM</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="UM"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'UM'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--Underinsured Motorists-->
						<STEP STEPID="11" CALC_ID="1011" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID11" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:choose>
									<xsl:when test="VEHICLES/VEHICLE/UMCSL !=''  and VEHICLES/VEHICLE/UMCSL !='0' and VEHICLES/VEHICLE/UMCSL != 'NO COVERAGE'">
										<xsl:value-of select="VEHICLES/VEHICLE/UIMCSL" />
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="VEHICLES/VEHICLE/UIMSPLIT" />
									</xsl:otherwise>
								</xsl:choose>
							</L_PATH> <!-- change for combined SL -->
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>UIM</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="UIM"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'UIM'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--Uninsured Motorists - Property Damagae-->
						<STEP STEPID="12" CALC_ID="1012" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID12" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="VEHICLES/VEHICLE/PDDEDUCTIBLE != 0">
										<xsl:value-of select="VEHICLES/VEHICLE/PDDEDUCTIBLE" />
									</xsl:when>
									<xsl:otherwise></xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/PDLIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>UIMPD</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="UMPD"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'UIMPD'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--Comprehensive-->
						<STEP STEPID="13" CALC_ID="1013" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID13" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE" />
							</D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>COMP</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="COMP"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Collision -->
						<STEP STEPID="14" CALC_ID="1014" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID14" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/COLLISIONDEDUCTIBLE" />
							</D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>COLL</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="COLL"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Road Service -->
						<STEP STEPID="15" CALC_ID="1015" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID15" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/ROADSERVICE" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>RD_SRVC</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="ROADSERVICES"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'RD_SRVC'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Helmet and Riding Apparel (M-15) -->
						<STEP STEPID="16" CALC_ID="1016" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID16" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/HELMET" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>HELMET</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'HELMET'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Trailer Coverage (M-49) -->
						<STEP STEPID="17" CALC_ID="1017" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID17" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/MCYCLETRAILER" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>TRLR</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="TRAILER_COVERAGE_DISPLAY" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'TRLR'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--Additional Physical Damage Coverage(M-14)  -->
						<STEP STEPID="18" CALC_ID="1018" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID18" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/ADDLPD" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>ADD_PD</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="ADDITIONAL_PHYSICAL_DAMAGE" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'ADD_PD'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--Discount:Multi-Cycle-->
						<STEP STEPID="19" CALC_ID="1019" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID19" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_MLT_CYC</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKMULTICYCLE" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD>
								<xsl:call-template name="MULTIVEHICLE_CREDIT" />
							</COM_EXT_AD>
						</STEP>
						<!-- Discount:Transfer/Renewal -->
						<STEP STEPID="20" CALC_ID="1020" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID20" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_XFR</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKTRANSFERRENEWAL" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD>
								<xsl:call-template name="TRANSFER_RENEWAL_CREDIT" />
							</COM_EXT_AD>
						</STEP>
						<!-- Discount:Multi-Policy(Cycle -Auto/Home) -->
						<STEP STEPID="21" CALC_ID="1021" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID21" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_MLT_POL</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKMULTIPOLICY" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD>
								<xsl:call-template name="MULTIPOLICY_CREDIT" />
							</COM_EXT_AD>
						</STEP>
						<!-- Discount:Preferred Risk -->
						<STEP STEPID="22" CALC_ID="1022" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID22" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_PRFRD_RSK</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKPREFERDRISK" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD>
								<xsl:call-template name="EXT_AD_PREFERDRISK" />
							</COM_EXT_AD>
						</STEP>
						<!-- Discount:Mature Operator Credit -->
						<STEP STEPID="23" CALC_ID="1023" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID23" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_MTR_OPRT</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKMATUREOPERATOR" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD><xsl:call-template name="MATURE_CREDIT_PERCENT" /><xsl:text>%</xsl:text></COM_EXT_AD>
						</STEP>
						<!-- Discount:Insurance Score Credit -->
						<STEP STEPID="24" CALC_ID="1024" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID24" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_INS_SCR</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKINSURANCESCORE" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD>
								<xsl:call-template name="INSURANCESCORE_PERCENT" />
							</COM_EXT_AD>
						</STEP>
						<!-- Surcharge :No cycle Endorsement on License  -->
						<STEP STEPID="25" CALC_ID="1025" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID25" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>S</COMPONENT_TYPE>
							<COMPONENT_CODE>S_NO_CYCL_END</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKNOCYCLEENDORS" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD>
								<xsl:call-template name="NO_CYCLE_ENDORSEMENT_SURCHARGE_CREDIT" />
							</COM_EXT_AD>
						</STEP>
						<!-- Discount:Bike Type display  -->
						<STEP STEPID="26" CALC_ID="1026" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID26" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>
								<xsl:call-template name="BIKE_TYPE_COMPONENT_TYPE" />
							</COMPONENT_TYPE>
							<COMPONENT_CODE>D_BKE_TYP</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKBIKETYPETEXT" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD>
								<xsl:call-template name="EXT_ADBIKETYPEPERCENTAGE" />
							</COM_EXT_AD>
						</STEP>
						<!-- Surcharge: ACCIDENT and VIOLATION -->
						<STEP STEPID="28" CALC_ID="1028" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID28" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>S</COMPONENT_TYPE>
							<COMPONENT_CODE>S_ACCI_VIOL</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKACCIDENTVIOLATION" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD>
								<xsl:call-template name="ACCIDENT_VIOLATION_SURCHRAGE_PERCENTAGE" /><xsl:text>%</xsl:text>
							</COM_EXT_AD>
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
						<!-- MINIMUM Premium For Suspended Comp Vehicle-->
						<STEP STEPID="27" CALC_ID="1027" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID27" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MIN_PREM_SUS</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Total Premium -->
						<STEP STEPID="29" CALC_ID="1029" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID29" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>SUMTOTAL</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="FINALPREMIUM"></xsl:call-template>
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
	<!-- ######################################################################################################-->
	<!-- ######################################### FINAL PREMIUM ############################################## -->
	<!-- ######################################################################################################-->
	<xsl:template name="MINIMUMPREMIUM">
		<xsl:variable name="TOTALPREMIUM">
			<xsl:call-template name="GETPREMIUM" />
		</xsl:variable>
		
		<xsl:variable name="VAR_MIN_PREMIUM_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINIMUM_VEHICLE_PREMIUM_COMP_ONLY']/NODE[@ID='MINIMUM_PREMIUM_SCO']/ATTRIBUTES/@MIN_PREMIUM" />
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/COMPONLY='YES' and $TOTALPREMIUM &lt;= $VAR_MIN_PREMIUM_VALUE">
				<xsl:value-of select="$VAR_MIN_PREMIUM_VALUE - $TOTALPREMIUM " />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>				
	</xsl:template>
	<xsl:template name="FINALPREMIUM_1">
		<xsl:variable name="VAR_FINAL_PREM">
			<xsl:choose>
				<!-- For Michigan -Motorcycle-->
				<xsl:when test="POLICY/STATENAME ='MICHIGAN'">
					<xsl:call-template name="FINALPREMIUM_MICHIGAN" />
				</xsl:when>
				<!-- For Indiana -Motorcycle -->
				<xsl:when test="POLICY/STATENAME ='INDIANA'">
					<xsl:call-template name="FINALPREMIUM_INDIANA" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:value-of select="$VAR_FINAL_PREM"></xsl:value-of>
	</xsl:template>
	<xsl:template name="FINALPREMIUM">
		<!-- If the total policy premium is less than the minimum premium, 
			- subtract the total premium from the minimum to  get the difference
			- add the diff in premium to the total premium
				- If BI,PD is combined then add the difference in the CSL
				- Else Add 60% of difference in premium to the BI and remainder to PD.
		 -->
		<xsl:variable name="CALCULATEDPREMIUM">
			<xsl:call-template name="GETPREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_MIN_PREMIUM_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINIMUM_VEHICLE_PREMIUM_COMP_ONLY']/NODE[@ID='MINIMUM_PREMIUM_SCO']/ATTRIBUTES/@MIN_PREMIUM" />
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/COMPONLY='YES' and $CALCULATEDPREMIUM &lt;= $VAR_MIN_PREMIUM_VALUE">
				<xsl:value-of select="round($VAR_MIN_PREMIUM_VALUE)" />
			</xsl:when>
			<xsl:otherwise><xsl:value-of select="$CALCULATEDPREMIUM" /></xsl:otherwise>
		</xsl:choose>	
	</xsl:template>
	<xsl:template name="GETPREMIUM">
		<xsl:choose>
			<!-- For Michigan -Motorcycle-->
			<xsl:when test="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name="FINALPREMIUM_MICHIGAN" />
			</xsl:when>
			<!-- For Indiana -Motorcycle -->
			<xsl:when test="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name="FINALPREMIUM_INDIANA" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PREMIUM_DIFFERENCE">
		<xsl:variable name="POLICYTERM" select="POLICY/POLICYTERMS" />
		<!-- Fetch the minimum premium required. -->
		<xsl:variable name="MINIMUM_FINAL_PREMIUM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINIMUM_CYCLE_PREMIUM']/NODE[@ID ='MINIMUM_PREMIUM']/ATTRIBUTES[@MIN_MONTH &lt;= $POLICYTERM and @MAX_MONTH &gt;= $POLICYTERM]/@MIN_PREMIUM" />
		<!-- Check the final premium value -->
		<xsl:variable name="P_TEMP_FINAL_PREMIUM">
			<!-- Final Premium -->
			<xsl:variable name="VAR1">
				<xsl:choose>
					<xsl:when test="STATENAME = 'MICHIGAN'">
						<xsl:variable name="VAR_MCCA">
							<xsl:call-template name="MCCAFEE" />
						</xsl:variable>
						(<xsl:call-template name="FINALPREMIUM_MICHIGAN" /> - <xsl:value-of select="$VAR_MCCA" />)
					</xsl:when>
					<xsl:otherwise>
						<xsl:call-template name="FINALPREMIUM_INDIANA" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:variable>
			<xsl:value-of select="$VAR1" />
		</xsl:variable>
		<xsl:value-of select="$MINIMUM_FINAL_PREMIUM - $P_TEMP_FINAL_PREMIUM" />
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Final Premium - Motorcycle MICHIGAN ^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="FINALPREMIUM_MICHIGAN">
		<!-- If it is combined single limit then BI and PD will not be calculated separately.-->
		<xsl:variable name="VAR1">
			<xsl:choose>
				<xsl:when test="VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL=''">
					<xsl:variable name="VAR_BI">
						<xsl:call-template name="BI" />
					</xsl:variable>
					<xsl:variable name="VAR_PD">
						<xsl:call-template name="PD" />
					</xsl:variable>
					<xsl:value-of select="$VAR_BI+$VAR_PD" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:call-template name="CSLBIPD" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="MEDPAY" />
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:call-template name="UM" />
		</xsl:variable>
		<xsl:variable name="VAR4">
			<xsl:call-template name="COMP" />
		</xsl:variable>
		<xsl:variable name="VAR5">
			<xsl:call-template name="COLL" />
		</xsl:variable>
		<xsl:variable name="VAR6">
			<xsl:call-template name="ROADSERVICES" />
		</xsl:variable>
		<xsl:variable name="VAR7">
			<xsl:call-template name="TRAILER_COVERAGE_DISPLAY" />
		</xsl:variable>
		<xsl:variable name="VAR8">
			<xsl:call-template name="MCCAFEE" />
		</xsl:variable>
		<xsl:variable name="VAR9">
			<xsl:call-template name="ADDITIONAL_PHYSICAL_DAMAGE" />
		</xsl:variable>
		<xsl:value-of select="round(round(round(round(round(round(round(round($VAR1+$VAR2)+$VAR3)+$VAR4)+$VAR5)+$VAR6)+$VAR7)+$VAR8)+$VAR9)" />
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Final Premuim - Motorcycle MICHIGAN ^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Final Premuim - Motorcycle INDIANA ^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="FINALPREMIUM_INDIANA">
		<!-- If it is combined single limit then BI and PD will not be calculated separately.-->
		<xsl:variable name="VAR1">
			<xsl:choose>
				<xsl:when test="VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL=''">
					<xsl:variable name="VAR_BI">
						<xsl:call-template name="BI" />
					</xsl:variable>
					<xsl:variable name="VAR_PD">
						<xsl:call-template name="PD" />
					</xsl:variable>
					<xsl:value-of select="$VAR_BI+$VAR_PD" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:call-template name="CSLBIPD" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="MEDPAY" />
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:call-template name="UM" />
		</xsl:variable>
		<xsl:variable name="VAR4">
			<xsl:call-template name="UIM" />
		</xsl:variable>
		<xsl:variable name="VAR5">
			<xsl:call-template name="UMPD" />
		</xsl:variable>
		<xsl:variable name="VAR6">
			<xsl:call-template name="COMP" />
		</xsl:variable>
		<xsl:variable name="VAR7">
			<xsl:call-template name="COLL" />
		</xsl:variable>
		<xsl:variable name="VAR8">
			<xsl:call-template name="ROADSERVICES" />
		</xsl:variable>
		<xsl:variable name="VAR9">
			<xsl:call-template name="TRAILER_COVERAGE_DISPLAY" />
		</xsl:variable>
		<xsl:variable name="VAR10">
			<xsl:call-template name="ADDITIONAL_PHYSICAL_DAMAGE" />
		</xsl:variable>
		<xsl:value-of select="round(round(round(round(round(round(round(round(round($VAR1+$VAR2)+$VAR3)+$VAR4)+$VAR5)+$VAR6)+$VAR7)+$VAR8)+$VAR9)+$VAR10)" />
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Final Premuim - Motorcycle INDIANA ^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ######################################### END OF FINAL PREMIUM ################################################### -->
	<!-- ####################################################################################################################### -->
	<!-- ######################################### START OF OTHER TEMPLATES ################################################### -->
	<!-- ####################################################################################################################### -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ BodilyInjury. Michigan and Indiana ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="BI">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/COMPONLY = 'YES' or VEHICLES/VEHICLE/BI = '' or VEHICLES/VEHICLE/BI = '0' or VEHICLES/VEHICLE/BI = 'NO COVERAGE'  or VEHICLES/VEHICLE/BI = '0/0' ">0</xsl:when>
			<!-- <xsl:when test="POLICY/CSL = 0 or POLICY/CSL='' or POLICY/CSL='NO COVERAGE' "> -->
			<xsl:when test="VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL='' or VEHICLES/VEHICLE/CSL='NO COVERAGE' ">
				<!-- Multiply 
				1. Territory Base for BI 
				2. Class 
				3. CC Factor
				4. Limit Factor
				5. Preferred Risk
				6. Type of Bike(Credit/Surcharge)
				7. Transfer/Renewal Credit
				8. Mature Credit
				9. Accident/Violation Factor
				10. No Cycle Endorsement Surcharge
				11. Multi-Bike credit
				12. Score Credit
				13. Multi-Policy Credit
				14. Term of Policy Factor -->
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'BI'" />
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="CLASS">
						<xsl:with-param name="FACTORELEMENT" select="'BI'" />
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="CC">
						<xsl:with-param name="FACTORELEMENT" select="'BI'" />
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="LIMIT">
						<xsl:with-param name="FACTORELEMENT" select="'BI'" />
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="PREFERRED_RISK" />
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:call-template name="TYPE_OF_BIKE" />
				</xsl:variable>
				<xsl:variable name="VAR7">
					<xsl:call-template name="TRANSFER_RENEWAL" />
				</xsl:variable>
				<xsl:variable name="VAR8">
					<xsl:call-template name="MATURE_CREDIT" />
				</xsl:variable>
				<xsl:variable name="VAR9">
					<xsl:call-template name="ACCIDENT_VIOLATION_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR10">
					<xsl:call-template name="NO_CYCLE_ENDORSEMENT_SURCHARGE" />
				</xsl:variable>
				<xsl:variable name="VAR11">
					<xsl:call-template name="MULTIVEHICLE" />
				</xsl:variable>
				<xsl:variable name="VAR12">
					<xsl:call-template name="INSURANCESCORE" />
				</xsl:variable>
				<xsl:variable name="VAR13">
					<xsl:call-template name="MULTIPOLICY" />
				</xsl:variable>
				<xsl:variable name="VAR14">
					<xsl:call-template name="POLICYTERM" />
				</xsl:variable>
				<!--	<xsl:value-of select="round(round(round(round(round(round(round(round(round(round(round(round(round($VAR1*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)*$VAR12)*$VAR13)*$VAR14)" />
				-->
				<!--xsl:value-of select="round($VAR1*$VAR2*$VAR3*$VAR4*$VAR5*$VAR6*$VAR7*$VAR8*$VAR9*$VAR10*$VAR11*$VAR12*$VAR13*$VAR14)" /-->
				<xsl:variable name="FIRST_STEP" select="($VAR1*$VAR2)" />
				<xsl:variable name="VARFIS1" select="round(format-number($FIRST_STEP,'##.0000'))" />
				<xsl:variable name="SECOND_STEP" select="$VARFIS1 * $VAR3" />
				<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" />
				<xsl:variable name="THIRD_STEP" select="$VARSEC2 * $VAR4" />
				<xsl:variable name="VARTHI3" select="round(format-number($THIRD_STEP,'##.0000'))" />
				<xsl:variable name="FORTH_STEP" select="$VARTHI3 * $VAR5" />
				<xsl:variable name="VARFOR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
				<xsl:variable name="FIFTH_STEP" select="$VARFOR4 * $VAR6" />
				<xsl:variable name="VARFIF5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
				<xsl:variable name="SIX_STEP" select="$VARFIF5 * $VAR7" />
				<xsl:variable name="VARSIX6" select="round(format-number($SIX_STEP,'##.0000'))" />
				<xsl:variable name="SEVEN_STEP" select="$VARSIX6 * $VAR8" />
				<xsl:variable name="VARSEV7" select="round(format-number($SEVEN_STEP,'##.0000'))" />
				<xsl:variable name="EIGHT_STEP" select="$VARSEV7 * $VAR9" />
				<xsl:variable name="VAREIG8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
				<xsl:variable name="NINE_STEP" select="$VAREIG8 * $VAR10" />
				<xsl:variable name="VARNIN9" select="round(format-number($NINE_STEP,'##.0000'))" />
				<xsl:variable name="TENTH_STEP" select="$VARNIN9 * $VAR11" />
				<xsl:variable name="VARTEN10" select="round(format-number($TENTH_STEP,'##.0000'))" />
				<xsl:variable name="ELEVEN_STEP" select="$VARTEN10 * $VAR12" />
				<xsl:variable name="VARELE11" select="round(format-number($ELEVEN_STEP,'##.0000'))" />
				<xsl:variable name="TWELVE_STEP" select="$VARELE11 * $VAR13" />
				<xsl:variable name="VARTWE12" select="round(format-number($TWELVE_STEP,'##.0000'))" />
				<xsl:variable name="THERTEEN_STEP" select="$VARTWE12 * $VAR14" />
				<xsl:value-of select="round($THERTEEN_STEP)" />
			</xsl:when>
			<xsl:otherwise>
				0.00 
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Bodily Injury ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^-->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Property Damage ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^-->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="PD">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/COMPONLY = 'YES' or VEHICLES/VEHICLE/BI = 'NO COVERAGE' or VEHICLES/VEHICLE/PD = 'NO COVERAGE'  or VEHICLES/VEHICLE/PD = ''  or VEHICLES/VEHICLE/PD = '0'">0</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL='' or VEHICLES/VEHICLE/CSL='NO COVERAGE' ">
				<!-- Multiply 
				1. Territory Base for PD 
				2. Class 
				3. CC Factor
				4. Limit Factor
				5. Preferred Risk
				6. Type of Bike(Credit/Surcharge)
				7. Transfer/Renewal Credit
				8. Mature Credit
				9. Accident/Violation Factor
				10. No Cycle Endorsement Surcharge
				11. Multi-Bike credit
				12. Score Credit
				13. Multi-Policy Credit
				14. Term of Policy Factor -->
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'PD'" />
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="CLASS">
						<xsl:with-param name="FACTORELEMENT" select="'PD'" />
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="CC">
						<xsl:with-param name="FACTORELEMENT" select="'PD'" />
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="LIMIT">
						<xsl:with-param name="FACTORELEMENT" select="'PD'" />
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="PREFERRED_RISK" />
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:call-template name="TYPE_OF_BIKE" />
				</xsl:variable>
				<xsl:variable name="VAR7">
					<xsl:call-template name="TRANSFER_RENEWAL" />
				</xsl:variable>
				<xsl:variable name="VAR8">
					<xsl:call-template name="MATURE_CREDIT" />
				</xsl:variable>
				<xsl:variable name="VAR9">
					<xsl:call-template name="ACCIDENT_VIOLATION_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR10">
					<xsl:call-template name="NO_CYCLE_ENDORSEMENT_SURCHARGE" />
				</xsl:variable>
				<xsl:variable name="VAR11">
					<xsl:call-template name="MULTIVEHICLE" />
				</xsl:variable>
				<xsl:variable name="VAR12">
					<xsl:call-template name="INSURANCESCORE" />
				</xsl:variable>
				<xsl:variable name="VAR13">
					<xsl:call-template name="MULTIPOLICY" />
				</xsl:variable>
				<xsl:variable name="VAR14">
					<xsl:call-template name="POLICYTERM" />
				</xsl:variable>
				<!--xsl:value-of select="round(round(round(round(round(round(round(round(round(round(round(round(round(round($VAR1)*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)*$VAR12)*$VAR13)*$VAR14)" /-->
				<!--
			  		<xsl:value-of select ="$VAR1*$VAR2*$VAR3*$VAR4*$VAR5*$VAR6*$VAR7*$VAR8*$VAR9*$VAR10*$VAR11*$VAR12*$VAR13*$VAR14"/> 
 	 			-->
				<xsl:variable name="FIRST_STEP" select="($VAR1*$VAR2)" />
				<xsl:variable name="VARFIS1" select="round(format-number($FIRST_STEP,'##.0000'))" />
				<xsl:variable name="SECOND_STEP" select="$VARFIS1 * $VAR3" />
				<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" />
				<xsl:variable name="THIRD_STEP" select="$VARSEC2 * $VAR4" />
				<xsl:variable name="VARTHI3" select="round(format-number($THIRD_STEP,'##.0000'))" />
				<xsl:variable name="FORTH_STEP" select="$VARTHI3 * $VAR5" />
				<xsl:variable name="VARFOR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
				<xsl:variable name="FIFTH_STEP" select="$VARFOR4 * $VAR6" />
				<xsl:variable name="VARFIF5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
				<xsl:variable name="SIX_STEP" select="$VARFIF5 * $VAR7" />
				<xsl:variable name="VARSIX6" select="round(format-number($SIX_STEP,'##.0000'))" />
				<xsl:variable name="SEVEN_STEP" select="$VARSIX6 * $VAR8" />
				<xsl:variable name="VARSEV7" select="round(format-number($SEVEN_STEP,'##.0000'))" />
				<xsl:variable name="EIGHT_STEP" select="$VARSEV7 * $VAR9" />
				<xsl:variable name="VAREIG8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
				<xsl:variable name="NINE_STEP" select="$VAREIG8 * $VAR10" />
				<xsl:variable name="VARNIN9" select="round(format-number($NINE_STEP,'##.0000'))" />
				<xsl:variable name="TENTH_STEP" select="$VARNIN9 * $VAR11" />
				<xsl:variable name="VARTEN10" select="round(format-number($TENTH_STEP,'##.0000'))" />
				<xsl:variable name="ELEVEN_STEP" select="$VARTEN10 * $VAR12" />
				<xsl:variable name="VARELE11" select="round(format-number($ELEVEN_STEP,'##.0000'))" />
				<xsl:variable name="TWELVE_STEP" select="$VARELE11 * $VAR13" />
				<xsl:variable name="VARTWE12" select="round(format-number($TWELVE_STEP,'##.0000'))" />
				<xsl:variable name="THERTEEN_STEP" select="$VARTWE12 * $VAR14" />
				<xsl:variable name="VARTHE13" select="round(format-number($THERTEEN_STEP,'##.0000'))" />
				<xsl:value-of select="round($VARTHE13)" />
			</xsl:when>
			<xsl:otherwise>
			0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Property Damage ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^-->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Combined Single Limit (BI AND PD) ^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="CSLBIPD">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/CSL = '0' or VEHICLES/VEHICLE/CSL='' or VEHICLES/VEHICLE/CSL='NO COVERAGE'  or VEHICLES/VEHICLE/COMPONLY = 'YES' ">0</xsl:when>
			<xsl:otherwise>
				<!-- Multiply 
				1. Sum of Territory Base for BI and PD
				2. Class 
				3. CC(BI) Factor
				4. CSL Limit Factor
				5. Preferred Risk
				6. Type of Bike(Credit/Surcharge)
				7. Transfer/Renewal Credit
				8. Mature Credit
				9. Accident/Violation Factor
				10. No Cycle Endorsement Surcharge
				11. Multi-Bike credit
				12. Score Credit
				13. Multi-Policy Credit
				14. Term of Policy Factor -->
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="CLASS">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="CC">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="CSL_LIMIT" />
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:call-template name="PREFERRED_RISK" />
				</xsl:variable>
				<xsl:variable name="VAR7">
					<xsl:call-template name="TYPE_OF_BIKE" />
				</xsl:variable>
				<xsl:variable name="VAR8">
					<xsl:call-template name="TRANSFER_RENEWAL" />
				</xsl:variable>
				<xsl:variable name="VAR9">
					<xsl:call-template name="MATURE_CREDIT" />
				</xsl:variable>
				<xsl:variable name="VAR10">
					<xsl:call-template name="ACCIDENT_VIOLATION_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR11">
					<xsl:call-template name="NO_CYCLE_ENDORSEMENT_SURCHARGE" />
				</xsl:variable>
				<xsl:variable name="VAR12">
					<xsl:call-template name="MULTIVEHICLE" />
				</xsl:variable>
				<xsl:variable name="VAR13">
					<xsl:call-template name="INSURANCESCORE" />
				</xsl:variable>
				<xsl:variable name="VAR14">
					<xsl:call-template name="MULTIPOLICY" />
				</xsl:variable>
				<xsl:variable name="VAR15">
					<xsl:call-template name="POLICYTERM" />
				</xsl:variable>
				<!--
				<xsl:value-of select ="round(round(round(round(round(round(round(round(round(round(round(round(round(round(round($VAR1+$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)*$VAR12)*$VAR13)*$VAR14)*$VAR15)*$VAR16)"/>
			-->
				<!--xsl:value-of select="($VAR1+$VAR2)*$VAR3*$VAR4*$VAR5*$VAR6*$VAR7*$VAR8*$VAR9*$VAR10*$VAR11*$VAR12*$VAR13*$VAR14*$VAR15" /-->
				<xsl:variable name="FIRST_STEP" select="($VAR1+$VAR2)" />
				<xsl:variable name="VARFIS1" select="round(format-number($FIRST_STEP,'##.0000'))" />
				<!--xsl:variable name="SECOND_STEP" select="$VARFIS1 * $VAR11" />
				<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" /-->
				<xsl:variable name="THIRD_STEP" select="$VARFIS1 * $VAR3" />
				<xsl:variable name="VARTHI3" select="round(format-number($THIRD_STEP,'##.0000'))" />
				<xsl:variable name="FORTH_STEP" select="$VARTHI3 * $VAR4" />
				<xsl:variable name="VARFOR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
				<xsl:variable name="FIFTH_STEP" select="$VARFOR4 * $VAR5" />
				<xsl:variable name="VARFIF5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
				<xsl:variable name="SIX_STEP" select="$VARFIF5 * $VAR6" />
				<xsl:variable name="VARSIX6" select="round(format-number($SIX_STEP,'##.0000'))" />
				<xsl:variable name="SEVEN_STEP" select="$VARSIX6 * $VAR7" />
				<xsl:variable name="VARSEV7" select="round(format-number($SEVEN_STEP,'##.0000'))" />
				<xsl:variable name="EIGHT_STEP" select="$VARSEV7 * $VAR8" />
				<xsl:variable name="VAREIG8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
				<xsl:variable name="NINE_STEP" select="$VAREIG8 * $VAR9" />
				<xsl:variable name="VARNIN9" select="round(format-number($NINE_STEP,'##.0000'))" />
				<xsl:variable name="TENTH_STEP" select="$VARNIN9 * $VAR10" />
				<xsl:variable name="VARTEN10" select="round(format-number($TENTH_STEP,'##.0000'))" />
				<xsl:variable name="SECOND_STEP" select="$VARTEN10 * $VAR11" />
				<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" />
				<xsl:variable name="ELEVEN_STEP" select="$VARSEC2 * $VAR12" />
				<xsl:variable name="VARELE11" select="round(format-number($ELEVEN_STEP,'##.0000'))" />
				<xsl:variable name="TWELVE_STEP" select="$VARELE11 * $VAR13" />
				<xsl:variable name="VARTWE12" select="round(format-number($TWELVE_STEP,'##.0000'))" />
				<xsl:variable name="THERTEEN_STEP" select="$VARTWE12 * $VAR14" />
				<xsl:variable name="VARTHE13" select="round(format-number($THERTEEN_STEP,'##.0000'))" />
				<xsl:variable name="FOURTEEN_STEP" select="$VARTHE13 * $VAR15" />
				<xsl:variable name="VARFOUR14" select="round(format-number($FOURTEEN_STEP,'##.0000'))" />
				<xsl:value-of select="round($VARFOUR14)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Combined Single Limit (BI and PD) ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Medical Payments ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="MEDPAY">
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME = 'MICHIGAN'">
				<xsl:call-template name="MEDPAY_MICHIGAN" />
			</xsl:when>
			<xsl:when test="POLICY/STATENAME = 'INDIANA'">
				<xsl:call-template name="MEDPAY_INDIANA" />
			</xsl:when>
			<xsl:otherwise>
			0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MEDPAY_MICHIGAN">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/TYPE = 'NO COVERAGE' or VEHICLES/VEHICLE/TYPE ='' or VEHICLES/VEHICLE/COMPONLY = 'YES' ">0</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/MEDPMLIMIT != 0 and  VEHICLES/VEHICLE/MEDPMLIMIT !='' and VEHICLES/VEHICLE/MEDPMLIMIT != 'NO COVERAGE' ">
				<xsl:variable name="PMEDICALTYPE">
					<xsl:value-of select="VEHICLES/VEHICLE/MEDICALTYPE" />
				</xsl:variable>
				<xsl:variable name="PMEDICALDEDUCTIBLE">
					<xsl:value-of select="normalize-space(VEHICLES/VEHICLE/MEDICALDEDUCTIBLE)" />
				</xsl:variable>
				<xsl:variable name="PMEDICALDESCRIPTION">
					<xsl:value-of select="VEHICLES/VEHICLE/TYPE" />
				</xsl:variable>
				<xsl:variable name="PMEDICALLIMIT">
					<xsl:value-of select="VEHICLES/VEHICLE/MEDPMLIMIT" />
				</xsl:variable>
				<xsl:variable name="MIN_CC" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MEDICALOPTIONS']/NODE[@ID = 'MEDICALOPTIONS_FACTOR' ]/ATTRIBUTES[@TYPE = $PMEDICALTYPE and @DEDUCTIBLE=$PMEDICALDEDUCTIBLE ]/@MIN_CC" />
				<xsl:variable name="VAR_CC" select="VEHICLES/VEHICLE/CC" />
				<xsl:variable name="VAR_CC_M">
					<xsl:value-of select="translate($VAR_CC,'###,###','#')" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$VAR_CC_M &gt; $MIN_CC">
						<!-- Multiply 
					1. Territory Base for MedPay 
					2. Class 
					3. CC Factor
					4. Limit Factor (Medical Payments)
					5. Preferred Risk
					6. Type of Bike(Credit/Surcharge)
					7. Transfer/Renewal Credit
					8. Mature Credit
					9. Accident/Violation Factor
					10. No Cycle Endorsement Surcharge
					11. Multi-Bike credit
					12. Score Credit
					13. Multi-Policy Credit
					14. Term of Policy Factor -->
						<!-- new calculation using round by  praveen singh  -->
						<xsl:variable name="VAR1">
							<xsl:call-template name="TERRITORYBASE">
								<xsl:with-param name="FACTORELEMENT" select="'MEDPAY'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:call-template name="CLASS">
								<xsl:with-param name="FACTORELEMENT" select="'MEDPAY'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR3">
							<xsl:call-template name="CC">
								<xsl:with-param name="FACTORELEMENT" select="'MEDPAY'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR4">
							<xsl:call-template name="LIMIT">
								<xsl:with-param name="FACTORELEMENT" select="'MEDPAYOPTION'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR5">
							<xsl:call-template name="MEDPAYLIMIT_INCREASEDLIMITS"/>
						</xsl:variable>
						<xsl:variable name="VAR6">
							<xsl:call-template name="PREFERRED_RISK" />
						</xsl:variable>
						<xsl:variable name="VAR7">
							<xsl:call-template name="TYPE_OF_BIKE" />
						</xsl:variable>
						<xsl:variable name="VAR8">
							<xsl:call-template name="TRANSFER_RENEWAL" />
						</xsl:variable>
						<xsl:variable name="VAR9">
							<xsl:call-template name="MATURE_CREDIT" />
						</xsl:variable>
						<xsl:variable name="VAR10">
							<xsl:call-template name="ACCIDENT_VIOLATION_FACTOR" />
						</xsl:variable>
						<xsl:variable name="VAR11">
							<xsl:call-template name="NO_CYCLE_ENDORSEMENT_SURCHARGE" />
						</xsl:variable>
						<xsl:variable name="VAR12">
							<xsl:call-template name="MULTIVEHICLE" />
						</xsl:variable>
						<xsl:variable name="VAR13">
							<xsl:call-template name="INSURANCESCORE" />
						</xsl:variable>
						<xsl:variable name="VAR14">
							<xsl:call-template name="MULTIPOLICY" />
						</xsl:variable>
						<xsl:variable name="VAR15">
							<xsl:call-template name="POLICYTERM" />
						</xsl:variable>
						<!--xsl:value-of select="round($VAR1*$VAR2*$VAR3*$VAR4*$VAR5*$VAR6*$VAR7*$VAR8*$VAR9*$VAR10*$VAR11*$VAR12*$VAR13*$VAR14)" /-->
						<xsl:variable name="FIRST_STEP" select="($VAR1*$VAR2)" />
						<xsl:variable name="VARFIS1" select="round(format-number($FIRST_STEP,'##.0000'))" />
						<xsl:variable name="SECOND_STEP" select="$VARFIS1 * $VAR3" />
						<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" />
						<xsl:variable name="THIRD_STEP" select="$VARSEC2 * $VAR4" />
						<xsl:variable name="VARTHI3" select="round(format-number($THIRD_STEP,'##.0000'))" />
						<xsl:variable name="FORTH_STEP" select="$VARTHI3 * $VAR5" />
						<xsl:variable name="VARFOR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
						<xsl:variable name="FIFTH_STEP" select="$VARFOR4 * $VAR6" />
						<xsl:variable name="VARFIF5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
						<xsl:variable name="SIX_STEP" select="$VARFIF5 * $VAR7" />
						<xsl:variable name="VARSIX6" select="round(format-number($SIX_STEP,'##.0000'))" />
						<xsl:variable name="SEVEN_STEP" select="$VARSIX6 * $VAR8" />
						<xsl:variable name="VARSEV7" select="round(format-number($SEVEN_STEP,'##.0000'))" />
						<xsl:variable name="EIGHT_STEP" select="$VARSEV7 * $VAR9" />
						<xsl:variable name="VAREIG8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
						<xsl:variable name="NINE_STEP" select="$VAREIG8 * $VAR10" />
						<xsl:variable name="VARNIN9" select="round(format-number($NINE_STEP,'##.0000'))" />
						<xsl:variable name="TENTH_STEP" select="$VARNIN9 * $VAR11" />
						<xsl:variable name="VARTEN10" select="round(format-number($TENTH_STEP,'##.0000'))" />
						<xsl:variable name="ELEVEN_STEP" select="$VARTEN10 * $VAR12" />
						<xsl:variable name="VARELE11" select="round(format-number($ELEVEN_STEP,'##.0000'))" />
						<xsl:variable name="TWELVE_STEP" select="$VARELE11 * $VAR13" />
						<xsl:variable name="VARTWE12" select="round(format-number($TWELVE_STEP,'##.0000'))" />
						<xsl:variable name="THERTEEN_STEP" select="$VARTWE12 * $VAR14" />
						<xsl:variable name="VARTHE13" select="round(format-number($THERTEEN_STEP,'##.0000'))" />
						<xsl:variable name="FOURTEEN_STEP" select="$VARTHE13 * $VAR15" />
						<xsl:variable name="VARTHE14" select="round(format-number($FOURTEEN_STEP,'##.0000'))" />
						<xsl:value-of select="round($VARTHE14)" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MEDPAY_INDIANA">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/MEDPMTYPE = '' or VEHICLES/VEHICLE/MEDPMTYPE = 'NO COVERAGE' or VEHICLES/VEHICLE/COMPONLY = 'YES' ">0</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/MEDPM != 0 and VEHICLES/VEHICLE/MEDPM !='' ">
				<!-- Multiply 
				1. Territory Base for MEDPAY 
				2. Class 
				3. CC Factor
				4. Limit Factor (Medical Payments)
				5. Preferred Risk
				6. Type of Bike(Credit/Surcharge)
				7. Transfer/Renewal Credit
				8. Mature Credit
				9. Accident/Violation Factor
			    10. No Cycle Endorsement Surcharge
				11. Multi-Bike credit
				12. Score Credit
				13. Multi-Policy Credit
				14. Term of Policy Factor -->
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'MEDPAY'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="CLASS">
						<xsl:with-param name="FACTORELEMENT" select="'MEDPAY'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="CC">
						<xsl:with-param name="FACTORELEMENT" select="'MEDPAY'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="LIMIT">
						<xsl:with-param name="FACTORELEMENT" select="'MEDPAYCOVGLIMIT'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="MEDPAYLIMIT_INCREASEDLIMITS"/>
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:call-template name="PREFERRED_RISK" />
				</xsl:variable>
				<xsl:variable name="VAR7">
					<xsl:call-template name="TYPE_OF_BIKE" />
				</xsl:variable>
				<xsl:variable name="VAR8">
					<xsl:call-template name="TRANSFER_RENEWAL" />
				</xsl:variable>
				<xsl:variable name="VAR9">
					<xsl:call-template name="MATURE_CREDIT" />
				</xsl:variable>
				<xsl:variable name="VAR10">
					<xsl:call-template name="ACCIDENT_VIOLATION_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR11">
					<xsl:call-template name="NO_CYCLE_ENDORSEMENT_SURCHARGE" />
				</xsl:variable>
				<xsl:variable name="VAR12">
					<xsl:call-template name="MULTIVEHICLE" />
				</xsl:variable>
				<xsl:variable name="VAR13">
					<xsl:call-template name="INSURANCESCORE" />
				</xsl:variable>
				<xsl:variable name="VAR14">
					<xsl:call-template name="MULTIPOLICY" />
				</xsl:variable>
				<xsl:variable name="VAR15">
					<xsl:call-template name="POLICYTERM" />
				</xsl:variable>
				<!--xsl:value-of select="$VAR1*$VAR2*$VAR3*$VAR4*$VAR5*$VAR6*$VAR7*$VAR8*$VAR9*$VAR10*$VAR11*$VAR12*$VAR13*$VAR14" /-->
				<xsl:variable name="FIRST_STEP" select="($VAR1*$VAR2)" />
				<xsl:variable name="VARFIS1" select="round(format-number($FIRST_STEP,'##.0000'))" />
				<xsl:variable name="SECOND_STEP" select="$VARFIS1 * $VAR3" />
				<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" />
				<xsl:variable name="THIRD_STEP" select="$VARSEC2 * $VAR4" />
				<xsl:variable name="VARTHI3" select="round(format-number($THIRD_STEP,'##.0000'))" />
				<xsl:variable name="FORTH_STEP" select="$VARTHI3 * $VAR5" />
				<xsl:variable name="VARFOR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
				<xsl:variable name="FIFTH_STEP" select="$VARFOR4 * $VAR6" />
				<xsl:variable name="VARFIF5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
				<xsl:variable name="SIX_STEP" select="$VARFIF5 * $VAR7" />
				<xsl:variable name="VARSIX6" select="round(format-number($SIX_STEP,'##.0000'))" />
				<xsl:variable name="SEVEN_STEP" select="$VARSIX6 * $VAR8" />
				<xsl:variable name="VARSEV7" select="round(format-number($SEVEN_STEP,'##.0000'))" />
				<xsl:variable name="EIGHT_STEP" select="$VARSEV7 * $VAR9" />
				<xsl:variable name="VAREIG8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
				<xsl:variable name="NINE_STEP" select="$VAREIG8 * $VAR10" />
				<xsl:variable name="VARNIN9" select="round(format-number($NINE_STEP,'##.0000'))" />
				<xsl:variable name="TENTH_STEP" select="$VARNIN9 * $VAR11" />
				<xsl:variable name="VARTEN10" select="round(format-number($TENTH_STEP,'##.0000'))" />
				<xsl:variable name="ELEVEN_STEP" select="$VARTEN10 * $VAR12" />
				<xsl:variable name="VARELE11" select="round(format-number($ELEVEN_STEP,'##.0000'))" />
				<xsl:variable name="TWELVE_STEP" select="$VARELE11 * $VAR13" />
				<xsl:variable name="VARTWE12" select="round(format-number($TWELVE_STEP,'##.0000'))" />
				<xsl:variable name="THERTEEN_STEP" select="$VARTWE12 * $VAR14" />
				<xsl:variable name="VARTHE13" select="round(format-number($THERTEEN_STEP,'##.0000'))" />
				<xsl:variable name="FOURTEEN_STEP" select="$VARTHE13 * $VAR15"/>
				<xsl:variable name="VARTHE14" select="round(format-number($FOURTEEN_STEP,'##.0000'))"/>
				<xsl:value-of select="round($VARTHE14)" />
			</xsl:when>
			<xsl:otherwise>
		0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Medical Payments ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Uninsured Motorists ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="UM">
		<xsl:choose>
			<!-- For Michigan -Motorcycle -->
			<xsl:when test="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name="UM_MICHIGAN" />
			</xsl:when>
			<!-- For Indiana -Motorcycle -->
			<xsl:when test="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name="UM_INDIANA" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="UM_MICHIGAN">
		<xsl:choose>
			<xsl:when test="(VEHICLES/VEHICLE/UMSPLIT ='' or VEHICLES/VEHICLE/UMSPLIT ='0/0' or VEHICLES/VEHICLE/UMSPLIT ='NO CONVERAGE' ) and (VEHICLES/VEHICLE/UMCSL ='' or VEHICLES/VEHICLE/UMCSL ='0' or VEHICLES/VEHICLE/UMCSL ='NO COVERAGE') or VEHICLES/VEHICLE/COMPONLY = 'YES' ">0</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'UM'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:choose>
						<xsl:when test="(VEHICLES/VEHICLE/UMSPLIT !='' and VEHICLES/VEHICLE/UMSPLIT !='0/0' and  VEHICLES/VEHICLE/UMSPLIT !='0' and VEHICLES/VEHICLE/UMSPLIT !='NO COVERAGE')"> <!-- Split Limit -->
							<xsl:call-template name="LIMIT">
								<xsl:with-param name="FACTORELEMENT" select="'UM_MICHIGAN_SPLIT'" />
							</xsl:call-template>
						</xsl:when>
						<xsl:when test="VEHICLES/VEHICLE/UMCSL !='' and VEHICLES/VEHICLE/UMCSL !='0' and VEHICLES/VEHICLE/UMCSL !='NO COVERAGE' "> <!-- Split Limit -->
							<xsl:call-template name="LIMIT">
								<xsl:with-param name="FACTORELEMENT" select="'UM_MICHIGAN_CSL'" />
							</xsl:call-template>
						</xsl:when>
						<xsl:otherwise>0.00</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="POLICYTERM" />
				</xsl:variable>
				<xsl:variable name="FINALVALUE">
					<xsl:value-of select="round($VAR1 * $VAR2 * $VAR3)" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$FINALVALUE = 1.00"> 
					0
			</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$FINALVALUE" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="UM_INDIANA">
		<!-- Check if  it is 
			1. No coverage
			2. BI Only
			3. BI and PD -->
		<xsl:variable name="VAR1">
			<xsl:choose>
				<xsl:when test="VEHICLES/VEHICLE/TYPE ='' or VEHICLES/VEHICLE/TYPE ='NO COVERAGE' or  VEHICLES/VEHICLE/COMPONLY = 'YES' ">0</xsl:when> <!-- No Coverage -->
				<xsl:when test="(VEHICLES/VEHICLE/UMSPLIT !='' and VEHICLES/VEHICLE/UMSPLIT !='0/0' and VEHICLES/VEHICLE/UMSPLIT !='0' and VEHICLES/VEHICLE/UMSPLIT !='NO COVERAGE') "> <!-- Split Limit -->
					<xsl:call-template name="LIMIT">
						<xsl:with-param name="FACTORELEMENT" select="'UM_INDIANA_SPLIT'" />
					</xsl:call-template>
				</xsl:when>
				<xsl:when test="VEHICLES/VEHICLE/UMCSL !='' and VEHICLES/VEHICLE/UMCSL !='0' and VEHICLES/VEHICLE/UMCSL !='NO COVERAGE' "> <!-- Split Limit -->
					<xsl:call-template name="LIMIT">
						<xsl:with-param name="FACTORELEMENT" select="'UM_INDIANA_CSL'" />
					</xsl:call-template>
				</xsl:when>
				<xsl:otherwise>0.00</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="POLICYTERM" />
		</xsl:variable>
		<xsl:value-of select="round($VAR1*$VAR2)" />
	</xsl:template>
	<!--Start of UM Property Damage -->
	<xsl:template name="UMPD">
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='INDIANA'">
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/TYPE ='NO COVERAGE' or VEHICLES/VEHICLE/TYPE ='' or VEHICLES/VEHICLE/COMPONLY = 'YES'">0</xsl:when>
					<xsl:when test="(VEHICLES/VEHICLE/TYPE != ''and VEHICLES/VEHICLE/TYPE !=normalize-space('BI ONLY') and VEHICLES/VEHICLE/PDLIMIT &gt; 0)"> <!-- UMPD -->
						<xsl:variable name="PPDDEDUCTIBLE" select="VEHICLES/VEHICLE/PDDEDUCTIBLE" />
						<xsl:variable name="PPDLIMIT" select="VEHICLES/VEHICLE/PDLIMIT" />
						<xsl:variable name="LRANGE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMPDRANGE']/NODE[@ID='DEDUCTIBLERANGE']/ATTRIBUTES/@LOWERRANGE" />
						<xsl:variable name="HRANGE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMPDRANGE']/NODE[@ID='DEDUCTIBLERANGE']/ATTRIBUTES/@HIGHERRANGE" />
						<xsl:variable name="VAR1">
							<xsl:choose>
								<xsl:when test="$PPDDEDUCTIBLE = $LRANGE">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMPDLIMITS']/NODE[@ID='UMPD']/ATTRIBUTES[@LIMIT =$PPDLIMIT]/@DEDUCTIBLE_0" />
								</xsl:when>
								<xsl:when test="$PPDDEDUCTIBLE = $HRANGE">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMPDLIMITS']/NODE[@ID='UMPD']/ATTRIBUTES[@LIMIT =$PPDLIMIT]/@DEDUCTIBLE_300" />
								</xsl:when>
								<xsl:otherwise>0.00</xsl:otherwise>
							</xsl:choose>
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:call-template name="POLICYTERM" />
						</xsl:variable>
						<xsl:value-of select="round($VAR1*$VAR2)" />
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
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Uninsured Motorists ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Underinsured Motorists ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="UIM">
		<xsl:variable name="VAR1">
			<xsl:choose>
				<!--Add UIM only if IsUnderInsuredMotorist is true and this is applicalble only in Indiana state -->
				<xsl:when test="VEHICLES/VEHICLE/COMPONLY = 'YES'">0.00</xsl:when>
				<xsl:when test="POLICY/STATENAME='INDIANA' and VEHICLES/VEHICLE/ISUNDERINSUREDMOTORISTS ='Y'">
					<xsl:call-template name="LIMIT">
						<xsl:with-param name="FACTORELEMENT" select="'UIM'" />
					</xsl:call-template>
				</xsl:when>
				<xsl:otherwise>
					0.00
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="POLICYTERM" />
		</xsl:variable>
		<xsl:value-of select="round($VAR1 * $VAR2)" />
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Underinsured Motorists ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Comprehensive ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="COMP">
		<xsl:variable name="P_MAXAGE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEAGEGROUPOTHER']/NODE[@ID='VEHICLEAGEGROUPCOMP']/@MAXAGE" />
		<xsl:choose>
			<!--Any Cycle over 25 years old are not eligible for Comprehensive and Collision coverages. ASFA PRAVEEN (05-JUNE-2007)  -->
			<xsl:when test="VEHICLES/VEHICLE/AGE &lt;= $P_MAXAGE">
				<xsl:choose>
					<!-- For Michigan - Motorcycle  -->
					<xsl:when test="POLICY/STATENAME ='MICHIGAN'">
						<xsl:call-template name="COMP_MICHIGAN" />
					</xsl:when>
					<!-- For Indiana - Motorcycle.. can call a separate template if the logic for calculation in different states changes. -->
					<xsl:when test="POLICY/STATENAME ='INDIANA'">
						<xsl:call-template name="COMP_MICHIGAN" />
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="COMP_MICHIGAN">
		<!-- Multiply 
			1. Territory Base
			2. Class
			3. CC Factor
			4. Deductible percentage
			5. Age
			6. Preferred Risk
			7. Type of Bike
			8. Transfer/Renewal Credit
			9. Mature Credit
			10.Accident/Violation surharge
			11.No cycle endorsement surcharge 
			12. Multibike credit
			13. Score Credit
			14. Multi-Policy Credit
			15. Term of Policy Factor	-->
		<xsl:variable name="VAR_COMP_DEDUC" select="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE" />
		<xsl:choose>
			<xsl:when test="$VAR_COMP_DEDUC &gt; 0">
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="CLASS">
						<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="CC">
						<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="DEDUCTIBLE">
						<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="AGE">
						<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:call-template name="PREFERRED_RISK" />
				</xsl:variable>
				<xsl:variable name="VAR7">
					<xsl:call-template name="TYPE_OF_BIKE" />
				</xsl:variable>
				<xsl:variable name="VAR8">
					<xsl:call-template name="TRANSFER_RENEWAL" />
				</xsl:variable>
				<xsl:variable name="VAR9">
					<xsl:call-template name="MATURE_CREDIT" />
				</xsl:variable>
				<xsl:variable name="VAR10">
					<xsl:call-template name="ACCIDENT_VIOLATION_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR11">
					<xsl:call-template name="NO_CYCLE_ENDORSEMENT_SURCHARGE" />
				</xsl:variable>
				<xsl:variable name="VAR12">
					<xsl:call-template name="MULTIVEHICLE" />
				</xsl:variable>
				<xsl:variable name="VAR13">
					<xsl:call-template name="INSURANCESCORE" />
				</xsl:variable>
				<xsl:variable name="VAR14">
					<xsl:call-template name="MULTIPOLICY" />
				</xsl:variable>
				<xsl:variable name="VAR15">
					<xsl:call-template name="POLICYTERM" />
				</xsl:variable>
				<!--xsl:value-of select="round($VAR1*$VAR2*$VAR3*$VAR4*$VAR5*$VAR6*$VAR7*$VAR8*$VAR9*$VAR10*$VAR11*$VAR12*$VAR13*$VAR14*$VAR15)" /-->
				<xsl:variable name="FIRST_STEP" select="($VAR1*$VAR2)" />
				<xsl:variable name="VARFIS1" select="round(format-number($FIRST_STEP,'##.0000'))" />
				<xsl:variable name="SECOND_STEP" select="$VARFIS1 * $VAR3" />
				<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" />
				<xsl:variable name="THIRD_STEP" select="$VARSEC2 * $VAR4" />
				<xsl:variable name="VARTHI3" select="round(format-number($THIRD_STEP,'##.0000'))" />
				<xsl:variable name="FORTH_STEP" select="$VARTHI3 * $VAR5" />
				<xsl:variable name="VARFOR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
				<xsl:variable name="FIFTH_STEP" select="$VARFOR4 * $VAR6" />
				<xsl:variable name="VARFIF5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
				<xsl:variable name="SIX_STEP" select="$VARFIF5 * $VAR7" />
				<xsl:variable name="VARSIX6" select="round(format-number($SIX_STEP,'##.0000'))" />
				<xsl:variable name="SEVEN_STEP" select="$VARSIX6 * $VAR8" />
				<xsl:variable name="VARSEV7" select="round(format-number($SEVEN_STEP,'##.0000'))" />
				<xsl:variable name="EIGHT_STEP" select="$VARSEV7 * $VAR9" />
				<xsl:variable name="VAREIG8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
				<xsl:variable name="NINE_STEP" select="$VAREIG8 * $VAR10" />
				<xsl:variable name="VARNIN9" select="round(format-number($NINE_STEP,'##.0000'))" />
				<xsl:variable name="TENTH_STEP" select="$VARNIN9 * $VAR11" />
				<xsl:variable name="VARTEN10" select="round(format-number($TENTH_STEP,'##.0000'))" />
				<xsl:variable name="ELEVEN_STEP" select="$VARTEN10 * $VAR12" />
				<xsl:variable name="VARELE11" select="round(format-number($ELEVEN_STEP,'##.0000'))" />
				<xsl:variable name="TWELVE_STEP" select="$VARELE11 * $VAR13" />
				<xsl:variable name="VARTWE12" select="round(format-number($TWELVE_STEP,'##.0000'))" />
				<xsl:variable name="THERTEEN_STEP" select="$VARTWE12 * $VAR14" />
				<xsl:variable name="VARTHE13" select="round(format-number($THERTEEN_STEP,'##.0000'))" />
				<xsl:variable name="FOURTEEN_STEP" select="$VARTHE13 * $VAR15" />
				<xsl:variable name="VARFOURTEEN14" select="round(format-number($FOURTEEN_STEP,'##.0000'))" />
				<xsl:value-of select="round($VARFOURTEEN14)" />				
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Comprehensive ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Collision ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="COLL">
		<xsl:variable name="P_MAXAGE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEAGEGROUPCOLLISION']/NODE[@ID='VEHICLEAGEGROUPCOLL']/@MAXAGE" />
		<xsl:choose>
			<!--Any Cycle over 25 years old are not eligible for Comprehensive and Collision coverages. ASFA PRAVEEN (05-JUNE-2007)  -->
			<xsl:when test="VEHICLES/VEHICLE/AGE &lt;= $P_MAXAGE">
				<xsl:choose>
					<!-- For Michigan - Motorcycle  -->
					<xsl:when test="normalize-space(POLICY/STATENAME) ='MICHIGAN'">
						<xsl:call-template name="COLL_MICHIGAN" />
					</xsl:when>
					<!-- For Indiana - Motorcycle -->
					<xsl:when test="normalize-space(POLICY/STATENAME) ='INDIANA'">
						<xsl:call-template name="COLL_MICHIGAN" />
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="COLL_MICHIGAN">
		<!-- Multiply 
			1. Territory Base
			2. Class
			3. CC Factor
			4. Deductible percentage
			5. Age
			6. Preferred Risk
			7. Type of Bike
			8. Transfer/Renewal Credit
			9. Mature Credit
			10.Accident/Violation surharge
			11.No cycle endorsement surcharge 
			12. Multibike credit
			13. Score Credit
			14. Multi-Policy Credit
			15. Term of Policy Factor	-->
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/COLLISIONDEDUCTIBLE !='' or VEHICLES/VEHICLE/COLLISIONDEDUCTIBLE != '0' or VEHICLES/VEHICLE/COMPONLY = 'YES'">
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="CLASS">
						<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="CC">
						<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="DEDUCTIBLE">
						<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="AGE">
						<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:call-template name="PREFERRED_RISK" />
				</xsl:variable>
				<xsl:variable name="VAR7">
					<xsl:call-template name="TYPE_OF_BIKE" />
				</xsl:variable>
				<xsl:variable name="VAR8">
					<xsl:call-template name="TRANSFER_RENEWAL" />
				</xsl:variable>
				<xsl:variable name="VAR9">
					<xsl:call-template name="MATURE_CREDIT" />
				</xsl:variable>
				<xsl:variable name="VAR10">
					<xsl:call-template name="ACCIDENT_VIOLATION_FACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR11">
					<xsl:call-template name="NO_CYCLE_ENDORSEMENT_SURCHARGE" />
				</xsl:variable>
				<xsl:variable name="VAR12">
					<xsl:call-template name="MULTIVEHICLE" />
				</xsl:variable>
				<xsl:variable name="VAR13">
					<xsl:call-template name="INSURANCESCORE" />
				</xsl:variable>
				<xsl:variable name="VAR14">
					<xsl:call-template name="MULTIPOLICY" />
				</xsl:variable>
				<xsl:variable name="VAR15">
					<xsl:call-template name="POLICYTERM" />
				</xsl:variable>
				<!--xsl:value-of select="round($VAR1*$VAR2*$VAR3*$VAR4*$VAR5*$VAR6*$VAR7*$VAR8*$VAR9*$VAR10*$VAR11*$VAR12*$VAR13*$VAR14*$VAR15)" /-->
				<xsl:variable name="FIRST_STEP" select="($VAR1*$VAR2)" />
				<xsl:variable name="VARFIS1" select="round(format-number($FIRST_STEP,'##.0000'))" />
				<xsl:variable name="SECOND_STEP" select="$VARFIS1 * $VAR3" />
				<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" />
				<xsl:variable name="THIRD_STEP" select="$VARSEC2 * $VAR4" />
				<xsl:variable name="VARTHI3" select="round(format-number($THIRD_STEP,'##.0000'))" />
				<xsl:variable name="FORTH_STEP" select="$VARTHI3 * $VAR5" />
				<xsl:variable name="VARFOR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
				<xsl:variable name="FIFTH_STEP" select="$VARFOR4 * $VAR6" />
				<xsl:variable name="VARFIF5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
				<xsl:variable name="SIX_STEP" select="$VARFIF5 * $VAR7" />
				<xsl:variable name="VARSIX6" select="round(format-number($SIX_STEP,'##.0000'))" />
				<xsl:variable name="SEVEN_STEP" select="$VARSIX6 * $VAR8" />
				<xsl:variable name="VARSEV7" select="round(format-number($SEVEN_STEP,'##.0000'))" />
				<xsl:variable name="EIGHT_STEP" select="$VARSEV7 * $VAR9" />
				<xsl:variable name="VAREIG8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
				<xsl:variable name="NINE_STEP" select="$VAREIG8 * $VAR10" />
				<xsl:variable name="VARNIN9" select="round(format-number($NINE_STEP,'##.0000'))" />
				<xsl:variable name="TENTH_STEP" select="$VARNIN9 * $VAR11" />
				<xsl:variable name="VARTEN10" select="round(format-number($TENTH_STEP,'##.0000'))" />
				<xsl:variable name="ELEVEN_STEP" select="$VARTEN10 * $VAR12" />
				<xsl:variable name="VARELE11" select="round(format-number($ELEVEN_STEP,'##.0000'))" />
				<xsl:variable name="TWELVE_STEP" select="$VARELE11 * $VAR13" />
				<xsl:variable name="VARTWE12" select="round(format-number($TWELVE_STEP,'##.0000'))" />
				<xsl:variable name="THERTEEN_STEP" select="$VARTWE12 * $VAR14" />
				<xsl:variable name="VARTHE13" select="round(format-number($THERTEEN_STEP,'##.0000'))" />
				<xsl:variable name="FOURTEEN_STEP" select="$VARTHE13 * $VAR15" />
				<xsl:variable name="VARFOURTEEN14" select="round(format-number($FOURTEEN_STEP,'##.0000'))" />
				<xsl:value-of select="round($VARFOURTEEN14)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Collision ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Michigan Statutory Fee ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="MCCAFEE">
		<!-- Pro rata basis using the table in the factormaster
		 Not applicable for cc 1-1-50 cc or less bikes -->
		<!-- new calculation added by praveen singh -->
		<xsl:variable name="VAR_MIN_CC" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MIN_CC_CYCLE']/NODE[@ID='MIN_CC']/ATTRIBUTES/@CC" />
		<xsl:variable name="VAR_CC1" select="VEHICLES/VEHICLE/CC" /> <!-- CC FROM XML -->
		<xsl:variable name="VAR_CC" select="translate($VAR_CC1,',','')" /> <!-- FORMATING 1,000 TO 1000 -->
		<xsl:variable name="VAR_QUOTEEFFECTIVEDATE" select="POLICY/QUOTEEFFDATE" />
		<xsl:variable name="P_YEAR" select="substring($VAR_QUOTEEFFECTIVEDATE,7,4)" />
		<xsl:variable name="P_MONTH" select="substring($VAR_QUOTEEFFECTIVEDATE,1,2)" />
		<xsl:variable name="P_DAY" select="substring($VAR_QUOTEEFFECTIVEDATE,4,5)" />
		<xsl:variable name="VAR_MCCAFEEEFFECTIVEDATENEW" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SHORT_TERM_TABLES']/NODE[@ID='STATUTORY_RECOUPMENT_FEE_PRO_RATA_NEW']/ATTRIBUTES/@RATE_EFFECTIVEDATE" />
		<xsl:variable name="P_YEAR_MCCAFEEEFFECTIVEDATENEW" select="substring($VAR_MCCAFEEEFFECTIVEDATENEW,7,4)" />
		<xsl:variable name="P_MONTH_MCCAFEEEFFECTIVEDATENEW" select="substring($VAR_MCCAFEEEFFECTIVEDATENEW,1,2)" />
		<xsl:variable name="P_DAY_MCCAFEEEFFECTIVEDATENEW" select="substring($VAR_MCCAFEEEFFECTIVEDATENEW,4,5)" />
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='MICHIGAN' and (VEHICLES/VEHICLE/COMPONLY='NO' or VEHICLES/VEHICLE/COMPONLY='')">
				<xsl:choose>
					<xsl:when test="$VAR_CC ='' or $VAR_CC &lt; $VAR_MIN_CC">
					  0.00  
				</xsl:when>
					<xsl:when test="$VAR_CC &gt; $VAR_MIN_CC">
						<xsl:variable name="PPOLICYTERMMONTHS">
							<xsl:value-of select="POLICY/POLICYTERMS" />
						</xsl:variable>
						<xsl:variable name="PRECOUPMENTFEE">
							<xsl:choose>
								<xsl:when test="$P_YEAR &lt;= $P_YEAR_MCCAFEEEFFECTIVEDATENEW and $P_MONTH &lt; $P_MONTH_MCCAFEEEFFECTIVEDATENEW ">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SHORT_TERM_TABLES']/NODE[@ID='STATUTORY_RECOUPMENT_FEE_PRO_RATA_OLD']/ATTRIBUTES/@FEE" />
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SHORT_TERM_TABLES']/NODE[@ID='STATUTORY_RECOUPMENT_FEE_PRO_RATA_NEW']/ATTRIBUTES/@FEE" />
								</xsl:otherwise>
							</xsl:choose>
						</xsl:variable>
						<xsl:variable name="MFEES">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SHORT_TERM_TABLES']/NODE[@ID='STATUTORY_RECOUPMENT_FEE_PRO_RATA_NEW']/ATTRIBUTES[@MONTHS = $PPOLICYTERMMONTHS]/@FACTOR" />
						</xsl:variable>
						<xsl:value-of select="round($PRECOUPMENTFEE * $MFEES)" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Michigan Statutory Fee ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Road Services ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="ROADSERVICES">
		<!-- This is included in case of Goldwing and Tour Bikes. No need to add. -->
		<xsl:variable name="PROADSERVICE" select="VEHICLES/VEHICLE/ROADSERVICE" />
		<xsl:choose>
			<xsl:when test="(VEHICLES/VEHICLE/VEHICLETYPE ='G' or VEHICLES/VEHICLE/VEHICLETYPE ='T') and (VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE !='' and VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE &gt; 0) and (VEHICLES/VEHICLE/COLLISIONDEDUCTIBLE !='' and VEHICLES/VEHICLE/COLLISIONDEDUCTIBLE &gt; 0)">0</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="$PROADSERVICE = '' or $PROADSERVICE = 0 or VEHICLES/VEHICLE/COMPONLY = 'YES'">
					 0
				</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ADDITIONAL_OPTIONAL_COVERAGES']/NODE[@ID='ROADSERVICELIMIT']/ATTRIBUTES[@COVERAGE = $PROADSERVICE]/@RATE" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ROADSERVICES_DISPLAY">
		<!-- This is included in case of Goldwing and Tour Bikes. No need to add.Display Included for these two types -->
		<xsl:variable name="PROADSERVICE" select="VEHICLES/VEHICLE/ROADSERVICE" />
		<xsl:choose>
			<xsl:when test="(VEHICLES/VEHICLE/VEHICLETYPE ='G' or VEHICLES/VEHICLE/VEHICLETYPE ='T') and (VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE !='' and VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE &gt; 0) and (VEHICLES/VEHICLE/COLLISIONDEDUCTIBLE !='' and VEHICLES/VEHICLE/COLLISIONDEDUCTIBLE &gt; 0)">			
			<xsl:text>Included</xsl:text>
		</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="$PROADSERVICE = '' or $PROADSERVICE = '0' or VEHICLES/VEHICLE/COMPONLY = 'YES'">
					 0
				</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ADDITIONAL_OPTIONAL_COVERAGES']/NODE[@ID='ROADSERVICELIMIT']/ATTRIBUTES[@COVERAGE = $PROADSERVICE]/@RATE" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Road Services ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Helmet and Riding Apparel (M-15) ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="HELMET_RIDING_APPAREL_DISPLAY">
		<xsl:variable name="PCOMPREHENSIVEDEDUCTIBLE" select="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE" />
		<xsl:choose>
			<xsl:when test="$PCOMPREHENSIVEDEDUCTIBLE = '' or $PCOMPREHENSIVEDEDUCTIBLE = 0">0</xsl:when>
			<xsl:otherwise>					
			<xsl:text>Included</xsl:text>
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Helmet and Riding Apparel (M-15) ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Trailer Coverage (M-49) ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ******************************************************************************************************************** -->
	<!-- New Calculation Added by praveen singh  -->
	<xsl:template name="TRAILER_COVERAGE">
		<xsl:variable name="PMOTORCYCLETRAILER" select="VEHICLES/VEHICLE/MCYCLETRAILER" />
		<xsl:variable name="PMOTORCYCLECLASS" select="VEHICLES/VEHICLE/CLASS" />
		<xsl:variable name="PCOMPDEDUCTIBLE" select="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE" />
		<xsl:variable name="PCOLLDEDUCTIBLE" select="VEHICLES/VEHICLE/COLLISIONDEDUCTIBLE" />
		<xsl:choose>
			<xsl:when test="$PMOTORCYCLETRAILER = '' or $PMOTORCYCLETRAILER = 0">0</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="$PCOMPDEDUCTIBLE !='' and $PCOMPDEDUCTIBLE !='0' and $PCOMPDEDUCTIBLE &gt; 0 and ($PCOLLDEDUCTIBLE ='' or $PCOLLDEDUCTIBLE ='0')">
						<!-- COMP only -->
						<xsl:choose>
							<xsl:when test="$PMOTORCYCLECLASS ='A'">
								<xsl:variable name="VAR1">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ADDITIONAL_OPTIONAL_COVERAGES']/NODE[@ID='MOTORCYCLE_TRAILER_ENDORSEMENT']/ATTRIBUTES[@COVERAGE1 = 'COMP' and @COVERAGE2 ='']/@CLASSA" />
								</xsl:variable>
								<xsl:variable name="VAR2">
									<xsl:value-of select="$PMOTORCYCLETRAILER" />
								</xsl:variable>
								<xsl:variable name="VAR3">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ADDITIONAL_OPTIONAL_COVERAGES']/NODE[@ID='MOTORCYCLE_TRAILER_ENDORSEMENT']/ATTRIBUTES[@COVERAGE1 = 'COMP' and @COVERAGE2 ='']/@FOR_EACH" />
								</xsl:variable>
								<xsl:value-of select=" $VAR1 * ($VAR2 div $VAR3)" />
							</xsl:when>
							<xsl:when test="$PMOTORCYCLECLASS ='B'">
								<xsl:variable name="VAR1">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ADDITIONAL_OPTIONAL_COVERAGES']/NODE[@ID='MOTORCYCLE_TRAILER_ENDORSEMENT']/ATTRIBUTES[@COVERAGE1 = 'COMP' and @COVERAGE2 ='']/@CLASSB" />
								</xsl:variable>
								<xsl:variable name="VAR2">
									<xsl:value-of select="$PMOTORCYCLETRAILER" />
								</xsl:variable>
								<xsl:variable name="VAR3">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ADDITIONAL_OPTIONAL_COVERAGES']/NODE[@ID='MOTORCYCLE_TRAILER_ENDORSEMENT']/ATTRIBUTES[@COVERAGE1 = 'COMP' and @COVERAGE2 ='']/@FOR_EACH" />
								</xsl:variable>
								<xsl:value-of select="$VAR1 * ($VAR2 div $VAR3)" />
							</xsl:when>
							<xsl:when test="$PMOTORCYCLECLASS ='C'">
								<xsl:variable name="VAR1">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ADDITIONAL_OPTIONAL_COVERAGES']/NODE[@ID='MOTORCYCLE_TRAILER_ENDORSEMENT']/ATTRIBUTES[@COVERAGE1 = 'COMP' and @COVERAGE2 ='']/@CLASSC" />
								</xsl:variable>
								<xsl:variable name="VAR2">
									<xsl:value-of select="$PMOTORCYCLETRAILER" />
								</xsl:variable>
								<xsl:variable name="VAR3">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ADDITIONAL_OPTIONAL_COVERAGES']/NODE[@ID='MOTORCYCLE_TRAILER_ENDORSEMENT']/ATTRIBUTES[@COVERAGE1 = 'COMP' and @COVERAGE2 ='']/@FOR_EACH" />
								</xsl:variable>
								<xsl:value-of select="$VAR1 * ($VAR2 div $VAR3)" />
							</xsl:when>
							<xsl:otherwise>0</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:when test="$PCOMPDEDUCTIBLE !='' and $PCOMPDEDUCTIBLE !='0' and $PCOMPDEDUCTIBLE &gt; 0 and $PCOLLDEDUCTIBLE !='' and $PCOLLDEDUCTIBLE !='0' and $PCOLLDEDUCTIBLE &gt; 0">
						<!-- COMP and COLL -->
						<xsl:choose>
							<xsl:when test="$PMOTORCYCLECLASS ='A'">
								<xsl:variable name="VAR1">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ADDITIONAL_OPTIONAL_COVERAGES']/NODE[@ID='MOTORCYCLE_TRAILER_ENDORSEMENT']/ATTRIBUTES[@COVERAGE1 = 'COMP' and @COVERAGE2 ='COLL']/@CLASSA" />
								</xsl:variable>
								<xsl:variable name="VAR2">
									<xsl:value-of select="$PMOTORCYCLETRAILER" />
								</xsl:variable>
								<xsl:variable name="VAR3">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ADDITIONAL_OPTIONAL_COVERAGES']/NODE[@ID='MOTORCYCLE_TRAILER_ENDORSEMENT']/ATTRIBUTES[@COVERAGE1 = 'COMP' and @COVERAGE2 ='']/@FOR_EACH" />
								</xsl:variable>
								<!--xsl:value-of select="round($VAR1 * round($VAR2 div $VAR3))"/-->
								<xsl:value-of select="$VAR1 * ($VAR2 div $VAR3)" />
							</xsl:when>
							<xsl:when test="$PMOTORCYCLECLASS ='B'">
								<xsl:variable name="VAR1">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ADDITIONAL_OPTIONAL_COVERAGES']/NODE[@ID='MOTORCYCLE_TRAILER_ENDORSEMENT']/ATTRIBUTES[@COVERAGE1 = 'COMP' and @COVERAGE2 ='COLL']/@CLASSB" />
								</xsl:variable>
								<xsl:variable name="VAR2">
									<xsl:value-of select="$PMOTORCYCLETRAILER" />
								</xsl:variable>
								<xsl:variable name="VAR3">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ADDITIONAL_OPTIONAL_COVERAGES']/NODE[@ID='MOTORCYCLE_TRAILER_ENDORSEMENT']/ATTRIBUTES[@COVERAGE1 = 'COMP' and @COVERAGE2 ='']/@FOR_EACH" />
								</xsl:variable>
								<!--xsl:value-of select="round($VAR1 * round($VAR2 div $VAR3))"/-->
								<xsl:value-of select="$VAR1 * ($VAR2 div $VAR3)" />
							</xsl:when>
							<xsl:when test="$PMOTORCYCLECLASS ='C'">
								<xsl:variable name="VAR1">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ADDITIONAL_OPTIONAL_COVERAGES']/NODE[@ID='MOTORCYCLE_TRAILER_ENDORSEMENT']/ATTRIBUTES[@COVERAGE1 = 'COMP' and @COVERAGE2 ='COLL']/@CLASSC" />
								</xsl:variable>
								<xsl:variable name="VAR2">
									<xsl:value-of select="$PMOTORCYCLETRAILER" />
								</xsl:variable>
								<xsl:variable name="VAR3">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ADDITIONAL_OPTIONAL_COVERAGES']/NODE[@ID='MOTORCYCLE_TRAILER_ENDORSEMENT']/ATTRIBUTES[@COVERAGE1 = 'COMP' and @COVERAGE2 ='']/@FOR_EACH" />
								</xsl:variable>
								<!--xsl:value-of select="round($VAR1 * round($VAR2 div $VAR3))"/-->
								<xsl:value-of select="$VAR1 * ($VAR2 div $VAR3)" />
							</xsl:when>
							<xsl:otherwise>0</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="TRAILER_COVERAGE_DISPLAY">
		<xsl:variable name="PMOTORCYCLETRAILER" select="VEHICLES/VEHICLE/MCYCLETRAILER" />
		<xsl:variable name="PMOTORCYCLECLASS" select="VEHICLES/VEHICLE/CLASS" />
		<xsl:variable name="PCOMPDEDUCTIBLE" select="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE" />
		<xsl:variable name="PCOLLDEDUCTIBLE" select="VEHICLES/VEHICLE/COLLISIONDEDUCTIBLE" />
		<xsl:variable name="VAR1">
			<xsl:choose>
				<xsl:when test="$PMOTORCYCLETRAILER = '' or $PMOTORCYCLETRAILER = 0">0</xsl:when>
				<xsl:otherwise>
					<xsl:call-template name="TRAILER_COVERAGE" /> <!-- Included  -->
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="POLICYTERM" />
		</xsl:variable>
		<xsl:value-of select="round(round($VAR1)*$VAR2)" />
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Trailer Coverage (M-49) ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Additional Physical Damage  ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="ADDITIONAL_PHYSICAL_DAMAGE">
		<xsl:variable name="PCOMPDEDUCTIBLE" select="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE" />
		<xsl:variable name="PCOLLDEDUCTIBLE" select="VEHICLES/VEHICLE/COLLISIONDEDUCTIBLE" />
		<xsl:variable name="VAR1">
			<xsl:choose>
				<xsl:when test="VEHICLES/VEHICLE/ADDLPD != '' and VEHICLES/VEHICLE/ADDLPD &gt; 0">
					<xsl:choose>
						<xsl:when test="$PCOMPDEDUCTIBLE !='' and $PCOMPDEDUCTIBLE !='0' and $PCOMPDEDUCTIBLE &gt; 0 and ($PCOLLDEDUCTIBLE ='' or $PCOLLDEDUCTIBLE ='0')">
							<!-- COMP only -->
							<xsl:variable name="VAR1">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ADDITIONAL_OPTIONAL_COVERAGES']/NODE[@ID='ADDITIONAL_PHYSICAL_DAMAGE']/ATTRIBUTES[@COVERAGE1 = 'COMP' and @COVERAGE2 ='']/@RATE" />
							</xsl:variable>
							<xsl:variable name="VAR2">
								<xsl:value-of select="VEHICLES/VEHICLE/ADDLPD" />
							</xsl:variable>
							<xsl:variable name="VAR3">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ADDITIONAL_OPTIONAL_COVERAGES']/NODE[@ID='ADDITIONAL_PHYSICAL_DAMAGE']/ATTRIBUTES[@COVERAGE1 = 'COMP' and @COVERAGE2 ='']/@FOR_EACH" />
							</xsl:variable>
							<xsl:value-of select="round($VAR1 * ($VAR2 div $VAR3))" />
						</xsl:when>
						<xsl:when test="$PCOMPDEDUCTIBLE !='' and $PCOMPDEDUCTIBLE !='0' and $PCOMPDEDUCTIBLE &gt; 0 and $PCOLLDEDUCTIBLE !='' and $PCOLLDEDUCTIBLE !='0' and $PCOLLDEDUCTIBLE &gt; 0">
							<!-- COMP and COLL -->
							<xsl:variable name="VAR1">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ADDITIONAL_OPTIONAL_COVERAGES']/NODE[@ID='ADDITIONAL_PHYSICAL_DAMAGE']/ATTRIBUTES[@COVERAGE1 = 'COMP' and @COVERAGE2 ='COLL']/@RATE" />
							</xsl:variable>
							<xsl:variable name="VAR2">
								<xsl:value-of select="VEHICLES/VEHICLE/ADDLPD" />
							</xsl:variable>
							<xsl:variable name="VAR3">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ADDITIONAL_OPTIONAL_COVERAGES']/NODE[@ID='ADDITIONAL_PHYSICAL_DAMAGE']/ATTRIBUTES[@COVERAGE1 = 'COMP' and @COVERAGE2 ='COLL']/@FOR_EACH" />
							</xsl:variable>
							<xsl:value-of select="round($VAR1 * ($VAR2 div $VAR3))" />
						</xsl:when>
						<xsl:otherwise>1.00</xsl:otherwise>
					</xsl:choose>
				</xsl:when>
				<xsl:otherwise>0.00</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="POLICYTERM" />
		</xsl:variable>
		<xsl:value-of select="round($VAR1*$VAR2)" />
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of - Additional Physical Damage ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Discount - Multi Cycle ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="MULTIVEHICLE">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/MULTICYCLE ='Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTICYCLE']/NODE[@ID='MULTICYCLEDISCOUNT']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MULTIVEHICLE_DISPLAY">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/MULTICYCLE='Y'">
			<xsl:text>Included</xsl:text>
		</xsl:when>
			<xsl:otherwise>
			0
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MULTIVEHICLE_CREDIT">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/MULTICYCLE ='Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTICYCLE']/NODE[@ID='MULTICYCLEDISCOUNT']/ATTRIBUTES/@CREDIT" />
				<xsl:text>%</xsl:text>
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of - Multi Cycle ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Discount - Tranfer/Renewal  ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- This credit is  applicable only if the insured had coverage in force in 
      the preceeding 12 months with Wolverine Mutual or any other Company -->
	<xsl:template name="TRANSFER_RENEWAL">
		<xsl:variable name="PYEARSCONTINSURED">
			<xsl:value-of select="POLICY/YEARSCONTINSURED" />
		</xsl:variable>
		<xsl:variable name="PYEARSCONTINSUREDWITHWOLVERINE">
			<xsl:value-of select="POLICY/YEARSCONTINSUREDWITHWOLVERINE" />
		</xsl:variable>
		<xsl:variable name="PTRANSFERRENEWAL">
			<xsl:value-of select="VEHICLES/VEHICLE/TRANSFERRENEWAL" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PTRANSFERRENEWAL='TRUE'">
				<xsl:choose>
					<xsl:when test="POLICY/STATENAME='INDIANA'">
						<xsl:choose>
							<xsl:when test="($PYEARSCONTINSURED &gt;= 1 or $PYEARSCONTINSUREDWITHWOLVERINE &gt;= 1) and (VEHICLES/VEHICLE/DRIVEREXPERINCE = 'TRUE')">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TRANSFER_RENEWAL']/NODE[@ID='TRANSFER_RENEWAL_DISCOUNT']/ATTRIBUTES/@FACTOR" />
							</xsl:when>
							<xsl:otherwise>1.00</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>
						<xsl:choose>
							<xsl:when test="($PYEARSCONTINSURED &gt;= 1 or $PYEARSCONTINSUREDWITHWOLVERINE &gt;= 1)">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TRANSFER_RENEWAL']/NODE[@ID='TRANSFER_RENEWAL_DISCOUNT']/ATTRIBUTES/@FACTOR" />
							</xsl:when>
							<xsl:otherwise>1.00</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="TRANSFER_RENEWAL_DISPLAY">
		<xsl:variable name="PTRANSFERRENEWAL">
			<xsl:value-of select="VEHICLES/VEHICLE/TRANSFERRENEWAL" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PTRANSFERRENEWAL='TRUE'">
				<xsl:choose>
					<xsl:when test="POLICY/STATENAME='INDIANA'">
						<xsl:choose>
							<xsl:when test="((POLICY/YEARSCONTINSURED &gt;= 1 or POLICY/YEARSCONTINSUREDWITHWOLVERINE &gt;=1)  and (VEHICLES/VEHICLE/DRIVEREXPERINCE = 'TRUE'))">
						<xsl:text>Included</xsl:text>
					</xsl:when>
							<xsl:otherwise>
					      0
					</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>
						<xsl:choose>
							<xsl:when test="((POLICY/YEARSCONTINSURED &gt;= 1 or POLICY/YEARSCONTINSUREDWITHWOLVERINE &gt;=1))">
						<xsl:text>Included</xsl:text>
					</xsl:when>
							<xsl:otherwise>
						0
					</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="TRANSFER_RENEWAL_CREDIT">
		<xsl:variable name="PYEARSCONTINSURED">
			<xsl:value-of select="POLICY/YEARSCONTINSURED" />
		</xsl:variable>
		<xsl:variable name="PYEARSCONTINSUREDWITHWOLVERINE">
			<xsl:value-of select="POLICY/YEARSCONTINSUREDWITHWOLVERINE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PYEARSCONTINSURED &gt;= 1 or $PYEARSCONTINSUREDWITHWOLVERINE &gt;= 1">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TRANSFER_RENEWAL']/NODE[@ID='TRANSFER_RENEWAL_DISCOUNT']/ATTRIBUTES/@CREDIT" />
				<xsl:text>%</xsl:text>
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Tranfer/Renewal ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Multi-policy  ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="MULTIPOLICY">
		<xsl:choose>
			<xsl:when test="POLICY/MULTIPOLICYAUTOHOMEDISCOUNT = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIPOLICY']/NODE[@ID ='MULTIPOLICYDISCOUNT']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MULTIPOLICY_DISPLAY">
		<xsl:choose>
			<xsl:when test="POLICY/MULTIPOLICYAUTOHOMEDISCOUNT = 'Y'"><xsl:text>Included</xsl:text></xsl:when>
			<xsl:otherwise>
			0
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MULTIPOLICY_CREDIT">
		<xsl:choose>
			<xsl:when test="POLICY/MULTIPOLICYAUTOHOMEDISCOUNT = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIPOLICY']/NODE[@ID ='MULTIPOLICYDISCOUNT']/ATTRIBUTES/@CREDIT" />
				<xsl:text>%</xsl:text>
			</xsl:when>
			<xsl:otherwise>
		  1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Multi policy  ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Preferred Risk  ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="PREFERRED_RISK">
		<!-- A preferred risk credit will be given when
		1. the operator has been licensed for atleast three yrs to drive an auto or motorcycle
		2. had no surchargeeable accidents or violations exceeding 2 points. 
		3. age of the driver should be greater than 18 -->
		<xsl:variable name="PLICUNDER3YRS">
			<xsl:value-of select="VEHICLES/VEHICLE/ASSIGNEDDRIVERLICENCE3YEAR" />
		</xsl:variable>
		<xsl:variable name="PALLDRIVERSOVER18">
			<xsl:value-of select="VEHICLES/VEHICLE/ALLDRIVERSOVER18" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PLICUNDER3YRS ='TRUE' and $PALLDRIVERSOVER18 ='TRUE'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PREFERREDRISK']/NODE[@ID ='PREFERREDRISKDISCOUNT']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>1.00 </xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PREFERRED_RISK_CREDIT">
		<!-- A preferred risk credit will be given when
			 1. the operator has been licensed for three yr to drive an auto or motorcycle
			 2. had no surchargeeable accidents or violations exceeding 2 points. 
		-->
		<xsl:variable name="PLICUNDER3YRS">
			<xsl:value-of select="VEHICLES/VEHICLE/ASSIGNEDDRIVERLICENCE3YEAR" />
		</xsl:variable>
		<xsl:variable name="PALLDRIVERSOVER18">
			<xsl:value-of select="VEHICLES/VEHICLE/ALLDRIVERSOVER18" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PLICUNDER3YRS ='TRUE' and  $PALLDRIVERSOVER18 ='TRUE'">-<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PREFERREDRISK']/NODE[@ID ='PREFERREDRISKDISCOUNT']/ATTRIBUTES/@CREDIT" /><xsl:text>%</xsl:text></xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PREFERRED_RISK_DISPLAY">
		<!-- A preferred risk credit will be given when
			 1. the operator has been licensed for three yr to drive an auto or motorcycle
			 2. had no surchargeeable accidents or violations exceeding 2 points. 
			 3. Has all drivers over 18 yrs.
		-->
		<xsl:variable name="PLICUNDER3YRS">
			<xsl:value-of select="VEHICLES/VEHICLE/ASSIGNEDDRIVERLICENCE3YEAR" />
		</xsl:variable>
		<xsl:variable name="PALLDRIVERSOVER18">
			<xsl:value-of select="VEHICLES/VEHICLE/ALLDRIVERSOVER18" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="normalize-space($PLICUNDER3YRS) ='TRUE' and  normalize-space($PALLDRIVERSOVER18) ='TRUE'">	
				<xsl:text>Included</xsl:text>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Preferred Risk  ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Mature Credit  ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="MATURE_CREDIT">
		<xsl:variable name="VAR_AGE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MATUREOPERATOR']/NODE[@ID='MATUREOPERATOR_CREDIT']/ATTRIBUTES/@AGE" />
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/MATUREOPERATORCREDIT='Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MATUREOPERATOR']/NODE[@ID='MATUREOPERATOR_CREDIT']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MATURE_CREDIT_DISPLAY">
		<xsl:variable name="VAR_AGE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MATUREOPERATOR']/NODE[@ID='MATUREOPERATOR_CREDIT']/ATTRIBUTES/@AGE" />
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/MATUREOPERATORCREDIT='Y'">
			<xsl:text>Included</xsl:text>
		</xsl:when>
			<xsl:otherwise>
			0
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MATURE_CREDIT_PERCENT">
		<xsl:variable name="VAR_AGE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MATUREOPERATOR']/NODE[@ID='MATUREOPERATOR_CREDIT']/ATTRIBUTES/@AGE" />
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/MATUREOPERATORCREDIT='Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MATUREOPERATOR']/NODE[@ID='MATUREOPERATOR_CREDIT']/ATTRIBUTES/@CREDIT" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Mature Credit ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Insurance Score  ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="INSURANCESCORE">
		<xsl:variable name="INS1" select="POLICY/INSURANCESCORE"></xsl:variable>
		<xsl:variable name="INS" select="translate(translate($INS1,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')" />
		<!-- FOR MAXIMUM SCORE -->
		<xsl:variable name="VAR_MAX_SCORE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MAX = 'MAX_SCORE']/@MINSCORE" />
		<xsl:choose>
			<xsl:when test="$INS = 0 or $INS = ''">
			1.00
		</xsl:when>
			<xsl:when test="$INS = 'NOHITNOSCORE'"> <!-- NO HIT NO SCORE -->
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE = 'N']/@FACTOR" />
			</xsl:when>
			<xsl:when test="$INS &gt;= $VAR_MAX_SCORE">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE = $VAR_MAX_SCORE]/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@FACTOR" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="INSURANCESCORE_DISPLAY">
		<xsl:variable name="INS1" select="POLICY/INSURANCESCORE"></xsl:variable>
		<!--<xsl:variable name="INS"  -->
		<xsl:variable name="INS">
			<xsl:value-of select="translate(translate($INS1,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')" />
		</xsl:variable>
		<!-- FOR MAXIMUM SCORE -->
		<xsl:variable name="VAR_MAX_SCORE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MAX = 'MAX_SCORE']/@MINSCORE" />
		<xsl:variable name="INSDISCOUNT">
			<xsl:choose>
				<xsl:when test="$INS = 0">
					<xsl:value-of select="$INS"></xsl:value-of>
				</xsl:when>
				<xsl:when test="$INS = ''">
				1.00
			</xsl:when>
				<xsl:when test="$INS = 'NOHITNOSCORE'"> <!-- NO HIT NO SCORE -->
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE = 'N']/@FACTOR" />
				</xsl:when>
				<!--
			<xsl:when test = "$INS &gt;= 751">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE = 751]/@FACTOR"/>
			</xsl:when>		
			-->
				<xsl:when test="$INS &gt;= $VAR_MAX_SCORE">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE = $VAR_MAX_SCORE]/@FACTOR" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@FACTOR" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$INSDISCOUNT = 0 or $INSDISCOUNT='' or $INSDISCOUNT=1">
			0
		</xsl:when>
			<xsl:when test="$INSDISCOUNT &gt; 0 and $INSDISCOUNT &lt; 1">
			<xsl:text>Included</xsl:text>
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
					<xsl:value-of select="round((1 - $VAR_SCORE) * 100)" />
					<xsl:text>%</xsl:text>
				</xsl:when>
				<xsl:when test="$VAR_SCORE != '' and $VAR_SCORE &gt; 1.00">
					<xsl:value-of select="round(($VAR_SCORE -1 ) * 100)" />
					<xsl:text>%</xsl:text>
				</xsl:when>
			</xsl:choose>
		</xsl:variable>
		<xsl:value-of select="$VAR_SCORE_PERCENT" />
	</xsl:template>
	<xsl:template name="INSURENCESCOREDISPLAY">
		<xsl:choose>
			<xsl:when test="normalize-space(POLICY/INSURANCESCORE)='NOHITNOSCORE'">No Hit No Score</xsl:when>
			<xsl:when test="normalize-space(POLICY/INSURANCESCORE)&gt;=0">
				<xsl:value-of select="POLICY/INSURANCESCORE" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Insurance Score ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of No Cycle Endorsement on License ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="NO_CYCLE_ENDORSEMENT_SURCHARGE_CREDIT">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/NOCYCLENDMT = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='NO_CYCLE_ENDORSEMENT']/NODE[@ID ='NO_CYCLE_ENDORSEMENT_SURCHARGE']/ATTRIBUTES/@SURCHARGE" />
				<xsl:text>%</xsl:text>
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="NO_CYCLE_ENDORSEMENT_SURCHARGE">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/NOCYCLENDMT = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='NO_CYCLE_ENDORSEMENT']/NODE[@ID ='NO_CYCLE_ENDORSEMENT_SURCHARGE']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="NO_CYCLE_ENDORSEMENT_DISPLAY">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/NOCYCLENDMT = 'Y'">
			<xsl:text>Included</xsl:text>
		</xsl:when>
			<xsl:otherwise>
			0
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of No Cycle Endorsement on License ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Type of Bike ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="TYPE_OF_BIKE">
		<xsl:variable name="PBIKETYPE" select="normalize-space(VEHICLES/VEHICLE/VEHICLETYPE)" />
		<!--<xsl:variable name="PVEHICLECLASS" select="VEHICLES/VEHICLE/CLASS"/>-->
		<xsl:variable name="PVEHICLECLASS" select="normalize-space(VEHICLES/VEHICLE/CLASS)" />
		<xsl:choose>
			<xsl:when test="$PBIKETYPE = '' or $PVEHICLECLASS = '' or $PBIKETYPE = 'X'">
				<xsl:text>1.00</xsl:text>
			</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="normalize-space($PVEHICLECLASS) ='A'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPE_OF_BIKE']/NODE[@ID='BIKETYPE']/ATTRIBUTES[@TYPE = $PBIKETYPE]/@CLASSA" />
					</xsl:when>
					<xsl:when test="normalize-space($PVEHICLECLASS) ='B'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPE_OF_BIKE']/NODE[@ID='BIKETYPE']/ATTRIBUTES[@TYPE = $PBIKETYPE]/@CLASSB" />
					</xsl:when>
					<xsl:when test="normalize-space($PVEHICLECLASS) ='C'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPE_OF_BIKE']/NODE[@ID='BIKETYPE']/ATTRIBUTES[@TYPE = $PBIKETYPE]/@CLASSC" />
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="BIKE_TYPE_DISPLAY">
		<xsl:variable name="PBIKETYPE" select="VEHICLES/VEHICLE/VEHICLETYPE" />
		<!--Get the factor -->
		<xsl:variable name="PFACTOR">
			<xsl:call-template name="TYPE_OF_BIKE" />
		</xsl:variable>
		<!--Get the description -->
		<xsl:variable name="PDESCRIPTION">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPE_OF_BIKE']/NODE[@ID='BIKETYPE']/ATTRIBUTES[@TYPE = $PBIKETYPE]/@DESC" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PFACTOR != '' and $PFACTOR != 1.00"><xsl:text>Included</xsl:text></xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="BIKE_TYPE_COMPONENT_TYPE">
		<!--Get the factor -->
		<xsl:variable name="PFACTOR">
			<xsl:call-template name="TYPE_OF_BIKE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PFACTOR &gt; 1">S</xsl:when>
			<xsl:when test="$PFACTOR &lt;= 1">D</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="BIKE_TYPE_TEXT">
		<xsl:variable name="PBIKETYPE" select="VEHICLES/VEHICLE/VEHICLETYPE" /> <!--Get the factor -->
		<xsl:variable name="PFACTOR">
			<xsl:call-template name="TYPE_OF_BIKE" />
		</xsl:variable>
		<!--Get the description -->
		<xsl:variable name="PDESCRIPTION">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPE_OF_BIKE']/NODE[@ID='BIKETYPE']/ATTRIBUTES[@TYPE = $PBIKETYPE]/@DESC" />
		</xsl:variable>
		<!-- converting   factor into percentage -->
		<xsl:variable name="VAR_PERCENT">
			<xsl:choose>
				<xsl:when test="$PFACTOR != '' and $PFACTOR &lt; 1.00">
					<xsl:value-of select="round((1 - $PFACTOR) * 100)" />
					<xsl:text>%</xsl:text>
				</xsl:when>
				<xsl:when test="$PFACTOR != '' and $PFACTOR &gt; 1.00">
					<xsl:value-of select="round(($PFACTOR -1 ) * 100)" />
					<xsl:text>%</xsl:text>
				</xsl:when>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PFACTOR != '' and $PFACTOR &lt; 1.00">
				<xsl:text>Discount : </xsl:text>
				<xsl:value-of select="$PDESCRIPTION" />
				<xsl:text> -</xsl:text>
				<xsl:value-of select="$VAR_PERCENT" />
			</xsl:when>
			<xsl:when test="$PFACTOR != '' and $PFACTOR &gt; 1.00">
				<xsl:text>Surcharge : </xsl:text>
				<xsl:value-of select="$PDESCRIPTION" />
				<xsl:text> -</xsl:text>
				<xsl:value-of select="$VAR_PERCENT" />
			</xsl:when>
			<xsl:when test="$PFACTOR != '' and $PFACTOR = 1.00">
		    0
	</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ACCIDENT_VIOLATION_SURCHRAGE_PERCENTAGE">
		<!-- Accident Surcharge  ITRACK # 5081 -->
		<!-- Run a loop for each Assign Driver Accident Points. -->
		<!--xsl:variable name="VAR_SUMOFACCIDENTPOINT" select="user:GetPvioAcc(-1)" />
		<xsl:variable name="ACCIDENTSURCHARGE">
			<xsl:for-each select="VEHICLES/VEHICLE/ASSIGNDRIVIOACCPNT[@ID &lt; 9999999]">
				<xsl:variable name="ACCIDENTPOINTS">
					<xsl:choose><xsl:when test="SUMOFACCIDENTPOINTS &gt;=0 and SUMOFACCIDENTPOINTS !=''"><xsl:value-of select="SUMOFACCIDENTPOINTS" /></xsl:when><xsl:otherwise>0</xsl:otherwise></xsl:choose>
				</xsl:variable>
				<xsl:variable name="VIOLATIONPOINTS">
						<xsl:choose><xsl:when test="SUMOFVIOLATIONPOINTS &gt;=0 and SUMOFVIOLATIONPOINTS !=''"><xsl:value-of select="SUMOFVIOLATIONPOINTS" /></xsl:when><xsl:otherwise>0</xsl:otherwise></xsl:choose>
				</xsl:variable>
				<xsl:variable name="MISCPOINTS">
					<xsl:choose><xsl:when test="SUMOFMISCPOINTS &gt;=0 and SUMOFMISCPOINTS !=''"><xsl:value-of select="SUMOFMISCPOINTS" /></xsl:when><xsl:otherwise>0</xsl:otherwise></xsl:choose>
				</xsl:variable>
				<xsl:variable name="TOTALPOINTS">
					<xsl:value-of select="$VIOLATIONPOINTS + $ACCIDENTPOINTS + $MISCPOINTS" />
				</xsl:variable>
				<xsl:variable name="VAR_ACCIDENT_PERCENTAGE">
					<xsl:choose>
						<xsl:when test="$TOTALPOINTS !='' and $TOTALPOINTS !='0' and $TOTALPOINTS &gt; 0">
							<xsl:choose>
								<xsl:when test="$TOTALPOINTS &gt; 13">
									<xsl:variable name="VAR1">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS=13]/@SURCHARGEPERCENT" />
									</xsl:variable>
									<xsl:variable name="VAR2">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/@FOR_EACH_ADDITIONAL" />
									</xsl:variable>
									<xsl:value-of select="$VAR1 + round(($TOTALPOINTS - 13) * $VAR2)" />
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS=$TOTALPOINTS]/@SURCHARGEPERCENT" />
								</xsl:otherwise>
							</xsl:choose>
						</xsl:when>
						<xsl:otherwise>0.00</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:if test="$VAR_ACCIDENT_PERCENTAGE !=0">
					<xsl:value-of select="user:GetPvioAcc($VAR_ACCIDENT_PERCENTAGE)" />
				</xsl:if>
			</xsl:for-each>
		</xsl:variable-->
		<!-- Violation Surcharge -->
		<!-- run a loop for each assign driver violation point
		<xsl:variable name="VIOLATIONSURCHARGE">
			<xsl:for-each select="VEHICLES/VEHICLE/ASSIGNDRIVIOACCPNT[@ID &lt; 9999999]">
				<xsl:variable name="VIOLATIONPOINTS">
					<xsl:value-of select="SUMOFVIOLATIONPOINTS" />
				</xsl:variable>
				<xsl:variable name="VAR_VIOLATION_PERCENTAGE">
					<xsl:choose>
						<xsl:when test="$VIOLATIONPOINTS !='' and $VIOLATIONPOINTS !='0' and $VIOLATIONPOINTS &gt; 0">
							<xsl:choose>
								<xsl:when test="$VIOLATIONPOINTS &gt; 13">
									<xsl:variable name="VAR1">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEVIOLATIONPOINTS']/ATTRIBUTES[@POINTS=13]/@SURCHARGE" />
									</xsl:variable>
									<xsl:variable name="VAR2">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEVIOLATIONPOINTS']/@FOR_EACH_ADDITIONAL" />
									</xsl:variable>
									<xsl:value-of select="$VAR1 + round(($VIOLATIONPOINTS - 13) * $VAR2)" />
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEVIOLATIONPOINTS']/ATTRIBUTES[@POINTS=$VIOLATIONPOINTS]/@SURCHARGE" />
								</xsl:otherwise>
							</xsl:choose>
						</xsl:when>
						<xsl:otherwise>0.00</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:if test="$VAR_VIOLATION_PERCENTAGE !=0">
					<xsl:value-of select="user:GetPvioAcc($VAR_VIOLATION_PERCENTAGE)" />
				</xsl:if>
			</xsl:for-each>
		</xsl:variable>-->
		<!--xsl:variable name="var1" select="user:GetPvioAcc(0)" />
		<xsl:value-of select="$var1" /-->
		<xsl:variable name="ACCIDENTPOINTS">
			<xsl:choose>
				<xsl:when test="VEHICLES/VEHICLE/SUMOFACCIDENTPOINTS &gt;=0 and VEHICLES/VEHICLE/SUMOFACCIDENTPOINTS !=''">
					<xsl:value-of select="VEHICLES/VEHICLE/SUMOFACCIDENTPOINTS" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VIOLATIONPOINTS">
			<xsl:choose>
				<xsl:when test="VEHICLES/VEHICLE/SUMOFVIOLATIONPOINTS &gt;=0 and VEHICLES/VEHICLE/SUMOFVIOLATIONPOINTS !=''">
					<xsl:value-of select="VEHICLES/VEHICLE/SUMOFVIOLATIONPOINTS" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="MISCPOINTS">
			<xsl:choose>
				<xsl:when test="VEHICLES/VEHICLE/SUMOFMISCPOINTS &gt;=0 and VEHICLES/VEHICLE/SUMOFMISCPOINTS !=''">
					<xsl:value-of select="VEHICLES/VEHICLE/SUMOFMISCPOINTS" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="TOTALPOINTS">
			<xsl:value-of select="$VIOLATIONPOINTS + $ACCIDENTPOINTS + $MISCPOINTS" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$TOTALPOINTS !='' and $TOTALPOINTS !='0' and $TOTALPOINTS &gt; 0">
				<xsl:choose>
					<xsl:when test="$TOTALPOINTS &gt; 13">
						<xsl:variable name="VAR1">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS=13]/@SURCHARGEPERCENT" />
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/@FOR_EACH_ADDITIONAL" />
						</xsl:variable>
						<xsl:value-of select="$VAR1 + round(($TOTALPOINTS - 13) * $VAR2)" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS=$TOTALPOINTS]/@SURCHARGEPERCENT" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ACCIDENT_VIOLATION_SURCHARGE_DISPLAY">
		<xsl:variable name="PACCVIOSURVAL">
			<xsl:call-template name="ACCIDENT_VIOLATION_SURCHRAGE_PERCENTAGE"></xsl:call-template>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PACCVIOSURVAL &gt; 0"><xsl:text>Included</xsl:text></xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Type of Bike ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Territory Base ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="TERRITORYBASE">
		<xsl:param name="FACTORELEMENT" /> <!-- Depending on the factor element BI/PD..etc -->
		<xsl:choose>
			<!-- For Michigan Automobile -->
			<xsl:when test="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name="TERRITORYBASE_MICHIGAN">
					<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
				</xsl:call-template>
			</xsl:when>
			<!-- For Indiana Automobile -->
			<xsl:when test="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name="TERRITORYBASE_INDIANA">
					<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
				</xsl:call-template>
			</xsl:when>
			<xsl:otherwise>1</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Territory base michigan -->
	<xsl:template name="TERRITORYBASE_MICHIGAN">
		<xsl:param name="FACTOR" /> <!-- Depending on the factor element BI/PD..etc -->
		<!--<xsl:variable name="PTERRITORYCODE" select="POLICY/TERRITORY"/>-->
		<!--xsl:variable name="PTERRITORYCODE" select="QUICKQUOTE/POLICY/TERRITORY"/-->
		<xsl:variable name="PTERRITORYCODE" select="VEHICLES/VEHICLE/TERRCODEGARAGEDLOCATION" />
		<xsl:choose>
			<xsl:when test="$PTERRITORYCODE =''">
			1.00
		</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="$FACTOR = 'BI'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE=$PTERRITORYCODE]/@BIBASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'PD'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE= $PTERRITORYCODE]/@PDBASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'MEDPAY'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE= $PTERRITORYCODE]/@MEDPAYBASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'COMP'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPREHENSIVETERRITORYBASE']/NODE[@ID ='COMPREHENSIVETERRITORY']/ATTRIBUTES[@TERRITORY = $PTERRITORYCODE]/@BASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'COLL'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLISIONTERRITORYBASE']/NODE[@ID ='COLLISIONTERRITORY']/ATTRIBUTES[@TERRITORY = $PTERRITORYCODE]/@BASE" />
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="TERRITORYBASE_INDIANA">
		<xsl:param name="FACTOR" /> <!-- Depending on the factor element BI/PD..etc -->
		<!--xsl:variable name="PTERRITORYCODE" select="POLICY/TERRITORY"/-->
		<xsl:variable name="PTERRITORYCODE" select="VEHICLES/VEHICLE/TERRCODEGARAGEDLOCATION" />
		<xsl:choose>
			<xsl:when test="$PTERRITORYCODE =''">
			1.00
		</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="$FACTOR = 'BI'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE=$PTERRITORYCODE]/@BIBASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'PD'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE=$PTERRITORYCODE]/@PDBASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'MEDPAY'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE=$PTERRITORYCODE]/@MEDPAYBASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'COMP'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPREHENSIVETERRITORYBASE']/NODE[@ID ='COMPREHENSIVETERRITORY']/ATTRIBUTES[@TERRITORY =$PTERRITORYCODE]/@BASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'COLL'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLISIONTERRITORYBASE']/NODE[@ID ='COLLISIONTERRITORY']/ATTRIBUTES[@TERRITORY =$PTERRITORYCODE]/@BASE" />
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Territory base michigan -->
	<xsl:template name="TERRITORYBASE_MICHIGAN_1">
		<xsl:param name="FACTOR" /> <!-- Depending on the factor element BI/PD..etc -->
		<!--xsl:variable name="PTERRITORYCODE" select="POLICY/TERRITORY"/-->
		<xsl:variable name="PTERRITORYCODE" select="VEHICLES/VEHICLE/TERRCODEGARAGEDLOCATION" />
		<xsl:choose>
			<xsl:when test="$PTERRITORYCODE =''">
			1.00
		</xsl:when>
			<xsl:otherwise>
			IF ('<xsl:value-of select="$FACTOR" />' = 'BI')THEN 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE=$PTERRITORYCODE]/@BIBASE" /> 
			ELSE IF ('<xsl:value-of select="$FACTOR" />' = 'PD')THEN 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE= $PTERRITORYCODE]/@PDBASE" /> 
			ELSE IF ('<xsl:value-of select="$FACTOR" />' = 'MEDPAY')THEN 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE= $PTERRITORYCODE]/@MEDPAYBASE" /> 
			ELSE IF ('<xsl:value-of select="$FACTOR" />' = 'COMP') THEN 			 
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPREHENSIVETERRITORYBASE']/NODE[@ID ='COMPREHENSIVETERRITORY']/ATTRIBUTES[@TERRITORY = $PTERRITORYCODE]/@BASE" /> 
			ELSE IF ('<xsl:value-of select="$FACTOR" />' = 'COLL') THEN  
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLISIONTERRITORYBASE']/NODE[@ID ='COLLISIONTERRITORY']/ATTRIBUTES[@TERRITORY = $PTERRITORYCODE]/@BASE" /> 
			ELSE
				1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="TERRITORYBASE_INDIANA_1">
		<xsl:param name="FACTOR" /> <!-- Depending on the factor element BI/PD..etc -->
		<!--xsl:variable name="PTERRITORYCODE" select="POLICY/TERRITORY"/-->
		<xsl:variable name="PTERRITORYCODE" select="VEHICLES/VEHICLE/TERRCODEGARAGEDLOCATION" />
		<xsl:choose>
			<xsl:when test="$PTERRITORYCODE =''">
			1.00
		</xsl:when>
			<xsl:otherwise>
			IF ('<xsl:value-of select="$FACTOR" />' = 'BI')THEN 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE=$PTERRITORYCODE ]/@BIBASE" /> 
			ELSE IF ('<xsl:value-of select="$FACTOR" />' = 'PD')THEN 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE= $PTERRITORYCODE ]/@PDBASE" /> 
			ELSE IF ('<xsl:value-of select="$FACTOR" />' = 'MEDPAY')THEN 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE= $PTERRITORYCODE ]/@MEDPAYBASE" /> 
			ELSE IF ('<xsl:value-of select="$FACTOR" />' = 'COMP') THEN 			 
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPREHENSIVETERRITORYBASE']/NODE[@ID ='COMPREHENSIVETERRITORY']/ATTRIBUTES[@TERRITORY = $PTERRITORYCODE ]/@BASE" /> 
			ELSE IF ('<xsl:value-of select="$FACTOR" />' = 'COLL') THEN  
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLISIONTERRITORYBASE']/NODE[@ID ='COLLISIONTERRITORY']/ATTRIBUTES[@TERRITORY = $PTERRITORYCODE ]/@BASE" /> 
			ELSE
				1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Territory Base ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Policy Term ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="POLICYTERM"> <!-- in months -->
		<xsl:variable name="PPOLICYTERM">
			<xsl:value-of select="POLICY/POLICYTERMS" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PPOLICYTERM ='' or $PPOLICYTERM = '0'">
				<!--	default policy term is 1.00	defined in master XML -->
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='POLICY_TERM']/NODE[@ID='POLICYTERM']/ATTRIBUTES/@DEFAULT_POLICY_TERM" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SHORT_TERM_TABLES']/NODE[@ID='SPECIAL_EARNED_PREMIUM']/ATTRIBUTES[@MONTHS= $PPOLICYTERM ]/@FACTOR" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Policy Term ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Class  ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="CLASS">
		<xsl:param name="FACTORELEMENT" /> <!-- Depending on the factor element BI/PD..etc -->
		<xsl:variable name="PVEHICLECLASS" select="normalize-space(VEHICLES/VEHICLE/CLASS)" />
		<xsl:choose>
			<xsl:when test="$PVEHICLECLASS ='0' or $PVEHICLECLASS =''">
			1.00
		</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="$FACTORELEMENT = 'BI'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CLASSRELATIVITY']/NODE[@ID='CLASS']/ATTRIBUTES[@CLASSCODE= $PVEHICLECLASS ]/@RELATIVITY" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'PD'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CLASSRELATIVITY']/NODE[@ID='CLASS']/ATTRIBUTES[@CLASSCODE= $PVEHICLECLASS ]/@RELATIVITY" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'MEDPAY'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CLASSRELATIVITY']/NODE[@ID='CLASS']/ATTRIBUTES[@CLASSCODE= $PVEHICLECLASS ]/@RELATIVITY" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'UM'">
					1.00
				</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'UIM'">
					1.00
				</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'COMP'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSCOMPREHENSIVE']/NODE[@ID='DRIVERCLASSCOMP']/ATTRIBUTES[@CLASS= $PVEHICLECLASS ]/@RELATIVITY" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'COLL'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSCOLLISION']/NODE[@ID='DRIVERCLASSCOLL']/ATTRIBUTES[@CLASS= $PVEHICLECLASS ]/@RELATIVITY" />
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Class ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Age ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="AGE">
		<xsl:param name="FACTORELEMENT" /> <!-- Depending on the factor element COMP/COLL..etc -->
		<xsl:variable name="VAR_VEHICLE_AGE" select="VEHICLES/VEHICLE/AGE"></xsl:variable>
		<xsl:variable name="PAGE">
			<xsl:choose>
				<xsl:when test="$VAR_VEHICLE_AGE &gt; '3'">
					<xsl:value-of select="3" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$VAR_VEHICLE_AGE" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PAGE &gt; 0">
				<xsl:choose>
					<xsl:when test="$FACTORELEMENT ='COMP'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEAGEGROUPOTHER']/NODE[@ID='VEHICLEAGEGROUPCOMP']/ATTRIBUTES[@AGE = $PAGE]/@RELATIVITY" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT ='COLL'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEAGEGROUPCOLLISION']/NODE[@ID='VEHICLEAGEGROUPCOLL']/ATTRIBUTES[@AGE = $PAGE ]/@RELATIVITY" />
					</xsl:when>
					<xsl:otherwise>1.00	</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Age ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of CC ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="CC">
		<xsl:param name="FACTORELEMENT" /> <!-- Depending on the factor element BI,PD,MEDPAY..etc -->
		<xsl:variable name="PCC" select="VEHICLES/VEHICLE/CC"></xsl:variable>
		<xsl:variable name="PVEHICLECLASS" select="VEHICLES/VEHICLE/CLASS"></xsl:variable>
		<xsl:choose>
			<xsl:when test="$PCC &gt; 0">
				<xsl:choose>
					<xsl:when test="$FACTORELEMENT ='BI'">
						<xsl:choose>
							<xsl:when test="$PVEHICLECLASS ='A'">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CC_RELATIVITY']/NODE[@ID='CC_RELATIVITY_BI']/ATTRIBUTES[@CC_MIN &lt;= $PCC and @CC_MAX &gt;= $PCC ]/@CLASSA" />
							</xsl:when>
							<xsl:when test="$PVEHICLECLASS ='B'">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CC_RELATIVITY']/NODE[@ID='CC_RELATIVITY_BI']/ATTRIBUTES[@CC_MIN &lt;= $PCC and @CC_MAX &gt;= $PCC ]/@CLASSB" />
							</xsl:when>
							<xsl:when test="$PVEHICLECLASS ='C'">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CC_RELATIVITY']/NODE[@ID='CC_RELATIVITY_BI']/ATTRIBUTES[@CC_MIN &lt;= $PCC and @CC_MAX &gt;= $PCC ]/@CLASSC" />
							</xsl:when>
						</xsl:choose>
					</xsl:when>
					<xsl:when test="$FACTORELEMENT ='PD'">
						<xsl:choose>
							<xsl:when test="$PVEHICLECLASS ='A'">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CC_RELATIVITY']/NODE[@ID='CC_RELATIVITY_PD']/ATTRIBUTES[@CC_MIN &lt;= $PCC and @CC_MAX &gt;= $PCC ]/@CLASSA" />
							</xsl:when>
							<xsl:when test="$PVEHICLECLASS ='B'">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CC_RELATIVITY']/NODE[@ID='CC_RELATIVITY_PD']/ATTRIBUTES[@CC_MIN &lt;= $PCC and @CC_MAX &gt;= $PCC ]/@CLASSB" />
							</xsl:when>
							<xsl:when test="$PVEHICLECLASS ='C'">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CC_RELATIVITY']/NODE[@ID='CC_RELATIVITY_PD']/ATTRIBUTES[@CC_MIN &lt;= $PCC and @CC_MAX &gt;= $PCC ]/@CLASSC" />
							</xsl:when>
						</xsl:choose>
					</xsl:when>
					<xsl:when test="$FACTORELEMENT ='MEDPAY'">
						<xsl:choose>
							<xsl:when test="$PVEHICLECLASS ='A'">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CC_RELATIVITY']/NODE[@ID='CC_RELATIVITY_MEDPAY']/ATTRIBUTES[@CC_MIN &lt;= $PCC and @CC_MAX &gt;= $PCC ]/@CLASSA" />
							</xsl:when>
							<xsl:when test="$PVEHICLECLASS ='B'">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CC_RELATIVITY']/NODE[@ID='CC_RELATIVITY_MEDPAY']/ATTRIBUTES[@CC_MIN &lt;= $PCC and @CC_MAX &gt;= $PCC ]/@CLASSB" />
							</xsl:when>
							<xsl:when test="$PVEHICLECLASS ='C'">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CC_RELATIVITY']/NODE[@ID='CC_RELATIVITY_MEDPAY']/ATTRIBUTES[@CC_MIN &lt;= $PCC and @CC_MAX &gt;= $PCC ]/@CLASSC" />
							</xsl:when>
						</xsl:choose>
					</xsl:when>
					<xsl:when test="$FACTORELEMENT ='COMP'">
						<xsl:choose>
							<xsl:when test="$PVEHICLECLASS ='A'">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SYMBOLRELATIVITYOTHERTHANCOLLISON']/NODE[@ID='SYMBOLRELATIVITYOTHERTHANCOLL']/ATTRIBUTES[@CC_MIN &lt;= $PCC and @CC_MAX &gt;= $PCC ]/@CLASS_A_RELATIVITY" />
							</xsl:when>
							<xsl:when test="$PVEHICLECLASS ='B'">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SYMBOLRELATIVITYOTHERTHANCOLLISON']/NODE[@ID='SYMBOLRELATIVITYOTHERTHANCOLL']/ATTRIBUTES[@CC_MIN &lt;= $PCC and @CC_MAX &gt;= $PCC ]/@CLASS_B_RELATIVITY" />
							</xsl:when>
							<xsl:when test="$PVEHICLECLASS ='C'">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SYMBOLRELATIVITYOTHERTHANCOLLISON']/NODE[@ID='SYMBOLRELATIVITYOTHERTHANCOLL']/ATTRIBUTES[@CC_MIN &lt;= $PCC and @CC_MAX &gt;= $PCC ]/@CLASS_C_RELATIVITY" />
							</xsl:when>
						</xsl:choose>
					</xsl:when>
					<xsl:when test="$FACTORELEMENT ='COLL'">
						<xsl:choose>
							<xsl:when test="$PVEHICLECLASS ='A'">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SYMBOLRELATIVITYCOLLISION']/NODE[@ID='SYMBOLRELATIVITYCOLL']/ATTRIBUTES[@CC_MIN &lt;= $PCC and @CC_MAX &gt;= $PCC ]/@CLASS_A_RELATIVITY" />
							</xsl:when>
							<xsl:when test="$PVEHICLECLASS ='B'">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SYMBOLRELATIVITYCOLLISION']/NODE[@ID='SYMBOLRELATIVITYCOLL']/ATTRIBUTES[@CC_MIN &lt;= $PCC and @CC_MAX &gt;= $PCC ]/@CLASS_B_RELATIVITY" />
							</xsl:when>
							<xsl:when test="$PVEHICLECLASS ='C'">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SYMBOLRELATIVITYCOLLISION']/NODE[@ID='SYMBOLRELATIVITYCOLL']/ATTRIBUTES[@CC_MIN &lt;= $PCC and @CC_MAX &gt;= $PCC ]/@CLASS_C_RELATIVITY" />
							</xsl:when>
						</xsl:choose>
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
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of CC ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of LIMIT ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="LIMIT">
		<xsl:param name="FACTORELEMENT" />
		<xsl:choose>
			<xsl:when test="$FACTORELEMENT = 'BI'"> <!-- BI -->
				<xsl:variable name="COVERAGELL" select="VEHICLES/VEHICLE/BILIMIT1" /> <!-- Lower Limit CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
				<xsl:variable name="COVERAGEUL" select="VEHICLES/VEHICLE/BILIMIT2" /> <!-- Upper Limit -->
				<xsl:choose>
					<xsl:when test="$COVERAGELL ='' or $COVERAGELL= '0'">
					  1.00   
				</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIMITS']/NODE[@ID= $FACTORELEMENT ]/ATTRIBUTES[@MINCOVERAGE= $COVERAGELL and @MAXCOVERAGE = $COVERAGEUL ]/@RELATIVITY" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT ='UM_MICHIGAN_SPLIT'">
				<xsl:variable name="COVERAGELL" select="VEHICLES/VEHICLE/UMSPLITLIMIT1" /> <!-- Lower Limit CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
				<xsl:variable name="COVERAGEUL" select="VEHICLES/VEHICLE/UMSPLITLIMIT2" /> <!-- Upper Limit -->
				<!-- Split limit -->
				<xsl:choose>
					<xsl:when test="$COVERAGELL !='' and $COVERAGELL!= '0' and $COVERAGEUL !='' and $COVERAGEUL != '0'">
						<xsl:variable name="PMINLIMITUM" select="VEHICLES/VEHICLE/UMSPLITLIMIT1"></xsl:variable>
						<xsl:variable name="PMAXLIMITUM" select="VEHICLES/VEHICLE/UMSPLITLIMIT2"></xsl:variable>
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMBILIMITS']/NODE[@ID ='UMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMITUM and @MAXLIMIT=$PMAXLIMITUM]/@UMPERCYCLE" />
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
				<!-- ELSE
					1.00 -->
			</xsl:when>
			<xsl:when test="$FACTORELEMENT ='UM_MICHIGAN_CSL'">
				<xsl:variable name="PCSL" select="VEHICLES/VEHICLE/UMCSL" />
				<!-- Combined single limit -->
				<xsl:choose>
					<xsl:when test="$PCSL !='' and $PCSL!='0' and $PCSL!='NO COVERAGE'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUMBILIMITS']/NODE[@ID ='CSLUMBI']/ATTRIBUTES[@CSLUMBILIMITS = $PCSL]/@CSLUMPERCYCLE" />
					</xsl:when>
					<xsl:otherwise>
					1.00
				</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='UM'"> <!-- UM -->
				<xsl:variable name="PSTATE" select="POLICY/STATENAME" />
				<!-- SKB -->
				<xsl:variable name="COVERAGEUM" select="VEHICLES/VEHICLE/UMSPLIT" />
				<xsl:variable name="COVERAGELL" select="VEHICLES/VEHICLE/UMSPLITLIMIT1" /> <!-- Lower Limit CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
				<xsl:variable name="COVERAGEUL" select="VEHICLES/VEHICLE/UMSPLITLIMIT2" /> <!-- Upper Limit -->
				<xsl:variable name="PCSL" select="VEHICLES/VEHICLE/UMCSL" />
				<xsl:choose>
					<xsl:when test="$PSTATE = 'MICHIGAN' ">
						<xsl:choose>
							<!-- Check if split limit or combined single limit -->
							<xsl:when test="$COVERAGEUM = '0/0' or $COVERAGEUM ='' or $COVERAGEUM ='NO COVERAGE' or $COVERAGEUM ='0'">
										1.00
									</xsl:when>
							<xsl:when test="$PCSL !='' and $PCSL != '0' "> <!-- If combined single limit -->
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUMBILIMITS']/NODE[@ID ='CSLUMBI']/ATTRIBUTES[@CSLUMBILIMITS = $PCSL]/@CSLUMPERCYCLE" />
							</xsl:when>
							<xsl:otherwise> <!-- If  split limit -->
								<xsl:variable name="PMINLIMIT" select="VEHICLES/VEHICLE/UMSPLITLIMIT1"></xsl:variable>
								<xsl:variable name="PMAXLIMIT" select="VEHICLES/VEHICLE/UMSPLITLIMIT2"></xsl:variable>
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMBILIMITS']/NODE[@ID ='UMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMIT and @MAXLIMIT=$PMAXLIMIT]/@UMPERCYCLE" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT ='UM_INDIANA_SPLIT'">
				<xsl:variable name="COVERAGELL" select="VEHICLES/VEHICLE/UMSPLITLIMIT1" /> <!-- Lower Limit CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
				<xsl:variable name="COVERAGEUL" select="VEHICLES/VEHICLE/UMSPLITLIMIT2" /> <!-- Upper Limit -->
				<!-- Split limit -->
				<xsl:choose>
					<xsl:when test="$COVERAGELL !='' and $COVERAGELL!= '0' and $COVERAGEUL !='' and $COVERAGEUL != '0'">
						<xsl:variable name="PMINLIMITUM" select="VEHICLES/VEHICLE/UMSPLITLIMIT1"></xsl:variable>
						<xsl:variable name="PMAXLIMITUM" select="VEHICLES/VEHICLE/UMSPLITLIMIT2"></xsl:variable>
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SLCYCLES']/NODE[@ID ='SLCYCLE']/ATTRIBUTES[@MINLIMIT = $PMINLIMITUM and @MAXLIMIT=$PMAXLIMITUM]/@UMPERCYCLE" />
					</xsl:when>
					<xsl:otherwise>
					1.00
				</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT ='UM_INDIANA_CSL'">
				<xsl:variable name="PCSL" select="VEHICLES/VEHICLE/UMCSL" />
				<!-- Combined single limit -->
				<xsl:choose>
					<xsl:when test="$PCSL !='' and $PCSL!='0' and $PCSL!='NO COVERAGE'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLCYCLES']/NODE[@ID ='CSLCYCLE']/ATTRIBUTES[@LIMIT = $PCSL]/@UMPERCYCLE" />
					</xsl:when>
					<xsl:otherwise>
					1.00
				</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT = 'UIM'"> <!-- UIM -->
				<xsl:variable name="COVERAGELL" select="VEHICLES/VEHICLE/UIMSPLITLIMIT1" /> <!-- Lower Limit CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
				<xsl:variable name="COVERAGEUL" select="VEHICLES/VEHICLE/UIMSPLITLIMIT2" /> <!-- Upper Limit -->
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/ISUNDERINSUREDMOTORISTS ='Y'">
						<!-- Check if split limit or combined single limit -->
						<xsl:choose>
							<!-- Split Limit -->
							<xsl:when test="(VEHICLES/VEHICLE/UIMSPLIT !='' and VEHICLES/VEHICLE/UIMSPLIT !='0/0' and VEHICLES/VEHICLE/UIMSPLIT !='NO COVERAGE') ">
								<xsl:variable name="PMINLIMIT" select="VEHICLES/VEHICLE/UIMSPLITLIMIT1"></xsl:variable>
								<xsl:variable name="PMAXLIMIT" select="VEHICLES/VEHICLE/UIMSPLITLIMIT2"></xsl:variable>
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UIMBILIMITS']/NODE[@ID ='UIMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMIT and @MAXLIMIT=$PMAXLIMIT]/@UIMRATES" />
							</xsl:when>
							<!-- Combined Single Limit -->
							<xsl:when test="VEHICLES/VEHICLE/UIMCSL !='' and VEHICLES/VEHICLE/UIMCSL !='0' and VEHICLES/VEHICLE/UIMCSL !='NO COVERAGE'">
								<xsl:choose>
									<xsl:when test="VEHICLES/VEHICLE/UMCSL &gt; 0">
										<xsl:variable name="PUMCSL" select="VEHICLES/VEHICLE/UIMCSL" />
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUIMBILIMITS']/NODE[@ID ='CSLUIMBI']/ATTRIBUTES[@CSLUIMBILIMITS = $PUMCSL]/@CSLUIMRATES" />
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
					</xsl:when>
					<xsl:otherwise>
						0.00
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='PD'"> <!-- PD -->
				<xsl:variable name="COVERAGE" select="VEHICLES/VEHICLE/PD" /> <!-- CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
				<xsl:choose>
					<xsl:when test="$COVERAGE ='' or $COVERAGE = '0'">
					1.00
				</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIMITS']/NODE[@ID = $FACTORELEMENT ]/ATTRIBUTES[@COVERAGE = $COVERAGE ]/@RELATIVITY" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='MEDPAYOPTION'"> <!-- MEDPAY limit :michigan-->
				<xsl:call-template name="MEDPAYLIMIT_RELATIVITY_MICHIGAN" />
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='MEDPAYCOVGLIMIT'"> <!-- MEDPAY limit :indiana -->
				<xsl:call-template name="MEDPAYLIMIT_RELATIVITY_INDIANA" />
			</xsl:when>
			<xsl:otherwise> 
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="MEDPAYLIMIT_RELATIVITY_MICHIGAN">
		<xsl:variable name="PMEDICALTYPE">
			<xsl:value-of select="VEHICLES/VEHICLE/MEDICALTYPE" />
		</xsl:variable>
		<xsl:variable name="PMEDICALDEDUCTIBLE">
			<xsl:value-of select="normalize-space(VEHICLES/VEHICLE/MEDICALDEDUCTIBLE)" />
		</xsl:variable>
		<xsl:variable name="PMEDICALDESCRIPTION">
			<xsl:value-of select="VEHICLES/VEHICLE/TYPE" />
		</xsl:variable>
		<xsl:variable name="PMEDICALLIMIT">
			<xsl:value-of select="VEHICLES/VEHICLE/MEDPMLIMIT" />
		</xsl:variable>
		<xsl:variable name="PBASE">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MEDICALOPTIONS']/NODE[@ID = 'MEDICALOPTIONS_FACTOR']/ATTRIBUTES[@TYPE = $PMEDICALTYPE and @DEDUCTIBLE=$PMEDICALDEDUCTIBLE ]/@BASE" />
		</xsl:variable>
		<!-- Calculations - Medical may be purchased in increments of 5000 .for limits above 5000 multiply the  base
		 by the units desired. Exception: if the M-43 option for 1000 is chosen then there is no more than 1000 limit available 
		Formula will be Factor * (Limit divided by base)-->
		<xsl:choose>
			<xsl:when test="$PMEDICALTYPE !='' and $PMEDICALTYPE != 'NO COVERAGE'">
				<xsl:variable name="VAR1">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MEDICALOPTIONS']/NODE[@ID = 'MEDICALOPTIONS_FACTOR' ]/ATTRIBUTES[@TYPE = $PMEDICALTYPE and @DEDUCTIBLE=$PMEDICALDEDUCTIBLE ]/@MEDICALRELATIVITY" />
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:value-of select="$PMEDICALLIMIT div $PBASE" />
				</xsl:variable>
				<!--xsl:value-of select="$VAR1*$VAR2" /-->
				<xsl:value-of select="$VAR1"/>
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
		<xsl:template name="MEDPAYLIMIT_INCREASEDLIMITS_MICHIGAN">
		<xsl:variable name="PMEDICALTYPE">
			<xsl:value-of select="VEHICLES/VEHICLE/MEDICALTYPE" />
		</xsl:variable>
		<xsl:variable name="PMEDICALDEDUCTIBLE">
			<xsl:value-of select="normalize-space(VEHICLES/VEHICLE/MEDICALDEDUCTIBLE)" />
		</xsl:variable>
		<xsl:variable name="PMEDICALDESCRIPTION">
			<xsl:value-of select="VEHICLES/VEHICLE/TYPE" />
		</xsl:variable>
		<xsl:variable name="PMEDICALLIMIT">
			<xsl:value-of select="VEHICLES/VEHICLE/MEDPMLIMIT" />
		</xsl:variable>
		<xsl:variable name="PBASE">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MEDICALOPTIONS']/NODE[@ID = 'MEDICALOPTIONS_FACTOR']/ATTRIBUTES[@TYPE = $PMEDICALTYPE and @DEDUCTIBLE=$PMEDICALDEDUCTIBLE ]/@BASE" />
		</xsl:variable>
		<!-- Calculations - Medical may be purchased in increments of 5000 .for limits above 5000 multiply the  base
		 by the units desired. Exception: if the M-43 option for 1000 is chosen then there is no more than 1000 limit available 
		Formula will be Factor * (Limit divided by base)-->
		<xsl:choose>
			<xsl:when test="$PMEDICALTYPE !='' and $PMEDICALTYPE != 'NO COVERAGE'">
				<xsl:variable name="VAR1">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MEDICALOPTIONS']/NODE[@ID = 'MEDICALOPTIONS_FACTOR' ]/ATTRIBUTES[@TYPE = $PMEDICALTYPE and @DEDUCTIBLE=$PMEDICALDEDUCTIBLE ]/@MEDICALRELATIVITY" />
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:value-of select="$PMEDICALLIMIT div $PBASE" />
				</xsl:variable>
				<!--xsl:value-of select="$VAR1*$VAR2" /-->
				<xsl:value-of select="$VAR2"/>
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="MEDPAYLIMIT_INCREASEDLIMITS">
		<xsl:choose>
			<!-- For Michigan - Motorcycle  -->
			<xsl:when test="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name="MEDPAYLIMIT_INCREASEDLIMITS_MICHIGAN" />
			</xsl:when>
			<!-- For Indiana - Motorcycle.. can call a separate template if the logic for calculation in different states changes. -->
			<xsl:when test="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name="MEDPAYLIMIT_INCREASEDLIMITS_INDIANA" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>		
	</xsl:template>
	<xsl:template name="MEDPAYLIMIT_RELATIVITY_INDIANA">
		<!-- <xsl:variable name="PMEDICALTYPE"><xsl:value-of select="VEHICLES/VEHICLE/MEDPMTYPE"/></xsl:variable> -->
		<xsl:variable name="PMEDICALTYPE">
			<xsl:value-of select="VEHICLES/VEHICLE/MEDICALTYPE" />
		</xsl:variable>
		<xsl:variable name="PMEDICALLIMIT">
			<xsl:value-of select="VEHICLES/VEHICLE/MEDPM" />
		</xsl:variable>
		<xsl:variable name="PBASE">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MEDICALPAYMENT']/NODE[@ID = 'MEDICALPAYMENT_FACTOR']/ATTRIBUTES[@TYPE=$PMEDICALTYPE]/@BASE" />
		</xsl:variable>
		<!-- Calculations -  Formula will be Factor * (Limit divided by base)-->
		<xsl:choose>
			<xsl:when test="$PMEDICALTYPE !='' and $PMEDICALTYPE != 'NO COVERAGE'">
				<xsl:variable name="VAR1">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MEDICALPAYMENT']/NODE[@ID = 'MEDICALPAYMENT_FACTOR' ]/ATTRIBUTES[@TYPE=$PMEDICALTYPE]/@MEDICALRELATIVITY" />
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:value-of select="$PMEDICALLIMIT div $PBASE" />
				</xsl:variable>
				<!--xsl:value-of select="$VAR1 * $VAR2" /-->
				<xsl:value-of select="$VAR1" />
			</xsl:when>
			<xsl:otherwise>
			  1.00  
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MEDPAYLIMIT_INCREASEDLIMITS_INDIANA">
		<!-- <xsl:variable name="PMEDICALTYPE"><xsl:value-of select="VEHICLES/VEHICLE/MEDPMTYPE"/></xsl:variable> -->
		<xsl:variable name="PMEDICALTYPE">
			<xsl:value-of select="VEHICLES/VEHICLE/MEDICALTYPE" />
		</xsl:variable>
		<xsl:variable name="PMEDICALLIMIT">
			<xsl:value-of select="VEHICLES/VEHICLE/MEDPM" />
		</xsl:variable>
		<xsl:variable name="PBASE">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MEDICALPAYMENT']/NODE[@ID = 'MEDICALPAYMENT_FACTOR']/ATTRIBUTES[@TYPE=$PMEDICALTYPE]/@BASE" />
		</xsl:variable>
		<!-- Calculations -  Formula will be Factor * (Limit divided by base)-->
		<xsl:choose>
			<xsl:when test="$PMEDICALTYPE !='' and $PMEDICALTYPE != 'NO COVERAGE'">
				<xsl:variable name="VAR1">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MEDICALPAYMENT']/NODE[@ID = 'MEDICALPAYMENT_FACTOR' ]/ATTRIBUTES[@TYPE=$PMEDICALTYPE]/@MEDICALRELATIVITY" />
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:value-of select="$PMEDICALLIMIT div $PBASE" />
				</xsl:variable>
				<!--xsl:value-of select="$VAR1 * $VAR2" /-->
				<xsl:value-of select="$VAR2" />
			</xsl:when>
			<xsl:otherwise>
			  1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Limit ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of CSL Limit ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="CSL_LIMIT">
		<xsl:variable name="PCSLLIMIT" select="VEHICLES/VEHICLE/CSL" /> <!-- ASK DEEPAK TO GIVE CSL FOR ALL NODES -->
		<xsl:choose>
			<xsl:when test="$PCSLLIMIT !='' and $PCSLLIMIT &gt; 0">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMBINEDSINGLELIMITS']/NODE[@ID ='CSL']/ATTRIBUTES[@CSLLIMIT=$PCSLLIMIT]/@CSLRELATIVITY" />
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of CSL Limit ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Accident violation factor ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="ACCIDENT_VIOLATION_FACTOR"> <!--SURCHARGE -->
		<!-- 1. Get the violation and accident points.
		2. Against each violation/accident point, fetch the surcharge  (After 13 points, calculate the surcharge using "For each additional" value
		3. Add the surcharges
		4. Divide the sum by 100 and add 1 -->
		<!-- Accident Surcharge  -->
		<xsl:variable name="ACCIDENTPOINTS">
			<xsl:value-of select="VEHICLES/VEHICLE/SUMOFACCIDENTPOINTS" />
		</xsl:variable>
		<xsl:variable name="VIOLATIONPOINTS">
			<xsl:value-of select="VEHICLES/VEHICLE/SUMOFVIOLATIONPOINTS" />
		</xsl:variable>
		<xsl:variable name="MISCPOINTS">
			<xsl:value-of select="VEHICLES/VEHICLE/SUMOFMISCPOINTS" />
		</xsl:variable>
		<xsl:variable name="TOTALPOINTS">
			<xsl:value-of select="$VIOLATIONPOINTS + $ACCIDENTPOINTS + $MISCPOINTS" />
		</xsl:variable>
		<xsl:variable name="ACCIDENTSURCHARGE">
			<xsl:choose>
				<xsl:when test="$TOTALPOINTS !='' and $TOTALPOINTS !='0' and $TOTALPOINTS &gt; 0">
					<xsl:choose>
						<xsl:when test="$TOTALPOINTS &gt; 13">
							<xsl:variable name="VAR1">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS=13]/@SURCHARGE" />
							</xsl:variable>
							<xsl:variable name="VAR2">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/@FOR_EACH_ADDITIONAL" />
							</xsl:variable>
							<xsl:variable name="VAR3">
								<xsl:value-of select="($TOTALPOINTS - 13) * $VAR2" />
							</xsl:variable>
							<xsl:value-of select="$VAR1 + $VAR3" />
							<!--xsl:value-of select="$VAR1"/-->
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS=$TOTALPOINTS]/@SURCHARGE" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:when>
				<xsl:otherwise>0.00</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:value-of select="(($ACCIDENTSURCHARGE) div 100) + 1.00" />
		<!--xsl:value-of select="$ACCIDENTSURCHARGE"/-->
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Accident violation factor ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Deductible ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="DEDUCTIBLE">
		<xsl:param name="FACTORELEMENT" />
		<xsl:choose>
			<xsl:when test="$FACTORELEMENT = 'COMP'">
				<xsl:variable name="PCOMPREHENSIVEDEDUCTIBLE" select="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE" />
				<xsl:choose>
					<xsl:when test="$PCOMPREHENSIVEDEDUCTIBLE ='' or $PCOMPREHENSIVEDEDUCTIBLE ='0'"><!-- SKB-->
					1.00
				</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLEO']/NODE[@ID='DEDTBLO']/ATTRIBUTES[@DEDUCTIBLE = $PCOMPREHENSIVEDEDUCTIBLE ]/@RELATIVITY" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT = 'COLL' and POLICY/STATENAME ='MICHIGAN'">
				<xsl:variable name="PCOLLISIONDEDUCTIBLE" select="VEHICLES/VEHICLE/COLLISIONDEDUCTIBLE" />
				<xsl:choose>
					<xsl:when test="$PCOLLISIONDEDUCTIBLE ='' or $PCOLLISIONDEDUCTIBLE ='0'"><!-- SKB-->
						0.00
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@DEDUCTIBLE = $PCOLLISIONDEDUCTIBLE ]/@RELATIVITY" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT = 'COLL' and POLICY/STATENAME ='INDIANA'">
				<xsl:variable name="PCOLLISIONDEDUCTIBLE" select="VEHICLES/VEHICLE/COLLISIONDEDUCTIBLE" />
				<xsl:choose>
					<xsl:when test="$PCOLLISIONDEDUCTIBLE ='' or $PCOLLISIONDEDUCTIBLE ='0'"><!-- SKB-->
					0.00
				</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@DEDUCTIBLE = $PCOLLISIONDEDUCTIBLE ]/@RELATIVITY" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Deductible ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="MEDICAL_DEDUCTIBLE">
		<xsl:variable name="VAR1">
			<xsl:value-of select="POLICY/MEDICALDEDUCTIBLE" />
		</xsl:variable>
		<xsl:if test="$VAR1 &gt; 0">
			<xsl:value-of select="$VAR1" />
		</xsl:if>
	</xsl:template>
	<!-- *******************************************************************************************************-->
	<!-- ************** Other Templates for DISCOUNT TEXT IN SPLIT TABLE (START) **************************************************************************-->
	<!-- *******************************************************************************************************-->
	<xsl:template name="REMARKMULTICYCLE"><xsl:text>Discount : Multi-Cycle</xsl:text></xsl:template>
	<xsl:template name="REMARKTRANSFERRENEWAL"><xsl:text>Discount : Transfer/Renewal</xsl:text></xsl:template>
	<xsl:template name="REMARKMULTIPOLICY"><xsl:text>Discount : Multi-Policy (Cycle/Auto/Home)</xsl:text></xsl:template>
	<xsl:template name="REMARKPREFERDRISK"><xsl:text>Discount : Preferred Risk</xsl:text></xsl:template>
	<xsl:template name="EXT_AD_PREFERDRISK">
		<!-- A preferred risk credit will be given when
			 1. the operator has been licensed for three yr to drive an auto or motorcycle
			 2. had no surchargeeable accidents or violations exceeding 2 points. 
		-->
		<xsl:variable name="PLICUNDER3YRS">
			<xsl:value-of select="VEHICLES/VEHICLE/ASSIGNEDDRIVERLICENCE3YEAR" />
		</xsl:variable>
		<xsl:variable name="PALLDRIVERSOVER18">
			<xsl:value-of select="VEHICLES/VEHICLE/ALLDRIVERSOVER18" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PLICUNDER3YRS ='TRUE' and  $PALLDRIVERSOVER18 ='TRUE'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PREFERREDRISK']/NODE[@ID ='PREFERREDRISKDISCOUNT']/ATTRIBUTES/@CREDIT" />
				<xsl:text>%</xsl:text>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="REMARKMATUREOPERATOR"><xsl:text>Discount : Mature Operator</xsl:text></xsl:template>
	<xsl:template name="REMARKINSURANCESCORE"><xsl:text>Discount : Insurance Score Credit(</xsl:text><xsl:call-template name="INSURENCESCOREDISPLAY" /><xsl:text>)</xsl:text></xsl:template>
	<xsl:template name="REMARKNOCYCLEENDORS"><xsl:text>Surcharge : No Cycle Endorsement on License</xsl:text></xsl:template>
	<xsl:template name="REMARKBIKETYPETEXT">
		<xsl:variable name="PBIKETYPE" select="VEHICLES/VEHICLE/VEHICLETYPE" />
		<!--Get the factor -->
		<xsl:variable name="PFACTOR">
			<xsl:call-template name="TYPE_OF_BIKE" />
		</xsl:variable>
		<!--Get the description -->
		<xsl:variable name="PDESCRIPTION">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPE_OF_BIKE']/NODE[@ID='BIKETYPE']/ATTRIBUTES[@TYPE = $PBIKETYPE]/@DESC" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PFACTOR != '' and $PFACTOR &lt; 1.00">
				<xsl:text>Discount : </xsl:text><xsl:value-of select="$PDESCRIPTION" /> 
			</xsl:when>
			<xsl:when test="$PFACTOR != '' and $PFACTOR &gt; 1.00">
				<xsl:text>Surcharge : </xsl:text><xsl:value-of select="$PDESCRIPTION" /> 
			</xsl:when>
			<xsl:when test="$PFACTOR != '' and $PFACTOR = 1.00">
				0
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="EXT_ADBIKETYPEPERCENTAGE">
		<xsl:variable name="PBIKETYPE" select="VEHICLES/VEHICLE/VEHICLETYPE" />
		<!--Get the factor -->
		<xsl:variable name="PFACTOR">
			<xsl:call-template name="TYPE_OF_BIKE" />
		</xsl:variable>
		<!--Get the description -->
		<xsl:variable name="PDESCRIPTION">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPE_OF_BIKE']/NODE[@ID='BIKETYPE']/ATTRIBUTES[@TYPE = $PBIKETYPE]/@DESC" />
		</xsl:variable>
		<!-- converting   factor into percentage -->
		<xsl:variable name="VAR_PERCENT">
			<xsl:choose>
				<xsl:when test="$PFACTOR != '' and $PFACTOR &lt; 1.00">
					<xsl:value-of select="round((1 - $PFACTOR) * 100)" />
					<xsl:text>%</xsl:text>
				</xsl:when>
				<xsl:when test="$PFACTOR != '' and $PFACTOR &gt; 1.00">
					<xsl:value-of select="round(($PFACTOR -1 ) * 100)" />
					<xsl:text>%</xsl:text>
				</xsl:when>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PFACTOR != ''">
				<xsl:value-of select="$VAR_PERCENT" />
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="REMARKACCIDENTVIOLATION"><xsl:text>Surcharge : Accident and Violation</xsl:text></xsl:template>
	<!-- *******************************************************************************************************-->
	<!-- ************** Other Templates for DISCOUNT TEXT IN SPLIT TABLE (END) **************************************************************************-->
	<!-- *******************************************************************************************************-->
	<!-- *******************************************************************************************************-->
	<!-- ************** Other Templates for Lables **************************************************************************-->
	<!-- *******************************************************************************************************-->
	<!--  Group Details  -->
	<xsl:template name="GROUPID0"><xsl:choose><xsl:when test="VEHICLES/VEHICLE/COMPONLY='YES'"><xsl:text>MOTORCYCLES (COMPREHENSIVE ONLY)</xsl:text></xsl:when>	<xsl:otherwise><xsl:text>MOTORCYCLES</xsl:text></xsl:otherwise></xsl:choose></xsl:template>
	<xsl:template name="GROUPID1"><xsl:text>Final Premium</xsl:text></xsl:template>
	<xsl:template name="PRODUCTNAME"><xsl:text>Motorcycle</xsl:text></xsl:template>
	<xsl:template name="STEPID0"><xsl:value-of select="VEHICLES/VEHICLE/VEHICLEROWID" />.<xsl:value-of select="'   '" /><xsl:value-of select="VEHICLES/VEHICLE/YEAR" /> <xsl:value-of select="'   '" />  <xsl:value-of select="VEHICLES/VEHICLE/MAKE" /> <xsl:value-of select="'   '" />   <xsl:value-of select="VEHICLES/VEHICLE/MODEL" /></xsl:template>
	<xsl:template name="STEPID1"><xsl:text>CC:</xsl:text><xsl:value-of select="VEHICLES/VEHICLE/CC" /><xsl:text>, Symbol </xsl:text><xsl:value-of select="VEHICLES/VEHICLE/SYMBOL" /></xsl:template>
	<xsl:template name="STEPID2"><xsl:text>Class </xsl:text><xsl:value-of select="VEHICLES/VEHICLE/CLASS" /><xsl:text>, </xsl:text><xsl:value-of select="VEHICLES/VEHICLE/VEHICLETYPEDESC" /></xsl:template>
	<xsl:template name="STEPID3"><xsl:choose><xsl:when test="VEHICLES/VEHICLE/ZIPCODEGARAGEDLOCATION !='' and VEHICLES/VEHICLE/ZIPCODEGARAGEDLOCATION !='0'"><xsl:text>Garaged Location: </xsl:text><xsl:value-of select="VEHICLES/VEHICLE/GARAGEDLOCATION" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose></xsl:template>
	<xsl:template name="STEPID4"><xsl:text>Bodily Injury Liability</xsl:text></xsl:template>
	<xsl:template name="STEPID5"><xsl:text>Property Damage Liability</xsl:text></xsl:template>
	<xsl:template name="STEPID6"><xsl:text>Combined Single Limit Liability (BI and PD)</xsl:text></xsl:template>
	<xsl:template name="STEPID7"><xsl:text>Michigan Statutory Assessments</xsl:text></xsl:template>
	<xsl:template name="STEPID8"><xsl:call-template name="MEDICAL_PAYMENTS_DISPLAY" /></xsl:template>
	<xsl:template name="STEPID9"><xsl:call-template name="MEDICAL_OPTIONS_DISPLAY" /></xsl:template>
	<xsl:template name="STEPID10"><xsl:text>Uninsured Motorists</xsl:text></xsl:template>
	<xsl:template name="STEPID11"><xsl:text>Underinsured Motorists</xsl:text></xsl:template>
	<xsl:template name="STEPID12"><xsl:text>Uninsured Motorists Property Damage</xsl:text></xsl:template>
	<xsl:template name="STEPID13"><xsl:text>Comprehensive</xsl:text></xsl:template>
	<xsl:template name="STEPID14"><xsl:text>Collision</xsl:text></xsl:template>
	<xsl:template name="STEPID15"><xsl:text>Road Service</xsl:text></xsl:template>
	<xsl:template name="STEPID16"><xsl:text>Helmet and Riding Apparel(M-15)</xsl:text></xsl:template>
	<xsl:template name="STEPID17"><xsl:text>Trailer Coverage(M-49)</xsl:text></xsl:template>
	<xsl:template name="STEPID18"><xsl:text>Additional Physical Damage Coverage</xsl:text></xsl:template>
	<xsl:template name="STEPID19"><xsl:text>Discount : Multi-Cycle -</xsl:text><xsl:call-template name="MULTIVEHICLE_CREDIT" /></xsl:template>
	<xsl:template name="STEPID20"><xsl:text>Discount : Transfer/Renewal -</xsl:text><xsl:call-template name="TRANSFER_RENEWAL_CREDIT" /></xsl:template>
	<xsl:template name="STEPID21"><xsl:text>Discount : Multi-Policy (Cycle/Auto/Home) -</xsl:text><xsl:call-template name="MULTIPOLICY_CREDIT" /></xsl:template>
	<xsl:template name="STEPID22"><xsl:text>Discount : Preferred Risk</xsl:text><xsl:call-template name="PREFERRED_RISK_CREDIT" /></xsl:template>
	<xsl:template name="STEPID23"><xsl:text>Discount : Mature Operator -</xsl:text><xsl:call-template name="MATURE_CREDIT_PERCENT" /><xsl:text>%</xsl:text></xsl:template>
	<xsl:template name="STEPID24"><xsl:text>Discount : Insurance Score Credit(</xsl:text><xsl:call-template name="INSURENCESCOREDISPLAY" /><xsl:text>) -</xsl:text><xsl:call-template name="INSURANCESCORE_PERCENT" /></xsl:template>
	<xsl:template name="STEPID25"><xsl:text>Surcharge : No Cycle Endorsement on License -</xsl:text><xsl:call-template name="NO_CYCLE_ENDORSEMENT_SURCHARGE_CREDIT" /></xsl:template>
	<xsl:template name="STEPID26"><xsl:call-template name="BIKE_TYPE_TEXT" /></xsl:template>
	<xsl:template name="STEPID28"><xsl:text>Surcharge : Accident and Violation -</xsl:text><xsl:call-template name="ACCIDENT_VIOLATION_SURCHRAGE_PERCENTAGE" /><xsl:text>%</xsl:text></xsl:template>
	<xsl:template name="STEPID27"><xsl:text>Cycle (Comp Only) Minimum Premium</xsl:text></xsl:template>
	<xsl:template name="STEPID29"><xsl:text>Total Motorcycle Premium</xsl:text></xsl:template>
	<xsl:template name="MEDICAL_PAYMENTS_DISPLAY"><xsl:text>Medical Payments ( </xsl:text><xsl:value-of select="VEHICLES/VEHICLE/MEDPMTYPE" /><xsl:text> )</xsl:text></xsl:template>
	<xsl:template name="MEDICAL_OPTIONS_DISPLAY"><xsl:choose><xsl:when test="normalize-space(POLICY/STATENAME) = 'MICHIGAN'"><xsl:text>Medical Payments options (</xsl:text><xsl:value-of select="VEHICLES/VEHICLE/TYPE" /><xsl:text>)</xsl:text></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose></xsl:template>
	<xsl:template name="COMPONENT_CODE">
		<xsl:param name="FACTORELEMENT" />
		<xsl:choose>
			<xsl:when test="$FACTORELEMENT=normalize-space('BI')">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'"><xsl:text>207</xsl:text></xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'"><xsl:text>127</xsl:text></xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('PD')">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'"><xsl:text>208</xsl:text></xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'"><xsl:text>128</xsl:text></xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('CSL')">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'"><xsl:text>206</xsl:text></xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'"><xsl:text>126</xsl:text></xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('MED_PMT')">
				<xsl:if test="POLICY/STATENAME='INDIANA'"><xsl:text>770</xsl:text></xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('MED_OPT')">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
					<xsl:choose>
						<xsl:when test="VEHICLES/VEHICLE/TYPE=normalize-space('$1000 Medical')"><xsl:text>843</xsl:text></xsl:when>
						<xsl:otherwise><xsl:text>769</xsl:text></xsl:otherwise>
					</xsl:choose>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('UM')">
				<xsl:choose>
					<xsl:when test="(VEHICLES/VEHICLE/UMSPLIT !='' and VEHICLES/VEHICLE/UMSPLIT !='0/0' and VEHICLES/VEHICLE/UMSPLIT !='NO COVERAGE')">
						<xsl:if test="POLICY/STATENAME='MICHIGAN'"><xsl:text>212</xsl:text></xsl:if>
						<xsl:if test="POLICY/STATENAME='INDIANA'"><xsl:text>132</xsl:text></xsl:if>
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/UMCSL !='' and VEHICLES/VEHICLE/UMCSL !='0' and VEHICLES/VEHICLE/UMCSL !='NO COVERAGE' ">
						<xsl:if test="POLICY/STATENAME='MICHIGAN'"><xsl:text>211</xsl:text></xsl:if>
						<xsl:if test="POLICY/STATENAME='INDIANA'"><xsl:text>131</xsl:text></xsl:if>
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('UIM')">
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/ISUNDERINSUREDMOTORISTS ='Y'">
						<xsl:choose>
							<xsl:when test="(VEHICLES/VEHICLE/UIMSPLIT !='' and VEHICLES/VEHICLE/UIMSPLIT !='0/0' and VEHICLES/VEHICLE/UIMSPLIT !='NO COVERAGE')">
								<xsl:if test="POLICY/STATENAME='INDIANA'"><xsl:text>214</xsl:text></xsl:if>
							</xsl:when>
							<xsl:when test="VEHICLES/VEHICLE/UIMCSL !='' and VEHICLES/VEHICLE/UIMCSL !='0' and VEHICLES/VEHICLE/UIMCSL !='NO COVERAGE'">
								<xsl:if test="POLICY/STATENAME='INDIANA'"><xsl:text>133</xsl:text></xsl:if>
							</xsl:when>
							<xsl:otherwise>0</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('UIMPD')">
				<xsl:choose>
					<xsl:when test="(VEHICLES/VEHICLE/TYPE != ''and VEHICLES/VEHICLE/TYPE !=normalize-space('BI ONLY') and VEHICLES/VEHICLE/PDLIMIT &gt; 0)">
						<xsl:if test="POLICY/STATENAME='INDIANA'"><xsl:text>199</xsl:text></xsl:if>
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('COMP')">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'"><xsl:text>217</xsl:text></xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'"><xsl:text>201</xsl:text></xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('COMP')">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'"><xsl:text>217</xsl:text></xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'"><xsl:text>201</xsl:text></xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('COLL')">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'"><xsl:text>216</xsl:text></xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'"><xsl:text>200</xsl:text></xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('RD_SRVC')">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'"><xsl:text>218</xsl:text></xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'"><xsl:text>202</xsl:text></xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('HELMET')">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'"><xsl:text>219</xsl:text></xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'"><xsl:text>203</xsl:text></xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('TRLR')">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'"><xsl:text>220</xsl:text></xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'"><xsl:text>204</xsl:text></xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('ADD_PD')">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'"><xsl:text>1024</xsl:text></xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'"><xsl:text>1023</xsl:text></xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('ADD_PD')">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'"><xsl:text>1024</xsl:text></xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'"><xsl:text>1023</xsl:text></xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT=normalize-space('MCCAFEE')"><xsl:text>20008</xsl:text></xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Accident and Violation-->
</xsl:stylesheet>
