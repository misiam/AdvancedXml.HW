<?xml version="1.0" encoding="utf-8"?>

<!--xmlns:lib="http://library.by/catalog"-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns="http://www.w3.org/2005/Atom"
    xmlns:lib="http://library.by/catalog"
    xmlns:script="urn:my-scripts"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl script lib"
>
    <xsl:output method="xml" indent="yes"/>
  <msxsl:script implements-prefix='script' language='CSharp'>
    <![CDATA[
    public string registrationDate() {
      return System.DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ssZ");
    }
    public string atomId() {
      return System.Guid.NewGuid().ToString();
    }
  ]]>
  </msxsl:script>

  <xsl:template match="/"  >
    <feed xmlns="http://www.w3.org/2005/Atom">
      <title>New Arrivals</title>
      <id><xsl:value-of select="script:atomId()"/></id>
      <updated><xsl:value-of select="script:registrationDate()"/></updated>
      <xsl:apply-templates />
    </feed>
  </xsl:template>
  
<xsl:template match="lib:catalog">
  <xsl:apply-templates select="lib:book"/>
</xsl:template>
  
<xsl:template match="lib:book">
  <entry>
        <title><xsl:value-of select="lib:title"/></title>
    <xsl:if test="lib:isbn"><link href="http://my.safaribooksonline.com/{lib:isbn}/" /></xsl:if>
    <id><xsl:value-of select="@id"/></id>
    <updated><xsl:value-of select="lib:registration_date"/></updated>
    <content type="text/xml">
      <author><xsl:value-of select="lib:author"/></author>
      <genre><xsl:value-of select="lib:genre"/></genre>
      <publisher><xsl:value-of select="lib:publisher"/></publisher>
      <publish_date><xsl:value-of select="lib:publish_date"/></publish_date>
      <description><xsl:value-of select="lib:description"/></description>
    </content>
  
  </entry>
  <!--<entry>
    <title><xsl:value-of select="title"/></title>
    <xsl:if test="isbn"><link href="http://my.safaribooksonline.com/{isbn}/" /></xsl:if>
    <id><xsl:copy-of select="@id"/></id>
    <xsl:copy-of select="registration_date"/>
    <content type="text/xml">
      <xsl:copy-of select="author"/>
      <xsl:copy-of select="genre"/>
      <xsl:copy-of select="publisher"/>
      <xsl:copy-of select="publish_date"/>
      <xsl:copy-of select="description"/>
    </content>
  </entry>-->
    

</xsl:template>
 
  <!--<xsl:template match="*" mode="copy-no-namespaces">
    <xsl:element name="{local-name()}">
        <xsl:copy-of select="@*"/>
        <xsl:apply-templates select="node()" mode="copy-no-namespaces"/>
    </xsl:element>
  
</xsl:template>-->

</xsl:stylesheet>
