    
 /*----------------------------------------------------------                
Proc Name   : dbo.[Proc_FetchRuleXmlData]             
CREATED BY  : Naveen Pujari           
CREATED DATE: Sept 16, 2011             
Purpose     : This Procedure is used to fetch  the top 50 records  and create a log for each record combination  in a log file.           
Revison History :                
Used In      : Ebix Advantage Web                
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/          
--DROP PROC [Proc_FetchRuleXmlData]         
CREATE PROC [dbo].[Proc_FetchRuleXmlData]               
          
AS                
BEGIN         
  select top 50 * from ruletest   with(nolock) 
                 
END                
                
                
              
            
          
     
      