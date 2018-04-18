IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolQuoteDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolQuoteDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------    
Proc Name      : dbo.Proc_GetPolQuoteDetails  
Created by     : Deepak Gupta  
Date           : Sep 26 2006  
Purpose        : Get the Quote Details from QOT_CUSTOMER_QUOTE_LIST_POL  
Revison History :    
Modified by   :  Pravesh K. Chandel    
Description  :   look records if Corrective user Was Commited On the Policy  
dated		 :14 may 2007  

Modified by   :  Pravesh K. Chandel    
Description   :   Applying no lock while fetching Records
dated		 :3 Aug 2009  

Used In        : Wolverine    

------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------  
drop proc dbo.Proc_GetPolQuoteDetails    
 Proc_GetPolQuoteDetails 40761,1,3,'ENDORSE'
*/   

 

CREATE  PROC [dbo].[Proc_GetPolQuoteDetails]    
@CUSTOMER_ID INT,  
@POLICY_ID INT,    
@POLICY_VERSION_ID SMALLINT,  
@CALLEDFOR varchar(10) =null  
AS    
BEGIN    
if (@CALLEDFOR='ENDORSE')  
begin   
	declare @CORRECTIVE_USER_PRE_POLICY_VERSION_ID int  
	-- set @CORRECTIVE_USER_PRE_POLICY_VERSION_ID=dbo.func_FindCorrectiveUserProcess(@CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID)    

	/* premium posted only in 
	25	Commit New Business
	18	Commit Renewal Process
	14	Commit Endorsement Process
	16	Commit Reinstate Process
	32	Commit Rewrite Process
	*/ 
	SET @CORRECTIVE_USER_PRE_POLICY_VERSION_ID  = @POLICY_VERSION_ID

	IF NOT EXISTS ( SELECT  CUSTOMER_ID FROM QOT_CUSTOMER_QUOTE_LIST_POL with(nolock)  -- added by pravesh 
				WHERE CUSTOMER_ID =@CUSTOMER_ID  
				AND POLICY_ID = @POLICY_ID  
				AND POLICY_VERSION_ID = @CORRECTIVE_USER_PRE_POLICY_VERSION_ID 
			)
	BEGIN   
		select  @CORRECTIVE_USER_PRE_POLICY_VERSION_ID = NEW_POLICY_VERSION_ID from pol_policy_process  b with(nolock),pol_process_master m with(nolock)	
		where m.process_id=b.process_id
		and customer_id=@CUSTOMER_ID and policy_id=@POLICY_ID
		and NEW_POLICY_VERSION_ID<=@POLICY_VERSION_ID
		and b.process_id in(25,18,14,16,32,37) 
	--order by new_policy_version_id asc
	END
		SELECT  * FROM QOT_CUSTOMER_QUOTE_LIST_POL    with(nolock)  -- added by pravesh
		WHERE CUSTOMER_ID =@CUSTOMER_ID  
		AND POLICY_ID = @POLICY_ID  
		AND POLICY_VERSION_ID = @CORRECTIVE_USER_PRE_POLICY_VERSION_ID  
end  
else  
	SELECT  * FROM QOT_CUSTOMER_QUOTE_LIST_POL    with(nolock)   -- added by pravesh
	WHERE CUSTOMER_ID =@CUSTOMER_ID  
	AND POLICY_ID = @POLICY_ID  
	AND POLICY_VERSION_ID = @POLICY_VERSION_ID  

  
END    




GO

