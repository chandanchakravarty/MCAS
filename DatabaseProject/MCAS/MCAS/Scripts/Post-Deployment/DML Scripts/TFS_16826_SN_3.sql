IF EXISTS (SELECT * FROM [mnt_template_Master]  WHERE ID in (21,25))
BEGIN
  Update mnt_template_Master set parentId=NULL where ID in (21,25)
END
IF EXISTS (SELECT * FROM [mnt_template_Master]  WHERE ID in (68,69))
BEGIN
  Update mnt_template_Master set OutPutFormat='pdf' where ID in (68,69)
END
IF EXISTS (SELECT * FROM [mnt_template_Master]  WHERE OutPutFormat='pdf')
BEGIN
  Update mnt_template_Master set OutPutFormat='PDF' where OutPutFormat='pdf'
END
IF EXISTS (SELECT * FROM [mnt_template_Master]  WHERE OutPutFormat='docx')
BEGIN
  Update mnt_template_Master set OutPutFormat='WORD' where OutPutFormat='docx'
END