IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_POL_POLICY_PROCESS_DUE_DATE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_POL_POLICY_PROCESS_DUE_DATE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                      
Proc Name       : dbo.[Proc_POL_POLICY_PROCESS_DUE_DATE]                      
Created by       : Pravesh K chandel            
Date            : 10/08/2008                      
Purpose        : For updating the due date of POL_POLICY_PROCESS table                      
Revison History :        
Used In        : Wolverine                      
Date     Review By          Comments                      
------   ------------       -------------------------
drop proc dbo.[Proc_POL_POLICY_PROCESS_DUE_DATE]
*/                      
CREATE PROC [dbo].[Proc_POL_POLICY_PROCESS_DUE_DATE]                      
(                      
  @CUSTOMER_ID       INT,                      
  @POLICY_ID        INT,                      
  @POLICY_VERSION_ID      SMALLINT,                      
  @ROW_ID        INT ,
  @DUE_DATE_OUT  datetime  =null output
)
as
BEGIN

DECLARE @INSTALL_PLAN_ID INT,@GRACE_PERIOD INT,@DUE_DATE DATETIME
SELECT @INSTALL_PLAN_ID = ISNULL(INSTALL_PLAN_ID,0) FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) 
	 WHERE  CUSTOMER_ID				 =  @CUSTOMER_ID                      
			AND POLICY_ID			 =  @POLICY_ID                      
			AND POLICY_VERSION_ID    =  @POLICY_VERSION_ID    
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
		AND NEW_POLICY_VERSION_ID	=  @POLICY_VERSION_ID                      
		AND ROW_ID					=  @ROW_ID   
 end
  set @DUE_DATE_OUT = @DUE_DATE
END










GO

