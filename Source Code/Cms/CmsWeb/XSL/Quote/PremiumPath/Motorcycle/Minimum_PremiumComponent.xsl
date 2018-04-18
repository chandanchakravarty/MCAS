<?xml version="1.0" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:user="urn:user-namespace-here">
	<msxsl:script language="c#" implements-prefix="user">
<![CDATA[
		int Premium=0,CountMotor=0;
		public int GetCal(int StepPremiumValue)
		{
		
			int val=0;
			if(StepPremiumValue < 0)
			{
				Premium=0;	
			}
			else
			{
				val=StepPremiumValue;
				Premium = Premium + val;
			}
			return Premium;
		}
		public int GetCount(int CountBoat)
		{
			int val=0;
			if(CountBoat < 0)
			{
				CountMotor=0;	
			}
			else
			{
				val=CountBoat;
				CountMotor = CountMotor + val;
			}
			return CountMotor;
			
		}
]]></msxsl:script>
	<!-- ============================================================================================ 
								Loading ProductFactorMaster File and other variables (START)					  
 ============================================================================================ -->
	<xsl:variable name="RDMotorcycleDoc" select="document('FactorPath')"></xsl:variable>
	<!-- ============================================================================================ 
								Loading ProductFactorMaster File (END)						  
 ============================================================================================ -->
	<xsl:template match="/">
		<xsl:apply-templates select="PRIMIUM" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--								MOTORCYCLE Template(START)								  -->
	<!-- ============================================================================================ -->
	<xsl:template match="PRIMIUM">
		<PREMIUM>
			<CREATIONDATE></CREATIONDATE>
			<GETPATH>
				<PRODUCT PRODUCTID="0">
					<GROUP GROUPID="0">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<SUBGROUP>
							<IF>
								<STEP STEPID="0">
								{
									<PATH>
										<xsl:call-template name="BI_MINIMUM_PREMIUM" />
									</PATH>
								}
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="1">
								{
									<PATH>
										<xsl:call-template name="PD_MINIMUM_PREMIUM" />
									</PATH>
								}
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="2">
								{
									<PATH>
										<xsl:call-template name="CSL_MINIMUM_PREMIUM" />
									</PATH>
								}
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<STEP STEPID="3">
								<PATH>
									<xsl:variable name="PMINIMUMPREMIUM">
										<xsl:call-template name="MINIMUMPREMIUM" />
									</xsl:variable>
									<xsl:choose>
										<xsl:when test="$PMINIMUMPREMIUM &gt; 0">Included</xsl:when>
										<xsl:otherwise>0</xsl:otherwise>
									</xsl:choose>
								</PATH>
							</STEP>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="4">
								{
									<PATH>
										<xsl:call-template name="MINIMUMPREMIUM" />
									</PATH>
								}
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<IF>
								<STEP STEPID="5">
								{
									<PATH>
										<xsl:call-template name="FINAL_PREMIUM" />
									</PATH>
								}
								</STEP>
							</IF>
						</SUBGROUP>
					</GROUP>
				</PRODUCT>
			</GETPATH>
			<CALCULATION>
				<PRODUCT PRODUCTID="0">
					<xsl:attribute name="DESC" />
					<GROUP GROUPID="0" CALC_ID="10000" PREFIX="" SUFIX="" ISCALCULATE="F">
						<xsl:attribute name="DESC">DESCRIPTIONNOTREQUIRED</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION />
						</GROUPCONDITION>
						<GROUPRULE />
						<GROUPFORMULA />
						<GDESC />
						<!--Underlying Policy Limits  -->
						<STEP STEPID="0" CALC_ID="1000" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">DESCRIPTIONNOTREQUIRED</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BI_MINMUM</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="BI_MINIMUM_PREMIUM" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<STEP STEPID="1" CALC_ID="1001" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">DESCRIPTIONNOTREQUIRED</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>PD_MINMUM</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="PD_MINIMUM_PREMIUM" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<STEP STEPID="2" CALC_ID="1002" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">DESCRIPTIONNOTREQUIRED</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>CSL_MINMUM</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="CSL_MINIMUM_PREMIUM" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--Minimum Premium -->
						<STEP STEPID="3" CALC_ID="1003" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID0" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MINIMUMPREMIUM</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<STEP STEPID="4" CALC_ID="1004" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">DESCRIPTIONNOTREQUIRED</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MINIMUMPRE</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="MINIMUMPREMIUM" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--Final Minimum Premium -->
						<STEP STEPID="5" CALC_ID="1005" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">DESCRIPTIONNOTREQUIRED</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>FINAL_PREMIUM</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
					</GROUP>
				</PRODUCT>
			</CALCULATION>
		</PREMIUM>
	</xsl:template>
	<!-- =========================================================================================================== 
								STEPDETAILS Template(END)								  
 ============================================================================================================== -->
	<xsl:template name="MINIMUMPREMIUM">
		<xsl:variable name="PREMIUMDIFFERENCE">
			<xsl:call-template name="PREMIUM_DIFFERENCE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PREMIUMDIFFERENCE &gt; 0">
				<xsl:value-of select="$PREMIUMDIFFERENCE" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Calculate Total Number Of MotorCycle -->
	<xsl:template name="RISK_COUNT">
		<xsl:variable name="COUNT_VEHICLE" select="user:GetCount(-1)" />
		<xsl:for-each select="RISK">
			<xsl:choose>
				<xsl:when test="STEP[@TS='0']/@GROUPDESC='MOTORCYCLES (COMPREHENSIVE ONLY)'">
					<xsl:variable name="MOTORCYCLE" select="user:GetCount(0)" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:variable name="MOTORCYCLE" select="user:GetCount(1)" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:for-each>
		<xsl:variable name="TOTAL_MOTOR" select="user:GetCount(0)" />
		<xsl:value-of select="$TOTAL_MOTOR" />
	</xsl:template>
	<!-- Calculating BI factor for minimum premium  -->
	<xsl:template name="BI_MINIMUM_PREMIUM">
		<xsl:variable name="BI_FACTOR" select="$RDMotorcycleDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINIMUM_CYCLE_PREMIUM']/NODE[@ID ='MINIMUM_PREMIUM_FACTOR']/ATTRIBUTES[@COVERAGE_TYPE='BI']/@FACTOR" />
		<xsl:variable name="PMINIMUM_PREMIUM">
			<xsl:call-template name="MINIMUMPREMIUM" />
		</xsl:variable>
		<xsl:variable name="PCOUNTVEHILCE">
			<xsl:call-template name="RISK_COUNT" />
		</xsl:variable>
		<xsl:variable name="total">
			<xsl:for-each select="RISK/STEP">
				<xsl:choose>
					<xsl:when test="contains(@COMPONENT_CODE,'BI')">
						<xsl:value-of select="@STEPPREMIUM" />
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:for-each>
		</xsl:variable>
		<xsl:if test="$total &gt; 0">
			<xsl:value-of select="round(($PMINIMUM_PREMIUM div $PCOUNTVEHILCE)* $BI_FACTOR)" />
		</xsl:if>
	</xsl:template>
	<!-- Calculating PD factor for minimum premium -->
	<xsl:template name="PD_MINIMUM_PREMIUM">
		<xsl:variable name="PD_FACTOR" select="$RDMotorcycleDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINIMUM_CYCLE_PREMIUM']/NODE[@ID ='MINIMUM_PREMIUM_FACTOR']/ATTRIBUTES[@COVERAGE_TYPE='PD']/@FACTOR" />
		<xsl:variable name="PMINIMUM_PREMIUM">
			<xsl:call-template name="MINIMUMPREMIUM" />
		</xsl:variable>
		<xsl:variable name="PCOUNTVEHILCE">
			<xsl:call-template name="RISK_COUNT" />
		</xsl:variable>
		<xsl:variable name="total">
			<xsl:for-each select="RISK/STEP">
				<xsl:choose>
					<xsl:when test="contains(@COMPONENT_CODE,'PD')">
						<xsl:value-of select="@STEPPREMIUM" />
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:for-each>
		</xsl:variable>
		<xsl:if test="$total &gt; 0">
			<xsl:value-of select="round(($PMINIMUM_PREMIUM div $PCOUNTVEHILCE)* $PD_FACTOR)" />
		</xsl:if>
	</xsl:template>
	<!-- Calculating CSL factor for minimum premium -->
	<xsl:template name="CSL_MINIMUM_PREMIUM">
		<xsl:variable name="CSL_FACTOR" select="$RDMotorcycleDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINIMUM_CYCLE_PREMIUM']/NODE[@ID ='MINIMUM_PREMIUM_FACTOR']/ATTRIBUTES[@COVERAGE_TYPE='CSL']/@FACTOR" />
		<xsl:variable name="PMINIMUM_PREMIUM">
			<xsl:call-template name="MINIMUMPREMIUM" />
		</xsl:variable>
		<xsl:variable name="PCOUNTVEHILCE">
			<xsl:call-template name="RISK_COUNT" />
		</xsl:variable>
		<xsl:variable name="total">
			<xsl:for-each select="RISK/STEP">
				<xsl:choose>
					<xsl:when test="contains(@COMPONENT_CODE,'CSL')">
						<xsl:value-of select="@STEPPREMIUM" />
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:for-each>
		</xsl:variable>
		<xsl:if test="$total &gt; 0">
			<xsl:value-of select="round(($PMINIMUM_PREMIUM div $PCOUNTVEHILCE)* $CSL_FACTOR)" />
		</xsl:if>
	</xsl:template>
	<xsl:template name="PREMIUM_DIFFERENCE">
		<xsl:variable name="POLICYTERM" select="RISK/CLIENT_TOP_INFO/@APP_TERMS" />
		<!-- Fetch the minimum premium required.
		 -->
		<xsl:variable name="MINIMUM_FINAL_PREMIUM" select="$RDMotorcycleDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINIMUM_CYCLE_PREMIUM']/NODE[@ID ='MINIMUM_PREMIUM']/ATTRIBUTES[@MIN_MONTH &lt;= $POLICYTERM and @MAX_MONTH &gt;= $POLICYTERM]/@MIN_PREMIUM" />
		<!-- Check the final premium value -->
		<xsl:variable name="P_TEMP_FINAL_PREMIUM">
			<!-- Final Premium -->
			<xsl:variable name="VAR_SUMOFMCCAFEE" select="user:GetCal(-1)" />
			<xsl:variable name="VAR_MCCA">
				<xsl:variable name="VAR_MCCAFEE">
					<xsl:for-each select="RISK/STEP">
						<xsl:choose>
							<xsl:when test="contains(@COMPONENT_CODE,'MCCAFEE')">
								<xsl:variable name="total" select="@STEPPREMIUM" />
								<xsl:if test="$total &gt; 0">
									<xsl:value-of select="user:GetCal(string($total))" />
								</xsl:if>
							</xsl:when>
							<xsl:otherwise>0</xsl:otherwise>
						</xsl:choose>
					</xsl:for-each>
				</xsl:variable>
				<xsl:variable name="VAR_SUMOFMCCAFEES" select="user:GetCal(0)" />
				<xsl:value-of select="$VAR_SUMOFMCCAFEES" />
			</xsl:variable>
			<xsl:variable name="TOTALSUM">
				<xsl:call-template name="TOTAL_PREMIUM" />
			</xsl:variable>
			<xsl:variable name="MCCA">
				<xsl:value-of select="$VAR_MCCA" />
			</xsl:variable>
			<xsl:variable name="VAR1">
				<xsl:choose>
					<xsl:when test="$VAR_MCCA !=0 or $VAR_MCCA !=''">
						<xsl:value-of select="$TOTALSUM - $MCCA" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:call-template name="TOTAL_PREMIUM" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:variable>
			<xsl:value-of select="$VAR1" />
		</xsl:variable>
		<xsl:value-of select="$MINIMUM_FINAL_PREMIUM - $P_TEMP_FINAL_PREMIUM" />
	</xsl:template>
	<xsl:template name="TOTAL_PREMIUM">
		<!-- Run a loop for each Risk. -->
		<xsl:variable name="VAR_SUMOFPREMIUMS" select="user:GetCal(-1)" />
		<xsl:variable name="var_TOTAL_PREMIUM">
			<xsl:for-each select="RISK/STEP">
				<xsl:choose>
					<xsl:when test="contains(@COMPONENT_CODE,normalize-space('SUMTOTAL'))">
						<xsl:variable name="total" select="@STEPPREMIUM" />
						<xsl:value-of select="user:GetCal(string($total))" />
					</xsl:when>
				</xsl:choose>
			</xsl:for-each>
		</xsl:variable>
		<xsl:variable name="VAR_SUMOFPREMIUM" select="user:GetCal(0)" />
		<xsl:value-of select="$VAR_SUMOFPREMIUM" />
	</xsl:template>
	<xsl:template name="FINAL_PREMIUM">
		<xsl:variable name="var_TOTAL_PREMIUM">
			<xsl:call-template name="TOTAL_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="var_PREMIUM_DIFFERENCE">
			<xsl:call-template name="PREMIUM_DIFFERENCE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$var_PREMIUM_DIFFERENCE &gt;= 0">
				<xsl:value-of select="$var_PREMIUM_DIFFERENCE + $var_TOTAL_PREMIUM " />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$var_TOTAL_PREMIUM" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="STEPID0">Minimum Premium</xsl:template>
	<xsl:template name="STEPID1">Total Premium</xsl:template>
</xsl:stylesheet>