﻿// ==========================================================================
//  IEntityWithCreatedBy.cs
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex Group
//  All rights reserved.
// ==========================================================================

using Squidex.Infrastructure;

namespace Squidex.Domain.Apps.Entities
{
    public interface IEntityWithCreatedBy
    {
        RefToken CreatedBy { get; }
    }
}
