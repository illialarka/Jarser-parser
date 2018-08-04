namespace Jarser.ErrorsProcessing
{
    public class Error
    {
        public Error(string caption, string errorText)
        {
            Caption = caption;
            ErrorText = errorText;
        }

        public string Caption { get; set; }

        public string ErrorText { get; set; }
    }
}
