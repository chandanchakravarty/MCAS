<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xinclude="http://www.w3.org/1999/XML/xinclude"  version="1.0">
<xsl:output encoding="ISO8859-1" omit-xml-declaration="no" method="xml"/>

<xsl:template match="/">
<xsl:apply-templates select="INPUTXML/DWELLINGDETAILS"/>
</xsl:template>

<xsl:template match="INPUTXML/DWELLINGDETAILS">
	<FACTORMASTERPATH>
		<xsl:call-template name ="FINAL_PATH"></xsl:call-template>
	</FACTORMASTERPATH>
</xsl:template>

<xsl:template name ="FINAL_PATH">
	<xsl:choose>
		<xsl:when test ="LOBNAME = 'REDW' and EDATE = '01/01/2005'" >/cmsweb/XSL/Quote/PremiumPath/RentalDwelling/Premium.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'REDW' and EDATE = '01/01/2006'" >/cmsweb/XSL/Quote/PremiumPath/RentalDwelling/Premium.xsl</xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="ddd">
0
</xsl:template>
</xsl:stylesheet>