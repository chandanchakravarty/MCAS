<!--	Check  for the Policy Level Product Mandatory Rules-->
<!--	Name : Charles Gomes-->
<!--	Date : 21 Apr,2010  -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:user="urn:user-namespace-here">
  <msxsl:script language="c#" implements-prefix="user">
    <![CDATA[ 
		int intCSS=1;
		public int ApplyColor(int colorvalue)
		{
			intCSS=colorvalue;			
			return intCSS;
		}	
    int  intIsValid = 1;
		public int AssignUnderwritter(int assignvalue)
		{
			intIsValid = intIsValid*assignvalue;
			return intIsValid;
		}
    int  intHeaderValid = 1;
		public int AssignHeader(int assignvalue)
		{
			intHeaderValid = intHeaderValid*assignvalue;
			return intHeaderValid;
		}
    public int SetHeader()
		{
			intHeaderValid = 1;
			return intHeaderValid;
		}
    
    int status=0;
    public int RemumsgSt()
		{			
			return status;
		}
    public int RemumsgSt(int intstatus)
		{	
			status= intstatus;
      return status;
		}
    
				
]]>
  </msxsl:script>
  <xsl:variable name="MyDoc_Path" select="INPUTXML/MESSAGE_FILE_PATH"></xsl:variable>
  <xsl:variable name="MyDoc" select="document($MyDoc_Path)"></xsl:variable>
  <xsl:template match="/">
    <html>
      <head>
        <xsl:variable name="myName" select="INPUTXML/CSSNUM/@CSSVALUE"></xsl:variable>
        <xsl:if test="user:ApplyColor($myName) = 0"></xsl:if>
        <xsl:choose>
          <xsl:when test="user:ApplyColor($myName) = 1">
            <LINK id="lk" href="/cms/cmsweb/css/css1.css" type="text/css" rel="stylesheet" />
          </xsl:when>
          <xsl:when test="user:ApplyColor($myName) = 2">
            <LINK href="/cms/cmsweb/css/css2.css" type="text/css" rel="stylesheet" />
          </xsl:when>
          <xsl:when test="user:ApplyColor($myName) = 3">
            <LINK href="/cms/cmsweb/css/css3.css" type="text/css" rel="stylesheet" />
          </xsl:when>
          <xsl:when test="user:ApplyColor($myName) = 4">
            <LINK href="/cms/cmsweb/css/css4.css" type="text/css" rel="stylesheet" />
          </xsl:when>
          <xsl:otherwise>
            <LINK href="/cms/cmsweb/css/css1.css" type="text/css" rel="stylesheet" />
          </xsl:otherwise>
        </xsl:choose>
      </head>
      <table border="2" align="center" width='90%'>
        <tr>
          <td class="pageheader" width="18%">
            <xsl:value-of select="$MyDoc//message[@messageid='91']"/>
          </td><!--Customer Name :-->
          <td class="midcolora" width="36%">
            <xsl:value-of select="INPUTXML/APPLICATIONS/APPLICATION/CUSTOMER_NAME" />
          </td>
          <td class="pageheader" width="18%">
            <xsl:value-of select="$MyDoc//message[@messageid='92']"/>
          </td>
          <!--Customer Type :-->
          <td class="midcolora" width="36%">
            <xsl:value-of select="INPUTXML/APPLICATIONS/APPLICATION/CUSTOMER_TYPE" />
          </td>
        </tr>
        <tr>
          <td class="pageheader">
            <xsl:value-of select="$MyDoc//message[@messageid='93']"/>
          </td><!--Address :-->
          <td class="midcolora">
            <xsl:value-of select="INPUTXML/APPLICATIONS/APPLICATION/ADDRESS" />
          </td>
          <td class="pageheader">
            <xsl:value-of select="$MyDoc//message[@messageid='94']"/>
          </td><!--Phone:-->
          <td class="midcolora">
            <xsl:value-of select="INPUTXML/APPLICATIONS/APPLICATION/CUSTOMER_HOME_PHONE" />
          </td>
        </tr>
        <tr>
          <xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=0">
            <td class="pageheader">
              <xsl:value-of select="$MyDoc//message[@messageid='95']"/>
            </td><!--App. No :-->
          </xsl:if>
          <xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=1">
            <td class="pageheader">
              <xsl:value-of select="$MyDoc//message[@messageid='103']"/>
            </td><!--Policy No :-->
          </xsl:if>
          <td class="midcolora">
            <xsl:value-of select="INPUTXML/APPLICATIONS/APPLICATION/APP_NO" />
          </td>
          <xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=0">
            <td class="pageheader">
              <xsl:value-of select="$MyDoc//message[@messageid='96']"/>
            </td><!--App. Version :-->
            <td class="midcolora">
              <xsl:value-of select="INPUTXML/APPLICATIONS/APPLICATION/APP_VERSION_NO" />
            </td>
          </xsl:if>
          <xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=1">
            <td class="pageheader">
              <xsl:value-of select="$MyDoc//message[@messageid='97']"/>
            </td><!--Policy Version :-->
            <td class="midcolora">
              <xsl:value-of select="INPUTXML/APPLICATIONS/APPLICATION/POLICY_DISP_VERSION" />
            </td>
          </xsl:if>
        </tr>
      </table>
      <table border="1" align="center" width='100%'>
        <tr>
          <xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=0">
            <td class="headereffectcenter" colspan="6">
              <xsl:value-of select="$MyDoc//message[@messageid='98']"/>
            </td><!--Application verification status-->
          </xsl:if>
          <xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=1">
            <td class="headereffectcenter" colspan="6">
              <xsl:value-of select="$MyDoc//message[@messageid='99']"/>
            </td><!--Policy verification status-->
          </xsl:if>
        </tr>
        <tr>
          <td class="pageheader">
            <xsl:value-of select="$MyDoc//message[@messageid='100']"/>
          </td><!--Please complete the following information:-->
        </tr>
        <!-- Call for different Messages -->
        <xsl:choose>
          <xsl:when test="1=1">
            <xsl:call-template name="SHOWPRODUCTSINPUTDETAIL" />
            
            <xsl:call-template name="LOCATIONS" />
            <xsl:call-template name="COINSURANCESDETAIL" />
			  <!--<xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='38'">
				  <xsl:call-template name="REMUNERATIONS"/>
			  </xsl:if>-->
            <xsl:call-template name="REINSURANCESDETAIL"/>
            <xsl:call-template name="DISCOUNTS"/>

		    <xsl:call-template name="RISKSDETAILS"/>
			  <xsl:call-template name="BILLINGINFORMATIONS"/>
          </xsl:when>
        </xsl:choose>
		  <xsl:choose>
			  <xsl:when test ="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='38'">
				  <xsl:choose>
					  <xsl:when test="INPUTXML/BILLING_INFOS/@COUNT != -1">
						  <xsl:if test="user:SetHeader()=1" />
							  <xsl:apply-templates select="BILLING_INFO"></xsl:apply-templates>
				 
					  </xsl:when>
					  <xsl:otherwise>
						  <tr>
							  <td class="pageheader">
								  <xsl:value-of select="$MyDoc//message[@messageid='109']"/>
							  </td>
						  </tr>
						  <xsl:call-template name="BILLING_MESSAGE"></xsl:call-template>
					  </xsl:otherwise>
				  </xsl:choose>
			  </xsl:when >
			  <xsl:otherwise>
				  <xsl:choose>
					  <xsl:when test="INPUTXML/BILLING_INFOS/@COUNT>0">
						  <xsl:if test="user:SetHeader()=1" />
						  <xsl:for-each select="INPUTXML/BILLING_INFOS">
							  <xsl:apply-templates select="BILLING_INFO"></xsl:apply-templates>
						  </xsl:for-each>
					  </xsl:when>
					  <xsl:otherwise>
						  <xsl:if test="INPUTXML/BILLING_INFOS/@COUNT=0">
							  <tr>
								  <td class="pageheader">
									  <xsl:value-of select="$MyDoc//message[@messageid='109']"/>
								  </td>
							  </tr>
							  <xsl:call-template name="BILLING_MESSAGE"></xsl:call-template>
						  </xsl:if>
					  </xsl:otherwise>
				  </xsl:choose>
			  </xsl:otherwise>
		  </xsl:choose>
      </table>
    </html>
  </xsl:template>
  <xsl:template name="SHOWPRODUCTSINPUTDETAIL">
    <xsl:apply-templates select="INPUTXML/APPLICATIONS/APPLICATION"></xsl:apply-templates>
  </xsl:template>
  <xsl:template match="INPUTXML/APPLICATIONS/APPLICATION">
    <xsl:choose>
      <xsl:when test="POLICY_CURRENCY=''or
                 APP_EFFECTIVE_DATE=''or 
                APP_EXPIRATION_DATE=''or 
                APP_INCEPTION_DATE=''or 
                 POLICY_LOB=''  or 
                APP_INCEPTION_DATE='' or 
                BILL_TYPE_ID='' or BILL_TYPE_ID=0 or
                CO_INSURANCE='' or 
                POLICY_SUBLOB='' or 
                APP_NUMBER='' or 
                UNDERWRITER='' or
                APP_VERSION=''or 
                APP_VERSION='' or 
                APP_TERMS='' or 
                CSR='' or 
                DIV_ID=''or
                PAYOR='' or 
                BILLTO='0'or
                POLICY_LEVEL_COMISSION=''or
                AGENCY_ID=''or
                DOWN_PAY_MODE=''">
        <tr>
          <xsl:choose>
            <xsl:when test="CALLED_FROM=1">
              <td class="pageheader">
                <xsl:value-of select="$MyDoc//message[@messageid='101']"/>
              </td><!--For Policy information:-->
            </xsl:when>
            <xsl:otherwise>
              <td class="pageheader">
                <xsl:value-of select="$MyDoc//message[@messageid='102']"/>
              </td><!--For Application information:-->
            </xsl:otherwise>
          </xsl:choose>
        </tr>
        <xsl:call-template name="UNDERWRITER" />
        <xsl:call-template name="POLICY_LOB" />
        <xsl:call-template name="APP_TERMS" />
        <xsl:call-template name="BILL_TYPE_ID" />
        <xsl:call-template name="POLICY_CURRENCY" />
        <xsl:call-template name="CO_INSURANCE" />
        <xsl:call-template name="POLICY_SUBLOB" />
        <xsl:call-template name="APP_NUMBER" />
        <xsl:call-template name="APP_VERSION"/>
        <xsl:call-template name="CSR"/>
        <xsl:call-template name="DIV_ID"/>
        <xsl:call-template name="APP_EFFECTIVE_DATE" />
        <xsl:call-template name="APP_EXPIRATION_DATE" />
        <xsl:call-template name="APP_INCEPTION_DATE" />
        <xsl:call-template name="PAYOR" />
        <xsl:call-template name="BILLTO"/>
        <xsl:call-template name="POLICY_LEVEL_COMISSION"/>
        <xsl:call-template name="AGENCY_ID"/>
        <xsl:call-template name="DOWN_PAY_MODE"/>
        
        <!--<xsl:call-template name="ACCOUNT_TYPE"/>-->
        <!--<xsl:call-template name="ZIP_CODE"/>
        <xsl:call-template name="ADDRESS1"/>
        <xsl:call-template name="CITY"/>
        <xsl:call-template name="STATE"/>
        <xsl:call-template name="COUNTRY"/>
        <xsl:call-template name="CO_APP_NUMBER"/>
        <xsl:call-template name="CO_APP_DISTRICT"/>-->
        <!--<xsl:call-template name="CO_APPL_GENDER"/>-->
        <!--<xsl:call-template name="CO_APP_ORIGINAL_ISSUE"/>
        <xsl:call-template name="CO_APP_REGIONAL_IDENTIFICATION"/>
        <xsl:call-template name="CO_APPL_MARITAL_STATUS"/>
        <xsl:call-template name="CO_APPL_DOB"/>
        <xsl:call-template name="CO_APPL_CREATION_DATE"/>
        <xsl:call-template name="CO_APP_ACCOUNT_TYPE"/>-->
        
      </xsl:when>
      <xsl:otherwise></xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!-- ================================= TEMPLATES =============================================-->
  <xsl:template name="DOWN_PAY_MODE">
    <xsl:choose>
      <xsl:when test="DOWN_PAY_MODE=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='1']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="AGENCY_ID">
    <xsl:choose>
      <xsl:when test="AGENCY_ID=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='2']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="BILLTO">
    <xsl:choose>
      <xsl:when test="BILLTO='0'">
        <tr>
          <td>
          <xsl:value-of select="$MyDoc//message[@messageid='3']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="PAYOR">
    <xsl:choose>
      <xsl:when test="PAYOR=''">
        <tr>
          <td>
          <xsl:value-of select="$MyDoc//message[@messageid='4']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="POLICY_SUBLOB">
    <xsl:choose>
      <xsl:when test="POLICY_SUBLOB=''">
        <tr>
          <td>
          <xsl:value-of select="$MyDoc//message[@messageid='5']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="APP_VERSION">
    <xsl:choose>
      <xsl:when test="APP_VERSION=''">
        <tr>
          <td>
          <xsl:value-of select="$MyDoc//message[@messageid='6']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="DIV_ID">
    <xsl:choose>
      <xsl:when test="DIV_ID=''">
        <tr>
          <td>
            <xsl:value-of select="$MyDoc//message[@messageid='7']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="CO_INSURANCE">
    <xsl:choose>
      <xsl:when test="CO_INSURANCE=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='8']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="CSR">
    <xsl:choose>
      <xsl:when test="CSR=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='9']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="POLICY_CURRENCY">
    <xsl:choose>
      <xsl:when test="POLICY_CURRENCY=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='10']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="UNDERWRITER">
    <xsl:choose>
      <xsl:when test="UNDERWRITER=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='11']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="APP_EFFECTIVE_DATE">
    <xsl:choose>
      <xsl:when test="APP_EFFECTIVE_DATE='12'">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='12']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="APP_EXPIRATION_DATE">
    <xsl:choose>
      <xsl:when test="APP_EXPIRATION_DATE=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='13']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="APP_INCEPTION_DATE">
    <xsl:choose>
      <xsl:when test="APP_INCEPTION_DATE=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='14']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="POLICY_LOB">
    <xsl:choose>
      <xsl:when test="POLICY_LOB=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='15']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="APP_TERMS">
    <xsl:choose>
      <xsl:when test="APP_TERMS=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='16']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="BILL_TYPE_ID">
    <xsl:choose>
      <xsl:when test="BILL_TYPE_ID=0">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='17']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="APP_NUMBER">
    <xsl:choose>
      <xsl:when test="APP_NUMBER=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='18']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="POLICY_LEVEL_COMISSION">
    <xsl:choose>
      <xsl:when test="POLICY_LEVEL_COMISSION=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='19']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
 
   
 
  <!-- Location Screen -->
  <xsl:template name="LOCATIONS">
    <xsl:choose>
      <xsl:when test="INPUTXML/LOCATIONS/@COUNT>0">
        <xsl:if test="user:SetHeader()=1" />
        <xsl:for-each select="INPUTXML/LOCATIONS">
          <xsl:apply-templates select="LOCATION"></xsl:apply-templates>
        </xsl:for-each>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>        
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template match="LOCATION">
    <xsl:choose>
      <xsl:when test="LOCATION_ID='' or  LOC_ADD1='' or LOC_ZIP=''  or DISTRICT='' or CAL_NUM='' or  NAME='' or LOC_CITY='' or LOC_NUM='' or LOC_COUNTRY='' or NUMBER=''">
        <xsl:if test="user:AssignHeader(1)=1">
          <tr>
            <td class="pageheader"><xsl:value-of select="$MyDoc//message[@messageid='115']"/></td>
          </tr>
        </xsl:if>
        <xsl:if test="user:AssignHeader(0)=0" />
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="LOCATION_ID=''or
                 LOC_ADD1='' or
                 LOC_ZIP='' or
                
                 DISTRICT='' or
                 CAL_NUM='' or
                 NAME='' or
                 LOC_CITY='' or
                 LOC_NUM='' or
                 LOC_COUNTRY='' or
                 NUMBER=''">
        <!--<tr>
          <td class="midcolora">
            <table width="60%">
              <tr>
                <xsl:if test="LOC_NUM=''">
                  <td class="pageheader" width="18%">
                    <xsl:value-of select="$MyDoc//message[@messageid='116']"/>
                  </td>
                  <td class="midcolora" width="36%">
                    <xsl:value-of select="LOC_NUM" />
                  </td>
                </xsl:if>
                <xsl:if test="NAME=''">
                  <td class="pageheader" width="18%">
                    <xsl:value-of select="$MyDoc//message[@messageid='117']"/>
                  </td>
                  <td class="midcolora" width="36%">
                    <xsl:value-of select="NAME" />
                  </td>
                </xsl:if>
              </tr>
            </table>
          </td>
        </tr>-->
        <xsl:if test="user:AssignUnderwritter(0) = 0"></xsl:if>
        <xsl:call-template name="LOC_ADD1" />
        <xsl:call-template name="LOC_ZIP" />
        <!--<xsl:call-template name="ACTIVITY_TYPE" />-->
        <xsl:call-template name="DISTRICT" />
        <xsl:call-template name="CAL_NUM" />
        <xsl:call-template name="NAME" />
        <xsl:call-template name="LOC_CITY" />
        <xsl:call-template name="LOC_NUM"/>
        <xsl:call-template name="LOC_COUNTRY"/>
        <xsl:call-template name="LOCATION_NUMBER"/>
      </xsl:when>
      <xsl:otherwise>
        <tr>
          <td>
            <xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
          </td>
        </tr>
      </xsl:otherwise>
    </xsl:choose>
    <!-- </table>	-->
  </xsl:template>
  <xsl:template name="LOCATIONSMESSAGE">
    <tr>
      <td class="pageheader">
        <xsl:value-of select="$MyDoc//message[@messageid='118']"/>
      </td>
    </tr>
    <tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='20']"/>
      </td>
    </tr>
    <tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='21']"/>
      </td>
    </tr>
    <tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='22']"/>
      </td>
    </tr>
    <tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='23']"/>
      </td>
    </tr>
    <tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='24']"/>
      </td>
    </tr>
    <tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='25']"/></td>
    </tr>
    <tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='26']"/>
      </td>
    </tr>
    <tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='27']"/>
      </td>
    </tr>
    <tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='28']"/>
      </td>
    </tr>
    <tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='29']"/>
      </td>
    </tr>
    <!--<tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='30']"/>
      </td>
    </tr>
    <tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='31']"/>
      </td>
    </tr>-->
  </xsl:template>
  <xsl:template name="LOC_ADD1">
    <xsl:choose>
      <xsl:when test="LOC_ADD1=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='32']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="LOC_ZIP">
    <xsl:choose>
      <xsl:when test="LOC_ZIP=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='23']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <!--<xsl:template name="ACTIVITY_TYPE">
    <xsl:choose>
      <xsl:when test="ACTIVITY_TYPE=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='31']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>-->
  <xsl:template name="DISTRICT">
    <xsl:choose>
      <xsl:when test="DISTRICT=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='26']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="CAL_NUM">
    <xsl:choose>
      <xsl:when test="CAL_NUM=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='20']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="NAME">
    <xsl:choose>
      <xsl:when test="NAME=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='22']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="LOC_CITY">
    <xsl:choose>
      <xsl:when test="LOC_CITY=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='27']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="LOC_NUM">
    <xsl:choose>
      <xsl:when test="LOC_NUM=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='21']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="LOC_COUNTRY">
    <xsl:choose>
      <xsl:when test="LOC_COUNTRY=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='28']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="LOCATION_NUMBER">
    <xsl:choose>
      <xsl:when test="NUMBER=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='168']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <!-- COINSURANCE SCREEN -->
  <xsl:template name="COINSURANCESDETAIL">
    <xsl:variable name="Cval">
      <xsl:value-of select="INPUTXML/COINSURANCES/@COUNT"/>
    </xsl:variable>
    <xsl:choose >
      <xsl:when test="$Cval > 0 ">
        <td>
          <xsl:if test="user:SetHeader()= 1" />
          <xsl:for-each select="INPUTXML/COINSURANCES">
            <xsl:apply-templates select="COINSURANCE"></xsl:apply-templates>
          </xsl:for-each>
        </td>
      </xsl:when>
      <!--<xsl:when test="$Cval = 0 ">
        <td>
          --><!--<tr>
            <td class="pageheader">
              <xsl:value-of select="$MyDoc//message[@messageid='119']"/>
            </td>
          </tr>--><!--
          --><!--<xsl:if test="user:SetHeader()= 1" />--><!--
          <xsl:call-template name="COINSURANCEMESSAGE" />
        </td>
      </xsl:when>-->
    </xsl:choose>
  </xsl:template>
  <xsl:template match="COINSURANCE">
    <xsl:choose>
      <!--<tr>
       <td class="pageheader">For Coinsurance Information:</td>
     </tr>-->
      <xsl:when test="TOTAL_COINSURANCE_PERCENT='' or TOTAL_COINSURANCE_FEE='' or COINSURANCE_FEE_FOLLOWER=''">
        <xsl:if test="user:AssignHeader(1)=1">
          <tr>
            <td class="pageheader">
              <xsl:value-of select="$MyDoc//message[@messageid='119']"/>
            </td>
          </tr>
          <xsl:call-template name="TOTAL_COINSURANCE_PERCENT" />
          <xsl:call-template name="TOTAL_COINSURANCE_FEE" />
          <xsl:call-template name="COINSURANCE_FEE_FOLLOWER" />
        </xsl:if>
        <xsl:if test="user:AssignHeader(0)=0" />
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="COINSURANCE_PERCENT='' or DUPLICATE=''">
        <tr>
          <td class="pageheader">
           
            <xsl:value-of select="$MyDoc//message[@messageid='120']"/>
            <xsl:value-of select="MNT_REIN_COMAPANY_LIST/REIN_COMAPANY_NAME"/>
          </td>
        </tr>
        <xsl:call-template name="COINSURANCE_PERCENT" />
        <xsl:call-template name="DUPLICATE" />
         
      
      </xsl:when>
    </xsl:choose>
    
    <!-- </table>	-->
  </xsl:template>
  <xsl:template name="COINSURANCE_PERCENT">
    <xsl:choose>
      <xsl:when test="COINSURANCE_PERCENT=''">
        <tr>
          <td class="midcolora">
           
            <xsl:value-of select="$MyDoc//message[@messageid='34']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="DUPLICATE">
    <xsl:choose>
      <xsl:when test="DUPLICATE=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='135']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="TOTAL_COINSURANCE_PERCENT">
    <xsl:choose>
      <xsl:when test="TOTAL_COINSURANCE_PERCENT='' and APP_CO_INSURANCE = '14548'">
           <tr>
            <td  class="midcolora">
              <xsl:value-of select="$MyDoc//message[@messageid='213']"/>
              </td>
          </tr>
        </xsl:when>
       <xsl:when test="TOTAL_COINSURANCE_PERCENT='' and APP_CO_INSURANCE = '14549'">
        <tr>
          <td  class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='214']"/>
          </td>
        </tr>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:AssignHeader(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="TOTAL_COINSURANCE_FEE">
    <xsl:choose>
      <xsl:when test="TOTAL_COINSURANCE_FEE=''">
        <tr>
          <td class="midcolora">
            <xsl:if test="user:RemumsgSt() = 0">
              <xsl:value-of select="$MyDoc//message[@messageid='219']"/>
              <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
            </xsl:if>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="COINSURANCE_FEE_FOLLOWER">
    <xsl:choose>
      <xsl:when test="COINSURANCE_FEE_FOLLOWER=''">
        <tr>
          <td class="midcolora">
            <xsl:if test="user:RemumsgSt() = 0">
              <xsl:value-of select="$MyDoc//message[@messageid='218']"/>
              <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
            </xsl:if>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <!--<xsl:template name="TRANSACTION_ID">
    <xsl:choose>
      <xsl:when test="TRANSACTION_ID=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='35']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>-->
  <!--Commented By Lalit for Leader policy No non mandatory -->
  <!--<xsl:template name="LEADER_POLICY_NUMBER">
    <xsl:choose>
      <xsl:when test="LEADER_POLICY_NUMBER=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='36']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>-->
  <xsl:template name="COINSURANCEMESSAGE">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='34']"/>
          </td>
        </tr>
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='35']"/>
          </td>
        </tr>
    <tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='36']"/>
      </td>
    </tr>
  </xsl:template>
  <xsl:template name="REMUNERATIONS">
	  <xsl:choose>
		  <xsl:when test ="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='38'">

		  <xsl:choose>
				  <xsl:when test="INPUTXML/REMUNERATIONS/REMUNERATION/COMMISSION_PERCENT != '0.0000'">
					  
					  <xsl:if test="user:SetHeader()=1" />
					  <xsl:for-each select="INPUTXML/REMUNERATIONS">
						  <xsl:apply-templates select="REMUNERATION"></xsl:apply-templates>
					  </xsl:for-each>
				  </xsl:when>
				  <xsl:otherwise>
						  <xsl:if test="user:SetHeader()=1" />
						  <xsl:call-template name="REMUNERATIONMESSAGE"></xsl:call-template>
				  </xsl:otherwise>
			  </xsl:choose>
		  </xsl:when>	
		  <xsl:otherwise>
				<xsl:choose>
      <xsl:when test="INPUTXML/REMUNERATIONS/@COUNT >0">
        <xsl:if test="user:SetHeader()=1" />
        <xsl:for-each select="INPUTXML/REMUNERATIONS">
          <xsl:apply-templates select="REMUNERATION"></xsl:apply-templates>
        </xsl:for-each>
      </xsl:when>
      <xsl:otherwise>        
        <xsl:if test="INPUTXML/REMUNERATIONS/@COUNT=0">
          <xsl:if test="user:SetHeader()=1" />
          <xsl:call-template name="REMUNERATIONMESSAGE"></xsl:call-template>
        </xsl:if>
      </xsl:otherwise>
    </xsl:choose>
		  </xsl:otherwise>
	  </xsl:choose>
  </xsl:template>
  <xsl:template name="REMUNERATIONMESSAGE">
	  <xsl:choose>
		  <xsl:when test ="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='38'">
			  <xsl:choose>
				  <xsl:when test="INPUTXML/REMUNERATIONS/REMUNERATION/COMMISSION_PERCENT = '0.0000'">
					  <xsl:if test="user:AssignHeader(1)=1">
						  <tr>
							  <td class="pageheader">
								  <xsl:value-of select="$MyDoc//message[@messageid='121']"/>
							  </td>
						  </tr>
					  </xsl:if>
					  <xsl:if test="user:AssignHeader(0)=0" />
					  <tr>
						  <td class="midcolora">
							  <xsl:value-of select="$MyDoc//message[@messageid='37']"/>
						  </td>
					  </tr>
				  </xsl:when>
			  </xsl:choose>
			  </xsl:when>
		  <xsl:otherwise>
			  <xsl:choose>
				  <xsl:when test="INPUTXML/REMUNERATIONS/@COUNT=0">
					  <tr>
						  <td class="midcolora">
							  <xsl:value-of select="$MyDoc//message[@messageid='37']"/>
						  </td>
					  </tr>
					  <tr>
						  <td class="midcolora">
							  <xsl:value-of select="$MyDoc//message[@messageid='38']"/>
						  </td>
					  </tr>
					  <tr>
						  <td class="midcolora">
							  <xsl:value-of select="$MyDoc//message[@messageid='39']"/>
						  </td>
					  </tr>
					  <tr>
						  <td class="midcolora">
							  <xsl:value-of select="$MyDoc//message[@messageid='40']"/>
						  </td>
					  </tr>
					  <tr>
						  <td class="midcolora">
							  <xsl:value-of select="$MyDoc//message[@messageid='130']"/>
						  </td>
					  </tr>
					  <tr>
						  <td class="midcolora">
							  <xsl:value-of select="$MyDoc//message[@messageid='131']"/>
						  </td>
					  </tr>
					  <tr>
						  <td class="midcolora">
							  <xsl:value-of select="$MyDoc//message[@messageid='132']"/>
						  </td>
					  </tr>
				  </xsl:when>
			  </xsl:choose>
		  </xsl:otherwise>
	  </xsl:choose>
  </xsl:template>
  <xsl:template match="REMUNERATION">
    <xsl:choose>
      <xsl:when test="COMMISSION_TYPE='43' and COMMISSION_PERCENT >'100' ">
        <xsl:if test="user:AssignHeader(1)=1">
          <tr>
            <td class="pageheader">
              <xsl:value-of select="$MyDoc//message[@messageid='121']"/>
            </td>
          </tr>
        </xsl:if>
        <xsl:if test="user:AssignHeader(0)=0" />
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="BROKER_ID='' or COMMISSION_TYPE='' or COMMISSION_PERCENT='' ">
        <xsl:if test="user:AssignHeader(1)=1">
          <tr>
            <td class="pageheader">
              <xsl:value-of select="$MyDoc//message[@messageid='121']"/>
            </td>
          </tr>
        </xsl:if>
        <xsl:if test="user:AssignHeader(0)=0" />
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="COMMISSION_TYPE='43' and COMMISSION_PERCENT ='' ">
        <tr>
          <td class="pageheader">
            <xsl:if test="user:RemumsgSt() = 0">
            <xsl:value-of select="$MyDoc//message[@messageid='121']"/>
            <xsl:value-of select="MNT_AGENCY_LIST/AGENCY_DISPLAY_NAME"/>
              <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
            </xsl:if>
          </td>
        </tr>
        <xsl:call-template name="BROKER_ID" />
        <xsl:call-template name="COMMISSION_TYPE" />
        <xsl:call-template name="COMMISSION_PERCENT"/>
        <xsl:call-template name="COMMISSION_PERCENT1"/>
        Commented By Lalit dec 27,2010 for remove amount field mandatory
        <xsl:call-template name="AMOUNT"/>
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="COMMISSION_TYPE='44' and COMMISSION_PERCENT ='' ">
        <tr>
          <td class="pageheader">
            <xsl:if test="user:RemumsgSt() = 0">
            <xsl:value-of select="$MyDoc//message[@messageid='121']"/>
            <xsl:value-of select="MNT_AGENCY_LIST/AGENCY_DISPLAY_NAME"/>
              <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
            </xsl:if>
          </td>
        </tr>
        <xsl:call-template name="COMMISSION_PERCENT2"/>
       </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="COMMISSION_TYPE='45' and COMMISSION_PERCENT ='' ">
        <tr>
          <td class="pageheader">
            <xsl:if test="user:RemumsgSt() = 0">
            <xsl:value-of select="$MyDoc//message[@messageid='121']"/>
            <xsl:value-of select="MNT_AGENCY_LIST/AGENCY_DISPLAY_NAME"/>
              <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
            </xsl:if>
          </td>
        </tr>
        <xsl:call-template name="COMMISSION_PERCENT3"/>
      </xsl:when>
    </xsl:choose>
    
  </xsl:template>
  <xsl:template name="AMOUNT">
    <xsl:choose>
      <xsl:when test="AMOUNT=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='40']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="BROKER_ID">
    <xsl:choose>
      <xsl:when test="BROKER_ID=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='39']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="COMMISSION_TYPE">
    <xsl:choose>
      <xsl:when test="COMMISSION_TYPE=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='38']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="COMMISSION_PERCENT">
    <xsl:choose>
      <xsl:when test="COMMISSION_PERCENT=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='37']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="COMMISSION_PERCENT1">
    <xsl:choose>
      <xsl:when test="COMMISSION_TYPE='43' and COMMISSION_PERCENT ='' ">
        <tr>
          <td class="midcolora">
            <xsl:if test="user:RemumsgSt() = 0">
            <xsl:choose>
              <xsl:when test="TRANSACTION_TYPE = '14560'">
                <xsl:value-of select="$MyDoc//message[@messageid='130']"/>
              </xsl:when>
              <xsl:when test="TRANSACTION_TYPE != '14560' and  POLICY_LEVEL_COMM_APPLIES ='N' ">
                <xsl:value-of select="$MyDoc//message[@messageid='134']"/>
              </xsl:when>
              <xsl:otherwise>
                <xsl:value-of select="$MyDoc//message[@messageid='130']"/>
              </xsl:otherwise>
            </xsl:choose>

            <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
            </xsl:if>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="COMMISSION_PERCENT2">
    <xsl:choose>
      <xsl:when test="COMMISSION_TYPE='44' and COMMISSION_PERCENT ='' ">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='131']"/>
           </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="COMMISSION_PERCENT3">
    <xsl:choose>
      <xsl:when test="COMMISSION_TYPE='45' and COMMISSION_PERCENT ='' ">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='132']"/>
             </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  
  

  <!--REINSURANCE SCREEN-->
  <xsl:template name="REINSURANCESDETAIL">
    <xsl:choose>
      <xsl:when test="INPUTXML/REINSURANCES/@COUNT>0">
        <xsl:if test="user:SetHeader()=1" />
        <xsl:for-each select="INPUTXML/REINSURANCES">
          <xsl:apply-templates select="REINSURANCE"></xsl:apply-templates>
        </xsl:for-each>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template match="REINSURANCE">
    <xsl:choose>
      <xsl:when test="CONTRACT_FACULTATIVE='' or REINSURANCE_CEDED=''">
        <xsl:if test="user:AssignHeader(1)=1">
          <tr>
            <td class="pageheader">
              <xsl:value-of select="$MyDoc//message[@messageid='123']"/>
          </td>
          </tr>
        </xsl:if>
        <xsl:if test="user:AssignHeader(0)=0" />
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="CONTRACT_FACULTATIVE=''or
                REINSURANCE_CEDED='' ">
        <tr>
          <td class="pageheader">
            <xsl:value-of select="$MyDoc//message[@messageid='126']"/>
            <xsl:value-of select="MNT_REIN_COMAPANY_LIST/REIN_COMAPANY_NAME"/>
          </td>
        </tr>
        <xsl:call-template name="CONTRACT_FACULTATIVE" />
        <xsl:call-template name="REINSURANCE_CEDED" />
        <!--<xsl:call-template name="REINSURANCE_COMMISSION" />-->
      </xsl:when>
    </xsl:choose>
    <!--</table>-->
  </xsl:template>
  <xsl:template name="CONTRACT_FACULTATIVE">
    <xsl:choose>
      <xsl:when test="CONTRACT_FACULTATIVE=''">
        <tr>
          <td class="midcolora">
            <tr>
              <td class="midcolora">
                <xsl:value-of select="$MyDoc//message[@messageid='41']"/>
              </td>
            </tr>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="REINSURANCE_CEDED">
    <xsl:choose>
      <xsl:when test="REINSURANCE_CEDED=''">
        <tr>
          <td class="midcolora">
            <tr>
              <td class="midcolora">
                <xsl:value-of select="$MyDoc//message[@messageid='42']"/>
              </td>
            </tr>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="REINSURANCE_COMMISSION">
    <xsl:choose>
      <xsl:when test="REINSURANCE_COMMISSION=''">
        <tr>
          <td class="midcolora">
            <tr>
              <td class="midcolora">
                <xsl:value-of select="$MyDoc//message[@messageid='43']"/>
              </td>
            </tr>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
 <!--DISCOUNT/SURCHARGE -->
  <xsl:template name="DISCOUNTS">
    <xsl:choose>
      <xsl:when test="INPUTXML/DISCOUNTS/@COUNT>0">
        <xsl:if test="user:SetHeader()=1" />
        <xsl:for-each select="INPUTXML/DISCOUNTS">
          <xsl:apply-templates select="DISCOUNT"></xsl:apply-templates>
        </xsl:for-each>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template match="DISCOUNT">
    <xsl:choose>
      <xsl:when test="PERCENTAGE=''">
        <xsl:if test="user:AssignHeader(1)=1">
          <tr>
            <td class="pageheader">
              <xsl:value-of select="$MyDoc//message[@messageid='124']"/>
            </td>
          </tr>
        </xsl:if>
        <xsl:if test="user:AssignHeader(0)=0" />
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="PERCENTAGE=''">
        <tr>
          <td class="pageheader">
            <xsl:value-of select="$MyDoc//message[@messageid='125']"/>
            <xsl:value-of select="CLT_CUSTOMER_LIST/CUSTOMER_FIRST_NAME"/>
          </td>
        </tr>
        <xsl:call-template name="PERCENTAGE" />
      </xsl:when>
    </xsl:choose>
    <!-- </table>	-->
  </xsl:template>
  <xsl:template name="PERCENTAGE">
    <xsl:choose>
      <xsl:when test="PERCENTAGE=''">
        <tr>
          <td class="midcolora">
            <tr>
              <td class="midcolora">
                <xsl:value-of select="$MyDoc//message[@messageid='44']"/>
              </td>
            </tr>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <!--Risk Details -->
  <xsl:template name="RISKSDETAILS">
    <xsl:choose>
      <xsl:when test="INPUTXML/RISKS/@COUNT>0">
        <xsl:if test="user:SetHeader()=1" />
            <xsl:for-each select="INPUTXML/RISKS/RISK">            
              <xsl:call-template name="RISK"></xsl:call-template>
            </xsl:for-each>
        
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="INPUTXML/RISKS/@COUNT=0">
          <xsl:if test="user:SetHeader()=1" />
          <tr>
            <td class="pageheader">
              <xsl:value-of select="$MyDoc//message[@messageid='127']"/>
            </td>
          </tr>
          <xsl:call-template name="RISKMESSAGE"></xsl:call-template>
        </xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="RISKMESSAGE">
    <xsl:choose>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='13'">
        <xsl:call-template name="HULL"></xsl:call-template>
      </xsl:when>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='9'">
        <xsl:call-template name="RISK_LOCATIONS"></xsl:call-template>
      </xsl:when>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='10'">
        <xsl:call-template name="LOCATIONS_INFO"></xsl:call-template>
      </xsl:when>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='11'">
        <xsl:call-template name="COMPANY_LOCATIONS"></xsl:call-template>
      </xsl:when>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='12'">
        <xsl:call-template name="LIABILITY_INFO"></xsl:call-template>
      </xsl:when>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='14'">
        <xsl:call-template name="LOCATIONS_INFO"></xsl:call-template>
      </xsl:when>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='15'">
        <xsl:call-template name="PERSONAL_INFO"></xsl:call-template>
      </xsl:when>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='16'">
        <xsl:call-template name="ROBBERY_LOCATIONS"></xsl:call-template>
      </xsl:when>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='17'">
        <xsl:call-template name="VEHICLE_DETAILS"></xsl:call-template>
      </xsl:when>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='18'">
        <xsl:call-template name="TRANSPORT_VEHICLE_INFO"></xsl:call-template>
      </xsl:when>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='19'">
        <xsl:call-template name="DWELLING_LOCATIONS"></xsl:call-template>
        </xsl:when>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='20'">
        <xsl:call-template name="VOYAGE_INFORMATION"></xsl:call-template>
      </xsl:when>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='21'">
        <xsl:call-template name="GROUP_PERSONAL_ACCIDENT_INFO"></xsl:call-template>
      </xsl:when>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='22'">
        <xsl:call-template name="PERSONAL_ACCIDENT_INFO"></xsl:call-template>
      </xsl:when>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='23'">
        <xsl:call-template name="VOYAGE_INFORMATION"></xsl:call-template>
      </xsl:when>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='28'">
        <xsl:call-template name="VEHICLE_DETAILS"></xsl:call-template>
      </xsl:when>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='27'">
        <xsl:call-template name="VEHICLE_DETAILS"></xsl:call-template>
      </xsl:when>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='25'">
        <xsl:call-template name="COMPANY_LOCATIONS"></xsl:call-template>
      </xsl:when>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='26'">
        <xsl:call-template name="RISK_LOCATIONS"></xsl:call-template>
      </xsl:when>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='29'">
        <xsl:call-template name="VEHICLE_DETAILS"></xsl:call-template>
      </xsl:when>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='30'">
        <xsl:call-template name="TRANSPORT_VEHICLE_INFO"></xsl:call-template>
      </xsl:when>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='31'">
        <xsl:call-template name="TRANSPORT_VEHICLE_INFO"></xsl:call-template>
      </xsl:when>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='32'">
        <xsl:call-template name="LOCATIONS_INFO"></xsl:call-template>
      </xsl:when>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='33'">
        <xsl:call-template name="GROUP_PERSONAL_ACCIDENT_INFO"></xsl:call-template>
      </xsl:when>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='34'">
        <xsl:call-template name="GROUP_PERSONAL_ACCIDENT_INFO"></xsl:call-template>
      </xsl:when>
	  <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='38'">
			<xsl:call-template name="VEHICLE_DETAILS"></xsl:call-template>
	  </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="LOCATIONS_INFO">
    <tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='45']"/>
      </td>
    </tr>
  </xsl:template>
  <xsl:template name="COMPANY_LOCATIONS">
    <tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='46']"/>
      </td>
    </tr>
  </xsl:template>
  <xsl:template name="LIABILITY_INFO">
    <tr>
      <td class="midcolora">
       
        <xsl:value-of select="$MyDoc//message[@messageid='47']"/>
      </td>
    </tr>
  </xsl:template>
  <xsl:template name="PERSONAL_INFO">
    <tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='48']"/>
      </td>
    </tr>
  </xsl:template>
  <xsl:template name="ROBBERY_LOCATIONS">
    <tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='49']"/>
      </td>
    </tr>
  </xsl:template>
  <xsl:template name="VEHICLE_DETAILS">
    <tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='50']"/>
      </td>
    </tr>
  </xsl:template>
  <xsl:template name="TRANSPORT_VEHICLE_INFO">
    <tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='50']"/>
      </td>
    </tr>
  </xsl:template>
  <xsl:template name="DWELLING_LOCATIONS">
    <tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='51']"/>
      </td>
    </tr>
  </xsl:template>
  <xsl:template name="VOYAGE_INFORMATION">
    <tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='52']"/>
      </td>
    </tr>
  </xsl:template>
  <xsl:template name="GROUP_PERSONAL_ACCIDENT_INFO">
    <tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='53']"/>
      </td>
    </tr>
  </xsl:template>
  <xsl:template name="PERSONAL_ACCIDENT_INFO">
    <tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='54']"/>
      </td>
    </tr>
  </xsl:template>
 <xsl:template name="HULL">
    <tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='55']"/>
      </td>
    </tr>
  </xsl:template>
    <xsl:template name="RISK_LOCATIONS">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='56']"/>
        </td>
      </tr>
  </xsl:template>
  
  <xsl:template name="RISK" >   
    <xsl:choose>
      <xsl:when test="LOCATION=''  or VESSEL_NUMBER='' or NAME_OF_VESSEL='' or TYPE_OF_VESSEL='' or STATE_ID=''
              or CPF_NUM=''
              or INDIVIDUAL_NAME=''
              or APPLICANT_ID=''
              or REG_ID_ISSUES=''
              or REG_IDEN=''
              or REG_ID_ISSUES=''
              or REG_IDEN=''
              or POSITION_ID=''">
        <xsl:if test="user:AssignHeader(1)=1">
          <tr>
            <td class="pageheader">
              <xsl:value-of select="$MyDoc//message[@messageid='127']"/>
            </td>            
          </tr>          
        </xsl:if>
        <xsl:if test="user:AssignHeader(0)=0" />
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="LOB_ID=9">
        <xsl:choose>
          <xsl:when test="LOCATION='' ">
            <tr>
              <td class="pageheader">
                <xsl:value-of select="$MyDoc//message[@messageid='118']"/>
                <xsl:value-of select="LOCATION"/>
              </td>
            </tr>
            <xsl:call-template name="LOCATION"/>
            <!--<xsl:call-template name="ACTIVITY_TYPE_RISK"/>-->
            <!--<xsl:call-template name="CONSTRUCTION" />-->
          </xsl:when> 
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="LOB_ID=13">
        <xsl:choose>
          <xsl:when test="VESSEL_NUMBER=''or NAME_OF_VESSEL=''or TYPE_OF_VESSEL=''">
            <tr>
              <td class="pageheader">
                <xsl:value-of select="$MyDoc//message[@messageid='128']"/>
                <xsl:value-of select="MANUFACTURER"/>
              </td>
            </tr>
            <xsl:call-template name="VESSEL_NUMBER" />
            <xsl:call-template name="NAME_OF_VESSEL" />
            <xsl:call-template name="TYPE_OF_VESSEL"/>
          </xsl:when>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="LOB_ID='15'">
        <xsl:choose>
          <xsl:when test="
                STATE_ID=''
              or CPF_NUM=''
              or INDIVIDUAL_NAME=''
              or APPLICANT_ID=''
              or REG_ID_ISSUES=''
              or  REG_IDEN=''
              or POSITION_ID=''
              or CODE=''">
            <xsl:call-template name="STATE_ID" />
            <xsl:call-template name="CPF_NUM" />
            <xsl:call-template name="INDIVIDUAL_NAME"/>
            <xsl:call-template name="APPLICANT_ID" />
            <xsl:call-template name="REG_IDEN" />
            <xsl:call-template name="REG_ID_ISSUES"/>
            <xsl:call-template name="POSITION_ID"/>
            <xsl:call-template name="CODE"/>
          </xsl:when>
        </xsl:choose>
      </xsl:when>
      <xsl:when test='LOB_ID=21'>
        <xsl:choose>
          <xsl:when test="
                STATE_ID=''
              or CPF_NUM=''
              or INDIVIDUAL_NAME=''
              or REG_ID_ISSUES=''
              or REG_IDEN=''
              or POSITION_ID=''
              or CODE=''">
            <xsl:call-template name="STATE_ID" />
            <xsl:call-template name="CPF_NUM" />
            <xsl:call-template name="INDIVIDUAL_NAME"/>
            <xsl:call-template name="REG_IDEN" />
            <xsl:call-template name="REG_ID_ISSUES"/>
            <xsl:call-template name="POSITION_ID"/>
            <xsl:call-template name="CODE"/>
          </xsl:when>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="LOB_ID=10">
        <xsl:choose>
          <xsl:when test="LOCATION='' ">
            <xsl:call-template name="LOCATION" />
            <!--<xsl:call-template name="CONSTRUCTION" />-->
          </xsl:when>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="LOB_ID=11">
        <xsl:choose>
          <xsl:when test="LOCATION=''or CONSTRUCTION=''">
            <xsl:call-template name="LOCATION" />
            <xsl:call-template name="CONSTRUCTION" />
          </xsl:when>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="LOB_ID=12">
        <xsl:choose>
          <xsl:when test="LOCATION='' or CONSTRUCTION=''">
            <xsl:call-template name="LOCATION" />
            <xsl:call-template name="CONSTRUCTION" />
          </xsl:when>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="LOB_ID=14">
        <xsl:choose>
          <xsl:when test="LOCATION='' or CONSTRUCTION=''">
            <xsl:call-template name="LOCATION" />
            <xsl:call-template name="CONSTRUCTION"/>
          </xsl:when>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="LOB_ID=16">
        <xsl:choose>
          <xsl:when test="LOCATION=''or CONSTRUCTION=''">
            <xsl:call-template name="LOCATION" />
            <xsl:call-template name="CONSTRUCTION" />
          </xsl:when>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="LOB_ID=19">
        <xsl:choose>
          <xsl:when test="LOCATION=''">
            <xsl:call-template name="LOCATION" />
          </xsl:when>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="LOB_ID=22">
        <xsl:choose>
          <xsl:when test="START_DATE=''or END_DATE=''or NUMBER_OF_PASSENGER=''">
            <xsl:call-template name="START_DATE" />
            <xsl:call-template name="END_DATE" />
            <xsl:call-template name="NUMBER_OF_PASSENGERS" />
          </xsl:when>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="LOB_ID=17">
        <xsl:choose>
          <xsl:when test="MANUFACTURED_YEAR=''or VEHICLE_NUMBER=''or CLIENT_ORDER=''or RISK_EFFECTIVE_DATE=''or RISK_EXPIRE_DATE=''">
            <xsl:call-template name="MANUFACTURED_YEAR" />
            <xsl:call-template name="VEHICLE_NUMBER" />
            <xsl:call-template name="CLIENT_ORDER" />
            <xsl:call-template name="RISK_EFFECTIVE_DATE" />
            <xsl:call-template name="RISK_EXPIRE_DATE" />
          </xsl:when>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="LOB_ID=18">
        <xsl:choose>
          <xsl:when test="MANUFACTURED_YEAR=''or VEHICLE_NUMBER=''or CLIENT_ORDER=''or RISK_EFFECTIVE_DATE=''or RISK_EXPIRE_DATE=''">
            <xsl:call-template name="MANUFACTURED_YEAR" />
            <xsl:call-template name="VEHICLE_NUMBER" />
            <xsl:call-template name="CLIENT_ORDER" />
            <xsl:call-template name="RISK_EFFECTIVE_DATE" />
            <xsl:call-template name="RISK_EXPIRE_DATE" />
          </xsl:when>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="LOB_ID=20">
        <xsl:choose>
          <xsl:when test="COMMODITY_NUMBER=''or DEPARTING_DATE=''or COMMODITY=''or ORIGIN_COUNTRY=''or ORIGIN_STATE=''or ORIGIN_CITY=''or
                    DESTINATION_COUNTRY=''or DESTINATION_STATE=''or DESTINATION_CITY=''or CONVEYANCE_TYPE='' ">
            <xsl:call-template name="COMMODITY_NUMBER" />
            <xsl:call-template name="DEPARTING_DATE" />
            <xsl:call-template name="COMMODITY" />
            <xsl:call-template name="ORIGIN_COUNTRY" />
            <xsl:call-template name="ORIGIN_STATE" />
            <xsl:call-template name="ORIGIN_CITY" />
            <xsl:call-template name="DESTINATION_COUNTRY" />
            <xsl:call-template name="DESTINATION_STATE" />
            <xsl:call-template name="DESTINATION_CITY" />
            <xsl:call-template name="CONVEYANCE_TYPE" />
          </xsl:when>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="LOB_ID=23">
        <xsl:choose>
          <xsl:when test="COMMODITY_NUMBER=''or DEPARTING_DATE=''or COMMODITY=''or ORIGIN_COUNTRY=''or ORIGIN_STATE=''or ORIGIN_CITY=''or
                    DESTINATION_COUNTRY=''or DESTINATION_STATE=''or DESTINATION_CITY=''or CONVEYANCE_TYPE='' ">
            <!--<tr>
              <td class="pageheader">
                Location:
                <xsl:value-of select="LOCATION"/>
              </td>
            </tr>-->
            <xsl:call-template name="COMMODITY_NUMBER" />
            <xsl:call-template name="DEPARTING_DATE" />
            <xsl:call-template name="COMMODITY" />
            <xsl:call-template name="ORIGIN_COUNTRY" />
            <xsl:call-template name="ORIGIN_STATE" />
            <xsl:call-template name="ORIGIN_CITY" />
            <xsl:call-template name="DESTINATION_COUNTRY" />
            <xsl:call-template name="DESTINATION_STATE" />
            <xsl:call-template name="DESTINATION_CITY" />
            <xsl:call-template name="CONVEYANCE_TYPE" />
          </xsl:when>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="STATE_ID">
    <xsl:choose>
      <xsl:when test="STATE_ID=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='29']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="CPF_NUM">
    <xsl:choose>
      <xsl:when test="CPF_NUM=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='60']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="INDIVIDUAL_NAME">
    <xsl:choose>
      <xsl:when test="INDIVIDUAL_NAME=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='61']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="APPLICANT_ID">
    <xsl:choose>
      <xsl:when test="APPLICANT_ID=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='62']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="REG_IDEN">
    <xsl:choose>
      <xsl:when test="REG_IDEN=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='63']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="REG_ID_ISSUES">
    <xsl:choose>
      <xsl:when test="REG_ID_ISSUES=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='64']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="POSITION_ID">
    <xsl:choose>
      <xsl:when test="POSITION_ID=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='65']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="CODE">
    <xsl:choose>
      <xsl:when test="CODE=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='66']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <!--<xsl:template name="ACTIVITY_TYPE_RISK">
    <xsl:choose>
      <xsl:when test="ACTIVITY_TYPE=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='68']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>-->
  <xsl:template name="CONSTRUCTION">
    <xsl:choose>
      <xsl:when test="CONSTRUCTION=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='67']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="LOCATION">
    <xsl:choose>
      <xsl:when test="LOCATION=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='69']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="VESSEL_NUMBER">
    <xsl:choose>
      <xsl:when test="VESSEL_NUMBER=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='70']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="NAME_OF_VESSEL">
    <xsl:choose>
      <xsl:when test="NAME_OF_VESSEL=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='71']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="TYPE_OF_VESSEL">
    <xsl:choose>
      <xsl:when test="TYPE_OF_VESSEL=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='72']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="START_DATE">
    <xsl:choose>
      <xsl:when test="START_DATE=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='73']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="END_DATE">
    <xsl:choose>
      <xsl:when test="END_DATE=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='74']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="NUMBER_OF_PASSENGERS">
    <xsl:choose>
      <xsl:when test="NUMBER_OF_PASSENGERS=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='75']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="MANUFACTURED_YEAR">
    <xsl:choose>
      <xsl:when test="MANUFACTURED_YEAR=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='76']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="VEHICLE_NUMBER">
    <xsl:choose>
      <xsl:when test="VEHICLE_NUMBER=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='77']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="CLIENT_ORDER">
    <xsl:choose>
      <xsl:when test="CLIENT_ORDER=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='78']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="RISK_EFFECTIVE_DATE">
    <xsl:choose>
      <xsl:when test="RISK_EFFECTIVE_DATE=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='79']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="RISK_EXPIRE_DATE">
    <xsl:choose>
      <xsl:when test="RISK_EXPIRE_DATE=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='80']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="COMMODITY_NUMBER">
    <xsl:choose>
      <xsl:when test="COMMODITY_NUMBER=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='81']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="DEPARTING_DATE">
    <xsl:choose>
      <xsl:when test="DEPARTING_DATE=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='82']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="COMMODITY">
    <xsl:choose>
      <xsl:when test="COMMODITY=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='83']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="ORIGIN_COUNTRY">
    <xsl:choose>
      <xsl:when test="ORIGIN_COUNTRY=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='84']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="ORIGIN_STATE">
    <xsl:choose>
      <xsl:when test="ORIGIN_STATE=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='85']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="ORIGIN_CITY">
    <xsl:choose>
      <xsl:when test="ORIGIN_CITY=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='86']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="DESTINATION_COUNTRY">
    <xsl:choose>
      <xsl:when test="DESTINATION_COUNTRY=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='87']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="DESTINATION_STATE">
    <xsl:choose>
      <xsl:when test="DESTINATION_STATE=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='88']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="DESTINATION_CITY">
    <xsl:choose>
      <xsl:when test="DESTINATION_CITY=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='89']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="CONVEYANCE_TYPE">
    <xsl:choose>
      <xsl:when test="CONVEYANCE_TYPE=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='58']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <!--<xsl:template name="RISKMESSAGE">
 <xsl:choose>
     <xsl:when test="LOB_ID='13'">
       <xsl:call-template name="HULL"></xsl:call-template>
    </xsl:when>
  </xsl:choose>
</xsl:template>
  <xsl:template name="HULL">
        <tr>
          <td class="midclora">Please enter hull Information</td>
        </tr>
  </xsl:template>-->
  <!-- BILLING INFO DETAILS-->
  <xsl:template name="BILLINGINFORMATIONS">

	  <xsl:choose>
		  <xsl:when test ="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID='38'">
			  <xsl:choose>
				  <xsl:when test="INPUTXML/BILLING_INFOS/@COUNT > 0">
					  <xsl:if test="user:SetHeader()=1" />
						  <xsl:apply-templates select="BILLING_INFO"></xsl:apply-templates>
				  </xsl:when>
				  <xsl:otherwise>
						  <tr>
							  <td class="pageheader">
								  <xsl:value-of select="$MyDoc//message[@messageid='109']"/>
							  </td>
						  </tr>
						  <xsl:call-template name="BILLING_MESSAGE"></xsl:call-template>
				  </xsl:otherwise>
			  </xsl:choose>
		  </xsl:when >
		  <xsl:otherwise>
			  <xsl:choose>
				  <xsl:when test="INPUTXML/BILLING_INFOS/@COUNT>0">
					  <xsl:if test="user:SetHeader()=1" />
					  <xsl:for-each select="INPUTXML/BILLING_INFOS">
						  <xsl:apply-templates select="BILLING_INFO"></xsl:apply-templates>
					  </xsl:for-each>
				  </xsl:when>
				  <xsl:otherwise>
					  <xsl:if test="INPUTXML/BILLING_INFOS/@COUNT=0">
						  <tr>
							  <td class="pageheader">
								  <xsl:value-of select="$MyDoc//message[@messageid='109']"/>
							  </td>
						  </tr>
						  <xsl:call-template name="BILLING_MESSAGE"></xsl:call-template>
					  </xsl:if>
				  </xsl:otherwise>
			  </xsl:choose>
		  </xsl:otherwise>
	  </xsl:choose>
  </xsl:template>
  <xsl:template match="BILLING_INFO">
	  <tr>
		  <td class="midcolora">
			  <xsl:value-of select="$MyDoc//message[@messageid='50']"/>
		  </td>
	  </tr>
   </xsl:template>
 <!--</table>-->
  <xsl:template name="TOTAL_PREMIUM">
    <xsl:choose>
      <xsl:when test="TOTAL_PREMIUM=''">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='33']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="BILLING_MESSAGE">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='59']"/>
          </td>
        </tr>
  </xsl:template>
</xsl:stylesheet>
  