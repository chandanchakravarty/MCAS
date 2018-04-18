IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SetUserIDForUnderwriter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SetUserIDForUnderwriter]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          : Dbo.Proc_SetUserIDForUnderwriter  
Created by           : Sumit Chhabra
Date                    : 30/01/2006
Purpose               :   
Revison History :  
Used In                :   Wolverine    
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE   PROCEDURE Proc_SetUserIDForUnderwriter  
(  
 @USERID varchar(100),
 @AGENCY_ID int
)  
AS  
BEGIN  
UPDATE MNT_AGENCY_LIST SET 
	HOME_UNDERWRITER=@USERID,
	MOTOR_UNDERWRITER=@USERID,
	PRIVATE_UNDERWRITER=@USERID,
	UMBRELLA_UNDERWRITER=@USERID,
	GENERAL_UNDERWRITER=@USERID,
	WATER_UNDERWRITER=@USERID,
	RENTAL_UNDERWRITER=@USERID
WHERE
	AGENCY_ID=@AGENCY_ID

END  



GO

