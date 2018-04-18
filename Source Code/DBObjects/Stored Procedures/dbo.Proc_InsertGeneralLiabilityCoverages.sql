IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertGeneralLiabilityCoverages]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertGeneralLiabilityCoverages]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_InsertGeneralLiabilityCoverages        
Created by      : Ravindra       
Date            : 03-28-2006    
Purpose         : To add record in APP_GENERAL_COVERAGE_LIMITS table    
Revison History :        
Used In         :   Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
--- drop proc Proc_InsertGeneralLiabilityCoverages
CREATE PROC dbo.Proc_InsertGeneralLiabilityCoverages
(        
  @CUSTOMER_ID              int,        
  @APP_ID                int,        
  @APP_VERSION_ID        smallint ,      
  @COVERAGE_L_AMOUNT Decimal(18,0),
  @COVERAGE_L_ID Int,
  @COVERAGE_L_AGGREGATE decimal,
  @COVERAGE_O_AMOUNT decimal,
  @COVERAGE_O_ID Int,
  @COVERAGE_O_AGGREGATE decimal,
  @COVERAGE_M_EACH_PERSON_AMOUNT decimal,
  @COVERAGE_M_EACH_PERSON_ID Int,
  @COVERAGE_M_EACH_OCC_AMOUNT Decimal,
  @COVERAGE_M_EACH_OCC_ID Int,
  @TOTAL_GENERAL_AGGREGATE Decimal,
  @CREATED_BY int,
  @CREATED_DATETIME datetime
)        
AS        
       
BEGIN      
        
 If Exists(SELECT CUSTOMER_ID FROM APP_GENERAL_COVERAGE_LIMITS     
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
  	APP_ID = @APP_ID AND      
	APP_VERSION_ID = @APP_VERSION_ID )       
   
	 RETURN -2  
      
        
 INSERT INTO APP_GENERAL_COVERAGE_LIMITS     
  (    
  CUSTOMER_ID,
  APP_ID     ,
  APP_VERSION_ID    ,
  COVERAGE_L_AMOUNT ,
  COVERAGE_L_ID ,
  COVERAGE_L_AGGREGATE ,
  COVERAGE_O_AMOUNT ,
  COVERAGE_O_ID ,
  COVERAGE_O_AGGREGATE ,
  COVERAGE_M_EACH_PERSON_AMOUNT ,
  COVERAGE_M_EACH_PERSON_ID ,
  COVERAGE_M_EACH_OCC_AMOUNT,
  COVERAGE_M_EACH_OCC_ID,
  TOTAL_GENERAL_AGGREGATE,     
  IS_ACTIVE,
  CREATED_BY,                 
  CREATED_DATETIME    
 )           
  values        
 (        
  @CUSTOMER_ID,
  @APP_ID     ,
  @APP_VERSION_ID    ,
  @COVERAGE_L_AMOUNT ,
  @COVERAGE_L_ID ,
  @COVERAGE_L_AGGREGATE ,
  @COVERAGE_O_AMOUNT ,
  @COVERAGE_O_ID ,
  @COVERAGE_O_AGGREGATE ,
  @COVERAGE_M_EACH_PERSON_AMOUNT ,
  @COVERAGE_M_EACH_PERSON_ID ,
  @COVERAGE_M_EACH_OCC_AMOUNT,
  @COVERAGE_M_EACH_OCC_ID,
  @TOTAL_GENERAL_AGGREGATE,     
  'Y',
  @CREATED_BY,                 
  @CREATED_DATETIME     
 )        
                      
END      
      
  
      
      
      
      
    
  



GO

