using Microsoft.Extensions.Configuration;
using Music.World.Common;
using Music.World.DTO;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Music.World.Service
{

    public class MusicService : IMusicService
    {
        private readonly IConfiguration _config;

        public MusicService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> GetMusicDetails()
        {
            ProxyService proxy = new ProxyService(_config["Settings:BaseUrl"]);
            var response = proxy.Get("api/v1/festivals");

            if (!response.IsSuccessStatusCode)
            {
                var asyncResult = response.Content.ReadAsStringAsync().Result;

                Log.Error("MusicService, StatusCode: {0}, Description: {1}, Result: {2}", response.StatusCode, response.ReasonPhrase, asyncResult);
                throw new HttpRequestException("Error occured" + asyncResult);
            }

            var result = await response.Content.ReadAsStringAsync();

            return MapToRecordDetails(result);
        }

        private string MapToRecordDetails(string musicDetails)
        {
            if (!string.IsNullOrWhiteSpace(musicDetails))
            {
                var musicDetailsDTO = JsonConvert.DeserializeObject<List<MusicDetailsDTO>>(musicDetails);

                var recordDetailsDtoList = new List<RecordDetailsDTO>();

                foreach (var item in musicDetailsDTO)
                {
                    foreach (var band in item.Bands)
                    {
                        var festivalName = string.IsNullOrWhiteSpace(item.Name) ? "Blank" : item.Name;
                        var recordLabel = string.IsNullOrWhiteSpace(band.RecordLabel) ? "Blank" : band.RecordLabel;
                        var bandName = string.IsNullOrWhiteSpace(band.Name) ? "Blank" : band.Name;

                        try
                        {
                            var recordDetailsDto = recordDetailsDtoList.FirstOrDefault(r => r.RecordName.Contains(recordLabel));

                            if (recordDetailsDto == null)
                            {
                                recordDetailsDtoList.Add(new RecordDetailsDTO
                                {
                                    RecordName = recordLabel,
                                    Bands = new List<Band>
                                            {
                                                new Band
                                                {
                                                    Name = bandName,
                                                    Festivals = new List<string> { festivalName }
                                                }
                                            }
                                });
                            }
                            else
                            {
                                var bandDetails = recordDetailsDto.Bands.FirstOrDefault(b => b.Name == bandName);

                                if (bandDetails == null)
                                {
                                    recordDetailsDto.Bands.Add(new Band { Name = bandName, Festivals = new List<string> { festivalName } });
                                }
                                else
                                {
                                    bandDetails.Festivals.Add(festivalName);
                                }
                            }
                        }


                        catch (Exception ex)
                        {
                            var a = ex.Message;
                            throw ex;
                        }
                    }
                }

                return JsonConvert.SerializeObject(recordDetailsDtoList);
            }

            return null;
        }
    }
}
