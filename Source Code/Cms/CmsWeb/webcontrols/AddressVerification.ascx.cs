namespace Cms.CmsWeb.webcontrols
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Xml;
	/// <summary>
	///		Summary description for AddressVerification.
	/// </summary>
	public class AddressVerification : System.Web.UI.UserControl
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			//register 
			Ajax.Utility.RegisterTypeForAjax(typeof(AddressVerification)); 
			Ajax.Utility.RegisterTypeForAjax(typeof(AddressDetails));
			Ajax.Utility.RegisterTypeForAjax(typeof(Address));
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}

	#region address object
	// class holds basic address info
	[Serializable()]
	public struct Address
	{
		private string _address1n2;
        private string _address2;
		private string _city;
		private string _state;
		private string _zip;
		private string _areacode;
		private string _number;
		private string _street;
		private string _unit;
        private string _District;
		public string Address1n2
		{
			get
			{
				return _address1n2;
			}
			set
			{
				_address1n2 = value;
			}
		}
        public string Address2
        {
            get
            {
                return _address2;
            }
            set
            {
                _address2 = value;
            }
        }

		public string City
		{
			get
			{
				return _city;
			}
			set
			{
				_city = value;
			}
		}

		public string State
		{
			get
			{
				return _state;
			}
			set
			{
				_state = value;
			}
		}
        public string District 
        { 
            get 
            {
                return _District; 
            }
            set
            {
                _District = value;
            }
        }
        
		public string Zip
		{
			get
			{
				return _zip;
			}
			set
			{
				_zip = value;
			}
		}

		public string AreaCode
		{
			get
			{
				return _areacode;
			}
			set
			{
				_areacode = value;
			}
		}
		public string Number
		{
			get
			{
				return _number;
			}
			set
			{
				_number = value;
			}
		}
		public string Street
		{
			get
			{
				return _street;
			}
			set
			{
				_street = value;
			}
		}
		public string Unit
		{
			get
			{
				return _unit;
			}
			set
			{
				_unit = value;
			}
		}
	}
	#endregion

	#region AddressDetails
	
	// return XML is parsed and populated 
	// sample return XML from the webservice 
	// <verifyaddr><status></status><statusdesc></statusdesc><reccount></reccount><addresses><addr><addr1n2></addr1n2><city></city><state></state><zip></zip><areacode></areacode></addr></addresses></verifyaddr>";
	// webreference to "http://wolverine.ebix.com/services/verifyaddr/verifyaddress.asmx"

	[Serializable()]
	public class AddressDetails
	{
		private const string CRITICAL_ERROR			= "999";
		private const string CRITICAL_ERROR_MSG		= "Critical Error";

		private string _status;
		private string _statusdesc;
		private string _reccount;
		private Address[] _address;
        		
		public string Status
		{
			get { return _status; }
		}
		public string StatusDesc
		{
			get { return _statusdesc; }
		}
		public string RecCount
		{
			get { return _reccount; }
		}

		public Address[] AddressProperty
		{
			get { return _address; }
		}

		private void SetAddressBound(int MaxNoOfAddress)
		{
			this._address = new Address[MaxNoOfAddress];
		}
		
		public AddressDetails(){}

		[Ajax.AjaxMethod()]
        public static AddressDetails GetAddressDetails(string Address1n2,  string City, string State, string Zip)
		{
			// web reference; re-code this method as required.
			string CmsWebUrl = System.Configuration.ConfigurationSettings.AppSettings["CmsWebUrl"].ToString();
			Cms.CmsWeb.com.ebix.wolverine.VerifyAddress obj = new Cms.CmsWeb.com.ebix.wolverine.VerifyAddress(CmsWebUrl);
            string strXmlReturn = obj.CheckAddress("", "", Address1n2, City + " " + State + " " + Zip);
			
			return (CreateAddressDetails(strXmlReturn));
		}
          [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public static AddressDetails GetAddressDetailsBR(string Address1n2,string District, string City, string State, string Zip)
        {
           
                return (CreateAddressDetailsBR(Zip));
            
        }
        /// <summary>
        /// Create the Address Details for the brazil Implementation
        /// </summary>
        /// <param name="strXmlReturn"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod(true)]
        public static AddressDetails CreateAddressDetailsBR(String ZipCode)
        { 
             
			AddressDetails objAddrDtls	= new AddressDetails();
             DataSet ds = new DataSet();
            try
            {

                    string strBaseXml = "<verifyaddr><status></status><statusdesc></statusdesc><reccount></reccount><addresses></addresses></verifyaddr>";
                  
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(strBaseXml);

                    objAddrDtls._status = "1";//GetValueFromXml(xmlDoc, "status");
                    objAddrDtls._reccount = "1";// GetValueFromXml(xmlDoc, "reccount");
                    objAddrDtls.SetAddressBound(int.Parse(objAddrDtls._reccount));
                   
				 
                    CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();  //Declare the object ob common webservice

                    String ReturnValue = String.Empty;
                

                    Int32 ContryID = 5;//For the Brazil 
                   
                    ReturnValue = obj.GetAddressDetailsBasedOnZipeCode(ZipCode, ContryID);
                    if (ReturnValue != "")
                    {
                        String[] Addresses = ReturnValue.Split('^');

                        if (Addresses[2].ToString() != "")
                            objAddrDtls._address[0].Zip = Addresses[2].ToString();
                        else
                            objAddrDtls._address[0].Zip = String.Empty;
                        if (Addresses[3].ToString() != "")
                            objAddrDtls._address[0].Address1n2 = Addresses[3].ToString() + " " +Convert.ToString(Addresses[4]);
                        else
                            objAddrDtls._address[0].Address1n2 = String.Empty;
                        //if (Addresses[4].ToString() != "")
                        //    objAddrDtls._address[0].Address2 = Addresses[4].ToString();
                        //else
                        //    objAddrDtls._address[0].Address2 = String.Empty;
                        if (Addresses[5].ToString() != "")
                            objAddrDtls._address[0].District = Addresses[5].ToString();
                        else
                            objAddrDtls._address[0].District = String.Empty;
                        if (Addresses[5].ToString() != "")
                            objAddrDtls._address[0].City = Addresses[6].ToString();
                        else
                            objAddrDtls._address[0].City = String.Empty;
                        if (Addresses[1].ToString() != "")
                            objAddrDtls._address[0].State = Addresses[7].ToString();
                        else
                            objAddrDtls._address[0].State = String.Empty;
                        objAddrDtls._statusdesc = "RETURN ADDRESS FIXED";
                    }
                    else
                        {
                           //if( Cms.BusinessLayer.BlCommon.ClsCommon.BL_LANG_ID==2)
                           //    objAddrDtls._statusdesc = "Detalhes de endereço não corresponde!"; 
                           //else
                           //    objAddrDtls._statusdesc = "Address Details does not match !"; 
                            objAddrDtls._statusdesc = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1885");
                    }
               
            }
            catch (Exception ex)
            {  
                objAddrDtls._status = CRITICAL_ERROR;
                objAddrDtls._statusdesc = CRITICAL_ERROR_MSG;
                System.Diagnostics.Debug.Write(ex.Message);
            }
            finally
            {
                if (ds != null) ds = null;
            }

            return (objAddrDtls);
        }

		public static AddressDetails CreateAddressDetails(string strXmlReturn)
		{
			
			XmlDocument xmlDoc	= new XmlDocument();
			AddressDetails objAddrDtls	= new AddressDetails();	

			try
			{
				xmlDoc.LoadXml(strXmlReturn);

				objAddrDtls._status			= GetValueFromXml(xmlDoc, "status") ;
				objAddrDtls._statusdesc		= GetValueFromXml(xmlDoc, "statusdesc");
				objAddrDtls._reccount 		= GetValueFromXml(xmlDoc, "reccount");
				objAddrDtls.SetAddressBound(int.Parse(objAddrDtls._reccount));
				if(objAddrDtls._reccount!="0")
				{
					// fetch multiple addr methods if any
					XmlNodeList nodeList = xmlDoc.SelectNodes ("//addr" ); 
					for (int intCount=0; intCount < nodeList.Count; intCount++)
					{
						if(nodeList.Item(intCount).SelectNodes("//addr1n2").Item(intCount).InnerXml!="")
							objAddrDtls._address[intCount].Address1n2 		= nodeList.Item(intCount).SelectNodes("//addr1n2").Item(intCount).InnerXml.ToString() ;

						if(nodeList.Item(intCount).SelectNodes("//city").Item(intCount).InnerXml!="")
							objAddrDtls._address[intCount].City				= nodeList.Item(intCount).SelectNodes("//city").Item(intCount).InnerXml.ToString() ;

						if(nodeList.Item(intCount).SelectNodes("//state").Item(intCount).InnerXml!="")
							objAddrDtls._address[intCount].State	 		= nodeList.Item(intCount).SelectNodes("//state").Item(intCount).InnerXml.ToString() ;

						if(nodeList.Item(intCount).SelectNodes("//zip").Item(intCount).InnerXml!="")
							objAddrDtls._address[intCount].Zip		 		= nodeList.Item(intCount).SelectNodes("//zip").Item(intCount).InnerXml.ToString() ;

						if(nodeList.Item(intCount).SelectNodes("//areacode").Item(intCount).InnerXml!="")
							objAddrDtls._address[intCount].AreaCode  		= nodeList.Item(intCount).SelectNodes("//areacode").Item(intCount).InnerXml.ToString() ;

						if(nodeList.Item(intCount).SelectNodes("//number").Item(intCount).InnerXml!="")
							objAddrDtls._address[intCount].Number  		= nodeList.Item(intCount).SelectNodes("//number").Item(intCount).InnerXml.ToString() ;
						else
							objAddrDtls._address[intCount].Number  		= "";

						if(nodeList.Item(intCount).SelectNodes("//street").Item(intCount).InnerXml!="")
							objAddrDtls._address[intCount].Street  		= nodeList.Item(intCount).SelectNodes("//street").Item(intCount).InnerXml.ToString() ;
						else
							objAddrDtls._address[intCount].Street  		= "";

						if(nodeList.Item(intCount).SelectNodes("//unit").Item(intCount).InnerXml!="")
							objAddrDtls._address[intCount].Unit  		= nodeList.Item(intCount).SelectNodes("//unit").Item(intCount).InnerXml.ToString() ;
						else
							objAddrDtls._address[intCount].Unit  		= "";

					}
				}
			}
			catch(Exception ex)
			{
				objAddrDtls._status			= CRITICAL_ERROR;
				objAddrDtls._statusdesc		= CRITICAL_ERROR_MSG;
				System.Diagnostics.Debug.Write(ex.Message);			
			}
			finally
			{
				if (xmlDoc!= null) xmlDoc = null;
			}
	
			return(objAddrDtls);
	
		}	

		private static string GetValueFromXml(XmlDocument xmlDoc, string nodeName)
		{
			return(xmlDoc.SelectSingleNode("//" + nodeName).InnerText.ToString()); 
		}
		#endregion

	}

}
