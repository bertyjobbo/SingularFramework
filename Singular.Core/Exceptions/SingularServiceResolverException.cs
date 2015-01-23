using System;

namespace Singular.Core.Exceptions
{
    public class SingularServiceResolverException<T>: Exception where T:class
    {
        private readonly string _message = "Dependency " + typeof (T) + " could not be resolved. ";

        public SingularServiceResolverException(string message) : base()
        {
            _message = _message + message;
        }

        public override string Message
        {
            get { return _message; }
        }
    }
}
