using Dental_H.Model;
using Dental_H.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Dental_H.DAO
{
    public class OdontogramaDAO
    {
        public List<OdontogramaRegistro> ObtenerHistorial(int idPaciente)
        {
            List<OdontogramaRegistro> registros = new List<OdontogramaRegistro>();
            using (MySqlConnection conexion = Conexion.obtenerConexion())
            {
                conexion.Open();
                AsegurarEsquema(conexion);
                string sql = @"SELECT h.id_historial_diente AS id_registro, d.id_paciente,
                                      d.numero AS numero_pieza, d.tipo, h.estado, h.diagnostico,
                                      h.tratamiento, h.fecha_registro,
                                      h.fecha_procedimiento AS fecha_padecimiento
                               FROM HistorialDiente h
                               INNER JOIN Diente d ON d.id_diente = h.id_diente
                               WHERE d.id_paciente = @idPaciente
                               ORDER BY h.fecha_registro DESC, h.id_historial_diente DESC";
                MySqlCommand comando = new MySqlCommand(sql, conexion);
                comando.Parameters.AddWithValue("@idPaciente", idPaciente);
                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        registros.Add(LeerRegistro(reader));
                    }
                }
            }
            return registros;
        }

        public OdontogramaRegistro ObtenerUltimoRegistro(int idPaciente, string numeroPieza)
        {
            using (MySqlConnection conexion = Conexion.obtenerConexion())
            {
                conexion.Open();
                AsegurarEsquema(conexion);
                string sql = @"SELECT h.id_historial_diente AS id_registro, d.id_paciente,
                                      d.numero AS numero_pieza, d.tipo, h.estado, h.diagnostico,
                                      h.tratamiento, h.fecha_registro,
                                      h.fecha_procedimiento AS fecha_padecimiento
                               FROM HistorialDiente h
                               INNER JOIN Diente d ON d.id_diente = h.id_diente
                               WHERE d.id_paciente = @idPaciente AND d.numero = @numeroPieza
                               ORDER BY h.fecha_registro DESC, h.id_historial_diente DESC
                               LIMIT 1";
                MySqlCommand comando = new MySqlCommand(sql, conexion);
                comando.Parameters.AddWithValue("@idPaciente", idPaciente);
                comando.Parameters.AddWithValue("@numeroPieza", numeroPieza);
                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    OdontogramaRegistro registro = LeerRegistro(reader);
                    reader.Close();
                    registro.Marcas = ObtenerMarcas(conexion, registro.IdRegistro);
                    return registro;
                }
            }
        }

        public bool GuardarRegistro(OdontogramaRegistro registro)
        {
            using (MySqlConnection conexion = Conexion.obtenerConexion())
            {
                conexion.Open();
                AsegurarEsquema(conexion);
                MySqlTransaction transaccion = conexion.BeginTransaction();
                try
                {
                    int idDiente = ObtenerOCrearDiente(conexion, transaccion, registro);
                    string insertarHistorial = @"INSERT INTO HistorialDiente
                        (id_diente, fecha_registro, fecha_procedimiento, diagnostico, estado, tratamiento)
                        VALUES (@idDiente, @fechaRegistro, @fechaPadecimiento, @diagnostico, @estado, @tratamiento)";
                    MySqlCommand historial = new MySqlCommand(insertarHistorial, conexion, transaccion);
                    historial.Parameters.AddWithValue("@idDiente", idDiente);
                    historial.Parameters.AddWithValue("@fechaRegistro", registro.FechaRegistro);
                    historial.Parameters.AddWithValue("@fechaPadecimiento", registro.FechaPadecimiento);
                    historial.Parameters.AddWithValue("@diagnostico", registro.Diagnostico);
                    historial.Parameters.AddWithValue("@estado", registro.Estado);
                    historial.Parameters.AddWithValue("@tratamiento", registro.Tratamiento);
                    historial.ExecuteNonQuery();
                    long idHistorial = historial.LastInsertedId;

                    foreach (MarcaOdontograma marca in registro.Marcas)
                    {
                        string insertarMarca = @"INSERT INTO MarcaOdontograma
                            (id_historial_diente, tipo_padecimiento, superficie, posicion_x, posicion_y)
                            VALUES (@idHistorial, @padecimiento, @superficie, @posicionX, @posicionY)";
                        MySqlCommand comandoMarca = new MySqlCommand(insertarMarca, conexion, transaccion);
                        comandoMarca.Parameters.AddWithValue("@idHistorial", idHistorial);
                        comandoMarca.Parameters.AddWithValue("@padecimiento", marca.Padecimiento);
                        comandoMarca.Parameters.AddWithValue("@superficie", marca.Superficie);
                        comandoMarca.Parameters.AddWithValue("@posicionX", Convert.ToInt32(marca.PosicionX * 10000));
                        comandoMarca.Parameters.AddWithValue("@posicionY", Convert.ToInt32(marca.PosicionY * 10000));
                        comandoMarca.ExecuteNonQuery();
                    }

                    transaccion.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    MessageBox.Show(ex.Message, "Error al guardar odontograma", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public bool EliminarRegistro(int idRegistro)
        {
            using (MySqlConnection conexion = Conexion.obtenerConexion())
            {
                conexion.Open();
                AsegurarEsquema(conexion);
                MySqlTransaction transaccion = conexion.BeginTransaction();
                try
                {
                    MySqlCommand marcas = new MySqlCommand("DELETE FROM MarcaOdontograma WHERE id_historial_diente = @idRegistro", conexion, transaccion);
                    marcas.Parameters.AddWithValue("@idRegistro", idRegistro);
                    marcas.ExecuteNonQuery();
                    MySqlCommand historial = new MySqlCommand("DELETE FROM HistorialDiente WHERE id_historial_diente = @idRegistro", conexion, transaccion);
                    historial.Parameters.AddWithValue("@idRegistro", idRegistro);
                    historial.ExecuteNonQuery();
                    transaccion.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    MessageBox.Show(ex.Message, "Error al eliminar registro dental", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private int ObtenerOCrearDiente(MySqlConnection conexion, MySqlTransaction transaccion, OdontogramaRegistro registro)
        {
            MySqlCommand buscar = new MySqlCommand(
                "SELECT id_diente FROM Diente WHERE id_paciente = @idPaciente AND numero = @numero LIMIT 1",
                conexion,
                transaccion);
            buscar.Parameters.AddWithValue("@idPaciente", registro.IdPaciente);
            buscar.Parameters.AddWithValue("@numero", registro.NumeroPieza);
            object existente = buscar.ExecuteScalar();

            if (existente != null)
            {
                int idDiente = Convert.ToInt32(existente);
                MySqlCommand actualizar = new MySqlCommand(
                    "UPDATE Diente SET tipo = @tipo, estado = @estado WHERE id_diente = @idDiente",
                    conexion,
                    transaccion);
                actualizar.Parameters.AddWithValue("@tipo", registro.Tipo);
                actualizar.Parameters.AddWithValue("@estado", registro.Estado);
                actualizar.Parameters.AddWithValue("@idDiente", idDiente);
                actualizar.ExecuteNonQuery();
                return idDiente;
            }

            MySqlCommand insertar = new MySqlCommand(
                "INSERT INTO Diente (numero, tipo, estado, id_paciente) VALUES (@numero, @tipo, @estado, @idPaciente)",
                conexion,
                transaccion);
            insertar.Parameters.AddWithValue("@numero", registro.NumeroPieza);
            insertar.Parameters.AddWithValue("@tipo", registro.Tipo);
            insertar.Parameters.AddWithValue("@estado", registro.Estado);
            insertar.Parameters.AddWithValue("@idPaciente", registro.IdPaciente);
            insertar.ExecuteNonQuery();
            return Convert.ToInt32(insertar.LastInsertedId);
        }

        private List<MarcaOdontograma> ObtenerMarcas(MySqlConnection conexion, int idRegistro)
        {
            List<MarcaOdontograma> marcas = new List<MarcaOdontograma>();
            MySqlCommand comando = new MySqlCommand(
                @"SELECT superficie, tipo_padecimiento, posicion_x, posicion_y
                  FROM MarcaOdontograma WHERE id_historial_diente = @idRegistro",
                conexion);
            comando.Parameters.AddWithValue("@idRegistro", idRegistro);
            using (MySqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    marcas.Add(new MarcaOdontograma
                    {
                        Superficie = reader["superficie"].ToString(),
                        Padecimiento = reader["tipo_padecimiento"].ToString(),
                        PosicionX = Convert.ToSingle(reader["posicion_x"]) / 10000f,
                        PosicionY = Convert.ToSingle(reader["posicion_y"]) / 10000f
                    });
                }
            }
            return marcas;
        }

        private OdontogramaRegistro LeerRegistro(MySqlDataReader reader)
        {
            return new OdontogramaRegistro
            {
                IdRegistro = Convert.ToInt32(reader["id_registro"]),
                IdPaciente = Convert.ToInt32(reader["id_paciente"]),
                NumeroPieza = reader["numero_pieza"].ToString(),
                Tipo = reader["tipo"].ToString(),
                Estado = reader["estado"].ToString(),
                Diagnostico = reader["diagnostico"].ToString(),
                Tratamiento = reader["tratamiento"].ToString(),
                FechaRegistro = reader["fecha_registro"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(reader["fecha_registro"]),
                FechaPadecimiento = reader["fecha_padecimiento"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(reader["fecha_padecimiento"])
            };
        }

        private void AsegurarEsquema(MySqlConnection conexion)
        {
            AgregarColumnaSiFalta(conexion, "HistorialDiente", "tratamiento", "VARCHAR(180) NULL");
            AgregarColumnaSiFalta(conexion, "MarcaOdontograma", "superficie", "VARCHAR(20) NOT NULL DEFAULT 'Vertical'");
        }

        private void AgregarColumnaSiFalta(MySqlConnection conexion, string tabla, string columna, string definicion)
        {
            string existeSql = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
                                 WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = @tabla AND COLUMN_NAME = @columna";
            MySqlCommand existe = new MySqlCommand(existeSql, conexion);
            existe.Parameters.AddWithValue("@tabla", tabla);
            existe.Parameters.AddWithValue("@columna", columna);
            if (Convert.ToInt32(existe.ExecuteScalar()) > 0)
            {
                return;
            }

            new MySqlCommand("ALTER TABLE " + tabla + " ADD COLUMN " + columna + " " + definicion, conexion).ExecuteNonQuery();
        }
    }
}
