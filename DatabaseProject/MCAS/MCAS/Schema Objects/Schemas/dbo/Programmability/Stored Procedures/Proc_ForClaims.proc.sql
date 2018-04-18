CREATE PROCEDURE [dbo].[Proc_ForClaims]
	@ACCIDENTCLAIM_ID [int]
WITH EXECUTE AS CALLER
AS
BEGIN            



DECLARE @DATA AS TABLE (xml_DATA XML)
DECLARE @MDATA AS TABLE (Mxml_DATA XML)
Declare @ClaimId int 

Select @ClaimId = ClaimId From CLM_Claims  where AccidentClaimId = @ACCIDENTCLAIM_ID 


     INSERT INTO @DATA       
    
 SELECT ISNULL((        
    
    SELECT * 
    FROM ClaimAccidentDetails where AccidentClaimId = @ACCIDENTCLAIM_ID 
    
    FOR XML Auto, Elements
    ), '<ClaimAccidentDetails></ClaimAccidentDetails>')    

        

 INSERT INTO @DATA       
    
 SELECT ISNULL((        
    
    SELECT *
    FROM CLM_Claims Claims where AccidentClaimId = @ACCIDENTCLAIM_ID 
    
    FOR XML Auto, Elements
    ), '<Claims></Claims>')     


 INSERT INTO @DATA       
    
 SELECT ISNULL((        
    
    SELECT *
    FROM CLM_ThirdParty where ClaimId = @ClaimId 
    
    FOR XML Auto, Elements
    ), '<ThirdParty></ThirdParty>')     
 
 
 INSERT INTO @DATA       
    
 SELECT ISNULL((        
    
--    SELECT isNull(ClaimId, '') 'ClaimItttttd'   
    SELECT *    
    FROM CLM_ClaimReserve  where ClaimId = @ClaimId 
    
    FOR XML Auto, Elements
    ), '<CLM_ClaimReserve></CLM_ClaimReserve>')    
    

 INSERT INTO @DATA       
    
 SELECT ISNULL((        
    
--    SELECT isNull(ClaimId, '') 'ClaimItttttd'   
    SELECT *    
    FROM CLM_ClaimPayment  where ClaimId = @ClaimId 
    
    FOR XML Auto, Elements
    ), '<CLM_ClaimPayment></CLM_ClaimPayment>')    
    
    
    
    /*
    
    
    PATH('CLAIM')        
    
     ,TYPE        
    
     ,ROOT('Claims')        
    
    ), '<Claims></Claims>')    
    */
    
    
    
         INSERT INTO @MDATA       
    
 SELECT ISNULL((        
 
 Select xml_DATA as 'Node' from @DATA for XML PATH('')        
    
     ,TYPE        

    
--    SELECT * 
  --  FROM ClaimAccidentDetails where AccidentClaimId = 105 
    
    --FOR XML Auto
    ), '<Accident></Accident>')    
    
Select Mxml_DATA as 'INPUTXML' from @MDATA for XML PATH('')        
    
     ,TYPE        
    

    
    
/*
    
Select xml_DATA as 'Data' from @DATA for XML PATH('')        
    
     ,TYPE        
    
     ,ROOT('Master')     
     */

End


