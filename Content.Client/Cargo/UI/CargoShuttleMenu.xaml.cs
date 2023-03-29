using Content.Client.UserInterface.Controls;
using Content.Shared.Cargo;
using Content.Shared.Cargo.Prototypes;
using Robust.Client.AutoGenerated;
using Robust.Client.GameObjects;
using Robust.Client.UserInterface.XAML;
using Robust.Shared.Prototypes;
using Robust.Shared.Timing;

namespace Content.Client.Cargo.UI
{
    [GenerateTypedNameReferences]
    public sealed partial class CargoShuttleMenu : FancyWindow
    {
        private readonly IPrototypeManager _protoManager;
        private readonly SpriteSystem _spriteSystem;

        public CargoShuttleMenu(IPrototypeManager protoManager, SpriteSystem spriteSystem)
        {
            RobustXamlLoader.Load(this);
            _protoManager = protoManager;
            _spriteSystem = spriteSystem;
            Title = Loc.GetString("cargo-shuttle-console-menu-title");
        }

        public void SetAccountName(string name)
        {
            AccountNameLabel.Text = name;
        }

        public void SetShuttleName(string name)
        {
            ShuttleNameLabel.Text = name;
        }

        public void SetOrders(List<CargoOrderData> orders)
        {
            Orders.DisposeAllChildren();

            foreach (var order in orders)
            {
                 var product = _protoManager.Index<CargoProductPrototype>(order.ProductId);
                 var productName = product.Name;

                 var row = new CargoOrderRow
                 {
                     Order = order,
                     Icon = { Texture = _spriteSystem.Frame0(product.Icon) },
                     ProductName =
                     {
                         Text = Loc.GetString(
                             "cargo-console-menu-populate-orders-cargo-order-row-product-name-text",
                             ("productName", productName),
                             ("orderAmount", order.OrderQuantity - order.NumDispatched),
                             ("orderRequester", order.Requester))
                     },
                     Description = {Text = Loc.GetString("cargo-console-menu-order-reason-description",
                         ("reason", order.Reason))}
                 };

                 row.Approve.Visible = false;
                 row.Cancel.Visible = false;

                 Orders.AddChild(row);
            }
        }
    }
}
