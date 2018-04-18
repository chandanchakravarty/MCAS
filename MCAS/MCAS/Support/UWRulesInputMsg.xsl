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
        <TITLE>Claim Verification Status</TITLE>
      </head>
      <table border="2" align="center" width='90%'>
        <tr>
          <td class="pageheader" width="18%" style="background-color:#C2E0FF;font-weight:bold" >
              <xsl:value-of select="$MyDoc//message[@messageid='1']"/>
          </td>
          <td class="midcolora" width="36%" style="background-color:#C2E0FF;font-weight:bold" >
            <xsl:value-of select="INPUTXML/ACCIDENTS/ACCIDENTDETAIL/ClaimNo" />
          </td>
          <td class="pageheader" width="18%" style="background-color:#C2E0FF;font-weight:bold">
            <xsl:value-of select="$MyDoc//message[@messageid='2']"/>
          </td>
          <td class="midcolora" width="36%" style="background-color:#C2E0FF;font-weight:bold" > 
            <xsl:value-of select="INPUTXML/ACCIDENTS/ACCIDENTDETAIL/AccidentDate" />
          </td>
        </tr>
      </table>
      <table border="1px" align="center" width='100%' cellpadding='0' cellspacing='0'>
        <tr>
            <td class="headereffectcenter" colspan="6" style="color:#FFFFFF;background-color:#f38733;text-align:center;font-size:16pt;margin-top:10px;border:1px solid #c3e4f6;">
              <xsl:value-of select="$MyDoc//message[@messageid='3']"/>
            </td>
        </tr>
        <tr>
          <td class="pageheader" style="background-color:#1e91cf;font-size:12pt;border:1px solid #c3e4f6;">
            <xsl:value-of select="$MyDoc//message[@messageid='4']"/>
          </td>
        </tr>
        <!-- Call for different Messages -->
        <xsl:choose>
          <xsl:when test="1=1">
            <xsl:call-template name="SHOWACCIDENTINPUTDETAIL" />
            
          </xsl:when>
        </xsl:choose>
      </table>
    </html>
  </xsl:template>
  
  <xsl:template name="SHOWACCIDENTINPUTDETAIL">
    <xsl:apply-templates select="INPUTXML/ACCIDENTS/ACCIDENTDETAIL"></xsl:apply-templates>
    <xsl:apply-templates select="INPUTXML/CLAIMS/CLAIMDETAIL"></xsl:apply-templates>
    <xsl:apply-templates select="INPUTXML/THIRDPARTIES/TPDETAIL"></xsl:apply-templates>
    <xsl:apply-templates select="INPUTXML/CLAIMNOTES/NOTESDETAIL"></xsl:apply-templates>
    <xsl:apply-templates select="INPUTXML/CLAIMTRANSACTIONS/TRANSDETAIL"></xsl:apply-templates>
    <xsl:apply-templates select="INPUTXML/CLAIMDIARY/DIARYDETAIL"></xsl:apply-templates>
    <xsl:apply-templates select="INPUTXML/CLAIMS/ERRORS"></xsl:apply-templates>
    <xsl:apply-templates select="INPUTXML/TPARTIES/ERRORS"></xsl:apply-templates>
    <xsl:apply-templates select="INPUTXML/NOTES/ERRORS"></xsl:apply-templates>
    <xsl:apply-templates select="INPUTXML/TRANSACTIONS/ERRORS"></xsl:apply-templates>
    <xsl:apply-templates select="INPUTXML/DIARY/ERRORS"></xsl:apply-templates>
  </xsl:template>
  
  <xsl:template match="INPUTXML/ACCIDENTS/ACCIDENTDETAIL">
    <xsl:choose>
      <xsl:when test="ClaimNo=''or
                 Facts=''or 
                AccidentDate='' or
                IPNo = '' or
                BusServiceNo = '' or
                VehicleNo = '' or
                ReportedDate = '' or
                TPClaimentStatus = '' or
                DutyIO = '' or
                Make='' or
                ModelNo ='' or
                DriverEmployeeNo = '' or
                DriverName = '' or
                DriverNRICNo ='' or
                DriverMobileNo = ''
                ">
        <tr>
          <td class="pageheader" style="background-color:#f8fcfe;font-size:12pt;border:1px solid #c3e4f6;">
            <xsl:value-of select="$MyDoc//message[@messageid='100']"/>
          </td>
        </tr>
        <xsl:call-template name="ClaimNo"/>
        <xsl:call-template name="Facts" />
        <xsl:call-template name="AccidentDate" />
        <xsl:call-template name="IPNo" />
        <xsl:call-template name="BusServiceNo" />
        <xsl:call-template name="VehicleNo" />
        <xsl:call-template name="ReportedDate" />
        <xsl:call-template name="TPClaimentStatus" />
        <xsl:call-template name="DutyIO" />
        <xsl:call-template name="Make" />
        <xsl:call-template name="ModelNo" />
        <xsl:call-template name="DriverEmployeeNo" />
        <xsl:call-template name="DriverName" />
        <xsl:call-template name="DriverNRICNo" />
        <xsl:call-template name="DriverMobileNo" />
      </xsl:when>
      <xsl:otherwise></xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!-- ================================= TEMPLATES =============================================-->
  <xsl:template name="ClaimNo">
    <xsl:choose>
      <xsl:when test="ClaimNo=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='5']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
   </xsl:template>
  
  <xsl:template name="Facts">
    <xsl:choose>
      <xsl:when test="Facts=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='6']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  
  <xsl:template name="AccidentDate">
    <xsl:choose>
      <xsl:when test="AccidentDate=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='7']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="IPNo">
    <xsl:choose>
      <xsl:when test="IPNo=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='11']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="BusServiceNo">
    <xsl:choose>
      <xsl:when test="BusServiceNo=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='12']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="VehicleNo">
    <xsl:choose>
      <xsl:when test="VehicleNo=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='13']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="ReportedDate">
    <xsl:choose>
      <xsl:when test="ReportedDate=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='14']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="TPClaimentStatus">
    <xsl:choose>
      <xsl:when test="TPClaimentStatus=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='15']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="DutyIO">
    <xsl:choose>
      <xsl:when test="DutyIO=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='16']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  
  <xsl:template name="Make">
    <xsl:choose>
      <xsl:when test="Make=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='8']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  
  <xsl:template name="ModelNo">
    <xsl:choose>
      <xsl:when test="ModelNo=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='9']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="DriverEmployeeNo">
    <xsl:choose>
      <xsl:when test="DriverEmployeeNo=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='17']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="DriverName">
    <xsl:choose>
      <xsl:when test="DriverName=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='18']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  
  <xsl:template name="DriverNRICNo">
    <xsl:choose>
      <xsl:when test="DriverNRICNo=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='10']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="DriverMobileNo">
    <xsl:choose>
      <xsl:when test="DriverMobileNo=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='19']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template match="INPUTXML/CLAIMS/CLAIMDETAIL">
    <xsl:choose>
      <xsl:when test="ClaimantName=''or
                ClaimantNRICPPNO=''or
                ClaimantType=''or
                ClaimantAddress1=''or
                ClaimantAddress2=''or
                ClaimantAddress3=''or
                PostalCode=''or
                ClaimantContactNo=''or
                VehicleRegnNo=''or
                ClaimType=''or
                ClaimRecordNo=''or
                TimeBarDate=''or
                 ClaimDate=''or 
                ClaimOfficer='' or
                AccidentCause = '' or
                CaseCategory = '' or
                ClaimantStatus = '' or
                CaseStatus = ''
                ">
        <tr>
          <td class="pageheader" style="background-color:#f8fcfe;font-size:12pt;border:1px solid #c3e4f6;">
            <xsl:value-of select="$MyDoc//message[@messageid='200']"/>
          </td>
        </tr>

        <xsl:call-template name="ClaimantName" />
        <xsl:call-template name="ClaimantNRICPPNO" />
        <xsl:call-template name="ClaimantType" />
        <xsl:call-template name="ClaimantAddress1" />
        <xsl:call-template name="ClaimantAddress2" />
        <xsl:call-template name="ClaimantAddress3" />
        <xsl:call-template name="PostalCode" />
        <xsl:call-template name="ClaimantContactNo" />
        <xsl:call-template name="ClaimType" />
        <xsl:call-template name="VehicleRegnNo" />
        <xsl:call-template name="ClaimantType" />
        <xsl:call-template name="ClaimRecordNo" />
        <xsl:call-template name="TimeBarDate" />
        <xsl:call-template name="ClaimDate" />
        <xsl:call-template name="ClaimOfficer" />
        <xsl:call-template name="AccidentCause" />
        <xsl:call-template name="CaseCategory" />
        <xsl:call-template name="ClaimantStatus" />
        <xsl:call-template name="CaseStatus" />
      </xsl:when>
      <xsl:otherwise></xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!-- ================================= TEMPLATES =============================================-->
  <xsl:template name="ClaimantName">
    <xsl:choose>
      <xsl:when test="ClaimantName=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='46']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="ClaimantNRICPPNO">
    <xsl:choose>
      <xsl:when test="ClaimantNRICPPNO=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='47']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="ClaimantType">
    <xsl:choose>
      <xsl:when test="ClaimantType=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='48']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="ClaimantAddress1">
    <xsl:choose>
      <xsl:when test="ClaimantAddress1=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='49']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="ClaimantAddress2">
    <xsl:choose>
      <xsl:when test="ClaimantAddres2=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='50']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="ClaimantAddress3">
    <xsl:choose>
      <xsl:when test="ClaimantAddress3=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='51']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="PostalCode">
    <xsl:choose>
      <xsl:when test="PostalCode=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='52']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="ClaimantContactNo">
    <xsl:choose>
      <xsl:when test="ClaimantContactNo=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='53']"/>
          </td>
        </tr> 
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  
  <xsl:template name="ClaimType">
    <xsl:choose>
      <xsl:when test="ClaimType=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='20']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  
  <xsl:template name="VehicleRegnNo">
  <xsl:choose>
    <xsl:when test="VehicleRegnNo=''">
      <tr>
        <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
          <xsl:value-of select="$MyDoc//message[@messageid='54']"/>
        </td>
      </tr>
    </xsl:when>
  </xsl:choose>
</xsl:template>

  <xsl:template name="ClaimRecordNo">
    <xsl:choose>
      <xsl:when test="ClaimRecordNo=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='55']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="TimeBarDate">
    <xsl:choose>
      <xsl:when test="TimeBarDate=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='56']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="ClaimDate">
    <xsl:choose>
      <xsl:when test="ClaimDate=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='21']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="ClaimOfficer">
    <xsl:choose>
      <xsl:when test="ClaimOfficer=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='22']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="AccidentCause">
    <xsl:choose>
      <xsl:when test="AccidentCause=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='23']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="CaseCategory">
    <xsl:choose>
      <xsl:when test="CaseCategory=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='24']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="CaseStatus">
    <xsl:choose>
      <xsl:when test="CaseStatus=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='25']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="ClaimantStatus">
    <xsl:choose>
      <xsl:when test="ClaimantStatus=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='26']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template match="INPUTXML/THIRDPARTIES/TPDETAIL">
    <xsl:choose>
      <xsl:when test="OtherPartyType=''or
                 CompanyName=''or 
                TPClaimant=''
                ">
        <tr>
          <td class="pageheader" style="background-color:#eee;font-size:12pt;border:.5px solid #c3e4f6;">
            <xsl:value-of select="$MyDoc//message[@messageid='300']"/>
          </td>
        </tr>
        <xsl:call-template name="OtherPartyType" />
        <xsl:call-template name="CompanyName" />
        <xsl:call-template name="TPClaimant" />
      </xsl:when>
      <xsl:otherwise></xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!-- ================================= TEMPLATES =============================================-->
  <xsl:template name="OtherPartyType">
    <xsl:choose>
      <xsl:when test="OtherPartyType=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='27']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="CompanyName">
    <xsl:choose>
      <xsl:when test="CompanyName=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='28']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="TPClaimant">
    <xsl:choose>
      <xsl:when test="TPClaimant=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='15']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template match="INPUTXML/CLAIMNOTES/NOTESDETAIL">
    <xsl:choose>
      <xsl:when test="NoteCode=''or
                 NoteDate=''or 
                NoteTime='' or
                ImageCode=''or
                 ImageId=''
                ">
        <tr>
          <td class="pageheader" style="background-color:#eee;font-size:12pt;border:.5px solid #c3e4f6;">
            <xsl:value-of select="$MyDoc//message[@messageid='400']"/>
          </td>
        </tr>
        <xsl:call-template name="NoteCode" />
        <xsl:call-template name="NoteDate" />
        <xsl:call-template name="NoteTime" />
        <xsl:call-template name="ImageCode" />
        <xsl:call-template name="ImageId" />
      </xsl:when>
      <xsl:otherwise></xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!-- ================================= TEMPLATES =============================================-->
  <xsl:template name="NoteCode">
    <xsl:choose>
      <xsl:when test="NoteCode=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='29']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="NoteDate">
    <xsl:choose>
      <xsl:when test="NoteDate=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='30']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="NoteTime">
    <xsl:choose>
      <xsl:when test="NoteTime=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='31']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="ImageCode">
    <xsl:choose>
      <xsl:when test="ImageCode=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='32']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="ImageId">
    <xsl:choose>
      <xsl:when test="ImageId=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='33']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template match="INPUTXML/CLAIMTRANSACTIONS/TRANSDETAIL">
    <xsl:choose>
      <xsl:when test="TransactionDate=''or
                 TransactionType=''or 
                CreditorName='' or
                Authorizedby=''or
                 ExpenseCode=''
                ">
        <tr>
          <td class="pageheader" style="background-color:#eee;font-size:12pt;border:1px solid #c3e4f6;">
            <xsl:value-of select="$MyDoc//message[@messageid='500']"/>
          </td>
        </tr>
        <xsl:call-template name="TransactionDate" />
        <xsl:call-template name="TransactionType" />
        <xsl:call-template name="CreditorName" />
        <xsl:call-template name="Authorizedby" />
        <xsl:call-template name="ExpenseCode" />
      </xsl:when>
      <xsl:otherwise></xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!-- ================================= TEMPLATES =============================================-->
  <xsl:template name="TransactionDate">
    <xsl:choose>
      <xsl:when test="TransactionDate=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='34']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="TransactionType">
    <xsl:choose>
      <xsl:when test="TransactionType=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='35']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="CreditorName">
    <xsl:choose>
      <xsl:when test="CreditorName=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='36']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="Authorizedby">
    <xsl:choose>
      <xsl:when test="Authorizedby=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='37']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="ExpenseCode">
    <xsl:choose>
      <xsl:when test="ExpenseCode=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='38']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template match="INPUTXML/CLAIMDIARY/DIARYDETAIL">
    <xsl:choose>
      <xsl:when test="LISTTYPEID=''or
                  TOUSERID=''or 
                  FROMUSERID='' or
                  STARTTIME=''or
                  ReminderBeforeCompletion='' or
                  Escalation='' or
                  SUBJECTLINE=''
                ">
        <tr>
          <td class="pageheader" style="background-color:#eee;font-size:12pt;border:1px solid #c3e4f6;">
            <xsl:value-of select="$MyDoc//message[@messageid='600']"/>
          </td>
        </tr>
        <xsl:call-template name="LISTTYPEID" />
        <xsl:call-template name="TOUSERID" />
        <xsl:call-template name="FROMUSERID" />
        <xsl:call-template name="STARTTIME" />
        <xsl:call-template name="ReminderBeforeCompletion" />
        <xsl:call-template name="Escalation" />
        <xsl:call-template name="SUBJECTLINE" />
      </xsl:when>
      <xsl:otherwise></xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!-- ================================= TEMPLATES =============================================-->
  <xsl:template name="LISTTYPEID">
    <xsl:choose>
      <xsl:when test="LISTTYPEID=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='39']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="TOUSERID">
    <xsl:choose>
      <xsl:when test="TOUSERID=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='40']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="FROMUSERID">
    <xsl:choose>
      <xsl:when test="FROMUSERID=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='41']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="STARTTIME">
    <xsl:choose>
      <xsl:when test="STARTTIME=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='42']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="ReminderBeforeCompletion">
    <xsl:choose>
      <xsl:when test="ReminderBeforeCompletion=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='43']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="Escalation">
    <xsl:choose>
      <xsl:when test="Escalation=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='44']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="SUBJECTLINE">
    <xsl:choose>
      <xsl:when test="SUBJECTLINE=''">
        <tr>
          <td class="midcolora" style="background-color:#FFF;font-size:12pt;border:none;">
            <xsl:value-of select="$MyDoc//message[@messageid='45']"/>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template match="INPUTXML/CLAIMS/ERRORS">
    <xsl:if test="./CLAIMERROR[@ERRFOUND = 'T']">
      <tr>
        <td style="background-color:#f8fcfe;font-size:12pt;border:1px solid #c3e4f6;border-bottom-style: none;">
          <xsl:value-of select="$MyDoc//message[@messageid='1000']"/>
        </td>
      </tr>
    </xsl:if>
  </xsl:template>

  <xsl:template match="INPUTXML/TPARTIES/ERRORS">
    <xsl:choose>
      <xsl:when test="TPClaimant='Y'">
        <xsl:if test="./TPARTYERROR[@ERRFOUND = 'T']">
          <tr>
            <td style="background-color:#f8fcfe;font-size:12pt;border:1px solid #c3e4f6;border-bottom-style: none;">
              <xsl:value-of select="$MyDoc//message[@messageid='2000']"/>
            </td>
          </tr>
        </xsl:if>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template match="INPUTXML/NOTES/ERRORS">
    <xsl:if test="./NOTEERROR[@ERRFOUND = 'T']">
      <tr>
        <td style="background-color:#f8fcfe;font-size:12pt;border:1px solid #c3e4f6;border-bottom-style: none;">
          <xsl:value-of select="$MyDoc//message[@messageid='3000']"/>
        </td>
      </tr>
    </xsl:if>
  </xsl:template>

  <xsl:template match="INPUTXML/TRANSACTIONS/ERRORS">
    <xsl:if test="./TRANSACTIONERROR[@ERRFOUND = 'T']">
      <tr>
        <td style="background-color:#f8fcfe;font-size:12pt;border:1px solid #c3e4f6;border-bottom-style: none;">
          <xsl:value-of select="$MyDoc//message[@messageid='4000']"/>
        </td>
      </tr>
    </xsl:if>
  </xsl:template>

  <xsl:template match="INPUTXML/DIARY/ERRORS">
    <xsl:if test="./DIARYERROR[@ERRFOUND = 'T']">
      <tr>
        <td style="background-color:#f8fcfe;font-size:12pt;border:1px solid #c3e4f6;">
          <xsl:value-of select="$MyDoc//message[@messageid='5000']"/>
        </td>
      </tr>
    </xsl:if>
  </xsl:template>
  
</xsl:stylesheet>
  