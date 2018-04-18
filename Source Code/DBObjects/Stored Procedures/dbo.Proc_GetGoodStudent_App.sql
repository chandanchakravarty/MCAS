IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetGoodStudent_App]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetGoodStudent_App]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*--------------------------------
Created By		:P.K Chandel
Created Date	:24 Sep 2008
Purpose			: To get Whether a Driver is elligible for Good Student Discount or Not
Used In			: Wolverine/BRICS
*/
--drop proc dbo.Proc_GetGoodStudent_App
create Proc dbo.Proc_GetGoodStudent_App
(
@CUSTOMER_ID INT,
@APP_ID SMALLINT,
@APP_VERSION_ID INT,
@VEHICLE_ID SMALLINT,
@GOODSTUDENT nvarchar(10) output
)
as
BEGIN

	DECLARE @ASSPOINTDRIVER NVARCHAR(20),@RATEDDRIVER INT,@SUM_MVR_POINTS int,@ACCIDENT_POINTS int,@APP_EFFECTIVE_DATE datetime
	DECLARE    @ACCIDENT_NUM_YEAR   INT
	DECLARE    @VIOLATION_NUM_YEAR  INT
	DECLARE    @ACCIDENT_PAID_AMOUNT  INT
    DECLARE    @VIOLATION_NUM_YEAR_REFER  INT

	SET  @ACCIDENT_NUM_YEAR=3
	SET @VIOLATION_NUM_YEAR=2
	SET @VIOLATION_NUM_YEAR_REFER=3
	SET @ACCIDENT_PAID_AMOUNT=1000 
	SET @SUM_MVR_POINTS =0
	SET @ACCIDENT_POINTS =0

	--geting policy effective date 
	SELECT @APP_EFFECTIVE_DATE =  APP_EFFECTIVE_DATE FROM APP_LIST  WITH (NOLOCK)      
			WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID   
	--geting rated driver of the Vehicle
	SELECT @RATEDDRIVER = CLASS_DRIVERID FROM APP_VEHICLES AV  WITH (NOLOCK)  WHERE CUSTOMER_ID= @CUSTOMER_ID AND APP_ID= @APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND  VEHICLE_ID=@VEHICLE_ID                                           
	-- 25 YEARS PREV DATE
DECLARE @TWENTYFIVEYEARDATE DATETIME
DECLARE @TWENTYFIVEYEARDAYS INT
SET @TWENTYFIVEYEARDATE = DATEADD(YEAR,-25,@APP_EFFECTIVE_DATE)
SET @TWENTYFIVEYEARDAYS = DATEDIFF(DAY,@TWENTYFIVEYEARDATE,@APP_EFFECTIVE_DATE)	
-- checking whether rated driver is with point assigned or not
 IF EXISTS(
			SELECT ADDS.DRIVER_ID FROM  APP_DRIVER_ASSIGNED_VEHICLE ADDS WITH (NOLOCK) 
			WHERE ADDS.CUSTOMER_ID=@CUSTOMER_ID 
				AND ADDS.APP_ID=@APP_ID 
				AND ADDS.APP_VERSION_ID=@APP_VERSION_ID 
				AND ADDS.VEHICLE_ID=@VEHICLE_ID 
				AND ADDS.DRIVER_ID  = @RATEDDRIVER
				AND ADDS.APP_VEHICLE_PRIN_OCC_ID IN (11398,11925,11927,11929) -- CONSIDER ONLY IF RATED DRIVER IS WITH POINTS ASSIGNED
			)
	BEGIN	
		CREATE TABLE #RATEDDRIVER_VIOLATION_ACCIDENT
		(
			[SUM_MVR_POINTS]		INT,                                            
			[ACCIDENT_POINTS]		INT,                            
			[COUNT_ACCIDENTS]		INT,
			[MVR_POINTS]		INT            
		) 
		
		INSERT INTO #RATEDDRIVER_VIOLATION_ACCIDENT EXEC GetMVRViolationPoints @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID ,@RATEDDRIVER,@ACCIDENT_NUM_YEAR,@VIOLATION_NUM_YEAR,@VIOLATION_NUM_YEAR_REFER,@ACCIDENT_PAID_AMOUNT           

		SELECT @SUM_MVR_POINTS = SUM_MVR_POINTS,@ACCIDENT_POINTS = ACCIDENT_POINTS FROM #RATEDDRIVER_VIOLATION_ACCIDENT
			IF(@SUM_MVR_POINTS IS NULL)
				SET @SUM_MVR_POINTS	=0
			IF(@ACCIDENT_POINTS IS NULL)
				SET @ACCIDENT_POINTS =0
		SET @SUM_MVR_POINTS= @SUM_MVR_POINTS + @ACCIDENT_POINTS
		DROP TABLE  #RATEDDRIVER_VIOLATION_ACCIDENT
	END
	--	RATED DRIVER VIOLATION AND ACCIDENT (END)
	-- Rated Driver OverRide (Itrack 4544) (Start)
	SET @GOODSTUDENT = 'FALSE'

	DECLARE @GOODDRIVER NVARCHAR(10)
	SELECT @GOODDRIVER = ADDS.DRIVER_ID 
		FROM  APP_DRIVER_ASSIGNED_VEHICLE ADDS WITH (NOLOCK) 
		INNER JOIN APP_DRIVER_DETAILS WITH (NOLOCK)                  
		ON ADDS.CUSTOMER_ID=APP_DRIVER_DETAILS.CUSTOMER_ID AND                  
		ADDS.APP_ID=APP_DRIVER_DETAILS.APP_ID AND                  
		ADDS.APP_VERSION_ID = APP_DRIVER_DETAILS.APP_VERSION_ID                    
		AND  ADDS.DRIVER_ID=APP_DRIVER_DETAILS.DRIVER_ID 
                  
		WHERE ADDS.CUSTOMER_ID=@CUSTOMER_ID 
		AND ADDS.APP_ID=@APP_ID 
		AND ADDS.APP_VERSION_ID=@APP_VERSION_ID 
		AND ADDS.DRIVER_ID=@RATEDDRIVER
		AND (ISNULL(APP_DRIVER_DETAILS.DRIVER_GOOD_STUDENT,0)=1 AND ISNULL(APP_DRIVER_DETAILS.FULL_TIME_STUDENT,0)=1) 
		AND (DATEDIFF(DAY,APP_DRIVER_DETAILS.DRIVER_DOB,@APP_EFFECTIVE_DATE) < @TWENTYFIVEYEARDAYS) 
		AND ADDS.APP_VEHICLE_PRIN_OCC_ID IN (11927,11928,11929,11930)         

	IF (@GOODDRIVER IS NOT NULL)
	BEGIN
		SET @GOODSTUDENT='TRUE' --based on Screen Drop Down
	END   

	IF(@SUM_MVR_POINTS < 3 AND @GOODDRIVER IS NOT NULL)
		BEGIN
			SET @GOODSTUDENT='TRUE' -- based on screen and points asigned
		END
	 ELSE
		BEGIN 
			SET @GOODSTUDENT='FALSE'
		END 
		

END

GO

