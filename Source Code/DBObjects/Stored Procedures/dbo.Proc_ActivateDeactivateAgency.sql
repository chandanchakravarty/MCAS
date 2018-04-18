IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateAgency]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateAgency]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_ActivateDeactivateAgency    
Created by      : Anurag        
Date            : 14 Mar,2005        
Purpose         : To Activate/Deactivate the record in Agency table        
Revison History :        
Used In         : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
-- drop proc dbo.Proc_ActivateDeactivateAgency 321,'N'       
CREATE  PROC dbo.Proc_ActivateDeactivateAgency        
(        
 @CODE     numeric(9),     
 @IS_ACTIVE  Char(1)        
)        
AS        
BEGIN        
      
 DECLARE @AGENCY_ID Int     
 SELECT @AGENCY_ID = AGENCY_ID FROM MNT_AGENCY_LIST     
 WHERE AGENCY_ID   = @CODE        
 
  
IF( @IS_ACTIVE='N')  
 BEGIN  
  --Checking in customer table    
  IF EXISTS (  SELECT CUSTOMER_AGENCY_ID FROM CLT_CUSTOMER_LIST      
      WHERE  CUSTOMER_AGENCY_ID = @AGENCY_ID  )  
  return -2    
     
  --Checking in application table    
  IF  EXISTS (  SELECT APP_AGENCY_ID FROM APP_LIST      
   WHERE  APP_AGENCY_ID = @AGENCY_ID )
  return -2    
     
     
  --Checking in deposit table    
  IF  EXISTS (  select main.deposit_id    
   from act_current_deposits main     
   left join act_current_deposit_line_items det on main.deposit_id = det.deposit_id    
   where det.deposit_type='AGN' and det.receipt_from_id  = @AGENCY_ID )  
  return -2    
     
     
  --Checking in journal entry table    
  IF  EXISTS (  select main.deposit_id    
   from act_current_deposits main     
   left join act_current_deposit_line_items det on main.deposit_id = det.deposit_id    
   where det.deposit_type='AGN' and det.receipt_from_id  = @AGENCY_ID )   
  return -2

else
BEGIN  
  UPDATE MNT_AGENCY_LIST        
  SET         
  Is_Active = @IS_ACTIVE       
  WHERE        
  AGENCY_ID   = @CODE        
         
  return 1 
END    
 END  
ELSE  
 BEGIN  
  UPDATE MNT_AGENCY_LIST        
  SET         
  Is_Active = @IS_ACTIVE       
  WHERE        
  AGENCY_ID   = @CODE        
         
  return 1  
 
 END  
END    
    


GO

