<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xinclude="http://www.w3.org/1999/XML/xinclude"  version="1.0">
<xsl:output encoding="ISO8859-1" omit-xml-declaration="no" method="xml"/>

<xsl:template match="/">
<xsl:apply-templates select="DWELLINGDETAILS"/>
</xsl:template>

<xsl:template match="DWELLINGDETAILS">
	<FACTORMASTERPATH>
		<xsl:call-template name ="FINAL_PATH"></xsl:call-template>
	</FACTORMASTERPATH>
</xsl:template>

<xsl:template name ="FINAL_PATH">
	<xsl:choose>
		<xsl:when test ="LOBNAME = 'HO' and EDATE = '01/01/05'" >
			<xsl:call-template name ="HO_PRODUCT_FACTOR_MASTER_PATH"/>
		</xsl:when>
		<xsl:when test ="LOBNAME = 'AUTO' and EDATE = '01/01/05'">
			<xsl:call-template name ="AUTO_PRODUCT_FACTOR_MASTER_PATH"/>
		</xsl:when>
		<xsl:when test ="LOBNAME = 'MOTORCYCLE' and EDATE = '01/01/05'">
			<xsl:call-template name ="MOTORCYCLE_PRODUCT_FACTOR_MASTER_PATH"/>
		</xsl:when>
		<xsl:when test ="LOBNAME = 'WATERCRAFT' and EDATE = '01/01/05'">
			<xsl:call-template name ="WATERCRAFT_PRODUCT_FACTOR_MASTER_PATH"/>
		</xsl:when>
		<xsl:when test ="LOBNAME = 'RANTALDEWLLING' and EDATE = '01/01/05'">
			<xsl:call-template name ="RANTALDEWLLING_PRODUCT_FACTOR_MASTER_PATH"/>
		</xsl:when>
		<xsl:when test ="LOBNAME = 'GENERALLIABILITY' and EDATE = '01/01/05'">
			<xsl:call-template name ="GENERALLIABILITY_PRODUCT_FACTOR_MASTER_PATH"/>
		</xsl:when>
		<xsl:otherwise>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="ddd">
0
</xsl:template>
<xsl:template name ="HO_PRODUCT_FACTOR_MASTER_PATH">
	<xsl:choose>								
		<xsl:when test ="STATENAME = 'INDIANA'">MasterData/HO/ProductFactorsMaster_HO_Indiana.xml</xsl:when>
		<xsl:when test ="STATENAME = 'MICHIGAN' and PRODUCTNAME = 'HO-3' and PRODUCT_PREMIER = 'Premier'">MasterData/HO/ProductFactorsMaster_HO_Premier_Michigan.xml</xsl:when>
		<xsl:when test ="STATENAME = 'MICHIGAN' and PRODUCTNAME = 'HO-5' and PREMIER = 'Premier'">MasterData/HO/ProductFactorsMaster_HO_Premier_Michigan.xml</xsl:when>
		<xsl:when test ="STATENAME = 'MICHIGAN' and PRODUCTNAME = 'HO-3' and PRODUCT_PREMIER = ''">MasterData/HO/ProductFactorsMaster_HO_Regular_Michigan.xml</xsl:when>
		<xsl:when test ="STATENAME = 'MICHIGAN' and PRODUCTNAME = 'HO-4' and PRODUCT_PREMIER = ''">MasterData/HO/ProductFactorsMaster_HO_Regular_Michigan.xml</xsl:when>
		<xsl:when test ="STATENAME = 'MICHIGAN' and PRODUCTNAME = 'HO-5' and PRODUCT_PREMIER = ''">MasterData/HO/ProductFactorsMaster_HO_Regular_Michigan.xml</xsl:when>
		<xsl:when test ="STATENAME = 'MICHIGAN' and PRODUCTNAME = 'HO-6' and PRODUCT_PREMIER = ''">MasterData/HO/ProductFactorsMaster_HO_Regular_Michigan.xml</xsl:when>
		<xsl:otherwise>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name ="AUTO_PRODUCT_FACTOR_MASTER_PATH">
</xsl:template>

<xsl:template name ="MOTORCYCLE_PRODUCT_FACTOR_MASTER_PATH">
</xsl:template>

<xsl:template name ="WATERCRAFT_PRODUCT_FACTOR_MASTER_PATH">
</xsl:template>

<xsl:template name ="RANTALDEWLLING_PRODUCT_FACTOR_MASTER_PATH">
</xsl:template>

<xsl:template name ="GENERALLIABILITY_PRODUCT_FACTOR_MASTER_PATH">
</xsl:template>
</xsl:stylesheet>