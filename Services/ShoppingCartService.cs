using GameGallery.Models;
using Newtonsoft.Json;

namespace GameGallery.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string SessionKey = "Cart";

        public ShoppingCartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ISession Session => _httpContextAccessor.HttpContext.Session;

        private List<CartItem> GetCart()
        {
            var cartJson = Session.GetString(SessionKey);
            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItem>();
            }
            return JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
        }

        private void SaveCart(List<CartItem> cart)
        {
            Session.SetString(SessionKey, JsonConvert.SerializeObject(cart));
        }

        public void AddToCart(Games game, int quantity)
        {
            var cart = GetCart();

            var existingItem = cart.FirstOrDefault(c => c.GameId == game.GameId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem
                {
                    GameId = game.GameId,
                    Quantity = 1,
                    Price = game.Price.Value,
                    Games = game
                });
            }

            SaveCart(cart);
        }

        public void RemoveFromCart(int gameId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.GameId == gameId);
            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }
        }

        public void ClearCart()
        {
            Session.Remove(SessionKey);
        }

        public List<CartItem> GetCartItems()
        {
            return GetCart();
        }

        public decimal GetTotalPrice()
        {
            return GetCart().Sum(c => c.Price * c.Quantity);
        }

        public int GetTotalQuantity() 
       {
            return GetCart().Sum(c => c.Quantity);
        }
    }
}