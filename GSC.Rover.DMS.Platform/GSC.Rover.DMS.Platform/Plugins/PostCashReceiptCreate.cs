// <copyright file="PostCashReceiptCreate.cs" company="">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author></author>
// <date>5/10/2016 2:45:37 PM</date>
// <summary>Implements the PostCashReceiptCreate Plugin.</summary>
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
    using GSC.Rover.DMS.BusinessLogic.Common;
    using GSC.Rover.DMS.BusinessLogic.CashReceipt;

    /// <summary>
    /// PostCashReceiptCreate Plugin.
    /// </summary>    
    public class PostCashReceiptCreate: Plugin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostCashReceiptCreate"/> class.
        /// </summary>
        public PostCashReceiptCreate()
            : base(typeof(PostCashReceiptCreate))
        {
            base.RegisteredEvents.Add(new Tuple<int, string, string, Action<LocalPluginContext>>(40, "Create", "gsc_sls_cashreceipt", new Action<LocalPluginContext>(ExecutePostCashReceiptCreate)));

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
        protected void ExecutePostCashReceiptCreate(LocalPluginContext localContext)
        {
            if (localContext == null)
            {
                throw new ArgumentNullException("localContext");
            }

            IPluginExecutionContext context = localContext.PluginExecutionContext;
            IOrganizationService service = localContext.OrganizationService;
            ITracingService trace = localContext.TracingService;
            Entity CashReceiptEntity = (Entity)context.InputParameters["Target"];

            
            if (CashReceiptEntity.LogicalName != "gsc_sls_cashreceipt") {
                return; 
            }

            try
            {
                Entity cashReceipt = service.Retrieve(CashReceiptEntity.LogicalName, CashReceiptEntity.Id, new ColumnSet(true));

                CashReceiptHandler cashReceiptHandler = new CashReceiptHandler(service, trace);
                cashReceiptHandler.PopulateSalesDocumentSubGrid(cashReceipt);
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException(ex.ToString());
            }

            // TODO: Implement your custom Plug-in business logic.
        }
    }
}