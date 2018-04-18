IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--- DROP PROC [dbo].[PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS]  
CREATE      PROCEDURE [dbo].[PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS]      
 @CUSTOMERID   int,                      
 @POLID                int,                      
 @VERSIONID   int,                      
 @BOATID   int,                      
 @CALLEDFROM  VARCHAR(20)                      
AS                      
BEGIN              
                  
 --IF @CALLEDFROM='APPLICATION'             
                    
-- BEGIN                      
--  IF (@BOATID>0)                      
--  BEGIN                      
--   SELECT 1 AS BOAT_TRAILER,isnull(RANK,0) RANK,             
--   CASE NATURE_OF_INTEREST             
--   WHEN '11815' THEN 'AI'             
--   WHEN '11866' THEN 'AI'            
--   --WHEN '11867' THEN 'AI'            
--   WHEN '11394' THEN 'AI'            
--   --WHEN '11590' THEN 'AI'                        
--   WHEN '11392' THEN 'LP'             
--   WHEN '11393' THEN 'LP'             
--   WHEN '11865' THEN 'LP'             
--ELSE ''  END AS NATURE_OF_INTEREST,                      
--   --HL.HOLDER_NAME AS HOLDER_NAME,                      
--   CASE isnull(hl.HOLDER_NAME,'')  WHEN '' THEN CAI.HOLDER_NAME ELSE HL.HOLDER_NAME END AS HOLDER_NAME,                      
--   CASE isnull(hl.holder_id,'')                       
--    when '' then RTRIM(ISNULL(CAI.HOLDER_ADD1,'')) +                       
--     case CAI.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end +                       
--     RTRIM(ISNULL(CAI.HOLDER_ADD2,''))                        
--     /*case CAI.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end                       
--       */                    
--    else                      
--     RTRIM(ISNULL(HL.HOLDER_ADD1,''))+                       
--     case HL.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end +                       
--     RTRIM(ISNULL(HL.HOLDER_ADD2,''))-- +                       
--    /* case HL.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end                       
--      */                
--    end AS ADDRESS,   
  
-- CASE isnull(hl.holder_id,'')                       
--    when '' then RTRIM(ISNULL(CAI.HOLDER_ADD1,'')) +                       
--     case CAI.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end +                       
--     RTRIM(ISNULL(CAI.HOLDER_ADD2,''))                        
--     /*case CAI.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end                       
--       */                    
--    else                      
--     RTRIM(ISNULL(HL.HOLDER_ADD1,''))+                       
--     case HL.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end +                       
--     RTRIM(ISNULL(HL.HOLDER_ADD2,''))-- +                       
--    /* case HL.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end                       
--      */                
--    end AS ACCORDADDRESS,     --ITRACK 4134                  
                        
--    CASE isnull(hl.holder_id,'')                       
--     when '' then                       
--      RTRIM(ISNULL(CAI.HOLDER_CITY,'')) +                       
----      case CAI.HOLDER_CITY                       
----       when '' then ''                       
----       else ', ' end +                      
--      ' '+ ISNULL(CSL1.STATE_CODE,'') +                       
--      case CSL1.STATE_CODE                       
--       when '' then ''                       
--       else ' ' end +                       
--      ISNULL(CAI.HOLDER_ZIP,'')                      
--     else                      
--      RTRIM(ISNULL(HL.HOLDER_CITY,''))+                       
----      case HL.HOLDER_CITY                       
----       when '' then ''                       
----       else ', ' end +                      
--     ' '+ ISNULL(CSL.STATE_CODE,'') +    
--      case CSL.STATE_CODE                       
--       when '' then ''                       
--       else ' ' end +                       
--      ISNULL(HL.HOLDER_ZIP,'')                      
--     end as CITYSTATEZIP,   
-- CASE isnull(hl.holder_id,'')                       
--     when '' then                       
--      RTRIM(ISNULL(CAI.HOLDER_CITY,'')) +                       
----      case CAI.HOLDER_CITY                       
----       when '' then ''                       
----       else ', ' end +                      
--      ' '+ ISNULL(CSL1.STATE_CODE,'') +                       
--      case CSL1.STATE_CODE                       
--       when '' then ''                       
--       else ' ' end +                       
--      ISNULL(CAI.HOLDER_ZIP,'')                      
--     else                      
--      RTRIM(ISNULL(HL.HOLDER_CITY,''))+                       
----      case HL.HOLDER_CITY                       
----       when '' then ''                       
----       else ', ' end +                      
--     ' '+ ISNULL(CSL.STATE_CODE,'') +    
--      case CSL.STATE_CODE                       
--       when '' then ''                       
--       else ' ' end +                       
--      ISNULL(HL.HOLDER_ZIP,'')                      
--     end as HOLDERCITYSTATEZIP,                           
                         
--   ISNULL(LOAN_REF_NUMBER,'') AS LOAN_REF_NUMBER,CAST (WI.BOAT_NO AS VARCHAR) BOAT_NO,                      
--   ISNULL(ML1.LOOKUP_VALUE_DESC,'') AS ADDLINTNAME,NATURE_OF_INTEREST,                    
--   WI.BOAT_ID, CAI.ADD_INT_ID                      
--   FROM APP_WATERCRAFT_INFO WI                      
--   INNER JOIN APP_WATERCRAFT_COV_ADD_INT CAI WITH(NOLOCK) ON WI.APP_ID = CAI.APP_ID AND                      
--    WI.APP_VERSION_ID = CAI.APP_VERSION_ID AND WI.CUSTOMER_ID = CAI.CUSTOMER_ID AND                       
--    WI.BOAT_ID = CAI.BOAT_ID                      
--   LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST HL WITH(NOLOCK) ON CAI.HOLDER_ID = HL.HOLDER_ID                  
--   LEFT JOIN MNT_COUNTRY_STATE_LIST CSL WITH(NOLOCK) ON CSL.STATE_ID=HL.HOLDER_STATE                      
--   LEFT JOIN MNT_COUNTRY_STATE_LIST CSL1 WITH(NOLOCK) ON CSL1.STATE_ID=CAI.HOLDER_STATE                      
--   LEFT OUTER JOIN MNT_LOOKUP_VALUES ML1 WITH(NOLOCK) ON ML1.LOOKUP_UNIQUE_ID = NATURE_OF_INTEREST and LOOKUP_ID='1213'                      
--   WHERE WI.CUSTOMER_ID = @CUSTOMERID AND WI.APP_ID = @POLID AND WI.APP_VERSION_ID = @VERSIONID AND                      
--   WI.BOAT_ID =  @BOATID and wi.is_active='Y'  AND ISNULL(CAI.IS_ACTIVE,'N')='Y' --AND (CAI.HOLDER_ID IS NULL OR (CAI.HOLDER_ID IS NOT NULL))  
-- -- AND ISNULL(HL.IS_ACTIVE,'N') = 'Y'))                                        
                       
--   UNION ALL                      
                         
--   SELECT 2 AS BOAT_TRAILER,isnull(RANK,0) RANK,             
--   CASE NATURE_OF_INTEREST             
--  WHEN '11815' THEN 'AI'             
--  WHEN '11866' THEN 'AI'            
--  --WHEN '11867' THEN 'AI'            
--  WHEN '11394' THEN 'AI'             
--  WHEN '11590' THEN 'AI'                        
--  WHEN '11392' THEN 'LP'            
--  WHEN '11393' THEN 'LP'            
--  WHEN '11865' THEN 'LP'            
-- ELSE ''  END AS NATURE_OF_INTEREST,                      
                         
--   CASE isnull(hl.HOLDER_NAME,'')  WHEN '' THEN TAI.HOLDER_NAME ELSE HL.HOLDER_NAME END AS HOLDER_NAME,                      
                          
--    CASE isnull(hl.holder_id,'')                       
--    when '' then RTRIM(ISNULL(TAI.HOLDER_ADD1,'')) +                       
--     case TAI.HOLDER_ADD2                    
--      when '' then ''                       
--      else ', ' end +                       
--     RTRIM(ISNULL(TAI.HOLDER_ADD2,'')) --+                       
--    /* case TAI.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end                       
--      */                     
--    else                      
--     RTRIM(ISNULL(HL.HOLDER_ADD2,''))+                       
--     case HL.HOLDER_ADD1                 
--      when '' then ''                       
--      else ', ' end +                       
--     RTRIM(ISNULL(HL.HOLDER_ADD2,'')) --+                       
--    /* case HL.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end                       
--      */                
--end AS ADDRESS,      
  
  
--  CASE isnull(hl.holder_id,'')                       
--    when '' then RTRIM(ISNULL(TAI.HOLDER_ADD1,'')) +                       
--     case TAI.HOLDER_ADD2                    
--      when '' then ''                       
--      else ', ' end +                       
--     RTRIM(ISNULL(TAI.HOLDER_ADD2,'')) --+                       
--    /* case TAI.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end                       
--      */                     
--    else                      
--     RTRIM(ISNULL(HL.HOLDER_ADD1,''))+                       
--     case HL.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end +                       
--     RTRIM(ISNULL(HL.HOLDER_ADD2,'')) --+                       
--    /* case HL.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end                       
--      */                
--   end AS ACCORDADDRESS,                       
                   
--    CASE isnull(hl.holder_id,'')                       
--     when '' then                       
--      RTRIM(ISNULL(TAI.HOLDER_CITY,'')) +                       
----      case TAI.HOLDER_CITY                       
----       when '' then ''                       
----       else ', ' end +                      
--    ' '+ ISNULL(CSL1.STATE_CODE,'') +                       
--      case CSL1.STATE_CODE                       
--       when '' then ''                       
--       else ' ' end +                       
--      ISNULL(TAI.HOLDER_ZIP,'')                      
--     else                      
--      RTRIM(ISNULL(HL.HOLDER_CITY,''))+                       
----      case HL.HOLDER_CITY                       
----       when '' then ''                       
----       else ', ' end +                      
--   ' '+   ISNULL(CSL.STATE_CODE,'') +                       
--      case CSL.STATE_CODE                       
--       when '' then ''                       
--       else ' ' end +                       
--      ISNULL(HL.HOLDER_ZIP,'')                      
--     end as CITYSTATEZIP,   
                
--    CASE isnull(hl.holder_id,'')                       
--     when '' then                       
--      RTRIM(ISNULL(TAI.HOLDER_CITY,'')) +                       
----      case TAI.HOLDER_CITY                       
----       when '' then ''                       
----       else ', ' end +                      
--    ' '+ ISNULL(CSL1.STATE_CODE,'') +                       
--      case CSL1.STATE_CODE                       
--       when '' then ''                       
--       else ' ' end +                       
--      ISNULL(TAI.HOLDER_ZIP,'')                      
--     else                      
--      RTRIM(ISNULL(HL.HOLDER_CITY,''))+                       
----      case HL.HOLDER_CITY                       
----       when '' then ''                       
----       else ', ' end +                      
--   ' '+   ISNULL(CSL.STATE_CODE,'') +                       
--      case CSL.STATE_CODE                       
--       when '' then ''                       
--       else ' ' end +                       
--      ISNULL(HL.HOLDER_ZIP,'')                      
--     end as HOLDERCITYSTATEZIP,                      
                        
                         
--   ISNULL(LOAN_REF_NUMBER,'') AS LOAN_REF_NUMBER, ISNULL(CAST(WI.BOAT_NO AS VARCHAR),'') + '/' + ISNULL(CAST(WTI.TRAILER_NO AS VARCHAR),'') AS BOAT_NO,                      
--   ISNULL(ML1.LOOKUP_VALUE_DESC,'') AS ADDLINTNAME,NATURE_OF_INTEREST,                    
--   WI.BOAT_ID, TAI.ADD_INT_ID                        
--   FROM APP_WATERCRAFT_INFO WI                      
--   INNER JOIN APP_WATERCRAFT_TRAILER_INFO WTI WITH(NOLOCK) ON WI.APP_ID = WTI.APP_ID AND                      
--    WI.APP_VERSION_ID = WTI.APP_VERSION_ID AND WI.CUSTOMER_ID = WTI.CUSTOMER_ID AND                       
--    WI.BOAT_ID = WTI.ASSOCIATED_BOAT      
--   INNER JOIN APP_WATERCRAFT_TRAILER_ADD_INT TAI WITH(NOLOCK) ON TAI.APP_ID = WTI.APP_ID AND                      
--    TAI.APP_VERSION_ID = WTI.APP_VERSION_ID AND TAI.CUSTOMER_ID = WTI.CUSTOMER_ID AND                       
--    TAI.TRAILER_ID = WTI.TRAILER_ID                       
--   LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST HL WITH(NOLOCK) ON TAI.HOLDER_ID = HL.HOLDER_ID                      
--   LEFT JOIN MNT_COUNTRY_STATE_LIST CSL WITH(NOLOCK) ON CSL.STATE_ID=HL.HOLDER_STATE                      
--   LEFT JOIN MNT_COUNTRY_STATE_LIST CSL1 WITH(NOLOCK) ON CSL1.STATE_ID=TAI.HOLDER_STATE                      
--   LEFT OUTER JOIN MNT_LOOKUP_VALUES ML1 WITH(NOLOCK) ON ML1.LOOKUP_UNIQUE_ID = NATURE_OF_INTEREST and LOOKUP_ID='1213'                      
--   WHERE WI.CUSTOMER_ID = @CUSTOMERID AND WI.APP_ID = @POLID AND WI.APP_VERSION_ID = @VERSIONID AND                      
--   WI.BOAT_ID =  @BOATID and wi.is_active='Y' AND ISNULL(TAI.IS_ACTIVE,'N')='Y' --AND (TAI.HOLDER_ID IS NULL OR (TAI.HOLDER_ID IS NOT NULL AND ISNULL(HL.IS_ACTIVE,'N') = 'Y'))                                        
--   order by rank                      
--  END                      
--  ELSE                      
--  BEGIN                      
--   SELECT 1 AS BOAT_TRAILER,isnull(RANK,0) RANK,             
--CASE NATURE_OF_INTEREST              
--  WHEN '11815' THEN 'AI'             
--  WHEN '11866' THEN 'AI'             
--  --WHEN '11867' THEN 'AI'             
--  WHEN '11394' THEN 'AI'             
--  --WHEN '11590' THEN 'AI'                        
--  WHEN '11392' THEN 'LP'             
--  WHEN '11393' THEN 'LP'            
--  WHEN '11865' THEN 'LP'            
-- ELSE ''  END AS NATURE_OF_INTEREST,                      
--   CASE isnull(hl.HOLDER_NAME,'')  WHEN '' THEN CAI.HOLDER_NAME ELSE HL.HOLDER_NAME END AS HOLDER_NAME,                      
--   CASE isnull(hl.holder_id,'')                       
--    when '' then RTRIM(ISNULL(CAI.HOLDER_ADD1,'')) +                   
--     case CAI.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end +                       
--     RTRIM(ISNULL(CAI.HOLDER_ADD2,'')) --+                       
--    /* case CAI.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end                       
--     */                      
--    else                      
--     RTRIM(ISNULL(HL.HOLDER_ADD1,''))+                       
-- case HL.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end +                       
--     RTRIM(ISNULL(HL.HOLDER_ADD2,''))-- +                       
--     /*case HL.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end                       
--      */                
--    end AS ADDRESS,  
  
-- CASE isnull(hl.holder_id,'')                       
--    when '' then RTRIM(ISNULL(CAI.HOLDER_ADD1,'')) +                   
--     case CAI.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end +                       
--     RTRIM(ISNULL(CAI.HOLDER_ADD2,'')) --+                       
--    /* case CAI.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end                       
--     */                      
--    else                      
--     RTRIM(ISNULL(HL.HOLDER_ADD1,''))+                       
-- case HL.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end +                       
--     RTRIM(ISNULL(HL.HOLDER_ADD2,''))-- +                       
--     /*case HL.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end                       
--      */                
--    end AS ACCORDADDRESS,  
  
                  
--    CASE isnull(hl.holder_id,'')   
--     when '' then                 
--      RTRIM(ISNULL(CAI.HOLDER_CITY,'')) +                       
----      case CAI.HOLDER_CITY                      
----       when '' then ''                       
----       else ', ' end +                      
--   ' '+   ISNULL(CSL1.STATE_CODE,'') +                       
--      case CSL1.STATE_CODE                       
--       when '' then ''                       
--       else ' ' end +                       
--      ISNULL(CAI.HOLDER_ZIP,'')                      
--     else                      
--      RTRIM(ISNULL(HL.HOLDER_CITY,''))+                       
----      case HL.HOLDER_CITY                       
----       when '' then ''                       
----       else ', ' end +                      
--    ' '+  ISNULL(CSL.STATE_CODE,'') +                       
--      case CSL.STATE_CODE                       
--       when '' then ''                       
--       else ' ' end +                       
--      ISNULL(HL.HOLDER_ZIP,'')                      
--     end as CITYSTATEZIP,   
  
-- CASE isnull(hl.holder_id,'')                       
--     when '' then                       
--      RTRIM(ISNULL(CAI.HOLDER_CITY,'')) +                       
----      case CAI.HOLDER_CITY                      
----       when '' then ''                       
----       else ', ' end +                      
--   ' '+   ISNULL(CSL1.STATE_CODE,'') +                       
--      case CSL1.STATE_CODE                       
--       when '' then ''                       
--       else ' ' end +                       
--      ISNULL(CAI.HOLDER_ZIP,'')                      
--     else                      
--      RTRIM(ISNULL(HL.HOLDER_CITY,''))+                       
----      case HL.HOLDER_CITY                       
----       when '' then ''                       
----       else ', ' end +                      
--    ' '+  ISNULL(CSL.STATE_CODE,'') +                       
--      case CSL.STATE_CODE                       
--       when '' then ''                       
--       else ' ' end +                       
--      ISNULL(HL.HOLDER_ZIP,'')                      
--     end as HOLDERCITYSTATEZIP,                      
                        
                      
--   ISNULL(LOAN_REF_NUMBER,'') AS LOAN_REF_NUMBER,CAST(WI.BOAT_NO AS VARCHAR) BOAT_NO,                 
--   ISNULL(ML1.LOOKUP_VALUE_DESC,'') AS ADDLINTNAME,NATURE_OF_INTEREST,                    
--   WI.BOAT_ID, CAI.ADD_INT_ID                        
--   FROM APP_WATERCRAFT_INFO WI                      
--   INNER JOIN APP_WATERCRAFT_COV_ADD_INT CAI WITH(NOLOCK) ON WI.APP_ID = CAI.APP_ID AND                      
--    WI.APP_VERSION_ID = CAI.APP_VERSION_ID AND WI.CUSTOMER_ID = CAI.CUSTOMER_ID AND                       
--    WI.BOAT_ID = CAI.BOAT_ID                      
--   LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST HL WITH(NOLOCK) ON CAI.HOLDER_ID = HL.HOLDER_ID                      
--   LEFT JOIN MNT_COUNTRY_STATE_LIST CSL WITH(NOLOCK) ON CSL.STATE_ID=HL.HOLDER_STATE                
--   LEFT JOIN MNT_COUNTRY_STATE_LIST CSL1 WITH(NOLOCK) ON CSL1.STATE_ID=CAI.HOLDER_STATE                      
--   LEFT OUTER JOIN MNT_LOOKUP_VALUES ML1 WITH(NOLOCK) ON ML1.LOOKUP_UNIQUE_ID = NATURE_OF_INTEREST and LOOKUP_ID='1213'                      
--   WHERE WI.CUSTOMER_ID = @CUSTOMERID AND WI.APP_ID = @POLID AND WI.APP_VERSION_ID = @VERSIONID                      
--   and wi.is_active='Y' AND ISNULL(CAI.IS_ACTIVE,'N')='Y'  --AND (CAI.HOLDER_ID IS NULL OR (CAI.HOLDER_ID IS NOT NULL AND ISNULL(HL.IS_ACTIVE,'N') = 'Y'))                                        
                       
--   UNION ALL                      
                         
--   SELECT 2 AS BOAT_TRAILER,isnull(RANK,0) RANK,             
--CASE NATURE_OF_INTEREST             
--  WHEN '11815' THEN 'AI'            
--  WHEN '11866' THEN 'AI'             
--  --WHEN '11867' THEN 'AI'            
--  WHEN '11394' THEN 'AI'            
--  --WHEN '11590' THEN 'AI'                        
--  WHEN '11392' THEN 'LP'            
--  WHEN '11393' THEN 'LP'             
--  WHEN '11865' THEN 'LP'            
-- ELSE ''  END AS NATURE_OF_INTEREST,       
--   CASE isnull(hl.HOLDER_NAME,'')  WHEN '' THEN TAI.HOLDER_NAME ELSE HL.HOLDER_NAME END AS HOLDER_NAME,                      
--   CASE isnull(hl.holder_id,'')                       
--    when '' then RTRIM(ISNULL(TAI.HOLDER_ADD1,'')) +                       
--     case TAI.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end +                       
--     RTRIM(ISNULL(TAI.HOLDER_ADD2,'')) --+                       
--     /*case TAI.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end                       
--       */                    
--    else                      
--     RTRIM(ISNULL(HL.HOLDER_ADD1,''))+                       
--     case HL.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end +                       
--     RTRIM(ISNULL(HL.HOLDER_ADD2,'')) --+                       
--    /* case HL.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end                       
--      */                
--    end AS ADDRESS,   
-- CASE isnull(hl.holder_id,'')                       
--    when '' then RTRIM(ISNULL(TAI.HOLDER_ADD1,'')) +                       
--     case TAI.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end +                       
--     RTRIM(ISNULL(TAI.HOLDER_ADD2,'')) --+                       
--     /*case TAI.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end                       
--       */                    
--    else                      
--     RTRIM(ISNULL(HL.HOLDER_ADD1,''))+                       
--     case HL.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end +                       
--     RTRIM(ISNULL(HL.HOLDER_ADD2,'')) --+                       
--    /* case HL.HOLDER_ADD2                       
--      when '' then ''                       
--      else ', ' end                       
--      */                
--    end AS ACCORDADDRESS,                       
                        
--    CASE isnull(hl.holder_id,'')                       
--     when '' then                       
--      RTRIM(ISNULL(TAI.HOLDER_CITY,'')) +                       
----      case TAI.HOLDER_CITY                       
----       when '' then ''                       
----       else ', ' end +                      
--     ' '+ ISNULL(CSL1.STATE_CODE,'') +                       
--      case CSL1.STATE_CODE                       
--       when '' then ''                       
--       else ' ' end +                       
--      ISNULL(TAI.HOLDER_ZIP,'')                      
--     else                      
--      RTRIM(ISNULL(HL.HOLDER_CITY,''))+                       
----      case HL.HOLDER_CITY                       
----       when '' then ''                       
----       else ', ' end +                      
--    ' '+  ISNULL(CSL.STATE_CODE,'') +                       
--      case CSL.STATE_CODE                       
--       when '' then ''                       
--       else ' ' end +                       
--      ISNULL(HL.HOLDER_ZIP,'')                      
--     end as CITYSTATEZIP,      
  
--CASE isnull(hl.holder_id,'')                       
--     when '' then                       
--      RTRIM(ISNULL(TAI.HOLDER_CITY,'')) +                       
----      case TAI.HOLDER_CITY                       
----       when '' then ''                       
----       else ', ' end +                      
--     ' '+ ISNULL(CSL1.STATE_CODE,'') +                       
--      case CSL1.STATE_CODE                       
--       when '' then ''                       
--       else ' ' end +                       
--      ISNULL(TAI.HOLDER_ZIP,'')                      
--     else                      
--      RTRIM(ISNULL(HL.HOLDER_CITY,''))+                       
----      case HL.HOLDER_CITY                       
----       when '' then ''                       
----       else ', ' end +                      
--    ' '+  ISNULL(CSL.STATE_CODE,'') +          
--      case CSL.STATE_CODE                       
--       when '' then ''                       
--       else ' ' end +                       
--      ISNULL(HL.HOLDER_ZIP,'')                      
--     end as HOLDERCITYSTATEZIP,                                      
--   ISNULL(LOAN_REF_NUMBER,'') AS LOAN_REF_NUMBER, ISNULL(CAST(WI.BOAT_NO AS VARCHAR),'') + '/' + ISNULL(CAST(WTI.TRAILER_NO AS VARCHAR),'') AS BOAT_NO,                      
--   ISNULL(ML1.LOOKUP_VALUE_DESC,'') AS ADDLINTNAME,NATURE_OF_INTEREST,                    
--   WI.BOAT_ID, TAI.ADD_INT_ID                        
--   FROM APP_WATERCRAFT_INFO WI                      
--   INNER JOIN APP_WATERCRAFT_TRAILER_INFO WTI WITH(NOLOCK) ON WI.APP_ID = WTI.APP_ID AND                      
--    WI.APP_VERSION_ID = WTI.APP_VERSION_ID AND WI.CUSTOMER_ID = WTI.CUSTOMER_ID AND                       
--    WI.BOAT_ID = WTI.ASSOCIATED_BOAT                      
--   INNER JOIN APP_WATERCRAFT_TRAILER_ADD_INT TAI WITH(NOLOCK) ON TAI.APP_ID = WTI.APP_ID AND                      
--    TAI.APP_VERSION_ID = WTI.APP_VERSION_ID AND TAI.CUSTOMER_ID = WTI.CUSTOMER_ID AND                       
--    TAI.TRAILER_ID = WTI.TRAILER_ID                       
--   LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST HL WITH(NOLOCK) ON TAI.HOLDER_ID = HL.HOLDER_ID                      
--   LEFT JOIN MNT_COUNTRY_STATE_LIST CSL WITH(NOLOCK) ON CSL.STATE_ID=HL.HOLDER_STATE                      
--   LEFT JOIN MNT_COUNTRY_STATE_LIST CSL1 WITH(NOLOCK) ON CSL1.STATE_ID=TAI.HOLDER_STATE                      
--   LEFT OUTER JOIN MNT_LOOKUP_VALUES ML1 WITH(NOLOCK) ON ML1.LOOKUP_UNIQUE_ID = NATURE_OF_INTEREST and LOOKUP_ID='1213'                      
--   WHERE WI.CUSTOMER_ID = @CUSTOMERID AND WI.APP_ID = @POLID AND WI.APP_VERSION_ID = @VERSIONID                      
--   and wi.is_active='Y'  AND ISNULL(TAI.IS_ACTIVE,'N')='Y'  --AND (TAI.HOLDER_ID IS NULL OR (TAI.HOLDER_ID IS NOT NULL AND ISNULL(HL.IS_ACTIVE,'N') = 'Y'))                                        
--   order by rank                      
--  END                      
-- END                      
-- ELSE IF @CALLEDFROM='POLICY' 
IF @CALLEDFROM='POLICY'                      
 BEGIN                      
  IF (@BOATID>0)                      
  BEGIN                      
   SELECT 1 AS BOAT_TRAILER,isnull(RANK,0) RANK,            
 CASE NATURE_OF_INTEREST            
 WHEN '11815' THEN 'AI'             
 WHEN '11866' THEN 'AI'            
 --WHEN '11867' THEN 'AI'             
 WHEN '11394' THEN 'AI'            
 --WHEN '11590' THEN 'AI'                        
 WHEN '11392' THEN 'LP'            
 WHEN '11393' THEN 'LP'             
 WHEN '11865' THEN 'LP'             
ELSE '' END AS NATURE_OF_INTEREST,                      
   CASE isnull(hl.HOLDER_NAME,'')  WHEN '' THEN CAI.HOLDER_NAME ELSE HL.HOLDER_NAME END AS HOLDER_NAME,                      
   CASE isnull(hl.holder_id,'')                       
    when '' then RTRIM(ISNULL(CAI.HOLDER_ADD1,'')) +                       
     case CAI.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end +                       
     RTRIM(ISNULL(CAI.HOLDER_ADD2,'')) --+                       
    /* case CAI.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end                       
      */                     
    else                      
     RTRIM(ISNULL(HL.HOLDER_ADD1,''))+                       
     case HL.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end +                       
     RTRIM(ISNULL(HL.HOLDER_ADD2,''))/* +                       
     case HL.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end   */                    
                      
    end AS ADDRESS,   
CASE isnull(hl.holder_id,'')                       
    when '' then RTRIM(ISNULL(CAI.HOLDER_ADD1,'')) +                       
     case CAI.HOLDER_ADD2                       
      when '' then ''         
      else ', ' end +                 
     RTRIM(ISNULL(CAI.HOLDER_ADD2,'')) --+                       
    /* case CAI.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end                       
      */                     
    else                      
     RTRIM(ISNULL(HL.HOLDER_ADD1,''))+                       
     case HL.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end +                       
     RTRIM(ISNULL(HL.HOLDER_ADD2,''))/* +                       
     case HL.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end   */                    
                      
    end AS ACCORDADDRESS,                       
                         
    CASE isnull(hl.holder_id,'')                       
     when '' then                       
      RTRIM(ISNULL(CAI.HOLDER_CITY,'')) +                       
--      case CAI.HOLDER_CITY                       
--       when '' then ''                       
--       else ', ' end +                      
    ' '+  ISNULL(CSL1.STATE_CODE,'') +                       
      case CSL1.STATE_CODE                       
       when '' then ''                       
       else ' ' end +                       
      ISNULL(CAI.HOLDER_ZIP,'')                      
     else          
      RTRIM(ISNULL(HL.HOLDER_CITY,''))+        
--      case HL.HOLDER_CITY                       
--       when '' then ''                       
--       else ', ' end +                      
     ' '+ ISNULL(CSL.STATE_CODE,'') +                       
      case CSL.STATE_CODE                       
       when '' then ''                       
       else ' ' end +                       
      ISNULL(HL.HOLDER_ZIP,'')                      
     end as CITYSTATEZIP,    
 CASE isnull(hl.holder_id,'')                       
     when '' then                       
      RTRIM(ISNULL(CAI.HOLDER_CITY,'')) +                       
--      case CAI.HOLDER_CITY                       
--       when '' then ''                       
--       else ', ' end +                      
    ' '+  ISNULL(CSL1.STATE_CODE,'') +                       
      case CSL1.STATE_CODE                       
       when '' then ''                       
       else ' ' end +                       
      ISNULL(CAI.HOLDER_ZIP,'')                      
     else          
      RTRIM(ISNULL(HL.HOLDER_CITY,''))+        
--      case HL.HOLDER_CITY                       
--       when '' then ''                       
--       else ', ' end +                      
     ' '+ ISNULL(CSL.STATE_CODE,'') +                       
      case CSL.STATE_CODE                       
       when '' then ''                       
       else ' ' end +                       
      ISNULL(HL.HOLDER_ZIP,'')                      
     end as HOLDERCITYSTATEZIP,                      
                       
   ISNULL(LOAN_REF_NUMBER,'') AS LOAN_REF_NUMBER,CAST(WI.BOAT_NO AS VARCHAR) BOAT_NO,                      
   ISNULL(ML1.LOOKUP_VALUE_DESC,'') AS ADDLINTNAME,NATURE_OF_INTEREST,                    
   WI.BOAT_ID, CAI.ADD_INT_ID ,  
    'WAT' AS ADD_INT_NAME                    
   FROM POL_WATERCRAFT_INFO WI WITH(NOLOCK)                     
   INNER JOIN POL_WATERCRAFT_COV_ADD_INT CAI WITH(NOLOCK) ON WI.POLICY_ID = CAI.POLICY_ID AND                      
    WI.POLICY_VERSION_ID = CAI.POLICY_VERSION_ID AND WI.CUSTOMER_ID = CAI.CUSTOMER_ID AND                       
    WI.BOAT_ID = CAI.BOAT_ID                      
   LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST HL WITH(NOLOCK) ON CAI.HOLDER_ID = HL.HOLDER_ID                      
   LEFT JOIN MNT_COUNTRY_STATE_LIST CSL WITH(NOLOCK) ON CSL.STATE_ID=HL.HOLDER_STATE                      
   LEFT JOIN MNT_COUNTRY_STATE_LIST CSL1 WITH(NOLOCK) ON CSL1.STATE_ID=CAI.HOLDER_STATE                      
   LEFT OUTER JOIN MNT_LOOKUP_VALUES ML1 WITH(NOLOCK) ON ML1.LOOKUP_UNIQUE_ID = NATURE_OF_INTEREST and LOOKUP_ID='1213'                
   WHERE WI.CUSTOMER_ID = @CUSTOMERID AND WI.POLICY_ID = @POLID AND WI.POLICY_VERSION_ID = @VERSIONID AND                      
   WI.BOAT_ID =  @BOATID and wi.is_active='Y' AND ISNULL(CAI.IS_ACTIVE,'N')='Y' --AND (CAI.HOLDER_ID IS NULL OR (CAI.HOLDER_ID IS NOT NULL AND ISNULL(HL.IS_ACTIVE,'N') = 'Y'))                                        
                         
   UNION ALL                      
                         
   SELECT 2 AS BOAT_TRAILER,isnull(RANK,0) RANK,            
 CASE NATURE_OF_INTEREST            
 WHEN '11815' THEN 'AI'             
 WHEN '11866' THEN 'AI'             
 --WHEN '11867' THEN 'AI'             
 WHEN '11394' THEN 'AI'            
 --WHEN '11590' THEN 'AI'                        
 WHEN '11392' THEN 'LP'            
 WHEN '11393' THEN 'LP'             
 WHEN '11865' THEN 'LP'            
 ELSE ''  END AS NATURE_OF_INTEREST,                      
   CASE isnull(hl.HOLDER_NAME,'')  WHEN '' THEN TAI.HOLDER_NAME ELSE HL.HOLDER_NAME END AS HOLDER_NAME,                      
    CASE isnull(hl.holder_id,'')                       
    when '' then RTRIM(ISNULL(TAI.HOLDER_ADD1,'')) +                       
     case TAI.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end +                  
     RTRIM(ISNULL(TAI.HOLDER_ADD2,''))/* +                       
     case TAI.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end    */                   
                           
    else                      
     RTRIM(ISNULL(HL.HOLDER_ADD1,''))+                       
     case HL.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end +                       
     RTRIM(ISNULL(HL.HOLDER_ADD2,'')) /*+                       
     case HL.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end    */                   
                      
    end AS ADDRESS,    
  
CASE isnull(hl.holder_id,'')                       
    when '' then RTRIM(ISNULL(TAI.HOLDER_ADD1,'')) +                       
     case TAI.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end +                  
     RTRIM(ISNULL(TAI.HOLDER_ADD2,''))/* +                       
     case TAI.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end    */                   
                           
    else                      
     RTRIM(ISNULL(HL.HOLDER_ADD1,''))+                       
     case HL.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end +                       
     RTRIM(ISNULL(HL.HOLDER_ADD2,'')) /*+                       
     case HL.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end    */                   
                      
    end AS ACCORDADDRESS,                       
                         
    CASE isnull(hl.holder_id,'')                       
     when '' then                       
      RTRIM(ISNULL(TAI.HOLDER_CITY,'')) +                       
--      case TAI.HOLDER_CITY                       
--       when '' then ''                       
--       else ', ' end +                   
    ' '+  ISNULL(CSL1.STATE_CODE,'') +            
     case CSL1.STATE_CODE                    
       when '' then ''                       
       else ' ' end +    
      ISNULL(TAI.HOLDER_ZIP,'')                      
     else                      
  RTRIM(ISNULL(HL.HOLDER_CITY,''))+                       
--      case HL.HOLDER_CITY                       
--       when '' then ''                       
--       else ', ' end +                      
     ' '+ ISNULL(CSL.STATE_CODE,'') +                       
      case CSL.STATE_CODE                       
       when '' then ''                       
       else ' ' end +                       
      ISNULL(HL.HOLDER_ZIP,'')                      
     end as CITYSTATEZIP,   
  
 CASE isnull(hl.holder_id,'')                       
  when '' then                       
      RTRIM(ISNULL(TAI.HOLDER_CITY,'')) +                       
--      case TAI.HOLDER_CITY                       
--       when '' then ''                       
--       else ', ' end +                   
    ' '+  ISNULL(CSL1.STATE_CODE,'') +            
     case CSL1.STATE_CODE                    
       when '' then ''                       
       else ' ' end +    
      ISNULL(TAI.HOLDER_ZIP,'')                      
     else                      
  RTRIM(ISNULL(HL.HOLDER_CITY,''))+                       
--      case HL.HOLDER_CITY                       
--       when '' then ''                       
--       else ', ' end +                      
     ' '+ ISNULL(CSL.STATE_CODE,'') +                       
      case CSL.STATE_CODE                       
       when '' then ''                       
       else ' ' end +                       
      ISNULL(HL.HOLDER_ZIP,'')                      
     end as HOLDERCITYSTATEZIP,                      
                        
   ISNULL(LOAN_REF_NUMBER,'') AS LOAN_REF_NUMBER, ISNULL(CAST(WI.BOAT_NO AS VARCHAR),'') + '/' + ISNULL(CAST(WTI.TRAILER_NO AS VARCHAR),'') AS BOAT_NO,                      
   ISNULL(ML1.LOOKUP_VALUE_DESC,'') AS ADDLINTNAME,NATURE_OF_INTEREST ,                    
   WTI.TRAILER_ID AS BOAT_ID, TAI.ADD_INT_ID  ,  
 'WTRAL' AS ADD_INT_NAME                                       
   FROM POL_WATERCRAFT_INFO WI WITH(NOLOCK)                     
   INNER JOIN POL_WATERCRAFT_TRAILER_INFO WTI WITH(NOLOCK) ON WI.POLICY_ID = WTI.POLICY_ID AND                      
    WI.POLICY_VERSION_ID = WTI.POLICY_VERSION_ID AND WI.CUSTOMER_ID = WTI.CUSTOMER_ID AND                       
    WI.BOAT_ID = WTI.ASSOCIATED_BOAT                      
   INNER JOIN POL_WATERCRAFT_TRAILER_ADD_INT TAI WITH(NOLOCK) ON TAI.POLICY_ID = WTI.POLICY_ID AND                      
    TAI.POLICY_VERSION_ID = WTI.POLICY_VERSION_ID AND TAI.CUSTOMER_ID = WTI.CUSTOMER_ID AND                       
    TAI.TRAILER_ID = WTI.TRAILER_ID  and   ISNULL(TAI.IS_ACTIVE,'N')='Y'                      
   LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST HL WITH(NOLOCK) ON TAI.HOLDER_ID = HL.HOLDER_ID                      
   LEFT JOIN MNT_COUNTRY_STATE_LIST CSL WITH(NOLOCK) ON CSL.STATE_ID=HL.HOLDER_STATE                      
   LEFT JOIN MNT_COUNTRY_STATE_LIST CSL1 WITH(NOLOCK) ON CSL1.STATE_ID=TAI.HOLDER_STATE       
   LEFT OUTER JOIN MNT_LOOKUP_VALUES ML1 WITH(NOLOCK) ON ML1.LOOKUP_UNIQUE_ID = NATURE_OF_INTEREST and LOOKUP_ID='1213'                      
   WHERE WI.CUSTOMER_ID = @CUSTOMERID AND WI.POLICY_ID = @POLID AND WI.POLICY_VERSION_ID = @VERSIONID AND                      
   WI.BOAT_ID =  @BOATID and wi.is_active='Y' AND ISNULL(TAI.IS_ACTIVE,'N')='Y' --AND (TAI.HOLDER_ID IS NULL OR (TAI.HOLDER_ID IS NOT NULL AND ISNULL(HL.IS_ACTIVE,'N') = 'Y'))                                        
   order by rank                      
  END                      
  ELSE                      
  BEGIN                      
   SELECT 1 AS BOAT_TRAILER,isnull(RANK,0) RANK,            
 CASE NATURE_OF_INTEREST             
 WHEN '11815' THEN 'AI'            
 WHEN '11866' THEN 'AI'            
 --WHEN '11867' THEN 'AI'            
 WHEN '11394' THEN 'AI'             
 --WHEN '11590' THEN 'AI'                        
 WHEN '11392' THEN 'LP'             
 WHEN '11393' THEN 'LP'            
 WHEN '11865' THEN 'LP'             
ELSE ''  END AS NATURE_OF_INTEREST,                      
   CASE isnull(hl.HOLDER_NAME,'')  WHEN '' THEN CAI.HOLDER_NAME ELSE HL.HOLDER_NAME END AS HOLDER_NAME,                      
    CASE isnull(hl.holder_id,'')                       
    when '' then RTRIM(ISNULL(CAI.HOLDER_ADD1,'')) +                       
     case CAI.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end +                       
     RTRIM(ISNULL(CAI.HOLDER_ADD2,''))/* +                      
     case CAI.HOLDER_ADD2            
      when '' then ''    
      else ', ' end   */    
               
    else                      
     RTRIM(ISNULL(HL.HOLDER_ADD1,''))+                       
     case HL.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end +                       
     RTRIM(ISNULL(HL.HOLDER_ADD2,'')) /*+               
     case HL.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end    */                   
    
    end AS ADDRESS,    
  
CASE isnull(hl.holder_id,'')                       
    when '' then RTRIM(ISNULL(CAI.HOLDER_ADD1,'')) +                       
     case CAI.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end +                       
     RTRIM(ISNULL(CAI.HOLDER_ADD2,''))/* +                      
     case CAI.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end   */                    
                           
    else                      
     RTRIM(ISNULL(HL.HOLDER_ADD1,''))+                       
     case HL.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end +                       
     RTRIM(ISNULL(HL.HOLDER_ADD2,'')) /*+               
     case HL.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end    */                   
    
    end AS ACCORDADDRESS,  
                     
    CASE isnull(hl.holder_id,'')                       
     when '' then                       
      RTRIM(ISNULL(CAI.HOLDER_CITY,'')) +                       
--      case CAI.HOLDER_CITY                       
--       when '' then ''                       
--       else ', ' end +                      
    ' '+  ISNULL(CSL1.STATE_CODE,'') +                       
      case CSL1.STATE_CODE                       
       when '' then ''                       
       else ' ' end +                       
      ISNULL(CAI.HOLDER_ZIP,'')                      
     else                      
      RTRIM(ISNULL(HL.HOLDER_CITY,''))+                       
--      case HL.HOLDER_CITY                       
--       when '' then ''                       
--       else ', ' end +                      
     ' '+ ISNULL(CSL.STATE_CODE,'') +                       
      case CSL.STATE_CODE                       
       when '' then ''                       
       else ' ' end +                       
      ISNULL(HL.HOLDER_ZIP,'')                      
     end as CITYSTATEZIP,     
 CASE isnull(hl.holder_id,'')                       
     when '' then                       
      RTRIM(ISNULL(CAI.HOLDER_CITY,'')) +                       
--      case CAI.HOLDER_CITY                       
--       when '' then ''                       
--       else ', ' end +                      
    ' '+  ISNULL(CSL1.STATE_CODE,'') +                       
      case CSL1.STATE_CODE                       
       when '' then ''                       
       else ' ' end +                       
      ISNULL(CAI.HOLDER_ZIP,'')                      
     else                      
      RTRIM(ISNULL(HL.HOLDER_CITY,''))+                       
--      case HL.HOLDER_CITY                       
--       when '' then ''                       
--       else ', ' end +                      
     ' '+ ISNULL(CSL.STATE_CODE,'') +                       
      case CSL.STATE_CODE                       
       when '' then ''                       
       else ' ' end +                       
      ISNULL(HL.HOLDER_ZIP,'')                      
     end as HOLDERCITYSTATEZIP,                      
                      
   ISNULL(LOAN_REF_NUMBER,'') AS LOAN_REF_NUMBER,CAST(WI.BOAT_NO AS VARCHAR) BOAT_NO,                      
   ISNULL(ML1.LOOKUP_VALUE_DESC,'') AS ADDLINTNAME,NATURE_OF_INTEREST,                    
   WI.BOAT_ID, CAI.ADD_INT_ID ,  
 'WAT' AS ADD_INT_NAME                       
   FROM POL_WATERCRAFT_INFO WI  WITH(NOLOCK)                    
  INNER JOIN POL_WATERCRAFT_COV_ADD_INT CAI WITH(NOLOCK) ON WI.POLICY_ID = CAI.POLICY_ID AND                      
    WI.POLICY_VERSION_ID = CAI.POLICY_VERSION_ID AND WI.CUSTOMER_ID = CAI.CUSTOMER_ID AND                       
    WI.BOAT_ID = CAI.BOAT_ID                      
   LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST HL WITH(NOLOCK) ON CAI.HOLDER_ID = HL.HOLDER_ID                      
   LEFT JOIN MNT_COUNTRY_STATE_LIST CSL WITH(NOLOCK) ON CSL.STATE_ID=HL.HOLDER_STATE                      
   LEFT JOIN MNT_COUNTRY_STATE_LIST CSL1 WITH(NOLOCK) ON CSL1.STATE_ID=CAI.HOLDER_STATE                      
   LEFT OUTER JOIN MNT_LOOKUP_VALUES ML1 WITH(NOLOCK) ON ML1.LOOKUP_UNIQUE_ID = NATURE_OF_INTEREST and LOOKUP_ID='1213'                      
   WHERE WI.CUSTOMER_ID = @CUSTOMERID AND WI.POLICY_ID = @POLID AND WI.POLICY_VERSION_ID = @VERSIONID                      
   and wi.is_active='Y'  AND ISNULL(CAI.IS_ACTIVE,'N')='Y'  --AND (CAI.HOLDER_ID IS NULL OR (CAI.HOLDER_ID IS NOT NULL AND ISNULL(HL.IS_ACTIVE,'N') = 'Y'))                                        
   UNION ALL                      
                         
   SELECT 2 AS BOAT_TRAILER,isnull(RANK,0) RANK,             
CASE NATURE_OF_INTEREST             
 WHEN '11815' THEN 'AI'             
 WHEN '11866' THEN 'AI'             
 --WHEN '11867' THEN 'AI'             
 WHEN '11394' THEN 'AI'             
 --WHEN '11590' THEN 'AI'                        
 WHEN '11392' THEN 'LP'            
 WHEN '11393' THEN 'LP'             
 WHEN '11865' THEN 'LP'             
ELSE '' END AS NATURE_OF_INTEREST,                      
   CASE isnull(hl.HOLDER_NAME,'')  WHEN '' THEN TAI.HOLDER_NAME ELSE HL.HOLDER_NAME END AS HOLDER_NAME,                      
   CASE isnull(hl.holder_id,'')                       
    when '' then RTRIM(ISNULL(TAI.HOLDER_ADD1,'')) +                       
     case TAI.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end +                       
     RTRIM(ISNULL(TAI.HOLDER_ADD2,''))/* +                       
 case TAI.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end                       
                         */  
    else                      
     RTRIM(ISNULL(HL.HOLDER_ADD1,''))+                       
     case HL.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end +                       
     RTRIM(ISNULL(HL.HOLDER_ADD2,'')) /*+                       
     case HL.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end  */                     
                      
    end AS ADDRESS,   
 CASE isnull(hl.holder_id,'')                       
    when '' then RTRIM(ISNULL(TAI.HOLDER_ADD1,'')) +                       
     case TAI.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end +                       
     RTRIM(ISNULL(TAI.HOLDER_ADD2,''))/* +                       
 case TAI.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end                       
                         */  
    else                      
     RTRIM(ISNULL(HL.HOLDER_ADD1,''))+                       
     case HL.HOLDER_ADD2                      
      when '' then ''                       
      else ', ' end +                       
     RTRIM(ISNULL(HL.HOLDER_ADD2,'')) /*+                       
     case HL.HOLDER_ADD2                       
      when '' then ''                       
      else ', ' end  */                     
                      
    end AS ACCORDADDRESS,                       
                        
    CASE isnull(hl.holder_id,'')                       
     when '' then                       
      RTRIM(ISNULL(TAI.HOLDER_CITY,'')) +                       
--      case TAI.HOLDER_CITY                       
--       when '' then ''                       
--       else ', ' end +    
' '   + ISNULL(CSL1.STATE_CODE,'') +        
      case CSL1.STATE_CODE                       
   when '' then ''                       
       else ' ' end +                       
      ISNULL(TAI.HOLDER_ZIP,'')                      
     else                      
      RTRIM(ISNULL(HL.HOLDER_CITY,''))+                       
--      case HL.HOLDER_CITY                       
--       when '' then ''                       
--       else ', ' end +                      
     ' '+ ISNULL(CSL.STATE_CODE,'') +                       
      case CSL.STATE_CODE                       
       when '' then ''                       
       else ' ' end +                 ISNULL(HL.HOLDER_ZIP,'')                      
     end as CITYSTATEZIP,  
  
  CASE isnull(hl.holder_id,'')                       
     when '' then                       
      RTRIM(ISNULL(TAI.HOLDER_CITY,'')) +                       
--      case TAI.HOLDER_CITY                       
--       when '' then ''                       
--       else ', ' end +    
' '   + ISNULL(CSL1.STATE_CODE,'') +                       
      case CSL1.STATE_CODE                       
       when '' then ''                       
       else ' ' end +                       
      ISNULL(TAI.HOLDER_ZIP,'')                      
     else                      
      RTRIM(ISNULL(HL.HOLDER_CITY,''))+                       
--      case HL.HOLDER_CITY                       
--       when '' then ''                       
--       else ', ' end +                      
     ' '+ ISNULL(CSL.STATE_CODE,'') +                       
      case CSL.STATE_CODE                       
       when '' then ''                       
       else ' ' end +                 ISNULL(HL.HOLDER_ZIP,'')                      
     end as HOLDERCITYSTATEZIP,  
  
ISNULL(LOAN_REF_NUMBER,'') AS LOAN_REF_NUMBER, ISNULL(CAST(WI.BOAT_NO AS VARCHAR),'') + '/' + ISNULL(CAST(WTI.TRAILER_NO AS VARCHAR),'') AS BOAT_NO,                      
   ISNULL(ML1.LOOKUP_VALUE_DESC,'') AS ADDLINTNAME,NATURE_OF_INTEREST,                    
   WTI.TRAILER_ID AS BOAT_ID, TAI.ADD_INT_ID ,  
 'WTRAL' AS ADD_INT_NAME                        
   FROM POL_WATERCRAFT_INFO WI  WITH(NOLOCK)                    
   INNER JOIN POL_WATERCRAFT_TRAILER_INFO WTI WITH(NOLOCK) ON WI.POLICY_ID = WTI.POLICY_ID AND                      
    WI.POLICY_VERSION_ID = WTI.POLICY_VERSION_ID AND WI.CUSTOMER_ID = WTI.CUSTOMER_ID AND                       
    WI.BOAT_ID = WTI.ASSOCIATED_BOAT                      
   INNER JOIN POL_WATERCRAFT_TRAILER_ADD_INT TAI WITH(NOLOCK) ON TAI.POLICY_ID = WTI.POLICY_ID AND                      
    TAI.POLICY_VERSION_ID = WTI.POLICY_VERSION_ID AND TAI.CUSTOMER_ID = WTI.CUSTOMER_ID AND                       
    TAI.TRAILER_ID = WTI.TRAILER_ID                       
   LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST HL WITH(NOLOCK) ON TAI.HOLDER_ID = HL.HOLDER_ID           
   LEFT JOIN MNT_COUNTRY_STATE_LIST CSL WITH(NOLOCK) ON CSL.STATE_ID=HL.HOLDER_STATE                      
   LEFT JOIN MNT_COUNTRY_STATE_LIST CSL1 WITH(NOLOCK) ON CSL1.STATE_ID=TAI.HOLDER_STATE                      
   LEFT OUTER JOIN MNT_LOOKUP_VALUES ML1 WITH(NOLOCK) ON ML1.LOOKUP_UNIQUE_ID = NATURE_OF_INTEREST and LOOKUP_ID='1213'                      
   WHERE WI.CUSTOMER_ID = @CUSTOMERID AND WI.POLICY_ID = @POLID AND WI.POLICY_VERSION_ID = @VERSIONID                      
   and wi.is_active='Y' AND ISNULL(TAI.IS_ACTIVE,'N')='Y' --AND (TAI.HOLDER_ID IS NULL OR (TAI.HOLDER_ID IS NOT NULL AND ISNULL(HL.IS_ACTIVE,'N') = 'Y'))                                        
   order by rank                      
  END                      
 END                       
END             
  
GO

