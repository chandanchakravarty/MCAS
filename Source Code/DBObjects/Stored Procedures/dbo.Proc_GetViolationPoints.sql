IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetViolationPoints]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetViolationPoints]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                    
Proc Name        : dbo.Proc_GetViolationPoints
Created by       : Sumit Chhabra
Date             : 16/12/2005                    
Purpose     		:	 Retrieve the sum of MVR points for a driver
Revison History :                    
Used In  : Wolverine                     
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/                     
CREATE PROCEDURE Proc_GetViolationPoints                    
(                    
                     
 @CUSTOMER_ID int,                    
 @APP_ID  int,                    
 @APP_VERSION_ID smallint,                    
 @DRIVER_ID INT
)                    
AS                    
BEGIN     
SELECT SUM(MVR_POINTS) AS MVR_POINTS FROM APP_MVR_INFORMATION A JOIN MNT_VIOLATIONS M
ON A.VIOLATION_ID=M.VIOLATION_ID
WHERE A.CUSTOMER_ID=@CUSTOMER_ID AND A.APP_ID=@APP_ID AND A.APP_VERSION_ID=@APP_VERSION_ID AND A.DRIVER_ID=@DRIVER_ID

      
END      
  



GO

