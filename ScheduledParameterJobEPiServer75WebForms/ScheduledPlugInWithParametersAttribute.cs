using System;
using EPiServer.PlugIn;

namespace EPiServer.Templates.Alloy.ScheduledParameterJob
{
    public class ScheduledPlugInWithParametersAttribute : ScheduledPlugInAttribute
    {
        /// <summary>
        /// The class where your parameter controls are defined, for instance:
        /// DefinitionsClass = "EPiServer.CodeSample.ScheduledJobs.ParameterJob.DefinitionSample"
        /// </summary>
        public string DefinitionsClass { get; set; }

        /// <summary>
        /// The assembly where your DefinitionsClass resides, for instance:
        /// DefinitionsAssembly = "EPiServer.Templates.AlloyTech"
        /// </summary>
        public string DefinitionsAssembly { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(DefinitionsClass))
            {
                throw new Exception(
                    "You must supply a parameter definition class to use this attribute; ParameterDefinitionClass='namespace.classname'.");
            }
            if (string.IsNullOrEmpty(DefinitionsAssembly))
            {
                throw new Exception(
                    "You must supply the assembly of the parameter definition class to use this attribute; DefinitionsAssembly='assembly'.");
            }
        }
    }
}
