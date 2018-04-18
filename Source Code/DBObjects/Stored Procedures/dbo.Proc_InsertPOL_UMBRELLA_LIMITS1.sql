IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPOL_UMBRELLA_LIMITS1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPOL_UMBRELLA_LIMITS1]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*  ----------------------------------------------------------              
Proc Name       : dbo.Proc_InsertPOL_UMBRELLA_LIMITS1          
Created by      : Ravindra    
Date            : 03-22-2006    
Purpose         : Inserts a record into UMBRELLA_LIITS              
Revison History :              
Modified By 	:pravesh k Chandel
Date		:13 june 2007
Purpose		: add new Parmeter @CLIENT_UPDATE_DATE to save 
Used In         : Wolverine         
     
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------             
*/          
  
--drop proc dbo.Proc_InsertPOL_UMBRELLA_LIMITS1  
          
CREATE PROCEDURE dbo.Proc_InsertPOL_UMBRELLA_LIMITS1          
(          
 @CUSTOMER_ID int,          
 @POLICY_ID int,          
 @POLICY_VERSION_ID smallint,          
 @POLICY_LIMITS     decimal(18),          
 @RETENTION_LIMITS     decimal(18),          
 @UNINSURED_MOTORIST_LIMIT     decimal(18),          
 @UNDERINSURED_MOTORIST_LIMIT     decimal(18),          
 @OTHER_LIMIT     decimal(18),          
 @OTHER_DESCRIPTION     nvarchar(130),          
 @CREATED_BY int,
 @TERRITORY int      ,
 @CLIENT_UPDATE_DATE datetime =null     
          
)          
          
As          
          
--IF NOT EXISTS          
--(          
--SELECT POLICY_ID FROM POL_UMBRELLA_LIMITS          
-- WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
--  POLICY_ID = @POLICY_ID AND          
--  POLICY_VERSION_ID = @POLICY_VERSION_ID          
--)          
BEGIN          
 INSERT INTO POL_UMBRELLA_LIMITS          
 (          
  CUSTOMER_ID,          
  POLICY_ID,          
  POLICY_VERSION_ID,          
  POLICY_LIMITS,          
  RETENTION_LIMITS,          
  UNINSURED_MOTORIST_LIMIT,          
  UNDERINSURED_MOTORIST_LIMIT,          
  OTHER_LIMIT,          
  OTHER_DESCRIPTION,          
  CREATED_BY,          
  CREATED_DATETIME,  
  IS_ACTIVE,
  TERRITORY  ,
  CLIENT_UPDATE_DATE
 )          
 VALUES          
 (          
  @CUSTOMER_ID,          
  @POLICY_ID,          
  @POLICY_VERSION_ID,          
  @POLICY_LIMITS,          
  @RETENTION_LIMITS,          
  @UNINSURED_MOTORIST_LIMIT,          
  @UNDERINSURED_MOTORIST_LIMIT,          
  @OTHER_LIMIT,          
  @OTHER_DESCRIPTION,          
  @CREATED_BY,          
  GetDate()    ,  
  'Y',
  @TERRITORY  ,
  @CLIENT_UPDATE_DATE        
          
 )          
           
RETURN 1          
END          
        
        
        
        
          
          
          
          
          
          
          
          
        
      
    
  







GO

