namespace cleanArchitecture.Domain.Routes.BaseRouter
{
    public partial class Router
    {
        public class CartRouter : Router
        {
            private const string Prefix = Rule + "Cart";
            public const string Add = Prefix + "/items";
            public const string Update = Prefix + "/items/{itemId}";
            public const string Remove = Prefix + "/items/{itemId}";
            public const string GetCart = Prefix + "/{sessionId}";
            public const string ClearCart = Prefix + "/{sessionId}/clear";
        }
    }
}
