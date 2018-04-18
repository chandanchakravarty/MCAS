IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_AppGetPolicyState]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_AppGetPolicyState]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                            
Proc Name        : dbo.Proc_AppGetPolicyState        
Created by       : Sumit Chhabra        
Date             : 29/12/2005                            
Purpose         : Retrieve state id and policy type  
Revison History :                            
Used In  : Wolverine                             
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/                             
--drop proc Proc_AppGetPolicyState
CREATE PROCEDURE Proc_AppGetPolicyState        
(                            
                             
 @CUSTOMER_ID int,                            
 @APP_ID  int,                            
 @APP_VERSION_ID smallint                            
)                            
AS                           
    
BEGIN 
declare @state_id int     
declare @policy_id int       
declare @policy_type varchar(30)
--declare @policy_type_code int
SELECT @state_id=state_id, @policy_id=policy_type FROM app_list    
WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID

select @policy_type=v.lookup_value_code from mnt_lookup_values v join mnt_lookup_tables t on v.lookup_id=t.lookup_id
where v.lookup_unique_id=@policy_id

select @state_id as State_ID,@policy_type as Policy_Type,@policy_id as PKG_CODE
  
END           




GO

