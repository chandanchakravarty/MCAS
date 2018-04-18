IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_Customer_Id]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_Customer_Id]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran     
--DROP PROC [dbo].[Proc_Process_1099]     
--go
/*----------------------------------------------------------                            
                  
Proc Name     : dbo.Proc_Get_Customer_Id                           
Created by      :Raghav                    
Date            : 15/09/2008                         
Purpose         : To get  Customer_Id 
Revison History :                            
Used In         : Wolverine                            
------------------------------*/
--Drop Proc Proc_Get_Customer_Id
CREATE PROC Proc_Get_Customer_Id
(
@CHECK_ID int
) 
 AS
BEGIN
SELECT CUSTOMER_ID,POLICY_ID,POLICY_VER_TRACKING_ID FROM ACT_CHECK_INFORMATION WHERE CHECK_ID = @CHECK_ID

END



---EXEC Proc_Get_Customer_Id 779


GO

