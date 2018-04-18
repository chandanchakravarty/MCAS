IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRuleUMSchedule_UnderLaying_Count]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRuleUMSchedule_UnderLaying_Count]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*=================================================================================================
Proc Name   	 : dbo.Proc_GetRuleUMSchedule_UnderLaying_Count      
Created by  	 : Ashwini      
Date       	 : 17 Oct. 2006
Purpose    	 : Count the schedule of underlaying records for UM  
Revison History  :                      
====================================================================================================
Date     Review By          Comments                          
                 
====  =============      ===========================================================================*/
create procedure dbo.Proc_GetRuleUMSchedule_UnderLaying_Count      
(      
	 @CUSTOMER_ID int,      
	 @APP_ID int,      
	 @APP_VERSION_ID int      
)          
as               
begin                
      
 select   POLICY_LOB
	from       APP_UMBRELLA_UNDERLYING_POLICIES       
		where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID        
			                 
end     
  




  









GO

