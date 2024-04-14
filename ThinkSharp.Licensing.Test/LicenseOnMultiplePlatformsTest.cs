// Copyright (c) Jan-Niklas Schäfer. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
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

            var licGeneratedOnWindows = "bmJTY2BTZHpNeWReaWkBIXJPYW9PBhY4dxohY2AybwA+exRLDXxYHGwMKh04BBAyDx88GRUhEh82GhU7FB8BIREwDx09BBMxFR0sGxE7EB02GxEMKnlJeHU7dGhffywLS0piSApiYlhbTmR1Tmt0E2ZWdhlaYlRmcUBCYmUudXV/Q21AQUF2E3JrUEtZQ3ZIaGFnTm13ZG5rbUVMGF5ZEkZiC2B9aXVLUF5+YBVIa0cnRxdpTE95W1lGdX5AXBVoV09LfhQuQx5dbHloT0JjSWN0VWcnaVFmT1lUZUdLdUxBexF4VHdLaUVpD1U5W0NwE05YcxkuEB07W2BPdH87eWYzFHlYE2NRa05nFg==";
            var licGeneratedOnLinux   = "bmJTY2BTZHpNeWReaWkBeG9AYm4hbGxYbQA4cRFXDWg1e2wsb2lGfywxFAI9Hw4zEB84CxE5Ghg0ERQ4LR09BBEwDx88HhEhEB02GxE7EB0Bf2RSdBdYbnJVLXw0SVFSaUZ2bBBuS0VJTns4axhKXlNtVEhfTRhNTW5qZXhJb1dmSFBEen11G21uEXl2U3lrEQZ+ZWRTaGtcEkRmdXd9HVQ3eEZ4RkVyZQJmRxJuWllfQkNbE2laGGBxWns1aVNTb1RGeElGZ0hpTHNUa2VKZ2lLUnx2c0NCEBl+XHZDSUZcQlRGcEt+eUlrYR9/TFVDE2lAXFVST1x5SFUwRlxbXHk4C0V0TBw=";

            var licWin = Lic.Verifier
                .WithRsaPublicKey(publicKey)
                .WithApplicationCode("ABC")
                .LoadAndVerify(licGeneratedOnWindows);

            var licLinux = Lic.Verifier
                .WithRsaPublicKey(publicKey)
                .WithApplicationCode("ABC")
                .LoadAndVerify(licGeneratedOnLinux);
        }
    }
}
