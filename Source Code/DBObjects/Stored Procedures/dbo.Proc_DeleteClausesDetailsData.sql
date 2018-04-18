IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteClausesDetailsData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteClausesDetailsData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
/*----------------------------------------------------------                            
Proc Name      : dbo.[Proc_DeleteClausesDetailsData]                            
Created by     : Praveen Kumar                          
Date           : 02/05/2010                            
Purpose        : Delete data from MNT_CLAUSES                                                   
Used In        : Ebix Advantage                        
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/                            
--drop proc dbo.[Proc_DeleteClausesDetailsData]  
  
CREATE PROC [dbo].[Proc_DeleteClausesDetailsData] 
(  
@CLAUSE_ID INT  
)  
AS  
BEGIN  
 DELETE FROM MNT_CLAUSES         
 WHERE CLAUSE_ID=@CLAUSE_ID 
END
GO

