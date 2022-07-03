using Utilities;


namespace MailSystem
{
    public abstract class MailContent
    {
        protected string content;

        protected abstract void GenerateContent();

        public override string ToString()
        {
            return content;
        }

    }
}