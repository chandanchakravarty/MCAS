IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_AppGetEffDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_AppGetEffDate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                            
Proc Name        : dbo.Proc_AppGetEffDate        
Created by       : MANOJ RATHORE        
Date             : 3rd jan 2008                          
Purpose         : Retrieve effective date  
Revison History :                            
Used In  : Wolverine                             
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/                             
--drop proc dbo.Proc_AppGetEffDate
CREATE PROCEDURE dbo.Proc_AppGetEffDate --1009,458,1  
(                            
 @CUSTOMER_ID int,                            
 @APP_ID  int,                            
 @APP_VERSION_ID smallint,
 @CALLED_FROM VARCHAR(5)                            
)                            
AS                           
    
BEGIN 
DECLARE @EFF_DATE VARCHAR(20)
DECLARE @Effective_Date VARCHAR(20)
SET     @Effective_Date='01/01/2008' 
--declare @policy_type_code int
	IF(@CALLED_FROM ='APP')
	BEGIN 
		SELECT @EFF_DATE = ISNULL(DATEDIFF(DAY,@Effective_Date,CONVERT(VARCHAR(20),APP_INCEPTION_DATE,101)),'') FROM APP_LIST    
		WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID 
		AND DATEDIFF(DAY,@Effective_Date,CONVERT(VARCHAR(20),APP_INCEPTION_DATE,101)) >= 0	
	END
	ELSE IF(@CALLED_FROM = 'POL')
		SELECT @EFF_DATE = ISNULL(DATEDIFF(DAY,@Effective_Date,CONVERT(VARCHAR(20),APP_INCEPTION_DATE,101)),'') FROM POL_CUSTOMER_POLICY_LIST    
		WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@APP_ID AND POLICY_VERSION_ID=@APP_VERSION_ID 
		AND DATEDIFF(DAY,@Effective_Date,CONVERT(VARCHAR(20),APP_INCEPTION_DATE,101)) >= 0	
	SELECT ISNULL(@EFF_DATE,'') AS EFF_DATE
  
END 









GO

