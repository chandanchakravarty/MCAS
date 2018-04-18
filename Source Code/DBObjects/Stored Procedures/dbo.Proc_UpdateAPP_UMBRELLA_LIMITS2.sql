IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAPP_UMBRELLA_LIMITS2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAPP_UMBRELLA_LIMITS2]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*  
----------------------------------------------------------      
Proc Name       : dbo.Proc_UpdateAPP_UMBRELA_LIMITS2  
Created by      : Pradeep    
Date            : 26 May,2005      
Purpose         : Updates a record into UMBRELLA_LIITS      
Revison History :      
Used In         : Wolverine  
Modified By     : Mohit Gupta
Modified On     : 20/10/2005
Purpose         : removing scale from the decimal type field.    
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------     
*/  
  
CREATE         PROCEDURE Proc_UpdateAPP_UMBRELLA_LIMITS2  
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
 @UNDER_INSURED_MOTORIST     decimal(18),  
 @WATERCRAFT     decimal(18),  
 @NUM_OF_OTHER     int,  
 @OTHER     decimal(18),  
 @DEPOSIT     decimal(18),  
 @ESTIMATED_TOTAL_PRE     decimal(18),  
 @CALCULATIONS     NVarChar(100),  
 @MODIFIED_BY     int  
  
  
  
)  
  
As  
  
IF  EXISTS  
(  
 SELECT * FROM APP_UMBRELLA_LIMITS  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
  APP_ID = @APP_ID AND  
  APP_VERSION_ID = @APP_VERSION_ID  
)  
BEGIN  
 UPDATE APP_UMBRELLA_LIMITS  
 SET  
    
  BASIC = @BASIC,  
  RESIDENCES_OWNER_OCCUPIED = @RESIDENCES_OWNER_OCCUPIED,  
  NUM_OF_RENTAL_UNITS = @NUM_OF_RENTAL_UNITS,  
  RENTAL_UNITS = @RENTAL_UNITS,  
  NUM_OF_AUTO = @NUM_OF_AUTO,  
  AUTOMOBILES = @AUTOMOBILES,  
  NUM_OF_OPERATORS = @NUM_OF_OPERATORS,  
  OPER_UNDER_AGE = @OPER_UNDER_AGE,  
  NUM_OF_UNLIC_RV = @NUM_OF_UNLIC_RV,  
  UNLIC_RV = @UNLIC_RV,  
  NUM_OF_UNINSU_MOTORIST = @NUM_OF_UNINSU_MOTORIST,  
  UNISU_MOTORIST = @UNISU_MOTORIST,  
  UNDER_INSURED_MOTORIST = @UNDER_INSURED_MOTORIST,  
  WATERCRAFT = @WATERCRAFT,  
  NUM_OF_OTHER = @NUM_OF_OTHER,  
  OTHER = @OTHER,  
  DEPOSIT = @DEPOSIT,  
  ESTIMATED_TOTAL_PRE = @ESTIMATED_TOTAL_PRE ,  
  CALCULATIONS = @CALCULATIONS,  
  MODIFIED_BY = @MODIFIED_BY,  
  LAST_UPDATED_DATETIME = GetDate()  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
  APP_ID = @APP_ID AND  
  APP_VERSION_ID = @APP_VERSION_ID  
   
 RETURN 1  
END  
  
  
  
  
  
  
  
  
  
  
  



GO

