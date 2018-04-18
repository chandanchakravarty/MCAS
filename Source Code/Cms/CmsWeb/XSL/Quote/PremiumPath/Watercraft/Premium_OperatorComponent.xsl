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
					<COMPONENT_TYPE></COMPONENT_TYPE>
					<COMPONENT_CODE></COMPONENT_CODE>
					<COMP_ACT_PRE></COMP_ACT_PRE>
					<COMP_REMARKS></COMP_REMARKS>
					<COMP_EXT></COMP_EXT>
					<COM_EXT_AD></COM_EXT_AD>
				</STEP> 
				
				<!-- Assigned Boat -->
				<STEP STEPID="1" CALC_ID="1001" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID1"/></xsl:attribute> 
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
		
					<!-- Points -MVR, Violation Points, Accident Points-->
				<STEP STEPID="2" CALC_ID="1002" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID2"/></xsl:attribute> 
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
<xsl:template name = "STEPID1">
		<xsl:choose>
			<xsl:when test ="OPERATORS/OPERATOR/BOATASSIGNEDASOPERATOR = '0' or OPERATORS/OPERATOR/BOATASSIGNEDASOPERATOR =''">
				Unassigned
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name ="ASSIGNED_BOATID" select="OPERATORS/OPERATOR/BOATASSIGNEDASOPERATOR"/>
				<xsl:value-of select ="OPERATORS/OPERATOR/BOATDRIVEDAS"/> Operator - Boat <xsl:value-of select ="OPERATORS/OPERATOR/BOATASSIGNEDASOPERATOR"/> (<xsl:value-of select="BOATS/BOAT[@ID=$ASSIGNED_BOATID]/BOATTYPE"/>)		
			</xsl:otherwise>
		</xsl:choose>
	
	</xsl:template>
<xsl:template name = "STEPID3">
<xsl:variable name ="ASSIGNED_BOATID" select="OPERATORS/OPERATOR/BOATASSIGNEDASOPERATOR"/>
<xsl:value-of select ="BOATS/BOAT[@ID=$ASSIGNED_BOATID]/YEAR"/>Boat <xsl:value-of select ="BOATS/BOAT[@ID=$ASSIGNED_BOATID]/MODEL"/>, Serial:<xsl:value-of select ="BOATS/BOAT[@ID=$ASSIGNED_BOATID]/SERIALNUMBER"/> <br />- <xsl:value-of select ="BOATS/BOAT[@ID=$ASSIGNED_BOATID]/LENGTH"/> Feet,<xsl:value-of select ="BOATS/BOAT[@ID=$ASSIGNED_BOATID]/WEIGHT"/> lbs,<xsl:value-of select ="BOATS/BOAT[@ID=$ASSIGNED_BOATID]/HORSEPOWER"/> HP,<xsl:value-of select ="BOATS/BOAT[@ID=$ASSIGNED_BOATID]/CAPABLESPEED"/> MPH, <xsl:value-of select ="BOATS/BOAT[@ID=$ASSIGNED_BOATID]/WATERS"/> </xsl:template>
<!--Set MVR in GETVIOLATIONPOINTS and Violations in MVR : 7 march 06 Praveen K-->
<!--Now we are using SUMOFVIOLATIONPOINTS as MVR point : 26 Apr 2007 Asfa Praveen -->
<xsl:template name = "STEPID2">Points - MVR: <xsl:call-template name="GETMVRPOINTS" /> <!--xsl:call-template name="GETVIOLATIONPOINTS" /-->, Violation: <xsl:call-template name="GETVIOLATIONPOINTS" />, Accident: <xsl:call-template name="GETACCIDENTPOINTS" /> </xsl:template>
<!--<xsl:template name = "STEPID2">Points - MVR: <xsl:value-of select="OPERATORS/OPERATOR/MVR" />,Violation: <xsl:call-template name="GETVIOLATIONPOINTS" />, Accident: <xsl:call-template name="GETACCIDENTPOINTS" /> </xsl:template>-->
<xsl:template name="GETMVRPOINTS">
	<xsl:choose>
		<xsl:when test="OPERATORS/OPERATOR/MVR &gt; 0">
			<xsl:value-of select="OPERATORS/OPERATOR/MVR" />
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>	
	</xsl:choose>
</xsl:template>
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
  

