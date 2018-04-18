<?xml version="1.0"?> 
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="/">
    <html>
      
      <script language="javascript">
		<![CDATA[
			function CloseWindow()
			{
				window.close(true);
			}
		]]>
	</script>
	<table border="0" cellSpacing='1' cellPadding='1' width='100%'>
	<tr bgcolor="bebebe">
		<td style="FONT-WEIGHT: normal; FONT-SIZE: 14pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial;  TEXT-ALIGN: left" colspan = '4'><b>Ebix Mutual Insurance Company</b></td> 
	  </tr>
	  <tr bgcolor="d3d3d3">
		<td style="FONT-WEIGHT: bold; FONT-SIZE: 10pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: center" colspan = '4'>Territory</td> 
	  </tr>
           <tr bgcolor="d3d3d3">
		<td style="FONT-WEIGHT: bold; FONT-SIZE: 10pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: center" colspan = '4'>Watercraft</td> 
	  </tr>
	  
	  <tr bgcolor="d3d3d3">
	  <td style="FONT-WEIGHT: bold;FONT-SIZE: 8pt; FONT-FAMILY: Verdana, helvetica, sans-serif; TEXT-ALIGN: left">Description</td> 
	  <td style="FONT-WEIGHT: bold;FONT-SIZE: 8pt; FONT-FAMILY: Verdana, helvetica, sans-serif; TEXT-ALIGN: left">Deductible</td> 
	  <!--<td style="FONT-WEIGHT: bold;FONT-SIZE: 8pt; FONT-FAMILY: Verdana, helvetica, sans-serif; TEXT-ALIGN: left">Factor</td> -->
	  <td style="FONT-WEIGHT: bold;FONT-SIZE: 8pt; FONT-FAMILY: Verdana, helvetica, sans-serif; TEXT-ALIGN: left">Limit</td> 
	  <td style="FONT-WEIGHT: bold;FONT-SIZE: 8pt; FONT-FAMILY: Verdana, helvetica, sans-serif; TEXT-ALIGN: left">Premium</td> 
	  <!--<td style="FONT-WEIGHT: bold;FONT-SIZE: 8pt; FONT-FAMILY: Verdana, helvetica, sans-serif; TEXT-ALIGN: left">Final Premium</td>-->
	</tr>
	
	<!-- Dates -->
	<tr bgcolor="ffffff">
	<td colspan = '2' style="FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: RIGHT" >Quoted On</td> 
	<td colspan = '2' style="FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: RIGHT">	<xsl:value-of select="PRIMIUM/QDATE/@PREDATE"/></td>   
	
	</tr>
	<tr bgcolor="ffffff">
	<td  colspan = '2' style="FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: RIGHT" >Quote Effective</td> 
	<td  colspan = '2' style="FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: RIGHT">	<xsl:value-of select="PRIMIUM/QDATE/@QEDATE"/></td>   
	
	</tr>
	<tr bgcolor="ffffff">
	<td  colspan = '2' style="FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: RIGHT" >Rates Effective</td> 
	<td  colspan = '2' style="FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: RIGHT"><xsl:value-of select="PRIMIUM/QDATE/@REDATE"/></td>   
	</tr>
	
	<!-- Premium  Calculation -->
	<xsl:apply-templates select="PRIMIUM"/>
	 </table>
    </html>
  </xsl:template>
  <xsl:template match="PRIMIUM">
    
  	  
      <xsl:apply-templates select="STEP"/>   
    
  </xsl:template>
 <xsl:template match="STEP">
	<xsl:choose>
		<xsl:when test="(@TS mod 2) = 0">
		<xsl:choose>
			<xsl:when test="(@STEPPREMIUM) = 0">
				<!--<tr bgcolor="ffdab9">
						<td style="FONT-WEIGHT: normal; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: left"><xsl:value-of select="@GROUPDESC"/><xsl:value-of select="@STEPDESC"/></td>
						<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: left"><xsl:value-of select="@DESC"/></td>
						<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@LIMITVALUE"/></td>
      					<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@DRDUCTIBLEVALUE"/></td>
	      				<td style="FONT-WEIGHT: bold; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@STEPPREMIUM"/></td>
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
				<xsl:when test ="@GROUPDESC='DESCRIPTIONNOTREQUIRED'">
					<!--tr bgcolor="ffdab9">
						<td style="FONT-WEIGHT: normal; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: left"></td>
						<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@LIMITVALUE"/></td>
      					<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@DRDUCTIBLEVALUE"/></td>
	      				<td style="FONT-WEIGHT: bold; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@STEPPREMIUM"/></td>
					</tr-->
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
		<xsl:choose>
			<xsl:when test="(@STEPPREMIUM) = 1 or (@STEPPREMIUM) = 0 or (@STEPPREMIUM) = ''">
				<!--<tr bgcolor="ffdab9">
					td style="FONT-WEIGHT: bold; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: left"><xsl:value-of select="@GROUPDESC"/><xsl:value-of select="@STEPDESC"/></td>
					<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: left"><xsl:value-of select="@DESC"/></td>
					<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@LIMITVALUE"/></td>
      				<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@DRDUCTIBLEVALUE"/></td>
					<td style="FONT-WEIGHT: bold; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@STEPPREMIUM"/></td>
				</tr>-->
			</xsl:when>
			<xsl:otherwise>
				<tr bgcolor="ffdab9">
					<td style="FONT-WEIGHT: bold; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: left"><xsl:value-of select="@GROUPDESC"/><xsl:value-of select="@STEPDESC"/></td>
					<!--<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: left"><xsl:value-of select="@DESC"/></td>-->
					<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@LIMITVALUE"/></td>
      				<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@DRDUCTIBLEVALUE"/></td>
					<td style="FONT-WEIGHT: bold; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@STEPPREMIUM"/></td>
				</tr>
			</xsl:otherwise>
		</xsl:choose>	
   	</xsl:when>
   	<xsl:otherwise>
   		<xsl:choose>
   			<!--<xsl:when test="(@STEPPREMIUM) = 1 or (@STEPPREMIUM) = 0">-->
			<xsl:when test="(@STEPPREMIUM) = 0">
				<!--<tr bgcolor="ffdab9">
						<td style="FONT-WEIGHT: normal; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: left"><xsl:value-of select="@GROUPDESC"/><xsl:value-of select="@STEPDESC"/></td>
						<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: left"><xsl:value-of select="@DESC"/></td>
						<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@LIMITVALUE"/></td>
      					<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@DRDUCTIBLEVALUE"/></td>
						<td style="FONT-WEIGHT: bold; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"><xsl:value-of select="@STEPPREMIUM"/></td>
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
 <xsl:template name="QDATE">
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




