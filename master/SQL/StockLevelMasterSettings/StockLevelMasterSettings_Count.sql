select count(1)
FROM TB_M_STOCK_LEVEL A 
	JOIN TB_M_UNIT_PROD B ON A.LINE_CD = B.LINE_CD AND A.PART_NO = B.PART_NO AND A.CFC = B.CFC AND A.STATUS_CD = B.STATUS_CD
WHERE 1=1
	and(nullif(@LineCd,'')is null or A.LINE_CD like '%'+@LineCd+'%')
	and(nullif(@Cfc,'')is null or A.CFC like '%'+@Cfc+'%')
	and(nullif(@StatusCd,'')is null or A.STATUS_CD like '%'+@StatusCd+'%')
	and(nullif(@processCd,'')is null or SUBSTRING(A.LINE_CD, 5, 1) like '%'+@processCd+'%')
	and(nullif(@PartNo1,'')is null or LEFT(A.PART_NO, 5) like '%'+@PartNo1+'%')
	and(nullif(@PartNo2,'')is null or SUBSTRING(A.PART_NO, 6, 5) like '%'+@PartNo2+'%')
	and(nullif(@PartNo3,'')is null or RIGHT(A.PART_NO, 2) like '%'+@PartNo3+'%')