--select * from MNT_UW_QUESTIONS    
  
--  Proc_GetUWQuestions 41  
    
CREATE PROC Proc_GetUWQuestions    
 (    
  @LOB_ID int    
 )    
AS    
BEGIN    
 --select A.QUES_ID as PARENT_QUES_ID,A.PARENT_ID,A.QUES_DESC as PARENT_QUES_DESC,A.QUES_TYPE as PARENT_QUES_TYPE,A.QUES_LEVEL,    
 --B.QUES_ID as CHILD_QUES_ID,B.PARENT_ID as CHILD_ID,B.QUES_DESC as CHILD_QUES_DESC,B.QUES_TYPE as CHILD_QUES_TYPE from    
 --(select * from MNT_UW_QUESTIONS where PARENT_ID IS NULL) A    
 --LEFT JOIN     
 --(select * from MNT_UW_QUESTIONS where PARENT_ID IS NOT NULL) B    
 --ON A.QUES_ID = B.PARENT_ID    
 WITH Ranked ( PARENT_ID, rnk, QUES_DESC )   
             AS ( SELECT PARENT_ID,  
                         ROW_NUMBER() OVER( PARTITION BY PARENT_ID ORDER BY PARENT_ID ),  
                         CAST( QUES_DESC AS VARCHAR(8000) )  
                    FROM MNT_UW_QUESTIONS),  
   AnchorRanked ( PARENT_ID, rnk, QUES_DESC )   
             AS ( SELECT PARENT_ID, rnk, QUES_DESC  
                    FROM Ranked  
                   WHERE rnk = 1 ),  
RecurRanked ( PARENT_ID, rnk, QUES_DESC )  
             AS ( SELECT PARENT_ID, rnk, QUES_DESC  
                    FROM AnchorRanked  
                   UNION ALL  
                  SELECT Ranked.PARENT_ID, Ranked.rnk,  
                         RecurRanked.QUES_DESC + '^ ' + Ranked.QUES_DESC  
                    FROM Ranked  
                   INNER JOIN RecurRanked  
                      ON Ranked.PARENT_ID = RecurRanked.PARENT_ID  
                     AND Ranked.rnk = RecurRanked.rnk + 1 )  
select distinct A.QUES_ID as PARENT_QUES_ID,A.QUES_DESC as PARENT_QUES_DESC,A.QUES_TYPE as PARENT_QUES_TYPE,A.QUES_LEVEL, B.QUES_TYPE+'^'+C.QUES_ANS_TYPE AS QUES_ANS_TYPE  
from  
(  
 select * from MNT_UW_QUESTIONS where PARENT_ID is NULL  
) A  
  
LEFT JOIN  
(  
 select * from MNT_UW_QUESTIONS where PARENT_ID IS NOT NULL  
) B  
  
ON A.QUES_ID = B.PARENT_ID    
  
LEFT JOIN  
(  
  
SELECT PARENT_ID,MAX( QUES_DESC ) AS QUES_ANS_TYPE  
      FROM RecurRanked Where PARENT_ID is not null--not in (select QUES_ID from MNT_UW_QUESTIONS where PARENT_ID is NULL)  
  GROUP BY PARENT_ID  
    
 ) C  
   
 ON  
 A.QUES_ID = C.PARENT_ID  
  
 WHERE A.LOB_ID = @LOB_ID and A.IS_ACTIVE = 'Y'    
    
END  
  
  