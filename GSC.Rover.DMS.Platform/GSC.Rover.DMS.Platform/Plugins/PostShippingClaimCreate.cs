// <copyright file="PostShippingClaimCreate.cs" company="">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author></author>
// <date>8/2/2016 4:16:46 PM</date>
// <summary>Implements the PostShippingClaimCreate Plugin.</summary>
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
// </auto-generated>
namespace GSC.Rover.DMS.Platform.Plugins
{
    using System;
    using System.ServiceModel;
    using Microsoft.Xrm.Sdk;
    using GSC.Rover.DMS.BusinessLogic.ShippingClaim;
    using GSC.Rover.DMS.BusinessLogic.Common;
    using Microsoft.Xrm.Sdk.Query;

    /// <summary>
    /// PostShippingClaimCreate Plugin.
    /// </summary>    
    public class PostShippingClaimCreate: Plugin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostShippingClaimCreate"/> class.
        /// </summary>
        public PostShippingClaimCreate()
            : base(typeof(PostShippingClaimCreate))
        {
            base.RegisteredEvents.Add(new Tuple<int, string, string, Action<LocalPluginContext>>(40, "Create", "gsc_sls_shippingclaim", new Action<LocalPluginContext>(ExecutePostShippingClaimCreate)));

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
        protected void ExecutePostShippingClaimCreate(LocalPluginContext localContext)
        {
            if (localContext == null)
            {
                throw new ArgumentNullException("localContext");
            }

            IPluginExecutionContext context = localContext.PluginExecutionContext;
            IOrganizationService service = localContext.OrganizationService;
            ITracingService trace = localContext.TracingService;
            Entity shippingClaimEntity = (Entity)context.InputParameters["Target"];

            string message = context.MessageName;
            string error = "";

            if (context.Depth > 1) { return; }

            try
            {
                ShippingClaimHandler shippingClaimHandler = new ShippingClaimHandler(service, trace);

                EntityCollection shippingClaimRecords = CommonHandler.RetrieveRecordsByOneValue("gsc_sls_shippingclaim", "gsc_sls_shippingclaimid", shippingClaimEntity.Id, service, null, OrderType.Ascending,
                    new[] { "gsc_receivingtransactionid" });

                if (shippingClaimRecords != null && shippingClaimRecords.Entities.Count > 0)
                {
                    Entity shippingClaim = shippingClaimRecords.Entities[0];
                    shippingClaimHandler.ReplicateReceivingTransactionDetailFields(shippingClaim);                  
                }

            }
            catch (Exception ex)
            {
                //throw new InvalidPluginExecutionException(String.Concat("(Exception)\n", ex.Message, Environment.NewLine, ex.StackTrace, Environment.NewLine, error));
                throw new InvalidPluginExecutionException(ex.Message);
            }
        }
    }
}
