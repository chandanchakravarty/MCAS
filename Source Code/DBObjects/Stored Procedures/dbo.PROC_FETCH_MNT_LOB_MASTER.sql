IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_FETCH_MNT_LOB_MASTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_FETCH_MNT_LOB_MASTER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                            
Proc Name       : DBO.[PROC_FETCH_MNT_LOB_MASTER]                            
Created by      : PRADEEP KUSHWAHA                          
Date            : 03-09-2010                            
Purpose			: retrieving data from MNT_LOB_MASTER                            
Revison History :                   
Modify by       : Pradeep Kushwaha   
Date            : 17-Oct-2010                   
Purpose			: Get SUSEP Process number table data based on the LobId          
                         
Used In			: Ebix Advantage                        
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/                            
--DROP  PROC DBO.PROC_FETCH_MNT_LOB_MASTER
   
CREATE PROC [dbo].[PROC_FETCH_MNT_LOB_MASTER]      
		@LOB_ID INT 
AS                                                     
BEGIN    
DECLARE @SUSEP_PROCESS_NUMBERS  NVARCHAR(10) 
SELECT  LOB_ID,LOB_CODE,LOB_DESC,LOB_CATEGORY,
		LOB_TYPE,LOB_PKG,LOB_ACORD_STD,DEF_CLAIMS_TYPE,
		LOB_PREFIX,LOB_SUFFIX,LOB_SEED,MAPPING_LOOKUP_ID,
		OVERRIDE_LOB_PREFIX,REWRITE_SEED,SUSEP_LOB_ID,
		SUSEP_LOB_CODE,COMMISSION_LEVEL,IS_ACTIVE,
		CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,APPLICABLE_COMMISSION ,
		@SUSEP_PROCESS_NUMBERS as SUSEP_PROCESS_NUMBERS,
		isnull(ADMINISTRATIVE_EXPENSE,0)as ADMINISTRATIVE_EXPENSE
FROM   
		MNT_LOB_MASTER WITH(NOLOCK)  
WHERE  
		LOB_ID= @LOB_ID 

SELECT  LOB_ID ,SUSEP_PROCESS_ID,SUSEP_PROCESS_NO 
		FROM   MNT_PRODUCT_SUSEP_PROCESS_NUMBER WITH(NOLOCK)  
WHERE  LOB_ID= @LOB_ID 		

END
GO

