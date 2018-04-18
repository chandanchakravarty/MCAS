<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xinclude="http://www.w3.org/1999/XML/xinclude"  version="1.0">
<xsl:output encoding="ISO8859-1" omit-xml-declaration="no" method="xml"/>

<xsl:template match="/">
<xsl:apply-templates select="INPUTXML/RECREATIONVEHICLE"/>
</xsl:template>

<xsl:template match="INPUTXML/RECREATIONVEHICLE">
	<FACTORMASTERPATH>
		<xsl:call-template name ="FINAL_PATH"></xsl:call-template>
	</FACTORMASTERPATH>
</xsl:template>

<xsl:template name ="FINAL_PATH">
	<xsl:choose>
		<xsl:when test ="LOBNAME = 'RV' and EDATE = '04/01/2004'" >/cmsweb/XSL/Quote/PremiumPath/RV/Premium.xsl</xsl:when>		  
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:template>

 
</xsl:stylesheet>