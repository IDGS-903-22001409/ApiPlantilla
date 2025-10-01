using ApiPlantilla.Models.catEmpleados;
using System.Data;
using System.Data.SqlClient;

namespace ApiPlantilla.Data
{
    public class claCatEmpleadosData
    {
        private readonly string conexion;

        public claCatEmpleadosData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<clsCatEmpleados>> ListaEmpleados()
        {
            List<clsCatEmpleados> lista = new List<clsCatEmpleados>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();

                SqlCommand cmd = new SqlCommand("sp_listaEmpleado", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new clsCatEmpleados
                        {
                            IdEmpleado = Convert.ToInt32(reader["idEmpleado"]),
                            strNombreCompleto = reader["NombreCompleto"].ToString(),
                            strCorreo = reader["Correo"].ToString(),                            
                            strSueldo = Convert.ToDouble(reader["Sueldo"]),
                            strFechaContratacion = reader["FechaContrato"].ToString(),
                            strEstatus = reader["Estatus"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<clsCatEmpleados?> ObtenerEmpleado(int pintIdEmpleado)
        {
            clsCatEmpleados? empleado = null;

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();

                SqlCommand cmd = new SqlCommand("sp_obtenerEmpleado", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@IdEmpleado", pintIdEmpleado);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        empleado = new clsCatEmpleados
                        {
                            IdEmpleado = Convert.ToInt32(reader["idEmpleado"]),
                            strNombreCompleto = reader["NombreCompleto"].ToString(),
                            strCorreo = reader["Correo"].ToString(),
                            strSueldo = Convert.ToDouble(reader["Sueldo"]),
                            strFechaContratacion = reader["FechaContrato"].ToString(),
                            strEstatus = reader["Estatus"].ToString()
                        };
                    }
                }
            }

            return empleado;
        }

        public async Task<bool> CrearEmpleado(clsCatEmpleados pobjEmpleado)
        {
            bool bolRespuesta = true;
            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_crearEmpleado", con);
                cmd.Parameters.AddWithValue("@NombreCompleto", pobjEmpleado.strNombreCompleto);
                cmd.Parameters.AddWithValue("@Correo", pobjEmpleado.strCorreo);
                cmd.Parameters.AddWithValue("@Sueldo", pobjEmpleado.strSueldo);                
                cmd.Parameters.AddWithValue("@FechaContrato", DateTime.Parse(pobjEmpleado.strFechaContratacion));
                cmd.Parameters.AddWithValue("@Estatus", pobjEmpleado.strEstatus);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    bolRespuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return bolRespuesta;
        }

        public async Task<bool> EditarEmpleado(clsCatEmpleados pobjEmpleado)
        {
            bool bolRespuesta = true;
            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_editarEmpleado", con);
                cmd.Parameters.AddWithValue("@IdEmpleado", pobjEmpleado.IdEmpleado);
                cmd.Parameters.AddWithValue("@NombreCompleto", pobjEmpleado.strNombreCompleto);
                cmd.Parameters.AddWithValue("@Correo", pobjEmpleado.strCorreo);
                cmd.Parameters.AddWithValue("@Sueldo", pobjEmpleado.strSueldo);
                cmd.Parameters.AddWithValue("@FechaContrato", pobjEmpleado.strFechaContratacion);
                cmd.Parameters.AddWithValue("@Estatus", pobjEmpleado.strEstatus);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    bolRespuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return bolRespuesta;
        }

        public async Task<bool> EliminarEmpleado(int pobjEmpleado)
        {
            bool bolRespuesta = true;
            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_eliminarEmpleado", con);
                cmd.Parameters.AddWithValue("@IdEmpleado", pobjEmpleado);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    bolRespuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return bolRespuesta;
        }
    }
}
