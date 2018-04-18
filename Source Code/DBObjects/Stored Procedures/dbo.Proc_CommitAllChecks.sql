IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CommitAllChecks]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CommitAllChecks]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*---------------------------------------------------------------------------          
CREATE BY     	: Praveen Kasana        
CREATE DATETIME : 14 Dec '2006         
PURPOSE      	: This will commit all the general pending checks (except claims) in system
REVIOSN HISTORY          
        
Revised By  Date  Reason          
----------------------------------------------------------------------------*/          
-- drop proc dbo.Proc_CommitAllChecks  
    
CREATE  PROC dbo.Proc_CommitAllChecks          
(     

 @IS_COMMITED      NVARCHAR(2),  
 @DATE_COMMITTED      DATETIME,  
 @MODIFIED_BY       INT,  
 @LAST_UPDATED_DATETIME DATETIME,  
 @DIV_ID     INT,  
 @DEPT_ID     INT,  
 @PC_ID      INT  
     
)
AS
BEGIN


--Declare constants for Check Type

DECLARE	@ACC_Check	Int
DECLARE	@RPC_Check	Int
DECLARE	@ROP_Check	Int
DECLARE	@RSC_Check	Int
DECLARE	@CC_Check	Int
DECLARE	@VC_Check	Int
DECLARE	@MOC_Check	Int
DECLARE	@REC_Check	Int

SET	@ACC_Check	=	2472
SET	@RPC_Check	=	2474
SET	@ROP_Check	=	9935
SET	@RSC_Check	=	9936
SET	@CC_Check	=	9937 --CLAIMS NOT USED
SET	@VC_Check	=	9938
SET	@MOC_Check	=	9940 
SET	@REC_Check	=	9945

CREATE TABLE #TEMP_ACT_CHECK_INFO       
(    
 [ROW_ID] [int] IDENTITY(1,1) NOT NULL ,          
 CHECK_ID  INT  ,
 IS_COMMITED CHAR(2)	
  
)

INSERT INTO #TEMP_ACT_CHECK_INFO        
(  
 CHECK_ID,
 IS_COMMITED
)
SELECT CHECK_ID ,ISNULL(IS_COMMITED,'N') 
FROM ACT_CHECK_INFORMATION WHERE ISNULL(IS_COMMITED,'N') <> 'Y'  AND 
CHECK_TYPE IN(@ACC_Check,@RPC_Check,@ROP_Check,@RSC_Check,@VC_Check,@REC_Check,@MOC_Check)

--------DECLARING SURSOR SELECT * FROM #TEMP_ACT_CHECK_INFO
DECLARE CUR_COMMIT_CHECKS CURSOR
	LOCAL FORWARD_ONLY STATIC 
	FOR 
	SELECT CHECK_ID FROM #TEMP_ACT_CHECK_INFO 
	--DECLARE VAR FOR FETCHIN IT FROM CURSOR	
	DECLARE @CHECK_ID INT --,@COMMIT CHAR(2)

	OPEN CUR_COMMIT_CHECKS

	FETCH NEXT FROM CUR_COMMIT_CHECKS INTO @CHECK_ID

	WHILE @@FETCH_STATUS = 0   
	 BEGIN
		EXEC Proc_CommitACT_CHECK_INFORMATION @CHECK_ID,@IS_COMMITED,@DATE_COMMITTED,@MODIFIED_BY,@LAST_UPDATED_DATETIME,@DIV_ID,@DEPT_ID,@PC_ID

		FETCH NEXT FROM CUR_COMMIT_CHECKS INTO @CHECK_ID
		IF @@FETCH_STATUS <> 0 
			BREAK
	 END

	--CLOSING AND DEALLOCATING THE CURSOR
	DROP TABLE #TEMP_ACT_CHECK_INFO
	CLOSE CUR_COMMIT_CHECKS
	DEALLOCATE CUR_COMMIT_CHECKS

END
 









GO

