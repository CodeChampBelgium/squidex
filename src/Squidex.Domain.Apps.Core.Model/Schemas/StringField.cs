﻿// ==========================================================================
//  StringField.cs
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex Group
//  All rights reserved.
// ==========================================================================

namespace Squidex.Domain.Apps.Core.Schemas
{
    public sealed class StringField : Field<StringFieldProperties>
    {
        public StringField(long id, string name, Partitioning partitioning)
            : base(id, name, partitioning, new StringFieldProperties())
        {
        }

        public StringField(long id, string name, Partitioning partitioning, StringFieldProperties properties)
            : base(id, name, partitioning, properties)
        {
        }

        public override T Accept<T>(IFieldVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
