IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertACT_POL_CREDIT_CARD_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertACT_POL_CREDIT_CARD_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------      
Proc Name       : dbo.Proc_InsertACT_POL_CREDIT_CARD_DETAILS  
Created by      : Swastika Gaur  
Date            : 28th May 2007  
Purpose      	: Evaluation      
Revison History :      
Used In  : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------    */
-- drop proc dbo.Proc_InsertACT_POL_CREDIT_CARD_DETAILS   
CREATE PROC [dbo].[Proc_InsertACT_POL_CREDIT_CARD_DETAILS]      
(      
	@CUSTOMER_ID 		INT,      
	@POLICY_ID 		INT,      
	@POLICY_VERSION_ID		 SMALLINT, 
	@PAY_PAL_REF_ID		Varchar(200) 
	 
)      
AS      
BEGIN   

 /*ITRACK 4636*/
  DECLARE @CURRENT_TERM INT
  SELECT @CURRENT_TERM = CURRENT_TERM FROM POL_CUSTOMER_POLICY_LIST 
	WHERE CUSTOMER_ID = @CUSTOMER_ID 
	AND   POLICY_ID   = @POLICY_ID 
	AND   POLICY_VERSION_ID = @POLICY_VERSION_ID  
   
      
IF NOT EXISTS(SELECT CUSTOMER_ID FROM ACT_POL_CREDIT_CARD_DETAILS (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID  
AND POLICY_VERSION_ID = @POLICY_VERSION_ID)        
 BEGIN  -- INSERT    
 	 INSERT INTO ACT_POL_CREDIT_CARD_DETAILS      
	 (      
	  CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, PAY_PAL_REF_ID,CREATED_DATETIME
	 )      
	 /*VALUES      
	 (      
	  @CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID,@PAY_PAL_REF_ID,GETDATE()
	 ) */

	SELECT  CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID,@PAY_PAL_REF_ID,GETDATE()
	FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)
	WHERE CUSTOMER_ID 	= @CUSTOMER_ID 
	AND   POLICY_ID   	= @POLICY_ID 
	AND CURRENT_TERM 	= @CURRENT_TERM	     

RETURN 1       
 END      
ELSE  
 BEGIN  
/* UPDATE ACT_POL_CREDIT_CARD_DETAILS
 	SET   PAY_PAL_REF_ID		= @PAY_PAL_REF_ID,
	      LAST_UPDATED_DATETIME	= GETDATE()		
		
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID  */

UPDATE CC_POL
SET   CC_POL.PAY_PAL_REF_ID		= @PAY_PAL_REF_ID,
      CC_POL.LAST_UPDATED_DATETIME	= GETDATE()		
FROM ACT_POL_CREDIT_CARD_DETAILS AS CC_POL WITH(NOLOCK)
INNER JOIN POL_CUSTOMER_POLICY_LIST POL WITH(NOLOCK)
ON CC_POL.CUSTOMER_ID 	= POL.CUSTOMER_ID
AND CC_POL.POLICY_ID 	= POL.POLICY_ID
	WHERE CC_POL.CUSTOMER_ID 	= @CUSTOMER_ID 
	AND   CC_POL.POLICY_ID   	= @POLICY_ID 
	AND   POL.CURRENT_TERM 		= @CURRENT_TERM	     


 RETURN 2     --UDPATED  
 END  
END    

























GO

