IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Copy_APP_VEHICLE_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Copy_APP_VEHICLE_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


----------------------------------------------------

   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
---   
  --drop proc dbo.Proc_Copy_APP_VEHICLE_COVERAGES
/*----------------------------------------------------------            
Proc Name   : dbo.Proc_Copy_APP_VEHICLE_COVERAGES  
Created by  : Pradeep  
Date        : 18 October,2005  
Purpose     : Get the Vehicle IDs for coying coverages            
Revison History  :                  
 ------------------------------------------------------------                        
Date     Review By          Comments                      
             
------   ------------       -------------------------*/            
CREATE PROCEDURE dbo.Proc_Copy_APP_VEHICLE_COVERAGES  
(  
 @VEHICLE_ID_FROM smallint,  
 @VEHICLE_ID_TO smallint,  
 @CUSTOMER_ID int,  
 @APP_ID int,  
 @APP_VERSION_ID int  
)      
AS           
BEGIN            
  
  
 IF NOT EXISTS  
 (  
  SELECT * FROM APP_VEHICLE_COVERAGES  
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND   
  APP_ID = @APP_ID AND   
  APP_VERSION_ID = @APP_VERSION_ID  AND  
  VEHICLE_ID = @VEHICLE_ID_FROM   
 )  
 BEGIN  
  RETURN -1  
 END  
   
 --Delete existing coverages for the vehicle  
 IF EXISTS  
 (  
  SELECT * FROM APP_VEHICLE_COVERAGES  
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND   
  APP_ID = @APP_ID AND   
  APP_VERSION_ID = @APP_VERSION_ID  AND  
  VEHICLE_ID = @VEHICLE_ID_TO  
 )  
 BEGIN  
  DELETE FROM APP_VEHICLE_COVERAGES  
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND   
  APP_ID = @APP_ID AND   
  APP_VERSION_ID = @APP_VERSION_ID  AND  
  VEHICLE_ID = @VEHICLE_ID_TO  
   
 END  
   
 --Delete existing endorsements if exists  
 IF EXISTS  
 (  
  SELECT * FROM APP_VEHICLE_ENDORSEMENTS  
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND   
   APP_ID = @APP_ID AND   
   APP_VERSION_ID = @APP_VERSION_ID  AND  
   VEHICLE_ID = @VEHICLE_ID_TO   
 )  
 BEGIN  
 DELETE FROM APP_VEHICLE_ENDORSEMENTS  
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND   
   APP_ID = @APP_ID AND   
   APP_VERSION_ID = @APP_VERSION_ID  AND  
   VEHICLE_ID = @VEHICLE_ID_TO   
 END  
   
   
 --Copy coverages  
 INSERT INTO APP_VEHICLE_COVERAGES  
 (  
  CUSTOMER_ID,  
  APP_ID,  
  APP_VERSION_ID,  
  VEHICLE_ID,  
  COVERAGE_ID,  
  COVERAGE_CODE_ID,  
  LIMIT_OVERRIDE,  
  LIMIT_1,  
  LIMIT_1_TYPE,  
  LIMIT_2,  
  LIMIT_2_TYPE,  
  DEDUCT_OVERRIDE,  
  DEDUCTIBLE_1,  
  DEDUCTIBLE_1_TYPE,  
  DEDUCTIBLE_2,  
  DEDUCTIBLE_2_TYPE,  
  WRITTEN_PREMIUM,  
  FULL_TERM_PREMIUM,  
  IS_SYSTEM_COVERAGE,  
  LIMIT1_AMOUNT_TEXT,  
  LIMIT2_AMOUNT_TEXT,  
  DEDUCTIBLE1_AMOUNT_TEXT,  
  DEDUCTIBLE2_AMOUNT_TEXT


 )  
 SELECT CUSTOMER_ID,  
  APP_ID,  
  APP_VERSION_ID,  
  @VEHICLE_ID_TO,  
  COVERAGE_ID,  
  COVERAGE_CODE_ID,  
  LIMIT_OVERRIDE,  
  LIMIT_1,  
  LIMIT_1_TYPE,  
  LIMIT_2,  
  LIMIT_2_TYPE,  
  DEDUCT_OVERRIDE,  
  DEDUCTIBLE_1,  
  DEDUCTIBLE_1_TYPE,  
  DEDUCTIBLE_2,  
  DEDUCTIBLE_2_TYPE,  
  WRITTEN_PREMIUM,  
  FULL_TERM_PREMIUM,  
  IS_SYSTEM_COVERAGE,  
  LIMIT1_AMOUNT_TEXT,  
  LIMIT2_AMOUNT_TEXT,  
  DEDUCTIBLE1_AMOUNT_TEXT,  
  DEDUCTIBLE2_AMOUNT_TEXT 


 FROM  APP_VEHICLE_COVERAGES  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND   
  APP_ID = @APP_ID AND   
  APP_VERSION_ID = @APP_VERSION_ID  AND  
  VEHICLE_ID = @VEHICLE_ID_FROM  
       
End  
  
  
  
GO

