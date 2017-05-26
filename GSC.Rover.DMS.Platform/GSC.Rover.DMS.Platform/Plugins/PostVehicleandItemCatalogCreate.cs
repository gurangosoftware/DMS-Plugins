// <copyright file="PostVehicleandItemCatalogCreate.cs" company="">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author></author>
// <date>10/7/2016 10:38:57 AM</date>
// <summary>Implements the PostVehicleandItemCatalogCreate Plugin.</summary>
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
// </auto-generated>
namespace GSC.Rover.DMS.Platform.Plugins
{
    using System;
    using System.ServiceModel;
    using Microsoft.Xrm.Sdk;
    using GSC.Rover.DMS.BusinessLogic.Product;

    /// <summary>
    /// PostVehicleandItemCatalogCreate Plugin.
    /// </summary>    
    public class PostVehicleandItemCatalogCreate: Plugin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostVehicleandItemCatalogCreate"/> class.
        /// </summary>
        public PostVehicleandItemCatalogCreate()
            : base(typeof(PostVehicleandItemCatalogCreate))
        {
            base.RegisteredEvents.Add(new Tuple<int, string, string, Action<LocalPluginContext>>(40, "Create", "product", new Action<LocalPluginContext>(ExecutePostVehicleandItemCatalogCreate)));

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
        protected void ExecutePostVehicleandItemCatalogCreate(LocalPluginContext localContext)
        {
            if (localContext == null)
            {
                throw new ArgumentNullException("localContext");
            }

            IPluginExecutionContext context = localContext.PluginExecutionContext;
            IOrganizationService service = localContext.OrganizationService;
            ITracingService trace = localContext.TracingService;
            Entity product = (Entity)context.InputParameters["Target"];

            string message = context.MessageName;
            string error = "";

            try
            {
                ProductHandler productHandler = new ProductHandler(service, trace);

                productHandler.ReplicateModelDescriptionName(product);
                productHandler.PopulateTaxRate(product);
            }
            catch (Exception ex)
            {
                //throw new InvalidPluginExecutionException(String.Concat("(Exception)\n", ex.Message, Environment.NewLine, ex.StackTrace, Environment.NewLine, error));
                throw new InvalidPluginExecutionException(ex.Message);
            }
        }
    }
}
