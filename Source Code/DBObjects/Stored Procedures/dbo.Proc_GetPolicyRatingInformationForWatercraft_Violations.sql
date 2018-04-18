IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyRatingInformationForWatercraft_Violations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyRatingInformationForWatercraft_Violations]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO




--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    
       
/*------------------------------------------------------------------------------------------------------------------------------------------------        
Proc Name            :  Proc_GetPolicyRatingInformationForWatercraft_Violations                
Created by           :  SHAFI      
Date                 :  16 March 2006            
Purpose              :  To get the information for creating the input xml for Watercraft                
Revison History      :                
Used In              :  Wolverine                
---------------------------------------------------------------------------------------------------------------------------------------------        
Date     Review By          Comments                
--------------------------------------------------------------------------------------------------------------------------------------------------        
            
              
*/       -- drop proc  dbo.Proc_GetPolicyRatingInformationForWatercraft_Violations       
        
CREATE PROC dbo.Proc_GetPolicyRatingInformationForWatercraft_Violations               
(                
 @CUSTOMERID    int,                
 @policyID     int,                
 @policyVERSIONID  int,                
 @DRIVERID     int,              
 --@VIOLATIONID  int              
 @POL_WATER_MVR_ID int             
)                
AS                
                
BEGIN                
                
set quoted_identifier off                
                
 DECLARE @VIODATE         varchar(100)          
          
 DECLARE @VIOLATIONTYPE    varchar(100)    --Violation DESC      
 DECLARE @VIOLATIONTYPEID      varchar(100)    --Violation Type ID
 DECLARE @VIOLATIONTYPE_IN    	varchar(100)    --VIOLATION DESC  
 DECLARE @VIOLATIONTYPE_EXT    	varchar(100)    --VIOLATION DESC        
 DECLARE @VIOLATIONDESC    varchar(100) 
 DECLARE @VIOLATIONDESC_IN    	varchar(100)
 DECLARE @VIOLATIONDESC_EXT    	varchar(100)        
 DECLARE @VIOLATIONDESCID       varchar(100)    --Violation Description  ID      
 DECLARE @MVRPOINTS        varchar(100)    --MVR POINTS  ()      
 DECLARE @WOLVERINE_VIOLATIONS  varchar(100)    --VIOLATION POINTS (SD POINTS)      
 DECLARE @VIOLATION_CODE        varchar(100)    --VIOLATION CODE ()          
 DECLARE @AMOUNTPAID      varchar(100)         
           
 DECLARE @DEATH           varchar(100)                  
                
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------                
--Violation Description START                
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------                
               
 /*SELECT         
 @VIOLATIONTYPE = ISNULL(LOOKUP_VALUE_DESC,'')                  
 FROM         
 POL_WATERCRAFT_MVR_INFORMATION WITH (NOLOCK) INNER JOIN MNT_LOOKUP_VALUES   WITH (NOLOCK)      
 ON         
 LOOKUP_UNIQUE_ID = VIOLATION_ID            */    
          
---------    
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------                
SELECT        
    @VIOLATIONTYPEID = ISNULL(VIOLATION_TYPE,'0'),      
    @VIOLATIONDESCID = ISNULL(VIOLATION_ID,'0'),             
    @AMOUNTPAID =  ISNULL(MVR_AMOUNT,'0'),                                
    @DEATH  =  ISNULL(MVR_DEATH,'N') ,                   
    @VIODATE  =  ISNULL(convert(varchar(10),MVR_DATE ,101),''),
    @MVRPOINTS = ISNULL(POINTS_ASSIGNED,0) + ISNULL(ADJUST_VIOLATION_POINTS,0),
    @WOLVERINE_VIOLATIONS =  ISNULL(POINTS_ASSIGNED,0) + ISNULL(ADJUST_VIOLATION_POINTS,0),
    @VIOLATIONTYPE_EXT	=  ISNULL(DETAILS,''),
    @VIOLATIONDESC_EXT  =  ISNULL(DETAILS,'')            
FROM          
 POL_WATERCRAFT_MVR_INFORMATION WITH (NOLOCK)               
WHERE         
 CUSTOMER_ID =@CUSTOMERID AND POLICY_ID=@policyID AND POLICY_VERSION_ID=@policyVERSIONID AND DRIVER_ID = @DRIVERID AND APP_WATER_MVR_ID = @POL_WATER_MVR_ID      
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------                
      
      
----FETCHING THE DATA FROM MNT_VIOLATION ACCORDING TO THE VIOLATION DESC ID ----------      
SELECT      
   @VIOLATIONTYPE_IN 		= ISNULL(VIOLATION_DES,''),      
   @VIOLATIONDESC_IN 		= ISNULL(VIOLATION_DES,''),
--@VIOLATIONTYPE = ISNULL(VIOLATION_DES,''),      
--@VIOLATION_ID = VIOLATIONID,      
   @VIOLATIONDESC =  ISNULL(VIOLATION_DES,''),      
  -- @MVRPOINTS = ISNULL(MVR_POINTS,'0'),      
  -- @WOLVERINE_VIOLATIONS = ISNULL(SD_POINTS,'0'),      
   @VIOLATION_CODE = ISNULL(VIOLATION_CODE,'0')      
      
 FROM MNT_VIOLATIONS WHERE VIOLATION_ID = @VIOLATIONDESCID      

IF (@VIOLATIONTYPE_IN IS NULL OR @VIOLATIONTYPE_IN ='0' OR LTRIM(RTRIM(@VIOLATIONTYPE_IN)) ='' )
BEGIN
 SET @VIOLATIONTYPE = @VIOLATIONTYPE_EXT
END
ELSE
BEGIN
 SET @VIOLATIONTYPE = @VIOLATIONTYPE_IN
END
IF (@VIOLATIONDESC_IN IS NULL OR @VIOLATIONDESC_IN ='0' OR LTRIM(RTRIM(@VIOLATIONDESC_IN)) ='' )
BEGIN
 SET @VIOLATIONDESC = @VIOLATIONDESC_EXT
END
ELSE
BEGIN
 SET @VIOLATIONDESC = @VIOLATIONDESC_IN
END       
                  
----------END FETCHING----------------      
    
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------                
/*SELECT                 
    @AMOUNTPAID =  ISNULL(MVR_AMOUNT,'0'),                                
    @DEATH  =  ISNULL(MVR_DEATH,'N') ,                   
    @VIODATE  =  ISNULL(convert(varchar(10),MVR_DATE ,101),'')          
FROM          
 POL_WATERCRAFT_MVR_INFORMATION  WITH (NOLOCK)              
WHERE         
 CUSTOMER_ID =@CUSTOMERID AND POLICY_ID=@policyID AND POLICY_VERSION_ID=@policyVERSIONID AND DRIVER_ID = @DRIVERID */             
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------                
           
SELECT                
  @VIODATE      AS VIODATE,                 
  ISNULL(@VIOLATIONTYPE,'0')  AS VIOLATIONTYPE,      
  ISNULL(@VIOLATIONTYPEID,'0')  AS VIOLATIONID,      
  ISNULL(@VIOLATIONDESC,'0')  AS VIOLATIONDESC,      
  ISNULL(@VIOLATIONDESCID,'0')  AS VIOLATIONDESCID,      
  ISNULL(@MVRPOINTS,'0')    AS MVRPOINTS,      
  ISNULL(@WOLVERINE_VIOLATIONS,'0') AS WOLVERINE_VIOLATIONS,      
  ISNULL(@VIOLATION_CODE,'0')   AS VIOLATION_CODE,      
  ISNULL(@AMOUNTPAID,'0')     AS AMOUNTPAID,                
  ISNULL(@DEATH,'0')       AS DEATH                
END                
                
                
              
          
        
      
      
      
    
  
  





GO

