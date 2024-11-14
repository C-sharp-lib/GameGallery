using GameGallery.Models;

namespace GameGallery.Services
{
    public interface IShoppingCartService
    {
        void AddToCart(Games game, int quantity);
        void RemoveFromCart(int gameId);
        void ClearCart();
        List<CartItem> GetCartItems();
        decimal GetTotalPrice();
        int GetTotalQuantity();
    }
}
