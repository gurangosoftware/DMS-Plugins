// <copyright file="PreValidateInvoiceDelete.cs" company="">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author></author>
// <date>1/21/2017 4:08:59 PM</date>
// <summary>Implements the PreValidateInvoiceDelete Plugin.</summary>
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
// </auto-generated>
namespace GSC.Rover.DMS.Platform.Plugins
{
    using System;
    using System.ServiceModel;
    using Microsoft.Xrm.Sdk;

    using BusinessLogic.Common;
    using BusinessLogic.Invoice;
    using Microsoft.Xrm.Sdk.Query;

    /// <summary>
    /// PreValidateInvoiceDelete Plugin.
    /// </summary>    
    public class PreValidateInvoiceDelete: Plugin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreValidateInvoiceDelete"/> class.
        /// </summary>
        public PreValidateInvoiceDelete()
            : base(typeof(PreValidateInvoiceDelete))
        {
            base.RegisteredEvents.Add(new Tuple<int, string, string, Action<LocalPluginContext>>(10, "Delete", "invoice", new Action<LocalPluginContext>(ExecutePreValidateInvoiceDelete)));

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
        protected void ExecutePreValidateInvoiceDelete(LocalPluginContext localContext)
        {
            if (localContext == null)
            {
                throw new ArgumentNullException("localContext");
            }

            IPluginExecutionContext context = localContext.PluginExecutionContext;
            IOrganizationService service = localContext.OrganizationService;
            ITracingService trace = localContext.TracingService;
            var SalesInvoiceTargetEntity = (EntityReference)context.InputParameters["Target"];
            string message = context.MessageName;
            string error = "";

            if (context.Depth > 1) { return; }

            try
            {
                InvoiceHandler InvoiceHandler = new InvoiceHandler(service, trace);

                EntityCollection InvoiceCollection = CommonHandler.RetrieveRecordsByOneValue(SalesInvoiceTargetEntity.LogicalName, "invoiceid", SalesInvoiceTargetEntity.Id, service,
                    null, OrderType.Ascending, new[] { "invoiceid", "gsc_salesinvoicestatus", "salesorderid" });

                if (InvoiceCollection.Entities.Count > 0)
                {
                    if (InvoiceHandler.ValidateDelete(InvoiceCollection.Entities[0]) == true)
                    {
                        throw new InvalidPluginExecutionException("Unable to delete invoice record(s). Only record(s) with open status can be deleted.");
                    }
                    InvoiceHandler.DeleteOpenInvoice(InvoiceCollection.Entities[0]);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unable to delete invoice record(s). Only record(s) with open status can be deleted."))
                    throw new InvalidPluginExecutionException(ex.Message);
                else
                    throw new InvalidPluginExecutionException(String.Concat("(Exception)\n", ex.Message, Environment.NewLine, ex.StackTrace, Environment.NewLine, error));
            }
        }
    }
}
