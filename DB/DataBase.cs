﻿using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TourApp.DB
{
    public enum DBTable
    {
        FavoriteTour,
        AutoSearchTour,
        FoundTour
    }
    public static class DataBase
    {
        public static LiteDatabase db { get; private set; }

        public static void Connect()
        {
            db = new LiteDatabase("Filename=./data.db;Connection=shared");
            //db.GetCollection<TourtItem>(DBTable.FavoriteApartment.ToString()).EnsureIndex(a => a.Address);
           // db.GetCollection<FoundTour>(DBTable.FoundApartment.ToString()).EnsureIndex(a => a.apartment.Address);
        }

        public static void Insert<T>(T item, DBTable collectionName)
        {
            db.GetCollection<T>(collectionName.ToString()).Insert(item);
        }

        public static void Update<T>(T item, DBTable collectionName)
        {
            db.GetCollection<T>(collectionName.ToString()).Update(item);
        }

        public static void Delete(BsonValue id, DBTable collectionName)
        {
            db.GetCollection(collectionName.ToString()).Delete(id);
        }

        public static void Delete<T>(Expression<Func<T, bool>> predicate, DBTable collectionName)
        {
            db.GetCollection<T>(collectionName.ToString()).DeleteMany(predicate);
        }

        public static List<T> GetCollectionList<T>(DBTable collectionName)
        {
            return db.GetCollection<T>(collectionName.ToString()).FindAll().ToList();
        }

        public static ILiteQueryable<T> Query<T>(DBTable collectionName)
        {
            return db.GetCollection<T>(collectionName.ToString()).Query();
        }
    }
}
