﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.Common
{
    public class Message
    {
        public static string PeticionOk { get; } = "Petición Exitosa"; 
        public static string SinDocumentos { get; } = "No hay Documento de respaldo"; 
        public static string EntidadNull { get; } = "Entidad Nula";
        public static string ErrorServidor { get; } = "Error interno del servidor: ";
        public static string IdNull { get; } = "ID Invalido";
        public static string ParticipanteCrear { get; } = "Creando Participante";
        public static string GetParticipanteById{ get; } = "Get Participante";

    }
}
