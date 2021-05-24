SELECT count(1)
FROM TB_M_UNIT_PROD_HEIJUNKA A
	WHERE 1=1
		and(nullif(@HeijunkaCd,'')is null or A.HEIJUNKA_CD like '%'+@HeijunkaCd+'%')
		and(nullif(@LineCd,'')is null or A.LINE_CD like '%'+@LineCd+'%')
		and(nullif(@Cfc,'')is null or A.CFC like '%'+@Cfc+'%')