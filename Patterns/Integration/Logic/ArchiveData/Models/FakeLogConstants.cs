using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveData.Models
{
  public static class FakeLogConstants
  {

    public const string FAKE_LOG_XML_REQ = @"
<?xml version=""1.0"" encoding=""UTF-8""?>
<Message FakeDatatypesVersion = ""BT""

				 FakeTransportVersion=""BT""
         FakeTransactionDomain=""XXXX""
         FakeTransactionVersion= ""BT""
         FakeStructuresVersion=""BT""
         FakeECLVersion=""BT""
				 xsi:noNamespaceSchemaLocation=""Faketransport.xsd""
         FakeVersion=""BT""
				 xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
	<FakeHeader>
		<To>
			<Primary>
				<Identification>000003</Identification>
				<Qualifier>IIN</Qualifier>
			</Primary>
			<Secondary>
				<Identification>X4Q2</Identification>
				<Qualifier>PCN</Qualifier>
			</Secondary>
		</To>
		<From>
			<Primary>
				<Identification>1234567111</Identification>
				<Qualifier>D</Qualifier>
			</Primary>
		</From>
		<MessageID>8045D45130CD41139FCC36781DA6FEFA</MessageID>
		<RelatesToMessageID></RelatesToMessageID>
		<SentTime>2020-01-07T09:30:00Z</SentTime>
		<SoftwareSenderCertificationID>2233445566</SoftwareSenderCertificationID>
	</FakeHeader>
	<FakeBody>
		<FakeRequest>
			<Patient>
				<Name>
					<LastName>DOE</LastName>
					<FirstName>JOHN</FirstName>
				</Name>
				<Gender>M</Gender>
				<DateOfBirth>1918-11-11</DateOfBirth>
			</Patient>
			<BenefitsCoordination>
				<PBMMemberID>JREREM03TEST2018</PBMMemberID>
				<CardholderID>JREREM03TEST2018</CardholderID>
				<GroupID>JREREM03</GroupID>
				<PersonCode>01</PersonCode>
			</BenefitsCoordination>
			<Product>
				<DrugCoded>
					<NDC>00310075590</NDC>
				</DrugCoded>
				<Quantity>
					<Value>30</Value>
					<CodeListQualifier>38</CodeListQualifier>
					<QuantityUnitOfMeasure>
						<Code>C48542</Code>
					</QuantityUnitOfMeasure>
				</Quantity>
				<DaysSupply>30</DaysSupply>
				<DispensedAsWrittenProductSelectionCode>0</DispensedAsWrittenProductSelectionCode>
			</Product>
			<Prescriber>
				<Identification>
					<NPI>1112223333</NPI>
				</Identification>
				<LastName>BODD</LastName>
			</Prescriber>
			<Pharmacy>
				<Identification>
					<NPI>1234567897</NPI>
				</Identification>
				<BusinessName>ELMO STREET PHARMACY</BusinessName>
				<PrimaryTelephoneNumber>3012225555</PrimaryTelephoneNumber>
			</Pharmacy>
		</FakeRequest>
	</FakeBody>
</Message>
";

		public const string FAKE_FLOW_1 = 
			@"{""cadr"":""15.1.33.44:53322"",""cagn"":""insomnia/2020.4.1"",""cprt"":""POST  http://APPSRV-0001:80/fake/check"",""mid"":""3045D45130CD41139FCC36781DA6FEFA"",""rel"":""<na>""}";

		public const string FAKE_FLOW_2 =
			@"{""cadr"":""15.1.33.55:53323"",""cagn"":""insomnia/2020.4.1"",""cprt"":""POST  http://APPSRV-0002:80/fake/check"",""mid"":""3045D45130CD41139FCC36781DA6FEFA"",""rel"":""<na>""}";

		public const string FAKE_FLOW_3 =
			@"{""cadr"":""15.1.33.66:53323"",""cagn"":""insomnia/2020.4.1"",""cprt"":""POST  http://WEBSRV-0002:80/fake/check"",""mid"":""3045D45130CD41139FCC36781DA6FEFA"",""rel"":""<na>""}";

		public const string FAKE_FLOW_4 =
			@"{""cadr"":""15.1.33.77:53323"",""cagn"":""insomnia/2020.4.1"",""cprt"":""POST  http://WEBSRV-0001:80/fake/check"",""mid"":""3045D45130CD41139FCC36781DA6FEFA"",""rel"":""<na>""}";

		public const string FAKE_JSON_REQ =
			@"{""channel"":""--any--"",""request"":{""Header"":{""To"":{""Primary"":{""Identification"":""000003"",""Qualifier"":""IIN""},""Secondary"":{""Identification"":""ABCD"",""Qualifier"":""PCN""}},""From"":{""Primary"":{""Identification"":""1234567111"",""Qualifier"":""D""}},""MessageID"":""8045D45130CD41139FCC36781DA6FEFA"",""SentTime"":""2020-01-07T09:30:00Z"",""SoftwareSenderCertificationID"":""2233445566""},""Body"":{""Item"":{""__entity"":""request"",""Patient"":{""DateOfBirth"":""1944-06-06T00:00:00Z"",""Gender"":""M"",""Name"":{""LastName"":""JONES"",""FirstName"":""GARY""}},""BenefitsCoordination"":{""PBMMemberID"":""NWIRON03TEST2018"",""CardholderID"":""JREREM03TEST2018"",""GroupID"":""JREREM03"",""PersonCode"":""01""},""ProductRequested"":{""DrugCoded"":{""NDC"":""00310075590""},""DaysSupply"":30,""DAWCode"":""0"",""Quantity"":{""Value"":30,""CodeListQualifier"":""38"",""QuantityUnitOfMeasure"":{""Code"":""C48542""}}},""Prescriber"":{""Identification"":{""NPI"":""1112223338""},""LastName"":""HOSENFEFFER""},""Pharmacy"":{""Identification"":{""NPI"":""1234567897""},""BusinessName"":""ELMO STREET PHARMACY"",""PrimaryTelephoneNumber"":""3012225555""}}},""DatatypesVersion"":""BT"",""TransportVersion"":""BT"",""TransactionDomain"":""XXXX"",""TransactionVersion"":""BT"",""StructuresVersion"":""BT"",""ECLVersion"":""BT"",""RTPBVersion"":""BT""}}";

		public const string FAKE_JSON_RESP =
			@"{""response"":{""Header"":{""To"":{""Primary"":{""Identification"":""--any--"",""Qualifier"":""D""}},""From"":{""Primary"":{""Identification"":""JREREMY"",""Qualifier"":""PY""},""Secondary"":{""Identification"":""009999"",""Qualifier"":""IIN""},""Tertiary"":{""Identification"":""ABCD"",""Qualifier"":""PCN""}},""MessageID"":""23aa768a1b7c433891cd9b69b43ca752"",""RelatesToMessageID"":""8045D45130CD41139FCC36781DA6FEFA"",""SentTime"":""2020-11-06T14:24:08.773Z"",""SoftwareSenderCertificationID"":""Jreremy""},""Body"":{""Item"":{""__entity"":""response"",""Response"":{""Item"":{""__entity"":""processed"",""Note"":""Processed"",""HelpDeskSupportType"":""3"",""HelpDeskBusinessUnits"":[{""HelpdeskBusinessUnitType"":""5"",""HelpDeskCommunicationNumbers"":{""Telephone"":{""Number"":""8003614542""}}}]}},""ResponseProduct"":{""DrugCoded"":{""NDC"":""00310075590""},""DrugDescription"":""N\/A"",""ePAEnabled"":false,""Quantity"":{""Value"":30,""CodeListQualifier"":""38"",""QuantityUnitOfMeasure"":{""Code"":""C48542""}},""PharmacyResponses"":[{""Pharmacy"":{""PharmacyType"":""A"",""Identification"":{""NPI"":""1234567897""},""BusinessName"":""ELMO STREET PHARMACY"",""PrimaryTelephoneNumber"":""3012225555""},""PricingAndCoverage"":{""PricingAndCoverageIndicator"":""1"",""CoverageStatusCode"":""1"",""Quantity"":{""Value"":30,""CodeListQualifier"":""38"",""QuantityUnitOfMeasure"":{""Code"":""C48542""}},""DaysSupply"":30,""EstimatedPatientFinancialResponsibility"":4,""PatientPayComponents"":[{""PatientPayComponentQualifier"":""05"",""PatientPayComponentAmount"":4}],""CoverageRestrictionCode"":[{""CoverageRestrictionCode"":""92""}]}}]},""ResponseProductAlternatives"":[{""DrugCoded"":{""NDC"":""70515062930""},""DrugDescription"":""ALTOPREV     TAB 40MG ER"",""ePAEnabled"":false,""Quantity"":{""Value"":30,""CodeListQualifier"":""38"",""QuantityUnitOfMeasure"":{""Code"":""C48542""}},""PharmacyResponses"":[{""Pharmacy"":{""PharmacyType"":""A"",""Identification"":{""NPI"":""1234567897""},""BusinessName"":""ELMO STREET PHARMACY"",""PrimaryTelephoneNumber"":""3012225555""},""PricingAndCoverage"":{""PricingAndCoverageIndicator"":""1"",""CoverageStatusCode"":""1"",""Quantity"":{""Value"":30,""CodeListQualifier"":""38"",""QuantityUnitOfMeasure"":{""Code"":""C48542""}},""DaysSupply"":30,""EstimatedPatientFinancialResponsibility"":4,""PatientPayComponents"":[{""PatientPayComponentQualifier"":""05"",""PatientPayComponentAmount"":4}],""CoverageRestrictionCode"":[{""CoverageRestrictionCode"":""92""}]}}]},{""DrugCoded"":{""NDC"":""66869020490""},""DrugDescription"":""LIVALO       TAB 2MG"",""ePAEnabled"":false,""Quantity"":{""Value"":30,""CodeListQualifier"":""38"",""QuantityUnitOfMeasure"":{""Code"":""C48542""}},""PharmacyResponses"":[{""Pharmacy"":{""PharmacyType"":""A"",""Identification"":{""NPI"":""1234567897""},""BusinessName"":""ELMO STREET PHARMACY"",""PrimaryTelephoneNumber"":""3012225555""},""PricingAndCoverage"":{""PricingAndCoverageIndicator"":""1"",""CoverageStatusCode"":""1"",""Quantity"":{""Value"":30,""CodeListQualifier"":""38"",""QuantityUnitOfMeasure"":{""Code"":""C48542""}},""DaysSupply"":30,""EstimatedPatientFinancialResponsibility"":4,""PatientPayComponents"":[{""PatientPayComponentQualifier"":""05"",""PatientPayComponentAmount"":4}],""CoverageRestrictionCode"":[{""CoverageRestrictionCode"":""92""}]}}]},{""DrugCoded"":{""NDC"":""25208020109""},""DrugDescription"":""ZYPITAMAG    TAB 2MG"",""ePAEnabled"":false,""Quantity"":{""Value"":30,""CodeListQualifier"":""38"",""QuantityUnitOfMeasure"":{""Code"":""C48542""}},""PharmacyResponses"":[{""Pharmacy"":{""PharmacyType"":""A"",""Identification"":{""NPI"":""1234567897""},""BusinessName"":""ELMO STREET PHARMACY"",""PrimaryTelephoneNumber"":""3012225555""},""PricingAndCoverage"":{""PricingAndCoverageIndicator"":""1"",""CoverageStatusCode"":""1"",""Quantity"":{""Value"":30,""CodeListQualifier"":""38"",""QuantityUnitOfMeasure"":{""Code"":""C48542""}},""DaysSupply"":30,""EstimatedPatientFinancialResponsibility"":4,""PatientPayComponents"":[{""PatientPayComponentQualifier"":""05"",""PatientPayComponentAmount"":4}],""CoverageRestrictionCode"":[{""CoverageRestrictionCode"":""92""}]}}]},{""DrugCoded"":{""NDC"":""13668017930""},""DrugDescription"":""ROSUVASTATIN TAB 5MG"",""ePAEnabled"":false,""Quantity"":{""Value"":30,""CodeListQualifier"":""38"",""QuantityUnitOfMeasure"":{""Code"":""C48542""}},""PharmacyResponses"":[{""Pharmacy"":{""PharmacyType"":""A"",""Identification"":{""NPI"":""1234567897""},""BusinessName"":""ELMO STREET PHARMACY"",""PrimaryTelephoneNumber"":""3012225555""},""PricingAndCoverage"":{""PricingAndCoverageIndicator"":""1"",""CoverageStatusCode"":""1"",""Quantity"":{""Value"":30,""CodeListQualifier"":""38"",""QuantityUnitOfMeasure"":{""Code"":""C48542""}},""DaysSupply"":30,""EstimatedPatientFinancialResponsibility"":4,""PatientPayComponents"":[{""PatientPayComponentQualifier"":""05"",""PatientPayComponentAmount"":4}],""CoverageRestrictionCode"":[{""CoverageRestrictionCode"":""92""}]}}]},{""DrugCoded"":{""NDC"":""00093505698""},""DrugDescription"":""ATORVASTATIN TAB 10MG"",""ePAEnabled"":false,""Quantity"":{""Value"":30,""CodeListQualifier"":""38"",""QuantityUnitOfMeasure"":{""Code"":""C48542""}},""PharmacyResponses"":[{""Pharmacy"":{""PharmacyType"":""A"",""Identification"":{""NPI"":""1234567897""},""BusinessName"":""ELMO STREET PHARMACY"",""PrimaryTelephoneNumber"":""3012225555""},""PricingAndCoverage"":{""PricingAndCoverageIndicator"":""1"",""CoverageStatusCode"":""1"",""Quantity"":{""Value"":30,""CodeListQualifier"":""38"",""QuantityUnitOfMeasure"":{""Code"":""C48542""}},""DaysSupply"":30,""EstimatedPatientFinancialResponsibility"":4,""PatientPayComponents"":[{""PatientPayComponentQualifier"":""05"",""PatientPayComponentAmount"":4}],""CoverageRestrictionCode"":[{""CoverageRestrictionCode"":""92""}]}}]}]}},""DatatypesVersion"":""BT"",""TransportVersion"":""BT"",""TransactionDomain"":""XXXX"",""TransactionVersion"":""BT"",""StructuresVersion"":""BT"",""ECLVersion"":""BT"",""RTPBVersion"":""BT""}}";

		public const string FAKE_LOG_XML_RESP =
			@"<?xml version=""1.0"" encoding=""UTF-8""?><RTPBMessage xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" RTPBDatatypesVersion=""BT"" RTPBECLVersion=""BT"" RTPBStructuresVersion=""BT"" RTPBTransactionDomain=""XXXX"" RTPBTransactionVersion=""BT"" RTPBTransportVersion=""BT"" RTPBVersion=""BT""><RTPBHeader><To><Primary><Identification>--any--</Identification><Qualifier>D</Qualifier></Primary></To><From><Primary><Identification>JREREMY</Identification><Qualifier>PY</Qualifier></Primary><Secondary><Identification>009999</Identification><Qualifier>IIN</Qualifier></Secondary><Tertiary><Identification>ABCD</Identification><Qualifier>PCN</Qualifier></Tertiary></From><MessageID>23aa768a1b7c433891cd9b69b43ca752</MessageID><RelatesToMessageID>8045D45130CD41139FCC36781DA6FEFA</RelatesToMessageID><SentTime>2020-11-06T14:24:08.7737650Z</SentTime><SoftwareSenderCertificationID>Jreremy</SoftwareSenderCertificationID></RTPBHeader><RTPBBody><RTPBResponse><Response><Processed><Note>Processed</Note><HelpDeskSupportType>3</HelpDeskSupportType><HelpDeskBusinessUnit><HelpdeskBusinessUnitType>5</HelpdeskBusinessUnitType><HelpDeskCommunicationNumbers><Telephone><Number>8003614542</Number></Telephone></HelpDeskCommunicationNumbers></HelpDeskBusinessUnit></Processed></Response><ResponseAlternativeProduct><DrugCoded><NDC>70515062930</NDC></DrugCoded><DrugDescription>ALTOPREV     TAB 40MG ER</DrugDescription><Quantity><Value>30</Value><QuantityUnitOfMeasure><Code>C48542</Code></QuantityUnitOfMeasure><CodeListQualifier>38</CodeListQualifier></Quantity><ePAEnabled>false</ePAEnabled><PricingAndCoverages><Pharmacy><PharmacyType>A</PharmacyType><Identification><NPI>1234567897</NPI></Identification><BusinessName>ELM STREET PHARMACY</BusinessName><PrimaryTelephoneNumber>3012225555</PrimaryTelephoneNumber></Pharmacy><PricingAndCoverage><PricingAndCoverageIndicator>1</PricingAndCoverageIndicator><CoverageStatusCode>1</CoverageStatusCode><Quantity><Value>30</Value><QuantityUnitOfMeasure><Code>C48542</Code></QuantityUnitOfMeasure><CodeListQualifier>38</CodeListQualifier></Quantity><DaysSupply>30</DaysSupply><EstimatedPatientFinancialResponsibility>4</EstimatedPatientFinancialResponsibility><PatientPayComponent><PatientPayComponentQualifier>05</PatientPayComponentQualifier><PatientPayComponentAmount>4</PatientPayComponentAmount></PatientPayComponent><CoverageRestriction><CoverageRestrictionCode>92</CoverageRestrictionCode></CoverageRestriction></PricingAndCoverage></PricingAndCoverages></ResponseAlternativeProduct><ResponseAlternativeProduct><DrugCoded><NDC>66869020490</NDC></DrugCoded><DrugDescription>LIVALO       TAB 2MG</DrugDescription><Quantity><Value>30</Value><QuantityUnitOfMeasure><Code>C48542</Code></QuantityUnitOfMeasure><CodeListQualifier>38</CodeListQualifier></Quantity><ePAEnabled>false</ePAEnabled><PricingAndCoverages><Pharmacy><PharmacyType>A</PharmacyType><Identification><NPI>1234567897</NPI></Identification><BusinessName>ELM STREET PHARMACY</BusinessName><PrimaryTelephoneNumber>3012225555</PrimaryTelephoneNumber></Pharmacy><PricingAndCoverage><PricingAndCoverageIndicator>1</PricingAndCoverageIndicator><CoverageStatusCode>1</CoverageStatusCode><Quantity><Value>30</Value><QuantityUnitOfMeasure><Code>C48542</Code></QuantityUnitOfMeasure><CodeListQualifier>38</CodeListQualifier></Quantity><DaysSupply>30</DaysSupply><EstimatedPatientFinancialResponsibility>4</EstimatedPatientFinancialResponsibility><PatientPayComponent><PatientPayComponentQualifier>05</PatientPayComponentQualifier><PatientPayComponentAmount>4</PatientPayComponentAmount></PatientPayComponent><CoverageRestriction><CoverageRestrictionCode>92</CoverageRestrictionCode></CoverageRestriction></PricingAndCoverage></PricingAndCoverages></ResponseAlternativeProduct><ResponseAlternativeProduct><DrugCoded><NDC>25208020109</NDC></DrugCoded><DrugDescription>ZYPITAMAG    TAB 2MG</DrugDescription><Quantity><Value>30</Value><QuantityUnitOfMeasure><Code>C48542</Code></QuantityUnitOfMeasure><CodeListQualifier>38</CodeListQualifier></Quantity><ePAEnabled>false</ePAEnabled><PricingAndCoverages><Pharmacy><PharmacyType>A</PharmacyType><Identification><NPI>1234567897</NPI></Identification><BusinessName>ELM STREET PHARMACY</BusinessName><PrimaryTelephoneNumber>3012225555</PrimaryTelephoneNumber></Pharmacy><PricingAndCoverage><PricingAndCoverageIndicator>1</PricingAndCoverageIndicator><CoverageStatusCode>1</CoverageStatusCode><Quantity><Value>30</Value><QuantityUnitOfMeasure><Code>C48542</Code></QuantityUnitOfMeasure><CodeListQualifier>38</CodeListQualifier></Quantity><DaysSupply>30</DaysSupply><EstimatedPatientFinancialResponsibility>4</EstimatedPatientFinancialResponsibility><PatientPayComponent><PatientPayComponentQualifier>05</PatientPayComponentQualifier><PatientPayComponentAmount>4</PatientPayComponentAmount></PatientPayComponent><CoverageRestriction><CoverageRestrictionCode>92</CoverageRestrictionCode></CoverageRestriction></PricingAndCoverage></PricingAndCoverages></ResponseAlternativeProduct><ResponseAlternativeProduct><DrugCoded><NDC>13668017930</NDC></DrugCoded><DrugDescription>ROSUVASTATIN TAB 5MG</DrugDescription><Quantity><Value>30</Value><QuantityUnitOfMeasure><Code>C48542</Code></QuantityUnitOfMeasure><CodeListQualifier>38</CodeListQualifier></Quantity><ePAEnabled>false</ePAEnabled><PricingAndCoverages><Pharmacy><PharmacyType>A</PharmacyType><Identification><NPI>1234567897</NPI></Identification><BusinessName>ELM STREET PHARMACY</BusinessName><PrimaryTelephoneNumber>3012225555</PrimaryTelephoneNumber></Pharmacy><PricingAndCoverage><PricingAndCoverageIndicator>1</PricingAndCoverageIndicator><CoverageStatusCode>1</CoverageStatusCode><Quantity><Value>30</Value><QuantityUnitOfMeasure><Code>C48542</Code></QuantityUnitOfMeasure><CodeListQualifier>38</CodeListQualifier></Quantity><DaysSupply>30</DaysSupply><EstimatedPatientFinancialResponsibility>4</EstimatedPatientFinancialResponsibility><PatientPayComponent><PatientPayComponentQualifier>05</PatientPayComponentQualifier><PatientPayComponentAmount>4</PatientPayComponentAmount></PatientPayComponent><CoverageRestriction><CoverageRestrictionCode>92</CoverageRestrictionCode></CoverageRestriction></PricingAndCoverage></PricingAndCoverages></ResponseAlternativeProduct><ResponseAlternativeProduct><DrugCoded><NDC>00093505698</NDC></DrugCoded><DrugDescription>ATORVASTATIN TAB 10MG</DrugDescription><Quantity><Value>30</Value><QuantityUnitOfMeasure><Code>C48542</Code></QuantityUnitOfMeasure><CodeListQualifier>38</CodeListQualifier></Quantity><ePAEnabled>false</ePAEnabled><PricingAndCoverages><Pharmacy><PharmacyType>A</PharmacyType><Identification><NPI>1234567897</NPI></Identification><BusinessName>ELM STREET PHARMACY</BusinessName><PrimaryTelephoneNumber>3012225555</PrimaryTelephoneNumber></Pharmacy><PricingAndCoverage><PricingAndCoverageIndicator>1</PricingAndCoverageIndicator><CoverageStatusCode>1</CoverageStatusCode><Quantity><Value>30</Value><QuantityUnitOfMeasure><Code>C48542</Code></QuantityUnitOfMeasure><CodeListQualifier>38</CodeListQualifier></Quantity><DaysSupply>30</DaysSupply><EstimatedPatientFinancialResponsibility>4</EstimatedPatientFinancialResponsibility><PatientPayComponent><PatientPayComponentQualifier>05</PatientPayComponentQualifier><PatientPayComponentAmount>4</PatientPayComponentAmount></PatientPayComponent><CoverageRestriction><CoverageRestrictionCode>92</CoverageRestrictionCode></CoverageRestriction></PricingAndCoverage></PricingAndCoverages></ResponseAlternativeProduct><ResponseProduct><DrugCoded><NDC>00310075590</NDC></DrugCoded><DrugDescription>N/A</DrugDescription><Quantity><Value>30</Value><QuantityUnitOfMeasure><Code>C48542</Code></QuantityUnitOfMeasure><CodeListQualifier>38</CodeListQualifier></Quantity><ePAEnabled>false</ePAEnabled><PricingAndCoverages><Pharmacy><PharmacyType>A</PharmacyType><Identification><NPI>1234567897</NPI></Identification><BusinessName>ELMO STREET PHARMACY</BusinessName><PrimaryTelephoneNumber>3012225555</PrimaryTelephoneNumber></Pharmacy><PricingAndCoverage><PricingAndCoverageIndicator>1</PricingAndCoverageIndicator><CoverageStatusCode>1</CoverageStatusCode><Quantity><Value>30</Value><QuantityUnitOfMeasure><Code>C48542</Code></QuantityUnitOfMeasure><CodeListQualifier>38</CodeListQualifier></Quantity><DaysSupply>30</DaysSupply><EstimatedPatientFinancialResponsibility>4</EstimatedPatientFinancialResponsibility><PatientPayComponent><PatientPayComponentQualifier>05</PatientPayComponentQualifier><PatientPayComponentAmount>4</PatientPayComponentAmount></PatientPayComponent><CoverageRestriction><CoverageRestrictionCode>92</CoverageRestrictionCode></CoverageRestriction></PricingAndCoverage></PricingAndCoverages></ResponseProduct></RTPBResponse></RTPBBody></RTPBMessage>";



	}
}
