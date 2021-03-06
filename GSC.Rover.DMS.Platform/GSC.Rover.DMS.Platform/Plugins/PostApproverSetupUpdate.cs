// <copyright file="PostApproverSetupUpdate.cs" company="">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author></author>
// <date>5/2/2017 2:55:40 PM</date>
// <summary>Implements the PostApproverSetupUpdate Plugin.</summary>
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
// </auto-generated>
namespace GSC.Rover.DMS.Platform.Plugins
{
    using System;
    using System.ServiceModel;
    using Microsoft.Xrm.Sdk;

    using GSC.Rover.DMS.BusinessLogic.Common;
    using GSC.Rover.DMS.BusinessLogic.ApproverSetup;

    /// <summary>
    /// PostApproverSetupUpdate Plugin.
    /// Fires when the following attributes are updated:
    /// All Attributes
    /// </summary>    
    public class PostApproverSetupUpdate: Plugin
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
        /// Initializes a new instance of the <see cref="PostApproverSetupUpdate"/> class.
        /// </summary>
        public PostApproverSetupUpdate()
            : base(typeof(PostApproverSetupUpdate))
        {
            base.RegisteredEvents.Add(new Tuple<int, string, string, Action<LocalPluginContext>>(40, "Update", "gsc_cmn_approversetup", new Action<LocalPluginContext>(ExecutePostApproverSetupUpdate)));

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
        protected void ExecutePostApproverSetupUpdate(LocalPluginContext localContext)
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
            Entity approverSetupEntity = (Entity)context.InputParameters["Target"];

            if (!(context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)) { return; }
            trace.Trace(approverSetupEntity.LogicalName);
            if (approverSetupEntity.LogicalName != "gsc_cmn_approversetup") { return; }

            if (context.Mode == 0) //Synchronous Plug-in
            {
                string message = context.MessageName;

                try
                {
                    #region Pre-images
                    Int32 preImageStatus = preImageEntity.GetAttributeValue<OptionSetValue>("statecode").Value;
                    #endregion

                    #region Post-images
                    Int32 postImageStatus = postImageEntity.GetAttributeValue<OptionSetValue>("statecode").Value;
                    #endregion

                    ApproverSetupHandler approverSetupHandler = new ApproverSetupHandler(service, trace);

                    if (preImageStatus != postImageStatus && preImageStatus == 1)
                    {
                        approverSetupHandler.RestrictDuplicateSetup(postImageEntity);
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
