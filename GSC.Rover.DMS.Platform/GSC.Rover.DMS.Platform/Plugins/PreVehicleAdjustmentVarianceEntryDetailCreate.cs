// <copyright file="PreVehicleAdjustmentVarianceEntryDetailCreate.cs" company="">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author></author>
// <date>8/31/2016 10:39:00 AM</date>
// <summary>Implements the PreVehicleAdjustmentVarianceEntryDetailCreate Plugin.</summary>
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
// </auto-generated>
namespace GSC.Rover.DMS.Platform.Plugins
{
    using System;
    using System.ServiceModel;
    using Microsoft.Xrm.Sdk;

    using GSC.Rover.DMS.BusinessLogic.VehicleAdjustmentVarianceEntryDetail;

    /// <summary>
    /// PreVehicleAdjustmentVarianceEntryDetailCreate Plugin.
    /// </summary>    
    public class PreVehicleAdjustmentVarianceEntryDetailCreate: Plugin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreVehicleAdjustmentVarianceEntryDetailCreate"/> class.
        /// </summary>
        public PreVehicleAdjustmentVarianceEntryDetailCreate()
            : base(typeof(PreVehicleAdjustmentVarianceEntryDetailCreate))
        {
            base.RegisteredEvents.Add(new Tuple<int, string, string, Action<LocalPluginContext>>(20, "Create", "gsc_sls_adjustmentvariancedetail", new Action<LocalPluginContext>(ExecutePreVehicleAdjustmentVarianceEntryDetailCreate)));

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
        protected void ExecutePreVehicleAdjustmentVarianceEntryDetailCreate(LocalPluginContext localContext)
        {
            if (localContext == null)
            {
                throw new ArgumentNullException("localContext");
            }

            IPluginExecutionContext context = localContext.PluginExecutionContext;
            IOrganizationService service = localContext.OrganizationService;
            ITracingService trace = localContext.TracingService;
            Entity vehicleAdjustmentVarianceEntryDetailEntity = (Entity)context.InputParameters["Target"];

            string message = context.MessageName;
            string error = "";

            try
            {                
                VehicleAdjustmentVarianceEntryDetailHandler vehicleAdjustmentVarianceEntryDetailHandler = new VehicleAdjustmentVarianceEntryDetailHandler(service, trace);
                vehicleAdjustmentVarianceEntryDetailHandler.CheckExistingInventoryRecord(vehicleAdjustmentVarianceEntryDetailEntity, message);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Please insert product information in Vehicle and Item Catalog"))
                {
                    throw new InvalidPluginExecutionException(ex.Message);
                }
                else if (ex.Message.Contains("Record is already posted. Cannot allocate new vehicle."))
                {
                    throw new InvalidPluginExecutionException("Record is already posted. Cannot allocate new vehicle.");
                }
                else if (ex.Message.Contains("Vehicle already exists in Adjustment/Variance Entry."))
                {
                    throw new InvalidPluginExecutionException("Vehicle already exists in Adjustment/Variance Entry.");
                }
                else if (ex.Message.Contains("Vehicle already exists in inventory."))
                {
                    throw new InvalidPluginExecutionException("Vehicle already exists in inventory.");
                }
                else
                {
                    throw new InvalidPluginExecutionException(String.Concat("(Exception)\n", ex.Message, Environment.NewLine, ex.StackTrace, Environment.NewLine, error));
                }                
            }
        }
    }
}
