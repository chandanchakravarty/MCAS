IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRuleUMBoatIDs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRuleUMBoatIDs]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*=================================================================================================
Proc Name   	 : dbo.Proc_GetRuleUMBoatIDs      
Created by  	 : Ashwini      
Date       	 : 17 Oct. 2006
Purpose    	 : Get the  Boat IDs for UM
Revison History  :                      
====================================================================================================
Date     Review By          Comments                          
                 
====  =============      ===========================================================================*/
create procedure dbo.Proc_GetRuleUMBoatIDs      
(      
	 @CUSTOMER_ID int,      
	 @APP_ID int,      
	 @APP_VERSION_ID int      
)          
as               
begin                
      
 select   BOAT_ID      
	from       APP_UMBRELLA_WATERCRAFT_INFO       
		where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID and IS_ACTIVE='Y'       
			order by   BOAT_ID                  
end     
  



GO

