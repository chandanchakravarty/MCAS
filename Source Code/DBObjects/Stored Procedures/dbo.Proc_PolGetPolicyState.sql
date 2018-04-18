IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_PolGetPolicyState]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_PolGetPolicyState]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                
Proc Name        : dbo.Proc_PolGetPolicyState            
Created by       : Sumit Chhabra            
Date             : 08/03/2006  
Purpose         : Retrieve state id and policy type  for policy  
Revison History :                                
Used In  : Wolverine                                 
------------------------------------------------------------                                
Date     Review By          Comments                                
------   ------------       -------------------------*/                                 
CREATE PROCEDURE Proc_PolGetPolicyState   
(                 
                                 
 @CUSTOMER_ID int,                                
 @POLICY_ID  int,                                
 @POLICY_VERSION_ID smallint                                
)                                
AS                               
        
BEGIN     
declare @state_id int         
declare @policy_type_id int           
declare @policy_type varchar(30)    
SELECT @state_id=state_id, @policy_type_id=policy_type FROM POL_CUSTOMER_POLICY_LIST  
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID      
  
select @policy_type=v.lookup_value_code from mnt_lookup_values v join mnt_lookup_tables t on v.lookup_id=t.lookup_id    
where v.lookup_unique_id=@policy_type_id    
    
SELECT @STATE_ID AS STATE_ID,@POLICY_TYPE AS POLICY_TYPE,@POLICY_TYPE_ID AS POLICY_TYPE_ID   
      
END               
  



GO

