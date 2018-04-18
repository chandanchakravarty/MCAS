IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAPP_PKG_LOB_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAPP_PKG_LOB_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.APP_PKG_LOB_DETAILS  
Created by      : Ajit Singh Chahal  
Date            : 4/28/2005  
Purpose       :To Update records in APP_PKG_LOB_DETAILS.  
Revison History :  
Used In        : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--drop proc Proc_UpdateAPP_PKG_LOB_DETAILS  
CREATE   PROC Dbo.Proc_UpdateAPP_PKG_LOB_DETAILS  
(  
@REC_ID     int,  
@CUSTOMER_ID     int,  
@APP_ID     int,  
@APP_VERSION_ID     smallint,  
@LOB     nvarchar(10),  
@SUB_LOB     nvarchar(10)  
)  
AS  
Begin  
  
declare @count int  
  
select @count=count(*) from APP_PKG_LOB_DETAILS   
where CUSTOMER_ID=@CUSTOMER_ID and APP_VERSION_ID=@APP_VERSION_ID and APP_ID=@APP_ID and LOB = @LOB and SUB_LOB = @SUB_LOB and REC_ID <> @REC_ID  
  
if(@count<=0)  
BEGIN  
 update  APP_PKG_LOB_DETAILS set  
  LOB=@LOB,  
  SUB_LOB=@SUB_LOB  
  where REC_ID = @REC_ID  AND
	CUSTOMER_ID=@CUSTOMER_ID   AND
	  APP_ID=@APP_ID 	AND  
	  APP_VERSION_ID=@APP_VERSION_ID 
 END  
  
end  



GO

