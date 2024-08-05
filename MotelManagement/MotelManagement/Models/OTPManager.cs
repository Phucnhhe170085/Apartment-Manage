using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotelManagement.Models
{
    internal class OTPManager
    {
        public static string CurrentOtp { get; set; }
        public static string Email { get; set; }
        public static bool IsOtpVerified { get; set; }
    }
}
