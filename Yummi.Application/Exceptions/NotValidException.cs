namespace Yummi.Application.Exceptions
{
    public class NotValidException:Exception
    {
        internal NotValidException()
        {
            Errors = new List<string>();
        }

        internal NotValidException(string message) : base(message)
        {
            Errors = new List<string>();
        }

        internal NotValidException(string message, Exception inner) : base(message, inner)
        {
            Errors = new List<string>();
        }
        public List<string> Errors { get; }
    }
}
