﻿// ==========================================================================
//  AppDomainObject.cs
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex Group
//  All rights reserved.
// ==========================================================================

using System;
using Squidex.Domain.Apps.Core.Apps;
using Squidex.Domain.Apps.Entities.Apps.Commands;
using Squidex.Domain.Apps.Entities.Apps.State;
using Squidex.Domain.Apps.Events;
using Squidex.Domain.Apps.Events.Apps;
using Squidex.Infrastructure;
using Squidex.Infrastructure.Commands;
using Squidex.Infrastructure.EventSourcing;
using Squidex.Infrastructure.Reflection;

namespace Squidex.Domain.Apps.Entities.Apps
{
    public sealed class AppDomainObject : DomainObjectBase<AppState>
    {
        private readonly InitialPatterns initialPatterns;

        public AppDomainObject(InitialPatterns initialPatterns)
        {
            Guard.NotNull(initialPatterns, nameof(initialPatterns));

            this.initialPatterns = initialPatterns;
        }

        public AppDomainObject Create(CreateApp command)
        {
            ThrowIfCreated();

            var appId = new NamedId<Guid>(command.AppId, command.Name);

            RaiseEvent(CreateInitalEvent(appId, command.Actor, command.Name));
            RaiseEvent(CreateInitialOwner(appId, command.Actor));
            RaiseEvent(CreateInitialLanguage(appId, command.Actor));

            foreach (var pattern in initialPatterns)
            {
                RaiseEvent(CreateInitialPattern(appId, command.Actor, pattern.Key, pattern.Value));
            }

            return this;
        }

        public AppDomainObject UpdateLanguage(UpdateLanguage command)
        {
            ThrowIfNotCreated();

            RaiseEvent(SimpleMapper.Map(command, new AppLanguageUpdated()));

            return this;
        }

        public AppDomainObject UpdateClient(UpdateClient command)
        {
            ThrowIfNotCreated();

            if (!string.IsNullOrWhiteSpace(command.Name))
            {
                RaiseEvent(SimpleMapper.Map(command, new AppClientRenamed()));
            }

            if (command.Permission.HasValue)
            {
                RaiseEvent(SimpleMapper.Map(command, new AppClientUpdated { Permission = command.Permission.Value }));
            }

            return this;
        }

        public AppDomainObject AssignContributor(AssignContributor command)
        {
            ThrowIfNotCreated();

            RaiseEvent(SimpleMapper.Map(command, new AppContributorAssigned()));

            return this;
        }

        public AppDomainObject RemoveContributor(RemoveContributor command)
        {
            ThrowIfNotCreated();

            RaiseEvent(SimpleMapper.Map(command, new AppContributorRemoved()));

            return this;
        }

        public AppDomainObject AttachClient(AttachClient command)
        {
            ThrowIfNotCreated();

            RaiseEvent(SimpleMapper.Map(command, new AppClientAttached()));

            return this;
        }

        public AppDomainObject RevokeClient(RevokeClient command)
        {
            ThrowIfNotCreated();

            RaiseEvent(SimpleMapper.Map(command, new AppClientRevoked()));

            return this;
        }

        public AppDomainObject AddLanguage(AddLanguage command)
        {
            ThrowIfNotCreated();

            RaiseEvent(SimpleMapper.Map(command, new AppLanguageAdded()));

            return this;
        }

        public AppDomainObject RemoveLanguage(RemoveLanguage command)
        {
            ThrowIfNotCreated();

            RaiseEvent(SimpleMapper.Map(command, new AppLanguageRemoved()));

            return this;
        }

        public AppDomainObject ChangePlan(ChangePlan command)
        {
            ThrowIfNotCreated();

            RaiseEvent(SimpleMapper.Map(command, new AppPlanChanged()));

            return this;
        }

        public AppDomainObject AddPattern(AddPattern command)
        {
            ThrowIfNotCreated();

            RaiseEvent(SimpleMapper.Map(command, new AppPatternAdded()));

            return this;
        }

        public AppDomainObject DeletePattern(DeletePattern command)
        {
            ThrowIfNotCreated();

            RaiseEvent(SimpleMapper.Map(command, new AppPatternDeleted()));

            return this;
        }

        public AppDomainObject UpdatePattern(UpdatePattern command)
        {
            ThrowIfNotCreated();

            RaiseEvent(SimpleMapper.Map(command, new AppPatternUpdated()));

            return this;
        }

        private void RaiseEvent(AppEvent @event)
        {
            if (@event.AppId == null)
            {
                @event.AppId = new NamedId<Guid>(State.Id, State.Name);
            }

            RaiseEvent(Envelope.Create(@event));
        }

        private static AppCreated CreateInitalEvent(NamedId<Guid> appId, RefToken actor, string name)
        {
            return new AppCreated { AppId = appId, Actor = actor, Name = name };
        }

        private static AppPatternAdded CreateInitialPattern(NamedId<Guid> appId, RefToken actor, Guid id, AppPattern p)
        {
            return new AppPatternAdded { AppId = appId, Actor = actor, PatternId = id, Name = p.Name, Pattern = p.Pattern, Message = p.Message };
        }

        private static AppLanguageAdded CreateInitialLanguage(NamedId<Guid> appId, RefToken actor)
        {
            return new AppLanguageAdded { AppId = appId, Actor = actor, Language = Language.EN };
        }

        private static AppContributorAssigned CreateInitialOwner(NamedId<Guid> appId, RefToken actor)
        {
            return new AppContributorAssigned { AppId = appId, Actor = actor, ContributorId = actor.Identifier, Permission = AppContributorPermission.Owner };
        }

        private void ThrowIfNotCreated()
        {
            if (string.IsNullOrWhiteSpace(State.Name))
            {
                throw new DomainException("App has not been created.");
            }
        }

        private void ThrowIfCreated()
        {
            if (!string.IsNullOrWhiteSpace(State.Name))
            {
                throw new DomainException("App has already been created.");
            }
        }

        protected override void OnRaised(Envelope<IEvent> @event)
        {
            UpdateState(State.Apply(@event));
        }
    }
}
