IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyOldVersion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyOldVersion]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                
Proc Name       : dbo.Proc_UpdatePolicyOldVersion                                                
Created by      : praveen kasana                            
Date            : 22/Aug.2007                         
Purpose         : Update old policy version ID during rollback in table ACT_CUSTOMER_BALANCE_INFORMATION                              
Revison History :                                                
Used In         : Wolverine                                                
        
---------------------------------------------------------- */                                      
                        
-- drop proc dbo.Proc_UpdatePolicyOldVersion   
                        
CREATE PROC [dbo].[Proc_UpdatePolicyOldVersion]                                                                 
(                                                            
	@CUSTOMER_ID int,                                                            
	@POLICY_ID  int,                                                            
	@POLICY_VERSION_ID smallint,                                                            
	@NEW_POLICY_VERSION_ID smallint                                                            
)                                                            
AS                                                            
BEGIN           

--Charles (18-Nov-09), Itrack 6648
UPDATE ACT_POLICY_INSTALL_PLAN_DATA SET POLICY_VERSION_ID =  @POLICY_VERSION_ID  
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POLICY_ID AND POLICY_VERSION_ID = @NEW_POLICY_VERSION_ID    
--End of addition.  
  
update ACT_CUSTOMER_OPEN_ITEMS  WITH (ROWLOCK)   
set POLICY_VERSION_ID = @POLICY_VERSION_ID        
where customer_id=@CUSTOMER_ID and policy_id =@POLICY_ID and policy_version_id=@NEW_POLICY_VERSION_ID         

update ACT_CUSTOMER_BALANCE_INFORMATION  WITH (ROWLOCK)   
set POLICY_VERSION_ID = @POLICY_VERSION_ID        
where customer_id=@CUSTOMER_ID and policy_id =@POLICY_ID and policy_version_id=@NEW_POLICY_VERSION_ID         
       
--Ravindra(09-23-2008)  
UPDATE ACT_ACCOUNTS_POSTING_DETAILS SET POLICY_VERSION_ID = @POLICY_VERSION_ID        
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POLICY_ID AND POLICY_VERSION_ID=@NEW_POLICY_VERSION_ID  
  
--Shikha(03-03-2009)  
UPDATE ACT_CHECK_INFORMATION SET POLICY_VER_TRACKING_ID =  @POLICY_VERSION_ID  
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POLICY_ID AND POLICY_VER_TRACKING_ID = @NEW_POLICY_VERSION_ID    
--End of addition.     
  
update MNT_TRANSACTION_LOG  WITH (ROWLOCK)       
set POLICY_VER_TRACKING_ID = @POLICY_VERSION_ID        
where client_id=@CUSTOMER_ID and policy_id =@POLICY_ID and POLICY_VER_TRACKING_ID=@NEW_POLICY_VERSION_ID          
      
END           
        
        
  
  


GO

