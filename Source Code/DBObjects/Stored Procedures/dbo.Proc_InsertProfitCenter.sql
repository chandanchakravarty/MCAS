IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertProfitCenter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertProfitCenter]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_InsertProfitCenter  
Created by      : Priya  
Date            : 3/10/2005  
Purpose         : To add record in profit center table  
Revison History :  
Used In         :   Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--drop proc Proc_InsertProfitCenter
CREATE  PROC dbo.Proc_InsertProfitCenter  
(  
@Code    nvarchar(6)  ,  
@Name    nvarchar(200)  ,  
@Add1      nvarchar(100)  ,  
@Add2      nvarchar(100)  ,  
@City      nvarchar(70)  ,  
@State  nvarchar(10)  ,  
@Zip      nvarchar(11)  ,  
@Country      nvarchar(10)  ,  
@Phone      nvarchar(40)  ,  
@Extension      nvarchar(5)  ,  
@Fax      nvarchar(40)  ,  
@EMail      nvarchar(50)  ,  
@Created_By     int   ,  
@Created_DateTime   datetime  ,  
@ProfitCenterId numeric  = null OUTPUT   
)  
AS  
BEGIN  
 /*Checking whether the code already exists or not  */  
 Declare @Count numeric  
 SELECT @Count = Count(PC_CODE)   
  FROM MNT_PROFIT_CENTER_LIST  
  WHERE PC_CODE = @CODE   
   
 IF @Count >= 1   
 BEGIN  
  /*Record already exist*/  
  SELECT @ProfitCenterId = -1  
 END  
 ELSE  
 BEGIN  
  INSERT INTO MNT_PROFIT_CENTER_LIST(  
   PC_CODE,  
   PC_NAME,  
   PC_ADD1,  
   PC_Add2,  
   PC_CITY,  
   PC_STATE,  
   PC_ZIP,  
   PC_COUNTRY,  
   PC_PHONE,  
   PC_EXT,  
   PC_FAX,  
   PC_EMAIL,  
                        IS_ACTIVE,  
   CREATED_BY,  
   CREATED_DATETIME  
   )  
  VALUES(  
   @Code,  
   @Name,  
   @Add1,  
   @Add2,  
   @City,  
   @State,  
   @Zip,  
   @Country,  
   @Phone,  
   @Extension,  
   @Fax,  
   @EMail,  
                        'Y',  
   @Created_By,  
   @Created_DateTime  
   )  
  SELECT @ProfitCenterId = Max(PC_ID)  
   FROM MNT_PROFIT_CENTER_LIST  
 END  
   
END  
  
  
  





GO

