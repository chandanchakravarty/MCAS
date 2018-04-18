IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchZipState]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchZipState]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          : dbo.Proc_FetchZipState  
Created by         :   
Date               :   
Purpose            :   
Revison History    :  
Used In            :   Wolverine    
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/   
-- drop proc dbo.Proc_FetchZipState   
CREATE PROCEDURE dbo.Proc_FetchZipState   
(  
 @STATE_ID int,  
 @ZIP_ID VARCHAR(10)  
)  
AS  
BEGIN   
   SELECT DISTINCT ZIP from MNT_TERRITORY_CODES (NOLOCK)  
   WHERE  STATE = @STATE_ID and ZIP like (SUBSTRING(@ZIP_ID,1,5) + '%')  
END  
   






GO

