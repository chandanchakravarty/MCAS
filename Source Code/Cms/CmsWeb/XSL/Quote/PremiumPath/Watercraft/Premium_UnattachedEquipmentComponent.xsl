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
					<!--<GROUP GROUPID="0">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
					</GROUP>-->
					<!--Group for Unattached Equipment-->
					<GROUP GROUPID="0">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<!-- Unattached Equipment -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="0">
								{
								<PATH>
										<xsl:call-template name="UNATTACHED_PREMIUM" />
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
										<xsl:call-template name="UNATTACHED_PREMIUM_VALUE" />
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
					<!--Product Name -->
					<xsl:attribute name="DESC">
						<xsl:call-template name="PRODUCTNAME" />
					</xsl:attribute>
					<!--<GROUP GROUPID="0" CALC_ID="10000">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID0"></xsl:call-template>
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<GROUPRULE></GROUPRULE>
						<GROUPFORMULA></GROUPFORMULA>
						<GDESC></GDESC>
					</GROUP>-->
					<!--Group for Scheduled Miscellaneous Sports Equipment-->
					<GROUP GROUPID="1" CALC_ID="10000" DESC="DESCRIPTIONNOTREQUIRED">
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
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="BOAT/UNATTACHEDEQUIPMENT_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="BOAT/UNATTACHEDEQUIPMENT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_UNATTACH_INCLUDE</COMPONENT_CODE>
							<COMP_ACT_PRE><xsl:call-template name="UNATTACHED_PREMIUM" /></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'BOAT_UNATTACH_INCLUDE'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Electronic Item -->
						<STEP STEPID="1" CALC_ID="1001" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID1" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="BOAT/UNATTACHEDEQUIPMENT_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="BOAT/UNATTACHEDEQUIPMENT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BOAT_UNATTACH_PREMIUM</COMPONENT_CODE>
							<COMP_ACT_PRE><xsl:call-template name="UNATTACHED_PREMIUM_VALUE" /></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'BOAT_UNATTACH_PREMIUM'"></xsl:with-param>
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
	<xsl:template name="GROUPID0"><xsl:text>Unattached Equipment and Personal Effects(unscheduled)- Actual Cash Basis</xsl:text></xsl:template>
	<xsl:template name="GROUPID1"></xsl:template>
	<xsl:template name="STEPID0"><xsl:text>Unattached Equipment and Personal Effects(unscheduled)- Actual Cash Basis</xsl:text></xsl:template>
	<xsl:template name="STEPID1"><xsl:text>Unattached Equipment and Personal Effects(unscheduled)- Actual Cash Basis</xsl:text></xsl:template>
	<!-- Electronic Item: Premium -->
	<xsl:template name="UNATTACHED_PREMIUM_VALUE">
		<xsl:variable name="P_UNATTACHEDEQUIPMENT">
			<xsl:value-of select="BOAT/UNATTACHEDEQUIPMENT" />
		</xsl:variable>
		<xsl:variable name="P_UNATTACHEDEQUIPMENT_VALUE" select="translate($P_UNATTACHEDEQUIPMENT,'$','')" />
		<xsl:variable name="VAR_MINIMUM">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID ='UNATTACHEDEQUIPMENT']/ATTRIBUTES/@MINIMUM" />
		</xsl:variable>
		<xsl:variable name="VAR_RATE_PER_VALUE">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID ='UNATTACHEDEQUIPMENT']/ATTRIBUTES/@RATE_PER_VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR_RATE">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID ='UNATTACHEDEQUIPMENT']/ATTRIBUTES/@RATE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$P_UNATTACHEDEQUIPMENT_VALUE = $VAR_MINIMUM ">0</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="round(round(round($P_UNATTACHEDEQUIPMENT_VALUE - $VAR_MINIMUM) div $VAR_RATE_PER_VALUE) * $VAR_RATE)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="UNATTACHED_PREMIUM">
		<xsl:variable name="P_UNATTACHEDEQUIPMENT">
			<xsl:value-of select="BOAT/UNATTACHEDEQUIPMENT" />
		</xsl:variable>
		<xsl:variable name="P_UNATTACHEDEQUIPMENT_VALUE" select="translate($P_UNATTACHEDEQUIPMENT,'$','')" />
		<xsl:variable name="VAR_MINIMUM">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID ='UNATTACHEDEQUIPMENT']/ATTRIBUTES/@MINIMUM" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$P_UNATTACHEDEQUIPMENT_VALUE =$VAR_MINIMUM"><xsl:text>Included</xsl:text></xsl:when>
			<xsl:otherwise>			
			0		
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="COMPONENT_CODE">
		<xsl:param name="FACTORELEMENT" />
		<xsl:choose>
		<xsl:when test="$FACTORELEMENT='BOAT_UNATTACH_INCLUDE'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
				<xsl:text>71</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
				<xsl:text>26</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME ='WISCONSIN'">
				<xsl:text>823</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='BOAT_UNATTACH_PREMIUM'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
				<xsl:text>71</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
				<xsl:text>26</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME ='WISCONSIN'">
				<xsl:text>823</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>
