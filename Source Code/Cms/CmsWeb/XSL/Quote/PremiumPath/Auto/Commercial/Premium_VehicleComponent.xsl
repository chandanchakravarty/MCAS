<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xinclude="http://www.w3.org/1999/XML/xinclude"
	version="1.0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="urn:user-namespace-here">
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
	<xsl:variable name="HOCoveragesDoc" select="document('FactorPath')" />
	<xsl:variable name="VT_TRAILER" select="'TR'" />
	<xsl:variable name="VT_COLL" select="'COLL'" />
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
						<!-- Radius of Use -->
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
						<!-- Residual Bodily Injury -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="4">
								{
									<PATH>
										<xsl:call-template name="BI" />
									</PATH>
								}
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Residual Property Damage -->
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
								 <xsl:call-template name="CSLBIPD" /> 
										 
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- PIP -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="7">
									<PATH>
							{
								 <xsl:call-template name="PIP" /> 
								 
							}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Michigan Statutory Assessment -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="8">
									<PATH>
							{
								 <xsl:call-template name="MCCAFEE" />  
								 
							}	
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- PPI -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="9">
									<PATH>
								{
									 <xsl:call-template name="PPI" /> 
									 
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Medical Payments -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="10">
									<PATH>
								{
									<xsl:call-template name="MEDPAY" />  
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!--Uninsured Motorists -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="11">
									<PATH>
								{
									<xsl:call-template name="UM" />  
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Under Insured Motorists -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="12">
									<PATH>
								{
									<xsl:call-template name="UIM" /> 
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!--Uninsured Motorists - Property Damagae-->
						<SUBGROUP>
							<IF>
								<STEP STEPID="13">
									<PATH>
								{
									<xsl:call-template name="UMPD" />  
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Comprehensive -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="14">
									<PATH>
								{
									<xsl:call-template name="COMP" />  
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Collision -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="15">
									<PATH>
								{
									<xsl:call-template name="COLL" /> 
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Mini-tort -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="16">
									<PATH>
										<xsl:call-template name="MINITORT" />
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Extra Equipment - Comprehensive  -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="17">
									<PATH>
								{
									<xsl:call-template name="EXTRAEQUIPMENTCOMP"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Extra Equipment -Broadened Collision -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="18">
									<PATH>
								{
									<xsl:call-template name="EXTRAEQUIPMENTCOLL"></xsl:call-template>
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!--Extended Non-Owned Coverage for Named Individual (A-35)-->
						<SUBGROUP>
							<IF>
								<STEP STEPID="19">
									<PATH>
								{
									<xsl:call-template name="ENOLIABILITY_PREMIUM" />
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Discount:Work Loss Waiver -->
						<SUBGROUP>
							<STEP STEPID="20">
								<PATH>
									<xsl:call-template name="WAIVEWORKLOSS_DISPLAY"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Discount:Multi-Car-->
						<SUBGROUP>
							<STEP STEPID="21">
								<PATH>
									<xsl:call-template name="MULTIVEHICLE_DISPLAY"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Discount:Multi-Policy(Auto/Home)-->
						<SUBGROUP>
							<STEP STEPID="22">
								<PATH>
									<xsl:call-template name="MULTIPOLICY_DISPLAY"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Discount:Insurance Score Credit-->
						<SUBGROUP>
							<STEP STEPID="23">
								<PATH>
									<xsl:call-template name="INSURANCESCORE_DISPLAY"></xsl:call-template>
								</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="24">
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
						<!-- Final Premium -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="25">
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
						<!-- Residual Bodily Injury -->
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
								<xsl:call-template name="BI" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Residual Property Damage -->
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
								<xsl:call-template name="PD" />
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
								<xsl:call-template name="CSLBIPD" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'CSL'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- PIP -->
						<STEP STEPID="7" CALC_ID="1007" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID7" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH><xsl:call-template name="PIP_DEDUCTIBLE" /></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>PIP</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="PIP" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'PIP'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Michigan Statutory Assessment -->
						<STEP STEPID="8" CALC_ID="1008" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID8" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MCCAFEE</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="MCCAFEE" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'MCCAFEE'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- PPI -->
						<STEP STEPID="9" CALC_ID="1009" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID9" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>1,000,000</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>PPI</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="PPI" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'PPI'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Medical Payments -->
						<STEP STEPID="10" CALC_ID="1010" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID10" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/MPLIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MED_PMT</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="MEDPAY" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'MED_PMT'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Uninsured Motorist -->
						<STEP STEPID="11" CALC_ID="1011" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID11" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:call-template name="UNINSUREDMOTORIST" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>UM</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="UM" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'UM'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Underinsured Motorist -->
						<STEP STEPID="12" CALC_ID="1012" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID12" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:call-template name="UIMINSUREDMOTORIST" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>UIM</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="UIM" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'UIM'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Uninsured Motorist -Property Damage-->
						<STEP STEPID="13" CALC_ID="1013" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID13" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:call-template name="PDDEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/PDLIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>UMPD</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="UMPD" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'UMPD'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Comprehensive -->
						<STEP STEPID="14" CALC_ID="1014" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID14" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE" />
							</D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>COMP</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="COMP" />
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
						<STEP STEPID="15" CALC_ID="1015" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID15" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<!--xsl:value-of select="VEHICLES/VEHICLE/COLLISIONDEDUCTIBLE" /-->
								<xsl:choose>
									<xsl:when test="VEHICLES/VEHICLE/COLLISIONDEDUCTIBLE='0  LIMITED'">LIMITED</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="VEHICLES/VEHICLE/COLLISIONDEDUCTIBLE" />
									</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>COLL</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="COLL" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Mini-tort -->
						<STEP STEPID="16" CALC_ID="1016" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID16" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:call-template name="MINITORTLIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>M_TRT</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="MINITORT" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'M_TRT'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Extra Equipment - Comprehensive  -->
						<STEP STEPID="17" CALC_ID="1017" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID17" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/EXTRAEQUIPCOMPREHENSIVEDEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/INSURANCEAMOUNT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>XTR_COMP</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="EXTRAEQUIPMENTCOMP"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'XTR_COMP'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Extra Equipment -Broadened Collision -->
						<STEP STEPID="18" CALC_ID="1018" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID18" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:call-template name="EXTRAEQUIPMENT_DEDUC" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/INSURANCEAMOUNT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>XTR_COLL</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="EXTRAEQUIPMENTCOLL"></xsl:call-template>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'XTR_COLL'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--Extended Non-Owned Coverage for Named Individual (A-35)-->
						<STEP STEPID="19" CALC_ID="1019" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID19" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>ENOL</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="ENOLIABILITY_PREMIUM" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'ENO'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Discount:Work Loss Waiver -->
						<STEP STEPID="20" CALC_ID="1020" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID20" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>D_WRK_LSS</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS><xsl:call-template name="STEPID20" /></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Discount:Multi-Car-->
						<STEP STEPID="21" CALC_ID="1021" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID21" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_MLT_CR</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS><xsl:call-template name="STEPID21" /></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Discount:Multi-Policy(Auto/Home)-->
						<STEP STEPID="22" CALC_ID="1022" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID22" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_MLT_POL</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS><xsl:call-template name="STEPID22" /></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Discount:Insurance Score Credit-->
						<STEP STEPID="23" CALC_ID="1023" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID23" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>
								<xsl:call-template name="INSURANCE_SCORE_COMPONENT_TYPE" />
							</COMPONENT_TYPE>
							<COMPONENT_CODE>D_INS_SCR</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS><xsl:call-template name="REMARKINSURANCE_SCORE_DISPLAY" /></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD>
								<xsl:call-template name="INSURANCESCORE_PERCENT" />
							</COM_EXT_AD>
						</STEP>
						<!-- Surcharge: ACCIDENT and VIOLATION -->
						<STEP STEPID="24" CALC_ID="1024" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID24" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>S</COMPONENT_TYPE>
							<COMPONENT_CODE>S_ACCI_VIOL</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS><xsl:call-template name="STEPID24" /></COMP_REMARKS>
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
	<!-- ######################################### FINAL PREMIUM ################# -->
	<xsl:template name="FINALPREMIUM">
		<xsl:choose>
			<!-- For Michigan -Commercial Automobile -->
			<xsl:when test="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name="FINALPREMIUM_COMMERCIAL_MICHIGAN" />
			</xsl:when>
			<!-- For Indiana -Commercial Automobile -->
			<xsl:when test="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name="FINALPREMIUM_COMMERCIAL_INDIANA" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- For Michigan -Commercial Automobile -->
	<xsl:template name="FINALPREMIUM_COMMERCIAL_MICHIGAN">
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
					<xsl:variable name="VAR_FINAL">
						<xsl:value-of select="$VAR_BI + $VAR_PD" />
					</xsl:variable>
					<xsl:value-of select="$VAR_FINAL" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:call-template name="CSLBIPD" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="PPI" />
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:call-template name="PIP" />
		</xsl:variable>
		<xsl:variable name="VAR4">
			<xsl:call-template name="MEDPAY" />
		</xsl:variable>
		<xsl:variable name="VAR5">
			<xsl:call-template name="MINITORT" />
		</xsl:variable>
		<xsl:variable name="VAR6">
			<xsl:call-template name="UM" />
		</xsl:variable>
		<xsl:variable name="VAR7">
			<xsl:choose>
				<xsl:when test="VEHICLES/VEHICLE/ISUNDERINSUREDMOTORISTS ='TRUE'">
					<xsl:call-template name="UIM" />
				</xsl:when>
				<xsl:otherwise>
				0.00
			</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR8">
			<xsl:call-template name="COMP" />
		</xsl:variable>
		<xsl:variable name="VAR9">
			<!--xsl:choose>
				<xsl:when test="COVGCOLLISIONTYPE ='LIMITED'">
					<xsl:call-template name="LTDCOLL" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:call-template name="COLL" />
				</xsl:otherwise>
			</xsl:choose-->
			<xsl:call-template name="COLL" />
		</xsl:variable>
		<xsl:variable name="VAR10">
			<xsl:call-template name="MCCAFEE" />
		</xsl:variable>
		<xsl:variable name="VAR11">
			<xsl:call-template name="EXTRAEQUIPMENTCOMP" />
		</xsl:variable>
		<xsl:variable name="VAR12">
			<xsl:call-template name="EXTRAEQUIPMENTCOLL" />
		</xsl:variable>
		<xsl:variable name="VAR13">
			<xsl:call-template name="UMPD" />
		</xsl:variable>
		<xsl:value-of select="round($VAR1 + $VAR2 + $VAR3 + $VAR4 + $VAR5 + $VAR6 + $VAR7 + $VAR8 + $VAR9 + $VAR10 + $VAR11 + $VAR12 + $VAR13)" />
	</xsl:template>
	<xsl:template name="FINALPREMIUM_COMMERCIAL_INDIANA">
		<xsl:call-template name="FINALPREMIUM_COMMERCIAL_MICHIGAN" />
	</xsl:template>
	<!-- ######################################### END OF FINAL PREMIUM ################# -->
	<!-- Start of BI AND PD -->
	<xsl:template name="CSLBIPD_MICIGAN">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE =$VT_TRAILER  or VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL='' or VEHICLES/VEHICLE/CSL='NO COVERAGE' or VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO' ">
			0.00
		</xsl:when>
			<xsl:otherwise>
				<!-- Multiply 
				1. Sum of Territory Base for BI and PD
				2. Insurance Score
				3. Driver Class Relativity
				4. CSL Rate for single car
				5. Intermediate Factor
				6. CSL Rate for multi-car -->
				<xsl:variable name="VAR1">
					<xsl:variable name="VAR_BI">
						<xsl:call-template name="TERRITORYBASE">
							<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
						</xsl:call-template>
					</xsl:variable>
					<xsl:variable name="VAR_PD">
						<xsl:call-template name="TERRITORYBASE">
							<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
						</xsl:call-template>
					</xsl:variable>
					<xsl:value-of select="$VAR_BI + $VAR_PD" />
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="INSURANCESCORE" />
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="CLASS">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="CSLRELATIVITY" />
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="INTERMEDIATEFACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:choose>
						<xsl:when test="MULTICARDISCOUNT='TRUE'">
							<xsl:call-template name="MULTICARDISCOUNTCSL" />
						</xsl:when>
						<xsl:otherwise>
							1.00
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<!--xsl:value-of select="round($VAR1 * $VAR2 * $VAR3 * $VAR4 * $VAR5 * $VAR6)" /-->
				<!--xsl:value-of select="round(round(round(round(round($VAR1 * $VAR2 )* $VAR3)* $VAR4)* $VAR5)* $VAR6)" /-->
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
				<xsl:value-of select="round($VARFIF5)" />
				
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="CSLBIPD_INDIANA">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE =$VT_TRAILER  or VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL='' or VEHICLES/VEHICLE/CSL='NO COVERAGE' or VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO'">
			0.00
		</xsl:when>
			<xsl:otherwise>
				<!-- Multiply 
				1. Sum of Territory Base for BI and PD
				3. Driver Class Relativity
				4. CSL Rate for single car
				5. Intermediate Factor
				6. CSL Rate for multi-car -->
				<xsl:variable name="VAR1">
					<xsl:variable name="VAR_BI">
						<xsl:call-template name="TERRITORYBASE">
							<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
						</xsl:call-template>
					</xsl:variable>
					<xsl:variable name="VAR_PD">
						<xsl:call-template name="TERRITORYBASE">
							<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
						</xsl:call-template>
					</xsl:variable>
					<xsl:value-of select="$VAR_BI + $VAR_PD" />
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="CLASS">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="CSLRELATIVITY" />
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="INTERMEDIATEFACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:choose>
						<xsl:when test="MULTICARDISCOUNT='TRUE'">
							<xsl:call-template name="MULTICARDISCOUNTCSL" />
						</xsl:when>
						<xsl:otherwise>
							1.00
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<!--xsl:value-of select="round($VAR1 * $VAR3 * $VAR4 * $VAR5 * $VAR6)" /-->
				<!--xsl:value-of select="round(round(round(round($VAR1 * $VAR3)*$VAR4)* $VAR5)* $VAR6)" /-->
				<xsl:variable name="FIRST_STEP" select="($VAR1)" />
				<xsl:variable name="VARFIS1" select="round(format-number($FIRST_STEP,'##.0000'))" />
				<xsl:variable name="SECOND_STEP" select="$VARFIS1 * $VAR3" />
				<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" />
				<xsl:variable name="THIRD_STEP" select="$VARSEC2 * $VAR4" />
				<xsl:variable name="VARTHI3" select="round(format-number($THIRD_STEP,'##.0000'))" />
				<xsl:variable name="FORTH_STEP" select="$VARTHI3 * $VAR5" />
				<xsl:variable name="VARFOR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
				<xsl:variable name="FIFTH_STEP" select="$VARFOR4 * $VAR6" />
				<xsl:variable name="VARFIF5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
				<xsl:value-of select="round($VARFIF5)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="CSLBIPD">
		<xsl:choose>
			<!-- For Michigan -Commercial Automobile -->
			<xsl:when test="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name="CSLBIPD_MICIGAN" />
			</xsl:when>
			<!-- For Indiana -Commercial Automobile -->
			<xsl:when test="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name="CSLBIPD_INDIANA" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- BodilyInjury -->
	<xsl:template name="BI">
		<xsl:choose>
			<!-- For Michigan -Commercial Automobile -->
			<xsl:when test="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name="BI_COMMERCIAL_MICHIGAN" />
			</xsl:when>
			<!-- For Indiana -Commercial Automobile -->
			<xsl:when test="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name="BI_COMMERCIAL_INDIANA" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Start of BI Commercial Michigan -->
	<xsl:template name="BI_COMMERCIAL_MICHIGAN">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE =$VT_TRAILER">0.00</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/BI ='NO COVERAGE' or VEHICLES/VEHICLE/BI ='' or VEHICLES/VEHICLE/BI ='0' or VEHICLES/VEHICLE/BI = '0/0' or VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO'">
			0.00
		</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL=''">
				<!-- Multiply 
				1. Territory Base Of BI
				2. A Constant factor  
				3. Insurance Score 
				4. Class
				5. Limit
				6. Intermediate Factor
				7. Surcharge-->
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="CONSTANT_FACTOR">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="INSURANCESCORE" />
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="CLASS">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="LIMIT">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:call-template name="INTERMEDIATEFACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR7">
					<xsl:call-template name="SURCHARGE" />
				</xsl:variable>
				<!--xsl:value-of select="round(round(round(round(round(round($VAR1 * $VAR2) *$VAR3) * $VAR4) * $VAR5) * $VAR6) * $VAR7)" /-->
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
				<xsl:value-of select="round($VARSIX6)" />
			</xsl:when>
			<xsl:otherwise>
			0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of BI Commercial Michigan -->
	<!-- Start of BI Commercial Indiana -->
	<xsl:template name="BI_COMMERCIAL_INDIANA">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE =$VT_TRAILER">0.00</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/BI ='NO COVERAGE' or VEHICLES/VEHICLE/BI ='' or VEHICLES/VEHICLE/BI ='0' or VEHICLES/VEHICLE/BI = '0/0' or VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO'">
			0.00
		</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL=''">
				<!-- Multiply 
				1. Territory Base Of BI
				2. A Constant factor  
			
				4. Class
				5. Limit
				6. Intermediate Factor
				7. Surcharge-->
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="CONSTANT_FACTOR">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="CLASS">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="LIMIT">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:call-template name="INTERMEDIATEFACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR7">
					<xsl:call-template name="SURCHARGE" />
				</xsl:variable>
				<!--xsl:value-of select="round(round(round(round(round($VAR1 * $VAR2) * $VAR4) * $VAR5) * $VAR6) * $VAR7)" /-->
				<xsl:variable name="FIRST_STEP" select="($VAR1*$VAR2)" />
				<xsl:variable name="VARFIS1" select="round(format-number($FIRST_STEP,'##.0000'))" />
				<xsl:variable name="SECOND_STEP" select="$VARFIS1" />
				<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" />
				<xsl:variable name="THIRD_STEP" select="$VARSEC2 * $VAR4" />
				<xsl:variable name="VARTHI3" select="round(format-number($THIRD_STEP,'##.0000'))" />
				<xsl:variable name="FORTH_STEP" select="$VARTHI3 * $VAR5" />
				<xsl:variable name="VARFOR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
				<xsl:variable name="FIFTH_STEP" select="$VARFOR4 * $VAR6" />
				<xsl:variable name="VARFIF5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
				<xsl:variable name="SIX_STEP" select="$VARFIF5 * $VAR7" />
				<xsl:variable name="VARSIX6" select="round(format-number($SIX_STEP,'##.0000'))" />
				<xsl:value-of select="round($VARSIX6)" />
			</xsl:when>
			<xsl:otherwise>
			0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of BI Commercial Indiana -->
	<!-- *********************** Start of Property Damage ************************************************************************-->
	<!-- Property Damage -->
	<xsl:template name="PD">
		<xsl:choose>
			<!-- For Michigan -Commercial Automobile -->
			<xsl:when test="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name="PD_COMMERCIAL_MICHIGAN" />
			</xsl:when>
			<!-- For Indiana -Commercial Automobile -->
			<xsl:when test="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name="PD_COMMERCIAL_INDIANA" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Start of PD Commercial Michigan -->
	<xsl:template name="PD_COMMERCIAL_MICHIGAN">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/PD = 'NO COVERAGE' or VEHICLES/VEHICLE/PD = '' or VEHICLES/VEHICLE/PD = '0' or VEHICLES/VEHICLE/VEHICLETYPE =$VT_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO'">
		0.00
	</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="CONSTANT_FACTOR">
						<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="INSURANCESCORE" />
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="CLASS">
						<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="LIMIT">
						<xsl:with-param name="FACTORELEMENT" select="'PD'" />
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:call-template name="INTERMEDIATEFACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR7">
					<xsl:call-template name="SURCHARGE" />
				</xsl:variable>
				<!--xsl:value-of select="round(round(round(round(round(round($VAR1 * $VAR2) * $VAR3) * $VAR4) * $VAR5) * $VAR6) * $VAR7)" /-->
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
				<xsl:value-of select="round($VARSIX6)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of Property Damage - Commercial Michigan -->
	<!-- Start of Property Damage - Commercial Indiana-->
	<xsl:template name="PD_COMMERCIAL_INDIANA">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/PD = 'NO COVERAGE' or VEHICLES/VEHICLE/PD = '' or VEHICLES/VEHICLE/PD = '0' or VEHICLES/VEHICLE/VEHICLETYPE =$VT_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO'">
		0.00
	</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="CLASS">
						<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="LIMIT">
						<xsl:with-param name="FACTORELEMENT" select="'PD'" />
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:call-template name="INTERMEDIATEFACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR7">
					<xsl:call-template name="SURCHARGE" />
				</xsl:variable>
				<!--xsl:value-of select="round(round(round(round($VAR1  * $VAR4) * $VAR5) * $VAR6) * $VAR7)" /-->
				<xsl:variable name="FIRST_STEP" select="($VAR1)" />
				<xsl:variable name="VARFIS1" select="round(format-number($FIRST_STEP,'##.0000'))" />
				<xsl:variable name="SECOND_STEP" select="$VARFIS1" />
				<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" />
				<xsl:variable name="THIRD_STEP" select="$VARSEC2 * $VAR4" />
				<xsl:variable name="VARTHI3" select="round(format-number($THIRD_STEP,'##.0000'))" />
				<xsl:variable name="FORTH_STEP" select="$VARTHI3 * $VAR5" />
				<xsl:variable name="VARFOR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
				<xsl:variable name="FIFTH_STEP" select="$VARFOR4 * $VAR6" />
				<xsl:variable name="VARFIF5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
				<xsl:variable name="SIX_STEP" select="$VARFIF5 * $VAR7" />
				<xsl:variable name="VARSIX6" select="round(format-number($SIX_STEP,'##.0000'))" />
				<xsl:value-of select="round($VARSIX6)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- **********************************  END OF PD ******************************************************************************  -->
	<!-- PPI -->
	<xsl:template name="PPI">
		<xsl:choose>
			<!-- For Michigan -Commercial Automobile -->
			<xsl:when test="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name="PPI_COMMERCIAL_MICHIGAN" />
			</xsl:when>
			<!-- For Indiana -Commercial Automobile -->
			<xsl:when test="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name="PPI_COMMERCIAL_INDIANA" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PPI_COMMERCIAL_INDIANA">0.00</xsl:template>
	<xsl:template name="PPI_COMMERCIAL_MICHIGAN">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE =$VT_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO' ">0.00</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'PPI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="CONSTANT_FACTOR">
						<xsl:with-param name="FACTORELEMENT" select="'PPI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="INSURANCESCORE" />
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="CLASS">
						<xsl:with-param name="FACTORELEMENT" select="'PPI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="INTERMEDIATEFACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:call-template name="SURCHARGE" />
				</xsl:variable>
				<!--xsl:value-of select="round(round(round(round(round($VAR1 * $VAR2) * $VAR3) * $VAR4) * $VAR5) * $VAR6)" /-->
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
				<xsl:value-of select="round($VARFIF5)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of PPI -->
	<!-- PIP -->
	<xsl:template name="PIP">
		<xsl:choose>
			<!-- For Michigan -Commercial Automobile -->
			<xsl:when test="POLICY/STATENAME ='MICHIGAN' ">
				<xsl:call-template name="PIP_COMMERCIAL_MICHIGAN" />
			</xsl:when>
			<!-- For Indiana -Commercial Automobile -->
			<xsl:when test="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name="PIP_COMMERCIAL_INDIANA" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- PIP Commercial Indiana -->
	<xsl:template name="PIP_COMMERCIAL_INDIANA">0.00</xsl:template>
	<!-- PIP Commercial Michigan -->
	<xsl:template name="PIP_COMMERCIAL_MICHIGAN">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE =$VT_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO'">0.00</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/MEDPM != '' and VEHICLES/VEHICLE/MEDPM != 'NO COVERAGE' ">
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'PIP'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="INSURANCESCORE" />
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="CLASS">
						<xsl:with-param name="FACTORELEMENT" select="'PIP'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="COVERAGE" />
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:call-template name="INTERMEDIATEFACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR7">
					<xsl:call-template name="SURCHARGE" />
				</xsl:variable>
				<xsl:variable name="VAR8">
					<xsl:call-template name="PIPDISCOUNT"/>
				</xsl:variable>
				<!--xsl:value-of select="round(round(round(round(round($VAR1 * $VAR3) * $VAR4) * $VAR5) * $VAR6) * $VAR7)" /-->
				<xsl:variable name="FIRST_STEP" select="($VAR1)" />
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
				<xsl:value-of select="round($VARSIX6)" />
			</xsl:when>
			<xsl:otherwise>
			0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of PIP -->
	<!-- Uninsured Motorists -->
	<xsl:template name="UM">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/TYPE ='NO COVERAGE' or VEHICLES/VEHICLE/VEHICLETYPE =$VT_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO'">
			0.00
		</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<!-- For Michigan -Commercial Automobile -->
					<xsl:when test="POLICY/STATENAME ='MICHIGAN' ">
						<xsl:call-template name="UM_COMMERCIAL_MICHIGAN" />
					</xsl:when>
					<!-- For Indiana -Commercial Automobile -->
					<xsl:when test="POLICY/STATENAME ='INDIANA'">
						<xsl:call-template name="UM_COMMERCIAL_INDIANA" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="UM_COMMERCIAL_MICHIGAN">
		<xsl:variable name="VAR1">
			<xsl:call-template name="TERRITORYBASE">
				<xsl:with-param name="FACTORELEMENT" select="'UM'"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="CONSTANT_FACTOR">
				<xsl:with-param name="FACTORELEMENT" select="'UM'"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:call-template name="LIMIT">
				<xsl:with-param name="FACTORELEMENT" select="'UM'" />
			</xsl:call-template>
		</xsl:variable>
		<xsl:value-of select="round(round($VAR1 * $VAR2) * $VAR3)" />
	</xsl:template>
	<xsl:template name="UM_COMMERCIAL_INDIANA">
		<!--  rule is same for UM in both state -->
		<xsl:variable name="VAR1">
			<xsl:call-template name="TERRITORYBASE">
				<xsl:with-param name="FACTORELEMENT" select="'UM'"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="LIMIT">
				<xsl:with-param name="FACTORELEMENT" select="'UM'" />
			</xsl:call-template>
		</xsl:variable>
		<xsl:value-of select="round($VAR1 * $VAR2)" />
	</xsl:template>
	<!--Start of UM Property Damage -->
	<xsl:template name="UMPD">
		<!--	1. get base 
			2. get limit value
			3. no deductible(if applies) -->
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='INDIANA'">
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/TYPE ='NO COVERAGE' or VEHICLES/VEHICLE/TYPE ='' or VEHICLES/VEHICLE/VEHICLETYPE =$VT_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO'"> <!-- No Coverage -->
					0.00
				    </xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/PDLIMIT !='' and VEHICLES/VEHICLE/PDLIMIT !='0'"> <!-- UMPD -->
						<xsl:variable name="PPDDEDUCTIBLE" select="VEHICLES/VEHICLE/PDDEDUCTIBLE" /> <!-- VEHICLES/VEHICLE/PDDEDUCTIBLE  change this later.. feb 22-->
						<!--xsl:variable name="PPDLIMIT" select="VEHICLES/VEHICLE/PDLIMIT" /-->
						<xsl:variable name="PPDLIMIT">
							<xsl:choose>
								<xsl:when test="contains(VEHICLES/VEHICLE/PDLIMIT,',')">
									<xsl:value-of select="VEHICLES/VEHICLE/PDLIMIT" />
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="format-number(VEHICLES/VEHICLE/PDLIMIT,'###,###')" />
								</xsl:otherwise>
							</xsl:choose>
						</xsl:variable>
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMPDLIMITS']/NODE[@ID='UMPD']/ATTRIBUTES[@LIMIT =$PPDLIMIT and @DEDUCTIBLE =$PPDDEDUCTIBLE]/@VALUE" />
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
	<!-- End of Uninsured Motorists-->
	<!-- Comprehensive -->
	<xsl:template name="COMP">
		<xsl:choose>
			<!-- For Michigan -Commercial Automobile -->
			<xsl:when test="POLICY/STATENAME ='MICHIGAN' ">
				<xsl:call-template name="COMP_COMMERCIAL_MICHIGAN" />
			</xsl:when>
			<!-- For Indiana -Commercial Automobile -->
			<xsl:when test="POLICY/STATENAME ='INDIANA' ">
				<xsl:call-template name="COMP_COMMERCIAL_INDIANA" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- BEGIN: COMP Commercial Michigan -->
	<xsl:template name="COMP_COMMERCIAL_MICHIGAN">
		<!-- Approach different for TRAILERS -->
		<xsl:choose>
			<xsl:when test="normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_TRAILER">
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE !='' and VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE &gt; 0">
						<!-- Get the comprehensive amount against cost,age and multiply by deductible factor -->
						<xsl:variable name="VAR_AGE" select="VEHICLES/VEHICLE/AGE" />
						<xsl:variable name="VAR_COST" select="VEHICLES/VEHICLE/COST" />
						<xsl:variable name="VAR_DEDUCTIBLE" select="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE" />
						<!-- Fetch the base amount for the resp age category -->
						<xsl:variable name="VAR_COMM_BASE_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMVATTACHENDT']/NODE[@ID='COMPREHENSIVE_BASE_VALUE']/ATTRIBUTES[@AGE_MIN &lt;= $VAR_AGE and @AGE_MAX &gt;= $VAR_AGE]/@BASE_VALUE" />
						<!-- Fetch the max limit present -->
						<xsl:variable name="VAR_MAX_COST_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMVATTACHENDT']/NODE[@ID='COMPREHENSIVE']/@MAX_COST" />
						<!-- Fetch the deductible factor -->
						<xsl:variable name="VAR_DEDUCTIBE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMVATTACHENDT']/NODE[@ID='COMPREHENSIVE_DED_FACTOR']/ATTRIBUTES[@DEDUCTIBLE = $VAR_DEDUCTIBLE]/@FACTOR" />
						<!-- If the cost exceeds this max limit then -->
						<xsl:choose>
							<xsl:when test="$VAR_COST &gt;= $VAR_MAX_COST_VALUE">
								<!-- Fetch the factor against max limit-->
								<xsl:variable name="VAR_MAX_COST_VALUE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMVATTACHENDT']/NODE[@ID='COMPREHENSIVE']/ATTRIBUTES[@COST_MAX = $VAR_MAX_COST_VALUE]/@COMP_RATE_FACTOR" />
								<!-- Fetch the differencr in amount -->
								<xsl:variable name="VAR_DIFFERENCE" select="$VAR_COST - $VAR_MAX_COST_VALUE" />
								<!-- Fetch the each additional amount -->
								<xsl:variable name="VAR_FOR_EACH_ADDTN_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMVATTACHENDT']/NODE[@ID='COMPREHENSIVE']/ATTRIBUTES/@COST_ADDITIONAL" />
								<!-- Fetch the factor value for each additional amount -->
								<xsl:variable name="VAR_ADDTN_VALUE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMVATTACHENDT']/NODE[@ID='COMPREHENSIVE']/ATTRIBUTES/@COMP_RATE_ADDTNL_FACTOR" />
								<!-- Prepare the formula e.g base value * ( 4.03+((32000-30000)/1000)*0.16)-->
								<xsl:value-of select="round(round($VAR_COMM_BASE_VALUE * ($VAR_MAX_COST_VALUE_FACTOR + round(round($VAR_DIFFERENCE div $VAR_FOR_EACH_ADDTN_VALUE) * $VAR_ADDTN_VALUE_FACTOR))) * $VAR_DEDUCTIBE_FACTOR)" />
							</xsl:when>
							<xsl:otherwise> <!-- Otherwise if the cost is below the max limit -->
								<!-- Fetch the resp factor -->
								<xsl:variable name="VAR_COMM_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMVATTACHENDT']/NODE[@ID='COMPREHENSIVE']/ATTRIBUTES[@COST_MIN &lt;= $VAR_COST and @COST_MAX &gt;= $VAR_COST]/@COMP_RATE_FACTOR" />
								<!-- Multiply factor with  base value -->
								<xsl:value-of select="round(round($VAR_COMM_BASE_VALUE * $VAR_COMM_FACTOR) * $VAR_DEDUCTIBE_FACTOR)" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE !='' and VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE &gt; 0">
						<xsl:variable name="VAR1">
							<xsl:call-template name="TERRITORYBASE">
								<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:call-template name="CONSTANT_FACTOR">
								<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR3">
							<xsl:call-template name="INSURANCESCORE" />
						</xsl:variable>
						<xsl:variable name="VAR4">
							<xsl:call-template name="SYMBOL">
								<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR5">
							<xsl:call-template name="AGE">
								<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR6">
							<xsl:call-template name="DEDUCTIBLE">
								<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR7">
							<xsl:call-template name="INTERMEDIATEFACTOR" />
						</xsl:variable>
						<!--xsl:value-of select="round(round(round(round(round(round($VAR1 * $VAR2) * $VAR3) * $VAR4) * $VAR5) * $VAR6) * $VAR7)" /-->
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
						<xsl:value-of select="round($VARSIX6)" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- intermediate factor for comperhensive -->
	<xsl:template name="INTERMEDIATEFACTOR_COMP">
		<xsl:variable name="PMILESEACHWAY" select="VEHICLES/VEHICLE/MILESEACHWAY" />
		<xsl:variable name="PINTERMEDIATEFACTOR" select="VEHICLES/VEHICLE/VEHICLERATINGCODE" />
		<xsl:choose>
			<xsl:when test="'$PINTERMEDIATEFACTOR' ='L'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMVATTACHENDT']/NODE[@ID='CUF']/ATTRIBUTES[@DESC = 'LOCAL']/@FACTOR" />
			</xsl:when>
			<xsl:when test="'$PINTERMEDIATEFACTOR' ='I'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMVATTACHENDT']/NODE[@ID='CUF']/ATTRIBUTES[@DESC = 'INTERMEDIATE']/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- intermediate factor for collision -->
	<xsl:template name="INTERMEDIATEFACTOR_COLL">
		<xsl:variable name="PMILESEACHWAY" select="VEHICLES/VEHICLE/MILESEACHWAY" />
		<xsl:variable name="PINTERMEDIATEFACTOR" select="VEHICLES/VEHICLE/VEHICLERATINGCODE" />
		<xsl:choose>
			<xsl:when test="'$PINTERMEDIATEFACTOR' ='L'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLVATTACHENDT']/NODE[@ID='COLLUF']/ATTRIBUTES[@DESC = 'LOCAL']/@FACTOR" />
			</xsl:when>
			<xsl:when test="'$PINTERMEDIATEFACTOR' ='I'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLVATTACHENDT']/NODE[@ID='COLLUF']/ATTRIBUTES[@DESC = 'INTERMEDIATE']/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- END: COMP Commercial Michigan -->
	<!-- BEGIN: COMP Commercial Indiana -->
	<xsl:template name="COMP_COMMERCIAL_INDIANA">
		<!-- Approach different for Travel Campers -->
		<xsl:choose>
			<xsl:when test="normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_TRAILER">
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE !='' and VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE &gt; 0">
						<!-- Get the comprehensive amount against cost,age and multiply by deductible factor -->
						<xsl:variable name="VAR_AGE" select="VEHICLES/VEHICLE/AGE" />
						<xsl:variable name="VAR_COST" select="VEHICLES/VEHICLE/COST" />
						<xsl:variable name="VAR_DEDUCTIBLE" select="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE" />
						<!-- Fetch the base amount for the resp age category -->
						<xsl:variable name="VAR_COMM_BASE_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMVATTACHENDT']/NODE[@ID='COMPREHENSIVE_BASE_VALUE']/ATTRIBUTES[@AGE_MIN &lt;= $VAR_AGE and @AGE_MAX &gt;= $VAR_AGE]/@BASE_VALUE" />
						<!-- Fetch the max limit present -->
						<xsl:variable name="VAR_MAX_COST_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMVATTACHENDT']/NODE[@ID='COMPREHENSIVE']/@MAX_COST" />
						<!-- Fetch the deductible factor -->
						<xsl:variable name="VAR_DEDUCTIBE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMVATTACHENDT']/NODE[@ID='COMPREHENSIVE_DED_FACTOR']/ATTRIBUTES[@DEDUCTIBLE = $VAR_DEDUCTIBLE]/@FACTOR" />
						<!-- If the cost exceeds this max limit then -->
						<xsl:choose>
							<xsl:when test="$VAR_COST &gt;= $VAR_MAX_COST_VALUE">
								<!-- Fetch the factor against max limit-->
								<xsl:variable name="VAR_MAX_COST_VALUE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMVATTACHENDT']/NODE[@ID='COMPREHENSIVE']/ATTRIBUTES[@COST_MAX = $VAR_MAX_COST_VALUE]/@COMP_RATE_FACTOR" />
								<!-- Fetch the differencr in amount -->
								<xsl:variable name="VAR_DIFFERENCE" select="$VAR_COST - $VAR_MAX_COST_VALUE" />
								<!-- Fetch the each additional amount -->
								<xsl:variable name="VAR_FOR_EACH_ADDTN_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMVATTACHENDT']/NODE[@ID='COMPREHENSIVE']/ATTRIBUTES/@COST_ADDITIONAL" />
								<!-- Fetch the factor value for each additional amount -->
								<xsl:variable name="VAR_ADDTN_VALUE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMVATTACHENDT']/NODE[@ID='COMPREHENSIVE']/ATTRIBUTES/@COMP_RATE_ADDTNL_FACTOR" />
								<!-- Prepare the formula e.g base value * ( 4.03+((32000-30000)/1000)*0.16)-->
								<xsl:value-of select="round(round($VAR_COMM_BASE_VALUE * ($VAR_MAX_COST_VALUE_FACTOR + round(round($VAR_DIFFERENCE div $VAR_FOR_EACH_ADDTN_VALUE) * $VAR_ADDTN_VALUE_FACTOR)))*$VAR_DEDUCTIBE_FACTOR)" />
							</xsl:when>
							<xsl:otherwise> <!-- Otherwise if the cost is below the max limit -->
								<!-- Fetch the resp factor -->
								<xsl:variable name="VAR_COMM_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMVATTACHENDT']/NODE[@ID='COMPREHENSIVE']/ATTRIBUTES[@COST_MIN &lt;= $VAR_COST and @COST_MAX &gt;= $VAR_COST]/@COMP_RATE_FACTOR" />
								<!-- Multiply factor with  base value -->
								<xsl:value-of select="round(round($VAR_COMM_BASE_VALUE * $VAR_COMM_FACTOR )* $VAR_DEDUCTIBE_FACTOR)" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE !='' and VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE &gt; 0">
						<xsl:variable name="VAR1">
							<xsl:call-template name="TERRITORYBASE">
								<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:call-template name="SYMBOL">
								<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR3">
							<xsl:call-template name="DEDUCTIBLE">
								<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR4">
							<xsl:call-template name="AGE">
								<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR5">
							<xsl:call-template name="SURCHARGE" />
						</xsl:variable>
						<xsl:variable name="VAR6">
							<xsl:call-template name="INTERMEDIATEFACTOR_COMP" />
						</xsl:variable>
						<!--xsl:value-of select="round(round(round(round(round($VAR1*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)" /-->
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
						<xsl:value-of select="round($VARFIF5)" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- END: COMP Commercial Indiana -->
	<!-- End of Comprehensive  -->
	<!-- Collision -->
	<xsl:template name="COLL">
		<xsl:choose>
			<!-- For Michigan -Commercial Automobile -->
			<xsl:when test="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name="COLL_COMMERCIAL_MICHIGAN" />
			</xsl:when>
			<!-- For Indiana -Commercial Automobile -->
			<xsl:when test="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name="COLL_COMMERCIAL_INDIANA" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Start Michigan -Commercial Automobile -->
	<xsl:template name="COLL_COMMERCIAL_MICHIGAN">
		<xsl:choose>
			<xsl:when test="normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_TRAILER">
				<xsl:choose>
					<xsl:when test=" (VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE !='' and VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE &gt; 0)">
						<!-- Get the coll amount against cost,age and multiply by deductible factor -->
						<xsl:variable name="VAR_AGE" select="VEHICLES/VEHICLE/AGE" />
						<xsl:variable name="VAR_COST" select="VEHICLES/VEHICLE/COST" />
						<!-- Fetch the base amount for the resp age category -->
						<xsl:variable name="VAR_COMM_BASE_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLVATTACHENDT']/NODE[@ID='COLLISION_BASE_VALUE']/ATTRIBUTES[@AGE_MIN &lt;= $VAR_AGE and @AGE_MAX &gt;= $VAR_AGE]/@BASE_VALUE" />
						<!-- Fetch the max limit present -->
						<xsl:variable name="VAR_MAX_COST_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLVATTACHENDT']/NODE[@ID='COLLISION']/@MAX_COST" />
						<xsl:variable name="VAR_DEDUCTIBLE" select="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE" />
						<xsl:variable name="VAR_DEDUCTIBLE_TYPE" select="VEHICLES/VEHICLE/COVGCOLLISIONTYPE" />
						<!-- Get the deductible factor 	-->
						<xsl:variable name="VAR_DEDUCTIBLE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLVATTACHENDT']/NODE[@ID='COLLISION_DED_FACTOR']/ATTRIBUTES[@DEDUCTIBLE=normalize-space($VAR_DEDUCTIBLE) and @COLLISIONTYPE=normalize-space($VAR_DEDUCTIBLE_TYPE)]/@RELATIVITY" />
						<!-- If the cost exceeds this max limit then -->
						<xsl:choose>
							<xsl:when test="$VAR_COST &gt;= $VAR_MAX_COST_VALUE">
								<!-- Fetch the factor against max limit -->
								<xsl:variable name="VAR_MAX_COST_VALUE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLVATTACHENDT']/NODE[@ID='COLLISION']/ATTRIBUTES[@COST_MAX = $VAR_MAX_COST_VALUE]/@COMP_RATE_FACTOR" />
								<!-- Fetch the differencr in amount -->
								<xsl:variable name="VAR_DIFFERENCE" select="$VAR_COST - $VAR_MAX_COST_VALUE" />
								<!-- Fetch the each additional amount -->
								<xsl:variable name="VAR_FOR_EACH_ADDTN_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLVATTACHENDT']/NODE[@ID='COLLISION']/ATTRIBUTES/@COST_ADDITIONAL" />
								<!-- Fetch the factor value for each additional amount -->
								<xsl:variable name="VAR_ADDTN_VALUE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLVATTACHENDT']/NODE[@ID='COLLISION']/ATTRIBUTES/@COMP_RATE_ADDTNL_FACTOR" />
								<!-- Prepare the formula e.g base value * ( 4.03+((32000-30000)/1000)*0.16)-->
								<xsl:value-of select="round(round($VAR_COMM_BASE_VALUE * ($VAR_MAX_COST_VALUE_FACTOR + (($VAR_DIFFERENCE div $VAR_FOR_EACH_ADDTN_VALUE) * $VAR_ADDTN_VALUE_FACTOR)))*$VAR_DEDUCTIBLE_FACTOR)" />
							</xsl:when>
							<xsl:otherwise> <!-- Otherwise if the cost is below the max limit -->
								<!-- Fetch the resp factor -->
								<xsl:variable name="VAR_COMM_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLVATTACHENDT']/NODE[@ID='COLLISION']/ATTRIBUTES[@COST_MIN &lt;= $VAR_COST and @COST_MAX &gt;= $VAR_COST]/@COMP_RATE_FACTOR" />
								<!-- Multiply factor with  base value -->
								<xsl:value-of select="round(round($VAR_COMM_BASE_VALUE * $VAR_COMM_FACTOR)*$VAR_DEDUCTIBLE_FACTOR)" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="(VEHICLES/VEHICLE/COVGCOLLISIONTYPE ='LIMITED')">
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="CONSTANT_FACTOR">
						<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="INSURANCESCORE" />
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="SYMBOL">
						<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="AGE">
						<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:call-template name="DEDUCTIBLE">
						<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR7">
					<xsl:call-template name="INTERMEDIATEFACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR8">
					<xsl:call-template name="SURCHARGE" />
				</xsl:variable>
				<!--xsl:value-of select="round(round(round(round(round(round(round($VAR1 * $VAR2) * $VAR3) * $VAR4) * $VAR5) * $VAR6) * $VAR7) * $VAR8)" /-->
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
				<xsl:variable name="VARSEVN7" select="round(format-number($SEVEN_STEP,'##.0000'))" />
				<xsl:value-of select="round($VARSEVN7)" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test=" (VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE !='' and VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE &gt; 0 and VEHICLES/VEHICLE/VEHICLETYPE_SCO != 'SCO')">
						<xsl:variable name="VAR1">
							<xsl:call-template name="TERRITORYBASE">
								<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:call-template name="CONSTANT_FACTOR">
								<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR3">
							<xsl:call-template name="INSURANCESCORE" />
						</xsl:variable>
						<xsl:variable name="VAR4">
							<xsl:call-template name="SYMBOL">
								<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR5">
							<xsl:call-template name="AGE">
								<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR6">
							<xsl:call-template name="DEDUCTIBLE">
								<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR7">
							<xsl:call-template name="INTERMEDIATEFACTOR" />
						</xsl:variable>
						<xsl:variable name="VAR8">
							<xsl:call-template name="SURCHARGE" />
						</xsl:variable>
						<!--xsl:value-of select="round(round(round(round(round(round(round($VAR1 * $VAR2) * $VAR3) * $VAR4) * $VAR5) * $VAR6) * $VAR7) * $VAR8)" /-->
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
						<xsl:variable name="VARSEVN7" select="round(format-number($SEVEN_STEP,'##.0000'))" />
						<xsl:value-of select="round($VARSEVN7)" />
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End Michigan -Commercial Automobile -->
	<!-- Start Indiana -Commercial Automobile -->
	<xsl:template name="COLL_COMMERCIAL_INDIANA">
		<xsl:choose>
			<xsl:when test="normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_TRAILER">
				<xsl:choose>
					<xsl:when test=" (VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE !='' and VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE &gt; 0)">
						<!-- Get the coll amount against cost,age and multiply by deductible factor -->
						<xsl:variable name="VAR_AGE" select="VEHICLES/VEHICLE/AGE" />
						<xsl:variable name="VAR_COST" select="VEHICLES/VEHICLE/COST" />
						<!-- Fetch the base amount for the resp age category -->
						<xsl:variable name="VAR_COMM_BASE_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLVATTACHENDT']/NODE[@ID='COLLISION_BASE_VALUE']/ATTRIBUTES[@AGE_MIN &lt;= $VAR_AGE and @AGE_MAX &gt;= $VAR_AGE]/@BASE_VALUE" />
						<!-- Fetch the max limit present -->
						<xsl:variable name="VAR_MAX_COST_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLVATTACHENDT']/NODE[@ID='COLLISION']/@MAX_COST" />
						<xsl:variable name="VAR_DEDUCTIBLE" select="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE" />
						<!-- Get the deductible factor 	-->
						<xsl:variable name="VAR_DEDUCTIBLE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLVATTACHENDT']/NODE[@ID='COLLISION_DED_FACTOR']/ATTRIBUTES[@DEDUCTIBLE=normalize-space($VAR_DEDUCTIBLE)]/@FACTOR" />
						<!-- If the cost exceeds this max limit then -->
						<xsl:choose>
							<xsl:when test="$VAR_COST &gt;= $VAR_MAX_COST_VALUE">
								<!-- Fetch the factor against max limit -->
								<xsl:variable name="VAR_MAX_COST_VALUE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLVATTACHENDT']/NODE[@ID='COLLISION']/ATTRIBUTES[@COST_MAX = $VAR_MAX_COST_VALUE]/@COMP_RATE_FACTOR" />
								<!-- Fetch the differencr in amount -->
								<xsl:variable name="VAR_DIFFERENCE" select="$VAR_COST - $VAR_MAX_COST_VALUE" />
								<!-- Fetch the each additional amount -->
								<xsl:variable name="VAR_FOR_EACH_ADDTN_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLVATTACHENDT']/NODE[@ID='COLLISION']/ATTRIBUTES/@COST_ADDITIONAL" />
								<!-- Fetch the factor value for each additional amount -->
								<xsl:variable name="VAR_ADDTN_VALUE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLVATTACHENDT']/NODE[@ID='COLLISION']/ATTRIBUTES/@COMP_RATE_ADDTNL_FACTOR" />
								<!-- Prepare the formula e.g base value * ( 4.03+((32000-30000)/1000)*0.16)-->
								<xsl:value-of select="round(round($VAR_COMM_BASE_VALUE * ($VAR_MAX_COST_VALUE_FACTOR + (($VAR_DIFFERENCE div $VAR_FOR_EACH_ADDTN_VALUE) * $VAR_ADDTN_VALUE_FACTOR)))*$VAR_DEDUCTIBLE_FACTOR)" />
							</xsl:when>
							<xsl:otherwise> <!-- Otherwise if the cost is below the max limit -->
								<!-- Fetch the resp factor -->
								<xsl:variable name="VAR_COMM_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLVATTACHENDT']/NODE[@ID='COLLISION']/ATTRIBUTES[@COST_MIN &lt;= $VAR_COST and @COST_MAX &gt;= $VAR_COST]/@COMP_RATE_FACTOR" />
								<!-- Multiply factor with  base value -->
								<xsl:value-of select="round(round($VAR_COMM_BASE_VALUE * $VAR_COMM_FACTOR)*$VAR_DEDUCTIBLE_FACTOR)" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="(VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE !='' and VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE &gt; 0 and VEHICLES/VEHICLE/VEHICLETYPE_SCO != 'SCO')">
						<xsl:variable name="VAR1">
							<xsl:call-template name="TERRITORYBASE">
								<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:call-template name="SYMBOL">
								<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR3">
							<xsl:call-template name="DEDUCTIBLE">
								<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR4">
							<xsl:call-template name="AGE">
								<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR5">
							<xsl:call-template name="SURCHARGE" />
						</xsl:variable>
						<xsl:variable name="VAR6">
							<xsl:call-template name="INTERMEDIATEFACTOR_COLL" />
						</xsl:variable>
						<!--xsl:value-of select="round(round(round(round(round($VAR1*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)" /-->
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
						<xsl:value-of select="round($VARFIF5)" />
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End Indiana - Commercial Automobile -->
	<!-- End of Collision  -->
	<!-- Limited Collision -->
	<xsl:template name="LTDCOLL">
		<xsl:variable name="VAR1">
			<xsl:call-template name="TERRITORYBASE">
				<xsl:with-param name="FACTORELEMENT" select="'LTDCOLL'"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="INSURANCESCORE" />
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:call-template name="OPTIMAL" />
		</xsl:variable>
		<xsl:variable name="VAR4">
			<xsl:call-template name="TYPEOFVEHICLE">
				<xsl:with-param name="FACTORELEMENT" select="'LTDCOLL'"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR5">
			<xsl:call-template name="CLASS">
				<xsl:with-param name="FACTORELEMENT" select="'LTDCOLL'"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR6">
			<xsl:call-template name="SYMBOL">
				<xsl:with-param name="FACTORELEMENT" select="'LTDCOLL'"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR7">
			<xsl:call-template name="LIMITFACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR8">
			<xsl:call-template name="AGE">
				<xsl:with-param name="FACTORELEMENT" select="'LTDCOLL'"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR9">
			<xsl:call-template name="ABS" />
		</xsl:variable>
		<xsl:variable name="VAR10">
			<xsl:call-template name="MULTIVEHICLE">
				<xsl:with-param name="FACTORELEMENT" select="'LTDCOLL'"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR11">
			<xsl:call-template name="VEHICLEUSE" />
		</xsl:variable>
		<xsl:variable name="VAR12">
			<xsl:call-template name="DRIVERDISCOUNT" />
		</xsl:variable>
		<!--xsl:variable name="VAR13">
			<xsl:call-template name="GOODSTUDENT" />
		</xsl:variable-->
		<xsl:variable name="VAR14">
			<xsl:call-template name="SURCHARGE" />
		</xsl:variable>
		<xsl:variable name="VAR15">
			<xsl:call-template name="MULTIPOLICY" />
		</xsl:variable>
		<xsl:value-of select="round($VAR1*$VAR2*$VAR3*$VAR4*$VAR5*$VAR6*$VAR7*$VAR8*$VAR9*$VAR10*$VAR11*$VAR12*$VAR14*$VAR15)" />
	</xsl:template>
	<!-- End of Limited Collision -->
	<!-- Mini-Tort -->
	<xsl:template name="MINITORT">
		<xsl:choose>
			<!-- For Michigan -Commercial Automobile -->
			<xsl:when test="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name="MINITORT_COMMERCIAL_MICHIGAN" />
			</xsl:when>
			<!-- For Indiana -Commercial Automobile -->
			<xsl:when test="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name="MINITORT_COMMERCIAL_INDIANA" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MINITORT_COMMERCIAL_INDIANA">
		0.00
	</xsl:template>
	<xsl:template name="MINITORT_COMMERCIAL_MICHIGAN">
		<xsl:variable name="VAR1">
			<xsl:choose>
				<xsl:when test="VEHICLES/VEHICLE/MINITORTPDLIAB = 'TRUE' and VEHICLES/VEHICLE/VEHICLETYPE_SCO != 'SCO'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINITORT']/NODE[@ID='MINITORTRATE']/ATTRIBUTES/@RATE_PER_VEHICLE" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR2">1.00</xsl:variable>
		<xsl:value-of select="$VAR1 * $VAR2" />
	</xsl:template>
	<xsl:template name="MINITORTLIMIT">
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME='MICHIGAN' and VEHICLES/VEHICLE/MINITORTPDLIAB = 'TRUE' and VEHICLES/VEHICLE/VEHICLETYPE_SCO != 'SCO'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINITORT']/NODE[@ID='MINITORTRATE']/ATTRIBUTES/@LIMIT" />
			</xsl:when>
			<xsl:otherwise>
			0.00		
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of Mini-Tort-->
	<!-- MCCA Fee-->
	<xsl:template name="MCCAFEE">
		<xsl:variable name="VAR_QUOTEEFFECTIVEDATE" select="POLICY/QUOTEEFFDATE" />
		<xsl:variable name="P_YEAR" select="substring($VAR_QUOTEEFFECTIVEDATE,7,4)" />
		<xsl:variable name="P_MONTH" select="substring($VAR_QUOTEEFFECTIVEDATE,1,2)" />
		<xsl:variable name="P_DAY" select="substring($VAR_QUOTEEFFECTIVEDATE,4,5)" />
		<xsl:variable name="VAR_MCCAFEEEFFECTIVEDATENEW" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MCCAFEE']/NODE[@ID='MCCAFEESEMIANNUAL_NEW']/ATTRIBUTES/@RATE_EFFECTIVEDATE" />
		<xsl:variable name="P_YEAR_MCCAFEEEFFECTIVEDATENEW" select="substring($VAR_MCCAFEEEFFECTIVEDATENEW,7,4)" />
		<xsl:variable name="P_MONTH_MCCAFEEEFFECTIVEDATENEW" select="substring($VAR_MCCAFEEEFFECTIVEDATENEW,1,2)" />
		<xsl:variable name="P_DAY_MCCAFEEEFFECTIVEDATENEW" select="substring($VAR_MCCAFEEEFFECTIVEDATENEW,4,5)" />
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE =$VT_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO'">0.00</xsl:when>
			<xsl:when test="POLICY/STATENAME ='MICHIGAN' and  VEHICLES/VEHICLE/MEDPM != '' ">
				<xsl:choose>
					<xsl:when test="$P_YEAR &lt;= $P_YEAR_MCCAFEEEFFECTIVEDATENEW and $P_MONTH &lt; $P_MONTH_MCCAFEEEFFECTIVEDATENEW and $P_DAY &lt; $P_DAY_MCCAFEEEFFECTIVEDATENEW">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MCCAFEE']/NODE[@ID='MCCAFEESEMIANNUALOLD']/ATTRIBUTES/@RATE_PER_VEHICLE" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MCCAFEE']/NODE[@ID='MCCAFEESEMIANNUALNEW']/ATTRIBUTES/@RATE_PER_VEHICLE" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of MCCA Fee-->
	<!-- Age -->
	<xsl:template name="AGE">
		<xsl:param name="FACTORELEMENT" />
		<xsl:variable name="VAR_AGE" select="VEHICLES/VEHICLE/AGE"></xsl:variable>
		<xsl:variable name="VAR_MAXAGE">
			<xsl:choose>
				<xsl:when test="$FACTORELEMENT ='COMP'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEAGEGROUPOTHER']/NODE[@ID='VEHICLEAGEGROUPCOMP']/@MAXAGE" />
				</xsl:when>
				<xsl:when test="$FACTORELEMENT ='COLL'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEAGEGROUPCOLLISION']/NODE[@ID='VEHICLEAGEGROUPCOLL']/@MAXAGE" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEAGEGROUPOTHER']/NODE[@ID='VEHICLEAGEGROUPCOMP']/@MAXAGE" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="PAGE">
			<xsl:choose>
				<xsl:when test="$VAR_AGE &gt;	$VAR_MAXAGE	">
					<xsl:value-of select="$VAR_MAXAGE" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$VAR_AGE" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PAGE &gt; 0 ">
				<xsl:choose>
					<xsl:when test="$FACTORELEMENT ='COMP'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEAGEGROUPOTHER']/NODE[@ID='VEHICLEAGEGROUPCOMP']/ATTRIBUTES[@AGE = $PAGE ]/@RELATIVITY" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT ='COLL'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEAGEGROUPCOLLISION']/NODE[@ID='VEHICLEAGEGROUPCOLL']/ATTRIBUTES[@AGEGROUP = $PAGE ]/@FACTOR" />
					</xsl:when>
					<xsl:otherwise>
						1.00
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Work Loss Waiver -->
	<xsl:template name="WAIVEWORKLOSS">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/WAIVEWORKLOSS = 'TRUE'">
				1.00	
		</xsl:when>
			<xsl:otherwise>
			0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="WAIVEWORKLOSS_DISPLAY">
		0.00
		<!--xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/WAIVEWORKLOSS = 'TRUE'">
			Included	
		</xsl:when>
			<xsl:otherwise>
			0
		</xsl:otherwise>
		</xsl:choose-->
	</xsl:template>
	<!-- *******************************************************************************************************-->
	<!-- ************** Other Factors **************************************************************************-->
	<!-- *******************************************************************************************************-->
	<!-- Territory Base -->
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
			<xsl:when test="normalize-space(POLICY/STATENAME)='INDIANA'">
				<xsl:call-template name="TERRITORYBASE_INDIANA">
					<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
				</xsl:call-template>
			</xsl:when>
			<xsl:otherwise>10</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Territory base michigan -->
	<xsl:template name="TERRITORYBASE_MICHIGAN">
		<xsl:param name="FACTOR" /> <!-- Depending on the factor element BI/PD..etc -->
		<xsl:variable name="PTERRITORYCODE" select="VEHICLES/VEHICLE/TERRCODEGARAGEDLOCATION" />
		<xsl:choose>
			<xsl:when test="$PTERRITORYCODE =''">
			1.00
		</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="$FACTOR = 'BI'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE = $PTERRITORYCODE]/@BIBASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'PD'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE= $PTERRITORYCODE ]/@PDBASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'PPI'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE= $PTERRITORYCODE ]/@PPIBASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'PIP'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE= $PTERRITORYCODE ]/@PIPBASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'UM'"> 
				1.00<!--xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMBILIMITS']/NODE[@ID='UMBI']/ATTRIBUTES[@TERRITORYCODE= $PTERRITORYCODE ]/@PIPBASE" /-->
			</xsl:when>
					<xsl:when test="$FACTOR = 'UIM'"> 
					1.00	
			</xsl:when>
					<xsl:when test="$FACTOR = 'COMP'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPREHENSIVETERRITORYBASE']/NODE[@ID ='COMPREHENSIVETERRITORY']/ATTRIBUTES[@TERRITORY = $PTERRITORYCODE ]/@BASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'COLL'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLISIONTERRITORYBASE']/NODE[@ID ='COLLISIONTERRITORY']/ATTRIBUTES[@TERRITORY = $PTERRITORYCODE ]/@BASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'LTDCOLL'"> 
				1.00	 
			</xsl:when>
					<xsl:otherwise> 1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of Territory base michigan -->
	<!-- Territory base Indiana -->
	<xsl:template name="TERRITORYBASE_INDIANA">
		<xsl:param name="FACTOR" /> <!-- Depending on the factor element BI/PD..etc -->
		<xsl:variable name="PTERRITORYCODE" select="VEHICLES/VEHICLE/TERRCODEGARAGEDLOCATION" />
		<xsl:choose>
			<xsl:when test="$PTERRITORYCODE =''">
			1.00
		</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="$FACTOR = 'BI'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE = $PTERRITORYCODE]/@BIBASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'PD'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE= $PTERRITORYCODE ]/@PDBASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'MEDPAY' and POLICY/STATENAME = 'INDIANA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE= $PTERRITORYCODE ]/@MEDPAYBASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'UM'"> 
						1.00
					</xsl:when>
					<xsl:when test="$FACTOR = 'UIM'"> 
							1.00
						</xsl:when>
					<xsl:when test="$FACTOR = 'COMP'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPREHENSIVETERRITORYBASE']/NODE[@ID ='COMPREHENSIVETERRITORY']/ATTRIBUTES[@TERRITORY = $PTERRITORYCODE ]/@BASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'COLL'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLISIONTERRITORYBASE']/NODE[@ID ='COLLISIONTERRITORY']/ATTRIBUTES[@TERRITORY = $PTERRITORYCODE ]/@BASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'LTDCOLL'"> 
							1.00	
				</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of Territory base Indiana -->
	<!-- End of Territory Base -->
	<!-- Insurance Score -->
	<xsl:template name="INSURANCESCORE">
		<xsl:variable name="INS1" select="POLICY/INSURANCESCORE"></xsl:variable>
		<xsl:variable name="INS" select="translate(translate($INS1,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')" />
		<xsl:variable name="VAR_MAX_SCORE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORE']/NODE[@ID ='IS']/ATTRIBUTES[@MAX = 'MAX_SCORE']/@MINSCORE" />
		<xsl:choose>
			<xsl:when test="$INS = 0 or $INS = '' or VEHICLES/VEHICLE/VEHICLETYPE =$VT_TRAILER or POLICY/STATENAME='INDIANA'">
			1.00 
		</xsl:when>
			<xsl:when test="normalize-space($INS1) = 'NOHITNOSCORE'">
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
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE =$VT_TRAILER  or POLICY/STATENAME='INDIANA'">0.00</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="INS1" select="POLICY/INSURANCESCORE"></xsl:variable>
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
						<xsl:when test="$INS = ''">1.00</xsl:when>
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
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$INSDISCOUNT = 0 or $INSDISCOUNT='' or $INSDISCOUNT=1"><xsl:text>0</xsl:text></xsl:when>
					<xsl:when test="$INSDISCOUNT &gt; 0"><xsl:text>Included</xsl:text></xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Insurance score in percentage -->
	<xsl:template name="INSURANCESCORE_PERCENT">
		<xsl:variable name="VAR_SCORE">
			<xsl:call-template name="INSURANCESCORE" />
		</xsl:variable>
		<xsl:variable name="VAR_SCORE_PERCENT">
			<xsl:choose>
				<!--xsl:when test="normalize-space(POLICY/INSURANCESCORE)= 'NOHITNOSCORE'">No Hit No Score</xsl:when-->
				<xsl:when test="$VAR_SCORE != '' and $VAR_SCORE &lt; 1.00">
					<xsl:value-of select="round((1 - $VAR_SCORE) * 100)" /><xsl:text>%</xsl:text>
				</xsl:when>
				<xsl:when test="$VAR_SCORE != '' and $VAR_SCORE &gt; 1.00">
					<xsl:value-of select="round(($VAR_SCORE -1 ) * 100)" /><xsl:text>%</xsl:text> 
				</xsl:when>
			</xsl:choose>
		</xsl:variable>
		<xsl:value-of select="$VAR_SCORE_PERCENT" />
	</xsl:template>
	<xsl:template name="INSURENCESCOREDISPLAY">
		<xsl:choose>
			<xsl:when test="normalize-space(POLICY/INSURANCESCORE)='NOHITNOSCORE'"><xsl:text>No Hit No Score</xsl:text></xsl:when>
			<xsl:when test="normalize-space(POLICY/INSURANCESCORE)&gt;=0">
				<xsl:value-of select="POLICY/INSURANCESCORE" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="INSURANCE_SCORE_DISPLAY">
		<xsl:variable name="INS1" select="POLICY/INSURANCESCORE"></xsl:variable>
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
				<xsl:when test="$INS = ''"><xsl:text>1.00</xsl:text></xsl:when>
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
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$INSDISCOUNT &lt; 1">
					<xsl:text>Discount : Insurance Score Credit (</xsl:text><xsl:call-template name="INSURENCESCOREDISPLAY" /><xsl:text>) - </xsl:text><xsl:call-template name="INSURANCESCORE_PERCENT"></xsl:call-template>	
					</xsl:when>
			<xsl:otherwise>
					<xsl:text>Surcharge : Insurance Score Credit (</xsl:text><xsl:call-template name="INSURENCESCOREDISPLAY" /><xsl:text>) - </xsl:text><xsl:call-template name="INSURANCESCORE_PERCENT"></xsl:call-template>	
					</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="INSURANCE_SCORE_COMPONENT_TYPE">
		<xsl:variable name="INS1" select="POLICY/INSURANCESCORE"></xsl:variable>
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
				<xsl:when test="$INS = ''">1.00</xsl:when>
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
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$INSDISCOUNT &lt; 1">D</xsl:when>
			<xsl:otherwise>S</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Optimal -->
	<xsl:template name="OPTIMAL">
	1.00
</xsl:template>
	<!-- Antique or classic or motor home-->
	<xsl:template name="TYPEOFVEHICLE_1">
	1.00
</xsl:template>
	<xsl:template name="TYPEOFVEHICLE">
		<xsl:param name="FACTORELEMENT" /> <!-- Depending on the factor element BI/PD..etc -->
		<xsl:variable name="PVEHICLETYPE" select="VEHICLES/VEHICLE/VEHICLETYPE" />
		<!-- take the basic rates -->
		<!-- multiply with deductible rates in case of collision and comprehensive -->
		<xsl:choose>
			<xsl:when test="'$FACTORELEMENT' = 'BI'">
				<xsl:choose>
					<xsl:when test="$PVEHICLETYPE='AA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@BI" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='CA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@BI" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='SA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@BI" />
					</xsl:when>
					<xsl:otherwise>
						1.00
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="'$FACTORELEMENT' = 'PD'">
				<xsl:choose>
					<xsl:when test="$PVEHICLETYPE='AA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@PD" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='CA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@PD" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='SA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@PD" />
					</xsl:when>
					<xsl:otherwise>
					1.00
				</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="'$FACTORELEMENT' = 'PPI'">
				<xsl:choose>
					<xsl:when test="$PVEHICLETYPE='AA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@PPI" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='CA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@PPI" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='SA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@PPI" />
					</xsl:when>
					<xsl:otherwise>
					1.00
				</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="'$FACTORELEMENT' = 'PIP'">
				<xsl:choose>
					<xsl:when test="$PVEHICLETYPE='AA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@PIP" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='CA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@PIP" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='SA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@PIP" />
					</xsl:when>
					<xsl:otherwise>
						1.00
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="'$FACTORELEMENT' = 'UM'">
				<xsl:choose>
					<xsl:when test="$PVEHICLETYPE='AA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@UM" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='CA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@UM" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='SA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@UM" />
					</xsl:when>
					<xsl:otherwise>
				1.00
			</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="'$FACTORELEMENT' = 'UIM'">
				<xsl:choose>
					<xsl:when test="$PVEHICLETYPE='AA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@UIM" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='CA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@UIM" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='SA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@UIM" />
					</xsl:when>
					<xsl:otherwise>
					1.00
				</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="'$FACTORELEMENT' = 'COMP'">
				<xsl:choose>
					<xsl:when test="$PVEHICLETYPE='AA'">
						<xsl:variable name="VAR1">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@COMP50" />
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:call-template name="FETCHDEDUCTIBLEFACTOR">
								<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:value-of select="$VAR1*$VAR2" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='CA'">
						<xsl:variable name="VAR1">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@COMP50" />
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:call-template name="FETCHDEDUCTIBLEFACTOR">
								<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:value-of select="$VAR1*$VAR2" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='SA'">
						<xsl:variable name="VAR1">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@COMP50" />
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:call-template name="FETCHDEDUCTIBLEFACTOR">
								<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:value-of select="$VAR1*$VAR2" />
					</xsl:when>
					<xsl:otherwise>
				1.00
			</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="'$FACTORELEMENT' = 'COLL' or '$FACTORELEMENT' = 'LTDCOLL'">
				<xsl:choose>
					<xsl:when test="$PVEHICLETYPE='AA'">
						<xsl:variable name="VAR1">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@COLL250" />
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:call-template name="FETCHDEDUCTIBLEFACTOR">
								<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:value-of select="$VAR1*$VAR2" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='CA'">
						<xsl:variable name="VAR1">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@COLL250" />
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:call-template name="FETCHDEDUCTIBLEFACTOR">
								<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:value-of select="$VAR1*$VAR2" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE = 'SA'">
						<xsl:variable name="VAR1">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@COLL250" />
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:call-template name="FETCHDEDUCTIBLEFACTOR">
								<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:value-of select="$VAR1*$VAR2" />
					</xsl:when>
					<xsl:otherwise>
					1.00
				</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise> 1.00 </xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="FETCHDEDUCTIBLEFACTOR">
		<xsl:param name="FACTOR" /> <!-- Depending on the factor  BI/PD..etc -->
		<xsl:choose>
			<xsl:when test="$FACTOR = 'COMP'">
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE = 50">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COMP']/ATTRIBUTES/@DEDUCTIBLE50" />
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE = 100">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COMP']/ATTRIBUTES/@DEDUCTIBLE100" />
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE = 150">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COMP']/ATTRIBUTES/@DEDUCTIBLE150" />
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE = 200">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COMP']/ATTRIBUTES/@DEDUCTIBLE200" />
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE = 250">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COMP']/ATTRIBUTES/@DEDUCTIBLE250" />
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE = 500">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COMP']/ATTRIBUTES/@DEDUCTIBLE500" />
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE = 750">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COMP']/ATTRIBUTES/@DEDUCTIBLE750" />
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE = 1000">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COMP']/ATTRIBUTES/@DEDUCTIBLE1000" />
					</xsl:when>
					<xsl:otherwise>
					1.00
				</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTOR = 'COLL' or $FACTOR = 'LTDCOLL'">
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 50">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE50" />
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 100">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE100" />
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 150">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE150" />
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 200">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE200" />
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 250">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE250" />
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 500">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE500" />
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 750">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE750" />
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 1000">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE1000" />
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
	<!-- End of Vehicle Type -->
	<!-- Vehicle Class -->
	<xsl:template name="CLASS">
		<xsl:param name="FACTORELEMENT" /> <!-- Depending on the factor element BI/PD..etc -->
		<xsl:variable name="PMEDLIMIT">
			<xsl:choose>
				<xsl:when test="contains(VEHICLES/VEHICLE/MPLIMIT,',')">
					<xsl:value-of select="VEHICLES/VEHICLE/MPLIMIT" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="format-number(VEHICLES/VEHICLE/MPLIMIT,'###,###')" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="PVEHICLECLASS">
			<!-- this is to be corrected -->
			<xsl:value-of select="VEHICLES/VEHICLE/VEHICLECLASS_COMM" /> <!-- need to determine driver class first-->
			<!--<xsl:value-of select="5" />     here class is hardcoded as 4 error in input xml -->
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLECLASS_COMM = ''">
			1.00
		</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="$FACTORELEMENT='BI'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CLASSRELATIVITIES']/NODE[@ID='CR']/ATTRIBUTES[@CODE= $PVEHICLECLASS]/@RELATIVITY" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT='PD'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CLASSRELATIVITIES']/NODE[@ID='CR']/ATTRIBUTES[@CODE= $PVEHICLECLASS] /@RELATIVITY" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT='PPI'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CLASSRELATIVITIES']/NODE[@ID='CR']/ATTRIBUTES[@CODE= $PVEHICLECLASS]/@RELATIVITY" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT='PIP'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CLASSRELATIVITIES']/NODE[@ID='CR']/ATTRIBUTES[@CODE= $PVEHICLECLASS]/@RELATIVITY" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT='MEDPAY' or $PMEDLIMIT !='' or $PMEDLIMIT !=0">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CLASSRELATIVITIES']/NODE[@ID='CR_MED']/ATTRIBUTES[@LIMIT=$PMEDLIMIT]/@MEDREL" />
					</xsl:when>
					<xsl:otherwise> 1.00 </xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Limit -->
	<xsl:template name="LIMIT">
		<xsl:param name="FACTORELEMENT" />
		<xsl:variable name="PVEHICLEROWID" select="VEHICLES/VEHICLE/VEHICLEROWID"></xsl:variable> <!-- Ask Deepak -->
		<xsl:choose>
			<xsl:when test="$FACTORELEMENT = 'BI'"> <!-- BI -->
				<xsl:variable name="COVERAGELL" select="VEHICLES/VEHICLE/BILIMIT1" /> <!-- Lower Limit CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
				<xsl:variable name="COVERAGEUL" select="VEHICLES/VEHICLE/BILIMIT2" /> <!-- Upper Limit -->
				<xsl:choose>
					<xsl:when test="$COVERAGELL ='' or $COVERAGELL='0'">
					1.00
				</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIMITS']/NODE[@ID= $FACTORELEMENT ]/ATTRIBUTES[@MINCOVERAGE= $COVERAGELL and @MAXCOVERAGE = $COVERAGEUL ]/@RELATIVITY" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='MEDPAY'"> <!-- Medpay -->
				<xsl:variable name="PMEDPAY">
					<xsl:choose>
						<xsl:when test="contains(VEHICLES/VEHICLE/MPLIMIT,',')">
							<xsl:value-of select="VEHICLES/VEHICLE/MPLIMIT" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="format-number(VEHICLES/VEHICLE/MPLIMIT,'###,###')" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$PMEDPAY !='' and $PMEDPAY !=0">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIMITS']/NODE[@ID=$FACTORELEMENT]/ATTRIBUTES[@COVERAGE = $PMEDPAY]/@RELATIVITY" />
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='UM'"> <!-- UM -->
				<xsl:variable name="COVERAGELL" select="VEHICLES/VEHICLE/UMSPLITLIMIT1" /> <!-- Lower Limit CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
				<xsl:variable name="COVERAGEUL" select="VEHICLES/VEHICLE/UMSPLITLIMIT2" /> <!-- Upper Limit -->
				<xsl:choose>
					<!-- Check if split limit or combined single limit -->
					<xsl:when test="VEHICLES/VEHICLE/UMCSL !='' and VEHICLES/VEHICLE/UMCSL !='0'"> <!-- If combined single limit -->
						<!--xsl:variable name="PUMCSL" select="normalize-space(VEHICLES/VEHICLE/UMCSL)" /-->
						<xsl:variable name="PUMCSL">
							<xsl:choose>
								<xsl:when test="contains(VEHICLES/VEHICLE/UMCSL,',')">
									<xsl:value-of select="VEHICLES/VEHICLE/UMCSL" />
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="format-number(VEHICLES/VEHICLE/UMCSL,'###,###')" />
								</xsl:otherwise>
							</xsl:choose>
						</xsl:variable>
						<xsl:choose>
							<xsl:when test="$PVEHICLEROWID &gt; 0 and $PVEHICLEROWID =1">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUMBILIMITS']/NODE[@ID ='CSLUMBI']/ATTRIBUTES[@CSLUMBI=$PUMCSL]/@CSLUMRATES1" />
							</xsl:when>
							<xsl:when test="$PVEHICLEROWID &gt; 1">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUMBILIMITS']/NODE[@ID ='CSLUMBI']/ATTRIBUTES[@CSLUMBI=$PUMCSL]/@CSLUMRATES2" />
							</xsl:when>
							<xsl:otherwise>
								0.00
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise> <!-- If  split limit -->
						<xsl:variable name="PMINLIMIT" select="VEHICLES/VEHICLE/UMSPLITLIMIT1"></xsl:variable>
						<xsl:variable name="PMAXLIMIT" select="VEHICLES/VEHICLE/UMSPLITLIMIT2"></xsl:variable>
						<xsl:choose>
							<xsl:when test="$PMINLIMIT != 0 and $PMAXLIMIT != 0">
								<xsl:choose>
									<xsl:when test="$PVEHICLEROWID &gt; 0 and $PVEHICLEROWID =1">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMBILIMITS']/NODE[@ID ='UMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMIT and @MAXLIMIT=$PMAXLIMIT]/@UMRATE1" />
									</xsl:when>
									<xsl:when test="$PVEHICLEROWID &gt; 1">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMBILIMITS']/NODE[@ID ='UMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMIT and @MAXLIMIT=$PMAXLIMIT]/@UMRATE2" />
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
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT = 'UIM'"> <!-- UIM -->
				<xsl:variable name="COVERAGELL" select="VEHICLES/VEHICLE/UIMSPLITLIMIT1" /> <!-- Lower Limit CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
				<xsl:variable name="COVERAGEUL" select="VEHICLES/VEHICLE/UIMSPLITLIMIT2" /> <!-- Upper Limit -->
				<xsl:variable name="COVERAGECSL" select="VEHICLES/VEHICLE/UIMCSL" />
				<xsl:variable name="COVERAGE"> <!-- CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
					<xsl:choose>
						<xsl:when test="contains($COVERAGECSL,',')">
							<xsl:call-template name="STRING-REPLACE-ALL">
								<xsl:with-param name="text" select="$COVERAGECSL" />
								<xsl:with-param name="replace" select="','" />
								<xsl:with-param name="by" select="''" />
							</xsl:call-template>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$COVERAGECSL" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/ISUNDERINSUREDMOTORISTS ='TRUE'">
						<xsl:choose> <!-- Check if split limit or combined single limit -->
							<xsl:when test="$COVERAGECSL &gt; 0 or $COVERAGECSL !=''"> <!-- if combined limit -->
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUIMBILIMITS']/NODE[@ID ='CSLUIMBI']/ATTRIBUTES[@CSLUIMBILIMITS = $COVERAGE]/@CSLUIMRATES" />
							</xsl:when>
							<xsl:when test="$COVERAGELL != 0 or $COVERAGEUL != 0 and COVERAGEUL != ''"> <!-- If  split limit -->
								<xsl:choose>
									<xsl:when test="VEHICLES/VEHICLE/UIMSPLIT !='' and VEHICLES/VEHICLE/UIMSPLIT !='0' and VEHICLES/VEHICLE/UIMSPLIT !='0/0'  ">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UIMBILIMITS']/NODE[@ID ='UIMBI']/ATTRIBUTES[@MINLIMIT = $COVERAGELL and @MAXLIMIT=$COVERAGEUL]/@UIMRATES" />
									</xsl:when>
									<xsl:otherwise>0.00</xsl:otherwise>
								</xsl:choose>
							</xsl:when>
							<xsl:otherwise>0.00</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>
						0.00
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when> <!-- UIM END-->
			<xsl:otherwise> <!-- PD -->
				<xsl:variable name="COVERAGE"> <!-- CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
					<xsl:choose>
						<xsl:when test="contains(VEHICLES/VEHICLE/PD,',')">
							<xsl:value-of select="VEHICLES/VEHICLE/PD" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="format-number(VEHICLES/VEHICLE/PD,'###,###')" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$COVERAGE ='' or $COVERAGE='0'">
					1.00
				</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIMITS']/NODE[@ID = $FACTORELEMENT ]/ATTRIBUTES[@COVERAGE = $COVERAGE ]/@RELATIVITY" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Multi-vehicle -->
	<xsl:template name="MULTIVEHICLE">
		1.00
		<!--xsl:param name="FACTORELEMENT" /> 
		<xsl:variable name="PDRIVERCLASS" select="DRIVERS/DRIVER[@ID='1']/DRIVERCLASSCOMPONENT1" /> 
		<xsl:choose>
			<xsl:when test="$FACTORELEMENT='BI'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLEBI']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR" />
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='PD'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLEPD']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR" />
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='PPI'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLEPPI']/ATTRIBUTES[@CLASS  = $PDRIVERCLASS]/@FACTOR" />
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='PIP'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLEPIP']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR" />
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='COLL'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLECOLL']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR" />
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='LTDCOLL'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLECOLL']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR" />
			</xsl:when>
			<xsl:otherwise> 1.00 </xsl:otherwise>
		</xsl:choose-->
	</xsl:template>
	<xsl:template name="MULTIVEHICLE_DISPLAY">
		0
		<!--xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/MULTICARDISCOUNT='TRUE'">
			Included
		</xsl:when>
			<xsl:otherwise>
			0
		</xsl:otherwise>
		</xsl:choose-->
	</xsl:template>
	<!-- Multicar discount CSL -->
	<xsl:template name="MULTICARDISCOUNTCSL">
		1
		<!--xsl:variable name="PDRIVERCLASS">
			<xsl:value-of select="DRIVERS/DRIVER[@ID='1']/DRIVERCLASS" />
			<xsl:value-of select="VEHICLES/VEHICLE/VEHICLEUSE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PDRIVERCLASS ='P1' or $PDRIVERCLASS ='P2'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTICARCSL']/ATTRIBUTES[@DRIVERCLASS = $PDRIVERCLASS]/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTICARCSL']/ATTRIBUTES[@DRIVERCLASS = 'OTHER']/@FACTOR" />
			</xsl:otherwise>
		</xsl:choose-->
	</xsl:template>
	<!-- Extra Equipment :Comprehensive-->
	<xsl:template name="EXTRAEQUIPMENTCOMP">
		<xsl:variable name="PDEDUCTIBLE" select="VEHICLES/VEHICLE/EXTRAEQUIPCOMPREHENSIVEDEDUCTIBLE" />
		<xsl:variable name="PINSURANCEAMOUNT" select="VEHICLES/VEHICLE/INSURANCEAMOUNT" />
		<xsl:choose>
			<xsl:when test="$PINSURANCEAMOUNT != '' and $PINSURANCEAMOUNT != 'NO COVERAGE' and $PDEDUCTIBLE !='' and $PDEDUCTIBLE != '0' and VEHICLES/VEHICLE/VEHICLETYPE_SCO != 'SCO'">
				<!-- Check for Rate per value -->
				<xsl:variable name="PRATEPERVALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COMP']/@RATE_PER_VALUE" />
				<!-- Check the Extra Equipment component -->
				<xsl:variable name="PEXTRAEQUIPMENTCOMP">
					<xsl:choose>
						<xsl:when test="$PDEDUCTIBLE='' or $PDEDUCTIBLE=0">
							0.00
						</xsl:when>
						<xsl:when test="POLICY/STATENAME='MICHIGAN'">
							<xsl:choose>
								<xsl:when test="$PDEDUCTIBLE=250">
									<xsl:variable name="VAR1">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COMP']/@RATE" />
									</xsl:variable>
									<xsl:value-of select="round($VAR1 * ($PINSURANCEAMOUNT div $PRATEPERVALUE))" />
								</xsl:when>
								<!-- Multiply rate with Deductible Relativity-->
								<xsl:otherwise>
									<xsl:variable name="VAR1" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK=normalize-space('COMP')]/@RATE" />
									<xsl:variable name="VAR2" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLEO']/NODE[@ID='DEDTBLO']/ATTRIBUTES[@DEDUCTIBLE=$PDEDUCTIBLE]/@RELATIVITY" />
									<xsl:value-of select="round(round($VAR1*($PINSURANCEAMOUNT div $PRATEPERVALUE))* $VAR2)" />
								</xsl:otherwise>
							</xsl:choose>
						</xsl:when>
						<!-- Multiply rate with Deductible Relativity-->
						<xsl:otherwise>
							<xsl:variable name="VAR1">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COMP' and @DEDUCTIBLE=$PDEDUCTIBLE]/@RATE" />
							</xsl:variable>
							<xsl:value-of select="round($VAR1 * ($PINSURANCEAMOUNT div $PRATEPERVALUE))" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<!-- Check for Minimum Premium  -->
				<xsl:variable name="PMINIMUMVALUE">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COMP']/@MINIMUM_PREMIUM" />
				</xsl:variable>
				<!-- If the extra equipment component is less than the minimum premium component then return minimum premium -->
				<xsl:choose>
					<xsl:when test="$PEXTRAEQUIPMENTCOMP=0 or '$PEXTRAEQUIPMENTCOMP' =''">
						0.00
					</xsl:when>
					<xsl:otherwise>
						<xsl:choose>
							<xsl:when test="$PEXTRAEQUIPMENTCOMP &gt; $PMINIMUMVALUE">
								<xsl:value-of select="$PEXTRAEQUIPMENTCOMP" />
							</xsl:when>
							<xsl:when test="$PEXTRAEQUIPMENTCOMP &lt;= $PMINIMUMVALUE">
								<xsl:value-of select="$PMINIMUMVALUE" />
							</xsl:when>
							<xsl:otherwise>0.00</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0.00 </xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--  ***************************************************************************************  -->
	<!-- Extra Equipment :Collision-->
	<xsl:template name="EXTRAEQUIPMENTCOLL">
		<xsl:variable name="PDEDUCTIBLE" select="VEHICLES/VEHICLE/EXTRAEQUIPCOLLISIONDEDUCTIBLE" />
		<xsl:variable name="PCOLLISIONTYPE" select="VEHICLES/VEHICLE/EXTRAEQUIPCOLLISIONTYPE" />
		<xsl:variable name="PINSURANCEAMOUNT" select="VEHICLES/VEHICLE/INSURANCEAMOUNT" />
		<xsl:choose>
			<xsl:when test="($PINSURANCEAMOUNT !='' and $PINSURANCEAMOUNT !='NO COVERAGE' and $PINSURANCEAMOUNT &gt; 0)and ($PDEDUCTIBLE !='' and $PDEDUCTIBLE &gt; 0 and VEHICLES/VEHICLE/VEHICLETYPE_SCO != 'SCO')">
				<!-- Check for Rate per value ****** -->
				<xsl:variable name="PRATEPERVALUE">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE_PER_VALUE" />
				</xsl:variable>
				<!-- Check the Extra Equipment component -->
				<xsl:variable name="PEXTRAEQUIPMENTCOLL">
					<xsl:choose>
						<xsl:when test="($PINSURANCEAMOUNT !='' and $PINSURANCEAMOUNT !='NO COVERAGE' and $PINSURANCEAMOUNT &gt; 0  and $PDEDUCTIBLE !='' and $PDEDUCTIBLE != '0' and $PCOLLISIONTYPE != 'LIMITED')">
							<xsl:choose>
								<xsl:when test="POLICY/STATENAME='MICHIGAN'">
									<xsl:choose>
										<!--xsl:when test="normalize-space($PDEDUCTIBLE)=250">
											<xsl:variable name="VAR1">
												<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE" />
											</xsl:variable>
											<xsl:value-of select="round($VAR1 * ($PINSURANCEAMOUNT div $PRATEPERVALUE))" />
										</xsl:when-->
										<xsl:when test="normalize-space($PCOLLISIONTYPE)='LIMITED'">
											<xsl:variable name="VAR1">
												<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE" />
											</xsl:variable>
											<xsl:variable name="VAR2">
												<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@COLLISIONTYPE=$PCOLLISIONTYPE]/@RELATIVITY" />
											</xsl:variable>
											<xsl:value-of select="round($VAR1 * ($PINSURANCEAMOUNT div $PRATEPERVALUE) * $VAR2)" />
										</xsl:when>
										<!-- Multiply rate with Deductible Relativity-->
										<xsl:otherwise>
											<xsl:variable name="VAR1" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@COLLISIONTYPE=$PCOLLISIONTYPE and @DEDUCTIBLE=$PDEDUCTIBLE]/@RELATIVITY" />
											<xsl:variable name="VAR2" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK=normalize-space('COLL')]/@RATE" />
											<xsl:value-of select="round($VAR1 * ($PINSURANCEAMOUNT div $PRATEPERVALUE) * $VAR2)" />
										</xsl:otherwise>
									</xsl:choose>
								</xsl:when>
								<xsl:when test="POLICY/STATENAME = 'INDIANA'">
									<xsl:choose>
										<xsl:when test="'$PDEDUCTIBLE' = 0">0</xsl:when>
										<!--xsl:when test="$PDEDUCTIBLE=250">
											<xsl:variable name="VAR1">
												<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL' and @DEDUCTIBLE=$PDEDUCTIBLE]/@RATE" />
											</xsl:variable>
											<xsl:value-of select="round($VAR1*($PINSURANCEAMOUNT div $PRATEPERVALUE))" />
										</xsl:when-->
										<xsl:when test="$PDEDUCTIBLE &gt; 0">
											<xsl:variable name="VAR1">
												<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL' and @DEDUCTIBLE=$PDEDUCTIBLE]/@RATE" />
											</xsl:variable>
											<xsl:variable name="VAR2">
												<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@DEDUCTIBLE=$PDEDUCTIBLE]/@RELATIVITY" />
											</xsl:variable>
											<xsl:value-of select="round($VAR1 * ($PINSURANCEAMOUNT div $PRATEPERVALUE) * $VAR2)" />
										</xsl:when>
										<!-- Multiply rate with Deductible Relativity -->
										<xsl:otherwise>0.00</xsl:otherwise>
									</xsl:choose>
								</xsl:when>
								<xsl:otherwise>
								0.00
							</xsl:otherwise>
							</xsl:choose>
						</xsl:when>
						<!-- Limited Collision-->
						<xsl:when test="($PINSURANCEAMOUNT !='' and $PINSURANCEAMOUNT !='NO COVERAGE' and $PINSURANCEAMOUNT &gt; 0 and $PCOLLISIONTYPE = 'LIMITED')">
							<xsl:choose>
								<xsl:when test="POLICY/STATENAME='MICHIGAN'">
									<xsl:choose>
										<xsl:when test="normalize-space($PCOLLISIONTYPE)='LIMITED'">
											<xsl:variable name="VAR1">
												<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE" />
											</xsl:variable>
											<xsl:variable name="VAR2">
												<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@COLLISIONTYPE=$PCOLLISIONTYPE]/@RELATIVITY" />
											</xsl:variable>
											<xsl:value-of select="round($VAR1 * ($PINSURANCEAMOUNT div $PRATEPERVALUE) * $VAR2)" />
										</xsl:when>
										<!-- Multiply rate with Deductible Relativity-->
										<xsl:otherwise>
											<xsl:variable name="VAR1" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@COLLISIONTYPE=$PCOLLISIONTYPE and @DEDUCTIBLE=$PDEDUCTIBLE]/@RELATIVITY" />
											<xsl:variable name="VAR2" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK=normalize-space('COLL')]/@RATE" />
											<xsl:value-of select="round($VAR1 * ($PINSURANCEAMOUNT div $PRATEPERVALUE) * $VAR2)" />
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
				</xsl:variable>
				<!-- Check for Minimum Premium -->
				<xsl:variable name="PMINIMUMVALUE">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@MINIMUM_PREMIUM" />
				</xsl:variable>
				<!-- If the extra equipment component is less than the minimum premium component then return minimum premium -->
				<xsl:choose>
					<xsl:when test="'$PEXTRAEQUIPMENTCOLL'='' or ('$PEXTRAEQUIPMENTCOLL' ='0' and '$PCOLLISIONTYPE' !='LIMITED')">
					0.00
				</xsl:when>
					<xsl:otherwise>
						<xsl:choose>
							<xsl:when test="$PEXTRAEQUIPMENTCOLL &gt; $PMINIMUMVALUE">
								<xsl:value-of select="$PEXTRAEQUIPMENTCOLL" />
							</xsl:when>
							<xsl:when test="$PEXTRAEQUIPMENTCOLL &lt; $PMINIMUMVALUE">
								<xsl:value-of select="$PMINIMUMVALUE" />
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="$PEXTRAEQUIPMENTCOLL" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Intermediate Factor -->
	<xsl:template name="INTERMEDIATEFACTOR">
		<xsl:variable name="PMILESEACHWAY" select="VEHICLES/VEHICLE/MILESEACHWAY" />
		<xsl:variable name="PINTERMEDIATEFACTOR" select="normalize-space(VEHICLES/VEHICLE/VEHICLERATINGCODE)" />
		<xsl:choose>
			<xsl:when test="$PINTERMEDIATEFACTOR ='L'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INTERMEDIATE_FACTOR']/NODE[@ID='INTERMEDIATE']/ATTRIBUTES[@DESC = 'LOCAL']/@FACTOR" />
			</xsl:when>
			<xsl:when test="$PINTERMEDIATEFACTOR ='I'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INTERMEDIATE_FACTOR']/NODE[@ID='INTERMEDIATE']/ATTRIBUTES[@DESC = 'INTERMEDIATE']/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Vehicle Use -->
	<xsl:template name="VEHICLEUSE">
		<xsl:variable name="PDRIVERCLASS" select="VEHICLES/VEHICLE/VEHICLECLASSCOMPONENT1" />
		<xsl:variable name="PUSECODE" select="VEHICLES/VEHICLE/VEHICLEUSE" /> <!-- ask deepak -->
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEUSERELATIVITY']/NODE[@ID='VEHICLEUSE']/ATTRIBUTES[@USECODE = 'P' and @CLASS = 'P']/@FACTOR" />
	</xsl:template>
	<!-- Premier or Safe driver discount -->
	<xsl:template name="DRIVERDISCOUNT">
		<!-- 1. Check for Premier or Safe Discount 
		 2. If Premier then call Premier template.
		 3. If Safe then check for Surcharge.
		 4. If Surcharge > 1 then do not call Safe Driver Template ..return 1.00
		 5.			else call SafeDriver template.-->
		<xsl:choose>
			<xsl:when test="DRIVERS/DRIVER[@ID='1']/NOPREMDRIVERDISC='TRUE'">
				<xsl:call-template name="PREMIERDISCOUNT" />
			</xsl:when>
			<xsl:when test="DRIVERS/DRIVER[@ID='1']/NOPREMDRIVERDISC='FALSE'">
				<xsl:variable name="PSURCHARGE">
					<xsl:call-template name="SURCHARGE" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$PSURCHARGE &gt; 0">
						<xsl:call-template name="SAFEDISCOUNT" />
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
	<xsl:template name="PREMIERDISCOUNT">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERDISCOUNT']/NODE[@ID ='DRIVERDISC']/ATTRIBUTES/@SAFEDRIVERFACTOR" />
	</xsl:template>
	<xsl:template name="SAFEDISCOUNT">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERDISCOUNT']/NODE[@ID ='DRIVERDISC']/ATTRIBUTES/@PREMIERDRIVERFACTOR" />
	</xsl:template>
	<xsl:template name="DRIVERDISCOUNT_CREDIT">
		<!-- 1. Check for Premier or Safe Discount 
		 2. If Premier then call Premier template.
		 3. If Safe then check for Surcharge.
		 4. If Surcharge > 1 then do not call Safe Driver Template ..return 1.00
		 5.			else call SafeDriver template.-->
		<xsl:choose>
			<xsl:when test="DRIVERS/DRIVER[@ID='1']/NOPREMDRIVERDISC='FALSE'">
				<xsl:call-template name="PREMIERDISCOUNT_CREDIT" />
			</xsl:when>
			<xsl:when test="DRIVERS/DRIVER[@ID='1']/NOPREMDRIVERDISC='TRUE'">
				<xsl:variable name="PSURCHARGE">
					<xsl:call-template name="SURCHARGE" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$PSURCHARGE &gt; 0">
						<xsl:call-template name="SAFEDISCOUNT_CREDIT" />
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PREMIERDISCOUNT_CREDIT">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERDISCOUNT']/NODE[@ID ='DRIVERDISC']/ATTRIBUTES/@SAFEDRIVERCREDIT" />
	</xsl:template>
	<xsl:template name="SAFEDISCOUNT_CREDIT">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERDISCOUNT']/NODE[@ID ='DRIVERDISC']/ATTRIBUTES/@PREMIERDRIVERCREDIT" />
	</xsl:template>
	<!-- End of Premier or Safe driver discount -->
	<!-- Good student discount -->
	<xsl:template name="GOODSTUDENT">
		<xsl:choose>
			<xsl:when test="DRIVERS/DRIVER/GOODSTUDENT = 'TRUE'">
				<!-- Check if the surcharge is greater less than 2.-->
				<xsl:variable name="PSURCHARGE">
					<xsl:call-template name="SURCHARGE" />
				</xsl:variable>
				<xsl:choose>
					<!-- discount applicable only if surcharge is less than 2 -->
					<xsl:when test="$PSURCHARGE &lt; 2">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='GOODSTUDENT']/NODE[@ID ='GOODSTUDENTDISCOUNT']/ATTRIBUTES/@FACTOR" />
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
	<xsl:template name="STRING-REPLACE-ALL">
		<xsl:param name="text" />
		<xsl:param name="replace" />
		<xsl:param name="by" />
		<xsl:choose>
			<xsl:when test="contains($text,$replace)">
				<xsl:value-of select="substring-before($text,$replace)" />
				<xsl:value-of select="$by" />
				<xsl:call-template name="STRING-REPLACE-ALL">
					<xsl:with-param name="text" select="substring-after($text,$replace)" />
					<xsl:with-param name="replace" select="$replace" />
					<xsl:with-param name="by" select="$by" />
				</xsl:call-template>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$text" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Combined Single Limit Relativity -->
	<xsl:template name="CSLRELATIVITY">
		<xsl:variable name="PCSLLIMIT" select="VEHICLES/VEHICLE/CSL" /> <!-- ASK DEEPAK TO GIVE CSL FOR ALL NODES -->
		<xsl:variable name="COVERAGE"> <!-- CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
			<xsl:choose>
				<xsl:when test="contains($PCSLLIMIT,',')">
					<xsl:call-template name="STRING-REPLACE-ALL">
						<xsl:with-param name="text" select="$PCSLLIMIT" />
						<xsl:with-param name="replace" select="','" />
						<xsl:with-param name="by" select="''" />
					</xsl:call-template>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$PCSLLIMIT" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$COVERAGE &gt; 0">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMBINEDSINGLELIMITS']/NODE[@ID ='CSL']/ATTRIBUTES[@CSLLIMIT=$COVERAGE]/@CSLRELATIVITY" />
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="SURCHARGE">
		<!-- 1. Get the violation and accident points.
		2. Against each violation/accident point, fetch the surcharge  (After 13 points, calculate the surcharge using "For each additional" value
		3. Add the surcharges
		4. Divide the sum by 100 and add 1 -->
		<!-- Accident Surcharge  -->
		<!-- Run a loop for each Assign Driver Accident Points. -->
		<xsl:variable name="VAR_SUMOFACCIDENTPOINT" select="user:GetPvioAcc(-1)" />
		<xsl:variable name="ACCIDENTSURCHARGE">
			<xsl:for-each select="VEHICLES/VEHICLE/ASSIGNDRIVIOACCPNT[@ID &lt; 9999999]">
				<xsl:variable name="ACCIDENTPOINTS">
					<xsl:value-of select="SUMOFACCIDENTPOINTS" />
				</xsl:variable>
				<xsl:variable name="VAR_ACCIDENT_PERCENTAGE">
					<xsl:choose>
						<xsl:when test="$ACCIDENTPOINTS !='' and $ACCIDENTPOINTS !='0' and $ACCIDENTPOINTS &gt; 0">
							<xsl:choose>
								<xsl:when test="$ACCIDENTPOINTS &gt; 13">
									<xsl:variable name="VAR1">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS=13]/@SURCHARGEPERCENT" />
									</xsl:variable>
									<xsl:variable name="VAR2">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/@FOR_EACH_ADDITIONAL" />
									</xsl:variable>
									<xsl:value-of select="$VAR1 + round(($ACCIDENTPOINTS - 13) * $VAR2)" />
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS=$ACCIDENTPOINTS]/@SURCHARGEPERCENT" />
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
		</xsl:variable>
		<!-- Violation Surcharge -->
		<!-- run a loop for each assign driver violation point	 -->
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
		</xsl:variable>
		<xsl:variable name="var1" select="user:GetPvioAcc(0)" />
		<xsl:value-of select="(($var1) div 100.00) + 1.00" />
	</xsl:template>
	<xsl:template name="ACCIDENT_VIOLATION_SURCHRAGE_PERCENTAGE">
		<!-- Accident Surcharge  -->
		<!-- Run a loop for each Assign Driver Accident Points. -->
		<xsl:variable name="VAR_SUMOFACCIDENTPOINT" select="user:GetPvioAcc(-1)" />
		<xsl:variable name="ACCIDENTSURCHARGE">
			<xsl:for-each select="VEHICLES/VEHICLE/ASSIGNDRIVIOACCPNT[@ID &lt; 9999999]">
				<xsl:variable name="ACCIDENTPOINTS">
					<xsl:value-of select="SUMOFACCIDENTPOINTS" />
				</xsl:variable>
				<xsl:variable name="VAR_ACCIDENT_PERCENTAGE">
					<xsl:choose>
						<xsl:when test="$ACCIDENTPOINTS !='' and $ACCIDENTPOINTS !='0' and $ACCIDENTPOINTS &gt; 0">
							<xsl:choose>
								<xsl:when test="$ACCIDENTPOINTS &gt; 13">
									<xsl:variable name="VAR1">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS=13]/@SURCHARGEPERCENT" />
									</xsl:variable>
									<xsl:variable name="VAR2">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/@FOR_EACH_ADDITIONAL" />
									</xsl:variable>
									<xsl:value-of select="$VAR1 + round(($ACCIDENTPOINTS - 13) * $VAR2)" />
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS=$ACCIDENTPOINTS]/@SURCHARGEPERCENT" />
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
		</xsl:variable>
		<!-- Violation Surcharge -->
		<!-- run a loop for each assign driver violation point	 -->
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
		</xsl:variable>
		<xsl:variable name="var1" select="user:GetPvioAcc(0)" />
		<xsl:value-of select="$var1" />
	</xsl:template>
	<xsl:template name="ACCIDENT_VIOLATION_SURCHARGE_DISPLAY">
		<xsl:variable name="PACCVIOSURVAL">
			<xsl:call-template name="ACCIDENT_VIOLATION_SURCHRAGE_PERCENTAGE"></xsl:call-template>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PACCVIOSURVAL &gt; 0">Included</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Surcharge -->
	<!--xsl:template name="SURCHARGE"-->
	<!-- 1. Get the violation and accident points.
		2. Against each violation/accident point, fetch the surcharge  (After 13 points, calculate the surcharge using "For each additional" value
		3. Add the surcharges
		4. Divide the sum by 100 and add 1 -->
	<!-- Accident Surcharge  -->
	<!--xsl:variable name="ACCIDENTPOINTS">
			<xsl:value-of select="VEHICLES/VEHICLE/SUMOFACCIDENTPOINTS" />
		</xsl:variable>
		<xsl:variable name="ACCIDENTSURCHARGE">
			<xsl:choose>
				<xsl:when test="$ACCIDENTPOINTS !='' and $ACCIDENTPOINTS !='0' and $ACCIDENTPOINTS &gt; 0">
					<xsl:choose>
						<xsl:when test="$ACCIDENTPOINTS &gt; 13">
							<xsl:variable name="VAR1">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS=13]/@SURCHARGE" />
							</xsl:variable>
							<xsl:variable name="VAR2">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/@FOR_EACH_ADDITIONAL" />
							</xsl:variable>
							<xsl:value-of select="$VAR1 + round(($ACCIDENTPOINTS - 13) * $VAR2)" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS=$ACCIDENTPOINTS]/@SURCHARGE" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:when>
				<xsl:otherwise>0.00</xsl:otherwise>
			</xsl:choose>
		</xsl:variable-->
	<!-- Violation Surcharge -->
	<!--xsl:variable name="VIOLATIONPOINTS">
			<xsl:value-of select="VEHICLES/VEHICLE/SUMOFVIOLATIONPOINTS" />
		</xsl:variable>
		<xsl:variable name="VIOLATIONSURCHARGE">
			<xsl:choose>
				<xsl:when test="$VIOLATIONPOINTS !='' and $VIOLATIONPOINTS !='0' and $VIOLATIONPOINTS &gt; 0">
					<xsl:choose>
						<xsl:when test="$VIOLATIONPOINTS &gt; 13"-->
	<!-- new calculation added by praveen singh -->
	<!--xsl:variable name="VAR1">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEVIOLATIONPOINTS']/ATTRIBUTES[@POINTS=13]/@SURCHARGEPERCENT" />
							</xsl:variable>
							<xsl:variable name="VAR2">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEVIOLATIONPOINTS']/@FOR_EACH_ADDITIONAL" />
							</xsl:variable>
							<xsl:value-of select="$VAR1 + round(($VIOLATIONPOINTS - 13) * $VAR2)" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEVIOLATIONPOINTS']/ATTRIBUTES[@POINTS=$VIOLATIONPOINTS]/@SURCHARGEPERCENT" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:when>
				<xsl:otherwise>0.00</xsl:otherwise>
			</xsl:choose>
		</xsl:variable-->
	<!--
	(
		(
			(<xsl:value-of select="$ACCIDENTSURCHARGE"/>)
			+
			(<xsl:value-of select="$VIOLATIONSURCHARGE"/>)
		)
		DIV 100.00
	)
	+ 1.00
-->
	<!-- new calculation added by praveen singh -->
	<!--xsl:value-of select="(($ACCIDENTSURCHARGE+$VIOLATIONSURCHARGE) div 100.00) + 1.00" />
	</xsl:template-->
	<!--  Multi Policy discount  -->
	<xsl:template name="MULTIPOLICY">
		0.00<!--xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE =$VT_TRAILER">0.00</xsl:when>
			<xsl:when test="POLICY/MULTIPOLICYAUTOHOMEDISCOUNT = 'TRUE'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIPOLICY']/NODE[@ID ='MULTIPOLICYDISCOUNT']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>
		1.00
		</xsl:otherwise>
		</xsl:choose-->
	</xsl:template>
	<!-- Multipolicy Display -->
	<xsl:template name="MULTIPOLICY_DISPLAY">
		0.00<!--xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE =$VT_TRAILER">0.00</xsl:when>
			<xsl:when test="POLICY/MULTIPOLICYAUTOHOMEDISCOUNT = 'TRUE' or POLICY/MULTIPOLICYAUTOHOMEDISCOUNT = 'Y'">
			Included
		</xsl:when>
			<xsl:otherwise>
			0
		</xsl:otherwise>
		</xsl:choose-->
	</xsl:template>
	<!-- Coverage Type discount  -->
	<xsl:template name="COVERAGETYPE">
	<!--xsl:variable name="PPIPCOVERAGECODE" select ="VEHICLES/VEHICLE/PIPCOVERAGECODE"></xsl:variable>
	<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = $PPIPCOVERAGECODE ]/@RELATIVITY"/-->
	1.00
</xsl:template>
	<!-- Symbol-->
	<xsl:template name="SYMBOL">
		<xsl:param name="FACTORELEMENT" />
		<xsl:variable name="PAGE" select="VEHICLES/VEHICLE/AGE"></xsl:variable> <!-- select="AGE" check with deepak -->
		<xsl:variable name="PSYMBOL" select="VEHICLES/VEHICLE/SYMBOL"></xsl:variable>
		<xsl:choose>
			<xsl:when test="$PAGE !='' and $PSYMBOL !='' and $PSYMBOL !='0' ">
				<xsl:choose>
					<xsl:when test="$FACTORELEMENT='COMP'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SYMBOLRELATIVITYOTHERTHANCOLLISON']/NODE[@ID='SYMBOLRELATIVITYOTHERTHANCOLL']/ATTRIBUTES[@SYMBOL = $PSYMBOL]/@RELATIVITY" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'COLL' or $FACTORELEMENT = 'LTDCOLL' ">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SYMBOLRELATIVITYCOLLISION']/NODE[@ID='SYMBOLRELATIVITYCOLL']/ATTRIBUTES[@SYMBOL = $PSYMBOL ]/@RELATIVITY" />
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
	<!-- ******************************** START: DEDUCTIBLE *************************************************************-->
	<xsl:template name="DEDUCTIBLE">
		<xsl:param name="FACTORELEMENT" />
		<xsl:choose>
			<xsl:when test="$FACTORELEMENT = 'COMP'">
				<xsl:variable name="PCOMPREHENSIVEDEDUCTIBLE" select="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE" />
				<xsl:choose>
					<xsl:when test="$PCOMPREHENSIVEDEDUCTIBLE !='' and $PCOMPREHENSIVEDEDUCTIBLE !='0'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLEO']/NODE[@ID='DEDTBLO']/ATTRIBUTES[@DEDUCTIBLE = $PCOMPREHENSIVEDEDUCTIBLE ]/@RELATIVITY" />
					</xsl:when>
					<xsl:otherwise>
					0.00
				</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT = 'COLL' and POLICY/STATENAME ='MICHIGAN'">
				<xsl:variable name="PCOLLISIONTYPE" select="VEHICLES/VEHICLE/COVGCOLLISIONTYPE" />
				<xsl:variable name="PCOVGCOLLISIONDEDUCTIBLE" select="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE" />
				<xsl:choose>
					<xsl:when test="$PCOLLISIONTYPE !='' and $PCOVGCOLLISIONDEDUCTIBLE !=''">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@COLLISIONTYPE = $PCOLLISIONTYPE and @DEDUCTIBLE = $PCOVGCOLLISIONDEDUCTIBLE ]/@RELATIVITY" />
					</xsl:when>
					<xsl:otherwise>
						0.00
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT = 'COLL' and POLICY/STATENAME ='INDIANA'">
				<xsl:variable name="PCOVGCOLLISIONDEDUCTIBLE" select="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE" />
				<xsl:choose>
					<xsl:when test="$PCOVGCOLLISIONDEDUCTIBLE !='' and $PCOVGCOLLISIONDEDUCTIBLE !='0'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@DEDUCTIBLE = $PCOVGCOLLISIONDEDUCTIBLE ]/@RELATIVITY" />
					</xsl:when>
					<xsl:otherwise>
						0.00
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="EXTRAEQUIPMENT_DEDUC">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/EXTRAEQUIPCOLLISIONDEDUCTIBLE = '0 LIMITED'"></xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="VEHICLES/VEHICLE/EXTRAEQUIPCOLLISIONDEDUCTIBLE" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ******************************** END: DEDUCTIBLE *************************************************************-->
	<!-- Limit Factor for Limited Collision-->
	<xsl:template name="LIMITFACTOR">
		<xsl:variable name="PCOLLISIONTYPE" select="VEHICLES/VEHICLE/COLLISIONTYPE" />
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@COLLISIONTYPE = $PCOLLISIONTYPE ]/@RELATIVITY" />
	</xsl:template>
	<!-- End of Deductibles -->
	<!-- Stated Amount -->
	<xsl:template name="STATEDAMT">
1
</xsl:template>
	<!-- Fiberglass or Inner Shield -->
	<xsl:template name="FIBERGLASS">
1
</xsl:template>
	<!-- Covergae for Commercial -->
	<xsl:template name="COVERAGE">
		<xsl:variable name="PCOVERAGE" select="VEHICLES/VEHICLE/MEDPM"/>
			<xsl:choose>
				<xsl:when test="$PCOVERAGE='FULLMEDICALFULLWAGELOSS'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMMERCIAL_VEHICLE_PIP_OPTIONS']/NODE[@ID='CVPO']/ATTRIBUTES[@NAME='FULL MEDICAL AND FULL WORK LOSS']/@RELATIVITY" />
				</xsl:when>
				<xsl:when test="$PCOVERAGE='FULLMEDICALEXCESSWAGEWORKCOMP'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMMERCIAL_VEHICLE_PIP_OPTIONS']/NODE[@ID='CVPO']/ATTRIBUTES[@NAME='FULL MEDICAL AND EXCESS WORK LOSS AND/OR WORK COMP']/@RELATIVITY" />
				</xsl:when>
				<xsl:when test="$PCOVERAGE='EXCESSMEDICALFULLWAGELOSS'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMMERCIAL_VEHICLE_PIP_OPTIONS']/NODE[@ID='CVPO']/ATTRIBUTES[@NAME='EXCESS MEDICAL AND FULL WORK LOSS']/@RELATIVITY" />
				</xsl:when>
				<xsl:when test="$PCOVERAGE='EXCESSMEDICALEXCESSWAGELOSSWORKCOMP'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMMERCIAL_VEHICLE_PIP_OPTIONS']/NODE[@ID='CVPO']/ATTRIBUTES[@NAME='EXCESS MEDICAL AND EXCESS WORK LOSS AND/OR WORK COMP']/@RELATIVITY" />
				</xsl:when>
				<xsl:otherwise>
					1
				</xsl:otherwise>
			</xsl:choose>
	</xsl:template>
	<xsl:template name="PIP_DEDUCTIBLE">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/MEDPM = 'EXCESSMEDICALFULLWAGELOSS'"><xsl:text>300</xsl:text></xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/MEDPM = 'FULLMEDICALEXCESSWAGEWORKCOMP'"><xsl:text>300</xsl:text></xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/MEDPM = 'EXCESSMEDICALEXCESSWAGELOSSWORKCOMP'"><xsl:text>300</xsl:text></xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PIPDISCOUNT">
		<xsl:variable name="COVERAGE" select="VEHICLES/VEHICLE/MEDPM"/>
		<xsl:variable name="WCEXC" select="VEHICLES/VEHICLE/WCEXCCOVS"/>
			<xsl:choose>
				<xsl:when test="$WCEXC='WCINSURD'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMMERCIAL_VEHICLE_PIP_OPTIONS']/NODE[@ID='WCEXCD']/ATTRIBUTES[@NAME=$WCEXC]/@FACTOR" />
				</xsl:when>	
				<xsl:when test="$COVERAGE='EXCESSMEDICALFULLWAGELOSS' or $COVERAGE='EXCESSMEDICALEXCESSWAGELOSSWORKCOMP'">
					<xsl:choose>
						<xsl:when test="$WCEXC='CAB91'">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMMERCIAL_VEHICLE_PIP_OPTIONS']/NODE[@ID='WCEXCD']/ATTRIBUTES[@NAME=$COVERAGE]/@FACTOR" />
						</xsl:when>
						<xsl:otherwise>1</xsl:otherwise>
					</xsl:choose>
				</xsl:when>
				<xsl:otherwise>1</xsl:otherwise>	
			</xsl:choose>
	</xsl:template>
	<!-- End of templates-->
	<!-- Medical Payment -->
	<xsl:template name="MEDPAY">
		<!-- 1. GET THE territory base
			2. get class relativity
			3. get limit relativity
			4. get intermediate factor
			5. get surcharge -->
		<xsl:variable name="VAR_MPLIMIT" select="VEHICLES/VEHICLE/MPLIMIT" />
		<xsl:variable name="PTERRITORYCODE" select="VEHICLES/VEHICLE/TERRCODEGARAGEDLOCATION" />
		<xsl:variable name="PSTATE" select="POLICY/STATENAME" />
		<xsl:choose>
			<xsl:when test="$PSTATE = 'INDIANA'">
				<xsl:choose>
					<xsl:when test="$VAR_MPLIMIT ='NO COVERAGE' or $VAR_MPLIMIT ='' or $VAR_MPLIMIT='0' or normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO'">0.00</xsl:when>
					<xsl:otherwise>
						<!-- the logic should be implemented -->
						<xsl:variable name="VAR1">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID ='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE = $PTERRITORYCODE ]/@MEDBASE" />
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:call-template name="CLASS">
								<xsl:with-param name="FACTORELEMENT" select="'MEDPAY'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR3">
							<xsl:call-template name="LIMIT">
								<xsl:with-param name="FACTORELEMENT" select="'MEDPAY'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR4">
							<xsl:call-template name="INTERMEDIATEFACTOR" />
						</xsl:variable>
						<xsl:variable name="VAR5">
							<xsl:call-template name="SURCHARGE" />
						</xsl:variable>
						<xsl:value-of select="round(round(round(round($VAR1 * $VAR2) * $VAR3) * $VAR4) * $VAR5)" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="UIM">
		<xsl:choose>
			<xsl:when test="normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO'">0.00</xsl:when>
			<xsl:when test="POLICY/STATENAME = 'MICHIGAN'">0</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'UIM'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="LIMIT">
						<xsl:with-param name="FACTORELEMENT" select="'UIM'" />
					</xsl:call-template>
				</xsl:variable>
				<xsl:value-of select="round($VAR1*$VAR2)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Constant Factor -->
	<xsl:template name="CONSTANT_FACTOR">
		<xsl:param name="FACTORELEMENT" />
		<xsl:choose>
			<xsl:when test="$FACTORELEMENT = 'BI'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CONSTANTFACTOR']/NODE[@ID='CONSTANT_FACTOR']/ATTRIBUTES[@COMPONENT = $FACTORELEMENT]/@CONSTANT" />
			</xsl:when>
			<xsl:otherwise>1</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Garaged Location Text -->
	<xsl:template name="GARAGED_LOCATION_TEXT">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/ZIPCODEGARAGEDLOCATION !='' and VEHICLES/VEHICLE/ZIPCODEGARAGEDLOCATION !='0'">
			Garaged Location: <xsl:value-of select="VEHICLES/VEHICLE/GARAGEDLOCATION" />
		</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************************************* -->
	<!-- ************************************  Start of Extended Non-Owned Coverage for Named Individual(A-35) ********************************************************************************** -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="ENOLIABILITY_PREMIUM">
		<xsl:variable name="PENOCNI" select="normalize-space(VEHICLES/VEHICLE/ENOCNI)" />
		<xsl:variable name="PENOMIN" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENOLIABILITY']/NODE[@ID='ENOLBI']/@MIN_PREMIUM" />
		<xsl:variable name="PPENOPREMIUM">
			<xsl:choose>
				<xsl:when test="$PENOCNI='TRUE' and $PENOCNI!=''">
					<xsl:variable name="PCOVERAGEBI" select="VEHICLES/VEHICLE/BI" /> <!-- Limit Bi -->
					<xsl:variable name="PCOVERAGELL" select="VEHICLES/VEHICLE/BILIMIT1" /> <!-- Lower Limit Bi -->
					<xsl:variable name="PCOVERAGEUL" select="VEHICLES/VEHICLE/BILIMIT2" /> <!-- Upper Limit Bi -->
					<xsl:variable name="PCOVERAGEPD"> <!--Physical Damage Limit -->
						<xsl:choose>
							<xsl:when test="contains(VEHICLES/VEHICLE/PD,',')">
								<xsl:value-of select="VEHICLES/VEHICLE/PD" />
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="format-number(VEHICLES/VEHICLE/PD,'###,###')" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:variable>
					<xsl:choose>
						<xsl:when test="($PCOVERAGELL != '0' and $PCOVERAGELL != '') and ($PCOVERAGEUL != '0' and $PCOVERAGEUL !='')">
							<xsl:variable name="PENOBI">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENOLIABILITY']/NODE[@ID='ENOLBI']/ATTRIBUTES[@MINLIMIT=$PCOVERAGELL and @MAXLIMIT=$PCOVERAGEUL]/@BIRATES" />
							</xsl:variable>
							<xsl:variable name="PENOPD">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENOLIABILITY']/NODE[@ID='ENOLPD']/ATTRIBUTES[@PDLIMIT=$PCOVERAGEPD]/@PDRATES" />
							</xsl:variable>
							<xsl:choose>
								<xsl:when test="($PCOVERAGEBI ='' and $PCOVERAGEBI ='0')">
									0
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="round($PENOBI + $PENOPD)" />
								</xsl:otherwise>
							</xsl:choose>
						</xsl:when>
						<xsl:otherwise>
							<xsl:variable name="PCOVERAGECSL"> <!-- CSL Limit  -->
								<xsl:choose>
									<xsl:when test="contains(VEHICLES/VEHICLE/CSL,',')">
										<xsl:value-of select="VEHICLES/VEHICLE/CSL" />
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="format-number(VEHICLES/VEHICLE/CSL,'###,###')" />
									</xsl:otherwise>
								</xsl:choose>
							</xsl:variable>
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENOLIABILITY']/NODE[@ID='ENOLCSL']/ATTRIBUTES[@CSLLIMIT=$PCOVERAGECSL]/@CSLRATES" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PENOMIN &lt; $PPENOPREMIUM">
				<xsl:choose>
					<xsl:when test="$PPENOPREMIUM &gt; 0">
						<xsl:value-of select="$PPENOPREMIUM" />
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="$PPENOPREMIUM &gt; 0">
						<xsl:value-of select="$PENOMIN" />
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PDDEDUCTIBLE">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/PDDEDUCTIBLE !='0'">
				<xsl:value-of select="VEHICLES/VEHICLE/PDDEDUCTIBLE" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************************************* -->
	<!-- ************************************  End of Extended Non-Owned Coverage for Named Individual(A-35) ********************************************************************************** -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- Insurance Score Display Remark -->
	<xsl:template name="REMARKINSURANCE_SCORE_DISPLAY">
		<xsl:variable name="INS1" select="POLICY/INSURANCESCORE"></xsl:variable>
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
				<xsl:when test="$INS = ''">1.00</xsl:when>
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
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$INSDISCOUNT &lt; 1">
					<xsl:text>Discount: Insurance Score Credit (</xsl:text><xsl:call-template name="INSURENCESCOREDISPLAY" /><xsl:text>)</xsl:text>	
					</xsl:when>
			<xsl:otherwise>
					<xsl:text>Surcharge: Insurance Score Credit (</xsl:text><xsl:call-template name="INSURENCESCOREDISPLAY" /><xsl:text>)</xsl:text>	
					</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--  Template for Lables   -->
	<!--  Group Details  -->
	<xsl:template name="GROUPID0"><xsl:text>VEHICLE : </xsl:text><xsl:value-of select="VEHICLES/VEHICLE/VEHICLETYPEDESC" /></xsl:template>
	<xsl:template name="GROUPID1"><xsl:text>Final Premium</xsl:text></xsl:template>
	<xsl:template name="PRODUCTNAME"><xsl:text>Private Passenger Automobile</xsl:text></xsl:template>
	<xsl:template name="STEPID0"><xsl:value-of select="VEHICLES/VEHICLE/VEHICLEROWID" />.<xsl:value-of select="'   '" /><xsl:value-of select="VEHICLES/VEHICLE/YEAR" /> <xsl:value-of select="'   '" />  <xsl:value-of select="VEHICLES/VEHICLE/MAKE" /> <xsl:value-of select="'   '" />   <xsl:value-of select="VEHICLES/VEHICLE/MODEL" /> <xsl:value-of select="'   '" />    VIN:<xsl:value-of select="VEHICLES/VEHICLE/VIN" /></xsl:template>
	<xsl:template name="STEPID1"><xsl:text>Symbol </xsl:text><xsl:value-of select="VEHICLES/VEHICLE/SYMBOL" />,<xsl:value-of select="'   '" /><xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE  =$VT_TRAILER">
								Actual Cash Value: $<xsl:value-of select="VEHICLES/VEHICLE/COST" />
							</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLECLASS_COMM !='0'"> Class : <xsl:value-of select="VEHICLES/VEHICLE/VEHICLECLASS_COMM" /></xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose><!--xsl:value-of select="VEHICLES/VEHICLE/VEHICLECLASSCOMPONENT2" /--></xsl:template>
	<!--<xsl:template name = "STEPID2">Use: <xsl:value-of select="VEHICLES/VEHICLE/VEHICLEUSEDESC"/>  <xsl:value-of select ="'   '"/>Miles Each Way : 5</xsl:template>-->
	<xsl:template name="STEPID2"><xsl:text>Radius of Use: </xsl:text><xsl:value-of select="VEHICLES/VEHICLE/RADIUSOFUSE" />  <xsl:value-of select="'   '" /><!--Miles Each Way : 5--></xsl:template>
	<xsl:template name="STEPID3">
		<xsl:call-template name="GARAGED_LOCATION_TEXT" />
	</xsl:template>
	<xsl:template name="STEPID4"><xsl:text>Residual Bodily Injury Liability</xsl:text></xsl:template>
	<xsl:template name="STEPID5"><xsl:text>Residual Property Damage Liability</xsl:text></xsl:template>
	<!--xsl:template name="STEPID6">Residual Liability (BI and PD)</xsl:template-->
	<xsl:template name="STEPID6"><xsl:text>Single Limit Liability (CSL)</xsl:text></xsl:template>
	<xsl:template name="STEPID7"><xsl:text>Personal Injury Protection - </xsl:text><xsl:call-template name="PIP_DISPLAY" /></xsl:template>
	<xsl:template name="STEPID8"><xsl:text>Michigan Statutory Assessments</xsl:text></xsl:template>
	<xsl:template name="STEPID9"><xsl:text>Property Protection Insurance</xsl:text></xsl:template>
	<xsl:template name="STEPID10"><xsl:text>Medical Payments </xsl:text><xsl:value-of select="VEHICLES/VEHICLE/MEDPM" /> </xsl:template>
	<xsl:template name="STEPID11"><xsl:text>Uninsured Motorists</xsl:text></xsl:template>
	<xsl:template name="STEPID12"><xsl:text>Underinsured Motorists</xsl:text></xsl:template>
	<xsl:template name="STEPID13"><xsl:text>Uninsured Motorists Property Damage</xsl:text></xsl:template>
	<xsl:template name="STEPID14"><xsl:text>Damage to Your Auto - Comprehensive</xsl:text></xsl:template>
	<xsl:template name="STEPID15"><xsl:text>Damage to Your Auto - </xsl:text><xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONTYPE = 'BROAD'"><xsl:text>Broadened</xsl:text></xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONTYPE = 'LIMITED'"><xsl:text>Limited</xsl:text></xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONTYPE = 'REGULAR'"><xsl:text>Regular</xsl:text></xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose><xsl:value-of select="' '" /><xsl:text> Collision</xsl:text></xsl:template>
	<xsl:template name="STEPID16"><xsl:text>Mini-Tort PD Liability</xsl:text></xsl:template>
	<xsl:template name="STEPID17"><xsl:text>Extra Equipment - Comprehensive</xsl:text></xsl:template>
	<xsl:template name="STEPID18">
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME = 'MICHIGAN'"><xsl:text>Extra Equipment -</xsl:text><xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/EXTRAEQUIPCOLLISIONTYPE = 'BROAD'"><xsl:text>Broadened</xsl:text></xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/EXTRAEQUIPCOLLISIONTYPE = 'LIMITED'"><xsl:text>Limited</xsl:text></xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/EXTRAEQUIPCOLLISIONTYPE = 'REGULAR'"><xsl:text>Regular</xsl:text></xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose><xsl:value-of select="' '" /><xsl:text>Collision</xsl:text></xsl:when>
			<xsl:when test="POLICY/STATENAME = 'INDIANA'"><xsl:text>Extra Equipment -Collision</xsl:text></xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="STEPID19"><xsl:text>Extended Non-Owned Coverage for Named Individual</xsl:text></xsl:template>
	<xsl:template name="STEPID20"><xsl:text>Discount:Work Loss Waiver</xsl:text></xsl:template>
	<xsl:template name="STEPID21"><xsl:text>Discount:Multi-Car</xsl:text></xsl:template>
	<xsl:template name="STEPID22"><xsl:text>Discount:Multi-Policy(Auto/Home)</xsl:text></xsl:template>
	<xsl:template name="STEPID23">
		<xsl:call-template name="INSURANCE_SCORE_DISPLAY"></xsl:call-template>
	</xsl:template>
	<xsl:template name="STEPID24"><xsl:text>Surcharge:Accident and Violation - </xsl:text><xsl:call-template name="ACCIDENT_VIOLATION_SURCHRAGE_PERCENTAGE" /><xsl:text>%</xsl:text></xsl:template>
	<xsl:template name="STEPID25"><xsl:text>Total Vehicle Premium</xsl:text></xsl:template>
	<xsl:template name="PIP_DISPLAY">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/MEDPM = 'FULLMEDICALFULLWAGELOSS'"><xsl:text>Full Medical &amp; Full Wage Loss</xsl:text></xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/MEDPM = 'EXCESSMEDICALEXCESSWAGELOSSWORKCOMP'"><xsl:text>Excess Medical &amp; Excess Wage Loss &amp;/Or Work Comp</xsl:text></xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/MEDPM = 'EXCESSMEDICALFULLWAGELOSS'"><xsl:text>Excess Medical &amp; Full Wage Loss</xsl:text></xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/MEDPM = 'FULLMEDICALEXCESSWAGEWORKCOMP'"><xsl:text>Full Medical, Excess Wage &amp;/Or Work Comp</xsl:text></xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="UIMINSUREDMOTORIST">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/UIMSPLIT !='' and VEHICLES/VEHICLE/UIMSPLIT != '0/0' and VEHICLES/VEHICLE/UIMSPLIT != '0' and VEHICLES/VEHICLE/UIMSPLIT != 'NO COVERAGE'">
				<xsl:value-of select="VEHICLES/VEHICLE/UIMSPLIT" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="VEHICLES/VEHICLE/UIMCSL" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="UNINSUREDMOTORIST">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/UMSPLIT !='' and VEHICLES/VEHICLE/UMSPLIT != '0/0' and VEHICLES/VEHICLE/UMSPLIT != '0' and VEHICLES/VEHICLE/UMSPLIT != 'NO COVERAGE'">
				<xsl:value-of select="VEHICLES/VEHICLE/UMSPLIT" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="VEHICLES/VEHICLE/UMCSL" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- thing to be done -->
	<!--
1 . impelmentation of class is not correct currntly it is hardcoded 
	template name - CLASS
2) in indiana , medical payment is not implemented 
3) logic of coverage is not implemented in "COVERAGE" template
-->
	<xsl:template name="COMPONENT_CODE">
		<xsl:param name="FACTORELEMENT" />
		<xsl:choose>
			<xsl:when test="$FACTORELEMENT='BI'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
				     <xsl:text>114</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
				     <xsl:text>2</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='PD'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
				    <xsl:text>115</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
				     <xsl:text>4</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='CSL'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
				   <xsl:text>113</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
					<xsl:text>1</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='PIP'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
					<xsl:text>116</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='PPI'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
					<xsl:text>117</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='MED_PMT'">
				<xsl:if test="POLICY/STATENAME='INDIANA'">
						<xsl:text>6</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='UM'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
					<xsl:choose>
						<xsl:when test="(VEHICLES/VEHICLE/UMSPLIT !='' and VEHICLES/VEHICLE/UMSPLIT !='0/0' and VEHICLES/VEHICLE/UMSPLIT !='NO COVERAGE') ">
						<xsl:text>120</xsl:text>
				</xsl:when>
						<xsl:when test="VEHICLES/VEHICLE/UMCSL !='' and VEHICLES/VEHICLE/UMCSL !='0' and VEHICLES/VEHICLE/UMCSL !='NO COVERAGE' ">
							<xsl:text>119</xsl:text>
				</xsl:when>
					</xsl:choose>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
					<xsl:choose>
						<xsl:when test="(VEHICLES/VEHICLE/UMSPLIT !='' and VEHICLES/VEHICLE/UMSPLIT !='0/0' and VEHICLES/VEHICLE/UMSPLIT !='NO COVERAGE') ">
							<xsl:text>12</xsl:text>
						</xsl:when>
						<xsl:when test="VEHICLES/VEHICLE/UMCSL !='' and VEHICLES/VEHICLE/UMCSL !='0' and VEHICLES/VEHICLE/UMCSL !='NO COVERAGE' ">
							<xsl:text>9</xsl:text>
						</xsl:when>
					</xsl:choose>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='UIM'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
					<xsl:choose>
						<xsl:when test="(VEHICLES/VEHICLE/UMSPLIT !='' and VEHICLES/VEHICLE/UMSPLIT !='0/0' and VEHICLES/VEHICLE/UMSPLIT !='NO COVERAGE') ">
								<xsl:text>121</xsl:text>
				</xsl:when>
						<xsl:when test="VEHICLES/VEHICLE/UMCSL !='' and VEHICLES/VEHICLE/UMCSL !='0' and VEHICLES/VEHICLE/UMCSL !='NO COVERAGE' ">
								<xsl:text>304</xsl:text>
				</xsl:when>
					</xsl:choose>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
					<xsl:choose>
						<xsl:when test="(VEHICLES/VEHICLE/UMSPLIT !='' and VEHICLES/VEHICLE/UMSPLIT !='0/0' and VEHICLES/VEHICLE/UMSPLIT !='NO COVERAGE') ">
									<xsl:text>34</xsl:text>
						</xsl:when>
						<xsl:when test="VEHICLES/VEHICLE/UMCSL !='' and VEHICLES/VEHICLE/UMCSL !='0' and VEHICLES/VEHICLE/UMCSL !='NO COVERAGE' ">
							<xsl:text>14</xsl:text>
						</xsl:when>
					</xsl:choose>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='UMPD'">
				<xsl:if test="POLICY/STATENAME='INDIANA'">
						<xsl:text>36</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='COMP'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
						<xsl:text>123</xsl:text>
					</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
						<xsl:text>42</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='COLL'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
							<xsl:text>122</xsl:text>
					</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
							<xsl:text>38</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='M_TRT'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
							<xsl:text>118</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='RD_SRVC'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
							<xsl:text>124</xsl:text>
					</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
							<xsl:text>44</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='RNT_RMBRS'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
						<xsl:text>125</xsl:text>
					</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
						<xsl:text>45</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='LN_LSE'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
						<xsl:text>249</xsl:text>
					</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
						<xsl:text>46</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='SND_RPR'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
						<xsl:text>252</xsl:text>
					</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
						<xsl:text>50</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='SND_RCV'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
						<xsl:text>1030</xsl:text>
					</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
						<xsl:text>1029</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='XTR_COMP'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
						<xsl:text>301</xsl:text>
					</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
						<xsl:text>300</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='XTR_COLL'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
						<xsl:text>303</xsl:text>
					</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
						<xsl:text>302</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='XTR_COLL'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
						<xsl:text>303</xsl:text>
					</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
						<xsl:text>302</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='MCCAFEE'">
					<xsl:text>20009</xsl:text>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='ENO'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
						<xsl:text>254</xsl:text>
					</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
						<xsl:text>52</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>