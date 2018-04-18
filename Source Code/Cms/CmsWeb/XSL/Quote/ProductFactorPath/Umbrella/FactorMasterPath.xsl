<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xinclude="http://www.w3.org/1999/XML/xinclude"
	version="1.0">
	<xsl:output encoding="ISO8859-1" omit-xml-declaration="no" method="xml" />
	<xsl:template match="/">
		<xsl:apply-templates select="INPUTXML/QUICKQUOTE" />
	</xsl:template>
	<xsl:template match="INPUTXML/QUICKQUOTE">
		<FACTORMASTERPATH>
			<xsl:call-template name="FINAL_PATH"></xsl:call-template>
		</FACTORMASTERPATH>
	</xsl:template>
	<xsl:template name="FINAL_PATH">
		<xsl:choose>
			<xsl:when test="UMBRELLA/APPLICANT_INFIORMATION/STATENAME = 'MICHIGAN'">
				<xsl:choose>
					<xsl:when test="EDATE = '07/01/2006'">/cmsweb/XSL/Quote/MasterData/Umbrella/Michigan/ProductFactorsMaster_Umbrella_Michigan_07_2006.xml</xsl:when>
					<xsl:when test="EDATE = '01/01/2003'">/cmsweb/XSL/Quote/MasterData/Umbrella/Michigan/ProductFactorsMaster_Umbrella_Michigan_01_2003.xml</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
		<xsl:choose>
			<xsl:when test="UMBRELLA/APPLICANT_INFIORMATION/STATENAME = 'INDIANA'">
				<xsl:choose>
					<xsl:when test="EDATE = '07/01/2006'">/cmsweb/XSL/Quote/MasterData/Umbrella/Indiana/ProductFactorsMaster_Umbrella_Indiana_07_2006.xml</xsl:when>
					<xsl:when test="EDATE = '01/01/2003'">/cmsweb/XSL/Quote/MasterData/Umbrella/Indiana/ProductFactorsMaster_Umbrella_Indiana_01_2003.xml</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>