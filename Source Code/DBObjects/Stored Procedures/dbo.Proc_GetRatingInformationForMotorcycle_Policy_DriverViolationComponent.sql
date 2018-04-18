IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRatingInformationForMotorcycle_Policy_DriverViolationComponent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRatingInformationForMotorcycle_Policy_DriverViolationComponent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                    
Proc Name    	: Proc_GetRatingInformationForMotorcycle_Policy_DriverViolationComponent      
Created by   	: Praveen Singh       
Date         	: 10/01/2006          
Purpose      	: Get the violation detail for motorcycle driver     
Revison History  :           
------------------------------------------------------------                                
Date     Review By          Comments                              
                     
------   ------------       -------------------------*/         
CREATE  PROC Proc_GetRatingInformationForMotorcycle_Policy_DriverViolationComponent        
(        
@CUSTOMERID     	INT,        
@POLID     		INT,        
@POLVERSIONID   	INT,        
@DRIVERID     		INT,  
@POL_MVR_ID  		INT          
     
)        
AS        
        
BEGIN        
SET QUOTED_IDENTIFIER OFF        
        
DECLARE @VIODATE     		nvarchar(100)        
DECLARE @VIOLATIONTYPE    	varchar(100)    --VIOLATION DESC        
DECLARE @VIOLATIONTYPEID      	varchar(100)    --VIOLATION TYPE ID        
DECLARE @VIOLATIONTYPE_IN    	varchar(100)    --VIOLATION DESC  
DECLARE @VIOLATIONTYPE_EXT    	varchar(100)    --VIOLATION DESC          
DECLARE @VIOLATIONDESC    	varchar(100)          
DECLARE @VIOLATIONDESCID       	varchar(100)    --VIOLATION DESCRIPTION  ID        
DECLARE @VIOLATIONDESC_IN    	varchar(100)
DECLARE @VIOLATIONDESC_EXT    	varchar(100)
DECLARE @MVRPOINTS        	varchar(100)    --MVR POINTS  ()        
DECLARE @WOLVERINE_VIOLATIONS  	varchar(100)    --VIOLATION POINTS (SD POINTS)        
DECLARE @VIOLATION_CODE        	varchar(100)    --VIOLATION CODE ()        
DECLARE @AMOUNTPAID     	nvarchar(100)        
DECLARE @DEATH     		nvarchar(100)      
DECLARE @DRIVER_DRIV_TYPE 	NVARCHAR(10)
DECLARE @VIOLATION_APPLICABLE   VARCHAR(4)          
         
---MODIFIED ON 24 APRIL 2006      
SELECT          
    @VIOLATIONTYPEID 		= ISNULL(VIOLATION_TYPE,'0'),        
    @VIOLATIONDESCID 		= ISNULL(VIOLATION_ID,'0'),               
    @AMOUNTPAID 		= ISNULL(MVR_AMOUNT,'0'),                                  
    @DEATH  			= ISNULL(MVR_DEATH,'N') ,                     
    @VIODATE  			= ISNULL(CONVERT(VARCHAR(10),MVR_DATE ,101),''),
    @MVRPOINTS			= ISNULL(POINTS_ASSIGNED,0) + ISNULL(ADJUST_VIOLATION_POINTS,0),
    @WOLVERINE_VIOLATIONS 	= ISNULL(POINTS_ASSIGNED,0) + ISNULL(ADJUST_VIOLATION_POINTS,0),
    @VIOLATIONTYPE_EXT		=  ISNULL(DETAILS,''),
    @VIOLATIONDESC_EXT  	=  ISNULL(DETAILS,'')            
FROM            
POL_MVR_INFORMATION WITH (NOLOCK)                 
WHERE           
CUSTOMER_ID =@CUSTOMERID AND POLICY_ID=@POLID AND POLICY_VERSION_ID=@POLVERSIONID AND DRIVER_ID = @DRIVERID AND POL_MVR_ID = @POL_MVR_ID        

---  End      
      
----FETCHING THE DATA FROM MNT_VIOLATION ACCORDING TO THE VIOLATION DESC ID ----------        
SELECT        
@VIOLATIONTYPE = ISNULL(VIOLATION_DES,''),        
@VIOLATIONDESC =  ISNULL(VIOLATION_DES,''),        
--@MVRPOINTS = ISNULL(MVR_POINTS,'0'),        
--@WOLVERINE_VIOLATIONS = ISNULL(SD_POINTS,'0'),        
@VIOLATION_CODE = ISNULL(VIOLATION_CODE,'0')        

FROM MNT_VIOLATIONS WITH (NOLOCK)  
WHERE VIOLATION_ID = @VIOLATIONDESCID        

/*IF (@VIOLATIONTYPE_IN IS NULL OR @VIOLATIONTYPE_IN ='0' OR LTRIM(RTRIM(@VIOLATIONTYPE_IN)) ='' )
BEGIN
SET @VIOLATIONTYPE = @VIOLATIONTYPE_EXT
END
ELSE
BEGIN
SET @VIOLATIONTYPE = @VIOLATIONTYPE_IN
END */

IF (@VIOLATIONDESC_IN IS NULL OR @VIOLATIONDESC_IN ='0' OR LTRIM(RTRIM(@VIOLATIONDESC_IN)) ='' )
BEGIN
SET @VIOLATIONDESC = @VIOLATIONDESC_EXT
END
ELSE
BEGIN
SET @VIOLATIONDESC = @VIOLATIONDESC_IN
END         
----------------------------------
---Added by Manoj Rathore on 23 Jun. 2009 Itrack # 5847
SET @VIOLATION_APPLICABLE='Y'
SELECT @DRIVER_DRIV_TYPE=DRIVER_DRIV_TYPE FROM POL_DRIVER_DETAILS
WHERE CUSTOMER_ID =@CUSTOMERID AND POLICY_ID=@POLID AND POLICY_VERSION_ID=@POLVERSIONID AND DRIVER_ID = @DRIVERID

IF(@DRIVER_DRIV_TYPE='11942')
 BEGIN
 SET @VIOLATION_APPLICABLE='N'
 END 
     
----------------------------------        
SELECT                  
  @VIODATE      		AS VIODATE,                   
  @VIOLATIONTYPE  		AS VIOLATIONTYPE,        
  @VIOLATIONTYPEID  		AS VIOLATIONID,        
  @VIOLATIONDESC  		AS VIOLATIONDESC,        
  @VIOLATIONDESCID  		AS VIOLATIONDESCID,        
  @MVRPOINTS    		AS MVR,  	--MVR POINTS      
  @WOLVERINE_VIOLATIONS 	AS WOLVERINE_VIOLATIONS,  --VIOLATION POINTS      
  @VIOLATION_CODE   		AS VIOLATION_CODE,        
  @AMOUNTPAID     		AS AMOUNTPAID,                  
  @DEATH       			AS DEATH
  --ISNULL(@VIOLATION_APPLICABLE,'Y')		AS VIOLATION_APPLICABLE           
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

END      
      
    
    
    
  












GO

