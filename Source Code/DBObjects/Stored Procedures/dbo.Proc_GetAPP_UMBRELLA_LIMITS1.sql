IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAPP_UMBRELLA_LIMITS1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAPP_UMBRELLA_LIMITS1]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*  
----------------------------------------------------------      
Proc Name       : dbo.Proc_GetAPP_UMBRELA_LIMITS1  
Created by      : Pradeep    
Date            : 26 May,2005      
Purpose         : Selects a single record from UMBRELLA_LIITS      
Revison History :      
Used In         : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       ------------------------- 
Modified by	:Pravesh K Chandel
Date		:13 june 2007
purpopse	: Add new parameter @CLIENT_UPDATE_DATE    
*/  
--DROP  PROCEDURE dbo.Proc_GetAPP_UMBRELLA_LIMITS1   
CREATE      PROCEDURE dbo.Proc_GetAPP_UMBRELLA_LIMITS1  
(  
 @CUSTOMER_ID int,  
 @APP_ID int,  
 @APP_VERSION_ID smallint  
)  
  
As  
  
SELECT CUSTOMER_ID,  
 APP_ID,  
 APP_VERSION_ID,  
 POLICY_LIMITS,  
 RETENTION_LIMITS,  
 UNINSURED_MOTORIST_LIMIT,  
 UNDERINSURED_MOTORIST_LIMIT,  
 OTHER_LIMIT,  
 OTHER_DESCRIPTION,  
 TERRITORY,
 CREATED_DATETIME  ,
convert(varchar,CLIENT_UPDATE_DATE,101) as CLIENT_UPDATE_DATE
FROM APP_UMBRELLA_LIMITS  
WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
 APP_ID = @APP_ID AND  
 APP_VERSION_ID = @APP_VERSION_ID  
  
  
  
  
  
  
  
  





GO

