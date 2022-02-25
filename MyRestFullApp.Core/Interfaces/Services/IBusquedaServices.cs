using MyRestFullApp.Core.Dtos;
using MyRestFullApp.Core.Dtos.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyRestFullApp.Core.Interfaces.Services
{
    public interface IBusquedaServices
    {
        Task<BaseObjectResultDto<BusquedaDto>> GetArticulos(string pais);
    }
}
