﻿// ==========================================================================
//  IEntityWithVersion.cs
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex Group
//  All rights reserved.
// ==========================================================================

namespace Squidex.Domain.Apps.Entities
{
    public interface IEntityWithVersion
    {
        long Version { get; }
    }
}
