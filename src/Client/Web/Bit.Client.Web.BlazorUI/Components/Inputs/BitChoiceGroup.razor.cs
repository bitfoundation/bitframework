﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bit.Client.Web.BlazorUI
{
    public partial class BitChoiceGroup
    {
        private List<BitChoiceOption> _options = new List<BitChoiceOption>();

        [Parameter] public string Name { get; set; } = Guid.NewGuid().ToString();
        [Parameter] public string Value { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        public override Task SetParametersAsync(ParameterView parameters)
        {
            foreach (ParameterValue parameter in parameters)
            {
                switch (parameter.Name)
                {
                    case nameof(Name):
                        Name = (string)parameter.Value;
                        break;
                    case nameof(Value):
                        Value = (string)parameter.Value;
                        break;
                    case nameof(ChildContent):
                        ChildContent = (RenderFragment)parameter.Value;
                        break;
                }
            }
            return base.SetParametersAsync(parameters);
        }

        internal void ChangeSelection(BitChoiceOption option)
        {
            if (IsEnabled)
            {
                foreach (BitChoiceOption item in _options)
                    item.SetOptionCheckedStatus(item == option);
                Value = option.Value;
            }
        }

        internal void RegisterOption(BitChoiceOption option)
        {
            _options.Add(option);
        }

        internal void UnregisterRadio(BitChoiceOption option)
        {
            _options.Remove(option);
        }

    }
}