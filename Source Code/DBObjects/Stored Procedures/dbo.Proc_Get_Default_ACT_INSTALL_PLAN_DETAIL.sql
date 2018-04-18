IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_Default_ACT_INSTALL_PLAN_DETAIL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_Default_ACT_INSTALL_PLAN_DETAIL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------      
Proc Name       : dbo.Proc_Get_Default_ACT_INSTALL_PLAN_DETAIL      
Created by      : Vijay Joshi      
Date            : 17/03/2006    
Purpose      : To get the default installment plan   
Modified by:sonal
Date:20 july 2010 
Revison History :      
Used In  : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
-- drop proc dbo.Proc_Get_Default_ACT_INSTALL_PLAN_DETAIL    6  
CREATE PROC [dbo].[Proc_Get_Default_ACT_INSTALL_PLAN_DETAIL]      
(    
@APPLABLE_POLTERM INT = null ,
@LOB_ID INT=null /* to check for particular product too*/   
)    
AS      
BEGIN     
DECLARE @DEFAULT_PLAN Int  
   
SET @DEFAULT_PLAN  =0   
SELECT @DEFAULT_PLAN = ISNULL(IDEN_PLAN_ID ,0)  
FROM ACT_INSTALL_PLAN_DETAIL      
WHERE ISNULL(DEFAULT_PLAN,0) = 1    
AND APPLABLE_POLTERM = @APPLABLE_POLTERM  
      
IF ( @DEFAULT_PLAN = 0)  
 SELECT @DEFAULT_PLAN = ISNULL(IDEN_PLAN_ID ,0)  
 FROM ACT_INSTALL_PLAN_DETAIL      
 WHERE ISNULL(SYSTEM_GENERATED_FULL_PAY,0)  = 1  
  
SELECT @DEFAULT_PLAN  
END    
    
  
    
  
    
  
  
GO

