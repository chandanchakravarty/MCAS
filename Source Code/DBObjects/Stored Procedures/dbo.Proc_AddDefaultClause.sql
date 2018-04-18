IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_AddDefaultClause]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_AddDefaultClause]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                          
Proc Name        : dbo.Proc_InsertClauseOnInstallment      
Created by       : Lalit chauhan      
Date             : 22/10/2010                                        
Purpose          : insert clauses when installment genrates.    
Used In          : Ebix Advantage                  
reffence itrack     : 799
updated refference itrack : - 
Used in					: stored procedured(Proc_InsertPolicyPremiumItems)	
------------------------------------------------------------                                          
Date     Review By          Comments                                          
------   ------------       -------------------------    
drop proc Proc_AddDefaultClause 28059,16,1,398
sp_find p
*/
CREATE Proc [dbo].[Proc_AddDefaultClause]
(
@CUSTOMER_ID INT,
@POLICY_ID INT ,
@POLICY_VERSION_ID INT,
@USER_ID INT,
@RETVAL INT = NULL OUT 
)
AS
BEGIN
DECLARE @CLAUSE_CODE NVARCHAR(500) = '7008',--'9999_0_7008_1_05082010',
@MAX_POL_CLAUSE_ID INT,@POLICY_LOB INT

	SELECT @POLICY_LOB =  POLICY_LOB FROM  POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE 
	CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID
	

	
	SET @RETVAL = 0
	
	IF EXISTS(SELECT TOTAL_TRAN_PREMIUM FROM ACT_POLICY_INSTALL_PLAN_DATA  WITH(NOLOCK) WHERE 
		 CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID 
		 AND TOTAL_TRAN_PREMIUM > 0  )
		 BEGIN	
	
			IF NOT EXISTS(SELECT * FROM POL_CLAUSES WITH(NOLOCK) WHERE 
			CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND 
			POLICY_VERSION_ID =@POLICY_VERSION_ID AND CLAUSE_CODE = @CLAUSE_CODE)
			BEGIN
		
				SELECT @MAX_POL_CLAUSE_ID = ISNULL(MAX(POL_CLAUSE_ID),0)+1 frOM POL_CLAUSES WITH(NOLOCK)  WHERE CUSTOMER_ID = @CUSTOMER_ID 
				AND POLICY_ID = @POLICY_ID
				
				INSERT INTO POL_CLAUSES(POL_CLAUSE_ID,
				CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,
				CLAUSE_ID,CLAUSE_TITLE,CLAUSE_DESCRIPTION,IS_ACTIVE,CREATED_BY,            
				CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,SUSEP_LOB_ID,CLAUSE_TYPE,
				ATTACH_FILE_NAME,CLAUSE_CODE)      
				SELECT  @MAX_POL_CLAUSE_ID , 
				 @CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID, 
				 CLAUSE_ID,CLAUSE_TITLE, CLAUSE_DESCRIPTION, 'Y', @USER_ID, GETDATE(),            
				NULL, NULL,@POLICY_LOB,CLAUSE_TYPE,ATTACH_FILE_NAME ,CLAUSE_CODE FROM MNT_CLAUSES WITH(NOLOCK)       
				WHERE CLAUSE_CODE = @CLAUSE_CODE --LOB_ID=@SUSEP_LOB_ID AND SUBLOB_ID = 0    
			 END		
		
		END
	ELSE 
		DELETE FROM POL_CLAUSES wHERE 
		CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID 
		AND CLAUSE_CODE = @CLAUSE_CODE
END

GO

