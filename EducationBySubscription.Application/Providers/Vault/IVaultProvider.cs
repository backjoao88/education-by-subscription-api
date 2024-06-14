namespace EducationBySubscription.Application.Providers.Vault;

public interface IVaultProvider
{
    public string GetSecret(string name);
}