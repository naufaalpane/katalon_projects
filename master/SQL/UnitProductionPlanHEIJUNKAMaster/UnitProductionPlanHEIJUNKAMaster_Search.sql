select * from (
	SELECT ROW_NUMBER() OVER(ORDER BY A.CREATED_DT DESC) AS ROW_NO, 
	A.COMPANY_CD,
	A.LINE_CD,
	A.CFC,
	A.HEIJUNKA_CD,
	A.SUM_SIGN,
	A.PART_NO,
	LEFT(A.PART_NO, 5) AS PART_NO1,
	SUBSTRING(A.PART_NO, 6, 5) AS PART_NO2,
	RIGHT(A.PART_NO, 2) AS PART_NO3,
	A.CREATED_BY,
	CONVERT(varchar, A.CREATED_DT, 111) AS CREATED_DT
	FROM TB_M_UNIT_PROD_HEIJUNKA A
	WHERE 1=1
		and(nullif(@HeijunkaCd,'')is null or A.HEIJUNKA_CD like '%'+@HeijunkaCd+'%')
		and(nullif(@LineCd,'')is null or A.LINE_CD like '%'+@LineCd+'%')
		and(nullif(@Cfc,'')is null or A.CFC like '%'+@Cfc+'%')
) tb where 1 = 1 and tb.ROW_NO between @RowStart and @RowEnd