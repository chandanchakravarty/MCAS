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
			
			<!--Group for OPERATORS-->
			<GROUP GROUPID="0">				
				<GROUPCONDITION>
					<CONDITION>
					</CONDITION>
				</GROUPCONDITION> 
				
						<!-- OPERATOR Name, Birth Date -->
						<SUBGROUP>			
							<STEP STEPID="0">
								<PATH>ND</PATH>
							</STEP>					
						</SUBGROUP>
						
						<SUBGROUP>
							<STEP STEPID="1">
								<PATH>ND</PATH>
							</STEP>
						</SUBGROUP>
						
						<!-- Points -MVR, Violation Points, Accident Points-->
						<SUBGROUP>
							<STEP STEPID="2">
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
			
			<!--Group for OPERATORS-->
			<GROUP GROUPID="0" CALC_ID="10000"> 
				<xsl:attribute name="DESC"><xsl:call-template name="GROUPID0"/></xsl:attribute> 
				<GROUPCONDITION>
					<CONDITION>
					</CONDITION>
				</GROUPCONDITION> 
				<GROUPRULE></GROUPRULE>
				<GROUPFORMULA></GROUPFORMULA>
				<GDESC></GDESC>
				
							
				<!-- OPERATOR Name, Birth Date -->
				<STEP STEPID="0" CALC_ID="1000" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID0"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				
				<!-- Assigned Boat -->
				<STEP STEPID="1" CALC_ID="1001" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID1"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
		
					<!-- Points -MVR, Violation Points, Accident Points-->
				<STEP STEPID="2" CALC_ID="1002" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID2"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 				
			</GROUP> 	
			
			
			<PRODUCTFORMULA> 
				<!--Template -->
				<GROUP0 GROUPID="10000">
					<VALUE></VALUE>					 
				</GROUP0>					
			</PRODUCTFORMULA>
		</PRODUCT>
	</CALCULATION>
</PREMIUM>
</xsl:template>
<!--  Template for Lables   -->
<!--  Group Details  -->       
<xsl:template name ="PRODUCTNAME">Watercraft</xsl:template>
<xsl:template name ="GROUPID0">Operators</xsl:template>
<xsl:template name = "STEPID0"><xsl:value-of select ="OPERATORS/OPERATOR/OPERATORFNAME"/><xsl:value-of select ="'   '"/><xsl:value-of select ="OPERATORS/OPERATOR/OPERATORLNAME"/> , <xsl:value-of select ="OPERATORS/OPERATOR/BIRTHDATE"/> (<xsl:value-of select ="OPERATORS/OPERATOR/AGEOFDRIVER"/>) </xsl:template>
<xsl:template name = "STEPID1"><xsl:value-of select ="OPERATORS/OPERATOR/BOATDRIVEDAS"/> Operator - Boat<xsl:value-of select ="OPERATORS/OPERATOR/BOATASSIGNEDASOPERATOR"/></xsl:template>
<xsl:template name = "STEPID3"><xsl:value-of select ="BOATS/BOAT/YEAR"/>Boat <xsl:value-of select ="BOATS/BOAT/MODEL"/>, Serial:<xsl:value-of select ="BOATS/BOAT/SERIALNUMBER"/> <br />- <xsl:value-of select ="BOATS/BOAT/LENGTH"/> Feet,<xsl:value-of select ="BOATS/BOAT/WEIGHT"/> lbs,<xsl:value-of select ="BOATS/BOAT/HORSEPOWER"/> HP,<xsl:value-of select ="BOATS/BOAT/CAPABLESPEED"/> MPH, <xsl:value-of select ="BOATS/BOAT/WATERS"/> </xsl:template>
<xsl:template name = "STEPID2">Points - MVR: <xsl:value-of select="OPERATORS/OPERATOR/MVR" />,Violation: <xsl:call-template name="GETVIOLATIONPOINTS" />, Accident: <xsl:call-template name="GETACCIDENTPOINTS" /> </xsl:template>
<xsl:template name="GETVIOLATIONPOINTS">
	<xsl:choose>
		<xsl:when test="OPERATORS/OPERATOR/SUMOFVIOLATIONPOINTS &gt; 0">
			<xsl:value-of select="OPERATORS/OPERATOR/SUMOFVIOLATIONPOINTS" />
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>	
	</xsl:choose>
</xsl:template>
<xsl:template name="GETACCIDENTPOINTS">
	<xsl:choose>
		<xsl:when test="OPERATORS/OPERATOR/SUMOFACCIDENTPOINTS &gt; 0">
			<xsl:value-of select="OPERATORS/OPERATOR/SUMOFACCIDENTPOINTS" />
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>	
	</xsl:choose>
</xsl:template>
</xsl:stylesheet>
  

