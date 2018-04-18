<?xml version="1.0" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:msxsl="urn:schemas-microsoft-com:xslt"
                xmlns:js="urn:custom-javascript"
	              xmlns:user="urn:user-namespace-here">
  <msxsl:script language="c#" implements-prefix="user">
    <![CDATA[ 
		
		int intCSS=1;
		public int ApplyColor(int colorvalue)
		{
			intCSS=colorvalue;			
			return intCSS;
		}
		
		Double intTotalPremium ;
		public Double CalculatePremium(Double tt)
		{
			intTotalPremium = intTotalPremium + tt;
			return intTotalPremium;
		}
		int intTotalamp=0;
		public int Calculateamp(int tt)
		{
			intTotalamp = intTotalamp + tt;
			return intTotalamp;
		}
    string  tempdesc="";
		public string RetunDescription(string desc)
    {
      if(tempdesc!=desc)
      { 
         tempdesc= desc;
         return desc;
      }else
      {
          return "";
      }
    }
    string strPolicycurrency;
    public void PolicyCurrency(string currency)
    {
       strPolicycurrency = currency;
       
    }
    public string Amountformat(string amount)
    {
      if(strPolicycurrency=="2")
      {
         amount =  amount.Replace(",", "~").Replace(".", "^").Replace("~", ".").Replace("^", ",");
         return amount;
      }else
      {
          return amount;
      }
    }
    String PolicyTotalAmount;
    public string GetTotalPremium(string Amount)
    {  
        if(Amount != "")
          PolicyTotalAmount = Amount;
          
       return PolicyTotalAmount;
    }
]]>
  </msxsl:script>

  <!--<msxsl:script language="JavaScript" implements-prefix="js">
    <![CDATA[
    
      function printReport()
      {
        window.print();
      }
    
    
    ]]>
    
  </msxsl:script>-->

    <!--<xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>-->
  <xsl:template match="/">
  <html>
    <head>
      <xsl:variable name="Col">
        <!--<xsl:value-of select="PRIMIUM/HEADER/@CSSVALUE"></xsl:value-of>-->
        <xsl:text>1</xsl:text>
      </xsl:variable>
      <xsl:variable name="myName">
        <xsl:choose>
          <xsl:when test="$Col = '' or $Col=' ' or $Col &lt; 0 ">
            <xsl:value-of select="1" />
          </xsl:when>
          <xsl:otherwise>
            <xsl:value-of select="1" />
          </xsl:otherwise>
        </xsl:choose>
      </xsl:variable>
      <!-- <xsl:variable name="myName" select="1"/> -->
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
      <title>Ebix Advantage</title>
    </head>
    <script language="javascript">
      <![CDATA[
		
		
			function CloseWindow()
			{
				window.close(true);
			}
			function printReport()
			{
				window.print();
			}
			
		]]>
    </script>
    <body bgcolor="ffffff">
      <table border="0" cellSpacing='1' cellPadding='1' align="center" width='100%'>
        <tr class="midcolora" align="center">
          <!--GROUP HEADING-->
          <td align="center" colspan="5">
            <b>
              <xsl:text>Private Motor Premium Calculation Summary</xsl:text>
            </b>
          </td>
        </tr>
        <tr class="headereffectCenter">
          <!--GROUP HEADING-->
          <td align="left" colspan="5">
            <xsl:text>Premium Calculation Summary - Reference No :</xsl:text>
            <xsl:value-of select="QUICKQUOTE/POLICY/QQ_NUMBER"/>
          </td>          
        </tr>
        <tr class="midcolora">
          <td width="20%" align="left" >
            <xsl:text>Basic Premium Before Demerit Points</xsl:text>
        </td>
          <td width="20%">
            <xsl:text>&#160;</xsl:text>
          </td>
          <td width="20%">
            <xsl:text>&#160;</xsl:text>
          </td>
          <td width="5%">S$</td>
          <td align="right" width="35%">
            <xsl:value-of select="format-number((QUICKQUOTE/POLICY/RECEIVED_PRMIUM),'###,###.00')"/>
          </td>          
        </tr>
        <tr class="midcolora">
          <td width="20%" align="left" >
            <xsl:text>Less Demerit Free Discount 5%</xsl:text>
        </td>
          <td width="20%">
            <xsl:text>&#160;</xsl:text>
          </td>
          <td width="20%">
            <xsl:text>&#160;</xsl:text>
          </td>
          <td width="5%">S$</td>
          <td align="right" width="35%">
            <!--<xsl:call-template name="CALC_DEMERIT_DISCOUNT" />-->
            <xsl:value-of select="format-number((QUICKQUOTE/POLICY/DEMERIT_DISC_AMT),'###,###.00')"/>
          </td>
        </tr>
        <tr class="midcolora">
          <td colspan="5">
            <xsl:text>&#160;</xsl:text>
          </td>
        </tr>
        <tr class="midcolora">
          <td width="20%" align="left" >
            <xsl:text>Premium Before GST for the Period</xsl:text>
          </td>
          <td width="20%">
            <xsl:text>&#160;</xsl:text>
          </td>
          <td width="20%">
            <xsl:text>&#160;</xsl:text>
          </td>
          <td width="5%">S$</td>
          <td align="right" width="35%">
            <xsl:call-template name="CALC_PREMIUM_AFTER_DEMERIT_DISC" />
          </td>
        </tr>
        <tr>
          <td class="midcolora" colspan="5">
            <xsl:text>&#160;</xsl:text>
          </td>
        </tr>
        <tr class="midcolora">
          <td width="20%">
            <xsl:text>GST 7.00 %</xsl:text>
        </td>
          <td width="20%">
            <xsl:text>&#160;</xsl:text>
          </td>
          <td width="20%">
            <xsl:text>&#160;</xsl:text>
          </td>
          <td width="5%">S$</td>
          <td align="right" width="35%">
            <!--<xsl:call-template name="CALC_GST" />-->
            <xsl:value-of select="format-number((QUICKQUOTE/POLICY/GST_AMOUNT),'###,###.00')"/>
            
          </td>
        </tr>
        <tr class="midcolora" >
          <td colspan="5">
            <xsl:text>&#160;</xsl:text>
          </td>
        </tr>
        <tr class="midcolora" >
          <td width="20%">
            <xsl:text>Premium Payable for the Period</xsl:text>
        </td>
          <td width="20%">
            <xsl:text>&#160;</xsl:text>
          </td>
          <td width="20%">
            <xsl:text>&#160;</xsl:text>
          </td>
          <td width="5%">S$</td>
          <td align="right" width="35%">
            <!--<xsl:call-template name="CALC_PREMIUM_AFTER_GST" />-->
            <xsl:value-of select="format-number((QUICKQUOTE/POLICY/FINAL_PREMIUM),'###,###.00')"/>
          </td>
        </tr>
        <tr class="midcolora">
          <td colspan="5">
            <xsl:text>&#160;</xsl:text>
          </td>
        </tr>
        <tr class="midcolora">
          <td width="20%">
            <xsl:text>Excess:</xsl:text>
          </td>
          <td width="20%">
            <xsl:text>Named driver</xsl:text>
          </td>
          <td width="20%">
            <!--<xsl:text>&#160;</xsl:text>-->
            <xsl:text>S$&#160;</xsl:text>
            <xsl:value-of select="format-number((QUICKQUOTE/POLICY/NAMED_DRIVER_AMT),'###,###.00')"/>
          </td>
          <td width="5%">
            <xsl:text>&#160;</xsl:text>
          </td>
          <td width="35%">
            <xsl:text>&#160;</xsl:text>
          </td>
        </tr>
        <tr class="midcolora">
          <td colspan="5">
            <xsl:text>&#160;</xsl:text>
          </td>
        </tr>
        <tr class="midcolora">
          <td width="20%"></td>
          <td width="20%">
            <xsl:text>Unnamed driver</xsl:text>
          </td>
          <td width="20%">
            <!--<xsl:text>&#160;</xsl:text>-->
            <xsl:text>S$&#160;</xsl:text>
            <xsl:value-of select="format-number((QUICKQUOTE/POLICY/UNNAMED_DRIVER_AMT),'###,###.00')"/>
          </td>
          <td width="5%">
            <xsl:text>&#160;</xsl:text>
          </td>
          <td width="35%">
            <xsl:text>&#160;</xsl:text>
          </td>
        </tr>
        <tr class="midcolora">
          <td colspan="5">
            <xsl:text>&#160;</xsl:text>
          </td>
        </tr>
        <tr class="midcolora">
          <td width="35%">
            <xsl:text>Total Premium Payable for the Period (S$)</xsl:text>
          </td>
          <td width="35%">
            <xsl:text>&#160;</xsl:text>
          </td>
          <td width="35%">
            <xsl:text>&#160;</xsl:text>
          </td>
          <td width="5%">S$</td>
          <td align="right" width="35%">
            <!--<xsl:call-template name="CALC_FINAL_PREMIUM" />-->
            <xsl:value-of select="format-number((QUICKQUOTE/POLICY/FINAL_PREMIUM),'###,###.00')"/>

          </td>
        </tr>
        <tr class="midcolora">
          <td colspan="5">
            <xsl:text>&#160;</xsl:text>
          </td>
        </tr>
        <tr class="headereffectCenter">
          <!--GROUP HEADING-->
          <td align="left" colspan="5">
            <xsl:text>Extra benefits included for the above Insurance:</xsl:text>            
          </td>      
        </tr>
        <tr class="midcolora">
          <td align="left" colspan="5">
            <xsl:text>A.Additional benefits of Cover Type Comprehensive:-</xsl:text>
          </td>
        </tr>
        <tr class="midcolora">
          <td align="left" colspan="5">
            <xsl:text>&#160; 1. Flood @/or other convulsion of nature and Strike, Riot and Commotion.</xsl:text>
          </td>
        </tr>
        <tr class="midcolora">
          <td align="left" colspan="5">
            <xsl:text>&#160; 2. Legal liability of Passengers for Acts of negligence.</xsl:text>
          </td>
        </tr>
        <tr class="midcolora">
          <td align="left" colspan="5">
            <xsl:text>&#160; 3. Passenger Liability.</xsl:text>
          </td>
        </tr>
        <tr class="midcolora">
          <td align="left" colspan="5">
            <xsl:text>&#160; 4. Free Windscreen Cover,additional premium will be charged for reinstallment of Windscreen.</xsl:text>
          </td>
        </tr>
        <tr class="midcolora">
          <td align="left" colspan="5">
            <xsl:text>&#160; 5. Settlement based on market value at time of loss.</xsl:text>
          </td>
        </tr>
        <tr class="headereffectCenter">
          <!--GROUP HEADING-->
          <td align="left" colspan="5">
            <xsl:text>Optioanal Benefit:</xsl:text>
          </td>
        </tr>
        <tr class="midcolora">
          <td align="left" colspan="5">
            <xsl:text>Solar Film Windscreen can be covered seperately. please refer to Underwriter.</xsl:text>
          </td>
        </tr>
        <tr class="midcolora">
          <td></td>
        </tr>
        <tr class="midcolora" align="center">
          
          <!--<td class="midcolora" valign="bottom" colspan="2" width="50%">
            <input type="button">
              <xsl:attribute name="id">btnBack</xsl:attribute>
              <xsl:attribute name="value">
                <xsl:text>Back</xsl:text>
              </xsl:attribute>
              --><!--Print--><!--
              <xsl:attribute name="onClick">printReport();</xsl:attribute>
              <xsl:attribute name="vAlign">bottom</xsl:attribute>
              <xsl:attribute name="class">clsButton</xsl:attribute>
            </input>
          </td>-->
          <td class="midcolora" valign="bottom" colspan="5">
            <input type="button">
              <xsl:attribute name="id">btnPrint</xsl:attribute>
              <xsl:attribute name="value">Print</xsl:attribute>
              <xsl:attribute name="onClick">printReport();</xsl:attribute>
              <xsl:attribute name="vAlign">bottom</xsl:attribute>
              <xsl:attribute name="class">clsButton</xsl:attribute>
            </input>
          </td>
        </tr>
      </table>
    </body>
  </html>
  
  </xsl:template>

  <xsl:template name="CALC_DEMERIT_DISCOUNT">
    <xsl:variable name="VAR_BASE_AMT">
      <xsl:value-of select="QUICKQUOTE/POLICY/RECEIVED_PRMIUM"/>
    </xsl:variable>
    <xsl:variable name="VAR_IS_DEMERIT">
      <xsl:value-of select="QUICKQUOTE/POLICY/DEMERIT_DISCOUNT"/>
    </xsl:variable>
    <xsl:choose>
      <xsl:when test="$VAR_IS_DEMERIT = 1">
        <xsl:value-of select="format-number(($VAR_BASE_AMT * 0.05),'###,###.00')"/>
      </xsl:when>
      <xsl:otherwise>
        <!--<xsl:text>N/A</xsl:text>-->
        <xsl:value-of select="$VAR_IS_DEMERIT"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="CALC_PREMIUM_AFTER_DEMERIT_DISC">
    <xsl:variable name="VAR_BASE_AMT">
      <xsl:value-of select="QUICKQUOTE/POLICY/RECEIVED_PRMIUM"/>
    </xsl:variable>
    <xsl:variable name="VAR_DEMERIT_AMT">
      <xsl:value-of select="QUICKQUOTE/POLICY/DEMERIT_DISC_AMT"/>
    </xsl:variable>

    <xsl:value-of select="format-number(($VAR_BASE_AMT - $VAR_DEMERIT_AMT),'###,###.00')"/>
   
   
  </xsl:template>

  <xsl:template name="CALC_GST">
    <xsl:variable name="VAR_BASE_AMT">
      <xsl:value-of select="QUICKQUOTE/POLICY/RECEIVED_PRMIUM"/>
    </xsl:variable>
    <xsl:variable name="VAR_IS_DEMERIT">
      <xsl:value-of select="QUICKQUOTE/POLICY/DEMERIT_DISCOUNT"/>
    </xsl:variable>
    
    <xsl:variable name="VAR_DISC_DEMERIT">
      <xsl:choose>
        <xsl:when test="$VAR_IS_DEMERIT = 1">
          <xsl:value-of select="$VAR_BASE_AMT * 0.05"/>
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="0"/>
        </xsl:otherwise>
      </xsl:choose>      
    </xsl:variable>
    
    <xsl:variable name="VAR_DISC_PREMIUM">
      <xsl:value-of select="$VAR_BASE_AMT - $VAR_DISC_DEMERIT"/>
    </xsl:variable>

    <xsl:value-of select="format-number(($VAR_DISC_PREMIUM * 0.07),'###,###.00')"/>
  </xsl:template>

  <xsl:template name="CALC_PREMIUM_AFTER_GST">
    <xsl:variable name="VAR_BASE_AMT">
      <xsl:value-of select="QUICKQUOTE/POLICY/RECEIVED_PRMIUM"/>
    </xsl:variable>
    <xsl:variable name="VAR_IS_DEMERIT">
      <xsl:value-of select="QUICKQUOTE/POLICY/DEMERIT_DISCOUNT"/>
    </xsl:variable>

    <xsl:variable name="VAR_DISC_DEMERIT">
      <xsl:choose>
        <xsl:when test="$VAR_IS_DEMERIT = 1">
          <xsl:value-of select="$VAR_BASE_AMT * 0.05"/>
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="0"/>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:variable>

    <xsl:variable name="VAR_DISC_PREMIUM">
      <xsl:value-of select="$VAR_BASE_AMT - $VAR_DISC_DEMERIT"/>
    </xsl:variable>
    <xsl:variable name="VAR_GST_AMT">
      <xsl:value-of select="$VAR_DISC_PREMIUM * 0.07"/>
    </xsl:variable>

    <xsl:value-of select="format-number(($VAR_DISC_PREMIUM + $VAR_GST_AMT),'###,###.00')"/>
  </xsl:template>

  <xsl:template name="CALC_FINAL_PREMIUM">
    <xsl:variable name="VAR_BASE_AMT">
      <xsl:value-of select="QUICKQUOTE/POLICY/RECEIVED_PRMIUM"/>
    </xsl:variable>
    <xsl:variable name="VAR_IS_DEMERIT">
      <xsl:value-of select="QUICKQUOTE/POLICY/DEMERIT_DISCOUNT"/>
    </xsl:variable>

    <xsl:variable name="VAR_DISC_DEMERIT">
      <xsl:choose>
        <xsl:when test="$VAR_IS_DEMERIT = 1">
          <xsl:value-of select="$VAR_BASE_AMT * 0.05"/>
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="0"/>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:variable>

    <xsl:variable name="VAR_DISC_PREMIUM">
      <xsl:value-of select="$VAR_BASE_AMT - $VAR_DISC_DEMERIT"/>
    </xsl:variable>
    <xsl:variable name="VAR_GST_AMT">
      <xsl:value-of select="$VAR_DISC_PREMIUM * 0.07"/>
    </xsl:variable>

    <xsl:value-of select="format-number(($VAR_DISC_PREMIUM + $VAR_GST_AMT),'###,###.00')"/>
  </xsl:template>
  
</xsl:stylesheet>
