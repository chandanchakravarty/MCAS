CREATE PROCEDURE [dbo].[Proc_VisionInsertTable]
	@tableName [varchar](100),
	@FieldsName [varchar](8000),
	@FieldsValue [varchar](8000)
WITH EXECUTE AS CALLER
AS
declare @strSql   varchar(8000)        
declare @strFields  varchar(8000)        
declare @strValues   varchar(8000)        
declare @intLenName     numeric        
declare @intLenValues    numeric        
declare @intFirstRowValueEndPos numeric        
declare @intFirstRowFieldEndPos numeric        
Begin        
        
  select @intLenName = len(@FieldsName)        
 select @intLenValues= len(@FieldsValue)        
 while (@intLenName > 0)         
 Begin        
  PRINT '@FieldsName ' +  @FieldsName        
  select @intFirstRowFieldEndPos = charindex('##', @FieldsName, 0)        
  PRINT '@intFirstRowFieldEndPos ' +  convert(nvarchar,@intFirstRowFieldEndPos)        
  if @intFirstRowFieldEndPos = 0        
  begin        
   print 'start @strValues ' + @strValues        
   select @strFields = @FieldsName        
   select @strValues = '''' + @FieldsValue          
   select @intLenName = 0        
  end         
  else        
  BEGIN        
   select @strFields = left(@FieldsName, @intFirstRowFieldEndPos - 1 )        
   select @FieldsName = right(@FieldsName, @intLenName - (@intFirstRowFieldEndPos - 1) -4 )        
   select @intLenName = len(@FieldsName)        
           
   select @intFirstRowValueEndPos = charindex('##', @FieldsValue, 0)        
   select @strValues = left(@FieldsValue, @intFirstRowValueEndPos - 1 )        
   select @FieldsValue = right(@FieldsValue, @intLenValues - (@intFirstRowValueEndPos - 1) -4 )        
   select @intLenValues= len(@FieldsValue)        
  END        
  select @strFields = replace(@strFields, '~*', ',')        
  select @strFields = left(@strFields,len(@strFields) -1 )     
  select @strValues = replace( ltrim(rtrim(@strValues)), char(39), char(39) + char(39))    
  
  --PRINT ' @FieldsValue === ' + @FieldsValue        
  PRINT ' 1 @strValues === ' + @strValues        
  --select @strValues  = replace(@strValues, char(39), char(39))        
  if(CHARINDEX('convert',@strValues) > 0)          
   Begin        
    --select @strValues  = replace(@strValues, char(39), char(39))        
    select @strValues  = replace(@strValues, '~*', ''',''')        
   End        
    else        
   Begin        
     --select @strValues  = replace(@strValues, char(39), char(39) + char(39))        
     select @strValues  = replace(@strValues, '~*', ''',''')         
   End        
  PRINT ' Befroe @strValues === ' +substring(@strValues, 2, Len(@strValues) )       
  
  Select @strValues =Replace(@strValues,'convert(datetime,''','convert(datetime,')  
  Select @strValues =Replace(@strValues,''',103',',103')   
  Select @strValues = substring(@strValues, 2, Len(@strValues) )   
  
--Select @strValues = left(@strValues,len(@strValues) - 3 )        
  
  select @strValues = left(@strValues,len(@strValues) - 2 )        
  PRINT ' @strValues === ' + @strValues         
  select @strValues  = replace(@strValues, '''convert', 'convert')        
  select @strValues  = replace(@strValues, '103)''', '103)')        
          
  --select @strSql = 'insert into ' + @tableName + ' (' + @strFields + ') values(''' + @strValues  + ''')'        
  select @strSql = 'insert into ' + @tableName + ' (' + @strFields + ') values(' + @strValues  + ')'        
  print  @strSql        
  execute (@strSql)        
 End        
end


