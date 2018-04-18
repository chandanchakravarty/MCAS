IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAPP_UMBRELLA_LIMITS1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAPP_UMBRELLA_LIMITS1]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*          
----------------------------------------------------------              
Proc Name       : dbo.Proc_InsertAPP_UMBRELA_LIMITS1          
Created by      : Pradeep            
Date            : 26 May,2005              
Purpose         : Inserts a record into UMBRELLA_LIITS              
Revison History :              
Used In         : Wolverine         
      
Modified By     : Mohit Gupta      
Modified On     : 20/10/2005      
Purpose         : removing precision from the size of decimal type parameter.       
    
Modified By  : Ravindra        
Modified On : 03-24-206    
Purpose  : Setting IS_ACTIVE filed to 'Y'     

Modified by	:Pravesh K Chandel
Date		:13 june 2007
purpopse	: Add new parameter @CLIENT_UPDATE_DATE
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------             
*/          
    
--drop proc dbo.Proc_InsertAPP_UMBRELLA_LIMITS1    
          
CREATE       PROCEDURE dbo.Proc_InsertAPP_UMBRELLA_LIMITS1          
(          
 @CUSTOMER_ID int,          
 @APP_ID int,          
 @APP_VERSION_ID smallint,          
 @POLICY_LIMITS     decimal(18),          
 @RETENTION_LIMITS     decimal(18),          
 @UNINSURED_MOTORIST_LIMIT     decimal(18),          
 @UNDERINSURED_MOTORIST_LIMIT     decimal(18),          
 @OTHER_LIMIT     decimal(18),          
 @OTHER_DESCRIPTION     nvarchar(130),          
 @CREATED_BY int,  
 @TERRITORY int  ,         
  @CLIENT_UPDATE_DATE datetime =null             
)          
          
As          
          
--IF NOT EXISTS          
--(          
-- SELECT * FROM APP_UMBRELLA_LIMITS          
-- WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
--  APP_ID = @APP_ID AND          
--  APP_VERSION_ID = @APP_VERSION_ID          
--)          
BEGIN          
 INSERT INTO APP_UMBRELLA_LIMITS          
 (          
  CUSTOMER_ID,          
  APP_ID,          
  APP_VERSION_ID,          
  POLICY_LIMITS,          
  RETENTION_LIMITS,          
  UNINSURED_MOTORIST_LIMIT,          
  UNDERINSURED_MOTORIST_LIMIT,          
  OTHER_LIMIT,          
  OTHER_DESCRIPTION,          
  CREATED_BY,          
  CREATED_DATETIME   ,    
  IS_ACTIVE,  
  TERRITORY ,
  CLIENT_UPDATE_DATE 
 )          
 VALUES          
 (          
  @CUSTOMER_ID,          
  @APP_ID,          
  @APP_VERSION_ID,          
  @POLICY_LIMITS,          
  @RETENTION_LIMITS,          
  @UNINSURED_MOTORIST_LIMIT,          
  @UNDERINSURED_MOTORIST_LIMIT,          
  @OTHER_LIMIT,          
  @OTHER_DESCRIPTION,          
  @CREATED_BY,          
  GetDate()      ,    
  'Y',  
  @TERRITORY    ,
  @CLIENT_UPDATE_DATE 
 )          
           
 RETURN 1          
END          
        
        
   
  
        
          
          
          
          
          
          
          
          
        
      
    
  





GO

