﻿using FluentValidation;

using Microsoft.AspNetCore.Http;

using Nop.Plugin.Api.DTOs.SpecificationAttributes;
using Nop.Plugin.Api.Helpers;

using System;
using System.Collections.Generic;

namespace Nop.Plugin.Api.Validators
{
    public class SpecificationAttributeOptionDtoValidator : BaseDtoValidator<SpecificationAttributeOptionDto>
    {
        public SpecificationAttributeOptionDtoValidator(IHttpContextAccessor httpContextAccessor, IJsonHelper jsonHelper, Dictionary<string, object> requestJsonDictionary) : base(httpContextAccessor, jsonHelper, requestJsonDictionary)
        {
            string httpMethod = httpContextAccessor.HttpContext.Request.Method;
            if (string.IsNullOrEmpty(httpMethod) || httpMethod.Equals("post", StringComparison.InvariantCultureIgnoreCase))
            {
                //apply "create" rules
                RuleFor(x => x.Id).Equal(0).WithMessage("id must be zero or null for new records");

                ApplyNameRule();
                ApplySpecificationAttributeIdRule();
            }
            else if (httpMethod.Equals("put", StringComparison.InvariantCultureIgnoreCase))
            {
                //apply "update" rules
                RuleFor(x => x.Id).GreaterThan(0).WithMessage("invalid id");

                if (requestJsonDictionary.ContainsKey("name"))
                {
                    ApplyNameRule();
                }

                if (requestJsonDictionary.ContainsKey("specification_attribute_id"))
                {
                    ApplySpecificationAttributeIdRule();
                }
            }
        }

        private void ApplyNameRule()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("invalid name");
        }

        private void ApplySpecificationAttributeIdRule()
        {
            RuleFor(x => x.SpecificationAttributeId).GreaterThan(0).WithMessage("invalid specification attribute id");
        }
    }
}