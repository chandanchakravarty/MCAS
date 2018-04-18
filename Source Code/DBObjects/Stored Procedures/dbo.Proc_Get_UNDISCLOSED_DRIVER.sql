IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_UNDISCLOSED_DRIVER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_UNDISCLOSED_DRIVER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
proc dbo.Proc_SAVE_UNDISCLOSED_DRIVER
created by   :Pravesh Chandel
Dated	      : 19 Dec 2006
purpose	      : to save UNDISCLOSED Driver Information 		
*/

create procedure dbo.Proc_Get_UNDISCLOSED_DRIVER
(
@CUSTOMER_ID INT ,
@APP_ID INT, 
@APP_VERSION_ID int
)
as
begin

select * from APP_UNDISCLOSED_DRIVER where customer_id=@CUSTOMER_ID and app_id=@APP_ID and app_version_id=@APP_VERSION_ID

end



GO

