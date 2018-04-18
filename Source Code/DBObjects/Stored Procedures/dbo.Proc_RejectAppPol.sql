IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_RejectAppPol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_RejectAppPol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------        
Proc Name          : Dbo.Proc_RejectAppPol        
Created by         : Pradeep Kushwaha
Date               : 16/June/2010        
Purpose            : To set the Policy and App status as a REJECT           

Revison History	   :              
Used In			   : Ebix Advantage web
------------------------------------------------------------              
Date     Review By          Comments          
drop proc Proc_RejectAppPol  
------   ------------       -------------------------*/        
CREATE PROC [dbo].[Proc_RejectAppPol]
(        
 @CUSTOMER_ID INT,                                                                 
 @POLICY_ID INT,                                             
 @POLICY_VERSION_ID SMALLINT, 
 @CALLEDFROM NVARCHAR(20)         
)     
AS        
BEGIN        
	IF(@CALLEDFROM='APP')
		BEGIN
			IF EXISTS (SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID)   
				BEGIN
					UPDATE POL_CUSTOMER_POLICY_LIST SET APP_STATUS = 'REJECT' WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID
				END
			 
		END
	ELSE IF (@CALLEDFROM='POL')
		BEGIN
			IF EXISTS (SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID)   
				BEGIN
					UPDATE POL_CUSTOMER_POLICY_LIST SET APP_STATUS = 'REJECT',POLICY_STATUS =null WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID
				END
		 
		END
 		
END 
 

 --select * from POL_CUSTOMER_POLICY_LIST where CUSTOMER_ID =2126  AND POLICY_ID =47 AND POLICY_VERSION_ID =1 
GO

