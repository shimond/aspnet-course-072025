using Api.Contracts;

namespace Api.Services;

public class GeneratorImplementationService : IGeneratorService
{
    public string GenerateString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random random = new Random();
        char[] stringChars = new char[length];
        for (int i = 0; i < length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }
        return new string(stringChars);
    }

    public int GenerateInt(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }

    public GeneratorImplementationService()
    {
            
    }

}
