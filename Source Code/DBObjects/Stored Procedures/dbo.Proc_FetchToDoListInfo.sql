IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchToDoListInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchToDoListInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran  
--DROP PROC Dbo.Proc_FetchToDoListInfo  
--go  
/*----------------------------------------------------------                    
Proc Name   : dbo.Proc_FetchToDoListInfo                    
Created by       : Sibin Philip                   
Date             : 21 Oct 09                    
Purpose    : Retrieving data from todolist table                    
Revison History  :                    
Used In    : Wolverine     
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/                    
--DROP PROC Dbo.Proc_FetchToDoListInfo  196704              
CREATE PROC [dbo].[Proc_FetchToDoListInfo]                 
@LIST_ID VARCHAR(1000)                
                    
AS   
  
declare @TEMPSTR NVARCHAR(3000)  
  
BEGIN    
    
  SET @TEMPSTR = N'SELECT  T.LISTID,                                      
  TT.TYPEDESC,                  
  ISNULL(T.CUSTOMER_ID,0) AS CUSTOMER_ID,                    
  ISNULL(A.APP_ID,0) AS APP_ID,ISNULL(A.APP_VERSION_ID,0) AS APP_VERSION_ID,                    
  ISNULL(P.POLICY_ID,0) AS POLICY_ID,ISNULL(P.POLICY_VERSION_ID,0) AS POLICY_VERSION_ID,  
  RTRIM(RTRIM(C.CUSTOMER_FIRST_NAME + '' ''+     
  CASE WHEN ISNULL(C.CUSTOMER_MIDDLE_NAME ,'''')='''' THEN '''' ELSE ISNULL(C.CUSTOMER_MIDDLE_NAME + '' '','''') END  +    
  isnull(C.CUSTOMER_LAST_NAME, ''''))) AS CUSTOMER_NAME           
 FROM  TODOLIST T                    
 LEFT OUTER JOIN MNT_USER_LIST UL ON UL.USER_ID = T.FROMUSERID                    
 LEFT OUTER JOIN TODOLISTTYPES TT ON TT.TYPEID = T.LISTTYPEID                    
 LEFT OUTER JOIN CLT_CUSTOMER_LIST C ON C.CUSTOMER_ID = T.CUSTOMER_ID                    
 LEFT OUTER JOIN APP_LIST A ON A.APP_ID = T.APP_ID AND A.APP_VERSION_ID = T.APP_VERSION_ID                    
  AND A.CUSTOMER_ID = T.CUSTOMER_ID                    
 LEFT OUTER JOIN POL_CUSTOMER_POLICY_LIST P ON P.POLICY_ID = T.POLICY_ID AND P.POLICY_VERSION_ID = T.POLICY_VERSION_ID                    
  AND P.CUSTOMER_ID = T.CUSTOMER_ID'  
   
 SET @TEMPSTR = @TEMPSTR + ' WHERE ' + @LIST_ID  
   exec sp_executesql @TEMPSTR    
  
             
END                 
  
--go  
--exec Proc_FetchToDoListInfo '(LISTID=174695)'  
--rollback tran  
  
GO

