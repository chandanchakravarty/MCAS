IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_GetACT_APP_EFT_CUST_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_GetACT_APP_EFT_CUST_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : dbo.proc_GetACT_APP_EFT_CUST_INFO          
Created by      : Praveen kasana   
Date            : 17-jan-2006  
Purpose         : Get Info from ACT_APP_EFT_CUST_INFO 
Revison History :          
Used In         :      

-----------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          

-- drop proc dbo.proc_GetACT_APP_EFT_CUST_INFO  1151,115,1         
CREATE PROCEDURE dbo.proc_GetACT_APP_EFT_CUST_INFO          
(          
 @CUSTOMER_ID int,        
 @APP_ID int,        
 @APP_VERSION_ID int        
)          
AS          
BEGIN          
   
DECLARE @FEDERAL_ID INT
DECLARE @DFI_ACC_NO NVARCHAR(20)
DECLARE @TRANSIT_ROUTING_NO INT 
DECLARE @ACCOUNT_TYPE varchar(4)


SELECT  
FEDERAL_ID,  DFI_ACC_NO,TRANSIT_ROUTING_NO,ACCOUNT_TYPE,EFT_TENTATIVE_DATE
FROM ACT_APP_EFT_CUST_INFO  
WHERE  CUSTOMER_ID = @CUSTOMER_ID AND        
  APP_ID = @APP_ID AND        
  APP_VERSION_ID = @APP_VERSION_ID         

        

  
END     


        
        
        
      
    
  
  
  
  
  
  
  
  
  
  






GO

