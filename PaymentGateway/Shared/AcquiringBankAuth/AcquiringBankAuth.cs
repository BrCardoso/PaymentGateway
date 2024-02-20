namespace Application.Shared.AcquiringBank
{
    public class AcquiringBankAuth : IAcquiringBankAuth
    {
        public async Task<string> GetAuthorizationTokenAsync(CancellationToken cancellationToken)
        {
            //Simulate token creation to perform operations with the acquiring bank
            return await Task.FromResult("ushuHSKJAJXJN");
        }
    }
}
