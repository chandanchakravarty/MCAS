IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_AssignClaimToPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_AssignClaimToPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                          
                          
Proc Name       : Proc_AssignClaimToPolicy    
Created by      : Sumit Chhabra          
Date            : 10/05/2006                          
Purpose         : Assign the existing dummy claim to system policy    
Revison History :                          
Used In                   : Wolverine                          
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/                          
CREATE PROC dbo.Proc_AssignClaimToPolicy                          
(                          
@CUSTOMER_ID int,    
@POLICY_ID int,    
@POLICY_VERSION_ID int,    
@DUMMY_POLICY_ID int,    
@CLAIM_ID int,  
@CONTINUE_WITH_LOB int                            
)                          
AS                          
BEGIN    
--Check if the user wishes to match the policy lob with the dummy policy lob  
--1 indicates to continue with LOB chosen, 0 indicates that policy and dummy policy LOB be matched  
if(@CONTINUE_WITH_LOB = 0)  
begin  
--Check that the LOB of the selected policy matches with the LOB of the dummy claim/policy specified    
 if(not    
   (SELECT LOB_ID FROM CLM_DUMMY_POLICY WHERE DUMMY_POLICY_ID=@DUMMY_POLICY_ID)=    
              (SELECT POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WHERE     
              CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID))    
   return -1 --discontinue processing as selected policy and dummy policy/claim have different LOBs specified    
end    
--Continue processing as selected policy and dummy policy/claim have matching LOBs or the user wishes to continue   

--Check for loss date of the claim and expiration & effective date of policy
declare @LOSS_DATE datetime
declare @NEW_POLICY_VERSION_ID int
select @LOSS_DATE=loss_date from clm_claim_info where claim_id=@claim_id

--Check loss of date against the current policy being worked upon          
if not exists(select customer_id from pol_customer_policy_list where          
  CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID           
   and (app_effective_date<=@LOSS_DATE and app_expiration_date>=@LOSS_DATE))          
begin --Loss date does not match with current policy, lets try other versions of the same policy      
	--Check for other versions of the policy           
	if exists(select policy_version_id from pol_customer_policy_list where          
	   CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID and           
	   (app_effective_date<=@LOSS_DATE and app_expiration_date>=@LOSS_DATE))          
	begin          
		select  @POLICY_VERSION_ID=policy_version_id from pol_customer_policy_list where          
		   CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID and           
		   (app_effective_date<=@LOSS_DATE and app_expiration_date>=@LOSS_DATE) order by app_effective_date          
	end   
	else --When neither current policy version matches nor are there any other policy versions matching loss date, return with error code of -2	
		return -2
end


--update claim info table with the data from the policy and set its dummy policy field to null to indicate that the status    
--of the claim has changed from dummy policy to actual policy    
 UPDATE     
  CLM_CLAIM_INFO     
 SET      
  CUSTOMER_ID=@CUSTOMER_ID,POLICY_ID=@POLICY_ID,POLICY_VERSION_ID=@POLICY_VERSION_ID,DUMMY_POLICY_ID=NULL    
 WHERE     
  CLAIM_ID=@CLAIM_ID    
    
 return @POLICY_VERSION_ID --return with policy_vesion_id so that if the version changes during the proc then it should
														--be done so at the page level also
    
     
END      



       


GO

