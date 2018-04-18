IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetNextRecrCompany_ID_Number]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetNextRecrCompany_ID_Number]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name   : dbo.Proc_GetNextRecrCompany_ID_Number               
Created by  :Pradeep                
Date        :17 May,2005              
Purpose     : Returns the next COMPANY_ID_NUMBER for the         
       CUSTOMERID, APPID and APPVersionID                 
Revison History  :   
Modified By : Sumit Chhabra  
Date : Nov, 07,2005  
Purpose: To put a limit on the new company id at 9999                     
 ------------------------------------------------------------                            
Date     Review By          Comments                          
                 
------   ------------       -------------------------*/          

CREATE     PROCEDURE dbo.Proc_GetNextRecrCompany_ID_Number        
(        
         
 @CUSTOMER_ID Int,        
 @APP_ID Int,        
 @APP_VERSION_ID SmallInt        
)        
        
As        
BEGIN         
 DECLARE @MAX int
 DECLARE @APP_EFFECTIVE_DATE VARCHAR(12)

 SELECT @APP_EFFECTIVE_DATE = convert(char,APP_EFFECTIVE_DATE,101)  FROM APP_LIST 
	 WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
		   APP_ID = @APP_ID AND         
		   APP_VERSION_ID = @APP_VERSION_ID         

 SELECT @MAX = ISNULL(MAX(COMPANY_ID_NUMBER),0)        
 FROM APP_HOME_OWNER_RECREATIONAL_VEHICLES        
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
  APP_ID = @APP_ID AND         
  APP_VERSION_ID = @APP_VERSION_ID         
     
 IF @MAX = 9999    
 BEGIN        
  SET @MAX = -2        
 END        
    
 set @MAX=@MAX+1    
 select @MAX as NEXT_COMPANY_ID, @APP_EFFECTIVE_DATE as APP_EFFECTIVE_DATE

END



GO

