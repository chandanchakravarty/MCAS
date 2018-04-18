<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xinclude="http://www.w3.org/1999/XML/xinclude"  version="1.0">
<xsl:output encoding="ISO8859-1" omit-xml-declaration="no" method="xml"/>

<!--   Lookup id 1203,1223 in masterlookup_values. these are distinct codes. 
	 Indiana and michigan states have same code for same policy types
	 ANY CHANGES HERE WILL NEED TO BE INCORPORATED IN RESP. FACTORMASTERPATH FILE TOO
	  --> 
	<xsl:variable name="POLICYCODE_DP2REPAIR" select="'DP-2^REPAIR'"/>
	<xsl:variable name="POLICYCODE_DP2REPLACE" select="'DP-2^REPLACE'"/>
	<xsl:variable name="POLICYCODE_DP3PREMIER" select="'DP-3^PREMIER'"/>
	<xsl:variable name="POLICYCODE_DP3REPAIR" select="'DP-3^REPAIR'"/>
	<xsl:variable name="POLICYCODE_DP3REPLACE" select="'DP-3^REPLACE'"/>


	<!-- Break up of the above -->
	<!-- Policy types -->
	<xsl:variable name="POLICYTYPE_DP2" select="'DP-2'"/>
	<xsl:variable name="POLICYTYPE_DP3" select="'DP-3'"/>

	<!-- to check ProductPremier node-->
	<xsl:variable name="POLICYDESC_REPAIR" select="'REPAIR'"/>
	<xsl:variable name="POLICYDESC_REPLACEMENT" select="'REPLACE'"/>
	<xsl:variable name="POLICYDESC_PREMIER" select="'PREMIER'"/>
	
	<!-- Renewal quote effective date check-->
	<!--Michigan   -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE" select="'03/1/2009'" /> <!--Quote effective date check -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_DAY" select="1" /> <!--date day -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_MONTH" select="03" /> <!--date month -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_YEAR" select="2009" /> <!--date year -->
	
	<!--Indiana   -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_INDIANA" select="'04/1/2009'" /> <!--Quote effective date check -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_DAY_INDIANA" select="1" /> <!--date day -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_MONTH_INDIANA" select="04" /> <!--date month -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_YEAR_INDIANA" select="2009" /> <!--date year -->
	
	<xsl:template match="/">
	<xsl:apply-templates select="INPUTXML/DWELLINGDETAILS"/>
	</xsl:template>

	<xsl:template match="INPUTXML/DWELLINGDETAILS">
		<FACTORMASTERPATH>
			<xsl:call-template name ="FINAL_PATH"/>
		</FACTORMASTERPATH>
	</xsl:template>

	<xsl:template name ="FINAL_PATH">
		<xsl:choose>
			<xsl:when test ="LOBNAME = 'REDW'" > 
	 			<xsl:call-template name ="HO_PRODUCT_FACTOR_MASTER_PATH"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	 

	<xsl:template name ="HO_PRODUCT_FACTOR_MASTER_PATH">
		<!-- Read the Product Premier in caps -->
		<xsl:variable name="P_PRODUCT_PREMIER" select="PRODUCT_PREMIER"></xsl:variable>
		<xsl:variable name="PRODUCT_PREMIER_IN_CAPS"  select="translate(translate($P_PRODUCT_PREMIER,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')"/>
				<xsl:variable name="P_DATE" select="QUOTEEFFDATE" />
				<xsl:variable name="P_YEAR" select="substring($P_DATE,7,4)" />
				<xsl:variable name="P_MONTH" select="substring($P_DATE,1,2)" />
				<xsl:variable name="P_DAY" select="substring($P_DATE,4,5)" />
		<!-- Pick the resp. file from the list -->
		<xsl:choose>
			<xsl:when test ="STATENAME ='INDIANA'"> 
				<xsl:choose> 
					<xsl:when test ="EDATE ='11/01/2003' and PRODUCTNAME = $POLICYTYPE_DP2 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPLACEMENT" >/cmsweb/XSL/Quote/MasterData/RentalDwelling/Indiana/ProductFactorsMaster_RD_Indiana_01_2005.xml</xsl:when>
					<xsl:when test ="EDATE ='11/01/2003' and PRODUCTNAME = $POLICYTYPE_DP3 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPLACEMENT" >/cmsweb/XSL/Quote/MasterData/RentalDwelling/Indiana/ProductFactorsMaster_RD_Indiana_01_2005.xml</xsl:when>
					<xsl:when test ="EDATE ='11/01/2003' and PRODUCTNAME = $POLICYTYPE_DP3 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPAIR" >/cmsweb/XSL/Quote/MasterData/RentalDwelling/Indiana/ProductFactorsMaster_RD_Indiana_01_2005.xml</xsl:when>
					<xsl:when test ="EDATE ='11/01/2003' and PRODUCTNAME = $POLICYTYPE_DP2 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPAIR" >/cmsweb/XSL/Quote/MasterData/RentalDwelling/Indiana/ProductFactorsMaster_RD_Indiana_01_2005.xml</xsl:when>
			
					<xsl:when test ="EDATE ='01/01/2006' and PRODUCTNAME = $POLICYTYPE_DP2 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPLACEMENT" >/cmsweb/XSL/Quote/MasterData/RentalDwelling/Indiana/ProductFactorsMaster_RD_Indiana_01_2006.xml</xsl:when>
					<xsl:when test ="EDATE ='01/01/2006' and PRODUCTNAME = $POLICYTYPE_DP3 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPLACEMENT" >/cmsweb/XSL/Quote/MasterData/RentalDwelling/Indiana/ProductFactorsMaster_RD_Indiana_01_2006.xml</xsl:when>
					<xsl:when test ="EDATE ='01/01/2006' and PRODUCTNAME = $POLICYTYPE_DP3 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPAIR" >/cmsweb/XSL/Quote/MasterData/RentalDwelling/Indiana/ProductFactorsMaster_RD_Indiana_01_2006.xml</xsl:when>
					<xsl:when test ="EDATE ='01/01/2006' and PRODUCTNAME = $POLICYTYPE_DP2 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPAIR" >/cmsweb/XSL/Quote/MasterData/RentalDwelling/Indiana/ProductFactorsMaster_RD_Indiana_01_2006.xml</xsl:when>
					
					<xsl:when test ="EDATE ='04/01/2009' and PRODUCTNAME = $POLICYTYPE_DP2 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPLACEMENT" ><xsl:choose><xsl:when test="($P_MONTH &lt;= $QUOTE_EFFECTIVE_DATE_MONTH_INDIANA and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_INDIANA and normalize-space(NEWBUSINESSFACTOR)='N')">/cmsweb/XSL/Quote/MasterData/RentalDwelling/Indiana/ProductFactorsMaster_RD_Indiana_01_2006.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/RentalDwelling/Indiana/ProductFactorsMaster_RD_Indiana_04_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test ="EDATE ='04/01/2009' and PRODUCTNAME = $POLICYTYPE_DP3 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPLACEMENT" ><xsl:choose><xsl:when test="($P_MONTH &lt;= $QUOTE_EFFECTIVE_DATE_MONTH_INDIANA and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_INDIANA and normalize-space(NEWBUSINESSFACTOR)='N')">/cmsweb/XSL/Quote/MasterData/RentalDwelling/Indiana/ProductFactorsMaster_RD_Indiana_01_2006.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/RentalDwelling/Indiana/ProductFactorsMaster_RD_Indiana_04_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test ="EDATE ='04/01/2009' and PRODUCTNAME = $POLICYTYPE_DP3 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPAIR" ><xsl:choose><xsl:when test="($P_MONTH &lt;= $QUOTE_EFFECTIVE_DATE_MONTH_INDIANA and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_INDIANA and normalize-space(NEWBUSINESSFACTOR)='N')">/cmsweb/XSL/Quote/MasterData/RentalDwelling/Indiana/ProductFactorsMaster_RD_Indiana_01_2006.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/RentalDwelling/Indiana/ProductFactorsMaster_RD_Indiana_04_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test ="EDATE ='04/01/2009' and PRODUCTNAME = $POLICYTYPE_DP2 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPAIR" ><xsl:choose><xsl:when test="($P_MONTH &lt;= $QUOTE_EFFECTIVE_DATE_MONTH_INDIANA and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_INDIANA and normalize-space(NEWBUSINESSFACTOR)='N')">/cmsweb/XSL/Quote/MasterData/RentalDwelling/Indiana/ProductFactorsMaster_RD_Indiana_01_2006.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/RentalDwelling/Indiana/ProductFactorsMaster_RD_Indiana_04_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					 
				</xsl:choose>
			</xsl:when>					
		
			<xsl:when test ="STATENAME ='MICHIGAN'"> 
				<xsl:choose>
					<xsl:when test ="EDATE ='01/01/2005' and PRODUCTNAME = $POLICYTYPE_DP2 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPLACEMENT" >/cmsweb/XSL/Quote/MasterData/RentalDwelling/Michigan/ProductFactorsMaster_RD_Regular_Michigan_01_2005.xml</xsl:when>
					<xsl:when test ="EDATE ='01/01/2005' and PRODUCTNAME = $POLICYTYPE_DP3 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPLACEMENT" >/cmsweb/XSL/Quote/MasterData/RentalDwelling/Michigan/ProductFactorsMaster_RD_Regular_Michigan_01_2005.xml</xsl:when>
					<xsl:when test ="EDATE ='01/01/2005' and PRODUCTNAME = $POLICYTYPE_DP3 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_PREMIER" >/cmsweb/XSL/Quote/MasterData/RentalDwelling/Michigan/ProductFactorsMaster_RD_Premier_Michigan_01_2005.xml</xsl:when>
					<xsl:when test ="EDATE ='01/01/2005' and PRODUCTNAME = $POLICYTYPE_DP3 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPAIR" >/cmsweb/XSL/Quote/MasterData/RentalDwelling/Michigan/ProductFactorsMaster_RD_Regular_Michigan_01_2005.xml</xsl:when>
					<xsl:when test ="EDATE ='01/01/2005' and PRODUCTNAME = $POLICYTYPE_DP2 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPAIR" >/cmsweb/XSL/Quote/MasterData/RentalDwelling/Michigan/ProductFactorsMaster_RD_Regular_Michigan_01_2005.xml</xsl:when>
					
					<xsl:when test ="EDATE ='01/01/2006' and PRODUCTNAME = $POLICYTYPE_DP2 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPLACEMENT" >/cmsweb/XSL/Quote/MasterData/RentalDwelling/Michigan/ProductFactorsMaster_RD_Regular_Michigan_01_2006.xml</xsl:when>
					<xsl:when test ="EDATE ='01/01/2006' and PRODUCTNAME = $POLICYTYPE_DP3 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPLACEMENT" >/cmsweb/XSL/Quote/MasterData/RentalDwelling/Michigan/ProductFactorsMaster_RD_Regular_Michigan_01_2006.xml</xsl:when>
					<xsl:when test ="EDATE ='01/01/2006' and PRODUCTNAME = $POLICYTYPE_DP3 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_PREMIER" >/cmsweb/XSL/Quote/MasterData/RentalDwelling/Michigan/ProductFactorsMaster_RD_Premier_Michigan_01_2006.xml</xsl:when>
					<xsl:when test ="EDATE ='01/01/2006' and PRODUCTNAME = $POLICYTYPE_DP3 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPAIR" >/cmsweb/XSL/Quote/MasterData/RentalDwelling/Michigan/ProductFactorsMaster_RD_Regular_Michigan_01_2006.xml</xsl:when>
					<xsl:when test ="EDATE ='01/01/2006' and PRODUCTNAME = $POLICYTYPE_DP2 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPAIR" >/cmsweb/XSL/Quote/MasterData/RentalDwelling/Michigan/ProductFactorsMaster_RD_Regular_Michigan_01_2006.xml</xsl:when>
			
					<xsl:when test ="EDATE ='01/01/2009' and PRODUCTNAME = $POLICYTYPE_DP2 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPLACEMENT" >/cmsweb/XSL/Quote/MasterData/RentalDwelling/Michigan/ProductFactorsMaster_RD_Regular_Michigan_01_2009.xml</xsl:when>
					<xsl:when test ="EDATE ='01/01/2009' and PRODUCTNAME = $POLICYTYPE_DP3 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPLACEMENT" >/cmsweb/XSL/Quote/MasterData/RentalDwelling/Michigan/ProductFactorsMaster_RD_Regular_Michigan_01_2009.xml</xsl:when>
					<xsl:when test ="EDATE ='01/01/2009' and PRODUCTNAME = $POLICYTYPE_DP3 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_PREMIER" >/cmsweb/XSL/Quote/MasterData/RentalDwelling/Michigan/ProductFactorsMaster_RD_Premier_Michigan_01_2009.xml</xsl:when>
					<xsl:when test ="EDATE ='01/01/2009' and PRODUCTNAME = $POLICYTYPE_DP3 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPAIR" >/cmsweb/XSL/Quote/MasterData/RentalDwelling/Michigan/ProductFactorsMaster_RD_Regular_Michigan_01_2009.xml</xsl:when>
					<xsl:when test ="EDATE ='01/01/2009' and PRODUCTNAME = $POLICYTYPE_DP2 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPAIR" >/cmsweb/XSL/Quote/MasterData/RentalDwelling/Michigan/ProductFactorsMaster_RD_Regular_Michigan_01_2009.xml</xsl:when>
					
					<xsl:when test ="EDATE ='03/01/2009' and PRODUCTNAME = $POLICYTYPE_DP2 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPLACEMENT" ><xsl:choose><xsl:when test="($P_MONTH &lt;= $QUOTE_EFFECTIVE_DATE_MONTH and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR and normalize-space(NEWBUSINESSFACTOR)='N')">/cmsweb/XSL/Quote/MasterData/RentalDwelling/Michigan/ProductFactorsMaster_RD_Regular_Michigan_01_2009.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/RentalDwelling/Michigan/ProductFactorsMaster_RD_Regular_Michigan_03_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test ="EDATE ='03/01/2009' and PRODUCTNAME = $POLICYTYPE_DP3 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPLACEMENT" ><xsl:choose><xsl:when test="($P_MONTH &lt;= $QUOTE_EFFECTIVE_DATE_MONTH  and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR and normalize-space(NEWBUSINESSFACTOR)='N')">/cmsweb/XSL/Quote/MasterData/RentalDwelling/Michigan/ProductFactorsMaster_RD_Regular_Michigan_01_2009.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/RentalDwelling/Michigan/ProductFactorsMaster_RD_Regular_Michigan_03_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test ="EDATE ='03/01/2009' and PRODUCTNAME = $POLICYTYPE_DP3 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_PREMIER" ><xsl:choose><xsl:when test="($P_MONTH &lt;= $QUOTE_EFFECTIVE_DATE_MONTH  and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR and normalize-space(NEWBUSINESSFACTOR)='N')">/cmsweb/XSL/Quote/MasterData/RentalDwelling/Michigan/ProductFactorsMaster_RD_Premier_Michigan_01_2009.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/RentalDwelling/Michigan/ProductFactorsMaster_RD_Premier_Michigan_03_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test ="EDATE ='03/01/2009' and PRODUCTNAME = $POLICYTYPE_DP3 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPAIR" ><xsl:choose><xsl:when test="($P_MONTH &lt;= $QUOTE_EFFECTIVE_DATE_MONTH  and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR and normalize-space(NEWBUSINESSFACTOR)='N')">/cmsweb/XSL/Quote/MasterData/RentalDwelling/Michigan/ProductFactorsMaster_RD_Regular_Michigan_01_2009.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/RentalDwelling/Michigan/ProductFactorsMaster_RD_Regular_Michigan_03_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test ="EDATE ='03/01/2009' and PRODUCTNAME = $POLICYTYPE_DP2 and $PRODUCT_PREMIER_IN_CAPS = $POLICYDESC_REPAIR" ><xsl:choose><xsl:when test="($P_MONTH &lt;= $QUOTE_EFFECTIVE_DATE_MONTH  and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR and normalize-space(NEWBUSINESSFACTOR)='N')">/cmsweb/XSL/Quote/MasterData/RentalDwelling/Michigan/ProductFactorsMaster_RD_Regular_Michigan_01_2009.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/RentalDwelling/Michigan/ProductFactorsMaster_RD_Regular_Michigan_03_2009.xml</xsl:otherwise></xsl:choose></xsl:when>
				</xsl:choose>
			</xsl:when>		 
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>