using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VolumeWebService.DataAccess;
using VolumeWebService.Models;
using VolumeWebService.VolumeCalculator;

namespace VolumeWebService.Controllers
{
    [ApiController]
    [Route("calculate")]
    
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculator _calculator;

        public CalculatorController(ICalculator calculator)
        {
            _calculator = calculator;
        }
        
        [HttpGet]
        [Route("cylinder")]
        public ActionResult<VolumeResult> GetCylinderVolume(decimal height, decimal radius)
        {
            return CreateVolumeResult(height, radius, Type.Cylinder);
        }

        [HttpPost]
        [Route("cylinder")]
        public async Task<ActionResult<VolumeResult>> InsertCylinder(decimal height, decimal radius)
        {
            return await InsertVolumeResult(CreateVolumeResult(height, radius, Type.Cylinder));
        }
        
        [HttpGet]
        [Route("cone")]
        public ActionResult<VolumeResult> GetConeVolume(decimal height, decimal radius)
        {
            return CreateVolumeResult(height, radius, Type.Cone);
        }
        
        [HttpPost]
        [Route("cone")]
        public async Task<ActionResult<VolumeResult>> InsertCone(decimal height, decimal radius)
        {
            return await InsertVolumeResult(CreateVolumeResult(height, radius, Type.Cone));
        }

        [HttpGet]
        public ActionResult<List<VolumeResult>> GetAllVolumeResult()
        {
            using VolumeDBContext dbContext = new VolumeDBContext();
            
            return dbContext.VolumeResults.ToList();
        }

        private VolumeResult CreateVolumeResult(decimal height, decimal radius, Type type)
        {
            return new VolumeResult
            {
                Height = height,
                Radius = radius,
                Type = type,
                Volume = type == Type.Cone ? _calculator.CalculateVolumeCone(height, radius) : _calculator.CalculateVolumeCylinder(height, radius)
            };
        }

        private async Task<VolumeResult> InsertVolumeResult(VolumeResult volumeResultToInsert)
        {
            using (VolumeDBContext dbContext = new VolumeDBContext())
            {
                await dbContext.VolumeResults.AddAsync(volumeResultToInsert);
                await dbContext.SaveChangesAsync();
            }

            return volumeResultToInsert;
        }
    }
}