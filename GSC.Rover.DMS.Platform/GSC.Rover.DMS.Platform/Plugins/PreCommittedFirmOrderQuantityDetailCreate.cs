// <copyright file="PreCommittedFirmOrderQuantityDetailCreate.cs" company="">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author></author>
// <date>2/28/2017 4:19:16 PM</date>
// <summary>Implements the PreCommittedFirmOrderQuantityDetailCreate Plugin.</summary>
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
// </auto-generated>
namespace GSC.Rover.DMS.Platform.Plugins
{
    using System;
    using System.ServiceModel;
    using Microsoft.Xrm.Sdk;
    using GSC.Rover.DMS.BusinessLogic.CommittedFirmOrderQuantityDetail;
    using GSC.Rover.DMS.BusinessLogic.CommittedFirmOrderQuantity;

    /// <summary>
    /// PreCommittedFirmOrderQuantityDetailCreate Plugin.
    /// </summary>    
    public class PreCommittedFirmOrderQuantityDetailCreate: Plugin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreCommittedFirmOrderQuantityDetailCreate"/> class.
        /// </summary>
        public PreCommittedFirmOrderQuantityDetailCreate()
            : base(typeof(PreCommittedFirmOrderQuantityDetailCreate))
        {
            base.RegisteredEvents.Add(new Tuple<int, string, string, Action<LocalPluginContext>>(20, "Create", "gsc_sls_committedfirmorderquantitydetail", new Action<LocalPluginContext>(ExecutePreCommittedFirmOrderQuantityDetailCreate)));

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
        protected void ExecutePreCommittedFirmOrderQuantityDetailCreate(LocalPluginContext localContext)
        {
            if (localContext == null)
            {
                throw new ArgumentNullException("localContext");
            }

            IPluginExecutionContext context = localContext.PluginExecutionContext;
            IOrganizationService service = localContext.OrganizationService;
            ITracingService trace = localContext.TracingService;
            Entity entity = (Entity)context.InputParameters["Target"];

            string message = context.MessageName;
            string error = "";

            try
            {
                CommittedFirmOrderQuantityDetailHandler cfoQuantityDetailHandler = new CommittedFirmOrderQuantityDetailHandler(service, trace);
                cfoQuantityDetailHandler.RestrictDeleteCreate(entity);
                cfoQuantityDetailHandler.SubmitVPO(entity, message);
                cfoQuantityDetailHandler.PopulateSiteField(entity);
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException(ex.Message);
            }
        }
    }
}
