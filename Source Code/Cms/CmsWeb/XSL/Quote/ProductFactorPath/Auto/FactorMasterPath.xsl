<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xinclude="http://www.w3.org/1999/XML/xinclude"
	version="1.0">
	<xsl:output encoding="ISO8859-1" omit-xml-declaration="no" method="xml" />
	<!-- Renewal quote effective date check-->
	<!--Michigan   -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE" select="'02/1/2008'" /> <!--Quote effective date check -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_DAY" select="1" /> <!--date day -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_MONTH" select="02" /> <!--date month -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_YEAR" select="2008" /> <!--date YEAR -->
	<!-- Indiana  -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_INDIANA" select="'06/1/2008'" /> <!--Quote effective date check -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_DAY_INDIANA" select="1" /> <!--date day -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_MONTH_INDIANA" select="06" /> <!--date month -->
	<xsl:variable name="QUOTE_EFFECTIVE_DATE_YEAR_INDIANA" select="2008" /> <!--date YEAR -->
	
	
	<xsl:template match="/">
		<xsl:apply-templates select="INPUTXML/QUICKQUOTE" />
	</xsl:template>
	<!-- *************************************************************************************************************** -->
	<xsl:template match="INPUTXML/QUICKQUOTE">
		<FACTORMASTERPATH>
			<xsl:choose> <!-- was here ..FINAL_PATHPER missing...need to copy it from the prev version n make necessary chanbges for discoubt surcjharge -->
				<xsl:when test="POLICY/CALLEDFROM = 'DISCOUNTSP'">
				<!-- <xsl:call-template name="AUTO_PRODUCT_FACTOR_MASTER_PATH_PERSONAL"></xsl:call-template>^<xsl:call-template name="AUTO_PRODUCT_FACTOR_MASTER_PATH_COMMERCIAL"></xsl:call-template> -->
				<xsl:call-template name="AUTO_PRODUCT_FACTOR_MASTER_PATH_PERSONAL"></xsl:call-template>
				
				</xsl:when>
				<xsl:when test="POLICY/CALLEDFROM = 'DISCOUNTSC'">
				<!-- <xsl:call-template name="AUTO_PRODUCT_FACTOR_MASTER_PATH_PERSONAL"></xsl:call-template>^<xsl:call-template name="AUTO_PRODUCT_FACTOR_MASTER_PATH_COMMERCIAL"></xsl:call-template> -->
				<xsl:call-template name="AUTO_PRODUCT_FACTOR_MASTER_PATH_COMMERCIAL"></xsl:call-template>
				
				</xsl:when>
				<xsl:otherwise>
					<xsl:call-template name="FINAL_PATH"></xsl:call-template>
				</xsl:otherwise>
			</xsl:choose>
		</FACTORMASTERPATH>
	</xsl:template> 
	<!-- *************************************************************************************************************** -->
	<xsl:template name="FINAL_PATH">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPEUSE = 'PERSONAL'">
				<xsl:call-template name="AUTO_PRODUCT_FACTOR_MASTER_PATH_PERSONAL" />
			</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPEUSE = 'COMMERCIAL'">
				<xsl:call-template name="AUTO_PRODUCT_FACTOR_MASTER_PATH_COMMERCIAL" />
			</xsl:when>
			<xsl:otherwise><xsl:call-template name="AUTO_PRODUCT_FACTOR_MASTER_PATH_PERSONAL" /> </xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- *************************************************************************************************************** -->
	<xsl:template name="AUTO_PRODUCT_FACTOR_MASTER_PATH_PERSONAL">
				<xsl:variable name="P_DATE" select="POLICY/QUOTEEFFDATE" />
				<xsl:variable name="P_YEAR" select="substring($P_DATE,7,4)" />
				<xsl:variable name="P_MONTH" select="substring($P_DATE,1,2)" />
				<xsl:variable name="P_DAY" select="substring($P_DATE,4,5)" />
		<xsl:choose>						
			<xsl:when test="POLICY/STATENAME = 'MICHIGAN' or STATENAME='MICHIGAN'">				
				<xsl:choose>
					<xsl:when test="EDATE = '10/01/2004'">/cmsweb/XSL/Quote/MasterData/Auto/Personal/Michigan/ProductFactorsMaster_AUTO_Personal_Michigan_10_2004.xml</xsl:when>
					<xsl:when test="EDATE = '08/01/2005'">/cmsweb/XSL/Quote/MasterData/Auto/Personal/Michigan/ProductFactorsMaster_AUTO_Personal_Michigan_08_2005.xml</xsl:when>
					<xsl:when test="EDATE = '06/01/2006'">/cmsweb/XSL/Quote/MasterData/Auto/Personal/Michigan/ProductFactorsMaster_AUTO_Personal_Michigan_06_2006.xml</xsl:when>
					<xsl:when test="EDATE = '07/01/2007'">/cmsweb/XSL/Quote/MasterData/Auto/Personal/Michigan/ProductFactorsMaster_AUTO_Personal_Michigan_07_2007.xml</xsl:when>
					<xsl:when test="EDATE = '02/01/2008'"><xsl:choose><xsl:when test="($P_MONTH &lt;= $QUOTE_EFFECTIVE_DATE_MONTH and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR and POLICY/RENEWAL=normalize-space('TRUE'))">/cmsweb/XSL/Quote/MasterData/Auto/Personal/Michigan/ProductFactorsMaster_AUTO_Personal_Michigan_07_2007.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/AUTO/Personal/Michigan/ProductFactorsMaster_AUTO_Personal_Michigan_02_2008.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test="EDATE = '07/01/2008'">/cmsweb/XSL/Quote/MasterData/Auto/Personal/Michigan/ProductFactorsMaster_AUTO_Personal_Michigan_07_2008.xml</xsl:when>
					<xsl:when test="EDATE = '07/01/2009'">/cmsweb/XSL/Quote/MasterData/Auto/Personal/Michigan/ProductFactorsMaster_AUTO_Personal_Michigan_07_2009.xml</xsl:when>
					<!-- testing, for this date(12/01/2005) in indiana master data is released so 
					we will have to include this date in michigan sate , but we will call the previous version 
					of file which is the latest version for this stste --> 
					 
					<xsl:otherwise>/cmsweb/XSL/Quote/MasterData/Auto/Personal/Michigan/ProductFactorsMaster_AUTO_Personal_Michigan_08_2005.xml</xsl:otherwise>
			  											
				</xsl:choose>
			</xsl:when>			
			<xsl:when test="POLICY/STATENAME = 'INDIANA' or STATENAME='INDIANA'">
				<xsl:choose>
					<xsl:when test="EDATE = '04/01/2005'">/cmsweb/XSL/Quote/MasterData/AUTO/Personal/Indiana/ProductFactorsMaster_AUTO_Personal_Indiana_04_2005.xml</xsl:when>
					<xsl:when test="EDATE = '08/01/2005'">/cmsweb/XSL/Quote/MasterData/AUTO/Personal/Indiana/ProductFactorsMaster_AUTO_Personal_Indiana_08_2005.xml</xsl:when>
					<xsl:when test="EDATE = '12/01/2005'">/cmsweb/XSL/Quote/MasterData/AUTO/Personal/Indiana/ProductFactorsMaster_AUTO_Personal_Indiana_12_2005.xml</xsl:when>
					<!--xsl:when test="EDATE = '02/01/2006'">/cmsweb/XSL/Quote/MasterData/AUTO/Personal/Indiana/ProductFactorsMaster_AUTO_Personal_Indiana_02_2006.xml</xsl:when-->
					<xsl:when test="EDATE = '06/01/2008'">
        <xsl:choose>
          <xsl:when test="($P_MONTH &lt;= $QUOTE_EFFECTIVE_DATE_MONTH_INDIANA and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR_INDIANA and POLICY/RENEWAL=normalize-space('TRUE'))">/cmsweb/XSL/Quote/MasterData/AUTO/Personal/Indiana/ProductFactorsMaster_AUTO_Personal_Indiana_12_2005.xml
          </xsl:when>
          <xsl:otherwise>/cmsweb/XSL/Quote/MasterData/AUTO/Personal/Indiana/ProductFactorsMaster_AUTO_Personal_Indiana_06_2008.xml
          </xsl:otherwise>
        </xsl:choose>
         </xsl:when>
					<xsl:when test="EDATE = '01/01/2010'">/cmsweb/XSL/Quote/MasterData/AUTO/Personal/Indiana/ProductFactorsMaster_AUTO_Personal_Indiana_01_2010.xml</xsl:when>
				<xsl:otherwise> 
					/cmsweb/XSL/Quote/MasterData/AUTO/Personal/Indiana/ProductFactorsMaster_AUTO_Personal_Indiana_12_2005.xml				
				</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
      
			<xsl:otherwise>/cmsweb/XSL/Quote/MasterData/AUTO/Personal/Others/ProductFactorsMaster_AUTO_Personal_Others_States_09_2010.xml	
      </xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- *************************************************************************************************************** -->
	<xsl:template name="AUTO_PRODUCT_FACTOR_MASTER_PATH_COMMERCIAL">
		<xsl:variable name="P_DATE" select="POLICY/QUOTEEFFDATE" />
		<xsl:variable name="P_YEAR" select="substring($P_DATE,7,4)" />
		<xsl:variable name="P_MONTH" select="substring($P_DATE,1,2)" />
		<xsl:variable name="P_DAY" select="substring($P_DATE,4,5)" />
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME = 'MICHIGAN'">
				<xsl:choose>
					
					<xsl:when test="EDATE = '08/01/2005'">/cmsweb/XSL/Quote/MasterData/AUTO/Commercial/Michigan/ProductFactorsMaster_AUTO_Commercial_Michigan_8_2005.xml</xsl:when>
					<xsl:when test="EDATE = '06/01/2006'">/cmsweb/XSL/Quote/MasterData/AUTO/Commercial/Michigan/ProductFactorsMaster_AUTO_Commercial_Michigan_6_2006.xml</xsl:when>
					<xsl:when test="EDATE = '02/01/2008'"><xsl:choose><xsl:when test="($P_MONTH &lt;= $QUOTE_EFFECTIVE_DATE_MONTH and $P_YEAR &lt;= $QUOTE_EFFECTIVE_DATE_YEAR and POLICY/RENEWAL=normalize-space('TRUE'))">/cmsweb/XSL/Quote/MasterData/AUTO/Commercial/Michigan/ProductFactorsMaster_AUTO_Commercial_Michigan_6_2006.xml</xsl:when><xsl:otherwise>/cmsweb/XSL/Quote/MasterData/AUTO/Commercial/Michigan/ProductFactorsMaster_AUTO_Commercial_Michigan_2_2008.xml</xsl:otherwise></xsl:choose></xsl:when>
					<xsl:when test="EDATE = '07/01/2008'">/cmsweb/XSL/Quote/MasterData/AUTO/Commercial/Michigan/ProductFactorsMaster_AUTO_Commercial_Michigan_7_2008.xml</xsl:when>
					<xsl:when test="EDATE = '07/01/2009'">/cmsweb/XSL/Quote/MasterData/AUTO/Commercial/Michigan/ProductFactorsMaster_AUTO_Commercial_Michigan_7_2009.xml</xsl:when>
			<xsl:otherwise> 
				/cmsweb/XSL/Quote/MasterData/AUTO/Commercial/Michigan/ProductFactorsMaster_AUTO_Commercial_Michigan_8_2005.xml
			</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="POLICY/STATENAME = 'INDIANA'">
				<xsl:choose>
					<xsl:when test="EDATE = '12/01/2005'">/cmsweb/XSL/Quote/MasterData/AUTO/Commercial/Indiana/ProductFactorsMaster_AUTO_Commercial_Indiana_12_2005.xml</xsl:when>
					
				<xsl:otherwise> 
					/cmsweb/XSL/Quote/MasterData/AUTO/Commercial/Indiana/ProductFactorsMaster_AUTO_Commercial_Indiana_12_2005.xml
				</xsl:otherwise>	
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- *************************************************************************************************************** -->
</xsl:stylesheet>
