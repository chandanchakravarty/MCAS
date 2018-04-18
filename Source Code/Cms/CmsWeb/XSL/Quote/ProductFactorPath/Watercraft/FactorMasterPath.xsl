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
		<xsl:when test ="LOBNAME = 'BOAT'">
			<xsl:call-template name ="BOAT_PRODUCT_FACTOR_MASTER_PATH"/>
		</xsl:when>
		 
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:template>


<xsl:template name ="BOAT_PRODUCT_FACTOR_MASTER_PATH">
	
	<xsl:variable name ="P_STATE_DOCKED_IN" select="BOATS/BOAT/STATEDOCKEDIN"/>
	<xsl:variable name="P_STATE_DOCKED_IN_UPPERCASE"  select="translate(translate($P_STATE_DOCKED_IN,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')"/>

	<xsl:choose>	
		<xsl:when test ="$P_STATE_DOCKED_IN_UPPERCASE = 'MICHIGAN'"> 
			<xsl:choose>
				<xsl:when test ="EDATE = '03/01/2005'">/cmsweb/XSL/Quote/MasterData/Watercraft/Michigan/ProductFactorsMaster_Watercraft_Michigan_03_2005.xml</xsl:when>
				<xsl:when test ="EDATE = '03/01/2006'">/cmsweb/XSL/Quote/MasterData/Watercraft/Michigan/ProductFactorsMaster_Watercraft_Michigan_03_2006.xml</xsl:when>
			</xsl:choose>
			  
		</xsl:when>
		<xsl:when test ="$P_STATE_DOCKED_IN_UPPERCASE = 'INDIANA'"> 
				<xsl:choose>
						<xsl:when test ="EDATE = '03/01/2005'">/cmsweb/XSL/Quote/MasterData/Watercraft/Indiana/ProductFactorsMaster_Watercraft_Indiana_03_2005.xml</xsl:when>	  
						<xsl:when test ="EDATE = '03/01/2006'">/cmsweb/XSL/Quote/MasterData/Watercraft/Indiana/ProductFactorsMaster_Watercraft_Indiana_03_2006.xml</xsl:when>	   
				</xsl:choose>	  
		</xsl:when>
		<xsl:when test ="$P_STATE_DOCKED_IN_UPPERCASE = 'WISCONSIN'"> 
				<xsl:choose>
					<xsl:when test ="EDATE = '03/01/2005'">/cmsweb/XSL/Quote/MasterData/Watercraft/Wisconsin/ProductFactorsMaster_Watercraft_Wisconsin_03_2005.xml</xsl:when>	  
					<xsl:when test ="EDATE = '03/01/2006'">/cmsweb/XSL/Quote/MasterData/Watercraft/Wisconsin/ProductFactorsMaster_Watercraft_Wisconsin_03_2006.xml </xsl:when>	  
				</xsl:choose>
		</xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>					
</xsl:template>



</xsl:stylesheet>