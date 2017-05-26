// <copyright file="PreValidateSalesOrderDelete.cs" company="">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author></author>
// <date>11/23/2016 5:45:46 PM</date>
// <summary>Implements the PreValidateSalesOrderDelete Plugin.</summary>
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
// </auto-generated>
namespace GSC.Rover.DMS.Platform.Plugins
{
    using System;
    using System.ServiceModel;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using BusinessLogic.Common;
    using BusinessLogic.SalesOrder;
    using GSC.Rover.DMS.BusinessLogic.AllocatedVehicle;

    /// <summary>
    /// PreValidateSalesOrderDelete Plugin.
    /// </summary>    
    public class PreValidateSalesOrderDelete: Plugin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreValidateSalesOrderDelete"/> class.
        /// </summary>
        public PreValidateSalesOrderDelete()
            : base(typeof(PreValidateSalesOrderDelete))
        {
            base.RegisteredEvents.Add(new Tuple<int, string, string, Action<LocalPluginContext>>(10, "Delete", "salesorder", new Action<LocalPluginContext>(ExecutePreValidateSalesOrderDelete)));

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
        protected void ExecutePreValidateSalesOrderDelete(LocalPluginContext localContext)
        {
            if (localContext == null)
            {
                throw new ArgumentNullException("localContext");
            }

            IPluginExecutionContext context = localContext.PluginExecutionContext;
            IOrganizationService service = localContext.OrganizationService;
            ITracingService trace = localContext.TracingService;
            var SalesOrderTargetEntity = (EntityReference)context.InputParameters["Target"];
            string message = context.MessageName;
            string error = "";

            try
            {
                SalesOrderHandler SalesOrderHandler = new SalesOrderHandler(service, trace);

                EntityCollection SalesOrderCollection = CommonHandler.RetrieveRecordsByOneValue(SalesOrderTargetEntity.LogicalName, "salesorderid", SalesOrderTargetEntity.Id, service,
                    null, OrderType.Ascending, new[] { "salesorderid", "gsc_statuscopy", "quoteid" });

                if (SalesOrderCollection.Entities.Count > 0)
                {
                    if (SalesOrderHandler.ValidateDelete(SalesOrderCollection.Entities[0]) == false)
                        throw new InvalidPluginExecutionException("Sales order record(s) cannot be deleted.");
                }

            }

            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException(ex.Message);
            }
        }
    }
}
