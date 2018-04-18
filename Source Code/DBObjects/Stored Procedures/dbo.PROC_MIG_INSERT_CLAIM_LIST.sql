IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_INSERT_CLAIM_LIST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_INSERT_CLAIM_LIST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*----------------------------------------------------------                                                                  
Proc Name             : Dbo.PROC_MIG_INSERT_CLAIM_LIST                                                                  
Created by            : Santosh Kumar Gautam                                                                 
Date                  : 26 May 2011                                                                
Purpose               : TO CREATE NEW CLAIM AND UPDATE EXISTS CLAIM DETAILS 
Revison History       :                                                                  
Used In               : ACCEPETED COINSURANCE DATA INITIAL LOAD                     
------------------------------------------------------------                                                                  
Date     Review By          Comments                                     
                            
drop Proc PROC_MIG_INSERT_CLAIM_LIST    220                                                  
------   ------------       -------------------------*/                                                                  
--                                     
                                      
--                                   
                                
CREATE PROCEDURE [dbo].[PROC_MIG_INSERT_CLAIM_LIST]          
@IMPORT_REQUEST_ID    INT        
AS                                      
BEGIN                               
          
 SET NOCOUNT ON;    
      
  BEGIN TRY        
           
             
    DECLARE @COUNTER					INT =1        
    DECLARE @TOTAL_RECORD_COUNT         INT =0        
    DECLARE @LEADER_CLAIM_NUMBER        NVARCHAR(20) =0       
    DECLARE @MOVEMENT_TYPE		        INT =0       
    DECLARE @HAS_ERROR                  INT =0  
            
     CREATE TABLE #TEMP_CLAIM_LIST        
     (        
		ID					INT  IDENTITY,            
		LEADER_CLAIM_NUMBER NVARCHAR(20)  ,
		MOVEMENT_TYPE       INT
     )        
            
            
    INSERT INTO #TEMP_CLAIM_LIST        
     (                  
       LEADER_CLAIM_NUMBER ,
       MOVEMENT_TYPE
     )        
     (        
      SELECT LEADER_CLAIM_NUMBER,MOVEMENT_TYPE
      FROM   MIG_CLAIM_DETAILS        
      WHERE  IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND HAS_ERRORS='N' AND IS_DELETED='N'     
      GROUP BY LEADER_CLAIM_NUMBER,MOVEMENT_TYPE       
     )                  
             
             
             
     SELECT @TOTAL_RECORD_COUNT =COUNT(ID) FROM #TEMP_CLAIM_LIST        
        
   
                
      
     ---------------------------------------        
     --- LOOP FOR INSERT CLAIM DETAILS      
     ---------------------------------------        
     WHILE(@COUNTER <=@TOTAL_RECORD_COUNT)        
     BEGIN        
             
         
                
        SELECT @LEADER_CLAIM_NUMBER = LEADER_CLAIM_NUMBER  ,
			   @MOVEMENT_TYPE		= MOVEMENT_TYPE
        FROM   #TEMP_CLAIM_LIST        
        WHERE  ID =@COUNTER        
       
           
        --------------------------------------      
		--- VALIDATE CLAIM DATA
		--------------------------------------      
		EXEC PROC_MIG_VALIDATE_CLAIM_DATA      
		@IMPORT_REQUEST_ID         = @IMPORT_REQUEST_ID, 
		@LEADER_CLAIM_NUMBER       = @LEADER_CLAIM_NUMBER,      
		@MOVEMENT_TYPE			   = @MOVEMENT_TYPE  ,    
		@HAS_ERRORS                = @HAS_ERROR OUT    
		
		 
       
            
        ------------------------------------------------        
		--- @MOVEMENT_TYPE 1 : INSERT CLAIM DETAILS    
		--- @MOVEMENT_TYPE 2 : UPDATE CLAIM DETAILS 
		------------------------------------------------
        IF(@MOVEMENT_TYPE=1 )
         BEGIN
            
            IF(@HAS_ERROR =0)
            BEGIN
				--------------------------------------        
				--- INSERT CLAIM DETAILS 
				-------------------------------------- 
				EXEC PROC_MIG_INSERT_CLAIM_DETAIL 
					 @IMPORT_REQUEST_ID    = @IMPORT_REQUEST_ID,
					 @LEADER_CLAIM_NUMBER  = @LEADER_CLAIM_NUMBER
			END
		 END
		 ELSE  
		 BEGIN
		    --------------------------------------        
			--- UPDATE CLAIM DETAILS 
			-------------------------------------- 
		    EXEC PROC_MIG_UPDATE_CLAIM_DETAIL
				 @IMPORT_REQUEST_ID    = @IMPORT_REQUEST_ID,  
				 @LEADER_CLAIM_NUMBER  = @LEADER_CLAIM_NUMBER
		 END
			 
         
        SET @COUNTER+=1        
                
          
     END        
             
     DROP TABLE #TEMP_CLAIM_LIST        
             
     END TRY        
   BEGIN CATCH        
           
           
   END CATCH        
             
             
             
END 








GO

