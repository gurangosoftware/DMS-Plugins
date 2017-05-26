// <copyright file="PostOrderUpdate.cs" company="">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author></author>
// <date>3/8/2016 2:34:34 PM</date>
// <summary>Implements the PostOrderUpdate Plugin.</summary>
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
// </auto-generated>
namespace GSC.Rover.DMS.Platform.Plugins
{
    using System;
    using System.ServiceModel;
    using Microsoft.Xrm.Sdk;
    using GSC.Rover.DMS.BusinessLogic.SalesOrder;

    /// <summary>
    /// PostOrderUpdate Plugin.
    /// Fires when the following attributes are updated:
    /// All Attributes
    /// </summary>    
    public class PostOrderUpdate: Plugin
    {
        /// <summary>
        /// Alias of the image registered for the snapshot of the 
        /// primary entity's attributes before the core platform operation executes.
        /// The image contains the following attributes:
        /// All Attributes
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
        /// Initializes a new instance of the <see cref="PostOrderUpdate"/> class.
        /// </summary>
        public PostOrderUpdate()
            : base(typeof(PostOrderUpdate))
        {
            base.RegisteredEvents.Add(new Tuple<int, string, string, Action<LocalPluginContext>>(40, "Update", "salesorder", new Action<LocalPluginContext>(ExecutePostOrderUpdate)));

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
        protected void ExecutePostOrderUpdate(LocalPluginContext localContext)
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
            Entity salesOrder = (Entity)context.InputParameters["Target"];

            if (!(context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)) { return; }

            if (salesOrder.LogicalName != "salesorder") { return; }

            if (context.Mode == 0) //Synchronous Plug-in
            {
                string message = context.MessageName;

                try
                {
                    #region Pre-images
                    var preImageCustomerId = preImageEntity.GetAttributeValue<EntityReference>("customerid") != null
                        ? preImageEntity.GetAttributeValue<EntityReference>("customerid").Id
                        : Guid.Empty;

                    var preImageProductId = preImageEntity.GetAttributeValue<EntityReference>("gsc_productid") != null
                        ? preImageEntity.GetAttributeValue<EntityReference>("gsc_productid").Id
                        : Guid.Empty;

                    var preImageBankId = preImageEntity.GetAttributeValue<EntityReference>("gsc_bankid") != null
                        ? preImageEntity.GetAttributeValue<EntityReference>("gsc_bankid").Id
                        : Guid.Empty;

                    var preImageUnitPrice = preImageEntity.GetAttributeValue<Money>("gsc_unitprice") != null
                        ? preImageEntity.GetAttributeValue<Money>("gsc_unitprice").Value
                        : Decimal.Zero;

                    var preImageDpAmount = preImageEntity.GetAttributeValue<Money>("gsc_applytodpamount") != null
                        ? preImageEntity.GetAttributeValue<Money>("gsc_applytodpamount").Value
                        : Decimal.Zero;

                    var preImageAfAmount = preImageEntity.GetAttributeValue<Money>("gsc_applytoafamount") != null
                        ? preImageEntity.GetAttributeValue<Money>("gsc_applytoafamount").Value
                        : Decimal.Zero;

                    var preImageUpAmount = preImageEntity.GetAttributeValue<Money>("gsc_applytoupamount") != null
                        ? preImageEntity.GetAttributeValue<Money>("gsc_applytoupamount").Value
                        : Decimal.Zero;

                    var preImagePreferredColor1 = preImageEntity.GetAttributeValue<EntityReference>("gsc_vehiclecolorid1") != null
                        ? preImageEntity.GetAttributeValue<EntityReference>("gsc_vehiclecolorid1").Id
                        : Guid.Empty;

                    var preImagePreferredColor2 = preImageEntity.GetAttributeValue<EntityReference>("gsc_vehiclecolorid2") != null
                        ? preImageEntity.GetAttributeValue<EntityReference>("gsc_vehiclecolorid2").Id
                        : Guid.Empty;

                    var preImagePreferredColor3 = preImageEntity.GetAttributeValue<EntityReference>("gsc_vehiclecolorid3") != null
                        ? preImageEntity.GetAttributeValue<EntityReference>("gsc_vehiclecolorid3").Id
                        : Guid.Empty;

                    var preImageFreeChattelFee = preImageEntity.GetAttributeValue<Boolean>("gsc_freechattelfee");

                    var preImageTotalInsurance = preImageEntity.GetAttributeValue<Money>("gsc_totalinsurancecharges") != null
                        ? preImageEntity.GetAttributeValue<Money>("gsc_totalinsurancecharges").Value
                        : Decimal.Zero;

                    var preImageFinancingScheme = preImageEntity.GetAttributeValue<EntityReference>("gsc_financingschemeid") != null
                        ? preImageEntity.GetAttributeValue<EntityReference>("gsc_financingschemeid").Id
                        : Guid.Empty;

                    var preImageNetDownPayment = preImageEntity.GetAttributeValue<Money>("gsc_netdownpayment") != null
                        ? preImageEntity.GetAttributeValue<Money>("gsc_netdownpayment").Value
                        : Decimal.Zero;

                    var preImageIsRequestForAllocation = preImageEntity.GetAttributeValue<Boolean>("gsc_isrequestforallocation");

                    var preImageInventoryIdforAllocation = preImageEntity.GetAttributeValue<String>("gsc_inventoryidtoallocate") != null
                       ? preImageEntity.GetAttributeValue<String>("gsc_inventoryidtoallocate")
                       : String.Empty;

                    var preImageChattelFeeEditable = preImageEntity.GetAttributeValue<Money>("gsc_chattelfeeeditable") != null
                        ? preImageEntity.GetAttributeValue<Money>("gsc_chattelfeeeditable").Value
                        : Decimal.Zero;

                    var preImagePaymentMode = preImageEntity.GetAttributeValue<OptionSetValue>("gsc_paymentmode") != null
                        ? preImageEntity.GetAttributeValue<OptionSetValue>("gsc_paymentmode").Value
                        : 0;

                    var preImageChattelFee = preImageEntity.GetAttributeValue<Money>("gsc_chattelfee") != null
                        ? preImageEntity.GetAttributeValue<Money>("gsc_chattelfee").Value
                        : Decimal.Zero;

                    var preImageInsurance = preImageEntity.GetAttributeValue<Money>("gsc_insurance") != null
                        ? preImageEntity.GetAttributeValue<Money>("gsc_insurance").Value
                        : Decimal.Zero;

                    var preImageOtherCharges = preImageEntity.GetAttributeValue<Money>("gsc_othercharges") != null
                        ? preImageEntity.GetAttributeValue<Money>("gsc_othercharges").Value
                        : Decimal.Zero;

                    var preImageDownPaymentAmount = preImageEntity.GetAttributeValue<Money>("gsc_downpaymentamount") != null
                        ? preImageEntity.GetAttributeValue<Money>("gsc_downpaymentamount").Value
                        : Decimal.Zero;

                    var preImageStatus = preImageEntity.Contains("gsc_status")
                        ? preImageEntity.GetAttributeValue<OptionSetValue>("gsc_status").Value
                        : Decimal.Zero;

                    var preImageReservation = preImageEntity.GetAttributeValue<Money>("gsc_reservationfee") != null
                        ? preImageEntity.GetAttributeValue<Money>("gsc_reservationfee").Value
                        : Decimal.Zero;

                    var preImageAllocatedItemstoDelete = preImageEntity.Contains("gsc_allocateditemstodelete")
                        ? preImageEntity.GetAttributeValue<String>("gsc_allocateditemstodelete")
                        : String.Empty;

                    var preImageIsReadyPDI = preImageEntity.GetAttributeValue<Boolean>("gsc_isreadyforpdi");

                    var preImageIsTransferforInvoice = preImageEntity.GetAttributeValue<Boolean>("gsc_istransferforinvoicing");

                    #endregion

                    #region Post-images
                    var postImageCustomerId = postImageEntity.GetAttributeValue<EntityReference>("customerid") != null
                        ? postImageEntity.GetAttributeValue<EntityReference>("customerid").Id
                        : Guid.Empty;

                    var postImageProductId = postImageEntity.GetAttributeValue<EntityReference>("gsc_productid") != null
                        ? postImageEntity.GetAttributeValue<EntityReference>("gsc_productid").Id
                        : Guid.Empty;

                    var postImageBankId = postImageEntity.GetAttributeValue<EntityReference>("gsc_bankid") != null
                        ? postImageEntity.GetAttributeValue<EntityReference>("gsc_bankid").Id
                        : Guid.Empty;

                    var postImageUnitPrice = postImageEntity.GetAttributeValue<Money>("gsc_unitprice") != null
                        ? postImageEntity.GetAttributeValue<Money>("gsc_unitprice").Value
                        : Decimal.Zero;

                    var postImageDpAmount = postImageEntity.GetAttributeValue<Money>("gsc_applytodpamount") != null
                        ? postImageEntity.GetAttributeValue<Money>("gsc_applytodpamount").Value
                        : Decimal.Zero;

                    var postImageAfAmount = postImageEntity.GetAttributeValue<Money>("gsc_applytoafamount") != null
                        ? postImageEntity.GetAttributeValue<Money>("gsc_applytoafamount").Value
                        : Decimal.Zero;

                    var postImageUpAmount = postImageEntity.GetAttributeValue<Money>("gsc_applytoupamount") != null
                        ? postImageEntity.GetAttributeValue<Money>("gsc_applytoupamount").Value
                        : Decimal.Zero;

                    var postImagePreferredColor1 = postImageEntity.GetAttributeValue<EntityReference>("gsc_vehiclecolorid1") != null
                        ? postImageEntity.GetAttributeValue<EntityReference>("gsc_vehiclecolorid1").Id
                        : Guid.Empty;

                    var postImagePreferredColor2 = postImageEntity.GetAttributeValue<EntityReference>("gsc_vehiclecolorid2") != null
                        ? postImageEntity.GetAttributeValue<EntityReference>("gsc_vehiclecolorid2").Id
                        : Guid.Empty;

                    var postImagePreferredColor3 = postImageEntity.GetAttributeValue<EntityReference>("gsc_vehiclecolorid3") != null
                        ? postImageEntity.GetAttributeValue<EntityReference>("gsc_vehiclecolorid3").Id
                        : Guid.Empty;

                    var postImageFreeChattelFee = postImageEntity.GetAttributeValue<Boolean>("gsc_freechattelfee");

                    var postImageTotalInsurance = postImageEntity.GetAttributeValue<Money>("gsc_totalinsurancecharges") != null
                        ? postImageEntity.GetAttributeValue<Money>("gsc_totalinsurancecharges").Value
                        : Decimal.Zero;

                    var postImageFinancingScheme = postImageEntity.GetAttributeValue<EntityReference>("gsc_financingschemeid") != null
                        ? postImageEntity.GetAttributeValue<EntityReference>("gsc_financingschemeid").Id
                        : Guid.Empty;

                    var postImageNetDownPayment = postImageEntity.GetAttributeValue<Money>("gsc_netdownpayment") != null
                        ? postImageEntity.GetAttributeValue<Money>("gsc_netdownpayment").Value
                        : Decimal.Zero;

                    var postImageIsRequestForAllocation = postImageEntity.GetAttributeValue<Boolean>("gsc_isrequestforallocation");

                    var postImageInventoryIdforAllocation = postImageEntity.GetAttributeValue<String>("gsc_inventoryidtoallocate") != null
                       ? postImageEntity.GetAttributeValue<String>("gsc_inventoryidtoallocate")
                       : String.Empty;

                    var postImageChattelFeeEditable = postImageEntity.GetAttributeValue<Money>("gsc_chattelfeeeditable") != null
                        ? postImageEntity.GetAttributeValue<Money>("gsc_chattelfeeeditable").Value
                        : Decimal.Zero;

                    var postImagePaymentMode = postImageEntity.GetAttributeValue<OptionSetValue>("gsc_paymentmode") != null
                        ? postImageEntity.GetAttributeValue<OptionSetValue>("gsc_paymentmode").Value
                        : 0;

                    var postImageChattelFee = postImageEntity.GetAttributeValue<Money>("gsc_chattelfee") != null
                        ? postImageEntity.GetAttributeValue<Money>("gsc_chattelfee").Value
                        : Decimal.Zero;

                    var postImageInsurance = postImageEntity.GetAttributeValue<Money>("gsc_insurance") != null
                        ? postImageEntity.GetAttributeValue<Money>("gsc_insurance").Value
                        : Decimal.Zero;

                    var postImageOtherCharges = postImageEntity.GetAttributeValue<Money>("gsc_othercharges") != null
                        ? postImageEntity.GetAttributeValue<Money>("gsc_othercharges").Value
                        : Decimal.Zero;

                    var postImageDownPaymentAmount = postImageEntity.GetAttributeValue<Money>("gsc_downpaymentamount") != null
                        ? postImageEntity.GetAttributeValue<Money>("gsc_downpaymentamount").Value
                        : Decimal.Zero;

                    var postImageStatus = postImageEntity.GetAttributeValue<OptionSetValue>("gsc_status") != null
                        ? postImageEntity.GetAttributeValue<OptionSetValue>("gsc_status").Value
                        : Decimal.Zero;

                    var postImageReservation = postImageEntity.GetAttributeValue<Money>("gsc_reservationfee") != null
                        ? postImageEntity.GetAttributeValue<Money>("gsc_reservationfee").Value
                        : Decimal.Zero;

                    var postImageAllocatedItemstoDelete = postImageEntity.Contains("gsc_allocateditemstodelete")
                        ? postImageEntity.GetAttributeValue<String>("gsc_allocateditemstodelete")
                        : String.Empty;

                    var postImageIsTransferforInvoice = postImageEntity.GetAttributeValue<Boolean>("gsc_istransferforinvoicing");

                    var postImageIsReadyPDI = postImageEntity.GetAttributeValue<Boolean>("gsc_isreadyforpdi");

                    #endregion

                    SalesOrderHandler salesOrderHandler = new SalesOrderHandler(service, trace);

                    if (preImageCustomerId != postImageCustomerId)
                    {
                        salesOrderHandler.PopulateCustomerInfo(postImageEntity);
                    }

                    //Function triggered on Payment Mode change
                    if (preImagePaymentMode != postImagePaymentMode)
                    {
                        salesOrderHandler.SetNetPriceAmount(postImageEntity, message);
                        salesOrderHandler.SetChattelFeeAmount(postImageEntity, message);
                    }

                    //Functions triggered on Product change
                    if (preImageProductId != postImageProductId)
                    {
                        salesOrderHandler.ReplicateVehicleDetails(postImageEntity, message);
                        salesOrderHandler.DeleteExistingVehicleFreebies(postImageEntity);
                        salesOrderHandler.DeleteOrderCabChassis(postImageEntity);
                        salesOrderHandler.SetNetPriceAmount(postImageEntity, message);

                    }

                    //Functions triggered on Unit Price, Bank, or Free Chattel Fee change
                    if (preImageBankId != postImageBankId || preImageUnitPrice != postImageUnitPrice || preImageFreeChattelFee != postImageFreeChattelFee)
                    {
                        salesOrderHandler.SetChattelFeeAmount(postImageEntity, message);
                    }

                    //Functions triggered on Chattel Fee (gsc_chattelfeeeditable) change
                    if (preImageChattelFeeEditable != postImageChattelFeeEditable)
                    {
                        salesOrderHandler.ReplicateEditableChattelFee(postImageEntity);
                    }

                    //Functions triggered on Apply Amount fields
                    if (preImageDpAmount != postImageDpAmount || preImageAfAmount != postImageAfAmount || preImageUpAmount != postImageUpAmount || preImageDownPaymentAmount != postImageDownPaymentAmount)
                    {
                        salesOrderHandler.SetLessDiscountValues(postImageEntity, message);
                    }

                    if (preImageTotalInsurance != postImageTotalInsurance)
                    {
                        salesOrderHandler.UpdateInsurance(postImageEntity);
                    }

                    //Function triggered Preferred Color change
                    if (preImagePreferredColor1 != postImagePreferredColor1)
                    {
                        salesOrderHandler.SetVehicleColorAmount(postImageEntity, message);
                    }

                    if (preImageFinancingScheme != postImageFinancingScheme)
                    {
                        salesOrderHandler.DeleteExistingMonthlyAmortizationRecords(postImageEntity);
                    }

                    //Functions triggered on Net Down Payment or Total Premium change
                    if (preImageNetDownPayment != postImageNetDownPayment)
                    {
                        salesOrderHandler.SetNetPriceAmount(postImageEntity, message);
                    }

                    if (preImageBankId != postImageBankId)
                    {
                        salesOrderHandler.DeleteExistingRequirementChecklist(preImageEntity);
                        salesOrderHandler.CreateRequirementChecklist(postImageEntity, message);
                    }

                    //Functions triggered on IsRequestForAllocation change
                    if (preImageIsRequestForAllocation != postImageIsRequestForAllocation)
                    {
                        if (salesOrderHandler.CheckifHasVehicleAllocator(postImageEntity) == false)
                            throw new InvalidPluginExecutionException("There is no setup for vehicle allocator.");
                        else
                            salesOrderHandler.SetRequestAllocationDate(postImageEntity);
                    }

                    //Functions triggered on InventoryIdforAllocation change
                    if (preImageInventoryIdforAllocation != postImageInventoryIdforAllocation)
                    {
                        salesOrderHandler.AllocateVehicle(postImageEntity);
                    }

                    //Functions triggered on Net Down Payment, Chattel Fee, Insurance Charges, and Other Charges field on Summary section
                    if (preImageNetDownPayment != postImageNetDownPayment || preImageChattelFee != postImageChattelFee || preImageInsurance != postImageInsurance || preImageOtherCharges != postImageOtherCharges)
                    {
                        salesOrderHandler.SetTotalCashOutlayAmount(postImageEntity, message);
                    }

                    //Function triggered on change of status
                    if (preImageStatus != postImageStatus)
                    {
                        salesOrderHandler.UpdateStatus(postImageEntity);
                        salesOrderHandler.UnAllocateVehicle(postImageEntity);
                    }

                    //Function triggered on Update gsc_allocateditemstodelete
                    if (preImageAllocatedItemstoDelete != postImageAllocatedItemstoDelete)
                    {
                        salesOrderHandler.DeleteVehicleAllocatedItems(postImageEntity);
                    }

                    //Function trigger on Update gsc_istransferforinvoicing
                    if (preImageIsTransferforInvoice != postImageIsTransferforInvoice)
                    {
                        salesOrderHandler.SetTransferInvoiceDate(postImageEntity);
                    }

                    if (preImageReservation != postImageReservation)
                    {
                        salesOrderHandler.UpdateReservation(postImageEntity);
                    }

                    //Recompute Unit Price when Markup % Changed - Leslie Baliguat 05/08/17
                    var preMarkup = preImageEntity.Contains("gsc_markup")
                           ? preImageEntity.GetAttributeValue<Double>("gsc_markup")
                           : 0;
                    var postMarkup = postImageEntity.Contains("gsc_markup")
                           ? postImageEntity.GetAttributeValue<Double>("gsc_markup")
                           : 0;

                    if (preMarkup != postMarkup)
                    {
                        salesOrderHandler.UpdateGovernmentTax(postImageEntity, message);
                    }

                    if (preImageIsReadyPDI != postImageIsReadyPDI)
                    {
                        salesOrderHandler.UpdatePDIStatus(postImageEntity);
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