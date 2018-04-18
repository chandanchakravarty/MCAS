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

<xsl:template name ="FINAL_PATH">
	<xsl:choose>
		<xsl:when test ="UMBRELLA/APPLICANT_INFIORMATION/LOBNAME = 'UMB' and  TYPE = 'WATERCRAFT'" >/cmsweb/XSL/Quote/PremiumPath/Umbrella/Premium_WaterCraftComponent.xsl</xsl:when>		  
		<xsl:when test ="UMBRELLA/APPLICANT_INFIORMATION/LOBNAME = 'UMB' and  TYPE = 'UMBRELLA'" >/cmsweb/XSL/Quote/PremiumPath/Umbrella/Premium.xsl</xsl:when>		  
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:template>

 
</xsl:stylesheet>