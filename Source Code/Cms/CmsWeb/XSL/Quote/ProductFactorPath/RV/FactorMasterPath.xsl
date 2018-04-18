<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xinclude="http://www.w3.org/1999/XML/xinclude"  version="1.0">
<xsl:output encoding="ISO8859-1" omit-xml-declaration="no" method="xml"/>


	<xsl:template match="/">
	<xsl:apply-templates select="INPUTXML/RECREATIONVEHICLE"/>
	</xsl:template>

	<xsl:template match="INPUTXML/RECREATIONVEHICLE">
		<FACTORMASTERPATH>
			<xsl:call-template name ="FINAL_PATH"/>
		</FACTORMASTERPATH>
	</xsl:template>

	<xsl:template name ="FINAL_PATH">
		<xsl:choose>
			<xsl:when test ="LOBNAME = 'RV'" > 
	 			<xsl:call-template name ="RV_PRODUCT_FACTOR_MASTER_PATH"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>

 

	<xsl:template name ="RV_PRODUCT_FACTOR_MASTER_PATH">
		<!-- Pick the resp. file from the list -->
		<xsl:choose>
			<xsl:when test ="STATENAME ='INDIANA'"> 
				<xsl:choose> 
					<xsl:when test ="EDATE ='04/01/2004' " >/cmsweb/XSL/Quote/MasterData/RecreationalVehicles/Indiana/ProductFactorsMaster_RV_Indiana_04_2004.xml</xsl:when>
					<xsl:otherwise></xsl:otherwise>
					 
				</xsl:choose>
			</xsl:when>					
		
			<xsl:when test ="STATENAME ='MICHIGAN'"> 
				<xsl:choose>
					<xsl:when test ="EDATE ='04/01/2004' " >/cmsweb/XSL/Quote/MasterData/RecreationalVehicles/Michigan/ProductFactorsMaster_RV_Michigan_04_2004.xml</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:when>	
		 
		</xsl:choose>
	</xsl:template>

	 


</xsl:stylesheet> 

 