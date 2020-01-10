USE [LootLoOnlineDatabase]
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_OfferProducts_Search_Paging_Sorting]    Script Date: 10-01-2020 10:26:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SP_GET_OfferProducts_Search_Paging_Sorting]
(
    -- Optional Filters for Dynamic Search
	 @searchText       NVARCHAR(250) = NULL, 
	 @ProductBrand varchar(100)  = NULL, -- Search option ProductBrand 
	 @Attributes varchar(200)  = NULL, -- Search option Attributes
	 @CategoryId int = 0, -- Search option Attributes
	 @MacId  NVARCHAR(250) = NULL,
    -- Pagination
    @PageNbr            INT = 1,
    @PageSize           INT = 100,
    -- Sort Details
    @SortCol            NVARCHAR(20) = ''
)
AS
BEGIN
SET NOCOUNT ON
SET FMTONLY OFF
    DECLARE
        @ltitle    NVARCHAR(250), 
		@lproductBrand NVARCHAR(250),
        @lattributes  NVARCHAR(250),
		@lcategoryId INT,
		@lmacId  NVARCHAR(250)
     
    DECLARE
        @lPageNbr   INT,
        @lPageSize  INT,
        @lSortCol   NVARCHAR(20),
        @lFirstRec  INT,
        @lLastRec   INT,
        @lTotalRows INT

	DECLARE @tempCategory TABLE
		(
			Id INT,
			ParentID INT,
			Name VARCHAR(255),
			Description VARCHAR(255),
			SiteName VARCHAR(255)
		);
 
    SET @ltitle         = LTRIM(RTRIM(@searchText))
	SET @lproductBrand         = LTRIM(RTRIM(@ProductBrand))
    SET @lattributes         = LTRIM(RTRIM(@Attributes))
	SET @lcategoryId         = LTRIM(RTRIM(@CategoryId))
	SET @lmacId         = LTRIM(RTRIM(@MacId))
   
 
    SET @lPageNbr   = @PageNbr
    SET @lPageSize  = @PageSize
    SET @lSortCol   = LTRIM(RTRIM(@SortCol))
     
    SET @lFirstRec  = ( @lPageNbr - 1 ) * @lPageSize
    SET @lLastRec   = ( @lPageNbr * @lPageSize + 1 ) 
    SET @lTotalRows = @lFirstRec - @lLastRec + 1

 IF @lcategoryId=0
 	BEGIN
		IF EXISTS(SELECT MacId FROM [User].[VisitedUsers] WHERE MacId like '%'+@lmacId+'%')
			BEGIN
				SELECT * INTO #tempVisitedUser FROM [User].[VisitedUsers] vu WHERE vu.MacId like '%'+@lmacId+'%' order by vu.CreatedDate desc
				
				 DECLARE @CatagoryID INT
				 DECLARE @ProductId NVARCHAR(250)

				DECLARE VisitedUser_cursor CURSOR FOR     
					SELECT CatagoryID,ProductId   
					FROM #tempVisitedUser  
					order by CreatedDate; 

				OPEN VisitedUser_cursor 
					FETCH NEXT FROM VisitedUser_cursor     
					INTO @CatagoryID,@ProductId    
					print 'Started'       
					WHILE @@FETCH_STATUS = 0    
					BEGIN   

						INSERT @tempCategory  Exec [GetParentChildCategories] @CatagoryID;

						FETCH NEXT FROM VisitedUser_cursor     
						INTO @CatagoryID,@ProductId      
					END     
				CLOSE VisitedUser_cursor;    
				DEALLOCATE VisitedUser_cursor;
			END
		ELSE
			BEGIN
					--INSERT @tempCategory  Exec [GetParentChildCategories] @lcategoryId;
					INSERT INTO @tempCategory SELECT ID,ParentId,Name,Description,SiteName FROM [Admin].[Category]
			END
	END
ELSE
	BEGIN
		INSERT @tempCategory  Exec [GetParentChildCategories] @lcategoryId;
	END


	SELECT op.* ,
		ROW_NUMBER() OVER 
			(PARTITION BY op.CategoryId ORDER BY op.discountPercentage DESC) AS CatCount
	INTO #TempOfferProducts 
	FROM Offer.OfferProducts op
	--INNER JOIN @tempCategory tc on op.CategoryId=tc.Id
	WHERE op.CategoryId in (select ID from @tempCategory) AND
		  (@ltitle IS NULL OR op.title LIKE '%' + @ltitle + '%') AND
		  (@lproductBrand IS NULL OR op.productBrand LIKE '%' + @lproductBrand + '%')AND
		  (@lattributes IS NULL OR op.attributes LIKE '%' + @lattributes + '%')AND 
		  op.inStock != 0
		  order by op.discountPercentage DESC;

    ; WITH CTE_Results
    AS (
        SELECT distinct ROW_NUMBER() OVER 
		(ORDER BY
            CASE WHEN @lSortCol = 'title_Asc' THEN op.title
                END ASC,
            CASE WHEN @lSortCol = 'title_Desc' THEN op.title
                END DESC, 
			CASE WHEN @lSortCol = 'productBrand_Asc' THEN op.productBrand
                END ASC,
            CASE WHEN @lSortCol = 'productBrand_Desc' THEN op.productBrand
                END DESC, 
             CASE WHEN @lSortCol = 'attributes_Asc' THEN op.attributes
                END ASC,
            CASE WHEN @lSortCol = 'attributes_Desc' THEN op.attributes
                END DESC
            ) AS ROWNUM,
			COUNT (*) OVER () AS TotalOffers,
            op.productId,op.categoryPath,op.title,op.productDescription,
			op.productUrl,op.imageUrls_200,op.imageUrls_400,op.imageUrls_800,op.currency,op.SellingPrice,op.SpecialPrice,
			op.maximumRetailPrice,op.discountPercentage,op.offers,op.attributes,op.productBrand,op.shippingCharges,
			op.estimatedDeliveryTime,op.sellerName,op.sellerAverageRating,op.sellerNoOfRatings,op.sellerNoOfReviews,
			op.keySpecs,op.booksInfo,op.CategoryId,op.CreatedDate,op.SiteName
			from #TempOfferProducts op WITH(NOLOCK)
			
			WHERE 
				@CatagoryId >0 OR (@CategoryId=0 AND op.CatCount=1)
	   )
	

    SELECT distinct
         op.productId,op.categoryPath,op.title,op.productDescription,op.productUrl,op.imageUrls_200,op.imageUrls_400,op.imageUrls_800,op.currency,op.SellingPrice,op.SpecialPrice,
		 op.maximumRetailPrice,op.discountPercentage,op.offers,op.attributes,op.productBrand,op.shippingCharges,
		 op.estimatedDeliveryTime,op.sellerName,op.sellerAverageRating,op.sellerNoOfRatings,op.sellerNoOfReviews,
		 op.keySpecs,op.booksInfo,op.CategoryId,op.CreatedDate,op.SiteName,
		 op.ROWNUM,op.TotalOffers
    FROM CTE_Results AS op
    WHERE
        op.ROWNUM > @lFirstRec 
    AND op.ROWNUM < @lLastRec
	--GROUP BY op.productId,op.categoryPath,op.title,op.productDescription,op.productUrl,op.imageUrls_200,op.imageUrls_400,op.imageUrls_800,op.currency,op.SellingPrice,op.SpecialPrice,
	--		op.maximumRetailPrice,op.discountPercentage,op.offers,op.attributes,op.productBrand,op.ROWNUM,op.TotalOffers
    ORDER BY   op.ROWNUM ASC
   
   SET NOCOUNT OFF
END
