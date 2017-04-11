<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:lib="http://library.by/catalog"
>
  <xsl:output method="html" indent="yes"/>
  <xsl:param name="Report_Title"/>

  <xsl:key name="genres_of_books" match="/lib:catalog/lib:book" use="lib:genre"/>
  
  <xsl:template match="/">
    <html>
      <head>
        <title>
          <xsl:value-of select="$Report_Title" />
        </title>
      </head>
      <body>
        <h2><strong><xsl:value-of select="$Report_Title" /></strong></h2>
        <hr/>
        <div>
        <xsl:for-each select="/lib:catalog/lib:book[count(. | key('genres_of_books', lib:genre)[1]) = 1]">
          <xsl:sort select="lib:genre" order="ascending"/>
          <xsl:apply-templates select="lib:genre" />
            <table style="height: 70px;" width="700" border="1">
            <tbody>
              <tr>
                <td style="background-color:gray; color:white;">Autor</td>
                <td style="background-color:gray; color:white;">Name</td>
                <td style="background-color:gray; color:white;">Publish Date</td>
                <td style="background-color:gray; color:white;">Registration Date</td>
              </tr>
                  <xsl:apply-templates select="key('genres_of_books',  lib:genre)" />
              <tr>
                <td colspan="3"></td>
                <td><strong>Total: <xsl:value-of select="count(key('genres_of_books', lib:genre))"/></strong></td>
              </tr>
            </tbody>
            </table>
        </xsl:for-each>
        </div>

          <hr/>
          <strong>
            Total count of books: <xsl:value-of select="count(/lib:catalog/lib:book)"/>
          </strong>
      </body>
    </html>
  </xsl:template>
  
  <xsl:template match="lib:catalog">
    <xsl:apply-templates select="lib:book"/>
  </xsl:template>

  <xsl:template match="lib:book">
    <tr>
      <td><xsl:value-of select="lib:author"/></td>
      <td><xsl:value-of select="lib:title"/></td>
      <td><xsl:value-of select="lib:publish_date"/></td>
      <td><xsl:value-of select="lib:registration_date"/></td>
    </tr>
  </xsl:template>
  
   <xsl:template match="lib:genre">
    <h3>
      <xsl:value-of select="."/>
    </h3>
  </xsl:template> 

</xsl:stylesheet>
