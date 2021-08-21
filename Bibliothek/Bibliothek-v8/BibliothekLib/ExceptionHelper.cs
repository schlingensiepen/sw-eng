using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliothekLib
{
    class ExceptionHelper
    {
        private static String exceptionToString(Exception ex)
        {
            if (ex == null) return "null";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format("Trace Exception {0}", ex.Message));
            var exception = ex as DbEntityValidationException;
            if (exception != null)
            {
                sb.AppendLine(entityValidationExceptionToString(exception));
            }
            sb.AppendLine("Data");
            foreach (DictionaryEntry dat in ex.Data)
            {
                sb.AppendLine(String.Format("    {0} = {1}", dat.Key, dat.Value));
            }
            sb.AppendLine("Stacktrace");
            sb.AppendLine(ex.StackTrace);

            sb.AppendLine("Innereception");
            sb.AppendLine(exceptionToString(ex.InnerException));
            return sb.ToString();
        }

        private static String entityValidationExceptionToString(DbEntityValidationException ex)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(String.Format("Validation Error: {0}", ex.Message));
            foreach (var failure in ex.EntityValidationErrors)
            {
                sb.AppendLine(String.Format("{0} failed validation\n", failure.Entry.Entity.GetType()));
                foreach (var error in failure.ValidationErrors)
                {
                    sb.AppendLine(String.Format("    - {0} : {1}", error.PropertyName, error.ErrorMessage));
                }
            }
            return sb.ToString();
        }
    }
}
