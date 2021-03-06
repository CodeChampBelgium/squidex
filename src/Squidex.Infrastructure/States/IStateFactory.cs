﻿// ==========================================================================
//  IStateFactory.cs
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex Group
//  All rights reserved.
// ==========================================================================

using System;
using System.Threading.Tasks;

namespace Squidex.Infrastructure.States
{
    public interface IStateFactory
    {
        Task<T> GetSingleAsync<T>(string key) where T : IStatefulObject<string>;

        Task<T> GetSingleAsync<T>(Guid key) where T : IStatefulObject<Guid>;

        Task<T> GetSingleAsync<T, TKey>(TKey key) where T : IStatefulObject<TKey>;

        Task<T> CreateAsync<T>(string key) where T : IStatefulObject<string>;

        Task<T> CreateAsync<T>(Guid key) where T : IStatefulObject<Guid>;

        Task<T> CreateAsync<T, TKey>(TKey key) where T : IStatefulObject<TKey>;
    }
}
