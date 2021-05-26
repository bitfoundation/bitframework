﻿using System.Linq;
using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bit.Client.Web.BlazorUI.Tests.Rating
{
    [TestClass]
    public class BitRatingTests : BunitTestContext
    {
        [DataTestMethod,
            DataRow(Visual.Fluent, true),
            DataRow(Visual.Fluent, false),

            DataRow(Visual.Cupertino, true),
            DataRow(Visual.Cupertino, false),

            DataRow(Visual.Material, true),
            DataRow(Visual.Material, false)]
        public void BitRatingShouldRespectIsReadonly(Visual visual, bool isReadonly)
        {
            var component = RenderComponent<BitRatingTest>(parameters =>
            {
                parameters.Add(p => p.IsReadonly, isReadonly);
                parameters.Add(p => p.Visual, visual);
            });
            var bitRating = component.Find("div");

            var visualClass = visual == Visual.Cupertino ? "cupertino" : visual == Visual.Material ? "material" : "fluent";

            Assert.AreEqual(bitRating.ClassList.Contains($"bit-rating-readonly-{visualClass}"), isReadonly);
        }

        [DataTestMethod,
            DataRow(Visual.Fluent, true),
            DataRow(Visual.Fluent, false),

            DataRow(Visual.Cupertino, true),
            DataRow(Visual.Cupertino, false),

            DataRow(Visual.Material, true),
            DataRow(Visual.Material, false)]
        public void BitRatingShouldRespectIsEnabled(Visual visual, bool isEnabled)
        {
            var component = RenderComponent<BitRatingTest>(parameters =>
            {
                parameters.Add(p => p.IsEnabled, isEnabled);
                parameters.Add(p => p.Visual, visual);
            });
            var bitRating = component.Find("div");

            var isEnabledClass = isEnabled ? "enabled" : "disabled";
            var visualClass = visual == Visual.Cupertino ? "cupertino" : visual == Visual.Material ? "material" : "fluent";

            Assert.IsTrue(bitRating.ClassList.Contains($"bit-rating-{isEnabledClass}-{visualClass}"));
        }

        [DataTestMethod,
            DataRow(Visual.Fluent, null),
            DataRow(Visual.Fluent, RatingSize.Small),
            DataRow(Visual.Fluent, RatingSize.Large),

            DataRow(Visual.Cupertino, null),
            DataRow(Visual.Cupertino, RatingSize.Small),
            DataRow(Visual.Cupertino, RatingSize.Large),

            DataRow(Visual.Material, null),
            DataRow(Visual.Material, RatingSize.Small),
            DataRow(Visual.Material, RatingSize.Large)]
        public void BitRatingShouldRespectSize(Visual visual, RatingSize size)
        {
            var component = RenderComponent<BitRatingTest>(parameters =>
            {
                parameters.Add(p => p.Size, size);
                parameters.Add(p => p.Visual, visual);
            });
            var bitRating = component.Find("div");

            var sizeClass = size == RatingSize.Large ? "large" : "small";
            var visualClass = visual == Visual.Cupertino ? "cupertino" : visual == Visual.Material ? "material" : "fluent";

            Assert.IsTrue(bitRating.ClassList.Contains($"bit-rating-{sizeClass}-{visualClass}"));
        }

        [DataTestMethod,
            DataRow("HeartFill", "Heart", 10, false),
            DataRow("HeartFill", "Heart", 10, true),
            DataRow("HeartFill", "Heart", 10, false)]
        public void BitRatingShouldRespectCustomIcon(string icon, string unselectedIcon, int max, bool allowZeroStars)
        {
            var component = RenderComponent<BitRatingTest>(parameters =>
            {
                parameters.Add(p => p.Icon, icon);
                parameters.Add(p => p.UnselectedIcon, unselectedIcon);
                parameters.Add(p => p.Max, max);
                parameters.Add(p => p.AllowZeroStars, allowZeroStars);
            });

            int defaultRating = allowZeroStars ? 0 : 1;

            var bitRating = component.FindAll("button");
            var bitRatingIcon = component.FindAll("i");

            var filledBitRatingIconCount = bitRatingIcon.Where(r => r.ClassList.Contains($"bit-icon--{icon}")).Count();
            var unselectedBitRatingIconCount = bitRatingIcon.Where(r => r.ClassList.Contains($"bit-icon--{unselectedIcon}")).Count();

            Assert.AreEqual(bitRating.Count(), max);
            Assert.AreEqual(filledBitRatingIconCount, defaultRating);
            Assert.AreEqual(unselectedBitRatingIconCount, (max - defaultRating));
        }

        [DataTestMethod,
            DataRow(10, 3, false, 3),
            DataRow(10, 0, true, 0),
            DataRow(10, 0, false, 1)]
        public void BitRatingShouldRespectIconAndValue(int max, int clickedIndex, bool allowZeroStars, int expectedResult)
        {
            var icon = "FavoriteStarFill";
            var unselectedIcon = "FavoriteStar";

            var component = RenderComponent<BitRatingTest>(parameters =>
            {
                parameters.Add(p => p.Max, max);
                parameters.Add(p => p.AllowZeroStars, allowZeroStars);
            });

            var bitRating = component.FindAll("button");

            if (clickedIndex == 0)
            {
                clickedIndex = 1;
            }

            if (clickedIndex > 0 && allowZeroStars is false)
                bitRating[clickedIndex - 1].Click();

            var bitRatingIcon = component.FindAll("i");

            var filledBitRatingIconCount = bitRatingIcon.Where(r => r.ClassList.Contains($"bit-icon--{icon}")).Count();
            var unselectedBitRatingIconCount = bitRatingIcon.Where(r => r.ClassList.Contains($"bit-icon--{unselectedIcon}")).Count();

            Assert.AreEqual(bitRating.Count(), max);

            Assert.AreEqual(filledBitRatingIconCount, expectedResult);
            Assert.AreEqual(unselectedBitRatingIconCount, (max - expectedResult));
        }
    }
}
