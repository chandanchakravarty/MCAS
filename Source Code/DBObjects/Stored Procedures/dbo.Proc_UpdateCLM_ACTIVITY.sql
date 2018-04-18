IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_ACTIVITY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_ACTIVITY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------    
Proc Name       : dbo.Proc_UpdateCLM_ACTIVITY    
Created by      : Vijay Arora    
Date            : 5/24/2006    
Purpose     : To Update a record in table named CLM_ACTIVITY    
Revison History :    
Used In  : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
drop proc dbo.Proc_UpdateCLM_ACTIVITY    
------   ------------       -------------------------*/    
CREATE PROC [dbo].[Proc_UpdateCLM_ACTIVITY]    
(    
 @CLAIM_ID     int,    
 @ACTIVITY_ID     int,    
 @REASON_DESCRIPTION     varchar(500),    
 @MODIFIED_BY     int,  
 @RESERVE_TRAN_CODE int,  
 @ACTION_ON_PAYMENT int,  
 @ACCOUNTING_SUPPRESSED bit = 0    ,
 @COI_TRAN_TYPE  int,
 @TEXT_ID int = NULL,
 @TEXT_DESCRIPTION varchar(2000) = NULL 
)    
AS    
BEGIN    
Update  CLM_ACTIVITY    
set    
REASON_DESCRIPTION  =  @REASON_DESCRIPTION,    
MODIFIED_BY  =  @MODIFIED_BY,    
LAST_UPDATED_DATETIME  =  GETDATE() ,  
RESERVE_TRAN_CODE = @RESERVE_TRAN_CODE,  
ACTION_ON_PAYMENT = @ACTION_ON_PAYMENT,  
ACCOUNTING_SUPPRESSED = @ACCOUNTING_SUPPRESSED,
COI_TRAN_TYPE = @COI_TRAN_TYPE,
TEXT_ID = @TEXT_ID,
TEXT_DESCRIPTION = @TEXT_DESCRIPTION 
 where  CLAIM_ID = @CLAIM_ID and @ACTIVITY_ID  =  ACTIVITY_ID    
END  
GO

