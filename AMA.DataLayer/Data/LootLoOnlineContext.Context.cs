﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AMA.DataLayer.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class LootLoOnlineDatabaseEntities : DbContext
    {
        public LootLoOnlineDatabaseEntities()
            : base("name=LootLoOnlineDatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<OfferBrand> OfferBrands { get; set; }
        public virtual DbSet<C__EFMigrationsHistory> C__EFMigrationsHistory { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<VisitedUser> VisitedUsers { get; set; }
        public virtual DbSet<AllOffer> AllOffers { get; set; }
        public virtual DbSet<DealsOfTheDayOffer> DealsOfTheDayOffers { get; set; }
        public virtual DbSet<OfferProduct> OfferProducts { get; set; }
    
        [DbFunction("LootLoOnlineDatabaseEntities", "fn_split_string_to_column")]
        public virtual IQueryable<fn_split_string_to_column_Result> fn_split_string_to_column(string @string, string delimiter)
        {
            var stringParameter = @string != null ?
                new ObjectParameter("string", @string) :
                new ObjectParameter("string", typeof(string));
    
            var delimiterParameter = delimiter != null ?
                new ObjectParameter("delimiter", delimiter) :
                new ObjectParameter("delimiter", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<fn_split_string_to_column_Result>("[LootLoOnlineDatabaseEntities].[fn_split_string_to_column](@string, @delimiter)", stringParameter, delimiterParameter);
        }
    
        [DbFunction("LootLoOnlineDatabaseEntities", "Split")]
        public virtual IQueryable<Split_Result> Split(string line, string splitOn)
        {
            var lineParameter = line != null ?
                new ObjectParameter("Line", line) :
                new ObjectParameter("Line", typeof(string));
    
            var splitOnParameter = splitOn != null ?
                new ObjectParameter("SplitOn", splitOn) :
                new ObjectParameter("SplitOn", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<Split_Result>("[LootLoOnlineDatabaseEntities].[Split](@Line, @SplitOn)", lineParameter, splitOnParameter);
        }
    
        [DbFunction("LootLoOnlineDatabaseEntities", "SplitString")]
        public virtual IQueryable<SplitString_Result> SplitString(string inputString, string delimiter)
        {
            var inputStringParameter = inputString != null ?
                new ObjectParameter("InputString", inputString) :
                new ObjectParameter("InputString", typeof(string));
    
            var delimiterParameter = delimiter != null ?
                new ObjectParameter("Delimiter", delimiter) :
                new ObjectParameter("Delimiter", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<SplitString_Result>("[LootLoOnlineDatabaseEntities].[SplitString](@InputString, @Delimiter)", inputStringParameter, delimiterParameter);
        }
    
        public virtual ObjectResult<GetAllOfferProducts_Result> GetAllOfferProducts(string siteName)
        {
            var siteNameParameter = siteName != null ?
                new ObjectParameter("SiteName", siteName) :
                new ObjectParameter("SiteName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllOfferProducts_Result>("GetAllOfferProducts", siteNameParameter);
        }
    
        public virtual ObjectResult<GetTop100OfferProducts_Result> GetTop100OfferProducts(string siteName)
        {
            var siteNameParameter = siteName != null ?
                new ObjectParameter("SiteName", siteName) :
                new ObjectParameter("SiteName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetTop100OfferProducts_Result>("GetTop100OfferProducts", siteNameParameter);
        }
    
        public virtual int InsertOrUpdateIntoVisitedUsers(Nullable<int> iD, string macId, string catagory, string iPAddress, Nullable<int> count)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            var macIdParameter = macId != null ?
                new ObjectParameter("MacId", macId) :
                new ObjectParameter("MacId", typeof(string));
    
            var catagoryParameter = catagory != null ?
                new ObjectParameter("Catagory", catagory) :
                new ObjectParameter("Catagory", typeof(string));
    
            var iPAddressParameter = iPAddress != null ?
                new ObjectParameter("IPAddress", iPAddress) :
                new ObjectParameter("IPAddress", typeof(string));
    
            var countParameter = count.HasValue ?
                new ObjectParameter("Count", count) :
                new ObjectParameter("Count", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertOrUpdateIntoVisitedUsers", iDParameter, macIdParameter, catagoryParameter, iPAddressParameter, countParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<SP_GET_AllOffers_Search_Paging_Sorting_Result> SP_GET_AllOffers_Search_Paging_Sorting(string title, string name, string category, Nullable<System.DateTime> startTime, Nullable<System.DateTime> endTime, Nullable<int> pageNbr, Nullable<int> pageSize, string sortCol)
        {
            var titleParameter = title != null ?
                new ObjectParameter("title", title) :
                new ObjectParameter("title", typeof(string));
    
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var categoryParameter = category != null ?
                new ObjectParameter("category", category) :
                new ObjectParameter("category", typeof(string));
    
            var startTimeParameter = startTime.HasValue ?
                new ObjectParameter("startTime", startTime) :
                new ObjectParameter("startTime", typeof(System.DateTime));
    
            var endTimeParameter = endTime.HasValue ?
                new ObjectParameter("endTime", endTime) :
                new ObjectParameter("endTime", typeof(System.DateTime));
    
            var pageNbrParameter = pageNbr.HasValue ?
                new ObjectParameter("PageNbr", pageNbr) :
                new ObjectParameter("PageNbr", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColParameter = sortCol != null ?
                new ObjectParameter("SortCol", sortCol) :
                new ObjectParameter("SortCol", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GET_AllOffers_Search_Paging_Sorting_Result>("SP_GET_AllOffers_Search_Paging_Sorting", titleParameter, nameParameter, categoryParameter, startTimeParameter, endTimeParameter, pageNbrParameter, pageSizeParameter, sortColParameter);
        }
    
        public virtual ObjectResult<SP_GET_OfferProducts_Search_Paging_Sorting_Result> SP_GET_OfferProducts_Search_Paging_Sorting(string title, string categoryPath, Nullable<decimal> discountPercentage, Nullable<int> pageNbr, Nullable<int> pageSize, string sortCol)
        {
            var titleParameter = title != null ?
                new ObjectParameter("title", title) :
                new ObjectParameter("title", typeof(string));
    
            var categoryPathParameter = categoryPath != null ?
                new ObjectParameter("categoryPath", categoryPath) :
                new ObjectParameter("categoryPath", typeof(string));
    
            var discountPercentageParameter = discountPercentage.HasValue ?
                new ObjectParameter("discountPercentage", discountPercentage) :
                new ObjectParameter("discountPercentage", typeof(decimal));
    
            var pageNbrParameter = pageNbr.HasValue ?
                new ObjectParameter("PageNbr", pageNbr) :
                new ObjectParameter("PageNbr", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColParameter = sortCol != null ?
                new ObjectParameter("SortCol", sortCol) :
                new ObjectParameter("SortCol", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GET_OfferProducts_Search_Paging_Sorting_Result>("SP_GET_OfferProducts_Search_Paging_Sorting", titleParameter, categoryPathParameter, discountPercentageParameter, pageNbrParameter, pageSizeParameter, sortColParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual int InsertOrUpdateIntoAllOffers(Nullable<System.DateTime> startTime, Nullable<System.DateTime> endTime, string title, string name, string description, string url, string category, string imageUrls_default, string imageUrls_mid, string imageUrls_low, string availability)
        {
            var startTimeParameter = startTime.HasValue ?
                new ObjectParameter("startTime", startTime) :
                new ObjectParameter("startTime", typeof(System.DateTime));
    
            var endTimeParameter = endTime.HasValue ?
                new ObjectParameter("endTime", endTime) :
                new ObjectParameter("endTime", typeof(System.DateTime));
    
            var titleParameter = title != null ?
                new ObjectParameter("title", title) :
                new ObjectParameter("title", typeof(string));
    
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("description", description) :
                new ObjectParameter("description", typeof(string));
    
            var urlParameter = url != null ?
                new ObjectParameter("url", url) :
                new ObjectParameter("url", typeof(string));
    
            var categoryParameter = category != null ?
                new ObjectParameter("category", category) :
                new ObjectParameter("category", typeof(string));
    
            var imageUrls_defaultParameter = imageUrls_default != null ?
                new ObjectParameter("imageUrls_default", imageUrls_default) :
                new ObjectParameter("imageUrls_default", typeof(string));
    
            var imageUrls_midParameter = imageUrls_mid != null ?
                new ObjectParameter("imageUrls_mid", imageUrls_mid) :
                new ObjectParameter("imageUrls_mid", typeof(string));
    
            var imageUrls_lowParameter = imageUrls_low != null ?
                new ObjectParameter("imageUrls_low", imageUrls_low) :
                new ObjectParameter("imageUrls_low", typeof(string));
    
            var availabilityParameter = availability != null ?
                new ObjectParameter("availability", availability) :
                new ObjectParameter("availability", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertOrUpdateIntoAllOffers", startTimeParameter, endTimeParameter, titleParameter, nameParameter, descriptionParameter, urlParameter, categoryParameter, imageUrls_defaultParameter, imageUrls_midParameter, imageUrls_lowParameter, availabilityParameter);
        }
    
        public virtual int InsertOrUpdateIntoOfferProducts(string productId, string title, string productDescription, string imageUrls_200, string imageUrls_400, string imageUrls_800, string productFamily, Nullable<decimal> maximumRetailPrice, Nullable<decimal> flipkartSellingPrice, Nullable<decimal> flipkartSpecialPrice, string currency, string productUrl, string productBrand, Nullable<decimal> discountPercentage, Nullable<int> inStock, string offers, string categoryPath, string attributes)
        {
            var productIdParameter = productId != null ?
                new ObjectParameter("productId", productId) :
                new ObjectParameter("productId", typeof(string));
    
            var titleParameter = title != null ?
                new ObjectParameter("title", title) :
                new ObjectParameter("title", typeof(string));
    
            var productDescriptionParameter = productDescription != null ?
                new ObjectParameter("productDescription", productDescription) :
                new ObjectParameter("productDescription", typeof(string));
    
            var imageUrls_200Parameter = imageUrls_200 != null ?
                new ObjectParameter("imageUrls_200", imageUrls_200) :
                new ObjectParameter("imageUrls_200", typeof(string));
    
            var imageUrls_400Parameter = imageUrls_400 != null ?
                new ObjectParameter("imageUrls_400", imageUrls_400) :
                new ObjectParameter("imageUrls_400", typeof(string));
    
            var imageUrls_800Parameter = imageUrls_800 != null ?
                new ObjectParameter("imageUrls_800", imageUrls_800) :
                new ObjectParameter("imageUrls_800", typeof(string));
    
            var productFamilyParameter = productFamily != null ?
                new ObjectParameter("productFamily", productFamily) :
                new ObjectParameter("productFamily", typeof(string));
    
            var maximumRetailPriceParameter = maximumRetailPrice.HasValue ?
                new ObjectParameter("maximumRetailPrice", maximumRetailPrice) :
                new ObjectParameter("maximumRetailPrice", typeof(decimal));
    
            var flipkartSellingPriceParameter = flipkartSellingPrice.HasValue ?
                new ObjectParameter("flipkartSellingPrice", flipkartSellingPrice) :
                new ObjectParameter("flipkartSellingPrice", typeof(decimal));
    
            var flipkartSpecialPriceParameter = flipkartSpecialPrice.HasValue ?
                new ObjectParameter("flipkartSpecialPrice", flipkartSpecialPrice) :
                new ObjectParameter("flipkartSpecialPrice", typeof(decimal));
    
            var currencyParameter = currency != null ?
                new ObjectParameter("currency", currency) :
                new ObjectParameter("currency", typeof(string));
    
            var productUrlParameter = productUrl != null ?
                new ObjectParameter("productUrl", productUrl) :
                new ObjectParameter("productUrl", typeof(string));
    
            var productBrandParameter = productBrand != null ?
                new ObjectParameter("productBrand", productBrand) :
                new ObjectParameter("productBrand", typeof(string));
    
            var discountPercentageParameter = discountPercentage.HasValue ?
                new ObjectParameter("discountPercentage", discountPercentage) :
                new ObjectParameter("discountPercentage", typeof(decimal));
    
            var inStockParameter = inStock.HasValue ?
                new ObjectParameter("inStock", inStock) :
                new ObjectParameter("inStock", typeof(int));
    
            var offersParameter = offers != null ?
                new ObjectParameter("offers", offers) :
                new ObjectParameter("offers", typeof(string));
    
            var categoryPathParameter = categoryPath != null ?
                new ObjectParameter("categoryPath", categoryPath) :
                new ObjectParameter("categoryPath", typeof(string));
    
            var attributesParameter = attributes != null ?
                new ObjectParameter("attributes", attributes) :
                new ObjectParameter("attributes", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertOrUpdateIntoOfferProducts", productIdParameter, titleParameter, productDescriptionParameter, imageUrls_200Parameter, imageUrls_400Parameter, imageUrls_800Parameter, productFamilyParameter, maximumRetailPriceParameter, flipkartSellingPriceParameter, flipkartSpecialPriceParameter, currencyParameter, productUrlParameter, productBrandParameter, discountPercentageParameter, inStockParameter, offersParameter, categoryPathParameter, attributesParameter);
        }
    
        public virtual int RemoveOldOfferProducts()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RemoveOldOfferProducts");
        }
    }
}