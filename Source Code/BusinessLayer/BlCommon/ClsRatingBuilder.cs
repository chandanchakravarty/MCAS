using System;
using System.Data;
using System.Xml;
using System.IO;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Configuration;
namespace Cms.BusinessLayer.BlCommon
{

	
	/// <summary>
	/// Summary description for ClsRatingBuilder.
	/// </summary>	
	public class ClsRatingBuilder
	{

		private static  string xmlFilePath = System.Web.HttpContext.Current.Server.MapPath(ConfigurationSettings.AppSettings["MasterDataXMLPath"]);
	
		private static string 	xslFilePath = System.Web.HttpContext.Current.Server.MapPath(ConfigurationSettings.AppSettings["MasterDataXSLPath"]);

	
		public bool ReadOnlyFile()
		{
			try
			{
				return true;
			}
			catch
			{
			return true;
			}
			finally
			{
			
			}
		}
	 
		public ClsRatingBuilder()
		{

			
		}

		/// <summary>
		/// This method is  used to fetch the products from the XML file.
		/// </summary>
		public static DataSet FetchProducts()
		{
			try
			{
				DataSet dsTemp = new DataSet("PRODUCTS");
				dsTemp.ReadXml(xmlFilePath);
				return dsTemp;
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{
			
			}
		}

		/// <summary>
		/// This method is  used to fetch the factors of a selected product from the XML file.
		/// </summary>
		public static DataSet FetchFactors(string ProductID)
		{
			try
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(xmlFilePath);
				XmlElement xmlElement= xmlDoc.DocumentElement;
				XmlNode nodFACTOR=xmlElement.SelectSingleNode("//PRODUCT[@ID='"+ProductID+"']");
				System.IO.StringReader xmlSR = new System.IO.StringReader("<DataSet>"+nodFACTOR.InnerXml +"</DataSet>");
				DataSet dsTemp = new DataSet("FACTORS");
				dsTemp.ReadXml(xmlSR);
				return dsTemp;

			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{
			 
			}
		}
		/// <summary>
		/// This method is  used to fetch the nodes of a selected factor and product from the XML file.
		/// </summary>
		public static DataSet FetchNodes(string ProductID,string FactorID)
		{
			try
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(xmlFilePath);	
				XmlElement xmlElement= xmlDoc.DocumentElement;
				XmlNode nodNODE=xmlElement.SelectSingleNode(@"//PRODUCT[@ID='"+ProductID+"']//FACTOR[@ID='"+FactorID+"'] ");
				System.IO.StringReader xmlSR = new System.IO.StringReader("<DataSet>"+nodNODE.InnerXml +"</DataSet>");
				DataSet dsTemp = new DataSet("NODES");
				dsTemp.ReadXml(xmlSR);
				return dsTemp;
				
				 

			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{
			
			}
		}

		
			/// <summary>
			/// This method is  used to fetch the nodes of a selected factor and product from the XML file.
			/// </summary>
			public static DataSet GetAttributes(string ProductID,string FactorID,string NodeID)
			{
				try
				{
					XmlDocument xmlDoc = new XmlDocument();
					xmlDoc.Load(xmlFilePath);
					XmlElement xmlElement= xmlDoc.DocumentElement;
					XmlNode nodNODE=xmlElement.SelectSingleNode(@"//PRODUCT[@ID='"+ProductID+"']//FACTOR[@ID='"+FactorID+"']//NODE[@ID='"+NodeID+"'] ");
					System.IO.StringReader xmlSR = new System.IO.StringReader("<NODE>"+nodNODE.InnerXml +"</NODE>");
					DataSet dsTemp = new DataSet("NODES");
					dsTemp.ReadXml(xmlSR);
					return dsTemp;		
				 

				}
				catch(Exception exc)
				{
					throw(exc);
				}
				finally
				{
			
				}
			}

		/// <summary>
		/// This method is  used to fetch the nodes of a selected factor and product from the XML file.
		/// </summary>
		public static string GetNamesOfAttributes(string ProductID,string FactorID,string NodeID)
		{
			try
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(xmlFilePath);
				XmlElement xmlElement= xmlDoc.DocumentElement;
				XmlNode nodNODE=xmlElement.SelectSingleNode(@"//PRODUCT[@ID='"+ProductID+"']//FACTOR[@ID='"+FactorID+"']//NODE[@ID='"+NodeID+"'] ");
				System.IO.StringReader xmlSR = new System.IO.StringReader("<NODE>"+nodNODE.InnerXml +"</NODE>");
				DataSet dsTemp = new DataSet("NODES");
				dsTemp.ReadXml(xmlSR);
				string colNames="";
				for (int colNumber=0; colNumber< dsTemp.Tables[0].Columns.Count ;colNumber++)
				{
					if (colNames.Trim()=="")
					{
						colNames =dsTemp.Tables[0].Columns[colNumber].ColumnName;
					}
					else
					{
						colNames =colNames +"^"+dsTemp.Tables[0].Columns[colNumber].ColumnName ;
					}

				}
				return colNames;		
				 

			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{
			
			}
		}

		/// <summary>
		/// This method is  used to Transform the file and output an HTML string.
		/// </summary>
		public static string GetTransformedXMLAttributes(string ProductID,string FactorID,string NodeID)
		{
			StringWriter writer = new StringWriter();
			XslTransform xslt = new XslTransform();				
			XmlDocument xmlDocTemp = new XmlDocument();					

			try
			{
				// Load the xsl file
				xslt.Load(xslFilePath);				
				
				//fill the dataset with the attribute nodes
				DataSet dsTEMP= ClsRatingBuilder.GetAttributes(ProductID ,FactorID,NodeID);
				xmlDocTemp.LoadXml(dsTEMP.GetXml());
			
				//Create the navigator object and transform the xml
				XPathNavigator nav = ((IXPathNavigable) xmlDocTemp).CreateNavigator();
				xslt.Transform(nav,null,writer);
 		
				return writer.ToString();
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{
				writer.Close();					
			}
		}
		/// <summary>
		/// This method is  used to replace the node .
		/// </summary>
		public static void ReplaceNode(string ProductID,string FactorID,string NodeID, string newInnerXML)
		{
			try
			{			
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(xmlFilePath);
				XmlElement xmlElement= xmlDoc.DocumentElement;
				XmlNode nodNODE=xmlElement.SelectSingleNode(@"//PRODUCT[@ID='"+ProductID+"']//FACTOR[@ID='"+FactorID+"'] //NODE[@ID='"+NodeID+"'] ");
				nodNODE.InnerXml = newInnerXML;
				xmlDoc.Save(xmlFilePath);
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{
			
			}
		}

		public static void AppendAttribute(string ProductID,string FactorID,string NodeID, string newAttribute) 
		{
			try
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(xmlFilePath);
				XmlElement xmlElement= xmlDoc.DocumentElement;
				XmlNode  nodNODE=xmlElement.SelectSingleNode(@"//PRODUCT[@ID='"+ProductID+"']//FACTOR[@ID='"+FactorID+"'] //NODE[@ID='"+NodeID+"'] ");

				XmlAttribute child = xmlDoc.CreateAttribute(newAttribute);
				child.Value = "";
			

				foreach(XmlNode nodTemp in nodNODE.ChildNodes)
				{
					nodTemp.Attributes.Append(child);	
					xmlDoc.Save(xmlFilePath);
				}

				
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{}
		}


	}
}
