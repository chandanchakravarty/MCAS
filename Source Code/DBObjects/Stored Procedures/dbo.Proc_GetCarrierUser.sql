IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCarrierUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCarrierUser]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : Proc_GetCarrierUser        
Created by      : Swarup          
Date            : 20-Aug-2007         
Purpose     : Get values from  MNT_USER_LIST table          
Revison History :          
Used In  : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
      
-- drop proc dbo.Proc_GetCarrierUser         
CREATE PROC dbo.Proc_GetCarrierUser
AS
BEGIN
SELECT USER_ID,(USER_FNAME + ' - ' + USER_LNAME) AS USER_NAME   
FROM MNT_USER_LIST WHERE  USER_SYSTEM_ID = 'W001' ORDER BY USER_FNAME
END   


GO

