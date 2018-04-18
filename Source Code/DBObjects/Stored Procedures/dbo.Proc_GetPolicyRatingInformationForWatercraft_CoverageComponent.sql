IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyRatingInformationForWatercraft_CoverageComponent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyRatingInformationForWatercraft_CoverageComponent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




--Proc_GetPolicyRatingInformationForWatercraft_CoverageComponent            
/* --------------------------------------------------------------------------------------------------                                                                                            
Proc Name                : Dbo.Proc_GetPolicyRatingInformationForWatercraft_CoverageComponent                                                                            
Created by               : shafi                                                                                          
Date                     : 01 mar          2006        
Purpose                  : To get the coverages for Watercraft                                                                                          
Revison History          :                                                                                            
Used In                  : Wolverine                                                                                            
----------------------------------------------------------------------------------------------------                                                                                            
Date     Review By          Comments                                                                                            
------   ------------       ------------------------------------------------------------------------*/                                                                                            
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
              
CREATE PROCEDURE dbo.Proc_GetPolicyRatingInformationForWatercraft_CoverageComponent                      
(                      
@CUSTOMER_ID   INT,                      
@POLICY_ID        INT,                      
@POLICY_VERSION_ID INT,                      
@BOAT_ID       INT                      
)                      
AS                      
BEGIN  --MAIN                       
SET QUOTED_IDENTIFIER OFF                       
                      
DECLARE @ReturnValue VARCHAR(8000)                      
DECLARE @LobID INT                       
DECLARE @StateID INT                       
DECLARE @BOATTYPE VARCHAR(100)  
DECLARE  @C  VARCHAR(1000)                  
                    
set @ReturnValue=''                    
                      
Select @LobID=4, @StateID=State_ID from POL_CUSTOMER_POLICY_LIST WITH (NOLOCK)                       
WHERE CUSTOMER_ID =@CUSTOMER_ID and POLICY_ID =@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                      
                      
                    
select @BOATTYPE=TYPE_OF_WATERCRAFT from POL_WATERCRAFT_INFO  WITH (NOLOCK)                  
WHERE CUSTOMER_ID =@CUSTOMER_ID and POLICY_ID =@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID  and BOAT_ID=@BOAT_ID                    
                      
                     
                   
                      
                  
               
 SET @RETURNVALUE=''               
SELECT @RETURNVALUE =  (CASE COV_CODE               
              
WHEN 'EBSMECE'  THEN CASE ISNULL(POL_WATERCRAFT_COVERAGE_INFO.COVERAGE_CODE_ID,0)
                           WHEN 0 THEN  '<OP720>N</OP720>'
                           ELSE  '<OP720>Y</OP720>' END                                 
WHEN 'EBSMWL'  THEN   CASE ISNULL(POL_WATERCRAFT_COVERAGE_INFO.COVERAGE_CODE_ID,0)
                           WHEN 0 THEN  '<OP900>N</OP900>'
                           ELSE  '<OP900>Y</OP900>'  END          
WHEN 'EBSCEAV'  THEN  CASE ISNULL(POL_WATERCRAFT_COVERAGE_INFO.COVERAGE_CODE_ID,0)
                           WHEN 0 THEN  '<AV100>N</AV100>'
                           ELSE  '<AV100>Y</AV100>'  END                              
 END ) + ISNULL(@RETURNVALUE,'') 
  FROM  MNT_COVERAGE   WITH (NOLOCK) LEFT OUTER JOIN   POL_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) ON                   
  POL_WATERCRAFT_COVERAGE_INFO.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID  AND                      
  POL_WATERCRAFT_COVERAGE_INFO.CUSTOMER_ID = @CUSTOMER_ID  AND                       
  POL_WATERCRAFT_COVERAGE_INFO.POLICY_ID  = @POLICY_ID   AND                      
  POL_WATERCRAFT_COVERAGE_INFO.POLICY_VERSION_ID  = @POLICY_VERSION_ID AND                        
  POL_WATERCRAFT_COVERAGE_INFO.BOAT_ID  = @BOAT_ID                      
  WHERE                             
  MNT_COVERAGE.STATE_ID  = @STATEID  AND                         
  MNT_COVERAGE.LOB_ID = @LOBID  
  AND COV_CODE IN ('EBSMECE','EBSMWL','EBSCEAV')                        
                     
                   
                   
  
              
                  

--GET THE DEDUCTIBLE
                  
SELECT    @C =                                 
(CASE COV_CODE                                 
    /*           
WHEN 'EBPPDAV'  THEN  CASE ISNULL(POL_WATERCRAFT_COVERAGE_INFO.COVERAGE_CODE_ID,0)
                           WHEN 0 THEN  ''
                           ELSE  CAST(ISNULL(POL_WATERCRAFT_COVERAGE_INFO.DEDUCTIBLE_1, 0) AS VARCHAR)+         
                                 CAST(ISNULL(POL_WATERCRAFT_COVERAGE_INFO.DEDUCTIBLE1_AMOUNT_TEXT, '') AS VARCHAR) END                                 
WHEN 'EBPPDACV'  THEN   CASE ISNULL(POL_WATERCRAFT_COVERAGE_INFO.COVERAGE_CODE_ID,0)
                           WHEN 0 THEN  ''
                           ELSE  CAST(ISNULL(POL_WATERCRAFT_COVERAGE_INFO.DEDUCTIBLE_1, 0) AS VARCHAR)+         
                                 CAST(ISNULL(POL_WATERCRAFT_COVERAGE_INFO.DEDUCTIBLE1_AMOUNT_TEXT, '') AS VARCHAR) END          
WHEN 'EBPPDJ'  THEN  CASE ISNULL(POL_WATERCRAFT_COVERAGE_INFO.COVERAGE_CODE_ID,0)
                           WHEN 0 THEN  ''
                           ELSE  CAST(ISNULL(POL_WATERCRAFT_COVERAGE_INFO.DEDUCTIBLE_1, 0) AS VARCHAR)+         
                                 CAST(ISNULL(POL_WATERCRAFT_COVERAGE_INFO.DEDUCTIBLE1_AMOUNT_TEXT, '') AS VARCHAR) END                              */
WHEN 'BDEDUC'  THEN  CASE ISNULL(POL_WATERCRAFT_COVERAGE_INFO.COVERAGE_CODE_ID,0)  
                           WHEN 0 THEN  ''  
                           ELSE  CAST(ISNULL(POL_WATERCRAFT_COVERAGE_INFO.DEDUCTIBLE_1, 0) AS VARCHAR)+           
                                 CAST(ISNULL(POL_WATERCRAFT_COVERAGE_INFO.DEDUCTIBLE1_AMOUNT_TEXT, '') AS VARCHAR) END                                
 
 END ) + isnull(@C,'')          
           
		FROM MNT_COVERAGE  WITH (NOLOCK) LEFT OUTER JOIN      POL_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) ON                     
		POL_WATERCRAFT_COVERAGE_INFO.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID  AND                      
		POL_WATERCRAFT_COVERAGE_INFO.CUSTOMER_ID = @CUSTOMER_ID  AND                       
		POL_WATERCRAFT_COVERAGE_INFO.POLICY_ID  = @POLICY_ID   AND                      
		POL_WATERCRAFT_COVERAGE_INFO.POLICY_VERSION_ID  = @POLICY_VERSION_ID AND                        
		POL_WATERCRAFT_COVERAGE_INFO.BOAT_ID  = @BOAT_ID                      
		WHERE                             
		MNT_COVERAGE.STATE_ID  = @STATEID  AND                         
		MNT_COVERAGE.LOB_ID = @LOBID   
		AND COV_CODE = 'BDEDUC' --IN ('EBPPDAV','EBPPDACV','EBPPDJ','BDEDUC')                      



 --Change the format from 100-1% to 1%-100


	DECLARE @DA VARCHAR(200)
	SET @DA=CASE CHARINDEX('-', @C)
	
	WHEN 0 THEN
	'<DEDUCTIBLE>' + @C +  '</DEDUCTIBLE>'
	ELSE
	'<DEDUCTIBLE>' +  SUBSTRING(@C ,CHARINDEX('-', @C) +1,2) + '-' +  SUBSTRING(@C ,0,CHARINDEX('-', @C)) + '</DEDUCTIBLE>'
	END

                  
        
------  Deductible  END             
------------------------------------------------------------------------------------------------            
             
            
            
   SET @C = ISNULL(@RETURNVALUE,'') + ISNULL(@DA,'')
   SELECT  @C AS  COVERAGES                 
END   ----End of main Begin                   
                
                
              
            
          
        
        
        
      
    
  





GO

