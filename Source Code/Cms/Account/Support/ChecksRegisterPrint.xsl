<?xml version="1.0"?> 

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="urn:user-namespace-here">
<msxsl:script language="C#" implements-prefix="user"><![CDATA[ 
		 int glbl = 0;
		 int temp = 0;
		private int gettotal(int a  )
		{
			int flag = 0;
			
			if(a == temp){
				flag = 0;
			} else{
				flag = 1;
			}
			
			++glbl;
			temp = a;
			
		return flag;
		}
		
]]></msxsl:script>

<xsl:template match="/">
	<table width="100%" border='0' cellspacing="1" cellpadding="0">
		<tr>
			<td class="headereffectcenter" colspan="8">
		Checks Register
			</td>
		</tr>
		<tr>
			<td class="midcolorc" colspan="8">
				<b>Today's Date : </b> <xsl:value-of select="//NewDataSet/Date"/>
			</td>
		</tr>
		<tr>
			<td class="midcolorc" colspan="8">
				<b>Time : </b> <xsl:value-of select="//NewDataSet/Time"/>
		</td>
		</tr>
	
		<xsl:for-each select="//NewDataSet/Table">
			
			<xsl:variable name="check2" select="CHECK_TYPE"/>
				<xsl:choose>
				<xsl:when test= "user:gettotal($check2) = 0" >
					<xsl:call-template name ="Row"/>
				</xsl:when>	
				<xsl:otherwise>
					<xsl:call-template name ="Header">
						<xsl:with-param name="CheckType" select="CHECK_TYPE"></xsl:with-param>
					</xsl:call-template>
					<xsl:call-template name ="Row"/>
				</xsl:otherwise>
				</xsl:choose>	 
				
					
					
		 		
		</xsl:for-each>
	</table>

</xsl:template>

<xsl:template name="Header">
	<xsl:param name="CheckType" />
	<tr>
		<td class="midcolora" colspan="8" width="100%" height="12px"></td>
	</tr>
	<tr>
		<td class="midcolora" colspan="1" width="10%"><b>Check Type</b></td>
		<td class="midcolora" colspan="7" width="90%">
			<xsl:value-of select="./CHECK_TYPE_DESC"/>
		</td>
	</tr>
	<tr>
		<TD class="HeadRow">Account #</TD>
		<TD class="HeadRow">Check #</TD>
		<TD class="HeadRow">Check Date</TD>
		<TD class="HeadRow">Payee</TD>
		<TD class="HeadRow">Amount</TD>
		<TD class="HeadRow">Staus</TD>
		<TD class="HeadRow">Offset A/c #</TD>
		<TD class="HeadRow">Cleared</TD>
	</tr>
</xsl:template>

<xsl:template name="Row">
	<tr>
	<td class="midcolora" colspan="1" ><!--<xsl:value-of select="CHECK_TYPE=number($CheckType)" />:<xsl:value-of select="$CheckType" />: <xsl:value-of select="./CHECK_TYPE" />:-->
		<xsl:value-of select="./ACC_DISP_NUMBER"/>
	</td>

	<td class="midcolora" colspan="1" >
		<xsl:value-of select="./CHECK_NUMBER"/>
	</td>

	<td class="midcolora" colspan="1" >
		<xsl:value-of select="./CHECK_DATE"/>
	</td>

	<td class="midcolora" colspan="1" >
		<xsl:value-of select="./PAYEE_ENTITY_NAME"/>
	</td>

	<td class="midcolora" colspan="1" >
		<xsl:value-of select="./CHECK_AMOUNT"/>
	</td>

	<td class="midcolora" colspan="1" >
		<xsl:value-of select="./Staus"/>
	</td>

	<td class="midcolora" colspan="1" >
		<xsl:value-of select="./OFFSET_ACC_ID"/>
	</td>
	<td class="midcolora" colspan="1" >
		<xsl:value-of select="./Cleared"/>
	</td>
	</tr>
</xsl:template>

</xsl:stylesheet>