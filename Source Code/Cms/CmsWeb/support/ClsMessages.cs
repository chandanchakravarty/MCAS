using System;
using System.Text;
using System.Xml; 
using System.IO; 
using System.Configuration;
/******************************************************************************************
	<Author					: Nidhi >
	<Start Date				: March 20, 2005 >
	<End Date				: March 21, 2005 >
	<Description			: This class is used for extracting generalized messages. All the methods of this class will be called across the application for fetching messages to be prompted to the users on the screen>
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: >
	<Modified By			: >
	<Purpose				: >
	
	<Modified Date			: >
	<Modified By			: >
	<Purpose				: >
					
*******************************************************************************************/
namespace Cms.CmsWeb
{
	/// <summary>
	///  Class for extracting generalized messages to be prompted across the application
	/// </summary>
	public class ClsMessages
	{
		private static XmlDocument docMessages;
        private static String LangCulture;
        public static bool IsEODProcess=false;
		private const string GENERALMESSAGESSCREENID="G";

		public ClsMessages()
		{  }
		
		static ClsMessages()
		{ }       

        /// <summary>
        /// Loads the XML document for customised messages based on Current Culture
        /// </summary>
        /// <param name="lang_culture">LANGUAGE-CULTURE</param>
        /// Added by Charles on 9-Mar-2010 for Multilingual Implementation
        public static void SetCustomizedXml(string lang_culture)
        {
            try
            {
                if (docMessages == null)
                {
                    docMessages = new XmlDocument();
                    LangCulture = lang_culture;
                    docMessages.Load(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["XMLMessagesPath"]));
                }
                //if (!IsEODProcess)
                //System.Web.HttpContext.Current.Session.Add("docMessages", docMessages.InnerXml);

                /*if (lang_culture == "en-US" || lang_culture == "" || lang_culture == null )
                {
                    docMessages.Load(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["XMLMessagesPath"]));
                }
                else
                {
                    docMessages.Load(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["XMLMessagesPath"].Replace(".xml", "." + lang_culture + ".xml")));
                }
                */
            }
            catch (Exception exc)
            {
                throw (exc);
            }            
        }
        public static string LANG_CULTURE
        {
            set
            {
                LangCulture  = value;
            }
            get
            {
                if (IsEODProcess)
                {
                    return LangCulture;
                }
                else if (System.Web.HttpContext.Current.Session != null && System.Web.HttpContext.Current.Session["languageCode"] != null)
                {
                    LangCulture = System.Web.HttpContext.Current.Session["languageCode"].ToString();
                    return System.Web.HttpContext.Current.Session["languageCode"].ToString();
                }
                else
                {
                    System.Web.HttpContext.Current.Response.Redirect("/cms/cmsweb/aspx/login.aspx", true);
                    return "en-US";
                }
            }
        }
        private static XmlDocument getdocMessages()
        {
            try
            {
               if (docMessages != null)
                {
                    return docMessages;
                }
                else
                {
                    //docMessages = null;
                    docMessages = new XmlDocument();
                    docMessages.Load(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["XMLMessagesPath"]));
                    return docMessages;
                }
            }
            catch (Exception exc)
            {
                throw (new Exception("Error while loading the CustomizedMessages XML.", exc));
            }
        }
        /// <summary>
        /// Accepts Screen ID and tab ID and returns the string message.
        /// </summary>
        /// <param name="screenID">ScreenID of the screen whose message has to be fetched.</param>
        /// <param name="tabID">TabID of the Tab whose titles has to be fetched.</param> 
        /// <returns>Tab Title as a string object</returns>
        /// Added by Charles on 9-Mar-2010 for Multilingual Implementation
        public static string GetTabTitles(string screenID, string tabID)
        {
            try
            {
                docMessages = getdocMessages();

                XmlNode root = docMessages.FirstChild;
                bool screenIDFound = false;
                if (screenID == "")
                    screenID = GENERALMESSAGESSCREENID;

                XmlNode nodScreen = root.SelectSingleNode("Culture[@Code='" + LANG_CULTURE + "']/screen[@screenid='" + screenID + "']");
                if (nodScreen != null)
                {
                    screenIDFound = true;
                    if (nodScreen.ChildNodes.Count == 0)
                    {                       
                        return "";
                    }
                    else
                    {
                        bool tabIdFound = false;
                        XmlNode nodMessage = nodScreen.SelectSingleNode("tab[@tabid='" + tabID + "']");
                        if (nodMessage != null)
                        {
                            tabIdFound = true;
                            return nodMessage.InnerText;
                        }
                        if (!tabIdFound)
                        {                           
                            return "";
                        }
                    }
                }
                else
                {                    
                    return "";
                }
                if (!screenIDFound)
                {
                    return "";
                }
                return "";
            }
            catch (Exception exc)
            {
                throw (exc);
            }            	
        }

        /// <summary>
        /// Accepts Screen ID and button ID and returns the button text.
        /// </summary>
        /// <param name="screenID">ScreenID of the screen whose message has to be fetched.</param>
        /// <param name="buttonID">ButtonID of the Buttons whose text has to be fetched.</param> 
        /// <returns>Button Caption as a string object</returns>
        /// Added by Charles on 9-Mar-2010 for Multilingual Implementation
        public static string GetButtonsText(string screenID, string buttonID)
        {
            try
            {
                docMessages = getdocMessages();
                XmlNode root = docMessages.FirstChild;
                bool screenIDFound = false;
                if (screenID == "")
                    screenID = GENERALMESSAGESSCREENID;

                XmlNode nodScreen = root.SelectSingleNode("Culture[@Code='" + LANG_CULTURE + "']/screen[@screenid='" + screenID + "']");
                if (nodScreen != null)
                {
                    screenIDFound = true;
                    if (nodScreen.ChildNodes.Count == 0)
                    {
                        return FetchGeneralButtonsText(buttonID);
                    }
                    else
                    {
                        bool buttonIdFound = false;
                        XmlNode nodMessage = nodScreen.SelectSingleNode("button[@buttonid='" + buttonID + "']");
                        if (nodMessage != null)
                        {
                            buttonIdFound = true;
                            return nodMessage.InnerText;
                        }
                        if (!buttonIdFound)
                        {
                            return FetchGeneralButtonsText(buttonID);
                        }
                    }
                }
                else
                {
                    return FetchGeneralButtonsText(buttonID);
                }
                if (!screenIDFound)
                {
                    return FetchGeneralButtonsText(buttonID);
                }
                return "";
            }
            catch (Exception exc)
            {
                throw (exc);
            }
        }

        /// <summary>
        /// It accepts Is_Acrive and returns the string Caption for button btnActivateDeactivate from the general messages. 
        /// </summary>        
        /// <param name="condition">NEW or Y or N</param>  
        /// <returns>btnActivateDeactivate Caption as a string object</returns>
        /// Added by Charles on 22-Mar-2010 for Multilingual Implementation
        public static string FetchActivateDeactivateButtonsText(string condition)
        {
            try
            {
                docMessages = getdocMessages();
                XmlNode root = docMessages.FirstChild;
                bool foundMessageID = false;
                if (condition == "" || condition == null || condition =="0")
                {
                    condition = "NEW";
                }
                XmlNode nodScreen = root.SelectSingleNode("Culture[@Code='" + LANG_CULTURE + "']/screen[@screenid='" + GENERALMESSAGESSCREENID + "']");
                if (nodScreen != null)
                {
                    XmlNode nodMessage = nodScreen.SelectSingleNode("button[@buttonid='btnActivateDeactivate'][@condition='"+ condition +"']");
                    if (nodMessage != null)
                    {
                        foundMessageID = true;
                        return nodMessage.InnerText;
                    }
                }
                if (!foundMessageID)
                {
                    return "";
                }

                return "";
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// It accepts ButtonID and returns the string Caption against the button id from the general messages. 
        /// </summary>
        /// <param name="buttonID">ButtonID of the button whose text property has to be fetched.</param>  
        /// <returns>Button Caption as a string object</returns>
        /// Added by Charles on 11-Mar-2010 for Multilingual Implementation
        public static string FetchGeneralButtonsText(string buttonID)
        {
            try
            {
                docMessages = getdocMessages();
                XmlNode root = docMessages.FirstChild;
                bool foundMessageID = false;
                XmlNode nodScreen = root.SelectSingleNode("Culture[@Code='" + LANG_CULTURE + "']/screen[@screenid='" + GENERALMESSAGESSCREENID + "']");
                if (nodScreen != null)
                {
                    XmlNode nodMessage = nodScreen.SelectSingleNode("button[@buttonid='" + buttonID + "']");
                    if (nodMessage != null)
                    {
                        foundMessageID = true;
                        return nodMessage.InnerText;
                    }
                }
                if (!foundMessageID)
                {
                    return "";
                }

                return "";
            }
            catch 
            {
                return "";
            }            
        }       

		#region FUNCTIONS FOR CUSTOMIZED MESSAGES

		/// <summary>
		/// Accepts Screen ID and message ID and returns the string message.
		/// </summary>
		/// <param name="screenID">ScreenID of the screen whose message has to be fetched</param>
		/// <param name="messageID">MessageID of the message that has to be fetched. 
		/// If it does not exist in the screen having the passed ScreenID, then it will be fetched from the general messages.</param>
		/// <returns>Message as a string object</returns>
		public static string GetMessage(string screenID,string messageID)
		{
			try
			{                
				// Traverse to the node having the attribute screenid = screenID
                docMessages = getdocMessages();
                XmlNode root = docMessages.FirstChild;
				bool screenIDFound=false;
                XmlNode nodScreen = root.SelectSingleNode("Culture[@Code='" + LANG_CULTURE + "']/screen[@screenid='" + screenID + "']");
				if ( nodScreen != null)
				{
					screenIDFound =true;	
					if(nodScreen.ChildNodes.Count==0)
					{
						/* If this screenid node exists and the message id does not exist
							* then extract the message from the screenId="G"
							*/							
						return FetchGeneralMessage(messageID);
					}
					else
					{
						bool messageIdFound=false;
						XmlNode nodMessage = nodScreen.SelectSingleNode("message[@messageid='"+messageID +"']");
						if  (nodMessage != null)
						{
							messageIdFound=true;
							return nodMessage.InnerText;							
						}
						if (!messageIdFound)
						{
							/* If this screenid node exists and the message id does not exist
								* then extract the message from the screenId="G" */	
							return FetchGeneralMessage(messageID);
						}
					}
				}
				else
				{
					/* If this screenid node doesnt exist
						* then fetch from general section */
					return FetchGeneralMessage(messageID);

				}
				if (!screenIDFound )
				{
					return FetchGeneralMessage(messageID);
				}
				return "";
			}
			catch
			{
                return "";
			}
			finally
			{
			
			}		
		}

		
		/// <summary>
		/// Accepts Screen ID , message ID,,Punctuation Mark and returns the string message.
		/// </summary>
		/// <param name="screenID">ScreenID of the screen whose message has to be fetched</param>
		/// <param name="messageID">MessageID of the message that has to be fetched. 
		/// If it does not exist in the screen having the passed ScreenID, then it will be fetched from the general messages.</param>
		/// <param name="punctuationMark">Punctuation mark that has to be appended in the end of the message. Can be '.','?','!' etc</param>
		/// <returns>Message as a string object</returns>
		private static string GetMessage(string screenID,string messageID,string punctuationMark)
		{
			try
			{                
				// Traverse to the node having the attribute screenid = screenID
                docMessages = getdocMessages();
                XmlNode root = docMessages.FirstChild;
				bool screenIDFound=false;
                XmlNode nodScreen = root.SelectSingleNode("Culture[@Code='" + LANG_CULTURE + "']/screen[@screenid='" + screenID + "']");
				if ( nodScreen != null)
				{
					screenIDFound =true;	
					XmlNodeList nodMessages = nodScreen.ChildNodes;
					if(nodMessages.Count==0)
					{
						/* If this screenid node exists and the message id does not exist
							* then extract the message from the screenId="G"
							*/							
						return FetchGeneralMessage(messageID)+punctuationMark;
					}
					else
					{
						bool messageIdFound=false;
						XmlNode nodMessage = nodScreen.SelectSingleNode("message[@messageid='"+messageID +"']");
						if  (nodMessage != null)
						{
							messageIdFound=true;
							return nodMessage.InnerText+punctuationMark;							
						}
						if (!messageIdFound)
						{
							/* If this screenid node exists and the message id does not exist
								* then extract the message from the screenId="G" */	
							return FetchGeneralMessage(messageID)+punctuationMark;
						}
					}
				}
				else
				{
					/* If this screenid node doesnt exist
						* then fetch from general section */
					return FetchGeneralMessage(messageID)+punctuationMark;

				}
				if (!screenIDFound )
				{
					return FetchGeneralMessage(messageID)+punctuationMark;
				}
				return "";
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
		/// Overloaded method. It accepts ScreenID,MessageID,User Message,Position of message and returns the string message. 
		/// </summary>
		/// <param name="screenID">ScreenID of the screen whose message has to be fetched</param>
		/// <param name="messageID">MessageID of the message that has to be fetched. 
		/// If it does not exist in the screen having the passed ScreenID, then it will be fetched from the general messages.</param>
		/// <param name="appendMessage">User message that has to be appended as prefix or suffix along with the fetched message.</param>
		/// <param name="prefix">If true, appendMessage will be prefixed to the fetched message. If false, appendMessage will be suffixed to the fetched message</param>
		/// <returns>Concatenated message as a string object</returns>
		private static string GetMessage(string screenID,string messageID, string appendMessage, bool prefix)
		{
			try
			{               
				// Traverse to the node having the attribute screenid = screenID
                docMessages = getdocMessages();
                XmlNode root = docMessages.FirstChild;
				bool screenIDFound=false;
				string returnMessage="";
                XmlNode nodScreen = root.SelectSingleNode("Culture[@Code='" + LANG_CULTURE + "']/screen[@screenid='" + screenID + "']");
				if ( nodScreen != null)
				{
					screenIDFound =true;	
					XmlNodeList nodMessages = nodScreen.ChildNodes;
					if(nodMessages.Count==0)
					{
						/* If this screenid node exists and the message id does not exist
							* then extract the message from the screenId="G"
							*/		
						returnMessage = FetchGeneralMessage(messageID);
						if (prefix)
						{
							return appendMessage + " " + returnMessage;
						}
						else
						{
							return returnMessage + " " + appendMessage;
						}
						
					}
					else
					{
						bool messageIdFound=false;
						XmlNode nodMessage = nodScreen.SelectSingleNode("message[@messageid='"+messageID +"']");
						if  (nodMessage != null)
						{
							messageIdFound=true;
							returnMessage = nodMessage.InnerText;
							if (prefix)
							{
								return appendMessage + " " + returnMessage;
							}
							else
							{
								return returnMessage + " " + appendMessage;
							}		 			
						}
						if (!messageIdFound)
						{
							/* If this screenid node exists and the message id does not exist
							* then extract the message from the screenId="G"
							*/		
							returnMessage = FetchGeneralMessage(messageID);
							if (prefix)
							{
								return appendMessage + " " + returnMessage;
							}
							else
							{
								return returnMessage + " " + appendMessage;
							}
						}
						
					}
				}
				else
				{
					/* If this screenid node doesnt exist
					 * then fetch the message from the general section (from the screenId="G")*/
					returnMessage = FetchGeneralMessage(messageID);
					if (prefix)
					{
						return appendMessage + " " + returnMessage;
					}
					else
					{
						return returnMessage + " " + appendMessage;
					}

				}
				if (!screenIDFound )
				{
					returnMessage = FetchGeneralMessage(messageID);
					if (prefix)
					{
						return appendMessage + " " + returnMessage;
					}
					else
					{
						return returnMessage + " " + appendMessage;
					}
				}
				return "";
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
		/// Overloaded method. It accepts ScreenID,MessageID,User Message,Position of message,Punctuation Mark and returns the string message. 
		/// </summary>
		/// <param name="screenID">ScreenID of the screen whose message has to be fetched</param>
		/// <param name="messageID">MessageID of the message that has to be fetched. 
		/// If it does not exist in the screen having the passed ScreenID, then it will be fetched from the general messages.</param>
		/// <param name="appendMessage">User message that has to be appended as prefix or suffix along with the fetched message.</param>
		/// <param name="prefix">If true, appendMessage will be prefixed to the fetched message. If false, appendMessage will be suffixed to the fetched message</param>
		/// <param name="punctuationMark">Punctuation mark that has to be appended in the end of the message. Can be '.','?','!' etc</param>
		/// <returns>Concatenated message as a string object</returns>
		private static string GetMessage(string screenID,string messageID, string appendMessage, bool prefix, string punctuationMark)
		{
			try
			{               
				// Traverse to the node having the attribute screenid = screenID
                docMessages = getdocMessages();
                XmlNode root = docMessages.FirstChild;
				bool screenIDFound=false;
				string returnMessage="";
                XmlNode nodScreen = root.SelectSingleNode("Culture[@Code='" + LANG_CULTURE + "']/screen[@screenid='" + screenID + "']");
				if ( nodScreen != null)
				{
					screenIDFound =true;	
					XmlNodeList nodMessages = nodScreen.ChildNodes;
					if(nodMessages.Count==0)
					{
						/* If this screenid node exists and the message id does not exist
							* then extract the message from the screenId="G"
							*/		
						returnMessage = FetchGeneralMessage(messageID);
						if (prefix)
						{
							return appendMessage + " " + returnMessage+punctuationMark;
						}
						else
						{
							return returnMessage + " " + appendMessage+punctuationMark;
						}
						
					}
					else
					{
						bool messageIdFound=false;
						XmlNode nodMessage = nodScreen.SelectSingleNode("message[@messageid='"+messageID +"']");
						if  (nodMessage != null)
						{
							messageIdFound=true;
							returnMessage = nodMessage.InnerText;
							if (prefix)
							{
								return appendMessage + " " + returnMessage+punctuationMark;
							}
							else
							{
								return returnMessage + " " + appendMessage+punctuationMark;
							}
						}
						if (!messageIdFound)
						{
							/* If this screenid node exists and the message id does not exist
							* then extract the message from the screenId="G"
							*/		
							returnMessage = FetchGeneralMessage(messageID);
							if (prefix)
							{
								return appendMessage + " " + returnMessage+punctuationMark;
							}
							else
							{
								return returnMessage + " " + appendMessage+punctuationMark;
							}
						}
						
					}
				}
				else
				{
					/* If this screenid node doesnt exist
					 * then fetch the message from the general section (from the screenId="G")*/
					returnMessage = FetchGeneralMessage(messageID);
					if (prefix)
					{
						return appendMessage + " " + returnMessage+punctuationMark;
					}
					else
					{
						return returnMessage + " " + appendMessage+punctuationMark;
					}

				}
				if (!screenIDFound )
				{
					returnMessage = FetchGeneralMessage(messageID);
					if (prefix)
					{
						return appendMessage + " " + returnMessage+punctuationMark;
					}
					else
					{
						return returnMessage + " " + appendMessage+punctuationMark;
					}
				}
				return "";
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
		/// Overloaded method. It accepts ScreenID,MessageID,User Message as Prefix ,User Message as Suffix.
		/// </summary>
		/// <param name="screenID">ScreenID of the screen whose message has to be fetched</param>
		/// <param name="messageID">MessageID of the message that has to be fetched. 
		/// If it does not exist in the screen having the passed ScreenID, then it will be fetched from the general messages.</param>
		/// <param name="prefixMessage">User message that has to be prefixed to the fetched message.</param>
		/// <param name="suffixMessage">User message that has to be suffixed to the fetched message.</param>
		/// <returns>Concatenated message as a string object</returns>
		private static  string GetMessage(string screenID,string messageID, string prefixMessage, string suffixMessage)
		{
			try
			{
                docMessages = getdocMessages();
                // Traverse to the node having the attribute screenid = screenID
				XmlNode root = docMessages.FirstChild;
				bool screenIDFound=false;
				string returnMessage="";
                XmlNode nodScreen = root.SelectSingleNode("Culture[@Code='" + LANG_CULTURE + "']/screen[@screenid='" + screenID + "']");
				if ( nodScreen != null)
				{
					screenIDFound =true;	
					XmlNodeList nodMessages = nodScreen.ChildNodes;
					if(nodMessages.Count==0)
					{
						/* If this screenid node exists and the message id does not exist
							* then extract the message from the screenId="G"
							*/		
						returnMessage = FetchGeneralMessage(messageID);
						return prefixMessage+" " +returnMessage + " " + suffixMessage;
					}
					else
					{
						/* If this screenid node exists and the contains message nodes 
							* then extract the message node having messageID= messageID parameter
							*/		
						bool messageIdFound=false;
						XmlNode nodMessage = nodScreen.SelectSingleNode("message[@messageid='"+messageID +"']");
						if  (nodMessage != null)
						{
							messageIdFound=true;
							returnMessage = nodMessage.InnerText;
							return prefixMessage+" " +returnMessage + " " + suffixMessage;
						}
						if (!messageIdFound)
						{
							/* If this screenid node exists and the message id does not exist
							* then get the message from the general section */
							returnMessage = FetchGeneralMessage(messageID);
							return prefixMessage+" " +returnMessage + " " + suffixMessage;
						}
						
					}
				}
				else
				{
					/* If this screenid node doesnt exist
					 * then get the message from the general section */
					returnMessage = FetchGeneralMessage(messageID);
					return prefixMessage+" " +returnMessage + " " + suffixMessage;
				}
				if (!screenIDFound )
				{
					returnMessage = FetchGeneralMessage(messageID);
					return prefixMessage+" " +returnMessage + " " + suffixMessage;
				}
				return "";
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
		/// Overloaded method. It accepts ScreenID,MessageID,User Message as Prefix ,User Message as Suffix.
		/// </summary>
		/// <param name="screenID">ScreenID of the screen whose message has to be fetched</param>
		/// <param name="messageID">MessageID of the message that has to be fetched. 
		/// If it does not exist in the screen having the passed ScreenID, then it will be fetched from the general messages.</param>
		/// <param name="prefixMessage">User message that has to be prefixed to the fetched message.</param>
		/// <param name="suffixMessage">User message that has to be suffixed to the fetched message.</param>
		/// <param name="punctuationMark">Punctuation mark that has to be appended in the end of the message. Can be '.','?','!' etc</param>
		/// <returns>Concatenated message as a string object</returns>
		private static  string GetMessage(string screenID,string messageID, string prefixMessage, string suffixMessage, string punctuationMark)
		{
			try
			{
                docMessages = getdocMessages();
                // Traverse to the node having the attribute screenid = screenID
				XmlNode root = docMessages.FirstChild;
				bool screenIDFound=false;
				string returnMessage="";
                XmlNode nodScreen = root.SelectSingleNode("Culture[@Code='" + LANG_CULTURE + "']/screen[@screenid='" + screenID + "']");
				if ( nodScreen != null)
				{
					screenIDFound =true;	
					XmlNodeList nodMessages = nodScreen.ChildNodes;
					if(nodMessages.Count==0)
					{
						/* If this screenid node exists and the message id does not exist
							* then extract the message from the screenId="G"
							*/		
						returnMessage = FetchGeneralMessage(messageID);
						return prefixMessage+" " +returnMessage + " " + suffixMessage+punctuationMark;
					}
					else
					{
						/* If this screenid node exists and the contains message nodes 
							* then extract the message node having messageID= messageID parameter
							*/		
						bool messageIdFound=false;
						XmlNode nodMessage = nodScreen.SelectSingleNode("message[@messageid='"+messageID +"']");
						if  (nodMessage != null)
						{
							messageIdFound=true;
							returnMessage = nodMessage.InnerText;
							return prefixMessage+" " +returnMessage + " " + suffixMessage+punctuationMark;
						}
						if (!messageIdFound)
						{
							/* If this screenid node exists and the message id does not exist
							* then get the message from the general section */
							returnMessage = FetchGeneralMessage(messageID);
							return prefixMessage+" " +returnMessage + " " + suffixMessage+punctuationMark;
						}
						
					}
				}
				else
				{
					/* If this screenid node doesnt exist
					 * then get the message from the general section */
					returnMessage = FetchGeneralMessage(messageID);
					return prefixMessage+" " +returnMessage + " " + suffixMessage+punctuationMark;
				}
				if (!screenIDFound )
				{
					returnMessage = FetchGeneralMessage(messageID);
					return prefixMessage+" " +returnMessage + " " + suffixMessage+punctuationMark;
				}
				return "";
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
		/// It accepts MessageID and returns the string message against the message id from the general messages. 
		/// </summary>
		/// <param name="messageID">MessageID of the message that has to be fetched. 
		/// <returns>Message as a string object</returns>
		public  static string FetchGeneralMessage(string messageID)
		{
			try 
			{
                docMessages = getdocMessages();
                XmlNode root = docMessages.FirstChild;
				bool foundMessageID=false;
                XmlNode nodScreen = root.SelectSingleNode("Culture[@Code='" + LANG_CULTURE + "']/screen[@screenid='" + GENERALMESSAGESSCREENID + "']");
				if (nodScreen != null)																								  
				{
                    // Added by Charles on 19-May-2010 for Multilingual Support 
                    /* date format assign according to user preferences 
                     * changed by Lalit Feb 22,2011
                     if (messageID.Trim() == "22" && (new cmsbase()).GetLanguageID().Trim() != "1")//Date Format Message
                    {
                        messageID = messageID.Trim() + "." + (new cmsbase()).GetLanguageCode().Trim();                        
                    }//Added till here
                     */
					XmlNode nodMessage = nodScreen.SelectSingleNode("message[@messageid='"+ messageID +"']");
					if (nodMessage !=null)
					{
						foundMessageID=true;
						return nodMessage.InnerText;							
					}
				}
				if (!foundMessageID)
				{
					throw(new Exception("Message node against the message id : "+ messageID +" not found in the CustomizedMessages XML."));
				}
				
				return "";
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally{}
		
		}

		/// <summary>
		/// It accepts MessageID and returns the string message against the message id from the general messages. 
		/// </summary>
		/// <param name="messageID">MessageID of the message that has to be fetched. 
		/// <param name="punctuationMark">Punctuation mark that has to be appended in the end of the message. Can be '.','?','!' etc</param>
		/// <returns>Message as a string object</returns>
		private static string FetchGeneralMessage(string messageID, string punctuationMark)
		{
			try 
			{
                docMessages = getdocMessages();
                XmlNode root = docMessages.FirstChild;
				bool foundMessageID=false;
                XmlNode nodScreen = root.SelectSingleNode("Culture[@Code='" + LANG_CULTURE + "']/screen[@screenid='" + GENERALMESSAGESSCREENID + "']");
				if (nodScreen != null)																								  
				{
					XmlNode nodMessage = nodScreen.SelectSingleNode("message[@messageid='"+ messageID +"']");
					if (nodMessage !=null)
					{
						foundMessageID=true;
						return nodMessage.InnerText+punctuationMark;							
					}
				}   
				if (!foundMessageID)
				{
					throw(new Exception("Message node against the message id : "+ messageID +" not found in the CustomizedMessages XML."));
				}
				
				return "";
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally{}
		
		}

		# endregion
	}
}