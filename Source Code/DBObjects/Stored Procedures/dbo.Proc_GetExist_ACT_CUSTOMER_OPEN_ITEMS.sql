IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetExist_ACT_CUSTOMER_OPEN_ITEMS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetExist_ACT_CUSTOMER_OPEN_ITEMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*--------------------------------------------------------------------        
CREATED BY   : Vijay Arora        
CREATED DATE TIME : 17-01-2006        
PURPOSE    :  Get that Item is exists in Table Named ACT_CUSTOMER_OPEN_ITEMS or not.    
REVIEW HISTORY        
REVIEW BY  DATE  PURPOSE        
---------------------------------------------------------------------*/        
CREATE PROCEDURE dbo.Proc_GetExist_ACT_CUSTOMER_OPEN_ITEMS    
(        
 @CUSTOMER_ID  INT,          
 @POLICY_ID    INT,           
 @POLICY_VERSION_ID  INT,           
 @ITEM_STATUS VARCHAR(3),
 @RESULT INT OUTPUT      
)        
AS        
BEGIN        
        
IF EXISTS (SELECT ITEM_STATUS FROM ACT_CUSTOMER_OPEN_ITEMS WHERE CUSTOMER_ID = @CUSTOMER_ID AND         
         POLICY_ID = @POLICY_ID AND    
               POLICY_VERSION_ID = @POLICY_VERSION_ID AND ITEM_STATUS = @ITEM_STATUS )    
 BEGIN    
  	SET @RESULT = 1
	RETURN @RESULT
 END
ELSE
 BEGIN
  	SET @RESULT = 0
	RETURN @RESULT

 END    
   
END        
    
  





GO

