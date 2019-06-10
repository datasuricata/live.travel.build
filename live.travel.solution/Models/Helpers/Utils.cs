using System;

namespace live.travel.solution.Models.Helpers {
    public static class Utils {

        public static string RemoveSpaces(this string str) {
            if (!string.IsNullOrEmpty(str)) {
                str = str.Replace(" ", "");
                str = str.Trim();
            }
            return str;
        }

        public static string ToFormatCPF(this string str) {
            if (!string.IsNullOrEmpty(str))
                str = Convert.ToUInt64(str).ToString(@"000\.000\.000\-00");
            return str;
        }

        public static string CleanFormat(this string str) {
            if (string.IsNullOrEmpty(str))
                str = str.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
            return str;
        }

        public static string FormatarData(this string str) {
            if (!string.IsNullOrEmpty(str)) {
                DateTime dateTime;
                if (DateTime.TryParse(str, out dateTime)) {
                    str = dateTime.ToString("dd/MM/yyyy");
                }
            }
            return str;
        }
    }
}
