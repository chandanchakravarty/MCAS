IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DOC_GetTemplates]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DOC_GetTemplates]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                        
Proc Name       : dbo.Proc_DOC_GetTemplates        
Created by      : Deepak Gupta                 
Date            : 09-15-2006                                 
Purpose         : To Get List of Documents        
Revison History :                        
Used In         : Wolverine                        
                       
------------------------------------------------------------                        
Date      Review By          Comments             
03/15/2007 Shailja Rampal    Changed. Removed LOB=0 and AGENCY_ID=0 as per #1238    
  
  
Modified by swarup on 14-jan-2008 for itrack # 1238   
  
If I do not enter in any LOB at the time I am requesting the letter,   
then is should list all template for the template type selected.   
However, if an LOB is entered, it should only show the templates with the matching LOB attached to them.                 
------   ------------       -------------------------*/                        
--drop PROC DBO.Proc_DOC_GetTemplates              
CREATE PROC DBO.Proc_DOC_GetTemplates        
(                        
 @LETTERTYPE  varchar(50),        
 @LOB  int = NULL,        
 @AGENCY  int = NULL        
)                        
AS        
DECLARE @STRSQL VARCHAR(8000)        
BEGIN                        
 SELECT @STRSQL = 'SELECT TEMPLATE_ID,DISPLAYNAME,LOOKUP_VALUE_DESC         
 FROM DOC_TEMPLATE_LIST         
 INNER JOIN MNT_LOOKUP_VALUES ON LOOKUP_UNIQUE_ID=LETTERTYPE        
 WHERE         
  LOOKUP_VALUE_CODE IN (''' + @LETTERTYPE + ''')'  
IF (@LOB<>0)        
 SELECT  @STRSQL = @STRSQL + ' AND (LOB=' + STR(@LOB) +')'    
IF (@AGENCY<>0)       
  SELECT @STRSQL = @STRSQL + ' AND (AGENCY_ID=' + STR(@AGENCY) + ')'        
  SELECT  @STRSQL = @STRSQL +' AND DOC_TEMPLATE_LIST.IS_ACTIVE=''Y''     
  AND DOC_TEMPLATE_LIST.QUERYXML is not null      
 ORDER BY DISPLAYNAME';   
print @STRSQL       
 EXEC(@STRSQL);        
END   
  
GO

