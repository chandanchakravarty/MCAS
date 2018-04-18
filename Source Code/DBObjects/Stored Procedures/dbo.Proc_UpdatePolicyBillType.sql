IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyBillType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyBillType]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--begin tran
--drop proc Proc_UpdatePolicyBillType
--go 
--Text
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

/*----------------------------------------------------------                  
Proc Name       : dbo.Proc_UpdatePolicyBillType            
Created by      : Pravesh k Chandel         
Date            : 6-Oct-2008          
Purpose       : update Policy bill type (mortagagee to Insured and vice versa)
Revison History :                  
Used In    : Wolverine                  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/             
--drop proc dbo.Proc_UpdatePolicyBillType              
CREATE PROC dbo.Proc_UpdatePolicyBillType
(                  
	  @CUSTOMER_ID  INT,              
	  @POLICY_ID  INT,              
	  @POLICY_VERSION_ID INT,     
	  @BILL_TYPE_ID	INT 
 )
AS
begin                  
IF(@BILL_TYPE_ID = 11276)

BEGIN           
UPDATE POL_CUSTOMER_POLICY_LIST SET BILL_TYPE_ID=@BILL_TYPE_ID
	WHERE	CUSTOMER_ID			= @CUSTOMER_ID
		AND POLICY_ID			= @POLICY_ID
		AND CURRENT_TERM=
			(SELECT CURRENT_TERM FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)
				WHERE	CUSTOMER_ID			= @CUSTOMER_ID
					AND POLICY_ID			= @POLICY_ID
					AND POLICY_VERSION_ID	=@POLICY_VERSION_ID
			)
 
END              

ELSE 
BEGIN  
     
		DECLARE @CURRENT_TERM INT  , @ADD_INT Int , @DWELLING_ID Int

		SELECT  @CURRENT_TERM = CURRENT_TERM , @ADD_INT = ADD_INT_ID , @DWELLING_ID = DWELLING_ID
		FROM POL_CUSTOMER_POLICY_LIST
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND
		POLICY_ID =  @POLICY_ID AND
		POLICY_VERSION_ID = @POLICY_VERSION_ID 

    	 UPDATE POL_HOME_OWNER_ADD_INT SET BILL_MORTAGAGEE = NULL         
		 FROM POL_HOME_OWNER_ADD_INT 
         WHERE CUSTOMER_ID = @CUSTOMER_ID 
		 AND POLICY_ID = @POLICY_ID         
		 AND POLICY_VERSION_ID IN ( SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST 
				WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND CURRENT_TERM = @CURRENT_TERM)
			       
         
	UPDATE POL_CUSTOMER_POLICY_LIST SET BILL_TYPE_ID=@BILL_TYPE_ID , ADD_INT_ID = NULL , DWELLING_ID = NULL 
	WHERE	CUSTOMER_ID			= @CUSTOMER_ID
		AND POLICY_ID			= @POLICY_ID
		AND CURRENT_TERM=
			(SELECT CURRENT_TERM FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)
				WHERE	CUSTOMER_ID			= @CUSTOMER_ID
					AND POLICY_ID			= @POLICY_ID
					AND POLICY_VERSION_ID	=@POLICY_VERSION_ID
			)

        
 
END              
END             

--go
--exec  Proc_UpdatePolicyBillType 1275,130,4,11150,1,1
--rollback tran  


--select * from POL_HOME_OWNER_ADD_INT where customer_id = 1274  and policy_id = 4  and policy_version_id
--
--select add_int_id ,DWELLING_ID ,   * 
--from POL_HOME_OWNER_ADD_INT where customer_id = 1275  and policy_id = 130 and policy_version_id = 4






GO

