<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xinclude="http://www.w3.org/1999/XML/xinclude"
	version="1.0">
	<xsl:output encoding="ISO8859-1" omit-xml-declaration="no" method="xml" />
	<xsl:variable name="HOCoveragesDoc" select="document('FactorPath')"></xsl:variable>
	<xsl:template match="/">
		<xsl:apply-templates select="INPUTXML/QUICKQUOTE" />
	</xsl:template>
	<xsl:template match="INPUTXML/QUICKQUOTE">
		<PREMIUM>
			<CREATIONDATE></CREATIONDATE>
			<GETPATH>
				<PRODUCT PRODUCTID="0">
					<!--Group for Violations/Accidents-->
					<GROUP GROUPID="0">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<!-- Accident -->
						<SUBGROUP>
							<STEP STEPID="0">
								<PATH>ND</PATH>
							</STEP>
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
					<!--Group for Violations-->
					<GROUP GROUPID="0" CALC_ID="10000" DESC="DESCRIPTIONNOTREQUIRED">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<GROUPRULE></GROUPRULE>
						<GROUPFORMULA></GROUPFORMULA>
						<GDESC></GDESC>
						<!-- Violations/Accidents -->
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
					</GROUP>
					<PRODUCTFORMULA>
						<!-- Template 
				<GROUP0 GROUPID="10000">
					<VALUE></VALUE>					 
				</GROUP0>	-->
						<GROUP1 GROUPID="10000">
							<VALUE>(@[CALC_ID=10000])</VALUE>
						</GROUP1>
					</PRODUCTFORMULA>
				</PRODUCT>
			</CALCULATION>
		</PREMIUM>
	</xsl:template>
	<!--  Template for Lables   -->
	<!--  Group Details  -->
	<xsl:template name="PRODUCTNAME">Private Passenger Automobile</xsl:template>
	<xsl:template name="STEPID0">
		<!--Modified by Asfa in order to display violation/accident points at App as at QQ rate o/p - 18-May-2007 -->
		<xsl:variable name="AMOUNT">
			<xsl:value-of select="VIOLATION/AMOUNTPAID" />
		</xsl:variable>
		<xsl:variable name="VIOLATIONID">
			<xsl:value-of select="VIOLATION/VIOLATIONID" />
		</xsl:variable>
		<xsl:variable name="VIOLATIONTYPE">
			<xsl:value-of select="VIOLATION/VIOLATIONTYPE" />
		</xsl:variable>
		<xsl:variable name="VIOLATIONAPPLICABLE">
			<xsl:value-of select="VIOLATION_APPLICABLE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$AMOUNT !='' and $VIOLATIONAPPLICABLE = 'Y'">
				<xsl:choose>
					<xsl:when test="$AMOUNT &gt;= 0">
						<xsl:choose>
							<!--15048 ACCIDENT Indiana -->
							<!--15049 ACCIDENT Michigan -->
							<xsl:when test="$VIOLATIONID = 15048 or $VIOLATIONID = 15049">
								<xsl:choose>
									<xsl:when test="$VIOLATIONTYPE !=''">
										<xsl:text>Accident: </xsl:text><xsl:value-of select="VIOLATION/VIODATE" />,<xsl:value-of select="'   '" /><xsl:text>$</xsl:text><xsl:value-of select="VIOLATION/AMOUNTPAID" /><xsl:text> Damage, </xsl:text><xsl:value-of select="VIOLATION/VIOLATIONTYPE" />
									 </xsl:when>
									<xsl:otherwise>
										<xsl:text>Accident: </xsl:text><xsl:value-of select="VIOLATION/VIODATE" />,<xsl:value-of select="'   '" /><xsl:text>$</xsl:text><xsl:value-of select="VIOLATION/AMOUNTPAID" /><xsl:text> Damage</xsl:text>
									</xsl:otherwise>
								</xsl:choose>
							</xsl:when>
							<xsl:otherwise>
								<xsl:choose>
									<xsl:when test="$VIOLATIONTYPE !=''">
										<xsl:text>Violation: </xsl:text><xsl:value-of select="VIOLATION/VIODATE" />,<xsl:value-of select="'   '" /><xsl:text>$</xsl:text><xsl:value-of select="VIOLATION/AMOUNTPAID" /><xsl:text> Damage, </xsl:text><xsl:value-of select="VIOLATION/VIOLATIONTYPE" />
									 </xsl:when>
									<xsl:otherwise>
										<xsl:text>Violation: </xsl:text><xsl:value-of select="VIOLATION/VIODATE" />,<xsl:value-of select="'   '" /><xsl:text>$</xsl:text><xsl:value-of select="VIOLATION/AMOUNTPAID" /><xsl:text> Damage</xsl:text></xsl:otherwise>
								</xsl:choose>
							</xsl:otherwise>
						</xsl:choose>
						<!--<xsl:choose>
						<xsl:when test="$AMOUNT &lt; 1000">Violation: <xsl:value-of select ="VIOLATION/VIODATE"/>,<xsl:value-of select ="'   '"/>$<xsl:value-of select ="VIOLATION/AMOUNTPAID"/> Damage, 
							<xsl:choose>
								<xsl:when test="contains(VIOLATION/VIOLATIONTYPE,'(')">
									<xsl:value-of select ="VIOLATION/VIOLATIONTYPE"/>
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select ="VIOLATION/VIOLATIONTYPE"/>(<xsl:value-of select ="VIOLATION/MVRPOINTS"/>/<xsl:value-of select ="VIOLATION/WOLVERINE_VIOLATIONS"/>)
								</xsl:otherwise>
							</xsl:choose>
						</xsl:when>
						<xsl:otherwise>Accident: <xsl:value-of select ="VIOLATION/VIODATE"/>,<xsl:value-of select ="'   '"/>$<xsl:value-of select ="VIOLATION/AMOUNTPAID"/> Damage,
							<xsl:choose>
								<xsl:when test="contains(VIOLATION/VIOLATIONTYPE,'(')">
									<xsl:value-of select ="VIOLATION/VIOLATIONTYPE"/>
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select ="VIOLATION/VIOLATIONTYPE"/>(<xsl:value-of select ="VIOLATION/MVRPOINTS"/>/<xsl:value-of select ="VIOLATION/WOLVERINE_VIOLATIONS"/>)
								</xsl:otherwise>
							</xsl:choose>
						</xsl:otherwise>
					</xsl:choose>-->
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--
				Violation: <xsl:value-of select ="VIOLATION/VIODATE"/>
				<xsl:value-of select ="''"/>,
				$<xsl:value-of select ="VIOLATION/AMOUNTPAID"/> Damage,
				<xsl:value-of select ="VIOLATION/VIOLATIONTYPE"/>
		</xsl:when>
		<xsl:when test ="$AMOUNT !='' and $AMOUNT &gt; 1000">
				Accident: <xsl:value-of select ="VIOLATION/VIODATE"/>
				<xsl:value-of select ="'   '"/>,
				$<xsl:value-of select ="VIOLATION/AMOUNTPAID"/> Damage, 
				<xsl:value-of select ="VIOLATION/VIOLATIONTYPE"/>  >
		</xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>  
</xsl:template-->
</xsl:stylesheet>
