using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TechnicalTest.Interfaces;
using TechnicalTest.Models;

namespace TechnicalTest.Controllers
{
    [Authorize]
    [ApiController]
    [Route("domino/[action]")]
    public class DominoController : ControllerBase
    {
        private IDomino _domino;
        public DominoController(IDomino process)
        {
            _domino = process;
        }

        [HttpPost]
        public dynamic OrderPieces(List<PieceDto> pieces) { 
        
            return _domino.OrderPieces(pieces);
        }
        [HttpGet]
        public dynamic Test(List<PieceDto> pieces)
        {

            var data = _domino.OrderPieces(pieces);
            
            return data;
        }
    }
}
