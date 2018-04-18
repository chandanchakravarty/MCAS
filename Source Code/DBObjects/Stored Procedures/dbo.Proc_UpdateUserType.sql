IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateUserType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateUserType]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------  
Proc Name       : dbo.Proc_UpdateUserType  
Created by      : Gaurav  
Date            : 3/7/2005  
Purpose         : To add record in User_Types table  
Revison History :  
Used In         :   Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
-- drop proc Proc_UpdateUserType  
CREATE PROC dbo.Proc_UpdateUserType  
(  
@User_Type_Id  smallint,     
@User_Type_Code nvarchar(5),  
@User_Type_Desc nvarchar(25),  
@User_Type_System nchar(1),  
@User_Type_For_Carrier smallint,  
--@Is_Active  nchar(1),  
@Modified_By  int,  
@Last_Updated_DateTime DateTime,  
@SYSTEM_GENERATED_CODE INT  
  
)  
 AS  
BEGIN  
   
 If Exists(SELECT User_Type_Code  
    FROM MNT_USER_TYPES  
    WHERE User_Type_Code = @User_Type_Code AND User_Type_Id <> @User_Type_Id)  
   BEGIN  
    /*Code already exists*/  
   return 0  
   END  
   ELSE  
 BEGIN  
  UPDATE MNT_USER_TYPES  SET  
      User_Type_Code = @User_Type_Code,  
      User_Type_Desc = @User_Type_Desc,  
      User_Type_System = @User_Type_System,  
      --User_Type_For_Carrier = @User_Type_For_Carrier,
      --Is_Active  = @Is_Active,  
      Modified_By  = @Modified_By,  
      Last_Updated_DateTime = @Last_Updated_DateTime,  
      SYSTEM_GENERATED_CODE = @SYSTEM_GENERATED_CODE  
         
    
  WHERE User_Type_Id = @User_Type_Id  
 END  
END  
  
  
 


GO

