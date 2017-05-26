// <copyright file="PreValidateAccountDelete.cs" company="">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author></author>
// <date>1/23/2017 3:20:46 PM</date>
// <summary>Implements the PreValidateAccountDelete Plugin.</summary>
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
    using GSC.Rover.DMS.BusinessLogic.Account;
    using Microsoft.Xrm.Sdk.Query;
    /// <summary>
    /// PreValidateAccountDelete Plugin.
    /// </summary>    
    public class PreValidateAccountDelete: Plugin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreValidateAccountDelete"/> class.
        /// </summary>
        public PreValidateAccountDelete()
            : base(typeof(PreValidateAccountDelete))
        {
            base.RegisteredEvents.Add(new Tuple<int, string, string, Action<LocalPluginContext>>(10, "Delete", "account", new Action<LocalPluginContext>(ExecutePreValidateAccountDelete)));

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
        protected void ExecutePreValidateAccountDelete(LocalPluginContext localContext)
        {
            if (localContext == null)
            {
                throw new ArgumentNullException("localContext");
            }

            IPluginExecutionContext context = localContext.PluginExecutionContext;
            IOrganizationService service = localContext.OrganizationService;
            ITracingService trace = localContext.TracingService;
            var accountTargetEntity = (EntityReference)context.InputParameters["Target"];
            string message = context.MessageName;
            string error = "";

            if (context.Depth > 1) { return; }

            try
            {
                EntityCollection accountEC = CommonHandler.RetrieveRecordsByOneValue("account", "accountid", accountTargetEntity.Id, service,
                null, OrderType.Ascending, new[] { "accountid", "gsc_ispotential" });
                AccountHandler accountHandler = new AccountHandler(service, trace);
                Entity accountEntity = accountEC.Entities[0];
                Boolean isProspect = accountEntity.GetAttributeValue<Boolean>("gsc_ispotential");
                if (isProspect == true)
                {
                    if (accountHandler.IsUsedInTransaction(accountEntity) == true)
                    {
                        throw new InvalidPluginExecutionException("Unable to delete prospect record(s) used in transaction");
                    }
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unable to delete prospect record(s) used in transaction"))
                    throw new InvalidPluginExecutionException(ex.Message);
                else
                    throw new InvalidPluginExecutionException(String.Concat("(Exception)\n", ex.Message, Environment.NewLine, ex.StackTrace, Environment.NewLine, error));
            }
        }
    }
}
