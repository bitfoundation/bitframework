﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Bit.Client.Web.BlazorUI
{
    public partial class BitCompoundButton
    {
        private ButtonStyle buttonStyle = ButtonStyle.Primary;

        [Parameter] public string Text { get; set; }

        [Parameter] public string SecondaryText { get; set; }

        [Parameter]
        public ButtonStyle ButtonStyle
        {
            get => buttonStyle;
            set
            {
                buttonStyle = value;
                ClassBuilder.Reset();
            }
        }

        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected override string RootElementClass => "bit-cmpnd-btn";

        protected override void RegisterComponentClasses()
        {
            ClassBuilder.Register(() => IsEnabled is false ? string.Empty :
                ButtonStyle == ButtonStyle.Primary ? $"{RootElementClass}-primary-{VisualClassRegistrar()}"
                : $"{RootElementClass}-standard-{VisualClassRegistrar()}");
        }

        protected virtual async Task HandleOnClick(MouseEventArgs e)
        {
            if (IsEnabled)
            {
                await OnClick.InvokeAsync(e);
            }
        }

        public override Task SetParametersAsync(ParameterView parameters)
        {
            foreach (ParameterValue parameter in parameters)
            {
                switch (parameter.Name)
                {
                    case nameof(OnClick):
                        OnClick = (EventCallback<MouseEventArgs>)parameter.Value;
                        break;

                    case nameof(Text):
                        Text = (string)parameter.Value;
                        break;

                    case nameof(SecondaryText):
                        SecondaryText = (string)parameter.Value;
                        break;

                    case nameof(ButtonStyle):
                        ButtonStyle = (ButtonStyle)parameter.Value;
                        break;
                }
            }

            return base.SetParametersAsync(parameters);
        }
    }
}
