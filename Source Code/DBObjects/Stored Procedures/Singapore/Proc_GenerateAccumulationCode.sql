  
  
/*------------------------------------------------------------------------------------------------------------------    
Proc Name          : Dbo.Proc_GenerateAccumulationCode    
Created by           : Ruchika Chauhan    
Date                    : 7-March-2012    
Purpose               : To generate AccumulationCode on the basis of Accumulation ID from MNT_ACCUMULATION_REFERENCE  
Revison History :    
Used In                :   Singapore Implementation    
---------------------------------------------------------------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -----------------------------------------------------------------------------------------*/    
  
CREATE PROC Dbo.Proc_GenerateAccumulationCode  
 (  
 @Acc_id INT    
 )  
AS    
BEGIN    
 DECLARE @CRITERIA_CODE NVARCHAR(25),  
 @MAXROWS INT,  
 @ACC_REF_NO NVARCHAR(25)
    
 SELECT @CRITERIA_CODE = CRITERIA_CODE FROM MNT_ACCUMULATION_CRITERIA_MASTER   
 WHERE CRITERIA_ID=(SELECT CRITERIA_ID FROM MNT_ACCUMULATION_REFERENCE WHERE ACC_ID=@Acc_id)  
  
 SELECT @ACC_REF_NO=ACC_REF_NO FROM MNT_ACCUMULATION_REFERENCE WHERE ACC_ID=@Acc_id 
 SELECT @MAXROWS=COUNT(*) + 1 FROM POL_ACCUMULATION_DETAILS  
  
 SELECT @ACC_REF_NO + @CRITERIA_CODE + CAST(@MAXROWS AS NVARCHAR(25)) as 'Acc_Code'  
 
END  
  
--declare @ACC_CODE nvarchar(25)  
--exec Proc_GenerateAccumulationCode 1, @ACC_CODE out  
--print @ACC_CODE   
--drop proc Proc_GenerateAccumulationCode



