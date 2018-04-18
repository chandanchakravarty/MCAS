using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Globalization;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Xml;
using MCAS.Entity;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
namespace MCAS.Web.Objects.CommonHelper
{
    public class CommonUtilities
    {
        public class CommonType
        {
            public int intID { get; set; }
            public String Id { get; set; }
            public String Text { get; set; }
            public int? PartyId { get; set; }

            public static string GetConnString(MCASEntities ent)
            {
                EntityConnection ec = (EntityConnection)ent.Connection;
                SqlConnection sc = (SqlConnection)ec.StoreConnection;
                return sc.ConnectionString;
            }
            public static string GetValue(string strXml, string strNode)
            {
                string strValue = "";
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(strXml);
                XmlNode xNode = xDoc.SelectSingleNode(strNode);
                if (xNode != null)
                    strValue = xNode.InnerText;
                xDoc = null;
                return strValue;
            }

        }

        #region publishing application Exceptions
        public sealed class ExceptionManager
        {
            private static string mCnnString;
            public static string ExceptionConString
            {
                get
                {
                    return mCnnString;
                }
                set
                {
                    mCnnString = value;
                }
            }
            public static void PublishException(Exception ex, int userId)
            {
                PublishException(ex, null, 0, "", userId);
            }
            public static void PublishException(Exception ex, int entityId, string entityType, int userId)
            {
                PublishException(ex, null, entityId, entityType, userId);
            }
            public static void PublishException(Exception ex, NameValueCollection additionalInfo, int entityId, string entityType, int userId)
            {
                int accidentClaim_id, policy_id, policy_version_id, claim_id, entity_id, user_id;
                accidentClaim_id = policy_id = policy_version_id = claim_id = entity_id = user_id = 0;
                string system_id = "MCAS", entity_type = "";
                if (additionalInfo != null)
                {
                    accidentClaim_id = additionalInfo.Get("accidentClaim_id") != null ? int.Parse(additionalInfo.Get("accidentClaim_id")) : 0;
                    policy_id = additionalInfo.Get("policy_id") != null ? int.Parse(additionalInfo.Get("policy_id")) : 0;
                    claim_id = additionalInfo.Get("claim_id") != null ? int.Parse(additionalInfo.Get("claim_id")) : 0;
                    entity_id = additionalInfo.Get("entity_id") != null ? int.Parse(additionalInfo.Get("entity_id")) : 0;
                    entity_type = additionalInfo.Get("entity_type") != null ? additionalInfo.Get("entity_type").ToString() : "";
                    policy_version_id = additionalInfo.Get("policy_version_id") != null ? int.Parse(additionalInfo.Get("policy_version_id")) : 0;
                }
                user_id = userId;
                if (entityId != 0) entity_id = entityId;
                if (entityType != "") entity_type = entityType;
                string strConStr = System.Configuration.ConfigurationManager.AppSettings["ExceptionConString"] != null ? System.Configuration.ConfigurationManager.AppSettings["ExceptionConString"].ToString() : "";
                if (strConStr == "") throw (new Exception("Connection String not configured for Exception Publisher."));
                MCAS.ExceptionPublisher.ExceptionManagement.ExceptionManager.ExceptionConString = strConStr;
                MCAS.ExceptionPublisher.ExceptionManagement.ExceptionManager.ExceptionProcName = "Proc_InsertExceptionLog";
                MCAS.ExceptionPublisher.ExceptionManagement.ExceptionManager.Parameters.Clear();
                MCAS.ExceptionPublisher.ExceptionManagement.ExceptionManager.Parameters.Add("@accidentClaim_id", accidentClaim_id);
                MCAS.ExceptionPublisher.ExceptionManagement.ExceptionManager.Parameters.Add("@policy_id", policy_id);
                MCAS.ExceptionPublisher.ExceptionManagement.ExceptionManager.Parameters.Add("@policy_version_id", policy_version_id);
                MCAS.ExceptionPublisher.ExceptionManagement.ExceptionManager.Parameters.Add("@claim_id", claim_id);
                MCAS.ExceptionPublisher.ExceptionManagement.ExceptionManager.Parameters.Add("@entity_id", entity_id);
                MCAS.ExceptionPublisher.ExceptionManagement.ExceptionManager.Parameters.Add("@entity_type", entity_type);
                MCAS.ExceptionPublisher.ExceptionManagement.ExceptionManager.Parameters.Add("@system_id", system_id);
                MCAS.ExceptionPublisher.ExceptionManagement.ExceptionManager.Parameters.Add("@user_id", user_id);
                MCAS.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }
        #endregion

        public class IsEMail
        {
            public List<string> ResultInfo { get; set; }

            /**
             * Checks that an email address conforms to RFCs 5321, 5322 and others. With
             * verbose information.
             * 
             * @param email
             *            The email address to check
             * @param checkDNS
             *            If true then a DNS check for A and MX records will be made
             * @return Result-Object of the email analysis.
             * @throws DNSLookupException
             *             Is thrown if an internal error in the DNS lookup appeared.
             */
            public bool IsEmailValid(String email)
            {
                ResultInfo = new List<string>();
                if (email == null)
                {
                    email = string.Empty;
                }

                // Check that $email is a valid address. Read the following RFCs to
                // understand the constraints:
                // the upper limit on address lengths should normally be considered to
                // be 254
                // NB My erratum has now been verified by the IETF so the correct answer
                // is 254
                //
                // The maximum total length of a reverse-path or forward-path is 256
                // characters (including the punctuation and element separators)
                // NB There is a mandatory 2-character wrapper round the actual address
                int emailLength = email.Length;
                // revision 1.17: Max length reduced to 254 (see above)
                if (emailLength > 254)
                {
                    this.ResultInfo.Add(@"
                    Email is too long.
                    The maximum total length of a reverse-path or forward-path is 256
                    characters (including the punctuation and element separators)
                    ");
                    return false;
                }

                // Contemporary email addresses consist of a "local part" separated from
                // a "domain part" (a fully-qualified domain name) by an at-sign ("@").
                int atIndex = email.LastIndexOf('@');

                if (atIndex == -1)
                {
                    this.ResultInfo.Add(@"
                    Email is too long.
                    Contemporary email addresses consist of a ""local part"" separated from
                    a ""domain part"" (a fully-qualified domain name) by an at-sign (""@"").
                    ");
                    return false;
                }
                if (atIndex == 0)
                {
                    this.ResultInfo.Add(@"
                    Email is too long.
                    Contemporary email addresses consist of a ""local part"" separated from
                    a ""domain part"" (a fully-qualified domain name) by an at-sign (""@"").
                    ");
                    return false;
                }
                if (atIndex == emailLength - 1)
                {
                    this.ResultInfo.Add(@"
                Email is too long.
                Contemporary email addresses consist of a ""local part"" separated from
                a ""domain part"" (a fully-qualified domain name) by an at-sign (""@"").
                ");
                    return false;
                }

                // Sanitize comments
                // - remove nested comments, quotes and dots in comments
                // - remove parentheses and dots from quoted strings
                int braceDepth = 0;
                bool inQuote = false;
                bool escapeThisChar = false;

                for (int i = 0; i < emailLength; ++i)
                {
                    char charX = email.ToCharArray()[i];
                    bool replaceChar = false;

                    if (charX == '\\')
                    {
                        escapeThisChar = !escapeThisChar; // Escape the next character?
                    }
                    else
                    {
                        switch (charX)
                        {
                            case '(':
                                if (escapeThisChar)
                                {
                                    replaceChar = true;
                                }
                                else
                                {
                                    if (inQuote)
                                    {
                                        replaceChar = true;
                                    }
                                    else
                                    {
                                        if (braceDepth++ > 0)
                                        {
                                            replaceChar = true; // Increment brace depth
                                        }
                                    }
                                }

                                break;
                            case '?':
                                if (escapeThisChar)
                                {
                                    replaceChar = true;
                                }
                                else
                                {
                                    if (inQuote)
                                    {
                                        replaceChar = true;
                                    }
                                    else
                                    {
                                        if (braceDepth++ > 0)
                                        {
                                            replaceChar = true; // Increment brace depth
                                        }
                                    }
                                }

                                break;
                            case '<':
                                if (escapeThisChar)
                                {
                                    replaceChar = true;
                                }
                                else
                                {
                                    if (inQuote)
                                    {
                                        replaceChar = true;
                                    }
                                    else
                                    {
                                        if (braceDepth++ > 0)
                                        {
                                            replaceChar = true; // Increment brace depth
                                        }
                                    }
                                }

                                break;
                            case '>':
                                if (escapeThisChar)
                                {
                                    replaceChar = true;
                                }
                                else
                                {
                                    if (inQuote)
                                    {
                                        replaceChar = true;
                                    }
                                    else
                                    {
                                        if (braceDepth++ > 0)
                                        {
                                            replaceChar = true; // Increment brace depth
                                        }
                                    }
                                }

                                break;
                            case ')':
                                if (escapeThisChar)
                                {
                                    replaceChar = true;
                                }
                                else
                                {
                                    if (inQuote)
                                    {
                                        replaceChar = true;
                                    }
                                    else
                                    {
                                        if (--braceDepth > 0)
                                            replaceChar = true; // Decrement brace depth
                                        if (braceDepth < 0)
                                        {
                                            braceDepth = 0;
                                        }
                                    }
                                }

                                break;
                            case '"':
                                if (escapeThisChar)
                                {
                                    replaceChar = true;
                                }
                                else
                                {
                                    if (braceDepth == 0)
                                    {
                                        // Are we inside a quoted string?
                                        inQuote = !inQuote;
                                    }
                                    else
                                    {
                                        replaceChar = true;
                                    }
                                }

                                break;
                            case '.': // Dots don't help us either
                                if (escapeThisChar)
                                {
                                    replaceChar = true;
                                }
                                else
                                {
                                    if (braceDepth > 0)
                                        replaceChar = true;
                                }

                                break;
                        }

                        escapeThisChar = false;
                        if (replaceChar)
                        {
                            // Replace the offending character with something harmless
                            // revision 1.12: Line above replaced because PHPLint
                            // doesn't like that syntax
                            email = replaceCharAt(email, i, 'x');
                        }

                    }
                }

                String localPart = email.Substring(0, atIndex);
                String domain = email.Substring(atIndex + 1);
                // Folding white space
                String FWS = "(?:(?:(?:[ \\t]*(?:\\r\\n))?[ \\t]+)|(?:[ \\t]+(?:(?:\\r\\n)[ \\t]+)*))";

                // Let's check the local part for RFC compliance...
                //
                // local-part = dot-atom / quoted-string / obs-local-part
                // obs-local-part = word *("." word)
                //
                // Problem: need to distinguish between "first.last" and "first"."last"
                // (i.e. one element or two). And I suck at regular expressions.

                Regex regex = new Regex("(?m)\\.(?=(?:[^\\\"]*\\\"[^\\\"]*\\\")*(?![^\\\"]*\\\"))");
                String[] dotArray = regex.Split(localPart);
                int partLength = 0;

                #region foreach block
                foreach (String element in dotArray)
                {
                    string working_element = element; // for use in our for loop, can't work on a foreach target SCO-04152011

                    // Remove any leading or trailing FWS
                    Regex repRegex = new Regex("^" + FWS + "|" + FWS + "$");
                    String new_element = repRegex.Replace(working_element, string.Empty);

                    if (!working_element.Equals(new_element))
                    {
                        // FWS is unlikely in the real world
                        this.ResultInfo.Add(@"
                        Folding White Space

		                local-part = dot-atom / quoted-string / obs-local-part
		                obs-local-part = word *(""."" word)
                    ");
                    }
                    working_element = new_element; // version 2.3: Warning condition added

                    int elementLength = new_element.Length;

                    if (elementLength == 0)
                    {
                        // Can't have empty element (consecutive dots or
                        // dots at the start or end)
                        this.ResultInfo.Add(@"
				        Can't have empty element (consecutive dots or
				        dots at the start or end)
                    ");
                        return false;
                    }
                    // revision 1.15: Speed up the test and get rid of
                    // "uninitialized string offset" notices from PHP

                    // We need to remove any valid comments (i.e. those at the start or
                    // end of the element)
                    if (working_element.Substring(0) == "(")
                    {
                        // Comments are unlikely in the real world
                        // return_status = IsEMailResult.ISEMAIL_COMMENTS;

                        // version 2.0: Warning condition added
                        int indexBrace = working_element.IndexOf(")");
                        if (indexBrace != -1)
                        {
                            Regex pregMatch = new Regex("(?<!\\\\)[\\(\\)]");
                            if (pregMatch.Matches(working_element.Substring(1, indexBrace - 1)).Count > 0)
                            {
                                // Illegal characters in comment
                                this.ResultInfo.Add(@"
				            Illegal characters in comment
                        ");
                                return false;
                            }
                            working_element = working_element.Substring(indexBrace + 1, elementLength - indexBrace - 1);
                            elementLength = working_element.Length;
                        }
                    }

                    if (working_element.Substring(elementLength - 1) == ")")
                    {
                        // Comments are unlikely in the real world
                        // return_status = IsEMailResult.ISEMAIL_COMMENTS;

                        // version 2.0: Warning condition added
                        int indexBrace = working_element.LastIndexOf("(");
                        if (indexBrace != -1)
                        {
                            Regex pregMatch = new Regex("(?<!\\\\)(?:[\\(\\)])");
                            if (pregMatch.Matches(working_element.Substring(indexBrace + 1, elementLength - indexBrace - 2)).Count > 0)
                            {
                                // Illegal characters in comment						
                                this.ResultInfo.Add(@"
				            Illegal characters in comment
                        ");
                                return false;
                            }
                            working_element = working_element.Substring(0, indexBrace);
                            elementLength = working_element.Length;
                        }
                    }

                    // Remove any remaining leading or trailing FWS around the element
                    // (having removed any comments)
                    Regex fwsRegex = new Regex("^" + FWS + "|" + FWS + "$");

                    new_element = fwsRegex.Replace(working_element, string.Empty);

                    //// FWS is unlikely in the real world
                    //if (!working_element.equals(new_element))
                    //    return_status = IsEMailResult.ISEMAIL_FWS;

                    working_element = new_element;
                    // version 2.0: Warning condition added

                    // What's left counts towards the maximum length for this part
                    if (partLength > 0)
                        partLength++; // for the dot
                    partLength += working_element.Length;

                    // Each dot-delimited component can be an atom or a quoted string
                    // (because of the obs-local-part provision)

                    Regex quotRegex = new Regex("(?s)^\"(?:.)*\"$");
                    if (quotRegex.Matches(working_element).Count > 0)
                    {
                        // Quoted-string tests:
                        // Quoted string is unlikely in the real world
                        // return_status = IsEMailResult.ISEMAIL_QUOTEDSTRING;
                        // version 2.0: Warning condition added
                        // Remove any FWS
                        // A warning condition, but we've already raised
                        // ISEMAIL_QUOTEDSTRING
                        Regex newRepRegex = new Regex("(?<!\\\\)" + FWS);
                        working_element = newRepRegex.Replace(working_element, string.Empty);

                        // My regular expression skills aren't up to distinguishing
                        // between \" \\" \\\" \\\\" etc.
                        // So remove all \\ from the string first...
                        Regex slashRegex = new Regex("\\\\\\\\");
                        working_element = slashRegex.Replace(working_element, string.Empty);

                        Regex quot2Regex = new Regex("(?<!\\\\|^)[\"\\r\\n\\x00](?!$)|\\\\\"$|\"\"");
                        if (quot2Regex.Matches(working_element).Count > 0)
                        {
                            // ", CR, LF and NUL must be escaped
                            // version 2.0: allow ""@example.com because it's
                            // technically valid					
                            this.ResultInfo.Add(@"
				            "", CR, LF and NUL must be escaped
                        ");
                            return false;
                        }
                    }
                    else
                    {
                        // Unquoted string tests:
                        //
                        // Period (".") may...appear, but may not be used to start or
                        // end the
                        // local part, nor may two or more consecutive periods appear.
                        //
                        // A zero-length element implies a period at the beginning or
                        // end of the
                        // local part, or two periods together. Either way it's not
                        // allowed.
                        if (string.IsNullOrEmpty(working_element))
                        {
                            // Dots in wrong place
                            this.ResultInfo.Add(@"
				        A zero-length element implies a period at the beginning or
				        end of the local part, or two periods together. Either way it's not
				        allowed.
                    ");
                            return false;
                        }

                        // Any ASCII graphic (printing) character other than the
                        // at-sign ("@"), backslash, double quote, comma, or square
                        // brackets may
                        // appear without quoting. If any of that list of excluded
                        // characters
                        // are to appear, they must be quoted
                        //
                        // Any excluded characters? i.e. 0x00-0x20, (, ), <, >, [, ], :,
                        // ;, @, \, comma, period, "
                        Regex quot3Regex = new Regex("[\\x00-\\x20\\(\\)<>\\[\\]:;@\\\\,\\.\"]");
                        if (quot3Regex.Matches(working_element).Count > 0)
                        {
                            // These characters must be in a quoted string
                            this.ResultInfo.Add(@"
				         Any ASCII graphic (printing) character other than the
				         at-sign (""@""), backslash, double quote, comma, or square
				         brackets may appear without quoting. If any of that list of excluded
				         characters are to appear, they must be quoted
                        ");
                            return false;
                        }
                        //Regex quot4Regex = new Regex("^\\w+");
                        //if (quot4Regex.Matches(working_element).Count == 0) 
                        //{
                        //    // First character is an odd one
                        //    return_status = IsEMailResult.ISEMAIL_UNLIKELYINITIAL;
                        //}
                    }
                }
                #endregion end foreach

                if (partLength > 64)
                {
                    // Local part must be 64 characters or less
                    this.ResultInfo.Add(@"
				Local part must be 64 characters or less
            ");
                    return false;
                }

                // Now let's check the domain part...

                // The domain name can also be replaced by an IP address in square
                // brackets
                //
                // IPv4 is the default format for address literals. Alternative formats
                // can
                // be defined. At the time of writing only IPv6 has been defined as an
                // alternative format. Non-IPv4 formats must be tagged to show what type
                // of address literal they are. The registry of current tags is here:
                // http://www.iana.org/assignments/address-literal-tags
                if (new Regex("^\\[(.)+]$").Matches(domain).Count == 1)
                {
                    //// It's an address-literal
                    //// Quoted string is unlikely in the real world
                    //return_status = IsEMailResult.ISEMAIL_ADDRESSLITERAL;

                    // version 2.0: Warning condition added
                    String addressLiteral = domain.Substring(1, domain.Length - 2);

                    String IPv6;
                    int groupMax = 8;
                    // revision 2.1: new IPv6 testing strategy

                    String colon = ":"; // Revision 2.7: Daniel Marschall's new
                    // IPv6 testing strategy
                    String double_colon = "::";

                    String IPv6tag = "IPv6:";

                    // Extract IPv4 part from the end of the address-literal (if there
                    // is one)
                    Regex splitRegex = new Regex("\\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");

                    MatchCollection matchesIP1 = splitRegex.Matches(addressLiteral);
                    if (matchesIP1.Count > 0)
                    {
                        int index = addressLiteral.LastIndexOf(matchesIP1[0].Value);

                        if (index == 0)
                        {
                            // Nothing there except a valid IPv4 address, so...
                            return true;
                            // version 2.0: return warning if one is set
                        }
                        else
                        {
                            // - // Assume it's an attempt at a mixed address (IPv6 +
                            // IPv4)
                            // - if ($addressLiteral[$index - 1] !== ':') return
                            // IsEMailResult.ISEMAIL_IPV4BADPREFIX; // Character
                            // preceding IPv4 address must be ':'
                            // revision 2.1: new IPv6 testing strategy
                            if (!addressLiteral.Substring(0, 5).Equals(IPv6tag))
                            {
                                // RFC5321 section 4.1.3		
                                this.ResultInfo.Add(@"
                            Character preceding IPv4 address must be ':'
						    RFC5321 section 4.1.3	
                        ");
                                return false;
                            }
                            // -
                            // - $IPv6 = substr($addressLiteral, 5, ($index === 7) ? 2 :
                            // $index - 6);
                            // - $groupMax = 6;
                            // revision 2.1: new IPv6 testing strategy
                            IPv6 = addressLiteral.Substring(5, index - 5) + "0000:0000"; // Convert IPv4 part to IPv6 format
                        }
                    }
                    else
                    {
                        // It must be an attempt at pure IPv6
                        if (!addressLiteral.Substring(0, 5).Equals(IPv6tag))
                        {
                            // RFC5321 section 4.1.3
                            this.ResultInfo.Add(@"
                            Invalid IPV6 address
						    RFC5321 section 4.1.3	
                        ");
                            return false;
                        }
                        IPv6 = addressLiteral.Substring(5);
                        // - $groupMax = 8;
                        // revision 2.1: new IPv6 testing strategy
                    }

                    // Revision 2.7: Daniel Marschall's new IPv6 testing strategy
                    Regex split2Regex = new Regex(colon);
                    string[] matchesIP = split2Regex.Split(IPv6);
                    int groupCount = matchesIP.Length;
                    int currIndex = IPv6.IndexOf(double_colon);

                    if (currIndex == -1)
                    {
                        // We need exactly the right number of groups
                        if (groupCount != groupMax)
                        {
                            // RFC5321 section 4.1.3
                            this.ResultInfo.Add(@"
                            Invalid IPV6 groupcount
						    RFC5321 section 4.1.3	
                        ");
                            return false;
                        }
                    }
                    else
                    {
                        if (currIndex != IPv6.LastIndexOf(double_colon))
                        {
                            // More than one '::'
                            this.ResultInfo.Add(@"
                            IPV6 double double colon present
						    RFC5321 section 4.1.3	
                        ");
                            return false;
                        }
                        if ((currIndex == 0) || (currIndex == (IPv6.Length - 2)))
                        {
                            groupMax++; // RFC 4291 allows :: at the start or end of an
                        }
                        // address with 7 other groups in addition
                        if (groupCount > groupMax)
                        {
                            // Too many IPv6 groups in address
                            this.ResultInfo.Add(@"
                            Too many groups in section
						    RFC5321 section 4.1.3	
                        ");
                            return false;
                        }
                        if (groupCount == groupMax)
                        {
                            // Eliding a single group with :: is deprecated by RFCs 5321 & 5952
                            // & 5952
                            this.ResultInfo.Add(@"Eliding a single group with :: is deprecated by RFCs 5321 & 5952");
                        }
                    }

                    // Check for single : at start and end of address
                    // Revision 2.7: Daniel Marschall's new IPv6 testing strategy
                    if (IPv6.StartsWith(colon) && (!IPv6.StartsWith(double_colon)))
                    {
                        // Address starts with a single colon
                        this.ResultInfo.Add(@"
                    IPV6 must start with a single colon
				    RFC5321 section 4.1.3	
                        ");
                        return false;
                    }
                    if (IPv6.EndsWith(colon) && (!IPv6.EndsWith(double_colon)))
                    {
                        // Address ends with a single colon
                        this.ResultInfo.Add(@"
                    IPV6 must end with a single colon
				    RFC5321 section 4.1.3	
                        ");
                        return false;
                    }

                    // Check for unmatched characters
                    foreach (String s in matchesIP)
                    {
                        Regex goodStuff = new Regex("^[0-9A-Fa-f]{0,4}$");
                        if (goodStuff.Matches(s).Count == 0)
                        {
                            this.ResultInfo.Add(@"
                    IPV6 address contains bad characters
				    RFC5321 section 4.1.3	
                        ");
                            return false;
                        }
                    }

                    // It's a valid IPv6 address, so...
                    return true;
                    // revision 2.1: bug fix: now correctly return warning status

                }
                else
                {
                    // It's a domain name...

                    // The syntax of a legal Internet host name was specified in RFC-952
                    // One aspect of host name syntax is hereby changed: the
                    // restriction on the first character is relaxed to allow either a
                    // letter or a digit.
                    // (http://tools.ietf.org/html/rfc1123#section-2.1)
                    //
                    // NB RFC 1123 updates RFC 1035, but this is not currently apparent
                    // from reading RFC 1035.
                    //
                    // Most common applications, including email and the Web, will
                    // generally not
                    // permit...escaped strings

                    //
                    // the better strategy has now become to make the
                    // "at least one period" test,
                    // to verify LDH conformance (including verification that the
                    // apparent TLD name
                    // is not all-numeric)

                    //
                    // Characters outside the set of alphabetic characters, digits, and
                    // hyphen MUST NOT appear in domain name
                    // labels for SMTP clients or servers

                    //
                    // RFC5321 precludes the use of a trailing dot in a domain name for
                    // SMTP purposes


                    Regex split2Regex = new Regex("(?m)\\.(?=(?:[^\\\"]*\\\"[^\\\"]*\\\")*(?![^\\\"]*\\\"))");
                    dotArray = split2Regex.Split(domain);
                    partLength = 0;
                    // Since we use 'element' after the foreach
                    // loop let's make sure it has a value
                    String lastElement = "";
                    // revision 1.13: Line above added because PHPLint now checks for
                    // Definitely Assigned Variables

                    string[] arr = new string[] { "com", "net", "org", "in" };
                    string[] str = new string[] { "(", ")" };
                    if (dotArray.Length == 1)
                    {
                        this.ResultInfo.Add(@"The mail host probably isn't a TLD");
                        return false;
                    }
                    if (dotArray.Contains("web"))
                    {
                        return false;
                    }
                    if ((dotArray.Length == 4) && (dotArray[0].Length > 3 || dotArray[1].Length > 3 || dotArray[2].Length > 3 || dotArray[3].Length > 3))
                    {
                        // if(dotArray[0].Length>3 ||dotArray[1].Length>3||dotArray[2].Length>3 || dotArray[3].Length>3)
                        this.ResultInfo.Add(@"The mail host probably isn't a TLD");
                        return false;
                    }

                    //if (!arr.Contains(dotArray[dotArray.Length-1])) {
                    //    return false;
                    //}

                    //if (!str.Contains(dotArray[dotArray.Length - 1]))  {
                    //    return false;
                    //}

                    //if (!arr.Contains(dotArray[dotArray.Length - 1])) {
                    //    return false;
                    //}

                    // version 2.0: downgraded to a warning

                    foreach (String element in dotArray)
                    {
                        // sanjay code

                        string working_element = element;
                        lastElement = element;
                        // Remove any leading or trailing FWS
                        Regex newReg = new Regex("^" + FWS + "|" + FWS + "$");
                        String new_element = newReg.Replace(working_element, string.Empty);


                        if (!element.Equals(new_element))
                        {
                            this.ResultInfo.Add(@"FWS is unlikely in the real world");
                        }
                        working_element = new_element;
                        // version 2.0: Warning condition added
                        int elementLength = working_element.Length;

                        // Each dot-delimited component must be of type atext
                        // A zero-length element implies a period at the beginning or
                        // end of the
                        // local part, or two periods together. Either way it's not
                        // allowed.
                        if (elementLength == 0)
                        {
                            // Dots in wrong place
                            this.ResultInfo.Add(@"
				         Each dot-delimited component must be of type atext
				         A zero-length element implies a period at the beginning or
				         end of the
				         local part, or two periods together. Either way it's not
				         allowed.
                        ");
                            return false;
                        }
                        // revision 1.15: Speed up the test and get rid of
                        // "uninitialized string offset" notices from PHP

                        // Then we need to remove all valid comments (i.e. those at the
                        // start or end of the element
                        if (working_element.Substring(0, 1) == "(")
                        {
                            this.ResultInfo.Add(@"Comments are unlikely in the real world");


                            // version 2.0: Warning condition added
                            int indexBrace = working_element.IndexOf(")");

                            if (indexBrace != -1)
                            {
                                Regex comments1Regex = new Regex("(?<!\\\\)[\\(\\)]");
                                if (comments1Regex.Matches(working_element.Substring(1, indexBrace - 1)).Count > 0)
                                {
                                    // revision 1.17: Fixed name of constant (also
                                    // spotted by turboflash - thanks!)
                                    // Illegal characters in comment
                                    this.ResultInfo.Add(@"
                            Illegal characters in comment
                        ");
                                    return false;
                                }
                                working_element = working_element.Substring(indexBrace + 1, elementLength - indexBrace - 1);
                                elementLength = working_element.Length;
                            }
                        }

                        if (working_element.Substring(elementLength - 1, 1) == ")")
                        {
                            // Comments are unlikely in the real world
                            // return_status = IsEMailResult.ISEMAIL_COMMENTS;

                            // version 2.0: Warning condition added
                            int indexBrace = working_element.LastIndexOf("(");
                            if (indexBrace != -1)
                            {

                                Regex commentRegex = new Regex("(?<!\\\\)(?:[\\(\\)])");
                                if (commentRegex.Matches(working_element.Substring(indexBrace + 1, elementLength - indexBrace - 2)).Count > 0)
                                {
                                    // revision 1.17: Fixed name of constant (also
                                    // spotted by turboflash - thanks!)
                                    // Illegal characters in comment
                                    this.ResultInfo.Add(@"
                            Illegal characters in comment
                        ");
                                    return false;
                                }


                                working_element = working_element.Substring(0, indexBrace);
                                elementLength = working_element.Length;
                                return false;
                            }
                        }

                        // Remove any leading or trailing FWS around the element (inside
                        // any comments)
                        Regex repRegex = new Regex("^" + FWS + "|" + FWS + "$");
                        new_element = repRegex.Replace(working_element, string.Empty);
                        //if (!element.equals(new_element)) 
                        //{
                        //    // FWS is unlikely in the real world
                        //    return_status = IsEMailResult.ISEMAIL_FWS;
                        //}
                        working_element = new_element;
                        // version 2.0: Warning condition added

                        // What's left counts towards the maximum length for this part
                        if (partLength > 0)
                        {
                            partLength++; // for the dot
                        }

                        partLength += working_element.Length;

                        // The DNS defines domain name syntax very generally -- a
                        // string of labels each containing up to 63 8-bit octets,
                        // separated by dots, and with a maximum total of 255
                        // octets.
                        if (elementLength > 63)
                        {
                            // Label must be 63 characters or less
                            this.ResultInfo.Add(@"
				         The DNS defines domain name syntax very generally -- a
				         string of labels each containing up to 63 8-bit octets,
				         separated by dots, and with a maximum total of 255
				         octets.
                        ");
                            return false;
                        }

                        // Any ASCII graphic (printing) character other than the
                        // at-sign ("@"), backslash, double quote, comma, or square
                        // brackets may
                        // appear without quoting. If any of that list of excluded
                        // characters
                        // are to appear, they must be quoted

                        //
                        // If the hyphen is used, it is not permitted to appear at
                        // either the beginning or end of a label.

                        //
                        // Any excluded characters? i.e. 0x00-0x20, (, ), <, >, [, ], :,
                        // ;, @, \, comma, period, "

                        Regex badChars = new Regex("[\\x00-\\x20\\(\\)()<>\\[\\]:;@\\\\,\\.\"]|^-|-$");
                        if (badChars.Matches(working_element).Count > 0)
                        {
                            // Illegal character in domain name
                            this.ResultInfo.Add(@"
                    Illegal character in domain name
                        ");
                            return false;
                        }
                    }

                    if (partLength > 255)
                    {
                        // Domain part must be 255 characters or less

                        this.ResultInfo.Add(@"
				    Domain part must be 255 characters or less
                        ");
                        return false;
                    }

                    Regex foo = new Regex("^[0-9]+$");
                    if (foo.Matches(lastElement).Count > 0)
                    {
                        this.ResultInfo.Add(@"TLD probably isn't all-numeric
                ");
                        // version 2.0: Downgraded to a warning
                    }
                }

                // Eliminate all other factors, and the one which remains must be the
                // truth. (Sherlock Holmes, The Sign of Four)
                return true;
            }

            /**
             * Replaces a char in a String
             * 
             * @param s
             *            The input string
             * @param pos
             *            The position of the char to be replaced
             * @param c
             *            The new char
             * @return The new String
             * @see Source: http://www.rgagnon.com/javadetails/java-0030.html
             */
            private static String replaceCharAt(String s, int pos, char c)
            {
                return s.Substring(0, pos) + c + s.Substring(pos + 1);
            }
        }        

        public string GenerateXMLForChangedColumns<T>(T OldSummary, T NewSummary, string[] checkList)
        {
            string strChangeXml = "";
            bool flag = false;
            foreach (var fld in NewSummary.GetType().GetProperties())
            {
                if (fld.PropertyType.Namespace == "System")
                {
                    string OldValue = string.Empty;
                    string NewValue = string.Empty;
                    if (OldSummary != null)
                        OldValue = Convert.ToString(OldSummary.GetType().GetProperty(fld.Name).GetValue(OldSummary, null)).Trim();
                    if (NewSummary != null)
                        NewValue = Convert.ToString(NewSummary.GetType().GetProperty(fld.Name).GetValue(NewSummary, null)).Trim();

                    if (OldValue == "0")
                        OldValue = "0.00";

                    if (NewValue == "0")
                        NewValue = "0.00";

                    if (NewValue == "0.00" && OldValue == "")
                    {
                        OldValue = "0.00";
                    }

                    if (OldValue != NewValue && (checkList != null && checkList.Contains(fld.Name)))
                    {
                        flag = true;
                    }


                    if (OldValue != NewValue)
                        strChangeXml += "<Column label=\"\" field=\"" + fld.Name + "\" OldValue=\"" + OldValue + "\" NewValue=\"" + NewValue + "\" />";
                }
            }
            if (!flag && checkList != null && checkList.Length > 0)
                strChangeXml = "";

            return strChangeXml;
        }

        public DataTable CreateDataTable<T>(T obj, string[] ignoreList)
        {
            var properties = obj.GetType().GetProperties().Where(t => t.PropertyType.Namespace == "System" && !ignoreList.Contains(t.Name)).ToList();
            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            object[] values = new object[properties.Count];
            for (int i = 0; i < properties.Count; i++)
            {
                values[i] = properties[i].GetValue(obj, null) ?? DBNull.Value;
            }
            dataTable.Rows.Add(values);

            return dataTable;
        }

        public DataTable CreateDataTable<T>(List<T> list, string[] ignoreList)
        {
            Type type = typeof(T);
            var properties = type.GetProperties().Where(t => t.PropertyType.Namespace == "System" && !ignoreList.Contains(t.Name)).ToArray();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity, null) ?? DBNull.Value;
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        #region BusCaptionGrid
        public class DatatableGrid
        {
            public List<string[]> data { get; set; }
            public int draw { get; set; }
            public int recordsTotal { get; set; }
            public int recordsFiltered { get; set; }
        }

        #endregion

        public static string GetuserDisplayName(string p)
        {
            MCASEntities _db = new MCASEntities();
            string result = string.Empty;
            try
            {
                result = (from l in _db.MNT_Users where l.UserId == p select l.UserDispName).FirstOrDefault() ?? p;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                _db.Dispose();
            }
            return result;
        }
    }
    public static class MultiDimensionalArrayExtensions
    {
        /// <summary>
        ///   Orders the two dimensional array by the provided key in the key selector.
        /// </summary>
        /// <typeparam name="T">The type of the source two-dimensional array.</typeparam>
        /// <param name="source">The source two-dimensional array.</param>
        /// <param name="keySelector">The selector to retrieve the column to sort on.</param>
        /// <returns>A new two dimensional array sorted on the key.</returns>
        public static T[,] OrderBy<T>(this T[,] source, Func<T[], T> keySelector)
        {
            return source.ConvertToSingleDimension().OrderBy(keySelector).ConvertToMultiDimensional();
        }
        /// <summary>
        ///   Orders the two dimensional array by the provided key in the key selector in descending order.
        /// </summary>
        /// <typeparam name="T">The type of the source two-dimensional array.</typeparam>
        /// <param name="source">The source two-dimensional array.</param>
        /// <param name="keySelector">The selector to retrieve the column to sort on.</param>
        /// <returns>A new two dimensional array sorted on the key.</returns>
        public static T[,] OrderByDescending<T>(this T[,] source, Func<T[], T> keySelector)
        {
            return source.ConvertToSingleDimension().
              OrderByDescending(keySelector).ConvertToMultiDimensional();
        }
        /// <summary>
        ///   Converts a two dimensional array to single dimensional array.
        /// </summary>
        /// <typeparam name="T">The type of the two dimensional array.</typeparam>
        /// <param name="source">The source two dimensional array.</param>
        /// <returns>The repackaged two dimensional array as a single dimension based on rows.</returns>
        private static IEnumerable<T[]> ConvertToSingleDimension<T>(this T[,] source)
        {
            T[] arRow;

            for (int row = 0; row < source.GetLength(0); ++row)
            {
                arRow = new T[source.GetLength(1)];

                for (int col = 0; col < source.GetLength(1); ++col)
                    arRow[col] = source[row, col];

                yield return arRow;
            }
        }
        /// <summary>
        ///   Converts a collection of rows from a two dimensional array back into a two dimensional array.
        /// </summary>
        /// <typeparam name="T">The type of the two dimensional array.</typeparam>
        /// <param name="source">The source collection of rows to convert.</param>
        /// <returns>The two dimensional array.</returns>
        private static T[,] ConvertToMultiDimensional<T>(this IEnumerable<T[]> source)
        {
            T[,] twoDimensional;
            T[][] arrayOfArray;
            int numberofColumns;

            arrayOfArray = source.ToArray();
            numberofColumns = (arrayOfArray.Length > 0) ? arrayOfArray[0].Length : 0;
            twoDimensional = new T[arrayOfArray.Length, numberofColumns];

            for (int row = 0; row < arrayOfArray.GetLength(0); ++row)
                for (int col = 0; col < numberofColumns; ++col)
                    twoDimensional[row, col] = arrayOfArray[row][col];

            return twoDimensional;
        }
    }
}
