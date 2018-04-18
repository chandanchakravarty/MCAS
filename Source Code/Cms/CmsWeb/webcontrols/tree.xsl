<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" indent="yes" encoding="utf-8"/>
  <xsl:template match="/Nodes">
    <NodeItems>
      <xsl:call-template name = "NodeList" />
    </NodeItems>
  </xsl:template>
  <xsl:template name="NodeList">
    <xsl:apply-templates select = "Node" />
  </xsl:template>
  <xsl:template match="Node">
    <NodeItem>
      <xsl:attribute name="Text">
        <xsl:value-of select="PageName"/>
      </xsl:attribute>
      <xsl:attribute name="NavigateUrl">
        <xsl:text></xsl:text>
        <xsl:value-of select="RedirectURL"/>
      </xsl:attribute>
      <xsl:attribute name="ValueF">      
        <xsl:value-of select="PageURL"/>
      </xsl:attribute>
      <xsl:if test="count(Node)>0">
        <xsl:call-template name="NodeList" />
      </xsl:if>
    </NodeItem>
  </xsl:template>
</xsl:stylesheet>