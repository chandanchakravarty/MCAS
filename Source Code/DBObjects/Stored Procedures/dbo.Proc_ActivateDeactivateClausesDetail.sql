IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateClausesDetail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateClausesDetail]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                
Proc Name       : dbo.[Proc_ActivateDeactivateClausesDetail]                
Created by      : Praveen Kumar   
Date            : 02/05/2010             
Purpose         :To Activate and deactivate records in MNT_CLAUSES table.                
Revison History :                
Used In        : Ebix Advantage                
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
--DROP PROC dbo.[Proc_ActivateDeactivateClausesDetail]         
      
      
CREATE  PROC [dbo].[Proc_ActivateDeactivateClausesDetail]      
(              
 @CLAUSE_ID Int,                     
 @IS_ACTIVE   NChar(1)              
)              
AS              
BEGIN      
UPDATE MNT_CLAUSES          
 SET               
    Is_Active  = @IS_ACTIVE             
 WHERE              
    CLAUSE_ID  =@CLAUSE_ID             
END 
GO

