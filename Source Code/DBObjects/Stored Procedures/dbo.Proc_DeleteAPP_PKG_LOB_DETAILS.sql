IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteAPP_PKG_LOB_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteAPP_PKG_LOB_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          : Dbo.Proc_DeleteAPP_PKG_LOB_DETAILS  
Created by           : Mohit Gupta  
Date                    : 30/06/2005  
Purpose               :   
Revison History :  
Used In                :   Wolverine    
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--drop proc Proc_DeleteAPP_PKG_LOB_DETAILS
CREATE   PROCEDURE Proc_DeleteAPP_PKG_LOB_DETAILS  
(  
 @REC_ID int  ,
 @CUSTOMER_ID     int,  
 @APP_ID     int,  
 @APP_VERSION_ID     smallint
)  
AS  
BEGIN  
DELETE FROM APP_PKG_LOB_DETAILS  
  where REC_ID = @REC_ID  AND
	CUSTOMER_ID=@CUSTOMER_ID   AND
	  APP_ID=@APP_ID 	AND  
	  APP_VERSION_ID=@APP_VERSION_ID 
END  



GO

