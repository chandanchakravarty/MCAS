IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_INSERT_IMPORT_ERROR_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_INSERT_IMPORT_ERROR_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                  
Proc Name             : Dbo.PROC_MIG_INSERT_IMPORT_ERROR_DETAILS                                                 
Created by            : Santosh Kumar Gautam                                                 
Date                  : 04 May 2011                                                
Purpose               : TO INSERT ERROR DETAILS 
Revison History       :                                                  
Used In               : ACCEPETED COINSURANCE DATA INITIAL LOAD     
------------------------------------------------------------                                                  
Date     Review By          Comments                     
            
drop Proc PROC_MIG_INSERT_IMPORT_ERROR_DETAILS                                         
------   ------------       -------------------------*/                                                  
--                     
                      
--                   
                
CREATE PROCEDURE [dbo].[PROC_MIG_INSERT_IMPORT_ERROR_DETAILS]      
            
	@IMPORT_REQUEST_ID        INT  ,  
	@IMPORT_SERIAL_NO         [INT] ,      
	@ERROR_SOURCE_FILE        [nvarchar](512) ,  
	@ERROR_SOURCE_COLUMN      [nvarchar](4000) ,  
	@ERROR_SOURCE_COLUMN_VALUE [nvarchar](MAX),  
	@ERROR_ROW_NUMBER         INT  ,   
	@ERROR_TYPES              [nvarchar](4000) ,  
	@ACTUAL_RECORD_DATA       [nvarchar](4000) ,
	@ERROR_MODE               CHAR(4)='SE',  -- DEFAULT FROM SSIS PACKAGE ERROR 
	@ERROR_SOURCE_TYPE        CHAR(4) 
	
 --=======================================================================
 --  1 SORCE TYPE MEANS FOR WICH FILE TYPE THIS ERROR IS RAISED	                               
 --     * APP  - ISSUANCE FILE (APPLICATION/POLICY DATA)  
 --     * COV  - COVERAGE FILE (COVERAGE DATA)                 	                               
 --     * ISTC - INSTALMENT CANCELLATION FILE (INSTALMENT CANCELLATION DATA)                 	                               
 --     * CLM  - FNOL FILE (CLAIM DATA)   
 --     * CLMP - CLAIM PAID FILE (CLAIM PAYMENT DATA)  
 -------------------------------------------------------------------------
 -- 2 ERROR MODE
 --     * SE   - ERROR FOUND BY SSIS (INVALID DATA ERROR)               	                               
 --     * VE   - ERROR FOUND BY SP (VALIDATION ERROR)
 --=======================================================================

AS                  
BEGIN               
          
         -- DECLARE @IMPORT_ERROR_ID INT =0  
           
          
         --- SELECT @IMPORT_ERROR_ID=(ISNULL(MAX([IMPORT_ERROR_ID]),0)+1)  FROM [dbo].[MIG_IMPORT_ERROR_DETAILS]  WITH(NOLOCK)                    
  
            
            INSERT INTO [MIG_IMPORT_ERROR_DETAILS]  
            (  
				 --[IMPORT_ERROR_ID]           
				[IMPORT_REQUEST_ID]       
				,[IMPORT_SERIAL_NO]            
				,[ERROR_DATETIME]       
				,[ERROR_SOURCE_FILE]       
				,[ERROR_SOURCE_COLUMN]     
				,[ERROR_SOURCE_COLUMN_VALUE]  
				,[ERROR_ROW_NUMBER]        
				,[ERROR_TYPES]             
				,[ACTUAL_RECORD_DATA]  
				,[ERROR_MODE]
				,[ERROR_SOURCE_TYPE]
            )  
            VALUES  
            (  
               --  @IMPORT_ERROR_ID  
				 @IMPORT_REQUEST_ID       
				,@IMPORT_SERIAL_NO            
				,GETDATE()    
				,@ERROR_SOURCE_FILE     
				,@ERROR_SOURCE_COLUMN   
				,@ERROR_SOURCE_COLUMN_VALUE    
				,@ERROR_ROW_NUMBER        
				,@ERROR_TYPES             
				,@ACTUAL_RECORD_DATA  
				,@ERROR_MODE 
				,@ERROR_SOURCE_TYPE   
                 
            )  
         
END                      
    



GO

