IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetNewBoatNo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetNewBoatNo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name          : Dbo.Proc_GetNewBoatNo      
Created by           : Nidhi      
Date                    : 28/04/2005      
Purpose               : To get the new insured vehicle number      
Revison History :      
Used In                :   Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
--DROP PROC Dbo.Proc_GetNewBoatNo 
CREATE PROC Dbo.Proc_GetNewBoatNo      
@CUSTOMER_ID INT,      
@APP_ID INT,      
@APP_VERSION_ID INT,      
@CALLEDFROM varchar(4)       
as      
BEGIN      
      
 --if (@CALLEDFROM ='WAT' or @CALLEDFROM='RENT' or @CALLEDFROM='HOME')      
 if (@CALLEDFROM ='WAT' or @CALLEDFROM='HOME')      
 begin      
  SELECT    (isnull(MAX(BOAT_NO),0)) +1 as BOAT_NO      
  FROM         APP_WATERCRAFT_INFO WITH(NOLOCK)
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID      
 end      
 else if (@CALLEDFROM ='UMB')      
 begin      
  SELECT    (isnull(MAX(BOAT_NO),0)) +1 as BOAT_NO      
  FROM         APP_UMBRELLA_WATERCRAFT_INFO WITH(NOLOCK)
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID      
 end      
END      
    
  






GO

