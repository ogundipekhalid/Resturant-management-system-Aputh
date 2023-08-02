using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Dtos;

namespace RDMS.Payment
{
    public interface IPaystackServices
    {
        Task<InitializePaymentResponseModel> InitializePayment(InitializePaymentRequestModel model);
        Task<VerifyAccountNumberResponseModel> VerifyAccountNumber(
            VerifyAccountNumberRequestModel model
        );
        Task<CreateTransferRecipientResponseModel> CreateTransferRecipient(
            CreateTransferRecipientRequestModel model
        );
        Task<VerifyTransactionResponseModel> VerifyTransaction(string referenceNumber);
        Task<TransferMoneyToUserResponseModel> TransferMoneyToUser(
            CreateTransferRecipientResponseModel model
        );
    }
}
