﻿// ==========================================================================
//  FindExtensions.cs
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex Group
//  All rights reserved.
// ==========================================================================

using System.Collections.Generic;
using Microsoft.OData.Core.UriParser;
using MongoDB.Driver;
using Squidex.Core.Schemas;

namespace Squidex.Read.MongoDb.Contents.Visitors
{
    public static class FindExtensions
    {
        private static readonly FilterDefinitionBuilder<MongoContentEntity> Filter = Builders<MongoContentEntity>.Filter;

        public static IFindFluent<MongoContentEntity, MongoContentEntity> Sort(this IFindFluent<MongoContentEntity, MongoContentEntity>  cursor, ODataUriParser query, Schema schema)
        {

            return cursor.Sort(SortBuilder.BuildSort(query, schema));
        }

        public static IFindFluent<MongoContentEntity, MongoContentEntity> Take(this IFindFluent<MongoContentEntity, MongoContentEntity> cursor, ODataUriParser query)
        {
            var top = query.ParseTop();

            if (top.HasValue)
            {
                cursor = cursor.Limit((int)top.Value);
            }

            return cursor;
        }

        public static IFindFluent<MongoContentEntity, MongoContentEntity> Skip(this IFindFluent<MongoContentEntity, MongoContentEntity> cursor, ODataUriParser query)
        {
            var skip = query.ParseSkip();

            if (skip.HasValue)
            {
                cursor = cursor.Skip((int)skip.Value);
            }

            return cursor;
        }

        public static IFindFluent<MongoContentEntity, MongoContentEntity> Find(this IMongoCollection<MongoContentEntity> cursor, ODataUriParser query, Schema schema, bool nonPublished)
        {
            var filters = new List<FilterDefinition<MongoContentEntity>>
            {
                Filter.Eq(x => x.IsDeleted, false)
            };

            if (!nonPublished)
            {
                filters.Add(Filter.Eq(x => x.IsPublished, false));
            }

            var filter = FilterBuilder.Build(query, schema);

            if (filter != null)
            {
                filters.Add(filter);
            }

            return cursor.Find(Filter.And(filters));
        }
    }
}