<!-- ============================================================================================ 
	File Name		:	Primium.xsl																  
	Description		:	Generate the Premium_WaterCraft_Component for the Umbrella  
	Developed By:NEERAJ SINGH 
	Date:   03 Nov.2006   											  		
 ============================================================================================ -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:user="urn:user-namespace-here">
	<xsl:output encoding="ISO8859-1" omit-xml-declaration="no" method="xml" />
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
				<PRODUCT PRODUCTID="0" DESC="WATERCRAFT EXPOSURES">
					<GROUP GROUPID="0">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<SUBGROUP>
							<IF>
								<STEP STEPID="0">
								{
								<PATH>
										<xsl:call-template name="WATERCRAFT_EXCLUSION" />
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
						<xsl:attribute name="DESC">
								ND
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
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_IN_EX</COMPONENT_CODE>
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
	<!-- ========================================================================================================= 
								Templates for Lables  (START)							  
 ============================================================================================================= -->
	<!-- ===============================   Group Details	====================================================== -->
	<xsl:template name="GROUPID0">Watercraft Exposures</xsl:template>
	<!-- ===============================   Step Details	====================================================== -->
	<xsl:template name="STEPID0"><xsl:value-of select="normalize-space(WATERCRAFT_EXPOSURES/WATERCRAFT/BOATTYPE)" /><xsl:value-of select="' '"/>(<xsl:value-of select="normalize-space(WATERCRAFT_EXPOSURES/WATERCRAFT/LENGTH)" />feet,<xsl:value-of select="' '"/><xsl:value-of select="format-number(WATERCRAFT_EXPOSURES/WATERCRAFT/RATEDSPEED,'###')" />MPH,<xsl:value-of select="' '"/><xsl:value-of select="format-number(WATERCRAFT_EXPOSURES/WATERCRAFT/HORSEPOWER,'###')" />.00HP)</xsl:template>
	<!--========================================================================================================== 
									Template for Lables  (END)								  
 ============================================================================================================== -->
	<xsl:template name="WATERCRAFT_EXCLUSION">
		<xsl:variable name="VAR_TERRITORY" select="APPLICANT_INFIORMATION/TERRITORYCODES" />
			<xsl:for-each select="WATERCRAFT_EXPOSURES/WATERCRAFT">
				<!-- add the premiums -->
				<xsl:variable name="VAR_LENTH" select="LENGTH" />
				<xsl:variable name="VAR_HORSEPOWER" select="HORSEPOWER" />
				<xsl:variable name="VAR_SPEED" select="RATEDSPEED" />
				<xsl:variable name="VAR_PREMIUM_FOR_BOAT" select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='WATERCRAFT_PREMIUMS']/NODE[@ID='PREMIUM_VALUE']/ATTRIBUTES[@TERRITORY = $VAR_TERRITORY and @SPEED_LL &lt;= $VAR_SPEED and @SPEED_UL &gt;= $VAR_SPEED and @LENGTH_LL &lt;= $VAR_LENTH and @LENGTH_UL &gt;= $VAR_LENTH and @HP_LL &lt;= $VAR_HORSEPOWER and @HP_UL &gt;= $VAR_HORSEPOWER]/@PREMIUM" />
			<xsl:choose>
				<xsl:when test="$VAR_PREMIUM_FOR_BOAT != '' and $VAR_PREMIUM_FOR_BOAT != '0'">Included</xsl:when>
				<xsl:otherwise>Excluded</xsl:otherwise>
			</xsl:choose>
		</xsl:for-each>
	</xsl:template>
	<xsl:template name="PREMIUM_FACTOR">
		<xsl:variable name="UUMBRELLALIMIT" select="UUMBRELLA_LIMIT" />
		<xsl:value-of select="$RDUmbrellaDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PREMIUM_FACTOR']/NODE[@ID='PER_PREMIUM_VALUE']/ATTRIBUTES[@UMBRELLA_LIMIT = $UUMBRELLALIMIT]/@FACTOR_VALUE" />
	</xsl:template>
</xsl:stylesheet>