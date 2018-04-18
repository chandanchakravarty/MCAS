IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETXMLACT_BUDGET_PLAN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETXMLACT_BUDGET_PLAN]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name       : dbo.ACT_BUDGET_PLAN      
Created by      : Manoj Rathore    
Date            : 6/22/2007      
Purpose     :To get xml record of Budget Plan entity.      
Revison History :      
Used In  : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
-- DROP PROC dbo.Proc_GetXMLACT_BUDGET_PLAN   1  
CREATE PROC [dbo].[PROC_GETXMLACT_BUDGET_PLAN]      
(      
@IDEN_ROW_ID     INT    
    
)      
AS BEGIN      
SELECT       
CONVERT(VARCHAR,GL_ID) + '-'+CONVERT(VARCHAR,FISCAL_ID) AS GL_ID,    
ACCOUNT_ID as BUDGET_CATEGORY_ID ,  
FISCAL_ID,    
--FISCAL_START_DATE,    
--FISCAL_END_DATE,    
cast(JAN_BUDGET as bigint) as JAN_BUDGET,    
cast(FEB_BUDGET as bigint) as FEB_BUDGET,    
cast(MARCH_BUDGET as bigint) as MARCH_BUDGET,    
cast(APRIL_BUDGET as bigint) as APRIL_BUDGET,    
cast(MAY_BUDGET as bigint) as MAY_BUDGET,    
cast(JUNE_BUDGET as bigint) as JUNE_BUDGET,    
cast(JULY_BUDGET as bigint) as JULY_BUDGET ,    
cast(AUG_BUDGET as bigint) as AUG_BUDGET,    
cast(SEPT_BUDGET as bigint) as SEPT_BUDGET,    
cast(OCT_BUDGET as bigint) as OCT_BUDGET ,    
cast(NOV_BUDGET as bigint) as NOV_BUDGET,    
cast(DEC_BUDGET as bigint)as DEC_BUDGET  
,  

cast(isnull(JAN_BUDGET,0) as bigint) +     
cast(isnull(FEB_BUDGET,0) as bigint) +     
cast(isnull(MARCH_BUDGET,0) as bigint) +     
cast(isnull(APRIL_BUDGET,0) as bigint) +     
cast(isnull(MAY_BUDGET,0) as bigint) +   
cast(isnull(JUNE_BUDGET,0) as bigint)  +   
cast(isnull(JULY_BUDGET,0) as bigint) +   
cast(isnull(AUG_BUDGET,0) as bigint)  +  
cast(isnull(SEPT_BUDGET,0) as bigint)  +  
cast(isnull(OCT_BUDGET,0) as bigint)  +  
cast(isnull(NOV_BUDGET,0) as bigint)  +  
cast(isnull(DEC_BUDGET,0) as bigint)

 AS TOTAL_CALCULATION_AMT  
  
         
FROM ACT_BUDGET_PLAN       
WHERE IDEN_ROW_ID   = @IDEN_ROW_ID        
END      
      
  
  


GO

