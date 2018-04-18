<!-- ============================================================================================ -->
<!-- File Name		:	Premium_PolicyComponent.xsl																  -->
<!-- Description	:	This xsl file is for generating the Adjusted
						Minimum Primium For Scheduled Equipment and Policy Total Premium		  -->
<!-- Developed By	:	Asfa Praveen															  -->
<!-- Date			:	19-June-2007															  -->
<!-- ============================================================================================ -->

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
	<xsl:variable name="RDWatercraftDoc" select="document('FactorPath')"></xsl:variable>
	<!-- ============================================================================================ 
								Loading ProductFactorMaster File (END)						  
	 ============================================================================================ -->
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
										<xsl:call-template name="SCH_MINIMUM_PREMIUM_DIFFERENCE" />
									</PATH>
								}
								</STEP>
							</IF>
						</SUBGROUP>
						<SUBGROUP>
							<STEP STEPID="1">
								<PATH>
									<xsl:variable name="PMINIMUMPREMIUM">
										<xsl:call-template name="ADJUSTED_MINIMUM_PREMIUM" />
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
								<STEP STEPID="2">
								{
									<PATH>
										<xsl:call-template name="ADJUSTED_MINIMUM_PREMIUM" />
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
						<!--Minimum Premium -->
						<STEP STEPID="0" CALC_ID="1000" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">DESCRIPTIONNOTREQUIRED</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MINIMUMPREMIUM_SCH</COMPONENT_CODE>
							<COMP_ACT_PRE><xsl:call-template name="SCH_MINIMUM_PREMIUM_DIFFERENCE" /></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<STEP STEPID="1" CALC_ID="1001" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID1" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MINIMUMPREMIUM</COMPONENT_CODE>
							<COMP_ACT_PRE><xsl:call-template name="ADJUSTED_MINIMUM_PREMIUM" /></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<STEP STEPID="2" CALC_ID="1002" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">DESCRIPTIONNOTREQUIRED</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MINIMUMPRE</COMPONENT_CODE>
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
	<!-- STEP DESCRIPTION  -->
	<xsl:template name="STEPID0">Minimum Premium Adjustment for Scheduled Miscellaneous Equipment</xsl:template>
	<xsl:template name="STEPID1">Minimum Premium</xsl:template>
	<!-- MINIMUM PREMIUM FOR SCHEDULED EQUIPMENT -->
	<xsl:template name="SCH_MINIMUM_PREMIUM_DIFFERENCE">
		<xsl:variable name="VAR_SUMOFSCH" select="user:GetCal(-1)" />
		<xsl:variable name="VAR_SCH">
			<xsl:variable name="VAR_SCH_MSE">
				<xsl:for-each select="RISK[@TYPE='MSE']/STEP">
					<xsl:choose>
						<xsl:when test="contains(@COMPONENT_CODE,'SUMTOTAL')">
							<xsl:variable name="total" select="@STEPPREMIUM" />
							<xsl:if test="$total &gt; 0">
								<xsl:value-of select="user:GetCal(string($total))" />
							</xsl:if>
						</xsl:when>
						<xsl:otherwise>0</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</xsl:variable>
			<xsl:variable name="VAR_SUMOFSCHMSE" select="user:GetCal(0)" />
			<xsl:value-of select="$VAR_SUMOFSCHMSE" />
		</xsl:variable>
		<xsl:variable name="VAR_MIN_PREMIUM" select="$RDWatercraftDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SCHEDULED_MISC_SPORTS_EQUIPMENT']/@MIN_PREMIUM" />
		<xsl:choose>
			<xsl:when test="$VAR_SCH &lt; $VAR_MIN_PREMIUM and $VAR_SCH &gt; 0">
				<xsl:value-of select="$VAR_MIN_PREMIUM - $VAR_SCH" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--ADJUSTED MINIMUM PREMIUM POLICY LEVEL-->
	<xsl:template name="ADJUSTED_MINIMUM_PREMIUM">
		<xsl:variable name="VAR_MIN_PREMIUM">
			<xsl:call-template name="MINIMUM_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_BOAT_COUNT">
			<xsl:call-template name="BOAT_COUNT" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$VAR_BOAT_COUNT &gt; 1 and $VAR_MIN_PREMIUM &gt; 0">
				<xsl:value-of select="round($VAR_MIN_PREMIUM div $VAR_BOAT_COUNT)" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$VAR_MIN_PREMIUM" />
			</xsl:otherwise>
		</xsl:choose>		
	</xsl:template>
	<xsl:template name="MINIMUM_PREMIUM">
		<!-- Run a loop for each Risk. -->
		<xsl:variable name="VAR_MINIMUM_PREMIUM">
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
		</xsl:variable>
		<xsl:variable name="VAR_MIN_PREMIUM" select="$RDWatercraftDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINIMUM_BOAT_PREMIUM']/NODE[@ID ='MINIMUM_PREMIUM']/ATTRIBUTES/@MINIMUM_VALUE" />
		<xsl:variable name="VAR_SCH_MINIMUM_PREMIUM">
			<xsl:call-template name="SCH_MINIMUM_PREMIUM_DIFFERENCE" />
		</xsl:variable>
		<xsl:variable name="TOTAL_PREMIUM">
			<xsl:value-of select="$VAR_MINIMUM_PREMIUM + $VAR_SCH_MINIMUM_PREMIUM" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$TOTAL_PREMIUM &lt; $VAR_MIN_PREMIUM">
				<xsl:value-of select="($VAR_MIN_PREMIUM - $TOTAL_PREMIUM)" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="BOAT_COUNT">
		<xsl:variable name="COUNT_BOAT" select="user:GetCount(-1)" />
		<xsl:variable name="TOTAL_BOAT">
		<xsl:for-each select="RISK/STEP">
		<xsl:choose>
			<xsl:when test="contains(@GROUPDESC,'Boat')">
					<xsl:variable name="BOAT" select="user:GetCount(1)" />
					<xsl:value-of select="user:GetCal(string($BOAT))" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
			</xsl:for-each>
		</xsl:variable>
		<xsl:variable name="TOTAL_MOTOR" select="user:GetCount(0)" />
		<xsl:value-of select="$TOTAL_MOTOR" />
	</xsl:template>
</xsl:stylesheet>
