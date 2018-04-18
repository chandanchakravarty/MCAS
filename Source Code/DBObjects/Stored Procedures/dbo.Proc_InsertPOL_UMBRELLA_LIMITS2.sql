IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPOL_UMBRELLA_LIMITS2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPOL_UMBRELLA_LIMITS2]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*    
----------------------------------------------------------        
Proc Name       : dbo.Proc_InsertPOL_UMBRELLA_LIMITS2    
Created by      : Ravindra
Date            : 03-22-2006
Purpose         : Inserts a record into UMBRELLA_LIITS        
Revison History :        
Used In         : Wolverine     
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------       
*/    
    
CREATE       PROCEDURE Proc_InsertPOL_UMBRELLA_LIMITS2    
(    
 @CUSTOMER_ID int,    
 @POLICY_ID int,    
 @POLICY_VERSION_ID smallint,    
 @BASIC     decimal(18),    
 @RESIDENCES_OWNER_OCCUPIED     decimal(18),    
 @NUM_OF_RENTAL_UNITS     int,    
 @RENTAL_UNITS     decimal(18),    
 @NUM_OF_AUTO     int,    
 @AUTOMOBILES     decimal(18),    
 @NUM_OF_OPERATORS     int,    
 @OPER_UNDER_AGE     decimal(18),    
 @NUM_OF_UNLIC_RV     int,    
 @UNLIC_RV     decimal(18),    
 @NUM_OF_UNINSU_MOTORIST     int,    
 @UNISU_MOTORIST     decimal(18),    
 @UNDER_INSURED_MOTORIST     int,    
 @WATERCRAFT     decimal(18),    
 @NUM_OF_OTHER     int,    
 @OTHER     decimal(18),    
 @DEPOSIT     decimal(18),    
 @ESTIMATED_TOTAL_PRE     decimal(18),    
 @CALCULATIONS     NVarChar(100),    
 @CREATED_BY     int    
)    
    
As    
    
IF NOT EXISTS    
(    
 SELECT POLICY_ID FROM POL_UMBRELLA_LIMITS    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
  POLICY_ID = @POLICY_ID AND    
  POLICY_VERSION_ID = @POLICY_VERSION_ID    
)    
BEGIN    
 INSERT INTO POL_UMBRELLA_LIMITS    
 (    
  CUSTOMER_ID,    
  POLICY_ID,    
  POLICY_VERSION_ID,    
  BASIC,    
  RESIDENCES_OWNER_OCCUPIED,    
  NUM_OF_RENTAL_UNITS,    
  RENTAL_UNITS,    
  NUM_OF_AUTO,    
  AUTOMOBILES,    
  NUM_OF_OPERATORS,    
  OPER_UNDER_AGE,    
  NUM_OF_UNLIC_RV,    
  UNLIC_RV,    
  NUM_OF_UNINSU_MOTORIST,    
  UNISU_MOTORIST,    
  UNDER_INSURED_MOTORIST,    
  WATERCRAFT,    
  NUM_OF_OTHER,    
  OTHER,    
  DEPOSIT,    
  ESTIMATED_TOTAL_PRE,    
  CALCULATIONS,    
  IS_ACTIVE,    
  CREATED_BY,    
  CREATED_DATETIME    
      
 )    
 VALUES    
 (    
  @CUSTOMER_ID,    
  @POLICY_ID,    
  @POLICY_VERSION_ID,    
  @BASIC,    
  @RESIDENCES_OWNER_OCCUPIED,    
  @NUM_OF_RENTAL_UNITS,    
  @RENTAL_UNITS,    
  @NUM_OF_AUTO,    
  @AUTOMOBILES,    
  @NUM_OF_OPERATORS,    
  @OPER_UNDER_AGE,    
  @NUM_OF_UNLIC_RV,    
  @UNLIC_RV,    
  @NUM_OF_UNINSU_MOTORIST,    
  @UNISU_MOTORIST,    
  @UNDER_INSURED_MOTORIST,    
  @WATERCRAFT,    
  @NUM_OF_OTHER,    
  @OTHER,    
  @DEPOSIT,    
  @ESTIMATED_TOTAL_PRE,    
  @CALCULATIONS,    
  'Y',    
  @CREATED_BY,    
  GetDate()    
 )    
    
     
 RETURN 1    
END    
    
    
    
    
    
    
    
    
  



GO

