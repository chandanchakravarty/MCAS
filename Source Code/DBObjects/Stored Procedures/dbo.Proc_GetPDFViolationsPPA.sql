IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPDFViolationsPPA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPDFViolationsPPA]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------        
Proc Name          : Dbo.Proc_GetPDFViolationsPPA        
Created by          : Mohit Agarwal        
Date                : 4-Apr- 2007         
Purpose             : Returns the Violations of a application or policy 
		      from mvr and prior loss tables        
Revison History :        
Used In             :   Wolverine     
modified by	    :Pravesh   K. Chandel
dated 		   :14 April 2007
        
------------------------------------------------------------        
        
Date     Review By          Comments        
--drop PROCEDURE Proc_GetPDFViolationsPPA      
------   ------------       -------------------------*/        
CREATE  PROCEDURE dbo.Proc_GetPDFViolationsPPA        
(        
 @CUSTOMERID   int,        
 @POLID                int,        
 @VERSIONID   int,        
 @CALLEDFROM  VARCHAR(20)        
)        
AS        
BEGIN        
 DECLARE @POL_EFF_DATE DateTime  
 DECLARE @VIOL_COUNT_TEMP INT  
 DECLARE @VIOL_COUNT INT  
 SET @VIOL_COUNT = 0  
 IF (@CALLEDFROM='APPLICATION')        
 BEGIN       
  
   SELECT @POL_EFF_DATE=APP_EFFECTIVE_DATE FROM APP_LIST  with(nolock) WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@POLID AND APP_VERSION_ID=@VERSIONID  
   IF @POL_EFF_DATE IS NULL  
      SET @POL_EFF_DATE = GETDATE()  
  
   SELECT  DRIVER_ID, MVR_DATE, MVR_AMOUNT, MVR_DEATH, MLV.VIOLATION_DES AS VIOLATION_DESC        
   FROM  APP_MVR_INFORMATION AMI  with(nolock)          
   LEFT OUTER JOIN MNT_VIOLATIONS MLV ON AMI.VIOLATION_ID = MLV.VIOLATION_ID      
   WHERE AMI.CUSTOMER_ID=@CUSTOMERID AND AMI.APP_ID=@POLID AND AMI.APP_VERSION_ID=@VERSIONID AND AMI.IS_ACTIVE='Y'  
   AND DATEDIFF(year, MVR_DATE, @POL_EFF_DATE) < 3        
   UNION      
      
   SELECT DRIVER_ID, OCCURENCE_DATE AS MVR_DATE, AMOUNT_PAID AS MVR_AMOUNT,'N' AS MVR_DEATH, MLV.LOOKUP_VALUE_DESC AS VIOLATION_DESC      
   FROM APP_PRIOR_LOSS_INFO   with(nolock)      
   LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV  with(nolock) ON LOSS_TYPE = MLV.LOOKUP_UNIQUE_ID      
   WHERE CUSTOMER_ID = @CUSTOMERID AND LOB = '2' AND DATEDIFF(year, OCCURENCE_DATE, @POL_EFF_DATE) < 3   
       
   ORDER BY MVR_DATE   
  
   SELECT @VIOL_COUNT_TEMP = COUNT(*) FROM APP_MVR_INFORMATION AMI   with(nolock)  
   WHERE AMI.CUSTOMER_ID=@CUSTOMERID AND AMI.APP_ID=@POLID AND AMI.APP_VERSION_ID=@VERSIONID AND AMI.IS_ACTIVE='Y'  
   AND DATEDIFF(year, MVR_DATE, @POL_EFF_DATE) < 3 AND AMI.VIOLATION_TYPE IS NOT NULL        
     
   SET @VIOL_COUNT = @VIOL_COUNT_TEMP  
  
   SELECT @VIOL_COUNT_TEMP = COUNT(*) FROM APP_PRIOR_LOSS_INFO   with(nolock) 
   WHERE CUSTOMER_ID = @CUSTOMERID AND LOB = '2' AND DATEDIFF(year, OCCURENCE_DATE, @POL_EFF_DATE) < 3   
   AND LOSS_TYPE IS NOT NULL   
  
   SET @VIOL_COUNT = @VIOL_COUNT + @VIOL_COUNT_TEMP  
  
   SELECT @VIOL_COUNT AS VIOL_COUNT  
  
 END        
 ELSE IF (@CALLEDFROM='POLICY')        
 BEGIN        
   SELECT @POL_EFF_DATE=POLICY_EFFECTIVE_DATE FROM POL_CUSTOMER_POLICY_LIST with(nolock) WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID AND POLICY_VERSION_ID=@VERSIONID  
   IF @POL_EFF_DATE IS NULL  
      SET @POL_EFF_DATE = GETDATE()   
  
   SELECT  DRIVER_ID, MVR_DATE, MVR_AMOUNT, MVR_DEATH, MLV.VIOLATION_DES AS VIOLATION_DESC        
   FROM  POL_MVR_INFORMATION PMI with(nolock)        
   LEFT OUTER JOIN MNT_VIOLATIONS MLV ON PMI.VIOLATION_ID = MLV.VIOLATION_ID      
   WHERE PMI.CUSTOMER_ID=@CUSTOMERID AND PMI.POLICY_ID=@POLID AND PMI.POLICY_VERSION_ID=@VERSIONID AND PMI.IS_ACTIVE='Y'        
   AND DATEDIFF(year, MVR_DATE, @POL_EFF_DATE) < 3        
      
   UNION      
      
   SELECT DRIVER_ID, OCCURENCE_DATE AS MVR_DATE, AMOUNT_PAID AS MVR_AMOUNT,'N' AS MVR_DEATH, MLV.LOOKUP_VALUE_DESC AS VIOLATION_DESC      
   FROM APP_PRIOR_LOSS_INFO  with(nolock)     
   LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ON LOSS_TYPE = MLV.LOOKUP_UNIQUE_ID      
   WHERE CUSTOMER_ID = @CUSTOMERID AND LOB = '2' AND DATEDIFF(year, OCCURENCE_DATE, @POL_EFF_DATE) < 3        
  
   ORDER BY MVR_DATE      
  
   SELECT @VIOL_COUNT_TEMP = COUNT(*) FROM POL_MVR_INFORMATION PMI   with(nolock)  
   WHERE PMI.CUSTOMER_ID=@CUSTOMERID AND PMI.POLICY_ID=@POLID AND PMI.POLICY_VERSION_ID=@VERSIONID AND PMI.IS_ACTIVE='Y'  
   AND DATEDIFF(year, MVR_DATE, @POL_EFF_DATE) < 3 AND PMI.VIOLATION_TYPE IS NOT NULL        
     
   SET @VIOL_COUNT = @VIOL_COUNT_TEMP  
  
   SELECT @VIOL_COUNT_TEMP = COUNT(*) FROM APP_PRIOR_LOSS_INFO  with(nolock)  
   WHERE CUSTOMER_ID = @CUSTOMERID AND LOB = '2' AND DATEDIFF(year, OCCURENCE_DATE, @POL_EFF_DATE) < 3   
   AND LOSS_TYPE IS NOT NULL   
  
   SET @VIOL_COUNT = @VIOL_COUNT + @VIOL_COUNT_TEMP  
  
   SELECT @VIOL_COUNT AS VIOL_COUNT  
  
  END        
END        
                
        
        
        
        
        
        
        
        
      
    
  






GO

