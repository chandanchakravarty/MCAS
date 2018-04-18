    
 /*----------------------------------------------------------                      
Proc Name       : dbo.[Proc_InsertMasterValue]              
Created by      : SNEHA          
Date            : 24/10/2011                      
Purpose         :INSERT RECORDS IN MNT_MASTER_VALUE TABLE.                      
Revison History :                      
Used In        : Ebix Advantage                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------      
DROP PROC dbo.[Proc_InsertMasterValue]        
      
*/  

      
CREATE PROC [dbo].Proc_InsertMasterValue   
(   
@TYPE_UNIQUE_ID		    [INT] OUT,	
@TYPE_ID                [INT],
@LOSS_TYPE				[NVARCHAR] (10),
@CODE					[NVARCHAR] (10),
@NAME					[NVARCHAR] (100),
@DESCRIPTION			[NVARCHAR] (100),
@RECOVERY_TYPE			[NVARCHAR] (10),
@IS_ACTIVE				[nchar] (2)
)
AS 
BEGIN
SELECT  @TYPE_UNIQUE_ID=ISNULL(MAX(TYPE_UNIQUE_ID),0)+1 FROM MNT_MASTER_VALUE    

INSERT INTO MNT_MASTER_VALUE
(
TYPE_UNIQUE_ID		 ,	
TYPE_ID              ,
LOSS_TYPE			 ,
CODE				 ,
NAME				 ,
DESCRIPTION			 ,
RECOVERY_TYPE		 ,
IS_ACTIVE				 
)
VALUES
(
@TYPE_UNIQUE_ID		 ,	
@TYPE_ID			 ,
@LOSS_TYPE			 ,
@CODE				 ,
@NAME				 ,
@DESCRIPTION		 ,
@RECOVERY_TYPE		 ,
@IS_ACTIVE							 
)
END
