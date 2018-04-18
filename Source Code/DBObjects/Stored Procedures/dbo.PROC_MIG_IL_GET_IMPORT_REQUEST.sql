
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_GET_IMPORT_REQUEST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_GET_IMPORT_REQUEST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
 /*----------------------------------------------------------                                                            
Proc Name             : Dbo.[PROC_MIG_IL_GET_IMPORT_REQUEST]                                                            
Created by            : Santosh Kumar Gautam                                                           
Date                  : 16 Aug 2011                                                          
Purpose               : TO GET IMPORT REQUEST LIST DETAILS            
Revison History       :                                                            
Used In               : INITIAL LOAD               
------------------------------------------------------------                                                            
Date     Review By          Comments                               
                      
drop Proc [PROC_MIG_IL_GET_IMPORT_REQUEST]                                                   
------   ------------       -------------------------*/                                                            
--                               
                                
--                             
                          
CREATE PROCEDURE [dbo].[PROC_MIG_IL_GET_IMPORT_REQUEST]                         
                               
AS                                
BEGIN        

  -- 14938 Contact
  -- 14937 Coapplicant
  -- 14936 Customer                 
                   
   DECLARE @IMPORT_STATUS VARCHAR(8)          
           
   -- IF ALREADY ETL IS RUNNING THEN REURN NULL        
   IF EXISTS(SELECT IMPORT_REQUEST_ID FROM MIG_IL_IMPORT_REQUEST WHERE REQUEST_STATUS='INPRG' AND IS_ACTIVE='Y')     
     BEGIN      
          SELECT 0 AS IMPORT_REQUEST_ID,'' AS CUSTOMER_FILE,'' AS COAPP_FILE,'' AS CONTACT_FILE,  
          0 AS SUBMITTED_BY  
                 
        RETURN       
     END  
           
   IF EXISTS            
   (           
	   SELECT M.[IMPORT_REQUEST_ID]        
	   FROM   MIG_IL_IMPORT_REQUEST M WITH(NOLOCK) LEFT OUTER JOIN            
		[MIG_IL_IMPORT_REQUEST_FILES]  MF WITH(NOLOCK) ON M.[IMPORT_REQUEST_ID]=MF.[IMPORT_REQUEST_ID]              
	   WHERE  REQUEST_STATUS='INQUEUE' AND [IS_PROCESSED]='N' AND [HAS_ERRORS]  ='N' AND [IS_ACTIVE]   ='Y'            
    )            
   BEGIN   
   

     -- SELECT ALL RECORDS   
     SELECT IMPORT_REQUEST_ID,ISNULL([14936],'') AS CUSTOMER_FILE,ISNULL([14937],'') AS COAPP_FILE,
                              ISNULL([14938],'') AS CONTACT_FILE ,SUBMITTED_BY 
                FROM 


  
	 (SELECT * FROM  
	 ( SELECT A.IMPORT_REQUEST_ID,IMPORT_FILE_PATH,IMPORT_FILE_TYPE ,A.SUBMITTED_BY  
	   FROM MIG_IL_IMPORT_REQUEST A LEFT OUTER JOIN   
		[MIG_IL_IMPORT_REQUEST_FILES] B on A.IMPORT_REQUEST_ID=B.IMPORT_REQUEST_ID  
	   WHERE  REQUEST_STATUS='INQUEUE' AND      
			 [IS_PROCESSED]='N' AND                  
			 [HAS_ERRORS]  ='N' AND                
			 [IS_ACTIVE]   ='Y'    
		) S  
	   PIVOT   
		(   
		   MAX(IMPORT_FILE_PATH)  
		   FOR IMPORT_FILE_TYPE IN ([14936],[14937],[14938])   
		) p  
	   ) TABLE1             
    END            
 ELSE    -- IF NO FILES EXISTS              
   SELECT 0 AS IMPORT_REQUEST_ID,'' AS CUSTOMER_FILE,'' AS COAPP_FILE,'' AS CONTACT_FILE,  
          0 AS SUBMITTED_BY      
         
                   
                        
                   
END 



