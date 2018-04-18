IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCOUNTY_FROM_ZIP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCOUNTY_FROM_ZIP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          : Dbo.Proc_GetCOUNTY_FROM_ZIP  
Created by           : Pradeep Iyer  
Date                    : Oct 7 , 2005  
Purpose               : Gets the County from the Zip code  
Revison History :  
Used In                :   Wolverine    
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE    PROCEDURE Proc_GetCOUNTY_FROM_ZIP  
(  
 @CUSTOMER_ID int,  
 @APP_ID int,  
 @APP_VERSION_ID int,  
 @ZIP NVarChar(10)   
   
)  
AS  
BEGIN  
  
DECLARE @STATE_ID Int  
DECLARE @LOB Smallint  
DECLARE @COUNTY NVarChar(75)  
  
SELECT @STATE_ID = A.STATE_ID,  
       @LOB = A.APP_LOB  
FROM APP_LIST A  
WHERE  A.CUSTOMER_ID=@CUSTOMER_ID    AND  
               A.APP_ID=@APP_ID     AND  
               A.APP_VERSION_ID=@APP_VERSION_ID  
  
SELECT @COUNTY = COUNTY   
FROM MNT_TERRITORY_CODES  
WHERE ZIP = @ZIP AND  
 STATE = @STATE_ID AND  
 LOBID = @LOB  
  
RETURN @COUNTY  
  
END  
  
  



GO

