using System.ComponentModel.DataAnnotations;

namespace EnglishApiClient.Infrastructure.ValidationAttributes
{
    public class OnlyLettersAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var str = value as string;
            if (str != null)
            {
                return str.All(c => char.IsWhiteSpace(c) || char.IsLetter(c));
            }
            return false;
        }
    }
}
