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
		<xsl:when test ="LOBNAME = 'CYCL'">
			<xsl:call-template name ="CYCLE_PRODUCT_FACTOR_MASTER_PATH"/>
		</xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name ="CYCLE_PRODUCT_FACTOR_MASTER_PATH">
	<xsl:choose>
		<xsl:when test ="POLICY/STATENAME = 'MICHIGAN'">
			<xsl:choose>			
				<xsl:when test="EDATE = '04/01/2006'">/cmsweb/XSL/Quote/MasterData/Motorcycle/Michigan/ProductFactorMaster_CYCLE_Michigan_04_2006.xml</xsl:when>
				<xsl:when test="EDATE = '07/01/2005'">/cmsweb/XSL/Quote/MasterData/Motorcycle/Michigan/ProductFactorMaster_CYCLE_Michigan_07_2005.xml</xsl:when>
				<xsl:when test="EDATE = '07/01/2004'">/cmsweb/XSL/Quote/MasterData/Motorcycle/Michigan/ProductFactorMaster_CYCLE_Michigan_07_2004.xml</xsl:when>
				<xsl:when test="EDATE = '07/01/2006'">/cmsweb/XSL/Quote/MasterData/Motorcycle/Michigan/ProductFactorMaster_CYCLE_Michigan_07_2006.xml</xsl:when>
				<xsl:when test="EDATE = '07/01/2008'">/cmsweb/XSL/Quote/MasterData/Motorcycle/Michigan/ProductFactorMaster_CYCLE_Michigan_07_2008.xml</xsl:when>
				<xsl:when test="EDATE = '07/01/2009'">/cmsweb/XSL/Quote/MasterData/Motorcycle/Michigan/ProductFactorMaster_CYCLE_Michigan_07_2009.xml</xsl:when>
				<xsl:otherwise></xsl:otherwise>   
			</xsl:choose>
		</xsl:when>
		
		
		
		<xsl:when test ="POLICY/STATENAME = 'INDIANA'"> 
			<xsl:choose>			
				<xsl:when test="EDATE = '04/01/2006'">/cmsweb/XSL/Quote/MasterData/Motorcycle/Indiana/ProductFactorMaster_CYCLE_Indiana_04_2006.xml</xsl:when>
				<xsl:when test="EDATE = '07/01/2005'">/cmsweb/XSL/Quote/MasterData/Motorcycle/Indiana/ProductFactorMaster_CYCLE_Indiana_07_2005.xml</xsl:when>
				<xsl:when test="EDATE = '07/01/2004'">/cmsweb/XSL/Quote/MasterData/Motorcycle/Indiana/ProductFactorMaster_CYCLE_Indiana_07_2004.xml</xsl:when>
			    <xsl:otherwise>/cmsweb/XSL/Quote/MasterData/Motorcycle/Indiana/ProductFactorMaster_CYCLE_Indiana_04_2006.xml</xsl:otherwise>  
			</xsl:choose>		
		</xsl:when>
		
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>					
</xsl:template>
</xsl:stylesheet>