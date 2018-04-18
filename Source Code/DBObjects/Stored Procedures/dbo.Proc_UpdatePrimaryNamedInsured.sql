IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePrimaryNamedInsured]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePrimaryNamedInsured]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------        
Proc Name          : Dbo.Proc_UpdatePrimaryNamedInsured        
Created by         : Mohit Gupta        
Date               : 28-10-2005      
Purpose            :         
Revison History :        
    
Modified by   :Pravesh K chandel    
Modified date  : 28 July 09    
purpose    : To update Home Coverages Itrack 6179    
    
Used In            :   Wolverine          
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
--DROP PROCEDURE Proc_UpdatePrimaryNamedInsured     
CREATE   PROCEDURE [dbo].[Proc_UpdatePrimaryNamedInsured]        
(        
 @POLICY_ID int,      
 @POLICY_VERSION_ID smallint,      
 @APPLICANT_ID int,        
 @CUSTOMER_ID int,        
 @MODIFIED_BY int,        
 @IS_PRIMARY_APPLICANT int  ,  
 @COMMISSION_PERCENT DECIMAL(8,4)   ,     
 @FEES_PERCENT  DECIMAL(8,4)   ,  
 @PRO_LABORE_PERCENT  DECIMAL(8,4) = null    
)        
AS        
BEGIN        
DECLARE @LOB_ID INT    ,@TRAN_TYPE INT ,@OPEN_POLICY INT  = 14560
SELECT @LOB_ID=POLICY_LOB,@TRAN_TYPE = TRANSACTION_TYPE   FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)    
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID     
 AND POLICY_VERSION_ID=@POLICY_VERSION_ID    
    
INSERT INTO POL_APPLICANT_LIST        
(        
POLICY_ID,POLICY_VERSION_ID,APPLICANT_ID,CUSTOMER_ID,CREATED_BY,CREATED_DATETIME,IS_PRIMARY_APPLICANT ,COMMISSION_PERCENT,FEES_PERCENT ,PRO_LABORE_PERCENT  
)        
VALUES        
(        
@POLICY_ID, @POLICY_VERSION_ID, @APPLICANT_ID,@CUSTOMER_ID,@MODIFIED_BY,GETDATE(),@IS_PRIMARY_APPLICANT,@COMMISSION_PERCENT,@FEES_PERCENT, @PRO_LABORE_PERCENT  
)        
--------Added by pravesh on 28 July 09 Itrack 6179    
IF (@LOB_ID=1)    
BEGIN    
 EXEC PROC_UPDATE_POLICY_HOME_COVERAGE_BY_APPLICANT  @CUSTOMER_ID,@APPLICANT_ID,@POLICY_ID,@POLICY_VERSION_ID    
END    
---- END HERE     
    
    
--update remuneration details
--update primary applicantif user changed primary applicant after remuneration/Billing save
--
	IF(@TRAN_TYPE <> @OPEN_POLICY )
	BEGIN
		IF(@IS_PRIMARY_APPLICANT = 1)
		BEGIN
			IF NOT EXISTS (SELECT * FROM POL_REMUNERATION WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID 
			AND POLICY_ID = @POLICY_ID
			AND POLICY_VERSION_ID = @POLICY_VERSION_ID
			AND CO_APPLICANT_ID = @APPLICANT_ID)
			BEGIN
					UPDATE POL_REMUNERATION SET CO_APPLICANT_ID = @APPLICANT_ID WHERE CUSTOMER_ID = @CUSTOMER_ID 
					AND POLICY_ID = @POLICY_ID
					AND POLICY_VERSION_ID = @POLICY_VERSION_ID
					--AND CO_APPLICANT_ID = @APPLICANT_ID
			
			END
			
			IF NOT EXISTS (SELECT * FROM ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID 
			AND POLICY_ID = @POLICY_ID
			AND POLICY_VERSION_ID = @POLICY_VERSION_ID
			AND CO_APPLICANT_ID = @APPLICANT_ID)
			BEGIN
					UPDATE ACT_POLICY_INSTALLMENT_DETAILS SET CO_APPLICANT_ID = @APPLICANT_ID WHERE CUSTOMER_ID = @CUSTOMER_ID 
					AND POLICY_ID = @POLICY_ID
					AND POLICY_VERSION_ID = @POLICY_VERSION_ID
					--AND CO_APPLICANT_ID = @APPLICANT_ID
			
			END
			
			
		END
	END
END
 


        
      
    
    
GO

