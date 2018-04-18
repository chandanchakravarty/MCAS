IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckApp_Rule_Verification_Status]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckApp_Rule_Verification_Status]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------    
Proc Name       : dbo.Proc_CheckApp_Rule_Verification_Status    
Created by      : Manoj Rathore    
Date            : 06-10-2005    
Purpose         : To showing the images on the Client Top .    
Revison History :    
Used In         :   wolvorine    
    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
--drop PROC dbo.Proc_CheckApp_Rule_Verification_Status  
CREATE PROC [dbo].[Proc_CheckApp_Rule_Verification_Status]  
(  
@CUSTOMER_ID INT,                                                
@APP_ID INT,       
@APP_VERSION_ID SMALLINT  ,
@CALLED_FROM varchar(10) =''
)  
AS  
BEGIN  
 DECLARE @RETURN_VALUE INT   
  if( @CALLED_FROM='POL')
  begin
   IF EXISTS (SELECT CUSTOMER_ID FROM 
			POL_CUSTOMER_POLICY_LIST  WITH(NOLOCK)      
			WHERE CUSTOMER_ID=@CUSTOMER_ID 
			AND POLICY_ID=@APP_ID 
			AND POLICY_VERSION_ID = @APP_VERSION_ID 
			AND POLICY_STATUS='APPLICATION'
			AND IS_ACTIVE = 'Y' 
			)      
		SET @RETURN_VALUE= 1  
	else	
	SET @RETURN_VALUE= -1  
  end
  else
  begin
   IF EXISTS (SELECT CUSTOMER_ID FROM 
			POL_CUSTOMER_POLICY_LIST  WITH(NOLOCK)      
			WHERE CUSTOMER_ID=@CUSTOMER_ID 
			AND APP_ID=@APP_ID 
			AND APP_VERSION_ID = @APP_VERSION_ID 
			AND POLICY_STATUS<>'APPLICATION'
			AND IS_ACTIVE = 'Y' 
			)      
		SET @RETURN_VALUE= -1  
   ELSE   
   
   SELECT @RETURN_VALUE= RULE_VERIFICATION FROM APP_LIST                                                      
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND IS_ACTIVE = 'Y'   
   end   
  
  RETURN  @RETURN_VALUE  
     
    
   
END  




GO

