// <copyright file="PreValidateCommittedFirmOrderQuantityDetailDelete.cs" company="">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author></author>
// <date>3/3/2017 11:33:37 AM</date>
// <summary>Implements the PreValidateCommittedFirmOrderQuantityDetailDelete Plugin.</summary>
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
// </auto-generated>
namespace GSC.Rover.DMS.Platform.Plugins
{
    using System;
    using System.ServiceModel;
    using Microsoft.Xrm.Sdk;
    using GSC.Rover.DMS.BusinessLogic.Common;
    using Microsoft.Xrm.Sdk.Query;
    using GSC.Rover.DMS.BusinessLogic.CommittedFirmOrderQuantityDetail;

    /// <summary>
    /// PreValidateCommittedFirmOrderQuantityDetailDelete Plugin.
    /// </summary>    
    public class PreValidateCommittedFirmOrderQuantityDetailDelete: Plugin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreValidateCommittedFirmOrderQuantityDetailDelete"/> class.
        /// </summary>
        public PreValidateCommittedFirmOrderQuantityDetailDelete()
            : base(typeof(PreValidateCommittedFirmOrderQuantityDetailDelete))
        {
            base.RegisteredEvents.Add(new Tuple<int, string, string, Action<LocalPluginContext>>(10, "Delete", "gsc_sls_committedfirmorderquantitydetail", new Action<LocalPluginContext>(ExecutePreValidateCommittedFirmOrderQuantityDetailDelete)));

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
        protected void ExecutePreValidateCommittedFirmOrderQuantityDetailDelete(LocalPluginContext localContext)
        {
            if (localContext == null)
            {
                throw new ArgumentNullException("localContext");
            }

            IPluginExecutionContext context = localContext.PluginExecutionContext;
            IOrganizationService service = localContext.OrganizationService;
            ITracingService trace = localContext.TracingService;
            var vehicleInTransit = (EntityReference)context.InputParameters["Target"];
            string message = context.MessageName;

            try
            {
                EntityCollection cfoDetailCollection = CommonHandler.RetrieveRecordsByOneValue("gsc_sls_committedfirmorderquantitydetail", "gsc_sls_committedfirmorderquantitydetailid", vehicleInTransit.Id, service,
                    null, OrderType.Ascending, new[] { "gsc_allocatedquantity" });

                CommittedFirmOrderQuantityDetailHandler cfoDetailHandler = new CommittedFirmOrderQuantityDetailHandler(service, trace);
                if (!cfoDetailHandler.RestrictDeleteCreate(cfoDetailCollection.Entities[0]))
                    throw new InvalidPluginExecutionException("Cannot delete this record.");

                if (!cfoDetailHandler.RestrictDelete(cfoDetailCollection.Entities[0]))
                    throw new InvalidPluginExecutionException("Record(s) not successfully deleted.");
            }

            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException(ex.Message);
            }
        }
    }
}
