// Copyright (c) Jan-Niklas Schäfer. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;

namespace ThinkSharp.Licensing
{
    public class SignedLicenseException : Exception
    {
        public SignedLicenseException(string message)
            : base(message)
        { }
    }

    public class SignedLicenseExpiredException : SignedLicenseException
    {
        public SignedLicenseExpiredException(string message, SignedLicense license) : base(message)
        {
            License = license;
        }

        public SignedLicense License { get; }
    }

    public class SignedLicenseInvalidAppException : SignedLicenseException
    {
        public SignedLicenseInvalidAppException(string message, SignedLicense license) : base(message)
        {
            License = license;
        }

        public SignedLicense License { get; }
    }

    public class SignedLicenseInvalidComputerException : SignedLicenseException
    {
        public SignedLicenseInvalidComputerException(string message, SignedLicense license) : base(message)
        {
            License = license;
        }

        public SignedLicense License { get; }
    }
}
