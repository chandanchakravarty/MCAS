IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertACT_APP_EFT_CUST_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertACT_APP_EFT_CUST_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------            
Proc Name       : dbo.Proc_InsertACT_APP_EFT_CUST_INFO        
Created by      : Praveen kasana          
Date            : 16 Jan 2006        
Purpose      : Evaluation            
Revison History :            
Used In  : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
-- drop proc dbo.Proc_InsertACT_APP_EFT_CUST_INFO         
CREATE PROC dbo.Proc_InsertACT_APP_EFT_CUST_INFO            
(            
 @CUSTOMER_ID        int,            
 @APP_ID          int,            
 @APP_VERSION_ID        smallint,            
 @FEDERAL_ID      varchar (100)  ,            
 @DFI_ACC_NO     NVARCHAR(100),           
 @TRANSIT_ROUTING_NO varchar(100),        
 @ACCOUNT_TYPE   varchar(4),        
 @CREATED_BY        int,            
 @CREATED_DATETIME   datetime,            
 @MODIFIED_BY  INT  = null,        
 @LAST_UPDATED_DATETIME datetime,        
 @EFT_TENTATIVE_DATE int        
        
)            
AS            
BEGIN            
            
 If NOT EXISTS(SELECT CUSTOMER_ID FROM ACT_APP_EFT_CUST_INFO WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID        
 AND APP_VERSION_ID = @APP_VERSION_ID)              
 BEGIN            
  /*insert*/           
        
 INSERT INTO ACT_APP_EFT_CUST_INFO            
 (            
  CUSTOMER_ID, APP_ID, APP_VERSION_ID, FEDERAL_ID,            
  DFI_ACC_NO, TRANSIT_ROUTING_NO, CREATED_BY, CREATED_DATETIME,ACCOUNT_TYPE,EFT_TENTATIVE_DATE        
 )            
 VALUES            
 (            
  @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID, @FEDERAL_ID,            
  @DFI_ACC_NO, @TRANSIT_ROUTING_NO, @CREATED_BY, @CREATED_DATETIME ,@ACCOUNT_TYPE,@EFT_TENTATIVE_DATE        
 )            
             
 return 1             
         
 END            
 ELSE        
 BEGIN        
 UPDATE ACT_APP_EFT_CUST_INFO SET         
 FEDERAL_ID = @FEDERAL_ID,        
 DFI_ACC_NO = @DFI_ACC_NO,        
 TRANSIT_ROUTING_NO = @TRANSIT_ROUTING_NO,        
 ACCOUNT_TYPE = @ACCOUNT_TYPE,        
 MODIFIED_BY = @MODIFIED_BY,        
 EFT_TENTATIVE_DATE = @EFT_TENTATIVE_DATE        
  --LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME        
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID        
 return 2     --Udpated        
        
         
 END        
            
         
END          
      
      
    
    
  






GO

