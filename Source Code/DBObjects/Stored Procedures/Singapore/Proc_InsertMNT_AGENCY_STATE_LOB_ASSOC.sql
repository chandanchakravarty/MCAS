 /*----------------------------------------------------------                                
                                
Proc Name       : Proc_InsertMNT_AGENCY_STATE_LOB_ASSOC                
Created by      : Sumit Chhabra                
Date            : 09/01/2007                                
Purpose         : Insert of Loss Codes Type in MNT_AGENCY_STATE_LOB_ASSOC        
Revison History :                                
Used In                   : Wolverine                                
------------------------------------------------------------                                
Date     Review By          Comments                                
------   ------------       -------------------------*/                                
ALTER PROC dbo.Proc_InsertMNT_AGENCY_STATE_LOB_ASSOC                               
(                                
 @AGENCY_ID int,        
 @STATE_ID int,  
 @LOB_ID_TYPES varchar(100),        
 @CREATED_BY int,        
 @CREATED_DATETIME datetime        
)                                
AS                                
BEGIN                           
        
declare @MNT_AGENCY_STATE_LOB_ASSOC_ID int        
declare @DataExistedFlag int    
    
--Delete all data related to that particular LOB before inserting/updating new data        
 DELETE FROM MNT_AGENCY_STATE_LOB_ASSOC WHERE STATE_ID=@STATE_ID AND AGENCY_ID=@AGENCY_ID  
     
     
        
SELECT @MNT_AGENCY_STATE_LOB_ASSOC_ID=ISNULL(MAX(MNT_AGENCY_STATE_LOB_ASSOC_ID),0) + 1 FROM MNT_AGENCY_STATE_LOB_ASSOC        
 WHERE AGENCY_ID=@AGENCY_ID  
        
--if there is no data for insertion, return from the procedure        
if (@LOB_ID_TYPES is null) or (@LOB_ID_TYPES='')    
 return -1       
        
DECLARE @CURRENT_LOSS_ID_TYPE VARCHAR(10)        
DECLARE @COUNT INT        
SET @COUNT=2        
      
 SET @CURRENT_LOSS_ID_TYPE = DBO.PIECE(@LOB_ID_TYPES,',',1)                       
             
 --Run a loop to go through the list of comma-separated values for insertion        
while @CURRENT_LOSS_ID_TYPE is not null              
 BEGIN                      
  --Insert LossCodesType data      
--  IF NOT EXISTS(SELECT MNT_AGENCY_STATE_LOB_ASSOC_ID FROM MNT_AGENCY_STATE_LOB_ASSOC WHERE LOB_ID=@LOB_ID AND AGENCY_ID=@AGENCY_ID)    
    INSERT INTO MNT_AGENCY_STATE_LOB_ASSOC         
    (        
     MNT_AGENCY_STATE_LOB_ASSOC_ID,        
  AGENCY_ID,  
     LOB_ID,        
     STATE_ID,             
     IS_ACTIVE,        
     CREATED_BY,        
     CREATED_DATETIME        
    )        
    values        
    (        
     @MNT_AGENCY_STATE_LOB_ASSOC_ID,    
  @AGENCY_ID,      
     @CURRENT_LOSS_ID_TYPE,        
     @STATE_ID,        
     'Y',        
     @CREATED_BY,        
     @CREATED_DATETIME        
    )      
    --increment the MNT_AGENCY_STATE_LOB_ASSOC_ID           
      SET @CURRENT_LOSS_ID_TYPE=DBO.PIECE(@LOB_ID_TYPES,',',@COUNT)                
   SET @COUNT=@COUNT+1       
     SET @MNT_AGENCY_STATE_LOB_ASSOC_ID=@MNT_AGENCY_STATE_LOB_ASSOC_ID+1        
 END            
END              
      
    
  
  