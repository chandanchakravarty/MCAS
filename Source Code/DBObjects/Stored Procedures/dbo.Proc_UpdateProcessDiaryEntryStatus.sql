IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateProcessDiaryEntryStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateProcessDiaryEntryStatus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------              
Proc Name        : dbo.Proc_UpdateProcessDiaryEntryStatus      
Created by        : Vijay Arora      
Date                : 22-12-2005      
Purpose          : Update the Diary Entry Status for process related diary entries     
Revison History  :              
MODIFIED BY  :PRAVESH K CHANDEL    
MODIFIED DATE : 25 SEP 2007    
PURPOSE  : MARK COMPLETE DIARY ENTRY ONLY FOR PROCESS MODULE    
    
MODIFIED BY  :PRAVESH K CHANDEL    
MODIFIED DATE : 18 oct 2007    
PURPOSE  : MARK COMPLETE DIARY ENTRY for Application submited to Policy    
    
Used In          : Wolverine              
------------------------------------------------------------              
Date     Review By          Comments     
DROP PROC dbo.Proc_UpdateProcessDiaryEntryStatus              
------   ------------       -------------------------*/              
CREATE  PROC dbo.Proc_UpdateProcessDiaryEntryStatus              
(              
 @CUSTOMER_ID int,    
 @POLICY_ID int,    
 @POLICY_VERSION_ID smallint,    
 @PROCESS_ROW_ID  int,              
 @LIST_OPEN   char(2),  
 @CALLEDFROM varchar(10) = null      
)              
AS              
              
BEGIN 

Declare @ERROR_CODE INT
SET @ERROR_CODE=0


UPDATE TODOLIST 
SET LISTOPEN = @LIST_OPEN     
WHERE     
CUSTOMER_ID = @CUSTOMER_ID AND    
POLICY_ID = @POLICY_ID AND    
POLICY_VERSION_ID = @POLICY_VERSION_ID AND    
(PROCESS_ROW_ID = @PROCESS_ROW_ID )--OR PROCESS_ROW_ID IS NULL)    
AND ISNULL(MODULE_ID,0)=1 -- FOR PROCESS MODULE    

--(RULES_VERIFIED IS NULL OR RULES_VERIFIED=0)    
--Update Application Submiitted to Policy Follow up    
--if @CALLEDFROM=30 i.e Rollback Rescind Process  
SELECT @ERROR_CODE=@@ERROR
	IF @ERROR_CODE<>0
		GOTO PROBLEM

if(@CALLEDFROM != '30')  
begin  
	 UPDATE TODOLIST  
		SET LISTOPEN = @LIST_OPEN     
		WHERE     
		CUSTOMER_ID = @CUSTOMER_ID AND    
		POLICY_ID = @POLICY_ID AND    
		POLICY_VERSION_ID = @POLICY_VERSION_ID AND LISTTYPEID=35    

		SELECT @ERROR_CODE=@@ERROR
		IF @ERROR_CODE<>0
		GOTO PROBLEM
end 
	
if(@CALLEDFROM = '9')  
	begin  
		UPDATE TODOLIST   
		SET LISTOPEN = @LIST_OPEN     
		WHERE     
		CUSTOMER_ID = @CUSTOMER_ID AND    
		POLICY_ID = @POLICY_ID AND    
		POLICY_VERSION_ID = @POLICY_VERSION_ID AND LISTTYPEID=37 AND MODULE_ID=1  

		SELECT @ERROR_CODE=@@ERROR
		IF @ERROR_CODE<>0
		GOTO PROBLEM
	end   
--updating/marking renewal Folllowup from EOD for Expiring in next 35 or 10 days added by Pravesh on 11 Jan 2009 Itrack 5340
if(@CALLEDFROM = '18')  
	begin  
		UPDATE TODOLIST   
		SET LISTOPEN = @LIST_OPEN     
		WHERE     
		CUSTOMER_ID = @CUSTOMER_ID AND    
		POLICY_ID = @POLICY_ID AND    
		POLICY_VERSION_ID = @POLICY_VERSION_ID AND LISTTYPEID=41 AND MODULE_ID=1  

		SELECT @ERROR_CODE=@@ERROR
		IF @ERROR_CODE<>0
		GOTO PROBLEM
	end   


RETURN 1
PROBLEM:

RETURN -1

END     






GO

