using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyselfStaj2.Classes;
using MyselfStaj2.Interfaces;
using MyselfStaj2.Services;

namespace MyselfStaj2.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class EntityController : ControllerBase 
    {
        private readonly IEntityService<Coordinate> _service;
        public EntityController(IEntityService<Coordinate> service)
        {

            _service = service;
        }

        [HttpGet]

        public Response GetAll()
        {
            return _service.GetAllCoordinate();
        }

        [HttpGet]
        public Response GetById(int id)
        {
           return _service.GetCoordinateById(id);
        }
        [HttpPost]

        public Response Insert(int id, Coordinate c)
        {
            return _service.InsertCoordinate(id,c);
        }

        [HttpPut]
        public Response Update(int id, Coordinate c)
        {
            return _service.UpdateCoordinate(id,c);

        }

        [HttpDelete]

        public Response Delete(int id)
        {
            return _service.DeleteCoordinate(id);
        }
    }
}
