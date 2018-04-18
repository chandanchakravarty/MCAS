IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SAVE_UNDISCLOSED_DRIVER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SAVE_UNDISCLOSED_DRIVER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
proc dbo.Proc_SAVE_UNDISCLOSED_DRIVER
created by   :Pravesh Chandel
Dated	      : 13 Dec 2006
purpose	      : to save UNDISCLOSED Driver Information 		
drop proc dbo.Proc_SAVE_UNDISCLOSED_DRIVER
*/

create procedure dbo.Proc_SAVE_UNDISCLOSED_DRIVER
(
@CUSTOMER_ID INT ,
@APP_ID INT, 
@APP_VERSION_ID int,
@DRIVER_ID      INT,
@UNDISCLOSED_DRIVER_ID INT=null,
@DRIVER_NAME VARCHAR(30)
)
as
begin
if not Exists(select * from APP_UNDISCLOSED_DRIVER where CUSTOMER_ID =@CUSTOMER_ID and APP_ID  =@APP_ID   
and APP_VERSION_ID =@APP_VERSION_ID
and DRIVER_ID =@DRIVER_ID
and DRIVER_NAME =@DRIVER_NAME

	)

begin
	DECLARE @TEMP_ID INT
	SELECT @TEMP_ID=ISNULL(MAX(UNDISCLOSED_DRIVER_ID),0) +1  FROM APP_UNDISCLOSED_DRIVER
	
	insert into APP_UNDISCLOSED_DRIVER
	(
	CUSTOMER_ID ,
	APP_ID , 
	APP_VERSION_ID ,
	DRIVER_ID    ,
	UNDISCLOSED_DRIVER_ID ,
	DRIVER_NAME 
	)
	values
	(
	@CUSTOMER_ID ,
	@APP_ID , 
	@APP_VERSION_ID ,
	@DRIVER_ID  ,
	@TEMP_ID,
	@DRIVER_NAME 
)
end
end




GO

