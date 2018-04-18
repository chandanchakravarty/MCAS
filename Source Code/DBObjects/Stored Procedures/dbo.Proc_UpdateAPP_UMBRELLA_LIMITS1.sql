IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAPP_UMBRELLA_LIMITS1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAPP_UMBRELLA_LIMITS1]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*      
----------------------------------------------------------          
Proc Name       : dbo.Proc_UpdateAPP_UMBRELA_LIMITS1      
Created by      : Pradeep        
Date            : 26 May,2005          
Purpose         : Updates a record into UMBRELLA_LIITS          
Revison History :          
Used In         : Wolverine   
  
Modified By     : Mohit Gupta  
Modified On     : 20/10/2005  
Purpose         : removing precision from the size of decimal type parameter.         
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------         
Modified by	:Pravesh K Chandel
Date		:13 june 2007
purpopse	: Add new parameter @CLIENT_UPDATE_DATE
*/      
-- DROP PROCEDURE dbo.Proc_UpdateAPP_UMBRELLA_LIMITS1      
CREATE      PROCEDURE dbo.Proc_UpdateAPP_UMBRELLA_LIMITS1      
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
 @MODIFIED_BY int,
 @TERRITORY int            ,
 @CLIENT_UPDATE_DATE datetime =null         
)      
      
As      
      
IF EXISTS      
(      
 SELECT * FROM APP_UMBRELLA_LIMITS      
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
  APP_ID = @APP_ID AND      
  APP_VERSION_ID = @APP_VERSION_ID      
)      
BEGIN      
 UPDATE APP_UMBRELLA_LIMITS      
 SET      
 POLICY_LIMITS = @POLICY_LIMITS,      
 RETENTION_LIMITS = @RETENTION_LIMITS,      
 UNINSURED_MOTORIST_LIMIT = @UNINSURED_MOTORIST_LIMIT,      
 UNDERINSURED_MOTORIST_LIMIT = @UNDERINSURED_MOTORIST_LIMIT,      
 OTHER_LIMIT = @OTHER_LIMIT,      
 OTHER_DESCRIPTION = @OTHER_DESCRIPTION,      
 MODIFIED_BY = @MODIFIED_BY,  
 TERRITORY =@TERRITORY ,          
 LAST_UPDATED_DATETIME = GetDate()    ,
 CLIENT_UPDATE_DATE =  @CLIENT_UPDATE_DATE 
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
  APP_ID = @APP_ID AND      
  APP_VERSION_ID = @APP_VERSION_ID      
       
 RETURN 1      
END      
      
      
      
      
      
      
      
    
  





GO

