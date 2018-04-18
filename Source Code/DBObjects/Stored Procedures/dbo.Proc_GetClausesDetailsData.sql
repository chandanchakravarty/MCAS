IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClausesDetailsData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClausesDetailsData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                              
Proc Name        : dbo.[Proc_GetClausesDetailsData]                              
Created by       : Praveen Kumar                            
Date             : 02/05/2010                              
Purpose          : retrieving data from MNT_CLAUSES                                                     
Used In          : Ebix Advantage                          
------------------------------------------------------------                              
Date     Review By          Comments                              
------   ------------       -------------------------*/                              
--drop proc dbo.[Proc_GetClausesDetailsData] 1   
    
CREATE PROCEDURE [dbo].[Proc_GetClausesDetailsData]     
@CLAUSE_ID INT,
@CUSTOMER_ID INT =NULL,
@POLICY_ID INT =NULL,
@POLICY_VERSION_ID INT =NULL,
@IS_CHECKED NCHAR(1) = NULL    
AS    
BEGIN     
IF ISNULL(@IS_CHECKED,'0') = '1'
BEGIN
	SELECT    
	MNT.CLAUSE_ID,  
	MNT.LOB_ID,  
	MNT.SUBLOB_ID,  
	POL.CLAUSE_TITLE,  
	POL.CLAUSE_DESCRIPTION,  
	ISNULL(MNT.IS_ACTIVE,'N') AS IS_ACTIVE,  
	--MNT.CREATED_BY,  
	--MNT.CREATED_DATETIME,  
	MNT.MODIFIED_BY,  
	MNT.LAST_UPDATED_DATETIME  ,
	POL.CLAUSE_TYPE,
	MNT.PROCESS_TYPE,
	POL.ATTACH_FILE_NAME,
	POL.CLAUSE_CODE
	FROM MNT_CLAUSES MNT WITH(NOLOCK)
	LEFT OUTER JOIN POL_CLAUSES POL WITH(NOLOCK)
	ON POL.CLAUSE_ID = MNT.CLAUSE_ID     
	WHERE POL.CUSTOMER_ID = @CUSTOMER_ID AND POL.POLICY_ID = @POLICY_ID AND POL.POLICY_VERSION_ID = @POLICY_VERSION_ID AND POL.CLAUSE_ID =@CLAUSE_ID  	
END
ELSE
BEGIN
	SELECT    
	CLAUSE_ID,  
	LOB_ID,  
	SUBLOB_ID,  
	CLAUSE_TITLE,  
	CLAUSE_DESCRIPTION,  
	ISNULL(IS_ACTIVE,'N') AS IS_ACTIVE,  
	--CREATED_BY,  
	--CREATED_DATETIME,  
	MODIFIED_BY,  
	LAST_UPDATED_DATETIME ,
	CLAUSE_TYPE,
	PROCESS_TYPE,
	ATTACH_FILE_NAME ,
	CLAUSE_CODE
	FROM MNT_CLAUSES WITH(NOLOCK)   
	WHERE CLAUSE_ID =@CLAUSE_ID    
 END
END 
GO

