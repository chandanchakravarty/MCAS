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
			<!--Group for Scheduled Miscellaneous Sports Equipment-->
			<GROUP GROUPID="0">				
				<GROUPCONDITION>
					<CONDITION>
					</CONDITION>
				</GROUPCONDITION> 					
				<!-- Scheduled Miscellaneous Sports Equipment -->	
					<SUBGROUP>
						<STEP STEPID="0">
							<PATH>ND</PATH>
						</STEP>					
					</SUBGROUP>	
					<SUBGROUP>
						<IF>
							<STEP STEPID="1">
								<PATH>
									{
									<xsl:call-template name="SCHEDULED_SPORTS_EQUIPMENT"/>
									}
									<!--<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID='ELECTRONIC']/ATTRIBUTES[@ID='EHOSUPP']/@DED100"></xsl:value-of>-->
								</PATH>
							</STEP>
						</IF>
					</SUBGROUP>				 						
				</GROUP>
		</PRODUCT>
	</GETPATH>
	<CALCULATION>
		<PRODUCT PRODUCTID="0" >
			<!--Product Name -->
			<xsl:attribute name="DESC"><xsl:call-template name="PRODUCTNAME"/></xsl:attribute> 
			<!--Group for Scheduled Miscellaneous Sports Equipment-->
			<GROUP GROUPID="0" CALC_ID="10000" DESC="DESCRIPTIONNOTREQUIRED"> 			
				<GROUPCONDITION>
					<CONDITION>
					</CONDITION>
				</GROUPCONDITION> 
				<GROUPRULE></GROUPRULE>
				<GROUPFORMULA></GROUPFORMULA>
				<GDESC></GDESC>
				<!-- Scheduled Miscellaneous Sports Equipment -->
				<STEP STEPID="0" CALC_ID="1000" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"></xsl:attribute> 
					<PATH>P</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				<!-- Electronic Item -->
				<STEP STEPID="1" CALC_ID="1001" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID1"/></xsl:attribute> 
					<PATH>P</PATH>
					<D_PATH><xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_DEDUCTIBLE"/></D_PATH>
					<L_PATH><xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_AMOUNT"/></L_PATH>
				</STEP> 
			</GROUP>
		</PRODUCT>		
	</CALCULATION>
</PREMIUM>
</xsl:template>
<xsl:template name ="PRODUCTNAME">Watercraft</xsl:template>
<xsl:template name ="GROUPID0">DESCRIPTIONNOTREQUIRED</xsl:template>

<xsl:template name = "STEPID0">Scheduled Miscellaneous Sports Equipment</xsl:template>
<!-- Electronic Item -->
<xsl:template name = "STEPID1">
	 <xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_ITEM" />, Serial:<xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_SERIAL_NO" />
	 <xsl:choose>
		<xsl:when test="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_ELECTRONIC = 'Y'">E</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>	
	</xsl:choose>
</xsl:template>

<!-- Electronic Item: Premium -->

<xsl:template name = "SCHEDULED_SPORTS_EQUIPMENT">
	<!-- Check whether the item is electronic or not -->
	<xsl:variable name="DED_AMOUNT" select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_DEDUCTIBLE"></xsl:variable>
	<xsl:choose>
		<xsl:when test="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_ELECTRONIC = 'Y'">
			<!-- If yes then find out the deuctible selected-->			
			<xsl:choose>
				<xsl:when test="$DED_AMOUNT = 0">
					<xsl:choose>
					<xsl:when test="POLICY/ATTACHTOWOLVERINE = 'Y' and POLICY/BOATHOMEDISC = 'Y'">
						((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID='ELECTRONIC']/ATTRIBUTES[@ID='EHOSUPP']/@DED0"></xsl:value-of>)
						* 
						(<xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_AMOUNT"></xsl:value-of>)
						DIV 100.00)
					</xsl:when>
					<xsl:otherwise>
						((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID='ELECTRONIC']/ATTRIBUTES[@ID='EBOATPOL']/@DED0"></xsl:value-of>)
						* 
						(<xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_AMOUNT"></xsl:value-of>)
						DIV 100.00)
					</xsl:otherwise>
					</xsl:choose>
				</xsl:when>
				<xsl:when test="$DED_AMOUNT = 25">
					<xsl:choose>
					<xsl:when test="POLICY/ATTACHTOWOLVERINE = 'Y' and POLICY/BOATHOMEDISC = 'Y'">
						((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID='ELECTRONIC']/ATTRIBUTES[@ID='EHOSUPP']/@DED25"></xsl:value-of>)
						* 
						(<xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_AMOUNT"></xsl:value-of>)
						DIV 100.00)
					</xsl:when>
					<xsl:otherwise>
						((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID='ELECTRONIC']/ATTRIBUTES[@ID='EBOATPOL']/@DED25"></xsl:value-of>)
						* 
						(<xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_AMOUNT"></xsl:value-of>)
						DIV 100.00)
					</xsl:otherwise>
					</xsl:choose>
				</xsl:when>				
				<xsl:when test ="$DED_AMOUNT = 50">
					<xsl:choose>
					<xsl:when test="POLICY/ATTACHTOWOLVERINE = 'Y' and POLICY/BOATHOMEDISC = 'Y'">
						((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID='ELECTRONIC']/ATTRIBUTES[@ID='EHOSUPP']/@DED50"></xsl:value-of>)
						* 
						(<xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_AMOUNT"></xsl:value-of>)
						DIV 100.00)
					</xsl:when>
					<xsl:otherwise>
						((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID='ELECTRONIC']/ATTRIBUTES[@ID='EBOATPOL']/@DED50"></xsl:value-of>)
						* 
						(<xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_AMOUNT"></xsl:value-of>)
						DIV 100.00)
					</xsl:otherwise>
					</xsl:choose>
				</xsl:when>
				<xsl:when test ="$DED_AMOUNT = 100">
					<xsl:choose>
						<xsl:when test="POLICY/ATTACHTOWOLVERINE = 'Y') and (POLICY/BOATHOMEDISC = 'Y'">							
							((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID='ELECTRONIC']/ATTRIBUTES[@ID='EHOSUPP']/@DED100"></xsl:value-of>)
							* 
							(<xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_AMOUNT"></xsl:value-of>)
							DIV 100.00)
						</xsl:when>
						<xsl:otherwise>
							((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID='ELECTRONIC']/ATTRIBUTES[@ID='EBOATPOL']/@DED100"></xsl:value-of>)
							* 
							(<xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_AMOUNT"></xsl:value-of>)
							DIV 100.00)
						</xsl:otherwise>	
					</xsl:choose>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$DED_AMOUNT"></xsl:value-of>
				</xsl:otherwise>
			</xsl:choose>			
		</xsl:when>	
		<xsl:otherwise >
			<xsl:choose>
				<xsl:when test="$DED_AMOUNT = 0">
					<xsl:choose>
						<xsl:when test="POLICY/ATTACHTOWOLVERINE = 'Y' and POLICY/BOATHOMEDISC = 'Y'">
							<!--((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID='ALLOTHER']/ATTRIBUTES[@ID='AHOSUPP']/@DED0"></xsl:value-of>)
							* 
							(<xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_AMOUNT"></xsl:value-of>)
							DIV 100.00)-->
							10.00
						</xsl:when>							
						<xsl:otherwise>
							<!--((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID='ALLOTHER']/ATTRIBUTES[@ID='ABOATPOL']/@DED0"></xsl:value-of>)
							* 
							(<xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_AMOUNT"></xsl:value-of>)
							DIV 100.00)-->
							10.00
						</xsl:otherwise>							
					</xsl:choose>
				</xsl:when>
				<xsl:when test="$DED_AMOUNT = 25">
					<xsl:choose>
						<xsl:when test="POLICY/ATTACHTOWOLVERINE = 'Y' and POLICY/BOATHOMEDISC = 'Y'">
							<!--((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID='ALLOTHER']/ATTRIBUTES[@ID='AHOSUPP']/@DED25"></xsl:value-of>
							* 
							(<xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_AMOUNT"></xsl:value-of>)
							DIV 00.00)-->
							10.00
						</xsl:when>							
						<xsl:otherwise>
							<!--((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID='ALLOTHER']/ATTRIBUTES[@ID='ABOATPOL']/@DED25"></xsl:value-of>)
							* 
							(<xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_AMOUNT"></xsl:value-of>)
							DIV 100.00)-->
							10.00
						</xsl:otherwise>							
					</xsl:choose>
				</xsl:when>				
				<xsl:when test="$DED_AMOUNT = 50">
					<xsl:choose>
						<xsl:when test="POLICY/ATTACHTOWOLVERINE = 'Y' and POLICY/BOATHOMEDISC = 'Y'">
							<!--((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID='ALLOTHER']/ATTRIBUTES[@ID='AHOSUPP']/@DED50"></xsl:value-of>)
							* 
							(<xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_AMOUNT"></xsl:value-of>)
							DIV 100.00)-->
							10.00
						</xsl:when>							
						<xsl:otherwise>
							<!--((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID='ALLOTHER']/ATTRIBUTES[@ID='ABOATPOL']/@DED50"></xsl:value-of>)
							* 
							(<xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_AMOUNT"></xsl:value-of>)
							DIV 100.00)-->
							10.00
						</xsl:otherwise>							
					</xsl:choose>
				</xsl:when>
				<xsl:when test="$DED_AMOUNT = 100">
					<xsl:choose>
						<xsl:when test="POLICY/ATTACHTOWOLVERINE = 'Y' and POLICY/BOATHOMEDISC = 'Y'">
							<!--((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID='ALLOTHER']/ATTRIBUTES[@ID='AHOSUPP']/@DED100"></xsl:value-of>
							* 
							(<xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_AMOUNT"></xsl:value-of>)
							DIV 100.00)-->
							10.00
						</xsl:when>							
						<xsl:otherwise>
							<!--((<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID='ALLOTHER']/ATTRIBUTES[@ID='ABOATPOL']/@DED100"></xsl:value-of>)
							* 
							(<xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_AMOUNT"></xsl:value-of>)
							DIV 100.00)-->
							10.00
						</xsl:otherwise>							
					</xsl:choose>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$DED_AMOUNT"></xsl:value-of>
				</xsl:otherwise>
			</xsl:choose>							
		</xsl:otherwise>		
	</xsl:choose>		
</xsl:template>
</xsl:stylesheet>