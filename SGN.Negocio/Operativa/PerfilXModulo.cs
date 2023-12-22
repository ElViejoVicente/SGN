﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGN.Negocio.Operativa
{
    [Serializable]
    public class PerfilXModulo
    {
        #region Propiedades
        public int IdPerfil { get; set; } = 0;
        public int IdModulo { get; set; } = 0;
        public Boolean Creado { get; set; } = false;
        #endregion
    }
}
