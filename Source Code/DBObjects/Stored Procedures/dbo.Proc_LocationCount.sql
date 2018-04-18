IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_LocationCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_LocationCount]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------      
Proc Name       :  dbo.Proc_GeneralLocations  
Created by      :  Raghav Gupta    
Date            :  07/16/2008      
Purpose         :  Count the number of Active Location
Revison History :      
Used In         :   Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/  
--DROP  PROC dbo.Proc_LocationCount 
CREATE PROC [dbo].[Proc_LocationCount]
  
(  
@CUSTOMER_ID int,  
@APP_ID int,  
@APP_VERSION_ID int  ,
@CALLED_FROM varchar(10)
)	
AS  
BEGIN  
DECLARE @RET_VAL INT 
IF (@CALLED_FROM = 'APP')
BEGIN
	SELECT @ret_val = count(*) FROM APP_LOCATIONS AL WHERE   
	CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND IS_ACTIVE = 'Y'  
END
ELSE 
BEGIN
	SELECT @RET_VAL = COUNT(*) FROM POL_LOCATIONS PL WHERE   
	CUSTOMER_ID = @CUSTOMER_ID AND  POLICY_ID = @APP_ID AND POLICY_VERSION_ID = @APP_VERSION_ID AND IS_ACTIVE = 'Y'  
END

	RETURN @RET_VAL  

END 



GO

