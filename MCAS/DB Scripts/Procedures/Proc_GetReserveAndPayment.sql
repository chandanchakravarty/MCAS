
/****** Object:  StoredProcedure [dbo].[Proc_GetReserveAndPayment]    Script Date: 08/14/2014 18:21:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetReserveAndPayment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetReserveAndPayment]
GO


/****** Object:  StoredProcedure [dbo].[Proc_GetReserveAndPayment]    Script Date: 08/14/2014 18:21:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/* 
===========================================================================================================                                                                                            
Proc Name                : dbo.Proc_GetReserveAndPayment
                                                                         
Created by               : Santosh Kumar
                                                                                 
Date                     : 5 Aug 2014                                                                                                                                          
Purpose                  : To Fetch the Data for Reserve, Expense and Payment against a claim id
Revison History          :                                                                                                                                                          
Used In                  : MCAS        
===========================================================================================================                                                                                            
Date     Review By          Comments                                                                                                                                                          
==========================================================================================================                                                                               
exec dbo.Proc_GetReserveAndPayment 112

*/                                                          


CREATE proc [dbo].[Proc_GetReserveAndPayment]
@ClaimID as int
AS                                                                                                                                

           
BEGIN  

Declare @Count1 as int,
		@Count2 as int,
		@Count3 as int,
		@MaxCount as int,
		@totalRecords int,
		@I int
		
DECLARE @DATA AS TABLE (Count1 int)
          

  CREATE TABLE #R (
    R_ID int IDENTITY (1, 1) PRIMARY KEY,
    R_Date Datetime, 
    R_Type nvarchar(10),
    Claimaint nvarchar(200),
    Amount decimal
  )
  
  CREATE TABLE #E (
    E_ID int IDENTITY (1, 1) PRIMARY KEY,
    E_Date Datetime, 
    E_Type nvarchar(10),    
    Claimaint nvarchar(200),
    Amount decimal
  )
    
    CREATE TABLE #P (
    P_ID int IDENTITY (1, 1) PRIMARY KEY,
    P_Date Datetime, 
    P_Type nvarchar(10),
    Claimaint nvarchar(200),
    Amount decimal
  )
  
CREATE TABLE #Final (  
    F_ID int IDENTITY (1, 1) PRIMARY KEY,
    R_Date Datetime, 
    R_Type nvarchar(10),
    R_Claimaint nvarchar(200),
    R_Amount decimal,       
    E_Date Datetime, 
    E_Type nvarchar(10),    
    E_Claimaint nvarchar(200),
    E_Amount decimal,    
    P_Date Datetime, 
    P_Type nvarchar(10),
    P_Claimaint nvarchar(200),
    P_Amount decimal  
)
  
  
  
  /*
  Insert into #E
  Select '','',4,8979.88
  */
  
  Insert into #R
  Select
	CR.CreatedDate,
	case (ISNULL(CR.ReserveType, '1')) when '1' then 'I' else 'M' end ,
	case (ISNULL(CR.ClaimantID, '0')) when '0' then 'OD' else CT.CompanyName end ,
	CR.FinalReserveOrgAmt 	
  From CLM_ClaimReserve CR
  Left join 
  CLM_ThirdParty CT
  on CR.ClaimantID = CT.TPartyId
  Where CR.ClaimId= @ClaimId
  


--Select * From #R


  Insert into #E
  Select
	CP.CreatedDate,
	CP.PaymentType,
	case (ISNULL(CP.ClaimantID, '0')) when '0' then 'OD' else CT.CompanyName end ,
	CP.PayableLocalAmt
  From CLM_ClaimPayment CP
  inner join 
  CLM_ThirdParty CT
  on CP.ClaimantID = CT.TPartyId
  Where CP.ClaimId= @ClaimId
  and 
  PaymentType='Expense'   

  Insert into #P
  Select
	CP.CreatedDate,
	CP.PaymentType,
	case (ISNULL(CP.ClaimantID, '0')) when '0' then 'OD' else CT.CompanyName end ,
	CP.PayableLocalAmt
  From CLM_ClaimPayment CP
  inner join 
  CLM_ThirdParty CT
  on CP.ClaimantID = CT.TPartyId
  Where CP.ClaimId= @ClaimId
  and 
  PaymentType='Reserve'
  
/*
Select * From
  CLM_ClaimPayment
  Where ClaimId=116
*/

--Select * From #P
/*
Select @Count2 = Count(*) From #P
Select @Count3 = Count(*) From #E
*/
insert into @DATA
Select Count(*) From #R
insert into @DATA
Select Count(*) From #P
insert into @DATA
Select Count(*) From #E


Select @MaxCount = Max(Count1) From @DATA


  SELECT
    @I = 1
  SELECT
    @totalRecords = Max(Count1) From @DATA

  WHILE (@I <= @totalRecords)
  BEGIN
    Insert into #Final(R_Claimaint)
	Values(1)
	
	

    /*
    E_Date Datetime, 
    E_Type nvarchar(10),    
    E_Amount decimal,    
    P_Date Datetime, 
    P_Type nvarchar(10),
    P_Claimaint int,
    P_Amount decimal  
*/
	

  
  
		SELECT @I = @I + 1
  END
  
  Update #Final
	Set
    #Final.R_Date=#R.R_Date, 
    #Final.R_Type=#R.R_Type,  
    #Final.R_Claimaint=#R.Claimaint,
    #Final.R_Amount=#R.Amount
    From #Final
    inner join #R
    on #Final.F_ID = #R.R_ID 
    
    
    Update #Final
	Set
    #Final.P_Date=#P.P_Date, 
    #Final.P_Type=#P.P_Type,  
    #Final.P_Claimaint=#P.Claimaint,
    #Final.P_Amount=#P.Amount
    From #Final
    inner join #P
    on #Final.F_ID = #P.P_ID
    
    
    Update #Final
	Set
    #Final.E_Date=#E.E_Date, 
    #Final.E_Type=#E.E_Type, 
    #Final.E_Claimaint=#E.Claimaint,     
    #Final.E_Amount=#E.Amount
    From #Final
    inner join #E
    on #Final.F_ID = #E.E_ID
    

  
  Select 
    F_ID as ReserveId,
    @ClaimId as ClaimID,
    R_Date as CreatedDate, 
    R_Type as ReserveType,
    R_Claimaint as ClaimantID,
    R_Amount as FinalReserveOrgAmt,       
    E_Date as ExpenseDate, 
    E_Type as ExpType,    
    E_Claimaint as E_Claimaint,
    E_Amount as ExpenseAmount,    
    P_Date as PayDate, 
    P_Type as PayType,
    P_Claimaint as PayCLID,
    P_Amount as PayAmount
  from #Final


--dbo.Proc_GetReserveAndPayment 150
  DROP TABLE #R
  DROP TABLE #E
  DROP TABLE #P
  DROP TABLE #Final

End

GO


