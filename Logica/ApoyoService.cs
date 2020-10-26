using System;
using System.Collections.Generic;
using Datos;
using Entidad;

namespace Logica
{
    public class ApoyoService
    {
        private readonly ConnectionManager _conexion;
        private readonly ApoyoRepository _repositorio;
        public ApoyoService(string connectionString)
        {
            _conexion = new ConnectionManager(connectionString);
            _repositorio = new ApoyoRepository(_conexion);
        }

        public GuardarApoyoResponse Guardar(Apoyo apoyo)
        {
            try
            {
                _conexion.Open();
                apoyo.IdApoyo = _repositorio.Guardar(apoyo);
                _conexion.Close();
                return new GuardarApoyoResponse(apoyo);
            }
            catch (Exception e)
            {
                return new GuardarApoyoResponse($"Error de la Aplicacion: {e.Message}");
            }
            finally { _conexion.Close(); }
        }


       


        public class GuardarApoyoResponse
        {
            public GuardarApoyoResponse(Apoyo apoyo)
            {
                Error = false;
                Apoyo = apoyo;
            }
            public GuardarApoyoResponse(string mensaje)
            {
                Error = true;
                Mensaje = mensaje;
            }
            public bool Error { get; set; }
            public string Mensaje { get; set; }
            public Apoyo Apoyo { get; set; }
        }

        public List<Apoyo> ConsultarTodos()
        {
            _conexion.Open();
            List<Apoyo> apoyos = _repositorio.ConsultarTodos();
            _conexion.Close();
            return apoyos;
        }

        public Apoyo ConsultarByPersona(string id){
            
            _conexion.Open();
            Apoyo apoyo = _repositorio.ConsultaByPersona(id);
            _conexion.Close();
            return apoyo;
            
        }

    }
}