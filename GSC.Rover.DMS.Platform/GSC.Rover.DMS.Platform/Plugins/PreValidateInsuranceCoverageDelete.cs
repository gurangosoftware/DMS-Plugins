// <copyright file="PreValidateInsuranceCoverageDelete.cs" company="">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author></author>
// <date>11/10/2016 2:38:02 PM</date>
// <summary>Implements the PreValidateInsuranceCoverageDelete Plugin.</summary>
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
// </auto-generated>
namespace GSC.Rover.DMS.Platform.Plugins
{
    using System;
    using System.ServiceModel;
    using Microsoft.Xrm.Sdk;
    using GSC.Rover.DMS.BusinessLogic.Insurance;
    using GSC.Rover.DMS.BusinessLogic.Common;
    using Microsoft.Xrm.Sdk.Query;

    /// <summary>
    /// PreValidateInsuranceCoverageDelete Plugin.
    /// </summary>    
    public class PreValidateInsuranceCoverageDelete: Plugin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreValidateInsuranceCoverageDelete"/> class.
        /// </summary>
        public PreValidateInsuranceCoverageDelete()
            : base(typeof(PreValidateInsuranceCoverageDelete))
        {
            base.RegisteredEvents.Add(new Tuple<int, string, string, Action<LocalPluginContext>>(10, "Delete", "gsc_cmn_insurancecoverage", new Action<LocalPluginContext>(ExecutePreValidateInsuranceCoverageDelete)));

            // Note : you can register for more events here if this plugin is not specific to an individual entity and message combination.
            // You may also need to update your RegisterFile.crmregister plug-in registration file to reflect any change.
        }

        /// <summary>
        /// Executes the plug-in.
        /// </summary>
        /// <param name="localContext">The <see cref="LocalPluginContext"/> which contains the
        /// <see cref="IPluginExecutionContext"/>,
        /// <see cref="IOrganizationService"/>
        /// and <see cref="ITracingService"/>
        /// </param>
        /// <remarks>
        /// For improved performance, Microsoft Dynamics CRM caches plug-in instances.
        /// The plug-in's Execute method should be written to be stateless as the constructor
        /// is not called for every invocation of the plug-in. Also, multiple system threads
        /// could execute the plug-in at the same time. All per invocation state information
        /// is stored in the context. This means that you should not use global variables in plug-ins.
        /// </remarks>
        protected void ExecutePreValidateInsuranceCoverageDelete(LocalPluginContext localContext)
        {
            if (localContext == null)
            {
                throw new ArgumentNullException("localContext");
            }
            
            IPluginExecutionContext context = localContext.PluginExecutionContext;
            IOrganizationService service = localContext.OrganizationService;
            ITracingService trace = localContext.TracingService;
            var insuranceCoverageEntity = (EntityReference)context.InputParameters["Target"];

            try
            {
                EntityCollection coverageCollection = CommonHandler.RetrieveRecordsByOneValue("gsc_cmn_insurancecoverage", "gsc_cmn_insurancecoverageid", insuranceCoverageEntity.Id, service, null, OrderType.Ascending,
                    new[] { "gsc_premium", "gsc_insuranceid" });

                InsuranceCoverageHandler coverageHandler = new InsuranceCoverageHandler(service, trace);
                coverageHandler.ComputeTotalPremium(coverageCollection.Entities[0], context.MessageName);
            } 
            catch (Exception ex)
            {
               throw new InvalidPluginExecutionException(String.Concat("(Exception)\n", ex.Message, Environment.NewLine, ex.StackTrace));
            }
        }
    }
}
