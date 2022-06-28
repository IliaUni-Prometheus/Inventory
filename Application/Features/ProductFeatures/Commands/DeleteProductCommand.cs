using static Shared.CQRSInfrastructure;

namespace Application.Features.ProductFeatures.Commands
{
    public class DeleteProductCommand : ICommand<bool?>
    {
        public int Id { get; private set; }

        public DeleteProductCommand(int id) { this.Id = id; }
    }
}
