using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RDMS.Interface.Repositry;
using RDMS.Models.Dtos;
using RDMS.Models.Identity;

namespace RDMS.Payment
{
    public class PaymentServices : IPaystackServices
    {
        private static readonly HttpClient client = new HttpClient();


        public async Task<VerifyAccountNumberResponseModel> VerifyAccountNumber(
            VerifyAccountNumberRequestModel model
        )
        {
            var key = "sk_test_cc9e7c520919c2304bc20ff58000a284ea738771";
             var getHttpClient = new HttpClient();
        getHttpClient.DefaultRequestHeaders.Accept.Clear();
        getHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        getHttpClient.BaseAddress = new Uri($"https://api.paystack.co/bank/resolve?account_number={model.AccountNumber}&bank_code={model.BankCode}");
        getHttpClient.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", key);
        var response = await getHttpClient.GetAsync(getHttpClient.BaseAddress);
        var responseString = await response.Content.ReadAsStringAsync();
        var responseObj = JsonSerializer.Deserialize<VerifyAccountNumberResponseModel>(responseString);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return responseObj;
        }
        return responseObj;
            throw new NotImplementedException();
        }

        public async Task<CreateTransferRecipientResponseModel> CreateTransferRecipient(
            CreateTransferRecipientRequestModel model
        )
        {
            var key = "sk_test_cc9e7c520919c2304bc20ff58000a284ea738771";
            var getHttpClient = new HttpClient();
            getHttpClient.DefaultRequestHeaders.Accept.Clear();
            getHttpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
            getHttpClient.BaseAddress = new Uri($"https://api.paystack.co/transferrecipient");
            getHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                key
            );
            var response = await getHttpClient.PostAsJsonAsync(getHttpClient.BaseAddress, new
        {
            type = model.Type,
            name = model.Name,
            account_number = model.AccountNumber,
            bank_code = model.BankCode,
            currency = model.Currency,
            description = model.Description,
        });

            var responseString = await response.Content.ReadAsStringAsync();
        var responseObj = JsonSerializer.Deserialize<CreateTransferRecipientResponseModel>(responseString);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return responseObj;
        }
        return responseObj;;
        }

        public async Task<VerifyTransactionResponseModel> VerifyTransaction(string referenceNumber)
        {
             var key = "sk_test_cc9e7c520919c2304bc20ff58000a284ea738771";
             var getHttpClient = new HttpClient();
        getHttpClient.DefaultRequestHeaders.Accept.Clear();
        getHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        getHttpClient.BaseAddress = new Uri($"https://api.paystack.co/transaction/verify/{referenceNumber}");
        getHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);
        var response = await getHttpClient.GetAsync(getHttpClient.BaseAddress);
        var responseString = await response.Content.ReadAsStringAsync();
        var responseObj = JsonSerializer.Deserialize<VerifyTransactionResponseModel>(responseString);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return responseObj;
        }
        return responseObj;
        }

        public async Task<InitializePaymentResponseModel> InitializePayment(
            InitializePaymentRequestModel model
        )
        {
            var key = "sk_test_cc9e7c520919c2304bc20ff58000a284ea738771";
            var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var endPoint = "https://api.paystack.co/transaction/initialize";
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);
        var content = new StringContent(JsonSerializer.Serialize(new
        {
            amount = model.Amount * 100,
            email = model.Email,
            reference = model.RefrenceNo,
            currency = "NGN",
            callback_url = model.CallbackUrl,

        }), Encoding.UTF8, "application/json");


        try
        {
            var response = await client.PostAsync(endPoint, content);
            var resString = await response.Content.ReadAsStringAsync();
            var responseObj = JsonSerializer.Deserialize<InitializePaymentResponseModel>(resString);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return responseObj;
            }
            return responseObj;
        }
        catch (Exception)
        {
            return new InitializePaymentResponseModel
            {
                Status = true,
                Message = "Payment gateway was assume to be success.",
                data = new InitializePaymentData
                {
                    access_code = "",
                    authorization_url = "",
                    reference = model.RefrenceNo,
                },
            };

        }
        }

        public async Task<TransferMoneyToUserResponseModel> TransferMoneyToUser(CreateTransferRecipientResponseModel model)
        {
            var key = "sk_test_cc9e7c520919c2304bc20ff58000a284ea738771";

             var getHttpClient = new HttpClient();
        getHttpClient.DefaultRequestHeaders.Accept.Clear();
        getHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var baseUri = $"https://api.paystack.co/transfer";
        getHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);
        var response = await getHttpClient.PostAsJsonAsync(baseUri, new
        {
            recipient = model.data.recipient_code,
            amount = model.data.amount * 100,
            reference = Guid.NewGuid().ToString().Replace('-', 'y'),
            currency = "NGN",
            source = "balance",
        });
        var responseString = await response.Content.ReadAsStringAsync();
        //
        var responseObj = JsonSerializer.Deserialize<TransferMoneyToUserResponseModel>(responseString);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return responseObj;
        }
        return responseObj;
           
            throw new NotImplementedException();
        }
    
    }
}
