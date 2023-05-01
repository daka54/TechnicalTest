using TechnicalTest.Interfaces;
using TechnicalTest.Models;

namespace TechnicalTest.Services
{
    public class dominoService : IDomino
    {
        public dynamic OrderPieces(List<PieceDto> pieces)
        {
            var count = pieces.Count();
            var road = new List<PieceDto>();
            var chains = new List<List<PieceDto>>();
            dynamic response;
            foreach (var piece in pieces)
            {
                //Valida que los puntos de las fichas esten entre 0 y 6
                response = ValidatePieces(piece);
                if (response != null)
                {
                    return response;
                }
                //Calida que la cantidad de fichas este entre 2 y 6
                if (!Enumerable.Range(2, 6).Contains(pieces.Count)) {
                    response = "Cantidad de fichas invalida(Min 2, Max 6)";
                    return response;
                }
            }
            //Llama un metodo recursivo que me retorna una lista de todas las combinaciones validas con las fichas dadas
            chains = dominoChain(road, pieces, new List<List<PieceDto>>());

            //Obtenida la lista de todas las combinaciones validas con las fichas dadas las recorremos hasta encontrar
            //la primera que cumpla con la condicion de que el primer punto de la primer ficha y el ultimo de la ultima ficha iguales
            foreach (var chain in chains)
            {
                if (ValidateChain(chain, count)) 
                {
                    return chain;
                }
            }
            response = "El ejercicio no tiene solucion";
            return response;
        }

        public List<List<PieceDto>> dominoChain(List<PieceDto> chain, List<PieceDto> list, List<List<PieceDto>> result)
        {
            
            for (int i = 0; i < list.Count; i++) 
            { 
                PieceDto piece = list[i];
                
                if (CanAdd(piece, chain))
                {
                    chain.Add(piece);
                    PieceDto aux = list[i];
                    list.RemoveAt(i);
                    dominoChain(chain, list, result);
                    list.Insert(i, aux);
                    chain.RemoveAt(chain.Count - 1);
                }

                piece = Flipped(piece);

                if (CanAdd(piece, chain))
                {
                    chain.Add(piece);
                    PieceDto aux = list[i];
                    list.RemoveAt(i);
                    dominoChain(chain, list, result);
                    list.Insert(i, aux);
                    chain.RemoveAt(chain.Count - 1);
                }                
                
            }

            result.Add(new List<PieceDto> (chain));
            Console.WriteLine(PrintChain(chain));
            return result;
        }

        //Valida que los puntos de las fichas esten entre 0 y 6
        public bool ValidateChain(List<PieceDto> pieces, int count)
        {
            if (pieces.Count == count)
            {
                if (pieces[0].sideA == pieces[count  - 1].sideB)
                {
                    return true;
                }
            }
            return false;
        }
        public dynamic ValidatePieces(PieceDto piece)
        {
            dynamic response = null;
            if (!Enumerable.Range(0, 6).Contains(piece.sideA) || !Enumerable.Range(0, 6).Contains(piece.sideB))
            {
                response = "Los Puntos de las Fichas deben estar entre 0 y 6"; 
            }
            return response;
        }

        //Pasa el lado a al lado b y el b al a
        public PieceDto Flipped(PieceDto piece)
        {
            PieceDto pieceFlipped = new PieceDto();
            pieceFlipped.sideA = piece.sideB; pieceFlipped.sideB = piece.sideA;
            return pieceFlipped;
        }

        //Valida si la ficha se puede agregar a la cadena
        public bool CanAdd(PieceDto piece, List<PieceDto> chain)
        {
            return chain == null || chain.Count == 0 || chain.Last().sideB == piece?.sideA;
        }


        //Imprime todas las cadenas en consola
        public string PrintChain(List<PieceDto> pieces)
        {
            string chain = "";
            foreach(PieceDto piece in pieces)
            {
                chain += $"[{piece.sideA.ToString()}/{piece.sideB.ToString()}] ";
            }
            return chain;
        }
    }
}
