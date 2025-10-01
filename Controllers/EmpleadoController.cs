using ApiPlantilla.Data;
using ApiPlantilla.Models.catEmpleados;
using Microsoft.AspNetCore.Mvc;


namespace prjAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly claCatEmpleadosData _objempleadoData;
        public EmpleadoController(claCatEmpleadosData objempleadoData)
        {
            _objempleadoData = objempleadoData;
        }
        [HttpGet]
        public async Task<IActionResult> listar()
        {
            try
            {
                List<clsCatEmpleados> lista = await _objempleadoData.ListaEmpleados();
                return StatusCode(StatusCodes.Status200OK, lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{idEmpleado}")]
        public async Task<IActionResult> Obtener(int idEmpleado)
        {
            try
            {
                clsCatEmpleados objEmpleado = await _objempleadoData.ObtenerEmpleado(idEmpleado);
                return StatusCode(StatusCodes.Status200OK, objEmpleado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> crear([FromBody] clsCatEmpleados objEmpleado)
        {
            try
            {
                bool respuesta = await _objempleadoData.CrearEmpleado(objEmpleado);
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> editar([FromBody] clsCatEmpleados objEmpleado)
        {
            try
            {
                bool respuesta = await _objempleadoData.EditarEmpleado(objEmpleado);
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{IdEmpleado}")]
        public async Task<IActionResult> eliminar(int IdEmpleado)
        {
            try
            {
                bool respuesta = await _objempleadoData.EliminarEmpleado(IdEmpleado);
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}