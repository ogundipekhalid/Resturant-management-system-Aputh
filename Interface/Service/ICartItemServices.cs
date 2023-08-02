using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Models.Dtos;
using RDMS.Models.Entity;

namespace RDMS.Interface.Service
{
    public interface ICartItemServices
    {
        Task<BaseResponce<CartItemDto>> AddCartItem(AddCartItemViewModel model);
        Task<BaseResponce<IEnumerable<CartItemDto>>> GetUnpaidCartItemByUserId();
        Task<InitializePaymentResponseModel> GetUnpaidCartItemByUserIds();
        BaseResponce<CartItemDto> DeleteCartItem(int userId, int id);
        BaseResponce<IEnumerable<CartItemDto>> GetAllCartItem();
        BaseResponce<CartItemDto> GetCartItem(int id);
        BaseResponce<IEnumerable<CartItemDto>> GetCartItemByUserId(int userId);
        BaseResponce<IEnumerable<CartItemDto>> GetCartItemByBranchId(int branchId);
        BaseResponce<CartItemDto> RemoveCartItem(int id);
        BaseResponce<CartItemDto> Update(int userId, UpdateCartItemViewModel model);
    }
}