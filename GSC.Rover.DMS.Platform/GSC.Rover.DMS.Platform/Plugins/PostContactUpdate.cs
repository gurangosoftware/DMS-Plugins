// <copyright file="PostContactUpdate.cs" company="">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author></author>
// <date>10/6/2016 1:25:09 PM</date>
// <summary>Implements the PostContactUpdate Plugin.</summary>
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
// </auto-generated>
namespace GSC.Rover.DMS.Platform.Plugins
{
    using System;
    using System.ServiceModel;
    using Microsoft.Xrm.Sdk;

    using GSC.Rover.DMS.BusinessLogic.Contact;
    using GSC.Rover.DMS.BusinessLogic.Common;

    /// <summary>
    /// PostContactUpdate Plugin.
    /// Fires when the following attributes are updated:
    /// All Attributes
    /// </summary>    
    public class PostContactUpdate: Plugin
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
        /// Initializes a new instance of the <see cref="PostContactUpdate"/> class.
        /// </summary>
        public PostContactUpdate()
            : base(typeof(PostContactUpdate))
        {
            base.RegisteredEvents.Add(new Tuple<int, string, string, Action<LocalPluginContext>>(40, "Update", "contact", new Action<LocalPluginContext>(ExecutePostContactUpdate)));

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
        protected void ExecutePostContactUpdate(LocalPluginContext localContext)
        {
            if (localContext == null)
            {
                throw new ArgumentNullException("localContext");
            }

            IPluginExecutionContext context = localContext.PluginExecutionContext;
            IOrganizationService service = localContext.OrganizationService;
            ITracingService trace = localContext.TracingService;

            Entity preImageEntity = (context.PreEntityImages != null && context.PreEntityImages.Contains(this.preImageAlias)) ? context.PreEntityImages[this.preImageAlias] : null;
            Entity postImageEntity = (context.PostEntityImages != null && context.PostEntityImages.Contains(this.postImageAlias)) ? context.PostEntityImages[this.postImageAlias] : null;

            if (!(context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)) { return; }

            if (postImageEntity.LogicalName != "contact") { return; }
            Entity contactEntity = (Entity)context.InputParameters["Target"];

            string message = context.MessageName;

            if (context.Mode == 0) //Synchronous plugin
            {
                try
                {
                    ContactHandler contactHandler = new ContactHandler(service, trace);

                    #region Pre-images
                    Guid preImageTaxId = preImageEntity.GetAttributeValue<EntityReference>("gsc_taxid") != null
                        ? preImageEntity.GetAttributeValue<EntityReference>("gsc_taxid").Id
                        : Guid.Empty;

                    Guid preImageThemeId = preImageEntity.GetAttributeValue<EntityReference>("gsc_themes") != null
                     ? preImageEntity.GetAttributeValue<EntityReference>("gsc_themes").Id
                     : Guid.Empty;

                    var preImageMobile = preImageEntity.Contains("mobilephone")
                     ? preImageEntity.GetAttributeValue<String>("mobilephone")
                     : String.Empty;

                    var preImageEmail = preImageEntity.Contains("emailaddress1")
                     ? preImageEntity.GetAttributeValue<String>("emailaddress1")
                     : String.Empty;

                    var preImageRelation = preImageEntity.Contains("gsc_contactrelation")
                     ? preImageEntity.GetAttributeValue<String>("gsc_contactrelation")
                     : String.Empty;

                    var preImageFirstName = preImageEntity.Contains("firstname") ? preImageEntity.GetAttributeValue<string>("firstname")
                        : String.Empty;

                    var preImageLastName = preImageEntity.Contains("lastname") ? preImageEntity.GetAttributeValue<string>("lastname")
                        : String.Empty;

                    var preImageBirthdate = preImageEntity.Contains("birthdate") ? preImageEntity.GetAttributeValue<DateTime?>("birthdate")
                        : (DateTime?)null;

                    var preImageCityName = preImageEntity.Contains("gsc_cityname") ? preImageEntity.GetAttributeValue<string>("gsc_cityname")
                        : String.Empty;

                    var preImageCityIdName = preImageEntity.Contains("gsc_cityid") ? preImageEntity.GetAttributeValue<EntityReference>("gsc_cityid").Name
                        : String.Empty;
                    #endregion

                    #region Post-images
                    Guid postImageTaxId = postImageEntity.GetAttributeValue<EntityReference>("gsc_taxid") != null
                        ? postImageEntity.GetAttributeValue<EntityReference>("gsc_taxid").Id
                        : Guid.Empty;

                    Guid postImageThemeId = postImageEntity.GetAttributeValue<EntityReference>("gsc_themes") != null
                      ? postImageEntity.GetAttributeValue<EntityReference>("gsc_themes").Id
                      : Guid.Empty;

                    var postImageMobile = postImageEntity.Contains("mobilephone")
                     ? postImageEntity.GetAttributeValue<String>("mobilephone")
                     : String.Empty;

                    var postImageEmail = postImageEntity.Contains("emailaddress1")
                     ? postImageEntity.GetAttributeValue<String>("emailaddress1")
                     : String.Empty;

                    var postImageRelation = postImageEntity.Contains("gsc_contactrelation")
                     ? postImageEntity.GetAttributeValue<String>("gsc_contactrelation")
                     : String.Empty;

                    var postImageFirstName = postImageEntity.Contains("firstname") ? postImageEntity.GetAttributeValue<string>("firstname")
                        : String.Empty;

                    var postImageLastName = postImageEntity.Contains("lastname") ? postImageEntity.GetAttributeValue<string>("lastname")
                        : String.Empty;

                    var postImageBirthdate = postImageEntity.Contains("birthdate") ? postImageEntity.GetAttributeValue<DateTime?>("birthdate")
                        : (DateTime?)null;

                    var postImageCityName = postImageEntity.Contains("gsc_cityname") ? postImageEntity.GetAttributeValue<string>("gsc_cityname")
                        : String.Empty;

                    var postImageCityIdName = postImageEntity.Contains("gsc_cityid") ? postImageEntity.GetAttributeValue<EntityReference>("gsc_cityid").Name
                        : String.Empty;

                    #endregion

                    if (preImageFirstName != postImageFirstName || preImageLastName != postImageLastName || preImageMobile != postImageMobile || preImageBirthdate != postImageBirthdate)
                    {
                        contactHandler.CheckForExistingRecords(postImageEntity);
                    }

                    if (preImageTaxId != postImageTaxId)
                    {
                        contactHandler.PopulateTaxRate(postImageEntity);
                    }

                    if (preImageThemeId != postImageThemeId)
                    {
                        contactHandler.ChangeThemeUrl(postImageEntity);
                    }

                    if (preImageMobile != postImageMobile || preImageEmail != postImageEmail || preImageRelation != postImageRelation)
                    {
                        if (postImageEntity.Contains("gsc_recordtype") && postImageEntity.GetAttributeValue<OptionSetValue>("gsc_recordtype").Value == 100000003)
                        {
                            contactHandler.UpdatePrimaryContactDetails(postImageEntity);
                        }
                    }

                    if (postImageCityName != postImageCityIdName && postImageCityName != String.Empty && preImageCityIdName == postImageCityIdName)
                    {
                        contactHandler.SetCity(postImageEntity);
                    }
                    
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("This record has been identified as a fraud account. Please ask the customer to provide further information."))
                        throw new InvalidPluginExecutionException("This record has been identified as a fraud account. Please ask the customer to provide further information.");
                    else if (ex.Message.Contains("This record already exists."))
                        throw new InvalidPluginExecutionException("This record already exists.");
                    else
                    throw new InvalidPluginExecutionException(String.Concat("(Exception)\n", ex.Message, Environment.NewLine, ex.StackTrace));
                }
            }
        }
    }
}
