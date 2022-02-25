using System;
using System.Collections.Generic;
using System.Text;

namespace MyRestFullApp.Core.Dtos.Results
{
    public class BaseResultDto
    {
        public BaseResultDto()
        {
            this.SuccessMessages = "";
            this.WarningMessages = "";
            this.ErrorMessages = "";
        }

        /// <summary>
        /// Resultado de la operación.
        /// </summary>
        /// <value>True el resultado es correcto</value>
        /// <value>False el resultado es incorrecto</value>
        public bool Result { get; set; }

        /// <summary>
        /// Mensaje de éxito
        /// </summary>
        public string SuccessMessages { get; set; }

        /// <summary>
        /// Mensaje de advertencia
        /// </summary>
        public string WarningMessages { get; set; }

        /// <summary>
        /// Mensaje de Error
        /// </summary>
        public string ErrorMessages { get; set; }

        /// <summary>
        /// Número de registros total para el filtro especificado
        /// </summary>
        /// <remarks>Tiene sentido en los resultados que devuelven un listado</remarks>
        public int TotalRows { get; set; }
    }
}
