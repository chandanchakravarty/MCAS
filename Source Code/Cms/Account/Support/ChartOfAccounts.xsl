<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="/">
    <!--html>
<head>
<LINK href="css1.css" type='text/css' rel='stylesheet'/>
</head>
<body-->
    <table width="100%" cellspacing="1" cellpadding="1">
      <tr>
        <td class="headereffectcenter" colspan="6">
          Chart of Accounts
        </td>
      </tr>

      <tr>
        <td class="midcolorc" colspan="6">
          <b>Today's Date : </b>
          <xsl:value-of select="//NewDataSet/Date"/>
        </td>
      </tr>
      <tr>
        <td class="midcolorc" colspan="6">
          <b>Time : </b>
          <xsl:value-of select="//NewDataSet/Time"/>
        </td>
      </tr>

      <tr>
        <td class="HeadRow">
          Account
        </td>
        <td class="HeadRow">
          Name
        </td>
        <td class="HeadRow">
          Account Type
        </td>
        <td class="HeadRow">
          Total Level
        </td>
        <td class="HeadRow">
          Cash Type
        </td>
        <td class="HeadRow">
          Active
        </td>
      </tr>
      <xsl:for-each select="//NewDataSet/Table">
        <tr>
          <xsl:choose>
            <xsl:when test="./ACC_LEVEL_TYPE = '14457'">
              <td class="midcolora">
                <xsl:value-of select="./ACC_DISP_NUMBER"/>
              </td>
              <td class="midcolora">
                <xsl:value-of select="./ACC_DESCRIPTION"/>
              </td>
              <td class="midcolora">
                <xsl:value-of select="./ACC_TYPE_DESC"/>
              </td>
              <td class="midcolora">
                <xsl:value-of select="./ACC_TOTALS_LEVEL"/>
              </td>
              <td class="midcolora">
                <xsl:value-of select="./CashType"/>
              </td>
              <td class="midcolora">
                <xsl:value-of select="./Active"/>
              </td>
            </xsl:when>
            <xsl:when test="./ACC_LEVEL_TYPE = '14458' or ACC_LEVEL_TYPE = '14460'">
              <td class="midcolora">
                <xsl:value-of select="./ACC_DISP_NUMBER"/>
              </td>
              <td class="midcolora" colspan="2">
                <b>
                  <xsl:value-of select="./ACC_DESCRIPTION"/>
                </b>
              </td>
              <td class="midcolora" colspan="2">
                <xsl:value-of select="./ACC_TOTALS_LEVEL"/>
              </td>
              <td class="midcolora">
                <xsl:value-of select="./Active"/>
              </td>
            </xsl:when>
            <xsl:otherwise>
              <td class="midcolora" colspan="5" height="17px">
                <xsl:value-of select="./ACC_DISP_NUMBER"/>
              </td>
              <td class="midcolora">
                <xsl:value-of select="./Active"/>
              </td>
            </xsl:otherwise>
          </xsl:choose>
        </tr>
      </xsl:for-each>
    </table>
    <!--/body>
</html-->
  </xsl:template>
</xsl:stylesheet>
