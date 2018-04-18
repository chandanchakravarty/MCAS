IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckExistPolicyAndSecondaryHeat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckExistPolicyAndSecondaryHeat]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name               : Dbo.Proc_CheckExistPolicy        
Created by              : Shafi      
Date                    : 16/03/2006      
Purpose                 :         
Revison History :        
Used In                :   Wolverine          
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
--exec Proc_CheckExistPolicyAndSecondaryHeat  837 ,3 ,6,7    
--DROP PROC Proc_CheckExistPolicy      
CREATE PROCEDURE Proc_CheckExistPolicyAndSecondaryHeat      
(        
 @CUSTOMERID INT ,  
 @APPID   INT,     
 @APPVERSIONID INT,  
 @LOBID   VARCHAR(20)  
)        
AS        
BEGIN  
 DECLARE @MULTIPOLICY INT      
 DECLARE @SECONDARY_HEAT_TYPE    INT   
 SET @MULTIPOLICY=0  
 SET @SECONDARY_HEAT_TYPE=0  
--FOR HOMEOWNER IF THER IS ANY EXISTING POLICY OF AUTO  


       SELECT @MULTIPOLICY = COUNT(CUSTOMER_ID) FROM POL_CUSTOMER_POLICY_LIST WHERE  CUSTOMER_ID = @CUSTOMERID  
       SELECT @SECONDARY_HEAT_TYPE=COUNT(SECONDARY_HEAT_TYPE) FROM APP_HOME_RATING_INFO WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND SECONDARY_HEAT_TYPE <>'6211'        
       SELECT @MULTIPOLICY AS MULTIPOLICY,  
              @SECONDARY_HEAT_TYPE AS SECONDARY_HEAT_TYPE  
END        
        
      
    
  



GO

