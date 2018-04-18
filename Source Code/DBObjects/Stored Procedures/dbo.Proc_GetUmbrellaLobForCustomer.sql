IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUmbrellaLobForCustomer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUmbrellaLobForCustomer]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*-------------------------------------------------------------
Proc Name       : dbo.Proc_GetUmbrellaLobForCustomer
Created by      : Ravindra      
Date            : 03-23-2005     
Purpose         :     
Revison History :      
Used In         : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/   
  
--drop proc Proc_GetUmbrellaLobForCustomer    
CREATE PROCEDURE dbo.Proc_GetUmbrellaLobForCustomer    
(        
 @CUSTOMER_ID int  ,    
 @APP_ID   int    ,
 @APP_VERSION_ID int    
     
)            
AS                 
    
BEGIN  

select A.LOB AS LOB_ID,L.LOB_DESC AS LOB_DESC
  from APP_PKG_LOB_DETAILS A
 inner join MNT_LOB_MASTER L on A.LOB = L.LOB_ID
	
	where CUSTOMER_ID=@CUSTOMER_ID
	AND APP_ID=@APP_ID 
	AND APP_VERSION_ID=@APP_VERSION_ID

END



GO

