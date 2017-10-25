using System;
using AutomappedProj.Entities;
using FluentNHibernate.Automapping;

namespace AutomappedProj
{
    class ExampleAutomappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.Namespace == "AutomappedProj.Entities";
        }

        public override bool IsComponent(Type type)
        {
            return type == typeof(Location);
        }
    }
}