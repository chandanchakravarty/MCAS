
Alter PROC dbo.Proc_Add_MNT_CURRENCY_MASTER (    
 @CURRENCY_ID int out,
 @CURR_CODE nvarchar(6) = null,    
 @CURR_DESC nvarchar(50) = null ,    
 @CURR_SYMBOL nvarchar(4)= null)    
     
AS    
    
DECLARE @NEWCURRID int    
    
IF not exists (select * from MNT_CURRENCY_MASTER where CURR_CODE = @CURR_CODE)    
BEGIN    
    
 SELECT @CURRENCY_ID = MAX(CURRENCY_ID) + 1 from MNT_CURRENCY_MASTER    
    
 INSERT INTO [dbo].[MNT_CURRENCY_MASTER]    
           ([CURRENCY_ID]    
           ,[CURR_CODE]    
           ,[CURR_DESC]    
           ,[CURR_SYMBOL]    
           ,[CURR_PRECISION]    
           ,[CURR_CALCULATEFORMAT]    
           ,[CURR_PRINTFORMAT]    
           ,[CURR_CHECKDIGITS]    
           ,[CURR_CHECKDECIMAL]    
           ,[CURR_DECIMALSEPR]    
           ,[CURR_THOUSANDSEPR]    
           ,[IS_ACTIVE]    
           ,[CREATED_BY]    
           ,[CREATED_DATETIME]    
           ,[MODIFIED_BY]    
           ,[LAST_UPDATED_DATETIME])    
     VALUES    
           (@CURRENCY_ID 
           ,@CURR_CODE    
           ,@CURR_DESC    
           ,@CURR_SYMBOL    
           ,2    
           ,'000000000.00'    
           ,'000.000.000.00'    
           ,'Dollars'    
           ,'Cents'    
           ,'.'    
           ,','    
           ,'Y'    
           ,NULL    
           ,GETDATE()    
           ,NULL    
           ,NULL)    
    
END 