﻿// ==========================================================================
//  RuleAggregateCommand.cs
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex Group
//  All rights reserved.
// ==========================================================================

using System;
using Squidex.Infrastructure.Commands;

namespace Squidex.Domain.Apps.Entities.Rules.Commands
{
    public abstract class RuleAggregateCommand : AppCommand, IAggregateCommand
    {
        public Guid RuleId { get; set; }

        Guid IAggregateCommand.AggregateId
        {
            get { return RuleId; }
        }
    }
}
