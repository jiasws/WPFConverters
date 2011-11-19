using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest
{
    public sealed class CaseConverterTest : UnitTest
    {
        private CaseConverter caseConverter;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.caseConverter = new CaseConverter();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.Equal(CharacterCasing.Normal, this.caseConverter.Casing);
        }

        [Fact]
        public void Constructor_Casing_ShouldSetCasing()
        {
            this.caseConverter = new CaseConverter(CharacterCasing.Upper);
            Assert.Equal(CharacterCasing.Upper, this.caseConverter.Casing);
            this.caseConverter = new CaseConverter(CharacterCasing.Lower);
            Assert.Equal(CharacterCasing.Lower, this.caseConverter.Casing);
        }

        [Fact]
        public void Casing_ShouldThrowIfInvalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => this.caseConverter.Casing = (CharacterCasing)100);
            Assert.Equal("'100' is not a valid value for property 'Casing'.", ex.Message);
        }

        [Fact]
        public void Casing_ShouldGetAndSetCasing()
        {
            Assert.Equal(CharacterCasing.Normal, this.caseConverter.Casing);
            this.caseConverter.Casing = CharacterCasing.Upper;
            Assert.Equal(CharacterCasing.Upper, this.caseConverter.Casing);
            this.caseConverter.Casing = CharacterCasing.Lower;
            Assert.Equal(CharacterCasing.Lower, this.caseConverter.Casing);
        }

        [Fact]
        public void Convert_ShouldDoNothingIfValueIsNotAString()
        {
            Assert.Same(DependencyProperty.UnsetValue, this.caseConverter.Convert(123, null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, this.caseConverter.Convert(123d, null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, this.caseConverter.Convert(DateTime.Now, null, null, null));
        }

        [Fact]
        public void Convert_ShouldDoNothingIfCasingIsNormal()
        {
            Assert.Equal(CharacterCasing.Normal, this.caseConverter.Casing);
            Assert.Equal("abcd", this.caseConverter.Convert("abcd", null, null, null));
            Assert.Equal("ABCD", this.caseConverter.Convert("ABCD", null, null, null));
            Assert.Equal("AbCd", this.caseConverter.Convert("AbCd", null, null, null));
        }

        [Fact]
        public void Convert_ShouldConvertStringsToSpecifiedCasing()
        {
            this.caseConverter.Casing = CharacterCasing.Lower;
            Assert.Equal("abcd", this.caseConverter.Convert("abcd", null, null, null));
            Assert.Equal("abcd", this.caseConverter.Convert("ABCD", null, null, null));
            Assert.Equal("abcd", this.caseConverter.Convert("AbCd", null, null, null));

            this.caseConverter.Casing = CharacterCasing.Upper;
            Assert.Equal("ABCD", this.caseConverter.Convert("abcd", null, null, null));
            Assert.Equal("ABCD", this.caseConverter.Convert("ABCD", null, null, null));
            Assert.Equal("ABCD", this.caseConverter.Convert("AbCd", null, null, null));
        }

        [Fact]
        public void Convert_ShouldUseSpecifiedCulture()
        {
            CultureInfo cultureInfo = new CultureInfo("tr");

            this.caseConverter.Casing = CharacterCasing.Lower;
            Assert.Equal("ijk", this.caseConverter.Convert("ijk", null, null, cultureInfo));
            Assert.Equal("ıjk", this.caseConverter.Convert("IJK", null, null, cultureInfo));
            Assert.Equal("ijk", this.caseConverter.Convert("iJk", null, null, cultureInfo));

            this.caseConverter.Casing = CharacterCasing.Upper;
            Assert.Equal("İJK", this.caseConverter.Convert("ijk", null, null, cultureInfo));
            Assert.Equal("IJK", this.caseConverter.Convert("IJK", null, null, cultureInfo));
            Assert.Equal("İJK", this.caseConverter.Convert("iJk", null, null, cultureInfo));
        }

        [Fact]
        public void ConvertBack_ShouldReturnUnsetValue()
        {
            Assert.Same(DependencyProperty.UnsetValue, this.caseConverter.ConvertBack(null, null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, this.caseConverter.ConvertBack(123, null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, this.caseConverter.ConvertBack(DateTime.Now, null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, this.caseConverter.ConvertBack("abc", null, null, null));
        }
    }
}
