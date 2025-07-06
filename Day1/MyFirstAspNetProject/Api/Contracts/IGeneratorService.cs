namespace Api.Contracts;

public interface IGeneratorService
{
    string GenerateString(int length);
    int GenerateInt(int min, int max);
}
