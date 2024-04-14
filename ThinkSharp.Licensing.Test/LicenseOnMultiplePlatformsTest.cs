// Copyright (c) Jan-Niklas Schäfer. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ThinkSharp.Licensing.Test
{
    [TestClass]
    public class LicenseOnMultiplePlatformsTest
    {
        [TestMethod]
        public void TestSerializationForMultiplePlatforms()
        {
            var publicKey = "BgIAAACkAABSU0ExAAQAAAEAAQDNT6oCg/FRfYLRq+btygouPxmPDR9uXP7OdOHOe+sQBkviVA2LHRKH9gXXgy+nkOklJBJEXG/Eg4oblnrqNV4jhof8kVzQGZw/+KLgruoRctyrsy33fTpdFyZL3MvUEkBJD2X6iPjQv0b4zEN346iDSugxP7+7ucSo+8xbmwhWwA==";
            var privateKey = "BwIAAACkAABSU0EyAAQAAAEAAQDNT6oCg/FRfYLRq+btygouPxmPDR9uXP7OdOHOe+sQBkviVA2LHRKH9gXXgy+nkOklJBJEXG/Eg4oblnrqNV4jhof8kVzQGZw/+KLgruoRctyrsy33fTpdFyZL3MvUEkBJD2X6iPjQv0b4zEN346iDSugxP7+7ucSo+8xbmwhWwD+99xLjjK4d+PF5A5yVzPgF5KvHkcq9k+RxSuLMngrVr69k1Wzl63uascdkTKC4wR9boESrN0YYPVqLtXTVqNzzEwpGHZClNgp7FQuP+dAe44iIRbLR7VvTezeVRo8l1SVruVU77ViCfvG8ZsFQonX9RsA8ZdncycwlfZIw7CPfAxnCUyiNucKJD22yxyGJUPeNRtaWSbOU0Fi+sJZ4NhfY4r7BFOhwgJkj9Ihiv408wQwa3dLaN58a5R+BnjN/UxO1/r4STBhevLxd5OnPsn5bgdrpnWgjA1DKzxUDFdea/U6Ku8e73mpQqx/upYutJJIZP/SQ84xDZG+vBAbpF9lFStE9sV37l77cfdM/inVCU6e02wHZiR65uNsrjUQxHCKUOe1qClFFiKovmWYZZoZIpFrOC5+h6WCvUeJWUEJfQURrOQzVpAi18Np+JL2DfNmZNGOeREyvPyPEEp0NoSqWw5zqt+9vVMLYX14jXg1HUuJm19PHt/mbEzR3ppCeSosiLKDjbOqsyutDc0Ow6MYZTeBOsYbVFk1OxGtODJWSd2c5TYe2qbxIGCb5JUXZm91YOtxqQbWdRsJEz5HPVgY=";

            var licGeneratedOnWindows3 = "bmJTY2BTZHpNeWReaWkBIXJPYW9PBhE0eXshHWA1ZAA9HW1SDXtYZhAMKh04BBA1Dx88GRUhEB02GxE7EB0BIREwDx09BBMxFR0sGxE7EB02GxEMKnlJeHU7dGhffywLRGk7aHlxYQJZSW9OGQZCTHR3URtaElVLQx5GclR1aB87b0xIaht0ZHVocX98RhR7amxaZVJZF0ZqHVh5WUM+T2RvbVliW29pTRk+WRhDch5ke0JoE354ZHdVEXVZRGhjZB00TmpORGZHeBVZQkhdRUZnQmE9W3BxaE5oQUxmD2xdTlt7D29VTnE4S3ldWmk3FGh9UUB2THlUakZGdEdqQ3htd1xNQW5pV0lvFg==";
            var licGeneratedOnWindows2 ="bmJTY2BTZHpNeWReaWkBIXJPYW9PBnk4aWIhcxc1YQA+Ym80DXtGc3kMKh04BBA1Dx88GRUhEB02GxE7EB0BIREwDx09BBMxFR0sGxE7EB02GxEMKnlJeHU7dGhffywLZlV8ZHRoZEZ4TWJSUx08RXtQVUliRUdrRlRUSmhrUwZ4TxliUE8+QlV7aFp+RXZGTVRjfEZWemtGSEpRVGtrb1VKU0ptZ1JScW9pHQpIbgZFchRxQldAcVdqa1RKfhRpWnVuZU12Rhp9URkxUAJVZGVJdAJ8GGJKVVloWE02bnhiXGhZZVxqQ3YxeR52Tmt1QWI+HWdZSUBLE3BRZVlBTFs1QRRpZnVQamlnFg==";
            var licGeneratedOnWindows = "bmJTY2BTZHpNeWReaWkBIXJPYW9PBhY4dxohY2AybwA+exRLDXxYHGwMKh04BBAyDx88GRUhEh82GhU7FB8BIREwDx09BBMxFR0sGxE7EB02GxEMKnlJeHU7dGhffywLS0piSApiYlhbTmR1Tmt0E2ZWdhlaYlRmcUBCYmUudXV/Q21AQUF2E3JrUEtZQ3ZIaGFnTm13ZG5rbUVMGF5ZEkZiC2B9aXVLUF5+YBVIa0cnRxdpTE95W1lGdX5AXBVoV09LfhQuQx5dbHloT0JjSWN0VWcnaVFmT1lUZUdLdUxBexF4VHdLaUVpD1U5W0NwE05YcxkuEB07W2BPdH87eWYzFHlYE2NRa05nFg==";
            var licGeneratedOnUbuntuServer   = "bmJTY2BTZHpNeWReaWkGeG9AYm4hEhFOcwBUGmtGDRRZchMscGFIeSsxFAI9Hw4zEB84CxAxGhw7ERMyKh09BBEwDx88HhEhEB02GxE7EB0Gf2RSdBdYbnJVKkp7bG5IcnUnElVkWGV7XQ5RWmt5XVlod3dkWVJXemw8AHlsTGR0TXl2RldcABM2QWJYRkprRl9OYENNZ3piX3VkSx9KH2VNFX8jTRM1R2VrW3BlY2ZnamwzF0J2QndVRhl+U1ByT2I5Eml0UBg/Wm52UF9bfHZ0TnRpU2pTRmBrbmNPSkxCZxNsVGx/U1FwUktOaWRgYkInfW04GEU+Q05VZ3dbZhdxS2Rafhw=";
            var licGeneratedOnWSLUbuntu  = "bmJTY2BTZHpNeWReaWkBIXJPYW9PBmJPd30hZXlFEAA0cXJUDWlGbnYMKh04BBA1Dx88GRUhEBQ2GBg7EhoBIREwDx09BBMxFR0sGxE7EB02GxEMKnlJeHU7dGhffywLehprQktYEUF8bEBwRRhiW2pocWdeb2l2ZU4jb1JKdkl6anlGR15kWERmF2Z2TEgweFR6aUdLEWN8bG5OCxlpElV1VXRHakpic1V+GVY4d2tWU01mZh86RRZnYmp2HFVFdFlCaW1laGdeTmNPGGsnZkxqQ3xWU3NMFGpfTk9TEWU8XHttbF5JfnRqeVQ7eWdLZFh7f0REWHo6XnZmEko0aG13eR5aUnFJbXRdFg==";

            var licenses = new[] 
            {
                (licGeneratedOnWindows, new DateTime(2024, 04, 13)),
                (licGeneratedOnWindows2, new DateTime(2024, 04, 14)),
                (licGeneratedOnWindows3, new DateTime(2024, 04, 14)),                
                (licGeneratedOnUbuntuServer, new DateTime(2024, 04, 14)), 
                (licGeneratedOnWSLUbuntu, new DateTime(2024, 04, 14))
            };
            
            foreach ( var (license, issueDate) in licenses )
            {
                var lic = Lic.Verifier
                    .WithRsaPublicKey(publicKey)
                    .WithApplicationCode("ABC")
                    .LoadAndVerify(license);

                Assert.IsFalse(lic.HasHardwareIdentifier);
                Assert.IsTrue(lic.HasExpirationDate);
                Assert.AreEqual(issueDate, lic.IssueDate.Date);
                Assert.AreEqual(new DateTime(2050, 01, 01), lic.ExpirationDate);
                Assert.AreEqual(1, lic.Properties.Count);
                Assert.AreEqual("TEST", lic.Properties["TEST"]);
            }
        }
    }
}
