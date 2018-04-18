IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckAPP_Status]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckAPP_Status]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 

/* =======================================================================                         
 Proc Name       : dbo.Proc_CheckAPP_Status                                      
 Created by      : Shafi                                      
 Date            : 02 Feb. 2006                                     
 Purpose         : Check for Converted Apploication Status  
  ========================================================================*/    
--drop PROC Proc_CheckAPP_Status    
CREATE PROC Proc_CheckAPP_Status    
(                                            
@CUSTOMER_ID INT,                                            
@APP_ID INT,   
@APP_VERSION_ID INT,   
@convertr  INT OUTPUT    
)    
AS    
BEGIN    
IF not EXISTS (SELECT * FROM APP_LIST                                                  
WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID  )  
begin 
print 'd'
	SET @convertr= 1    
end
ELSE    
       --Check For Existance Of Policy
        BEGIN
		IF not EXISTS (SELECT * FROM POL_CUSTOMER_POLICY_LIST    
		WHERE CUSTOMER_ID=@CUSTOMER_ID AND    
		APP_ID=@APP_ID AND APP_VERSION_ID = @APP_VERSION_ID 
		 )    
			SET @convertr=1
		ELSE
			SET @convertr=2  
	END

END


GO

