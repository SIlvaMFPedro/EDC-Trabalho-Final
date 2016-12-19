--DROP TABLE [dbo].[PresidentSet]
--DROP XML SCHEMA COLLECTION [dbo].[schema_presidentes]

CREATE XML SCHEMA COLLECTION [dbo].[schema_presidentes] AS 
N'

<xs:schema attributeFormDefault="unqualified" elementFormDefault="qualified" xmlns:xs="http://www.w3.org/2001/XMLSchema">
  <xs:element name="root">
    <xs:complexType>
      <xs:sequence>
        <xs:element name="presidentes">
          <xs:complexType>
            <xs:sequence>
              <xs:element maxOccurs="unbounded" name="presidente">
                <xs:complexType>
                  <xs:sequence>
                    <xs:element name="nome" type="xs:string" />
                    <xs:element name="partido" type="xs:string" />
                    <xs:element name="atributos">
                      <xs:complexType>
                        <xs:sequence>
                          <xs:element name="numero" type="xs:unsignedByte" />
                          <xs:element name="idade" type="xs:string" />
                          <xs:element name="inicio" type="xs:string" />
                          <xs:element name="fim" type="xs:string" />
                          <xs:element minOccurs="0" name="vice1" type="xs:string" />
                          <xs:element minOccurs="0" name="vice2" type="xs:string" />
                          <xs:element minOccurs="0" name="vice3" type="xs:string" />
                          <xs:element minOccurs="0" name="vice" type="xs:string" />
                          <xs:element name="mandatos" type="xs:unsignedByte" />
                        </xs:sequence>
                      </xs:complexType>
                    </xs:element>
                  </xs:sequence>
                </xs:complexType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>

'
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PresidentSet'
CREATE TABLE [dbo].[PresidentSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
	[xml_presidentes] [xml](Document [dbo].[schema_presidentes])
);
GO

