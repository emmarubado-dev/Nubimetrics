using MyRestFullApp.Core.Dtos;
using MyRestFullApp.Core.Dtos.Results;
using MyRestFullApp.Core.Interfaces;
using MyRestFullApp.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyRestFullApp.Core.Services
{
    public class PaisesServices : IPaisesServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaisesServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseObjectResultDto<PaisDto>> GetPaisML(string pais)
        {
            var paisDto = new BaseObjectResultDto<PaisDto>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var task = await httpClient.GetAsync($"https://api.mercadolibre.com/classified_locations/countries/{pais}");
                    var taskResponse = await task.Content.ReadAsStringAsync();

                    if (String.IsNullOrEmpty(taskResponse))
                    {
                        paisDto.WarningMessages = "No se encontraron datos";
                        paisDto.TotalRows = 0;
                        paisDto.Result = true;
                        paisDto.Element = null;

                        return paisDto;
                    }
                    paisDto.Result = true;
                    paisDto.Element = Newtonsoft.Json.JsonConvert.DeserializeObject<PaisDto>(taskResponse);

                    return paisDto;
                }
            }
            catch(Exception ex)
            {
                paisDto.TotalRows = 0;
                paisDto.Element = null;
                paisDto.ErrorMessages = ex.Message;
                paisDto.Result = false;
            }
            return paisDto;
        }
    }
}
