// <copyright file="PostVehiclePurchaseOrderUpdate.cs" company="">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author></author>
// <date>6/30/2016 5:31:51 PM</date>
// <summary>Implements the PostVehiclePurchaseOrderUpdate Plugin.</summary>
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
// </auto-generated>
namespace GSC.Rover.DMS.Platform.Plugins
{
    using System;
    using System.ServiceModel;
    using Microsoft.Xrm.Sdk;
    using GSC.Rover.DMS.BusinessLogic.VehiclePurchaseOrder;
    using Microsoft.Xrm.Sdk.Query;


    /// <summary>
    /// PostVehiclePurchaseOrderUpdate Plugin.
    /// Fires when the following attributes are updated:
    /// All Attributes
    /// </summary>    
    public class PostVehiclePurchaseOrderUpdate: Plugin
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
        /// Initializes a new instance of the <see cref="PostVehiclePurchaseOrderUpdate"/> class.
        /// </summary>
        public PostVehiclePurchaseOrderUpdate()
            : base(typeof(PostVehiclePurchaseOrderUpdate))
        {
            base.RegisteredEvents.Add(new Tuple<int, string, string, Action<LocalPluginContext>>(40, "Update", "gsc_cmn_purchaseorder", new Action<LocalPluginContext>(ExecutePostVehiclePurchaseOrderUpdate)));

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
        protected void ExecutePostVehiclePurchaseOrderUpdate(LocalPluginContext localContext)
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

            Entity vehiclePurchaseOrder = (Entity)context.InputParameters["Target"];

            if (vehiclePurchaseOrder.LogicalName != "gsc_cmn_purchaseorder") { return; }

            if (context.Mode == 0) //synchronous plugin
            {
                try 
                {
                    VehiclePurchasOrderHandler vpoHandler = new VehiclePurchasOrderHandler(service, trace);

                    Entity purchaseOrder = service.Retrieve(vehiclePurchaseOrder.LogicalName, vehiclePurchaseOrder.Id, new ColumnSet(true));

                    #region pre images
                    var preDeliveryDate = preImageEntity.Contains("gsc_desireddate") ? preImageEntity.GetAttributeValue<DateTime>("gsc_desireddate")
                        : (DateTime?)null;
                    var preApprovalStatus = preImageEntity.Contains("gsc_approvalstatus") ? preImageEntity.GetAttributeValue<OptionSetValue>("gsc_approvalstatus").Value
                        : 0;
                    var preImagePOStatus = preImageEntity.GetAttributeValue<OptionSetValue>("gsc_vpostatus") != null
                        ? preImageEntity.GetAttributeValue<OptionSetValue>("gsc_vpostatus").Value
                        : 0;
                    #endregion

                    #region post images
                    var postDeliveryDate = postImageEntity.Contains("gsc_desireddate") ? postImageEntity.GetAttributeValue<DateTime>("gsc_desireddate")
                        : (DateTime?)null;
                    var postApprovalStatus = postImageEntity.Contains("gsc_approvalstatus") ? postImageEntity.GetAttributeValue<OptionSetValue>("gsc_approvalstatus").Value
                        : 0;
                    var postImagePOStatus = postImageEntity.GetAttributeValue<OptionSetValue>("gsc_vpostatus") != null
                        ? postImageEntity.GetAttributeValue<OptionSetValue>("gsc_vpostatus").Value
                        : 0;
                    #endregion

                    if (preDeliveryDate != postDeliveryDate)
                    {
                        if (vpoHandler.ValidateDesiredDate(postImageEntity) == false)
                            throw new InvalidPluginExecutionException("Delivery date cannot be earlier than Purchase Order creation date.");
                    }

                    //approval status changed
                    if (preApprovalStatus != postApprovalStatus)
                    {
                        if (postApprovalStatus == 100000003)
                        {
                            vpoHandler.GetLevel1ApproverEmails(postImageEntity);
                        }
                    }

                    if (preImagePOStatus != postImagePOStatus)
                    {

                        vpoHandler.AdjustProductQuantity(purchaseOrder);
                        vpoHandler.UpdatePurchaseOrderStatusCopy(postImageEntity);
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Delivery date cannot be earlier than Purchase Order creation date."))
                        throw new InvalidPluginExecutionException(ex.Message);
                    else
                        throw new InvalidPluginExecutionException(String.Concat("(Exception)\n", ex.Message, Environment.NewLine, ex.StackTrace));
                }
            }
        }
    }
}
