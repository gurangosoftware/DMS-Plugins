// <copyright file="PostQuoteChargeUpdate.cs" company="">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author></author>
// <date>3/14/2016 2:52:24 PM</date>
// <summary>Implements the PostQuoteChargeUpdate Plugin.</summary>
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
// </auto-generated>
namespace GSC.Rover.DMS.Platform.Plugins
{
    using System;
    using System.ServiceModel;
    using Microsoft.Xrm.Sdk;
    using GSC.Rover.DMS.BusinessLogic.QuoteCharge;
    using GSC.Rover.DMS.BusinessLogic.Common;
    using Microsoft.Xrm.Sdk.Query;

    /// <summary>
    /// PostQuoteChargeUpdate Plugin.
    /// Fires when the following attributes are updated:
    /// All Attributes
    /// </summary>    
    public class PostQuoteChargeUpdate: Plugin
    {
        /// <summary>
        /// Alias of the image registered for the snapshot of the 
        /// primary entity's attributes before the core platform operation executes.
        /// The image contains the following attributes:
        /// All Attributes
        /// </summary>
        private readonly string preImageAlias = "PreImage";

        /// <summary>
        /// Alias of the image registered for the snapshot of the 
        /// primary entity's attributes after the core platform operation executes.
        /// The image contains the following attributes:
        /// All Attributes
        /// 
        /// Note: Only synchronous post-event and asynchronous registered plug-ins 
        /// have PostEntityImages populated.
        /// </summary>
        private readonly string postImageAlias = "PostImage";

        /// <summary>
        /// Initializes a new instance of the <see cref="PostQuoteChargeUpdate"/> class.
        /// </summary>
        public PostQuoteChargeUpdate()
            : base(typeof(PostQuoteChargeUpdate))
        {
            base.RegisteredEvents.Add(new Tuple<int, string, string, Action<LocalPluginContext>>(40, "Update", "gsc_cmn_quotecharge", new Action<LocalPluginContext>(ExecutePostQuoteChargeUpdate)));

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
        protected void ExecutePostQuoteChargeUpdate(LocalPluginContext localContext)
        {
            if (localContext == null)
            {
                throw new ArgumentNullException("localContext");
            }

            IPluginExecutionContext context = localContext.PluginExecutionContext;

            Entity preImageEntity = (context.PreEntityImages != null && context.PreEntityImages.Contains(this.preImageAlias)) ? context.PreEntityImages[this.preImageAlias] : null;
            Entity postImageEntity = (context.PostEntityImages != null && context.PostEntityImages.Contains(this.postImageAlias)) ? context.PostEntityImages[this.postImageAlias] : null;

            IOrganizationService service = localContext.OrganizationService;
            ITracingService trace = localContext.TracingService;

            if (!(context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)) { return; }

            Entity quoteChargeEntity = (Entity)context.InputParameters["Target"];

            if (quoteChargeEntity.LogicalName != "gsc_cmn_quotecharge") { return; }

            if (context.Mode == 0) //Synchronous Plugin
            {
                try
                {
                    QuoteChargeHandler quoteChargeHandler = new QuoteChargeHandler(service, trace);

                    string message = context.MessageName;

                    var preImageChargeId = preImageEntity.Contains("gsc_chargesid")
                        ? preImageEntity.GetAttributeValue<EntityReference>("gsc_chargesid").Id
                        : Guid.Empty;

                    var postImageChargeId = postImageEntity.Contains("gsc_chargesid")
                        ? postImageEntity.GetAttributeValue<EntityReference>("gsc_chargesid").Id
                        : Guid.Empty;

                    var preActualCost = preImageEntity.Contains("gsc_actualcost")
                        ? preImageEntity.GetAttributeValue<Money>("gsc_actualcost").Value
                        : Decimal.Zero;

                    var postActualCost = postImageEntity.Contains("gsc_actualcost")
                        ? postImageEntity.GetAttributeValue<Money>("gsc_actualcost").Value
                        : Decimal.Zero; 
                    
                    var preImageFree = preImageEntity.GetAttributeValue<Boolean>("gsc_free");
                    var postImageFree = postImageEntity.GetAttributeValue<Boolean>("gsc_free");
                    
                    if (preImageChargeId != postImageChargeId)
                    {
                        quoteChargeHandler.ReplicateChargeAmount(postImageEntity, message);
                        quoteChargeHandler.SetQuoteTotalChargesAmount(postImageEntity, message);
                    }

                    if (preImageFree != postImageFree)
                    {
                        quoteChargeHandler.FreeCharges(postImageEntity);
                        quoteChargeHandler.SetQuoteTotalChargesAmount(postImageEntity, message);
                    }

                    if (preActualCost != postActualCost)
                    {
                        quoteChargeHandler.SetQuoteTotalChargesAmount(postImageEntity, message);
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
}