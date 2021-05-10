﻿using System.Threading.Tasks;
using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bit.Client.Web.BlazorUI.Tests.Buttons
{
    [TestClass]
    public class BitButtonTests : BunitTestContext
    {
        [DataTestMethod,
            DataRow(Visual.Fluent, true, ButtonStyle.Primary),
            DataRow(Visual.Fluent, true, ButtonStyle.Standard),
            DataRow(Visual.Fluent, false, ButtonStyle.Primary),
            DataRow(Visual.Fluent, false, ButtonStyle.Standard),

            DataRow(Visual.Cupertino, true, ButtonStyle.Primary),
            DataRow(Visual.Cupertino, true, ButtonStyle.Standard),
            DataRow(Visual.Cupertino, false, ButtonStyle.Primary),
            DataRow(Visual.Cupertino, false, ButtonStyle.Standard),

            DataRow(Visual.Material, true, ButtonStyle.Primary),
            DataRow(Visual.Material, true, ButtonStyle.Standard),
            DataRow(Visual.Material, false, ButtonStyle.Primary),
            DataRow(Visual.Material, false, ButtonStyle.Standard),
        ]
        public Task BitButtonTest(Visual visual, bool isEnabled, ButtonStyle style)
        {
            var com = RenderComponent<BitButtonTest>(parameters =>
            {
                parameters.Add(p => p.Visual, visual);
                parameters.Add(p => p.IsEnabled, isEnabled);
                parameters.Add(p => p.ButtonStyle, style);
            });

            var bitButton = com.Find(".bit-btn");

            var isEnabledClass = isEnabled ? "enabled" : "disabled";
            var visualClass = visual == Visual.Cupertino ? "cupertino" : visual == Visual.Material ? "material" : "fluent";

            Assert.IsTrue(bitButton.ClassList.Contains($"bit-btn-{isEnabledClass}-{visualClass}"));

            bitButton.Click();

            Assert.AreEqual(isEnabled ? 1 : 0, com.Instance.CurrentCount);

            return Task.CompletedTask;
        }

        [DataTestMethod, DataRow("Detailed description")]
        public Task BitButtonAriaDescriptionTest(string ariaDescription)
        {
            var com = RenderComponent<BitButtonTest>(parameters =>
            {
                parameters.Add(p => p.AriaDescription, ariaDescription);
            });

            var bitButton = com.Find(".bit-btn");

            Assert.IsTrue(bitButton.HasAttribute("aria-describedby"));

            return Task.CompletedTask;
        }

        [DataTestMethod, DataRow("Detailed label")]
        public Task BitButtonAriaLabelTest(string ariaLabel)
        {
            var com = RenderComponent<BitButtonTest>(parameters =>
            {
                parameters.Add(p => p.AriaLabel, ariaLabel);
            });

            var bitButton = com.Find(".bit-btn");

            Assert.IsTrue(bitButton.HasAttribute("aria-label"));

            return Task.CompletedTask;
        }

        [DataTestMethod, DataRow(true, true), DataRow(false, false), DataRow(null, false)]
        public Task BitButtonAriaHiddenTest(bool ariaHidden, bool expectedResult)
        {
            var com = RenderComponent<BitButtonTest>(parameters =>
            {
                parameters.Add(p => p.AriaHidden, ariaHidden);
            });

            var bitButton = com.Find(".bit-btn");

            Assert.AreEqual(bitButton.HasAttribute("aria-hidden"), expectedResult);

            return Task.CompletedTask;
        }
    }
}
