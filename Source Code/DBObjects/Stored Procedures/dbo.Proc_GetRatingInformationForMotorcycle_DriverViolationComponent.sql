IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRatingInformationForMotorcycle_DriverViolationComponent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRatingInformationForMotorcycle_DriverViolationComponent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------          
Proc Name           : Dbo.Proc_GetRatingInformationForAuto_DriverViolationComponent          
Created by          : Nidhi.          
Date                : 19/12/2005          
Purpose             : To get the information for creating the input xml            
Revison History     :          
Used In             :   Creating InputXML for vehicle          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
         
CREATE  PROC dbo.Proc_GetRatingInformationForMotorcycle_DriverViolationComponent         
(          
@CUSTOMERID      INT,          
@APPID      INT,          
@APPVERSIONID    INT,          
@DRIVERID      INT ,         
@APP_MVR_ID   INT      
)          
AS          
          
BEGIN          
SET QUOTED_IDENTIFIER OFF          
          
DECLARE @VIODATE      nvarchar(100)          
DECLARE @VIOLATIONTYPE     varchar(100)    --VIOLATION DESC        
DECLARE @VIOLATIONTYPEID       varchar(100)    --VIOLATION TYPE ID 
DECLARE @VIOLATIONTYPE_IN     varchar(100)    --VIOLATION DESC    
DECLARE @VIOLATIONTYPE_EXT     varchar(100)    --VIOLATION DESC          
DECLARE @VIOLATIONDESC     varchar(100)          
DECLARE @VIOLATIONDESCID        varchar(100)    --VIOLATION DESCRIPTION  ID 
DECLARE @VIOLATIONDESC_IN     varchar(100)  
DECLARE @VIOLATIONDESC_EXT     varchar(100)         
DECLARE @MVRPOINTS         varchar(100)    --MVR POINTS  ()        
DECLARE @WOLVERINE_VIOLATIONS   varchar(100)    --VIOLATION POINTS (SD POINTS)        
DECLARE @VIOLATION_CODE         varchar(100)    --VIOLATION CODE ()        
DECLARE @AMOUNTPAID     nvarchar(100)          
DECLARE @DEATH      nvarchar(100)          
DECLARE @DRIVER_DRIV_TYPE NVARCHAR(10)
DECLARE @VIOLATION_APPLICABLE VARCHAR(4)  
----------------------------------          
-- MVR_AMOUNT , VIOLATION_DES , MVR_DATE , MVR_DEATH START          
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------




  
    
---MODIFIED   
SELECT          
    @VIOLATIONTYPEID  = ISNULL(VIOLATION_TYPE,'0'),        
    @VIOLATIONDESCID  = ISNULL(VIOLATION_ID,'0'),               
    @AMOUNTPAID  = ISNULL(MVR_AMOUNT,'0'),                                  
    @DEATH    = ISNULL(MVR_DEATH,'N') ,                     
    @VIODATE    = ISNULL(CONVERT(VARCHAR(10),MVR_DATE ,101),''),
    @MVRPOINTS   = ISNULL(POINTS_ASSIGNED,0) + ISNULL(ADJUST_VIOLATION_POINTS,0),  
    @WOLVERINE_VIOLATIONS  = ISNULL(POINTS_ASSIGNED,0) + ISNULL(ADJUST_VIOLATION_POINTS,0),  
    @VIOLATIONTYPE_EXT  =  ISNULL(DETAILS,''),  
    @VIOLATIONDESC_EXT   =  ISNULL(DETAILS,'')   
FROM            
 APP_MVR_INFORMATION WITH (NOLOCK)                 
WHERE           
 CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   
 AND DRIVER_ID = @DRIVERID AND APP_MVR_ID = @APP_MVR_ID        
      
    
      
----FETCHING THE DATA FROM MNT_VIOLATION ACCORDING TO THE VIOLATION DESC ID ----------        
SELECT  
   @VIOLATIONTYPE = ISNULL(VIOLATION_DES,''),          
   @VIOLATIONDESC =  ISNULL(VIOLATION_DES,''),        
   --@VIOLATIONTYPE   = ISNULL(VIOLATION_DES,''),        
   --@VIOLATIONDESC   = ISNULL(VIOLATION_DES,''),        
  -- @MVRPOINTS    = ISNULL(MVR_POINTS,'0'),        
   @WOLVERINE_VIOLATIONS = ISNULL(SD_POINTS,'0'),        
   @VIOLATION_CODE   = ISNULL(VIOLATION_CODE,'0')        
        
 FROM MNT_VIOLATIONS WITH (NOLOCK) WHERE VIOLATION_ID = @VIOLATIONDESCID      

        
	/* IF (@VIOLATIONTYPE_IN IS NULL OR @VIOLATIONTYPE_IN ='0' OR LTRIM(RTRIM(@VIOLATIONTYPE_IN)) ='' )  
	BEGIN  
	 SET @VIOLATIONTYPE = @VIOLATIONTYPE_EXT  
	END  
	ELSE  BEGIN  
	 SET @VIOLATIONTYPE = @VIOLATIONTYPE_IN  
	END   */

	IF (@VIOLATIONDESC_IN IS NULL OR @VIOLATIONDESC_IN ='0' OR LTRIM(RTRIM(@VIOLATIONDESC_IN)) ='' )  
	BEGIN  
	 SET @VIOLATIONDESC = @VIOLATIONDESC_EXT  
	END  
	ELSE  
	BEGIN  
	 SET @VIOLATIONDESC = @VIOLATIONDESC_IN  
	END

---Added by Manoj Rathore on 23 Jun. 2009 Itrack # 5847
SET @VIOLATION_APPLICABLE='Y'
SELECT @DRIVER_DRIV_TYPE=DRIVER_DRIV_TYPE FROM APP_DRIVER_DETAILS
WHERE CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND DRIVER_ID = @DRIVERID

IF(@DRIVER_DRIV_TYPE='11942') 
BEGIN 
SET @VIOLATION_APPLICABLE='N' 
END 
     
SELECT                  
  isnull(@VIODATE,'0')       AS VIODATE,                   
  isnull(@VIOLATIONTYPE,'0')   AS VIOLATIONTYPE,        
  isnull(@VIOLATIONTYPEID,'0')   AS VIOLATIONID,        
  isnull(@VIOLATIONDESC,'0')   AS VIOLATIONDESC,        
  isnull(@VIOLATIONDESCID,'0')   AS VIOLATIONDESCID,        
  isnull(@MVRPOINTS,'0')     AS MVR,    --MVR POINTS      
  isnull(@WOLVERINE_VIOLATIONS,'0') AS WOLVERINE_VIOLATIONS,  --VIOLATION POINTS      
  isnull(@VIOLATION_CODE,'0')    AS VIOLATION_CODE,        
  isnull(@AMOUNTPAID,'0')      AS AMOUNTPAID,                  
  isnull(@DEATH,'0')         AS DEATH                  
  --isnull(@VIOLATION_APPLICABLE,'Y') AS VIOLATION_APPLICABLE      
    
          
END







GO

