IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAPP_UMBRELLA_LIMITS2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAPP_UMBRELLA_LIMITS2]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*  
----------------------------------------------------------      
Proc Name       : dbo.Proc_InsertAPP_UMBRELA_LIMITS2  
Created by      : Pradeep    
Date            : 26 May,2005      
Purpose         : Inserts a record into UMBRELLA_LIITS      
Revison History :      
Used In         : Wolverine   
Modified By     : Mohit Gupta.
Modified On     : 20/10/2005.
Purpose         : removing scale from the decimal type fields   
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------     
*/  
  
CREATE       PROCEDURE Proc_InsertAPP_UMBRELLA_LIMITS2  
(  
 @CUSTOMER_ID int,  
 @APP_ID int,  
 @APP_VERSION_ID smallint,  
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
 SELECT * FROM APP_UMBRELLA_LIMITS  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
  APP_ID = @APP_ID AND  
  APP_VERSION_ID = @APP_VERSION_ID  
)  
BEGIN  
 INSERT INTO APP_UMBRELLA_LIMITS  
 (  
  CUSTOMER_ID,  
  APP_ID,  
  APP_VERSION_ID,  
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
  @APP_ID,  
  @APP_VERSION_ID,  
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

