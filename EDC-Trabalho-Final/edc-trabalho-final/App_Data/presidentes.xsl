<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">

    <presidentes>

      <xsl:for-each  select="root/presidentes/presidente">

        <presidente>
          
         <xsl:attribute name="Nome">
            <xsl:value-of select="nome"/>
          </xsl:attribute>
         <xsl:attribute name="Partido">
            <xsl:value-of select="partido"/>
          </xsl:attribute>

          <xsl:for-each  select="atributos">


          <xsl:attribute name="Idade">
            <xsl:value-of select="idade"/>
          </xsl:attribute>



          <xsl:attribute name="PresidenteN">
            <xsl:value-of select="numero"/>
          </xsl:attribute>



         </xsl:for-each>

        </presidente>

      </xsl:for-each>



    </presidentes>
  </xsl:template>


</xsl:stylesheet>