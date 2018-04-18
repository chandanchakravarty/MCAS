IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMNT_VIOLATIONS_QQ]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMNT_VIOLATIONS_QQ]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                
Proc Name        :          Proc_GetMNT_VIOLATIONS_QQ                                                 
Created by       :          Praveen Kasana                                               
Date             :          05/03/2006                                                
Purpose          :          Get the violation_types  information for mnt_violations In case called from QQ                                    
Revison History  :                                                
Used In          :           Wolverine                                                
------------------------------------------------------------                                                
Date     Review By          Comments                                                
------   ------------       -------------------------*/            
--drop proc     DBO.Proc_GetMNT_VIOLATIONS_QQ  
CREATE PROC DBO.Proc_GetMNT_VIOLATIONS_QQ                                     
(                                                       
@VIOLATION_ID INT,                  
@STATE_NAME  varchar(20),                             
@APP_LOBCODE varchar(10)                
                            
)                                               
AS                                                
BEGIN                   
                
--Get StateName                              
Declare @STATE_ID int                              
select @STATE_ID=STATE_ID from MNT_COUNTRY_STATE_LIST                              
where STATE_NAME=@STATE_NAME                      
                
--Get LOB ID from LOBCODE                   
Declare @APP_LOB int                                  
                  
SELECT 
	@APP_LOB=LOB_ID        
FROM 
	MNT_LOB_MASTER 
WHERE LOB_CODE=@APP_LOBCODE        


                        
	SELECT VIOLATION_ID,MVR_POINTS,CAST(isnull(SD_POINTS,'0') AS VARCHAR) AS WOLVERINE_VIOLATIONS ,VIOLATION_CODE ,     
	 (isnull(VIOLATION_DES,'') + ' (' + CAST(isnull(MVR_POINTS,'') AS VARCHAR) +  '/' +  CAST(isnull(SD_POINTS,'') AS VARCHAR)  +  ')' )AS VIOLATION_DES                                    
	              
	FROM  MNT_VIOLATIONS         
	           
	WHERE         
	 STATE=@STATE_ID AND LOB=@APP_LOB         
	 AND VIOLATION_PARENT=(SELECT VIOLATION_GROUP FROM MNT_VIOLATIONS WHERE VIOLATION_ID=@VIOLATION_ID)                            
	         
	ORDER BY VIOLATION_DES FOR XML AUTO;  

      
END                         
        
      
                            
                              
                    
                
               
            
          
        
      
    
  





GO

