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
			
			<!--Group for Violations/Accidents-->
			<GROUP GROUPID="0">				
				<GROUPCONDITION>
					<CONDITION>
					</CONDITION>
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
		<PRODUCT PRODUCTID="0" >
			
			<!--Product Name -->
			<xsl:attribute name="DESC"><xsl:call-template name="PRODUCTNAME"/></xsl:attribute> 
			
			<!--Group for Violations-->
			<GROUP GROUPID="0" CALC_ID="10000" DESC="Violations"> 				 
				<GROUPCONDITION>
					<CONDITION>
					</CONDITION>
				</GROUPCONDITION> 
				<GROUPRULE></GROUPRULE>
				<GROUPFORMULA></GROUPFORMULA>
				<GDESC></GDESC>
				
				<!-- Violations/Accidents -->
				<STEP STEPID="0" CALC_ID="1000" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID0"/></xsl:attribute> 
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

<xsl:template name ="PRODUCTNAME">Watercraft</xsl:template>
<xsl:template name = "HELLO"> Hello -<xsl:value-of select="VIOLATION/AMOUNTPAID"></xsl:value-of></xsl:template>
<xsl:template name = "STEPID0">
	<xsl:variable name="AMOUNT"><xsl:value-of select="VIOLATION/AMOUNTPAID"/></xsl:variable>	
	<xsl:choose>
		<xsl:when test ="$AMOUNT !=''">
			<xsl:choose>	
				<xsl:when test ="$AMOUNT &gt;= 0">
					<xsl:choose>
						<xsl:when test="$AMOUNT &lt; 1000">Violation: <xsl:value-of select ="VIOLATION/VIODATE"/><xsl:value-of select ="'   '"/>,$<xsl:value-of select ="VIOLATION/AMOUNTPAID"/> Damage, 
							<xsl:choose>
								<xsl:when test="contains(VIOLATION/VIOLATIONTYPE,'(')">
									<xsl:value-of select ="VIOLATION/VIOLATIONTYPE"/>
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select ="VIOLATION/VIOLATIONTYPE"/>(<xsl:value-of select ="VIOLATION/MVRPOINTS"/>/<xsl:value-of select ="VIOLATION/WOLVERINE_VIOLATIONS"/>)
								</xsl:otherwise>
							</xsl:choose>
						</xsl:when>
						<xsl:otherwise>Accident: <xsl:value-of select ="VIOLATION/VIODATE"/><xsl:value-of select ="'   '"/>,$<xsl:value-of select ="VIOLATION/AMOUNTPAID"/> Damage,
							<xsl:choose>
								<xsl:when test="contains(VIOLATION/VIOLATIONTYPE,'(')">
									<xsl:value-of select ="VIOLATION/VIOLATIONTYPE"/>
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select ="VIOLATION/VIOLATIONTYPE"/>(<xsl:value-of select ="VIOLATION/MVRPOINTS"/>/<xsl:value-of select ="VIOLATION/WOLVERINE_VIOLATIONS"/>)
								</xsl:otherwise>
							</xsl:choose>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:when>
				<xsl:otherwise>		
				</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:template>
</xsl:stylesheet>
  

