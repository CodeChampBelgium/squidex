﻿// ==========================================================================
//  AppPattern.cs
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex Group
//  All rights reserved.
// ==========================================================================

using System.Diagnostics.Contracts;
using Squidex.Infrastructure;

namespace Squidex.Domain.Apps.Core.Apps
{
    public sealed class AppPattern
    {
        private readonly string name;
        private readonly string pattern;
        private readonly string message;

        public string Name
        {
            get { return name; }
        }

        public string Pattern
        {
            get { return pattern; }
        }

        public string Message
        {
            get { return message; }
        }

        public AppPattern(string name, string pattern, string message = null)
        {
            Guard.NotNullOrEmpty(name, nameof(name));
            Guard.NotNullOrEmpty(pattern, nameof(pattern));

            this.name = name;
            this.pattern = pattern;
            this.message = message;
        }

        [Pure]
        public AppPattern Update(string name, string pattern, string defaultMessage)
        {
            return new AppPattern(name, pattern, defaultMessage);
        }
    }
}