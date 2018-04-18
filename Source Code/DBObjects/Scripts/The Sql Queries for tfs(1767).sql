--begin transaction abhi
If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=202)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Empresa'
where LOOKUP_UNIQUE_ID=202
end
----
If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=129)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Adicionais do Segurado-Estado ou Subdivisões Políticas-Permissões'
where LOOKUP_UNIQUE_ID=129
end

If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=130)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Adicionais de Cobrança de Pessoas/Entidades'
where LOOKUP_UNIQUE_ID=130
end


If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=131)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Adicionais de Interesse'
where LOOKUP_UNIQUE_ID=131
end



If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10818)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Adicionais do Segurado -Outros Membros do Agregado Familiar'
where LOOKUP_UNIQUE_ID=10818
end

If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10819)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Adicionais do Segurado'
where LOOKUP_UNIQUE_ID=10819
end

If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10820)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Adicionais de Interesse do Locador'
where LOOKUP_UNIQUE_ID=10820
end

If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10821)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Adicionais do Segurado e Credor Hipotecário'
where LOOKUP_UNIQUE_ID=10821
end

If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10822)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Adicionais de Interesse do  Segurado Nomeado'
where LOOKUP_UNIQUE_ID=10822
end

If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10823)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Cobrança Alternativa de Pessoas/Entidades'
where LOOKUP_UNIQUE_ID=10823
end

If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10824)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Adicionais do Segurado Locador'
where LOOKUP_UNIQUE_ID=10824
end

If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10825)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Adicionais do Segurado  Locador  e Perda do Beneficiário'
where LOOKUP_UNIQUE_ID=10825
end

If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10826)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Titular Certificado'
where LOOKUP_UNIQUE_ID=10826
end

If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10827)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Co-Proprietário do Local Segurado'
where LOOKUP_UNIQUE_ID=10827
end

If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10828)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Custódia da Empresa'
where LOOKUP_UNIQUE_ID=10828
end



If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10830)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Adicionais do Segurado  e Penhores do Titular'
where LOOKUP_UNIQUE_ID=10830
end

If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10831)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Perda de Beneficiário e de Adicionais de Interesse'
where LOOKUP_UNIQUE_ID=10831
end

If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10832)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Adicionais de Interesse - Condições de Perda a Pagar'
where LOOKUP_UNIQUE_ID=10832
end

If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10833)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Penhor do Titular'
where LOOKUP_UNIQUE_ID=10833
end


If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10834)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Interesse em Arrendamento de Terra'
where LOOKUP_UNIQUE_ID=10834
end

If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10835)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Perda do Credor a Pagar'
where LOOKUP_UNIQUE_ID=10835
end

If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10836)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Perda do Beneficiário'
where LOOKUP_UNIQUE_ID=10836
end

If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10837)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Locador do Equipamento Alugado'
where LOOKUP_UNIQUE_ID=10837
end

If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10838)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Credor Hipotecário, Atribuido ou Destinatário'
where LOOKUP_UNIQUE_ID=10838
end

If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10839)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Credor Hipotecário'
where LOOKUP_UNIQUE_ID=10839
end

If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10840)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Gerente / Locador'
where LOOKUP_UNIQUE_ID=10840
end

If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10841)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Serviço de  Agência de Hipotecas'
where LOOKUP_UNIQUE_ID=10841
end

If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10842)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Outros'
where LOOKUP_UNIQUE_ID=10842
end

If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10843)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Perda de Beneficiário,Adicionais do Segurado e Interesses Adicionais'
where LOOKUP_UNIQUE_ID=10843
end

If Exists(select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID=10844)
begin
update MNT_LOOKUP_VALUES_MULTILINGUAL
set LOOKUP_VALUE_DESC='Perda de Garantia do Beneficiário'
where LOOKUP_UNIQUE_ID=10844
end

--rollback transaction abhi


























