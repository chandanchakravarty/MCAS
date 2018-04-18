IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateReinsurancePostingContract]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateReinsurancePostingContract]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name       : dbo.Proc_AddReinsuranceContract    
Created by      : Shafee    
Date            : 4/1/2005    
Purpose     :         
Revison History :      
Used In  : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
  
CREATE  PROC Dbo.Proc_UpdateReinsurancePostingContract     
(    
@GL_ID INT,
@CONTRACT_ID smallint,    
@COMMISION_APPLICABLE char(1),    
@REIN_PREMIUM_ACT int,    
@REIN_PAYMENT_ACT int,    
@REIN_COMMISION_ACT int,    
@REIN_COMMISION_RECEVABLE int    
)    
AS    
Begin    
--Insert Record    
    
update MNT_REINSURANCE_POSTING set    
    
 COMMISION_APPLICABLE=@COMMISION_APPLICABLE,    
 REIN_PREMIUM_ACT= @REIN_PREMIUM_ACT,    
 REIN_PAYMENT_ACT= @REIN_PAYMENT_ACT,    
 REIN_COMMISION_ACT= @REIN_COMMISION_ACT,    
 REIN_COMMISION_RECEVABLE= @REIN_COMMISION_RECEVABLE   
 where    
 CONTRACT_ID=@CONTRACT_ID AND GL_ID=@GL_ID
    
    
    
    
End    
    
  



GO

