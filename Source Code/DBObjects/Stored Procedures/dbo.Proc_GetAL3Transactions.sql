IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAL3Transactions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAL3Transactions]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*                                                                    
----------------------------------------------------------                                          
Proc Name       : dbo.Proc_GetAL3Transactions                                                                
Created by      : Mohit Agarwal                                                                   
Date            : 03-11-2006                                
                                      
Purpose         : Selects records for Customer report XML                                         
Revison History :       
Modified By :       Pravesh K. Chandel
Modified Date :       28 nov 2008
Purpose  :        add search on Policy numbers 
                                                              
Modified By :       Pravesh K. Chandel
Modified Date :       4 dec 2008
Purpose  :          Modify Created Date Time (Transaction Effective_date) to process Effective Date


Used In         : Wolverine                    
      
------------------------------------------------------------                                        
                                       
Date     Review By          Comments                                                                
               
------   ------------       -------------------------                                                                        
*/      
-- drop proc dbo.Proc_GetAL3Transactions  
      
CREATE PROC [dbo].[Proc_GetAL3Transactions]      
(      
@LOB_ID NVARCHAR(4000),      
@AGENCY_ID NVARCHAR(4000),      
@PROCESS_ID NVARCHAR(4000),      
@FROM_DATE NVARCHAR(100),      
@TO_DATE NVARCHAR(100) ,
@POLICY_NUMBERS nvarchar(4000) = '',
@TRANS_FUNCTION nvarchar(10) = ''
)      
AS      
BEGIN      
      
DECLARE @QUERY AS NVARCHAR(4000)      
DECLARE @PREMIUM AS INT    


      
SET @QUERY =      
'SELECT       
 POL_CUS.AGENCY_ID AS AGENCY_ID, AGL.AGENCY_CODE AS AGENCY_CODE, AGL.NUM_AGENCY_CODE AS NUM_AGENCY_CODE,     
 AGL.AGENCY_DISPLAY_NAME AS AGENCY_NAME, POL_POL.PROCESS_ID AS PROCESS_ID,       
 POL_PROC.PROCESS_CODE AS PROCESS_CODE, CONVERT(VARCHAR(20),POL_POL.EFFECTIVE_DATETIME,101) AS CREATED_DATETIME,      
 POL_POL.CUSTOMER_ID AS CUSTOMER_ID, POL_CUS.CURRENT_TERM As CURRENT_TERM,      
 POL_POL.POLICY_ID AS POLICY_ID, POL_CUS.POLICY_VERSION_ID AS POLICY_VERSION_ID,      
 POL_CUS.POLICY_LOB AS LOB_ID, ISNULL(MLM.LOB_DESC,'''') AS LOB_DESC,
 ISNULL(MSLM.SUB_LOB_DESC,'''') AS SUB_LOB_DESC'

/* code changed by aarora. now we are not using Transaction_premium section anymore 
 , ISNULL(CPS.RISK_ID,0) AS RISK_ID,
 ISNULL(CPSD.COMPONENT_CODE,'''') AS COMPONENT_CODE, ISNULL(CPSD.PREMIUM,0) AS PREMIUM  ,
 code change ends
*/
SET @QUERY = @QUERY + ', ISNULL(MNTL.LOOKUP_VALUE_DESC,'' '') AS CANCELLATION_TYPE,
 ISNULL(POL_POL.CANCELLATION_TYPE,'' '') AS CANCELLATION_TYPE_CODE,
 ISNULL(MNTL1.LOOKUP_VALUE_DESC,'' '') AS CANCELLATION_OPTION,
 ISNULL(POL_POL.CANCELLATION_OPTION,'' '') AS CANCELLATION_OPTION_CODE,
 ISNULL(MNTL2.LOOKUP_VALUE_DESC,'' '') AS REASON, 
 ISNULL(POL_POL.REASON,'' '') AS REASON_CODE '



SET @QUERY = @QUERY + ' 
FROM       
 POL_POLICY_PROCESS POL_POL      
      
 INNER JOIN POL_CUSTOMER_POLICY_LIST POL_CUS      
 ON POL_POL.CUSTOMER_ID = POL_CUS.CUSTOMER_ID      
 AND POL_POL.POLICY_ID = POL_CUS.POLICY_ID      
 AND POL_POL.NEW_POLICY_VERSION_ID = POL_CUS.POLICY_VERSION_ID 
 AND POL_POL.PROCESS_STATUS = ''COMPLETE'' ' 
 
 IF (@TRANS_FUNCTION = 'SYNC')
 	SET @QUERY = @QUERY + ' AND POL_CUS.POLICY_STATUS = ''NORMAL''  ' 
 
 SET @QUERY = @QUERY + '

 INNER JOIN POL_PROCESS_MASTER POL_PROC      
 ON POL_POL.PROCESS_ID = POL_PROC.PROCESS_ID      
      
 INNER JOIN MNT_AGENCY_LIST AGL       
 ON AGL.AGENCY_ID = POL_CUS.AGENCY_ID      
    
 LEFT OUTER JOIN MNT_LOB_MASTER MLM     
 ON POL_CUS.POLICY_LOB = MLM.LOB_ID      
    
 LEFT OUTER JOIN MNT_SUB_LOB_MASTER MSLM     
 ON POL_CUS.POLICY_LOB = MSLM.LOB_ID      '
 
/* code changed by aarora. now we are not using Transaction_premium section anymore 
  LEFT OUTER JOIN CLT_PREMIUM_SPLIT CPS    
 ON POL_POL.CUSTOMER_ID = CPS.CUSTOMER_ID      
 AND POL_POL.POLICY_ID = CPS.POLICY_ID      
 AND POL_POL.NEW_POLICY_VERSION_ID = CPS.POLICY_VERSION_ID     
    
 LEFT OUTER JOIN CLT_PREMIUM_SPLIT_DETAILS CPSD     
 ON CPS.UNIQUE_ID = CPSD.SPLIT_UNIQUE_ID     
  code change ends
*/

SET @QUERY = @QUERY + ' LEFT OUTER JOIN MNT_LOOKUP_VALUES MNTL     
 ON MNTL.LOOKUP_UNIQUE_ID =  POL_POL.CANCELLATION_TYPE
 
LEFT OUTER JOIN MNT_LOOKUP_VALUES MNTL1     
 ON MNTL1.LOOKUP_UNIQUE_ID =  POL_POL.CANCELLATION_OPTION  

LEFT OUTER JOIN MNT_LOOKUP_VALUES MNTL2     
 ON MNTL2.LOOKUP_UNIQUE_ID =  POL_POL.REASON  '


SET @QUERY = @QUERY + '      
      
WHERE      
POL_POL.PROCESS_ID IN (' + @PROCESS_ID + ') AND POL_POL.COMPLETED_DATETIME >=''' + @FROM_DATE + '''       
AND POL_POL.COMPLETED_DATETIME <= ''' + @TO_DATE + '''      
AND POL_CUS.POLICY_LOB IN (''' + @LOB_ID + ''') ' 

IF (@AGENCY_ID = 0) 
	SET @QUERY = @QUERY +  ' AND POL_CUS.AGENCY_ID IN (SELECT AGENCY_ID FROM MNT_AGENCY_LIST)  '
ELSE
	SET @QUERY = @QUERY +  ' AND POL_CUS.AGENCY_ID IN (' + @AGENCY_ID +') '

IF (@POLICY_NUMBERS!='')
	SET @QUERY = @QUERY + ' AND POL_CUS.POLICY_NUMBER IN (''' + @POLICY_NUMBERS + ''') ' 
 
SET @QUERY = @QUERY + ' ORDER BY AGENCY_ID, CUSTOMER_ID, PROCESS_ID, LOB_ID'      
      
--select (@QUERY)      
EXEC (@QUERY)      
      
      
END      



GO

