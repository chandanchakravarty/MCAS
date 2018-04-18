IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_AppGetPolicyState_For_Policy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_AppGetPolicyState_For_Policy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                              
Proc Name        : dbo.Proc_AppGetPolicyState_For_Policy          
Created by       : SHAFI          
Date             : 17/02/06                              
Purpose         : Retrieve state id and policy type    
Revison History :                              
Used In  : Wolverine                               
------------------------------------------------------------                              
Date     Review By          Comments                              
------   ------------       -------------------------*/                               
CREATE PROCEDURE Proc_AppGetPolicyState_For_Policy          
(                              
                               
 @CUSTOMER_ID int,                              
 @POL_ID  int,                              
 @POL_VERSION_ID smallint                              
)                              
AS                             
      
BEGIN   
declare @state_id int       
declare @policy_id int         
declare @policy_type varchar(30)  
SELECT @state_id=state_id, @policy_id=policy_type FROM POL_CUSTOMER_POLICY_LIST      
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID  
  
select @policy_type=v.lookup_value_code from mnt_lookup_values v join mnt_lookup_tables t on v.lookup_id=t.lookup_id  
where v.lookup_unique_id=@policy_id  
  
select @state_id as State_ID,@policy_type as Policy_Type  
    
END             



GO

