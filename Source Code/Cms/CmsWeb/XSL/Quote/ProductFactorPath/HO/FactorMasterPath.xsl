<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xinclude="http://www.w3.org/1999/XML/xinclude"
	version="1.0">
	<xsl:output encoding="ISO8859-1" omit-xml-declaration="no" method="xml" />
	<!-- Lookup id 1195,1216 in masterlookup_values. these are distinct codes. 
	 Indiana and michigan states have same code for same policy types -->
	<xsl:variable name="POLICYCODE_HO2REPAIR" select="'HO-2^REPAIR'" />
	<xsl:variable name="POLICYCODE_HO2REPLACE" select="'HO-2^REPLACE'" />
	<xsl:variable name="POLICYCODE_HO3PREMIER" select="'HO-3^PREMIER'" />
	<xsl:variable name="POLICYCODE_HO3REPAIR" select="'HO-3^REPAIR'" />
	<xsl:variable name="POLICYCODE_HO3REPLACE" select="'HO-3^REPLACE'" />
	<xsl:variable name="POLICYCODE_HO4DELUXE" select="'HO-4^DELUXE'" />
	<xsl:variable name="POLICYCODE_HO4TENANT" select="'HO-4^TENANT'" />
	<xsl:variable name="POLICYCODE_HO5PREMIER" select="'HO-5^PREMIER'" />
	<xsl:variable name="POLICYCODE_HO5REPLACE" select="'HO-5^REPLACE'" />
	<xsl:variable name="POLICYCODE_HO6DELUXE" select="'HO-6^DELUXE'" />
	<xsl:variable name="POLICYCODE_HO6UNIT" select="'HO-6^UNIT'" />
	<!-- Break up of the above -->
	<!-- Policy types -->
	<xsl:variable name="POLICYTYPE_HO2" select="'HO-2'" />
	<xsl:variable name="POLICYTYPE_HO3" select="'HO-3'" />
	<xsl:variable name="POLICYTYPE_HO4" select="'HO-4'" />
	<xsl:variable name="POLICYTYPE_HO5" select="'HO-5'" />
	<xsl:variable name="POLICYTYPE_HO6" select="'HO-6'" />
	<!-- to check ProductPremier node-->
	<xsl:variable name="POLICYDESC_REPAIR" select="'REPAIR'" />
	<xsl:variable name="POLICYDESC_REPLACEMENT" select="'REPLACE'" />
	<xsl:variable name="POLICYDESC_DELUXE" select="'DELUXE'" />
	<xsl:variable name="POLICYDESC_TENANT" select="'TENANT'" />
	<xsl:variable name="POLICYDESC_PREMIER" select="'PREMIER'" />
	<xsl:variable name="POLICYDESC_UNIT" select="'UNIT'" />
	<!-- Renewal quote effective date check michigan-->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE" select="'01/15/2008'" /> <!--Quote effective date check -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_DAY" select="15" /> <!--date day -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_MONTH" select="01" /> <!--date month -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_YEAR" select="2008" /> <!--date month -->
	
	<!-- Renewal quote effective date check michigan-->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_MICHIGAN_2009" select="'03/01/2009'" /> <!--Quote effective date check -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_DAY_MICHIGAN_2009" select="01" /> <!--date day -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_MONTH_MICHIGAN_2009" select="03" /> <!--date month -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_YEAR_MICHIGAN_2009" select="2009" /> <!--date month -->
	
	<!-- Renewal quote effective date check indiana-->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_INDINA_2008" select="'11/15/2008'" /> <!--Quote effective date check -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_DAY_INDINA_2008" select="15" /> <!--date day -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_MONTH_INDINA_2008" select="11" /> <!--date month -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_YEAR_INDINA_2008" select="2008" /> <!--date YEAR -->
	
	<!-- Renewal quote effective date check indiana-->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_INDINA_2009" select="'05/01/2009'" /> <!--Quote effective date check -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_DAY_INDINA_2009" select="01" /> <!--date day -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_MONTH_INDINA_2009" select="05" /> <!--date month -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_YEAR_INDINA_2009" select="2009" /> <!--date YEAR -->
	
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
			<xsl:when test="LOBNAME = 'HO'">
				<xsl:call-template name="HO_PRODUCT_FACTOR_MASTER_PATH" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="HO_PRODUCT_FACTOR_MASTER_PATH">
		<xsl:variable name="P_DATE" select="QUOTEEFFDATE" />
		<xsl:variable name="P_YEAR" select="substring($P_DATE,7,4)" />
		<xsl:variable name="P_MONTH" select="substring($P_DATE,1,2)" />
		<xsl:variable name="P_DAY" select="substring($P_DATE,4,5)" />
		<xsl:choose>
			<xsl:when test="STATENAME = 'INDIANA'">
				<xsl:choose>
					<!-- For Indiana -->
					<xsl:when test="EDATE ='12/01/2004' and PRODUCTNAME = 'HO-2' and PRODUCT_PREMIER=$POLICYDESC_REPLACEMENT">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_12_01_2004.xml</xsl:when>
					<xsl:when test="EDATE ='12/01/2004' and PRODUCTNAME = 'HO-3' and PRODUCT_PREMIER=$POLICYDESC_REPLACEMENT ">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_12_01_2004.xml</xsl:when>
					<xsl:when test="EDATE ='12/01/2004' and PRODUCTNAME = 'HO-5' and PRODUCT_PREMIER=$POLICYDESC_REPLACEMENT">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_12_01_2004.xml</xsl:when>
					<!-- REPAIR COST-->
					<xsl:when test="EDATE ='12/01/2004' and PRODUCTNAME = 'HO-2' and PRODUCT_PREMIER=$POLICYDESC_REPAIR">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_12_01_2004.xml</xsl:when>
					<xsl:when test="EDATE ='12/01/2004' and PRODUCTNAME = 'HO-3' and PRODUCT_PREMIER=$POLICYDESC_REPAIR">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_12_01_2004.xml</xsl:when>
					<xsl:when test="EDATE ='12/01/2004' and PRODUCTNAME = 'HO-4' and PRODUCT_PREMIER=$POLICYDESC_TENANT">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_12_01_2004.xml</xsl:when>
					<xsl:when test="EDATE ='12/01/2004' and PRODUCTNAME = 'HO-6' and PRODUCT_PREMIER=$POLICYDESC_UNIT">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_12_01_2004.xml</xsl:when>
					<!-- DELUXE-->
					<xsl:when test="EDATE ='12/01/2004' and PRODUCTNAME = 'HO-4' and PRODUCT_PREMIER = $POLICYDESC_DELUXE">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_12_01_2004.xml</xsl:when>
					<xsl:when test="EDATE ='12/01/2004' and PRODUCTNAME = 'HO-6' and PRODUCT_PREMIER = $POLICYDESC_DELUXE">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_12_01_2004.xml</xsl:when>
					<!-- for effective date EDATE = '03/01/2005'  -->
					<!-- For Indiana -->
					<xsl:when test="EDATE ='03/01/2005' and PRODUCTNAME = 'HO-2'  and PRODUCT_PREMIER=$POLICYDESC_REPLACEMENT">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_01_2005.xml</xsl:when>
					<xsl:when test="EDATE ='03/01/2005' and PRODUCTNAME = 'HO-3'  and PRODUCT_PREMIER=$POLICYDESC_REPLACEMENT">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_01_2005.xml</xsl:when>
					<xsl:when test="EDATE ='03/01/2005' and PRODUCTNAME = 'HO-5'  and PRODUCT_PREMIER=$POLICYDESC_REPLACEMENT ">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_01_2005.xml</xsl:when>
					<!-- REPAIR COST-->
					<xsl:when test="EDATE ='03/01/2005' and PRODUCTNAME = 'HO-2' and PRODUCT_PREMIER=$POLICYDESC_REPAIR">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_01_2005.xml</xsl:when>
					<xsl:when test="EDATE ='03/01/2005' and PRODUCTNAME = 'HO-3' and PRODUCT_PREMIER=$POLICYDESC_REPAIR">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_01_2005.xml</xsl:when>
					<xsl:when test="EDATE ='03/01/2005' and PRODUCTNAME = 'HO-4' and PRODUCT_PREMIER=$POLICYDESC_TENANT">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_01_2005.xml</xsl:when>
					<xsl:when test="EDATE ='03/01/2005' and PRODUCTNAME = 'HO-6' and PRODUCT_PREMIER=$POLICYDESC_UNIT">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_01_2005.xml</xsl:when>
					<!-- DELUXE-->
					<xsl:when test="EDATE ='03/01/2005' and PRODUCTNAME = 'HO-4' and PRODUCT_PREMIER = $POLICYDESC_DELUXE">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_01_2005.xml</xsl:when>
					<xsl:when test="EDATE ='03/01/2005' and PRODUCTNAME = 'HO-6' and PRODUCT_PREMIER = $POLICYDESC_DELUXE">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_01_2005.xml</xsl:when>
					<!-- for effective date EDATE = '02/01/2006'  -->
					<!-- For Indiana -->
					<xsl:when test="EDATE ='02/01/2006' and PRODUCTNAME = 'HO-2'  and PRODUCT_PREMIER=$POLICYDESC_REPLACEMENT">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_02_01_2006.xml</xsl:when>
					<xsl:when test="EDATE ='02/01/2006' and PRODUCTNAME = 'HO-3'  and PRODUCT_PREMIER=$POLICYDESC_REPLACEMENT">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_02_01_2006.xml</xsl:when>
					<xsl:when test="EDATE ='02/01/2006' and PRODUCTNAME = 'HO-5'  and PRODUCT_PREMIER=$POLICYDESC_REPLACEMENT">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_02_01_2006.xml</xsl:when>
					<!-- REPAIR COST-->
					<xsl:when test="EDATE ='02/01/2006' and PRODUCTNAME = 'HO-2' and PRODUCT_PREMIER=$POLICYDESC_REPAIR">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_02_01_2006.xml</xsl:when>
					<xsl:when test="EDATE ='02/01/2006' and PRODUCTNAME = 'HO-3' and PRODUCT_PREMIER=$POLICYDESC_REPAIR">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_02_01_2006.xml</xsl:when>
					<xsl:when test="EDATE ='02/01/2006' and PRODUCTNAME = 'HO-4' and PRODUCT_PREMIER=$POLICYDESC_TENANT">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_02_01_2006.xml</xsl:when>
					<xsl:when test="EDATE ='02/01/2006' and PRODUCTNAME = 'HO-6' and PRODUCT_PREMIER=$POLICYDESC_UNIT">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_02_01_2006.xml</xsl:when>
					<!-- DELUXE-->
					<xsl:when test="EDATE ='02/01/2006' and PRODUCTNAME = 'HO-4' and PRODUCT_PREMIER = $POLICYDESC_DELUXE">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_02_01_2006.xml</xsl:when>
					<xsl:when test="EDATE ='02/01/2006' and PRODUCTNAME = 'HO-6' and PRODUCT_PREMIER = $POLICYDESC_DELUXE">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_02_01_2006.xml</xsl:when>
					<!-- for effective date EDATE = '03/01/2007'  -->
					<!-- For Indiana -->
					<xsl:when test="EDATE ='03/01/2007' and PRODUCTNAME = 'HO-2'  and PRODUCT_PREMIER=$POLICYDESC_REPLACEMENT">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_2007.xml</xsl:when>
					<xsl:when test="EDATE ='03/01/2007' and PRODUCTNAME = 'HO-3'  and PRODUCT_PREMIER=$POLICYDESC_REPLACEMENT">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_2007.xml</xsl:when>
					<xsl:when test="EDATE ='03/01/2007' and PRODUCTNAME = 'HO-5'  and PRODUCT_PREMIER=$POLICYDESC_REPLACEMENT">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_2007.xml</xsl:when>
					<!-- REPAIR COST-->
					<xsl:when test="EDATE ='03/01/2007' and PRODUCTNAME = 'HO-2' and PRODUCT_PREMIER=$POLICYDESC_REPAIR">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_2007.xml</xsl:when>
					<xsl:when test="EDATE ='03/01/2007' and PRODUCTNAME = 'HO-3' and PRODUCT_PREMIER=$POLICYDESC_REPAIR">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_2007.xml</xsl:when>
					<xsl:when test="EDATE ='03/01/2007' and PRODUCTNAME = 'HO-4' and PRODUCT_PREMIER=$POLICYDESC_TENANT">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_2007.xml</xsl:when>
					<xsl:when test="EDATE ='03/01/2007' and PRODUCTNAME = 'HO-6' and PRODUCT_PREMIER=$POLICYDESC_UNIT">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_2007.xml</xsl:when>
					<!-- DELUXE-->
					<xsl:when test="EDATE ='03/01/2007' and PRODUCTNAME = 'HO-4' and PRODUCT_PREMIER = $POLICYDESC_DELUXE">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_2007.xml</xsl:when>
					<xsl:when test="EDATE ='03/01/2007' and PRODUCTNAME = 'HO-6' and PRODUCT_PREMIER = $POLICYDESC_DELUXE">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_2007.xml</xsl:when>
					
					<!-- For effective Edate = '10/15/2008' -->
					<!-- For Indiana -->
					<xsl:when test="EDATE ='10/15/2008' and PRODUCTNAME = 'HO-2'  and PRODUCT_PREMIER=$POLICYDESC_REPLACEMENT"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_INDINA_2008 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_INDINA_2008 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_INDINA_2008 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_2007.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_10_2008.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test="EDATE ='10/15/2008' and PRODUCTNAME = 'HO-3'  and PRODUCT_PREMIER=$POLICYDESC_REPLACEMENT"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_INDINA_2008 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_INDINA_2008 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_INDINA_2008 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_2007.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_10_2008.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test="EDATE ='10/15/2008' and PRODUCTNAME = 'HO-5'  and PRODUCT_PREMIER=$POLICYDESC_REPLACEMENT"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_INDINA_2008 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_INDINA_2008 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_INDINA_2008 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_2007.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_10_2008.xml</xsl:otherwise></xsl:choose></xsl:when>
					<!-- REPAIR COST-->
					<xsl:when test="EDATE ='10/15/2008' and PRODUCTNAME = 'HO-2' and PRODUCT_PREMIER=$POLICYDESC_REPAIR"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_INDINA_2008 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_INDINA_2008 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_INDINA_2008 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_2007.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_10_2008.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test="EDATE ='10/15/2008' and PRODUCTNAME = 'HO-3' and PRODUCT_PREMIER=$POLICYDESC_REPAIR"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_INDINA_2008 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_INDINA_2008 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_INDINA_2008 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_2007.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_10_2008.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test="EDATE ='10/15/2008' and PRODUCTNAME = 'HO-4' and PRODUCT_PREMIER=$POLICYDESC_TENANT"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_INDINA_2008 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_INDINA_2008 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_INDINA_2008 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_2007.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_10_2008.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test="EDATE ='10/15/2008' and PRODUCTNAME = 'HO-6' and PRODUCT_PREMIER=$POLICYDESC_UNIT"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_INDINA_2008 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_INDINA_2008 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_INDINA_2008 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_2007.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_10_2008.xml</xsl:otherwise></xsl:choose></xsl:when>
					<!-- DELUXE-->
					<xsl:when test="EDATE ='10/15/2008' and PRODUCTNAME = 'HO-4' and PRODUCT_PREMIER = $POLICYDESC_DELUXE"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_INDINA_2008 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_INDINA_2008 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_INDINA_2008 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_2007.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_10_2008.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test="EDATE ='10/15/2008' and PRODUCTNAME = 'HO-6' and PRODUCT_PREMIER = $POLICYDESC_DELUXE"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_INDINA_2008 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_INDINA_2008 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_INDINA_2008 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_03_2007.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_10_2008.xml</xsl:otherwise></xsl:choose></xsl:when>
					
					<!-- For effective Edate = '04/01/2009' -->
					<!-- For Indiana -->
					<xsl:when test="EDATE ='04/01/2009' and PRODUCTNAME = 'HO-2'  and PRODUCT_PREMIER=$POLICYDESC_REPLACEMENT"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_INDINA_2009 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_INDINA_2009 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_INDINA_2009 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_10_2008.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_04_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test="EDATE ='04/01/2009' and PRODUCTNAME = 'HO-3'  and PRODUCT_PREMIER=$POLICYDESC_REPLACEMENT"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_INDINA_2009 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_INDINA_2009 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_INDINA_2009 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_10_2008.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_04_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test="EDATE ='04/01/2009' and PRODUCTNAME = 'HO-5'  and PRODUCT_PREMIER=$POLICYDESC_REPLACEMENT"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_INDINA_2009 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_INDINA_2009 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_INDINA_2009 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_10_2008.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_04_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					<!-- REPAIR COST-->
					<xsl:when test="EDATE ='04/01/2009' and PRODUCTNAME = 'HO-2' and PRODUCT_PREMIER=$POLICYDESC_REPAIR"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_INDINA_2009 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_INDINA_2009 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_INDINA_2009 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_10_2008.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_04_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test="EDATE ='04/01/2009' and PRODUCTNAME = 'HO-3' and PRODUCT_PREMIER=$POLICYDESC_REPAIR"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_INDINA_2009 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_INDINA_2009 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_INDINA_2009 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_10_2008.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_04_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test="EDATE ='04/01/2009' and PRODUCTNAME = 'HO-4' and PRODUCT_PREMIER=$POLICYDESC_TENANT"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_INDINA_2009 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_INDINA_2009 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_INDINA_2009 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_10_2008.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_04_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test="EDATE ='04/01/2009' and PRODUCTNAME = 'HO-6' and PRODUCT_PREMIER=$POLICYDESC_UNIT"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_INDINA_2009 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_INDINA_2009 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_INDINA_2009 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_10_2008.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_04_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					<!-- DELUXE-->
					<xsl:when test="EDATE ='04/01/2009' and PRODUCTNAME = 'HO-4' and PRODUCT_PREMIER = $POLICYDESC_DELUXE"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_INDINA_2009 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_INDINA_2009 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_INDINA_2009 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_10_2008.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_04_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test="EDATE ='04/01/2009' and PRODUCTNAME = 'HO-6' and PRODUCT_PREMIER = $POLICYDESC_DELUXE"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_INDINA_2009 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_INDINA_2009 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_INDINA_2009 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_10_2008.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/Indiana/ProductFactorsMaster_HO_Indiana_04_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					
				</xsl:choose>
			</xsl:when>
			<!-- For MICHIGAN -->
			<xsl:when test="STATENAME = 'MICHIGAN'">
				<xsl:choose>
					<!-- For Michigan REPAIR COST -->
					<xsl:when test="PRODUCTNAME = 'HO-2' and EDATE ='12/01/2004' and PRODUCT_PREMIER=$POLICYDESC_REPAIR">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_12_2004.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-3' and EDATE ='12/01/2004' and PRODUCT_PREMIER=$POLICYDESC_REPAIR">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_12_2004.xml</xsl:when>
					<!-- For Michigan REGULAR -->
					<xsl:when test="PRODUCTNAME = 'HO-2' and EDATE ='12/01/2004' and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_12_2004.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-3' and EDATE ='12/01/2004' and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_12_2004.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-4' and EDATE ='12/01/2004' and PRODUCT_PREMIER = $POLICYDESC_TENANT">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_12_2004.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-5' and EDATE ='12/01/2004' and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_12_2004.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-6' and EDATE ='12/01/2004' and PRODUCT_PREMIER = $POLICYDESC_UNIT">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_12_2004.xml</xsl:when>
					<!-- For Michigan DELUXE  -->
					<xsl:when test="PRODUCTNAME = 'HO-4' and EDATE ='12/01/2004' and PRODUCT_PREMIER = $POLICYDESC_DELUXE">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_12_2004.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-6' and EDATE ='12/01/2004' and PRODUCT_PREMIER = $POLICYDESC_DELUXE">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_12_2004.xml</xsl:when>
					<!-- For Michigan PREMIER -->
					<xsl:when test="PRODUCTNAME = 'HO-3' and EDATE ='12/01/2004' and PRODUCT_PREMIER = $POLICYDESC_PREMIER">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Premier_Michigan_12_2004.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-5' and EDATE ='12/01/2004' and PRODUCT_PREMIER = $POLICYDESC_PREMIER">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Premier_Michigan_12_2004.xml</xsl:when>
					<!-- for effective date  03/01/2005 -->
					<!-- For Michigan REPAIR COST -->
					<xsl:when test="PRODUCTNAME = 'HO-2' and EDATE ='03/01/2005' and PRODUCT_PREMIER=$POLICYDESC_REPAIR">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_03_2005.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-3' and EDATE ='03/01/2005' and PRODUCT_PREMIER=$POLICYDESC_REPAIR">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_03_2005.xml</xsl:when>
					<!-- For Michigan REGULAR -->
					<xsl:when test="PRODUCTNAME = 'HO-2' and EDATE ='03/01/2005' and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_03_2005.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-3' and EDATE ='03/01/2005' and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_03_2005.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-4' and EDATE ='03/01/2005' and PRODUCT_PREMIER = $POLICYDESC_TENANT">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_03_2005.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-5' and EDATE ='03/01/2005' and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_03_2005.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-6' and EDATE ='03/01/2005' and PRODUCT_PREMIER = $POLICYDESC_UNIT">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_03_2005.xml</xsl:when>
					<!-- For Michigan DELUXE  -->
					<xsl:when test="PRODUCTNAME = 'HO-4' and EDATE ='03/01/2005' and PRODUCT_PREMIER = $POLICYDESC_DELUXE">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_03_2005.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-6' and EDATE ='03/01/2005' and PRODUCT_PREMIER = $POLICYDESC_DELUXE">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_03_2005.xml</xsl:when>
					<!-- For Michigan PREMIER -->
					<xsl:when test="PRODUCTNAME = 'HO-3' and EDATE ='03/01/2005' and PRODUCT_PREMIER =$POLICYDESC_PREMIER">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Premier_Michigan_03_2005.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-5' and EDATE ='03/01/2005' and PRODUCT_PREMIER = $POLICYDESC_PREMIER">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Premier_Michigan_03_2005.xml</xsl:when>
					
					<!-- for effective date   02/01/2006-->
					<!-- For Michigan REPAIR COST 
					<xsl:when test ="PRODUCTNAME = 'HO-2' and EDATE ='02/01/2006' and PRODUCT_PREMIER=$POLICYDESC_REPAIR" >/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					<xsl:when test ="PRODUCTNAME = 'HO-3' and EDATE ='02/01/2006' and PRODUCT_PREMIER=$POLICYDESC_REPAIR" >/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					-->  <!-- For Michigan REGULAR 
					<xsl:when test ="PRODUCTNAME = 'HO-2' and EDATE ='02/01/2006' and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT" >/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					<xsl:when test ="PRODUCTNAME = 'HO-3' and EDATE ='02/01/2006' and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT" >/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					<xsl:when test ="PRODUCTNAME = 'HO-4' and EDATE ='02/01/2006' and PRODUCT_PREMIER = $POLICYDESC_TENANT" >/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					<xsl:when test ="PRODUCTNAME = 'HO-5' and EDATE ='02/01/2006' and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT" >/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					<xsl:when test ="PRODUCTNAME = 'HO-6' and EDATE ='02/01/2006' and PRODUCT_PREMIER = $POLICYDESC_UNIT" >/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					 -->
					<!-- For Michigan DELUXE  					
					<xsl:when test ="PRODUCTNAME = 'HO-4' and EDATE ='02/01/2006' and PRODUCT_PREMIER = $POLICYDESC_DELUXE" >/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					<xsl:when test ="PRODUCTNAME = 'HO-6' and EDATE ='02/01/2006' and PRODUCT_PREMIER = $POLICYDESC_DELUXE" >/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					 -->
					<!-- For Michigan PREMIER
					<xsl:when test ="PRODUCTNAME = 'HO-3' and EDATE ='02/01/2006' and PRODUCT_PREMIER = $POLICYDESC_PREMIER" >/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Premier_Michigan_02_2006.xml</xsl:when>
					<xsl:when test ="PRODUCTNAME = 'HO-5' and EDATE ='02/01/2006' and PRODUCT_PREMIER = $POLICYDESC_PREMIER" >/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Premier_Michigan_02_2006.xml</xsl:when>
					  -->
					<!-- for effective date   08/01/2006-->
					<!-- For Michigan REPAIR COST -->
					<xsl:when test="PRODUCTNAME = 'HO-2' and EDATE ='08/01/2006' and PRODUCT_PREMIER=$POLICYDESC_REPAIR">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-3' and EDATE ='08/01/2006' and PRODUCT_PREMIER=$POLICYDESC_REPAIR">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					<!-- For Michigan REGULAR -->
					<xsl:when test="PRODUCTNAME = 'HO-2' and EDATE ='08/01/2006' and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-3' and EDATE ='08/01/2006' and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-4' and EDATE ='08/01/2006' and PRODUCT_PREMIER = $POLICYDESC_TENANT">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-5' and EDATE ='08/01/2006' and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-6' and EDATE ='08/01/2006' and PRODUCT_PREMIER = $POLICYDESC_UNIT">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					<!-- For Michigan DELUXE  -->
					<xsl:when test="PRODUCTNAME = 'HO-4' and EDATE ='08/01/2006' and PRODUCT_PREMIER = $POLICYDESC_DELUXE">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-6' and EDATE ='08/01/2006' and PRODUCT_PREMIER = $POLICYDESC_DELUXE">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					<!-- For Michigan PREMIER -->
					<xsl:when test="PRODUCTNAME = 'HO-3' and EDATE ='08/01/2006' and PRODUCT_PREMIER = $POLICYDESC_PREMIER">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Premier_Michigan_08_2006.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-5' and EDATE ='08/01/2006' and PRODUCT_PREMIER = $POLICYDESC_PREMIER">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Premier_Michigan_08_2006.xml</xsl:when>
					
					<!-- for effective date   01/01/2008-->
					<!-- For Michigan  -->
					<xsl:when test="PRODUCTNAME = 'HO-2' and EDATE ='01/01/2008' and PRODUCT_PREMIER=$POLICYDESC_REPAIR">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-3' and EDATE ='01/01/2008' and PRODUCT_PREMIER=$POLICYDESC_REPAIR">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					<!-- For Michigan REGULAR -->
					<xsl:when test="PRODUCTNAME = 'HO-2' and EDATE ='01/01/2008' and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-3' and EDATE ='01/01/2008' and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-4' and EDATE ='01/01/2008' and PRODUCT_PREMIER = $POLICYDESC_TENANT">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-5' and EDATE ='01/01/2008' and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-6' and EDATE ='01/01/2008' and PRODUCT_PREMIER = $POLICYDESC_UNIT">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					<!-- For Michigan DELUXE  -->
					<xsl:when test="PRODUCTNAME = 'HO-4' and EDATE ='01/01/2008' and PRODUCT_PREMIER = $POLICYDESC_DELUXE">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-6' and EDATE ='01/01/2008' and PRODUCT_PREMIER = $POLICYDESC_DELUXE">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-3' and EDATE ='01/01/2008' and PRODUCT_PREMIER =$POLICYDESC_PREMIER"><xsl:choose><xsl:when test="($P_DAY &lt;= $QUOTE_EFFECTIVE_DATE_DAY  and $P_MONTH = $QUOTE_EFFECTIVE_DATE_MONTH and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR and NEWBUSINESSFACTOR=normalize-space('REN'))">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Premier_Michigan_03_2005.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Premier_Michigan_01_2008.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-5' and EDATE ='01/01/2008' and PRODUCT_PREMIER =$POLICYDESC_PREMIER"><xsl:choose><xsl:when test="($P_DAY &lt;= $QUOTE_EFFECTIVE_DATE_DAY  and $P_MONTH = $QUOTE_EFFECTIVE_DATE_MONTH and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR and NEWBUSINESSFACTOR=normalize-space('REN'))">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Premier_Michigan_03_2005.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Premier_Michigan_01_2008.xml</xsl:otherwise></xsl:choose></xsl:when>				
					
					<!-- for effective date   02/01/2009-->
					<!-- For Michigan  -->
					<xsl:when test="PRODUCTNAME = 'HO-2' and EDATE ='02/01/2009' and PRODUCT_PREMIER=$POLICYDESC_REPAIR"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_MICHIGAN_2009 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_MICHIGAN_2009 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_MICHIGAN_2009 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_02_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-3' and EDATE ='02/01/2009' and PRODUCT_PREMIER=$POLICYDESC_REPAIR"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_MICHIGAN_2009 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_MICHIGAN_2009 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_MICHIGAN_2009 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_02_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					<!-- For Michigan REGULAR -->
					<xsl:when test="PRODUCTNAME = 'HO-2' and EDATE ='02/01/2009' and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_MICHIGAN_2009 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_MICHIGAN_2009 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_MICHIGAN_2009 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_02_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-3' and EDATE ='02/01/2009' and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_MICHIGAN_2009 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_MICHIGAN_2009 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_MICHIGAN_2009 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_02_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-4' and EDATE ='02/01/2009' and PRODUCT_PREMIER = $POLICYDESC_TENANT"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_MICHIGAN_2009 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_MICHIGAN_2009 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_MICHIGAN_2009 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_02_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-5' and EDATE ='02/01/2009' and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_MICHIGAN_2009 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_MICHIGAN_2009 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_MICHIGAN_2009 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_02_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-6' and EDATE ='02/01/2009' and PRODUCT_PREMIER = $POLICYDESC_UNIT"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_MICHIGAN_2009 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_MICHIGAN_2009 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_MICHIGAN_2009 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_02_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					<!-- For Michigan DELUXE  -->
					<xsl:when test="PRODUCTNAME = 'HO-4' and EDATE ='02/01/2009' and PRODUCT_PREMIER = $POLICYDESC_DELUXE"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_MICHIGAN_2009 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_MICHIGAN_2009 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_MICHIGAN_2009 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_02_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-6' and EDATE ='02/01/2009' and PRODUCT_PREMIER = $POLICYDESC_DELUXE"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_MICHIGAN_2009 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_MICHIGAN_2009 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_MICHIGAN_2009 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_08_2006.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Regular_Michigan_02_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-3' and EDATE ='02/01/2009' and PRODUCT_PREMIER =$POLICYDESC_PREMIER"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_MICHIGAN_2009 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_MICHIGAN_2009 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_MICHIGAN_2009 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Premier_Michigan_01_2008.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Premier_Michigan_02_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test="PRODUCTNAME = 'HO-5' and EDATE ='02/01/2009' and PRODUCT_PREMIER =$POLICYDESC_PREMIER"><xsl:choose><xsl:when test="$P_DAY &lt;=$QUOTE_EFFECTIVE_DATE_DAY_MICHIGAN_2009 and $P_MONTH &lt;=$QUOTE_EFFECTIVE_DATE_MONTH_MICHIGAN_2009 and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_MICHIGAN_2009 and NEWBUSINESSFACTOR=normalize-space('REN')">/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Premier_Michigan_01_2008.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/HO/MICHIGAN/ProductFactorsMaster_HO_Premier_Michigan_02_2009.xml</xsl:otherwise></xsl:choose></xsl:when>				

				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>