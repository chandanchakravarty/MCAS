IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateRemarks]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateRemarks]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_UpdateRemarks  
Created by      :Priya  
Date            : 5/27/2005  
Purpose         : To update the record in real estate table  
Revison History :  
Used In         :   wolverine  
  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE  PROC Dbo.Proc_UpdateRemarks    
(  
@CUSTOMER_ID  int,  
@APP_ID        int,  
@APP_VERSION_ID   smallint,  
@LOCATION_ID      smallint,  
@REMARKS  nvarchar(100)
)  
AS  
BEGIN  
  
 DECLARE @REMARKS_VALUE NVARCHAR(100)
 DECLARE @RETURNVALUE INT
 
 SELECT @REMARKS_VALUE=REMARKS FROM APP_UMBRELLA_REAL_ESTATE_LOCATION
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND    APP_ID     =@APP_ID  AND    APP_VERSION_ID=@APP_VERSION_ID AND  
 LOCATION_ID    =@LOCATION_ID  
 IF(@REMARKS_VALUE IS NOT NULL)
 BEGIN 
	SET @RETURNVALUE=-5
 END
 ELSE 
 BEGIN 
 	SET @RETURNVALUE=1
 END
 ---
 UPDATE APP_UMBRELLA_REAL_ESTATE_LOCATION  
 SET REMARKS =@REMARKS  
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND    APP_ID     =@APP_ID  AND    APP_VERSION_ID=@APP_VERSION_ID AND  
  LOCATION_ID    =@LOCATION_ID  

 RETURN @RETURNVALUE
    
END  
  



GO

