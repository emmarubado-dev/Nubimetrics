using MyRestFullApp.Core.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRestFullApp.Core.Dtos.Results
{
    public class BaseObjectResultDto<TBdo> : BaseResultDto
       where TBdo : BaseDto
    {
        /// <summary>
        /// Elemento del resultado
        /// </summary>
        public TBdo Element { get; set; }
    }
}
