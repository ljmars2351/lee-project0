﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderSystem.Library
{
    public static class OrderMapper
    {
        public static Customer Map(DataAccess.Customer cust) => new Customer
        {
            CustId = cust.Id,
            FName = cust.FirstName,
            LName = cust.LastName,
            DateOfBirth = cust.DateOfBirth,
            Loc = (int)cust.PrefLoc
        };

        public static DataAccess.Customer Map(Customer cust) => new DataAccess.Customer
        {
            Id = cust.CustId,
            FirstName = cust.FName,
            LastName = cust.LName,
            DateOfBirth = cust.DateOfBirth,
            PrefLoc = cust.Loc
        };

        public static Location Map(DataAccess.Location location) => new Location
        {
            LocId = location.Id,
            StreetAddress = location.Street,
            City = location.City,
            State = location.State,
            Zip = location.Zip
        };

        public static DataAccess.Location Map(Location location) => new DataAccess.Location
        {
            Id = location.LocId,
            Street = location.StreetAddress,
            City = location.City,
            State = location.State,
            Zip = location.Zip
        };

        public static Products Map(DataAccess.Product product) => new Products
        {
            ProdId = product.Id,
            Name = product.Name,
            Price = (decimal)product.Price
        };

        public static DataAccess.Product Map(Products prod) => new DataAccess.Product
        {
            Id = prod.ProdId,
            Name = prod.Name,
            Price = prod.Price
        };

        public static Products Map(DataAccess.ProdHist prodHist) => new Products
        {
            ProdId = prodHist.Id,
            Name = prodHist.Name,
            Price = (decimal)prodHist.Price
        };

        public static DataAccess.ProdHist DualMap(Products prod) => new DataAccess.ProdHist
        {
            Id = prod.ProdId,
            Name = prod.Name,
            Price = prod.Price
        };

        public static Order Map(DataAccess.Cart cart) => new Order
        {
            OrdId = cart.Id,
            StoreId = cart.LocId,
            CustId = cart.CustId,
            ProdId = cart.ProdId,
            Quantity = cart.Quant,
            OrdTIme = (DateTime)cart.Time
        };

        public static DataAccess.Cart Map(Order ord) => new DataAccess.Cart
        {
            Id = ord.OrdId,
            LocId = ord.StoreId,
            CustId = ord.CustId,
            ProdId = ord.ProdId,
            Quant = ord.Quantity,
            Time = ord.OrdTIme
        };

        

        public static IEnumerable<Customer> Map(IEnumerable<DataAccess.Customer> cust) => cust.Select(Map);

        public static IEnumerable<DataAccess.Customer> Map(IEnumerable<Customer> cust) => cust.Select(Map);

        public static IEnumerable<Location> Map(IEnumerable<DataAccess.Location> loc) => loc.Select(Map);

        public static IEnumerable<DataAccess.Location> Map(IEnumerable<Location> loc) => loc.Select(Map);

        public static IEnumerable<Products> Map(IEnumerable<DataAccess.Product> prod) => prod.Select(Map);

        public static IEnumerable<DataAccess.Product> Map(IEnumerable<Products> prod) => prod.Select(Map);

        public static IEnumerable<DataAccess.ProdHist> DualMap(IEnumerable<Products> prod) => prod.Select(DualMap);

        public static IEnumerable<Products> Map(IEnumerable<DataAccess.ProdHist> prod) => prod.Select(Map);

        public static IEnumerable<Order> Map(IEnumerable<DataAccess.Cart> cart) => cart.Select(Map);

        public static IEnumerable<DataAccess.Cart> Map(IEnumerable<Order> ord) => ord.Select(Map);
    }
}
