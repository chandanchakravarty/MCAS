IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteAgency]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteAgency]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------        
Proc Name     : dbo.Proc_DeleteAgency        
Created by    : Ashwani         
Date         : July 25, 2005    
Purpose     : Deletes a record from MNT_AGENCY_LIST  
Revison History :    
Used In   :   Wolverine           
 ------------------------------------------------------------                    
Date     Review By          Comments                  
         
------   ------------       -------------------------*/        
-- drop proc dbo.Proc_DeleteAgency    
CREATE PROC [dbo].[Proc_DeleteAgency]    
(    
  @AGENCY_ID Int    
)    
AS    
BEGIN    
 --Checking in customer table  
 IF EXISTS (  SELECT CUSTOMER_AGENCY_ID FROM CLT_CUSTOMER_LIST WITH(NOLOCK)   
     WHERE  CUSTOMER_AGENCY_ID = @AGENCY_ID  )    
 return -2  
  
 --Checking in application table  
 IF  EXISTS (  SELECT APP_AGENCY_ID FROM APP_LIST WITH(NOLOCK)   
  WHERE  APP_AGENCY_ID = @AGENCY_ID )  
 return -2  
  
  
 --Checking in deposit table  
 IF  EXISTS (  select main.deposit_id  
  from act_current_deposits main WITH(NOLOCK)  
  left join act_current_deposit_line_items det on main.deposit_id = det.deposit_id  
  where det.deposit_type='AGN' and det.receipt_from_id  = @AGENCY_ID )  
 return -2  
  
  
 --Checking in journal entry table  
 IF  EXISTS (  select main.deposit_id  
  from act_current_deposits main WITH(NOLOCK)  
  left join act_current_deposit_line_items det on main.deposit_id = det.deposit_id  
  where det.deposit_type='AGN' and det.receipt_from_id  = @AGENCY_ID )  
 return -2  
 
 --Checking in AGENCY UNDERWRITERS table
 
 IF EXISTS( SELECT AGENCY_ID FROM MNT_AGENCY_UNDERWRITERS WITH(NOLOCK)
 WHERE AGENCY_ID=@AGENCY_ID)
 RETURN -2
 
 IF EXISTS(SELECT AGENCY_ID FROM ACT_AGENCY_OPEN_ITEMS WITH(NOLOCK)
 WHERE AGENCY_ID=@AGENCY_ID)
 RETURN -2
 
  
  
 DELETE FROM MNT_AGENCY_LIST    
 WHERE  AGENCY_ID = @AGENCY_ID  
  
 return 1  
    
END  
  
  
  
  
  
  
GO

