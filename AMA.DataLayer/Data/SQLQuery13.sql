USE [LootLoOnlineDatabase]
GO
/****** Object:  Schema [Admin]    Script Date: 29-11-2019 12:40:12 ******/
CREATE SCHEMA [Admin]
GO
/****** Object:  Schema [Flipkart]    Script Date: 29-11-2019 12:40:12 ******/
CREATE SCHEMA [Flipkart]
GO
/****** Object:  Schema [Offer]    Script Date: 29-11-2019 12:40:12 ******/
CREATE SCHEMA [Offer]
GO
/****** Object:  Schema [User]    Script Date: 29-11-2019 12:40:12 ******/
CREATE SCHEMA [User]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_ConvertToDateTime]    Script Date: 29-11-2019 12:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_ConvertToDateTime] (@Datetime BIGINT)
RETURNS DATETIME
AS
BEGIN
    DECLARE @LocalTimeOffset BIGINT
           ,@AdjustedLocalDatetime BIGINT;
    SET @LocalTimeOffset = DATEDIFF(second,GETDATE(),GETUTCDATE())
    SET @AdjustedLocalDatetime = @Datetime - @LocalTimeOffset
    RETURN (SELECT DATEADD(second,@AdjustedLocalDatetime, CAST('1970-01-01 00:00:00' AS datetime)))
END;

GO
/****** Object:  UserDefinedFunction [dbo].[fn_split_string_to_column]    Script Date: 29-11-2019 12:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_split_string_to_column] (
    @string NVARCHAR(MAX),
    @delimiter CHAR(1)
    )
RETURNS @out_put TABLE (
    [column_id] INT IDENTITY(1, 1) NOT NULL,
    [value] NVARCHAR(MAX)
    )
AS
BEGIN
    DECLARE @value NVARCHAR(MAX),
        @pos INT = 0,
        @len INT = 0

    SET @string = CASE 
            WHEN RIGHT(@string, 1) != @delimiter
                THEN @string + @delimiter
            ELSE @string
            END

    WHILE CHARINDEX(@delimiter, @string, @pos + 1) > 0
    BEGIN
        SET @len = CHARINDEX(@delimiter, @string, @pos + 1) - @pos
        SET @value = SUBSTRING(@string, @pos, @len)

        INSERT INTO @out_put ([value])
        SELECT LTRIM(RTRIM(@value)) AS [column]

        SET @pos = CHARINDEX(@delimiter, @string, @pos + @len) + 1
    END

    RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[Split]    Script Date: 29-11-2019 12:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[Split]
(
    @Line nvarchar(MAX),
    @SplitOn nvarchar(5) = ','
)
RETURNS @RtnValue table
(
    Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY CLUSTERED,
    Data nvarchar(100) NOT NULL
)
AS
BEGIN
    IF @Line IS NULL RETURN

    DECLARE @split_on_len INT = LEN(@SplitOn)
    DECLARE @start_at INT = 1
    DECLARE @end_at INT
    DECLARE @data_len INT

    WHILE 1=1
    BEGIN
        SET @end_at = CHARINDEX(@SplitOn,@Line,@start_at)
        SET @data_len = CASE @end_at WHEN 0 THEN LEN(@Line) ELSE @end_at-@start_at END
        INSERT INTO @RtnValue (data) VALUES( SUBSTRING(@Line,@start_at,@data_len) );
        IF @end_at = 0 BREAK;
        SET @start_at = @end_at + @split_on_len
    END

    RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[SplitString]    Script Date: 29-11-2019 12:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[SplitString] (
      @InputString                  VARCHAR(8000),
      @Delimiter                    VARCHAR(50)
)

RETURNS @Items TABLE (
      Item                          VARCHAR(8000)
)

AS
BEGIN
      IF @Delimiter = ' '
      BEGIN
            SET @Delimiter = ','
            SET @InputString = REPLACE(@InputString, ' ', @Delimiter)
      END

      IF (@Delimiter IS NULL OR @Delimiter = '')
            SET @Delimiter = ','

--INSERT INTO @Items VALUES (@Delimiter) -- Diagnostic
--INSERT INTO @Items VALUES (@InputString) -- Diagnostic

      DECLARE @Item                 VARCHAR(8000)
      DECLARE @ItemList       VARCHAR(8000)
      DECLARE @DelimIndex     INT

      SET @ItemList = @InputString
      SET @DelimIndex = CHARINDEX(@Delimiter, @ItemList, 0)
      WHILE (@DelimIndex != 0)
      BEGIN
            SET @Item = SUBSTRING(@ItemList, 0, @DelimIndex)
            INSERT INTO @Items VALUES (@Item)

            -- Set @ItemList = @ItemList minus one less item
            SET @ItemList = SUBSTRING(@ItemList, @DelimIndex+1, LEN(@ItemList)-@DelimIndex)
            SET @DelimIndex = CHARINDEX(@Delimiter, @ItemList, 0)
      END -- End WHILE

      IF @Item IS NOT NULL -- At least one delimiter was encountered in @InputString
      BEGIN
            SET @Item = @ItemList
            INSERT INTO @Items VALUES (@Item)
      END

      -- No delimiters were encountered in @InputString, so just return @InputString
      ELSE INSERT INTO @Items VALUES (@InputString)

      RETURN

END -- End Function

GO
/****** Object:  Table [Admin].[Category]    Script Date: 29-11-2019 12:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Admin].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NULL CONSTRAINT [DF_Category_ParentId]  DEFAULT ((0)),
	[Name] [nvarchar](250) NULL,
	[Description] [nvarchar](250) NULL,
	[SiteName] [nvarchar](250) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Admin].[OfferBrands]    Script Date: 29-11-2019 12:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Admin].[OfferBrands](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BrandName] [nvarchar](250) NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [PK_OfferBrands] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 29-11-2019 12:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Log]    Script Date: 29-11-2019 12:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Thread] [varchar](255) NOT NULL,
	[Level] [varchar](50) NOT NULL,
	[Logger] [varchar](255) NOT NULL,
	[Message] [varchar](4000) NOT NULL,
	[Exception] [varchar](2000) NULL,
 CONSTRAINT [PK_Log4NetLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Flipkart].[AllOffers]    Script Date: 29-11-2019 12:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Flipkart].[AllOffers](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[startTime] [datetime] NULL,
	[endTime] [datetime] NULL,
	[title] [nvarchar](500) NULL,
	[name] [nvarchar](500) NULL,
	[description] [nvarchar](4000) NULL,
	[url] [nvarchar](500) NULL,
	[category] [nvarchar](500) NULL,
	[imageUrls_default] [nvarchar](500) NULL,
	[imageUrls_mid] [nvarchar](500) NULL,
	[imageUrls_low] [nvarchar](500) NULL,
	[availability] [nvarchar](50) NULL,
 CONSTRAINT [PK_AllOffers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Flipkart].[DealsOfTheDayOffer]    Script Date: 29-11-2019 12:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Flipkart].[DealsOfTheDayOffer](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](500) NOT NULL,
	[title] [nvarchar](500) NOT NULL,
	[description] [nvarchar](2000) NOT NULL,
	[url] [nvarchar](500) NOT NULL,
	[availability] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_DealsOfTheDayOffer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Offer].[OfferProducts]    Script Date: 29-11-2019 12:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Offer].[OfferProducts](
	[productId] [nvarchar](50) NOT NULL,
	[validTill] [datetime] NULL,
	[title] [nvarchar](1000) NOT NULL,
	[productDescription] [nvarchar](max) NULL,
	[imageUrls_200] [nvarchar](2000) NULL,
	[imageUrls_400] [nvarchar](2000) NULL,
	[imageUrls_800] [nvarchar](2000) NULL,
	[productFamily] [nvarchar](max) NULL,
	[maximumRetailPrice] [decimal](18, 2) NOT NULL,
	[SellingPrice] [decimal](18, 2) NOT NULL,
	[SpecialPrice] [decimal](18, 2) NOT NULL,
	[currency] [nvarchar](50) NULL,
	[productUrl] [nvarchar](500) NULL,
	[productBrand] [nvarchar](250) NULL,
	[inStock] [bit] NULL,
	[codAvailable] [bit] NULL,
	[discountPercentage] [decimal](18, 2) NULL,
	[offers] [nvarchar](max) NULL,
	[categoryPath] [nvarchar](500) NULL,
	[attributes] [nvarchar](max) NULL,
	[shippingCharges] [decimal](18, 2) NULL,
	[estimatedDeliveryTime] [nvarchar](500) NULL,
	[sellerName] [nvarchar](500) NULL,
	[sellerAverageRating] [decimal](18, 2) NULL,
	[sellerNoOfRatings] [decimal](18, 2) NULL,
	[sellerNoOfReviews] [decimal](18, 2) NULL,
	[keySpecs] [nvarchar](max) NULL,
	[detailedSpecs] [nvarchar](max) NULL,
	[specificationList] [nvarchar](max) NULL,
	[booksInfo] [nvarchar](max) NULL,
	[lifeStyleInfo] [nvarchar](max) NULL,
	[IsUpdated] [bit] NULL CONSTRAINT [DF_OfferProducts_IsUpdated]  DEFAULT ((0)),
	[CategoryId] [int] NULL,
	[CreatedDate] [datetime] NULL CONSTRAINT [DF_OfferProducts_CreatedDate]  DEFAULT (getdate()),
	[SiteName] [nvarchar](50) NULL,
 CONSTRAINT [PK_OfferProducts] PRIMARY KEY CLUSTERED 
(
	[productId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [User].[Users]    Script Date: 29-11-2019 12:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [User].[Users](
	[ID] [bigint] NOT NULL,
	[MacID] [nvarchar](250) NULL,
	[IPAddress] [nvarchar](250) NULL,
	[Name] [nvarchar](250) NULL,
	[Email] [nvarchar](250) NULL,
	[ContractNumber] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [User].[VisitedUsers]    Script Date: 29-11-2019 12:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [User].[VisitedUsers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MacId] [nvarchar](250) NULL,
	[IPAddress] [nvarchar](200) NULL,
	[Count] [int] NULL,
	[Catagory] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL CONSTRAINT [DF_VisitedUsers_CreatedDate]  DEFAULT (getdate()),
	[ProductTitle] [nvarchar](250) NULL,
	[ProductId] [nvarchar](50) NULL,
	[LastVisitedDate] [datetime] NULL,
	[ClickedOnBuyNow] [bit] NULL,
 CONSTRAINT [PK_VisitedUsers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  StoredProcedure [dbo].[GetAllOfferProducts]    Script Date: 29-11-2019 12:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetAllOfferProducts]
(  
   @SiteName VARCHAR(50) 
)  
AS  
BEGIN  

	IF (@SiteName='ALL')
		BEGIN  
			select distinct op.productId,op.categoryPath,op.title,op.productDescription,op.productUrl,imageUrls_200,imageUrls_400,imageUrls_800,op.currency,op.flipkartSellingPrice,op.flipkartSpecialPrice,
			op.maximumRetailPrice,op.discountPercentage,op.offers,op.attributes,op.productBrand from Flipkart.OfferProducts op
			WHERE  op.inStock != 0 and op.discountPercentage between 50 and 100  
			and (op.categoryPath like 'Electronics' or op.categoryPath like 'Furniture%' or op.categoryPath like 'Apparels%'
			  )
			  and op.categoryPath not like '%Kids%'
			group by op.productId,op.categoryPath,op.title,op.productDescription,op.productUrl,imageUrls_200,imageUrls_400,imageUrls_800,op.currency,op.flipkartSellingPrice,op.flipkartSpecialPrice,
			op.maximumRetailPrice,op.discountPercentage,op.offers,op.attributes,op.productBrand
		    order by  op.discountPercentage desc 
	--		op.CreatedDate desc
			
		END  
	ELSE IF (@SiteName='Flipkart')
		BEGIN  
			select op.productId
			,op.title
			,op.productDescription
			,imageUrls_200,imageUrls_400,imageUrls_800
			,op.maximumRetailPrice
			,op.flipkartSellingPrice
			,op.flipkartSpecialPrice
			,op.currency
			,op.productUrl
			,op.discountPercentage
			,op.inStock
			,op.offers
			,op.categoryPath
			,op.attributes
			,op.CreatedDate from Flipkart.OfferProducts op
			WHERE  op.inStock != 0 and op.discountPercentage > 50
			order by op.discountPercentage desc, op.CreatedDate desc
		END  
END

GO
/****** Object:  StoredProcedure [dbo].[GetParentChildCategories]    Script Date: 29-11-2019 12:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetParentChildCategories]
(
    @ParentId int
)
AS
BEGIN
    SET NOCOUNT ON;
    WITH cte AS
    ( 
        SELECT C1.* FROM Admin.Category as C1
        WHERE ParentId = @ParentId
        UNION ALL
        SELECT C2.* FROM Admin.Category as C2
        INNER JOIN cte AS C on C2.ParentId = C.Id
    )
    SELECT * FROM cte;
END
GO
/****** Object:  StoredProcedure [dbo].[GetTop100OfferProducts]    Script Date: 29-11-2019 12:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetTop100OfferProducts]
(  
   @SiteName VARCHAR(50) 
)  
AS  
BEGIN  

	IF (@SiteName='ALL')
		BEGIN  
			select distinct op.productId,op.categoryPath,op.title,op.productDescription,op.productUrl,op.imageUrls_200,op.imageUrls_400,op.imageUrls_800,op.currency,op.flipkartSellingPrice,op.flipkartSpecialPrice,
			op.maximumRetailPrice,op.discountPercentage,op.offers,op.attributes,op.productBrand from Flipkart.OfferProducts op
			WHERE  op.inStock != 0 and op.discountPercentage between 70 and 100  
			and (op.categoryPath like 'Electronics' or op.categoryPath like 'Furniture%' or op.categoryPath like 'Apparels%'
			  )
			  and op.categoryPath not like '%Kids%'
			group by op.productId,op.categoryPath,op.title,op.productDescription,op.productUrl,op.imageUrls_200,op.imageUrls_400,op.imageUrls_800,op.currency,op.flipkartSellingPrice,op.flipkartSpecialPrice,
			op.maximumRetailPrice,op.discountPercentage,op.offers,op.attributes,op.productBrand
		    order by  op.discountPercentage desc 
	--		op.CreatedDate desc			
		END  
	ELSE IF (@SiteName='Flipkart')
		BEGIN  
			select top 100 op.productId
			,op.title
			,op.productDescription
			,op.imageUrls_200,op.imageUrls_400,op.imageUrls_800
			,op.maximumRetailPrice
			,op.flipkartSellingPrice
			,op.flipkartSpecialPrice
			,op.currency
			,op.productUrl
			,op.discountPercentage
			,op.inStock
			,op.offers
			,op.categoryPath
			,op.attributes
			,op.CreatedDate from Flipkart.OfferProducts op
			WHERE  op.inStock != 0 and op.discountPercentage > 50
			order by op.discountPercentage desc, op.CreatedDate desc
		END  
END

GO
/****** Object:  StoredProcedure [dbo].[InsertOrUpdateIntoVisitedUsers]    Script Date: 29-11-2019 12:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[InsertOrUpdateIntoVisitedUsers]
(  
   @ID int,  
   @MacId VARCHAR(500),  
   @Catagory varchar(4000),  
   @IPAddress VARCHAR(2000),  
   @Count int
   
)  
AS  
BEGIN  

	IF EXISTS(SELECT MacId FROM [User].VisitedUsers WHERE MacId = @MacId)
		BEGIN  
			UPDATE [User].VisitedUsers SET  
			MacId = @MacId, 
			Catagory = @Catagory,
			IPAddress = @IPAddress, 
			CreatedDate = GETDATE(), 
			[Count]= @Count
			WHERE ID = @ID
			
		END  
	ELSE
		BEGIN  
			insert into [User].VisitedUsers(MacId,Catagory,IPAddress,[Count],CreatedDate) 
			values(@MacId, @Catagory,@IPAddress,@Count,GETDATE())  
		END  
END

GO
/****** Object:  StoredProcedure [dbo].[SP_GET_AllOffers_Search_Paging_Sorting]    Script Date: 29-11-2019 12:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GET_AllOffers_Search_Paging_Sorting]
(
    -- Optional Filters for Dynamic Search
	 @searchText       NVARCHAR(250) = NULL, 
	--@title       NVARCHAR(250) = NULL, 
	--@name       NVARCHAR(250) = NULL, 
	--@category NVARCHAR(250) = NULL,
	@startTime DATETIME = NULL,
	@endTime DATETIME = NULL,
    -- Pagination
    @PageNbr            INT = 1,
    @PageSize           INT = 10,
    -- Sort Details
    @SortCol            NVARCHAR(20) = ''
)
AS
BEGIN
    DECLARE
        @ltitle    NVARCHAR(250),
		@lname    NVARCHAR(250), 
		@lcategory NVARCHAR(250),
		@lstartTime DATETIME,
		@lendTime DATETIME
     
    DECLARE
        @lPageNbr   INT,
        @lPageSize  INT,
        @lSortCol   NVARCHAR(20),
        @lFirstRec  INT,
        @lLastRec   INT,
        @lTotalRows INT
 
    SET @ltitle        = LTRIM(RTRIM(@searchText))
	SET @lname         = LTRIM(RTRIM(@searchText))
	SET @lcategory     = LTRIM(RTRIM(@searchText))
	SET @lstartTime    = LTRIM(RTRIM(@startTime))
	SET @lendTime      = LTRIM(RTRIM(@endTime))
   
 
    SET @lPageNbr   = @PageNbr
    SET @lPageSize  = @PageSize
    SET @lSortCol   = LTRIM(RTRIM(@SortCol))
     
    SET @lFirstRec  = ( @lPageNbr - 1 ) * @lPageSize
    SET @lLastRec   = ( @lPageNbr * @lPageSize + 1 ) 
    SET @lTotalRows = @lFirstRec - @lLastRec + 1
 
    ; WITH CTE_Results
    AS (
        SELECT ROW_NUMBER() OVER (ORDER BY
 
            CASE WHEN @lSortCol = 'startTime_Asc' THEN o.startTime
                END ASC,
            CASE WHEN @lSortCol = 'startTime_Desc' THEN o.startTime
                END DESC, 

			CASE WHEN @lSortCol = 'endTime_Asc' THEN o.endTime
                END ASC,
            CASE WHEN @lSortCol = 'endTime_Desc' THEN o.endTime
                END DESC

            ) AS ROWNUM, o.Id,
			Count(*) over () AS TotalCount,
            o.startTime,o.endTime,o.title,o.name,
			o.[description],o.url,o.category,o.imageUrls_default,o.imageUrls_low,
			o.imageUrls_mid,o.[availability]
			from Flipkart.AllOffers o
			
			WHERE  
			(@ltitle IS NULL OR o.title LIKE '%' + @ltitle + '%')
			AND (@lname IS NULL OR o.name LIKE '%' + @lname + '%')
			AND (@lcategory IS NULL OR o.category LIKE '%' + @lcategory + '%')
			AND (@lstartTime IS NULL OR o.startTime >=@lstartTime)
			AND (@lendTime IS NULL OR o.endTime >=@lendTime)
			AND O.[availability] ='LIVE'
		
    )
    SELECT o.Id,
         o.startTime,o.endTime,o.title,o.name,
			o.[description],o.url,o.category,o.imageUrls_default,o.imageUrls_low,
			o.imageUrls_mid,o.[availability],o.TotalCount
    FROM CTE_Results AS o
    WHERE
        o.ROWNUM > @lFirstRec 
    AND o.ROWNUM < @lLastRec
	ORDER BY  o.ROWNUM ASC  
         
END

GO
/****** Object:  StoredProcedure [dbo].[SP_GET_OfferProducts_Search]    Script Date: 29-11-2019 12:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GET_OfferProducts_Search]
(
    -- Optional Filters for Dynamic Search
	 @searchText       NVARCHAR(250) = NULL, 
    -- Pagination
    @PageNbr            INT = 1,
    @PageSize           INT = 20
  
)
AS
BEGIN
    DECLARE
        @ltitle    NVARCHAR(250), 
		@lcategoryPath NVARCHAR(250),
		@lproductBrand NVARCHAR(250)
       
     
    DECLARE
        @lPageNbr   INT,
        @lPageSize  INT,
        @lSortCol   NVARCHAR(20),
        @lFirstRec  INT,
        @lLastRec   INT,
        @lTotalRows INT
 
    SET @ltitle         = LTRIM(RTRIM(@searchText))
	SET @lcategoryPath         = LTRIM(RTRIM(@searchText))
	SET @lproductBrand         = LTRIM(RTRIM(@searchText))
 

    SET @lPageNbr   = @PageNbr
    SET @lPageSize  = @PageSize
     
    SET @lFirstRec  = ( @lPageNbr - 1 ) * @lPageSize
    SET @lLastRec   = ( @lPageNbr * @lPageSize + 1 ) 
    SET @lTotalRows = @lFirstRec - @lLastRec + 1
 
    ; WITH CTE_Results
    AS (
        SELECT distinct ROW_NUMBER() OVER 
				(PARTITION BY op.categoryPath ORDER BY op.discountPercentage DESC,op.validTill DESC)
				AS CattopProductNum,

            op.productId,op.categoryPath,op.title,op.validTill,op.productDescription,
			op.productUrl,op.imageUrls_200,op.imageUrls_400,op.imageUrls_800,op.currency,op.SellingPrice,op.SpecialPrice,
			op.maximumRetailPrice,op.discountPercentage,op.offers,op.attributes,op.productBrand
			from Offer.OfferProducts op
			WHERE 
			
			(@ltitle IS NULL OR op.title LIKE '%' + @ltitle + '%')
			AND op.inStock != 0 
			OR (@lcategoryPath IS NULL OR op.categoryPath LIKE '%' + @lcategoryPath + '%')
			OR (@lproductBrand IS NULL OR op.productBrand LIKE '%' + @lproductBrand + '%')
			
		
    )
    SELECT 
         op.productId,op.categoryPath,op.title,op.validTill,op.productDescription,op.productUrl,op.imageUrls_200,op.imageUrls_400,op.imageUrls_800,op.currency,op.SellingPrice,op.SpecialPrice,
			op.maximumRetailPrice,op.discountPercentage,op.offers,op.attributes,op.productBrand
    FROM CTE_Results AS op
    WHERE
  	 op.CattopProductNum=1
	GROUP BY op.productId,op.categoryPath,op.title,op.validTill,op.productDescription,op.productUrl,op.imageUrls_200,op.imageUrls_400,op.imageUrls_800,op.currency,op.SellingPrice,op.SpecialPrice,
			op.maximumRetailPrice,op.discountPercentage,op.offers,op.attributes,op.productBrand
    ORDER BY   op.discountPercentage desc, op.validTill DESC
         
END

GO
/****** Object:  StoredProcedure [dbo].[SP_GET_OfferProducts_Search_Paging_Sorting]    Script Date: 29-11-2019 12:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GET_OfferProducts_Search_Paging_Sorting]
(
    -- Optional Filters for Dynamic Search
	 @searchText       NVARCHAR(250) = NULL, 
   -- @title       NVARCHAR(250) = NULL, 
	--@categoryPath NVARCHAR(250) = NULL,
   -- @discountPercentage DECIMAL(18,2) = NULL,
    -- Pagination
    @PageNbr            INT = 1,
    @PageSize           INT = 100,
    -- Sort Details
    @SortCol            NVARCHAR(20) = ''
)
AS
BEGIN
    DECLARE
        @ltitle    NVARCHAR(250), 
		@lcategoryPath NVARCHAR(250),
        @ldiscountPercentage  DECIMAL(18,2)
     
    DECLARE
        @lPageNbr   INT,
        @lPageSize  INT,
        @lSortCol   NVARCHAR(20),
        @lFirstRec  INT,
        @lLastRec   INT,
        @lTotalRows INT
 
    SET @ltitle         = LTRIM(RTRIM(@searchText))
	 SET @lcategoryPath         = LTRIM(RTRIM(@searchText))
  --  SET @ldiscountPercentage         = LTRIM(RTRIM(@discountPercentage))
   
 
    SET @lPageNbr   = @PageNbr
    SET @lPageSize  = @PageSize
    SET @lSortCol   = LTRIM(RTRIM(@SortCol))
     
    SET @lFirstRec  = ( @lPageNbr - 1 ) * @lPageSize
    SET @lLastRec   = ( @lPageNbr * @lPageSize + 1 ) 
    SET @lTotalRows = @lFirstRec - @lLastRec + 1
 
    ; WITH CTE_Results
    AS (
        SELECT distinct ROW_NUMBER() OVER 
		
		(ORDER BY
 
            CASE WHEN @lSortCol = 'title_Asc' THEN op.title
                END ASC,
            CASE WHEN @lSortCol = 'title_Desc' THEN op.title
                END DESC, 

			CASE WHEN @lSortCol = 'categoryPath_Asc' THEN op.categoryPath
                END ASC,
            CASE WHEN @lSortCol = 'categoryPath_Desc' THEN op.categoryPath
                END DESC, 
 
            CASE WHEN @lSortCol = 'discountPercentage_Asc' THEN op.discountPercentage
                END ASC,
            CASE WHEN @lSortCol = 'discountPercentage_Desc' THEN op.discountPercentage
                END DESC
            ) AS ROWNUM,

			ROW_NUMBER() OVER 
				(PARTITION BY op.categoryPath ORDER BY op.discountPercentage DESC)
				AS CattopProductNum,
			
			Count(*) over () AS TotalCount,

            op.productId,op.categoryPath,op.title,op.productDescription,
			op.productUrl,op.imageUrls_200,op.imageUrls_400,op.imageUrls_800,op.currency,op.SellingPrice,op.SpecialPrice,
			op.maximumRetailPrice,op.discountPercentage,op.offers,op.attributes,op.productBrand
			--from Offer.OfferProducts op
			FROM (
					SELECT  fop.*,
							ROW_NUMBER() OVER (PARTITION BY fop.categoryPath ORDER BY fop.discountPercentage DESC) ROW_NUM,
							Count(*) over () AS TotalCount
					FROM Offer.OfferProducts fop
					)  op

			--INNER JOIN
			--		(
			--			SELECT distinct ROW_NUMBER() OVER 
			--	(PARTITION BY iop.categoryPath ORDER BY iop.discountPercentage DESC)
			--	AS CattopProductNum,
			--			 iop.categoryPath, MAX(iop.discountPercentage) maxDiscountPercentage
			--			FROM Offer.OfferProducts iop  
						
			--			GROUP BY iop.categoryPath, iop.discountPercentage
			--			--ORDER BY maxDiscountPercentage desc
			--		) cat ON op.categoryPath = cat.categoryPath AND
			--					cat.CattopProductNum=1 and
			--				op.discountPercentage= cat.maxDiscountPercentage AND
			--				cat.maxDiscountPercentage>0

			WHERE 
			op.ROW_NUM=1 AND
			-- (((op.flipkartSellingPrice - op.flipkartSpecialPrice)/op.flipkartSellingPrice)*100) >30 AND
			 
			(@ltitle IS NULL OR op.title LIKE '%' + @ltitle + '%')
			AND (@lcategoryPath IS NULL OR op.categoryPath LIKE '%' + @lcategoryPath + '%')
			--AND (@ldiscountPercentage IS NULL OR op.discountPercentage >=@ldiscountPercentage)
			AND op.inStock != 0 
			--AND op.discountPercentage between 50 and 100  
			
			--and (op.categoryPath like 'Electronics' or op.categoryPath like 'Furniture%' or op.categoryPath like 'Apparels%'
			--  )
			--  and op.categoryPath not like '%Kids%'
			--		op.CreatedDate desc
		
    )
    SELECT distinct
         op.productId,op.categoryPath,op.title,op.productDescription,op.productUrl,op.imageUrls_200,op.imageUrls_400,op.imageUrls_800,op.currency,op.SellingPrice,op.SpecialPrice,
			op.maximumRetailPrice,op.discountPercentage,op.offers,op.attributes,op.productBrand, op.ROWNUM, op.TotalCount
    FROM CTE_Results AS op
    WHERE
        op.ROWNUM > @lFirstRec 
    AND op.ROWNUM < @lLastRec
	AND op.CattopProductNum=1
	GROUP BY op.productId,op.categoryPath,op.title,op.productDescription,op.productUrl,op.imageUrls_200,op.imageUrls_400,op.imageUrls_800,op.currency,op.SellingPrice,op.SpecialPrice,
			op.maximumRetailPrice,op.discountPercentage,op.offers,op.attributes,op.productBrand,op.ROWNUM,  op.TotalCount
    ORDER BY   op.discountPercentage desc--, op.ROWNUM ASC
         
END

GO
/****** Object:  StoredProcedure [Flipkart].[InsertOrUpdateIntoAllOffers]    Script Date: 29-11-2019 12:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [Flipkart].[InsertOrUpdateIntoAllOffers]
(  
   @startTime datetime, 
   @endTime datetime,  
   @title VARCHAR(500),  
   @name varchar(500),  
   @description nvarchar(2000),
   @url nvarchar(500),  
   @category nvarchar(500),  
   @imageUrls_default NVARCHAR(2000),  
   @imageUrls_mid NVARCHAR(2000),  
   @imageUrls_low NVARCHAR(2000),  
   @availability varchar(200)
)  
AS  
BEGIN  

	IF NOT EXISTS(SELECT name FROM Flipkart.AllOffers WHERE name = @name and url=@url)
	--	BEGIN  
	--		UPDATE Flipkart.AllOffers SET  
	--		startTime = @startTime, 
	--		endTime = @endTime,
	--		title = @title,  
	--		name=@name,
	--		[description]=@description,
	--		url=@url,
	--		category=@category,
	--		imageUrls_default = @imageUrls_default,
	--		imageUrls_mid = @imageUrls_mid,
	--		imageUrls_low = @imageUrls_low,
	--		[availability]=@availability
	--		WHERE title = @title
			
	--	END  
	--ELSE
		BEGIN  
			insert into Flipkart.AllOffers(startTime,endTime,title,name,[description],url,category,imageUrls_default,imageUrls_mid,imageUrls_low,[availability]) 
			values( @startTime,@endTime,@title,@name,@description,@url,@category,@imageUrls_default,@imageUrls_mid,@imageUrls_low,@availability)  
		END  
END

GO
/****** Object:  StoredProcedure [Flipkart].[InsertOrUpdateIntoOfferProducts]    Script Date: 29-11-2019 12:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [Flipkart].[InsertOrUpdateIntoOfferProducts]
(  
   @productId VARCHAR(50),  
   @title VARCHAR(500),  
   @productDescription varchar(4000),  
   @imageUrls_200 VARCHAR(2000), 
   @imageUrls_400 VARCHAR(2000), 
   @imageUrls_800 VARCHAR(2000),  
   @productFamily nvarchar(2000),
   @maximumRetailPrice decimal(18, 2),  
   @flipkartSellingPrice decimal(18, 2),  
   @flipkartSpecialPrice decimal(18, 2),
   @currency varchar(20),  
   @productUrl VARCHAR(2500),  
   @productBrand nvarchar(20),
   @inStock bit, 
   @codAvailable bit, 
   @discountPercentage decimal(18, 2),  
   @offers nvarchar(2000),
   @categoryPath NVARCHAR(2000),  
   @attributes nvarchar(2000),
   @shippingCharges decimal(18, 2),
   @estimatedDeliveryTime NVARCHAR(2000),
   @sellerName NVARCHAR(500),
   @sellerAverageRating decimal(18, 2),
   @sellerNoOfRatings decimal(18, 2),
   @sellerNoOfReviews decimal(18, 2),
   @keySpecs NVARCHAR(2000),
   @detailedSpecs NVARCHAR(4000),
   @specificationList NVARCHAR(4000),
   @booksInfo NVARCHAR(4000),
   @lifeStyleInfo NVARCHAR(4000),
   @IsUpdated bit,
   @CategoryId int,
   @CreatedDate datetime
)  
AS  
BEGIN  
DECLARE @dbFlipkartSellingPrice Decimal(18,2), @dbFlipkartSpecialPrice Decimal(18,2)

	IF EXISTS(SELECT productId FROM Flipkart.OfferProducts WHERE productId = @productId)
		BEGIN  
			SELECT  @dbFlipkartSpecialPrice = flipkartSpecialPrice
				 FROM Flipkart.OfferProducts WHERE productId = @productId

			IF(@dbFlipkartSpecialPrice>@flipkartSpecialPrice)
			BEGIN  
				UPDATE Flipkart.OfferProducts SET  
						flipkartSellingPrice=@flipkartSellingPrice	WHERE productId = @productId
			END

			--UPDATE Flipkart.OfferProducts SET  
			--productId = @productId, title = @title, productDescription = @productDescription,  
			--imageUrls = @imageUrls, productFamily=@productFamily, maximumRetailPrice=@maximumRetailPrice,
			--flipkartSellingPrice=@flipkartSellingPrice,flipkartSpecialPrice=@flipkartSpecialPrice,
			--currency=@currency,productUrl=@productUrl,productBrand=@productBrand,discountPercentage=@discountPercentage,
			--inStock=@inStock, offers=@offers,categoryPath=@categoryPath,attributes=@attributes, CreatedDate =getdate()
			--WHERE productId = @productId
			
		END  
	ELSE
		BEGIN  
			insert into Flipkart.OfferProducts(productId,title,productDescription,imageUrls_200,imageUrls_400,imageUrls_800,productFamily,maximumRetailPrice,
			flipkartSellingPrice,flipkartSpecialPrice,currency,productUrl,productBrand,inStock,codAvailable,
			discountPercentage,offers,categoryPath,attributes,shippingCharges,estimatedDeliveryTime,sellerName,
			sellerAverageRating,sellerNoOfRatings,sellerNoOfReviews,keySpecs,detailedSpecs,specificationList,
			booksInfo,lifeStyleInfo,IsUpdated,CategoryId,CreatedDate) 
			values( @productId,@title,@productDescription,@imageUrls_200,@imageUrls_400,@imageUrls_800,@productFamily,@maximumRetailPrice,
			@flipkartSellingPrice,@flipkartSpecialPrice,@currency,@productUrl,@productBrand,@inStock,@codAvailable,
			@discountPercentage,@offers,@categoryPath,@attributes,@shippingCharges,@estimatedDeliveryTime,@sellerName,
			@sellerAverageRating,@sellerNoOfRatings,@sellerNoOfReviews,@keySpecs,@detailedSpecs,@specificationList,
			@booksInfo,@lifeStyleInfo,@IsUpdated,@CategoryId,GETDATE())  
		END  
END


GO
/****** Object:  StoredProcedure [Flipkart].[RemoveOldOfferProducts]    Script Date: 29-11-2019 12:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Flipkart].[RemoveOldOfferProducts] AS
BEGIN
    DELETE Flipkart.OfferProducts
    WHERE DATEADD(HOUR,-2,GetDate()) > CreatedDate
    
    DELETE Flipkart.AllOffers
    WHERE DATEADD(HOUR,-2,GetDate()) >= endTime
END


GO
