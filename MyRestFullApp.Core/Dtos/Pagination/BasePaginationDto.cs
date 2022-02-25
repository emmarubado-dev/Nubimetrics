using System;
using System.Collections.Generic;
using System.Text;

namespace MyRestFullApp.Core.Dtos.Pagination
{
    public class BasePaginationDto
    {
        /// <summary>
        /// Página actual que se está mostrando
        /// </summary>
        public int? CurrentPage { get; set; }

        /// <summary>
        /// Número de registros/filas por página
        /// </summary>
        public int? RowsPerPage { get; set; }

    }
}
