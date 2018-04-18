IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE       PROCEDURE dbo.PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS                
 @CUSTOMERID   int,                  
 @POLID                int,                  
 @VERSIONID   int,                  
 @VEHICLEID   int,                  
 @CALLEDFROM  VARCHAR(20)                  
AS                  
BEGIN                  
 IF @CALLEDFROM='APPLICATION'                  
 BEGIN                  
  IF (@VEHICLEID>0)                  
  BEGIN                  
   SELECT ML1.LOOKUP_VALUE_DESC as NATUREOFINTEREST,isnull(RANK,0) RANK, CASE CAI.NATURE_OF_INTEREST  WHEN '11815' THEN 'AI' WHEN '11866' THEN 'AI' WHEN '11867' THEN 'AI' WHEN '11394' THEN 'AI' WHEN '11590' THEN 'AI'                  
   WHEN '11392' THEN 'LP' WHEN '11393' THEN 'LP' WHEN '11865' THEN 'LP' ELSE '' END AS NATURE_OF_INTEREST,                  
   --HL.HOLDER_NAME AS HOLDER_NAME,                  
   CASE isnull(hl.HOLDER_NAME,'')  WHEN '' THEN CAI.HOLDER_NAME ELSE HL.HOLDER_NAME END AS HOLDER_NAME,                  
   CASE isnull(hl.holder_id,'')                   
    when '' then RTRIM(ISNULL(CAI.HOLDER_ADD1,'')) +                   
     case CAI.HOLDER_ADD2                   
      when '' then ''                   
      else ', ' end +                   
     RTRIM(ISNULL(CAI.HOLDER_ADD2,'')) +                   
     case CAI.HOLDER_ADD2                   
      when '' then ''                   
      else '' end                   
                       
    else                  
     RTRIM(ISNULL(HL.HOLDER_ADD1,''))+                   
     case HL.HOLDER_ADD2                   
      when '' then ''                   
      else ', ' end +                   
     RTRIM(ISNULL(HL.HOLDER_ADD2,'')) +                   
     case HL.HOLDER_ADD2                   
      when '' then ''                   
      else '' end                   
                  
    end AS ADDRESS,                   
    CASE isnull(hl.holder_id,'')                   
     when '' then                   
      RTRIM(ISNULL(CAI.HOLDER_CITY,'')) +                   
      case CAI.HOLDER_CITY                   
       when '' then ''                   
       else ', ' end +                  
      ISNULL(CSL1.STATE_CODE,'') +                   
      case CSL1.STATE_CODE                   
       when '' then ''                   
       else ' ' end +                   
      ISNULL(CAI.HOLDER_ZIP,'')                  
     else                  
      RTRIM(ISNULL(HL.HOLDER_CITY,''))+                   
      case HL.HOLDER_CITY                   
       when '' then ''                   
       else ', ' end +                  
      ISNULL(CSL.STATE_CODE,'') +                   
      case CSL.STATE_CODE                   
       when '' then ''                   
       else ' ' end +                   
      ISNULL(HL.HOLDER_ZIP,'')                  
     end as CITYSTATEZIP,                    
                     
   ISNULL(LOAN_REF_NUMBER,'') AS LOAN_REF_NUMBER,CAST (WI.insured_veh_Number AS VARCHAR) vehicle_NO,                  
   ISNULL(ML1.LOOKUP_VALUE_DESC,'') AS ADDLINTNAME,CAI.NATURE_OF_INTEREST,wi.INSURED_VEH_NUMBER,                
   WI.VEHICLE_ID, CAI.ADD_INT_ID                  
   FROM APP_VEHICLES WI  WITH(NOLOCK)                  
   INNER JOIN APP_ADD_OTHER_INT CAI WITH(NOLOCK) ON WI.APP_ID = CAI.APP_ID AND                  
    WI.APP_VERSION_ID = CAI.APP_VERSION_ID AND WI.CUSTOMER_ID = CAI.CUSTOMER_ID AND                   
    WI.VEHICLE_ID = CAI.VEHICLE_ID and ISNULL(CAI.is_active,'N')='Y'                  
   LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST HL WITH(NOLOCK) ON CAI.HOLDER_ID = HL.HOLDER_ID                  
   LEFT JOIN MNT_COUNTRY_STATE_LIST CSL WITH(NOLOCK) ON CSL.STATE_ID=HL.HOLDER_STATE                  
   LEFT JOIN MNT_COUNTRY_STATE_LIST CSL1 WITH(NOLOCK) ON CSL1.STATE_ID=CAI.HOLDER_STATE       
 LEFT OUTER JOIN MNT_LOOKUP_VALUES ML1 WITH(NOLOCK) ON ML1.LOOKUP_UNIQUE_ID = CAI.NATURE_OF_INTEREST and LOOKUP_ID='1213'            
   WHERE WI.CUSTOMER_ID = @CUSTOMERID AND WI.APP_ID = @POLID AND WI.APP_VERSION_ID = @VERSIONID AND                  
   WI.VEHICLE_ID =  @VEHICLEID and ISNULL(wi.is_active,'N')='Y' --AND (CAI.HOLDER_ID IS NULL OR (CAI.HOLDER_ID IS NOT NULL AND ISNULL(HL.IS_ACTIVE,'N') = 'Y'))                  
                   
                     
  END           
  ELSE                  
  BEGIN                  
   SELECT ML1.LOOKUP_VALUE_DESC as NATUREOFINTEREST,isnull(RANK,0) RANK, CASE CAI.NATURE_OF_INTEREST  WHEN '11815' THEN 'AI' WHEN '11866' THEN 'AI' WHEN '11867' THEN 'AI' WHEN '11394' THEN 'AI' WHEN '11590' THEN 'AI'                  
   WHEN '11392' THEN 'LP' WHEN '11393' THEN 'LP' WHEN '11865' THEN 'LP' ELSE '' END AS NATURE_OF_INTEREST,                  
   CASE isnull(hl.HOLDER_NAME,'')  WHEN '' THEN CAI.HOLDER_NAME ELSE HL.HOLDER_NAME END AS HOLDER_NAME,                  
  CASE isnull(hl.holder_id,'')                   
    when '' then RTRIM(ISNULL(CAI.HOLDER_ADD1,'')) +                   
     case CAI.HOLDER_ADD2                   
      when '' then ''                   
 else ', ' end +                   
     RTRIM(ISNULL(CAI.HOLDER_ADD2,'')) +                   
     case CAI.HOLDER_ADD2                   
      when '' then ''                   
      else '' end                   
                       
    else                  
     RTRIM(ISNULL(HL.HOLDER_ADD1,''))+                   
     case HL.HOLDER_ADD2                   
      when '' then ''                   
      else ', ' end +                   
     RTRIM(ISNULL(HL.HOLDER_ADD2,'')) +                   
     case HL.HOLDER_ADD2                   
      when '' then ''                   
      else '' end                   
                  
    end AS ADDRESS,                   
    CASE isnull(hl.holder_id,'')                   
     when '' then                 
      RTRIM(ISNULL(CAI.HOLDER_CITY,'')) +                   
      case CAI.HOLDER_CITY                   
       when '' then ''                   
       else ', ' end +                  
      ISNULL(CSL1.STATE_CODE,'') +                   
      case CSL1.STATE_CODE                   
       when '' then ''                   
       else ' ' end +                   
      ISNULL(CAI.HOLDER_ZIP,'')                  
     else                  
      RTRIM(ISNULL(HL.HOLDER_CITY,''))+                   
      case HL.HOLDER_CITY                   
       when '' then ''                   
       else ', ' end +                   
      ISNULL(CSL.STATE_CODE,'') +                   
      case CSL.STATE_CODE                   
       when '' then ''                   
       else ' ' end +                   
      ISNULL(HL.HOLDER_ZIP,'')                  
     end as CITYSTATEZIP,                  
                  
   ISNULL(LOAN_REF_NUMBER,'') AS LOAN_REF_NUMBER,CAST(WI.INSURED_VEH_NUMBER AS VARCHAR) VEHICLE_NO,                  
   ISNULL(ML1.LOOKUP_VALUE_DESC,'') AS ADDLINTNAME,CAI.NATURE_OF_INTEREST,wi.INSURED_VEH_NUMBER,                  
    WI.VEHICLE_ID, CAI.ADD_INT_ID                  
   FROM APP_VEHICLES WI      WITH(NOLOCK)              
   INNER JOIN APP_ADD_OTHER_INT CAI WITH(NOLOCK) ON WI.APP_ID = CAI.APP_ID AND                  
    WI.APP_VERSION_ID = CAI.APP_VERSION_ID AND WI.CUSTOMER_ID = CAI.CUSTOMER_ID AND                   
    WI.VEHICLE_ID = CAI.VEHICLE_ID and ISNULL(cai.is_active,'N')='Y'
   LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST HL WITH(NOLOCK) ON CAI.HOLDER_ID = HL.HOLDER_ID
   LEFT JOIN MNT_COUNTRY_STATE_LIST CSL WITH(NOLOCK) ON CSL.STATE_ID=HL.HOLDER_STATE                  
   LEFT JOIN MNT_COUNTRY_STATE_LIST CSL1 WITH(NOLOCK) ON CSL1.STATE_ID=CAI.HOLDER_STATE                  
   LEFT OUTER JOIN MNT_LOOKUP_VALUES ML1 WITH(NOLOCK) ON ML1.LOOKUP_UNIQUE_ID = CAI.NATURE_OF_INTEREST and LOOKUP_ID='1213'                
   WHERE WI.CUSTOMER_ID = @CUSTOMERID AND WI.APP_ID = @POLID AND WI.APP_VERSION_ID = @VERSIONID                 
   and ISNULL(wi.is_active,'N')='Y' --AND (CAI.HOLDER_ID IS NULL OR (CAI.HOLDER_ID IS NOT NULL AND ISNULL(HL.IS_ACTIVE,'N') = 'Y'))                                  
                   
  END                  
 END                  
 ELSE IF @CALLEDFROM='POLICY'                  
 BEGIN                  
  IF (@VEHICLEID>0)                  
  BEGIN                  
   SELECT ML1.LOOKUP_VALUE_DESC as NATUREOFINTEREST,isnull(RANK,0) RANK, CASE CAI.NATURE_OF_INTEREST  WHEN '11815' THEN 'AI' WHEN '11866' THEN 'AI' WHEN '11867' THEN 'AI' WHEN '11394' THEN 'AI' WHEN '11590' THEN 'AI'                  
   WHEN '11392' THEN 'LP' WHEN '11393' THEN 'LP' WHEN '11865' THEN 'LP' ELSE '' END AS NATURE_OF_INTEREST,                  
   CASE isnull(hl.HOLDER_NAME,'')  WHEN '' THEN CAI.HOLDER_NAME ELSE HL.HOLDER_NAME END AS HOLDER_NAME,                  
   CASE isnull(hl.holder_id,'')                   
    when '' then RTRIM(ISNULL(CAI.HOLDER_ADD1,'')) +                   
     case CAI.HOLDER_ADD2                   
      when '' then ''                   
      else ', ' end +                   
     RTRIM(ISNULL(CAI.HOLDER_ADD2,'')) +               
     case CAI.HOLDER_ADD2                   
      when '' then ''                   
      else '' end                   
                       
    else                  
     RTRIM(ISNULL(HL.HOLDER_ADD1,''))+                   
     case HL.HOLDER_ADD2                   
      when '' then ''                   
      else ', ' end +                   
     RTRIM(ISNULL(HL.HOLDER_ADD2,'')) +                   
     case HL.HOLDER_ADD2             
      when '' then ''                   
      else '' end                   
                  
    end AS ADDRESS,                   
    CASE isnull(hl.holder_id,'')                   
     when '' then                   
      RTRIM(ISNULL(CAI.HOLDER_CITY,'')) +                   
      case CAI.HOLDER_CITY                   
       when '' then ''                   
       else ', ' end +            
 ISNULL(CSL1.STATE_CODE,'') +                   
      case CSL1.STATE_CODE                   
       when '' then ''                   
       else ' ' end +                   
      ISNULL(CAI.HOLDER_ZIP,'')                  
     else                  
      RTRIM(ISNULL(HL.HOLDER_CITY,''))+                   
      case HL.HOLDER_CITY                   
       when '' then ''                   
       else ', ' end +                  
 ISNULL(CSL.STATE_CODE,'') +                   
      case CSL.STATE_CODE                   
       when '' then ''                   
       else ' ' end +                   
      ISNULL(HL.HOLDER_ZIP,'')                  
     end as CITYSTATEZIP,                  
   ISNULL(LOAN_REF_NUMBER,'') AS LOAN_REF_NUMBER,CAST(WI.INSURED_VEH_NUMBER AS VARCHAR) VEHICLE_NO,                  
   ISNULL(ML1.LOOKUP_VALUE_DESC,'') AS ADDLINTNAME,CAI.NATURE_OF_INTEREST,wi.INSURED_VEH_NUMBER,                  
   WI.VEHICLE_ID, CAI.ADD_INT_ID ,
	'AUTO'    ADD_INT_NAME                   
   FROM POL_VEHICLES WI   WITH(NOLOCK)                 
   INNER JOIN POL_ADD_OTHER_INT CAI WITH(NOLOCK) ON WI.POLICY_ID = CAI.POLICY_ID AND                  
    WI.POLICY_VERSION_ID = CAI.POLICY_VERSION_ID AND WI.CUSTOMER_ID = CAI.CUSTOMER_ID AND                   
    WI.VEHICLE_ID = CAI.VEHICLE_ID  AND ISNULL(CAI.IS_ACTIVE,'N')='Y'                
   LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST HL WITH(NOLOCK) ON CAI.HOLDER_ID = HL.HOLDER_ID
   LEFT JOIN MNT_COUNTRY_STATE_LIST CSL WITH(NOLOCK) ON CSL.STATE_ID=HL.HOLDER_STATE                  
   LEFT JOIN MNT_COUNTRY_STATE_LIST CSL1 WITH(NOLOCK) ON CSL1.STATE_ID=CAI.HOLDER_STATE                  
   LEFT OUTER JOIN MNT_LOOKUP_VALUES ML1 WITH(NOLOCK) ON ML1.LOOKUP_UNIQUE_ID = CAI.NATURE_OF_INTEREST and LOOKUP_ID='1213'                  
WHERE WI.CUSTOMER_ID = @CUSTOMERID AND WI.POLICY_ID = @POLID AND WI.POLICY_VERSION_ID = @VERSIONID AND      
   WI.VEHICLE_ID =  @VEHICLEID and ISNULL(wi.is_active,'N')='Y' --AND (CAI.HOLDER_ID IS NULL OR (CAI.HOLDER_ID IS NOT NULL AND ISNULL(HL.IS_ACTIVE,'N') = 'Y'))                                    
                     
  END                  
  ELSE                  
  BEGIN                  
   SELECT ML1.LOOKUP_VALUE_DESC as NATUREOFINTEREST,isnull(RANK,0) RANK, CASE CAI.NATURE_OF_INTEREST  WHEN '11815' THEN 'AI' WHEN '11866' THEN 'AI' WHEN '11867' THEN 'AI' WHEN '11394' THEN 'AI' WHEN '11590' THEN 'AI'                  
   WHEN '11392' THEN 'LP' WHEN '11393' THEN 'LP' WHEN '11865' THEN 'LP' ELSE '' END AS NATURE_OF_INTEREST,              
   CASE isnull(hl.HOLDER_NAME,'')  WHEN '' THEN CAI.HOLDER_NAME ELSE HL.HOLDER_NAME END AS HOLDER_NAME,                  
    CASE isnull(hl.holder_id,'')                   
    when '' then RTRIM(ISNULL(CAI.HOLDER_ADD1,'')) +                   
     case CAI.HOLDER_ADD2                   
      when '' then ''                   
      else ', ' end +                   
     RTRIM(ISNULL(CAI.HOLDER_ADD2,'')) +                   
     case CAI.HOLDER_ADD2                   
      when '' then ''                   
      else '' end                   
                       
    else                  
     RTRIM(ISNULL(HL.HOLDER_ADD1,''))+          
     case HL.HOLDER_ADD2                   
      when '' then ''                   
      else ', ' end +                   
     RTRIM(ISNULL(HL.HOLDER_ADD2,'')) +                   
     case HL.HOLDER_ADD2                   
      when '' then ''                   
      else '' end                   
                  
    end AS ADDRESS,                   
    CASE isnull(hl.holder_id,'')                   
     when '' then                   
      RTRIM(ISNULL(CAI.HOLDER_CITY,'')) +                   
      case CAI.HOLDER_CITY                   
       when '' then ''                   
       else ', ' end +                  
      ISNULL(CSL1.STATE_CODE,'') +                   
      case CSL1.STATE_CODE                   
       when '' then ''                   
       else ' ' end +                   
      ISNULL(CAI.HOLDER_ZIP,'')                  
     else                  
      RTRIM(ISNULL(HL.HOLDER_CITY,''))+                   
      case HL.HOLDER_CITY                   
       when '' then ''                   
       else ', ' end +                  
      ISNULL(CSL.STATE_CODE,'') +                   
      case CSL.STATE_CODE                   
       when '' then ''           
       else ' ' end +                   
      ISNULL(HL.HOLDER_ZIP,'')                  
     end as CITYSTATEZIP,                  
   ISNULL(LOAN_REF_NUMBER,'') AS LOAN_REF_NUMBER,CAST(WI.INSURED_VEH_NUMBER AS VARCHAR) VEHICLE_NO,                  
   ISNULL(ML1.LOOKUP_VALUE_DESC,'') AS ADDLINTNAME,CAI.NATURE_OF_INTEREST,wi.INSURED_VEH_NUMBER,                
   WI.VEHICLE_ID, CAI.ADD_INT_ID ,
	'AUTO'    ADD_INT_NAME                        
   FROM POL_VEHICLES WI WITH(NOLOCK)                 
   INNER JOIN POL_ADD_OTHER_INT CAI WITH(NOLOCK) ON WI.POLICY_ID = CAI.POLICY_ID AND                  
    WI.POLICY_VERSION_ID = CAI.POLICY_VERSION_ID AND WI.CUSTOMER_ID = CAI.CUSTOMER_ID AND                   
    WI.VEHICLE_ID = CAI.VEHICLE_ID  AND ISNULL(CAI.IS_ACTIVE,'N')='Y'                
   LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST HL WITH(NOLOCK) ON CAI.HOLDER_ID = HL.HOLDER_ID
   LEFT JOIN MNT_COUNTRY_STATE_LIST CSL WITH(NOLOCK) ON CSL.STATE_ID=HL.HOLDER_STATE                  
   LEFT JOIN MNT_COUNTRY_STATE_LIST CSL1 WITH(NOLOCK) ON CSL1.STATE_ID=CAI.HOLDER_STATE                  
   LEFT OUTER JOIN MNT_LOOKUP_VALUES ML1 WITH(NOLOCK) ON ML1.LOOKUP_UNIQUE_ID = CAI.NATURE_OF_INTEREST and LOOKUP_ID='1213'                  
   WHERE WI.CUSTOMER_ID = @CUSTOMERID AND WI.POLICY_ID = @POLID AND WI.POLICY_VERSION_ID = @VERSIONID         
   and ISNULL(wi.is_active,'N')='Y' --AND (CAI.HOLDER_ID IS NULL OR (CAI.HOLDER_ID IS NOT NULL AND ISNULL(HL.IS_ACTIVE,'N') = 'Y'))                        
  END                  
 END                   
END                   


GO

