namespace ButaAPI.Services.Concretes
{
    public class ModifyText
    {
        public string NormalizeName(string name)
        {
            var correctName = name.ToLower();
            return correctName = char.ToUpper(correctName[0]) + correctName.Substring(1);
        }
    }
}
