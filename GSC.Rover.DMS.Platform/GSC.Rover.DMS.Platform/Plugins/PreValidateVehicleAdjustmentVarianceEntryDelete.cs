// <copyright file="PreValidateVehicleAdjustmentVarianceEntryDelete.cs" company="">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author></author>
// <date>9/1/2016 5:10:15 PM</date>
// <summary>Implements the PreValidateVehicleAdjustmentVarianceEntryDelete Plugin.</summary>
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
    using GSC.Rover.DMS.BusinessLogic.VehicleAdjustmentVarianceEntry;

    /// <summary>
    /// PreValidateVehicleAdjustmentVarianceEntryDelete Plugin.
    /// </summary>    
    public class PreValidateVehicleAdjustmentVarianceEntryDelete: Plugin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreValidateVehicleAdjustmentVarianceEntryDelete"/> class.
        /// </summary>
        public PreValidateVehicleAdjustmentVarianceEntryDelete()
            : base(typeof(PreValidateVehicleAdjustmentVarianceEntryDelete))
        {
            base.RegisteredEvents.Add(new Tuple<int, string, string, Action<LocalPluginContext>>(10, "Delete", "gsc_sls_vehicleadjustmentvarianceentry", new Action<LocalPluginContext>(ExecutePreValidateVehicleAdjustmentVarianceEntryDelete)));

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
        protected void ExecutePreValidateVehicleAdjustmentVarianceEntryDelete(LocalPluginContext localContext)
        {
            if (localContext == null)
            {
                throw new ArgumentNullException("localContext");
            }

            IPluginExecutionContext context = localContext.PluginExecutionContext;
            IOrganizationService service = localContext.OrganizationService;
            ITracingService trace = localContext.TracingService;
            EntityReference vehicleAdjusmentVarianceEntryEntity = (EntityReference)context.InputParameters["Target"];
            string message = context.MessageName;

            if (context.Depth > 1) { return; }

            try
            {
                EntityCollection vehicleAdjustmentVarianceEntryRecords = CommonHandler.RetrieveRecordsByOneValue("gsc_sls_vehicleadjustmentvarianceentry", "gsc_sls_vehicleadjustmentvarianceentryid", vehicleAdjusmentVarianceEntryEntity.Id, service,
                    null, OrderType.Ascending, new[] { "gsc_adjustmentvariancestatus" });

                VehicleAdjustmentVarianceEntryHandler vehicleAdjustmentVarianceEntryHandler = new VehicleAdjustmentVarianceEntryHandler(service, trace);
                vehicleAdjustmentVarianceEntryHandler.AdjustInventoryOnUnpostedDelete(vehicleAdjustmentVarianceEntryRecords.Entities[0]);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unable to delete already posted Vehicle Adjustment/Variance Entry"))
                    throw new InvalidPluginExecutionException("Unable to delete already posted Vehicle Adjustment/Variance Entry");
                else
                    throw new InvalidPluginExecutionException(String.Concat("(Exception)\n", ex.Message, Environment.NewLine, ex.StackTrace));
            }
        }
    }
}