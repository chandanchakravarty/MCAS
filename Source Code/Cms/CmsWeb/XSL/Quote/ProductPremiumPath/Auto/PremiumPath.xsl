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
<!--
	<xsl:choose>
	 
		<xsl:when test ="LOBNAME = 'AUTO' and EDATE = '03/01/2005' and  TYPE = 'DRIVER'		and POLICY/STATENAME = 'MICHIGAN' and VEHICLES/VEHICLE/VEHICLETYPEUSE ='PERSONAL' ">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_DriverComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'AUTO' and EDATE = '03/01/2005' and  TYPE = 'VEHICLE'	and POLICY/STATENAME = 'MICHIGAN' and VEHICLES/VEHICLE/VEHICLETYPEUSE ='PERSONAL'">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_VehicleComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'AUTO' and EDATE = '02/01/2006' and  TYPE = 'DRIVER'		and POLICY/STATENAME = 'MICHIGAN' and VEHICLES/VEHICLE/VEHICLETYPEUSE ='PERSONAL'">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_DriverComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'AUTO' and EDATE = '02/01/2006' and  TYPE = 'VEHICLE'	and POLICY/STATENAME = 'MICHIGAN' and VEHICLES/VEHICLE/VEHICLETYPEUSE ='PERSONAL'">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_VehicleComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'AUTO' and EDATE = '02/01/2006' and  TYPE = 'VIOLATION'	">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_ViolationComponent.xsl</xsl:when>
		
		<xsl:when test ="LOBNAME = 'AUTO' and EDATE = '03/01/2005' and  TYPE = 'DRIVER'		and POLICY/STATENAME = 'INDIANA' and VEHICLES/VEHICLE/VEHICLETYPEUSE ='PERSONAL'">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_DriverComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'AUTO' and EDATE = '03/01/2005' and  TYPE = 'VEHICLE'	and POLICY/STATENAME = 'INDIANA' and VEHICLES/VEHICLE/VEHICLETYPEUSE ='PERSONAL'">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_VehicleComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'AUTO' and EDATE = '02/01/2006' and  TYPE = 'DRIVER'		and POLICY/STATENAME = 'INDIANA' and VEHICLES/VEHICLE/VEHICLETYPEUSE ='PERSONAL'">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_DriverComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'AUTO' and EDATE = '02/01/2006' and  TYPE = 'VEHICLE'	and POLICY/STATENAME = 'INDIANA' and VEHICLES/VEHICLE/VEHICLETYPEUSE ='PERSONAL'">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_VehicleComponent.xsl</xsl:when>
		
		
		<xsl:when test ="LOBNAME = 'AUTO' and EDATE = '03/01/2005' and  TYPE = 'DRIVER'		and POLICY/STATENAME = 'MICHIGAN' and VEHICLES/VEHICLE/VEHICLETYPEUSE ='COMMERCIAL'">/cmsweb/XSL/Quote/PremiumPath/Auto/Commercial/Premium_DriverComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'AUTO' and EDATE = '03/01/2005' and  TYPE = 'VEHICLE'	and POLICY/STATENAME = 'MICHIGAN' and VEHICLES/VEHICLE/VEHICLETYPEUSE ='COMMERCIAL'">/cmsweb/XSL/Quote/PremiumPath/Auto/Commercial/Premium_VehicleComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'AUTO' and EDATE = '02/01/2006' and  TYPE = 'DRIVER'		and POLICY/STATENAME = 'MICHIGAN' and VEHICLES/VEHICLE/VEHICLETYPEUSE ='COMMERCIAL'">/cmsweb/XSL/Quote/PremiumPath/Auto/Commercial/Premium_DriverComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'AUTO' and EDATE = '02/01/2006' and  TYPE = 'VEHICLE'	and POLICY/STATENAME = 'MICHIGAN' and VEHICLES/VEHICLE/VEHICLETYPEUSE ='COMMERCIAL'">/cmsweb/XSL/Quote/PremiumPath/Auto/Commercial/Premium_VehicleComponent.xsl</xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
	-->
	
	 
	 
		
		 
<!-- ******************************************************************************************** -->
<!-- ***********************|           FOR PERSONAL         |********************************* -->
<!-- ******************************************************************************************** -->
			
		 <xsl:choose> 
		 
		  <!-- FOR MICHIGAN -->
			<xsl:when test="(VEHICLES/VEHICLE/VEHICLETYPEUSE ='PERSONAL' and POLICY/STATENAME = 'MICHIGAN') or STATENAME='MICHIGAN'">
				<xsl:choose>
					<xsl:when test="EDATE = '04/01/2005' and  TYPE = 'DRIVER'">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_DriverComponent.xsl</xsl:when>
					<xsl:when test="EDATE = '04/01/2005' and  TYPE = 'VEHICLE'">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_VehicleComponent.xsl</xsl:when>
					<xsl:when test="EDATE = '08/01/2005' and  TYPE = 'DRIVER'">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_DriverComponent.xsl</xsl:when>
					<xsl:when test="EDATE = '08/01/2005' and  TYPE = 'VEHICLE'">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_VehicleComponent.xsl</xsl:when>
					<xsl:when test="EDATE = '12/01/2005' and  TYPE = 'DRIVER'">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_DriverComponent.xsl</xsl:when>
					<xsl:when test="EDATE = '12/01/2005' and  TYPE = 'VEHICLE'">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_VehicleComponent.xsl</xsl:when>
				<xsl:otherwise> 
						<xsl:choose> 
							<xsl:when test="TYPE = 'DRIVER'">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_DriverComponent.xsl</xsl:when>
							<xsl:when test="TYPE = 'VEHICLE'">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_VehicleComponent.xsl</xsl:when>
							<xsl:when test="TYPE = 'VIOLATION'">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_ViolationComponent.xsl</xsl:when>
						</xsl:choose>	
				</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			
			<!-- FOR INDIANA -->
			<xsl:when test="(VEHICLES/VEHICLE/VEHICLETYPEUSE ='PERSONAL' and POLICY/STATENAME = 'INDIANA') or STATENAME='INDIANA'">
				<xsl:choose>
					<xsl:when test="EDATE = '10/01/2004' and  TYPE = 'DRIVER'">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_DriverComponent.xsl</xsl:when>
					<xsl:when test="EDATE = '10/01/2004' and  TYPE = 'VEHICLE'">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_VehicleComponent_12_2009.xsl</xsl:when>
					<xsl:when test="EDATE = '04/01/2005' and  TYPE = 'DRIVER'">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_DriverComponent.xsl</xsl:when>
					<xsl:when test="EDATE = '04/01/2005' and  TYPE = 'VEHICLE'">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_VehicleComponent_12_2009.xsl</xsl:when>
					<xsl:when test="EDATE = '08/01/2005' and  TYPE = 'DRIVER'	">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_DriverComponent.xsl</xsl:when>
					<xsl:when test="EDATE = '08/01/2005' and  TYPE = 'VEHICLE'">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_VehicleComponent_12_2009.xsl</xsl:when>
					<xsl:when test="EDATE = '12/01/2005' and  TYPE = 'DRIVER'">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_DriverComponent.xsl</xsl:when>
					<xsl:when test="EDATE = '12/01/2005' and  TYPE = 'VEHICLE'">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_VehicleComponent_12_2009.xsl</xsl:when>
					<xsl:when test="EDATE = '01/01/2010' and  TYPE = 'DRIVER'">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_DriverComponent.xsl</xsl:when>
					<xsl:when test="EDATE = '01/01/2010' and  TYPE = 'VEHICLE'">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_VehicleComponent.xsl</xsl:when>
					<xsl:when test="TYPE = 'VIOLATION'	">/cmsweb/XSL/Quote/PremiumPath/Auto/Personal/Premium_ViolationComponent.xsl</xsl:when>
				</xsl:choose>
			</xsl:when>
			
			<!-- ******************************************************************************************** -->
			<!-- ***********************|           FOR COMMERCIAL         |********************************* -->
			<!-- ******************************************************************************************** -->
			
			<!-- FOR MICHIGAN -->
			<xsl:when test="(VEHICLES/VEHICLE/VEHICLETYPEUSE ='COMMERCIAL' and POLICY/STATENAME = 'MICHIGAN') or STATENAME='MICHIGAN'">
				<xsl:choose>
					
					<xsl:when test="EDATE = '08/01/2005' and  TYPE = 'DRIVER'">/cmsweb/XSL/Quote/PremiumPath/Auto/Commercial/Premium_DriverComponent.xsl</xsl:when>
					<xsl:when test="EDATE = '08/01/2005' and  TYPE = 'VEHICLE'">/cmsweb/XSL/Quote/PremiumPath/Auto/Commercial/Premium_VehicleComponent.xsl</xsl:when>
					
					
					<xsl:when test="TYPE = 'VIOLATION'	">/cmsweb/XSL/Quote/PremiumPath/Auto/Commercial/Premium_ViolationComponent.xsl</xsl:when>
				</xsl:choose>
			</xsl:when>
			
			<!-- FOR INDIANA -->
			<xsl:when test="(VEHICLES/VEHICLE/VEHICLETYPEUSE ='COMMERCIAL' and POLICY/STATENAME = 'INDIANA') or STATENAME='INDIANA'">
				<xsl:choose>
				 
					<xsl:when test="EDATE = '12/01/2005' and  TYPE = 'DRIVER'	">/cmsweb/XSL/Quote/PremiumPath/Auto/Commercial/Premium_DriverComponent.xsl</xsl:when>
					<xsl:when test="EDATE = '12/01/2005' and  TYPE = 'VEHICLE'">/cmsweb/XSL/Quote/PremiumPath/Auto/Commercial/Premium_VehicleComponent.xsl</xsl:when>
					<xsl:when test="TYPE = 'VIOLATION'">/cmsweb/XSL/Quote/PremiumPath/Auto/Commercial/Premium_ViolationComponent.xsl</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
</xsl:template>
</xsl:stylesheet>