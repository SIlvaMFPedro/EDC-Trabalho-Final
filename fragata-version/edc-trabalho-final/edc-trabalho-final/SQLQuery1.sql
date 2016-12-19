CREATE XML SCHEMA COLLECTION [dbo].[schema_presidentes] AS 
N'
<xs:schema xmlns:xs="http://www.w3.org/2001/XMLSchema" elementFormDefault="qualified" attributeFormDefault="unqualified">
	<xs:element name="ROOT">
		<xs:complexType>
			<xs:sequence>
				<xs:element name="presidentes">
					<xs:complexType>
						<xs:sequence>
							<xs:element name="presidente" maxOccurs="unbounded">
								<xs:complexType>
									<xs:sequence>
										<xs:element name="nome" type="xs:string"/>
										<xs:element name="partido" type="xs:string"/>
										<xs:element name="atributos">
											<xs:complexType>
												<xs:sequence>
													<xs:element name="numero" type="xs:unsignedByte"/>
													<xs:element name="idade" type="xs:unsignedByte"/>
													<xs:element name="inicio">
														<xs:simpleType>
															<xs:restriction base="xs:string">
																<xs:pattern value="([0-9]{2})(/[0-9]{2})(/[0-9]{4})?"/>
															</xs:restriction>
														</xs:simpleType>
													</xs:element>
													<xs:element name="fim">
														<xs:simpleType>
															<xs:restriction base="xs:string">
																<xs:pattern value="([0-9]{2})(/[0-9]{2})(/[0-9]{4})?"/>
															</xs:restriction>
														</xs:simpleType>
													</xs:element>
													<xs:element name="vice" type="xs:string"/>
													<xs:element name="vice1" type="xs:string"/>
													<xs:element name="vice2" type="xs:string"/>
													<xs:element name="vice3" type="xs:string"/>
													<xs:element name="mandatos" type="xs:unsignedByte"/>
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