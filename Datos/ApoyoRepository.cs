using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

using Entidad;

namespace Datos
{
    public class ApoyoRepository
    {
        private readonly SqlConnection _connection;
        private readonly List<Apoyo> _apoyos = new List<Apoyo>();
        public ApoyoRepository(ConnectionManager connection)
        {
            _connection = connection._conexion;
        }
        public void Guardar(Apoyo apoyo)
        {


            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"Insert Into Apoyo (Modalidad,Fecha, Aporte) 
                                        values (@Modalidad,@Fecha,@Aporte)";
                command.Parameters.AddWithValue("@Modalidad", apoyo.modalidad);
                command.Parameters.AddWithValue("@Fecha", apoyo.fecha);
                command.Parameters.AddWithValue("@Aporte", apoyo.Aporte);
                var filas = command.ExecuteNonQuery();
            }
        }


         public void Modificar(Apoyo apoyo)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"UPDATE Apoyo SET Modalidad = @Modalidad, Fecha = @Fecha, Aporte = @Aporte where Identificacion = @Identificacion";
                command.Parameters.AddWithValue("@Modalidad", apoyo.modalidad);
                command.Parameters.AddWithValue("@Fecha", apoyo.fecha);
                command.Parameters.AddWithValue("@Aporte", apoyo.Aporte);
                var filas = command.ExecuteNonQuery();
            }
            
        }


        public List<Apoyo> ConsultarTodos()
        {
            SqlDataReader dataReader;
            List<Apoyo> apoyos = new List<Apoyo>();
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "Select * from Apoyo ";
                dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Apoyo apoyo = DataReaderMapToPerson(dataReader);
                        apoyos.Add(apoyo);
                    }
                }
            }
            
            return apoyos;
        }

        private Apoyo DataReaderMapToPerson(SqlDataReader dataReader)
        {
            if(!dataReader.HasRows) return null;
            Apoyo apoyo = new Apoyo();
            apoyo.modalidad = (string)dataReader["Modalidad"];
            apoyo.fecha = (string)dataReader["Fecha"];
            apoyo.Aporte = (string)dataReader["Aporte"];
            
            return apoyo;
        }

    }
}