using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Shared
{
    public class FingerprintConstants
    {
        public const int TouchSensor1 = 0;
        public const int TouchSensor2 = 1;
        public const int RemoveFinger = 2;
        public const int FingerprintExists = 3;
        public const int FingerprintDoesNotMatch = 4;
        public const int EnrolmentSuccess = 5;
        public const int Error = 6;
        public const int NoMatchFound = 7;
        public const int FingerprintFound = 8;

        public static readonly Dictionary<int, string> StatusMessages = new()
        {
            { TouchSensor1, "Place your finger on the sensor" },
            { TouchSensor2, "Place the same finger on the sensor again" },
            { RemoveFinger, "Remove your finger from the sensor" },
            { FingerprintExists, "Existing fingerprint detected" },
            { FingerprintDoesNotMatch, "This fingerprints do not match" },
            { EnrolmentSuccess, "Fingerprint enrolment is successful" },
            { Error, "An unexpected error occurred" },
            { FingerprintFound, "Fingerprint found!" }
        };
    }
}
