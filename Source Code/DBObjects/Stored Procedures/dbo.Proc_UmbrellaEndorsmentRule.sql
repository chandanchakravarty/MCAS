IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UmbrellaEndorsmentRule]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UmbrellaEndorsmentRule]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*----------------------------------------------------------              
Proc Name   : dbo.Proc_UmbrellaEndorsmentRule
Created by  : Sumit Chhabra              
Date        : 08 May,2007            
Purpose     :               
Revison History  :                    
 ------------------------------------------------------------                          
Date     Review By          Comments                        
               
------   ------------       -------------------------*/        
      
CREATE PROCEDURE dbo.Proc_UmbrellaEndorsmentRule      
(             
 @CUSTOMER_ID Int,      
 @APP_POL_ID Int,      
 @APP_POL_VERSION_ID SmallInt,      
 @CALLED_FOR varchar(20),
 @CALLED_FROM varchar(20)
)      
      
As 
begin
DECLARE @IS_BOAT_EXCLUDED int
DECLARE @RETURN_VALUE int
DECLARE @COVERAGE_CODE_ID varchar(100)
DECLARE @STATE_ID int

set @RETURN_VALUE=-1
IF(UPPER(@CALLED_FROM)='APPLICATION')
BEGIN
	SELECT @STATE_ID = STATE_ID FROM APP_LIST where CUSTOMER_ID=@CUSTOMER_ID and
						APP_ID = @APP_POL_ID and
						APP_VERSION_ID = @APP_POL_VERSION_ID

	if(UPPER(@CALLED_FOR) = 'REC_VEH')
	BEGIN
		SELECT @IS_BOAT_EXCLUDED=COUNT(IS_BOAT_EXCLUDED) from APP_UMBRELLA_RECREATIONAL_VEHICLES 
				where   CUSTOMER_ID=@CUSTOMER_ID and
						APP_ID = @APP_POL_ID and
						APP_VERSION_ID = @APP_POL_VERSION_ID and
						ACTIVE = 'Y' AND
						IS_BOAT_EXCLUDED = 10963 --yes

		IF(@IS_BOAT_EXCLUDED=1)
		BEGIN
			IF(@STATE_ID=22)
				SET @COVERAGE_CODE_ID = 'UBDRMV'
			ELSE
				SET @COVERAGE_CODE_ID = '1048'
		END
			
	END
	ELSE if(UPPER(@CALLED_FOR) = 'BOAT')
	BEGIN
		SELECT @IS_BOAT_EXCLUDED=COUNT(IS_BOAT_EXCLUDED) from APP_UMBRELLA_WATERCRAFT_INFO
				where   CUSTOMER_ID=@CUSTOMER_ID and
						APP_ID = @APP_POL_ID and
						APP_VERSION_ID = @APP_POL_VERSION_ID and
						IS_ACTIVE = 'Y' and
						IS_BOAT_EXCLUDED = 10963  --yes
		IF(@IS_BOAT_EXCLUDED=1)
		BEGIN
			IF(@STATE_ID=22)
				SET @COVERAGE_CODE_ID = 'UBDW'
			ELSE
				SET @COVERAGE_CODE_ID = ''
		END
		
	END
	ELSE if(UPPER(@CALLED_FOR) = 'VEHICLE')
	BEGIN
		SELECT @IS_BOAT_EXCLUDED=COUNT(IS_EXCLUDED) from APP_UMBRELLA_VEHICLE_INFO
				where   CUSTOMER_ID=@CUSTOMER_ID and
						APP_ID = @APP_POL_ID and
						APP_VERSION_ID = @APP_POL_VERSION_ID and
						IS_ACTIVE = 'Y' and
						IS_EXCLUDED = 10963  --yes
		IF(@IS_BOAT_EXCLUDED=1)
		BEGIN
			IF(@STATE_ID=22)
				SET @COVERAGE_CODE_ID = 'UBEXDAE'
			ELSE
				SET @COVERAGE_CODE_ID = 'UBEXDAE'
		END
	END
END
ELSE
BEGIN
	SELECT @STATE_ID = STATE_ID FROM POL_CUSTOMER_POLICY_LIST where CUSTOMER_ID=@CUSTOMER_ID and
						POLICY_ID = @APP_POL_ID and
						POLICY_VERSION_ID = @APP_POL_VERSION_ID

	if(UPPER(@CALLED_FOR) = 'REC_VEH')
	BEGIN
		SELECT @IS_BOAT_EXCLUDED=COUNT(IS_BOAT_EXCLUDED) from POL_UMBRELLA_RECREATIONAL_VEHICLES 
				where   CUSTOMER_ID=@CUSTOMER_ID and
						POLICY_ID = @APP_POL_ID and
						POLICY_VERSION_ID = @APP_POL_VERSION_ID and
						ACTIVE = 'Y' and
						IS_BOAT_EXCLUDED = 10963  --yes
		IF(@IS_BOAT_EXCLUDED=1)
		BEGIN
			IF(@STATE_ID=22)
				SET @COVERAGE_CODE_ID = 'UBDRMV'
			ELSE
				SET @COVERAGE_CODE_ID = '1048'
		END
			
	END
	ELSE if(UPPER(@CALLED_FOR) = 'BOAT')
	BEGIN
		SELECT @IS_BOAT_EXCLUDED=COUNT(IS_BOAT_EXCLUDED) from POL_UMBRELLA_WATERCRAFT_INFO
				where   CUSTOMER_ID=@CUSTOMER_ID and
						POLICY_ID = @APP_POL_ID and
						POLICY_VERSION_ID = @APP_POL_VERSION_ID and
						IS_ACTIVE = 'Y' and
						IS_BOAT_EXCLUDED = 10963  --yes		
		IF(@IS_BOAT_EXCLUDED=1)
		BEGIN
			IF(@STATE_ID=22)
				SET @COVERAGE_CODE_ID = 'UBDW'
			ELSE
				SET @COVERAGE_CODE_ID = ''
		END
	END
	ELSE if(UPPER(@CALLED_FOR) = 'VEHICLE')
	BEGIN
		SELECT @IS_BOAT_EXCLUDED=COUNT(IS_EXCLUDED) from POL_UMBRELLA_VEHICLE_INFO
				where   CUSTOMER_ID=@CUSTOMER_ID and
						POLICY_ID = @APP_POL_ID and
						POLICY_VERSION_ID = @APP_POL_VERSION_ID and
						IS_ACTIVE = 'Y' and
						IS_EXCLUDED = 10963  --yes		
		IF(@IS_BOAT_EXCLUDED=1)
		BEGIN
			IF(@STATE_ID=22)
				SET @COVERAGE_CODE_ID = 'UBEXDAE'
			ELSE
				SET @COVERAGE_CODE_ID = 'UBEXDAE'
		END
	END
END

IF(UPPER(@CALLED_FOR)='APPLICATION')
BEGIN
	IF(@IS_BOAT_EXCLUDED=1)
	BEGIN		
		begin
		exec Proc_SAVE_UMBRELLA_COVERAGES @CUSTOMER_ID,@APP_POL_ID,@APP_POL_VERSION_ID,null,@COVERAGE_CODE_ID,null,null,null,null
		/*UPDATE POL_UMBRELLA_COVERAGE SET COV_CODE=1
			where   CUSTOMER_ID=@CUSTOMER_ID and
						POLICY_ID = @APP_POL_ID and
						POL_VERSION_ID = @APP_POL_VERSION_ID and
						COV_CODE in (@COVERAGE_CODE_ID)*/
				SET @RETURN_VALUE = 1
		end
	END
END
ELSE
BEGIN

	IF(@IS_BOAT_EXCLUDED=1)
	BEGIN		
		begin				
		exec Proc_SAVE_POL_UMBRELLA_DEFAULT_COVERAGES @CUSTOMER_ID,@APP_POL_ID,@APP_POL_VERSION_ID,null,@COVERAGE_CODE_ID,null,null,null,null
		
		/*UPDATE POL_UMBRELLA_COVERAGE SET COV_CODE=1
			where   CUSTOMER_ID=@CUSTOMER_ID and
						POLICY_ID = @APP_POL_ID and
						POL_VERSION_ID = @APP_POL_VERSION_ID and
						COV_CODE in (@COVERAGE_CODE_ID)*/
				SET @RETURN_VALUE = 1
		end
	END
END		
	RETURN @RETURN_VALUE
end


GO

