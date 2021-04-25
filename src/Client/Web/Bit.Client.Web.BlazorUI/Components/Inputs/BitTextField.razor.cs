﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Bit.Client.Web.BlazorUI
{
    public partial class BitTextField
    {
        private bool isMultiLine;
        private bool isReadonly = false;
        private string focusClass = "";
        private TextFieldType type = TextFieldType.Text;

        [Parameter] public int MaxLength { get; set; } = -1;

        [Parameter] public string Value { get; set; }

        [Parameter] public string Placeholder { get; set; }

        [Parameter]
        public bool IsReadonly
        {
            get => isReadonly;
            set
            {
                isReadonly = value;
                ClassBuilder.Reset();
            }
        }

        [Parameter]
        public TextFieldType Type
        {
            get => type;
            set
            {
                type = value;
                ClassBuilder.Reset();
            }
        }

        [Parameter]
        public bool IsMultiLine
        {
            get => isMultiLine;
            set
            {
                isMultiLine = value;
                ClassBuilder.Reset();
            }
        }

        [Parameter] public EventCallback<FocusEventArgs> OnFocusIn { get; set; }

        [Parameter] public EventCallback<FocusEventArgs> OnFocusOut { get; set; }

        [Parameter] public EventCallback<FocusEventArgs> OnFocus { get; set; }

        [Parameter] public EventCallback<ChangeEventArgs> OnChange { get; set; }

        [Parameter] public EventCallback<KeyboardEventArgs> OnKeyDown { get; set; }

        [Parameter] public EventCallback<KeyboardEventArgs> OnKeyUp { get; set; }

        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        public string FocusClass
        {
            get => focusClass;
            set
            {
                focusClass = value;
                ClassBuilder.Reset();
            }
        }

        protected override string RootElementClass => "bit-txt-fld";

        protected override void RegisterComponentClasses()
        {
            ClassBuilder.Register(() => IsMultiLine && Type == TextFieldType.Text ? $"{RootElementClass}-multiline-{VisualClassRegistrar()}" : string.Empty);
            ClassBuilder.Register(() => IsEnabled is false ? $"{RootElementClass}-disabled-{VisualClassRegistrar()}" : string.Empty);
            ClassBuilder.Register(() => IsEnabled && IsReadonly ? $"{RootElementClass}-readonly-{VisualClassRegistrar()}" : string.Empty);
            ClassBuilder.Register(() => FocusClass);
        }

        protected virtual async Task HandleFocusIn(FocusEventArgs e)
        {
            if (IsEnabled)
            {
                FocusClass = $"{RootElementClass}-focused-{VisualClassRegistrar()}";
                await OnFocusIn.InvokeAsync(e);
            }
        }

        protected virtual async Task HandleFocusOut(FocusEventArgs e)
        {
            if (IsEnabled)
            {
                FocusClass = "";
                await OnFocusOut.InvokeAsync(e);
            }
        }

        protected virtual async Task HandleFocus(FocusEventArgs e)
        {
            if (IsEnabled)
            {
                FocusClass = $"{RootElementClass}-focused-{VisualClassRegistrar()}";
                await OnFocus.InvokeAsync(e);
            }
        }

        protected virtual async Task HandleChange(ChangeEventArgs e)
        {
            if (IsEnabled)
            {
                await OnChange.InvokeAsync(e);
            }
        }

        protected virtual async Task HandleKeyDown(KeyboardEventArgs e)
        {
            if (IsEnabled)
            {
                await OnKeyDown.InvokeAsync(e);
            }
        }

        protected virtual async Task HandleKeyUp(KeyboardEventArgs e)
        {
            if (IsEnabled)
            {
                await OnKeyUp.InvokeAsync(e);
            }
        }

        protected virtual async Task HandleClick(MouseEventArgs e)
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
                    case nameof(MaxLength):
                        MaxLength = (int)parameter.Value;
                        break;

                    case nameof(Value):
                        Value = (string)parameter.Value;
                        break;

                    case nameof(Placeholder):
                        Placeholder = (string)parameter.Value;
                        break;

                    case nameof(IsReadonly):
                        IsReadonly = (bool)parameter.Value;
                        break;

                    case nameof(Type):
                        Type = (TextFieldType)parameter.Value;
                        break;

                    case nameof(IsMultiLine):
                        IsMultiLine = (bool)parameter.Value;
                        break;

                    case nameof(OnFocusIn):
                        OnFocusIn = (EventCallback<FocusEventArgs>)parameter.Value;
                        break;

                    case nameof(OnFocusOut):
                        OnFocusOut = (EventCallback<FocusEventArgs>)parameter.Value;
                        break;

                    case nameof(OnFocus):
                        OnFocus = (EventCallback<FocusEventArgs>)parameter.Value;
                        break;

                    case nameof(OnChange):
                        OnChange = (EventCallback<ChangeEventArgs>)parameter.Value;
                        break;

                    case nameof(OnKeyDown):
                        OnKeyDown = (EventCallback<KeyboardEventArgs>)parameter.Value;
                        break;

                    case nameof(OnKeyUp):
                        OnKeyUp = (EventCallback<KeyboardEventArgs>)parameter.Value;
                        break;

                    case nameof(OnClick):
                        OnClick = (EventCallback<MouseEventArgs>)parameter.Value;
                        break;
                }
            }
            return base.SetParametersAsync(parameters);
        }
    }
}
