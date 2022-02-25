using MyRestFullApp.Core.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRestFullApp.Core.Dtos.Results
{
    public class BaseListingResultDto<TBdo> : BaseResultDto
        where TBdo : BaseDto
    {
        public BaseListingResultDto()
        {
            this.Elements = new List<TBdo>();
        }

        /// <summary>
        /// Listado de elementos del resultado
        /// </summary>
        public ICollection<TBdo> Elements { get; set; }
    }
}
