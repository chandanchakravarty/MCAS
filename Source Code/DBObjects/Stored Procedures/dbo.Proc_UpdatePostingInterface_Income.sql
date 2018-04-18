IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePostingInterface_Income]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePostingInterface_Income]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------        
Proc Name       : dbo.UpdatePostingInterface_Income        
Created by      : Ajit Singh Chahal        
Date            : 5/26/2005        
Purpose       :To add posting interface - Income        
Revison History :        
Used In        : Wolverine        
      
Modification History :      
      
Date  Modified By      Comments      
------------------------------------------------      
7th Apr'06 Swastika Gaur  Added  fields for fees      
      
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/      
--drop proc   Proc_UpdatePostingInterface_Income    
CREATE PROC [dbo].[Proc_UpdatePostingInterface_Income]        
(        
@GL_ID     int,      
@FISCAL_ID int =null,    
@INC_PRM_WRTN     int,      
@INC_PRM_WRTN_MCCA     int,      
@INC_PRM_WRTN_OTH_STATE_ASSESS_FEE     int,      
@INC_REINS_CEDED_EXCESS_CON     int,      
@INC_REINS_CEDED_CAT_CON     int,      
@INC_REINS_CEDED_UMBRELLA_CON     int,      
@INC_REINS_CEDED_FACUL_CON     int,      
@INC_REINS_CEDED_MCCA_CON     int,       
@INC_CHG_UNEARN_PRM     int,      
@INC_CHG_UNEARN_PRM_MCCA     int,      
@INC_CHG_UNEARN_PRM_OTH_STATE_FEE     int,      
@INC_CHG_CEDED_UNEARN_MCCA     int,      
@INC_CHG_CEDED_UNEARN_UMBRELLA_REINS     int,      
--Start : Added fields      
@INC_INSTALLMENT_FEES     int,      
@INC_RE_INSTATEMENT_FEES     int,      
@INC_NON_SUFFICIENT_FUND_FEES     int,      
@INC_LATE_FEES    int,    
@INC_SERVICE_CHARGE int ,    
@INC_CONVENIENCE_FEE int,    
--Added by Pradeep Kushwaha   
@INC_INTEREST_AMOUNT int,    
@INC_POLICY_TAXES int ,    
@INC_POLICY_FEES int,    
--Added till here     
--End      
@MODIFIED_BY     int,      
@LAST_UPDATED_DATETIME     datetime      
)        
AS        
BEGIN        
update ACT_GENERAL_LEDGER        
set        
 INC_PRM_WRTN= @INC_PRM_WRTN,      
 INC_PRM_WRTN_MCCA=@INC_PRM_WRTN_MCCA,      
 INC_PRM_WRTN_OTH_STATE_ASSESS_FEE= @INC_PRM_WRTN_OTH_STATE_ASSESS_FEE,      
 INC_REINS_CEDED_EXCESS_CON=@INC_REINS_CEDED_EXCESS_CON,      
 INC_REINS_CEDED_CAT_CON= @INC_REINS_CEDED_CAT_CON,      
 INC_REINS_CEDED_UMBRELLA_CON=@INC_REINS_CEDED_UMBRELLA_CON,      
 INC_REINS_CEDED_FACUL_CON=@INC_REINS_CEDED_FACUL_CON,      
 INC_REINS_CEDED_MCCA_CON= @INC_REINS_CEDED_MCCA_CON,      
 INC_CHG_UNEARN_PRM= @INC_CHG_UNEARN_PRM,      
 INC_CHG_UNEARN_PRM_MCCA= @INC_CHG_UNEARN_PRM_MCCA,      
 INC_CHG_UNEARN_PRM_OTH_STATE_FEE= @INC_CHG_UNEARN_PRM_OTH_STATE_FEE,      
 INC_CHG_CEDED_UNEARN_MCCA= @INC_CHG_CEDED_UNEARN_MCCA,      
 INC_CHG_CEDED_UNEARN_UMBRELLA_REINS=@INC_CHG_CEDED_UNEARN_UMBRELLA_REINS,      
 INC_INSTALLMENT_FEES =@INC_INSTALLMENT_FEES,      
 INC_RE_INSTATEMENT_FEES=@INC_RE_INSTATEMENT_FEES,      
 INC_NON_SUFFICIENT_FUND_FEES=@INC_NON_SUFFICIENT_FUND_FEES,      
 INC_LATE_FEES=@INC_LATE_FEES,     
 INC_SERVICE_CHARGE = @INC_SERVICE_CHARGE,    
 INC_CONVENIENCE_FEE = @INC_CONVENIENCE_FEE,      
 --Added by Pradeep Kushwaha on 30-August-2010 
 INC_INTEREST_AMOUNT=@INC_INTEREST_AMOUNT,  
 INC_POLICY_TAXES=@INC_POLICY_TAXES,  
 INC_POLICY_FEES=@INC_POLICY_FEES,  
 --Added till here
 MODIFIED_BY = @MODIFIED_BY,      
 LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME      
where GL_ID = @GL_ID      
and    
FISCAL_ID = @FISCAL_ID    
end         
        
        
      
    
    
    
GO

