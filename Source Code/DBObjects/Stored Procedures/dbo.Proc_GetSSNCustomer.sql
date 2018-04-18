IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetSSNCustomer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetSSNCustomer]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------              
Proc Name       : dbo.[Proc_GetSSNCustomer]      
Created by      : Praveen kasana        
Date            : 29 August 2009
Purpose         : Selects SSNO of Customer Used in Make Application (Watercraft)  */

CREATE PROC [dbo].[Proc_GetSSNCustomer]                                        
(                                        
  @CUSTOMER_ID  INT                   
)                         
AS                                        
BEGIN  
	SELECT SSN_NO FROM CLT_CUSTOMER_LIST WITH(NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMER_ID	
END



GO

