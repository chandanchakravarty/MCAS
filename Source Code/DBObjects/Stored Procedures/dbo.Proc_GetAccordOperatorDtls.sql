IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAccordOperatorDtls]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAccordOperatorDtls]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[Proc_GetAccordOperatorDtls]              
(            
 @CUSTOMERID   int,            
 @POLID                int,            
 @VERSIONID   int,            
 @CALLEDFROM  VARCHAR(20)            
-- @VEHICLEID  int=0            
)            
AS            
BEGIN            
 IF (@CALLEDFROM='APPLICATION')            
 BEGIN            
      
   SELECT DISTINCT            
    --LTRIM(RTRIM(STR(BOAT_NO))) + '/' +         
    LTRIM(RTRIM(STR(DD.DRIVER_ID))) OPERATORNO,            
    DRIVER_FNAME + ' ' + DRIVER_MNAME + ' ' + DRIVER_LNAME + ' ' + DRIVER_SUFFIX DRIVER_NAME,DRIVER_SEX,CONVERT(VARCHAR(11),DRIVER_DOB,101) DRIVER_DOB,            
    DRIVER_DRIV_LIC,DRIVER_LIC_STATE,STATE_NAME,DRIVER_SSN,MARITAL_STATUS      
  
   FROM  APP_WATERCRAFT_DRIVER_DETAILS DD             
     INNER JOIN MNT_COUNTRY_STATE_LIST ON STATE_ID=DRIVER_LIC_STATE            
   WHERE DD.CUSTOMER_ID=@CUSTOMERID AND DD.APP_ID=@POLID AND DD.APP_VERSION_ID=@VERSIONID AND DD.IS_ACTIVE='Y'            
   ORDER BY OPERATORNO--,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME            
  END            
 --END            
 ELSE IF (@CALLEDFROM='POLICY')            
 BEGIN            
              
  
   SELECT DISTINCT            
    --LTRIM(RTRIM(STR(BOAT_NO))) + '/' +         
    LTRIM(RTRIM(STR(DD.DRIVER_ID))) OPERATORNO,            
    DRIVER_FNAME + ' ' + DRIVER_MNAME + ' ' + DRIVER_LNAME DRIVER_NAME,DRIVER_SEX,CAST(DRIVER_DOB AS VARCHAR(11)) DRIVER_DOB,            
    DRIVER_DRIV_LIC,DRIVER_LIC_STATE,STATE_NAME,DRIVER_SSN,MARITAL_STATUS     
    FROM  POL_WATERCRAFT_DRIVER_DETAILS dd  with(nolock)          
    INNER JOIN MNT_COUNTRY_STATE_LIST  with(nolock) ON STATE_ID=DRIVER_LIC_STATE            
   WHERE DD.CUSTOMER_ID=@CUSTOMERID AND DD.POLICY_ID=@POLID AND DD.POLICY_VERSION_ID=@VERSIONID AND DD.IS_ACTIVE='Y'            
   ORDER BY OPERATORNO--BOAT_NO,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME          
  
 END            
END        
    
    
    
  
  



---------------- End of Neeraj's scripts -----------------


GO

