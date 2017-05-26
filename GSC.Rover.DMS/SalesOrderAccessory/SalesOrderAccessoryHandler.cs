﻿using GSC.Rover.DMS.BusinessLogic.Common;
using GSC.Rover.DMS.BusinessLogic.SalesOrder;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSC.Rover.DMS.BusinessLogic.SalesOrderAccessory
{
    public class SalesOrderAccessoryHandler
    {
        private readonly IOrganizationService _organizationService;
        private readonly ITracingService _tracingService;

        public SalesOrderAccessoryHandler(IOrganizationService service, ITracingService trace)
        {
            _organizationService = service;
            _tracingService = trace;
        }

        //Created By: Artum Ramos, Created On: 4/4/2017
        /*Purpose: Populate Item Details
         * Registration Details:
         * Event/Message: 
         *      Pre/Create:
         *      Post/Update: gsc_productid
         * Primary Entity: gsc_sls_orderaccessory
         */
        public Entity PopulateDetails(Entity salesOrderAccessory, String message)
        {
            _tracingService.Trace("Started PopulateDetails Method...");
            if (HasExistingAccessory(salesOrderAccessory) == true)
                throw new InvalidPluginExecutionException("Accessory already exists...");
            var itemId = salesOrderAccessory.Contains("gsc_productid") ? salesOrderAccessory.GetAttributeValue<EntityReference>("gsc_productid").Id
                : Guid.Empty;

            EntityCollection itemCollection = CommonHandler.RetrieveRecordsByOneValue("product", "productid", itemId, _organizationService, null, OrderType.Ascending,
                new[] { "productnumber"});

            if (itemCollection != null && itemCollection.Entities.Count > 0)
            {
                _tracingService.Trace("Retrieve Product");

                var itemEntity = itemCollection.Entities[0];

                salesOrderAccessory["gsc_itemnumber"] = itemEntity.Contains("productnumber")
                    ? itemEntity.GetAttributeValue<String>("productnumber")
                    : String.Empty;

                if (message.Equals("Update"))
                {
                    Entity accessoryToUpdate = _organizationService.Retrieve(salesOrderAccessory.LogicalName, salesOrderAccessory.Id,
                       new ColumnSet("gsc_itemnumber"));
                    accessoryToUpdate["gsc_itemnumber"] = salesOrderAccessory["gsc_itemnumber"];
                    _organizationService.Update(accessoryToUpdate);
                }
            }
            _tracingService.Trace("Ended PopulateDetails Method...");
            return salesOrderAccessory;
        }

        //Created By: Leslie Baliguat, Created On: 4/17/2017
        /*Purpose: Cannot delete acccessory that are default by vehicle model
         * Registration Details:
         * Event/Message: 
         *      PreValidate/Delete: 
         * Primary Entity: gsc_sls_orderaccessory
         */
        public Boolean IsAccessoryStandard(Entity orderAccessory)
        {
            if (orderAccessory.GetAttributeValue<Boolean>("gsc_standard"))
                return true;

            return false;
        }

        private bool HasExistingAccessory(Entity salesOrderAccessory)
        {
            _tracingService.Trace("Started HasExistingAccessory Method...");
            var orderId = salesOrderAccessory.Contains("gsc_orderid") ? salesOrderAccessory.GetAttributeValue<EntityReference>("gsc_orderid").Id
                : Guid.Empty;
            var productId = salesOrderAccessory.Contains("gsc_productid") ? salesOrderAccessory.GetAttributeValue<EntityReference>("gsc_productid").Id
                : Guid.Empty;

            EntityCollection orderAccessoryCollection = CommonHandler.RetrieveRecordsByOneValue("gsc_sls_orderaccessory", "gsc_orderid", orderId, _organizationService,
                null, OrderType.Ascending, new[] { "gsc_productid" });

            if (orderAccessoryCollection != null && orderAccessoryCollection.Entities.Count > 0)
            {
                _tracingService.Trace("Order Accessories retrieved: " + orderAccessoryCollection.Entities);
                foreach (Entity existingOrderAccessory in orderAccessoryCollection.Entities)
                {
                    var compareProductId = existingOrderAccessory.Contains("gsc_productid") ? existingOrderAccessory.GetAttributeValue<EntityReference>("gsc_productid").Id
                        : Guid.Empty;
                    if (productId == compareProductId)
                        return true;
                }
            }
            _tracingService.Trace("Ending HasExistingAccessory Method...");
            return false;
        }
    }
}
