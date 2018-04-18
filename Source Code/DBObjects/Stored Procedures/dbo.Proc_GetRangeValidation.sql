IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRangeValidation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRangeValidation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                  
Proc Name                : Dbo.Proc_GetRangeValidation                                  
Created by               : Ashwani                                                  
Date                     : 29 Nov.,2005                                                
Purpose                  : To get the date range validation to submit the application  
Revison History          :                                                  
Used In                  : Wolverine                                                  
------------------------------------------------------------                                                  
Date     Review By          Comments                                                  
------   ------------       -------------------------*/                                                  
CREATE  proc Dbo.Proc_GetRangeValidation                                                  
(                                                  
 @CUSTOMER_ID    int,                                                  
 @APP_ID    int,                                                  
 @APP_VERSION_ID   int ,   
 @ISVALID int output             
)                                                  
as                                   
begin   
 declare @DATEDIFFERECE int    
   
 if exists(select CUSTOMER_ID from APP_LIST where CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID )                                    
 begin                        
  select @DATEDIFFERECE=datediff(day, APP_EFFECTIVE_DATE, getdate())   
  from  APP_LIST                    
  where CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID                   
 end                              
 else                              
 begin                                   
  set @DATEDIFFERECE=0  
  set @ISVALID =-1  
 end    
   
   
 if(@DATEDIFFERECE>15)  
 begin   
  set @ISVALID=1  
 end  
 else    
 begin    
  set @ISVALID=0  
 end   
end                  
  



GO

