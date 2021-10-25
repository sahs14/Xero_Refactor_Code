using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Validators
{
    public static class ProductOptionValidator
    {
               public static List<string> Validate(this ProductOptionDto productOption)
        {
            List<string> retMessages = new List<string>();

            if (string.IsNullOrEmpty(productOption.Name) || string.IsNullOrWhiteSpace(productOption.Name))
            {
                retMessages.Add("Product Option Name is required");
            }

            if (string.IsNullOrEmpty(productOption.Description) || string.IsNullOrWhiteSpace(productOption.Description))
            {
                retMessages.Add("Product Option Description is required");
            }
            return retMessages;
        }
    }
}