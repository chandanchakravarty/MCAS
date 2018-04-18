IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetReserveCodes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetReserveCodes]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : dbo.Proc_GetReserveCodes          
Created by      : Sumit Chhabra
Date            : 06/20/2006          
Purpose   		  : To retrieve Resreve Transaction Codes
Revison History :          
Used In  : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
CREATE PROC dbo.Proc_GetReserveCodes          
(          
 @CLAIM_ID     int          
)          
AS          
BEGIN          

declare @RESERVE_AMOUNT int
declare @RE_OPEN_RESERVE int
set  @RE_OPEN_RESERVE = 11839

SELECT TOP 1 @RESERVE_AMOUNT = ISNULL(RESERVE_AMOUNT,0) FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID ORDER BY ACTIVITY_ID DESC

if @RESERVE_AMOUNT is null or @RESERVE_AMOUNT = 0 
SELECT V.LOOKUP_UNIQUE_ID,V.LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES V JOIN MNT_LOOKUP_TABLES T 
	ON V.LOOKUP_ID=T.LOOKUP_ID WHERE T.LOOKUP_NAME = 'CRTC'
else
	SELECT V.LOOKUP_UNIQUE_ID,V.LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES V JOIN MNT_LOOKUP_TABLES T 
	ON V.LOOKUP_ID=T.LOOKUP_ID WHERE T.LOOKUP_NAME = 'CRTC' and lookup_unique_id<>@RE_OPEN_RESERVE

END          
        
      
    
  



GO

