IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimDriversInformationLookupUp]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimDriversInformationLookupUp]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                              
Proc Name       : dbo.Proc_GetClaimDriversInformationLookupUp                        
Created by      : Sumit Chhabra                            
Date            : 04/05/2006                              
Purpose         : Get Table Data for various dropdowns at use in Driver Information screen        
Created by      : Sumit Chhabra                             
Revison History :                              
Used In        : Wolverine                              
------------------------------------------------------------                              
Date     Review By          Comments                              
------   ------------       -------------------------*/                              
CREATE PROC dbo.Proc_GetClaimDriversInformationLookupUp        
@CUSTOMER_ID int,  
@POLICY_ID int,  
@POLICY_VERSION_ID smallint,  
@CLAIM_ID int      
AS                              
BEGIN                               
  
--Get the States List  
SELECT STATE_ID,STATE_NAME FROM MNT_COUNTRY_STATE_LIST  
        
--Get Lookup Values for YESNO dropdown  
exec Proc_GetLookupValues 'YESNO'  
        
--Get Owners List for current claim  
exec Proc_GET_OWNERS_LIST @CLAIM_ID  
  
--Get Named Insured (0 is being passed as paramter to vehicle_owner to let it know that the request is coming for claims driver screen  
exec Proc_GetNamedInsured @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,0,@CLAIM_ID  
  
--Get List of Policy Drivers against current policy  
exec Proc_GetPolicyDriversListForClaims @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID  
  
--Get Lookup for Purpose of Use  
exec Proc_GetLookupValues 'VHUCP',null,1   
  
--Commented the following recordset as the field contents have changed
--Get Claim Relationship to Insured Values --(type_id = 10 = Relationship to the Insured)  
--  SELECT DETAIL_TYPE_ID,DETAIL_TYPE_DESCRIPTION FROM  CLM_TYPE_DETAIL WHERE TYPE_ID=10   
                        
END                        
                    
  



GO

