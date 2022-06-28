using System.Globalization;

namespace ClothesShop.API.Helpers
{
    public class AppException : Exception
    {
        public AppException() : base() { }

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] agrs) 
            : base(String.Format(CultureInfo.CurrentCulture, message, agrs)) { }
    }
}
