IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckMVR_Requird]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckMVR_Requird]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                
Proc Name       : dbo.Proc_CheckMVR_Requird                                
Created by      : Pravesh K. Chandel  
Date            : 11 Jan-2007                
Purpose       : to Check If MVR Required  
Revison History :     
modified by	: pravesh
modified date	: 29 march 07
purpose		: Add vehicle Id and fetch MVR date from  POL_DRIVER_DETAILS instead of  POL_MVR_INFORMATION                        

modified by	: pravesh
modified date	: 19 march 09
purpose		: do changes as per Itrack 5465

modified by	: pravesh
modified date	: 22 June 09
purpose		: do changes as per Itrack 5994 for MotorCycle


Used In       : Wolverine      
Proc_CheckMVR_Requird 837,130,2  
DROP PROC dbo.Proc_CheckMVR_Requird  
*/  
Create Proc [dbo].[Proc_CheckMVR_Requird]  
(  
@CUSTOMER_ID INT,  
@POLICY_ID  int,  
@POLICY_VERSION_ID smallint  
)  
as  
BEGIN  
  
DECLARE @STATEID INT  
DECLARE @LOBID INT  
DECLARE @SUB_LOBID INT  
DECLARE @SUB_LOBCODE VARCHAR(10)  
DECLARE @RENEW_EFFECTIVE_DATE DATETIME  
SELECT  @STATEID = STATE_ID ,@LOBID=POLICY_LOB ,@SUB_LOBID=POLICY_SUBLOB,@SUB_LOBCODE=MNT.SUB_LOB_CODE ,
@RENEW_EFFECTIVE_DATE= APP_EFFECTIVE_DATE
 FROM POL_CUSTOMER_POLICY_LIST PL  with(nolock)
 LEFT JOIN MNT_SUB_LOB_MASTER MNT with(nolock) ON PL.POLICY_LOB=MNT.LOB_ID and PL.POLICY_SUBLOB=MNT.SUB_LOB_ID   
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
--select @SUB_LOBCODE  
CREATE TABLE #TEMP_DRIVER_DETAILS  
(  
LOB_ID INT,  
DRIVER_AGE INT,  
DRIVER_ID INT,  
DRIVER_FNAME NVARCHAR(75),  
DRIVER_MNAME NVARCHAR(25),  
DRIVER_LNAME NVARCHAR(75),  
DRIVER_CITY  NVARCHAR(40),  
DRIVER_STATE NVARCHAR(10),  
DRIVER_ZIP   NVARCHAR(11),  
STATE_CODE   NVARCHAR(10),  
DRIVER_DRIV_LIC NVARCHAR(30),  
DRIVER_SUFFIX   NVARCHAR(11),  
DRIVER_DOB datetime,  
DRIVER_SEX     NVARCHAR(2),  
STATE_ID      NVARCHAR(2),  
VEHICLE_ID    smallint
)  
declare @DriverAge int  
DECLARE  @DRIVER_ID INT  
DECLARE @DRIVER_FNAME NVARCHAR(75)  
DECLARE @DRIVER_MNAME NVARCHAR(25)  
DECLARE @DRIVER_LNAME NVARCHAR(75)  
DECLARE @DRIVER_CITY  NVARCHAR(40)  
DECLARE @DRIVER_STATE NVARCHAR(10)  
DECLARE @DRIVER_ZIP   NVARCHAR(11)  
DECLARE @STATE_CODE  nVARCHAR(10)  
DECLARE @DRIVER_DRIV_LIC NVARCHAR(30)  
DECLARE @DRIVER_SUFFIX   NVARCHAR(11)  
DECLARE @DRIVER_DOB datetime  
DECLARE @DRIVER_SEX     NVARCHAR(2)  
DECLARE @STATE_ID      NVARCHAR(2)  
DECLARE @VEHICLE_ID      smallint
  
DECLARE @MVR_DATE DATETIME  

DECLARE @INSURANCE_SCORE INT  
DECLARE @FAULT INT  
  
DECLARE  CR CURSOR for select dbo.GetAge(driver_dob,@RENEW_EFFECTIVE_DATE) AS Driver_Age,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,  
   DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,mnt.STATE_CODE,DRIVER_DRIV_LIC,DRIVER_SUFFIX,DRIVER_DOB,DRIVER_SEX,DRIVER_STATE AS STATE_ID ,VEHICLE_ID 
                --datediff(dd,driver_dob,getdate()),datediff(yy,driver_dob,getdate())years,datediff(mm,driver_dob,getdate()),datediff(mm,driver_dob,getdate())/12,  
   from pol_driver_details PL with(nolock) left join mnt_country_state_list mnt with(nolock) on pl.DRIVER_LIC_STATE=mnt.state_id  
  where   
   CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
  
IF (@STATEID=14 AND @LOBID=2 ) --AND ISNULL(@SUB_LOBCODE,'')='TLB')  Change as per Itrack 5465
 begin  
  
  OPEN CR  
  FETCH NEXT FROM CR INTO @DriverAge,@DRIVER_ID,@DRIVER_FNAME,@DRIVER_MNAME,@DRIVER_LNAME,@DRIVER_CITY,@DRIVER_STATE,@DRIVER_ZIP,@STATE_CODE,@DRIVER_DRIV_LIC,@DRIVER_SUFFIX,@DRIVER_DOB,@DRIVER_SEX,@DRIVER_STATE ,@VEHICLE_ID 
  WHILE @@FETCH_STATUS = 0  
  BEGIN   
   select @MVR_DATE=max(DATE_ORDERED) from POL_DRIVER_DETAILS with(nolock) where CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND DRIVER_ID=@DRIVER_ID    
   if (@DriverAge>=16 and @DriverAge<=24)  
	begin 
	IF (DATEDIFF(dd,@MVR_DATE,@RENEW_EFFECTIVE_DATE)>=365 OR @MVR_DATE IS NULL )  
		INSERT INTO #TEMP_DRIVER_DETAILS VALUES(@LOBID,@DriverAge,@DRIVER_ID,@DRIVER_FNAME,@DRIVER_MNAME,@DRIVER_LNAME,@DRIVER_CITY,@DRIVER_STATE,@DRIVER_ZIP,@STATE_CODE,@DRIVER_DRIV_LIC,@DRIVER_SUFFIX,@DRIVER_DOB,@DRIVER_SEX,@STATEID,@VEHICLE_ID) --@DRIVER_STATE,@VEHICLE_ID)  
	end
   else if (@DriverAge>=25)  --(((@DriverAge>=25 and @DriverAge<=34) or @DriverAge>=70) and @LOBID=2)   Change as per Itrack 5465 
    begin  
--	   select @MVR_DATE=max(DATE_ORDERED) from POL_DRIVER_DETAILS with(nolock) where CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND DRIVER_ID=@DRIVER_ID    
        IF (DATEDIFF(dd,@MVR_DATE,@RENEW_EFFECTIVE_DATE)>=2*365 OR @MVR_DATE IS NULL )  
      	   INSERT INTO #TEMP_DRIVER_DETAILS VALUES(@LOBID,@DriverAge,@DRIVER_ID,@DRIVER_FNAME,@DRIVER_MNAME,@DRIVER_LNAME,@DRIVER_CITY,@DRIVER_STATE,@DRIVER_ZIP,@STATE_CODE,@DRIVER_DRIV_LIC,@DRIVER_SUFFIX,@DRIVER_DOB,@DRIVER_SEX,@STATEID,@VEHICLE_ID) --,@DRIVER_STATE,@VEHICLE_ID) 

    end  
  FETCH NEXT FROM CR INTO @DriverAge,@DRIVER_ID,@DRIVER_FNAME,@DRIVER_MNAME,@DRIVER_LNAME,@DRIVER_CITY,@DRIVER_STATE,@DRIVER_ZIP,@STATE_CODE,@DRIVER_DRIV_LIC,@DRIVER_SUFFIX,@DRIVER_DOB,@DRIVER_SEX,@DRIVER_STATE ,@VEHICLE_ID 
  END  
  CLOSE CR  
  DEALLOCATE CR  
 end  
--ELSE if ((@STATEID=14 AND ISNULL(@SUB_LOBCODE,'')<>'TLB') or @LOBID=3 )   Commented as per Itrack 5465 
ELSE if (@LOBID=3 and @STATEID=14)  -- State Cindition Added as per Itrack 5994
 begin  
  OPEN CR  
  FETCH NEXT FROM CR INTO @DriverAge,@DRIVER_ID,@DRIVER_FNAME,@DRIVER_MNAME,@DRIVER_LNAME,@DRIVER_CITY,@DRIVER_STATE,@DRIVER_ZIP,@STATE_CODE,@DRIVER_DRIV_LIC,@DRIVER_SUFFIX,@DRIVER_DOB,@DRIVER_SEX,@DRIVER_STATE ,@VEHICLE_ID 
  WHILE @@FETCH_STATUS = 0  
  BEGIN   
	select @MVR_DATE=max(DATE_ORDERED) from POL_DRIVER_DETAILS with(nolock) where CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND DRIVER_ID=@DRIVER_ID       
   if (@DriverAge>=16 and @DriverAge<=24)
	begin  
	IF (DATEDIFF(dd,@MVR_DATE,@RENEW_EFFECTIVE_DATE)>=365 OR @MVR_DATE IS NULL)  -- CHECK DEFFRENCE B/W MVR DATE AND RENEWAL EFFECTIVE DATE  
		INSERT INTO #TEMP_DRIVER_DETAILS VALUES(@LOBID,@DriverAge,@DRIVER_ID,@DRIVER_FNAME,@DRIVER_MNAME,@DRIVER_LNAME,@DRIVER_CITY,@DRIVER_STATE,@DRIVER_ZIP,@STATE_CODE,@DRIVER_DRIV_LIC,@DRIVER_SUFFIX,@DRIVER_DOB,@DRIVER_SEX,@STATEID,@VEHICLE_ID) --,@DRIVER_STATE,@VEHICLE_ID)  
	end
   else if (@DriverAge>=25)  
    begin  
         --select @MVR_DATE=max(DATE_ORDERED) from POL_DRIVER_DETAILS with(nolock) where CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND DRIVER_ID=@DRIVER_ID    
      IF (DATEDIFF(dd,@MVR_DATE,@RENEW_EFFECTIVE_DATE)>=365*2 OR @MVR_DATE IS NULL)  -- CHECK DEFFRENCE B/W MVR DATE AND RENEWAL EFFECTIVE DATE  
      BEGIN   
		/* Condition Commented as Itrack 5994 
      --FETCH INSURANCE SCORE  
      SELECT  @INSURANCE_SCORE=ISNULL(APPLY_INSURANCE_SCORE,0) FROM POL_CUSTOMER_POLICY_LIST with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID   
      SELECT  @FAULT = COUNT(OCCURENCE_DATE) FROM APP_PRIOR_LOSS_INFO with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID AND LOB=@LOBID AND AT_FAULT=10963 AND DRIVER_ID=@DRIVER_ID 
					AND DATEDIFF(dd,OCCURENCE_DATE,@RENEW_EFFECTIVE_DATE)<=3*365   -- >=36  Changed by Pravesh on 19 March 09 Itrack 5465
        IF (not (@INSURANCE_SCORE>=701 AND @FAULT = 0)) --NO FAULT IN LAST 3 YEAR  --(not (@INSURANCE_SCORE>=701 AND @FAULT <> 0)) --NO FAULT IN LAST 3 YEAR Changed by Pravesh on 19 March 09 Itrack 5465
     	  INSERT INTO #TEMP_DRIVER_DETAILS VALUES(@LOBID,@DriverAge,@DRIVER_ID,@DRIVER_FNAME,@DRIVER_MNAME,@DRIVER_LNAME,@DRIVER_CITY,@DRIVER_STATE,@DRIVER_ZIP,@STATE_CODE,@DRIVER_DRIV_LIC,@DRIVER_SUFFIX,@DRIVER_DOB,@DRIVER_SEX,@STATEID,@VEHICLE_ID)--,@DRIVER_STATE,@VEHICLE_ID)  		*/   		INSERT INTO #TEMP_DRIVER_DETAILS VALUES(@LOBID,@DriverAge,@DRIVER_ID,@DRIVER_FNAME,@DRIVER_MNAME,@DRIVER_LNAME,@DRIVER_CITY,@DRIVER_STATE,@DRIVER_ZIP,@STATE_CODE,@DRIVER_DRIV_LIC,@DRIVER_SUFFIX,@DRIVER_DOB,@DRIVER_SEX,@STATEID,@VEHICLE_ID)--,@DRIVER_STATE,@VEHICLE_ID)        END  
    end  
  FETCH NEXT FROM CR INTO @DriverAge,@DRIVER_ID,@DRIVER_FNAME,@DRIVER_MNAME,@DRIVER_LNAME,@DRIVER_CITY,@DRIVER_STATE,@DRIVER_ZIP,@STATE_CODE,@DRIVER_DRIV_LIC,@DRIVER_SUFFIX,@DRIVER_DOB,@DRIVER_SEX,@DRIVER_STATE,@VEHICLE_ID  
  END  
  CLOSE CR  
  DEALLOCATE CR  
   
 end  
/*ELSE if (@LOBID=3)  
 begin  
  OPEN CR  
  FETCH NEXT FROM CR INTO @DriverAge,@DRIVER_ID,@DRIVER_FNAME,@DRIVER_MNAME,@DRIVER_LNAME,@DRIVER_CITY,@DRIVER_STATE,@DRIVER_ZIP,@STATE_CODE,@DRIVER_DRIV_LIC,@DRIVER_SUFFIX,@DRIVER_DOB,@DRIVER_SEX,@DRIVER_STATE  
  WHILE @@FETCH_STATUS = 0  
  BEGIN   
      
   if (@DriverAge>=16 and @DriverAge<=24)  
    INSERT INTO #TEMP_DRIVER_DETAILS VALUES(@LOBID,@DriverAge,@DRIVER_ID,@DRIVER_FNAME,@DRIVER_MNAME,@DRIVER_LNAME,@DRIVER_CITY,@DRIVER_STATE,@DRIVER_ZIP,@STATE_CODE,@DRIVER_DRIV_LIC,@DRIVER_SUFFIX,@DRIVER_DOB,@DRIVER_SEX,@DRIVER_STATE)  
  else if (@DriverAge>=25)  
    begin  
        select @MVR_DATE=MAX(MVR_DATE) from POL_MVR_INFORMATION where CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND DRIVER_ID=@DRIVER_ID    
        SELECT @RENEW_EFFECTIVE_DATE= POLICY_EFFECTIVE_DATE FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID   
      IF (DATEDIFF(mm,@MVR_DATE,@RENEW_EFFECTIVE_DATE)>=24)  -- CHECK DEFFRENCE B/W MVR DATE AND RENEWAL EFFECTIVE DATE  
      BEGIN   
      --FETCH INSURANCE SCORE  
      SELECT  @INSURANCE_SCORE=ISNULL(APPLY_INSURANCE_SCORE,0) FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID   
      SELECT  @FAULT = COUNT(OCCURENCE_DATE) FROM APP_PRIOR_LOSS_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND LOB=@LOBID AND AT_FAULT=10963 AND DRIVER_ID=@DRIVER_ID AND DATEDIFF(mm,OCCURENCE_DATE,GETDATE())>=36  
        IF (@INSURANCE_SCORE>=701 AND @FAULT <>0) --NO FAULT IN LAAST 3 YEAR  
       INSERT INTO #TEMP_DRIVER_DETAILS VALUES(@LOBID,@DriverAge,@DRIVER_ID,@DRIVER_FNAME,@DRIVER_MNAME,@DRIVER_LNAME,@DRIVER_CITY,@DRIVER_STATE,@DRIVER_ZIP,@STATE_CODE,@DRIVER_DRIV_LIC,@DRIVER_SUFFIX,@DRIVER_DOB,@DRIVER_SEX,@DRIVER_STATE)  
      END  
    end  
  FETCH NEXT FROM CR INTO @DriverAge,@DRIVER_ID,@DRIVER_FNAME,@DRIVER_MNAME,@DRIVER_LNAME,@DRIVER_CITY,@DRIVER_STATE,@DRIVER_ZIP,@STATE_CODE,@DRIVER_DRIV_LIC,@DRIVER_SUFFIX,@DRIVER_DOB,@DRIVER_SEX,@DRIVER_STATE  
  END  
  CLOSE CR  
  DEALLOCATE CR  
 end  
  
  
*/  
SELECT * FROM #TEMP_DRIVER_DETAILS with(nolock)
DROP TABLE #TEMP_DRIVER_DETAILS  
END  
  
/*  
 select dbo.GetAge(driver_dob,getdate()) AS Driver_Age,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,  
   DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,mnt.STATE_CODE,DRIVER_DRIV_LIC,DRIVER_SUFFIX,DRIVER_DOB,DRIVER_SEX,DRIVER_STATE AS STATE_ID  
   from pol_driver_details PL left join mnt_country_state_list mnt on pl.DRIVER_STATE=mnt.state_id  
  where   
   CUSTOMER_ID=837 and driver_id=1 --AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
*/  














GO

