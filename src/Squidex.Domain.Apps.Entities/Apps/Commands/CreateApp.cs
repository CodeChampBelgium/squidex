﻿// ==========================================================================
//  CreateApp.cs
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex Group
//  All rights reserved.
// ==========================================================================

using System;
using Squidex.Infrastructure.Commands;

namespace Squidex.Domain.Apps.Entities.Apps.Commands
{
    public sealed class CreateApp : SquidexCommand, IAggregateCommand
    {
        public Guid AppId { get; set; }

        public string Name { get; set; }

        Guid IAggregateCommand.AggregateId
        {
            get { return AppId; }
        }

        public CreateApp()
        {
            AppId = Guid.NewGuid();
        }
    }
}