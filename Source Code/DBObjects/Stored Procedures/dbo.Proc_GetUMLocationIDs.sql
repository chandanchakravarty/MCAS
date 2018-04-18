IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUMLocationIDs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUMLocationIDs]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*=======================================================================
Proc Name   	 : dbo.Proc_GetUMLocationIDs    
Created by 	 : Ashwani    
Date       	 : 16 Oct. 2006
Purpose    	 : Get the location IDs  for UM rules
Revison History  :                    
=======================================================================
Date     Review By          Comments                        
               
=====  ============= ===============================================*/
create procedure dbo.Proc_GetUMLocationIDs    
(    
	@CUSTOMER_ID int,    
	@APP_ID int,    
	@APP_VERSION_ID int    
)        
as             
begin              
    
select   LOCATION_ID    
	from APP_UMBRELLA_REAL_ESTATE_LOCATION     
	where CUSTOMER_ID = @CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID   
 		and IS_ACTIVE='Y'     
	order by   LOCATION_ID                
end  
  





GO

