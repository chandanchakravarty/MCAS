<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xinclude="http://www.w3.org/1999/XML/xinclude"  version="1.0">
<xsl:output encoding="ISO8859-1" omit-xml-declaration="no" method="xml"/>

<xsl:template match="/">
<xsl:apply-templates select="INPUTXML/QUICKQUOTE"/>
</xsl:template>

<xsl:template match="INPUTXML/QUICKQUOTE">
	<FACTORMASTERPATH>
		<xsl:call-template name ="FINAL_PATH"></xsl:call-template>
	</FACTORMASTERPATH>
</xsl:template>
<!-- EDATE are in MM/DD/YYYY -->
<xsl:template name ="FINAL_PATH">
	<xsl:choose>
		<xsl:when test ="LOBNAME = 'CYCL' and EDATE = '07/01/2004' and  TYPE = 'DRIVER'">/cmsweb/XSL/Quote/PremiumPath/Motorcycle/Premium_DriverComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'CYCL' and EDATE = '07/01/2004' and  TYPE = 'VEHICLE'">/cmsweb/XSL/Quote/PremiumPath/Motorcycle/Premium_VehicleComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'CYCL' and EDATE = '07/01/2004' and  TYPE = 'VIOLATION'">/cmsweb/XSL/Quote/PremiumPath/Motorcycle/Premium_ViolationComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'CYCL' and EDATE = '07/01/2004' and  TYPE = 'MINIMUMPREMIUM'">/cmsweb/XSL/Quote/PremiumPath/Motorcycle/Minimum_PremiumComponent.xsl</xsl:when>
	
		<xsl:when test ="LOBNAME = 'CYCL' and EDATE = '07/01/2005' and  TYPE = 'DRIVER'">/cmsweb/XSL/Quote/PremiumPath/Motorcycle/Premium_DriverComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'CYCL' and EDATE = '07/01/2005' and  TYPE = 'VEHICLE'">/cmsweb/XSL/Quote/PremiumPath/Motorcycle/Premium_VehicleComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'CYCL' and EDATE = '07/01/2005' and  TYPE = 'VIOLATION'">/cmsweb/XSL/Quote/PremiumPath/Motorcycle/Premium_ViolationComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'CYCL' and EDATE = '07/01/2005' and  TYPE = 'MINIMUMPREMIUM'">/cmsweb/XSL/Quote/PremiumPath/Motorcycle/Minimum_PremiumComponent.xsl</xsl:when>
	
		<xsl:when test ="LOBNAME = 'CYCL' and EDATE = '04/01/2006' and  TYPE = 'DRIVER'">/cmsweb/XSL/Quote/PremiumPath/Motorcycle/Premium_DriverComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'CYCL' and EDATE = '04/01/2006' and  TYPE = 'VEHICLE'">/cmsweb/XSL/Quote/PremiumPath/Motorcycle/Premium_VehicleComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'CYCL' and EDATE = '04/01/2006' and  TYPE = 'VIOLATION'">/cmsweb/XSL/Quote/PremiumPath/Motorcycle/Premium_ViolationComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'CYCL' and EDATE = '04/01/2006' and  TYPE = 'MINIMUMPREMIUM'">/cmsweb/XSL/Quote/PremiumPath/Motorcycle/Minimum_PremiumComponent.xsl</xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:template>


		
		
<xsl:template name="ddd">
0
</xsl:template>
</xsl:stylesheet>
