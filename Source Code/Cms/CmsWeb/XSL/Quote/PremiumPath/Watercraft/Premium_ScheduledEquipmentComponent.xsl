<?xml version="1.0" ?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:user="urn:user-namespace-here">
	<xsl:variable name="HOCoveragesDoc" select="document('FactorPath')"></xsl:variable>
	<xsl:template match="/">
		<xsl:apply-templates select="INPUTXML/QUICKQUOTE" />
	</xsl:template>
	<xsl:template match="INPUTXML/QUICKQUOTE">
		<PREMIUM>
			<CREATIONDATE></CREATIONDATE>
			<GETPATH>
				<PRODUCT PRODUCTID="0">
					<GROUP GROUPID="0">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
					</GROUP>
					<!--Group for Scheduled Miscellaneous Sports Equipment-->
					<GROUP GROUPID="1">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
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
									   <xsl:call-template name="SCHEDULED_SPORTS_EQUIPMENT" />  
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
					<!--Group for Scheduled Miscellaneous Sports Equipment-->
					<GROUP GROUPID="1" CALC_ID="10001" DESC="DESCRIPTIONNOTREQUIRED">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<GROUPRULE></GROUPRULE>
						<GROUPFORMULA></GROUPFORMULA>
						<GDESC></GDESC>
						<!-- Scheduled Miscellaneous Sports Equipment -->
						<STEP STEPID="0" CALC_ID="1000" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID0" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE></COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Electronic Item -->
						<STEP STEPID="1" CALC_ID="1001" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID1" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH>
								<xsl:variable name="DEDUCTIBLE_AMOUNT" select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_DEDUCTIBLE"></xsl:variable>
								<xsl:variable name="DED_AMOUNT" select="translate(translate($DEDUCTIBLE_AMOUNT,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')" />
								<xsl:choose>
									<xsl:when test="normalize-space($DED_AMOUNT) = 'NONE' or normalize-space($DED_AMOUNT)=''">
										0
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_DEDUCTIBLE" />
									</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_AMOUNT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>SUMTOTAL_S</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="SCHEDULED_SPORTS_EQUIPMENT" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'SUMTOTAL_S'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
					</GROUP>
				</PRODUCT>
			</CALCULATION>
		</PREMIUM>
	</xsl:template>
	<xsl:template name="PRODUCTNAME"><xsl:text>Watercraft</xsl:text></xsl:template>
	<xsl:template name="GROUPID0"><xsl:text>Scheduled Miscellaneous Sports Equipment</xsl:text></xsl:template>
	<xsl:template name="GROUPID1"></xsl:template>
	<xsl:template name="STEPID0"></xsl:template>
	<!-- Electronic Item -->
	<xsl:template name="STEPID1">
		<xsl:choose>
			<xsl:when test="normalize-space(POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_ITEM_DESC)!= ''">
				<xsl:text>Equipment :</xsl:text><xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_ITEM_NAME" /><xsl:text>,Description :</xsl:text><xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_ITEM_DESC" /><xsl:text>, Serial:</xsl:text><xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_SERIAL_NO" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:text>Equipment :</xsl:text><xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_ITEM_NAME" /><xsl:text>, Serial:</xsl:text><xsl:value-of select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_SERIAL_NO" />
			</xsl:otherwise>
		</xsl:choose>
		<xsl:choose>
			<xsl:when test="normalize-space(POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_ELECTRONIC) = 'Y'"> E</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Electronic Item: Premium -->
	<xsl:template name="SCHEDULED_SPORTS_EQUIPMENT">
		<!-- Get the Amount and deductible amount -->
		<xsl:variable name="AMOUNT" select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_AMOUNT" />
		<xsl:variable name="DEDUCTIBLE_AMOUNT" select="POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_DEDUCTIBLE"></xsl:variable>
		<xsl:variable name="DED_AMOUNT" select="translate(translate($DEDUCTIBLE_AMOUNT,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')" />
		<!-- On the basis of Electronic/Non Electronic and Attached to Home, get the node name from which the deductible factor is to be picked -->
		<xsl:variable name="NODENAME">
			<xsl:choose>
				<xsl:when test="normalize-space(POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_ELECTRONIC) = 'Y' and (POLICY/ATTACHTOWOLVERINE = 'Y' or POLICY/BOATHOMEDISC = 'Y')"><xsl:text>MISC_SPORTS_EQUIPMENT_ELECTRONIC_BOAT_HOME</xsl:text></xsl:when>
				<xsl:when test="normalize-space(POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_ELECTRONIC) = 'Y' and POLICY/ATTACHTOWOLVERINE != 'Y' and POLICY/BOATHOMEDISC != 'Y'"><xsl:text>MISC_SPORTS_EQUIPMENT_ELECTRONIC_BOAT</xsl:text></xsl:when>
				<xsl:when test="normalize-space(POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_ELECTRONIC) = 'N' and (POLICY/ATTACHTOWOLVERINE = 'Y' or POLICY/BOATHOMEDISC = 'Y')"><xsl:text>MISC_SPORTS_EQUIPMENT_ALLOTHER_BOAT_HOME</xsl:text></xsl:when>
				<xsl:when test="normalize-space(POLICY/SCHEDULEDMISCSPORTS/SCH_MISC/SCH_MISC_ELECTRONIC) = 'N' and POLICY/ATTACHTOWOLVERINE != 'Y' and POLICY/BOATHOMEDISC != 'Y'"><xsl:text>MISC_SPORTS_EQUIPMENT_ALLOTHER_BOAT</xsl:text></xsl:when>
			</xsl:choose>
		</xsl:variable>
		<!-- Fetch Rate per value -->
		<xsl:variable name="RATE_PER_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SCHEDULED_MISC_SPORTS_EQUIPMENT']/NODE[@ID=$NODENAME]/@RATE_PER_VALUE" />
		<!-- Fetch Deductible factor -->
		<xsl:variable name="DEDUCTIBLE_FACTOR">
			<xsl:choose>
				<!-- NONE OR BLANK DEDUCTIBLE TO BE TREATED AS 0 -->
				<xsl:when test="normalize-space($DED_AMOUNT) = 'NONE' or normalize-space($DED_AMOUNT)=''">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SCHEDULED_MISC_SPORTS_EQUIPMENT']/NODE[@ID=$NODENAME]/ATTRIBUTES[@DEDUCTIBLE ='0']/@FACTOR" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SCHEDULED_MISC_SPORTS_EQUIPMENT']/NODE[@ID=$NODENAME]/ATTRIBUTES[@DEDUCTIBLE =$DED_AMOUNT ]/@FACTOR" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<!-- Calculate the Premium -->
		<xsl:variable name="MIN_CALC_PREMIUM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINIMUM_CAL_PREMIUM']/NODE[@ID='CALCULATED_PREMIUM']/ATTRIBUTES[@ID ='PREMIUM']/@VALUE" />
		<xsl:variable name="CALPREMIUM" select="round(($DEDUCTIBLE_FACTOR*$AMOUNT) div $RATE_PER_VALUE)" />
		<xsl:choose>
			<xsl:when test="$CALPREMIUM &lt; $MIN_CALC_PREMIUM">
				<xsl:value-of select="$MIN_CALC_PREMIUM" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$CALPREMIUM" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="COMPONENT_CODE">
		<xsl:param name="FACTORELEMENT" />
		<xsl:choose>
			<xsl:when test="$FACTORELEMENT='SUMTOTAL_S'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
				<xsl:text>20005</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
				<xsl:text>20004</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME ='WISCONSIN'">
				<xsl:text>20006</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>