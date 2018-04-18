IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePOL_POLICY_PROCESS_NOTICE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePOL_POLICY_PROCESS_NOTICE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                      
Proc Name       : dbo.Proc_UpdatePOL_POLICY_PROCESS_NOTICE                      
Created by       : PRAVESH                
Date            : 12/04/2007                      
Purpose        : For updating the record of POL_POLICY_PROCESS table                      
Revison History :        
modified by 	:Pravesh K chandel
modified date	:30 oct 2007
purpose		:Updating POL_VER_EFFECTIVE_DATE      

modified by 	:Pravesh K chandel
modified date	:7 july 2008
purpose		:Updating DUE Date   
        
Used In        : Wolverine                      
Date     Review By          Comments                      
------   ------------       -------------------------
drop proc dbo.Proc_UpdatePOL_POLICY_PROCESS_NOTICE
*/                      
CREATE PROC [dbo].[Proc_UpdatePOL_POLICY_PROCESS_NOTICE]                      
(                      
  @CUSTOMER_ID       INT,                      
  @POLICY_ID        INT,                      
  @POLICY_VERSION_ID      SMALLINT,                      
  @ROW_ID        INT,                      
  @PROCESS_ID       INT,
  @CANCELLATION_NOTICE_SENT   char(1)                   
)
as
BEGIN

 UPDATE  POL_POLICY_PROCESS                   
	set CANCELLATION_NOTICE_SENT=@CANCELLATION_NOTICE_SENT
	WHERE                       
	 CUSTOMER_ID			 =  @CUSTOMER_ID                      
	 AND POLICY_ID			 =  @POLICY_ID                      
	 AND POLICY_VERSION_ID   =  @POLICY_VERSION_ID                      
	 AND ROW_ID				 =  @ROW_ID   


--declare @EFFECTIVE_DATETIME datetime             
--declare @EXPIRY_DATE datetime      
DECLARE @NEW_POLICY_VERSION_ID       INT
SELECT 
--@EFFECTIVE_DATETIME=EFFECTIVE_DATETIME,@EXPIRY_DATE=EXPIRY_DATE,
@NEW_POLICY_VERSION_ID=NEW_POLICY_VERSION_ID
	FROM POL_POLICY_PROCESS WITH(NOLOCK) 
	WHERE   CUSTOMER_ID     =  @CUSTOMER_ID                      
	 AND POLICY_ID          =  @POLICY_ID                      
	 AND POLICY_VERSION_ID  =  @POLICY_VERSION_ID                      
	 AND ROW_ID             =  @ROW_ID    
/*-- update POL_CUSTOMER_POLICY_LIST POL_VER_EFFECTIVE_DATE                 
UPDATE POL_CUSTOMER_POLICY_LIST                  
 SET POL_VER_EFFECTIVE_DATE = @EFFECTIVE_DATETIME                  
 -- POL_VER_EXPIRATION_DATE = @EXPIRY_DATE                  
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID  AND POLICY_VERSION_ID=@NEW_POLICY_VERSION_ID                      
*/

--Ravindra(08-21-2008): This is supposed to be implemented at form level, commenting here 
/*
--ADDED BY PRAVESH ON 7 JULY 2008 TO UPDATE DUE DATE AS ASKED BY RAVINDER'S MAIL
DECLARE @INSTALL_PLAN_ID INT,@GRACE_PERIOD INT,@DUE_DATE DATETIME
SELECT @INSTALL_PLAN_ID = ISNULL(INSTALL_PLAN_ID,0) FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) 
	 WHERE  CUSTOMER_ID				 =  @CUSTOMER_ID                      
			AND POLICY_ID			 =  @POLICY_ID                      
			AND POLICY_VERSION_ID    =  @NEW_POLICY_VERSION_ID    
IF (ISNULL(@INSTALL_PLAN_ID,0)=0) --install plan not applicable the pick grace period of full pay plan as discussed with Rajan
	SELECT @GRACE_PERIOD = ISNULL(GRACE_PERIOD,0) FROM ACT_INSTALL_PLAN_DETAIL WITH(NOLOCK) WHERE ISNULL(SYSTEM_GENERATED_FULL_PAY,0)=1
ELSE
	SELECT @GRACE_PERIOD = ISNULL(GRACE_PERIOD,0) FROM ACT_INSTALL_PLAN_DETAIL WITH(NOLOCK) WHERE IDEN_PLAN_ID = @INSTALL_PLAN_ID

 SET @DUE_DATE = DATEADD(DD,@GRACE_PERIOD,GETDATE())

IF(DATEPART(WEEKDAY,@DUE_DATE)=1) --1 SUNDAY
	SET @DUE_DATE = DATEADD(DD,1,GETDATE())
ELSE IF (DATEPART(WEEKDAY,@DUE_DATE)=7) -- 7 SATURDAY
	SET @DUE_DATE = DATEADD(DD,2,GETDATE())

if (@DUE_DATE is not null)
begin
 UPDATE  POL_POLICY_PROCESS                   
	SET DUE_DATE=@DUE_DATE
	WHERE  CUSTOMER_ID				=  @CUSTOMER_ID                      
		AND POLICY_ID				=  @POLICY_ID                      
		AND POLICY_VERSION_ID   =  @POLICY_VERSION_ID                      
		AND ROW_ID					=  @ROW_ID   
 end
*/

END











GO

