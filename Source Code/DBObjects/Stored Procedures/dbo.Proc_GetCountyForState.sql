IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCountyForState]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCountyForState]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------  
Proc Name          : dbo.Proc_GetCountyForState  
Created by         : Swastika Gaur
Date               : 13th Jun'06  
Purpose            : To get County Information  from MNT_TERRITORY_CODES table  
Revison History    :  
Used In            :   Wolverine  
  
------------------------------------------------------------  
  
Date     Review By          Comments  
------   ------------       -------------------------*/  
-- drop proc dbo.Proc_GetCountyForState  
Create PROC dbo.Proc_GetCountyForState  
(  
 @STATE  int  
)  
AS  
BEGIN  
  
	SELECT DISTINCT COUNTY FROM MNT_TERRITORY_CODES (NOLOCK) WHERE STATE = @STATE
	  
END  
  
  





GO

