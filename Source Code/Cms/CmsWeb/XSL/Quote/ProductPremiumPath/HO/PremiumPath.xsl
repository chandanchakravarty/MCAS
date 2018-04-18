<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xinclude="http://www.w3.org/1999/XML/xinclude" version="1.0">
	<xsl:output encoding="ISO8859-1" omit-xml-declaration="no" method="xml" />
	<xsl:template match="/">
		<xsl:apply-templates select="INPUTXML/DWELLINGDETAILS" />
	</xsl:template>
	<xsl:template match="INPUTXML/DWELLINGDETAILS">
		<FACTORMASTERPATH>
			<xsl:call-template name="FINAL_PATH"></xsl:call-template>
		</FACTORMASTERPATH>
	</xsl:template>
	<xsl:template name="FINAL_PATH">
		<xsl:choose>
			<xsl:when test="STATENAME = 'INDIANA'">
				<xsl:choose>
					<xsl:when test="LOBNAME = 'HO' and EDATE = '12/01/2004'">/cmsweb/XSL/Quote/PremiumPath/HO/Premium_09_2008.xsl</xsl:when>
					<xsl:when test="LOBNAME = 'HO' and EDATE = '03/01/2005'">/cmsweb/XSL/Quote/PremiumPath/HO/Premium_09_2008.xsl</xsl:when>
					<xsl:when test="LOBNAME = 'HO' and EDATE = '02/01/2006'">/cmsweb/XSL/Quote/PremiumPath/HO/Premium_09_2008.xsl</xsl:when>
					<xsl:when test="LOBNAME = 'HO' and EDATE = '02/01/2006'">/cmsweb/XSL/Quote/PremiumPath/HO/Premium_09_2008.xsl</xsl:when>
					<xsl:when test="LOBNAME = 'HO' and EDATE = '10/01/2008'">/cmsweb/XSL/Quote/PremiumPath/HO/Premium.xsl</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="STATENAME = 'MICHIGAN'">
				<xsl:choose>
					<xsl:when test="LOBNAME = 'HO' and EDATE = '12/01/2004'">/cmsweb/XSL/Quote/PremiumPath/HO/Premium_09_2008.xsl</xsl:when>
					<xsl:when test="LOBNAME = 'HO' and EDATE = '03/01/2005'">/cmsweb/XSL/Quote/PremiumPath/HO/Premium_09_2008.xsl</xsl:when>
					<xsl:when test="LOBNAME = 'HO' and EDATE = '02/01/2006'">/cmsweb/XSL/Quote/PremiumPath/HO/Premium_09_2008.xsl</xsl:when>
					<xsl:when test="LOBNAME = 'HO' and EDATE = '02/01/2006'">/cmsweb/XSL/Quote/PremiumPath/HO/Premium_09_2008.xsl</xsl:when>
					<xsl:when test="LOBNAME = 'HO' and EDATE = '02/01/2009'">/cmsweb/XSL/Quote/PremiumPath/HO/Premium.xsl</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>/cmsweb/XSL/Quote/PremiumPath/HO/Premium.xsl</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>