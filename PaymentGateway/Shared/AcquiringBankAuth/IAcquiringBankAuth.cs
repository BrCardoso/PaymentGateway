namespace Application.Shared.AcquiringBank
{
    public interface IAcquiringBankAuth
    {
        Task<string> GetAuthorizationTokenAsync(CancellationToken cancellationToken);
    }
}