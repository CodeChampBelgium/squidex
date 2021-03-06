﻿// ==========================================================================
//  IPersistent.cs
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex Group
//  All rights reserved.
// ==========================================================================

using System.Collections.Generic;
using System.Threading.Tasks;
using Squidex.Infrastructure.EventSourcing;

namespace Squidex.Infrastructure.States
{
    public interface IPersistence : IPersistence<object>
    {
    }

    public interface IPersistence<TState>
    {
        long Version { get; }

        Task WriteEventsAsync(IEnumerable<Envelope<IEvent>> @events);

        Task WriteSnapshotAsync(TState state);

        Task ReadAsync(long expectedVersion = EtagVersion.Any);
    }
}
