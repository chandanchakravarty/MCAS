 

<!-- ==================================================================================================
File Name			:	RentalInputVerification.XSL 
Purpose				:	Rental Input Verification 
Name				:	Praveen Singh
Date				:	17 july 2006  
======================================================================================================== -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:user="urn:user-namespace-here">
	<xsl:output encoding="ISO8859-1" omit-xml-declaration="no" method="xml" />
	<!-- ============================================================================= -->
	<msxsl:script language="c#" implements-prefix="user">
<![CDATA[ 
		
		int intCSS=1;
		public int ApplyColor(int colorvalue)
		{
			intCSS=colorvalue;			
			return intCSS;
		}
		int statusFlag=1;
		 
		 
		int intIsRejected = 1;
		public int IsApplicationAcceptable(int tt)
		{
			intIsRejected = intIsRejected*tt;
			return intIsRejected;
		}
		int intIsReferred = 1;
		public int CheckRefer(int tt)
		{
			intIsReferred = intIsReferred*tt;
			return intIsReferred;
		}
		 
		int intVerifyXML=1;
		public int AllVerified(int status)
		{
			intVerifyXML=intVerifyXML * status;			
			return intVerifyXML;
		}	 
		


			
]]></msxsl:script>

 

	<!-- ============================================================================= -->
		

<xsl:variable name="HOCoveragesDoc" select="document('FactorPath')"/>
 
<!-- ============================================================================================ 
								Loading ProductFactorMaster File (END)						  
 ============================================================================================ -->
		 
	<xsl:template match="/">
		<html>
			<head>
				<xsl:variable name="myName" select="QUICKQUOTE/CSSNUM/@CSSVALUE"></xsl:variable> 
 				<xsl:if test="user:ApplyColor($myName) = 0"></xsl:if>
				<xsl:choose>
					<xsl:when test="user:ApplyColor($myName) = 1">
						<LINK id="lk" href="/cms/cmsweb/css/css1.css" type="text/css" rel="stylesheet" />
					</xsl:when>
					<xsl:when test="user:ApplyColor($myName) = 2">
						<LINK href="/cms/cmsweb/css/css2.css" type="text/css" rel="stylesheet" />
					</xsl:when>
					<xsl:when test="user:ApplyColor($myName) = 3">
						<LINK href="/cms/cmsweb/css/css3.css" type="text/css" rel="stylesheet" />
					</xsl:when>
					<xsl:when test="user:ApplyColor($myName) = 4">
						<LINK href="/cms/cmsweb/css/css4.css" type="text/css" rel="stylesheet" />
					</xsl:when>
					<xsl:otherwise>
						<LINK href="/cms/cmsweb/css/css1.css" type="text/css" rel="stylesheet" />
					</xsl:otherwise>
				</xsl:choose>
			</head>
			<body class="midcolora" bgcolor="ffffff">
				<br/>
				<xsl:apply-templates select="QUICKQUOTE" />
			</body>
		<span style="display:none">
			<returnValue>
				<xsl:value-of select="user:AllVerified(1)" />
			</returnValue>
		</span>
		
		</html>
		
		
		
		
	</xsl:template>
	
	
<xsl:template match="QUICKQUOTE">
		
		<table border="0" align="center" width='90%'>
		<tr class="headereffectCenter">
			<td colspan='6' style="FONT-SIZE: 14pt">
				<b>
					Error Report
				</b>
			</td>
		</tr>
		<tr><td></td></tr>
		<tr><td></td></tr>
		<tr><td></td></tr>
		
		</table>
		
		<table border="0" align="center" width='90%' bgcolor="ffffff">
		  <ul type="disc"> 
		  	 <tr>
				<td class="midcolora">
				    <xsl:call-template name="MAIN_FOR_INPUT_XML"/>  
				  
				</td>
			</tr>
		 </ul>
		</table>
		
	</xsl:template>
	
	
 <xsl:template name="MAIN_FOR_INPUT_XML">
 <xsl:value-of select="user:AllVerified(1)" />
 </xsl:template>
</xsl:stylesheet>