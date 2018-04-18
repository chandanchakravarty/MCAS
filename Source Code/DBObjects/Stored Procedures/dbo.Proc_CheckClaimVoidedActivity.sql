IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckClaimVoidedActivity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckClaimVoidedActivity]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_CheckClaimVoidedActivity        
Created by      : Santosh Kumar Gautam     
Date            : 27 Dec 2010   
Purpose         :     
Revison History :        
Used In   : CLAIM        
------------------------------------------------------------ */        
-- DROP PROC dbo.Proc_CheckClaimVoidedActivity       
 
CREATE PROC [dbo].[Proc_CheckClaimVoidedActivity]        
(        
 @CLAIM_ID            		int,    
 @ACTIVITY_ID         		int    
)        
AS        
    
BEGIN        
 
 SELECT IS_VOIDED_REVERSED_ACTIVITY FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID
 
 
END      

    
    
    
    
    
    
    
GO

