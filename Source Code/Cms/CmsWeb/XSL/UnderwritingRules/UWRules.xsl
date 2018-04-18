<!--	Check  for the Policy Level Product Rules-->
<!--	Name : Charles Gomes-->
<!--	Date : 21 Apr,2010  -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:user="urn:user-namespace-here">
  <msxsl:script language="c#" implements-prefix="user">
    <![CDATA[ 
		int intIsRejected = 1;
		public int IsApplicationAcceptable(int tt)
		{
			intIsRejected = intIsRejected*tt;
			return intIsRejected;
		}
		int intIsReferred = 1;
		public int CheckRefer(int tt)
		{
			intIsReferred = intIsReferred*tt;
			return intIsReferred;
		}
		int intCSS=1;
		public int ApplyColor(int colorvalue)
		{
			intCSS=colorvalue;			
			return intCSS;
		}
		int intVerify=1;
		public int AllVerified(int status)
		{
			intVerify=intVerify*status;			
			return intVerify;
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
    string COAPP_ID="";
    public int ShowApplicant(String Co_AppID)
		{	
      if(COAPP_ID != Co_AppID)
      {
			    COAPP_ID = Co_AppID;
          return 1;
      }
       else
       {
         return  0;
       }
      
		}
    int  intHeaderValid = 1;
		public int AssignHeader(int assignvalue)
		{
			intHeaderValid = intHeaderValid*assignvalue;
			return intHeaderValid;
		}
    int  CountMsg = 0;
		public int SetCoverageMsg(int Count)
		{
			CountMsg = CountMsg + Count;
			return CountMsg;
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
          </td>
          <!--Customer Name :-->
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
          </td>
          <!--Address :-->
          <td class="midcolora">
            <xsl:value-of select="INPUTXML/APPLICATIONS/APPLICATION/ADDRESS" />
          </td>
          <td class="pageheader">
            <xsl:value-of select="$MyDoc//message[@messageid='94']"/>
          </td>
          <!--Phone:-->
          <td class="midcolora">
            <xsl:value-of select="INPUTXML/APPLICATIONS/APPLICATION/CUSTOMER_HOME_PHONE" />
          </td>
        </tr>
        <tr>
          <xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=0">
            <td class="pageheader">
              <xsl:value-of select="$MyDoc//message[@messageid='95']"/>
            </td>
            <!--App. No :-->
          </xsl:if>
          <xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=1">
            <td class="pageheader">
              <xsl:value-of select="$MyDoc//message[@messageid='103']"/>
            </td>
            <!--Policy No :-->
          </xsl:if>
          <td class="midcolora">
            <xsl:value-of select="INPUTXML/APPLICATIONS/APPLICATION/APP_NO" />
          </td>
          <xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=0">
            <td class="pageheader">
              <xsl:value-of select="$MyDoc//message[@messageid='96']"/>
            </td>
            <!--App. Version :-->
            <td class="midcolora">
              <xsl:value-of select="INPUTXML/APPLICATIONS/APPLICATION/APP_VERSION_NO" />
            </td>
          </xsl:if>
          <xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=1">
            <td class="pageheader">
              <xsl:value-of select="$MyDoc//message[@messageid='97']"/>
            </td>
            <!--Policy Version :-->
            <td class="midcolora">
              <xsl:value-of select="INPUTXML/APPLICATIONS/APPLICATION/POLICY_DISP_VERSION" />
            </td>
          </xsl:if>
        </tr>
      </table>
      <xsl:call-template name="PRODUCT_REJECTION_CASES" />
      <!--UnCommented by Lalit April 01,2011 ,Billing info premium == coverage premium is reffered rule.i-track #962-->
      <xsl:call-template name="PRODUCT_REFERRED_CASES" />


      <table border="1" align="center" width='100%'>
        <tr>
          <xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=0">
            <td class="headereffectcenter">
              <xsl:value-of select="$MyDoc//message[@messageid='98']"/>
            </td>
            <!--Application verification status-->
          </xsl:if>
          <xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=1">
            <td class="headereffectcenter">
              <xsl:value-of select="$MyDoc//message[@messageid='99']"/>
            </td>
            <!--Policy verification status-->
          </xsl:if>
        </tr>
        <xsl:choose>
          <xsl:when test="user:IsApplicationAcceptable(1) = 0">
            <tr>
              <xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=0">
                <td class="pageheader">
                  <xsl:value-of select="$MyDoc//message[@messageid='104']"/>
                </td>
                <!--This application has been rejected on the following grounds-->
              </xsl:if>
              <xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=1">
                <td class="pageheader">
                  <xsl:value-of select="$MyDoc//message[@messageid='105']"/>
                </td>
                <!--This Policy has been rejected on the following grounds-->
              </xsl:if>
            </tr>
            <!--rejected messages-->
            <tr>
              <td>
               
                <xsl:apply-templates select="INPUTXML/APPLICATIONS/APPLICATION" />                
                <xsl:apply-templates select="INPUTXML/REMUNERATIONS/REMUNERATION" />
                <!--Commented by Lalit. April 01, 2011 move to refered rule-->
                <!--Uncommented by Aditya on 30-09-2011 for TFS BUG # 478-->                
                <xsl:apply-templates select="INPUTXML/BILLING_INFOS/BILLING_INFO" />
                <xsl:apply-templates select="INPUTXML/RISKS/RISK" />
                <xsl:apply-templates select="INPUTXML/COINSURANCES/COINSURANCE" />
                <!--<xsl:apply-templates select="INPUTXML/COVERAGES/COVERAGE" />-->
                <xsl:apply-templates select="INPUTXML/REINSURANCES/REINSURANCE" />
                <!--Added by Aditya for tfs bug # 177-->
                
              </td>
            </tr>
          </xsl:when>
          <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID !='13' and INPUTXML/APPLICATIONS/APPLICATION/LOB_ID !='38'">
            <tr>
              <xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=0">
                <td class="pageheader">
                  <xsl:value-of select="$MyDoc//message[@messageid='106']"/>
                </td>
                <!--This application has been referred on the following grounds-->
              </xsl:if>
              <xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=1">
                <td class="pageheader">
                  <xsl:value-of select="$MyDoc//message[@messageid='107']"/>
                </td>
                <!--This Policy has been referred on the following grounds-->
              </xsl:if>
            </tr>
            <!--referred messages-->
            <tr>
              <td>
                <xsl:apply-templates select="INPUTXML/BILLING_INFOS/BILLING_INFO" />
                <xsl:apply-templates select="INPUTXML/RISKS/RISK" />
                <xsl:apply-templates select="INPUTXML/REMUNERATIONS/REMUNERATION" />
                <xsl:apply-templates select="INPUTXML/APPLICATIONS/APPLICATION"></xsl:apply-templates>
                <xsl:apply-templates select="INPUTXML/APPLICANTS/APPLICANT"></xsl:apply-templates>
                <xsl:apply-templates select="INPUTXML/ENDORSEMENT_REMUNERATIONS/REMU" />
                <xsl:apply-templates select="INPUTXML/REINSURANCES/REINSURANCE" />  <!--Added by Aditya for tfs bug # 180-->
                <!--<xsl:apply-templates select="INPUTXML/COVERAGES/COVERAGE" />-->  <!--Commented by Aditya For itrack # 1532,this msg has been shifted to confirmation box-->
              </td>
            </tr>
          </xsl:when>
          <xsl:otherwise>
            <tr>
              <td class="midcolora">
                <xsl:value-of select="$MyDoc//message[@messageid='108']"/>
              </td>
              <!--All the underwriting rules are verified.-->
              <xsl:if test="user:AllVerified(0)=1"></xsl:if>
            </tr>
          </xsl:otherwise>
        </xsl:choose>
      </table>
    </html>
    <span style="display:none">
      <returnValue>
        <xsl:value-of select="user:IsApplicationAcceptable(1)" />
      </returnValue>
      <verifyStatus>
        <xsl:value-of select="user:AllVerified(1)" />
      </verifyStatus>
		
      <ReferedStatus>
		  <xsl:choose>
		  <xsl:when test ="INPUTXML/APPLICATIONS/APPLICATION/LOB_ID ='13' or INPUTXML/APPLICATIONS/APPLICATION/LOB_ID ='38'">
			  <xsl:value-of select="user:CheckRefer(1) = 1" />
		  </xsl:when>
			  <xsl:otherwise>
				  <xsl:value-of select="user:CheckRefer(1)" />
			  </xsl:otherwise>
		  </xsl:choose>
        
      </ReferedStatus>
    </span>
  </xsl:template>
  <xsl:template name="PRODUCT_REJECTION_CASES">
    <!--  1 -->
    <xsl:call-template name="RJ_OF_CO_APP" />
    <!--  2 -->
    <xsl:call-template name="COUNT_LEADER1" />
    <!--  3 -->
    <!--8-->
    <xsl:call-template name="RJ_OF_COMMISSION" />
    <xsl:call-template name="RJ_OF_FEE_PERCENT" />
    <xsl:call-template name="RJ_OF_PRO_LABOUR" />

    <xsl:call-template name="SUM_OF_LIMIT" />
    <!--  4 -->
    <xsl:call-template name="BASIC_SUM_LIMIT" />
    <!--  5 -->
    <xsl:call-template name="COUNT_LEADER_FOLLWER" />
    <!--  6 -->
    <!--<xsl:call-template name="COUNT_FOLLOWER" />-->
    <!--  7 -->
    <!--Commented by Lalit April 01,2011 ,Billing info premium = coverage premium is reffered rule.i-track #962-->
    <!--<xsl:call-template name="BILLING_INFOS" />-->

    <!--  7 -->
    <xsl:call-template name="COUNT_IS_MAIN" />
    <xsl:call-template name="SUBCOVERAGE_SI_SUM" />
    <xsl:call-template name="SUBCOVERAGE_WP_SUM" />
    <xsl:call-template name="POLICY_COMMISSION_PERCENT" />
    <xsl:call-template name="POLICY_FEES_PERCENT" />
    <xsl:call-template name="POLICY_PRO_LABORE_PERCENT" />
    <xsl:call-template name="APPLICATION_AGENCY_TERMINATED" />
    <xsl:call-template name="INSTALLMENT_AMOUNT_SUM" /> <!--Added by Aditya on 30-08-2011 for TFS Bug # 478-->
    <xsl:call-template name="REINSURANCE_CEDED_REJECT" />   <!--Added by Aditya for tfs bug # 177-->


  </xsl:template>
  <!--Rejected Case of Co_Applicant in remuneration-->

  <xsl:template match="REMUNERATION">
    <xsl:choose>
      <xsl:when test="INPUTXML/REMUNERATIONS/COUNT>0">
        <xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>

      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template match="REINSURANCE">
    <xsl:choose>
      <xsl:when test="INPUTXML/REINSURANCES/COUNT>0">
        <xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  
  <xsl:template match="INSTALLMENT_AMOUNT_SUM"> <!--Added by Aditya on 30-08-2011 for TFS Bug # 478-->
    <xsl:choose>
      <xsl:when test="INPUTXML/BILLING_INFOS/COUNT>0">
        <xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template match="BILLING_INFO">
    <xsl:choose>
      <xsl:when test="INPUTXML/BILLING_INFOS/COUNT>0">
        <xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template match="RISK">
    <xsl:choose>
      <xsl:when test="INPUTXML/RISKS/COUNT>0">
        <xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template match="COINSURANCE">
    <xsl:choose>
      <xsl:when test="INPUTXML/RISKS/COUNT>0">
        <xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template match="REINSURANCE">  <!--Added by Aditya for tfs bug # 180-->
    <xsl:choose>
      <xsl:when test="user:IsApplicationAcceptable(1) = 0"></xsl:when>
      <xsl:when test="user:CheckRefer(1) = 0">
        <xsl:if test="IS_DISREGARD_RI_CONTRACT='Y'">
          <xsl:if test="user:RemumsgSt() = 0">
            <xsl:call-template name="REINSURANCES_HEADER"></xsl:call-template>
            <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
          </xsl:if>
        </xsl:if>
        <xsl:if test="user:SetCoverageMsg(1) = 1">
          <xsl:call-template name="REINSURANCE_MSG"></xsl:call-template>
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
      </xsl:otherwise>      
    </xsl:choose>
  </xsl:template>
  
  
  <!--itrack # 1329.-->
  <!--<xsl:template match="COVERAGE">
    <xsl:choose>
      <xsl:when test="user:IsApplicationAcceptable(1) = 0"></xsl:when>
      <xsl:when test="user:CheckRefer(1) = 0">
        <xsl:if test="TIV='Y'">
          <xsl:if test="user:RemumsgSt() = 0">
          <xsl:call-template name="COVERAGES_HEADER"></xsl:call-template>
          <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
          </xsl:if>
        </xsl:if>
        <xsl:if test="user:SetCoverageMsg(1) = 1">
          <xsl:call-template name="COVERAGES_MSG"></xsl:call-template>
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        
      </xsl:otherwise>
      --><!--
      commented vy Lalit there is no reject rule for covrages
      <xsl:when test="INPUTXML/COVERAGES/COUNT>0">
        <xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
      </xsl:otherwise>--><!--
    </xsl:choose>
  </xsl:template>-->   <!--Commented by Aditya For itrack # 1532,this msg has been shifted to confirmation box-->
  <!--Reject for remuneration--> 
  
  <xsl:template name="RJ_OF_CO_APP">
    <xsl:choose>
      <xsl:when test="INPUTXML/REMUNERATIONS/REMUNERATION/CO_APPLICANT = 'Y'">
        <xsl:if test="user:IsApplicationAcceptable(0) = 0">
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="COUNT_LEADER1">
    <xsl:choose>
      <xsl:when test="INPUTXML/REMUNERATIONS/REMUNERATION/COUNT_LEADER = 'Y'">
        <xsl:if test="user:IsApplicationAcceptable(0) = 0">
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="RJ_OF_COMMISSION">
    <xsl:choose>
      <xsl:when test="INPUTXML/REMUNERATIONS/REMUNERATION/REMU_COMMISSION_PERCENT = 'Y'">
        <xsl:if test="user:IsApplicationAcceptable(0) = 0">
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="RJ_OF_FEE_PERCENT">
    <xsl:choose>
      <xsl:when test="INPUTXML/REMUNERATIONS/REMUNERATION/REMU_FEES_PERCENT = 'Y'">
        <xsl:if test="user:IsApplicationAcceptable(0) = 0">
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="RJ_OF_PRO_LABOUR">
    <xsl:choose>
      <xsl:when test="INPUTXML/REMUNERATIONS/REMUNERATION/REMU_PRO_LABORE_PERCENT = 'Y'">
        <xsl:if test="user:IsApplicationAcceptable(0) = 0">
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="POLICY_COMMISSION_PERCENT">
    <xsl:choose>
      <xsl:when test="INPUTXML/REMUNERATIONS/REMUNERATION/POL_COMMISSION_PERCENT = 'Y'">
        <xsl:if test="user:IsApplicationAcceptable(0) = 0">
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="POLICY_FEES_PERCENT">
    <xsl:choose>
      <xsl:when test="INPUTXML/REMUNERATIONS/REMUNERATION/POL_FEES_PERCENT = 'Y'">
        <xsl:if test="user:IsApplicationAcceptable(0) = 0">
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="POLICY_PRO_LABORE_PERCENT">
    <xsl:choose>
      <xsl:when test="INPUTXML/REMUNERATIONS/REMUNERATION/POL_PRO_LABORE_PERCENT = 'Y'">
        <xsl:if test="user:IsApplicationAcceptable(0) = 0">
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="APPLICATION_AGENCY_TERMINATED">
    <xsl:choose>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/TERMINATION_DATE = 'Y'">
        <xsl:if test="user:IsApplicationAcceptable(0) = 0">
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="SUM_OF_LIMIT">
    <xsl:choose>
      <xsl:when test="INPUTXML/RISKS/RISK/MAXIMUM_LIMIT = 'Y'">
        <xsl:if test="user:CheckRefer(0) = 0">
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="BASIC_SUM_LIMIT">
    <xsl:choose>
      <xsl:when test="INPUTXML/RISKS/RISK/VALUE_AT_RISK = 'Y'">
        <xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="COUNT_LEADER_FOLLWER">
    <xsl:choose>
      <xsl:when test="INPUTXML/COINSURANCES/COINSURANCE/LEADER_COUNT = 'Y'">
        <xsl:if test="user:IsApplicationAcceptable(0) = 0">
        </xsl:if>
      </xsl:when>
      <xsl:when test="INPUTXML/COINSURANCES/COINSURANCE/FOLLOWER_COUNT = 'Y'">
        <xsl:if test="user:IsApplicationAcceptable(0) = 0">
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="COUNT_IS_MAIN">
    <xsl:choose>
      <xsl:when test="INPUTXML/RISKS/RISK/COUNT_ISMAIN = 'Y'">
        <xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="SUBCOVERAGE_SI_SUM">
    <xsl:choose>
      <xsl:when test="INPUTXML/RISKS/RISK/SUB_COV_SI_SUM = 'Y'">
        <xsl:if test="user:IsApplicationAcceptable(0) = 0">
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template> 
  <xsl:template name="SUBCOVERAGE_WP_SUM">
    <xsl:choose>
      <xsl:when test="INPUTXML/RISKS/RISK/SUB_COV_WP_SUM = 'Y'">
        <xsl:if test="user:IsApplicationAcceptable(0) = 0">
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="REINSURANCE_CEDED_REJECT">   <!--Added by Aditya for tfs bug # 177-->
    <xsl:choose>
      <xsl:when test="INPUTXML/REINSURANCES/REINSURANCE/REINSURANCE_CEDED_SUM = 'Y'">
        <xsl:if test="user:IsApplicationAcceptable(0) = 0">
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  
  
  <xsl:template name="INSTALLMENT_AMOUNT_SUM"> <!--Added by Aditya on 30-08-2011 for TFS Bug # 478-->
    <xsl:choose>
      <xsl:when test="INPUTXML/BILLING_INFOS/BILLING_INFO/INSTALLMENT_AMOUNT = 'Y'">
        <xsl:if test="user:IsApplicationAcceptable(0) = 0">
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  
 
  <!--<xsl:template match="RISK">-->
  <!--<xsl:choose>
      <xsl:when test="user:IsApplicationAcceptable(1) = 0">
           <xsl:choose>
          <xsl:when test="SUB_COV_SI_SUM = 'Y'">
            -->
  <!--<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>-->
  <!--
            <xsl:if test="user:RemumsgSt() = 0">
              <xsl:call-template name="REJECT_RISK_MSG" />
              <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
            </xsl:if>
          </xsl:when>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>-->
  <!--</xsl:template>-->

  <!--<xsl:template name="COUNT_FOLLOWER">
    <xsl:choose>
      <xsl:when test="INPUTXML/COINSURANCES/COINSURANCE/FOLLOWER_COUNT = 'Y'">
        <xsl:if test="user:IsApplicationAcceptable(0) = 0">
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>-->
  <!--Message Template-->

  <xsl:template match="INPUTXML/REMUNERATIONS/REMUNERATION">
    <xsl:choose>
      <xsl:when test="user:IsApplicationAcceptable(1) = 0">
        <xsl:choose>
          <xsl:when test="CO_APPLICANT = 'Y'">
            <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
            <xsl:if test="user:RemumsgSt() = 0">
              <xsl:call-template name="REMUNERATION_MSG" />
              <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
            </xsl:if>
          </xsl:when>

          <xsl:when test="COUNT_LEADER = 'Y'">
            <xsl:variable name="CO_APPLICANT_ID" select="CO_APPLICANT_ID"/>
            <xsl:if test="user:ShowApplicant($CO_APPLICANT_ID) = 1" >
              <xsl:if test="user:RemumsgSt(0)"></xsl:if>
            </xsl:if>
            <xsl:if test="user:RemumsgSt() = 0">
              <xsl:if test="TRANSACTION_TYPE='14560'">
                <tr>
                  <td  class="midcolora">
                    <b>
                      <xsl:value-of select="$MyDoc//message[@messageid='140']"></xsl:value-of>

                      <!--FOR CO_APPLICANT :-->

                    </b>
                    <xsl:value-of select="CO_APPLICANT_NAME"/>
                  </td>
                </tr>
              </xsl:if>
            </xsl:if>
            <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
            <xsl:if test="user:RemumsgSt() = 0">
              <xsl:call-template name="REMUNERATION_MSG" />
              <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
            </xsl:if>
          </xsl:when>

          <xsl:when test="REMU_COMMISSION_PERCENT = 'Y' or REMU_FEES_PERCENT  = 'Y' or REMU_PRO_LABORE_PERCENT  = 'Y' or POL_COMMISSION_PERCENT = 'Y' or POL_FEES_PERCENT = 'Y' or POL_PRO_LABORE_PERCENT = 'Y' " >

            <xsl:variable name="CO_APPLICANT_ID" select="CO_APPLICANT_ID"/>
            <xsl:if test="user:ShowApplicant($CO_APPLICANT_ID) = 1" >
              <xsl:if test="user:RemumsgSt(0)"></xsl:if>
            </xsl:if>
            <xsl:if test="user:RemumsgSt() = 0">
              <tr>
                <td  class="midcolora">
                  <b>
                    <xsl:value-of select="$MyDoc//message[@messageid='140']"></xsl:value-of>
                    <!--FOR CO_APPLICANT :-->
                  </b>
                  <xsl:value-of select="CO_APPLICANT_NAME"/>
                </td>
              </tr>
            </xsl:if>
            <xsl:choose>

              <xsl:when test="REMU_COMMISSION_PERCENT = 'Y' " >
                <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
                <xsl:if test="user:RemumsgSt() = 0">
                  <xsl:call-template name="REMUNERATION_MSG" />
                  <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
                </xsl:if>
              </xsl:when>
              <xsl:when test="REMU_FEES_PERCENT  = 'Y' " >
                <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
                <xsl:if test="user:RemumsgSt() = 0">
                  <xsl:call-template name="REMUNERATION_MSG" />
                  <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
                </xsl:if>
              </xsl:when>
              <xsl:when test="REMU_PRO_LABORE_PERCENT  = 'Y' " >
                <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
                <xsl:if test="user:RemumsgSt() = 0">
                  <xsl:call-template name="REMUNERATION_MSG" />
                  <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
                </xsl:if>
              </xsl:when>
              <xsl:when test="POL_COMMISSION_PERCENT  = 'Y' " >
                <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
                <xsl:if test="user:RemumsgSt() = 0">
                  <xsl:call-template name="REMUNERATION_MSG" />
                  <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
                </xsl:if>
              </xsl:when>
              <xsl:when test="POL_PRO_LABORE_PERCENT  = 'Y' " >
                <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
                <xsl:if test="user:RemumsgSt() = 0">
                  <xsl:call-template name="REMUNERATION_MSG" />
                  <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
                </xsl:if>
              </xsl:when>
              <xsl:when test="POL_FEES_PERCENT  = 'Y' " >
                <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
                <xsl:if test="user:RemumsgSt() = 0">
                  <xsl:call-template name="REMUNERATION_MSG" />
                  <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
                </xsl:if>
              </xsl:when>

            </xsl:choose>
          </xsl:when>
        </xsl:choose>
      </xsl:when>
      <xsl:when test="user:CheckRefer(1) = 0 ">
        <xsl:call-template name="REMUNERATION_REFFER">
        </xsl:call-template>

        <xsl:if  test="user:CheckRefer(0) = 0 "></xsl:if>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  

  <!--<xsl:template match="INPUTXML/RISKS/RISK/MAXIMUM_LIMIT">

     
    -->
  <!--<xsl:choose>
      <xsl:when test="user:IsApplicationAcceptable(1) = 0">
        <xsl:choose>
          <xsl:when test="MAXIMUM_LIMIT = 'Y'">FOR RISK
            <xsl:value-of select="RISK_DESC"/>
            -->
  <!--<xsl:if test="user:RemumsgSt() = 0">-->
  <!--
              <xsl:call-template name="RISK_MSG" />
              -->
  <!--<xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>-->
  <!--
            -->
  <!--</xsl:if>-->
  <!--
          </xsl:when>
          <xsl:when test="BASIC_COVERAGE_SI = 'Y'">
            <xsl:if test="user:RemumsgSt() = 0">
              <xsl:call-template name="RISK_MSG" />
              <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
            </xsl:if>
          </xsl:when>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>-->
  <!--
  </xsl:template>-->
 

  <xsl:template match="INPUTXML/COINSURANCES/COINSURANCE">
    <xsl:choose>
      <xsl:when test="user:IsApplicationAcceptable(1) = 0">
        <xsl:choose>
          <xsl:when test="LEADER_COUNT = 'Y'">
            <xsl:if test="user:RemumsgSt() = 0">
              <xsl:call-template name="COINSURANCE_MSG" />
              <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
            </xsl:if>
          </xsl:when>
          <xsl:when test="FOLLOWER_COUNT = 'Y'">
            <xsl:if test="user:RemumsgSt() = 0">
              <xsl:call-template name="COINSURANCE_MSG" />
              <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
            </xsl:if>
          </xsl:when>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template match="INPUTXML/RISKS/RISK">
    <xsl:choose>
      <xsl:when test="user:IsApplicationAcceptable(1) = 0 ">

        <xsl:if test="SUB_COV_SI_SUM = 'Y' or SUB_COV_WP_SUM = 'Y' ">


          <tr>
            <td class="pageheader">
              <b>
                <xsl:value-of select="$MyDoc//message[@messageid='127']"/>
                <xsl:value-of select="RISK_DESC"/>
              </b>
            </td>
          </tr>
        </xsl:if>

        <xsl:if test="SUB_COV_SI_SUM = 'Y' ">
          <tr>
            <td class="midcolora">
              <xsl:if test="user:RemumsgSt() = 0">
                <xsl:call-template name="REJECT_RISK_MSG" />
                <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
              </xsl:if>
              <!--Main coverage SI is not equal to sum of all sub coverages SI-->
            </td>
          </tr>
        </xsl:if>

        <xsl:if test="SUB_COV_WP_SUM = 'Y' ">
          <tr>
            <td class="midcolora">
              <xsl:if test="user:RemumsgSt() = 0">
                <xsl:call-template name="REJECT_RISK_MSG" />
                <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
              </xsl:if>
              <!--Main coverage SI is not equal to sum of all sub coverages SI-->
            </td>
          </tr>
        </xsl:if>


      </xsl:when>
      <xsl:when test="user:CheckRefer(1) = 0">
        <xsl:if test="COUNT_ISMAIN = 'Y' or MAXIMUM_LIMIT = 'Y' or VALUE_AT_RISK = 'Y' ">
          <tr>
            <td class="pageheader">
              <b>
                <xsl:value-of select="$MyDoc//message[@messageid='127']"/>
                <xsl:value-of select="RISK_DESC"/>
              </b>
            </td>
          </tr>
        </xsl:if>
        <xsl:if test="COUNT_ISMAIN = 'Y'">
          <xsl:call-template name="COVERAGE_MSG" />
        </xsl:if>
        <xsl:if test="MAXIMUM_LIMIT = 'Y'  or VALUE_AT_RISK = 'Y' ">
          <xsl:call-template name="RISK_MSG" />
        </xsl:if>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  
  <xsl:template match="INPUTXML/REINSURANCES/REINSURANCE">   <!--Added by Aditya for tfs bug # 177-->
    <xsl:choose>
      <xsl:when test="user:IsApplicationAcceptable(1) = 0">
        <xsl:choose>
          <xsl:when test="REINSURANCE_CEDED_SUM = 'Y'">            
            <xsl:if test="user:RemumsgSt() = 0">
              <xsl:call-template name="REINSURANCE_REJECT_MSG" />
              <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
            </xsl:if>
          </xsl:when>         
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <!--<xsl:template match="INPUTXML/COINSURANCES/COINSURANCE">
    <xsl:choose>
      <xsl:when test="user:IsApplicationAcceptable(1) = 0">
        <xsl:choose>
          <xsl:when test="FOLLOWER_COUNT = 'Y'">
            <xsl:if test="user:RemumsgSt() = 0">
              <xsl:call-template name="COINSURANCE_MSG" />
              <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
            </xsl:if>
          </xsl:when>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
  </xsl:template>-->
  <xsl:template name="REMUNERATION_MSG">
    <xsl:if  test="CO_APPLICANT = 'Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='110']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if  test="COUNT_LEADER = 'Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='133']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if  test="REMU_COMMISSION_PERCENT = 'Y' and CO_APPLICANT != 'Y' " >
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='137']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if  test="REMU_FEES_PERCENT = 'Y' and CO_APPLICANT != 'Y' ">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='138']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if  test="REMU_PRO_LABORE_PERCENT = 'Y' and CO_APPLICANT != 'Y' ">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='139']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if  test="COMMISSION_PERCENT = 'Y' and COMMISSION_TYPE='43'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='130']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if  test="POL_COMMISSION_PERCENT = 'Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='191']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if  test="POL_PRO_LABORE_PERCENT = 'Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='193']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if  test="POL_FEES_PERCENT = 'Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='192']"/>
        </td>
      </tr>
    </xsl:if>

  </xsl:template>

  <xsl:template name="AGENCY_TERMINATED_MSG">
    <xsl:if  test="TERMINATION_DATE='Y'">
      <tr>
        <td class="midcolora">
          <b>
            <xsl:value-of select="$MyDoc//message[@messageid='217']"></xsl:value-of>

          </b>
        </td>
      </tr>
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='216']"/>
        </td>
      </tr>
    </xsl:if>

  </xsl:template>
  <xsl:template name="REJECT_RISK_MSG">
    <xsl:if  test="SUB_COV_SI_SUM = 'Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='188']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if  test="SUB_COV_WP_SUM = 'Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='212']"/>
        </td>
      </tr>
    </xsl:if>
  </xsl:template>
  <!--Start Rejected Templates -->
  <xsl:template name="BILLING_INFOS">
    <xsl:choose>
      <xsl:when test="INPUTXML/BILLING_INFOS/BILLING_INFO/TOTAL_PREMIUM = 'Y'">
        <xsl:if test="user:CheckRefer(0) = 0">
        </xsl:if>
      </xsl:when>
      <xsl:when test="INPUTXML/BILLING_INFOS/BILLING_INFO/TOTAL_TRAN_PREMIUM = 'Y'">
        <xsl:if test="user:CheckRefer(0) = 0">
        </xsl:if>
      </xsl:when>
      <xsl:when test="INPUTXML/BILLING_INFOS/BILLING_INFO/POLICY_DUE_DATE = 'Y'">
        <xsl:if test="user:CheckRefer(0) = 0">
        </xsl:if>
      </xsl:when>
      <xsl:when test="INPUTXML/BILLING_INFOS/BILLING_INFO/ADDITIONAL_ENDORSEMENT = 'Y'">
        <xsl:if test="user:CheckRefer(0) = 0">
        </xsl:if>
      </xsl:when>
      <xsl:when test="INPUTXML/BILLING_INFOS/BILLING_INFO/REFUND_ENDORSEMENT = 'Y'">
        <xsl:if test="user:CheckRefer(0) = 0">
        </xsl:if>
      </xsl:when>    
      
      <xsl:otherwise>
        <xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="RISK_MSG">
    <xsl:if  test="MAXIMUM_LIMIT='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='112']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if  test="VALUE_AT_RISK = 'Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='113']"/>
        </td>
      </tr>
    </xsl:if>
  </xsl:template>

  <xsl:template name="COINSURANCE_MSG">
    <xsl:if  test="LEADER_COUNT='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='111']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if  test="FOLLOWER_COUNT='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='114']"/>
        </td>
      </tr>
    </xsl:if>

  </xsl:template>
  <xsl:template name="COVERAGE_MSG">
    <xsl:if  test="COUNT_ISMAIN='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='136']"/>
        </td>
      </tr>
    </xsl:if>
  </xsl:template>
  <!--Rejected Info-->
  <xsl:template match="INPUTXML/BILLING_INFOS/BILLING_INFO">
    <xsl:choose>
      <xsl:when test="user:CheckRefer(1) = 0">
        <xsl:choose>
          <xsl:when test="TOTAL_PREMIUM = 'Y'">
            <xsl:call-template name="BILLING_INFO_COVG" />
          </xsl:when>
          <xsl:when test="TOTAL_TRAN_PREMIUM = 'Y'">
            <xsl:call-template name="BILLING_INFO_COVG" />
          </xsl:when>
          <xsl:when test="POLICY_DUE_DATE = 'Y'">
            <xsl:call-template name="BILLING_INFO_COVG" />
          </xsl:when>
          <xsl:when test="ADDITIONAL_ENDORSEMENT = 'Y'">
            <xsl:call-template name="BILLING_INFO_COVG" />
          </xsl:when>
          <xsl:when test="REFUND_ENDORSEMENT = 'Y'">
            <xsl:call-template name="BILLING_INFO_COVG" />
          </xsl:when>

        </xsl:choose>
      </xsl:when>
      <xsl:when test="user:IsApplicationAcceptable(1) = 0"> <!--Added by Aditya on 30-08-2011 for TFS Bug # 478-->
        <xsl:choose>
          <xsl:when test="INSTALLMENT_AMOUNT = 'Y'">
            <xsl:call-template name="BILLING_INFO_COVG" />
          </xsl:when>         
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="IS_RJ_APP1">
    <xsl:choose>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/Policy_Currency='Y'">
        <xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="IS_RJ_INACTIVE_APPLICATION">
    <xsl:choose>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/INACTIVE_APPLICATION='Y'">
        <xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="IS_RJ_INACTIVE_AGENCY">
    <xsl:choose>
      <xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/INACTIVE_AGENCY='Y'">
        <xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!--Commented By Lalit August 09 ,2010-->
  <!--REJECTED TEMPLATES-->
  <!--<xsl:template name="IS_APPLICATION_REJECTED">
    <xsl:choose>
      <xsl:when test="INPUTXML/APPLICATIONS/POL_BILLINGINFO/TOTAL_TRAN_PREMIUM = 'Y'">
        <xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template match="BILLING_INFOS_COVG">
    <xsl:choose>
      <xsl:when test="INPUTXML/BILLING_INFOS/COUNT > 0">
        <xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>-->
  <!-- =========================End Rejected Templates ======================================== -->
  <xsl:template name="PRODUCT_REFERRED_CASES">
    <!--Added By Lait For reffered rule,i-track # 962-->
    <xsl:call-template name="BILLING_INFOS" />
    <xsl:call-template name="REMUNERATIONS_REFFERED" />
    <xsl:call-template name="ENDORSEMENT_REMUNERATIONS_REFFERED" />
    <xsl:call-template name="APPLICATIONS_REFFERED" />
    <xsl:call-template name="APPLICANTS_REFFERED" />
    <xsl:call-template name="REINSURANCES_REFFERED" />  <!--Added by Aditya for tfs bug # 180-->
    <!--<xsl:call-template name="COVERAGES_REFFERED" />-->  <!--Commented by Aditya For itrack # 1532,this msg has been shifted to confirmation box-->
  </xsl:template>
  <!-- Application Information  -->
  <xsl:template match="INPUTXML/APPLICATIONS/APPLICATION">
    <xsl:choose>
      <!-- Rejected  Info -->
      <xsl:when test="user:IsApplicationAcceptable(1) = 0">
        <xsl:choose>
          <xsl:when test="LOSS_AMT_EXCEED='Y' or 
                    INACTIVE_APPLICATION='Y' or 
                    INACTIVE_AGENCY='Y' or 
                    POLICY_CURRENCY ='Y' or 
                    INPUTXML/BILLING_INFOS/BILLING_INFO/TOTAL_PREMIUM !='Y' or 
                    POLICY_DUE_DATE = 'Y' or
                    TERMINATION_DATE = 'Y' "
                    >
            <!-- INPUTXML/BILLING_INFOS/BILLING_INFO/TOTAL_PREMIUM ='Y'-->
            <!--<xsl:call-template name="LOSS_AMT_EXCEED" />-->
            <xsl:call-template name="INACTIVE_APPLICATION" />
            <xsl:call-template name="INACTIVE_AGENCY" />
            <xsl:call-template name="AGENCY_TERMINATED_MSG" />
          </xsl:when>
        </xsl:choose>
      </xsl:when>
      <xsl:when test="user:CheckRefer(1) = 0">
        <xsl:call-template name="APPLICATIONS_MESSAGE"></xsl:call-template>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="BILLING_INFO_COVG">
    <xsl:choose>
      <xsl:when test="TOTAL_PREMIUM ='Y' or TOTAL_TRAN_PREMIUM='Y' ">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='90']"/>
          </td>
          <!--Coverages Total Premium is not equal to Total Policy Premium-->
        </tr>
      </xsl:when>
      <xsl:when test="POLICY_DUE_DATE='Y' ">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='190']"/>
          </td>
          <!--Coverages Total Premium is not equal to Total Policy Premium-->
        </tr>
      </xsl:when>
      <xsl:when test="ADDITIONAL_ENDORSEMENT='Y' ">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='195']"/>
          </td>
          <!--Coverages Total Premium is not equal to Total Policy Premium-->
        </tr>
      </xsl:when>
      <xsl:when test="REFUND_ENDORSEMENT='Y' ">
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='196']"/>
          </td>
          <!--Coverages Total Premium is not equal to Total Policy Premium-->
        </tr>
      </xsl:when>
      <xsl:when test="INSTALLMENT_AMOUNT='Y' "> <!--Added by Aditya on 30-08-2011 for TFS Bug # 478-->
        <tr>
          <td class="midcolora">
            <xsl:value-of select="$MyDoc//message[@messageid='232']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="REINSURANCE_REJECT_MSG">   <!--Added by Aditya for tfs bug # 177-->
    <xsl:if  test="REINSURANCE_CEDED_SUM='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='235']"/>
        </td>
      </tr>
    </xsl:if>
  </xsl:template>

  <xsl:template name="DISCOUNT">
    <xsl:choose>
      <xsl:when test="DISCOUNT='Y'">
        <TR>
          <td class="midcolora">Discount is not Eligible.</td>
        </TR>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="PRIOR_LOSS_Y">
    <xsl:choose>
      <xsl:when test="PRIOR_LOSS_Y='Y'">
        <TR>
          <td class="midcolora">Prior Losses occurred but no information provided.</td>
        </TR>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="PRIOR_LOSS_N">
    <xsl:choose>
      <xsl:when test="PRIOR_LOSS_N='Y'">
        <TR>
          <td class="midcolora">Prior Losses not occurred but information provided.</td>
        </TR>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="INACTIVE_APPLICATION">
    <xsl:if test="INACTIVE_APPLICATION='Y'">
      <TR>
        <td class="midcolora">This Customer is Inactive.</td>
      </TR>
    </xsl:if>
  </xsl:template>
  <xsl:template name="INACTIVE_AGENCY">
    <xsl:if test="INACTIVE_AGENCY='Y'">
      <TR>
        <td class="midcolora">Selected Agency is Inactive.</td>
      </TR>
    </xsl:if>
  </xsl:template>

  <xsl:template name="REMUNERATIONS_REFFERED">
    <xsl:choose>
      <xsl:when test="INPUTXML/REMUNERATIONS/@COUNT>0">
        <xsl:choose>
          <xsl:when test="INPUTXML/REMUNERATIONS/REMUNERATION/COMMISSION_PERCENT ='Y' ">
            <xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
          </xsl:when>
          <!--<xsl:when test="INPUTXML/REMUNERATIONS/REMUNERATION/DELETE_BROKER_NBS ='Y' "> Commented by Aditya for itrack # 1282
            <xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
          </xsl:when>-->
        </xsl:choose>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>

  </xsl:template>
  <xsl:template name="REMUNERATION_REFFER">
    <xsl:choose>
      <xsl:when test="COMMISSION_TYPE='43' and COMMISSION_PERCENT ='Y' ">
        <tr>
          <td class="pageheader">
            <xsl:value-of select="$MyDoc//message[@messageid='121']"/>
            <xsl:value-of select="MNT_AGENCY_LIST/AGENCY_DISPLAY_NAME"/>
            <xsl:if test="TRANSACTION_TYPE='14560'">
              <tr>
                <td  class="midcolora">
                  <b>
                    <xsl:value-of select="$MyDoc//message[@messageid='140']"></xsl:value-of>
                  </b>
                  <xsl:value-of select="CO_APPLICANT_NAME"/>
                </td>
              </tr>
            </xsl:if>
          </td>
        </tr>
        <xsl:call-template name="COMMISSION_PERCENT_MESSAGE"/>
      </xsl:when>
      <xsl:when test="COMMISSION_TYPE='44' and COMMISSION_PERCENT ='Y' ">
        <tr>
          <td class="pageheader">
            <xsl:value-of select="$MyDoc//message[@messageid='121']"/>
            <xsl:value-of select="MNT_AGENCY_LIST/AGENCY_DISPLAY_NAME"/>
            <xsl:if test="TRANSACTION_TYPE='14560'">
              <tr>
                <td  class="midcolora">
                  <b>
                    <xsl:value-of select="$MyDoc//message[@messageid='140']"></xsl:value-of>
                  </b>
                  <xsl:value-of select="CO_APPLICANT_NAME"/>
                </td>
              </tr>
            </xsl:if>
          </td>
        </tr>
        <xsl:call-template name="COMMISSION_PERCENT_MESSAGE"/>
      </xsl:when>
      <xsl:when test="COMMISSION_TYPE='45' and COMMISSION_PERCENT ='Y' ">
        <tr>
          <td class="pageheader">
            <xsl:value-of select="$MyDoc//message[@messageid='121']"/>
            <xsl:value-of select="MNT_AGENCY_LIST/AGENCY_DISPLAY_NAME"/>
            <xsl:if test="TRANSACTION_TYPE='14560'">
              <tr>
                <td  class="midcolora">
                  <b>
                    <xsl:value-of select="$MyDoc//message[@messageid='140']"></xsl:value-of>
                  </b>
                  <xsl:value-of select="CO_APPLICANT_NAME"/>
                </td>
              </tr>
            </xsl:if>
          </td>
        </tr>
        <xsl:call-template name="COMMISSION_PERCENT_MESSAGE"/>
      </xsl:when>
      <xsl:when test="COMMISSION_PERCENT ='Y' and POLICY_LEVEL_COMM_APPLIES='Y' ">
        <tr>
          <td class="pageheader">
            <xsl:value-of select="$MyDoc//message[@messageid='121']"/>
            <xsl:value-of select="MNT_AGENCY_LIST/AGENCY_DISPLAY_NAME"/>
            <xsl:if test="TRANSACTION_TYPE='14560'">
              <tr>
                <td  class="midcolora">
                  <b>
                    <xsl:value-of select="$MyDoc//message[@messageid='140']"></xsl:value-of>
                  </b>
                  <xsl:value-of select="CO_APPLICANT_NAME"/>
                </td>
              </tr>
            </xsl:if>
          </td>
        </tr>
        <xsl:call-template name="COMMISSION_PERCENT_MESSAGE"/>
      </xsl:when>
      <!--<xsl:when test="DELETE_BROKER_NBS ='Y' "> Commented by Aditya for itrack # 1282
        <xsl:if test="user:RemumsgSt() = 0">
          <tr>
            <td class="pageheader">
              <xsl:value-of select="$MyDoc//message[@messageid='121']"/>
              <xsl:value-of select="MNT_AGENCY_LIST/AGENCY_DISPLAY_NAME"/>
            </td>
          </tr>
          <xsl:call-template name="COMMISSION_PERCENT_MESSAGE"/>
          <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
        </xsl:if>
      </xsl:when>-->
      <xsl:otherwise>
        <xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="COMMISSION_PERCENT_MESSAGE">

    <xsl:if test="COMMISSION_TYPE='43' and COMMISSION_PERCENT ='Y' ">
      <tr>
        <td class="midcolora">
          <xsl:choose >
            <xsl:when  test="POLICY_LEVEL_COMM_APPLIES ='Y'">
              <xsl:value-of select="$MyDoc//message[@messageid='130']"/>
            </xsl:when>
            <xsl:otherwise>
              <xsl:value-of select="$MyDoc//message[@messageid='194']"/>
            </xsl:otherwise>
          </xsl:choose>
          <!--<xsl:value-of select="$MyDoc//message[@messageid='134']"/>-->
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="COMMISSION_TYPE='44' and COMMISSION_PERCENT ='Y' ">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='131']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="COMMISSION_TYPE='45' and COMMISSION_PERCENT ='Y' ">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='132']"/>
        </td>
      </tr>
    </xsl:if>
    <!--<xsl:if test="DELETE_BROKER_NBS ='Y' "> Commented by Aditya for itrack # 1282
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='220']"/>
        </td>
      </tr>
    </xsl:if>-->

    <!--<xsl:if test="COMMISSION_PERCENT ='Y' and POLICY_LEVEL_COMM_APPLIES='Y' ">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='130']"/>
        </td>
      </tr>
    </xsl:if>-->
  </xsl:template>

  <xsl:template name="ENDORSEMENT_REMUNERATIONS_REFFERED">
    <xsl:choose>
      <xsl:when test="INPUTXML/ENDORSEMENT_REMUNERATIONS/@COUNT>0">
        <xsl:for-each select="INPUTXML/ENDORSEMENT_REMUNERATIONS/REMU">
          <xsl:call-template name="ENDORSEMENT_REMUNERATION_REFFER"></xsl:call-template >
        </xsl:for-each>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="ENDORSEMENT_REMUNERATION_REFFER">
    <xsl:choose>
      <xsl:when test="IS_COMMISSION_CHANGED = 'Y' or PREV_BROKER_COUNT='Y' or DELETE_BROKER_NBS='Y'"> <!--Added by aditya for itrack # 1282-->
        <xsl:if test="user:CheckRefer(0)=0">
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:CheckRefer(1)=0" />
      </xsl:otherwise>
    </xsl:choose>
    <!--</xsl:when>
    </xsl:choose>-->
  </xsl:template>
  <xsl:template match="REMU">
    <xsl:choose>
      <xsl:when test="IS_COMMISSION_CHANGED='Y' or PREV_BROKER_COUNT='Y' or DELETE_BROKER_NBS='Y'"> <!--Added by aditya for itrack # 1282-->
        <tr>
          <td class="pageheader">
            <xsl:if test="user:RemumsgSt() = 0">
              <xsl:value-of select="$MyDoc//message[@messageid='121']"/>
              <xsl:if test="user:RemumsgSt(1) = 1"></xsl:if>
            </xsl:if>
            <xsl:value-of select="MNT_AGENCY_LIST/AGENCY_DISPLAY_NAME"/>
          </td>
        </tr>
        <tr>
          <td class="pageheader">
            <xsl:value-of select="$MyDoc//message[@messageid='223']"/>
            <xsl:value-of select="AGENCY_NAME"/>
            <xsl:value-of select="MNT_AGENCY_LIST/AGENCY_DISPLAY_NAME"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
    <xsl:if test="IS_COMMISSION_CHANGED ='Y' ">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='221']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="PREV_BROKER_COUNT ='Y' ">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='222']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="DELETE_BROKER_NBS ='Y' ">  <!--Added by aditya for itrack # 1282-->
      <tr>
        <td class="pageheader">
          <xsl:value-of select="$MyDoc//message[@messageid='173']"/>
          <xsl:value-of select="CO_APPLICANT_NAME"/>
          <xsl:value-of select="MNT_AGENCY_LIST/CO_APPLICANT_NAME"/>
        </td>
      </tr>         
          <tr>
            <td  class="midcolora"> <!--Added by aditya for itrack # 1282-->
              <xsl:choose >
                <xsl:when  test="AGENCY_TYPE_ID ='14701'">
                  <xsl:value-of select="$MyDoc//message[@messageid='220']"/>
                </xsl:when>
                <xsl:otherwise>
                  <xsl:value-of select="$MyDoc//message[@messageid='233']"/>
                </xsl:otherwise>
              </xsl:choose>    
            </td>
          </tr>
        </xsl:if>     
        <!--<tr>
          <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='220']"/>
        </td>
      </tr>-->         
  </xsl:template>

  <!-- Application_refFered-->
  <xsl:template name="APPLICATIONS_REFFERED">
    <xsl:choose>
      <xsl:when test="INPUTXML/APPLICATIONS/@COUNT>0">
        <xsl:for-each select="INPUTXML/APPLICATIONS/APPLICATION">
          <xsl:call-template name="APPLICATIONS_REFFER"></xsl:call-template >
        </xsl:for-each>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="APPLICATIONS_REFFER">
    <xsl:choose>
      <xsl:when test="CUSTOMER_ZIP='Y' or
                CUSTOMER_ADDRESS1='Y' or
                CUSTOMER_CITY='Y' or
                CUSTOMER_STATE='Y' or
                CUSTOMER_COUNTRY='Y' or
                NUMBER='Y' or
                ORIGINAL_ISSUE='Y' or
                REGIONAL_IDENTIFICATION='Y' or
                MARITAL_STATUS='Y' or
                CREATION_DATE='Y' or
                GENDER='Y' or
                REG_ID_ISSUE='Y' or
                DATE_OF_BIRTH='Y'">


        <xsl:if test="user:CheckRefer(0)=0">
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:CheckRefer(1)=0" />
      </xsl:otherwise>
    </xsl:choose>
    <!-- </table>	-->
  </xsl:template>
  <xsl:template name="APPLICATIONS_MESSAGE">
    <xsl:choose >
      <xsl:when test="CUSTOMER_ZIP='Y' or
                CUSTOMER_ADDRESS1='Y' or
                CUSTOMER_CITY='Y' or
                
                CUSTOMER_COUNTRY='Y' or
                NUMBER='Y' or
                ORIGINAL_ISSUE='Y' or
                REGIONAL_IDENTIFICATION='Y' or
                MARITAL_STATUS='Y' or
                CREATION_DATE='Y' or
                GENDER='Y' or
                REG_ID_ISSUE='Y' or
                DATE_OF_BIRTH='Y'" >
        <tr>
          <td class="pageheader">
            <xsl:value-of select="$MyDoc//message[@messageid='174']"/>
          </td>
        </tr>

      </xsl:when>

    </xsl:choose>




    <xsl:if test="CUSTOMER_ZIP='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='142']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if  test="CUSTOMER_ADDRESS1='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='143']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if  test="CUSTOMER_CITY='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='144']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="CUSTOMER_STATE='Y' and INPUTXML/APPLICATIONS/APPLICATION/LOB_ID !='38'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='145']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="CUSTOMER_COUNTRY='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='146']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if  test="NUMBER='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='147']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="CUSTOMER_DISTRICT='Y' and INPUTXML/APPLICATIONS/APPLICATION/LOB_ID !='38'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='169']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if  test="ORIGINAL_ISSUE='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='148']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if  test="REGIONAL_IDENTIFICATION='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='149']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="MARITAL_STATUS='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='150']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="REG_ID_ISSUE='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='151']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if  test="DATE_OF_BIRTH='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='152']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="CREATION_DATE='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='166']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if  test="GENDER='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='170']"/>
        </td>
      </tr>
    </xsl:if>
  </xsl:template>

  <!-- Applicant information-->
  <!--Applicant screen-->
  <xsl:template name="APPLICANTS_REFFERED">

    <xsl:choose>
      <xsl:when test="INPUTXML/APPLICANTS/@COUNT>0">
        <xsl:for-each select="INPUTXML/APPLICANTS/APPLICANT">
          <xsl:call-template name="APPLICANT_REFFER"></xsl:call-template>
        </xsl:for-each>
      </xsl:when>

    </xsl:choose>
  </xsl:template>
  <xsl:template name="REINSURANCES_REFFERED">  <!--Added by Aditya for tfs bug # 180-->
    <xsl:choose>
      <xsl:when test="INPUTXML/REINSURANCES/@COUNT>0">
        <xsl:for-each select="INPUTXML/REINSURANCES/REINSURANCE">
          <xsl:choose>
            <xsl:when test="IS_DISREGARD_RI_CONTRACT='Y'">
              <xsl:if test="user:CheckRefer(0)=0"></xsl:if>
            </xsl:when>
            <xsl:otherwise>
              <xsl:if test="user:CheckRefer(1)=0" />
            </xsl:otherwise>
          </xsl:choose>
        </xsl:for-each>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <!--itrack # 1329.Added referred rule if TIV > max(LayerAMount) -->
  <!--<xsl:template name="COVERAGES_REFFERED">
    <xsl:choose>
      <xsl:when test="INPUTXML/COVERAGES/@COUNT>0">
        <xsl:for-each select="INPUTXML/COVERAGES/COVERAGE">
          <xsl:choose>
            <xsl:when test="TIV='Y'">
              <xsl:if test="user:CheckRefer(0)=0"></xsl:if>
            </xsl:when>
            <xsl:otherwise>
              <xsl:if test="user:CheckRefer(1)=0" />
            </xsl:otherwise>
          </xsl:choose>
        </xsl:for-each>
      </xsl:when>
    </xsl:choose>
  </xsl:template>-->  <!--Commented by Aditya For itrack # 1532,this msg has been shifted to confirmation box-->
  
  <xsl:template name="APPLICANT_REFFER">
    <xsl:choose>
      <xsl:when test="CO_APP_REG_ID_ISSUE='Y' 
                or CO_APP_ORIGINAL_ISSUE='Y' 
                or CO_APP_REGIONAL_IDENTIFICATION='Y' 
                or CO_APPL_MARITAL_STATUS='Y' 
                or CO_APPL_DOB='Y' 
                or CO_APPL_CREATION_DATE='Y' 
                or CO_APPL_GENDER='Y' 
                or ZIP_CODE='Y' 
                or ADDRESS1='Y' 
                or CITY='Y' 
                or STATE='Y' 
                or COUNTRY='Y' 
                or CO_APP_NUMBER='Y' 
                or CO_APP_DISTRICT='Y' 
                or CURRENT_COMMISSION='Y' 
                or PREV_COMMISSION='Y' 
                or CURRENT_FEES='Y'
                or PREV_FEES='Y' 
                or CURRENT_PRO_LABORE='Y' 
                or PREV_PROLABORE='Y'">

        <xsl:if test="user:CheckRefer(0)=0">
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="user:CheckRefer(1)=0" />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template match="APPLICANT">
    <xsl:choose>

      <xsl:when test="CO_APP_REG_ID_ISSUE='Y' or CO_APP_ORIGINAL_ISSUE='Y' or CO_APP_REGIONAL_IDENTIFICATION='Y' or CO_APPL_MARITAL_STATUS='Y' or CO_APPL_DOB='Y' or CO_APPL_CREATION_DATE='Y' or CO_APPL_GENDER='Y' or
                ZIP_CODE='Y' or ADDRESS1='Y' or CITY='Y' or STATE='Y' or COUNTRY='Y' or CO_APP_NUMBER='Y' or CO_APP_DISTRICT='Y' 
                or CURRENT_COMMISSION='Y' 
                or PREV_COMMISSION='Y' 
                or CURRENT_FEES='Y'
                or PREV_FEES='Y' 
                or CURRENT_PRO_LABORE='Y' 
                or PREV_PROLABORE='Y'">

        <tr>
          <td class="pageheader">
            <b>
              <xsl:value-of select="$MyDoc//message[@messageid='173']"/>
            </b>
            <xsl:value-of select="FIRST_NAME"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>

    <xsl:if test="CO_APP_REG_ID_ISSUE='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='160']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="CO_APP_ORIGINAL_ISSUE='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='161']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="CO_APP_REGIONAL_IDENTIFICATION='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='162']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="CO_APPL_MARITAL_STATUS='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='163']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="CO_APPL_DOB='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='164']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="CO_APPL_CREATION_DATE='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='167']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="CO_APPL_GENDER='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='172']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="ZIP_CODE='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='154']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="ADDRESS1='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='155']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="CITY='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='156']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="STATE='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='157']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="COUNTRY='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='158']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="CO_APP_NUMBER='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='159']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="CO_APP_DISTRICT='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='171']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="PREV_COMMISSION='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='224']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="PREV_FEES='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='225']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="PREV_PROLABORE='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='226']"/>
        </td>
      </tr>
    </xsl:if>

    <xsl:if test="CURRENT_COMMISSION='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='227']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="CURRENT_FEES='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='228']"/>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="CURRENT_PRO_LABORE='Y'">
      <tr>
        <td class="midcolora">
          <xsl:value-of select="$MyDoc//message[@messageid='229']"/>
        </td>
      </tr>
    </xsl:if>
  </xsl:template>

  <!-- Billing Information-->
  <xsl:template name="DFI_ACC_NO_RULE">
    <xsl:if test="DFI_ACC_NO_RULE='Y'">
      <TR>
        <td class="midcolora">Complete DFI Account Number or Transit/Routing Number in Billing Info.</td>
      </TR>
    </xsl:if>
  </xsl:template>
  <!-- Credit Card-->
  <xsl:template name="CREDIT_CARD">
    <xsl:if test="CREDIT_CARD='Y'">
      <tr>
        <td class="midcolora">Complete First Name and Last Name and Card Type and Card CVV/CCV # and Credit Card # and Valid To (Month/Year) in Billing Info.</td>
      </tr>
    </xsl:if>
  </xsl:template>
  <!-- Effective Date-->
  <xsl:template name="APPEFFECTIVEDATE">
    <xsl:if test="APPEFFECTIVEDATE='Y'">
      <tr>
        <td class="midcolora">Effective Date is less than 2000.</td>
      </tr>
    </xsl:if>
  </xsl:template>
  <!-- TOTAL Premium Date-->
  <xsl:template name="TOTAL_PREMIUM_AT_RENEWAL">
    <xsl:if test="TOTAL_PREMIUM_AT_RENEWAL='Y'">
      <tr>
        <td class="midcolora">Balance Due on policy is greater than equal to Past Due at Renewal.</td>
      </tr>
    </xsl:if>
  </xsl:template>
  <xsl:template name="CLAIM_EFFECTIVE">
    <xsl:if test="CLAIM_EFFECTIVE='Y'">
      <tr>
        <td class="midcolora">Policy had claim(s) reported.</td>
      </tr>
    </xsl:if>
  </xsl:template>

  <xsl:template name="REINSURANCES_HEADER">  <!--Added by Aditya for tfs bug # 180-->
    <tr>
      <td class="pageheader">
        <xsl:value-of select="$MyDoc//message[@messageid='123']"/>
      </td>
    </tr>
  </xsl:template>
  <xsl:template name="REINSURANCE_MSG">
    <xsl:if test="IS_DISREGARD_RI_CONTRACT='Y'">
    <tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='234']"/>
      </td>
    </tr>
    </xsl:if>
  </xsl:template>
  
 
  <!--Coverages Referred -->
  <!--Commented by Aditya For itrack # 1532,this msg has been shifted to confirmation box-->
  <!--<xsl:template name="COVERAGES_HEADER">  
    <tr>
      <td class="pageheader">
        <xsl:value-of select="$MyDoc//message[@messageid='231']"/>
      </td>
    </tr>
  </xsl:template>-->
  <!--<xsl:template name="COVERAGES_MSG">
    <xsl:if test="TIV='Y'">
    <tr>
      <td class="midcolora">
        <xsl:value-of select="$MyDoc//message[@messageid='230']"/>
      </td>
    </tr>
    </xsl:if>
  </xsl:template>-->  <!--Commented by Aditya For itrack # 1532,this msg has been shifted to confirmation box-->
  <!--End Reffered Templates -->
</xsl:stylesheet>
