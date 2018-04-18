  
--Proc Name       : dbo.Proc_UpdateMasterValue       
--Created by      : SNEHA      
--Date            : 24/10/2011             
--Purpose   :To Update in MNT_MASTER_VALUE   
--Revison History :                
--Used In   : Ebix Advantage                
--------------------------------------------------------------                
--Date     Review By          Comments                
--------   ------------       -------------------------*/                
--DROP PROC dbo.Proc_UpdateMasterValue   
CREATE PROC [dbo].Proc_UpdateMasterValue    
(     
	@TYPE_UNIQUE_ID		    [INT] ,	
	@TYPE_ID			     [INT] ,
	@LOSS_TYPE				[NVARCHAR] (10),	
	@CODE					[NVARCHAR] (10),
	@NAME					[NVARCHAR] (100),
	@DESCRIPTION			[NVARCHAR] (100),
	@RECOVERY_TYPE			[NVARCHAR] (10)
	
)    
AS    
BEGIN    
    
 UPDATE MNT_MASTER_VALUE    
 SET    
	LOSS_TYPE			 =@LOSS_TYPE	 ,
	TYPE_ID=              @TYPE_ID,
	CODE				 =@CODE,
	NAME				 =@NAME,
	DESCRIPTION			 =@DESCRIPTION,
	RECOVERY_TYPE		 =@RECOVERY_TYPE

 WHERE TYPE_UNIQUE_ID   = @TYPE_UNIQUE_ID    
END    

