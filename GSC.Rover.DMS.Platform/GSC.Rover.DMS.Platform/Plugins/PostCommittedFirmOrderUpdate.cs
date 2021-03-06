// <copyright file="PostCommittedFirmOrderUpdate.cs" company="">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author></author>
// <date>6/9/2016 10:28:15 AM</date>
// <summary>Implements the PostCommittedFirmOrderUpdate Plugin.</summary>
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
// </auto-generated>
namespace GSC.Rover.DMS.Platform.Plugins
{
    using System;
    using System.ServiceModel;
    using Microsoft.Xrm.Sdk;
    using GSC.Rover.DMS.BusinessLogic.CommittedFirmOrder;
    using Microsoft.Xrm.Sdk.Query;

    /// <summary>
    /// PostCommittedFirmOrderUpdate Plugin.
    /// Fires when the following attributes are updated:
    /// All Attributes
    /// </summary>    
    public class PostCommittedFirmOrderUpdate: Plugin
    {
        /// <summary>
        /// Alias of the image registered for the snapshot of the 
        /// primary entity's attributes before the core platform operation executes.
        /// The image contains the following attributes:
        /// No Attributes
        /// </summary>
        private readonly string preImageAlias = "preImage";

        /// <summary>
        /// Alias of the image registered for the snapshot of the 
        /// primary entity's attributes after the core platform operation executes.
        /// The image contains the following attributes:
        /// No Attributes
        /// 
        /// Note: Only synchronous post-event and asynchronous registered plug-ins 
        /// have PostEntityImages populated.
        /// </summary>
        private readonly string postImageAlias = "postImage";

        /// <summary>
        /// Initializes a new instance of the <see cref="PostCommittedFirmOrderUpdate"/> class.
        /// </summary>
        public PostCommittedFirmOrderUpdate()
            : base(typeof(PostCommittedFirmOrderUpdate))
        {
            base.RegisteredEvents.Add(new Tuple<int, string, string, Action<LocalPluginContext>>(40, "Update", "gsc_sls_committedfirmorder", new Action<LocalPluginContext>(ExecutePostCommittedFirmOrderUpdate)));

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
        protected void ExecutePostCommittedFirmOrderUpdate(LocalPluginContext localContext)
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

            Entity cfoEntity = (Entity)context.InputParameters["Target"];

            if (cfoEntity.LogicalName != "gsc_sls_committedfirmorder") { return; }

            if (context.Mode == 0) //Synchronous Plugin
            {
                try
                {
                    CommittedFirmOrderHandler cfoHandler = new CommittedFirmOrderHandler(service, trace);

                    var preBaseModelId = preImageEntity.GetAttributeValue<EntityReference>("gsc_vehiclebasemodelid") != null
                        ? preImageEntity.GetAttributeValue<EntityReference>("gsc_vehiclebasemodelid").Id
                        : Guid.Empty;
                    var preProdId = preImageEntity.GetAttributeValue<EntityReference>("gsc_productid") != null
                        ? preImageEntity.GetAttributeValue<EntityReference>("gsc_productid").Id
                        : Guid.Empty;
                    var preColorId = preImageEntity.GetAttributeValue<EntityReference>("gsc_vehiclecolorid") != null
                        ? preImageEntity.GetAttributeValue<EntityReference>("gsc_vehiclecolorid").Id
                        : Guid.Empty;
                    var preDealerId = preImageEntity.GetAttributeValue<EntityReference>("gsc_dealerfilterid") != null
                        ? preImageEntity.GetAttributeValue<EntityReference>("gsc_dealerfilterid").Id
                        : Guid.Empty;
                    var preBranchId = preImageEntity.GetAttributeValue<EntityReference>("gsc_branchfilterid") != null
                        ? preImageEntity.GetAttributeValue<EntityReference>("gsc_branchfilterid").Id
                        : Guid.Empty;
                    var preSiteId = preImageEntity.GetAttributeValue<EntityReference>("gsc_siteid") != null
                        ? preImageEntity.GetAttributeValue<EntityReference>("gsc_siteid").Id
                        : Guid.Empty;
                    var preGenerate = preImageEntity.GetAttributeValue<Boolean>("gsc_generatecfoquantity");


                    var postBaseModelId = postImageEntity.GetAttributeValue<EntityReference>("gsc_vehiclebasemodelid") != null
                        ? postImageEntity.GetAttributeValue<EntityReference>("gsc_vehiclebasemodelid").Id
                        : Guid.Empty;
                    var postProdId = postImageEntity.GetAttributeValue<EntityReference>("gsc_productid") != null
                        ? postImageEntity.GetAttributeValue<EntityReference>("gsc_productid").Id
                        : Guid.Empty;
                    var postColorId = postImageEntity.GetAttributeValue<EntityReference>("gsc_vehiclecolorid") != null
                        ? postImageEntity.GetAttributeValue<EntityReference>("gsc_vehiclecolorid").Id
                        : Guid.Empty;
                    var postDealerId = postImageEntity.GetAttributeValue<EntityReference>("gsc_dealerfilterid") != null
                        ? postImageEntity.GetAttributeValue<EntityReference>("gsc_dealerfilterid").Id
                        : Guid.Empty;
                    var postBranchId = postImageEntity.GetAttributeValue<EntityReference>("gsc_branchfilterid") != null
                        ? postImageEntity.GetAttributeValue<EntityReference>("gsc_branchfilterid").Id
                        : Guid.Empty;
                    var postSiteId = postImageEntity.GetAttributeValue<EntityReference>("gsc_siteid") != null
                        ? postImageEntity.GetAttributeValue<EntityReference>("gsc_siteid").Id
                        : Guid.Empty;
                    var postGenerate = postImageEntity.GetAttributeValue<Boolean>("gsc_generatecfoquantity");


                    if (preBaseModelId != postBaseModelId || preProdId != postProdId || preColorId != postColorId)
                    {
                        cfoHandler.DeleteSuggestedCFODetails(postImageEntity);
                        cfoHandler.SuggestCFOQuantity(postImageEntity);
                    }

                    if (preGenerate != postGenerate)
                    {
                        cfoHandler.GenerateCFOQuantity(postImageEntity);
                    }



                }

                catch (Exception ex)
                {
                    throw new InvalidPluginExecutionException(ex.Message);
                }
            }
        }
    }
}
