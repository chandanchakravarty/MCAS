<!--======================================================================================================
File Name		: FinalQuote.xsl
Purpose			: Implemetating stylesheet
Date			: 26 Oct. 2005
Developed By	:Ashwani 
 ======================================================================================================-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:template match="/">
<html>
	<table border="0" cellSpacing='1' cellPadding='1' width='100%'>
		<xsl:apply-templates select="PREMIUMXML"/>
	</table>
</html>
</xsl:template>
<xsl:template match="PREMIUMXML">
	<tr bgcolor="bebebe">
		<td style="FONT-WEIGHT: normal; FONT-SIZE: 14pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial;  TEXT-ALIGN: center" colspan = '6'><b>Ebix Mutual Insurance Company</b></td> 
	</tr>
	<tr bgcolor="d3d3d3">
		<td style="FONT-WEIGHT: bold; FONT-SIZE: 10pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: center" colspan = '6'>Rental Dwelling Quote ( <xsl:value-of select = "DWELLINGDETAILS/STEP/@PRODUCTDESC"/> )</td> 
	</tr>
	<tr>
		<xsl:apply-templates select="QDATE"/>		
	</tr>  					 
		<xsl:apply-templates select="DWELLINGDETAILS"/>  
	<tr>
		<td colspan='6'><hr></hr></td>
	</tr>
	<tr  bgcolor="d3d3d3" >	 
		<!--<td colspan="5" style="FONT-WEIGHT: bold; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right">Total Premium= <xsl:value-of select = "sum(//STEP[@GROUPVAL='Final Premium']/@STEPPREMIUM)"/> </td>-->
	</tr>
</xsl:template>
<xsl:template match="DWELLINGDETAILS"> 
	<tr> 
		<td style="FONT-WEIGHT: bold;FONT-SIZE: 8pt; FONT-FAMILY: Verdana, helvetica, sans-serif; TEXT-ALIGN: left"> Dwelling: <xsl:value-of select="@ADDRESS"/></td>
	</tr>
	<tr bgcolor="d3d3d3">
		<td style="FONT-WEIGHT: bold;FONT-SIZE: 8pt; FONT-FAMILY: Verdana, helvetica, sans-serif; TEXT-ALIGN: left">Description</td> 
		<td style="FONT-WEIGHT: bold;FONT-SIZE: 8pt; FONT-FAMILY: Verdana, helvetica, sans-serif; TEXT-ALIGN: left">Deductible</td> 
		<!--<td style="FONT-WEIGHT: bold;FONT-SIZE: 8pt; FONT-FAMILY: Verdana, helvetica, sans-serif; TEXT-ALIGN: left">Factor</td> -->
		<td style="FONT-WEIGHT: bold;FONT-SIZE: 8pt; FONT-FAMILY: Verdana, helvetica, sans-serif; TEXT-ALIGN: left">Limit</td> 
		<td style="FONT-WEIGHT: bold;FONT-SIZE: 8pt; FONT-FAMILY: Verdana, helvetica, sans-serif; TEXT-ALIGN: right">Premium</td> 
	</tr>	
	<xsl:apply-templates select="STEP"/>
</xsl:template> 
<xsl:template match="STEP">
<xsl:choose>
<xsl:when test="(@TS mod 2) = 0">
	<xsl:choose>
	<xsl:when test="(@STEPPREMIUM) = 0">
		<!--<tr bgcolor="ffdab9">
		<td></td>
		<td></td>
		<td></td>
		<td></td>
		</tr>-->
	</xsl:when>
	<xsl:otherwise>
		<xsl:choose>
			<xsl:when test ="@GROUPDESC =''">
			<tr bgcolor="ffdab9">
				<td style="FONT-WEIGHT: normal; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: left"><xsl:value-of select="@GROUPDESC"/><xsl:value-of select="@STEPDESC"/></td>
				<!--<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: left"><xsl:value-of select="@DESC"/></td>-->
				<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@LIMITVALUE"/></td>
				<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@DRDUCTIBLEVALUE"/></td>
				<td style="FONT-WEIGHT: bold; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@STEPPREMIUM"/></td>
			</tr>
			</xsl:when>
			<xsl:otherwise>
			<tr bgcolor="ffdab9">
				<td style="FONT-WEIGHT: bold; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: left"><xsl:value-of select="@GROUPDESC"/><xsl:value-of select="@STEPDESC"/></td>
				<!--<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: left"><xsl:value-of select="@DESC"/></td>-->
				<td style="FONT-WEIGHT: bold; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@LIMITVALUE"/></td>
				<td style="FONT-WEIGHT: bold; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@DRDUCTIBLEVALUE"/></td>
				<td style="FONT-WEIGHT: bold; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@STEPPREMIUM"/></td>
			</tr>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:otherwise>
</xsl:choose>
</xsl:when>
<xsl:when test="(@TS) = 'Final Premium'">
	<!--<xsl:choose>
	<xsl:when test="(@STEPPREMIUM) = 1 or (@STEPPREMIUM) = 0">
		<tr bgcolor="d3d3d3">
		<td></td>
		<td></td>
		<td></td>
		<td></td>
		</tr>
	</xsl:when>
	<xsl:otherwise>-->
	<tr bgcolor="ffdab9">
		<td style="FONT-WEIGHT: bold; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: left"><xsl:value-of select="@GROUPDESC"/><xsl:value-of select="@STEPDESC"/></td>
		<!--<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: left"><xsl:value-of select="@DESC"/></td>-->
		<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@LIMITVALUE"/></td>
		<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@DRDUCTIBLEVALUE"/></td>
		<td style="FONT-WEIGHT: bold; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@STEPPREMIUM"/></td>
	</tr>
	<!--</xsl:otherwise>
	</xsl:choose>-->	
</xsl:when>
<xsl:otherwise>
	<xsl:choose>
		<!--<xsl:when test="(@STEPPREMIUM) = 1 or (@STEPPREMIUM) = 0">-->
		<xsl:when test="(@STEPPREMIUM) = 0">
			<!--<tr bgcolor="ffdab9">
			<td></td>
			<td></td>
			<td></td>
			<td></td>
			</tr>-->
		</xsl:when>
		<xsl:otherwise>
			<xsl:choose>
			<xsl:when test ="@GROUPDESC =''">
				<tr bgcolor="ffdab9">
					<td style="FONT-WEIGHT: normal; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: left"><xsl:value-of select="@GROUPDESC"/><xsl:value-of select="@STEPDESC"/></td>
					<!--<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: left"><xsl:value-of select="@DESC"/></td>-->
					<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@LIMITVALUE"/></td>
					<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@DRDUCTIBLEVALUE"/></td>
					<td style="FONT-WEIGHT: bold; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@STEPPREMIUM"/></td>
				</tr>
			</xsl:when>
			<xsl:otherwise>
				<tr bgcolor="ffdab9">
					<td style="FONT-WEIGHT: bold; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: left"><xsl:value-of select="@GROUPDESC"/><xsl:value-of select="@STEPDESC"/></td>
					<!--<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: left"><xsl:value-of select="@DESC"/></td>-->
					<td style="FONT-WEIGHT: bold; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@LIMITVALUE"/></td>
					<td style="FONT-WEIGHT: bold; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@DRDUCTIBLEVALUE"/></td>
					<td style="FONT-WEIGHT: bold; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@STEPPREMIUM"/></td>
				</tr>	
			</xsl:otherwise>
		</xsl:choose>
	</xsl:otherwise>
	</xsl:choose>
</xsl:otherwise>
</xsl:choose>
</xsl:template>
<xsl:template match="QDATE">
	<tr bgcolor="ffffff">
		<td colspan = '2' style="FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: RIGHT" >Quoted On</td> 
		<td colspan = '2' style="FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: RIGHT">	<xsl:value-of select="@PREDATE"/></td>   
	</tr>
	<tr bgcolor="ffffff">
		<td  colspan = '2' style="FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: RIGHT" >Quote Effective</td> 
		<td  colspan = '2' style="FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: RIGHT">	<xsl:value-of select="@QEDATE"/></td>   
	</tr>
	<tr bgcolor="ffffff">
		<td  colspan = '2' style="FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: RIGHT" >Rates Effective</td> 
		<td  colspan = '2' style="FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: RIGHT"><xsl:value-of select="@REDATE"/></td>   
	</tr>
</xsl:template>
</xsl:stylesheet>




