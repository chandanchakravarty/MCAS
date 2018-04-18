<?xml version="1.0" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:user="urn:user-namespace-here">
	<msxsl:script language="c#" implements-prefix="user">
<![CDATA[ 
		int intTotalPremium ;
		public int CalculatePremium(int tt)
		{
			intTotalPremium = intTotalPremium + tt;
			return intTotalPremium;
		}
		
]]></msxsl:script>
	<xsl:template match="/">
		<html>
			<head>
				<title>Wolverine Premiums</title>
			</head>
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
					<td style="FONT-WEIGHT: normal; FONT-SIZE: 14pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial;  TEXT-ALIGN: left"
						colspan='4'>
						<b>Wolverine Mutual Insurance Company</b>
					</td>
				</tr>
				<tr bgcolor="d3d3d3">
					<td style="FONT-WEIGHT: bold; FONT-SIZE: 10pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: center"
						colspan='4'>Territory</td>
				</tr>
				<tr bgcolor="d3d3d3">
					<td style="FONT-WEIGHT: bold; FONT-SIZE: 10pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: center"
						colspan='4'>Watercraft</td>
				</tr>
				<tr bgcolor="d3d3d3">
					<td style="FONT-WEIGHT: bold;FONT-SIZE: 8pt; FONT-FAMILY: Verdana, helvetica, sans-serif; TEXT-ALIGN: left">Description</td>
					<td style="FONT-WEIGHT: bold;FONT-SIZE: 8pt; FONT-FAMILY: Verdana, helvetica, sans-serif; TEXT-ALIGN: left">Deductible</td>
					<td style="FONT-WEIGHT: bold;FONT-SIZE: 8pt; FONT-FAMILY: Verdana, helvetica, sans-serif; TEXT-ALIGN: left">Limit</td>
					<td style="FONT-WEIGHT: bold;FONT-SIZE: 8pt; FONT-FAMILY: Verdana, helvetica, sans-serif; TEXT-ALIGN: left">Premium</td>
				</tr>
				<!-- Dates -->
				<tr bgcolor="ffffff">
					<td colspan='2' style="FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: RIGHT">Quoted On</td>
					<td colspan='2' style="FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: RIGHT">
						<xsl:value-of select="PRIMIUM/QDATE/@PREDATE" />
					</td>
				</tr>
				<tr bgcolor="ffffff">
					<td colspan='2' style="FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: RIGHT">Quote Effective</td>
					<td colspan='2' style="FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: RIGHT">
						<xsl:value-of select="PRIMIUM/QDATE/@QEDATE" />
					</td>
				</tr>
				<tr bgcolor="ffffff">
					<td colspan='2' style="FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: RIGHT">Rates Effective</td>
					<td colspan='2' style="FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: RIGHT">
						<xsl:value-of select="PRIMIUM/QDATE/@REDATE" />
					</td>
				</tr>
				<!-- Premium  Calculation -->
				<xsl:apply-templates select="PRIMIUM" />
			</table>
		</html>
	</xsl:template>
	<xsl:template match="PRIMIUM">
		<xsl:apply-templates select="STEP" />
		<tr bgcolor="ffdab9">
			<td>.</td>
			<td></td>
			<td></td>
			<td>.</td>
		</tr>
		<tr bgcolor="ffdab9">
			<td style="FONT-WEIGHT: BOLD;FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: LEF">Final Premium</td>
			<td></td>
			<td></td>
			<td style="FONT-WEIGHT: BOLD;FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right">
				<xsl:value-of select="user:CalculatePremium(0)" />
			</td>
		</tr>
	</xsl:template>
	<xsl:template match="STEP">
		<xsl:choose>
			<xsl:when test="@GROUPDESC =''">
				<xsl:choose>
					<xsl:when test="@STEPDESC = 'Total Outboard Premium' or @STEPDESC = 'Total Inboard Premium' or @STEPDESC = 'Total Inboard/Outboard Premium' or @STEPDESC = 'Total Sailboat Premium' or @STEPDESC = 'Total Jet Ski Premium' or @STEPDESC = 'Total Waverunner Premium' or @STEPDESC = 'Total Jetski Trailer Premium' or @STEPDESC = 'Total WaveRunner Trailer Premium'">
						<tr>
							<td style="HEIGHT: 1px; display= 'none'">
								<xsl:value-of select="user:CalculatePremium(@STEPPREMIUM)" />
							</td>
						</tr>
						<tr bgcolor="ffdab9">
							<td style="FONT-WEIGHT: BOLD; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: left">
								<xsl:value-of select="@GROUPDESC" />
								<xsl:value-of select="@STEPDESC" />
							</td>
							<td style="FONT-WEIGHT: BOLD; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right">
								<xsl:value-of select="@LIMITVALUE" />
							</td>
							<td style="FONT-WEIGHT: BOLD; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right">
								<xsl:value-of select="@DRDUCTIBLEVALUE" />
							</td>
							<td style="FONT-WEIGHT: BOLD; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right">
								<xsl:value-of select="@STEPPREMIUM" />
							</td>
						</tr>
						<tr>
							<td style="HEIGHT: 1px; display= 'none'"></td>
						</tr>
					</xsl:when>
					<xsl:when test="(@STEPPREMIUM) = '0'"></xsl:when>
					<xsl:otherwise>
						<tr bgcolor="ffdab9">
							<td style="FONT-WEIGHT: normal; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: left">
								<xsl:value-of select="@STEPDESC" />
							</td>
							<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right">
								<xsl:value-of select="@LIMITVALUE" />
							</td>
							<td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right">
								<xsl:value-of select="@DRDUCTIBLEVALUE" />
							</td>
							<td style="FONT-WEIGHT: bold; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right">
								<xsl:value-of select="@STEPPREMIUM" />
							</td>
						</tr>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="@GROUPDESC !='' and @GROUPDESC != 'DESCRIPTIONNOTREQUIRED' and @GROUPDESC != 'Final Premium'">
				<tr bgcolor="ffdab9">
					<td style="FONT-WEIGHT: BOLD; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: left">
						<xsl:value-of select="@GROUPDESC" />
					</td>
					<td style="FONT-WEIGHT: BOLD; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right">
						<xsl:value-of select="@LIMITVALUE" />
					</td>
					<td style="FONT-WEIGHT: BOLD; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right">
						<xsl:value-of select="@DRDUCTIBLEVALUE" />
					</td>
					<td style="FONT-WEIGHT: BOLD; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right">
						<xsl:value-of select="@STEPPREMIUM" />
					</td>
				</tr>
			</xsl:when>
			<xsl:when test="@GROUPDESC='DESCRIPTIONNOTREQUIRED'"></xsl:when>
			<xsl:when test="@GROUPDESC='Final Premium'"></xsl:when>
			<xsl:when test="(@STEPPREMIUM) = '0' or (@STEPPREMIUM) = ''"></xsl:when>
			<xsl:otherwise>
				<tr bgcolor="ffdab9">
					<td style="FONT-WEIGHT: bold; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: left">
						<xsl:value-of select="@GROUPDESC" />
						<xsl:value-of select="@STEPDESC" />
						<xsl:value-of select="@STEPDESC" />
					</td>
					<td style="FONT-WEIGHT: bold; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right">
						<xsl:value-of select="@LIMITVALUE" />
					</td>
					<td style="FONT-WEIGHT: bold; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right">
						<xsl:value-of select="@DRDUCTIBLEVALUE" />
					</td>
					<td style="FONT-WEIGHT: bold; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right">
						<xsl:value-of select="@STEPPREMIUM" />
					</td>
				</tr>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>