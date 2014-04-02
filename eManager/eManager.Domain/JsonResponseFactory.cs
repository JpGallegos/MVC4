using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eManager.Domain
{
    public class JsonResponseFactory
    {
        public static object ErrorResponse(string error)
        {
            return new { Success = false, ErrorMessage = error };
        }

        public static object SuccessResponse()
        {
            return new { Success = true };
        }

        public static object SuccessResponse(object referenceObject)
        {
            return new { Success = true, Object = referenceObject };
        }
    }
}
