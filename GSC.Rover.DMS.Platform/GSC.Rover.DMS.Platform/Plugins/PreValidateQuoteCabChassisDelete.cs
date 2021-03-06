// <copyright file="PreValidateQuoteCabChassisDelete.cs" company="">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author></author>
// <date>9/22/2016 10:11:16 AM</date>
// <summary>Implements the PreValidateQuoteCabChassisDelete Plugin.</summary>
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
// </auto-generated>
namespace GSC.Rover.DMS.Platform.Plugins
{
    using System;
    using System.ServiceModel;
    using Microsoft.Xrm.Sdk;
    using GSC.Rover.DMS.BusinessLogic.QuoteCabChassis;
    using GSC.Rover.DMS.BusinessLogic.Common;
    using Microsoft.Xrm.Sdk.Query;

    /// <summary>
    /// PreValidateQuoteCabChassisDelete Plugin.
    /// </summary>    
    public class PreValidateQuoteCabChassisDelete: Plugin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreValidateQuoteCabChassisDelete"/> class.
        /// </summary>
        public PreValidateQuoteCabChassisDelete()
            : base(typeof(PreValidateQuoteCabChassisDelete))
        {
            base.RegisteredEvents.Add(new Tuple<int, string, string, Action<LocalPluginContext>>(10, "Delete", "gsc_sls_quotecabchassis", new Action<LocalPluginContext>(ExecutePreValidateQuoteCabChassisDelete)));

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
        protected void ExecutePreValidateQuoteCabChassisDelete(LocalPluginContext localContext)
        {
            if (localContext == null)
            {
                throw new ArgumentNullException("localContext");
            }

            IPluginExecutionContext context = localContext.PluginExecutionContext;
            IOrganizationService service = localContext.OrganizationService;
            ITracingService trace = localContext.TracingService;
            var quoteCabChassisEntity = (EntityReference)context.InputParameters["Target"];
            string message = context.MessageName;
            string error = "";

            if (context.Depth > 1) { return; }

            try
            {
                QuoteCabChassisHandler quoteCabChassisHandler = new QuoteCabChassisHandler(service, trace);

                EntityCollection quoteCabChassisCollection = CommonHandler.RetrieveRecordsByOneValue("gsc_sls_quotecabchassis", "gsc_sls_quotecabchassisid", quoteCabChassisEntity.Id, service, null, OrderType.Ascending,
                    new[] { "gsc_quoteid", "gsc_amount", "gsc_financing" });

                if (quoteCabChassisCollection != null && quoteCabChassisCollection.Entities.Count > 0)
                {
                    Entity quoteCabChassis = quoteCabChassisCollection.Entities[0];
                    quoteCabChassisHandler.SetCCAddOnAmount(quoteCabChassis, message);
                }

            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException(String.Concat("(Exception)\n", ex.Message, Environment.NewLine, ex.StackTrace, Environment.NewLine, error));
            }
        }
    }
}
