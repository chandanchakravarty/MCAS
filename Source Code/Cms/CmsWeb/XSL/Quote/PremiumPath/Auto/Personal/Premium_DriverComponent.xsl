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
			
			<!--Group for Drivers-->
			<GROUP GROUPID="0">				
				<GROUPCONDITION>
					<CONDITION>
					</CONDITION>
				</GROUPCONDITION>  
				
						<!-- Driver Name, Birth Date --> 
						<SUBGROUP>			
							<STEP STEPID="0">
								<PATH>ND</PATH>
							</STEP>					
						</SUBGROUP>
						
						
						<!-- Marital Status and Vehicle Operated -->
						<SUBGROUP>
							<STEP STEPID="1">
								<PATH>ND</PATH>
							</STEP>			
						</SUBGROUP>
						
						<!-- College Student -->
						<SUBGROUP>
							<STEP STEPID="2"> 
								<PATH>ND</PATH>
							</STEP>			
						</SUBGROUP>			
						
						
						<!-- Points -MVR, Violation Points, Accident Points-->
						<SUBGROUP>
							<STEP STEPID="3">
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
			
			<!--Group for Drivers-->
			<GROUP GROUPID="0" CALC_ID="10000"> 
				<xsl:attribute name="DESC"><xsl:call-template name="GROUPID0"/></xsl:attribute> 
				<GROUPCONDITION>
					<CONDITION>
					</CONDITION>
				</GROUPCONDITION> 
				<GROUPRULE></GROUPRULE>
				<GROUPFORMULA></GROUPFORMULA>
				<GDESC></GDESC>
				
							
				<!-- Driver Name, Birth Date -->
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
				
				<!-- Marital Status and Vehicle Operated -->
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
				
				<!-- College Student -->
				<STEP STEPID="2" CALC_ID="1002"  PREFIX="" SUFIX ="" ISCALCULATE="F">
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
				
				<!-- Points -MVR, Violation Points, Accident Points-->
				<STEP STEPID="3" CALC_ID="1003" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID3"/></xsl:attribute> 
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
<xsl:template name ="GROUPID0">DRIVER : <xsl:value-of select ="DRIVERS/DRIVER/DRIVERFNAME"/></xsl:template>
<xsl:template name ="PRODUCTNAME">Private Passenger Automobile</xsl:template>
<xsl:template name = "STEPID0"><xsl:value-of select ="DRIVERS/DRIVER/DRIVERFNAME"/><xsl:value-of select ="'   '"/><xsl:value-of select ="DRIVERS/DRIVER/DRIVERLNAME"/> , <xsl:value-of select ="DRIVERS/DRIVER/BIRTHDATE"/> (<xsl:value-of select ="DRIVERS/DRIVER/AGEOFDRIVER"/>) </xsl:template>
<!-- <xsl:template name = "STEPID1"><xsl:value-of select ="DRIVERS/DRIVER/MARITALSTATUS"/><xsl:value-of select ="'   '"/><xsl:value-of select ="DRIVERS/DRIVER/GENDER"/>, Operator - <xsl:value-of select ="'   '"/> <xsl:value-of select ="DRIVERS/DRIVER/VEHICLEDRIVEDAS"/>, Auto <xsl:call-template name="GETVEHICLEFROMDRIVER"><xsl:with-param name="PDRIVERID" select="DRIVERS/DRIVER/ID"></xsl:with-param></xsl:call-template> </xsl:template> -->

<xsl:template name = "STEPID1"><xsl:value-of select ="DRIVERS/DRIVER/MARITALSTATUS"/><xsl:value-of select ="'   '"/><xsl:value-of select ="DRIVERS/DRIVER/GENDER"/>, Operator - <xsl:value-of select ="'   '"/> <xsl:value-of select ="DRIVERS/DRIVER/VEHICLEDRIVEDAS"/>, Vehicle <xsl:value-of select="DRIVERS/DRIVER/VEHICLEASSIGNEDASOPERATOR"/> </xsl:template>


<xsl:template name = "STEPID2">

	<xsl:choose>
		<xsl:when test="DRIVERS/DRIVER/COLLEGESTUDENT ='TRUE'">College Student</xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:template>
 
<xsl:template name = "STEPID3">Points - MVR: ,
							   Violation: <xsl:call-template name="GETVIOLATIONPOINTS" />, Accident: <xsl:call-template name="GETACCIDENTPOINTS" /> </xsl:template>



<xsl:template name="GETVIOLATIONPOINTS">
	<xsl:choose>
		<xsl:when test="DRIVERS/DRIVER/SUMOFVIOLATIONPOINTS &gt; 0">
			<xsl:value-of select="DRIVERS/DRIVER/SUMOFVIOLATIONPOINTS" />
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>	
	</xsl:choose>
</xsl:template>
<xsl:template name="GETACCIDENTPOINTS">
	<xsl:choose>
		<xsl:when test="DRIVERS/DRIVER/SUMOFACCIDENTPOINTS &gt; 0">
			<xsl:value-of select="DRIVERS/DRIVER/SUMOFACCIDENTPOINTS" />
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>	
	</xsl:choose>
</xsl:template>

</xsl:stylesheet>
  

