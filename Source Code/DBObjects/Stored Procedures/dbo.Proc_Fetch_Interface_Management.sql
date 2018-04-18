IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Fetch_Interface_Management]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Fetch_Interface_Management]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------                              
Proc Name    : dbo.Proc_Fetch_Interface_Management                            
Created by   : Shubhanshu Pandey           
Date         : 27-4-2010                  
Purpose      : RETREIVING THE INFORMATON FROM TABLE PAGNET_EXPORT_FILES AND PAGNET_EXPORT_RECORD ON BASIS OF INPUT PARAMETERS.     
                              
 ----------------------------------------------------------                                          
Date     Review By          Comments                                        
 drop proc dbo.Proc_Fetch_Interface_Management '1',1,'04/30/2011','05/12/2011','Document rejected by Pagnet - returned to Ebix'                  
----   ------------       -------------------------*/                             
CREATE PROC [dbo].[Proc_Fetch_Interface_Management]    
(  
    @INTERFACE_FILE VARCHAR(20),  
    @FILE_TYPE INT, 
    @FROM_DATE datetime,  
    @TO_DATE datetime
    --,@STATUS VARCHAR(500)=''  
)  
AS   
BEGIN  
DECLARE @INTERFACE_TYPE INT

	   
IF(@FILE_TYPE = 2)
BEGIN

	SELECT PER.FILE_ID,   
    PEF.FILE_NAMES,   
    --CAST(DAY(PEF.CREATE_DATE) AS VARCHAR(2))+'-'+ CAST(MONTH(PEF.CREATE_DATE) AS VARCHAR(2))+'-'+ CAST(YEAR(PEF.CREATE_DATE) AS VARCHAR(4)) AS CREATE_DATE,  
    PEF.CREATE_DATE,  
    CASE   
   WHEN STATUS = 'Generated' THEN 'APR'   
   --WHEN STATUS = 'Imported by PagNet with success' THEN 'PRC/RET'  
   --WHEN STATUS = 'Imported by PagNet with error' THEN 'ERR'  
   --ELSE 'END'    
    END AS CURRENT_FILE_FOLDER,  
    CASE  
   WHEN  PEF.INTERFACE_CODE = 1 THEN 'Commission'  
   WHEN  PEF.INTERFACE_CODE = 2 THEN 'Claim Expense'    
   WHEN  PEF.INTERFACE_CODE = 3 THEN 'Claim Indemnity'   
   WHEN  PEF.INTERFACE_CODE = 4 THEN 'Customer Refund'   
   WHEN  PEF.INTERFACE_CODE = 5 THEN 'CO or RI'   
    END AS FILE_TYPE
    ,  
    CASE   
   WHEN  @FILE_TYPE =1  THEN 'Input'  
   WHEN  @FILE_TYPE =2  THEN 'Output'  
    END AS INPUT_OUTPUT,     
    COUNT(*) AS NUMBER_OF_RECORDS  
    
     
FROM PAGNET_EXPORT_FILES PEF WITH(NOLOCK)   
INNER JOIN PAGNET_EXPORT_RECORD PER WITH(NOLOCK) ON (PEF.ID = PER.FILE_ID)  
WHERE 

STATUS = 'Generated' AND
CAST(PEF.INTERFACE_CODE AS INT) = CAST(@INTERFACE_FILE AS INT) AND   
 PEF.CREATE_DATE between @FROM_DATE AND DATEADD(DD,1,@TO_DATE)  
 --AND  PER.RETURN_STATUS =@STATUS 
     
 --CONVERT(VARCHAR,PEF.CREATE_DATE) >= CONVERT(VARCHAR,@FROM_DATE) AND    
 --CONVERT(VARCHAR,PEF.CREATE_DATE) <= CONVERT(VARCHAR,@TO_DATE)   
GROUP BY   
    PER.FILE_ID,      
    PEF.FILE_NAMES,   
   -- CAST(DAY(PEF.CREATE_DATE) AS VARCHAR(2))+'-'+ CAST(MONTH(PEF.CREATE_DATE) AS VARCHAR(2))+'-'+ CAST(YEAR(PEF.CREATE_DATE) AS VARCHAR(4)),  
   PEF.CREATE_DATE,  
    STATUS,   
    PEF.INTERFACE_CODE  

END
ELSE IF(@FILE_TYPE = 1)
BEGIN
	SELECT PER.FILE_ID,   
    PEF.FILE_NAMES,   
    --CAST(DAY(PEF.CREATE_DATE) AS VARCHAR(2))+'-'+ CAST(MONTH(PEF.CREATE_DATE) AS VARCHAR(2))+'-'+ CAST(YEAR(PEF.CREATE_DATE) AS VARCHAR(4)) AS CREATE_DATE,  
    PEF.CREATE_DATE,  
    CASE   
   --WHEN STATUS = 'Generated' THEN 'APR'   
   WHEN STATUS = 'Imported by PagNet with success' THEN 'PRC/RET'  
   WHEN STATUS = 'Imported by PagNet with error' THEN 'ERR' 
   WHEN STATUS = 'Return Processed' THEN 'RET'
   WHEN STATUS = 'Ok' THEN 'END' 
   ELSE 'END'    
    END AS CURRENT_FILE_FOLDER,  
    CASE  
   WHEN  PEF.INTERFACE_CODE = 1 THEN 'Commission'  
   WHEN  PEF.INTERFACE_CODE = 2 THEN 'Claim Expense'    
   WHEN  PEF.INTERFACE_CODE = 3 THEN 'Claim Indemnity'   
   WHEN  PEF.INTERFACE_CODE = 4 THEN 'Customer Refund'   
   WHEN  PEF.INTERFACE_CODE = 5 THEN 'CO or RI'   
    END AS FILE_TYPE,  
    CASE   
   WHEN  @FILE_TYPE =1  THEN 'Input'  
   WHEN  @FILE_TYPE =2  THEN 'Output'  
    END AS INPUT_OUTPUT,     
    COUNT(*) AS NUMBER_OF_RECORDS  
    
     
FROM PAGNET_EXPORT_FILES PEF WITH(NOLOCK)   
INNER JOIN PAGNET_EXPORT_RECORD PER WITH(NOLOCK) ON (PEF.ID = PER.FILE_ID)  
WHERE 
STATUS <> 'Generated' AND
	CAST(PEF.INTERFACE_CODE AS INT) = CAST(@INTERFACE_FILE AS INT) AND   
 PEF.CREATE_DATE between @FROM_DATE AND DATEADD(DD,1,@TO_DATE)  
  --AND  PER.RETURN_STATUS =@STATUS    
 --CONVERT(VARCHAR,PEF.CREATE_DATE) >= CONVERT(VARCHAR,@FROM_DATE) AND    
 --CONVERT(VARCHAR,PEF.CREATE_DATE) <= CONVERT(VARCHAR,@TO_DATE)   
GROUP BY   
    PER.FILE_ID,      
    PEF.FILE_NAMES,   
   -- CAST(DAY(PEF.CREATE_DATE) AS VARCHAR(2))+'-'+ CAST(MONTH(PEF.CREATE_DATE) AS VARCHAR(2))+'-'+ CAST(YEAR(PEF.CREATE_DATE) AS VARCHAR(4)),  
   PEF.CREATE_DATE,  
    STATUS,   
    PEF.INTERFACE_CODE  
END
ELSE
PRINT ''	   
	   
	   
	   	 
END
GO

