IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUmbrellaFarmInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUmbrellaFarmInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE dbo.Proc_GetUmbrellaFarmInfo
(    
 @CUSTOMER_ID int  ,
 @APP_ID  int,
 @APP_VERSION_ID int,
 @FARM_ID int
 
)        
AS             

BEGIN                  

SELECT * FROM APP_UMBRELLA_FARM_INFO
WHERE  CUSTOMER_ID=@CUSTOMER_ID
  AND APP_ID=@APP_ID 
  AND APP_VERSION_ID=@APP_VERSION_ID
  AND FARM_ID=@FARM_ID


End  
  



GO

