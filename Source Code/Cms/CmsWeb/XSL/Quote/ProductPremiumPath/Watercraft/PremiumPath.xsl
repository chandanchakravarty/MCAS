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
	 	
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '03/01/2005' and TYPE =  'BOAT'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_BoatComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '03/01/2005' and TYPE =  'OPERATOR'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_OperatorComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '03/01/2005' and TYPE =  'VIOLATION'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_ViolationComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '03/01/2005' and TYPE =  'SCHEDULEDEQUIPMENT'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_ScheduledEquipmentComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '03/01/2005' and TYPE =  'ENDORSEMENTS'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_EndorsementsComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '03/01/2005' and TYPE =  'POLICY'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_PolicyComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '03/01/2005' and TYPE =  'UNATTACHEDEQUIPMENT'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_UnattachedEquipmentComponent.xsl</xsl:when>
		
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '03/01/2006' and TYPE =  'BOAT'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_BoatComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '03/01/2006' and TYPE =  'OPERATOR'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_OperatorComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '03/01/2006' and TYPE =  'VIOLATION'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_ViolationComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '03/01/2006' and TYPE =  'SCHEDULEDEQUIPMENT'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_ScheduledEquipmentComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '03/01/2006' and TYPE =  'ENDORSEMENTS'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_EndorsementsComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '03/01/2006' and TYPE =  'POLICY'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_PolicyComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '03/01/2006' and TYPE =  'UNATTACHEDEQUIPMENT'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_UnattachedEquipmentComponent.xsl</xsl:when>
<!--

		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '04/01/2005' and TYPE =  'BOAT'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_BoatComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '04/01/2005' and TYPE =  'OPERATOR'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_OperatorComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '04/01/2005' and TYPE =  'VIOLATION'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_ViolationComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '04/01/2005' and TYPE =  'SCHEDULEDEQUIPMENT'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_ScheduledEquipmentComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '04/01/2005' and TYPE =  'ENDORSEMENTS'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_EndorsementsComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '04/01/2005' and TYPE =  'POLICY'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_PolicyComponent.xsl</xsl:when>
		
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '08/01/2005' and TYPE =  'BOAT'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_BoatComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '08/01/2005' and TYPE =  'OPERATOR'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_OperatorComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '08/01/2005' and TYPE =  'VIOLATION'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_ViolationComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '08/01/2005' and TYPE =  'SCHEDULEDEQUIPMENT'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_ScheduledEquipmentComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '08/01/2005' and TYPE =  'ENDORSEMENTS'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_EndorsementsComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '08/01/2005' and TYPE =  'POLICY'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_PolicyComponent.xsl</xsl:when>

		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '12/01/2005' and TYPE =  'BOAT'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_BoatComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '12/01/2005' and TYPE =  'OPERATOR'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_OperatorComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '12/01/2005' and TYPE =  'VIOLATION'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_ViolationComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '12/01/2005' and TYPE =  'SCHEDULEDEQUIPMENT'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_ScheduledEquipmentComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '12/01/2005' and TYPE =  'ENDORSEMENTS'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_EndorsementsComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '12/01/2005' and TYPE =  'POLICY'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_PolicyComponent.xsl</xsl:when>

		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '10/01/2004' and TYPE =  'BOAT'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_BoatComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '10/01/2004' and TYPE =  'OPERATOR'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_OperatorComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '10/01/2004' and TYPE =  'VIOLATION'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_ViolationComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '10/01/2004' and TYPE =  'SCHEDULEDEQUIPMENT'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_ScheduledEquipmentComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '10/01/2004' and TYPE =  'ENDORSEMENTS'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_EndorsementsComponent.xsl</xsl:when>
		<xsl:when test ="LOBNAME = 'BOAT' and EDATE = '10/01/2004' and TYPE =  'POLICY'">/cmsweb/XSL/Quote/PremiumPath/Watercraft/Premium_PolicyComponent.xsl</xsl:when>

-->
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:template>
</xsl:stylesheet>