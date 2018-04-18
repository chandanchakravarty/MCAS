IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMNT_AGENCY_STATE_LOB_ASSOC]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMNT_AGENCY_STATE_LOB_ASSOC]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                        
Proc Name        :   dbo.Proc_GetMNT_AGENCY_STATE_LOB_ASSOC                                         
Created by         : Sumit Chhabra                                        
Date                :01/09/2006                                        
Purpose           :  Get the table informatoin in the form of xml data                                        
Revison History  :                                        
Used In             :    
Modified By         :     
Modified On         :     
Purpose             :     
----------------------------------------------------------                                        
Date     Review By          Comments                
DROP PROC dbo.Proc_GetMNT_AGENCY_STATE_LOB_ASSOC      
----   ------------       -------------------------*/                                        
CREATE PROC [dbo].[Proc_GetMNT_AGENCY_STATE_LOB_ASSOC]                                        
(                                        
@AGENCY_ID smallint,  
@STATE_ID smallint  
)                                        
AS                                        
BEGIN        


if exists(select MNT_AGENCY_STATE_LOB_ASSOC_ID from MNT_AGENCY_STATE_LOB_ASSOC where agency_id=@agency_id and state_id=@state_id)                                
	 SELECT MLM.LOB_ID,MLM.LOB_DESC FROM MNT_LOB_STATE MLS  
		 INNER JOIN MNT_LOB_MASTER MLM ON MLS.LOB_ID=MLM.LOB_ID   
		 WHERE MLS.PARENT_LOB IS NULL AND STATE_ID=@STATE_ID and 
				MLM.LOB_ID not in  (select lob_id from MNT_AGENCY_STATE_LOB_ASSOC where agency_id=@agency_id and state_id=@state_id)
		 ORDER BY LOB_DESC  
else
	 SELECT MLM.LOB_ID,MLM.LOB_DESC FROM MNT_LOB_STATE MLS  
		 INNER JOIN MNT_LOB_MASTER MLM ON MLS.LOB_ID=MLM.LOB_ID   
		 WHERE MLS.PARENT_LOB IS NULL AND STATE_ID=@STATE_ID 
				 and 
				MLM.LOB_ID not in  (select lob_id from MNT_AGENCY_STATE_LOB_ASSOC where agency_id=@agency_id and state_id=@state_id)
		ORDER BY LOB_DESC  



 SELECT     
	 MNT_AGENCY_STATE_LOB_ASSOC_ID,    
	 AGENCY_ID,    
	 STATE_ID,    
	 MLM.LOB_DESC AS LOB_DESC,
	 MASL.LOB_ID,    
	 MASL.CREATED_DATETIME,    
	 MASL.MODIFIED_BY,    
	 MASL.LAST_UPDATED_DATETIME,    
	 MASL.IS_ACTIVE    
 FROM    
	 MNT_AGENCY_STATE_LOB_ASSOC MASL 
 LEFT OUTER JOIN    
	MNT_LOB_MASTER MLM
 ON
	MASL.LOB_ID = MLM.LOB_ID
 WHERE    
	 AGENCY_ID = @AGENCY_ID  and  
	 STATE_ID = @STATE_ID  
 ORDER BY 
	 MLM.LOB_DESC  
END 

GO

