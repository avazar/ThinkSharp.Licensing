// Copyright (c) Jan-Niklas Schäfer. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThinkSharp.Licensing.Test.Signing;

namespace ThinkSharp.Licensing.Test
{
    [TestClass]
    public class SignedLicenseTest
    {
        [TestMethod]
        public void TestInitialization()
        {
            var file = new SignedLicense("HardwareID", "SerialNumber", DateTime.Now, DateTime.Now + TimeSpan.FromDays(10), null);
            AssertDefaultPropertiesAreValid(file);
            Assert.AreEqual(0, file.Properties.Count);
        }

        [TestMethod]
        public void TestSerialization()
        {
            var license = new SignedLicense(HardwareIdentifier.NoHardwareIdentifier, "SerialNumber", DateTime.Now, DateTime.Now + TimeSpan.FromDays(10), CreateProperties());
            license.Sign(new LengthSigner());
            var licPlainText = license.SerializeAsPlainText();
            var licEncrypted = license.Serialize();

            var licensePlainText = SignedLicense.Deserialize(licPlainText);
            var licenseEncrypted = SignedLicense.Deserialize(licEncrypted);

            Assert.AreEqual(licensePlainText.SerializeAsPlainText(), licenseEncrypted.SerializeAsPlainText());
            Assert.AreEqual(licensePlainText.Serialize(), licenseEncrypted.Serialize());
        }

        [TestMethod]
        public void TestInitialization_WithProperties()
        {
            var file = new SignedLicense("HardwareID", "SerialNumber", DateTime.Now, DateTime.Now + TimeSpan.FromDays(10), CreateProperties());
            AssertDefaultPropertiesAreValid(file);
            AssertPropertiesAreValid(file);
        }

        [TestMethod]
        public void TestInitialization_WithProperties_WithColone()
        {
            var properties = new Dictionary<string, string>();
            properties.Add("Pro:p1", "Val1");
            properties.Add("Prop2", "Val2");
            try
            {
                var file = new SignedLicense("HardwareID", "SerialNumber", DateTime.Now, DateTime.Now + TimeSpan.FromDays(10), properties);
                Assert.Fail("FormatException expected.");
            }
            catch (FormatException) { }
        }

        [TestMethod]
        public void TestSigning_WithoutProperties()
        {
            var file = new SignedLicense("HardwareID", "SerialNumber", DateTime.Now, DateTime.Now + TimeSpan.FromDays(10), null);
            file.Sign(new LengthSigner());
            var content = file.Serialize();
            var newFile = SignedLicense.Deserialize(content);

            AssertDefaultPropertiesAreValid(newFile);
            Assert.AreEqual(0, newFile.Properties.Count);
            newFile.Verify(new LengthSigner());

            try
            {
                newFile.Verify(new DoubleLengthSigner());
                Assert.Fail("LicenseException expected");
            }
            catch (SignedLicenseException) { }
        }

        [TestMethod]
        public void TestSigning_WithProperties()
        {
            var file = new SignedLicense("HardwareID", "SerialNumber", DateTime.Now, DateTime.Now + TimeSpan.FromDays(10), CreateProperties());
            file.Sign(new LengthSigner());
            var content = file.Serialize();
            var newFile = SignedLicense.Deserialize(content);

            AssertDefaultPropertiesAreValid(newFile);
            AssertPropertiesAreValid(file);
            newFile.Verify(new LengthSigner());

            try
            {
                newFile.Verify(new DoubleLengthSigner());
                Assert.Fail("LicenseException expected");
            }
            catch (SignedLicenseException) { }
        }

        [TestMethod]
        public void TestSigning_WithProperties_DifferentCultures()
        {
            var cultureDE = new CultureInfo("de");
            var cultureEN = new CultureInfo("en");

            
            Thread.CurrentThread.CurrentCulture = cultureDE;
            var file = new SignedLicense("HardwareID", "SerialNumber", DateTime.Now, DateTime.Now + TimeSpan.FromDays(10), CreateProperties());
            file.Sign(new HashCodeSigner());
            var content = file.Serialize();
            Thread.CurrentThread.CurrentCulture = cultureEN;
            var newFile = SignedLicense.Deserialize(content);
            newFile.Verify(new HashCodeSigner());
        }

        [TestMethod]
        public void TestHasXXXProperties_True()
        {
            var publicKey = "BgIAAACkAABSU0ExAAQAAAEAAQAFIs58zPLD7fmD/wtMHI9LCSYPQy1Iep7jPC0+Ct4Tiw8jV1QaxQmHb3y88IsTggqTjOsh3hIx2keQRJr4YfQQ1NNzaZiOG7E7wRR7EC3NKNLsX7lp2VKsOzye8sNZR8o+5J4fWNCTZV3BEmNTE3aCxCV4hGU7NLwG5wHeVoD6qw==";
            var privateKey = "BwIAAACkAABSU0EyAAQAAAEAAQAFIs58zPLD7fmD/wtMHI9LCSYPQy1Iep7jPC0+Ct4Tiw8jV1QaxQmHb3y88IsTggqTjOsh3hIx2keQRJr4YfQQ1NNzaZiOG7E7wRR7EC3NKNLsX7lp2VKsOzye8sNZR8o+5J4fWNCTZV3BEmNTE3aCxCV4hGU7NLwG5wHeVoD6q2NeAWMvkbvcR4qdFxGwBb/XK9RSv2aR3WDgbtmQbMLkeg/qQyAvdxDGGjx0pDIX0woZM48MBQvknSB6YVo7G9d3VpIvLZR5S28UjVHe2OougXMk7br4lFDaLThCAUf2OsmpyEq8GEnNrvq7nXRHXPL1LFGrua9Qu2wGugUyVKzM98CuF8Wx904V6cF/aebM30rFSfTTAt0C7LCs3sK7ZfS1R2GmLwOlX7wgfOHALCfwGzXCAiheKrFTmc2lGeBzSWm53g0Dh8J3FG+ljzVJ9QCdJz5DGyVo/0XZwKGCV/H1q3Fw4wsPhMBFJT7cgevxSRS7wAKitN9phIE3YxzgyZsDJELs6iz7zI8yUAJt0YGaRQfl7A2nieJqRUlul+1KZkR6L20ZAPieh+LqgiOeEeCv/5c/LpZvwFn/9WdQKIxRyQeR8/yqUMgRrmu1ffpUT2XgHf3qvycAzjsBSQE7Z/LRk3nv+1fyStzLC/CzWwvK5WrsAu49gDsiTWpjYPq/F6riIVUdXbezj1GoxcRnkB59ftGe0bnqICD2aZ3FKj6zB/vE/lg2Bzx1cZh86L4uOxMcGCSZa8pN+mD3ylZ6dAg=";


            var lic = Lic.Builder
                .WithRsaPrivateKey(privateKey)
                .WithHardwareIdentifier(HardwareIdentifier.ForCurrentComputer())
                .WithSerialNumber(SerialNumber.Create("GSA"))
                .ExpiresIn(TimeSpan.FromDays(100))
                .WithProperty("Name", "Bill Gates")
                .WithProperty("Company", "Microsoft")
                .SignAndCreate();

            Assert.IsTrue(lic.HasExpirationDate);
            Assert.IsTrue(lic.HasHardwareIdentifier);
            Assert.IsTrue(lic.HasSerialNumber);

            var serialized = lic.Serialize();
            var deserializedLic = Lic.Verifier
                .WithRsaPublicKey(publicKey)                
                .WithApplicationCode("GSA")
                .LoadAndVerify(serialized);

            Assert.IsTrue(deserializedLic.HasExpirationDate);
            Assert.IsTrue(deserializedLic.HasHardwareIdentifier);
            Assert.IsTrue(deserializedLic.HasSerialNumber);
        }

        [TestMethod]
        public void TestHasXXXProperties_False()
        {
            var publicKey = "BgIAAACkAABSU0ExAAQAAAEAAQAFIs58zPLD7fmD/wtMHI9LCSYPQy1Iep7jPC0+Ct4Tiw8jV1QaxQmHb3y88IsTggqTjOsh3hIx2keQRJr4YfQQ1NNzaZiOG7E7wRR7EC3NKNLsX7lp2VKsOzye8sNZR8o+5J4fWNCTZV3BEmNTE3aCxCV4hGU7NLwG5wHeVoD6qw==";
            var privateKey = "BwIAAACkAABSU0EyAAQAAAEAAQAFIs58zPLD7fmD/wtMHI9LCSYPQy1Iep7jPC0+Ct4Tiw8jV1QaxQmHb3y88IsTggqTjOsh3hIx2keQRJr4YfQQ1NNzaZiOG7E7wRR7EC3NKNLsX7lp2VKsOzye8sNZR8o+5J4fWNCTZV3BEmNTE3aCxCV4hGU7NLwG5wHeVoD6q2NeAWMvkbvcR4qdFxGwBb/XK9RSv2aR3WDgbtmQbMLkeg/qQyAvdxDGGjx0pDIX0woZM48MBQvknSB6YVo7G9d3VpIvLZR5S28UjVHe2OougXMk7br4lFDaLThCAUf2OsmpyEq8GEnNrvq7nXRHXPL1LFGrua9Qu2wGugUyVKzM98CuF8Wx904V6cF/aebM30rFSfTTAt0C7LCs3sK7ZfS1R2GmLwOlX7wgfOHALCfwGzXCAiheKrFTmc2lGeBzSWm53g0Dh8J3FG+ljzVJ9QCdJz5DGyVo/0XZwKGCV/H1q3Fw4wsPhMBFJT7cgevxSRS7wAKitN9phIE3YxzgyZsDJELs6iz7zI8yUAJt0YGaRQfl7A2nieJqRUlul+1KZkR6L20ZAPieh+LqgiOeEeCv/5c/LpZvwFn/9WdQKIxRyQeR8/yqUMgRrmu1ffpUT2XgHf3qvycAzjsBSQE7Z/LRk3nv+1fyStzLC/CzWwvK5WrsAu49gDsiTWpjYPq/F6riIVUdXbezj1GoxcRnkB59ftGe0bnqICD2aZ3FKj6zB/vE/lg2Bzx1cZh86L4uOxMcGCSZa8pN+mD3ylZ6dAg=";

            var lic = Lic.Builder
                .WithRsaPrivateKey(privateKey)
                .WithoutHardwareIdentifier()
                .WithoutSerialNumber()
                .WithoutExpiration()
                .WithProperty("Name", "Bill Gates")
                .WithProperty("Company", "Microsoft")
                .SignAndCreate();

            Assert.IsFalse(lic.HasExpirationDate);
            Assert.IsFalse(lic.HasHardwareIdentifier);
            Assert.IsFalse(lic.HasSerialNumber);

            var serialized = lic.Serialize();
            var deserializedLic = Lic.Verifier
                .WithRsaPublicKey(publicKey)
                .WithoutApplicationCode()
                .LoadAndVerify(serialized);

            Assert.IsFalse(deserializedLic.HasExpirationDate);
            Assert.IsFalse(deserializedLic.HasHardwareIdentifier);
            Assert.IsFalse(deserializedLic.HasSerialNumber);
        }


        private static void AssertDefaultPropertiesAreValid(SignedLicense file)
        {
            Assert.AreEqual("HardwareID", file.HardwareIdentifier);
            Assert.AreEqual(DateTime.UtcNow.Date, file.IssueDate.Date);
            Assert.AreEqual("SerialNumber", file.SerialNumber);
        }

        private static Dictionary<string, string> CreateProperties()
        {
            var properties = new Dictionary<string, string>();
            properties.Add("Prop1", "Val1");
            properties.Add("Prop2", "Val2");
            return properties;
        }

        private static void AssertPropertiesAreValid(SignedLicense file)
        {
            Assert.AreEqual(2, file.Properties.Count);
            Assert.AreEqual("Val1", file.Properties["Prop1"]);
            Assert.AreEqual("Val2", file.Properties["Prop2"]);
        }
    }
}
