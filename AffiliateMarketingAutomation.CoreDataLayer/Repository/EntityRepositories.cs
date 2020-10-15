﻿using AffiliateMarketingAutomation.CoreDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AffiliateMarketingAutomation.CoreDataLayer
{
    #region Offer Product
    public interface IOfferProductRepository : IEntity<OfferProduct>
    {
    }
    public class OfferProductRepository : Entity<OfferProduct>, IOfferProductRepository
    {
    }
    #endregion

    #region AllOffer
    public interface IAllOfferRepository : IEntity<AllOffer>
    {
    }
    public class AllOfferRepository : Entity<AllOffer>, IAllOfferRepository
    {
    }

    #endregion
    #region DealsOfTheDayOffer
    public interface IDealsOfTheDayOfferRepository : IEntity<DealsOfTheDay>
    {
    }
    public class DealsOfTheDayOfferRepository : Entity<DealsOfTheDay>, IDealsOfTheDayOfferRepository
    {
    }

    #endregion

    #region Visited User
    public interface IVisitedUserRepository : IEntity<VisitedUser>
    {
    }
    public class VisitedUserRepository : Entity<VisitedUser>, IVisitedUserRepository
    {
    }
    #endregion

    #region Category
    public interface ICategoryRepository : IEntity<Category>
    {
    }
    public class CategoryRepository : Entity<Category>, ICategoryRepository
    {
    }
    #endregion

    #region Offer Brand
    public interface IOfferBrandRepository : IEntity<OfferBrand>
    {
    }
    public class OfferBrandRepository : Entity<OfferBrand>, IOfferBrandRepository
    {
    }
    #endregion
}
