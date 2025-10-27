using Security.Models;
using Security.Models.DTOS;

namespace Security.Services
{
    public interface IHospitalService
    {
        Task<IEnumerable<Hospital>> GetAll();
        Task<Hospital> GetOne(Guid id);
        Task<Hospital> CreateHospital(CreateHospitalDto dto);
        Task<IEnumerable<Hospital>> GetAllHospitalsType13();
        Task<bool> UpdateHospital(Guid id, UpdateHospitalDto dto);
        Task<bool> DeleteHospital(Guid id);
    }
}
