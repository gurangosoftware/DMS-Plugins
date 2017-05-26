// <copyright file="PreValidateAllocatedVehicleDelete.cs" company="">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author></author>
// <date>5/18/2016 2:23:20 PM</date>
// <summary>Implements the PreValidateAllocatedVehicleDelete Plugin.</summary>
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
// </auto-generated>
namespace GSC.Rover.DMS.Platform.Plugins
{
    using System;
    using System.ServiceModel;
    using Microsoft.Xrm.Sdk;
    using GSC.Rover.DMS.BusinessLogic.AllocatedVehicle;
    using GSC.Rover.DMS.BusinessLogic.Common;
    using Microsoft.Xrm.Sdk.Query;

    /// <summary>
    /// PreValidateAllocatedVehicleDelete Plugin.
    /// </summary>    
    public class PreValidateAllocatedVehicleDelete: Plugin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreValidateAllocatedVehicleDelete"/> class.
        /// </summary>
        public PreValidateAllocatedVehicleDelete()
            : base(typeof(PreValidateAllocatedVehicleDelete))
        {
            base.RegisteredEvents.Add(new Tuple<int, string, string, Action<LocalPluginContext>>(10, "Delete", "gsc_iv_allocatedvehicle", new Action<LocalPluginContext>(ExecutePreValidateAllocatedVehicleDelete)));

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
        protected void ExecutePreValidateAllocatedVehicleDelete(LocalPluginContext localContext)
        {
            if (localContext == null)
            {
                throw new ArgumentNullException("localContext");
            }
            
            IPluginExecutionContext context = localContext.PluginExecutionContext;
            IOrganizationService service = localContext.OrganizationService;
            ITracingService trace = localContext.TracingService;
            var allocatedVehicle = (EntityReference)context.InputParameters["Target"];
            string message = context.MessageName;
            string error = "";

            trace.Trace("Condition Context");
            //if (context.Depth > 1) { return; }

            trace.Trace("Start Try catch");
            try
            {
                AllocatedVehicleHandler allocatedVehicleHandler = new AllocatedVehicleHandler(service, trace);

                EntityCollection allocatedRecords = CommonHandler.RetrieveRecordsByOneValue("gsc_iv_allocatedvehicle", "gsc_iv_allocatedvehicleid", allocatedVehicle.Id, service,
                    null, OrderType.Ascending, new[] { "gsc_orderid", "gsc_inventoryid" , "gsc_vehicletransferid", "gsc_vehicleintransittransferid"});

                if (allocatedRecords != null && allocatedRecords.Entities.Count > 0)
                 {
                     Entity allocatedEntity = allocatedRecords.Entities[0];
                     allocatedVehicleHandler.IsSubjectforPDI(allocatedEntity);
                     allocatedVehicleHandler.RemoveAllocation(allocatedEntity);
                 }
               
            }
            catch (Exception ex)
            {
                //if(ex.Message.Contains("Unable to delete record that is already shipped."))
                   // throw new InvalidPluginExecutionException("Unable to delete record that is already shipped.");
               // else
                    throw new InvalidPluginExecutionException(ex.Message);
            }


        }
    }
}