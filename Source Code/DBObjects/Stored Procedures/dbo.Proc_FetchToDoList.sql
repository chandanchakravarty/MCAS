IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchToDoList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchToDoList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*----------------------------------------------------------                    
Proc Name      : dbo.Proc_FetchToDoList                    
Created by       : Anurag Verma                    
Date             : 5/5/2005                    
Purpose       : retrieving data from todolist table                    
Revison History :                    
Used In        : Wolverine                    
                
Modified By : Anurag Verma                
Modified On : 15/03/2007                
Purpose  : Adding module_id column in select clause            
      
      
Reviewed By : Anurag Verma      
Reviewed On : 06-07-2007          
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/                    
--DROP PROC Dbo.Proc_FetchToDoList 13393                  
CREATE PROC [dbo].[Proc_FetchToDoList]                    
@LIST_ID INT                    
                    
AS                    
                    
BEGIN                    
 SELECT  T.LISTID,                     
  T.TOUSERID,                     
  T.FROMUSERID,                     
  T.RECDATE,                     
  T.FOLLOWUPDATE,                     
  T.LISTTYPEID,                    
  T.SUBJECTLINE,                     
  T.NOTE,                     
  T.SYSTEMFOLLOWUPID,                     
  T.PRIORITY,                     
  datepart(hh,T.STARTTIME) as STARTTIMEHOUR,                     
  datepart(mi,T.STARTTIME) as STARTTIMEMINUTE,                     
  datepart(hh,T.ENDTIME) as ENDTIMEHOUR,                     
  datepart(mi,T.ENDTIME) as ENDTIMEMINUTE,    
  T.STARTTIME,T.ENDTIME,                    
  isnull(UL.USER_TITLE,'')+' '+isnull(UL.USER_FNAME,'')+' '+isnull(UL.USER_LNAME,'')  as FROMUSERNAME,                    
  T.LISTOPEN LISTOPEN,                    
  TT.TYPEDESC,                    
  'LISTID='+cast(T.LISTID as varchar(8000)) as UniqueGrdId,                    
  T.CUSTOMER_ID,                    
  RTRIM(RTRIM(C.CUSTOMER_FIRST_NAME + ' ' +   
  CASE WHEN ISNULL(C.CUSTOMER_MIDDLE_NAME ,'')='' THEN '' ELSE ISNULL(C.CUSTOMER_MIDDLE_NAME + ' ','') END  +  
  isnull(C.CUSTOMER_LAST_NAME, ''))) AS CUSTOMER_NAME,                    
  A.APP_ID, A.APP_VERSION_ID, A.APP_NUMBER,                    
  P.POLICY_ID,P.POLICY_VERSION_ID,P.POLICY_NUMBER, CCI.CLAIM_NUMBER, CCI.LOB_ID, CCI.CLAIM_ID,                
  ISNULL(T.RULES_VERIFIED,0) AS RULES_VERIFIED,ISNULL(MODULE_ID,0) MODULE_ID,             
 QQ.QQ_NUMBER as QUOTE_NUMBER ,QQ.QQ_ID as QUOTEID              
 FROM  TODOLIST T                    
 LEFT OUTER JOIN MNT_USER_LIST UL ON UL.USER_ID = T.FROMUSERID                    
 LEFT OUTER JOIN TODOLISTTYPES TT ON TT.TYPEID = T.LISTTYPEID                    
 LEFT OUTER JOIN CLT_CUSTOMER_LIST C ON C.CUSTOMER_ID = T.CUSTOMER_ID                    
 LEFT OUTER JOIN APP_LIST A ON A.APP_ID = T.APP_ID AND A.APP_VERSION_ID = T.APP_VERSION_ID                    
  AND A.CUSTOMER_ID = T.CUSTOMER_ID                    
 LEFT OUTER JOIN POL_CUSTOMER_POLICY_LIST P ON P.POLICY_ID = T.POLICY_ID AND P.POLICY_VERSION_ID = T.POLICY_VERSION_ID                    
  AND P.CUSTOMER_ID = T.CUSTOMER_ID                    
 LEFT OUTER JOIN CLM_CLAIM_INFO CCI ON ISNULL(CCI.CLAIM_ID,0) = ISNULL(T.CLAIMID,0)                  
 LEFT  JOIN CLT_QUICKQUOTE_LIST QQ ON QQ.QQ_ID = T.QUOTEID AND T.CUSTOMER_ID = QQ.CUSTOMER_ID                 
--  AND QQ.APP_ID = T.APP_ID AND QQ.APP_VERSION_ID = T.APP_VERSION_ID              
 WHERE  LISTID=@LIST_ID                    
                    
END                 

GO

