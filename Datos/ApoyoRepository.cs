using System.ComponentModel.Design;
using System.Data;
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
        public int Guardar(Apoyo apoyo)
        {

            //Select TOP(1) Id from conexinal.dbo.Facturas ORDER BY Id DESC;
            SqlDataReader dataReader;
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"Insert Into Apoyo (Modalidad,Fecha, Aporte,Persona) 
                                        values (@Modalidad,@Fecha,@Aporte,@Persona)";
                command.Parameters.AddWithValue("@Modalidad", apoyo.modalidad);
                command.Parameters.AddWithValue("@Fecha", apoyo.fecha);
                command.Parameters.AddWithValue("@Aporte", apoyo.Aporte);
                command.Parameters.AddWithValue("@Persona", apoyo.Persona.Identificacion);
                var filas = command.ExecuteNonQuery();

                command.CommandText = @"Select TOP(1) Id_Apoyo from Apoyo ORDER BY Id_Apoyo DESC;";
                dataReader = command.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        apoyo.IdApoyo = (int)dataReader["Id_Apoyo"];
                    }
                }              
            }

            return apoyo.IdApoyo;
        }


        public void Modificar(Apoyo apoyo)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"UPDATE Apoyo SET Modalidad = @Modalidad, Fecha = @Fecha, Aporte = @Aporte, Persona = @Persona where Id_Apoyo = @Id_Apoyo";
                command.Parameters.AddWithValue("@Modalidad", apoyo.modalidad);
                command.Parameters.AddWithValue("@Fecha", apoyo.fecha);
                command.Parameters.AddWithValue("@Aporte", apoyo.Aporte);
                command.Parameters.AddWithValue("@Persona", apoyo.Persona.Identificacion);
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
                        Apoyo apoyo = DataReaderMapToApoyo(dataReader);
                        apoyos.Add(apoyo);
                    }
                }
            }
            
            return apoyos;
        }

        public Apoyo ConsultaByPersona(string id)
        {
            SqlDataReader dataReader;
            Apoyo apoyo = new Apoyo();
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "Select * from Apoyo WHERE Persona = @param";
                command.Parameters.AddWithValue("@param", id);
                dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        apoyo = DataReaderMapToApoyo(dataReader);
                    }
                }
            }
            
            return apoyo;
        }

        private Apoyo DataReaderMapToApoyo(SqlDataReader dataReader)
        {
            if(!dataReader.HasRows) return null;
            Apoyo apoyo = new Apoyo();
            apoyo.IdApoyo = (int)dataReader["Id_Apoyo"];
            apoyo.modalidad = (string)dataReader["Modalidad"];
            apoyo.fecha = (string)dataReader["Fecha"];
            apoyo.Aporte = (string)dataReader["Aporte"];
            apoyo.Persona = new Persona() { Identificacion = (string)dataReader["Persona"] };
            return apoyo;
        }

    }
}