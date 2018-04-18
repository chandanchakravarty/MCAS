IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetSelectedPolicyDetailsInformationForUMB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetSelectedPolicyDetailsInformationForUMB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

/*----------------------------------------------------------                                                                        
Proc Name           : Proc_GetSelectedPolicyDetailsInformationForUMB                                                              
Created by          : Neeraj singh                                                                       
Date                : 10-01-2006                                                                        
Purpose             : To get the information for creating the input xml                                                                         
Revison History     :                                                                        
Used In             : Wolverine                                                                        
------------------------------------------------------------                                                                        
Date     Review By          Comments                                                                        
------   ------------       -------------------------*/                  
--   drop proc Proc_GetSelectedPolicyDetailsInformationForUMB     
                                  
CREATE     PROC dbo.Proc_GetSelectedPolicyDetailsInformationForUMB      
 (        
 @POLICY_NUMBER NVARCHAR(20),       
 @POLICY_COMPANY NVARCHAR(20),      
 @IS_POLICY NVARCHAR(20)       
 )        
        
AS        
BEGIN        
SET QUOTED_IDENTIFIER OFF        
        
DECLARE @ID INT        
DECLARE @VERSION_ID SMALLINT        
DECLARE @LOB NVARCHAR(20)        
DECLARE @DATAACCESSPOINT INT        
      
IF(LTRIM(RTRIM(@POLICY_COMPANY))='Wolverine')      
 BEGIN      
  IF(@IS_POLICY ='1')       
   BEGIN       
        -- FETCH POLICY DETAILS FOR WOLVERINE CUSTOMER        
      select        
           @ID = POLICY_ID,                  
            @VERSION_ID = POLICY_VERSION_ID,                  
           @LOB = POLICY_LOB                  
      FROM POL_CUSTOMER_POLICY_LIST                    
           WHERE SUBSTRING(POLICY_NUMBER,1,8) =  ltrim(rtrim(substring(@POLICY_NUMBER,1,8) ))                  
           and POLICY_DISP_VERSION =  ltrim(rtrim(substring(@POLICY_NUMBER,12,3) ))        
          
       SET @DATAACCESSPOINT=1        
         
   END      
       
        
  IF(@IS_POLICY ='0')       
   BEGIN       
     -- FETCH APPLICTION DETAILS FOR WOLVERINE CUSTOMER        
      select        
           @ID = APP_ID,                  
                 @VERSION_ID = APP_VERSION_ID,                  
           @LOB = APP_LOB                  
      FROM APP_LIST                    
           WHERE SUBSTRING(APP_NUMBER,1,11) =  ltrim(rtrim(substring(@POLICY_NUMBER,1,11) ))                  
           and APP_VERSION =  ltrim(rtrim(substring(@POLICY_NUMBER,15,3) ))        
         
     SET @DATAACCESSPOINT=2        
   END        
 END       
ELSE      
 BEGIN      
        
         
     SELECT         
      @ID = APP_ID,                  
                 @VERSION_ID = APP_VERSION_ID,                  
            @LOB = POLICY_LOB         
     FROM  APP_UMBRELLA_UNDERLYING_POLICIES        
     WHERE POLICY_NUMBER=@POLICY_NUMBER         
         
   SET @DATAACCESSPOINT=3        
 END        
          
END         
BEGIN        
SELECT        
@ID AS IDS,        
@VERSION_ID AS VERSION_ID,        
@LOB AS LOB,        
@DATAACCESSPOINT AS DATAACCESSPOINT        
END        
SET QUOTED_IDENTIFIER OFF         
      
    
  


GO

