IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyRatingInformationForAuto_DriverViolationComponent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyRatingInformationForAuto_DriverViolationComponent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------          
Proc Name             : Dbo.Proc_GetPolicyRatingInformationForAuto_DriverViolationComponent          
Created by            : Shafi.          
Date                  : 02/03/2006         
Purpose                : To get the information for creating the input xml            
Revison History    :          
Used In                 :   Creating InputXML for vehicle          
------------------------------------------------------------------------------------------------------------------------------          
      
-----------------------------------------------------------------------------------------------------------------------------*/          
          
CREATE PROC [dbo].[Proc_GetPolicyRatingInformationForAuto_DriverViolationComponent]  
(          
@CUSTOMERID     int,          
@POLICYID     int,          
@POLICYVERSIONID   int,          
@DRIVERID      int,          
--@VIOLATIONID    int          
@POL_MVR_ID  int       
)          
AS          
          
BEGIN          
 set quoted_identifier off          
 
 DECLARE @VIODATE nvarchar(100)         
     
 DECLARE @VIOLATIONTYPE    varchar(100)
 DECLARE @VIOLATIONTYPE_IN    	varchar(100)      
 DECLARE @VIOLATIONTYPE_EXT    	varchar(100)                  
 DECLARE @VIOLATIONTYPEID      varchar(100)              
 DECLARE @VIOLATIONDESC    varchar(100) 
 DECLARE @VIOLATIONDESC_IN    	varchar(100)
 DECLARE @VIOLATIONDESC_EXT    	varchar(100)           
 DECLARE @VIOLATIONDESCID       varchar(100)              
 DECLARE @MVRPOINTS        varchar(100)              
 DECLARE @WOLVERINE_VIOLATIONS  varchar(100)              
 DECLARE @VIOLATION_CODE        varchar(100)           
 DECLARE @AMOUNTPAID nvarchar(100)          
 DECLARE @DEATH nvarchar(100)          
          
       
    
---Modified on 24 April 2006        
SELECT            
    @VIOLATIONTYPEID = ISNULL(VIOLATION_TYPE,'0'),          
    @VIOLATIONDESCID = ISNULL(VIOLATION_ID,'0'),                 
    @AMOUNTPAID =  ISNULL(MVR_AMOUNT,'0'),                                    
    @DEATH  =  ISNULL(MVR_DEATH,'N') ,                       
    @VIODATE  =  ISNULL(convert(varchar(10),MVR_DATE ,101),''), 
	@WOLVERINE_VIOLATIONS =  ISNULL(POINTS_ASSIGNED,0) + ISNULL(ADJUST_VIOLATION_POINTS,0),
    @VIOLATIONTYPE_EXT	=  ISNULL(DETAILS,''),
    @VIOLATIONDESC_EXT  =  ISNULL(DETAILS,'')               
FROM              
 POL_MVR_INFORMATION PVI  WITH (NOLOCK) INNER JOIN POL_DRIVER_ASSIGNED_VEHICLE PDD WITH (NOLOCK) 
 ON  PVI.CUSTOMER_ID = PDD.CUSTOMER_ID
 AND  PVI.POLICY_ID = PDD.POLICY_ID
 AND  PVI.POLICY_VERSION_ID = PDD.POLICY_VERSION_ID               
 AND PVI.DRIVER_ID = PDD.DRIVER_ID
WHERE             
 PVI.CUSTOMER_ID =@CUSTOMERID AND PVI.POLICY_ID=@POLICYID AND PVI.POLICY_VERSION_ID=@POLICYVERSIONID AND PVI.DRIVER_ID = @DRIVERID AND POL_MVR_ID = @POL_MVR_ID
 AND PDD.APP_VEHICLE_PRIN_OCC_ID IN (11398,11925,11929,11927)          
        
---  End        
      
----FETCHING THE DATA FROM MNT_VIOLATION ACCORDING TO THE VIOLATION DESC ID ----------          
SELECT 
	@VIOLATIONTYPE_IN 		= ISNULL(VIOLATION_DES,''),      
	@VIOLATIONDESC_IN 		= ISNULL(VIOLATION_DES,''), 
	@VIOLATIONDESC =  ISNULL(VIOLATION_DES,''),                  
	@VIOLATION_CODE = ISNULL(VIOLATION_CODE,'0')          
          
 FROM MNT_VIOLATIONS WITH (NOLOCK) WHERE VIOLATION_ID = @VIOLATIONDESCID          
          
----------END FETCHING----------------            
  
IF (@VIOLATIONTYPE_IN IS NULL OR @VIOLATIONTYPE_IN ='0' OR LTRIM(RTRIM(@VIOLATIONTYPE_IN)) ='' )
BEGIN
 SET @VIOLATIONTYPE = @VIOLATIONTYPE_EXT
END
ELSE
BEGIN
 SET @VIOLATIONTYPE = @VIOLATIONTYPE_IN
END
IF (@VIOLATIONDESC_IN IS NULL OR @VIOLATIONDESC_IN ='0' OR LTRIM(RTRIM(@VIOLATIONDESC_IN)) ='' )
BEGIN
 SET @VIOLATIONDESC = @VIOLATIONDESC_EXT
END
ELSE
BEGIN
 SET @VIOLATIONDESC = @VIOLATIONDESC_IN
END               
          
          
 /* Finally select */          
SELECT                    
  @VIODATE      AS VIODATE,                     
  @VIOLATIONTYPE  AS VIOLATIONTYPE,          
  @VIOLATIONTYPEID  AS VIOLATIONID,          
  @VIOLATIONDESC  AS VIOLATIONDESC,          
  @VIOLATIONDESCID  AS VIOLATIONDESCID,          
  @MVRPOINTS    AS MVRPOINTS,  --MVR Points        
  @WOLVERINE_VIOLATIONS AS WOLVERINE_VIOLATIONS,  --VIOLATION POINTS        
  @VIOLATION_CODE   AS VIOLATION_CODE,          
  @AMOUNTPAID     AS AMOUNTPAID,                    
  @DEATH       AS DEATH               
          
END        
        













GO

