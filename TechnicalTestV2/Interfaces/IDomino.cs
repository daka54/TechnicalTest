using TechnicalTest.Models;

namespace TechnicalTest.Interfaces
{
    public interface IDomino
    {
        dynamic OrderPieces(List<PieceDto> pieces);
    }
}
