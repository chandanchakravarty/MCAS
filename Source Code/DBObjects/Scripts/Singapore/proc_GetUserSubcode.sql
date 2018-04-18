/*----------------------------------------------------------                          
Proc Name       : dbo.Proc_GetUserSubCode                 
Created by      : Avijit                       
Date            : 7/12/2011                          
Purpose       : Return the next available sub code                         
Revison History :                          
Used In   : EAW Singapore     TFS 2713
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/                   
-- Proc_GetUsersubcode 
-- drop proc dbo.Proc_GetUsersubcode
CREATE  PROC [dbo].[Proc_GetUserSubCode]                          
AS                          
BEGIN                          
select max(SUB_CODE)+1 from MNT_USER_LIST
END 
