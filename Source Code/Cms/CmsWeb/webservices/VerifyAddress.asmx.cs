using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace VerifyAddr
{
	/// <summary>
	/// Webservice : service to verify address
	/// </summary>
	public class VerifyAddress : System.Web.Services.WebService
	{
		public int			hUnz;
		public char[]		szFirmName	= new char[51];
		public char[]		szPRUrb		= new char[51];
		public char[]		szDelLine	= new char[51];
		public char[]		szLastLine	= new char[51];
		public char[]		szAreaCode	= new char[4];
		public char[]		szErrorText	= new char[51];
		public char[]		szCity		= new char[30];
		public char[]		szState		= new char[3]; 
		public char[]		szZip		= new char[11];
		public int			ResultCode;

		// code added for GETSTREETPARTS
		public char[]		szNumber	= new char[51];
		public char[]		szStreet	= new char[51];
		public char[]		szUnit		= new char[51];
		// end code addition
		
		//Declares For External DLL Call To UNZDLL.DLL

		[DllImport("Unzdll32.dll")]
		public static extern int UNZ_INIT_EX();
		
		[DllImport("Unzdll32.dll")]
		public static extern int UNZ_TERM (int hUnz );
		
		[DllImport("Unzdll32.dll")]
		public static extern int  UNZ_CHECKADDRESS (int hUnz, string Line1, string line2, string line3, string Line4);

		[DllImport("Unzdll32.dll")]
		public static extern void UNZ_GETSTDADDRESS (int hUnz, byte[] bszFirmName, byte[] bszPRUrb, byte[] bszDelLine, byte[] bszLastLine);

		[DllImport("Unzdll32.dll")]
		public static extern void UNZ_GETERRORTEXT (int hUnz, byte[] bszErrorText);

		[DllImport("Unzdll32.dll")]
		public static extern int UNZ_GETMATCHCOUNT (int hUnz); 
		
		[DllImport("Unzdll32.dll")]
		public static extern void UNZ_GETMATCHADDR (int hUnz, int intItem, byte[] bszFirmName, byte[] bszPRUrb, byte[] bszDelLine, byte[] bszLastLine);

		[DllImport("Unzdll32.dll")]
		public static extern void  UNZ_GETCITY (int hUnz, byte[] szCity);

		[DllImport("Unzdll32.dll")]
		public static extern void  UNZ_GETSTATE (int hUnz, byte[] szState);

		[DllImport("Unzdll32.dll")]
		public static extern void  UNZ_GETZIP (int hUnz, byte[] szZip);

		[DllImport("Unzdll32.dll")]
		public static extern void  UNZ_GETAREACODE (int hUnz, byte[] szAreaCode);

		[DllImport("Unzdll32.dll")]
		public static extern void  UNZ_GETCITYSTZIP (int hUnz, byte[] szCity, byte[] szState, byte[] szZip);

		// code added for GETSTREETPARTS
		[DllImport("Unzdll32.dll")]
		public static extern void  UNZ_GETSTREETPARTS(int hUNZ,  byte[] szNumber,  byte[] szStreet,  byte[] szUnit);
		// end code addition

		//Group   Range   Meaning
		//no error    0   The address was verified with no changes
		//warnings    1 - 49  The address was corrected as indicated
		//critical    50 - 79 The address could not be verified or corrected
		//runtime 80 - 99 A non-fatal error occurred
		//fatal   >= 100  DLL initialization failed (returned by UNZ_ERROR() only)
		// The following code is returned by UNZ_CHECKADDRESS() if the address was verified with no corrections.

		public const   int XC_GOODADDR = 0;    //Address was verified with no corrections

		// The following codes may be returned by UNZ_CHECKADDRESS()
		// indicating that the address was corrected.
		// If multiple corrections are necessary to achieve an address match,
		// only the final correction is reported as a return code by
		// UNZ_CHECKADDRESS(). For a detailed indication of multiple address
		// corrections, you should use function UNZ_GETADDRESSFLAGS() to retrieve
		// a correction vector containing a single character for each correction
		// which was applied.

		//error   return  error
		//code    value   meaning

		public const   int  XC_ZIP		= 1;      // ZIP code or ZIP+4 added/corrected
		public const   int  XC_STATE	= 2;      // state name added/corrected
		public const   int  XC_CITY		= 3;      // C   city name added/corrected
		public const   int  XC_ADDR		= 4;      // A   street number and/or predirection corrected
		public const   int  XC_STREET	= 5;      // N   street name, suffix, and/or postdirection corrected
		public const   int  XC_POB		= 6;      // P   PO Box address corrected
		public const   int  XC_UNIT		= 7;      // #   Secondary address (apt/ste) corrected or ignored
		public const   int  XC_FIRM		= 8;      // F   Firm name not found, ignored
		public const   int  XC_URB		= 9;      // U   PR Urb name corrected
		public const   int  XC_CS		= 10;     // C, S    missing city-state corrected to match ZIP
		public const   int  XC_NONDEL	= 20;     // D   Address verified, but USPS doesn//t deliver (no +4)
		public const   int  XC_EXTRA	= 21;     // X   Extraneous characters (ignored)
		public const   int  XC_5DIG		= 30;     // Z   Verified only 5-digit ZIP code (UNZ_CHECKZIP())

		// The following codes may be returned by UNZ_CHECKADDRESS
		// indicating that the address could not be verified or corrected.

		public const   int  XC_BADADDR	= 50;    // unable to verify address
		public const   int  XC_BADCSZ	= 50;    // total city-state-zip mismatch
		public const   int  XC_NODATA	= 51;    // insufficient address data
		public const   int  XC_BADSTR	= 52;    // street name not found
		public const   int  XC_BADNBR	= 53;    // street number or box number out of range
		public const   int  XC_NOSTR	= 54;    // city has no streets (Unable to load street list)
		public const   int  XC_MULT		= 55;    // Multiple matching records
		public const   int  XC_BADURB	= 56;    // PR Urb not found in city street list
		public const   int  XC_FHERR	= 57;    // Firm/Highrise name conflict

		// The following codes may be returned by UNZ_CHECKADDRESS()
		// or UNZ_CHECKZIP(), They indicate that a runtime error
		// was detected by the DLL during the verification of an
		// address or ZIP code. These errors are not fatal, and
		// do not prevent further calls to the DLL. These codes may
		// indicate errors in passing parameters by the calling program,
		// hardware I/O errors, or database internal content errors.

		public const   int  RTE00 = 80;         // invalid or missing parameter block
		public const   int  RTE01 = 81;         // invalid handle parameter
		public const   int  RTE02 = 82;         // missing function parameter
		public const   int  RTE03 = 83;         // z8.dat file seek error
		public const   int  RTE04 = 84;         // z8.dat file read error
		public const   int  RTE05 = 85;         // z8.dat record type error
		public const   int  RTE06 = 86;         // z8.dat record length error
		public const   int  RTE07 = 87;         // z8.dat record format error

		// This group of codes is returned only by UNZ_CHECKZIP().

		public const   int  XC_GOODZIP	= 0;     // Valid ZIP+4, single address found
		public const   int  XC_ARNG		= 40;    // ZIP+4 applies to primary address range
		public const   int  XC_URNG		= 41;    // ZIP+4 applies to secondary address range
		public const   int  XC_BADZIP	= 60;    // ZIP code format error (xxxxx or xxxxx-xxxx)
		public const   int  XC_ZNF		= 61;    // ZIP code not found

		// ** Functions Are Not Being Used But Declarations Are Provided
		//Declare Function UNZ_GETCITYSTZIP Lib "UNZDLL.DLL" (ByVal hUnz as int, ByVal szCity As String, ByVal szState As String, ByVal szZip As String) As int
		//Declare Function UNZ_GETSTREETPARTS Lib "UNZDLL.DLL" (ByVal hUnz as int, ByVal szNumber As String, ByVal szStreet As String, ByVal szUnit As String) As int
		
		// bszDelLine contains address1 and address2.

		private static Regex m_toSentence = new Regex(@"(?<=(?:^|\d+|[^\w\s])\s*)(\w)([\w\s]*)", RegexOptions.Compiled | RegexOptions.Multiline);

		public VerifyAddress()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
		}

		[WebMethod]
		public string CheckAddress(string FirmOrHighRiseName, string UrbanizationName, string StreetAddress1n2, string CityStateZip )
		{
			int hUnz = 0;
			
			string Line1 = FirmOrHighRiseName;
			string Line2 = UrbanizationName;
			string Line3 = StreetAddress1n2;
			string Line4 = CityStateZip;

            try
            {
                hUnz = UNZ_INIT_EX();
            }
            catch { }
			System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();

			byte[] bszFirmName	= enc.GetBytes( szFirmName );
			byte[] bszPRUrb		= enc.GetBytes( szPRUrb );
			byte[] bszDelLine	= enc.GetBytes( szDelLine );
			byte[] bszLastLine	= enc.GetBytes( szLastLine);
			byte[] bszAreaCode	= enc.GetBytes( szAreaCode );
			byte[] bszCity		= enc.GetBytes( szCity );
			byte[] bszState		= enc.GetBytes( szState);
			byte[] bszZip		= enc.GetBytes( szZip );
			byte[] bszErrorText	= enc.GetBytes( szErrorText );

			// code added for GETSTREETPARTS
			byte[] bszNumber	= enc.GetBytes( szNumber );
			byte[] bszStreet	= enc.GetBytes( szStreet );
			byte[] bszUnit		= enc.GetBytes( szUnit );
			// end code addition
            int ResultCode=0;
            try
            {
                ResultCode = UNZ_CHECKADDRESS(hUnz, Line1, Line2, Line3, Line4);
            }
            catch { }
			// for return
			string strBaseXml	= "<verifyaddr><status></status><statusdesc></statusdesc><reccount></reccount><addresses></addresses></verifyaddr>";
			string strReturn	= null;
			string strReturnDesc= null;
			long intMatchCount	= 0;
			
			XmlDocument xmlDoc	= new XmlDocument();
			xmlDoc.LoadXml(strBaseXml);

			UpdateXmlElements(xmlDoc, "status", ResultCode.ToString());

			try
			{
				switch( ResultCode)
				{
					case XC_MULT: // multiple address
					
						UpdateXmlElements(xmlDoc, "statusdesc", "Multiple Addresses Found");
						intMatchCount = UNZ_GETMATCHCOUNT(hUnz);
						// append record count to xml
						UpdateXmlElements(xmlDoc, "reccount", intMatchCount.ToString());

						if (intMatchCount > 0)
						{
							for( int intRecordCount = 1; intRecordCount <= intMatchCount; intRecordCount++)
							{
								UNZ_GETMATCHADDR(hUnz, intRecordCount, bszFirmName, bszPRUrb, bszDelLine, bszLastLine);
								UNZ_GETCITYSTZIP(hUnz, bszCity, bszState, bszZip);
								UNZ_GETAREACODE(hUnz, bszAreaCode);
								
								// code added for GETSTREETPARTS
								UNZ_GETSTREETPARTS(hUnz, bszNumber, bszStreet, bszUnit);
								// end code addition
								CreateAddressXml(xmlDoc, bszDelLine, bszCity, bszState, bszZip, bszAreaCode, bszNumber, bszStreet, bszUnit);
							}
						}
						break;

					case  XC_BADADDR: // address uncorrectable
					case  XC_NODATA	: 
					case  XC_BADSTR	: 
					case  XC_BADNBR : 
					case  XC_NOSTR	: 
					case  XC_BADURB	: 
					case  XC_FHERR	: 
					
						UNZ_GETERRORTEXT (hUnz, bszErrorText);
						strReturnDesc = "Address uncorrectable: " + ByteArrayToString(bszErrorText);
						UpdateXmlElements(xmlDoc, "statusdesc", strReturnDesc);
						UpdateXmlElements(xmlDoc, "reccount", intMatchCount.ToString());	// append record count to xml
						// code added for GETSTREETPARTS
						UNZ_GETSTREETPARTS(hUnz, bszNumber, bszStreet, bszUnit);
						// end code addition
						CreateAddressXml(xmlDoc, bszDelLine, bszCity, bszState, bszZip, bszAreaCode, bszNumber, bszStreet, bszUnit);
						break;

					case XC_GOODADDR : // correct address

						UpdateXmlElements(xmlDoc, "statusdesc", "Correct Address");
						intMatchCount = 1;
						UpdateXmlElements(xmlDoc, "reccount", intMatchCount.ToString());	// append record count to xml
						UNZ_GETSTDADDRESS (hUnz, bszFirmName, bszPRUrb, bszDelLine, bszLastLine);
						UNZ_GETCITYSTZIP(hUnz, bszCity, bszState, bszZip);
						UNZ_GETAREACODE(hUnz, bszAreaCode);
						// code added for GETSTREETPARTS
						UNZ_GETSTREETPARTS(hUnz, bszNumber, bszStreet, bszUnit);
						// end code addition
						CreateAddressXml(xmlDoc, bszDelLine, bszCity, bszState, bszZip, bszAreaCode, bszNumber, bszStreet, bszUnit);
						break;

					default: // return address is fixed
					
						UpdateXmlElements(xmlDoc, "statusdesc", "Return Address Fixed");
						intMatchCount = 1;
						UpdateXmlElements(xmlDoc, "reccount", intMatchCount.ToString());	// append record count to xml
						UNZ_GETSTDADDRESS(hUnz, bszFirmName, bszPRUrb, bszDelLine, bszLastLine);
						UNZ_GETCITYSTZIP(hUnz, bszCity, bszState, bszZip);
						UNZ_GETAREACODE(hUnz, bszAreaCode);
						// code added for GETSTREETPARTS
						UNZ_GETSTREETPARTS(hUnz, bszNumber, bszStreet, bszUnit);
						// end code addition
						CreateAddressXml(xmlDoc, bszDelLine, bszCity, bszState, bszZip, bszAreaCode, bszNumber, bszStreet, bszUnit);
						break;
				}
			}
			catch (Exception ex)
			{
				strReturnDesc = "Address uncorrectable: " + ex.Message ;
				UpdateXmlElements(xmlDoc, "statusdesc", strReturnDesc);
			}
			finally
			{
				strReturn = CreateFinalXml(xmlDoc);
				if (xmlDoc!=null) 
					xmlDoc = null;
				
			}
			return (strReturn);
		}

		#region Supporting methods
		private string ByteArrayToString(byte[] bytArray) 
		{
			return (System.Text.Encoding.ASCII.GetString(bytArray));
		}

		private string CheckForSplCharacters(string input)
		{
			input = input.Replace('\0', ' ' ).Trim();
			input = input.Replace("&", "&amp;" ).Trim();
			input = input.Replace("<", "&lt;" ).Trim();
			input = input.Replace(">", "&gt;" ).Trim();
			input = input.Replace("\'", "&apos;" ).Trim();
			input = input.ToUpper();  
			return(input);
		}
		
		public static String ToSentence(String value)
		{
			return m_toSentence.Replace(value, new MatchEvaluator(ToSentenceCBK));
		}

		private static String ToSentenceCBK(Match m)
		{
			return m.Groups[1].Value.ToUpper() + m.Groups[2].Value.ToLower();
		}

		private void UpdateXmlElements(XmlDocument xmlDoc, string nodeName, string nodeText)
		{
			string[] arrLocalString = nodeText.Split(' '); 
			string strOutput = null;

			if (nodeName == "state")
			{
				xmlDoc.SelectSingleNode("//" + nodeName ).InnerText = CheckForSplCharacters(nodeText);
			}
			else
			{
				for(int i=0; i<= arrLocalString.GetUpperBound(0); i++)
				{
					strOutput = strOutput + ' ' + ToSentence(arrLocalString[i]).ToString();
				}
				xmlDoc.SelectSingleNode("//" + nodeName ).InnerText = CheckForSplCharacters(strOutput);
			}
		}

		private void CreateAddressXml(XmlDocument xmlDoc, byte[] bszDelLine, byte[] bszCity, byte[] bszState, byte[] bszZip, byte[] bszAreaCode, byte[] bszNumber, byte[] bszStreet, byte[] bszUnit)
		{
			string strAddressXml		= "<addr><addr1n2></addr1n2><city></city><state></state><zip></zip><areacode></areacode><number></number><street></street><unit></unit></addr>";
			XmlDocument xmlDocAddress	= new XmlDocument();
			xmlDocAddress.LoadXml(strAddressXml);

			UpdateXmlElements(xmlDocAddress, "addr1n2", ByteArrayToString(bszDelLine));
			UpdateXmlElements(xmlDocAddress, "city", ByteArrayToString(bszCity));
			UpdateXmlElements(xmlDocAddress, "state", ByteArrayToString(bszState));
			UpdateXmlElements(xmlDocAddress, "zip", ByteArrayToString(bszZip));
			UpdateXmlElements(xmlDocAddress, "areacode", ByteArrayToString(bszAreaCode));
			// code added for GETSTREETPARTS
			UpdateXmlElements(xmlDocAddress, "number", ByteArrayToString(bszNumber));
			UpdateXmlElements(xmlDocAddress, "street", ByteArrayToString(bszStreet));
			UpdateXmlElements(xmlDocAddress, "unit", ByteArrayToString(bszUnit));
			// end code addition
			XmlNode rootNodeToAppend	= xmlDocAddress.GetElementsByTagName("addr").Item(0); 
			XmlNode rootNodeLocation	= xmlDoc.GetElementsByTagName ("addresses").Item(0); 
			XmlNode nodeReference		= xmlDoc.GetElementsByTagName("").Item(0); 

			XmlNode newNode				= xmlDoc.ImportNode(rootNodeToAppend, true); 
			rootNodeLocation.InsertAfter(newNode, nodeReference); 
			xmlDocAddress = null;
		}

		private string CreateFinalXml(XmlDocument xmlDoc)		
		{
			return(xmlDoc.InnerXml);
		}
		#endregion

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

	}
}
