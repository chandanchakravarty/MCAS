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
          Plano de Contas
        </td>
      </tr>

      <tr>
        <td class="midcolorc" colspan="6">
          <b>A data de hoje : </b>
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
          Conta
        </td>
        <td class="HeadRow">
          Nome
        </td>
        <td class="HeadRow">
          Tipo de Conta
        </td>
        <td class="HeadRow">
          Total Nível
        </td>
        <td class="HeadRow">
          Tipo de Caixa
        </td>
        <td class="HeadRow">
          Active
        </td>
      </tr>
      <xsl:for-each select="//NewDataSet/Table">
        <tr>
          <xsl:choose>
            <xsl:when test="./ACC_LEVEL_TYPE = 'AS'">
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
            <xsl:when test="./ACC_LEVEL_TYPE = 'Head' or ACC_LEVEL_TYPE = 'Total'">
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
