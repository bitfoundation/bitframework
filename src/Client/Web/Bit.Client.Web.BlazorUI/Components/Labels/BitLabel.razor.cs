﻿using Microsoft.AspNetCore.Components;

namespace Bit.Client.Web.BlazorUI
{
    public partial class BitLabel
    {
        private bool isRequired;

        /// <summary>
        /// A label for a form element
        /// </summary>
        [Parameter] public string? For { get; set; }

        /// <summary>
        /// Whether the associated form field is required or not
        /// </summary>
        [Parameter]
        public bool IsRequired
        {
            get => isRequired;
            set
            {
                isRequired = value;
                ClassBuilder.Reset();
            }
        }

        /// <summary>
        /// The content of label, It can be Any custom tag or a text
        /// </summary>
        [Parameter] public RenderFragment? ChildContent { get; set; }

        protected override string RootElementClass => "bit-lbl";

        protected override void RegisterComponentClasses()
        {
            ClassBuilder.Register(() => IsRequired ? $"{RootElementClass}-required-{VisualClassRegistrar()}" : string.Empty);
        }
    }
}
