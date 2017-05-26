// <copyright file="PostOrderChargeCreate.cs" company="">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author></author>
// <date>3/14/2016 3:59:42 PM</date>
// <summary>Implements the PostOrderChargeCreate Plugin.</summary>
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
// </auto-generated>
namespace GSC.Rover.DMS.Platform.Plugins
{
    using System;
    using System.ServiceModel;
    using Microsoft.Xrm.Sdk;
    using GSC.Rover.DMS.BusinessLogic.SalesOrderCharge;
    using GSC.Rover.DMS.BusinessLogic.Common;
    using Microsoft.Xrm.Sdk.Query;

    /// <summary>
    /// PostOrderChargeCreate Plugin.
    /// </summary>    
    public class PostOrderChargeCreate: Plugin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostOrderChargeCreate"/> class.
        /// </summary>
        public PostOrderChargeCreate()
            : base(typeof(PostOrderChargeCreate))
        {
            base.RegisteredEvents.Add(new Tuple<int, string, string, Action<LocalPluginContext>>(40, "Create", "gsc_cmn_ordercharge", new Action<LocalPluginContext>(ExecutePostOrderChargeCreate)));

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
        protected void ExecutePostOrderChargeCreate(LocalPluginContext localContext)
        {
            if (localContext == null)
            {
                throw new ArgumentNullException("localContext");
            }

            IPluginExecutionContext context = localContext.PluginExecutionContext;
            IOrganizationService service = localContext.OrganizationService;
            ITracingService trace = localContext.TracingService;
            Entity salesOrderChargeEntity = (Entity)context.InputParameters["Target"];

            string message = context.MessageName;
            string error = "";

            try
            {
                SalesOrderChargeHandler salesOrderChargeHandler = new SalesOrderChargeHandler(service, trace);
                salesOrderChargeHandler.SetOrderTotalChargesAmount(salesOrderChargeEntity, message);
            }
            catch (Exception ex)
            {
                //throw new InvalidPluginExecutionException(String.Concat("(Exception)\n", ex.Message, Environment.NewLine, ex.StackTrace, Environment.NewLine, error));
                throw new InvalidPluginExecutionException(ex.Message);
            }
        }
    }
}