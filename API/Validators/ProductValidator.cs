using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Validators
{
    public static class ProductValidator
    {
        public static List<string> Validate(this ProductDto product)
        {
            List<string> retMessages = new List<string>();

            if (string.IsNullOrEmpty(product.Name) || string.IsNullOrWhiteSpace(product.Name))
            {
                retMessages.Add("Product Name is required");
            }

            if (string.IsNullOrEmpty(product.Description) || string.IsNullOrWhiteSpace(product.Description))
            {
                retMessages.Add("Product Description is required");
            }

            if (product.Price == 0)
            {
                retMessages.Add("Price is required");
            }

            if (product.DeliveryPrice == 0)
            {
                retMessages.Add("Delivery Price is required");
            }

            return retMessages;
        }
    }
}